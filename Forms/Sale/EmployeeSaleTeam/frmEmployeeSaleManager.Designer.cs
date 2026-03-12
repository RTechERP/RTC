namespace Forms.Sale.EmployeeSaleManager
{
    partial class frmEmployeeSaleManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeSaleManager));
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlGroupSale = new DevExpress.XtraTreeList.TreeList();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEmployeeTeamName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTeamID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.coltreeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddTeam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateTeam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteTeam = new System.Windows.Forms.ToolStripButton();
            this.grdDetails = new DevExpress.XtraGrid.GridControl();
            this.grvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmpID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeamCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmpFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmpTeamID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeamName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colParentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddEmp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteEmp = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlGroupSale)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetails)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Sửa";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Xóa";
            this.barButtonItem3.Id = 4;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "THÊM";
            this.barButtonItem4.Id = 5;
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "SỬA";
            this.barButtonItem5.Id = 6;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "XÓA";
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Id = 8;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(397, 127);
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tlGroupSale);
            this.splitContainerControl1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdDetails);
            this.splitContainerControl1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1046, 585);
            this.splitContainerControl1.SplitterPosition = 493;
            this.splitContainerControl1.TabIndex = 62;
            // 
            // tlGroupSale
            // 
            this.tlGroupSale.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlGroupSale.Appearance.HeaderPanel.Options.UseFont = true;
            this.tlGroupSale.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.tlGroupSale.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.tlGroupSale.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlGroupSale.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tlGroupSale.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tlGroupSale.ColumnPanelRowHeight = 40;
            this.tlGroupSale.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colID,
            this.colEmployeeTeamName,
            this.colTeamID,
            this.coltreeListColumn4,
            this.colCode});
            this.tlGroupSale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlGroupSale.Location = new System.Drawing.Point(0, 55);
            this.tlGroupSale.Name = "tlGroupSale";
            this.tlGroupSale.OptionsBehavior.Editable = false;
            this.tlGroupSale.OptionsBehavior.PopulateServiceColumns = true;
            this.tlGroupSale.OptionsDragAndDrop.AcceptOuterNodes = true;
            this.tlGroupSale.OptionsDragAndDrop.CanCloneNodesOnDrop = true;
            this.tlGroupSale.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
            this.tlGroupSale.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlGroupSale.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.tlGroupSale.Size = new System.Drawing.Size(493, 530);
            this.tlGroupSale.TabIndex = 61;
            this.tlGroupSale.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeList;
            this.tlGroupSale.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlGroupSale_FocusedNodeChanged);
            this.tlGroupSale.DoubleClick += new System.EventHandler(this.tlGroupSale_DoubleClick);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colEmployeeTeamName
            // 
            this.colEmployeeTeamName.Caption = "Team/ Chức vụ";
            this.colEmployeeTeamName.FieldName = "Name";
            this.colEmployeeTeamName.Name = "colEmployeeTeamName";
            this.colEmployeeTeamName.Visible = true;
            this.colEmployeeTeamName.VisibleIndex = 1;
            this.colEmployeeTeamName.Width = 332;
            // 
            // colTeamID
            // 
            this.colTeamID.Caption = "ID team";
            this.colTeamID.FieldName = "EmployeeTeamSaleID";
            this.colTeamID.Name = "colTeamID";
            // 
            // coltreeListColumn4
            // 
            this.coltreeListColumn4.FieldName = "ParentID";
            this.coltreeListColumn4.Name = "coltreeListColumn4";
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã team";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 136;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddTeam,
            this.toolStripSeparator3,
            this.btnUpdateTeam,
            this.toolStripSeparator1,
            this.btnDeleteTeam});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(493, 55);
            this.toolStrip1.TabIndex = 60;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddTeam
            // 
            this.btnAddTeam.AutoSize = false;
            this.btnAddTeam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTeam.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAddTeam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddTeam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddTeam.Name = "btnAddTeam";
            this.btnAddTeam.Size = new System.Drawing.Size(50, 52);
            this.btnAddTeam.Tag = "frmEmployeeManager_Add";
            this.btnAddTeam.Text = "Thêm";
            this.btnAddTeam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddTeam.Click += new System.EventHandler(this.btnAddTeam_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 50);
            // 
            // btnUpdateTeam
            // 
            this.btnUpdateTeam.AutoSize = false;
            this.btnUpdateTeam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateTeam.Image = global::Forms.Properties.Resources.Edit_32x32;
            this.btnUpdateTeam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUpdateTeam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateTeam.Name = "btnUpdateTeam";
            this.btnUpdateTeam.Size = new System.Drawing.Size(50, 52);
            this.btnUpdateTeam.Tag = "frmEmployeeManager_Edit";
            this.btnUpdateTeam.Text = "Sửa";
            this.btnUpdateTeam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdateTeam.Click += new System.EventHandler(this.btnUpdateTeam_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDeleteTeam
            // 
            this.btnDeleteTeam.AutoSize = false;
            this.btnDeleteTeam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteTeam.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDeleteTeam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteTeam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteTeam.Name = "btnDeleteTeam";
            this.btnDeleteTeam.Size = new System.Drawing.Size(50, 52);
            this.btnDeleteTeam.Tag = "frmEmployeeManager_Del";
            this.btnDeleteTeam.Text = "Xóa";
            this.btnDeleteTeam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteTeam.Click += new System.EventHandler(this.btnDeleteTeam_Click);
            // 
            // grdDetails
            // 
            this.grdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetails.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdDetails.Location = new System.Drawing.Point(0, 55);
            this.grdDetails.MainView = this.grvDetails;
            this.grdDetails.Margin = new System.Windows.Forms.Padding(2);
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.Size = new System.Drawing.Size(543, 530);
            this.grdDetails.TabIndex = 0;
            this.grdDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDetails});
            // 
            // grvDetails
            // 
            this.grvDetails.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDetails.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvDetails.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDetails.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDetails.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDetails.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDetails.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDetails.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDetails.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDetails.Appearance.Row.Options.UseFont = true;
            this.grvDetails.Appearance.Row.Options.UseTextOptions = true;
            this.grvDetails.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDetails.ColumnPanelRowHeight = 40;
            this.grvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmpID,
            this.colTeamCode,
            this.colEmpFullName,
            this.colEmpTeamID,
            this.colTeamName,
            this.colParentID,
            this.colEmpCode,
            this.gridColumn1});
            this.grvDetails.DetailHeight = 284;
            this.grvDetails.GridControl = this.grdDetails;
            this.grvDetails.GroupCount = 1;
            this.grvDetails.LevelIndent = 10;
            this.grvDetails.Name = "grvDetails";
            this.grvDetails.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvDetails.OptionsBehavior.Editable = false;
            this.grvDetails.OptionsBehavior.ReadOnly = true;
            this.grvDetails.OptionsView.ShowAutoFilterRow = true;
            this.grvDetails.OptionsView.ShowGroupPanel = false;
            this.grvDetails.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTeamName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colEmpID
            // 
            this.colEmpID.Caption = "ID";
            this.colEmpID.FieldName = "ID";
            this.colEmpID.MinWidth = 19;
            this.colEmpID.Name = "colEmpID";
            this.colEmpID.Width = 70;
            // 
            // colTeamCode
            // 
            this.colTeamCode.Caption = "Mã chức vụ";
            this.colTeamCode.FieldName = "TeamCode";
            this.colTeamCode.MinWidth = 19;
            this.colTeamCode.Name = "colTeamCode";
            this.colTeamCode.Width = 70;
            // 
            // colEmpFullName
            // 
            this.colEmpFullName.Caption = "Tên nhân viên";
            this.colEmpFullName.FieldName = "FullName";
            this.colEmpFullName.MinWidth = 19;
            this.colEmpFullName.Name = "colEmpFullName";
            this.colEmpFullName.Visible = true;
            this.colEmpFullName.VisibleIndex = 1;
            this.colEmpFullName.Width = 485;
            // 
            // colEmpTeamID
            // 
            this.colEmpTeamID.Caption = "ID chức vụ";
            this.colEmpTeamID.FieldName = "EmployeeTeamSaleID";
            this.colEmpTeamID.MinWidth = 19;
            this.colEmpTeamID.Name = "colEmpTeamID";
            this.colEmpTeamID.Width = 70;
            // 
            // colTeamName
            // 
            this.colTeamName.Caption = "Chức vụ";
            this.colTeamName.FieldName = "TeamName";
            this.colTeamName.Name = "colTeamName";
            this.colTeamName.Visible = true;
            this.colTeamName.VisibleIndex = 0;
            // 
            // colParentID
            // 
            this.colParentID.Caption = "ParentID";
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            // 
            // colEmpCode
            // 
            this.colEmpCode.Caption = "Mã nhân viên";
            this.colEmpCode.FieldName = "EmployeeCode";
            this.colEmpCode.Name = "colEmpCode";
            this.colEmpCode.Visible = true;
            this.colEmpCode.VisibleIndex = 0;
            this.colEmpCode.Width = 200;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "LinkID";
            this.gridColumn1.FieldName = "LinkID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddEmp,
            this.toolStripSeparator2,
            this.btnDeleteEmp});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(543, 55);
            this.toolStrip2.TabIndex = 59;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddEmp
            // 
            this.btnAddEmp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEmp.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAddEmp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddEmp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddEmp.Name = "btnAddEmp";
            this.btnAddEmp.Size = new System.Drawing.Size(45, 52);
            this.btnAddEmp.Tag = "frmEmployeeManager_Add";
            this.btnAddEmp.Text = "Thêm";
            this.btnAddEmp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddEmp.Click += new System.EventHandler(this.btnAddEmp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDeleteEmp
            // 
            this.btnDeleteEmp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteEmp.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDeleteEmp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteEmp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteEmp.Name = "btnDeleteEmp";
            this.btnDeleteEmp.Size = new System.Drawing.Size(36, 52);
            this.btnDeleteEmp.Tag = "frmEmployeeManager_Del";
            this.btnDeleteEmp.Text = "Xóa";
            this.btnDeleteEmp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteEmp.Click += new System.EventHandler(this.btnDeleteEmp_Click);
            // 
            // frmEmployeeSaleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 585);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmEmployeeSaleManager";
            this.Text = "QUẢN LÝ NHÂN VIÊN SALE";
            this.Load += new System.EventHandler(this.frmEmployeeSaleManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            this.splitContainerControl1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlGroupSale)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetails)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddTeam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnUpdateTeam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDeleteTeam;
        private DevExpress.XtraGrid.GridControl grdDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpID;
        private DevExpress.XtraGrid.Columns.GridColumn colTeamCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpTeamID;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddEmp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDeleteEmp;
        private DevExpress.XtraTreeList.TreeList tlGroupSale;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEmployeeTeamName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTeamID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn coltreeListColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn colTeamName;
        private DevExpress.XtraGrid.Columns.GridColumn colParentID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
    }
}