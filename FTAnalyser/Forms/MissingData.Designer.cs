namespace FTAnalyzer.Forms
{
    partial class MissingData
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mediaSlider1 = new MediaSlider.MediaSlider();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mediaSlider1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 279);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Birth";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 435);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(638, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mediaSlider1
            // 
            this.mediaSlider1.Animated = false;
            this.mediaSlider1.AnimationSize = 0.1F;
            this.mediaSlider1.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.mediaSlider1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.mediaSlider1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.mediaSlider1.BackColor = System.Drawing.Color.Transparent;
            this.mediaSlider1.BackgroundImage = null;
            this.mediaSlider1.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mediaSlider1.ButtonBorderColor = System.Drawing.Color.Black;
            this.mediaSlider1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mediaSlider1.ButtonCornerRadius = ((uint)(4u));
            this.mediaSlider1.ButtonSize = new System.Drawing.Size(14, 14);
            this.mediaSlider1.ButtonStyle = MediaSlider.MediaSlider.ButtonType.Round;
            this.mediaSlider1.ContextMenuStrip = null;
            this.mediaSlider1.LargeChange = 2;
            this.mediaSlider1.Location = new System.Drawing.Point(7, 20);
            this.mediaSlider1.Margin = new System.Windows.Forms.Padding(0);
            this.mediaSlider1.Maximum = 10;
            this.mediaSlider1.Minimum = 0;
            this.mediaSlider1.Name = "mediaSlider1";
            this.mediaSlider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.mediaSlider1.ShowButtonOnHover = false;
            this.mediaSlider1.Size = new System.Drawing.Size(150, 150);
            this.mediaSlider1.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.mediaSlider1.SmallChange = 1;
            this.mediaSlider1.SmoothScrolling = false;
            this.mediaSlider1.TabIndex = 0;
            this.mediaSlider1.TickColor = System.Drawing.Color.DarkGray;
            this.mediaSlider1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mediaSlider1.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.mediaSlider1.TrackBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mediaSlider1.TrackDepth = 6;
            this.mediaSlider1.TrackFillColor = System.Drawing.Color.Transparent;
            this.mediaSlider1.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.mediaSlider1.TrackShadow = false;
            this.mediaSlider1.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.mediaSlider1.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.mediaSlider1.Value = 0;
            // 
            // MissingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 457);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MissingData";
            this.Text = "Missing Data Configuration";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private MediaSlider.MediaSlider mediaSlider1;
    }
}