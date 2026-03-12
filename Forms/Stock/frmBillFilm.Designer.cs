namespace BMS
{
    partial class frmBillFilm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillFilm));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnFind = new System.Windows.Forms.Button();
			this.grdMaster = new DevExpress.XtraGrid.GridControl();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			this.btnCancelApproved = new System.Windows.Forms.ToolStripMenuItem();
			this.duyệtBáoGiáToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUser = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreatDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTypeBill = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.panelPagingToolbar = new System.Windows.Forms.Panel();
			this.txtTotalPage = new System.Windows.Forms.TextBox();
			this.btnFirst = new System.Windows.Forms.Button();
			this.txtPageSize = new System.Windows.Forms.NumericUpDown();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.btnLast = new System.Windows.Forms.Button();
			this.txtPageNumber = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtFilterText = new System.Windows.Forms.TextBox();
			this.grdData = new DevExpress.XtraGrid.GridControl();
			this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAskPriceID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemSearchLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colManufacturerCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.colSupplierID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.repositoryItemCboSupplier = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colSupplierCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colPriceCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTaxImportPercent = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTaxImporPrice = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDeliveryCost = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colBankCost = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCustomsCost = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colContactName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colContactPhone = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colContactEmail = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colContactWebsite = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTaxImporTotal = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTotalPriceCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTotalVAT = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colNeededBigBoxQty = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colRequestPriceDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemSearchLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn29 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnGoodsReceivedNote = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMenu = new System.Windows.Forms.ToolStrip();
			this.btnGoodsDeliveryNote = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
			this.panelPagingToolbar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCboSupplier)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			this.mnuMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 42);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnFind);
			this.splitContainer1.Panel1.Controls.Add(this.grdMaster);
			this.splitContainer1.Panel1.Controls.Add(this.panelPagingToolbar);
			this.splitContainer1.Panel1.Controls.Add(this.label7);
			this.splitContainer1.Panel1.Controls.Add(this.txtFilterText);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.grdData);
			this.splitContainer1.Size = new System.Drawing.Size(1127, 710);
			this.splitContainer1.SplitterDistance = 387;
			this.splitContainer1.TabIndex = 29;
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFind.Location = new System.Drawing.Point(284, 6);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(63, 23);
			this.btnFind.TabIndex = 50;
			this.btnFind.Text = "Tìm kiếm";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// grdMaster
			// 
			this.grdMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdMaster.ContextMenuStrip = this.contextMenuStrip1;
			this.grdMaster.Location = new System.Drawing.Point(3, 35);
			this.grdMaster.MainView = this.grvMaster;
			this.grdMaster.Name = "grdMaster";
			this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2,
            this.repositoryItemCheckEdit1});
			this.grdMaster.Size = new System.Drawing.Size(1121, 349);
			this.grdMaster.TabIndex = 28;
			this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
			this.grdMaster.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCancelApproved,
            this.duyệtBáoGiáToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(172, 48);
			// 
			// btnCancelApproved
			// 
			this.btnCancelApproved.Name = "btnCancelApproved";
			this.btnCancelApproved.Size = new System.Drawing.Size(171, 22);
			this.btnCancelApproved.Tag = "frmQuotation_Approved";
			this.btnCancelApproved.Text = "Hủy duyệt bảo giá";
			this.btnCancelApproved.Click += new System.EventHandler(this.btnCancelApproved_Click);
			// 
			// duyệtBáoGiáToolStripMenuItem
			// 
			this.duyệtBáoGiáToolStripMenuItem.Name = "duyệtBáoGiáToolStripMenuItem";
			this.duyệtBáoGiáToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.duyệtBáoGiáToolStripMenuItem.Tag = "frmQuotation_Approved";
			this.duyệtBáoGiáToolStripMenuItem.Text = "Duyệt báo giá";
			this.duyệtBáoGiáToolStripMenuItem.Click += new System.EventHandler(this.btnIsApproved_Click);
			// 
			// grvMaster
			// 
			this.grvMaster.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.grvMaster.Appearance.EvenRow.Options.UseBackColor = true;
			this.grvMaster.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.grvMaster.Appearance.FocusedRow.Options.UseBackColor = true;
			this.grvMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.grvMaster.Appearance.HeaderPanel.Options.UseFont = true;
			this.grvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.grvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.grvMaster.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.grvMaster.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.grvMaster.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.grvMaster.Appearance.OddRow.BackColor = System.Drawing.Color.Gainsboro;
			this.grvMaster.Appearance.OddRow.Options.UseBackColor = true;
			this.grvMaster.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.grvMaster.Appearance.SelectedRow.Options.UseBackColor = true;
			this.grvMaster.ColumnPanelRowHeight = 40;
			this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCode,
            this.colTypeName,
            this.colSupplier,
            this.colCustomer,
            this.colUser,
            this.colCreatDate,
            this.colTypeBill});
			this.grvMaster.GridControl = this.grdMaster;
			this.grvMaster.Name = "grvMaster";
			this.grvMaster.OptionsBehavior.AutoExpandAllGroups = true;
			this.grvMaster.OptionsBehavior.Editable = false;
			this.grvMaster.OptionsBehavior.ReadOnly = true;
			this.grvMaster.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
			this.grvMaster.OptionsView.ColumnAutoWidth = false;
			this.grvMaster.OptionsView.EnableAppearanceEvenRow = true;
			this.grvMaster.OptionsView.ShowGroupPanel = false;
			this.grvMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMaster_FocusedRowChanged);
			// 
			// colID
			// 
			this.colID.AppearanceCell.Options.UseTextOptions = true;
			this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colID.AppearanceHeader.Options.UseFont = true;
			this.colID.AppearanceHeader.Options.UseTextOptions = true;
			this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colID.Caption = "ID";
			this.colID.FieldName = "ID";
			this.colID.Name = "colID";
			this.colID.OptionsColumn.AllowEdit = false;
			// 
			// colCode
			// 
			this.colCode.AppearanceCell.Options.UseTextOptions = true;
			this.colCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCode.AppearanceHeader.Options.UseFont = true;
			this.colCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colCode.Caption = "Số phiếu";
			this.colCode.FieldName = "Code";
			this.colCode.Name = "colCode";
			this.colCode.OptionsColumn.AllowEdit = false;
			this.colCode.Visible = true;
			this.colCode.VisibleIndex = 0;
			this.colCode.Width = 137;
			// 
			// colTypeName
			// 
			this.colTypeName.AppearanceCell.Options.UseTextOptions = true;
			this.colTypeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colTypeName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colTypeName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colTypeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colTypeName.AppearanceHeader.Options.UseFont = true;
			this.colTypeName.AppearanceHeader.Options.UseTextOptions = true;
			this.colTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colTypeName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colTypeName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colTypeName.Caption = "Phiếu nhập/xuất";
			this.colTypeName.FieldName = "TypeName";
			this.colTypeName.Name = "colTypeName";
			this.colTypeName.OptionsColumn.AllowEdit = false;
			this.colTypeName.Visible = true;
			this.colTypeName.VisibleIndex = 1;
			this.colTypeName.Width = 144;
			// 
			// colSupplier
			// 
			this.colSupplier.AppearanceCell.Options.UseTextOptions = true;
			this.colSupplier.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSupplier.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colSupplier.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colSupplier.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colSupplier.AppearanceHeader.Options.UseFont = true;
			this.colSupplier.AppearanceHeader.Options.UseTextOptions = true;
			this.colSupplier.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSupplier.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colSupplier.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colSupplier.Caption = "Nhà cung cấp";
			this.colSupplier.FieldName = "SupplierName";
			this.colSupplier.Name = "colSupplier";
			this.colSupplier.OptionsColumn.AllowEdit = false;
			this.colSupplier.Visible = true;
			this.colSupplier.VisibleIndex = 2;
			this.colSupplier.Width = 239;
			// 
			// colCustomer
			// 
			this.colCustomer.AppearanceCell.Options.UseTextOptions = true;
			this.colCustomer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCustomer.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCustomer.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCustomer.AppearanceHeader.Options.UseFont = true;
			this.colCustomer.AppearanceHeader.Options.UseTextOptions = true;
			this.colCustomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCustomer.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCustomer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colCustomer.Caption = "Khách hàng";
			this.colCustomer.FieldName = "CustomerName";
			this.colCustomer.Name = "colCustomer";
			this.colCustomer.OptionsColumn.AllowEdit = false;
			this.colCustomer.Visible = true;
			this.colCustomer.VisibleIndex = 3;
			this.colCustomer.Width = 146;
			// 
			// colUser
			// 
			this.colUser.AppearanceCell.Options.UseTextOptions = true;
			this.colUser.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUser.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colUser.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colUser.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUser.AppearanceHeader.Options.UseFont = true;
			this.colUser.AppearanceHeader.Options.UseTextOptions = true;
			this.colUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUser.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colUser.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colUser.Caption = "Người nhập/xuất";
			this.colUser.FieldName = "FullName";
			this.colUser.Name = "colUser";
			this.colUser.OptionsColumn.AllowEdit = false;
			this.colUser.Visible = true;
			this.colUser.VisibleIndex = 4;
			this.colUser.Width = 136;
			// 
			// colCreatDate
			// 
			this.colCreatDate.AppearanceCell.Options.UseTextOptions = true;
			this.colCreatDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreatDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCreatDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colCreatDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCreatDate.AppearanceHeader.Options.UseFont = true;
			this.colCreatDate.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreatDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreatDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCreatDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colCreatDate.Caption = "Ngày nhập";
			this.colCreatDate.FieldName = "CreatDate";
			this.colCreatDate.Name = "colCreatDate";
			this.colCreatDate.OptionsColumn.AllowEdit = false;
			this.colCreatDate.Visible = true;
			this.colCreatDate.VisibleIndex = 5;
			this.colCreatDate.Width = 181;
			// 
			// colTypeBill
			// 
			this.colTypeBill.Caption = "Type Bill";
			this.colTypeBill.FieldName = "TypeBill";
			this.colTypeBill.Name = "colTypeBill";
			this.colTypeBill.OptionsColumn.AllowEdit = false;
			// 
			// repositoryItemMemoEdit2
			// 
			this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Caption = "Check";
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// panelPagingToolbar
			// 
			this.panelPagingToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPagingToolbar.Controls.Add(this.txtTotalPage);
			this.panelPagingToolbar.Controls.Add(this.btnFirst);
			this.panelPagingToolbar.Controls.Add(this.txtPageSize);
			this.panelPagingToolbar.Controls.Add(this.btnPrev);
			this.panelPagingToolbar.Controls.Add(this.btnNext);
			this.panelPagingToolbar.Controls.Add(this.label6);
			this.panelPagingToolbar.Controls.Add(this.btnLast);
			this.panelPagingToolbar.Controls.Add(this.txtPageNumber);
			this.panelPagingToolbar.Location = new System.Drawing.Point(863, 3);
			this.panelPagingToolbar.Name = "panelPagingToolbar";
			this.panelPagingToolbar.Size = new System.Drawing.Size(261, 27);
			this.panelPagingToolbar.TabIndex = 49;
			// 
			// txtTotalPage
			// 
			this.txtTotalPage.Location = new System.Drawing.Point(108, 4);
			this.txtTotalPage.Name = "txtTotalPage";
			this.txtTotalPage.ReadOnly = true;
			this.txtTotalPage.Size = new System.Drawing.Size(27, 20);
			this.txtTotalPage.TabIndex = 8;
			// 
			// btnFirst
			// 
			this.btnFirst.Location = new System.Drawing.Point(2, 2);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(30, 23);
			this.btnFirst.TabIndex = 7;
			this.btnFirst.Text = "<<";
			this.btnFirst.UseVisualStyleBackColor = true;
			this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
			// 
			// txtPageSize
			// 
			this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.txtPageSize.Location = new System.Drawing.Point(204, 4);
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
			this.txtPageSize.Size = new System.Drawing.Size(54, 20);
			this.txtPageSize.TabIndex = 11;
			this.txtPageSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.txtPageSize.ValueChanged += new System.EventHandler(this.txtPageSize_ValueChanged);
			// 
			// btnPrev
			// 
			this.btnPrev.Location = new System.Drawing.Point(34, 2);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(30, 23);
			this.btnPrev.TabIndex = 7;
			this.btnPrev.Text = "<";
			this.btnPrev.UseVisualStyleBackColor = true;
			this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(138, 2);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(30, 23);
			this.btnNext.TabIndex = 7;
			this.btnNext.Text = ">";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(97, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(12, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "\\";
			// 
			// btnLast
			// 
			this.btnLast.Location = new System.Drawing.Point(170, 2);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(30, 23);
			this.btnLast.TabIndex = 7;
			this.btnLast.Text = ">>";
			this.btnLast.UseVisualStyleBackColor = true;
			this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
			// 
			// txtPageNumber
			// 
			this.txtPageNumber.Location = new System.Drawing.Point(67, 4);
			this.txtPageNumber.Name = "txtPageNumber";
			this.txtPageNumber.ReadOnly = true;
			this.txtPageNumber.Size = new System.Drawing.Size(27, 20);
			this.txtPageNumber.TabIndex = 8;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
			this.label7.Location = new System.Drawing.Point(11, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 13);
			this.label7.TabIndex = 47;
			this.label7.Text = "Từ khóa";
			// 
			// txtFilterText
			// 
			this.txtFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilterText.Location = new System.Drawing.Point(61, 7);
			this.txtFilterText.Name = "txtFilterText";
			this.txtFilterText.Size = new System.Drawing.Size(208, 20);
			this.txtFilterText.TabIndex = 48;
			// 
			// grdData
			// 
			this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdData.Location = new System.Drawing.Point(3, 3);
			this.grdData.MainView = this.grvData;
			this.grdData.Name = "grdData";
			this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemSearchLookUpEdit1,
            this.repositoryItemSearchLookUpEdit2,
            this.repositoryItemSearchLookUpEdit3});
			this.grdData.Size = new System.Drawing.Size(1124, 293);
			this.grdData.TabIndex = 30;
			this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
			// 
			// grvData
			// 
			this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
			this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.grvData.Appearance.Row.Options.UseTextOptions = true;
			this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.grvData.ColumnPanelRowHeight = 50;
			this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colAskPriceID,
            this.colManufacturerCode,
            this.colSupplierID,
            this.colQty,
            this.colVAT,
            this.colPriceCurrency,
            this.colTaxImportPercent,
            this.colTaxImporPrice,
            this.colDeliveryCost,
            this.colBankCost,
            this.colCustomsCost,
            this.colContactName,
            this.colContactPhone,
            this.colContactEmail,
            this.colContactWebsite,
            this.colTaxImporTotal,
            this.colTotalPriceCurrency,
            this.colTotalVAT,
            this.colName,
            this.colNeededBigBoxQty,
            this.colRequestPriceDetailID});
			this.grvData.CustomizationFormBounds = new System.Drawing.Rectangle(1390, 432, 210, 382);
			this.grvData.GridControl = this.grdData;
			this.grvData.Name = "grvData";
			this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
			this.grvData.OptionsBehavior.Editable = false;
			this.grvData.OptionsView.ColumnAutoWidth = false;
			this.grvData.OptionsView.RowAutoHeight = true;
			this.grvData.OptionsView.ShowFooter = true;
			this.grvData.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn1.AppearanceHeader.Options.UseFont = true;
			this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn1.Caption = "ID";
			this.gridColumn1.FieldName = "ID";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			// 
			// colAskPriceID
			// 
			this.colAskPriceID.AppearanceCell.Options.UseTextOptions = true;
			this.colAskPriceID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colAskPriceID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colAskPriceID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colAskPriceID.AppearanceHeader.Options.UseFont = true;
			this.colAskPriceID.AppearanceHeader.Options.UseTextOptions = true;
			this.colAskPriceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colAskPriceID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colAskPriceID.Caption = "Người phụ trách";
			this.colAskPriceID.ColumnEdit = this.repositoryItemSearchLookUpEdit3;
			this.colAskPriceID.FieldName = "AskPriceID";
			this.colAskPriceID.Name = "colAskPriceID";
			this.colAskPriceID.Width = 98;
			// 
			// repositoryItemSearchLookUpEdit3
			// 
			this.repositoryItemSearchLookUpEdit3.AutoHeight = false;
			this.repositoryItemSearchLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemSearchLookUpEdit3.Name = "repositoryItemSearchLookUpEdit3";
			this.repositoryItemSearchLookUpEdit3.NullText = "";
			this.repositoryItemSearchLookUpEdit3.View = this.gridView1;
			// 
			// gridView1
			// 
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23});
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn21
			// 
			this.gridColumn21.Caption = "ID";
			this.gridColumn21.FieldName = "ID";
			this.gridColumn21.Name = "gridColumn21";
			this.gridColumn21.Width = 128;
			// 
			// gridColumn22
			// 
			this.gridColumn22.Caption = "Mã";
			this.gridColumn22.FieldName = "Code";
			this.gridColumn22.Name = "gridColumn22";
			this.gridColumn22.Visible = true;
			this.gridColumn22.VisibleIndex = 0;
			this.gridColumn22.Width = 128;
			// 
			// gridColumn23
			// 
			this.gridColumn23.Caption = "Tên";
			this.gridColumn23.FieldName = "FullName";
			this.gridColumn23.Name = "gridColumn23";
			this.gridColumn23.Visible = true;
			this.gridColumn23.VisibleIndex = 1;
			this.gridColumn23.Width = 175;
			// 
			// colManufacturerCode
			// 
			this.colManufacturerCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.colManufacturerCode.AppearanceHeader.Options.UseFont = true;
			this.colManufacturerCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colManufacturerCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colManufacturerCode.Caption = "Hãng";
			this.colManufacturerCode.ColumnEdit = this.repositoryItemMemoEdit1;
			this.colManufacturerCode.FieldName = "ManufacturerCode";
			this.colManufacturerCode.Name = "colManufacturerCode";
			this.colManufacturerCode.OptionsColumn.AllowEdit = false;
			this.colManufacturerCode.Width = 113;
			// 
			// repositoryItemMemoEdit1
			// 
			this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
			// 
			// colSupplierID
			// 
			this.colSupplierID.Caption = "Nhà cung cấp";
			this.colSupplierID.ColumnEdit = this.repositoryItemSearchLookUpEdit1;
			this.colSupplierID.FieldName = "SupplierID";
			this.colSupplierID.Name = "colSupplierID";
			this.colSupplierID.Width = 109;
			// 
			// repositoryItemSearchLookUpEdit1
			// 
			this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
			this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
			this.repositoryItemSearchLookUpEdit1.NullText = "";
			this.repositoryItemSearchLookUpEdit1.View = this.repositoryItemCboSupplier;
			// 
			// repositoryItemCboSupplier
			// 
			this.repositoryItemCboSupplier.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn25,
            this.colSupplierCode,
            this.gridColumn26});
			this.repositoryItemCboSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.repositoryItemCboSupplier.Name = "repositoryItemCboSupplier";
			this.repositoryItemCboSupplier.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.repositoryItemCboSupplier.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn25
			// 
			this.gridColumn25.Caption = "ID";
			this.gridColumn25.FieldName = "ID";
			this.gridColumn25.Name = "gridColumn25";
			this.gridColumn25.Width = 128;
			// 
			// colSupplierCode
			// 
			this.colSupplierCode.Caption = "Mã";
			this.colSupplierCode.FieldName = "SupplierCode";
			this.colSupplierCode.Name = "colSupplierCode";
			this.colSupplierCode.Visible = true;
			this.colSupplierCode.VisibleIndex = 0;
			this.colSupplierCode.Width = 128;
			// 
			// gridColumn26
			// 
			this.gridColumn26.Caption = "Tên";
			this.gridColumn26.FieldName = "SupplierName";
			this.gridColumn26.Name = "gridColumn26";
			this.gridColumn26.Visible = true;
			this.gridColumn26.VisibleIndex = 1;
			this.gridColumn26.Width = 175;
			// 
			// colQty
			// 
			this.colQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colQty.AppearanceCell.Options.UseFont = true;
			this.colQty.AppearanceCell.Options.UseTextOptions = true;
			this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colQty.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colQty.AppearanceHeader.Options.UseFont = true;
			this.colQty.AppearanceHeader.Options.UseTextOptions = true;
			this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colQty.Caption = "Số lượng";
			this.colQty.ColumnEdit = this.repositoryItemTextEdit1;
			this.colQty.DisplayFormat.FormatString = "n0";
			this.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colQty.FieldName = "Qty";
			this.colQty.Name = "colQty";
			this.colQty.Visible = true;
			this.colQty.VisibleIndex = 1;
			this.colQty.Width = 122;
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.DisplayFormat.FormatString = "n0";
			this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemTextEdit1.EditFormat.FormatString = "n0";
			this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemTextEdit1.Mask.EditMask = "n0";
			this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// colVAT
			// 
			this.colVAT.Caption = "VAT (%)";
			this.colVAT.ColumnEdit = this.repositoryItemTextEdit1;
			this.colVAT.DisplayFormat.FormatString = "n0";
			this.colVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colVAT.FieldName = "VAT";
			this.colVAT.Name = "colVAT";
			this.colVAT.Width = 36;
			// 
			// colPriceCurrency
			// 
			this.colPriceCurrency.Caption = "Đơn giá ngoại tệ";
			this.colPriceCurrency.ColumnEdit = this.repositoryItemTextEdit1;
			this.colPriceCurrency.DisplayFormat.FormatString = "n1";
			this.colPriceCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colPriceCurrency.FieldName = "PriceCurrency";
			this.colPriceCurrency.Name = "colPriceCurrency";
			// 
			// colTaxImportPercent
			// 
			this.colTaxImportPercent.Caption = "Thuế NK (%)";
			this.colTaxImportPercent.ColumnEdit = this.repositoryItemTextEdit1;
			this.colTaxImportPercent.DisplayFormat.FormatString = "n0";
			this.colTaxImportPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colTaxImportPercent.FieldName = "TaxImportPercent";
			this.colTaxImportPercent.Name = "colTaxImportPercent";
			this.colTaxImportPercent.ToolTip = "Thuế nhập khẩu (%)";
			this.colTaxImportPercent.Width = 40;
			// 
			// colTaxImporPrice
			// 
			this.colTaxImporPrice.Caption = "Chi phí nhập khẩu";
			this.colTaxImporPrice.ColumnEdit = this.repositoryItemTextEdit1;
			this.colTaxImporPrice.DisplayFormat.FormatString = "n0";
			this.colTaxImporPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colTaxImporPrice.FieldName = "TaxImporPrice";
			this.colTaxImporPrice.Name = "colTaxImporPrice";
			this.colTaxImporPrice.OptionsColumn.AllowEdit = false;
			// 
			// colDeliveryCost
			// 
			this.colDeliveryCost.Caption = "Chi phí vận chuyển";
			this.colDeliveryCost.ColumnEdit = this.repositoryItemTextEdit1;
			this.colDeliveryCost.DisplayFormat.FormatString = "n0";
			this.colDeliveryCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colDeliveryCost.FieldName = "DeliveryCost";
			this.colDeliveryCost.Name = "colDeliveryCost";
			this.colDeliveryCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DeliveryCost", "{0:n0}")});
			this.colDeliveryCost.Width = 59;
			// 
			// colBankCost
			// 
			this.colBankCost.Caption = "Chi phí ngân hàng";
			this.colBankCost.ColumnEdit = this.repositoryItemTextEdit1;
			this.colBankCost.DisplayFormat.FormatString = "n0";
			this.colBankCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colBankCost.FieldName = "BankCost";
			this.colBankCost.Name = "colBankCost";
			this.colBankCost.OptionsColumn.AllowEdit = false;
			this.colBankCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BankCost", "{0:n0}")});
			// 
			// colCustomsCost
			// 
			this.colCustomsCost.Caption = "Chi phí hải quan";
			this.colCustomsCost.ColumnEdit = this.repositoryItemTextEdit1;
			this.colCustomsCost.DisplayFormat.FormatString = "n0";
			this.colCustomsCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colCustomsCost.FieldName = "CustomsCost";
			this.colCustomsCost.Name = "colCustomsCost";
			this.colCustomsCost.OptionsColumn.AllowEdit = false;
			this.colCustomsCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CustomsCost", "{0:n0}")});
			// 
			// colContactName
			// 
			this.colContactName.Caption = "Người liên hệ";
			this.colContactName.FieldName = "ContactName";
			this.colContactName.Name = "colContactName";
			this.colContactName.Width = 128;
			// 
			// colContactPhone
			// 
			this.colContactPhone.Caption = "SĐT";
			this.colContactPhone.FieldName = "ContactPhone";
			this.colContactPhone.Name = "colContactPhone";
			this.colContactPhone.Width = 129;
			// 
			// colContactEmail
			// 
			this.colContactEmail.Caption = "Email";
			this.colContactEmail.FieldName = "ContactEmail";
			this.colContactEmail.Name = "colContactEmail";
			this.colContactEmail.Width = 160;
			// 
			// colContactWebsite
			// 
			this.colContactWebsite.Caption = "Website";
			this.colContactWebsite.FieldName = "ContactWebsite";
			this.colContactWebsite.Name = "colContactWebsite";
			this.colContactWebsite.Width = 214;
			// 
			// colTaxImporTotal
			// 
			this.colTaxImporTotal.Caption = "Tổng chi phí nhập khẩu";
			this.colTaxImporTotal.ColumnEdit = this.repositoryItemTextEdit1;
			this.colTaxImporTotal.DisplayFormat.FormatString = "n0";
			this.colTaxImporTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colTaxImporTotal.FieldName = "TaxImporTotal";
			this.colTaxImporTotal.Name = "colTaxImporTotal";
			this.colTaxImporTotal.OptionsColumn.AllowEdit = false;
			this.colTaxImporTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TaxImporTotal", "{0:n0}")});
			this.colTaxImporTotal.Width = 81;
			// 
			// colTotalPriceCurrency
			// 
			this.colTotalPriceCurrency.Caption = "Tổng giá nhập ngoại tệ";
			this.colTotalPriceCurrency.ColumnEdit = this.repositoryItemTextEdit1;
			this.colTotalPriceCurrency.FieldName = "TotalPriceCurrency";
			this.colTotalPriceCurrency.Name = "colTotalPriceCurrency";
			this.colTotalPriceCurrency.OptionsColumn.AllowEdit = false;
			this.colTotalPriceCurrency.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPriceCurrency", "{0:n0}")});
			// 
			// colTotalVAT
			// 
			this.colTotalVAT.Caption = "Tổng tiền VAT";
			this.colTotalVAT.ColumnEdit = this.repositoryItemTextEdit1;
			this.colTotalVAT.FieldName = "TotalVAT";
			this.colTotalVAT.Name = "colTotalVAT";
			this.colTotalVAT.OptionsColumn.AllowEdit = false;
			this.colTotalVAT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalVAT", "{0:n0}")});
			this.colTotalVAT.Width = 71;
			// 
			// colName
			// 
			this.colName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colName.AppearanceCell.Options.UseFont = true;
			this.colName.AppearanceCell.Options.UseTextOptions = true;
			this.colName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colName.AppearanceHeader.Options.UseFont = true;
			this.colName.AppearanceHeader.Options.UseTextOptions = true;
			this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colName.Caption = "Sản phẩm";
			this.colName.ColumnEdit = this.repositoryItemMemoEdit1;
			this.colName.FieldName = "Name";
			this.colName.Name = "colName";
			this.colName.Visible = true;
			this.colName.VisibleIndex = 0;
			this.colName.Width = 145;
			// 
			// colNeededBigBoxQty
			// 
			this.colNeededBigBoxQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colNeededBigBoxQty.AppearanceCell.Options.UseFont = true;
			this.colNeededBigBoxQty.AppearanceCell.Options.UseTextOptions = true;
			this.colNeededBigBoxQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colNeededBigBoxQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colNeededBigBoxQty.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colNeededBigBoxQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colNeededBigBoxQty.AppearanceHeader.Options.UseFont = true;
			this.colNeededBigBoxQty.AppearanceHeader.Options.UseTextOptions = true;
			this.colNeededBigBoxQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colNeededBigBoxQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colNeededBigBoxQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colNeededBigBoxQty.Caption = "Số hộp to cần cắt";
			this.colNeededBigBoxQty.ColumnEdit = this.repositoryItemMemoEdit1;
			this.colNeededBigBoxQty.FieldName = "NeededBigBoxQty";
			this.colNeededBigBoxQty.Name = "colNeededBigBoxQty";
			this.colNeededBigBoxQty.Visible = true;
			this.colNeededBigBoxQty.VisibleIndex = 2;
			this.colNeededBigBoxQty.Width = 165;
			// 
			// colRequestPriceDetailID
			// 
			this.colRequestPriceDetailID.Caption = "RequestPriceDetailID";
			this.colRequestPriceDetailID.FieldName = "RequestPriceDetailID";
			this.colRequestPriceDetailID.Name = "colRequestPriceDetailID";
			// 
			// repositoryItemSearchLookUpEdit2
			// 
			this.repositoryItemSearchLookUpEdit2.AutoHeight = false;
			this.repositoryItemSearchLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemSearchLookUpEdit2.Name = "repositoryItemSearchLookUpEdit2";
			this.repositoryItemSearchLookUpEdit2.NullText = "";
			this.repositoryItemSearchLookUpEdit2.View = this.gridView2;
			// 
			// gridView2
			// 
			this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn28,
            this.gridColumn29,
            this.gridColumn30});
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn28
			// 
			this.gridColumn28.Caption = "ID";
			this.gridColumn28.FieldName = "ID";
			this.gridColumn28.Name = "gridColumn28";
			this.gridColumn28.Width = 128;
			// 
			// gridColumn29
			// 
			this.gridColumn29.Caption = "Mã";
			this.gridColumn29.FieldName = "ManufacturerCode";
			this.gridColumn29.Name = "gridColumn29";
			this.gridColumn29.Visible = true;
			this.gridColumn29.VisibleIndex = 0;
			// 
			// gridColumn30
			// 
			this.gridColumn30.Caption = "Tên";
			this.gridColumn30.FieldName = "ManufacturerName";
			this.gridColumn30.Name = "gridColumn30";
			this.gridColumn30.Visible = true;
			this.gridColumn30.VisibleIndex = 1;
			this.gridColumn30.Width = 309;
			// 
			// btnGoodsReceivedNote
			// 
			this.btnGoodsReceivedNote.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGoodsReceivedNote.Image = ((System.Drawing.Image)(resources.GetObject("btnGoodsReceivedNote.Image")));
			this.btnGoodsReceivedNote.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnGoodsReceivedNote.Name = "btnGoodsReceivedNote";
			this.btnGoodsReceivedNote.Size = new System.Drawing.Size(108, 33);
			this.btnGoodsReceivedNote.Tag = "";
			this.btnGoodsReceivedNote.Text = "Thêm phiếu nhập";
			this.btnGoodsReceivedNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnGoodsReceivedNote.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
			// 
			// btnDelete
			// 
			this.btnDelete.AutoSize = false;
			this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(55, 40);
			this.btnDelete.Tag = "";
			this.btnDelete.Text = "Xóa";
			this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// mnuMenu
			// 
			this.mnuMenu.AutoSize = false;
			this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGoodsReceivedNote,
            this.toolStripSeparator2,
            this.btnGoodsDeliveryNote,
            this.toolStripSeparator3,
            this.btnDelete,
            this.toolStripSeparator1});
			this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.mnuMenu.Location = new System.Drawing.Point(0, 0);
			this.mnuMenu.Name = "mnuMenu";
			this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.mnuMenu.Size = new System.Drawing.Size(1127, 42);
			this.mnuMenu.TabIndex = 23;
			this.mnuMenu.Text = "toolStrip2";
			// 
			// btnGoodsDeliveryNote
			// 
			this.btnGoodsDeliveryNote.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGoodsDeliveryNote.Image = ((System.Drawing.Image)(resources.GetObject("btnGoodsDeliveryNote.Image")));
			this.btnGoodsDeliveryNote.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnGoodsDeliveryNote.Name = "btnGoodsDeliveryNote";
			this.btnGoodsDeliveryNote.Size = new System.Drawing.Size(106, 33);
			this.btnGoodsDeliveryNote.Tag = "";
			this.btnGoodsDeliveryNote.Text = "Thêm phiếu xuất";
			this.btnGoodsDeliveryNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnGoodsDeliveryNote.Click += new System.EventHandler(this.btnGoodsDeliveryNote_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
			// 
			// frmBillFilm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1127, 752);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.mnuMenu);
			this.Name = "frmBillFilm";
			this.Text = "KHO";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmQuotation_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
			this.panelPagingToolbar.ResumeLayout(false);
			this.panelPagingToolbar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCboSupplier)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			this.mnuMenu.ResumeLayout(false);
			this.mnuMenu.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Panel panelPagingToolbar;
        private System.Windows.Forms.TextBox txtTotalPage;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAskPriceID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemCboSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImportPercent;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImporPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliveryCost;
        private DevExpress.XtraGrid.Columns.GridColumn colBankCost;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomsCost;
        private DevExpress.XtraGrid.Columns.GridColumn colContactName;
        private DevExpress.XtraGrid.Columns.GridColumn colContactPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colContactEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colContactWebsite;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxImporTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPriceCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalVAT;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn29;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colNeededBigBoxQty;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestPriceDetailID;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnCancelApproved;
        private System.Windows.Forms.ToolStripMenuItem duyệtBáoGiáToolStripMenuItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.ToolStripButton btnGoodsReceivedNote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip mnuMenu;
		private DevExpress.XtraGrid.Columns.GridColumn colID;
		private DevExpress.XtraGrid.Columns.GridColumn colCode;
		private DevExpress.XtraGrid.Columns.GridColumn colTypeName;
		private DevExpress.XtraGrid.Columns.GridColumn colSupplier;
		private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
		private DevExpress.XtraGrid.Columns.GridColumn colUser;
		private DevExpress.XtraGrid.Columns.GridColumn colCreatDate;
		private System.Windows.Forms.ToolStripButton btnGoodsDeliveryNote;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private DevExpress.XtraGrid.Columns.GridColumn colTypeBill;
	}
}