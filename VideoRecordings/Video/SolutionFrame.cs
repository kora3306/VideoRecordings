using Common;
using DevExpress.XtraWaitForm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeemmoData.Controls;
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

namespace VideoRecordings.Video
{
    public partial class SolutionFrame : Form
    {
        VideoPlay video;
        public SolutionFrame(VideoPlay videoPlay)
        {
            InitializeComponent();
            video = videoPlay;
        }

        private void SolutionFrame_Load(object sender, EventArgs e)
        {
            textBox_url.Text = video.Uri;
            textBox_SP.Text = video.ProjectName;
            textBox_numb.Text = video.Id.ToString();
        }

        public delegate void MyDeletgate(VideoPlay play);
        public event MyDeletgate MyRefreshEvent;
        public void OnRefresh(VideoPlay play)
        {
            MyRefreshEvent.Invoke(play);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(!int.TryParse(textBox_interval.Text.Trim(),out int interval))
                    return;
                bool win = SolutionOfTheFrame(video.Uri, video.Id, interval);
                if (win)
                {
                    VideoPlay play = Methods.GetNewImages(video.Id);
                    OnRefresh(play);
                }
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("输入格式不正确");
                return;
            }

        }

        private bool SolutionOfTheFrame(string uri, int id, int step)
        {
            //string url = $"http://192.168.1.224:8081/deframe";
            string url = Program.Urlpath + $"/deframe";
            List<Solution> jsonDic= new List<Solution>();
            Solution solution= new Solution(uri,id,step);
            jsonDic.Add(solution);
            string json = JsonConvert.SerializeObject(jsonDic);
            JObject obj = WebClinetHepler.Post_New(url, json);
            if (obj == null)
            {
                MessageBox.Show($"编号{id}解帧失败");
                Program.log.Error($"编号{id}解帧失败");
                return false;
            }
            Program.log.Error($"编号{id}解帧,间隔{step}");
            return true;
        }
    }
}
