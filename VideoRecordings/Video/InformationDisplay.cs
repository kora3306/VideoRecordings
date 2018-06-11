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
using Common;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.IO;
using Manina.Windows.Forms;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Diagnostics;

namespace VideoRecordings
{
    public partial class InformationDisplay : DevExpress.XtraEditors.XtraForm
    {
        public VideoPlay transmissionvideo = new VideoPlay();     //当前选中的文件
        public List<VideoPlay> videoplays = new List<VideoPlay>();  //所有文件
        public List<string> imageurl = new List<string>();   //图片url
        GridHitInfo hInfo = new GridHitInfo();
        VideoInformation information;     //主文件夹窗口
        VideoProject project;      //传入的文件夹
        bool isFirst = true;      //初次定位文件夹为0
        public bool Hasbeen = false;   //是否一致使用外部播放器
        public InformationDisplay(VideoInformation info, VideoProject focusedfolder)
        {
            project = focusedfolder;
            InitializeComponent();
            information = info;
            PostVideos();
            if (project!=null)
            {
                toolStripStatusLabel1.Text = $"视频 :{project.Name}    {project.Place}";
            }           
        }

        /// <summary>
        /// 加载姓名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformationDisplay_Load(object sender, EventArgs e)
        {
            label2.Text = $"欢迎:{Program.UserName}";          
        }

        /// <summary>
        /// 打开文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenVideoPaly();
        }

