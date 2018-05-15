using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Manina.Windows.Forms;
using System.Net;
using VideoRecordings.Properties;

namespace VideoRecordings
{
    public partial class ShowImage : DevExpress.XtraEditors.XtraForm
    {
        List<ImageListViewItem> items;
        int index = 0;
        public ShowImage(List<ImageListViewItem> item)
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(this.panel1_MouseWheel);
            items = item;
        }
        /// <summary>
        /// 放大传入的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowImage_Load(object sender, EventArgs e)
        {
            ImageFromWebTest();
            pictureBox2.Image = Resources._01;
            pictureBox3.Image = Resources._02;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Left:
                    index = index == 0 ? 0 : index - 1;
                    ImageFromWebTest();
                    return true;
                case Keys.Right:
                    index = index == items.Count - 1 ? items.Count - 1 : index + 1;
                    ImageFromWebTest();
                    return true;
                case Keys.F1:
                    this.Close();
                    return true;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ImageFromWebTest()
        {
            string url = Program.Urlpath + "/video/snapshot/" + items[index].Text;
            if (items[index].FileName.StartsWith("http"))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (WebResponse response = request.GetResponse())
                {
                    Image img = Image.FromStream(response.GetResponseStream());
                    Image bmp = new Bitmap(img);
                    img.Dispose();
                    pictureBox1.Image = bmp;
                }
            }
            else
            {

                Image img = Image.FromFile(items[index].FileName);
                Image bmp = new Bitmap(img);
                img.Dispose();
                pictureBox1.Image = bmp;
            }

            label2.Text = items[index].Text;
        }

        private void panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 110)
            {
                index = index == 0 ? items.Count - 1 : index - 1;
                ImageFromWebTest();
            }
            else if (e.Delta < -110)
            {
                index = index == items.Count - 1 ? 0 : index + 1;
                ImageFromWebTest();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            index = index == items.Count - 1 ? 0 : index + 1;
            ImageFromWebTest();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            index = index == 0 ? items.Count - 1 : index - 1;
            ImageFromWebTest();
        }
    }
}