
namespace BMS
{
    partial class frmProjectSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectSummary));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.cboBusinessField = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView9 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBussinessID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBussinessCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBussinessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colBussinessNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label32 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboProjectType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ProjectType = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLeader = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboPM = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbUser = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.stackPanel2 = new DevExpress.Utils.Layout.StackPanel();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.txtShowCount = new System.Windows.Forms.TextBox();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDMaster = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriotityText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullNameTech = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContactEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanDS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanDE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualDS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualDE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullNamePM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPersonalPriotity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusOld = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPMID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserIDSale = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBussinessField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectProcessType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserMission = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.cboProjectStatus = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatusID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboUserTech = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettingTree = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPersonalPriotity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectPartList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectWorker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSummaryWorker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.bntProjectItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGranttChartProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectEmployees = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.btnChangeProjectReport = new System.Windows.Forms.ToolStripButton();
            this.btnIsApproved = new System.Windows.Forms.ToolStripButton();
            this.btnCancelApprove = new System.Windows.Forms.ToolStripButton();
            this.btnGroupFile = new System.Windows.Forms.ToolStripButton();
            this.btnProjectCost = new System.Windows.Forms.ToolStripButton();
            this.btnProjectType = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlProjectTypeMaster = new DevExpress.XtraTreeList.TreeList();
            this.colSeleted = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colProjectTypeMaster = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLeaderMater = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProjectTypeID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLeaderID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.grdProjectItem = new DevExpress.XtraGrid.GridControl();
            this.grvProjectItem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMission = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colIdProjectItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserProjectItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlanEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoteProjectItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colTotalDayPlan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPercentItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPercentageActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeRequest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemLateActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboUserID = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.cboStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnDeleteRepo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDownloadFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDownloadFile = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colGroupFileCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPathShort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDownloadGroupFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDownloadGroupFile = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDownloadProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDownloadProject = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colPathFull = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBusinessField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel2)).BeginInit();
            this.stackPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserTech.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlProjectTypeMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProjectItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProjectItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRepo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownloadFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownloadGroupFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownloadProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExcelExport);
            this.panel1.Controls.Add(this.cboBusinessField);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cboProjectType);
            this.panel1.Controls.Add(this.ProjectType);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbCustomer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboLeader);
            this.panel1.Controls.Add(this.cboPM);
            this.panel1.Controls.Add(this.cbUser);
            this.panel1.Controls.Add(this.txtFilterText);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.stackPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1594, 63);
            this.panel1.TabIndex = 33;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(1112, 28);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(69, 23);
            this.btnExcelExport.TabIndex = 253;
            this.btnExcelExport.Text = "Xuất Excel";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // cboBusinessField
            // 
            this.cboBusinessField.Location = new System.Drawing.Point(999, 1);
            this.cboBusinessField.Name = "cboBusinessField";
            this.cboBusinessField.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBusinessField.Properties.Appearance.Options.UseFont = true;
            this.cboBusinessField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboBusinessField.Properties.NullText = "";
            this.cboBusinessField.Properties.PopupView = this.gridView9;
            this.cboBusinessField.Properties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.cboBusinessField.Size = new System.Drawing.Size(182, 22);
            this.cboBusinessField.TabIndex = 252;
            this.cboBusinessField.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // gridView9
            // 
            this.gridView9.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView9.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView9.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView9.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView9.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView9.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView9.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView9.Appearance.Row.Options.UseTextOptions = true;
            this.gridView9.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView9.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView9.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBussinessID,
            this.colBussinessCode,
            this.colBussinessName,
            this.colBussinessNote});
            this.gridView9.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView9.Name = "gridView9";
            this.gridView9.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView9.OptionsView.ShowGroupPanel = false;
            // 
            // colBussinessID
            // 
            this.colBussinessID.Caption = "ID";
            this.colBussinessID.FieldName = "ID";
            this.colBussinessID.Name = "colBussinessID";
            // 
            // colBussinessCode
            // 
            this.colBussinessCode.Caption = "Mã lĩnh vực";
            this.colBussinessCode.FieldName = "Code";
            this.colBussinessCode.Name = "colBussinessCode";
            this.colBussinessCode.Visible = true;
            this.colBussinessCode.VisibleIndex = 0;
            this.colBussinessCode.Width = 164;
            // 
            // colBussinessName
            // 
            this.colBussinessName.Caption = "Tên lĩnh vực";
            this.colBussinessName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBussinessName.FieldName = "Name";
            this.colBussinessName.Name = "colBussinessName";
            this.colBussinessName.Visible = true;
            this.colBussinessName.VisibleIndex = 1;
            this.colBussinessName.Width = 369;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colBussinessNote
            // 
            this.colBussinessNote.Caption = "Ghi chú";
            this.colBussinessNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colBussinessNote.FieldName = "Note";
            this.colBussinessNote.Name = "colBussinessNote";
            this.colBussinessNote.Visible = true;
            this.colBussinessNote.VisibleIndex = 2;
            this.colBussinessNote.Width = 293;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(897, 4);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(94, 16);
            this.label32.TabIndex = 251;
            this.label32.Text = "Lĩnh vực dự án";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(722, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 174;
            this.label6.Text = "PM";
            // 
            // cboProjectType
            // 
            this.cboProjectType.EditValue = "";
            this.cboProjectType.Location = new System.Drawing.Point(69, 29);
            this.cboProjectType.Name = "cboProjectType";
            this.cboProjectType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectType.Properties.DropDownRows = 5;
            this.cboProjectType.Properties.PopupFormMinSize = new System.Drawing.Size(200, 230);
            this.cboProjectType.Properties.SelectAllItemCaption = "Tất cả";
            this.cboProjectType.Size = new System.Drawing.Size(225, 20);
            this.cboProjectType.TabIndex = 172;
            this.cboProjectType.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // ProjectType
            // 
            this.ProjectType.AutoSize = true;
            this.ProjectType.Location = new System.Drawing.Point(3, 32);
            this.ProjectType.Name = "ProjectType";
            this.ProjectType.Size = new System.Drawing.Size(58, 13);
            this.ProjectType.TabIndex = 173;
            this.ProjectType.Text = "Kiểu dự án";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(506, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Từ khóa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(506, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 169;
            this.label1.Text = "Khách hàng";
            // 
            // cbCustomer
            // 
            this.cbCustomer.EditValue = "";
            this.cbCustomer.Location = new System.Drawing.Point(576, 3);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCustomer.Properties.NullText = "";
            this.cbCustomer.Properties.PopupFormMinSize = new System.Drawing.Size(1000, 500);
            this.cbCustomer.Properties.PopupView = this.gridView3;
            this.cbCustomer.Size = new System.Drawing.Size(138, 20);
            this.cbCustomer.TabIndex = 168;
            this.cbCustomer.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // gridView3
            // 
            this.gridView3.ColumnPanelRowHeight = 30;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn33,
            this.gridColumn11,
            this.gridColumn35});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "ID";
            this.gridColumn33.FieldName = "ID";
            this.gridColumn33.Name = "gridColumn33";
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn11.AppearanceHeader.Options.UseFont = true;
            this.gridColumn11.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn11.Caption = "Mã khách hàng";
            this.gridColumn11.FieldName = "CustomerCode";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 206;
            // 
            // gridColumn35
            // 
            this.gridColumn35.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn35.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn35.AppearanceHeader.Options.UseFont = true;
            this.gridColumn35.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn35.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn35.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn35.Caption = "Tên khách hàng";
            this.gridColumn35.FieldName = "CustomerName";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 1;
            this.gridColumn35.Width = 497;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(304, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 170;
            this.label2.Text = "Kỹ thuật";
            // 
            // cboLeader
            // 
            this.cboLeader.EditValue = "";
            this.cboLeader.Location = new System.Drawing.Point(356, 29);
            this.cboLeader.Name = "cboLeader";
            this.cboLeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLeader.Properties.NullText = "";
            this.cboLeader.Properties.PopupView = this.gridView6;
            this.cboLeader.Size = new System.Drawing.Size(138, 20);
            this.cboLeader.TabIndex = 166;
            this.cboLeader.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // gridView6
            // 
            this.gridView6.ColumnPanelRowHeight = 30;
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "ID";
            this.gridColumn16.FieldName = "ID";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn17.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn17.AppearanceHeader.Options.UseFont = true;
            this.gridColumn17.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn17.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn17.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn17.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn17.Caption = "Tên nhân viên";
            this.gridColumn17.FieldName = "FullName";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            this.gridColumn17.Width = 296;
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn18.AppearanceHeader.Options.UseFont = true;
            this.gridColumn18.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn18.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn18.Caption = "Mã nhân viên";
            this.gridColumn18.FieldName = "Code";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 0;
            this.gridColumn18.Width = 177;
            // 
            // gridColumn19
            // 
            this.gridColumn19.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn19.AppearanceHeader.Options.UseFont = true;
            this.gridColumn19.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn19.Caption = "Phòng ban";
            this.gridColumn19.FieldName = "DepartmentName";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 2;
            this.gridColumn19.Width = 230;
            // 
            // cboPM
            // 
            this.cboPM.EditValue = "";
            this.cboPM.Location = new System.Drawing.Point(751, 2);
            this.cboPM.Name = "cboPM";
            this.cboPM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPM.Properties.NullText = "";
            this.cboPM.Properties.PopupView = this.gridView1;
            this.cboPM.Size = new System.Drawing.Size(138, 20);
            this.cboPM.TabIndex = 166;
            this.cboPM.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 30;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn12,
            this.gridColumn13});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "EmployeeID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn8.Caption = "Tên nhân viên";
            this.gridColumn8.FieldName = "FullName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 296;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn12.AppearanceHeader.Options.UseFont = true;
            this.gridColumn12.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn12.Caption = "Mã nhân viên";
            this.gridColumn12.FieldName = "Code";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            this.gridColumn12.Width = 177;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn13.AppearanceHeader.Options.UseFont = true;
            this.gridColumn13.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn13.Caption = "Phòng ban";
            this.gridColumn13.FieldName = "DepartmentName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 230;
            // 
            // cbUser
            // 
            this.cbUser.EditValue = "";
            this.cbUser.Location = new System.Drawing.Point(356, 3);
            this.cbUser.Name = "cbUser";
            this.cbUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbUser.Properties.NullText = "";
            this.cbUser.Properties.PopupView = this.searchLookUpEdit1View;
            this.cbUser.Size = new System.Drawing.Size(138, 20);
            this.cbUser.TabIndex = 166;
            this.cbUser.EditValueChanged += new System.EventHandler(this.btnFind_Click);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 30;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn20,
            this.gridColumn14,
            this.gridColumn15});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ID";
            this.gridColumn2.FieldName = "ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn20.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn20.AppearanceHeader.Options.UseFont = true;
            this.gridColumn20.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn20.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn20.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn20.Caption = "Tên nhân viên";
            this.gridColumn20.FieldName = "FullName";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 1;
            this.gridColumn20.Width = 296;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn14.AppearanceHeader.Options.UseFont = true;
            this.gridColumn14.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "Mã nhân viên";
            this.gridColumn14.FieldName = "Code";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            this.gridColumn14.Width = 177;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "Phòng ban";
            this.gridColumn15.FieldName = "DepartmentName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            this.gridColumn15.Width = 230;
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(576, 29);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(442, 20);
            this.txtFilterText.TabIndex = 71;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.Location = new System.Drawing.Point(304, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 167;
            this.label11.Text = "Sale";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(1028, 28);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(69, 23);
            this.btnFind.TabIndex = 73;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(214, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(80, 20);
            this.dtpEndDate.TabIndex = 162;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 164;
            this.label3.Text = "Từ ngày";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(69, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(80, 20);
            this.dtpFromDate.TabIndex = 165;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(155, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 163;
            this.label4.Text = "Đến ngày";
            // 
            // stackPanel2
            // 
            this.stackPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stackPanel2.AutoSize = true;
            this.stackPanel2.Controls.Add(this.btnFirst);
            this.stackPanel2.Controls.Add(this.btnPrev);
            this.stackPanel2.Controls.Add(this.txtPageNumber);
            this.stackPanel2.Controls.Add(this.label9);
            this.stackPanel2.Controls.Add(this.txtTotalPage);
            this.stackPanel2.Controls.Add(this.btnNext);
            this.stackPanel2.Controls.Add(this.btnLast);
            this.stackPanel2.Controls.Add(this.txtPageSize);
            this.stackPanel2.Controls.Add(this.txtShowCount);
            this.stackPanel2.Location = new System.Drawing.Point(1197, 3);
            this.stackPanel2.Name = "stackPanel2";
            this.stackPanel2.Size = new System.Drawing.Size(395, 30);
            this.stackPanel2.TabIndex = 31;
            // 
            // btnFirst
            // 
            this.btnFirst.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnFirst.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnFirst.Appearance.Options.UseBackColor = true;
            this.btnFirst.Appearance.Options.UseForeColor = true;
            this.btnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.ImageOptions.Image")));
            this.btnFirst.Location = new System.Drawing.Point(3, 3);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnFirst.Size = new System.Drawing.Size(23, 23);
            this.btnFirst.TabIndex = 143;
            this.btnFirst.Text = "Trang trước";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.Location = new System.Drawing.Point(32, 3);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPrev.Size = new System.Drawing.Size(23, 23);
            this.btnPrev.TabIndex = 141;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Location = new System.Drawing.Point(61, 4);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 21);
            this.txtPageNumber.TabIndex = 13;
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(92, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Location = new System.Drawing.Point(114, 4);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(25, 21);
            this.txtTotalPage.TabIndex = 12;
            this.txtTotalPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Appearance.Options.UseBackColor = true;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.Location = new System.Drawing.Point(145, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 142;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnLast.Appearance.Options.UseBackColor = true;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.Location = new System.Drawing.Point(174, 3);
            this.btnLast.Name = "btnLast";
            this.btnLast.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnLast.Size = new System.Drawing.Size(23, 23);
            this.btnLast.TabIndex = 144;
            this.btnLast.Text = "`";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(203, 4);
            this.txtPageSize.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtPageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(83, 21);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPageSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtPageSize.ValueChanged += new System.EventHandler(this.txtPageSize_TextChanged);
            // 
            // txtShowCount
            // 
            this.txtShowCount.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtShowCount.Enabled = false;
            this.txtShowCount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtShowCount.ForeColor = System.Drawing.Color.Black;
            this.txtShowCount.Location = new System.Drawing.Point(292, 4);
            this.txtShowCount.Name = "txtShowCount";
            this.txtShowCount.Size = new System.Drawing.Size(100, 21);
            this.txtShowCount.TabIndex = 152;
            this.txtShowCount.Text = "Entries";
            this.txtShowCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grdMaster
            // 
            this.grdMaster.AccessibleDescription = "";
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Location = new System.Drawing.Point(2, 2);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboProjectStatus,
            this.repositoryItemMemoEdit2});
            this.grdMaster.Size = new System.Drawing.Size(1590, 435);
            this.grdMaster.TabIndex = 34;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            this.grdMaster.DoubleClick += new System.EventHandler(this.grdMaster_DoubleClick);
            // 
            // grvMaster
            // 
            this.grvMaster.AutoFillColumn = this.colCustomerID;
            this.grvMaster.ColumnPanelRowHeight = 50;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDMaster,
            this.colCustomerID,
            this.colPriotityText,
            this.colProjectCode,
            this.colProjectName,
            this.colProjectShortName,
            this.colProjectStatus,
            this.colUserID,
            this.colFullNameTech,
            this.colNote,
            this.colContactName,
            this.colContactPhone,
            this.colContactEmail,
            this.colIsApproved,
            this.colPO,
            this.colCreatedDate,
            this.colCreatedBy,
            this.colUpdateBy,
            this.colPlanDS,
            this.colPlanDE,
            this.colActualDS,
            this.colActualDE,
            this.colEU,
            this.colFullNamePM,
            this.colCurrentState,
            this.colPersonalPriotity,
            this.colStatusOld,
            this.colPODate,
            this.colPMID,
            this.colUserIDSale,
            this.colBussinessField,
            this.colProjectProcessType,
            this.colUserMission});
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsCustomization.AllowRowSizing = true;
            this.grvMaster.OptionsSelection.MultiSelect = true;
            this.grvMaster.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvMaster.OptionsView.ColumnAutoWidth = false;
            this.grvMaster.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsView.RowAutoHeight = true;
            this.grvMaster.OptionsView.ShowFooter = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMaster_FocusedRowChanged);
            // 
            // colCustomerID
            // 
            this.colCustomerID.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colCustomerID.AppearanceCell.Options.UseFont = true;
            this.colCustomerID.AppearanceCell.Options.UseTextOptions = true;
            this.colCustomerID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomerID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCustomerID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCustomerID.AppearanceHeader.Options.UseFont = true;
            this.colCustomerID.AppearanceHeader.Options.UseForeColor = true;
            this.colCustomerID.AppearanceHeader.Options.UseTextOptions = true;
            this.colCustomerID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCustomerID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCustomerID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCustomerID.Caption = "Khách hàng";
            this.colCustomerID.FieldName = "CustomerName";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.OptionsColumn.AllowEdit = false;
            this.colCustomerID.OptionsColumn.ReadOnly = true;
            this.colCustomerID.Visible = true;
            this.colCustomerID.VisibleIndex = 16;
            this.colCustomerID.Width = 235;
            // 
            // colIDMaster
            // 
            this.colIDMaster.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colIDMaster.AppearanceCell.Options.UseFont = true;
            this.colIDMaster.AppearanceCell.Options.UseTextOptions = true;
            this.colIDMaster.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDMaster.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDMaster.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIDMaster.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIDMaster.AppearanceHeader.Options.UseFont = true;
            this.colIDMaster.AppearanceHeader.Options.UseForeColor = true;
            this.colIDMaster.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDMaster.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDMaster.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIDMaster.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDMaster.Caption = "ID";
            this.colIDMaster.FieldName = "ID";
            this.colIDMaster.Name = "colIDMaster";
            this.colIDMaster.OptionsColumn.AllowEdit = false;
            this.colIDMaster.OptionsColumn.ReadOnly = true;
            // 
            // colPriotityText
            // 
            this.colPriotityText.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPriotityText.AppearanceCell.Options.UseFont = true;
            this.colPriotityText.AppearanceCell.Options.UseTextOptions = true;
            this.colPriotityText.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriotityText.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPriotityText.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colPriotityText.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPriotityText.AppearanceHeader.Options.UseFont = true;
            this.colPriotityText.AppearanceHeader.Options.UseForeColor = true;
            this.colPriotityText.AppearanceHeader.Options.UseTextOptions = true;
            this.colPriotityText.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPriotityText.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPriotityText.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPriotityText.Caption = "Mức ưu tiên";
            this.colPriotityText.FieldName = "PriotityText";
            this.colPriotityText.Name = "colPriotityText";
            this.colPriotityText.OptionsColumn.AllowEdit = false;
            this.colPriotityText.OptionsColumn.ReadOnly = true;
            this.colPriotityText.Visible = true;
            this.colPriotityText.VisibleIndex = 2;
            this.colPriotityText.Width = 60;
            // 
            // colProjectCode
            // 
            this.colProjectCode.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colProjectCode.AppearanceCell.Options.UseFont = true;
            this.colProjectCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProjectCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProjectCode.AppearanceHeader.Options.UseFont = true;
            this.colProjectCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectCode.Caption = "Mã dự án";
            this.colProjectCode.FieldName = "ProjectCode";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.OptionsColumn.AllowEdit = false;
            this.colProjectCode.OptionsColumn.ReadOnly = true;
            this.colProjectCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ProjectCode", "{0}")});
            this.colProjectCode.Visible = true;
            this.colProjectCode.VisibleIndex = 4;
            this.colProjectCode.Width = 136;
            // 
            // colProjectName
            // 
            this.colProjectName.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colProjectName.AppearanceCell.Options.UseFont = true;
            this.colProjectName.AppearanceCell.Options.UseTextOptions = true;
            this.colProjectName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProjectName.AppearanceHeader.Options.UseFont = true;
            this.colProjectName.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectName.Caption = "Tên dự án";
            this.colProjectName.FieldName = "ProjectName";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.OptionsColumn.ReadOnly = true;
            this.colProjectName.Width = 303;
            // 
            // colProjectShortName
            // 
            this.colProjectShortName.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colProjectShortName.AppearanceCell.Options.UseFont = true;
            this.colProjectShortName.AppearanceCell.Options.UseTextOptions = true;
            this.colProjectShortName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectShortName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectShortName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectShortName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProjectShortName.AppearanceHeader.Options.UseFont = true;
            this.colProjectShortName.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectShortName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectShortName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectShortName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectShortName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectShortName.Caption = "Tên rút gọn";
            this.colProjectShortName.FieldName = "ProjectShortName";
            this.colProjectShortName.Name = "colProjectShortName";
            this.colProjectShortName.OptionsColumn.AllowEdit = false;
            this.colProjectShortName.OptionsColumn.ReadOnly = true;
            this.colProjectShortName.Width = 94;
            // 
            // colProjectStatus
            // 
            this.colProjectStatus.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colProjectStatus.AppearanceCell.Options.UseFont = true;
            this.colProjectStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colProjectStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProjectStatus.AppearanceHeader.Options.UseFont = true;
            this.colProjectStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProjectStatus.Caption = "Trạng thái";
            this.colProjectStatus.FieldName = "ProjectStatusName";
            this.colProjectStatus.Name = "colProjectStatus";
            this.colProjectStatus.Visible = true;
            this.colProjectStatus.VisibleIndex = 0;
            this.colProjectStatus.Width = 97;
            // 
            // colUserID
            // 
            this.colUserID.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colUserID.AppearanceCell.Options.UseFont = true;
            this.colUserID.AppearanceCell.Options.UseTextOptions = true;
            this.colUserID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUserID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUserID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUserID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUserID.AppearanceHeader.Options.UseFont = true;
            this.colUserID.AppearanceHeader.Options.UseForeColor = true;
            this.colUserID.AppearanceHeader.Options.UseTextOptions = true;
            this.colUserID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUserID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUserID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUserID.Caption = "Người phụ trách(sale)";
            this.colUserID.FieldName = "FullNameSale";
            this.colUserID.Name = "colUserID";
            this.colUserID.OptionsColumn.AllowEdit = false;
            this.colUserID.OptionsColumn.ReadOnly = true;
            this.colUserID.Visible = true;
            this.colUserID.VisibleIndex = 11;
            this.colUserID.Width = 140;
            // 
            // colFullNameTech
            // 
            this.colFullNameTech.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colFullNameTech.AppearanceCell.Options.UseFont = true;
            this.colFullNameTech.AppearanceCell.Options.UseTextOptions = true;
            this.colFullNameTech.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullNameTech.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullNameTech.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFullNameTech.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colFullNameTech.AppearanceHeader.Options.UseFont = true;
            this.colFullNameTech.AppearanceHeader.Options.UseForeColor = true;
            this.colFullNameTech.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullNameTech.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullNameTech.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullNameTech.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullNameTech.Caption = "Người phụ trách(kỹ thuật)";
            this.colFullNameTech.FieldName = "FullNameTech";
            this.colFullNameTech.Name = "colFullNameTech";
            this.colFullNameTech.OptionsColumn.AllowEdit = false;
            this.colFullNameTech.OptionsColumn.ReadOnly = true;
            this.colFullNameTech.Visible = true;
            this.colFullNameTech.VisibleIndex = 12;
            this.colFullNameTech.Width = 140;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNote.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.FieldName = "Mô tả dự án";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.OptionsColumn.ReadOnly = true;
            this.colNote.Width = 163;
            // 
            // colContactName
            // 
            this.colContactName.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colContactName.AppearanceCell.Options.UseFont = true;
            this.colContactName.AppearanceCell.Options.UseTextOptions = true;
            this.colContactName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colContactName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colContactName.AppearanceHeader.Options.UseFont = true;
            this.colContactName.AppearanceHeader.Options.UseForeColor = true;
            this.colContactName.AppearanceHeader.Options.UseTextOptions = true;
            this.colContactName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colContactName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactName.Caption = "Người liên hệ";
            this.colContactName.FieldName = "ContactName";
            this.colContactName.Name = "colContactName";
            this.colContactName.OptionsColumn.AllowEdit = false;
            this.colContactName.OptionsColumn.ReadOnly = true;
            this.colContactName.Width = 103;
            // 
            // colContactPhone
            // 
            this.colContactPhone.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colContactPhone.AppearanceCell.Options.UseFont = true;
            this.colContactPhone.AppearanceCell.Options.UseTextOptions = true;
            this.colContactPhone.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactPhone.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactPhone.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colContactPhone.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colContactPhone.AppearanceHeader.Options.UseFont = true;
            this.colContactPhone.AppearanceHeader.Options.UseForeColor = true;
            this.colContactPhone.AppearanceHeader.Options.UseTextOptions = true;
            this.colContactPhone.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colContactPhone.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactPhone.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactPhone.Caption = "Số điện thoại";
            this.colContactPhone.FieldName = "ContactPhone";
            this.colContactPhone.Name = "colContactPhone";
            this.colContactPhone.OptionsColumn.AllowEdit = false;
            this.colContactPhone.OptionsColumn.ReadOnly = true;
            this.colContactPhone.Width = 90;
            // 
            // colContactEmail
            // 
            this.colContactEmail.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colContactEmail.AppearanceCell.Options.UseFont = true;
            this.colContactEmail.AppearanceCell.Options.UseTextOptions = true;
            this.colContactEmail.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactEmail.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactEmail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colContactEmail.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colContactEmail.AppearanceHeader.Options.UseFont = true;
            this.colContactEmail.AppearanceHeader.Options.UseForeColor = true;
            this.colContactEmail.AppearanceHeader.Options.UseTextOptions = true;
            this.colContactEmail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colContactEmail.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContactEmail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContactEmail.Caption = "Email";
            this.colContactEmail.FieldName = "ContactEmail";
            this.colContactEmail.Name = "colContactEmail";
            this.colContactEmail.OptionsColumn.AllowEdit = false;
            this.colContactEmail.OptionsColumn.ReadOnly = true;
            this.colContactEmail.Width = 104;
            // 
            // colIsApproved
            // 
            this.colIsApproved.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colIsApproved.AppearanceCell.Options.UseFont = true;
            this.colIsApproved.AppearanceCell.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsApproved.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIsApproved.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIsApproved.AppearanceHeader.Options.UseFont = true;
            this.colIsApproved.AppearanceHeader.Options.UseForeColor = true;
            this.colIsApproved.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsApproved.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsApproved.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsApproved.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApproved.Caption = "Duyệt";
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.OptionsColumn.AllowEdit = false;
            this.colIsApproved.OptionsColumn.ReadOnly = true;
            this.colIsApproved.Width = 55;
            // 
            // colPO
            // 
            this.colPO.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPO.AppearanceCell.Options.UseFont = true;
            this.colPO.AppearanceCell.Options.UseTextOptions = true;
            this.colPO.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPO.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPO.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPO.AppearanceHeader.Options.UseFont = true;
            this.colPO.AppearanceHeader.Options.UseForeColor = true;
            this.colPO.AppearanceHeader.Options.UseTextOptions = true;
            this.colPO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPO.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPO.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPO.Caption = "PO";
            this.colPO.FieldName = "PO";
            this.colPO.Name = "colPO";
            this.colPO.OptionsColumn.AllowEdit = false;
            this.colPO.OptionsColumn.ReadOnly = true;
            this.colPO.Visible = true;
            this.colPO.VisibleIndex = 9;
            this.colPO.Width = 144;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colCreatedDate.AppearanceCell.Options.UseFont = true;
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCreatedDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreatedDate.AppearanceHeader.Options.UseFont = true;
            this.colCreatedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.OptionsColumn.AllowEdit = false;
            this.colCreatedDate.OptionsColumn.ReadOnly = true;
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 1;
            this.colCreatedDate.Width = 99;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colCreatedBy.AppearanceCell.Options.UseFont = true;
            this.colCreatedBy.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedBy.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedBy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedBy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCreatedBy.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCreatedBy.AppearanceHeader.Options.UseFont = true;
            this.colCreatedBy.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedBy.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedBy.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedBy.Caption = "Người tạo";
            this.colCreatedBy.FieldName = "CreatedBy";
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.OptionsColumn.AllowEdit = false;
            this.colCreatedBy.OptionsColumn.ReadOnly = true;
            this.colCreatedBy.Visible = true;
            this.colCreatedBy.VisibleIndex = 20;
            this.colCreatedBy.Width = 112;
            // 
            // colUpdateBy
            // 
            this.colUpdateBy.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colUpdateBy.AppearanceCell.Options.UseFont = true;
            this.colUpdateBy.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdateBy.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdateBy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdateBy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUpdateBy.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUpdateBy.AppearanceHeader.Options.UseFont = true;
            this.colUpdateBy.AppearanceHeader.Options.UseForeColor = true;
            this.colUpdateBy.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdateBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdateBy.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdateBy.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdateBy.Caption = "Người sửa";
            this.colUpdateBy.FieldName = "UpdatedBy";
            this.colUpdateBy.Name = "colUpdateBy";
            this.colUpdateBy.OptionsColumn.AllowEdit = false;
            this.colUpdateBy.OptionsColumn.ReadOnly = true;
            this.colUpdateBy.Visible = true;
            this.colUpdateBy.VisibleIndex = 21;
            this.colUpdateBy.Width = 112;
            // 
            // colPlanDS
            // 
            this.colPlanDS.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPlanDS.AppearanceCell.Options.UseFont = true;
            this.colPlanDS.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPlanDS.AppearanceHeader.Options.UseFont = true;
            this.colPlanDS.AppearanceHeader.Options.UseForeColor = true;
            this.colPlanDS.AppearanceHeader.Options.UseTextOptions = true;
            this.colPlanDS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPlanDS.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPlanDS.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPlanDS.Caption = "Ngày bắt đầu dự kiến";
            this.colPlanDS.FieldName = "PlanDateStart";
            this.colPlanDS.Name = "colPlanDS";
            this.colPlanDS.OptionsColumn.AllowEdit = false;
            this.colPlanDS.OptionsColumn.ReadOnly = true;
            this.colPlanDS.Visible = true;
            this.colPlanDS.VisibleIndex = 17;
            this.colPlanDS.Width = 95;
            // 
            // colPlanDE
            // 
            this.colPlanDE.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPlanDE.AppearanceCell.Options.UseFont = true;
            this.colPlanDE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPlanDE.AppearanceHeader.Options.UseFont = true;
            this.colPlanDE.AppearanceHeader.Options.UseForeColor = true;
            this.colPlanDE.AppearanceHeader.Options.UseTextOptions = true;
            this.colPlanDE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPlanDE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPlanDE.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPlanDE.Caption = "Ngày kết thúc dự kiến";
            this.colPlanDE.FieldName = "PlanDateEndSummary";
            this.colPlanDE.Name = "colPlanDE";
            this.colPlanDE.OptionsColumn.AllowEdit = false;
            this.colPlanDE.OptionsColumn.ReadOnly = true;
            this.colPlanDE.Visible = true;
            this.colPlanDE.VisibleIndex = 7;
            this.colPlanDE.Width = 109;
            // 
            // colActualDS
            // 
            this.colActualDS.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colActualDS.AppearanceCell.Options.UseFont = true;
            this.colActualDS.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colActualDS.AppearanceHeader.Options.UseFont = true;
            this.colActualDS.AppearanceHeader.Options.UseForeColor = true;
            this.colActualDS.AppearanceHeader.Options.UseTextOptions = true;
            this.colActualDS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActualDS.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colActualDS.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colActualDS.Caption = "Ngày bắt đầu thực tế";
            this.colActualDS.FieldName = "ActualDateStart";
            this.colActualDS.Name = "colActualDS";
            this.colActualDS.OptionsColumn.AllowEdit = false;
            this.colActualDS.OptionsColumn.ReadOnly = true;
            this.colActualDS.Visible = true;
            this.colActualDS.VisibleIndex = 18;
            this.colActualDS.Width = 95;
            // 
            // colActualDE
            // 
            this.colActualDE.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colActualDE.AppearanceCell.Options.UseFont = true;
            this.colActualDE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colActualDE.AppearanceHeader.Options.UseFont = true;
            this.colActualDE.AppearanceHeader.Options.UseForeColor = true;
            this.colActualDE.AppearanceHeader.Options.UseTextOptions = true;
            this.colActualDE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActualDE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colActualDE.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colActualDE.Caption = "Ngày kết thúc thực tế";
            this.colActualDE.FieldName = "ActualDateEnd";
            this.colActualDE.Name = "colActualDE";
            this.colActualDE.OptionsColumn.AllowEdit = false;
            this.colActualDE.OptionsColumn.ReadOnly = true;
            this.colActualDE.Visible = true;
            this.colActualDE.VisibleIndex = 19;
            this.colActualDE.Width = 95;
            // 
            // colEU
            // 
            this.colEU.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colEU.AppearanceCell.Options.UseFont = true;
            this.colEU.AppearanceCell.Options.UseTextOptions = true;
            this.colEU.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEU.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEU.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEU.AppearanceHeader.Options.UseFont = true;
            this.colEU.AppearanceHeader.Options.UseForeColor = true;
            this.colEU.AppearanceHeader.Options.UseTextOptions = true;
            this.colEU.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEU.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEU.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEU.Caption = "End User";
            this.colEU.FieldName = "EndUserName";
            this.colEU.Name = "colEU";
            this.colEU.OptionsColumn.AllowEdit = false;
            this.colEU.OptionsColumn.ReadOnly = true;
            this.colEU.Visible = true;
            this.colEU.VisibleIndex = 8;
            this.colEU.Width = 243;
            // 
            // colFullNamePM
            // 
            this.colFullNamePM.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colFullNamePM.AppearanceCell.Options.UseFont = true;
            this.colFullNamePM.AppearanceCell.Options.UseTextOptions = true;
            this.colFullNamePM.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullNamePM.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullNamePM.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFullNamePM.AppearanceHeader.Options.UseFont = true;
            this.colFullNamePM.AppearanceHeader.Options.UseForeColor = true;
            this.colFullNamePM.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullNamePM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullNamePM.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullNamePM.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullNamePM.Caption = "PM";
            this.colFullNamePM.FieldName = "FullNamePM";
            this.colFullNamePM.Name = "colFullNamePM";
            this.colFullNamePM.OptionsColumn.AllowEdit = false;
            this.colFullNamePM.OptionsColumn.ReadOnly = true;
            this.colFullNamePM.Visible = true;
            this.colFullNamePM.VisibleIndex = 13;
            this.colFullNamePM.Width = 140;
            // 
            // colCurrentState
            // 
            this.colCurrentState.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colCurrentState.AppearanceCell.Options.UseFont = true;
            this.colCurrentState.AppearanceCell.Options.UseForeColor = true;
            this.colCurrentState.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrentState.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrentState.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrentState.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCurrentState.AppearanceHeader.Options.UseFont = true;
            this.colCurrentState.AppearanceHeader.Options.UseForeColor = true;
            this.colCurrentState.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrentState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrentState.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCurrentState.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCurrentState.Caption = "Hiện trạng";
            this.colCurrentState.FieldName = "CurrentState";
            this.colCurrentState.Name = "colCurrentState";
            this.colCurrentState.OptionsColumn.AllowEdit = false;
            this.colCurrentState.OptionsColumn.ReadOnly = true;
            this.colCurrentState.Visible = true;
            this.colCurrentState.VisibleIndex = 15;
            this.colCurrentState.Width = 402;
            // 
            // colPersonalPriotity
            // 
            this.colPersonalPriotity.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPersonalPriotity.AppearanceCell.Options.UseFont = true;
            this.colPersonalPriotity.AppearanceCell.Options.UseTextOptions = true;
            this.colPersonalPriotity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPersonalPriotity.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPersonalPriotity.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPersonalPriotity.AppearanceHeader.Options.UseFont = true;
            this.colPersonalPriotity.AppearanceHeader.Options.UseForeColor = true;
            this.colPersonalPriotity.AppearanceHeader.Options.UseTextOptions = true;
            this.colPersonalPriotity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPersonalPriotity.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPersonalPriotity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPersonalPriotity.Caption = "Mức độ ưu tiên cá nhân";
            this.colPersonalPriotity.FieldName = "PersonalPriotity";
            this.colPersonalPriotity.Name = "colPersonalPriotity";
            this.colPersonalPriotity.OptionsColumn.AllowEdit = false;
            this.colPersonalPriotity.OptionsColumn.ReadOnly = true;
            this.colPersonalPriotity.Visible = true;
            this.colPersonalPriotity.VisibleIndex = 3;
            this.colPersonalPriotity.Width = 88;
            // 
            // colStatusOld
            // 
            this.colStatusOld.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colStatusOld.AppearanceCell.Options.UseFont = true;
            this.colStatusOld.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusOld.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusOld.Caption = "Trạng thái cũ";
            this.colStatusOld.FieldName = "StatusOld";
            this.colStatusOld.Name = "colStatusOld";
            this.colStatusOld.OptionsColumn.AllowEdit = false;
            this.colStatusOld.OptionsColumn.ReadOnly = true;
            // 
            // colPODate
            // 
            this.colPODate.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPODate.AppearanceCell.Options.UseFont = true;
            this.colPODate.AppearanceCell.Options.UseTextOptions = true;
            this.colPODate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPODate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPODate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPODate.AppearanceHeader.Options.UseFont = true;
            this.colPODate.AppearanceHeader.Options.UseForeColor = true;
            this.colPODate.AppearanceHeader.Options.UseTextOptions = true;
            this.colPODate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPODate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPODate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPODate.Caption = "Ngày PO";
            this.colPODate.FieldName = "PODate";
            this.colPODate.Name = "colPODate";
            this.colPODate.OptionsColumn.AllowEdit = false;
            this.colPODate.OptionsColumn.ReadOnly = true;
            this.colPODate.Visible = true;
            this.colPODate.VisibleIndex = 10;
            this.colPODate.Width = 137;
            // 
            // colPMID
            // 
            this.colPMID.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colPMID.AppearanceCell.Options.UseFont = true;
            this.colPMID.AppearanceHeader.Options.UseTextOptions = true;
            this.colPMID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPMID.Caption = "PM ID";
            this.colPMID.FieldName = "PMID";
            this.colPMID.Name = "colPMID";
            // 
            // colUserIDSale
            // 
            this.colUserIDSale.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.colUserIDSale.AppearanceCell.Options.UseFont = true;
            this.colUserIDSale.AppearanceHeader.Options.UseTextOptions = true;
            this.colUserIDSale.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUserIDSale.Caption = "gridColumn21";
            this.colUserIDSale.FieldName = "UserID";
            this.colUserIDSale.Name = "colUserIDSale";
            // 
            // colBussinessField
            // 
            this.colBussinessField.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBussinessField.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBussinessField.AppearanceHeader.Options.UseFont = true;
            this.colBussinessField.AppearanceHeader.Options.UseForeColor = true;
            this.colBussinessField.AppearanceHeader.Options.UseTextOptions = true;
            this.colBussinessField.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBussinessField.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBussinessField.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBussinessField.Caption = "Lĩnh vực dự án";
            this.colBussinessField.FieldName = "BussinessField";
            this.colBussinessField.Name = "colBussinessField";
            this.colBussinessField.Visible = true;
            this.colBussinessField.VisibleIndex = 14;
            this.colBussinessField.Width = 130;
            // 
            // colProjectProcessType
            // 
            this.colProjectProcessType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectProcessType.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProjectProcessType.AppearanceHeader.Options.UseFont = true;
            this.colProjectProcessType.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectProcessType.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectProcessType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectProcessType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProjectProcessType.Caption = "Trạng thái dự án";
            this.colProjectProcessType.FieldName = "ProjectProcessType";
            this.colProjectProcessType.Name = "colProjectProcessType";
            this.colProjectProcessType.Visible = true;
            this.colProjectProcessType.VisibleIndex = 5;
            this.colProjectProcessType.Width = 125;
            // 
            // colUserMission
            // 
            this.colUserMission.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colUserMission.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUserMission.AppearanceHeader.Options.UseFont = true;
            this.colUserMission.AppearanceHeader.Options.UseForeColor = true;
            this.colUserMission.AppearanceHeader.Options.UseTextOptions = true;
            this.colUserMission.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUserMission.Caption = "Nội dung công việc";
            this.colUserMission.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colUserMission.FieldName = "UserMission";
            this.colUserMission.Name = "colUserMission";
            this.colUserMission.Visible = true;
            this.colUserMission.VisibleIndex = 6;
            this.colUserMission.Width = 443;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // cboProjectStatus
            // 
            this.cboProjectStatus.AutoHeight = false;
            this.cboProjectStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectStatus.Name = "cboProjectStatus";
            this.cboProjectStatus.NullText = "";
            this.cboProjectStatus.PopupView = this.repositoryItemSearchLookUpEdit2View;
            // 
            // repositoryItemSearchLookUpEdit2View
            // 
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = true;
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemSearchLookUpEdit2View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemSearchLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatusID,
            this.colStatusName});
            this.repositoryItemSearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit2View.Name = "repositoryItemSearchLookUpEdit2View";
            this.repositoryItemSearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // colStatusID
            // 
            this.colStatusID.Caption = "STT";
            this.colStatusID.FieldName = "ProjectStatusID";
            this.colStatusID.Name = "colStatusID";
            this.colStatusID.Visible = true;
            this.colStatusID.VisibleIndex = 0;
            this.colStatusID.Width = 124;
            // 
            // colStatusName
            // 
            this.colStatusName.Caption = "Trạng thái";
            this.colStatusName.FieldName = "StatusName";
            this.colStatusName.Name = "colStatusName";
            this.colStatusName.Visible = true;
            this.colStatusName.VisibleIndex = 1;
            this.colStatusName.Width = 1456;
            // 
            // cboUserTech
            // 
            this.cboUserTech.EditValue = "";
            this.cboUserTech.Enabled = false;
            this.cboUserTech.Location = new System.Drawing.Point(675, 345);
            this.cboUserTech.Name = "cboUserTech";
            this.cboUserTech.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserTech.Properties.NullText = "";
            this.cboUserTech.Properties.PopupView = this.gridView4;
            this.cboUserTech.Size = new System.Drawing.Size(187, 20);
            this.cboUserTech.TabIndex = 167;
            this.cboUserTech.Visible = false;
            // 
            // gridView4
            // 
            this.gridView4.ColumnPanelRowHeight = 30;
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "ID";
            this.gridColumn9.FieldName = "ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn10.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn10.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn10.Caption = "Tên người phụ trách";
            this.gridColumn10.FieldName = "FullName";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 175;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(47, 47);
            this.btnNew.Tag = "frmProject_Update";
            this.btnNew.Text = "Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.ToolTipText = "Thêm";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 47);
            this.btnEdit.Tag = "frmProject_Update";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 47);
            this.btnDelete.Tag = "frmProject_Update";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ExportXlsLarge;
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(79, 47);
            this.btnExportExcel.Tag = "";
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 50);
            // 
            // btnSettingTree
            // 
            this.btnSettingTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettingTree.Image = global::Forms.PrintRibbonControllerResources.folder;
            this.btnSettingTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettingTree.Name = "btnSettingTree";
            this.btnSettingTree.Size = new System.Drawing.Size(89, 47);
            this.btnSettingTree.Tag = "";
            this.btnSettingTree.Text = "Cây thư mục";
            this.btnSettingTree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 50);
            // 
            // btnPersonalPriotity
            // 
            this.btnPersonalPriotity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPersonalPriotity.Name = "btnPersonalPriotity";
            this.btnPersonalPriotity.Size = new System.Drawing.Size(142, 47);
            this.btnPersonalPriotity.Text = "Mức độ ưu tiên cá nhân";
            this.btnPersonalPriotity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 50);
            // 
            // btnProjectPartList
            // 
            this.btnProjectPartList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectPartList.Name = "btnProjectPartList";
            this.btnProjectPartList.Size = new System.Drawing.Size(105, 47);
            this.btnProjectPartList.Tag = "";
            this.btnProjectPartList.Text = "Danh mục vật tư";
            this.btnProjectPartList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 50);
            // 
            // btnProjectWorker
            // 
            this.btnProjectWorker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectWorker.Name = "btnProjectWorker";
            this.btnProjectWorker.Size = new System.Drawing.Size(104, 47);
            this.btnProjectWorker.Tag = "frmProject_ProjectWorker";
            this.btnProjectWorker.Text = "Nhân công dự án";
            this.btnProjectWorker.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 50);
            // 
            // btnSummaryWorker
            // 
            this.btnSummaryWorker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSummaryWorker.Name = "btnSummaryWorker";
            this.btnSummaryWorker.Size = new System.Drawing.Size(124, 47);
            this.btnSummaryWorker.Tag = "frmProject_SummaryWorker";
            this.btnSummaryWorker.Text = "Tổng hợp nhân công";
            this.btnSummaryWorker.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 50);
            // 
            // btnProjectAll
            // 
            this.btnProjectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectAll.Name = "btnProjectAll";
            this.btnProjectAll.Size = new System.Drawing.Size(132, 47);
            this.btnProjectAll.Text = "DS báo cáo công việc ";
            this.btnProjectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 50);
            // 
            // btnProjectStatus
            // 
            this.btnProjectStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectStatus.Name = "btnProjectStatus";
            this.btnProjectStatus.Size = new System.Drawing.Size(104, 47);
            this.btnProjectStatus.Tag = "frmProject_ProjectStatus";
            this.btnProjectStatus.Text = "Trạng thái dự án";
            this.btnProjectStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 50);
            // 
            // bntProjectItem
            // 
            this.bntProjectItem.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_DocumentMapLarge;
            this.bntProjectItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntProjectItem.Name = "bntProjectItem";
            this.bntProjectItem.Size = new System.Drawing.Size(123, 47);
            this.bntProjectItem.Text = "Hạng mục công việc";
            this.bntProjectItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 50);
            // 
            // btnGranttChartProject
            // 
            this.btnGranttChartProject.Image = global::Forms.PrintRibbonControllerResources.icons8_gantt_chart_32;
            this.btnGranttChartProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGranttChartProject.Name = "btnGranttChartProject";
            this.btnGranttChartProject.Size = new System.Drawing.Size(108, 47);
            this.btnGranttChartProject.Text = "Tiến độ công việc";
            this.btnGranttChartProject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 50);
            // 
            // btnProjectEmployees
            // 
            this.btnProjectEmployees.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectEmployees.Name = "btnProjectEmployees";
            this.btnProjectEmployees.Size = new System.Drawing.Size(96, 47);
            this.btnProjectEmployees.Text = "Người tham gia";
            this.btnProjectEmployees.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 50);
            // 
            // btnChangeProjectReport
            // 
            this.btnChangeProjectReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeProjectReport.Name = "btnChangeProjectReport";
            this.btnChangeProjectReport.Size = new System.Drawing.Size(156, 47);
            this.btnChangeProjectReport.Tag = "frmProject_UpdateProjectIDInDailyReportTechnical";
            this.btnChangeProjectReport.Text = "Chuyển báo cáo công việc";
            this.btnChangeProjectReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnIsApproved
            // 
            this.btnIsApproved.AutoSize = false;
            this.btnIsApproved.Enabled = false;
            this.btnIsApproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIsApproved.Image = ((System.Drawing.Image)(resources.GetObject("btnIsApproved.Image")));
            this.btnIsApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIsApproved.Name = "btnIsApproved";
            this.btnIsApproved.Size = new System.Drawing.Size(80, 41);
            this.btnIsApproved.Tag = "";
            this.btnIsApproved.Text = "Duyệt";
            this.btnIsApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIsApproved.Visible = false;
            // 
            // btnCancelApprove
            // 
            this.btnCancelApprove.AutoSize = false;
            this.btnCancelApprove.Enabled = false;
            this.btnCancelApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelApprove.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelApprove.Image")));
            this.btnCancelApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelApprove.Name = "btnCancelApprove";
            this.btnCancelApprove.Size = new System.Drawing.Size(80, 41);
            this.btnCancelApprove.Tag = "";
            this.btnCancelApprove.Text = "Hủy duyệt";
            this.btnCancelApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelApprove.Visible = false;
            // 
            // btnGroupFile
            // 
            this.btnGroupFile.AutoSize = false;
            this.btnGroupFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroupFile.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_OpenLarge;
            this.btnGroupFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGroupFile.Name = "btnGroupFile";
            this.btnGroupFile.Size = new System.Drawing.Size(80, 41);
            this.btnGroupFile.Tag = "frmProject_Update";
            this.btnGroupFile.Text = "Nhóm file";
            this.btnGroupFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGroupFile.Visible = false;
            // 
            // btnProjectCost
            // 
            this.btnProjectCost.AutoSize = false;
            this.btnProjectCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProjectCost.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_CustomizeLarge;
            this.btnProjectCost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectCost.Name = "btnProjectCost";
            this.btnProjectCost.Size = new System.Drawing.Size(120, 41);
            this.btnProjectCost.Tag = "frmProject_Update";
            this.btnProjectCost.Text = "Xem chi phí";
            this.btnProjectCost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnProjectCost.Visible = false;
            // 
            // btnProjectType
            // 
            this.btnProjectType.AutoSize = false;
            this.btnProjectType.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_PageOrientationLarge;
            this.btnProjectType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectType.Name = "btnProjectType";
            this.btnProjectType.Size = new System.Drawing.Size(120, 41);
            this.btnProjectType.Text = "Kiểu dự án";
            this.btnProjectType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnProjectType.Visible = false;
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator4,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnExportExcel,
            this.toolStripSeparator7,
            this.btnSettingTree,
            this.toolStripSeparator9,
            this.btnPersonalPriotity,
            this.toolStripSeparator11,
            this.btnProjectPartList,
            this.toolStripSeparator10,
            this.btnProjectWorker,
            this.toolStripSeparator12,
            this.btnSummaryWorker,
            this.toolStripSeparator14,
            this.btnProjectAll,
            this.toolStripSeparator15,
            this.btnProjectStatus,
            this.toolStripSeparator23,
            this.bntProjectItem,
            this.toolStripSeparator22,
            this.btnGranttChartProject,
            this.toolStripSeparator24,
            this.btnProjectEmployees,
            this.toolStripSeparator13,
            this.btnChangeProjectReport,
            this.btnIsApproved,
            this.btnCancelApprove,
            this.btnGroupFile,
            this.btnProjectCost,
            this.btnProjectType});
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1924, 50);
            this.mnuMenu.TabIndex = 26;
            this.mnuMenu.Text = "toolStrip2";
            this.mnuMenu.Visible = false;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 502);
            this.splitContainerControl2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.tlProjectTypeMaster);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.grdProjectItem);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1594, 239);
            this.splitContainerControl2.SplitterPosition = 292;
            this.splitContainerControl2.TabIndex = 169;
            // 
            // tlProjectTypeMaster
            // 
            this.tlProjectTypeMaster.ColumnPanelRowHeight = 50;
            this.tlProjectTypeMaster.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colSeleted,
            this.colProjectTypeMaster,
            this.colLeaderMater,
            this.colProjectTypeID,
            this.colLeaderID});
            this.tlProjectTypeMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlProjectTypeMaster.Location = new System.Drawing.Point(0, 0);
            this.tlProjectTypeMaster.Name = "tlProjectTypeMaster";
            this.tlProjectTypeMaster.OptionsBehavior.Editable = false;
            this.tlProjectTypeMaster.OptionsBehavior.ReadOnly = true;
            this.tlProjectTypeMaster.OptionsCustomization.AllowSort = false;
            this.tlProjectTypeMaster.OptionsFilter.AllowColumnMRUFilterList = false;
            this.tlProjectTypeMaster.OptionsFilter.AllowFilterEditor = false;
            this.tlProjectTypeMaster.OptionsFilter.AllowMRUFilterList = false;
            this.tlProjectTypeMaster.OptionsView.ShowIndicator = false;
            this.tlProjectTypeMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.tlProjectTypeMaster.RowHeight = 20;
            this.tlProjectTypeMaster.Size = new System.Drawing.Size(292, 239);
            this.tlProjectTypeMaster.TabIndex = 3;
            // 
            // colSeleted
            // 
            this.colSeleted.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSeleted.AppearanceHeader.Options.UseFont = true;
            this.colSeleted.AppearanceHeader.Options.UseForeColor = true;
            this.colSeleted.AppearanceHeader.Options.UseTextOptions = true;
            this.colSeleted.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSeleted.Caption = "Dự án";
            this.colSeleted.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colSeleted.FieldName = "Selected";
            this.colSeleted.MaxWidth = 50;
            this.colSeleted.MinWidth = 50;
            this.colSeleted.Name = "colSeleted";
            this.colSeleted.OptionsFilter.AllowAutoFilter = false;
            this.colSeleted.OptionsFilter.AllowFilter = false;
            this.colSeleted.Visible = true;
            this.colSeleted.VisibleIndex = 0;
            this.colSeleted.Width = 50;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colProjectTypeMaster
            // 
            this.colProjectTypeMaster.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProjectTypeMaster.AppearanceHeader.Options.UseBorderColor = true;
            this.colProjectTypeMaster.AppearanceHeader.Options.UseFont = true;
            this.colProjectTypeMaster.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectTypeMaster.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectTypeMaster.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectTypeMaster.Caption = "Kiểu dự án";
            this.colProjectTypeMaster.FieldName = "ProjectTypeName";
            this.colProjectTypeMaster.MaxWidth = 80;
            this.colProjectTypeMaster.MinWidth = 80;
            this.colProjectTypeMaster.Name = "colProjectTypeMaster";
            this.colProjectTypeMaster.OptionsFilter.AllowAutoFilter = false;
            this.colProjectTypeMaster.OptionsFilter.AllowFilter = false;
            this.colProjectTypeMaster.Visible = true;
            this.colProjectTypeMaster.VisibleIndex = 1;
            this.colProjectTypeMaster.Width = 80;
            // 
            // colLeaderMater
            // 
            this.colLeaderMater.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLeaderMater.AppearanceHeader.Options.UseBorderColor = true;
            this.colLeaderMater.AppearanceHeader.Options.UseFont = true;
            this.colLeaderMater.AppearanceHeader.Options.UseForeColor = true;
            this.colLeaderMater.AppearanceHeader.Options.UseTextOptions = true;
            this.colLeaderMater.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLeaderMater.Caption = "Leader";
            this.colLeaderMater.FieldName = "FullName";
            this.colLeaderMater.Name = "colLeaderMater";
            this.colLeaderMater.OptionsFilter.AllowAutoFilter = false;
            this.colLeaderMater.OptionsFilter.AllowFilter = false;
            this.colLeaderMater.Width = 164;
            // 
            // colProjectTypeID
            // 
            this.colProjectTypeID.Caption = "ID";
            this.colProjectTypeID.FieldName = "ID";
            this.colProjectTypeID.Name = "colProjectTypeID";
            // 
            // colLeaderID
            // 
            this.colLeaderID.Caption = "Leader ID";
            this.colLeaderID.FieldName = "LeaderID";
            this.colLeaderID.Name = "colLeaderID";
            // 
            // grdProjectItem
            // 
            this.grdProjectItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProjectItem.Location = new System.Drawing.Point(0, 0);
            this.grdProjectItem.MainView = this.grvProjectItem;
            this.grdProjectItem.Name = "grdProjectItem";
            this.grdProjectItem.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboUserID,
            this.repositoryItemMemoEdit3,
            this.repositoryItemDateEdit1,
            this.cboStatus,
            this.btnDeleteRepo,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit4});
            this.grdProjectItem.Size = new System.Drawing.Size(1292, 239);
            this.grdProjectItem.TabIndex = 34;
            this.grdProjectItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvProjectItem});
            // 
            // grvProjectItem
            // 
            this.grvProjectItem.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvProjectItem.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvProjectItem.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvProjectItem.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvProjectItem.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvProjectItem.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvProjectItem.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvProjectItem.Appearance.Row.Options.UseTextOptions = true;
            this.grvProjectItem.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvProjectItem.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvProjectItem.AutoFillColumn = this.colMission;
            this.grvProjectItem.ColumnPanelRowHeight = 50;
            this.grvProjectItem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdProjectItem,
            this.colStatus,
            this.colUserProjectItem,
            this.colMission,
            this.colPlanStartDate,
            this.colPlanEndDate,
            this.colActualStartDate,
            this.colActualEndDate,
            this.colSTT,
            this.colNoteProjectItem,
            this.colTotalDayPlan,
            this.colPercentItem,
            this.colChargerID,
            this.colPercentageActual,
            this.colProjectEmployeeName,
            this.colCode,
            this.colEmployeeRequest,
            this.colItemLateActual,
            this.colProjectTypeName});
            this.grvProjectItem.GridControl = this.grdProjectItem;
            this.grvProjectItem.Name = "grvProjectItem";
            this.grvProjectItem.OptionsBehavior.Editable = false;
            this.grvProjectItem.OptionsBehavior.ReadOnly = true;
            this.grvProjectItem.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvProjectItem.OptionsCustomization.AllowGroup = false;
            this.grvProjectItem.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.grvProjectItem.OptionsFind.FindNullPrompt = "Nhập cụm từ cần tìm kiếm vào đây...";
            this.grvProjectItem.OptionsSelection.MultiSelect = true;
            this.grvProjectItem.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvProjectItem.OptionsView.ColumnAutoWidth = false;
            this.grvProjectItem.OptionsView.RowAutoHeight = true;
            this.grvProjectItem.OptionsView.ShowFooter = true;
            this.grvProjectItem.OptionsView.ShowGroupPanel = false;
            this.grvProjectItem.OptionsView.ShowIndicator = false;
            this.grvProjectItem.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvProjectItem_RowStyle);
            // 
            // colMission
            // 
            this.colMission.Caption = "Công việc";
            this.colMission.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colMission.FieldName = "Mission";
            this.colMission.Name = "colMission";
            this.colMission.Visible = true;
            this.colMission.VisibleIndex = 6;
            this.colMission.Width = 284;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colIdProjectItem
            // 
            this.colIdProjectItem.Caption = "ID";
            this.colIdProjectItem.FieldName = "ID";
            this.colIdProjectItem.Name = "colIdProjectItem";
            this.colIdProjectItem.Width = 53;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "StatusText";
            this.colStatus.Name = "colStatus";
            this.colStatus.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 2;
            this.colStatus.Width = 79;
            // 
            // colUserProjectItem
            // 
            this.colUserProjectItem.Caption = "Người phụ trách";
            this.colUserProjectItem.FieldName = "FullName";
            this.colUserProjectItem.Name = "colUserProjectItem";
            this.colUserProjectItem.Visible = true;
            this.colUserProjectItem.VisibleIndex = 4;
            this.colUserProjectItem.Width = 118;
            // 
            // colPlanStartDate
            // 
            this.colPlanStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colPlanStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPlanStartDate.Caption = "Ngày bắt đầu dự kiến";
            this.colPlanStartDate.FieldName = "PlanStartDate";
            this.colPlanStartDate.Name = "colPlanStartDate";
            this.colPlanStartDate.Visible = true;
            this.colPlanStartDate.VisibleIndex = 8;
            this.colPlanStartDate.Width = 92;
            // 
            // colPlanEndDate
            // 
            this.colPlanEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colPlanEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPlanEndDate.Caption = "Ngày kết thúc dự kiến";
            this.colPlanEndDate.FieldName = "PlanEndDate";
            this.colPlanEndDate.Name = "colPlanEndDate";
            this.colPlanEndDate.Visible = true;
            this.colPlanEndDate.VisibleIndex = 10;
            this.colPlanEndDate.Width = 92;
            // 
            // colActualStartDate
            // 
            this.colActualStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colActualStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActualStartDate.Caption = "Ngày bắt đầu thực tế";
            this.colActualStartDate.FieldName = "ActualStartDate";
            this.colActualStartDate.Name = "colActualStartDate";
            this.colActualStartDate.Visible = true;
            this.colActualStartDate.VisibleIndex = 11;
            this.colActualStartDate.Width = 92;
            // 
            // colActualEndDate
            // 
            this.colActualEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colActualEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActualEndDate.Caption = "Ngày kết thúc thực tế";
            this.colActualEndDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colActualEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colActualEndDate.FieldName = "ActualEndDate";
            this.colActualEndDate.Name = "colActualEndDate";
            this.colActualEndDate.Visible = true;
            this.colActualEndDate.VisibleIndex = 12;
            this.colActualEndDate.Width = 91;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowSize = false;
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 43;
            // 
            // colNoteProjectItem
            // 
            this.colNoteProjectItem.Caption = "Ghi chú";
            this.colNoteProjectItem.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colNoteProjectItem.FieldName = "Note";
            this.colNoteProjectItem.Name = "colNoteProjectItem";
            this.colNoteProjectItem.Visible = true;
            this.colNoteProjectItem.VisibleIndex = 13;
            this.colNoteProjectItem.Width = 135;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colTotalDayPlan
            // 
            this.colTotalDayPlan.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayPlan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDayPlan.Caption = "Tổng số ngày";
            this.colTotalDayPlan.FieldName = "TotalDayPlan";
            this.colTotalDayPlan.Name = "colTotalDayPlan";
            this.colTotalDayPlan.Visible = true;
            this.colTotalDayPlan.VisibleIndex = 9;
            this.colTotalDayPlan.Width = 70;
            // 
            // colPercentItem
            // 
            this.colPercentItem.AppearanceCell.Options.UseTextOptions = true;
            this.colPercentItem.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPercentItem.Caption = "%";
            this.colPercentItem.DisplayFormat.FormatString = "#";
            this.colPercentItem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPercentItem.FieldName = "PercentItem";
            this.colPercentItem.Name = "colPercentItem";
            this.colPercentItem.Visible = true;
            this.colPercentItem.VisibleIndex = 5;
            this.colPercentItem.Width = 43;
            // 
            // colChargerID
            // 
            this.colChargerID.Caption = "Charger ID";
            this.colChargerID.FieldName = "UserID";
            this.colChargerID.Name = "colChargerID";
            // 
            // colPercentageActual
            // 
            this.colPercentageActual.AppearanceCell.Options.UseTextOptions = true;
            this.colPercentageActual.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPercentageActual.Caption = "% Thực tế";
            this.colPercentageActual.DisplayFormat.FormatString = "#";
            this.colPercentageActual.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPercentageActual.FieldName = "PercentageActual";
            this.colPercentageActual.Name = "colPercentageActual";
            this.colPercentageActual.Visible = true;
            this.colPercentageActual.VisibleIndex = 15;
            this.colPercentageActual.Width = 58;
            // 
            // colProjectEmployeeName
            // 
            this.colProjectEmployeeName.Caption = "Người tham gia";
            this.colProjectEmployeeName.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colProjectEmployeeName.FieldName = "ProjectEmployeeName";
            this.colProjectEmployeeName.Name = "colProjectEmployeeName";
            this.colProjectEmployeeName.Visible = true;
            this.colProjectEmployeeName.VisibleIndex = 14;
            this.colProjectEmployeeName.Width = 113;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Code", "{0}")});
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 95;
            // 
            // colEmployeeRequest
            // 
            this.colEmployeeRequest.Caption = "Người giao việc";
            this.colEmployeeRequest.FieldName = "EmployeeRequest";
            this.colEmployeeRequest.Name = "colEmployeeRequest";
            this.colEmployeeRequest.Visible = true;
            this.colEmployeeRequest.VisibleIndex = 7;
            this.colEmployeeRequest.Width = 116;
            // 
            // colItemLateActual
            // 
            this.colItemLateActual.FieldName = "ItemLateActual";
            this.colItemLateActual.Name = "colItemLateActual";
            // 
            // colProjectTypeName
            // 
            this.colProjectTypeName.Caption = "Kiểu hạng mục";
            this.colProjectTypeName.FieldName = "ProjectTypeName";
            this.colProjectTypeName.Name = "colProjectTypeName";
            this.colProjectTypeName.Visible = true;
            this.colProjectTypeName.VisibleIndex = 3;
            this.colProjectTypeName.Width = 119;
            // 
            // cboUserID
            // 
            this.cboUserID.AutoHeight = false;
            this.cboUserID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserID.Name = "cboUserID";
            this.cboUserID.NullText = "";
            this.cboUserID.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.gridColumn5});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridColumn5.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Họ tên";
            this.gridColumn5.FieldName = "FullName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // cboStatus
            // 
            this.cboStatus.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.cboStatus.AppearanceDropDownHeader.Options.UseFont = true;
            this.cboStatus.AppearanceDropDownHeader.Options.UseTextOptions = true;
            this.cboStatus.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cboStatus.AutoHeight = false;
            this.cboStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("value", "value", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("status", "Trạng thái", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.False)});
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.NullText = "";
            // 
            // btnDeleteRepo
            // 
            this.btnDeleteRepo.AutoHeight = false;
            this.btnDeleteRepo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDeleteRepo.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnDeleteRepo.Name = "btnDeleteRepo";
            this.btnDeleteRepo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 63);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(1594, 678);
            this.grdData.TabIndex = 170;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colDownloadFile,
            this.colGroupFileCode,
            this.colGroupFileName,
            this.colFileName,
            this.colPathShort,
            this.col,
            this.colDownloadGroupFile,
            this.colDownloadProject,
            this.colPathFull});
            this.grvData.CustomizationFormBounds = new System.Drawing.Rectangle(1150, -42, 210, 382);
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            // 
            // colDownloadFile
            // 
            this.colDownloadFile.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDownloadFile.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDownloadFile.AppearanceHeader.Options.UseFont = true;
            this.colDownloadFile.AppearanceHeader.Options.UseForeColor = true;
            this.colDownloadFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colDownloadFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDownloadFile.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDownloadFile.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDownloadFile.Caption = "Download File";
            this.colDownloadFile.ColumnEdit = this.btnDownloadFile;
            this.colDownloadFile.Name = "colDownloadFile";
            this.colDownloadFile.Visible = true;
            this.colDownloadFile.VisibleIndex = 1;
            this.colDownloadFile.Width = 60;
            // 
            // btnDownloadFile
            // 
            this.btnDownloadFile.AutoHeight = false;
            this.btnDownloadFile.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDownloadFile.Name = "btnDownloadFile";
            this.btnDownloadFile.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colGroupFileCode
            // 
            this.colGroupFileCode.Caption = "gridColumn4";
            this.colGroupFileCode.FieldName = "GroupFileCode";
            this.colGroupFileCode.Name = "colGroupFileCode";
            // 
            // colGroupFileName
            // 
            this.colGroupFileName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colGroupFileName.AppearanceCell.Options.UseFont = true;
            this.colGroupFileName.AppearanceCell.Options.UseTextOptions = true;
            this.colGroupFileName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGroupFileName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupFileName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colGroupFileName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGroupFileName.AppearanceHeader.Options.UseFont = true;
            this.colGroupFileName.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupFileName.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupFileName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGroupFileName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupFileName.Caption = "Nhóm File";
            this.colGroupFileName.FieldName = "GroupFileName";
            this.colGroupFileName.Name = "colGroupFileName";
            this.colGroupFileName.OptionsColumn.ReadOnly = true;
            this.colGroupFileName.Visible = true;
            this.colGroupFileName.VisibleIndex = 2;
            this.colGroupFileName.Width = 230;
            // 
            // colFileName
            // 
            this.colFileName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colFileName.AppearanceCell.Options.UseFont = true;
            this.colFileName.AppearanceCell.Options.UseTextOptions = true;
            this.colFileName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFileName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFileName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFileName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colFileName.AppearanceHeader.Options.UseFont = true;
            this.colFileName.AppearanceHeader.Options.UseForeColor = true;
            this.colFileName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFileName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFileName.Caption = "Tên file";
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.OptionsColumn.ReadOnly = true;
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 3;
            this.colFileName.Width = 279;
            // 
            // colPathShort
            // 
            this.colPathShort.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colPathShort.AppearanceCell.Options.UseFont = true;
            this.colPathShort.AppearanceCell.Options.UseTextOptions = true;
            this.colPathShort.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPathShort.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPathShort.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colPathShort.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPathShort.AppearanceHeader.Options.UseFont = true;
            this.colPathShort.AppearanceHeader.Options.UseForeColor = true;
            this.colPathShort.AppearanceHeader.Options.UseTextOptions = true;
            this.colPathShort.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPathShort.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPathShort.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPathShort.Caption = "Đường dẫn";
            this.colPathShort.FieldName = "PathShort";
            this.colPathShort.Name = "colPathShort";
            this.colPathShort.OptionsColumn.ReadOnly = true;
            this.colPathShort.Width = 372;
            // 
            // col
            // 
            this.col.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col.AppearanceCell.Options.UseFont = true;
            this.col.AppearanceCell.Options.UseTextOptions = true;
            this.col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.col.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.col.AppearanceHeader.Options.UseFont = true;
            this.col.AppearanceHeader.Options.UseForeColor = true;
            this.col.AppearanceHeader.Options.UseTextOptions = true;
            this.col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.col.Caption = "Mô tả dự án";
            this.col.FieldName = "Note";
            this.col.Name = "col";
            this.col.OptionsColumn.ReadOnly = true;
            this.col.Width = 273;
            // 
            // colDownloadGroupFile
            // 
            this.colDownloadGroupFile.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDownloadGroupFile.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDownloadGroupFile.AppearanceHeader.Options.UseFont = true;
            this.colDownloadGroupFile.AppearanceHeader.Options.UseForeColor = true;
            this.colDownloadGroupFile.AppearanceHeader.Options.UseTextOptions = true;
            this.colDownloadGroupFile.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDownloadGroupFile.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDownloadGroupFile.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDownloadGroupFile.Caption = "Download Nhóm File";
            this.colDownloadGroupFile.ColumnEdit = this.btnDownloadGroupFile;
            this.colDownloadGroupFile.Name = "colDownloadGroupFile";
            this.colDownloadGroupFile.Visible = true;
            this.colDownloadGroupFile.VisibleIndex = 0;
            this.colDownloadGroupFile.Width = 60;
            // 
            // btnDownloadGroupFile
            // 
            this.btnDownloadGroupFile.AutoHeight = false;
            this.btnDownloadGroupFile.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDownloadGroupFile.Name = "btnDownloadGroupFile";
            this.btnDownloadGroupFile.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colDownloadProject
            // 
            this.colDownloadProject.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colDownloadProject.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDownloadProject.AppearanceHeader.Options.UseFont = true;
            this.colDownloadProject.AppearanceHeader.Options.UseForeColor = true;
            this.colDownloadProject.AppearanceHeader.Options.UseTextOptions = true;
            this.colDownloadProject.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDownloadProject.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDownloadProject.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDownloadProject.Caption = "Download Dự Án";
            this.colDownloadProject.ColumnEdit = this.btnDownloadProject;
            this.colDownloadProject.Name = "colDownloadProject";
            this.colDownloadProject.Width = 60;
            // 
            // btnDownloadProject
            // 
            this.btnDownloadProject.AutoHeight = false;
            this.btnDownloadProject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDownloadProject.Name = "btnDownloadProject";
            this.btnDownloadProject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colPathFull
            // 
            this.colPathFull.Caption = "gridColumn3";
            this.colPathFull.FieldName = "PathFull";
            this.colPathFull.Name = "colPathFull";
            this.colPathFull.Width = 80;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdMaster);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 63);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1594, 439);
            this.panelControl1.TabIndex = 171;
            // 
            // frmProjectSummary
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1594, 741);
            this.Controls.Add(this.splitContainerControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboUserTech);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmProjectSummary";
            this.Text = "TỔNG HỢP DỰ ÁN";
            this.Load += new System.EventHandler(this.frmProjectSummary_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBusinessField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel2)).EndInit();
            this.stackPanel2.ResumeLayout(false);
            this.stackPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserTech.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlProjectTypeMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProjectItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvProjectItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRepo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownloadFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownloadGroupFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownloadProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboBusinessField;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView9;
        private DevExpress.XtraGrid.Columns.GridColumn colBussinessID;
        private DevExpress.XtraGrid.Columns.GridColumn colBussinessCode;
        private DevExpress.XtraGrid.Columns.GridColumn colBussinessName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBussinessNote;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cboProjectType;
        private System.Windows.Forms.Label ProjectType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cbCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchLookUpEdit cboLeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.SearchLookUpEdit cboPM;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.SearchLookUpEdit cbUser;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private DevExpress.Utils.Layout.StackPanel stackPanel2;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private System.Windows.Forms.TextBox txtShowCount;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraGrid.Columns.GridColumn colIDMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colPriotityText;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colFullNameTech;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colContactName;
        private DevExpress.XtraGrid.Columns.GridColumn colContactPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colContactEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colPO;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanDS;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanDE;
        private DevExpress.XtraGrid.Columns.GridColumn colActualDS;
        private DevExpress.XtraGrid.Columns.GridColumn colActualDE;
        private DevExpress.XtraGrid.Columns.GridColumn colEU;
        private DevExpress.XtraGrid.Columns.GridColumn colFullNamePM;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentState;
        private DevExpress.XtraGrid.Columns.GridColumn colPersonalPriotity;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusOld;
        private DevExpress.XtraGrid.Columns.GridColumn colPODate;
        private DevExpress.XtraGrid.Columns.GridColumn colPMID;
        private DevExpress.XtraGrid.Columns.GridColumn colUserIDSale;
        private DevExpress.XtraGrid.Columns.GridColumn colBussinessField;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectProcessType;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboProjectStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusName;
        private DevExpress.XtraEditors.SearchLookUpEdit cboUserTech;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn colUserMission;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnSettingTree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnPersonalPriotity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton btnProjectPartList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnProjectWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnSummaryWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton btnProjectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton btnProjectStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripButton bntProjectItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripButton btnGranttChartProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripButton btnProjectEmployees;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton btnChangeProjectReport;
        private System.Windows.Forms.ToolStripButton btnIsApproved;
        private System.Windows.Forms.ToolStripButton btnCancelApprove;
        private System.Windows.Forms.ToolStripButton btnGroupFile;
        private System.Windows.Forms.ToolStripButton btnProjectCost;
        private System.Windows.Forms.ToolStripButton btnProjectType;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraTreeList.TreeList tlProjectTypeMaster;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSeleted;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectTypeMaster;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLeaderMater;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectTypeID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLeaderID;
        private DevExpress.XtraGrid.GridControl grdProjectItem;
        private DevExpress.XtraGrid.Views.Grid.GridView grvProjectItem;
        private DevExpress.XtraGrid.Columns.GridColumn colMission;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colIdProjectItem;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colUserProjectItem;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPlanEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colActualStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colActualEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteProjectItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDayPlan;
        private DevExpress.XtraGrid.Columns.GridColumn colPercentItem;
        private DevExpress.XtraGrid.Columns.GridColumn colChargerID;
        private DevExpress.XtraGrid.Columns.GridColumn colPercentageActual;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeRequest;
        private DevExpress.XtraGrid.Columns.GridColumn colItemLateActual;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectTypeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboUserID;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteRepo;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colDownloadFile;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDownloadFile;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupFileCode;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colPathShort;
        private DevExpress.XtraGrid.Columns.GridColumn col;
        private DevExpress.XtraGrid.Columns.GridColumn colDownloadGroupFile;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDownloadGroupFile;
        private DevExpress.XtraGrid.Columns.GridColumn colDownloadProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDownloadProject;
        private DevExpress.XtraGrid.Columns.GridColumn colPathFull;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}