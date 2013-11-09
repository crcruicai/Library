#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/22 9:52:03
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

namespace CRC.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class ChatTreeView : ScrollableControl
    {
        #region 字段与变量
        /// <summary>
        /// 滚动条
        /// </summary>
        private ChatListVScroll chatVScroll;
        /// <summary>
        /// 鼠标的位置
        /// </summary>
        private Point m_ptMousePos;
        /// <summary>
        /// 鼠标按下的键
        /// </summary>
        private MouseButtons _MouseClickButton;



        #endregion

        #region 事件与委托

        #endregion


        #region 构造函数
        public ChatTreeView()
        {
            this.AutoScroll = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            //_Nodes = new ChatNodeCollection(this);
        }
        #endregion

        #region 属性


        private ChatNode  _SelectItem;
        /// <summary>
        /// 当前被选中的节点.
        /// </summary>
        [Browsable (false )]
        public ChatNode SelectItem
        {
            get { return _SelectItem; }
            set { _SelectItem = value; }
        }




        private ChatNodeCollection  _Items;
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 获取列表中所有列表项的集合
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChatNodeCollection Items
        {
            get
            { 
                return _Items;
            }
        }

        /// <summary>
        /// 获取或者设置滚动条背景色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("滚动条的背景颜色")]
        public Color ScrollBackColor
        {
            get { return chatVScroll.BackColor; }
            set { chatVScroll.BackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条滑块默认颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条滑块默认情况下的颜色")]
        public Color ScrollSliderDefaultColor
        {
            get { return chatVScroll.SliderDefaultColor; }
            set { chatVScroll.SliderDefaultColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条点下的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("滚动条滑块被点击或者鼠标移动到上面时候的颜色")]
        public Color ScrollSliderDownColor
        {
            get { return chatVScroll.SliderDownColor; }
            set { chatVScroll.SliderDownColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条箭头的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条箭头的背景颜色")]
        public Color ScrollArrowBackColor
        {
            get { return chatVScroll.ArrowBackColor; }
            set { chatVScroll.ArrowBackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条的箭头颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("滚动条箭头的颜色")]
        public Color ScrollArrowColor
        {
            get { return chatVScroll.ArrowColor; }
            set { chatVScroll.ArrowColor = value; }
        }

        private Color arrowColor;
        /// <summary>
        /// 获取或者设置列表项箭头的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("列表项上面的箭头的颜色")]
        public Color ArrowColor
        {
            get { return arrowColor; }
            set
            {
                if (arrowColor == value) return;
                arrowColor = value;
                this.Invalidate();
            }
        }

        private Color itemColor;
        /// <summary>
        /// 获取或者设置列表项背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表项的背景色")]
        public Color ItemColor
        {
            get { return itemColor; }
            set
            {
                if (itemColor == value) return;
                itemColor = value;
            }
        }

        private Color subItemColor;
        /// <summary>
        /// 获取或者设置子项的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表子项的背景色")]
        public Color SubItemColor
        {
            get { return subItemColor; }
            set
            {
                if (subItemColor == value) return;
                subItemColor = value;
            }
        }

        private Color itemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到列表项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("当鼠标移动到列表项上面的颜色")]
        public Color ItemMouseOnColor
        {
            get { return itemMouseOnColor; }
            set { itemMouseOnColor = value; }
        }

        private Color subItemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightBlue"), Category("ControlColor")]
        [Description("当鼠标移动到子项上面的颜色")]
        public Color SubItemMouseOnColor
        {
            get { return subItemMouseOnColor; }
            set { subItemMouseOnColor = value; }
        }

        private Color subItemSelectColor;
        /// <summary>
        /// 获取或者设置选中的子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Wheat"), Category("ControlColor")]
        [Description("当列表子项被选中时候的颜色")]
        public Color SubItemSelectColor
        {
            get { return subItemSelectColor; }
            set { subItemSelectColor = value; }
        }



        #endregion

        #region 公共函数

        #endregion

        #region 私有函数

        #endregion

        #region 绘制函数
        protected override void OnPaint(PaintEventArgs e)
        {
            int height = 25;//定义每个项的高度.

            Graphics g = e.Graphics;
            g.TranslateTransform(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
            //g.TranslateTransform(0, -chatVScroll.Value);        //根据滚动条的值设置坐标偏移
            Rectangle rectItem = new Rectangle(0, 1, this.Width, height);      //列表项区域
            Rectangle rectSubItem = new Rectangle(0, height +1, this.Width, 0);   //子项区域
            SolidBrush sb = new SolidBrush(this.itemColor);

            try
            {
                foreach (var item in Items)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }



            base.OnPaint(e);
        }

        /// <summary>
        /// 绘制子项.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="node"></param>
        /// <param name="rect"></param>
        /// <param name="sb"></param>
        public virtual void DrawItem(Graphics g,ChatNode node,ref Rectangle rect ,SolidBrush sb)
        {
            //设置子项的颜色.
            if (node.Equals(SelectItem))sb.Color = this.subItemSelectColor ;
            else if(node.Equals(SelectItem) )sb.Color =this.subItemMouseOnColor ;
            else sb.Color =this.subItemColor ;
            
            //设置文本绘制的方式.
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Near;
            sf.FormatFlags = StringFormatFlags.FitBlackBox;


        }

        /// <summary>
        /// 绘制展开或关闭的图标.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sb"></param>
        public virtual void DrawIcon(Graphics g, SolidBrush sb, Rectangle rectItem,bool isExpand)
        {
            if (isExpand)
            {
                sb.Color = this.arrowColor;
                g.FillPolygon(sb, new Point[] { 
                        new Point(2, rectItem.Top + 11), 
                        new Point(12, rectItem.Top + 11), 
                        new Point(7, rectItem.Top + 16) });
            }
            else
            {
                //绘制 不展开的三角形.
                sb.Color = this.arrowColor;
                g.FillPolygon(sb, new Point[] { 
                        new Point(5, rectItem.Top + 8), 
                        new Point(5, rectItem.Top + 18), 
                        new Point(10, rectItem.Top + 13) });
            }
        }

        #endregion

    }
}
