
namespace BMS
{
    partial class frmVehicleManagementDetail
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
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveNew = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.txtPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.txtLicensePlate = new System.Windows.Forms.MaskedTextBox();
            this.txtVehicleName = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.lblLicensePlate = new System.Windows.Forms.Label();
            this.lblVehicleName = new System.Windows.Forms.Label();
            this.lblVehicleCategory = new System.Windows.Forms.Label();
            this.lblSlot = new System.Windows.Forms.Label();
            this.txtSlot = new System.Windows.Forms.NumericUpDown();
            this.txtDriverName = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDriver = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboVehicleCategory = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddVehicleCategory = new System.Windows.Forms.Button();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSlot)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDriver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboVehicleCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 52);
            this.btnSave.Tag = "frmVehicleManagement_Add";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSaveNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(113, 52);
            this.btnSaveNew.Tag = "frmVehicleManagement_Add";
            this.btnSaveNew.Text = "Cất && Thêm mới";
            this.btnSaveNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
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
            this.btnSaveNew});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(653, 55);
            this.mnuMenu.TabIndex = 3;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPhoneNumber.Location = new System.Drawing.Point(73, 60);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(379, 27);
            this.txtPhoneNumber.TabIndex = 15;
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicensePlate.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtLicensePlate.Location = new System.Drawing.Point(72, 58);
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.Size = new System.Drawing.Size(550, 27);
            this.txtLicensePlate.TabIndex = 13;
            // 
            // txtVehicleName
            // 
            this.txtVehicleName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVehicleName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtVehicleName.Location = new System.Drawing.Point(73, 25);
            this.txtVehicleName.Name = "txtVehicleName";
            this.txtVehicleName.Size = new System.Drawing.Size(550, 27);
            this.txtVehicleName.TabIndex = 12;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblPhoneNumber.Location = new System.Drawing.Point(11, 64);
            this.lblPhoneNumber.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPhoneNumber.Size = new System.Drawing.Size(39, 19);
            this.lblPhoneNumber.TabIndex = 11;
            this.lblPhoneNumber.Tag = "3";
            this.lblPhoneNumber.Text = "SDT";
            // 
            // lblDriverName
            // 
            this.lblDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblDriverName.Location = new System.Drawing.Point(11, 31);
            this.lblDriverName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDriverName.Size = new System.Drawing.Size(50, 19);
            this.lblDriverName.TabIndex = 10;
            this.lblDriverName.Tag = "3";
            this.lblDriverName.Text = "Lái xe";
            // 
            // lblLicensePlate
            // 
            this.lblLicensePlate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLicensePlate.AutoSize = true;
            this.lblLicensePlate.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblLicensePlate.Location = new System.Drawing.Point(6, 62);
            this.lblLicensePlate.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblLicensePlate.Name = "lblLicensePlate";
            this.lblLicensePlate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblLicensePlate.Size = new System.Drawing.Size(60, 19);
            this.lblLicensePlate.TabIndex = 9;
            this.lblLicensePlate.Tag = "3";
            this.lblLicensePlate.Text = "Biển số";
            // 
            // lblVehicleName
            // 
            this.lblVehicleName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVehicleName.AutoSize = true;
            this.lblVehicleName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblVehicleName.Location = new System.Drawing.Point(6, 29);
            this.lblVehicleName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblVehicleName.Name = "lblVehicleName";
            this.lblVehicleName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVehicleName.Size = new System.Drawing.Size(57, 19);
            this.lblVehicleName.TabIndex = 8;
            this.lblVehicleName.Tag = "3";
            this.lblVehicleName.Text = "Tên xe";
            // 
            // lblVehicleCategory
            // 
            this.lblVehicleCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVehicleCategory.AutoSize = true;
            this.lblVehicleCategory.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblVehicleCategory.Location = new System.Drawing.Point(6, 96);
            this.lblVehicleCategory.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblVehicleCategory.Name = "lblVehicleCategory";
            this.lblVehicleCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVehicleCategory.Size = new System.Drawing.Size(59, 19);
            this.lblVehicleCategory.TabIndex = 16;
            this.lblVehicleCategory.Tag = "3";
            this.lblVehicleCategory.Text = "Loại xe";
            // 
            // lblSlot
            // 
            this.lblSlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSlot.AutoSize = true;
            this.lblSlot.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblSlot.Location = new System.Drawing.Point(458, 96);
            this.lblSlot.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSlot.Size = new System.Drawing.Size(73, 19);
            this.lblSlot.TabIndex = 17;
            this.lblSlot.Tag = "3";
            this.lblSlot.Text = "Chỗ ngồi";
            // 
            // txtSlot
            // 
            this.txtSlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSlot.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlot.Location = new System.Drawing.Point(537, 92);
            this.txtSlot.Maximum = new decimal(new int[] {
            46,
            0,
            0,
            0});
            this.txtSlot.Name = "txtSlot";
            this.txtSlot.Size = new System.Drawing.Size(86, 27);
            this.txtSlot.TabIndex = 209;
            this.txtSlot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDriverName
            // 
            this.txtDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDriverName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtDriverName.Location = new System.Drawing.Point(303, 26);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(319, 27);
            this.txtDriverName.TabIndex = 213;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboDriver);
            this.groupBox1.Controls.Add(this.txtDriverName);
            this.groupBox1.Controls.Add(this.lblDriverName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSTT);
            this.groupBox1.Controls.Add(this.lblPhoneNumber);
            this.groupBox1.Controls.Add(this.txtPhoneNumber);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 102);
            this.groupBox1.TabIndex = 214;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin người lái xe";
            // 
            // cboDriver
            // 
            this.cboDriver.EditValue = "";
            this.cboDriver.Location = new System.Drawing.Point(73, 26);
            this.cboDriver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboDriver.Properties.Appearance.Options.UseFont = true;
            this.cboDriver.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDriver.Properties.NullText = "";
            this.cboDriver.Properties.PopupView = this.gridView2;
            this.cboDriver.Size = new System.Drawing.Size(223, 26);
            this.cboDriver.TabIndex = 214;
            this.cboDriver.EditValueChanged += new System.EventHandler(this.cboDriver_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView2.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Appearance.Row.Options.UseForeColor = true;
            this.gridView2.Appearance.Row.Options.UseTextOptions = true;
            this.gridView2.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView2.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView2.ColumnPanelRowHeight = 40;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridView2.DetailHeight = 554;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GroupCount = 1;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn7, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 29;
            this.colID.Name = "colID";
            this.colID.Width = 109;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mã nhân viên";
            this.gridColumn5.FieldName = "Code";
            this.gridColumn5.MinWidth = 29;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 306;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tên nhân viên";
            this.gridColumn6.FieldName = "FullName";
            this.gridColumn6.MinWidth = 29;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 621;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Phòng ban";
            this.gridColumn7.FieldName = "DepartmentName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 101;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(493, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 11;
            this.label1.Tag = "3";
            this.label1.Text = "STT";
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTT.Location = new System.Drawing.Point(537, 60);
            this.txtSTT.Maximum = new decimal(new int[] {
            46,
            0,
            0,
            0});
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(85, 27);
            this.txtSTT.TabIndex = 209;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnAddVehicleCategory);
            this.groupBox2.Controls.Add(this.cboVehicleCategory);
            this.groupBox2.Controls.Add(this.lblVehicleName);
            this.groupBox2.Controls.Add(this.lblLicensePlate);
            this.groupBox2.Controls.Add(this.txtVehicleName);
            this.groupBox2.Controls.Add(this.txtSlot);
            this.groupBox2.Controls.Add(this.txtLicensePlate);
            this.groupBox2.Controls.Add(this.lblSlot);
            this.groupBox2.Controls.Add(this.lblVehicleCategory);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupBox2.Location = new System.Drawing.Point(12, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(628, 139);
            this.groupBox2.TabIndex = 216;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin xe";
            // 
            // cboVehicleCategory
            // 
            this.cboVehicleCategory.EditValue = "";
            this.cboVehicleCategory.Location = new System.Drawing.Point(72, 93);
            this.cboVehicleCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboVehicleCategory.Name = "cboVehicleCategory";
            this.cboVehicleCategory.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboVehicleCategory.Properties.Appearance.Options.UseFont = true;
            this.cboVehicleCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVehicleCategory.Properties.NullText = "";
            this.cboVehicleCategory.Properties.PopupView = this.gridView1;
            this.cboVehicleCategory.Size = new System.Drawing.Size(351, 26);
            this.cboVehicleCategory.TabIndex = 215;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 40;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.DetailHeight = 554;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.MinWidth = 29;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 109;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã loại xe";
            this.gridColumn2.FieldName = "CategoryCode";
            this.gridColumn2.MinWidth = 29;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 306;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên loại xe";
            this.gridColumn3.FieldName = "CategoryName";
            this.gridColumn3.MinWidth = 29;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 621;
            // 
            // btnAddVehicleCategory
            // 
            this.btnAddVehicleCategory.AutoSize = true;
            this.btnAddVehicleCategory.Image = global::Forms.Properties.Resources.add_16x161;
            this.btnAddVehicleCategory.Location = new System.Drawing.Point(430, 95);
            this.btnAddVehicleCategory.Name = "btnAddVehicleCategory";
            this.btnAddVehicleCategory.Size = new System.Drawing.Size(22, 23);
            this.btnAddVehicleCategory.TabIndex = 216;
            this.btnAddVehicleCategory.UseVisualStyleBackColor = true;
            this.btnAddVehicleCategory.Click += new System.EventHandler(this.btnAddVehicleCategory_Click);
            // 
            // frmVehicleManagementDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 332);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmVehicleManagementDetail";
            this.Text = "QUẢN LÝ CHI TẾT XE";
            this.Load += new System.EventHandler(this.frmVehicleManagementDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSlot)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDriver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboVehicleCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveNew;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumber;
        private System.Windows.Forms.MaskedTextBox txtLicensePlate;
        private System.Windows.Forms.TextBox txtVehicleName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Label lblLicensePlate;
        private System.Windows.Forms.Label lblVehicleName;
        private System.Windows.Forms.Label lblVehicleCategory;
        private System.Windows.Forms.Label lblSlot;
        private System.Windows.Forms.NumericUpDown txtSlot;
        private System.Windows.Forms.MaskedTextBox txtDriverName;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDriver;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private DevExpress.XtraEditors.SearchLookUpEdit cboVehicleCategory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.Button btnAddVehicleCategory;
    }
}