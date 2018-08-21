using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using Manina.Windows.Forms;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;

namespace VideoRecordings.ChannelGrouping
{
    public partial class Grouping : Form
    {
        private MyGroup showgroups;
        private List<GalleryGroup> galleries;
        private string _selectgroup = string.Empty;
        private bool IsExpand = true;
        public Grouping()
        {
            InitializeComponent();
            InitializeComboBox();
        }


        private List<string> listOnit_city = new List<string>();
        private List<string> listNew_city = new List<string>();

        private List<string> listOnit__street = new List<string>();
        private List<string> listNew__street = new List<string>();

        private List<string> listOnit_site = new List<string>();
        private List<string> listNew_site = new List<string>();

        private List<string> listOnit_uid = new List<string>();
        private List<string> listNew__uid = new List<string>();

        private void InitializeComboBox()
        {
            showgroups = MyGroup.GetGetGroups();          
            SetShowTree();
            SetShowGroups();
            SetCityComboBox();
        }

        private void SetShowTree()
        {
            if (showgroups == null) return;
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

        private void SetShowGroups()
        {
            galleries = GroupData.GetAllGalleryGroup();
            if (galleries == null) return;
            treeList2.BeginUpdate();
            treeList2.Nodes.Clear();
            TreeListNode selectnode = new TreeListNode();
            foreach (GalleryGroup item in galleries.OrderByDescending(t => t.Id))
            {
                if (item.Id == 0) continue;
                TreeListNode ParentNode = treeList2.AppendNode(null, null);
                ParentNode.SetValue(treeList2.Columns[0], item.Id + ":" + item.Name);
                ParentNode.Tag = item.Id;
                if (_selectgroup?.Split(':').Last() == item.Name)
                {
                    selectnode = ParentNode;
                }
                foreach (EquipmentInfo equip in item.Equipments)
                {
                    TreeListNode tree = ParentNode.Nodes.Add("");
                    tree.SetValue(treeList1.Columns[0], equip.Id + ":" + equip.Name);
                    tree.Tag = equip.Id;
                }
            }
            if (_selectgroup == string.Empty && treeList2.Nodes.Count > 0)
                selectnode = treeList2.Nodes[0];
            treeList2.EndUpdate();
            treeList2.ExpandAll();
            IsExpand = true;
            treeList2.FocusedNode = selectnode;
        }

        #region  设置选择条件

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



        #endregion


        /// <summary>
        /// 方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void AddgroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddGroup addGroup = new AddGroup();
            addGroup.MySaveEvent += new AddGroup.MyEvent(SetShowGroups);
            addGroup.ShowDialog();
        }

        private void deletegroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show($"是否删除{treeList2.FocusedNode.GetValue(0)}的分组？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!MyGroup.DeleteGroup(int.Parse(treeList2.FocusedNode.Tag.ToString())))
            {
                MessageBox.Show("删除分组失败");
                return;
            }
            SetShowGroups();
        }

        private List<TreeListNode> GetGroupCheckNode()
        {
            List<TreeListNode> nodes = new List<TreeListNode>();
            foreach (TreeListNode tree in treeList2.Nodes)
            {
                if (tree.Checked) nodes.Add(tree);
                foreach (TreeListNode node in tree.Nodes)
                {
                    if (node.Checked)
                    {
                        nodes.Add(node);
                    }
                }
            }
            return nodes;
        }

        private List<TreeListNode> GetEquipmentCheckNode()
        {
            List<TreeListNode> nodes = new List<TreeListNode>();
            foreach (TreeListNode tree in treeList1.Nodes)
            {
                if (tree.Checked) nodes.Add(tree);
            }
            return nodes;
        }

        private void updategroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeList2.FocusedNode == null) return;
            UpdateGroup updateGroup = new UpdateGroup(treeList2.FocusedNode);
            updateGroup.MySaveEvent += new UpdateGroup.MyEvent(SetShowGroups);
            updateGroup.ShowDialog();
        }

        private void DeleteEqipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeListNode> nodes = GetGroupCheckNode();
            if (nodes.Count == 0 )
            {
                return;
            }
            Dictionary<int, List<int>> trees = new Dictionary<int, List<int>>();
            foreach (TreeListNode node in nodes)
            {
                if (node.Level == 0) continue;
                if (!trees.Keys.Contains(int.Parse(node.ParentNode.Tag.ToString())))
                {
                    trees.Add(int.Parse(node.ParentNode.Tag.ToString()), new List<int>());
                }
                trees[int.Parse(node.ParentNode.Tag.ToString())].Add(int.Parse(node.Tag.ToString()));
            }

            foreach (var item in trees.Keys)
            {
                if (!MyGroup.DeleteEquipmentToGroup(item, trees[item]))
                {
                    MessageBox.Show($"{item}:分组移除失败");
                }
            }
            SetShowGroups();
        }

        private void addtogroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeListNode> nodes = GetEquipmentCheckNode();
            if (treeList2.FocusedNode == null || treeList2.FocusedNode.Level != 0)
            {
                MessageBox.Show("请选中需要添加的分组");
                return;
            }
            List<int> ids = nodes.Select(t => int.Parse(t.Tag.ToString())).ToList();
            if (!MyGroup.AddEquipmentToGroup(int.Parse(treeList2.FocusedNode.Tag.ToString()), ids))
            {
                MessageBox.Show("添加失败");
                return;
            }

            InitializeComboBox();
        }



        /// <summary>
        /// 选中勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            treeList1.FocusedNode.Checked = !treeList1.FocusedNode.Checked;

            SetShowImage(int.Parse(treeList1.FocusedNode.Tag.ToString()));
        }

        private void treeList2_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (treeList2.FocusedNode == null || treeList2.Nodes.Count == 0)
            {
                return;
            }
            if (treeList2.FocusedNode.Level == 0)
            {
                _selectgroup = e.Node.GetValue(0).ToString();
                return;
            }
            treeList2.FocusedNode.Checked = !treeList2.FocusedNode.Checked;
            SetShowImage(int.Parse(treeList2.FocusedNode.Tag.ToString()));
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

        //
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void RefreshAll()
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

        private void DelEqiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TreeListNode> nodes = GetEquipmentCheckNode();
            if (nodes.Count == 0) return;
            if (MessageBox.Show(
                    $"删除通道将清除通道所属视频,是否删除\r\n{string.Join(",\r\n", nodes.Select(t => t.GetValue(0).ToString()).ToList())}\r\n共{nodes.Count()}个通道",
                    "删除提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!EquipmentData.DeleteEquipmengt(nodes.Select(t=> int.Parse(t.Tag.ToString())).ToList()))
            {
                MessageBox.Show($"删除失败");
            }

            RefreshAll();

        }

        private void OPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsExpand)
            {
                treeList2.CollapseAll();
                IsExpand = false;
                return;
            }
            treeList2.ExpandAll();
            IsExpand = true;
        }

        public void SetShowImage(int id)
        {
            int image_id = EquipmentData.GetImageId(id);
            SetTheListView(image_id);
        }

        /// <summary>
        /// 控件加载图片
        /// </summary>
        public void SetTheListView(int id)
        {
            string imageUrl = Program.Urlpath + $"/video/snapshot/{id}";
            imageListView1.Items.Clear();
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            imageListView1.ThumbnailSize = new Size(260, 210);
                ImageListViewItem item = new ImageListViewItem(imageUrl) { Text = imageUrl.Split('/').Last() };
            imageListView1.Items.Add(item);
        }
    }
}
