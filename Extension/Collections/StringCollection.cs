/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 11:32:46
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
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using CRC.DebuggerViews;
    #endregion

    #region StringCollection
    /// <summary>
    /// <para>String 集合(实现了通知绑定,属性通知更改)</para>
    /// <para>详情查看该类实现的接口.</para>
    /// A full featured string collection with osservable behaviour usefull with 
    /// databinding
    /// </summary>
    [Serializable, DebuggerTypeProxy(typeof(Fcore_StringCollectionView)), DebuggerDisplay("Count = {Count}")]
    public class StringCollection : IList<string>, ICollection<string>, IEnumerable<string>,
        System.Collections.IEnumerable, ICloneable, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region SimpleMonitor
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

        #region Items
        private IList<string> _items = null;

        private List<string> Items
        {
            get
            {
                return (List<string>)_items;
            }
            set
            {
                _items = value;
            }
        }
        #endregion

        #region Ctor
        public StringCollection()
        {
            this.Items = new List<string>();
            this.Monitor = new SimpleMonitor();
        }

        internal StringCollection(IList<string> coll)
        {
            this.Items = new List<string>(coll);
            this.Monitor = new SimpleMonitor();
        }
        #endregion

        #region Reentrancy Methods
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

        #region Xml Methods
        /// <summary>
        /// Get a Xml version of the collection
        /// &lt;collection&gt;
        /// &lt;item&gt;String Value&lt;/item&gt;
        /// &lt;/collection&gt;
        /// </summary>
        /// <returns></returns>
        public XElement ToXml()
        {
            XElement xCollection = new XElement("collection");

            var items = from p in this.Items select new XElement("item", p);

            if (items.Count() > 0)
                xCollection.Add(items.ToArray());

            return xCollection;
        }

        /// <summary>
        /// Import the xml rappresentation of the collection
        /// </summary>
        /// <param name="xCollection"></param>
        public void ImportXml(XElement xCollection)
        {
            this.CheckReentrancy();
            if (xCollection.Name.LocalName != "collection")
                throw new Exception("Invalid xml");
            var items = from p in xCollection.Descendants("item")
                        select p.Value;
            Items.AddRange(items);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                items));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }
        #endregion

        #region Range Methods
        public List<string> GetRange(int index, int count)
        {
            return Items.GetRange(index, count);
        }

        /// <summary>
        /// Insert a specified range of item
        /// </summary>
        /// <param name="index">The zero based index where the elements should be inserted</param>
        /// <param name="collection">The elements to be inserted</param>
        public void InsertRange(int index, IEnumerable<string> collection)
        {
            this.CheckReentrancy();
            Items.InsertRange(index, collection);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                collection, index));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }
        #endregion

        #region Methods
        public int LastIndexOf(string item)
        {
            return Items.LastIndexOf(item);
        }

        public int LastIndexOf(string item, int index)
        {
            return Items.LastIndexOf(item, index);
        }

        public int LastIndexOf(string item, int index, int count)
        {
            return Items.LastIndexOf(item, index, count);
        }

        public void Reverse(int index, int count)
        {
            this.CheckReentrancy();
            Items.Reverse(index, count);
            // var items = (IList<string>)Items.GetRange(index, count);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void Reverse()
        {
            this.CheckReentrancy();
            Items.Reverse();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null));
            OnPropertyChanged("Item[]");
        }

        public void RemoveRange(int index, int count)
        {
            var items = Items.GetRange(index, count);
            Items.RemoveRange(index, count);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                items));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void RemoveAll(Predicate<string> match)
        {
            this.CheckReentrancy();
            var c = Items.RemoveAll(match);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, ""));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void Sort()
        {
            this.CheckReentrancy();
            Items.Sort();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public void Sort(Comparison<string> comparison)
        {
            this.CheckReentrancy();
            Items.Sort(comparison);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public void Sort(IComparer<string> comparer)
        {
            this.CheckReentrancy();
            Items.Sort(comparer);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public void Sort(int index, int count, IComparer<string> comparer)
        {
            this.CheckReentrancy();
            Items.Sort(index, count, comparer);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Item[]");
        }

        public bool Exists(Predicate<string> match)
        {
            return Items.Exists(match);
        }

        public void Move(int oldIndex, int newIndex)
        {
            this.CheckReentrancy();
            var t = this.Items[oldIndex];
            this.Items.RemoveAt(oldIndex);
            this.Items.Insert(newIndex, t);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Move, t, oldIndex, newIndex));
            // OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }
        #endregion

        #region FindMethods
        public int FindIndex(int startIndex, int count, Predicate<string> match)
        {
            return Items.FindIndex(startIndex, count, match);
        }

        public int FindIndex(int startIndex, Predicate<string> match)
        {
            return Items.FindIndex(startIndex, match);
        }

        public int FindIndex(Predicate<string> match)
        {
            return Items.FindIndex(match);
        }

        public string FindLast(Predicate<string> match)
        {
            return Items.FindLast(match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<string> match)
        {
            return Items.FindLastIndex(startIndex, count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<string> match)
        {
            return Items.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(Predicate<string> match)
        {
            return Items.FindLastIndex(match);
        }
        #endregion

        #region IList<string> Members

        public int IndexOf(string item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, string item)
        {
            this.CheckReentrancy();
            Items.Insert(index, item);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, index));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void RemoveAt(int index)
        {
            this.CheckReentrancy();
            var item = Items[index];
            Items.RemoveAt(index);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public string this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
                OnPropertyChanged("Item[]");
            }
        }

        #endregion

        #region ICollection<string> Members

        public void Add(string item)
        {
            this.CheckReentrancy();
            this.Items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void AddRange(IEnumerable<string> items)
        {
            this.Items.AddRange(items);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                 NotifyCollectionChangedAction.Add, items));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public void Clear()
        {
            this.CheckReentrancy();
            Items.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
            OnPropertyChanged("Count");
            OnPropertyChanged("Item[]");
        }

        public bool Contains(string item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string item)
        {
            this.CheckReentrancy();
            var b = Items.Remove(item);

            if (b)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove, item));

                OnPropertyChanged("Count");
                OnPropertyChanged("Item[]");
            }

            return b;
        }

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

        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region ICollection<string> Members

        void ICollection<string>.Add(string item)
        {
            this.Add(item);
        }

        void ICollection<string>.Clear()
        {
            this.Clear();
        }

        bool ICollection<string>.Contains(string item)
        {
            return this.Contains(item);
        }

        void ICollection<string>.CopyTo(string[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }

        int ICollection<string>.Count
        {
            get { return this.Count; }
        }

        bool ICollection<string>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<string>.Remove(string item)
        {
            return this.Remove(item);
        }

        #endregion

        #region IEnumerable<string> Members

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new StringCollection(Items.ToArray());
        }

        public void RemoveDuplicates()
        {
            HashSet<string> hashset = new HashSet<string>(Items);

            Items = hashset.ToList();
        }

        public void Trim()
        {
            var tmp = new List<string>(Items.Count);

            foreach (var s in Items)
            {
                if (string.IsNullOrEmpty(s) == false && !string.IsNullOrEmpty(s.Trim()))
                    tmp.Add(s);
            }
            Items = tmp;
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    #endregion
}
