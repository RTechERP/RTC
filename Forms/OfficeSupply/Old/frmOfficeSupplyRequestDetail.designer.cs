
namespace BMS
{
    partial class frmOfficeSupplyRequestDetail
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
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblAsterisk5 = new System.Windows.Forms.Label();
            this.lblAsterisk7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantityReceive = new System.Windows.Forms.TextBox();
            this.lblQuantityReceive = new System.Windows.Forms.Label();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveAndReset = new System.Windows.Forms.ToolStripButton();
            this.lblOfficeSupply = new System.Windows.Forms.Label();
            this.cboOfficeSupply = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOSID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOSUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfficeSupplyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDateRequest = new System.Windows.Forms.Label();
            this.dtpDateRequest = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.chkExceedsLimit = new System.Windows.Forms.CheckBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.cboUserName = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboOfficeSupply.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(25, 64);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(89, 16);
            this.lblUserName.TabIndex = 69;
            this.lblUserName.Text = "Người đăng ký";
            // 
            // lblAsterisk5
            // 
            this.lblAsterisk5.AutoSize = true;
            this.lblAsterisk5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk5.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk5.Location = new System.Drawing.Point(116, 64);
            this.lblAsterisk5.Name = "lblAsterisk5";
            this.lblAsterisk5.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk5.TabIndex = 66;
            this.lblAsterisk5.Text = "(*)";
            // 
            // lblAsterisk7
            // 
            this.lblAsterisk7.AutoSize = true;
            this.lblAsterisk7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk7.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk7.Location = new System.Drawing.Point(92, 64);
            this.lblAsterisk7.Name = "lblAsterisk7";
            this.lblAsterisk7.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk7.TabIndex = 71;
            this.lblAsterisk7.Text = "(*)";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 52);
            this.btnSave.Tag = "frmOfficeSupplyRequest_New";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(142, 129);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(174, 24);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(56, 132);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(59, 16);
            this.lblQuantity.TabIndex = 61;
            this.lblQuantity.Text = "Số lượng";
            // 
            // txtQuantityReceive
            // 
            this.txtQuantityReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuantityReceive.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantityReceive.Location = new System.Drawing.Point(464, 129);
            this.txtQuantityReceive.Name = "txtQuantityReceive";
            this.txtQuantityReceive.Size = new System.Drawing.Size(187, 24);
            this.txtQuantityReceive.TabIndex = 4;
            this.txtQuantityReceive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
            // 
            // lblQuantityReceive
            // 
            this.lblQuantityReceive.AutoSize = true;
            this.lblQuantityReceive.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantityReceive.Location = new System.Drawing.Point(343, 132);
            this.lblQuantityReceive.Name = "lblQuantityReceive";
            this.lblQuantityReceive.Size = new System.Drawing.Size(120, 16);
            this.lblQuantityReceive.TabIndex = 60;
            this.lblQuantityReceive.Text = "Số lượng thực nhận";
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator1,
            this.btnSaveAndReset});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(668, 55);
            this.mnuMenu.TabIndex = 59;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveAndReset
            // 
            this.btnSaveAndReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndReset.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSaveAndReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAndReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAndReset.Name = "btnSaveAndReset";
            this.btnSaveAndReset.Size = new System.Drawing.Size(113, 52);
            this.btnSaveAndReset.Tag = "frmOfficeSupplyRequest_New";
            this.btnSaveAndReset.Text = "Cất && Thêm mới";
            this.btnSaveAndReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAndReset.Click += new System.EventHandler(this.btnSaveAndReset_Click);
            // 
            // lblOfficeSupply
            // 
            this.lblOfficeSupply.AutoSize = true;
            this.lblOfficeSupply.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfficeSupply.Location = new System.Drawing.Point(10, 99);
            this.lblOfficeSupply.Name = "lblOfficeSupply";
            this.lblOfficeSupply.Size = new System.Drawing.Size(105, 16);
            this.lblOfficeSupply.TabIndex = 58;
            this.lblOfficeSupply.Text = "Văn phòng phẩm";
            // 
            // cboOfficeSupply
            // 
            this.cboOfficeSupply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOfficeSupply.EditValue = "";
            this.cboOfficeSupply.Location = new System.Drawing.Point(142, 94);
            this.cboOfficeSupply.Margin = new System.Windows.Forms.Padding(8);
            this.cboOfficeSupply.Name = "cboOfficeSupply";
            this.cboOfficeSupply.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cboOfficeSupply.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOfficeSupply.Properties.Appearance.Options.UseFont = true;
            this.cboOfficeSupply.Properties.AutoHeight = false;
            this.cboOfficeSupply.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOfficeSupply.Properties.NullText = "";
            this.cboOfficeSupply.Properties.PopupFormMinSize = new System.Drawing.Size(515, 355);
            this.cboOfficeSupply.Properties.PopupFormSize = new System.Drawing.Size(515, 355);
            this.cboOfficeSupply.Properties.PopupView = this.gridView2;
            this.cboOfficeSupply.Size = new System.Drawing.Size(509, 28);
            this.cboOfficeSupply.TabIndex = 2;
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOSID,
            this.colOSUnitID,
            this.colOfficeSupplyName});
            this.gridView2.DetailHeight = 297;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colOSID
            // 
            this.colOSID.Caption = "OfficeSupplyID";
            this.colOSID.FieldName = "ID";
            this.colOSID.MinWidth = 37;
            this.colOSID.Name = "colOSID";
            this.colOSID.Width = 170;
            // 
            // colOSUnitID
            // 
            this.colOSUnitID.Caption = "OfficeSupplyUnitID";
            this.colOSUnitID.FieldName = "SupplyUnitID";
            this.colOSUnitID.MinWidth = 19;
            this.colOSUnitID.Name = "colOSUnitID";
            this.colOSUnitID.Width = 70;
            // 
            // colOfficeSupplyName
            // 
            this.colOfficeSupplyName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colOfficeSupplyName.AppearanceHeader.Options.UseFont = true;
            this.colOfficeSupplyName.Caption = "Văn phòng phẩm";
            this.colOfficeSupplyName.FieldName = "NameNCC";
            this.colOfficeSupplyName.MinWidth = 37;
            this.colOfficeSupplyName.Name = "colOfficeSupplyName";
            this.colOfficeSupplyName.Visible = true;
            this.colOfficeSupplyName.VisibleIndex = 0;
            this.colOfficeSupplyName.Width = 170;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(142, 270);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(509, 108);
            this.txtNote.TabIndex = 6;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(62, 272);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(50, 16);
            this.lblNote.TabIndex = 75;
            this.lblNote.Text = "Ghi chú";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(116, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 76;
            this.label1.Text = "(*)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(116, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 16);
            this.label2.TabIndex = 77;
            this.label2.Text = "(*)";
            // 
            // lblDateRequest
            // 
            this.lblDateRequest.AutoSize = true;
            this.lblDateRequest.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateRequest.Location = new System.Drawing.Point(34, 176);
            this.lblDateRequest.Name = "lblDateRequest";
            this.lblDateRequest.Size = new System.Drawing.Size(84, 16);
            this.lblDateRequest.TabIndex = 78;
            this.lblDateRequest.Text = "Ngày đăng ký";
            // 
            // dtpDateRequest
            // 
            this.dtpDateRequest.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateRequest.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpDateRequest.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateRequest.Location = new System.Drawing.Point(142, 170);
            this.dtpDateRequest.Name = "dtpDateRequest";
            this.dtpDateRequest.Size = new System.Drawing.Size(174, 24);
            this.dtpDateRequest.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(116, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 16);
            this.label3.TabIndex = 79;
            this.label3.Text = "(*)";
            // 
            // chkExceedsLimit
            // 
            this.chkExceedsLimit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkExceedsLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExceedsLimit.Location = new System.Drawing.Point(346, 171);
            this.chkExceedsLimit.Margin = new System.Windows.Forms.Padding(2);
            this.chkExceedsLimit.Name = "chkExceedsLimit";
            this.chkExceedsLimit.Size = new System.Drawing.Size(132, 28);
            this.chkExceedsLimit.TabIndex = 81;
            this.chkExceedsLimit.Text = "Vuợt định mức";
            this.chkExceedsLimit.UseVisualStyleBackColor = true;
            this.chkExceedsLimit.CheckedChanged += new System.EventHandler(this.chkExceedsLimit_CheckedChanged);
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReason.Location = new System.Drawing.Point(10, 224);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(124, 16);
            this.lblReason.TabIndex = 82;
            this.lblReason.Text = "Lý do vượt định mức";
            // 
            // txtReason
            // 
            this.txtReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(142, 205);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ReadOnly = true;
            this.txtReason.Size = new System.Drawing.Size(509, 55);
            this.txtReason.TabIndex = 83;
            // 
            // cboUserName
            // 
            this.cboUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUserName.Location = new System.Drawing.Point(142, 58);
            this.cboUserName.Name = "cboUserName";
            this.cboUserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUserName.Properties.Appearance.Options.UseFont = true;
            this.cboUserName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserName.Properties.NullText = "";
            this.cboUserName.Properties.PopupView = this.gridView3;
            this.cboUserName.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.cboUserName.Size = new System.Drawing.Size(514, 24);
            this.cboUserName.TabIndex = 84;
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView3.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView3.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.Appearance.Row.Options.UseTextOptions = true;
            this.gridView3.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView3.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView3.ColumnPanelRowHeight = 40;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.GroupCount = 1;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.RowAutoHeight = true;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn11, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Mã nhân viên";
            this.gridColumn9.FieldName = "Code";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            this.gridColumn9.Width = 379;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Tên nhân viên";
            this.gridColumn10.FieldName = "FullName";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 659;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Phòng ban";
            this.gridColumn11.FieldName = "DepartmentName";
            this.gridColumn11.FieldNameSortGroup = "DepartmentSTT";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // frmOfficeSupplyRequestDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 385);
            this.Controls.Add(this.cboUserName);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.chkExceedsLimit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpDateRequest);
            this.Controls.Add(this.lblDateRequest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.cboOfficeSupply);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblAsterisk5);
            this.Controls.Add(this.lblAsterisk7);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.txtQuantityReceive);
            this.Controls.Add(this.lblQuantityReceive);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.lblOfficeSupply);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmOfficeSupplyRequestDetail";
            this.Text = "ĐĂNG KÝ VĂN PHÒNG PHẨM";
            this.Load += new System.EventHandler(this.frmOfficeSupplyRequestDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboOfficeSupply.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblAsterisk5;
        private System.Windows.Forms.Label lblAsterisk7;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantityReceive;
        private System.Windows.Forms.Label lblQuantityReceive;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.Label lblOfficeSupply;
        private DevExpress.XtraEditors.SearchLookUpEdit cboOfficeSupply;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colOSID;
        private DevExpress.XtraGrid.Columns.GridColumn colOfficeSupplyName;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn colOSUnitID;
        private System.Windows.Forms.Label lblDateRequest;
        private System.Windows.Forms.DateTimePicker dtpDateRequest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkExceedsLimit;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.ToolStripButton btnSaveAndReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboUserName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}