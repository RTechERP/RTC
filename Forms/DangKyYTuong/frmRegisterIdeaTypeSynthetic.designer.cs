
namespace BMS
{
    partial class frmRegisterIdeaTypeSynthetic
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
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCatalogType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit5});
            this.grdData.Size = new System.Drawing.Size(1070, 436);
            this.grdData.TabIndex = 57;
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
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colTT,
            this.colCatalogType,
            this.colNameDepartment,
            this.colCode,
            this.colName,
            this.colDepartmentSTT});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsFind.ShowFindButton = false;
            this.grvData.OptionsFind.ShowSearchNavButtons = false;
            this.grvData.OptionsPrint.PrintHeader = false;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colNameDepartment, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.Caption = "gridColumn1";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 37;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.Width = 70;
            // 
            // colTT
            // 
            this.colTT.AppearanceCell.Options.UseTextOptions = true;
            this.colTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTT.Caption = "STT";
            this.colTT.FieldName = "STT";
            this.colTT.MinWidth = 15;
            this.colTT.Name = "colTT";
            this.colTT.OptionsColumn.AllowEdit = false;
            this.colTT.Visible = true;
            this.colTT.VisibleIndex = 0;
            this.colTT.Width = 145;
            // 
            // colCatalogType
            // 
            this.colCatalogType.Caption = "Loại danh mục";
            this.colCatalogType.FieldName = "CatalogTypeText";
            this.colCatalogType.MinWidth = 15;
            this.colCatalogType.Name = "colCatalogType";
            this.colCatalogType.OptionsColumn.AllowEdit = false;
            this.colCatalogType.Visible = true;
            this.colCatalogType.VisibleIndex = 1;
            this.colCatalogType.Width = 140;
            // 
            // colNameDepartment
            // 
            this.colNameDepartment.Caption = "Phòng ban";
            this.colNameDepartment.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colNameDepartment.FieldName = "NameDepartment";
            this.colNameDepartment.FieldNameSortGroup = "DepartmentSTT";
            this.colNameDepartment.MinWidth = 15;
            this.colNameDepartment.Name = "colNameDepartment";
            this.colNameDepartment.OptionsColumn.AllowEdit = false;
            this.colNameDepartment.Visible = true;
            this.colNameDepartment.VisibleIndex = 3;
            this.colNameDepartment.Width = 274;
            // 
            // repositoryItemMemoEdit5
            // 
            this.repositoryItemMemoEdit5.Name = "repositoryItemMemoEdit5";
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã đề tài";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 2;
            this.colCode.Width = 361;
            // 
            // colName
            // 
            this.colName.Caption = "Tên đề tài";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 3;
            this.colName.Width = 399;
            // 
            // colDepartmentSTT
            // 
            this.colDepartmentSTT.FieldName = "DepartmentSTT";
            this.colDepartmentSTT.Name = "colDepartmentSTT";
            // 
            // frmRegisterIdeaTypeSynthetic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 436);
            this.Controls.Add(this.grdData);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmRegisterIdeaTypeSynthetic";
            this.Text = "LOẠI HÌNH ĐỀ TÀI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRegisterIdeaTypeSynthetic_FormClosed);
            this.Load += new System.EventHandler(this.frmRegisterIdeaTypeSynthetic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colTT;
        private DevExpress.XtraGrid.Columns.GridColumn colCatalogType;
        private DevExpress.XtraGrid.Columns.GridColumn colNameDepartment;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentSTT;
    }
}