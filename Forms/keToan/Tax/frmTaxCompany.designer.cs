
namespace BMS
{
    partial class frmTaxCompany
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
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFullName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colTaxCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAddress = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPhoneNumber = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDirector = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPosition = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBuyerEnglish = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAddressBuyerEnglish = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLegalRepresentativeEnglish = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBuyerVietnamese = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAddressBuyerVienamese = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.TaxVietnamese = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.txtEditStatus = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDel,
            this.toolStripSeparator1,
            this.btnRefresh});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1174, 55);
            this.mnuMenu.TabIndex = 121;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Forms.Properties.Resources.AddItem_32x32;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(10, 1, 0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(52, 54);
            this.btnAdd.Tag = "frmTaxCompany_Add";
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = global::Forms.Properties.Resources.Edit_32x32;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(41, 54);
            this.btnEdit.Tag = "frmTaxCompany_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Image = global::Forms.Properties.Resources.delete_32x32;
            this.btnDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(40, 54);
            this.btnDel.Tag = "frmTaxCompany_Del";
            this.btnDel.Text = "Xóa";
            this.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 55);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtEditStatus,
            this.repositoryItemCheckEdit1,
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1174, 519);
            this.grdData.TabIndex = 123;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            // 
            // grvData
            // 
            this.grvData.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.BandPanel.Options.UseFont = true;
            this.grvData.Appearance.BandPanel.Options.UseForeColor = true;
            this.grvData.Appearance.BandPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.BandPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
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
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand4,
            this.gridBand1,
            this.gridBand2,
            this.gridBand3});
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colID,
            this.colName,
            this.colCode,
            this.colTaxCode,
            this.colAddress,
            this.colPhoneNumber,
            this.colDirector,
            this.colPosition,
            this.colFullName,
            this.colBuyerEnglish,
            this.colAddressBuyerEnglish,
            this.colLegalRepresentativeEnglish,
            this.colBuyerVietnamese,
            this.colAddressBuyerVienamese,
            this.TaxVietnamese});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand4
            // 
            this.gridBand4.Columns.Add(this.colCode);
            this.gridBand4.Columns.Add(this.colName);
            this.gridBand4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 0;
            this.gridBand4.Width = 226;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã công ty";
            this.colCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.Width = 116;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colName
            // 
            this.colName.Caption = "Tên công ty";
            this.colName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.Width = 110;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "THÔNG TIN CHUNG";
            this.gridBand1.Columns.Add(this.colID);
            this.gridBand1.Columns.Add(this.colFullName);
            this.gridBand1.Columns.Add(this.colTaxCode);
            this.gridBand1.Columns.Add(this.colAddress);
            this.gridBand1.Columns.Add(this.colPhoneNumber);
            this.gridBand1.Columns.Add(this.colDirector);
            this.gridBand1.Columns.Add(this.colPosition);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 1;
            this.gridBand1.Width = 1308;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colFullName
            // 
            this.colFullName.Caption = "Tên đầy đủ";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.Width = 437;
            // 
            // colTaxCode
            // 
            this.colTaxCode.Caption = "Mã số thuế";
            this.colTaxCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTaxCode.FieldName = "TaxCode";
            this.colTaxCode.Name = "colTaxCode";
            this.colTaxCode.Visible = true;
            this.colTaxCode.Width = 199;
            // 
            // colAddress
            // 
            this.colAddress.Caption = "Trụ sở chính";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddress.FieldName = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Visible = true;
            this.colAddress.Width = 299;
            // 
            // colPhoneNumber
            // 
            this.colPhoneNumber.Caption = "Điện thoại";
            this.colPhoneNumber.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPhoneNumber.FieldName = "PhoneNumber";
            this.colPhoneNumber.Name = "colPhoneNumber";
            this.colPhoneNumber.Visible = true;
            this.colPhoneNumber.Width = 123;
            // 
            // colDirector
            // 
            this.colDirector.Caption = "Giám đốc";
            this.colDirector.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDirector.FieldName = "Director";
            this.colDirector.Name = "colDirector";
            this.colDirector.Visible = true;
            this.colDirector.Width = 118;
            // 
            // colPosition
            // 
            this.colPosition.Caption = "Chức vụ";
            this.colPosition.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPosition.FieldName = "Position";
            this.colPosition.Name = "colPosition";
            this.colPosition.Visible = true;
            this.colPosition.Width = 132;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "THÔNG TIN TIẾNG ANH";
            this.gridBand2.Columns.Add(this.colBuyerEnglish);
            this.gridBand2.Columns.Add(this.colAddressBuyerEnglish);
            this.gridBand2.Columns.Add(this.colLegalRepresentativeEnglish);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 2;
            this.gridBand2.Width = 912;
            // 
            // colBuyerEnglish
            // 
            this.colBuyerEnglish.Caption = "Buyer";
            this.colBuyerEnglish.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBuyerEnglish.FieldName = "BuyerEnglish";
            this.colBuyerEnglish.Name = "colBuyerEnglish";
            this.colBuyerEnglish.Visible = true;
            this.colBuyerEnglish.Width = 240;
            // 
            // colAddressBuyerEnglish
            // 
            this.colAddressBuyerEnglish.Caption = "AddressBuyer";
            this.colAddressBuyerEnglish.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddressBuyerEnglish.FieldName = "AddressBuyerEnglish";
            this.colAddressBuyerEnglish.Name = "colAddressBuyerEnglish";
            this.colAddressBuyerEnglish.Visible = true;
            this.colAddressBuyerEnglish.Width = 483;
            // 
            // colLegalRepresentativeEnglish
            // 
            this.colLegalRepresentativeEnglish.Caption = "Legal Representative";
            this.colLegalRepresentativeEnglish.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colLegalRepresentativeEnglish.FieldName = "LegalRepresentativeEnglish";
            this.colLegalRepresentativeEnglish.Name = "colLegalRepresentativeEnglish";
            this.colLegalRepresentativeEnglish.Visible = true;
            this.colLegalRepresentativeEnglish.Width = 189;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "THÔNG TIN TIẾNG VIỆT";
            this.gridBand3.Columns.Add(this.colBuyerVietnamese);
            this.gridBand3.Columns.Add(this.colAddressBuyerVienamese);
            this.gridBand3.Columns.Add(this.TaxVietnamese);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 3;
            this.gridBand3.Width = 876;
            // 
            // colBuyerVietnamese
            // 
            this.colBuyerVietnamese.Caption = "Buyer";
            this.colBuyerVietnamese.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBuyerVietnamese.FieldName = "BuyerVietnamese";
            this.colBuyerVietnamese.Name = "colBuyerVietnamese";
            this.colBuyerVietnamese.Visible = true;
            this.colBuyerVietnamese.Width = 244;
            // 
            // colAddressBuyerVienamese
            // 
            this.colAddressBuyerVienamese.Caption = "Address Buyer";
            this.colAddressBuyerVienamese.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAddressBuyerVienamese.FieldName = "AddressBuyerVienamese";
            this.colAddressBuyerVienamese.Name = "colAddressBuyerVienamese";
            this.colAddressBuyerVienamese.Visible = true;
            this.colAddressBuyerVienamese.Width = 396;
            // 
            // TaxVietnamese
            // 
            this.TaxVietnamese.Caption = "Tax";
            this.TaxVietnamese.ColumnEdit = this.repositoryItemMemoEdit1;
            this.TaxVietnamese.FieldName = "TaxVietnamese";
            this.TaxVietnamese.Name = "TaxVietnamese";
            this.TaxVietnamese.Visible = true;
            this.TaxVietnamese.Width = 236;
            // 
            // txtEditStatus
            // 
            this.txtEditStatus.Appearance.Options.UseTextOptions = true;
            this.txtEditStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtEditStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtEditStatus.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.txtEditStatus.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtEditStatus.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtEditStatus.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtEditStatus.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.txtEditStatus.AppearanceFocused.Options.UseTextOptions = true;
            this.txtEditStatus.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtEditStatus.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtEditStatus.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.txtEditStatus.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtEditStatus.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtEditStatus.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtEditStatus.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.txtEditStatus.AutoHeight = false;
            this.txtEditStatus.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtEditStatus.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtEditStatus.Name = "txtEditStatus";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::Forms.Properties.Resources.refresh2_32x32;
            this.btnRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(74, 54);
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmTaxCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 574);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmTaxCompany";
            this.Text = "CÔNG TY";
            this.Load += new System.EventHandler(this.frmTaxCompany_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtEditStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTaxCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddress;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPhoneNumber;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDirector;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPosition;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFullName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBuyerEnglish;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddressBuyerEnglish;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLegalRepresentativeEnglish;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBuyerVietnamese;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAddressBuyerVienamese;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TaxVietnamese;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private System.Windows.Forms.ToolStripButton btnRefresh;
    }
}