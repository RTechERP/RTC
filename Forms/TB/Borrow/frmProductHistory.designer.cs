namespace BMS
{
    partial class frmProductHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductHistory));
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.chkAdminConfirm = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnBorrow = new System.Windows.Forms.ToolStripButton();
            this.btnReturn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDuyenMuon = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBorrowNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReturnNew = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnProductHistoryLate = new System.Windows.Forms.ToolStripButton();
            this.btnListError = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoseNew = new System.Windows.Forms.ToolStripButton();
            this.btnLose = new System.Windows.Forms.ToolStripButton();
            this.btnBroken = new System.Windows.Forms.ToolStripButton();
            this.btnXuat = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHistoryLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWriteError = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.btnShowBillExport = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductRTCID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeopleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberBorrow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberLose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateLose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeople = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateReturmExpected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoteStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdminConfirm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerialNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillExportTechnicalID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQRCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductRTCQRCodeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillExportCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillTypeText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddressBoxActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colModulaLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModulaLocationDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusPersonText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.dtpNgayGiaHan = new System.Windows.Forms.DateTimePicker();
            this.labelGiaHanMuon = new System.Windows.Forms.Label();
            this.cboStatus = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeamName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeamID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmpID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdminConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // repositoryItemMemoEdit5
            // 
            this.repositoryItemMemoEdit5.Name = "repositoryItemMemoEdit5";
            // 
            // chkAdminConfirm
            // 
            this.chkAdminConfirm.AutoHeight = false;
            this.chkAdminConfirm.Caption = "Check";
            this.chkAdminConfirm.Name = "chkAdminConfirm";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(568, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Từ khóa";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(621, 5);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(179, 21);
            this.txtFilterText.TabIndex = 42;
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnFind.Location = new System.Drawing.Point(571, 34);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 46;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnBorrow
            // 
            this.btnBorrow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrow.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrow.Image")));
            this.btnBorrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(90, 41);
            this.btnBorrow.Tag = "";
            this.btnBorrow.Text = "Đăng kí mượn";
            this.btnBorrow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 41);
            this.btnReturn.Tag = "frmProductHistory_ReturnProduct_New";
            this.btnReturn.Text = "Trả thiết bị";
            this.btnReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBorrow,
            this.toolStripSeparator4,
            this.btnReturn,
            this.toolStripSeparator2,
            this.btnDuyenMuon,
            this.toolStripSeparator5,
            this.btnLoad,
            this.toolStripSeparator6,
            this.btnEdit,
            this.toolStripSeparator7,
            this.btnBorrowNew,
            this.toolStripSeparator8,
            this.btnReturnNew,
            this.btnExport,
            this.btnProductHistoryLate,
            this.btnListError,
            this.toolStripSeparator3,
            this.btnLoseNew,
            this.btnLose,
            this.toolStripSeparator1,
            this.btnBroken,
            this.btnXuat});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1298, 44);
            this.mnuMenu.TabIndex = 49;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // btnDuyenMuon
            // 
            this.btnDuyenMuon.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuyenMuon.Image = ((System.Drawing.Image)(resources.GetObject("btnDuyenMuon.Image")));
            this.btnDuyenMuon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDuyenMuon.Name = "btnDuyenMuon";
            this.btnDuyenMuon.Size = new System.Drawing.Size(80, 41);
            this.btnDuyenMuon.Tag = "frmProductHistory_ReturnProduct_New";
            this.btnDuyenMuon.Text = "Duyệt Mượn";
            this.btnDuyenMuon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDuyenMuon.Click += new System.EventHandler(this.btnDuyenMuon_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 40);
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(59, 41);
            this.btnLoad.Text = "Làm mới";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 40);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(104, 41);
            this.btnEdit.Tag = "frmProductRTC_EditPeolpeBorrow_New";
            this.btnEdit.Text = "Sửa người mượn";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 40);
            // 
            // btnBorrowNew
            // 
            this.btnBorrowNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrowNew.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrowNew.Image")));
            this.btnBorrowNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBorrowNew.Name = "btnBorrowNew";
            this.btnBorrowNew.Size = new System.Drawing.Size(116, 41);
            this.btnBorrowNew.Tag = "";
            this.btnBorrowNew.Text = "Đăng kí mượn New";
            this.btnBorrowNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBorrowNew.Click += new System.EventHandler(this.btnBorrowNew_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.AutoSize = false;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 40);
            // 
            // btnReturnNew
            // 
            this.btnReturnNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnReturnNew.Image")));
            this.btnReturnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReturnNew.Name = "btnReturnNew";
            this.btnReturnNew.Size = new System.Drawing.Size(99, 41);
            this.btnReturnNew.Tag = "frmProductHistory_ReturnProduct_New";
            this.btnReturnNew.Text = "Trả thiết bị New";
            this.btnReturnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReturnNew.Click += new System.EventHandler(this.btnReturnNew_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(37, 41);
            this.btnExport.Tag = "frmBillExport_Sale_New";
            this.btnExport.Text = "Xuất";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnProductHistoryLate
            // 
            this.btnProductHistoryLate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductHistoryLate.Image = global::Forms.Properties.Resources.InsertTableOfCaptions_32x32;
            this.btnProductHistoryLate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProductHistoryLate.Name = "btnProductHistoryLate";
            this.btnProductHistoryLate.Size = new System.Drawing.Size(57, 41);
            this.btnProductHistoryLate.Tag = "frmProductHistory_ReturnProduct_New";
            this.btnProductHistoryLate.Text = "Quá hạn";
            this.btnProductHistoryLate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnProductHistoryLate.Click += new System.EventHandler(this.btnProductHistoryLate_Click);
            // 
            // btnListError
            // 
            this.btnListError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListError.Image = ((System.Drawing.Image)(resources.GetObject("btnListError.Image")));
            this.btnListError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnListError.Name = "btnListError";
            this.btnListError.Size = new System.Drawing.Size(85, 41);
            this.btnListError.Tag = "frmProductHistory_Broken_New";
            this.btnListError.Text = "Danh sách lỗi";
            this.btnListError.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnListError.Visible = false;
            this.btnListError.Click += new System.EventHandler(this.btnListError_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // btnLoseNew
            // 
            this.btnLoseNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoseNew.Image = ((System.Drawing.Image)(resources.GetObject("btnLoseNew.Image")));
            this.btnLoseNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoseNew.Name = "btnLoseNew";
            this.btnLoseNew.Size = new System.Drawing.Size(126, 41);
            this.btnLoseNew.Tag = "frmProductHistory_Lose_New";
            this.btnLoseNew.Text = "Thiết bị mất QRCode";
            this.btnLoseNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoseNew.Visible = false;
            this.btnLoseNew.Click += new System.EventHandler(this.btnLoseNew_Click);
            // 
            // btnLose
            // 
            this.btnLose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLose.Image = ((System.Drawing.Image)(resources.GetObject("btnLose.Image")));
            this.btnLose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLose.Name = "btnLose";
            this.btnLose.Size = new System.Drawing.Size(79, 41);
            this.btnLose.Tag = "frmProductHistory_Lose_New";
            this.btnLose.Text = "Thiết bị mất";
            this.btnLose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLose.Visible = false;
            this.btnLose.Click += new System.EventHandler(this.btnLose_Click);
            // 
            // btnBroken
            // 
            this.btnBroken.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBroken.Image = ((System.Drawing.Image)(resources.GetObject("btnBroken.Image")));
            this.btnBroken.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBroken.Name = "btnBroken";
            this.btnBroken.Size = new System.Drawing.Size(84, 41);
            this.btnBroken.Tag = "frmProductHistory_Broken_New";
            this.btnBroken.Text = "Thiết bị hỏng";
            this.btnBroken.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBroken.Visible = false;
            this.btnBroken.Click += new System.EventHandler(this.btnBroken_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.Image = ((System.Drawing.Image)(resources.GetObject("btnXuat.Image")));
            this.btnXuat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(37, 41);
            this.btnXuat.Tag = "frmBillExport_Sale_New";
            this.btnXuat.Text = "Xuất";
            this.btnXuat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXuat.Visible = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.btnHistoryLog,
            this.btnWriteError,
            this.btnDetail,
            this.btnShowBillExport,
            this.btnDeleteHistory});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(299, 136);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.deleteToolStripMenuItem.Text = "Find";
            this.deleteToolStripMenuItem.Visible = false;
            // 
            // btnHistoryLog
            // 
            this.btnHistoryLog.Name = "btnHistoryLog";
            this.btnHistoryLog.Size = new System.Drawing.Size(298, 22);
            this.btnHistoryLog.Text = "Lịch sử gia hạn";
            this.btnHistoryLog.Click += new System.EventHandler(this.btnHistoryLog_Click);
            // 
            // btnWriteError
            // 
            this.btnWriteError.Name = "btnWriteError";
            this.btnWriteError.Size = new System.Drawing.Size(298, 22);
            this.btnWriteError.Text = "Ghi lại lỗi quy trình nhập xuất kho cá nhân";
            this.btnWriteError.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(298, 22);
            this.btnDetail.Text = "Chi tiết";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnShowBillExport
            // 
            this.btnShowBillExport.Name = "btnShowBillExport";
            this.btnShowBillExport.Size = new System.Drawing.Size(298, 22);
            this.btnShowBillExport.Text = "Xem phiếu xuất";
            this.btnShowBillExport.Click += new System.EventHandler(this.btnShowBillExport_Click);
            // 
            // btnDeleteHistory
            // 
            this.btnDeleteHistory.Name = "btnDeleteHistory";
            this.btnDeleteHistory.Size = new System.Drawing.Size(298, 22);
            this.btnDeleteHistory.Text = "Xoá";
            this.btnDeleteHistory.Click += new System.EventHandler(this.btnDeleteHistory_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Đến ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(74, 5);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(95, 21);
            this.dtpFromDate.TabIndex = 50;
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
            // grdData
            // 
            this.grdData.ContextMenuStrip = this.contextMenuStrip;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.Location = new System.Drawing.Point(0, 107);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit6});
            this.grdData.Size = new System.Drawing.Size(1298, 490);
            this.grdData.TabIndex = 54;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            this.grdData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grdData_MouseClick);
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductRTCID,
            this.colPeopleID,
            this.colProductName,
            this.colProductCode,
            this.colSerial,
            this.colMaker,
            this.colNumberBorrow,
            this.colNumberLose,
            this.colAddress,
            this.colDate1,
            this.colDateLose,
            this.colPeople,
            this.colDateReturmExpected,
            this.colDate2,
            this.colProject,
            this.colNoteStatus,
            this.colCountDate,
            this.colAdminConfirm,
            this.colSerialNumber,
            this.colPartNumber,
            this.colStatus,
            this.gridColumn1,
            this.colBillExportTechnicalID,
            this.colQRCode,
            this.colProductRTCQRCodeID,
            this.colStatusNew,
            this.colID,
            this.colBillExportCode,
            this.colBillTypeText,
            this.colCode,
            this.colDepartmentName,
            this.colAddressBoxActual,
            this.colModulaLocationName,
            this.colModulaLocationDetailID,
            this.colStatusPerson,
            this.colStatusPersonText});
            this.grvData.GridControl = this.grdData;
            this.grvData.HorzScrollStep = 5;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsCustomization.AllowRowSizing = true;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsFind.ShowCloseButton = false;
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvData_CustomDrawCell);
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            // 
            // colProductRTCID
            // 
            this.colProductRTCID.Caption = "ProductRTCID";
            this.colProductRTCID.FieldName = "ProductRTCID";
            this.colProductRTCID.Name = "colProductRTCID";
            // 
            // colPeopleID
            // 
            this.colPeopleID.Caption = "PeopleID";
            this.colPeopleID.FieldName = "PeopleID";
            this.colPeopleID.Name = "colPeopleID";
            // 
            // colProductName
            // 
            this.colProductName.Caption = "Tên";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 2;
            this.colProductName.Width = 145;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Mã Sản Phẩm";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 4;
            this.colProductCode.Width = 100;
            // 
            // colSerial
            // 
            this.colSerial.Caption = "Code";
            this.colSerial.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSerial.FieldName = "Serial";
            this.colSerial.Name = "colSerial";
            this.colSerial.Visible = true;
            this.colSerial.VisibleIndex = 8;
            this.colSerial.Width = 70;
            // 
            // colMaker
            // 
            this.colMaker.Caption = "Hãng";
            this.colMaker.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colMaker.FieldName = "Maker";
            this.colMaker.Name = "colMaker";
            this.colMaker.Visible = true;
            this.colMaker.VisibleIndex = 9;
            this.colMaker.Width = 72;
            // 
            // colNumberBorrow
            // 
            this.colNumberBorrow.Caption = "Số lượng mượn";
            this.colNumberBorrow.FieldName = "NumberBorrow";
            this.colNumberBorrow.Name = "colNumberBorrow";
            this.colNumberBorrow.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NumberBorrow", "{0:0.##}")});
            this.colNumberBorrow.Visible = true;
            this.colNumberBorrow.VisibleIndex = 10;
            this.colNumberBorrow.Width = 66;
            // 
            // colNumberLose
            // 
            this.colNumberLose.Caption = "Số lượng mất";
            this.colNumberLose.FieldName = "NumberLose";
            this.colNumberLose.Name = "colNumberLose";
            // 
            // colAddress
            // 
            this.colAddress.Caption = "Vị trí (Hộp)";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colAddress.FieldName = "AddressBox";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 11;
            this.colAddress.Width = 105;
            // 
            // colDate1
            // 
            this.colDate1.Caption = "Ngày mượn";
            this.colDate1.FieldName = "DateBorrow";
            this.colDate1.Name = "colDate1";
            this.colDate1.Visible = true;
            this.colDate1.VisibleIndex = 15;
            this.colDate1.Width = 97;
            // 
            // colDateLose
            // 
            this.colDateLose.Caption = "gridColumn1";
            this.colDateLose.FieldName = "DateLose";
            this.colDateLose.Name = "colDateLose";
            // 
            // colPeople
            // 
            this.colPeople.Caption = "Người mượn";
            this.colPeople.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colPeople.FieldName = "FullName";
            this.colPeople.Name = "colPeople";
            this.colPeople.Visible = true;
            this.colPeople.VisibleIndex = 14;
            this.colPeople.Width = 128;
            // 
            // colDateReturmExpected
            // 
            this.colDateReturmExpected.Caption = "Ngày dự kiến trả";
            this.colDateReturmExpected.FieldName = "DateReturnExpected";
            this.colDateReturmExpected.Name = "colDateReturmExpected";
            this.colDateReturmExpected.Visible = true;
            this.colDateReturmExpected.VisibleIndex = 16;
            this.colDateReturmExpected.Width = 90;
            // 
            // colDate2
            // 
            this.colDate2.Caption = "Ngày trả";
            this.colDate2.FieldName = "DateReturn";
            this.colDate2.Name = "colDate2";
            this.colDate2.Visible = true;
            this.colDate2.VisibleIndex = 17;
            this.colDate2.Width = 80;
            // 
            // colProject
            // 
            this.colProject.Caption = "Dự án";
            this.colProject.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colProject.FieldName = "Project";
            this.colProject.Name = "colProject";
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 18;
            this.colProject.Width = 119;
            // 
            // colNoteStatus
            // 
            this.colNoteStatus.Caption = "Note";
            this.colNoteStatus.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colNoteStatus.FieldName = "Note";
            this.colNoteStatus.Name = "colNoteStatus";
            this.colNoteStatus.Visible = true;
            this.colNoteStatus.VisibleIndex = 19;
            this.colNoteStatus.Width = 151;
            // 
            // colCountDate
            // 
            this.colCountDate.Caption = "gridColumn1";
            this.colCountDate.FieldName = "CountDate";
            this.colCountDate.Name = "colCountDate";
            // 
            // colAdminConfirm
            // 
            this.colAdminConfirm.Caption = "Duyệt";
            this.colAdminConfirm.ColumnEdit = this.chkAdminConfirm;
            this.colAdminConfirm.FieldName = "AdminConfirm";
            this.colAdminConfirm.Name = "colAdminConfirm";
            this.colAdminConfirm.Visible = true;
            this.colAdminConfirm.VisibleIndex = 22;
            this.colAdminConfirm.Width = 54;
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.Caption = "Serial";
            this.colSerialNumber.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSerialNumber.FieldName = "SerialNumber";
            this.colSerialNumber.Name = "colSerialNumber";
            this.colSerialNumber.Visible = true;
            this.colSerialNumber.VisibleIndex = 6;
            this.colSerialNumber.Width = 85;
            // 
            // colPartNumber
            // 
            this.colPartNumber.Caption = "Part Number";
            this.colPartNumber.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colPartNumber.FieldName = "PartNumber";
            this.colPartNumber.Name = "colPartNumber";
            this.colPartNumber.Visible = true;
            this.colPartNumber.VisibleIndex = 7;
            this.colPartNumber.Width = 93;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "StatusText";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 1;
            this.colStatus.Width = 92;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã nội bộ";
            this.gridColumn1.FieldName = "ProductCodeRTC";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 93;
            // 
            // colBillExportTechnicalID
            // 
            this.colBillExportTechnicalID.Caption = "BillExportTechnicalID";
            this.colBillExportTechnicalID.FieldName = "BillExportTechnicalID";
            this.colBillExportTechnicalID.Name = "colBillExportTechnicalID";
            this.colBillExportTechnicalID.Width = 69;
            // 
            // colQRCode
            // 
            this.colQRCode.Caption = "Mã QRCode";
            this.colQRCode.FieldName = "ProductQRCode";
            this.colQRCode.Name = "colQRCode";
            this.colQRCode.Visible = true;
            this.colQRCode.VisibleIndex = 5;
            this.colQRCode.Width = 83;
            // 
            // colProductRTCQRCodeID
            // 
            this.colProductRTCQRCodeID.Caption = "ProductRTCQRCodeID";
            this.colProductRTCQRCodeID.FieldName = "ProductRTCQRCodeID";
            this.colProductRTCQRCodeID.Name = "colProductRTCQRCodeID";
            // 
            // colStatusNew
            // 
            this.colStatusNew.Caption = "StatusNew";
            this.colStatusNew.FieldName = "StatusNew";
            this.colStatusNew.Name = "colStatusNew";
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colBillExportCode
            // 
            this.colBillExportCode.Caption = "Mã phiếu xuất";
            this.colBillExportCode.FieldName = "BillExportCode";
            this.colBillExportCode.Name = "colBillExportCode";
            this.colBillExportCode.Visible = true;
            this.colBillExportCode.VisibleIndex = 20;
            this.colBillExportCode.Width = 109;
            // 
            // colBillTypeText
            // 
            this.colBillTypeText.Caption = "Loại phiếu";
            this.colBillTypeText.FieldName = "BillTypeText";
            this.colBillTypeText.Name = "colBillTypeText";
            this.colBillTypeText.Visible = true;
            this.colBillTypeText.VisibleIndex = 21;
            this.colBillTypeText.Width = 81;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Width = 109;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 13;
            this.colDepartmentName.Width = 121;
            // 
            // colAddressBoxActual
            // 
            this.colAddressBoxActual.Caption = "Vị trí trả";
            this.colAddressBoxActual.ColumnEdit = this.repositoryItemMemoEdit6;
            this.colAddressBoxActual.FieldName = "AddressBoxActual";
            this.colAddressBoxActual.Name = "colAddressBoxActual";
            this.colAddressBoxActual.Width = 111;
            // 
            // repositoryItemMemoEdit6
            // 
            this.repositoryItemMemoEdit6.Name = "repositoryItemMemoEdit6";
            // 
            // colModulaLocationName
            // 
            this.colModulaLocationName.Caption = "Vị trí Modula";
            this.colModulaLocationName.FieldName = "ModulaLocationName";
            this.colModulaLocationName.Name = "colModulaLocationName";
            this.colModulaLocationName.Visible = true;
            this.colModulaLocationName.VisibleIndex = 12;
            this.colModulaLocationName.Width = 102;
            // 
            // colModulaLocationDetailID
            // 
            this.colModulaLocationDetailID.FieldName = "ModulaLocationDetailID";
            this.colModulaLocationDetailID.Name = "colModulaLocationDetailID";
            // 
            // colStatusPerson
            // 
            this.colStatusPerson.FieldName = "StatusPerson";
            this.colStatusPerson.Name = "colStatusPerson";
            // 
            // colStatusPersonText
            // 
            this.colStatusPersonText.Caption = "Trạng thái modula";
            this.colStatusPersonText.FieldName = "StatusPersonText";
            this.colStatusPersonText.Name = "colStatusPersonText";
            this.colStatusPersonText.Visible = true;
            this.colStatusPersonText.VisibleIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Trạng thái";
            // 
            // stackPanel1
            // 
            this.stackPanel1.AutoSize = true;
            this.stackPanel1.Controls.Add(this.btnFirst);
            this.stackPanel1.Controls.Add(this.btnPrev);
            this.stackPanel1.Controls.Add(this.txtPageNumber);
            this.stackPanel1.Controls.Add(this.label10);
            this.stackPanel1.Controls.Add(this.txtTotalPage);
            this.stackPanel1.Controls.Add(this.btnNext);
            this.stackPanel1.Controls.Add(this.btnLast);
            this.stackPanel1.Controls.Add(this.txtPageSize);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.stackPanel1.Location = new System.Drawing.Point(1029, 2);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(267, 59);
            this.stackPanel1.TabIndex = 63;
            // 
            // btnFirst
            // 
            this.btnFirst.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnFirst.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnFirst.Appearance.Options.UseBackColor = true;
            this.btnFirst.Appearance.Options.UseForeColor = true;
            this.btnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.ImageOptions.Image")));
            this.btnFirst.Location = new System.Drawing.Point(3, 18);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnFirst.Size = new System.Drawing.Size(19, 23);
            this.btnFirst.TabIndex = 143;
            this.btnFirst.Text = "Trang trước";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.Location = new System.Drawing.Point(28, 18);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPrev.Size = new System.Drawing.Size(19, 23);
            this.btnPrev.TabIndex = 141;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(53, 19);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 21);
            this.txtPageNumber.TabIndex = 152;
            this.txtPageNumber.Text = "1";
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(84, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 20);
            this.label10.TabIndex = 151;
            this.label10.Text = "/";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Location = new System.Drawing.Point(106, 19);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(25, 21);
            this.txtTotalPage.TabIndex = 12;
            this.txtTotalPage.Text = "1";
            this.txtTotalPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Appearance.Options.UseBackColor = true;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.Location = new System.Drawing.Point(137, 18);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(19, 23);
            this.btnNext.TabIndex = 142;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnLast.Appearance.Options.UseBackColor = true;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.Location = new System.Drawing.Point(162, 18);
            this.btnLast.Name = "btnLast";
            this.btnLast.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnLast.Size = new System.Drawing.Size(19, 23);
            this.btnLast.TabIndex = 144;
            this.btnLast.Text = "`";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(187, 18);
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
            this.txtPageSize.Size = new System.Drawing.Size(77, 23);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPageSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.txtPageSize.ValueChanged += new System.EventHandler(this.txtPageSize_ValueChanged);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.Location = new System.Drawing.Point(825, 32);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(56, 23);
            this.btnGiaHan.TabIndex = 62;
            this.btnGiaHan.Tag = "frmProductHistory_Borrow_New";
            this.btnGiaHan.Text = "Gia hạn";
            this.btnGiaHan.UseVisualStyleBackColor = false;
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // dtpNgayGiaHan
            // 
            this.dtpNgayGiaHan.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayGiaHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayGiaHan.Location = new System.Drawing.Point(722, 32);
            this.dtpNgayGiaHan.Name = "dtpNgayGiaHan";
            this.dtpNgayGiaHan.Size = new System.Drawing.Size(97, 21);
            this.dtpNgayGiaHan.TabIndex = 60;
            // 
            // labelGiaHanMuon
            // 
            this.labelGiaHanMuon.AutoSize = true;
            this.labelGiaHanMuon.Location = new System.Drawing.Point(652, 39);
            this.labelGiaHanMuon.Name = "labelGiaHanMuon";
            this.labelGiaHanMuon.Size = new System.Drawing.Size(64, 13);
            this.labelGiaHanMuon.TabIndex = 61;
            this.labelGiaHanMuon.Text = "Gia hạn đến";
            // 
            // cboStatus
            // 
            this.cboStatus.EditValue = "";
            this.cboStatus.Location = new System.Drawing.Point(74, 35);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("0", "Đã trả"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("1", "Đang mượn"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("2", "Thiết bị đã mất"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("3", "Thiết bị hỏng"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("4", "Đăng ký trả"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("7", "Đăng ký mượn")});
            this.cboStatus.Properties.SelectAllItemCaption = "Tất cả";
            this.cboStatus.Size = new System.Drawing.Size(488, 20);
            this.cboStatus.TabIndex = 63;
            this.cboStatus.EditValueChanged += new System.EventHandler(this.cboStatus_EditValueChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.cboEmployee);
            this.panelControl1.Controls.Add(this.btnImportExcel);
            this.panelControl1.Controls.Add(this.btnExportExcel);
            this.panelControl1.Controls.Add(this.btnGiaHan);
            this.panelControl1.Controls.Add(this.dtpNgayGiaHan);
            this.panelControl1.Controls.Add(this.txtFilterText);
            this.panelControl1.Controls.Add(this.labelGiaHanMuon);
            this.panelControl1.Controls.Add(this.stackPanel1);
            this.panelControl1.Controls.Add(this.btnFind);
            this.panelControl1.Controls.Add(this.label7);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.dtpFromDate);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.dtpEndDate);
            this.panelControl1.Controls.Add(this.cboStatus);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 44);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1298, 63);
            this.panelControl1.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(338, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 71;
            this.label6.Text = "Nhân viên";
            // 
            // cboEmployee
            // 
            this.cboEmployee.Location = new System.Drawing.Point(412, 4);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployee.Properties.Appearance.Options.UseFont = true;
            this.cboEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployee.Properties.NullText = "";
            this.cboEmployee.Properties.PopupView = this.gridView2;
            this.cboEmployee.Size = new System.Drawing.Size(150, 22);
            this.cboEmployee.TabIndex = 72;
            this.cboEmployee.EditValueChanged += new System.EventHandler(this.cboEmployee_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.ColumnPanelRowHeight = 40;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmpCode,
            this.colTeamName,
            this.colFullName,
            this.colDepartName,
            this.colUserID,
            this.colDepartID,
            this.colTeamID,
            this.colEmpID});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GroupCount = 2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartName, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTeamName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colEmpCode
            // 
            this.colEmpCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEmpCode.AppearanceHeader.Options.UseFont = true;
            this.colEmpCode.AppearanceHeader.Options.UseForeColor = true;
            this.colEmpCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmpCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmpCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmpCode.Caption = "Mã nhân viên";
            this.colEmpCode.FieldName = "Code";
            this.colEmpCode.Name = "colEmpCode";
            this.colEmpCode.Visible = true;
            this.colEmpCode.VisibleIndex = 0;
            this.colEmpCode.Width = 227;
            // 
            // colTeamName
            // 
            this.colTeamName.Caption = "Team";
            this.colTeamName.FieldName = "TeamName";
            this.colTeamName.MinWidth = 15;
            this.colTeamName.Name = "colTeamName";
            this.colTeamName.Visible = true;
            this.colTeamName.VisibleIndex = 2;
            this.colTeamName.Width = 56;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFullName.AppearanceHeader.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Options.UseForeColor = true;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 1;
            this.colFullName.Width = 489;
            // 
            // colDepartName
            // 
            this.colDepartName.Caption = "Phòng ban";
            this.colDepartName.FieldName = "DepartmentName";
            this.colDepartName.MinWidth = 15;
            this.colDepartName.Name = "colDepartName";
            this.colDepartName.Visible = true;
            this.colDepartName.VisibleIndex = 1;
            this.colDepartName.Width = 56;
            // 
            // colUserID
            // 
            this.colUserID.Caption = "UserID";
            this.colUserID.FieldName = "UserID";
            this.colUserID.Name = "colUserID";
            // 
            // colDepartID
            // 
            this.colDepartID.Caption = "DepartmentID";
            this.colDepartID.FieldName = "DepartmentID";
            this.colDepartID.MinWidth = 15;
            this.colDepartID.Name = "colDepartID";
            this.colDepartID.Width = 56;
            // 
            // colTeamID
            // 
            this.colTeamID.Caption = "TeamID";
            this.colTeamID.FieldName = "TeamID";
            this.colTeamID.MinWidth = 15;
            this.colTeamID.Name = "colTeamID";
            this.colTeamID.Width = 56;
            // 
            // colEmpID
            // 
            this.colEmpID.Caption = "EmployeeID";
            this.colEmpID.FieldName = "EmployeeID";
            this.colEmpID.MinWidth = 15;
            this.colEmpID.Name = "colEmpID";
            this.colEmpID.Width = 56;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Location = new System.Drawing.Point(939, 2);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnImportExcel.TabIndex = 70;
            this.btnImportExcel.Tag = "frm_ProductHistory_ImportExcel_New";
            this.btnImportExcel.Text = "Nhập Excel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Visible = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(806, 4);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 69;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // toolTipController1
            // 
            this.toolTipController1.AppearanceTitle.Image = global::Forms.Properties.Resources.button_ok_icon;
            this.toolTipController1.AppearanceTitle.Options.UseImage = true;
            // 
            // frmProductHistory
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 597);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmProductHistory";
            this.Tag = "";
            this.Text = "LỊCH SỬ MƯỢN";
            this.Activated += new System.EventHandler(this.frmProductHistory_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductHistory_FormClosed);
            this.Load += new System.EventHandler(this.frmListTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdminConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private System.Windows.Forms.ToolStripButton btnBorrow;
        private System.Windows.Forms.ToolStripButton btnReturn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colMaker;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberBorrow;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colSerial;
        private DevExpress.XtraGrid.Columns.GridColumn colDate1;
        private DevExpress.XtraGrid.Columns.GridColumn colPeople;
        private DevExpress.XtraGrid.Columns.GridColumn colDate2;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteStatus;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.Columns.GridColumn colDateReturmExpected;
        private DevExpress.XtraGrid.Columns.GridColumn colCountDate;
        private System.Windows.Forms.ToolStripButton btnLose;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberLose;
        private DevExpress.XtraGrid.Columns.GridColumn colDateLose;
        private DevExpress.XtraGrid.Columns.GridColumn colAdminConfirm;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPartNumber;
        private System.Windows.Forms.ToolStripButton btnBroken;
        private System.Windows.Forms.ToolStripButton btnListError;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.Label labelGiaHanMuon;
        private System.Windows.Forms.DateTimePicker dtpNgayGiaHan;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRTCID;
        private DevExpress.XtraGrid.Columns.GridColumn colPeopleID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private System.Windows.Forms.ToolStripButton btnLoad;
		private System.Windows.Forms.ToolStripButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.ToolStripButton btnXuat;
        private DevExpress.XtraGrid.Columns.GridColumn colBillExportTechnicalID;
        private System.Windows.Forms.ToolStripButton btnBorrowNew;
        private DevExpress.XtraGrid.Columns.GridColumn colQRCode;
        private System.Windows.Forms.ToolStripButton btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRTCQRCodeID;
        private System.Windows.Forms.ToolStripButton btnLoseNew;
        private System.Windows.Forms.ToolStripButton btnReturnNew;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusNew;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDuyenMuon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem btnHistoryLog;
        private System.Windows.Forms.ToolStripMenuItem btnWriteError;
        private System.Windows.Forms.ToolStripMenuItem btnDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colBillExportCode;
        private System.Windows.Forms.ToolStripMenuItem btnShowBillExport;
        private DevExpress.XtraGrid.Columns.GridColumn colBillTypeText;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteHistory;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cboStatus;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkAdminConfirm;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Button btnExportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartID;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartName;
        private DevExpress.XtraGrid.Columns.GridColumn colTeamName;
        private DevExpress.XtraGrid.Columns.GridColumn colTeamID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpID;
        private System.Windows.Forms.ToolStripButton btnProductHistoryLate;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressBoxActual;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit6;
        private DevExpress.XtraGrid.Columns.GridColumn colModulaLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colModulaLocationDetailID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusPerson;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusPersonText;
    }
}