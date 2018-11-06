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
using VideoRecordings.GetDatas;
using VideoRecordings.Models;
using System.Reflection;
using System.Threading.Tasks;
using VideoRecordings.Label;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils.Menu;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;

namespace VideoRecordings
{
    public partial class VideoRecording : Form
    {

        TypeLabels Selectlabels = new TypeLabels(); //标签
        VideoPlay videoplay;      //传入的视频
        List<VideoPlay> videos;
        Process process = new Process();   //启动外部程序线程
        List<string> paths = new List<string>();  //截图路径
        int i = 1;            //Tag
        int pageindex = 1;
        List<string> imageurl = new List<string>();        //已有图片读取
        TypeLabels staticlabel = new TypeLabels();
        bool folding = false;

        public VideoRecording(VideoPlay videopath,List<VideoPlay> videoPlays)
        {
            videoplay = videopath;
            videos = videoPlays;
            InitializeComponent();
            videoPlayers1.MyEvent += new Aplayer.VideoPlayers.MyDelegate(ImageAdd);
            videoPlayers1.path = AppSettings.ImageSavePath;
            dropDownButton1.DropDownControl = CreateDXPopupMenu();
            SetAllListNodes(videoPlays);
        }


        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public virtual void OnSave()
        {
            MyEvent?.Invoke();
        }

        private void VideoRecording_Load(object sender, EventArgs e)
        {
  
            linkLabel1.Text = AppSettings.ImageSavePath;
            imageListView1.Focus();
            imageListView1.DiskCache = AppSettings.Persistent;
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
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void save_button_ClickAsync(object sender, EventArgs e)
        {
            ImageAdd();
            await SaveInfo();
            DeleteFolder(AppSettings.ImageSavePath);
            if (treeList1.FocusedNode==treeList1.Nodes.LastNode.LastNode)
            {
                treeList1.FocusedNode = treeList1.Nodes.FirstNode;
            }
            treeList1.MoveNext();
            while (treeList1.FocusedNode.Level==0)
            {
                treeList1.MoveNext();
            }
        }

        private async Task SaveInfo()
        {
            var islabel =  SaveLabelAsync();
            var isimage =  SaveImageAsync();
            var istime  =  SaveTimeAsync();
            await Task.WhenAll(islabel,isimage,istime);
            if (islabel.Result & isimage.Result & istime.Result)
                return;
            MessageBox.Show("保存失败");
        }

        /// <summary>
        /// 选择标签加入已有标签栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (label_treeView.SelectedNode.Level != 0)
            {
                TreeNode parentnode = label_treeView.SelectedNode.Parent;
                if (Selectlabels.Count(t => t.Name == parentnode.Text) > 0)
                {
                    foreach (TypeLabel tree in Selectlabels)
                    {
                        if (tree.Name == parentnode.Text)
                        {
                            if (tree.Labels.Count(t => t.Name == label_treeView.SelectedNode.Text) > 0)
                                return;
                            tree.Labels.Add(Methods.GetNodeToVideoLabel(label_treeView.SelectedNode));
                            break;
                        }
                    }
                }
                else
                {
                    TypeLabel typeLabel = Methods.GetNodeToTypeLabel(parentnode);
                    typeLabel.Labels.Add(Methods.GetNodeToVideoLabel(label_treeView.SelectedNode));
                    Selectlabels.Add(typeLabel);
                }
                SetLabelText();
            }
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
            Selectlabels.Clear();
            SetLabelText();
        }

        /// <summary>
        /// 重置外部播放器路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoPath path = AppSettings.VideoPlayPath.FirstOrDefault(t=>t.Name==dropDownButton1.Text);
            if (path!=null)
            {
                AppSettings.VideoPlayPath.Remove(path);
            }
            Methods.WritePath(AppSettings.PlayerPath, AppSettings.VideoPlayPath);
        }

