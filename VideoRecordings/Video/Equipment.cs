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
        Dictionary<int, string> AllEquipmengt = new Dictionary<int, string>();
        List<EquipmentInfo> Equipments = new List<EquipmentInfo>();
        List<string> listOnit = new List<string>();
        List<string> listNew = new List<string>();  //搜索集合

        InformationDisplay information;
        QueryVideo video;
        public Equipment(InformationDisplay info, string projectname)
        {
            InitializeComponent();
            information = info;
            project = projectname;
        }

        public Equipment(QueryVideo queryVideo, string projectname)
        {
            InitializeComponent();
            video = queryVideo;
            project = projectname;
        }

        private void Equipment_Load(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            radioButton2.Checked = true;
            SetAllEquipmengt();
            AddComItems();
        }

        private void AddComItems()
        {
            comboBox1.Items.Clear();
            listOnit = AllEquipmengt.Select(t => t.Value).ToList();
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
            if (string.IsNullOrEmpty(name))
                return;
            if (listOnit.Contains(name))
            {
                MessageBox.Show("已经存在当前标签");
                return;
            }
            if (!GetData.AddEquipment(project, name))
            {
                MessageBox.Show("新建设备失败");
                return;
            }
            MessageBox.Show("上传设备成功");
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
            if (string.IsNullOrEmpty(textBox2.Text.Trim())) return;
            int id = AllEquipmengt.FirstOrDefault(t => t.Value == name).Key;
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
            int id = AllEquipmengt.FirstOrDefault(t => t.Value == name).Key;
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
            information?.PostVideos();
            video?.RefreshImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EquipmentModification();
            information?.AddItems();
            video?.AddItems();
            SetAllEquipmengt();
            textBox1.Text = string.Empty;
            AddComItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EquipmentModification();
            information?.AddItems();
            video?.AddItems();
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
            Equipments = GetData.GetEquipment(project);
            AllEquipmengt = Equipments.ToDictionary(t => t.Id, t => t.Name);
        }
    }
}
