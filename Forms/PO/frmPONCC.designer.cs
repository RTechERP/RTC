namespace BMS
{
    partial class frmPONCC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPONCC));
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.btnNewGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteGroup = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIsApproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelApproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bntEditExcel = new System.Windows.Forms.ToolStripButton();
            this.cbChoseExcel = new System.Windows.Forms.ToolStripComboBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
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
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDMaster = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colReceivedDatePO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliveryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalMoneyPO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeIDID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddressDelivery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRulePay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankingFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFedexAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierVoucher = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOriginItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRuleIncoterm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdDetail = new DevExpress.XtraGrid.GridControl();
            this.grvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCodeOfSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPONCCID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestBuyRTCID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyRequest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFeeShip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVatMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoteDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceSale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cbTT = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.AutoSize = false;
            this.btnNewGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGroup.Image")));
            this.btnNewGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(80, 37);
            this.btnNewGroup.Tag = "frmPONCC_Update";
            this.btnNewGroup.Text = "&Tạo PO";
            this.btnNewGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.AutoSize = false;
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 40);
            // 
            // btnEditGroup
            // 
            this.btnEditGroup.AutoSize = false;
            this.btnEditGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnEditGroup.Image")));
            this.btnEditGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditGroup.Name = "btnEditGroup";
            this.btnEditGroup.Size = new System.Drawing.Size(80, 37);
            this.btnEditGroup.Tag = "frmPONCC_Update";
            this.btnEditGroup.Text = "&Sửa PO";
            this.btnEditGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditGroup.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.AutoSize = false;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 40);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.AutoSize = false;
            this.btnDeleteGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.Image")));
            this.btnDeleteGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(80, 37);
            this.btnDeleteGroup.Tag = "frmPONCC_Update";
            this.btnDeleteGroup.Text = "Xóa PO";
            this.btnDeleteGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewGroup,
            this.toolStripSeparator9,
            this.btnEditGroup,
            this.toolStripSeparator8,
            this.btnDeleteGroup,
            this.toolStripSeparator5,
            this.btnIsApproved,
            this.toolStripSeparator2,
            this.btnCancelApproved,
            this.toolStripSeparator3,
            this.btnExcel,
            this.toolStripSeparator1,
            this.bntEditExcel,
            this.cbChoseExcel});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1441, 80);
            this.mnuMenu.TabIndex = 49;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 40);
            // 
            // btnIsApproved
            // 
            this.btnIsApproved.AutoSize = false;
            this.btnIsApproved.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIsApproved.Image = ((System.Drawing.Image)(resources.GetObject("btnIsApproved.Image")));
            this.btnIsApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIsApproved.Name = "btnIsApproved";
            this.btnIsApproved.Size = new System.Drawing.Size(80, 37);
            this.btnIsApproved.Tag = "frmPONCC_IsApproved";
            this.btnIsApproved.Text = "Duyệt ";
            this.btnIsApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIsApproved.Click += new System.EventHandler(this.btnIsApproved_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // btnCancelApproved
            // 
            this.btnCancelApproved.AutoSize = false;
            this.btnCancelApproved.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelApproved.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreview;
            this.btnCancelApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelApproved.Name = "btnCancelApproved";
            this.btnCancelApproved.Size = new System.Drawing.Size(80, 37);
            this.btnCancelApproved.Tag = "frmPONCC_IsApproved";
            this.btnCancelApproved.Text = "Hủy duyệt";
            this.btnCancelApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelApproved.Click += new System.EventHandler(this.btnCancelApproved_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // bntEditExcel
            // 
            this.bntEditExcel.AutoSize = false;
            this.bntEditExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntEditExcel.Image = global::Forms.Properties.Resources.ExportToXLS_32x32;
            this.bntEditExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntEditExcel.Name = "bntEditExcel";
            this.bntEditExcel.Size = new System.Drawing.Size(100, 41);
            this.bntEditExcel.Tag = "frmPONCC_Update";
            this.bntEditExcel.Text = "Chỉnh sửa Excel";
            this.bntEditExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bntEditExcel.Click += new System.EventHandler(this.bntEditExcel_Click);
            // 
            // cbChoseExcel
            // 
            this.cbChoseExcel.Items.AddRange(new object[] {
            "Việt",
            "Anh"});
            this.cbChoseExcel.Margin = new System.Windows.Forms.Padding(15, 13, 0, 15);
            this.cbChoseExcel.Name = "cbChoseExcel";
            this.cbChoseExcel.Size = new System.Drawing.Size(121, 23);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(1033, 17);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 10);
            this.btnExportExcel.TabIndex = 47;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterText.Location = new System.Drawing.Point(954, 51);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(164, 22);
            this.txtFilterText.TabIndex = 53;
            this.txtFilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterText_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(1124, 50);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 54;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
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
            this.panel6.Location = new System.Drawing.Point(1205, 47);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(233, 27);
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
            this.txtPageSize.Size = new System.Drawing.Size(43, 20);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.Value = new decimal(new int[] {
            50,
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
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
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
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(57, 50);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(91, 22);
            this.dtpFromDate.TabIndex = 162;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(213, 50);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(91, 22);
            this.dtpEndDate.TabIndex = 159;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 14);
            this.label3.TabIndex = 161;
            this.label3.Text = "Từ ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(154, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 14);
            this.label4.TabIndex = 160;
            this.label4.Text = "Đến ngày";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(901, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 14);
            this.label7.TabIndex = 163;
            this.label7.Text = "Từ khóa";
            // 
            // cboSupplier
            // 
            this.cboSupplier.EditValue = "";
            this.cboSupplier.Location = new System.Drawing.Point(397, 50);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSupplier.Properties.Appearance.Options.UseFont = true;
            this.cboSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSupplier.Properties.NullText = "";
            this.cboSupplier.Properties.PopupView = this.gridView3;
            this.cboSupplier.Size = new System.Drawing.Size(221, 20);
            this.cboSupplier.TabIndex = 164;
            // 
            // gridView3
            // 
            this.gridView3.ColumnPanelRowHeight = 30;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn33,
            this.gridColumn35});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "ID";
            this.gridColumn33.FieldName = "ID";
            this.gridColumn33.Name = "gridColumn33";
            // 
            // gridColumn35
            // 
            this.gridColumn35.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn35.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn35.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn35.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn35.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn35.AppearanceHeader.Options.UseFont = true;
            this.gridColumn35.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn35.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn35.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn35.Caption = "Nhà cung cấp";
            this.gridColumn35.FieldName = "NameNCC";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 0;
            this.gridColumn35.Width = 175;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 165;
            this.label1.Text = "Nhà cung cấp";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 80);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grdMaster);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1441, 614);
            this.splitContainerControl1.SplitterPosition = 427;
            this.splitContainerControl1.TabIndex = 166;
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Location = new System.Drawing.Point(0, 0);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4,
            this.repositoryItemMemoEdit5});
            this.grdMaster.Size = new System.Drawing.Size(1441, 427);
            this.grdMaster.TabIndex = 1;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            this.grdMaster.DoubleClick += new System.EventHandler(this.grdMaster_DoubleClick);
            // 
            // grvMaster
            // 
            this.grvMaster.ColumnPanelRowHeight = 41;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDMaster,
            this.colIsApproved,
            this.colPOCode,
            this.colMaNCC,
            this.colNCC,
            this.colReceivedDatePO,
            this.colEmployeeID,
            this.colDeliveryTime,
            this.colNote,
            this.colStatusText,
            this.colDeliveryDate,
            this.colTotalMoneyPO,
            this.colEmail,
            this.colSupplierID,
            this.colEmployeeIDID,
            this.colCompany,
            this.colAddressDelivery,
            this.colRulePay,
            this.colBankingFee,
            this.colFedexAccount,
            this.colSupplierVoucher,
            this.colOriginItem,
            this.colRuleIncoterm,
            this.colStatus,
            this.colCurrency});
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsView.ColumnAutoWidth = false;
            this.grvMaster.OptionsView.ShowFooter = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvMaster_RowCellStyle);
            this.grvMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMaster_FocusedRowChanged);
            // 
            // colIDMaster
            // 
            this.colIDMaster.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIDMaster.AppearanceCell.Options.UseFont = true;
            this.colIDMaster.Caption = "ID";
            this.colIDMaster.FieldName = "ID";
            this.colIDMaster.Name = "colIDMaster";
            // 
            // colIsApproved
            // 
            this.colIsApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIsApproved.AppearanceCell.Options.UseFont = true;
            this.colIsApproved.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIsApproved.AppearanceHeader.Options.UseFont = true;
            this.colIsApproved.AppearanceHeader.Options.UseForeColor = true;
            this.colIsApproved.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsApproved.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.Caption = "Duyệt";
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.Visible = true;
            this.colIsApproved.VisibleIndex = 0;
            this.colIsApproved.Width = 60;
            // 
            // colPOCode
            // 
            this.colPOCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colPOCode.AppearanceCell.Options.UseFont = true;
            this.colPOCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPOCode.AppearanceHeader.Options.UseFont = true;
            this.colPOCode.AppearanceHeader.Options.UseForeColor = true;
            this.colPOCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colPOCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPOCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPOCode.Caption = "Số PONCC";
            this.colPOCode.FieldName = "POCode";
            this.colPOCode.Name = "colPOCode";
            this.colPOCode.Visible = true;
            this.colPOCode.VisibleIndex = 2;
            this.colPOCode.Width = 125;
            // 
            // colMaNCC
            // 
            this.colMaNCC.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colMaNCC.AppearanceCell.Options.UseFont = true;
            this.colMaNCC.AppearanceCell.Options.UseTextOptions = true;
            this.colMaNCC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaNCC.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaNCC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colMaNCC.AppearanceHeader.Options.UseFont = true;
            this.colMaNCC.AppearanceHeader.Options.UseForeColor = true;
            this.colMaNCC.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaNCC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaNCC.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaNCC.Caption = "Mã nhà cung cấp";
            this.colMaNCC.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colMaNCC.FieldName = "CodeNCC";
            this.colMaNCC.Name = "colMaNCC";
            this.colMaNCC.Visible = true;
            this.colMaNCC.VisibleIndex = 4;
            this.colMaNCC.Width = 126;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colNCC
            // 
            this.colNCC.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colNCC.AppearanceCell.Options.UseFont = true;
            this.colNCC.AppearanceCell.Options.UseTextOptions = true;
            this.colNCC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNCC.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNCC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNCC.AppearanceHeader.Options.UseFont = true;
            this.colNCC.AppearanceHeader.Options.UseForeColor = true;
            this.colNCC.AppearanceHeader.Options.UseTextOptions = true;
            this.colNCC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNCC.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNCC.Caption = "Nhà cung cấp";
            this.colNCC.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colNCC.FieldName = "NameNCC";
            this.colNCC.Name = "colNCC";
            this.colNCC.Visible = true;
            this.colNCC.VisibleIndex = 5;
            this.colNCC.Width = 248;
            // 
            // repositoryItemMemoEdit5
            // 
            this.repositoryItemMemoEdit5.Name = "repositoryItemMemoEdit5";
            // 
            // colReceivedDatePO
            // 
            this.colReceivedDatePO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colReceivedDatePO.AppearanceCell.Options.UseFont = true;
            this.colReceivedDatePO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colReceivedDatePO.AppearanceHeader.Options.UseFont = true;
            this.colReceivedDatePO.AppearanceHeader.Options.UseForeColor = true;
            this.colReceivedDatePO.AppearanceHeader.Options.UseTextOptions = true;
            this.colReceivedDatePO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReceivedDatePO.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colReceivedDatePO.Caption = "Ngày tạo PO";
            this.colReceivedDatePO.FieldName = "ReceivedDatePO";
            this.colReceivedDatePO.Name = "colReceivedDatePO";
            this.colReceivedDatePO.Visible = true;
            this.colReceivedDatePO.VisibleIndex = 7;
            this.colReceivedDatePO.Width = 145;
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
            this.colEmployeeID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeID.Caption = "Nhân viên mua hàng";
            this.colEmployeeID.FieldName = "FullName";
            this.colEmployeeID.Name = "colEmployeeID";
            this.colEmployeeID.Visible = true;
            this.colEmployeeID.VisibleIndex = 9;
            this.colEmployeeID.Width = 149;
            // 
            // colDeliveryTime
            // 
            this.colDeliveryTime.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colDeliveryTime.AppearanceCell.Options.UseFont = true;
            this.colDeliveryTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDeliveryTime.AppearanceHeader.Options.UseFont = true;
            this.colDeliveryTime.AppearanceHeader.Options.UseForeColor = true;
            this.colDeliveryTime.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeliveryTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeliveryTime.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDeliveryTime.Caption = "Ngày dự kiến hàng về";
            this.colDeliveryTime.FieldName = "ExpectedDate";
            this.colDeliveryTime.Name = "colDeliveryTime";
            this.colDeliveryTime.Width = 98;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 6;
            this.colNote.Width = 217;
            // 
            // colStatusText
            // 
            this.colStatusText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colStatusText.AppearanceCell.Options.UseFont = true;
            this.colStatusText.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colStatusText.AppearanceHeader.Options.UseFont = true;
            this.colStatusText.AppearanceHeader.Options.UseForeColor = true;
            this.colStatusText.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusText.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusText.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusText.Caption = "Trạng thái";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 1;
            this.colStatusText.Width = 121;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colDeliveryDate.AppearanceCell.Options.UseFont = true;
            this.colDeliveryDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDeliveryDate.AppearanceHeader.Options.UseFont = true;
            this.colDeliveryDate.AppearanceHeader.Options.UseForeColor = true;
            this.colDeliveryDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeliveryDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeliveryDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDeliveryDate.Caption = "Ngày dự kiến giao hàng";
            this.colDeliveryDate.FieldName = "DeliveryDate";
            this.colDeliveryDate.Name = "colDeliveryDate";
            this.colDeliveryDate.Visible = true;
            this.colDeliveryDate.VisibleIndex = 8;
            this.colDeliveryDate.Width = 120;
            // 
            // colTotalMoneyPO
            // 
            this.colTotalMoneyPO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colTotalMoneyPO.AppearanceCell.Options.UseFont = true;
            this.colTotalMoneyPO.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalMoneyPO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalMoneyPO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTotalMoneyPO.AppearanceHeader.Options.UseFont = true;
            this.colTotalMoneyPO.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalMoneyPO.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalMoneyPO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalMoneyPO.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalMoneyPO.Caption = "Tổng tiền PO";
            this.colTotalMoneyPO.DisplayFormat.FormatString = "n0";
            this.colTotalMoneyPO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalMoneyPO.FieldName = "TotalMoneyPO";
            this.colTotalMoneyPO.Name = "colTotalMoneyPO";
            this.colTotalMoneyPO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalMoneyPO", "SUM={0:n0}")});
            this.colTotalMoneyPO.Visible = true;
            this.colTotalMoneyPO.VisibleIndex = 3;
            this.colTotalMoneyPO.Width = 101;
            // 
            // colEmail
            // 
            this.colEmail.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colEmail.AppearanceCell.Options.UseFont = true;
            this.colEmail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEmail.AppearanceHeader.Options.UseFont = true;
            this.colEmail.AppearanceHeader.Options.UseForeColor = true;
            this.colEmail.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmail.Caption = "Email";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Width = 125;
            // 
            // colSupplierID
            // 
            this.colSupplierID.Caption = "SupplierID";
            this.colSupplierID.FieldName = "SupplierID";
            this.colSupplierID.Name = "colSupplierID";
            // 
            // colEmployeeIDID
            // 
            this.colEmployeeIDID.Caption = "EmployeeID";
            this.colEmployeeIDID.FieldName = "EmployeeID";
            this.colEmployeeIDID.Name = "colEmployeeIDID";
            // 
            // colCompany
            // 
            this.colCompany.Caption = "Công ty";
            this.colCompany.FieldName = "Company";
            this.colCompany.Name = "colCompany";
            // 
            // colAddressDelivery
            // 
            this.colAddressDelivery.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colAddressDelivery.AppearanceHeader.Options.UseFont = true;
            this.colAddressDelivery.AppearanceHeader.Options.UseForeColor = true;
            this.colAddressDelivery.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddressDelivery.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddressDelivery.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressDelivery.Caption = "Địa chỉ giao hàng";
            this.colAddressDelivery.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colAddressDelivery.FieldName = "AddressDelivery";
            this.colAddressDelivery.Name = "colAddressDelivery";
            this.colAddressDelivery.Visible = true;
            this.colAddressDelivery.VisibleIndex = 13;
            this.colAddressDelivery.Width = 177;
            // 
            // colRulePay
            // 
            this.colRulePay.AppearanceCell.Options.UseTextOptions = true;
            this.colRulePay.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRulePay.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRulePay.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colRulePay.AppearanceHeader.Options.UseFont = true;
            this.colRulePay.AppearanceHeader.Options.UseForeColor = true;
            this.colRulePay.AppearanceHeader.Options.UseTextOptions = true;
            this.colRulePay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRulePay.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRulePay.Caption = "Điều khoản thanh toán";
            this.colRulePay.FieldName = "RulePay";
            this.colRulePay.Name = "colRulePay";
            this.colRulePay.Visible = true;
            this.colRulePay.VisibleIndex = 14;
            this.colRulePay.Width = 278;
            // 
            // colBankingFee
            // 
            this.colBankingFee.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBankingFee.AppearanceHeader.Options.UseFont = true;
            this.colBankingFee.AppearanceHeader.Options.UseForeColor = true;
            this.colBankingFee.AppearanceHeader.Options.UseTextOptions = true;
            this.colBankingFee.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBankingFee.Caption = "Phí ngân hàng";
            this.colBankingFee.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colBankingFee.FieldName = "BankingFee";
            this.colBankingFee.Name = "colBankingFee";
            this.colBankingFee.Visible = true;
            this.colBankingFee.VisibleIndex = 11;
            this.colBankingFee.Width = 182;
            // 
            // colFedexAccount
            // 
            this.colFedexAccount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFedexAccount.AppearanceHeader.Options.UseFont = true;
            this.colFedexAccount.AppearanceHeader.Options.UseForeColor = true;
            this.colFedexAccount.AppearanceHeader.Options.UseTextOptions = true;
            this.colFedexAccount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFedexAccount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFedexAccount.Caption = "Fedex Account";
            this.colFedexAccount.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colFedexAccount.FieldName = "FedexAccount";
            this.colFedexAccount.Name = "colFedexAccount";
            this.colFedexAccount.Visible = true;
            this.colFedexAccount.VisibleIndex = 12;
            this.colFedexAccount.Width = 170;
            // 
            // colSupplierVoucher
            // 
            this.colSupplierVoucher.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colSupplierVoucher.AppearanceHeader.Options.UseFont = true;
            this.colSupplierVoucher.AppearanceHeader.Options.UseForeColor = true;
            this.colSupplierVoucher.AppearanceHeader.Options.UseTextOptions = true;
            this.colSupplierVoucher.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSupplierVoucher.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSupplierVoucher.Caption = "NCC xử lý chứng từ";
            this.colSupplierVoucher.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colSupplierVoucher.FieldName = "SupplierVoucher";
            this.colSupplierVoucher.Name = "colSupplierVoucher";
            this.colSupplierVoucher.Visible = true;
            this.colSupplierVoucher.VisibleIndex = 10;
            this.colSupplierVoucher.Width = 180;
            // 
            // colOriginItem
            // 
            this.colOriginItem.Caption = "OriginItem";
            this.colOriginItem.FieldName = "OriginItem";
            this.colOriginItem.Name = "colOriginItem";
            this.colOriginItem.Width = 118;
            // 
            // colRuleIncoterm
            // 
            this.colRuleIncoterm.AppearanceCell.Options.UseTextOptions = true;
            this.colRuleIncoterm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRuleIncoterm.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colRuleIncoterm.AppearanceHeader.Options.UseFont = true;
            this.colRuleIncoterm.AppearanceHeader.Options.UseForeColor = true;
            this.colRuleIncoterm.AppearanceHeader.Options.UseTextOptions = true;
            this.colRuleIncoterm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRuleIncoterm.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRuleIncoterm.Caption = "Điều khoán Incoterm";
            this.colRuleIncoterm.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colRuleIncoterm.FieldName = "RuleIncoterm";
            this.colRuleIncoterm.Name = "colRuleIncoterm";
            this.colRuleIncoterm.Visible = true;
            this.colRuleIncoterm.VisibleIndex = 15;
            this.colRuleIncoterm.Width = 250;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "Currency";
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            // 
            // grdDetail
            // 
            this.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetail.Location = new System.Drawing.Point(0, 0);
            this.grdDetail.MainView = this.grvDetail;
            this.grdDetail.Name = "grdDetail";
            this.grdDetail.Size = new System.Drawing.Size(1441, 177);
            this.grdDetail.TabIndex = 3;
            this.grdDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDetail,
            this.gridView1});
            // 
            // grvDetail
            // 
            this.grvDetail.ColumnPanelRowHeight = 42;
            this.grvDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDelete,
            this.colSTT,
            this.colIDDetail,
            this.colProductCode_,
            this.colProductName_,
            this.colProductCodeOfSupplier,
            this.colPONCCID,
            this.colRequestBuyRTCID,
            this.colQtyRequest,
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
            this.colPriceSale});
            this.grvDetail.GridControl = this.grdDetail;
            this.grvDetail.Name = "grvDetail";
            this.grvDetail.OptionsBehavior.ReadOnly = true;
            this.grvDetail.OptionsView.ColumnAutoWidth = false;
            this.grvDetail.OptionsView.ShowFooter = true;
            this.grvDetail.OptionsView.ShowGroupPanel = false;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colDelete.AppearanceCell.Options.UseFont = true;
            this.colDelete.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colDelete.ImageOptions.Image = global::Forms.Properties.Resources.cancel_16x161;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowEdit = false;
            this.colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.ReadOnly = true;
            this.colDelete.OptionsFilter.AllowAutoFilter = false;
            this.colDelete.OptionsFilter.AllowFilter = false;
            this.colDelete.Width = 28;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.FieldName = "STT";
            this.colSTT.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colSTT.ImageOptions.Image = global::Forms.Properties.Resources.add_16x161;
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSTT.OptionsFilter.AllowAutoFilter = false;
            this.colSTT.OptionsFilter.AllowFilter = false;
            this.colSTT.Width = 30;
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
            this.colProductCode_.VisibleIndex = 0;
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
            this.colProductName_.VisibleIndex = 1;
            this.colProductName_.Width = 327;
            // 
            // colProductCodeOfSupplier
            // 
            this.colProductCodeOfSupplier.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductCodeOfSupplier.AppearanceHeader.Options.UseFont = true;
            this.colProductCodeOfSupplier.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCodeOfSupplier.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCodeOfSupplier.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCodeOfSupplier.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCodeOfSupplier.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCodeOfSupplier.Caption = "Mã SP theo NCC";
            this.colProductCodeOfSupplier.FieldName = "ProductCodeOfSupplier";
            this.colProductCodeOfSupplier.Name = "colProductCodeOfSupplier";
            this.colProductCodeOfSupplier.Visible = true;
            this.colProductCodeOfSupplier.VisibleIndex = 2;
            this.colProductCodeOfSupplier.Width = 221;
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
            this.colQtyRequest.VisibleIndex = 4;
            this.colQtyRequest.Width = 71;
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
            this.colPrice.VisibleIndex = 5;
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
            this.colFeeShip.VisibleIndex = 10;
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
            this.colThanhTien.VisibleIndex = 6;
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
            this.colUnit.VisibleIndex = 3;
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
            this.coltotalPrice.DisplayFormat.FormatString = "n0";
            this.coltotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.coltotalPrice.FieldName = "totalPrice";
            this.coltotalPrice.Name = "coltotalPrice";
            this.coltotalPrice.OptionsColumn.AllowEdit = false;
            this.coltotalPrice.OptionsColumn.ReadOnly = true;
            this.coltotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "totalPrice", "SUM={0:n0}")});
            this.coltotalPrice.Visible = true;
            this.coltotalPrice.VisibleIndex = 11;
            this.coltotalPrice.Width = 158;
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
            this.colVat.VisibleIndex = 7;
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
            this.colVatMoney.VisibleIndex = 8;
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
            this.colNoteDetail.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNoteDetail.FieldName = "Note";
            this.colNoteDetail.Name = "colNoteDetail";
            this.colNoteDetail.Visible = true;
            this.colNoteDetail.VisibleIndex = 13;
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
            this.colDiscount.VisibleIndex = 9;
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
            this.colCurrencyExchange.FieldName = "CurrencyExchange";
            this.colCurrencyExchange.Name = "colCurrencyExchange";
            this.colCurrencyExchange.Visible = true;
            this.colCurrencyExchange.VisibleIndex = 12;
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
            this.colPriceSale.FieldName = "PriceSale";
            this.colPriceSale.Name = "colPriceSale";
            this.colPriceSale.OptionsColumn.ReadOnly = true;
            this.colPriceSale.Width = 132;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdDetail;
            this.gridView1.Name = "gridView1";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(624, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 165;
            this.label2.Text = "Trạng thái";
            // 
            // cbTT
            // 
            this.cbTT.FormattingEnabled = true;
            this.cbTT.Items.AddRange(new object[] {
            "Đang tiến hành",
            "Hoàn thành",
            "Huỷ",
            "Tất cả"});
            this.cbTT.Location = new System.Drawing.Point(693, 50);
            this.cbTT.Name = "cbTT";
            this.cbTT.Size = new System.Drawing.Size(202, 21);
            this.cbTT.TabIndex = 167;
            // 
            // frmPONCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 694);
            this.Controls.Add(this.cbTT);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.cboSupplier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.txtFilterText);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.btnExportExcel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPONCC";
            this.Text = "PO NHÀ CUNG CẤP";
            this.Load += new System.EventHandler(this.frmPONCC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private System.Windows.Forms.ToolStripButton btnNewGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnEditGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnDeleteGroup;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ToolStripButton btnIsApproved;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCancelApproved;
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
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.SearchLookUpEdit cboSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colPOCode;
        private DevExpress.XtraGrid.Columns.GridColumn colNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colReceivedDatePO;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryTime;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalMoneyPO;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colIDMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colMaNCC;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeIDID;
        private DevExpress.XtraGrid.GridControl grdDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
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
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStripComboBox cbChoseExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressDelivery;
        private DevExpress.XtraGrid.Columns.GridColumn colRulePay;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton bntEditExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colBankingFee;
        private DevExpress.XtraGrid.Columns.GridColumn colFedexAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierVoucher;
        private DevExpress.XtraGrid.Columns.GridColumn colOriginItem;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleIncoterm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTT;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCodeOfSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}