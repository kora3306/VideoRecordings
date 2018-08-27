using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoRecordings.Models;

namespace VideoRecordings.Video
{
    public partial class BatchSolution : Form
    {
        private List<VideoPlay> plays;

        public delegate void MyDeletgate();
        public event MyDeletgate MyRefreshEvent;

        public void OnRefresh()
        {
            MyRefreshEvent.Invoke();
        }

        public BatchSolution(List<VideoPlay> videoPlays)
        {
            InitializeComponent();
            plays = videoPlays;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(),out int interval))
                return;
            bool win = SolutionOfTheFrame(interval);
            if (!win)
            {
                MessageBox.Show("添加解帧视频失败");
            }
            this.Close();
        }

        private bool SolutionOfTheFrame(int step)
        {
            string url = Program.Urlpath + $"/deframe";
            List<Solution> jsonDic = new List<Solution>();
            foreach (var video in plays)
            {
                Solution solution=new Solution(video.Uri,video.Id,step);
                jsonDic.Add(solution);
            }
            string json = JsonConvert.SerializeObject(jsonDic);
            JObject obj = WebClinetHepler.Post_New(url, json);
            if (obj != null)
            {
                BackSolution back = JsonConvert.DeserializeObject<BackSolution>(obj.ToString());
                int wincount = plays.Count - back.Deframing.Count - back.FrameExists.Count - back.NotFound.Count;
                MessageShow messageShow = new MessageShow("本次成功添加视频:",wincount, "已经添加过解帧的视频：",back.Deframing.Count
                , "已经解帧完成的视频：",back.FrameExists.Count, "没有找到的视频：",back.NotFound.Count, "队列中解帧视频数：",back.WaitTasks);
                messageShow.ShowDialog();
                OnRefresh();
                DialogResult = DialogResult.OK;
                return true;
            }

            DialogResult = DialogResult.Cancel;
            return false;
        }
    }
}
