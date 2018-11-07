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
            comboBox_top.SelectedIndex = 0;
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
                if(!int.TryParse(textBox_interval.Text.Trim(),out int step))
                    return;
                string folder = textBox_folder.Text.Trim();
                string note = textBox_note.Text.Trim();
                int top = comboBox_top.SelectedIndex;
                List<int> ids = new List<int>() {video.Id};
                bool win = GetDatas.SolutionData.SolutionOfTheFrame(new Solution(folder, note, ids, step, top));
                if (win)
                {
                    MessageBox.Show("添加失败");
                    return;
                }
                VideoPlay play = Methods.GetNewImages(video.Id);
                OnRefresh(play);
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("输入格式不正确");
                return;
            }

        }
    }
}
