using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    partial class frmQuotationDetailHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuotationDetailHistory));
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnChoose = new System.Windows.Forms.ToolStripButton();
            this.colFinishTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinishPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxImporTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAskPriceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemCboSupplier = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomsCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartCodeRTC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartNameRTC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCboSupplier)).BeginInit();
            this.SuspendLayout();
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
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnChoose});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1172, 42);
            this.mnuMenu.TabIndex = 5;
            this.mnuMenu.TabStop = true;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnChoose
            // 
            this.btnChoose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChoose.Image = ((System.Drawing.Image)(resources.GetObject("btnChoose.Image")));
            this.btnChoose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(39, 33);
            this.btnChoose.Text = "Chọn";
            this.btnChoose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // colFinishTotalPrice
            // 
            this.colFinishTotalPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colFinishTotalPrice.AppearanceCell.Options.UseFont = true;
            this.colFinishTotalPrice.Caption = "Tổng tiền sau chi phí";
            this.colFinishTotalPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colFinishTotalPrice.DisplayFormat.FormatString = "n0";
            this.colFinishTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinishTotalPrice.FieldName = "FinishTotalPrice";
            this.colFinishTotalPrice.Name = "colFinishTotalPrice";
            this.colFinishTotalPrice.OptionsColumn.AllowEdit = false;
            this.colFinishTotalPrice.OptionsColumn.AllowIncrementalSearch = false;
            this.colFinishTotalPrice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFinishTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinishTotalPrice", "{0:n0}")});
            this.colFinishTotalPrice.Visible = true;
            this.colFinishTotalPrice.VisibleIndex = 10;
            this.colFinishTotalPrice.Width = 69;
            // 
            // colFinishPrice
            // 
            this.colFinishPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colFinishPrice.AppearanceCell.Options.UseFont = true;
            this.colFinishPrice.Caption = "Đơn giá sau chi phí";
            this.colFinishPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colFinishPrice.DisplayFormat.FormatString = "n0";
            this.colFinishPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinishPrice.FieldName = "FinishPrice";
            this.colFinishPrice.Name = "colFinishPrice";
            this.colFinishPrice.OptionsColumn.AllowEdit = false;
            this.colFinishPrice.OptionsColumn.AllowIncrementalSearch = false;
            this.colFinishPrice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFinishPrice.Visible = true;
            this.colFinishPrice.VisibleIndex = 10;
            this.colFinishPrice.Width = 62;
            // 
            // colTaxImporTotal
            // 
            this.colTaxImporTotal.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colTaxImporTotal.AppearanceCell.Options.UseFont = true;
            this.colTaxImporTotal.Caption = "Tổng chi phí nhập khẩu";
            this.colTaxImporTotal.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTaxImporTotal.DisplayFormat.FormatString = "n0";
            this.colTaxImporTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTaxImporTotal.FieldName = "TaxImporTotal";
            this.colTaxImporTotal.Name = "colTaxImporTotal";
            this.colTaxImporTotal.OptionsColumn.AllowEdit = false;
            this.colTaxImporTotal.OptionsColumn.AllowIncrementalSearch = false;
            this.colTaxImporTotal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTaxImporTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TaxImporTotal", "{0:n0}")});
            this.colTaxImporTotal.Visible = true;
            this.colTaxImporTotal.VisibleIndex = 10;
            this.colTaxImporTotal.Width = 61;
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(0, 45);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemSearchLookUpEdit1,
            this.repositoryItemSearchLookUpEdit2,
            this.repositoryItemSearchLookUpEdit3});
            this.grdData.Size = new System.Drawing.Size(1172, 511);
            this.grdData.TabIndex = 6;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 60;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colName,
            this.colCode,
            this.colAskPriceID,
            this.colManufacturerID,
            this.colSupplierID,
            this.colQty,
            this.colUnit,
            this.colPriceVT,
            this.colVAT,
            this.colPriceCurrency,
            this.colDeliveryCost,
            this.colBankCost,
            this.colCustomsCost,
            this.gridColumn4,
            this.gridColumn5,
            this.colTotalVAT,
            this.gridColumn18,
            this.colPrice,
            this.colTotalPrice,
            this.colPartCodeRTC,
            this.colPartNameRTC,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn7});
            this.grvData.CustomizationFormBounds = new System.Drawing.Rectangle(1390, 429, 210, 382);
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsFilter.AllowFilterEditor = false;
            this.grvData.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.grvData.OptionsFind.AllowFindPanel = false;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            // 
            // colName
            // 
            this.colName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colName.AppearanceCell.Options.UseFont = true;
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
            this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.Caption = "Tên";
            this.colName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colName.FieldName = "PartName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowIncrementalSearch = false;
            this.colName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 4;
            this.colName.Width = 128;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.Caption = "Mã";
            this.colCode.FieldName = "PartCode";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowIncrementalSearch = false;
            this.colCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 3;
            this.colCode.Width = 120;
            // 
            // colAskPriceID
            // 
            this.colAskPriceID.AppearanceCell.Options.UseTextOptions = true;
            this.colAskPriceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAskPriceID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAskPriceID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAskPriceID.AppearanceHeader.Options.UseFont = true;
            this.colAskPriceID.AppearanceHeader.Options.UseTextOptions = true;
            this.colAskPriceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAskPriceID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAskPriceID.Caption = "Người hỏi giá";
            this.colAskPriceID.ColumnEdit = this.repositoryItemSearchLookUpEdit3;
            this.colAskPriceID.FieldName = "AskPriceID";
            this.colAskPriceID.Name = "colAskPriceID";
            this.colAskPriceID.Width = 98;
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
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "ID";
            this.gridColumn15.FieldName = "ID";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Width = 128;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Mã";
            this.gridColumn16.FieldName = "Code";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 0;
            this.gridColumn16.Width = 128;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Tên";
            this.gridColumn17.FieldName = "FullName";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            this.gridColumn17.Width = 175;
            // 
            // colManufacturerID
            // 
            this.colManufacturerID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colManufacturerID.AppearanceCell.Options.UseFont = true;
            this.colManufacturerID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colManufacturerID.AppearanceHeader.Options.UseFont = true;
            this.colManufacturerID.AppearanceHeader.Options.UseTextOptions = true;
            this.colManufacturerID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colManufacturerID.Caption = "Hãng";
            this.colManufacturerID.ColumnEdit = this.repositoryItemSearchLookUpEdit2;
            this.colManufacturerID.FieldName = "ManufacturerID";
            this.colManufacturerID.Name = "colManufacturerID";
            this.colManufacturerID.OptionsColumn.AllowIncrementalSearch = false;
            this.colManufacturerID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colManufacturerID.Width = 84;
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
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn13,
            this.gridColumn14});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ID";
            this.gridColumn2.FieldName = "ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Mã";
            this.gridColumn13.FieldName = "ManufacturerCode";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Tên";
            this.gridColumn14.FieldName = "ManufacturerName";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 1;
            this.gridColumn14.Width = 309;
            // 
            // colSupplierID
            // 
            this.colSupplierID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colSupplierID.AppearanceCell.Options.UseFont = true;
            this.colSupplierID.Caption = "Nhà cung cấp";
            this.colSupplierID.ColumnEdit = this.repositoryItemSearchLookUpEdit1;
            this.colSupplierID.FieldName = "SupplierID";
            this.colSupplierID.Name = "colSupplierID";
            this.colSupplierID.OptionsColumn.AllowIncrementalSearch = false;
            this.colSupplierID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSupplierID.Width = 86;
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
            this.gridColumn1,
            this.colSupplierCode,
            this.gridColumn12});
            this.repositoryItemCboSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCboSupplier.Name = "repositoryItemCboSupplier";
            this.repositoryItemCboSupplier.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCboSupplier.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 128;
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
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Tên";
            this.gridColumn12.FieldName = "SupplierName";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            this.gridColumn12.Width = 175;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colQty.AppearanceCell.Options.UseFont = true;
            this.colQty.Caption = "SL";
            this.colQty.ColumnEdit = this.repositoryItemTextEdit1;
            this.colQty.DisplayFormat.FormatString = "n0";
            this.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowIncrementalSearch = false;
            this.colQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 7;
            this.colQty.Width = 32;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colUnit.AppearanceCell.Options.UseFont = true;
            this.colUnit.Caption = "Đơn vị";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowIncrementalSearch = false;
            this.colUnit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colUnit.Width = 41;
            // 
            // colPriceVT
            // 
            this.colPriceVT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colPriceVT.AppearanceCell.Options.UseFont = true;
            this.colPriceVT.Caption = "Đơn giá nhập";
            this.colPriceVT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPriceVT.DisplayFormat.FormatString = "n0";
            this.colPriceVT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceVT.FieldName = "PriceVT";
            this.colPriceVT.Name = "colPriceVT";
            this.colPriceVT.OptionsColumn.AllowIncrementalSearch = false;
            this.colPriceVT.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPriceVT.Visible = true;
            this.colPriceVT.VisibleIndex = 9;
            this.colPriceVT.Width = 60;
            // 
            // colVAT
            // 
            this.colVAT.Caption = "VAT (%)";
            this.colVAT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colVAT.DisplayFormat.FormatString = "n0";
            this.colVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVAT.FieldName = "VAT";
            this.colVAT.Name = "colVAT";
            this.colVAT.Width = 36;
            // 
            // colPriceCurrency
            // 
            this.colPriceCurrency.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colPriceCurrency.AppearanceCell.Options.UseFont = true;
            this.colPriceCurrency.Caption = "Đơn giá ngoại tệ";
            this.colPriceCurrency.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPriceCurrency.DisplayFormat.FormatString = "n0";
            this.colPriceCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceCurrency.FieldName = "PriceCurrency";
            this.colPriceCurrency.Name = "colPriceCurrency";
            this.colPriceCurrency.OptionsColumn.AllowIncrementalSearch = false;
            this.colPriceCurrency.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPriceCurrency.Visible = true;
            this.colPriceCurrency.VisibleIndex = 8;
            this.colPriceCurrency.Width = 49;
            // 
            // colDeliveryCost
            // 
            this.colDeliveryCost.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colDeliveryCost.AppearanceCell.Options.UseFont = true;
            this.colDeliveryCost.Caption = "Chi phí vận chuyển";
            this.colDeliveryCost.ColumnEdit = this.repositoryItemTextEdit1;
            this.colDeliveryCost.DisplayFormat.FormatString = "n0";
            this.colDeliveryCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDeliveryCost.FieldName = "DeliveryCost";
            this.colDeliveryCost.Name = "colDeliveryCost";
            this.colDeliveryCost.OptionsColumn.AllowEdit = false;
            this.colDeliveryCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DeliveryCost", "{0:n0}")});
            this.colDeliveryCost.Width = 54;
            // 
            // colBankCost
            // 
            this.colBankCost.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colBankCost.AppearanceCell.Options.UseFont = true;
            this.colBankCost.Caption = "Chi phí ngân hàng";
            this.colBankCost.ColumnEdit = this.repositoryItemTextEdit1;
            this.colBankCost.DisplayFormat.FormatString = "n0";
            this.colBankCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBankCost.FieldName = "BankCost";
            this.colBankCost.Name = "colBankCost";
            this.colBankCost.OptionsColumn.AllowEdit = false;
            this.colBankCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BankCost", "{0:n0}")});
            this.colBankCost.Width = 51;
            // 
            // colCustomsCost
            // 
            this.colCustomsCost.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colCustomsCost.AppearanceCell.Options.UseFont = true;
            this.colCustomsCost.Caption = "Chi phí hải quan";
            this.colCustomsCost.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCustomsCost.DisplayFormat.FormatString = "n0";
            this.colCustomsCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCustomsCost.FieldName = "CustomsCost";
            this.colCustomsCost.Name = "colCustomsCost";
            this.colCustomsCost.OptionsColumn.AllowEdit = false;
            this.colCustomsCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CustomsCost", "{0:n0}")});
            this.colCustomsCost.Width = 53;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.Caption = "Đơn giá sau chi phí";
            this.gridColumn4.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn4.DisplayFormat.FormatString = "n0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "FinishPrice";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 10;
            this.gridColumn4.Width = 62;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.Caption = "Tổng tiền sau chi phí";
            this.gridColumn5.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn5.DisplayFormat.FormatString = "n0";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "FinishTotalPrice";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 11;
            this.gridColumn5.Width = 69;
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
            this.colTotalVAT.Width = 59;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "SL/ 1set";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Width = 52;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colPrice.AppearanceCell.Options.UseFont = true;
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPrice.Caption = "Đơn giá báo";
            this.colPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPrice.DisplayFormat.FormatString = "n0";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.AllowIncrementalSearch = false;
            this.colPrice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 12;
            this.colPrice.Width = 62;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.colTotalPrice.AppearanceCell.Options.UseFont = true;
            this.colTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalPrice.Caption = "Tổng tiền báo";
            this.colTotalPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTotalPrice.DisplayFormat.FormatString = "n0";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.OptionsColumn.AllowIncrementalSearch = false;
            this.colTotalPrice.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.VisibleIndex = 13;
            this.colTotalPrice.Width = 69;
            // 
            // colPartCodeRTC
            // 
            this.colPartCodeRTC.Caption = "Mã theo RTC";
            this.colPartCodeRTC.FieldName = "PartCodeRTC";
            this.colPartCodeRTC.Name = "colPartCodeRTC";
            this.colPartCodeRTC.OptionsColumn.AllowIncrementalSearch = false;
            this.colPartCodeRTC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPartCodeRTC.Visible = true;
            this.colPartCodeRTC.VisibleIndex = 5;
            this.colPartCodeRTC.Width = 115;
            // 
            // colPartNameRTC
            // 
            this.colPartNameRTC.Caption = "Tên theo RTC";
            this.colPartNameRTC.FieldName = "PartNameRTC";
            this.colPartNameRTC.Name = "colPartNameRTC";
            this.colPartNameRTC.OptionsColumn.AllowIncrementalSearch = false;
            this.colPartNameRTC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPartNameRTC.Visible = true;
            this.colPartNameRTC.VisibleIndex = 6;
            this.colPartNameRTC.Width = 114;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.Caption = "Dự án";
            this.gridColumn3.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn3.FieldName = "ProjectShortName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 92;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.Caption = "Khách hàng";
            this.gridColumn6.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn6.FieldName = "CustomerShortName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 96;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 7F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.Caption = "Báo giá";
            this.gridColumn7.FieldName = "QuotationCode";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 74;
            // 
            // frmQuotationDetailHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 557);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmQuotationDetailHistory";
            this.Text = "LỊCH SỬ BÁO GIÁ KHÁCH HÀNG";
            this.Load += new System.EventHandler(this.frmQuotationDetailHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCboSupplier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnChoose;
        private DevExpress.XtraGrid.Columns.GridColumn colFinishTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colFinishPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImporTotal;
        private DevExpress.XtraGrid.GridControl grdData;
        private GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAskPriceID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit3;
        private GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturerID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit2;
        private GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private GridView repositoryItemCboSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceVT;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryCost;
        private DevExpress.XtraGrid.Columns.GridColumn colBankCost;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomsCost;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalVAT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPartCodeRTC;
        private DevExpress.XtraGrid.Columns.GridColumn colPartNameRTC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    }
}