using System.Collections;

namespace FTAnalyzer.Theme
{
    // Drop-in replacement for CheckedListBox that presents the same call-site surface
    // (Items.Add/Clear/Contains/indexer, SetItemChecked/GetItemChecked, CheckedIndices,
    // CheckedItems, ColumnWidth) but is backed by real CheckBox controls inside an AutoScroll
    // Panel instead of an OS-drawn ListBox.
    //
    // Why: Windows' dark-mode scrollbar theming (SetWindowTheme with "DarkMode_Explorer", see
    // NativeMethods.SetScrollBarTheme) does not support the classic ListBox window class that
    // CheckedListBox wraps - confirmed via Microsoft's own guidance (no documented SetWindowTheme
    // subclass works for ListBox scrollbars) and community writeups (the only working fix is
    // process-wide IAT-hooking uxtheme.dll, far too invasive for one control). A plain Panel's
    // scrollbar is ordinary NC-area chrome on a generic window class, which - like TreeView and
    // plain Panel elsewhere in this app - responds correctly to SetWindowTheme.
    //
    // Items are positioned by hand (ArrangeItems) rather than via FlowLayoutPanel's own
    // WrapContents/FlowDirection.TopDown flow: that combination (especially together with
    // AutoScroll) is a long-documented WinForms layout bug where rows in a wrapped column overlap
    // instead of stacking - reproduced here for the multi-column ckbDataErrors list regardless of
    // font scale, spacing, or Margin tuning. Owning the layout outright removes that failure mode.
    //
    // A checkbox's Click only fires on genuine user interaction (never from a programmatic
    // .Checked assignment), so ItemCheckedChanged only fires the same way the original
    // CheckedListBox's user-driven events did - bulk operations like "Select All"/"Clear All" or
    // initial population via SetItemChecked don't trigger it, matching the original control's
    // behavior (avoids redundant recompute/registry-write storms during those loops).
    public sealed class ThemedCheckBoxList : Panel
    {
        const int RowGap = 6;
        const int ColumnGap = 8;

        readonly List<CheckBox> _checkBoxes = [];
        int _columnWidth;

        public ThemedCheckBoxList()
        {
            AutoScroll = true;
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            Items = new ItemCollection(this);
        }

        public ItemCollection Items { get; }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public int ColumnWidth
        {
            get => _columnWidth;
            set
            {
                _columnWidth = value;
                foreach (CheckBox checkBox in _checkBoxes)
                {
                    checkBox.AutoSize = value == 0;
                    checkBox.Width = value;
                }
                ArrangeItems();
            }
        }

        public event EventHandler? ItemCheckedChanged;

        public void SetItemChecked(int index, bool value) => _checkBoxes[index].Checked = value;

        public bool GetItemChecked(int index) => _checkBoxes[index].Checked;

        public IEnumerable<int> CheckedIndices
        {
            get
            {
                for (int i = 0; i < _checkBoxes.Count; i++)
                    if (_checkBoxes[i].Checked)
                        yield return i;
            }
        }

        public IReadOnlyCollection<object> CheckedItems =>
            [.. _checkBoxes.Where(checkBox => checkBox.Checked).Select(checkBox => checkBox.Tag!)];

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Native FlowLayoutPanel/Panel has no border by default - CheckedListBox's native
            // sunken-field border gave this area a visible boundary (especially important for
            // ckbFactExclude/ckbFactSelect, which have no surrounding GroupBox). Drawing it
            // ourselves (rather than BorderStyle.FixedSingle/Fixed3D) avoids the same native-chrome
            // color problem the scrollbar had - see VirtualDataGridView.ApplyColors's identical
            // note about Fixed3D ignoring app colors.
            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using Pen borderPen = new(ActiveColors.Border);
            e.Graphics.DrawRectangle(borderPen, rect);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ArrangeItems();
        }

