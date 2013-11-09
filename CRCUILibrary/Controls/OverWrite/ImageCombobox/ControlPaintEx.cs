using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */
    public sealed class ControlPaintEx
    {
        private ControlPaintEx() { }

        /// <summary>
        /// 绘制检测标记
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public static void DrawCheckedFlag(
            Graphics g, Rectangle rect, Color color)
        {
            PointF[] points = new PointF[3];
            points[0] = new PointF(
                rect.X + rect.Width / 4.5f,
                rect.Y + rect.Height / 2.5f);
            points[1] = new PointF(
                rect.X + rect.Width / 2.5f,
                rect.Bottom - rect.Height / 3f);
            points[2] = new PointF(
                rect.Right - rect.Width / 4.0f,
                rect.Y + rect.Height / 4.5f);
            using (Pen pen = new Pen(color, 2F))
            {
                g.DrawLines(pen, points);
            }
        }

        /// <summary>
        /// 绘制玻璃效果(默认效果)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="glassRect">表示定义椭圆的边框</param>
        /// <param name="alphaCenter">椭圆中心的System.Drawing.Color 的 alpha 值</param>
        /// <param name="alphaSurround">椭圆边界的System.Drawing.Color 的 alpha 值</param>
        public static void DrawGlass(
            Graphics g, RectangleF glassRect,
            int alphaCenter, int alphaSurround)
        {
            DrawGlass(g, glassRect, Color.White, alphaCenter, alphaSurround);
        }

        /// <summary>
        /// 绘制玻璃效果
        /// </summary>
        /// <param name="g"></param>
        /// <param name="glassRect">表示定义椭圆的边框</param>
        /// <param name="glassColor">椭圆内部的颜色</param>
        /// <param name="alphaCenter">椭圆中心的System.Drawing.Color 的 alpha 值</param>
        /// <param name="alphaSurround">椭圆边界的System.Drawing.Color 的 alpha 值</param>
        public static void DrawGlass(
           Graphics g,
            RectangleF glassRect,
            Color glassColor,
            int alphaCenter,
            int alphaSurround)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(glassRect); //绘制一个椭圆
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    //设置中心的颜色
                    brush.CenterColor = Color.FromArgb(alphaCenter, glassColor);
                    //设置周边的颜色.
                    brush.SurroundColors = new Color[] { 
                        Color.FromArgb(alphaSurround, glassColor) };
                    //指定中心点.
                    brush.CenterPoint = new PointF(
                        glassRect.X + glassRect.Width / 2,
                        glassRect.Y + glassRect.Height / 2);
                    //绘制.
                    g.FillPath(brush, path);
                }
            }
        }
        /// <summary>
        /// 绘制背景图片.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect)
        {
            DrawBackgroundImage(
                g,
                backgroundImage,
                backColor,
                backgroundImageLayout,
                bounds,
                clipRect,
                Point.Empty,
                RightToLeft.No);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        /// <param name="scrollOffset"></param>
        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect,
            Point scrollOffset)
        {
            DrawBackgroundImage(
                g,
                backgroundImage,
                backColor,
                backgroundImageLayout,
                bounds,
                clipRect,
                scrollOffset,
                RightToLeft.No);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        /// <param name="scrollOffset"></param>
        /// <param name="rightToLeft"></param>
        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect,
            Point scrollOffset,
            RightToLeft rightToLeft)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (backgroundImageLayout == ImageLayout.Tile)
            {
                using (TextureBrush brush = new TextureBrush(backgroundImage, WrapMode.Tile))
                {
                    if (scrollOffset != Point.Empty)
                    {
                        Matrix transform = brush.Transform;
                        transform.Translate((float)scrollOffset.X, (float)scrollOffset.Y);
                        brush.Transform = transform;
                    }
                    g.FillRectangle(brush, clipRect);
                    return;
                }
            }
            Rectangle rect = CalculateBackgroundImageRectangle(
                bounds,
                backgroundImage,
                backgroundImageLayout);
            if ((rightToLeft == RightToLeft.Yes) &&
                (backgroundImageLayout == ImageLayout.None))
            {
                rect.X += clipRect.Width - rect.Width;
            }
            using (SolidBrush brush2 = new SolidBrush(backColor))
            {
                g.FillRectangle(brush2, clipRect);
            }
            if (!clipRect.Contains(rect))
            {
                if ((backgroundImageLayout == ImageLayout.Stretch) ||
                    (backgroundImageLayout == ImageLayout.Zoom))
                {
                    rect.Intersect(clipRect);
                    g.DrawImage(backgroundImage, rect);
                }
                else if (backgroundImageLayout == ImageLayout.None)
                {
                    rect.Offset(clipRect.Location);
                    Rectangle destRect = rect;
                    destRect.Intersect(clipRect);
                    Rectangle rectangle3 = new Rectangle(Point.Empty, destRect.Size);
                    g.DrawImage(
                        backgroundImage,
                        destRect,
                        rectangle3.X,
                        rectangle3.Y,
                        rectangle3.Width,
                        rectangle3.Height,
                        GraphicsUnit.Pixel);
                }
                else
                {
                    Rectangle rectangle4 = rect;
                    rectangle4.Intersect(clipRect);
                    Rectangle rectangle5 = new Rectangle(
                        new Point(rectangle4.X - rect.X, rectangle4.Y - rect.Y),
                        rectangle4.Size);
                    g.DrawImage(
                        backgroundImage,
                        rectangle4,
                        rectangle5.X,
                        rectangle5.Y,
                        rectangle5.Width,
                        rectangle5.Height,
                        GraphicsUnit.Pixel);
                }
            }
            else
            {
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(
                    backgroundImage,
                    rect,
                    0,
                    0,
                    backgroundImage.Width,
                    backgroundImage.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
                imageAttr.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="orientation"></param>
        public static void DrawScrollBarTrack(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Orientation orientation)
        {
            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRect(
                g,
                rect,
                begin,
                end,
                begin,
                begin,
                blend,
                mode,
                true,
                false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="orientation"></param>
        /// <param name="changeColor"></param>
        public static void DrawScrollBarThumb(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Orientation orientation,
            bool changeColor)
        {
            if (changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            if (bHorizontal)
            {
                rect.Inflate(0, -1);
            }
            else
            {
                rect.Inflate(-1, 0);
            }

            DrawGradientRoundRect(
                g,
                rect,
                begin,
                end,
                border,
                innerBorder,
                blend,
                mode,
                4,
                RoundStyle.All,
                true,
                true);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="fore"></param>
        /// <param name="orientation"></param>
        /// <param name="arrowDirection"></param>
        /// <param name="changeColor"></param>
        public static void DrawScrollBarArraw(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Color fore,
            Orientation orientation,
            ArrowDirection arrowDirection,
            bool changeColor)
        {
            if (changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            rect.Inflate(-1, -1);

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRoundRect(
                g,
                rect,
                begin,
                end,
                border,
                innerBorder,
                blend,
                mode,
                4,
                RoundStyle.All,
                true,
                true);

            using (SolidBrush brush = new SolidBrush(fore))
            {
                RenderHelper.RenderArrowInternal(
                    g,
                    rect,
                    arrowDirection,
                    brush);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public static void DrawScrollBarSizer(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end)
        {
            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRect(
                 g,
                 rect,
                 begin,
                 end,
                 begin,
                 begin,
                 blend,
                 LinearGradientMode.Horizontal,
                 true,
                 false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="blend"></param>
        /// <param name="mode"></param>
        /// <param name="drawBorder"></param>
        /// <param name="drawInnerBorder"></param>
        internal static void DrawGradientRect(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Blend blend,
            LinearGradientMode mode,
            bool drawBorder,
            bool drawInnerBorder)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, begin, end, mode))
            {
                brush.Blend = blend;
                g.FillRectangle(brush, rect);
            }

            if (drawBorder)
            {
                ControlPaint.DrawBorder(
                    g, rect, border, ButtonBorderStyle.Solid);
            }

            if (drawInnerBorder)
            {
                rect.Inflate(-1, -1);
                ControlPaint.DrawBorder(
                    g, rect, border, ButtonBorderStyle.Solid);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="blend"></param>
        /// <param name="mode"></param>
        /// <param name="radios"></param>
        /// <param name="roundStyle"></param>
        /// <param name="drawBorder"></param>
        /// <param name="drawInnderBorder"></param>
        internal static void DrawGradientRoundRect(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Blend blend,
            LinearGradientMode mode,
            int radios,
            RoundStyle roundStyle,
            bool drawBorder,
            bool drawInnderBorder)
        {
            using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                rect, radios, roundStyle, true))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                      rect, begin, end, mode))
                {
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }

                if (drawBorder)
                {
                    using (Pen pen = new Pen(border))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            if (drawInnderBorder)
            {
                rect.Inflate(-1, -1);
                using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                    rect, radios, roundStyle, true))
                {
                    using (Pen pen = new Pen(innerBorder))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
        /// <summary>
        /// 计算背景图片的区域.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="backgroundImage">背景图片</param>
        /// <param name="imageLayout">指定控件上图片的位置.</param>
        /// <returns></returns>
        internal static Rectangle CalculateBackgroundImageRectangle(
            Rectangle bounds,
            Image backgroundImage,
            ImageLayout imageLayout)
        {
            Rectangle rectangle = bounds;
            if (backgroundImage != null)
            {
                switch (imageLayout)
                {
                    case ImageLayout.None://无 
                        rectangle.Size = backgroundImage.Size;
                        return rectangle;

                    case ImageLayout.Tile://
                        return rectangle;

                    case ImageLayout.Center://图片居中显示.
                        {
                            rectangle.Size = backgroundImage.Size;
                            Size size = bounds.Size;
                            if (size.Width > rectangle.Width)
                            {
                                rectangle.X = (size.Width - rectangle.Width) / 2;
                            }
                            if (size.Height > rectangle.Height)
                            {
                                rectangle.Y = (size.Height - rectangle.Height) / 2;
                            }
                            return rectangle;
                        }
                    case ImageLayout.Stretch://图片拉伸.
                        rectangle.Size = bounds.Size;
                        return rectangle;

                    case ImageLayout.Zoom://图片放大.
                        {
                            Size size2 = backgroundImage.Size;
                            float num = ((float)bounds.Width) / ((float)size2.Width);
                            float num2 = ((float)bounds.Height) / ((float)size2.Height);
                            if (num >= num2)
                            {
                                rectangle.Height = bounds.Height;
                                rectangle.Width = (int)((size2.Width * num2) + 0.5);
                                if (bounds.X >= 0)
                                {
                                    rectangle.X = (bounds.Width - rectangle.Width) / 2;
                                }
                                return rectangle;
                            }
                            rectangle.Width = bounds.Width;
                            rectangle.Height = (int)((size2.Height * num) + 0.5);
                            if (bounds.Y >= 0)
                            {
                                rectangle.Y = (bounds.Height - rectangle.Height) / 2;
                            }
                            return rectangle;
                        }
                }
            }
            return rectangle;
        }
    }
}
