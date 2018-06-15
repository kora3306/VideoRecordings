namespace VideoRecordings
{
    partial class ImageInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageInformation));
            this.label2 = new System.Windows.Forms.Label();
            this.gridColumn_record_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gridColumn_note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gridColumn_recorder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scanning_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_place = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_start_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_end_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_uri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_size = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_video_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_replicator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_oldUri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(1003, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "欢迎:XX";
            // 
            // gridColumn_record_time
            // 
            this.gridColumn_record_time.Caption = "记录时间";
            this.gridColumn_record_time.FieldName = "RecordTime";
            this.gridColumn_record_time.Name = "gridColumn_record_time";
            this.gridColumn_record_time.OptionsColumn.AllowEdit = false;
            this.gridColumn_record_time.Visible = true;
            this.gridColumn_record_time.VisibleIndex = 11;
            this.gridColumn_record_time.Width = 85;
            // 
            // gridColumn_status
            // 
            this.gridColumn_status.Caption = "状态";
            this.gridColumn_status.FieldName = "Status";
            this.gridColumn_status.Name = "gridColumn_status";
            this.gridColumn_status.OptionsColumn.AllowEdit = false;
            this.gridColumn_status.Visible = true;
            this.gridColumn_status.VisibleIndex = 2;
            this.gridColumn_status.Width = 67;
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 5);
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Location = new System.Drawing.Point(0, 521);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1111, 83);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.bindingSource1;
            this.tableLayoutPanel1.SetColumnSpan(this.bindingNavigator1, 5);
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 491);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1111, 30);
            this.bindingNavigator1.TabIndex = 7;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 27);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 30);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // gridColumn_note
            // 
            this.gridColumn_note.Caption = "备注";
            this.gridColumn_note.FieldName = "Note";
            this.gridColumn_note.Name = "gridColumn_note";
            this.gridColumn_note.OptionsColumn.AllowEdit = false;
            this.gridColumn_note.Visible = true;
            this.gridColumn_note.VisibleIndex = 12;
            this.gridColumn_note.Width = 186;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 5);
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(493, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "入库视频信息";
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.button3.Location = new System.Drawing.Point(108, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 24);
            this.button3.TabIndex = 5;
            this.button3.Text = "查询图片";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.button1.Location = new System.Drawing.Point(3, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "添加图片文件夹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button2.Location = new System.Drawing.Point(193, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 24);
            this.button2.TabIndex = 8;
            this.button2.Text = "标签配置管理";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // gridColumn_recorder
            // 
            this.gridColumn_recorder.Caption = "记录人员";
            this.gridColumn_recorder.FieldName = "Recorder";
            this.gridColumn_recorder.Name = "gridColumn_recorder";
            this.gridColumn_recorder.OptionsColumn.AllowEdit = false;
            this.gridColumn_recorder.Visible = true;
            this.gridColumn_recorder.VisibleIndex = 8;
            this.gridColumn_recorder.Width = 66;
            // 
            // gridControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gridControl1, 5);
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridControl1.Location = new System.Drawing.Point(3, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1105, 405);
            this.gridControl1.TabIndex = 1;
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
            this.DELToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 92);
            // 
            // scanning_ToolStripMenuItem
            // 
            this.scanning_ToolStripMenuItem.Name = "scanning_ToolStripMenuItem";
            this.scanning_ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.scanning_ToolStripMenuItem.Text = "扫描文件夹内容并打开";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem.Text = "扫描文件夹";
            // 
            // ModifyToolStripMenuItem
            // 
            this.ModifyToolStripMenuItem.Name = "ModifyToolStripMenuItem";
            this.ModifyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ModifyToolStripMenuItem.Text = "修改文件信息";
            // 
            // DELToolStripMenuItem
            // 
            this.DELToolStripMenuItem.Name = "DELToolStripMenuItem";
            this.DELToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.DELToolStripMenuItem.Text = "清除扫描的文件信息";
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_Id,
            this.gridColumn_name,
            this.gridColumn_place,
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
            this.gridColumn_oldUri});
            this.gridView1.DetailHeight = 300;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 30;
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
            this.gridColumn_name.Width = 63;
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
            this.gridColumn_place.Width = 66;
            // 
            // gridColumn_start_date
            // 
            this.gridColumn_start_date.Caption = "开始时间";
            this.gridColumn_start_date.FieldName = "StartDate";
            this.gridColumn_start_date.Name = "gridColumn_start_date";
            this.gridColumn_start_date.OptionsColumn.AllowEdit = false;
            this.gridColumn_start_date.Visible = true;
            this.gridColumn_start_date.VisibleIndex = 3;
            this.gridColumn_start_date.Width = 66;
            // 
            // gridColumn_end_date
            // 
            this.gridColumn_end_date.Caption = "终止时间";
            this.gridColumn_end_date.FieldName = "EndDate";
            this.gridColumn_end_date.Name = "gridColumn_end_date";
            this.gridColumn_end_date.OptionsColumn.AllowEdit = false;
            this.gridColumn_end_date.Visible = true;
            this.gridColumn_end_date.VisibleIndex = 4;
            this.gridColumn_end_date.Width = 66;
            // 
            // gridColumn_uri
            // 
            this.gridColumn_uri.Caption = "存放路径";
            this.gridColumn_uri.FieldName = "Uri";
            this.gridColumn_uri.Name = "gridColumn_uri";
            this.gridColumn_uri.OptionsColumn.ReadOnly = true;
            this.gridColumn_uri.Visible = true;
            this.gridColumn_uri.VisibleIndex = 9;
            this.gridColumn_uri.Width = 284;
            // 
            // gridColumn_size
            // 
            this.gridColumn_size.Caption = "文件大小";
            this.gridColumn_size.FieldName = "Size";
            this.gridColumn_size.Name = "gridColumn_size";
            this.gridColumn_size.OptionsColumn.AllowEdit = false;
            this.gridColumn_size.Visible = true;
            this.gridColumn_size.VisibleIndex = 5;
            this.gridColumn_size.Width = 36;
            // 
            // gridColumn_video_count
            // 
            this.gridColumn_video_count.Caption = "图片数量";
            this.gridColumn_video_count.FieldName = "ImageCount";
            this.gridColumn_video_count.Name = "gridColumn_video_count";
            this.gridColumn_video_count.OptionsColumn.AllowEdit = false;
            this.gridColumn_video_count.Visible = true;
            this.gridColumn_video_count.VisibleIndex = 6;
            this.gridColumn_video_count.Width = 36;
            // 
            // gridColumn_replicator
            // 
            this.gridColumn_replicator.Caption = "拷贝人员";
            this.gridColumn_replicator.FieldName = "Replicator";
            this.gridColumn_replicator.Name = "gridColumn_replicator";
            this.gridColumn_replicator.OptionsColumn.AllowEdit = false;
            this.gridColumn_replicator.Visible = true;
            this.gridColumn_replicator.VisibleIndex = 7;
            this.gridColumn_replicator.Width = 66;
            // 
            // gridColumn_oldUri
            // 
            this.gridColumn_oldUri.Caption = "原路径";
            this.gridColumn_oldUri.FieldName = "OldUri";
            this.gridColumn_oldUri.Name = "gridColumn_oldUri";
            this.gridColumn_oldUri.Visible = true;
            this.gridColumn_oldUri.VisibleIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.bindingNavigator1, 0, 3);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1111, 604);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ImageInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 604);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ImageInformation";
            this.Text = "ImageInformation";
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_record_time;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_status;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem scanning_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DELToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_place;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_start_date;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_end_date;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_uri;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_size;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_video_count;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_replicator;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_recorder;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_note;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_oldUri;
    }
}