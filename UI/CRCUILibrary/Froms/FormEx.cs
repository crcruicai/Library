﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CRC.Froms
{
    /// <summary>
    /// 扩展From的功能.支持全屏.是否可以连接到桌面.
    /// </summary>
    public partial class FormEx : Form
    {
        #region 字段与变量
        //Member variables
        private bool _EnableCloseButton;
        private bool _FullScreen;
        private bool _Sizable;
        private Boolean _Movable;
        private bool _DesktopAttached;
        private IntPtr _PreviousParent;

        /*
         * Constants
         */
        //Paramaters to EnableMenuItem Win32 function
        private const int SC_CLOSE = 0xF060; //The Close Box identifier
        private const int MF_ENABLED = 0x0;  //Enabled Value
        private const int MF_DISABLED = 0x2; //Disabled Value

        //Windows Messages
        private const int WM_NCPAINT = 0x85;//Paint non client area message
        private const int WM_PAINT = 0xF;//Paint client area message
        private const int WM_SIZE = 0x5;//Resize the form message
        private const int WM_IME_NOTIFY = 0x282;//Notify IME Window message
        private const int WM_SETFOCUS = 0x0007;//Form.Activate message
        private const int WM_SYSCOMMAND = 0x112; //SysCommand message
        private const int WM_SIZING = 0x214; //Resize Message
        private const int WM_NCLBUTTONDOWN = 0xA1; //Left Mouse Button on Non-Client Area is Down
        private const int WM_NCACTIVATE = 0x86; //Message sent to the window when it's activated or deactivated

        //WM_SIZING WParams that stands for Hit Tests in the direction the form is resizing
        private const int HHT_ONHEADER = 0x0002;
        private const int HT_TOPLEFT = 0XD;
        private const int HT_TOP = 0XC;
        private const int HT_TOPRIGHT = 0XE;
        private const int HT_RIGHT = 0XB;
        private const int HT_BOTTOMRIGHT = 0X11;
        private const int HT_BOTTOM = 0XF;
        private const int HT_BOTTOMLEFT = 0X10;
        private const int HT_LEFT = 0XA;

        //WM_SYSCOMMAND WParams that stands for which operation is beeing done
        private const int SC_DRAGMOVE = 0xF012; //SysCommand Dragmove parameter
        private const int SC_MOVE = 0xF010; //SysCommand Move with keyboard command

        //Remember Window State before beeing set to FullScreen
        private FormWindowState _FormWindowState;
        private Point _Location;
        private bool _TopMost;
        private Size _Size;
        private bool _MaxBox;
        private Graphics _GraphicsFrameArea = null;

        #endregion

        #region API
        /// <summary>
        /// Occurs when the frame area (including Title Bar, excluding the client area) is redrawn."
        /// </summary>
        [Description("Occurs when The frame area (including Title Bar, excluding the client area) needs repainting."), Category("Appearance")]
        public event PaintEventHandler PaintFrameArea;

        //GetSystemMenu Win32 API Declaration (The Window Title bar a is SystemMenu)
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        //EnableMenuItem Win32 API Declaration - Set the enabled values of the title bar (from GetSystemMenu) items
        [DllImport("user32.dll")]
        private static extern int EnableMenuItem(IntPtr hMenu, int wIDEnable, int wValue);

        //Get Desktop Window Handle
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        //Set Parent Window - We will use this to set ProgMan as the Window Parent
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //Find any Window in the OS. We will look for the parent of where the desktop is (ProgMan)
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        //Get the device component of the window to allow drawing on the title bar and frame
        [DllImport("User32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        //Releases the Device Component after it's been used
        [DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        #endregion

        #region 构造函数
        public FormEx()
        {
            _Movable = true;
            _FullScreen = false;
            _EnableCloseButton = true;
            _Sizable = true;
            _DesktopAttached = false;
            InitializeComponent();
            SaveFormState();
        }

        #endregion

        #region 属性
        /// <summary>
        /// <para>获取或设置窗体的关闭按钮是否可以使用(false:禁用(但不会隐藏))</para>
        /// Gets or Sets the value indicating wether the Close Button on the window title bar is enabled.
        /// </summary>
        [Browsable(true), DefaultValue(true), Category("Window Style")]
        [Description("Determines wether the Close Button on the window title bar is enabled.")]
        public bool CloseButton
        {
            get
            {
                return _EnableCloseButton;
            }
            set
            {
                _EnableCloseButton = value;
                CloseBoxEnable(_EnableCloseButton);
            }
        }

        /// <summary>
        /// <para>获取或设置窗体是否处于全屏模式.</para>
        /// Gets or Sets the value indicating wether the Form is in Full Screen mode
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Layout")]
        [Description("Determines wether the the Form is in Full Screen mode.")]
        public bool FullScreen
        {
            get
            {
                return _FullScreen;
            }
            set
            {
                if (!DesignMode)
                {
                    if (value && _FullScreen != value)
                        SetFullScreen(true);

                    if (!value && _FullScreen != value)
                        SetFullScreen(false);
                }
                else
                    _FullScreen = value;
            }
        }

        /// <summary>
        /// <para>获取或设置窗体是否可以移动</para>
        /// Gets or Sets the value indicating wether the Form is Movable.
        /// </summary>
        [Browsable(true), DefaultValue(true), Category("Layout")]
        [Description("Determines wether the Form is movable.")]
        public bool Movable
        {
            get
            {
                return _Movable;
            }
            set
            {
                _Movable = value;
            }
        }

        /// <summary>
        /// <para>获取或设置一个值，指示阉的形式是相当大的。</para>
        /// Gets or Sets the value indicating wether the Form is Sizable.
        /// </summary>
        [Browsable(true), DefaultValue(true), Category("Layout")]
        [Description("Determines wether the Form is sizable.")]
        public bool Sizable
        {
            get
            {
                return _Sizable;
            }
            set
            {
                _Sizable = value;
            }
        }

        /// <summary> 
        /// <para>获取或设置一个值，指示窗体的形式是连接到桌面。</para>
        /// Gets or Sets the value indicating wether the Form is attached to the Desktop.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Layout")]
        [Description("Determines wether the Form is attached to the Desktop.")]
        public bool DesktopAttached
        {
            get
            {
                return _DesktopAttached;
            }
            set
            {
                _DesktopAttached = value;
                this.MinimizeBox = !value;

                if (value)
                    _PreviousParent = SetParent(this.Handle, FindWindow("Progman", null));
                else
                    SetParent(this.Handle, _PreviousParent);
            }
        }

        #endregion

        #region 私有函数
        /// <summary>
        /// 保存窗体的状态.
        /// </summary>
        private void SaveFormState()
        {
            _FormWindowState = this.WindowState;
            _Location = this.Location;
            _TopMost = this.TopMost;
            _Size = this.Size;
            _MaxBox = this.MaximizeBox;
        }

        /// <summary>
        /// 恢复窗体的状态.
        /// </summary>
        private void RecallFormState()
        {
            this.WindowState = this._FormWindowState;
            this.TopMost = this._TopMost;
            this.Size = this._Size;
            this.MaximizeBox = this._MaxBox;
            this.Location = this._Location;
        }
        /// <summary>
        /// 设置获取取消窗体全屏.
        /// </summary>
        /// <param name="fullscreen">true:全屏.flase 取消全屏</param>
        private void SetFullScreen(bool fullscreen)
        {
            _FullScreen = fullscreen;

            if (fullscreen)
            {
                SaveFormState();
                this.MaximizeBox = false;
                this.WindowState = FormWindowState.Normal;
                this.Location = new Point(0, 0);
                this.TopMost = true;
                Screen currentScreen = Screen.FromHandle(this.Handle);
                this.Size = new System.Drawing.Size(currentScreen.Bounds.Width, currentScreen.Bounds.Height);
            }
            else
                RecallFormState();
        }


        //
        /// <summary>
        /// 调用API 显示或取消 关闭按钮.
        /// Calls Win32 API to disable or enable the close Box.
        /// </summary>
        /// <param name="enabled"></param>
        private void CloseBoxEnable(bool enabled)
        {
            IntPtr thisSystemMenu = GetSystemMenu(this.Handle, false);

            if (enabled == true)
                EnableMenuItem(thisSystemMenu, SC_CLOSE, MF_ENABLED);
            else
                EnableMenuItem(thisSystemMenu, SC_CLOSE, MF_DISABLED);
        }


        #endregion

        #region 重载函数
        protected override void WndProc(ref Message m)
        {
            // Prevents moving or resizing through the task bar
            if ((m.Msg == WM_SYSCOMMAND && (m.WParam == new IntPtr(SC_DRAGMOVE) || m.WParam == new IntPtr(SC_MOVE))))
            {
                if (_FullScreen || !_Movable)
                    return;
            }

            // Preventes Resizing from dragging the borders
            if (m.Msg == WM_SIZING || (m.Msg == WM_NCLBUTTONDOWN && (m.WParam == new IntPtr(HT_TOPLEFT) || m.WParam == new IntPtr(HT_TOP)
                || m.WParam == new IntPtr(HT_TOPRIGHT) || m.WParam == new IntPtr(HT_RIGHT) || m.WParam == new IntPtr(HT_BOTTOMRIGHT)
                || m.WParam == new IntPtr(HT_BOTTOM) || m.WParam == new IntPtr(HT_BOTTOMLEFT) || m.WParam == new IntPtr(HT_LEFT))))
            {
                if (_FullScreen || !_Sizable || !_Movable)
                    return;
            }

            base.WndProc(ref m);

            // Handles painting of the Non Client Area
            if (m.Msg == WM_NCPAINT || m.Msg == WM_IME_NOTIFY || m.Msg == WM_SIZE || m.Msg == WM_NCACTIVATE)
            {
                // To avoid unnecessary graphics recreation and thus improving performance
                if (_GraphicsFrameArea == null || m.Msg == WM_SIZE)
                {
                    ReleaseDC(this.Handle, m_WndHdc);
                    m_WndHdc = GetWindowDC(this.Handle);
                    _GraphicsFrameArea = Graphics.FromHdc(m_WndHdc);

                    Rectangle clientRecToScreen = new Rectangle(this.PointToScreen(new Point(this.ClientRectangle.X, this.ClientRectangle.Y)), new System.Drawing.Size(this.ClientRectangle.Width, this.ClientRectangle.Height));
                    Rectangle clientRectangle = new Rectangle(clientRecToScreen.X - this.Location.X, clientRecToScreen.Y - this.Location.Y, clientRecToScreen.Width, clientRecToScreen.Height);

                    _GraphicsFrameArea.ExcludeClip(clientRectangle);
                }

                RectangleF recF = _GraphicsFrameArea.VisibleClipBounds;

                PaintEventArgs pea = new PaintEventArgs(_GraphicsFrameArea, new Rectangle((int)recF.X, (int)recF.Y, (int)recF.Width, (int)recF.Height));
                OnPaintFrameArea(pea);
                CloseBoxEnable(_EnableCloseButton);
                this.Refresh();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnPaintFrameArea(PaintEventArgs e)
        {
            PaintEventHandler handler = PaintFrameArea;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private IntPtr m_WndHdc;
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            PaintFrameArea = null;
            ReleaseDC(this.Handle, m_WndHdc);
        }

        //Overrides attempts to move the form by code when in Full Screen mode.
        protected override void OnLocationChanged(EventArgs e)
        {
            if (_FullScreen)
            {
                this.Location = new Point(0, 0);
                return;
            }

            base.OnLocationChanged(e);
        }

        //Allows changes using the desginer to be reflected immediately
        protected override void OnChangeUICues(UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
            CloseBoxEnable(_EnableCloseButton);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.CloseButton || e.CloseReason == CloseReason.TaskManagerClosing || e.CloseReason == CloseReason.FormOwnerClosing || e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.WindowsShutDown)
                base.OnFormClosing(e);
            else
                e.Cancel = true;
        }

        #endregion

        #region  公有函数
        /// <summary>
        /// Get if Key is Up or Down
        /// </summary>
        /// <param name="key">The keyboard key to evaluate</param>
        /// <returns>If the key is Up or Down</returns>
        public KeyState GetKeyState(Keys key)
        {
            return KeyStateCheck.GetKeyState(key);
        }

        /// <summary>
        /// // Get if key is toggled or untgled (useful to detect if capslock or nunlock is on)
        /// </summary>
        /// <param name="key">The keyboard key to evaluate</param>
        /// <returns>If the key is toggled or untoggled</returns>
        public KeyValue GetKeyValue(Keys key)
        {
            return KeyStateCheck.GetToggled(key);
        }

        #endregion

        #region 示例代码.
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    FormEx ex = new FormEx();
        //    //ex.CloseButton = false;
        //    //ex.DesktopAttached = true;
        //    ex.FullScreen = true;
        //    Application.Run(ex);
        //}

        #endregion
    }
}
