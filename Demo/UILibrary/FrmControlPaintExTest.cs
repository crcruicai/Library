/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/23 22:42:11
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRC.Controls;
using System.Net;
namespace UILibrary
{
    public partial class FrmControlPaintExTest : Form
    {
        public FrmControlPaintExTest()
        {
            InitializeComponent();
         

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawCheckedFlagTest(g);
            DrawScrollBarArrawTest(g);
            DrawScrollBarSizerTest(g);
            base.OnPaint(e);
        }
        private void DrawCheckedFlagTest(Graphics g)
        {
           
            Rectangle rect = new Rectangle(50, 50, 20, 20);
            ControlPaintEx.DrawCheckedFlag(g, rect, Color.Black);

        }

        private void DrawScrollBarArrawTest(Graphics g)
        {
           
            Rectangle rect = new Rectangle(100, 50, 20, 20);
            ControlPaintEx.DrawScrollBarArraw(g, rect, Color.Black, Color.Bisque, Color.BlueViolet,
                Color.CadetBlue, Color.Chartreuse, Orientation.Vertical, ArrowDirection.Down, true);
        }

        private void DrawScrollBarSizerTest(Graphics g)
        {
            Rectangle rect = new Rectangle(100, 70, 20, 50);
            ControlPaintEx.DrawScrollBarSizer(g, rect, Color.Black, Color.Bisque);
        }
     


    }
}
