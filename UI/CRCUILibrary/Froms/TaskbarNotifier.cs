// C# TaskbarNotifier Class v1.0
// by John O'Byrne - 02 december 2002
// 01 april 2003 : Small fix in the OnMouseUp handler
// 11 january 2003 : Patrick Vanden Driessche <pvdd@devbrains.be> added a few enhancements
//           Small Enhancements/Bugfix
//           Small bugfix: When Content text measures larger than the corresponding ContentRectangle
//                         the focus rectangle was not correctly drawn. This has been solved.
//           Added KeepVisibleOnMouseOver
//           Added ReShowOnMouseOver
//           Added If the Title or Content are too long to fit in the corresponding Rectangles,
//                 the text is truncateed and the ellipses are appended (StringTrimming).

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CRC.Froms
{
    public delegate void IndexEventHandler(object sender, PlaceIndexEventArgs e);

    /// <summary>
    /// </summary>
    public class PlaceIndexEventArgs : EventArgs
    {

        public PlaceIndexEventArgs(int idx)
        {
            Index = idx;
        }

        public int Index { get; set; }

    }

    /// <summary>
    ///     <para>TaskbarNotifier允许显示MSN风格/剥皮即时消息弹出窗口</para>
    ///     TaskbarNotifier allows to display MSN style/Skinned instant messaging popups
    /// </summary>
    public class TaskbarNotifier : Form
    {
        #region TaskbarNotifier Protected Members

        protected Bitmap _BackgroundBitmap = null;
        protected Bitmap _CloseBitmap = null;
        protected Point _CloseBitmapLocation;
        protected Size _CloseBitmapSize;
        protected string _ContentText;
        protected int _HideEvents;
        protected Color _HoverContentColor = Color.FromArgb(0, 0, 0x66);
        protected Font _HoverContentFont = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        protected Color _HoverTitleColor = Color.FromArgb(255, 0, 0);
        protected Font _HoverTitleFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
        protected int _IncrementHide;
        protected int _IncrementShow;
        protected bool _IsMouseDown = false;
        protected bool _IsMouseOverClose = false;
        protected bool _IsMouseOverContent = false;
        protected bool _IsMouseOverPopup = false;
        protected bool _IsMouseOverTitle = false;
        protected bool _KeepVisibleOnMouseOver = true; // Added Rev 002
        protected Color _NormalContentColor = Color.FromArgb(0, 0, 0);
        protected Font _NormalContentFont = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        protected Color _NormalTitleColor = Color.FromArgb(255, 0, 0);
        protected Font _NormalTitleFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
        protected bool _ReShowOnMouseOver = false; // Added Rev 002
        protected Rectangle _RealContentRectangle;
        protected Rectangle _RealTitleRectangle;
        protected int _ShowEvents;
        protected TaskbarStates _TaskbarStates = TaskbarStates.Hidden;
        protected int _TimeToHide;
        protected int _TimeToShow;
        protected int _TimeToStay;
        protected Timer _Timer = new Timer();
        protected string _TitleText;
        protected int _VisibleEvents;
        protected Rectangle _WorkAreaRectangle;

        #endregion

        #region TaskbarNotifier Public Members

        public bool CloseClickable = true;
        public bool ContentClickable = true;
        public bool EnableSelectionRectangle = true;
        public bool TitleClickable = false;
        public Rectangle TitleRectangle { get; set; }
        public Rectangle ContentRectangle { get; set; }

        public event EventHandler CloseClick = null;
        public event EventHandler TitleClick = null;
        public event EventHandler ContentClick = null;
        public event IndexEventHandler MessageHide = null;

        #endregion

        #region MultiWindow

        private int _Index;

        #endregion

        #region TaskbarNotifier Enums

        /// <summary>
        ///     <para>不同弹出动画，状态列表</para>
        ///     List of the different popup animation status
        /// </summary>
        public enum TaskbarStates
        {
            /// <summary>
            ///     隐藏
            /// </summary>
            Hidden = 0,

            /// <summary>
            ///     正在显示
            /// </summary>
            Appearing = 1,

            /// <summary>
            ///     可见的
            /// </summary>
            Visible = 2,

            /// <summary>
            ///     正在消失中
            /// </summary>
            Disappearing = 3
        }

        #endregion

        #region TaskbarNotifier Constructor

        /// <summary>
        ///     <para>该构造TaskbarNotifier</para>
        ///     The Constructor for TaskbarNotifier
        /// </summary>
        public TaskbarNotifier()
        {
            // Window Style
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = false;
            TopMost = true;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;

            _Timer.Enabled = true;
            _Timer.Tick += OnTimer;
        }

        #endregion

        #region TaskbarNotifier Properties

        /// <summary>
        ///     <para>获取当前TaskbarState（隐藏，显示，可见，隐藏中）</para>
        ///     Get the current _TaskbarStates (Hidden, showing, Visible, hiding)
        /// </summary>
        public TaskbarStates TaskbarState
        {
            get { return _TaskbarStates; }
        }

        /// <summary>
        ///     <para>获取/设置弹出窗口标题文字</para>
        ///     Get/Set the popup Title Text
        /// </summary>
        public string TitleText
        {
            get { return _TitleText; }
            set
            {
                _TitleText = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置弹出的文本内容</para>
        ///     Get/Set the popup Content Text
        /// </summary>
        public string ContentText
        {
            get { return _ContentText; }
            set
            {
                _ContentText = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置普通标题颜色</para>
        ///     Get/Set the Normal Title Color
        /// </summary>
        public Color NormalTitleColor
        {
            get { return _NormalTitleColor; }
            set
            {
                _NormalTitleColor = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置悬停标题颜色</para>
        ///     Get/Set the Hover Title Color
        /// </summary>
        public Color HoverTitleColor
        {
            get { return _HoverTitleColor; }
            set
            {
                _HoverTitleColor = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置正常的内容颜色</para>
        ///     Get/Set the Normal Content Color
        /// </summary>
        public Color NormalContentColor
        {
            get { return _NormalContentColor; }
            set
            {
                _NormalContentColor = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置内容悬停颜色</para>
        ///     Get/Set the Hover Content Color
        /// </summary>
        public Color HoverContentColor
        {
            get { return _HoverContentColor; }
            set
            {
                _HoverContentColor = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置普通标题字体</para>
        ///     Get/Set the Normal Title Font
        /// </summary>
        public Font NormalTitleFont
        {
            get { return _NormalTitleFont; }
            set
            {
                _NormalTitleFont = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置悬停标题字体</para>
        ///     Get/Set the Hover Title Font
        /// </summary>
        public Font HoverTitleFont
        {
            get { return _HoverTitleFont; }
            set
            {
                _HoverTitleFont = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置正常的内容字体</para>
        ///     Get/Set the Normal Content Font
        /// </summary>
        public Font NormalContentFont
        {
            get { return _NormalContentFont; }
            set
            {
                _NormalContentFont = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>获取/设置哈弗内容字体</para>
        ///     Get/Set the Hover Content Font
        /// </summary>
        public Font HoverContentFont
        {
            get { return _HoverContentFont; }
            set
            {
                _HoverContentFont = value;
                Refresh();
            }
        }

        /// <summary>
        ///     <para>表示如果弹出应保持可见，当鼠标指针位于它。添加牧师002</para>
        ///     Indicates if the popup should remain Visible when the mouse pointer is over it.
        ///     Added Rev 002
        /// </summary>
        public bool KeepVisibleOnMousOver
        {
            get { return _KeepVisibleOnMouseOver; }
            set { _KeepVisibleOnMouseOver = value; }
        }

        /// <summary>
        ///     <para>指示是否弹出，应再次出现时，鼠标移过它，而它的消失。添加牧师002</para>
        ///     Indicates if the popup should appear again when mouse moves over it while it's Disappearing.
        ///     Added Rev 002
        /// </summary>
        public bool ReShowOnMouseOver
        {
            get { return _ReShowOnMouseOver; }
            set { _ReShowOnMouseOver = value; }
        }

        /// <summary>
        ///     显示的时间.
        /// </summary>
        public int TimeToShow
        {
            get { return _TimeToShow; }
            set { _TimeToShow = value; }
        }

        /// <summary>
        ///     停留的时间.
        /// </summary>
        public int TimeToStay
        {
            get { return _TimeToStay; }
            set { _TimeToStay = value; }
        }

        /// <summary>
        ///     隐藏的时间
        /// </summary>
        public int TimeToHide
        {
            get { return _TimeToHide; }
            set { _TimeToHide = value; }
        }

        #endregion

        #region TaskbarNotifier Public Methods

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        /// <summary>
        ///     <para>显示窗体.</para>
        ///     Displays the popup for a certain amount of time
        /// </summary>
        /// <param name="strTitle">The string which will be shown as the title of the popup</param>
        /// <param name="strContent">The string which will be shown as the content of the popup</param>
        /// <param name="nTimeToShow">Duration of the showing animation (in milliseconds)</param>
        /// <param name="nTimeToStay">Duration of the Visible state before collapsing (in milliseconds)</param>
        /// <param name="nTimeToHide">Duration of the hiding animation (in milliseconds)</param>
        /// <returns>Nothing</returns>
        public void Show(string strTitle, string strContent, int nTimeToShow, int nTimeToStay, int nTimeToHide)
        {
            _WorkAreaRectangle = Screen.GetWorkingArea(_WorkAreaRectangle);
            _TitleText = strTitle;
            _ContentText = strContent;
            _VisibleEvents = nTimeToStay;
            CalculateMouseRectangles();

            // We calculate the pixel increment and the _Timer value for the showing animation
            int nEvents;
            if(nTimeToShow > 10)
            {
                nEvents = Math.Min((nTimeToShow/10), _BackgroundBitmap.Height);
                _ShowEvents = nTimeToShow/nEvents;
                _IncrementShow = _BackgroundBitmap.Height/nEvents;
            }
            else
            {
                _ShowEvents = 10;
                _IncrementShow = _BackgroundBitmap.Height;
            }

            // We calculate the pixel increment and the _Timer value for the hiding animation
            if(nTimeToHide > 10)
            {
                nEvents = Math.Min((nTimeToHide/10), _BackgroundBitmap.Height);
                _HideEvents = nTimeToHide/nEvents;
                _IncrementHide = _BackgroundBitmap.Height/nEvents;
            }
            else
            {
                _HideEvents = 10;
                _IncrementHide = _BackgroundBitmap.Height;
            }

            switch(_TaskbarStates)
            {
                case TaskbarStates.Hidden:
                    _TaskbarStates = TaskbarStates.Appearing;
                    SetBounds(_WorkAreaRectangle.Right - _BackgroundBitmap.Width - 17, _WorkAreaRectangle.Bottom - _Index*_BackgroundBitmap.Height, _BackgroundBitmap.Width, 0);
                    _Timer.Interval = _ShowEvents;
                    _Timer.Start();
                    // We Show the popup without stealing focus
                    ShowWindow(Handle, 4);
                    break;

                case TaskbarStates.Appearing:
                    Refresh();
                    break;

                case TaskbarStates.Visible:
                    _Timer.Stop();
                    _Timer.Interval = _VisibleEvents;
                    _Timer.Start();
                    Refresh();
                    break;

                case TaskbarStates.Disappearing:
                    _Timer.Stop();
                    _TaskbarStates = TaskbarStates.Visible;
                    SetBounds(_WorkAreaRectangle.Right - _BackgroundBitmap.Width - 17, _WorkAreaRectangle.Bottom - (_BackgroundBitmap.Height + 1)*_Index, _BackgroundBitmap.Width, _BackgroundBitmap.Height*_Index);
                    _Timer.Interval = _VisibleEvents;
                    _Timer.Start();
                    Refresh();
                    break;
            }
        }

        /// <summary>
        ///     显示窗体.
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strContent"></param>
        /// <param name="count"></param>
        public void Show(string strTitle, string strContent, int count)
        {
            _Index = count;
            Show(strTitle, strContent, _TimeToShow, _TimeToStay, _TimeToHide);
        }

        /// <summary>
        ///     <para>隐藏弹出窗体.</para>
        ///     Hides the popup
        /// </summary>
        /// <returns>Nothing</returns>
        public new void Hide()
        {
            if(_TaskbarStates == TaskbarStates.Hidden)
                return;
            _Timer.Stop();
            _TaskbarStates = TaskbarStates.Hidden;
            base.Hide();
            MessageHide(this, new PlaceIndexEventArgs(_Index));
        }

        /// <summary>
        ///     <para>设置背景图片.</para>
        ///     Sets the background bitmap and its transparency color
        /// </summary>
        /// <param name="strFilename">Path of the Background Bitmap on the disk</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be Visible</param>
        /// <returns>Nothing</returns>
        public void SetBackgroundBitmap(string strFilename, Color transparencyColor)
        {
            _BackgroundBitmap = new Bitmap(strFilename);
            Width = _BackgroundBitmap.Width;
            Height = _BackgroundBitmap.Height;
            Region = BitmapToRegion(_BackgroundBitmap, transparencyColor);
        }

        /// <summary>
        ///     <para>设置背景图片</para>
        ///     Sets the background bitmap and its transparency color
        /// </summary>
        /// <param name="image">Image/Bitmap object which represents the Background Bitmap</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be Visible</param>
        /// <returns>Nothing</returns>
        public void SetBackgroundBitmap(Image image, Color transparencyColor)
        {
            _BackgroundBitmap = new Bitmap(image);
            Width = _BackgroundBitmap.Width;
            Height = _BackgroundBitmap.Height;
            Region = BitmapToRegion(_BackgroundBitmap, transparencyColor);
        }

        /// <summary>
        ///     <para>设置关闭按钮的图片.</para>
        ///     Sets the 3-State Close Button bitmap, its transparency color and its coordinates
        /// </summary>
        /// <param name="strFilename">Path of the 3-state Close button Bitmap on the disk (width must a multiple of 3)</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be Visible</param>
        /// <param name="position">Location of the close button on the popup</param>
        /// <returns>Nothing</returns>
        public void SetCloseBitmap(string strFilename, Color transparencyColor, Point position)
        {
            _CloseBitmap = new Bitmap(strFilename);
            _CloseBitmap.MakeTransparent(transparencyColor);
            _CloseBitmapSize = new Size(_CloseBitmap.Width/3, _CloseBitmap.Height);
            _CloseBitmapLocation = position;
        }

        /// <summary>
        ///     <para>设置关闭按钮的图片</para>
        ///     Sets the 3-State Close Button bitmap, its transparency color and its coordinates
        /// </summary>
        /// <param name="image">
        ///     Image/Bitmap object which represents the 3-state Close button Bitmap (width must be a multiple of
        ///     3)
        /// </param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be Visible</param>
        /// ///
        /// <param name="position">Location of the close button on the popup</param>
        /// <returns>Nothing</returns>
        public void SetCloseBitmap(Image image, Color transparencyColor, Point position)
        {
            _CloseBitmap = new Bitmap(image);
            _CloseBitmap.MakeTransparent(transparencyColor);
            _CloseBitmapSize = new Size(_CloseBitmap.Width/3, _CloseBitmap.Height);
            _CloseBitmapLocation = position;
        }

        #endregion

        #region TaskbarNotifier Protected Methods

        /// <summary>
        ///     绘制关闭按钮.
        /// </summary>
        /// <param name="grfx"></param>
        protected void DrawCloseButton(Graphics grfx)
        {
            if(_CloseBitmap == null)
                return;
            var rectDest = new Rectangle(_CloseBitmapLocation, _CloseBitmapSize);
            Rectangle rectSrc;

            if(_IsMouseOverClose)
            {
                if(_IsMouseDown)
                    rectSrc = new Rectangle(new Point(_CloseBitmapSize.Width*2, 0), _CloseBitmapSize);
                else
                    rectSrc = new Rectangle(new Point(_CloseBitmapSize.Width, 0), _CloseBitmapSize);
            }
            else
                rectSrc = new Rectangle(new Point(0, 0), _CloseBitmapSize);


            grfx.DrawImage(_CloseBitmap, rectDest, rectSrc, GraphicsUnit.Pixel);
        }

        /// <summary>
        ///     绘制文本.
        /// </summary>
        /// <param name="grfx"></param>
        protected void DrawText(Graphics grfx)
        {
            if(!string.IsNullOrEmpty(_TitleText))
            {
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                if(_IsMouseOverTitle)
                    grfx.DrawString(_TitleText, _HoverTitleFont, new SolidBrush(_HoverTitleColor), TitleRectangle, sf);
                else
                    grfx.DrawString(_TitleText, _NormalTitleFont, new SolidBrush(NormalTitleColor), TitleRectangle, sf);
            }

            if(!string.IsNullOrEmpty(_ContentText))
            {
                var sf = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.MeasureTrailingSpaces,
                    Trimming = StringTrimming.Word
                };

                if(_IsMouseOverContent)
                {
                    grfx.DrawString(_ContentText, _HoverContentFont, new SolidBrush(_HoverContentColor), ContentRectangle, sf);
                    if(EnableSelectionRectangle)
                        ControlPaint.DrawBorder3D(grfx, _RealContentRectangle, Border3DStyle.Etched, Border3DSide.Top | Border3DSide.Bottom | Border3DSide.Left | Border3DSide.Right);

                }
                else
                    grfx.DrawString(_ContentText, _NormalContentFont, new SolidBrush(_NormalContentColor), ContentRectangle, sf);
            }
        }

        /// <summary>
        ///     计算鼠标区域.
        /// </summary>
        protected void CalculateMouseRectangles()
        {
            Graphics grfx = CreateGraphics();
            var sf = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.MeasureTrailingSpaces};
            SizeF sizefTitle = grfx.MeasureString(_TitleText, _HoverTitleFont, TitleRectangle.Width, sf);
            SizeF sizefContent = grfx.MeasureString(_ContentText, _HoverContentFont, ContentRectangle.Width, sf);
            grfx.Dispose();

            // Added Rev 002
            //We should check if the title size really fits inside the pre-defined title rectangle
            if(sizefTitle.Height > TitleRectangle.Height)
            {
                _RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, TitleRectangle.Width, TitleRectangle.Height);
            }
            else
            {
                _RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, (int) sizefTitle.Width, (int) sizefTitle.Height);
            }
            _RealTitleRectangle.Inflate(0, 2);

            // Added Rev 002
            //We should check if the Content size really fits inside the pre-defined Content rectangle
            if(sizefContent.Height > ContentRectangle.Height)
            {
                _RealContentRectangle = new Rectangle((ContentRectangle.Width - (int) sizefContent.Width)/2 + ContentRectangle.Left, ContentRectangle.Top, (int) sizefContent.Width, ContentRectangle.Height);
            }
            else
            {
                _RealContentRectangle = new Rectangle((ContentRectangle.Width - (int) sizefContent.Width)/2 + ContentRectangle.Left, (ContentRectangle.Height - (int) sizefContent.Height)/2 + ContentRectangle.Top, (int) sizefContent.Width, (int) sizefContent.Height);
            }
            _RealContentRectangle.Inflate(0, 2);
        }

        /// <summary>
        ///     从图片中获取区域.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="transparencyColor"></param>
        /// <returns></returns>
        protected Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
        {
            if(bitmap == null)
                throw new ArgumentNullException("bitmap", "Bitmap cannot be null!");

            int height = bitmap.Height;
            int width = bitmap.Width;

            var path = new GraphicsPath();

            for(int j = 0; j < height; j++)
            {
                for(int i = 0; i < width; i++)
                {
                    if(bitmap.GetPixel(i, j) == transparencyColor)
                        continue;

                    int x0 = i;

                    while((i < width) && (bitmap.GetPixel(i, j) != transparencyColor))
                        i++;

                    path.AddRectangle(new Rectangle(x0, j, i - x0, 1));
                }
            }

            var region = new Region(path);
            path.Dispose();
            return region;
        }

        #endregion

        #region TaskbarNotifier Events Overrides

        /// <summary>
        ///     引发Timer
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ea"></param>
        protected void OnTimer(Object obj, EventArgs ea)
        {
            switch(_TaskbarStates)
            {
                case TaskbarStates.Appearing:
                    if(Height < _BackgroundBitmap.Height)
                        SetBounds(Left, Top - _IncrementShow, Width, Height + _IncrementShow);
                    else
                    {
                        _Timer.Stop();
                        Height = _BackgroundBitmap.Height;
                        _Timer.Interval = _VisibleEvents;
                        _TaskbarStates = TaskbarStates.Visible;
                        _Timer.Start();
                    }
                    break;

                case TaskbarStates.Visible:
                    _Timer.Stop();
                    _Timer.Interval = _HideEvents;
                    // Added Rev 002
                    if((_KeepVisibleOnMouseOver && !_IsMouseOverPopup) || (!_KeepVisibleOnMouseOver))
                    {
                        _TaskbarStates = TaskbarStates.Disappearing;
                    }
                    //_TaskbarStates = TaskbarStates.Disappearing;		// Rev 002
                    _Timer.Start();
                    break;

                case TaskbarStates.Disappearing:
                    // Added Rev 002
                    if(_ReShowOnMouseOver && _IsMouseOverPopup)
                    {
                        _TaskbarStates = TaskbarStates.Appearing;
                    }
                    else
                    {
                        if(Top < _WorkAreaRectangle.Bottom - _Index*_BackgroundBitmap.Height)
                            SetBounds(Left, Top + _IncrementHide, Width, Height - _IncrementHide);
                        else
                            Hide();
                    }
                    break;
            }

        }


        protected override void OnMouseEnter(EventArgs ea)
        {
            base.OnMouseEnter(ea);
            _IsMouseOverPopup = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs ea)
        {
            base.OnMouseLeave(ea);
            _IsMouseOverPopup = false;
            _IsMouseOverClose = false;
            _IsMouseOverTitle = false;
            _IsMouseOverContent = false;
            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);

            bool bContentModified = false;

            if((mea.X > _CloseBitmapLocation.X) && (mea.X < _CloseBitmapLocation.X + _CloseBitmapSize.Width) && (mea.Y > _CloseBitmapLocation.Y) && (mea.Y < _CloseBitmapLocation.Y + _CloseBitmapSize.Height) && CloseClickable)
            {
                if(!_IsMouseOverClose)
                {
                    _IsMouseOverClose = true;
                    _IsMouseOverTitle = false;
                    _IsMouseOverContent = false;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else if(_RealContentRectangle.Contains(new Point(mea.X, mea.Y)) && ContentClickable)
            {
                if(!_IsMouseOverContent)
                {
                    _IsMouseOverClose = false;
                    _IsMouseOverTitle = false;
                    _IsMouseOverContent = true;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else if(_RealTitleRectangle.Contains(new Point(mea.X, mea.Y)) && TitleClickable)
            {
                if(!_IsMouseOverTitle)
                {
                    _IsMouseOverClose = false;
                    _IsMouseOverTitle = true;
                    _IsMouseOverContent = false;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else
            {
                if(_IsMouseOverClose || _IsMouseOverTitle || _IsMouseOverContent)
                    bContentModified = true;

                _IsMouseOverClose = false;
                _IsMouseOverTitle = false;
                _IsMouseOverContent = false;
                Cursor = Cursors.Default;
            }

            if(bContentModified)
                Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs mea)
        {
            base.OnMouseDown(mea);
            _IsMouseDown = true;

            if(_IsMouseOverClose)
                Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            base.OnMouseUp(mea);
            _IsMouseDown = false;

            if(_IsMouseOverClose)
            {
                Hide();

                if(CloseClick != null)
                    CloseClick(this, new EventArgs());
            }
            else if(_IsMouseOverTitle)
            {
                if(TitleClick != null)
                    TitleClick(this, new EventArgs());
            }
            else if(_IsMouseOverContent)
            {
                if(ContentClick != null)
                    ContentClick(this, new EventArgs());
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            grfx.PageUnit = GraphicsUnit.Pixel;

            var offscreenBitmap = new Bitmap(_BackgroundBitmap.Width, _BackgroundBitmap.Height);
            Graphics offScreenGraphics = Graphics.FromImage(offscreenBitmap);

            if(_BackgroundBitmap != null)
            {
                offScreenGraphics.DrawImage(_BackgroundBitmap, 0, 0, _BackgroundBitmap.Width, _BackgroundBitmap.Height);
            }

            DrawCloseButton(offScreenGraphics);
            DrawText(offScreenGraphics);

            grfx.DrawImage(offscreenBitmap, 0, 0);
        }

        #endregion

        //多窗口显示

    }
}