using Common;
using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using Newtonsoft.Json.Linq;

namespace VideoRecordings
{
    public partial class VideoRecording : DevExpress.XtraEditors.XtraForm
    {

        List<string> labels = new List<string>(); //标签
        VideoPlay videoplay;      //传入的文件信息
        InformationDisplay information;   //文件窗口
        Process process = new Process();   //启动外部程序线程
        List<string> paths = new List<string>();  //截图路径
        Point po = new Point();         //窗口定位点
        int i = 1;            //Tag
        QueryVideo queryVideo;          //查询窗口
        List<string> imageurl = new List<string>();        //已有图片读取
        Dictionary<string, string> LabelsNumber = new Dictionary<string, string>();   // 标签对照
        Dictionary<string, string> AllLabel = new Dictionary<string, string>();
        bool folding = false;
        bool isquery;
        public VideoRecording(VideoPlay videopath, bool query ,InformationDisplay info)
        {
            information = info;
            videoplay = videopath;
            isquery = query;
            InitializeComponent();
            videoPlayer1.MyEvent += new DXApplication1.VideoPlayers_test.MyDelegate(ImageAdd);
            videoPlayer1.path = Program.ImageSavePath;       
        }

        public VideoRecording(VideoPlay videopath, bool query, QueryVideo video )
        {
            videoplay = videopath;
            queryVideo = video;
            isquery = query;
            InitializeComponent();
            videoPlayer1.MyEvent += new DXApplication1.VideoPlayers_test.MyDelegate(ImageAdd);
            videoPlayer1.path = Program.ImageSavePath;
        }

        private void VideoRecording_Load(object sender, EventArgs e)
        {
            PlayVideo();
            GetLabels();
            SetPoint();
            AlreadyLabels();
            GetIntToString();
            linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;    //鼠标悬停显示下划线
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;   //不显示下划线
            linkLabel1.Text = Program.ImageSavePath;
            timeEdit_start.Time = DateTime.Parse(videoplay.StartTime);
            timeEdit_end.Time = DateTime.Parse(videoplay.EndTime);
            dateTimePicker1.Text = videoplay.RecordTime;
            label6.Text = videoplay.Id.ToString();
            label6.ForeColor = Color.Red;
            imageListView1.Focus();
            imageListView1.DiskCache = Program.Persistent;
            Methods.AddIsTest(this);
            if (label_treeView.Nodes.Count != 0)
            {
                label_treeView.Nodes[0].Expand();
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelImage();
        }

        /// <summary>
        /// 刷新截图显示控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageAdd();
        }

        /// <summary>
        /// 标签打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = linkLabel1.Text;
            openFileDialog1.Filter = "所有文件(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// 使用外部播放器播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void other_button_Click(object sender, EventArgs e)
        {
            if (Program.VideoPlay != string.Empty)
            {
                if (!File.Exists(Program.VideoPlay))
                {
                    MessageBox.Show("没有指定播放器");
                    return;
                }
                process = Process.Start(Program.VideoPlay, Program.ReturnStringUrl(Methods.ConversionString(videoplay.Uri)));
                return;
            }
            else
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.VideoPlay = openFileDialog1.FileName;
                    Methods.WritePath(Program.PlayerPath,openFileDialog1.FileName);
                    process = Process.Start(Program.VideoPlay, Program.ReturnStringUrl(Methods.ConversionString(videoplay.Uri)));
                    Program.log.Info("使用关联播放器播放", new Exception($"Program.VideoPlay"));
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_button_Click(object sender, EventArgs e)
        {
            ImageAdd();
            SaveLabel();
            SaveImage();
            SaveTime();
            DeleteFolder(Program.ImageSavePath);
            videoplay.Labels = labels;
            VideoPlay play = Methods.GetNewImages(videoplay.Id);
            if (!isquery)
            {
                information.RefreshData(play);
                information.RefreshNewImage(play);
                information.Show();
                if (play.Labels.Count()!=0)
                {
                    information.RefshHomePage();
                }
            }
            else
            {
                queryVideo.RefreshData(play);
                queryVideo.RefreshNewImage(play);
                if (play.Labels.Count() != 0)
                {
                    queryVideo.RefshHomePage();
                }           
                queryVideo.Show();
            }
            this.Close();
        }

        /// <summary>
        /// 选择标签加入已有标签栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tree = label_treeView.SelectedNode;
            if (LabelsNumber.Values.Contains(tree.Text) || labels.Contains(tree.Text))
            {
                return;
            }
            labels.Add(tree.Text);
            SetLabelText();
        }

