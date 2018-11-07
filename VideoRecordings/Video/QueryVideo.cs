using System;
using System.Collections.Generic;
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
using DevExpress.XtraBars.Docking;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DevExpress.XtraWaitForm;
using SeemmoData.Controls;
using VideoRecordings.Equipment;
using VideoRecordings.GetDatas;
using VideoRecordings.Models;
using VideoRecordings.Video;
using VideoRecordings.Label;

namespace VideoRecordings
{
    public partial class QueryVideo : XtraForm
    {
        public VideoPlay transmissionvideo = new VideoPlay();     //选中的文件
        public List<VideoPlay> videoplays = new List<VideoPlay>();  //所有筛选的文件
        public List<string> imageurl = new List<string>();       //选中文件的图片的url
        bool isFirst = true;  //每次查询选择定位选中文件0
        bool iscollpase = false;
        private bool isvisbel = false;
        List<VideoProject> Videos = new List<VideoProject>();
        GridHitInfo hInfo = new GridHitInfo();
        private List<EquipmentInfo> equipments = new List<EquipmentInfo>();
        private Dictionary<int, EquipmentInfo> mydic = new Dictionary<int, EquipmentInfo>();
        List<string> replicators = new List<string>();
        List<string> places = new List<string>();

        List<string> listOnitGroup = new List<string>();
        List<string> listNewGrouop = new List<string>();  //搜索集合

        List<string> listOnitGroups = new List<string>();
        List<string> listNewGrouops = new List<string>();  //搜索集合

        List<string> listOnitProName = new List<string>();
        List<string> listNewProName = new List<string>();

        private MyLabels MyLabels;

        private AddEquipment addEquipment;
        private SelectEquipment selectEquipment;

        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public virtual void OnSave()
        {
            MyEvent?.Invoke();
        }

        public QueryVideo()
        {
            InitializeComponent();
           
        }

        private void QueryVideo_Load(object sender, EventArgs e)
        {
            addEquipment = new AddEquipment();
            selectEquipment = new SelectEquipment();
            addEquipment.MySaveEvent += new AddEquipment.MyDelegate(RefEquipment);
            selectEquipment.MySaveEvent += new SelectEquipment.MyEvent(RefreshImage);
            selectEquipment.MyRefreshEvent += new SelectEquipment.MyEvent(GridViewClear);
            dockPanel5.Visibility = DockVisibility.Hidden;
            WaitFormEx.Run(() => 
            {
                Videos = VideoData.GetAllFolder();
                MyLabels = new MyLabels();            
                places = RepeatList(Videos, "place");
                replicators = RepeatList(Videos, "replicator");
                listOnitProName = Videos.Select(t => t.Name).ToList();
                listOnitGroups = GroupData.GetAllGalleryGroup().OrderBy(t => t.Id).Select(t => $"{t.Id}:{t.Name}").ToList();
                listOnitGroup = MyGroup.GetGetGroups().Equipments.OrderBy(t => t.Id).Select(t => $"{t.Id}:{t.Name}").ToList();
            });

            List<string> text = MyLabels.AllLabelsToDic.Select(t => t.Value).ToList();
            textBox_label.AutoCompleteCustomSource.Clear();
            textBox_label.AutoCompleteCustomSource.AddRange(text.ToArray());

            SetComboBoxInfo(places, comboBox1);
            SetComboBoxInfo(replicators, comboBox2);

            comboBox_proname.Items.AddRange(listOnitProName.ToArray());
            comboBox_groups.Items.AddRange(listOnitGroups.ToArray());
            comboBox_group.Items.AddRange(listOnitGroup.ToArray());

            imageListView1.DiskCache = AppSettings.Persistent;
            gridView1.OptionsBehavior.AutoExpandAllGroups = false;
            Methods.AddIsTest(this);
            RefAllConditions();
        }

        public void RefEquipment()
        {
            selectEquipment.Refresh();
        }

        public void GridViewClear()
        {
            bindingSource1.DataSource = null;
        }

