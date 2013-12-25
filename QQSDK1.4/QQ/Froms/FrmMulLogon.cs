using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CWebQQ.Data;

namespace CWebQQ
{
    public partial class FrmMulLogon : Form
    {
        private Manager _QQManager;

        public FrmMulLogon(Manager qqManager)
        {
            InitializeComponent();
            _QQManager = qqManager;
        }

        public FrmMulLogon()
        {
            InitializeComponent();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Person item = listView1.SelectedItems[0].Tag  as Person;
                if (!item.IsLogin)
                {
                    item=_QQManager.Login(item);
                    if(item.IsLogin )
                        listView1 .SelectedItems [0].SubItems[2].Text="在线" ;
                }
            }
        }

        private Person Login(Person item)
        {
            if ((item = _QQManager.Login(item)) != null)
            {
                ListViewItem litem = new ListViewItem(item.QQ);
                litem.SubItems.Add(item.NikeName);
                litem.SubItems.Add(item.State);
                litem.ImageIndex = item.PictureIndex;
                litem.Tag = item;
                litem.Checked = true;
                listView1.Items.Add(litem);
            }
            return item;
        }

        private void FrmMulLogon_Load(object sender, EventArgs e)
        {
            LoadQQList();
        }

        /// <summary>
        /// 加载所有QQ列表.
        /// </summary>
        private void LoadQQList()
        {

           
            //listView1.LargeImageList = PublicSouces.Settings.QQIamgeList;
            foreach (var item in _QQManager.PersonList)
            {
                ListViewItem litem = new ListViewItem(item.QQ);
                litem.SubItems.Add(item.NikeName);
                if (item.IsLogin)
                {
                    litem.SubItems.Add(item.State);
                }
                else
                {
                    litem.SubItems.Add("离线");
                }

                litem.ImageIndex = item.PictureIndex;
                litem.Tag = item;
                litem.Checked = item.IsLogin;
                listView1.Items.Add(litem);
            }


        }

        private void buttonLogon_Click(object sender, EventArgs e)
        {
            //if (_QQManager.QQMap.Count == 3) buttonLogon.Enabled = false;
            Login(null);
            //UpdateListView();
        }

        private void UpdateListView(Person item)
        {
            //if (PublicSouces.Settings.PersonList.Contains(item))
            //{
            //    ListViewItem[] items=listView1.Items.Find(item.QQ,true);
            //    if (items.Length > 0)
            //    {
            //        items[0].ImageIndex = item.PictureIndex;
            //    }
            //    else
            //    {
            //        ListViewItem litem = new ListViewItem(item.QQ);
            //        litem.ImageIndex = item.PictureIndex;
            //        litem.Tag = item;
            //        litem.SubItems.Add(item.NikeName);
            //        litem.SubItems.Add(item.State);
            //        listView1.Items.Add(item.QQ);
            //    }
            //}
            //else
            //{
            //    if (item != null)
            //    {
            //        ListViewItem litem = new ListViewItem(item.QQ);
            //        litem.ImageIndex = item.PictureIndex;
            //        litem.Tag = item;
            //        litem.SubItems.Add(item.NikeName);
            //        litem.SubItems.Add(item.State);
            //        listView1.Items.Add(item.QQ);
            //    }
            //}
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                Person person = listView1.SelectedItems[0].Tag as Person;
                _QQManager.PersonList.Remove(person);
                listView1.Items.Remove(listView1.SelectedItems[0]);

            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            
            if (listView1.SelectedItems.Count > 0)
            {
                //ListViewItem litem= listView1.SelectedItems[0];
                Person person = listView1.SelectedItems[0].Tag as Person;
                if (person != null)
                {
                    //UpdateListView(PublicSouces.Mangers.LoginQQ(person));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person item = new Person();
            item.QQ = "398117542";
            item.Password = "crcruicai392759";
            item.State = "在线";
            item.PictureIndex = 0;
            ListViewItem litem = new ListViewItem(item.QQ);
            litem.ImageIndex = item.PictureIndex;
            litem.Tag = item;
            listView1.Items.Add(litem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //PublicSouces.Settings.QQIamgeList.Images.Add(WebQQ.Properties.Resources.HighPriority);
            //PublicSouces.Settings.QQIamgeList.Images.Add(WebQQ.Properties.Resources.LowPriority);

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonAutoLogin_Click(object sender, EventArgs e)
        {
            Person item;

            for (int i = 0; i < _QQManager.PersonList.Count; i++)
            {
                item = _QQManager.PersonList[i];
                item = _QQManager.AutoLogin(item, (image) =>
                    {
                        string num = "";
                        FrmInputCode code = new FrmInputCode(image);
                        if (code.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            num = code.InputText;
                        }
                        code.Close();
                        return num;
                    });
                if (item.IsLogin)
                {
                    listView1.Items[0].SubItems[2].Text = "在线";
                }

            }
        }



        

    }
}
