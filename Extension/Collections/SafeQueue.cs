/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 11:08:36
 * 描述说明：在线程上安全的队列.
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
    /// 在线程上安全的队列.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeQueue<T> : Queue<T>
    {
        private object _SafeLock = new object();

        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="item"></param>
        public new void Enqueue(T item)
        {
            lock (_SafeLock)
            {
                base.Enqueue(item);
            }
        }

        /// <summary>
        /// 出列
        /// </summary>
        /// <returns></returns>
        public new T Dequeue()
        {
            lock (_SafeLock)
            {
                return base.Dequeue();
            }
        }
        /// <summary>
        /// 出列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Dequeue(out T item)
        {
            lock (_SafeLock)
            {
                if (this.Count > 0)
                {
                    item = base.Dequeue();
                    return true;
                }
                else
                {
                    item = default(T);
                    return false;
                }
            }
        }

    }
}
