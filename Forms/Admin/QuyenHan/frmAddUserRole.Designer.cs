
namespace Forms.Admin.QuyenHan
{
    partial class frmAddUserRole
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
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.grdUser2 = new DevExpress.XtraGrid.GridControl();
            this.grvUser2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFuncName2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRole2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.grdUser1 = new DevExpress.XtraGrid.GridControl();
            this.grvUser1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFuncName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoleName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckbCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelUser2 = new System.Windows.Forms.Label();
            this.labelUser1 = new System.Windows.Forms.Label();
            this.cboUserFrom = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.cboUserTo = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUser2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUser2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUser1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUser1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Size = new System.Drawing.Size(883, 56);
            this.mnuMenu.TabIndex = 0;
            this.mnuMenu.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 53);
            this.btnSave.Text = "CẤT";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdUser2
            // 
            this.grdUser2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdUser2.Location = new System.Drawing.Point(0, 42);
            this.grdUser2.MainView = this.grvUser2;
            this.grdUser2.Name = "grdUser2";
            this.grdUser2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.grdUser2.Size = new System.Drawing.Size(436, 352);
            this.grdUser2.TabIndex = 1;
            this.grdUser2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvUser2});
            // 
            // grvUser2
            // 
            this.grvUser2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFuncName2,
            this.colRole2});
            this.grvUser2.GridControl = this.grdUser2;
            this.grvUser2.GroupCount = 1;
            this.grvUser2.Name = "grvUser2";
            this.grvUser2.OptionsView.ShowGroupPanel = false;
            this.grvUser2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colRole2, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colFuncName2, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colFuncName2
            // 
            this.colFuncName2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFuncName2.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colFuncName2.AppearanceHeader.Options.UseFont = true;
            this.colFuncName2.AppearanceHeader.Options.UseForeColor = true;
            this.colFuncName2.AppearanceHeader.Options.UseTextOptions = true;
            this.colFuncName2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFuncName2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFuncName2.Caption = "Chức năng";
            this.colFuncName2.FieldName = "Functions";
            this.colFuncName2.Name = "colFuncName2";
            this.colFuncName2.OptionsColumn.AllowEdit = false;
            this.colFuncName2.Visible = true;
            this.colFuncName2.VisibleIndex = 0;
            // 
            // colRole2
            // 
            this.colRole2.Caption = "Nhóm quyền";
            this.colRole2.FieldName = "Name";
            this.colRole2.Name = "colRole2";
            this.colRole2.OptionsColumn.AllowEdit = false;
            this.colRole2.Visible = true;
            this.colRole2.VisibleIndex = 1;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // grdUser1
            // 
            this.grdUser1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdUser1.Location = new System.Drawing.Point(0, 42);
            this.grdUser1.MainView = this.grvUser1;
            this.grdUser1.Name = "grdUser1";
            this.grdUser1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ckbCheck});
            this.grdUser1.Size = new System.Drawing.Size(437, 352);
            this.grdUser1.TabIndex = 0;
            this.grdUser1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvUser1,
            this.gridView2});
            // 
            // grvUser1
            // 
            this.grvUser1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFuncName1,
            this.colRoleName1,
            this.colRoleId});
            this.grvUser1.GridControl = this.grdUser1;
            this.grvUser1.GroupCount = 1;
            this.grvUser1.Name = "grvUser1";
            this.grvUser1.OptionsSelection.MultiSelect = true;
            this.grvUser1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvUser1.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            this.grvUser1.OptionsView.ShowGroupPanel = false;
            this.grvUser1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colRoleName1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colFuncName1
            // 
            this.colFuncName1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFuncName1.AppearanceHeader.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.colFuncName1.AppearanceHeader.Options.UseFont = true;
            this.colFuncName1.AppearanceHeader.Options.UseForeColor = true;
            this.colFuncName1.AppearanceHeader.Options.UseTextOptions = true;
            this.colFuncName1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFuncName1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFuncName1.Caption = "Chức năng";
            this.colFuncName1.FieldName = "Functions";
            this.colFuncName1.Name = "colFuncName1";
            this.colFuncName1.OptionsColumn.AllowEdit = false;
            this.colFuncName1.Visible = true;
            this.colFuncName1.VisibleIndex = 1;
            // 
            // colRoleName1
            // 
            this.colRoleName1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRoleName1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRoleName1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRoleName1.Caption = "Nhóm quyền";
            this.colRoleName1.FieldName = "Name";
            this.colRoleName1.Name = "colRoleName1";
            this.colRoleName1.OptionsColumn.AllowEdit = false;
            this.colRoleName1.Visible = true;
            this.colRoleName1.VisibleIndex = 2;
            // 
            // colRoleId
            // 
            this.colRoleId.Caption = "ID";
            this.colRoleId.FieldName = "RoleID";
            this.colRoleId.Name = "colRoleId";
            this.colRoleId.OptionsColumn.AllowEdit = false;
            // 
            // ckbCheck
            // 
            this.ckbCheck.AutoHeight = false;
            this.ckbCheck.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.CheckBox;
            this.ckbCheck.Name = "ckbCheck";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdUser1;
            this.gridView2.Name = "gridView2";
            // 
            // labelUser2
            // 
            this.labelUser2.AutoSize = true;
            this.labelUser2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUser2.Location = new System.Drawing.Point(10, 10);
            this.labelUser2.Name = "labelUser2";
            this.labelUser2.Size = new System.Drawing.Size(98, 16);
            this.labelUser2.TabIndex = 1;
            this.labelUser2.Text = "Đến người dùng";
            // 
            // labelUser1
            // 
            this.labelUser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUser1.AutoSize = true;
            this.labelUser1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUser1.Location = new System.Drawing.Point(3, 10);
            this.labelUser1.Name = "labelUser1";
            this.labelUser1.Size = new System.Drawing.Size(92, 16);
            this.labelUser1.TabIndex = 0;
            this.labelUser1.Text = "Từ Người dùng";
            // 
            // cboUserFrom
            // 
            this.cboUserFrom.Location = new System.Drawing.Point(101, 5);
            this.cboUserFrom.Name = "cboUserFrom";
            this.cboUserFrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboUserFrom.Properties.Appearance.Options.UseFont = true;
            this.cboUserFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserFrom.Properties.NullText = "";
            this.cboUserFrom.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboUserFrom.Size = new System.Drawing.Size(336, 26);
            this.cboUserFrom.TabIndex = 16;
            this.cboUserFrom.EditValueChanged += new System.EventHandler(this.cboUser1_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.searchLookUpEdit1View.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.Row.Options.UseTextOptions = true;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.searchLookUpEdit1View.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 40;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName1,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn6});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.GroupCount = 1;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.AutoExpandAllGroups = true;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.OptionsView.ShowIndicator = false;
            this.searchLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colName1
            // 
            this.colName1.Caption = "Tên nhân viên";
            this.colName1.FieldName = "FullName";
            this.colName1.Name = "colName1";
            this.colName1.Visible = true;
            this.colName1.VisibleIndex = 1;
            this.colName1.Width = 1066;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã nhân viên";
            this.gridColumn1.FieldName = "Code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 442;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Phòng ban";
            this.gridColumn2.FieldName = "DepartmentName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 261);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(883, 189);
            this.splitContainer1.SplitterDistance = 436;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 56);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grdUser1);
            this.splitContainerControl1.Panel1.Controls.Add(this.cboUserFrom);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelUser1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdUser2);
            this.splitContainerControl1.Panel2.Controls.Add(this.cboUserTo);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelUser2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(883, 394);
            this.splitContainerControl1.SplitterPosition = 437;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // cboUserTo
            // 
            this.cboUserTo.Location = new System.Drawing.Point(114, 5);
            this.cboUserTo.Name = "cboUserTo";
            this.cboUserTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboUserTo.Properties.Appearance.Options.UseFont = true;
            this.cboUserTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserTo.Properties.NullText = "";
            this.cboUserTo.Properties.PopupView = this.gridView1;
            this.cboUserTo.Size = new System.Drawing.Size(319, 26);
            this.cboUserTo.TabIndex = 16;
            this.cboUserTo.EditValueChanged += new System.EventHandler(this.cboUser2_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridView1.ColumnPanelRowHeight = 40;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn5, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên nhân viên";
            this.gridColumn3.FieldName = "FullName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 1066;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Mã nhân viên";
            this.gridColumn4.FieldName = "Code";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 442;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Phòng ban";
            this.gridColumn5.FieldName = "DepartmentName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "gridColumn6";
            this.gridColumn6.FieldName = "UserID";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // frmAddUserRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 450);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmAddUserRole";
            this.Text = "COPY QUYỀN";
            this.Load += new System.EventHandler(this.frmAddUserRole_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUser2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUser2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUser1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUser1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            this.splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            this.splitContainerControl1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboUserTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private DevExpress.XtraGrid.GridControl grdUser2;
        private DevExpress.XtraGrid.Views.Grid.GridView grvUser2;
        private DevExpress.XtraGrid.Columns.GridColumn colFuncName2;
        private DevExpress.XtraGrid.Columns.GridColumn colRole2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.GridControl grdUser1;
        private DevExpress.XtraGrid.Views.Grid.GridView grvUser1;
        private DevExpress.XtraGrid.Columns.GridColumn colFuncName1;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleName1;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleId;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbCheck;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Label labelUser2;
        private System.Windows.Forms.Label labelUser1;
        public DevExpress.XtraEditors.SearchLookUpEdit cboUserFrom;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colName1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        public DevExpress.XtraEditors.SearchLookUpEdit cboUserTo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}