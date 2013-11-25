using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace CRC.Controls
{
    /// <summary>
    /// 系统虚线 下拉框.
    /// </summary>
    public class LineComboBox : ComboBox 
    {
        
        /// <summary>
        /// Constructor Setting Startup Settings
        /// </summary>
        public LineComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed; 
            this.DropDownStyle = ComboBoxStyle.DropDownList;          
            FillLineTypes();
        }

        /// <summary>
        /// Adds All DashStyles To List
        /// </summary>
        private void FillLineTypes() 
        { 
            this.Items.Clear();
            string[] DashStyles = Enum.GetNames(typeof(DashStyle));
            for (int i = 0; i <= DashStyles.Length - 1; i++) 
            {                
                this.Items.Add(DashStyles[i]);
            }    
        } 

        /// <summary>
        /// Override OnDrawItem, To Draw The Lines
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e) 
        { 
           
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (e.Index < 0)  return;
           
            Rectangle rect = e.Bounds;
            Pen pen = new Pen(Color.Black, 2);
            DashStyle style;
            if (Enum .TryParse <DashStyle>(Items[e.Index].ToString(),out style))
            {
                pen.DashStyle = style;
                e.Graphics.DrawLine(pen, rect.X, rect.Y + rect.Height / 2, rect.Right, rect.Y + rect.Height / 2);
            }

            base.OnDrawItem(e);
           
        }

    }
}
