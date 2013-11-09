using CRC.Controls;

namespace DemoApp
{
    partial class FrmCheckComboBox
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
            CheckBoxProperties checkBoxProperties1 = new CheckBoxProperties();
            CheckBoxProperties checkBoxProperties2 = new CheckBoxProperties();
            CheckBoxProperties checkBoxProperties3 = new CheckBoxProperties();
            CheckBoxProperties checkBoxProperties4 = new CheckBoxProperties();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbIListDataSource = new CheckComboBox();
            this.cmbManual = new CheckComboBox();
            this.cmbDataTableDataSource = new CheckComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxComboBox1 = new CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Populated Manually using ComboBox.Items";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Populated using a custom IList DataSource";
            // 
            // cmbIListDataSource
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbIListDataSource.CheckBoxProperties = checkBoxProperties1;
            this.cmbIListDataSource.DisplayMemberSingleItem = "";
            this.cmbIListDataSource.FormattingEnabled = true;
            this.cmbIListDataSource.Location = new System.Drawing.Point(3, 67);
            this.cmbIListDataSource.Name = "cmbIListDataSource";
            this.cmbIListDataSource.Size = new System.Drawing.Size(152, 21);
            this.cmbIListDataSource.TabIndex = 3;
            // 
            // cmbManual
            // 
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbManual.CheckBoxProperties = checkBoxProperties2;
            this.cmbManual.DisplayMemberSingleItem = "";
            this.cmbManual.FormattingEnabled = true;
            this.cmbManual.Location = new System.Drawing.Point(3, 22);
            this.cmbManual.Name = "cmbManual";
            this.cmbManual.Size = new System.Drawing.Size(151, 21);
            this.cmbManual.TabIndex = 0;
            // 
            // cmbDataTableDataSource
            // 
            checkBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbDataTableDataSource.CheckBoxProperties = checkBoxProperties3;
            this.cmbDataTableDataSource.DisplayMemberSingleItem = "";
            this.cmbDataTableDataSource.FormattingEnabled = true;
            this.cmbDataTableDataSource.Location = new System.Drawing.Point(3, 116);
            this.cmbDataTableDataSource.Name = "cmbDataTableDataSource";
            this.cmbDataTableDataSource.Size = new System.Drawing.Size(152, 21);
            this.cmbDataTableDataSource.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Populated using a DataTable DataSource";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "A different look. Accessed via CheckBoxProperties";
            // 
            // checkBoxComboBox1
            // 
            checkBoxProperties4.AutoSize = true;
            checkBoxProperties4.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            checkBoxProperties4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            checkBoxProperties4.ForeColor = System.Drawing.SystemColors.ControlText;
            checkBoxProperties4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxComboBox1.CheckBoxProperties = checkBoxProperties4;
            this.checkBoxComboBox1.DisplayMemberSingleItem = "";
            this.checkBoxComboBox1.FormattingEnabled = true;
            this.checkBoxComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4",
            "Item 5",
            "Item 6"});
            this.checkBoxComboBox1.Location = new System.Drawing.Point(3, 161);
            this.checkBoxComboBox1.Name = "checkBoxComboBox1";
            this.checkBoxComboBox1.Size = new System.Drawing.Size(151, 21);
            this.checkBoxComboBox1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 187);
            this.Controls.Add(this.checkBoxComboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDataTableDataSource);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbIListDataSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbManual);
            this.Name = "Form1";
            this.Text = "DEMO of CheckBoxComboBox";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckComboBox cmbManual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CheckComboBox cmbIListDataSource;
        private CheckComboBox cmbDataTableDataSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CheckComboBox checkBoxComboBox1;
    }
}

