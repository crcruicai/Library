/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 23:32:59
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QQRobot
{
    /// <summary>
    /// 字典,值为列表.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    class DictionaryCollection<TKey, TValue> : Dictionary<TKey, IList<TValue>>
    {

        #region 字段与变量

        #endregion 字段与变量

        #region 构造函数
        /// <summary>
        /// 字典,值为列表.
        /// </summary>
        public DictionaryCollection()
            : base()
        {

        }

        /// <summary>
        /// 字典,值为列表.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DictionaryCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion 构造函数

        #region 属性

        #endregion 属性

        #region 公共函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
            {
                this[key].Add(value);
            }
            else
            {
                IList<TValue> list = new List<TValue>();
                list.Add(value);
                this.Add(key, list);
            }

        }


        public TValue FindValue(TKey key, Predicate<TValue> predicate)
        {
            if (this.ContainsKey(key))
            {
                foreach (var item in this[key])
                {
                    if (predicate(item))
                        return item;
                }
                return default(TValue);
            }
            else
            {
                return default(TValue);
            }
        }

        #endregion 公共函数

        #region 私有函数

        #endregion 私有函数

        

    }
}
