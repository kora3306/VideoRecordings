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
using System.Web.Script.Serialization;
using System.IO;
using Manina.Windows.Forms;
using Newtonsoft.Json.Linq;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DevExpress.XtraWaitForm;
using SeemmoData.Controls;
using VideoRecordings.Video;

namespace VideoRecordings
{
    public partial class QueryVideo : DevExpress.XtraEditors.XtraForm
    {
        public VideoPlay transmissionvideo = new VideoPlay();     //选中的文件
        public List<VideoPlay> videoplays = new List<VideoPlay>();  //所有筛选的文件
        public List<string> imageurl = new List<string>();       //选中文件的图片的url
        bool isFirst = true;  //每次查询选择定位选中文件0
        bool iscollpase = false;
        public bool Hasbeen = false;
        List<VideoProject> Videos = new List<VideoProject>();
        GridHitInfo hInfo = new GridHitInfo();
        Dictionary<string, int> queryvidoe = new Dictionary<string, int>();
        List<TreeNode> Nodes = new List<TreeNode>();
        List<string> replicators = new List<string>();
        MyLabel MyLabel;
        MyEquipment equipment;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public virtual void OnSave()
        {
            MyEvent?.Invoke();
        }

        public QueryVideo(List<VideoProject> inform)
        {
            InitializeComponent();
            Videos = inform;
            timeEdit_start.Time = DateTime.MinValue;
            timeEdit_end.Time = DateTime.MaxValue;
        }

        private void QueryVideo_Load(object sender, EventArgs e)
        {
            MyLabel = new MyLabel();
            Nodes = MyLabel.treeNodes;
            SetInformations();
            SetRadioButton();
            comboBox_proname.SelectedIndex = -1;
            comboBox_place.SelectedIndex = -1;
            comboBox_replicator.SelectedIndex = -1;
            dateTimePicker1.Value = Convert.ToDateTime("2010-1-1");
            dateTimePicker2.Value = DateTime.Now;
            List<string> text = MyLabel.LabelAll.Select(t => t.Value).ToList();
            textBox_label.AutoCompleteCustomSource.Clear();
            textBox_label.AutoCompleteCustomSource.AddRange(text.ToArray());
            textBox1.AutoCompleteCustomSource.Clear();
            textBox1.AutoCompleteCustomSource.AddRange(text.ToArray());
            imageListView1.DiskCache = Program.Persistent;
            gridView1.OptionsBehavior.AutoExpandAllGroups = false;
            label14.Text = $"欢迎:{Program.UserName}";
            Methods.AddIsTest(this);
        }

