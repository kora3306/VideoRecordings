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

namespace VideoRecordings
{
    public partial class QueryVideo : DevExpress.XtraEditors.XtraForm
    {
        public VideoPlay transmissionvideo = new VideoPlay();     //选中的文件
        public List<VideoPlay> videoplays = new List<VideoPlay>();  //所有筛选的文件
        public List<string> imageurl = new List<string>();       //选中文件的图片的url
        bool isFirst = true;  //每次查询选择定位选中文件0
        VideoInformation information;
        public QueryVideo(VideoInformation video)
        {   
            InitializeComponent();
            information = video;
            Program.IsQuery = true;
            timeEdit_start.Time = DateTime.MinValue;
            timeEdit_end.Time = DateTime.MaxValue;
            comboBox_jl.SelectedIndex = 1;
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
                if (textBox_name.Text.Trim() != string.Empty)
                {
                    getjson += $"name={textBox_name.Text.Trim()}&";
                }
                if (comboBox_stu.Text != string.Empty)
                {
                    getjson += $"status={comboBox_stu.SelectedIndex}&";
                }
                if (textBox_fid.Text.Trim() != string.Empty)
                {
                    getjson += $"project_name={textBox_fid.Text.Trim()}&";
                }
                if (comboBox_jl.Text != string.Empty)
                {
                    getjson += $"recorded={comboBox_jl.SelectedIndex}&";
                }
                if (comboBox_jz.Text != string.Empty)
                {
                    getjson += $"deframed={comboBox_jz.SelectedIndex}&";
                }
                if (textBox_vid.Text.Trim() != string.Empty)
                {
                    getjson += $"id={textBox_vid.Text.Trim()}&";
                }
                if (textBox_label.Text.Trim() != string.Empty)
                {
                    getjson += $"label[]={Conversion(textBox_label.Text.Trim())}&";
                }
                if (!getjson.EndsWith("&"))
                {
                    getjson += "&";
                }
                getjson += $"start_time={timeEdit_start.Text}&end_time={timeEdit_end.Text}";
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
            JsonObject obj = WebClinetHepler.GetJson(url, na);
            if (obj == null) return;
            JsonObject data = obj["videos"];
            for (int i = 0; i < data.Length; i++)
            {
                string json = (new JavaScriptSerializer()).Serialize((Dictionary<string, object>)(object)data[i].Value);
                VideoPlay video = JsonHelper.DeserializeDataContractJson<VideoPlay>(json);
                videoplays.Add(video);
            }
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
                    button1.PerformClick();
                    break;
                case Keys.D:
                    DelImage();
                    return true;
                case Keys.Up:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == 0 ? 0 : gridView1.FocusedRowHandle - 1;
                    if (gridView1.FocusedRowHandle<0)
                    {
                        return true;
                    }
                    transmissionvideo = videoplays[gridView1.FocusedRowHandle];
                    GetIntToString();
                    return true;
                case Keys.Down:
                    gridView1.FocusedRowHandle = gridView1.FocusedRowHandle == gridView1.RowCount - 1 ? gridView1.RowCount - 1 : gridView1.FocusedRowHandle + 1;
                    if (gridView1.FocusedRowHandle < 0)
                    {
                        return true;
                    }
                    transmissionvideo = videoplays[gridView1.FocusedRowHandle];
                    GetIntToString();
                    return true;
                case Keys.Escape:
                    information.Show();
                    this.Close();
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
            transmissionvideo = videoplays[rowIndex];
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
            if (transmissionvideo==null||transmissionvideo.ImageId == null)
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
            imageListView1.ThumbnailSize = new Size(260, 260);
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
            GetIntToString();
        }

        /// <summary>
        /// 双击播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
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
                new VideoRecording(transmissionvideo, new InformationDisplay(null, null), this).ShowDialog();
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("指定目录没有找到该视频,是否继续标注", "提示", MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (MsgBoxResult == DialogResult.Yes)
                {
                    new VideoRecording(transmissionvideo, new InformationDisplay(null, null), this).ShowDialog();
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
            Program.IsQuery = false;
        }

        /// <summary>
        /// 双击放大图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageListView1_DoubleClick(object sender, EventArgs e)
        {
            if (imageListView1.SelectedItems.Count == 0)
            {
                return;
            }
            List<ImageListViewItem> showimages = new List<ImageListViewItem>();
            foreach (var item in imageListView1.SelectedItems)
            {
                showimages.Add(item);
            }
            new ShowImage(showimages).ShowDialog();
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        public void DelImage()
        {
            string url = Program.Urlpath + "/video/snapshot/";
            if (imageListView1.SelectedItems.Count == 0)
            {
                return;
            }
            foreach (var item in imageListView1.SelectedItems)
            {
                JsonObject obj = WebClinetHepler.Delete(url + item.Text.Split('/').Last());
                if (obj == null)
                {
                    MessageBox.Show(url + item.Text.Split('/').Last() + "删除失败");
                    return;
                }
                imageListView1.Items.Remove(item);
                imageurl.Remove(url + item.Text.Split('/').Last());
            }
            RefreshImage();
            gridView1.RefreshData();
            Program.log.Error($"删除图片{url}");
        }

        /// <summary>
        /// 删除图片按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelImage();
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
            VideoPlay video = videoplays[e.RowHandle];
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

    }
}