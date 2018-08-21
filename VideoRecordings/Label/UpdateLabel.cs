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
        public UpdateLabel(int id)
        {
            InitializeComponent();
            index = id;
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
            }
            OnRefresh();
            this.Close();
        }

        public delegate void MyEvent();

        public event MyEvent MyRefreshEvent;

        public void OnRefresh()
        {
            MyRefreshEvent?.Invoke();
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