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
        QueryVideo queryVideo;
        Dictionary<string, string> allscenes;
        string labeltext;
        List<string> labels = new List<string>();
        List<string> texts = new List<string>();
        public SelectLabel(QueryVideo query, List<TreeNode> list, Dictionary<string, string> dic, Dictionary<string, string> labelall, string text)
        {
            InitializeComponent();
            queryVideo = query;
            allscenes = dic;
            labeltext = text;
            treeView1.Nodes.AddRange(list.ToArray());
            treeView1.ExpandAll();
            labels = CheckBoxNodes().Select(t => t.Text).ToList();
            LabelRefreshDate();
            SetTreeNodes();
            texts = labelall.Select(t => t.Value).ToList();
        }

        private void SelectLabel_Load(object sender, EventArgs e)
        {           
            textBox2.AutoCompleteCustomSource.Clear();
            textBox2.AutoCompleteCustomSource.AddRange(texts.ToArray());
            textBox1.AutoCompleteCustomSource.Clear();
            textBox1.AutoCompleteCustomSource.AddRange(texts.ToArray());
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

        private void RefeshText()
        {
            textBox2.AutoCompleteCustomSource.Clear();
            textBox2.AutoCompleteCustomSource.AddRange(labels.ToArray());
        }

        private void SetTreeNodes()
        {
            if (string.IsNullOrEmpty(labeltext))
            {
                treeView2.Nodes.Clear();
                foreach (TreeNode item in treeView1.Nodes)
                {
                    foreach (TreeNode it in item.Nodes)
                    {
                        it.Checked = false;
                    }
                }
                return;
            }
            treeView2.Nodes.Clear();
            labels = labeltext.Split(',').ToList();
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Nodes.Count != 0)
                {
                    foreach (TreeNode it in item.Nodes)
                    {
                        foreach (string label in labels)
                        {
                            if (label == it.Text)
                            {
                                it.Checked = true;
                            }
                        }
                    }
                }
            }
            LabelRefreshDate();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (!allscenes.Values.Contains(node.Text))
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
            RefeshText();
        }

        private void LabelRefreshDate()
        {
            treeView2.Nodes.Clear();
            treeView2.Nodes.AddRange(labels.Select(t => new TreeNode
            {
                Text = t,
                Name = t,
                ForeColor = Color.Blue,
                NodeFont = new Font("Arial", 12)
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
            RefeshText();
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
            queryVideo.StartScreening(labels);
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
            string text = textBox1.Text.Trim();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(text) || treeView1.Nodes.Count == 0)
                return;
            treeView2.Focus();
            foreach (TreeNode tree in treeView2.Nodes)
            {
                if (tree.Text == text)
                {
                    treeView2.SelectedNode = tree;
                    return;
                }
                foreach (TreeNode node in tree.Nodes)
                {
                    if (node.Text == text)
                    {
                        treeView2.SelectedNode = node;
                        return;
                    }
                }
            }
            treeView2.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (textBox1.Focused)
                    {
                        button2.PerformClick();
                        return true;
                    }
                    if (textBox2.Focused)
                    {
                        button3.PerformClick();
                        return true;
                    }                   
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}