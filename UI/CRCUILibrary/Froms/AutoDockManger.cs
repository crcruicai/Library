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
        private Form _Form;

        /// <summary>
        /// 
        /// </summary>
        private bool _IsOrg = false;
        /// <summary>
        /// 窗体的所在区域.
        /// </summary>
        private Rectangle _LastBoard;
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
        private Timer _Timer;
        /// <summary>
        /// 状态.
        /// </summary>
        private int _Status = 2;
        private bool _IsOpen = false;
        /// <summary>
        /// 描述如何靠边锚定.
        /// </summary>
        internal AnchorStyles _DockSide = AnchorStyles.None;

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
                return _Form;
            }
            set
            {
                _Form = value;
                if (_Form != null)
                {
                    _Form.LocationChanged += new EventHandler(_form_LocationChanged);
                    _Form.SizeChanged += new EventHandler(_form_SizeChanged);
                    _Form.TopMost = true;
                }
            }
        }

        [Description("是否启动自动隐藏功能.")]
        [DefaultValue(false)]
        public bool IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                if (value == true)
                {
                    _Timer.Start();//启动定时器.
                }
                else
                {
                    _Timer.Stop();//关闭定时器.
                }
                _IsOpen = value;
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 初始化.
        /// </summary>
        private void InitializeComponent()
        {
            _Timer = new Timer();
            _Timer.Tick += this.CheckPosTimer_Tick;

        }

        private void CheckPosTimer_Tick(object sender, EventArgs e)
        {
            if (DesignMode)//设计模式.
            {
                return;
            }

            if (_Form == null || _IsOrg == false)
            {
                return;
            }

            if (_Form.Bounds.Contains(Cursor.Position))//鼠标是否在窗体上面.
            {
                switch (_DockSide)
                {
                    case AnchorStyles.Top://顶部停靠
                        if (_Status == DOCKING)
                            _Form.Location = new Point(_Form.Location.X, 0);
                        break;
                    case AnchorStyles.Right://右边停靠
                        if (_Status == DOCKING)
                            _Form.Location = new Point(Screen.PrimaryScreen.Bounds.Width - _Form.Width, 1);
                        break;
                    case AnchorStyles.Left://左边停靠.
                        if (_Status == DOCKING)
                            _Form.Location = new Point(0, 1);
                        break;
                }
            }
            else
            {
                //鼠标不在窗体上面.
                switch (_DockSide)
                {
                    case AnchorStyles.Top://
                        _Form.Location = new Point(_Form.Location.X, (_Form.Height - 4) * (-1));
                        break;
                    case AnchorStyles.Right:
                        _Form.Size = new Size(_Form.Width, Screen.PrimaryScreen.WorkingArea.Height);
                        _Form.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 4, 1);
                        break;
                    case AnchorStyles.Left:
                        _Form.Size = new Size(_Form.Width, Screen.PrimaryScreen.WorkingArea.Height);
                        _Form.Location = new Point((-1) * (_Form.Width - 4), 1);
                        break;
                    case AnchorStyles.None:
                        if (_IsOrg == true && _Status == OFF)
                        {
                            if (_Form.Bounds.Width != _LastBoard.Width || _Form.Bounds.Height != _LastBoard.Height)
                            {
                                _Form.Size = new Size(_LastBoard.Width, _LastBoard.Height);
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
            if (_Form.Top <= 0)
            {
                _DockSide = AnchorStyles.Top;
                if (_Form.Bounds.Contains(Cursor.Position))
                    _Status = PRE_DOCKING;
                else
                    _Status = DOCKING;
            }
            else if (_Form.Left <= 0)
            {
                _DockSide = AnchorStyles.Left;
                if (_Form.Bounds.Contains(Cursor.Position))
                    _Status = PRE_DOCKING;
                else
                    _Status = DOCKING;
            }
            else if (_Form.Left >= Screen.PrimaryScreen.Bounds.Width - _Form.Width)
            {
                _DockSide = AnchorStyles.Right;
                if (_Form.Bounds.Contains(Cursor.Position))
                    _Status = PRE_DOCKING;
                else
                    _Status = DOCKING;
            }
            else
            {
                //窗体没有 停靠在屏幕侧边
                _DockSide = AnchorStyles.None;
                _Status = OFF;
            }
        }

        //窗体在屏幕的位置改变时.
        private void _form_LocationChanged(object sender, EventArgs e)
        {
            GetDockSide();
            if (_IsOrg == false)
            {
                //保存窗体所在的区域.
                _LastBoard = _Form.Bounds;
                _IsOrg = true;
            }
        }

        //窗体大小改变时
        private void _form_SizeChanged(object sender, EventArgs e)
        {
            if (_IsOrg == true && _Status == OFF)
            {
                _LastBoard = _Form.Bounds;
            }
        }

        #endregion
    }
}
