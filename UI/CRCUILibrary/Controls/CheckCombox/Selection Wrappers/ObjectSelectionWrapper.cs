using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Data;

namespace CRC.Controls
{
    /// <summary>
    /// Used together with the ListSelectionWrapper in order to wrap data sources for a CheckBoxComboBox.
    /// It helps to ensure you don't add an extra "Selected" property to a class that don't really need or want that information.
    /// </summary>
    public class ObjectSelectionWrapper<T>
    {
        public ObjectSelectionWrapper(T item, ListSelectionWrapper<T> container)
            : base()
        {
            _Container = container;
            _Item = item;
        }


        #region PRIVATE PROPERTIES

        /// <summary>
        /// Used as a count indicator for the item. Not necessarily displayed.
        /// </summary>
        private int _Count = 0;
        /// <summary>
        /// Is this item selected.
        /// </summary>
        private bool _Selected = false;
        /// <summary>
        /// A reference to the wrapped item.
        /// </summary>
        private T _Item;
        /// <summary>
        /// The containing list for these selections.
        /// </summary>
        private ListSelectionWrapper<T> _Container;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// An indicator of how many items with the specified status is available for the current filter level.
        /// Thaught this would make the app a bit more user-friendly and help not to miss items in Statusses
        /// that are not often used.
        /// </summary>
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
        /// <summary>
        /// A reference to the item wrapped.
        /// </summary>
        public T Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        /// <summary>
        /// The item display value. If ShowCount is true, it displays the "Name [Count]".
        /// </summary>
        public string Name
        {
            get 
            {
                string name = null;
                if (string.IsNullOrEmpty(_Container.DisplayNameProperty))
                    name = Item.ToString();
                else if (Item is DataRow) // A specific implementation for DataRow
                    name = ((DataRow)((Object)Item))[_Container.DisplayNameProperty].ToString();
                else
                {
                    PropertyDescriptorCollection pds = TypeDescriptor.GetProperties(Item);
                    foreach (PropertyDescriptor pd in pds)
                        if (pd.Name.CompareTo(_Container.DisplayNameProperty) == 0)
                        {
                            var value = pd.GetValue(Item);
                            if(value != null)
                                name = (string)value.ToString();
                            break;
                        }
                    if(!string.IsNullOrEmpty(name))
                        return _Container.ShowCounts ? String.Format("{0} [{1}]", name, Count) : name;
                    PropertyInfo pi = Item.GetType().GetProperty(_Container.DisplayNameProperty);
                    if (pi == null)
                        throw new Exception(String.Format(
                            "Property {0} cannot be found on {1}.",
                            _Container.DisplayNameProperty,
                            Item.GetType()));
                    name = pi.GetValue(Item, null).ToString();
                }
                return _Container.ShowCounts ? String.Format("{0} [{1}]", name, Count) : name;
            }
        }
        /// <summary>
        /// The textbox display value. The names concatenated.
        /// </summary>
        public string NameConcatenated
        {
            get { return _Container.SelectedNames; }
        }
        /// <summary>
        /// Indicates whether the item is selected.
        /// </summary>
        public bool Selected
        {
            get { return _Selected; }
            set { _Selected = value; }
        }

        #endregion
    }
}
