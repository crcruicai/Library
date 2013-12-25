namespace CRC.Controls
{
    partial class SwapSelectList
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.DestList = new System.Windows.Forms.ListBox();
            this.SourceLst = new System.Windows.Forms.ListBox();
            this.SrcLabel = new System.Windows.Forms.Label();
            this.DestLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DestLabel);
            this.groupBox1.Controls.Add(this.SrcLabel);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.DestList);
            this.groupBox1.Controls.Add(this.SourceLst);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.MinimumSize = new System.Drawing.Size(300, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 311);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Resize += new System.EventHandler(this.groupBox1_Resize);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button4.Location = new System.Drawing.Point(197, 211);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 20);
            this.button4.TabIndex = 5;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(197, 167);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 20);
            this.button3.TabIndex = 4;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(197, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 20);
            this.button2.TabIndex = 3;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(197, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 20);
            this.button1.TabIndex = 2;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DestList
            // 
            this.DestList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DestList.FormattingEnabled = true;
            this.DestList.HorizontalScrollbar = true;
            this.DestList.Location = new System.Drawing.Point(244, 41);
            this.DestList.Name = "DestList";
            this.DestList.Size = new System.Drawing.Size(168, 264);
            this.DestList.TabIndex = 1;
            this.DestList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DestList_MouseDoubleClick);
            // 
            // SourceLst
            // 
            this.SourceLst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SourceLst.FormattingEnabled = true;
            this.SourceLst.HorizontalScrollbar = true;
            this.SourceLst.Location = new System.Drawing.Point(16, 41);
            this.SourceLst.Name = "SourceLst";
            this.SourceLst.Size = new System.Drawing.Size(168, 264);
            this.SourceLst.TabIndex = 0;
            this.SourceLst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SourceLst_MouseDoubleClick);
            // 
            // SrcLabel
            // 
            this.SrcLabel.AutoSize = true;
            this.SrcLabel.Location = new System.Drawing.Point(13, 16);
            this.SrcLabel.Name = "SrcLabel";
            this.SrcLabel.Size = new System.Drawing.Size(0, 13);
            this.SrcLabel.TabIndex = 6;
            // 
            // DestLabel
            // 
            this.DestLabel.AutoSize = true;
            this.DestLabel.Location = new System.Drawing.Point(244, 20);
            this.DestLabel.Name = "DestLabel";
            this.DestLabel.Size = new System.Drawing.Size(0, 13);
            this.DestLabel.TabIndex = 7;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(427, 311);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox DestList;
        private System.Windows.Forms.ListBox SourceLst;
        private System.Windows.Forms.Label DestLabel;
        private System.Windows.Forms.Label SrcLabel;
    }
}
