using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace CRC.Controls
{
    public partial class SwapSelectList : UserControl
    {
        public SwapSelectList()
        {
            InitializeComponent();
        }

        private ArrayList source = new ArrayList();
        private ArrayList dest = new ArrayList(); 

        private void AddElements(System.Windows.Forms.ListBox tempLst, ArrayList tempArray)
        {
                tempLst.Items.Clear();
                int intMaxElements = tempArray.Count; 
                object[] bunchOfStuff = new object[intMaxElements];
                bunchOfStuff = tempArray.ToArray();
                tempLst.Items.AddRange(bunchOfStuff);
                    
        }

        public void SetSourceData(ArrayList sourceArray)
        {
            source = sourceArray;
            AddElements(this.SourceLst, source);
        }

        public ArrayList GetDestData()
        {
            return dest;
        }

        public void SetDestData(ArrayList DestArray)
        {
            dest = DestArray;
            AddElements(this.DestList, dest);
        }

        public ArrayList GetSourceData()
        {
            return source;
        }

        public string ItemText
        {
            get
            {
                return this.groupBox1.Text;
            }
            set
            {
                this.groupBox1.Text = value;
            }
         }

        public string SourceLabel
        {
            get
            {
                return this.SrcLabel.Text;
            }
            set
            {
                this.SrcLabel.Text = value;
            }
        }

        public string DestinationLabel
        {
            get
            {
                return this.DestLabel.Text;
            }
            set
            {
                this.DestLabel.Text = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SourceLst.SelectedIndex != -1)
            {
                dest.Add(SourceLst.SelectedItem);
                AddElements(this.DestList, dest);
                source.Remove(SourceLst.SelectedItem);
                AddElements(this.SourceLst, source);
            }
          }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DestList.SelectedIndex != -1)
            {
                source.Add(DestList.SelectedItem);
                AddElements(this.SourceLst, source);
                dest.Remove(DestList.SelectedItem);
                AddElements(this.DestList, dest);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (object i in source)
            {
                dest.Add(i);
            }
            source.Clear();

            AddElements(this.SourceLst, source);
            AddElements(this.DestList, dest);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            foreach (object i in dest)
            {
                source.Add(i);

            }
            dest.Clear();

            AddElements(this.DestList, dest);
            AddElements(this.SourceLst, source);

        }

        private void SourceLst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SourceLst.SelectedIndex != -1)
            {
                dest.Add(SourceLst.SelectedItem);
                AddElements(this.DestList, dest);
                source.Remove(SourceLst.SelectedItem);
                AddElements(this.SourceLst, source);
            }
        }

        private void DestList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (DestList.SelectedIndex != -1)
            {
                source.Add(DestList.SelectedItem);
                AddElements(this.SourceLst, source);
                dest.Remove(DestList.SelectedItem);
                AddElements(this.DestList, dest);
            }

        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {

            this.SourceLst.Left = this.groupBox1.Left+10;
            this.SourceLst.Top = this.groupBox1.Top+40;
            this.SourceLst.Height = this.groupBox1.Height-50;
            this.SourceLst.Width = (this.groupBox1.Width/2) - 40;

            this.DestList.Left = this.groupBox1.Left + (this.groupBox1.Width / 2)+30;
            this.DestList.Top = this.groupBox1.Top + 40;
            this.DestList.Height = this.groupBox1.Height - 50;
            this.DestList.Width = (this.groupBox1.Width / 2) - 40;

            this.button1.Top = (this.groupBox1.Height - 110) / 2;
            this.button2.Top = this.button1.Top+30;
            this.button3.Top = this.button2.Top+30;
            this.button4.Top = this.button3.Top+30;

            this.SrcLabel.Left = this.groupBox1.Left + 10;
            this.SrcLabel.Top = this.groupBox1.Top + 20;

            this.DestLabel.Left = this.groupBox1.Left + (this.groupBox1.Width / 2) + 30;
            this.DestLabel.Top = this.groupBox1.Top + 20;
        }
    }
}