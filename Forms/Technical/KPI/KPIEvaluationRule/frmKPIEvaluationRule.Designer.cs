
namespace BMS
{
    partial class frmKPIEvaluationRule
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
            this.treeData = new DevExpress.XtraTreeList.TreeList();
            this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSTT = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEvaluationCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRuleContent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colMaxPercent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPercentageAdjustment = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMaxPercentageAdjustment = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRuleNote = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNote = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colKPIEvaluationRuleID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboDepartMent = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSessionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colSessionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSessionCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSesionYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSesionQuarter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdDetails = new DevExpress.XtraGrid.GridControl();
            this.grvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRuleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colRuleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRuleKPISessionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRuleKPIPositionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboPosition = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colRuleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypePositionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnAddExam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateExam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteExam = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCopyExam = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddRule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditRule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteRule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartMent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPosition)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeData
            // 
            this.treeData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.treeData.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeData.Appearance.Row.Options.UseFont = true;
            this.treeData.Appearance.Row.Options.UseTextOptions = true;
            this.treeData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.treeData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.treeData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colParentID,
            this.colID,
            this.colSTT,
            this.colEvaluationCode,
            this.colRuleContent,
            this.colMaxPercent,
            this.colPercentageAdjustment,
            this.colMaxPercentageAdjustment,
            this.colRuleNote,
            this.colNote,
            this.colKPIEvaluationRuleID});
            this.treeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeData.Location = new System.Drawing.Point(0, 55);
            this.treeData.Name = "treeData";
            this.treeData.OptionsBehavior.Editable = false;
            this.treeData.OptionsBehavior.PopulateServiceColumns = true;
            this.treeData.OptionsBehavior.ReadOnly = true;
            this.treeData.OptionsView.AutoWidth = false;
            this.treeData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.treeData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.treeData.Size = new System.Drawing.Size(838, 573);
            this.treeData.TabIndex = 128;
            this.treeData.CustomColumnDisplayText += new DevExpress.XtraTreeList.CustomColumnDisplayTextEventHandler(this.treeData_CustomColumnDisplayText);
            this.treeData.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeData_CustomDrawNodeCell);
            this.treeData.DoubleClick += new System.EventHandler(this.treeData_DoubleClick);
            // 
            // colParentID
            // 
            this.colParentID.Caption = "ParentID";
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colSTT
            // 
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 148;
            // 
            // colEvaluationCode
            // 
            this.colEvaluationCode.Caption = "Mã Rule";
            this.colEvaluationCode.FieldName = "EvaluationCode";
            this.colEvaluationCode.Name = "colEvaluationCode";
            this.colEvaluationCode.Visible = true;
            this.colEvaluationCode.VisibleIndex = 1;
            this.colEvaluationCode.Width = 153;
            // 
            // colRuleContent
            // 
            this.colRuleContent.Caption = "Nội dung đánh giá";
            this.colRuleContent.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colRuleContent.FieldName = "RuleContent";
            this.colRuleContent.Name = "colRuleContent";
            this.colRuleContent.Visible = true;
            this.colRuleContent.VisibleIndex = 2;
            this.colRuleContent.Width = 328;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colMaxPercent
            // 
            this.colMaxPercent.Caption = "Tổng % thưởng tối đa";
            this.colMaxPercent.FieldName = "MaxPercent";
            this.colMaxPercent.Format.FormatString = "n1";
            this.colMaxPercent.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxPercent.Name = "colMaxPercent";
            this.colMaxPercent.Visible = true;
            this.colMaxPercent.VisibleIndex = 3;
            this.colMaxPercent.Width = 97;
            // 
            // colPercentageAdjustment
            // 
            this.colPercentageAdjustment.Caption = "Số % trừ (cộng) 1 lần";
            this.colPercentageAdjustment.FieldName = "PercentageAdjustment";
            this.colPercentageAdjustment.Format.FormatString = "n1";
            this.colPercentageAdjustment.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPercentageAdjustment.Name = "colPercentageAdjustment";
            this.colPercentageAdjustment.Visible = true;
            this.colPercentageAdjustment.VisibleIndex = 4;
            this.colPercentageAdjustment.Width = 92;
            // 
            // colMaxPercentageAdjustment
            // 
            this.colMaxPercentageAdjustment.Caption = "Số % trừ (cộng) lớn nhất";
            this.colMaxPercentageAdjustment.FieldName = "MaxPercentageAdjustment";
            this.colMaxPercentageAdjustment.Format.FormatString = "n1";
            this.colMaxPercentageAdjustment.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxPercentageAdjustment.Name = "colMaxPercentageAdjustment";
            this.colMaxPercentageAdjustment.Visible = true;
            this.colMaxPercentageAdjustment.VisibleIndex = 5;
            this.colMaxPercentageAdjustment.Width = 111;
            // 
            // colRuleNote
            // 
            this.colRuleNote.Caption = "Rule";
            this.colRuleNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colRuleNote.FieldName = "RuleNote";
            this.colRuleNote.Name = "colRuleNote";
            this.colRuleNote.Visible = true;
            this.colRuleNote.VisibleIndex = 6;
            this.colRuleNote.Width = 306;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 7;
            this.colNote.Width = 325;
            // 
            // colKPIEvaluationRuleID
            // 
            this.colKPIEvaluationRuleID.Caption = "KPIEvaluationRuleID";
            this.colKPIEvaluationRuleID.FieldName = "KPIEvaluationRuleID";
            this.colKPIEvaluationRuleID.Name = "colKPIEvaluationRuleID";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.cboDepartMent);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtYear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1294, 42);
            this.panelControl1.TabIndex = 130;
            // 
            // cboDepartMent
            // 
            this.cboDepartMent.EditValue = "";
            this.cboDepartMent.Enabled = false;
            this.cboDepartMent.Location = new System.Drawing.Point(74, 8);
            this.cboDepartMent.Name = "cboDepartMent";
            this.cboDepartMent.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartMent.Properties.Appearance.Options.UseFont = true;
            this.cboDepartMent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartMent.Properties.NullText = "";
            this.cboDepartMent.Properties.PopupView = this.gridView3;
            this.cboDepartMent.Size = new System.Drawing.Size(175, 24);
            this.cboDepartMent.TabIndex = 284;
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridView3.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.gridView3.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView3.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView3.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.Appearance.Row.Options.UseTextOptions = true;
            this.gridView3.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridView3.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView3.ColumnPanelRowHeight = 40;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn24});
            this.gridView3.DetailHeight = 355;
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsBehavior.ReadOnly = true;
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "ID";
            this.gridColumn5.FieldName = "ID";
            this.gridColumn5.MinWidth = 19;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 70;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Tên phòng ban";
            this.gridColumn6.FieldName = "Name";
            this.gridColumn6.MinWidth = 19;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 1115;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Mã phòng ban";
            this.gridColumn24.FieldName = "Code";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 0;
            this.gridColumn24.Width = 500;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 283;
            this.label2.Text = "Phòng ban";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 113;
            this.label3.Text = "Năm";
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(294, 8);
            this.txtYear.Margin = new System.Windows.Forms.Padding(4);
            this.txtYear.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(75, 24);
            this.txtYear.TabIndex = 112;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Location = new System.Drawing.Point(377, 7);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 27);
            this.btnSearch.TabIndex = 103;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 42);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.treeData);
            this.splitContainerControl1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1294, 628);
            this.splitContainerControl1.SplitterPosition = 446;
            this.splitContainerControl1.TabIndex = 131;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(446, 628);
            this.splitContainerControl2.SplitterPosition = 278;
            this.splitContainerControl2.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.grdMaster);
            this.groupControl1.Controls.Add(this.toolStrip1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(446, 278);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "KỲ ĐÁNH GIÁ";
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grdMaster.Location = new System.Drawing.Point(2, 78);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Margin = new System.Windows.Forms.Padding(4);
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
            this.grdMaster.Size = new System.Drawing.Size(442, 198);
            this.grdMaster.TabIndex = 1;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            // 
            // grvMaster
            // 
            this.grvMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.grvMaster.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvMaster.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvMaster.Appearance.Row.Options.UseFont = true;
            this.grvMaster.Appearance.Row.Options.UseTextOptions = true;
            this.grvMaster.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.AutoFillColumn = this.colSessionName;
            this.grvMaster.ColumnPanelRowHeight = 50;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSessionID,
            this.colSessionCode,
            this.colSessionName,
            this.colSesionYear,
            this.colSesionQuarter,
            this.colDepartmentID});
            this.grvMaster.DetailHeight = 444;
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsBehavior.ReadOnly = true;
            this.grvMaster.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsView.RowAutoHeight = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMaster_FocusedRowChanged);
            this.grvMaster.DoubleClick += new System.EventHandler(this.grvMaster_DoubleClick);
            // 
            // colSessionName
            // 
            this.colSessionName.Caption = "Tên kỳ";
            this.colSessionName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSessionName.FieldName = "Name";
            this.colSessionName.MinWidth = 29;
            this.colSessionName.Name = "colSessionName";
            this.colSessionName.Visible = true;
            this.colSessionName.VisibleIndex = 1;
            this.colSessionName.Width = 122;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colSessionID
            // 
            this.colSessionID.Caption = "ID";
            this.colSessionID.FieldName = "ID";
            this.colSessionID.MinWidth = 29;
            this.colSessionID.Name = "colSessionID";
            this.colSessionID.Width = 109;
            // 
            // colSessionCode
            // 
            this.colSessionCode.Caption = "Mã kỳ";
            this.colSessionCode.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSessionCode.FieldName = "Code";
            this.colSessionCode.MinWidth = 29;
            this.colSessionCode.Name = "colSessionCode";
            this.colSessionCode.Visible = true;
            this.colSessionCode.VisibleIndex = 0;
            this.colSessionCode.Width = 128;
            // 
            // colSesionYear
            // 
            this.colSesionYear.AppearanceCell.Options.UseTextOptions = true;
            this.colSesionYear.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSesionYear.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSesionYear.Caption = "Năm";
            this.colSesionYear.FieldName = "YearEvaluation";
            this.colSesionYear.MinWidth = 29;
            this.colSesionYear.Name = "colSesionYear";
            this.colSesionYear.Visible = true;
            this.colSesionYear.VisibleIndex = 2;
            this.colSesionYear.Width = 57;
            // 
            // colSesionQuarter
            // 
            this.colSesionQuarter.AppearanceCell.Options.UseTextOptions = true;
            this.colSesionQuarter.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSesionQuarter.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSesionQuarter.Caption = "Quý";
            this.colSesionQuarter.FieldName = "QuarterEvaluation";
            this.colSesionQuarter.MinWidth = 29;
            this.colSesionQuarter.Name = "colSesionQuarter";
            this.colSesionQuarter.Visible = true;
            this.colSesionQuarter.VisibleIndex = 3;
            this.colSesionQuarter.Width = 49;
            // 
            // colDepartmentID
            // 
            this.colDepartmentID.Caption = "Phòng ban";
            this.colDepartmentID.FieldName = "DepartmentID";
            this.colDepartmentID.Name = "colDepartmentID";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSession,
            this.toolStripSeparator3,
            this.btnUpdateSession,
            this.toolStripSeparator5,
            this.btnDeleteSession,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(2, 23);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(442, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddSession
            // 
            this.btnAddSession.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSession.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAddSession.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSession.Name = "btnAddSession";
            this.btnAddSession.Size = new System.Drawing.Size(45, 52);
            this.btnAddSession.Tag = "frmKPIEvaluationFactors_AddSession";
            this.btnAddSession.Text = "Thêm";
            this.btnAddSession.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddSession.Click += new System.EventHandler(this.btAddSession_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 50);
            // 
            // btnUpdateSession
            // 
            this.btnUpdateSession.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSession.Image = global::Forms.Properties.Resources.Edit_32x321;
            this.btnUpdateSession.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUpdateSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateSession.Name = "btnUpdateSession";
            this.btnUpdateSession.Size = new System.Drawing.Size(36, 52);
            this.btnUpdateSession.Tag = "frmKPIEvaluationFactors_UpdateSession";
            this.btnUpdateSession.Text = "Sửa";
            this.btnUpdateSession.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdateSession.Click += new System.EventHandler(this.btnUpdateSession_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDeleteSession
            // 
            this.btnDeleteSession.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSession.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDeleteSession.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteSession.Name = "btnDeleteSession";
            this.btnDeleteSession.Size = new System.Drawing.Size(36, 52);
            this.btnDeleteSession.Tag = "frmKPIEvaluationFactors_DeleteSession";
            this.btnDeleteSession.Text = "Xóa";
            this.btnDeleteSession.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteSession.Click += new System.EventHandler(this.btnDeleteSession_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.grdDetails);
            this.groupControl2.Controls.Add(this.toolStrip3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(446, 340);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "RULE ĐÁNH GIÁ";
            // 
            // grdDetails
            // 
            this.grdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetails.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grdDetails.Location = new System.Drawing.Point(2, 78);
            this.grdDetails.MainView = this.grvDetails;
            this.grdDetails.Margin = new System.Windows.Forms.Padding(4);
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit3,
            this.cboPosition});
            this.grdDetails.Size = new System.Drawing.Size(442, 260);
            this.grdDetails.TabIndex = 1;
            this.grdDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDetails});
            // 
            // grvDetails
            // 
            this.grvDetails.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.grvDetails.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvDetails.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDetails.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDetails.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDetails.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDetails.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDetails.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDetails.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDetails.Appearance.Row.Options.UseFont = true;
            this.grvDetails.Appearance.Row.Options.UseTextOptions = true;
            this.grvDetails.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDetails.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDetails.AutoFillColumn = this.colRuleName;
            this.grvDetails.ColumnPanelRowHeight = 50;
            this.grvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRuleID,
            this.colRuleKPISessionID,
            this.colRuleKPIPositionID,
            this.colRuleCode,
            this.colRuleName,
            this.colTypePositionName});
            this.grvDetails.DetailHeight = 444;
            this.grvDetails.GridControl = this.grdDetails;
            this.grvDetails.GroupCount = 1;
            this.grvDetails.Name = "grvDetails";
            this.grvDetails.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvDetails.OptionsBehavior.Editable = false;
            this.grvDetails.OptionsBehavior.ReadOnly = true;
            this.grvDetails.OptionsView.RowAutoHeight = true;
            this.grvDetails.OptionsView.ShowGroupPanel = false;
            this.grvDetails.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTypePositionName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvDetails.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDetails_FocusedRowChanged);
            this.grvDetails.DoubleClick += new System.EventHandler(this.grvDetails_DoubleClick);
            // 
            // colRuleName
            // 
            this.colRuleName.Caption = "Tên đánh giá";
            this.colRuleName.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colRuleName.FieldName = "RuleName";
            this.colRuleName.MinWidth = 29;
            this.colRuleName.Name = "colRuleName";
            this.colRuleName.Visible = true;
            this.colRuleName.VisibleIndex = 1;
            this.colRuleName.Width = 167;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colRuleID
            // 
            this.colRuleID.Caption = "ID";
            this.colRuleID.FieldName = "ID";
            this.colRuleID.MinWidth = 29;
            this.colRuleID.Name = "colRuleID";
            this.colRuleID.Width = 109;
            // 
            // colRuleKPISessionID
            // 
            this.colRuleKPISessionID.Caption = "KPISessionID";
            this.colRuleKPISessionID.FieldName = "KPISessionID";
            this.colRuleKPISessionID.MinWidth = 29;
            this.colRuleKPISessionID.Name = "colRuleKPISessionID";
            this.colRuleKPISessionID.Width = 109;
            // 
            // colRuleKPIPositionID
            // 
            this.colRuleKPIPositionID.Caption = "Vị trí";
            this.colRuleKPIPositionID.ColumnEdit = this.cboPosition;
            this.colRuleKPIPositionID.FieldName = "KPIPositionID";
            this.colRuleKPIPositionID.MinWidth = 29;
            this.colRuleKPIPositionID.Name = "colRuleKPIPositionID";
            this.colRuleKPIPositionID.Width = 62;
            // 
            // cboPosition
            // 
            this.cboPosition.AutoHeight = false;
            this.cboPosition.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPosition.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PositionCode", "Mã vị trí"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PositionName", "Tên vị trí")});
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.NullText = "";
            // 
            // colRuleCode
            // 
            this.colRuleCode.Caption = "Mã đánh giá";
            this.colRuleCode.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colRuleCode.FieldName = "RuleCode";
            this.colRuleCode.MinWidth = 29;
            this.colRuleCode.Name = "colRuleCode";
            this.colRuleCode.Visible = true;
            this.colRuleCode.VisibleIndex = 0;
            this.colRuleCode.Width = 127;
            // 
            // colTypePositionName
            // 
            this.colTypePositionName.Caption = "Chức vụ";
            this.colTypePositionName.FieldName = "TypePositionName";
            this.colTypePositionName.Name = "colTypePositionName";
            this.colTypePositionName.Visible = true;
            this.colTypePositionName.VisibleIndex = 2;
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddExam,
            this.toolStripSeparator6,
            this.btnUpdateExam,
            this.toolStripSeparator7,
            this.btnDeleteExam,
            this.toolStripSeparator8,
            this.btnCopyExam});
            this.toolStrip3.Location = new System.Drawing.Point(2, 23);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(442, 55);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnAddExam
            // 
            this.btnAddExam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddExam.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAddExam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddExam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddExam.Name = "btnAddExam";
            this.btnAddExam.Size = new System.Drawing.Size(45, 52);
            this.btnAddExam.Tag = "frmKPIEvaluationFactors_AddExam";
            this.btnAddExam.Text = "Thêm";
            this.btnAddExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddExam.Click += new System.EventHandler(this.btnAddExam_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 50);
            // 
            // btnUpdateExam
            // 
            this.btnUpdateExam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateExam.Image = global::Forms.Properties.Resources.Edit_32x321;
            this.btnUpdateExam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUpdateExam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateExam.Name = "btnUpdateExam";
            this.btnUpdateExam.Size = new System.Drawing.Size(36, 52);
            this.btnUpdateExam.Tag = "frmKPIEvaluationFactors_UpdateExam";
            this.btnUpdateExam.Text = "Sửa";
            this.btnUpdateExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdateExam.Click += new System.EventHandler(this.btnUpdateExam_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDeleteExam
            // 
            this.btnDeleteExam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteExam.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDeleteExam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteExam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteExam.Name = "btnDeleteExam";
            this.btnDeleteExam.Size = new System.Drawing.Size(36, 52);
            this.btnDeleteExam.Tag = "frmKPIEvaluationFactors_DeleteExam";
            this.btnDeleteExam.Text = "Xóa";
            this.btnDeleteExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteExam.Click += new System.EventHandler(this.btnDeleteExam_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.AutoSize = false;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 50);
            // 
            // btnCopyExam
            // 
            this.btnCopyExam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyExam.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_PageOrientationLarge;
            this.btnCopyExam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCopyExam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyExam.Name = "btnCopyExam";
            this.btnCopyExam.Size = new System.Drawing.Size(70, 52);
            this.btnCopyExam.Tag = "frmKPIEvaluationFactors_Copy";
            this.btnCopyExam.Text = "Sao chép";
            this.btnCopyExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCopyExam.Click += new System.EventHandler(this.btnCopyExam_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddRule,
            this.toolStripSeparator2,
            this.btnEditRule,
            this.toolStripSeparator4,
            this.btnDeleteRule,
            this.toolStripSeparator9});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(838, 55);
            this.toolStrip2.TabIndex = 129;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddRule
            // 
            this.btnAddRule.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRule.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAddRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(45, 52);
            this.btnAddRule.Tag = "frmKPIEvaluationFactors_AddSession";
            this.btnAddRule.Text = "Thêm";
            this.btnAddRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // btnEditRule
            // 
            this.btnEditRule.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRule.Image = global::Forms.Properties.Resources.Edit_32x321;
            this.btnEditRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditRule.Name = "btnEditRule";
            this.btnEditRule.Size = new System.Drawing.Size(36, 52);
            this.btnEditRule.Tag = "frmKPIEvaluationFactors_UpdateSession";
            this.btnEditRule.Text = "Sửa";
            this.btnEditRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditRule.Click += new System.EventHandler(this.btnEditRule_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 50);
            // 
            // btnDeleteRule
            // 
            this.btnDeleteRule.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteRule.Image = global::Forms.Properties.Resources.Trash_32x32;
            this.btnDeleteRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Size = new System.Drawing.Size(36, 52);
            this.btnDeleteRule.Tag = "frmKPIEvaluationFactors_DeleteSession";
            this.btnDeleteRule.Text = "Xóa";
            this.btnDeleteRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteRule.Click += new System.EventHandler(this.btnDeleteRule_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.AutoSize = false;
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 50);
            // 
            // frmKPIEvaluationRule
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 670);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmKPIEvaluationRule";
            this.Text = "RULE ĐÁNH GIÁ";
            this.Load += new System.EventHandler(this.frmKPIEvaluationRule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartMent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            this.splitContainerControl1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPosition)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeData;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Button btnSearch;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colSessionName;
        private DevExpress.XtraGrid.Columns.GridColumn colSessionID;
        private DevExpress.XtraGrid.Columns.GridColumn colSessionCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSesionYear;
        private DevExpress.XtraGrid.Columns.GridColumn colSesionQuarter;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentID;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddSession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnUpdateSession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnDeleteSession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleName;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleID;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleKPISessionID;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleKPIPositionID;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colRuleCode;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnAddExam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnUpdateExam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnDeleteExam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnCopyExam;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSTT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRuleContent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMaxPercent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPercentageAdjustment;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMaxPercentageAdjustment;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRuleNote;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNote;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colKPIEvaluationRuleID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtYear;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddRule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEditRule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDeleteRule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboPosition;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEvaluationCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
		private DevExpress.XtraEditors.SearchLookUpEdit cboDepartMent;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
		private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn colTypePositionName;
    }
}