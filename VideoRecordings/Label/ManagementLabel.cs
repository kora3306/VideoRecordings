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

namespace VideoRecordings
{
    public partial class ManagementLabel : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, string> LabelsNumber = new Dictionary<string, string>();   // 标签对照
        Dictionary<string, string> LabelAll = new Dictionary<string, string>();
        public ManagementLabel()
        {
            InitializeComponent();
        }

        private void ManagementLabel_Load(object sender, EventArgs e)
        {
            GetLabels();
            treeView1.ExpandAll();
        }

        /// <summary>
        /// 获取标签集合加入选择标签栏
        /// </summary>
        public void GetLabels()
        {
            treeView1.Nodes.Clear();
            LabelsNumber.Clear();
            LabelAll.Clear();
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null || obj["result"] == null || obj["result"].Count() == 0)
            {
                return;
            }
            List<TreeNode> items = new List<TreeNode>();
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                TreeNode tree = new TreeNode { Text = obj["result"][i]["name"].ToString(),
                    ForeColor = Color.Green };
                LabelsNumber.Add(obj["result"][i]["id"].ToString(), obj["result"][i]["name"].ToString());
                LabelAll.Add(obj["result"][i]["id"].ToString(), obj["result"][i]["name"].ToString());
                for (int j = 0; j < obj["result"][i]["children"].Count(); j++)
                {
                    TreeNode node = new TreeNode { Text = obj["result"][i]["children"][j]["name"].ToString(),
                        Tag= obj["result"][i]["children"][j]["is_fre"].ToString(),
                        ForeColor = obj["result"][i]["children"][j]["is_fre"].ToString() == "1"? Color.Red:Color.Blue};
                    LabelAll.Add(obj["result"][i]["children"][j]["id"].ToString(), obj["result"][i]["children"][j]["name"].ToString());
                    tree.Nodes.Add(node);
                }
                items.Add(tree);
            }
            treeView1.Nodes.AddRange(items.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> lose = new List<string>();
            bool isfalse = false;
            if (treeView1.Nodes.Count == 0)
            {
                return;
            }
            foreach (var json in GetDelLabel())
            {
                JObject obj = WebClinetHepler.Delete_New(json);
                if (obj == null)
                {
                    lose.Add(json.Split('/').Last());
                    isfalse = true;
                }
            }
            if (isfalse)
            {
                MessageBox.Show($"第{string.Join(",", lose)},删除失败");
                return;
            }
            MessageBox.Show("删除成功");
            GetLabels();
            RefreshTreeView();
        }

        private string GetNumber(string text)
        {
            foreach (var item in LabelAll)
            {
                if (item.Value == text)
                {
                    return item.Key;
                }
            }
            return string.Empty;
        }

        private List<string> GetDelLabel()
        {
            List<string> DelLabels = new List<string>();
            foreach (string item in GetAllCheckdNumber())
            {
                DelLabels.Add(Program.Urlpath + "/label/" + item);
            }
            return DelLabels;
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            new AddLabel(this, LabelsNumber).ShowDialog();
        }

        public void RefreshTreeView()
        {
            treeView1.Refresh();
            treeView1.ExpandAll();
        }

        private void UpLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UpdateLabel(this, GetNumber(treeView1.SelectedNode.Text)).ShowDialog();
        }

        private void DelLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelOneLabel();
        }

        private void DelOneLabel()
        {
            if (LabelsNumber.Values.Contains(treeView1.SelectedNode.Text))
            {
                if (MessageBox.Show($"删除<{treeView1.SelectedNode.Text}>将删除下面的所有子标签,是否删除？", "提示",
                    MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
            }
            string index = GetNumber(treeView1.SelectedNode.Text);
            string url = Program.Urlpath + "/label/" + index;
            JObject obj = WebClinetHepler.Delete_New(url);
            if (obj == null)
            {
                MessageBox.Show("删除失败");
                return;
            }
            MessageBox.Show("删除成功");
            GetLabels();
            RefreshTreeView();
        }

        public List<TreeNode> GetChecked()//返回所有被选中的节点
        {
            List<TreeNode> ltn = new List<TreeNode>();
            TreeNode tn = null;//tn里面有名称、索引等属性自己去出来
            TreeNodeCollection tnc = treeView1.Nodes;//获取treeview的子节点的集合
            for (int i = 0; i < tnc.Count; i++)//两级的循环只能找出两级中所有被选中节点
            {
                tn = tnc[i];
                if (tn.GetNodeCount(true) > 0)//下面还有子节点
                {
                    TreeNodeCollection tnb = tn.Nodes;
                    for (int j = 0; j < tnb.Count; j++)
                    {
                        tn = tnb[j];
                        if (tn.Checked) ltn.Add(tn);

                    }
                }

            }
            return ltn;
        }

        private void treeView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = treeView1.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    treeView1.SelectedNode = tn;
                }
            }
        }
        /// <summary>
        /// 修改常用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> list = GetAllCheckdNumber().Select(t => int.Parse(t)).ToList();
            string json= (new JavaScriptSerializer()).Serialize(list);
            string url = Program.Urlpath + "/labels/frequent";
            JObject obj = WebClinetHepler.Patch_New(url,json);
            if (obj==null)
            {
                MessageBox.Show("设置常用失败");
                return;
            }
            GetLabels();
            RefreshTreeView();
        }

        private List<string> GetAllCheckdNumber()
        {
            List<string> indexs = new List<string>();
            foreach (var item in GetChecked())
            {
                indexs.Add(GetNumber(item.Text));
            }
            return indexs;
        }

        public Dictionary<string,string>GetLabelNumber()
        {
            Dictionary<string, string> scenarios = new Dictionary<string, string>();
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null || obj["result"] == null || obj["result"].Count() == 0)
            {
                return new Dictionary<string, string>();
            }
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                scenarios.Add(obj["result"][i]["id"].ToString(), obj["result"][i]["name"].ToString());
            }
            return scenarios;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> list = GetAllCheckdNumber().Select(t => int.Parse(t)).ToList();
            string json = (new JavaScriptSerializer()).Serialize(list);
            string url = Program.Urlpath + "/labels/infrequent";
            JObject obj = WebClinetHepler.Patch_New(url, json);
            if (obj == null)
            {
                MessageBox.Show("取消常用失败");
                return;
            }
            GetLabels();
            RefreshTreeView();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 0)
                return;
            if (node.Checked)
                node.Nodes.OfType<TreeNode>().ToList().ForEach(x => x.Checked = true);
            else
                node.Nodes.OfType<TreeNode>().ToList().ForEach(x => x.Checked = false);
        }
    }
}