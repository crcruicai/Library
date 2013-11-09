namespace UILibrary
{
    partial class FrmLayerTreeView
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
            this.m_LayerTree = new CRC.Controls.LayerTreeView();
            this.SuspendLayout();
            // 
            // m_LayerTree
            // 
            this.m_LayerTree.CheckBoxes = true;
            this.m_LayerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LayerTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.m_LayerTree.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(194)))), ((int)(((byte)(241)))));
            this.m_LayerTree.Font = new System.Drawing.Font("宋体", 13F);
            this.m_LayerTree.FullRowSelect = true;
            this.m_LayerTree.Indent = 0;
            this.m_LayerTree.Location = new System.Drawing.Point(0, 0);
            this.m_LayerTree.Name = "m_LayerTree";
            this.m_LayerTree.ShowLines = false;
            this.m_LayerTree.Size = new System.Drawing.Size(284, 262);
            this.m_LayerTree.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(222)))), ((int)(((byte)(250)))));
            this.m_LayerTree.TabIndex = 0;
            // 
            // FrmLayerTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.m_LayerTree);
            this.Name = "FrmLayerTreeView";
            this.Text = "FrmLayerTreeView";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.LayerTreeView m_LayerTree;
    }
}