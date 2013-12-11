/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/30 15:38:57
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CWebQQ.UI
{
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
            button1.Click += new EventHandler(button1_Click);
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (SearchClick != null)
            {
                SearchClick(button1, e);
            }
        }

        public event EventHandler SearchClick;

        public ComboBox Box
        {
            get
            {
                return comboBox1;
            }
        }
      



    }
}
