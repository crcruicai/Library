using CRC.Controls;
namespace ListViewCollectionDemo
{
    partial class FrmGroupableListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGroupableListView));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.exListView1 = new CRC.Controls.GroupableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupableListView1 = new CRC.Controls.GroupableListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user.png");
            this.imageList1.Images.SetKeyName(1, "user_female.png");
            // 
            // exListView1
            // 
            this.exListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.exListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.exListView1.GroupLabelText = "Group by :";
            this.exListView1.GroupsGUIs = true;
            this.exListView1.Location = new System.Drawing.Point(9, 31);
            this.exListView1.Name = "exListView1";
            this.exListView1.ShowGroupLabel = true;
            this.exListView1.Size = new System.Drawing.Size(522, 179);
            this.exListView1.SmallImageList = this.imageList1;
            this.exListView1.TabIndex = 0;
            this.exListView1.ToolStripImage = null;
            this.exListView1.UseCompatibleStateImageBehavior = false;
            this.exListView1.View = System.Windows.Forms.View.Details;
            this.exListView1.SelectedIndexChanged += new System.EventHandler(this.exListView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 129;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Sex";
            this.columnHeader2.Width = 111;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Job";
            this.columnHeader3.Width = 125;
            // 
            // groupableListView1
            // 
            this.groupableListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.groupableListView1.GroupLabelText = "Group by :";
            this.groupableListView1.GroupsGUIs = false;
            this.groupableListView1.Location = new System.Drawing.Point(9, 225);
            this.groupableListView1.Name = "groupableListView1";
            this.groupableListView1.ShowGroupLabel = false;
            this.groupableListView1.Size = new System.Drawing.Size(522, 160);
            this.groupableListView1.TabIndex = 1;
            this.groupableListView1.ToolStripImage = null;
            this.groupableListView1.UseCompatibleStateImageBehavior = false;
            this.groupableListView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 128;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 133;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 147;
            // 
            // FrmGroupableListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 454);
            this.Controls.Add(this.groupableListView1);
            this.Controls.Add(this.exListView1);
            this.Name = "FrmGroupableListView";
            this.Text = "Groupable ListView TestApp";
            this.ResumeLayout(false);

        }

        #endregion

        private GroupableListView exListView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private GroupableListView groupableListView1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;





    }
}

