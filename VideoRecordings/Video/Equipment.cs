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
    public partial class Equipment : Form
    {
        string project;
        MyEquipment equipment;
        List<string> listOnit = new List<string>();
        List<string> listNew = new List<string>();  //搜索集合

        public Equipment(string projectname)
        {
            InitializeComponent();
            project = projectname;
        }

        private void Equipment_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            radioButton2.Checked = true;
            equipment = new MyEquipment(project);
            SetAllEquipmengt();
            AddComItems();
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

        private void AddComItems()
        {
            comboBox1.Items.Clear();
            listOnit =equipment.AllEquipmengt.Select(t =>t.Key+":"+ t.Value).ToList();
            comboBox1.Items.AddRange(listOnit.ToArray());
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox2.Visible = true;
                return;
            }
            textBox2.Visible = false;
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

        private void Add(string name)
        {
            if (!GetData.AddEquipment(project, name))
            {
                MessageBox.Show("新建设备失败");
                return;
            }
            MessageBox.Show("新建设备成功");
        }

        private void Update(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            if (!listOnit.Contains(name))
            {
                MessageBox.Show("不存在当前标签");
                return;
            }
            string equipment0 = name.Split(':').Last();
            if (string.IsNullOrEmpty(textBox2.Text.Trim())) return;
            int id = equipment.AllEquipmengt.FirstOrDefault(t => t.Value == equipment0).Key;
            if (!GetData.UpdateEquipmengt(id, textBox2.Text.Trim()))
            {
                MessageBox.Show("修改设备名称失败");
                return;
            }
            MessageBox.Show("修改设备名称成功");
        }

        private void Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            if (!listOnit.Contains(name))
            {
                MessageBox.Show("不存在当前标签");
                return;
            }
            string equipment0 = name.Split(':').Last();
            int id =equipment.AllEquipmengt.FirstOrDefault(t => t.Value == equipment0).Key;
            if (!GetData.DeleteEquipmengt(id))
            {
                MessageBox.Show("删除设备失败");
                return;
            }
            MessageBox.Show("删除设备成功");
        }

        private void EquipmentModification()
        {
            string name = comboBox1.Text.Trim();
            if (radioButton1.Checked)
            {                
                name = textBox1.Text.Trim();            
                Add(name);
                return;
            }
            else if (radioButton2.Checked)
            {
                Delete(name);
            }
            else
            {
                Update(name);             
            }
            OnRefresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EquipmentModification();
            OnSave();
            SetAllEquipmengt();
            textBox1.Text = string.Empty;
            AddComItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EquipmentModification();
            OnSave();
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Visible = true;
                return;
            }
            textBox1.Visible = false;
        }

        private void SetAllEquipmengt()
        {
            equipment = new MyEquipment(project);
        }
    }
}
