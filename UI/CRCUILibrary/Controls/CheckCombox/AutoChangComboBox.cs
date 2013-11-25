using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CRC.Controls
{
    /// <summary>
    /// 指示如何刷选子项的委托.
    /// <para>如果souce包含在dest中,返回true.不在返回false</para>
    /// </summary>
    /// <param name="souce">源字符串</param>
    /// <param name="dest">目标字符串.</param>
    /// <returns></returns>
    public delegate bool FitlerItem(string souce,string dest);

    //更改组件的图标需要引用Drawing
    
    //[ToolboxBitmap("C:\\SearchButton.ico")]
    /// <summary>
    /// 自动根据文本进行刷选的ComboBox
    /// <para>可以通过修改委托方法,对自定义对子项进行刷选.</para>
    /// <para>需要此功能时,请修改属性Fitler</para>
    /// </summary>
    public class AutoChangComboBox : ComboBox
    {
        public AutoChangComboBox()
        {

        }

        private FitlerItem  _Fitler;
        /// <summary>
        /// 指示如何刷新子项的.
        /// </summary>
        public FitlerItem  Fitler
        {
            get
            {
                if (_Fitler != null)
                {
                    _Fitler = new FitlerItem(GetFitler);
                }
                return _Fitler;
            }
            set
            {
                _Fitler = value;
            }
        }


        //定义数组
        private ArrayList m_list = new ArrayList();

        //当控件成为窗体的活动控件时
        protected override void OnEnter(EventArgs e)
        {
            //数组清空
            m_list.Clear();
            //将ComboBox中的项追加到数组集合
            m_list.AddRange(this.Items);
            //触发父类事件
            base.OnEnter(e);
        }

        //当控件不再是活动控件时
        protected override void OnLeave(EventArgs e)
        {
            //ComboBox的集合清空
            this.Items.Clear();
            //将数组中的项追加到ComboBox
            this.Items.AddRange(m_list.ToArray());
            //触发父类事件
            base.OnLeave(e);
        }

        //当文本更改时
        protected override void OnTextUpdate(EventArgs e)
        {
            //如何ComboBox的集合中有值就将ComboBox中的项删除
            while (this.Items.Count > 0)
            {
                this.Items.RemoveAt(0);
            }
            foreach (object o in this.m_list)
            {
                if(Fitler(this.Text ,o.ToString ()))
                {
                    this.Items.Add(o);
                } 
            }
            if (this.Items.Count == 0)
                this.Items.Add(this.Text);
            this.DroppedDown = true;
            this.Cursor = Cursors.Default;
            base.OnTextUpdate(e);
        }


        /// <summary>
        /// 默认刷新子项.
        /// </summary>
        /// <param name="souce"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        private bool GetFitler(string souce, string dest)
        {
            return GetChineseSpell(dest.ToLower()).Contains(souce.ToLower());
        }


        /// <summary>
        /// 传入字符串获得各个汉字的首字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        static private string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        //
        /// <summary>
        /// 传入汉字获取首字母的方法(一个字符)
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        static private string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else
                return cnChar;
        }
    }
}
