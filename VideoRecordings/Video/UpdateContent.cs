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
using VideoRecordings.Models;
using Newtonsoft.Json;

namespace VideoRecordings
{
    public partial class UpdateContent : DevExpress.XtraEditors.XtraForm
    {
        VideoProject videoProject;
        string oldinformation;
        public UpdateContent(VideoProject project)
        {
            InitializeComponent();
            videoProject = project;
           oldinformation=JsonConvert.SerializeObject(project);
        }

        private void UpdateContent_Load(object sender, EventArgs e)
        {
            textBox_name.Text = string.IsNullOrEmpty(videoProject.Name) ? string.Empty : videoProject.Name;
            textBox_place.Text = string.IsNullOrEmpty(videoProject.Place) ? string.Empty : videoProject.Place;
            comboBox_scenes.SelectedIndex = videoProject.Scenes == 0 || videoProject.Scenes == 1 ? videoProject.Scenes : 2;
            if (videoProject.StartDate != null)
            {
                dateTimePicker_s.Value = DateTime.Parse(videoProject.StartDate);
            }
            if (videoProject.EndDate != null)
            {
                dateTimePicker_e.Value = DateTime.Parse(videoProject.EndDate);
            }
            textBox_siza.Text = string.IsNullOrEmpty(videoProject.Size) ? string.Empty : videoProject.Size;
            textBox_count.Text = string.IsNullOrEmpty(videoProject.VideoCount.ToString()) ? "0" : videoProject.VideoCount.ToString();
            textBox_rep.Text = string.IsNullOrEmpty(videoProject.Replicator) ? string.Empty : videoProject.Replicator;
            textBox_rec.Text = string.IsNullOrEmpty(videoProject.Recorder) ? string.Empty : videoProject.Recorder;
            textBox_uri.Text = string.IsNullOrEmpty(videoProject.Uri) ? string.Empty : videoProject.Uri;
            textBox_note.Text = string.IsNullOrEmpty(videoProject.Note) ? string.Empty : videoProject.Note;
        }

        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public virtual void OnSave()
        {
            MyEvent?.Invoke();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string url = AppSettings.Urlpath + "/video/project/" + videoProject.Id;
            string json = RfreshDate();
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            JObject obj = WebClinetHepler.Patch_New(url, json);
            if (obj == null)
            {
                MessageBox.Show("修改失败");
                Program.log.Error($"project_id:{videoProject.Id},更改信息{json}");
                return;
            }
            OnSave();
            Program.log.Error($"修改视频信息,原信息{oldinformation},修改后{json}");
            this.Close();
        }

        public string RfreshDate()
        {
            Dictionary<string, object> postjson = new Dictionary<string, object>();
            if (textBox_name.Text != videoProject.Name)
            {
                postjson.Add("name", textBox_name.Text);
                videoProject.Name = textBox_name.Text;
            }
            if (textBox_place.Text != videoProject.Place)
            {
                postjson.Add("place", textBox_place.Text);
                videoProject.Place = textBox_place.Text;
            }
            if (comboBox_scenes.SelectedIndex != videoProject.Scenes)
            {
                postjson.Add("scenes", comboBox_scenes.SelectedIndex);
                videoProject.Scenes = comboBox_scenes.SelectedIndex;
                videoProject.ScenesName = comboBox_scenes.Text;
            }
            if (dateTimePicker_s.Value != DateTime.Parse(videoProject.StartDate))
            {
                postjson.Add("start_date", dateTimePicker_s.Value.ToString("yyyy-MM-dd"));
                videoProject.StartDate = dateTimePicker_s.Value.ToString("yyyy-MM-dd");
            }
            if (dateTimePicker_e.Value != DateTime.Parse(videoProject.EndDate))
            {
                postjson.Add("end_date", dateTimePicker_e.Value.ToString("yyyy-MM-dd"));
                videoProject.EndDate = dateTimePicker_e.Value.ToString("yyyy-MM-dd");
            }
            if (textBox_siza.Text != videoProject.Size)
            {
                postjson.Add("size", textBox_siza.Text);
                videoProject.Size = textBox_siza.Text;
            }
            if (textBox_count.Text != videoProject.VideoCount.ToString())
            {
                try
                {
                    postjson.Add("video_count", int.Parse(textBox_count.Text));
                    videoProject.VideoCount = int.Parse(textBox_count.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("输入视频数量格式有误");
                    return string.Empty;
                }
            }
            if (textBox_rep.Text != videoProject.Replicator)
            {
                postjson.Add("replicator", textBox_rep.Text);
                videoProject.Replicator = textBox_rep.Text;
            }
            if (textBox_rec.Text != videoProject.Recorder)
            {
                postjson.Add("recorder", textBox_rec.Text);
                videoProject.Recorder = textBox_rec.Text;
            }
            if (textBox_note.Text != videoProject.Note)
            {
                postjson.Add("note", textBox_note.Text);
                videoProject.Note = textBox_note.Text;
            }
            if (textBox_uri.Text != videoProject.Uri)
            {
                postjson.Add("uri",StringToString(textBox_uri.Text));
                videoProject.Uri = StringToString(textBox_uri.Text);
            }
            string json = (new JavaScriptSerializer()).Serialize(postjson);
            return json;
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