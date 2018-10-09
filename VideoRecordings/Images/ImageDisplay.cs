using Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Manina.Windows.Forms;
using Newtonsoft.Json.Linq;
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
using VideoRecordings.Models;

namespace VideoRecordings.Images
{
    public partial class ImageDisplay : Form
    {
        ImageInformation imageInformation;
        ImageProject project;
        GridHitInfo hInfo = new GridHitInfo();

        public int imagesize = 96;
        public int fittersize = 96;

        public ImagePlay transmissionvideo = new ImagePlay();     //当前选中的文件

        public List<ImagePlay> images = new List<ImagePlay>();
        public List<string> imageurl = new List<string>();   //图片url

        bool isFirst = true;      //初次定位文件夹为0

        public ImageDisplay(ImageInformation information, ImageProject imageproject)
        {
            InitializeComponent();
            imageInformation = information;
            project = imageproject;
            CeShi();
            if (project != null)
            {
                toolStripStatusLabel1.Text = $"视频 :{project.Name}    {project.Place}";
            }
            label2.Text = $"欢迎:{Program.User.RealName}";
        }


        private void CeShi()
        {
            List<string> path = getDirectory(project.Uri.ToString());
            int i = 1;
            foreach (var item in path)
            {
                images.Add(new ImagePlay
                {
                    Name = i.ToString(),
                    Uri = item,
                    CreateTime = "20170809",
                    StartTime = "20170903",
                    RecordTime = "20170905",
                    EndTime = "20180605",
                    Status = "在用",
                    Count = new DirectoryInfo(item).GetFiles().Count(),
                    Labels = new List<string>() { "11", "22" },
                    ImageId = new List<int>() { 1, 2 }

                });
            }
            bindingSource1.DataSource = images;
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
                transmissionvideo = images.Count == 0 ? null : images.First();
                isFirst = false;
            }
            RefreshImage();
            GetIntToString();

            bindingSource1.DataSource = images;

        }

        /// <summary>
        /// 获取所有文件信息,转化为集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public void GettListVideo(string url, string na)
        {
            images.Clear();
            //JObject obj = WebClinetHepler.GetJObject(url, na);
            //if (obj == null) return;
            //images = JsonHelper.DeserializeDataContractJson<List<ImageProject>>(obj["videos"].ToString());
            //Program.log.Error($"获取{url}信息", new Exception("获取成功"));

        }

        /// <summary>
        /// 刷新焦点行图片集合
        /// </summary>
        public void RefreshImage()
        {
            foreach (var item in images)
            {
                if (transmissionvideo.Id == item.Id)
                {
                    transmissionvideo = item;
                    break;
                }
            }
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
        /// 刷新标签
        /// </summary>
        /// <param name="play"></param>
        public void RefreshData(ImagePlay play)
        {
            foreach (var item in images)
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
            bindingSource1.DataSource = images;
        }

        public static List<string> getDirectory(string path)
        {
            List<string> paths = new List<string>();
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                paths.Add(d.FullName);
            }
            return paths;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                return;
            }
            transmissionvideo = (ImagePlay)gridView1.GetRow(rowIndex);
            RefreshImage();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            transmissionvideo = (ImagePlay)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (hInfo.InRowCell)
            {
                new ImagePull(this, transmissionvideo).Show();
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }
    }
}
