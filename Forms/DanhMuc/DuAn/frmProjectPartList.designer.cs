
namespace Forms.DanhMuc.DuAn
{
    partial class frmProjectPartList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectPartList));
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupMaterial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyReturn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQtyFull = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLeadTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpectedReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDeleted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btlExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSelection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbPersonManager = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.btnSet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStatus = new System.Windows.Forms.Button();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.lbProjectName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPersonManager.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(0, 47);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1858, 545);
            this.grdData.TabIndex = 1;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSTT,
            this.colGroupMaterial,
            this.colModel,
            this.colProductCode,
            this.colManufacturer,
            this.colUnit,
            this.colQtyMin,
            this.colQtyReturn,
            this.colQtyFull,
            this.colPrice,
            this.colAmount,
            this.colVAT,
            this.colLeadTime,
            this.colExpectedReturnDate,
            this.colStatus,
            this.colNote,
            this.colNote1,
            this.colNote2,
            this.colStatusText,
            this.colFullName,
            this.colNCC,
            this.colRequestDate,
            this.colIsDeleted});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvData_CustomDrawCell);
            this.grvData.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvData_RowCellStyle);
            this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colSTT.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSTT.AppearanceHeader.Options.UseFont = true;
            this.colSTT.AppearanceHeader.Options.UseForeColor = true;
            this.colSTT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSTT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.MinWidth = 50;
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 1;
            this.colSTT.Width = 50;
            // 
            // colGroupMaterial
            // 
            this.colGroupMaterial.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colGroupMaterial.AppearanceCell.Options.UseFont = true;
            this.colGroupMaterial.AppearanceCell.Options.UseTextOptions = true;
            this.colGroupMaterial.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGroupMaterial.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupMaterial.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colGroupMaterial.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGroupMaterial.AppearanceHeader.Options.UseFont = true;
            this.colGroupMaterial.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupMaterial.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupMaterial.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupMaterial.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGroupMaterial.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupMaterial.Caption = "Tên vật tư";
            this.colGroupMaterial.FieldName = "GroupMaterial";
            this.colGroupMaterial.MinWidth = 150;
            this.colGroupMaterial.Name = "colGroupMaterial";
            this.colGroupMaterial.Visible = true;
            this.colGroupMaterial.VisibleIndex = 5;
            this.colGroupMaterial.Width = 150;
            // 
            // colModel
            // 
            this.colModel.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colModel.AppearanceCell.Options.UseFont = true;
            this.colModel.AppearanceCell.Options.UseTextOptions = true;
            this.colModel.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colModel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colModel.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colModel.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colModel.AppearanceHeader.Options.UseFont = true;
            this.colModel.AppearanceHeader.Options.UseForeColor = true;
            this.colModel.AppearanceHeader.Options.UseTextOptions = true;
            this.colModel.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colModel.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colModel.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colModel.Caption = "Thông số kỹ thuật";
            this.colModel.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colModel.FieldName = "Model";
            this.colModel.MaxWidth = 250;
            this.colModel.MinWidth = 180;
            this.colModel.Name = "colModel";
            this.colModel.Visible = true;
            this.colModel.VisibleIndex = 7;
            this.colModel.Width = 180;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colProductCode.AppearanceCell.Options.UseFont = true;
            this.colProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colProductCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductCode.AppearanceHeader.Options.UseFont = true;
            this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã Thiết bị";
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.MinWidth = 150;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 4;
            this.colProductCode.Width = 150;
            // 
            // colManufacturer
            // 
            this.colManufacturer.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colManufacturer.AppearanceCell.Options.UseFont = true;
            this.colManufacturer.AppearanceCell.Options.UseTextOptions = true;
            this.colManufacturer.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colManufacturer.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colManufacturer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colManufacturer.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colManufacturer.AppearanceHeader.Options.UseFont = true;
            this.colManufacturer.AppearanceHeader.Options.UseForeColor = true;
            this.colManufacturer.AppearanceHeader.Options.UseTextOptions = true;
            this.colManufacturer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colManufacturer.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colManufacturer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colManufacturer.Caption = "Hãng sản xuất";
            this.colManufacturer.FieldName = "Manufacturer";
            this.colManufacturer.MinWidth = 120;
            this.colManufacturer.Name = "colManufacturer";
            this.colManufacturer.Visible = true;
            this.colManufacturer.VisibleIndex = 6;
            this.colManufacturer.Width = 120;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colUnit.AppearanceCell.Options.UseFont = true;
            this.colUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colUnit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colUnit.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Options.UseForeColor = true;
            this.colUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.Caption = "Đơn vị";
            this.colUnit.FieldName = "Unit";
            this.colUnit.MinWidth = 60;
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 11;
            this.colUnit.Width = 60;
            // 
            // colQtyMin
            // 
            this.colQtyMin.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colQtyMin.AppearanceCell.Options.UseFont = true;
            this.colQtyMin.AppearanceCell.Options.UseTextOptions = true;
            this.colQtyMin.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQtyMin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyMin.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colQtyMin.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQtyMin.AppearanceHeader.Options.UseFont = true;
            this.colQtyMin.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyMin.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyMin.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQtyMin.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyMin.Caption = "Số Lượng/ 1 máy";
            this.colQtyMin.FieldName = "QtyMin";
            this.colQtyMin.MinWidth = 100;
            this.colQtyMin.Name = "colQtyMin";
            this.colQtyMin.Visible = true;
            this.colQtyMin.VisibleIndex = 8;
            this.colQtyMin.Width = 100;
            // 
            // colQtyReturn
            // 
            this.colQtyReturn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colQtyReturn.AppearanceCell.Options.UseFont = true;
            this.colQtyReturn.AppearanceCell.Options.UseTextOptions = true;
            this.colQtyReturn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQtyReturn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyReturn.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colQtyReturn.AppearanceHeader.Options.UseFont = true;
            this.colQtyReturn.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyReturn.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyReturn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyReturn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQtyReturn.Caption = "Số lượng đã về";
            this.colQtyReturn.FieldName = "QtyReturned";
            this.colQtyReturn.Name = "colQtyReturn";
            this.colQtyReturn.OptionsColumn.FixedWidth = true;
            this.colQtyReturn.Visible = true;
            this.colQtyReturn.VisibleIndex = 10;
            this.colQtyReturn.Width = 86;
            // 
            // colQtyFull
            // 
            this.colQtyFull.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colQtyFull.AppearanceCell.Options.UseFont = true;
            this.colQtyFull.AppearanceCell.Options.UseTextOptions = true;
            this.colQtyFull.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQtyFull.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyFull.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colQtyFull.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQtyFull.AppearanceHeader.Options.UseFont = true;
            this.colQtyFull.AppearanceHeader.Options.UseForeColor = true;
            this.colQtyFull.AppearanceHeader.Options.UseTextOptions = true;
            this.colQtyFull.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQtyFull.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQtyFull.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQtyFull.Caption = "Số lượng Tổng";
            this.colQtyFull.FieldName = "QtyFull";
            this.colQtyFull.MinWidth = 80;
            this.colQtyFull.Name = "colQtyFull";
            this.colQtyFull.Visible = true;
            this.colQtyFull.VisibleIndex = 9;
            this.colQtyFull.Width = 80;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colPrice.AppearanceCell.Options.UseFont = true;
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPrice.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPrice.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colPrice.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPrice.AppearanceHeader.Options.UseFont = true;
            this.colPrice.AppearanceHeader.Options.UseForeColor = true;
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPrice.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPrice.Caption = "Giá (VND)";
            this.colPrice.DisplayFormat.FormatString = "#,###";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.MinWidth = 100;
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 12;
            this.colPrice.Width = 100;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colAmount.AppearanceCell.Options.UseFont = true;
            this.colAmount.AppearanceCell.Options.UseTextOptions = true;
            this.colAmount.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAmount.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAmount.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colAmount.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.AppearanceHeader.Options.UseForeColor = true;
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAmount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAmount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAmount.Caption = "Thành tiền";
            this.colAmount.DisplayFormat.FormatString = "#,###";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.MinWidth = 100;
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 13;
            this.colAmount.Width = 100;
            // 
            // colVAT
            // 
            this.colVAT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colVAT.AppearanceCell.Options.UseFont = true;
            this.colVAT.AppearanceCell.Options.UseTextOptions = true;
            this.colVAT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colVAT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colVAT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colVAT.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colVAT.AppearanceHeader.Options.UseFont = true;
            this.colVAT.AppearanceHeader.Options.UseForeColor = true;
            this.colVAT.AppearanceHeader.Options.UseTextOptions = true;
            this.colVAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVAT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colVAT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colVAT.Caption = "VAT (%)";
            this.colVAT.FieldName = "VAT_pt";
            this.colVAT.MaxWidth = 50;
            this.colVAT.MinWidth = 50;
            this.colVAT.Name = "colVAT";
            this.colVAT.Width = 50;
            // 
            // colLeadTime
            // 
            this.colLeadTime.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colLeadTime.AppearanceCell.Options.UseFont = true;
            this.colLeadTime.AppearanceCell.Options.UseTextOptions = true;
            this.colLeadTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLeadTime.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLeadTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colLeadTime.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colLeadTime.AppearanceHeader.Options.UseFont = true;
            this.colLeadTime.AppearanceHeader.Options.UseForeColor = true;
            this.colLeadTime.AppearanceHeader.Options.UseTextOptions = true;
            this.colLeadTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLeadTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLeadTime.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLeadTime.Caption = "Tiến độ";
            this.colLeadTime.FieldName = "LeadTime";
            this.colLeadTime.MinWidth = 100;
            this.colLeadTime.Name = "colLeadTime";
            this.colLeadTime.Visible = true;
            this.colLeadTime.VisibleIndex = 14;
            this.colLeadTime.Width = 100;
            // 
            // colExpectedReturnDate
            // 
            this.colExpectedReturnDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colExpectedReturnDate.AppearanceCell.Options.UseFont = true;
            this.colExpectedReturnDate.AppearanceCell.Options.UseTextOptions = true;
            this.colExpectedReturnDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExpectedReturnDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colExpectedReturnDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExpectedReturnDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colExpectedReturnDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colExpectedReturnDate.AppearanceHeader.Options.UseFont = true;
            this.colExpectedReturnDate.AppearanceHeader.Options.UseForeColor = true;
            this.colExpectedReturnDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colExpectedReturnDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colExpectedReturnDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colExpectedReturnDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colExpectedReturnDate.Caption = "Ngày về dự kiến";
            this.colExpectedReturnDate.FieldName = "ExpectedReturnDate";
            this.colExpectedReturnDate.MinWidth = 120;
            this.colExpectedReturnDate.Name = "colExpectedReturnDate";
            this.colExpectedReturnDate.Visible = true;
            this.colExpectedReturnDate.VisibleIndex = 17;
            this.colExpectedReturnDate.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colStatus.AppearanceCell.Options.UseFont = true;
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
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
            this.colNote.MinWidth = 150;
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 19;
            this.colNote.Width = 150;
            // 
            // colNote1
            // 
            this.colNote1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colNote1.AppearanceCell.Options.UseFont = true;
            this.colNote1.AppearanceCell.Options.UseTextOptions = true;
            this.colNote1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colNote1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNote1.AppearanceHeader.Options.UseFont = true;
            this.colNote1.AppearanceHeader.Options.UseForeColor = true;
            this.colNote1.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote1.Caption = "Chất lượng";
            this.colNote1.FieldName = "Note1";
            this.colNote1.MinWidth = 100;
            this.colNote1.Name = "colNote1";
            this.colNote1.Visible = true;
            this.colNote1.VisibleIndex = 18;
            this.colNote1.Width = 100;
            // 
            // colNote2
            // 
            this.colNote2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colNote2.AppearanceCell.Options.UseFont = true;
            this.colNote2.AppearanceCell.Options.UseTextOptions = true;
            this.colNote2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colNote2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNote2.AppearanceHeader.Options.UseFont = true;
            this.colNote2.AppearanceHeader.Options.UseForeColor = true;
            this.colNote2.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote2.Caption = "Ghi chú 2";
            this.colNote2.FieldName = "Note2";
            this.colNote2.Name = "colNote2";
            // 
            // colStatusText
            // 
            this.colStatusText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colStatusText.AppearanceCell.Options.UseFont = true;
            this.colStatusText.AppearanceCell.Options.UseTextOptions = true;
            this.colStatusText.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusText.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusText.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colStatusText.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatusText.AppearanceHeader.Options.UseFont = true;
            this.colStatusText.AppearanceHeader.Options.UseForeColor = true;
            this.colStatusText.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusText.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusText.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusText.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusText.Caption = "Tình trạng";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.MinWidth = 150;
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 2;
            this.colStatusText.Width = 150;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colFullName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colFullName.AppearanceHeader.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Options.UseForeColor = true;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Người phụ trách";
            this.colFullName.FieldName = "FullName";
            this.colFullName.MinWidth = 150;
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 3;
            this.colFullName.Width = 150;
            // 
            // colNCC
            // 
            this.colNCC.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colNCC.AppearanceCell.Options.UseFont = true;
            this.colNCC.AppearanceCell.Options.UseTextOptions = true;
            this.colNCC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNCC.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNCC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colNCC.AppearanceHeader.Options.UseFont = true;
            this.colNCC.AppearanceHeader.Options.UseForeColor = true;
            this.colNCC.AppearanceHeader.Options.UseTextOptions = true;
            this.colNCC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNCC.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNCC.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNCC.Caption = "NCC";
            this.colNCC.FieldName = "NCC";
            this.colNCC.MinWidth = 100;
            this.colNCC.Name = "colNCC";
            this.colNCC.Visible = true;
            this.colNCC.VisibleIndex = 15;
            this.colNCC.Width = 100;
            // 
            // colRequestDate
            // 
            this.colRequestDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.colRequestDate.AppearanceCell.Options.UseFont = true;
            this.colRequestDate.AppearanceCell.Options.UseTextOptions = true;
            this.colRequestDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRequestDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRequestDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.colRequestDate.AppearanceHeader.Options.UseFont = true;
            this.colRequestDate.AppearanceHeader.Options.UseForeColor = true;
            this.colRequestDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colRequestDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRequestDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRequestDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRequestDate.Caption = "Ngày yêu cầu";
            this.colRequestDate.FieldName = "RequestDate";
            this.colRequestDate.MinWidth = 110;
            this.colRequestDate.Name = "colRequestDate";
            this.colRequestDate.Visible = true;
            this.colRequestDate.VisibleIndex = 16;
            this.colRequestDate.Width = 110;
            // 
            // colIsDeleted
            // 
            this.colIsDeleted.AppearanceCell.Options.UseTextOptions = true;
            this.colIsDeleted.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsDeleted.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsDeleted.Caption = "Đã xóa";
            this.colIsDeleted.FieldName = "IsDeleted";
            this.colIsDeleted.Name = "colIsDeleted";
            this.colIsDeleted.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            this.colIsDeleted.Width = 20;
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 41);
            this.btnNew.Tag = "frmProjectPartList_New";
            this.btnNew.Text = "Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.ToolTipText = "Thêm";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 41);
            this.btnEdit.Tag = "frmProjectPartList_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 41);
            this.btnDelete.Tag = "frmProjectPartList_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // btlExcel
            // 
            this.btlExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btlExcel.Image = ((System.Drawing.Image)(resources.GetObject("btlExcel.Image")));
            this.btlExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlExcel.Name = "btlExcel";
            this.btlExcel.Size = new System.Drawing.Size(91, 41);
            this.btlExcel.Tag = "frmProjectPartList_Excel";
            this.btlExcel.Text = "Nhập Excel";
            this.btlExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btlExcel.Click += new System.EventHandler(this.btlExcel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btlExcel,
            this.toolStripSeparator4,
            this.btnExportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1858, 44);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 41);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 41);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.AutoSize = false;
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(80, 41);
            this.btnExportExcel.Tag = "";
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDEmployee,
            this.colIsSelection,
            this.colCode,
            this.gridColumn1});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colIDEmployee
            // 
            this.colIDEmployee.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDEmployee.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDEmployee.Caption = "ID";
            this.colIDEmployee.FieldName = "ID";
            this.colIDEmployee.Name = "colIDEmployee";
            // 
            // colIsSelection
            // 
            this.colIsSelection.Caption = "IsSelection";
            this.colIsSelection.MaxWidth = 50;
            this.colIsSelection.Name = "colIsSelection";
            this.colIsSelection.Visible = true;
            this.colIsSelection.VisibleIndex = 2;
            this.colIsSelection.Width = 50;
            // 
            // colCode
            // 
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Tên nhân viên";
            this.gridColumn1.FieldName = "FullName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // cbPersonManager
            // 
            this.cbPersonManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPersonManager.Location = new System.Drawing.Point(1570, 10);
            this.cbPersonManager.Name = "cbPersonManager";
            this.cbPersonManager.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbPersonManager.Properties.Appearance.Options.UseFont = true;
            this.cbPersonManager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPersonManager.Properties.NullText = "";
            this.cbPersonManager.Properties.PopupView = this.gridView1;
            this.cbPersonManager.Size = new System.Drawing.Size(214, 26);
            this.cbPersonManager.TabIndex = 7;
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSet.Location = new System.Drawing.Point(1789, 9);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(57, 28);
            this.btnSet.TabIndex = 8;
            this.btnSet.Tag = "frmProjectPartList_SetUser";
            this.btnSet.Text = "SET";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1419, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 19);
            this.label6.TabIndex = 9;
            this.label6.Text = "Người phụ trách (*)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1102, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tình trạng (*)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnStatus
            // 
            this.btnStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnStatus.Location = new System.Drawing.Point(1341, 10);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(69, 28);
            this.btnStatus.TabIndex = 11;
            this.btnStatus.Tag = "frmProjectPartList_SetStatus";
            this.btnStatus.Text = "SET";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // cbStatus
            // 
            this.cbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Chưa đặt hàng",
            "Đã về",
            "Đã đặt hàng",
            "Không đặt hàng"});
            this.cbStatus.Location = new System.Drawing.Point(1213, 11);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 26);
            this.cbStatus.TabIndex = 13;
            // 
            // lbProjectName
            // 
            this.lbProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProjectName.AutoSize = true;
            this.lbProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbProjectName.Location = new System.Drawing.Point(512, 10);
            this.lbProjectName.Name = "lbProjectName";
            this.lbProjectName.Size = new System.Drawing.Size(0, 26);
            this.lbProjectName.TabIndex = 14;
            // 
            // frmProjectPartList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1858, 592);
            this.Controls.Add(this.lbProjectName);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.cbPersonManager);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmProjectPartList";
            this.Text = "TIẾN ĐỘ VẬT TƯ DỰ ÁN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProjectPartList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPersonManager.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupMaterial;
        private DevExpress.XtraGrid.Columns.GridColumn colModel;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturer;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyMin;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyFull;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colLeadTime;
        private DevExpress.XtraGrid.Columns.GridColumn colExpectedReturnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colNote1;
        private DevExpress.XtraGrid.Columns.GridColumn colNote2;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btlExcel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colIDEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelection;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SearchLookUpEdit cbPersonManager;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.Label lbProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colNCC;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestDate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDeleted;
        private DevExpress.XtraGrid.Columns.GridColumn colQtyReturn;
    }
}