        /// <summary>
        /// 选择标签加入已有标签栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_treeView_DoubleClick(object sender, EventArgs e)
        {
            //TreeNode tree = label_treeView.SelectedNode;
            //if (labels.Contains(tree.Text))
            //{
            //    return;
            //}
            //labels.Add(tree.Text);
            //SetLabelText();
        }

        /// <summary>
        /// 双击删除已有的标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            DeleteLabel();
        }

        /// <summary>
        /// 清空所有已有的标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 清空已有标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labels.Clear();
            SetLabelText();
        }

        /// <summary>
        /// 重置外部播放器路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.VideoPlay = string.Empty;
        }

        /// <summary>
        /// 设置使用播放器状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (information.Hasbeen)
            {
                button1.Text = "使用外部播放器";
                information.Hasbeen = false;
                queryVideo.Hasbeen = false;
            }
            else
            {
                button1.Text = "使用页面播放器";
                information.Hasbeen = true;
                queryVideo.Hasbeen = true;
            }

        }
      //
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F6:
                    SetPoint();
                    videoPlayer1.Screenshots();
                    return true;
                case Keys.S:
                    save_button.PerformClick();
                    return true;
                case Keys.D:
                    DelImage();
                    return true;
                case Keys.Q:
                    ImageAdd();
                    return true;
                case Keys.Control | Keys.C:
                    copyToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Control | Keys.V:
                    pasteToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Z:
                    videoPlayer1.SetTime(0);
                    return true;
                case Keys.X:
                    videoPlayer1.SetTime(1);
                    return true;
                case Keys.C:
                    videoPlayer1.SetTime(2);
                    return true;
                case Keys.Space:
                    videoPlayer1.PlayOrPause();
                    return true;
                case Keys.E:
                    timeEdit_start.Focus();
                    return true;
                case Keys.R:
                    timeEdit_end.Focus();
                    return true;
                case Keys.T:
                    dateTimePicker1.Focus();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 关联关闭外部播放器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoRecording_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool start = information == null ? queryVideo.Hasbeen : information.Hasbeen;
            if (!start)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception)
                {
                    return;
                }

            }
        }

        /// <summary>
        /// 移动更新坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoRecording_Move(object sender, EventArgs e)
        {
            po = PointToScreen(new Point(videoPlayer1.Left, videoPlayer1.Top));
            videoPlayer1.point = po;
        }

        /// <summary>
        /// 删除已经选择的标签
        /// </summary>
        private void DeleteLabel()
        {
            TreeNode tree = treeView1.SelectedNode;
            if (tree == null) return;
            if (!labels.Contains(tree.Text))
            {
                return;
            }
            labels.Remove(tree.Text);
            treeView1.Nodes.Remove(tree);
        }

        /// <summary>
        /// 将已经截图的图片加入显示栏
        /// </summary>
        private void ImageAdd()
        {
            paths.Clear();
            if (Program.ImageSavePath == string.Empty)
            {
                MessageBox.Show("请设置截图读取路径,与图片保存保持一致");
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(Program.ImageSavePath);
            FileInfo[] fil = dir.GetFiles();
            if (fil.Count() == 0) return;
            foreach (FileInfo f in fil)
            {
                if (f.FullName.Split('.').Last() == "bmp" || f.FullName.Split('.').Last() == "BMP")
                {
                    Bitmap image = new Bitmap(f.FullName);
                    image.Save(f.FullName.Replace("bmp", "jpg"), ImageFormat.Jpeg);
                    image.Dispose();
                    File.Delete(f.FullName);
                }
                paths.Add(f.FullName.Replace("bmp", "jpg"));//添加文件的路径到列表
            }
            imageListView1.ThumbnailSize = new Size(150, 70);
            foreach (var item in paths)
            {
                imageListView1
                  .Items.Add(
                    new ImageListViewItem()
                    {
                        Text = item.Split('\\').Last(),
                        FileName = item,
                        Tag = i
                    });
            }
            i++;
        }

        /// <summary>
        /// 设置内部截图的坐标
        /// </summary>
        public void SetPoint()
        {
            po = PointToScreen(new Point(videoPlayer1.Left, videoPlayer1.Top));
            videoPlayer1.point = po;
        }

        /// <summary>
        /// Post保存标签
        /// </summary>
        public void SaveLabel()
        {
            string json = string.Empty;
            if (labels == null || labels.Count == 0)
            {
                json = "[]";
            }
            else
            {
                List<int> postlabel = labels.Select(t => StringToInt(t)).ToList();
                json = (new JavaScriptSerializer()).Serialize(postlabel);
            }
            string posturl = Program.Urlpath + $"/video/{videoplay.Id}/labels";          
            JObject obj = WebClinetHepler.Post_New(posturl, json);
            if (obj == null)
            {
                MessageBox.Show("添加标签失败");
                Program.log.Error($"上传标签失败{posturl}", new Exception($"{json}"));
                return;
            }
        }

        /// <summary>
        /// post保存图片
        /// </summary>
        public void SaveImage()
        {
            if (paths.Count == 0) return;
            string posturl = Program.Urlpath + $"/video/{videoplay.Id}/snapshots";
            List<string> saveimage = new List<string>();
            foreach (var item in paths)
            {
                saveimage.Add(GetPictureData(item));
            }
            foreach (var item in saveimage)
            {
                List<string> post = new List<string>();
                post.Add(item);
                string json = new JavaScriptSerializer().Serialize(post);
                //string json = $"[{item}]";
                JObject obj = WebClinetHepler.Post_New(posturl, json);
                if (obj == null)
                {
                    MessageBox.Show("添加图片失败");
                    return;
                }

            }
        }

        /// <summary>
        /// 获取标签集合加入选择标签栏
        /// </summary>
        public void GetLabels()
        {
            label_treeView.Nodes.Clear();
            LabelsNumber.Clear();
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj["result"] == null || obj["result"].Count() == 0)
            {
                return;
            }
            List<TreeNode> items = new List<TreeNode>();
            TreeNode used = new TreeNode { Text = "常用" };
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                TreeNode tree = new TreeNode { Text = obj["result"][i]["name"].ToString(), ForeColor = Color.Green };
                LabelsNumber.Add(obj["result"][i]["id"].ToString(), obj["result"][i]["name"].ToString());
                for (int j = 0; j < obj["result"][i]["children"].Count(); j++)
                {
                    TreeNode node = new TreeNode { Text = obj["result"][i]["children"][j]["name"].ToString(), Tag = obj["result"][i]["children"][j]["is_fre"].ToString(), ForeColor = Color.Red };
                    tree.Nodes.Add(node);
                    AllLabel.Add(obj["result"][i]["children"][j]["id"].ToString(), obj["result"][i]["children"][j]["name"].ToString());
                    if (node.Tag.ToString() == "1")
                    {
                        used.Nodes.Add(new TreeNode { Text = obj["result"][i]["children"][j]["name"].ToString(), ForeColor = Color.Red });
                    }
                }
                items.Add(tree);
            }
            items.OrderBy(t => t.Text);
            items.Insert(0, used);

            label_treeView.Nodes.AddRange(items.ToArray());
        }

        private int StringToInt(string text)
        {
            return int.Parse(AllLabel.Where(t => t.Value == text).First().Key);
        }

        /// <summary>
        /// 将已有标签加入标签栏
        /// </summary>
        private void AlreadyLabels()
        {
            foreach (var item in videoplay.Labels)
            {
                TreeNode sonItem = new TreeNode();
                sonItem.Text = sonItem.Name = item;
                sonItem.ForeColor = Color.Blue;
                sonItem.NodeFont = new Font("Arial", 9);
                treeView1.Nodes.Add(sonItem);
                labels.Add(item);
            }

        }

        /// <summary>
        /// 将已经选择的标签加入标签栏
        /// </summary>
        private void SetLabelText()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.AddRange(labels.Select(t => new TreeNode
            {
                Text = t,
                Name = t,
                ForeColor = Color.Blue,
                NodeFont = new Font("Arial", 12)
            }).ToArray());
        }

        /// <summary>
        /// 播放视频
        /// </summary>
        private void PlayVideo()
        {
            bool start = information == null ? queryVideo.Hasbeen : information.Hasbeen;
            if (start)
            {
                try
                {
                    process = Process.Start(Program.VideoPlay, Program.ReturnStringUrl(Methods.ConversionString(videoplay.Uri)));
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请重新设置播放器");
                    Program.log.Debug($"播放视频失败", ex);
                    return;
                }
            }
            else
            {
                videoPlayer1.URL = Program.ReturnStringUrl(Methods.ConversionString(videoplay.Uri));
                videoPlayer1.VideoPalying();
            }
        }

        /// <summary>
        /// 将图片id转成需要用到的url字符串
        /// </summary>
        public void GetIntToString()
        {
            imageurl.Clear();
            string url = Program.Urlpath + "/video/snapshot/";
            foreach (var item in videoplay.ImageId)
            {
                imageurl.Add(url + item);
            }
            SetTheListView();
        }

        /// <summary>
        /// 图片加入集合
        /// </summary>
        public void SetTheListView()
        {
            imageListView1.Items.Clear();
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            foreach (string imageUrl in imageurl)
            {
                ImageListViewItem item = new ImageListViewItem(imageUrl) { Text = imageUrl.Split('/').Last() };
                items.Add(item);
            }
            imageListView1.ThumbnailSize = new Size(180, 90);
            imageListView1.Items.AddRange(items.ToArray());

        }

        /// <summary>
        /// 图片转二进制(base64)
        /// </summary>
        /// <param name="imagepath">图片地址</param>
        /// <returns>二进制</returns>
        public string GetPictureData(string imagepath)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            string str = Convert.ToBase64String(byData);
            fs.Close();
            return str;
        }

        /// <summary>
        /// 本机屏幕截图
        /// </summary>
        public void CaptureTheScreen()
        {
            Point point = new Point(po.X, po.Y + 50);
            Bitmap bit = new Bitmap(videoPlayer1.Width, videoPlayer1.Height - 150);
            Graphics g = Graphics.FromImage(bit);
            g.CopyFromScreen(point, new Point(0, 0), bit.Size);
            string photoname = DateTime.Now.Ticks.ToString();
            bit.Save(Program.ImageSavePath + "\\" + photoname + ".jpg", ImageFormat.Jpeg);
            g.Dispose();
        }

        /// <summary>
        /// 删除图片 本地..网络
        /// </summary>
        public void DelImage()
        {
            string url = Program.Urlpath + "/video/snapshot/";
            if (imageListView1.SelectedItems.Count == 0)
            {
                return;
            }
            foreach (var item in imageListView1.SelectedItems)
            {
                if (imageurl.Contains(item.FileName))
                {
                    JObject obj = WebClinetHepler.Delete_New(url + item.Text.Split('/').Last());
                    if (obj == null)
                    {
                        MessageBox.Show(url + item.Text.Split('/').Last() + "删除失败");

                        return;
                    }
                    imageListView1.Items.Remove(item);
                    imageurl.Remove(url + item.Text.Split('/').Last());
                }
                else
                {
                    File.Delete(item.FileName);
                    paths.Remove(item.FileName);
                    imageListView1.Items.Remove(item);
                }
            }
            SetTheListView();
            ImageAdd();
        }

        /// <summary>
        /// 双击放大图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageListView1_DoubleClick(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }

        /// <summary>
        /// 保存时间
        /// </summary>
        private void SaveTime()
        {
            Dictionary<string, string> patchjson = new Dictionary<string, string>();
            string start = timeEdit_start.Text;
            string end = timeEdit_end.Text;
            string url = Program.Urlpath + "/video/" + videoplay.Id;
            patchjson.Add("start_time", start);
            patchjson.Add("end_time", end);
            patchjson.Add("record_time", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            string json = (new JavaScriptSerializer()).Serialize(patchjson);
            JObject obj = WebClinetHepler.Patch_New(url, json);
            if (obj == null)
            {
                MessageBox.Show("起止时间上传失败");
                Program.log.Error($"上传起止时间出错{url}", new Exception($"{json}"));
                return;
            }
        }

        /// <summary>
        /// 清空指定的文件夹，但不删除文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        DeleteFolder(d1.FullName);////递归删除子文件夹
                    }
                    Directory.Delete(d);
                }
            }
            Program.log.Debug($"清空文件夹{dir}");
        }

        /// <summary>
        /// 打开关闭节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!folding)
            {
                label_treeView.ExpandAll();
                folding = true;
            }
            else
            {
                label_treeView.CollapseAll();
                folding = false;
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labels.Count==0)
            {
                return;
            }
            Program.labels =Methods.CopyToList(labels);          
            Program.log.Info($"复制{string.Join(",",labels)}");
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.labels.Count==0)
            {
                return;
            }
            foreach (var item in Program.labels)
            {
                if (AllLabel.Values.Contains(item))
                {
                    if (labels.Contains(item)) continue;
                    labels.Add(item);
                    SetLabelText();
                }              
            }
            Program.log.Info($"粘贴{string.Join(",",Program.labels)}到{videoplay.Id.ToString()}");
        }

        private void openimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }

    }
}
