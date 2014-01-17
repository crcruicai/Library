using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace CRC.Controls
{
    /// <summary>
    /// <para>保持一个额外的选择“与计数“值的每个项目在列表中。
    /// 有用的CheckBoxComboBox。它保存了一个引用列表[索引]项目是否被选中与否，也提供了计数，</para>
    /// Maintains an additional "Selected"  "Count" value for each item in a List.
    /// Useful in the CheckBoxComboBox. It holds a reference to the List[Index] Item and 
    /// whether it is selected or not.
    /// It also caters for a Count, if needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListSelectionWrapper<T> : List<ObjectSelectionWrapper<T>>
    {
        #region 构造函数.

        /// <summary><para>对象没有属性被指定用于显示目的，就这么简单的ToString（）操作将被执行。</para>
        /// No property on the object is specified for display purposes, so simple ToString() operation 
        /// will be performed.
        /// </summary>
        public ListSelectionWrapper(IEnumerable source, bool showCounts = false) : base()
        {
            _Source = source;
            _ShowCounts = showCounts;
            if(_Source is IBindingList)
                ((IBindingList) _Source).ListChanged += new ListChangedEventHandler(ListSelectionWrapper_ListChanged);
            Populate();
        }

        /// <summary><para>显示名称“属性指定的ToString（）将不会被执行的项目，这是特别有用的DataTable中实现的PropertyDescriptor用于读取值的PropertyDescriptor如果没有找到，物业将用于</para>
        /// A Display "Name" property is specified. ToString() will not be performed on items.
        /// This is specifically useful on DataTable implementations, or where PropertyDescriptors are used to read the values.
        /// If a PropertyDescriptor is not found, a Property will be used.
        /// </summary>
        public ListSelectionWrapper(IEnumerable source, string usePropertyAsDisplayName) : this(source, false, usePropertyAsDisplayName)
        {
        }

        /// <summary><para>显示名称“属性指定的ToString（）将不会被执行的项目，这是特别有用的DataTable中实现的PropertyDescriptor用于读取值的PropertyDescriptor如果没有找到，物业将用于</para>
        /// A Display "Name" property is specified. ToString() will not be performed on items.
        /// This is specifically useful on DataTable implementations, or where PropertyDescriptors are used to read the values.
        /// If a PropertyDescriptor is not found, a Property will be used.
        /// </summary>
        public ListSelectionWrapper(IEnumerable source, bool showCounts, string usePropertyAsDisplayName) : this(source, showCounts)
        {
            _DisplayNameProperty = usePropertyAsDisplayName;
        }

        #endregion

        #region 私有字段

        /// <summary><para>是一个计数指标。</para>
        /// Is a Count indicator used.
        /// </summary>
        private bool _ShowCounts;

        /// <summary><para>原来包裹的值列表的选择“，可能是计数“功能。</para>
        /// The original List of values wrapped. A "Selected" and possibly "Count" functionality is added.
        /// </summary>
        private IEnumerable _Source;

        /// <summary><para>用来表示不使用ToString（），但读取此属性，而不是作为一个显示值。</para>
        /// Used to indicate NOT to use ToString(), but read this property instead as a display value.
        /// </summary>
        private string _DisplayNameProperty = null;

        #endregion

        #region 属性

        /// <summary><para>规定时，表示不应该执行项目上的ToString（），此属性将不是读。的PropertyDescriptor用于读取值DataTable中实现，这是特别有用的。</para>
        /// When specified, indicates that ToString() should not be performed on the items. 
        /// This property will be read instead. 
        /// This is specifically useful on DataTable implementations, where PropertyDescriptors are used to read the values.
        /// </summary>
        public string DisplayNameProperty
        {
            get { return _DisplayNameProperty; }
            set { _DisplayNameProperty = value; }
        }

        /// <summary>
        /// Builds a concatenation list of selected items in the list.
        /// </summary>
        public string SelectedNames
        {
            get
            {
                string text = "";
                foreach(ObjectSelectionWrapper<T> item in this)
                {
                    if(item.Selected)
                        text += (string.IsNullOrEmpty(text) ? String.Format("\"{0}\"", item.Name) : String.Format(" & \"{0}\"", item.Name));
                }
                return text;
            }
        }

        /// <summary><para>表示项目显示值（姓名）是否应包括计数。</para>
        /// Indicates whether the Item display value (Name) should include a count.
        /// </summary>
        public bool ShowCounts
        {
            get { return _ShowCounts; }
            set { _ShowCounts = value; }
        }

        #endregion

        #region 辅助方法

        /// <summary><para>重置所有计数为零。</para>
        /// Reset all counts to zero.
        /// </summary>
        public void ClearCounts()
        {
            foreach(ObjectSelectionWrapper<T> item in this)
                item.Count = 0;
        }

        /// <summary><para>创建一个ObjectSelectionWrapper项目，需要注意的是子类的构造函数签名是重要的。</para>
        /// Creates a ObjectSelectionWrapper item.
        /// Note that the constructor signature of sub classes classes are important.
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        private ObjectSelectionWrapper<T> CreateSelectionWrapper(IEnumerator Object)
        {
            Type[] types = new Type[] {typeof(T), this.GetType()};
            ConstructorInfo ci = typeof(ObjectSelectionWrapper<T>).GetConstructor(types);
            if(ci == null)
                throw new Exception(String.Format("The selection wrapper class {0} must have a constructor with ({1} Item, {2} Container) parameters.", typeof(ObjectSelectionWrapper<T>), typeof(T), this.GetType()));
            object[] parameters = new object[] {Object.Current, this};
            object result = ci.Invoke(parameters);
            return (ObjectSelectionWrapper<T>) result;
        }

        public ObjectSelectionWrapper<T> FindObjectWithItem(T Object)
        {
            return Find(new Predicate<ObjectSelectionWrapper<T>>(delegate(ObjectSelectionWrapper<T> target) { return target.Item.Equals(Object); }));
        }

        /*
        public TSelectionWrapper FindObjectWithKey(object key)
        {
            return FindObjectWithKey(new object[] { key });
        }

        public TSelectionWrapper FindObjectWithKey(object[] keys)
        {
            return Find(new Predicate<TSelectionWrapper>(
                            delegate(TSelectionWrapper target)
                            {
                                return
                                    ReflectionHelper.CompareKeyValues(
                                        ReflectionHelper.GetKeyValuesFromObject(target.Item, target.Item.TableInfo),
                                        keys);
                            }));
        }

        public object[] GetArrayOfSelectedKeys()
        {
            List<object> List = new List<object>();
            foreach (TSelectionWrapper Item in this)
                if (Item.Selected)
                {
                    if (Item.Item.TableInfo.KeyProperties.Length == 1)
                        List.Add(ReflectionHelper.GetKeyValueFromObject(Item.Item, Item.Item.TableInfo));
                    else
                        List.Add(ReflectionHelper.GetKeyValuesFromObject(Item.Item, Item.Item.TableInfo));
                }
            return List.ToArray();
        }

        public T[] GetArrayOfSelectedKeys<T>()
        {
            List<T> List = new List<T>();
            foreach (TSelectionWrapper Item in this)
                if (Item.Selected)
                {
                    if (Item.Item.TableInfo.KeyProperties.Length == 1)
                        List.Add((T)ReflectionHelper.GetKeyValueFromObject(Item.Item, Item.Item.TableInfo));
                    else
                        throw new LibraryException("This generator only supports single value keys.");
                    // List.Add((T)ReflectionHelper.GetKeyValuesFromObject(Item.Item, Item.Item.TableInfo));
                }
            return List.ToArray();
        }
        */

        private void Populate()
        {
            Clear();
            /*
            for(int Index = 0; Index <= _Source.Count -1; Index++)
                Add(CreateSelectionWrapper(_Source[Index]));
             */
            IEnumerator enumerator = _Source.GetEnumerator();
            while(enumerator.MoveNext())
                Add(CreateSelectionWrapper(enumerator));
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSelectionWrapper_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch(e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    Add(CreateSelectionWrapper((IEnumerator) ((IBindingList) _Source)[e.NewIndex]));
                    break;
                case ListChangedType.ItemDeleted:
                    Remove(FindObjectWithItem((T) ((IBindingList) _Source)[e.OldIndex]));
                    break;
                case ListChangedType.Reset:
                    Populate();
                    break;
            }
        }

        #endregion
    }
}