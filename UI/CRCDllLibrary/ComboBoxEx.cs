using System.Text;
using System.Globalization;
using System.ComponentModel;
using System.Collections;
using Microsoft.International.Converters.PinYinConverter;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace CRC.Controls
{
    //功能：输入字母，自动筛选Items中对应拼音首字母的项目，如：bk，筛选“博客”，“边框”等。另外支持英文字母开头项目，支持自定义项目颜色。

    //拼音方案直接引用微软自家的类库，详见：http://www.microsoft.com/zh-cn/download/details.aspx?id=15251
    //类库支持获取简体中文字符的常用属性比如拼音，多音字，同音字，笔画数。
    //请下载并在项目中引用ChnCharInfo.dll

    public class ComboBoxEx : System.Windows.Forms.ComboBox
    {
        /// <summary>
        /// 解决ComboBox的一个Bug，详见：
        /// https://connect.microsoft.com/VisualStudio/feedback/details/721868/combobox-throws-argumentoutofrangeexception-after-clearing-while-in-droppeddown-state
        /// </summary>
        public override int SelectedIndex
        {
            get
            {
                if (Items.Count > 0)
                    return base.SelectedIndex;
                else
                    return -1;
            }
            set
            {
                if (Items.Count > 0)
                {
                    base.SelectedIndex = value;
                }
            }
        }

        public ComboBoxEx()
        {
            Enter += new System.EventHandler(ComboBoxEx_Enter);
            Leave += new System.EventHandler(ComboBoxEx_Leave);
            TextUpdate += new System.EventHandler(ComboBoxEx_TextUpdate);

            DrawMode = DrawMode.OwnerDrawVariable;
            DrawItem += new DrawItemEventHandler(ComboBoxEx_DrawItem);
        }

        #region 自定义绘制每项的颜色
        private void ComboBoxEx_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (e.Index != -1)
            {
                cb.DrawMode = DrawMode.OwnerDrawVariable;
                // Draw the background of the ListBox control for each item.
                e.DrawBackground();
                // Define the default color of the brush as black.
                Brush myBrush = Brushes.Black;

                // Determine the color of the brush to draw each item based on the index of the item to draw.
                //if (e.Index % 2 == 0)
                //{
                //    myBrush = Brushes.DarkSlateGray;
                //}
                // Draw background color for each item.
                //e.Graphics.FillRectangle(myBrush, e.Bounds);

                // Draw the current item text based on the current Font and the custom brush settings.
                e.Graphics.DrawString(cb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
            }
        }
        #endregion

        #region 支持拼音首字母查询

        private ArrayList lstItems = new ArrayList();

        /// <summary>
        /// 汉字转拼音首字母
        /// </summary>
        /// <param name="scrChar"></param>
        /// <returns></returns>
        public List<string> Chinese2Pinyin(char scrChar)
        {
            try
            {
                ChineseChar cnChar = new ChineseChar(scrChar);
                List<string> lstStr = new List<string>(cnChar.PinyinCount);
                for (int i = 0; i < cnChar.PinyinCount; i++)
                {
                    lstStr.Add(cnChar.Pinyins[i]);
                }
                return lstStr;
            }
            catch
            {
                return new List<string> { scrChar.ToString() };
            }
        }

        private void ComboBoxEx_Enter(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            lstItems.Clear();
            lstItems.AddRange(cb.Items);
        }

        private void ComboBoxEx_Leave(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            object obj = null;

            if (cb.Items.Count != 0)
                obj = cb.SelectedItem;

            cb.DataSource = null;
            cb.Items.Clear();
            //cb.Items.AddRange(this.lstTeam.ToArray());
            cb.DataSource = (ArrayList)lstItems.Clone();
            cb.SelectedItem = obj;
        }

        private void ComboBoxEx_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = (ComboBox)sender;
                cb.DataSource = null;

                if (cb.Items.Count == 0)
                {
                    cb.SelectedIndex = -1;
                    cb.SelectedItem = null;
                }

                if (string.IsNullOrEmpty(cb.Text))
                {
                    cb.Items.Clear();
                    cb.Items.AddRange(lstItems.ToArray());
                }
                else
                {
                    ArrayList lstCurItems = new ArrayList();

                    int count = cb.Text.Length - 1;
                    if (0 == count)
                        lstCurItems = lstItems;
                    else
                        lstCurItems.AddRange(cb.Items);

                    cb.Items.Clear();
                    foreach (object obj in lstCurItems)
                    {
                        if (count < obj.ToString().Length)
                        {
                            foreach (string item in Chinese2Pinyin(obj.ToString()[count]))
                            {
                                if (item.ToLower()[0] == cb.Text.ToLower()[count])
                                {
                                    cb.Items.Add(obj);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (cb.Items.Count == 0)
                {
                    cb.SelectedIndex = -1;
                    cb.SelectedItem = null;
                    cb.Text = null;
                }
                cb.SelectionStart = cb.Text.Length;
                cb.DropDownHeight = 140;
                cb.DroppedDown = true;
                cb.Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}