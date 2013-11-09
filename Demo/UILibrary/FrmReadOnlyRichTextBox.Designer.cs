namespace UILibrary
{
    partial class FrmReadOnlyRichTextBox
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
            this.readOnlyRichTextBox1 = new CRC.Controls.ReadOnlyRichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readOnlyRichTextBox1
            // 
            this.readOnlyRichTextBox1.Location = new System.Drawing.Point(54, 57);
            this.readOnlyRichTextBox1.Name = "readOnlyRichTextBox1";
            this.readOnlyRichTextBox1.ReadOnly = true;
            this.readOnlyRichTextBox1.Size = new System.Drawing.Size(149, 194);
            this.readOnlyRichTextBox1.TabIndex = 0;
            this.readOnlyRichTextBox1.TabStop = false;
            this.readOnlyRichTextBox1.Text = "来哦啦看得见分开哦阿里;阿德考虑飞机\n你都无法获取焦点,怎么复制呢?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ReadOnlyRichTextBox";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(263, 57);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(136, 194);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "来哦啦看得见分开哦阿里;阿德考虑飞机\n选择上面的文字,我是可以使用Ctrl+C进行复制的";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "系统的RichTextBox";
            // 
            // FrmReadOnlyRichTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 365);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readOnlyRichTextBox1);
            this.Name = "FrmReadOnlyRichTextBox";
            this.Text = "FrmReadOnlyRichTextBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CRC.Controls.ReadOnlyRichTextBox readOnlyRichTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
    }
}