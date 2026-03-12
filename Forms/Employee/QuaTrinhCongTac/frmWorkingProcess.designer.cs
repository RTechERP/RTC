
namespace BMS
{
    partial class frmWorkingProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkingProcess));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnapproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbkDuyet = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJobPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIndirectManagement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDirectManagement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDecisionNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDecisionDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProbationarySalary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBasicSalary = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShiftEat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGasoline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHouse = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSkin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiligence = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOther = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbkDuyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator4,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnApproved,
            this.toolStripSeparator1,
            this.btnUnapproved,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 43);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 40);
            this.btnNew.Tag = "frmWorkingProcess_HRUse";
            this.btnNew.Text = "Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 43);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Image = global::Forms.Properties.Resources.edit_16x16;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 40);
            this.btnEdit.Tag = "frmWorkingProcess_HRUse";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 40);
            this.btnDelete.Tag = "frmWorkingProcess_HRUse";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // btnApproved
            // 
            this.btnApproved.AutoSize = false;
            this.btnApproved.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnApproved.Image = ((System.Drawing.Image)(resources.GetObject("btnApproved.Image")));
            this.btnApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApproved.Name = "btnApproved";
            this.btnApproved.Size = new System.Drawing.Size(80, 40);
            this.btnApproved.Tag = "frmWorkingProcess_HRUse";
            this.btnApproved.Text = "Duyệt";
            this.btnApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApproved.Click += new System.EventHandler(this.btnApproved_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnUnapproved
            // 
            this.btnUnapproved.AutoSize = false;
            this.btnUnapproved.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnUnapproved.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreviewLarge;
            this.btnUnapproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnapproved.Name = "btnUnapproved";
            this.btnUnapproved.Size = new System.Drawing.Size(120, 40);
            this.btnUnapproved.Tag = "frmWorkingProcess_HRUse";
            this.btnUnapproved.Text = "Hủy duyệt";
            this.btnUnapproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnapproved.Click += new System.EventHandler(this.btnUnapproved_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(100, 40);
            this.toolStripButton1.Tag = "frmWorkingProcess_HRUse";
            this.toolStripButton1.Text = " Trạng thái";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(12, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 128;
            this.label3.Text = "Từ ngày";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(73, 4);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(107, 24);
            this.dtpStartDate.TabIndex = 137;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(186, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 138;
            this.label4.Text = "Đến ngày";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(254, 4);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(107, 24);
            this.dtpEndDate.TabIndex = 139;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label5.Location = new System.Drawing.Point(367, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 140;
            this.label5.Text = "Từ khóa";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSearch.Location = new System.Drawing.Point(428, 4);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(222, 24);
            this.txtSearch.TabIndex = 141;
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnFind.Location = new System.Drawing.Point(656, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 27);
            this.btnFind.TabIndex = 142;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(203, 4);
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
            this.txtPageSize.Size = new System.Drawing.Size(65, 24);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.Value = new decimal(new int[] {
            1000000000,
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
            this.btnPrev.Location = new System.Drawing.Point(32, 6);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPrev.Size = new System.Drawing.Size(23, 20);
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
            this.btnFirst.Location = new System.Drawing.Point(3, 6);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnFirst.Size = new System.Drawing.Size(23, 20);
            this.btnFirst.TabIndex = 143;
            this.btnFirst.Text = "Trang trước";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnLast.Appearance.Options.UseBackColor = true;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.Location = new System.Drawing.Point(174, 6);
            this.btnLast.Name = "btnLast";
            this.btnLast.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnLast.Size = new System.Drawing.Size(23, 20);
            this.btnLast.TabIndex = 144;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(92, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPageNumber.Location = new System.Drawing.Point(61, 4);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 24);
            this.txtPageNumber.TabIndex = 13;
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTotalPage.Location = new System.Drawing.Point(114, 4);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(25, 24);
            this.txtTotalPage.TabIndex = 12;
            this.txtTotalPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Appearance.Options.UseBackColor = true;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.Location = new System.Drawing.Point(145, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(23, 20);
            this.btnNext.TabIndex = 142;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 32);
            this.panel1.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txtPageSize);
            this.panel2.Controls.Add(this.btnFirst);
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.txtTotalPage);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Controls.Add(this.txtPageNumber);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1092, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 32);
            this.panel2.TabIndex = 203;
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Location = new System.Drawing.Point(0, 75);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbkDuyet,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.grdData.Size = new System.Drawing.Size(1370, 668);
            this.grdData.TabIndex = 31;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colIsApproved,
            this.colFullName,
            this.colStartDate,
            this.colEndDate,
            this.colJobPosition,
            this.colWorkUnit,
            this.colStatus,
            this.colIndirectManagement,
            this.colDirectManagement,
            this.colDecisionNumber,
            this.colDecisionDay,
            this.colProbationarySalary,
            this.colBasicSalary,
            this.colShiftEat,
            this.colGasoline,
            this.colPhone,
            this.colHouse,
            this.colSkin,
            this.colDiligence,
            this.colOther,
            this.colNote,
            this.colCreatedDate,
            this.colCreatedBy,
            this.colUpdatedBy,
            this.colUpdatedDate});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.RowHeight = 30;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Width = 294;
            // 
            // colIsApproved
            // 
            this.colIsApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.colIsApproved.AppearanceCell.Options.UseFont = true;
            this.colIsApproved.AppearanceCell.Options.UseTextOptions = true;
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
            this.colIsApproved.Caption = "Duyệt";
            this.colIsApproved.ColumnEdit = this.cbkDuyet;
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.MaxWidth = 60;
            this.colIsApproved.MinWidth = 60;
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.OptionsColumn.AllowEdit = false;
            this.colIsApproved.OptionsColumn.AllowMove = false;
            this.colIsApproved.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIsApproved.OptionsFilter.AllowAutoFilter = false;
            this.colIsApproved.OptionsFilter.AllowFilter = false;
            this.colIsApproved.Visible = true;
            this.colIsApproved.VisibleIndex = 0;
            this.colIsApproved.Width = 60;
            // 
            // cbkDuyet
            // 
            this.cbkDuyet.AutoHeight = false;
            this.cbkDuyet.Name = "cbkDuyet";
            this.cbkDuyet.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F);
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
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullName.FieldName = "NV";
            this.colFullName.MaxWidth = 150;
            this.colFullName.MinWidth = 150;
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.AllowMove = false;
            this.colFullName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFullName.OptionsFilter.AllowAutoFilter = false;
            this.colFullName.OptionsFilter.AllowFilter = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 1;
            this.colFullName.Width = 150;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.colStartDate.AppearanceCell.Options.UseFont = true;
            this.colStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStartDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colStartDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStartDate.AppearanceHeader.Options.UseFont = true;
            this.colStartDate.AppearanceHeader.Options.UseForeColor = true;
            this.colStartDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStartDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStartDate.Caption = "Từ ngày";
            this.colStartDate.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.MaxWidth = 100;
            this.colStartDate.MinWidth = 100;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.OptionsColumn.AllowEdit = false;
            this.colStartDate.OptionsColumn.AllowMove = false;
            this.colStartDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStartDate.OptionsFilter.AllowAutoFilter = false;
            this.colStartDate.OptionsFilter.AllowFilter = false;
            this.colStartDate.Visible = true;
            this.colStartDate.VisibleIndex = 2;
            this.colStartDate.Width = 100;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.colEndDate.AppearanceCell.Options.UseFont = true;
            this.colEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEndDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEndDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colEndDate.AppearanceHeader.Options.UseFont = true;
            this.colEndDate.AppearanceHeader.Options.UseForeColor = true;
            this.colEndDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEndDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEndDate.Caption = "Đến ngày";
            this.colEndDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.MaxWidth = 100;
            this.colEndDate.MinWidth = 100;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.OptionsColumn.AllowEdit = false;
            this.colEndDate.OptionsColumn.AllowMove = false;
            this.colEndDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEndDate.OptionsFilter.AllowAutoFilter = false;
            this.colEndDate.OptionsFilter.AllowFilter = false;
            this.colEndDate.Visible = true;
            this.colEndDate.VisibleIndex = 3;
            this.colEndDate.Width = 100;
            // 
            // colJobPosition
            // 
            this.colJobPosition.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.colJobPosition.AppearanceCell.Options.UseFont = true;
            this.colJobPosition.AppearanceCell.Options.UseTextOptions = true;
            this.colJobPosition.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colJobPosition.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colJobPosition.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colJobPosition.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colJobPosition.AppearanceHeader.Options.UseFont = true;
            this.colJobPosition.AppearanceHeader.Options.UseForeColor = true;
            this.colJobPosition.AppearanceHeader.Options.UseTextOptions = true;
            this.colJobPosition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colJobPosition.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colJobPosition.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colJobPosition.Caption = "Vị trí ";
            this.colJobPosition.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colJobPosition.FieldName = "Name";
            this.colJobPosition.MaxWidth = 120;
            this.colJobPosition.MinWidth = 120;
            this.colJobPosition.Name = "colJobPosition";
            this.colJobPosition.OptionsColumn.AllowEdit = false;
            this.colJobPosition.OptionsColumn.AllowMove = false;
            this.colJobPosition.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colJobPosition.OptionsFilter.AllowAutoFilter = false;
            this.colJobPosition.OptionsFilter.AllowFilter = false;
            this.colJobPosition.Visible = true;
            this.colJobPosition.VisibleIndex = 4;
            this.colJobPosition.Width = 120;
            // 
            // colWorkUnit
            // 
            this.colWorkUnit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colWorkUnit.AppearanceCell.Options.UseFont = true;
            this.colWorkUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colWorkUnit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWorkUnit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWorkUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colWorkUnit.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colWorkUnit.AppearanceHeader.Options.UseFont = true;
            this.colWorkUnit.AppearanceHeader.Options.UseForeColor = true;
            this.colWorkUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colWorkUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWorkUnit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colWorkUnit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colWorkUnit.Caption = "Đơn vị công tác";
            this.colWorkUnit.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colWorkUnit.FieldName = "Names";
            this.colWorkUnit.MaxWidth = 120;
            this.colWorkUnit.MinWidth = 120;
            this.colWorkUnit.Name = "colWorkUnit";
            this.colWorkUnit.OptionsColumn.AllowEdit = false;
            this.colWorkUnit.OptionsColumn.AllowMove = false;
            this.colWorkUnit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colWorkUnit.OptionsFilter.AllowAutoFilter = false;
            this.colWorkUnit.OptionsFilter.AllowFilter = false;
            this.colWorkUnit.Visible = true;
            this.colWorkUnit.VisibleIndex = 5;
            this.colWorkUnit.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colStatus.AppearanceCell.Options.UseFont = true;
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colStatus.FieldName = "StatusName";
            this.colStatus.MaxWidth = 110;
            this.colStatus.MinWidth = 110;
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.OptionsColumn.AllowMove = false;
            this.colStatus.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStatus.OptionsFilter.AllowAutoFilter = false;
            this.colStatus.OptionsFilter.AllowFilter = false;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 6;
            this.colStatus.Width = 110;
            // 
            // colIndirectManagement
            // 
            this.colIndirectManagement.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIndirectManagement.AppearanceCell.Options.UseFont = true;
            this.colIndirectManagement.AppearanceCell.Options.UseTextOptions = true;
            this.colIndirectManagement.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIndirectManagement.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIndirectManagement.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIndirectManagement.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIndirectManagement.AppearanceHeader.Options.UseFont = true;
            this.colIndirectManagement.AppearanceHeader.Options.UseForeColor = true;
            this.colIndirectManagement.AppearanceHeader.Options.UseTextOptions = true;
            this.colIndirectManagement.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIndirectManagement.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIndirectManagement.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIndirectManagement.Caption = "Quản lý gián tiếp";
            this.colIndirectManagement.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colIndirectManagement.FieldName = "QuanLyGT";
            this.colIndirectManagement.MaxWidth = 150;
            this.colIndirectManagement.MinWidth = 150;
            this.colIndirectManagement.Name = "colIndirectManagement";
            this.colIndirectManagement.OptionsColumn.AllowEdit = false;
            this.colIndirectManagement.OptionsColumn.AllowMove = false;
            this.colIndirectManagement.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIndirectManagement.OptionsFilter.AllowAutoFilter = false;
            this.colIndirectManagement.OptionsFilter.AllowFilter = false;
            this.colIndirectManagement.Visible = true;
            this.colIndirectManagement.VisibleIndex = 7;
            this.colIndirectManagement.Width = 150;
            // 
            // colDirectManagement
            // 
            this.colDirectManagement.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDirectManagement.AppearanceCell.Options.UseFont = true;
            this.colDirectManagement.AppearanceCell.Options.UseTextOptions = true;
            this.colDirectManagement.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDirectManagement.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDirectManagement.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDirectManagement.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDirectManagement.AppearanceHeader.Options.UseFont = true;
            this.colDirectManagement.AppearanceHeader.Options.UseForeColor = true;
            this.colDirectManagement.AppearanceHeader.Options.UseTextOptions = true;
            this.colDirectManagement.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDirectManagement.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDirectManagement.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDirectManagement.Caption = "Quản lý trực tiếp";
            this.colDirectManagement.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDirectManagement.FieldName = "QuanLyTT";
            this.colDirectManagement.MaxWidth = 150;
            this.colDirectManagement.MinWidth = 150;
            this.colDirectManagement.Name = "colDirectManagement";
            this.colDirectManagement.OptionsColumn.AllowEdit = false;
            this.colDirectManagement.OptionsColumn.AllowMove = false;
            this.colDirectManagement.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDirectManagement.OptionsFilter.AllowAutoFilter = false;
            this.colDirectManagement.OptionsFilter.AllowFilter = false;
            this.colDirectManagement.Visible = true;
            this.colDirectManagement.VisibleIndex = 8;
            this.colDirectManagement.Width = 150;
            // 
            // colDecisionNumber
            // 
            this.colDecisionNumber.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDecisionNumber.AppearanceCell.Options.UseFont = true;
            this.colDecisionNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colDecisionNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDecisionNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDecisionNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDecisionNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDecisionNumber.AppearanceHeader.Options.UseFont = true;
            this.colDecisionNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colDecisionNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colDecisionNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDecisionNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDecisionNumber.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDecisionNumber.Caption = "Số quyết định";
            this.colDecisionNumber.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDecisionNumber.FieldName = "DecisionNumber";
            this.colDecisionNumber.MaxWidth = 100;
            this.colDecisionNumber.MinWidth = 100;
            this.colDecisionNumber.Name = "colDecisionNumber";
            this.colDecisionNumber.OptionsColumn.AllowEdit = false;
            this.colDecisionNumber.OptionsColumn.AllowMove = false;
            this.colDecisionNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDecisionNumber.OptionsFilter.AllowAutoFilter = false;
            this.colDecisionNumber.OptionsFilter.AllowFilter = false;
            this.colDecisionNumber.Visible = true;
            this.colDecisionNumber.VisibleIndex = 9;
            this.colDecisionNumber.Width = 100;
            // 
            // colDecisionDay
            // 
            this.colDecisionDay.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDecisionDay.AppearanceCell.Options.UseFont = true;
            this.colDecisionDay.AppearanceCell.Options.UseTextOptions = true;
            this.colDecisionDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDecisionDay.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDecisionDay.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDecisionDay.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDecisionDay.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDecisionDay.AppearanceHeader.Options.UseFont = true;
            this.colDecisionDay.AppearanceHeader.Options.UseForeColor = true;
            this.colDecisionDay.AppearanceHeader.Options.UseTextOptions = true;
            this.colDecisionDay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDecisionDay.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDecisionDay.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDecisionDay.Caption = "Ngày quyết định";
            this.colDecisionDay.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDecisionDay.FieldName = "DecisionDay";
            this.colDecisionDay.MaxWidth = 100;
            this.colDecisionDay.MinWidth = 100;
            this.colDecisionDay.Name = "colDecisionDay";
            this.colDecisionDay.OptionsColumn.AllowEdit = false;
            this.colDecisionDay.OptionsColumn.AllowMove = false;
            this.colDecisionDay.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDecisionDay.OptionsFilter.AllowAutoFilter = false;
            this.colDecisionDay.OptionsFilter.AllowFilter = false;
            this.colDecisionDay.Visible = true;
            this.colDecisionDay.VisibleIndex = 10;
            this.colDecisionDay.Width = 100;
            // 
            // colProbationarySalary
            // 
            this.colProbationarySalary.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colProbationarySalary.AppearanceCell.Options.UseFont = true;
            this.colProbationarySalary.AppearanceCell.Options.UseTextOptions = true;
            this.colProbationarySalary.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProbationarySalary.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProbationarySalary.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProbationarySalary.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProbationarySalary.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProbationarySalary.AppearanceHeader.Options.UseFont = true;
            this.colProbationarySalary.AppearanceHeader.Options.UseForeColor = true;
            this.colProbationarySalary.AppearanceHeader.Options.UseTextOptions = true;
            this.colProbationarySalary.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProbationarySalary.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProbationarySalary.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProbationarySalary.Caption = "Lương thử việc";
            this.colProbationarySalary.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProbationarySalary.DisplayFormat.FormatString = "N0";
            this.colProbationarySalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProbationarySalary.FieldName = "ProbationarySalary";
            this.colProbationarySalary.MaxWidth = 80;
            this.colProbationarySalary.MinWidth = 80;
            this.colProbationarySalary.Name = "colProbationarySalary";
            this.colProbationarySalary.OptionsColumn.AllowEdit = false;
            this.colProbationarySalary.OptionsColumn.AllowMove = false;
            this.colProbationarySalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colProbationarySalary.OptionsFilter.AllowAutoFilter = false;
            this.colProbationarySalary.OptionsFilter.AllowFilter = false;
            this.colProbationarySalary.Visible = true;
            this.colProbationarySalary.VisibleIndex = 11;
            this.colProbationarySalary.Width = 80;
            // 
            // colBasicSalary
            // 
            this.colBasicSalary.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colBasicSalary.AppearanceCell.Options.UseFont = true;
            this.colBasicSalary.AppearanceCell.Options.UseTextOptions = true;
            this.colBasicSalary.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBasicSalary.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBasicSalary.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBasicSalary.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBasicSalary.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBasicSalary.AppearanceHeader.Options.UseFont = true;
            this.colBasicSalary.AppearanceHeader.Options.UseForeColor = true;
            this.colBasicSalary.AppearanceHeader.Options.UseTextOptions = true;
            this.colBasicSalary.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBasicSalary.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBasicSalary.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBasicSalary.Caption = "Lương cơ bản";
            this.colBasicSalary.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBasicSalary.DisplayFormat.FormatString = "N0";
            this.colBasicSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBasicSalary.FieldName = "BasicSalary";
            this.colBasicSalary.MaxWidth = 80;
            this.colBasicSalary.MinWidth = 80;
            this.colBasicSalary.Name = "colBasicSalary";
            this.colBasicSalary.OptionsColumn.AllowEdit = false;
            this.colBasicSalary.OptionsColumn.AllowMove = false;
            this.colBasicSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBasicSalary.OptionsFilter.AllowAutoFilter = false;
            this.colBasicSalary.OptionsFilter.AllowFilter = false;
            this.colBasicSalary.Visible = true;
            this.colBasicSalary.VisibleIndex = 12;
            this.colBasicSalary.Width = 80;
            // 
            // colShiftEat
            // 
            this.colShiftEat.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colShiftEat.AppearanceCell.Options.UseFont = true;
            this.colShiftEat.AppearanceCell.Options.UseTextOptions = true;
            this.colShiftEat.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colShiftEat.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShiftEat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colShiftEat.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colShiftEat.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colShiftEat.AppearanceHeader.Options.UseFont = true;
            this.colShiftEat.AppearanceHeader.Options.UseForeColor = true;
            this.colShiftEat.AppearanceHeader.Options.UseTextOptions = true;
            this.colShiftEat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShiftEat.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShiftEat.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colShiftEat.Caption = "Ăn ca";
            this.colShiftEat.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colShiftEat.DisplayFormat.FormatString = "N0";
            this.colShiftEat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colShiftEat.FieldName = "ShiftEat";
            this.colShiftEat.MaxWidth = 80;
            this.colShiftEat.MinWidth = 80;
            this.colShiftEat.Name = "colShiftEat";
            this.colShiftEat.OptionsColumn.AllowEdit = false;
            this.colShiftEat.OptionsColumn.AllowMove = false;
            this.colShiftEat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colShiftEat.OptionsFilter.AllowAutoFilter = false;
            this.colShiftEat.OptionsFilter.AllowFilter = false;
            this.colShiftEat.Visible = true;
            this.colShiftEat.VisibleIndex = 13;
            this.colShiftEat.Width = 80;
            // 
            // colGasoline
            // 
            this.colGasoline.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colGasoline.AppearanceCell.Options.UseFont = true;
            this.colGasoline.AppearanceCell.Options.UseTextOptions = true;
            this.colGasoline.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGasoline.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGasoline.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGasoline.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colGasoline.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGasoline.AppearanceHeader.Options.UseFont = true;
            this.colGasoline.AppearanceHeader.Options.UseForeColor = true;
            this.colGasoline.AppearanceHeader.Options.UseTextOptions = true;
            this.colGasoline.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGasoline.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGasoline.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGasoline.Caption = "Xăng Xe";
            this.colGasoline.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colGasoline.DisplayFormat.FormatString = "N0";
            this.colGasoline.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGasoline.FieldName = "Gasoline";
            this.colGasoline.MaxWidth = 80;
            this.colGasoline.MinWidth = 80;
            this.colGasoline.Name = "colGasoline";
            this.colGasoline.OptionsColumn.AllowEdit = false;
            this.colGasoline.OptionsColumn.AllowMove = false;
            this.colGasoline.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colGasoline.OptionsFilter.AllowAutoFilter = false;
            this.colGasoline.OptionsFilter.AllowFilter = false;
            this.colGasoline.Visible = true;
            this.colGasoline.VisibleIndex = 14;
            this.colGasoline.Width = 80;
            // 
            // colPhone
            // 
            this.colPhone.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colPhone.AppearanceCell.Options.UseFont = true;
            this.colPhone.AppearanceCell.Options.UseTextOptions = true;
            this.colPhone.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPhone.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPhone.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPhone.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPhone.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPhone.AppearanceHeader.Options.UseFont = true;
            this.colPhone.AppearanceHeader.Options.UseForeColor = true;
            this.colPhone.AppearanceHeader.Options.UseTextOptions = true;
            this.colPhone.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPhone.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPhone.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPhone.Caption = "Điện thoại";
            this.colPhone.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPhone.DisplayFormat.FormatString = "N0";
            this.colPhone.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPhone.FieldName = "Phone";
            this.colPhone.MaxWidth = 80;
            this.colPhone.MinWidth = 80;
            this.colPhone.Name = "colPhone";
            this.colPhone.OptionsColumn.AllowEdit = false;
            this.colPhone.OptionsColumn.AllowMove = false;
            this.colPhone.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPhone.OptionsFilter.AllowAutoFilter = false;
            this.colPhone.OptionsFilter.AllowFilter = false;
            this.colPhone.Visible = true;
            this.colPhone.VisibleIndex = 15;
            this.colPhone.Width = 80;
            // 
            // colHouse
            // 
            this.colHouse.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colHouse.AppearanceCell.Options.UseFont = true;
            this.colHouse.AppearanceCell.Options.UseTextOptions = true;
            this.colHouse.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colHouse.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHouse.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHouse.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colHouse.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colHouse.AppearanceHeader.Options.UseFont = true;
            this.colHouse.AppearanceHeader.Options.UseForeColor = true;
            this.colHouse.AppearanceHeader.Options.UseTextOptions = true;
            this.colHouse.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHouse.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHouse.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHouse.Caption = "Nhà ở";
            this.colHouse.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colHouse.DisplayFormat.FormatString = "N0";
            this.colHouse.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHouse.FieldName = "House";
            this.colHouse.MaxWidth = 80;
            this.colHouse.MinWidth = 80;
            this.colHouse.Name = "colHouse";
            this.colHouse.OptionsColumn.AllowEdit = false;
            this.colHouse.OptionsColumn.AllowMove = false;
            this.colHouse.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colHouse.OptionsFilter.AllowAutoFilter = false;
            this.colHouse.OptionsFilter.AllowFilter = false;
            this.colHouse.Visible = true;
            this.colHouse.VisibleIndex = 16;
            this.colHouse.Width = 80;
            // 
            // colSkin
            // 
            this.colSkin.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colSkin.AppearanceCell.Options.UseFont = true;
            this.colSkin.AppearanceCell.Options.UseTextOptions = true;
            this.colSkin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSkin.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSkin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSkin.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colSkin.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSkin.AppearanceHeader.Options.UseFont = true;
            this.colSkin.AppearanceHeader.Options.UseForeColor = true;
            this.colSkin.AppearanceHeader.Options.UseTextOptions = true;
            this.colSkin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSkin.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSkin.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSkin.Caption = "Trang phục";
            this.colSkin.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSkin.DisplayFormat.FormatString = "N0";
            this.colSkin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSkin.FieldName = "Skin";
            this.colSkin.MaxWidth = 80;
            this.colSkin.MinWidth = 80;
            this.colSkin.Name = "colSkin";
            this.colSkin.OptionsColumn.AllowEdit = false;
            this.colSkin.OptionsColumn.AllowMove = false;
            this.colSkin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSkin.OptionsFilter.AllowAutoFilter = false;
            this.colSkin.OptionsFilter.AllowFilter = false;
            this.colSkin.Visible = true;
            this.colSkin.VisibleIndex = 17;
            this.colSkin.Width = 80;
            // 
            // colDiligence
            // 
            this.colDiligence.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDiligence.AppearanceCell.Options.UseFont = true;
            this.colDiligence.AppearanceCell.Options.UseTextOptions = true;
            this.colDiligence.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colDiligence.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiligence.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiligence.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDiligence.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDiligence.AppearanceHeader.Options.UseFont = true;
            this.colDiligence.AppearanceHeader.Options.UseForeColor = true;
            this.colDiligence.AppearanceHeader.Options.UseTextOptions = true;
            this.colDiligence.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDiligence.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDiligence.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDiligence.Caption = "Chuyên cần";
            this.colDiligence.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDiligence.DisplayFormat.FormatString = "N0";
            this.colDiligence.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDiligence.FieldName = "Diligence";
            this.colDiligence.MaxWidth = 80;
            this.colDiligence.MinWidth = 80;
            this.colDiligence.Name = "colDiligence";
            this.colDiligence.OptionsColumn.AllowEdit = false;
            this.colDiligence.OptionsColumn.AllowMove = false;
            this.colDiligence.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDiligence.OptionsFilter.AllowAutoFilter = false;
            this.colDiligence.OptionsFilter.AllowFilter = false;
            this.colDiligence.Visible = true;
            this.colDiligence.VisibleIndex = 18;
            this.colDiligence.Width = 80;
            // 
            // colOther
            // 
            this.colOther.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colOther.AppearanceCell.Options.UseFont = true;
            this.colOther.AppearanceCell.Options.UseTextOptions = true;
            this.colOther.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOther.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOther.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOther.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colOther.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colOther.AppearanceHeader.Options.UseFont = true;
            this.colOther.AppearanceHeader.Options.UseForeColor = true;
            this.colOther.AppearanceHeader.Options.UseTextOptions = true;
            this.colOther.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colOther.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colOther.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colOther.Caption = "Khác";
            this.colOther.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colOther.DisplayFormat.FormatString = "N0";
            this.colOther.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOther.FieldName = "Other";
            this.colOther.MaxWidth = 80;
            this.colOther.MinWidth = 80;
            this.colOther.Name = "colOther";
            this.colOther.OptionsColumn.AllowEdit = false;
            this.colOther.OptionsColumn.AllowMove = false;
            this.colOther.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colOther.OptionsFilter.AllowAutoFilter = false;
            this.colOther.OptionsFilter.AllowFilter = false;
            this.colOther.Visible = true;
            this.colOther.VisibleIndex = 19;
            this.colOther.Width = 80;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNote.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.MaxWidth = 250;
            this.colNote.MinWidth = 250;
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.OptionsColumn.AllowMove = false;
            this.colNote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNote.OptionsFilter.AllowAutoFilter = false;
            this.colNote.OptionsFilter.AllowFilter = false;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 20;
            this.colNote.Width = 250;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCreatedDate.AppearanceCell.Options.UseFont = true;
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCreatedDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreatedDate.AppearanceHeader.Options.UseFont = true;
            this.colCreatedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Width = 136;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCreatedBy.AppearanceCell.Options.UseFont = true;
            this.colCreatedBy.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedBy.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedBy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedBy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCreatedBy.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreatedBy.AppearanceHeader.Options.UseFont = true;
            this.colCreatedBy.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedBy.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedBy.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedBy.Caption = "Người tạo";
            this.colCreatedBy.FieldName = "CreatedBy";
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.Width = 136;
            // 
            // colUpdatedBy
            // 
            this.colUpdatedBy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colUpdatedBy.AppearanceCell.Options.UseFont = true;
            this.colUpdatedBy.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdatedBy.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdatedBy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedBy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUpdatedBy.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUpdatedBy.AppearanceHeader.Options.UseFont = true;
            this.colUpdatedBy.AppearanceHeader.Options.UseForeColor = true;
            this.colUpdatedBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdatedBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdatedBy.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdatedBy.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedBy.Caption = "Người update";
            this.colUpdatedBy.FieldName = "UpdatedBy";
            this.colUpdatedBy.Name = "colUpdatedBy";
            this.colUpdatedBy.Width = 136;
            // 
            // colUpdatedDate
            // 
            this.colUpdatedDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colUpdatedDate.AppearanceCell.Options.UseFont = true;
            this.colUpdatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdatedDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdatedDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUpdatedDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUpdatedDate.AppearanceHeader.Options.UseFont = true;
            this.colUpdatedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colUpdatedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdatedDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdatedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedDate.Caption = "Ngày Update";
            this.colUpdatedDate.FieldName = "UpdatedDate";
            this.colUpdatedDate.Name = "colUpdatedDate";
            this.colUpdatedDate.Width = 136;
            // 
            // frmWorkingProcess
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 743);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmWorkingProcess";
            this.Text = "QUÁ TRÌNH CÔNG TÁC";
            this.Load += new System.EventHandler(this.WorkingProcess_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbkDuyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnApproved;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUnapproved;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApproved;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit cbkDuyet;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colJobPosition;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colIndirectManagement;
        private DevExpress.XtraGrid.Columns.GridColumn colDirectManagement;
        private DevExpress.XtraGrid.Columns.GridColumn colDecisionNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDecisionDay;
        private DevExpress.XtraGrid.Columns.GridColumn colProbationarySalary;
        private DevExpress.XtraGrid.Columns.GridColumn colBasicSalary;
        private DevExpress.XtraGrid.Columns.GridColumn colShiftEat;
        private DevExpress.XtraGrid.Columns.GridColumn colGasoline;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colHouse;
        private DevExpress.XtraGrid.Columns.GridColumn colSkin;
        private DevExpress.XtraGrid.Columns.GridColumn colDiligence;
        private DevExpress.XtraGrid.Columns.GridColumn colOther;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
    }
}