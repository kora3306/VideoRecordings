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
using Newtonsoft.Json.Linq;
using Common;
using VideoRecordings.GetDatas;

namespace VideoRecordings
{
    public partial class UpdateLabel : DevExpress.XtraEditors.XtraForm
    {
        int index;
        RefreshType type;
        public UpdateLabel(string info,RefreshType refresh)
        {
            InitializeComponent();
            index = int.Parse(info.Split(':').First());
            textBox1.Text = info.Split(':').Last();
            type = refresh;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("修改不能为空");
                return;
            }
            if (!LabelData.UpdateLabelName(index,textBox1.Text.Trim()))
            {
                MessageBox.Show("修改失败");
                Program.log.Error($"修改标签名,label_id:{index},修改名:{textBox1.Text.Trim()}");
            }
            OnRefresh(type);
            this.Close();
        }

        public delegate void MyEvent(RefreshType type);

        public event MyEvent MyRefreshEvent;

        public void OnRefresh(RefreshType type)
        {
            MyRefreshEvent?.Invoke(type);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    button1.PerformClick();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}