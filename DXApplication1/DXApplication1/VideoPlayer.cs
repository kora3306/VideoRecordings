using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxAPlayer3Lib;
using VideoPlayer;
using System.IO;
using DXApplication1.Properties;

namespace DXApplication1
{
    public partial class VideoPlayers_test : UserControl
    {

        ToolTip tip = new ToolTip();
        PlayClass _play = new PlayClass();
        AxPlayer _Player;
        public Point point = new Point();
        public string URL = string.Empty;
        public string path = "";
        public VideoPlayers_test()
        {
            InitializeComponent();
            tableLayoutPanel1.BackColor = Color.Black;
           
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            _Player = axPlayer1;
            axPlayer1.OnBuffer += new _IPlayerEvents_OnBufferEventHandler(axPlayer1_OnBuffer);
            axPlayer1.OnStateChanged += new _IPlayerEvents_OnStateChangedEventHandler(axPlayer1_OnStateChanged);
            axPlayer1.OnSeekCompleted += new _IPlayerEvents_OnSeekCompletedEventHandler(axPlayer1_OnSeekCompleted);
            axPlayer1.OnOpenSucceeded += new EventHandler(axPlayer1_OnOpenSucceeded);
            axPlayer1.OnDownloadCodec += new _IPlayerEvents_OnDownloadCodecEventHandler(axPlayer1_OnDownloadCodec);
            axPlayer1.SetCustomLogo(Properties.Resources.logo.GetHbitmap().ToInt32());  //自定义logo
            axPlayer1.SetVolume(50);
            this.Resize += new EventHandler(FormResize);
            pic_play_pause.SizeMode = PictureBoxSizeMode.Zoom;
        }
        #region  axPlayer事件处理程序

        /// <summary>
        /// axPlayer的鼠标、键盘事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void axPlayer1_OnMessage(object sender, _IPlayerEvents_OnMessageEvent e)
        {
            //throw new NotImplementedException();
            switch (e.nMessage)
            {
                case conf.WM_RBUTTONDOWN:
                    int tempstatus = axPlayer1.GetState();

                    if (axPlayer1.GetState() == 5)
                    {
                        contextMenuStrip1.Items["playpause"].Text = "暂停";
                    }
                    else
                    {
                        contextMenuStrip1.Items["playpause"].Text = "播放";
                    }
                    contextMenuStrip1.Show(axPlayer1, axPlayer1.PointToClient(Cursor.Position));
                    break;
                default: break;
            }
        }
        /// <summary>
        /// 格式不支持，需要下载解码器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void axPlayer1_OnDownloadCodec(object sender, _IPlayerEvents_OnDownloadCodecEvent e)
        {
            MessageBox.Show("需要解码器:" + e.strCodecPath);
            if (!Directory.Exists(@"codecs"))
            {
                return;
            }
            else
            {
                CopyDirectory("codecs", StrToPath(e.strCodecPath));
            }
            MyPlay?.Invoke();
        }
        /// <summary>
        /// 文件打开完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void axPlayer1_OnOpenSucceeded(object sender, EventArgs e)
        {
            label1.Text = "00:00:00";
            label2.Text = TimeToString(TimeSpan.FromMilliseconds(axPlayer1.GetDuration()));
            colorSlider2.Enabled = true;
            colorSlider2.Maximum = axPlayer1.GetDuration()/1000;
            timer1.Start();
        }
        /// <summary>
        /// 跳转指定位置完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void axPlayer1_OnSeekCompleted(object sender, _IPlayerEvents_OnSeekCompletedEvent e)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 播放器状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void axPlayer1_OnStateChanged(object sender, _IPlayerEvents_OnStateChangedEvent e)
        {
            if (e.nNewState == 0)  //就绪
            {
                //初始化
            }
            switch (e.nNewState)
            {
                case (int)PlayClass.PLAY_STATE.PS_READY:
                    colorSlider2.Maximum = 100;
                    colorSlider2.Value = 0;
                    colorSlider2.Enabled = false;
                    label1.Text = "00:00:00";
                    label2.Text = "00:00:00";
                    label3.Text = "准备就绪";
                    timer1.Stop();
                    break;
                case (int)PlayClass.PLAY_STATE.PS_OPENING: label3.Text = "正在打开"; break;
                case (int)PlayClass.PLAY_STATE.PS_PLAY:
                case (int)PlayClass.PLAY_STATE.PS_PLAYING:
                    label3.Text = "正在播放";
                    pic_play_pause.ErrorImage =Resources.stop1;
                    ChangErrPic(pic_play_pause); break;
                case (int)PlayClass.PLAY_STATE.PS_PAUSED:
                case (int)PlayClass.PLAY_STATE.PS_PAUSING:
                    label3.Text = "暂停播放";
                    pic_play_pause.ErrorImage = Resources.strat;
                    ChangErrPic(pic_play_pause); break;
                case (int)PlayClass.PLAY_STATE.PS_CLOSING:
                    pic_play_pause.ErrorImage = Resources.stop1;
                    ChangErrPic(pic_play_pause);
                    break;
                default:
                    break;
            }

            Console.WriteLine("播放器状态:" + e.nNewState);
        }
        /// <summary>
        /// 缓冲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void axPlayer1_OnBuffer(object sender, _IPlayerEvents_OnBufferEvent e)
        {
            if (e.nPercent != 100)
            {
                label3.Text = "正在缓冲...(" + e.nPercent + "%)";
            }
            else
            {
                label3.Text = "正在播放"; ;
            }
        }

