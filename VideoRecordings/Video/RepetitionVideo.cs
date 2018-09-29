using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;
using DevExpress.XtraGrid;

namespace VideoRecordings.Video
{
    public partial class RepetitionVideo : Form
    {
        public Dictionary<int, string> statuss = new Dictionary<int, string>()
        {
            { 0,"尚未处理" },
            { 1,"正在处理"},
            { 2,"处理完成"}
        };

        GridHitInfo hInfo = new GridHitInfo();

        private  string log;

        public RepetitionVideo()
        {
            InitializeComponent();
            log = Program.User.Name;
            SetINfo(log);
        }

        private void SetINfo(string logname,string number=null)
        {
            List<Repetitions> repetitions = VideoData.GetRepetitions(logname,number);
            bindingSource1.DataSource = repetitions.OrderByDescending(t=>t.ID);
            gridView1.RefreshData();
        }

        private void Show20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetINfo(log,"20");
            Program.log.Info("查看20条查重记录");
        }

        private void show50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetINfo(log,"50");
            Program.log.Info("查看50条查重记录");
        }

        private void showallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetINfo(log,"-1");
            Program.log.Info("查看所有查重记录");
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Status")
            {
                switch (e.DisplayText)
                {
                    case "0":     
                    case "1":
                    case "2":
                        e.DisplayText = statuss[int.Parse(e.DisplayText)];
                        break;
                    default:
                        break;
                }
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }

        private void outToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetINfo(log);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void ShowInfo()
        {
            try
            {
                Repetitions re = (Repetitions)gridView1.GetRow(gridView1.FocusedRowHandle);
                ShowRepetition show = new ShowRepetition(re);
                show.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("没有要显示的记录");
                return;
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repetitions re = (Repetitions)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (MessageBox.Show($"是否删除编号{re.ID}的记录？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!VideoData.DeleteRepetition(re.ID))
            {
                MessageBox.Show("删除记录失败");
                Program.log.Error($"删除查重记录{re.ID}--失败");
                return;
            }
            MessageBox.Show("删除成功");
            Program.log.Info($"删除查重记录{re.ID}");
            SetINfo(log);
        }
    }
}
