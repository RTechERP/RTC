
namespace BMS
{
    partial class frmInventoryBorrowNCCDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventoryBorrowNCCDemo));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.cboNCC = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSupplierSaleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierSaleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierSaleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chiTiếtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiTiếtPhiếuNhậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiTiếtPhiếuXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiTiếtPhiếuMượnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductNewCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductRTCID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityReturnNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSuplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExportCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiverExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ExportCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExportDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillImportID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillExportID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInventoryLate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBorrowing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberReal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDBorrow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExportExcel);
            this.panel3.Controls.Add(this.cboNCC);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.dtpFromDate);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtFilterText);
            this.panel3.Controls.Add(this.btnFind);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.dtpEndDate);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1297, 36);
            this.panel3.TabIndex = 53;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(898, 10);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(95, 24);
            this.btnExportExcel.TabIndex = 159;
            this.btnExportExcel.Text = "Xuất excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // cboNCC
            // 
            this.cboNCC.Location = new System.Drawing.Point(396, 11);
            this.cboNCC.Margin = new System.Windows.Forms.Padding(2);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNCC.Properties.NullText = "";
            this.cboNCC.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboNCC.Size = new System.Drawing.Size(164, 20);
            this.cboNCC.TabIndex = 53;
            this.cboNCC.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.searchLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSupplierSaleID,
            this.colSupplierSaleCode,
            this.colSupplierSaleName});
            this.searchLookUpEdit1View.DetailHeight = 284;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colSupplierSaleID
            // 
            this.colSupplierSaleID.Caption = "SupplierSaleID";
            this.colSupplierSaleID.FieldName = "ID";
            this.colSupplierSaleID.Name = "colSupplierSaleID";
            // 
            // colSupplierSaleCode
            // 
            this.colSupplierSaleCode.Caption = "Mã nhà cung cấp";
            this.colSupplierSaleCode.FieldName = "CodeNCC";
            this.colSupplierSaleCode.Name = "colSupplierSaleCode";
            this.colSupplierSaleCode.Visible = true;
            this.colSupplierSaleCode.VisibleIndex = 0;
            this.colSupplierSaleCode.Width = 285;
            // 
            // colSupplierSaleName
            // 
            this.colSupplierSaleName.Caption = "Tên nhà cung cấp";
            this.colSupplierSaleName.FieldName = "NameNCC";
            this.colSupplierSaleName.Name = "colSupplierSaleName";
            this.colSupplierSaleName.Visible = true;
            this.colSupplierSaleName.VisibleIndex = 1;
            this.colSupplierSaleName.Width = 532;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.txtPageSize);
            this.panel6.Controls.Add(this.btnPrev);
            this.panel6.Controls.Add(this.btnFirst);
            this.panel6.Controls.Add(this.btnLast);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtPageNumber);
            this.panel6.Controls.Add(this.txtTotalPage);
            this.panel6.Controls.Add(this.btnNext);
            this.panel6.Location = new System.Drawing.Point(1009, 6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(276, 27);
            this.panel6.TabIndex = 158;
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(191, 3);
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
            this.txtPageSize.Size = new System.Drawing.Size(82, 20);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtPageSize.ValueChanged += new System.EventHandler(this.txtPageSize_ValueChanged);
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
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
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
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
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
            this.btnLast.Text = "`";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(86, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(57, 3);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 20);
            this.txtPageNumber.TabIndex = 13;
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
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(60, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(97, 20);
            this.dtpFromDate.TabIndex = 134;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(574, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 132;
            this.label5.Text = "Từ khóa";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(622, 11);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(191, 20);
            this.txtFilterText.TabIndex = 125;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(817, 10);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(64, 24);
            this.btnFind.TabIndex = 126;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(336, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 119;
            this.label2.Text = "Lọc NCC";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(230, 10);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(97, 20);
            this.dtpEndDate.TabIndex = 121;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "Từ ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(172, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 122;
            this.label4.Text = "Đến ngày";
            // 
            // grdData
            // 
            this.grdData.ContextMenuStrip = this.contextMenuStrip2;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.Location = new System.Drawing.Point(0, 36);
            this.grdData.MainView = this.grvMaster;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(1297, 389);
            this.grdData.TabIndex = 54;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chiTiếtToolStripMenuItem,
            this.chiTiếtPhiếuNhậpToolStripMenuItem,
            this.chiTiếtPhiếuXuấtToolStripMenuItem,
            this.chiTiếtPhiếuMượnToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(181, 92);
            // 
            // chiTiếtToolStripMenuItem
            // 
            this.chiTiếtToolStripMenuItem.Name = "chiTiếtToolStripMenuItem";
            this.chiTiếtToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chiTiếtToolStripMenuItem.Text = "Chi tiết";
            this.chiTiếtToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtToolStripMenuItem_Click);
            // 
            // chiTiếtPhiếuNhậpToolStripMenuItem
            // 
            this.chiTiếtPhiếuNhậpToolStripMenuItem.Name = "chiTiếtPhiếuNhậpToolStripMenuItem";
            this.chiTiếtPhiếuNhậpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chiTiếtPhiếuNhậpToolStripMenuItem.Text = "Chi tiết phiếu nhập";
            this.chiTiếtPhiếuNhậpToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtPhiếuNhậpToolStripMenuItem_Click);
            // 
            // chiTiếtPhiếuXuấtToolStripMenuItem
            // 
            this.chiTiếtPhiếuXuấtToolStripMenuItem.Name = "chiTiếtPhiếuXuấtToolStripMenuItem";
            this.chiTiếtPhiếuXuấtToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chiTiếtPhiếuXuấtToolStripMenuItem.Text = "Chi tiết phiếu xuất";
            this.chiTiếtPhiếuXuấtToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtPhiếuXuấtToolStripMenuItem_Click);
            // 
            // chiTiếtPhiếuMượnToolStripMenuItem
            // 
            this.chiTiếtPhiếuMượnToolStripMenuItem.Name = "chiTiếtPhiếuMượnToolStripMenuItem";
            this.chiTiếtPhiếuMượnToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chiTiếtPhiếuMượnToolStripMenuItem.Text = "Chi tiết phiếu mượn";
            this.chiTiếtPhiếuMượnToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtPhiếuMượnToolStripMenuItem_Click);
            // 
            // grvMaster
            // 
            this.grvMaster.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.grvMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvMaster.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvMaster.ColumnPanelRowHeight = 40;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductGroupName,
            this.colProductCode,
            this.colProductName,
            this.colProductNewCode,
            this.colProductRTCID,
            this.colImportCreateDate,
            this.colTotalQuantityReturnNCC,
            this.colSuplierName,
            this.colDeliver,
            this.colReceiver,
            this.colExportCode,
            this.colImportCode,
            this.colProjectName,
            this.colReceiverExport,
            this.ExportCreateDate,
            this.colImportDetailID,
            this.colExportDetailID,
            this.colBillImportID,
            this.colBillExportID,
            this.colNumber,
            this.colInventoryLate,
            this.colImport,
            this.colExport,
            this.colBorrowing,
            this.colNumberReal,
            this.colIDBorrow,
            this.colCustomerName});
            this.grvMaster.GridControl = this.grdData;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsCustomization.AllowRowSizing = true;
            this.grvMaster.OptionsView.AllowCellMerge = true;
            this.grvMaster.OptionsView.ColumnAutoWidth = false;
            this.grvMaster.OptionsView.RowAutoHeight = true;
            this.grvMaster.OptionsView.ShowAutoFilterRow = true;
            this.grvMaster.OptionsView.ShowFooter = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.grvMaster_CellMerge);
            this.grvMaster.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.grvMaster_CustomSummaryCalculate);
            // 
            // colProductGroupName
            // 
            this.colProductGroupName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductGroupName.AppearanceCell.Options.UseFont = true;
            this.colProductGroupName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductGroupName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductGroupName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colProductGroupName.AppearanceHeader.Options.UseFont = true;
            this.colProductGroupName.Caption = "Tên nhóm";
            this.colProductGroupName.FieldName = "ProductGroupName";
            this.colProductGroupName.Name = "colProductGroupName";
            this.colProductGroupName.Visible = true;
            this.colProductGroupName.VisibleIndex = 0;
            this.colProductGroupName.Width = 177;
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductCode.AppearanceCell.Options.UseFont = true;
            this.colProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã sản phẩm";
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 1;
            this.colProductCode.Width = 151;
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductName.AppearanceCell.Options.UseFont = true;
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.Caption = "Tên sản phẩm";
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 2;
            this.colProductName.Width = 291;
            // 
            // colProductNewCode
            // 
            this.colProductNewCode.AppearanceCell.Options.UseFont = true;
            this.colProductNewCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductNewCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductNewCode.Caption = "Mã nội bộ";
            this.colProductNewCode.FieldName = "ProductCodeRTC";
            this.colProductNewCode.Name = "colProductNewCode";
            this.colProductNewCode.Visible = true;
            this.colProductNewCode.VisibleIndex = 3;
            this.colProductNewCode.Width = 142;
            // 
            // colProductRTCID
            // 
            this.colProductRTCID.Caption = "gridColumn9";
            this.colProductRTCID.FieldName = "ProductRTCID";
            this.colProductRTCID.Name = "colProductRTCID";
            // 
            // colImportCreateDate
            // 
            this.colImportCreateDate.AppearanceCell.Options.UseTextOptions = true;
            this.colImportCreateDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colImportCreateDate.Caption = "Ngày nhập kho";
            this.colImportCreateDate.FieldName = "ImportCreateDate";
            this.colImportCreateDate.Name = "colImportCreateDate";
            this.colImportCreateDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colImportCreateDate.Visible = true;
            this.colImportCreateDate.VisibleIndex = 8;
            this.colImportCreateDate.Width = 126;
            // 
            // colTotalQuantityReturnNCC
            // 
            this.colTotalQuantityReturnNCC.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuantityReturnNCC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalQuantityReturnNCC.Caption = "SL Phải trả NCC";
            this.colTotalQuantityReturnNCC.FieldName = "TotalQuantityReturnNCC";
            this.colTotalQuantityReturnNCC.Name = "colTotalQuantityReturnNCC";
            this.colTotalQuantityReturnNCC.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.colTotalQuantityReturnNCC.OptionsColumn.ReadOnly = true;
            this.colTotalQuantityReturnNCC.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "TotalQuantityReturnNCC", "", "1")});
            this.colTotalQuantityReturnNCC.Visible = true;
            this.colTotalQuantityReturnNCC.VisibleIndex = 6;
            this.colTotalQuantityReturnNCC.Width = 82;
            // 
            // colSuplierName
            // 
            this.colSuplierName.Caption = "Nhà cung cấp";
            this.colSuplierName.FieldName = "SuplierSaleName";
            this.colSuplierName.Name = "colSuplierName";
            this.colSuplierName.Visible = true;
            this.colSuplierName.VisibleIndex = 4;
            this.colSuplierName.Width = 259;
            // 
            // colDeliver
            // 
            this.colDeliver.Caption = "Người giao";
            this.colDeliver.FieldName = "DeliverImport";
            this.colDeliver.Name = "colDeliver";
            this.colDeliver.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDeliver.Visible = true;
            this.colDeliver.VisibleIndex = 10;
            this.colDeliver.Width = 174;
            // 
            // colReceiver
            // 
            this.colReceiver.Caption = "Người nhận";
            this.colReceiver.FieldName = "ReceiverImport";
            this.colReceiver.Name = "colReceiver";
            this.colReceiver.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colReceiver.Visible = true;
            this.colReceiver.VisibleIndex = 9;
            this.colReceiver.Width = 175;
            // 
            // colExportCode
            // 
            this.colExportCode.Caption = "Số phiếu xuất";
            this.colExportCode.FieldName = "ExportCode";
            this.colExportCode.Name = "colExportCode";
            this.colExportCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colExportCode.Visible = true;
            this.colExportCode.VisibleIndex = 11;
            this.colExportCode.Width = 104;
            // 
            // colImportCode
            // 
            this.colImportCode.Caption = "Số phiếu nhập";
            this.colImportCode.FieldName = "ImportCode";
            this.colImportCode.Name = "colImportCode";
            this.colImportCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colImportCode.Visible = true;
            this.colImportCode.VisibleIndex = 7;
            this.colImportCode.Width = 134;
            // 
            // colProjectName
            // 
            this.colProjectName.Caption = "Tên dự án";
            this.colProjectName.FieldName = "ProjectName";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colProjectName.Visible = true;
            this.colProjectName.VisibleIndex = 14;
            this.colProjectName.Width = 204;
            // 
            // colReceiverExport
            // 
            this.colReceiverExport.Caption = "Người mượn (xuất)";
            this.colReceiverExport.FieldName = "ReceiverExport";
            this.colReceiverExport.Name = "colReceiverExport";
            this.colReceiverExport.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colReceiverExport.Visible = true;
            this.colReceiverExport.VisibleIndex = 13;
            this.colReceiverExport.Width = 128;
            // 
            // ExportCreateDate
            // 
            this.ExportCreateDate.AppearanceCell.Options.UseTextOptions = true;
            this.ExportCreateDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ExportCreateDate.Caption = "Ngày xuất kho";
            this.ExportCreateDate.FieldName = "ExportCreateDate";
            this.ExportCreateDate.Name = "ExportCreateDate";
            this.ExportCreateDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ExportCreateDate.Visible = true;
            this.ExportCreateDate.VisibleIndex = 12;
            this.ExportCreateDate.Width = 99;
            // 
            // colImportDetailID
            // 
            this.colImportDetailID.Caption = "ImportDetailID";
            this.colImportDetailID.FieldName = "ImportDetailID";
            this.colImportDetailID.Name = "colImportDetailID";
            this.colImportDetailID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colExportDetailID
            // 
            this.colExportDetailID.Caption = "ExportDetailID";
            this.colExportDetailID.FieldName = "ExportDetailID";
            this.colExportDetailID.Name = "colExportDetailID";
            this.colExportDetailID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colBillImportID
            // 
            this.colBillImportID.Caption = "BillImportID";
            this.colBillImportID.FieldName = "BillImportID";
            this.colBillImportID.Name = "colBillImportID";
            // 
            // colBillExportID
            // 
            this.colBillExportID.Caption = "BillExportID";
            this.colBillExportID.FieldName = "BillExportID";
            this.colBillExportID.Name = "colBillExportID";
            // 
            // colNumber
            // 
            this.colNumber.Caption = "colNumber";
            this.colNumber.FieldName = "Number";
            this.colNumber.Name = "colNumber";
            // 
            // colInventoryLate
            // 
            this.colInventoryLate.Caption = "colInventoryLate";
            this.colInventoryLate.FieldName = "InventoryLate";
            this.colInventoryLate.Name = "colInventoryLate";
            // 
            // colImport
            // 
            this.colImport.Caption = "colImport";
            this.colImport.FieldName = "NumberImport";
            this.colImport.Name = "colImport";
            // 
            // colExport
            // 
            this.colExport.Caption = "colExport";
            this.colExport.FieldName = "NumberExport";
            this.colExport.Name = "colExport";
            // 
            // colBorrowing
            // 
            this.colBorrowing.Caption = "colBorrowing";
            this.colBorrowing.FieldName = "NumberBorrowing";
            this.colBorrowing.Name = "colBorrowing";
            // 
            // colNumberReal
            // 
            this.colNumberReal.Caption = "colNumberReal";
            this.colNumberReal.FieldName = "InventoryReal";
            this.colNumberReal.Name = "colNumberReal";
            // 
            // colIDBorrow
            // 
            this.colIDBorrow.Caption = "colIDBorrow";
            this.colIDBorrow.FieldName = "BorrowID";
            this.colIDBorrow.Name = "colIDBorrow";
            // 
            // colCustomerName
            // 
            this.colCustomerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCustomerName.AppearanceHeader.Options.UseFont = true;
            this.colCustomerName.Caption = "Tên khách hàng";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 5;
            this.colCustomerName.Width = 208;
            // 
            // frmInventoryBorrowNCCDemo
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 425);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmInventoryBorrowNCCDemo";
            this.Text = "BÁO CÁO MƯỢN NCC KHO DEMO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInventoryBorrowNCCDemo_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExportExcel;
        private DevExpress.XtraEditors.SearchLookUpEdit cboNCC;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierSaleID;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierSaleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierSaleName;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colProductGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductNewCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRTCID;
        private DevExpress.XtraGrid.Columns.GridColumn colImportCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityReturnNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colSuplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliver;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiver;
        private DevExpress.XtraGrid.Columns.GridColumn colExportCode;
        private DevExpress.XtraGrid.Columns.GridColumn colImportCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiverExport;
        private DevExpress.XtraGrid.Columns.GridColumn ExportCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colImportDetailID;
        private DevExpress.XtraGrid.Columns.GridColumn colExportDetailID;
        private DevExpress.XtraGrid.Columns.GridColumn colBillImportID;
        private DevExpress.XtraGrid.Columns.GridColumn colBillExportID;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtPhiếuNhậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtPhiếuXuấtToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn colNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colInventoryLate;
        private DevExpress.XtraGrid.Columns.GridColumn colImport;
        private DevExpress.XtraGrid.Columns.GridColumn colExport;
        private DevExpress.XtraGrid.Columns.GridColumn colBorrowing;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberReal;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtPhiếuMượnToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn colIDBorrow;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
    }
}