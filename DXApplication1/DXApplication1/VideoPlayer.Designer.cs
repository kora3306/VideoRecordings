namespace DXApplication1
{
    partial class VideoPlayers
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayers));
            this.axPlayer1 = new AxAPlayer3Lib.AxPlayer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelpro = new System.Windows.Forms.Panel();
            this.colorSlider2 = new MB.Controls.ColorSlider();
            this.panelbottom = new System.Windows.Forms.Panel();
            this.colorSlidersound = new MB.Controls.ColorSlider();
            this.picsound = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_play_pause = new System.Windows.Forms.PictureBox();
            this.paneltop = new System.Windows.Forms.Panel();
            this.lbltitle = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playpause = new System.Windows.Forms.ToolStripMenuItem();
            this.停止toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.快进 = new System.Windows.Forms.ToolStripMenuItem();
            this.后退 = new System.Windows.Forms.ToolStripMenuItem();
            this.显示字母ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在最前toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.播放时在最前toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axPlayer1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelpro.SuspendLayout();
            this.panelbottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picsound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_play_pause)).BeginInit();
            this.paneltop.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axPlayer1
            // 
            this.axPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPlayer1.Enabled = true;
            this.axPlayer1.Location = new System.Drawing.Point(3, 40);
            this.axPlayer1.Name = "axPlayer1";
            this.axPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPlayer1.OcxState")));
            this.axPlayer1.Size = new System.Drawing.Size(926, 513);
            this.axPlayer1.TabIndex = 0;
            this.axPlayer1.OnMessage += new AxAPlayer3Lib._IPlayerEvents_OnMessageEventHandler(this.axPlayer1_OnMessage);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.05263F));
            this.tableLayoutPanel1.Controls.Add(this.panelpro, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.axPlayer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelbottom, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.paneltop, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.768559F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.23144F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(932, 621);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // panelpro
            // 
            this.panelpro.Controls.Add(this.colorSlider2);
            this.panelpro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelpro.Location = new System.Drawing.Point(3, 559);
            this.panelpro.Name = "panelpro";
            this.panelpro.Size = new System.Drawing.Size(926, 6);
            this.panelpro.TabIndex = 18;
            // 
            // colorSlider2
            // 
            this.colorSlider2.BackColor = System.Drawing.Color.Transparent;
            this.colorSlider2.BarInnerColor = System.Drawing.Color.DarkRed;
            this.colorSlider2.BarOuterColor = System.Drawing.Color.Red;
            this.colorSlider2.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlider2.ColorSchema = MB.Controls.ColorSlider.ColorSchemas.PerlRedCoral;
            this.colorSlider2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorSlider2.ElapsedInnerColor = System.Drawing.Color.Blue;
            this.colorSlider2.ElapsedOuterColor = System.Drawing.Color.Blue;
            this.colorSlider2.LargeChange = ((uint)(5u));
            this.colorSlider2.Location = new System.Drawing.Point(0, 0);
            this.colorSlider2.Name = "colorSlider2";
            this.colorSlider2.Size = new System.Drawing.Size(926, 6);
            this.colorSlider2.SmallChange = ((uint)(1u));
            this.colorSlider2.TabIndex = 52;
            this.colorSlider2.TabStop = false;
            this.colorSlider2.Text = "colorSlider2";
            this.colorSlider2.ThumbRoundRectSize = new System.Drawing.Size(10, 10);
            this.colorSlider2.Value = 0;
            this.colorSlider2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.colorSlider2_Scroll);
            this.colorSlider2.MouseHover += new System.EventHandler(this.colorSlider2_MouseHover);
            // 
            // panelbottom
            // 
            this.panelbottom.BackColor = System.Drawing.Color.Transparent;
            this.panelbottom.BackgroundImage = global::DXApplication1.Properties.Resources.bottom;
            this.panelbottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelbottom.Controls.Add(this.colorSlidersound);
            this.panelbottom.Controls.Add(this.picsound);
            this.panelbottom.Controls.Add(this.label3);
            this.panelbottom.Controls.Add(this.label2);
            this.panelbottom.Controls.Add(this.label5);
            this.panelbottom.Controls.Add(this.label1);
            this.panelbottom.Controls.Add(this.pic_play_pause);
            this.panelbottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbottom.Location = new System.Drawing.Point(3, 571);
            this.panelbottom.Name = "panelbottom";
            this.panelbottom.Size = new System.Drawing.Size(926, 47);
            this.panelbottom.TabIndex = 17;
            // 
            // colorSlidersound
            // 
            this.colorSlidersound.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.colorSlidersound.BackColor = System.Drawing.Color.Transparent;
            this.colorSlidersound.BarInnerColor = System.Drawing.Color.DodgerBlue;
            this.colorSlidersound.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlidersound.ElapsedInnerColor = System.Drawing.SystemColors.HotTrack;
            this.colorSlidersound.ElapsedOuterColor = System.Drawing.Color.DarkTurquoise;
            this.colorSlidersound.LargeChange = ((uint)(5u));
            this.colorSlidersound.Location = new System.Drawing.Point(790, 11);
            this.colorSlidersound.Name = "colorSlidersound";
            this.colorSlidersound.Size = new System.Drawing.Size(122, 20);
            this.colorSlidersound.SmallChange = ((uint)(1u));
            this.colorSlidersound.TabIndex = 72;
            this.colorSlidersound.TabStop = false;
            this.colorSlidersound.Text = "colorSlider1";
            this.colorSlidersound.ThumbOuterColor = System.Drawing.Color.Transparent;
            this.colorSlidersound.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlidersound.ThumbSize = 7;
            this.colorSlidersound.Scroll += new System.Windows.Forms.ScrollEventHandler(this.colorSlider1_Scroll);
            // 
            // picsound
            // 
            this.picsound.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.picsound.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picsound.ErrorImage")));
            this.picsound.Image = ((System.Drawing.Image)(resources.GetObject("picsound.Image")));
            this.picsound.Location = new System.Drawing.Point(747, 9);
            this.picsound.Name = "picsound";
            this.picsound.Size = new System.Drawing.Size(23, 26);
            this.picsound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picsound.TabIndex = 71;
            this.picsound.TabStop = false;
            this.picsound.Click += new System.EventHandler(this.picsound_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(133, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.label3.Size = new System.Drawing.Size(53, 25);
            this.label3.TabIndex = 68;
            this.label3.Text = "准备就绪";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(72, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.label2.Size = new System.Drawing.Size(61, 47);
            this.label2.TabIndex = 66;
            this.label2.Text = "000:00:00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(61, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.label5.Size = new System.Drawing.Size(11, 25);
            this.label5.TabIndex = 67;
            this.label5.Text = "/";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.label1.Size = new System.Drawing.Size(61, 47);
            this.label1.TabIndex = 63;
            this.label1.Text = "00:00:000";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pic_play_pause
            // 
            this.pic_play_pause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_play_pause.BackColor = System.Drawing.Color.Transparent;
            this.pic_play_pause.ErrorImage = global::DXApplication1.Properties.Resources.play;
            this.pic_play_pause.Image = global::DXApplication1.Properties.Resources.strat;
            this.pic_play_pause.InitialImage = global::DXApplication1.Properties.Resources.play;
            this.pic_play_pause.Location = new System.Drawing.Point(432, 6);
            this.pic_play_pause.Name = "pic_play_pause";
            this.pic_play_pause.Size = new System.Drawing.Size(45, 32);
            this.pic_play_pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_play_pause.TabIndex = 15;
            this.pic_play_pause.TabStop = false;
            this.pic_play_pause.Click += new System.EventHandler(this.pic_play_pause_Click);
            this.pic_play_pause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pic_MouseDown);
            this.pic_play_pause.MouseEnter += new System.EventHandler(this.Pic_MouseEnter);
            this.pic_play_pause.MouseLeave += new System.EventHandler(this.Pic_MouseLeave);
            // 
            // paneltop
            // 
            this.paneltop.BackColor = System.Drawing.Color.Transparent;
            this.paneltop.BackgroundImage = global::DXApplication1.Properties.Resources.top;
            this.paneltop.Controls.Add(this.lbltitle);
            this.paneltop.Controls.Add(this.label4);
            this.paneltop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneltop.Location = new System.Drawing.Point(3, 3);
            this.paneltop.Name = "paneltop";
            this.paneltop.Size = new System.Drawing.Size(926, 31);
            this.paneltop.TabIndex = 18;
            // 
            // lbltitle
            // 
            this.lbltitle.BackColor = System.Drawing.Color.Transparent;
            this.lbltitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbltitle.ForeColor = System.Drawing.Color.White;
            this.lbltitle.Location = new System.Drawing.Point(5, 0);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(744, 31);
            this.lbltitle.TabIndex = 24;
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbltitle.Click += new System.EventHandler(this.lbltitle_Click);
            this.lbltitle.DoubleClick += new System.EventHandler(this.lbltitle_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(5, 18);
            this.label4.TabIndex = 25;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playpause,
            this.停止toolStripMenuItem2,
            this.快进,
            this.后退,
            this.显示字母ToolStripMenuItem,
            this.在最前toolStripMenuItem3,
            this.截图ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 158);
            // 
            // playpause
            // 
            this.playpause.Name = "playpause";
            this.playpause.Size = new System.Drawing.Size(124, 22);
            this.playpause.Text = "播放";
            this.playpause.Click += new System.EventHandler(this.playpause_Click);
            // 
            // 停止toolStripMenuItem2
            // 
            this.停止toolStripMenuItem2.Name = "停止toolStripMenuItem2";
            this.停止toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.停止toolStripMenuItem2.Text = "停止";
            this.停止toolStripMenuItem2.Click += new System.EventHandler(this.停止toolStripMenuItem2_Click);
            // 
            // 快进
            // 
            this.快进.Name = "快进";
            this.快进.Size = new System.Drawing.Size(124, 22);
            this.快进.Text = "快进";
            // 
            // 后退
            // 
            this.后退.Name = "后退";
            this.后退.Size = new System.Drawing.Size(124, 22);
            this.后退.Text = "后退";
            // 
            // 显示字母ToolStripMenuItem
            // 
            this.显示字母ToolStripMenuItem.Name = "显示字母ToolStripMenuItem";
            this.显示字母ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.显示字母ToolStripMenuItem.Text = "显示字幕";
            this.显示字母ToolStripMenuItem.Click += new System.EventHandler(this.显示字母ToolStripMenuItem_Click);
            // 
            // 在最前toolStripMenuItem3
            // 
            this.在最前toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.播放时在最前toolStripMenuItem3,
            this.toolStripMenuItem2});
            this.在最前toolStripMenuItem3.Name = "在最前toolStripMenuItem3";
            this.在最前toolStripMenuItem3.Size = new System.Drawing.Size(124, 22);
            this.在最前toolStripMenuItem3.Text = "在最前";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Checked = true;
            this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "总是在最前";
            // 
            // 播放时在最前toolStripMenuItem3
            // 
            this.播放时在最前toolStripMenuItem3.Name = "播放时在最前toolStripMenuItem3";
            this.播放时在最前toolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.播放时在最前toolStripMenuItem3.Text = "播放时在最前";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem2.Text = "取消最前";
            // 
            // 截图ToolStripMenuItem
            // 
            this.截图ToolStripMenuItem.Name = "截图ToolStripMenuItem";
            this.截图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.截图ToolStripMenuItem.Text = "截图";
            this.截图ToolStripMenuItem.Click += new System.EventHandler(this.截图ToolStripMenuItem_Click);
            // 
            // VideoPlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VideoPlayers";
            this.Size = new System.Drawing.Size(932, 621);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.VideoPlayer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.axPlayer1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelpro.ResumeLayout(false);
            this.panelbottom.ResumeLayout(false);
            this.panelbottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picsound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_play_pause)).EndInit();
            this.paneltop.ResumeLayout(false);
            this.paneltop.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxAPlayer3Lib.AxPlayer axPlayer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pic_play_pause;
        private System.Windows.Forms.Panel panelbottom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private MB.Controls.ColorSlider colorSlider2;
        private MB.Controls.ColorSlider colorSlidersound;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playpause;
        private System.Windows.Forms.ToolStripMenuItem 停止toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 快进;
        private System.Windows.Forms.ToolStripMenuItem 后退;
        private System.Windows.Forms.ToolStripMenuItem 显示字母ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在最前toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 播放时在最前toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 截图ToolStripMenuItem;
        private System.Windows.Forms.Panel panelpro;
        private System.Windows.Forms.Panel paneltop;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picsound;
    }
}
