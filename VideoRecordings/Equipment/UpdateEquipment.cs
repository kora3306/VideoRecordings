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
using VideoRecordings.Models;

namespace VideoRecordings.Video
{
    public partial class UpdateEquipment : Form
    {
        EquipmentInfo equip;
        public UpdateEquipment(EquipmentInfo equipmentInfo)
        {
            InitializeComponent();
            equip = equipmentInfo;
            InitializeTextBox();
        }

        private void InitializeTextBox()
        {
            TextBox_city.Text = TextBox_newcity.Text = equip.City;
            TextBox_street.Text = TextBox_newstreet.Text = equip.Street;
            TextBox_site.Text = TextBox_newsite.Text= equip.Site;
            TextBox_uid.Text = TextBox_newuid.Text=equip.Uid;
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
            if (string.IsNullOrEmpty(TextBox_newcity.Text.Trim())||string.IsNullOrEmpty(TextBox_newstreet.Text.Trim())||
                string.IsNullOrEmpty(TextBox_newsite.Text.Trim())||string.IsNullOrEmpty(TextBox_newuid.Text.Trim()))
                return;
            if (!EquipmentData.UpdateEquipmengt(equip.Id, TextBox_newcity.Text.Trim(), TextBox_newstreet.Text.Trim()
            , TextBox_newsite.Text.Trim(), TextBox_newuid.Text.Trim()))
            {
                MessageBox.Show("修改设备失败");
                return;
            }
            MessageBox.Show("修改设备成功");
            OnSave();
            OnRefresh();
            this.Close();
            Program.log.Error($"修改设备名,EquipmengtID:{equip.Id},设备名:{TextBox_newcity.Text.Trim()+TextBox_newstreet.Text.Trim()+TextBox_newsite.Text.Trim()+TextBox_newuid.Text.Trim()}");
        }
    }
}
