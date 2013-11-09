namespace UILibrary
{
    partial class FrmChatGroupBox
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
            this.chatGroupBox1 = new CRC.Controls.ChatGroupBox();
            this.SuspendLayout();
            // 
            // chatGroupBox1
            // 
            this.chatGroupBox1.Location = new System.Drawing.Point(72, 48);
            this.chatGroupBox1.Name = "chatGroupBox1";
            this.chatGroupBox1.Size = new System.Drawing.Size(264, 322);
            this.chatGroupBox1.TabIndex = 0;
            this.chatGroupBox1.Text = "chatGroupBox1";
            // 
            // FrmChatGroupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 436);
            this.Controls.Add(this.chatGroupBox1);
            this.Name = "FrmChatGroupBox";
            this.Text = "FrmChatGroupBox";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.ChatGroupBox chatGroupBox1;
    }
}