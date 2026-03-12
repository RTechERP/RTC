
namespace Forms.Employee
{
    partial class frmteamtrees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmteamtrees));
            this.btnAddStaff = new System.Windows.Forms.Button();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.btnDeleteStaff = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboUser = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDUserTeamLink = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvUserTeamLink = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTeam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdUserTeamLink = new DevExpress.XtraGrid.GridControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeTeam = new DevExpress.XtraTreeList.TreeList();
            this.colTreeID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDepartmentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLeader = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTeamType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUserTeamLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserTeamLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeTeam)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddStaff
            // 
            this.btnAddStaff.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddStaff.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAddStaff.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnAddStaff.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddStaff.Image = global::Forms.Properties.Resources.icons8_plus_math_16;
            this.btnAddStaff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddStaff.Location = new System.Drawing.Point(47, 7);
            this.btnAddStaff.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddStaff.Name = "btnAddStaff";
            this.btnAddStaff.Padding = new System.Windows.Forms.Padding(2);
            this.btnAddStaff.Size = new System.Drawing.Size(90, 36);
            this.btnAddStaff.TabIndex = 2;
            this.btnAddStaff.Tag = "frmteamtrees_AddStaff";
            this.btnAddStaff.Text = "Thêm NV";
            this.btnAddStaff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddStaff.UseVisualStyleBackColor = false;
            this.btnAddStaff.Click += new System.EventHandler(this.btnAddStaff_Click_1);
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.btnDeleteStaff);
            this.stackPanel1.Controls.Add(this.btnAddStaff);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            this.stackPanel1.Location = new System.Drawing.Point(348, 0);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(237, 50);
            this.stackPanel1.TabIndex = 0;
            // 
            // btnDeleteStaff
            // 
            this.btnDeleteStaff.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDeleteStaff.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnDeleteStaff.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteStaff.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteStaff.Image = global::Forms.Properties.Resources.icons8_close_16;
            this.btnDeleteStaff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteStaff.Location = new System.Drawing.Point(142, 7);
            this.btnDeleteStaff.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnDeleteStaff.Name = "btnDeleteStaff";
            this.btnDeleteStaff.Padding = new System.Windows.Forms.Padding(2);
            this.btnDeleteStaff.Size = new System.Drawing.Size(90, 36);
            this.btnDeleteStaff.TabIndex = 3;
            this.btnDeleteStaff.Tag = "frmteamtrees_DeleteStaff";
            this.btnDeleteStaff.Text = "Xóa NV";
            this.btnDeleteStaff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteStaff.UseVisualStyleBackColor = false;
            this.btnDeleteStaff.Click += new System.EventHandler(this.btnDeleteStaff_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.stackPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 50);
            this.panel1.TabIndex = 19;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Họ tên";
            this.gridColumn2.FieldName = "FullName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // cboUser
            // 
            this.cboUser.AutoHeight = false;
            this.cboUser.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUser.Name = "cboUser";
            this.cboUser.NullText = "";
            this.cboUser.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // colCode
            // 
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCode.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colCode.ShowUnboundExpressionMenu = true;
            this.colCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Code", "{0}")});
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 211;
            // 
            // colUserID
            // 
            this.colUserID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colUserID.AppearanceHeader.Options.UseBackColor = true;
            this.colUserID.AppearanceHeader.Options.UseFont = true;
            this.colUserID.AppearanceHeader.Options.UseForeColor = true;
            this.colUserID.AppearanceHeader.Options.UseTextOptions = true;
            this.colUserID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUserID.Caption = "Tên nhân viên";
            this.colUserID.FieldName = "FullName";
            this.colUserID.Name = "colUserID";
            this.colUserID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colUserID.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colUserID.ShowUnboundExpressionMenu = true;
            this.colUserID.Visible = true;
            this.colUserID.VisibleIndex = 1;
            this.colUserID.Width = 459;
            // 
            // colIDUserTeamLink
            // 
            this.colIDUserTeamLink.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colIDUserTeamLink.AppearanceHeader.Options.UseBackColor = true;
            this.colIDUserTeamLink.AppearanceHeader.Options.UseFont = true;
            this.colIDUserTeamLink.AppearanceHeader.Options.UseForeColor = true;
            this.colIDUserTeamLink.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDUserTeamLink.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDUserTeamLink.Caption = "ID";
            this.colIDUserTeamLink.FieldName = "ID";
            this.colIDUserTeamLink.Name = "colIDUserTeamLink";
            this.colIDUserTeamLink.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ID", "{0}")});
            // 
            // grvUserTeamLink
            // 
            this.grvUserTeamLink.ColumnPanelRowHeight = 30;
            this.grvUserTeamLink.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDUserTeamLink,
            this.colUserID,
            this.colCode,
            this.colTeam});
            this.grvUserTeamLink.GridControl = this.grdUserTeamLink;
            this.grvUserTeamLink.GroupCount = 1;
            this.grvUserTeamLink.Name = "grvUserTeamLink";
            this.grvUserTeamLink.OptionsBehavior.Editable = false;
            this.grvUserTeamLink.OptionsBehavior.ReadOnly = true;
            this.grvUserTeamLink.OptionsCustomization.AllowColumnMoving = false;
            this.grvUserTeamLink.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.grvUserTeamLink.OptionsView.ShowFooter = true;
            this.grvUserTeamLink.OptionsView.ShowGroupPanel = false;
            this.grvUserTeamLink.OptionsView.ShowIndicator = false;
            this.grvUserTeamLink.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTeam, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colTeam
            // 
            this.colTeam.Caption = "Team";
            this.colTeam.FieldName = "Team";
            this.colTeam.Name = "colTeam";
            this.colTeam.Visible = true;
            this.colTeam.VisibleIndex = 2;
            // 
            // grdUserTeamLink
            // 
            this.grdUserTeamLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserTeamLink.Location = new System.Drawing.Point(0, 50);
            this.grdUserTeamLink.MainView = this.grvUserTeamLink;
            this.grdUserTeamLink.Name = "grdUserTeamLink";
            this.grdUserTeamLink.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboUser});
            this.grdUserTeamLink.Size = new System.Drawing.Size(585, 570);
            this.grdUserTeamLink.TabIndex = 20;
            this.grdUserTeamLink.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvUserTeamLink});
            this.grdUserTeamLink.Load += new System.EventHandler(this.grdUserTeamLink_Load_1);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 45);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.cboDepartment);
            this.splitContainerControl1.Panel1.Controls.Add(this.label1);
            this.splitContainerControl1.Panel1.Controls.Add(this.treeTeam);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdUserTeamLink);
            this.splitContainerControl1.Panel2.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1375, 620);
            this.splitContainerControl1.SplitterPosition = 780;
            this.splitContainerControl1.TabIndex = 21;
            this.splitContainerControl1.SizeChanged += new System.EventHandler(this.frmTeam_SizeChanged);
            // 
            // cboDepartment
            // 
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(116, 16);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(296, 21);
            this.cboDepartment.TabIndex = 2;
            this.cboDepartment.SelectedIndexChanged += new System.EventHandler(this.cboDepartment_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Phòng ban";
            // 
            // treeTeam
            // 
            this.treeTeam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeTeam.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Black;
            this.treeTeam.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.treeTeam.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.treeTeam.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.treeTeam.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeTeam.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeTeam.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeTeam.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeTeam.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTreeID,
            this.colDepartmentID,
            this.colName,
            this.colParentID,
            this.colLeader,
            this.colTeamType});
            this.treeTeam.CustomizationFormBounds = new System.Drawing.Rectangle(583, 272, 252, 266);
            this.treeTeam.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.treeTeam.Location = new System.Drawing.Point(3, 50);
            this.treeTeam.Name = "treeTeam";
            this.treeTeam.OptionsBehavior.Editable = false;
            this.treeTeam.OptionsView.ShowSummaryFooter = true;
            this.treeTeam.Size = new System.Drawing.Size(775, 570);
            this.treeTeam.TabIndex = 0;
            this.treeTeam.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeTeam_NodeCellStyle);
            this.treeTeam.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeTeam_FocusedNodeChanged);
            this.treeTeam.DoubleClick += new System.EventHandler(this.treeTeam_DoubleClick);
            // 
            // colTreeID
            // 
            this.colTreeID.Caption = "ID";
            this.colTreeID.FieldName = "ID";
            this.colTreeID.Name = "colTreeID";
            // 
            // colDepartmentID
            // 
            this.colDepartmentID.Caption = "DepartmentID";
            this.colDepartmentID.FieldName = "DepartmentID";
            this.colDepartmentID.Name = "colDepartmentID";
            this.colDepartmentID.OptionsColumn.AllowEdit = false;
            this.colDepartmentID.OptionsColumn.AllowFocus = false;
            // 
            // colName
            // 
            this.colName.Caption = "Tên Nhóm";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 250;
            // 
            // colParentID
            // 
            this.colParentID.Caption = "ParentID";
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            // 
            // colLeader
            // 
            this.colLeader.Caption = "Trưởng Nhóm";
            this.colLeader.FieldName = "Leader";
            this.colLeader.Name = "colLeader";
            this.colLeader.Visible = true;
            this.colLeader.VisibleIndex = 1;
            this.colLeader.Width = 338;
            // 
            // colTeamType
            // 
            this.colTeamType.Caption = "Loại";
            this.colTeamType.FieldName = "TypeName";
            this.colTeamType.Name = "colTeamType";
            this.colTeamType.Visible = true;
            this.colTeamType.VisibleIndex = 2;
            this.colTeamType.Width = 162;
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(40, 33);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            this.toolStripSeparator3.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 40);
            this.btnDelete.Tag = "frmteamtrees_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 40);
            this.btnEdit.Tag = "frmteamtrees_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 40);
            this.btnNew.Tag = "frmteamtrees_new";
            this.btnNew.Text = "&Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click_1);
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator4,
            this.btnReset,
            this.btnExcel,
            this.toolStripSeparator3});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1375, 45);
            this.mnuMenu.TabIndex = 20;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = false;
            this.btnReset.Image = global::Forms.Properties.Resources.refresh2_16x16;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 40);
            this.btnReset.Text = "Reset";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // frmteamtrees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 665);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmteamtrees";
            this.Text = "TEAM";
            this.Load += new System.EventHandler(this.frmteamtrees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUserTeamLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserTeamLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeTeam)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddStaff;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Button btnDeleteStaff;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboUser;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colIDUserTeamLink;
        private DevExpress.XtraGrid.Views.Grid.GridView grvUserTeamLink;
        private DevExpress.XtraGrid.GridControl grdUserTeamLink;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private DevExpress.XtraTreeList.TreeList treeTeam;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTreeID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDepartmentID;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLeader;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private System.Windows.Forms.ToolStripButton btnReset;
        private DevExpress.XtraGrid.Columns.GridColumn colTeam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTeamType;
    }
}