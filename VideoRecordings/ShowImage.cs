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

namespace VideoRecordings
{
    public partial class ShowImage : DevExpress.XtraEditors.XtraForm
    {
        List<ImageListViewItem> items;
        public ShowImage(List<ImageListViewItem> item)
        {
            InitializeComponent();
            items=item;
        }
        /// <summary>
        /// 放大传入的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowImage_Load(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(700,700);
            imageListView1.Items.AddRange(items.ToArray());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);    
        }
    }
}