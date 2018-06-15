using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRecordings.Images
{
    public partial class ImagePull : Form
    {
        ImageDisplay display;
        ImagePlay play;
        List<string> paths = new List<string>();
        List<string> size = new List<string>() { "96*96", "124*124", "196*196", "240*240" };
        List<string> size1 = new List<string>() { "96*96", "124*124", "196*196", "240*240" };
        private string selectFilterPath = string.Empty;
        private string selectImagePath = string.Empty;
        private string selectTxtPath = string.Empty;
        List<string> images = new List<string>();
        const int ShowSize= 10;
        int imagesize;
        int filtersize;

        public ImagePull(ImageDisplay imageDisplay, ImagePlay imagePlay)
        {
            InitializeComponent();
            display = imageDisplay;
            play = imagePlay;
            selectImagePath = imagePlay.Uri;      
            selectFilterPath = Path.Combine(selectImagePath, "filter");          
        }

        private void ImagePull_Load(object sender, EventArgs e)
        {
            images = Directory.GetFiles(selectImagePath).ToList();
            imagesize = display.imagesize;
            filtersize = display.fittersize;
            comboBox1.DataSource = size;
            comboBox2.DataSource = size1;
            SetComIndex(comboBox1,imagesize);
            SetComIndex(comboBox2,filtersize);
            SetImage();
        }

        private void SetComIndex(ComboBox box,int num)
        {
            int i = 0;
            foreach (var item in box.Items)
            {
                if (item.ToString().StartsWith(num.ToString()))
                {
                    box.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        private void RefrashImage()
        {
            List<string> showimage = GenerateRandomNumber(ShowSize, images.Count);
            imageListView_image.Items.Clear();
            if (Directory.Exists(play.Uri))
            {
                imageListView_image
                    .Items.AddRange(showimage.OrderBy(t => t).Select(t =>
                    new ImageListViewItem
                    {
                        Text = t.Split('\\').Last().Substring(0, t.Split('\\').Last().Length - 4),
                        FileName = t,
                        Tag = t
                    }).ToArray());
            }
            toolStripStatusLabel1.Text = imageListView_image.Items.Count.ToString();
        }

        //private List<string> GetListTreeNode()
        //{
        //    List<string> tree = new List<string>();
        //    var files = Directory.GetFiles(play.Uri);
        //    foreach (var item in files)
        //    {
        //        paths.Add(item);
        //        tree.Add(item.Split('\\').Last());
        //    }
        //    return tree;
        //}

        //private List<TreeNode> GetTreeNode()
        //{
        //    int i = 1;
        //    List<TreeNode> nodes = new List<TreeNode>();
        //    foreach (var item in GetListTreeNode())
        //    {
        //        TreeNode node = new TreeNode() { Name = item, Text = item, Tag =i++};
        //        nodes.Add(node);
        //    }
        //    return nodes;
        //}

        private void imageListView_image_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Buttons == MouseButtons.Left )
            {
                e.Item.Checked = !e.Item.Checked;
                e.Item.BackColor = e.Item.Checked ? Color.PowderBlue:Color.White;
            }
        }

        private void imageListView_image_DoubleClick(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView_image);
        }

        private void imageListView_fitter_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //DataObject data = e.Data as DataObject;
                //if (data == null) return;
                //Dictionary<string, string> images = (Dictionary<string, string>)data.GetData(DataFormats.FileDrop);
                if (!Directory.Exists(selectFilterPath))
                    Directory.CreateDirectory(selectFilterPath);
                foreach (var item in imageListView_image.CheckedItems)
                {
                    if (File.Exists(item.FileName.Replace(selectImagePath, selectFilterPath)))
                    {
                        continue;
                    }
                    File.Copy(item.FileName, item.FileName.Replace(selectImagePath, selectFilterPath));
                }                        
                SetImage();
                //OperationLog.InfoLog(_className, $"choose\tfilter_image\t{images.Count}\tchoose {images.Count} pic from image to filter");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RefrashFilterImage()
        {
            imageListView_filter.Items.Clear();
            if (Directory.Exists(selectFilterPath))
            {
                imageListView_filter
                    .Items.AddRange(Directory.GetFiles(selectFilterPath).OrderBy(t => t).Select(t =>
                    new ImageListViewItem
                    {
                        Text = t.Split('\\').Last().Substring(0, t.Split('\\').Last().Length - 4),
                        FileName = t,
                        Tag = t
                    }).ToArray());
            }
        }

        private void SetImage()
        {
            RefrashImage();
            RefrashFilterImage();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {   
                case Keys.D:
                    DeleteFilter();
                    break;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DeleteFilter()
        {
            foreach (var item in imageListView_filter.SelectedItems)
            {
                File.Delete(item.FileName);
            }
            RefrashFilterImage();
        }

        public List<string> GenerateRandomNumber(int Length,int max)
        {
            if (Length > max) Length = max;
            List<string> newRandom = new List<string>(Length);
            List<int> vs = new List<int>();
            Random rd = new Random();
            while (vs.Count< Length)
            {
                int index = rd.Next(max);
                if (vs.Contains(index)) continue;
                newRandom.Add(images[index]);
                vs.Add(index);
            }
            return newRandom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefrashImage();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {          
            imageListView_image.ThumbnailSize = new Size(int.Parse(comboBox1.Text.Split('*').First()), int.Parse(comboBox1.Text.Split('*').Last()));
            display.imagesize = int.Parse(comboBox1.Text.Split('*').First());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageListView_filter.ThumbnailSize = new Size(int.Parse(comboBox2.Text.Split('*').First()), int.Parse(comboBox2.Text.Split('*').Last()));
            display.fittersize = int.Parse(comboBox2.Text.Split('*').First());
        }

        private void imageListView_image_SelectionChanged(object sender, EventArgs e)
        {
            bool _checked = !imageListView_image.SelectedItems.All(s => s.Checked);
            foreach (var item in imageListView_image.SelectedItems)
            {
                item.Checked = _checked;
            }
        }


    }
}
