namespace UILibrary
{
    partial class FrmDbListView
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode12,
            treeNode13});
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("First", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Second", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "3",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "4",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "5",
            "1",
            "2"}, -1);
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode19,
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode26,
            treeNode27});
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("First", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Second", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "3",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "4",
            "1",
            "2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "5",
            "1",
            "2"}, -1);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dbTreeView1 = new CRC.Controls.DbTreeView();
            this.dbListView1 = new CRC.Controls.DbListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dbTreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dbListView1);
            this.splitContainer1.Size = new System.Drawing.Size(563, 198);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 0;
            // 
            // dbTreeView1
            // 
            this.dbTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbTreeView1.Location = new System.Drawing.Point(0, 0);
            this.dbTreeView1.Name = "dbTreeView1";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node8";
            treeNode2.Text = "Node8";
            treeNode3.Name = "Node9";
            treeNode3.Text = "Node9";
            treeNode4.Name = "Node10";
            treeNode4.Text = "Node10";
            treeNode5.Name = "Node2";
            treeNode5.Text = "Node2";
            treeNode6.Name = "Node3";
            treeNode6.Text = "Node3";
            treeNode7.Name = "Node0";
            treeNode7.Text = "Node0";
            treeNode8.Name = "Node5";
            treeNode8.Text = "Node5";
            treeNode9.Name = "Node11";
            treeNode9.Text = "Node11";
            treeNode10.Name = "Node12";
            treeNode10.Text = "Node12";
            treeNode11.Name = "Node13";
            treeNode11.Text = "Node13";
            treeNode12.Name = "Node6";
            treeNode12.Text = "Node6";
            treeNode13.Name = "Node7";
            treeNode13.Text = "Node7";
            treeNode14.Name = "Node4";
            treeNode14.Text = "Node4";
            this.dbTreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode14});
            this.dbTreeView1.Size = new System.Drawing.Size(187, 198);
            this.dbTreeView1.TabIndex = 0;
            // 
            // dbListView1
            // 
            this.dbListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.dbListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbListView1.FullRowSelect = true;
            listViewGroup1.Header = "First";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Second";
            listViewGroup2.Name = "listViewGroup2";
            this.dbListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup2;
            listViewItem4.Group = listViewGroup2;
            this.dbListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.dbListView1.Location = new System.Drawing.Point(0, 0);
            this.dbListView1.Name = "dbListView1";
            this.dbListView1.Size = new System.Drawing.Size(372, 198);
            this.dbListView1.TabIndex = 0;
            this.dbListView1.UseCompatibleStateImageBehavior = false;
            this.dbListView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ColumnHeader1";
            this.columnHeader1.Width = 117;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ColumnHeader2";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ColumnHeader3";
            this.columnHeader3.Width = 108;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(563, 403);
            this.splitContainer2.SplitterDistance = 201;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.listView1);
            this.splitContainer3.Size = new System.Drawing.Size(563, 201);
            this.splitContainer3.SplitterDistance = 187;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode15.Name = "Node1";
            treeNode15.Text = "Node1";
            treeNode16.Name = "Node8";
            treeNode16.Text = "Node8";
            treeNode17.Name = "Node9";
            treeNode17.Text = "Node9";
            treeNode18.Name = "Node10";
            treeNode18.Text = "Node10";
            treeNode19.Name = "Node2";
            treeNode19.Text = "Node2";
            treeNode20.Name = "Node3";
            treeNode20.Text = "Node3";
            treeNode21.Name = "Node0";
            treeNode21.Text = "Node0";
            treeNode22.Name = "Node5";
            treeNode22.Text = "Node5";
            treeNode23.Name = "Node11";
            treeNode23.Text = "Node11";
            treeNode24.Name = "Node12";
            treeNode24.Text = "Node12";
            treeNode25.Name = "Node13";
            treeNode25.Text = "Node13";
            treeNode26.Name = "Node6";
            treeNode26.Text = "Node6";
            treeNode27.Name = "Node7";
            treeNode27.Text = "Node7";
            treeNode28.Name = "Node4";
            treeNode28.Text = "Node4";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode28});
            this.treeView1.Size = new System.Drawing.Size(187, 201);
            this.treeView1.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            listViewGroup3.Header = "First";
            listViewGroup3.Name = "listViewGroup1";
            listViewGroup4.Header = "Second";
            listViewGroup4.Name = "listViewGroup2";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            listViewItem6.Group = listViewGroup3;
            listViewItem7.Group = listViewGroup3;
            listViewItem8.Group = listViewGroup4;
            listViewItem9.Group = listViewGroup4;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(372, 201);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ColumnHeader1";
            this.columnHeader4.Width = 117;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ColumnHeader2";
            this.columnHeader5.Width = 119;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ColumnHeader3";
            this.columnHeader6.Width = 108;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 403);
            this.Controls.Add(this.splitContainer2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private CRC.Controls.DbTreeView dbTreeView1;
        private CRC.Controls.DbListView dbListView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}

