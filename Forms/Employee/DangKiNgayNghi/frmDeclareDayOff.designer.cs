
namespace BMS
{
    partial class frmDeclareDayOff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeclareDayOff));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDayInYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDayOnLeave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDayNoOnLeave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDayRemain = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYearOnLeave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator6,
            this.btnEdit,
            this.toolStripSeparator7,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnImportExcel,
            this.toolStripSeparator2,
            this.btnExportExcel});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(967, 42);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 39);
            this.btnAdd.Tag = "frmDayOff_HRUse";
            this.btnAdd.Text = "&Thêm";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 41);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 39);
            this.btnEdit.Tag = "frmDayOff_HRUse";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 41);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 39);
            this.btnDelete.Tag = "frmDayOff_HRUse";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.Image = global::Forms.Properties.Resources.import;
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(81, 36);
            this.btnImportExcel.Text = "Nhập excel";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = global::Forms.Properties.Resources.ExportToXLS_32x32;
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(80, 36);
            this.btnExportExcel.Text = "Xuất excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Location = new System.Drawing.Point(0, 42);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.Size = new System.Drawing.Size(967, 418);
            this.grdMaster.TabIndex = 23;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            // 
            // grvMaster
            // 
            this.grvMaster.ColumnPanelRowHeight = 50;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeCode,
            this.colEmployeeName,
            this.colTotalDayInYear,
            this.colTotalDayOnLeave,
            this.colTotalDayNoOnLeave,
            this.colTotalDayRemain,
            this.colYearOnLeave,
            this.colDepartmentName});
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.GroupCount = 1;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsFind.AlwaysVisible = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.RowHeight = 25;
            this.grvMaster.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvMaster.DoubleClick += new System.EventHandler(this.grvMaster_DoubleClick);
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceCell.Options.HighPriority = true;
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceHeader.Options.UseBackColor = true;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colID.OptionsFilter.AllowAutoFilter = false;
            this.colID.OptionsFilter.AllowFilter = false;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeCode.AppearanceCell.Options.HighPriority = true;
            this.colEmployeeCode.AppearanceCell.Options.UseFont = true;
            this.colEmployeeCode.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployeeCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmployeeCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeCode.AppearanceHeader.Options.UseBackColor = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployeeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmployeeCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeCode.Caption = "Mã nhân viên";
            this.colEmployeeCode.FieldName = "Code";
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.VisibleIndex = 0;
            this.colEmployeeCode.Width = 153;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeName.AppearanceCell.Options.HighPriority = true;
            this.colEmployeeName.AppearanceCell.Options.UseFont = true;
            this.colEmployeeName.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployeeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colEmployeeName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmployeeName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeName.AppearanceHeader.Options.UseBackColor = true;
            this.colEmployeeName.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeName.AppearanceHeader.Options.UseForeColor = true;
            this.colEmployeeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployeeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEmployeeName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeName.Caption = "Tên nhân viên";
            this.colEmployeeName.FieldName = "FullName";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 1;
            this.colEmployeeName.Width = 493;
            // 
            // colTotalDayInYear
            // 
            this.colTotalDayInYear.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayInYear.AppearanceCell.Options.HighPriority = true;
            this.colTotalDayInYear.AppearanceCell.Options.UseFont = true;
            this.colTotalDayInYear.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayInYear.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDayInYear.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayInYear.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayInYear.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayInYear.AppearanceHeader.Options.UseBackColor = true;
            this.colTotalDayInYear.AppearanceHeader.Options.UseFont = true;
            this.colTotalDayInYear.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDayInYear.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDayInYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDayInYear.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayInYear.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayInYear.Caption = "Tổng số ngày phép";
            this.colTotalDayInYear.DisplayFormat.FormatString = "N";
            this.colTotalDayInYear.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalDayInYear.FieldName = "TotalDayInYear";
            this.colTotalDayInYear.Name = "colTotalDayInYear";
            this.colTotalDayInYear.Visible = true;
            this.colTotalDayInYear.VisibleIndex = 3;
            this.colTotalDayInYear.Width = 163;
            // 
            // colTotalDayOnLeave
            // 
            this.colTotalDayOnLeave.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayOnLeave.AppearanceCell.Options.HighPriority = true;
            this.colTotalDayOnLeave.AppearanceCell.Options.UseFont = true;
            this.colTotalDayOnLeave.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayOnLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDayOnLeave.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayOnLeave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayOnLeave.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayOnLeave.AppearanceHeader.Options.UseBackColor = true;
            this.colTotalDayOnLeave.AppearanceHeader.Options.UseFont = true;
            this.colTotalDayOnLeave.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDayOnLeave.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDayOnLeave.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDayOnLeave.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayOnLeave.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayOnLeave.Caption = "Tổng số ngày nghỉ phép";
            this.colTotalDayOnLeave.DisplayFormat.FormatString = "N";
            this.colTotalDayOnLeave.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalDayOnLeave.FieldName = "TotalDayOnLeave";
            this.colTotalDayOnLeave.Name = "colTotalDayOnLeave";
            this.colTotalDayOnLeave.OptionsColumn.AllowEdit = false;
            this.colTotalDayOnLeave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalDayOnLeave.OptionsColumn.ReadOnly = true;
            this.colTotalDayOnLeave.OptionsFilter.AllowAutoFilter = false;
            this.colTotalDayOnLeave.OptionsFilter.AllowFilter = false;
            this.colTotalDayOnLeave.Width = 138;
            // 
            // colTotalDayNoOnLeave
            // 
            this.colTotalDayNoOnLeave.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayNoOnLeave.AppearanceCell.Options.HighPriority = true;
            this.colTotalDayNoOnLeave.AppearanceCell.Options.UseFont = true;
            this.colTotalDayNoOnLeave.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayNoOnLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDayNoOnLeave.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayNoOnLeave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayNoOnLeave.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayNoOnLeave.AppearanceHeader.Options.UseBackColor = true;
            this.colTotalDayNoOnLeave.AppearanceHeader.Options.UseFont = true;
            this.colTotalDayNoOnLeave.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDayNoOnLeave.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDayNoOnLeave.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDayNoOnLeave.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayNoOnLeave.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayNoOnLeave.Caption = "Tổng số ngày nghỉ không phép";
            this.colTotalDayNoOnLeave.DisplayFormat.FormatString = "N";
            this.colTotalDayNoOnLeave.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalDayNoOnLeave.FieldName = "TotalDayNoOnLeave";
            this.colTotalDayNoOnLeave.Name = "colTotalDayNoOnLeave";
            this.colTotalDayNoOnLeave.OptionsColumn.AllowEdit = false;
            this.colTotalDayNoOnLeave.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalDayNoOnLeave.OptionsColumn.ReadOnly = true;
            this.colTotalDayNoOnLeave.OptionsFilter.AllowAutoFilter = false;
            this.colTotalDayNoOnLeave.OptionsFilter.AllowFilter = false;
            this.colTotalDayNoOnLeave.Width = 162;
            // 
            // colTotalDayRemain
            // 
            this.colTotalDayRemain.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayRemain.AppearanceCell.Options.HighPriority = true;
            this.colTotalDayRemain.AppearanceCell.Options.UseFont = true;
            this.colTotalDayRemain.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayRemain.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDayRemain.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayRemain.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayRemain.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDayRemain.AppearanceHeader.Options.UseBackColor = true;
            this.colTotalDayRemain.AppearanceHeader.Options.UseFont = true;
            this.colTotalDayRemain.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDayRemain.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDayRemain.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDayRemain.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayRemain.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDayRemain.Caption = "Tổng số phép còn lại";
            this.colTotalDayRemain.DisplayFormat.FormatString = "N";
            this.colTotalDayRemain.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalDayRemain.FieldName = "TotalDayRemain";
            this.colTotalDayRemain.Name = "colTotalDayRemain";
            this.colTotalDayRemain.OptionsColumn.AllowEdit = false;
            this.colTotalDayRemain.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotalDayRemain.OptionsColumn.ReadOnly = true;
            this.colTotalDayRemain.OptionsFilter.AllowAutoFilter = false;
            this.colTotalDayRemain.OptionsFilter.AllowFilter = false;
            this.colTotalDayRemain.Width = 168;
            // 
            // colYearOnLeave
            // 
            this.colYearOnLeave.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colYearOnLeave.AppearanceCell.Options.HighPriority = true;
            this.colYearOnLeave.AppearanceCell.Options.UseFont = true;
            this.colYearOnLeave.AppearanceCell.Options.UseTextOptions = true;
            this.colYearOnLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colYearOnLeave.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colYearOnLeave.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colYearOnLeave.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colYearOnLeave.AppearanceHeader.Options.UseBackColor = true;
            this.colYearOnLeave.AppearanceHeader.Options.UseFont = true;
            this.colYearOnLeave.AppearanceHeader.Options.UseForeColor = true;
            this.colYearOnLeave.AppearanceHeader.Options.UseTextOptions = true;
            this.colYearOnLeave.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colYearOnLeave.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colYearOnLeave.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colYearOnLeave.Caption = "Năm";
            this.colYearOnLeave.FieldName = "YearOnleave";
            this.colYearOnLeave.Name = "colYearOnLeave";
            this.colYearOnLeave.Visible = true;
            this.colYearOnLeave.VisibleIndex = 2;
            this.colYearOnLeave.Width = 133;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 4;
            // 
            // frmDeclareDayOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 460);
            this.Controls.Add(this.grdMaster);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDeclareDayOff";
            this.Text = "KHAI BÁO NGÀY PHÉP";
            this.Load += new System.EventHandler(this.frmDeclareDayOff_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDayInYear;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDayOnLeave;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDayNoOnLeave;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDayRemain;
        private DevExpress.XtraGrid.Columns.GridColumn colYearOnLeave;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
    }
}