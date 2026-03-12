
namespace BMS
{
    partial class frmEmployeePayrollBonusDeuctionDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeePayrollBonusDeuctionDetail));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbName = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtOtherBonus = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPunish5S = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPankingCar = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKPIBonus = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.txtMonth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.txtOtherDeduction = new DevExpress.XtraEditors.TextEdit();
            this.txtBHXH = new DevExpress.XtraEditors.TextEdit();
            this.txtSalaryAdvance = new DevExpress.XtraEditors.TextEdit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherBonus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPunish5S.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPankingCar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKPIBonus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherDeduction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBHXH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalaryAdvance.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.mnuMenu.Size = new System.Drawing.Size(410, 43);
            this.mnuMenu.TabIndex = 6;
            this.mnuMenu.TabStop = true;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 40);
            this.btnSave.Tag = "frmThuHoPhongBan_HRUse";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Họ và tên";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ứng lương";
            // 
            // cbName
            // 
            this.cbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbName.Location = new System.Drawing.Point(122, 81);
            this.cbName.Name = "cbName";
            this.cbName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbName.Properties.Appearance.Options.UseFont = true;
            this.cbName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbName.Properties.NullText = "";
            this.cbName.Properties.PopupView = this.searchLookUpEdit1View;
            this.cbName.Size = new System.Drawing.Size(276, 24);
            this.cbName.TabIndex = 15;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 35;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDDetail,
            this.gridColumn2,
            this.gridColumn3});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colIDDetail
            // 
            this.colIDDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIDDetail.AppearanceHeader.Options.UseBackColor = true;
            this.colIDDetail.AppearanceHeader.Options.UseFont = true;
            this.colIDDetail.AppearanceHeader.Options.UseForeColor = true;
            this.colIDDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDDetail.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDDetail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDDetail.Caption = "ID";
            this.colIDDetail.FieldName = "ID";
            this.colIDDetail.Name = "colIDDetail";
            this.colIDDetail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIDDetail.OptionsFilter.AllowAutoFilter = false;
            this.colIDDetail.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "Mã nhân viên";
            this.gridColumn2.FieldName = "Code";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Tên nhân viên";
            this.gridColumn3.FieldName = "FullName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // txtOtherBonus
            // 
            this.txtOtherBonus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOtherBonus.Location = new System.Drawing.Point(122, 152);
            this.txtOtherBonus.Name = "txtOtherBonus";
            this.txtOtherBonus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherBonus.Properties.Appearance.Options.UseFont = true;
            this.txtOtherBonus.Properties.BeepOnError = false;
            this.txtOtherBonus.Properties.DisplayFormat.FormatString = "N0";
            this.txtOtherBonus.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtOtherBonus.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtOtherBonus.Properties.MaskSettings.Set("mask", "N0");
            this.txtOtherBonus.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtOtherBonus.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtOtherBonus.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtOtherBonus.Properties.UseMaskAsDisplayFormat = true;
            this.txtOtherBonus.Size = new System.Drawing.Size(276, 24);
            this.txtOtherBonus.TabIndex = 18;
            this.txtOtherBonus.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Gửi xe ô tô";
            // 
            // txtPunish5S
            // 
            this.txtPunish5S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPunish5S.Location = new System.Drawing.Point(122, 221);
            this.txtPunish5S.Name = "txtPunish5S";
            this.txtPunish5S.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPunish5S.Properties.Appearance.Options.UseFont = true;
            this.txtPunish5S.Properties.BeepOnError = false;
            this.txtPunish5S.Properties.DisplayFormat.FormatString = "N0";
            this.txtPunish5S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPunish5S.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtPunish5S.Properties.MaskSettings.Set("mask", "N0");
            this.txtPunish5S.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtPunish5S.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtPunish5S.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtPunish5S.Properties.UseMaskAsDisplayFormat = true;
            this.txtPunish5S.Size = new System.Drawing.Size(276, 24);
            this.txtPunish5S.TabIndex = 20;
            this.txtPunish5S.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "Thưởng khác";
            // 
            // txtPankingCar
            // 
            this.txtPankingCar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPankingCar.Location = new System.Drawing.Point(122, 186);
            this.txtPankingCar.Name = "txtPankingCar";
            this.txtPankingCar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPankingCar.Properties.Appearance.Options.UseFont = true;
            this.txtPankingCar.Properties.BeepOnError = false;
            this.txtPankingCar.Properties.DisplayFormat.FormatString = "N0";
            this.txtPankingCar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPankingCar.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtPankingCar.Properties.MaskSettings.Set("mask", "N0");
            this.txtPankingCar.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtPankingCar.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtPankingCar.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtPankingCar.Properties.UseMaskAsDisplayFormat = true;
            this.txtPankingCar.Size = new System.Drawing.Size(276, 24);
            this.txtPankingCar.TabIndex = 22;
            this.txtPankingCar.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "Phạt 5s\r\n";
            // 
            // txtKPIBonus
            // 
            this.txtKPIBonus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKPIBonus.Location = new System.Drawing.Point(122, 116);
            this.txtKPIBonus.Name = "txtKPIBonus";
            this.txtKPIBonus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKPIBonus.Properties.Appearance.Options.UseFont = true;
            this.txtKPIBonus.Properties.BeepOnError = false;
            this.txtKPIBonus.Properties.DisplayFormat.FormatString = "N0";
            this.txtKPIBonus.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtKPIBonus.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtKPIBonus.Properties.MaskSettings.Set("mask", "N0");
            this.txtKPIBonus.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtKPIBonus.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtKPIBonus.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtKPIBonus.Properties.UseMaskAsDisplayFormat = true;
            this.txtKPIBonus.Size = new System.Drawing.Size(276, 24);
            this.txtKPIBonus.TabIndex = 26;
            this.txtKPIBonus.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Thưởng  KPIs ";
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(51, 50);
            this.txtYear.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.txtYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(51, 22);
            this.txtYear.TabIndex = 209;
            this.txtYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonth.Location = new System.Drawing.Point(157, 50);
            this.txtMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(38, 22);
            this.txtMonth.TabIndex = 208;
            this.txtMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(109, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 207;
            this.label3.Text = "Tháng";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.Location = new System.Drawing.Point(8, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 17);
            this.label9.TabIndex = 206;
            this.label9.Text = "Năm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Khoản trừ khác";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 295);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 17);
            this.label10.TabIndex = 10;
            this.label10.Text = "BHXH";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 370);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 17);
            this.label11.TabIndex = 10;
            this.label11.Text = "Note";
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(122, 367);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(275, 111);
            this.txtNote.TabIndex = 14;
            // 
            // txtOtherDeduction
            // 
            this.txtOtherDeduction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOtherDeduction.Location = new System.Drawing.Point(121, 257);
            this.txtOtherDeduction.Name = "txtOtherDeduction";
            this.txtOtherDeduction.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherDeduction.Properties.Appearance.Options.UseFont = true;
            this.txtOtherDeduction.Properties.BeepOnError = false;
            this.txtOtherDeduction.Properties.DisplayFormat.FormatString = "N0";
            this.txtOtherDeduction.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtOtherDeduction.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtOtherDeduction.Properties.MaskSettings.Set("mask", "N0");
            this.txtOtherDeduction.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtOtherDeduction.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtOtherDeduction.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtOtherDeduction.Properties.UseMaskAsDisplayFormat = true;
            this.txtOtherDeduction.Size = new System.Drawing.Size(276, 24);
            this.txtOtherDeduction.TabIndex = 20;
            this.txtOtherDeduction.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // txtBHXH
            // 
            this.txtBHXH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBHXH.Location = new System.Drawing.Point(121, 292);
            this.txtBHXH.Name = "txtBHXH";
            this.txtBHXH.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHXH.Properties.Appearance.Options.UseFont = true;
            this.txtBHXH.Properties.BeepOnError = false;
            this.txtBHXH.Properties.DisplayFormat.FormatString = "N0";
            this.txtBHXH.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBHXH.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtBHXH.Properties.MaskSettings.Set("mask", "N0");
            this.txtBHXH.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtBHXH.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtBHXH.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtBHXH.Properties.UseMaskAsDisplayFormat = true;
            this.txtBHXH.Size = new System.Drawing.Size(276, 24);
            this.txtBHXH.TabIndex = 20;
            this.txtBHXH.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // txtSalaryAdvance
            // 
            this.txtSalaryAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalaryAdvance.Location = new System.Drawing.Point(121, 328);
            this.txtSalaryAdvance.Name = "txtSalaryAdvance";
            this.txtSalaryAdvance.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalaryAdvance.Properties.Appearance.Options.UseFont = true;
            this.txtSalaryAdvance.Properties.BeepOnError = false;
            this.txtSalaryAdvance.Properties.DisplayFormat.FormatString = "N0";
            this.txtSalaryAdvance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSalaryAdvance.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSalaryAdvance.Properties.MaskSettings.Set("mask", "N0");
            this.txtSalaryAdvance.Properties.MaskSettings.Set("hideInsignificantZeros", null);
            this.txtSalaryAdvance.Properties.MaskSettings.Set("autoHideDecimalSeparator", null);
            this.txtSalaryAdvance.Properties.MaskSettings.Set("valueAfterDelete", null);
            this.txtSalaryAdvance.Properties.UseMaskAsDisplayFormat = true;
            this.txtSalaryAdvance.Size = new System.Drawing.Size(276, 24);
            this.txtSalaryAdvance.TabIndex = 20;
            this.txtSalaryAdvance.TextChanged += new System.EventHandler(this.txtKPIBonus_TextChanged);
            // 
            // frmEmployeePayrollBonusDeuctionDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 482);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtKPIBonus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPankingCar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSalaryAdvance);
            this.Controls.Add(this.txtBHXH);
            this.Controls.Add(this.txtOtherDeduction);
            this.Controls.Add(this.txtPunish5S);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtOtherBonus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mnuMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmEmployeePayrollBonusDeuctionDetail";
            this.Text = "CHI TIẾT";
            this.Load += new System.EventHandler(this.frmThuHoPhongBanDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherBonus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPunish5S.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPankingCar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKPIBonus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherDeduction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBHXH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalaryAdvance.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SearchLookUpEdit cbName;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colIDDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.TextEdit txtOtherBonus;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit txtPunish5S;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtPankingCar;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtKPIBonus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtYear;
        private System.Windows.Forms.NumericUpDown txtMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNote;
        private DevExpress.XtraEditors.TextEdit txtOtherDeduction;
        private DevExpress.XtraEditors.TextEdit txtBHXH;
        private DevExpress.XtraEditors.TextEdit txtSalaryAdvance;
    }
}