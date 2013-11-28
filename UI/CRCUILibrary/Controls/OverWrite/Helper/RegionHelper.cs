using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace CRC.Controls
{
    public static class RegionHelper
    {
        /// <summary>
        /// 为控件设置关联的窗口区域.
        /// </summary>
        /// <param name="control">要设置窗口区域的控件.</param>
        /// <param name="bounds">窗口区域.</param>
        /// <param name="radius">圆角半径.</param>
        /// <param name="roundStyle">圆角样式.</param>
        public static void SetControlRegion(Control control, Rectangle bounds,int radius,RoundStyle roundStyle)
        {
            using (GraphicsPath path =GraphicsPathHelper.CreateFilletRectangle(bounds, radius, roundStyle, true)) 
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                if (control.Region != null)
                {
                    control.Region.Dispose();
                }
                control.Region = region;
            }
        }

        /// <summary>
        /// 为控件设置关联的窗口区域.
        /// </summary>
        /// <param name="control">要设置窗口区域的控件.</param>
        /// <param name="bounds">窗口区域.</param>
        public static void SetControlRegion(Control control, Rectangle bounds)
        {
            SetControlRegion(control, bounds, 8, RoundStyle.All);
        }
    }


}
