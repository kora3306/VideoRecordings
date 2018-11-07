namespace VideoRecordings.Video
{
    partial class Frame
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TOP_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ref_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_step = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_uri = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_note = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1098, 594);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TOP_ToolStripMenuItem,
            this.Ref_ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // TOP_ToolStripMenuItem
            // 
            this.TOP_ToolStripMenuItem.Name = "TOP_ToolStripMenuItem";
            this.TOP_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.TOP_ToolStripMenuItem.Text = "视频解帧置顶";
            this.TOP_ToolStripMenuItem.Click += new System.EventHandler(this.TOP_ToolStripMenuItem_Click);
            // 
            // Ref_ToolStripMenuItem
            // 
            this.Ref_ToolStripMenuItem.Name = "Ref_ToolStripMenuItem";
            this.Ref_ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.Ref_ToolStripMenuItem.Text = "刷新";
            this.Ref_ToolStripMenuItem.Click += new System.EventHandler(this.Ref_ToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridView1.Appearance.GroupFooter.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumn_ID,
            this.gridColumn_step,
            this.gridColumn_uri,
            this.gridColumn_name,
            this.gridColumn_note});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.AllowHtmlDrawGroups = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnId
            // 
            this.gridColumnId.Caption = "队列序号";
            this.gridColumnId.FieldName = "Id";
            this.gridColumnId.MaxWidth = 60;
            this.gridColumnId.Name = "gridColumnId";
            this.gridColumnId.OptionsColumn.AllowEdit = false;
            this.gridColumnId.Visible = true;
            this.gridColumnId.VisibleIndex = 1;
            this.gridColumnId.Width = 60;
            // 
            // gridColumn_ID
            // 
            this.gridColumn_ID.Caption = "视频编号";
            this.gridColumn_ID.FieldName = "VideoId";
            this.gridColumn_ID.MaxWidth = 100;
            this.gridColumn_ID.Name = "gridColumn_ID";
            this.gridColumn_ID.OptionsColumn.AllowEdit = false;
            this.gridColumn_ID.Visible = true;
            this.gridColumn_ID.VisibleIndex = 2;
            this.gridColumn_ID.Width = 100;
            // 
            // gridColumn_step
            // 
            this.gridColumn_step.Caption = "解帧间隔";
            this.gridColumn_step.FieldName = "Step";
            this.gridColumn_step.MaxWidth = 60;
            this.gridColumn_step.Name = "gridColumn_step";
            this.gridColumn_step.OptionsColumn.AllowEdit = false;
            this.gridColumn_step.Visible = true;
            this.gridColumn_step.VisibleIndex = 3;
            this.gridColumn_step.Width = 60;
            // 
            // gridColumn_uri
            // 
            this.gridColumn_uri.Caption = "视频路径";
            this.gridColumn_uri.FieldName = "VideoUri";
            this.gridColumn_uri.Name = "gridColumn_uri";
            this.gridColumn_uri.Visible = true;
            this.gridColumn_uri.VisibleIndex = 6;
            this.gridColumn_uri.Width = 540;
            // 
            // gridColumn_name
            // 
            this.gridColumn_name.Caption = "存放文件夹";
            this.gridColumn_name.FieldName = "TaskName";
            this.gridColumn_name.Name = "gridColumn_name";
            this.gridColumn_name.Visible = true;
            this.gridColumn_name.VisibleIndex = 4;
            // 
            // gridColumn_note
            // 
            this.gridColumn_note.Caption = "备注";
            this.gridColumn_note.FieldName = "Note";
            this.gridColumn_note.Name = "gridColumn_note";
            this.gridColumn_note.Visible = true;
            this.gridColumn_note.VisibleIndex = 5;
            // 
            // Frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 594);
            this.Controls.Add(this.gridControl1);
            this.Name = "Frame";
            this.Text = "Frame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frame_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_uri;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_step;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TOP_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Ref_ToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_name;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_note;
    }
}