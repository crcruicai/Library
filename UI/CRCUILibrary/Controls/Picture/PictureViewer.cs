#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/22 10:50:17
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using CRC;
namespace CRC.Controls
{
    /// <summary>
    /// 图片查看控件,支持滚动栏.
    /// <para>CTRL按键 支持缩放,ALT按键 支持水平滚动</para>
    /// </summary>
    public class PictureViewer : ScrollableControl
    {
        #region 字段与变量

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建PictureViewer的一个实例
        /// </summary>
        public PictureViewer()
        {
            this.AutoScroll = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | 
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            //InitializeComponent();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置图片文件
        /// </summary>
        [Description("如果同时设置了Image属性和ImageFile属性，使用Image属性设置的图像。")]
        public string ImageFile
        {
            get;
            set;
        }

        /// <summary>
        /// 图像
        /// </summary>
        private Image _image = null;
        /// <summary>
        /// 获取或设置图像。
        /// </summary>
        [Description("如果同时设置了Image属性和ImageFile属性，使用Image属性设置的图像。")]
        public Image Image
        {
            get
            {
                if (_image != null)
                {
                    return _image;
                }
                else
                {
                    if (!string.IsNullOrEmpty(ImageFile))
                    {
                        _image = Image.FromFile(ImageFile);
                    }
                }
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        /// <summary>
        /// 缩放步进
        /// </summary>
        private int _zoomStep = 10;
        /// <summary>
        /// 获取或设置缩放步进，1至100之间
        /// </summary>
        public int ZoomStep
        {
            get { return _zoomStep; }
            set
            {
                if (value < 1)
                {
                    _zoomStep = 1;
                }
                else if (value > 100)
                {
                    _zoomStep = 100;
                }
                else
                {
                    _zoomStep = value;
                }
            }
        }

        /// <summary>
        /// 最大缩放比率，表示为百分之几。
        /// </summary>
        private int _maxZoomPercent = 1000;
        /// <summary>
        /// 获取或设置最大缩放比率，表示为百分之几。
        /// </summary>
        public int MaxZoomPercent
        {
            get { return _maxZoomPercent; }
            set { _maxZoomPercent = value; }
        }

        /// <summary>
        /// 当前缩放比例
        /// </summary>
        private int _zoomPercent = 100;
        /// <summary>
        /// 获取或设置当前缩放比例
        /// </summary>
        public int ZoomPercent
        {
            get { return _zoomPercent; }
            set
            {
                if (_zoomPercent < 1)
                {
                    _zoomPercent = 1;
                }
                else if (_zoomPercent > MaxZoomPercent)
                {
                    _zoomPercent = MaxZoomPercent;
                }
                else
                {
                    _zoomPercent = value;
                }
                this.Invalidate();
            }
        }

        /// <summary>
        /// 适应模式
        /// </summary>
        private FitMode _fitMode = FitMode.FitAll;
        /// <summary>
        /// 获取或设置初始适应模式
        /// </summary>
        public FitMode FitMode
        {
            get { return _fitMode; }
            set
            {
                _fitMode = value;
                if (_fitMode == FitMode.FitPercent)
                {
                    ZoomPercent = 100;
                }
                else
                {
                    //if(!this.DesignMode )
                    this.Invalidate();
                }
            }
        }
        #endregion

        #region 公共函数

        #endregion

        #region 私有函数
        /// <summary>
        /// 适应工作区
        /// </summary>
        /// <returns>图像大小</returns>
        private Size FitAll()
        {
            int width = 0;
            int height = 0;

            var imageRatio = (float)Image.Width / (float)Image.Height;
            // 较宽
            if (imageRatio > 1)
            {
                width = Math.Min(Image.Width, this.ClientSize.Width);
                height = (int)(width / imageRatio);
                if (height > this.ClientSize.Height)
                {
                    height = this.ClientSize.Height;
                    width = (int)(height * imageRatio);
                }
            }
            else
            {
                height = Math.Min(Image.Height, this.ClientSize.Height);
                width = (int)(height * imageRatio);
                if (width > this.ClientSize.Width)
                {
                    width = this.ClientSize.Width;
                    height = (int)(height / imageRatio);
                }
            }
            return new Size(width, height);
        }

        /// <summary>
        /// 自适应宽度
        /// </summary>
        /// <returns>图像大小</returns>
        private Size FitWidth()
        {
            int width = this.ClientSize.Width;
            int height = (int)(Image.Height * (float)width / (float)Image.Width);
            return new Size(width, height);
        }

        /// <summary>
        /// 适应到指定百分比
        /// </summary>
        /// <param name="percent">百分比，默认100</param>
        /// <returns>图像大小</returns>
        private Size FitPercent(int percent = 100)
        {
            if (percent == 100)
            {
                return Image.Size;
            }

            if (percent < 1)
            {
                percent = 1;
            }
            else if (percent > MaxZoomPercent)
            {
                percent = MaxZoomPercent;
            }

            int width = (int)(Image.Width * ((float)percent / 100f));
            int height = (int)(Image.Height * ((float)percent / 100f));
            return new Size(width, height);
        }

        /// <summary>
        /// 创建图像区域矩形
        /// </summary>
        private Rectangle CreateRectangle()
        {
            var size = Size.Empty;
            if (FitMode == FitMode.FitAll)
            {
                size = FitAll();
            }
            else if (FitMode == FitMode.FitWidth)
            {
                size = FitWidth();
            }
            else
            {
                size = FitPercent(this.ZoomPercent);
            }
            var x = Math.Max((this.Size.Width - size.Width) / 2, 0);
            var y = Math.Max((this.Size.Height - size.Height) / 2, 0);
            return new Rectangle(new Point(x, y), size);
        }


        /// <summary>
        /// 绘制图像
        /// </summary>
        private void DrawImage(Graphics e)
        {
            if (Image == null)
            {
                return;
            }
            //e.Clear(Color .Transparent);
            var rect = CreateRectangle();

            // 根据滚动条修改画布坐标原点的位置。
            Debug.WriteLine("H{0} V{1}".FormatWith(this.HorizontalScroll.Value, this.VerticalScroll.Value));
            e.TranslateTransform(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);

            // 设置滚动条出现的最小Size
            if (this.AutoScrollMinSize != rect.Size)
            {
                this.AutoScrollMinSize = rect.Size;
            }
            Debug.WriteLine("RECT {0}".FormatWith(rect.ToString()));
            e.DrawImage(Image, rect);
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            var g = pe.Graphics;
            DrawImage(pe.Graphics);
        }

        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <remarks>
        /// 按下Ctrl并滚动滚轮，缩放图片。
        /// </remarks>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta > 0)
                {
                    this.ZoomPercent += ZoomStep;
                }
                else
                {
                    this.ZoomPercent -= ZoomStep;
                }
                return;
            }
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                if (e.Delta < 0)
                {
                    if (this.HorizontalScroll.Maximum > this.HorizontalScroll.Value + 50)
                        this.HorizontalScroll.Value += 50;
                    else
                        this.HorizontalScroll.Value = this.HorizontalScroll.Maximum;
                }
                else
                {
                    if (this.HorizontalScroll.Value > 50)
                        this.HorizontalScroll.Value -= 50;
                    else
                    {
                        this.HorizontalScroll.Value = 0;
                    }
                }
                Graphics g = CreateGraphics();
                DrawImage(g);
              
            }
            else
            {
                base.OnMouseWheel(e);
            }
           
            
           
        }

        

        #endregion



    }

    /// <summary>
    /// 图像适应模式
    /// </summary>
    public enum FitMode
    {
        /// <summary>
        /// 全部适应
        /// </summary>
        FitAll,
        /// <summary>
        /// 适应宽度
        /// </summary>
        FitWidth,
        /// <summary>
        /// 适应到百分比
        /// </summary>
        FitPercent
    }
}
