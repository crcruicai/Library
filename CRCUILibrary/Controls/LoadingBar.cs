/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/18 10:02:52
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace CRC.Controls
{
    //http://www.cnblogs.com/HopeGi/p/3369044.html
    /// <summary>
    /// 加载滚动条.
    /// </summary>
    public class LoadingBar:Control
    {

        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            this.ResumeLayout(false);

            curLen = 0;
            barLength = 0.5f;      
        }

        internal float curLen;
        internal float barLength;

        public LoadingBar()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                curLen += 10;
                if (curLen >= this.Width * (1 + barLength)) curLen = 0;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rec=new Rectangle((int)(curLen - this.Width * barLength), 1, (int)(this.Width * barLength), this.Height - 2);
            if (Application.RenderWithVisualStyles)
            {
                VisualStyleRenderer glyphRenderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
                glyphRenderer.DrawBackground(e.Graphics, rec);
            }
            else
                e.Graphics.FillRectangle(Brushes.Green, rec);

            e.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width-1, this.Height-1);


        }
    }


}
