
namespace BMS
{
    partial class frmKPIErrorEmployeeSummaryMax
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
			this.cboEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dtpDateStart = new System.Windows.Forms.DateTimePicker();
			this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.txtKeyword = new System.Windows.Forms.TextBox();
			this.btnExportExcel = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.cboKPIErrorType = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.grdData = new DevExpress.XtraGrid.GridControl();
			this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colKPIErrorID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colContent = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboKPIErrorType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
			this.SuspendLayout();
			// 
			// cboEmployee
			// 
			this.cboEmployee.Location = new System.Drawing.Point(79, 34);
			this.cboEmployee.Name = "cboEmployee";
			this.cboEmployee.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboEmployee.Properties.Appearance.Options.UseFont = true;
			this.cboEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboEmployee.Properties.NullText = "";
			this.cboEmployee.Properties.PopupView = this.gridView3;
			this.cboEmployee.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit3});
			this.cboEmployee.Size = new System.Drawing.Size(253, 22);
			this.cboEmployee.TabIndex = 20;
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
			// repositoryItemMemoEdit3
			// 
			this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(5, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Nhân viên";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(173, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 16);
			this.label4.TabIndex = 16;
			this.label4.Text = "Đến ngày";
			// 
			// dtpDateStart
			// 
			this.dtpDateStart.CustomFormat = "dd/MM/yyyy";
			this.dtpDateStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpDateStart.Location = new System.Drawing.Point(79, 5);
			this.dtpDateStart.Name = "dtpDateStart";
			this.dtpDateStart.Size = new System.Drawing.Size(88, 23);
			this.dtpDateStart.TabIndex = 18;
			// 
			// dtpDateEnd
			// 
			this.dtpDateEnd.CustomFormat = "dd/MM/yyyy";
			this.dtpDateEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpDateEnd.Location = new System.Drawing.Point(244, 5);
			this.dtpDateEnd.Name = "dtpDateEnd";
			this.dtpDateEnd.Size = new System.Drawing.Size(88, 23);
			this.dtpDateEnd.TabIndex = 19;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(5, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 16);
			this.label3.TabIndex = 17;
			this.label3.Text = "Từ ngày";
			// 
			// panelControl1
			// 
			this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelControl1.Controls.Add(this.txtKeyword);
			this.panelControl1.Controls.Add(this.btnExportExcel);
			this.panelControl1.Controls.Add(this.btnSearch);
			this.panelControl1.Controls.Add(this.cboKPIErrorType);
			this.panelControl1.Controls.Add(this.cboDepartment);
			this.panelControl1.Controls.Add(this.cboEmployee);
			this.panelControl1.Controls.Add(this.label3);
			this.panelControl1.Controls.Add(this.label5);
			this.panelControl1.Controls.Add(this.label2);
			this.panelControl1.Controls.Add(this.label6);
			this.panelControl1.Controls.Add(this.label1);
			this.panelControl1.Controls.Add(this.dtpDateEnd);
			this.panelControl1.Controls.Add(this.label4);
			this.panelControl1.Controls.Add(this.dtpDateStart);
			this.panelControl1.Location = new System.Drawing.Point(12, 12);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(1266, 72);
			this.panelControl1.TabIndex = 21;
			// 
			// txtKeyword
			// 
			this.txtKeyword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtKeyword.Location = new System.Drawing.Point(401, 34);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.Size = new System.Drawing.Size(351, 23);
			this.txtKeyword.TabIndex = 22;
			// 
			// btnExportExcel
			// 
			this.btnExportExcel.AutoSize = true;
			this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExportExcel.Location = new System.Drawing.Point(839, 32);
			this.btnExportExcel.Name = "btnExportExcel";
			this.btnExportExcel.Size = new System.Drawing.Size(77, 26);
			this.btnExportExcel.TabIndex = 21;
			this.btnExportExcel.Text = "Xuất Excel";
			this.btnExportExcel.UseVisualStyleBackColor = true;
			this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.AutoSize = true;
			this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.Location = new System.Drawing.Point(758, 32);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 26);
			this.btnSearch.TabIndex = 21;
			this.btnSearch.Text = "Tìm kiếm";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// cboKPIErrorType
			// 
			this.cboKPIErrorType.Location = new System.Drawing.Point(401, 5);
			this.cboKPIErrorType.Name = "cboKPIErrorType";
			this.cboKPIErrorType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboKPIErrorType.Properties.Appearance.Options.UseFont = true;
			this.cboKPIErrorType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboKPIErrorType.Properties.NullText = "";
			this.cboKPIErrorType.Properties.PopupView = this.gridView2;
			this.cboKPIErrorType.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
			this.cboKPIErrorType.Size = new System.Drawing.Size(165, 22);
			this.cboKPIErrorType.TabIndex = 20;
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
			this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.gridView2.Appearance.Row.Options.UseFont = true;
			this.gridView2.Appearance.Row.Options.UseTextOptions = true;
			this.gridView2.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridView2.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView2.ColumnPanelRowHeight = 40;
			this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5});
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsBehavior.AutoExpandAllGroups = true;
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.RowAutoHeight = true;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn4
			// 
			this.gridColumn4.Caption = "Mã loại";
			this.gridColumn4.FieldName = "Code";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.Visible = true;
			this.gridColumn4.VisibleIndex = 0;
			this.gridColumn4.Width = 379;
			// 
			// gridColumn5
			// 
			this.gridColumn5.Caption = "Tên loại";
			this.gridColumn5.FieldName = "Name";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.Visible = true;
			this.gridColumn5.VisibleIndex = 1;
			this.gridColumn5.Width = 659;
			// 
			// repositoryItemMemoEdit2
			// 
			this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
			// 
			// cboDepartment
			// 
			this.cboDepartment.Location = new System.Drawing.Point(651, 5);
			this.cboDepartment.Name = "cboDepartment";
			this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboDepartment.Properties.Appearance.Options.UseFont = true;
			this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboDepartment.Properties.NullText = "";
			this.cboDepartment.Properties.PopupView = this.gridView1;
			this.cboDepartment.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
			this.cboDepartment.Size = new System.Drawing.Size(265, 22);
			this.cboDepartment.TabIndex = 20;
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
			this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.gridView1.Appearance.Row.Options.UseFont = true;
			this.gridView1.Appearance.Row.Options.UseTextOptions = true;
			this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.gridView1.ColumnPanelRowHeight = 40;
			this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.RowAutoHeight = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "Mã phòng ban";
			this.gridColumn1.FieldName = "Code";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
			this.gridColumn1.Width = 379;
			// 
			// gridColumn2
			// 
			this.gridColumn2.Caption = "Tên phòng ban";
			this.gridColumn2.FieldName = "Name";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 659;
			// 
			// repositoryItemMemoEdit1
			// 
			this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(338, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 16);
			this.label5.TabIndex = 15;
			this.label5.Text = "Loại lỗi";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(572, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 16);
			this.label2.TabIndex = 15;
			this.label2.Text = "Phòng ban";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(338, 37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57, 16);
			this.label6.TabIndex = 15;
			this.label6.Text = "Từ khóa";
			// 
			// grdData
			// 
			this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdData.Location = new System.Drawing.Point(12, 76);
			this.grdData.MainView = this.grvData;
			this.grdData.Name = "grdData";
			this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4});
			this.grdData.Size = new System.Drawing.Size(1266, 641);
			this.grdData.TabIndex = 22;
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
			this.grvData.ColumnPanelRowHeight = 50;
			this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmployeeID,
            this.colKPIErrorID,
            this.colCode,
            this.colFullName,
            this.colContent,
            this.colDepartmentName});
			this.grvData.GridControl = this.grdData;
			this.grvData.GroupCount = 2;
			this.grvData.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
			this.grvData.Name = "grvData";
			this.grvData.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True;
			this.grvData.OptionsBehavior.Editable = false;
			this.grvData.OptionsBehavior.ReadOnly = true;
			this.grvData.OptionsPrint.AutoWidth = false;
			this.grvData.OptionsView.ColumnAutoWidth = false;
			this.grvData.OptionsView.RowAutoHeight = true;
			this.grvData.OptionsView.ShowAutoFilterRow = true;
			this.grvData.OptionsView.ShowFooter = true;
			this.grvData.OptionsView.ShowGroupPanel = false;
			this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colFullName, DevExpress.Data.ColumnSortOrder.Ascending)});
			this.grvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvData_KeyDown);
			// 
			// colEmployeeID
			// 
			this.colEmployeeID.FieldName = "EmployeeID";
			this.colEmployeeID.Name = "colEmployeeID";
			this.colEmployeeID.Width = 80;
			// 
			// colKPIErrorID
			// 
			this.colKPIErrorID.FieldName = "KPIErrorID";
			this.colKPIErrorID.Name = "colKPIErrorID";
			// 
			// colCode
			// 
			this.colCode.Caption = "Mã nhân viên";
			this.colCode.ColumnEdit = this.repositoryItemMemoEdit4;
			this.colCode.FieldName = "Code";
			this.colCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.colCode.Name = "colCode";
			this.colCode.Width = 125;
			// 
			// repositoryItemMemoEdit4
			// 
			this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
			// 
			// colFullName
			// 
			this.colFullName.Caption = "Tên nhân viên";
			this.colFullName.ColumnEdit = this.repositoryItemMemoEdit4;
			this.colFullName.FieldName = "FullName";
			this.colFullName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.colFullName.Name = "colFullName";
			this.colFullName.Visible = true;
			this.colFullName.VisibleIndex = 1;
			this.colFullName.Width = 141;
			// 
			// colContent
			// 
			this.colContent.Caption = "Nội dung lỗi";
			this.colContent.ColumnEdit = this.repositoryItemMemoEdit4;
			this.colContent.FieldName = "Content";
			this.colContent.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.colContent.Name = "colContent";
			this.colContent.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
			this.colContent.Visible = true;
			this.colContent.VisibleIndex = 2;
			this.colContent.Width = 330;
			// 
			// colDepartmentName
			// 
			this.colDepartmentName.Caption = "Phòng ban";
			this.colDepartmentName.FieldName = "DepartmentName";
			this.colDepartmentName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.colDepartmentName.Name = "colDepartmentName";
			this.colDepartmentName.Visible = true;
			this.colDepartmentName.VisibleIndex = 0;
			this.colDepartmentName.Width = 662;
			// 
			// frmKPIErrorEmployeeSummaryMax
			// 
			this.AcceptButton = this.btnSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1290, 743);
			this.Controls.Add(this.grdData);
			this.Controls.Add(this.panelControl1);
			this.Name = "frmKPIErrorEmployeeSummaryMax";
			this.Text = "TỔNG HỢP NHÂN VIÊN NHIỀU LỖI";
			this.Load += new System.EventHandler(this.frmKPIErrorEmployeeSummaryMax_Load);
			((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.panelControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cboKPIErrorType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateStart;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboKPIErrorType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colKPIErrorID;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colContent;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
    }
}