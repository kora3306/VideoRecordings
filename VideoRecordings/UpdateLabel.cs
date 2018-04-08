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

namespace VideoRecordings
{
    public partial class UpdateLabel : DevExpress.XtraEditors.XtraForm
    {
        ManagementLabel management;
        string index;
        public UpdateLabel(ManagementLabel label,string text)
        {
            InitializeComponent();
            management = label;
            index = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==string.Empty||textBox1.Text.Trim()==string.Empty)
            {
                MessageBox.Show("修改不能为空");
                return;
            }
            string url = Program.Urlpath + "/label/" + index;
            Dictionary<string, string> update = new Dictionary<string, string>();
            update.Add("name", textBox1.Text.Trim());
            string json= (new JavaScriptSerializer()).Serialize(update);
            JObject obj = WebClinetHepler.Patch_New(url,json);
            if (obj==null)
            {
                MessageBox.Show("修改失败");
            }
            management.GetLabels();
            management.RefreshTreeView();
            this.Close();
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