        /// <summary>
        /// 删除图片按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.DelImage(imageListView1, imageurl);
            PostVideos();
            gridView1.RefreshData();
        }

        /// <summary>
        /// 点击加载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_Click(object sender, EventArgs e)
        {
            transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
            GetIntToString();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == 0 ? 0 : gridView1.FocusedRowHandle - 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                    GetIntToString();
                    return true;
                case Keys.Down:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                    GetIntToString();
                    return true;
                case Keys.D:
                    DELToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    information.Show();
                    return true;
                case Keys.F2:
                    Methods.OpenFolderAndSelectFile(Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri)));
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
        /// 双击打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (hInfo.InRowCell)
                OpenVideoPaly();
        }

        /// <summary>
        /// 焦点改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                return;
            }
            transmissionvideo = (VideoPlay)gridView1.GetRow(rowIndex);
            RefreshImage();
            DeleteFolder(Program.ImageSavePath);
        }

        /// <summary>
        /// 返回主信息页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformationDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!Program.IsQuery)
            //{
            //    information.Show();
            //}
        }


        /// <summary>
        /// 加载图片集合
        /// </summary>
        public void GetIntToString()
        {
            if (transmissionvideo == null || transmissionvideo?.ImageId.Count == 0)
            {
                imageListView1.Items.Clear();
                return;
            }
            imageurl.Clear();
            string url = Program.Urlpath + "/video/snapshot/";
            foreach (var item in transmissionvideo.ImageId)
            {
                imageurl.Add(url + item);
            }
            SetTheListView();
        }

        /// <summary>
        /// 控件加载图片
        /// </summary>
        public void SetTheListView()
        {
            imageListView1.Items.Clear();
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            imageListView1.ThumbnailSize = new Size(300, 180);
            foreach (string imageUrl in imageurl)
            {
                ImageListViewItem item = new ImageListViewItem(imageUrl) { Text = imageUrl.Split('/').Last() };
                items.Add(item);
            }
            imageListView1.Items.AddRange(items.ToArray());
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
            Program.log.Error($"清空{dir}");
        }

        /// <summary>
        /// 读取并加载信息
        /// </summary>
        public void PostVideos()
        {
            if (project == null)
            {
                return;
            }
            string posturl = Program.Urlpath + "/scan/video/project/" + project.Id.ToString();
            string geturl = Program.Urlpath + "/videos";
            string conditions = "project_name=" + project.Name;
            JObject obj = WebClinetHepler.Post_New(posturl);
            if (obj == null)
            {
                MessageBox.Show("扫描文件失败");
                Program.log.Error($"扫描{posturl}", new Exception("扫描失败"));
            }
            GettListVideo(geturl, conditions);
            if (isFirst)
            {
                transmissionvideo = videoplays.Count == 0 ? null : videoplays.First();
                isFirst = false;
            }
            RefreshImage();
            GetIntToString();
            bindingSource1.DataSource = videoplays;
        }

        /// <summary>
        /// 刷新标签
        /// </summary>
        /// <param name="play"></param>
        public void RefreshData(VideoPlay play)
        {
            foreach (var item in videoplays)
            {
                if (item.Name == play.Name)
                {
                    item.Labels = play.Labels;
                    item.ImageId = play.ImageId;
                    item.StartTime = play.StartTime;
                    item.EndTime = play.EndTime;
                    item.RecordTime = play.RecordTime;
                    break;
                }
            }
            bindingSource1.DataSource = videoplays;
        }

        public void RefreshNewImage(VideoPlay video)
        {
            imageurl.Clear();
            string url = Program.Urlpath + "/video/snapshot/";
            foreach (var item in video.ImageId)
            {
                imageurl.Add(url + item);
            }
            SetTheListView();
        }

        /// <summary>
        /// 刷新焦点行图片集合
        /// </summary>
        public void RefreshImage()
        {
            foreach (var item in videoplays)
            {
                if (transmissionvideo.Id == item.Id)
                {
                    transmissionvideo = item;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取所有文件信息,转化为集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public void GettListVideo(string url, string na)
        {
            videoplays.Clear();
            JObject obj = WebClinetHepler.GetJObject(url, na);
            if (obj == null) return;
            videoplays = JsonHelper.DeserializeDataContractJson<List<VideoPlay>>(obj["videos"].ToString());
            Program.log.Error($"获取{url}信息", new Exception("获取成功"));
        }

        /// <summary>
        /// 打开文件并播放
        /// </summary>
        private void OpenVideoPaly()
        {
            if (transmissionvideo == null || transmissionvideo.Uri == null) return;
            if (File.Exists(Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri))))
            {
                new VideoRecording(transmissionvideo,false, this).Show();
                Program.log.Error($"打开{Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri))}", new Exception("打开成功"));
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("指定目录没有找到该视频,是否继续标注", "提示", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    new VideoRecording(transmissionvideo,false, this).Show();
                    Program.log.Error($"打开{Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri))}", new Exception("没有找到视频"));
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 转换路径中的/为\
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ConversionString(string path)
        {
            string[] n = path.Split('/');
            if (n.Length > 1)
            {
                string Ret = string.Empty;
                for (int i = 0; i < n.Length; i++)
                {
                    Ret += n[i] + "\\";
                }
                return Ret.Substring(0, Ret.Length - 1);
            }
            return path;
        }

        /// <summary>
        /// 改变对应条件行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.RowHandle > videoplays.Count - 1)
            {
                return;
            }
            //e.Appearance.Font = new System.Drawing.Font("", 8, FontStyle.Regular);
            VideoPlay video = (VideoPlay)gridView1.GetRow(e.RowHandle);
            if (video.ImageId.Count != 0 && video.Labels.Count != 0)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (video.ImageId.Count != 0 && video.Labels.Count == 0)
            {
                e.Appearance.BackColor = Color.Khaki;
            }
            else if (video.ImageId.Count == 0 & video.Labels.Count != 0)
            {
                e.Appearance.BackColor = Color.LightPink;
            }
        }

        /// <summary>
        /// 自定义文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Status")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "在用";
                        break;
                    case "1":
                        e.DisplayText = "弃用";
                        break;
                    default:
                        break;
                }
            }
        }

        private void DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"是否删除编号{transmissionvideo.Id}的视频？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            string url = Program.Urlpath + "/video/" + transmissionvideo.Id;
            JObject obj = WebClinetHepler.Delete_New(url);
            if (obj == null)
            {
                MessageBox.Show("删除失败");
                Program.log.Error($"删除{transmissionvideo.Id}失败", new Exception($"{url}"));
            }
            videoplays.Remove(transmissionvideo);
            bindingSource1.DataSource = videoplays;
            gridView1.RefreshData();
            information.GetInformationShow();
            Program.log.Error($"删除{transmissionvideo.Id}", new Exception($"{url}"));
        }

        private void openimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }

        private void OpenfolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.OpenFolderAndSelectFile(Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri)));
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show(transmissionvideo.Name);
        }

    }


}