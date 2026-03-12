
namespace BMS
{
    partial class frmBillDocumentExportDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillDocumentExportDetail));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.chkAllBillExport = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboStatusNew = new DevExpress.XtraEditors.LookUpEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.cbProductGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnFind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBillDocumentExportType = new System.Windows.Forms.ComboBox();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colIDMaster = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBillDocumentImportType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.chkEditStatus = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDateStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colnameStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.cboViewNew = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNotePO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBBBG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNoteBBBG = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPXK = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNotePXK = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBBBGHH = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBBBGHH_Note = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDepartmentName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colAddress = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWarehouseType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colWarehouseName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductTypeText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullNameSender = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnApprove = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProductGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEditStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExcel});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1321, 36);
            this.mnuMenu.TabIndex = 119;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(69, 33);
            this.btnExcel.Tag = "frmExcel_Import";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // chkAllBillExport
            // 
            this.chkAllBillExport.AutoSize = true;
            this.chkAllBillExport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllBillExport.Location = new System.Drawing.Point(333, 6);
            this.chkAllBillExport.Name = "chkAllBillExport";
            this.chkAllBillExport.Size = new System.Drawing.Size(63, 20);
            this.chkAllBillExport.TabIndex = 188;
            this.chkAllBillExport.Text = "Tất cả";
            this.chkAllBillExport.UseVisualStyleBackColor = true;
            this.chkAllBillExport.CheckedChanged += new System.EventHandler(this.chkAllBillExport_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 184;
            this.label2.Text = "Lọc phiếu";
            // 
            // cboStatusNew
            // 
            this.cboStatusNew.Location = new System.Drawing.Point(77, 33);
            this.cboStatusNew.Name = "cboStatusNew";
            this.cboStatusNew.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusNew.Properties.Appearance.Options.UseFont = true;
            this.cboStatusNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusNew.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Trạng thái")});
            this.cboStatusNew.Properties.NullText = "";
            this.cboStatusNew.Size = new System.Drawing.Size(247, 22);
            this.cboStatusNew.TabIndex = 187;
            this.cboStatusNew.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cboStatusNew_Closed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(599, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 185;
            this.label6.Text = "Lọc kho";
            // 
            // cbProductGroup
            // 
            this.cbProductGroup.EditValue = "";
            this.cbProductGroup.Location = new System.Drawing.Point(659, 5);
            this.cbProductGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductGroup.Name = "cbProductGroup";
            this.cbProductGroup.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductGroup.Properties.Appearance.Options.UseFont = true;
            this.cbProductGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProductGroup.Size = new System.Drawing.Size(276, 22);
            this.cbProductGroup.TabIndex = 186;
            this.cbProductGroup.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cbProductGroup_Closed);
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(203, 19);
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
            this.txtPageSize.Size = new System.Drawing.Size(67, 21);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.btnPrev.Location = new System.Drawing.Point(32, 18);
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
            this.btnFirst.Location = new System.Drawing.Point(3, 18);
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
            this.btnLast.Location = new System.Drawing.Point(174, 18);
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
            this.label9.Location = new System.Drawing.Point(92, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(61, 19);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 21);
            this.txtPageNumber.TabIndex = 13;
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Location = new System.Drawing.Point(114, 19);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(25, 21);
            this.txtTotalPage.TabIndex = 12;
            this.txtTotalPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Appearance.Options.UseBackColor = true;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.Location = new System.Drawing.Point(145, 18);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 142;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 175;
            this.label3.Text = "Từ ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(77, 5);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(85, 22);
            this.dtpFromDate.TabIndex = 179;
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(862, 31);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(73, 26);
            this.btnFind.TabIndex = 177;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(168, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 174;
            this.label4.Text = "Đến ngày";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterText.Location = new System.Drawing.Point(404, 33);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(452, 22);
            this.txtFilterText.TabIndex = 176;
            this.txtFilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterText_KeyDown);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(239, 5);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(85, 22);
            this.dtpEndDate.TabIndex = 173;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(330, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 178;
            this.label5.Text = "Từ khóa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(402, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 172;
            this.label1.Text = "Trạng thái";
            // 
            // cboBillDocumentExportType
            // 
            this.cboBillDocumentExportType.BackColor = System.Drawing.SystemColors.Window;
            this.cboBillDocumentExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBillDocumentExportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBillDocumentExportType.ForeColor = System.Drawing.Color.Black;
            this.cboBillDocumentExportType.FormattingEnabled = true;
            this.cboBillDocumentExportType.Items.AddRange(new object[] {
            "--Tất cả--",
            "Đã hoàn thành",
            "Chưa hoàn thành"});
            this.cboBillDocumentExportType.Location = new System.Drawing.Point(476, 4);
            this.cboBillDocumentExportType.Name = "cboBillDocumentExportType";
            this.cboBillDocumentExportType.Size = new System.Drawing.Size(117, 24);
            this.cboBillDocumentExportType.TabIndex = 180;
            this.cboBillDocumentExportType.DropDownClosed += new System.EventHandler(this.cboBillDocumentExportType_DropDownClosed);
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdMaster.Location = new System.Drawing.Point(0, 100);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.cboViewNew,
            this.repositoryItemMemoEdit7,
            this.chkEditStatus,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.grdMaster.Size = new System.Drawing.Size(1321, 506);
            this.grdMaster.TabIndex = 128;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            this.grdMaster.DoubleClick += new System.EventHandler(this.grdMaster_DoubleClick);
            // 
            // grvMaster
            // 
            this.grvMaster.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grvMaster.Appearance.EvenRow.Options.UseBackColor = true;
            this.grvMaster.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvMaster.Appearance.OddRow.BackColor = System.Drawing.Color.Gainsboro;
            this.grvMaster.Appearance.OddRow.Options.UseBackColor = true;
            this.grvMaster.Appearance.Row.Options.UseForeColor = true;
            this.grvMaster.Appearance.Row.Options.UseTextOptions = true;
            this.grvMaster.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvMaster.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand3,
            this.gridBand4,
            this.gridBand2,
            this.gridBand6,
            this.gridBand5});
            this.grvMaster.ColumnPanelRowHeight = 50;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colIDMaster,
            this.colBillDocumentImportType,
            this.colIsApproved,
            this.colDateStatus,
            this.colnameStatus,
            this.colCode,
            this.colPXK,
            this.colPO,
            this.colBBBG,
            this.colBBBGHH,
            this.colProductTypeText,
            this.colDepartmentName,
            this.colEmployeeCode,
            this.colFullName,
            this.gridColumn3,
            this.colWarehouseType,
            this.gridColumn4,
            this.gridColumn5,
            this.colUnApprove,
            this.colWarehouseName,
            this.colNotePXK,
            this.colNotePO,
            this.colNoteBBBG,
            this.colBBBGHH_Note,
            this.colFullNameSender,
            this.colCustomerName,
            this.colAddress});
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            this.grvMaster.OptionsScrollAnnotations.ShowSelectedRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsSelection.MultiSelect = true;
            this.grvMaster.OptionsView.RowAutoHeight = true;
            this.grvMaster.OptionsView.ShowAutoFilterRow = true;
            this.grvMaster.OptionsView.ShowFooter = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.OptionsView.ShowIndicator = false;
            this.grvMaster.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvMaster_RowCellStyle);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = " ";
            this.gridBand1.Columns.Add(this.colIDMaster);
            this.gridBand1.Columns.Add(this.colBillDocumentImportType);
            this.gridBand1.Columns.Add(this.colIsApproved);
            this.gridBand1.Columns.Add(this.colDateStatus);
            this.gridBand1.Columns.Add(this.colnameStatus);
            this.gridBand1.Columns.Add(this.colCode);
            this.gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand1.MinWidth = 300;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.RowCount = 2;
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 586;
            // 
            // colIDMaster
            // 
            this.colIDMaster.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIDMaster.AppearanceCell.Options.UseFont = true;
            this.colIDMaster.AppearanceCell.Options.UseTextOptions = true;
            this.colIDMaster.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDMaster.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDMaster.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDMaster.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIDMaster.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIDMaster.AppearanceHeader.Options.UseFont = true;
            this.colIDMaster.AppearanceHeader.Options.UseForeColor = true;
            this.colIDMaster.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDMaster.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDMaster.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDMaster.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDMaster.Caption = "ID";
            this.colIDMaster.FieldName = "ID";
            this.colIDMaster.Name = "colIDMaster";
            this.colIDMaster.OptionsColumn.AllowEdit = false;
            this.colIDMaster.Width = 145;
            // 
            // colBillDocumentImportType
            // 
            this.colBillDocumentImportType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colBillDocumentImportType.AppearanceCell.Options.UseFont = true;
            this.colBillDocumentImportType.AppearanceCell.Options.UseTextOptions = true;
            this.colBillDocumentImportType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colBillDocumentImportType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBillDocumentImportType.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBillDocumentImportType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBillDocumentImportType.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBillDocumentImportType.AppearanceHeader.Options.UseFont = true;
            this.colBillDocumentImportType.AppearanceHeader.Options.UseForeColor = true;
            this.colBillDocumentImportType.AppearanceHeader.Options.UseTextOptions = true;
            this.colBillDocumentImportType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillDocumentImportType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBillDocumentImportType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBillDocumentImportType.Caption = "Trạng thái chứng từ";
            this.colBillDocumentImportType.FieldName = "BillDocumentExportTypeText";
            this.colBillDocumentImportType.Name = "colBillDocumentImportType";
            this.colBillDocumentImportType.Visible = true;
            this.colBillDocumentImportType.Width = 112;
            // 
            // colIsApproved
            // 
            this.colIsApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIsApproved.AppearanceCell.Options.UseFont = true;
            this.colIsApproved.AppearanceCell.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsApproved.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsApproved.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIsApproved.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIsApproved.AppearanceHeader.Options.UseFont = true;
            this.colIsApproved.AppearanceHeader.Options.UseForeColor = true;
            this.colIsApproved.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsApproved.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsApproved.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.Caption = "Nhận chứng từ";
            this.colIsApproved.ColumnEdit = this.chkEditStatus;
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.Visible = true;
            this.colIsApproved.Width = 74;
            // 
            // chkEditStatus
            // 
            this.chkEditStatus.AutoHeight = false;
            this.chkEditStatus.Caption = "Check";
            this.chkEditStatus.DisplayValueChecked = "X";
            this.chkEditStatus.DisplayValueGrayed = " ";
            this.chkEditStatus.DisplayValueUnchecked = " ";
            this.chkEditStatus.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.chkEditStatus.Name = "chkEditStatus";
            this.chkEditStatus.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colDateStatus
            // 
            this.colDateStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDateStatus.AppearanceCell.Options.UseFont = true;
            this.colDateStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colDateStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDateStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDateStatus.AppearanceHeader.Options.UseFont = true;
            this.colDateStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colDateStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDateStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateStatus.Caption = "Ngày nhận";
            this.colDateStatus.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateStatus.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateStatus.FieldName = "DateStatus";
            this.colDateStatus.Name = "colDateStatus";
            this.colDateStatus.Visible = true;
            this.colDateStatus.Width = 122;
            // 
            // colnameStatus
            // 
            this.colnameStatus.Caption = "Trạng thái";
            this.colnameStatus.FieldName = "nameStatus";
            this.colnameStatus.Name = "colnameStatus";
            this.colnameStatus.Visible = true;
            this.colnameStatus.Width = 128;
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Số phiếu";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BillImportCode", "{0}")});
            this.colCode.Visible = true;
            this.colCode.Width = 150;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridBand3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand3.Caption = "PO";
            this.gridBand3.Columns.Add(this.colPO);
            this.gridBand3.Columns.Add(this.colNotePO);
            this.gridBand3.MinWidth = 250;
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.RowCount = 2;
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 250;
            // 
            // colPO
            // 
            this.colPO.AppearanceCell.Options.UseFont = true;
            this.colPO.AppearanceCell.Options.UseTextOptions = true;
            this.colPO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPO.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPO.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPO.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPO.AppearanceHeader.Options.UseFont = true;
            this.colPO.AppearanceHeader.Options.UseForeColor = true;
            this.colPO.AppearanceHeader.Options.UseTextOptions = true;
            this.colPO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPO.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPO.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPO.Caption = "Trạng thái";
            this.colPO.ColumnEdit = this.cboViewNew;
            this.colPO.FieldName = "PO_Status";
            this.colPO.Name = "colPO";
            this.colPO.Visible = true;
            this.colPO.Width = 88;
            // 
            // cboViewNew
            // 
            this.cboViewNew.AutoHeight = false;
            this.cboViewNew.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboViewNew.Name = "cboViewNew";
            this.cboViewNew.NullText = "";
            this.cboViewNew.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colNotePO
            // 
            this.colNotePO.AppearanceCell.Options.UseForeColor = true;
            this.colNotePO.AppearanceCell.Options.UseTextOptions = true;
            this.colNotePO.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNotePO.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNotePO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNotePO.AppearanceHeader.Options.UseFont = true;
            this.colNotePO.AppearanceHeader.Options.UseTextOptions = true;
            this.colNotePO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNotePO.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNotePO.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNotePO.Caption = "Lý do / Ghi chú";
            this.colNotePO.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colNotePO.FieldName = "PO_Note";
            this.colNotePO.Name = "colNotePO";
            this.colNotePO.Visible = true;
            this.colNotePO.Width = 162;
            // 
            // repositoryItemMemoEdit7
            // 
            this.repositoryItemMemoEdit7.Name = "repositoryItemMemoEdit7";
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridBand4.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand4.AppearanceHeader.Options.UseFont = true;
            this.gridBand4.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand4.Caption = "BG";
            this.gridBand4.Columns.Add(this.colBBBG);
            this.gridBand4.Columns.Add(this.colNoteBBBG);
            this.gridBand4.MinWidth = 250;
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.RowCount = 2;
            this.gridBand4.VisibleIndex = 2;
            this.gridBand4.Width = 250;
            // 
            // colBBBG
            // 
            this.colBBBG.AppearanceCell.Options.UseFont = true;
            this.colBBBG.AppearanceCell.Options.UseTextOptions = true;
            this.colBBBG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBBBG.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBBBG.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBBBG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBBBG.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBBBG.AppearanceHeader.Options.UseFont = true;
            this.colBBBG.AppearanceHeader.Options.UseForeColor = true;
            this.colBBBG.AppearanceHeader.Options.UseTextOptions = true;
            this.colBBBG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBBBG.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBBBG.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBBBG.Caption = "Trạng thái";
            this.colBBBG.ColumnEdit = this.cboViewNew;
            this.colBBBG.FieldName = "BG_Status";
            this.colBBBG.Name = "colBBBG";
            this.colBBBG.Visible = true;
            this.colBBBG.Width = 85;
            // 
            // colNoteBBBG
            // 
            this.colNoteBBBG.AppearanceCell.Options.UseForeColor = true;
            this.colNoteBBBG.AppearanceCell.Options.UseTextOptions = true;
            this.colNoteBBBG.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNoteBBBG.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteBBBG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNoteBBBG.AppearanceHeader.Options.UseFont = true;
            this.colNoteBBBG.AppearanceHeader.Options.UseForeColor = true;
            this.colNoteBBBG.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoteBBBG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoteBBBG.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNoteBBBG.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteBBBG.Caption = "Lý do / Ghi chú";
            this.colNoteBBBG.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colNoteBBBG.FieldName = "BG_Note";
            this.colNoteBBBG.Name = "colNoteBBBG";
            this.colNoteBBBG.Visible = true;
            this.colNoteBBBG.Width = 165;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridBand2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand2.AppearanceHeader.Options.UseFont = true;
            this.gridBand2.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand2.Caption = "HD";
            this.gridBand2.Columns.Add(this.colPXK);
            this.gridBand2.Columns.Add(this.colNotePXK);
            this.gridBand2.MinWidth = 250;
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.RowCount = 2;
            this.gridBand2.VisibleIndex = 3;
            this.gridBand2.Width = 250;
            // 
            // colPXK
            // 
            this.colPXK.AppearanceCell.Options.UseFont = true;
            this.colPXK.AppearanceCell.Options.UseTextOptions = true;
            this.colPXK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPXK.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPXK.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPXK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPXK.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPXK.AppearanceHeader.Options.UseFont = true;
            this.colPXK.AppearanceHeader.Options.UseForeColor = true;
            this.colPXK.AppearanceHeader.Options.UseTextOptions = true;
            this.colPXK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPXK.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPXK.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPXK.Caption = "Trạng thái";
            this.colPXK.ColumnEdit = this.cboViewNew;
            this.colPXK.FieldName = "HD_Status";
            this.colPXK.Name = "colPXK";
            this.colPXK.Visible = true;
            this.colPXK.Width = 87;
            // 
            // colNotePXK
            // 
            this.colNotePXK.AppearanceCell.Options.UseForeColor = true;
            this.colNotePXK.AppearanceCell.Options.UseTextOptions = true;
            this.colNotePXK.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNotePXK.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNotePXK.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNotePXK.AppearanceHeader.Options.UseFont = true;
            this.colNotePXK.AppearanceHeader.Options.UseTextOptions = true;
            this.colNotePXK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNotePXK.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNotePXK.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNotePXK.Caption = "Lý do / Ghi chú";
            this.colNotePXK.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colNotePXK.FieldName = "HD_Note";
            this.colNotePXK.Name = "colNotePXK";
            this.colNotePXK.Visible = true;
            this.colNotePXK.Width = 163;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.gridBand6.AppearanceHeader.Options.UseFont = true;
            this.gridBand6.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand6.Caption = "BBBGHH";
            this.gridBand6.Columns.Add(this.colBBBGHH);
            this.gridBand6.Columns.Add(this.colBBBGHH_Note);
            this.gridBand6.MinWidth = 250;
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 4;
            this.gridBand6.Width = 250;
            // 
            // colBBBGHH
            // 
            this.colBBBGHH.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBBBGHH.AppearanceHeader.Options.UseFont = true;
            this.colBBBGHH.Caption = "Trạng thái";
            this.colBBBGHH.ColumnEdit = this.cboViewNew;
            this.colBBBGHH.FieldName = "BBBGHH_Status";
            this.colBBBGHH.Name = "colBBBGHH";
            this.colBBBGHH.Visible = true;
            this.colBBBGHH.Width = 91;
            // 
            // colBBBGHH_Note
            // 
            this.colBBBGHH_Note.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBBBGHH_Note.AppearanceHeader.Options.UseFont = true;
            this.colBBBGHH_Note.Caption = "Lý do / Ghi chú";
            this.colBBBGHH_Note.Name = "colBBBGHH_Note";
            this.colBBBGHH_Note.Visible = true;
            this.colBBBGHH_Note.Width = 159;
            // 
            // gridBand5
            // 
            this.gridBand5.Caption = " ";
            this.gridBand5.Columns.Add(this.colDepartmentName);
            this.gridBand5.Columns.Add(this.colEmployeeCode);
            this.gridBand5.Columns.Add(this.colFullName);
            this.gridBand5.Columns.Add(this.colCustomerName);
            this.gridBand5.Columns.Add(this.colAddress);
            this.gridBand5.Columns.Add(this.gridColumn3);
            this.gridBand5.Columns.Add(this.colWarehouseType);
            this.gridBand5.Columns.Add(this.colWarehouseName);
            this.gridBand5.Columns.Add(this.colProductTypeText);
            this.gridBand5.Columns.Add(this.colFullNameSender);
            this.gridBand5.Columns.Add(this.gridColumn5);
            this.gridBand5.Columns.Add(this.gridColumn4);
            this.gridBand5.Columns.Add(this.colUnApprove);
            this.gridBand5.MinWidth = 1600;
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.RowCount = 2;
            this.gridBand5.VisibleIndex = 5;
            this.gridBand5.Width = 1600;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colDepartmentName.AppearanceCell.Options.UseFont = true;
            this.colDepartmentName.AppearanceCell.Options.UseTextOptions = true;
            this.colDepartmentName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDepartmentName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDepartmentName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDepartmentName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDepartmentName.AppearanceHeader.Options.UseFont = true;
            this.colDepartmentName.AppearanceHeader.Options.UseForeColor = true;
            this.colDepartmentName.AppearanceHeader.Options.UseTextOptions = true;
            this.colDepartmentName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDepartmentName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDepartmentName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.Width = 125;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colEmployeeCode.AppearanceCell.Options.UseFont = true;
            this.colEmployeeCode.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployeeCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmployeeCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEmployeeCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colEmployeeCode.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployeeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmployeeCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeCode.Caption = "Mã NV";
            this.colEmployeeCode.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colEmployeeCode.FieldName = "EmployeeCode";
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.OptionsColumn.AllowEdit = false;
            this.colEmployeeCode.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.Width = 89;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFullName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colFullName.AppearanceHeader.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Options.UseForeColor = true;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Tên NV";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colFullName.Visible = true;
            this.colFullName.Width = 180;
            // 
            // colCustomerName
            // 
            this.colCustomerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCustomerName.AppearanceHeader.Options.UseFont = true;
            this.colCustomerName.AppearanceHeader.Options.UseTextOptions = true;
            this.colCustomerName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomerName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomerName.Caption = "Khách hàng";
            this.colCustomerName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.Width = 273;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colAddress
            // 
            this.colAddress.Caption = "Địa chỉ";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.Width = 293;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Ngày xuất";
            this.gridColumn3.FieldName = "CreatDate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.Width = 106;
            // 
            // colWarehouseType
            // 
            this.colWarehouseType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colWarehouseType.AppearanceCell.Options.UseFont = true;
            this.colWarehouseType.AppearanceCell.Options.UseTextOptions = true;
            this.colWarehouseType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWarehouseType.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWarehouseType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colWarehouseType.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colWarehouseType.AppearanceHeader.Options.UseFont = true;
            this.colWarehouseType.AppearanceHeader.Options.UseForeColor = true;
            this.colWarehouseType.AppearanceHeader.Options.UseTextOptions = true;
            this.colWarehouseType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWarehouseType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWarehouseType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWarehouseType.Caption = "Loại vật tư";
            this.colWarehouseType.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colWarehouseType.FieldName = "WarehouseType";
            this.colWarehouseType.Name = "colWarehouseType";
            this.colWarehouseType.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colWarehouseType.Visible = true;
            this.colWarehouseType.Width = 143;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colWarehouseName
            // 
            this.colWarehouseName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colWarehouseName.AppearanceCell.Options.UseFont = true;
            this.colWarehouseName.AppearanceCell.Options.UseTextOptions = true;
            this.colWarehouseName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colWarehouseName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWarehouseName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWarehouseName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colWarehouseName.AppearanceHeader.Options.UseFont = true;
            this.colWarehouseName.AppearanceHeader.Options.UseForeColor = true;
            this.colWarehouseName.AppearanceHeader.Options.UseTextOptions = true;
            this.colWarehouseName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWarehouseName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWarehouseName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWarehouseName.Caption = "Kho";
            this.colWarehouseName.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colWarehouseName.FieldName = "WarehouseName";
            this.colWarehouseName.Name = "colWarehouseName";
            this.colWarehouseName.Visible = true;
            this.colWarehouseName.Width = 101;
            // 
            // colProductTypeText
            // 
            this.colProductTypeText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductTypeText.AppearanceCell.Options.UseFont = true;
            this.colProductTypeText.AppearanceCell.Options.UseTextOptions = true;
            this.colProductTypeText.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductTypeText.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductTypeText.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductTypeText.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductTypeText.AppearanceHeader.Options.UseFont = true;
            this.colProductTypeText.AppearanceHeader.Options.UseForeColor = true;
            this.colProductTypeText.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductTypeText.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductTypeText.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductTypeText.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductTypeText.Caption = "Loại phiếu";
            this.colProductTypeText.FieldName = "ProductTypeText";
            this.colProductTypeText.Name = "colProductTypeText";
            this.colProductTypeText.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colProductTypeText.Visible = true;
            this.colProductTypeText.Width = 132;
            // 
            // colFullNameSender
            // 
            this.colFullNameSender.Caption = "Người giao";
            this.colFullNameSender.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colFullNameSender.FieldName = "FullNameSender";
            this.colFullNameSender.Name = "colFullNameSender";
            this.colFullNameSender.Visible = true;
            this.colFullNameSender.Width = 158;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn5.Caption = "Người giao";
            this.gridColumn5.FieldName = "FullNameSender";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn5.Width = 108;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "Status";
            this.gridColumn4.FieldName = "Status";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // colUnApprove
            // 
            this.colUnApprove.Caption = "gridColumn1";
            this.colUnApprove.FieldName = "UnApprove";
            this.colUnApprove.Name = "colUnApprove";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.chkAllBillExport);
            this.panelControl1.Controls.Add(this.stackPanel1);
            this.panelControl1.Controls.Add(this.dtpFromDate);
            this.panelControl1.Controls.Add(this.cbProductGroup);
            this.panelControl1.Controls.Add(this.btnFind);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.txtFilterText);
            this.panelControl1.Controls.Add(this.cboStatusNew);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.dtpEndDate);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.cboBillDocumentExportType);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 36);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1321, 64);
            this.panelControl1.TabIndex = 128;
            // 
            // stackPanel1
            // 
            this.stackPanel1.AutoSize = true;
            this.stackPanel1.Controls.Add(this.btnFirst);
            this.stackPanel1.Controls.Add(this.btnPrev);
            this.stackPanel1.Controls.Add(this.txtPageNumber);
            this.stackPanel1.Controls.Add(this.label9);
            this.stackPanel1.Controls.Add(this.txtTotalPage);
            this.stackPanel1.Controls.Add(this.btnNext);
            this.stackPanel1.Controls.Add(this.btnLast);
            this.stackPanel1.Controls.Add(this.txtPageSize);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.stackPanel1.Location = new System.Drawing.Point(1046, 2);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(273, 60);
            this.stackPanel1.TabIndex = 188;
            // 
            // frmBillDocumentExportDetail
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 606);
            this.Controls.Add(this.grdMaster);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mnuMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBillDocumentExportDetail";
            this.Text = "HỒ SƠ CHỨNG TỪ PHIẾU XUẤT SALE";
            this.Load += new System.EventHandler(this.frmBillDocumentExportDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProductGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEditStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvMaster;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIDMaster;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBillDocumentImportType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsApproved;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkEditStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDateStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductTypeText;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBBBG;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboViewNew;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNoteBBBG;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNotePO;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPXK;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNotePXK;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDepartmentName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEmployeeCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWarehouseType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWarehouseName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnApprove;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.LookUpEdit cboStatusNew;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbProductGroup;
        private System.Windows.Forms.CheckBox chkAllBillExport;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colnameStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBBBGHH;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBBBGHH_Note;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCustomerName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddress;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullNameSender;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private System.Windows.Forms.ComboBox cboBillDocumentExportType;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
    }
}