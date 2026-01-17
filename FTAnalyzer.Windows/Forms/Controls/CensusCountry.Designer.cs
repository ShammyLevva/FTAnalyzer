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
                if (disposing && (components is not null))
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
            groupBox1 = new GroupBox();
            rbUSA = new RadioButton();
            rbCanada = new RadioButton();
            rbUK = new RadioButton();
            rbWales = new RadioButton();
            rbEngland = new RadioButton();
            rbScotland = new RadioButton();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbUSA);
            groupBox1.Controls.Add(rbCanada);
            groupBox1.Controls.Add(rbUK);
            groupBox1.Controls.Add(rbWales);
            groupBox1.Controls.Add(rbEngland);
            groupBox1.Controls.Add(rbScotland);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(296, 83);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Census Country";
            groupBox1.Paint += groupBox1_Paint;
            // 
            // rbUSA
            // 
            rbUSA.AutoSize = true;
            rbUSA.Location = new Point(92, 48);
            rbUSA.Margin = new Padding(4, 3, 4, 3);
            rbUSA.Name = "rbUSA";
            rbUSA.Size = new Size(94, 19);
            rbUSA.TabIndex = 5;
            rbUSA.Text = "United States";
            rbUSA.UseVisualStyleBackColor = true;
            rbUSA.CheckedChanged += RbUSA_CheckedChanged;
            // 
            // rbCanada
            // 
            rbCanada.AutoSize = true;
            rbCanada.Location = new Point(7, 48);
            rbCanada.Margin = new Padding(4, 3, 4, 3);
            rbCanada.Name = "rbCanada";
            rbCanada.Size = new Size(65, 19);
            rbCanada.TabIndex = 4;
            rbCanada.Text = "Canada";
            rbCanada.UseVisualStyleBackColor = true;
            rbCanada.CheckedChanged += RbCanada_CheckedChanged;
            // 
            // rbUK
            // 
            rbUK.AutoSize = true;
            rbUK.Enabled = false;
            rbUK.Location = new Point(245, 22);
            rbUK.Margin = new Padding(4, 3, 4, 3);
            rbUK.Name = "rbUK";
            rbUK.Size = new Size(40, 19);
            rbUK.TabIndex = 3;
            rbUK.Text = "UK";
            rbUK.UseVisualStyleBackColor = true;
            // 
            // rbWales
            // 
            rbWales.AutoSize = true;
            rbWales.Location = new Point(174, 22);
            rbWales.Margin = new Padding(4, 3, 4, 3);
            rbWales.Name = "rbWales";
            rbWales.Size = new Size(56, 19);
            rbWales.TabIndex = 2;
            rbWales.Text = "Wales";
            rbWales.UseVisualStyleBackColor = true;
            rbWales.CheckedChanged += RbWales_CheckedChanged;
            // 
            // rbEngland
            // 
            rbEngland.AutoSize = true;
            rbEngland.Location = new Point(92, 22);
            rbEngland.Margin = new Padding(4, 3, 4, 3);
            rbEngland.Name = "rbEngland";
            rbEngland.Size = new Size(68, 19);
            rbEngland.TabIndex = 1;
            rbEngland.Text = "England";
            rbEngland.UseVisualStyleBackColor = true;
            rbEngland.CheckedChanged += RbEngland_CheckedChanged;
            // 
            // rbScotland
            // 
            rbScotland.AutoSize = true;
            rbScotland.Checked = true;
            rbScotland.Location = new Point(7, 22);
            rbScotland.Margin = new Padding(4, 3, 4, 3);
            rbScotland.Name = "rbScotland";
            rbScotland.Size = new Size(71, 19);
            rbScotland.TabIndex = 0;
            rbScotland.TabStop = true;
            rbScotland.Text = "Scotland";
            rbScotland.UseVisualStyleBackColor = true;
            rbScotland.CheckedChanged += RbScotland_CheckedChanged;
            // 
            // CensusCountry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "CensusCountry";
            Size = new Size(302, 90);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

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
