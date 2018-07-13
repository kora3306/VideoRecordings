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

namespace VideoRecordings
{
    public partial class SelectLabel : DevExpress.XtraEditors.XtraForm
    {
        List<string> labels = new List<string>();
        bool IsShowLabels;
        MyLabel MyLabel;

        List<string> listOnit = new List<string>();
        List<string> listNew = new List<string>();  //搜索集合

        public SelectLabel(string text = null, bool isshow = false)
        {
            InitializeComponent();
            IsShowLabels = isshow;
            MyLabel = new MyLabel();
            if (!string.IsNullOrEmpty(text))
                labels = text.Split(',').ToList();
        }

        private void SelectLabel_Load(object sender, EventArgs e)
        {
            List<TreeNode> trees = MyLabel.treeNodes;
            treeView1.Nodes.AddRange(trees.ToArray());
            treeView1.ExpandAll();
            if (treeView1.Nodes.Count != 0)
                treeView1.SelectedNode = treeView1.Nodes[0];
            SetTreeNodes();
            listOnit = MyLabel.LabelAll.Select(t => t.Value).ToList();
            comboBox1.Items.AddRange(listOnit.ToArray());
        }

        public delegate void MyDelegate(List<string> labels);
        public event MyDelegate MyRefreshEvent;
        public virtual void OnRefresh(List<string> labels)
        {
            MyRefreshEvent.Invoke(labels);
        }

        public event MyDelegate MySaveEvent;
        public virtual void OnSave(List<string> labels)
        {
            MySaveEvent.Invoke(labels);
        }

        private void treeView2_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = treeView2.SelectedNode;
            treeView2.Nodes.Remove(node);
            labels.Remove(node.Text);
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Nodes.Count != 0)
                {
                    foreach (TreeNode it in item.Nodes)
                    {
                        if (node.Text == it.Text)
                        {
                            it.Checked = false;
                            break;
                        }
                    }
                }
            }
            treeView1.Refresh();
        }
        /// <summary>
        /// 标签勾选
        /// </summary>
        private void SetTreeNodes()
        {
            foreach (var label in labels)
            {
                foreach (TreeNode tree in treeView1.Nodes)
                {
                    foreach (TreeNode node in tree.Nodes)
                    {
                        if (label == node.Text)
                        {
                            node.Checked = true;
                            continue;
                        }
                    }
                }
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (!MyLabel.LabelsNumber.Values.Contains(node.Text))
            {
                if (!node.Checked)
                {
                    node.Checked = true;
                    if (!labels.Contains(node.Text))
                    {
                        labels.Add(node.Text);
                    }
                    LabelRefreshDate();
                }
                else
                {
                    node.Checked = false;
                    labels.Remove(node.Text);
                    LabelRefreshDate();
                }

            }
        }

        /// <summary>
        /// 已选择栏添加已有标签
        /// </summary>
        private void LabelRefreshDate()
        {
            treeView2.Nodes.Clear();
            treeView2.Nodes.AddRange(labels.Select(t => new TreeNode
            {
                Text = t,
                Name = t,
                ForeColor = Color.Blue,
                NodeFont = new Font("微软雅黑", 12)
            }).ToArray());
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            //if (node.Nodes.Count == 0)
            //    return;
            if (node.Checked)
                node.Nodes.OfType<TreeNode>().ToList().ForEach(x => x.Checked = true);
            else
                node.Nodes.OfType<TreeNode>().ToList().ForEach(x => x.Checked = false);

            labels = CheckBoxNodes().Select(t => t.Text).ToList();
            LabelRefreshDate();
        }

        private List<TreeNode> CheckBoxNodes()
        {
            List<TreeNode> checks = new List<TreeNode>();
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    foreach (TreeNode it in item.Nodes)
                    {
                        if (it.Checked)
                        {
                            checks.Add(it);
                        }
                    }
                }
            }
            return checks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (labels.Count != 0)
            {
                DialogResult = DialogResult.OK;
            }
            if (!IsShowLabels)
            {
                OnSave(labels);
                this.Close();
                return;
            }
            OnRefresh(labels);
            this.Close();

        }

        private void SelectLabel_FormClosed(object sender, FormClosedEventArgs e)
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            labels.Clear();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = comboBox1.Text.Trim();
            if (string.IsNullOrEmpty(text) || treeView1.Nodes.Count == 0)
                return;
            treeView1.Focus();
            foreach (TreeNode tree in treeView1.Nodes)
            {
                if (tree.Text == text)
                {
                    treeView1.SelectedNode = tree;
                    return;
                }
                foreach (TreeNode node in tree.Nodes)
                {
                    if (node.Text == text)
                    {
                        treeView1.SelectedNode = node;
                        return;
                    }
                }
            }
            treeView1.Refresh();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (comboBox1.Focused)
                    {
                        button2.PerformClick();
                        return true;
                    }
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            List<string> first = listOnit.Where(t => t.Contains(comboBox1.Text)).ToList();
            if (first.Count == 0)
            {
                return;
            }
            try
            {
                this.comboBox1.Items.Clear();
                listNew.Clear();
                foreach (var item in listOnit)
                {
                    if (item.Contains(comboBox1.Text))
                    {
                        listNew.Add(item);
                    }
                }
                comboBox1.Items.AddRange(listNew.ToArray());
                comboBox1.SelectionStart = comboBox1.Text.Length;
                Cursor = Cursors.Default;
                comboBox1.DroppedDown = true;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}