        private void SetInformations()
        {
            if (Videos == null || Videos.Count == 0) return;
            string getpath = Program.Urlpath + "/video/projects";
            var result = Videos.Where(p => !Videos.Any(q => (p != q && p.Scenes == q.Scenes)));
            List<string> pronames = Videos.Select(t => t.Name).ToList();
            comboBox_proname.DataSource = pronames;
            comboBox_proname.AutoCompleteCustomSource.Clear();
            comboBox_proname.AutoCompleteCustomSource.AddRange(pronames.ToArray());
            List<string> places = RepeatList(Videos, "place");
            comboBox_place.DataSource = places;
            comboBox_place.AutoCompleteCustomSource.Clear();
            comboBox_place.AutoCompleteCustomSource.AddRange(places.ToArray());
            replicators = RepeatList(Videos, "replicator");
            comboBox_replicator.DataSource = replicators;
            comboBox_replicator.AutoCompleteCustomSource.Clear();
            comboBox_replicator.AutoCompleteCustomSource.AddRange(replicators.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isFirst = true;
            iscollpase = false;
            gridView1.OptionsBehavior.AutoExpandAllGroups = false;
            RefreshImage();
        }

        /// <summary>
        /// 将填入的信息整理并转换成查询条件
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            string getjson = string.Empty;
            try
            {
                if (comboBox_proname.SelectedIndex != -1)
                {
                    getjson += $"project_name={comboBox_proname.Text.Trim()}&";
                }
                if (comboBox_place.SelectedIndex != -1)
                {
                    getjson += $"place={comboBox_place.Text}&";
                }
                if (comboBox_replicator.SelectedIndex != -1)
                {
                    getjson += $"replicator={comboBox_replicator.Text}&";
                }
                if (!string.IsNullOrEmpty(textBox_name.Text))
                {
                    getjson += $"name={textBox_name.Text.Trim()}&";
                }
                if (!string.IsNullOrEmpty(textBox_vid.Text))
                {
                    getjson += $"id={textBox_vid.Text.Trim()}&";
                }
                if (!radioButton3.Checked)
                {
                    if (radioButton1.Checked)
                    {
                        getjson += "status=0&";
                    }
                    if (radioButton2.Checked)
                    {
                        getjson += "status=1&";
                    }
                }
                if (!radioButton6.Checked)
                {
                    if (radioButton4.Checked)
                    {
                        getjson += "recorded=1&";
                    }
                    if (radioButton5.Checked)
                    {
                        getjson += "recorded=0&";
                    }
                }
                if (!radioButton9.Checked)
                {
                    if (radioButton7.Checked)
                    {
                        getjson += "deframed=1&";
                    }
                    if (radioButton8.Checked)
                    {
                        getjson += "deframed=0&";
                    }
                }
                getjson += $"start_date={dateTimePicker1.Value.ToString("yyyy-MM-dd")}&end_date={dateTimePicker2.Value.ToString("yyyy-MM-dd")}&";

                getjson += $"start_time={timeEdit_start.Text}&end_time={timeEdit_end.Text}&";

                if (textBox_label.Text.Trim() != string.Empty)
                {
                    string labelnum = GetLabesString(textBox_label.Text);
                    if (string.IsNullOrEmpty(labelnum))
                        return string.Empty;
                    getjson += $"label[]=[{labelnum}]";
                }

                if (getjson.EndsWith("&"))
                {
                    getjson = getjson.Substring(0, getjson.Length - 1);
                }

                return getjson;

            }
            catch (Exception)
            {
                MessageBox.Show("查询条件有误");
                throw;
            }

        }

        /// <summary>
        /// 刷新图片
        /// </summary>
        public void RefreshImage()
        {
            string json = GetJson();
            if (string.IsNullOrEmpty(json)) return;
            WaitFormEx.Run(() =>
            {
                string url = Program.Urlpath + "/videos";
                if (!GettListVideo(url, json))
                    return;
            });
            if (videoplays.Count == 0)
            {
                MessageBox.Show("没有符合筛选条件的视频");
                bindingSource1.DataSource = videoplays;
                return;
            }
            bindingSource1.DataSource = null;
            bindingSource1.DataSource = videoplays;
            if (isFirst)
            {
                transmissionvideo = videoplays.Count == 0 ? null : videoplays.First();
                isFirst = false;
            }
            RefreshFocus();
            gridView1.RefreshData();
            Program.log.Error($"搜索条件{json}");
        }

        public void RefreshNewImage(VideoPlay video)
        {
            imageurl.Clear();
            string url = Program.Urlpath + "/video/snapshot/";
            foreach (var item in video.ImageId)
            {
                imageurl.Add(url + item);
            }
            SetTheListView();
        }

        /// <summary>
        /// 刷新选中的图片的标签和图片
        /// </summary>
        public void RefreshFocus()
        {
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
        /// 将返回的文件信息转化成对象集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public bool GettListVideo(string url, string na)
        {
            videoplays.Clear();
            JObject obj = WebClinetHepler.GetJObject(url, na);
            if (obj == null)
            {
                MessageBox.Show("请求失败");
                return false;
            }
            videoplays = GerQueryVideos(obj);
            return true;
        }

        private List<VideoPlay> GerQueryVideos(JObject obj)
        {
            queryvidoe.Clear();
            List<VideoPlay> palys = new List<VideoPlay>();
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                Projects project = JsonHelper.DeserializeDataContractJson<Projects>(obj["result"][i]["project"].ToString());
                for (int j = 0; j < obj["result"][i]["equipments"].Count(); j++)
                {
                    EquipmentInfo equipment = JsonHelper.DeserializeDataContractJson<EquipmentInfo>(obj["result"][i]["equipments"][j]["equipment_info"].ToString());
                    Completeness comple = JsonHelper.DeserializeDataContractJson<Completeness>(obj["result"][i]["equipments"][j]["statistic"].ToString());
                    List<VideoPlay> videos = JsonHelper.DeserializeDataContractJson<List<VideoPlay>>(obj["result"][i]["equipments"][j]["videos"].ToString());
                    videos.ForEach(t => { t.Project = project; t.Rquipment = equipment; });
                    palys.AddRange(videos);
                    if (queryvidoe.Keys.Contains(project.ProjectId))
                    {
                        queryvidoe[project.ProjectId] += comple.Total;
                    }
                    else
                    {
                        queryvidoe.Add(project.ProjectId, comple.Total);
                    }
                }
            }
            return palys;
        }

