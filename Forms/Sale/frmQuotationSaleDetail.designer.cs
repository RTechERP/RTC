
namespace BMS
{
    partial class frmQuotationSaleDetail
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
            this.cboProduct = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTermCondition = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeliveryPeriod = new System.Windows.Forms.TextBox();
            this.txtCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new DevExpress.XtraEditors.TextEdit();
            this.cboSale = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dteQuotationDate = new DevExpress.XtraEditors.DateEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this.cboCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.cboProject = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.grvProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TreeData = new DevExpress.XtraTreeList.TreeList();
            this.colDetailID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSTT = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMaker = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProductName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProductCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colQuantity = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUnit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUnitImportPriceVND = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalImportPriceVND = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTradePriceDetailID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnAddTermCondition = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cboProduct)).BeginInit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTermCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSale.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteQuotationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteQuotationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeData)).BeginInit();
            this.SuspendLayout();
            // 
            // cboProduct
            // 
            this.cboProduct.AutoHeight = false;
            this.cboProduct.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.NullText = "";
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1365, 52);
            this.mnuMenu.TabIndex = 20;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 49);
            this.btnSave.Tag = "frmQuotationSale_Edit";
            this.btnSave.Text = "Cất";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1109, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 16);
            this.label8.TabIndex = 133;
            this.label8.Text = "Tổng tiền nhập";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 125;
            this.label1.Text = "Mã báo giá";
            // 
            // cboTermCondition
            // 
            this.cboTermCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTermCondition.Location = new System.Drawing.Point(488, 117);
            this.cboTermCondition.Margin = new System.Windows.Forms.Padding(4);
            this.cboTermCondition.Name = "cboTermCondition";
            this.cboTermCondition.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTermCondition.Properties.Appearance.Options.UseFont = true;
            this.cboTermCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTermCondition.Properties.PopupFormSize = new System.Drawing.Size(0, 300);
            this.cboTermCondition.Size = new System.Drawing.Size(316, 22);
            this.cboTermCondition.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1109, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 16);
            this.label6.TabIndex = 126;
            this.label6.Text = "Thời gian giao hàng";
            // 
            // txtDeliveryPeriod
            // 
            this.txtDeliveryPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeliveryPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeliveryPeriod.Location = new System.Drawing.Point(1242, 55);
            this.txtDeliveryPeriod.Name = "txtDeliveryPeriod";
            this.txtDeliveryPeriod.Size = new System.Drawing.Size(113, 22);
            this.txtDeliveryPeriod.TabIndex = 9;
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerAddress.Location = new System.Drawing.Point(488, 85);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(345, 22);
            this.txtCustomerAddress.TabIndex = 4;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(96, 55);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(240, 22);
            this.txtCode.TabIndex = 0;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(342, 88);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(48, 16);
            this.label27.TabIndex = 146;
            this.label27.Text = "Địa chỉ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 16);
            this.label11.TabIndex = 128;
            this.label11.Text = "Sale Admin";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(342, 120);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(139, 16);
            this.label26.TabIndex = 147;
            this.label26.Text = "Điều khoản thanh toán";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalPrice.EditValue = "123123";
            this.txtTotalPrice.Location = new System.Drawing.Point(1242, 117);
            this.txtTotalPrice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrice.Properties.Appearance.Options.UseFont = true;
            this.txtTotalPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTotalPrice.Properties.DisplayFormat.FormatString = "n0";
            this.txtTotalPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotalPrice.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtTotalPrice.Properties.MaskSettings.Set("mask", "n0");
            this.txtTotalPrice.Properties.ReadOnly = true;
            this.txtTotalPrice.Properties.UseMaskAsDisplayFormat = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(113, 22);
            this.txtTotalPrice.TabIndex = 11;
            // 
            // cboSale
            // 
            this.cboSale.EditValue = "";
            this.cboSale.Location = new System.Drawing.Point(96, 117);
            this.cboSale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboSale.Name = "cboSale";
            this.cboSale.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSale.Properties.Appearance.Options.UseFont = true;
            this.cboSale.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSale.Properties.NullText = "";
            this.cboSale.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboSale.Size = new System.Drawing.Size(240, 22);
            this.cboSale.TabIndex = 2;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.searchLookUpEdit1View.DetailHeight = 444;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ID";
            this.gridColumn3.FieldName = "ID";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 88;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn4.Caption = "Mã";
            this.gridColumn4.FieldName = "Code";
            this.gridColumn4.MinWidth = 23;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 88;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn5.Caption = "Tên";
            this.gridColumn5.FieldName = "FullName";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 205;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(840, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 16);
            this.label10.TabIndex = 138;
            this.label10.Text = "SĐT";
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContactPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPhone.Location = new System.Drawing.Point(932, 85);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(171, 22);
            this.txtContactPhone.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1109, 88);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 16);
            this.label17.TabIndex = 130;
            this.label17.Text = "Ngày báo giá";
            // 
            // txtContactName
            // 
            this.txtContactName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactName.Location = new System.Drawing.Point(932, 55);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(171, 22);
            this.txtContactName.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(840, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 139;
            this.label7.Text = "Email";
            // 
            // dteQuotationDate
            // 
            this.dteQuotationDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dteQuotationDate.EditValue = null;
            this.dteQuotationDate.Location = new System.Drawing.Point(1242, 85);
            this.dteQuotationDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dteQuotationDate.Name = "dteQuotationDate";
            this.dteQuotationDate.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteQuotationDate.Properties.Appearance.Options.UseFont = true;
            this.dteQuotationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteQuotationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteQuotationDate.Size = new System.Drawing.Size(113, 22);
            this.dteQuotationDate.TabIndex = 10;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BeepOnError = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "0:n0";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatString = "0:n0";
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "n");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.UseMaskAsDisplayFormat = true;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(840, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 16);
            this.label13.TabIndex = 137;
            this.label13.Text = "Người liên hệ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(342, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 131;
            this.label3.Text = "Khách hàng";
            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContactEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactEmail.Location = new System.Drawing.Point(932, 117);
            this.txtContactEmail.Name = "txtContactEmail";
            this.txtContactEmail.Size = new System.Drawing.Size(171, 22);
            this.txtContactEmail.TabIndex = 8;
            // 
            // cboCustomer
            // 
            this.cboCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCustomer.EditValue = "";
            this.cboCustomer.Location = new System.Drawing.Point(488, 55);
            this.cboCustomer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomer.Properties.Appearance.Options.UseFont = true;
            this.cboCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCustomer.Properties.NullText = "";
            this.cboCustomer.Properties.PopupView = this.gridView1;
            this.cboCustomer.Size = new System.Drawing.Size(345, 22);
            this.cboCustomer.TabIndex = 3;
            this.cboCustomer.EditValueChanged += new System.EventHandler(this.cboCustomer_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.DetailHeight = 444;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ID";
            this.gridColumn6.FieldName = "ID";
            this.gridColumn6.MinWidth = 23;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Width = 88;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn7.Caption = "Mã";
            this.gridColumn7.FieldName = "CustomerCode";
            this.gridColumn7.MinWidth = 23;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 88;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn8.Caption = "Tên";
            this.gridColumn8.FieldName = "CustomerName";
            this.gridColumn8.MinWidth = 23;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 205;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 132;
            this.label4.Text = "Dự án";
            // 
            // cboProject
            // 
            this.cboProject.EditValue = "";
            this.cboProject.Location = new System.Drawing.Point(96, 85);
            this.cboProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboProject.Name = "cboProject";
            this.cboProject.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProject.Properties.Appearance.Options.UseFont = true;
            this.cboProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProject.Properties.NullText = "";
            this.cboProject.Properties.PopupView = this.grvProject;
            this.cboProject.Size = new System.Drawing.Size(240, 22);
            this.cboProject.TabIndex = 1;
            // 
            // grvProject
            // 
            this.grvProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.colCustomerID});
            this.grvProject.DetailHeight = 444;
            this.grvProject.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvProject.Name = "grvProject";
            this.grvProject.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvProject.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "ID";
            this.gridColumn9.FieldName = "ID";
            this.gridColumn9.MinWidth = 23;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 88;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.Caption = "Mã";
            this.gridColumn10.FieldName = "ProjectCode";
            this.gridColumn10.MinWidth = 23;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 88;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn11.Caption = "Tên";
            this.gridColumn11.FieldName = "ProjectName";
            this.gridColumn11.MinWidth = 23;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 205;
            // 
            // colCustomerID
            // 
            this.colCustomerID.Caption = "gridColumn1";
            this.colCustomerID.FieldName = "CustomerID";
            this.colCustomerID.MinWidth = 23;
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.Width = 88;
            // 
            // TreeData
            // 
            this.TreeData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.TreeData.Appearance.HeaderPanel.Options.UseFont = true;
            this.TreeData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.TreeData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.TreeData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TreeData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TreeData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TreeData.ColumnPanelRowHeight = 40;
            this.TreeData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDetailID,
            this.colSTT,
            this.colMaker,
            this.colProductName,
            this.colProductCode,
            this.colQuantity,
            this.colUnit,
            this.colUnitImportPriceVND,
            this.colTotalImportPriceVND,
            this.colTradePriceDetailID});
            this.TreeData.CustomizationFormBounds = new System.Drawing.Rectangle(1629, 473, 266, 452);
            this.TreeData.FixedLineWidth = 4;
            this.TreeData.HorzScrollStep = 4;
            this.TreeData.Location = new System.Drawing.Point(11, 149);
            this.TreeData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TreeData.MinWidth = 31;
            this.TreeData.Name = "TreeData";
            this.TreeData.OptionsBehavior.PopulateServiceColumns = true;
            this.TreeData.OptionsCustomization.AllowSort = false;
            this.TreeData.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.TreeData.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
            this.TreeData.OptionsSelection.MultiSelect = true;
            this.TreeData.OptionsView.ShowSummaryFooter = true;
            this.TreeData.Size = new System.Drawing.Size(1343, 473);
            this.TreeData.TabIndex = 12;
            this.TreeData.TreeLevelWidth = 28;
            this.TreeData.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.TreeData_CellValueChanged);
            // 
            // colDetailID
            // 
            this.colDetailID.Caption = "ID";
            this.colDetailID.FieldName = "ID";
            this.colDetailID.MinWidth = 31;
            this.colDetailID.Name = "colDetailID";
            this.colDetailID.OptionsFilter.AllowFilter = false;
            this.colDetailID.Width = 117;
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.FieldNameSort = "STT";
            this.colSTT.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowSort = false;
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 50;
            // 
            // colMaker
            // 
            this.colMaker.Caption = "Cụm";
            this.colMaker.FieldName = "Maker";
            this.colMaker.MinWidth = 31;
            this.colMaker.Name = "colMaker";
            this.colMaker.OptionsColumn.AllowMove = false;
            this.colMaker.OptionsColumn.AllowSort = false;
            this.colMaker.Visible = true;
            this.colMaker.VisibleIndex = 2;
            this.colMaker.Width = 94;
            // 
            // colProductName
            // 
            this.colProductName.Caption = "Tên sản phẩm";
            this.colProductName.FieldName = "ProductName";
            this.colProductName.MinWidth = 31;
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowSort = false;
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 1;
            this.colProductName.Width = 415;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Mã báo khách";
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.MinWidth = 31;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowSort = false;
            this.colProductCode.Width = 248;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Số lượng";
            this.colQuantity.ColumnEdit = this.repositoryItemTextEdit1;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.MinWidth = 23;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowSort = false;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            this.colQuantity.Width = 105;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "ĐVT";
            this.colUnit.FieldName = "Unit";
            this.colUnit.MinWidth = 31;
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowSort = false;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 4;
            this.colUnit.Width = 101;
            // 
            // colUnitImportPriceVND
            // 
            this.colUnitImportPriceVND.AllNodesSummary = true;
            this.colUnitImportPriceVND.Caption = "Đơn giá nhập chưa chi phí (VND)";
            this.colUnitImportPriceVND.FieldName = "UnitImportPriceVND";
            this.colUnitImportPriceVND.Format.FormatString = "N0";
            this.colUnitImportPriceVND.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUnitImportPriceVND.MinWidth = 31;
            this.colUnitImportPriceVND.Name = "colUnitImportPriceVND";
            this.colUnitImportPriceVND.OptionsColumn.AllowSort = false;
            this.colUnitImportPriceVND.OptionsColumn.ReadOnly = true;
            this.colUnitImportPriceVND.RowFooterSummary = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colUnitImportPriceVND.RowFooterSummaryStrFormat = "{0:n0}";
            this.colUnitImportPriceVND.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colUnitImportPriceVND.SummaryFooterStrFormat = "{0:n0}";
            this.colUnitImportPriceVND.Visible = true;
            this.colUnitImportPriceVND.VisibleIndex = 5;
            this.colUnitImportPriceVND.Width = 250;
            // 
            // colTotalImportPriceVND
            // 
            this.colTotalImportPriceVND.AllNodesSummary = true;
            this.colTotalImportPriceVND.Caption = "Tổng giá nhập chưa Chi phí (VND)";
            this.colTotalImportPriceVND.FieldName = "TotalImportPriceVND";
            this.colTotalImportPriceVND.Format.FormatString = "N0";
            this.colTotalImportPriceVND.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalImportPriceVND.MinWidth = 31;
            this.colTotalImportPriceVND.Name = "colTotalImportPriceVND";
            this.colTotalImportPriceVND.OptionsColumn.AllowSort = false;
            this.colTotalImportPriceVND.OptionsColumn.ReadOnly = true;
            this.colTotalImportPriceVND.RowFooterSummary = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colTotalImportPriceVND.RowFooterSummaryStrFormat = "{0:n0}";
            this.colTotalImportPriceVND.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colTotalImportPriceVND.SummaryFooterStrFormat = "{0:n0}";
            this.colTotalImportPriceVND.Visible = true;
            this.colTotalImportPriceVND.VisibleIndex = 6;
            this.colTotalImportPriceVND.Width = 267;
            // 
            // colTradePriceDetailID
            // 
            this.colTradePriceDetailID.FieldName = "TradePriceDetailID";
            this.colTradePriceDetailID.MinWidth = 23;
            this.colTradePriceDetailID.Name = "colTradePriceDetailID";
            this.colTradePriceDetailID.Width = 88;
            // 
            // btnAddTermCondition
            // 
            this.btnAddTermCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTermCondition.AutoSize = true;
            this.btnAddTermCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTermCondition.Image = global::Forms.Properties.Resources.add_16x16;
            this.btnAddTermCondition.Location = new System.Drawing.Point(811, 117);
            this.btnAddTermCondition.Name = "btnAddTermCondition";
            this.btnAddTermCondition.Size = new System.Drawing.Size(22, 22);
            this.btnAddTermCondition.TabIndex = 148;
            this.btnAddTermCondition.UseVisualStyleBackColor = true;
            this.btnAddTermCondition.Click += new System.EventHandler(this.btnAddTermCondition_Click);
            // 
            // frmQuotationSaleDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 633);
            this.Controls.Add(this.btnAddTermCondition);
            this.Controls.Add(this.TreeData);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTermCondition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDeliveryPeriod);
            this.Controls.Add(this.txtCustomerAddress);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtTotalPrice);
            this.Controls.Add(this.cboSale);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtContactName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dteQuotationDate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtContactEmail);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboProject);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmQuotationSaleDetail";
            this.Text = "CHI TIẾT BÁO GIÁ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmQuotationSaleDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboProduct)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTermCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteQuotationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteQuotationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TreeData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cboTermCondition;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeliveryPeriod;
        private System.Windows.Forms.TextBox txtCustomerAddress;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label26;
        private DevExpress.XtraEditors.TextEdit txtTotalPrice;
        private DevExpress.XtraEditors.SearchLookUpEdit cboSale;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtContactPhone;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.DateEdit dteQuotationDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContactEmail;
        private DevExpress.XtraEditors.SearchLookUpEdit cboCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SearchLookUpEdit cboProject;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProject;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraTreeList.TreeList TreeData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDetailID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSTT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMaker;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProductName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProductCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colQuantity;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUnit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUnitImportPriceVND;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalImportPriceVND;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTradePriceDetailID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private System.Windows.Forms.Button btnAddTermCondition;
    }
}