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
using Common;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.IO;
using Manina.Windows.Forms;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Diagnostics;
using VideoRecordings.Video;
using DevExpress.XtraGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Paint;
using System.Reflection;
using VideoRecordings.Equipment;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;
using VideoRecordings.Label;
using SeemmoData.Controls;

namespace VideoRecordings
{
    public partial class InformationDisplay : Form
    {
        Dictionary<int, string> SnapshotedMap = new Dictionary<int, string>()
        {
            {0,"未截图"},
            {1,"正在截图" },
            {2,"截图完成" },
            {3,"截图错误"}
        };
        public VideoPlay transmissionvideo = new VideoPlay();     //当前选中的文件
        public List<VideoPlay> videoplays = new List<VideoPlay>();  //所有文件
        public List<string> imageurl = new List<string>();   //图片url
        private List<Completeness> comple = new List<Completeness>();
        private Dictionary<int, EquipmentInfo> mydic=new Dictionary<int, EquipmentInfo>();
        private List<EquipmentInfo> equipments = new List<EquipmentInfo>();
        bool unfold = false;
        GridHitInfo hInfo = new GridHitInfo();
        VideoProject project;      //传入的文件夹
        bool isFirst = true;      //初次定位文件夹为0
        MyLabels MyLabel;
        int total = 0;
        int recorded = 0;

        private AddEquipment addEquipment;
        private SelectEquipment selectEquipment;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public virtual void OnSave()
        {
            MyEvent?.Invoke();
        }

        public InformationDisplay(VideoProject focusedfolder)
        {
            InitializeComponent();         
            project = focusedfolder;             
        }

        /// <summary>
        /// 加载姓名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformationDisplay_Load(object sender, EventArgs e)
        {
            ScanFolder();
            PostVideos();
            MyLabel = new MyLabels();
            addEquipment = new AddEquipment();
            selectEquipment = new SelectEquipment();
            addEquipment.MySaveEvent += new AddEquipment.MyDelegate(RefEquipment);
            selectEquipment.MySaveEvent += new SelectEquipment.MyEvent(PostVideos);
            selectEquipment.MyRefreshEvent += new SelectEquipment.MyEvent(GridViewClear);
            imageListView1.DiskCache = Program.Persistent;
            Methods.AddIsTest(this);
            toolStripStatusLabel1.Text = $"当前视频文件夹编号 :{project.Name}    地点:{project.Place}";
        }

        public void RefEquipment()
        {
            selectEquipment.RefreshData();
            Program.log.Info("刷新设备信息");
        }

        public void GridViewClear()
        {
            bindingSource1.DataSource = null;
            Program.log.Info("清空信息列表");
        }
        /// <summary>
        /// 打开文件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenVideoPaly();
        }

