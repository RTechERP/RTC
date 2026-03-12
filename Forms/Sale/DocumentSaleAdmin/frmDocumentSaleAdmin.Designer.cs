namespace BMS
{
    partial class frmDocumentSaleAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentSaleAdmin));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDepartCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdDocument = new DevExpress.XtraGrid.GridControl();
            this.grvDocument = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumnentTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colDatePromulgate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateEffective = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.grdDocumentFile = new DevExpress.XtraGrid.GridControl();
            this.grvDocumentFile = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteDocFile = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileNameOrigin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colDocumentFilePath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddFile = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteFile = new System.Windows.Forms.ToolStripButton();
            this.btnDownLoad = new System.Windows.Forms.ToolStripButton();
            this.barLargeButtonItem1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem2 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem3 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem4 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocumentFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteDocFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel1.Controls.Add(this.label1);
            this.splitContainerControl1.Panel1.Controls.Add(this.cboDepartment);
            this.splitContainerControl1.Panel1.Controls.Add(this.grdDocument);
            this.splitContainerControl1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdDocumentFile);
            this.splitContainerControl1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1287, 640);
            this.splitContainerControl1.SplitterPosition = 855;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(392, 58);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Phòng ban";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Location = new System.Drawing.Point(85, 58);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDepartment.Size = new System.Drawing.Size(301, 26);
            this.cboDepartment.TabIndex = 24;
            this.cboDepartment.EditValueChanged += new System.EventHandler(this.cboDepartment_EditValueChanged);
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
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 50;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDepartCode,
            this.colDepartName});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colDepartCode
            // 
            this.colDepartCode.Caption = "Mã phòng ban";
            this.colDepartCode.FieldName = "Code";
            this.colDepartCode.Name = "colDepartCode";
            this.colDepartCode.Visible = true;
            this.colDepartCode.VisibleIndex = 0;
            this.colDepartCode.Width = 408;
            // 
            // colDepartName
            // 
            this.colDepartName.Caption = "Tên phòng ban";
            this.colDepartName.FieldName = "Name";
            this.colDepartName.Name = "colDepartName";
            this.colDepartName.Visible = true;
            this.colDepartName.VisibleIndex = 1;
            this.colDepartName.Width = 1077;
            // 
            // grdDocument
            // 
            this.grdDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDocument.Location = new System.Drawing.Point(12, 90);
            this.grdDocument.MainView = this.grvDocument;
            this.grdDocument.Name = "grdDocument";
            this.grdDocument.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2});
            this.grdDocument.Size = new System.Drawing.Size(843, 538);
            this.grdDocument.TabIndex = 21;
            this.grdDocument.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDocument});
            // 
            // grvDocument
            // 
            this.grvDocument.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDocument.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDocument.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDocument.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDocument.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocument.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDocument.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDocument.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDocument.Appearance.Row.Options.UseFont = true;
            this.grvDocument.Appearance.Row.Options.UseTextOptions = true;
            this.grvDocument.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDocument.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDocument.ColumnPanelRowHeight = 45;
            this.grvDocument.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSTT,
            this.colDocumnentTypeName,
            this.colDocumentCode,
            this.colDocumentName,
            this.colDatePromulgate,
            this.colDateEffective,
            this.colDepartment,
            this.colGroupType,
            this.colDepartmentCode});
            this.grvDocument.GridControl = this.grdDocument;
            this.grvDocument.GroupCount = 1;
            this.grvDocument.Name = "grvDocument";
            this.grvDocument.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvDocument.OptionsFind.FindNullPrompt = "Nhập từ khóa để tìm kiếm";
            this.grvDocument.OptionsView.RowAutoHeight = true;
            this.grvDocument.OptionsView.ShowFooter = true;
            this.grvDocument.OptionsView.ShowGroupPanel = false;
            this.grvDocument.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartment, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvDocument.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvDocument_FocusedRowChanged);
            this.grvDocument.DoubleClick += new System.EventHandler(this.grvDocument_DoubleClick);
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
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.OptionsColumn.ReadOnly = true;
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 42;
            // 
            // colDocumnentTypeName
            // 
            this.colDocumnentTypeName.Caption = "Loại văn bản";
            this.colDocumnentTypeName.FieldName = "NameDocumentType";
            this.colDocumnentTypeName.Name = "colDocumnentTypeName";
            this.colDocumnentTypeName.OptionsColumn.AllowEdit = false;
            this.colDocumnentTypeName.OptionsColumn.ReadOnly = true;
            this.colDocumnentTypeName.Visible = true;
            this.colDocumnentTypeName.VisibleIndex = 1;
            this.colDocumnentTypeName.Width = 139;
            // 
            // colDocumentCode
            // 
            this.colDocumentCode.Caption = "Mã văn bản";
            this.colDocumentCode.FieldName = "Code";
            this.colDocumentCode.Name = "colDocumentCode";
            this.colDocumentCode.OptionsColumn.AllowEdit = false;
            this.colDocumentCode.OptionsColumn.ReadOnly = true;
            this.colDocumentCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Code", "{0}")});
            this.colDocumentCode.Visible = true;
            this.colDocumentCode.VisibleIndex = 2;
            this.colDocumentCode.Width = 91;
            // 
            // colDocumentName
            // 
            this.colDocumentName.Caption = "Tên văn bản";
            this.colDocumentName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colDocumentName.FieldName = "NameDocument";
            this.colDocumentName.Name = "colDocumentName";
            this.colDocumentName.OptionsColumn.AllowEdit = false;
            this.colDocumentName.OptionsColumn.ReadOnly = true;
            this.colDocumentName.Visible = true;
            this.colDocumentName.VisibleIndex = 3;
            this.colDocumentName.Width = 168;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colDatePromulgate
            // 
            this.colDatePromulgate.Caption = "Ngày ban hành";
            this.colDatePromulgate.FieldName = "DatePromulgate";
            this.colDatePromulgate.Name = "colDatePromulgate";
            this.colDatePromulgate.OptionsColumn.AllowEdit = false;
            this.colDatePromulgate.OptionsColumn.ReadOnly = true;
            this.colDatePromulgate.Visible = true;
            this.colDatePromulgate.VisibleIndex = 4;
            this.colDatePromulgate.Width = 109;
            // 
            // colDateEffective
            // 
            this.colDateEffective.Caption = "Ngày hiệu lực";
            this.colDateEffective.FieldName = "DateEffective";
            this.colDateEffective.Name = "colDateEffective";
            this.colDateEffective.OptionsColumn.AllowEdit = false;
            this.colDateEffective.OptionsColumn.ReadOnly = true;
            this.colDateEffective.Visible = true;
            this.colDateEffective.VisibleIndex = 5;
            this.colDateEffective.Width = 97;
            // 
            // colDepartment
            // 
            this.colDepartment.Caption = "Phòng ban";
            this.colDepartment.FieldName = "DepartmentName";
            this.colDepartment.FieldNameSortGroup = "DepartmentID";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.Visible = true;
            this.colDepartment.VisibleIndex = 6;
            this.colDepartment.Width = 106;
            // 
            // colGroupType
            // 
            this.colGroupType.Caption = "GroupType";
            this.colGroupType.FieldName = "GroupType";
            this.colGroupType.Name = "colGroupType";
            this.colGroupType.Width = 112;
            // 
            // colDepartmentCode
            // 
            this.colDepartmentCode.Caption = "gridColumn1";
            this.colDepartmentCode.FieldName = "DepartmentCode";
            this.colDepartmentCode.Name = "colDepartmentCode";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(855, 55);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Forms.Properties.Resources.AddItem_32x32;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 52);
            this.btnAdd.Text = "THÊM";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = global::Forms.Properties.Resources.Edit_32x32;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(38, 52);
            this.btnEdit.Text = "SỬA";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Forms.Properties.Resources.DeleteList2_32x32;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(39, 52);
            this.btnDelete.Text = "XÓA";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grdDocumentFile
            // 
            this.grdDocumentFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDocumentFile.Location = new System.Drawing.Point(0, 55);
            this.grdDocumentFile.MainView = this.grvDocumentFile;
            this.grdDocumentFile.Name = "grdDocumentFile";
            this.grdDocumentFile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDeleteDocFile,
            this.repositoryItemMemoEdit4});
            this.grdDocumentFile.Size = new System.Drawing.Size(422, 585);
            this.grdDocumentFile.TabIndex = 1;
            this.grdDocumentFile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDocumentFile});
            // 
            // grvDocumentFile
            // 
            this.grvDocumentFile.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDocumentFile.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.grvDocumentFile.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDocumentFile.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDocumentFile.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDocumentFile.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDocumentFile.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDocumentFile.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDocumentFile.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvDocumentFile.Appearance.Row.Options.UseFont = true;
            this.grvDocumentFile.Appearance.Row.Options.UseTextOptions = true;
            this.grvDocumentFile.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDocumentFile.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDocumentFile.ColumnPanelRowHeight = 40;
            this.grvDocumentFile.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDFileName,
            this.colFileName,
            this.colFileNameOrigin,
            this.colDocumentFilePath});
            this.grvDocumentFile.GridControl = this.grdDocumentFile;
            this.grvDocumentFile.Name = "grvDocumentFile";
            this.grvDocumentFile.OptionsBehavior.Editable = false;
            this.grvDocumentFile.OptionsBehavior.ReadOnly = true;
            this.grvDocumentFile.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.grvDocumentFile.OptionsSelection.MultiSelect = true;
            this.grvDocumentFile.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvDocumentFile.OptionsView.RowAutoHeight = true;
            this.grvDocumentFile.OptionsView.ShowFooter = true;
            this.grvDocumentFile.OptionsView.ShowGroupPanel = false;
            // 
            // colIDFileName
            // 
            this.colIDFileName.ColumnEdit = this.btnDeleteDocFile;
            this.colIDFileName.FieldName = "ID";
            this.colIDFileName.ImageOptions.Image = global::Forms.Properties.Resources.cancel_16x16;
            this.colIDFileName.Name = "colIDFileName";
            // 
            // btnDeleteDocFile
            // 
            this.btnDeleteDocFile.AutoHeight = false;
            this.btnDeleteDocFile.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnDeleteDocFile.Name = "btnDeleteDocFile";
            this.btnDeleteDocFile.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colFileName
            // 
            this.colFileName.Caption = "Tên file";
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.OptionsColumn.ReadOnly = true;
            this.colFileName.Width = 140;
            // 
            // colFileNameOrigin
            // 
            this.colFileNameOrigin.Caption = "Tên file";
            this.colFileNameOrigin.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colFileNameOrigin.FieldName = "FileNameOrigin";
            this.colFileNameOrigin.Name = "colFileNameOrigin";
            this.colFileNameOrigin.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FileNameOrigin", "{0}")});
            this.colFileNameOrigin.Visible = true;
            this.colFileNameOrigin.VisibleIndex = 1;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colDocumentFilePath
            // 
            this.colDocumentFilePath.Caption = "Đường dẫn ";
            this.colDocumentFilePath.FieldName = "FilePath";
            this.colDocumentFilePath.Name = "colDocumentFilePath";
            this.colDocumentFilePath.OptionsColumn.ReadOnly = true;
            this.colDocumentFilePath.Width = 254;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddFile,
            this.btnDeleteFile,
            this.btnDownLoad});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(422, 55);
            this.toolStrip2.TabIndex = 23;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddFile
            // 
            this.btnAddFile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFile.Image = global::Forms.Properties.Resources.AddItem_32x32;
            this.btnAddFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(45, 52);
            this.btnAddFile.Text = "THÊM";
            this.btnAddFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFile.Image = global::Forms.Properties.Resources.DeleteList2_32x32;
            this.btnDeleteFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(39, 52);
            this.btnDeleteFile.Text = "XÓA";
            this.btnDeleteFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // btnDownLoad
            // 
            this.btnDownLoad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownLoad.Image = global::Forms.Properties.Resources.download_32x32;
            this.btnDownLoad.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDownLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownLoad.Name = "btnDownLoad";
            this.btnDownLoad.Size = new System.Drawing.Size(77, 52);
            this.btnDownLoad.Text = "TẢI XUỐNG";
            this.btnDownLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDownLoad.Click += new System.EventHandler(this.btnDownLoad_Click);
            // 
            // barLargeButtonItem1
            // 
            this.barLargeButtonItem1.Caption = "THÊM";
            this.barLargeButtonItem1.Id = 12;
            this.barLargeButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem1.ImageOptions.Image")));
            this.barLargeButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem1.ImageOptions.LargeImage")));
            this.barLargeButtonItem1.MinSize = new System.Drawing.Size(80, 0);
            this.barLargeButtonItem1.Name = "barLargeButtonItem1";
            // 
            // barLargeButtonItem2
            // 
            this.barLargeButtonItem2.Caption = "SỬA";
            this.barLargeButtonItem2.Id = 1;
            this.barLargeButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem2.ImageOptions.Image")));
            this.barLargeButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem2.ImageOptions.LargeImage")));
            this.barLargeButtonItem2.MinSize = new System.Drawing.Size(80, 0);
            this.barLargeButtonItem2.Name = "barLargeButtonItem2";
            // 
            // barLargeButtonItem3
            // 
            this.barLargeButtonItem3.Caption = "XOÁ";
            this.barLargeButtonItem3.Id = 13;
            this.barLargeButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem3.ImageOptions.Image")));
            this.barLargeButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem3.ImageOptions.LargeImage")));
            this.barLargeButtonItem3.MinSize = new System.Drawing.Size(80, 0);
            this.barLargeButtonItem3.Name = "barLargeButtonItem3";
            // 
            // barLargeButtonItem4
            // 
            this.barLargeButtonItem4.Caption = "XUẤT EXCEL";
            this.barLargeButtonItem4.Id = 15;
            this.barLargeButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem4.ImageOptions.Image")));
            this.barLargeButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem4.ImageOptions.LargeImage")));
            this.barLargeButtonItem4.Name = "barLargeButtonItem4";
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1287, 0);
            this.barDockControl4.Margin = new System.Windows.Forms.Padding(4);
            this.barDockControl4.Size = new System.Drawing.Size(0, 640);
            // 
            // frmDocumentSaleAdmin
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 640);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControl4);
            this.Name = "frmDocumentSaleAdmin";
            this.Text = "BIỂU MẪU VĂN BẢN CHUNG";
            this.Load += new System.EventHandler(this.frmDocumentSaleAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            this.splitContainerControl1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDocumentFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteDocFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem1;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem2;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem3;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem4;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraGrid.GridControl grdDocument;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDocument;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumnentTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colDatePromulgate;
        private DevExpress.XtraGrid.Columns.GridColumn colDateEffective;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartment;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupType;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddFile;
        private System.Windows.Forms.ToolStripButton btnDeleteFile;
        private DevExpress.XtraGrid.GridControl grdDocumentFile;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDocumentFile;
        private DevExpress.XtraGrid.Columns.GridColumn colIDFileName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteDocFile;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colFileNameOrigin;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentFilePath;
        private System.Windows.Forms.ToolStripButton btnDownLoad;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentCode;
    }
}