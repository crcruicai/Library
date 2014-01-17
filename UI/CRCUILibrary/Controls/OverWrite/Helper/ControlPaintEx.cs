using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    ///     Control绘制帮助器.
    /// </summary>
    public sealed class ControlPaintEx
    {
        private ControlPaintEx()
        {
        }

        #region 公有函数

        /// <summary>
        ///     绘制检测标记(打钩标记)
        /// </summary>
        /// <param name="g">绘图平面</param>
        /// <param name="rect">打钩的区域</param>
        /// <param name="color">标记的颜色</param>
        public static void DrawCheckedFlag(Graphics g, Rectangle rect, Color color)
        {
            var points = new PointF[3];
            points[0] = new PointF(rect.X + rect.Width/4.5f, rect.Y + rect.Height/2.5f);
            points[1] = new PointF(rect.X + rect.Width/2.5f, rect.Bottom - rect.Height/3f);
            points[2] = new PointF(rect.Right - rect.Width/4.0f, rect.Y + rect.Height/4.5f);
            using(var pen = new Pen(color, 2F))
            {
                g.DrawLines(pen, points);
            }
        }

        /// <summary>
        ///     绘制玻璃效果(默认效果)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="glassRect">表示定义椭圆的边框</param>
        /// <param name="alphaCenter">椭圆中心的System.Drawing.Color 的 alpha 值</param>
        /// <param name="alphaSurround">椭圆边界的System.Drawing.Color 的 alpha 值</param>
        public static void DrawGlass(Graphics g, RectangleF glassRect, int alphaCenter, int alphaSurround)
        {
            DrawGlass(g, glassRect, Color.White, alphaCenter, alphaSurround);
        }

        /// <summary>
        ///     绘制玻璃效果
        /// </summary>
        /// <param name="g"></param>
        /// <param name="glassRect">表示定义椭圆的边框</param>
        /// <param name="glassColor">椭圆内部的颜色</param>
        /// <param name="alphaCenter">椭圆中心的System.Drawing.Color 的 alpha 值</param>
        /// <param name="alphaSurround">椭圆边界的System.Drawing.Color 的 alpha 值</param>
        public static void DrawGlass(Graphics g, RectangleF glassRect, Color glassColor, int alphaCenter, int alphaSurround)
        {
            using(var path = new GraphicsPath())
            {
                path.AddEllipse(glassRect); //绘制一个椭圆
                using(var brush = new PathGradientBrush(path))
                {
                    //设置中心的颜色
                    brush.CenterColor = Color.FromArgb(alphaCenter, glassColor);
                    //设置周边的颜色.
                    brush.SurroundColors = new[] {Color.FromArgb(alphaSurround, glassColor)};

                    //指定中心点.
                    brush.CenterPoint = new PointF(glassRect.X + glassRect.Width/2, glassRect.Y + glassRect.Height/2);
                    //绘制.
                    g.FillPath(brush, path);
                }
            }
        }

        /// <summary>
        ///     绘制背景图片.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        public static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect)
        {
            DrawBackgroundImage(g, backgroundImage, backColor, backgroundImageLayout, bounds, clipRect, Point.Empty, RightToLeft.No);
        }

        /// <summary>
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        /// <param name="scrollOffset"></param>
        public static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, Point scrollOffset)
        {
            DrawBackgroundImage(g, backgroundImage, backColor, backgroundImageLayout, bounds, clipRect, scrollOffset, RightToLeft.No);
        }

        /// <summary>
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        /// <param name="scrollOffset"></param>
        /// <param name="rightToLeft"></param>
        public static void DrawBackgroundImage(Graphics g, Image backgroundImage, Color backColor, ImageLayout backgroundImageLayout, Rectangle bounds, Rectangle clipRect, Point scrollOffset, RightToLeft rightToLeft)
        {
            if(g == null)
            {
                throw new ArgumentNullException("g");
            }
            if(backgroundImageLayout == ImageLayout.Tile)
            {
                using(var brush = new TextureBrush(backgroundImage, WrapMode.Tile))
                {
                    if(scrollOffset != Point.Empty)
                    {
                        Matrix transform = brush.Transform;
                        transform.Translate(scrollOffset.X, scrollOffset.Y);
                        brush.Transform = transform;
                    }
                    g.FillRectangle(brush, clipRect);
                    return;
                }
            }
            Rectangle rect = CalculateBackgroundImageRectangle(bounds, backgroundImage, backgroundImageLayout);


            if((rightToLeft == RightToLeft.Yes) && (backgroundImageLayout == ImageLayout.None))
            {
                rect.X += clipRect.Width - rect.Width;
            }
            using(var brush2 = new SolidBrush(backColor))
            {
                g.FillRectangle(brush2, clipRect);
            }
            if(!clipRect.Contains(rect))
            {
                if((backgroundImageLayout == ImageLayout.Stretch) || (backgroundImageLayout == ImageLayout.Zoom))
                {
                    rect.Intersect(clipRect);
                    g.DrawImage(backgroundImage, rect);
                }
                else if(backgroundImageLayout == ImageLayout.None)
                {
                    rect.Offset(clipRect.Location);
                    Rectangle destRect = rect;
                    destRect.Intersect(clipRect);
                    var rectangle3 = new Rectangle(Point.Empty, destRect.Size);
                    g.DrawImage(backgroundImage, destRect, rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, GraphicsUnit.Pixel);

                }
                else
                {
                    Rectangle rectangle4 = rect;
                    rectangle4.Intersect(clipRect);
                    var rectangle5 = new Rectangle(new Point(rectangle4.X - rect.X, rectangle4.Y - rect.Y), rectangle4.Size);
                    g.DrawImage(backgroundImage, rectangle4, rectangle5.X, rectangle5.Y, rectangle5.Width, rectangle5.Height, GraphicsUnit.Pixel);

                }
            }
            else
            {
                var imageAttr = new ImageAttributes();
                imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(backgroundImage, rect, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, imageAttr);
                imageAttr.Dispose();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="orientation"></param>
        public static void DrawScrollBarTrack(Graphics g, Rectangle rect, Color begin, Color end, Orientation orientation)
        {
            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            var blend = new Blend {Factors = new[] {1f, 0.5f, 0f}, Positions = new[] {0f, 0.5f, 1f}};

            DrawGradientRect(g, rect, begin, end, begin, begin, blend, mode, true, false);

        }

        /// <summary>
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="orientation"></param>
        /// <param name="changeColor"></param>
        public static void DrawScrollBarThumb(Graphics g, Rectangle rect, Color begin, Color end, Color border, Color innerBorder, Orientation orientation, bool changeColor)

        {
            if(changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            var blend = new Blend {Factors = new[] {1f, 0.5f, 0f}, Positions = new[] {0f, 0.5f, 1f}};

            if(bHorizontal)
            {
                rect.Inflate(0, -1);
            }
            else
            {
                rect.Inflate(-1, 0);
            }

            DrawGradientRoundRect(g, rect, begin, end, border, innerBorder, blend, mode, 4, RoundStyle.All, true, true);

        }

        /// <summary>
        ///     绘制滚动滑块的箭头区域.
        /// </summary>
        /// <param name="g">绘图平面</param>
        /// <param name="rect">绘图区域</param>
        /// <param name="begin">渐变起始颜色.</param>
        /// <param name="end">渐变结束颜色</param>
        /// <param name="border"></param>
        /// <param name="innerBorder">内框颜色.</param>
        /// <param name="fore">前颜色</param>
        /// <param name="orientation">滚动条的方向,水平或垂直</param>
        /// <param name="arrowDirection">箭头的方向</param>
        /// <param name="changeColor">是否启用渐变色</param>
        public static void DrawScrollBarArraw(Graphics g, Rectangle rect, Color begin, Color end, Color border, 
            Color innerBorder, Color fore, Orientation orientation, ArrowDirection arrowDirection, bool changeColor)
        {
            if(changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            rect.Inflate(-1, -1);

            var blend = new Blend {Factors = new[] {1f, 0.5f, 0f}, Positions = new[] {0f, 0.5f, 1f}};

            DrawGradientRoundRect(g, rect, begin, end, border, innerBorder, blend, mode, 4, RoundStyle.All, true, true);

            using(var brush = new SolidBrush(fore))
            {
                RenderHelper.RenderArrowInternal(g, rect, arrowDirection, brush);
            }
        }

        /// <summary>
        ///     绘制滚动条的滚动滑块.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">滑块的区域.</param>
        /// <param name="begin">起始颜色</param>
        /// <param name="end">结束颜色</param>
        public static void DrawScrollBarSizer(Graphics g, Rectangle rect, Color begin, Color end)
        {
            var blend = new Blend {Factors = new[] {1f, 0.5f, 0f}, Positions = new[] {0f, 0.5f, 1f}};

            DrawGradientRect(g, rect, begin, end, begin, begin, blend, LinearGradientMode.Horizontal, true, false);
        }

        #endregion

        #region 友元函数

        /// <summary>
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
        internal static void DrawGradientRect(Graphics g, Rectangle rect, Color begin, Color end, 
            Color border, Color innerBorder, Blend blend, LinearGradientMode mode, bool drawBorder, bool drawInnerBorder)
        {
            using(var brush = new LinearGradientBrush(rect, begin, end, mode))
            {
                brush.Blend = blend;
                g.FillRectangle(brush, rect);
            }

            if(drawBorder)
            {
                ControlPaint.DrawBorder(g, rect, border, ButtonBorderStyle.Solid);
            }

            if(drawInnerBorder)
            {
                rect.Inflate(-1, -1);
                ControlPaint.DrawBorder(g, rect, border, ButtonBorderStyle.Solid);
            }
        }

        /// <summary>
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
        internal static void DrawGradientRoundRect(Graphics g, Rectangle rect, Color begin, Color end, Color border, 
            Color innerBorder, Blend blend, LinearGradientMode mode, int radios, 
            RoundStyle roundStyle, bool drawBorder, bool drawInnderBorder)
        {
            using(GraphicsPath path = GraphicsPathHelper.CreateFilletRectangle(rect, radios, roundStyle, true))
            {
                using(var brush = new LinearGradientBrush(rect, begin, end, mode))
                {
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }

                if(drawBorder)
                {
                    using(var pen = new Pen(border))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            if(drawInnderBorder)
            {
                rect.Inflate(-1, -1);
                using(GraphicsPath path = GraphicsPathHelper.CreateFilletRectangle(rect, radios, roundStyle, true))
                {
                    using(var pen = new Pen(innerBorder))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        /// <summary>
        ///     计算背景图片的区域.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="backgroundImage">背景图片</param>
        /// <param name="imageLayout">指定控件上图片的位置.</param>
        /// <returns></returns>
        internal static Rectangle CalculateBackgroundImageRectangle(Rectangle bounds, Image backgroundImage, ImageLayout imageLayout)
        {
            Rectangle rectangle = bounds;
            if(backgroundImage != null)
            {
                switch(imageLayout)
                {
                    case ImageLayout.None: //无 
                        rectangle.Size = backgroundImage.Size;
                        return rectangle;

                    case ImageLayout.Tile: //
                        return rectangle;

                    case ImageLayout.Center: //图片居中显示.
                    {
                        rectangle.Size = backgroundImage.Size;
                        Size size = bounds.Size;
                        if(size.Width > rectangle.Width)
                        {
                            rectangle.X = (size.Width - rectangle.Width)/2;
                        }
                        if(size.Height > rectangle.Height)
                        {
                            rectangle.Y = (size.Height - rectangle.Height)/2;
                        }
                        return rectangle;
                    }
                    case ImageLayout.Stretch: //图片拉伸.
                        rectangle.Size = bounds.Size;
                        return rectangle;

                    case ImageLayout.Zoom: //图片放大.
                    {
                        Size size2 = backgroundImage.Size;
                        float num = bounds.Width/((float) size2.Width);
                        float num2 = bounds.Height/((float) size2.Height);
                        if(num >= num2)
                        {
                            rectangle.Height = bounds.Height;
                            rectangle.Width = (int) ((size2.Width*num2) + 0.5);
                            if(bounds.X >= 0)
                            {
                                rectangle.X = (bounds.Width - rectangle.Width)/2;
                            }
                            return rectangle;
                        }
                        rectangle.Width = bounds.Width;
                        rectangle.Height = (int) ((size2.Height*num) + 0.5);
                        if(bounds.Y >= 0)
                        {
                            rectangle.Y = (bounds.Height - rectangle.Height)/2;
                        }
                        return rectangle;
                    }
                }
            }
            return rectangle;
        }

        #endregion
    }
}