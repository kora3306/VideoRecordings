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
using DevExpress.XtraTreeList.Nodes;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;

namespace VideoRecordings
{
    public partial class SelectLabel : DevExpress.XtraEditors.XtraForm
    {
        private MyLabels typeLabels = new MyLabels();
        List<string> labels = new List<string>();
        private bool IsExpand = true;
        RefreshType Type;

        public SelectLabel(RefreshType type=RefreshType.None,string text = null)
        {
            InitializeComponent();
            Type = type;
            if (!string.IsNullOrEmpty(text))
                labels = text.Split(',').ToList();
        }

        private void SelectLabel_Load(object sender, EventArgs e)
        {
            SetShowGroups();
            SetShowTree();
        }

        private void SetShowTree()
        {
            if (labels == null) return;
            selects_treeList.Nodes.Clear();
            foreach (var item in labels.OrderBy(t => t.ToString()))
            {
                TreeListNode ParentNode = selects_treeList.AppendNode(item, null);//添加第一节点
                ParentNode.SetValue(selects_treeList.Columns[0], item);//添加第一节点显示的值
            }
        }

        private void SetShowGroups()
        {
            typeLabels = new MyLabels();
            if (typeLabels == null) return;
            List<TypeLabel> labels = new List<TypeLabel>();
            switch (Type)
            {
                case RefreshType.None:
                    labels = typeLabels.DynamicLabel.Union(typeLabels.StaticLabel).ToList();
                    break;
                case RefreshType.DynamicLabel:
                    labels = typeLabels.DynamicLabel;
                    break;
                case RefreshType.StaticLabel:
                    labels = typeLabels.StaticLabel;
                    break;
            }
            All_treeList.BeginUpdate();
            All_treeList.Nodes.Clear();
            foreach (TypeLabel item in labels.OrderByDescending(t => t.Name))
            {
                TreeListNode ParentNode = All_treeList.AppendNode(null, null);
                ParentNode.SetValue(All_treeList.Columns[0], item.Name);
                ParentNode.Tag = item.Id;
                foreach (VideoLabel equip in item.Labels)
                {
                    TreeListNode tree = ParentNode.Nodes.Add("");
                    tree.SetValue(All_treeList.Columns[0], equip.Name);
                    tree.Tag = equip.Id;
                }
            }
            All_treeList.EndUpdate();
            All_treeList.CollapseAll();
            IsExpand = true;
        }

        public delegate void MyDelegate(List<string> labels);
        public event MyDelegate MyRefreshEvent;
        public virtual void OnRefresh(List<string> labels)
        {
            MyRefreshEvent.Invoke(labels);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (labels.Count != 0)
            {
                DialogResult = DialogResult.OK;
                OnRefresh(labels);
                this.Close();
            }
        }

        //
        private void selects_treeList_DoubleClick(object sender, EventArgs e)
        {
            if (selects_treeList.FocusedNode != null)
            {             
                labels.Remove(selects_treeList.FocusedNode.GetValue(0).ToString());
                selects_treeList.Nodes.Remove(selects_treeList.FocusedNode);
            }
        }


        private void All_treeList_Click(object sender, EventArgs e)
        {
            if (All_treeList.FocusedNode == null || All_treeList.FocusedNode.Level == 0) return;
            if (!labels.Contains(All_treeList.FocusedNode.GetValue(0).ToString()))
            {
                labels.Add(All_treeList.FocusedNode.GetValue(0).ToString());
                SetShowTree();
            }
           
        }

        private void OPenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsExpand)
            {
                All_treeList.CollapseAll();
                IsExpand = false;
                return;
            }

            All_treeList.ExpandAll();
            IsExpand = true;
        }
    }
}