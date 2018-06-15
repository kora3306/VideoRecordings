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

namespace VideoRecordings.Video
{
    public partial class SolutionFrame : Form
    {
        VideoPlay video;
        InformationDisplay information;
        public SolutionFrame(InformationDisplay info, VideoPlay videoPlay)
        {
            InitializeComponent();
            video = videoPlay;
            information = info;
        }

        private void SolutionFrame_Load(object sender, EventArgs e)
        {
            textBox_url.Text = video.Uri;
            textBox_SP.Text = video.ProjectName;
            textBox_numb.Text = video.Id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int interval = int.Parse(textBox_interval.Text);
                //WaitFormEx.Run(() =>
                //{
                    bool win = SolutionOfTheFrame(video.Uri, video.Id, interval);
                    if (win)
                    {
                        VideoPlay play = Methods.GetNewImages(video.Id);
                        information.RefreshData(play);                    
                    }
                //});
                this.Close();
                information.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("输入格式不正确");
                return;
            }

        }

        private bool SolutionOfTheFrame(string uri, int id, int step)
        {
            string url = $"http://192.168.1.224:8081/deframe";
            Dictionary<string, object> serialize = new Dictionary<string, object>();
            serialize.Add("video_uri", uri);
            serialize.Add("video_id", id);
            serialize.Add("step", step);
            string json = JsonConvert.SerializeObject(serialize);
            JObject obj = WebClinetHepler.Post_New(url, json);
            if (obj == null)
            {
                MessageBox.Show($"编号{id}解帧失败");
                return false;
            }
            return true;
        }
    }
}