        //
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F6:
                    videoPlayers1.Screenshots();
                    return true;
                case Keys.S:
                    save_button.PerformClick();
                    return true;
                case Keys.D:
                    DelImage();
                    return true;
                case Keys.F:
                    ImageAdd();
                    return true;
                case Keys.Control | Keys.C:
                    copyToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Control | Keys.V:
                    pasteToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Z:
                    videoPlayers1.SetTime(0);
                    return true;
                case Keys.X:
                    videoPlayers1.SetTime(1);
                    return true;
                case Keys.C:
                    videoPlayers1.SetTime(2);
                    return true;
                case Keys.Space:
                    videoPlayers1.PlayOrPause();
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
                case Keys.Q:
                    ShowStaticLabels();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        /// <summary>
        /// 删除已经选择的标签
        /// </summary>
        private void DeleteLabel()
        {
            TreeNode tree = treeView1.SelectedNode;
            if (tree == null) return;
            if (tree.Level == 0)
            {
                if (MessageBox.Show("删除父标签将删除所有子标签,是否删除?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
                TypeLabel type = Selectlabels.FirstOrDefault(t => t.Name == tree.Text);
                treeView1.Nodes.Remove(tree);
                Selectlabels.Remove(type);
                return;
            }
            else
            {
                TypeLabel type = Selectlabels.FirstOrDefault(t => t.Name == tree.Parent.Text);
                if (tree.Parent.Nodes.Count == 1)
                {
                    Selectlabels.Remove(type);
                    treeView1.Nodes.Remove(tree.Parent);
                    return;
                }
                VideoLabel label = type.Labels.FirstOrDefault(t => t.Name == tree.Text);
                type.Labels.Remove(label);
                treeView1.Nodes.Remove(tree);
            }

        }

        /// <summary>
        /// 将已经截图的图片加入显示栏
        /// </summary>
        private void ImageAdd()
        {
            paths.Clear();
            if (AppSettings.ImageSavePath == string.Empty)
            {
                MessageBox.Show("请设置截图读取路径,与图片保存保持一致");
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(AppSettings.ImageSavePath);
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
            imageListView1.ThumbnailSize = new Size(180, 90);
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
        /// Post保存标签
        /// </summary>
        public async Task<bool> SaveLabelAsync()
        {
            List<VideoLabel> ids = new List<VideoLabel>();
            foreach (TypeLabel tree in staticlabel.Union(Selectlabels))
            {
                ids.AddRange(tree.Labels);
            }
            bool win = await LabelData.AddLabelToVideoAsync(videoplay.Id, ids.Select(t => t.Id).ToList());
            return win;
        }

        /// <summary>
        /// post保存图片
        /// </summary>
        public async Task<bool> SaveImageAsync()
        {
            List<string> saveimage = new List<string>();
            foreach (var item in paths)
            {
                saveimage.Add(GetPictureData(item));
            }
            string json = JsonConvert.SerializeObject(saveimage);
            bool win = await VideoData.SaveImageAsync(videoplay.Id, json);
            return win;
        }

        /// <summary>
        /// 获取标签集合加入选择标签栏
        /// </summary>
        public void GetLabels()
        {
            label_treeView.Nodes.Clear();
            List<TypeLabel> typeLabels = new MyLabels(videoplay.Id).DynamicLabel;
            List<TreeNode> items = new List<TreeNode>();
            foreach (TypeLabel labels in typeLabels)
            {
                TreeNode tree = new TreeNode() { Text = Name = labels.Name, Tag = labels.Id, ForeColor = Color.Red };
                foreach (VideoLabel label in labels.Labels)
                {
                    TreeNode node = new TreeNode() { Text = Name = label.Name, Tag = label.Id, ForeColor = Color.Blue };
                    tree.Nodes.Add(node);
                }

                items.Add(tree);
            }
            items.OrderBy(t => t.Text);
            label_treeView.Nodes.AddRange(items.ToArray());
        }

        /// <summary>
        /// 将已有标签加入标签栏
        /// </summary>
        //private void AlreadyLabels()
        //{
        //    foreach (var item in videoplay.Labels)
        //    {
        //        TreeNode tree = new TreeNode() { Text = Name = item.Name, Tag = item.Id };
        //        foreach (var it in item.Labels)
        //        {
        //            TreeNode node = new TreeNode() { Text = Name = it.Name, Tag = it.Id };
        //            tree.Nodes.Add(node);
        //        }
        //    }
        //}

        /// <summary>
        /// 将已经选择的标签加入标签栏
        /// </summary>
        private void SetLabelText()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            List<TreeNode> items = new List<TreeNode>();
            foreach (TypeLabel item in Selectlabels)
            {
                TreeNode tree = new TreeNode() { Text = Name = item.Name, Tag = item.Id, ForeColor = Color.Red };
                foreach (VideoLabel it in item.Labels)
                {
                    TreeNode node = new TreeNode() { Text = Name = it.Name, Tag = it.Id, ForeColor = Color.Blue };
                    tree.Nodes.Add(node);
                }

                items.Add(tree);
            }
            items.OrderBy(t => t.Text);
            treeView1.Nodes.AddRange(items.ToArray());
            treeView1.EndUpdate();
            treeView1.ExpandAll();
        }

        /// <summary>
        /// 播放视频
        /// </summary>
        private void PlayVideo()
        {
            if (toggleSwitch1.IsOn)
            {
                try
                {
                    process = Process.Start(AppSettings.VideoPlayPath.FirstOrDefault(t=>t.Name==dropDownButton1.Text).Path, AppSettings.ReturnStringUrl(Methods.ConversionString(videoplay.Uri)));
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
                videoPlayers1.URL = AppSettings.ReturnStringUrl(Methods.ConversionString(videoplay.Uri));
                videoPlayers1.VideoPalying();
            }
        }

        /// <summary>
        /// 将图片id转成需要用到的url字符串
        /// </summary>
        public void GetIntToString()
        {
            imageurl.Clear();
            string url = AppSettings.Urlpath + "/video/snapshot/";
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
        ///// 本机屏幕截图
        ///// </summary>
        //public void CaptureTheScreen()
        //{
        //    Point point = new Point(po.X, po.Y + 50);
        //    Bitmap bit = new Bitmap(videoPlayer1.Width, videoPlayer1.Height - 150);
        //    Graphics g = Graphics.FromImage(bit);
        //    g.CopyFromScreen(point, new Point(0, 0), bit.Size);
        //    string photoname = DateTime.Now.Ticks.ToString();
        //    bit.Save(Program.ImageSavePath + "\\" + photoname + ".jpg", ImageFormat.Jpeg);
        //    g.Dispose();
        //}

        /// <summary>
        /// 删除图片 本地..网络
        /// </summary>
        public void DelImage()
        {
            string url = AppSettings.Urlpath + "/video/snapshot/";
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
            Program.log.Info($"删除图片{string.Join(",",imageListView1.SelectedItems.Select(t=>t.Name).ToList())}");
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
        private async Task<bool> SaveTimeAsync()
        {
            Dictionary<string, string> patchjson = new Dictionary<string, string>();
            string start = timeEdit_start.Text;
            string end = timeEdit_end.Text;
            patchjson.Add("start_time", start);
            patchjson.Add("end_time", end);
            patchjson.Add("record_time", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            string json = JsonConvert.SerializeObject(patchjson);
            bool win = await VideoData.SaveTimeAsync(videoplay.Id, json);
            return win;
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
            if (Selectlabels.Count == 0)
            {
                return;
            }
            //Program.labels =(TypeLabels)Selectlabels.Clone();            
            Program.labels = (TypeLabels)Selectlabels.Clone();
            Program.log.Info($"复制{string.Join(",", Program.labels)}");
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.labels.Count == 0)
            {
                return;
            }
            foreach (TypeLabel item in Program.labels)
            {
                if (!Selectlabels.Contains(item))
                {
                    Selectlabels.Add((TypeLabel)item.Clone());
                    continue;
                }
                foreach (TypeLabel tree in Selectlabels)
                {
                    if (tree == item)
                    {
                        foreach (VideoLabel label in item.Labels)
                        {
                            if (!tree.Labels.Contains(label))
                            {
                                tree.Labels.Add((VideoLabel)label.Clone());
                            }
                        }
                    }

                }
            }
            SetLabelText();
            Program.log.Info($"粘贴{string.Join(",", Program.labels)}到{videoplay.Id.ToString()}");
        }

        private void openimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }

        private void ShowStaticLabels()
        {
            int id = videoplay.EquipmentID;
            ShowStaticLabel show = new ShowStaticLabel(id);
            show.Show();
        }

        private void SetAllListNodes(List<VideoPlay> videoplays)
        {
            if (videoplays == null) return;
            treeList1.BeginUpdate();
            treeList1.Nodes.Clear();
            TreeListNode selectnode = new TreeListNode();
            foreach (IGrouping<string,VideoPlay> item in videoplays.GroupBy(t=>t.EquipmentName.Split(':').First()).OrderByDescending(t=>int.Parse(t.Key.Split(':').First())))
            {
                TreeListNode ParentNode = treeList1.AppendNode(null, null);
                ParentNode.SetValue(treeList1.Columns[0],item.First().EquipmentName);
                foreach (VideoPlay video in item.OrderBy(t=>t.Id))
                {
                    TreeListNode tree = ParentNode.Nodes.Add("");
                    tree.SetValue(treeList1.Columns[0], video.Id);
                    tree.Tag = pageindex;                   
                    if (video.Id==videoplay.Id)
                    {
                        selectnode = tree;
                    }
                    pageindex++;
                }
            }
            treeList1.EndUpdate();
            treeList1.CollapseAll();
            if (selectnode != null)
                treeList1.FocusedNode = selectnode;
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeList1.FocusedNode.Level == 0) return;
            videoplay = VideoData.GetIndexVideoInfo(int.Parse(treeList1.FocusedNode.GetValue(0).ToString()));
            SetFocusInfo();
            SetLabel(int.Parse(treeList1.FocusedNode.Tag.ToString()));
            PlayVideo();
        }

        private void SetFocusInfo()
        {
            Selectlabels = (TypeLabels)videoplay.Labels.DynamicLabel.Clone();
            staticlabel = (TypeLabels)videoplay.Labels.StaticLabel.Clone();
            timeEdit_start.Time = DateTime.Parse(videoplay.StartTime);
            timeEdit_end.Time = DateTime.Parse(videoplay.EndTime);
            dateTimePicker1.Text = videoplay.RecordTime;
            GetLabels();
            GetIntToString();
            SetLabelText();
        }

        private void VideoRecording_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            OnSave();
            videoPlayers1.Dispose();
        }

        /// <summary>
        /// 添加播放器选择
        /// </summary>
        /// <returns></returns>
        private DXPopupMenu CreateDXPopupMenu()
        {
            DXPopupMenu menu = new DXPopupMenu();
            foreach (var item in AppSettings.Paths)
            {
                menu.Items.Add(new DXMenuItem(item, OnItemClick));
            }          
            return menu;
        }

        private void OnItemClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            dropDownButton1.Text = item.Caption;
        }

        /// <summary>
        /// 使用外部播放器播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            VideoPath path = AppSettings.VideoPlayPath.FirstOrDefault(t => t.Name == dropDownButton1.Text);
            if (path != null)
            {
                if (!File.Exists(path.Path))
                {
                    MessageBox.Show("没有指定播放器");
                    return;
                }
                process = Process.Start(path.Path, AppSettings.ReturnStringUrl(Methods.ConversionString(videoplay.Uri)));
                return;
            }
            else
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    VideoPath videoPath = new VideoPath() { Name = dropDownButton1.Text };
                    videoPath.Path = openFileDialog1.FileName;
                    process = Process.Start(videoPath.Path, AppSettings.ReturnStringUrl(Methods.ConversionString(videoplay.Uri)));
                    Program.log.Info("使用关联播放器播放", new Exception($"Program.VideoPlay"));
                    AppSettings.VideoPlayPath.Add(videoPath);
                    Methods.WritePath(AppSettings.PlayerPath, AppSettings.VideoPlayPath);
                }
            }
        }

        private void SetLabel(int index)
        {
            label2.Text = $"{index}/{videos.Count}";
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            try
            {
                if (e.SelectedControl is DevExpress.XtraTreeList.TreeList)

                {

                    TreeList tree = (TreeList)e.SelectedControl;

                    TreeListHitInfo hit = tree.CalcHitInfo(e.ControlMousePosition);

                    if (hit.HitInfoType == HitInfoType.Cell)

                    {

                        object cellInfo = new TreeListCellToolTipInfo(hit.Node, hit.Column, null);

                        string toolTip = $"{videos.FirstOrDefault(t => t.Id == int.Parse(hit.Node[hit.Column].ToString())).Name}";

                        //string.Format("{0} (Column: {1}, Node ID: {2})", hit.Node[hit.Column],
                        //hit.Column.VisibleIndex, hit.Node.Id);

                        e.Info = new DevExpress.Utils.ToolTipControlInfo(cellInfo, toolTip);

                    }

                }
            }
            catch (Exception)
            {
                TreeList tree = (TreeList)e.SelectedControl;
                TreeListHitInfo hit = tree.CalcHitInfo(e.ControlMousePosition);
                object cellInfo = new TreeListCellToolTipInfo(hit.Node, hit.Column, null);
                string toolTip = $"{hit.Node[hit.Column]}";
                e.Info = new DevExpress.Utils.ToolTipControlInfo(cellInfo, toolTip);
            }
      
        }
    }
}
