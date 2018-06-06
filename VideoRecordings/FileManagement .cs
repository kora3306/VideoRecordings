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
        Login _log;
        public FileManagement(Login login)
        {
            InitializeComponent();
            _log = login;
        }


        private void video_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetForm(1,"视频文件管理");
        }

        private void _imagetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetForm(2,"图片文件管理");
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
        private void SetForm(int index,string name)
        {
            string formClass = string.Empty;
            switch (index)
            {
                case 1:
                    formClass = "VideoRecordings.VideoInformation";
                    break;
                case 2:
                    formClass = "VideoRecordings.ImageInformation";
                    break;
                default:
                    break;
            }
            if (tabControlExHfrz.Contains(name))
                return;
            TabPage tabPageMapping = new TabPage(name) { Name = name };
            tabControlExHfrz.TabPages.Add(tabPageMapping);
            GenerateForm(formClass, tabPageMapping);              
        }

        private void FileManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            _log.Close();
        }
    }




}
