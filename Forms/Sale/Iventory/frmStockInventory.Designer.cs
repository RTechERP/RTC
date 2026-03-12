
namespace BMS
{
    partial class frmStockInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockInventory));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colIsEnough = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAddStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteStock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPOStock = new System.Windows.Forms.ToolStripButton();
            this.btnEditProductGroup = new System.Windows.Forms.ToolStripButton();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.btnBorrowNCCReport = new System.Windows.Forms.ToolStripButton();
            this.btnRequestBorrow = new System.Windows.Forms.ToolStripButton();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.chkAllProduct = new System.Windows.Forms.CheckBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeData = new DevExpress.XtraTreeList.TreeList();
            this.colIDTree = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProductGroupID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroup = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsVisible = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFullName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.grdProductGroupWarehouse = new DevExpress.XtraGrid.GridControl();
            this.grvProductGroupWarehouse = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNoteMaster = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colIDMaster = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberInStoreDauKy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWarehouseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberInStoreCuoiKy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductNewCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductSaleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongTra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongMuon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConLai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityReturnNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWarehouseID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinQuantityActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityExportKeep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityKeep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityLastActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantityNotRequestExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityRequestExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityUse = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWarehouseCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.repositoryItemPictureEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdDetail = new DevExpress.XtraGrid.GridControl();
            this.grvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDateImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.col1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductGroupWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductGroupWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // colIsEnough
            // 
            this.colIsEnough.Caption = "IsEnough";
            this.colIsEnough.FieldName = "IsEnough";
            this.colIsEnough.MinWidth = 19;
            this.colIsEnough.Name = "colIsEnough";
            this.colIsEnough.Visible = true;
            this.colIsEnough.VisibleIndex = 15;
            this.colIsEnough.Width = 70;
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddStock,
            this.toolStripSeparator1,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDeleteStock,
            this.toolStripSeparator4,
            this.btnExportExcel,
            this.toolStripSeparator5,
            this.toolStripSeparator7,
            this.btnPOStock,
            this.btnEditProductGroup,
            this.btnImportExcel,
            this.btnBorrowNCCReport,
            this.btnRequestBorrow});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1349, 36);
            this.mnuMenu.TabIndex = 19;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAddStock
            // 
            this.btnAddStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStock.Image = ((System.Drawing.Image)(resources.GetObject("btnAddStock.Image")));
            this.btnAddStock.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(75, 33);
            this.btnAddStock.Tag = "frmProductRTC_SALEAddProduct";
            this.btnAddStock.Text = "Tạo thiết bị";
            this.btnAddStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Image = global::Forms.Properties.Resources.edit_16x16;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(91, 33);
            this.btnEdit.Tag = "frmInventory_Edit";
            this.btnEdit.Text = "Sửa sản phẩm";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // btnDeleteStock
            // 
            this.btnDeleteStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteStock.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteStock.Image")));
            this.btnDeleteStock.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteStock.Name = "btnDeleteStock";
            this.btnDeleteStock.Size = new System.Drawing.Size(75, 33);
            this.btnDeleteStock.Tag = "frmProductRTC_SALEAddProduct";
            this.btnDeleteStock.Text = "Xóa thiết bị";
            this.btnDeleteStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteStock.Click += new System.EventHandler(this.btnDeleteStock_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(69, 33);
            this.btnExportExcel.Tag = "";
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 33);
            this.toolStripSeparator7.Visible = false;
            // 
            // btnPOStock
            // 
            this.btnPOStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPOStock.Image = global::Forms.Properties.Resources.InsertTableOfCaptions_16x16;
            this.btnPOStock.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPOStock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPOStock.Name = "btnPOStock";
            this.btnPOStock.Size = new System.Drawing.Size(63, 33);
            this.btnPOStock.Tag = "frmInventory_POStock";
            this.btnPOStock.Text = "Nhập kho";
            this.btnPOStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPOStock.Visible = false;
            this.btnPOStock.Click += new System.EventHandler(this.btnPOStock_Click);
            // 
            // btnEditProductGroup
            // 
            this.btnEditProductGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditProductGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnEditProductGroup.Image")));
            this.btnEditProductGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditProductGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditProductGroup.Name = "btnEditProductGroup";
            this.btnEditProductGroup.Size = new System.Drawing.Size(85, 33);
            this.btnEditProductGroup.Tag = "frmInventory_EditProductGroup";
            this.btnEditProductGroup.Text = "Sửa nhóm TB";
            this.btnEditProductGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditProductGroup.Visible = false;
            this.btnEditProductGroup.Click += new System.EventHandler(this.btnEditProductGroup_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(106, 33);
            this.btnImportExcel.Tag = "frmInventory_ImportExcel";
            this.btnImportExcel.Text = "Nhập Stock Excel";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.Visible = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnBorrowNCCReport
            // 
            this.btnBorrowNCCReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrowNCCReport.Image = ((System.Drawing.Image)(resources.GetObject("btnBorrowNCCReport.Image")));
            this.btnBorrowNCCReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnBorrowNCCReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBorrowNCCReport.Name = "btnBorrowNCCReport";
            this.btnBorrowNCCReport.Size = new System.Drawing.Size(79, 33);
            this.btnBorrowNCCReport.Text = "Báo cáo NCC";
            this.btnBorrowNCCReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBorrowNCCReport.Visible = false;
            this.btnBorrowNCCReport.Click += new System.EventHandler(this.btnBorrowNCCReport_Click);
            // 
            // btnRequestBorrow
            // 
            this.btnRequestBorrow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestBorrow.Image = global::Forms.Properties.Resources.BO_Rules;
            this.btnRequestBorrow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRequestBorrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRequestBorrow.Name = "btnRequestBorrow";
            this.btnRequestBorrow.Size = new System.Drawing.Size(91, 33);
            this.btnRequestBorrow.Text = "Yêu cầu mượn";
            this.btnRequestBorrow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRequestBorrow.Visible = false;
            this.btnRequestBorrow.Click += new System.EventHandler(this.btnRequestBorrow_Click);
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(69, 39);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(418, 20);
            this.txtFilterText.TabIndex = 51;
            // 
            // chkAllProduct
            // 
            this.chkAllProduct.AutoSize = true;
            this.chkAllProduct.Location = new System.Drawing.Point(558, 41);
            this.chkAllProduct.Name = "chkAllProduct";
            this.chkAllProduct.Size = new System.Drawing.Size(57, 17);
            this.chkAllProduct.TabIndex = 55;
            this.chkAllProduct.Text = "Tất cả";
            this.chkAllProduct.UseVisualStyleBackColor = true;
            this.chkAllProduct.CheckedChanged += new System.EventHandler(this.chkAllProduct_CheckedChanged);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Location = new System.Drawing.Point(493, 38);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(59, 23);
            this.btnFind.TabIndex = 54;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Từ khóa";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl2.Location = new System.Drawing.Point(12, 67);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.treeData);
            this.splitContainerControl2.Panel1.Controls.Add(this.grdProductGroupWarehouse);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1325, 625);
            this.splitContainerControl2.SplitterPosition = 231;
            this.splitContainerControl2.TabIndex = 56;
            // 
            // treeData
            // 
            this.treeData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeData.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.treeData.ColumnPanelRowHeight = 50;
            this.treeData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colIDTree,
            this.colProductGroupID,
            this.colGroup,
            this.colIsVisible,
            this.colFullName});
            this.treeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeData.FixedLineWidth = 1;
            this.treeData.HorzScrollStep = 2;
            this.treeData.Location = new System.Drawing.Point(0, 0);
            this.treeData.Name = "treeData";
            this.treeData.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeData.OptionsBehavior.Editable = false;
            this.treeData.OptionsBehavior.PopulateServiceColumns = true;
            this.treeData.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.treeData.OptionsView.ShowIndicator = false;
            this.treeData.Size = new System.Drawing.Size(231, 487);
            this.treeData.TabIndex = 18;
            this.treeData.TreeLevelWidth = 13;
            this.treeData.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeData_FocusedNodeChanged);
            this.treeData.DoubleClick += new System.EventHandler(this.treeData_DoubleClick);
            // 
            // colIDTree
            // 
            this.colIDTree.Caption = "ID";
            this.colIDTree.FieldName = "ID";
            this.colIDTree.MinWidth = 16;
            this.colIDTree.Name = "colIDTree";
            this.colIDTree.Width = 20;
            // 
            // colProductGroupID
            // 
            this.colProductGroupID.Caption = "Mã nhóm";
            this.colProductGroupID.FieldName = "ProductGroupID";
            this.colProductGroupID.MinWidth = 16;
            this.colProductGroupID.Name = "colProductGroupID";
            this.colProductGroupID.OptionsColumn.AllowEdit = false;
            this.colProductGroupID.OptionsColumn.AllowFocus = false;
            this.colProductGroupID.Visible = true;
            this.colProductGroupID.VisibleIndex = 0;
            this.colProductGroupID.Width = 86;
            // 
            // colGroup
            // 
            this.colGroup.Caption = "Tên nhóm";
            this.colGroup.FieldName = "ProductGroupName";
            this.colGroup.Name = "colGroup";
            this.colGroup.Visible = true;
            this.colGroup.VisibleIndex = 1;
            this.colGroup.Width = 105;
            // 
            // colIsVisible
            // 
            this.colIsVisible.Caption = "IsVisible";
            this.colIsVisible.FieldName = "IsVisible";
            this.colIsVisible.Name = "colIsVisible";
            // 
            // colFullName
            // 
            this.colFullName.Caption = "NV phụ trách";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Width = 105;
            // 
            // grdProductGroupWarehouse
            // 
            this.grdProductGroupWarehouse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdProductGroupWarehouse.Location = new System.Drawing.Point(0, 487);
            this.grdProductGroupWarehouse.MainView = this.grvProductGroupWarehouse;
            this.grdProductGroupWarehouse.Name = "grdProductGroupWarehouse";
            this.grdProductGroupWarehouse.Size = new System.Drawing.Size(231, 138);
            this.grdProductGroupWarehouse.TabIndex = 21;
            this.grdProductGroupWarehouse.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProductGroupWarehouse});
            // 
            // grvProductGroupWarehouse
            // 
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvProductGroupWarehouse.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvProductGroupWarehouse.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvProductGroupWarehouse.Appearance.Row.Options.UseFont = true;
            this.grvProductGroupWarehouse.Appearance.Row.Options.UseTextOptions = true;
            this.grvProductGroupWarehouse.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvProductGroupWarehouse.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvProductGroupWarehouse.ColumnPanelRowHeight = 40;
            this.grvProductGroupWarehouse.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn7});
            this.grvProductGroupWarehouse.GridControl = this.grdProductGroupWarehouse;
            this.grvProductGroupWarehouse.Name = "grvProductGroupWarehouse";
            this.grvProductGroupWarehouse.OptionsBehavior.Editable = false;
            this.grvProductGroupWarehouse.OptionsBehavior.ReadOnly = true;
            this.grvProductGroupWarehouse.OptionsView.RowAutoHeight = true;
            this.grvProductGroupWarehouse.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Kho";
            this.gridColumn1.FieldName = "WarehouseCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 57;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "NV phụ trách";
            this.gridColumn7.FieldName = "FullName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 118;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grdMaster);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl1.Size = new System.Drawing.Size(1084, 625);
            this.splitContainerControl1.SplitterPosition = 772;
            this.splitContainerControl1.TabIndex = 26;
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdMaster.Location = new System.Drawing.Point(0, 0);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4,
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageEdit1,
            this.repositoryItemPictureEdit2});
            this.grdMaster.Size = new System.Drawing.Size(1084, 625);
            this.grdMaster.TabIndex = 25;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            // 
            // grvMaster
            // 
            this.grvMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvMaster.Appearance.Row.Options.UseFont = true;
            this.grvMaster.Appearance.Row.Options.UseTextOptions = true;
            this.grvMaster.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.AutoFillColumn = this.colNoteMaster;
            this.grvMaster.ColumnPanelRowHeight = 50;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDMaster,
            this.colGroupNo,
            this.colProductGroupName,
            this.gridColumn2,
            this.gridColumn3,
            this.colMaker,
            this.gridColumn4,
            this.colNumberInStoreDauKy,
            this.colWarehouseName,
            this.colProjectTypeName,
            this.colNumberInStoreCuoiKy,
            this.colMinQuantity,
            this.colAddress,
            this.colNoteMaster,
            this.colLocationName,
            this.colSupplierName,
            this.colProductNewCode,
            this.colDetail,
            this.colProductSaleID,
            this.colTongTra,
            this.colTongMuon,
            this.colConLai,
            this.colNameNCC,
            this.colDeliver,
            this.colTotalQuantityReturnNCC,
            this.colIsEnough,
            this.colWarehouseID,
            this.colMinQuantityActual,
            this.colTotalQuantityExportKeep,
            this.colTotalQuantityKeep,
            this.colTotalQuantityLastActual,
            this.colTotalQuantityNotRequestExport,
            this.colQuantityRequestExport,
            this.colQuantityUse,
            this.colEmployeeStock,
            this.colTotalQuantity,
            this.colWarehouseCode});
            this.grvMaster.FixedLineWidth = 1;
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colIsEnough;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.LightGreen;
            formatConditionRuleValue1.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = false;
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.grvMaster.FormatRules.Add(gridFormatRule1);
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.GroupCount = 2;
            this.grvMaster.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "({0:n0})")});
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsCustomization.AllowRowSizing = true;
            this.grvMaster.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvMaster.OptionsSelection.MultiSelect = true;
            this.grvMaster.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvMaster.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.grvMaster.OptionsView.ColumnAutoWidth = false;
            this.grvMaster.OptionsView.RowAutoHeight = true;
            this.grvMaster.OptionsView.ShowAutoFilterRow = true;
            this.grvMaster.OptionsView.ShowFooter = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colWarehouseName, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colProjectTypeName, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNumberInStoreDauKy, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvMaster.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvMaster_RowStyle);
            this.grvMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMaster_FocusedRowChanged);
            this.grvMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvMaster_KeyDown);
            this.grvMaster.DoubleClick += new System.EventHandler(this.grvMaster_DoubleClick);
            // 
            // colNoteMaster
            // 
            this.colNoteMaster.Caption = "Ghi chú";
            this.colNoteMaster.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colNoteMaster.FieldName = "Note";
            this.colNoteMaster.Name = "colNoteMaster";
            this.colNoteMaster.Visible = true;
            this.colNoteMaster.VisibleIndex = 13;
            this.colNoteMaster.Width = 212;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colIDMaster
            // 
            this.colIDMaster.Caption = "ID";
            this.colIDMaster.FieldName = "ID";
            this.colIDMaster.Name = "colIDMaster";
            this.colIDMaster.Width = 56;
            // 
            // colGroupNo
            // 
            this.colGroupNo.Caption = "Nhóm";
            this.colGroupNo.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colGroupNo.FieldName = "ProductGroupNo";
            this.colGroupNo.Name = "colGroupNo";
            this.colGroupNo.Width = 56;
            // 
            // colProductGroupName
            // 
            this.colProductGroupName.Caption = "Tên nhóm";
            this.colProductGroupName.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colProductGroupName.FieldName = "ProductGroupName";
            this.colProductGroupName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colProductGroupName.Name = "colProductGroupName";
            this.colProductGroupName.Visible = true;
            this.colProductGroupName.VisibleIndex = 1;
            this.colProductGroupName.Width = 125;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã sản phẩm";
            this.gridColumn2.ColumnEdit = this.repositoryItemMemoEdit4;
            this.gridColumn2.FieldName = "ProductCode";
            this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 144;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên sản phẩm";
            this.gridColumn3.ColumnEdit = this.repositoryItemMemoEdit4;
            this.gridColumn3.FieldName = "ProductName";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 257;
            // 
            // colMaker
            // 
            this.colMaker.Caption = "Hãng";
            this.colMaker.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colMaker.FieldName = "Maker";
            this.colMaker.Name = "colMaker";
            this.colMaker.Visible = true;
            this.colMaker.VisibleIndex = 5;
            this.colMaker.Width = 99;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ĐVT";
            this.gridColumn4.FieldName = "Unit";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 96;
            // 
            // colNumberInStoreDauKy
            // 
            this.colNumberInStoreDauKy.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberInStoreDauKy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNumberInStoreDauKy.Caption = "Tồn DK";
            this.colNumberInStoreDauKy.FieldName = "TotalQuantityFirst";
            this.colNumberInStoreDauKy.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumberInStoreDauKy.Name = "colNumberInStoreDauKy";
            this.colNumberInStoreDauKy.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantityFirst", "{0:0.##}")});
            this.colNumberInStoreDauKy.Width = 116;
            // 
            // colWarehouseName
            // 
            this.colWarehouseName.Caption = "Kho";
            this.colWarehouseName.FieldName = "WarehouseName";
            this.colWarehouseName.Name = "colWarehouseName";
            this.colWarehouseName.Visible = true;
            this.colWarehouseName.VisibleIndex = 15;
            this.colWarehouseName.Width = 110;
            // 
            // colProjectTypeName
            // 
            this.colProjectTypeName.Caption = "Loại dự án";
            this.colProjectTypeName.FieldName = "ProjectTypeName";
            this.colProjectTypeName.Name = "colProjectTypeName";
            this.colProjectTypeName.Visible = true;
            this.colProjectTypeName.VisibleIndex = 16;
            this.colProjectTypeName.Width = 95;
            // 
            // colNumberInStoreCuoiKy
            // 
            this.colNumberInStoreCuoiKy.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberInStoreCuoiKy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNumberInStoreCuoiKy.Caption = "Tồn CK";
            this.colNumberInStoreCuoiKy.FieldName = "TotalQuantityLast";
            this.colNumberInStoreCuoiKy.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumberInStoreCuoiKy.Name = "colNumberInStoreCuoiKy";
            this.colNumberInStoreCuoiKy.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantityLast", "{0:0.##}")});
            this.colNumberInStoreCuoiKy.Visible = true;
            this.colNumberInStoreCuoiKy.VisibleIndex = 8;
            this.colNumberInStoreCuoiKy.Width = 109;
            // 
            // colMinQuantity
            // 
            this.colMinQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colMinQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMinQuantity.Caption = "Tồn tối thiểu Y/c";
            this.colMinQuantity.FieldName = "Quantity";
            this.colMinQuantity.Name = "colMinQuantity";
            this.colMinQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:0.##}")});
            this.colMinQuantity.Visible = true;
            this.colMinQuantity.VisibleIndex = 9;
            this.colMinQuantity.Width = 103;
            // 
            // colAddress
            // 
            this.colAddress.Caption = "Vị trí (Hộp)";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colAddress.FieldName = "AddressBox";
            this.colAddress.Name = "colAddress";
            this.colAddress.Width = 43;
            // 
            // colLocationName
            // 
            this.colLocationName.Caption = "Vị trí";
            this.colLocationName.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colLocationName.FieldName = "AddressBox";
            this.colLocationName.Name = "colLocationName";
            this.colLocationName.Visible = true;
            this.colLocationName.VisibleIndex = 12;
            this.colLocationName.Width = 187;
            // 
            // colSupplierName
            // 
            this.colSupplierName.Caption = "Nhà cung cấp";
            this.colSupplierName.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colSupplierName.FieldName = "SupplierName";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.Width = 79;
            // 
            // colProductNewCode
            // 
            this.colProductNewCode.Caption = "Mã nội bộ";
            this.colProductNewCode.FieldName = "ProductNewCode";
            this.colProductNewCode.Name = "colProductNewCode";
            this.colProductNewCode.Visible = true;
            this.colProductNewCode.VisibleIndex = 11;
            this.colProductNewCode.Width = 122;
            // 
            // colDetail
            // 
            this.colDetail.Caption = "Chi tiết nhập";
            this.colDetail.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colDetail.FieldName = "Detail";
            this.colDetail.Name = "colDetail";
            this.colDetail.Width = 211;
            // 
            // colProductSaleID
            // 
            this.colProductSaleID.FieldName = "ProductSaleID";
            this.colProductSaleID.Name = "colProductSaleID";
            // 
            // colTongTra
            // 
            this.colTongTra.AppearanceCell.Options.UseTextOptions = true;
            this.colTongTra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTongTra.Caption = "Tổng trả";
            this.colTongTra.FieldName = "ImportPT";
            this.colTongTra.Name = "colTongTra";
            this.colTongTra.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ImportPT", "{0:0.##}")});
            this.colTongTra.Width = 93;
            // 
            // colTongMuon
            // 
            this.colTongMuon.AppearanceCell.Options.UseTextOptions = true;
            this.colTongMuon.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTongMuon.Caption = "Tổng Mượn";
            this.colTongMuon.FieldName = "ExportPM";
            this.colTongMuon.Name = "colTongMuon";
            this.colTongMuon.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ExportPM", "{0:0.##}")});
            this.colTongMuon.Width = 107;
            // 
            // colConLai
            // 
            this.colConLai.AppearanceCell.Options.UseTextOptions = true;
            this.colConLai.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colConLai.Caption = "Đang mượn";
            this.colConLai.FieldName = "StillBorrowed";
            this.colConLai.Name = "colConLai";
            this.colConLai.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "StillBorrowed", "{0:0.##}")});
            this.colConLai.Width = 101;
            // 
            // colNameNCC
            // 
            this.colNameNCC.Caption = "NCC";
            this.colNameNCC.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colNameNCC.FieldName = "NameNCC";
            this.colNameNCC.Name = "colNameNCC";
            this.colNameNCC.Visible = true;
            this.colNameNCC.VisibleIndex = 4;
            this.colNameNCC.Width = 150;
            // 
            // colDeliver
            // 
            this.colDeliver.Caption = "Người nhập";
            this.colDeliver.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colDeliver.FieldName = "Deliver";
            this.colDeliver.Name = "colDeliver";
            this.colDeliver.Width = 174;
            // 
            // colTotalQuantityReturnNCC
            // 
            this.colTotalQuantityReturnNCC.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuantityReturnNCC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalQuantityReturnNCC.Caption = "SL Phải trả NCC";
            this.colTotalQuantityReturnNCC.FieldName = "TotalQuantityReturnNCC";
            this.colTotalQuantityReturnNCC.Name = "colTotalQuantityReturnNCC";
            this.colTotalQuantityReturnNCC.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantityReturnNCC", "{0:0.##}")});
            this.colTotalQuantityReturnNCC.Width = 113;
            // 
            // colWarehouseID
            // 
            this.colWarehouseID.FieldName = "WarehouseID";
            this.colWarehouseID.Name = "colWarehouseID";
            // 
            // colMinQuantityActual
            // 
            this.colMinQuantityActual.AppearanceCell.Options.UseTextOptions = true;
            this.colMinQuantityActual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMinQuantityActual.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMinQuantityActual.Caption = "Tồn tối thiểu thực tế";
            this.colMinQuantityActual.FieldName = "MinQuantityActual";
            this.colMinQuantityActual.Name = "colMinQuantityActual";
            this.colMinQuantityActual.Width = 123;
            // 
            // colTotalQuantityExportKeep
            // 
            this.colTotalQuantityExportKeep.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuantityExportKeep.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalQuantityExportKeep.Caption = "Xuất kho giữ";
            this.colTotalQuantityExportKeep.DisplayFormat.FormatString = "n2";
            this.colTotalQuantityExportKeep.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalQuantityExportKeep.FieldName = "TotalQuantityExportKeep";
            this.colTotalQuantityExportKeep.Name = "colTotalQuantityExportKeep";
            this.colTotalQuantityExportKeep.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantityExportKeep", "{0:n2}")});
            this.colTotalQuantityExportKeep.Width = 120;
            // 
            // colTotalQuantityKeep
            // 
            this.colTotalQuantityKeep.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuantityKeep.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalQuantityKeep.Caption = "SL giữ";
            this.colTotalQuantityKeep.DisplayFormat.FormatString = "n2";
            this.colTotalQuantityKeep.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalQuantityKeep.FieldName = "TotalQuantityKeep";
            this.colTotalQuantityKeep.Name = "colTotalQuantityKeep";
            this.colTotalQuantityKeep.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantityKeep", "{0:n2}")});
            this.colTotalQuantityKeep.Width = 94;
            // 
            // colTotalQuantityLastActual
            // 
            this.colTotalQuantityLastActual.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuantityLastActual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalQuantityLastActual.Caption = "SL tồn thực tế";
            this.colTotalQuantityLastActual.DisplayFormat.FormatString = "n2";
            this.colTotalQuantityLastActual.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalQuantityLastActual.FieldName = "TotalQuantityLastActual";
            this.colTotalQuantityLastActual.Name = "colTotalQuantityLastActual";
            this.colTotalQuantityLastActual.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalQuantityLastActual", "{0:n2}")});
            this.colTotalQuantityLastActual.Width = 135;
            // 
            // colTotalQuantityNotRequestExport
            // 
            this.colTotalQuantityNotRequestExport.Caption = "Tồn thực tế (Ko tính Yc xuất)";
            this.colTotalQuantityNotRequestExport.FieldName = "TotalQuantityNotRequestExport";
            this.colTotalQuantityNotRequestExport.Name = "colTotalQuantityNotRequestExport";
            this.colTotalQuantityNotRequestExport.Width = 145;
            // 
            // colQuantityRequestExport
            // 
            this.colQuantityRequestExport.Caption = "SL Yêu cầu xuất";
            this.colQuantityRequestExport.FieldName = "QuantityRequestExport";
            this.colQuantityRequestExport.Name = "colQuantityRequestExport";
            this.colQuantityRequestExport.Width = 143;
            // 
            // colQuantityUse
            // 
            this.colQuantityUse.Caption = "Tồn sử dụng";
            this.colQuantityUse.FieldName = "QuantityUse";
            this.colQuantityUse.Name = "colQuantityUse";
            this.colQuantityUse.Width = 117;
            // 
            // colEmployeeStock
            // 
            this.colEmployeeStock.Caption = "Người yêu cầu";
            this.colEmployeeStock.FieldName = "FullName";
            this.colEmployeeStock.Name = "colEmployeeStock";
            this.colEmployeeStock.Visible = true;
            this.colEmployeeStock.VisibleIndex = 10;
            this.colEmployeeStock.Width = 175;
            // 
            // colTotalQuantity
            // 
            this.colTotalQuantity.Caption = "SL yêu cầu";
            this.colTotalQuantity.FieldName = "TotalQuantity";
            this.colTotalQuantity.Name = "colTotalQuantity";
            this.colTotalQuantity.Visible = true;
            this.colTotalQuantity.VisibleIndex = 7;
            // 
            // colWarehouseCode
            // 
            this.colWarehouseCode.Caption = "Kho";
            this.colWarehouseCode.FieldName = "WarehouseCode";
            this.colWarehouseCode.Name = "colWarehouseCode";
            this.colWarehouseCode.Visible = true;
            this.colWarehouseCode.VisibleIndex = 14;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            this.repositoryItemImageEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
            this.repositoryItemImageEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // repositoryItemPictureEdit2
            // 
            this.repositoryItemPictureEdit2.Name = "repositoryItemPictureEdit2";
            this.repositoryItemPictureEdit2.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.grdDetail);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(0, 0);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "CHI TIẾT NHẬP";
            // 
            // grdDetail
            // 
            this.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetail.Location = new System.Drawing.Point(0, 22);
            this.grdDetail.MainView = this.grvDetail;
            this.grdDetail.Name = "grdDetail";
            this.grdDetail.Size = new System.Drawing.Size(0, 0);
            this.grdDetail.TabIndex = 2;
            this.grdDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDetail,
            this.bandedGridView1});
            // 
            // grvDetail
            // 
            this.grvDetail.ColumnPanelRowHeight = 45;
            this.grvDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDateImport,
            this.colCode,
            this.colQty});
            this.grvDetail.GridControl = this.grdDetail;
            this.grvDetail.Name = "grvDetail";
            this.grvDetail.OptionsBehavior.Editable = false;
            this.grvDetail.OptionsView.ShowFooter = true;
            this.grvDetail.OptionsView.ShowGroupPanel = false;
            this.grvDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvDetail_KeyDown);
            // 
            // colDateImport
            // 
            this.colDateImport.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDateImport.AppearanceCell.Options.UseFont = true;
            this.colDateImport.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDateImport.AppearanceHeader.Options.UseFont = true;
            this.colDateImport.AppearanceHeader.Options.UseForeColor = true;
            this.colDateImport.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateImport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateImport.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateImport.Caption = "Ngày nhập";
            this.colDateImport.FieldName = "CreatDate";
            this.colDateImport.Name = "colDateImport";
            this.colDateImport.Visible = true;
            this.colDateImport.VisibleIndex = 0;
            this.colDateImport.Width = 59;
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Dự án";
            this.colCode.FieldName = "ProjectCode";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 77;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQty.AppearanceCell.Options.UseFont = true;
            this.colQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQty.AppearanceHeader.Options.UseFont = true;
            this.colQty.AppearanceHeader.Options.UseForeColor = true;
            this.colQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.Caption = "Số lượng";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:0.##}")});
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 2;
            this.colQty.Width = 43;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.col1,
            this.bandedGridColumn2,
            this.bandedGridColumn3});
            this.bandedGridView1.GridControl = this.grdDetail;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.RowCount = 2;
            this.gridBand1.VisibleIndex = 0;
            // 
            // col1
            // 
            this.col1.Caption = "Cột 1";
            this.col1.Name = "col1";
            this.col1.Visible = true;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "bandedGridColumn2";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "bandedGridColumn3";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Visible = true;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.LightGreen;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(1248, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 57;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1283, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 58;
            this.label13.Text = "Không đủ";
            // 
            // frmStockInventory
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 704);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.splitContainerControl2);
            this.Controls.Add(this.txtFilterText);
            this.Controls.Add(this.chkAllProduct);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmStockInventory";
            this.Text = "TỒN KHO STOCK";
            this.Load += new System.EventHandler(this.frmStockInventory_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductGroupWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProductGroupWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnEditProductGroup;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnPOStock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnBorrowNCCReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnRequestBorrow;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.CheckBox chkAllProduct;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraTreeList.TreeList treeData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIDTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProductGroupID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroup;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsVisible;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFullName;
        private DevExpress.XtraGrid.GridControl grdProductGroupWarehouse;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProductGroupWarehouse;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteMaster;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colIDMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupNo;
        private DevExpress.XtraGrid.Columns.GridColumn colProductGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colMaker;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberInStoreDauKy;
        private DevExpress.XtraGrid.Columns.GridColumn colWarehouseName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberInStoreCuoiKy;
        private DevExpress.XtraGrid.Columns.GridColumn colMinQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductNewCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colProductSaleID;
        private DevExpress.XtraGrid.Columns.GridColumn colTongTra;
        private DevExpress.XtraGrid.Columns.GridColumn colTongMuon;
        private DevExpress.XtraGrid.Columns.GridColumn colConLai;
        private DevExpress.XtraGrid.Columns.GridColumn colNameNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliver;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityReturnNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colIsEnough;
        private DevExpress.XtraGrid.Columns.GridColumn colWarehouseID;
        private DevExpress.XtraGrid.Columns.GridColumn colMinQuantityActual;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityExportKeep;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityKeep;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityLastActual;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantityNotRequestExport;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityRequestExport;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityUse;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colDateImport;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeStock;
        private System.Windows.Forms.ToolStripButton btnAddStock;
        private System.Windows.Forms.ToolStripButton btnDeleteStock;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWarehouseCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}