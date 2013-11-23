
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/23 10:43:25
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

namespace CRC
{
    /// <summary>
    /// 颜色下拉框.
    /// <para>继承于ComboBox</para>
    /// </summary>
    public class ColorComboBox:ComboBox
    {
        #region 字段与变量

        #endregion 字段与变量
        
        #region 构造函数
        public ColorComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed; //DrawMode          
            Init();          
        }

        private void Init()
        {
            if (!DesignMode)
            {
                foreach (string CurrColourName in System.Enum.GetNames(typeof(System.Drawing.KnownColor))) //Get System's Known Colours
                {
                    Items.Add(Color.FromName(CurrColourName)); //Add Known Colours
                }
            }

        }

        #endregion 构造函数

        #region 属性

        #endregion 属性

        #region 公共函数
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return;
            }
            //Get Colour Object From Items List 
            Color CurrColour = (Color)Items[e.Index];

            //Create A Rectangle To Fit New Item 
            Rectangle ColourSize = new Rectangle(2, e.Bounds.Top + 2, e.Bounds.Width, e.Bounds.Height - 2);

            Brush ColourBrush; //New Colour Brush To Draw With

            e.DrawBackground(); //Draw Item's Background
            e.DrawFocusRectangle(); //Draw Item's Focus Rectangle

            if (e.State == System.Windows.Forms.DrawItemState.Selected) //If Item Selected
            {
                ColourBrush = Brushes.White; //Change To White
            }
            else
            {
                ColourBrush = Brushes.Black; //Change Back to Black
            }
            SolidBrush brush = new SolidBrush(CurrColour);

            e.Graphics.DrawRectangle(new Pen(CurrColour), ColourSize); //Draw New Item Rectangle With Current Colour
            e.Graphics.FillRectangle(brush, ColourSize); //Fill New Item rectangle With Current Colour

            //Add Border Around Rectangle
            ColourSize.Inflate(1, 1); //Border Size
            e.Graphics.DrawRectangle(Pens.Black, ColourSize); //Draw New Border
            
            brush.Color=GetNewColor (CurrColour);
           
           
            //Draw Current Colour Name, In The Middle 
            e.Graphics.DrawString(CurrColour.Name, Font,brush, e.Bounds.Height + 5, ((e.Bounds.Height - Font.Height) / 2) + e.Bounds.Top);
            brush=null;
            base.OnDrawItem(e);
        }


        private Color GetNewColor(Color one)
        {
            int r, g, b;
            r = one.R > 127 ? one.R - 128 : one.R + 128;
            g =one.G > 127 ? one.G - 128:one.G +128;
            b = one.B> 127 ? one.B - 128:one.B +128;
           
            return Color.FromArgb(r, g, b);
        }


        #endregion 公共函数

        #region 私有函数

        #endregion 私有函数






    }
}
