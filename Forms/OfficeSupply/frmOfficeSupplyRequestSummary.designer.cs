
namespace BMS
{
    partial class frmOfficeSupplyRequestSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOfficeSupplyRequestSummary));
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bandOfficeSupply = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSTT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCodeRTC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colOfficeSupplyName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colOfficeSupplyUnit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandAmount = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGD = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMH = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMKT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKD = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKYTHUAT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTKCK = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAGV = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHCM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandSummary = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTotalQuantity = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.dtpMonthPicker = new DevExpress.XtraEditors.DateEdit();
            this.lblDate = new System.Windows.Forms.Label();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnPriceRequest = new System.Windows.Forms.ToolStripButton();
            this.btnViewPriceRequest = new System.Windows.Forms.ToolStripButton();
            this.btnPurchaseRequest = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpDeadline = new System.Windows.Forms.DateTimePicker();
            this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMonth = new System.Windows.Forms.NumericUpDown();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.btnReload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnViewRequestBuy = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpMonthPicker.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpMonthPicker.Properties.CalendarTimeProperties)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(28, 31, 28, 31);
            this.grdData.Location = new System.Drawing.Point(0, 92);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(28, 31, 28, 31);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1362, 580);
            this.grdData.TabIndex = 28;
            this.grdData.Tag = "";
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
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.BandPanelRowHeight = 49;
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandOfficeSupply,
            this.bandAmount,
            this.bandSummary});
            this.grvData.ColumnPanelRowHeight = 60;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colOfficeSupplyName,
            this.colOfficeSupplyUnit,
            this.colAGV,
            this.colBN,
            this.colGD,
            this.colHCM,
            this.colHP,
            this.colHR,
            this.colKD,
            this.colKT,
            this.colKYTHUAT,
            this.colMH,
            this.colMKT,
            this.colTKCK,
            this.colTotalQuantity,
            this.colTotalPrice,
            this.colFullName,
            this.colNote,
            this.colUnitPrice,
            this.colSTT,
            this.colLR,
            this.colCodeRTC});
            this.grvData.DetailHeight = 3300;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsPrint.AutoWidth = false;
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.Tag = "";
            this.grvData.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvData_CustomColumnDisplayText);
            // 
            // bandOfficeSupply
            // 
            this.bandOfficeSupply.Columns.Add(this.colSTT);
            this.bandOfficeSupply.Columns.Add(this.colCodeRTC);
            this.bandOfficeSupply.Columns.Add(this.colOfficeSupplyName);
            this.bandOfficeSupply.Columns.Add(this.colOfficeSupplyUnit);
            this.bandOfficeSupply.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.bandOfficeSupply.Name = "bandOfficeSupply";
            this.bandOfficeSupply.OptionsBand.ShowCaption = false;
            this.bandOfficeSupply.VisibleIndex = 0;
            this.bandOfficeSupply.Width = 331;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.Width = 37;
            // 
            // colCodeRTC
            // 
            this.colCodeRTC.Caption = "Mã VPP";
            this.colCodeRTC.FieldName = "CodeRTC";
            this.colCodeRTC.Name = "colCodeRTC";
            this.colCodeRTC.Visible = true;
            this.colCodeRTC.Width = 72;
            // 
            // colOfficeSupplyName
            // 
            this.colOfficeSupplyName.AppearanceCell.Options.UseTextOptions = true;
            this.colOfficeSupplyName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOfficeSupplyName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOfficeSupplyName.Caption = "Tên Văn phòng phẩm";
            this.colOfficeSupplyName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colOfficeSupplyName.FieldName = "OfficeSupplyName";
            this.colOfficeSupplyName.Name = "colOfficeSupplyName";
            this.colOfficeSupplyName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "OfficeSupplyName", "{0}")});
            this.colOfficeSupplyName.Visible = true;
            this.colOfficeSupplyName.Width = 183;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colOfficeSupplyUnit
            // 
            this.colOfficeSupplyUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colOfficeSupplyUnit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOfficeSupplyUnit.Caption = "ĐVT";
            this.colOfficeSupplyUnit.FieldName = "OfficeSupplyUnit";
            this.colOfficeSupplyUnit.Name = "colOfficeSupplyUnit";
            this.colOfficeSupplyUnit.Visible = true;
            this.colOfficeSupplyUnit.Width = 39;
            // 
            // bandAmount
            // 
            this.bandAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandAmount.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bandAmount.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.bandAmount.AppearanceHeader.Options.UseFont = true;
            this.bandAmount.AppearanceHeader.Options.UseForeColor = true;
            this.bandAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.bandAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandAmount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandAmount.Caption = "Số lượng";
            this.bandAmount.Columns.Add(this.colGD);
            this.bandAmount.Columns.Add(this.colHR);
            this.bandAmount.Columns.Add(this.colKT);
            this.bandAmount.Columns.Add(this.colMH);
            this.bandAmount.Columns.Add(this.colMKT);
            this.bandAmount.Columns.Add(this.colKD);
            this.bandAmount.Columns.Add(this.colKYTHUAT);
            this.bandAmount.Columns.Add(this.colTKCK);
            this.bandAmount.Columns.Add(this.colAGV);
            this.bandAmount.Columns.Add(this.colBN);
            this.bandAmount.Columns.Add(this.colHP);
            this.bandAmount.Columns.Add(this.colHCM);
            this.bandAmount.Columns.Add(this.colLR);
            this.bandAmount.MinWidth = 662;
            this.bandAmount.Name = "bandAmount";
            this.bandAmount.VisibleIndex = 1;
            this.bandAmount.Width = 741;
            // 
            // colGD
            // 
            this.colGD.AppearanceCell.Options.UseTextOptions = true;
            this.colGD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGD.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGD.Caption = "Ban Giám đốc";
            this.colGD.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colGD.DisplayFormat.FormatString = "n0";
            this.colGD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGD.FieldName = "GD";
            this.colGD.Name = "colGD";
            this.colGD.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GD", "{0:n0}")});
            this.colGD.Visible = true;
            this.colGD.Width = 61;
            // 
            // colHR
            // 
            this.colHR.AppearanceCell.Options.UseTextOptions = true;
            this.colHR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHR.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHR.Caption = "HCNS";
            this.colHR.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colHR.DisplayFormat.FormatString = "n0";
            this.colHR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHR.FieldName = "HR";
            this.colHR.Name = "colHR";
            this.colHR.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "HR", "{0:n0}")});
            this.colHR.Visible = true;
            this.colHR.Width = 52;
            // 
            // colKT
            // 
            this.colKT.AppearanceCell.Options.UseTextOptions = true;
            this.colKT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colKT.Caption = "Kế toán";
            this.colKT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colKT.DisplayFormat.FormatString = "n0";
            this.colKT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colKT.FieldName = "KT";
            this.colKT.Name = "colKT";
            this.colKT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KT", "{0:n0}")});
            this.colKT.Visible = true;
            this.colKT.Width = 53;
            // 
            // colMH
            // 
            this.colMH.AppearanceCell.Options.UseTextOptions = true;
            this.colMH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMH.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMH.Caption = "Mua hàng";
            this.colMH.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colMH.DisplayFormat.FormatString = "n0";
            this.colMH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMH.FieldName = "MH";
            this.colMH.Name = "colMH";
            this.colMH.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MH", "{0:n0}")});
            this.colMH.Visible = true;
            this.colMH.Width = 55;
            // 
            // colMKT
            // 
            this.colMKT.AppearanceCell.Options.UseTextOptions = true;
            this.colMKT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMKT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMKT.Caption = "Phòng Marketing";
            this.colMKT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colMKT.DisplayFormat.FormatString = "n0";
            this.colMKT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMKT.FieldName = "MKT";
            this.colMKT.Name = "colMKT";
            this.colMKT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MKT", "{0:n0}")});
            this.colMKT.Visible = true;
            this.colMKT.Width = 64;
            // 
            // colKD
            // 
            this.colKD.AppearanceCell.Options.UseTextOptions = true;
            this.colKD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKD.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colKD.Caption = "Kinh doanh";
            this.colKD.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colKD.DisplayFormat.FormatString = "n0";
            this.colKD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colKD.FieldName = "KD";
            this.colKD.Name = "colKD";
            this.colKD.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KD", "{0:n0}")});
            this.colKD.Visible = true;
            this.colKD.Width = 59;
            // 
            // colKYTHUAT
            // 
            this.colKYTHUAT.AppearanceCell.Options.UseTextOptions = true;
            this.colKYTHUAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKYTHUAT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colKYTHUAT.Caption = "Kỹ thuật";
            this.colKYTHUAT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colKYTHUAT.DisplayFormat.FormatString = "n0";
            this.colKYTHUAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colKYTHUAT.FieldName = "KYTHUAT";
            this.colKYTHUAT.Name = "colKYTHUAT";
            this.colKYTHUAT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "KYTHUAT", "{0:n0}")});
            this.colKYTHUAT.Visible = true;
            this.colKYTHUAT.Width = 52;
            // 
            // colTKCK
            // 
            this.colTKCK.AppearanceCell.Options.UseTextOptions = true;
            this.colTKCK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTKCK.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTKCK.Caption = "Cơ khí - Thiết kế";
            this.colTKCK.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTKCK.DisplayFormat.FormatString = "n0";
            this.colTKCK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTKCK.FieldName = "TKCK";
            this.colTKCK.Name = "colTKCK";
            this.colTKCK.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TKCK", "{0:n0}")});
            this.colTKCK.Visible = true;
            this.colTKCK.Width = 70;
            // 
            // colAGV
            // 
            this.colAGV.AppearanceCell.Options.UseTextOptions = true;
            this.colAGV.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAGV.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAGV.Caption = "AGV";
            this.colAGV.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAGV.FieldName = "AGV";
            this.colAGV.Name = "colAGV";
            this.colAGV.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AGV", "{0:0.##}")});
            this.colAGV.Visible = true;
            this.colAGV.Width = 46;
            // 
            // colBN
            // 
            this.colBN.AppearanceCell.Options.UseTextOptions = true;
            this.colBN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBN.Caption = "Văn phòng BN";
            this.colBN.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBN.DisplayFormat.FormatString = "n0";
            this.colBN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBN.FieldName = "BN";
            this.colBN.Name = "colBN";
            this.colBN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BN", "{0:n0}")});
            this.colBN.Visible = true;
            this.colBN.Width = 54;
            // 
            // colHP
            // 
            this.colHP.AppearanceCell.Options.UseTextOptions = true;
            this.colHP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHP.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHP.Caption = "Văn Phòng HP";
            this.colHP.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colHP.DisplayFormat.FormatString = "n0";
            this.colHP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHP.FieldName = "HP";
            this.colHP.Name = "colHP";
            this.colHP.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "HP", "{0:n0}")});
            this.colHP.Visible = true;
            this.colHP.Width = 60;
            // 
            // colHCM
            // 
            this.colHCM.AppearanceCell.Options.UseTextOptions = true;
            this.colHCM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHCM.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHCM.Caption = "Văn phòng HCM";
            this.colHCM.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colHCM.DisplayFormat.FormatString = "n0";
            this.colHCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHCM.FieldName = "HCM";
            this.colHCM.Name = "colHCM";
            this.colHCM.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "HCM", "{0:n0}")});
            this.colHCM.Visible = true;
            this.colHCM.Width = 55;
            // 
            // colLR
            // 
            this.colLR.AppearanceCell.Options.UseTextOptions = true;
            this.colLR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLR.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLR.Caption = "Lắp ráp";
            this.colLR.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colLR.FieldName = "LR";
            this.colLR.Name = "colLR";
            this.colLR.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LR", "{0:n0}")});
            this.colLR.Visible = true;
            this.colLR.Width = 60;
            // 
            // bandSummary
            // 
            this.bandSummary.Columns.Add(this.colTotalQuantity);
            this.bandSummary.Columns.Add(this.colUnitPrice);
            this.bandSummary.Columns.Add(this.colTotalPrice);
            this.bandSummary.Columns.Add(this.colNote);
            this.bandSummary.Columns.Add(this.colFullName);
            this.bandSummary.Name = "bandSummary";
            this.bandSummary.OptionsBand.ShowCaption = false;
            this.bandSummary.VisibleIndex = 2;
            this.bandSummary.Width = 324;
            // 
            // colTotalQuantity
            // 
            this.colTotalQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalQuantity.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalQuantity.Caption = "Tổng";
            this.colTotalQuantity.DisplayFormat.FormatString = "n0";
            this.colTotalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalQuantity.FieldName = "TotalQuantity";
            this.colTotalQuantity.Name = "colTotalQuantity";
            this.colTotalQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantity", "{0:n0}")});
            this.colTotalQuantity.Visible = true;
            this.colTotalQuantity.Width = 46;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colUnitPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colUnitPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnitPrice.Caption = "Đơn giá";
            this.colUnitPrice.DisplayFormat.FormatString = "n2";
            this.colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.Width = 58;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalPrice.Caption = "Thành tiền";
            this.colTotalPrice.DisplayFormat.FormatString = "n0";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "{0:n0}")});
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.Width = 68;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.Width = 152;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.Caption = "Người đăng ký";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Width = 172;
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(78, 36);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dtpMonthPicker
            // 
            this.dtpMonthPicker.EditValue = new System.DateTime(2024, 3, 15, 11, 42, 14, 249);
            this.dtpMonthPicker.Location = new System.Drawing.Point(672, 14);
            this.dtpMonthPicker.Margin = new System.Windows.Forms.Padding(2);
            this.dtpMonthPicker.Name = "dtpMonthPicker";
            this.dtpMonthPicker.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMonthPicker.Properties.Appearance.Options.UseFont = true;
            this.dtpMonthPicker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpMonthPicker.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpMonthPicker.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.dtpMonthPicker.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
            this.dtpMonthPicker.Size = new System.Drawing.Size(106, 24);
            this.dtpMonthPicker.TabIndex = 206;
            this.dtpMonthPicker.Visible = false;
            this.dtpMonthPicker.EditValueChanged += new System.EventHandler(this.dtpMonthPicker_EditValueChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblDate.Location = new System.Drawing.Point(128, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(47, 17);
            this.lblDate.TabIndex = 205;
            this.lblDate.Text = "Tháng";
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExcel,
            this.btnPriceRequest,
            this.btnViewPriceRequest,
            this.btnViewRequestBuy,
            this.btnPurchaseRequest});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1362, 39);
            this.mnuMenu.TabIndex = 26;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnPriceRequest
            // 
            this.btnPriceRequest.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceRequest.Image = global::Forms.Properties.Resources.BOSale_16x16;
            this.btnPriceRequest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPriceRequest.Name = "btnPriceRequest";
            this.btnPriceRequest.Size = new System.Drawing.Size(114, 36);
            this.btnPriceRequest.Text = "Yêu cầu báo giá";
            this.btnPriceRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPriceRequest.Click += new System.EventHandler(this.btnPriceRequest_Click);
            // 
            // btnViewPriceRequest
            // 
            this.btnViewPriceRequest.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPriceRequest.Image = global::Forms.Properties.Resources.ViewMergedData_16x16;
            this.btnViewPriceRequest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnViewPriceRequest.Name = "btnViewPriceRequest";
            this.btnViewPriceRequest.Size = new System.Drawing.Size(146, 36);
            this.btnViewPriceRequest.Text = "Xem Yêu cầu báo giá";
            this.btnViewPriceRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewPriceRequest.Click += new System.EventHandler(this.btnViewPriceRequest_Click);
            // 
            // btnPurchaseRequest
            // 
            this.btnPurchaseRequest.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseRequest.Image = global::Forms.Properties.Resources.BO_Sale;
            this.btnPurchaseRequest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPurchaseRequest.Name = "btnPurchaseRequest";
            this.btnPurchaseRequest.Size = new System.Drawing.Size(94, 36);
            this.btnPurchaseRequest.Text = "Yêu cầu mua";
            this.btnPurchaseRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPurchaseRequest.Visible = false;
            this.btnPurchaseRequest.Click += new System.EventHandler(this.btnPurchaseRequest_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpDeadline);
            this.panel1.Controls.Add(this.cboDepartment);
            this.panel1.Controls.Add(this.txtMonth);
            this.panel1.Controls.Add(this.txtYear);
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Controls.Add(this.dtpMonthPicker);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1362, 53);
            this.panel1.TabIndex = 27;
            // 
            // dtpDeadline
            // 
            this.dtpDeadline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDeadline.CustomFormat = "dd/MM/yyyy";
            this.dtpDeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeadline.Location = new System.Drawing.Point(1272, 14);
            this.dtpDeadline.Name = "dtpDeadline";
            this.dtpDeadline.Size = new System.Drawing.Size(87, 22);
            this.dtpDeadline.TabIndex = 212;
            // 
            // cboDepartment
            // 
            this.cboDepartment.Location = new System.Drawing.Point(323, 14);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDepartment.Size = new System.Drawing.Size(266, 22);
            this.cboDepartment.TabIndex = 211;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
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
            this.gridColumn1.Caption = "Mã phòng ban";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 271;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên phòng ban";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 414;
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonth.Location = new System.Drawing.Point(181, 14);
            this.txtMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(55, 22);
            this.txtMonth.TabIndex = 210;
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(55, 14);
            this.txtYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(67, 22);
            this.txtYear.TabIndex = 209;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYear.Value = new decimal(new int[] {
            2024,
            0,
            0,
            0});
            // 
            // btnReload
            // 
            this.btnReload.AutoSize = true;
            this.btnReload.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnReload.Location = new System.Drawing.Point(595, 12);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(72, 27);
            this.btnReload.TabIndex = 208;
            this.btnReload.Text = "Tìm kiếm";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 205;
            this.label1.Text = "Năm";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(1152, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 205;
            this.label3.Text = "Deadline Y/c mua";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(242, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 205;
            this.label2.Text = "Phòng ban";
            // 
            // btnViewRequestBuy
            // 
            this.btnViewRequestBuy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewRequestBuy.Image = global::Forms.Properties.Resources.BO_Sale;
            this.btnViewRequestBuy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnViewRequestBuy.Name = "btnViewRequestBuy";
            this.btnViewRequestBuy.Size = new System.Drawing.Size(125, 36);
            this.btnViewRequestBuy.Text = "Xem yêu cầu mua";
            this.btnViewRequestBuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewRequestBuy.Visible = false;
            this.btnViewRequestBuy.Click += new System.EventHandler(this.btnViewRequestBuy_Click);
            // 
            // frmOfficeSupplyRequestSummary
            // 
            this.AcceptButton = this.btnReload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 672);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmOfficeSupplyRequestSummary";
            this.Text = "TỔNG HỢP ĐĂNG KÝ VĂN PHÒNG PHẨM";
            this.Load += new System.EventHandler(this.OfficeSupplyRequestSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpMonthPicker.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpMonthPicker.Properties)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraEditors.DateEdit dtpMonthPicker;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOfficeSupplyName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOfficeSupplyUnit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGD;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAGV;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTKCK;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHR;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKD;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKYTHUAT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMH;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMKT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBN;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHCM;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalQuantity;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalPrice;
        private System.Windows.Forms.Button btnReload;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.NumericUpDown txtMonth;
        private System.Windows.Forms.NumericUpDown txtYear;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnitPrice;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSTT;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLR;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandOfficeSupply;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCodeRTC;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandAmount;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandSummary;
        private System.Windows.Forms.ToolStripButton btnPriceRequest;
        private System.Windows.Forms.ToolStripButton btnPurchaseRequest;
        private System.Windows.Forms.DateTimePicker dtpDeadline;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btnViewPriceRequest;
        private System.Windows.Forms.ToolStripButton btnViewRequestBuy;
    }
}