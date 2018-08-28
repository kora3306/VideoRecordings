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
using VideoRecordings.GetDatas;
using VideoRecordings.Models;

namespace VideoRecordings
{
    public partial class AddLabel : DevExpress.XtraEditors.XtraForm
    {
        AllTypeLabel AllLabels;
        RefreshType Type = RefreshType.None;
        string comText;
        public AddLabel(RefreshType type=RefreshType.None, string text = null)
        {
            InitializeComponent();
            comText = text;
            Type = type;
            label3.Text = "添加标签:\r\t一次添加多个标签,以回车分开,\r\t添加标签类请选择标签类型为动态或静态";
            SetComboBox();
            SetComboxType();
        }

        public delegate void MyEvent(RefreshType type);

        public event MyEvent MyRefreshEvent;

        public void OnRefresh(RefreshType type)
        {
            MyRefreshEvent?.Invoke(type);
        }
        /// <summary>
        /// 将填入的标签加入选择的标签集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddLabels();
            SetComboBox();
            OnRefresh(Type);
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AddLabels();
            SetComboBox();
        }

        private void SetComboBox()
        {
            AllLabels = LabelData.GetAllLabel();
            List<string> label = AllLabels.DynamicLabel.Union(AllLabels.StaticLabel).OrderBy(t=>t.Id).Select(t=>t.Id+":"+t.Name).ToList();
            label.Insert(0, "0:新建标签种类");
            comboBox1.DataSource = label;
            if (string.IsNullOrEmpty(comText))
            {
                comboBox1.SelectedIndex = 0;
                return;
            }
            comboBox1.Text = comText;
        }

        private void AddLabels()
        {
            List<string> labels = GetLabels();
            if (labels.Count == 0||comboBox1.Text=="请选择要添加标签的种类")
                return ;
            if (!LabelData.AddLabels(int.Parse(comboBox1.Text.Split(':').First()),labels,GetLabelType()))
            {
                MessageBox.Show("添加失败");
                return;
            }
            Program.log.Error($"添加标签,种类{comboBox1.Text.Split(':').Last()}", new Exception($"{string.Join(",",labels)}"));
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            SetComboxType();
        }

        private void SetComboxType()
        {
            if (comboBox1.Text== "0:新建标签种类")
            {
                comboBox2.Enabled = true;
                comboBox2.Text = "动态标签";
                return;
            }
            comboBox2.Enabled = false;
            comboBox2.Text = string.Empty;
        }

        private int GetLabelType()
        {
            switch (comboBox2.Text)
            {
                case "动态标签":
                    Type = RefreshType.DynamicLabel;
                    return 0;
                case "静态标签":
                    Type = RefreshType.StaticLabel;
                    return 1;
                default:
                    return -1;
            }
        }
    }
}