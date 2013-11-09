namespace UILibrary
{
    partial class FrmGroupListBox
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupListBox1 = new CRC.Controls.GroupListBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            // 
            // groupListBox1
            // 
            this.groupListBox1.BackColor = System.Drawing.Color.White;
            this.groupListBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.groupListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupListBox1.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupListBox1.Location = new System.Drawing.Point(0, 0);
            this.groupListBox1.Name = "groupListBox1";
            this.groupListBox1.SelectSubItem = null;
            this.groupListBox1.Size = new System.Drawing.Size(284, 371);
            this.groupListBox1.SubItemColor = System.Drawing.Color.Coral;
            this.groupListBox1.TabIndex = 0;
            this.groupListBox1.Text = "groupListBox1";
            // 
            // FrmGroupListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 371);
            this.Controls.Add(this.groupListBox1);
            this.Name = "FrmGroupListBox";
            this.Text = "FrmGroupListBox";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.GroupListBox groupListBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
    }
}