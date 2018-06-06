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
       public ImagePull(ImageDisplay imageDisplay, ImagePlay imagePlay)
        {
            InitializeComponent();
            display = imageDisplay;
            play = imagePlay;
        }

        private void ImagePull_Load(object sender, EventArgs e)
        {
            
            List<TreeNode> nodes = GetTreeNode();
            toolStripStatusLabel1.Text = $"0/{nodes.Count}";
            treeView_image.Nodes.AddRange(nodes.ToArray());
            RefrashImage();
        }


        private void RefrashImage()
        {
            imageListView_image.Items.Clear();
            if (Directory.Exists(play.Uri))
            {
                imageListView_image
                    .Items.AddRange(Directory.GetFiles(play.Uri).OrderBy(t => t).Select(t =>
                    new ImageListViewItem
                    {
                        Text = t.Split('\\').Last().Substring(0, t.Split('\\').Last().Length - 4),
                        FileName = t,
                        Tag = t
                    }).ToArray());
            }
        }

        private List<string> GetListTreeNode()
        {
            List<string> tree = new List<string>();
            var files = Directory.GetFiles(play.Uri);
            foreach (var item in files)
            {
                paths.Add(item);
                tree.Add(item.Split('\\').Last());
            }
            return tree;
        }

        private List<TreeNode> GetTreeNode()
        {
            int i = 1;
            List<TreeNode> nodes = new List<TreeNode>();
            foreach (var item in GetListTreeNode())
            {
                TreeNode node = new TreeNode() { Name = item, Text = item, Tag =i++};
                nodes.Add(node);
            }
            return nodes;
        }

        private void imageListView_image_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Buttons == MouseButtons.Left )
            {
                e.Item.Checked = !e.Item.Checked;
            }
        }
    }
}
