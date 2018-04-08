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
using System.Web.Script.Serialization;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using System.Reflection;

namespace VideoRecordings
{
    public partial class VideoInformation : DevExpress.XtraEditors.XtraForm
    {
        List<VideoProject> Videos = new List<VideoProject>();   //文件夹集合
        VideoProject focusedfolder;       //选中的文件夹
        Login log;
        public VideoInformation(Login login)
        {
            InitializeComponent();
            GetInformationShow();
            log = login;
            TestText();
            label2.Text = $"欢迎:{Program.UserName}";
        }

        /// <summary>
        /// 增加文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            new AddVideo(this).Show();
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            new QueryVideo(this).Show();
        }

        /// <summary>
        /// 双击打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            new InformationDisplay(this, focusedfolder).Show();
        }

        /// <summary>
        /// 扫描文件夹 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string posturl = Program.Urlpath + "/scan/video/project/" + focusedfolder.Id.ToString();
            string conditions = "project_name=" + focusedfolder.Name;
            JsonObject obj = WebClinetHepler.Post(posturl);
            if (obj == null)
            {
                MessageBox.Show("扫描文件失败");
                Program.log.Error("扫描文件失败",new Exception("扫描文件失败"));
            }
            else
            {
                MessageBox.Show("扫描文件已完成");
            }
        }

        /// <summary>
        /// 读取并打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanning_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new InformationDisplay(this,focusedfolder).Show();
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
                    return true;
                case Keys.Down:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 关联登录窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                log.Close();
            }
            catch (Exception ex)
            {
                Program.log.Error("关闭登录窗口", ex);
                throw;
            }
          
        }

        /// <summary>
        /// 焦点改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0||rowIndex> Videos.Count-1)
            {
                return;
            }
            focusedfolder = Videos[rowIndex];
        }

        /// <summary>
        /// 获取所有文件夹信息
        /// </summary>
        public void GetInformationShow(VideoProject video = null, bool fouse = false)
        {
            string getpath = Program.Urlpath + "/video/projects";
            GetListVideo(getpath, "video_projects");
            if (Videos == null || Videos.Count == 0)
            {
                return;
            }
            bindingSource1.DataSource = Videos;
            FouseRow(video,fouse);
            gridView1.RefreshData();           
        }

        /// <summary>
        /// 获取所有文件夹信息,转化成类集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public void GetListVideo(string url, string na)
        {
            Videos.Clear();
            try
            {
                List<string> datajson = new List<string>();
                JsonObject obj = WebClinetHepler.GetJson(url);
                if (obj == null) return;
                JsonObject data = obj[$"{na}"];
                for (int i = 0; i < data.Length; i++)
                {
                    string json = (new JavaScriptSerializer()).Serialize((Dictionary<string, object>)(object)data[i].Value);
                    VideoProject video = JsonHelper.DeserializeDataContractJson<VideoProject>(json);
                    Videos.Add(video);
                }
                Videos = Videos.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                Program.log.Error("获取文件夹信息", ex);
                throw;
            }
           
        }

        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UpdateContent(this,focusedfolder).Show();
        }

        public void FouseRow(VideoProject video , bool fouse)
        {
            if (!fouse) return ;
            int i = 0;
            foreach (var item in Videos)
            {
                if (video.Name==item.Name)
                {
                    break;
                }
                i++;
            }
            gridView1.FocusedRowHandle = i;
        }

        private void DELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (focusedfolder == null || focusedfolder.Uri == null) return;
                DialogResult dr = MessageBox.Show("删除扫描信息将会清除所有已经保存的视频信息,确认是否删除?", "是否删除扫描信息",
                    MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                string url = Program.Urlpath + "/clear/video/project/" + focusedfolder.Id;
                JObject obj = WebClinetHepler.Post_New(url);
                if (obj == null)
                {
                    MessageBox.Show("清除扫描信息失败");
                    Program.log.Error("清除扫描信息", new Exception("清除扫描信息失败"));
                }
                Program.log.Error($"{focusedfolder.Id}清除扫描信息", new Exception($"清除扫描信息{obj == null}"));
            }
            catch (Exception ex)
            {
                Program.log.Error($"清除扫描信息", ex);
                throw;
            }
           
        }

        private void TestText()
        {
            if (Program.GetAppConfig("TestApi") != "0")
            {
                Text += "(测试)";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ManagementLabel().ShowDialog();
        }
    }
    /// <summary>
    /// 文件夹信息
    /// </summary>
    [DataContract]
    public class VideoProject
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "place")]
        public string Place { get; set; }

        [DataMember(Name = "scenes")]
        public int Scenes { get; set; }

        [DataMember(Name = "scenes_name")]
        public string ScenesName { get; set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        [DataMember(Name = "video_count")]
        public int VideoCount { get; set; }

        [DataMember(Name = "replicator")]
        public string Replicator { get; set;}

        [DataMember(Name = "recorder")]
        public string Recorder { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "record_tim")]
        public string RecordTime { get; set; }

        [DataMember(Name = "status_name")]
        public string Status { get; set; }
    }
}