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
    public partial class Frame : Form
    {
        List<Solution> frames;
        public Frame()
        {
            InitializeComponent();
            Refresh();
        }

        private void Frame_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void  NewRefresh()
        {
            gridControl1.DataSource = null;
            frames = GetDatas.VideoData.GetFrame();
            gridControl1.DataSource = frames;
        }

        private List<Solution> GetSelectRow()
        {
            int[] rownumber = gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return null;
            List<Solution> videos = new List<Solution>();
            foreach (var it in rownumber)
            {
                if (it < 0) continue;
                Solution video = (Solution)gridView1.GetRow(it);
                videos.Add(video);
            }
            return videos;
        }

        private void TOP_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Solution> solutions = GetSelectRow();
            if(solutions==null||solutions.Count==0) return;
            if (GetDatas.VideoData.QueueSolution(solutions))
            {
                NewRefresh();
                return;
            }
            MessageBox.Show("置顶失败");
        }

        private void Ref_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewRefresh();
        }
    }
}
