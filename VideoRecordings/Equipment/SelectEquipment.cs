using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dal;
using DevExpress.XtraTreeList.Nodes;
using VideoRecordings.Models;
using VideoRecordings.GetDatas;
using Manina.Windows.Forms;
using VideoRecordings.Video;

namespace VideoRecordings.Equipment
{
    public partial class SelectEquipment : Form
    {

        private MyGroup showgroups;
        private List<GalleryGroup> galleries;
        private List<int> postids;

        public List<int> PostIds
        {
            get { return postids; }
            set { postids = value; }
        }

        private List<string> listOnit_city = new List<string>();
        private List<string> listNew_city = new List<string>();

        private List<string> listOnit__street = new List<string>();
        private List<string> listNew__street = new List<string>();

        private List<string> listOnit_site = new List<string>();
        private List<string> listNew_site = new List<string>();

        private List<string> listOnit_uid = new List<string>();
        private List<string> listNew__uid = new List<string>();

        public SelectEquipment()
        {
            InitializeComponent();
            InitializeComboBox();
            this.Location = new Point(800,100);
        }


        private void InitializeComboBox()
        {
            showgroups = MyGroup.GetGetGroups();
            SetShowTree();
            SetCityComboBox();
        }

        private void SetShowTree()
        {
            treeList1.Nodes.Clear();
            foreach (var item in showgroups.Equipments.OrderByDescending(t => t.Id))
            {
                TreeListNode ParentNode = treeList1.AppendNode(item.Name, null);//添加第一节点
                ParentNode.SetValue(treeList1.Columns[0], item.Id + ":" + item.Name);//添加第一节点显示的值
                ParentNode.Tag = item.Id;
            }
            if (treeList1.FocusedNode == null) return;
            int image_id = EquipmentData.GetImageId(int.Parse(treeList1.FocusedNode.Tag.ToString()));
            SetTheListView(image_id);
        }

        public delegate void MyEvent();
        public event MyEvent MySaveEvent;
        public void OnSave()
        {
            MySaveEvent?.Invoke();
        }

        public event MyEvent MyRefreshEvent;

        public void OnRefresh()
        {
            MyRefreshEvent?.Invoke();
        }

        /// <summary>
        /// 设置comboBox
        /// </summary>
        private void SetCityComboBox()
        {
            comboBox_city.Items.Clear();
            listOnit_city = showgroups.Cities;
            comboBox_city.Items.AddRange(listOnit_city.ToArray());
        }

        private void SetStreetComboBox()
        {
            comboBox_street.Items.Clear();
            listOnit__street = showgroups.Streets;
            comboBox_street.Items.AddRange(listOnit__street.ToArray());
        }

        private void SetSiteComboBox()
        {
            comboBox_site.Items.Clear();
            listOnit_site = showgroups.Sites;
            comboBox_site.Items.AddRange(listOnit_site.ToArray());
        }

        private void SetUidComboBox()
        {
            comboBox_uid.Items.Clear();
            listOnit_uid = showgroups.Uids;
            comboBox_uid.Items.AddRange(listOnit_uid.ToArray());
        }

        /// <summary>
        /// 更改选择条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            showgroups = GroupData.GetGroupShows(comboBox_city.Text);
            SetShowTree();
            SetStreetComboBox();
            comboBox_street.Text = string.Empty;
            comboBox_site.Text = string.Empty;
            comboBox_site.Items.Clear();
            comboBox_uid.Text = string.Empty;
            comboBox_uid.Items.Clear();
        }

        private void comboBox_street_SelectedIndexChanged(object sender, EventArgs e)
        {
            showgroups = GroupData.GetGroupShows(comboBox_city.Text, comboBox_street.Text);
            SetShowTree();
            SetSiteComboBox();
            comboBox_site.Text = string.Empty;
            comboBox_uid.Text = string.Empty;
            comboBox_uid.Items.Clear();
        }

        private void comboBox_site_SelectedIndexChanged(object sender, EventArgs e)
        {
            showgroups = GroupData.GetGroupShows(comboBox_city.Text, comboBox_street.Text, comboBox_site.Text);
            SetShowTree();
            SetUidComboBox();
            comboBox_uid.Text = string.Empty;
        }

        private void comboBox_uid_SelectedIndexChanged(object sender, EventArgs e)
        {
            showgroups = GroupData.GetGroupShows(comboBox_city.Text, comboBox_street.Text, comboBox_site.Text, comboBox_uid.Text);
            SetShowTree();
        }

        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_city_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_city, listOnit_city, listNew_city);
        }

        private void comboBox_street_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_street, listOnit__street, listNew__street);
        }

        private void comboBox_site_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_site, listOnit_site, listNew_site);
        }

        private void comboBox_uid_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_uid, listOnit_uid, listNew__uid);
        }

        private void SetComboxBox(ComboBox com, List<string> listOnit, List<string> listNew)
        {
            List<string> first = listOnit.Where(t => t.Contains(com.Text)).ToList();
            if (first.Count == 0)
            {
                return;
            }
            try
            {
                com.Items.Clear();
                listNew.Clear();
                foreach (var item in listOnit)
                {
                    if (item.Contains(com.Text))
                    {
                        listNew.Add(item);
                    }
                }
                com.Items.AddRange(listNew.ToArray());
                com.SelectionStart = com.Text.Length;
                Cursor = Cursors.Default;
                com.DroppedDown = true;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null||PostIds.Count==0) return;
            if (!EquipmentData.AddVideoInEquipment(int.Parse(treeList1.FocusedNode.Tag.ToString()), PostIds))
            {
                MessageBox.Show("添加视频到通道失败");
                return;
            }
            OnRefresh();
            OnSave();       
            this.Hide();
        }

        private void SelectEquipment_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void RefreshData()
        {
            showgroups = GroupData.GetGroupShows(comboBox_city.Text, comboBox_street.Text, comboBox_site.Text, comboBox_uid.Text);
            SetShowTree();
        }

        private void RefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void button_ref_Click(object sender, EventArgs e)
        {
            InitializeComboBox();
            comboBox_city.Text = string.Empty;
            comboBox_street.Text = string.Empty;
            comboBox_street.Items.Clear();
            comboBox_site.Text = string.Empty;
            comboBox_site.Items.Clear();
            comboBox_uid.Text = string.Empty;
            comboBox_uid.Items.Clear();
        }

        /// <summary>
        /// 控件加载图片
        /// </summary>
        public void SetTheListView(int id)
        {
            string imageUrl = Program.Urlpath + $"/video/snapshot/{id}";
            imageListView1.Items.Clear();
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            imageListView1.ThumbnailSize = new Size(290, 180);
            ImageListViewItem item = new ImageListViewItem(imageUrl) { Text = imageUrl.Split('/').Last() };
            imageListView1.Items.Add(item);
        }

        private void treeList1_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null) return;
            int image_id = EquipmentData.GetImageId(int.Parse(treeList1.FocusedNode.Tag.ToString()));
            SetTheListView(image_id);
        }

        private void ADDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEquipment equipment = new AddEquipment(comboBox_city.Text,comboBox_street.Text,comboBox_site.Text);
            equipment.MySaveEvent += new AddEquipment.MyDelegate(RefreshData);
            equipment.Show();
        }

        private void imageListView1_DoubleClick(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }
    }
}
