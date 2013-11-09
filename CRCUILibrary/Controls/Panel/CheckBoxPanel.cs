using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace CRC.Controls
{

    /// <summary>	
    /// <para>CheckBoxPanel功能:使用一个CheckBox来控制该Panel中其他控件,自动设置他们的属性,Enable.</para>
    /// <para>控件的用法:选择该控件中的一部CheckBox作为控制对象,当他处于选中时,其他控件的Enable=true.否则为false.</para>
    /// A special panel that links one or more controls to a checkbox option. When the checkbox is
    /// checked, the controls in the panel will be enabled; when it's unchecked, they will all be
    /// disabled. Clicking in any of the controls will check the CheckBox and enable all the controls
    /// ready for editing. The master checkbox is the checkbox that is nearest the top of the panel
    /// (and if several are at the same height, the leftmost of those). Alternatively, you can link
    /// the master control explicitly to any checkbox in the form designer.
    /// 
    /// If (other than the checkbox) the panel contains only TextBox controls, then set
    /// AutoDisableBlankFields and if the focus is lost from the panel when the fields are empty, the
    /// checkbox will automatically be unticked and the fields disabled. 
    /// </summary>
    ///
    /// <remarks>	Jason Williams, 25/3/2008. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [DefaultProperty("BorderStyle")]
    [Docking(DockingBehavior.Ask)]
    [DefaultEvent("Paint")]
    [ClassInterface(1)]
    [ComVisible(true)]

    public class CheckBoxPanel : Panel
    {
        #region --- Initialisation -------------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>	Default Constructor. </summary>
        ///
        /// <remarks>	Jason Williams, 25/3/2008. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public CheckBoxPanel()
        {
            mAutoDisableBlankFields = true;

            InitializeComponent();
            BindEvents();

            this.Click += new EventHandler(CheckBoxPanel_Click);
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        #endregion
        #endregion Initialisation

        #region --- Event Handling -------------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>	Unbinds all event handlers for the MasterControl. </summary>
        ///
        /// <remarks>	Jason Williams, 25/3/2008. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void UnbindEvents()
        {
            if (mMasterControl != null)
                mMasterControl.CheckStateChanged -= new EventHandler(mMasterControl_CheckedChanged);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>	Binds all event handlers for the MasterControl. </summary>
        ///
        /// <remarks>	Jason Williams, 25/3/2008. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected void BindEvents()
        {
            // Find the Master control. Ticking this control enables the other controls in the panel
            if (mMasterControl == null)
            {
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        // The top-left-most CheckBox controls the shading of the other controls
                        if (mMasterControl == null || ctrl.Top < mMasterControl.Top ||
                            (ctrl.Top == mMasterControl.Top && ctrl.Left < mMasterControl.Left))
                        {
                            mMasterControl = (CheckBox)ctrl;
                        }
                    }
                }
            }

            if (mMasterControl != null)
            {
                mMasterControl.CheckedChanged += new EventHandler(mMasterControl_CheckedChanged);

                // Call the CheckedChanged handler directly to set the intial state of the controls
                mMasterControl_CheckedChanged(null, null);
            }

            if (mAutoDisableBlankFields)
            {
                // See if this contains only text fields - if it does, when they are all empty we untick the master control
                bool containsOnlyTextFields = true;
                foreach (Control ctrl in Controls)
                {
                    if (ctrl is TextBox || (ctrl is CheckBox && ctrl.Equals(mMasterControl)))
                    {
                        // Ignore TextBoxes and the master control
                    }
                    else
                    {
                        containsOnlyTextFields = false;
                        break;
                    }
                }

                if (containsOnlyTextFields)
                {
                    foreach (Control ctrl in Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            TextBox textCtrl = (TextBox)ctrl;
                            textCtrl.LostFocus += new EventHandler(TextCtrl_LostFocus);
                        }
                    }
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>	
        /// Event handler. Called by TextCtrl for lost focus events. Where the panel contains only
        /// text boxes, if they are all blank and the focus has left the panel, the master checkbox is
        /// unticked and all text fields become disabled when they lose the focus. That is, if the user
        /// activates the panel and then leaves it without actually entering any information, we disable
        /// the activated option again. 
        /// </summary>
        ///
        /// <remarks>	Jason Williams, 25/3/2008. </remarks>
        ///
        /// <param name="sender">	Source of the event. </param>
        /// <param name="e">		Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void TextCtrl_LostFocus(object sender, EventArgs e)
        {
            if (mMasterControl == null)
                return;

            foreach (Control ctrl in Controls)
            {
                TextBox textCtrl = ctrl as TextBox;
                if (textCtrl != null)
                {
                    if (textCtrl.Focused ||						// If the focus moved to another textbox in our panel, remain checked
                        !string.IsNullOrEmpty(textCtrl.Text))	// If a text box is non-empty, leave the option checked
                    {
                        return;
                    }
                }
            }

            // All text fields in the panel are blank, so we untick our checkbox
            mMasterControl.Checked = false;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>	
        /// Event handler. Called by MasterControl for checked changed events. Enables/disables the other
        /// child controls of the Panel based on the new check state. 
        /// </summary>
        ///
        /// <remarks>	Jason Williams, 25/3/2008. </remarks>
        ///
        /// <param name="sender">	Source of the event. </param>
        /// <param name="e">		Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void mMasterControl_CheckedChanged(object sender, EventArgs e)
        {
            if (mMasterControl == null)
                return;

            // Checked state has changed for the master checkbox. Enable/disable all child items
            foreach (Control ctrl in Controls)
            {
                if (!ctrl.Equals(mMasterControl))
                    ctrl.Enabled = mMasterControl.Checked;
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>	
        /// Event handler. Called by the CheckBoxPanel for click events. When the master CheckBox is
        /// unticked, we disable all child controls, and clicks on them fall through to this method. We
        /// interpret a click on any child control as a desire to enable the controls to edit their
        /// contents, and hence we tick the checkbox. 
        /// </summary>
        ///
        /// <remarks>	Jason Williams, 25/3/2008. </remarks>
        ///
        /// <param name="sender">	Source of the event. </param>
        /// <param name="e">		Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void CheckBoxPanel_Click(object sender, EventArgs e)
        {
            if (mMasterControl != null && !mMasterControl.Checked)
            {
                Point pos = PointToClient(System.Windows.Forms.Control.MousePosition);
                Control child = GetChildAtPoint(pos, GetChildAtPointSkip.None);
                if (child != null)
                {
                    mMasterControl.Checked = true;
                    child.Focus();
                }
            }
        }

        #endregion Event Handling

        #region --- Properties and Member variables --------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary><para>获取或设置控制这个Panel的CheckBox.CheckBox启用时,这个Panel的其他控件的Enable=true.</para>
        /// Gets or sets the master CheckBox that controls the children of this Panel. </summary>
        ///
        /// <value>	The master control. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Category("LinkedControls")]
        [Description("获取或设置控制这个Panel的CheckBox.CheckBox启用时,这个Panel的其他控件的Enable=true.")]
        public CheckBox MasterControl
        {
            get { return (mMasterControl); }
            set { UnbindEvents(); mMasterControl = value; BindEvents(); }
        }

        protected CheckBox mMasterControl;


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// <para>获取或设置选项，在AutoDisable时，焦点离开面板和所有的文本框是空白的，主复选框取消选中自动</para>
        /// Gets or sets the AutoDisable option. When on, if the focus leaves the panel and all textboxes
        /// are blank, the master checkbox is unticked automatically. 
        /// </summary>
        ///
        /// <value>	true if this will automatically disable blank fields, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets the AutoDisable option. When on, if the focus leaves the panel and all textboxes are blank, the master checkbox is unticked automatically")]
        public bool AutoDisableBlankFields
        {
            get { return (mAutoDisableBlankFields); }
            set { mAutoDisableBlankFields = value; }
        }

        protected bool mAutoDisableBlankFields;

        #endregion Properties and Member variables
    }
}