
namespace BMS
{
    partial class frmProjectTypeAssign
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
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAddStaff = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteStaff = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTreeID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlProjectType = new DevExpress.XtraTreeList.TreeList();
            this.colIsAllowedToAdd = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdUserTeamLink = new DevExpress.XtraGrid.GridControl();
            this.grvUserTeamLink = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssignID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboUser = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlProjectType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserTeamLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUserTeamLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = false;
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = global::Forms.Properties.Resources.refresh2_16x16;
            this.btnReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 40);
            this.btnReset.Text = "Refresh";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddStaff,
            this.toolStripSeparator2,
            this.btnDeleteStaff,
            this.toolStripSeparator1,
            this.btnReset});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1375, 45);
            this.mnuMenu.TabIndex = 22;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAddStaff
            // 
            this.btnAddStaff.AutoSize = false;
            this.btnAddStaff.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStaff.Image = global::Forms.Properties.Resources.add_16x161;
            this.btnAddStaff.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddStaff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddStaff.Name = "btnAddStaff";
            this.btnAddStaff.Size = new System.Drawing.Size(80, 40);
            this.btnAddStaff.Tag = "frmProjectTypeAssign_AddStaff";
            this.btnAddStaff.Text = "Thêm NV";
            this.btnAddStaff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddStaff.Click += new System.EventHandler(this.btnAddStaff_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnDeleteStaff
            // 
            this.btnDeleteStaff.AutoSize = false;
            this.btnDeleteStaff.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteStaff.Image = global::Forms.Properties.Resources.Trash_16x16;
            this.btnDeleteStaff.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteStaff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteStaff.Name = "btnDeleteStaff";
            this.btnDeleteStaff.Size = new System.Drawing.Size(80, 40);
            this.btnDeleteStaff.Tag = "frmProjectTypeAssign_DeleteStaff";
            this.btnDeleteStaff.Text = "Xóa NV";
            this.btnDeleteStaff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteStaff.Click += new System.EventHandler(this.btnDeleteStaff_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // colName
            // 
            this.colName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceCell.Options.UseFont = true;
            this.colName.Caption = "Tên";
            this.colName.FieldName = "ProjectTypeName";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 529;
            // 
            // colParentID
            // 
            this.colParentID.Caption = "ParentID";
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.Caption = "Mã";
            this.colCode.FieldName = "ProjectTypeCode";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 223;
            // 
            // colTreeID
            // 
            this.colTreeID.Caption = "ID";
            this.colTreeID.FieldName = "ID";
            this.colTreeID.Name = "colTreeID";
            // 
            // tlProjectType
            // 
            this.tlProjectType.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Black;
            this.tlProjectType.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlProjectType.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseFont = true;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.tlProjectType.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.tlProjectType.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tlProjectType.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colTreeID,
            this.colCode,
            this.colParentID,
            this.colName,
            this.colIsAllowedToAdd});
            this.tlProjectType.CustomizationFormBounds = new System.Drawing.Rectangle(583, 272, 252, 266);
            this.tlProjectType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlProjectType.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tlProjectType.Location = new System.Drawing.Point(0, 0);
            this.tlProjectType.Name = "tlProjectType";
            this.tlProjectType.OptionsBehavior.Editable = false;
            this.tlProjectType.OptionsView.ShowSummaryFooter = true;
            this.tlProjectType.Size = new System.Drawing.Size(578, 620);
            this.tlProjectType.TabIndex = 0;
            this.tlProjectType.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeTeam_NodeCellStyle);
            this.tlProjectType.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeTeam_FocusedNodeChanged);
            // 
            // colIsAllowedToAdd
            // 
            this.colIsAllowedToAdd.Caption = "Có thể thêm nv?";
            this.colIsAllowedToAdd.FieldName = "IsAllowedToAdd";
            this.colIsAllowedToAdd.MinWidth = 16;
            this.colIsAllowedToAdd.Name = "colIsAllowedToAdd";
            this.colIsAllowedToAdd.Width = 56;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Họ tên";
            this.gridColumn2.FieldName = "FullName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 45);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tlProjectType);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdUserTeamLink);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1375, 620);
            this.splitContainerControl1.SplitterPosition = 578;
            this.splitContainerControl1.TabIndex = 23;
            // 
            // grdUserTeamLink
            // 
            this.grdUserTeamLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserTeamLink.Location = new System.Drawing.Point(0, 0);
            this.grdUserTeamLink.MainView = this.grvUserTeamLink;
            this.grdUserTeamLink.Name = "grdUserTeamLink";
            this.grdUserTeamLink.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboUser});
            this.grdUserTeamLink.Size = new System.Drawing.Size(787, 620);
            this.grdUserTeamLink.TabIndex = 20;
            this.grdUserTeamLink.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvUserTeamLink});
            this.grdUserTeamLink.Load += new System.EventHandler(this.grdUserTeamLink_Load_1);
            // 
            // grvUserTeamLink
            // 
            this.grvUserTeamLink.ColumnPanelRowHeight = 30;
            this.grvUserTeamLink.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmployeeName,
            this.colEmployeeCode,
            this.colProjectType,
            this.colAssignID});
            this.grvUserTeamLink.GridControl = this.grdUserTeamLink;
            this.grvUserTeamLink.GroupCount = 1;
            this.grvUserTeamLink.Name = "grvUserTeamLink";
            this.grvUserTeamLink.OptionsBehavior.Editable = false;
            this.grvUserTeamLink.OptionsBehavior.ReadOnly = true;
            this.grvUserTeamLink.OptionsCustomization.AllowColumnMoving = false;
            this.grvUserTeamLink.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.grvUserTeamLink.OptionsView.ShowFooter = true;
            this.grvUserTeamLink.OptionsView.ShowGroupPanel = false;
            this.grvUserTeamLink.OptionsView.ShowIndicator = false;
            this.grvUserTeamLink.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colProjectType, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeName.AppearanceCell.Options.UseFont = true;
            this.colEmployeeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeName.AppearanceHeader.Options.UseBackColor = true;
            this.colEmployeeName.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeName.AppearanceHeader.Options.UseForeColor = true;
            this.colEmployeeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployeeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeName.Caption = "Tên nhân viên";
            this.colEmployeeName.FieldName = "FullName";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colEmployeeName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEmployeeName.ShowUnboundExpressionMenu = true;
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 1;
            this.colEmployeeName.Width = 459;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeCode.AppearanceCell.Options.UseFont = true;
            this.colEmployeeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmployeeCode.AppearanceHeader.Options.UseFont = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colEmployeeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployeeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployeeCode.Caption = "Mã nhân viên";
            this.colEmployeeCode.FieldName = "Code";
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colEmployeeCode.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colEmployeeCode.ShowUnboundExpressionMenu = true;
            this.colEmployeeCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Code", "{0}")});
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.VisibleIndex = 0;
            this.colEmployeeCode.Width = 211;
            // 
            // colProjectType
            // 
            this.colProjectType.Caption = "Danh mục";
            this.colProjectType.FieldName = "ProjectTypeName";
            this.colProjectType.MinWidth = 19;
            this.colProjectType.Name = "colProjectType";
            this.colProjectType.Visible = true;
            this.colProjectType.VisibleIndex = 2;
            this.colProjectType.Width = 70;
            // 
            // colAssignID
            // 
            this.colAssignID.FieldName = "AssignID";
            this.colAssignID.MinWidth = 19;
            this.colAssignID.Name = "colAssignID";
            this.colAssignID.Width = 70;
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
            // frmProjectTypeAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 665);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmProjectTypeAssign";
            this.Text = "PHÂN CÔNG CÔNG VIỆC";
            this.Load += new System.EventHandler(this.frmteamtrees_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlProjectType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUserTeamLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvUserTeamLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTreeID;
        private DevExpress.XtraTreeList.TreeList tlProjectType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdUserTeamLink;
        private DevExpress.XtraGrid.Views.Grid.GridView grvUserTeamLink;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboUser;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectType;
        private DevExpress.XtraGrid.Columns.GridColumn colAssignID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsAllowedToAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAddStaff;
        private System.Windows.Forms.ToolStripButton btnDeleteStaff;
    }
}