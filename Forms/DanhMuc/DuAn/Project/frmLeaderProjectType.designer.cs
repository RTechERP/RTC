
namespace BMS
{
    partial class frmLeaderProjectType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeaderProjectType));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlProjectType = new DevExpress.XtraTreeList.TreeList();
            this.colProjectTypeID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProjectTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.grdLeader = new DevExpress.XtraGrid.GridControl();
            this.grvLeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLeaderID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLeaderName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLeaderCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboUser = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.btnDeleteLeader = new System.Windows.Forms.Button();
            this.btnAddLeader = new System.Windows.Forms.Button();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnAddLeaders = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnDeleteLeaders = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlProjectType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 56);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tlProjectType);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdLeader);
            this.splitContainerControl1.Panel2.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1308, 727);
            this.splitContainerControl1.SplitterPosition = 780;
            this.splitContainerControl1.TabIndex = 20;
            // 
            // tlProjectType
            // 
            this.tlProjectType.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlProjectType.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseFont = true;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.tlProjectType.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlProjectType.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tlProjectType.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tlProjectType.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlProjectType.Appearance.Row.Options.UseFont = true;
            this.tlProjectType.Appearance.Row.Options.UseTextOptions = true;
            this.tlProjectType.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tlProjectType.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tlProjectType.ColumnPanelRowHeight = 50;
            this.tlProjectType.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colProjectTypeID,
            this.colProjectTypeName});
            this.tlProjectType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlProjectType.Location = new System.Drawing.Point(0, 0);
            this.tlProjectType.Name = "tlProjectType";
            this.tlProjectType.OptionsBehavior.PopulateServiceColumns = true;
            this.tlProjectType.OptionsCustomization.AllowFilter = false;
            this.tlProjectType.OptionsCustomization.AllowSort = false;
            this.tlProjectType.Size = new System.Drawing.Size(780, 727);
            this.tlProjectType.TabIndex = 4;
            this.tlProjectType.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlProjectType_FocusedNodeChanged);
            // 
            // colProjectTypeID
            // 
            this.colProjectTypeID.FieldName = "ID";
            this.colProjectTypeID.Name = "colProjectTypeID";
            // 
            // colProjectTypeName
            // 
            this.colProjectTypeName.Caption = "Kiểu dự án";
            this.colProjectTypeName.FieldName = "ProjectTypeName";
            this.colProjectTypeName.Name = "colProjectTypeName";
            this.colProjectTypeName.OptionsColumn.AllowEdit = false;
            this.colProjectTypeName.OptionsColumn.ReadOnly = true;
            this.colProjectTypeName.Visible = true;
            this.colProjectTypeName.VisibleIndex = 0;
            this.colProjectTypeName.Width = 122;
            // 
            // grdLeader
            // 
            this.grdLeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLeader.Location = new System.Drawing.Point(0, 0);
            this.grdLeader.MainView = this.grvLeader;
            this.grdLeader.Name = "grdLeader";
            this.grdLeader.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboUser});
            this.grdLeader.Size = new System.Drawing.Size(518, 727);
            this.grdLeader.TabIndex = 20;
            this.grdLeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLeader});
            // 
            // grvLeader
            // 
            this.grvLeader.ColumnPanelRowHeight = 50;
            this.grvLeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colLeaderID,
            this.colLeaderName,
            this.colLeaderCode,
            this.colProjectType});
            this.grvLeader.GridControl = this.grdLeader;
            this.grvLeader.GroupCount = 1;
            this.grvLeader.Name = "grvLeader";
            this.grvLeader.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvLeader.OptionsBehavior.Editable = false;
            this.grvLeader.OptionsBehavior.ReadOnly = true;
            this.grvLeader.OptionsCustomization.AllowColumnMoving = false;
            this.grvLeader.OptionsCustomization.AllowFilter = false;
            this.grvLeader.OptionsCustomization.AllowSort = false;
            this.grvLeader.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.grvLeader.OptionsSelection.MultiSelect = true;
            this.grvLeader.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvLeader.OptionsView.ShowFooter = true;
            this.grvLeader.OptionsView.ShowGroupPanel = false;
            this.grvLeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colProjectType, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colLeaderID
            // 
            this.colLeaderID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colLeaderID.AppearanceHeader.Options.UseBackColor = true;
            this.colLeaderID.AppearanceHeader.Options.UseFont = true;
            this.colLeaderID.AppearanceHeader.Options.UseForeColor = true;
            this.colLeaderID.AppearanceHeader.Options.UseTextOptions = true;
            this.colLeaderID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLeaderID.Caption = "ID";
            this.colLeaderID.FieldName = "ID";
            this.colLeaderID.Name = "colLeaderID";
            // 
            // colLeaderName
            // 
            this.colLeaderName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colLeaderName.AppearanceHeader.Options.UseBackColor = true;
            this.colLeaderName.AppearanceHeader.Options.UseFont = true;
            this.colLeaderName.AppearanceHeader.Options.UseForeColor = true;
            this.colLeaderName.AppearanceHeader.Options.UseTextOptions = true;
            this.colLeaderName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLeaderName.Caption = "Tên leader";
            this.colLeaderName.FieldName = "FullName";
            this.colLeaderName.Name = "colLeaderName";
            this.colLeaderName.Visible = true;
            this.colLeaderName.VisibleIndex = 2;
            this.colLeaderName.Width = 459;
            // 
            // colLeaderCode
            // 
            this.colLeaderCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.colLeaderCode.AppearanceHeader.Options.UseFont = true;
            this.colLeaderCode.AppearanceHeader.Options.UseForeColor = true;
            this.colLeaderCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colLeaderCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLeaderCode.Caption = "Mã leader";
            this.colLeaderCode.FieldName = "Code";
            this.colLeaderCode.Name = "colLeaderCode";
            this.colLeaderCode.Visible = true;
            this.colLeaderCode.VisibleIndex = 1;
            this.colLeaderCode.Width = 211;
            // 
            // colProjectType
            // 
            this.colProjectType.Caption = "Kiểu dự án";
            this.colProjectType.FieldName = "ProjectTypeName";
            this.colProjectType.FieldNameSortGroup = "ProjectTypeID";
            this.colProjectType.Name = "colProjectType";
            this.colProjectType.Visible = true;
            this.colProjectType.VisibleIndex = 2;
            // 
            // cboUser
            // 
            this.cboUser.AutoHeight = false;
            this.cboUser.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUser.Name = "cboUser";
            this.cboUser.NullText = "";
            this.cboUser.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Họ tên";
            this.gridColumn2.FieldName = "FullName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.stackPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 46);
            this.panel1.TabIndex = 19;
            this.panel1.Visible = false;
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.btnDeleteLeader);
            this.stackPanel1.Controls.Add(this.btnAddLeader);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            this.stackPanel1.Location = new System.Drawing.Point(258, 0);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(260, 46);
            this.stackPanel1.TabIndex = 0;
            // 
            // btnDeleteLeader
            // 
            this.btnDeleteLeader.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDeleteLeader.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnDeleteLeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteLeader.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteLeader.Image = global::Forms.Properties.Resources.icons8_close_16;
            this.btnDeleteLeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteLeader.Location = new System.Drawing.Point(135, 5);
            this.btnDeleteLeader.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnDeleteLeader.Name = "btnDeleteLeader";
            this.btnDeleteLeader.Padding = new System.Windows.Forms.Padding(2);
            this.btnDeleteLeader.Size = new System.Drawing.Size(120, 36);
            this.btnDeleteLeader.TabIndex = 3;
            this.btnDeleteLeader.Tag = "frmStaffManager_Update";
            this.btnDeleteLeader.Text = "Xóa Leader";
            this.btnDeleteLeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteLeader.UseVisualStyleBackColor = false;
            this.btnDeleteLeader.Click += new System.EventHandler(this.btnDeleteLeader_Click);
            // 
            // btnAddLeader
            // 
            this.btnAddLeader.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddLeader.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnAddLeader.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnAddLeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddLeader.Image = global::Forms.Properties.Resources.icons8_plus_math_16;
            this.btnAddLeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddLeader.Location = new System.Drawing.Point(10, 5);
            this.btnAddLeader.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddLeader.Name = "btnAddLeader";
            this.btnAddLeader.Padding = new System.Windows.Forms.Padding(2);
            this.btnAddLeader.Size = new System.Drawing.Size(120, 36);
            this.btnAddLeader.TabIndex = 2;
            this.btnAddLeader.Tag = "frmStaffManager_Update";
            this.btnAddLeader.Text = "Thêm Leader";
            this.btnAddLeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddLeader.UseVisualStyleBackColor = false;
            this.btnAddLeader.Click += new System.EventHandler(this.btnAddLeader_Click);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.btnRefresh,
            this.btnAddLeaders,
            this.btnDeleteLeaders});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 25;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bar2.BarAppearance.Hovered.Options.UseFont = true;
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddLeaders),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteLeaders)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Id = 14;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "REFRESH";
            this.btnRefresh.Id = 19;
            this.btnRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRefresh.ImageOptions.SvgImage")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // btnAddLeaders
            // 
            this.btnAddLeaders.Caption = "THÊM LEADER";
            this.btnAddLeaders.Id = 20;
            this.btnAddLeaders.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddLeaders.ImageOptions.Image")));
            this.btnAddLeaders.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddLeaders.ImageOptions.LargeImage")));
            this.btnAddLeaders.Name = "btnAddLeaders";
            this.btnAddLeaders.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddLeaders_ItemClick);
            // 
            // btnDeleteLeaders
            // 
            this.btnDeleteLeaders.Caption = "XÓA LEADER";
            this.btnDeleteLeaders.Id = 21;
            this.btnDeleteLeaders.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteLeaders.ImageOptions.Image")));
            this.btnDeleteLeaders.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteLeaders.ImageOptions.LargeImage")));
            this.btnDeleteLeaders.Name = "btnDeleteLeaders";
            this.btnDeleteLeaders.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteLeaders_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1308, 56);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 783);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1308, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 56);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 727);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1308, 56);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 727);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // frmLeaderProjectType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 783);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmLeaderProjectType";
            this.Text = "LEADER DỰ ÁN";
            this.Load += new System.EventHandler(this.frmLeaderProjectType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlProjectType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdLeader;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLeader;
        private DevExpress.XtraGrid.Columns.GridColumn colLeaderID;
        private DevExpress.XtraGrid.Columns.GridColumn colLeaderName;
        private DevExpress.XtraGrid.Columns.GridColumn colLeaderCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboUser;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraTreeList.TreeList tlProjectType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectTypeID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectType;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarLargeButtonItem btnRefresh;
        private DevExpress.XtraBars.BarLargeButtonItem btnAddLeaders;
        private DevExpress.XtraBars.BarLargeButtonItem btnDeleteLeaders;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Button btnDeleteLeader;
        private System.Windows.Forms.Button btnAddLeader;
    }
}