        public  void SetComboBoxInfo(List<string>texts, System.Windows.Forms.ComboBox combo)
        {
            combo.DataSource = texts;
            combo.AutoCompleteCustomSource.Clear();
            combo.AutoCompleteCustomSource.AddRange(texts.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isFirst = true;
            iscollpase = false;
            imageurl.Clear();
            gridView1.OptionsBehavior.AutoExpandAllGroups = false;
            RefreshImage();
            mydic = equipments.ToDictionary(t=>t.Id);
        }

        /// <summary>
        /// 将填入的信息整理并转换成查询条件
        /// </summary>
        /// <returns></returns>
        public string GetCriteriaJson()
        {
            string getjson = string.Empty;
            try
            {
                if (comboBox_proname.SelectedIndex != -1)
                {
                    getjson += $"project_name={comboBox_proname.Text.Trim()}&";
                }
                if (comboBox1.SelectedIndex != -1)
                {
                    getjson += $"place={comboBox1.Text}&";
                }
                if (comboBox2.SelectedIndex != -1)
                {
                    getjson += $"replicator={comboBox2.Text}&";
                }
                if (!string.IsNullOrEmpty(textBox_name.Text))
                {
                    getjson += $"name={textBox_name.Text.Trim()}&";
                }
                if (!string.IsNullOrEmpty(textBox_vid.Text))
                {
                    getjson += $"id={textBox_vid.Text.Trim()}&";
                }
                if (radioGroup1.SelectedIndex!=2)
                {
                    getjson += $"status={radioGroup1.SelectedIndex}&";
                }
                if (radioGroup2.SelectedIndex!=2)
                {
                    if (radioGroup2.SelectedIndex==0)
                    {
                        getjson += "recorded=1&";
                    }
                    if (radioGroup2.SelectedIndex ==1)
                    {
                        getjson += "recorded=0&";
                    }
                }
                if (radioGroup3.SelectedIndex!=2)
                {
                    if (radioGroup3.SelectedIndex==0)
                    {
                        getjson += "deframed=2&";
                    }
                    if (radioGroup3.SelectedIndex==1)
                    {
                        getjson += "deframed=0&";
                    }
                }
                getjson += $"start_date={dateTimePicker1.Value.ToString("yyyy-MM-dd")}&end_date={dateTimePicker2.Value.ToString("yyyy-MM-dd")}&";

                getjson += $"start_time={timeEdit_start.Text}&end_time={timeEdit_end.Text}&";

                List<int> ids = GetEquipmenIds(comboBox_group.Text.Trim());
                if (ids.Count!=0)
                {
                    getjson += $"equipments=[{string.Join(",",ids)}]&";
                }

                if (!string.IsNullOrEmpty(comboBox_groups.Text.Trim()))
                {
                    getjson += $"equip_groups=[{int.Parse(comboBox_groups.Text.Trim().Split(':').First())}]&";
                }

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
            string json = GetCriteriaJson();
            if (string.IsNullOrEmpty(json)) return;
            int count = VideoData.GetQueryVideoCount(json);
            if (count>1000)
            {
               if(MessageBox.Show($"查询的条目共{count}条,查询时间过长,是否添加其他条件","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
                return;
            }
            WaitFormEx.Run(() =>
            {
                string url = AppSettings.Urlpath + "/videos?" + json;
                if (!GettListVideo(url))
                    return;
            });
            if (videoplays.Count == 0)
            {
                MessageBox.Show("没有符合筛选条件的视频");
                bindingSource1.DataSource = videoplays;
                return;
            }
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
            string url = AppSettings.Urlpath + "/video/snapshot/";
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
        public bool GettListVideo(string url)
        {
            videoplays.Clear();
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null)
            {
                MessageBox.Show("请求失败");
                return false;
            }
            videoplays = GerQueryVideos(obj);
            return true;
        }

        /// <summary>
        /// 转化类型到对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private List<VideoPlay> GerQueryVideos(JObject obj)
        {
            equipments.Clear();
            List<VideoPlay> palys = new List<VideoPlay>();
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                Projects project = JsonConvert.DeserializeObject<Projects>(obj["result"][i]["project"].ToString());
                for (int j = 0; j < obj["result"][i]["equipments"].Count(); j++)
                {
                    EquipmentInfo equipment = JsonConvert.DeserializeObject<EquipmentInfo>(obj["result"][i]["equipments"][j]["equipment_info"].ToString());
                    Completeness comple = JsonConvert.DeserializeObject<Completeness>(obj["result"][i]["equipments"][j]["statistic"].ToString());
                    List<VideoPlay> videos = JsonConvert.DeserializeObject<List<VideoPlay>>(obj["result"][i]["equipments"][j]["videos"].ToString());
                    videos.ForEach(t => { t.Project = project; t.Rquipment = equipment; });
                    if (equipments.Count(t=>t.Id==equipment.Id)==0)
                        equipments.Add(equipment);
                    palys.AddRange(videos);
                }
            }
            return palys.OrderBy(t=>t.EquipmentID).ToList();
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
                //    if(comboBox_group.Focused)
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
                case Keys.F2:
                    Methods.OpenFolderAndSelectFile(AppSettings.ReturnStringUrl(ConversionString(transmissionvideo.Uri)));
                    return true;
                //case Keys.Q:
                //    ShowStaticLabels();
                //    return true;
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
            DeleteFolder(AppSettings.ImageSavePath);
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
            Program.log.Info($"清空{dir}文件夹内容");
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
            string url = AppSettings.Urlpath + "/video/snapshot/";
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
            DeleteFolder(AppSettings.ImageSavePath);
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
            VideoRecording recording = new VideoRecording(transmissionvideo,videoplays);
            //recording.MyEvent += new VideoRecording.MyDelegate(RefreshImage);
            if (transmissionvideo.Uri == null) return;
            if (File.Exists(AppSettings.ReturnStringUrl(ConversionString(transmissionvideo.Uri))))
            {
                recording.Show();
                Program.log.Error($"打开{AppSettings.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri))}", new Exception("打开成功"));
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
            VideoPlay video = videoplays.FirstOrDefault(t => t.Id == play.Id);
            video.Labels = play.Labels;
            video.ImageId = play.ImageId;
            video.StartTime = play.StartTime;
            video.EndTime = play.EndTime;
            video.CreateTime = play.CreateTime;
            video.Recorded = play.Recorded;
            video.RecordTime = play.RecordTime;
            bindingSource1.DataSource = videoplays;
            bindingSource1.DataSource = videoplays;
            gridView1.RefreshData();
        }

        public void RefreshAllData(VideoPlay Rplay)
        {
            VideoPlay play = Methods.GetNewImages(Rplay.Id);
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
            Program.log.Info($"删除图片,video_ids:{string.Join(",",imageListView1.SelectedItems.Select(t=>t.Name).ToList())}");
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
            if (video.ImageId.Count != 0 && !string.IsNullOrEmpty(video.Label))
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (video.ImageId.Count != 0 && !string.IsNullOrEmpty(video.Label))
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
            if (MessageBox.Show($"是否删除编号{transmissionvideo.Id}的视频,删除视频前请确认解帧图片一并删除？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (!VideoData.DeleteVideo(transmissionvideo.Id))
            {
                MessageBox.Show("删除失败");
                Program.log.Error($"删除{transmissionvideo.Id}失败", new Exception($"{transmissionvideo.Id}"));
            }
            videoplays.Remove(transmissionvideo);
            bindingSource1.DataSource = videoplays;
            gridView1.RefreshData();
            OnSave();
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
                        if (!relist.Contains(item.Place)&&!string.IsNullOrEmpty(item.Place))
                        {
                            relist.Add(item.Place);
                        }
                    }
                    break;
                case "replicator":
                    foreach (var item in video)
                    {
                        if (!relist.Contains(item.Replicator)&&!string.IsNullOrEmpty(item.Replicator))
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
            button1.Focus();
            SelectLabel select = new SelectLabel(RefreshType.None,textBox_label.Text);
            select.MyRefreshEvent += new SelectLabel.MyDelegate(StartScreening);
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
                if (MyLabels.AllLabelsToDic.Values.Contains(item))
                {
                    string keys = MyLabels.AllLabelsToDic.FirstOrDefault(q => q.Value == item).Key.ToString();
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

        /// <summary>
        /// 重置
        /// </summary>
        private void RefAllConditions()
        {
            radioGroup1.SelectedIndex = 0;
            radioGroup2.SelectedIndex = 0;
            radioGroup3.SelectedIndex = 2;
            comboBox_proname.SelectedIndex = -1;
            comboBox_proname.Text = string.Empty;
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            comboBox_group.SelectedIndex = -1;
            comboBox_groups.SelectedIndex = -1;
            comboBox1.Text = string.Empty;
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
                int id = int.Parse(GridGroupRowInfo.EditValue.ToString());
                int count = videoplays.Count(t => t.EquipmentID == id);
                if (id == 0)
                    GridGroupRowInfo.GroupText = $"通道信息:{GridGroupRowInfo.EditValue}:无通道信息(数量:{count})";
                else
                    GridGroupRowInfo.GroupText = $"通道信息：{GridGroupRowInfo.EditValue} :{mydic[id].Name}({mydic[id].LabelStr})(数量:{count})";
            }
            else
            {
                try
                {
                    int index = gridview.GetDataRowHandleByGroupRowHandle(e.RowHandle);
                    string uri = gridview.GetRowCellValue(index, "Uri").ToString();
                    string project_name = gridview.GetRowCellValue(index, "ProjectName").ToString();
                    int EquipmentID = int.Parse(gridview.GetRowCellValue(index, "EquipmentID").ToString());
                    int count = videoplays.Count(t => t.EquipmentID == EquipmentID && t.ProjectName == project_name);
                    GridGroupRowInfo.GroupText = "数据编号:" + uri.Split('/').ToList()
                         .First(t => t.StartsWith("SP") || t.StartsWith("Sp")) + $" (数量:{count})";
                }
                catch (Exception)
                {
                    return;
                }

            }
        }

        /// <summary>
        /// 导出GridControl所有的信息
        /// </summary>
        /// <param name="gridControl1"></param>
        private void DataGridToExcel(GridControl gridControl1)
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Title = "导出数据信息",
                Filter = "Microsoft Excel|*.xls"
            };
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

        /// <summary>
        /// 显示高级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dockPanel2.Visibility == DockVisibility.AutoHide)
            {
                dockPanel2.Visibility = DockVisibility.Visible;
            }
            if (!isvisbel)
            {
                dockPanel5.Visibility = DockVisibility.Visible;
                isvisbel = true;
                button3.Text = "高级选项↓";
            }
            else
            {
                dockPanel5.Visibility = DockVisibility.Hidden;
                isvisbel = false;
                button3.Text = "高级选项↑";
            }
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
                    //把模型数据序列化并写入Json.net的JsonWriter流中 
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(writer, palys);
                //ser.Serialize(writer, ht); 
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                writer.Close();
                sw.Close();
            }
            Program.log.Error($"导出信息到json,路径:{con_file_path}");
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
                if (it < 0) continue;
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
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
            bindingSource1.DataSource = null;
            button1.PerformClick();
            Program.log.Info($"{string.Join(",", videos.Select(t => t.Id).ToList())}从设备删除视频失败");
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="label"></param>
        private void AddTheLabels(List<string> labels)
        {
            List<int> ids = MyLabels.GetSelectIds(labels);
            int[] rownumber = this.gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return;
            List<VideoPlay> videos = new List<VideoPlay>();
            foreach (var it in rownumber)
            {
                if (it < 0) continue;
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            List<int> ids1 = videos.Select(t => t.Id).ToList();
            if (!LabelData.BatchAddLabels(ids1, ids))
            {
                MessageBox.Show("添加标签失败");
                Program.log.Error($"视频:{string.Join(",", ids1)}添加标签:{string.Join(",", labels)},失败");
                return;
            }
            MessageBox.Show("添加标签成功");
            RefreshImage();
            Program.log.Error($"视频:{string.Join(",", ids1)}添加标签:{string.Join(",", labels)},成功");
        }

        /// <summary>
        /// 批量添加标签
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
        /// 查询界面快捷键Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 判断点击是否是数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            hInfo = gridView1.CalcHitInfo(e.Y, e.Y);
        }

        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addEquipment.Show();
        }

        /// <summary>
        /// 修改通道名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transmissionvideo?.Rquipment == null) return;
            UpdateEquipment write = new UpdateEquipment(transmissionvideo.Rquipment);
            write.MyRefreshEvent += new UpdateEquipment.MyDelegate(RefreshImage);
            write.MySaveEvent += new UpdateEquipment.MyDelegate(RefEquipment);
            write.Show();
        }

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DELEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.BeginUpdate();
            gridView1.BeginDataUpdate();
            if (transmissionvideo?.Rquipment == null) return;
            if (MessageBox.Show($"是否删除{transmissionvideo.EquipmentName}通道？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            if (EquipmentData.DeleteEquipmengt(transmissionvideo.EquipmentID))
            {
                MessageBox.Show("删除成功");
                RefreshImage();
                RefEquipment();
                Program.log.Info($"通道:{transmissionvideo.EquipmentID + transmissionvideo.EquipmentName}删除--成功");
                return;
            }
            MessageBox.Show("删除失败");
            Program.log.Error($"通道:{transmissionvideo.EquipmentID + transmissionvideo.EquipmentName}删除--失败");
            gridView1.EndDataUpdate();//结束数据的编辑
            gridView1.EndUpdate();   //结束视图的编辑
        }

        /// <summary>
        /// 批量添加标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rownumber = gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return;
            List<VideoPlay> videos = new List<VideoPlay>();
            foreach (var it in rownumber)
            {
                if (it < 0) continue;
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            BatchSolution batch = new BatchSolution(videos);
            batch.MyRefreshEvent += new BatchSolution.MyDeletgate(RefreshImage);
            batch.Show();
            Program.log.Info($"video_ids:{string.Join(",", videos.Select(t => t.Id).ToList())}添加批量标签");
        }

        /// <summary>
        ///  清除解帧信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rownumber = gridView1.GetSelectedRows();
            if (rownumber.Count() == 0) return;
            List<VideoPlay> videos = new List<VideoPlay>();
            if (MessageBox.Show("是否删除解帧文件夹？", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            foreach (var it in rownumber)
            {
                if(it<0) continue;
                VideoPlay video = (VideoPlay)gridView1.GetRow(it);
                videos.Add(video);
            }
            List<int> ids = videos.Select(t => t.Id).ToList();
            if (!SolutionData.DeleteSolution(ids))
            {
                MessageBox.Show("清除解帧信息失败");
                Program.log.Error($"{string.Join(",", ids)}从设备删除视频失败");
                return;
            }
            RefreshImage();
            Program.log.Info($"{string.Join(",", videos.Select(t => t.Id).ToList())}从设备删除视频失败");
        }

        /// <summary>
        /// 添加视频到通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> ids = GetCheckList().Select(t => t.Id).ToList();
            if (ids.Count == 0) return;
            selectEquipment.PostIds = ids;
            selectEquipment.Show();
            Program.log.Info($"viedo_id:{string.Join(",",ids)}添加通道");
        }

        private void ADStaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = gridView1.FocusedRowHandle;
            int row = gridView1.GetDataRowHandleByGroupRowHandle(index);
            VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
            int id = dr.EquipmentID;
            if (id == 0) return;
            SelectLabel select = new SelectLabel(RefreshType.StaticLabel, mydic[id].LabelStr);
            select.MyRefreshEvent += new SelectLabel.MyDelegate(SetLabelsToEquipment);
            select.ShowDialog();
            Program.log.Info($"显示静态标签,video_id:{id}");
        }

        public void SetLabelsToEquipment(List<string> labels)
        {
            int index = gridView1.FocusedRowHandle;
            int row = gridView1.GetDataRowHandleByGroupRowHandle(index);
            VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
            int id = dr.EquipmentID;
            if (id == 0) return;
            List<int> ids = new MyLabels().GetSelectIds(labels);
            if (LabelData.AddLabelToEquipment(id, ids))
            {
                MessageBox.Show("添加标签到通道失败");
                Program.log.Error($"通道ID:{id},添加标签:{string.Join(",", labels)}--失败");
                return;
            }
            Program.log.Error($"通道ID:{id},添加标签:{string.Join(",", labels)}");
            RefreshImage();
        }

        private void ShowStaticLabels()
        {
            int index = gridView1.FocusedRowHandle;
            int row = gridView1.GetDataRowHandleByGroupRowHandle(index);
            VideoPlay dr = (VideoPlay)gridView1.GetRow(row);
            int id = dr.EquipmentID;
            ShowStaticLabel show = new ShowStaticLabel(id);
            show.Show();
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transmissionvideo == null) return;
            Methods.OpenFolderAndSelectFile(AppSettings.ReturnStringUrl(Methods.ConversionString(transmissionvideo.Uri)));
            Program.log.Info($"定位文件夹,VideoId:{transmissionvideo.Id}");
        }

        private void OUTJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteJson(GetCheckList());
        }

        private void OUTExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridToExcel1(gridControl1);
        }

