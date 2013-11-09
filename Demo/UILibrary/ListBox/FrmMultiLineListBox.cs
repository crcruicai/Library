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
    public partial class FrmMultiLineListBox : Form
    {
        public FrmMultiLineListBox()
        {
            InitializeComponent();
            Load();
        }


        private void Load()
        {
            string s = "There was this guy from Trivandrum " +
                "who wanted to travel all round the world. " +
                "But this guy from Trivandrum could not manage " +
                "to do that and he's heart broken. All the queen's " +
                "soldiers and all the queen's men " +
                "couldn't put his poor heart together again";

            multiLineListBox1.Items.Add(s);
            multiTextListBox1.Items.Add(s);
            s = "Colin Davies, James Johnson, Jack Handy, Shog9, " +
                "Roger Wright, Roger Allen, PJ, Nnamdi, Mike Dunn, " +
                "Christian Graus, Nish, Cathy, Smitha, Lauren, " +
                "Kannan Kalyanaraman, Simon Walton, Isaac Sasson, " +
                "Matt Newman, Paul Watson, Andrew Peace - these fellows " +
                "are often found on Bob's HungOut";

            multiLineListBox1.Items.Add(s);
            multiTextListBox1.Items.Add(s);
            s = "Chris Maunder and David Cunningham run the Code Project web site";

            multiLineListBox1.Items.Add(s);
            multiTextListBox1.Items.Add(s);
            s = "I thoth I tho a puthy cath. I thoth I tho another puthy cath. " +
                "I deed. I deed tho a puthy cath and I deed tho another puthy cath";

            multiLineListBox1.Items.Add(s);
            multiTextListBox1.Items.Add(s);
            s = "I believe I can fly, I believe I can touch the sky. " +
                "I think about it every night and day, spread my wings " +
                "and I fly away. I belive I can fly.";

            for (int i = 0; i < 3; i++)
            {
                multiLineListBox1.Items.Add(s);
                multiTextListBox1.Items.Add(s);
            }

        }

    }
}
