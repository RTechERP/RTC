
namespace BMS
{
    partial class frmProjectSurvey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectSurvey));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnAdd = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnApprovedUrgent = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnUnApprovedUrgent = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnApprovedRequest = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnUnApprovedRequest = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnExportExcel = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnOpenFolder = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnProjectSurveyResult = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colIsUrgent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsApprovedUrgent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullNameRequest = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSDTCaNhan = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDateStart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDateEnd = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAddress = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPIC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDescription = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullNameApproved = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReasonUrgent = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colProjectTypeName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullNameLeaderTBP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullNameTechnical = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSDTCaNhanTechnical = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDateSurvey = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSurveySessionText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colResult = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReasonCancel = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectSurveyDetailID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnFind = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.cboEmployeeTechnical = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEmployeeSale = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboProject = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.colLeaderID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployeeTechnical.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployeeSale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnEdit,
            this.btnAdd,
            this.btnDelete,
            this.barButtonItem1,
            this.btnApprovedUrgent,
            this.btnUnApprovedUrgent,
            this.btnApprovedRequest,
            this.btnUnApprovedRequest,
            this.btnExportExcel,
            this.btnOpenFolder,
            this.btnProjectSurveyResult});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 26;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bar2.BarAppearance.Hovered.Options.UseFont = true;
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnApprovedUrgent),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUnApprovedUrgent),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnApprovedRequest),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUnApprovedRequest),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOpenFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnProjectSurveyResult)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "THÊM YÊU CẦU";
            this.btnAdd.Id = 12;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.LargeImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "SỬA YÊU CẦU";
            this.btnEdit.Id = 1;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.LargeImage")));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "XOÁ YÊU CẦU";
            this.btnDelete.Id = 13;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.LargeImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Id = 14;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnApprovedUrgent
            // 
            this.btnApprovedUrgent.Caption = "DUYỆT GẤP";
            this.btnApprovedUrgent.Id = 19;
            this.btnApprovedUrgent.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApprovedUrgent.ImageOptions.Image")));
            this.btnApprovedUrgent.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnApprovedUrgent.ImageOptions.LargeImage")));
            this.btnApprovedUrgent.Name = "btnApprovedUrgent";
            this.btnApprovedUrgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnApprovedUrgent_ItemClick);
            // 
            // btnUnApprovedUrgent
            // 
            this.btnUnApprovedUrgent.Caption = "HỦY DUYỆT GẤP";
            this.btnUnApprovedUrgent.Id = 20;
            this.btnUnApprovedUrgent.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUnApprovedUrgent.ImageOptions.Image")));
            this.btnUnApprovedUrgent.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnUnApprovedUrgent.ImageOptions.LargeImage")));
            this.btnUnApprovedUrgent.Name = "btnUnApprovedUrgent";
            this.btnUnApprovedUrgent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUnApprovedUrgent_ItemClick);
            // 
            // btnApprovedRequest
            // 
            this.btnApprovedRequest.Caption = "DUYỆT YÊU CẦU";
            this.btnApprovedRequest.Id = 21;
            this.btnApprovedRequest.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApprovedRequest.ImageOptions.Image")));
            this.btnApprovedRequest.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnApprovedRequest.ImageOptions.LargeImage")));
            this.btnApprovedRequest.Name = "btnApprovedRequest";
            this.btnApprovedRequest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnApprovedRequest_ItemClick);
            // 
            // btnUnApprovedRequest
            // 
            this.btnUnApprovedRequest.Caption = "HỦY DUYỆT YÊU CẦU";
            this.btnUnApprovedRequest.Id = 22;
            this.btnUnApprovedRequest.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUnApprovedRequest.ImageOptions.Image")));
            this.btnUnApprovedRequest.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnUnApprovedRequest.ImageOptions.LargeImage")));
            this.btnUnApprovedRequest.Name = "btnUnApprovedRequest";
            this.btnUnApprovedRequest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUnApprovedRequest_ItemClick);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Caption = "XUẤT EXCEL";
            this.btnExportExcel.Id = 23;
            this.btnExportExcel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.ImageOptions.Image")));
            this.btnExportExcel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.ImageOptions.LargeImage")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportExcel_ItemClick);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Caption = "CÂY THƯ MỤC";
            this.btnOpenFolder.Id = 24;
            this.btnOpenFolder.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.ImageOptions.Image")));
            this.btnOpenFolder.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOpenFolder.ImageOptions.LargeImage")));
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOpenFolder_ItemClick);
            // 
            // btnProjectSurveyResult
            // 
            this.btnProjectSurveyResult.Caption = "KẾT QUẢ KHẢO SÁT";
            this.btnProjectSurveyResult.Id = 25;
            this.btnProjectSurveyResult.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnProjectSurveyResult.ImageOptions.Image")));
            this.btnProjectSurveyResult.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnProjectSurveyResult.ImageOptions.LargeImage")));
            this.btnProjectSurveyResult.Name = "btnProjectSurveyResult";
            this.btnProjectSurveyResult.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProjectSurveyResult_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1338, 56);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 719);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1338, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 56);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 663);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1338, 56);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 663);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(12, 136);
            this.grdData.MainView = this.grvData;
            this.grdData.MenuManager = this.barManager1;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1314, 571);
            this.grdData.TabIndex = 4;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.BandPanel.Options.UseFont = true;
            this.grvData.Appearance.BandPanel.Options.UseForeColor = true;
            this.grvData.Appearance.BandPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.BandPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2});
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colID,
            this.colProjectCode,
            this.colProjectName,
            this.colFullNameRequest,
            this.colSDTCaNhan,
            this.colDateStart,
            this.colDateEnd,
            this.colAddress,
            this.colPIC,
            this.colDescription,
            this.colCustomerName,
            this.colIsUrgent,
            this.colIsApprovedUrgent,
            this.colReasonUrgent,
            this.colFullNameApproved,
            this.colProjectTypeName,
            this.colFullNameLeaderTBP,
            this.colStatusText,
            this.colFullNameTechnical,
            this.colReasonCancel,
            this.colNote,
            this.colProjectSurveyDetailID,
            this.colStatus,
            this.colDateSurvey,
            this.colSDTCaNhanTechnical,
            this.colProjectID,
            this.colEmployeeID,
            this.colResult,
            this.colSurveySessionText,
            this.colLeaderID});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsPrint.AutoWidth = false;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colProjectCode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "THÔNG TIN YÊU CẦU KHẢO SÁT";
            this.gridBand1.Columns.Add(this.colID);
            this.gridBand1.Columns.Add(this.colProjectName);
            this.gridBand1.Columns.Add(this.colIsUrgent);
            this.gridBand1.Columns.Add(this.colIsApprovedUrgent);
            this.gridBand1.Columns.Add(this.colFullNameRequest);
            this.gridBand1.Columns.Add(this.colSDTCaNhan);
            this.gridBand1.Columns.Add(this.colDateStart);
            this.gridBand1.Columns.Add(this.colDateEnd);
            this.gridBand1.Columns.Add(this.colProjectCode);
            this.gridBand1.Columns.Add(this.colCustomerName);
            this.gridBand1.Columns.Add(this.colAddress);
            this.gridBand1.Columns.Add(this.colPIC);
            this.gridBand1.Columns.Add(this.colDescription);
            this.gridBand1.Columns.Add(this.colFullNameApproved);
            this.gridBand1.Columns.Add(this.colReasonUrgent);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 2026;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colProjectName
            // 
            this.colProjectName.Caption = "Tên dự án";
            this.colProjectName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectName.FieldName = "ProjectName";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.Width = 157;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colIsUrgent
            // 
            this.colIsUrgent.Caption = "Khảo sát gấp";
            this.colIsUrgent.FieldName = "IsUrgent";
            this.colIsUrgent.Name = "colIsUrgent";
            this.colIsUrgent.Visible = true;
            this.colIsUrgent.Width = 78;
            // 
            // colIsApprovedUrgent
            // 
            this.colIsApprovedUrgent.Caption = "Duyệt gấp";
            this.colIsApprovedUrgent.FieldName = "IsApprovedUrgent";
            this.colIsApprovedUrgent.Name = "colIsApprovedUrgent";
            this.colIsApprovedUrgent.Visible = true;
            this.colIsApprovedUrgent.Width = 88;
            // 
            // colFullNameRequest
            // 
            this.colFullNameRequest.Caption = "Người yêu cầu";
            this.colFullNameRequest.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullNameRequest.FieldName = "FullNameRequest";
            this.colFullNameRequest.Name = "colFullNameRequest";
            this.colFullNameRequest.Visible = true;
            this.colFullNameRequest.Width = 137;
            // 
            // colSDTCaNhan
            // 
            this.colSDTCaNhan.Caption = "SĐT Người yêu cầu";
            this.colSDTCaNhan.FieldName = "SDTCaNhan";
            this.colSDTCaNhan.Name = "colSDTCaNhan";
            this.colSDTCaNhan.Visible = true;
            this.colSDTCaNhan.Width = 94;
            // 
            // colDateStart
            // 
            this.colDateStart.AppearanceCell.Options.UseTextOptions = true;
            this.colDateStart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStart.Caption = "Từ ngày";
            this.colDateStart.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateStart.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateStart.FieldName = "DateStart";
            this.colDateStart.Name = "colDateStart";
            this.colDateStart.Visible = true;
            this.colDateStart.Width = 97;
            // 
            // colDateEnd
            // 
            this.colDateEnd.AppearanceCell.Options.UseTextOptions = true;
            this.colDateEnd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateEnd.Caption = "Đến ngày";
            this.colDateEnd.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateEnd.FieldName = "DateEnd";
            this.colDateEnd.Name = "colDateEnd";
            this.colDateEnd.Visible = true;
            this.colDateEnd.Width = 91;
            // 
            // colProjectCode
            // 
            this.colProjectCode.Caption = "Mã dự án";
            this.colProjectCode.FieldName = "ProjectCode";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.Visible = true;
            this.colProjectCode.Width = 148;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Khách hàng";
            this.colCustomerName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.Width = 246;
            // 
            // colAddress
            // 
            this.colAddress.Caption = "Địa chỉ";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.Width = 254;
            // 
            // colPIC
            // 
            this.colPIC.Caption = "PIC";
            this.colPIC.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPIC.FieldName = "PIC";
            this.colPIC.Name = "colPIC";
            this.colPIC.Visible = true;
            this.colPIC.Width = 168;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Mô tả";
            this.colDescription.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.Width = 239;
            // 
            // colFullNameApproved
            // 
            this.colFullNameApproved.Caption = "Leader Sale";
            this.colFullNameApproved.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullNameApproved.FieldName = "FullNameApproved";
            this.colFullNameApproved.Name = "colFullNameApproved";
            this.colFullNameApproved.Visible = true;
            this.colFullNameApproved.Width = 185;
            // 
            // colReasonUrgent
            // 
            this.colReasonUrgent.Caption = "Lý do gấp";
            this.colReasonUrgent.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colReasonUrgent.FieldName = "ReasonUrgent";
            this.colReasonUrgent.Name = "colReasonUrgent";
            this.colReasonUrgent.Visible = true;
            this.colReasonUrgent.Width = 201;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "THÔNG TIN KỸ THUẬT KHẢO SÁT";
            this.gridBand2.Columns.Add(this.colProjectTypeName);
            this.gridBand2.Columns.Add(this.colFullNameLeaderTBP);
            this.gridBand2.Columns.Add(this.colStatusText);
            this.gridBand2.Columns.Add(this.colFullNameTechnical);
            this.gridBand2.Columns.Add(this.colSDTCaNhanTechnical);
            this.gridBand2.Columns.Add(this.colDateSurvey);
            this.gridBand2.Columns.Add(this.colSurveySessionText);
            this.gridBand2.Columns.Add(this.colResult);
            this.gridBand2.Columns.Add(this.colReasonCancel);
            this.gridBand2.Columns.Add(this.colNote);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 1531;
            // 
            // colProjectTypeName
            // 
            this.colProjectTypeName.Caption = "Kiểu khảo sát";
            this.colProjectTypeName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectTypeName.FieldName = "ProjectTypeName";
            this.colProjectTypeName.Name = "colProjectTypeName";
            this.colProjectTypeName.Visible = true;
            this.colProjectTypeName.Width = 152;
            // 
            // colFullNameLeaderTBP
            // 
            this.colFullNameLeaderTBP.Caption = "Leader Kỹ thuật";
            this.colFullNameLeaderTBP.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullNameLeaderTBP.FieldName = "FullNameLeaderTBP";
            this.colFullNameLeaderTBP.Name = "colFullNameLeaderTBP";
            this.colFullNameLeaderTBP.Visible = true;
            this.colFullNameLeaderTBP.Width = 140;
            // 
            // colStatusText
            // 
            this.colStatusText.Caption = "Trạng thái";
            this.colStatusText.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.Width = 142;
            // 
            // colFullNameTechnical
            // 
            this.colFullNameTechnical.Caption = "Kỹ thuật phụ trách";
            this.colFullNameTechnical.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullNameTechnical.FieldName = "FullNameTechnical";
            this.colFullNameTechnical.Name = "colFullNameTechnical";
            this.colFullNameTechnical.Visible = true;
            this.colFullNameTechnical.Width = 130;
            // 
            // colSDTCaNhanTechnical
            // 
            this.colSDTCaNhanTechnical.Caption = "SĐT Kỹ thuật";
            this.colSDTCaNhanTechnical.FieldName = "SDTCaNhanTechnical";
            this.colSDTCaNhanTechnical.Name = "colSDTCaNhanTechnical";
            this.colSDTCaNhanTechnical.Visible = true;
            this.colSDTCaNhanTechnical.Width = 117;
            // 
            // colDateSurvey
            // 
            this.colDateSurvey.AppearanceCell.Options.UseTextOptions = true;
            this.colDateSurvey.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateSurvey.Caption = "Ngày khảo sát";
            this.colDateSurvey.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateSurvey.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateSurvey.FieldName = "DateSurvey";
            this.colDateSurvey.Name = "colDateSurvey";
            this.colDateSurvey.Visible = true;
            this.colDateSurvey.Width = 91;
            // 
            // colSurveySessionText
            // 
            this.colSurveySessionText.Caption = "Buổi khảo sát";
            this.colSurveySessionText.FieldName = "SurveySessionText";
            this.colSurveySessionText.Name = "colSurveySessionText";
            this.colSurveySessionText.Visible = true;
            this.colSurveySessionText.Width = 80;
            // 
            // colResult
            // 
            this.colResult.Caption = "Kết quả khảo sát";
            this.colResult.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colResult.FieldName = "Result";
            this.colResult.Name = "colResult";
            this.colResult.Visible = true;
            this.colResult.Width = 182;
            // 
            // colReasonCancel
            // 
            this.colReasonCancel.Caption = "Lý do hủy";
            this.colReasonCancel.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colReasonCancel.FieldName = "ReasonCancel";
            this.colReasonCancel.Name = "colReasonCancel";
            this.colReasonCancel.Visible = true;
            this.colReasonCancel.Width = 245;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.Width = 252;
            // 
            // colProjectSurveyDetailID
            // 
            this.colProjectSurveyDetailID.FieldName = "ProjectSurveyDetailID";
            this.colProjectSurveyDetailID.Name = "colProjectSurveyDetailID";
            this.colProjectSurveyDetailID.Visible = true;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            // 
            // colProjectID
            // 
            this.colProjectID.Caption = "ProjectID";
            this.colProjectID.FieldName = "ProjectID";
            this.colProjectID.Name = "colProjectID";
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.Caption = "EmployeeID";
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.Name = "colEmployeeID";
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(861, 30);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 26);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtKeyword);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.cboEmployeeTechnical);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboEmployeeSale);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboProject);
            this.panel1.Controls.Add(this.dtpDateEnd);
            this.panel1.Controls.Add(this.dtpDateStart);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(12, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1314, 68);
            this.panel1.TabIndex = 14;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeyword.Location = new System.Drawing.Point(394, 32);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtKeyword.Size = new System.Drawing.Size(461, 23);
            this.txtKeyword.TabIndex = 11;
            // 
            // cboEmployeeTechnical
            // 
            this.cboEmployeeTechnical.Location = new System.Drawing.Point(73, 32);
            this.cboEmployeeTechnical.Name = "cboEmployeeTechnical";
            this.cboEmployeeTechnical.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployeeTechnical.Properties.Appearance.Options.UseFont = true;
            this.cboEmployeeTechnical.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployeeTechnical.Properties.NullText = "";
            this.cboEmployeeTechnical.Properties.PopupView = this.gridView1;
            this.cboEmployeeTechnical.Size = new System.Drawing.Size(254, 22);
            this.cboEmployeeTechnical.TabIndex = 10;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 40;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã nhân viên";
            this.gridColumn3.FieldName = "Code";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 612;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tên nhân viên";
            this.gridColumn4.FieldName = "FullName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 1003;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Kỹ thuật";
            // 
            // cboEmployeeSale
            // 
            this.cboEmployeeSale.Location = new System.Drawing.Point(720, 3);
            this.cboEmployeeSale.Name = "cboEmployeeSale";
            this.cboEmployeeSale.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployeeSale.Properties.Appearance.Options.UseFont = true;
            this.cboEmployeeSale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployeeSale.Properties.NullText = "";
            this.cboEmployeeSale.Properties.PopupView = this.gridView7;
            this.cboEmployeeSale.Size = new System.Drawing.Size(216, 22);
            this.cboEmployeeSale.TabIndex = 10;
            // 
            // gridView7
            // 
            this.gridView7.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView7.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView7.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView7.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView7.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView7.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView7.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridView7.Appearance.Row.Options.UseFont = true;
            this.gridView7.Appearance.Row.Options.UseTextOptions = true;
            this.gridView7.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView7.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView7.ColumnPanelRowHeight = 40;
            this.gridView7.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn15,
            this.gridColumn16});
            this.gridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView7.Name = "gridView7";
            this.gridView7.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView7.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Mã nhân viên";
            this.gridColumn15.FieldName = "Code";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 0;
            this.gridColumn15.Width = 612;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Tên nhân viên";
            this.gridColumn16.FieldName = "FullName";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 1;
            this.gridColumn16.Width = 1003;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(661, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 16);
            this.label14.TabIndex = 9;
            this.label14.Text = "NV Sale";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(333, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Từ khóa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(333, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dự án";
            // 
            // cboProject
            // 
            this.cboProject.Location = new System.Drawing.Point(394, 3);
            this.cboProject.MenuManager = this.barManager1;
            this.cboProject.Name = "cboProject";
            this.cboProject.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProject.Properties.Appearance.Options.UseFont = true;
            this.cboProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProject.Properties.NullText = "";
            this.cboProject.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboProject.Size = new System.Drawing.Size(263, 22);
            this.cboProject.TabIndex = 7;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.searchLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.Row.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 40;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã dự án";
            this.gridColumn1.FieldName = "ProjectCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 593;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên dự án";
            this.gridColumn2.FieldName = "ProjectName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 1022;
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.CustomFormat = "dd/MM/yyyy";
            this.dtpDateEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateEnd.Location = new System.Drawing.Point(237, 3);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(90, 23);
            this.dtpDateEnd.TabIndex = 5;
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.CustomFormat = "dd/MM/yyyy";
            this.dtpDateStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateStart.Location = new System.Drawing.Point(73, 3);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(90, 23);
            this.dtpDateStart.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(169, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 16);
            this.label11.TabIndex = 3;
            this.label11.Text = "Đến ngày";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "Từ ngày";
            // 
            // colLeaderID
            // 
            this.colLeaderID.FieldName = "LeaderID";
            this.colLeaderID.Name = "colLeaderID";
            this.colLeaderID.Visible = true;
            // 
            // frmProjectSurvey
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 719);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmProjectSurvey";
            this.Text = "KHẢO SÁT DỰ ÁN";
            this.Load += new System.EventHandler(this.frmProjectSurvey_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployeeTechnical.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployeeSale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarLargeButtonItem btnAdd;
        private DevExpress.XtraBars.BarLargeButtonItem btnEdit;
        private DevExpress.XtraBars.BarLargeButtonItem btnDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraGrid.GridControl grdData;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarLargeButtonItem btnApprovedUrgent;
        private DevExpress.XtraBars.BarLargeButtonItem btnUnApprovedUrgent;
        private DevExpress.XtraBars.BarLargeButtonItem btnApprovedRequest;
        private DevExpress.XtraBars.BarLargeButtonItem btnUnApprovedRequest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.SearchLookUpEdit cboProject;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployeeTechnical;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployeeSale;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKeyword;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsUrgent;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsApprovedUrgent;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullNameRequest;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSDTCaNhan;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDateStart;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDateEnd;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCustomerName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddress;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPIC;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDescription;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullNameApproved;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colReasonUrgent;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectTypeName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullNameLeaderTBP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStatusText;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullNameTechnical;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colReasonCancel;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNote;
        private DevExpress.XtraBars.BarLargeButtonItem btnExportExcel;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectSurveyDetailID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSDTCaNhanTechnical;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDateSurvey;
        private DevExpress.XtraBars.BarLargeButtonItem btnOpenFolder;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectID;
        private DevExpress.XtraBars.BarLargeButtonItem btnProjectSurveyResult;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEmployeeID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colResult;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSurveySessionText;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLeaderID;
    }
}