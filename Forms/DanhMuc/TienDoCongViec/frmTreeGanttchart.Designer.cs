
namespace Forms.DanhMuc.DuAn
{
    partial class frmGanttChartProjectItem
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
            this.cboProject = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.tlGanttChartProjectItem = new DevExpress.XtraTreeList.TreeList();
            this.bandTitle = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.bandProject = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.bandCustomer = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.bandPlan = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.colSTT = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMission = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.bandProjectName = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.bandCustomerName = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.bandPlanDetail = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.colStart = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalDays = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFinish = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bandNote = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlGanttChartProjectItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboProject
            // 
            this.cboProject.EditValue = "";
            this.cboProject.Location = new System.Drawing.Point(63, 7);
            this.cboProject.Name = "cboProject";
            this.cboProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProject.Properties.NullText = "";
            this.cboProject.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboProject.Size = new System.Drawing.Size(327, 20);
            this.cboProject.TabIndex = 0;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.gridColumn2,
            this.gridColumn3});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.Editable = false;
            this.searchLookUpEdit1View.OptionsBehavior.ReadOnly = true;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.Caption = "Mã dự án";
            this.gridColumn2.FieldName = "ProjectCode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.Caption = "Tên dự án";
            this.gridColumn3.FieldName = "ProjectName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dự án";
            // 
            // btnShow
            // 
            this.btnShow.AutoSize = true;
            this.btnShow.Location = new System.Drawing.Point(396, 6);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "Xem tiến độ";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn1.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.treeListColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn1.Caption = "Ngày bắt đầu";
            this.treeListColumn1.FieldName = "PlanDateStart";
            this.treeListColumn1.MinWidth = 120;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 1;
            this.treeListColumn1.Width = 120;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.treeListColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListColumn2.Caption = "Ngày bắt đầu";
            this.treeListColumn2.FieldName = "PlanDateStart";
            this.treeListColumn2.MinWidth = 120;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 120;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AutoSize = true;
            this.btnExportToExcel.Location = new System.Drawing.Point(477, 6);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportToExcel.TabIndex = 2;
            this.btnExportToExcel.Text = "Xuất excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // tlGanttChartProjectItem
            // 
            this.tlGanttChartProjectItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlGanttChartProjectItem.Appearance.BandPanel.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlGanttChartProjectItem.Appearance.BandPanel.Options.UseFont = true;
            this.tlGanttChartProjectItem.AutoFillFieldName = " c";
            this.tlGanttChartProjectItem.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.bandTitle,
            this.bandNote});
            this.tlGanttChartProjectItem.ColumnPanelRowHeight = 60;
            this.tlGanttChartProjectItem.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colSTT,
            this.colMission,
            this.colStart,
            this.colTotalDays,
            this.colFinish,
            this.colType});
            this.tlGanttChartProjectItem.Location = new System.Drawing.Point(12, 35);
            this.tlGanttChartProjectItem.Name = "tlGanttChartProjectItem";
            this.tlGanttChartProjectItem.OptionsBehavior.Editable = false;
            this.tlGanttChartProjectItem.OptionsBehavior.PopulateServiceColumns = true;
            this.tlGanttChartProjectItem.OptionsBehavior.ReadOnly = true;
            this.tlGanttChartProjectItem.OptionsCustomization.AllowBandMoving = false;
            this.tlGanttChartProjectItem.OptionsCustomization.AllowFilter = false;
            this.tlGanttChartProjectItem.OptionsCustomization.AllowSort = false;
            this.tlGanttChartProjectItem.OptionsFilter.AllowFilterEditor = false;
            this.tlGanttChartProjectItem.OptionsView.ShowIndicator = false;
            this.tlGanttChartProjectItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
            this.tlGanttChartProjectItem.Size = new System.Drawing.Size(1304, 306);
            this.tlGanttChartProjectItem.TabIndex = 3;
            this.tlGanttChartProjectItem.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.tlGanttChartProjectItem_NodeCellStyle);
            this.tlGanttChartProjectItem.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlGanttChartProjectItem_FocusedNodeChanged);
            // 
            // bandTitle
            // 
            this.bandTitle.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandTitle.AppearanceHeader.Options.UseFont = true;
            this.bandTitle.AppearanceHeader.Options.UseForeColor = true;
            this.bandTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.bandTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandTitle.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.bandProject,
            this.bandProjectName});
            this.bandTitle.Caption = "KẾ HOẠCH DỰ KIẾN TRIỂN KHAI DỰ ÁN";
            this.bandTitle.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.bandTitle.Name = "bandTitle";
            this.bandTitle.OptionsBand.AllowMove = false;
            this.bandTitle.OptionsBand.AllowSize = false;
            this.bandTitle.OptionsBand.FixedWidth = true;
            this.bandTitle.RowCount = 3;
            this.bandTitle.Width = 541;
            // 
            // bandProject
            // 
            this.bandProject.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandProject.AppearanceHeader.Options.UseFont = true;
            this.bandProject.AppearanceHeader.Options.UseForeColor = true;
            this.bandProject.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.bandCustomer});
            this.bandProject.Caption = "TÊN DỰ ÁN / PROJECT NAME";
            this.bandProject.Name = "bandProject";
            this.bandProject.OptionsBand.AllowMove = false;
            this.bandProject.OptionsBand.AllowSize = false;
            this.bandProject.OptionsBand.FixedWidth = true;
            this.bandProject.RowCount = 2;
            this.bandProject.Width = 224;
            // 
            // bandCustomer
            // 
            this.bandCustomer.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandCustomer.AppearanceHeader.Options.UseFont = true;
            this.bandCustomer.AppearanceHeader.Options.UseForeColor = true;
            this.bandCustomer.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.bandPlan});
            this.bandCustomer.Caption = "KHÁCH HÀNG / CUSTOMER";
            this.bandCustomer.Name = "bandCustomer";
            this.bandCustomer.OptionsBand.AllowMove = false;
            this.bandCustomer.OptionsBand.AllowSize = false;
            this.bandCustomer.OptionsBand.FixedWidth = true;
            this.bandCustomer.RowCount = 2;
            this.bandCustomer.Width = 224;
            // 
            // bandPlan
            // 
            this.bandPlan.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandPlan.AppearanceHeader.Options.UseFont = true;
            this.bandPlan.AppearanceHeader.Options.UseForeColor = true;
            this.bandPlan.Caption = "KẾ HOẠCH TRIỂN KHAI";
            this.bandPlan.Columns.Add(this.colSTT);
            this.bandPlan.Columns.Add(this.colMission);
            this.bandPlan.Name = "bandPlan";
            this.bandPlan.OptionsBand.AllowMove = false;
            this.bandPlan.OptionsBand.AllowSize = false;
            this.bandPlan.OptionsBand.FixedWidth = true;
            this.bandPlan.RowCount = 2;
            this.bandPlan.Width = 224;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSTT.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSTT.AppearanceHeader.Options.UseFont = true;
            this.colSTT.AppearanceHeader.Options.UseForeColor = true;
            this.colSTT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSTT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.Caption = "Stt / No";
            this.colSTT.FieldName = "STT";
            this.colSTT.MaxWidth = 30;
            this.colSTT.MinWidth = 30;
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 30;
            // 
            // colMission
            // 
            this.colMission.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMission.AppearanceCell.Options.UseFont = true;
            this.colMission.AppearanceCell.Options.UseTextOptions = true;
            this.colMission.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMission.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMission.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMission.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMission.AppearanceHeader.Options.UseFont = true;
            this.colMission.AppearanceHeader.Options.UseForeColor = true;
            this.colMission.AppearanceHeader.Options.UseTextOptions = true;
            this.colMission.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMission.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMission.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMission.Caption = "Nội dung công việc / Content";
            this.colMission.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colMission.FieldName = "Mission";
            this.colMission.MaxWidth = 202;
            this.colMission.MinWidth = 202;
            this.colMission.Name = "colMission";
            this.colMission.Visible = true;
            this.colMission.VisibleIndex = 1;
            this.colMission.Width = 202;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // bandProjectName
            // 
            this.bandProjectName.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandProjectName.AppearanceHeader.Options.UseFont = true;
            this.bandProjectName.AppearanceHeader.Options.UseForeColor = true;
            this.bandProjectName.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.bandCustomerName});
            this.bandProjectName.Name = "bandProjectName";
            this.bandProjectName.OptionsBand.AllowMove = false;
            this.bandProjectName.OptionsBand.AllowSize = false;
            this.bandProjectName.OptionsBand.FixedWidth = true;
            this.bandProjectName.RowCount = 2;
            this.bandProjectName.Width = 317;
            // 
            // bandCustomerName
            // 
            this.bandCustomerName.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandCustomerName.AppearanceHeader.Options.UseFont = true;
            this.bandCustomerName.AppearanceHeader.Options.UseForeColor = true;
            this.bandCustomerName.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.bandPlanDetail});
            this.bandCustomerName.Name = "bandCustomerName";
            this.bandCustomerName.OptionsBand.AllowMove = false;
            this.bandCustomerName.OptionsBand.AllowSize = false;
            this.bandCustomerName.OptionsBand.FixedWidth = true;
            this.bandCustomerName.RowCount = 2;
            this.bandCustomerName.Width = 317;
            // 
            // bandPlanDetail
            // 
            this.bandPlanDetail.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandPlanDetail.AppearanceHeader.Options.UseFont = true;
            this.bandPlanDetail.AppearanceHeader.Options.UseForeColor = true;
            this.bandPlanDetail.Columns.Add(this.colStart);
            this.bandPlanDetail.Columns.Add(this.colTotalDays);
            this.bandPlanDetail.Columns.Add(this.colFinish);
            this.bandPlanDetail.Columns.Add(this.colType);
            this.bandPlanDetail.Name = "bandPlanDetail";
            this.bandPlanDetail.OptionsBand.AllowMove = false;
            this.bandPlanDetail.OptionsBand.AllowSize = false;
            this.bandPlanDetail.OptionsBand.FixedWidth = true;
            this.bandPlanDetail.RowCount = 2;
            this.bandPlanDetail.Width = 317;
            // 
            // colStart
            // 
            this.colStart.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStart.AppearanceCell.Options.UseFont = true;
            this.colStart.AppearanceCell.Options.UseTextOptions = true;
            this.colStart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStart.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStart.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStart.AppearanceHeader.Options.UseFont = true;
            this.colStart.AppearanceHeader.Options.UseForeColor = true;
            this.colStart.AppearanceHeader.Options.UseTextOptions = true;
            this.colStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStart.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStart.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStart.Caption = "Start";
            this.colStart.FieldName = "StartDate";
            this.colStart.MaxWidth = 80;
            this.colStart.MinWidth = 80;
            this.colStart.Name = "colStart";
            this.colStart.Visible = true;
            this.colStart.VisibleIndex = 2;
            this.colStart.Width = 80;
            // 
            // colTotalDays
            // 
            this.colTotalDays.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalDays.AppearanceHeader.Options.UseFont = true;
            this.colTotalDays.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalDays.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalDays.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalDays.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDays.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalDays.Caption = "Working days";
            this.colTotalDays.FieldName = "TotalDay";
            this.colTotalDays.MaxWidth = 60;
            this.colTotalDays.MinWidth = 60;
            this.colTotalDays.Name = "colTotalDays";
            this.colTotalDays.Visible = true;
            this.colTotalDays.VisibleIndex = 3;
            this.colTotalDays.Width = 60;
            // 
            // colFinish
            // 
            this.colFinish.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFinish.AppearanceCell.Options.UseFont = true;
            this.colFinish.AppearanceCell.Options.UseTextOptions = true;
            this.colFinish.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFinish.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFinish.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFinish.AppearanceHeader.Options.UseFont = true;
            this.colFinish.AppearanceHeader.Options.UseForeColor = true;
            this.colFinish.AppearanceHeader.Options.UseTextOptions = true;
            this.colFinish.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFinish.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFinish.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFinish.Caption = "Finish";
            this.colFinish.FieldName = "EndDate";
            this.colFinish.MaxWidth = 80;
            this.colFinish.MinWidth = 80;
            this.colFinish.Name = "colFinish";
            this.colFinish.Visible = true;
            this.colFinish.VisibleIndex = 4;
            this.colFinish.Width = 80;
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colType.AppearanceHeader.Options.UseFont = true;
            this.colType.AppearanceHeader.Options.UseForeColor = true;
            this.colType.AppearanceHeader.Options.UseTextOptions = true;
            this.colType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colType.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colType.Caption = "Tiến độ / Progress";
            this.colType.FieldName = "TypeText";
            this.colType.MaxWidth = 60;
            this.colType.MinWidth = 60;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 5;
            this.colType.Width = 60;
            // 
            // bandNote
            // 
            this.bandNote.Name = "bandNote";
            this.bandNote.RowCount = 7;
            this.bandNote.Width = 909;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 361);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(817, 181);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // frmGanttChartProjectItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 624);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.tlGanttChartProjectItem);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProject);
            this.Name = "frmGanttChartProjectItem";
            this.Text = "TIẾN ĐỘ CÔNG VIỆC";
            this.Load += new System.EventHandler(this.frmTreeGanttchart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlGanttChartProjectItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit cboProject;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShow;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private System.Windows.Forms.Button btnExportToExcel;
        private DevExpress.XtraTreeList.TreeList tlGanttChartProjectItem;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMission;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colStart;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFinish;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSTT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalDays;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandTitle;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandProject;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandCustomer;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandPlan;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandProjectName;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandCustomerName;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandPlanDetail;
        private DevExpress.XtraTreeList.Columns.TreeListBand bandNote;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}