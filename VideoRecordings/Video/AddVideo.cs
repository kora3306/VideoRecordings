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
using System.Web.Script.Serialization;
using Common;
using Newtonsoft.Json.Linq;

namespace VideoRecordings
{
    public partial class AddVideo : DevExpress.XtraEditors.XtraForm
    {
        VideoInformation video;       
        public AddVideo(VideoInformation videoInformation)
        {
            InitializeComponent();
            video = videoInformation;
        }

        /// <summary>
        /// 将填入的信息转化成类,并加入文件夹集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string posturl = Program.Urlpath + "/video/project";
            string name = textBox_name.Text == string.Empty ? string.Empty : textBox_name.Text.Trim();
            string place = textBox_place.Text == string.Empty ? string.Empty : textBox_place.Text.Trim(); ;
            int scenes = comboBox_scenes.SelectedIndex;
            string start_time = dateTimePicker_start.Value.ToString("yyyy-MM-dd");
            string end_time = dateTimePicker_end.Value.ToString("yyyy-MM-dd");
            string url = textBox_url.Text == string.Empty ? string.Empty : textBox_url.Text.Trim();
            string size = textBox_size.Text == string.Empty ? string.Empty : textBox_size.Text.Trim();
            string replicator = textBox_replicator.Text == string.Empty ? string.Empty : textBox_replicator.Text.Trim();
            string recorder = textBox_recorder.Text == string.Empty ? string.Empty : textBox_recorder.Text.Trim();
            string note = textBox_note.Text == string.Empty ? string.Empty : textBox_note.Text.Trim();
            int count = 0;
            try
            {
                count = int.Parse(textBox_count.Text);
                url = StringToString(url);
            }
            catch (Exception)
            {
                MessageBox.Show("输入的数量格式有误");
                return;
            }


            VideoProject video_project = new VideoProject
            {
                Name = name,
                Place = place,
                Scenes = scenes,
                StartDate = start_time,
                VideoCount = count,
                EndDate = end_time,
                Uri = url,
                Size = size,               
                Replicator = replicator,
                Recorder = recorder,              
                Note = note,
            };

            string json = JsonHelper.SerializeDataContractJson(video_project);
            JObject returnobj = WebClinetHepler.Post_New(posturl, json);
            if (returnobj == null)
            {
                MessageBox.Show("上传失败");
                return;
            }
            else
            {
                MessageBox.Show("上传成功");
            }
            video.GetInformationShow(video_project,true);
            this.Close();
            Dispose();
            Program.log.Error("上传文件夹", new Exception($"{json}"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 字符串转化
        /// </summary>
        /// <returns></returns>
        public string StringToString(string str)
        {
            string tostr = string.Empty;
            if (str.StartsWith(@"\"))
            {
                tostr = str.Replace(@"\", "/");
                return tostr;
            }
            return str;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    button1.PerformClick();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}