namespace VideoRecordings.Video
{
    partial class BatchScreenshot
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_batch_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_user_name = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(785, 466);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_id,
            this.gridColumn_batch_id,
            this.gridColumn_status,
            this.gridColumn_user_name});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn_id
            // 
            this.gridColumn_id.Caption = "序号";
            this.gridColumn_id.FieldName = "ID";
            this.gridColumn_id.MaxWidth = 60;
            this.gridColumn_id.Name = "gridColumn_id";
            this.gridColumn_id.Visible = true;
            this.gridColumn_id.VisibleIndex = 0;
            this.gridColumn_id.Width = 60;
            // 
            // gridColumn_batch_id
            // 
            this.gridColumn_batch_id.Caption = "批次信息";
            this.gridColumn_batch_id.FieldName = "BatchId";
            this.gridColumn_batch_id.MaxWidth = 80;
            this.gridColumn_batch_id.Name = "gridColumn_batch_id";
            this.gridColumn_batch_id.Visible = true;
            this.gridColumn_batch_id.VisibleIndex = 1;
            this.gridColumn_batch_id.Width = 80;
            // 
            // gridColumn_status
            // 
            this.gridColumn_status.Caption = "运行状态";
            this.gridColumn_status.FieldName = "Status";
            this.gridColumn_status.MaxWidth = 80;
            this.gridColumn_status.Name = "gridColumn_status";
            this.gridColumn_status.Visible = true;
            this.gridColumn_status.VisibleIndex = 2;
            this.gridColumn_status.Width = 80;
            // 
            // gridColumn_user_name
            // 
            this.gridColumn_user_name.Caption = "添加用户";
            this.gridColumn_user_name.FieldName = "UserName";
            this.gridColumn_user_name.Name = "gridColumn_user_name";
            this.gridColumn_user_name.Visible = true;
            this.gridColumn_user_name.VisibleIndex = 3;
            this.gridColumn_user_name.Width = 506;
            // 
            // BatchScreenshot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 466);
            this.Controls.Add(this.gridControl1);
            this.Name = "BatchScreenshot";
            this.Text = "BatchScreenshot";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_batch_id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_status;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_user_name;
    }
}