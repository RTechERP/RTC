namespace BMS
{
    partial class frmContractSummary
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
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions3 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions4 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnApprovedContract = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancleContractApproved = new System.Windows.Forms.ToolStripButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cboContract = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegistedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colReasonCancel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSuplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeOrderText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flyoutPanel1 = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            this.txtReasonCancle = new System.Windows.Forms.RichTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.colEmployeeRecive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboContract.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).BeginInit();
            this.flyoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).BeginInit();
            this.flyoutPanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnApprovedContract,
            this.toolStripSeparator1,
            this.btnCancleContractApproved});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1559, 55);
            this.mnuMenu.TabIndex = 31;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnApprovedContract
            // 
            this.btnApprovedContract.AutoSize = false;
            this.btnApprovedContract.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprovedContract.Image = global::Forms.Properties.Resources.Apply_32x32;
            this.btnApprovedContract.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnApprovedContract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprovedContract.Name = "btnApprovedContract";
            this.btnApprovedContract.Size = new System.Drawing.Size(80, 52);
            this.btnApprovedContract.Tag = "frmPaymentOrder_ApproveDocument";
            this.btnApprovedContract.Text = "Duyệt";
            this.btnApprovedContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprovedContract.ToolTipText = "Duyệt";
            this.btnApprovedContract.Click += new System.EventHandler(this.btnApprovedContract_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // btnCancleContractApproved
            // 
            this.btnCancleContractApproved.AutoSize = false;
            this.btnCancleContractApproved.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancleContractApproved.Image = global::Forms.Properties.Resources.cancel_32x321;
            this.btnCancleContractApproved.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancleContractApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancleContractApproved.Name = "btnCancleContractApproved";
            this.btnCancleContractApproved.Size = new System.Drawing.Size(80, 52);
            this.btnCancleContractApproved.Tag = "frmPaymentOrder_ApproveDocument";
            this.btnCancleContractApproved.Text = "Hủy duyệt";
            this.btnCancleContractApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancleContractApproved.ToolTipText = "Hủy duyệt";
            this.btnCancleContractApproved.Click += new System.EventHandler(this.btnCancleContractApproved_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.dtpFromDate);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.dtpEndDate);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.cboContract);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtFilterText);
            this.panelControl1.Controls.Add(this.btnFind);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 55);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1559, 36);
            this.panelControl1.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Red;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1400, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 18);
            this.label5.TabIndex = 185;
            this.label5.Text = "Hủy nhận";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.LightGreen;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label8.Location = new System.Drawing.Point(1480, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 18);
            this.label8.TabIndex = 185;
            this.label8.Text = "Đã nhận";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 182;
            this.label1.Text = "Từ ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(66, 7);
            this.dtpFromDate.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(137, 23);
            this.dtpFromDate.TabIndex = 183;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label4.Location = new System.Drawing.Point(209, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 181;
            this.label4.Text = "Đến ngày";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(276, 7);
            this.dtpEndDate.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(136, 23);
            this.dtpEndDate.TabIndex = 180;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label3.Location = new System.Drawing.Point(418, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 176;
            this.label3.Text = "Mã hợp đồng";
            // 
            // cboContract
            // 
            this.cboContract.EditValue = "";
            this.cboContract.Location = new System.Drawing.Point(505, 7);
            this.cboContract.Name = "cboContract";
            this.cboContract.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cboContract.Properties.Appearance.Options.UseFont = true;
            this.cboContract.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboContract.Properties.NullText = "";
            this.cboContract.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboContract.Size = new System.Drawing.Size(313, 22);
            this.cboContract.TabIndex = 177;
            this.cboContract.EditValueChanged += new System.EventHandler(this.cboContract_EditValueChanged);
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
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 40;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.AutoExpandAllGroups = true;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.RowAutoHeight = true;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 406;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã hợp đồng";
            this.gridColumn2.FieldName = "DocumentName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 632;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label2.Location = new System.Drawing.Point(824, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 184;
            this.label2.Text = "Tìm kiếm";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtFilterText.Location = new System.Drawing.Point(890, 7);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(291, 23);
            this.txtFilterText.TabIndex = 179;
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnFind.Location = new System.Drawing.Point(1187, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 29);
            this.btnFind.TabIndex = 178;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 91);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.grdData.Size = new System.Drawing.Size(1559, 496);
            this.grdData.TabIndex = 37;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
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
            this.grvData.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grvData.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            this.grvData.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.AppearancePrint.Row.Options.UseFont = true;
            this.grvData.AppearancePrint.Row.Options.UseTextOptions = true;
            this.grvData.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSTT,
            this.colRegistedDate,
            this.colStatusText,
            this.colDateApproved,
            this.colEmployeeRegister,
            this.colEmployeeRecive,
            this.colDepartmentName,
            this.colDocumentType,
            this.colDocumentName,
            this.colReasonCancel,
            this.colCode,
            this.colSuplierName,
            this.colProjectFullName,
            this.colTypeOrderText,
            this.colTotalMoney,
            this.colUnit,
            this.colNote,
            this.colStatus});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsPrint.AutoWidth = false;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvData_RowStyle);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colSTT.AppearanceHeader.Options.UseFont = true;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 1;
            this.colSTT.Width = 69;
            // 
            // colRegistedDate
            // 
            this.colRegistedDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colRegistedDate.AppearanceCell.Options.UseFont = true;
            this.colRegistedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colRegistedDate.AppearanceHeader.Options.UseFont = true;
            this.colRegistedDate.Caption = "Ngày đăng ký";
            this.colRegistedDate.FieldName = "RegistedDate";
            this.colRegistedDate.Name = "colRegistedDate";
            this.colRegistedDate.Visible = true;
            this.colRegistedDate.VisibleIndex = 2;
            this.colRegistedDate.Width = 158;
            // 
            // colStatusText
            // 
            this.colStatusText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colStatusText.AppearanceCell.Options.UseFont = true;
            this.colStatusText.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colStatusText.AppearanceHeader.Options.UseFont = true;
            this.colStatusText.Caption = "Trạng thái";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 3;
            this.colStatusText.Width = 158;
            // 
            // colDateApproved
            // 
            this.colDateApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDateApproved.AppearanceCell.Options.UseFont = true;
            this.colDateApproved.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDateApproved.AppearanceHeader.Options.UseFont = true;
            this.colDateApproved.Caption = "Ngày nhận/hủy";
            this.colDateApproved.FieldName = "DateApproved";
            this.colDateApproved.Name = "colDateApproved";
            this.colDateApproved.Visible = true;
            this.colDateApproved.VisibleIndex = 4;
            this.colDateApproved.Width = 158;
            // 
            // colEmployeeRegister
            // 
            this.colEmployeeRegister.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colEmployeeRegister.AppearanceCell.Options.UseFont = true;
            this.colEmployeeRegister.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEmployeeRegister.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeRegister.Caption = "Người đăng ký";
            this.colEmployeeRegister.FieldName = "EmployeeRegister";
            this.colEmployeeRegister.Name = "colEmployeeRegister";
            this.colEmployeeRegister.Visible = true;
            this.colEmployeeRegister.VisibleIndex = 5;
            this.colEmployeeRegister.Width = 158;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDepartmentName.AppearanceCell.Options.UseFont = true;
            this.colDepartmentName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDepartmentName.AppearanceHeader.Options.UseFont = true;
            this.colDepartmentName.Caption = "Bộ phận";
            this.colDepartmentName.DisplayFormat.FormatString = "n2";
            this.colDepartmentName.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 7;
            this.colDepartmentName.Width = 158;
            // 
            // colDocumentType
            // 
            this.colDocumentType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDocumentType.AppearanceCell.Options.UseFont = true;
            this.colDocumentType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDocumentType.AppearanceHeader.Options.UseFont = true;
            this.colDocumentType.Caption = "Loại văn bản";
            this.colDocumentType.FieldName = "DocumentType";
            this.colDocumentType.Name = "colDocumentType";
            this.colDocumentType.Visible = true;
            this.colDocumentType.VisibleIndex = 8;
            this.colDocumentType.Width = 203;
            // 
            // colDocumentName
            // 
            this.colDocumentName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDocumentName.AppearanceCell.Options.UseFont = true;
            this.colDocumentName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDocumentName.AppearanceHeader.Options.UseFont = true;
            this.colDocumentName.Caption = "Tên văn bản";
            this.colDocumentName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colDocumentName.FieldName = "DocumentName";
            this.colDocumentName.Name = "colDocumentName";
            this.colDocumentName.Visible = true;
            this.colDocumentName.VisibleIndex = 9;
            this.colDocumentName.Width = 181;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colReasonCancel
            // 
            this.colReasonCancel.Caption = "Lý do hủy hợp đồng";
            this.colReasonCancel.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colReasonCancel.FieldName = "ReasonCancel";
            this.colReasonCancel.Name = "colReasonCancel";
            this.colReasonCancel.Visible = true;
            this.colReasonCancel.VisibleIndex = 11;
            this.colReasonCancel.Width = 232;
            // 
            // colCode
            // 
            this.colCode.Caption = "Số ĐNTT";
            this.colCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 10;
            this.colCode.Width = 169;
            // 
            // colSuplierName
            // 
            this.colSuplierName.Caption = "Nhà cung cấp";
            this.colSuplierName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSuplierName.FieldName = "SuplierName";
            this.colSuplierName.Name = "colSuplierName";
            this.colSuplierName.Visible = true;
            this.colSuplierName.VisibleIndex = 12;
            this.colSuplierName.Width = 284;
            // 
            // colProjectFullName
            // 
            this.colProjectFullName.Caption = "Dự án";
            this.colProjectFullName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectFullName.FieldName = "ProjectFullName";
            this.colProjectFullName.Name = "colProjectFullName";
            this.colProjectFullName.Visible = true;
            this.colProjectFullName.VisibleIndex = 13;
            this.colProjectFullName.Width = 266;
            // 
            // colTypeOrderText
            // 
            this.colTypeOrderText.Caption = "Phân loại chính ĐNTT";
            this.colTypeOrderText.FieldName = "TypeOrderText";
            this.colTypeOrderText.Name = "colTypeOrderText";
            this.colTypeOrderText.Visible = true;
            this.colTypeOrderText.VisibleIndex = 14;
            this.colTypeOrderText.Width = 201;
            // 
            // colTotalMoney
            // 
            this.colTotalMoney.Caption = "Số tiền";
            this.colTotalMoney.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colTotalMoney.FieldName = "TotalMoney";
            this.colTotalMoney.Name = "colTotalMoney";
            this.colTotalMoney.Visible = true;
            this.colTotalMoney.VisibleIndex = 15;
            this.colTotalMoney.Width = 209;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.DisplayFormat.FormatString = "n2";
            this.repositoryItemMemoEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colUnit
            // 
            this.colUnit.Caption = "ĐVT";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 16;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú / Chứng từ kèm theo";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 17;
            this.colNote.Width = 396;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // flyoutPanel1
            // 
            this.flyoutPanel1.AnimationRate = 100;
            this.flyoutPanel1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.flyoutPanel1.Appearance.Options.UseFont = true;
            this.flyoutPanel1.Controls.Add(this.flyoutPanelControl1);
            this.flyoutPanel1.Location = new System.Drawing.Point(24, 181);
            this.flyoutPanel1.Name = "flyoutPanel1";
            this.flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual;
            this.flyoutPanel1.Options.CloseOnOuterClick = true;
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Hovered.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Hovered.Options.UseFont = true;
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Normal.BackColor = System.Drawing.Color.Brown;
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Normal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Normal.Options.UseBackColor = true;
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Normal.Options.UseBorderColor = true;
            this.flyoutPanel1.OptionsButtonPanel.AppearanceButton.Normal.Options.UseFont = true;
            this.flyoutPanel1.OptionsButtonPanel.ButtonPanelLocation = DevExpress.Utils.FlyoutPanelButtonPanelLocation.Bottom;
            this.flyoutPanel1.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("OK", true, buttonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "btnOK", -1, false),
            new DevExpress.Utils.PeekFormButton("Cancel", true, buttonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, "btnCancel", -1, false)});
            this.flyoutPanel1.OptionsButtonPanel.ShowButtonPanel = true;
            this.flyoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.flyoutPanel1.Size = new System.Drawing.Size(486, 210);
            this.flyoutPanel1.TabIndex = 169;
            this.flyoutPanel1.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.flyoutPanel1_ButtonClick);
            // 
            // flyoutPanelControl1
            // 
            this.flyoutPanelControl1.Controls.Add(this.txtReasonCancle);
            this.flyoutPanelControl1.Controls.Add(this.label18);
            this.flyoutPanelControl1.Controls.Add(this.label17);
            this.flyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl1.FlyoutPanel = this.flyoutPanel1;
            this.flyoutPanelControl1.Location = new System.Drawing.Point(0, 0);
            this.flyoutPanelControl1.Name = "flyoutPanelControl1";
            this.flyoutPanelControl1.Size = new System.Drawing.Size(486, 180);
            this.flyoutPanelControl1.TabIndex = 0;
            // 
            // txtReasonCancle
            // 
            this.txtReasonCancle.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.txtReasonCancle.Location = new System.Drawing.Point(8, 27);
            this.txtReasonCancle.Name = "txtReasonCancle";
            this.txtReasonCancle.Size = new System.Drawing.Size(467, 148);
            this.txtReasonCancle.TabIndex = 71;
            this.txtReasonCancle.Text = "";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(107, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(21, 13);
            this.label18.TabIndex = 70;
            this.label18.Text = "(*)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(5, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 16);
            this.label17.TabIndex = 70;
            this.label17.Text = "Lý do hủy duyệt";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // colEmployeeRecive
            // 
            this.colEmployeeRecive.Caption = "Người nhận";
            this.colEmployeeRecive.FieldName = "EmployeeRecive";
            this.colEmployeeRecive.Name = "colEmployeeRecive";
            this.colEmployeeRecive.Visible = true;
            this.colEmployeeRecive.VisibleIndex = 6;
            this.colEmployeeRecive.Width = 127;
            // 
            // frmContractSummary
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 587);
            this.Controls.Add(this.flyoutPanel1);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmContractSummary";
            this.Text = "TỔNG HỢP HỢP ĐỒNG";
            this.Load += new System.EventHandler(this.frmContractSummary_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboContract.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanel1)).EndInit();
            this.flyoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).EndInit();
            this.flyoutPanelControl1.ResumeLayout(false);
            this.flyoutPanelControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnCancleContractApproved;
        private System.Windows.Forms.ToolStripButton btnApprovedContract;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SearchLookUpEdit cboContract;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colDateApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentType;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentName;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSuplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeOrderText;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colReasonCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.Utils.FlyoutPanel flyoutPanel1;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RichTextBox txtReasonCancle;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeRecive;
    }
}