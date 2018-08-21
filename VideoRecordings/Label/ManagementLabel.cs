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

namespace VideoRecordings
{
    public partial class ManagementLabel : DevExpress.XtraEditors.XtraForm
    {
        List<TypeLabel> AllLabels;
        private bool isExpand = true;
        public ManagementLabel()
        {
            InitializeComponent();
        }

        private void ManagementLabel_Load(object sender, EventArgs e)
        {
            GetLabels();
        }

        /// <summary>
        /// 获取标签集合加入选择标签栏
        /// </summary>
        public void GetLabels()
        {
            AllLabels = LabelData.GetAllLabel();
            treeList1.BeginUpdate();
            treeList1.Nodes.Clear();
            foreach (TypeLabel item in AllLabels.OrderByDescending(t => t.Id))
            {
                if (item.Id == 0) continue;
                TreeListNode ParentNode = treeList1.AppendNode(null, null);
                ParentNode.SetValue(treeList1.Columns[0], item.Id + ":" + item.Name);
                ParentNode.Tag = item.Id;
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

    
        private void UpLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null || treeList1.FocusedNode.Level == 0) return;
            UpdateLabel update=  new UpdateLabel(int.Parse(treeList1.FocusedNode.Tag.ToString()));
            update.MyRefreshEvent += new UpdateLabel.MyEvent(GetLabels);
            update.Show();
        }

        private void DelLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelOneLabel();
        }

        private void DelOneLabel()
        {
            if (treeList1.FocusedNode.Level==0)
            {
                if (MessageBox.Show($"删除<{treeList1.FocusedNode.GetValue(0)}>将删除下面的所有子标签,是否删除？", "提示",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            else
            {
                if (MessageBox.Show($"删除<{treeList1.FocusedNode.GetValue(0)}>标签,是否删除？", "提示",
                        MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            if (!LabelData.DeleteLabel(int.Parse(treeList1.FocusedNode.Tag.ToString())))
            {
                MessageBox.Show("删除失败");
                return;
            }
            GetLabels();
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
            AddLabel addLabel = new AddLabel();
            addLabel.MyRefreshEvent += new AddLabel.MyEvent(GetLabels);
            addLabel.Show();
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetLabels();
        }
    }
}