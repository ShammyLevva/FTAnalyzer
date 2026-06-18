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
            tableLayoutPanel1 = new TableLayoutPanel();
            ckbDirects = new CheckBox();
            ckbMarriage = new CheckBox();
            ckbUnknown = new CheckBox();
            ckbBlood = new CheckBox();
            ckbDescendants = new CheckBox();
            ckbMarriageDB = new CheckBox();
            ckbLinked = new CheckBox();
            groupBox2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            //
            // groupBox2
            //
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(tableLayoutPanel1);
            groupBox2.Location = new Point(4, 3);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 6);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Relationship Types";
            groupBox2.Paint += GroupBox2_Paint;
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanel1.Controls.Add(ckbDirects, 0, 0);
            tableLayoutPanel1.Controls.Add(ckbMarriage, 1, 0);
            tableLayoutPanel1.Controls.Add(ckbUnknown, 2, 0);
            tableLayoutPanel1.Controls.Add(ckbBlood, 0, 1);
            tableLayoutPanel1.Controls.Add(ckbDescendants, 1, 1);
            tableLayoutPanel1.Controls.Add(ckbMarriageDB, 0, 2);
            tableLayoutPanel1.Controls.Add(ckbLinked, 1, 2);
            tableLayoutPanel1.Location = new Point(4, 19);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel1.TabIndex = 7;
            //
            // ckbDirects
            //
            ckbDirects.AutoSize = true;
            ckbDirects.Checked = true;
            ckbDirects.CheckState = CheckState.Checked;
            ckbDirects.Margin = new Padding(4, 3, 12, 3);
            ckbDirects.Name = "ckbDirects";
            ckbDirects.TabIndex = 0;
            ckbDirects.Text = "Direct Ancestors";
            ckbDirects.UseVisualStyleBackColor = true;
            ckbDirects.CheckedChanged += Tickbox_CheckedChanged;
            //
            // ckbMarriage
            //
            ckbMarriage.AutoSize = true;
            ckbMarriage.Margin = new Padding(4, 3, 12, 3);
            ckbMarriage.Name = "ckbMarriage";
            ckbMarriage.TabIndex = 1;
            ckbMarriage.Text = "Related by Marriage";
            ckbMarriage.UseVisualStyleBackColor = true;
            ckbMarriage.CheckedChanged += Tickbox_CheckedChanged;
            //
            // ckbUnknown
            //
            ckbUnknown.AutoSize = true;
            ckbUnknown.Margin = new Padding(4, 3, 4, 3);
            ckbUnknown.Name = "ckbUnknown";
            ckbUnknown.TabIndex = 2;
            ckbUnknown.Text = "Unknown";
            ckbUnknown.UseVisualStyleBackColor = true;
            ckbUnknown.CheckedChanged += Tickbox_CheckedChanged;
            //
            // ckbBlood
            //
            ckbBlood.AutoSize = true;
            ckbBlood.Checked = true;
            ckbBlood.CheckState = CheckState.Checked;
            ckbBlood.Margin = new Padding(4, 3, 12, 3);
            ckbBlood.Name = "ckbBlood";
            ckbBlood.TabIndex = 3;
            ckbBlood.Text = "Blood Relations";
            ckbBlood.UseVisualStyleBackColor = true;
            ckbBlood.CheckedChanged += Tickbox_CheckedChanged;
            //
            // ckbDescendants
            //
            ckbDescendants.AutoSize = true;
            ckbDescendants.Checked = true;
            ckbDescendants.CheckState = CheckState.Checked;
            ckbDescendants.Margin = new Padding(4, 3, 12, 3);
            ckbDescendants.Name = "ckbDescendants";
            ckbDescendants.TabIndex = 4;
            ckbDescendants.Text = "Descendants";
            ckbDescendants.UseVisualStyleBackColor = true;
            ckbDescendants.CheckedChanged += Tickbox_CheckedChanged;
            //
            // ckbMarriageDB
            //
            ckbMarriageDB.AutoSize = true;
            ckbMarriageDB.Checked = true;
            ckbMarriageDB.CheckState = CheckState.Checked;
            ckbMarriageDB.Margin = new Padding(4, 3, 12, 3);
            ckbMarriageDB.Name = "ckbMarriageDB";
            ckbMarriageDB.TabIndex = 5;
            ckbMarriageDB.Text = "Married to Blood or Direct";
            ckbMarriageDB.UseVisualStyleBackColor = true;
            ckbMarriageDB.CheckedChanged += Tickbox_CheckedChanged;
            //
            // ckbLinked
            //
            ckbLinked.AutoSize = true;
            ckbLinked.Margin = new Padding(4, 3, 4, 3);
            ckbLinked.Name = "ckbLinked";
            ckbLinked.TabIndex = 6;
            ckbLinked.Text = "Linked through Marriages";
            ckbLinked.UseVisualStyleBackColor = true;
            ckbLinked.CheckStateChanged += Tickbox_CheckedChanged;
            //
            // RelationTypes
            //
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(groupBox2);
            Margin = new Padding(4, 3, 4, 3);
            Name = "RelationTypes";
            Layout += RelationTypes_Layout;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox ckbUnknown;
        private System.Windows.Forms.CheckBox ckbMarriageDB;
        private System.Windows.Forms.CheckBox ckbMarriage;
        private System.Windows.Forms.CheckBox ckbBlood;
        private System.Windows.Forms.CheckBox ckbDirects;
        private System.Windows.Forms.CheckBox ckbDescendants;
        private System.Windows.Forms.CheckBox ckbLinked;
    }
}
