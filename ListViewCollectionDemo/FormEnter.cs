using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ListViewCollectionDemo
{
    public class FormEnter : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.NumericUpDown numericUpDownAge;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private string firstname;
        private string lastname;
        private string datatag;
        private int age = 0;
        private System.Windows.Forms.Label labelLastname;
        private System.Windows.Forms.Label labelFirstname;
        private System.Windows.Forms.TextBox textBoxFirstname;
        private System.Windows.Forms.TextBox textBoxLastname;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.Label labelTag;
		private System.ComponentModel.Container components = null;

		public FormEnter()
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.labelFirstname = new System.Windows.Forms.Label();
            this.textBoxFirstname = new System.Windows.Forms.TextBox();
            this.labelAge = new System.Windows.Forms.Label();
            this.numericUpDownAge = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxLastname = new System.Windows.Forms.TextBox();
            this.labelLastname = new System.Windows.Forms.Label();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.labelTag = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAge)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFirstname
            // 
            this.labelFirstname.Location = new System.Drawing.Point(14, 17);
            this.labelFirstname.Name = "labelFirstname";
            this.labelFirstname.Size = new System.Drawing.Size(68, 25);
            this.labelFirstname.TabIndex = 0;
            this.labelFirstname.Text = "Firstname:";
            // 
            // textBoxFirstname
            // 
            this.textBoxFirstname.Location = new System.Drawing.Point(96, 17);
            this.textBoxFirstname.Name = "textBoxFirstname";
            this.textBoxFirstname.Size = new System.Drawing.Size(144, 21);
            this.textBoxFirstname.TabIndex = 1;
            // 
            // labelAge
            // 
            this.labelAge.Location = new System.Drawing.Point(19, 95);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(39, 25);
            this.labelAge.TabIndex = 2;
            this.labelAge.Text = "Age:";
            // 
            // numericUpDownAge
            // 
            this.numericUpDownAge.Location = new System.Drawing.Point(96, 95);
            this.numericUpDownAge.Name = "numericUpDownAge";
            this.numericUpDownAge.Size = new System.Drawing.Size(144, 21);
            this.numericUpDownAge.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(54, 122);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(90, 25);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(150, 122);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 25);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            // 
            // textBoxLastname
            // 
            this.textBoxLastname.Location = new System.Drawing.Point(96, 43);
            this.textBoxLastname.Name = "textBoxLastname";
            this.textBoxLastname.Size = new System.Drawing.Size(144, 21);
            this.textBoxLastname.TabIndex = 7;
            // 
            // labelLastname
            // 
            this.labelLastname.Location = new System.Drawing.Point(14, 43);
            this.labelLastname.Name = "labelLastname";
            this.labelLastname.Size = new System.Drawing.Size(72, 25);
            this.labelLastname.TabIndex = 6;
            this.labelLastname.Text = "Lastname:";
            // 
            // textBoxTag
            // 
            this.textBoxTag.Location = new System.Drawing.Point(96, 69);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(144, 21);
            this.textBoxTag.TabIndex = 9;
            // 
            // labelTag
            // 
            this.labelTag.Location = new System.Drawing.Point(14, 69);
            this.labelTag.Name = "labelTag";
            this.labelTag.Size = new System.Drawing.Size(72, 25);
            this.labelTag.TabIndex = 8;
            this.labelTag.Text = "Tag:";
            // 
            // FormEnter
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(255, 158);
            this.Controls.Add(this.textBoxTag);
            this.Controls.Add(this.labelTag);
            this.Controls.Add(this.textBoxLastname);
            this.Controls.Add(this.textBoxFirstname);
            this.Controls.Add(this.labelLastname);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDownAge);
            this.Controls.Add(this.labelAge);
            this.Controls.Add(this.labelFirstname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormEnter";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.FormEnter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
           
            firstname = textBoxFirstname.Text;
            lastname = textBoxLastname.Text;
            datatag = textBoxTag.Text;
            age = (int)numericUpDownAge.Value;
        }

        private void FormEnter_Load(object sender, System.EventArgs e)
        {
            textBoxFirstname.Text = firstname;
            textBoxLastname.Text = lastname;
            textBoxTag.Text = datatag;
            numericUpDownAge.Text = age.ToString();
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public string Datatag
        {
            get
            {
                return datatag;
            }
            set
            {
                datatag = value;
            }
        }
	}
}
