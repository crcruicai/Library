using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CRC.Controls
{

    #region 作者声明
    /* 作者：Starts_2000
     * 日期：2009-09-01
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */
    #endregion

    #region 使用方法
    /* 该控件的使用方法:
     *  
     *  _resizeHost = new PopupControlHost(_resizeControl);//_resizeControl 指定显示的内容.

        _regionHost.ChangeRegion = true;//设置显示区域。
        _regionHost.Opacity = 0.8F;//设置透明度。
        _resizeHost.CanResize = true;//设置可以改变大小。
        _resizeHost.OpenFocused = true;//把焦点设置到弹出窗体上。
     
     *  _resizeHost.Show(Button1);//Button1 父控件.表示将在Button1上显示内容.
     * 
     */
    #endregion

    #region PopupControlHost
    /// <summary>
    /// <para>弹出式控件宿主.</para>
    /// ToolStripDropDown控件的扩展.
    /// </summary>
    /// <remarks >
    /// <para></para>
    /// 该控件的使用方法:
    ///  _resizeHost = new PopupControlHost(_resizeControl);//_resizeControl 指定显示的内容.
    /// <para>
    ///  _regionHost.ChangeRegion = true;//设置显示区域。</para>
    ///  <para>
    ///  _regionHost.Opacity = 0.8F;//设置透明度。</para>
    ///  _resizeHost.CanResize = true;//设置可以改变大小。
    ///  _resizeHost.OpenFocused = true;//把焦点设置到弹出窗体上。
    /// 
    ///  _resizeHost.Show(Button1);//Button1 父控件.表示将在Button1上显示内容.
    /// 
    /// 
    /// 
    /// 
    ///</remarks>

    public class PopupControlHost : ToolStripDropDown
    {
        #region Fields

        private ToolStripControlHost _controlHost;
        private Control _popupControl;
        private bool _changeRegion;
        private bool _openFocused;
        private bool _acceptAlt;
        private bool _resizableTop;
        private bool _resizableLeft;
        private bool _canResize = false;
        private PopupControlHost _ownerPopup;
        private PopupControlHost _childPopup;
        private Color _borderColor = Color.FromArgb(23, 169, 254);

        #endregion

        #region Constructors

        /// <summary>
        /// 构建一个 PopupControlHost(可以展开显示)
        /// </summary>
        /// <param name="control">展开显示内容的控件,一般是自定义控件</param>
        public PopupControlHost(Control control)
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            AutoSize = false;
            Padding = Padding.Empty;
            Margin = Padding.Empty;
            CreateHost(control);
        }

        #endregion

        #region Properties
        /// <summary>
        /// 是否接受改变大小.
        /// </summary>
        public bool ChangeRegion
        {
            get { return _changeRegion; }
            set { _changeRegion = value; }
        }
        /// <summary>
        /// 展开时是否处于焦点状态.
        /// </summary>
        public bool OpenFocused
        {
            get { return _openFocused; }
            set { _openFocused = value; }
        }
        /// <summary>
        /// 是否接受Alt+F4关闭控件.
        /// </summary>
        public bool AcceptAlt
        {
            get { return _acceptAlt; }
            set { _acceptAlt = value; }
        }
        /// <summary>
        /// 是否接受重新设置大小.
        /// </summary>
        public bool CanResize
        {
            get { return _canResize; }
            set { _canResize = value; }
        }
        /// <summary>
        /// 获取或设置边框的颜色.
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// 判断快捷键是否有效.并进行处理.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (_acceptAlt && ((keyData & Keys.Alt) == Keys.Alt))
            {
                if ((keyData & Keys.F4) != Keys.F4)
                {
                    return false;
                }
                else
                {
                    Close();
                }
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 正在打开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOpening(CancelEventArgs e)
        {
            if (_popupControl.IsDisposed || _popupControl.Disposing)
            {
                e.Cancel = true;
                base.OnOpening(e);
                return;
            }
            _popupControl.RegionChanged += new EventHandler(PopupControlRegionChanged);
            UpdateRegion();
            base.OnOpening(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOpened(EventArgs e)
        {
            if (_openFocused)
            {
                _popupControl.Focus();
            }
            base.OnOpened(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
        {
            _popupControl.RegionChanged -= new EventHandler(PopupControlRegionChanged);
            base.OnClosing(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (_controlHost != null)
            {
                _controlHost.Size = new Size(
                    Width - Padding.Horizontal, Height - Padding.Vertical);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, 
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (!ProcessGrip(ref m))
            {
                base.WndProc(ref m);
            }
        }
        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!_changeRegion)
            {
                ControlPaint.DrawBorder(
                    e.Graphics,
                    ClientRectangle,
                    _borderColor,
                    ButtonBorderStyle.Solid);
            }
        }
        /// <summary>
        /// 更新区域
        /// </summary>
        protected void UpdateRegion()
        {
            if(!_changeRegion)
            {
                return;
            }

            if (base.Region != null)
            {
                base.Region.Dispose();
                base.Region = null;
            }
            if (_popupControl.Region != null)
            {
                base.Region = _popupControl.Region.Clone();
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">指定的父控件.</param>
        public void Show(Control control)
        {
            Show(control, control.ClientRectangle);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">指定的父控件.</param>
        /// <param name="center"></param>
        public void Show(Control control, bool center)
        {
            Show(control, control.ClientRectangle, center);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">指定的父控件.</param>
        /// <param name="rect">指定的工作区域</param>
        public void Show(Control control, Rectangle rect)
        {
            Show(control, rect, false);
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="control">指定的父控件.</param>
        /// <param name="rect"></param>
        /// <param name="center"></param>
        public void Show(Control control, Rectangle rect, bool center)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            SetOwnerItem(control);

            if (_canResize && !_changeRegion)
            {
                Padding = new Padding(3);
            }
            else if (!_changeRegion)
            {
                Padding = new Padding(1);
            }
            else
            {
                Padding = Padding.Empty;
            }

            int width = Padding.Horizontal;
            int height = Padding.Vertical;

            base.Size = new Size(
                   _popupControl.Width + width,
                   _popupControl.Height + height);

            _resizableTop = false;
            _resizableLeft = false;
            Point location = control.PointToScreen(
                new Point(rect.Left, rect.Bottom));
            Rectangle screen = Screen.FromControl(control).WorkingArea;
            //是否在中心.
            if (center)
            {
                if (location.X + (rect.Width + Size.Width) / 2 > screen.Right)
                {
                    location.X = screen.Right - Size.Width;
                    _resizableLeft = true;
                }
                else
                {
                    location.X = location.X - (Size.Width - rect.Width) / 2;
                }
            }
            else
            {
                if (location.X + Size.Width > (screen.Left + screen.Width))
                {
                    _resizableLeft = true;
                    location.X = (screen.Left + screen.Width) - Size.Width;
                }
            }

            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                _resizableTop = true;
                location.Y -= Size.Height + rect.Height;
            }
            //计算在屏幕上显示的坐标.
            location = control.PointToClient(location);
            //显示
            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 查找根OwerItem.
        /// </summary>
        /// <param name="control"></param>
        private void SetOwnerItem(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (control is PopupControlHost) //如果父控件是PopupControlHost
            {
                PopupControlHost popupControl = control as PopupControlHost;
                _ownerPopup = popupControl;
                _ownerPopup._childPopup = this;
                OwnerItem = popupControl.Items[0];
                return;
            }
            if (control.Parent != null)//如果父控件不是PopupControlHost
            {
                SetOwnerItem(control.Parent); //再向上层查找,直到父控件为PopupControlHost. 或者为null
            }
        }
        /// <summary>
        /// 创建宿主.
        /// </summary>
        /// <param name="control"></param>
        private void CreateHost(Control control)
        {
            if (control == null)
            {
                throw new ArgumentException("control");
            }

            _popupControl = control;
            _controlHost = new ToolStripControlHost(control, "popupControlHost");
            _controlHost.AutoSize = false;
            _controlHost.Padding = Padding.Empty;
            _controlHost.Margin = Padding.Empty;
            base.Size = new Size(
                control.Size.Width + Padding.Horizontal,
                control.Size.Height + Padding.Vertical);
            base.Items.Add(_controlHost);
        }
        /// <summary>
        /// 大小更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupControlRegionChanged(object sender, EventArgs e)
        {
            UpdateRegion();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand,
           Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool ProcessGrip(ref Message m)
        {
            if (_canResize && !_changeRegion)
            {
                switch (m.Msg)
                {
                    case NativeMethods.WM_NCHITTEST:
                        return OnNcHitTest(ref m);
                    case NativeMethods.WM_GETMINMAXINFO:
                        return OnGetMinMaxInfo(ref m);
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool OnGetMinMaxInfo(ref Message m)
        {
            Control hostedControl = _popupControl;
            if (hostedControl != null)
            {
                NativeMethods.MINMAXINFO minmax =
                    (NativeMethods.MINMAXINFO)Marshal.PtrToStructure(
                    m.LParam, typeof(NativeMethods.MINMAXINFO));

                if (hostedControl.MaximumSize.Width != 0)
                {
                    minmax.maxTrackSize.Width = hostedControl.MaximumSize.Width;
                }
                if (hostedControl.MaximumSize.Height != 0)
                {
                    minmax.maxTrackSize.Height = hostedControl.MaximumSize.Height;
                }

                minmax.minTrackSize = new Size(100, 100);
                if (hostedControl.MinimumSize.Width > minmax.minTrackSize.Width)
                {
                    minmax.minTrackSize.Width = 
                        hostedControl.MinimumSize.Width + Padding.Horizontal;
                }
                if (hostedControl.MinimumSize.Height > minmax.minTrackSize.Height)
                {
                    minmax.minTrackSize.Height = 
                        hostedControl.MinimumSize.Height + Padding.Vertical;
                }

                Marshal.StructureToPtr(minmax, m.LParam, false);
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private bool OnNcHitTest(ref Message m)
        {
            Point location = PointToClient(new Point(
                NativeMethods.LOWORD(m.LParam), NativeMethods.HIWORD(m.LParam)));
            Rectangle gripRect = Rectangle.Empty;
            if (_canResize && !_changeRegion)
            {
                if (_resizableLeft)
                {
                    if (_resizableTop)
                    {
                        gripRect = new Rectangle(0, 0, 6, 6);
                    }
                    else
                    {
                        gripRect = new Rectangle(
                            0,
                            Height - 6,
                            6,
                            6);
                    }
                }
                else
                {
                    if (_resizableTop)
                    {
                        gripRect = new Rectangle(Width - 6, 0, 6, 6);
                    }
                    else
                    {
                        gripRect = new Rectangle(
                            Width - 6,
                            Height - 6,
                            6,
                            6);
                    }
                }
            }

            if (gripRect.Contains(location))
            {
                if (_resizableLeft)
                {
                    if (_resizableTop)
                    {
                        m.Result = (IntPtr)NativeMethods.HTTOPLEFT;
                        return true;
                    }
                    else
                    {
                        m.Result = (IntPtr)NativeMethods.HTBOTTOMLEFT;
                        return true;
                    }
                }
                else
                {
                    if (_resizableTop)
                    {
                        m.Result = (IntPtr)NativeMethods.HTTOPRIGHT;
                        return true;
                    }
                    else
                    {
                        m.Result = (IntPtr)NativeMethods.HTBOTTOMRIGHT;
                        return true;
                    }
                }
            }
            else
            {
                Rectangle rectClient = ClientRectangle;
                if (location.X > rectClient.Right - 3 &&
                    location.X <= rectClient.Right &&
                    !_resizableLeft)
                {
                    m.Result = (IntPtr)NativeMethods.HTRIGHT;
                    return true;
                }
                else if (location.Y > rectClient.Bottom - 3 &&
                    location.Y <= rectClient.Bottom &&
                    !_resizableTop)
                {
                    m.Result = (IntPtr)NativeMethods.HTBOTTOM;
                    return true;
                }
                else if (location.X > -1 &&
                    location.X < 3 &&
                    _resizableLeft)
                {
                    m.Result = (IntPtr)NativeMethods.HTLEFT;
                    return true;
                }
                else if (location.Y > -1 &&
                    location.Y < 3 &&
                    _resizableTop)
                {
                    m.Result = (IntPtr)NativeMethods.HTTOP;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }

    #endregion

 
}