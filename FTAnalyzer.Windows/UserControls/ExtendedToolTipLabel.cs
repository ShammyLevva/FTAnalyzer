using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.UserControls
{
    public partial class ExtendedToolTipLabel : Label
    {
        readonly ToolTip _ToolTip;

        public ExtendedToolTipLabel()
        {
            InitializeComponent();
            _ToolTip = new ToolTip();
            MouseLeave += new EventHandler(ExtendedToolTipLabel_MouseLeave);
            MouseHover += new EventHandler(ExtendedToolTipLabel_MouseHover);
        }

        void ExtendedToolTipLabel_MouseHover(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ToolTipText))
            {
                int x = PointToClient(MousePosition).X + 10;
                _ToolTip.Show(ToolTipText, this, new Point(x, -10));
            }
        }

        public string ToolTipText { get; set; }

        void ExtendedToolTipLabel_MouseLeave(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ToolTipText))
            {
                _ToolTip.Hide(this);
            }
        }
    }
}
