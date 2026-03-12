
namespace BMS
{
    partial class frmProjectIsBehindSchedule
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpStartDate = new DevExpress.XtraEditors.DateEdit();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullNameTech = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstimatedStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEstimatedEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullNamePM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboProjectStatus = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatusID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtFind);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1302, 57);
            this.panel1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(443, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 20);
            this.label7.TabIndex = 71;
            this.label7.Text = "Từ khóa";
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.Location = new System.Drawing.Point(515, 11);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(257, 27);
            this.txtFind.TabIndex = 5;
            // 
            // btnLoad
            // 
            this.btnLoad.AutoSize = true;
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(778, 9);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(150, 30);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(222, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = null;
            this.dtpEndDate.Location = new System.Drawing.Point(267, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Properties.Appearance.Options.UseFont = true;
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.MaskSettings.Set("mask", "d");
            this.dtpEndDate.Size = new System.Drawing.Size(169, 26);
            this.dtpEndDate.TabIndex = 1;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.EditValue = null;
            this.dtpStartDate.Location = new System.Drawing.Point(49, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Properties.Appearance.Options.UseFont = true;
            this.dtpStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartDate.Properties.MaskSettings.Set("mask", "d");
            this.dtpStartDate.Size = new System.Drawing.Size(167, 26);
            this.dtpStartDate.TabIndex = 0;
            // 
            // grdData
            // 
            this.grdData.AccessibleDescription = "";
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 57);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cboProjectStatus,
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1302, 553);
            this.grdData.TabIndex = 30;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.AutoFillColumn = this.colCustomerID;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCustomerID,
            this.colProjectCode,
            this.colProjectName,
            this.colProjectStatus,
            this.colUserID,
            this.colFullNameTech,
            this.colNote,
            this.colCreatedDate,
            this.colCreatedBy,
            this.colUpdateBy,
            this.colEstimatedStartDate,
            this.colEstimatedEndDate,
            this.colActualStartDate,
            this.colActualEndDate,
            this.colEU,
            this.colFullNamePM,
            this.colCurrentState});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsCustomization.AllowRowSizing = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colCustomerID
            // 
            this.colCustomerID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colCustomerID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCustomerID.FieldName = "CustomerName";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.OptionsColumn.AllowEdit = false;
            this.colCustomerID.OptionsColumn.ReadOnly = true;
            this.colCustomerID.Visible = true;
            this.colCustomerID.VisibleIndex = 8;
            this.colCustomerID.Width = 235;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
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
            this.colID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colProjectCode
            // 
            this.colProjectCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colProjectCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectCode.FieldName = "ProjectCode";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.OptionsColumn.AllowEdit = false;
            this.colProjectCode.OptionsColumn.ReadOnly = true;
            this.colProjectCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ProjectCode", "{0}")});
            this.colProjectCode.Visible = true;
            this.colProjectCode.VisibleIndex = 2;
            this.colProjectCode.Width = 136;
            // 
            // colProjectName
            // 
            this.colProjectName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colProjectName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectName.FieldName = "ProjectName";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.OptionsColumn.AllowEdit = false;
            this.colProjectName.OptionsColumn.ReadOnly = true;
            this.colProjectName.Visible = true;
            this.colProjectName.VisibleIndex = 3;
            this.colProjectName.Width = 303;
            // 
            // colProjectStatus
            // 
            this.colProjectStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colProjectStatus.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProjectStatus.FieldName = "ProjectStatusName";
            this.colProjectStatus.Name = "colProjectStatus";
            this.colProjectStatus.Visible = true;
            this.colProjectStatus.VisibleIndex = 0;
            this.colProjectStatus.Width = 97;
            // 
            // colUserID
            // 
            this.colUserID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colUserID.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colUserID.FieldName = "FullNameSale";
            this.colUserID.Name = "colUserID";
            this.colUserID.OptionsColumn.AllowEdit = false;
            this.colUserID.OptionsColumn.ReadOnly = true;
            this.colUserID.Visible = true;
            this.colUserID.VisibleIndex = 4;
            this.colUserID.Width = 140;
            // 
            // colFullNameTech
            // 
            this.colFullNameTech.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colFullNameTech.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullNameTech.FieldName = "FullNameTech";
            this.colFullNameTech.Name = "colFullNameTech";
            this.colFullNameTech.OptionsColumn.AllowEdit = false;
            this.colFullNameTech.OptionsColumn.ReadOnly = true;
            this.colFullNameTech.Visible = true;
            this.colFullNameTech.VisibleIndex = 5;
            this.colFullNameTech.Width = 140;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Mô tả dự án";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.OptionsColumn.ReadOnly = true;
            this.colNote.Width = 163;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colCreatedDate.ColumnEdit = this.repositoryItemMemoEdit1;
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
            this.colCreatedBy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colCreatedBy.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCreatedBy.FieldName = "CreatedBy";
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.OptionsColumn.AllowEdit = false;
            this.colCreatedBy.OptionsColumn.ReadOnly = true;
            this.colCreatedBy.Width = 112;
            // 
            // colUpdateBy
            // 
            this.colUpdateBy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colUpdateBy.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colUpdateBy.FieldName = "UpdatedBy";
            this.colUpdateBy.Name = "colUpdateBy";
            this.colUpdateBy.OptionsColumn.AllowEdit = false;
            this.colUpdateBy.OptionsColumn.ReadOnly = true;
            this.colUpdateBy.Width = 112;
            // 
            // colEstimatedStartDate
            // 
            this.colEstimatedStartDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colEstimatedStartDate.AppearanceCell.Options.UseFont = true;
            this.colEstimatedStartDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEstimatedStartDate.AppearanceHeader.Options.UseFont = true;
            this.colEstimatedStartDate.AppearanceHeader.Options.UseForeColor = true;
            this.colEstimatedStartDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colEstimatedStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEstimatedStartDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEstimatedStartDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEstimatedStartDate.Caption = "Ngày bắt đầu dự kiến";
            this.colEstimatedStartDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colEstimatedStartDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colEstimatedStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colEstimatedStartDate.FieldName = "EstimatedStartDate";
            this.colEstimatedStartDate.Name = "colEstimatedStartDate";
            this.colEstimatedStartDate.OptionsColumn.AllowEdit = false;
            this.colEstimatedStartDate.OptionsColumn.ReadOnly = true;
            this.colEstimatedStartDate.Visible = true;
            this.colEstimatedStartDate.VisibleIndex = 9;
            this.colEstimatedStartDate.Width = 95;
            // 
            // colEstimatedEndDate
            // 
            this.colEstimatedEndDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colEstimatedEndDate.AppearanceCell.Options.UseFont = true;
            this.colEstimatedEndDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEstimatedEndDate.AppearanceHeader.Options.UseFont = true;
            this.colEstimatedEndDate.AppearanceHeader.Options.UseForeColor = true;
            this.colEstimatedEndDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colEstimatedEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEstimatedEndDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEstimatedEndDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEstimatedEndDate.Caption = "Ngày kết thúc dự kiến";
            this.colEstimatedEndDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colEstimatedEndDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colEstimatedEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colEstimatedEndDate.FieldName = "EstimatedEndDate";
            this.colEstimatedEndDate.Name = "colEstimatedEndDate";
            this.colEstimatedEndDate.OptionsColumn.AllowEdit = false;
            this.colEstimatedEndDate.OptionsColumn.ReadOnly = true;
            this.colEstimatedEndDate.Visible = true;
            this.colEstimatedEndDate.VisibleIndex = 10;
            this.colEstimatedEndDate.Width = 95;
            // 
            // colActualStartDate
            // 
            this.colActualStartDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colActualStartDate.AppearanceCell.Options.UseFont = true;
            this.colActualStartDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colActualStartDate.AppearanceHeader.Options.UseFont = true;
            this.colActualStartDate.AppearanceHeader.Options.UseForeColor = true;
            this.colActualStartDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colActualStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActualStartDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colActualStartDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colActualStartDate.Caption = "Ngày bắt đầu thực tế";
            this.colActualStartDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colActualStartDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colActualStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colActualStartDate.FieldName = "ActualStartDate";
            this.colActualStartDate.Name = "colActualStartDate";
            this.colActualStartDate.OptionsColumn.AllowEdit = false;
            this.colActualStartDate.OptionsColumn.ReadOnly = true;
            this.colActualStartDate.Visible = true;
            this.colActualStartDate.VisibleIndex = 11;
            this.colActualStartDate.Width = 95;
            // 
            // colActualEndDate
            // 
            this.colActualEndDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colActualEndDate.AppearanceCell.Options.UseFont = true;
            this.colActualEndDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colActualEndDate.AppearanceHeader.Options.UseFont = true;
            this.colActualEndDate.AppearanceHeader.Options.UseForeColor = true;
            this.colActualEndDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colActualEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colActualEndDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colActualEndDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colActualEndDate.Caption = "Ngày kết thúc thực tế";
            this.colActualEndDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colActualEndDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colActualEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colActualEndDate.FieldName = "ActualEndDate";
            this.colActualEndDate.Name = "colActualEndDate";
            this.colActualEndDate.OptionsColumn.AllowEdit = false;
            this.colActualEndDate.OptionsColumn.ReadOnly = true;
            this.colActualEndDate.Visible = true;
            this.colActualEndDate.VisibleIndex = 12;
            this.colActualEndDate.Width = 95;
            // 
            // colEU
            // 
            this.colEU.AppearanceCell.Options.UseTextOptions = true;
            this.colEU.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEU.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEU.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colEU.AppearanceHeader.Options.UseFont = true;
            this.colEU.AppearanceHeader.Options.UseForeColor = true;
            this.colEU.AppearanceHeader.Options.UseTextOptions = true;
            this.colEU.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEU.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colEU.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colEU.FieldName = "EU";
            this.colEU.Name = "colEU";
            this.colEU.OptionsColumn.AllowEdit = false;
            this.colEU.OptionsColumn.ReadOnly = true;
            this.colEU.Width = 103;
            // 
            // colFullNamePM
            // 
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
            this.colFullNamePM.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colFullNamePM.FieldName = "FullNamePM";
            this.colFullNamePM.Name = "colFullNamePM";
            this.colFullNamePM.OptionsColumn.AllowEdit = false;
            this.colFullNamePM.OptionsColumn.ReadOnly = true;
            this.colFullNamePM.Visible = true;
            this.colFullNamePM.VisibleIndex = 6;
            this.colFullNamePM.Width = 140;
            // 
            // colCurrentState
            // 
            this.colCurrentState.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.colCurrentState.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCurrentState.FieldName = "CurrentState";
            this.colCurrentState.Name = "colCurrentState";
            this.colCurrentState.OptionsColumn.AllowEdit = false;
            this.colCurrentState.OptionsColumn.ReadOnly = true;
            this.colCurrentState.Visible = true;
            this.colCurrentState.VisibleIndex = 7;
            this.colCurrentState.Width = 402;
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
            // frmProjectIsBehindSchedule
            // 
            this.AcceptButton = this.btnLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 610);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Name = "frmProjectIsBehindSchedule";
            this.Text = "DỰ ÁN CHẬM TIẾN ĐỘ";
            this.Load += new System.EventHandler(this.frmProjectIsBehindSchedule_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dtpEndDate;
        private DevExpress.XtraEditors.DateEdit dtpStartDate;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colFullNameTech;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colEstimatedStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEstimatedEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colActualStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colActualEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEU;
        private DevExpress.XtraGrid.Columns.GridColumn colFullNamePM;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentState;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboProjectStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusName;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}