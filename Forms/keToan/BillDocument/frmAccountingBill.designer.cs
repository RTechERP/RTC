
namespace BMS
{
    partial class frmAccountingBill
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
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSTT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBillNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBillDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNameNCC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colTotalMoney = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCurrencyCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTaxCompanyCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPOStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPONote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPXKStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPXKNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBBBGStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBBBGNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colKHACStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKHACNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBillImports = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPURName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPURStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDeliverTaxStatusText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDeliverTaxDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.memoformatNumber = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBillImportDetailsView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboPurStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboPur = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboTaxCompany = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSuplierSale = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoformatNumber)).BeginInit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTaxCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSuplierSale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Location = new System.Drawing.Point(8, 122);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.memoformatNumber});
            this.grdData.Size = new System.Drawing.Size(1327, 576);
            this.grdData.TabIndex = 0;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.BandPanelRowHeight = 40;
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.gridBand5,
            this.gridBand3,
            this.gridBand6,
            this.gridBand1,
            this.gridBand4});
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colKHACStatus,
            this.colKHACNote,
            this.colID,
            this.colBillNumber,
            this.colBillDate,
            this.colNameNCC,
            this.colTotalMoney,
            this.colCurrencyCode,
            this.colTaxCompanyCode,
            this.colPOStatus,
            this.colPONote,
            this.colPXKStatus,
            this.colPXKNote,
            this.colBBBGStatus,
            this.colBBBGNote,
            this.colPURName,
            this.colPURStatus,
            this.colDeliverTaxStatusText,
            this.colDeliverTaxDate,
            this.colSTT,
            this.colBillImports});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand2";
            this.gridBand2.Columns.Add(this.colID);
            this.gridBand2.Columns.Add(this.colSTT);
            this.gridBand2.Columns.Add(this.colBillNumber);
            this.gridBand2.Columns.Add(this.colBillDate);
            this.gridBand2.Columns.Add(this.colNameNCC);
            this.gridBand2.Columns.Add(this.colTotalMoney);
            this.gridBand2.Columns.Add(this.colCurrencyCode);
            this.gridBand2.Columns.Add(this.colTaxCompanyCode);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.OptionsBand.ShowCaption = false;
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 810;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 19;
            this.colID.Name = "colID";
            this.colID.Width = 61;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.MinWidth = 19;
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.Width = 41;
            // 
            // colBillNumber
            // 
            this.colBillNumber.Caption = "Số hóa đơn";
            this.colBillNumber.FieldName = "BillNumber";
            this.colBillNumber.MinWidth = 19;
            this.colBillNumber.Name = "colBillNumber";
            this.colBillNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BillNumber", "{0}")});
            this.colBillNumber.Visible = true;
            this.colBillNumber.Width = 94;
            // 
            // colBillDate
            // 
            this.colBillDate.Caption = "Ngày hợp đồng";
            this.colBillDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colBillDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colBillDate.FieldName = "BillDate";
            this.colBillDate.MinWidth = 19;
            this.colBillDate.Name = "colBillDate";
            this.colBillDate.Visible = true;
            this.colBillDate.Width = 149;
            // 
            // colNameNCC
            // 
            this.colNameNCC.Caption = "Nhà cung cấp";
            this.colNameNCC.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNameNCC.FieldName = "NameNCC";
            this.colNameNCC.MinWidth = 19;
            this.colNameNCC.Name = "colNameNCC";
            this.colNameNCC.Visible = true;
            this.colNameNCC.Width = 236;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colTotalMoney
            // 
            this.colTotalMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalMoney.Caption = "Tổng tiền hóa đơn";
            this.colTotalMoney.DisplayFormat.FormatString = "n2";
            this.colTotalMoney.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalMoney.FieldName = "TotalMoney";
            this.colTotalMoney.MinWidth = 19;
            this.colTotalMoney.Name = "colTotalMoney";
            this.colTotalMoney.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalMoney", "{0:n2}")});
            this.colTotalMoney.Visible = true;
            this.colTotalMoney.Width = 136;
            // 
            // colCurrencyCode
            // 
            this.colCurrencyCode.Caption = "Loại tiền";
            this.colCurrencyCode.FieldName = "CurrencyCode";
            this.colCurrencyCode.MinWidth = 19;
            this.colCurrencyCode.Name = "colCurrencyCode";
            this.colCurrencyCode.Visible = true;
            this.colCurrencyCode.Width = 72;
            // 
            // colTaxCompanyCode
            // 
            this.colTaxCompanyCode.Caption = "Công ty";
            this.colTaxCompanyCode.FieldName = "TaxCompanyCode";
            this.colTaxCompanyCode.MinWidth = 19;
            this.colTaxCompanyCode.Name = "colTaxCompanyCode";
            this.colTaxCompanyCode.Visible = true;
            this.colTaxCompanyCode.Width = 82;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.gridBand5.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand5.AppearanceHeader.Options.UseFont = true;
            this.gridBand5.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridBand5.Caption = "PO";
            this.gridBand5.Columns.Add(this.colPOStatus);
            this.gridBand5.Columns.Add(this.colPONote);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 1;
            this.gridBand5.Width = 327;
            // 
            // colPOStatus
            // 
            this.colPOStatus.Caption = "NHẬN";
            this.colPOStatus.FieldName = "POStatus";
            this.colPOStatus.MinWidth = 19;
            this.colPOStatus.Name = "colPOStatus";
            this.colPOStatus.Visible = true;
            this.colPOStatus.Width = 161;
            // 
            // colPONote
            // 
            this.colPONote.Caption = "NOTE";
            this.colPONote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPONote.FieldName = "PONote";
            this.colPONote.MinWidth = 19;
            this.colPONote.Name = "colPONote";
            this.colPONote.Visible = true;
            this.colPONote.Width = 166;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.gridBand3.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridBand3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridBand3.Caption = "PXK";
            this.gridBand3.Columns.Add(this.colPXKStatus);
            this.gridBand3.Columns.Add(this.colPXKNote);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 286;
            // 
            // colPXKStatus
            // 
            this.colPXKStatus.Caption = "NHẬN";
            this.colPXKStatus.FieldName = "PXKStatus";
            this.colPXKStatus.MinWidth = 19;
            this.colPXKStatus.Name = "colPXKStatus";
            this.colPXKStatus.Visible = true;
            this.colPXKStatus.Width = 147;
            // 
            // colPXKNote
            // 
            this.colPXKNote.Caption = "NOTE";
            this.colPXKNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPXKNote.FieldName = "PXKNote";
            this.colPXKNote.MinWidth = 19;
            this.colPXKNote.Name = "colPXKNote";
            this.colPXKNote.Visible = true;
            this.colPXKNote.Width = 139;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.gridBand6.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridBand6.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand6.AppearanceHeader.Options.UseFont = true;
            this.gridBand6.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridBand6.Caption = "BBBG";
            this.gridBand6.Columns.Add(this.colBBBGStatus);
            this.gridBand6.Columns.Add(this.colBBBGNote);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 3;
            this.gridBand6.Width = 252;
            // 
            // colBBBGStatus
            // 
            this.colBBBGStatus.Caption = "NHẬN";
            this.colBBBGStatus.FieldName = "BBBGStatus";
            this.colBBBGStatus.MinWidth = 19;
            this.colBBBGStatus.Name = "colBBBGStatus";
            this.colBBBGStatus.Visible = true;
            this.colBBBGStatus.Width = 131;
            // 
            // colBBBGNote
            // 
            this.colBBBGNote.Caption = "NOTE";
            this.colBBBGNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBBBGNote.FieldName = "BBBGNote";
            this.colBBBGNote.MinWidth = 19;
            this.colBBBGNote.Name = "colBBBGNote";
            this.colBBBGNote.Visible = true;
            this.colBBBGNote.Width = 121;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBand1.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridBand1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand1.Caption = "CHỨNG TỪ KHÁC";
            this.gridBand1.Columns.Add(this.colKHACStatus);
            this.gridBand1.Columns.Add(this.colKHACNote);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 4;
            this.gridBand1.Width = 296;
            // 
            // colKHACStatus
            // 
            this.colKHACStatus.Caption = "Nhận";
            this.colKHACStatus.FieldName = "KHACStatus";
            this.colKHACStatus.MinWidth = 19;
            this.colKHACStatus.Name = "colKHACStatus";
            this.colKHACStatus.Visible = true;
            this.colKHACStatus.Width = 147;
            // 
            // colKHACNote
            // 
            this.colKHACNote.Caption = "NOTE";
            this.colKHACNote.FieldName = "KHACNote";
            this.colKHACNote.MinWidth = 19;
            this.colKHACNote.Name = "colKHACNote";
            this.colKHACNote.Visible = true;
            this.colKHACNote.Width = 149;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "gridBand4";
            this.gridBand4.Columns.Add(this.colBillImports);
            this.gridBand4.Columns.Add(this.colPURName);
            this.gridBand4.Columns.Add(this.colPURStatus);
            this.gridBand4.Columns.Add(this.colDeliverTaxStatusText);
            this.gridBand4.Columns.Add(this.colDeliverTaxDate);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.OptionsBand.ShowCaption = false;
            this.gridBand4.VisibleIndex = 5;
            this.gridBand4.Width = 853;
            // 
            // colBillImports
            // 
            this.colBillImports.Caption = "Phiếu nhập";
            this.colBillImports.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBillImports.FieldName = "BillImports";
            this.colBillImports.MinWidth = 19;
            this.colBillImports.Name = "colBillImports";
            this.colBillImports.Visible = true;
            this.colBillImports.Width = 267;
            // 
            // colPURName
            // 
            this.colPURName.Caption = "PUR";
            this.colPURName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPURName.FieldName = "PURName";
            this.colPURName.MinWidth = 19;
            this.colPURName.Name = "colPURName";
            this.colPURName.Visible = true;
            this.colPURName.Width = 166;
            // 
            // colPURStatus
            // 
            this.colPURStatus.Caption = "Xác nhận của PUR";
            this.colPURStatus.FieldName = "PURStatus";
            this.colPURStatus.MinWidth = 19;
            this.colPURStatus.Name = "colPURStatus";
            this.colPURStatus.Visible = true;
            this.colPURStatus.Width = 145;
            // 
            // colDeliverTaxStatusText
            // 
            this.colDeliverTaxStatusText.Caption = "BG cho BP thuế";
            this.colDeliverTaxStatusText.FieldName = "DeliverTaxStatusText";
            this.colDeliverTaxStatusText.MinWidth = 19;
            this.colDeliverTaxStatusText.Name = "colDeliverTaxStatusText";
            this.colDeliverTaxStatusText.Visible = true;
            this.colDeliverTaxStatusText.Width = 145;
            // 
            // colDeliverTaxDate
            // 
            this.colDeliverTaxDate.Caption = "Ngày bàn giao";
            this.colDeliverTaxDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDeliverTaxDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDeliverTaxDate.FieldName = "DeliverTaxDate";
            this.colDeliverTaxDate.MinWidth = 19;
            this.colDeliverTaxDate.Name = "colDeliverTaxDate";
            this.colDeliverTaxDate.Visible = true;
            this.colDeliverTaxDate.Width = 130;
            // 
            // memoformatNumber
            // 
            this.memoformatNumber.DisplayFormat.FormatString = "N2";
            this.memoformatNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.memoformatNumber.Name = "memoformatNumber";
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator12,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator11,
            this.btnBillImportDetailsView,
            this.toolStripSeparator2,
            this.btnImportExcel});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1341, 55);
            this.mnuMenu.TabIndex = 52;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 52);
            this.btnAdd.Tag = "frmAccountingBill_Add";
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.AutoSize = false;
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 50);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = global::Forms.Properties.Resources.Edit_32x321;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 52);
            this.btnEdit.Tag = "frmAccountingBill_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 52);
            this.btnDelete.Tag = "frmAccountingBill_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.AutoSize = false;
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 50);
            // 
            // btnBillImportDetailsView
            // 
            this.btnBillImportDetailsView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillImportDetailsView.Image = global::Forms.Properties.Resources.ShowProduct_32x32;
            this.btnBillImportDetailsView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnBillImportDetailsView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBillImportDetailsView.Name = "btnBillImportDetailsView";
            this.btnBillImportDetailsView.Size = new System.Drawing.Size(83, 52);
            this.btnBillImportDetailsView.Text = "Phiếu nhập";
            this.btnBillImportDetailsView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBillImportDetailsView.Click += new System.EventHandler(this.btnBillImportDetailsView_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ExportXlsxLarge;
            this.btnImportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(81, 52);
            this.btnImportExcel.Tag = "frmAccountingBill_ImportExcel";
            this.btnImportExcel.Text = "Nhập excel";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.cboPurStatus);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.cboPur);
            this.panelControl1.Controls.Add(this.cboTaxCompany);
            this.panelControl1.Controls.Add(this.cboSuplierSale);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.btnFind);
            this.panelControl1.Controls.Add(this.txtKeyword);
            this.panelControl1.Controls.Add(this.dtpDateEnd);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.dtpDateStart);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 55);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1341, 66);
            this.panelControl1.TabIndex = 54;
            // 
            // cboPurStatus
            // 
            this.cboPurStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPurStatus.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPurStatus.FormattingEnabled = true;
            this.cboPurStatus.Location = new System.Drawing.Point(462, 35);
            this.cboPurStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cboPurStatus.Name = "cboPurStatus";
            this.cboPurStatus.Size = new System.Drawing.Size(182, 25);
            this.cboPurStatus.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(339, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 17);
            this.label8.TabIndex = 28;
            this.label8.Text = "NV mua xác nhận";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "NV mua";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(398, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Công ty";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(648, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Nhà cung cấp";
            // 
            // cboPur
            // 
            this.cboPur.EditValue = "";
            this.cboPur.Location = new System.Drawing.Point(64, 35);
            this.cboPur.Margin = new System.Windows.Forms.Padding(2);
            this.cboPur.Name = "cboPur";
            this.cboPur.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPur.Properties.Appearance.Options.UseFont = true;
            this.cboPur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPur.Properties.NullText = "";
            this.cboPur.Properties.PopupView = this.gridView3;
            this.cboPur.Size = new System.Drawing.Size(270, 24);
            this.cboPur.TabIndex = 23;
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridView3.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView3.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView3.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView3.DetailHeight = 284;
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã PUR";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.MinWidth = 15;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 56;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên PUR";
            this.gridColumn2.FieldName = "FullName";
            this.gridColumn2.MinWidth = 15;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 56;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.FieldName = "ID";
            this.gridColumn3.MinWidth = 15;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 56;
            // 
            // cboTaxCompany
            // 
            this.cboTaxCompany.EditValue = "";
            this.cboTaxCompany.Location = new System.Drawing.Point(462, 5);
            this.cboTaxCompany.Margin = new System.Windows.Forms.Padding(2);
            this.cboTaxCompany.Name = "cboTaxCompany";
            this.cboTaxCompany.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTaxCompany.Properties.Appearance.Options.UseFont = true;
            this.cboTaxCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTaxCompany.Properties.NullText = "";
            this.cboTaxCompany.Properties.PopupView = this.gridView1;
            this.cboTaxCompany.Size = new System.Drawing.Size(181, 24);
            this.cboTaxCompany.TabIndex = 22;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridView1.DetailHeight = 284;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "gridColumn4";
            this.gridColumn4.FieldName = "ID";
            this.gridColumn4.MinWidth = 15;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 56;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mã công ty";
            this.gridColumn5.FieldName = "Code";
            this.gridColumn5.MinWidth = 15;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 56;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tên công ty";
            this.gridColumn6.FieldName = "Name";
            this.gridColumn6.MinWidth = 15;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 56;
            // 
            // cboSuplierSale
            // 
            this.cboSuplierSale.EditValue = "";
            this.cboSuplierSale.Location = new System.Drawing.Point(750, 5);
            this.cboSuplierSale.Margin = new System.Windows.Forms.Padding(2);
            this.cboSuplierSale.Name = "cboSuplierSale";
            this.cboSuplierSale.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSuplierSale.Properties.Appearance.Options.UseFont = true;
            this.cboSuplierSale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSuplierSale.Properties.NullText = "";
            this.cboSuplierSale.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboSuplierSale.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
            this.cboSuplierSale.Size = new System.Drawing.Size(301, 24);
            this.cboSuplierSale.TabIndex = 21;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.searchLookUpEdit1View.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.searchLookUpEdit1View.Appearance.Row.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.searchLookUpEdit1View.DetailHeight = 284;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.RowAutoHeight = true;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.MinWidth = 15;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 56;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Mã NCC";
            this.gridColumn8.FieldName = "CodeNCC";
            this.gridColumn8.MinWidth = 15;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 322;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn9.Caption = "Tên NCC";
            this.gridColumn9.ColumnEdit = this.repositoryItemMemoEdit2;
            this.gridColumn9.FieldName = "NameNCC";
            this.gridColumn9.MinWidth = 15;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            this.gridColumn9.Width = 611;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridColumn10.Caption = "Ngày tạo";
            this.gridColumn10.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn10.FieldName = "NgayUpdate";
            this.gridColumn10.MinWidth = 15;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            this.gridColumn10.Width = 282;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(686, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Từ khoá";
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(982, 33);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(71, 26);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeyword.Location = new System.Drawing.Point(750, 36);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(228, 23);
            this.txtKeyword.TabIndex = 6;
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateEnd.Location = new System.Drawing.Point(231, 6);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(103, 23);
            this.dtpDateEnd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(163, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateStart.Location = new System.Drawing.Point(64, 6);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Size = new System.Drawing.Size(93, 23);
            this.dtpDateStart.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ ngày";
            // 
            // frmAccountingBill
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 707);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.grdData);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmAccountingBill";
            this.Text = "HÓA ĐƠN THANH TOÁN";
            this.Load += new System.EventHandler(this.frmAccountingBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoformatNumber)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTaxCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSuplierSale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSTT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBillNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBillDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNameNCC;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalMoney;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrencyCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTaxCompanyCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPOStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPONote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPXKStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPXKNote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBBBGStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBBBGNote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPURName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPURStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDeliverTaxStatusText;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDeliverTaxDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SearchLookUpEdit cboPur;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SearchLookUpEdit cboTaxCompany;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.SearchLookUpEdit cboSuplierSale;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private System.Windows.Forms.ComboBox cboPurStatus;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKHACStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKHACNote;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnBillImportDetailsView;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBillImports;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memoformatNumber;
    }
}