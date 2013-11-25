using System.Drawing;
using System.Windows.Forms; //Import Drawing NameSpace

namespace CRC.Controls
{
    /// <summary>
    /// ����������.
    /// </summary>
    public class FontComboBox : ComboBox
    {
        #region �ֶ������
        private SolidBrush FontForeColour;
        #endregion �ֶ������
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
        #region ���캯��

        #endregion ���캯��

        #region ����

        #endregion ����

        #region ��������
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

     
        #endregion ��������

        #region ˽�к���

        #endregion ˽�к���

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
