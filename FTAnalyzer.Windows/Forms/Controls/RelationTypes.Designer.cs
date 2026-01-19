using System;

namespace FTAnalyzer.Forms.Controls
{
    partial class RelationTypes
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
            groupBox2 = new GroupBox();
            ckbLinked = new CheckBox();
            ckbDescendants = new CheckBox();
            ckbUnknown = new CheckBox();
            ckbMarriageDB = new CheckBox();
            ckbMarriage = new CheckBox();
            ckbBlood = new CheckBox();
            ckbDirects = new CheckBox();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(ckbLinked);
            groupBox2.Controls.Add(ckbDescendants);
            groupBox2.Controls.Add(ckbUnknown);
            groupBox2.Controls.Add(ckbMarriageDB);
            groupBox2.Controls.Add(ckbMarriage);
            groupBox2.Controls.Add(ckbBlood);
            groupBox2.Controls.Add(ckbDirects);
            groupBox2.Location = new Point(4, 3);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.MaximumSize = new Size(369, 114);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 0);
            groupBox2.Size = new Size(369, 114);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Relationship Types";
            groupBox2.Paint += groupBox2_Paint;
            // 
            // ckbLinked
            // 
            ckbLinked.AutoSize = true;
            ckbLinked.Location = new Point(187, 76);
            ckbLinked.Margin = new Padding(4, 3, 4, 3);
            ckbLinked.Name = "ckbLinked";
            ckbLinked.Size = new Size(162, 19);
            ckbLinked.TabIndex = 6;
            ckbLinked.Text = "Linked through Marriages";
            ckbLinked.UseVisualStyleBackColor = true;
            ckbLinked.CheckStateChanged += Tickbox_CheckedChanged;
            // 
            // ckbDescendants
            // 
            ckbDescendants.AutoSize = true;
            ckbDescendants.Checked = true;
            ckbDescendants.CheckState = CheckState.Checked;
            ckbDescendants.Location = new Point(135, 50);
            ckbDescendants.Margin = new Padding(4, 3, 4, 3);
            ckbDescendants.Name = "ckbDescendants";
            ckbDescendants.Size = new Size(93, 19);
            ckbDescendants.TabIndex = 4;
            ckbDescendants.Text = "Descendants";
            ckbDescendants.UseVisualStyleBackColor = true;
            ckbDescendants.CheckedChanged += Tickbox_CheckedChanged;
            // 
            // ckbUnknown
            // 
            ckbUnknown.AutoSize = true;
            ckbUnknown.Location = new Point(284, 23);
            ckbUnknown.Margin = new Padding(4, 3, 4, 3);
            ckbUnknown.Name = "ckbUnknown";
            ckbUnknown.Size = new Size(77, 19);
            ckbUnknown.TabIndex = 2;
            ckbUnknown.Text = "Unknown";
            ckbUnknown.UseVisualStyleBackColor = true;
            ckbUnknown.CheckedChanged += Tickbox_CheckedChanged;
            // 
            // ckbMarriageDB
            // 
            ckbMarriageDB.AutoSize = true;
            ckbMarriageDB.Checked = true;
            ckbMarriageDB.CheckState = CheckState.Checked;
            ckbMarriageDB.Location = new Point(7, 76);
            ckbMarriageDB.Margin = new Padding(4, 3, 4, 3);
            ckbMarriageDB.Name = "ckbMarriageDB";
            ckbMarriageDB.Size = new Size(163, 19);
            ckbMarriageDB.TabIndex = 5;
            ckbMarriageDB.Text = "Married to Blood or Direct";
            ckbMarriageDB.UseVisualStyleBackColor = true;
            ckbMarriageDB.CheckedChanged += Tickbox_CheckedChanged;
            // 
            // ckbMarriage
            // 
            ckbMarriage.AutoSize = true;
            ckbMarriage.Location = new Point(135, 22);
            ckbMarriage.Margin = new Padding(4, 3, 4, 3);
            ckbMarriage.Name = "ckbMarriage";
            ckbMarriage.Size = new Size(131, 19);
            ckbMarriage.TabIndex = 1;
            ckbMarriage.Text = "Related by Marriage";
            ckbMarriage.UseVisualStyleBackColor = true;
            ckbMarriage.CheckedChanged += Tickbox_CheckedChanged;
            // 
            // ckbBlood
            // 
            ckbBlood.AutoSize = true;
            ckbBlood.Checked = true;
            ckbBlood.CheckState = CheckState.Checked;
            ckbBlood.Location = new Point(7, 50);
            ckbBlood.Margin = new Padding(4, 3, 4, 3);
            ckbBlood.Name = "ckbBlood";
            ckbBlood.Size = new Size(108, 19);
            ckbBlood.TabIndex = 3;
            ckbBlood.Text = "Blood Relations";
            ckbBlood.UseVisualStyleBackColor = true;
            ckbBlood.CheckedChanged += Tickbox_CheckedChanged;
            // 
            // ckbDirects
            // 
            ckbDirects.AutoSize = true;
            ckbDirects.Checked = true;
            ckbDirects.CheckState = CheckState.Checked;
            ckbDirects.Location = new Point(7, 23);
            ckbDirects.Margin = new Padding(4, 3, 4, 3);
            ckbDirects.Name = "ckbDirects";
            ckbDirects.Size = new Size(112, 19);
            ckbDirects.TabIndex = 0;
            ckbDirects.Text = "Direct Ancestors";
            ckbDirects.UseVisualStyleBackColor = true;
            ckbDirects.CheckedChanged += Tickbox_CheckedChanged;
            // 
            // RelationTypes
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(groupBox2);
            Margin = new Padding(4, 3, 4, 3);
            Name = "RelationTypes";
            Size = new Size(377, 120);
            Layout += RelationTypes_Layout;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbUnknown;
        private System.Windows.Forms.CheckBox ckbMarriageDB;
        private System.Windows.Forms.CheckBox ckbMarriage;
        private System.Windows.Forms.CheckBox ckbBlood;
        private System.Windows.Forms.CheckBox ckbDirects;
        private System.Windows.Forms.CheckBox ckbDescendants;
        private System.Windows.Forms.CheckBox ckbLinked;
    }
}
