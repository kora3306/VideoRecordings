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
using System.Web.Script.Serialization;
using Common;
using Newtonsoft.Json.Linq;

namespace VideoRecordings
{
    public partial class AddLabel : DevExpress.XtraEditors.XtraForm
    {
        ManagementLabel Management;
        Dictionary<string, string> labels;
        public AddLabel(ManagementLabel recording, Dictionary<string, string> labelNumber)
        {
            Management = recording;
            labels = labelNumber;
            InitializeComponent();
            comboBox1.DataSource = GetDicToList(labels);
            comboBox1.Text = "请选择要添加标签的种类";
        }

        /// <summary>
        /// 将填入的标签加入选择的标签集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddLabels();
            Management.GetLabels();
            Management.RefreshTreeView();
            textBox1.Text = string.Empty;
            comboBox1.DataSource = GetDicToList(Management.GetLabelNumber());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<string> GetDicToList(Dictionary<string, string> dic)
        {
            List<string> label = new List<string>();
            label = dic.Select(t => $"{t.Key}:{t.Value}").ToList();
            label.Insert(0, "0:新建标签种类");
            label.OrderBy(t => int.Parse(t.Split(':').First()));
            return label;
        }

        private void AddLabels()
        {
            string url = Program.Urlpath + "/labels";
            string json = PostJson(int.Parse(comboBox1.Text.Split(':').First()));
            if (json==string.Empty)
            {
                return;
            }
            JObject obj = WebClinetHepler.Post_New(url,json);
            if (obj==null)
            {
                MessageBox.Show("添加失败");
            }
            Program.log.Error($"添加标签,种类{comboBox1.Text.Split(':').Last()}", new Exception($"{json}"));
        }

        private List<string> GetLabels()
        {
            List<string> labels = new List<string>();
            List<string> striparr = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            striparr = striparr.Where(s => !string.IsNullOrEmpty(s)).ToList();
            foreach (var item in striparr)
            {
                if (!labels.Contains(item))
                {
                    labels.Add(item);
                }
            }
            return labels;
        }

        private string PostJson(int index)
        {
            if (GetLabels().Count==0)
            {
                return string.Empty;
            }
            List< Dictionary<string, object>> postlabel = new List<Dictionary<string, object>>();
            foreach (var item in GetLabels())
            {
                Dictionary<string, object> diclabel = new Dictionary<string, object>();
                diclabel.Add("parent_id", index);
                diclabel.Add("name", item);
                postlabel.Add(diclabel);
            }
            return (new JavaScriptSerializer()).Serialize(postlabel);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

    }
}