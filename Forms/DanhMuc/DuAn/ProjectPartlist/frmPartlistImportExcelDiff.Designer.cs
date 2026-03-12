namespace BMS
{
    partial class frmPartlistImportExcelDiff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartlistImportExcelDiff));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule2 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue2 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            this.colIsSameMaker = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colGroupMaterial = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colProductCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSuggest = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductGroupName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWarehouseType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductCodeStock = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGroupMaterialStock = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colManufacturer = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colManufacturerStock = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colUnit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colUnitStock = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsSameProductName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsSameProductCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIsSameUnit = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colProductSaleID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.cboProduct = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductCodePl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturerPl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupMaterialPl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // colIsSameMaker
            // 
            this.colIsSameMaker.FieldName = "IsSameMaker";
            this.colIsSameMaker.Name = "colIsSameMaker";
            this.colIsSameMaker.OptionsColumn.AllowEdit = false;
            this.colIsSameMaker.OptionsColumn.ReadOnly = true;
            this.colIsSameMaker.Visible = true;
            // 
            // colGroupMaterial
            // 
            this.colGroupMaterial.Caption = "Partlist";
            this.colGroupMaterial.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colGroupMaterial.FieldName = "GroupMaterial_Partlist";
            this.colGroupMaterial.Name = "colGroupMaterial";
            this.colGroupMaterial.Visible = true;
            this.colGroupMaterial.Width = 313;
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.Transparent;
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator2,
            this.btnCancel,
            this.toolStripSeparator3});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1313, 58);
            this.mnuMenu.TabIndex = 187;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 55);
            this.btnSave.Tag = "frmProjectPartList_New_ImportExcel";
            this.btnSave.Text = "Lấy thông tin tích xanh";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Image = global::Forms.Properties.Resources.removepivotfield_32x32;
            this.btnCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 55);
            this.btnCancel.Tag = "";
            this.btnCancel.Text = "Đóng";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 58);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboProduct,
            this.repositoryItemCheckEdit2,
            this.repositoryItemMemoEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1313, 597);
            this.gridControl1.TabIndex = 188;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridView1.Appearance.BandPanel.Options.UseFont = true;
            this.bandedGridView1.Appearance.BandPanel.Options.UseForeColor = true;
            this.bandedGridView1.Appearance.BandPanel.Options.UseTextOptions = true;
            this.bandedGridView1.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridView1.Appearance.BandPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bandedGridView1.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandedGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedGridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.bandedGridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.bandedGridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bandedGridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandedGridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridView1.Appearance.Row.Options.UseFont = true;
            this.bandedGridView1.Appearance.Row.Options.UseTextOptions = true;
            this.bandedGridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bandedGridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3,
            this.gridBand4});
            this.bandedGridView1.ColumnPanelRowHeight = 50;
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colID,
            this.colGroupMaterial,
            this.colProductCode,
            this.colManufacturer,
            this.colUnit,
            this.colGroupMaterialStock,
            this.colProductCodeStock,
            this.colManufacturerStock,
            this.colUnitStock,
            this.colProductGroupName,
            this.colIsSameProductName,
            this.colIsSameProductCode,
            this.colIsSameMaker,
            this.colIsSameUnit,
            this.colProductSaleID,
            this.colWarehouseType,
            this.colSuggest,
            this.bandedGridColumn1});
            this.bandedGridView1.CustomizationFormBounds = new System.Drawing.Rectangle(832, 276, 261, 425);
            gridFormatRule1.ApplyToRow = true;
            gridFormatRule1.Column = this.colIsSameMaker;
            gridFormatRule1.Name = "Format0";
            formatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue1.Value1 = "true";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            gridFormatRule2.ApplyToRow = true;
            gridFormatRule2.Column = this.colGroupMaterial;
            gridFormatRule2.Name = "Format1";
            formatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue2.Value1 = "true";
            gridFormatRule2.Rule = formatConditionRuleValue2;
            this.bandedGridView1.FormatRules.Add(gridFormatRule1);
            this.bandedGridView1.FormatRules.Add(gridFormatRule2);
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsFind.AlwaysVisible = true;
            this.bandedGridView1.OptionsView.RowAutoHeight = true;
            this.bandedGridView1.OptionsView.ShowAutoFilterRow = true;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            this.bandedGridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.bandedGridView1_RowCellStyle_1);
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colID);
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Columns.Add(this.colProductCode);
            this.gridBand1.Columns.Add(this.colSuggest);
            this.gridBand1.Columns.Add(this.colProductGroupName);
            this.gridBand1.Columns.Add(this.colWarehouseType);
            this.gridBand1.Columns.Add(this.colProductCodeStock);
            this.gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 231;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Tích xanh";
            this.bandedGridColumn1.ColumnEdit = this.repositoryItemCheckEdit2;
            this.bandedGridColumn1.FieldName = "IsFix";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn1.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Mã thiết bị";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.Width = 231;
            // 
            // colSuggest
            // 
            this.colSuggest.AppearanceCell.Options.UseTextOptions = true;
            this.colSuggest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSuggest.Caption = "Gợi ý mã";
            this.colSuggest.Name = "colSuggest";
            // 
            // colProductGroupName
            // 
            this.colProductGroupName.Caption = "Nhóm";
            this.colProductGroupName.FieldName = "ProductGroupName";
            this.colProductGroupName.Name = "colProductGroupName";
            this.colProductGroupName.OptionsColumn.AllowEdit = false;
            this.colProductGroupName.OptionsColumn.ReadOnly = true;
            this.colProductGroupName.Width = 182;
            // 
            // colWarehouseType
            // 
            this.colWarehouseType.FieldName = "Loại kho";
            this.colWarehouseType.Name = "colWarehouseType";
            // 
            // colProductCodeStock
            // 
            this.colProductCodeStock.Caption = "Mã thiết bị (trong kho)";
            this.colProductCodeStock.FieldName = "ProductCodeStock";
            this.colProductCodeStock.Name = "colProductCodeStock";
            this.colProductCodeStock.OptionsColumn.AllowEdit = false;
            this.colProductCodeStock.OptionsColumn.ReadOnly = true;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "TÊN THIẾT BỊ";
            this.gridBand2.Columns.Add(this.colGroupMaterial);
            this.gridBand2.Columns.Add(this.colGroupMaterialStock);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 623;
            // 
            // colGroupMaterialStock
            // 
            this.colGroupMaterialStock.Caption = "Kho";
            this.colGroupMaterialStock.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colGroupMaterialStock.FieldName = "GroupMaterial_Stock";
            this.colGroupMaterialStock.Name = "colGroupMaterialStock";
            this.colGroupMaterialStock.OptionsColumn.AllowEdit = false;
            this.colGroupMaterialStock.OptionsColumn.ReadOnly = true;
            this.colGroupMaterialStock.Visible = true;
            this.colGroupMaterialStock.Width = 310;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "HÃNG";
            this.gridBand3.Columns.Add(this.colManufacturer);
            this.gridBand3.Columns.Add(this.colManufacturerStock);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 219;
            // 
            // colManufacturer
            // 
            this.colManufacturer.Caption = "Partlist";
            this.colManufacturer.FieldName = "Manufacturer_Partlist";
            this.colManufacturer.Name = "colManufacturer";
            this.colManufacturer.Visible = true;
            this.colManufacturer.Width = 105;
            // 
            // colManufacturerStock
            // 
            this.colManufacturerStock.Caption = "Kho";
            this.colManufacturerStock.FieldName = "Manufacturer_Stock";
            this.colManufacturerStock.Name = "colManufacturerStock";
            this.colManufacturerStock.OptionsColumn.AllowEdit = false;
            this.colManufacturerStock.OptionsColumn.ReadOnly = true;
            this.colManufacturerStock.Visible = true;
            this.colManufacturerStock.Width = 114;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "ĐƠN VỊ";
            this.gridBand4.Columns.Add(this.colUnit);
            this.gridBand4.Columns.Add(this.colUnitStock);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 3;
            this.gridBand4.Width = 213;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "Partlist";
            this.colUnit.FieldName = "Unit_Partlist";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.ReadOnly = true;
            this.colUnit.Visible = true;
            this.colUnit.Width = 107;
            // 
            // colUnitStock
            // 
            this.colUnitStock.Caption = "Kho";
            this.colUnitStock.FieldName = "Unit_Stock";
            this.colUnitStock.Name = "colUnitStock";
            this.colUnitStock.OptionsColumn.AllowEdit = false;
            this.colUnitStock.OptionsColumn.ReadOnly = true;
            this.colUnitStock.Visible = true;
            this.colUnitStock.Width = 106;
            // 
            // colIsSameProductName
            // 
            this.colIsSameProductName.FieldName = "IsSameProductName";
            this.colIsSameProductName.Name = "colIsSameProductName";
            this.colIsSameProductName.OptionsColumn.AllowEdit = false;
            this.colIsSameProductName.OptionsColumn.ReadOnly = true;
            this.colIsSameProductName.Visible = true;
            // 
            // colIsSameProductCode
            // 
            this.colIsSameProductCode.FieldName = "IsSameProductCode";
            this.colIsSameProductCode.Name = "colIsSameProductCode";
            this.colIsSameProductCode.OptionsColumn.AllowEdit = false;
            this.colIsSameProductCode.OptionsColumn.ReadOnly = true;
            this.colIsSameProductCode.Visible = true;
            // 
            // colIsSameUnit
            // 
            this.colIsSameUnit.FieldName = "IsSameUnit";
            this.colIsSameUnit.Name = "colIsSameUnit";
            this.colIsSameUnit.OptionsColumn.AllowEdit = false;
            this.colIsSameUnit.OptionsColumn.ReadOnly = true;
            this.colIsSameUnit.Visible = true;
            // 
            // colProductSaleID
            // 
            this.colProductSaleID.FieldName = "ProductSaleID";
            this.colProductSaleID.Name = "colProductSaleID";
            this.colProductSaleID.OptionsColumn.AllowEdit = false;
            this.colProductSaleID.OptionsColumn.ReadOnly = true;
            this.colProductSaleID.Visible = true;
            // 
            // cboProduct
            // 
            this.cboProduct.AutoHeight = false;
            this.cboProduct.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.NullText = "Gợi ý";
            this.cboProduct.PopupView = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemGridLookUpEdit1View.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.repositoryItemGridLookUpEdit1View.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.repositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseForeColor = true;
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductCodePl,
            this.colManufacturerPl,
            this.colUnitPl,
            this.colGroupMaterialPl,
            this.gridColumn12});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colProductCodePl
            // 
            this.colProductCodePl.Caption = "Mã thiết bị";
            this.colProductCodePl.FieldName = "ProductCode";
            this.colProductCodePl.Name = "colProductCodePl";
            this.colProductCodePl.Visible = true;
            this.colProductCodePl.VisibleIndex = 0;
            // 
            // colManufacturerPl
            // 
            this.colManufacturerPl.Caption = "Hãng";
            this.colManufacturerPl.FieldName = "Manufacturer";
            this.colManufacturerPl.Name = "colManufacturerPl";
            this.colManufacturerPl.Visible = true;
            this.colManufacturerPl.VisibleIndex = 3;
            // 
            // colUnitPl
            // 
            this.colUnitPl.Caption = "Đơn vị";
            this.colUnitPl.FieldName = "Unit";
            this.colUnitPl.Name = "colUnitPl";
            this.colUnitPl.Visible = true;
            this.colUnitPl.VisibleIndex = 2;
            // 
            // colGroupMaterialPl
            // 
            this.colGroupMaterialPl.Caption = "Tên thiết bị";
            this.colGroupMaterialPl.FieldName = "GroupMaterial";
            this.colGroupMaterialPl.Name = "colGroupMaterialPl";
            this.colGroupMaterialPl.Visible = true;
            this.colGroupMaterialPl.VisibleIndex = 1;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "gridColumn12";
            this.gridColumn12.FieldName = "ID";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // frmPartlistImportExcelDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 655);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmPartlistImportExcelDiff";
            this.Text = "CHI TIẾT CÁC VẬT TƯ KHÁC THÔNG TIN TÍCH XANH";
            this.Load += new System.EventHandler(this.frmPartlistImportExcelDiff_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSuggest;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductGroupName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWarehouseType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductCodeStock;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGroupMaterial;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGroupMaterialStock;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colManufacturer;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colManufacturerStock;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUnitStock;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsSameProductName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsSameProductCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsSameMaker;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIsSameUnit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colProductSaleID;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboProduct;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCodePl;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturerPl;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPl;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupMaterialPl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}