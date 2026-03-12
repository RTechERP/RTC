namespace BMS
{
    partial class frmQuotationSummary
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
            this.btnFind = new System.Windows.Forms.Button();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnShowQuotationDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskPriceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxImportPercent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxImporPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomsCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuotationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactWebsite = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxImporTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinishPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinishTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPriceCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemCboSupplier = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemSearchLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.panelPagingToolbar = new System.Windows.Forms.Panel();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCboSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panelPagingToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(315, 6);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(63, 23);
            this.btnFind.TabIndex = 71;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.ContextMenuStrip = this.contextMenuStrip1;
            this.grdData.Location = new System.Drawing.Point(0, 35);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemSearchLookUpEdit1,
            this.repositoryItemSearchLookUpEdit2,
            this.repositoryItemSearchLookUpEdit3});
            this.grdData.Size = new System.Drawing.Size(1339, 675);
            this.grdData.TabIndex = 67;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.Click += new System.EventHandler(this.grdData_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShowQuotationDetail,
            this.btnExcel,
            this.xóaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 70);
            // 
            // btnShowQuotationDetail
            // 
            this.btnShowQuotationDetail.Name = "btnShowQuotationDetail";
            this.btnShowQuotationDetail.Size = new System.Drawing.Size(179, 22);
            this.btnShowQuotationDetail.Text = "Xem báo giá chi tiết";
            this.btnShowQuotationDetail.Click += new System.EventHandler(this.btnShowQuotationDetail_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.btnExcel.Size = new System.Drawing.Size(179, 22);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.xóaToolStripMenuItem.Text = "Xóa";
            this.xóaToolStripMenuItem.Visible = false;
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.OddRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grvData.Appearance.OddRow.BackColor2 = System.Drawing.Color.WhiteSmoke;
            this.grvData.Appearance.OddRow.Options.UseBackColor = true;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 7F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colName,
            this.colCode,
            this.colAskPriceID,
            this.colManufacturerCode,
            this.colSupplierName,
            this.colQty,
            this.colUnit,
            this.gridColumn23,
            this.colVAT,
            this.colPriceCurrency,
            this.colTaxImportPercent,
            this.colTaxImporPrice,
            this.colDeliveryCost,
            this.colBankCost,
            this.colCustomsCost,
            this.colContactName,
            this.colContactPhone,
            this.colQuotationID,
            this.colContactWebsite,
            this.colTaxImporTotal,
            this.colFinishPrice,
            this.colFinishTotalPrice,
            this.colTotalPrice,
            this.colTotalPriceCurrency,
            this.colTotalVAT,
            this.colManufacture,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9});
            this.grvData.CustomizationFormBounds = new System.Drawing.Rectangle(1675, 429, 210, 382);
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.EnableAppearanceOddRow = true;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // colName
            // 
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colName.AppearanceHeader.BackColor = System.Drawing.Color.Red;
            this.colName.AppearanceHeader.BackColor2 = System.Drawing.Color.Red;
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceHeader.Options.UseBackColor = true;
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
            this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.Caption = "Tên vật tư";
            this.colName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colName.FieldName = "PartName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 3;
            this.colName.Width = 134;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.Caption = "Mã vật tư";
            this.colCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCode.FieldName = "PartCode";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 2;
            this.colCode.Width = 112;
            // 
            // colAskPriceID
            // 
            this.colAskPriceID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colAskPriceID.AppearanceCell.Options.UseFont = true;
            this.colAskPriceID.AppearanceCell.Options.UseTextOptions = true;
            this.colAskPriceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAskPriceID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAskPriceID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAskPriceID.AppearanceHeader.Options.UseFont = true;
            this.colAskPriceID.AppearanceHeader.Options.UseTextOptions = true;
            this.colAskPriceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAskPriceID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAskPriceID.Caption = "Người phụ trách";
            this.colAskPriceID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAskPriceID.FieldName = "SaleName";
            this.colAskPriceID.Name = "colAskPriceID";
            this.colAskPriceID.OptionsColumn.AllowEdit = false;
            this.colAskPriceID.Visible = true;
            this.colAskPriceID.VisibleIndex = 14;
            this.colAskPriceID.Width = 85;
            // 
            // colManufacturerCode
            // 
            this.colManufacturerCode.AppearanceCell.Options.UseTextOptions = true;
            this.colManufacturerCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colManufacturerCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colManufacturerCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colManufacturerCode.AppearanceHeader.Options.UseFont = true;
            this.colManufacturerCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colManufacturerCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colManufacturerCode.Caption = "Mã theo RTC";
            this.colManufacturerCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colManufacturerCode.FieldName = "PartCodeRTC";
            this.colManufacturerCode.Name = "colManufacturerCode";
            this.colManufacturerCode.OptionsColumn.AllowEdit = false;
            this.colManufacturerCode.Visible = true;
            this.colManufacturerCode.VisibleIndex = 4;
            this.colManufacturerCode.Width = 105;
            // 
            // colSupplierName
            // 
            this.colSupplierName.Caption = "Nhà cung cấp";
            this.colSupplierName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSupplierName.FieldName = "SupplierShortName";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.Width = 85;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Options.UseTextOptions = true;
            this.colQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.Caption = "Số lượng";
            this.colQty.ColumnEdit = this.repositoryItemTextEdit1;
            this.colQty.DisplayFormat.FormatString = "n0";
            this.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 8;
            this.colQty.Width = 40;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "n0";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatString = "n0";
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.EditMask = "n0";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colUnit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.Caption = "Đơn vị";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 7;
            this.colUnit.Width = 37;
            // 
            // gridColumn23
            // 
            this.gridColumn23.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn23.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn23.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn23.Caption = "Đơn giá";
            this.gridColumn23.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn23.DisplayFormat.FormatString = "n0";
            this.gridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn23.FieldName = "Price";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 11;
            this.gridColumn23.Width = 70;
            // 
            // colVAT
            // 
            this.colVAT.Caption = "VAT (%)";
            this.colVAT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colVAT.DisplayFormat.FormatString = "n0";
            this.colVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVAT.FieldName = "VAT";
            this.colVAT.Name = "colVAT";
            this.colVAT.Width = 31;
            // 
            // colPriceCurrency
            // 
            this.colPriceCurrency.AppearanceCell.Options.UseTextOptions = true;
            this.colPriceCurrency.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPriceCurrency.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceCurrency.Caption = "Đơn giá nhập";
            this.colPriceCurrency.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPriceCurrency.DisplayFormat.FormatString = "n0";
            this.colPriceCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceCurrency.FieldName = "PriceVT";
            this.colPriceCurrency.Name = "colPriceCurrency";
            this.colPriceCurrency.Visible = true;
            this.colPriceCurrency.VisibleIndex = 9;
            // 
            // colTaxImportPercent
            // 
            this.colTaxImportPercent.Caption = "Thuế NK (%)";
            this.colTaxImportPercent.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTaxImportPercent.DisplayFormat.FormatString = "n0";
            this.colTaxImportPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTaxImportPercent.FieldName = "TaxImportPercent";
            this.colTaxImportPercent.Name = "colTaxImportPercent";
            this.colTaxImportPercent.ToolTip = "Thuế nhập khẩu (%)";
            this.colTaxImportPercent.Width = 40;
            // 
            // colTaxImporPrice
            // 
            this.colTaxImporPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTaxImporPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTaxImporPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTaxImporPrice.Caption = "Tổng tiền nhập";
            this.colTaxImporPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTaxImporPrice.DisplayFormat.FormatString = "n0";
            this.colTaxImporPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTaxImporPrice.FieldName = "TotalVT";
            this.colTaxImporPrice.Name = "colTaxImporPrice";
            this.colTaxImporPrice.OptionsColumn.AllowEdit = false;
            this.colTaxImporPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalVT", "{0:n0}")});
            this.colTaxImporPrice.Visible = true;
            this.colTaxImporPrice.VisibleIndex = 10;
            // 
            // colDeliveryCost
            // 
            this.colDeliveryCost.Caption = "Chi phí vận chuyển";
            this.colDeliveryCost.ColumnEdit = this.repositoryItemTextEdit1;
            this.colDeliveryCost.DisplayFormat.FormatString = "n0";
            this.colDeliveryCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDeliveryCost.FieldName = "DeliveryCost";
            this.colDeliveryCost.Name = "colDeliveryCost";
            this.colDeliveryCost.OptionsColumn.AllowEdit = false;
            this.colDeliveryCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DeliveryCost", "{0:n0}")});
            this.colDeliveryCost.Width = 48;
            // 
            // colBankCost
            // 
            this.colBankCost.Caption = "Chi phí ngân hàng";
            this.colBankCost.ColumnEdit = this.repositoryItemTextEdit1;
            this.colBankCost.DisplayFormat.FormatString = "n0";
            this.colBankCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBankCost.FieldName = "BankCost";
            this.colBankCost.Name = "colBankCost";
            this.colBankCost.OptionsColumn.AllowEdit = false;
            this.colBankCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BankCost", "{0:n0}")});
            // 
            // colCustomsCost
            // 
            this.colCustomsCost.Caption = "Chi phí hải quan";
            this.colCustomsCost.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCustomsCost.DisplayFormat.FormatString = "n0";
            this.colCustomsCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCustomsCost.FieldName = "CustomsCost";
            this.colCustomsCost.Name = "colCustomsCost";
            this.colCustomsCost.OptionsColumn.AllowEdit = false;
            this.colCustomsCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CustomsCost", "{0:n0}")});
            // 
            // colContactName
            // 
            this.colContactName.AppearanceCell.Options.UseTextOptions = true;
            this.colContactName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactName.Caption = "Tên theo RTC";
            this.colContactName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colContactName.FieldName = "PartNameRTC";
            this.colContactName.Name = "colContactName";
            this.colContactName.OptionsColumn.AllowEdit = false;
            this.colContactName.Visible = true;
            this.colContactName.VisibleIndex = 5;
            this.colContactName.Width = 117;
            // 
            // colContactPhone
            // 
            this.colContactPhone.Caption = "SĐT";
            this.colContactPhone.FieldName = "ContactPhone";
            this.colContactPhone.Name = "colContactPhone";
            this.colContactPhone.Width = 129;
            // 
            // colQuotationID
            // 
            this.colQuotationID.Caption = "QuotationID";
            this.colQuotationID.FieldName = "QuotationID";
            this.colQuotationID.Name = "colQuotationID";
            this.colQuotationID.Width = 160;
            // 
            // colContactWebsite
            // 
            this.colContactWebsite.AppearanceCell.Options.UseTextOptions = true;
            this.colContactWebsite.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactWebsite.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactWebsite.Caption = "Khách hàng";
            this.colContactWebsite.FieldName = "CustomerShortName";
            this.colContactWebsite.Name = "colContactWebsite";
            this.colContactWebsite.OptionsColumn.AllowEdit = false;
            this.colContactWebsite.Visible = true;
            this.colContactWebsite.VisibleIndex = 1;
            this.colContactWebsite.Width = 87;
            // 
            // colTaxImporTotal
            // 
            this.colTaxImporTotal.Caption = "Tổng chi phí nhập khẩu";
            this.colTaxImporTotal.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTaxImporTotal.DisplayFormat.FormatString = "n0";
            this.colTaxImporTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTaxImporTotal.FieldName = "TaxImporTotal";
            this.colTaxImporTotal.Name = "colTaxImporTotal";
            this.colTaxImporTotal.OptionsColumn.AllowEdit = false;
            this.colTaxImporTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TaxImporTotal", "{0:n0}")});
            this.colTaxImporTotal.Width = 81;
            // 
            // colFinishPrice
            // 
            this.colFinishPrice.Caption = "Đơn giá sau chi phí";
            this.colFinishPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colFinishPrice.DisplayFormat.FormatString = "n0";
            this.colFinishPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinishPrice.FieldName = "FinishPrice";
            this.colFinishPrice.Name = "colFinishPrice";
            this.colFinishPrice.OptionsColumn.AllowEdit = false;
            this.colFinishPrice.Width = 76;
            // 
            // colFinishTotalPrice
            // 
            this.colFinishTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colFinishTotalPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFinishTotalPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFinishTotalPrice.Caption = "Tổng tiền";
            this.colFinishTotalPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colFinishTotalPrice.DisplayFormat.FormatString = "n0";
            this.colFinishTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinishTotalPrice.FieldName = "TotalPrice";
            this.colFinishTotalPrice.Name = "colFinishTotalPrice";
            this.colFinishTotalPrice.OptionsColumn.AllowEdit = false;
            this.colFinishTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "{0:n0}")});
            this.colFinishTotalPrice.Visible = true;
            this.colFinishTotalPrice.VisibleIndex = 12;
            this.colFinishTotalPrice.Width = 77;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.Caption = "Tổng giá nhập";
            this.colTotalPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.OptionsColumn.AllowEdit = false;
            this.colTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "{0:n0}")});
            // 
            // colTotalPriceCurrency
            // 
            this.colTotalPriceCurrency.Caption = "Tổng giá nhập ngoại tệ";
            this.colTotalPriceCurrency.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTotalPriceCurrency.FieldName = "TotalPriceCurrency";
            this.colTotalPriceCurrency.Name = "colTotalPriceCurrency";
            this.colTotalPriceCurrency.OptionsColumn.AllowEdit = false;
            this.colTotalPriceCurrency.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPriceCurrency", "{0:n0}")});
            // 
            // colTotalVAT
            // 
            this.colTotalVAT.Caption = "Tổng tiền VAT";
            this.colTotalVAT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTotalVAT.FieldName = "TotalVAT";
            this.colTotalVAT.Name = "colTotalVAT";
            this.colTotalVAT.OptionsColumn.AllowEdit = false;
            this.colTotalVAT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalVAT", "{0:n0}")});
            this.colTotalVAT.Width = 60;
            // 
            // colManufacture
            // 
            this.colManufacture.AppearanceCell.Options.UseTextOptions = true;
            this.colManufacture.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colManufacture.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colManufacture.Caption = "Hãng";
            this.colManufacture.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colManufacture.FieldName = "ManufacturerCode";
            this.colManufacture.Name = "colManufacture";
            this.colManufacture.OptionsColumn.AllowEdit = false;
            this.colManufacture.Visible = true;
            this.colManufacture.VisibleIndex = 6;
            this.colManufacture.Width = 72;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn6.Caption = "Mã báo giá";
            this.gridColumn6.FieldName = "QuotationCode";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 86;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn8.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn8.Caption = "Ngày báo giá";
            this.gridColumn8.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "QuotationDate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 13;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn9.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn9.Caption = "Ngày tạo";
            this.gridColumn9.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn9.FieldName = "CreatedDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 15;
            this.gridColumn9.Width = 67;
            // 
            // repositoryItemSearchLookUpEdit1
            // 
            this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
            this.repositoryItemSearchLookUpEdit1.NullText = "";
            this.repositoryItemSearchLookUpEdit1.View = this.repositoryItemCboSupplier;
            // 
            // repositoryItemCboSupplier
            // 
            this.repositoryItemCboSupplier.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn21,
            this.colSupplierCode,
            this.gridColumn22});
            this.repositoryItemCboSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCboSupplier.Name = "repositoryItemCboSupplier";
            this.repositoryItemCboSupplier.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCboSupplier.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "ID";
            this.gridColumn21.FieldName = "ID";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Width = 128;
            // 
            // colSupplierCode
            // 
            this.colSupplierCode.Caption = "Mã";
            this.colSupplierCode.FieldName = "SupplierCode";
            this.colSupplierCode.Name = "colSupplierCode";
            this.colSupplierCode.Visible = true;
            this.colSupplierCode.VisibleIndex = 0;
            this.colSupplierCode.Width = 128;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Tên";
            this.gridColumn22.FieldName = "SupplierName";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 1;
            this.gridColumn22.Width = 175;
            // 
            // repositoryItemSearchLookUpEdit2
            // 
            this.repositoryItemSearchLookUpEdit2.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit2.Name = "repositoryItemSearchLookUpEdit2";
            this.repositoryItemSearchLookUpEdit2.NullText = "";
            this.repositoryItemSearchLookUpEdit2.View = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemSearchLookUpEdit3
            // 
            this.repositoryItemSearchLookUpEdit3.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit3.Name = "repositoryItemSearchLookUpEdit3";
            this.repositoryItemSearchLookUpEdit3.NullText = "";
            this.repositoryItemSearchLookUpEdit3.View = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Từ khóa";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(51, 7);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(260, 20);
            this.txtFilterText.TabIndex = 69;
            this.txtFilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterText_KeyDown);
            // 
            // panelPagingToolbar
            // 
            this.panelPagingToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPagingToolbar.Controls.Add(this.txtTotalPage);
            this.panelPagingToolbar.Controls.Add(this.btnFirst);
            this.panelPagingToolbar.Controls.Add(this.txtPageSize);
            this.panelPagingToolbar.Controls.Add(this.btnPrev);
            this.panelPagingToolbar.Controls.Add(this.btnNext);
            this.panelPagingToolbar.Controls.Add(this.label6);
            this.panelPagingToolbar.Controls.Add(this.btnLast);
            this.panelPagingToolbar.Controls.Add(this.txtPageNumber);
            this.panelPagingToolbar.Location = new System.Drawing.Point(1077, 3);
            this.panelPagingToolbar.Name = "panelPagingToolbar";
            this.panelPagingToolbar.Size = new System.Drawing.Size(261, 27);
            this.panelPagingToolbar.TabIndex = 70;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Location = new System.Drawing.Point(108, 4);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(27, 20);
            this.txtTotalPage.TabIndex = 8;
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(2, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 23);
            this.btnFirst.TabIndex = 7;
            this.btnFirst.Text = "<<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // txtPageSize
            // 
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(204, 4);
            this.txtPageSize.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtPageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(54, 20);
            this.txtPageSize.TabIndex = 11;
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPageSize.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txtPageSize.ValueChanged += new System.EventHandler(this.txtPageSize_ValueChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(34, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(30, 23);
            this.btnPrev.TabIndex = 7;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(138, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(97, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "\\";
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(170, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 23);
            this.btnLast.TabIndex = 7;
            this.btnLast.Text = ">>";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(67, 4);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(27, 20);
            this.txtPageNumber.TabIndex = 8;
            // 
            // frmQuotationSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1342, 711);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelPagingToolbar);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFilterText);
            this.Name = "frmQuotationSummary";
            this.Text = "BÁO GIÁ TỔNG HỢP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmQuotationSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCboSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panelPagingToolbar.ResumeLayout(false);
            this.panelPagingToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAskPriceID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemCboSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImportPercent;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImporPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryCost;
        private DevExpress.XtraGrid.Columns.GridColumn colBankCost;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomsCost;
        private DevExpress.XtraGrid.Columns.GridColumn colContactName;
        private DevExpress.XtraGrid.Columns.GridColumn colContactPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colQuotationID;
        private DevExpress.XtraGrid.Columns.GridColumn colContactWebsite;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImporTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colFinishPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colFinishTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPriceCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacture;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Panel panelPagingToolbar;
        private System.Windows.Forms.TextBox txtTotalPage;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.TextBox txtPageNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnExcel;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnShowQuotationDetail;
    }
}