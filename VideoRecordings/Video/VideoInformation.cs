﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common;
using System.Web.Script.Serialization;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.Utils;

namespace VideoRecordings
{
    public partial class VideoInformation : DevExpress.XtraEditors.XtraForm
    {
        List<VideoProject> Videos = new List<VideoProject>();   //文件夹集合
        VideoProject focusedfolder;       //选中的文件夹
        GridHitInfo hInfo = new GridHitInfo();


        public VideoInformation()
        {
            InitializeComponent();
            GetInformationShow();      
            label2.Text = $"欢迎:{Program.UserName}";
            Methods.AddIsTest(this);
        }

        /// <summary>
        /// 增加文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            new AddVideo(this).Show();
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            new QueryVideo(this, Videos).Show();
        }

        /// <summary>
        /// 双击打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            focusedfolder = (VideoProject)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (hInfo.InRowCell)
            {
                new InformationDisplay(this, focusedfolder).Show();
            }
        }

        /// <summary>
        /// 扫描文件夹 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string posturl = Program.Urlpath + "/scan/video/project/" + focusedfolder.Id.ToString();
            string conditions = "project_name=" + focusedfolder.Name;
            JObject obj = WebClinetHepler.Post_New(posturl);
            if (obj == null)
            {
                MessageBox.Show("扫描文件失败");
                Program.log.Error("扫描文件失败", new Exception("扫描文件失败"));
            }
            else
            {
                MessageBox.Show("扫描文件已完成");
            }
        }

        /// <summary>
        /// 读取并打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanning_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new InformationDisplay(this, focusedfolder).Show();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == 0 ? 0 : gridView1.FocusedRowHandle - 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    focusedfolder = (VideoProject)gridView1.GetRow(gridView1.FocusedRowHandle);
                    return true;
                case Keys.Down:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    focusedfolder = (VideoProject)gridView1.GetRow(gridView1.FocusedRowHandle);
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 焦点改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0 || rowIndex > Videos.Count - 1)
            {
                return;
            }
            focusedfolder = (VideoProject)gridView1.GetRow(rowIndex);
        }

        /// <summary>
        /// 获取所有文件夹信息
        /// </summary>
        public void GetInformationShow(VideoProject video = null, bool fouse = false)
        {
            Videos = GetData.GetAllFolder();
            if (Videos == null || Videos.Count == 0)
            {
                return;
            }
            bindingSource1.DataSource = Videos;
            FouseRow(video, fouse);
            ShowCompleteness();
            gridView1.RefreshData();
        }

        private void ModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UpdateContent(this, focusedfolder).Show();
        }

        public void FouseRow(VideoProject video, bool fouse)
        {
            if (!fouse) return;
            int i = 0;
            foreach (var item in Videos)
            {
                if (video.Name == item.Name)
                {
                    break;
                }
                i++;
            }
            gridView1.FocusedRowHandle = i;
        }

        private void DELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (focusedfolder == null || focusedfolder.Uri == null) return;
                DialogResult dr = MessageBox.Show("删除扫描信息将会清除所有已经保存的视频信息,确认是否删除?", "是否删除扫描信息",
                    MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                string url = Program.Urlpath + "/clear/video/project/" + focusedfolder.Id;
                JObject obj = WebClinetHepler.Post_New(url);
                if (obj == null)
                {
                    MessageBox.Show("清除扫描信息失败");
                    Program.log.Error("清除扫描信息", new Exception("清除扫描信息失败"));
                }
                Program.log.Error($"{focusedfolder.Id}清除扫描信息", new Exception($"清除扫描信息{obj == null}"));
            }
            catch (Exception ex)
            {
                Program.log.Error($"清除扫描信息", ex);
                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ManagementLabel().ShowDialog();
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }

        public void ShowCompleteness()
        {
            int all = Videos.Sum(t => t.Statistic.Total);
            int comple = Videos.Sum(t => t.Statistic.Recorded);
            toolStripStatusLabel1.Text = "完成度:" + (Convert.ToDouble(comple) / Convert.ToDouble(all)).ToString(("0.00%"))
            + $"({comple}/{all})";
        }

        public void RefshData(VideoProject project=null)
        {
            if (project == null)
            {
                GetInformationShow();
                return;
            }
            List<VideoProject> videos= GetData.GetAllFolder(project.Name);
            
            
        }

        /// <summary>
        /// 绘制进度条
        /// </summary>
        /// <param name="e"></param>矩形
        public void DrawProgressBar(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            string show = e.CellValue as string;
            string s= show.Split('%').First();
            decimal percent = Convert.ToDecimal(s);
            int width = (int)(100 * Math.Abs(percent / 100) * e.Bounds.Width / 100);
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height);
            Brush b = Brushes.LightBlue;
            if (percent < 100)
            {
                b = Brushes.LightCoral;
            }
            e.Graphics.FillRectangle(b, rect);
            Font font = new Font("宋体",10);
            e.Graphics.DrawString(show, font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y+e.Bounds.Height/4));
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Percent")
            {
                DrawProgressBar(e);

                e.Handled = true;

                DrawEditor(e);
            }
        }

        private void DrawEditor(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridCellInfo cell = e.Cell as GridCellInfo;
            Point offset = cell.CellValueRect.Location;
            BaseEditPainter pb = cell.ViewInfo.Painter as BaseEditPainter;
            AppearanceObject style = cell.ViewInfo.PaintAppearance;
            if (!offset.IsEmpty)
                cell.ViewInfo.Offset(offset.X, offset.Y);
            try
            {
                pb.Draw(new ControlGraphicsInfoArgs(cell.ViewInfo, e.Cache, cell.Bounds));
            }
            finally
            {
                if (!offset.IsEmpty)
                {
                    cell.ViewInfo.Offset(-offset.X, -offset.Y);
                }
            }
        }

        private void RefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInformationShow();
        }
    }

}