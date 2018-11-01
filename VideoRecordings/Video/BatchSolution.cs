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
            comboBox_top.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_folder.Text)||!int.TryParse(textBox1.Text.Trim(),out int interval))
            {
                MessageBox.Show("有必填项错误或为空");
                return;
            }
            string folder = textBox_folder.Text.Trim();
            string note = textBox_note.Text.Trim();
            int top = comboBox_top.SelectedIndex;
            bool win = GetDatas.SolutionData.SolutionOfTheFrame(new Solution(folder,note,plays.Select(t=>t.Id).ToList(), interval,top));
            if (!win)
            {
                MessageBox.Show("添加解帧视频失败");
                Program.log.Error($"添加解帧视频失败,信息{string.Concat(folder, note,string.Join(",",plays.Select(t => t.Id).ToList()), interval, top)}");
                return;
            }
            OnRefresh();
            this.Close();
        }

   
    }
}
