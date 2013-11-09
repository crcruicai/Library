using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRC;
namespace UILibrary
{
    public partial class FrmAutoDockManage : Form
    {
        AutoDockManage _AutoDock;
        public FrmAutoDockManage()
        {
            _AutoDock = new AutoDockManage();
            _AutoDock.DockForm = this;
            _AutoDock.IsOpen = true;
            
            InitializeComponent();
        }
    }
}
