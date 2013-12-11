using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Systems
{
    /// <summary>
    /// 字典 类的扩展方法.
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        /// <typeparam name="TKey">Key</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict == null) throw new ArgumentException("dict is not null");
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }


        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        /// <typeparam name="TKey">Key</typeparam>
        /// <typeparam name="TValue">值.</typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict == null) throw new ArgumentException("dict is not null");
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
            return dict;
        }
        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            if (dict == null) throw new ArgumentException("dict is not null");
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }




    }
}
