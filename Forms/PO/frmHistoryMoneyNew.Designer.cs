
namespace BMS
{
    partial class frmHistoryMoneyNew
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.cboPOCode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtMoneyDate = new DevExpress.XtraEditors.DateEdit();
            this.lbMoneyDate = new DevExpress.XtraEditors.LabelControl();
            this.txtMoneyNotPaid = new System.Windows.Forms.TextBox();
            this.lbMoneyNotPaid = new System.Windows.Forms.Label();
            this.txtMoneyPaid = new System.Windows.Forms.TextBox();
            this.lbMoneyPaid = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtBillCode = new System.Windows.Forms.TextBox();
            this.lbBillCode = new System.Windows.Forms.Label();
            this.lbPO = new System.Windows.Forms.Label();
            this.grdProductDetail = new DevExpress.XtraGrid.GridControl();
            this.grvProductDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpec = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOKHDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIntoMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMoneyPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMoneyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMoneyNotPaid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.btnSave = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnExportExcel = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPOCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMoneyDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMoneyDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 59);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.AutoScroll = true;
            this.splitContainerControl1.Panel1.Controls.Add(this.cboPOCode);
            this.splitContainerControl1.Panel1.Controls.Add(this.dtMoneyDate);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbMoneyDate);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtMoneyNotPaid);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbMoneyNotPaid);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtMoneyPaid);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbMoneyPaid);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtBillCode);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbBillCode);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbPO);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdProductDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1334, 597);
            this.splitContainerControl1.SplitterPosition = 128;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // cboPOCode
            // 
            this.cboPOCode.EditValue = "";
            this.cboPOCode.Location = new System.Drawing.Point(98, 27);
            this.cboPOCode.Name = "cboPOCode";
            this.cboPOCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPOCode.Properties.NullText = "";
            this.cboPOCode.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboPOCode.Size = new System.Drawing.Size(185, 20);
            this.cboPOCode.TabIndex = 13;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dtMoneyDate
            // 
            this.dtMoneyDate.EditValue = null;
            this.dtMoneyDate.Location = new System.Drawing.Point(1014, 28);
            this.dtMoneyDate.Name = "dtMoneyDate";
            this.dtMoneyDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMoneyDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMoneyDate.Size = new System.Drawing.Size(188, 20);
            this.dtMoneyDate.TabIndex = 12;
            // 
            // lbMoneyDate
            // 
            this.lbMoneyDate.Location = new System.Drawing.Point(889, 30);
            this.lbMoneyDate.Name = "lbMoneyDate";
            this.lbMoneyDate.Size = new System.Drawing.Size(81, 13);
            this.lbMoneyDate.TabIndex = 11;
            this.lbMoneyDate.Text = "Ngày thanh toán";
            // 
            // txtMoneyNotPaid
            // 
            this.txtMoneyNotPaid.Location = new System.Drawing.Point(593, 72);
            this.txtMoneyNotPaid.Name = "txtMoneyNotPaid";
            this.txtMoneyNotPaid.ReadOnly = true;
            this.txtMoneyNotPaid.Size = new System.Drawing.Size(188, 21);
            this.txtMoneyNotPaid.TabIndex = 9;
            // 
            // lbMoneyNotPaid
            // 
            this.lbMoneyNotPaid.AutoSize = true;
            this.lbMoneyNotPaid.Location = new System.Drawing.Point(481, 75);
            this.lbMoneyNotPaid.Name = "lbMoneyNotPaid";
            this.lbMoneyNotPaid.Size = new System.Drawing.Size(73, 13);
            this.lbMoneyNotPaid.TabIndex = 8;
            this.lbMoneyNotPaid.Text = "Số tiền còn lại";
            // 
            // txtMoneyPaid
            // 
            this.txtMoneyPaid.Location = new System.Drawing.Point(593, 27);
            this.txtMoneyPaid.Name = "txtMoneyPaid";
            this.txtMoneyPaid.Size = new System.Drawing.Size(188, 21);
            this.txtMoneyPaid.TabIndex = 7;
            this.txtMoneyPaid.TextChanged += new System.EventHandler(this.txtMoneyPaid_TextChanged);
            // 
            // lbMoneyPaid
            // 
            this.lbMoneyPaid.AutoSize = true;
            this.lbMoneyPaid.Location = new System.Drawing.Point(481, 30);
            this.lbMoneyPaid.Name = "lbMoneyPaid";
            this.lbMoneyPaid.Size = new System.Drawing.Size(77, 13);
            this.lbMoneyPaid.TabIndex = 6;
            this.lbMoneyPaid.Text = "Đã thanh toán";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(302, 70);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(302, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Thêm thông tin";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtBillCode
            // 
            this.txtBillCode.Location = new System.Drawing.Point(98, 67);
            this.txtBillCode.Name = "txtBillCode";
            this.txtBillCode.Size = new System.Drawing.Size(185, 21);
            this.txtBillCode.TabIndex = 3;
            // 
            // lbBillCode
            // 
            this.lbBillCode.AutoSize = true;
            this.lbBillCode.Location = new System.Drawing.Point(21, 70);
            this.lbBillCode.Name = "lbBillCode";
            this.lbBillCode.Size = new System.Drawing.Size(66, 13);
            this.lbBillCode.TabIndex = 2;
            this.lbBillCode.Text = "Mã Hóa Đơn";
            // 
            // lbPO
            // 
            this.lbPO.AutoSize = true;
            this.lbPO.Location = new System.Drawing.Point(21, 30);
            this.lbPO.Name = "lbPO";
            this.lbPO.Size = new System.Drawing.Size(38, 13);
            this.lbPO.TabIndex = 0;
            this.lbPO.Text = "Mã PO";
            // 
            // grdProductDetail
            // 
            this.grdProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProductDetail.Location = new System.Drawing.Point(0, 0);
            this.grdProductDetail.MainView = this.grvProductDetail;
            this.grdProductDetail.Name = "grdProductDetail";
            this.grdProductDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1});
            this.grdProductDetail.Size = new System.Drawing.Size(1334, 459);
            this.grdProductDetail.TabIndex = 0;
            this.grdProductDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProductDetail});
            // 
            // grvProductDetail
            // 
            this.grvProductDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCustomerName,
            this.colPOId,
            this.colSpec,
            this.colUserId,
            this.colProductID,
            this.colPOKHDetailID,
            this.colQty,
            this.colUnitPrice,
            this.colIntoMoney,
            this.colMoneyPaid,
            this.colVAT,
            this.colMoneyDate,
            this.colBankName,
            this.colMoneyNotPaid,
            this.colNote});
            this.grvProductDetail.GridControl = this.grdProductDetail;
            this.grvProductDetail.Name = "grvProductDetail";
            this.grvProductDetail.OptionsView.ShowFooter = true;
            this.grvProductDetail.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.grvProductDetail_RowClick);
            // 
            // colCustomerName
            // 
            this.colCustomerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCustomerName.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colCustomerName.AppearanceHeader.Options.UseFont = true;
            this.colCustomerName.AppearanceHeader.Options.UseForeColor = true;
            this.colCustomerName.AppearanceHeader.Options.UseTextOptions = true;
            this.colCustomerName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomerName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerName.Caption = "Khách hàng";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 0;
            // 
            // colPOId
            // 
            this.colPOId.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPOId.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colPOId.AppearanceHeader.Options.UseFont = true;
            this.colPOId.AppearanceHeader.Options.UseForeColor = true;
            this.colPOId.AppearanceHeader.Options.UseTextOptions = true;
            this.colPOId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPOId.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPOId.Caption = "Mã PO";
            this.colPOId.FieldName = "POId";
            this.colPOId.Name = "colPOId";
            this.colPOId.OptionsColumn.AllowEdit = false;
            this.colPOId.Visible = true;
            this.colPOId.VisibleIndex = 1;
            // 
            // colSpec
            // 
            this.colSpec.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSpec.AppearanceHeader.Options.UseFont = true;
            this.colSpec.AppearanceHeader.Options.UseForeColor = true;
            this.colSpec.AppearanceHeader.Options.UseTextOptions = true;
            this.colSpec.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSpec.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSpec.Caption = "Người phụ trách";
            this.colSpec.FieldName = "FullName";
            this.colSpec.Name = "colSpec";
            this.colSpec.OptionsColumn.AllowEdit = false;
            this.colSpec.Visible = true;
            this.colSpec.VisibleIndex = 2;
            // 
            // colUserId
            // 
            this.colUserId.Caption = "ID người dùng";
            this.colUserId.FieldName = "UserID";
            this.colUserId.Name = "colUserId";
            // 
            // colProductID
            // 
            this.colProductID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colProductID.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colProductID.AppearanceHeader.Options.UseFont = true;
            this.colProductID.AppearanceHeader.Options.UseForeColor = true;
            this.colProductID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductID.Caption = "Mã hàng";
            this.colProductID.FieldName = "ProductID";
            this.colProductID.Name = "colProductID";
            this.colProductID.OptionsColumn.AllowEdit = false;
            this.colProductID.Visible = true;
            this.colProductID.VisibleIndex = 3;
            // 
            // colPOKHDetailID
            // 
            this.colPOKHDetailID.Caption = "POKHDetailID";
            this.colPOKHDetailID.FieldName = "POKHDetailID";
            this.colPOKHDetailID.Name = "colPOKHDetailID";
            // 
            // colQty
            // 
            this.colQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQty.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colQty.AppearanceHeader.Options.UseFont = true;
            this.colQty.AppearanceHeader.Options.UseForeColor = true;
            this.colQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.Caption = "Số lượng";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowEdit = false;
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 4;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUnitPrice.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colUnitPrice.AppearanceHeader.Options.UseFont = true;
            this.colUnitPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colUnitPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnitPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnitPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnitPrice.Caption = "Đơn giá trước VAT";
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.OptionsColumn.AllowEdit = false;
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 5;
            // 
            // colIntoMoney
            // 
            this.colIntoMoney.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIntoMoney.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colIntoMoney.AppearanceHeader.Options.UseFont = true;
            this.colIntoMoney.AppearanceHeader.Options.UseForeColor = true;
            this.colIntoMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.colIntoMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIntoMoney.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIntoMoney.Caption = "Tổng tiền trước VAT";
            this.colIntoMoney.FieldName = "IntoMoney";
            this.colIntoMoney.Name = "colIntoMoney";
            this.colIntoMoney.OptionsColumn.AllowEdit = false;
            this.colIntoMoney.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IntoMoney", "{0:0.##}")});
            this.colIntoMoney.Visible = true;
            this.colIntoMoney.VisibleIndex = 6;
            // 
            // colMoneyPaid
            // 
            this.colMoneyPaid.Caption = "Tiền đã thanh toán";
            this.colMoneyPaid.FieldName = "MoneyPaid";
            this.colMoneyPaid.Name = "colMoneyPaid";
            // 
            // colVAT
            // 
            this.colVAT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVAT.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colVAT.AppearanceHeader.Options.UseFont = true;
            this.colVAT.AppearanceHeader.Options.UseForeColor = true;
            this.colVAT.AppearanceHeader.Options.UseTextOptions = true;
            this.colVAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVAT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colVAT.Caption = "VAT";
            this.colVAT.FieldName = "VAT";
            this.colVAT.Name = "colVAT";
            this.colVAT.Visible = true;
            this.colVAT.VisibleIndex = 9;
            // 
            // colMoneyDate
            // 
            this.colMoneyDate.Caption = "Ngày thanh toán";
            this.colMoneyDate.FieldName = "MoneyDate";
            this.colMoneyDate.Name = "colMoneyDate";
            // 
            // colBankName
            // 
            this.colBankName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colBankName.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colBankName.AppearanceHeader.Options.UseFont = true;
            this.colBankName.AppearanceHeader.Options.UseForeColor = true;
            this.colBankName.AppearanceHeader.Options.UseTextOptions = true;
            this.colBankName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBankName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBankName.Caption = "Ngân hàng thanh toán";
            this.colBankName.FieldName = "BankName";
            this.colBankName.Name = "colBankName";
            this.colBankName.Visible = true;
            this.colBankName.VisibleIndex = 7;
            // 
            // colMoneyNotPaid
            // 
            this.colMoneyNotPaid.Caption = "Tiền chưa thanh toán";
            this.colMoneyNotPaid.FieldName = "MoneyNotPaid";
            this.colMoneyNotPaid.Name = "colMoneyNotPaid";
            // 
            // colNote
            // 
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNote.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.Caption = "Ghi chú";
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 8;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(277, 140);
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar1.BarAppearance.Normal.Options.UseFont = true;
            this.bar1.BarName = "Main menu";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(277, 140);
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar3.BarAppearance.Normal.Options.UseFont = true;
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.FloatLocation = new System.Drawing.Point(277, 140);
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Main menu";
            // 
            // popupMenu1
            // 
            this.popupMenu1.Manager = this.barManager2;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar4});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSave,
            this.btnExportExcel});
            this.barManager2.MainMenu = this.bar4;
            this.barManager2.MaxItemId = 17;
            this.barManager2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            // 
            // bar4
            // 
            this.bar4.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar4.BarAppearance.Normal.Options.UseFont = true;
            this.bar4.BarName = "Main menu";
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar4.FloatLocation = new System.Drawing.Point(477, 133);
            this.bar4.FloatSize = new System.Drawing.Size(359, 61);
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExportExcel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar4.OptionsBar.MultiLine = true;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Main menu";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "LƯU";
            this.btnSave.Id = 12;
            this.btnSave.ImageOptions.SvgImage = global::Forms.Properties.Resources.import1;
            this.btnSave.MinSize = new System.Drawing.Size(80, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Caption = "Xuất Excel";
            this.btnExportExcel.Id = 16;
            this.btnExportExcel.ImageOptions.Image = global::Forms.Properties.Resources.ExportToXLS_32x321;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportExcel_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager2;
            this.barDockControl1.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControl1.Size = new System.Drawing.Size(1334, 59);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 656);
            this.barDockControl2.Manager = this.barManager2;
            this.barDockControl2.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControl2.Size = new System.Drawing.Size(1334, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 59);
            this.barDockControl3.Manager = this.barManager2;
            this.barDockControl3.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControl3.Size = new System.Drawing.Size(0, 597);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1334, 59);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControl4.Size = new System.Drawing.Size(0, 597);
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
            // frmHistoryMoneyNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 656);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "frmHistoryMoneyNew";
            this.Text = "frmHistoryMoneyNew";
            this.Load += new System.EventHandler(this.frmHistoryMoneyNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboPOCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMoneyDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMoneyDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TextBox txtMoneyNotPaid;
        private System.Windows.Forms.Label lbMoneyNotPaid;
        private System.Windows.Forms.TextBox txtMoneyPaid;
        private System.Windows.Forms.Label lbMoneyPaid;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtBillCode;
        private System.Windows.Forms.Label lbBillCode;
        private System.Windows.Forms.Label lbPO;
        private DevExpress.XtraGrid.GridControl grdProductDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProductDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colPOId;
        private DevExpress.XtraGrid.Columns.GridColumn colSpec;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colIntoMoney;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colUserId;
        private DevExpress.XtraGrid.Columns.GridColumn colBankName;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraEditors.DateEdit dtMoneyDate;
        private DevExpress.XtraEditors.LabelControl lbMoneyDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPOKHDetailID;
        private DevExpress.XtraEditors.SearchLookUpEdit cboPOCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colMoneyNotPaid;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.BarLargeButtonItem btnSave;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarLargeButtonItem btnExportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colMoneyPaid;
        private DevExpress.XtraGrid.Columns.GridColumn colMoneyDate;
    }
}