        #endregion

        /// <summary>
        /// 打开本地文件
        /// </summary>
        private void openfile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "所有文件|*.*|mp4|*.mp4|avi|*.avi|rm|*.rm|rmvb|*.rmvb|flv|*.flv|xr|*.xr";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    axPlayer1.Open(ofd.FileName);
                }
            }

        }

        public void VideoPalying()
        {
            if (URL != string.Empty)
            {
                axPlayer1.Open(URL);
            }
        }

        public void PlayOrPause()
        {

            int tempstatus = _Player.GetState();
            if (tempstatus == 5 || tempstatus == 3)
            {
                if (axPlayer1.GetState() == 5)  //播放-暂停
                {
                    axPlayer1.Pause();
                }
                else
                {
                    axPlayer1.Play();
                }
            }
            if (tempstatus==0)
            {
                VideoPalying();
            }
        }

        private void Stop()
        {
            axPlayer1.Close();
        }

        /// <summary>
        /// 定时更新进度条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = TimeToString(TimeSpan.FromMilliseconds(axPlayer1.GetPosition()));
            colorSlider2.Value = axPlayer1.GetPosition() <= 0 ? 0 : axPlayer1.GetPosition()/1000;
            if (axPlayer1.GetDuration()-1==axPlayer1.GetPosition())
            {
                VideoPalying();
            }

        }

        /// <summary>
        /// 时间格式转换
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        string TimeToString(TimeSpan span)
        {
            return span.Hours.ToString("00") + ":" +
            span.Minutes.ToString("00") + ":" +
            span.Seconds.ToString("00");
        }


        /// <summary>
        /// 字幕   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 显示字母ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text == "显示字幕")
            {
                (sender as ToolStripMenuItem).Text = "隐藏字幕";
                axPlayer1.SetConfig(504, "1");
            }
            else
            {
                (sender as ToolStripMenuItem).Text = "显示字幕";
                axPlayer1.SetConfig(504, "0");
            }
        }
        
        private void playpause_Click(object sender, EventArgs e)
        {

            PlayOrPause();

        }

        private void 停止toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Stop();
        }
    

        private void pic_play_pause_Click(object sender, EventArgs e)
        {
            PlayOrPause();
        }

        private void Pic_MouseEnter(object sender, EventArgs e)
        {
            ChangePic((PictureBox)sender, PlayClass.MouseStaue.enter);
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            ChangePic((PictureBox)sender, PlayClass.MouseStaue.enter);
        }

        private void Pic_MouseLeave(object sender, EventArgs e)
        {
            ChangePic((PictureBox)sender, PlayClass.MouseStaue.level);
        }

        private void Pic_MouseDown(object sender, MouseEventArgs e)
        {

        }

        //
        void ChangErrPic(PictureBox pic)
        {
            Rectangle rectangle = pic.RectangleToClient(this.ClientRectangle);
            if (rectangle.Contains(MousePosition))
            {
                ChangePic(pic, PlayClass.MouseStaue.enter);
            }
            else
            {
                ChangePic(pic, PlayClass.MouseStaue.normal);
            }
        }

        void ChangePic(PictureBox pic, PlayClass.MouseStaue status)
        {
            pic.Image = pic.ErrorImage;
        }

        private void VideoPlayer_Resize(object sender, EventArgs e)
        {
            Rounding(7);
        }

        void FormResize(object sender, EventArgs e)
        {
            _Player.Width = this.Width;
            _Player.Height = this.Height - paneltop.Height - panelbottom.Height - panelpro.Height;
            //picsavapic.Left = picopen.Left - picsavapic.Width;
            //pic_play_pause.Left = (this.Width - pic_play_pause.Width) / 2;
            ////picstop.Left = pic_play_pause.Left - picstop.Width - 10;
            //picsound.Left = pic_play_pause.Left + pic_play_pause.Width + 10;
            //colorSlidersound.Left = picsound.Left + picsound.Width + 5;

        }

        public bool Rounding(int angle = 0)
        {
            int hRgn;
            if (angle == 0)
            {
                angle = 5;
            }

            hRgn = Win32.CreateRoundRectRgn(0, 0, this.Width, this.Height, angle, angle);
            if (hRgn == 0)
            {
                return false;
            }
            if (Win32.SetWindowRgn(this.Handle.ToInt32(), hRgn, true) == 0)
            {
                return false;
            }
            Win32.DeleteObject(hRgn);
            return true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            openfile();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            CaptureTheScreen();
        }

        private void SavePic()
        {
            axPlayer1.SetConfig(702, Application.StartupPath + "\\截图.bmp");
        }

        private void 截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Screenshots();
            point = new Point(point.X, point.Y - paneltop.Height);
        }

        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            axPlayer1.SetVolume(colorSlidersound.Value * 10);  //10倍
        }

        private void colorSlider2_MouseHover(object sender, EventArgs e)
        {
            tip.Show(TimeToString(TimeSpan.FromMilliseconds(colorSlider2.Value*1000)), colorSlider2, 2000);
        }

        private void colorSlider2_Scroll(object sender, ScrollEventArgs e)
        {
            axPlayer1.SetPosition(colorSlider2.Value*1000);
            label1.Text = TimeToString(TimeSpan.FromMilliseconds(colorSlider2.Value*1000));
        }

        private void picmax_Click(object sender, EventArgs e)
        {
            MaxOrMin();
        }

        void MaxOrMin()
        {
            if (this.Width != Screen.PrimaryScreen.WorkingArea.Width || this.Height != Screen.PrimaryScreen.WorkingArea.Height)
            {
                _play.position.Left = this.Left;
                _play.position.Top = this.Top;
                _play.position.Width = this.Width;
                _play.position.Height = this.Height;
                this.Left = this.Top = 0;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            }
            else
            {
                this.Left = _play.position.Left;
                this.Top = _play.position.Top;
                this.Width = _play.position.Width;
                this.Height = _play.position.Height;
            }
        }

        private void lbltitle_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine(lbltitle.Text);
        }

        private void lbltitle_Click(object sender, EventArgs e)
        {
            Console.WriteLine(lbltitle.Text);
        }

        private void paneltop_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine(lbltitle.Text);
        }

        public void Getimage()
        {
            string photoname = DateTime.Now.Ticks.ToString();
            if (path.Substring(path.Length - 1, 1) != @"\")
                path = path + @"\";
            axPlayer1.SetConfig(702, path + "\\" + photoname + ".jpg");
        }

        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public event MyDelegate MyPlay;

        public void CaptureTheScreen()
        {
            SetPoint();
            Bitmap bit = new Bitmap(axPlayer1.Width+10, axPlayer1.Height+25);
            Graphics g = Graphics.FromImage(bit);
            g.CopyFromScreen(point, new Point(0, 0), bit.Size);
            string photoname = DateTime.Now.Ticks.ToString();
            if (path.Substring(path.Length - 1, 1) != @"\")

                path = path + @"\";
            bit.Save(path + "\\" + photoname + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
        }

        public void Screenshots()
        {
            Getimage();
            SavePic();
            MyEvent?.Invoke();
        }

        private void SetPoint()
        {
            point = new Point(point.X, point.Y + paneltop.Height);
        }

        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string StrToPath(string path)
        {
            List<string> paths = path.Split('\\').ToList();
            paths.Remove(paths.Last());
            string str = string.Empty;
            foreach (var item in paths)
            {
                str += item + "\\";
            }
            return str.Substring(0,str.Length-1);
        }

        private void picsound_Click(object sender, EventArgs e)
        {

        }

        public void  SetTime(int time)
        {
            switch ((TimeType)time)
            {
                case TimeType.Start:
                    colorSlider2.Value = 1 ;
                    break;
                case TimeType.Append:
                    if (colorSlider2.Value > (axPlayer1.GetDuration() / 1000)-100)
                    {
                        colorSlider2.Value = (axPlayer1.GetDuration() / 1000) - 1;
                        break;
                    }
                    colorSlider2.Value += 100;
                    break;
                case TimeType.End:
                    colorSlider2.Value = (axPlayer1.GetDuration() / 1000)-1;
                    PlayOrPause();
                    break;
                default:
                    break;
            }
            axPlayer1.SetPosition(colorSlider2.Value * 1000);
            label1.Text = TimeToString(TimeSpan.FromMilliseconds(colorSlider2.Value * 1000));
        }

    }

    public enum TimeType
    {
        Start = 0,
        Append = 1,
        End = 2
    }
}

