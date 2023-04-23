using System;

namespace FTAnalyzer.Forms
{
    partial class Notes
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notes));
            this.rtbNotes = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbNotes
            // 
            this.rtbNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNotes.Location = new System.Drawing.Point(0, 0);
            this.rtbNotes.Name = "rtbNotes";
            this.rtbNotes.Size = new System.Drawing.Size(611, 495);
            this.rtbNotes.TabIndex = 0;
            this.rtbNotes.Text = "";
            // 
            // Notes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 495);
            this.Controls.Add(this.rtbNotes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Notes";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.Notes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbNotes;
    }
}