        /// <summary>
        /// 将填入的标签转化成对应的条件筛选语句
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Conversion(string str)
        {
            string[] strs = str.Split(',');
            string json = (new JavaScriptSerializer()).Serialize(strs);
            return json;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                //case Keys.Enter:
                //    if (SetScreeningLabels())
                //    {
                //        return true;
                //    }
                //    button1.PerformClick();
                //    break;
                case Keys.D:
                    DELToolStripMenuItem.PerformClick();
                    return true;
                case Keys.Up:
                    if (gridControl1.Focused)
                    {
                        gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == 0 ? 0 : gridView1.FocusedRowHandle - 1;
                        if (gridView1.FocusedRowHandle < 0)
                        {
                            return true;
                        }
                        transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                        GetIntToString();
                        return true;
                    }
                    return base.ProcessCmdKey(ref msg, keyData);
                case Keys.Down:
                    if (gridControl1.Focused)
                    {
                        gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                        if (gridView1.FocusedRowHandle < 0)
                        {
                            return true;
                        }
                        transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
                        GetIntToString();
                        return true;
                    }
                    return base.ProcessCmdKey(ref msg, keyData);
                case Keys.Escape:
                    this.WindowState = FormWindowState.Minimized;
                    return true;
                case Keys.F2:
                    Methods.OpenFolderAndSelectFile(Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri)));
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 将数字转化成需要展示的数据
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
        /// 焦点行变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            if (rowIndex < 0)
            {
                return;
            }
            transmissionvideo = (VideoPlay)gridView1.GetRow(rowIndex);
            GetIntToString();
            DeleteFolder(Program.ImageSavePath);
        }

        /// <summary>
        /// 清空文件夹
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
        }

        /// <summary>
        /// 转化需要的图片成url
        /// </summary>
        public void GetIntToString()
        {
            if (transmissionvideo == null || transmissionvideo.ImageId == null)
            {
                imageListView1.Items.Clear();
                return;
            }
            imageurl.Clear();
            string url = Program.Urlpath + "/video/snapshot/";
            imageurl = transmissionvideo.ImageId.Select(t => url + t).ToList();
            SetTheListView();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        public void SetTheListView()
        {
            imageListView1.SuspendPaint();
            imageListView1.Items.Clear();
            imageListView1.ThumbnailSize = new Size(300, 180);
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            foreach (string item in imageurl)
            {
                ImageListViewItem it = new ImageListViewItem(item) { Text = item.Split('/').Last() };
                items.Add(it);
            }
            imageListView1.Items.AddRange(items.ToArray());
            imageListView1.ResumePaint();
        }

        /// <summary>
        /// 点击改变需要加载的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_Click(object sender, EventArgs e)
        {
            int i = gridView1.FocusedRowHandle;
            int index = gridView1.GetDataRowHandleByGroupRowHandle(i);
            transmissionvideo = (VideoPlay)gridView1.GetRow(index);
            GetIntToString();
            DeleteFolder(Program.ImageSavePath);
            AddItems();
        }

        /// <summary>
        /// 双击播放
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
        /// 播放视频
        /// </summary>
        private void OpenVideoPaly()
        {
            VideoRecording recording = new VideoRecording(transmissionvideo, Hasbeen);
            recording.MyEvent += new VideoRecording.MyDelegate(RefreshAllData);
            recording.SetMyRecordEvent += new VideoRecording.MyRecordDelegate(SetTheUse);
            if (transmissionvideo.Uri == null) return;
            if (File.Exists(Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri))))
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
            }
        }

        /// <summary>
        /// 转化需要的路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ConversionString(string path)
        {
            string[] n = path.Split('/');
            string Ret = string.Empty;
            for (int i = 0; i < n.Length; i++)
            {
                Ret += n[i] + "\\";
            }
            return Ret.Substring(0, Ret.Length - 1);
        }

        /// <summary>
        /// 刷新改变后的数据
        /// </summary>
        /// <param name="play"></param>
        public void RefreshData(VideoPlay play)
        {
            foreach (var item in videoplays)
            {
                if (item.Name == play.Name)
                {
                    item.Labels = play.Labels;
                    item.ImageId = play.ImageId;
                    item.StartTime = play.StartTime;
                    item.EndTime = play.EndTime;
                    item.RecordTime = play.RecordTime;
                    break;
                }
            }
            bindingSource1.DataSource = videoplays;
        }

        public void RefreshAllData(VideoPlay play)
        {
            RefreshData(play);
            RefreshNewImage(play);
        }

        /// <summary>
        /// 查询界面关闭后改变查询的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryVideo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Program.IsQuery = false;
        }

        /// <summary>
        /// 双击放大图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageListView1_DoubleClick(object sender, EventArgs e)
        {
            Methods.ShowImage(imageListView1);
        }

        /// <summary>
        /// 删除图片按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Methods.DelImage(imageListView1, imageurl);
            RefreshImage();
            gridView1.RefreshData();
        }

        /// <summary>
        /// 改变对应条件行颜色
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || e.RowHandle > videoplays.Count() - 1)
            {
                return;
            }
            VideoPlay video = (VideoPlay)gridView1.GetRow(e.RowHandle);
            if (video == null) return;
            if (video.ImageId.Count != 0 && video.Labels.Count != 0)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (video.ImageId.Count != 0 && video.Labels.Count == 0)
            {
                e.Appearance.BackColor = Color.Khaki;
            }
            else if (video.ImageId.Count == 0 & video.Labels.Count != 0)
            {
                e.Appearance.BackColor = Color.LightPink;
            }
            if (e.RowHandle == gridView1.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.SkyBlue;
            }
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.Remove)
            {
                if (MessageBox.Show("删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 删除视频按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"是否删除编号{transmissionvideo.Id}的视频？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            string url = Program.Urlpath + "/video/" + transmissionvideo.Id;
            JObject obj = WebClinetHepler.Delete_New(url);
            if (obj == null)
            {
                MessageBox.Show("删除失败");
                Program.log.Error($"删除{transmissionvideo.Id}失败", new Exception($"{url}"));
            }
            videoplays.Remove(transmissionvideo);
            bindingSource1.DataSource = videoplays;
            gridView1.RefreshData();
            OnSave();
            Program.log.Error($"删除{transmissionvideo.Id}", new Exception($"{url}"));
        }

        private void SetRadioButton()
        {
            radioButton1.Checked = true;
            radioButton4.Checked = true;
            radioButton9.Checked = true;
        }

        /// <summary>
        /// 提取不重复的集合属性
        /// </summary>
        /// <param name="video"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private List<string> RepeatList(List<VideoProject> video, string i)
        {
            List<string> relist = new List<string>();
            switch (i)
            {
                case "place":
                    foreach (var item in video)
                    {
                        if (!relist.Contains(item.Place))
                        {
                            relist.Add(item.Place);
                        }
                    }
                    break;
                case "replicator":
                    foreach (var item in video)
                    {
                        if (!relist.Contains(item.Replicator))
                        {
                            relist.Add(item.Replicator);
                        }
                    }
                    break;
                default:
                    break;
            }
            return relist;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MyLabel.LabelsNumber.Count == 0) return;
            button1.Focus();
            SelectLabel select = new SelectLabel(textBox_label.Text, true);
            select.MyRefreshEvent += new SelectLabel.MyDelegate(StartScreening);
            select.MySaveEvent += new SelectLabel.MyDelegate(AddTheLabels);
            select.ShowDialog();
        }

        /// <summary>
        ///  设置显示标签
        /// </summary>
        /// <param name="label"></param>
        public void StartScreening(List<string> label)
        {
            textBox_label.Text = string.Join(",", label);
        }

        /// <summary>
        /// 将标签转成Id集合
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetLabesString(string text)
        {
            List<string> labels = text.Split(',').ToList();
            string labeljson = string.Empty;
            foreach (var item in labels)
            {
                if (MyLabel.LabelAll.Values.Contains(item))
                {
                    string keys = MyLabel.LabelAll.Where(q => q.Value == item).Select(q => q.Key).First();
                    labeljson += keys + ",";
                }
                else
                {
                    MessageBox.Show($"不存在\"{item}\"标签");
                    return string.Empty;
                }
            }
            if (labeljson.EndsWith(","))
            {
                labeljson = labeljson.Substring(0, labeljson.Length - 1);
            }
            return labeljson;
        }

        /// <summary>
        /// 重置标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            RefAllConditions();
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }

        private bool SetScreeningLabels()
        {
            if (textBox1.Focused)
            {
                textBox_label.Text += textBox1.Text + ",";
                textBox1.Text = string.Empty;
                return true;
            }
            return false;
        }

        private void RefAllConditions()
        {
            SetRadioButton();
            comboBox_proname.SelectedIndex = -1;
            comboBox_proname.Text = string.Empty;
            comboBox_replicator.SelectedIndex = -1;
            comboBox_replicator.Text = string.Empty;
            comboBox_place.SelectedIndex = -1;
            comboBox_place.Text = string.Empty;
            dateTimePicker1.Value = Convert.ToDateTime("2010-1-1");
            dateTimePicker2.Value = DateTime.Now;
            timeEdit_start.Time = DateTime.MinValue;
            timeEdit_end.Time = DateTime.MaxValue;
            textBox_label.Text = string.Empty;
            textBox_name.Text = string.Empty;
            textBox_vid.Text = string.Empty;
        }

        private void gridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            GridView gridview = sender as GridView;
            if (GridGroupRowInfo.Column.GroupIndex == 0)
            {
                int index = gridview.GetDataRowHandleByGroupRowHandle(e.RowHandle);
                string uri = gridview.GetRowCellValue(index, "Uri").ToString();
                string project_name = gridview.GetRowCellValue(index, "ProjectName").ToString();
                GridGroupRowInfo.GroupText = "数据编号:" + uri.Split('/').ToList()
                     .First(t => t.StartsWith("SP") || t.StartsWith("Sp")) + $" (数量:{queryvidoe[$"{project_name}"]})";
            }
        }

        /// <summary>
        /// 导出GridControl所有的信息
        /// </summary>
        /// <param name="gridControl1"></param>
        private void DataGridToExcel(GridControl gridControl1)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出数据信息";
            fileDialog.Filter = "Microsoft Excel|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gridControl1.ExportToXls(fileDialog.FileName);
                    MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK);
                }
                catch (Exception)
                {
                    MessageBox.Show("导出失败！", "提示", MessageBoxButtons.OK);
                }


            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DataGridToExcel1(gridControl1);
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

        public void WriteJson(List<VideoPlay> palys)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Json";
            saveFileDialog.Filter = "Jsom文件(*.json)|*.json";
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
                JsonWriter writer = new JsonTextWriter(sw);
                //把模型数据序列化并写入Json.net的JsonWriter流中 
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, palys);
                //ser.Serialize(writer, ht); 
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                writer.Close();
                sw.Close();
            }
        }

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

        private void button4_Click(object sender, EventArgs e)
        {
            WriteJson(GetCheckList());
        }

        /// <summary>
        /// 展开关闭节点
        /// </summary>
        public void Unfold()
        {
            if (!iscollpase)
            {
                iscollpase = true;
                gridView1.ExpandAllGroups();
            }
            else
            {
                iscollpase = false;
                gridView1.CollapseAllGroups();
            }

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Unfold();
        }

        /// <summary>
        /// 刷新首页任务
        /// </summary>
        public void RefshHomePage()
        {
            OnSave();
        }

        private void ADDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Equipment equipment = new Equipment(transmissionvideo.ProjectName);
            equipment.MySaveEvent += new Equipment.MyDelegate(AddItems);
            equipment.MyRefreshEvent += new Equipment.MyDelegate(RefreshImage);
            equipment.Show();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem item)) return;
            int[] rownumber = this.gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return;
            List<VideoPlay> videos = new List<VideoPlay>();
            foreach (var it in rownumber)
            {
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            int id = equipment.AllEquipmengt.FirstOrDefault(t => t.Value == item.Text.Split(':').Last()).Key;
            List<int> ids = videos.Select(t => t.Id).ToList();
            if (!GetData.AddVideoInEquipment(id, ids))
            {
                MessageBox.Show("添加视频到设备失败");
                return;
            }
            MessageBox.Show("添加视频到设备成功");
            bindingSource1.DataSource = null;
            button1.PerformClick();
        }

        /// <summary>
        /// 添加右键设备项
        /// </summary>
        public void AddItems()
        {
            if (transmissionvideo == null) return;
            equipment = new MyEquipment(transmissionvideo.ProjectName);
            INToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in equipment.Equipments.OrderByDescending(t => t.Id))
            {
                ToolStripMenuItem it = new ToolStripMenuItem() { Text = item.Id + ":" + item.Name };
                it.Click += ToolStripMenuItem_Click;
                INToolStripMenuItem.DropDownItems.Add(it);
            }
        }

        /// <summary>
        /// 视频移出设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem item)) return;
            int[] rownumber = this.gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return;
            List<VideoPlay> videos = new List<VideoPlay>();
            foreach (var it in rownumber)
            {
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            Dictionary<string, List<VideoPlay>> VideoDic = new Dictionary<string, List<VideoPlay>>();
            foreach (VideoPlay video in videos)
            {
                if (!VideoDic.Keys.Contains(video.EquipmentName))
                {
                    VideoDic.Add(video.EquipmentName, new List<VideoPlay>());
                }
                VideoDic[video.EquipmentName].Add(video);
            }
            foreach (var it in VideoDic.Keys)
            {
                int id = equipment.AllEquipmengt.FirstOrDefault(t => t.Value == it).Key;
                List<int> ids = VideoDic[it].Select(t => t.Id).ToList();
                if (!GetData.DelteVideosFromEquipment(id, ids))
                {
                    MessageBox.Show($"{string.Join(",", ids)}从设备删除视频失败");
                }
            }
            MessageBox.Show("从设备删除视频成功");
            bindingSource1.DataSource = null;
            button1.PerformClick();
        }

        private void SetTheUse()
        {
            Hasbeen = !Hasbeen;
        }

        private void AddTheLabels(List<string> label)
        {
            List<int> ids = MyLabel.GetIds(label);
            int[] rownumber = this.gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return;
            List<VideoPlay> videos = new List<VideoPlay>();
            foreach (var it in rownumber)
            {
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            List<int> ids1 = videos.Select(t => t.Id).ToList();
            if (!GetData.BatchAddLabels(ids1, ids))
            {
                MessageBox.Show("添加标签失败");
                return;
            }
            MessageBox.Show("添加标签成功");
            RefreshImage();
        }

        private void AddlabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyLabel.LabelsNumber.Count == 0) return;
            button1.Focus();
            SelectLabel select = new SelectLabel(textBox_label.Text);
            select.MyRefreshEvent += new SelectLabel.MyDelegate(StartScreening);
            select.MySaveEvent += new SelectLabel.MyDelegate(AddTheLabels);
            select.ShowDialog();
        }

        private void textBox_label_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        public List<int> GetGroupIds(VideoPlay video)
        {
            return videoplays.Where(t => t.EquipmentName == video.EquipmentName).Select(w => w.Id).ToList();
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }

        private void ADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = equipment.AllEquipmengt.FirstOrDefault(t => t.Value == transmissionvideo.EquipmentName.Split(':').Last()).Key;
            AddEquipment write = new AddEquipment(transmissionvideo.ProjectName, index);
            write.MySaveEvent += new AddEquipment.MyDelegate(AddItems);
            write.Show();
        }

        private void UPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = equipment.AllEquipmengt.FirstOrDefault(t => t.Value == transmissionvideo.EquipmentName.Split(':').Last()).Key;
            Write write = new Write(transmissionvideo.ProjectName, index);
            write.MySaveEvent += new Write.MyDelegate(AddItems);
            write.MyRefreshEvent += new Write.MyDelegate(RefreshImage);
            write.Show();
        }

        private void DELEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.BeginUpdate();
            gridView1.BeginDataUpdate();
            var index = equipment.AllEquipmengt.FirstOrDefault(t => t.Value == transmissionvideo.EquipmentName.Split(':').Last());
            if (MessageBox.Show($"是否删除[{index.Value}]？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!GetData.DeleteEquipmengt(index.Key))
            {
                MessageBox.Show("删除失败");
                return;
            }
            MessageBox.Show("删除成功");
            RefreshImage();
            gridView1.EndDataUpdate();//结束数据的编辑
            gridView1.EndUpdate();   //结束视图的编辑
            AddItems();
        }
    }
}