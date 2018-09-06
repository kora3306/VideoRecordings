using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.IO;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace VideoRecordings
{
    public partial class ManagementLabel : DevExpress.XtraEditors.XtraForm
    {
        MyLabels AllLabels;
        TreeListHitInfo hInfo1 = new TreeListHitInfo();
        TreeListHitInfo hInfo2 = new TreeListHitInfo();
        private bool isExpand = true;
        private bool isExpand1 = false;
        string onlyStr = string.Empty;
        public ManagementLabel()
        {
            InitializeComponent();
        }

        private void ManagementLabel_Load(object sender, EventArgs e)
        {
            GetLabels(RefreshType.None);
            toolStripStatusLabel1.Text = "动态标签节点显示: 红色为关联车,蓝色为关联人";
        }

        /// <summary>
        /// 获取标签集合加入选择标签栏
        /// </summary>
        public void GetLabels(RefreshType type)
        {
            AllLabels = new MyLabels();
            switch (type)
            {
                case RefreshType.DynamicLabel:
                    SetDynamicLabel();
                    break;
                case RefreshType.StaticLabel:
                    SetStaticLabel();
                    break;
                case RefreshType.None:
                    SetDynamicLabel();
                    SetStaticLabel();
                    break;
            }
        }

        public void SetStaticLabel()
        {
            treeList1.BeginUpdate();
            treeList1.Nodes.Clear();
            foreach (TypeLabel item in AllLabels.StaticLabel.OrderByDescending(t => t.Id))
            {
                if (item.Id == 0) continue;
                TreeListNode ParentNode = treeList1.AppendNode(null, null);
                ParentNode.SetValue(treeList1.Columns[0], item.Id + ":" + item.Name);
                ParentNode.Tag = item;
                foreach (VideoLabel equip in item.Labels)
                {
                    TreeListNode tree = ParentNode.Nodes.Add("");
                    tree.SetValue(treeList1.Columns[0], equip.Id + ":" + equip.Name);
                    tree.Tag = equip.Id;
                }
            }
            treeList1.EndUpdate();
            treeList1.ExpandAll();
            isExpand = true;
        }

        public void SetDynamicLabel() 
        {
            treeList2.BeginUpdate();
            treeList2.Nodes.Clear();
            List<TypeLabel> showlabel = AllLabels.DynamicLabel;
            if(!string.IsNullOrEmpty(onlyStr))
            {
                int id = AllLabels.GetSelectIds(onlyStr);
                showlabel = AllLabels.DynamicLabel.Where(t => t.Ref != id).ToList();
            }
            foreach (TypeLabel item in showlabel.OrderByDescending(t => t.Id))
            {
                if (item.Id == 0) continue;
                TreeListNode ParentNode = treeList2.AppendNode(null, null);
                ParentNode.SetValue(treeList2.Columns[0], item.Id + ":" + item.Name);
                ParentNode.Tag = item;
                foreach (VideoLabel equip in item.Labels)
                {
                    TreeListNode tree = ParentNode.Nodes.Add("");
                    tree.SetValue(treeList2.Columns[0], equip.Id + ":" + equip.Name);
                    tree.Tag = equip.Id;
                }
            }
            treeList2.EndUpdate();
            treeList2.CollapseAll();
            isExpand1 = false;
        }

        /// <summary>
        /// 更改名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeListNode node = new TreeListNode();
            RefreshType type = RefreshType.None;
            if (treeList1.Focused)
            {
                if (treeList1.FocusedNode == null) return;
                node = treeList1.FocusedNode;
                type = RefreshType.StaticLabel;
            }
            else
            {
                if (treeList2.FocusedNode == null) return;
                node = treeList2.FocusedNode;
                type = RefreshType.DynamicLabel;
            }
            UpdateLabel update = new UpdateLabel(node.GetValue(0).ToString(),type);
            update.MyRefreshEvent += new UpdateLabel.MyEvent(GetLabels);
            update.Show();
        }

        private void DelLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelOneLabel();
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        private void DelOneLabel()
        {
            RefreshType type = RefreshType.None;
            if(treeList1.Focused&&treeList1.FocusedNode!=null)
            {
                DeleteShowMessage(treeList1);
                type = RefreshType.StaticLabel;
            }
            else if (treeList2.Focused&&treeList2.FocusedNode!=null)
            {
                DeleteShowMessage(treeList2);
                type = RefreshType.DynamicLabel;
            }          
            GetLabels(type);
        }

        public List<TreeListNode> GetChecked()//返回所有被选中的节点
        {
            List<TreeListNode> nodes = new List<TreeListNode>();
            foreach (TreeListNode tree in treeList1.Nodes)
            {
                if (tree.Checked)
                {
                    nodes.AddRange(tree.Nodes);
                    continue;
                }

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

        //展开
        private void OPenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isExpand)
            {
                treeList1.CollapseAll();
                isExpand = false;
                return;
            }

            treeList1.ExpandAll();
            isExpand = true;
        }

        private void AddLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = string.Empty;
            RefreshType type = RefreshType.None;
            if (treeList1.Focused&&treeList1.FocusedNode.Level==0)
            {
                text = treeList1.FocusedNode.GetValue(0).ToString();
                type = RefreshType.StaticLabel;
            }
            else if (treeList2.Focused&&treeList2.FocusedNode.Level==0)
            {
                text = treeList2.FocusedNode.GetValue(0).ToString();
                type = RefreshType.DynamicLabel;
            }
            AddLabel addLabel = new AddLabel(type,text);
            addLabel.MyRefreshEvent += new AddLabel.MyEvent(GetLabels);
            addLabel.Show();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyStr = string.Empty;
            GetLabels(RefreshType.None);
        }

        /// <summary>
        /// 删除提示
        /// </summary>
        /// <param name="tree"></param>
        private void DeleteShowMessage(TreeList tree)
        {
            if (tree.FocusedNode.Level == 0)
            {
                if (MessageBox.Show($"删除<{tree.FocusedNode.GetValue(0)}>将删除下面的所有子标签,是否删除？", "提示",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return ;
            }
            else
            {
                if (MessageBox.Show($"删除<{tree.FocusedNode.GetValue(0)}>标签,是否删除？", "提示",
                        MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return ;
            }
            if (!LabelData.DeleteLabel(int.Parse(tree.FocusedNode.GetValue(0).ToString().Split(':').First())))
            {
                MessageBox.Show("删除失败");
                return;
            }
        }

        /// <summary>
        /// 关联类型(车/人)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLabelRef(AllLabels.GetSelectIds(carToolStripMenuItem.Text));
        }

        private void personToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLabelRef(AllLabels.GetSelectIds(personToolStripMenuItem.Text));
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLabelRef(0);
        }


        /// <summary>
        /// 更改标签类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void staticlabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLabelType(true);
        }

        private void dnlabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLabelType(false);
        }

        /// <summary>
        /// 只显示关联人或者车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onlycarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyStr = "人";
            SetDynamicLabel();
        }

        private void onlypersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlyStr = "车";
            SetDynamicLabel();
        }


        /// <summary>
        /// 设置选中即勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_Click(object sender, EventArgs e)
        {
            if (hInfo1.InRowCell)
            {
                if (treeList1.FocusedNode == null) return;
                treeList1.FocusedNode.Checked = !treeList1.FocusedNode.Checked;
            }

        }

        private void treeList2_Click(object sender, EventArgs e)
        {
            if (hInfo2.InRowCell)
            {
                if (treeList2.FocusedNode == null) return;
                treeList2.FocusedNode.Checked = !treeList2.FocusedNode.Checked;
            }
 
        }


        /// <summary>
        ///  返回选中的节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private List<TreeListNode> GetcheckList(TreeList tree,bool level=false)
        {
            List<TreeListNode> level0 = new List<TreeListNode>();
            List<TreeListNode> level1 = new List<TreeListNode>();
            foreach (TreeListNode item in tree.Nodes)
            {
                if (item.Checked) level0.Add(item);
                foreach (TreeListNode it in item.Nodes)
                {
                    if (it.Checked) level1.Add(it);
                }
            }
            if (level) return level1;
            return level0;
        }
        /// <summary>
        /// 更改动态静态标签
        /// </summary>
        /// <param name="isstatic"></param>
        private void SetLabelType(bool isstatic)
        {
            List<TreeListNode> nodes = new List<TreeListNode>();
            if (treeList1.Focused)
                nodes = GetcheckList(treeList1);
            else
                nodes = GetcheckList(treeList2);
            List<int> ids = GetTypeLabels(nodes).Select(t => t.Id).ToList();
            if (!LabelData.SetLabelType(ids, isstatic))
                MessageBox.Show("更改类型失败");
            GetLabels(RefreshType.None);
        }

        /// <summary>
        /// 得到选中接的的tag
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<TypeLabel> GetTypeLabels(List<TreeListNode> nodes)
        {
            List<TypeLabel> typeLabels = new List<TypeLabel>();
            foreach (TreeListNode item in nodes)
            {
                TypeLabel label = (TypeLabel)item.Tag;
                typeLabels.Add(label);
            }
            return typeLabels;
        }

        /// <summary>
        /// 设置关联
        /// </summary>
        /// <param name="id"></param>
        private void SetLabelRef(int id)
        {
            List<TreeListNode> nodes = GetcheckList(treeList2);
            List<int> ids = nodes.Select(t => int.Parse(t.GetValue(0).ToString().Split(':').First())).ToList();
            if (!LabelData.RelevanceLabel(ids, id))
                MessageBox.Show("设置关联失败");
            GetLabels(RefreshType.DynamicLabel);
        }

        /// <summary>
        /// 关联变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList2_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeListNode node= e.Node;
            if (node.Level == 0)
            {
                TypeLabel typeLabel = (TypeLabel)node.Tag;
                if (typeLabel.Ref == AllLabels.GetSelectIds("车"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else if (typeLabel.Ref == AllLabels.GetSelectIds("人"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                return;
            }
        }

        /// <summary>
        /// 判断点击的位置是不是数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList2_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo2 = treeList2.CalcHitInfo(new Point(e.X,e.Y));
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo1 = treeList1.CalcHitInfo(new Point(e.X, e.Y));
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opendoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isExpand1)
            {
                treeList2.CollapseAll();
                isExpand1 = false;
                return;
            }
            treeList2.ExpandAll();
            isExpand1 = true;
        }
    }

    public enum RefreshType
    {
        DynamicLabel,
        StaticLabel,
        None
    }
}