        private void SetInfo()
        {

        }

        private List<int> GetEquipmenIds(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return new List<int>();
                List<int> ids = new List<int>();
                if (text.Split(':').Length == 2)
                {
                    ids.Add(int.Parse(text.Split(':').First()));
                    return ids;
                }
                List<string> name = text.Split('-').ToList();
                MyGroup showgroups;
                switch (name.Count)
                {
                    case 1:
                        showgroups= GroupData.GetGroupShows(name[0]);                       
                        break;
                    case 2:
                        showgroups= GroupData.GetGroupShows(name[0],name[1]);
                        break;
                    case 3:
                        showgroups= GroupData.GetGroupShows(name[0],name[1],name[2]);
                        break;
                    case 4:
                        showgroups= GroupData.GetGroupShows(name[0],name[1],name[2],name[3]);
                        break;
                    default:
                        return new List<int>();
                }
                ids = showgroups.Equipments.Select(t => t.Id).ToList();
                return ids;
            }
            catch (Exception)
            {
                MessageBox.Show("输入的设备编号有问题");
                return new List<int>();
            }

        }

        private void SetComboxBox(System.Windows.Forms.ComboBox com, List<string> listOnit, List<string> listNew)
        {
            string text = com.Text;
            List<string> first = listOnit.Where(t => t.Contains(com.Text)).ToList();
            if (first.Count == 0)
            {
                return;
            }
            try
            {
                com.Items.Clear();
                listNew.Clear();
                foreach (var item in listOnit)
                {
                    if (item.Contains(com.Text))
                    {
                        listNew.Add(item);
                    }
                }
                com.Items.AddRange(listNew.ToArray());
                com.SelectionStart = com.Text.Length;
                Cursor = Cursors.Default;
                com.DroppedDown = true;

            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                com.Text = text;
                com.Select(com.Text.Length, 0);
            }
        }

        private void comboBox_groups_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_groups,listOnitGroups,listNewGrouops);
        }

        private void comboBox_group_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_group, listOnitGroup, listNewGrouop);
        }

        private void comboBox_proname_TextUpdate(object sender, EventArgs e)
        {
            SetComboxBox(comboBox_proname, listOnitProName, listNewProName);
        }

    }
}