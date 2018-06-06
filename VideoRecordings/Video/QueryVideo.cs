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

namespace VideoRecordings
{
    public partial class QueryVideo : DevExpress.XtraEditors.XtraForm
    {
        public VideoPlay transmissionvideo = new VideoPlay();     //选中的文件
        public List<VideoPlay> videoplays = new List<VideoPlay>();  //所有筛选的文件
        public List<string> imageurl = new List<string>();       //选中文件的图片的url
        bool isFirst = true;  //每次查询选择定位选中文件0
        List<VideoProject> Videos = new List<VideoProject>();
        VideoInformation information;
        GridHitInfo hInfo = new GridHitInfo();
        Dictionary<string, string> LabelsNumber = new Dictionary<string, string>();   // 标签对照
        Dictionary<string, string> LabelAll = new Dictionary<string, string>();
        List<TreeNode> Nodes = new List<TreeNode>();

        public QueryVideo(VideoInformation video, List<VideoProject> inform)
        {
            InitializeComponent();
            information = video;
            Videos = inform;
            timeEdit_start.Time = DateTime.MinValue;
            timeEdit_end.Time = DateTime.MaxValue;
        }

        private void QueryVideo_Load(object sender, EventArgs e)
        {
            Nodes = GetLabels();
            SetInformations();
            SetRadioButton();
            comboBox_proname.SelectedIndex = -1;
            comboBox_place.SelectedIndex = -1;
            comboBox_replicator.SelectedIndex = -1;
            dateTimePicker1.Value = Convert.ToDateTime("2010-1-1");
            dateTimePicker2.Value = DateTime.Now;
            List<string> text = LabelAll.Select(t => t.Value).ToList();
            textBox_label.AutoCompleteCustomSource.Clear();
            textBox_label.AutoCompleteCustomSource.AddRange(text.ToArray());
            textBox1.AutoCompleteCustomSource.Clear();
            textBox1.AutoCompleteCustomSource.AddRange(text.ToArray());
            label14.Text = $"欢迎:{Program.UserName}";
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
            List<string> replicators = RepeatList(Videos, "replicator");
            comboBox_replicator.DataSource = replicators;
            comboBox_replicator.AutoCompleteCustomSource.Clear();
            comboBox_replicator.AutoCompleteCustomSource.AddRange(replicators.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isFirst = true;
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
                    getjson += $"label[]=[{GetLabesString(textBox_label.Text)}]&";
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
            if (string.IsNullOrEmpty(json))
            {
                DialogResult dr = MessageBox.Show("没有筛选信息,确认显示所有文件?", "提示信息！",
             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }
            string url = Program.Urlpath + "/videos";
            GettListVideo(url, json);
            bindingSource1.DataSource = videoplays;
            bindingNavigator1.BindingSource = bindingSource1;
            if (isFirst)
            {
                transmissionvideo = videoplays.Count == 0 ? null : videoplays.First();
                isFirst = false;
            }
            bindingNavigatorCountItem.Text = $"/ {videoplays.Count}";
            RefreshFocus();
            gridView1.RefreshData();
            bindingNavigator1.Refresh();
            GetIntToString();
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
        public void GettListVideo(string url, string na)
        {
            videoplays.Clear();
            JObject obj = WebClinetHepler.GetJObject(url, na);
            if (obj == null) return;
            videoplays = JsonHelper.DeserializeDataContractJson<List<VideoPlay>>(obj["videos"].ToString());
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
                case Keys.Enter:
                    if (SetScreeningLabels())
                    {
                        return true;
                    }
                    button1.PerformClick();
                    break;
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
                    information.Show();
                    this.Close();
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
            foreach (var item in transmissionvideo.ImageId)
            {
                imageurl.Add(url + item);
            }
            SetTheListView();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        public void SetTheListView()
        {
            imageListView1.Items.Clear();
            List<ImageListViewItem> items = new List<ImageListViewItem>();
            imageListView1.ThumbnailSize = new Size(300, 150);
            foreach (string imageUrl in imageurl)
            {
                ImageListViewItem item = new ImageListViewItem(imageUrl) { Text = imageUrl.Split('/').Last() };
                items.Add(item);
            }
            imageListView1.Items.AddRange(items.ToArray());
        }

        /// <summary>
        /// 点击改变需要加载的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_Click(object sender, EventArgs e)
        {
            transmissionvideo = (VideoPlay)gridView1.GetRow(gridView1.FocusedRowHandle);
            GetIntToString();
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
            if (transmissionvideo.Uri == null) return;
            if (File.Exists(Program.ReturnStringUrl(ConversionString(transmissionvideo.Uri))))
            {
                new VideoRecording(transmissionvideo, true, new InformationDisplay(null, null), this).Show();
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("指定目录没有找到该视频,是否继续标注", "提示", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    new VideoRecording(transmissionvideo, true, new InformationDisplay(null, null), this).Show();
                }
                else
                {
                    return;
                }
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
        }

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
            information.GetInformationShow();
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

        public List<TreeNode> GetLabels()
        {
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null || obj["result"] == null || obj["result"].Count() == 0)
            {
                return null;
            }
            List<TreeNode> items = new List<TreeNode>();
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                TreeNode tree = new TreeNode
                {
                    Text = obj["result"][i]["name"].ToString(),
                    ForeColor = Color.Green
                };
                LabelsNumber.Add(obj["result"][i]["id"].ToString(), obj["result"][i]["name"].ToString());
                for (int j = 0; j < obj["result"][i]["children"].Count(); j++)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = obj["result"][i]["children"][j]["name"].ToString(),
                        Tag = obj["result"][i]["children"][j]["is_fre"].ToString(),
                        ForeColor = Color.Blue
                    };
                    LabelAll.Add(obj["result"][i]["children"][j]["id"].ToString(), obj["result"][i]["children"][j]["name"].ToString());
                    tree.Nodes.Add(node);
                }
                items.Add(tree);
            }
            return items;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LabelsNumber.Count == 0) return;
            button1.Focus();
            new SelectLabel(this, Nodes, LabelsNumber, textBox_label.Text).ShowDialog();
        }

        public void StartScreening(List<string> label)
        {
            textBox_label.Text = string.Join(",", label);
        }

        public string GetLabesString(string text)
        {
            List<string> labels = text.Split(',').ToList();
            string labeljson = string.Empty; ;
            foreach (var item in labels)
            {
                if (LabelAll.Values.Contains(item))
                {
                    string keys = LabelAll.Where(q => q.Value == item).Select(q => q.Key).First();
                    labeljson += keys + ",";
                }
            }
            if (labeljson.EndsWith(","))
            {
                labeljson = labeljson.Substring(0, labeljson.Length - 1);
            }
            return labeljson;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            int index = gridview.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            string uri = gridview.GetRowCellValue(index, "Uri").ToString();
            GridGroupRowInfo.GroupText = uri.Split('/').ToList()
                 .First(t => t.StartsWith("SP") || t.StartsWith("Sp"));
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
    }
}