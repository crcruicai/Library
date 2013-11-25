#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/21 10:44:03
 * 描述说明：自定义层次的TreeView.只绘制根节点.
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
using CRC.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace CRC.Controls
{
    /// <summary>
    /// 自定义层次的TreeView.只绘制根节点.
    /// </summary>
    public class LayerTreeView : TreeView
    {
        #region 字段与变量.
        /// <summary>
        /// 渐变填充的颜色
        /// </summary>
        private Color _startColor = Color.FromArgb(195, 222, 250); 
        /// <summary>
        /// 渐变填充的颜色
        /// </summary>
        private Color _endColor = Color.FromArgb(147, 194, 241);   
        /// <summary>
        /// 字体
        /// </summary>
        private Font _defaultFont = new Font("宋体", 13f);         
        /// <summary>
        /// 减号图标
        /// </summary>
        private readonly Image _minusImage = Resources.minus;

        /// <summary>
        /// 加号图标
        /// </summary>
        private readonly Image _plusImage = Resources.plus;

        #endregion

        #region 属性
        /// <summary>
        /// 项渐变填充起始颜色
        /// </summary>
        public Color StartColor
        {
            get { return _startColor; }
            set { _startColor = value; }
        }

        /// <summary>
        /// 项渐变填充结束颜色
        /// </summary>
        public Color EndColor
        {
            get { return _endColor; }
            set { _endColor = value; }
        }

        #endregion

        #region 构造函数.
        /// <summary>
        /// 自定义层次的TreeView
        /// </summary>
        public LayerTreeView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            SetDefaultProperty();

            DrawNode += CustomDrawNode;
        }

        #endregion

        #region 私有函数
        /// <summary>
        /// 设置默认属性.
        /// </summary>
        private void SetDefaultProperty()
        {
            FullRowSelect = true;
            ShowLines = false;
            CheckBoxes = true;
            Indent = 0;
            Font = _defaultFont;
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
        }

        /// <summary>
        /// 自定义绘制根菜单的实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.DrawDefault = true;
                return;
            }

            LinearGradientMode mode = LinearGradientMode.Vertical;
            Rectangle rect = e.Bounds;

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, _startColor, _endColor, mode))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            Font nodeFont = _defaultFont;

            //绘制加减号
            e.Graphics.DrawImage((e.Node.IsExpanded ? _minusImage : _plusImage), e.Bounds.Location.X + 5, e.Bounds.Location.Y + 3);

            //绘制文字
            e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black, (e.Bounds.Location.X + 20), (e.Bounds.Location.Y));
        }
        #endregion
    }
}
