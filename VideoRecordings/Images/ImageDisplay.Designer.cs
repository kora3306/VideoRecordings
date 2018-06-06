namespace VideoRecordings.Images
{
    partial class ImageDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDisplay));
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.gridColumn_images = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_create_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
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
            this.imageListView1 = new Manina.Windows.Forms.ImageListView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.gridColumn_record_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.gridColumn_end_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dispalyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenfolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_project_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_uri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_recorded = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_deframed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_frame_path = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_label = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_start_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn_images
            // 
            this.gridColumn_images.Caption = "截图数量";
            this.gridColumn_images.FieldName = "Images";
            this.gridColumn_images.Name = "gridColumn_images";
            this.gridColumn_images.OptionsColumn.AllowEdit = false;
            this.gridColumn_images.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_images.Visible = true;
            this.gridColumn_images.VisibleIndex = 6;
            this.gridColumn_images.Width = 35;
            // 
            // gridColumn_create_time
            // 
            this.gridColumn_create_time.Caption = "创建时间";
            this.gridColumn_create_time.FieldName = "CreateTime";
            this.gridColumn_create_time.Name = "gridColumn_create_time";
            this.gridColumn_create_time.OptionsColumn.AllowEdit = false;
            this.gridColumn_create_time.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_create_time.Visible = true;
            this.gridColumn_create_time.VisibleIndex = 12;
            this.gridColumn_create_time.Width = 100;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(474, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件信息";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.bindingSource1;
            this.tableLayoutPanel1.SetColumnSpan(this.bindingNavigator1, 2);
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 418);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1184, 25);
            this.bindingNavigator1.TabIndex = 3;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
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
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // imageListView1
            // 
            this.imageListView1.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tableLayoutPanel1.SetColumnSpan(this.imageListView1, 2);
            this.imageListView1.ContextMenuStrip = this.contextMenuStrip2;
            this.imageListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView1.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageListView1.Location = new System.Drawing.Point(3, 446);
            this.imageListView1.Name = "imageListView1";
            this.imageListView1.PersistentCacheDirectory = "F:\\源码\\VideoRecordings/cache";
            this.imageListView1.PersistentCacheSize = ((long)(0));
            this.imageListView1.Size = new System.Drawing.Size(1178, 140);
            this.imageListView1.TabIndex = 4;
            this.imageListView1.UseWIC = Manina.Windows.Forms.UseWIC.ThumbnailsOnly;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DELToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            // 
            // DELToolStripMenuItem
            // 
            this.DELToolStripMenuItem.Name = "DELToolStripMenuItem";
            this.DELToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.DELToolStripMenuItem.Text = "删除截图";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(1076, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "欢迎:XX";
            // 
            // gridColumn_record_time
            // 
            this.gridColumn_record_time.Caption = "视频录制时间";
            this.gridColumn_record_time.FieldName = "RecordTime";
            this.gridColumn_record_time.Name = "gridColumn_record_time";
            this.gridColumn_record_time.OptionsColumn.AllowEdit = false;
            this.gridColumn_record_time.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_record_time.Visible = true;
            this.gridColumn_record_time.VisibleIndex = 2;
            this.gridColumn_record_time.Width = 78;
            // 
            // gridColumn_end_time
            // 
            this.gridColumn_end_time.Caption = "结束时间";
            this.gridColumn_end_time.FieldName = "EndTime";
            this.gridColumn_end_time.Name = "gridColumn_end_time";
            this.gridColumn_end_time.OptionsColumn.AllowEdit = false;
            this.gridColumn_end_time.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_end_time.Visible = true;
            this.gridColumn_end_time.VisibleIndex = 4;
            this.gridColumn_end_time.Width = 56;
            // 
            // gridControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gridControl1, 2);
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1178, 377);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dispalyToolStripMenuItem,
            this.DToolStripMenuItem,
            this.OpenfolderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 70);
            // 
            // dispalyToolStripMenuItem
            // 
            this.dispalyToolStripMenuItem.Name = "dispalyToolStripMenuItem";
            this.dispalyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.dispalyToolStripMenuItem.Text = "播放视频";
            // 
            // DToolStripMenuItem
            // 
            this.DToolStripMenuItem.Name = "DToolStripMenuItem";
            this.DToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DToolStripMenuItem.Text = "删除视频记录";
            // 
            // OpenfolderToolStripMenuItem
            // 
            this.OpenfolderToolStripMenuItem.Name = "OpenfolderToolStripMenuItem";
            this.OpenfolderToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.OpenfolderToolStripMenuItem.Text = "打开视频所在文件夹";
            // 
            // gridView1
            // 
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gridView1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_id,
            this.gridColumn_name,
            this.gridColumn_project_name,
            this.gridColumn_uri,
            this.gridColumn_status,
            this.gridColumn_recorded,
            this.gridColumn_deframed,
            this.gridColumn_frame_path,
            this.gridColumn_label,
            this.gridColumn_start_time,
            this.gridColumn_end_time,
            this.gridColumn_record_time,
            this.gridColumn_images,
            this.gridColumn_create_time});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 30;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn_id
            // 
            this.gridColumn_id.Caption = "视频编号";
            this.gridColumn_id.FieldName = "Id";
            this.gridColumn_id.Name = "gridColumn_id";
            this.gridColumn_id.OptionsColumn.AllowEdit = false;
            this.gridColumn_id.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_id.Visible = true;
            this.gridColumn_id.VisibleIndex = 0;
            this.gridColumn_id.Width = 55;
            // 
            // gridColumn_name
            // 
            this.gridColumn_name.Caption = "视频名称";
            this.gridColumn_name.FieldName = "Name";
            this.gridColumn_name.Name = "gridColumn_name";
            this.gridColumn_name.OptionsColumn.AllowEdit = false;
            this.gridColumn_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_name.Width = 62;
            // 
            // gridColumn_project_name
            // 
            this.gridColumn_project_name.Caption = "数据编号";
            this.gridColumn_project_name.FieldName = "ProjectName";
            this.gridColumn_project_name.Name = "gridColumn_project_name";
            this.gridColumn_project_name.OptionsColumn.AllowEdit = false;
            this.gridColumn_project_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_project_name.Visible = true;
            this.gridColumn_project_name.VisibleIndex = 1;
            this.gridColumn_project_name.Width = 60;
            // 
            // gridColumn_uri
            // 
            this.gridColumn_uri.Caption = "视频相对路径";
            this.gridColumn_uri.FieldName = "Uri";
            this.gridColumn_uri.Name = "gridColumn_uri";
            this.gridColumn_uri.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_uri.OptionsColumn.ReadOnly = true;
            this.gridColumn_uri.Visible = true;
            this.gridColumn_uri.VisibleIndex = 10;
            this.gridColumn_uri.Width = 248;
            // 
            // gridColumn_status
            // 
            this.gridColumn_status.Caption = "视频状态";
            this.gridColumn_status.FieldName = "Status";
            this.gridColumn_status.Name = "gridColumn_status";
            this.gridColumn_status.OptionsColumn.AllowEdit = false;
            this.gridColumn_status.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_status.Visible = true;
            this.gridColumn_status.VisibleIndex = 7;
            this.gridColumn_status.Width = 60;
            // 
            // gridColumn_recorded
            // 
            this.gridColumn_recorded.Caption = "是否记录";
            this.gridColumn_recorded.FieldName = "Recorded";
            this.gridColumn_recorded.Name = "gridColumn_recorded";
            this.gridColumn_recorded.OptionsColumn.AllowEdit = false;
            this.gridColumn_recorded.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_recorded.Visible = true;
            this.gridColumn_recorded.VisibleIndex = 8;
            this.gridColumn_recorded.Width = 60;
            // 
            // gridColumn_deframed
            // 
            this.gridColumn_deframed.Caption = "是否解帧";
            this.gridColumn_deframed.FieldName = "Deframed";
            this.gridColumn_deframed.Name = "gridColumn_deframed";
            this.gridColumn_deframed.OptionsColumn.AllowEdit = false;
            this.gridColumn_deframed.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_deframed.Visible = true;
            this.gridColumn_deframed.VisibleIndex = 9;
            this.gridColumn_deframed.Width = 60;
            // 
            // gridColumn_frame_path
            // 
            this.gridColumn_frame_path.Caption = "解帧路径";
            this.gridColumn_frame_path.FieldName = "FramePath";
            this.gridColumn_frame_path.Name = "gridColumn_frame_path";
            this.gridColumn_frame_path.OptionsColumn.AllowEdit = false;
            this.gridColumn_frame_path.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_frame_path.Visible = true;
            this.gridColumn_frame_path.VisibleIndex = 11;
            this.gridColumn_frame_path.Width = 71;
            // 
            // gridColumn_label
            // 
            this.gridColumn_label.Caption = "标签";
            this.gridColumn_label.FieldName = "Label";
            this.gridColumn_label.Name = "gridColumn_label";
            this.gridColumn_label.OptionsColumn.AllowEdit = false;
            this.gridColumn_label.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_label.Visible = true;
            this.gridColumn_label.VisibleIndex = 5;
            this.gridColumn_label.Width = 131;
            // 
            // gridColumn_start_time
            // 
            this.gridColumn_start_time.Caption = "开始时间";
            this.gridColumn_start_time.FieldName = "StartTime";
            this.gridColumn_start_time.Name = "gridColumn_start_time";
            this.gridColumn_start_time.OptionsColumn.AllowEdit = false;
            this.gridColumn_start_time.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn_start_time.Visible = true;
            this.gridColumn_start_time.VisibleIndex = 3;
            this.gridColumn_start_time.Width = 56;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bindingNavigator1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.imageListView1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.69216F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.01998F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.28786F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 671);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 82);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(171, 77);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ImageDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 671);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ImageDisplay";
            this.Text = "ImageDisplay";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_images;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_create_time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dispalyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenfolderToolStripMenuItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_project_name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_uri;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_status;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_recorded;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_deframed;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_frame_path;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_label;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_start_time;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_end_time;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_record_time;
        private Manina.Windows.Forms.ImageListView imageListView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem DELToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}