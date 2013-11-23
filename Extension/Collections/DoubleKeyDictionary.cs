/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/21 11:14:59
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Collections
{

    /// <summary>
    /// 拥有双关键字的词典.
    /// <para>注意:KeyOne,KeyTwo都是唯一关键字,添加重复的关键字是不允许的.</para>
    /// </summary>
    /// <typeparam name="TKeyOne"></typeparam>
    /// <typeparam name="TKeyTwo"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class DoubleKeyDictionary<TKeyOne,TKeyTwo,TValue>
    {
        private class KeyValue
        {
            public KeyValue (TKeyOne keyOne,TKeyTwo keyTwo,TValue value)
	        {
                KeyOne = keyOne;
                KeyTwo = keyTwo;
                Value = value;
	        }

            /// <summary>
            /// 
            /// </summary>
            public TValue  Value { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public TKeyOne KeyOne { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public TKeyTwo KeyTwo { get; set; }
        }
 

        #region 字段与变量
        Dictionary<TKeyOne, KeyValue> _OneMap;
        Dictionary<TKeyTwo , KeyValue> _TwoMap;


        #endregion 字段与变量

        #region 构造函数

        public DoubleKeyDictionary()
        {
            _OneMap = new Dictionary<TKeyOne, KeyValue>();
            _TwoMap = new Dictionary<TKeyTwo, KeyValue>();
        }

      

        #endregion 构造函数

        #region 属性

        #endregion 属性

        #region 公共函数

        #endregion 公共函数

        #region 私有函数

        #endregion 私有函数




        #region IDictionary<TKeyOne,TValue> 成员

        public void Add(TKeyOne keyOne,TKeyTwo keyTwo, TValue value)
        {
            if (keyOne == null) throw new ArgumentNullException("keyOne");
            if (keyTwo  == null) throw new ArgumentNullException("keyTwo ");
            if (value == null) throw new ArgumentNullException("value");
            KeyValue keyValue= new KeyValue(keyOne, keyTwo, value);
            _OneMap.Add(keyOne, keyValue);
            _TwoMap.Add(keyTwo, keyValue);

        }

        public bool ContainsKeyOne(TKeyOne key)
        {
            return _OneMap.ContainsKey(key);
        }
        public bool ContainsKeyTwo(TKeyTwo key)
        {
            return _TwoMap.ContainsKey(key);
        }


        public ICollection<TKeyOne> KeysOne
        {
            get { return _OneMap.Keys; }
        }
        public ICollection<TKeyTwo> KeysTwo
        {
            get { return _TwoMap.Keys; }
        }
        public bool RemoveOne(TKeyOne key)
        {
            if (_OneMap.ContainsKey(key))
            {
                KeyValue value = _OneMap[key];
                _OneMap.Remove(key);
                _TwoMap.Remove(value.KeyTwo);
                return true;
            }
            return false;
        }
        public bool RemoveTwo(TKeyTwo key)
        {
            if (_TwoMap.ContainsKey(key))
            {
                KeyValue value = _TwoMap[key];
                _OneMap.Remove(value.KeyOne);
                _TwoMap.Remove(value.KeyTwo);
            }
            return false;
        }

        public bool TryGetValue(TKeyOne key, out TValue value)
        {
            KeyValue keyValue;
            if (_OneMap.TryGetValue(key, out keyValue))
            {
                value = keyValue.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
        public bool TryGetValue(TKeyTwo key, out TValue value)
        {
            KeyValue keyValue;
            if (_TwoMap.TryGetValue(key, out keyValue))
            {
                value = keyValue.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
        public ICollection<TValue> Values
        {
            get 
            {
                List<TValue> list = new List<TValue>();
                foreach (var item in _OneMap.Values)
                {
                    list.Add(item.Value);
                }
                return list;
            }
        }

        public TValue GetOneValue(TKeyOne key)
        {
            if (_OneMap.ContainsKey(key))
            {
                KeyValue keyValue=_OneMap [key];

                return keyValue.Value;
            }
            throw new System.Collections.Generic.KeyNotFoundException();
        }
        public TValue GetTwoValue(TKeyTwo key)
        {
            if (_TwoMap.ContainsKey(key))
            {
                KeyValue keyValue = _TwoMap[key];

                return keyValue.Value;
            }
            throw new System.Collections.Generic.KeyNotFoundException();
        }

        public void SetOneValue(TKeyOne key,TValue value)
        {
            if (_OneMap.ContainsKey(key))
            {
                _OneMap[key].Value =value;
                KeyValue keyValue = _OneMap[key];
                _TwoMap[keyValue.KeyTwo].Value = value;
                return;
            }
            throw new System.Collections.Generic.KeyNotFoundException();

        }

        public void SetTwoValue(TKeyTwo key,TValue value)
        {
            if (_TwoMap.ContainsKey(key))
            {
                _TwoMap[key].Value = value;
                KeyValue keyValue = _TwoMap[key];
                _OneMap[keyValue.KeyOne].Value = value;
                return;
            }
            throw new System.Collections.Generic.KeyNotFoundException();

        }

        #endregion

        #region ICollection<KeyValuePair<TKeyOne,TValue>> 成员

  

        public void Clear()
        {
            _OneMap.Clear();
            _TwoMap.Clear();
        }

  
        public int Count
        {
            get { return _OneMap.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

    

        #endregion

        #region IEnumerable<KeyValuePair<TKeyOne,TValue>> 成员

        public IEnumerator<KeyValuePair<TKeyOne, TValue>> GetEnumeratorOne()
        {
            foreach (var item in _OneMap.Values)
            {
                KeyValuePair<TKeyOne, TValue> value = new KeyValuePair<TKeyOne, TValue>(item.KeyOne, item.Value);
                yield return value;
            }
        }

        public IEnumerator<KeyValuePair<TKeyTwo, TValue>> GetEnumeratorTwo()
        {
            foreach (var item in _TwoMap.Values)
            {
                KeyValuePair<TKeyTwo, TValue> value = new KeyValuePair<TKeyTwo, TValue>(item.KeyTwo, item.Value);
                yield return value;
            }
        }

      

        #endregion

       
    }
}
