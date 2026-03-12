
namespace BMS
{
    partial class frmInventoryByDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventoryByDate));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chiTiếtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyValues1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyValues2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityLast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddressBox = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductGroupID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductNewCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyValues3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1242, 41);
            this.mnuMenu.TabIndex = 19;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(91, 38);
            this.btnExportExcel.Tag = "";
            this.btnExportExcel.Text = "Xuất phiếu";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1242, 42);
            this.panel1.TabIndex = 21;
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(337, 6);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(98, 27);
            this.btnFind.TabIndex = 47;
            this.btnFind.Text = "LoadData";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Đến ngày";
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(117, 7);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 26);
            this.dtpDate.TabIndex = 0;
            // 
            // grdData
            // 
            this.grdData.ContextMenuStrip = this.contextMenuStrip1;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 83);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1242, 438);
            this.grdData.TabIndex = 22;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chiTiếtToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            // 
            // chiTiếtToolStripMenuItem
            // 
            this.chiTiếtToolStripMenuItem.Name = "chiTiếtToolStripMenuItem";
            this.chiTiếtToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.chiTiếtToolStripMenuItem.Text = "Chi tiết";
            this.chiTiếtToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtToolStripMenuItem_Click);
            // 
            // grvData
            // 
            this.grvData.ColumnPanelRowHeight = 45;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductID,
            this.colCreatDate,
            this.colQtyImport,
            this.colQtyValues1,
            this.colQtyValues2,
            this.colTotalQuantityLast,
            this.colExport,
            this.colProductCode,
            this.colProductName,
            this.colMaker,
            this.colUnit,
            this.colAddressBox,
            this.colProductGroupID,
            this.colItemType,
            this.colSupplierName,
            this.colProductNewCode,
            this.colLocationName,
            this.colQtyValues3});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            // 
            // colProductID
            // 
            this.colProductID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colProductID.AppearanceCell.Options.UseFont = true;
            this.colProductID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colProductID.AppearanceHeader.Options.UseFont = true;
            this.colProductID.AppearanceHeader.Options.UseForeColor = true;
            this.colProductID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductID.Caption = "ProductID";
            this.colProductID.FieldName = "ProductID";
            this.colProductID.Name = "colProductID";
            // 
            // colCreatDate
            // 
            this.colCreatDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreatDate.AppearanceCell.Options.UseFont = true;
            this.colCreatDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colCreatDate.AppearanceHeader.Options.UseFont = true;
            this.colCreatDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatDate.Caption = "Ngày";
            this.colCreatDate.FieldName = "CreatDate";
            this.colCreatDate.Name = "colCreatDate";
            this.colCreatDate.Visible = true;
            this.colCreatDate.VisibleIndex = 0;
            this.colCreatDate.Width = 216;
            // 
            // colQtyImport
            // 
            this.colQtyImport.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colQtyImport.AppearanceCell.Options.UseFont = true;
            this.colQtyImport.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colQtyImport.AppearanceHeader.Options.UseFont = true;
            this.colQtyImport.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyImport.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyImport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyImport.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyImport.Caption = "SL nhập";
            this.colQtyImport.FieldName = "QtyImport";
            this.colQtyImport.Name = "colQtyImport";
            this.colQtyImport.Visible = true;
            this.colQtyImport.VisibleIndex = 4;
            this.colQtyImport.Width = 94;
            // 
            // colQtyValues1
            // 
            this.colQtyValues1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colQtyValues1.AppearanceCell.Options.UseFont = true;
            this.colQtyValues1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colQtyValues1.AppearanceHeader.Options.UseFont = true;
            this.colQtyValues1.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyValues1.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyValues1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyValues1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyValues1.Caption = "Tồn kho";
            this.colQtyValues1.FieldName = "QtyValues1";
            this.colQtyValues1.Name = "colQtyValues1";
            this.colQtyValues1.Width = 97;
            // 
            // colQtyValues2
            // 
            this.colQtyValues2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colQtyValues2.AppearanceCell.Options.UseFont = true;
            this.colQtyValues2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colQtyValues2.AppearanceHeader.Options.UseFont = true;
            this.colQtyValues2.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyValues2.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyValues2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyValues2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyValues2.Caption = "Tồn 2";
            this.colQtyValues2.FieldName = "QtyValues2";
            this.colQtyValues2.Name = "colQtyValues2";
            this.colQtyValues2.Width = 53;
            // 
            // colTotalQuantityLast
            // 
            this.colTotalQuantityLast.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colTotalQuantityLast.AppearanceCell.Options.UseFont = true;
            this.colTotalQuantityLast.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colTotalQuantityLast.AppearanceHeader.Options.UseFont = true;
            this.colTotalQuantityLast.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalQuantityLast.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalQuantityLast.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalQuantityLast.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalQuantityLast.Caption = "Tồn cuối";
            this.colTotalQuantityLast.FieldName = "TotalQuantityLast";
            this.colTotalQuantityLast.Name = "colTotalQuantityLast";
            this.colTotalQuantityLast.Width = 41;
            // 
            // colExport
            // 
            this.colExport.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colExport.AppearanceCell.Options.UseFont = true;
            this.colExport.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colExport.AppearanceHeader.Options.UseFont = true;
            this.colExport.AppearanceHeader.Options.UseForeColor = true;
            this.colExport.AppearanceHeader.Options.UseTextOptions = true;
            this.colExport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExport.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExport.Caption = "Xuất";
            this.colExport.FieldName = "Export";
            this.colExport.Name = "colExport";
            this.colExport.Width = 39;
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colProductCode.AppearanceCell.Options.UseFont = true;
            this.colProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colProductCode.AppearanceHeader.Options.UseFont = true;
            this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã sản phẩm";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 2;
            this.colProductCode.Width = 149;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colProductName.AppearanceCell.Options.UseFont = true;
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colProductName.AppearanceHeader.Options.UseFont = true;
            this.colProductName.AppearanceHeader.Options.UseForeColor = true;
            this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.Caption = "Tên sản phẩm";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 3;
            this.colProductName.Width = 419;
            // 
            // colMaker
            // 
            this.colMaker.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colMaker.AppearanceCell.Options.UseFont = true;
            this.colMaker.AppearanceCell.Options.UseTextOptions = true;
            this.colMaker.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colMaker.AppearanceHeader.Options.UseFont = true;
            this.colMaker.AppearanceHeader.Options.UseForeColor = true;
            this.colMaker.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaker.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaker.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.Caption = "Hãng ";
            this.colMaker.FieldName = "Maker";
            this.colMaker.Name = "colMaker";
            this.colMaker.Width = 85;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colUnit.AppearanceCell.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Options.UseForeColor = true;
            this.colUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.Caption = "Đơn vị tính";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Width = 60;
            // 
            // colAddressBox
            // 
            this.colAddressBox.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colAddressBox.AppearanceCell.Options.UseFont = true;
            this.colAddressBox.AppearanceCell.Options.UseTextOptions = true;
            this.colAddressBox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressBox.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colAddressBox.AppearanceHeader.Options.UseFont = true;
            this.colAddressBox.AppearanceHeader.Options.UseForeColor = true;
            this.colAddressBox.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddressBox.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddressBox.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddressBox.Caption = "Vị trí thùng";
            this.colAddressBox.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddressBox.FieldName = "AddressBox";
            this.colAddressBox.Name = "colAddressBox";
            this.colAddressBox.Width = 92;
            // 
            // colProductGroupID
            // 
            this.colProductGroupID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colProductGroupID.AppearanceCell.Options.UseFont = true;
            this.colProductGroupID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colProductGroupID.AppearanceHeader.Options.UseFont = true;
            this.colProductGroupID.AppearanceHeader.Options.UseForeColor = true;
            this.colProductGroupID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductGroupID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductGroupID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductGroupID.Caption = "ProductGroupID";
            this.colProductGroupID.FieldName = "ProductGroupID";
            this.colProductGroupID.Name = "colProductGroupID";
            // 
            // colItemType
            // 
            this.colItemType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colItemType.AppearanceCell.Options.UseFont = true;
            this.colItemType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colItemType.AppearanceHeader.Options.UseFont = true;
            this.colItemType.AppearanceHeader.Options.UseForeColor = true;
            this.colItemType.AppearanceHeader.Options.UseTextOptions = true;
            this.colItemType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colItemType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colItemType.Caption = "ItemType";
            this.colItemType.FieldName = "ItemType";
            this.colItemType.Name = "colItemType";
            // 
            // colSupplierName
            // 
            this.colSupplierName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colSupplierName.AppearanceCell.Options.UseFont = true;
            this.colSupplierName.AppearanceCell.Options.UseTextOptions = true;
            this.colSupplierName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSupplierName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colSupplierName.AppearanceHeader.Options.UseFont = true;
            this.colSupplierName.AppearanceHeader.Options.UseForeColor = true;
            this.colSupplierName.AppearanceHeader.Options.UseTextOptions = true;
            this.colSupplierName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSupplierName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSupplierName.Caption = "Nhà cung cấp";
            this.colSupplierName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSupplierName.FieldName = "Suplier";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.Width = 146;
            // 
            // colProductNewCode
            // 
            this.colProductNewCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colProductNewCode.AppearanceCell.Options.UseFont = true;
            this.colProductNewCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colProductNewCode.AppearanceHeader.Options.UseFont = true;
            this.colProductNewCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductNewCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductNewCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductNewCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductNewCode.Caption = "Mã nội bộ";
            this.colProductNewCode.FieldName = "ProductNewCode";
            this.colProductNewCode.Name = "colProductNewCode";
            this.colProductNewCode.Visible = true;
            this.colProductNewCode.VisibleIndex = 1;
            this.colProductNewCode.Width = 149;
            // 
            // colLocationName
            // 
            this.colLocationName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colLocationName.AppearanceCell.Options.UseFont = true;
            this.colLocationName.AppearanceCell.Options.UseTextOptions = true;
            this.colLocationName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLocationName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colLocationName.AppearanceHeader.Options.UseFont = true;
            this.colLocationName.AppearanceHeader.Options.UseForeColor = true;
            this.colLocationName.AppearanceHeader.Options.UseTextOptions = true;
            this.colLocationName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLocationName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLocationName.Caption = "Vị trí";
            this.colLocationName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colLocationName.FieldName = "LocationName";
            this.colLocationName.Name = "colLocationName";
            this.colLocationName.Width = 80;
            // 
            // colQtyValues3
            // 
            this.colQtyValues3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQtyValues3.AppearanceCell.Options.UseFont = true;
            this.colQtyValues3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQtyValues3.AppearanceHeader.Options.UseFont = true;
            this.colQtyValues3.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyValues3.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyValues3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyValues3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyValues3.Caption = "Số lượng tồn";
            this.colQtyValues3.FieldName = "QtyValues3";
            this.colQtyValues3.Name = "colQtyValues3";
            this.colQtyValues3.Visible = true;
            this.colQtyValues3.VisibleIndex = 5;
            this.colQtyValues3.Width = 93;
            // 
            // frmInventoryByDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 521);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmInventoryByDate";
            this.Text = "BÁO CÁO TỒN KHO";
            this.Load += new System.EventHandler(this.frmInventoryByDate_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatDate;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyImport;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyValues1;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyValues2;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityLast;
        private DevExpress.XtraGrid.Columns.GridColumn colExport;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colMaker;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressBox;
        private DevExpress.XtraGrid.Columns.GridColumn colProductGroupID;
        private DevExpress.XtraGrid.Columns.GridColumn colItemType;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductNewCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyValues3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtToolStripMenuItem;
    }
}