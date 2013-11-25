/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/22 20:41:52
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListViewCollectionDemo
{
    public partial class FrmEditListView : Form
    {
        private Control[] Editors;

        public FrmEditListView()
        {
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            listViewEx1.SubItemClicked += new CRC.Controls.SubItemEventHandler(listViewEx1_SubItemClicked);
            listViewEx1.SubItemEndEditing += new CRC.Controls.SubItemEndEditingEventHandler(listViewEx1_SubItemEndEditing);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // Fill combo
            comboBox1.Items.AddRange(new string[] { "Peter", "Paul", "Mary", "Jack", "Betty" });

            // Add Columns
            listViewEx1.Columns.Add("Birthday", 80, HorizontalAlignment.Left);
            listViewEx1.Columns.Add("Name", 50, HorizontalAlignment.Left);
            listViewEx1.Columns.Add("Note", 80, HorizontalAlignment.Left);
            listViewEx1.Columns.Add("Password", 60, HorizontalAlignment.Left);
            listViewEx1.Columns.Add("Height", 60, HorizontalAlignment.Left);

            ListViewItem lvi;

            // Create sample ListView data.
            lvi = new ListViewItem("01.02.1964");
            lvi.SubItems.Add("Peter");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("****");	// This is what's displayed in the password column
            lvi.Tag = "pwd1";			// and that's the real password
            lvi.SubItems.Add("180");
            this.listViewEx1.Items.Add(lvi);

            lvi = new ListViewItem("12.04.1980");
            lvi.SubItems.Add("Jack");
            lvi.SubItems.Add("Hates sushi");
            lvi.SubItems.Add("****");
            lvi.Tag = "pwd2";
            lvi.SubItems.Add("185");
            this.listViewEx1.Items.Add(lvi);

            lvi = new ListViewItem("02.06.1976");
            lvi.SubItems.Add("Paul");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("****");
            lvi.Tag = "pwd3";
            lvi.SubItems.Add("172");
            this.listViewEx1.Items.Add(lvi);

            lvi = new ListViewItem("09.01.2000");
            lvi.SubItems.Add("Betty");
            lvi.SubItems.Add("");
            lvi.SubItems.Add("****");
            lvi.Tag = "pwd4";
            lvi.SubItems.Add("165");
            this.listViewEx1.Items.Add(lvi);

            Editors = new Control[] {
									dateTimePicker1,	// for column 0
									comboBox1,			// for column 1
									textBoxComment,		// for column 2
									textBoxPassword,	// for column 3
									numericUpDown1		// for column 4
									};

            // Immediately accept the new value once the value of the control has changed
            // (for example, the dateTimePicker and the comboBox)
            dateTimePicker1.ValueChanged += new EventHandler(control_SelectedValueChanged);
            comboBox1.SelectedIndexChanged += new EventHandler(control_SelectedValueChanged);

            listViewEx1.DoubleClickActivation = true;
        }

        private void control_SelectedValueChanged(object sender, System.EventArgs e)
        {
            listViewEx1.EndEditing(true);
        }

        private void listViewEx1_SubItemClicked(object sender, CRC.Controls.SubItemEventArgs e)
        {
            if (e.SubItem == 3) // Password field
            {
                // the current value (text) of the subitem is ****, so we have to provide
                // the control with the actual text (that's been saved in the item's Tag property)
                e.Item.SubItems[e.SubItem].Text = e.Item.Tag.ToString();
            }

            listViewEx1.StartEditing(Editors[e.SubItem], e.Item, e.SubItem);
        }

        private void listViewEx1_SubItemEndEditing(object sender, CRC.Controls.SubItemEndEditingEventArgs e)
        {
            if (e.SubItem == 3) // Password field
            {
                if (e.Cancel)
                {
                    e.DisplayText = new string(textBoxPassword.PasswordChar, e.Item.Tag.ToString().Length);
                }
                else
                {
                    // in order to display a series of asterisks instead of the plain password text
                    // (textBox.Text _gives_ plain text, after all), we have to modify what'll get
                    // displayed and save the plain value somewhere else.
                    string plain = e.DisplayText;
                    e.DisplayText = new string(textBoxPassword.PasswordChar, plain.Length);
                    e.Item.Tag = plain;
                }
            }
        }

        private void checkBoxDoubleClickActivation_CheckedChanged(object sender, System.EventArgs e)
        {
            listViewEx1.DoubleClickActivation = checkBoxDoubleClickActivation.Checked;
        }

        private void listViewEx1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // To show the real password (remember, the subitem's Text _is_ '*******'),
            // set the tooltip to the ListViewItem's tag (that's where the password is stored)
            ListViewItem item;
            int idx = listViewEx1.GetSubItemAt(e.X, e.Y, out item);
            if (item != null && idx == 3)
                toolTip1.SetToolTip(listViewEx1, item.Tag.ToString());
            else
                toolTip1.SetToolTip(listViewEx1, null);
        }
    }
}
