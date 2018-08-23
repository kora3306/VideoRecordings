namespace VideoRecordings
{
    partial class ManagementLabel
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
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OPenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.73218F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.26782F));
            this.tableLayoutPanel1.Controls.Add(this.treeList1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(926, 678);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Red;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.Row.Options.UseFont = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.tableLayoutPanel1.SetColumnSpan(this.treeList1, 2);
            this.treeList1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeList1.Location = new System.Drawing.Point(3, 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.ReadOnly = true;
            this.treeList1.OptionsFind.AlwaysVisible = true;
            this.treeList1.OptionsView.ShowSummaryFooter = true;
            this.treeList1.Size = new System.Drawing.Size(920, 637);
            this.treeList1.TabIndex = 4;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "标签";
            this.treeListColumn1.FieldName = "Label";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OPenToolStripMenuItem,
            this.RefreshToolStripMenuItem,
            this.AddLabelToolStripMenuItem,
            this.UpLabelToolStripMenuItem,
            this.DelLabelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 114);
            // 
            // OPenToolStripMenuItem
            // 
            this.OPenToolStripMenuItem.Name = "OPenToolStripMenuItem";
            this.OPenToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.OPenToolStripMenuItem.Text = "打开/关闭节点";
            this.OPenToolStripMenuItem.Click += new System.EventHandler(this.OPenToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.RefreshToolStripMenuItem.Text = "刷新";
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // AddLabelToolStripMenuItem
            // 
            this.AddLabelToolStripMenuItem.Name = "AddLabelToolStripMenuItem";
            this.AddLabelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.AddLabelToolStripMenuItem.Text = "新增标签";
            this.AddLabelToolStripMenuItem.Click += new System.EventHandler(this.AddLabelToolStripMenuItem_Click);
            // 
            // UpLabelToolStripMenuItem
            // 
            this.UpLabelToolStripMenuItem.Name = "UpLabelToolStripMenuItem";
            this.UpLabelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.UpLabelToolStripMenuItem.Text = "修改标签名";
            this.UpLabelToolStripMenuItem.Click += new System.EventHandler(this.UpLabelToolStripMenuItem_Click);
            // 
            // DelLabelToolStripMenuItem
            // 
            this.DelLabelToolStripMenuItem.Name = "DelLabelToolStripMenuItem";
            this.DelLabelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.DelLabelToolStripMenuItem.Text = "删除标签";
            this.DelLabelToolStripMenuItem.Click += new System.EventHandler(this.DelLabelToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Location = new System.Drawing.Point(0, 643);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(926, 35);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ManagementLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 678);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "ManagementLabel";
            this.Text = "ManagementLabel";
            this.Load += new System.EventHandler(this.ManagementLabel_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UpLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelLabelToolStripMenuItem;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.ToolStripMenuItem OPenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
    }
}