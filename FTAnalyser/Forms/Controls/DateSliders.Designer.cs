using System;

namespace FTAnalyzer.Forms.Controls
{
    partial class DateSliders
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception) { }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.msExact = new MediaSlider.MediaSlider();
            this.label6 = new System.Windows.Forms.Label();
            this.msApprox = new MediaSlider.MediaSlider();
            this.label5 = new System.Windows.Forms.Label();
            this.msJustYear = new MediaSlider.MediaSlider();
            this.label4 = new System.Windows.Forms.Label();
            this.msNarrow = new MediaSlider.MediaSlider();
            this.label3 = new System.Windows.Forms.Label();
            this.msWide = new MediaSlider.MediaSlider();
            this.label2 = new System.Windows.Forms.Label();
            this.msVeryWide = new MediaSlider.MediaSlider();
            this.label1 = new System.Windows.Forms.Label();
            this.msUnknown = new MediaSlider.MediaSlider();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.msExact);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.msApprox);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.msJustYear);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.msNarrow);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.msWide);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.msVeryWide);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.msUnknown);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(328, 156);
            this.groupBox.TabIndex = 29;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "groupBox1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Exact Date";
            // 
            // msExact
            // 
            this.msExact.Animated = false;
            this.msExact.AnimationSize = 0.2F;
            this.msExact.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msExact.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msExact.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msExact.BackColor = System.Drawing.SystemColors.Control;
            this.msExact.BackgroundImage = null;
            this.msExact.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msExact.ButtonBorderColor = System.Drawing.Color.Black;
            this.msExact.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msExact.ButtonCornerRadius = ((uint)(4u));
            this.msExact.ButtonSize = new System.Drawing.Size(14, 14);
            this.msExact.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msExact.ContextMenuStrip = null;
            this.msExact.LargeChange = 2;
            this.msExact.Location = new System.Drawing.Point(5, 129);
            this.msExact.Margin = new System.Windows.Forms.Padding(2);
            this.msExact.Maximum = 100;
            this.msExact.Minimum = 0;
            this.msExact.Name = "msExact";
            this.msExact.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msExact.ShowButtonOnHover = false;
            this.msExact.Size = new System.Drawing.Size(221, 14);
            this.msExact.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msExact.SmallChange = 1;
            this.msExact.SmoothScrolling = false;
            this.msExact.TabIndex = 41;
            this.msExact.TickColor = System.Drawing.Color.DarkGray;
            this.msExact.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msExact.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msExact.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msExact.TrackDepth = 6;
            this.msExact.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msExact.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msExact.TrackShadow = false;
            this.msExact.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msExact.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msExact.Value = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Approx Date";
            // 
            // msApprox
            // 
            this.msApprox.Animated = false;
            this.msApprox.AnimationSize = 0.2F;
            this.msApprox.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msApprox.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msApprox.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msApprox.BackColor = System.Drawing.SystemColors.Control;
            this.msApprox.BackgroundImage = null;
            this.msApprox.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msApprox.ButtonBorderColor = System.Drawing.Color.Black;
            this.msApprox.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msApprox.ButtonCornerRadius = ((uint)(4u));
            this.msApprox.ButtonSize = new System.Drawing.Size(14, 14);
            this.msApprox.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msApprox.ContextMenuStrip = null;
            this.msApprox.LargeChange = 2;
            this.msApprox.Location = new System.Drawing.Point(5, 111);
            this.msApprox.Margin = new System.Windows.Forms.Padding(2);
            this.msApprox.Maximum = 100;
            this.msApprox.Minimum = 0;
            this.msApprox.Name = "msApprox";
            this.msApprox.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msApprox.ShowButtonOnHover = false;
            this.msApprox.Size = new System.Drawing.Size(221, 14);
            this.msApprox.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msApprox.SmallChange = 1;
            this.msApprox.SmoothScrolling = false;
            this.msApprox.TabIndex = 39;
            this.msApprox.TickColor = System.Drawing.Color.DarkGray;
            this.msApprox.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msApprox.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msApprox.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msApprox.TrackDepth = 6;
            this.msApprox.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msApprox.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msApprox.TrackShadow = false;
            this.msApprox.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msApprox.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msApprox.Value = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Just Year";
            // 
            // msJustYear
            // 
            this.msJustYear.Animated = false;
            this.msJustYear.AnimationSize = 0.2F;
            this.msJustYear.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msJustYear.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msJustYear.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msJustYear.BackColor = System.Drawing.SystemColors.Control;
            this.msJustYear.BackgroundImage = null;
            this.msJustYear.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msJustYear.ButtonBorderColor = System.Drawing.Color.Black;
            this.msJustYear.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msJustYear.ButtonCornerRadius = ((uint)(4u));
            this.msJustYear.ButtonSize = new System.Drawing.Size(14, 14);
            this.msJustYear.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msJustYear.ContextMenuStrip = null;
            this.msJustYear.LargeChange = 2;
            this.msJustYear.Location = new System.Drawing.Point(5, 93);
            this.msJustYear.Margin = new System.Windows.Forms.Padding(2);
            this.msJustYear.Maximum = 100;
            this.msJustYear.Minimum = 0;
            this.msJustYear.Name = "msJustYear";
            this.msJustYear.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msJustYear.ShowButtonOnHover = false;
            this.msJustYear.Size = new System.Drawing.Size(221, 14);
            this.msJustYear.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msJustYear.SmallChange = 1;
            this.msJustYear.SmoothScrolling = false;
            this.msJustYear.TabIndex = 37;
            this.msJustYear.TickColor = System.Drawing.Color.DarkGray;
            this.msJustYear.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msJustYear.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msJustYear.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msJustYear.TrackDepth = 6;
            this.msJustYear.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msJustYear.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msJustYear.TrackShadow = false;
            this.msJustYear.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msJustYear.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msJustYear.Value = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Narrow Date";
            // 
            // msNarrow
            // 
            this.msNarrow.Animated = false;
            this.msNarrow.AnimationSize = 0.2F;
            this.msNarrow.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msNarrow.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msNarrow.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msNarrow.BackColor = System.Drawing.SystemColors.Control;
            this.msNarrow.BackgroundImage = null;
            this.msNarrow.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msNarrow.ButtonBorderColor = System.Drawing.Color.Black;
            this.msNarrow.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msNarrow.ButtonCornerRadius = ((uint)(4u));
            this.msNarrow.ButtonSize = new System.Drawing.Size(14, 14);
            this.msNarrow.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msNarrow.ContextMenuStrip = null;
            this.msNarrow.LargeChange = 2;
            this.msNarrow.Location = new System.Drawing.Point(5, 75);
            this.msNarrow.Margin = new System.Windows.Forms.Padding(2);
            this.msNarrow.Maximum = 100;
            this.msNarrow.Minimum = 0;
            this.msNarrow.Name = "msNarrow";
            this.msNarrow.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msNarrow.ShowButtonOnHover = false;
            this.msNarrow.Size = new System.Drawing.Size(221, 14);
            this.msNarrow.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msNarrow.SmallChange = 1;
            this.msNarrow.SmoothScrolling = false;
            this.msNarrow.TabIndex = 35;
            this.msNarrow.TickColor = System.Drawing.Color.DarkGray;
            this.msNarrow.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msNarrow.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msNarrow.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msNarrow.TrackDepth = 6;
            this.msNarrow.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msNarrow.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msNarrow.TrackShadow = false;
            this.msNarrow.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msNarrow.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msNarrow.Value = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Wide Date";
            // 
            // msWide
            // 
            this.msWide.Animated = false;
            this.msWide.AnimationSize = 0.2F;
            this.msWide.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msWide.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msWide.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msWide.BackColor = System.Drawing.SystemColors.Control;
            this.msWide.BackgroundImage = null;
            this.msWide.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msWide.ButtonBorderColor = System.Drawing.Color.Black;
            this.msWide.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msWide.ButtonCornerRadius = ((uint)(4u));
            this.msWide.ButtonSize = new System.Drawing.Size(14, 14);
            this.msWide.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msWide.ContextMenuStrip = null;
            this.msWide.LargeChange = 2;
            this.msWide.Location = new System.Drawing.Point(5, 57);
            this.msWide.Margin = new System.Windows.Forms.Padding(2);
            this.msWide.Maximum = 100;
            this.msWide.Minimum = 0;
            this.msWide.Name = "msWide";
            this.msWide.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msWide.ShowButtonOnHover = false;
            this.msWide.Size = new System.Drawing.Size(221, 14);
            this.msWide.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msWide.SmallChange = 1;
            this.msWide.SmoothScrolling = false;
            this.msWide.TabIndex = 33;
            this.msWide.TickColor = System.Drawing.Color.DarkGray;
            this.msWide.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msWide.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msWide.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msWide.TrackDepth = 6;
            this.msWide.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msWide.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msWide.TrackShadow = false;
            this.msWide.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msWide.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msWide.Value = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Very Wide Date";
            // 
            // msVeryWide
            // 
            this.msVeryWide.Animated = false;
            this.msVeryWide.AnimationSize = 0.2F;
            this.msVeryWide.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msVeryWide.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msVeryWide.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msVeryWide.BackColor = System.Drawing.SystemColors.Control;
            this.msVeryWide.BackgroundImage = null;
            this.msVeryWide.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msVeryWide.ButtonBorderColor = System.Drawing.Color.Black;
            this.msVeryWide.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msVeryWide.ButtonCornerRadius = ((uint)(4u));
            this.msVeryWide.ButtonSize = new System.Drawing.Size(14, 14);
            this.msVeryWide.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msVeryWide.ContextMenuStrip = null;
            this.msVeryWide.LargeChange = 2;
            this.msVeryWide.Location = new System.Drawing.Point(5, 39);
            this.msVeryWide.Margin = new System.Windows.Forms.Padding(2);
            this.msVeryWide.Maximum = 100;
            this.msVeryWide.Minimum = 0;
            this.msVeryWide.Name = "msVeryWide";
            this.msVeryWide.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msVeryWide.ShowButtonOnHover = false;
            this.msVeryWide.Size = new System.Drawing.Size(221, 14);
            this.msVeryWide.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msVeryWide.SmallChange = 1;
            this.msVeryWide.SmoothScrolling = false;
            this.msVeryWide.TabIndex = 31;
            this.msVeryWide.TickColor = System.Drawing.Color.DarkGray;
            this.msVeryWide.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msVeryWide.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msVeryWide.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msVeryWide.TrackDepth = 6;
            this.msVeryWide.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msVeryWide.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msVeryWide.TrackShadow = false;
            this.msVeryWide.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msVeryWide.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msVeryWide.Value = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Unknown Date";
            // 
            // msUnknown
            // 
            this.msUnknown.Animated = false;
            this.msUnknown.AnimationSize = 0.2F;
            this.msUnknown.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.msUnknown.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.msUnknown.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.msUnknown.BackColor = System.Drawing.SystemColors.Control;
            this.msUnknown.BackgroundImage = null;
            this.msUnknown.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.msUnknown.ButtonBorderColor = System.Drawing.Color.Black;
            this.msUnknown.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.msUnknown.ButtonCornerRadius = ((uint)(4u));
            this.msUnknown.ButtonSize = new System.Drawing.Size(14, 14);
            this.msUnknown.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.msUnknown.ContextMenuStrip = null;
            this.msUnknown.LargeChange = 2;
            this.msUnknown.Location = new System.Drawing.Point(5, 21);
            this.msUnknown.Margin = new System.Windows.Forms.Padding(2);
            this.msUnknown.Maximum = 100;
            this.msUnknown.Minimum = 0;
            this.msUnknown.Name = "msUnknown";
            this.msUnknown.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.msUnknown.ShowButtonOnHover = false;
            this.msUnknown.Size = new System.Drawing.Size(221, 14);
            this.msUnknown.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.msUnknown.SmallChange = 1;
            this.msUnknown.SmoothScrolling = false;
            this.msUnknown.TabIndex = 29;
            this.msUnknown.TickColor = System.Drawing.Color.DarkGray;
            this.msUnknown.TickStyle = System.Windows.Forms.TickStyle.None;
            this.msUnknown.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.msUnknown.TrackBorderColor = System.Drawing.SystemColors.ControlDark;
            this.msUnknown.TrackDepth = 6;
            this.msUnknown.TrackFillColor = System.Drawing.SystemColors.ControlLight;
            this.msUnknown.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.msUnknown.TrackShadow = false;
            this.msUnknown.TrackShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msUnknown.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.msUnknown.Value = 0;
            // 
            // DateSliders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "DateSliders";
            this.Size = new System.Drawing.Size(334, 163);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label7;
        private MediaSlider.MediaSlider msExact;
        private System.Windows.Forms.Label label6;
        private MediaSlider.MediaSlider msApprox;
        private System.Windows.Forms.Label label5;
        private MediaSlider.MediaSlider msJustYear;
        private System.Windows.Forms.Label label4;
        private MediaSlider.MediaSlider msNarrow;
        private System.Windows.Forms.Label label3;
        private MediaSlider.MediaSlider msWide;
        private System.Windows.Forms.Label label2;
        private MediaSlider.MediaSlider msVeryWide;
        private System.Windows.Forms.Label label1;
        private MediaSlider.MediaSlider msUnknown;
    }
}
