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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbLinked = new System.Windows.Forms.CheckBox();
            this.ckbDescendants = new System.Windows.Forms.CheckBox();
            this.ckbUnknown = new System.Windows.Forms.CheckBox();
            this.ckbMarriageDB = new System.Windows.Forms.CheckBox();
            this.ckbMarriage = new System.Windows.Forms.CheckBox();
            this.ckbBlood = new System.Windows.Forms.CheckBox();
            this.ckbDirects = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbLinked);
            this.groupBox2.Controls.Add(this.ckbDescendants);
            this.groupBox2.Controls.Add(this.ckbUnknown);
            this.groupBox2.Controls.Add(this.ckbMarriageDB);
            this.groupBox2.Controls.Add(this.ckbMarriage);
            this.groupBox2.Controls.Add(this.ckbBlood);
            this.groupBox2.Controls.Add(this.ckbDirects);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 93);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relationship Types";
            // 
            // ckbLinked
            // 
            this.ckbLinked.AutoSize = true;
            this.ckbLinked.Location = new System.Drawing.Point(158, 66);
            this.ckbLinked.Name = "ckbLinked";
            this.ckbLinked.Size = new System.Drawing.Size(146, 17);
            this.ckbLinked.TabIndex = 6;
            this.ckbLinked.Text = "Linked through Marriages";
            this.ckbLinked.UseVisualStyleBackColor = true;
            this.ckbLinked.CheckStateChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // ckbDescendants
            // 
            this.ckbDescendants.AutoSize = true;
            this.ckbDescendants.Checked = true;
            this.ckbDescendants.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDescendants.Location = new System.Drawing.Point(116, 43);
            this.ckbDescendants.Name = "ckbDescendants";
            this.ckbDescendants.Size = new System.Drawing.Size(89, 17);
            this.ckbDescendants.TabIndex = 5;
            this.ckbDescendants.Text = "Descendants";
            this.ckbDescendants.UseVisualStyleBackColor = true;
            this.ckbDescendants.CheckedChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // ckbUnknown
            // 
            this.ckbUnknown.AutoSize = true;
            this.ckbUnknown.Location = new System.Drawing.Point(243, 20);
            this.ckbUnknown.Name = "ckbUnknown";
            this.ckbUnknown.Size = new System.Drawing.Size(72, 17);
            this.ckbUnknown.TabIndex = 4;
            this.ckbUnknown.Text = "Unknown";
            this.ckbUnknown.UseVisualStyleBackColor = true;
            this.ckbUnknown.CheckedChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // ckbMarriageDB
            // 
            this.ckbMarriageDB.AutoSize = true;
            this.ckbMarriageDB.Checked = true;
            this.ckbMarriageDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMarriageDB.Location = new System.Drawing.Point(6, 66);
            this.ckbMarriageDB.Name = "ckbMarriageDB";
            this.ckbMarriageDB.Size = new System.Drawing.Size(146, 17);
            this.ckbMarriageDB.TabIndex = 3;
            this.ckbMarriageDB.Text = "Married to Blood or Direct";
            this.ckbMarriageDB.UseVisualStyleBackColor = true;
            this.ckbMarriageDB.CheckedChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // ckbMarriage
            // 
            this.ckbMarriage.AutoSize = true;
            this.ckbMarriage.Location = new System.Drawing.Point(116, 19);
            this.ckbMarriage.Name = "ckbMarriage";
            this.ckbMarriage.Size = new System.Drawing.Size(121, 17);
            this.ckbMarriage.TabIndex = 2;
            this.ckbMarriage.Text = "Related by Marriage";
            this.ckbMarriage.UseVisualStyleBackColor = true;
            this.ckbMarriage.CheckedChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // ckbBlood
            // 
            this.ckbBlood.AutoSize = true;
            this.ckbBlood.Checked = true;
            this.ckbBlood.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbBlood.Location = new System.Drawing.Point(6, 43);
            this.ckbBlood.Name = "ckbBlood";
            this.ckbBlood.Size = new System.Drawing.Size(100, 17);
            this.ckbBlood.TabIndex = 1;
            this.ckbBlood.Text = "Blood Relations";
            this.ckbBlood.UseVisualStyleBackColor = true;
            this.ckbBlood.CheckedChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // ckbDirects
            // 
            this.ckbDirects.AutoSize = true;
            this.ckbDirects.Checked = true;
            this.ckbDirects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDirects.Location = new System.Drawing.Point(6, 20);
            this.ckbDirects.Name = "ckbDirects";
            this.ckbDirects.Size = new System.Drawing.Size(104, 17);
            this.ckbDirects.TabIndex = 0;
            this.ckbDirects.Text = "Direct Ancestors";
            this.ckbDirects.UseVisualStyleBackColor = true;
            this.ckbDirects.CheckedChanged += new System.EventHandler(this.Tickbox_CheckedChanged);
            // 
            // RelationTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Name = "RelationTypes";
            this.Size = new System.Drawing.Size(320, 99);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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
