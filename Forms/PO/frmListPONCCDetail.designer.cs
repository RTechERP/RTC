
namespace BMS
{
    partial class frmListPONCCDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListPONCCDetail));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.btnXuat = new System.Windows.Forms.ToolStripButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPONCCCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOKHNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPONCCID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestBuyRTCID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyRequest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSLNhan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSLcon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFeeShip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colVat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVatMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoteDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceSale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateDelivery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceHistory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductNewCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExcel,
            this.btnXuat});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1419, 81);
            this.mnuMenu.TabIndex = 53;
            this.mnuMenu.Tag = "frmPONCC_Update";
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = false;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(80, 41);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.AutoSize = false;
            this.btnXuat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.Image = global::Forms.Properties.Resources.ConvertToRange_32x32;
            this.btnXuat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(80, 41);
            this.btnXuat.Tag = "frmPONCC_Update";
            this.btnXuat.Text = "Chọn";
            this.btnXuat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXuat.Visible = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 81);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.grdData.Size = new System.Drawing.Size(1419, 652);
            this.grdData.TabIndex = 54;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData,
            this.gridView1});
            this.grdData.Click += new System.EventHandler(this.grdData_Click);
            // 
            // grvData
            // 
            this.grvData.ColumnPanelRowHeight = 42;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColStatus,
            this.colPONCCCode,
            this.colPOKHNumber,
            this.colProjectCode,
            this.colIDDetail,
            this.colProductCode_,
            this.colProductName_,
            this.colPONCCID,
            this.colRequestBuyRTCID,
            this.colQtyRequest,
            this.colSLNhan,
            this.colSLcon,
            this.colPrice,
            this.colFeeShip,
            this.colThanhTien,
            this.colUnit,
            this.coltotalPrice,
            this.colVat,
            this.colVatMoney,
            this.colNoteDetail,
            this.colDiscount,
            this.colCurrencyExchange,
            this.colPriceSale,
            this.colProfitRate,
            this.colDateDelivery,
            this.colPriceHistory,
            this.colProductID,
            this.colProductNewCode});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvData_RowCellStyle);
            this.grvData.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvData_CustomColumnDisplayText);
            // 
            // ColStatus
            // 
            this.ColStatus.AppearanceCell.Options.UseTextOptions = true;
            this.ColStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColStatus.AppearanceHeader.Options.UseFont = true;
            this.ColStatus.AppearanceHeader.Options.UseForeColor = true;
            this.ColStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.ColStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColStatus.Caption = "Trạng thái";
            this.ColStatus.FieldName = "Status";
            this.ColStatus.Name = "ColStatus";
            this.ColStatus.OptionsColumn.AllowFocus = false;
            this.ColStatus.OptionsColumn.ReadOnly = true;
            this.ColStatus.Visible = true;
            this.ColStatus.VisibleIndex = 0;
            this.ColStatus.Width = 113;
            // 
            // colPONCCCode
            // 
            this.colPONCCCode.AppearanceCell.Options.UseFont = true;
            this.colPONCCCode.AppearanceCell.Options.UseForeColor = true;
            this.colPONCCCode.AppearanceCell.Options.UseTextOptions = true;
            this.colPONCCCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPONCCCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPONCCCode.AppearanceHeader.Options.UseFont = true;
            this.colPONCCCode.AppearanceHeader.Options.UseForeColor = true;
            this.colPONCCCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colPONCCCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPONCCCode.Caption = "Mã PONCC";
            this.colPONCCCode.FieldName = "POCode";
            this.colPONCCCode.Name = "colPONCCCode";
            this.colPONCCCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "POCode", "Số dòng:{0}")});
            this.colPONCCCode.Visible = true;
            this.colPONCCCode.VisibleIndex = 1;
            this.colPONCCCode.Width = 196;
            // 
            // colPOKHNumber
            // 
            this.colPOKHNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPOKHNumber.AppearanceHeader.Options.UseFont = true;
            this.colPOKHNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colPOKHNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colPOKHNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPOKHNumber.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPOKHNumber.Caption = "Số POKH";
            this.colPOKHNumber.FieldName = "PONumber";
            this.colPOKHNumber.Name = "colPOKHNumber";
            this.colPOKHNumber.Visible = true;
            this.colPOKHNumber.VisibleIndex = 2;
            this.colPOKHNumber.Width = 92;
            // 
            // colProjectCode
            // 
            this.colProjectCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectCode.AppearanceHeader.Options.UseFont = true;
            this.colProjectCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectCode.Caption = "Mã dự án";
            this.colProjectCode.FieldName = "ProjectCode";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.Visible = true;
            this.colProjectCode.VisibleIndex = 3;
            this.colProjectCode.Width = 112;
            // 
            // colIDDetail
            // 
            this.colIDDetail.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIDDetail.AppearanceCell.Options.UseFont = true;
            this.colIDDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIDDetail.AppearanceHeader.Options.UseFont = true;
            this.colIDDetail.AppearanceHeader.Options.UseForeColor = true;
            this.colIDDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDDetail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDDetail.Caption = "ID";
            this.colIDDetail.FieldName = "ID";
            this.colIDDetail.Name = "colIDDetail";
            // 
            // colProductCode_
            // 
            this.colProductCode_.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductCode_.AppearanceCell.Options.UseFont = true;
            this.colProductCode_.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductCode_.AppearanceHeader.Options.UseFont = true;
            this.colProductCode_.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode_.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode_.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode_.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode_.Caption = "Mã sản phẩm ";
            this.colProductCode_.FieldName = "ProductCode";
            this.colProductCode_.Name = "colProductCode_";
            this.colProductCode_.Visible = true;
            this.colProductCode_.VisibleIndex = 6;
            this.colProductCode_.Width = 202;
            // 
            // colProductName_
            // 
            this.colProductName_.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductName_.AppearanceCell.Options.UseFont = true;
            this.colProductName_.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductName_.AppearanceHeader.Options.UseFont = true;
            this.colProductName_.AppearanceHeader.Options.UseForeColor = true;
            this.colProductName_.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName_.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName_.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName_.Caption = "Tên sản phẩm";
            this.colProductName_.FieldName = "ProductName";
            this.colProductName_.Name = "colProductName_";
            this.colProductName_.OptionsColumn.ReadOnly = true;
            this.colProductName_.Visible = true;
            this.colProductName_.VisibleIndex = 7;
            this.colProductName_.Width = 327;
            // 
            // colPONCCID
            // 
            this.colPONCCID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colPONCCID.AppearanceCell.Options.UseFont = true;
            this.colPONCCID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPONCCID.AppearanceHeader.Options.UseFont = true;
            this.colPONCCID.AppearanceHeader.Options.UseForeColor = true;
            this.colPONCCID.AppearanceHeader.Options.UseTextOptions = true;
            this.colPONCCID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPONCCID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPONCCID.Caption = "PONCCID";
            this.colPONCCID.FieldName = "PONCCID";
            this.colPONCCID.Name = "colPONCCID";
            // 
            // colRequestBuyRTCID
            // 
            this.colRequestBuyRTCID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colRequestBuyRTCID.AppearanceCell.Options.UseFont = true;
            this.colRequestBuyRTCID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colRequestBuyRTCID.AppearanceHeader.Options.UseFont = true;
            this.colRequestBuyRTCID.AppearanceHeader.Options.UseForeColor = true;
            this.colRequestBuyRTCID.AppearanceHeader.Options.UseTextOptions = true;
            this.colRequestBuyRTCID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestBuyRTCID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRequestBuyRTCID.Caption = "RequestBuyRTCID";
            this.colRequestBuyRTCID.FieldName = "RequestBuyRTCID";
            this.colRequestBuyRTCID.Name = "colRequestBuyRTCID";
            // 
            // colQtyRequest
            // 
            this.colQtyRequest.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colQtyRequest.AppearanceCell.Options.UseFont = true;
            this.colQtyRequest.AppearanceCell.Options.UseTextOptions = true;
            this.colQtyRequest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQtyRequest.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colQtyRequest.AppearanceHeader.Options.UseFont = true;
            this.colQtyRequest.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyRequest.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyRequest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyRequest.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyRequest.Caption = "Số lượng đặt";
            this.colQtyRequest.DisplayFormat.FormatString = "n0";
            this.colQtyRequest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQtyRequest.FieldName = "QtyRequest";
            this.colQtyRequest.Name = "colQtyRequest";
            this.colQtyRequest.Visible = true;
            this.colQtyRequest.VisibleIndex = 9;
            this.colQtyRequest.Width = 71;
            // 
            // colSLNhan
            // 
            this.colSLNhan.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colSLNhan.AppearanceHeader.Options.UseFont = true;
            this.colSLNhan.AppearanceHeader.Options.UseForeColor = true;
            this.colSLNhan.AppearanceHeader.Options.UseTextOptions = true;
            this.colSLNhan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSLNhan.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSLNhan.Caption = "Số lượng nhận";
            this.colSLNhan.DisplayFormat.FormatString = "n0";
            this.colSLNhan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSLNhan.FieldName = "QtyReal";
            this.colSLNhan.Name = "colSLNhan";
            this.colSLNhan.Visible = true;
            this.colSLNhan.VisibleIndex = 10;
            // 
            // colSLcon
            // 
            this.colSLcon.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colSLcon.AppearanceHeader.Options.UseFont = true;
            this.colSLcon.AppearanceHeader.Options.UseForeColor = true;
            this.colSLcon.AppearanceHeader.Options.UseTextOptions = true;
            this.colSLcon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSLcon.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSLcon.Caption = "Số lượng còn";
            this.colSLcon.DisplayFormat.FormatString = "n0";
            this.colSLcon.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSLcon.FieldName = "Soluongcon";
            this.colSLcon.Name = "colSLcon";
            this.colSLcon.Visible = true;
            this.colSLcon.VisibleIndex = 11;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colPrice.AppearanceCell.Options.UseFont = true;
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPrice.AppearanceHeader.Options.UseFont = true;
            this.colPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPrice.Caption = "Giá mua";
            this.colPrice.DisplayFormat.FormatString = "n0";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 13;
            this.colPrice.Width = 143;
            // 
            // colFeeShip
            // 
            this.colFeeShip.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colFeeShip.AppearanceCell.Options.UseFont = true;
            this.colFeeShip.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFeeShip.AppearanceHeader.Options.UseFont = true;
            this.colFeeShip.AppearanceHeader.Options.UseForeColor = true;
            this.colFeeShip.AppearanceHeader.Options.UseTextOptions = true;
            this.colFeeShip.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFeeShip.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFeeShip.Caption = "Chi phí vận chuyển";
            this.colFeeShip.DisplayFormat.FormatString = "n0";
            this.colFeeShip.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFeeShip.FieldName = "FeeShip";
            this.colFeeShip.Name = "colFeeShip";
            this.colFeeShip.Visible = true;
            this.colFeeShip.VisibleIndex = 19;
            this.colFeeShip.Width = 129;
            // 
            // colThanhTien
            // 
            this.colThanhTien.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colThanhTien.AppearanceCell.Options.UseFont = true;
            this.colThanhTien.AppearanceCell.Options.UseTextOptions = true;
            this.colThanhTien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colThanhTien.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colThanhTien.AppearanceHeader.Options.UseFont = true;
            this.colThanhTien.AppearanceHeader.Options.UseForeColor = true;
            this.colThanhTien.AppearanceHeader.Options.UseTextOptions = true;
            this.colThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colThanhTien.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colThanhTien.Caption = "Thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "n0";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.OptionsColumn.AllowEdit = false;
            this.colThanhTien.OptionsColumn.ReadOnly = true;
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 15;
            this.colThanhTien.Width = 146;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colUnit.AppearanceCell.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Options.UseForeColor = true;
            this.colUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.Caption = "Đơn vị tính";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.ReadOnly = true;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 8;
            this.colUnit.Width = 112;
            // 
            // coltotalPrice
            // 
            this.coltotalPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.coltotalPrice.AppearanceCell.Options.UseFont = true;
            this.coltotalPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.coltotalPrice.AppearanceHeader.Options.UseFont = true;
            this.coltotalPrice.AppearanceHeader.Options.UseForeColor = true;
            this.coltotalPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.coltotalPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coltotalPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coltotalPrice.Caption = "Tổng tiền";
            this.coltotalPrice.ColumnEdit = this.repositoryItemTextEdit1;
            this.coltotalPrice.DisplayFormat.FormatString = "n0";
            this.coltotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.coltotalPrice.FieldName = "totalPrice";
            this.coltotalPrice.Name = "coltotalPrice";
            this.coltotalPrice.OptionsColumn.AllowEdit = false;
            this.coltotalPrice.OptionsColumn.ReadOnly = true;
            this.coltotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "totalPrice", "={0:n0}")});
            this.coltotalPrice.Visible = true;
            this.coltotalPrice.VisibleIndex = 20;
            this.coltotalPrice.Width = 158;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BeepOnError = false;
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "n");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.UseMaskAsDisplayFormat = true;
            // 
            // colVat
            // 
            this.colVat.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colVat.AppearanceCell.Options.UseFont = true;
            this.colVat.AppearanceCell.Options.UseTextOptions = true;
            this.colVat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colVat.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colVat.AppearanceHeader.Options.UseFont = true;
            this.colVat.AppearanceHeader.Options.UseForeColor = true;
            this.colVat.AppearanceHeader.Options.UseTextOptions = true;
            this.colVat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVat.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colVat.Caption = "% Thuế GTGT";
            this.colVat.FieldName = "Vat";
            this.colVat.Name = "colVat";
            this.colVat.Visible = true;
            this.colVat.VisibleIndex = 16;
            this.colVat.Width = 67;
            // 
            // colVatMoney
            // 
            this.colVatMoney.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colVatMoney.AppearanceCell.Options.UseFont = true;
            this.colVatMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colVatMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colVatMoney.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colVatMoney.AppearanceHeader.Options.UseFont = true;
            this.colVatMoney.AppearanceHeader.Options.UseForeColor = true;
            this.colVatMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.colVatMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVatMoney.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colVatMoney.Caption = "Tiền thuế GTGT";
            this.colVatMoney.DisplayFormat.FormatString = "n0";
            this.colVatMoney.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVatMoney.FieldName = "VatMoney";
            this.colVatMoney.Name = "colVatMoney";
            this.colVatMoney.OptionsColumn.AllowEdit = false;
            this.colVatMoney.OptionsColumn.ReadOnly = true;
            this.colVatMoney.Visible = true;
            this.colVatMoney.VisibleIndex = 17;
            this.colVatMoney.Width = 129;
            // 
            // colNoteDetail
            // 
            this.colNoteDetail.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colNoteDetail.AppearanceCell.Options.UseFont = true;
            this.colNoteDetail.AppearanceCell.Options.UseTextOptions = true;
            this.colNoteDetail.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNoteDetail.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNoteDetail.AppearanceHeader.Options.UseFont = true;
            this.colNoteDetail.AppearanceHeader.Options.UseForeColor = true;
            this.colNoteDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoteDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoteDetail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteDetail.Caption = "Diễn giải";
            this.colNoteDetail.FieldName = "Note";
            this.colNoteDetail.Name = "colNoteDetail";
            this.colNoteDetail.Visible = true;
            this.colNoteDetail.VisibleIndex = 4;
            this.colNoteDetail.Width = 300;
            // 
            // colDiscount
            // 
            this.colDiscount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDiscount.AppearanceHeader.Options.UseFont = true;
            this.colDiscount.AppearanceHeader.Options.UseForeColor = true;
            this.colDiscount.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiscount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiscount.Caption = "Chiết khấu";
            this.colDiscount.DisplayFormat.FormatString = "n0";
            this.colDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDiscount.FieldName = "Discount";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.Visible = true;
            this.colDiscount.VisibleIndex = 18;
            this.colDiscount.Width = 118;
            // 
            // colCurrencyExchange
            // 
            this.colCurrencyExchange.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCurrencyExchange.AppearanceHeader.Options.UseFont = true;
            this.colCurrencyExchange.AppearanceHeader.Options.UseForeColor = true;
            this.colCurrencyExchange.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrencyExchange.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrencyExchange.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrencyExchange.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrencyExchange.Caption = "Thành tiền quy đổi";
            this.colCurrencyExchange.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCurrencyExchange.FieldName = "CurrencyExchange";
            this.colCurrencyExchange.Name = "colCurrencyExchange";
            this.colCurrencyExchange.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CurrencyExchange", "={0:n0}")});
            this.colCurrencyExchange.Visible = true;
            this.colCurrencyExchange.VisibleIndex = 21;
            this.colCurrencyExchange.Width = 122;
            // 
            // colPriceSale
            // 
            this.colPriceSale.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPriceSale.AppearanceHeader.Options.UseFont = true;
            this.colPriceSale.AppearanceHeader.Options.UseForeColor = true;
            this.colPriceSale.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceSale.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceSale.Caption = "Giá bán";
            this.colPriceSale.DisplayFormat.FormatString = "n0";
            this.colPriceSale.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPriceSale.FieldName = "PriceSale";
            this.colPriceSale.Name = "colPriceSale";
            this.colPriceSale.OptionsColumn.ReadOnly = true;
            this.colPriceSale.Visible = true;
            this.colPriceSale.VisibleIndex = 12;
            this.colPriceSale.Width = 132;
            // 
            // colProfitRate
            // 
            this.colProfitRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProfitRate.AppearanceHeader.Options.UseFont = true;
            this.colProfitRate.AppearanceHeader.Options.UseForeColor = true;
            this.colProfitRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colProfitRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProfitRate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProfitRate.Caption = "Tỷ lệ lợi nhuận";
            this.colProfitRate.FieldName = "ProfitRate";
            this.colProfitRate.Name = "colProfitRate";
            this.colProfitRate.Visible = true;
            this.colProfitRate.VisibleIndex = 22;
            this.colProfitRate.Width = 94;
            // 
            // colDateDelivery
            // 
            this.colDateDelivery.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDateDelivery.AppearanceHeader.Options.UseFont = true;
            this.colDateDelivery.AppearanceHeader.Options.UseForeColor = true;
            this.colDateDelivery.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateDelivery.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateDelivery.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDateDelivery.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateDelivery.Caption = "Ngày giao hàng";
            this.colDateDelivery.FieldName = "ActualDate";
            this.colDateDelivery.Name = "colDateDelivery";
            this.colDateDelivery.Visible = true;
            this.colDateDelivery.VisibleIndex = 5;
            this.colDateDelivery.Width = 94;
            // 
            // colPriceHistory
            // 
            this.colPriceHistory.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPriceHistory.AppearanceHeader.Options.UseFont = true;
            this.colPriceHistory.AppearanceHeader.Options.UseForeColor = true;
            this.colPriceHistory.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriceHistory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriceHistory.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriceHistory.Caption = "Giá lịch sử";
            this.colPriceHistory.FieldName = "PriceHistory";
            this.colPriceHistory.Name = "colPriceHistory";
            this.colPriceHistory.Visible = true;
            this.colPriceHistory.VisibleIndex = 14;
            this.colPriceHistory.Width = 139;
            // 
            // colProductID
            // 
            this.colProductID.Caption = "ProductID";
            this.colProductID.FieldName = "ProductID";
            this.colProductID.Name = "colProductID";
            // 
            // colProductNewCode
            // 
            this.colProductNewCode.Caption = "ProductNewCode";
            this.colProductNewCode.FieldName = "ProductNewCode";
            this.colProductNewCode.Name = "colProductNewCode";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdData;
            this.gridView1.Name = "gridView1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(555, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 14);
            this.label7.TabIndex = 171;
            this.label7.Text = "Từ khóa";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFromDate.CustomFormat = "dd/MM//yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(51, 54);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(100, 22);
            this.dtpFromDate.TabIndex = 170;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM//yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(222, 54);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 22);
            this.dtpEndDate.TabIndex = 167;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-1, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 14);
            this.label3.TabIndex = 169;
            this.label3.Text = "Từ ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(160, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 14);
            this.label4.TabIndex = 168;
            this.label4.Text = "Đến ngày";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterText.Location = new System.Drawing.Point(614, 55);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(211, 22);
            this.txtFilterText.TabIndex = 164;
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(831, 54);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(66, 23);
            this.btnFind.TabIndex = 165;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnFirst.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnFirst.Appearance.Options.UseBackColor = true;
            this.btnFirst.Appearance.Options.UseForeColor = true;
            this.btnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.ImageOptions.Image")));
            this.btnFirst.Location = new System.Drawing.Point(3, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnFirst.Size = new System.Drawing.Size(23, 23);
            this.btnFirst.TabIndex = 143;
            this.btnFirst.Text = "Trang trước";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.txtPageSize);
            this.panel6.Controls.Add(this.btnPrev);
            this.panel6.Controls.Add(this.btnFirst);
            this.panel6.Controls.Add(this.btnLast);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtPageNumber);
            this.panel6.Controls.Add(this.txtTotalPage);
            this.panel6.Controls.Add(this.btnNext);
            this.panel6.Location = new System.Drawing.Point(1186, 54);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(233, 27);
            this.panel6.TabIndex = 166;
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(189, 3);
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
            this.txtPageSize.Size = new System.Drawing.Size(43, 22);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.Location = new System.Drawing.Point(30, 2);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPrev.Size = new System.Drawing.Size(23, 23);
            this.btnPrev.TabIndex = 141;
            // 
            // btnLast
            // 
            this.btnLast.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnLast.Appearance.Options.UseBackColor = true;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.Location = new System.Drawing.Point(162, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnLast.Size = new System.Drawing.Size(23, 23);
            this.btnLast.TabIndex = 144;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(86, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(57, 3);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 20);
            this.txtPageNumber.TabIndex = 13;
            this.txtPageNumber.Text = "1";
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Location = new System.Drawing.Point(106, 3);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(25, 20);
            this.txtTotalPage.TabIndex = 12;
            this.txtTotalPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Appearance.Options.UseBackColor = true;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.Location = new System.Drawing.Point(135, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 142;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 14);
            this.label1.TabIndex = 171;
            this.label1.Text = "Lọc";
            // 
            // cbFilter
            // 
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "Chưa hoàn thành",
            "Hoàn thành",
            "Tất cả"});
            this.cbFilter.Location = new System.Drawing.Point(360, 56);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(189, 21);
            this.cbFilter.TabIndex = 173;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // frmListPONCCDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 733);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFilterText);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmListPONCCDetail";
            this.Text = "DANH SÁCH MUA HÀNG";
            this.Load += new System.EventHandler(this.frmListPONCCDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colPONCCCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPOKHNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colIDDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode_;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName_;
        private DevExpress.XtraGrid.Columns.GridColumn colPONCCID;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestBuyRTCID;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyRequest;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colFeeShip;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn coltotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colVat;
        private DevExpress.XtraGrid.Columns.GridColumn colVatMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceSale;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitRate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSLNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colSLcon;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colDateDelivery;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnXuat;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraGrid.Columns.GridColumn colProductNewCode;
        private DevExpress.XtraGrid.Columns.GridColumn ColStatus;
        private System.Windows.Forms.ComboBox cbFilter;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}