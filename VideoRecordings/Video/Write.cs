using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRecordings.Video
{
    public partial class Write : Form
    {
        string project;
        bool isad;
        int id;
        public Write(string projectname,int number=0, bool isadd = true)
        {
            InitializeComponent();
            SetText(isadd);
            project = projectname;
            isad = isadd;
            id = number;
        }

        public delegate void MyDelegate();
        public event MyDelegate MySaveEvent;
        public virtual void OnSave()
        {
            MySaveEvent?.Invoke();
        }

        public event MyDelegate MyRefreshEvent;
        public virtual void OnRefresh()
        {
            MyRefreshEvent.Invoke();
        }

        private void SetText(bool isadd)
        {
            if (isadd)
            {
                label1.Text = "添加设备名";
                return;
            }
            label1.Text = "修改设备名";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultTextBox1.Text.Trim())) return;
            if (isad)
            {
                if (!GetData.AddEquipment(project, defaultTextBox1.Text.Trim()))
                {
                    MessageBox.Show("新建设备失败");
                    return;
                }
                MessageBox.Show("新建设备成功");
                OnSave();
            }
            else
            {
                if (!GetData.UpdateEquipmengt(id, defaultTextBox1.Text.Trim()))
                {
                    MessageBox.Show("修改设备失败");
                    return;
                }
                MessageBox.Show("修改设备成功");
                OnRefresh();
            }         
        }
    }
}
