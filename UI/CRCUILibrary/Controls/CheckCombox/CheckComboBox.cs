﻿ //Martin Lottering : 2007-10-27
 //--------------------------------
 //This is a usefull control in Filters. Allows you to save space and can replace a Grouped Box of CheckBoxes.
 //Currently used on the TasksFilter for TaskStatusses, which means the user can select which Statusses to include
 //in the "Search".
 //This control does not implement a CheckBoxListBox, instead it adds a wrapper for the normal ComboBox and Items. 
 //See the CheckBoxItems property.
 //----------------
 //ALSO IMPORTANT: In Data Binding when setting the DataSource. The ValueMember must be a bool type property, because it will 
 //be binded to the Checked property of the displayed CheckBox. Also see the DisplayMemberSingleItem for more information.
 //----------------
 //Extends the CodeProject PopupComboBox "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp"
 //by Lukasz Swiatkowski.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CRC.Controls
{
    /// <summary>
    /// 下拉Item为CheakBox 的ComboBox
    /// <para></para>
    /// </summary>
    public partial class CheckComboBox : PopupComboBox
    {
        #region CONSTRUCTOR

        public CheckComboBox()
            : base()
        {
            InitializeComponent();
            _CheckBoxProperties = new CheckBoxProperties();
            _CheckBoxProperties.PropertyChanged += new EventHandler(_CheckBoxProperties_PropertyChanged);
            // Dumps the ListControl in a(nother) Container to ensure the ScrollBar on the ListControl does not
            // Paint over the Size grip. Setting the Padding or Margin on the Popup or host control does
            // not work as I expected. I don't think it can work that way.
            CheckComboBoxListControlContainer ContainerControl = new CheckComboBoxListControlContainer();
            _CheckBoxComboBoxListControl = new CheckComboBoxListControl(this);
            _CheckBoxComboBoxListControl.Items.CheckBoxCheckedChanged += new EventHandler(Items_CheckBoxCheckedChanged);
            ContainerControl.Controls.Add(_CheckBoxComboBoxListControl);
            // This padding spaces neatly on the left-hand side and allows space for the size grip at the bottom.
            ContainerControl.Padding = new Padding(4, 0, 0, 14);
            // The ListControl FILLS the ListContainer.
            _CheckBoxComboBoxListControl.Dock = DockStyle.Fill;
            // The DropDownControl used by the base class. Will be wrapped in a popup by the base class.
            DropDownControl = ContainerControl;
            // Must be set after the DropDownControl is set, since the popup is recreated.
            // NOTE: I made the dropDown protected so that it can be accessible here. It was private.
            dropDown.Resizable = true;
        }

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// The checkbox list control. The public CheckBoxItems property provides a direct reference to its Items.
        /// </summary>
        private CheckComboBoxListControl _CheckBoxComboBoxListControl;
        /// <summary>
        /// In DataBinding operations, this property will be used as the DisplayMember in the CheckBoxComboBoxListBox.
        /// The normal/existing "DisplayMember" property is used by the TextBox of the ComboBox to display 
        /// a concatenated Text of the items selected. This concatenation and its formatting however is controlled 
        /// by the Binded object, since it owns that property.
        /// </summary>
        private string _DisplayMemberSingleItem = null;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// A direct reference to the Items of CheckBoxComboBoxListControl.
        /// You can use it to Get or Set the Checked status of items manually if you want.
        /// But do not manipulate the List itself directly, e.g. Adding and Removing, 
        /// since the list is synchronised when shown with the ComboBox.Items. So for changing 
        /// the list contents, use Items instead.
        /// </summary>
        [Browsable(false)]
        public CheckComboBoxItemList CheckBoxItems
        {
            get { return _CheckBoxComboBoxListControl.Items; }
        }
        /// <summary>
        /// The DataSource of the combobox. Refreshes the CheckBox wrappers when this is set.
        /// </summary>
        public new object DataSource
        {
            get { return base.DataSource; }
            set
            {
                base.DataSource = value;
                if (!string.IsNullOrEmpty(ValueMember))
                    // This ensures that at least the checkboxitems are available to be initialised.
                    _CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
            }
        }
        /// <summary>
        /// The ValueMember of the combobox. Refreshes the CheckBox wrappers when this is set.
        /// </summary>
        public new string ValueMember
        {
            get { return base.ValueMember; }
            set
            {
                base.ValueMember = value;
                if (!string.IsNullOrEmpty(ValueMember))
                    // This ensures that at least the checkboxitems are available to be initialised.
                    _CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
            }
        }
        /// <summary>
        /// In DataBinding operations, this property will be used as the DisplayMember in the CheckBoxComboBoxListBox.
        /// The normal/existing "DisplayMember" property is used by the TextBox of the ComboBox to display 
        /// a concatenated Text of the items selected. This concatenation however is controlled by the Binded 
        /// object, since it owns that property.
        /// </summary>
        public string DisplayMemberSingleItem
        {
            get { if (string.IsNullOrEmpty(_DisplayMemberSingleItem)) return DisplayMember; else return _DisplayMemberSingleItem; }
            set { _DisplayMemberSingleItem = value; }
        }

        /// <summary>
        /// Made this property Browsable again, since the Base Popup hides it. This class uses it again.
        /// Gets an object representing the collection of the items contained in this 
        /// System.Windows.Forms.ComboBox.
        /// </summary>
        /// <returns>A System.Windows.Forms.ComboBox.ObjectCollection representing the items in 
        /// the System.Windows.Forms.ComboBox.
        /// </returns>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new ObjectCollection Items
        {
            get { return base.Items; }
        }

        #endregion

        #region EVENTS & EVENT HANDLERS

        public event EventHandler CheckBoxCheckedChanged;

        private void Items_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            OnCheckBoxCheckedChanged(sender, e);
        }

        #endregion

        #region EVENT CALLERS and OVERRIDES e.g. OnResize()

        protected void OnCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (DropDownStyle != ComboBoxStyle.DropDownList)
            {
                string listText = "";
                foreach (CheckComboBoxItem item in _CheckBoxComboBoxListControl.Items)
                    if (item.Checked)
                        listText += string.IsNullOrEmpty(listText) ? item.Text : String.Format(", {0}", item.Text);
                Text = listText;
            }

            EventHandler handler = CheckBoxCheckedChanged;
            if (handler != null)
                handler(sender, e);
        }

        protected override void OnResize(EventArgs e)
        {
            // When the ComboBox is resized, the width of the dropdown 
            // is also resized to match the width of the ComboBox. I think it looks better.
            Size size = new Size(Width, DropDownControl.Height);
            dropDown.Size = size;
            base.OnResize(e);
        }

        #endregion

        #region HELPER MEMBERS

        /// <summary>
        /// 清理所有选中的Item
        /// </summary>
        public void ClearSelection()
        {
            foreach (CheckComboBoxItem item in CheckBoxItems)
                if (item.Checked)
                    item.Checked = false;
        }

        #endregion

        #region CHECKBOX PROPERTIES (DEFAULTS)

        private CheckBoxProperties _CheckBoxProperties;

        /// <summary>
        /// The properties that will be assigned to the checkboxes as default values.
        /// </summary>
        [Description("The properties that will be assigned to the checkboxes as default values.")]
        [Browsable(true)]
        public CheckBoxProperties CheckBoxProperties
        {
            get { return _CheckBoxProperties; }
            set { _CheckBoxProperties = value; _CheckBoxProperties_PropertyChanged(this, EventArgs.Empty); }
        }

        private void _CheckBoxProperties_PropertyChanged(object sender, EventArgs e)
        {
            foreach (CheckComboBoxItem item in CheckBoxItems)
                item.ApplyProperties(CheckBoxProperties);
        }

        #endregion
    }

    /// <summary>
    /// A container control for the ListControl to ensure the ScrollBar on the ListControl does not
    /// Paint over the Size grip. Setting the Padding or Margin on the Popup or host control does
    /// not work as I expected.
    /// </summary>
    [ToolboxItem(false)]
    public sealed class CheckComboBoxListControlContainer : UserControl
    {
        #region CONSTRUCTOR

        public CheckComboBoxListControlContainer()
            : base()
        {
            this.BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.FixedSingle;
            AutoScaleMode = AutoScaleMode.Inherit;
            ResizeRedraw = true;
            // If you don't set this, then resize operations cause an error in the base class.
            MinimumSize = new Size(1, 1);
            MaximumSize = new Size(500, 500);
        }
        #endregion

        #region RESIZE OVERRIDE REQUIRED BY THE POPUP CONTROL

        /// <summary>
        /// Prescribed by the Popup class to ensure Resize operations work correctly.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            Popup popup = Parent as Popup;
            if (popup != null && popup.ProcessResizing(ref m))
            {
                return;
            }
            base.WndProc(ref m);
        }
        #endregion
    }

    /// <summary>
    /// This ListControl that pops up to the User. It contains the CheckBoxComboBoxItems. 
    /// The items are docked DockStyle.Top in this control.
    /// </summary>
    [ToolboxItem(false)]
    public sealed class CheckComboBoxListControl : ScrollableControl
    {
        #region CONSTRUCTOR

        public CheckComboBoxListControl(CheckComboBox owner)
            : base()
        {
            DoubleBuffered = true;
            _CheckBoxComboBox = owner;
            BackColor = SystemColors.Window;
            // AutoScaleMode = AutoScaleMode.Inherit;
            AutoScroll = true;
            ResizeRedraw = true;
            // if you don't set this, a Resize operation causes an error in the base class.
            MinimumSize = new Size(1, 1);
            MaximumSize = new Size(500, 500);
        }

        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// Simply a reference to the CheckBoxComboBox.
        /// </summary>
        private readonly CheckComboBox _CheckBoxComboBox;
        /// <summary>
        /// A Typed list of ComboBoxCheckBoxItems.
        /// </summary>
        private readonly CheckComboBoxItemList _Items = new CheckComboBoxItemList();

        #endregion

        public CheckComboBoxItemList Items { get { return _Items; } }

        #region RESIZE OVERRIDE REQUIRED BY THE POPUP CONTROL

        /// <summary>
        /// Prescribed by the Popup control to enable Resize operations.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            Popup popup = Parent.Parent as Popup;
            if (popup != null && popup.ProcessResizing(ref m))
            {
                return;
            }
            base.WndProc(ref m);
        }

        #endregion

        #region PROTECTED MEMBERS

        protected override void OnVisibleChanged(EventArgs e)
        {
            // Synchronises the CheckBox list with the items in the ComboBox.
            SynchroniseControlsWithComboBoxItems();
            base.OnVisibleChanged(e);
        }
        /// <summary>
        /// Maintains the controls displayed in the list by keeping them in sync with the actual 
        /// items in the combobox. (e.g. removing and adding as well as ordering)
        /// </summary>
        public void SynchroniseControlsWithComboBoxItems()
        {
            SuspendLayout();
            Controls.Clear();
            #region Disposes all items that are no longer in the combo box list

            for (int index = _Items.Count - 1; index >= 0; index--)
            {
                CheckComboBoxItem item = _Items[index];
                if (!_CheckBoxComboBox.Items.Contains(item.ComboBoxItem))
                {
                    _Items.Remove(item);
                    item.Dispose();
                }
            }

            #endregion
            #region Recreate the list in the same order of the combo box items

            CheckComboBoxItemList newList = new CheckComboBoxItemList();
            foreach (object Object in _CheckBoxComboBox.Items)
            {
                CheckComboBoxItem item = _Items.Find(new Predicate<CheckComboBoxItem>(target => target.ComboBoxItem == Object));
                if (item == null)
                {
                    item = new CheckComboBoxItem(_CheckBoxComboBox, Object);
                    item.ApplyProperties(_CheckBoxComboBox.CheckBoxProperties);
                    // Item.TextAlign = ContentAlignment.MiddleCenter;
                }
                newList.Add(item);
                item.Dock = DockStyle.Top;
            }
            //NewList.CheckBoxCheckedChanged += _Items.CheckBoxCheckedChanged;
            _Items.Clear();
            _Items.AddRange(newList);

            #endregion
            #region Add the items to the controls in reversed order to maintain correct docking order

            if (newList.Count > 0)
            {
                // This reverse helps to maintain correct docking order.
                newList.Reverse();
                // If you get an error here that "Cannot convert to the desired type, it probably
                // means the controls are not binding correctly.
                // The Checked property is binded to the ValueMember property. It must be a bool for example.
                Controls.AddRange(newList.ToArray());
            }

            #endregion
            ResumeLayout();
        }

        #endregion
    }

    /// <summary>
    /// The CheckBox items displayed in the Popup of the ComboBox.
    /// </summary>
    [ToolboxItem(false)]
    public sealed class CheckComboBoxItem : CheckBox
    {
        #region CONSTRUCTOR

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner">A reference to the CheckBoxComboBox.</param>
        /// <param name="comboBoxItem">A reference to the item in the ComboBox.Items that this object is extending.</param>
        public CheckComboBoxItem(CheckComboBox owner, object comboBoxItem)
            : base()
        {
            DoubleBuffered = true;
            _CheckBoxComboBox = owner;
            _ComboBoxItem = comboBoxItem;
            if (_CheckBoxComboBox.DataSource != null)
                AddBindings();
            else
                Text = comboBoxItem.ToString();
        }
        #endregion

        #region PRIVATE PROPERTIES

        /// <summary>
        /// A reference to the CheckBoxComboBox.
        /// </summary>
        private readonly CheckComboBox _CheckBoxComboBox;
        /// <summary>
        /// A reference to the Item in ComboBox.Items that this object is extending.
        /// </summary>
        private readonly object _ComboBoxItem;

        #endregion

        #region PUBLIC PROPERTIES

        /// <summary>
        /// A reference to the Item in ComboBox.Items that this object is extending.
        /// </summary>
        public object ComboBoxItem
        {
            get { return _ComboBoxItem; }
        }

        #endregion

        #region BINDING HELPER

        /// <summary>
        /// When using Data Binding operations via the DataSource property of the ComboBox. This
        /// adds the required Bindings for the CheckBoxes.
        /// </summary>
        public void AddBindings()
        {
            // Note, the text uses "DisplayMemberSingleItem", not "DisplayMember" (unless its not assigned)
            DataBindings.Add(
                "Text",
                _ComboBoxItem,
                _CheckBoxComboBox.DisplayMemberSingleItem);
            // The ValueMember must be a bool type property usable by the CheckBox.Checked.
            DataBindings.Add(
                "Checked",
                _ComboBoxItem,
                _CheckBoxComboBox.ValueMember,
                false,
                // This helps to maintain proper selection state in the Binded object,
                // even when the controls are added and removed.
                DataSourceUpdateMode.OnPropertyChanged,
                false, null, null);
        }

        #endregion

        #region PROTECTED MEMBERS

        protected override void OnCheckedChanged(EventArgs e)
        {
            // Found that when this event is raised, the bool value of the binded item is not yet updated.
            if (_CheckBoxComboBox.DataSource != null)
            {
                PropertyInfo pi = ComboBoxItem.GetType().GetProperty(_CheckBoxComboBox.ValueMember);
                pi.SetValue(ComboBoxItem, Checked, null);
            }
            base.OnCheckedChanged(e);
            // Forces a refresh of the Text displayed in the main TextBox of the ComboBox,
            // since that Text will most probably represent a concatenation of selected values.
            // Also see DisplayMemberSingleItem on the CheckBoxComboBox for more information.
            if (_CheckBoxComboBox.DataSource != null)
            {
                string oldDisplayMember = _CheckBoxComboBox.DisplayMember;
                _CheckBoxComboBox.DisplayMember = null;
                _CheckBoxComboBox.DisplayMember = oldDisplayMember;
            }
        }

        #endregion

        #region HELPER MEMBERS

        internal void ApplyProperties(CheckBoxProperties properties)
        {
            this.Appearance = properties.Appearance;
            this.AutoCheck = properties.AutoCheck;
            this.AutoEllipsis = properties.AutoEllipsis;
            this.AutoSize = properties.AutoSize;
            this.CheckAlign = properties.CheckAlign;
            this.FlatAppearance.BorderColor = properties.FlatAppearanceBorderColor;
            this.FlatAppearance.BorderSize = properties.FlatAppearanceBorderSize;
            this.FlatAppearance.CheckedBackColor = properties.FlatAppearanceCheckedBackColor;
            this.FlatAppearance.MouseDownBackColor = properties.FlatAppearanceMouseDownBackColor;
            this.FlatAppearance.MouseOverBackColor = properties.FlatAppearanceMouseOverBackColor;
            this.FlatStyle = properties.FlatStyle;
            this.ForeColor = properties.ForeColor;
            this.RightToLeft = properties.RightToLeft;
            this.TextAlign = properties.TextAlign;
            this.ThreeState = properties.ThreeState;
        }

        #endregion
    }

    /// <summary>
    /// A Typed List of the CheckBox items.
    /// Simply a wrapper for the CheckBoxComboBox.Items. A list of CheckBoxComboBoxItem objects.
    /// This List is automatically synchronised with the Items of the ComboBox and extended to
    /// handle the additional boolean value. That said, do not Add or Remove using this List, 
    /// it will be lost or regenerated from the ComboBox.Items.
    /// </summary>
    [ToolboxItem(false)]
    public class CheckComboBoxItemList : List<CheckComboBoxItem>
    {
        #region EVENTS, This could be moved to the list control if needed

        public event EventHandler CheckBoxCheckedChanged;

        protected void OnCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            EventHandler handler = CheckBoxCheckedChanged;
            if (handler != null)
                handler(sender, e);
        }
        private void item_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckBoxCheckedChanged(sender, e);
        }

        #endregion

        #region LIST MEMBERS & OBSOLETE INDICATORS

        [Obsolete("Do not add items to this list directly. Use the ComboBox items instead.", false)]
        public new void Add(CheckComboBoxItem item)
        {
            item.CheckedChanged += new EventHandler(item_CheckedChanged);
            base.Add(item);
        }

        public new void AddRange(IEnumerable<CheckComboBoxItem> collection)
        {
            if(collection == null)
                return;
            foreach (CheckComboBoxItem item in collection)
                item.CheckedChanged += new EventHandler(item_CheckedChanged);
            base.AddRange(collection);
        }

        public new void Clear()
        {
            foreach (CheckComboBoxItem Item in this)
                Item.CheckedChanged -= item_CheckedChanged;
            base.Clear();
        }

        [Obsolete("Do not remove items from this list directly. Use the ComboBox items instead.", false)]
        public new bool Remove(CheckComboBoxItem item)
        {
            item.CheckedChanged -= item_CheckedChanged;
            return base.Remove(item);
        }

        #endregion
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CheckBoxProperties
    {
        public CheckBoxProperties() { }

        #region PRIVATE PROPERTIES

        private Appearance _Appearance = Appearance.Normal;
        private bool _AutoSize = false;
        private bool _AutoCheck = true;
        private bool _AutoEllipsis = false;
        private ContentAlignment _CheckAlign = ContentAlignment.MiddleLeft;
        private Color _FlatAppearanceBorderColor = Color.Empty;
        private int _FlatAppearanceBorderSize = 1;
        private Color _FlatAppearanceCheckedBackColor = Color.Empty;
        private Color _FlatAppearanceMouseDownBackColor = Color.Empty;
        private Color _FlatAppearanceMouseOverBackColor = Color.Empty;
        private FlatStyle _FlatStyle = FlatStyle.Standard;
        private Color _ForeColor = SystemColors.ControlText;
        private RightToLeft _RightToLeft = RightToLeft.No;
        private ContentAlignment _TextAlign = ContentAlignment.MiddleLeft;
        private bool _ThreeState = false;

        #endregion

        #region PUBLIC PROPERTIES

        [DefaultValue(Appearance.Normal)]
        public Appearance Appearance
        {
            get { return _Appearance; }
            set { _Appearance = value; OnPropertyChanged(); }
        }
        [DefaultValue(true)]
        public bool AutoCheck
        {
            get { return _AutoCheck; }
            set { _AutoCheck = value; OnPropertyChanged(); }
        }
        [DefaultValue(false)]
        public bool AutoEllipsis
        {
            get { return _AutoEllipsis; }
            set { _AutoEllipsis = value; OnPropertyChanged(); }
        }
        [DefaultValue(false)]
        public bool AutoSize
        {
            get { return _AutoSize; }
            set { _AutoSize = true; OnPropertyChanged(); }
        }
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment CheckAlign
        {
            get { return _CheckAlign; }
            set { _CheckAlign = value; OnPropertyChanged(); }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceBorderColor
        {
            get { return _FlatAppearanceBorderColor; }
            set { _FlatAppearanceBorderColor = value; OnPropertyChanged(); }
        }
        [DefaultValue(1)]
        public int FlatAppearanceBorderSize
        {
            get { return _FlatAppearanceBorderSize; }
            set { _FlatAppearanceBorderSize = value; OnPropertyChanged(); }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceCheckedBackColor
        {
            get { return _FlatAppearanceCheckedBackColor; }
            set { _FlatAppearanceCheckedBackColor = value; OnPropertyChanged(); }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceMouseDownBackColor
        {
            get { return _FlatAppearanceMouseDownBackColor; }
            set { _FlatAppearanceMouseDownBackColor = value; OnPropertyChanged(); }
        }
        [DefaultValue(typeof(Color), "")]
        public Color FlatAppearanceMouseOverBackColor
        {
            get { return _FlatAppearanceMouseOverBackColor; }
            set { _FlatAppearanceMouseOverBackColor = value; OnPropertyChanged(); }
        }
        [DefaultValue(FlatStyle.Standard)]
        public FlatStyle FlatStyle
        {
            get { return _FlatStyle; }
            set { _FlatStyle = value; OnPropertyChanged(); }
        }
        [DefaultValue(typeof(SystemColors), "ControlText")]
        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; OnPropertyChanged(); }
        }
        [DefaultValue(RightToLeft.No)]
        public RightToLeft RightToLeft
        {
            get { return _RightToLeft; }
            set { _RightToLeft = value; OnPropertyChanged(); }
        }
        [DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; OnPropertyChanged(); }
        }
        [DefaultValue(false)]
        public bool ThreeState
        {
            get { return _ThreeState; }
            set { _ThreeState = value; OnPropertyChanged(); }
        }

        #endregion

        #region EVENTS AND EVENT CALLERS

        /// <summary>
        /// Called when any property changes.
        /// </summary>
        public event EventHandler PropertyChanged;

        protected void OnPropertyChanged()
        {
            EventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}
