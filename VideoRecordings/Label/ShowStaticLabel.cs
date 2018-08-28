using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;

namespace VideoRecordings.Label
{
    public partial class ShowStaticLabel : Form
    {
        int index;
        public ShowStaticLabel(int id)
        {
            InitializeComponent();
            index = id;
            SetShowGroups();
        }

        private void SetShowGroups()
        {
            treeList1.BeginUpdate();
            treeList1.Nodes.Clear();
            foreach (TypeLabel item in LabelData.GetLabelToEquipment(index).OrderByDescending(t => t.Name))
            {
                TreeListNode ParentNode = treeList1.AppendNode(null, null);
                ParentNode.SetValue(treeList1.Columns[0], item.Name);
                ParentNode.Tag = item.Id;
                foreach (VideoLabel equip in item.Labels)
                {
                    TreeListNode tree = ParentNode.Nodes.Add("");
                    tree.SetValue(treeList1.Columns[0], equip.Name);
                    tree.Tag = equip.Id;
                }
            }
            treeList1.EndUpdate();
            treeList1.ExpandAll();
        }
    }
}
