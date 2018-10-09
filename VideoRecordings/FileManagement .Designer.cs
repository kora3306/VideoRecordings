namespace VideoRecordings
{
    partial class FileManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.video_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryVIdeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repetitionVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Frame_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._imagetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlExHfrz = new MyControl.TabControlEx();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.video_toolStripMenuItem,
            this._imagetoolStripMenuItem,
            this.userToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1048, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // video_toolStripMenuItem
            // 
            this.video_toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VideoToolStripMenuItem,
            this.queryVIdeoToolStripMenuItem,
            this.LabelToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.repetitionVideoToolStripMenuItem,
            this.Frame_ToolStripMenuItem});
            this.video_toolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.video_toolStripMenuItem.Name = "video_toolStripMenuItem";
            this.video_toolStripMenuItem.Size = new System.Drawing.Size(105, 25);
            this.video_toolStripMenuItem.Text = "视频文件管理";
            // 
            // VideoToolStripMenuItem
            // 
            this.VideoToolStripMenuItem.Name = "VideoToolStripMenuItem";
            this.VideoToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.VideoToolStripMenuItem.Tag = "VideoRecordings.VideoInformation";
            this.VideoToolStripMenuItem.Text = "视频管理";
            this.VideoToolStripMenuItem.Click += new System.EventHandler(this.VideoToolStripMenuItem_Click);
            // 
            // queryVIdeoToolStripMenuItem
            // 
            this.queryVIdeoToolStripMenuItem.Name = "queryVIdeoToolStripMenuItem";
            this.queryVIdeoToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.queryVIdeoToolStripMenuItem.Tag = "VideoRecordings.QueryVideo";
            this.queryVIdeoToolStripMenuItem.Text = "查询视频";
            this.queryVIdeoToolStripMenuItem.Click += new System.EventHandler(this.queryVIdeoToolStripMenuItem_Click);
            // 
            // LabelToolStripMenuItem
            // 
            this.LabelToolStripMenuItem.Name = "LabelToolStripMenuItem";
            this.LabelToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.LabelToolStripMenuItem.Tag = "VideoRecordings.ManagementLabel";
            this.LabelToolStripMenuItem.Text = "标签管理";
            this.LabelToolStripMenuItem.Click += new System.EventHandler(this.LabelToolStripMenuItem_Click);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.groupToolStripMenuItem.Tag = "VideoRecordings.ChannelGrouping.Grouping";
            this.groupToolStripMenuItem.Text = "通道分组";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // repetitionVideoToolStripMenuItem
            // 
            this.repetitionVideoToolStripMenuItem.Name = "repetitionVideoToolStripMenuItem";
            this.repetitionVideoToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.repetitionVideoToolStripMenuItem.Tag = "VideoRecordings.Video.RepetitionVideo";
            this.repetitionVideoToolStripMenuItem.Text = "视频查重";
            this.repetitionVideoToolStripMenuItem.Click += new System.EventHandler(this.repetitionVideoToolStripMenuItem_Click);
            // 
            // Frame_ToolStripMenuItem
            // 
            this.Frame_ToolStripMenuItem.Name = "Frame_ToolStripMenuItem";
            this.Frame_ToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.Frame_ToolStripMenuItem.Tag = "VideoRecordings.Video.Frame";
            this.Frame_ToolStripMenuItem.Text = "解帧队列";
            this.Frame_ToolStripMenuItem.Click += new System.EventHandler(this.Frame_ToolStripMenuItem_Click);
            // 
            // _imagetoolStripMenuItem
            // 
            this._imagetoolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._imagetoolStripMenuItem.Name = "_imagetoolStripMenuItem";
            this._imagetoolStripMenuItem.Size = new System.Drawing.Size(105, 25);
            this._imagetoolStripMenuItem.Text = "图片文件管理";
            this._imagetoolStripMenuItem.Visible = false;
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.userToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.userToolStripMenuItem.Text = "用户名";
            // 
            // tabControlExHfrz
            // 
            this.tabControlExHfrz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExHfrz.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlExHfrz.ItemSize = new System.Drawing.Size(66, 28);
            this.tabControlExHfrz.Location = new System.Drawing.Point(0, 28);
            this.tabControlExHfrz.Name = "tabControlExHfrz";
            this.tabControlExHfrz.Padding = new System.Drawing.Point(15, 3);
            this.tabControlExHfrz.SelectedIndex = 0;
            this.tabControlExHfrz.Size = new System.Drawing.Size(1048, 686);
            this.tabControlExHfrz.TabIndex = 2;
            // 
            // FileManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 714);
            this.Controls.Add(this.tabControlExHfrz);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FileManagement";
            this.Text = "FileManagement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileManagement_FormClosing);
            this.Load += new System.EventHandler(this.FileManagement_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem video_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _imagetoolStripMenuItem;
        public MyControl.TabControlEx tabControlExHfrz;
        private System.Windows.Forms.ToolStripMenuItem queryVIdeoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repetitionVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Frame_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
    }
}