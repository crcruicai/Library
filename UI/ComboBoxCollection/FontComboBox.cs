using System.Drawing;
using System.Windows.Forms; //Import Drawing NameSpace

namespace CRC.Controls
{
    /// <summary>
    /// 字体下拉框.
    /// </summary>
    public class FontComboBox : ComboBox
    {
        #region 字段与变量
        private SolidBrush FontForeColour;
        #endregion 字段与变量
        public FontComboBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;

            if (!DesignMode)
            {
                FontFamily[] families = FontFamily.Families;

                foreach (FontFamily family in families)
                {
                    FontStyle style = FontStyle.Bold; 

                    //These Are Only Available In Italic, Not In "Regular", So Test For Them, Else, Exception!!
                    if (family.Name == "Monotype Corsiva" || family.Name == "Brush Script MT" 
                        || family.Name == "Harlow Solid Italic" || family.Name == "Palace Script MT" || family.Name == "Vivaldi")
                    {
                        style = style | FontStyle.Italic; //Set Style To Italic, To Overt "Regular" & Exception
                    }


                    Items.Add(new Font(family.Name, 12, style, GraphicsUnit.Point)); 
                }
            }
        }
        #region 构造函数

        #endregion 构造函数

        #region 属性

        #endregion 属性

        #region 公共函数
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Brush FontBrush; //Brush To Be used

            //If No Current Colour
            if (FontForeColour == null)
            {
                FontForeColour = new SolidBrush(e.ForeColor);
            }
            else
            {
                if (!FontForeColour.Color.Equals(e.ForeColor)) 
                {
                    FontForeColour.Dispose(); 
                    FontForeColour = new SolidBrush(e.ForeColor); 
                }
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) 
            {
                FontBrush = SystemBrushes.HighlightText;
            }
            else
            {
                FontBrush = FontForeColour;
            }

            Font font = (Font)Items[e.Index]; 

            e.DrawBackground(); 

            e.Graphics.DrawString(font.Name, font, FontBrush, e.Bounds.X, e.Bounds.Y);

            e.DrawFocusRectangle();

        }


        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            Font font = (Font)Items[e.Index]; 
            SizeF stringSize = e.Graphics.MeasureString(font.Name, font); 
            e.ItemHeight = (int)stringSize.Height;
            e.ItemWidth = (int)stringSize.Width;
            base.OnMeasureItem(e);

        }

     
        #endregion 公共函数

        #region 私有函数

        #endregion 私有函数

    }




   public class FontData //Font ComboBox Class
    {
       public Font FCFont; //Font Used

       public FontData(Font FCCurrFont) 
        {
            FCFont = FCCurrFont;  //Set This Font Equal To Font Supplied
        } 
    
       /// <summary>
       /// Override ToString Method To Display Current Font's Name
       /// </summary>
       /// <returns></returns>
        public override string ToString() 
        { 
            return FCFont.Name; //Display Font Name
        } 

    }
}
