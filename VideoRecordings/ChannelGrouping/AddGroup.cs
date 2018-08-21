using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.Models;

namespace VideoRecordings.ChannelGrouping
{
    public partial class AddGroup : Form
    {
        public AddGroup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!MyGroup.AddGroup(textBox1.Text.Trim()))
            {
                MessageBox.Show("添加失败");
                return;
            }
            OnSave();
            this.Close();
        }

        public delegate void MyEvent();

        public  event MyEvent MySaveEvent;

        public void OnSave()
        {
            MySaveEvent?.Invoke();
        }


    }
}
