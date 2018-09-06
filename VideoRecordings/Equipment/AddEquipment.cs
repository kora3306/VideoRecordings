using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.GetDatas;

namespace VideoRecordings.Video
{
    public partial class AddEquipment : Form
    {

        public delegate void MyDelegate();
        public event MyDelegate MySaveEvent;
        public virtual void OnSave()
        {
            MySaveEvent?.Invoke();
        }

        public bool isclear = true;

        public AddEquipment(string city=null,string street=null,string site=null)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(city)) textBox1.Text = city;
            if (!string.IsNullOrEmpty(street)) textBox2.Text = street;
            if (!string.IsNullOrEmpty(site)) textBox3.Text = site;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text.Trim())||string.IsNullOrEmpty(textBox2.Text.Trim())
               ||string.IsNullOrEmpty(textBox3.Text.Trim())||string.IsNullOrEmpty(textBox4.Text.Trim()))
            {
                MessageBox.Show("请输入通道完整信息");
                return;
            }
            if (!EquipmentData.AddEquipment(textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim()
            , textBox4.Text.Trim()))
            {
                MessageBox.Show("新建设备失败");
                return;
            }
            MessageBox.Show("新建设备成功");
            ClearText();
            OnSave();         
            this.Hide();
        }

        private void AddEquipment_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                isclear = false;
                return;
            }
            isclear = true;
        }

        public void ClearText()
        {
            if (isclear)
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
            }
        }
    }
}