        /// <summary>
        /// 删除图片按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.DelImage(imageListView1, imageurl);
            PostVideos();
            gridView1.RefreshData();
        }

        /// <summary>
        /// 点击加载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_Click(object sender, EventArgs e)
        {
            transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
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
                    transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                    GetIntToString();
                    return true;
                case Keys.Down:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                    GetIntToString();
                    return true;
                case Keys.D:
                    DELToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    return true;
                case Keys.F2:
                    Methods.OpenFolderAndSelectFile(Program.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri)));
                    return true;
                case Keys.Space:
                    if (gridView1.FocusedRowHandle < 0) return true;
                    transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                    if (hInfo.InRowCell)
                        OpenVideoPaly();
                    return true;
                case Keys.Q:
                    ShowStaticLabels();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 双击放大图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageListView1_DoubleClick(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
            Program.log.Info($"放大{transmissionvideo.Id}图片");
        }

        /// <summary>
        /// 双击打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
            if (hInfo.InRowCell)
                OpenVideoPaly();
        }

        /// <summary>
        /// 焦点改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
       {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                rowIndex = gridView1.GetDataRowHandleByGroupRowHandle(rowIndex);
            }
            transmissionvideo = (VideoPlay)gridView1.GetRow(rowIndex);
            RefreshImage();
            GetIntToString();
            DeleteFolder(Program.ImageSavePath);
        }

        /// <summary>
        /// 加载图片集合
        /// </summary>
        public void GetIntToString()
        {
            if (transmissionvideo == null || transmissionvideo?.ImageId.Count == 0)
            {
                imageListView1.Items.Clear();
                return;
            }
            imageurl.Clear();
            imageurl = Methods.IdConvetToPath(transmissionvideo.ImageId);
            SetTheListView();
        }

        /// <summary>
        /// 控件加载图片
        /// </summary>
        public void SetTheListView()
        {
            imageListView1.Items.Clear();
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            imageListView1.ThumbnailSize = new Size(300, 180);
            foreach (string imageUrl in imageurl)
            {
                ImageListViewItem item = new ImageListViewItem(imageUrl) { Text = imageUrl.Split('/').Last() };
                items.Add(item);
            }
            imageListView1.Items.AddRange(items.ToArray());
            Program.log.Info($"加载图片{string.Join(",",imageurl)}");
        }

        /// <summary>
        /// 清空指定的文件夹，但不删除文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        DeleteFolder(d1.FullName);////递归删除子文件夹
                    }
                    Directory.Delete(d);
                }
            }
            Program.log.Info($"清空{dir}");
        }

        /// <summary>
        /// 读取并加载信息
        /// </summary>
        public void PostVideos()
        {       
            GettListVideo();
            if (isFirst)
            {
                transmissionvideo = videoplays.Count == 0 ? null : videoplays.First();
                isFirst = false;
            }
            mydic = equipments.ToDictionary(t => t.Id,s=>s);
            total = Sum(comple);
            recorded = Sum(comple, false);
            SetText();
            RefreshImage();
            bindingSource1.DataSource = videoplays;
            gridView1.FindFilterText = string.Empty;
            gridView1.CollapseAllGroups();
            GroupIndex();
        }

        private void ScanFolder()
        {
            if (project == null)
            {
                return;
            }
            if (!VideoData.ScanFolder(project))
            {
                MessageBox.Show("扫描文件失败");
                Program.log.Error($"扫描{project.Name}", new Exception("扫描失败"));
            }
        }

        private void SetText()
        {
            toolStripStatusLabel2.Text = "         当前文件夹完成度" + "  " + (Convert.ToDouble(recorded) / Convert.ToDouble(total)).ToString(("0.0%"))
                + $"({recorded}/{total})";
        }
        /// <summary>
        /// 刷新标签
        /// </summary>
        /// <param name="play"></param>
        public void RefreshData(VideoPlay play)
        {
            VideoPlay video = videoplays.FirstOrDefault(t => t.Id == play.Id);
            video.Labels = play.Labels;
            video.ImageId = play.ImageId;
            video.StartTime = play.StartTime;
            video.EndTime = play.EndTime;
            video.CreateTime = play.CreateTime;
            video.RecordTime = play.RecordTime;
            video.Recorded = play.Recorded;
            bindingSource1.DataSource = videoplays;
            gridView1.RefreshData();
            Program.log.Info($"保存并刷新{video.Id}");
        }

        /// <summary>
        /// 刷新焦点行图片集合
        /// </summary>
        public void RefreshImage()
        {
            if (transmissionvideo == null) return;
            foreach (var item in videoplays)
            {
                if (transmissionvideo.Id == item.Id)
                {
                    transmissionvideo = item;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取所有文件信息,转化为集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public void GettListVideo()
        {
            WaitFormEx.Run(() =>
            {
                videoplays.Clear();
                equipments.Clear();
                JObject obj = VideoData.GetAllVideoInfo(project.Name);
                if (obj == null || obj["result"].ToString() == "[]") return;
                for (int i = 0; i < obj["result"][0]["equipments"].Count(); i++)
                {
                    EquipmentInfo equipment = JsonConvert.DeserializeObject<EquipmentInfo>(obj["result"][0]["equipments"][i]["equipment_info"].ToString());
                    List<VideoPlay> videos = JsonConvert.DeserializeObject<List<VideoPlay>>(obj["result"][0]["equipments"][i]["videos"].ToString());
                    videos.ForEach(t => t.Rquipment = equipment);
                    videoplays.AddRange(videos);
                    equipments.Add(equipment);
                    Completeness com = JsonHelper.DeserializeDataContractJson<Completeness>(obj["result"][0]["equipments"][i]["statistic"].ToString());
                    comple.Add(com);
                }
            });
            Program.log.Error($"获取{project.Name}信息", new Exception("获取成功"));
        }

        /// <summary>
        /// 打开文件并播放
        /// </summary>
        private void OpenVideoPaly()
        {
            VideoRecording recording = new VideoRecording(transmissionvideo, videoplays);
            recording.MyEvent += new VideoRecording.MyDelegate(PostVideos);
            recording.WindowState = FormWindowState.Maximized;
            if (transmissionvideo == null || transmissionvideo.Uri == null) return;
            if (File.Exists(Program.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri))))
            {
                recording.Show();
                Program.log.Error($"打开{Program.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri))}", new Exception("打开成功"));
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("指定目录没有找到该视频,是否继续标注", "提示", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult != DialogResult.Yes)
                    return;
                recording.Show();
                Program.log.Error($"打开{Program.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri))}", new Exception("没有找到视频"));
            }
        }

        /// <summary>
        /// 改变对应条件行颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.RowHandle < 0 || e.RowHandle > videoplays.Count() - 1)
            {
                return;
            }
            VideoPlay video = (VideoPlay)gridView1.GetRow(e.RowHandle);
            if (video.ImageId.Count != 0 && !string.IsNullOrEmpty(video.Label))
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (video.ImageId.Count != 0 && string.IsNullOrEmpty(video.Label))
            {
                e.Appearance.BackColor = Color.Khaki;
            }
            else if (video.ImageId.Count == 0 & !string.IsNullOrEmpty(video.Label))
            {
                e.Appearance.BackColor = Color.LightPink;
            }
            if (e.RowHandle == gridView1.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.SkyBlue;
            }
        }
        /// <summary>
        /// 自定义文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Status")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "在用";
                        break;
                    case "1":
                        e.DisplayText = "弃用";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"是否删除编号{transmissionvideo.Id}的视频,删除视频前请确认解帧图片一并删除？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!VideoData.DeleteVideo(transmissionvideo.Id))
            {
                MessageBox.Show("删除失败");
                Program.log.Error($"删除{transmissionvideo.Id}失败", new Exception($"{transmissionvideo.Id}"));
            }
            videoplays.Remove(transmissionvideo);
            bindingSource1.DataSource = videoplays;
            transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
            gridView1.ExpandAllGroups();
            unfold = true;
            gridView1.RefreshData();
            OnSave();
            //Program.log.Error($"删除{transmissionvideo.Id}", new Exception($"{url}"));
        }

        /// <summary>
        /// 点击显示图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openimageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }

        /// <summary>
        /// 定位文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenfolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transmissionvideo == null) return;
            Methods.OpenFolderAndSelectFile(Program.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri)));
        }

        /// <summary>
        /// 判断点击是否数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.X, e.Y);
        }

        /// <summary>
        /// 解帧按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (transmissionvideo.Deframed != "未解帧")
            {
                MessageBox.Show("该视频已经解帧,如要重新解帧请删除原解帧路径");
                return;
            }
            SolutionFrame solu = new SolutionFrame(transmissionvideo);
            solu.MyRefreshEvent += new SolutionFrame.MyDeletgate(RefreshData);
            solu.ShowDialog();
            Program.log.Error($"解帧视频{transmissionvideo.Id}");
        }

        /// <summary>
        /// 清除解帧信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSolution_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除解帧文件夹？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (GetSelectRow() == null)
            {
                MessageBox.Show("没有需要删除的解帧视频");
                return;
            }
            List<int> ids = GetSelectRow().Select(t => t.Id).ToList();
            if (!SolutionData.DeleteSolution(ids))
            {
                MessageBox.Show("清除解帧信息失败");
                Program.log.Error($"清除解帧信息失败.Id:{string.Join(",",ids)}");
            }
            bindingSource1.DataSource = null;
            PostVideos();
        }

        /// <summary>
        /// 导出GridControl选中的信息
        /// </summary>
        /// <param name="gridControl1"></param>
        private void DataGridToExcel1(GridControl gridControl1)
        {
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsPrint.PrintSelectedRowsOnly = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "导出Excel",
                Filter = "Excel文件(*.pdf)|*.pdf"
            };
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridControl1.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Program.log.Info($"导出信息到Excel,路径:{saveFileDialog.FileName}");
        }

        /// <summary>
        /// 导出数据到json
        /// </summary>
        /// <param name="palys"></param>
        public void WriteJson(List<VideoPlay> palys)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "导出Json",
                Filter = "Jsom文件(*.json)|*.json"
            };
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            //构成配置文件路径 
            string con_file_path = saveFileDialog.FileName;
            if (string.IsNullOrEmpty(con_file_path)) return;
            if (!File.Exists(con_file_path))
            {
                File.Create(con_file_path).Close();
            }

            //把模型数据写到文件 
            using (StreamWriter sw = new StreamWriter(con_file_path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                //构建Json.net的写入流 
                JsonWriter writer = new JsonTextWriter(sw)
                {
                    Formatting = Formatting.Indented
                };
                //把模型数据序列化并写入Json.net的JsonWriter流中 
                serializer.Serialize(writer, palys);
                //ser.Serialize(writer, ht); 
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                writer.Close();
                sw.Close();
            }
            Program.log.Error($"导出信息到json,路径:{con_file_path}");
        }

        /// <summary>
        /// 所有勾选项
        /// </summary>
        /// <returns></returns>
        private List<VideoPlay> GetCheckList()
        {
            gridView1.CloseEditor();
            List<VideoPlay> plays = new List<VideoPlay>();
            int[] rownumber = gridView1.GetSelectedRows();
            for (int i = 0; i < rownumber.Count(); i++)
            {
                VideoPlay video = (VideoPlay)gridView1.GetRow(rownumber[i]);
                plays.Add(video);
            }

            return plays;
        }

        /// <summary>
        /// 返回完成数量
        /// </summary>
        /// <param name="comple"></param>
        /// <param name="issum"><true总数量false完成数量/param>
        /// <returns></returns>
        private int Sum(List<Completeness> comple, bool issum = true)
        {
            int sum = 0;
            if (issum)
            {
                for (int i = 0; i < comple.Count; i++)
                {
                    sum += comple[i].Total;
                }
            }
            else
            {
                for (int i = 0; i < comple.Count; i++)
                {
                    sum += comple[i].Recorded;
                }
            }
            return sum;
        }

        /// <summary>
        /// 打开关闭数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unfold)
            {
                unfold = false;
                gridView1.CollapseAllGroups();
                return;
            }
            unfold = true;
            gridView1.ExpandAllGroups();
            Program.log.Info("打开/关闭 数据行");
        }

        /// <summary>
        /// 视频移除通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem item)) return;
            List<VideoPlay> videos = GetSelectRow();
            if (videos == null) return;
            Dictionary<int, List<VideoPlay>> VideoDic = new Dictionary<int, List<VideoPlay>>();
            foreach (VideoPlay video in videos)
            {
                if (!VideoDic.Keys.Contains(video.EquipmentID))
                {
                    VideoDic.Add(video.EquipmentID, new List<VideoPlay>());
                }
                VideoDic[video.EquipmentID].Add(video);
            }
            foreach (var it in VideoDic.Keys)
            {
                List<int> ids = VideoDic[it].Select(t => t.Id).ToList();
                if (!EquipmentData.DelteVideosFromEquipment(it, ids))
                {
                    MessageBox.Show($"{string.Join(",", ids)}从设备删除视频失败");
                    Program.log.Error($"{string.Join(",", ids)}从设备删除视频失败");
                }
            }
            MessageBox.Show("从设备删除视频成功");
            Program.log.Info($"{string.Join(",", videos.Select(t=>t.Id).ToList())}从设备删除视频失败");
            bindingSource1.DataSource = null;
            PostVideos();
        }

        /// <summary>
        /// 选择标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddlabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectLabel select = new SelectLabel(RefreshType.DynamicLabel);
            select.MyRefreshEvent += new SelectLabel.MyDelegate(AddTheLabels);
            select.ShowDialog();
        }

        /// <summary>
        /// 批量打标签
        /// </summary>
        /// <param name="labels"></param>
        public void AddTheLabels(List<string> labels)
        {
            List<int> ids = MyLabel.GetSelectIds(labels);
            List<VideoPlay> videos = GetSelectRow();
            if (videos == null) return;
            List<int> ids1 = videos.Select(t => t.Id).ToList();
            if (!LabelData.BatchAddLabels(ids1, ids))
            {
                MessageBox.Show("添加标签失败");
                Program.log.Error($"视频:{string.Join(",",ids1)}添加标签:{string.Join(",",labels)},失败");
                return;
            }
            MessageBox.Show("添加标签成功");
            Program.log.Error($"视频:{string.Join(",", ids1)}添加标签:{string.Join(",", labels)},成功");
            PostVideos();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            int i = gridView1.FocusedRowHandle;
            if (i < 0) return;
            int index = gridView1.GetDataRowHandleByGroupRowHandle(i);
            transmissionvideo = (VideoPlay)gridView1.GetRow(index);
            RefreshImage();
            GetIntToString();
            DeleteFolder(Program.ImageSavePath);
        }

        /// <summary>
        /// 标题栏改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            int id = int.Parse(GridGroupRowInfo.EditValue.ToString());
            int count = videoplays.Count(t => t.EquipmentID == id);
            if (id == 0)
                GridGroupRowInfo.GroupText = $"({(-e.RowHandle).ToString()})通道信息：" + GridGroupRowInfo.EditValue + ":无通道信息" + $"({count})";
            else
                GridGroupRowInfo.GroupText = $"({(-e.RowHandle).ToString()})通道信息：" + GridGroupRowInfo.EditValue + ":" + mydic[id].Name + $"({count})"+
                    $"({mydic[id].LabelStr})";
        }

        private List<VideoPlay> GetSelectRow()
        {
            int[] rownumber = gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return null;
            List<VideoPlay> videos = new List<VideoPlay>();
            foreach (var it in rownumber)
            {
                if (it < 0) continue;
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            return videos;
        }

        /// <summary>
        /// 批量添加解帧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<VideoPlay> videos = GetSelectRow();
            if (videos == null) return;
            BatchSolution batch = new BatchSolution(videos);
            batch.MyRefreshEvent += new BatchSolution.MyDeletgate(RefEquipment);
            batch.ShowDialog();
            Program.log.Info($"添加批量解帧{string.Join(",",videos.Select(t=>t.Id).ToList())}");
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bindingSource1.DataSource = null;
            PostVideos();
        }

        /// <summary>
        /// 视频加入通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> ids = GetCheckList().Select(t => t.Id).ToList();
            if (ids.Count == 0) return;
            selectEquipment.PostIds = ids;
            selectEquipment.Show();
        }

        /// <summary>
        /// 关联,关闭对应的窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformationDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            addEquipment.Close();
            selectEquipment.Close();
        }

        public void GroupIndex()
        {
            for (int i = -1; gridView1.IsGroupRow(i); i--)
            {
                int row = gridView1.GetDataRowHandleByGroupRowHandle(i);
                VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
                if (dr == null) return;
                if (dr.EquipmentID==0)
                {
                    gridView1.SetRowExpanded(i, true);
                }
            }
        }

        /// <summary>
        /// 通道添加标签
        /// </summary>
        /// <param name="labels"></param>
        public void SetLabelsToEquipment(List<string> labels)
        {
            int index = gridView1.FocusedRowHandle;
            int row = gridView1.GetDataRowHandleByGroupRowHandle(index);
            VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
            int id = dr.EquipmentID;
            if (id == 0) return;
            List<int> ids = MyLabel.GetSelectIds(labels);
            if (!LabelData.AddLabelToEquipment(id,ids))
            {
                MessageBox.Show("添加标签到通道失败");
                Program.log.Error($"通道ID:{id},添加标签:{string.Join(",",labels)}--失败");
                return;
            }
            Program.log.Error($"通道ID:{id},添加标签:{string.Join(",", labels)}");
            PostVideos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdsLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = gridView1.FocusedRowHandle;
            int row = gridView1.GetDataRowHandleByGroupRowHandle(index);
            VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
            int id = dr.EquipmentID;
            if (id == 0) return;
            SelectLabel select = new SelectLabel(RefreshType.StaticLabel, mydic[id].LabelStr);
            select.MyRefreshEvent += new SelectLabel.MyDelegate(SetLabelsToEquipment);
            select.ShowDialog();
        }

        /// <summary>
        /// 展示静态标签
        /// </summary>
        private void ShowStaticLabels()
        {
            int index = gridView1.FocusedRowHandle;
            int row = gridView1.GetDataRowHandleByGroupRowHandle(index);
            VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
            int id = dr.EquipmentID;
            ShowStaticLabel show = new ShowStaticLabel(id);
            show.Show();
        }

        /// <summary>
        /// 导出到execl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eXeclToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridToExcel1(gridControl1);
        }

        /// <summary>
        /// 导出到json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jsonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WriteJson(GetCheckList());
        }

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transmissionvideo?.Rquipment == null) return;
            if (MessageBox.Show($"是否删除{transmissionvideo.EquipmentName}通道？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (EquipmentData.DeleteEquipmengt(transmissionvideo.EquipmentID))
            {
                MessageBox.Show("删除成功");
                Program.log.Info($"通道:{transmissionvideo.EquipmentID + transmissionvideo.EquipmentName}删除--成功");
                GridViewClear();
                PostVideos();
                RefEquipment();
                return;
            }
            MessageBox.Show("删除失败");
            Program.log.Error($"通道:{transmissionvideo.EquipmentID + transmissionvideo.EquipmentName}删除--失败");
        }

        /// <summary>
        /// 修改通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdategroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transmissionvideo?.Rquipment == null) return;
            UpdateEquipment write = new UpdateEquipment(transmissionvideo.Rquipment);
            write.MyRefreshEvent += new UpdateEquipment.MyDelegate(PostVideos);
            write.MySaveEvent += new UpdateEquipment.MyDelegate(RefEquipment);
            write.Show();
        }

        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddgroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addEquipment.Show();
        }

        /// <summary>
        /// 添加自动截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutomaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> ids = GetCheckList().Select(t => t.Id).ToList();
            if (ids.Count == 0) return;
            bool win = VideoData.AddAutomaticScreenshot(ids);
            if (win)
            {
                MessageBox.Show("添加成功");
                Program.log.Info($"添加自动截图,视频ID{string.Join(",",ids)}");
                bindingSource1.DataSource = null;
                PostVideos();
                return;
            }
            MessageBox.Show("添加失败");
            Program.log.Error($"添加自动截图,视频ID{string.Join(",", ids)}--失败");
        }

        private void Top_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<VideoPlay> videos = GetSelectRow();
            if (videos == null) return;
            BatchSolution batch = new BatchSolution(videos,true);
            batch.MyRefreshEvent += new BatchSolution.MyDeletgate(RefEquipment);
            batch.ShowDialog();
            Program.log.Info($"添加批量解帧(置顶){string.Join(",", videos.Select(t => t.Id).ToList())}");
        }
    }
}