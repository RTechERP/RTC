
namespace BMS
{
    partial class frmGanttChartProjectItemGrid
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
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bandTitle = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandProject = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandCustomer = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandPlan = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colIDMaster = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSTT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMission = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colFullName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.bandProjectName = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandCustomerName = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandPlanDetail = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colStartDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTotalDays = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colItemLate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandNote = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colFirst = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectTypeName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboProject = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnUpdateProject = new System.Windows.Forms.Button();
            this.dtpYear = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(15, 39);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.grdData.Size = new System.Drawing.Size(1352, 646);
            this.grdData.TabIndex = 7;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSteelBlue;
            this.grvData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandTitle,
            this.bandNote});
            this.grvData.ColumnPanelRowHeight = 60;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSTT,
            this.colMission,
            this.colFullName,
            this.colStartDate,
            this.colTotalDays,
            this.colEndDate,
            this.colType,
            this.colIDMaster,
            this.colFirst,
            this.colItemLate,
            this.colProjectTypeName});
            this.grvData.CustomizationFormBounds = new System.Drawing.Rectangle(1666, 359, 254, 489);
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsCustomization.AllowBandMoving = false;
            this.grvData.OptionsCustomization.AllowBandResizing = false;
            this.grvData.OptionsCustomization.AllowColumnMoving = false;
            this.grvData.OptionsCustomization.AllowColumnResizing = false;
            this.grvData.OptionsCustomization.AllowFilter = false;
            this.grvData.OptionsCustomization.AllowSort = false;
            this.grvData.OptionsFilter.AllowFilterEditor = false;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colProjectTypeName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvData.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvData_RowCellStyle);
            this.grvData.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvData_CustomColumnDisplayText);
            // 
            // bandTitle
            // 
            this.bandTitle.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandTitle.AppearanceHeader.Options.UseFont = true;
            this.bandTitle.AppearanceHeader.Options.UseForeColor = true;
            this.bandTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.bandTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandTitle.Caption = "KẾ HOẠCH TRIỂN KHAI DỰ ÁN";
            this.bandTitle.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandProject,
            this.bandProjectName});
            this.bandTitle.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.bandTitle.Name = "bandTitle";
            this.bandTitle.OptionsBand.AllowMove = false;
            this.bandTitle.OptionsBand.AllowSize = false;
            this.bandTitle.OptionsBand.FixedWidth = true;
            this.bandTitle.RowCount = 3;
            this.bandTitle.VisibleIndex = 0;
            this.bandTitle.Width = 824;
            // 
            // bandProject
            // 
            this.bandProject.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandProject.AppearanceHeader.Options.UseFont = true;
            this.bandProject.AppearanceHeader.Options.UseForeColor = true;
            this.bandProject.Caption = "TÊN DỰ ÁN / PROJECT";
            this.bandProject.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandCustomer});
            this.bandProject.Name = "bandProject";
            this.bandProject.OptionsBand.AllowMove = false;
            this.bandProject.OptionsBand.FixedWidth = true;
            this.bandProject.RowCount = 2;
            this.bandProject.VisibleIndex = 0;
            this.bandProject.Width = 482;
            // 
            // bandCustomer
            // 
            this.bandCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandCustomer.AppearanceHeader.Options.UseFont = true;
            this.bandCustomer.AppearanceHeader.Options.UseForeColor = true;
            this.bandCustomer.Caption = "KHÁCH HÀNG / CUSTOMER";
            this.bandCustomer.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandPlan});
            this.bandCustomer.Name = "bandCustomer";
            this.bandCustomer.OptionsBand.AllowMove = false;
            this.bandCustomer.OptionsBand.FixedWidth = true;
            this.bandCustomer.RowCount = 2;
            this.bandCustomer.VisibleIndex = 0;
            this.bandCustomer.Width = 482;
            // 
            // bandPlan
            // 
            this.bandPlan.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandPlan.AppearanceHeader.Options.UseFont = true;
            this.bandPlan.AppearanceHeader.Options.UseForeColor = true;
            this.bandPlan.Caption = "KẾ HOẠCH TRIỂN KHAI";
            this.bandPlan.Columns.Add(this.colIDMaster);
            this.bandPlan.Columns.Add(this.colSTT);
            this.bandPlan.Columns.Add(this.colMission);
            this.bandPlan.Columns.Add(this.colFullName);
            this.bandPlan.Name = "bandPlan";
            this.bandPlan.OptionsBand.AllowMove = false;
            this.bandPlan.OptionsBand.AllowSize = false;
            this.bandPlan.OptionsBand.FixedWidth = true;
            this.bandPlan.RowCount = 2;
            this.bandPlan.VisibleIndex = 0;
            this.bandPlan.Width = 482;
            // 
            // colIDMaster
            // 
            this.colIDMaster.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIDMaster.AppearanceCell.Options.UseFont = true;
            this.colIDMaster.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIDMaster.AppearanceHeader.Options.UseFont = true;
            this.colIDMaster.AppearanceHeader.Options.UseForeColor = true;
            this.colIDMaster.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDMaster.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDMaster.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDMaster.Caption = "ID";
            this.colIDMaster.FieldName = "ID";
            this.colIDMaster.Name = "colIDMaster";
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSTT.AppearanceHeader.Options.UseFont = true;
            this.colSTT.AppearanceHeader.Options.UseForeColor = true;
            this.colSTT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSTT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.Caption = "STT / No";
            this.colSTT.FieldName = "STT";
            this.colSTT.MinWidth = 50;
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colSTT.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSTT.OptionsFilter.AllowAutoFilter = false;
            this.colSTT.OptionsFilter.AllowFilter = false;
            this.colSTT.Visible = true;
            this.colSTT.Width = 55;
            // 
            // colMission
            // 
            this.colMission.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMission.AppearanceCell.Options.UseFont = true;
            this.colMission.AppearanceCell.Options.UseTextOptions = true;
            this.colMission.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMission.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMission.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMission.AppearanceHeader.Options.UseFont = true;
            this.colMission.AppearanceHeader.Options.UseForeColor = true;
            this.colMission.AppearanceHeader.Options.UseTextOptions = true;
            this.colMission.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMission.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMission.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMission.Caption = "Nội dung công việc / Content";
            this.colMission.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colMission.FieldName = "Mission";
            this.colMission.MinWidth = 267;
            this.colMission.Name = "colMission";
            this.colMission.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colMission.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colMission.OptionsFilter.AllowAutoFilter = false;
            this.colMission.OptionsFilter.AllowFilter = false;
            this.colMission.Visible = true;
            this.colMission.Width = 267;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFullName.AppearanceHeader.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Options.UseForeColor = true;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.Caption = "Người phụ trách";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colFullName.FieldName = "FullName";
            this.colFullName.MinWidth = 160;
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colFullName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFullName.OptionsFilter.AllowAutoFilter = false;
            this.colFullName.OptionsFilter.AllowFilter = false;
            this.colFullName.Visible = true;
            this.colFullName.Width = 160;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // bandProjectName
            // 
            this.bandProjectName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandProjectName.AppearanceHeader.Options.UseFont = true;
            this.bandProjectName.AppearanceHeader.Options.UseForeColor = true;
            this.bandProjectName.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandCustomerName});
            this.bandProjectName.Name = "bandProjectName";
            this.bandProjectName.OptionsBand.AllowMove = false;
            this.bandProjectName.OptionsBand.FixedWidth = true;
            this.bandProjectName.RowCount = 2;
            this.bandProjectName.VisibleIndex = 1;
            this.bandProjectName.Width = 342;
            // 
            // bandCustomerName
            // 
            this.bandCustomerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandCustomerName.AppearanceHeader.Options.UseFont = true;
            this.bandCustomerName.AppearanceHeader.Options.UseForeColor = true;
            this.bandCustomerName.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandPlanDetail});
            this.bandCustomerName.Name = "bandCustomerName";
            this.bandCustomerName.OptionsBand.AllowMove = false;
            this.bandCustomerName.OptionsBand.FixedWidth = true;
            this.bandCustomerName.RowCount = 2;
            this.bandCustomerName.VisibleIndex = 0;
            this.bandCustomerName.Width = 342;
            // 
            // bandPlanDetail
            // 
            this.bandPlanDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandPlanDetail.AppearanceHeader.Options.UseFont = true;
            this.bandPlanDetail.AppearanceHeader.Options.UseForeColor = true;
            this.bandPlanDetail.Columns.Add(this.colStartDate);
            this.bandPlanDetail.Columns.Add(this.colTotalDays);
            this.bandPlanDetail.Columns.Add(this.colEndDate);
            this.bandPlanDetail.Columns.Add(this.colType);
            this.bandPlanDetail.Columns.Add(this.colItemLate);
            this.bandPlanDetail.Name = "bandPlanDetail";
            this.bandPlanDetail.OptionsBand.AllowMove = false;
            this.bandPlanDetail.OptionsBand.AllowSize = false;
            this.bandPlanDetail.OptionsBand.FixedWidth = true;
            this.bandPlanDetail.RowCount = 2;
            this.bandPlanDetail.VisibleIndex = 0;
            this.bandPlanDetail.Width = 342;
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStartDate.AppearanceCell.Options.UseFont = true;
            this.colStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartDate.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStartDate.AppearanceHeader.Options.UseFont = true;
            this.colStartDate.AppearanceHeader.Options.UseForeColor = true;
            this.colStartDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStartDate.Caption = "Start";
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.MinWidth = 100;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStartDate.OptionsFilter.AllowAutoFilter = false;
            this.colStartDate.OptionsFilter.AllowFilter = false;
            this.colStartDate.Visible = true;
            this.colStartDate.Width = 100;
            // 
            // colTotalDays
            // 
            this.colTotalDays.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDays.AppearanceCell.Options.UseFont = true;
            this.colTotalDays.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDays.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDays.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDays.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDays.AppearanceHeader.Options.UseFont = true;
            this.colTotalDays.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDays.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDays.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDays.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDays.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDays.Caption = "Working Days";
            this.colTotalDays.FieldName = "TotalDay";
            this.colTotalDays.MinWidth = 70;
            this.colTotalDays.Name = "colTotalDays";
            this.colTotalDays.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalDays.OptionsFilter.AllowAutoFilter = false;
            this.colTotalDays.OptionsFilter.AllowFilter = false;
            this.colTotalDays.Visible = true;
            this.colTotalDays.Width = 70;
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEndDate.AppearanceCell.Options.UseFont = true;
            this.colEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndDate.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEndDate.AppearanceHeader.Options.UseFont = true;
            this.colEndDate.AppearanceHeader.Options.UseForeColor = true;
            this.colEndDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEndDate.Caption = "Finish";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.MinWidth = 100;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEndDate.OptionsFilter.AllowAutoFilter = false;
            this.colEndDate.OptionsFilter.AllowFilter = false;
            this.colEndDate.Visible = true;
            this.colEndDate.Width = 100;
            // 
            // colType
            // 
            this.colType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colType.AppearanceCell.Options.UseFont = true;
            this.colType.AppearanceCell.Options.UseTextOptions = true;
            this.colType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colType.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colType.AppearanceHeader.Options.UseFont = true;
            this.colType.AppearanceHeader.Options.UseForeColor = true;
            this.colType.AppearanceHeader.Options.UseTextOptions = true;
            this.colType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colType.Caption = "Tiến độ";
            this.colType.FieldName = "TypeText";
            this.colType.MinWidth = 72;
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colType.OptionsFilter.AllowAutoFilter = false;
            this.colType.OptionsFilter.AllowFilter = false;
            this.colType.Visible = true;
            this.colType.Width = 72;
            // 
            // colItemLate
            // 
            this.colItemLate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colItemLate.AppearanceCell.Options.UseFont = true;
            this.colItemLate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colItemLate.AppearanceHeader.Options.UseFont = true;
            this.colItemLate.AppearanceHeader.Options.UseForeColor = true;
            this.colItemLate.AppearanceHeader.Options.UseTextOptions = true;
            this.colItemLate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colItemLate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colItemLate.FieldName = "ItemLate";
            this.colItemLate.Name = "colItemLate";
            // 
            // bandNote
            // 
            this.bandNote.AppearanceHeader.Options.UseFont = true;
            this.bandNote.AppearanceHeader.Options.UseForeColor = true;
            this.bandNote.AppearanceHeader.Options.UseTextOptions = true;
            this.bandNote.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandNote.Name = "bandNote";
            this.bandNote.OptionsBand.AllowMove = false;
            this.bandNote.RowCount = 5;
            this.bandNote.VisibleIndex = 1;
            this.bandNote.Width = 30;
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colFirst);
            this.gridBand1.MinWidth = 30;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.OptionsBand.AllowMove = false;
            this.gridBand1.OptionsBand.AllowSize = false;
            this.gridBand1.OptionsBand.FixedWidth = true;
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 30;
            // 
            // colFirst
            // 
            this.colFirst.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFirst.AppearanceCell.Options.UseFont = true;
            this.colFirst.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFirst.AppearanceHeader.Options.UseFont = true;
            this.colFirst.AppearanceHeader.Options.UseForeColor = true;
            this.colFirst.AppearanceHeader.Options.UseTextOptions = true;
            this.colFirst.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFirst.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFirst.MinWidth = 30;
            this.colFirst.Name = "colFirst";
            this.colFirst.Visible = true;
            this.colFirst.Width = 30;
            // 
            // colProjectTypeName
            // 
            this.colProjectTypeName.Caption = "Team";
            this.colProjectTypeName.FieldName = "ProjectTypeName";
            this.colProjectTypeName.Name = "colProjectTypeName";
            this.colProjectTypeName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colProjectTypeName.Visible = true;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AutoSize = true;
            this.btnExportToExcel.Location = new System.Drawing.Point(755, 8);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportToExcel.TabIndex = 5;
            this.btnExportToExcel.Text = "Xuất excel";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnShow
            // 
            this.btnShow.AutoSize = true;
            this.btnShow.Location = new System.Drawing.Point(558, 8);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "Xem tiến độ";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(171, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Dự án";
            // 
            // cboProject
            // 
            this.cboProject.EditValue = "";
            this.cboProject.Location = new System.Drawing.Point(225, 9);
            this.cboProject.Name = "cboProject";
            this.cboProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProject.Properties.NullText = "";
            this.cboProject.Properties.PopupFormMinSize = new System.Drawing.Size(1000, 0);
            this.cboProject.Properties.PopupFormSize = new System.Drawing.Size(1000, 0);
            this.cboProject.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboProject.Size = new System.Drawing.Size(327, 20);
            this.cboProject.TabIndex = 3;
            this.cboProject.EditValueChanged += new System.EventHandler(this.cboProject_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 30;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn1});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.Editable = false;
            this.searchLookUpEdit1View.OptionsBehavior.ReadOnly = true;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "Mã dự án";
            this.gridColumn2.FieldName = "ProjectCode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 285;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "Tên dự án";
            this.gridColumn3.FieldName = "ProjectName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 987;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Khách hàng";
            this.gridColumn1.FieldName = "CustomerCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 384;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1319, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Quá hạn";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(1273, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1223, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Thực tế";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(1177, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 20);
            this.label5.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1127, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Dự kiến";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Aqua;
            this.label7.Location = new System.Drawing.Point(1081, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 20);
            this.label7.TabIndex = 8;
            // 
            // btnUpdateProject
            // 
            this.btnUpdateProject.AutoSize = true;
            this.btnUpdateProject.Location = new System.Drawing.Point(639, 8);
            this.btnUpdateProject.Name = "btnUpdateProject";
            this.btnUpdateProject.Size = new System.Drawing.Size(110, 23);
            this.btnUpdateProject.TabIndex = 9;
            this.btnUpdateProject.Text = "Cập nhật công việc";
            this.btnUpdateProject.UseVisualStyleBackColor = true;
            this.btnUpdateProject.Click += new System.EventHandler(this.btnUpdateProject_Click);
            // 
            // dtpYear
            // 
            this.dtpYear.CustomFormat = "yyyy";
            this.dtpYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpYear.Location = new System.Drawing.Point(73, 8);
            this.dtpYear.Name = "dtpYear";
            this.dtpYear.ShowUpDown = true;
            this.dtpYear.Size = new System.Drawing.Size(77, 20);
            this.dtpYear.TabIndex = 10;
            this.dtpYear.ValueChanged += new System.EventHandler(this.dtpYear_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Năm:";
            // 
            // frmGanttChartProjectItemGrid
            // 
            this.AcceptButton = this.btnShow;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 697);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpYear);
            this.Controls.Add(this.btnUpdateProject);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProject);
            this.Name = "frmGanttChartProjectItemGrid";
            this.Text = "TIẾN ĐỘ CÔNG VIỆC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmGanttChartProjectItemGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboProject;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSTT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMission;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStartDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalDays;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEndDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIDMaster;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFirst;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colItemLate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullName;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandTitle;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandProject;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandCustomer;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandPlan;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandProjectName;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandCustomerName;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandPlanDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandNote;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectTypeName;
        private System.Windows.Forms.Button btnUpdateProject;
        private System.Windows.Forms.DateTimePicker dtpYear;
        private System.Windows.Forms.Label label8;
    }
}