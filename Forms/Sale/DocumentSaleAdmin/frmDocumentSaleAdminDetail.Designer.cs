namespace BMS
{
    partial class frmDocumentSaleAdminDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentSaleAdminDetail));
            this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDepartCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.dtDocumentEffetive = new System.Windows.Forms.DateTimePicker();
            this.dtDatePromulqate = new System.Windows.Forms.DateTimePicker();
            this.cbxDocumentType = new System.Windows.Forms.ComboBox();
            this.txtNameDocument = new DevExpress.XtraEditors.TextEdit();
            this.txtCodeDocument = new DevExpress.XtraEditors.TextEdit();
            this.lblNgayHieuLuc = new DevExpress.XtraEditors.LabelControl();
            this.lblNgayBanHanh = new DevExpress.XtraEditors.LabelControl();
            this.lblTenVanBan = new DevExpress.XtraEditors.LabelControl();
            this.lblMaVanBan = new DevExpress.XtraEditors.LabelControl();
            this.lblLoaiVanBan = new DevExpress.XtraEditors.LabelControl();
            this.lblSTT = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnSaveAndClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnSaveAndNew = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameDocument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeDocument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDepartment
            // 
            this.cboDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDepartment.Location = new System.Drawing.Point(137, 64);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDepartment.Size = new System.Drawing.Size(172, 26);
            this.cboDepartment.TabIndex = 23;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.searchLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.Row.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 50;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDepartCode,
            this.colDepartName});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colDepartCode
            // 
            this.colDepartCode.Caption = "Mã phòng ban";
            this.colDepartCode.FieldName = "Code";
            this.colDepartCode.Name = "colDepartCode";
            this.colDepartCode.Visible = true;
            this.colDepartCode.VisibleIndex = 0;
            this.colDepartCode.Width = 408;
            // 
            // colDepartName
            // 
            this.colDepartName.Caption = "Tên phòng ban";
            this.colDepartName.FieldName = "Name";
            this.colDepartName.Name = "colDepartName";
            this.colDepartName.Visible = true;
            this.colDepartName.VisibleIndex = 1;
            this.colDepartName.Width = 1077;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(42, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 20);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "Phòng ban";
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtSTT.Location = new System.Drawing.Point(525, 98);
            this.txtSTT.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtSTT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(96, 26);
            this.txtSTT.TabIndex = 21;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSTT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dtDocumentEffetive
            // 
            this.dtDocumentEffetive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDocumentEffetive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDocumentEffetive.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDocumentEffetive.Location = new System.Drawing.Point(486, 160);
            this.dtDocumentEffetive.Name = "dtDocumentEffetive";
            this.dtDocumentEffetive.Size = new System.Drawing.Size(135, 26);
            this.dtDocumentEffetive.TabIndex = 20;
            // 
            // dtDatePromulqate
            // 
            this.dtDatePromulqate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDatePromulqate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDatePromulqate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDatePromulqate.Location = new System.Drawing.Point(137, 160);
            this.dtDatePromulqate.Name = "dtDatePromulqate";
            this.dtDatePromulqate.Size = new System.Drawing.Size(243, 26);
            this.dtDatePromulqate.TabIndex = 19;
            // 
            // cbxDocumentType
            // 
            this.cbxDocumentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDocumentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDocumentType.FormattingEnabled = true;
            this.cbxDocumentType.Location = new System.Drawing.Point(423, 63);
            this.cbxDocumentType.Name = "cbxDocumentType";
            this.cbxDocumentType.Size = new System.Drawing.Size(198, 28);
            this.cbxDocumentType.TabIndex = 10;
            // 
            // txtNameDocument
            // 
            this.txtNameDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameDocument.Location = new System.Drawing.Point(137, 128);
            this.txtNameDocument.Name = "txtNameDocument";
            this.txtNameDocument.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameDocument.Properties.Appearance.Options.UseFont = true;
            this.txtNameDocument.Size = new System.Drawing.Size(484, 26);
            this.txtNameDocument.TabIndex = 18;
            // 
            // txtCodeDocument
            // 
            this.txtCodeDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodeDocument.Location = new System.Drawing.Point(137, 96);
            this.txtCodeDocument.Name = "txtCodeDocument";
            this.txtCodeDocument.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeDocument.Properties.Appearance.Options.UseFont = true;
            this.txtCodeDocument.Size = new System.Drawing.Size(343, 26);
            this.txtCodeDocument.TabIndex = 17;
            // 
            // lblNgayHieuLuc
            // 
            this.lblNgayHieuLuc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNgayHieuLuc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayHieuLuc.Appearance.Options.UseFont = true;
            this.lblNgayHieuLuc.Location = new System.Drawing.Point(386, 164);
            this.lblNgayHieuLuc.Name = "lblNgayHieuLuc";
            this.lblNgayHieuLuc.Size = new System.Drawing.Size(94, 20);
            this.lblNgayHieuLuc.TabIndex = 11;
            this.lblNgayHieuLuc.Text = "Ngày hiệu lực";
            // 
            // lblNgayBanHanh
            // 
            this.lblNgayBanHanh.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayBanHanh.Appearance.Options.UseFont = true;
            this.lblNgayBanHanh.Location = new System.Drawing.Point(12, 163);
            this.lblNgayBanHanh.Name = "lblNgayBanHanh";
            this.lblNgayBanHanh.Size = new System.Drawing.Size(107, 20);
            this.lblNgayBanHanh.TabIndex = 12;
            this.lblNgayBanHanh.Text = "Ngày ban hành";
            // 
            // lblTenVanBan
            // 
            this.lblTenVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenVanBan.Appearance.Options.UseFont = true;
            this.lblTenVanBan.Location = new System.Drawing.Point(32, 131);
            this.lblTenVanBan.Name = "lblTenVanBan";
            this.lblTenVanBan.Size = new System.Drawing.Size(87, 20);
            this.lblTenVanBan.TabIndex = 13;
            this.lblTenVanBan.Text = "Tên văn bản";
            // 
            // lblMaVanBan
            // 
            this.lblMaVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaVanBan.Appearance.Options.UseFont = true;
            this.lblMaVanBan.Location = new System.Drawing.Point(37, 99);
            this.lblMaVanBan.Name = "lblMaVanBan";
            this.lblMaVanBan.Size = new System.Drawing.Size(82, 20);
            this.lblMaVanBan.TabIndex = 14;
            this.lblMaVanBan.Text = "Mã văn bản";
            // 
            // lblLoaiVanBan
            // 
            this.lblLoaiVanBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoaiVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiVanBan.Appearance.Options.UseFont = true;
            this.lblLoaiVanBan.Location = new System.Drawing.Point(315, 67);
            this.lblLoaiVanBan.Name = "lblLoaiVanBan";
            this.lblLoaiVanBan.Size = new System.Drawing.Size(90, 20);
            this.lblLoaiVanBan.TabIndex = 15;
            this.lblLoaiVanBan.Text = "Loại văn bản";
            // 
            // lblSTT
            // 
            this.lblSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSTT.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSTT.Appearance.Options.UseFont = true;
            this.lblSTT.Location = new System.Drawing.Point(486, 101);
            this.lblSTT.Name = "lblSTT";
            this.lblSTT.Size = new System.Drawing.Size(29, 20);
            this.lblSTT.TabIndex = 16;
            this.lblSTT.Text = "STT";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSaveAndNew,
            this.btnSaveAndClose});
            this.barManager1.MainMenu = this.bar3;
            this.barManager1.MaxItemId = 14;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.FloatLocation = new System.Drawing.Point(671, 121);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveAndClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveAndNew)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Main menu";
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Caption = "CẤT && ĐÓNG";
            this.btnSaveAndClose.Id = 12;
            this.btnSaveAndClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAndClose.ImageOptions.Image")));
            this.btnSaveAndClose.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSaveAndClose.ImageOptions.LargeImage")));
            this.btnSaveAndClose.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSaveAndClose.ItemAppearance.Normal.Options.UseFont = true;
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveAndClose_ItemClick);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Caption = "CẤT && THÊM MỚI";
            this.btnSaveAndNew.Id = 1;
            this.btnSaveAndNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAndNew.ImageOptions.Image")));
            this.btnSaveAndNew.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSaveAndNew.ImageOptions.LargeImage")));
            this.btnSaveAndNew.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndNew.ItemAppearance.Normal.Options.UseFont = true;
            this.btnSaveAndNew.Name = "btnSaveAndNew";
            this.btnSaveAndNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveAndNew_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(5);
            this.barDockControlTop.Size = new System.Drawing.Size(633, 56);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 196);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(5);
            this.barDockControlBottom.Size = new System.Drawing.Size(633, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 56);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(5);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 140);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(633, 56);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(5);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 140);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(125, 131);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(6, 20);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "*";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(125, 99);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(6, 20);
            this.labelControl2.TabIndex = 30;
            this.labelControl2.Text = "*";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(125, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(6, 20);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "*";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(411, 67);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(6, 20);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "*";
            // 
            // frmDocumentSaleAdminDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 196);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.cboDepartment);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtSTT);
            this.Controls.Add(this.dtDocumentEffetive);
            this.Controls.Add(this.dtDatePromulqate);
            this.Controls.Add(this.cbxDocumentType);
            this.Controls.Add(this.txtNameDocument);
            this.Controls.Add(this.txtCodeDocument);
            this.Controls.Add(this.lblNgayHieuLuc);
            this.Controls.Add(this.lblNgayBanHanh);
            this.Controls.Add(this.lblTenVanBan);
            this.Controls.Add(this.lblMaVanBan);
            this.Controls.Add(this.lblLoaiVanBan);
            this.Controls.Add(this.lblSTT);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmDocumentSaleAdminDetail";
            this.Text = "CHI TIẾT FORM BIỂU";
            this.Load += new System.EventHandler(this.frmDocumentSaleAdminDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameDocument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeDocument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private System.Windows.Forms.DateTimePicker dtDocumentEffetive;
        private System.Windows.Forms.DateTimePicker dtDatePromulqate;
        private System.Windows.Forms.ComboBox cbxDocumentType;
        private DevExpress.XtraEditors.TextEdit txtNameDocument;
        private DevExpress.XtraEditors.TextEdit txtCodeDocument;
        private DevExpress.XtraEditors.LabelControl lblNgayHieuLuc;
        private DevExpress.XtraEditors.LabelControl lblNgayBanHanh;
        private DevExpress.XtraEditors.LabelControl lblTenVanBan;
        private DevExpress.XtraEditors.LabelControl lblMaVanBan;
        private DevExpress.XtraEditors.LabelControl lblLoaiVanBan;
        private DevExpress.XtraEditors.LabelControl lblSTT;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarLargeButtonItem btnSaveAndClose;
        private DevExpress.XtraBars.BarLargeButtonItem btnSaveAndNew;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}