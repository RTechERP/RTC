using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    partial class frmChitietSanPhamSaleForKT
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumberDauKy = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grdDataImport = new DevExpress.XtraGrid.GridControl();
            this.grvDataImport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSophieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgaytao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhacungcap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdDataExport = new DevExpress.XtraGrid.GridControl();
            this.grvDataExport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyXuat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExportID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnameStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbProductCodeNew = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataImport)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataExport)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbProductCodeNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 18);
            this.label5.TabIndex = 132;
            this.label5.Text = "Mã  nội bộ :";
            // 
            // txtNumberDauKy
            // 
            this.txtNumberDauKy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberDauKy.Location = new System.Drawing.Point(714, 15);
            this.txtNumberDauKy.Margin = new System.Windows.Forms.Padding(2);
            this.txtNumberDauKy.Name = "txtNumberDauKy";
            this.txtNumberDauKy.ReadOnly = true;
            this.txtNumberDauKy.Size = new System.Drawing.Size(211, 26);
            this.txtNumberDauKy.TabIndex = 131;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(587, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 18);
            this.label7.TabIndex = 130;
            this.label7.Text = "Tồn kho đầu kỳ :";
            // 
            // grdDataImport
            // 
            this.grdDataImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDataImport.Location = new System.Drawing.Point(3, 16);
            this.grdDataImport.MainView = this.grvDataImport;
            this.grdDataImport.Name = "grdDataImport";
            this.grdDataImport.Size = new System.Drawing.Size(555, 569);
            this.grdDataImport.TabIndex = 135;
            this.grdDataImport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDataImport});
            // 
            // grvDataImport
            // 
            this.grvDataImport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSophieu,
            this.colNgaytao,
            this.colNhacungcap,
            this.colQty,
            this.colStatus,
            this.colImportID});
            this.grvDataImport.DetailHeight = 284;
            this.grvDataImport.GridControl = this.grdDataImport;
            this.grvDataImport.Name = "grvDataImport";
            this.grvDataImport.OptionsView.ShowAutoFilterRow = true;
            this.grvDataImport.OptionsView.ShowFooter = true;
            this.grvDataImport.OptionsView.ShowGroupPanel = false;
            this.grvDataImport.DoubleClick += new System.EventHandler(this.grvDataImport_DoubleClick);
            // 
            // colSophieu
            // 
            this.colSophieu.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSophieu.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSophieu.AppearanceHeader.Options.UseFont = true;
            this.colSophieu.AppearanceHeader.Options.UseForeColor = true;
            this.colSophieu.AppearanceHeader.Options.UseTextOptions = true;
            this.colSophieu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSophieu.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSophieu.Caption = "Số phiếu";
            this.colSophieu.FieldName = "BillImportCode";
            this.colSophieu.MinWidth = 15;
            this.colSophieu.Name = "colSophieu";
            this.colSophieu.OptionsColumn.ReadOnly = true;
            this.colSophieu.Visible = true;
            this.colSophieu.VisibleIndex = 0;
            this.colSophieu.Width = 56;
            // 
            // colNgaytao
            // 
            this.colNgaytao.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNgaytao.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNgaytao.AppearanceHeader.Options.UseFont = true;
            this.colNgaytao.AppearanceHeader.Options.UseForeColor = true;
            this.colNgaytao.AppearanceHeader.Options.UseTextOptions = true;
            this.colNgaytao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgaytao.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNgaytao.Caption = "Ngày tạo";
            this.colNgaytao.FieldName = "CreatDate";
            this.colNgaytao.MinWidth = 15;
            this.colNgaytao.Name = "colNgaytao";
            this.colNgaytao.OptionsColumn.ReadOnly = true;
            this.colNgaytao.Visible = true;
            this.colNgaytao.VisibleIndex = 1;
            this.colNgaytao.Width = 56;
            // 
            // colNhacungcap
            // 
            this.colNhacungcap.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNhacungcap.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNhacungcap.AppearanceHeader.Options.UseFont = true;
            this.colNhacungcap.AppearanceHeader.Options.UseForeColor = true;
            this.colNhacungcap.AppearanceHeader.Options.UseTextOptions = true;
            this.colNhacungcap.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNhacungcap.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNhacungcap.Caption = "Nhà cung cấp";
            this.colNhacungcap.FieldName = "Suplier";
            this.colNhacungcap.MinWidth = 15;
            this.colNhacungcap.Name = "colNhacungcap";
            this.colNhacungcap.OptionsColumn.ReadOnly = true;
            this.colNhacungcap.Visible = true;
            this.colNhacungcap.VisibleIndex = 2;
            this.colNhacungcap.Width = 56;
            // 
            // colQty
            // 
            this.colQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQty.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQty.AppearanceHeader.Options.UseFont = true;
            this.colQty.AppearanceHeader.Options.UseForeColor = true;
            this.colQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.Caption = "Số lượng";
            this.colQty.FieldName = "Qty";
            this.colQty.MinWidth = 15;
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.ReadOnly = true;
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "SUM={0:0.##}")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 3;
            this.colQty.Width = 56;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.Caption = "Duyệt";
            this.colStatus.FieldName = "Status";
            this.colStatus.MinWidth = 15;
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 4;
            this.colStatus.Width = 56;
            // 
            // colImportID
            // 
            this.colImportID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colImportID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colImportID.AppearanceHeader.Options.UseFont = true;
            this.colImportID.AppearanceHeader.Options.UseForeColor = true;
            this.colImportID.AppearanceHeader.Options.UseTextOptions = true;
            this.colImportID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colImportID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colImportID.Caption = "gridColumn3";
            this.colImportID.FieldName = "ID";
            this.colImportID.MinWidth = 15;
            this.colImportID.Name = "colImportID";
            this.colImportID.Width = 56;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdDataImport);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(561, 588);
            this.groupBox2.TabIndex = 137;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phiếu nhập";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdDataExport);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(570, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(561, 588);
            this.groupBox3.TabIndex = 138;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Phiếu Xuất";
            // 
            // grdDataExport
            // 
            this.grdDataExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDataExport.Location = new System.Drawing.Point(3, 16);
            this.grdDataExport.MainView = this.grvDataExport;
            this.grdDataExport.Name = "grdDataExport";
            this.grdDataExport.Size = new System.Drawing.Size(555, 569);
            this.grdDataExport.TabIndex = 135;
            this.grdDataExport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDataExport});
            // 
            // grvDataExport
            // 
            this.grvDataExport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colCreatDate,
            this.colQtyXuat,
            this.colIsApproved,
            this.colExportID,
            this.colnameStatus,
            this.colCustomer});
            this.grvDataExport.DetailHeight = 284;
            this.grvDataExport.GridControl = this.grdDataExport;
            this.grvDataExport.Name = "grvDataExport";
            this.grvDataExport.OptionsView.ShowAutoFilterRow = true;
            this.grvDataExport.OptionsView.ShowFooter = true;
            this.grvDataExport.OptionsView.ShowGroupPanel = false;
            this.grvDataExport.DoubleClick += new System.EventHandler(this.grvDataExport_DoubleClick);
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Số phiếu";
            this.colCode.FieldName = "Code";
            this.colCode.MinWidth = 15;
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.ReadOnly = true;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 256;
            // 
            // colCreatDate
            // 
            this.colCreatDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCreatDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreatDate.AppearanceHeader.Options.UseFont = true;
            this.colCreatDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatDate.Caption = "Ngày tạo";
            this.colCreatDate.FieldName = "CreatDate";
            this.colCreatDate.MinWidth = 15;
            this.colCreatDate.Name = "colCreatDate";
            this.colCreatDate.OptionsColumn.ReadOnly = true;
            this.colCreatDate.Visible = true;
            this.colCreatDate.VisibleIndex = 2;
            this.colCreatDate.Width = 256;
            // 
            // colQtyXuat
            // 
            this.colQtyXuat.AppearanceCell.Options.UseTextOptions = true;
            this.colQtyXuat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyXuat.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colQtyXuat.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQtyXuat.AppearanceHeader.Options.UseFont = true;
            this.colQtyXuat.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyXuat.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyXuat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyXuat.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyXuat.Caption = "Số lượng";
            this.colQtyXuat.FieldName = "Qty";
            this.colQtyXuat.MinWidth = 15;
            this.colQtyXuat.Name = "colQtyXuat";
            this.colQtyXuat.OptionsColumn.ReadOnly = true;
            this.colQtyXuat.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "SUM={0:0.##}")});
            this.colQtyXuat.Visible = true;
            this.colQtyXuat.VisibleIndex = 3;
            this.colQtyXuat.Width = 256;
            // 
            // colIsApproved
            // 
            this.colIsApproved.AppearanceCell.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colIsApproved.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIsApproved.AppearanceHeader.Options.UseFont = true;
            this.colIsApproved.AppearanceHeader.Options.UseForeColor = true;
            this.colIsApproved.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsApproved.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.Caption = "Duyệt";
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.MinWidth = 15;
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.OptionsColumn.ReadOnly = true;
            this.colIsApproved.Visible = true;
            this.colIsApproved.VisibleIndex = 4;
            this.colIsApproved.Width = 134;
            // 
            // colExportID
            // 
            this.colExportID.AppearanceCell.Options.UseTextOptions = true;
            this.colExportID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExportID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colExportID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colExportID.AppearanceHeader.Options.UseFont = true;
            this.colExportID.AppearanceHeader.Options.UseForeColor = true;
            this.colExportID.AppearanceHeader.Options.UseTextOptions = true;
            this.colExportID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExportID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExportID.Caption = "gridColumn3";
            this.colExportID.FieldName = "ID";
            this.colExportID.MinWidth = 15;
            this.colExportID.Name = "colExportID";
            this.colExportID.Width = 56;
            // 
            // colnameStatus
            // 
            this.colnameStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colnameStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnameStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colnameStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colnameStatus.AppearanceHeader.Options.UseFont = true;
            this.colnameStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colnameStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colnameStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colnameStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnameStatus.Caption = "Trạng thái";
            this.colnameStatus.FieldName = "nameStatus";
            this.colnameStatus.Name = "colnameStatus";
            this.colnameStatus.OptionsColumn.ReadOnly = true;
            this.colnameStatus.Visible = true;
            this.colnameStatus.VisibleIndex = 0;
            this.colnameStatus.Width = 236;
            // 
            // colCustomer
            // 
            this.colCustomer.AppearanceCell.Options.UseTextOptions = true;
            this.colCustomer.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCustomer.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCustomer.AppearanceHeader.Options.UseFont = true;
            this.colCustomer.AppearanceHeader.Options.UseForeColor = true;
            this.colCustomer.AppearanceHeader.Options.UseTextOptions = true;
            this.colCustomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomer.Caption = "Khách Hàng";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.OptionsColumn.ReadOnly = true;
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 5;
            this.colCustomer.Width = 477;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 52);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1134, 594);
            this.tableLayoutPanel1.TabIndex = 137;
            // 
            // cbProductCodeNew
            // 
            this.cbProductCodeNew.EditValue = "a";
            this.cbProductCodeNew.Location = new System.Drawing.Point(105, 16);
            this.cbProductCodeNew.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductCodeNew.Name = "cbProductCodeNew";
            this.cbProductCodeNew.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductCodeNew.Properties.Appearance.Options.UseFont = true;
            this.cbProductCodeNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProductCodeNew.Properties.NullText = "";
            this.cbProductCodeNew.Properties.PopupView = this.searchLookUpEdit1View;
            this.cbProductCodeNew.Properties.ReadOnly = true;
            this.cbProductCodeNew.Size = new System.Drawing.Size(409, 24);
            this.cbProductCodeNew.TabIndex = 143;
            this.cbProductCodeNew.EditValueChanged += new System.EventHandler(this.cbProductCode_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 30;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.searchLookUpEdit1View.DetailHeight = 284;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ProductSaleID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã sản phẩm";
            this.gridColumn2.FieldName = "ProductCode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tên sản phẩm";
            this.gridColumn3.FieldName = "ProductName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtNumberDauKy);
            this.panel1.Controls.Add(this.cbProductCodeNew);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1134, 46);
            this.panel1.TabIndex = 144;
            // 
            // frmChitietSanPhamSaleForKT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 647);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmChitietSanPhamSaleForKT";
            this.Text = "Lịch sử Nhập Xuất Sản phẩm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmChiTietSanPhamSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataImport)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataExport)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbProductCodeNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNumberDauKy;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl grdDataImport;
        private GridView grvDataImport;
        private DevExpress.XtraGrid.Columns.GridColumn colSophieu;
        private DevExpress.XtraGrid.Columns.GridColumn colNgaytao;
        private DevExpress.XtraGrid.Columns.GridColumn colNhacungcap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraGrid.GridControl grdDataExport;
        private GridView grvDataExport;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private SearchLookUpEdit cbProductCodeNew;
        private GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyXuat;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colImportID;
        private DevExpress.XtraGrid.Columns.GridColumn colExportID;
        private DevExpress.XtraGrid.Columns.GridColumn colnameStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.Panel panel1;
    }
}