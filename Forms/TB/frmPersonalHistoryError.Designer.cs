namespace BMS
{
    partial class frmHistoryError
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
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSerial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberBorrow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberLose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateLose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPeople = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateReturmExpected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoteStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdminConfirm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkAdminConfirm = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSerialNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescriptionError = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.lblListError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdminConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkAdminConfirm,
            this.repositoryItemButtonEdit1,
            this.repositoryItemMemoEdit2});
            this.grdData.Size = new System.Drawing.Size(922, 439);
            this.grdData.TabIndex = 55;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvData.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.grvData.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvData.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvData.ColumnPanelRowHeight = 32;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colProductName,
            this.colProductCode,
            this.colSerial,
            this.colMaker,
            this.colNumberBorrow,
            this.colNumberLose,
            this.colAddress,
            this.colDate1,
            this.colDateLose,
            this.colPeople,
            this.colDateReturmExpected,
            this.colDate2,
            this.colProject,
            this.colNoteStatus,
            this.colCountDate,
            this.colAdminConfirm,
            this.colSerialNumber,
            this.colPartNumber,
            this.colDescriptionError});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.HorzScrollStep = 5;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsCustomization.AllowRowSizing = true;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsFind.ShowCloseButton = false;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 15;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.Width = 56;
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProductName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductName.AppearanceHeader.Options.UseFont = true;
            this.colProductName.AppearanceHeader.Options.UseForeColor = true;
            this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductName.Caption = "Tên";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colProductName.FieldName = "ProductName";
            this.colProductName.MinWidth = 15;
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 0;
            this.colProductName.Width = 127;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colProductCode
            // 
            this.colProductCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProductCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductCode.AppearanceHeader.Options.UseFont = true;
            this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductCode.Caption = "Mã Sản Phẩm";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.MinWidth = 15;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowEdit = false;
            this.colProductCode.OptionsColumn.ReadOnly = true;
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 1;
            this.colProductCode.Width = 118;
            // 
            // colSerial
            // 
            this.colSerial.AppearanceCell.Options.UseTextOptions = true;
            this.colSerial.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSerial.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSerial.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSerial.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSerial.AppearanceHeader.Options.UseFont = true;
            this.colSerial.AppearanceHeader.Options.UseForeColor = true;
            this.colSerial.AppearanceHeader.Options.UseTextOptions = true;
            this.colSerial.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSerial.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSerial.Caption = "Code";
            this.colSerial.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSerial.FieldName = "Serial";
            this.colSerial.MinWidth = 15;
            this.colSerial.Name = "colSerial";
            this.colSerial.OptionsColumn.AllowEdit = false;
            this.colSerial.OptionsColumn.ReadOnly = true;
            this.colSerial.Visible = true;
            this.colSerial.VisibleIndex = 4;
            this.colSerial.Width = 51;
            // 
            // colMaker
            // 
            this.colMaker.AppearanceCell.Options.UseTextOptions = true;
            this.colMaker.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaker.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaker.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaker.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colMaker.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMaker.AppearanceHeader.Options.UseFont = true;
            this.colMaker.AppearanceHeader.Options.UseForeColor = true;
            this.colMaker.Caption = "Hãng";
            this.colMaker.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colMaker.FieldName = "Maker";
            this.colMaker.MinWidth = 15;
            this.colMaker.Name = "colMaker";
            this.colMaker.OptionsColumn.AllowEdit = false;
            this.colMaker.OptionsColumn.ReadOnly = true;
            this.colMaker.Visible = true;
            this.colMaker.VisibleIndex = 5;
            this.colMaker.Width = 43;
            // 
            // colNumberBorrow
            // 
            this.colNumberBorrow.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberBorrow.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberBorrow.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNumberBorrow.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNumberBorrow.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNumberBorrow.AppearanceHeader.Options.UseFont = true;
            this.colNumberBorrow.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberBorrow.Caption = "Số lượng mượn";
            this.colNumberBorrow.FieldName = "NumberBorrow";
            this.colNumberBorrow.MinWidth = 15;
            this.colNumberBorrow.Name = "colNumberBorrow";
            this.colNumberBorrow.OptionsColumn.AllowEdit = false;
            this.colNumberBorrow.OptionsColumn.ReadOnly = true;
            this.colNumberBorrow.Visible = true;
            this.colNumberBorrow.VisibleIndex = 6;
            this.colNumberBorrow.Width = 31;
            // 
            // colNumberLose
            // 
            this.colNumberLose.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberLose.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberLose.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberLose.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNumberLose.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNumberLose.AppearanceHeader.Options.UseFont = true;
            this.colNumberLose.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberLose.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberLose.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberLose.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberLose.Caption = "Số lượng mất";
            this.colNumberLose.FieldName = "NumberLose";
            this.colNumberLose.MinWidth = 15;
            this.colNumberLose.Name = "colNumberLose";
            this.colNumberLose.Width = 56;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceCell.Options.UseTextOptions = true;
            this.colAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAddress.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAddress.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            this.colAddress.AppearanceHeader.Options.UseForeColor = true;
            this.colAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAddress.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAddress.Caption = "Vị trí (Hộp)";
            this.colAddress.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colAddress.FieldName = "AddressBox";
            this.colAddress.MinWidth = 15;
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.OptionsColumn.ReadOnly = true;
            this.colAddress.Visible = true;
            this.colAddress.VisibleIndex = 7;
            this.colAddress.Width = 73;
            // 
            // colDate1
            // 
            this.colDate1.AppearanceCell.Options.UseTextOptions = true;
            this.colDate1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDate1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDate1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDate1.AppearanceHeader.Options.UseFont = true;
            this.colDate1.AppearanceHeader.Options.UseForeColor = true;
            this.colDate1.Caption = "Ngày mượn";
            this.colDate1.FieldName = "DateBorrow";
            this.colDate1.MinWidth = 15;
            this.colDate1.Name = "colDate1";
            this.colDate1.OptionsColumn.AllowEdit = false;
            this.colDate1.OptionsColumn.ReadOnly = true;
            this.colDate1.Visible = true;
            this.colDate1.VisibleIndex = 9;
            this.colDate1.Width = 79;
            // 
            // colDateLose
            // 
            this.colDateLose.AppearanceCell.Options.UseTextOptions = true;
            this.colDateLose.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateLose.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDateLose.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDateLose.AppearanceHeader.Options.UseFont = true;
            this.colDateLose.AppearanceHeader.Options.UseForeColor = true;
            this.colDateLose.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateLose.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateLose.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateLose.Caption = "gridColumn1";
            this.colDateLose.FieldName = "DateLose";
            this.colDateLose.MinWidth = 15;
            this.colDateLose.Name = "colDateLose";
            this.colDateLose.Width = 56;
            // 
            // colPeople
            // 
            this.colPeople.AppearanceCell.Options.UseTextOptions = true;
            this.colPeople.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPeople.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPeople.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPeople.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPeople.AppearanceHeader.Options.UseFont = true;
            this.colPeople.AppearanceHeader.Options.UseForeColor = true;
            this.colPeople.Caption = "Người mượn";
            this.colPeople.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colPeople.FieldName = "FullName";
            this.colPeople.MinWidth = 15;
            this.colPeople.Name = "colPeople";
            this.colPeople.OptionsColumn.AllowEdit = false;
            this.colPeople.OptionsColumn.ReadOnly = true;
            this.colPeople.Visible = true;
            this.colPeople.VisibleIndex = 8;
            this.colPeople.Width = 85;
            // 
            // colDateReturmExpected
            // 
            this.colDateReturmExpected.AppearanceCell.Options.UseTextOptions = true;
            this.colDateReturmExpected.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateReturmExpected.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDateReturmExpected.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDateReturmExpected.AppearanceHeader.Options.UseFont = true;
            this.colDateReturmExpected.AppearanceHeader.Options.UseForeColor = true;
            this.colDateReturmExpected.AppearanceHeader.Options.UseTextOptions = true;
            this.colDateReturmExpected.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateReturmExpected.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateReturmExpected.Caption = "Ngày dự kiến trả";
            this.colDateReturmExpected.FieldName = "DateReturnExpected";
            this.colDateReturmExpected.MinWidth = 15;
            this.colDateReturmExpected.Name = "colDateReturmExpected";
            this.colDateReturmExpected.OptionsColumn.AllowEdit = false;
            this.colDateReturmExpected.OptionsColumn.ReadOnly = true;
            this.colDateReturmExpected.Visible = true;
            this.colDateReturmExpected.VisibleIndex = 10;
            this.colDateReturmExpected.Width = 79;
            // 
            // colDate2
            // 
            this.colDate2.AppearanceCell.Options.UseTextOptions = true;
            this.colDate2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDate2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDate2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDate2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDate2.AppearanceHeader.Options.UseFont = true;
            this.colDate2.AppearanceHeader.Options.UseForeColor = true;
            this.colDate2.Caption = "Ngày trả";
            this.colDate2.FieldName = "DateReturn";
            this.colDate2.MinWidth = 15;
            this.colDate2.Name = "colDate2";
            this.colDate2.OptionsColumn.AllowEdit = false;
            this.colDate2.OptionsColumn.ReadOnly = true;
            this.colDate2.Visible = true;
            this.colDate2.VisibleIndex = 11;
            this.colDate2.Width = 79;
            // 
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProject.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProject.AppearanceHeader.Options.UseFont = true;
            this.colProject.AppearanceHeader.Options.UseForeColor = true;
            this.colProject.Caption = "Dự án";
            this.colProject.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colProject.FieldName = "Project";
            this.colProject.MinWidth = 15;
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            this.colProject.OptionsColumn.ReadOnly = true;
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 12;
            this.colProject.Width = 84;
            // 
            // colNoteStatus
            // 
            this.colNoteStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNoteStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNoteStatus.AppearanceHeader.Options.UseFont = true;
            this.colNoteStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colNoteStatus.Caption = "Note";
            this.colNoteStatus.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colNoteStatus.FieldName = "Note";
            this.colNoteStatus.MinWidth = 15;
            this.colNoteStatus.Name = "colNoteStatus";
            this.colNoteStatus.OptionsColumn.AllowEdit = false;
            this.colNoteStatus.OptionsColumn.ReadOnly = true;
            this.colNoteStatus.Visible = true;
            this.colNoteStatus.VisibleIndex = 13;
            this.colNoteStatus.Width = 70;
            // 
            // colCountDate
            // 
            this.colCountDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCountDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCountDate.Caption = "gridColumn1";
            this.colCountDate.FieldName = "CountDate";
            this.colCountDate.MinWidth = 15;
            this.colCountDate.Name = "colCountDate";
            this.colCountDate.Width = 56;
            // 
            // colAdminConfirm
            // 
            this.colAdminConfirm.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAdminConfirm.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAdminConfirm.AppearanceHeader.Options.UseFont = true;
            this.colAdminConfirm.AppearanceHeader.Options.UseForeColor = true;
            this.colAdminConfirm.Caption = "Duyệt";
            this.colAdminConfirm.ColumnEdit = this.chkAdminConfirm;
            this.colAdminConfirm.FieldName = "AdminConfirm";
            this.colAdminConfirm.MinWidth = 15;
            this.colAdminConfirm.Name = "colAdminConfirm";
            this.colAdminConfirm.OptionsColumn.AllowEdit = false;
            this.colAdminConfirm.Visible = true;
            this.colAdminConfirm.VisibleIndex = 14;
            this.colAdminConfirm.Width = 35;
            // 
            // chkAdminConfirm
            // 
            this.chkAdminConfirm.AutoHeight = false;
            this.chkAdminConfirm.Caption = "Check";
            this.chkAdminConfirm.Name = "chkAdminConfirm";
            this.chkAdminConfirm.Tag = "chkAdminConfirm";
            // 
            // colSerialNumber
            // 
            this.colSerialNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSerialNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSerialNumber.AppearanceHeader.Options.UseFont = true;
            this.colSerialNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colSerialNumber.Caption = "Serial";
            this.colSerialNumber.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSerialNumber.FieldName = "SerialNumber";
            this.colSerialNumber.MinWidth = 15;
            this.colSerialNumber.Name = "colSerialNumber";
            this.colSerialNumber.Visible = true;
            this.colSerialNumber.VisibleIndex = 2;
            this.colSerialNumber.Width = 64;
            // 
            // colPartNumber
            // 
            this.colPartNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPartNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPartNumber.AppearanceHeader.Options.UseFont = true;
            this.colPartNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colPartNumber.Caption = "Part Number";
            this.colPartNumber.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colPartNumber.FieldName = "PartNumber";
            this.colPartNumber.MinWidth = 15;
            this.colPartNumber.Name = "colPartNumber";
            this.colPartNumber.Visible = true;
            this.colPartNumber.VisibleIndex = 3;
            this.colPartNumber.Width = 47;
            // 
            // colDescriptionError
            // 
            this.colDescriptionError.AppearanceCell.Options.UseTextOptions = true;
            this.colDescriptionError.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescriptionError.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDescriptionError.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDescriptionError.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDescriptionError.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colDescriptionError.AppearanceHeader.Options.UseFont = true;
            this.colDescriptionError.AppearanceHeader.Options.UseForeColor = true;
            this.colDescriptionError.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescriptionError.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescriptionError.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDescriptionError.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDescriptionError.Caption = "Mô tả lỗi";
            this.colDescriptionError.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colDescriptionError.FieldName = "DescriptionError";
            this.colDescriptionError.MinWidth = 15;
            this.colDescriptionError.Name = "colDescriptionError";
            this.colDescriptionError.Visible = true;
            this.colDescriptionError.VisibleIndex = 15;
            this.colDescriptionError.Width = 133;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // lblListError
            // 
            this.lblListError.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListError.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListError.Location = new System.Drawing.Point(0, 0);
            this.lblListError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblListError.Name = "lblListError";
            this.lblListError.Size = new System.Drawing.Size(922, 45);
            this.lblListError.TabIndex = 56;
            this.lblListError.Text = "Danh sách lỗi cá nhân";
            this.lblListError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmHistoryError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 439);
            this.Controls.Add(this.lblListError);
            this.Controls.Add(this.grdData);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmHistoryError";
            this.Text = "DANH SÁCH LỖI MƯỢN THIẾT BỊ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHistoryError_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAdminConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSerial;
        private DevExpress.XtraGrid.Columns.GridColumn colMaker;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberBorrow;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberLose;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colDate1;
        private DevExpress.XtraGrid.Columns.GridColumn colDateLose;
        private DevExpress.XtraGrid.Columns.GridColumn colPeople;
        private DevExpress.XtraGrid.Columns.GridColumn colDateReturmExpected;
        private DevExpress.XtraGrid.Columns.GridColumn colDate2;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCountDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAdminConfirm;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkAdminConfirm;
        private DevExpress.XtraGrid.Columns.GridColumn colSerialNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colPartNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDescriptionError;
        private System.Windows.Forms.Label lblListError;
    }
}