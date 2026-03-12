
namespace BMS
{
    partial class frmPersonalSynthetic
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
            this.colHangMuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypetext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colValueText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHRApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTBPApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHRCancel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTBPCancel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueTextReal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMonth = new System.Windows.Forms.NumericUpDown();
            this.lbEmployee = new System.Windows.Forms.Label();
            this.grdAttendance = new DevExpress.XtraGrid.GridControl();
            this.grvAttendance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDChamCongMoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coToChuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAttendanceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsEarly = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOvetime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBussiness = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoFingerprint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnLeave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWFH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLunch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsLateRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsEarlyRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHolidayDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1374, 715);
            this.grdData.TabIndex = 1;
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
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHangMuc,
            this.colTypetext,
            this.colValue,
            this.colUnit,
            this.colNote,
            this.colValueText,
            this.colHRApproved,
            this.colTBPApproved,
            this.colHRCancel,
            this.colTBPCancel,
            this.colValueTextReal});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", null, "({0:0.##})")});
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.RowHeight = 30;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colTypetext, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colHangMuc
            // 
            this.colHangMuc.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colHangMuc.AppearanceCell.Options.UseFont = true;
            this.colHangMuc.AppearanceCell.Options.UseTextOptions = true;
            this.colHangMuc.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHangMuc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHangMuc.Caption = "Hạng mục";
            this.colHangMuc.FieldName = "HangMuc";
            this.colHangMuc.Name = "colHangMuc";
            this.colHangMuc.OptionsColumn.AllowEdit = false;
            this.colHangMuc.OptionsColumn.AllowMove = false;
            this.colHangMuc.OptionsColumn.AllowSize = false;
            this.colHangMuc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colHangMuc.OptionsFilter.AllowAutoFilter = false;
            this.colHangMuc.OptionsFilter.AllowFilter = false;
            this.colHangMuc.Visible = true;
            this.colHangMuc.VisibleIndex = 0;
            this.colHangMuc.Width = 266;
            // 
            // colTypetext
            // 
            this.colTypetext.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colTypetext.AppearanceCell.Options.UseFont = true;
            this.colTypetext.AppearanceCell.Options.UseTextOptions = true;
            this.colTypetext.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTypetext.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTypetext.Caption = "Loại";
            this.colTypetext.FieldName = "Typetext";
            this.colTypetext.Name = "colTypetext";
            this.colTypetext.OptionsColumn.AllowEdit = false;
            this.colTypetext.OptionsColumn.AllowMove = false;
            this.colTypetext.OptionsColumn.AllowSize = false;
            this.colTypetext.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTypetext.OptionsFilter.AllowAutoFilter = false;
            this.colTypetext.OptionsFilter.AllowFilter = false;
            this.colTypetext.Visible = true;
            this.colTypetext.VisibleIndex = 1;
            // 
            // colValue
            // 
            this.colValue.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colValue.AppearanceCell.Options.UseFont = true;
            this.colValue.AppearanceCell.Options.UseTextOptions = true;
            this.colValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colValue.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colValue.Caption = "Giá trị";
            this.colValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colValue.FieldName = "Value";
            this.colValue.MaxWidth = 150;
            this.colValue.Name = "colValue";
            this.colValue.OptionsColumn.AllowEdit = false;
            this.colValue.OptionsColumn.AllowMove = false;
            this.colValue.OptionsColumn.AllowSize = false;
            this.colValue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colValue.OptionsFilter.AllowAutoFilter = false;
            this.colValue.OptionsFilter.AllowFilter = false;
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colUnit.AppearanceCell.Options.UseFont = true;
            this.colUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colUnit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUnit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUnit.Caption = "Đơn vị";
            this.colUnit.FieldName = "Unit";
            this.colUnit.MaxWidth = 150;
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.AllowMove = false;
            this.colUnit.OptionsColumn.AllowSize = false;
            this.colUnit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colUnit.OptionsFilter.AllowAutoFilter = false;
            this.colUnit.OptionsFilter.AllowFilter = false;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 1;
            this.colUnit.Width = 68;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.OptionsColumn.AllowMove = false;
            this.colNote.OptionsColumn.AllowSize = false;
            this.colNote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNote.OptionsFilter.AllowAutoFilter = false;
            this.colNote.OptionsFilter.AllowFilter = false;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 8;
            this.colNote.Width = 453;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colValueText
            // 
            this.colValueText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colValueText.AppearanceCell.Options.UseFont = true;
            this.colValueText.AppearanceCell.Options.UseTextOptions = true;
            this.colValueText.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colValueText.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colValueText.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colValueText.Caption = "Giá trị (Đăng ký)";
            this.colValueText.FieldName = "ValueText";
            this.colValueText.Name = "colValueText";
            this.colValueText.ShowUnboundExpressionMenu = true;
            this.colValueText.UnboundExpression = "Iif([Value] = 0, \'\', ToStr([Value]))";
            this.colValueText.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colValueText.Visible = true;
            this.colValueText.VisibleIndex = 2;
            this.colValueText.Width = 83;
            // 
            // colHRApproved
            // 
            this.colHRApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colHRApproved.AppearanceCell.Options.UseFont = true;
            this.colHRApproved.AppearanceCell.Options.UseTextOptions = true;
            this.colHRApproved.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colHRApproved.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHRApproved.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHRApproved.Caption = "HR duyệt (lần)";
            this.colHRApproved.DisplayFormat.FormatString = "N0";
            this.colHRApproved.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHRApproved.FieldName = "HRApproved";
            this.colHRApproved.Name = "colHRApproved";
            this.colHRApproved.Visible = true;
            this.colHRApproved.VisibleIndex = 4;
            this.colHRApproved.Width = 80;
            // 
            // colTBPApproved
            // 
            this.colTBPApproved.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colTBPApproved.AppearanceCell.Options.UseFont = true;
            this.colTBPApproved.AppearanceCell.Options.UseTextOptions = true;
            this.colTBPApproved.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTBPApproved.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTBPApproved.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTBPApproved.Caption = "TBP duyệt (lần)";
            this.colTBPApproved.DisplayFormat.FormatString = "N0";
            this.colTBPApproved.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTBPApproved.FieldName = "TBPApproved";
            this.colTBPApproved.Name = "colTBPApproved";
            this.colTBPApproved.Visible = true;
            this.colTBPApproved.VisibleIndex = 5;
            this.colTBPApproved.Width = 93;
            // 
            // colHRCancel
            // 
            this.colHRCancel.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colHRCancel.AppearanceCell.Options.UseFont = true;
            this.colHRCancel.AppearanceCell.Options.UseTextOptions = true;
            this.colHRCancel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colHRCancel.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colHRCancel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colHRCancel.Caption = "HR hủy đăng ký nghỉ (lần)";
            this.colHRCancel.DisplayFormat.FormatString = "N0";
            this.colHRCancel.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHRCancel.FieldName = "HRCancel";
            this.colHRCancel.Name = "colHRCancel";
            this.colHRCancel.Visible = true;
            this.colHRCancel.VisibleIndex = 6;
            this.colHRCancel.Width = 105;
            // 
            // colTBPCancel
            // 
            this.colTBPCancel.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colTBPCancel.AppearanceCell.Options.UseFont = true;
            this.colTBPCancel.AppearanceCell.Options.UseTextOptions = true;
            this.colTBPCancel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTBPCancel.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTBPCancel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTBPCancel.Caption = "TBP hủy đăng ký nghỉ (lần)";
            this.colTBPCancel.DisplayFormat.FormatString = "N0";
            this.colTBPCancel.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTBPCancel.FieldName = "TBPCancel";
            this.colTBPCancel.Name = "colTBPCancel";
            this.colTBPCancel.Visible = true;
            this.colTBPCancel.VisibleIndex = 7;
            this.colTBPCancel.Width = 126;
            // 
            // colValueTextReal
            // 
            this.colValueTextReal.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colValueTextReal.AppearanceCell.Options.UseFont = true;
            this.colValueTextReal.AppearanceCell.Options.UseTextOptions = true;
            this.colValueTextReal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colValueTextReal.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colValueTextReal.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colValueTextReal.Caption = "Giá trị (Đã duyệt)";
            this.colValueTextReal.FieldName = "ValueTextReal";
            this.colValueTextReal.Name = "colValueTextReal";
            this.colValueTextReal.Visible = true;
            this.colValueTextReal.VisibleIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoadData);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtYear);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMonth);
            this.panel1.Controls.Add(this.lbEmployee);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1376, 44);
            this.panel1.TabIndex = 2;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Location = new System.Drawing.Point(221, 13);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadData.TabIndex = 13;
            this.btnLoadData.Text = "Load";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Năm";
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(45, 13);
            this.txtYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(62, 22);
            this.txtYear.TabIndex = 10;
            this.txtYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtYear.ValueChanged += new System.EventHandler(this.txtYear_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tháng";
            // 
            // txtMonth
            // 
            this.txtMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonth.Location = new System.Drawing.Point(166, 13);
            this.txtMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(48, 22);
            this.txtMonth.TabIndex = 11;
            this.txtMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.ValueChanged += new System.EventHandler(this.txtMonth_ValueChanged);
            // 
            // lbEmployee
            // 
            this.lbEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEmployee.AutoSize = true;
            this.lbEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmployee.Location = new System.Drawing.Point(713, 8);
            this.lbEmployee.Name = "lbEmployee";
            this.lbEmployee.Size = new System.Drawing.Size(325, 29);
            this.lbEmployee.TabIndex = 12;
            this.lbEmployee.Text = "Mã nhân viên - Tên nhân viên";
            this.lbEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdAttendance
            // 
            this.grdAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAttendance.Location = new System.Drawing.Point(0, 0);
            this.grdAttendance.MainView = this.grvAttendance;
            this.grdAttendance.Name = "grdAttendance";
            this.grdAttendance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit2,
            this.repositoryItemCheckEdit1});
            this.grdAttendance.Size = new System.Drawing.Size(1374, 715);
            this.grdAttendance.TabIndex = 32;
            this.grdAttendance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvAttendance});
            // 
            // grvAttendance
            // 
            this.grvAttendance.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvAttendance.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvAttendance.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvAttendance.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvAttendance.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvAttendance.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvAttendance.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvAttendance.AutoFillColumn = this.colFullName;
            this.grvAttendance.ColumnPanelRowHeight = 40;
            this.grvAttendance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSTT,
            this.colIDChamCongMoi,
            this.colFullName,
            this.coToChuc,
            this.gridColumn1,
            this.colAttendanceDate,
            this.gridColumn3,
            this.gridColumn4,
            this.colCheckIn,
            this.colCheckOut,
            this.colIsLate,
            this.colIsEarly,
            this.colOvetime,
            this.colBussiness,
            this.colNoFingerprint,
            this.colOnLeave,
            this.colWFH,
            this.colIsLunch,
            this.colIsLateRegister,
            this.colIsEarlyRegister,
            this.colHolidayDay});
            this.grvAttendance.GridControl = this.grdAttendance;
            this.grvAttendance.Name = "grvAttendance";
            this.grvAttendance.OptionsBehavior.Editable = false;
            this.grvAttendance.OptionsBehavior.ReadOnly = true;
            this.grvAttendance.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvAttendance.OptionsView.ShowFooter = true;
            this.grvAttendance.OptionsView.ShowGroupPanel = false;
            this.grvAttendance.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvAttendance_RowCellStyle);
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Tên";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 0;
            this.colFullName.Width = 239;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.Caption = "STT";
            this.colSTT.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.Width = 83;
            // 
            // colIDChamCongMoi
            // 
            this.colIDChamCongMoi.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIDChamCongMoi.AppearanceCell.Options.UseFont = true;
            this.colIDChamCongMoi.AppearanceCell.Options.UseTextOptions = true;
            this.colIDChamCongMoi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDChamCongMoi.Caption = "ID Người";
            this.colIDChamCongMoi.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colIDChamCongMoi.FieldName = "IDChamCongMoi";
            this.colIDChamCongMoi.Name = "colIDChamCongMoi";
            this.colIDChamCongMoi.OptionsColumn.AllowEdit = false;
            this.colIDChamCongMoi.Width = 88;
            // 
            // coToChuc
            // 
            this.coToChuc.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.coToChuc.AppearanceCell.Options.UseFont = true;
            this.coToChuc.AppearanceCell.Options.UseTextOptions = true;
            this.coToChuc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coToChuc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.coToChuc.Caption = "Tổ chức";
            this.coToChuc.ColumnEdit = this.repositoryItemMemoEdit2;
            this.coToChuc.FieldName = "ToChuc";
            this.coToChuc.Name = "coToChuc";
            this.coToChuc.OptionsColumn.AllowEdit = false;
            this.coToChuc.Width = 88;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "Chức danh";
            this.gridColumn1.ColumnEdit = this.repositoryItemMemoEdit2;
            this.gridColumn1.FieldName = "ChucDanh";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 98;
            // 
            // colAttendanceDate
            // 
            this.colAttendanceDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colAttendanceDate.AppearanceCell.Options.UseFont = true;
            this.colAttendanceDate.AppearanceCell.Options.UseTextOptions = true;
            this.colAttendanceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAttendanceDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAttendanceDate.Caption = "Ngày";
            this.colAttendanceDate.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colAttendanceDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colAttendanceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colAttendanceDate.FieldName = "AttendanceDate";
            this.colAttendanceDate.Name = "colAttendanceDate";
            this.colAttendanceDate.OptionsColumn.AllowEdit = false;
            this.colAttendanceDate.Visible = true;
            this.colAttendanceDate.VisibleIndex = 1;
            this.colAttendanceDate.Width = 110;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Ngày trong tuần";
            this.gridColumn3.ColumnEdit = this.repositoryItemMemoEdit2;
            this.gridColumn3.FieldName = "DayWeek";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Width = 102;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "Khoảng thời gian";
            this.gridColumn4.ColumnEdit = this.repositoryItemMemoEdit2;
            this.gridColumn4.FieldName = "Interval";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Width = 159;
            // 
            // colCheckIn
            // 
            this.colCheckIn.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCheckIn.AppearanceCell.Options.UseFont = true;
            this.colCheckIn.AppearanceCell.Options.UseTextOptions = true;
            this.colCheckIn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheckIn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCheckIn.Caption = "Giờ vào";
            this.colCheckIn.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colCheckIn.FieldName = "CheckIn";
            this.colCheckIn.MaxWidth = 100;
            this.colCheckIn.MinWidth = 100;
            this.colCheckIn.Name = "colCheckIn";
            this.colCheckIn.OptionsColumn.AllowEdit = false;
            this.colCheckIn.Visible = true;
            this.colCheckIn.VisibleIndex = 2;
            this.colCheckIn.Width = 100;
            // 
            // colCheckOut
            // 
            this.colCheckOut.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCheckOut.AppearanceCell.Options.UseFont = true;
            this.colCheckOut.AppearanceCell.Options.UseTextOptions = true;
            this.colCheckOut.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheckOut.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCheckOut.Caption = "Giờ ra";
            this.colCheckOut.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colCheckOut.FieldName = "CheckOut";
            this.colCheckOut.MaxWidth = 100;
            this.colCheckOut.MinWidth = 100;
            this.colCheckOut.Name = "colCheckOut";
            this.colCheckOut.OptionsColumn.AllowEdit = false;
            this.colCheckOut.Visible = true;
            this.colCheckOut.VisibleIndex = 3;
            this.colCheckOut.Width = 100;
            // 
            // colIsLate
            // 
            this.colIsLate.Caption = "Đi muộn";
            this.colIsLate.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsLate.FieldName = "IsLate";
            this.colIsLate.MaxWidth = 100;
            this.colIsLate.MinWidth = 100;
            this.colIsLate.Name = "colIsLate";
            this.colIsLate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IsLate", "{0:0.##}")});
            this.colIsLate.Width = 100;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colIsEarly
            // 
            this.colIsEarly.Caption = "Về sớm";
            this.colIsEarly.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsEarly.FieldName = "IsEarly";
            this.colIsEarly.MaxWidth = 100;
            this.colIsEarly.MinWidth = 100;
            this.colIsEarly.Name = "colIsEarly";
            this.colIsEarly.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Early", "{0:0.##}")});
            this.colIsEarly.Width = 100;
            // 
            // colOvetime
            // 
            this.colOvetime.Caption = "Làm thêm";
            this.colOvetime.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOvetime.FieldName = "OverTime";
            this.colOvetime.MaxWidth = 100;
            this.colOvetime.MinWidth = 100;
            this.colOvetime.Name = "colOvetime";
            this.colOvetime.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OverTime", "{0:0.##}")});
            this.colOvetime.Visible = true;
            this.colOvetime.VisibleIndex = 6;
            this.colOvetime.Width = 100;
            // 
            // colBussiness
            // 
            this.colBussiness.Caption = "Công tác";
            this.colBussiness.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colBussiness.FieldName = "Bussiness";
            this.colBussiness.MaxWidth = 100;
            this.colBussiness.MinWidth = 100;
            this.colBussiness.Name = "colBussiness";
            this.colBussiness.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Bussiness", "{0:0.##}")});
            this.colBussiness.Visible = true;
            this.colBussiness.VisibleIndex = 7;
            this.colBussiness.Width = 100;
            // 
            // colNoFingerprint
            // 
            this.colNoFingerprint.Caption = "Quên vân tay";
            this.colNoFingerprint.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colNoFingerprint.FieldName = "NoFingerprint";
            this.colNoFingerprint.MaxWidth = 100;
            this.colNoFingerprint.MinWidth = 100;
            this.colNoFingerprint.Name = "colNoFingerprint";
            this.colNoFingerprint.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NoFingerprint", "{0:0.##}")});
            this.colNoFingerprint.Visible = true;
            this.colNoFingerprint.VisibleIndex = 8;
            this.colNoFingerprint.Width = 100;
            // 
            // colOnLeave
            // 
            this.colOnLeave.Caption = "Nghỉ";
            this.colOnLeave.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colOnLeave.FieldName = "OnLeave";
            this.colOnLeave.MaxWidth = 100;
            this.colOnLeave.MinWidth = 100;
            this.colOnLeave.Name = "colOnLeave";
            this.colOnLeave.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OnLeave", "{0:0.##}")});
            this.colOnLeave.Visible = true;
            this.colOnLeave.VisibleIndex = 9;
            this.colOnLeave.Width = 100;
            // 
            // colWFH
            // 
            this.colWFH.Caption = "WFH";
            this.colWFH.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colWFH.FieldName = "WFH";
            this.colWFH.MaxWidth = 100;
            this.colWFH.MinWidth = 100;
            this.colWFH.Name = "colWFH";
            this.colWFH.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WFH", "{0:0.##}")});
            this.colWFH.Visible = true;
            this.colWFH.VisibleIndex = 10;
            this.colWFH.Width = 100;
            // 
            // colIsLunch
            // 
            this.colIsLunch.Caption = "Cơm ca";
            this.colIsLunch.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsLunch.FieldName = "IsLunch";
            this.colIsLunch.MaxWidth = 100;
            this.colIsLunch.MinWidth = 100;
            this.colIsLunch.Name = "colIsLunch";
            this.colIsLunch.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IsLunch", "{0:0.##}")});
            this.colIsLunch.Visible = true;
            this.colIsLunch.VisibleIndex = 11;
            this.colIsLunch.Width = 100;
            // 
            // colIsLateRegister
            // 
            this.colIsLateRegister.Caption = "Đi muộn";
            this.colIsLateRegister.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsLateRegister.FieldName = "IsLateRegister";
            this.colIsLateRegister.MaxWidth = 100;
            this.colIsLateRegister.MinWidth = 100;
            this.colIsLateRegister.Name = "colIsLateRegister";
            this.colIsLateRegister.Visible = true;
            this.colIsLateRegister.VisibleIndex = 4;
            this.colIsLateRegister.Width = 100;
            // 
            // colIsEarlyRegister
            // 
            this.colIsEarlyRegister.Caption = "Về sớm";
            this.colIsEarlyRegister.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsEarlyRegister.FieldName = "IsEarlyRegister";
            this.colIsEarlyRegister.MaxWidth = 100;
            this.colIsEarlyRegister.MinWidth = 100;
            this.colIsEarlyRegister.Name = "colIsEarlyRegister";
            this.colIsEarlyRegister.Visible = true;
            this.colIsEarlyRegister.VisibleIndex = 5;
            this.colIsEarlyRegister.Width = 100;
            // 
            // colHolidayDay
            // 
            this.colHolidayDay.FieldName = "HolidayDay";
            this.colHolidayDay.Name = "colHolidayDay";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 44);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1376, 740);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdData);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1374, 715);
            this.xtraTabPage1.Text = "THÔNG TIN CHUNG";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdAttendance);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1374, 715);
            this.xtraTabPage2.Text = "VÂN TAY";
            // 
            // frmPersonalSynthetic
            // 
            this.AcceptButton = this.btnLoadData;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 784);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmPersonalSynthetic";
            this.Text = "TỔNG HỢP CÁ NHÂN";
            this.Load += new System.EventHandler(this.frmPersonalSynthetic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colHangMuc;
        private DevExpress.XtraGrid.Columns.GridColumn colTypetext;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtMonth;
        private System.Windows.Forms.Label lbEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn colValueText;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colHRApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colTBPApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colHRCancel;
        private DevExpress.XtraGrid.Columns.GridColumn colTBPCancel;
        private System.Windows.Forms.Button btnLoadData;
        private DevExpress.XtraGrid.GridControl grdAttendance;
        private DevExpress.XtraGrid.Views.Grid.GridView grvAttendance;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colIDChamCongMoi;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn coToChuc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colAttendanceDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckIn;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckOut;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLate;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsEarly;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.Columns.GridColumn colOvetime;
        private DevExpress.XtraGrid.Columns.GridColumn colBussiness;
        private DevExpress.XtraGrid.Columns.GridColumn colNoFingerprint;
        private DevExpress.XtraGrid.Columns.GridColumn colOnLeave;
        private DevExpress.XtraGrid.Columns.GridColumn colWFH;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLunch;
        private DevExpress.XtraGrid.Columns.GridColumn colValueTextReal;
        private DevExpress.XtraGrid.Columns.GridColumn colIsLateRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colIsEarlyRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colHolidayDay;
    }
}