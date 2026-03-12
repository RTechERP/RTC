
namespace BMS
{
    partial class frmProductivityIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductivityIndex));
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nbrYear = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdReport = new DevExpress.XtraGrid.GridControl();
            this.grvReport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIndexGrantedAdmin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoReportDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoReportWeek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSuccessfulProposal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAVGIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbUser = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdPerformance = new DevExpress.XtraGrid.GridControl();
            this.grvPerformance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDBot = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCoefficient = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPerformance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPerformanceOld = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbPosition = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.nbrYear)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPerformance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPerformance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMonth
            // 
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Items.AddRange(new object[] {
            "Quý 1",
            "Quý 2",
            "Quý 3",
            "Quý 4"});
            this.cbMonth.Location = new System.Drawing.Point(257, 11);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(101, 21);
            this.cbMonth.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 18);
            this.label2.TabIndex = 63;
            this.label2.Text = "Quý";
            // 
            // nbrYear
            // 
            this.nbrYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbrYear.Location = new System.Drawing.Point(423, 10);
            this.nbrYear.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nbrYear.Name = "nbrYear";
            this.nbrYear.ReadOnly = true;
            this.nbrYear.Size = new System.Drawing.Size(85, 21);
            this.nbrYear.TabIndex = 64;
            this.nbrYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(377, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 18);
            this.label3.TabIndex = 65;
            this.label3.Text = "Năm";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 37);
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator2});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(800, 42);
            this.mnuMenu.TabIndex = 4;
            this.mnuMenu.TabStop = true;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.grdReport, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdPerformance, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.44118F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.55882F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 408);
            this.tableLayoutPanel1.TabIndex = 66;
            // 
            // grdReport
            // 
            this.grdReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReport.Location = new System.Drawing.Point(3, 3);
            this.grdReport.MainView = this.grvReport;
            this.grdReport.Name = "grdReport";
            this.grdReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbUser});
            this.grdReport.Size = new System.Drawing.Size(794, 261);
            this.grdReport.TabIndex = 5;
            this.grdReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvReport});
            this.grdReport.Click += new System.EventHandler(this.grdData_Click);
            // 
            // grvReport
            // 
            this.grvReport.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colUser,
            this.colIndexGrantedAdmin,
            this.colNoReportDate,
            this.colNoReportWeek,
            this.colSuccessfulProposal,
            this.colAVGIndex,
            this.colMonth});
            this.grvReport.GridControl = this.grdReport;
            this.grvReport.Name = "grvReport";
            this.grvReport.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvReport.OptionsView.ShowFooter = true;
            this.grvReport.OptionsView.ShowGroupPanel = false;
            this.grvReport.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            this.grvReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvReport_KeyDown);
            // 
            // colID
            // 
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colUser
            // 
            this.colUser.AppearanceCell.Options.UseTextOptions = true;
            this.colUser.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colUser.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUser.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colUser.AppearanceHeader.Options.UseFont = true;
            this.colUser.AppearanceHeader.Options.UseForeColor = true;
            this.colUser.AppearanceHeader.Options.UseTextOptions = true;
            this.colUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUser.Caption = "Nhân viên";
            this.colUser.FieldName = "UserID";
            this.colUser.Name = "colUser";
            // 
            // colIndexGrantedAdmin
            // 
            this.colIndexGrantedAdmin.AppearanceCell.Options.UseTextOptions = true;
            this.colIndexGrantedAdmin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colIndexGrantedAdmin.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIndexGrantedAdmin.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIndexGrantedAdmin.AppearanceHeader.Options.UseFont = true;
            this.colIndexGrantedAdmin.AppearanceHeader.Options.UseForeColor = true;
            this.colIndexGrantedAdmin.AppearanceHeader.Options.UseTextOptions = true;
            this.colIndexGrantedAdmin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIndexGrantedAdmin.Caption = "Các chỉ số đưa về cho Admin";
            this.colIndexGrantedAdmin.DisplayFormat.FormatString = "P";
            this.colIndexGrantedAdmin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIndexGrantedAdmin.FieldName = "IndexGrantedAdmin";
            this.colIndexGrantedAdmin.GroupFormat.FormatString = "p";
            this.colIndexGrantedAdmin.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIndexGrantedAdmin.Name = "colIndexGrantedAdmin";
            this.colIndexGrantedAdmin.Visible = true;
            this.colIndexGrantedAdmin.VisibleIndex = 0;
            // 
            // colNoReportDate
            // 
            this.colNoReportDate.AppearanceCell.Options.UseTextOptions = true;
            this.colNoReportDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNoReportDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNoReportDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNoReportDate.AppearanceHeader.Options.UseFont = true;
            this.colNoReportDate.AppearanceHeader.Options.UseForeColor = true;
            this.colNoReportDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoReportDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoReportDate.Caption = "Thiếu báo cáo ngày";
            this.colNoReportDate.DisplayFormat.FormatString = "P";
            this.colNoReportDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNoReportDate.FieldName = "NoReportDate";
            this.colNoReportDate.GroupFormat.FormatString = "p";
            this.colNoReportDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNoReportDate.Name = "colNoReportDate";
            this.colNoReportDate.Visible = true;
            this.colNoReportDate.VisibleIndex = 1;
            // 
            // colNoReportWeek
            // 
            this.colNoReportWeek.AppearanceCell.Options.UseTextOptions = true;
            this.colNoReportWeek.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNoReportWeek.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNoReportWeek.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colNoReportWeek.AppearanceHeader.Options.UseFont = true;
            this.colNoReportWeek.AppearanceHeader.Options.UseForeColor = true;
            this.colNoReportWeek.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoReportWeek.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoReportWeek.Caption = "Thiếu báo cáo tuần";
            this.colNoReportWeek.DisplayFormat.FormatString = "P";
            this.colNoReportWeek.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNoReportWeek.FieldName = "NoReportWeek";
            this.colNoReportWeek.GroupFormat.FormatString = "p";
            this.colNoReportWeek.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNoReportWeek.Name = "colNoReportWeek";
            this.colNoReportWeek.Visible = true;
            this.colNoReportWeek.VisibleIndex = 2;
            // 
            // colSuccessfulProposal
            // 
            this.colSuccessfulProposal.AppearanceCell.Options.UseTextOptions = true;
            this.colSuccessfulProposal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSuccessfulProposal.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSuccessfulProposal.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSuccessfulProposal.AppearanceHeader.Options.UseFont = true;
            this.colSuccessfulProposal.AppearanceHeader.Options.UseForeColor = true;
            this.colSuccessfulProposal.AppearanceHeader.Options.UseTextOptions = true;
            this.colSuccessfulProposal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSuccessfulProposal.Caption = "Số lần đề suất cải tiến hiệu suất làm việc thành công";
            this.colSuccessfulProposal.DisplayFormat.FormatString = "P";
            this.colSuccessfulProposal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSuccessfulProposal.FieldName = "SuccessfulProposal";
            this.colSuccessfulProposal.GroupFormat.FormatString = "p";
            this.colSuccessfulProposal.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSuccessfulProposal.Name = "colSuccessfulProposal";
            this.colSuccessfulProposal.Visible = true;
            this.colSuccessfulProposal.VisibleIndex = 3;
            // 
            // colAVGIndex
            // 
            this.colAVGIndex.AppearanceCell.Options.UseTextOptions = true;
            this.colAVGIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAVGIndex.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colAVGIndex.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colAVGIndex.AppearanceHeader.Options.UseFont = true;
            this.colAVGIndex.AppearanceHeader.Options.UseForeColor = true;
            this.colAVGIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colAVGIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAVGIndex.Caption = "Trung bình quý";
            this.colAVGIndex.FieldName = "AVGIndex";
            this.colAVGIndex.Name = "colAVGIndex";
            // 
            // colMonth
            // 
            this.colMonth.AppearanceCell.Options.UseTextOptions = true;
            this.colMonth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colMonth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMonth.AppearanceHeader.Options.UseFont = true;
            this.colMonth.AppearanceHeader.Options.UseForeColor = true;
            this.colMonth.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth.Caption = "Tháng";
            this.colMonth.FieldName = "Month";
            this.colMonth.Name = "colMonth";
            this.colMonth.Visible = true;
            this.colMonth.VisibleIndex = 4;
            // 
            // cbUser
            // 
            this.cbUser.AutoHeight = false;
            this.cbUser.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbUser.Name = "cbUser";
            this.cbUser.NullText = "";
            this.cbUser.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // grdPerformance
            // 
            this.grdPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPerformance.Location = new System.Drawing.Point(3, 270);
            this.grdPerformance.MainView = this.grvPerformance;
            this.grdPerformance.Name = "grdPerformance";
            this.grdPerformance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbPosition});
            this.grdPerformance.Size = new System.Drawing.Size(794, 135);
            this.grdPerformance.TabIndex = 6;
            this.grdPerformance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvPerformance});
            // 
            // grvPerformance
            // 
            this.grvPerformance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDBot,
            this.colCoefficient,
            this.colPerformance,
            this.colPerformanceOld,
            this.colPosition});
            this.grvPerformance.GridControl = this.grdPerformance;
            this.grvPerformance.Name = "grvPerformance";
            this.grvPerformance.OptionsView.ShowGroupPanel = false;
            this.grvPerformance.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvPerformance_CellValueChanged);
            this.grvPerformance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvPerformance_KeyDown);
            this.grvPerformance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grvPerformance_MouseDown);
            // 
            // colIDBot
            // 
            this.colIDBot.AppearanceCell.Options.UseTextOptions = true;
            this.colIDBot.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colIDBot.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIDBot.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIDBot.AppearanceHeader.Options.UseFont = true;
            this.colIDBot.AppearanceHeader.Options.UseForeColor = true;
            this.colIDBot.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDBot.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDBot.Caption = "ID";
            this.colIDBot.FieldName = "ID";
            this.colIDBot.Name = "colIDBot";
            // 
            // colCoefficient
            // 
            this.colCoefficient.AppearanceCell.Options.UseTextOptions = true;
            this.colCoefficient.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCoefficient.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCoefficient.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCoefficient.AppearanceHeader.Options.UseFont = true;
            this.colCoefficient.AppearanceHeader.Options.UseForeColor = true;
            this.colCoefficient.AppearanceHeader.Options.UseTextOptions = true;
            this.colCoefficient.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCoefficient.Caption = "Hệ số tính thưởng";
            this.colCoefficient.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCoefficient.FieldName = "Coefficient";
            this.colCoefficient.Name = "colCoefficient";
            this.colCoefficient.Visible = true;
            this.colCoefficient.VisibleIndex = 0;
            // 
            // colPerformance
            // 
            this.colPerformance.AppearanceCell.Options.UseTextOptions = true;
            this.colPerformance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPerformance.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPerformance.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPerformance.AppearanceHeader.Options.UseFont = true;
            this.colPerformance.AppearanceHeader.Options.UseForeColor = true;
            this.colPerformance.AppearanceHeader.Options.UseTextOptions = true;
            this.colPerformance.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPerformance.Caption = "Performance";
            this.colPerformance.DisplayFormat.FormatString = "p";
            this.colPerformance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPerformance.FieldName = "Performance";
            this.colPerformance.Name = "colPerformance";
            this.colPerformance.Visible = true;
            this.colPerformance.VisibleIndex = 1;
            // 
            // colPerformanceOld
            // 
            this.colPerformanceOld.AppearanceCell.Options.UseTextOptions = true;
            this.colPerformanceOld.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPerformanceOld.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPerformanceOld.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPerformanceOld.AppearanceHeader.Options.UseFont = true;
            this.colPerformanceOld.AppearanceHeader.Options.UseForeColor = true;
            this.colPerformanceOld.AppearanceHeader.Options.UseTextOptions = true;
            this.colPerformanceOld.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPerformanceOld.Caption = "PerformanceOld";
            this.colPerformanceOld.FieldName = "PerformanceOld";
            this.colPerformanceOld.Name = "colPerformanceOld";
            // 
            // colPosition
            // 
            this.colPosition.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPosition.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPosition.AppearanceHeader.Options.UseFont = true;
            this.colPosition.AppearanceHeader.Options.UseForeColor = true;
            this.colPosition.AppearanceHeader.Options.UseTextOptions = true;
            this.colPosition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPosition.Caption = "Chức vụ";
            this.colPosition.ColumnEdit = this.cbPosition;
            this.colPosition.FieldName = "IDSale";
            this.colPosition.Name = "colPosition";
            this.colPosition.Visible = true;
            this.colPosition.VisibleIndex = 2;
            // 
            // cbPosition
            // 
            this.cbPosition.AutoHeight = false;
            this.cbPosition.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // frmProductivityIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nbrYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmProductivityIndex";
            this.ShowInTaskbar = false;
            this.Text = "Kết quả báo cáo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProductivityIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nbrYear)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPerformance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvPerformance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nbrYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl grdReport;
        private DevExpress.XtraGrid.Views.Grid.GridView grvReport;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colUser;
        private DevExpress.XtraGrid.Columns.GridColumn colIndexGrantedAdmin;
        private DevExpress.XtraGrid.Columns.GridColumn colNoReportDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNoReportWeek;
        private DevExpress.XtraGrid.Columns.GridColumn colSuccessfulProposal;
        private DevExpress.XtraGrid.Columns.GridColumn colAVGIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbUser;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.GridControl grdPerformance;
        private DevExpress.XtraGrid.Views.Grid.GridView grvPerformance;
        private DevExpress.XtraGrid.Columns.GridColumn colIDBot;
        private DevExpress.XtraGrid.Columns.GridColumn colCoefficient;
        private DevExpress.XtraGrid.Columns.GridColumn colPerformance;
        private DevExpress.XtraGrid.Columns.GridColumn colPerformanceOld;
        private DevExpress.XtraGrid.Columns.GridColumn colPosition;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}