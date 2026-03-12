
namespace BMS
{
    partial class frmPORequestPriceRTC
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
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductNewCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProductCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMaker = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCustomerCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDeadline = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colQty = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colQuantityRequestPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colQuantityRequestRemain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDVT = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsPriceRequestStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStatusRequest = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colIsCheckPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDatePriceQuote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCurrencyID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCurrencyRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTotalPriceExchange = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSupplierSaleID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTotalDayLeadTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectPartlistPriceRequestID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.cboCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboSupplierSale = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSupplierSale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDateRequest = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnFind = new System.Windows.Forms.Button();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupplierSale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator5,
            this.btnDelete});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1278, 58);
            this.mnuMenu.TabIndex = 4;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 52);
            this.btnSave.Tag = "frmProductRTC_SaleCutomer";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 41);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 55);
            this.btnDelete.Tag = "frmProjectPartlistPriceRequest_Delete";
            this.btnDelete.Text = "Xoá Y/c";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(12, 92);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemCheckEdit1,
            this.cboCurrency,
            this.cboSupplierSale});
            this.grdData.Size = new System.Drawing.Size(1254, 555);
            this.grdData.TabIndex = 295;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colDeadline,
            this.colProductNewCode,
            this.colProductCode,
            this.colProductName,
            this.colIsPriceRequestStatus,
            this.colMaker,
            this.colCustomerCode,
            this.colQty,
            this.colQuantityRequestPrice,
            this.colQuantityRequestRemain,
            this.colDVT,
            this.colStatusRequest,
            this.colIsCheckPrice,
            this.colDatePriceQuote,
            this.colUnitPrice,
            this.colCurrencyID,
            this.colCurrencyRate,
            this.colTotalPrice,
            this.colTotalPriceExchange,
            this.colSupplierSaleID,
            this.colTotalDayLeadTime,
            this.colNote,
            this.colProjectPartlistPriceRequestID});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBand1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Columns.Add(this.colID);
            this.gridBand1.Columns.Add(this.colProductNewCode);
            this.gridBand1.Columns.Add(this.colProductCode);
            this.gridBand1.Columns.Add(this.colProductName);
            this.gridBand1.Columns.Add(this.colMaker);
            this.gridBand1.Columns.Add(this.colCustomerCode);
            this.gridBand1.Columns.Add(this.colDeadline);
            this.gridBand1.Columns.Add(this.colQty);
            this.gridBand1.Columns.Add(this.colQuantityRequestPrice);
            this.gridBand1.Columns.Add(this.colQuantityRequestRemain);
            this.gridBand1.Columns.Add(this.colDVT);
            this.gridBand1.Columns.Add(this.colIsPriceRequestStatus);
            this.gridBand1.Columns.Add(this.colNote);
            this.gridBand1.Columns.Add(this.colStatusRequest);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 1380;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colProductNewCode
            // 
            this.colProductNewCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductNewCode.AppearanceCell.Options.UseFont = true;
            this.colProductNewCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductNewCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductNewCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductNewCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductNewCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductNewCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductNewCode.AppearanceHeader.Options.UseFont = true;
            this.colProductNewCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductNewCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductNewCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductNewCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductNewCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductNewCode.Caption = "Mã nội bộ";
            this.colProductNewCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductNewCode.FieldName = "ProductNewCode";
            this.colProductNewCode.Name = "colProductNewCode";
            this.colProductNewCode.OptionsColumn.AllowEdit = false;
            this.colProductNewCode.OptionsColumn.ReadOnly = true;
            this.colProductNewCode.Visible = true;
            this.colProductNewCode.Width = 120;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductCode.AppearanceCell.Options.UseFont = true;
            this.colProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductCode.AppearanceHeader.Options.UseFont = true;
            this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã sản phẩm ";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowEdit = false;
            this.colProductCode.OptionsColumn.ReadOnly = true;
            this.colProductCode.Visible = true;
            this.colProductCode.Width = 120;
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductName.AppearanceCell.Options.UseFont = true;
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductName.AppearanceHeader.Options.UseFont = true;
            this.colProductName.AppearanceHeader.Options.UseForeColor = true;
            this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.Caption = "Tên sản phẩm";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Visible = true;
            this.colProductName.Width = 150;
            // 
            // colMaker
            // 
            this.colMaker.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colMaker.AppearanceCell.Options.UseFont = true;
            this.colMaker.AppearanceCell.Options.UseTextOptions = true;
            this.colMaker.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colMaker.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaker.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colMaker.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMaker.AppearanceHeader.Options.UseFont = true;
            this.colMaker.AppearanceHeader.Options.UseForeColor = true;
            this.colMaker.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaker.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaker.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaker.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.Caption = "Hãng";
            this.colMaker.FieldName = "Maker";
            this.colMaker.Name = "colMaker";
            this.colMaker.OptionsColumn.AllowEdit = false;
            this.colMaker.OptionsColumn.ReadOnly = true;
            this.colMaker.Visible = true;
            this.colMaker.Width = 100;
            // 
            // colCustomerCode
            // 
            this.colCustomerCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCustomerCode.AppearanceCell.Options.UseFont = true;
            this.colCustomerCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCustomerCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomerCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCustomerCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCustomerCode.AppearanceHeader.Options.UseFont = true;
            this.colCustomerCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCustomerCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCustomerCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomerCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomerCode.Caption = "Mã khách hàng";
            this.colCustomerCode.FieldName = "CustomerCode";
            this.colCustomerCode.Name = "colCustomerCode";
            this.colCustomerCode.OptionsColumn.AllowEdit = false;
            this.colCustomerCode.OptionsColumn.ReadOnly = true;
            this.colCustomerCode.Visible = true;
            this.colCustomerCode.Width = 120;
            // 
            // colDeadline
            // 
            this.colDeadline.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colDeadline.AppearanceCell.Options.UseFont = true;
            this.colDeadline.AppearanceCell.Options.UseTextOptions = true;
            this.colDeadline.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDeadline.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDeadline.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDeadline.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDeadline.AppearanceHeader.Options.UseFont = true;
            this.colDeadline.AppearanceHeader.Options.UseForeColor = true;
            this.colDeadline.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeadline.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeadline.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDeadline.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDeadline.Caption = "Deadline";
            this.colDeadline.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDeadline.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDeadline.FieldName = "Deadline";
            this.colDeadline.Name = "colDeadline";
            this.colDeadline.Visible = true;
            this.colDeadline.Width = 120;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQty.AppearanceCell.Options.UseFont = true;
            this.colQty.AppearanceCell.Options.UseTextOptions = true;
            this.colQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQty.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQty.AppearanceHeader.Options.UseFont = true;
            this.colQty.AppearanceHeader.Options.UseForeColor = true;
            this.colQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.Caption = "Số lượng PO";
            this.colQty.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colQty.DisplayFormat.FormatString = "n2";
            this.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowEdit = false;
            this.colQty.OptionsColumn.ReadOnly = true;
            this.colQty.Visible = true;
            this.colQty.Width = 100;
            // 
            // colQuantityRequestPrice
            // 
            this.colQuantityRequestPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQuantityRequestPrice.AppearanceCell.Options.UseFont = true;
            this.colQuantityRequestPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityRequestPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuantityRequestPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantityRequestPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQuantityRequestPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQuantityRequestPrice.AppearanceHeader.Options.UseFont = true;
            this.colQuantityRequestPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colQuantityRequestPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantityRequestPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantityRequestPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuantityRequestPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantityRequestPrice.Caption = "Số lượng đã yêu cầu";
            this.colQuantityRequestPrice.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colQuantityRequestPrice.DisplayFormat.FormatString = "n2";
            this.colQuantityRequestPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantityRequestPrice.FieldName = "QuantityRequestPrice";
            this.colQuantityRequestPrice.Name = "colQuantityRequestPrice";
            this.colQuantityRequestPrice.OptionsColumn.AllowEdit = false;
            this.colQuantityRequestPrice.OptionsColumn.ReadOnly = true;
            this.colQuantityRequestPrice.Visible = true;
            this.colQuantityRequestPrice.Width = 100;
            // 
            // colQuantityRequestRemain
            // 
            this.colQuantityRequestRemain.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colQuantityRequestRemain.AppearanceCell.Options.UseFont = true;
            this.colQuantityRequestRemain.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityRequestRemain.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuantityRequestRemain.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantityRequestRemain.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colQuantityRequestRemain.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQuantityRequestRemain.AppearanceHeader.Options.UseFont = true;
            this.colQuantityRequestRemain.AppearanceHeader.Options.UseForeColor = true;
            this.colQuantityRequestRemain.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantityRequestRemain.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantityRequestRemain.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuantityRequestRemain.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantityRequestRemain.Caption = "SL yêu cầu";
            this.colQuantityRequestRemain.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colQuantityRequestRemain.DisplayFormat.FormatString = "n2";
            this.colQuantityRequestRemain.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantityRequestRemain.FieldName = "QuantityRequestRemain";
            this.colQuantityRequestRemain.Name = "colQuantityRequestRemain";
            this.colQuantityRequestRemain.Visible = true;
            this.colQuantityRequestRemain.Width = 100;
            // 
            // colDVT
            // 
            this.colDVT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDVT.AppearanceCell.Options.UseFont = true;
            this.colDVT.AppearanceCell.Options.UseTextOptions = true;
            this.colDVT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDVT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDVT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDVT.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDVT.AppearanceHeader.Options.UseFont = true;
            this.colDVT.AppearanceHeader.Options.UseForeColor = true;
            this.colDVT.AppearanceHeader.Options.UseTextOptions = true;
            this.colDVT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDVT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDVT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDVT.Caption = "ĐVT";
            this.colDVT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDVT.FieldName = "Unit";
            this.colDVT.Name = "colDVT";
            this.colDVT.OptionsColumn.AllowEdit = false;
            this.colDVT.OptionsColumn.ReadOnly = true;
            this.colDVT.Visible = true;
            this.colDVT.Width = 70;
            // 
            // colIsPriceRequestStatus
            // 
            this.colIsPriceRequestStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIsPriceRequestStatus.AppearanceCell.Options.UseFont = true;
            this.colIsPriceRequestStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colIsPriceRequestStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsPriceRequestStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsPriceRequestStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIsPriceRequestStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIsPriceRequestStatus.AppearanceHeader.Options.UseFont = true;
            this.colIsPriceRequestStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colIsPriceRequestStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsPriceRequestStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsPriceRequestStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsPriceRequestStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsPriceRequestStatus.Caption = "Trạng thái yêu cầu";
            this.colIsPriceRequestStatus.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsPriceRequestStatus.FieldName = "IsPriceRequestStatus";
            this.colIsPriceRequestStatus.Name = "colIsPriceRequestStatus";
            this.colIsPriceRequestStatus.OptionsColumn.AllowEdit = false;
            this.colIsPriceRequestStatus.OptionsColumn.ReadOnly = true;
            this.colIsPriceRequestStatus.Visible = true;
            this.colIsPriceRequestStatus.Width = 80;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNote.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.Width = 200;
            // 
            // colStatusRequest
            // 
            this.colStatusRequest.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colStatusRequest.AppearanceCell.Options.UseFont = true;
            this.colStatusRequest.AppearanceCell.Options.UseTextOptions = true;
            this.colStatusRequest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusRequest.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusRequest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusRequest.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colStatusRequest.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatusRequest.AppearanceHeader.Options.UseFont = true;
            this.colStatusRequest.AppearanceHeader.Options.UseForeColor = true;
            this.colStatusRequest.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusRequest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusRequest.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusRequest.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusRequest.Caption = "Trạng thái";
            this.colStatusRequest.FieldName = "StatusRequest";
            this.colStatusRequest.Name = "colStatusRequest";
            this.colStatusRequest.OptionsColumn.AllowEdit = false;
            this.colStatusRequest.OptionsColumn.ReadOnly = true;
            this.colStatusRequest.Width = 100;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridBand2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand2.AppearanceHeader.Options.UseFont = true;
            this.gridBand2.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "BÁO GIÁ";
            this.gridBand2.Columns.Add(this.colIsCheckPrice);
            this.gridBand2.Columns.Add(this.colDatePriceQuote);
            this.gridBand2.Columns.Add(this.colUnitPrice);
            this.gridBand2.Columns.Add(this.colCurrencyID);
            this.gridBand2.Columns.Add(this.colCurrencyRate);
            this.gridBand2.Columns.Add(this.colTotalPrice);
            this.gridBand2.Columns.Add(this.colTotalPriceExchange);
            this.gridBand2.Columns.Add(this.colSupplierSaleID);
            this.gridBand2.Columns.Add(this.colTotalDayLeadTime);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 1015;
            // 
            // colIsCheckPrice
            // 
            this.colIsCheckPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIsCheckPrice.AppearanceCell.Options.UseFont = true;
            this.colIsCheckPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colIsCheckPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsCheckPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsCheckPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIsCheckPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIsCheckPrice.AppearanceHeader.Options.UseFont = true;
            this.colIsCheckPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colIsCheckPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsCheckPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsCheckPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsCheckPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsCheckPrice.Caption = "Check giá";
            this.colIsCheckPrice.FieldName = "IsCheckPrice";
            this.colIsCheckPrice.Name = "colIsCheckPrice";
            this.colIsCheckPrice.OptionsColumn.AllowEdit = false;
            this.colIsCheckPrice.OptionsColumn.ReadOnly = true;
            this.colIsCheckPrice.Visible = true;
            // 
            // colDatePriceQuote
            // 
            this.colDatePriceQuote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colDatePriceQuote.AppearanceCell.Options.UseFont = true;
            this.colDatePriceQuote.AppearanceCell.Options.UseTextOptions = true;
            this.colDatePriceQuote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDatePriceQuote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDatePriceQuote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDatePriceQuote.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDatePriceQuote.AppearanceHeader.Options.UseFont = true;
            this.colDatePriceQuote.AppearanceHeader.Options.UseForeColor = true;
            this.colDatePriceQuote.AppearanceHeader.Options.UseTextOptions = true;
            this.colDatePriceQuote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDatePriceQuote.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDatePriceQuote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDatePriceQuote.Caption = "Ngày báo giá";
            this.colDatePriceQuote.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDatePriceQuote.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDatePriceQuote.FieldName = "DatePriceQuote";
            this.colDatePriceQuote.Name = "colDatePriceQuote";
            this.colDatePriceQuote.OptionsColumn.AllowEdit = false;
            this.colDatePriceQuote.OptionsColumn.ReadOnly = true;
            this.colDatePriceQuote.Visible = true;
            this.colDatePriceQuote.Width = 120;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colUnitPrice.AppearanceCell.Options.UseFont = true;
            this.colUnitPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colUnitPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnitPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnitPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUnitPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUnitPrice.AppearanceHeader.Options.UseFont = true;
            this.colUnitPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colUnitPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnitPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnitPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnitPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnitPrice.Caption = "Đơn giá";
            this.colUnitPrice.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colUnitPrice.DisplayFormat.FormatString = "n2";
            this.colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnitPrice.FieldName = "UnitPriceRequest";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.OptionsColumn.AllowEdit = false;
            this.colUnitPrice.OptionsColumn.ReadOnly = true;
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.Width = 100;
            // 
            // colCurrencyID
            // 
            this.colCurrencyID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCurrencyID.AppearanceCell.Options.UseFont = true;
            this.colCurrencyID.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrencyID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrencyID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrencyID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCurrencyID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCurrencyID.AppearanceHeader.Options.UseFont = true;
            this.colCurrencyID.AppearanceHeader.Options.UseForeColor = true;
            this.colCurrencyID.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrencyID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrencyID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrencyID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrencyID.Caption = "Loại tiền";
            this.colCurrencyID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCurrencyID.FieldName = "CurrencyCode";
            this.colCurrencyID.Name = "colCurrencyID";
            this.colCurrencyID.OptionsColumn.AllowEdit = false;
            this.colCurrencyID.OptionsColumn.ReadOnly = true;
            this.colCurrencyID.Visible = true;
            // 
            // colCurrencyRate
            // 
            this.colCurrencyRate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCurrencyRate.AppearanceCell.Options.UseFont = true;
            this.colCurrencyRate.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrencyRate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrencyRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrencyRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCurrencyRate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCurrencyRate.AppearanceHeader.Options.UseFont = true;
            this.colCurrencyRate.AppearanceHeader.Options.UseForeColor = true;
            this.colCurrencyRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrencyRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrencyRate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrencyRate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrencyRate.Caption = "Tỷ giá";
            this.colCurrencyRate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCurrencyRate.DisplayFormat.FormatString = "n2";
            this.colCurrencyRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrencyRate.FieldName = "CurrencyRatePrice";
            this.colCurrencyRate.Name = "colCurrencyRate";
            this.colCurrencyRate.OptionsColumn.AllowEdit = false;
            this.colCurrencyRate.OptionsColumn.ReadOnly = true;
            this.colCurrencyRate.Visible = true;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colTotalPrice.AppearanceCell.Options.UseFont = true;
            this.colTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTotalPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalPrice.AppearanceHeader.Options.UseFont = true;
            this.colTotalPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalPrice.Caption = "Thành tiền chưa VAT";
            this.colTotalPrice.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTotalPrice.DisplayFormat.FormatString = "n2";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.OptionsColumn.AllowEdit = false;
            this.colTotalPrice.OptionsColumn.ReadOnly = true;
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.Width = 100;
            // 
            // colTotalPriceExchange
            // 
            this.colTotalPriceExchange.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colTotalPriceExchange.AppearanceCell.Options.UseFont = true;
            this.colTotalPriceExchange.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPriceExchange.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalPriceExchange.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalPriceExchange.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTotalPriceExchange.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalPriceExchange.AppearanceHeader.Options.UseFont = true;
            this.colTotalPriceExchange.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalPriceExchange.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalPriceExchange.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalPriceExchange.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalPriceExchange.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalPriceExchange.Caption = "Thành tiền quy đổi (VNĐ)";
            this.colTotalPriceExchange.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTotalPriceExchange.DisplayFormat.FormatString = "n2";
            this.colTotalPriceExchange.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPriceExchange.FieldName = "TotalPriceExchange";
            this.colTotalPriceExchange.Name = "colTotalPriceExchange";
            this.colTotalPriceExchange.OptionsColumn.AllowEdit = false;
            this.colTotalPriceExchange.OptionsColumn.ReadOnly = true;
            this.colTotalPriceExchange.Visible = true;
            this.colTotalPriceExchange.Width = 100;
            // 
            // colSupplierSaleID
            // 
            this.colSupplierSaleID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colSupplierSaleID.AppearanceCell.Options.UseFont = true;
            this.colSupplierSaleID.AppearanceCell.Options.UseTextOptions = true;
            this.colSupplierSaleID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSupplierSaleID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSupplierSaleID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colSupplierSaleID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSupplierSaleID.AppearanceHeader.Options.UseFont = true;
            this.colSupplierSaleID.AppearanceHeader.Options.UseForeColor = true;
            this.colSupplierSaleID.AppearanceHeader.Options.UseTextOptions = true;
            this.colSupplierSaleID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSupplierSaleID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSupplierSaleID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSupplierSaleID.Caption = "Nhà cung cấp";
            this.colSupplierSaleID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSupplierSaleID.FieldName = "NameNCC";
            this.colSupplierSaleID.Name = "colSupplierSaleID";
            this.colSupplierSaleID.OptionsColumn.AllowEdit = false;
            this.colSupplierSaleID.OptionsColumn.ReadOnly = true;
            this.colSupplierSaleID.Visible = true;
            this.colSupplierSaleID.Width = 300;
            // 
            // colTotalDayLeadTime
            // 
            this.colTotalDayLeadTime.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colTotalDayLeadTime.AppearanceCell.Options.UseFont = true;
            this.colTotalDayLeadTime.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayLeadTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayLeadTime.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayLeadTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTotalDayLeadTime.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalDayLeadTime.AppearanceHeader.Options.UseFont = true;
            this.colTotalDayLeadTime.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDayLeadTime.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDayLeadTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDayLeadTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayLeadTime.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayLeadTime.Caption = "Leadtime";
            this.colTotalDayLeadTime.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTotalDayLeadTime.DisplayFormat.FormatString = "n0";
            this.colTotalDayLeadTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalDayLeadTime.FieldName = "TotalDayLeadTime";
            this.colTotalDayLeadTime.Name = "colTotalDayLeadTime";
            this.colTotalDayLeadTime.OptionsColumn.AllowEdit = false;
            this.colTotalDayLeadTime.OptionsColumn.ReadOnly = true;
            this.colTotalDayLeadTime.Visible = true;
            this.colTotalDayLeadTime.Width = 70;
            // 
            // colProjectPartlistPriceRequestID
            // 
            this.colProjectPartlistPriceRequestID.FieldName = "ProjectPartlistPriceRequestID";
            this.colProjectPartlistPriceRequestID.Name = "colProjectPartlistPriceRequestID";
            // 
            // cboCurrency
            // 
            this.cboCurrency.AutoHeight = false;
            this.cboCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.NullText = "";
            this.cboCurrency.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Loại tiền";
            this.colCurrency.FieldName = "Code";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 0;
            // 
            // cboSupplierSale
            // 
            this.cboSupplierSale.AutoHeight = false;
            this.cboSupplierSale.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSupplierSale.Name = "cboSupplierSale";
            this.cboSupplierSale.NullText = "";
            this.cboSupplierSale.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSupplierSale});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colSupplierSale
            // 
            this.colSupplierSale.Caption = "Nhà cung cấp";
            this.colSupplierSale.FieldName = "NameNCC";
            this.colSupplierSale.Name = "colSupplierSale";
            this.colSupplierSale.Visible = true;
            this.colSupplierSale.VisibleIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(112, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 302;
            this.label5.Text = "(*)";
            // 
            // dtpDateRequest
            // 
            this.dtpDateRequest.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateRequest.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateRequest.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateRequest.Location = new System.Drawing.Point(481, 61);
            this.dtpDateRequest.Name = "dtpDateRequest";
            this.dtpDateRequest.Size = new System.Drawing.Size(132, 26);
            this.dtpDateRequest.TabIndex = 301;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(376, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 18);
            this.label3.TabIndex = 296;
            this.label3.Text = "Ngày yêu cầu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 18);
            this.label1.TabIndex = 298;
            this.label1.Text = "Người yêu cầu";
            // 
            // cboEmployee
            // 
            this.cboEmployee.Location = new System.Drawing.Point(143, 62);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployee.Properties.Appearance.Options.UseFont = true;
            this.cboEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployee.Properties.NullText = "";
            this.cboEmployee.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboEmployee.Size = new System.Drawing.Size(227, 24);
            this.cboEmployee.TabIndex = 299;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmployeeID,
            this.gridColumn1,
            this.colFullName,
            this.colDep});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.GroupCount = 1;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.AutoExpandAllGroups = true;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDep, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colEmployeeID.AppearanceCell.Options.UseFont = true;
            this.colEmployeeID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEmployeeID.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeID.AppearanceHeader.Options.UseForeColor = true;
            this.colEmployeeID.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployeeID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeID.Caption = "ID";
            this.colEmployeeID.FieldName = "ID";
            this.colEmployeeID.Name = "colEmployeeID";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Mã nhân viên";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 367;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFullName.AppearanceHeader.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Options.UseForeColor = true;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 1;
            this.colFullName.Width = 843;
            // 
            // colDep
            // 
            this.colDep.Caption = "Phòng ban";
            this.colDep.FieldName = "DepartmentName";
            this.colDep.Name = "colDep";
            this.colDep.Visible = true;
            this.colDep.VisibleIndex = 2;
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(619, 61);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(73, 26);
            this.btnFind.TabIndex = 303;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // frmPORequestPriceRTC
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 659);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDateRequest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboEmployee);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmPORequestPriceRTC";
            this.Text = "YÊU CẦU BÁO GIÁ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProjectPartlistPriceRequestDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupplierSale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductNewCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsPriceRequestStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMaker;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStatusRequest;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCustomerCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQuantityRequestRemain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsCheckPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDeadline;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDatePriceQuote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnitPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrencyID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrencyRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalPriceExchange;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSupplierSaleID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTotalDayLeadTime;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNote;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDateRequest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colDep;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQty;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQuantityRequestPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDVT;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboCurrency;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboSupplierSale;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierSale;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectPartlistPriceRequestID;
    }
}