using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRC.Controls;

namespace ComboboxDemo
{
    public partial class FrmColor: Form
    {
        public FrmColor()
        {
            InitializeComponent();
        }

        protected void OnColorChanged(object sender, ColorChangeArgs e)
        {
            MessageBox.Show(this,e.color.ToString(),"Selected color",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

    }
}
