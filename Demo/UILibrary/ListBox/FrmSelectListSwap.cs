using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace UILibrary
{
    public partial class FrmSelectListSwap : Form
    {
        public FrmSelectListSwap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArrayList Players = new ArrayList();
            Players.Add("Sachin Tendulkar");
            Players.Add("Virendra Sehwag");
            Players.Add("Rahul Dravid");
            Players.Add("Sourav Ganguly");
            Players.Add("Yuvaraj Singh");
            Players.Add("V V S Lakshman");
            Players.Add("Goutham Gambhir");
            Players.Add("Mahendra Singh Dhoni");
            Players.Add("Irfan Pathan");
            Players.Add("Zaheer Khan");
            Players.Add("Harbhajan Singh");
            Players.Add("Anil Kumble");
            Players.Add("Murali Karthik");
            Players.Add("Piyush Chavla");
            Players.Add("Ishanth Sharma");
            Players.Add("Praveen Kumar");
            Players.Add("Dinesh Karthik");
            Players.Add("Robin Uthappa");
            Players.Add("Rohith Sharma");
            Players.Add("Mohammad Kaif");

            this.userControl11.SetSourceData(Players);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList SelectedPlayers = new ArrayList();
            SelectedPlayers = this.userControl11.GetDestData();
            string result = "" ;
            foreach (string i in SelectedPlayers)
            {
                result += i + "::::";
            }
            MessageBox.Show(result);
        }
    }
}