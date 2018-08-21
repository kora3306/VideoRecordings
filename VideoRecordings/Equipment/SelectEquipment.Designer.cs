namespace VideoRecordings.Equipment
{
    partial class SelectEquipment
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
            this.RefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_ref = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.comboBox_uid = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_city = new System.Windows.Forms.ComboBox();
            this.comboBox_street = new System.Windows.Forms.ComboBox();
            this.comboBox_site = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.imageListView1 = new Manina.Windows.Forms.ImageListView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeList1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1058, 641);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.BorderColor = System.Drawing.Color.Transparent;
            this.treeList1.Appearance.FocusedCell.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Red;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseBorderColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseFont = true;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeList1.Location = new System.Drawing.Point(333, 3);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.ReadOnly = true;
            this.treeList1.OptionsFind.AlwaysVisible = true;
            this.treeList1.OptionsMenu.EnableColumnMenu = false;
            this.treeList1.OptionsView.ShowRowFooterSummary = true;
            this.treeList1.RowHeight = 30;
            this.treeList1.Size = new System.Drawing.Size(722, 635);
            this.treeList1.TabIndex = 10;
            this.treeList1.Click += new System.EventHandler(this.treeList1_Click);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceCell.Options.UseFont = true;
            this.treeListColumn1.Caption = "通道名称";
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.MinWidth = 30;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 48;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // RefToolStripMenuItem
            // 
            this.RefToolStripMenuItem.Name = "RefToolStripMenuItem";
            this.RefToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.RefToolStripMenuItem.Text = "刷新";
            this.RefToolStripMenuItem.Click += new System.EventHandler(this.RefToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imageListView1);
            this.groupBox1.Controls.Add(this.button_ref);
            this.groupBox1.Controls.Add(this.button_OK);
            this.groupBox1.Controls.Add(this.comboBox_uid);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox_city);
            this.groupBox1.Controls.Add(this.comboBox_street);
            this.groupBox1.Controls.Add(this.comboBox_site);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 635);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选条件";
            // 
            // button_ref
            // 
            this.button_ref.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_ref.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ref.Location = new System.Drawing.Point(163, 334);
            this.button_ref.Name = "button_ref";
            this.button_ref.Size = new System.Drawing.Size(97, 32);
            this.button_ref.TabIndex = 9;
            this.button_ref.Text = "重置条件";
            this.button_ref.UseVisualStyleBackColor = true;
            this.button_ref.Click += new System.EventHandler(this.button_ref_Click);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_OK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_OK.Location = new System.Drawing.Point(21, 334);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(97, 32);
            this.button_OK.TabIndex = 9;
            this.button_OK.Text = "确认";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // comboBox_uid
            // 
            this.comboBox_uid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox_uid.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_uid.FormattingEnabled = true;
            this.comboBox_uid.Location = new System.Drawing.Point(11, 281);
            this.comboBox_uid.Name = "comboBox_uid";
            this.comboBox_uid.Size = new System.Drawing.Size(284, 29);
            this.comboBox_uid.TabIndex = 4;
            this.comboBox_uid.SelectedIndexChanged += new System.EventHandler(this.comboBox_uid_SelectedIndexChanged);
            this.comboBox_uid.TextUpdate += new System.EventHandler(this.comboBox_uid_TextUpdate);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "城市:";
            // 
            // comboBox_city
            // 
            this.comboBox_city.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox_city.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_city.FormattingEnabled = true;
            this.comboBox_city.Location = new System.Drawing.Point(11, 57);
            this.comboBox_city.Name = "comboBox_city";
            this.comboBox_city.Size = new System.Drawing.Size(284, 29);
            this.comboBox_city.TabIndex = 1;
            this.comboBox_city.SelectedIndexChanged += new System.EventHandler(this.comboBox_city_SelectedIndexChanged);
            this.comboBox_city.TextUpdate += new System.EventHandler(this.comboBox_city_TextUpdate);
            // 
            // comboBox_street
            // 
            this.comboBox_street.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox_street.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_street.FormattingEnabled = true;
            this.comboBox_street.Location = new System.Drawing.Point(11, 132);
            this.comboBox_street.Name = "comboBox_street";
            this.comboBox_street.Size = new System.Drawing.Size(284, 29);
            this.comboBox_street.TabIndex = 2;
            this.comboBox_street.SelectedIndexChanged += new System.EventHandler(this.comboBox_street_SelectedIndexChanged);
            this.comboBox_street.TextUpdate += new System.EventHandler(this.comboBox_street_TextUpdate);
            // 
            // comboBox_site
            // 
            this.comboBox_site.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox_site.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_site.FormattingEnabled = true;
            this.comboBox_site.Location = new System.Drawing.Point(11, 207);
            this.comboBox_site.Name = "comboBox_site";
            this.comboBox_site.Size = new System.Drawing.Size(284, 29);
            this.comboBox_site.TabIndex = 3;
            this.comboBox_site.SelectedIndexChanged += new System.EventHandler(this.comboBox_site_SelectedIndexChanged);
            this.comboBox_site.TextUpdate += new System.EventHandler(this.comboBox_site_TextUpdate);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(7, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 21);
            this.label6.TabIndex = 8;
            this.label6.Text = "街道:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(7, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 21);
            this.label8.TabIndex = 8;
            this.label8.Text = "编号:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(7, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 21);
            this.label7.TabIndex = 8;
            this.label7.Text = "地址:";
            // 
            // imageListView1
            // 
            this.imageListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageListView1.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.imageListView1.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageListView1.Location = new System.Drawing.Point(11, 389);
            this.imageListView1.Name = "imageListView1";
            this.imageListView1.Size = new System.Drawing.Size(307, 237);
            this.imageListView1.TabIndex = 10;
            this.imageListView1.UseWIC = Manina.Windows.Forms.UseWIC.ThumbnailsOnly;
            // 
            // SelectEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 641);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SelectEquipment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelectEquipment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectEquipment_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_uid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_city;
        private System.Windows.Forms.ComboBox comboBox_street;
        private System.Windows.Forms.ComboBox comboBox_site;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RefToolStripMenuItem;
        private System.Windows.Forms.Button button_ref;
        private Manina.Windows.Forms.ImageListView imageListView1;
    }
}