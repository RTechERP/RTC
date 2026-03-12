
namespace BMS

{
    partial class frmCurrency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCurrency));
            this.grdCurrency = new DevExpress.XtraGrid.GridControl();
            this.grvCurrency = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameEng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameViet = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateExpire = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyRateOfficialQuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateExpriedOfficialQuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyRateUnofficialQuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateExpriedUnofficialQuota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.grdCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCurrency)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdCurrency
            // 
            this.grdCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCurrency.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdCurrency.Location = new System.Drawing.Point(0, 44);
            this.grdCurrency.MainView = this.grvCurrency;
            this.grdCurrency.Margin = new System.Windows.Forms.Padding(2);
            this.grdCurrency.Name = "grdCurrency";
            this.grdCurrency.Size = new System.Drawing.Size(1295, 615);
            this.grdCurrency.TabIndex = 19;
            this.grdCurrency.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCurrency});
            this.grdCurrency.DoubleClick += new System.EventHandler(this.btnEdit_Click);
            // 
            // grvCurrency
            // 
            this.grvCurrency.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvCurrency.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvCurrency.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvCurrency.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvCurrency.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvCurrency.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvCurrency.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvCurrency.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvCurrency.Appearance.Row.Options.UseTextOptions = true;
            this.grvCurrency.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvCurrency.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvCurrency.ColumnPanelRowHeight = 41;
            this.grvCurrency.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCode,
            this.colNameEng,
            this.colNameViet,
            this.colMinUnit,
            this.colCreatedBy,
            this.colRate,
            this.colNote,
            this.colCreatedDate,
            this.colUpdatedBy,
            this.colUpdatedDate,
            this.colDateStart,
            this.colDateExpire,
            this.colCurrencyRateOfficialQuota,
            this.colDateExpriedOfficialQuota,
            this.colCurrencyRateUnofficialQuota,
            this.colDateExpriedUnofficialQuota});
            this.grvCurrency.DetailHeight = 284;
            this.grvCurrency.GridControl = this.grdCurrency;
            this.grvCurrency.Name = "grvCurrency";
            this.grvCurrency.OptionsBehavior.Editable = false;
            this.grvCurrency.OptionsBehavior.ReadOnly = true;
            this.grvCurrency.OptionsFind.AlwaysVisible = true;
            this.grvCurrency.OptionsPrint.PrintHeader = false;
            this.grvCurrency.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 20F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.Caption = "gridColumn1";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 37;
            this.colID.Name = "colID";
            this.colID.Width = 70;
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Mã tiền tệ";
            this.colCode.FieldName = "Code";
            this.colCode.MinWidth = 15;
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 79;
            // 
            // colNameEng
            // 
            this.colNameEng.AppearanceCell.Options.UseTextOptions = true;
            this.colNameEng.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colNameEng.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNameEng.AppearanceHeader.Options.UseFont = true;
            this.colNameEng.AppearanceHeader.Options.UseForeColor = true;
            this.colNameEng.AppearanceHeader.Options.UseTextOptions = true;
            this.colNameEng.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNameEng.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNameEng.Caption = "Tên tiếng Anh";
            this.colNameEng.FieldName = "NameEnglist";
            this.colNameEng.MinWidth = 15;
            this.colNameEng.Name = "colNameEng";
            this.colNameEng.OptionsColumn.AllowEdit = false;
            this.colNameEng.Visible = true;
            this.colNameEng.VisibleIndex = 1;
            this.colNameEng.Width = 79;
            // 
            // colNameViet
            // 
            this.colNameViet.AppearanceCell.Options.UseTextOptions = true;
            this.colNameViet.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colNameViet.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNameViet.AppearanceHeader.Options.UseFont = true;
            this.colNameViet.AppearanceHeader.Options.UseForeColor = true;
            this.colNameViet.AppearanceHeader.Options.UseTextOptions = true;
            this.colNameViet.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNameViet.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNameViet.Caption = "Tên tiếng Việt";
            this.colNameViet.FieldName = "NameVietNamese";
            this.colNameViet.MinWidth = 15;
            this.colNameViet.Name = "colNameViet";
            this.colNameViet.OptionsColumn.AllowEdit = false;
            this.colNameViet.Visible = true;
            this.colNameViet.VisibleIndex = 2;
            this.colNameViet.Width = 79;
            // 
            // colMinUnit
            // 
            this.colMinUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colMinUnit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMinUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.colMinUnit.AppearanceHeader.Options.UseFont = true;
            this.colMinUnit.AppearanceHeader.Options.UseForeColor = true;
            this.colMinUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colMinUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMinUnit.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMinUnit.Caption = "Đơn vị nhỏ nhất";
            this.colMinUnit.FieldName = "MinUnit";
            this.colMinUnit.MinWidth = 15;
            this.colMinUnit.Name = "colMinUnit";
            this.colMinUnit.OptionsColumn.AllowEdit = false;
            this.colMinUnit.Visible = true;
            this.colMinUnit.VisibleIndex = 3;
            this.colMinUnit.Width = 73;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreatedBy.AppearanceHeader.Options.UseFont = true;
            this.colCreatedBy.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedBy.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedBy.Caption = "Người tạo";
            this.colCreatedBy.FieldName = "CreatedBy";
            this.colCreatedBy.MinWidth = 15;
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.OptionsColumn.AllowEdit = false;
            this.colCreatedBy.Width = 79;
            // 
            // colRate
            // 
            this.colRate.AppearanceCell.Options.UseTextOptions = true;
            this.colRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colRate.AppearanceHeader.Options.UseFont = true;
            this.colRate.AppearanceHeader.Options.UseForeColor = true;
            this.colRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRate.Caption = "Tỉ giá";
            this.colRate.FieldName = "CurrencyRate";
            this.colRate.MinWidth = 15;
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.AllowEdit = false;
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 4;
            this.colRate.Width = 79;
            // 
            // colNote
            // 
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.FieldName = "Note";
            this.colNote.MinWidth = 15;
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 11;
            this.colNote.Width = 79;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreatedDate.AppearanceHeader.Options.UseFont = true;
            this.colCreatedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.MinWidth = 15;
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.OptionsColumn.AllowEdit = false;
            this.colCreatedDate.Width = 79;
            // 
            // colUpdatedBy
            // 
            this.colUpdatedBy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdatedBy.AppearanceHeader.Options.UseFont = true;
            this.colUpdatedBy.AppearanceHeader.Options.UseForeColor = true;
            this.colUpdatedBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdatedBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdatedBy.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedBy.Caption = "Người cập nhật";
            this.colUpdatedBy.FieldName = "UpdatedBy";
            this.colUpdatedBy.MinWidth = 15;
            this.colUpdatedBy.Name = "colUpdatedBy";
            this.colUpdatedBy.OptionsColumn.AllowEdit = false;
            this.colUpdatedBy.Width = 79;
            // 
            // colUpdatedDate
            // 
            this.colUpdatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdatedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdatedDate.AppearanceHeader.Options.UseFont = true;
            this.colUpdatedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colUpdatedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdatedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedDate.Caption = "Ngày cập nhật";
            this.colUpdatedDate.FieldName = "UpdatedDate";
            this.colUpdatedDate.MinWidth = 15;
            this.colUpdatedDate.Name = "colUpdatedDate";
            this.colUpdatedDate.OptionsColumn.AllowEdit = false;
            this.colUpdatedDate.Width = 79;
            // 
            // colDateStart
            // 
            this.colDateStart.AppearanceCell.Options.UseTextOptions = true;
            this.colDateStart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStart.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDateStart.AppearanceHeader.Options.UseFont = true;
            this.colDateStart.AppearanceHeader.Options.UseForeColor = true;
            this.colDateStart.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStart.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateStart.Caption = "Ngày bắt đầu";
            this.colDateStart.FieldName = "DateStart";
            this.colDateStart.MinWidth = 15;
            this.colDateStart.Name = "colDateStart";
            this.colDateStart.OptionsColumn.AllowEdit = false;
            this.colDateStart.Visible = true;
            this.colDateStart.VisibleIndex = 5;
            this.colDateStart.Width = 79;
            // 
            // colDateExpire
            // 
            this.colDateExpire.AppearanceCell.Options.UseTextOptions = true;
            this.colDateExpire.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateExpire.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDateExpire.AppearanceHeader.Options.UseFont = true;
            this.colDateExpire.AppearanceHeader.Options.UseForeColor = true;
            this.colDateExpire.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateExpire.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateExpire.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateExpire.Caption = "Ngày hết hạn";
            this.colDateExpire.FieldName = "DateExpried";
            this.colDateExpire.MinWidth = 15;
            this.colDateExpire.Name = "colDateExpire";
            this.colDateExpire.OptionsColumn.AllowEdit = false;
            this.colDateExpire.Visible = true;
            this.colDateExpire.VisibleIndex = 6;
            this.colDateExpire.Width = 101;
            // 
            // colCurrencyRateOfficialQuota
            // 
            this.colCurrencyRateOfficialQuota.Caption = "Tỷ giá chính ngạch";
            this.colCurrencyRateOfficialQuota.DisplayFormat.FormatString = "n2";
            this.colCurrencyRateOfficialQuota.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrencyRateOfficialQuota.FieldName = "CurrencyRateOfficialQuota";
            this.colCurrencyRateOfficialQuota.MinWidth = 19;
            this.colCurrencyRateOfficialQuota.Name = "colCurrencyRateOfficialQuota";
            this.colCurrencyRateOfficialQuota.Visible = true;
            this.colCurrencyRateOfficialQuota.VisibleIndex = 7;
            // 
            // colDateExpriedOfficialQuota
            // 
            this.colDateExpriedOfficialQuota.Caption = "Ngày hết hạn TGCN";
            this.colDateExpriedOfficialQuota.FieldName = "DateExpriedOfficialQuota";
            this.colDateExpriedOfficialQuota.MinWidth = 19;
            this.colDateExpriedOfficialQuota.Name = "colDateExpriedOfficialQuota";
            this.colDateExpriedOfficialQuota.Visible = true;
            this.colDateExpriedOfficialQuota.VisibleIndex = 8;
            // 
            // colCurrencyRateUnofficialQuota
            // 
            this.colCurrencyRateUnofficialQuota.Caption = "Tỷ giá tiểu ngạch";
            this.colCurrencyRateUnofficialQuota.DisplayFormat.FormatString = "n2";
            this.colCurrencyRateUnofficialQuota.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrencyRateUnofficialQuota.FieldName = "CurrencyRateUnofficialQuota";
            this.colCurrencyRateUnofficialQuota.MinWidth = 19;
            this.colCurrencyRateUnofficialQuota.Name = "colCurrencyRateUnofficialQuota";
            this.colCurrencyRateUnofficialQuota.Visible = true;
            this.colCurrencyRateUnofficialQuota.VisibleIndex = 9;
            // 
            // colDateExpriedUnofficialQuota
            // 
            this.colDateExpriedUnofficialQuota.Caption = "Ngày hết hạn TGTN";
            this.colDateExpriedUnofficialQuota.FieldName = "DateExpriedUnofficialQuota";
            this.colDateExpriedUnofficialQuota.MinWidth = 19;
            this.colDateExpriedUnofficialQuota.Name = "colDateExpriedUnofficialQuota";
            this.colDateExpriedUnofficialQuota.Visible = true;
            this.colDateExpriedUnofficialQuota.VisibleIndex = 10;
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator3,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator4});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1295, 44);
            this.mnuMenu.TabIndex = 25;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 41);
            this.btnAdd.Tag = "frmCurrency_Add";
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.ToolTipText = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 41);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(128, 41);
            this.btnEdit.Tag = "frmCurrency_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.ToolTipText = "Sửa";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(128, 41);
            this.btnDelete.Tag = "frmCurrency_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 41);
            // 
            // frmCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 659);
            this.Controls.Add(this.grdCurrency);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmCurrency";
            this.Text = "QUẢN LÍ TIỀN TỆ";
            this.Load += new System.EventHandler(this.frmCurrency_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCurrency)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdCurrency;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colNameEng;
        private DevExpress.XtraGrid.Columns.GridColumn colNameViet;
        private DevExpress.XtraGrid.Columns.GridColumn colMinUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDateStart;
        private DevExpress.XtraGrid.Columns.GridColumn colDateExpire;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedBy;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyRateOfficialQuota;
        private DevExpress.XtraGrid.Columns.GridColumn colDateExpriedOfficialQuota;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyRateUnofficialQuota;
        private DevExpress.XtraGrid.Columns.GridColumn colDateExpriedUnofficialQuota;
    }
}