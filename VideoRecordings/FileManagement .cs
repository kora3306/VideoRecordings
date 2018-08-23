using MyControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRecordings
{
    public partial class FileManagement : Form
    {
        public FileManagement()
        {
            InitializeComponent();
            Methods.AddIsTest(this);
        }


        /// <summary>
        /// 反射获取窗体
        /// </summary>
        /// <param name="form"></param>
        /// <param name="sender"></param>
        public void GenerateForm(string form, object sender)
        {
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.BringToFront();
            fm.TopLevel = false;
            fm.Parent = (Panel)sender;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();
        }

        /// <summary>
        /// 判断获取加载的窗体
        /// </summary>
        /// <param name="index"></param>
        public  void SetForm(string formClass, string name)
        {
            if (formClass =="VideoRecordings.VideoInformation"&&tabControlExHfrz.Contains(name))
                return;
            tabControlExHfrz.SuspendLayout();
            TabPage tabPageMapping = new TabPage(name) { Name = name };
            tabControlExHfrz.TabPages.Add(tabPageMapping);
            GenerateForm(formClass, tabPageMapping);
            tabControlExHfrz.SelectTabEx(name);
            tabControlExHfrz.ResumeLayout(false);
        }

        private void FileManagement_Load(object sender, EventArgs e)
        {
            VideoToolStripMenuItem.PerformClick();
        }

        private void VideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item= sender as ToolStripMenuItem;
            if (item == null) return;
            SetForm(item.Tag.ToString(),item.Text);
        }

        private void queryVIdeoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            SetForm(item.Tag.ToString(), item.Text);
        }

        private void LabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            SetForm(item.Tag.ToString(), item.Text);
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            SetForm(item.Tag.ToString(), item.Text);
        }

        private void repetitionVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            SetForm(item.Tag.ToString(), item.Text);
        }
    }




}
