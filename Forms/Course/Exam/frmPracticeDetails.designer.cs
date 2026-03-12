
namespace BMS
{
    partial class frmPracticeDetails 
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdCourseResult = new DevExpress.XtraGrid.GridControl();
            this.grvCourseResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDExamResult = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamResultDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamResultStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamResult = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGoal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamCourseId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdExamResult = new DevExpress.XtraGrid.GridControl();
            this.grvExamResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colExamResultId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamResultSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamResultQuestion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colExamResultPoint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colExamResultNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveAndClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCourseResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCourseResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExamResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvExamResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 52);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1338, 492);
            this.splitContainerControl1.SplitterPosition = 667;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdCourseResult);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(667, 492);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Lịch sử thi";
            // 
            // grdCourseResult
            // 
            this.grdCourseResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCourseResult.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdCourseResult.Location = new System.Drawing.Point(2, 23);
            this.grdCourseResult.MainView = this.grvCourseResult;
            this.grdCourseResult.Margin = new System.Windows.Forms.Padding(2);
            this.grdCourseResult.Name = "grdCourseResult";
            this.grdCourseResult.Size = new System.Drawing.Size(663, 467);
            this.grdCourseResult.TabIndex = 0;
            this.grdCourseResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCourseResult});
            // 
            // grvCourseResult
            // 
            this.grvCourseResult.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Black;
            this.grvCourseResult.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvCourseResult.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvCourseResult.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvCourseResult.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.grvCourseResult.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvCourseResult.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvCourseResult.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvCourseResult.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvCourseResult.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvCourseResult.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvCourseResult.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvCourseResult.Appearance.Row.Options.UseFont = true;
            this.grvCourseResult.Appearance.Row.Options.UseTextOptions = true;
            this.grvCourseResult.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvCourseResult.ColumnPanelRowHeight = 50;
            this.grvCourseResult.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDExamResult,
            this.colEmployeeName,
            this.colExamResultDate,
            this.colExamResultStatus,
            this.colExamResult,
            this.colGoal,
            this.colExamCourseId});
            this.grvCourseResult.DetailHeight = 284;
            this.grvCourseResult.GridControl = this.grdCourseResult;
            this.grvCourseResult.Name = "grvCourseResult";
            this.grvCourseResult.OptionsBehavior.Editable = false;
            this.grvCourseResult.OptionsView.RowAutoHeight = true;
            this.grvCourseResult.OptionsView.ShowAutoFilterRow = true;
            this.grvCourseResult.OptionsView.ShowFooter = true;
            this.grvCourseResult.OptionsView.ShowGroupPanel = false;
            this.grvCourseResult.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvCourseResult_RowCellStyle);
            this.grvCourseResult.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvCourseResult_FocusedRowChanged);
            // 
            // colIDExamResult
            // 
            this.colIDExamResult.Caption = "ID";
            this.colIDExamResult.FieldName = "ID";
            this.colIDExamResult.MinWidth = 19;
            this.colIDExamResult.Name = "colIDExamResult";
            this.colIDExamResult.Width = 70;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Caption = "Họ tên";
            this.colEmployeeName.FieldName = "FullName";
            this.colEmployeeName.MinWidth = 19;
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FullName", "{0}")});
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 0;
            this.colEmployeeName.Width = 142;
            // 
            // colExamResultDate
            // 
            this.colExamResultDate.Caption = "Ngày thi";
            this.colExamResultDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colExamResultDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colExamResultDate.FieldName = "CreatedDate";
            this.colExamResultDate.MinWidth = 19;
            this.colExamResultDate.Name = "colExamResultDate";
            this.colExamResultDate.Visible = true;
            this.colExamResultDate.VisibleIndex = 1;
            this.colExamResultDate.Width = 128;
            // 
            // colExamResultStatus
            // 
            this.colExamResultStatus.Caption = "Trạng thái";
            this.colExamResultStatus.FieldName = "Status";
            this.colExamResultStatus.MinWidth = 19;
            this.colExamResultStatus.Name = "colExamResultStatus";
            this.colExamResultStatus.Visible = true;
            this.colExamResultStatus.VisibleIndex = 2;
            this.colExamResultStatus.Width = 127;
            // 
            // colExamResult
            // 
            this.colExamResult.Caption = "Điểm thực tế (%)";
            this.colExamResult.DisplayFormat.FormatString = "n0";
            this.colExamResult.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExamResult.FieldName = "PracticePoints";
            this.colExamResult.MinWidth = 19;
            this.colExamResult.Name = "colExamResult";
            this.colExamResult.Visible = true;
            this.colExamResult.VisibleIndex = 4;
            this.colExamResult.Width = 111;
            // 
            // colGoal
            // 
            this.colGoal.Caption = "Điểm cần đạt (%)";
            this.colGoal.DisplayFormat.FormatString = "n0";
            this.colGoal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGoal.FieldName = "Goal";
            this.colGoal.MinWidth = 19;
            this.colGoal.Name = "colGoal";
            this.colGoal.Visible = true;
            this.colGoal.VisibleIndex = 3;
            this.colGoal.Width = 111;
            // 
            // colExamCourseId
            // 
            this.colExamCourseId.Caption = "CourseId";
            this.colExamCourseId.FieldName = "CourseId";
            this.colExamCourseId.Name = "colExamCourseId";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grdExamResult);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(661, 492);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Kết quả thi";
            // 
            // grdExamResult
            // 
            this.grdExamResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdExamResult.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdExamResult.Location = new System.Drawing.Point(2, 23);
            this.grdExamResult.MainView = this.grvExamResult;
            this.grdExamResult.Margin = new System.Windows.Forms.Padding(2);
            this.grdExamResult.Name = "grdExamResult";
            this.grdExamResult.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemMemoEdit1});
            this.grdExamResult.Size = new System.Drawing.Size(657, 467);
            this.grdExamResult.TabIndex = 0;
            this.grdExamResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvExamResult});
            // 
            // grvExamResult
            // 
            this.grvExamResult.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvExamResult.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvExamResult.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvExamResult.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvExamResult.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvExamResult.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvExamResult.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvExamResult.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvExamResult.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvExamResult.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.grvExamResult.Appearance.Row.Options.UseFont = true;
            this.grvExamResult.Appearance.Row.Options.UseForeColor = true;
            this.grvExamResult.Appearance.Row.Options.UseTextOptions = true;
            this.grvExamResult.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvExamResult.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvExamResult.ColumnPanelRowHeight = 50;
            this.grvExamResult.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colExamResultId,
            this.colExamResultSTT,
            this.colExamResultQuestion,
            this.colExamResultPoint,
            this.colExamResultNote,
            this.colStatusText,
            this.colCreatedDate});
            this.grvExamResult.DetailHeight = 284;
            this.grvExamResult.GridControl = this.grdExamResult;
            this.grvExamResult.Name = "grvExamResult";
            this.grvExamResult.OptionsView.RowAutoHeight = true;
            this.grvExamResult.OptionsView.ShowAutoFilterRow = true;
            this.grvExamResult.OptionsView.ShowFooter = true;
            this.grvExamResult.OptionsView.ShowGroupPanel = false;
            // 
            // colExamResultId
            // 
            this.colExamResultId.Caption = "ID";
            this.colExamResultId.FieldName = "ID";
            this.colExamResultId.MinWidth = 19;
            this.colExamResultId.Name = "colExamResultId";
            this.colExamResultId.Width = 70;
            // 
            // colExamResultSTT
            // 
            this.colExamResultSTT.Caption = "STT";
            this.colExamResultSTT.FieldName = "STT";
            this.colExamResultSTT.MinWidth = 19;
            this.colExamResultSTT.Name = "colExamResultSTT";
            this.colExamResultSTT.OptionsColumn.AllowEdit = false;
            this.colExamResultSTT.Visible = true;
            this.colExamResultSTT.VisibleIndex = 0;
            this.colExamResultSTT.Width = 45;
            // 
            // colExamResultQuestion
            // 
            this.colExamResultQuestion.Caption = "Câu hỏi";
            this.colExamResultQuestion.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colExamResultQuestion.FieldName = "QuestionText";
            this.colExamResultQuestion.MinWidth = 19;
            this.colExamResultQuestion.Name = "colExamResultQuestion";
            this.colExamResultQuestion.OptionsColumn.AllowEdit = false;
            this.colExamResultQuestion.Visible = true;
            this.colExamResultQuestion.VisibleIndex = 1;
            this.colExamResultQuestion.Width = 273;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colExamResultPoint
            // 
            this.colExamResultPoint.Caption = "Điểm";
            this.colExamResultPoint.ColumnEdit = this.repositoryItemSpinEdit1;
            this.colExamResultPoint.DisplayFormat.FormatString = "n2";
            this.colExamResultPoint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colExamResultPoint.FieldName = "Point";
            this.colExamResultPoint.MinWidth = 19;
            this.colExamResultPoint.Name = "colExamResultPoint";
            this.colExamResultPoint.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Point", "SUM={0:0.##}")});
            this.colExamResultPoint.Visible = true;
            this.colExamResultPoint.VisibleIndex = 3;
            this.colExamResultPoint.Width = 71;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.DisplayFormat.FormatString = "n2";
            this.repositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.MaskSettings.Set("mask", "n2");
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            this.repositoryItemSpinEdit1.UseMaskAsDisplayFormat = true;
            // 
            // colExamResultNote
            // 
            this.colExamResultNote.Caption = "Ghi chú";
            this.colExamResultNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colExamResultNote.FieldName = "Note";
            this.colExamResultNote.MinWidth = 19;
            this.colExamResultNote.Name = "colExamResultNote";
            this.colExamResultNote.Visible = true;
            this.colExamResultNote.VisibleIndex = 4;
            this.colExamResultNote.Width = 119;
            // 
            // colStatusText
            // 
            this.colStatusText.Caption = "Tình trạng";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.OptionsColumn.AllowEdit = false;
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 2;
            this.colStatusText.Width = 107;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colCreatedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.btnSaveAndClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1338, 52);
            this.toolStrip1.TabIndex = 32;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = global::Forms.Properties.Resources.Save_32x322;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 49);
            this.toolStripButton1.Tag = "frmCourse_AddLesson";
            this.toolStripButton1.Text = "Cất";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndClose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSaveAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(78, 49);
            this.btnSaveAndClose.Tag = "frmCourse_AddLesson";
            this.btnSaveAndClose.Text = "Cất và đóng";
            this.btnSaveAndClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // frmPracticeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 544);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPracticeDetails";
            this.Text = "CHI TIẾT BÀI THI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPracticeDetails_FormClosing);
            this.Load += new System.EventHandler(this.frmPracticeDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCourseResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCourseResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExamResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvExamResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveAndClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdCourseResult;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCourseResult;
        private DevExpress.XtraGrid.Columns.GridColumn colIDExamResult;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultDate;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResult;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdExamResult;
        private DevExpress.XtraGrid.Views.Grid.GridView grvExamResult;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultId;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultQuestion;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultPoint;
        private DevExpress.XtraGrid.Columns.GridColumn colExamResultNote;
        private DevExpress.XtraGrid.Columns.GridColumn colGoal;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colExamCourseId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
    }
}