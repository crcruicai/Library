/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 23:23:02
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QQRobot
{
    public partial class FrmMain : Form
    {
        #region 字段与变量
        /// <summary>
        /// 自动停靠控件
        /// </summary>
        CRC.AutoDockManage _AutoDock;
        #endregion

        #region 构造函数
        public FrmMain()
        {
            InitializeComponent();

            _AutoDock = new CRC.AutoDockManage(this.components);
            _AutoDock.IsOpen = true;
            

        }
        #endregion

        #region 属性

        #endregion

        #region 公共函数

        #endregion

        #region 私有函数

        #endregion


        
    }
}
