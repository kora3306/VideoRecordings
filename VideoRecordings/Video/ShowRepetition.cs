using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;

namespace VideoRecordings.Video
{
    public partial class ShowRepetition : Form
    {
        List<ReturnRepetition> returns = new List<ReturnRepetition>();

        private Dictionary<int, string> nameDic = new Dictionary<int, string>()
        {
            { 0,"正常"},
            { 1,"此视频与视频库中已有视频重复"},
            { 2,"文件不存在"},
            { 3,"数据库错误"},
            { 4,"解帧错误"}
        };

        public ShowRepetition(Repetitions repetitions)
        {
            InitializeComponent();
            SetData(repetitions);
        }

        public void SetData(Repetitions repetitions)
        {
            returns = VideoData.GetOutResult(repetitions.ID);
            bindingSource1.DataSource = returns;
        }

        /// <summary>
        /// 导出GridControl选中的信息
        /// </summary>
        /// <param name="gridControl1"></param>
        private void DataGridToExcel1(GridControl gridControl1)
        {
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsPrint.PrintSelectedRowsOnly = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.pdf)|*.pdf";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridControl1.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            int id = int.Parse(GridGroupRowInfo.EditValue.ToString());
            GridGroupRowInfo.GroupText = $"{nameDic[id]}({returns.Count(t=>t.Code==id)})";
        }

        private void ExeclToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridToExcel1(gridControl1);
            Program.log.Info("导出查重信息到execl");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayVideo();
        }

        private void oldToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PlayVideo(false);
        }

        private void PlayVideo(bool isnew=true)
        {
            ReturnRepetition re = (ReturnRepetition)gridView1.GetRow(gridView1.FocusedRowHandle);
            VideoPlay play = new VideoPlay();
            if(isnew)
            {
                play= VideoData.GetIndexVideoInfo(re.ID);
            }
            else
            {
                if (re.DuplicateId == 0) return;
                play= VideoData.GetIndexVideoInfo(re.DuplicateId);
            }
            if(play==null)
            {
                MessageBox.Show("不存在该视频");
                return;
            }
            VideoRecording recording = new VideoRecording(play,null);
            if (play == null || play.Uri == null) return;
            if (File.Exists(Program.ReturnStringUrl(Methods.ConversionString(play.Uri))))
            {
                recording.Show();
                Program.log.Error($"打开{Program.ReturnStringUrl(Methods.ConversionString(play.Uri))}", new Exception("打开成功"));
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("指定目录没有找到该视频,是否继续标注", "提示", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult != DialogResult.Yes)
                    return;
                recording.Show();
                Program.log.Error($"打开{Program.ReturnStringUrl(Methods.ConversionString(play.Uri))}", new Exception("没有找到视频"));
            }
        }

        private void LocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFolder();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnRepetition re = (ReturnRepetition)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (MessageBox.Show($"是否删除编号{re.ID}的视频？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!VideoData.DeleteVideo(re.ID))
            {
                MessageBox.Show("删除失败");
                Program.log.Error($"删除{re.ID}失败", new Exception($"{re.ID}"));
                return;
            }
            if (!VideoData.DeleteRepetitionVideo(re.ID))
            {
                MessageBox.Show("删除失败");
                Program.log.Error($"删除{re.ID}失败", new Exception($"{re.ID}"));
                return;
            }
            MessageBox.Show("删除成功");
        }

        private void ShowFolder()
        {
            ReturnRepetition re = (ReturnRepetition)gridView1.GetRow(gridView1.FocusedRowHandle);
            Methods.OpenFolderAndSelectFile(Program.ReturnStringUrl(Methods.ConversionString(re.Path)));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {               
                case Keys.F1:
                    ShowFolder();
                    break;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
