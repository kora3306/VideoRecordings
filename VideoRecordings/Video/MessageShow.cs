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
    public partial class MessageShow : Form
    {
        public MessageShow(string message1, int info1, string message2, int info2, string message3, int info3, string message4, int info4, string message5 = null, int info5 = 0)
        {
            InitializeComponent();
            label1.Text = message1;
            label_info1.Text = info1.ToString();
            label2.Text = message2;
            label_info2.Text = info2.ToString();
            label3.Text = message3;
            label_info3.Text = info3.ToString();
            label4.Text = message4;
            label_info4.Text = info4.ToString();
            if (!string.IsNullOrEmpty(message5))
            {
                label5.Text = message5;
                label_info5.Text = info5.ToString();
            }
            else
            {
                label5.Visible = false;
                label_info5.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
