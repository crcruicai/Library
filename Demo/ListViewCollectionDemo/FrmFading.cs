using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using CRC.Controls;

namespace ListViewCollectionDemo
{
	public class FrmFading : System.Windows.Forms.Form
	{
        private FadingListView fadingListView;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.ColumnHeader columnHeaderAge;
        private System.Windows.Forms.ColumnHeader columnHeaderFirstname;
        private System.Windows.Forms.ColumnHeader columnHeaderLastname;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.ComponentModel.IContainer components = null;

		public FrmFading()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            this.fadingListView = new CRC.Controls.FadingListView();
            this.columnHeaderFirstname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.labelTag = new System.Windows.Forms.Label();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fadingListView
            // 
            this.fadingListView.AddColor = System.Drawing.Color.Red;
            this.fadingListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fadingListView.BackColor = System.Drawing.SystemColors.Window;
            this.fadingListView.ChangeColor = System.Drawing.Color.Green;
            this.fadingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFirstname,
            this.columnHeaderLastname,
            this.columnHeaderAge});
            this.fadingListView.DeleteColor = System.Drawing.Color.Blue;
            this.fadingListView.FadingTime = 10;
            this.fadingListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fadingListView.FullRowSelect = true;
            this.fadingListView.Location = new System.Drawing.Point(10, 9);
            this.fadingListView.MultiSelect = false;
            this.fadingListView.Name = "fadingListView";
            this.fadingListView.Size = new System.Drawing.Size(296, 202);
            this.fadingListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.fadingListView.TabIndex = 0;
            this.fadingListView.UseCompatibleStateImageBehavior = false;
            this.fadingListView.View = System.Windows.Forms.View.Details;
            this.fadingListView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
            // 
            // columnHeaderFirstname
            // 
            this.columnHeaderFirstname.Text = "Firstname";
            this.columnHeaderFirstname.Width = 95;
            // 
            // columnHeaderLastname
            // 
            this.columnHeaderLastname.Text = "Lastname";
            this.columnHeaderLastname.Width = 93;
            // 
            // columnHeaderAge
            // 
            this.columnHeaderAge.Text = "Age";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(4, 249);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(90, 25);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(112, 249);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(90, 25);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Location = new System.Drawing.Point(220, 249);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(90, 25);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // labelTag
            // 
            this.labelTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTag.Location = new System.Drawing.Point(19, 219);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(48, 17);
            this.labelTag.TabIndex = 4;
            this.labelTag.Text = "Tag:";
            // 
            // textBoxTag
            // 
            this.textBoxTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTag.Location = new System.Drawing.Point(77, 217);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.ReadOnly = true;
            this.textBoxTag.Size = new System.Drawing.Size(224, 21);
            this.textBoxTag.TabIndex = 5;
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(320, 285);
            this.Controls.Add(this.textBoxTag);
            this.Controls.Add(this.labelTag);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.fadingListView);
            this.MinimumSize = new System.Drawing.Size(336, 302);
            this.Name = "FormMain";
            this.Text = "Fading ListView Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

	

        private string RandomString(int seed)
        {
            StringBuilder builder = new StringBuilder();

            Random random = new Random(seed);
            char ch;
            for (int i = 0; i < random.Next(10) + 5; i++)
            {
                if (i == 0)
                {
                    ch = Convert.ToChar(Convert.ToInt32(25 * random.NextDouble() + 65)) ;
                }
                else
                {
                    ch = Convert.ToChar(Convert.ToInt32(25 * random.NextDouble() + 97)) ;
                }
                builder.Append(ch);
            }

            return builder.ToString();
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            FormEnter form = new FormEnter();
            form.StartPosition = FormStartPosition.CenterParent;
            form.Text = "Add";

            Random random = new Random();
            form.Firstname = RandomString(unchecked((int)DateTime.Now.Ticks / 7));
            form.Lastname = RandomString(unchecked((int)DateTime.Now.Ticks / 5));
            form.Datatag = RandomString(unchecked((int)DateTime.Now.Ticks / 3));
            form.Age = random.Next(100);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem listViewItem = new ListViewItem(new string[]{form.Firstname, form.Lastname, form.Age.ToString()});

                fadingListView.AddItem(listViewItem);

                FadingListView.SetTag(listViewItem, form.Datatag);
            }
        }

        private void buttonDelete_Click(object sender, System.EventArgs e)
        {
            ListViewItem listViewItem = fadingListView.SelectedItems[0];
            fadingListView.DeleteItem(listViewItem);

            textBoxTag.Clear();
        }

        private void buttonEdit_Click(object sender, System.EventArgs e)
        {
            FormEnter form = new FormEnter();
            form.StartPosition = FormStartPosition.CenterParent;
            form.Text = "Edit";

            ListViewItem listViewItem = fadingListView.SelectedItems[0];
            form.Firstname = listViewItem.SubItems[0].Text;
            form.Lastname = listViewItem.SubItems[1].Text;
            form.Datatag = (string)FadingListView.GetTag(listViewItem);
            form.Age = int.Parse(listViewItem.SubItems[2].Text);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                listViewItem.SubItems[0].Text = form.Firstname;
                listViewItem.SubItems[1].Text = form.Lastname;
                listViewItem.SubItems[2].Text = form.Age.ToString();
                FadingListView.SetTag(listViewItem, form.Datatag);

                fadingListView.ChangeItem(listViewItem);

                textBoxTag.Text = form.Datatag;
            }
        }

        private void ListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            buttonDelete.Enabled = fadingListView.SelectedIndices.Count == 1;
            buttonEdit.Enabled = fadingListView.SelectedIndices.Count == 1;

            if (fadingListView.SelectedIndices.Count == 1)
            {
                textBoxTag.Text = (string)FadingListView.GetTag(fadingListView.SelectedItems[0]);
            }
        }
	}
}
