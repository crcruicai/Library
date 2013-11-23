namespace ListViewCollectionDemo
{
    partial class FrmEditListView
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
            this.components = new System.ComponentModel.Container();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBoxDoubleClickActivation = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listViewEx1 = new CRC.Controls .EditListView();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(32, 56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(80, 20);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Visible = false;
            // 
            // textBoxComment
            // 
            this.textBoxComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxComment.Location = new System.Drawing.Point(32, 104);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(80, 16);
            this.textBoxComment.TabIndex = 3;
            this.textBoxComment.Text = "";
            this.textBoxComment.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Location = new System.Drawing.Point(32, 80);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Visible = false;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.Location = new System.Drawing.Point(32, 128);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(80, 20);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.Text = "";
            this.textBoxPassword.Visible = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown1.Location = new System.Drawing.Point(32, 152);
            this.numericUpDown1.Maximum = new System.Decimal(new int[] {
																		   230,
																		   0,
																		   0,
																		   0});
            this.numericUpDown1.Minimum = new System.Decimal(new int[] {
																		   120,
																		   0,
																		   0,
																		   0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new System.Decimal(new int[] {
																		 120,
																		 0,
																		 0,
																		 0});
            this.numericUpDown1.Visible = false;
            // 
            // checkBoxDoubleClickActivation
            // 
            this.checkBoxDoubleClickActivation.Checked = true;
            this.checkBoxDoubleClickActivation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDoubleClickActivation.Location = new System.Drawing.Point(8, 8);
            this.checkBoxDoubleClickActivation.Name = "checkBoxDoubleClickActivation";
            this.checkBoxDoubleClickActivation.Size = new System.Drawing.Size(176, 16);
            this.checkBoxDoubleClickActivation.TabIndex = 6;
            this.checkBoxDoubleClickActivation.Text = "DoubleClickActivation";
            this.checkBoxDoubleClickActivation.CheckedChanged += new System.EventHandler(this.checkBoxDoubleClickActivation_CheckedChanged);
            // 
            // listViewEx1
            // 
            this.listViewEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewEx1.DoubleClickActivation = false;
            this.listViewEx1.Location = new System.Drawing.Point(8, 32);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(344, 168);
            this.listViewEx1.TabIndex = 7;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(360, 205);
            this.Controls.Add(this.checkBoxDoubleClickActivation);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.listViewEx1);
            this.Name = "Form1";
            this.Text = "ListViewEx Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private CRC.Controls .EditListView  listViewEx1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBoxDoubleClickActivation;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}