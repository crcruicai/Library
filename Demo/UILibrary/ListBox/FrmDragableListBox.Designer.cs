namespace UILibrary
{
    partial class FrmDragableListBox
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
            this.dragableListBox1 = new CRC.Controls.DragableListBox();
            this.dragableListBox2 = new CRC.Controls.DragableListBox();
            this.SuspendLayout();
            // 
            // dragableListBox1
            // 
            this.dragableListBox1.DragAcross = true;
            this.dragableListBox1.DragSort = true;
            this.dragableListBox1.FormattingEnabled = true;
            this.dragableListBox1.ItemHeight = 12;
            this.dragableListBox1.Location = new System.Drawing.Point(12, 58);
            this.dragableListBox1.Name = "dragableListBox1";
            this.dragableListBox1.OddColor = System.Drawing.SystemColors.Window;
            this.dragableListBox1.Size = new System.Drawing.Size(150, 220);
            this.dragableListBox1.TabIndex = 0;
            // 
            // dragableListBox2
            // 
            this.dragableListBox2.DragAcross = true;
            this.dragableListBox2.DragSort = true;
            this.dragableListBox2.FormattingEnabled = true;
            this.dragableListBox2.ItemHeight = 12;
            this.dragableListBox2.Location = new System.Drawing.Point(208, 58);
            this.dragableListBox2.Name = "dragableListBox2";
            this.dragableListBox2.OddColor = System.Drawing.SystemColors.Window;
            this.dragableListBox2.Size = new System.Drawing.Size(156, 220);
            this.dragableListBox2.TabIndex = 1;
            // 
            // FrmDragableListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 334);
            this.Controls.Add(this.dragableListBox2);
            this.Controls.Add(this.dragableListBox1);
            this.Name = "FrmDragableListBox";
            this.Text = "FrmDragableListBox";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.DragableListBox dragableListBox1;
        private CRC.Controls.DragableListBox dragableListBox2;
    }
}