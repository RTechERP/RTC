
namespace BMS
{
    partial class frmInventoryByProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventoryByProduct));
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.productID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ProductNewCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.ProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.WarehouseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WarehouseType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InventoryTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InventoryReal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NumberBorrowing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ProductGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.SupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colMaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.btnLoad = new System.Windows.Forms.Button();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chiTiếtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExport = new System.Windows.Forms.Button();
            this.colTotalRequestImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalRequestExport = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "New item selection";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.miniToolStrip.Location = new System.Drawing.Point(0, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.miniToolStrip.Size = new System.Drawing.Size(1656, 48);
            this.miniToolStrip.TabIndex = 20;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(91, 42);
            this.btnExportExcel.Tag = "";
            this.btnExportExcel.Text = "Xuất phiếu";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 925F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 105);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1232, 416);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(0, 38);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemMemoEdit3,
            this.repositoryItemMemoEdit4});
            this.grdData.Size = new System.Drawing.Size(1334, 643);
            this.grdData.TabIndex = 49;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdData_MouseDown);
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
            this.grvData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvData.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            this.grvData.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.AppearancePrint.Row.Options.UseFont = true;
            this.grvData.AppearancePrint.Row.Options.UseTextOptions = true;
            this.grvData.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.productID,
            this.ProductNewCode,
            this.ProductCode,
            this.ProductName,
            this.WarehouseName,
            this.WarehouseType,
            this.InventoryTotal,
            this.InventoryReal,
            this.NumberBorrowing,
            this.ProductGroupName,
            this.SupplierName,
            this.colMaker,
            this.colUnitName,
            this.colTotalRequestImport,
            this.colTotalRequestExport});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 2;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsPrint.AutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.WarehouseType, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.WarehouseName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            // 
            // productID
            // 
            this.productID.Caption = "productID";
            this.productID.FieldName = "ID";
            this.productID.MinWidth = 19;
            this.productID.Name = "productID";
            this.productID.Width = 70;
            // 
            // ProductNewCode
            // 
            this.ProductNewCode.Caption = "Mã nội bộ";
            this.ProductNewCode.FieldName = "ProductNewCode";
            this.ProductNewCode.MinWidth = 19;
            this.ProductNewCode.Name = "ProductNewCode";
            this.ProductNewCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ProductNewCode", "{0:n0}")});
            this.ProductNewCode.Visible = true;
            this.ProductNewCode.VisibleIndex = 1;
            this.ProductNewCode.Width = 104;
            // 
            // ProductCode
            // 
            this.ProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.ProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ProductCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ProductCode.Caption = "Mã sản phẩm";
            this.ProductCode.ColumnEdit = this.repositoryItemMemoEdit2;
            this.ProductCode.FieldName = "ProductCode";
            this.ProductCode.MinWidth = 19;
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Visible = true;
            this.ProductCode.VisibleIndex = 2;
            this.ProductCode.Width = 139;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // ProductName
            // 
            this.ProductName.AppearanceCell.Options.UseTextOptions = true;
            this.ProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.ProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ProductName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ProductName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ProductName.Caption = "Tên sản phẩm";
            this.ProductName.ColumnEdit = this.repositoryItemMemoEdit3;
            this.ProductName.FieldName = "ProductName";
            this.ProductName.MinWidth = 19;
            this.ProductName.Name = "ProductName";
            this.ProductName.Visible = true;
            this.ProductName.VisibleIndex = 3;
            this.ProductName.Width = 165;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // WarehouseName
            // 
            this.WarehouseName.Caption = "Tên kho";
            this.WarehouseName.FieldName = "WarehouseName";
            this.WarehouseName.MinWidth = 19;
            this.WarehouseName.Name = "WarehouseName";
            this.WarehouseName.Visible = true;
            this.WarehouseName.VisibleIndex = 4;
            this.WarehouseName.Width = 74;
            // 
            // WarehouseType
            // 
            this.WarehouseType.Caption = "Loại kho";
            this.WarehouseType.FieldName = "WarehouseType";
            this.WarehouseType.MinWidth = 19;
            this.WarehouseType.Name = "WarehouseType";
            this.WarehouseType.Visible = true;
            this.WarehouseType.VisibleIndex = 4;
            this.WarehouseType.Width = 98;
            // 
            // InventoryTotal
            // 
            this.InventoryTotal.AppearanceCell.Options.UseTextOptions = true;
            this.InventoryTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.InventoryTotal.Caption = "Tồn thực tế";
            this.InventoryTotal.DisplayFormat.FormatString = "n2";
            this.InventoryTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.InventoryTotal.FieldName = "InventoryTotal";
            this.InventoryTotal.MinWidth = 19;
            this.InventoryTotal.Name = "InventoryTotal";
            this.InventoryTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InventoryTotal", "{0:n2}")});
            this.InventoryTotal.Visible = true;
            this.InventoryTotal.VisibleIndex = 6;
            this.InventoryTotal.Width = 80;
            // 
            // InventoryReal
            // 
            this.InventoryReal.AppearanceCell.Options.UseTextOptions = true;
            this.InventoryReal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.InventoryReal.Caption = "Tồn CK (Được sử dụng)";
            this.InventoryReal.DisplayFormat.FormatString = "n2";
            this.InventoryReal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.InventoryReal.FieldName = "InventoryReal";
            this.InventoryReal.MinWidth = 19;
            this.InventoryReal.Name = "InventoryReal";
            this.InventoryReal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InventoryReal", "{0:n2}")});
            this.InventoryReal.Visible = true;
            this.InventoryReal.VisibleIndex = 7;
            this.InventoryReal.Width = 98;
            // 
            // NumberBorrowing
            // 
            this.NumberBorrowing.AppearanceCell.Options.UseTextOptions = true;
            this.NumberBorrowing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.NumberBorrowing.Caption = "Đang mượn";
            this.NumberBorrowing.DisplayFormat.FormatString = "n2";
            this.NumberBorrowing.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.NumberBorrowing.FieldName = "NumberBorrowing";
            this.NumberBorrowing.MinWidth = 19;
            this.NumberBorrowing.Name = "NumberBorrowing";
            this.NumberBorrowing.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NumberBorrowing", "{0:n2}")});
            this.NumberBorrowing.Visible = true;
            this.NumberBorrowing.VisibleIndex = 8;
            this.NumberBorrowing.Width = 80;
            // 
            // ProductGroupName
            // 
            this.ProductGroupName.AppearanceCell.Options.UseTextOptions = true;
            this.ProductGroupName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ProductGroupName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ProductGroupName.AppearanceHeader.Options.UseTextOptions = true;
            this.ProductGroupName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ProductGroupName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ProductGroupName.Caption = "Tên nhóm";
            this.ProductGroupName.ColumnEdit = this.repositoryItemMemoEdit4;
            this.ProductGroupName.FieldName = "ProductGroupName";
            this.ProductGroupName.MinWidth = 19;
            this.ProductGroupName.Name = "ProductGroupName";
            this.ProductGroupName.Visible = true;
            this.ProductGroupName.VisibleIndex = 0;
            this.ProductGroupName.Width = 156;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // SupplierName
            // 
            this.SupplierName.AppearanceCell.Options.UseTextOptions = true;
            this.SupplierName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SupplierName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.SupplierName.AppearanceHeader.Options.UseTextOptions = true;
            this.SupplierName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SupplierName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.SupplierName.Caption = "Tên nhà cung cấp";
            this.SupplierName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.SupplierName.FieldName = "SupplierName";
            this.SupplierName.MinWidth = 19;
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.Visible = true;
            this.SupplierName.VisibleIndex = 11;
            this.SupplierName.Width = 271;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colMaker
            // 
            this.colMaker.Caption = "Hãng";
            this.colMaker.FieldName = "Maker";
            this.colMaker.Name = "colMaker";
            this.colMaker.Visible = true;
            this.colMaker.VisibleIndex = 4;
            this.colMaker.Width = 140;
            // 
            // colUnitName
            // 
            this.colUnitName.Caption = "ĐVT";
            this.colUnitName.FieldName = "UnitName";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.Visible = true;
            this.colUnitName.VisibleIndex = 5;
            this.colUnitName.Width = 70;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // btnLoad
            // 
            this.btnLoad.AutoSize = true;
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(478, 6);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(74, 27);
            this.btnLoad.TabIndex = 48;
            this.btnLoad.Text = "Tìm kiếm";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1656, 48);
            this.mnuMenu.TabIndex = 20;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tìm kiếm";
            // 
            // txtProductCode
            // 
            this.txtProductCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductCode.Location = new System.Drawing.Point(76, 8);
            this.txtProductCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(396, 23);
            this.txtProductCode.TabIndex = 2;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chiTiếtToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(113, 26);
            // 
            // chiTiếtToolStripMenuItem
            // 
            this.chiTiếtToolStripMenuItem.Name = "chiTiếtToolStripMenuItem";
            this.chiTiếtToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.chiTiếtToolStripMenuItem.Text = "Chi tiết";
            this.chiTiếtToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtToolStripMenuItem_Click);
            // 
            // btnExport
            // 
            this.btnExport.AutoSize = true;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(570, 6);
            this.btnExport.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(83, 27);
            this.btnExport.TabIndex = 48;
            this.btnExport.Text = "Xuất excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // colTotalRequestImport
            // 
            this.colTotalRequestImport.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalRequestImport.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalRequestImport.Caption = "Y/c nhập";
            this.colTotalRequestImport.DisplayFormat.FormatString = "n2";
            this.colTotalRequestImport.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalRequestImport.FieldName = "TotalRequestImport";
            this.colTotalRequestImport.Name = "colTotalRequestImport";
            this.colTotalRequestImport.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalRequestImport", "{0:n2}")});
            this.colTotalRequestImport.Visible = true;
            this.colTotalRequestImport.VisibleIndex = 9;
            // 
            // colTotalRequestExport
            // 
            this.colTotalRequestExport.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalRequestExport.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalRequestExport.Caption = "Y/c xuất";
            this.colTotalRequestExport.DisplayFormat.FormatString = "n2";
            this.colTotalRequestExport.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalRequestExport.FieldName = "TotalRequestExport";
            this.colTotalRequestExport.Name = "colTotalRequestExport";
            this.colTotalRequestExport.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalRequestExport", "{0:n2}")});
            this.colTotalRequestExport.Visible = true;
            this.colTotalRequestExport.VisibleIndex = 10;
            // 
            // frmInventoryByProduct
            // 
            this.AcceptButton = this.btnLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1334, 681);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmInventoryByProduct";
            this.Text = "TỒN KHO THEO SẢN PHẨM";
            this.Load += new System.EventHandler(this.frmInventoryByDate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.Button btnLoad;
        private DevExpress.XtraGrid.Columns.GridColumn ProductNewCode;
        private DevExpress.XtraGrid.Columns.GridColumn ProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn ProductName;
        private DevExpress.XtraGrid.Columns.GridColumn WarehouseName;
        private DevExpress.XtraGrid.Columns.GridColumn WarehouseType;
        private DevExpress.XtraGrid.Columns.GridColumn InventoryTotal;
        private DevExpress.XtraGrid.Columns.GridColumn InventoryReal;
        private DevExpress.XtraGrid.Columns.GridColumn NumberBorrowing;
        private DevExpress.XtraGrid.Columns.GridColumn ProductGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn SupplierName;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn productID;
        private System.Windows.Forms.Button btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn colMaker;
        public DevExpress.XtraGrid.GridControl grdData;
        public DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitName;
        public System.Windows.Forms.TextBox txtProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalRequestImport;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalRequestExport;
    }
}