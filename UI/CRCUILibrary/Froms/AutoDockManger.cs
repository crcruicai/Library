/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 11:56:57
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace CRC
{
    /// <summary>
    /// 窗体自动靠边隐藏组件.
    /// </summary>
    [Description("窗体自动靠边隐藏组件")]
    public partial class AutoDockManage : Component
    {
        #region 字段与变量
        /// <summary>
        /// 指定的窗体.
        /// </summary>
        private Form _form;

        /// <summary>
        /// 
        /// </summary>
        private bool IsOrg = false;
        /// <summary>
        /// 窗体的所在区域.
        /// </summary>
        private Rectangle lastBoard;
        /// <summary>
        /// 正在停靠的状态.
        /// </summary>
        private const int DOCKING = 0;
        /// <summary>
        /// 准备停靠的状态.
        /// </summary>
        private const int PRE_DOCKING = 1;
        /// <summary>
        /// 关闭停靠.
        /// </summary>
        private const int OFF = 2;
        private Timer timer;
        /// <summary>
        /// 状态.
        /// </summary>
        private int status = 2;
        private bool isopen = false;
        /// <summary>
        /// 描述如何靠边锚定.
        /// </summary>
        internal AnchorStyles dockSide = AnchorStyles.None;

        #endregion

        #region 构造函数.
        /// <summary>
        /// 窗体自动靠边隐藏组件.
        /// </summary>
        public AutoDockManage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体自动靠边隐藏组件.
        /// </summary>
        public AutoDockManage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 需要自动隐藏的窗体.
        /// </summary>
        [Description("用于控制要自动Dock的窗体")]
        public Form DockForm
        {
            get
            {
                return _form;
            }
            set
            {
                _form = value;
                if (_form != null)
                {
                    _form.LocationChanged += new EventHandler(_form_LocationChanged);
                    _form.SizeChanged += new EventHandler(_form_SizeChanged);
                    _form.TopMost = true;
                }
            }
        }

        [Description("是否启动自动隐藏功能.")]
        [DefaultValue(false)]
        public bool IsOpen
        {
            get
            {
                return isopen;
            }
            set
            {
                if (value == true)
                {
                    timer.Start();//启动定时器.
                }
                else
                {
                    timer.Stop();//关闭定时器.
                }
                isopen = value;
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 初始化.
        /// </summary>
        private void InitializeComponent()
        {
            timer = new Timer();
            timer.Tick += this.CheckPosTimer_Tick;

        }

        private void CheckPosTimer_Tick(object sender, EventArgs e)
        {
            if (DesignMode)//设计模式.
            {
                return;
            }

            if (_form == null || IsOrg == false)
            {
                return;
            }

            if (_form.Bounds.Contains(Cursor.Position))//鼠标是否在窗体上面.
            {
                switch (dockSide)
                {
                    case AnchorStyles.Top://顶部停靠
                        if (status == DOCKING)
                            _form.Location = new Point(_form.Location.X, 0);
                        break;
                    case AnchorStyles.Right://右边停靠
                        if (status == DOCKING)
                            _form.Location = new Point(Screen.PrimaryScreen.Bounds.Width - _form.Width, 1);
                        break;
                    case AnchorStyles.Left://左边停靠.
                        if (status == DOCKING)
                            _form.Location = new Point(0, 1);
                        break;
                }
            }
            else
            {
                //鼠标不在窗体上面.
                switch (dockSide)
                {
                    case AnchorStyles.Top://
                        _form.Location = new Point(_form.Location.X, (_form.Height - 4) * (-1));
                        break;
                    case AnchorStyles.Right:
                        _form.Size = new Size(_form.Width, Screen.PrimaryScreen.WorkingArea.Height);
                        _form.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 4, 1);
                        break;
                    case AnchorStyles.Left:
                        _form.Size = new Size(_form.Width, Screen.PrimaryScreen.WorkingArea.Height);
                        _form.Location = new Point((-1) * (_form.Width - 4), 1);
                        break;
                    case AnchorStyles.None:
                        if (IsOrg == true && status == OFF)
                        {
                            if (_form.Bounds.Width != lastBoard.Width || _form.Bounds.Height != lastBoard.Height)
                            {
                                _form.Size = new Size(lastBoard.Width, lastBoard.Height);
                            }
                        }
                        break;
                }
            }
        }



        /// <summary>
        /// 计算 窗体应当如何停靠.
        /// </summary>
        private void GetDockSide()
        {
            if (_form.Top <= 0)
            {
                dockSide = AnchorStyles.Top;
                if (_form.Bounds.Contains(Cursor.Position))
                    status = PRE_DOCKING;
                else
                    status = DOCKING;
            }
            else if (_form.Left <= 0)
            {
                dockSide = AnchorStyles.Left;
                if (_form.Bounds.Contains(Cursor.Position))
                    status = PRE_DOCKING;
                else
                    status = DOCKING;
            }
            else if (_form.Left >= Screen.PrimaryScreen.Bounds.Width - _form.Width)
            {
                dockSide = AnchorStyles.Right;
                if (_form.Bounds.Contains(Cursor.Position))
                    status = PRE_DOCKING;
                else
                    status = DOCKING;
            }
            else
            {
                //窗体没有 停靠在屏幕侧边
                dockSide = AnchorStyles.None;
                status = OFF;
            }
        }

        //窗体在屏幕的位置改变时.
        private void _form_LocationChanged(object sender, EventArgs e)
        {
            GetDockSide();
            if (IsOrg == false)
            {
                //保存窗体所在的区域.
                lastBoard = _form.Bounds;
                IsOrg = true;
            }
        }

        //窗体大小改变时
        private void _form_SizeChanged(object sender, EventArgs e)
        {
            if (IsOrg == true && status == OFF)
            {
                lastBoard = _form.Bounds;
            }
        }

        #endregion
    }
}
