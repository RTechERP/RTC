
namespace BMS
{
    partial class frmPOKHHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOKHHistory));
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnImportExcel = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnExportExcel = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colIndexCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPONumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityDeliver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityPending = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPriceVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliverDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPur = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BarName = "Main menu";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main menu";
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
            this.btnExportExcel,
            this.btnImportExcel});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 28;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar2.BarAppearance.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportExcel)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Caption = "Nhập excel";
            this.btnImportExcel.Id = 27;
            this.btnImportExcel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnImportExcel.ImageOptions.SvgImage")));
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportExcel_ItemClick);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Caption = "Xuất excel";
            this.btnExportExcel.Id = 24;
            this.btnExportExcel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnExportExcel.ImageOptions.SvgImage")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportExcel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1215, 59);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 661);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1215, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 59);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 602);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1215, 59);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 602);
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
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.cboCustomer);
            this.panelControl1.Controls.Add(this.txtKeywords);
            this.panelControl1.Controls.Add(this.btnFind);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.dtpStartDate);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.dtpEndDate);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 59);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1215, 34);
            this.panelControl1.TabIndex = 66;
            // 
            // cboCustomer
            // 
            this.cboCustomer.EditValue = "";
            this.cboCustomer.Location = new System.Drawing.Point(407, 5);
            this.cboCustomer.MenuManager = this.barManager1;
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCustomer.Properties.NullText = "";
            this.cboCustomer.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboCustomer.Size = new System.Drawing.Size(273, 20);
            this.cboCustomer.TabIndex = 57;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên khách hàng";
            this.gridColumn2.FieldName = "CustomerName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 930;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã khách hàng";
            this.gridColumn3.FieldName = "CustomerCode";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 321;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Ký hiệu";
            this.gridColumn4.FieldName = "CustomerShortName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 329;
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(739, 5);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(319, 21);
            this.txtKeywords.TabIndex = 42;
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnFind.Location = new System.Drawing.Point(1064, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 46;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(686, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Từ khóa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Từ ngày";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(74, 5);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(95, 21);
            this.dtpStartDate.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Đến ngày";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(235, 5);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(97, 21);
            this.dtpEndDate.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(338, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Khách hàng";
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 93);
            this.grdData.MainView = this.grvData;
            this.grdData.MenuManager = this.barManager1;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1215, 568);
            this.grdData.TabIndex = 67;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCustomerCode,
            this.colIndexCode,
            this.colPONumber,
            this.colPODate,
            this.colProductCode,
            this.colModel,
            this.colQuantity,
            this.colQuantityDeliver,
            this.colQuantityPending,
            this.colUnit,
            this.colNetPrice,
            this.colUnitPrice,
            this.colTotalPrice,
            this.colVAT,
            this.colTotalPriceVAT,
            this.colDeliverDate,
            this.colPaymentDate,
            this.colBillDate,
            this.colBillNumber,
            this.colDept,
            this.colSale,
            this.colPur,
            this.colPOTypeCode});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colCustomerCode
            // 
            this.colCustomerCode.Caption = "Mã khách";
            this.colCustomerCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCustomerCode.FieldName = "CustomerCode";
            this.colCustomerCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colCustomerCode.Name = "colCustomerCode";
            this.colCustomerCode.Visible = true;
            this.colCustomerCode.VisibleIndex = 0;
            this.colCustomerCode.Width = 199;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colIndexCode
            // 
            this.colIndexCode.Caption = "Index";
            this.colIndexCode.FieldName = "IndexCode";
            this.colIndexCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colIndexCode.Name = "colIndexCode";
            this.colIndexCode.Visible = true;
            this.colIndexCode.VisibleIndex = 2;
            this.colIndexCode.Width = 189;
            // 
            // colPONumber
            // 
            this.colPONumber.Caption = "Số PO";
            this.colPONumber.FieldName = "PONumber";
            this.colPONumber.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colPONumber.Name = "colPONumber";
            this.colPONumber.Visible = true;
            this.colPONumber.VisibleIndex = 3;
            this.colPONumber.Width = 157;
            // 
            // colPODate
            // 
            this.colPODate.AppearanceCell.Options.UseTextOptions = true;
            this.colPODate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPODate.Caption = "Ngày PO";
            this.colPODate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colPODate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPODate.FieldName = "PODate";
            this.colPODate.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colPODate.Name = "colPODate";
            this.colPODate.Visible = true;
            this.colPODate.VisibleIndex = 4;
            this.colPODate.Width = 134;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Mã hàng";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 5;
            this.colProductCode.Width = 163;
            // 
            // colModel
            // 
            this.colModel.Caption = "Model";
            this.colModel.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colModel.FieldName = "Model";
            this.colModel.Name = "colModel";
            this.colModel.Visible = true;
            this.colModel.VisibleIndex = 6;
            this.colModel.Width = 82;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantity.Caption = "SL";
            this.colQuantity.DisplayFormat.FormatString = "N2";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 7;
            this.colQuantity.Width = 48;
            // 
            // colQuantityDeliver
            // 
            this.colQuantityDeliver.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityDeliver.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantityDeliver.Caption = "SL giao";
            this.colQuantityDeliver.DisplayFormat.FormatString = "N2";
            this.colQuantityDeliver.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantityDeliver.FieldName = "QuantityDeliver";
            this.colQuantityDeliver.Name = "colQuantityDeliver";
            this.colQuantityDeliver.Visible = true;
            this.colQuantityDeliver.VisibleIndex = 8;
            this.colQuantityDeliver.Width = 53;
            // 
            // colQuantityPending
            // 
            this.colQuantityPending.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityPending.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantityPending.Caption = "SL pending";
            this.colQuantityPending.DisplayFormat.FormatString = "N2";
            this.colQuantityPending.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantityPending.FieldName = "QuantityPending";
            this.colQuantityPending.Name = "colQuantityPending";
            this.colQuantityPending.Visible = true;
            this.colQuantityPending.VisibleIndex = 9;
            this.colQuantityPending.Width = 72;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "ĐVT";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 10;
            this.colUnit.Width = 58;
            // 
            // colNetPrice
            // 
            this.colNetPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colNetPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNetPrice.Caption = "Giá net";
            this.colNetPrice.DisplayFormat.FormatString = "N2";
            this.colNetPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNetPrice.FieldName = "NetPrice";
            this.colNetPrice.Name = "colNetPrice";
            this.colNetPrice.Visible = true;
            this.colNetPrice.VisibleIndex = 11;
            this.colNetPrice.Width = 111;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colUnitPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colUnitPrice.Caption = "Đơn giá";
            this.colUnitPrice.DisplayFormat.FormatString = "N2";
            this.colUnitPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 12;
            this.colUnitPrice.Width = 104;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalPrice.Caption = "Thành tiền";
            this.colTotalPrice.DisplayFormat.FormatString = "N2";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.VisibleIndex = 13;
            this.colTotalPrice.Width = 120;
            // 
            // colVAT
            // 
            this.colVAT.AppearanceCell.Options.UseTextOptions = true;
            this.colVAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colVAT.Caption = "VAT";
            this.colVAT.DisplayFormat.FormatString = "N2";
            this.colVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colVAT.FieldName = "VAT";
            this.colVAT.Name = "colVAT";
            this.colVAT.Visible = true;
            this.colVAT.VisibleIndex = 14;
            this.colVAT.Width = 84;
            // 
            // colTotalPriceVAT
            // 
            this.colTotalPriceVAT.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPriceVAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalPriceVAT.Caption = "Tổng tiền sau VAT";
            this.colTotalPriceVAT.DisplayFormat.FormatString = "N2";
            this.colTotalPriceVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPriceVAT.FieldName = "TotalPriceVAT";
            this.colTotalPriceVAT.Name = "colTotalPriceVAT";
            this.colTotalPriceVAT.Visible = true;
            this.colTotalPriceVAT.VisibleIndex = 15;
            this.colTotalPriceVAT.Width = 87;
            // 
            // colDeliverDate
            // 
            this.colDeliverDate.AppearanceCell.Options.UseTextOptions = true;
            this.colDeliverDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeliverDate.Caption = "Giao hành thực tế";
            this.colDeliverDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDeliverDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDeliverDate.FieldName = "DeliverDate";
            this.colDeliverDate.Name = "colDeliverDate";
            this.colDeliverDate.Visible = true;
            this.colDeliverDate.VisibleIndex = 16;
            this.colDeliverDate.Width = 99;
            // 
            // colPaymentDate
            // 
            this.colPaymentDate.AppearanceCell.Options.UseTextOptions = true;
            this.colPaymentDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPaymentDate.Caption = "Thanh toán thực tế";
            this.colPaymentDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colPaymentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPaymentDate.FieldName = "PaymentDate";
            this.colPaymentDate.Name = "colPaymentDate";
            this.colPaymentDate.Visible = true;
            this.colPaymentDate.VisibleIndex = 17;
            this.colPaymentDate.Width = 93;
            // 
            // colBillDate
            // 
            this.colBillDate.AppearanceCell.Options.UseTextOptions = true;
            this.colBillDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillDate.Caption = "Ngày hóa đơn";
            this.colBillDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colBillDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colBillDate.FieldName = "BillDate";
            this.colBillDate.Name = "colBillDate";
            this.colBillDate.Visible = true;
            this.colBillDate.VisibleIndex = 18;
            this.colBillDate.Width = 115;
            // 
            // colBillNumber
            // 
            this.colBillNumber.Caption = "Số hóa đơn";
            this.colBillNumber.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBillNumber.FieldName = "BillNumber";
            this.colBillNumber.Name = "colBillNumber";
            this.colBillNumber.Visible = true;
            this.colBillNumber.VisibleIndex = 19;
            this.colBillNumber.Width = 90;
            // 
            // colDept
            // 
            this.colDept.Caption = "Công nợ";
            this.colDept.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDept.FieldName = "Dept";
            this.colDept.Name = "colDept";
            this.colDept.Visible = true;
            this.colDept.VisibleIndex = 20;
            this.colDept.Width = 98;
            // 
            // colSale
            // 
            this.colSale.Caption = "Sale";
            this.colSale.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSale.FieldName = "Sale";
            this.colSale.Name = "colSale";
            this.colSale.Visible = true;
            this.colSale.VisibleIndex = 21;
            this.colSale.Width = 100;
            // 
            // colPur
            // 
            this.colPur.Caption = "Pur";
            this.colPur.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPur.FieldName = "Pur";
            this.colPur.Name = "colPur";
            this.colPur.Visible = true;
            this.colPur.VisibleIndex = 22;
            this.colPur.Width = 100;
            // 
            // colPOTypeCode
            // 
            this.colPOTypeCode.Caption = "Loại";
            this.colPOTypeCode.FieldName = "POTypeCode";
            this.colPOTypeCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colPOTypeCode.Name = "colPOTypeCode";
            this.colPOTypeCode.Visible = true;
            this.colPOTypeCode.VisibleIndex = 1;
            this.colPOTypeCode.Width = 128;
            // 
            // frmPOKHHistory
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 661);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmPOKHHistory";
            this.Text = "LỊCH SỬ POKH";
            this.Load += new System.EventHandler(this.frmPOKHHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarLargeButtonItem btnExportExcel;
        private DevExpress.XtraBars.BarLargeButtonItem btnImportExcel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colIndexCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPONumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPODate;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colModel;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityDeliver;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityPending;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colNetPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPriceVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliverDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPaymentDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBillDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDept;
        private DevExpress.XtraGrid.Columns.GridColumn colSale;
        private DevExpress.XtraGrid.Columns.GridColumn colPur;
        private DevExpress.XtraEditors.SearchLookUpEdit cboCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colPOTypeCode;
    }
}