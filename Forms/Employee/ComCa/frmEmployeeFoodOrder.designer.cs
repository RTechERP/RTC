
namespace BMS
{
    partial class frmEmployeeFoodOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeFoodOrder));
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD29 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colD31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalLuch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDinner = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalFoodOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.label1);
            this.stackPanel1.Controls.Add(this.txtYear);
            this.stackPanel1.Controls.Add(this.label2);
            this.stackPanel1.Controls.Add(this.txtMonth);
            this.stackPanel1.Controls.Add(this.btnSearch);
            this.stackPanel1.Controls.Add(this.btnImportExcel);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackPanel1.Location = new System.Drawing.Point(0, 0);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(1369, 36);
            this.stackPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Năm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(55, 5);
            this.txtYear.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(74, 26);
            this.txtYear.TabIndex = 1;
            this.txtYear.ValueChanged += new System.EventHandler(this.txtYear_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(142, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tháng";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonth.Location = new System.Drawing.Point(198, 5);
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
            this.txtMonth.Size = new System.Drawing.Size(50, 26);
            this.txtMonth.TabIndex = 3;
            this.txtMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.ValueChanged += new System.EventHandler(this.txtMonth_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(261, 3);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Load";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.Location = new System.Drawing.Point(374, 3);
            this.btnImportExcel.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnImportExcel.TabIndex = 5;
            this.btnImportExcel.Text = "Xuất Excel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 36);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1369, 750);
            this.grdData.TabIndex = 1;
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
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.AutoFillColumn = this.colNote;
            this.grvData.ColumnPanelRowHeight = 45;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCode,
            this.colFullName,
            this.colName,
            this.colD1,
            this.colD2,
            this.colD3,
            this.colD4,
            this.colD5,
            this.colD6,
            this.colD7,
            this.colD8,
            this.colD9,
            this.colD10,
            this.colD11,
            this.colD12,
            this.colD13,
            this.colD14,
            this.colD15,
            this.colD16,
            this.colD17,
            this.colD18,
            this.colD19,
            this.colD20,
            this.colD21,
            this.colD22,
            this.colD23,
            this.colD24,
            this.colD25,
            this.colD26,
            this.colD27,
            this.colD28,
            this.colD29,
            this.colD30,
            this.colD31,
            this.colTotalLuch,
            this.colTotalDinner,
            this.colTotal,
            this.colTotalFoodOrder,
            this.colTotalActual,
            this.colNote,
            this.colDepartmentName,
            this.colDepartmentID});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.RowHeight = 32;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.OptionsFilter.AllowAutoFilter = false;
            this.colNote.OptionsFilter.AllowFilter = false;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 39;
            this.colNote.Width = 98;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            this.repositoryItemMemoEdit1.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsFilter.AllowAutoFilter = false;
            this.colID.OptionsFilter.AllowFilter = false;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsFilter.AllowAutoFilter = false;
            this.colCode.OptionsFilter.AllowFilter = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 96;
            // 
            // colFullName
            // 
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsFilter.AllowAutoFilter = false;
            this.colFullName.OptionsFilter.AllowFilter = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 1;
            this.colFullName.Width = 192;
            // 
            // colName
            // 
            this.colName.Caption = "Chức vụ";
            this.colName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsFilter.AllowAutoFilter = false;
            this.colName.OptionsFilter.AllowFilter = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 2;
            this.colName.Width = 153;
            // 
            // colD1
            // 
            this.colD1.AppearanceCell.Options.UseTextOptions = true;
            this.colD1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD1.Caption = "1";
            this.colD1.FieldName = "D1";
            this.colD1.MaxWidth = 30;
            this.colD1.MinWidth = 30;
            this.colD1.Name = "colD1";
            this.colD1.OptionsColumn.AllowMove = false;
            this.colD1.OptionsColumn.AllowSize = false;
            this.colD1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD1.OptionsFilter.AllowAutoFilter = false;
            this.colD1.OptionsFilter.AllowFilter = false;
            this.colD1.Visible = true;
            this.colD1.VisibleIndex = 3;
            this.colD1.Width = 30;
            // 
            // colD2
            // 
            this.colD2.AppearanceCell.Options.UseTextOptions = true;
            this.colD2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD2.Caption = "2";
            this.colD2.FieldName = "D2";
            this.colD2.MaxWidth = 30;
            this.colD2.MinWidth = 30;
            this.colD2.Name = "colD2";
            this.colD2.OptionsColumn.AllowMove = false;
            this.colD2.OptionsColumn.AllowSize = false;
            this.colD2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD2.OptionsFilter.AllowAutoFilter = false;
            this.colD2.OptionsFilter.AllowFilter = false;
            this.colD2.Visible = true;
            this.colD2.VisibleIndex = 4;
            this.colD2.Width = 30;
            // 
            // colD3
            // 
            this.colD3.AppearanceCell.Options.UseTextOptions = true;
            this.colD3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD3.Caption = "3";
            this.colD3.FieldName = "D3";
            this.colD3.MaxWidth = 30;
            this.colD3.MinWidth = 30;
            this.colD3.Name = "colD3";
            this.colD3.OptionsColumn.AllowMove = false;
            this.colD3.OptionsColumn.AllowSize = false;
            this.colD3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD3.OptionsFilter.AllowAutoFilter = false;
            this.colD3.OptionsFilter.AllowFilter = false;
            this.colD3.Visible = true;
            this.colD3.VisibleIndex = 5;
            this.colD3.Width = 30;
            // 
            // colD4
            // 
            this.colD4.AppearanceCell.Options.UseTextOptions = true;
            this.colD4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD4.Caption = "4";
            this.colD4.FieldName = "D4";
            this.colD4.MaxWidth = 30;
            this.colD4.MinWidth = 30;
            this.colD4.Name = "colD4";
            this.colD4.OptionsColumn.AllowMove = false;
            this.colD4.OptionsColumn.AllowSize = false;
            this.colD4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD4.OptionsFilter.AllowAutoFilter = false;
            this.colD4.OptionsFilter.AllowFilter = false;
            this.colD4.Visible = true;
            this.colD4.VisibleIndex = 6;
            this.colD4.Width = 30;
            // 
            // colD5
            // 
            this.colD5.AppearanceCell.Options.UseTextOptions = true;
            this.colD5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD5.Caption = "5";
            this.colD5.FieldName = "D5";
            this.colD5.MaxWidth = 30;
            this.colD5.MinWidth = 30;
            this.colD5.Name = "colD5";
            this.colD5.OptionsColumn.AllowMove = false;
            this.colD5.OptionsColumn.AllowSize = false;
            this.colD5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD5.OptionsFilter.AllowAutoFilter = false;
            this.colD5.OptionsFilter.AllowFilter = false;
            this.colD5.Visible = true;
            this.colD5.VisibleIndex = 7;
            this.colD5.Width = 30;
            // 
            // colD6
            // 
            this.colD6.AppearanceCell.Options.UseTextOptions = true;
            this.colD6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD6.Caption = "6";
            this.colD6.FieldName = "D6";
            this.colD6.MaxWidth = 30;
            this.colD6.MinWidth = 30;
            this.colD6.Name = "colD6";
            this.colD6.OptionsColumn.AllowMove = false;
            this.colD6.OptionsColumn.AllowSize = false;
            this.colD6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD6.OptionsFilter.AllowAutoFilter = false;
            this.colD6.OptionsFilter.AllowFilter = false;
            this.colD6.Visible = true;
            this.colD6.VisibleIndex = 8;
            this.colD6.Width = 30;
            // 
            // colD7
            // 
            this.colD7.AppearanceCell.Options.UseTextOptions = true;
            this.colD7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD7.Caption = "7";
            this.colD7.FieldName = "D7";
            this.colD7.MaxWidth = 30;
            this.colD7.MinWidth = 30;
            this.colD7.Name = "colD7";
            this.colD7.OptionsColumn.AllowMove = false;
            this.colD7.OptionsColumn.AllowSize = false;
            this.colD7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD7.OptionsFilter.AllowAutoFilter = false;
            this.colD7.OptionsFilter.AllowFilter = false;
            this.colD7.Visible = true;
            this.colD7.VisibleIndex = 9;
            this.colD7.Width = 30;
            // 
            // colD8
            // 
            this.colD8.AppearanceCell.Options.UseTextOptions = true;
            this.colD8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD8.Caption = "8";
            this.colD8.FieldName = "D8";
            this.colD8.MaxWidth = 30;
            this.colD8.MinWidth = 30;
            this.colD8.Name = "colD8";
            this.colD8.OptionsColumn.AllowMove = false;
            this.colD8.OptionsColumn.AllowSize = false;
            this.colD8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD8.OptionsFilter.AllowAutoFilter = false;
            this.colD8.OptionsFilter.AllowFilter = false;
            this.colD8.Visible = true;
            this.colD8.VisibleIndex = 10;
            this.colD8.Width = 30;
            // 
            // colD9
            // 
            this.colD9.AppearanceCell.Options.UseTextOptions = true;
            this.colD9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD9.Caption = "9";
            this.colD9.FieldName = "D9";
            this.colD9.MaxWidth = 30;
            this.colD9.MinWidth = 30;
            this.colD9.Name = "colD9";
            this.colD9.OptionsColumn.AllowMove = false;
            this.colD9.OptionsColumn.AllowSize = false;
            this.colD9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD9.OptionsFilter.AllowAutoFilter = false;
            this.colD9.OptionsFilter.AllowFilter = false;
            this.colD9.Visible = true;
            this.colD9.VisibleIndex = 11;
            this.colD9.Width = 30;
            // 
            // colD10
            // 
            this.colD10.AppearanceCell.Options.UseTextOptions = true;
            this.colD10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD10.Caption = "10";
            this.colD10.FieldName = "D10";
            this.colD10.MaxWidth = 30;
            this.colD10.MinWidth = 30;
            this.colD10.Name = "colD10";
            this.colD10.OptionsColumn.AllowMove = false;
            this.colD10.OptionsColumn.AllowSize = false;
            this.colD10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD10.OptionsFilter.AllowAutoFilter = false;
            this.colD10.OptionsFilter.AllowFilter = false;
            this.colD10.Visible = true;
            this.colD10.VisibleIndex = 12;
            this.colD10.Width = 30;
            // 
            // colD11
            // 
            this.colD11.AppearanceCell.Options.UseTextOptions = true;
            this.colD11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD11.Caption = "11";
            this.colD11.FieldName = "D11";
            this.colD11.MaxWidth = 30;
            this.colD11.MinWidth = 30;
            this.colD11.Name = "colD11";
            this.colD11.OptionsColumn.AllowMove = false;
            this.colD11.OptionsColumn.AllowSize = false;
            this.colD11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD11.OptionsFilter.AllowAutoFilter = false;
            this.colD11.OptionsFilter.AllowFilter = false;
            this.colD11.Visible = true;
            this.colD11.VisibleIndex = 13;
            this.colD11.Width = 30;
            // 
            // colD12
            // 
            this.colD12.AppearanceCell.Options.UseTextOptions = true;
            this.colD12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD12.Caption = "12";
            this.colD12.FieldName = "D12";
            this.colD12.MaxWidth = 30;
            this.colD12.MinWidth = 30;
            this.colD12.Name = "colD12";
            this.colD12.OptionsColumn.AllowMove = false;
            this.colD12.OptionsColumn.AllowSize = false;
            this.colD12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD12.OptionsFilter.AllowAutoFilter = false;
            this.colD12.OptionsFilter.AllowFilter = false;
            this.colD12.Visible = true;
            this.colD12.VisibleIndex = 14;
            this.colD12.Width = 30;
            // 
            // colD13
            // 
            this.colD13.AppearanceCell.Options.UseTextOptions = true;
            this.colD13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD13.Caption = "13";
            this.colD13.FieldName = "D13";
            this.colD13.MaxWidth = 30;
            this.colD13.MinWidth = 30;
            this.colD13.Name = "colD13";
            this.colD13.OptionsColumn.AllowMove = false;
            this.colD13.OptionsColumn.AllowSize = false;
            this.colD13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD13.OptionsFilter.AllowAutoFilter = false;
            this.colD13.OptionsFilter.AllowFilter = false;
            this.colD13.Visible = true;
            this.colD13.VisibleIndex = 15;
            this.colD13.Width = 30;
            // 
            // colD14
            // 
            this.colD14.AppearanceCell.Options.UseTextOptions = true;
            this.colD14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD14.Caption = "14";
            this.colD14.FieldName = "D14";
            this.colD14.MaxWidth = 30;
            this.colD14.MinWidth = 30;
            this.colD14.Name = "colD14";
            this.colD14.OptionsColumn.AllowMove = false;
            this.colD14.OptionsColumn.AllowSize = false;
            this.colD14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD14.OptionsFilter.AllowAutoFilter = false;
            this.colD14.OptionsFilter.AllowFilter = false;
            this.colD14.Visible = true;
            this.colD14.VisibleIndex = 16;
            this.colD14.Width = 30;
            // 
            // colD15
            // 
            this.colD15.AppearanceCell.Options.UseTextOptions = true;
            this.colD15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD15.Caption = "15";
            this.colD15.FieldName = "D15";
            this.colD15.MaxWidth = 30;
            this.colD15.MinWidth = 30;
            this.colD15.Name = "colD15";
            this.colD15.OptionsColumn.AllowMove = false;
            this.colD15.OptionsColumn.AllowSize = false;
            this.colD15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD15.OptionsFilter.AllowAutoFilter = false;
            this.colD15.OptionsFilter.AllowFilter = false;
            this.colD15.Visible = true;
            this.colD15.VisibleIndex = 17;
            this.colD15.Width = 30;
            // 
            // colD16
            // 
            this.colD16.AppearanceCell.Options.UseTextOptions = true;
            this.colD16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD16.Caption = "16";
            this.colD16.FieldName = "D16";
            this.colD16.MaxWidth = 30;
            this.colD16.MinWidth = 30;
            this.colD16.Name = "colD16";
            this.colD16.OptionsColumn.AllowMove = false;
            this.colD16.OptionsColumn.AllowSize = false;
            this.colD16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD16.OptionsFilter.AllowAutoFilter = false;
            this.colD16.OptionsFilter.AllowFilter = false;
            this.colD16.Visible = true;
            this.colD16.VisibleIndex = 18;
            this.colD16.Width = 30;
            // 
            // colD17
            // 
            this.colD17.AppearanceCell.Options.UseTextOptions = true;
            this.colD17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD17.Caption = "17";
            this.colD17.FieldName = "D17";
            this.colD17.MaxWidth = 30;
            this.colD17.MinWidth = 30;
            this.colD17.Name = "colD17";
            this.colD17.OptionsColumn.AllowMove = false;
            this.colD17.OptionsColumn.AllowSize = false;
            this.colD17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD17.OptionsFilter.AllowAutoFilter = false;
            this.colD17.OptionsFilter.AllowFilter = false;
            this.colD17.Visible = true;
            this.colD17.VisibleIndex = 19;
            this.colD17.Width = 30;
            // 
            // colD18
            // 
            this.colD18.AppearanceCell.Options.UseTextOptions = true;
            this.colD18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD18.Caption = "18";
            this.colD18.FieldName = "D18";
            this.colD18.MaxWidth = 30;
            this.colD18.MinWidth = 30;
            this.colD18.Name = "colD18";
            this.colD18.OptionsColumn.AllowMove = false;
            this.colD18.OptionsColumn.AllowSize = false;
            this.colD18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD18.OptionsFilter.AllowAutoFilter = false;
            this.colD18.OptionsFilter.AllowFilter = false;
            this.colD18.Visible = true;
            this.colD18.VisibleIndex = 20;
            this.colD18.Width = 30;
            // 
            // colD19
            // 
            this.colD19.AppearanceCell.Options.UseTextOptions = true;
            this.colD19.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD19.Caption = "19";
            this.colD19.FieldName = "D19";
            this.colD19.MaxWidth = 30;
            this.colD19.MinWidth = 30;
            this.colD19.Name = "colD19";
            this.colD19.OptionsColumn.AllowMove = false;
            this.colD19.OptionsColumn.AllowSize = false;
            this.colD19.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD19.OptionsFilter.AllowAutoFilter = false;
            this.colD19.OptionsFilter.AllowFilter = false;
            this.colD19.Visible = true;
            this.colD19.VisibleIndex = 21;
            this.colD19.Width = 30;
            // 
            // colD20
            // 
            this.colD20.AppearanceCell.Options.UseTextOptions = true;
            this.colD20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD20.Caption = "20";
            this.colD20.FieldName = "D20";
            this.colD20.MaxWidth = 30;
            this.colD20.MinWidth = 30;
            this.colD20.Name = "colD20";
            this.colD20.OptionsColumn.AllowMove = false;
            this.colD20.OptionsColumn.AllowSize = false;
            this.colD20.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD20.OptionsFilter.AllowAutoFilter = false;
            this.colD20.OptionsFilter.AllowFilter = false;
            this.colD20.Visible = true;
            this.colD20.VisibleIndex = 22;
            this.colD20.Width = 30;
            // 
            // colD21
            // 
            this.colD21.AppearanceCell.Options.UseTextOptions = true;
            this.colD21.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD21.Caption = "21";
            this.colD21.FieldName = "D21";
            this.colD21.MaxWidth = 30;
            this.colD21.MinWidth = 30;
            this.colD21.Name = "colD21";
            this.colD21.OptionsColumn.AllowMove = false;
            this.colD21.OptionsColumn.AllowSize = false;
            this.colD21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD21.OptionsFilter.AllowAutoFilter = false;
            this.colD21.OptionsFilter.AllowFilter = false;
            this.colD21.Visible = true;
            this.colD21.VisibleIndex = 23;
            this.colD21.Width = 30;
            // 
            // colD22
            // 
            this.colD22.AppearanceCell.Options.UseTextOptions = true;
            this.colD22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD22.Caption = "22";
            this.colD22.FieldName = "D22";
            this.colD22.MaxWidth = 30;
            this.colD22.MinWidth = 30;
            this.colD22.Name = "colD22";
            this.colD22.OptionsColumn.AllowMove = false;
            this.colD22.OptionsColumn.AllowSize = false;
            this.colD22.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD22.OptionsFilter.AllowAutoFilter = false;
            this.colD22.OptionsFilter.AllowFilter = false;
            this.colD22.Visible = true;
            this.colD22.VisibleIndex = 24;
            this.colD22.Width = 30;
            // 
            // colD23
            // 
            this.colD23.AppearanceCell.Options.UseTextOptions = true;
            this.colD23.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD23.Caption = "23";
            this.colD23.FieldName = "D23";
            this.colD23.MaxWidth = 30;
            this.colD23.MinWidth = 30;
            this.colD23.Name = "colD23";
            this.colD23.OptionsColumn.AllowMove = false;
            this.colD23.OptionsColumn.AllowSize = false;
            this.colD23.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD23.OptionsFilter.AllowAutoFilter = false;
            this.colD23.OptionsFilter.AllowFilter = false;
            this.colD23.Visible = true;
            this.colD23.VisibleIndex = 25;
            this.colD23.Width = 30;
            // 
            // colD24
            // 
            this.colD24.AppearanceCell.Options.UseTextOptions = true;
            this.colD24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD24.Caption = "24";
            this.colD24.FieldName = "D24";
            this.colD24.MaxWidth = 30;
            this.colD24.MinWidth = 30;
            this.colD24.Name = "colD24";
            this.colD24.OptionsColumn.AllowMove = false;
            this.colD24.OptionsColumn.AllowSize = false;
            this.colD24.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD24.OptionsFilter.AllowAutoFilter = false;
            this.colD24.OptionsFilter.AllowFilter = false;
            this.colD24.Visible = true;
            this.colD24.VisibleIndex = 26;
            this.colD24.Width = 30;
            // 
            // colD25
            // 
            this.colD25.AppearanceCell.Options.UseTextOptions = true;
            this.colD25.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD25.Caption = "25";
            this.colD25.FieldName = "D25";
            this.colD25.MaxWidth = 30;
            this.colD25.MinWidth = 30;
            this.colD25.Name = "colD25";
            this.colD25.OptionsColumn.AllowMove = false;
            this.colD25.OptionsColumn.AllowSize = false;
            this.colD25.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD25.OptionsFilter.AllowAutoFilter = false;
            this.colD25.OptionsFilter.AllowFilter = false;
            this.colD25.Visible = true;
            this.colD25.VisibleIndex = 27;
            this.colD25.Width = 30;
            // 
            // colD26
            // 
            this.colD26.AppearanceCell.Options.UseTextOptions = true;
            this.colD26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD26.Caption = "26";
            this.colD26.FieldName = "D26";
            this.colD26.MaxWidth = 30;
            this.colD26.MinWidth = 30;
            this.colD26.Name = "colD26";
            this.colD26.OptionsColumn.AllowMove = false;
            this.colD26.OptionsColumn.AllowSize = false;
            this.colD26.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD26.OptionsFilter.AllowAutoFilter = false;
            this.colD26.OptionsFilter.AllowFilter = false;
            this.colD26.Visible = true;
            this.colD26.VisibleIndex = 28;
            this.colD26.Width = 30;
            // 
            // colD27
            // 
            this.colD27.AppearanceCell.Options.UseTextOptions = true;
            this.colD27.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD27.Caption = "27";
            this.colD27.FieldName = "D27";
            this.colD27.MaxWidth = 30;
            this.colD27.MinWidth = 30;
            this.colD27.Name = "colD27";
            this.colD27.OptionsColumn.AllowMove = false;
            this.colD27.OptionsColumn.AllowSize = false;
            this.colD27.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD27.OptionsFilter.AllowAutoFilter = false;
            this.colD27.OptionsFilter.AllowFilter = false;
            this.colD27.Visible = true;
            this.colD27.VisibleIndex = 29;
            this.colD27.Width = 30;
            // 
            // colD28
            // 
            this.colD28.AppearanceCell.Options.UseTextOptions = true;
            this.colD28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD28.Caption = "28";
            this.colD28.FieldName = "D28";
            this.colD28.MaxWidth = 30;
            this.colD28.MinWidth = 30;
            this.colD28.Name = "colD28";
            this.colD28.OptionsColumn.AllowMove = false;
            this.colD28.OptionsColumn.AllowSize = false;
            this.colD28.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD28.OptionsFilter.AllowAutoFilter = false;
            this.colD28.OptionsFilter.AllowFilter = false;
            this.colD28.Visible = true;
            this.colD28.VisibleIndex = 30;
            this.colD28.Width = 30;
            // 
            // colD29
            // 
            this.colD29.AppearanceCell.Options.UseTextOptions = true;
            this.colD29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD29.Caption = "29";
            this.colD29.FieldName = "D29";
            this.colD29.MaxWidth = 30;
            this.colD29.MinWidth = 30;
            this.colD29.Name = "colD29";
            this.colD29.OptionsColumn.AllowMove = false;
            this.colD29.OptionsColumn.AllowSize = false;
            this.colD29.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD29.OptionsFilter.AllowAutoFilter = false;
            this.colD29.OptionsFilter.AllowFilter = false;
            this.colD29.Visible = true;
            this.colD29.VisibleIndex = 31;
            this.colD29.Width = 30;
            // 
            // colD30
            // 
            this.colD30.AppearanceCell.Options.UseTextOptions = true;
            this.colD30.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD30.Caption = "30";
            this.colD30.FieldName = "D30";
            this.colD30.MaxWidth = 30;
            this.colD30.MinWidth = 30;
            this.colD30.Name = "colD30";
            this.colD30.OptionsColumn.AllowMove = false;
            this.colD30.OptionsColumn.AllowSize = false;
            this.colD30.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD30.OptionsFilter.AllowAutoFilter = false;
            this.colD30.OptionsFilter.AllowFilter = false;
            this.colD30.Visible = true;
            this.colD30.VisibleIndex = 32;
            this.colD30.Width = 30;
            // 
            // colD31
            // 
            this.colD31.AppearanceCell.Options.UseTextOptions = true;
            this.colD31.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colD31.Caption = "31";
            this.colD31.FieldName = "D31";
            this.colD31.MaxWidth = 30;
            this.colD31.MinWidth = 30;
            this.colD31.Name = "colD31";
            this.colD31.OptionsColumn.AllowMove = false;
            this.colD31.OptionsColumn.AllowSize = false;
            this.colD31.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colD31.OptionsFilter.AllowAutoFilter = false;
            this.colD31.OptionsFilter.AllowFilter = false;
            this.colD31.Visible = true;
            this.colD31.VisibleIndex = 33;
            this.colD31.Width = 30;
            // 
            // colTotalLuch
            // 
            this.colTotalLuch.Caption = "Ăn ca ngày";
            this.colTotalLuch.FieldName = "TotalLuch";
            this.colTotalLuch.Name = "colTotalLuch";
            this.colTotalLuch.Visible = true;
            this.colTotalLuch.VisibleIndex = 34;
            this.colTotalLuch.Width = 61;
            // 
            // colTotalDinner
            // 
            this.colTotalDinner.Caption = "Ăn ca đêm";
            this.colTotalDinner.FieldName = "TotalDinner";
            this.colTotalDinner.Name = "colTotalDinner";
            this.colTotalDinner.Visible = true;
            this.colTotalDinner.VisibleIndex = 35;
            this.colTotalDinner.Width = 46;
            // 
            // colTotal
            // 
            this.colTotal.Caption = "Tổng số ăn ca";
            this.colTotal.FieldName = "countTotal";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsFilter.AllowAutoFilter = false;
            this.colTotal.OptionsFilter.AllowFilter = false;
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 36;
            this.colTotal.Width = 63;
            // 
            // colTotalFoodOrder
            // 
            this.colTotalFoodOrder.Caption = "Số ăn ca tại công ty";
            this.colTotalFoodOrder.FieldName = "TotalFoodOrder";
            this.colTotalFoodOrder.Name = "colTotalFoodOrder";
            this.colTotalFoodOrder.Visible = true;
            this.colTotalFoodOrder.VisibleIndex = 37;
            this.colTotalFoodOrder.Width = 78;
            // 
            // colTotalActual
            // 
            this.colTotalActual.Caption = "Số ăn ca được hưởng";
            this.colTotalActual.FieldName = "TotalActual";
            this.colTotalActual.Name = "colTotalActual";
            this.colTotalActual.Visible = true;
            this.colTotalActual.VisibleIndex = 38;
            this.colTotalActual.Width = 103;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 36;
            this.colDepartmentName.Width = 20;
            // 
            // colDepartmentID
            // 
            this.colDepartmentID.Caption = "gridColumn1";
            this.colDepartmentID.FieldName = "DepartmentID";
            this.colDepartmentID.Name = "colDepartmentID";
            // 
            // frmEmployeeFoodOrder
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 786);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.stackPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEmployeeFoodOrder";
            this.Text = "CHI TIẾT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEmployeeFoodOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtMonth;
        private System.Windows.Forms.Button btnSearch;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colD1;
        private DevExpress.XtraGrid.Columns.GridColumn colD2;
        private DevExpress.XtraGrid.Columns.GridColumn colD3;
        private DevExpress.XtraGrid.Columns.GridColumn colD4;
        private DevExpress.XtraGrid.Columns.GridColumn colD5;
        private DevExpress.XtraGrid.Columns.GridColumn colD6;
        private DevExpress.XtraGrid.Columns.GridColumn colD7;
        private DevExpress.XtraGrid.Columns.GridColumn colD8;
        private DevExpress.XtraGrid.Columns.GridColumn colD9;
        private DevExpress.XtraGrid.Columns.GridColumn colD10;
        private DevExpress.XtraGrid.Columns.GridColumn colD11;
        private DevExpress.XtraGrid.Columns.GridColumn colD12;
        private DevExpress.XtraGrid.Columns.GridColumn colD13;
        private DevExpress.XtraGrid.Columns.GridColumn colD14;
        private DevExpress.XtraGrid.Columns.GridColumn colD15;
        private DevExpress.XtraGrid.Columns.GridColumn colD16;
        private DevExpress.XtraGrid.Columns.GridColumn colD17;
        private DevExpress.XtraGrid.Columns.GridColumn colD18;
        private DevExpress.XtraGrid.Columns.GridColumn colD19;
        private DevExpress.XtraGrid.Columns.GridColumn colD20;
        private DevExpress.XtraGrid.Columns.GridColumn colD21;
        private DevExpress.XtraGrid.Columns.GridColumn colD22;
        private DevExpress.XtraGrid.Columns.GridColumn colD23;
        private DevExpress.XtraGrid.Columns.GridColumn colD24;
        private DevExpress.XtraGrid.Columns.GridColumn colD25;
        private DevExpress.XtraGrid.Columns.GridColumn colD26;
        private DevExpress.XtraGrid.Columns.GridColumn colD27;
        private DevExpress.XtraGrid.Columns.GridColumn colD28;
        private DevExpress.XtraGrid.Columns.GridColumn colD29;
        private DevExpress.XtraGrid.Columns.GridColumn colD30;
        private DevExpress.XtraGrid.Columns.GridColumn colD31;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.Button btnImportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalLuch;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDinner;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalFoodOrder;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalActual;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentID;
    }
}