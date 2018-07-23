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
        int id;
        MyEquipment equipment;
        List<string> listOnit = new List<string>();
        List<string> listNew = new List<string>();  //搜索集合
        public Write(string projectname, int number = 0)
        {
            InitializeComponent();
            project = projectname;
            id = number;
            equipment = new MyEquipment(projectname);
            AddComItems();
            SetComBoBox();
        }

        public event MyDelegate MySaveEvent;
        public virtual void OnSave()
        {
            MySaveEvent?.Invoke();
        }

        public delegate void MyDelegate();
        public event MyDelegate MyRefreshEvent;
        public virtual void OnRefresh()
        {
            MyRefreshEvent.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultTextBox1.Text.Trim())) return;
            if (!GetData.UpdateEquipmengt(int.Parse(comboBox1.Text.Split(':').First()), defaultTextBox1.Text.Trim()))
            {
                MessageBox.Show("修改设备失败");
                return;
            }
            MessageBox.Show("修改设备成功");
            OnSave();
            OnRefresh();
            this.Close();
        }

        private void AddComItems()
        {
            comboBox1.Items.Clear();
            listOnit = equipment.AllEquipmengt.Select(t => t.Key + ":" + t.Value).ToList();
            comboBox1.Items.AddRange(listOnit.ToArray());
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                this.comboBox1.Items.Clear();
                listNew.Clear();
                foreach (var item in listOnit)
                {
                    if (item.Contains(comboBox1.Text))
                    {
                        listNew.Add(item);
                    }
                }
                comboBox1.Items.AddRange(listNew.ToArray());
                comboBox1.SelectionStart = comboBox1.Text.Length;
                Cursor = Cursors.Default;
                comboBox1.DroppedDown = true;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void SetComBoBox()
        {
            int i = 0;
            foreach (var item in comboBox1.Items)
            {
                if (item.ToString().Split(':').First() == $"{id}")
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }
    }
}
