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
    public partial class AddEquipment : Form
    {
        string project;

        public delegate void MyDelegate();
        public event MyDelegate MySaveEvent;
        public virtual void OnSave()
        {
            MySaveEvent?.Invoke();
        }


        public AddEquipment(string projectname, int number = 0, bool isadd = true)
        {
            InitializeComponent();
            project = projectname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultTextBox1.Text.Trim())) return;

            if (!GetData.AddEquipment(project, defaultTextBox1.Text.Trim()))
            {
                MessageBox.Show("新建设备失败");
                return;
            }
            MessageBox.Show("新建设备成功");
            OnSave();
            this.Close();
        }
    }
}
