/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/31 15:57:02
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

namespace CRC.Util
{
    /// <summary>
    /// 字典,但每个项的值都是一本字典.
    /// <para></para>
    /// </summary>
    /// <typeparam name="TKey">本字典的键.</typeparam>
    /// <typeparam name="SKey">子字典的键.</typeparam>
    /// <typeparam name="SValue">子字典的值.</typeparam>
    [Serializable]
    public class ThreeDictionary<TKey,SKey,SValue>:Dictionary <TKey ,Dictionary <SKey ,SValue>>
    {
        #region 字段与变量

        


        #endregion 字段与变量


        #region 构造函数
        public ThreeDictionary():base()
        {

        }

        protected ThreeDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }


        #endregion 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frist">第一关键字.</param>
        /// <param name="secend">第二关键字.</param>
        /// <param name="value">值.</param>
        /// <param name="replace">如果键存在,是否替换值.true 替换.</param>
        public void Add(TKey frist, SKey secend, SValue value,bool replace)
        {
            if (frist == null) throw new ArgumentNullException("frist");
            if (value == null) throw new ArgumentNullException("value");
            if (secend  == null) throw new ArgumentNullException("secend ");

            if (this.ContainsKey(frist))//存在该键.
            {
                Dictionary<SKey, SValue> dic = this[frist];
                AddOrReplace(dic, secend, value, replace);
            }
            else
            {
                //新建 字典.
                Dictionary<SKey, SValue> dic = new Dictionary<SKey, SValue>();
                dic.Add(secend, value);
                this.Add(frist, dic);
            }
        }

        /// <summary>
        /// 根据键,将字典添加指定字典中.
        /// </summary>
        /// <param name="key">主键.</param>
        /// <param name="dic">字典.</param>
        /// <param name="replace">如果键存在,是否替换值.true 替换.</param>
        public void AddRange(TKey key, Dictionary<SKey, SValue> dic, bool replace)
        {
            if (dic == null) throw new ArgumentNullException("dic");
            if (key  == null) throw new ArgumentNullException("key ");
            if (this.ContainsKey(key))//存在该键.
            {
                Dictionary<SKey, SValue> items = this[key];
                foreach (var item in dic.Keys)
                {
                    AddOrReplace(items, item, dic[item], replace);
                }
            }
            else
            {
                this.Add(key, dic);
            }
        }


        /// <summary>
        /// 将键和值添加到指定的字典中.如果键存在,指定是否替换.不会报错.
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="replace"></param>
        private void AddOrReplace(Dictionary<SKey, SValue> dic, SKey key, SValue value, bool replace)
        {
            if (dic.ContainsKey(key))
            {
                //替换该值.
                if(replace)
                    dic[key] = value;
            }
            else
            {
                //添加该值.
                dic.Add(key, value);
            }

        }

        /// <summary>
        /// 从字典中移除指定值.
        /// </summary>
        /// <param name="frist">第一关键字.</param>
        /// <param name="secend">第二关键字.</param>
        /// <returns></returns>
        public bool Remove(TKey frist, SKey secend)
        {
            if (this.ContainsKey(frist))
            {
                return this[frist].Remove(secend);
            }
            return false;
        }

        /// <summary>
        /// 获取与指定的键相关联的值。
        /// </summary>
        /// <param name="frist">要获取的值的第一键。</param>
        /// <param name="secend">要获取的值的第二键</param>
        /// <param name="value">当此方法返回值时，如果找到该键，便会返回与指定的键相关联的值；否则，则会返回 value 参数的类型默认值。该参数未经初始化即被传递。</param>
        /// <returns>包含具有指定键的元素，则为 true；否则为
        ///     false。</returns>
        public bool TryGetValue(TKey frist, SKey secend, out SValue value)
        {
            if (frist == null) throw new ArgumentNullException("frist");
            if (secend  == null) throw new ArgumentNullException("secend ");
            if (this.ContainsKey(frist))
            {
               return  this[frist].TryGetValue(secend, out value);
            }
            value = default(SValue);
            return false;
        }

        /// <summary>
        /// 获取与指定的键相关联的值。
        /// </summary>
        /// <param name="frist">要获取的值的第一键。</param>
        /// <param name="secend">要获取的值的第二键.</param>
        /// <returns>当此方法返回值时，如果找到该键，便会返回与指定的键相关联的值；否则，则会返回 value 参数的类型默认值。</returns>
        public SValue FindValue(TKey frist, SKey secend)
        {
            SValue value;
            TryGetValue(frist, secend, out value);
            return value;
        }
    }
}
