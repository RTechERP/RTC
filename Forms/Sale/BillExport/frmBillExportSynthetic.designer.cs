
namespace BMS
{
    partial class frmBillExportSynthetic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillExportSynthetic));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.chkAllBillExport = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStatusNew = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProductGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
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
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.colIDMaster = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colAddress = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUser = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCreatDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colnameStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWarehouseType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnApprove = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWarehouseName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWarehouseID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDateStatus = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsPrepared = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsReceived = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPreparedDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRequestDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNameNCC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colQty = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProjectNameText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductGroupName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductTypeText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductNewCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSerialNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCodeNCC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCustomerCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProductGroup.Properties)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
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
            this.mnuMenu.Size = new System.Drawing.Size(1443, 53);
            this.mnuMenu.TabIndex = 51;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = global::Forms.Properties.Resources.ExportToXLS_32x32;
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(69, 49);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.stackPanel1);
            this.panelControl1.Controls.Add(this.panel6);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1443, 37);
            this.panelControl1.TabIndex = 52;
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.label3);
            this.stackPanel1.Controls.Add(this.dtpFromDate);
            this.stackPanel1.Controls.Add(this.label4);
            this.stackPanel1.Controls.Add(this.dtpEndDate);
            this.stackPanel1.Controls.Add(this.chkAllBillExport);
            this.stackPanel1.Controls.Add(this.label1);
            this.stackPanel1.Controls.Add(this.cboStatusNew);
            this.stackPanel1.Controls.Add(this.label2);
            this.stackPanel1.Controls.Add(this.cbProductGroup);
            this.stackPanel1.Controls.Add(this.label5);
            this.stackPanel1.Controls.Add(this.txtFilterText);
            this.stackPanel1.Controls.Add(this.btnFind);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackPanel1.Location = new System.Drawing.Point(2, 2);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(1166, 33);
            this.stackPanel1.TabIndex = 162;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 127;
            this.label3.Text = "Từ ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(56, 6);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(91, 21);
            this.dtpFromDate.TabIndex = 136;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(153, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Đến ngày";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(213, 6);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(91, 21);
            this.dtpEndDate.TabIndex = 125;
            // 
            // chkAllBillExport
            // 
            this.chkAllBillExport.AutoSize = true;
            this.chkAllBillExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllBillExport.Location = new System.Drawing.Point(310, 6);
            this.chkAllBillExport.Name = "chkAllBillExport";
            this.chkAllBillExport.Size = new System.Drawing.Size(63, 21);
            this.chkAllBillExport.TabIndex = 158;
            this.chkAllBillExport.Text = "Tất cả";
            this.chkAllBillExport.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(379, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Lọc phiếu";
            // 
            // cboStatusNew
            // 
            this.cboStatusNew.Location = new System.Drawing.Point(439, 6);
            this.cboStatusNew.Name = "cboStatusNew";
            this.cboStatusNew.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusNew.Properties.Appearance.Options.UseFont = true;
            this.cboStatusNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusNew.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Trạng thái")});
            this.cboStatusNew.Properties.NullText = "";
            this.cboStatusNew.Size = new System.Drawing.Size(128, 20);
            this.cboStatusNew.TabIndex = 159;
            this.cboStatusNew.EditValueChanged += new System.EventHandler(this.cboStatusNew_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(573, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 123;
            this.label2.Text = "Lọc kho";
            // 
            // cbProductGroup
            // 
            this.cbProductGroup.EditValue = "";
            this.cbProductGroup.Location = new System.Drawing.Point(624, 6);
            this.cbProductGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductGroup.Name = "cbProductGroup";
            this.cbProductGroup.Properties.Appearance.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductGroup.Properties.Appearance.Options.UseFont = true;
            this.cbProductGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProductGroup.Size = new System.Drawing.Size(184, 20);
            this.cbProductGroup.TabIndex = 130;
            this.cbProductGroup.EditValueChanged += new System.EventHandler(this.cbProductGroup_EditValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(813, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 135;
            this.label5.Text = "Tìm kiếm";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(867, 6);
            this.txtFilterText.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(200, 21);
            this.txtFilterText.TabIndex = 133;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(1071, 5);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(58, 23);
            this.btnFind.TabIndex = 131;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.txtPageSize);
            this.panel6.Controls.Add(this.btnPrev);
            this.panel6.Controls.Add(this.btnFirst);
            this.panel6.Controls.Add(this.btnLast);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtPageNumber);
            this.panel6.Controls.Add(this.txtTotalPage);
            this.panel6.Controls.Add(this.btnNext);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(1168, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(273, 33);
            this.panel6.TabIndex = 157;
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(189, 7);
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
            this.txtPageSize.Size = new System.Drawing.Size(76, 21);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.Location = new System.Drawing.Point(30, 5);
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
            this.btnFirst.Location = new System.Drawing.Point(3, 5);
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
            this.btnLast.Location = new System.Drawing.Point(162, 5);
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
            this.label9.Location = new System.Drawing.Point(86, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(57, 7);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 21);
            this.txtPageNumber.TabIndex = 13;
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Location = new System.Drawing.Point(106, 7);
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
            this.btnNext.Location = new System.Drawing.Point(135, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 142;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 90);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemCheckEdit1});
            this.grdData.Size = new System.Drawing.Size(1443, 432);
            this.grdData.TabIndex = 53;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.BandPanel.Options.UseFont = true;
            this.grvData.Appearance.BandPanel.Options.UseForeColor = true;
            this.grvData.Appearance.BandPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.BandPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grvData.Appearance.EvenRow.Options.UseBackColor = true;
            this.grvData.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvData.Appearance.OddRow.BackColor = System.Drawing.Color.Gainsboro;
            this.grvData.Appearance.OddRow.Options.UseBackColor = true;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand3,
            this.gridBand1,
            this.gridBand2});
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colIDMaster,
            this.colCode,
            this.colAddress,
            this.colCustomerName,
            this.colUser,
            this.colCreatDate,
            this.colIsApproved,
            this.colnameStatus,
            this.colWarehouseType,
            this.colStatus,
            this.gridColumn2,
            this.colProductType,
            this.colUnApprove,
            this.colWarehouseName,
            this.colWarehouseID,
            this.colDateStatus,
            this.colDepartmentName,
            this.colEmployeeCode,
            this.colIsPrepared,
            this.colIsReceived,
            this.colPreparedDate,
            this.colRequestDate,
            this.colNameNCC,
            this.colProductCode,
            this.colQty,
            this.colProductName,
            this.colUnit,
            this.colProjectNameText,
            this.colProductGroupName,
            this.colNote,
            this.colProductTypeText,
            this.colProductNewCode,
            this.colSerialNumber,
            this.colCodeNCC,
            this.colCustomerCode});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsCustomization.AllowRowSizing = true;
            this.grvData.OptionsPrint.AutoWidth = false;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvData.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colIDMaster
            // 
            this.colIDMaster.AppearanceCell.Options.UseTextOptions = true;
            this.colIDMaster.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDMaster.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDMaster.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDMaster.Caption = "ID";
            this.colIDMaster.FieldName = "ID";
            this.colIDMaster.Name = "colIDMaster";
            this.colIDMaster.OptionsColumn.AllowEdit = false;
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Số phiếu";
            this.colCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCode.Visible = true;
            this.colCode.Width = 134;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colAddress
            // 
            this.colAddress.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colAddress.AppearanceCell.Options.UseFont = true;
            this.colAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddress.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.Caption = "Địa chỉ";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.Visible = true;
            this.colAddress.Width = 206;
            // 
            // colCustomerName
            // 
            this.colCustomerName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCustomerName.AppearanceCell.Options.UseFont = true;
            this.colCustomerName.AppearanceCell.Options.UseTextOptions = true;
            this.colCustomerName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomerName.Caption = "Khách hàng";
            this.colCustomerName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colCustomerName.Visible = true;
            this.colCustomerName.Width = 205;
            // 
            // colUser
            // 
            this.colUser.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colUser.AppearanceCell.Options.UseFont = true;
            this.colUser.AppearanceCell.Options.UseTextOptions = true;
            this.colUser.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUser.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUser.Caption = "Tên NV";
            this.colUser.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colUser.FieldName = "FullName";
            this.colUser.Name = "colUser";
            this.colUser.OptionsColumn.AllowEdit = false;
            this.colUser.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colUser.Visible = true;
            this.colUser.Width = 152;
            // 
            // colCreatDate
            // 
            this.colCreatDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCreatDate.AppearanceCell.Options.UseFont = true;
            this.colCreatDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatDate.Caption = "Ngày xuất";
            this.colCreatDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCreatDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colCreatDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatDate.FieldName = "CreatDate";
            this.colCreatDate.Name = "colCreatDate";
            this.colCreatDate.Visible = true;
            this.colCreatDate.Width = 101;
            // 
            // colIsApproved
            // 
            this.colIsApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIsApproved.AppearanceCell.Options.UseFont = true;
            this.colIsApproved.Caption = "Nhân chứng từ";
            this.colIsApproved.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.Visible = true;
            this.colIsApproved.Width = 74;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.DisplayValueChecked = "x";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = " ";
            this.repositoryItemCheckEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colnameStatus
            // 
            this.colnameStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colnameStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colnameStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colnameStatus.Caption = "Trạng Thái";
            this.colnameStatus.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colnameStatus.FieldName = "nameStatus";
            this.colnameStatus.Name = "colnameStatus";
            this.colnameStatus.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colnameStatus.Visible = true;
            this.colnameStatus.Width = 109;
            // 
            // colWarehouseType
            // 
            this.colWarehouseType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colWarehouseType.AppearanceCell.Options.UseFont = true;
            this.colWarehouseType.AppearanceCell.Options.UseTextOptions = true;
            this.colWarehouseType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWarehouseType.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWarehouseType.Caption = "Loại vật tư";
            this.colWarehouseType.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colWarehouseType.FieldName = "WarehouseType";
            this.colWarehouseType.Name = "colWarehouseType";
            this.colWarehouseType.Visible = true;
            this.colWarehouseType.Width = 139;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "Người giao";
            this.gridColumn2.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn2.FieldName = "FullNameSender";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 131;
            // 
            // colProductType
            // 
            this.colProductType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colProductType.AppearanceCell.Options.UseFont = true;
            this.colProductType.Caption = "Loại phiếu";
            this.colProductType.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductType.FieldName = "ProductTypeText";
            this.colProductType.Name = "colProductType";
            this.colProductType.Width = 108;
            // 
            // colUnApprove
            // 
            this.colUnApprove.Caption = "gridColumn1";
            this.colUnApprove.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colUnApprove.FieldName = "UnApprove";
            this.colUnApprove.Name = "colUnApprove";
            // 
            // colWarehouseName
            // 
            this.colWarehouseName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colWarehouseName.AppearanceCell.Options.UseFont = true;
            this.colWarehouseName.AppearanceCell.Options.UseTextOptions = true;
            this.colWarehouseName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWarehouseName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWarehouseName.Caption = "Kho";
            this.colWarehouseName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colWarehouseName.FieldName = "WarehouseName";
            this.colWarehouseName.Name = "colWarehouseName";
            this.colWarehouseName.Width = 72;
            // 
            // colWarehouseID
            // 
            this.colWarehouseID.Caption = "ID KHO";
            this.colWarehouseID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colWarehouseID.FieldName = "WarehouseID";
            this.colWarehouseID.Name = "colWarehouseID";
            // 
            // colDateStatus
            // 
            this.colDateStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDateStatus.AppearanceCell.Options.UseFont = true;
            this.colDateStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colDateStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDateStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateStatus.Caption = "Ngày nhận";
            this.colDateStatus.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDateStatus.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateStatus.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateStatus.FieldName = "DateStatus";
            this.colDateStatus.Name = "colDateStatus";
            this.colDateStatus.Visible = true;
            this.colDateStatus.Width = 108;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.Width = 123;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.Caption = "Mã NV";
            this.colEmployeeCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colEmployeeCode.FieldName = "EmployeeCode";
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.Width = 109;
            // 
            // colIsPrepared
            // 
            this.colIsPrepared.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIsPrepared.AppearanceCell.Options.UseFont = true;
            this.colIsPrepared.Caption = "Chuẩn bị xong";
            this.colIsPrepared.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsPrepared.FieldName = "IsPrepared";
            this.colIsPrepared.MinWidth = 19;
            this.colIsPrepared.Name = "colIsPrepared";
            this.colIsPrepared.Width = 74;
            // 
            // colIsReceived
            // 
            this.colIsReceived.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIsReceived.AppearanceCell.Options.UseFont = true;
            this.colIsReceived.Caption = "Đã nhận hàng";
            this.colIsReceived.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsReceived.FieldName = "IsReceived";
            this.colIsReceived.MinWidth = 19;
            this.colIsReceived.Name = "colIsReceived";
            this.colIsReceived.Width = 70;
            // 
            // colPreparedDate
            // 
            this.colPreparedDate.Caption = "Ngày chuẩn bị hàng xong";
            this.colPreparedDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPreparedDate.FieldName = "PreparedDate";
            this.colPreparedDate.MinWidth = 19;
            this.colPreparedDate.Name = "colPreparedDate";
            this.colPreparedDate.Width = 70;
            // 
            // colRequestDate
            // 
            this.colRequestDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colRequestDate.AppearanceCell.Options.UseFont = true;
            this.colRequestDate.AppearanceCell.Options.UseForeColor = true;
            this.colRequestDate.AppearanceCell.Options.UseTextOptions = true;
            this.colRequestDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRequestDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRequestDate.Caption = "Ngày yêu cầu xuất kho";
            this.colRequestDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colRequestDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colRequestDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colRequestDate.FieldName = "RequestDate";
            this.colRequestDate.MinWidth = 19;
            this.colRequestDate.Name = "colRequestDate";
            this.colRequestDate.Visible = true;
            this.colRequestDate.Width = 117;
            // 
            // colNameNCC
            // 
            this.colNameNCC.Caption = "Nhà cung cấp";
            this.colNameNCC.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNameNCC.FieldName = "NameNCC";
            this.colNameNCC.Name = "colNameNCC";
            this.colNameNCC.Visible = true;
            this.colNameNCC.Width = 165;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Mã sản phẩm";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.MinWidth = 19;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.Width = 96;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Options.UseTextOptions = true;
            this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQty.Caption = "Tổng số lượng";
            this.colQty.FieldName = "Qty";
            this.colQty.MinWidth = 19;
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.Width = 63;
            // 
            // colProductName
            // 
            this.colProductName.Caption = "Tên sản phẩm";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.MinWidth = 19;
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.Width = 106;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "ĐVT";
            this.colUnit.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colUnit.FieldName = "Unit";
            this.colUnit.MinWidth = 19;
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.Width = 52;
            // 
            // colProjectNameText
            // 
            this.colProjectNameText.Caption = "Dự án ";
            this.colProjectNameText.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectNameText.FieldName = "ProjectNameText";
            this.colProjectNameText.MinWidth = 19;
            this.colProjectNameText.Name = "colProjectNameText";
            this.colProjectNameText.Visible = true;
            this.colProjectNameText.Width = 70;
            // 
            // colProductGroupName
            // 
            this.colProductGroupName.Caption = "Loại hàng";
            this.colProductGroupName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductGroupName.FieldName = "ProductGroupName";
            this.colProductGroupName.MinWidth = 19;
            this.colProductGroupName.Name = "colProductGroupName";
            this.colProductGroupName.Visible = true;
            this.colProductGroupName.Width = 70;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.MinWidth = 19;
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.Width = 174;
            // 
            // colProductTypeText
            // 
            this.colProductTypeText.Caption = "Hàng xuất";
            this.colProductTypeText.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductTypeText.FieldName = "ProductTypeText";
            this.colProductTypeText.MinWidth = 19;
            this.colProductTypeText.Name = "colProductTypeText";
            this.colProductTypeText.Visible = true;
            this.colProductTypeText.Width = 73;
            // 
            // colProductNewCode
            // 
            this.colProductNewCode.Caption = "Mã nội bộ";
            this.colProductNewCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductNewCode.FieldName = "ProductNewCode";
            this.colProductNewCode.MinWidth = 19;
            this.colProductNewCode.Name = "colProductNewCode";
            this.colProductNewCode.Visible = true;
            this.colProductNewCode.Width = 76;
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.Caption = "SerialNumber";
            this.colSerialNumber.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSerialNumber.FieldName = "SerialNumber";
            this.colSerialNumber.MinWidth = 19;
            this.colSerialNumber.Name = "colSerialNumber";
            this.colSerialNumber.Visible = true;
            this.colSerialNumber.Width = 118;
            // 
            // colCodeNCC
            // 
            this.colCodeNCC.Caption = "Mã NCC";
            this.colCodeNCC.FieldName = "CodeNCC";
            this.colCodeNCC.Name = "colCodeNCC";
            this.colCodeNCC.Visible = true;
            this.colCodeNCC.Width = 108;
            // 
            // colCustomerCode
            // 
            this.colCustomerCode.Caption = "Mã khách hàng";
            this.colCustomerCode.FieldName = "CustomerCode";
            this.colCustomerCode.Name = "colCustomerCode";
            this.colCustomerCode.Visible = true;
            this.colCustomerCode.Width = 122;
            // 
            // gridBand3
            // 
            this.gridBand3.Columns.Add(this.colIsApproved);
            this.gridBand3.Columns.Add(this.colDateStatus);
            this.gridBand3.Columns.Add(this.colnameStatus);
            this.gridBand3.Columns.Add(this.colRequestDate);
            this.gridBand3.Columns.Add(this.colCode);
            this.gridBand3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 0;
            this.gridBand3.Width = 542;
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colIDMaster);
            this.gridBand1.Columns.Add(this.colDepartmentName);
            this.gridBand1.Columns.Add(this.colEmployeeCode);
            this.gridBand1.Columns.Add(this.colUser);
            this.gridBand1.Columns.Add(this.colCustomerCode);
            this.gridBand1.Columns.Add(this.colCustomerName);
            this.gridBand1.Columns.Add(this.colCodeNCC);
            this.gridBand1.Columns.Add(this.colNameNCC);
            this.gridBand1.Columns.Add(this.colAddress);
            this.gridBand1.Columns.Add(this.colCreatDate);
            this.gridBand1.Columns.Add(this.colWarehouseType);
            this.gridBand1.Columns.Add(this.colStatus);
            this.gridBand1.Columns.Add(this.gridColumn2);
            this.gridBand1.Columns.Add(this.colProductCode);
            this.gridBand1.Columns.Add(this.colQty);
            this.gridBand1.Columns.Add(this.colProductName);
            this.gridBand1.Columns.Add(this.colUnit);
            this.gridBand1.Columns.Add(this.colProductNewCode);
            this.gridBand1.Columns.Add(this.colProjectNameText);
            this.gridBand1.Columns.Add(this.colProductGroupName);
            this.gridBand1.Columns.Add(this.colProductTypeText);
            this.gridBand1.Columns.Add(this.colSerialNumber);
            this.gridBand1.Columns.Add(this.colNote);
            this.gridBand1.Columns.Add(this.colProductType);
            this.gridBand1.Columns.Add(this.colUnApprove);
            this.gridBand1.Columns.Add(this.colWarehouseName);
            this.gridBand1.Columns.Add(this.colWarehouseID);
            this.gridBand1.Columns.Add(this.colIsPrepared);
            this.gridBand1.Columns.Add(this.colIsReceived);
            this.gridBand1.Columns.Add(this.colPreparedDate);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 1;
            this.gridBand1.Width = 2459;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "HỒ SƠ CHỨNG TỪ";
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 2;
            // 
            // frmBillExportSynthetic
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 522);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmBillExportSynthetic";
            this.Text = "TỔNG HỢP CHI TIẾT PHIẾU XUẤT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBillExportSynthetic_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProductGroup.Properties)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.CheckBox chkAllBillExport;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit cboStatusNew;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbProductGroup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIDMaster;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddress;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCustomerName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUser;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCreatDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsApproved;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colnameStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWarehouseType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnApprove;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWarehouseName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWarehouseID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDateStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDepartmentName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEmployeeCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsPrepared;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsReceived;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPreparedDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRequestDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNameNCC;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQty;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProjectNameText;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductGroupName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductTypeText;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductNewCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSerialNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCodeNCC;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCustomerCode;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
    }
}