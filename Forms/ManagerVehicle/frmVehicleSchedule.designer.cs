
namespace BMS
{
    partial class frmVehicleSchedule
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnDelete4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cboEmployee = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtVehicleMoney = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVehicleCategory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkStatus = new DevExpress.XtraEditors.CheckEdit();
            this.txtLicensePlate = new System.Windows.Forms.TextBox();
            this.cboNameVehicleCharge = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.grdNameVehicleCharge = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVehicleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameVehicleCharge = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLicensePlate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVehicleCategoryText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cboDepartureAddressActual = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDepartureAddressActual = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dteDepartureDateActual = new DevExpress.XtraEditors.DateTimeOffsetEdit();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVehicleMoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNameVehicleCharge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNameVehicleCharge)).BeginInit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteDepartureDateActual.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete4
            // 
            this.btnDelete4.AutoHeight = false;
            editorButtonImageOptions1.Image = global::Forms.Properties.Resources.cancel_16x16;
            this.btnDelete4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDelete4.Name = "btnDelete4";
            this.btnDelete4.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // cboEmployee
            // 
            this.cboEmployee.AutoHeight = false;
            this.cboEmployee.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.NullText = "";
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Appearance.BorderColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.groupControl3.Appearance.Options.UseBorderColor = true;
            this.groupControl3.Controls.Add(this.txtVehicleMoney);
            this.groupControl3.Controls.Add(this.label6);
            this.groupControl3.Controls.Add(this.txtVehicleCategory);
            this.groupControl3.Controls.Add(this.label3);
            this.groupControl3.Controls.Add(this.chkStatus);
            this.groupControl3.Controls.Add(this.txtLicensePlate);
            this.groupControl3.Controls.Add(this.cboNameVehicleCharge);
            this.groupControl3.Controls.Add(this.label4);
            this.groupControl3.Controls.Add(this.label2);
            this.groupControl3.Controls.Add(this.txtPhoneNumber);
            this.groupControl3.Controls.Add(this.txtDriverName);
            this.groupControl3.Controls.Add(this.label22);
            this.groupControl3.Controls.Add(this.label23);
            this.groupControl3.Controls.Add(this.label24);
            this.groupControl3.Controls.Add(this.label21);
            this.groupControl3.Controls.Add(this.label5);
            this.groupControl3.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.groupControl3.Location = new System.Drawing.Point(12, 58);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(851, 128);
            this.groupControl3.TabIndex = 12;
            this.groupControl3.Text = "THÔNG TIN XE";
            // 
            // txtVehicleMoney
            // 
            this.txtVehicleMoney.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVehicleMoney.EditValue = "0";
            this.txtVehicleMoney.Location = new System.Drawing.Point(564, 93);
            this.txtVehicleMoney.Name = "txtVehicleMoney";
            this.txtVehicleMoney.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleMoney.Properties.Appearance.Options.UseFont = true;
            this.txtVehicleMoney.Properties.MaskSettings.Set("mask", "n0");
            this.txtVehicleMoney.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtVehicleMoney.Properties.UseMaskAsDisplayFormat = true;
            this.txtVehicleMoney.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtVehicleMoney.Size = new System.Drawing.Size(281, 26);
            this.txtVehicleMoney.TabIndex = 272;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(396, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 271;
            this.label6.Text = "Tiền chuyến xe";
            // 
            // txtVehicleCategory
            // 
            this.txtVehicleCategory.Enabled = false;
            this.txtVehicleCategory.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleCategory.Location = new System.Drawing.Point(107, 93);
            this.txtVehicleCategory.Name = "txtVehicleCategory";
            this.txtVehicleCategory.Size = new System.Drawing.Size(280, 27);
            this.txtVehicleCategory.TabIndex = 270;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 269;
            this.label3.Text = "Loại xe";
            // 
            // chkStatus
            // 
            this.chkStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkStatus.Location = new System.Drawing.Point(107, 0);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStatus.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.chkStatus.Properties.Appearance.Options.UseFont = true;
            this.chkStatus.Properties.Appearance.Options.UseForeColor = true;
            this.chkStatus.Properties.AutoWidth = true;
            this.chkStatus.Properties.Caption = "Chủ động phương tiện";
            this.chkStatus.Size = new System.Drawing.Size(164, 20);
            this.chkStatus.TabIndex = 268;
            this.chkStatus.CheckedChanged += new System.EventHandler(this.chkStatus_CheckedChanged);
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicensePlate.Location = new System.Drawing.Point(107, 60);
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.Size = new System.Drawing.Size(280, 27);
            this.txtLicensePlate.TabIndex = 266;
            // 
            // cboNameVehicleCharge
            // 
            this.cboNameVehicleCharge.EditValue = "";
            this.cboNameVehicleCharge.Location = new System.Drawing.Point(107, 28);
            this.cboNameVehicleCharge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboNameVehicleCharge.Name = "cboNameVehicleCharge";
            this.cboNameVehicleCharge.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboNameVehicleCharge.Properties.Appearance.Options.UseFont = true;
            this.cboNameVehicleCharge.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNameVehicleCharge.Properties.NullText = "";
            this.cboNameVehicleCharge.Properties.PopupView = this.grdNameVehicleCharge;
            this.cboNameVehicleCharge.Size = new System.Drawing.Size(280, 26);
            this.cboNameVehicleCharge.TabIndex = 265;
            this.cboNameVehicleCharge.EditValueChanged += new System.EventHandler(this.cboNameVehicleCharge_EditValueChanged);
            // 
            // grdNameVehicleCharge
            // 
            this.grdNameVehicleCharge.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdNameVehicleCharge.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdNameVehicleCharge.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grdNameVehicleCharge.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grdNameVehicleCharge.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdNameVehicleCharge.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grdNameVehicleCharge.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grdNameVehicleCharge.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grdNameVehicleCharge.Appearance.Row.Options.UseFont = true;
            this.grdNameVehicleCharge.Appearance.Row.Options.UseForeColor = true;
            this.grdNameVehicleCharge.Appearance.Row.Options.UseTextOptions = true;
            this.grdNameVehicleCharge.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grdNameVehicleCharge.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grdNameVehicleCharge.ColumnPanelRowHeight = 40;
            this.grdNameVehicleCharge.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVehicleID,
            this.colNameVehicleCharge,
            this.colLicensePlate,
            this.colVehicleCategoryText,
            this.gridColumn1});
            this.grdNameVehicleCharge.DetailHeight = 554;
            this.grdNameVehicleCharge.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grdNameVehicleCharge.GroupCount = 1;
            this.grdNameVehicleCharge.Name = "grdNameVehicleCharge";
            this.grdNameVehicleCharge.OptionsBehavior.AutoExpandAllGroups = true;
            this.grdNameVehicleCharge.OptionsBehavior.Editable = false;
            this.grdNameVehicleCharge.OptionsBehavior.ReadOnly = true;
            this.grdNameVehicleCharge.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grdNameVehicleCharge.OptionsView.RowAutoHeight = true;
            this.grdNameVehicleCharge.OptionsView.ShowGroupPanel = false;
            this.grdNameVehicleCharge.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colVehicleCategoryText, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colVehicleID
            // 
            this.colVehicleID.Caption = "ID";
            this.colVehicleID.FieldName = "ID";
            this.colVehicleID.MinWidth = 29;
            this.colVehicleID.Name = "colVehicleID";
            this.colVehicleID.Width = 109;
            // 
            // colNameVehicleCharge
            // 
            this.colNameVehicleCharge.Caption = "Xe phụ trách";
            this.colNameVehicleCharge.FieldName = "VehicleName";
            this.colNameVehicleCharge.MinWidth = 29;
            this.colNameVehicleCharge.Name = "colNameVehicleCharge";
            this.colNameVehicleCharge.Visible = true;
            this.colNameVehicleCharge.VisibleIndex = 0;
            this.colNameVehicleCharge.Width = 527;
            // 
            // colLicensePlate
            // 
            this.colLicensePlate.Caption = "Biển số";
            this.colLicensePlate.FieldName = "LicensePlate";
            this.colLicensePlate.MinWidth = 29;
            this.colLicensePlate.Name = "colLicensePlate";
            this.colLicensePlate.Visible = true;
            this.colLicensePlate.VisibleIndex = 1;
            this.colLicensePlate.Width = 486;
            // 
            // colVehicleCategoryText
            // 
            this.colVehicleCategoryText.Caption = "Loại xe";
            this.colVehicleCategoryText.FieldName = "VehicleCategoryText";
            this.colVehicleCategoryText.Name = "colVehicleCategoryText";
            this.colVehicleCategoryText.Visible = true;
            this.colVehicleCategoryText.VisibleIndex = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Họ tên";
            this.gridColumn1.FieldName = "FullName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 472;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(450, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 263;
            this.label4.Text = "(*)";
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(69, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 262;
            this.label2.Text = "(*)";
            this.label2.Visible = false;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(564, 60);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(281, 27);
            this.txtPhoneNumber.TabIndex = 247;
            // 
            // txtDriverName
            // 
            this.txtDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDriverName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDriverName.Location = new System.Drawing.Point(564, 28);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(281, 27);
            this.txtDriverName.TabIndex = 246;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(396, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 20);
            this.label22.TabIndex = 224;
            this.label22.Text = "Lái xe";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(5, 67);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 20);
            this.label23.TabIndex = 225;
            this.label23.Text = "Biển số";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(396, 63);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 20);
            this.label24.TabIndex = 227;
            this.label24.Text = "SDT liên hệ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(4, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 20);
            this.label21.TabIndex = 239;
            this.label21.Text = "Xe phụ trách";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(487, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 264;
            this.label5.Text = "(*)";
            this.label5.Visible = false;
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(875, 55);
            this.mnuMenu.TabIndex = 8;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 52);
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.groupControl2.Appearance.BorderColor = System.Drawing.Color.Yellow;
            this.groupControl2.Appearance.Options.UseBackColor = true;
            this.groupControl2.Appearance.Options.UseBorderColor = true;
            this.groupControl2.Controls.Add(this.cboDepartureAddressActual);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.txtDepartureAddressActual);
            this.groupControl2.Controls.Add(this.label14);
            this.groupControl2.Controls.Add(this.label7);
            this.groupControl2.Controls.Add(this.label8);
            this.groupControl2.Controls.Add(this.dteDepartureDateActual);
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Location = new System.Drawing.Point(12, 93);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(851, 93);
            this.groupControl2.TabIndex = 13;
            this.groupControl2.Text = "LỊCH TRÌNH";
            this.groupControl2.Visible = false;
            // 
            // cboDepartureAddressActual
            // 
            this.cboDepartureAddressActual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartureAddressActual.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartureAddressActual.FormattingEnabled = true;
            this.cboDepartureAddressActual.Items.AddRange(new object[] {
            "Khác",
            "VP Hà Nội",
            "VP Bắc Ninh",
            "VP Hải Phòng",
            "VP Hồ Chí Minh"});
            this.cboDepartureAddressActual.Location = new System.Drawing.Point(175, 65);
            this.cboDepartureAddressActual.Name = "cboDepartureAddressActual";
            this.cboDepartureAddressActual.Size = new System.Drawing.Size(273, 27);
            this.cboDepartureAddressActual.TabIndex = 268;
            this.cboDepartureAddressActual.SelectedIndexChanged += new System.EventHandler(this.cboDepartureAddressActual_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 241;
            this.label1.Text = "Thời gian xuất phát";
            // 
            // txtDepartureAddressActual
            // 
            this.txtDepartureAddressActual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepartureAddressActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartureAddressActual.Location = new System.Drawing.Point(454, 51);
            this.txtDepartureAddressActual.Multiline = true;
            this.txtDepartureAddressActual.Name = "txtDepartureAddressActual";
            this.txtDepartureAddressActual.Size = new System.Drawing.Size(392, 37);
            this.txtDepartureAddressActual.TabIndex = 266;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(454, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(169, 21);
            this.label14.TabIndex = 267;
            this.label14.Text = "Điểm xuất phát cụ thể";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(125, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 264;
            this.label7.Text = "(*)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 262;
            this.label8.Text = "Điểm xuất phát";
            // 
            // dteDepartureDateActual
            // 
            this.dteDepartureDateActual.EditValue = null;
            this.dteDepartureDateActual.Location = new System.Drawing.Point(175, 27);
            this.dteDepartureDateActual.Name = "dteDepartureDateActual";
            this.dteDepartureDateActual.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteDepartureDateActual.Properties.Appearance.Options.UseFont = true;
            this.dteDepartureDateActual.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDepartureDateActual.Properties.MaskSettings.Set("mask", "dd/MM/yyyy hh:mm tt");
            this.dteDepartureDateActual.Properties.UseMaskAsDisplayFormat = true;
            this.dteDepartureDateActual.Size = new System.Drawing.Size(273, 26);
            this.dteDepartureDateActual.TabIndex = 250;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(148, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 255;
            this.label10.Text = "(*)";
            // 
            // frmVehicleSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 198);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.groupControl2);
            this.Name = "frmVehicleSchedule";
            this.Text = "XẾP XE";
            this.Load += new System.EventHandler(this.frmVehicleSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVehicleMoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNameVehicleCharge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNameVehicleCharge)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteDepartureDateActual.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label21;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboEmployee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SearchLookUpEdit cboNameVehicleCharge;
        private DevExpress.XtraGrid.Views.Grid.GridView grdNameVehicleCharge;
        private DevExpress.XtraGrid.Columns.GridColumn colVehicleID;
        private DevExpress.XtraGrid.Columns.GridColumn colNameVehicleCharge;
        private DevExpress.XtraGrid.Columns.GridColumn colLicensePlate;
        private System.Windows.Forms.TextBox txtLicensePlate;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.DateTimeOffsetEdit dteDepartureDateActual;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDepartureAddressActual;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboDepartureAddressActual;
        private DevExpress.XtraEditors.CheckEdit chkStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colVehicleCategoryText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVehicleCategory;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtVehicleMoney;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}