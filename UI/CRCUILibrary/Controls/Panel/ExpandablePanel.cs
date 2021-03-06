﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace CRC.Controls
{
    //标题可以添加图片,请查看并修改TODO标记的位置.

    /// <summary>
    /// 可进行伸缩的Panel
    /// </summary>
    public class ExpandablePanel : Panel, INotifyPropertyChanged
    {
        #region  构造器

        public ExpandablePanel()
            : base()
        {
            _Title = new GradientLabel();
            InitializeTitle();
        }

        #endregion

        #region  变量与字段
        /// <summary>
        /// 渐变标题
        /// </summary>
        private GradientLabel _Title;

        /// <summary>
        /// 指示控件是否处于收缩状态.
        /// </summary>
        private bool _Collapsed = false;

        /// <summary>
        /// Panle展开时的Size.
        /// </summary>
        private Size ExpandSize;



        #endregion

        #region   属性
        /// <summary>
        /// 标题的渐变颜色1
        /// </summary>
        [Category("Appearance")]
        public Color TitleBackColor1
        {
            get
            {
                return _Title.BackColor;
            }
            set
            {
                if (value != _Title.BackColor)
                {
                    _Title.BackColor = value;
                    _Title.Invalidate();
                }
            }
        }

        /// <summary>
        /// 标题的渐变颜色2
        /// </summary>
        [Category("Appearance")]
        public Color TitleBackColor2
        {
            get
            {
                return _Title.BackColor2;
            }
            set
            {
                if (value != _Title.BackColor2)
                {
                    _Title.BackColor2 = value;
                    _Title.Invalidate();
                }
            }
        }

        /// <summary>
        /// 标题的前背景颜色.
        /// </summary>
        [Category("Appearance")]
        public Color TitleForeColor
        {
            get
            {
                return _Title.ForeColor;
            }
            set
            {
                if (value != _Title.ForeColor)
                {
                    _Title.ForeColor = value;
                    _Title.Invalidate();
                }
            }
        }

        /// <summary>
        /// 标题的高度
        /// </summary>
        [Category("Appearance")]
        public int TitleHeight
        {
            get
            {
                return _Title.Height;
            }
            set
            {
                if (value != _Title.Height)
                {
                    _Title.Height = value;
                    _Title.Invalidate();
                }
            }
        }

        /// <summary>
        /// 标题的字体
        /// </summary>
        [Category("Appearance")]
        public Font TitleFont
        {
            get
            {
                return _Title.Font;
            }
            set
            {
                if (value != _Title.Font)
                {
                    _Title.Font = value;
                    _Title.Invalidate();
                }
            }
        }
        /// <summary>
        /// 指定标题背景颜色渐变的方向.
        /// </summary>
        [Category("Appearance")]
        public LinearGradientMode GradientMode
        {
            get
            {
                return _Title.GradientMode;
            }
            set
            {
                if (value != _Title.GradientMode)
                {
                    _Title.GradientMode = value;
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// 指示Panel是否处于收缩状态.
        /// </summary>
        [Category("Appearance")]
        public bool Collapsed
        {
            get
            {
                return _Collapsed;
            }
            set
            {
                if (value != _Collapsed)
                {
                    ToggleCollapsed();
                    OnPropertyChanged("Collapsed");
                }
            }
        }

        [Category("Appearance")]
        public string TitleText
        {
            get
            {
                return _Title.Text;
            }
            set
            {
                if (value != _Title.Text)
                {
                    _Title.Text = value;
                    _Title.Invalidate();
                    OnPropertyChanged("Text");

                }

            }
        }


        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (value != base.Font)
                {
                    base.Font = value;
                    _Title.Font = value;
                    _Title.Invalidate();
                }

            }
        }

        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                if (value == false)
                {
                    ExpandSize = this.Size;
                }
                base.AutoSize = value;
            }
        }





        #endregion

        #region  函数

        /// <summary>
        /// 初始化标题.
        /// </summary>
        private void InitializeTitle()
        {
            _Title.AutoEllipsis = true;
            _Title.AutoSize = false;
            _Title.Dock = DockStyle.Top;
            //TODO:这里要添加一个显示图片,没有应该也可以.
            //_Title .Image =
            _Title.ImageAlign = ContentAlignment.MiddleRight;
            _Title.TextAlign = ContentAlignment.MiddleLeft;
            _Title.Visible = true;
            _Title.Click += new EventHandler(_Title_Click);
            this.Controls.Add(_Title);

        }

        void _Title_Click(object sender, EventArgs e)
        {
            Collapsed = !Collapsed;
        }



        /// <summary>
        /// 控制Panel展开或收缩.
        /// </summary>
        private void ToggleCollapsed()
        {
            if (_Collapsed) //处于收缩的状态,展开
            {
                this.Size = ExpandSize;
                this.Padding = new Padding(this.Padding.Left, 2, this.Padding.Right, this.Padding.Bottom);
                _Collapsed = false;
                //TODO:修改展开时的图片
                //_Title .Image =
            }
            else
            {
                this.Padding = new Padding(this.Padding.Left, 2, this.Padding.Right, this.Padding.Bottom);
                this.AutoSize = false;
                this.Height = _Title.Height + 2;
                _Collapsed = true;
                //TODO: 修改收缩时的图片.
                //_Title .Image =

            }

        }
        #endregion

        #region INotifyPropertyChanged 成员

        /// <summary>
        /// 当属性更改时,触发事件.
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    /// <summary>
    /// 自定义可伸缩的Panel.
    /// </summary>
    public class ExpandPanel : Panel, INotifyPropertyChanged
    {
        #region 字段与变量
        /// <summary>
        /// 指示控件是否处于收缩状态.
        /// </summary>
        private bool _Collapsed = false;

        /// <summary>
        /// Panle展开时的Size.
        /// </summary>
        private Size ExpandSize;

        #endregion 字段与变量

        #region 构造函数
        public ExpandPanel()
        {

        }
        #endregion 构造函数

        #region 属性
        private Color _TitleBackColor1=Color.White ;
        /// <summary>
        /// 标题的渐变颜色1
        /// </summary>
        [Category("Appearance")]
        public Color TitleBackColor1
        {
            get
            {
                return _TitleBackColor1;
            }
            set
            {
                if (value != _TitleBackColor1)
                {
                    _TitleBackColor1 = value;
                    Invalidate();
                }
            }
        }

        private Color _TitleBackColor2=Color .WhiteSmoke;
        /// <summary>
        /// 标题的渐变颜色2
        /// </summary>
        [Category("Appearance")]
        public Color TitleBackColor2
        {
            get
            {
                return _TitleBackColor2;
            }
            set
            {
                if (value != _TitleBackColor2)
                {
                    _TitleBackColor2 = value;
                    Invalidate();
                }
            }
        }

        private int _TitleHeight=25;
        /// <summary>
        /// 标题的高度
        /// </summary>
        [Category("Appearance")]
        public int TitleHeight
        {
            get
            {
                return _TitleHeight ;
            }
            set
            {
                if (value != _TitleHeight)
                {
                    _TitleHeight = value;
                    Invalidate();
                }
            }
        }

        private LinearGradientMode _GradientMode;
        /// <summary>
        /// 指定标题背景颜色渐变的方向.
        /// </summary>
        [Category("Appearance")]
        public LinearGradientMode GradientMode
        {
            get
            {
                return _GradientMode;
            }
            set
            {
                if (value !=_GradientMode)
                {
                    _GradientMode = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 指示Panel是否处于收缩状态.
        /// </summary>
        [Category("Appearance")]
        public bool Collapsed
        {
            get
            {
                return _Collapsed;
            }
            set
            {
                if (value != _Collapsed)
                {
                    ToggleCollapsed();
                    OnPropertyChanged("Collapsed");
                }
            }
        }

        private string _TitleText;
        [Category("Appearance")]
        public string TitleText
        {
            get
            {
               
                return _TitleText;
            }
            set
            {
                if (value != _TitleText)
                {
                    _TitleText = value;
                    Invalidate();
                    OnPropertyChanged("Text");

                }

            }
        }

        private StringAlignment  _Alignment= StringAlignment.Near;
        /// <summary>
        /// 标题文本的对齐方式.
        /// </summary>
        public StringAlignment  Alignment
        {
            get { return _Alignment; }
            set 
            {
                if (_Alignment != value)
                {
                    _Alignment = value;
                    Invalidate();
                }

            }
        }


        #endregion 属性

        #region 公共函数
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (_TitleBounds.Contains(e.Location) && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                OnPropertyChanged("Collapsed");
                ToggleCollapsed();
            }
            base.OnMouseClick(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTitle(e.Graphics);
        }
        #endregion 公共函数

        #region 私有函数

        private void ToggleCollapsed()
        {
            if (_Collapsed) //处于收缩的状态,展开
            {
                this.Size = ExpandSize;
                this.Padding = new Padding(this.Padding.Left, 2, this.Padding.Right, this.Padding.Bottom);
                _Collapsed = false;
                //TODO:修改展开时的图片
                //_Title .Image =
            }
            else
            {
                this.Padding = new Padding(this.Padding.Left, 2, this.Padding.Right, this.Padding.Bottom);
                ExpandSize = this.Size;
                this.Size = new Size(this.Width, _TitleHeight);
                //this.Height = _TitleHeight + 2;
                _Collapsed = true;
                //TODO: 修改收缩时的图片.
                //_Title .Image =

            }
        }

        private Rectangle _TitleBounds;

        private void DrawTitle(Graphics g)
        {
            // 渲染底纹.
            _TitleBounds = new Rectangle(0, 0, this.Width, _TitleHeight);
            using (LinearGradientBrush br = new LinearGradientBrush(_TitleBounds, _TitleBackColor1, _TitleBackColor2, _GradientMode))
            {
                g.FillRectangle(br, _TitleBounds);
            }

            // 绘制文本.
            StringFormat drawFormat = new StringFormat();

            drawFormat.Alignment = _Alignment;
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Trimming = StringTrimming.EllipsisWord;

            g.CompositingQuality = CompositingQuality.AssumeLinear;
            RectangleF rect = new RectangleF(0, 0, this.Width, _TitleHeight);
            rect.Inflate(-1, -1);

            g.DrawString(_TitleText, Font, new SolidBrush(ForeColor), rect, drawFormat);
            

        }

        

        #endregion 私有函数






        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }


    /// <summary>
    /// 带有渐变渲染色彩的Lable
    /// </summary>
    public class GradientLabel : Label
    {

        /// <summary>
        /// 背景颜色2
        /// </summary>
        private Color _BackColor2;

        /// <summary>
        /// 指定线性渐变的方向.
        /// </summary>
        private LinearGradientMode _GradientMode;

        /// <summary>
        /// 背景颜色2
        /// </summary>
        public Color BackColor2
        {
            get
            {
                return _BackColor2;
            }
            set
            {
                _BackColor2 = value;
                base.Invalidate();//重新绘制
            }

        }

        /// <summary>
        /// 指定Lable背景颜色渐变的方向.
        /// </summary>
        public LinearGradientMode GradientMode
        {
            get
            {
                return _GradientMode;
            }
            set
            {
                _GradientMode = value;
                base.Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush br = new LinearGradientBrush(rect, this.BackColor, _BackColor2, _GradientMode))
            {
                pevent.Graphics.FillRectangle(br, rect);

            }
        }





    }

}
