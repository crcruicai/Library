/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/1 17:56:19
 * 描述说明：源码来自:
 *          Fireball 开源项目.
 * 
 * 更改历史：
 * 
 * *******************************************************/

#region Usings
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
#endregion

namespace CRC.Collections
{

 
    #region KeyedValue
    /// <summary>
    /// 键与值.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct KeyedValue<T>
    {
        /// <summary>
        /// 键.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 将此实例的值转换为 System.String。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (this.Key != null)
            {
                builder.Append(this.Key.ToString());
            }
            builder.Append(", ");
            if (this.Value != null)
            {
                builder.Append(this.Value.ToString());
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
    #endregion

    #region KeyedCollection
    /// <summary>
    /// 集合键嵌入值的集合.
    /// <para>该集合实现了<see cref="INotifyCollectionChanged"/>接口和<see cref="INotifyPropertyChanged"/>接口</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KeyedCollection<T> : 
        System.Collections.ObjectModel.KeyedCollection<string, KeyedValue<T>> , INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region Monitor
        /// <summary>
        /// 简单
        /// </summary>
        [Serializable]
        private class SimpleMonitor : IDisposable
        {
            // Fields
            private int _busyCount;

            // Methods
            public void Dispose()
            {
                this._busyCount--;
            }

            public void Enter()
            {
                this._busyCount++;
            }

            // Properties
            public bool Busy
            {
                get
                {
                    return (this._busyCount > 0);
                }
            }
        }

        private SimpleMonitor Monitor
        { get; set; }
        #endregion

        #region Add
        public void Add(string key, T value)
        {
            this.Add(new KeyedValue<T>() { Key = key, Value = value });
        }
        #endregion

        #region InsertItem
        /// <summary>
        /// 将元素插入指定的索引处.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, KeyedValue<T> item)
        {
            this.CheckReentrancy();
            base.InsertItem(index, item);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, index));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }
        #endregion

        #region ClearItems
        /// <summary>
        /// 清理所有元素.
        /// </summary>
        protected override void ClearItems()
        {
            this.CheckReentrancy();
            base.ClearItems();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset, null));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }
        #endregion

        #region RemoveItem
        /// <summary>
        /// 移除指定的元素.
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            this.CheckReentrancy();
            base.RemoveItem(index);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, null, index));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }
        #endregion

        #region SetItem
        /// <summary>
        /// 设置指定的元素.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, KeyedValue<T> item)
        {
            this.CheckReentrancy();
            base.SetItem(index, item);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace, item, index));
            this.OnPropertyChanged("Count");
            this.OnPropertyChanged("Item[]");
        }
        #endregion

        #region GetKeyItem
        /// <summary>
        /// 获取元素对应的键.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override string GetKeyForItem(KeyedValue<T> item)
        {
            return item.Key;
        }
        #endregion

        #region Monitor Methods
        /// <summary>
        /// .
        /// </summary>
        /// <returns></returns>
        protected IDisposable BlockReentrancy()
        {
            this.Monitor.Enter();
            return this.Monitor;
        }

        protected void CheckReentrancy()
        {
            if ((this.Monitor.Busy && (this.CollectionChanged != null)) && (this.CollectionChanged.GetInvocationList().Length > 1))
            {
                throw new InvalidOperationException("ObservableCollectionReentrancyNotAllowed");
            }
        }
        #endregion

        #region Changes Methods
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                using (this.BlockReentrancy())
                {
                    this.CollectionChanged(this, e);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
        #endregion

        #region INotifyCollectionChanged Members

        /// <summary>
        /// 集合更改时 引发事件.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// 属性更改时 引发事件.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    #endregion
}
