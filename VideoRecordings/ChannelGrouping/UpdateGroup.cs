using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using VideoRecordings.Models;

namespace VideoRecordings.ChannelGrouping
{
    public partial class UpdateGroup : Form
    {
        private TreeListNode Tree;
        public UpdateGroup(TreeListNode node)
        {
            InitializeComponent();
            Tree = node;
            defaultTextBox2.Text = node.GetValue(0).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultTextBox1.Text.Trim()))
                return;
            if (!MyGroup.UpdateGroup(int.Parse(Tree.Tag.ToString()),defaultTextBox1.Text.Trim()))
            {
                MessageBox.Show("修改失败");
                return;
            }

            OnSave();
            this.Close();
        }

        public delegate void MyEvent();
        public event MyEvent MySaveEvent;

        public void OnSave()
        {
            MySaveEvent?.Invoke();
        }
    }
}
