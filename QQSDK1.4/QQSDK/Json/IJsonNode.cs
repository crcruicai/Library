﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QQSDK.Json
{
    interface IJsonNode
    {
      

        IJsonNode Next { get; set; }

        IJsonNode Father { get; set; }

        IList<IJsonNode> Items { get; } 

    }

    /// <summary>
    /// 根节点.
    /// </summary>
    class RootNode
    {
        public RootNode()
        {
            Items = new List<IJsonNode>();

        }
        List<IJsonNode> Items { get; set; }
    

    
    }


    class LeafNode:IJsonNode
    {
        #region 变量与字段
        List<IJsonNode> _Items;
        IJsonNode _Father;
        IJsonNode _Next;

        #endregion
        public LeafNode(string name,string value,IJsonNode father,IJsonNode next)
        {
            _Items = new List<IJsonNode>();
            Name = name;
            Value = value;
            _Father = father;
            _Next = next;
        }

        public LeafNode():this("","",null,null)
        {

        }

        

        public string  Name { get; set; }

        public string Value { get; set; }


        #region IJsonNode 成员

       

        public IJsonNode Next
        {
            get
            {
                return _Next;
            }
            set
            {
                _Next = value;
            }
        }

        public IJsonNode Father
        {
            get
            {
                return _Father;
            }
            set
            {
                _Father = value;
            }
        }

        public IList<IJsonNode> Items
        {
            get
            {
                return _Items;
            }
            
        }

        #endregion
    }

    class LeafNodeColleage:IList <IJsonNode>
    {
        List<IJsonNode> _List = new List<IJsonNode>();

        public LeafNodeColleage()
        {

        }


        #region IList<IJsonNode> 成员

        public int IndexOf(IJsonNode item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IJsonNode item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IJsonNode this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<IJsonNode> 成员

        public void Add(IJsonNode item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IJsonNode item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IJsonNode[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(IJsonNode item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<IJsonNode> 成员

        public IEnumerator<IJsonNode> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    /// <summary>
    /// 类  名：JSONConvert
    /// 描  述：JSON解析类
    /// 编  写：dnawo
    /// 站  点：http://www.mzwu.com/
    /// 日  期：2010-01-06
    /// 版  本：1.1.0
    /// </summary>
    public static class JSONConvert
    {
        #region 全局变量

        private static JSONObject _json = new JSONObject();//寄存器
        private static readonly string _SEMICOLON = "@semicolon";//分号转义符
        private static readonly string _COMMA = "@comma"; //逗号转义符

        #endregion

        #region 字符串转义
        /// <summary>
        /// 字符串转义,将双引号内的:和,分别转成_SEMICOLON和_COMMA
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string StrEncode(string text)
        {
            MatchCollection matches = Regex.Matches(text, "\\\"[^\\\"]+\\\"");
            foreach (Match match in matches)
            {
                text = text.Replace(match.Value, match.Value.Replace(":", _SEMICOLON).Replace(",", _COMMA));
            }

            return text;
        }

        /// <summary>
        /// 字符串转义,将_SEMICOLON和_COMMA分别转成:和,
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string StrDecode(string text)
        {
            return text.Replace(_SEMICOLON, ":").Replace(_COMMA, ",");
        }

        #endregion

        #region JSON最小单元解析

        /// <summary>
        /// 最小对象转为JSONObject
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static JSONObject DeserializeSingletonObject(string text)
        {
            JSONObject jsonObject = new JSONObject();
            text = StrDecode(text);
            MatchCollection matches = Regex.Matches(text, "(\\\"(?<key>[^\\\"]+)\\\":\\\"(?<value>[^,\\\"]+)\\\")|(\\\"(?<key>[^\\\"]+)\\\":(?<value>[^,\\\"\\}]+))");
            foreach (Match match in matches)
            {
                string value = match.Groups["value"].Value;
                jsonObject.Add(match.Groups["key"].Value, _json.ContainsKey(value) ? _json[value] : StrDecode(value));
            }

            return jsonObject;
        }

        /// <summary>
        /// 最小数组转为JSONArray
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static JSONArray DeserializeSingletonArray(string text)
        {
            JSONArray jsonArray = new JSONArray();

            MatchCollection matches = Regex.Matches(text, "(\\\"(?<value>[^,\\\"]+)\")|(?<value>[^,\\[\\]]+)");
            foreach (Match match in matches)
            {
                string value = match.Groups["value"].Value;
                jsonArray.Add(_json.ContainsKey(value) ? _json[value] : StrDecode(value));
            }

            return jsonArray;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Deserialize(string text)
        {
            text = StrEncode(text);//转义;和,

            int count = 0;
            string key = string.Empty;
            string pattern = "(\\{[^\\[\\]\\{\\}]+\\})|(\\[[^\\[\\]\\{\\}]+\\])";

            while (Regex.IsMatch(text, pattern))
            {
                MatchCollection matches = Regex.Matches(text, pattern);
                foreach (Match match in matches)
                {
                    key = "___key" + count + "___";

                    if (match.Value.Substring(0, 1) == "{")
                        _json.Add(key, DeserializeSingletonObject(match.Value));
                    else
                        _json.Add(key, DeserializeSingletonArray(match.Value));

                    text = text.Replace(match.Value, key);

                    count++;
                }
            }
            return text;
        }

        #endregion

        #region 公共接口

        /// <summary>
        /// 序列化JSONObject对象
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static JSONObject DeserializeObject(string text)
        {
            _json.Clear();
            text = text.Replace("\r\n", "");
            string key= Deserialize(text);
            if(_json .ContainsKey (key))
            {
                return _json[key] as JSONObject ;
            }
            return null;
        }

        /// <summary>
        /// 序列化JSONArray对象
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static JSONArray DeserializeArray(string text)
        {
            return _json[Deserialize(text)] as JSONArray;
        }

        /// <summary>
        /// 反序列化JSONObject对象
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string SerializeObject(JSONObject jsonObject)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (KeyValuePair<string, object> kvp in jsonObject)
            {
                if (kvp.Value is JSONObject)
                {
                    sb.Append(string.Format("\"{0}\":{1},", kvp.Key, SerializeObject((JSONObject)kvp.Value)));
                }
                else if (kvp.Value is JSONArray)
                {
                    sb.Append(string.Format("\"{0}\":{1},", kvp.Key, SerializeArray((JSONArray)kvp.Value)));
                }
                else if (kvp.Value is String)
                {
                    sb.Append(string.Format("\"{0}\":\"{1}\",", kvp.Key, kvp.Value));
                }
                else
                {
                    sb.Append(string.Format("\"{0}\":\"{1}\",", kvp.Key, ""));
                }
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 反序列化JSONArray对象
        /// </summary>
        /// <param name="jsonArray"></param>
        /// <returns></returns>
        public static string SerializeArray(JSONArray jsonArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < jsonArray.Count; i++)
            {
                if (jsonArray[i] is JSONObject)
                {
                    sb.Append(string.Format("{0},", SerializeObject((JSONObject)jsonArray[i])));
                }
                else if (jsonArray[i] is JSONArray)
                {
                    sb.Append(string.Format("{0},", SerializeArray((JSONArray)jsonArray[i])));
                }
                else if (jsonArray[i] is String)
                {
                    sb.Append(string.Format("\"{0}\",", jsonArray[i]));
                }
                else
                {
                    sb.Append(string.Format("\"{0}\",", ""));
                }

            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        #endregion
    }

    /// <summary>
    /// 类  名：JSONObject
    /// 描  述：JSON对象类
    /// 编  写：dnawo
    /// 站  点：http://www.mzwu.com/
    /// 日  期：2010-01-06
    /// 版  本：1.1.0
    /// 
    ///  
    /// </summary>
    public class JSONObject : Dictionary<string, object>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if(base.ContainsKey(key))
                {
                    return base[key];
                }
                return null;
            }
        }

        public Type GetType(string key)
        {
            if(base.ContainsKey (key))
            {
                object obj = base[key];
                return obj.GetType();
            }
            return null;
        }

        public string GetString(string key)
        {
            return this[key] as string;
        }

        public JSONObject GetJSONObject(string key)
        {
            return this[key] as JSONObject;
        }

        public JSONArray GetJSONArray(string key)
        {
            return this[key] as JSONArray;
        }

    }


    /// <summary>
    /// 类  名：JSONArray
    /// 描  述：JSON数组类
    /// 编  写：dnawo
    /// 站  点：http://www.mzwu.com/
    /// 日  期：2010-01-06
    /// 版  本：1.1.0
    ///
    /// </summary>
    public class JSONArray : List<object>
    { }
  






}
