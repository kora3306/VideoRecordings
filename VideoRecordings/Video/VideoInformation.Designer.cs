namespace VideoRecordings
{
    partial class VideoInformation
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scanning_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_place = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_scenes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_start_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_end_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_uri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_size = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_video_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_replicator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_recorder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_record_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_percent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1094, 618);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gridControl1, 5);
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.gridControl1.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.gridControl1.EmbeddedNavigator.TextStringFormat = "当前数量 {0} / {1}";
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridControl1.Location = new System.Drawing.Point(3, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.tableLayoutPanel1.SetRowSpan(this.gridControl1, 2);
            this.gridControl1.Size = new System.Drawing.Size(1088, 447);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanning_ToolStripMenuItem,
            this.ToolStripMenuItem,
            this.ModifyToolStripMenuItem,
            this.DELToolStripMenuItem,
            this.RefToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 114);
            // 
            // scanning_ToolStripMenuItem
            // 
            this.scanning_ToolStripMenuItem.Name = "scanning_ToolStripMenuItem";
            this.scanning_ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.scanning_ToolStripMenuItem.Text = "扫描文件夹内容并打开";
            this.scanning_ToolStripMenuItem.Click += new System.EventHandler(this.scanning_ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem.Text = "扫描文件夹";
            this.ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ModifyToolStripMenuItem
            // 
            this.ModifyToolStripMenuItem.Name = "ModifyToolStripMenuItem";
            this.ModifyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ModifyToolStripMenuItem.Text = "修改文件信息";
            this.ModifyToolStripMenuItem.Click += new System.EventHandler(this.ModifyToolStripMenuItem_Click);
            // 
            // DELToolStripMenuItem
            // 
            this.DELToolStripMenuItem.Name = "DELToolStripMenuItem";
            this.DELToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.DELToolStripMenuItem.Text = "清除扫描的文件信息";
            this.DELToolStripMenuItem.Click += new System.EventHandler(this.DELToolStripMenuItem_Click);
            // 
            // RefToolStripMenuItem
            // 
            this.RefToolStripMenuItem.Name = "RefToolStripMenuItem";
            this.RefToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.RefToolStripMenuItem.Text = "刷新文件夹信息";
            this.RefToolStripMenuItem.Click += new System.EventHandler(this.RefToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_Id,
            this.gridColumn_name,
            this.gridColumn_place,
            this.gridColumn_scenes,
            this.gridColumn_start_date,
            this.gridColumn_end_date,
            this.gridColumn_uri,
            this.gridColumn_size,
            this.gridColumn_video_count,
            this.gridColumn_replicator,
            this.gridColumn_recorder,
            this.gridColumn_note,
            this.gridColumn_record_time,
            this.gridColumn_status,
            this.gridColumn_percent});
            this.gridView1.DetailHeight = 300;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 30;
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // gridColumn_Id
            // 
            this.gridColumn_Id.Caption = "文件夹编号";
            this.gridColumn_Id.FieldName = "Id";
            this.gridColumn_Id.MinWidth = 30;
            this.gridColumn_Id.Name = "gridColumn_Id";
            this.gridColumn_Id.OptionsColumn.AllowEdit = false;
            this.gridColumn_Id.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_Id.Width = 80;
            // 
            // gridColumn_name
            // 
            this.gridColumn_name.Caption = "数据编号";
            this.gridColumn_name.FieldName = "Name";
            this.gridColumn_name.Name = "gridColumn_name";
            this.gridColumn_name.OptionsColumn.AllowEdit = false;
            this.gridColumn_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_name.Visible = true;
            this.gridColumn_name.VisibleIndex = 0;
            this.gridColumn_name.Width = 29;
            // 
            // gridColumn_place
            // 
            this.gridColumn_place.Caption = "采集地点";
            this.gridColumn_place.FieldName = "Place";
            this.gridColumn_place.Name = "gridColumn_place";
            this.gridColumn_place.OptionsColumn.AllowEdit = false;
            this.gridColumn_place.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_place.Visible = true;
            this.gridColumn_place.VisibleIndex = 1;
            this.gridColumn_place.Width = 61;
            // 
            // gridColumn_scenes
            // 
            this.gridColumn_scenes.Caption = "场景";
            this.gridColumn_scenes.FieldName = "ScenesName";
            this.gridColumn_scenes.Name = "gridColumn_scenes";
            this.gridColumn_scenes.OptionsColumn.AllowEdit = false;
            this.gridColumn_scenes.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_scenes.Visible = true;
            this.gridColumn_scenes.VisibleIndex = 2;
            this.gridColumn_scenes.Width = 41;
            // 
            // gridColumn_start_date
            // 
            this.gridColumn_start_date.Caption = "开始时间";
            this.gridColumn_start_date.FieldName = "StartDate";
            this.gridColumn_start_date.Name = "gridColumn_start_date";
            this.gridColumn_start_date.OptionsColumn.AllowEdit = false;
            this.gridColumn_start_date.Visible = true;
            this.gridColumn_start_date.VisibleIndex = 4;
            this.gridColumn_start_date.Width = 60;
            // 
            // gridColumn_end_date
            // 
            this.gridColumn_end_date.Caption = "终止时间";
            this.gridColumn_end_date.FieldName = "EndDate";
            this.gridColumn_end_date.Name = "gridColumn_end_date";
            this.gridColumn_end_date.OptionsColumn.AllowEdit = false;
            this.gridColumn_end_date.Visible = true;
            this.gridColumn_end_date.VisibleIndex = 5;
            this.gridColumn_end_date.Width = 60;
            // 
            // gridColumn_uri
            // 
            this.gridColumn_uri.Caption = "存放路径";
            this.gridColumn_uri.FieldName = "Uri";
            this.gridColumn_uri.Name = "gridColumn_uri";
            this.gridColumn_uri.OptionsColumn.ReadOnly = true;
            this.gridColumn_uri.Visible = true;
            this.gridColumn_uri.VisibleIndex = 10;
            this.gridColumn_uri.Width = 212;
            // 
            // gridColumn_size
            // 
            this.gridColumn_size.Caption = "文件大小";
            this.gridColumn_size.FieldName = "Size";
            this.gridColumn_size.Name = "gridColumn_size";
            this.gridColumn_size.OptionsColumn.AllowEdit = false;
            this.gridColumn_size.Visible = true;
            this.gridColumn_size.VisibleIndex = 6;
            this.gridColumn_size.Width = 34;
            // 
            // gridColumn_video_count
            // 
            this.gridColumn_video_count.Caption = "视频数量";
            this.gridColumn_video_count.FieldName = "Count";
            this.gridColumn_video_count.Name = "gridColumn_video_count";
            this.gridColumn_video_count.OptionsColumn.AllowEdit = false;
            this.gridColumn_video_count.Visible = true;
            this.gridColumn_video_count.VisibleIndex = 7;
            this.gridColumn_video_count.Width = 34;
            // 
            // gridColumn_replicator
            // 
            this.gridColumn_replicator.Caption = "拷贝人员";
            this.gridColumn_replicator.FieldName = "Replicator";
            this.gridColumn_replicator.Name = "gridColumn_replicator";
            this.gridColumn_replicator.OptionsColumn.AllowEdit = false;
            this.gridColumn_replicator.Visible = true;
            this.gridColumn_replicator.VisibleIndex = 8;
            this.gridColumn_replicator.Width = 60;
            // 
            // gridColumn_recorder
            // 
            this.gridColumn_recorder.Caption = "记录人员";
            this.gridColumn_recorder.FieldName = "Recorder";
            this.gridColumn_recorder.Name = "gridColumn_recorder";
            this.gridColumn_recorder.OptionsColumn.AllowEdit = false;
            this.gridColumn_recorder.Visible = true;
            this.gridColumn_recorder.VisibleIndex = 9;
            this.gridColumn_recorder.Width = 60;
            // 
            // gridColumn_note
            // 
            this.gridColumn_note.Caption = "备注";
            this.gridColumn_note.FieldName = "Note";
            this.gridColumn_note.Name = "gridColumn_note";
            this.gridColumn_note.OptionsColumn.AllowEdit = false;
            this.gridColumn_note.Visible = true;
            this.gridColumn_note.VisibleIndex = 13;
            this.gridColumn_note.Width = 198;
            // 
            // gridColumn_record_time
            // 
            this.gridColumn_record_time.Caption = "记录时间";
            this.gridColumn_record_time.FieldName = "RecordTime";
            this.gridColumn_record_time.Name = "gridColumn_record_time";
            this.gridColumn_record_time.OptionsColumn.AllowEdit = false;
            this.gridColumn_record_time.Visible = true;
            this.gridColumn_record_time.VisibleIndex = 11;
            this.gridColumn_record_time.Width = 89;
            // 
            // gridColumn_status
            // 
            this.gridColumn_status.Caption = "状态";
            this.gridColumn_status.FieldName = "Status";
            this.gridColumn_status.Name = "gridColumn_status";
            this.gridColumn_status.OptionsColumn.AllowEdit = false;
            this.gridColumn_status.Visible = true;
            this.gridColumn_status.VisibleIndex = 3;
            this.gridColumn_status.Width = 49;
            // 
            // gridColumn_percent
            // 
            this.gridColumn_percent.Caption = "完成度";
            this.gridColumn_percent.ColumnEdit = this.repositoryItemProgressBar1;
            this.gridColumn_percent.FieldName = "Percent";
            this.gridColumn_percent.Name = "gridColumn_percent";
            this.gridColumn_percent.OptionsColumn.AllowEdit = false;
            this.gridColumn_percent.Visible = true;
            this.gridColumn_percent.VisibleIndex = 12;
            this.gridColumn_percent.Width = 83;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 5);
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1094, 85);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(171, 80);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 5);
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(484, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "入库视频信息";
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.button3.Location = new System.Drawing.Point(83, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 24);
            this.button3.TabIndex = 5;
            this.button3.Text = "查询视频";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.button1.Location = new System.Drawing.Point(3, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "添加视频";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(986, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "欢迎:XX";
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button2.Location = new System.Drawing.Point(163, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 24);
            this.button2.TabIndex = 8;
            this.button2.Text = "标签配置管理";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "The Bezier";
            // 
            // VideoInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1094, 618);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VideoInformation";
            this.Text = "文件夹目录";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_place;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_scenes;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_start_date;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_end_date;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_uri;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_size;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_video_count;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_replicator;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_recorder;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_note;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_record_time;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_percent;
          private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem scanning_ToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_status;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem ModifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DELToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem RefToolStripMenuItem;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}