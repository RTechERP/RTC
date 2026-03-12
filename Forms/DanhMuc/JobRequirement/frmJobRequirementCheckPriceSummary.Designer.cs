namespace Forms.DanhMuc.JobRequirement
{
    partial class frmJobRequirementCheckPriceSummary
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.grdCheckPrice = new DevExpress.XtraGrid.GridControl();
            this.grvCheckPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bandNCC = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colRowHandle = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNumberRequest = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDeliveryDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRequestDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colExpectedDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNoteCheckPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colHRSuggestion = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colJobRequirementID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCheckPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCheckPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1126, 55);
            this.toolStrip1.TabIndex = 39;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = global::Forms.Properties.Resources.exporttoxlsx_32x32;
            this.btnExportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(87, 52);
            this.btnExportExcel.Text = "XUẤT EXCEL";
            this.btnExportExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // grdCheckPrice
            // 
            this.grdCheckPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCheckPrice.Location = new System.Drawing.Point(0, 55);
            this.grdCheckPrice.MainView = this.grvCheckPrice;
            this.grdCheckPrice.Name = "grdCheckPrice";
            this.grdCheckPrice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdCheckPrice.Size = new System.Drawing.Size(1126, 437);
            this.grdCheckPrice.TabIndex = 40;
            this.grdCheckPrice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCheckPrice,
            this.gridView7});
            // 
            // grvCheckPrice
            // 
            this.grvCheckPrice.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvCheckPrice.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Transparent;
            this.grvCheckPrice.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvCheckPrice.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvCheckPrice.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvCheckPrice.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvCheckPrice.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvCheckPrice.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvCheckPrice.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvCheckPrice.Appearance.Row.Options.UseFont = true;
            this.grvCheckPrice.Appearance.Row.Options.UseTextOptions = true;
            this.grvCheckPrice.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvCheckPrice.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvCheckPrice.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandNCC,
            this.gridBand1});
            this.grvCheckPrice.ColumnPanelRowHeight = 50;
            this.grvCheckPrice.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colRowHandle,
            this.gridColumn7,
            this.colJobRequirementID,
            this.colDeliveryDate,
            this.colRequestDate,
            this.colCustomer,
            this.colProductCode,
            this.colQuantity,
            this.colUnit,
            this.colHRSuggestion,
            this.colExpectedDate,
            this.colNoteCheckPrice,
            this.colNumberRequest});
            this.grvCheckPrice.GridControl = this.grdCheckPrice;
            this.grvCheckPrice.Name = "grvCheckPrice";
            this.grvCheckPrice.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvCheckPrice.OptionsBehavior.Editable = false;
            this.grvCheckPrice.OptionsBehavior.ReadOnly = true;
            this.grvCheckPrice.OptionsPrint.AutoWidth = false;
            this.grvCheckPrice.OptionsView.ColumnAutoWidth = false;
            this.grvCheckPrice.OptionsView.RowAutoHeight = true;
            this.grvCheckPrice.OptionsView.ShowAutoFilterRow = true;
            this.grvCheckPrice.OptionsView.ShowFooter = true;
            this.grvCheckPrice.OptionsView.ShowGroupPanel = false;
            // 
            // bandNCC
            // 
            this.bandNCC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.bandNCC.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.bandNCC.AppearanceHeader.Options.UseFont = true;
            this.bandNCC.AppearanceHeader.Options.UseForeColor = true;
            this.bandNCC.AppearanceHeader.Options.UseTextOptions = true;
            this.bandNCC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandNCC.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandNCC.Caption = "Nhà cung cấp";
            this.bandNCC.Name = "bandNCC";
            this.bandNCC.VisibleIndex = 0;
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colRowHandle);
            this.gridBand1.Columns.Add(this.gridColumn7);
            this.gridBand1.Columns.Add(this.colNumberRequest);
            this.gridBand1.Columns.Add(this.colDeliveryDate);
            this.gridBand1.Columns.Add(this.colRequestDate);
            this.gridBand1.Columns.Add(this.colExpectedDate);
            this.gridBand1.Columns.Add(this.colCustomer);
            this.gridBand1.Columns.Add(this.colProductCode);
            this.gridBand1.Columns.Add(this.colQuantity);
            this.gridBand1.Columns.Add(this.colUnit);
            this.gridBand1.Columns.Add(this.colHRSuggestion);
            this.gridBand1.Columns.Add(this.colNoteCheckPrice);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 1;
            this.gridBand1.Width = 1707;
            // 
            // colRowHandle
            // 
            this.colRowHandle.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colRowHandle.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colRowHandle.AppearanceHeader.Options.UseFont = true;
            this.colRowHandle.AppearanceHeader.Options.UseForeColor = true;
            this.colRowHandle.AppearanceHeader.Options.UseTextOptions = true;
            this.colRowHandle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRowHandle.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRowHandle.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRowHandle.Caption = "RowHandle";
            this.colRowHandle.FieldName = "RowHandle";
            this.colRowHandle.Name = "colRowHandle";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "JrcpID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // colNumberRequest
            // 
            this.colNumberRequest.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colNumberRequest.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNumberRequest.AppearanceHeader.Options.UseFont = true;
            this.colNumberRequest.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberRequest.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberRequest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberRequest.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberRequest.Caption = "Mã yêu cầu";
            this.colNumberRequest.FieldName = "NumberRequest";
            this.colNumberRequest.Name = "colNumberRequest";
            this.colNumberRequest.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "NumberRequest", "{0}")});
            this.colNumberRequest.Visible = true;
            this.colNumberRequest.Width = 183;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.AppearanceCell.Options.UseTextOptions = true;
            this.colDeliveryDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeliveryDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colDeliveryDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDeliveryDate.AppearanceHeader.Options.UseFont = true;
            this.colDeliveryDate.AppearanceHeader.Options.UseForeColor = true;
            this.colDeliveryDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeliveryDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeliveryDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDeliveryDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDeliveryDate.Caption = "Ngày cần giao";
            this.colDeliveryDate.FieldName = "DeliveryDate";
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colDeliveryDate.OptionsColumn.AllowEdit = false;
            this.colDeliveryDate.OptionsColumn.ReadOnly = true;
            this.colDeliveryDate.Visible = true;
            this.colDeliveryDate.Width = 122;
            // 
            // colRequestDate
            // 
            this.colRequestDate.AppearanceCell.Options.UseTextOptions = true;
            this.colRequestDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colRequestDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colRequestDate.AppearanceHeader.Options.UseFont = true;
            this.colRequestDate.AppearanceHeader.Options.UseForeColor = true;
            this.colRequestDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colRequestDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRequestDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRequestDate.Caption = "Ngày yêu cầu";
            this.colRequestDate.FieldName = "RequestDate";
            this.colRequestDate.Name = "colRequestDate";
            this.colRequestDate.OptionsColumn.AllowEdit = false;
            this.colRequestDate.OptionsColumn.ReadOnly = true;
            this.colRequestDate.Visible = true;
            this.colRequestDate.Width = 128;
            // 
            // colExpectedDate
            // 
            this.colExpectedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colExpectedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExpectedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colExpectedDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colExpectedDate.AppearanceHeader.Options.UseFont = true;
            this.colExpectedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colExpectedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colExpectedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExpectedDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colExpectedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExpectedDate.Caption = "Thời hạn hoàn thành";
            this.colExpectedDate.FieldName = "ExpectedDate";
            this.colExpectedDate.Name = "colExpectedDate";
            this.colExpectedDate.OptionsColumn.AllowEdit = false;
            this.colExpectedDate.OptionsColumn.ReadOnly = true;
            this.colExpectedDate.Visible = true;
            this.colExpectedDate.Width = 142;
            // 
            // colCustomer
            // 
            this.colCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCustomer.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCustomer.AppearanceHeader.Options.UseFont = true;
            this.colCustomer.AppearanceHeader.Options.UseForeColor = true;
            this.colCustomer.AppearanceHeader.Options.UseTextOptions = true;
            this.colCustomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomer.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomer.Caption = "KH/EU";
            this.colCustomer.FieldName = "Customer";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.OptionsColumn.AllowEdit = false;
            this.colCustomer.OptionsColumn.ReadOnly = true;
            this.colCustomer.Visible = true;
            this.colCustomer.Width = 214;
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colProductCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductCode.AppearanceHeader.Options.UseFont = true;
            this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã hàng";
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowEdit = false;
            this.colProductCode.OptionsColumn.ReadOnly = true;
            this.colProductCode.Visible = true;
            this.colProductCode.Width = 169;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantity.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colQuantity.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQuantity.AppearanceHeader.Options.UseFont = true;
            this.colQuantity.AppearanceHeader.Options.UseForeColor = true;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantity.Caption = "Số lượng";
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Visible = true;
            this.colQuantity.Width = 64;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colUnit.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Options.UseForeColor = true;
            this.colUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.Caption = "Đơn vị";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.ReadOnly = true;
            this.colUnit.Visible = true;
            // 
            // colNoteCheckPrice
            // 
            this.colNoteCheckPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNoteCheckPrice.AppearanceCell.Options.UseFont = true;
            this.colNoteCheckPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colNoteCheckPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNoteCheckPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteCheckPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colNoteCheckPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNoteCheckPrice.AppearanceHeader.Options.UseFont = true;
            this.colNoteCheckPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colNoteCheckPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoteCheckPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoteCheckPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNoteCheckPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteCheckPrice.Caption = "Ghi chú";
            this.colNoteCheckPrice.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNoteCheckPrice.FieldName = "Note";
            this.colNoteCheckPrice.Name = "colNoteCheckPrice";
            this.colNoteCheckPrice.OptionsColumn.AllowEdit = false;
            this.colNoteCheckPrice.OptionsColumn.ReadOnly = true;
            this.colNoteCheckPrice.Visible = true;
            this.colNoteCheckPrice.Width = 338;
            // 
            // colHRSuggestion
            // 
            this.colHRSuggestion.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colHRSuggestion.AppearanceCell.Options.UseFont = true;
            this.colHRSuggestion.AppearanceCell.Options.UseTextOptions = true;
            this.colHRSuggestion.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHRSuggestion.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHRSuggestion.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colHRSuggestion.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colHRSuggestion.AppearanceHeader.Options.UseFont = true;
            this.colHRSuggestion.AppearanceHeader.Options.UseForeColor = true;
            this.colHRSuggestion.AppearanceHeader.Options.UseTextOptions = true;
            this.colHRSuggestion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHRSuggestion.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHRSuggestion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHRSuggestion.Caption = "HR đề xuất";
            this.colHRSuggestion.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colHRSuggestion.FieldName = "HRSuggestion";
            this.colHRSuggestion.Name = "colHRSuggestion";
            this.colHRSuggestion.OptionsColumn.AllowEdit = false;
            this.colHRSuggestion.OptionsColumn.ReadOnly = true;
            this.colHRSuggestion.Visible = true;
            this.colHRSuggestion.Width = 272;
            // 
            // colJobRequirementID
            // 
            this.colJobRequirementID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colJobRequirementID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colJobRequirementID.AppearanceHeader.Options.UseFont = true;
            this.colJobRequirementID.AppearanceHeader.Options.UseForeColor = true;
            this.colJobRequirementID.AppearanceHeader.Options.UseTextOptions = true;
            this.colJobRequirementID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colJobRequirementID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colJobRequirementID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colJobRequirementID.Caption = "bandedGridColumn5";
            this.colJobRequirementID.FieldName = "JobRequirementID";
            this.colJobRequirementID.Name = "colJobRequirementID";
            this.colJobRequirementID.OptionsColumn.AllowEdit = false;
            this.colJobRequirementID.OptionsColumn.ReadOnly = true;
            this.colJobRequirementID.Visible = true;
            // 
            // gridView7
            // 
            this.gridView7.GridControl = this.grdCheckPrice;
            this.gridView7.Name = "gridView7";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // frmJobRequirementCheckPriceSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 492);
            this.Controls.Add(this.grdCheckPrice);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmJobRequirementCheckPriceSummary";
            this.Text = "TỔNG HỢP CHECK GIÁ YÊU CẦU CÔNG VIỆC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmJobRequirementCheckPriceSummary_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCheckPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCheckPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton btnExportExcel;
        private DevExpress.XtraGrid.GridControl grdCheckPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvCheckPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandNCC;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRowHandle;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDeliveryDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRequestDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colExpectedDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCustomer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQuantity;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNoteCheckPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colHRSuggestion;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colJobRequirementID;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNumberRequest;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}