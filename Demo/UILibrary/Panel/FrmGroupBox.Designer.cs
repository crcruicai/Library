namespace UILibrary
{
    partial class FrmGroupBox
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupListbox1 = new CRC.Controls.GroupListbox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(342, 50);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(217, 300);
            this.textBox1.TabIndex = 1;
            // 
            // groupListbox1
            // 
            this.groupListbox1.AutoScroll = true;
            this.groupListbox1.AutoScrollMinSize = new System.Drawing.Size(172, 0);
            this.groupListbox1.BackColor = System.Drawing.Color.White;
            this.groupListbox1.ForeSubItemColor = System.Drawing.Color.White;
            this.groupListbox1.Location = new System.Drawing.Point(28, 50);
            this.groupListbox1.MouseOnCellItemColor = System.Drawing.Color.Turquoise;
            this.groupListbox1.MouseOnItemColor = System.Drawing.Color.Tan;
            this.groupListbox1.MouseOnSubItemColor = System.Drawing.Color.Thistle;
            this.groupListbox1.Name = "groupListbox1";
            this.groupListbox1.SelectCellItemColor = System.Drawing.Color.Wheat;
            this.groupListbox1.Size = new System.Drawing.Size(172, 389);
            this.groupListbox1.TabIndex = 0;
            this.groupListbox1.Text = "groupListbox1";
            this.groupListbox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.groupListbox1_MouseMove);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(404, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 3;
            // 
            // FrmGroupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 514);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupListbox1);
            this.Name = "FrmGroupBox";
            this.Text = "FrmGroupBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CRC.Controls.GroupListbox groupListbox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
    }
}