
namespace Forms
{
    partial class frmBorrowReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBorrowReport));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnResetData = new System.Windows.Forms.ToolStripButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductGroupNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit11 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colSerial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colMaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit10 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberInStore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerialNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colPartNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.colLocationImg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCodeRTC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitCountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBorrowCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBorrowing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnSupplierID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberReal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInventory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSLKiemKe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationImgPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBorrowCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxDateBorrow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.riAnhSp = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAnhSp)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExcel,
            this.toolStripSeparator1,
            this.btnResetData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1475, 38);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = false;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.RightToLeftAutoMirrorImage = true;
            this.btnExcel.Size = new System.Drawing.Size(70, 35);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // btnResetData
            // 
            this.btnResetData.AutoSize = false;
            this.btnResetData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnResetData.Image = global::Forms.Properties.Resources.refresh2_16x16;
            this.btnResetData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResetData.Name = "btnResetData";
            this.btnResetData.Size = new System.Drawing.Size(70, 35);
            this.btnResetData.Text = "Reset";
            this.btnResetData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnResetData.Click += new System.EventHandler(this.btnResetData_Click);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.Location = new System.Drawing.Point(0, 38);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4,
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageEdit1,
            this.riAnhSp,
            this.repositoryItemMemoEdit5,
            this.repositoryItemMemoEdit6,
            this.repositoryItemMemoEdit7,
            this.repositoryItemMemoEdit8,
            this.repositoryItemMemoEdit9,
            this.repositoryItemMemoEdit10,
            this.repositoryItemMemoEdit11});
            this.grdData.Size = new System.Drawing.Size(1475, 577);
            this.grdData.TabIndex = 20;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 45;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colProductGroupNo,
            this.colProductGroupName,
            this.colProductCode,
            this.colProductName,
            this.colSerial,
            this.colMaker,
            this.colNumber,
            this.colNumberInStore,
            this.colAddress,
            this.colNote,
            this.colStatus,
            this.colCreateDate,
            this.colSerialNumber,
            this.colPartNumber,
            this.colLocation,
            this.colLocationImg,
            this.colProductCodeRTC,
            this.colUnitCountName,
            this.colBorrowCustomer,
            this.colBorrowing,
            this.colNumberExport,
            this.colReturnSupplierID,
            this.colImport,
            this.colExport,
            this.colNumberReal,
            this.colInventory,
            this.colSLKiemKe,
            this.colLocationImgPath,
            this.colCreatedBy,
            this.colBorrowCount,
            this.colMaxDateBorrow,
            this.colTotalDay});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.HorzScrollStep = 5;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsCustomization.AllowRowSizing = true;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsFind.ShowCloseButton = false;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            // 
            // colID
            // 
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 15;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.Width = 56;
            // 
            // colProductGroupNo
            // 
            this.colProductGroupNo.AppearanceCell.Options.UseTextOptions = true;
            this.colProductGroupNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductGroupNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductGroupNo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductGroupNo.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductGroupNo.AppearanceHeader.Options.UseForeColor = true;
            this.colProductGroupNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductGroupNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductGroupNo.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductGroupNo.Caption = "Mã Nhóm";
            this.colProductGroupNo.FieldName = "ProductGroupNo";
            this.colProductGroupNo.MinWidth = 15;
            this.colProductGroupNo.Name = "colProductGroupNo";
            this.colProductGroupNo.OptionsColumn.ReadOnly = true;
            this.colProductGroupNo.Visible = true;
            this.colProductGroupNo.VisibleIndex = 12;
            this.colProductGroupNo.Width = 56;
            // 
            // colProductGroupName
            // 
            this.colProductGroupName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductGroupName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductGroupName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProductGroupName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductGroupName.AppearanceHeader.Options.UseFont = true;
            this.colProductGroupName.AppearanceHeader.Options.UseForeColor = true;
            this.colProductGroupName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductGroupName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductGroupName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductGroupName.Caption = "Tên nhóm";
            this.colProductGroupName.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colProductGroupName.FieldName = "ProductGroupName";
            this.colProductGroupName.MinWidth = 15;
            this.colProductGroupName.Name = "colProductGroupName";
            this.colProductGroupName.OptionsColumn.AllowEdit = false;
            this.colProductGroupName.OptionsColumn.ReadOnly = true;
            this.colProductGroupName.Visible = true;
            this.colProductGroupName.VisibleIndex = 11;
            this.colProductGroupName.Width = 63;
            // 
            // repositoryItemMemoEdit5
            // 
            this.repositoryItemMemoEdit5.Name = "repositoryItemMemoEdit5";
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProductCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductCode.AppearanceHeader.Options.UseFont = true;
            this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã sản phẩm";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit11;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.MinWidth = 15;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowEdit = false;
            this.colProductCode.OptionsColumn.ReadOnly = true;
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 4;
            this.colProductCode.Width = 191;
            // 
            // repositoryItemMemoEdit11
            // 
            this.repositoryItemMemoEdit11.Name = "repositoryItemMemoEdit11";
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProductName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductName.AppearanceHeader.Options.UseFont = true;
            this.colProductName.AppearanceHeader.Options.UseForeColor = true;
            this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.Caption = "Tên";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit6;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.MinWidth = 15;
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 5;
            this.colProductName.Width = 244;
            // 
            // repositoryItemMemoEdit6
            // 
            this.repositoryItemMemoEdit6.Name = "repositoryItemMemoEdit6";
            // 
            // colSerial
            // 
            this.colSerial.AppearanceCell.Options.UseTextOptions = true;
            this.colSerial.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colSerial.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSerial.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSerial.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSerial.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSerial.AppearanceHeader.Options.UseFont = true;
            this.colSerial.AppearanceHeader.Options.UseForeColor = true;
            this.colSerial.AppearanceHeader.Options.UseTextOptions = true;
            this.colSerial.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSerial.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSerial.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSerial.Caption = "Code";
            this.colSerial.ColumnEdit = this.repositoryItemMemoEdit9;
            this.colSerial.FieldName = "Serial";
            this.colSerial.MinWidth = 15;
            this.colSerial.Name = "colSerial";
            this.colSerial.OptionsColumn.AllowEdit = false;
            this.colSerial.OptionsColumn.ReadOnly = true;
            this.colSerial.Visible = true;
            this.colSerial.VisibleIndex = 17;
            this.colSerial.Width = 94;
            // 
            // repositoryItemMemoEdit9
            // 
            this.repositoryItemMemoEdit9.Name = "repositoryItemMemoEdit9";
            // 
            // colMaker
            // 
            this.colMaker.AppearanceCell.Options.UseTextOptions = true;
            this.colMaker.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colMaker.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaker.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colMaker.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMaker.AppearanceHeader.Options.UseFont = true;
            this.colMaker.AppearanceHeader.Options.UseForeColor = true;
            this.colMaker.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaker.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaker.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.Caption = "Hãng";
            this.colMaker.ColumnEdit = this.repositoryItemMemoEdit10;
            this.colMaker.FieldName = "Maker";
            this.colMaker.MinWidth = 15;
            this.colMaker.Name = "colMaker";
            this.colMaker.OptionsColumn.AllowEdit = false;
            this.colMaker.OptionsColumn.ReadOnly = true;
            this.colMaker.Visible = true;
            this.colMaker.VisibleIndex = 7;
            this.colMaker.Width = 76;
            // 
            // repositoryItemMemoEdit10
            // 
            this.repositoryItemMemoEdit10.Name = "repositoryItemMemoEdit10";
            // 
            // colNumber
            // 
            this.colNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNumber.AppearanceHeader.Options.UseFont = true;
            this.colNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumber.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumber.Caption = "SL tồn đầu";
            this.colNumber.FieldName = "Number";
            this.colNumber.MinWidth = 15;
            this.colNumber.Name = "colNumber";
            this.colNumber.OptionsColumn.AllowEdit = false;
            this.colNumber.OptionsColumn.ReadOnly = true;
            this.colNumber.Width = 48;
            // 
            // colNumberInStore
            // 
            this.colNumberInStore.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberInStore.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberInStore.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNumberInStore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberInStore.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNumberInStore.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNumberInStore.AppearanceHeader.Options.UseFont = true;
            this.colNumberInStore.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberInStore.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberInStore.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberInStore.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberInStore.Caption = "SL tồn kho";
            this.colNumberInStore.FieldName = "NumberInStore";
            this.colNumberInStore.MinWidth = 15;
            this.colNumberInStore.Name = "colNumberInStore";
            this.colNumberInStore.OptionsColumn.ReadOnly = true;
            this.colNumberInStore.Width = 90;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddress.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAddress.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            this.colAddress.AppearanceHeader.Options.UseForeColor = true;
            this.colAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.Caption = "Vị trí (Hộp)";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colAddress.FieldName = "AddressBox";
            this.colAddress.MinWidth = 15;
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.OptionsColumn.ReadOnly = true;
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 6;
            this.colAddress.Width = 125;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNote.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colNote.FieldName = "Note";
            this.colNote.MinWidth = 15;
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.OptionsColumn.ReadOnly = true;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 13;
            this.colNote.Width = 262;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "StatusProduct";
            this.colStatus.MinWidth = 15;
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 19;
            this.colStatus.Width = 56;
            // 
            // colCreateDate
            // 
            this.colCreateDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreateDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreateDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCreateDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreateDate.AppearanceHeader.Options.UseFont = true;
            this.colCreateDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreateDate.Caption = "Ngày Nhập";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.MinWidth = 15;
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.OptionsColumn.ReadOnly = true;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 86;
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colSerialNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colSerialNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSerialNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSerialNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSerialNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSerialNumber.AppearanceHeader.Options.UseFont = true;
            this.colSerialNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colSerialNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colSerialNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSerialNumber.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSerialNumber.Caption = "Serial";
            this.colSerialNumber.ColumnEdit = this.repositoryItemMemoEdit8;
            this.colSerialNumber.FieldName = "SerialNumber";
            this.colSerialNumber.MinWidth = 15;
            this.colSerialNumber.Name = "colSerialNumber";
            this.colSerialNumber.OptionsColumn.AllowEdit = false;
            this.colSerialNumber.OptionsColumn.ReadOnly = true;
            this.colSerialNumber.Visible = true;
            this.colSerialNumber.VisibleIndex = 16;
            this.colSerialNumber.Width = 109;
            // 
            // repositoryItemMemoEdit8
            // 
            this.repositoryItemMemoEdit8.Name = "repositoryItemMemoEdit8";
            // 
            // colPartNumber
            // 
            this.colPartNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colPartNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colPartNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPartNumber.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPartNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPartNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPartNumber.AppearanceHeader.Options.UseFont = true;
            this.colPartNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colPartNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colPartNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPartNumber.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPartNumber.Caption = "Part Number";
            this.colPartNumber.ColumnEdit = this.repositoryItemMemoEdit7;
            this.colPartNumber.FieldName = "PartNumber";
            this.colPartNumber.MinWidth = 15;
            this.colPartNumber.Name = "colPartNumber";
            this.colPartNumber.OptionsColumn.AllowEdit = false;
            this.colPartNumber.OptionsColumn.ReadOnly = true;
            this.colPartNumber.Visible = true;
            this.colPartNumber.VisibleIndex = 15;
            this.colPartNumber.Width = 138;
            // 
            // repositoryItemMemoEdit7
            // 
            this.repositoryItemMemoEdit7.Name = "repositoryItemMemoEdit7";
            // 
            // colLocation
            // 
            this.colLocation.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colLocation.AppearanceHeader.Options.UseForeColor = true;
            this.colLocation.Caption = "Location";
            this.colLocation.ColumnEdit = this.repositoryItemImageEdit1;
            this.colLocation.FieldName = "Location";
            this.colLocation.MinWidth = 15;
            this.colLocation.Name = "colLocation";
            this.colLocation.Width = 56;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.Appearance.Options.UseImage = true;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            this.repositoryItemImageEdit1.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
            this.repositoryItemImageEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // colLocationImg
            // 
            this.colLocationImg.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colLocationImg.AppearanceHeader.Options.UseForeColor = true;
            this.colLocationImg.Caption = "Ảnh";
            this.colLocationImg.FieldName = "LocationImg";
            this.colLocationImg.MinWidth = 15;
            this.colLocationImg.Name = "colLocationImg";
            this.colLocationImg.OptionsColumn.AllowEdit = false;
            this.colLocationImg.OptionsColumn.ReadOnly = true;
            this.colLocationImg.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colLocationImg.Visible = true;
            this.colLocationImg.VisibleIndex = 10;
            this.colLocationImg.Width = 45;
            // 
            // colProductCodeRTC
            // 
            this.colProductCodeRTC.AppearanceCell.Options.UseFont = true;
            this.colProductCodeRTC.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCodeRTC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCodeRTC.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCodeRTC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProductCodeRTC.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductCodeRTC.AppearanceHeader.Options.UseFont = true;
            this.colProductCodeRTC.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCodeRTC.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCodeRTC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCodeRTC.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCodeRTC.Caption = "Mã kế toán";
            this.colProductCodeRTC.FieldName = "ProductCodeRTC";
            this.colProductCodeRTC.Name = "colProductCodeRTC";
            this.colProductCodeRTC.OptionsColumn.AllowEdit = false;
            this.colProductCodeRTC.OptionsColumn.ReadOnly = true;
            this.colProductCodeRTC.Visible = true;
            this.colProductCodeRTC.VisibleIndex = 18;
            this.colProductCodeRTC.Width = 86;
            // 
            // colUnitCountName
            // 
            this.colUnitCountName.AppearanceCell.Options.UseTextOptions = true;
            this.colUnitCountName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnitCountName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnitCountName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUnitCountName.AppearanceHeader.Options.UseForeColor = true;
            this.colUnitCountName.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnitCountName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnitCountName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnitCountName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnitCountName.Caption = "ĐVT";
            this.colUnitCountName.FieldName = "UnitCountName";
            this.colUnitCountName.Name = "colUnitCountName";
            this.colUnitCountName.OptionsColumn.AllowEdit = false;
            this.colUnitCountName.OptionsColumn.ReadOnly = true;
            this.colUnitCountName.Visible = true;
            this.colUnitCountName.VisibleIndex = 8;
            this.colUnitCountName.Width = 44;
            // 
            // colBorrowCustomer
            // 
            this.colBorrowCustomer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colBorrowCustomer.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBorrowCustomer.AppearanceHeader.Options.UseFont = true;
            this.colBorrowCustomer.AppearanceHeader.Options.UseForeColor = true;
            this.colBorrowCustomer.AppearanceHeader.Options.UseTextOptions = true;
            this.colBorrowCustomer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBorrowCustomer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBorrowCustomer.Caption = "Đồ mượn Khách";
            this.colBorrowCustomer.FieldName = "BorrowCustomerText";
            this.colBorrowCustomer.Name = "colBorrowCustomer";
            this.colBorrowCustomer.OptionsColumn.ReadOnly = true;
            this.colBorrowCustomer.Visible = true;
            this.colBorrowCustomer.VisibleIndex = 14;
            this.colBorrowCustomer.Width = 73;
            // 
            // colBorrowing
            // 
            this.colBorrowing.AppearanceCell.Options.HighPriority = true;
            this.colBorrowing.AppearanceCell.Options.UseTextOptions = true;
            this.colBorrowing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colBorrowing.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBorrowing.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBorrowing.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBorrowing.AppearanceHeader.Options.UseFont = true;
            this.colBorrowing.AppearanceHeader.Options.UseForeColor = true;
            this.colBorrowing.AppearanceHeader.Options.UseTextOptions = true;
            this.colBorrowing.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBorrowing.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBorrowing.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBorrowing.Caption = "Đang mượn";
            this.colBorrowing.FieldName = "NumberBorrowing";
            this.colBorrowing.Name = "colBorrowing";
            this.colBorrowing.OptionsColumn.AllowEdit = false;
            this.colBorrowing.OptionsColumn.ReadOnly = true;
            this.colBorrowing.Width = 47;
            // 
            // colNumberExport
            // 
            this.colNumberExport.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNumberExport.AppearanceHeader.Options.UseFont = true;
            this.colNumberExport.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberExport.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberExport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberExport.Caption = "Xuất";
            this.colNumberExport.FieldName = "NumberExport";
            this.colNumberExport.Name = "colNumberExport";
            this.colNumberExport.Width = 91;
            // 
            // colReturnSupplierID
            // 
            this.colReturnSupplierID.Caption = "ID Nhà cung cấp";
            this.colReturnSupplierID.FieldName = "ReturnSupplierID";
            this.colReturnSupplierID.Name = "colReturnSupplierID";
            // 
            // colImport
            // 
            this.colImport.AppearanceCell.Options.UseTextOptions = true;
            this.colImport.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colImport.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colImport.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colImport.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colImport.AppearanceHeader.Options.UseFont = true;
            this.colImport.AppearanceHeader.Options.UseForeColor = true;
            this.colImport.AppearanceHeader.Options.UseTextOptions = true;
            this.colImport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colImport.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colImport.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colImport.Caption = "SL Nhập";
            this.colImport.FieldName = "NumberImport";
            this.colImport.Name = "colImport";
            this.colImport.OptionsColumn.AllowEdit = false;
            this.colImport.OptionsColumn.ReadOnly = true;
            this.colImport.Width = 38;
            // 
            // colExport
            // 
            this.colExport.AppearanceCell.Options.UseTextOptions = true;
            this.colExport.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colExport.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colExport.AppearanceHeader.Options.UseFont = true;
            this.colExport.AppearanceHeader.Options.UseForeColor = true;
            this.colExport.AppearanceHeader.Options.UseTextOptions = true;
            this.colExport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExport.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colExport.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExport.Caption = "SL Xuất";
            this.colExport.FieldName = "NumberExport";
            this.colExport.Name = "colExport";
            this.colExport.OptionsColumn.AllowEdit = false;
            this.colExport.OptionsColumn.ReadOnly = true;
            this.colExport.Width = 37;
            // 
            // colNumberReal
            // 
            this.colNumberReal.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberReal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNumberReal.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNumberReal.AppearanceHeader.Options.UseFont = true;
            this.colNumberReal.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberReal.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberReal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberReal.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNumberReal.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberReal.Caption = "SL Trong kho";
            this.colNumberReal.FieldName = "InventoryReal";
            this.colNumberReal.Name = "colNumberReal";
            this.colNumberReal.OptionsColumn.AllowEdit = false;
            this.colNumberReal.OptionsColumn.ReadOnly = true;
            this.colNumberReal.Width = 66;
            // 
            // colInventory
            // 
            this.colInventory.AppearanceCell.Options.UseTextOptions = true;
            this.colInventory.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInventory.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colInventory.AppearanceHeader.Options.UseFont = true;
            this.colInventory.AppearanceHeader.Options.UseForeColor = true;
            this.colInventory.AppearanceHeader.Options.UseTextOptions = true;
            this.colInventory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInventory.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colInventory.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colInventory.Caption = "Số Lượng";
            this.colInventory.FieldName = "Inventory";
            this.colInventory.MinWidth = 50;
            this.colInventory.Name = "colInventory";
            this.colInventory.OptionsColumn.AllowEdit = false;
            this.colInventory.OptionsColumn.ReadOnly = true;
            this.colInventory.Visible = true;
            this.colInventory.VisibleIndex = 9;
            this.colInventory.Width = 60;
            // 
            // colSLKiemKe
            // 
            this.colSLKiemKe.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSLKiemKe.AppearanceHeader.Options.UseFont = true;
            this.colSLKiemKe.AppearanceHeader.Options.UseForeColor = true;
            this.colSLKiemKe.Caption = "Số lượng kiểm";
            this.colSLKiemKe.FieldName = "SLKiemKe";
            this.colSLKiemKe.Name = "colSLKiemKe";
            this.colSLKiemKe.Visible = true;
            this.colSLKiemKe.VisibleIndex = 20;
            this.colSLKiemKe.Width = 57;
            // 
            // colLocationImgPath
            // 
            this.colLocationImgPath.Caption = "gridColumn2";
            this.colLocationImgPath.FieldName = "LocationImg";
            this.colLocationImgPath.Name = "colLocationImgPath";
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreatedBy.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedBy.Caption = "Người Tạo";
            this.colCreatedBy.FieldName = "CreatedBy";
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.Visible = true;
            this.colCreatedBy.VisibleIndex = 21;
            // 
            // colBorrowCount
            // 
            this.colBorrowCount.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBorrowCount.AppearanceHeader.Options.UseForeColor = true;
            this.colBorrowCount.Caption = "SL Mượn";
            this.colBorrowCount.FieldName = "BorrowCount";
            this.colBorrowCount.Name = "colBorrowCount";
            this.colBorrowCount.OptionsColumn.AllowEdit = false;
            this.colBorrowCount.OptionsColumn.AllowFocus = false;
            this.colBorrowCount.OptionsColumn.AllowSize = false;
            this.colBorrowCount.Visible = true;
            this.colBorrowCount.VisibleIndex = 2;
            // 
            // colMaxDateBorrow
            // 
            this.colMaxDateBorrow.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMaxDateBorrow.AppearanceHeader.Options.UseForeColor = true;
            this.colMaxDateBorrow.Caption = "Ngày Mượn Gần Nhất";
            this.colMaxDateBorrow.FieldName = "MaxDateBorrow";
            this.colMaxDateBorrow.MinWidth = 90;
            this.colMaxDateBorrow.Name = "colMaxDateBorrow";
            this.colMaxDateBorrow.OptionsColumn.AllowEdit = false;
            this.colMaxDateBorrow.OptionsColumn.AllowFocus = false;
            this.colMaxDateBorrow.OptionsColumn.AllowSize = false;
            this.colMaxDateBorrow.Visible = true;
            this.colMaxDateBorrow.VisibleIndex = 0;
            this.colMaxDateBorrow.Width = 100;
            // 
            // colTotalDay
            // 
            this.colTotalDay.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalDay.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDay.Caption = "Tổng Số Ngày";
            this.colTotalDay.FieldName = "TotalDay";
            this.colTotalDay.Name = "colTotalDay";
            this.colTotalDay.OptionsColumn.AllowEdit = false;
            this.colTotalDay.OptionsColumn.AllowFocus = false;
            this.colTotalDay.OptionsColumn.AllowSize = false;
            this.colTotalDay.Visible = true;
            this.colTotalDay.VisibleIndex = 3;
            this.colTotalDay.Width = 90;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // riAnhSp
            // 
            this.riAnhSp.Name = "riAnhSp";
            this.riAnhSp.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // frmBorrowReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 615);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmBorrowReport";
            this.Text = "BÁO CÁO MƯỢN THIẾT BỊ";
            this.Load += new System.EventHandler(this.frmBorrowReport_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAnhSp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colProductGroupNo;
        private DevExpress.XtraGrid.Columns.GridColumn colProductGroupName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit11;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit6;
        private DevExpress.XtraGrid.Columns.GridColumn colSerial;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit9;
        private DevExpress.XtraGrid.Columns.GridColumn colMaker;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit10;
        private DevExpress.XtraGrid.Columns.GridColumn colNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberInStore;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit8;
        private DevExpress.XtraGrid.Columns.GridColumn colPartNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationImg;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCodeRTC;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitCountName;
        private DevExpress.XtraGrid.Columns.GridColumn colBorrowCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colBorrowing;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberExport;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnSupplierID;
        private DevExpress.XtraGrid.Columns.GridColumn colImport;
        private DevExpress.XtraGrid.Columns.GridColumn colExport;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberReal;
        private DevExpress.XtraGrid.Columns.GridColumn colInventory;
        private DevExpress.XtraGrid.Columns.GridColumn colSLKiemKe;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationImgPath;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colBorrowCount;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxDateBorrow;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDay;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit riAnhSp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnResetData;
    }
}