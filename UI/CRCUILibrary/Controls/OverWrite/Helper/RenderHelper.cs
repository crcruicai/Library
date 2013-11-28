using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    /// 渲染帮助器.
    /// </summary>
    public class RenderHelper
    {

        #region 背景

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">渲染的区域.</param>
        /// <param name="baseColor">基色</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        /// <param name="style">指定圆角的风格.</param>
        /// <param name="drawBorder"></param>
        /// <param name="drawGlass"></param>
        /// <param name="mode">指定线性渐变的方向.</param>
        public static void RenderBackgroundInternal(Graphics g,Rectangle rect,Color baseColor,Color borderColor,
            Color innerBorderColor,RoundStyle style,bool drawBorder,bool drawGlass,LinearGradientMode mode)
        {
            RenderBackgroundInternal(g,rect,baseColor,borderColor,innerBorderColor,style,8,drawBorder,drawGlass,mode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">渲染的区域.</param>
        /// <param name="baseColor">基色</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        /// <param name="style">指定圆角的风格.</param>
        /// <param name="roundWidth">指定圆角的半径</param>
        /// <param name="drawBorder"></param>
        /// <param name="drawGlass"></param>
        /// <param name="mode">指定线性渐变的方向.</param>
        public static void RenderBackgroundInternal(Graphics g,Rectangle rect,Color baseColor,Color borderColor,
                Color innerBorderColor,RoundStyle style,int roundWidth,bool drawBorder,bool drawGlass,LinearGradientMode mode)
        {
            RenderBackgroundInternal(g,rect,baseColor,borderColor,innerBorderColor,style,8,0.45f,drawBorder,drawGlass,mode);
        }

        /// <summary>
        /// 渲染内部背景.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">渲染的区域.</param>
        /// <param name="baseColor">基色</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        /// <param name="style">指定圆角的风格.</param>
        /// <param name="roundWidth">指定圆角的半径</param>
        /// <param name="drawBorder"></param>
        /// <param name="drawGlass"></param>
        /// <param name="mode">指定线性渐变的方向.</param>
        /// <param name="basePosition"></param>
        public static void RenderBackgroundInternal(Graphics g,Rectangle rect,Color baseColor,Color borderColor,
            Color innerBorderColor,RoundStyle style,int roundWidth,float basePosition,bool drawBorder,bool drawGlass,LinearGradientMode mode)
        {
            if (drawBorder)
            {
                rect.Width--;
                rect.Height--;
            }

            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, mode))
            {
                DecoratorBrush(brush,baseColor, basePosition);
                if (style != RoundStyle.None)
                {
                    using (GraphicsPath path =GraphicsPathHelper.CreateFilletRectangle(rect, roundWidth, style, false))  
                    {
                        g.FillPath(brush, path);//填充区域.
                    }

                    if (baseColor.A > 80)
                    {
                        Rectangle rectTop = GetGradientModeRectangle(rect, basePosition, mode);
                        DrawTranslucence(g, rectTop, roundWidth);
                    }

                    if (drawGlass)
                    {
                        RectangleF glassRect = GetGlassRectangle(rect, basePosition, mode);
                        ControlPaintEx.DrawGlass(g, glassRect, 170, 0);
                    }

                    if (drawBorder)
                    {
                        DrawRoundBorder(g,rect,borderColor,innerBorderColor,style, roundWidth);
                    }
                }
                else
                {
                    g.FillRectangle(brush, rect);
                    if (baseColor.A > 80)
                    {
                        Rectangle rectTop = GetGradientModeRectangle(rect, basePosition, mode); 
                                               
                        using (SolidBrush brushAlpha =new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
                        {
                            g.FillRectangle(brushAlpha, rectTop);
                        }
                    }

                    if (drawGlass)
                    {
                        RectangleF glassRect = GetGlassRectangle(rect, basePosition, mode);
                        ControlPaintEx.DrawGlass(g, glassRect, 200, 0);
                    }

                    if (drawBorder)
                    {
                        DrawNormalBorder(g, rect, borderColor, innerBorderColor);
                    }
                }
            }
        }
        #endregion 背景



        /// <summary>
        /// 绘制半透明区域.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="roundWidth"></param>
        private static void DrawTranslucence(Graphics g,Rectangle rect, int roundWidth)
        {
            using (GraphicsPath pathTop = GraphicsPathHelper.CreateFilletRectangle(rect, roundWidth, RoundStyle.Top, false))
            {
                using (SolidBrush brushAlpha = new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
                {
                    g.FillPath(brushAlpha, pathTop);
                }
            }

        }

        /// <summary>
        /// 装饰画刷
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="baseColor"></param>
        /// <param name="basePosition"></param>
        private static void DecoratorBrush(LinearGradientBrush brush,Color baseColor, float basePosition )
        {
            Color[] colors = new Color[4];
            colors[0] = GetColor(baseColor, 0, 35, 24, 9);
            colors[1] = GetColor(baseColor, 0, 13, 8, 3);
            colors[2] = baseColor;
            colors[3] = GetColor(baseColor, 0, 35, 24, 9);

            ColorBlend blend = new ColorBlend();
            blend.Positions = new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
            blend.Colors = colors;
            brush.InterpolationColors = blend;
            
        }

        /// <summary>
        /// 绘制圆角边框.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="borderColor"></param>
        /// <param name="innerBorderColor"></param>
        /// <param name="style"></param>
        /// <param name="roundWidth"></param>
        private static void DrawRoundBorder(Graphics g, Rectangle rect, Color borderColor, Color innerBorderColor, RoundStyle style, int roundWidth)
        {
            using (GraphicsPath path = GraphicsPathHelper.CreateFilletRectangle(rect, roundWidth, style, false))
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawPath(pen, path);
                }
            }

            rect.Inflate(-1, -1);
            using (GraphicsPath path = GraphicsPathHelper.CreateFilletRectangle(rect, roundWidth, style, false))
            {
                using (Pen pen = new Pen(innerBorderColor))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        /// <summary>
        /// 计算新的渐变区域.
        /// </summary>
        /// <param name="rectTop"></param>
        /// <param name="basePosition"></param>
        /// <param name="mode"></param>
        private static Rectangle GetGradientModeRectangle(Rectangle rectTop, float basePosition, LinearGradientMode mode)
        {
            if (mode == LinearGradientMode.Vertical)
            {
                rectTop.Height = (int)(rectTop.Height * basePosition);
            }
            else
            {
                rectTop.Width = (int)(rectTop.Width * basePosition);
            }
            return rectTop;
        }

        /// <summary>
        /// 计算透明区域.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="basePosition"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static RectangleF GetGlassRectangle(Rectangle rect, float basePosition, LinearGradientMode mode)
        {
            RectangleF glassRect = rect;
            if (mode == LinearGradientMode.Vertical)
            {
                glassRect.Y = rect.Y + rect.Height * basePosition;
                glassRect.Height = (rect.Height - rect.Height * basePosition) * 2;
            }
            else
            {
                glassRect.X = rect.X + rect.Width * basePosition;
                glassRect.Width = (rect.Width - rect.Width * basePosition) * 2;
            }
            return glassRect;
        }

        /// <summary>
        /// 绘制矩形区域的边框(内外边框)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">矩形区域</param>
        /// <param name="borderColor">外边框颜色.</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        private static void DrawNormalBorder(Graphics g, Rectangle rect,Color borderColor, Color innerBorderColor)
        {
            using (Pen pen = new Pen(borderColor))
            {
                g.DrawRectangle(pen, rect);//绘制外边框,
                rect.Inflate(-1, -1);
                pen.Color = innerBorderColor;
                g.DrawRectangle(pen, rect);//绘制内边框.
            }
        }

        #region 箭头
        /// <summary>
        /// 渲染箭头.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dropDownRect"></param>
        /// <param name="direction"></param>
        /// <param name="brush"></param>
        public static void RenderArrowInternal(Graphics g,Rectangle dropDownRect,ArrowDirection direction,Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { 
                        new Point(point.X + 1, point.Y - 4), 
                        new Point(point.X + 1, point.Y + 4), 
                        new Point(point.X - 2, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { 
                        new Point(point.X - 4, point.Y + 1), 
                        new Point(point.X + 4, point.Y + 1), 
                        new Point(point.X, point.Y - 2) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 2, point.Y - 4), 
                        new Point(point.X - 2, point.Y + 4), 
                        new Point(point.X + 1, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 4, point.Y - 1), 
                        new Point(point.X + 4, point.Y - 1), 
                        new Point(point.X, point.Y + 2) };
                    break;
            }
            g.FillPolygon(brush, points);
        }
        #endregion

        #region 图片
        /// <summary>
        /// 渲染图片.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="image"></param>
        /// <param name="imageRect"></param>
        /// <param name="alpha"></param>
        public static void RenderAlphaImage(Graphics g,Image image,Rectangle imageRect,float alpha)
        {
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = { 
                    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
                    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
                    new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},        
                    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix,ColorMatrixFlag.Default,ColorAdjustType.Bitmap);

                g.DrawImage(image,imageRect,0,0,image.Width,image.Height,GraphicsUnit.Pixel,imageAttributes);
            }
        }
        #endregion

        #region 网格点
        /// <summary>
        /// 渲染网格点.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="pixelsBetweenDots"></param>
        /// <param name="outerColor"></param>
        public static void RenderGrid(Graphics g,Rectangle rect,Size pixelsBetweenDots,Color outerColor)
        {
            int outerWin32Corlor = ColorTranslator.ToWin32(outerColor);
            IntPtr hdc = g.GetHdc();

            for (int x = rect.X; x <= rect.Right; x += pixelsBetweenDots.Width)
            {
                for (int y = rect.Y; y <= rect.Bottom; y += pixelsBetweenDots.Height)
                {
                    SetPixel(hdc, x, y, outerWin32Corlor);
                }
            }

            g.ReleaseHdc(hdc);
        }
        #endregion


        #region 文字

        /// <summary>
        /// 绘制TabText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="page"></param>
        /// <param name="tabRect"></param>
        /// <param name="hasImage">是否有图片.</param>
        public static void DrawTabText(Graphics g, TabPage page, Rectangle tabRect, TabAlignment Alignment,int Radius,bool hasImage)
        {
            Rectangle textRect = tabRect;
            RectangleF newTextRect;
            StringFormat sf;

            //绘制文本.
            switch (Alignment)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (hasImage)//如果有图片
                    {
                        textRect.X = tabRect.X + Radius / 2 + tabRect.Height - 2;
                        textRect.Width = tabRect.Width - Radius - tabRect.Height;
                    }
                    TextRenderer.DrawText(g, page.Text, page.Font, textRect, page.ForeColor);
                    break;
                case TabAlignment.Left:
                    if (hasImage)
                    {
                        textRect.Height = tabRect.Height - tabRect.Width + 2;
                    }
                    g.TranslateTransform(textRect.X, textRect.Bottom);
                    g.RotateTransform(270F);

                    sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;

                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;

                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(page.Text, page.Font, brush, newTextRect, sf);

                    }
                    g.ResetTransform();

                    break;
                case TabAlignment.Right:
                    if (hasImage)
                    {
                        textRect.Y = tabRect.Y + Radius / 2 + tabRect.Width - 2;
                        textRect.Height = tabRect.Height - Radius - tabRect.Width;
                    }
                    g.TranslateTransform(textRect.Right, textRect.Y);
                    g.RotateTransform(90F);
                    sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(page.Text, page.Font, brush, newTextRect, sf);
                    }
                    g.ResetTransform();
                    break;
            }
        }



        #endregion

        /// <summary>
        /// 基于一种颜色,获取另一种颜色.
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color GetColor(Color colorBase, int a, int r, int g, int b)           
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(0, a + a0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(0, r + r0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(0, g + g0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(0, b + b0); }

            return Color.FromArgb(a, r, g, b);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern uint SetPixel(IntPtr hdc, int X, int Y, int crColor);
    }
}
