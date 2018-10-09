using Common;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.Models;

namespace VideoRecordings
{
    public partial class ImageInformation : Form
    {
        List<ImageProject> Images = new List<ImageProject>();   //文件夹集合
        ImageProject focusedfolder;       //选中的文件夹
        GridHitInfo hInfo = new GridHitInfo();

        public ImageInformation()
        {
            InitializeComponent();
            GetInformationShow();
            TestText();
            label2.Text = $"欢迎:{Program.User.RealName}";
        }

        private void TestText()
        {
            if (Program.GetAppConfig("TestApi") != "0")
            {
                Text += "(测试)";
            }
        }

        /// <summary>
        /// 获取所有文件夹信息
        /// </summary>
        public void GetInformationShow(ImageProject video = null, bool fouse = false)
        {
            string getpath = Program.Urlpath + "/video/projects";
            Images = GetListVideo(getpath, "video_projects");
            if (Images == null || Images.Count == 0)
                return;
            bindingSource1.DataSource = Images;
            FouseRow(video, fouse);
            gridView1.RefreshData();
        }

        public void FouseRow(ImageProject image, bool fouse)
        {
            if (!fouse) return;
            int i = 0;
            foreach (var item in Images)
            {
                if (image.Name == item.Name)
                {
                    break;
                }
                i++;
            }
            gridView1.FocusedRowHandle = i;
        }

        /// <summary>
        /// 获取所有文件夹信息,转化成类集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public List<ImageProject> GetListVideo(string url, string na)
        {
            List<ImageProject> project = new List<ImageProject>();
            try
            {
                //List<string> datajson = new List<string>();
                //JObject obj = WebClinetHepler.GetJObject(url);
                //if (obj == null) return null;
                //project = JsonHelper.DeserializeDataContractJson<List<ImageProject>>(obj[$"{na}"].ToString());
                //project = project.OrderBy(t => t.Name).ToList();
                project.Add(new ImageProject
                {
                    Name = "TP0178",
                    Bssiness = "车辆识别",
                    Place = "武汉",
                    StartDate = "20171207",
                    EndDate = "20171207",
                    Direction = "车头车尾",
                    Lanes = 3,
                    Uri = @"C:\Users\test\Desktop\cs",
                    OldUri = @"C:\Users\test\Desktop\cs",
                    ImageCount = 86,
                    Size = "24.5M",
                    Recorder = "熊缓",
                    Replicator = "赵大森",
                    RecordTime="2017/12/7",
                    Note= "样本量少于50张的新车型",
                    Status="在用",                   
                });

                return project;
            }
            catch (Exception ex)
            {
                Program.log.Error("获取文件夹信息", ex);
                throw;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Images.AddImage().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Images.QueryImage().Show();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            focusedfolder = (ImageProject)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (hInfo.InRowCell)
            {
                new Images.ImageDisplay(this,focusedfolder).Show();
            }
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
                    focusedfolder = (ImageProject)gridView1.GetRow(gridView1.FocusedRowHandle);
                    return true;
                case Keys.Down:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    focusedfolder = (ImageProject)gridView1.GetRow(gridView1.FocusedRowHandle);
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);    
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0 || rowIndex > Images.Count - 1)
            {
                return;
            }
            focusedfolder = (ImageProject)gridView1.GetRow(rowIndex);
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }
    }
}
