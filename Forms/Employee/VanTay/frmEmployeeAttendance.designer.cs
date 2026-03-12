
namespace BMS
{
    partial class frmEmployeeAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeAttendance));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDChamCongMoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToChuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChucDanh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttendanceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDayWeek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInterval = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLateRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsEarlyRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvertime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBussiness = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoFingerprint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnLeave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWFH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLunch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsEarly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHolidayDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsOverEarly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsOverLate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeLate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeEarly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurricular = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoFingerprintReal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.stackPanel2 = new DevExpress.Utils.Layout.StackPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel2)).BeginInit();
            this.stackPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExcel,
            this.toolStripSeparator5,
            this.btnImportExcel,
            this.toolStripSeparator1,
            this.btnDelete});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1443, 43);
            this.mnuMenu.TabIndex = 30;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(83, 36);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(90, 36);
            this.btnImportExcel.Tag = "frmAsset_HRManager";
            this.btnImportExcel.Text = "Nhập Excel";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Forms.Properties.Resources.Trash_16x16;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(38, 36);
            this.btnDelete.Tag = "frmAsset_HRManager";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 82);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemCheckEdit1});
            this.grdData.Size = new System.Drawing.Size(1443, 631);
            this.grdData.TabIndex = 31;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvData.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            this.grvData.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.AppearancePrint.Row.Options.UseFont = true;
            this.grvData.AppearancePrint.Row.Options.UseTextOptions = true;
            this.grvData.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.AutoFillColumn = this.colFullName;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSTT,
            this.colIDChamCongMoi,
            this.colFullName,
            this.colToChuc,
            this.colChucDanh,
            this.colAttendanceDate,
            this.colDayWeek,
            this.colInterval,
            this.colCheckIn,
            this.colCheckOut,
            this.colIsLateRegister,
            this.colIsEarlyRegister,
            this.colOvertime,
            this.colBussiness,
            this.colNoFingerprint,
            this.colOnLeave,
            this.colWFH,
            this.colIsLunch,
            this.colTotalDay,
            this.colDepartmentName,
            this.colIsLate,
            this.colIsEarly,
            this.colHolidayDay,
            this.colDepartmentID,
            this.colIsOverEarly,
            this.colIsOverLate,
            this.colDepartmentSTT,
            this.colTypeLate,
            this.colTypeEarly,
            this.colCurricular,
            this.colNoFingerprintReal,
            this.colCode});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "STT", null, "({0:n0})")});
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsPrint.AutoWidth = false;
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvData.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvData_RowCellStyle);
            this.grvData.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvData_CustomColumnDisplayText);
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 4;
            this.colFullName.Width = 179;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.Caption = "STT";
            this.colSTT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSTT.FieldName = "STT";
            this.colSTT.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colSTT.Name = "colSTT";
            this.colSTT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "STT", "{0}")});
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 1;
            this.colSTT.Width = 72;
            // 
            // colIDChamCongMoi
            // 
            this.colIDChamCongMoi.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIDChamCongMoi.AppearanceCell.Options.UseFont = true;
            this.colIDChamCongMoi.AppearanceCell.Options.UseTextOptions = true;
            this.colIDChamCongMoi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDChamCongMoi.Caption = "ID Người";
            this.colIDChamCongMoi.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colIDChamCongMoi.FieldName = "IDChamCongMoi";
            this.colIDChamCongMoi.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colIDChamCongMoi.Name = "colIDChamCongMoi";
            this.colIDChamCongMoi.Visible = true;
            this.colIDChamCongMoi.VisibleIndex = 2;
            this.colIDChamCongMoi.Width = 83;
            // 
            // colToChuc
            // 
            this.colToChuc.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colToChuc.AppearanceCell.Options.UseFont = true;
            this.colToChuc.AppearanceCell.Options.UseTextOptions = true;
            this.colToChuc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colToChuc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colToChuc.Caption = "Tổ chức";
            this.colToChuc.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colToChuc.FieldName = "ToChuc";
            this.colToChuc.Name = "colToChuc";
            this.colToChuc.Visible = true;
            this.colToChuc.VisibleIndex = 5;
            this.colToChuc.Width = 80;
            // 
            // colChucDanh
            // 
            this.colChucDanh.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colChucDanh.AppearanceCell.Options.UseFont = true;
            this.colChucDanh.AppearanceCell.Options.UseTextOptions = true;
            this.colChucDanh.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colChucDanh.Caption = "Chức danh";
            this.colChucDanh.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colChucDanh.FieldName = "ChucDanh";
            this.colChucDanh.Name = "colChucDanh";
            this.colChucDanh.Width = 74;
            // 
            // colAttendanceDate
            // 
            this.colAttendanceDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colAttendanceDate.AppearanceCell.Options.UseFont = true;
            this.colAttendanceDate.AppearanceCell.Options.UseTextOptions = true;
            this.colAttendanceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAttendanceDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAttendanceDate.Caption = "Ngày";
            this.colAttendanceDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAttendanceDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colAttendanceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colAttendanceDate.FieldName = "AttendanceDate";
            this.colAttendanceDate.Name = "colAttendanceDate";
            this.colAttendanceDate.Visible = true;
            this.colAttendanceDate.VisibleIndex = 6;
            this.colAttendanceDate.Width = 96;
            // 
            // colDayWeek
            // 
            this.colDayWeek.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDayWeek.AppearanceCell.Options.UseFont = true;
            this.colDayWeek.AppearanceCell.Options.UseTextOptions = true;
            this.colDayWeek.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDayWeek.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDayWeek.Caption = "Ngày trong tuần";
            this.colDayWeek.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDayWeek.FieldName = "DayWeek";
            this.colDayWeek.Name = "colDayWeek";
            this.colDayWeek.Visible = true;
            this.colDayWeek.VisibleIndex = 7;
            this.colDayWeek.Width = 98;
            // 
            // colInterval
            // 
            this.colInterval.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colInterval.AppearanceCell.Options.UseFont = true;
            this.colInterval.AppearanceCell.Options.UseTextOptions = true;
            this.colInterval.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInterval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colInterval.Caption = "Khoảng thời gian";
            this.colInterval.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colInterval.FieldName = "Interval";
            this.colInterval.Name = "colInterval";
            this.colInterval.Visible = true;
            this.colInterval.VisibleIndex = 8;
            this.colInterval.Width = 178;
            // 
            // colCheckIn
            // 
            this.colCheckIn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCheckIn.AppearanceCell.Options.UseFont = true;
            this.colCheckIn.AppearanceCell.Options.UseTextOptions = true;
            this.colCheckIn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheckIn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCheckIn.Caption = "Giờ vào";
            this.colCheckIn.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCheckIn.FieldName = "CheckIn";
            this.colCheckIn.Name = "colCheckIn";
            this.colCheckIn.Visible = true;
            this.colCheckIn.VisibleIndex = 9;
            this.colCheckIn.Width = 120;
            // 
            // colCheckOut
            // 
            this.colCheckOut.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCheckOut.AppearanceCell.Options.UseFont = true;
            this.colCheckOut.AppearanceCell.Options.UseTextOptions = true;
            this.colCheckOut.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheckOut.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCheckOut.Caption = "Giờ ra";
            this.colCheckOut.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCheckOut.FieldName = "CheckOut";
            this.colCheckOut.Name = "colCheckOut";
            this.colCheckOut.Visible = true;
            this.colCheckOut.VisibleIndex = 10;
            this.colCheckOut.Width = 120;
            // 
            // colIsLateRegister
            // 
            this.colIsLateRegister.AppearanceCell.Options.UseTextOptions = true;
            this.colIsLateRegister.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsLateRegister.Caption = "Đi muộn";
            this.colIsLateRegister.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsLateRegister.FieldName = "IsLateRegister";
            this.colIsLateRegister.Name = "colIsLateRegister";
            this.colIsLateRegister.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IsLateRegister", "{0:n0}")});
            this.colIsLateRegister.Visible = true;
            this.colIsLateRegister.VisibleIndex = 11;
            this.colIsLateRegister.Width = 120;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.DisplayValueChecked = "x";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = " ";
            this.repositoryItemCheckEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colIsEarlyRegister
            // 
            this.colIsEarlyRegister.AppearanceCell.Options.UseTextOptions = true;
            this.colIsEarlyRegister.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsEarlyRegister.Caption = "Về sớm";
            this.colIsEarlyRegister.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsEarlyRegister.FieldName = "IsEarlyRegister";
            this.colIsEarlyRegister.Name = "colIsEarlyRegister";
            this.colIsEarlyRegister.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IsEarlyRegister", "{0:n0}")});
            this.colIsEarlyRegister.Visible = true;
            this.colIsEarlyRegister.VisibleIndex = 12;
            this.colIsEarlyRegister.Width = 120;
            // 
            // colOvertime
            // 
            this.colOvertime.AppearanceCell.Options.UseTextOptions = true;
            this.colOvertime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOvertime.Caption = "Làm thêm";
            this.colOvertime.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOvertime.FieldName = "Overtime";
            this.colOvertime.Name = "colOvertime";
            this.colOvertime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Overtime", "{0:n0}")});
            this.colOvertime.Visible = true;
            this.colOvertime.VisibleIndex = 13;
            this.colOvertime.Width = 120;
            // 
            // colBussiness
            // 
            this.colBussiness.AppearanceCell.Options.UseTextOptions = true;
            this.colBussiness.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBussiness.Caption = "Công tác";
            this.colBussiness.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colBussiness.FieldName = "Bussiness";
            this.colBussiness.Name = "colBussiness";
            this.colBussiness.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Bussiness", "{0:n0}")});
            this.colBussiness.Visible = true;
            this.colBussiness.VisibleIndex = 14;
            this.colBussiness.Width = 120;
            // 
            // colNoFingerprint
            // 
            this.colNoFingerprint.AppearanceCell.Options.UseTextOptions = true;
            this.colNoFingerprint.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoFingerprint.Caption = "Đk Quên vân tay";
            this.colNoFingerprint.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colNoFingerprint.FieldName = "NoFingerprint";
            this.colNoFingerprint.Name = "colNoFingerprint";
            this.colNoFingerprint.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NoFingerprint", "{0:n0}")});
            this.colNoFingerprint.Visible = true;
            this.colNoFingerprint.VisibleIndex = 15;
            this.colNoFingerprint.Width = 120;
            // 
            // colOnLeave
            // 
            this.colOnLeave.AppearanceCell.Options.UseTextOptions = true;
            this.colOnLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOnLeave.Caption = "Nghỉ";
            this.colOnLeave.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOnLeave.FieldName = "OnLeave";
            this.colOnLeave.Name = "colOnLeave";
            this.colOnLeave.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OnLeave", "{0:n0}")});
            this.colOnLeave.Visible = true;
            this.colOnLeave.VisibleIndex = 16;
            this.colOnLeave.Width = 120;
            // 
            // colWFH
            // 
            this.colWFH.AppearanceCell.Options.UseTextOptions = true;
            this.colWFH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWFH.Caption = "WFH";
            this.colWFH.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colWFH.FieldName = "WFH";
            this.colWFH.Name = "colWFH";
            this.colWFH.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WFH", "{0:n0}")});
            this.colWFH.Visible = true;
            this.colWFH.VisibleIndex = 17;
            this.colWFH.Width = 120;
            // 
            // colIsLunch
            // 
            this.colIsLunch.Caption = "Cơm ca";
            this.colIsLunch.FieldName = "IsLunch";
            this.colIsLunch.Name = "colIsLunch";
            this.colIsLunch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IsLunch", "{0:0.##}")});
            this.colIsLunch.Width = 70;
            // 
            // colTotalDay
            // 
            this.colTotalDay.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colTotalDay.AppearanceCell.Options.UseFont = true;
            this.colTotalDay.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDay.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDay.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDay.Caption = "Tổng công";
            this.colTotalDay.FieldName = "TotalDay";
            this.colTotalDay.Name = "colTotalDay";
            this.colTotalDay.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalDay", "{0:0.##}")});
            this.colTotalDay.Width = 70;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.AppearanceCell.ForeColor = System.Drawing.Color.White;
            this.colDepartmentName.AppearanceCell.Options.UseForeColor = true;
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.FieldNameSortGroup = "DepartmentSTT";
            this.colDepartmentName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 0;
            this.colDepartmentName.Width = 20;
            // 
            // colIsLate
            // 
            this.colIsLate.FieldName = "IsLate";
            this.colIsLate.Name = "colIsLate";
            this.colIsLate.Width = 120;
            // 
            // colIsEarly
            // 
            this.colIsEarly.FieldName = "IsEarly";
            this.colIsEarly.Name = "colIsEarly";
            this.colIsEarly.Width = 120;
            // 
            // colHolidayDay
            // 
            this.colHolidayDay.FieldName = "HolidayDay";
            this.colHolidayDay.Name = "colHolidayDay";
            // 
            // colDepartmentID
            // 
            this.colDepartmentID.FieldName = "DepartmentID";
            this.colDepartmentID.Name = "colDepartmentID";
            // 
            // colIsOverEarly
            // 
            this.colIsOverEarly.FieldName = "IsOverEarly";
            this.colIsOverEarly.Name = "colIsOverEarly";
            this.colIsOverEarly.Width = 120;
            // 
            // colIsOverLate
            // 
            this.colIsOverLate.FieldName = "IsOverLate";
            this.colIsOverLate.Name = "colIsOverLate";
            this.colIsOverLate.Width = 120;
            // 
            // colDepartmentSTT
            // 
            this.colDepartmentSTT.FieldName = "DepartmentSTT";
            this.colDepartmentSTT.Name = "colDepartmentSTT";
            // 
            // colTypeLate
            // 
            this.colTypeLate.FieldName = "TypeLate";
            this.colTypeLate.Name = "colTypeLate";
            // 
            // colTypeEarly
            // 
            this.colTypeEarly.FieldName = "TypeEarly";
            this.colTypeEarly.Name = "colTypeEarly";
            // 
            // colCurricular
            // 
            this.colCurricular.AppearanceCell.Options.UseTextOptions = true;
            this.colCurricular.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurricular.Caption = "Ngoại khóa";
            this.colCurricular.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCurricular.FieldName = "Curricular";
            this.colCurricular.Name = "colCurricular";
            this.colCurricular.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Curricular", "{0:n0}")});
            this.colCurricular.Visible = true;
            this.colCurricular.VisibleIndex = 18;
            this.colCurricular.Width = 120;
            // 
            // colNoFingerprintReal
            // 
            this.colNoFingerprintReal.AppearanceCell.Options.UseTextOptions = true;
            this.colNoFingerprintReal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoFingerprintReal.Caption = "Quên vân tay thực tế";
            this.colNoFingerprintReal.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colNoFingerprintReal.FieldName = "IsNoFinger";
            this.colNoFingerprintReal.Name = "colNoFingerprintReal";
            this.colNoFingerprintReal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IsNoFinger", "{0:n0}")});
            this.colNoFingerprintReal.Visible = true;
            this.colNoFingerprintReal.VisibleIndex = 19;
            this.colNoFingerprintReal.Width = 120;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 3;
            this.colCode.Width = 121;
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.CustomFormat = "dd/MM/yyyy";
            this.dtpDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateStart.Location = new System.Drawing.Point(32, 6);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(107, 22);
            this.dtpDateStart.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(900, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(1186, 4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(93, 27);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Tìm kiếm";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(838, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Từ khoá";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Từ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.label1);
            this.stackPanel1.Controls.Add(this.dtpDateStart);
            this.stackPanel1.Controls.Add(this.label4);
            this.stackPanel1.Controls.Add(this.dtpDateEnd);
            this.stackPanel1.Controls.Add(this.label2);
            this.stackPanel1.Controls.Add(this.cboDepartment);
            this.stackPanel1.Controls.Add(this.label3);
            this.stackPanel1.Controls.Add(this.cboEmployee);
            this.stackPanel1.Controls.Add(this.label5);
            this.stackPanel1.Controls.Add(this.txtSearch);
            this.stackPanel1.Controls.Add(this.btnLoad);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.stackPanel1.Location = new System.Drawing.Point(2, 2);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(1296, 35);
            this.stackPanel1.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(145, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Đến";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.CustomFormat = "dd/MM/yyyy";
            this.dtpDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateEnd.Location = new System.Drawing.Point(182, 6);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(107, 22);
            this.dtpDateEnd.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(295, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Phòng ban";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDepartment
            // 
            this.cboDepartment.Location = new System.Drawing.Point(373, 6);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDepartment.Size = new System.Drawing.Size(190, 22);
            this.cboDepartment.TabIndex = 11;
            this.cboDepartment.EditValueChanged += new System.EventHandler(this.cboDepartment_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 40;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn8.Caption = "Phòng ban";
            this.gridColumn8.FieldName = "Name";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(569, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nhân viên";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmployee
            // 
            this.cboEmployee.Location = new System.Drawing.Point(642, 6);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployee.Properties.Appearance.Options.UseFont = true;
            this.cboEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployee.Properties.NullText = "";
            this.cboEmployee.Properties.PopupView = this.gridView1;
            this.cboEmployee.Size = new System.Drawing.Size(190, 22);
            this.cboEmployee.TabIndex = 11;
            this.cboEmployee.EditValueChanged += new System.EventHandler(this.cboEmployee_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 40;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn1});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn9.Caption = "ID";
            this.gridColumn9.FieldName = "ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn10.Caption = "Mã nhân viên";
            this.gridColumn10.FieldName = "Code";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 436;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn11.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn11.Caption = "Tên nhân viên";
            this.gridColumn11.FieldName = "FullName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 1179;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Phòng ban";
            this.gridColumn1.FieldName = "DepartmentName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // stackPanel2
            // 
            this.stackPanel2.Controls.Add(this.label6);
            this.stackPanel2.Controls.Add(this.label7);
            this.stackPanel2.Controls.Add(this.label8);
            this.stackPanel2.Controls.Add(this.label9);
            this.stackPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.stackPanel2.Location = new System.Drawing.Point(1115, 2);
            this.stackPanel2.Name = "stackPanel2";
            this.stackPanel2.Size = new System.Drawing.Size(326, 35);
            this.stackPanel2.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Red;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(3, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 19);
            this.label6.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Đi muộn \\ Về sớm";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Yellow;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(141, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 19);
            this.label8.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(183, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Đi muộn \\ Về sớm (>1h)";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.stackPanel1);
            this.panelControl1.Controls.Add(this.stackPanel2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 43);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1443, 39);
            this.panelControl1.TabIndex = 34;
            // 
            // frmEmployeeAttendance
            // 
            this.AcceptButton = this.btnLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 713);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEmployeeAttendance";
            this.Text = "VÂN TAY";
            this.Load += new System.EventHandler(this.frmEmployeeAttendance_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel2)).EndInit();
            this.stackPanel2.ResumeLayout(false);
            this.stackPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colIDChamCongMoi;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colToChuc;
        private DevExpress.XtraGrid.Columns.GridColumn colChucDanh;
        private DevExpress.XtraGrid.Columns.GridColumn colAttendanceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDayWeek;
        private DevExpress.XtraGrid.Columns.GridColumn colInterval;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckIn;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckOut;
        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLateRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colIsEarlyRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colOvertime;
        private DevExpress.XtraGrid.Columns.GridColumn colBussiness;
        private DevExpress.XtraGrid.Columns.GridColumn colNoFingerprint;
        private DevExpress.XtraGrid.Columns.GridColumn colOnLeave;
        private DevExpress.XtraGrid.Columns.GridColumn colWFH;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLunch;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDay;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsEarly;
        private DevExpress.XtraGrid.Columns.GridColumn colHolidayDay;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentID;
        private DevExpress.XtraGrid.Columns.GridColumn colIsOverEarly;
        private DevExpress.XtraGrid.Columns.GridColumn colIsOverLate;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentSTT;
        private DevExpress.Utils.Layout.StackPanel stackPanel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeLate;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeEarly;
        private DevExpress.XtraGrid.Columns.GridColumn colCurricular;
        private DevExpress.XtraGrid.Columns.GridColumn colNoFingerprintReal;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
    }
}