        // Positions every checkbox explicitly instead of trusting FlowLayoutPanel's TopDown+wrap
        // flow (see the class remarks above). Single-column lists (ColumnWidth == 0, e.g.
        // ckbFactSelect/ckbFactExclude) just stack top-to-bottom; ColumnWidth != 0 (ckbDataErrors)
        // wraps into a new column once a column has filled the panel's visible height.
        void ArrangeItems()
        {
            if (_checkBoxes.Count == 0)
                return;

            // A fixed-ColumnWidth checkbox (AutoSize == false) needs its Height set explicitly,
            // but PreferredSize has to be read only after the checkbox is parented (Controls.Add
            // already ran by the time Add() calls this) - queried any earlier, an unparented
            // control's Font getter falls back to the ambient Control.DefaultFont instead of the
            // container's actual (themed/font-scaled) Font, so the Height came out wrong for the
            // text that's actually rendered and the top of each row's text got clipped.
            foreach (CheckBox checkBox in _checkBoxes)
                if (!checkBox.AutoSize)
                    checkBox.Height = checkBox.PreferredSize.Height;

            int rowHeight = _checkBoxes.Max(checkBox => checkBox.PreferredSize.Height) + RowGap;

            if (_columnWidth == 0)
            {
                for (int i = 0; i < _checkBoxes.Count; i++)
                    _checkBoxes[i].Location = new Point(RowGap / 2, i * rowHeight);
                return;
            }

            // ClientSize shrinks once a vertical scrollbar appears, which would shrink
            // rowsPerColumn, which could add a column, which could trigger the scrollbar to
            // disappear again - avoid that oscillation by sizing columns off the border-adjusted
            // control height rather than the scrollbar-dependent ClientSize.
            int usableHeight = Math.Max(rowHeight, Height - 2);
            int rowsPerColumn = Math.Max(1, usableHeight / rowHeight);
            for (int i = 0; i < _checkBoxes.Count; i++)
            {
                int column = i / rowsPerColumn;
                int row = i % rowsPerColumn;
                _checkBoxes[i].Location = new Point(column * (_columnWidth + ColumnGap), row * rowHeight);
            }
        }

        public sealed class ItemCollection(ThemedCheckBoxList owner) : IEnumerable
        {
            public int Count => owner._checkBoxes.Count;

            public object this[int index] => owner._checkBoxes[index].Tag!;

            public int Add(object item)
            {
                CheckBox checkBox = new()
                {
                    Text = item.ToString() ?? string.Empty,
                    Tag = item,
                    AutoSize = owner._columnWidth == 0,
                    Width = owner._columnWidth,
                };
                // AutoSize covers width+height for the single-column lists (ColumnWidth == 0), but a
                // fixed ColumnWidth forces AutoSize off, so nothing recalculates Height when
                // FontScaler.Apply later changes this checkbox's Font directly (it walks every
                // control in the tree, not just the container) - rows would keep their
                // construction-time Height and start overlapping at larger font scales. Height still
                // isn't part of AutoSize here, so it needs the same manual refresh on every font change.
                if (!checkBox.AutoSize)
                    checkBox.Height = checkBox.PreferredSize.Height;
                checkBox.FontChanged += (_, _) => owner.ArrangeItems();
                checkBox.Click += (sender, _) => owner.ItemCheckedChanged?.Invoke(sender, EventArgs.Empty);
                owner._checkBoxes.Add(checkBox);
                owner.Controls.Add(checkBox);
                owner.ArrangeItems();
                return owner._checkBoxes.Count - 1;
            }

            public void Clear()
            {
                foreach (CheckBox checkBox in owner._checkBoxes)
                    checkBox.Dispose();
                owner._checkBoxes.Clear();
            }

            public bool Contains(object item) => owner._checkBoxes.Any(checkBox => Equals(checkBox.Tag, item));

            public IEnumerator GetEnumerator()
            {
                foreach (CheckBox checkBox in owner._checkBoxes)
                    yield return checkBox.Tag!;
            }
        }
    }
}
