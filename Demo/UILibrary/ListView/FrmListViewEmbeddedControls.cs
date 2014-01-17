/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 17:00:54
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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UILibrary
{
    public partial class FrmListViewEmbeddedControls : Form
    {
        public FrmListViewEmbeddedControls()
        {
            InitializeComponent();
        }


        private void b_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Thank you!");
        }

        // Once an embedded ProgressBar is clicked, it's removed from the ListView.
        // This way the ListViewSubItem's Text value becomes Visible.
        private void pb_Click(object sender, EventArgs e)
        {
            listViewEmbeddedControls1.RemoveEmbeddedControl(sender as Control);
        }

        // Update embedded ProgressBars
        private Random rnd = new Random();
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            int row = rnd.Next(listViewEmbeddedControls1.Items.Count);

            ProgressBar pb = listViewEmbeddedControls1.GetEmbeddedControl(1, row) as ProgressBar;
            if (pb == null)
            {
                int val = int.Parse(listViewEmbeddedControls1.Items[row].SubItems[1].Text);
                val += 5;
                if (val > 100)
                    val = 0;

                listViewEmbeddedControls1.Items[row].SubItems[1].Text = val.ToString();
                return;
            }

            if (pb.Value >= pb.Maximum - 5)
                pb.Value = pb.Minimum;
            else
                pb.Value += 5;

            listViewEmbeddedControls1.Items[row].SubItems[1].Text = pb.Value.ToString();
        }


        // Switch ListView View
        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            View v = (View)Enum.Parse(typeof(View), comboBox1.Text, true);
            listViewEmbeddedControls1.View = v;
        }

        // Sort ListView
        private void listViewEmbeddedControls1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            switch (listViewEmbeddedControls1.Sorting)
            {
                case SortOrder.None:
                case SortOrder.Ascending:
                    listViewEmbeddedControls1.Sorting = SortOrder.Descending;
                    break;
                case SortOrder.Descending:
                    listViewEmbeddedControls1.Sorting = SortOrder.Ascending;
                    break;
            }
        }

        private void FrmListViewEmbeddedControls_Load(object sender, EventArgs e)
        {
            // Create some controls and embed them in our ListView

            // First, a button:
            Button b = new Button();
            b.Text = "ClickMe";
            b.BackColor = SystemColors.Control;
            b.Font = this.Font;
            b.Click += new EventHandler(b_Click);
            // Put it in the first column of the fourth row
            listViewEmbeddedControls1.AddEmbeddedControl(b, 0, 3);

            // Third, a number of ProcessBars that will be updated by a _Timer
            Random r = new Random();
            foreach (ListViewItem i in listViewEmbeddedControls1.Items)
            {
                int cnt = r.Next(100);
                i.SubItems.Add(cnt.ToString());

                ProgressBar pb = new ProgressBar();
                pb.Value = cnt;
                pb.Click += new EventHandler(pb_Click);

                // Embed the ProgressBar in Column 2
                listViewEmbeddedControls1.AddEmbeddedControl(pb, 1, i.Index);
            }

            // Fill the View ComboBox
            Array a = Enum.GetValues(typeof(View));
            foreach (View v in a)
            {
                comboBox1.Items.Add(v.ToString());
            }
            // Default view is Details
            comboBox1.Text = View.Details.ToString();
        }
    }
}
