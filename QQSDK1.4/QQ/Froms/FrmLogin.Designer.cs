namespace CWebQQ.Froms
{
    partial class FrmLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textNumber = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textCheckCode = new System.Windows.Forms.TextBox();
            this.comState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.linkLabelChange = new System.Windows.Forms.LinkLabel();
            this.buttonLogon = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureCheckCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCheckCode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "QQ号码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码:";
            // 
            // textNumber
            // 
            this.textNumber.Location = new System.Drawing.Point(97, 18);
            this.textNumber.Name = "textNumber";
            this.textNumber.Size = new System.Drawing.Size(100, 21);
            this.textNumber.TabIndex = 2;
            this.textNumber.Leave += new System.EventHandler(this.textNumber_Leave);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(97, 54);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(100, 21);
            this.textPassword.TabIndex = 3;
            // 
            // textCheckCode
            // 
            this.textCheckCode.Location = new System.Drawing.Point(97, 90);
            this.textCheckCode.Name = "textCheckCode";
            this.textCheckCode.Size = new System.Drawing.Size(100, 21);
            this.textCheckCode.TabIndex = 4;
            // 
            // comState
            // 
            this.comState.FormattingEnabled = true;
            this.comState.Items.AddRange(new object[] {
            "在线",
            "忙碌",
            "Q我吧",
            "隐身",
            "离线",
            "请勿打扰"});
            this.comState.Location = new System.Drawing.Point(97, 132);
            this.comState.Name = "comState";
            this.comState.Size = new System.Drawing.Size(100, 20);
            this.comState.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "验证码:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(203, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // linkLabelChange
            // 
            this.linkLabelChange.AutoSize = true;
            this.linkLabelChange.Location = new System.Drawing.Point(262, 143);
            this.linkLabelChange.Name = "linkLabelChange";
            this.linkLabelChange.Size = new System.Drawing.Size(41, 12);
            this.linkLabelChange.TabIndex = 9;
            this.linkLabelChange.TabStop = true;
            this.linkLabelChange.Text = "换一张";
            this.linkLabelChange.Visible = false;
            this.linkLabelChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelChange_LinkClicked);
            // 
            // buttonLogon
            // 
            this.buttonLogon.Location = new System.Drawing.Point(60, 172);
            this.buttonLogon.Name = "buttonLogon";
            this.buttonLogon.Size = new System.Drawing.Size(101, 34);
            this.buttonLogon.TabIndex = 10;
            this.buttonLogon.Text = "登录(&O)";
            this.buttonLogon.UseVisualStyleBackColor = true;
            this.buttonLogon.Click += new System.EventHandler(this.buttonLogon_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(203, 172);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(93, 34);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "取消(&C)";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // pictureCheckCode
            // 
            this.pictureCheckCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureCheckCode.Location = new System.Drawing.Point(203, 90);
            this.pictureCheckCode.Name = "pictureCheckCode";
            this.pictureCheckCode.Size = new System.Drawing.Size(117, 50);
            this.pictureCheckCode.TabIndex = 7;
            this.pictureCheckCode.TabStop = false;
            this.pictureCheckCode.Visible = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.buttonLogon;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 218);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonLogon);
            this.Controls.Add(this.linkLabelChange);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureCheckCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comState);
            this.Controls.Add(this.textCheckCode);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QQ登录";
            this.Load += new System.EventHandler(this.FrmLogon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCheckCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNumber;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textCheckCode;
        private System.Windows.Forms.ComboBox comState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureCheckCode;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.LinkLabel linkLabelChange;
        private System.Windows.Forms.Button buttonLogon;
        private System.Windows.Forms.Button buttonCancel;
    }
}