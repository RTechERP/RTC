
namespace BMS
{
    partial class frmProjectWorker
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectWorker));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.treeListData = new DevExpress.XtraTreeList.TreeList();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTT = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colWorkContent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAmountPeople = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNumberOfDay = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalWorkforce = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTotalPrice = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsApprovedTBP = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsApprovedTBPText = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsDeleted = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStatusTBP = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDeleted = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboProjectWorkerType = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.txtPrice = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtAmountPeople = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtNumberOfDay = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprovedTBP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnapprovedTBP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWorkerSynthetic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btlExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnNewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnImportExcel = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescriptionVersion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colIDVersion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddVersion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateVersion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRemoveVersion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcelVersion = new System.Windows.Forms.ToolStripButton();
            this.btnImportVersion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectWorkerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // treeListData
            // 
            this.treeListData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListData.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeListData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeListData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeListData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeListData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListData.Appearance.Row.Options.UseFont = true;
            this.treeListData.ColumnPanelRowHeight = 50;
            this.treeListData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colID,
            this.colTT,
            this.colWorkContent,
            this.colAmountPeople,
            this.colNumberOfDay,
            this.colTotalWorkforce,
            this.colPrice,
            this.colTotalPrice,
            this.colIsApprovedTBP,
            this.colIsApprovedTBPText,
            this.colParentID,
            this.colIsDeleted});
            this.treeListData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListData.Location = new System.Drawing.Point(2, 99);
            this.treeListData.Name = "treeListData";
            this.treeListData.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeListData.OptionsBehavior.Editable = false;
            this.treeListData.OptionsBehavior.PopulateServiceColumns = true;
            this.treeListData.OptionsBehavior.ReadOnly = true;
            this.treeListData.OptionsMenu.ShowExpandCollapseItems = false;
            this.treeListData.OptionsMenu.ShowFooterItem = true;
            this.treeListData.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treeListData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.treeListData.OptionsView.ShowSummaryFooter = true;
            this.treeListData.Size = new System.Drawing.Size(1388, 375);
            this.treeListData.TabIndex = 3;
            this.treeListData.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListData_NodeCellStyle);
            this.treeListData.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeListData_AfterCheckNode);
            this.treeListData.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeListData_CustomDrawNodeCell);
            this.treeListData.DoubleClick += new System.EventHandler(this.treeListData_DoubleClick);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colTT
            // 
            this.colTT.Caption = "TT";
            this.colTT.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTT.FieldName = "TT";
            this.colTT.Name = "colTT";
            this.colTT.Visible = true;
            this.colTT.VisibleIndex = 0;
            this.colTT.Width = 92;
            // 
            // colWorkContent
            // 
            this.colWorkContent.Caption = "Nội dung công việc";
            this.colWorkContent.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colWorkContent.FieldName = "WorkContent";
            this.colWorkContent.Name = "colWorkContent";
            this.colWorkContent.Visible = true;
            this.colWorkContent.VisibleIndex = 2;
            this.colWorkContent.Width = 537;
            // 
            // colAmountPeople
            // 
            this.colAmountPeople.Caption = "Số người";
            this.colAmountPeople.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAmountPeople.FieldName = "AmountPeople";
            this.colAmountPeople.MinWidth = 100;
            this.colAmountPeople.Name = "colAmountPeople";
            this.colAmountPeople.RowFooterSummary = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colAmountPeople.RowFooterSummaryStrFormat = "{0:0}";
            this.colAmountPeople.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colAmountPeople.SummaryFooterStrFormat = "{0:0}";
            this.colAmountPeople.Visible = true;
            this.colAmountPeople.VisibleIndex = 3;
            this.colAmountPeople.Width = 100;
            // 
            // colNumberOfDay
            // 
            this.colNumberOfDay.Caption = "Số ngày";
            this.colNumberOfDay.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNumberOfDay.FieldName = "NumberOfDay";
            this.colNumberOfDay.MinWidth = 100;
            this.colNumberOfDay.Name = "colNumberOfDay";
            this.colNumberOfDay.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colNumberOfDay.SummaryFooterStrFormat = "{0:0}";
            this.colNumberOfDay.Visible = true;
            this.colNumberOfDay.VisibleIndex = 4;
            this.colNumberOfDay.Width = 100;
            // 
            // colTotalWorkforce
            // 
            this.colTotalWorkforce.Caption = "Tổng nhân công";
            this.colTotalWorkforce.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTotalWorkforce.FieldName = "TotalWorkforce";
            this.colTotalWorkforce.MinWidth = 100;
            this.colTotalWorkforce.Name = "colTotalWorkforce";
            this.colTotalWorkforce.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colTotalWorkforce.SummaryFooterStrFormat = "{0:0}";
            this.colTotalWorkforce.Visible = true;
            this.colTotalWorkforce.VisibleIndex = 5;
            this.colTotalWorkforce.Width = 100;
            // 
            // colPrice
            // 
            this.colPrice.Caption = "Đơn giá";
            this.colPrice.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colPrice.FieldName = "Price";
            this.colPrice.Format.FormatString = "c0";
            this.colPrice.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colPrice.MinWidth = 200;
            this.colPrice.Name = "colPrice";
            this.colPrice.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colPrice.SummaryFooterStrFormat = "{0:c0}";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 6;
            this.colPrice.Width = 200;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.Caption = "Thành tiền";
            this.colTotalPrice.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Format.FormatString = "c0";
            this.colTotalPrice.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colTotalPrice.MinWidth = 200;
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum;
            this.colTotalPrice.SummaryFooterStrFormat = "{0:c0}";
            this.colTotalPrice.Visible = true;
            this.colTotalPrice.VisibleIndex = 7;
            this.colTotalPrice.Width = 200;
            // 
            // colIsApprovedTBP
            // 
            this.colIsApprovedTBP.Caption = "IsApprovedTBP";
            this.colIsApprovedTBP.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colIsApprovedTBP.FieldName = "IsApprovedTBP";
            this.colIsApprovedTBP.Name = "colIsApprovedTBP";
            // 
            // colIsApprovedTBPText
            // 
            this.colIsApprovedTBPText.Caption = "TBP duyệt";
            this.colIsApprovedTBPText.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colIsApprovedTBPText.FieldName = "IsApprovedTBPText";
            this.colIsApprovedTBPText.Name = "colIsApprovedTBPText";
            this.colIsApprovedTBPText.Visible = true;
            this.colIsApprovedTBPText.VisibleIndex = 1;
            this.colIsApprovedTBPText.Width = 84;
            // 
            // colParentID
            // 
            this.colParentID.Caption = "ParentID";
            this.colParentID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            // 
            // colIsDeleted
            // 
            this.colIsDeleted.Caption = "IsDeleted";
            this.colIsDeleted.FieldName = "IsDeleted";
            this.colIsDeleted.Name = "colIsDeleted";
            // 
            // stackPanel1
            // 
            this.stackPanel1.AutoSize = true;
            this.stackPanel1.Controls.Add(this.label1);
            this.stackPanel1.Controls.Add(this.cboStatusTBP);
            this.stackPanel1.Controls.Add(this.label4);
            this.stackPanel1.Controls.Add(this.cboDeleted);
            this.stackPanel1.Controls.Add(this.label3);
            this.stackPanel1.Controls.Add(this.cboProjectWorkerType);
            this.stackPanel1.Controls.Add(this.label2);
            this.stackPanel1.Controls.Add(this.txtFind);
            this.stackPanel1.Controls.Add(this.btnSearch);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackPanel1.Location = new System.Drawing.Point(2, 67);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(1388, 32);
            this.stackPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trạng thái TBP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboStatusTBP
            // 
            this.cboStatusTBP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusTBP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusTBP.FormattingEnabled = true;
            this.cboStatusTBP.Items.AddRange(new object[] {
            "---Tất cả---",
            "Chưa duyệt",
            "Đã duyệt"});
            this.cboStatusTBP.Location = new System.Drawing.Point(107, 4);
            this.cboStatusTBP.Name = "cboStatusTBP";
            this.cboStatusTBP.Size = new System.Drawing.Size(160, 24);
            this.cboStatusTBP.TabIndex = 1;
            this.cboStatusTBP.SelectedIndexChanged += new System.EventHandler(this.cboStatusTBP_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 207;
            this.label4.Text = "Tình trạng";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboDeleted
            // 
            this.cboDeleted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDeleted.FormattingEnabled = true;
            this.cboDeleted.Items.AddRange(new object[] {
            "---Tất cả---",
            "Chưa xóa",
            "Đã xóa"});
            this.cboDeleted.Location = new System.Drawing.Point(346, 4);
            this.cboDeleted.Name = "cboDeleted";
            this.cboDeleted.Size = new System.Drawing.Size(157, 24);
            this.cboDeleted.TabIndex = 208;
            this.cboDeleted.SelectedIndexChanged += new System.EventHandler(this.cboDeleted_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(509, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 205;
            this.label3.Text = "Loại";
            // 
            // cboProjectWorkerType
            // 
            this.cboProjectWorkerType.Location = new System.Drawing.Point(546, 5);
            this.cboProjectWorkerType.Name = "cboProjectWorkerType";
            this.cboProjectWorkerType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProjectWorkerType.Properties.Appearance.Options.UseFont = true;
            this.cboProjectWorkerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectWorkerType.Properties.NullText = "";
            this.cboProjectWorkerType.Properties.PopupView = this.gridView2;
            this.cboProjectWorkerType.Size = new System.Drawing.Size(154, 22);
            this.cboProjectWorkerType.TabIndex = 206;
            this.cboProjectWorkerType.EditValueChanged += new System.EventHandler(this.cboProjectWorkerType_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView2.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Loại nhân công dự án";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 640;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã loại nhân công";
            this.gridColumn3.FieldName = "Code";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 353;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(706, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Từ khóa";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.Location = new System.Drawing.Point(769, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(202, 22);
            this.txtFind.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(977, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.txtPrice),
            new DevExpress.XtraBars.LinkPersistInfo(this.txtAmountPeople),
            new DevExpress.XtraBars.LinkPersistInfo(this.txtNumberOfDay)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.MinWidth = 100;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // txtPrice
            // 
            this.txtPrice.Caption = "Đơn giá";
            this.txtPrice.Edit = this.repositoryItemTextEdit1;
            this.txtPrice.Id = 0;
            this.txtPrice.Name = "txtPrice";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repositoryItemTextEdit1_KeyDown);
            this.repositoryItemTextEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repositoryItemTextEdit1_KeyPress);
            // 
            // txtAmountPeople
            // 
            this.txtAmountPeople.Caption = "Số người";
            this.txtAmountPeople.Edit = this.repositoryItemTextEdit2;
            this.txtAmountPeople.Id = 1;
            this.txtAmountPeople.Name = "txtAmountPeople";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            this.repositoryItemTextEdit2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repositoryItemTextEdit2_KeyDown);
            this.repositoryItemTextEdit2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repositoryItemTextEdit2_KeyPress);
            // 
            // txtNumberOfDay
            // 
            this.txtNumberOfDay.Caption = "Số ngày";
            this.txtNumberOfDay.Edit = this.repositoryItemTextEdit3;
            this.txtNumberOfDay.Id = 2;
            this.txtNumberOfDay.Name = "txtNumberOfDay";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            this.repositoryItemTextEdit3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repositoryItemTextEdit3_KeyDown);
            this.repositoryItemTextEdit3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repositoryItemTextEdit3_KeyPress);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.txtAmountPeople,
            this.txtNumberOfDay,
            this.txtPrice});
            this.barManager1.MaxItemId = 3;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1392, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 715);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1392, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 715);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1392, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 715);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeListData);
            this.groupControl1.Controls.Add(this.stackPanel1);
            this.groupControl1.Controls.Add(this.toolStrip1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1392, 476);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "NHÂN CÔNG";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDelete,
            this.toolStripSeparator1,
            this.btnApprovedTBP,
            this.toolStripSeparator6,
            this.btnUnapprovedTBP,
            this.toolStripSeparator7,
            this.btnWorkerSynthetic,
            this.toolStripSeparator5,
            this.btlExcel,
            this.toolStripSeparator4,
            this.btnExportExcel});
            this.toolStrip1.Location = new System.Drawing.Point(2, 23);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1388, 44);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 41);
            this.btnNew.Tag = "frmProjectWorker_New";
            this.btnNew.Text = "Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.ToolTipText = "Thêm";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 41);
            this.btnEdit.Tag = "frmProjectWorker_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 41);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Forms.Properties.Resources.Clear_32x32;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 41);
            this.btnDelete.Tag = "frmProjectWorker_Delete";
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
            // btnApprovedTBP
            // 
            this.btnApprovedTBP.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnApprovedTBP.Image = ((System.Drawing.Image)(resources.GetObject("btnApprovedTBP.Image")));
            this.btnApprovedTBP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprovedTBP.Name = "btnApprovedTBP";
            this.btnApprovedTBP.Size = new System.Drawing.Size(85, 41);
            this.btnApprovedTBP.Tag = "frmProjectWorker_ApprovedTBP";
            this.btnApprovedTBP.Text = "TBP Duyệt";
            this.btnApprovedTBP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprovedTBP.Click += new System.EventHandler(this.btnApprovedTBP_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 41);
            // 
            // btnUnapprovedTBP
            // 
            this.btnUnapprovedTBP.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnUnapprovedTBP.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreviewLarge;
            this.btnUnapprovedTBP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnapprovedTBP.Name = "btnUnapprovedTBP";
            this.btnUnapprovedTBP.Size = new System.Drawing.Size(116, 41);
            this.btnUnapprovedTBP.Tag = "frmProjectWorker_UnapprovedTBP";
            this.btnUnapprovedTBP.Text = "TBP Hủy duyệt";
            this.btnUnapprovedTBP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnapprovedTBP.Click += new System.EventHandler(this.btnUnapprovedTBP_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 41);
            // 
            // btnWorkerSynthetic
            // 
            this.btnWorkerSynthetic.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnWorkerSynthetic.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ParametersLarge;
            this.btnWorkerSynthetic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWorkerSynthetic.Name = "btnWorkerSynthetic";
            this.btnWorkerSynthetic.Size = new System.Drawing.Size(79, 41);
            this.btnWorkerSynthetic.Text = "Tổng hợp";
            this.btnWorkerSynthetic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWorkerSynthetic.Click += new System.EventHandler(this.btnWorkerSynthetic_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 41);
            // 
            // btlExcel
            // 
            this.btlExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btlExcel.Image = ((System.Drawing.Image)(resources.GetObject("btlExcel.Image")));
            this.btlExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btlExcel.Name = "btlExcel";
            this.btlExcel.Size = new System.Drawing.Size(91, 41);
            this.btlExcel.Tag = "frmProjectWorker_ImportExcel";
            this.btlExcel.Text = "Nhập Excel";
            this.btlExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btlExcel.Click += new System.EventHandler(this.btlExcel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 41);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.AutoSize = false;
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(80, 41);
            this.btnExportExcel.Tag = "";
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grdData);
            this.groupControl2.Controls.Add(this.toolStrip2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1392, 229);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.Text = "PHIÊN BẢN";
            // 
            // grdData
            // 
            this.grdData.ContextMenuStrip = this.contextMenuStrip1;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(2, 67);
            this.grdData.MainView = this.grvData;
            this.grdData.MenuManager = this.barManager1;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit3,
            this.btnImportExcel});
            this.grdData.Size = new System.Drawing.Size(1388, 160);
            this.grdData.TabIndex = 4;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewVersion,
            this.btnEditVersion,
            this.btnDeleteVersion,
            this.mnuImportExcel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // btnNewVersion
            // 
            this.btnNewVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewVersion.Image = global::Forms.Properties.Resources.AddItem_16x16;
            this.btnNewVersion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNewVersion.Name = "btnNewVersion";
            this.btnNewVersion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.btnNewVersion.Size = new System.Drawing.Size(180, 22);
            this.btnNewVersion.Tag = "frmProjectWorker_AddVersion";
            this.btnNewVersion.Text = "Thêm";
            this.btnNewVersion.Click += new System.EventHandler(this.btnNewVersion_Click);
            // 
            // btnEditVersion
            // 
            this.btnEditVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditVersion.Image = global::Forms.Properties.Resources.edit_16x16;
            this.btnEditVersion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditVersion.Name = "btnEditVersion";
            this.btnEditVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEditVersion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.btnEditVersion.Size = new System.Drawing.Size(180, 22);
            this.btnEditVersion.Tag = "frmProjectWorker_UpdateVersion";
            this.btnEditVersion.Text = "Sửa";
            this.btnEditVersion.Click += new System.EventHandler(this.btnEditVersion_Click);
            // 
            // btnDeleteVersion
            // 
            this.btnDeleteVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteVersion.Image = global::Forms.Properties.Resources.Trash_16x16;
            this.btnDeleteVersion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteVersion.Name = "btnDeleteVersion";
            this.btnDeleteVersion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.btnDeleteVersion.Size = new System.Drawing.Size(180, 22);
            this.btnDeleteVersion.Tag = "frmProjectWorker_RemoveVersion";
            this.btnDeleteVersion.Text = "Xoá";
            this.btnDeleteVersion.Click += new System.EventHandler(this.btnDeleteVersion_Click);
            // 
            // mnuImportExcel
            // 
            this.mnuImportExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuImportExcel.Name = "mnuImportExcel";
            this.mnuImportExcel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuImportExcel.Size = new System.Drawing.Size(180, 22);
            this.mnuImportExcel.Tag = "frmProjectWorker_ImportExcel";
            this.mnuImportExcel.Text = "Nhập excel";
            this.mnuImportExcel.Click += new System.EventHandler(this.mnuImportExcel_Click);
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
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.colCode,
            this.colDescriptionVersion,
            this.colIDVersion,
            this.colSTT,
            this.colIsActive});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvData_FocusedRowChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.ColumnEdit = this.btnImportExcel;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 44;
            // 
            // btnImportExcel
            // 
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnImportExcel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnImportExcel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Tag = "frmProjectPartList_New_ImportExcel";
            this.btnImportExcel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.OptionsColumn.ReadOnly = true;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 2;
            this.colCode.Width = 140;
            // 
            // colDescriptionVersion
            // 
            this.colDescriptionVersion.Caption = "Mô tả";
            this.colDescriptionVersion.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colDescriptionVersion.FieldName = "DescriptionVersion";
            this.colDescriptionVersion.Name = "colDescriptionVersion";
            this.colDescriptionVersion.OptionsColumn.AllowEdit = false;
            this.colDescriptionVersion.OptionsColumn.ReadOnly = true;
            this.colDescriptionVersion.Visible = true;
            this.colDescriptionVersion.VisibleIndex = 3;
            this.colDescriptionVersion.Width = 1063;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colIDVersion
            // 
            this.colIDVersion.FieldName = "ID";
            this.colIDVersion.Name = "colIDVersion";
            this.colIDVersion.OptionsColumn.AllowEdit = false;
            this.colIDVersion.OptionsColumn.ReadOnly = true;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.OptionsColumn.ReadOnly = true;
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 1;
            this.colSTT.Width = 43;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "Sử dụng";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.OptionsColumn.AllowEdit = false;
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 0;
            this.colIsActive.Width = 67;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddVersion,
            this.toolStripSeparator8,
            this.btnUpdateVersion,
            this.toolStripSeparator9,
            this.btnRemoveVersion,
            this.toolStripSeparator10,
            this.btnExportExcelVersion,
            this.btnImportVersion,
            this.toolStripSeparator11});
            this.toolStrip2.Location = new System.Drawing.Point(2, 23);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1388, 44);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddVersion
            // 
            this.btnAddVersion.AutoSize = false;
            this.btnAddVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVersion.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVersion.Image")));
            this.btnAddVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddVersion.Name = "btnAddVersion";
            this.btnAddVersion.Size = new System.Drawing.Size(80, 41);
            this.btnAddVersion.Tag = "frmProjectWorker_AddVersion";
            this.btnAddVersion.Text = "Thêm";
            this.btnAddVersion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddVersion.ToolTipText = "Thêm";
            this.btnAddVersion.Click += new System.EventHandler(this.btnAddVersion_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.AutoSize = false;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 41);
            // 
            // btnUpdateVersion
            // 
            this.btnUpdateVersion.AutoSize = false;
            this.btnUpdateVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateVersion.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateVersion.Image")));
            this.btnUpdateVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateVersion.Name = "btnUpdateVersion";
            this.btnUpdateVersion.Size = new System.Drawing.Size(80, 41);
            this.btnUpdateVersion.Tag = "frmProjectWorker_UpdateVersion";
            this.btnUpdateVersion.Text = "Sửa";
            this.btnUpdateVersion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdateVersion.Click += new System.EventHandler(this.btnUpdateVersion_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.AutoSize = false;
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 41);
            // 
            // btnRemoveVersion
            // 
            this.btnRemoveVersion.AutoSize = false;
            this.btnRemoveVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveVersion.Image = global::Forms.Properties.Resources.Clear_32x32;
            this.btnRemoveVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveVersion.Name = "btnRemoveVersion";
            this.btnRemoveVersion.Size = new System.Drawing.Size(80, 41);
            this.btnRemoveVersion.Tag = "frmProjectWorker_RemoveVersion";
            this.btnRemoveVersion.Text = "Xóa";
            this.btnRemoveVersion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRemoveVersion.Click += new System.EventHandler(this.btnRemoveVersion_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.AutoSize = false;
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 41);
            // 
            // btnExportExcelVersion
            // 
            this.btnExportExcelVersion.AutoSize = false;
            this.btnExportExcelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcelVersion.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcelVersion.Image")));
            this.btnExportExcelVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcelVersion.Name = "btnExportExcelVersion";
            this.btnExportExcelVersion.Size = new System.Drawing.Size(80, 41);
            this.btnExportExcelVersion.Tag = "";
            this.btnExportExcelVersion.Text = "Xuất Excel";
            this.btnExportExcelVersion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcelVersion.Click += new System.EventHandler(this.btnExportExcelVersion_Click);
            // 
            // btnImportVersion
            // 
            this.btnImportVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportVersion.Image = ((System.Drawing.Image)(resources.GetObject("btnImportVersion.Image")));
            this.btnImportVersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportVersion.Name = "btnImportVersion";
            this.btnImportVersion.Size = new System.Drawing.Size(91, 41);
            this.btnImportVersion.Tag = "frmProjectWorker_Excel";
            this.btnImportVersion.Text = "Nhập Excel";
            this.btnImportVersion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportVersion.Visible = false;
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.AutoSize = false;
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 41);
            this.toolStripSeparator11.Visible = false;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1392, 715);
            this.splitContainerControl1.SplitterPosition = 229;
            this.splitContainerControl1.TabIndex = 11;
            // 
            // frmProjectWorker
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 715);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmProjectWorker";
            this.Text = "NHÂN CÔNG DỰ ÁN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProjectWorker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectWorkerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraTreeList.TreeList treeListData;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStatusTBP;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colWorkContent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAmountPeople;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNumberOfDay;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalWorkforce;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTotalPrice;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsApprovedTBP;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsApprovedTBPText;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SearchLookUpEdit cboProjectWorkerType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsDeleted;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDeleted;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraBars.BarEditItem txtPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarEditItem txtAmountPeople;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarEditItem txtNumberOfDay;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btlExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnApprovedTBP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnUnapprovedTBP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnWorkerSynthetic;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnUpdateVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnRemoveVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnImportVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton btnExportExcelVersion;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnImportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDescriptionVersion;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colIDVersion;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnNewVersion;
        private System.Windows.Forms.ToolStripMenuItem btnEditVersion;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteVersion;
        private System.Windows.Forms.ToolStripMenuItem mnuImportExcel;
    }
}