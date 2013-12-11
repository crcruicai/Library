/*********************************************************
* 开发人员：TopC
* 创建时间：2013/12/3 9:14:32
* 描述说明：
*
* 更改历史：
*
* *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Systems
{
    /// <summary>
    /// Json 格式化器.
    /// </summary>
    public class JsonParson
    {
        #region 字段与变量
        /// <summary>
        /// Json的关键词 表
        /// </summary>
        private List<KeyChar> _KeyTable;
        #endregion

        #region 构造函数

        public JsonParson()
        {
            Init();
        }

        private void Init()
        {
            _KeyTable = new List<KeyChar>();
            _KeyTable .Add (new KeyChar ('{','}'));
            _KeyTable .Add (new KeyChar ('[',']'));
            _KeyTable.Add(new KeyChar(','));


        }
        #endregion

        #region 属性

        #endregion

        #region 公共函数

        public string Parser(string text)
        {
            text = text.Replace("\r\n", "");
            StringBuilder sb = new StringBuilder(text.Length);
            Stack<int> stack = new Stack<int>();
            int index = 0;
            int stackCount = 1;

            while (index < text.Length)
            {
                KeyChar key = GetIndex(text, index);
                if (key != null)
                {
                    switch (key.SeekFlag)
                    {
                        case CharFlag.Start:
                            if (key.Index != index)
                            {
                                sb.Append(GetLoopString(key.PreChar, stackCount));//添加占位符.
                                sb.Append(text.Substring(index, key.Index - index));
                                sb.Append("\r\n");
                            }
                            sb.Append(GetLoopString(key.PreChar, stackCount));
                            sb.Append(key.Start);
                            sb.Append("\r\n");
                            index = key.Index + 1;
                            stackCount++;
                            break;
                        case CharFlag.End:
                            sb.Append(GetLoopString(key.PreChar, stackCount));
                            sb.Append(text.Substring(index, key.Index - index));
                            sb.Append("\r\n");
                            stackCount--;
                            sb.Append(GetLoopString(key.EndChar, stackCount));
                            sb.Append(key.End);
                            sb.Append("\r\n");
                            index = key.Index + 1;
                            break;
                        case CharFlag.Same:
                            sb.Append(GetLoopString(key.PreChar, stackCount));
                            sb.Append(text.Substring(index, key.Index - index + 1));
                            sb.Append("\r\n");
                            index = key.Index + 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            return sb.ToString();
        }
        

        #endregion

        #region 私有函数
      


        private string GetLoopString(string text,int count)
        {
            if (count < 0) count = 0;
            StringBuilder sb = new StringBuilder(text.Length *count );
            for (int i = 0; i < count; i++)
            {
                sb.Append(text);
            }
            return sb.ToString();
        }


        private KeyChar GetIndex(string text,int index)
        {
            List<KeyChar> list = new List<KeyChar>();
            
            foreach (var item in _KeyTable)
            {
                int s = text.IndexOf(item.Start, index);
                int e = text.IndexOf(item.End, index);
                if(s!=-1 && e!=-1)
                {
                    if (item.Category  == CharCatraty.Same)
                    {
                        item.SeekFlag = CharFlag.Same;
                    }
                    else
                    {
                        item.SeekFlag = s < e ? CharFlag.Start : CharFlag.End;
                    }
                    item.Index = Math.Min(s, e);
                }
                else if(s!=-1)
                {
                    
                    item.Index = s;
                    if (item.Category == CharCatraty.Same)
                        item.SeekFlag = CharFlag.Start;
                    else 
                        item.SeekFlag = CharFlag.Start;
                }
                else if(e!=-1)
                {
                    item.Index = e;
                    if (item.Category == CharCatraty.Same)
                        item.SeekFlag = CharFlag.Start;
                    else 
                        item.SeekFlag = CharFlag.End;
                }
                else
                {
                    continue;
                }
                list.Add(item);

            }

            list.Sort();
            if (list.Count > 0) return list[0];
            return null;
        }

      

        #endregion


    }


    /// <summary>
    /// Json的关键字描述.
    /// </summary>
    class KeyChar : IComparable<KeyChar>
    {
        public KeyChar()
        {
            PreChar = "\t";
            EndChar = "\t";
        }

        public KeyChar(char c)
        {
            Start = c;
            End = c;
            Category =
                 CharCatraty.Same;
            PreChar = "\t";
            EndChar = "\t";
        }

        public KeyChar(char start,char end)
        {
            Start = start;
            End = end;
            PreChar = "\t";
            EndChar = "\t";
            Category = CharCatraty.Different;
        }
        /// <summary>
        /// 字符是否一致.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool Contains(char c)
        {
            if(c==Start || c==End )
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 指示KeyChar的类别.
        /// </summary>
        public CharCatraty Category { get; set; }


        /// <summary>
        /// 搜索到的标志.
        /// </summary>
        public CharFlag SeekFlag { get; set; }
        /// <summary>
        /// 前导字符.
        /// </summary>
        public string  PreChar { get; set; }
        
        /// <summary>
        /// 结束字符的前导字符.
        /// </summary>
        public string  EndChar { get; set; }

        /// <summary>
        /// 开始字符
        /// </summary>
        public char Start { get; set; }
        /// <summary>
        /// 结束字符.
        /// </summary>
        public char End { get; set; }
        /// <summary>
        /// 搜索的索引
        /// </summary>
        public int Index { get; set; }

        #region IComparable<KeyChar> 成员

        /// <summary>
        /// 比较.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(KeyChar other)
        {
            return Index.CompareTo(other.Index);
        }

        #endregion
    }

    /// <summary>
    /// 字符串的标记.
    /// </summary>
    enum CharFlag
    {
        /// <summary>
        /// 开始的字符.
        /// </summary>
        Start,
        /// <summary>
        /// 结束的字符.
        /// </summary>
        End,
        /// <summary>
        /// 开始的字符与结束的字符是一样.
        /// </summary>
        Same,
    }

    /// <summary>
    /// 指示KeyChar首位是否一致.
    /// </summary>
    enum CharCatraty
    {
        /// <summary>
        /// 不同
        /// </summary>
        Different,
        /// <summary>
        /// 相同.
        /// </summary>
        Same
    }
   
}
