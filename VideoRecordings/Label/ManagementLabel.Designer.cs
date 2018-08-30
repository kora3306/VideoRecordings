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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OPenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opendoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlycarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlypersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动态关联ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置标签类型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dnlabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticlabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 926F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainerControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(926, 678);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(3, 3);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(920, 638);
            this.splitContainerControl1.SplitterPosition = 594;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeList1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 638);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "静态标签";
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Red;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.Row.Options.UseFont = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeList1.Location = new System.Drawing.Point(3, 22);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.ReadOnly = true;
            this.treeList1.OptionsFind.AlwaysVisible = true;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.OptionsView.ShowSummaryFooter = true;
            this.treeList1.Size = new System.Drawing.Size(588, 613);
            this.treeList1.TabIndex = 4;
            this.treeList1.Click += new System.EventHandler(this.treeList1_Click);
            this.treeList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDown);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "静态标签";
            this.treeListColumn1.FieldName = "Label";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OPenToolStripMenuItem,
            this.opendoneToolStripMenuItem,
            this.onlycarToolStripMenuItem,
            this.onlypersonToolStripMenuItem,
            this.RefreshToolStripMenuItem,
            this.AddLabelToolStripMenuItem,
            this.UpLabelToolStripMenuItem,
            this.DelLabelToolStripMenuItem,
            this.动态关联ToolStripMenuItem,
            this.设置标签类型ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 224);
            // 
            // OPenToolStripMenuItem
            // 
            this.OPenToolStripMenuItem.Name = "OPenToolStripMenuItem";
            this.OPenToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.OPenToolStripMenuItem.Text = "打开/关闭节点(静)";
            this.OPenToolStripMenuItem.Click += new System.EventHandler(this.OPenToolStripMenuItem_Click);
            // 
            // opendoneToolStripMenuItem
            // 
            this.opendoneToolStripMenuItem.Name = "opendoneToolStripMenuItem";
            this.opendoneToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.opendoneToolStripMenuItem.Text = "打开/关闭节点(动)";
            this.opendoneToolStripMenuItem.Click += new System.EventHandler(this.opendoneToolStripMenuItem_Click);
            // 
            // onlycarToolStripMenuItem
            // 
            this.onlycarToolStripMenuItem.Name = "onlycarToolStripMenuItem";
            this.onlycarToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.onlycarToolStripMenuItem.Text = "不显示已关联车";
            this.onlycarToolStripMenuItem.Click += new System.EventHandler(this.onlycarToolStripMenuItem_Click);
            // 
            // onlypersonToolStripMenuItem
            // 
            this.onlypersonToolStripMenuItem.Name = "onlypersonToolStripMenuItem";
            this.onlypersonToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.onlypersonToolStripMenuItem.Text = "不显示已关联人";
            this.onlypersonToolStripMenuItem.Click += new System.EventHandler(this.onlypersonToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.RefreshToolStripMenuItem.Text = "刷新";
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // AddLabelToolStripMenuItem
            // 
            this.AddLabelToolStripMenuItem.Name = "AddLabelToolStripMenuItem";
            this.AddLabelToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.AddLabelToolStripMenuItem.Text = "新增标签";
            this.AddLabelToolStripMenuItem.Click += new System.EventHandler(this.AddLabelToolStripMenuItem_Click);
            // 
            // UpLabelToolStripMenuItem
            // 
            this.UpLabelToolStripMenuItem.Name = "UpLabelToolStripMenuItem";
            this.UpLabelToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.UpLabelToolStripMenuItem.Text = "修改标签名";
            this.UpLabelToolStripMenuItem.Click += new System.EventHandler(this.UpLabelToolStripMenuItem_Click);
            // 
            // DelLabelToolStripMenuItem
            // 
            this.DelLabelToolStripMenuItem.Name = "DelLabelToolStripMenuItem";
            this.DelLabelToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.DelLabelToolStripMenuItem.Text = "删除标签";
            this.DelLabelToolStripMenuItem.Click += new System.EventHandler(this.DelLabelToolStripMenuItem_Click);
            // 
            // 动态关联ToolStripMenuItem
            // 
            this.动态关联ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carToolStripMenuItem,
            this.personToolStripMenuItem,
            this.noneToolStripMenuItem});
            this.动态关联ToolStripMenuItem.Name = "动态关联ToolStripMenuItem";
            this.动态关联ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.动态关联ToolStripMenuItem.Text = "动态关联";
            // 
            // carToolStripMenuItem
            // 
            this.carToolStripMenuItem.Name = "carToolStripMenuItem";
            this.carToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.carToolStripMenuItem.Tag = "216";
            this.carToolStripMenuItem.Text = "车";
            this.carToolStripMenuItem.Click += new System.EventHandler(this.carToolStripMenuItem_Click);
            // 
            // personToolStripMenuItem
            // 
            this.personToolStripMenuItem.Name = "personToolStripMenuItem";
            this.personToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.personToolStripMenuItem.Tag = "215";
            this.personToolStripMenuItem.Text = "人";
            this.personToolStripMenuItem.Click += new System.EventHandler(this.personToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.noneToolStripMenuItem.Text = "无";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // 设置标签类型ToolStripMenuItem
            // 
            this.设置标签类型ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dnlabelToolStripMenuItem,
            this.staticlabelToolStripMenuItem});
            this.设置标签类型ToolStripMenuItem.Name = "设置标签类型ToolStripMenuItem";
            this.设置标签类型ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.设置标签类型ToolStripMenuItem.Text = "设置标签类型";
            // 
            // dnlabelToolStripMenuItem
            // 
            this.dnlabelToolStripMenuItem.Name = "dnlabelToolStripMenuItem";
            this.dnlabelToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.dnlabelToolStripMenuItem.Text = "动态标签";
            this.dnlabelToolStripMenuItem.Click += new System.EventHandler(this.dnlabelToolStripMenuItem_Click);
            // 
            // staticlabelToolStripMenuItem
            // 
            this.staticlabelToolStripMenuItem.Name = "staticlabelToolStripMenuItem";
            this.staticlabelToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.staticlabelToolStripMenuItem.Text = "静态标签";
            this.staticlabelToolStripMenuItem.Click += new System.EventHandler(this.staticlabelToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeList2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 638);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "动态标签";
            // 
            // treeList2
            // 
            this.treeList2.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList2.Appearance.Row.Options.UseFont = true;
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList2.ContextMenuStrip = this.contextMenuStrip1;
            this.treeList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList2.Location = new System.Drawing.Point(3, 22);
            this.treeList2.Name = "treeList2";
            this.treeList2.OptionsBehavior.Editable = false;
            this.treeList2.OptionsBehavior.ReadOnly = true;
            this.treeList2.OptionsFind.AlwaysVisible = true;
            this.treeList2.OptionsView.ShowCheckBoxes = true;
            this.treeList2.Size = new System.Drawing.Size(315, 613);
            this.treeList2.TabIndex = 0;
            this.treeList2.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList2_CustomDrawNodeCell);
            this.treeList2.Click += new System.EventHandler(this.treeList2_Click);
            this.treeList2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList2_MouseDown);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "动态标签";
            this.treeListColumn2.FieldName = "Label";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 644);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(926, 34);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(171, 29);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UpLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelLabelToolStripMenuItem;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.ToolStripMenuItem OPenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private System.Windows.Forms.ToolStripMenuItem 动态关联ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置标签类型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dnlabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staticlabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlycarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlypersonToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opendoneToolStripMenuItem;
    }
}