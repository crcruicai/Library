namespace CWebQQ
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLoginQQ = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMulLoginQQ = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAutoMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.全局设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFriendList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemMajor = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerEx4 = new CWebQQ.UI.SplitContainerEx();
            this.splitContainerEx2 = new CWebQQ.UI.SplitContainerEx();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textMessage = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolShowLable = new System.Windows.Forms.ToolStripLabel();
            this.textMyMessage = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSend = new System.Windows.Forms.Button();
            this.groupListBox1 = new CRC.Controls.GroupListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitContainerEx3 = new CWebQQ.UI.SplitContainerEx();
            this.richMessageHistory = new System.Windows.Forms.RichTextBox();
            this.searchBox1 = new CWebQQ.UI.SearchBox();
            this.splitContainerEx1 = new CWebQQ.UI.SplitContainerEx();
            this.listBoxNewMessage = new CRC.Controls.ChatTextListBox();
            this.searchBox2 = new CWebQQ.UI.SearchBox();
            this.contextMajorFriend = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemMajorDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.menuStrip1.SuspendLayout();
            this.contextFriendList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx4)).BeginInit();
            this.splitContainerEx4.Panel1.SuspendLayout();
            this.splitContainerEx4.Panel2.SuspendLayout();
            this.splitContainerEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).BeginInit();
            this.splitContainerEx2.Panel1.SuspendLayout();
            this.splitContainerEx2.Panel2.SuspendLayout();
            this.splitContainerEx2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx3)).BeginInit();
            this.splitContainerEx3.Panel2.SuspendLayout();
            this.splitContainerEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).BeginInit();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.contextMajorFriend.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑EToolStripMenuItem,
            this.设置SToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(802, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemLoginQQ,
            this.MenuItemMulLoginQQ});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // MenuItemLoginQQ
            // 
            this.MenuItemLoginQQ.Name = "MenuItemLoginQQ";
            this.MenuItemLoginQQ.Size = new System.Drawing.Size(144, 22);
            this.MenuItemLoginQQ.Text = "登录QQ";
            this.MenuItemLoginQQ.Click += new System.EventHandler(this.MenuItemLoginQQ_Click);
            // 
            // MenuItemMulLoginQQ
            // 
            this.MenuItemMulLoginQQ.Name = "MenuItemMulLoginQQ";
            this.MenuItemMulLoginQQ.Size = new System.Drawing.Size(144, 22);
            this.MenuItemMulLoginQQ.Text = "多个QQ登录";
            this.MenuItemMulLoginQQ.Click += new System.EventHandler(this.MenuItemMulLoginQQ_Click);
            // 
            // 编辑EToolStripMenuItem
            // 
            this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
            this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.编辑EToolStripMenuItem.Text = "编辑(&E)";
            // 
            // 设置SToolStripMenuItem
            // 
            this.设置SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAutoMessage,
            this.全局设置ToolStripMenuItem});
            this.设置SToolStripMenuItem.Name = "设置SToolStripMenuItem";
            this.设置SToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.设置SToolStripMenuItem.Text = "设置(&S)";
            // 
            // MenuItemAutoMessage
            // 
            this.MenuItemAutoMessage.Name = "MenuItemAutoMessage";
            this.MenuItemAutoMessage.Size = new System.Drawing.Size(181, 22);
            this.MenuItemAutoMessage.Text = "自动恢复消息设置...";
            this.MenuItemAutoMessage.Click += new System.EventHandler(this.MenuItemAutoMessage_Click);
            // 
            // 全局设置ToolStripMenuItem
            // 
            this.全局设置ToolStripMenuItem.Name = "全局设置ToolStripMenuItem";
            this.全局设置ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.全局设置ToolStripMenuItem.Text = "全局设置";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAbout});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // MenuItemAbout
            // 
            this.MenuItemAbout.Name = "MenuItemAbout";
            this.MenuItemAbout.Size = new System.Drawing.Size(136, 22);
            this.MenuItemAbout.Text = "关于本软件";
            this.MenuItemAbout.Click += new System.EventHandler(this.MenuItemAbout_Click);
            // 
            // contextFriendList
            // 
            this.contextFriendList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMajor});
            this.contextFriendList.Name = "contextFriendList";
            this.contextFriendList.Size = new System.Drawing.Size(149, 26);
            // 
            // MenuItemMajor
            // 
            this.MenuItemMajor.Name = "MenuItemMajor";
            this.MenuItemMajor.Size = new System.Drawing.Size(148, 22);
            this.MenuItemMajor.Text = "设为重点对象";
            this.MenuItemMajor.Click += new System.EventHandler(this.MenuItemMajor_Click);
            // 
            // splitContainerEx4
            // 
            this.splitContainerEx4.CollapsePanel = CWebQQ.UI.CollapsePanel.Panel2;
            this.splitContainerEx4.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx4.Location = new System.Drawing.Point(238, 25);
            this.splitContainerEx4.Name = "splitContainerEx4";
            // 
            // splitContainerEx4.Panel1
            // 
            this.splitContainerEx4.Panel1.Controls.Add(this.splitContainerEx2);
            // 
            // splitContainerEx4.Panel2
            // 
            this.splitContainerEx4.Panel2.Controls.Add(this.panel4);
            this.splitContainerEx4.Size = new System.Drawing.Size(564, 421);
            this.splitContainerEx4.SplitterDistance = 378;
            this.splitContainerEx4.TabIndex = 2;
            // 
            // splitContainerEx2
            // 
            this.splitContainerEx2.CollapsePanel = CWebQQ.UI.CollapsePanel.Panel2;
            this.splitContainerEx2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx2.Name = "splitContainerEx2";
            this.splitContainerEx2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx2.Panel1
            // 
            this.splitContainerEx2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainerEx2.Panel2
            // 
            this.splitContainerEx2.Panel2.Controls.Add(this.groupListBox1);
            this.splitContainerEx2.Size = new System.Drawing.Size(378, 421);
            this.splitContainerEx2.SplitterDistance = 263;
            this.splitContainerEx2.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textMessage);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.textMyMessage);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 263);
            this.panel2.TabIndex = 1;
            // 
            // textMessage
            // 
            this.textMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textMessage.Location = new System.Drawing.Point(0, 0);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(378, 141);
            this.textMessage.TabIndex = 4;
            this.textMessage.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolShowLable});
            this.toolStrip1.Location = new System.Drawing.Point(0, 141);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(378, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolShowLable
            // 
            this.toolShowLable.Name = "toolShowLable";
            this.toolShowLable.Size = new System.Drawing.Size(127, 22);
            this.toolShowLable.Text = "当前你正在交谈的QQ:";
            // 
            // textMyMessage
            // 
            this.textMyMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textMyMessage.Location = new System.Drawing.Point(0, 166);
            this.textMyMessage.Multiline = true;
            this.textMyMessage.Name = "textMyMessage";
            this.textMyMessage.Size = new System.Drawing.Size(378, 66);
            this.textMyMessage.TabIndex = 1;
            this.textMyMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textMyMessage_KeyDown);
            this.textMyMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textMyMessage_KeyUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 232);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 31);
            this.panel1.TabIndex = 2;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(289, 3);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "发送(&S)";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // groupListBox1
            // 
            this.groupListBox1.BackColor = System.Drawing.Color.White;
            this.groupListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupListBox1.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupListBox1.Location = new System.Drawing.Point(0, 0);
            this.groupListBox1.Name = "groupListBox1";
            this.groupListBox1.SelectSubItem = null;
            this.groupListBox1.Size = new System.Drawing.Size(378, 154);
            this.groupListBox1.TabIndex = 1;
            this.groupListBox1.Text = "groupListBox1";
            this.groupListBox1.DoubleClickSubItem += new System.EventHandler<CRC.Controls.GroupListBoxEventArgs>(this.groupListBox1_DoubleClickSubItem_1);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.splitContainerEx3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(182, 421);
            this.panel4.TabIndex = 3;
            // 
            // splitContainerEx3
            // 
            this.splitContainerEx3.CollapsePanel = CWebQQ.UI.CollapsePanel.Panel2;
            this.splitContainerEx3.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx3.Name = "splitContainerEx3";
            this.splitContainerEx3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx3.Panel2
            // 
            this.splitContainerEx3.Panel2.Controls.Add(this.richMessageHistory);
            this.splitContainerEx3.Panel2MinSize = 0;
            this.splitContainerEx3.Size = new System.Drawing.Size(182, 421);
            this.splitContainerEx3.SplitterDistance = 202;
            this.splitContainerEx3.TabIndex = 3;
            // 
            // richMessageHistory
            // 
            this.richMessageHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richMessageHistory.Location = new System.Drawing.Point(0, 0);
            this.richMessageHistory.Name = "richMessageHistory";
            this.richMessageHistory.Size = new System.Drawing.Size(182, 215);
            this.richMessageHistory.TabIndex = 0;
            this.richMessageHistory.Text = "";
            // 
            // searchBox1
            // 
            this.searchBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBox1.Location = new System.Drawing.Point(0, 0);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.Size = new System.Drawing.Size(182, 26);
            this.searchBox1.TabIndex = 2;
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.CollapsePanel = CWebQQ.UI.CollapsePanel.Panel2;
            this.splitContainerEx1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 25);
            this.splitContainerEx1.Name = "splitContainerEx1";
            this.splitContainerEx1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.listBoxNewMessage);
            this.splitContainerEx1.Size = new System.Drawing.Size(238, 421);
            this.splitContainerEx1.SplitterDistance = 222;
            this.splitContainerEx1.TabIndex = 5;
            // 
            // listBoxNewMessage
            // 
            this.listBoxNewMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxNewMessage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxNewMessage.FormattingEnabled = true;
            this.listBoxNewMessage.ItemHeight = 12;
            this.listBoxNewMessage.Location = new System.Drawing.Point(0, 0);
            this.listBoxNewMessage.Name = "listBoxNewMessage";
            this.listBoxNewMessage.Size = new System.Drawing.Size(238, 222);
            this.listBoxNewMessage.TabIndex = 0;
            this.listBoxNewMessage.SelectedIndexChanged += new System.EventHandler(this.listBoxNewMessage_SelectedIndexChanged);
            // 
            // searchBox2
            // 
            this.searchBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBox2.Location = new System.Drawing.Point(0, 0);
            this.searchBox2.Name = "searchBox2";
            this.searchBox2.Size = new System.Drawing.Size(238, 26);
            this.searchBox2.TabIndex = 2;
            // 
            // contextMajorFriend
            // 
            this.contextMajorFriend.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMajorDelete});
            this.contextMajorFriend.Name = "contextMajorFriend";
            this.contextMajorFriend.Size = new System.Drawing.Size(118, 26);
            // 
            // MenuItemMajorDelete
            // 
            this.MenuItemMajorDelete.Name = "MenuItemMajorDelete";
            this.MenuItemMajorDelete.Size = new System.Drawing.Size(117, 22);
            this.MenuItemMajorDelete.Text = "删除(&D)";
            this.MenuItemMajorDelete.Click += new System.EventHandler(this.MenuItemMajorDelete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(802, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 468);
            this.Controls.Add(this.splitContainerEx4);
            this.Controls.Add(this.splitContainerEx1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebQQ多用户版";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextFriendList.ResumeLayout(false);
            this.splitContainerEx4.Panel1.ResumeLayout(false);
            this.splitContainerEx4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx4)).EndInit();
            this.splitContainerEx4.ResumeLayout(false);
            this.splitContainerEx2.Panel1.ResumeLayout(false);
            this.splitContainerEx2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx2)).EndInit();
            this.splitContainerEx2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainerEx3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx3)).EndInit();
            this.splitContainerEx3.ResumeLayout(false);
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEx1)).EndInit();
            this.splitContainerEx1.ResumeLayout(false);
            this.contextMajorFriend.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void InitList()
        {
            this.MajorFriendList = new CRC.Controls.ChatListBox();
            this.FriendList = new CRC.Controls.ChatListBox();
            // 
            // FriendList
            // 
            this.FriendList.BackColor = System.Drawing.Color.White;
            this.FriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FriendList.ForeColor = System.Drawing.Color.DarkOrange;
            this.FriendList.Location = new System.Drawing.Point(0, 26);
            this.FriendList.Name = "FriendList";
            this.FriendList.SelectSubItem = null;
            this.FriendList.Size = new System.Drawing.Size(238, 179);
            this.FriendList.TabIndex = 1;
            this.FriendList.Text = "chatListBox1";

            this.splitContainerEx1.Panel2.Controls.Add(this.FriendList);
            this.splitContainerEx1.Panel2.Controls.Add(this.searchBox2);
            // 
            // MajorFriendList
            // 
            this.MajorFriendList.BackColor = System.Drawing.Color.White;
            this.MajorFriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MajorFriendList.ForeColor = System.Drawing.Color.DarkOrange;
            this.MajorFriendList.Location = new System.Drawing.Point(0, 26);
            this.MajorFriendList.Name = "MajorFriendList";
            this.MajorFriendList.SelectSubItem = null;
            this.MajorFriendList.Size = new System.Drawing.Size(182, 187);
            this.MajorFriendList.TabIndex = 1;
            this.MajorFriendList.Text = "chatListBox1";


            // 
            // splitContainerEx3.Panel1
            // 
            this.splitContainerEx3.Panel1.Controls.Add(this.MajorFriendList);
            this.splitContainerEx3.Panel1.Controls.Add(this.searchBox1);
        }


        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textMyMessage;
        private CWebQQ.UI.SplitContainerEx splitContainerEx1;
        private CWebQQ.UI.SplitContainerEx splitContainerEx2;
        private System.Windows.Forms.RichTextBox textMessage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private CRC.Controls.ChatTextListBox  listBoxNewMessage;
       
        private CRC.Controls.GroupListBox groupListBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAutoMessage;
        private System.Windows.Forms.ToolStripMenuItem 全局设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAbout;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ToolStripLabel toolShowLable;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLoginQQ;
        private CRC.Controls.ChatListBox FriendList;
        private System.Windows.Forms.ContextMenuStrip contextFriendList;
        private CRC.Controls.ChatListBox MajorFriendList;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMajor;
        private UI.SearchBox searchBox1;
        private UI.SearchBox searchBox2;
        private UI.SplitContainerEx splitContainerEx3;
        private System.Windows.Forms.RichTextBox richMessageHistory;
        private UI.SplitContainerEx splitContainerEx4;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMulLoginQQ;
        private System.Windows.Forms.ContextMenuStrip contextMajorFriend;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMajorDelete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    }
}