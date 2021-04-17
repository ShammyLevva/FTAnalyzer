using System;

namespace FTAnalyzer.Forms.Controls
{
    partial class CensusCountry
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUSA = new System.Windows.Forms.RadioButton();
            this.rbCanada = new System.Windows.Forms.RadioButton();
            this.rbUK = new System.Windows.Forms.RadioButton();
            this.rbWales = new System.Windows.Forms.RadioButton();
            this.rbEngland = new System.Windows.Forms.RadioButton();
            this.rbScotland = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUSA);
            this.groupBox1.Controls.Add(this.rbCanada);
            this.groupBox1.Controls.Add(this.rbUK);
            this.groupBox1.Controls.Add(this.rbWales);
            this.groupBox1.Controls.Add(this.rbEngland);
            this.groupBox1.Controls.Add(this.rbScotland);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 72);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Census Country";
            // 
            // rbUSA
            // 
            this.rbUSA.AutoSize = true;
            this.rbUSA.Location = new System.Drawing.Point(79, 42);
            this.rbUSA.Name = "rbUSA";
            this.rbUSA.Size = new System.Drawing.Size(89, 17);
            this.rbUSA.TabIndex = 5;
            this.rbUSA.Text = "United States";
            this.rbUSA.UseVisualStyleBackColor = true;
            this.rbUSA.CheckedChanged += new System.EventHandler(this.RbUSA_CheckedChanged);
            // 
            // rbCanada
            // 
            this.rbCanada.AutoSize = true;
            this.rbCanada.Location = new System.Drawing.Point(6, 42);
            this.rbCanada.Name = "rbCanada";
            this.rbCanada.Size = new System.Drawing.Size(62, 17);
            this.rbCanada.TabIndex = 4;
            this.rbCanada.Text = "Canada";
            this.rbCanada.UseVisualStyleBackColor = true;
            this.rbCanada.CheckedChanged += new System.EventHandler(this.RbCanada_CheckedChanged);
            // 
            // rbUK
            // 
            this.rbUK.AutoSize = true;
            this.rbUK.Enabled = false;
            this.rbUK.Location = new System.Drawing.Point(210, 19);
            this.rbUK.Name = "rbUK";
            this.rbUK.Size = new System.Drawing.Size(40, 17);
            this.rbUK.TabIndex = 3;
            this.rbUK.Text = "UK";
            this.rbUK.UseVisualStyleBackColor = true;
            // 
            // rbWales
            // 
            this.rbWales.AutoSize = true;
            this.rbWales.Location = new System.Drawing.Point(149, 19);
            this.rbWales.Name = "rbWales";
            this.rbWales.Size = new System.Drawing.Size(55, 17);
            this.rbWales.TabIndex = 2;
            this.rbWales.Text = "Wales";
            this.rbWales.UseVisualStyleBackColor = true;
            this.rbWales.CheckedChanged += new System.EventHandler(this.RbWales_CheckedChanged);
            // 
            // rbEngland
            // 
            this.rbEngland.AutoSize = true;
            this.rbEngland.Location = new System.Drawing.Point(79, 19);
            this.rbEngland.Name = "rbEngland";
            this.rbEngland.Size = new System.Drawing.Size(64, 17);
            this.rbEngland.TabIndex = 1;
            this.rbEngland.Text = "England";
            this.rbEngland.UseVisualStyleBackColor = true;
            this.rbEngland.CheckedChanged += new System.EventHandler(this.RbEngland_CheckedChanged);
            // 
            // rbScotland
            // 
            this.rbScotland.AutoSize = true;
            this.rbScotland.Checked = true;
            this.rbScotland.Location = new System.Drawing.Point(6, 19);
            this.rbScotland.Name = "rbScotland";
            this.rbScotland.Size = new System.Drawing.Size(67, 17);
            this.rbScotland.TabIndex = 0;
            this.rbScotland.TabStop = true;
            this.rbScotland.Text = "Scotland";
            this.rbScotland.UseVisualStyleBackColor = true;
            this.rbScotland.CheckedChanged += new System.EventHandler(this.RbScotland_CheckedChanged);
            // 
            // CensusCountry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CensusCountry";
            this.Size = new System.Drawing.Size(259, 78);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbUSA;
        private System.Windows.Forms.RadioButton rbCanada;
        private System.Windows.Forms.RadioButton rbUK;
        private System.Windows.Forms.RadioButton rbWales;
        private System.Windows.Forms.RadioButton rbEngland;
        private System.Windows.Forms.RadioButton rbScotland;
    }
}
