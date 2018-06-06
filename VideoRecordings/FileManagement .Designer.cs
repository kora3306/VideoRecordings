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
            this._imagetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlExHfrz = new MyControl.TabControlEx();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.video_toolStripMenuItem,
            this._imagetoolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1048, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // video_toolStripMenuItem
            // 
            this.video_toolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.video_toolStripMenuItem.Name = "video_toolStripMenuItem";
            this.video_toolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.video_toolStripMenuItem.Text = "视频文件管理";
            this.video_toolStripMenuItem.Click += new System.EventHandler(this.video_toolStripMenuItem_Click);
            // 
            // _imagetoolStripMenuItem
            // 
            this._imagetoolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._imagetoolStripMenuItem.Name = "_imagetoolStripMenuItem";
            this._imagetoolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this._imagetoolStripMenuItem.Text = "图片文件管理";
            this._imagetoolStripMenuItem.Click += new System.EventHandler(this._imagetoolStripMenuItem_Click);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileManagement_FormClosed);
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
    }
}