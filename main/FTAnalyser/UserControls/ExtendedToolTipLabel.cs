﻿using System;
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
        private ToolTip _ToolTip;
        private string _ToolTipText;

        public ExtendedToolTipLabel()
        {
            InitializeComponent();
            _ToolTip = new ToolTip();
            this.MouseLeave += new EventHandler(ExtendedToolTipLabel_MouseLeave);
            this.MouseHover += new EventHandler(ExtendedToolTipLabel_MouseHover);
        }

        void ExtendedToolTipLabel_MouseHover(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_ToolTipText))
            {
                int x = PointToClient(MousePosition).X + 10;
                _ToolTip.Show(_ToolTipText, this, new Point(x, -10));
            }
        }

        public string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; }
        }

        void ExtendedToolTipLabel_MouseLeave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_ToolTipText))
            {
                _ToolTip.Hide(this);
            }
        }
    }
}
