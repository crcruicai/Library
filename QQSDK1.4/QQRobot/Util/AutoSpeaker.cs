/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 23:25:31
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QQSDK.Net ;
using System.IO;
namespace QQRobot
{
    [Serializable]
    class AutoMessage:IComparable   
    {
        public int Count { get; set; }
        public string Text { get; set; }


        #region IComparable 成员

        public int CompareTo(object obj)
        {
            AutoMessage auto = obj as AutoMessage;
            if (auto == null) throw new ArgumentException("obj");
            if (auto.Count > Count) return 1;
            if (auto.Count == Count) return 0;
            else
                return -1;
           
        }

        #endregion
    }

    class RobotWordInfo
    {
        public string RobotName { get; set; }

        public string Speaker { get; set; }

        
    }


    class AutoSpeaker
    {
        #region 字段与变量

        private Random _Random = new Random();
        public static object WordMapLock = new object();

        private HttpWeb _HttpWeb;
        /// <summary>
        /// 对话词典.
        /// </summary>
        public static DictionaryCollection<string, AutoMessage> _WordMap;
        
        
        
        #endregion

        #region 构造函数
        public AutoSpeaker()
        {
            _HttpWeb = new HttpWeb();
        }

        #endregion

        #region 属性

        private string _RobotName;
        /// <summary>
        /// 对话的机器人的名称.
        /// </summary>	
        public string RobotName
        {
            get { return _RobotName; }
            set { _RobotName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Speaker { get; set; }


        #endregion

        #region 公共函数

        public string Speak(string word, string split)
        {
            string[] items = word.Split(new string[] {split},  StringSplitOptions.RemoveEmptyEntries );
            List<string> list = new List<string>();
            foreach (var item in items)
            {
                list.Add (GetAutoString (item));
            }
            if (list.Count > 0)
            {
                return ChangeText(list[_Random.Next(list.Count - 1)]);
            }
            else
            {
                return string.Empty;
            }
           
        }

        private string ChangeText(string text)
        {
            if (text == null) return string.Empty;
            return text = text.Replace("[cqname]", RobotName)
                    .Replace("[yqname]", RobotName)
                    .Replace("[name]", Speaker)
                    .Replace("[bq", "[")
                    .Replace("[enter]", "\\\\r\\\\n")
                    .Replace("iamxhj.com", "");
        }

        /// <summary>
        /// 根据对方的话,说话.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string Speak(string word)
        {
            string text;
            //检查是否为定义的命令.
            text = GetCommand(word);
            if (text != null)
                return text;

            //检查是否为定义的对话
            //text = GetAutoString(word);

            if (text == null)
            {
                if ((text =GetForAutoString(word))==null)
                {
                    //委托其他对象,获取对话.
                    //return GetWordNetApi(word);
                }
            }
            if(text!=null)
             text = text.Replace("[cqname]", RobotName)
                    .Replace("[yqname]", RobotName)
                    .Replace("[name]", Speaker)
                    .Replace("[bq", "[")
                    .Replace("[enter]", "\\\\r\\\\n")
                    .Replace("iamxhj.com", "");

                return text;          
           
             
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 是否为命令.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetCommand(string text)
        {
            return null;
        }

        /// <summary>
        /// 从字典中获取对话.没有就返回null.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetAutoString(string text)
        {
            lock (_WordMap)
            {
                if (_WordMap.ContainsKey(text))
                {
                    IList<AutoMessage> list = _WordMap[text];
                    //返回最小的值.
                    AutoMessage auto = list[_Random.Next(list.Count - 1)];
                    auto.Count++;
                    return auto.Text;
                }
            }
            return null;
        }


        private string GetForAutoString(string text)
        {
            lock (_WordMap )
            {
                List<string> list = new List<string>();
                foreach (var item in _WordMap)
                {
                    if (text.IndexOf(item.Key) > -1)
                    {
                        list.Add(item.Value[_Random.Next(item.Value.Count - 1)].Text);
                        //return item.Value[_Random.Next(item.Value.Count - 1)].Text;
                    }
                }
                if(list .Count >0)
                    return list[_Random.Next(list.Count - 1)];
                return null;
            }

        }


        private void SpiltWord()
        {
            //过滤一些代词,


        }

        /// <summary>
        /// 从其他的网络API中,获取对话.
        /// </summary>
        /// <param name="word">对话.</param>
        /// <returns></returns>
        private string GetWordNetApi(string word)
        {
           string text=_HttpWeb.PostWebRequest ("http://www.xiaohuangji.com/ajax.php","para=="+Encode .ToUTF8(word ),Encoding.UTF8);

           return text.Replace("小黄鸡", _RobotName);
        }


        private void AddWordMap(string key, string value)
        {
            lock (_WordMap)
            {
                AutoMessage auto = new AutoMessage()
                {
                    Count =1,
                     Text =value
                };
                List<AutoMessage >list=new List<AutoMessage> ();
                list.Add (auto);
                _WordMap.TryAdd(key, list);
            }
        }

        /// <summary>
        /// 随机产生一个表情.
        /// </summary>
        private void GetFace()
        {

        }
        #endregion

        public static void LoadReply()
        {
            _WordMap = new DictionaryCollection<string, AutoMessage>();
            string filepath = Environment .CurrentDirectory + "\\reply.ini";
            if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(filepath, Encoding.Default);
                string s;
                long i = 0;
                long j = 0;
                string key = "", words = "";
                while ((s = sr.ReadLine()) != null)
                {

                    i = i + 1;
                    if (i % 3 == 1)
                        key = s;
                    if (i % 3 == 2)
                    {
                        j = j + 1;
                        words = s;
                        if (key.Trim().Length > 0 && words.Trim().Length > 0)
                        {
                            _WordMap.Add(key, new AutoMessage() { Count = 0, Text = words });
                            //lvwWordsList.Items.Add(new ListViewItem(new string[] { j.ToString(), key, words }));
                        }
                    }
                }
                sr.Close();
            }
            else
            {
               
            }
        }

        public static void LoadMap()
        {
            string path = Environment.CurrentDirectory + "\\Words.db";
            _WordMap = Util.SerializeHelper.Load<DictionaryCollection<string, AutoMessage>>(path);
        }


        public static void SaveMap()
        {
            Util.SerializeHelper.Save(_WordMap, Environment.CurrentDirectory + "\\Words.db");
        }


    }
}
