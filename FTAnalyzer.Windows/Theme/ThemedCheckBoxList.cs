using System.Collections;

namespace FTAnalyzer.Theme
{
    // Drop-in replacement for CheckedListBox that presents the same call-site surface
    // (Items.Add/Clear/Contains/indexer, SetItemChecked/GetItemChecked, CheckedIndices,
    // CheckedItems, ColumnWidth) but is backed by real CheckBox controls inside an AutoScroll
    // FlowLayoutPanel instead of an OS-drawn ListBox.
    //
    // Why: Windows' dark-mode scrollbar theming (SetWindowTheme with "DarkMode_Explorer", see
    // NativeMethods.SetScrollBarTheme) does not support the classic ListBox window class that
    // CheckedListBox wraps - confirmed via Microsoft's own guidance (no documented SetWindowTheme
    // subclass works for ListBox scrollbars) and community writeups (the only working fix is
    // process-wide IAT-hooking uxtheme.dll, far too invasive for one control). A FlowLayoutPanel's
    // scrollbar is ordinary NC-area chrome on a generic window class, which - like TreeView and
    // plain Panel elsewhere in this app - responds correctly to SetWindowTheme.
    //
    // A checkbox's Click only fires on genuine user interaction (never from a programmatic
    // .Checked assignment), so ItemCheckedChanged only fires the same way the original
    // CheckedListBox's user-driven events did - bulk operations like "Select All"/"Clear All" or
    // initial population via SetItemChecked don't trigger it, matching the original control's
    // behavior (avoids redundant recompute/registry-write storms during those loops).
    public sealed class ThemedCheckBoxList : FlowLayoutPanel
    {
        readonly List<CheckBox> _checkBoxes = [];
        int _columnWidth;

        public ThemedCheckBoxList()
        {
            AutoScroll = true;
            FlowDirection = FlowDirection.TopDown;
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
                    checkBox.AutoSize = false;
                    checkBox.Width = value;
                }
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
                    Margin = new Padding(3),
                };
                checkBox.Click += (sender, _) => owner.ItemCheckedChanged?.Invoke(sender, EventArgs.Empty);
                owner._checkBoxes.Add(checkBox);
                owner.Controls.Add(checkBox);
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
