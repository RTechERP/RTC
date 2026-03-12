
namespace BMS
{
    partial class frmCopyQuestions
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.grdQuestion = new DevExpress.XtraGrid.GridControl();
            this.grvQuestion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCourseQuestionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuestionText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDapAnA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDapAnB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDapAnC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDapAnD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCorrectAnswers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCourseExamText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCourse = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCatalog = new DevExpress.XtraGrid.Columns.GridColumn();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.lblCourse = new DevExpress.XtraEditors.LabelControl();
            this.cboCourseCatalog = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.grvCatalog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCatalogID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCourseCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCourseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblCourseExam = new DevExpress.XtraEditors.LabelControl();
            this.cboCourse = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.grvCourseExam = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCourseID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.cboExamType = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboCourseExam = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblFilterText = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCourseCatalog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCatalog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCourse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCourseExam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCourseExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator3});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1328, 55);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 52);
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            this.toolStripSeparator3.Visible = false;
            // 
            // grdQuestion
            // 
            this.grdQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQuestion.Location = new System.Drawing.Point(0, 103);
            this.grdQuestion.MainView = this.grvQuestion;
            this.grdQuestion.Name = "grdQuestion";
            this.grdQuestion.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit3});
            this.grdQuestion.Size = new System.Drawing.Size(1328, 589);
            this.grdQuestion.TabIndex = 30;
            this.grdQuestion.Tag = "";
            this.grdQuestion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvQuestion});
            // 
            // grvQuestion
            // 
            this.grvQuestion.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvQuestion.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvQuestion.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvQuestion.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvQuestion.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvQuestion.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvQuestion.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvQuestion.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvQuestion.Appearance.Row.Options.UseFont = true;
            this.grvQuestion.Appearance.Row.Options.UseTextOptions = true;
            this.grvQuestion.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvQuestion.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvQuestion.ColumnPanelRowHeight = 45;
            this.grvQuestion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCourseQuestionID,
            this.colSTT,
            this.colQuestionText,
            this.colImage,
            this.colDapAnA,
            this.colDapAnB,
            this.colDapAnC,
            this.colDapAnD,
            this.colCorrectAnswers,
            this.colCourseExamText,
            this.colCourse,
            this.colCatalog});
            this.grvQuestion.GridControl = this.grdQuestion;
            this.grvQuestion.GroupCount = 3;
            this.grvQuestion.Name = "grvQuestion";
            this.grvQuestion.OptionsBehavior.Editable = false;
            this.grvQuestion.OptionsBehavior.ReadOnly = true;
            this.grvQuestion.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvQuestion.OptionsSelection.MultiSelect = true;
            this.grvQuestion.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvQuestion.OptionsView.RowAutoHeight = true;
            this.grvQuestion.OptionsView.ShowGroupPanel = false;
            this.grvQuestion.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCatalog, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCourse, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCourseExamText, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvQuestion.Tag = "";
            // 
            // colCourseQuestionID
            // 
            this.colCourseQuestionID.Caption = "ID";
            this.colCourseQuestionID.FieldName = "ID";
            this.colCourseQuestionID.Name = "colCourseQuestionID";
            this.colCourseQuestionID.Width = 262;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.OptionsColumn.AllowMove = false;
            this.colSTT.OptionsColumn.AllowShowHide = false;
            this.colSTT.OptionsColumn.TabStop = false;
            this.colSTT.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colSTT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "STT", "{0}")});
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 1;
            this.colSTT.Width = 49;
            // 
            // colQuestionText
            // 
            this.colQuestionText.Caption = "Nội dung câu hỏi";
            this.colQuestionText.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colQuestionText.FieldName = "QuestionText";
            this.colQuestionText.Name = "colQuestionText";
            this.colQuestionText.OptionsColumn.AllowEdit = false;
            this.colQuestionText.OptionsColumn.AllowMove = false;
            this.colQuestionText.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colQuestionText.Visible = true;
            this.colQuestionText.VisibleIndex = 2;
            this.colQuestionText.Width = 401;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colImage
            // 
            this.colImage.AppearanceCell.Options.UseTextOptions = true;
            this.colImage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colImage.Caption = "Ảnh";
            this.colImage.FieldName = "Image";
            this.colImage.MinWidth = 19;
            this.colImage.Name = "colImage";
            this.colImage.Visible = true;
            this.colImage.VisibleIndex = 3;
            this.colImage.Width = 70;
            // 
            // colDapAnA
            // 
            this.colDapAnA.Caption = "Đáp án A";
            this.colDapAnA.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colDapAnA.FieldName = "A";
            this.colDapAnA.Name = "colDapAnA";
            this.colDapAnA.Visible = true;
            this.colDapAnA.VisibleIndex = 4;
            this.colDapAnA.Width = 123;
            // 
            // colDapAnB
            // 
            this.colDapAnB.Caption = "Đáp án B";
            this.colDapAnB.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colDapAnB.FieldName = "B";
            this.colDapAnB.Name = "colDapAnB";
            this.colDapAnB.Visible = true;
            this.colDapAnB.VisibleIndex = 5;
            this.colDapAnB.Width = 123;
            // 
            // colDapAnC
            // 
            this.colDapAnC.Caption = "Đáp án C";
            this.colDapAnC.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colDapAnC.FieldName = "C";
            this.colDapAnC.Name = "colDapAnC";
            this.colDapAnC.Visible = true;
            this.colDapAnC.VisibleIndex = 6;
            this.colDapAnC.Width = 123;
            // 
            // colDapAnD
            // 
            this.colDapAnD.Caption = "Đáp án D";
            this.colDapAnD.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colDapAnD.FieldName = "D";
            this.colDapAnD.Name = "colDapAnD";
            this.colDapAnD.Visible = true;
            this.colDapAnD.VisibleIndex = 7;
            this.colDapAnD.Width = 123;
            // 
            // colCorrectAnswers
            // 
            this.colCorrectAnswers.Caption = "Đáp án đúng";
            this.colCorrectAnswers.FieldName = "CorrectAnswers";
            this.colCorrectAnswers.MinWidth = 19;
            this.colCorrectAnswers.Name = "colCorrectAnswers";
            this.colCorrectAnswers.Visible = true;
            this.colCorrectAnswers.VisibleIndex = 8;
            this.colCorrectAnswers.Width = 94;
            // 
            // colCourseExamText
            // 
            this.colCourseExamText.Caption = "Đề thi";
            this.colCourseExamText.FieldName = "CourseExam";
            this.colCourseExamText.MinWidth = 19;
            this.colCourseExamText.Name = "colCourseExamText";
            this.colCourseExamText.Visible = true;
            this.colCourseExamText.VisibleIndex = 8;
            this.colCourseExamText.Width = 70;
            // 
            // colCourse
            // 
            this.colCourse.Caption = "Khóa học";
            this.colCourse.FieldName = "Course";
            this.colCourse.MinWidth = 19;
            this.colCourse.Name = "colCourse";
            this.colCourse.Visible = true;
            this.colCourse.VisibleIndex = 9;
            this.colCourse.Width = 70;
            // 
            // colCatalog
            // 
            this.colCatalog.Caption = "Loại";
            this.colCatalog.FieldName = "CourseCatalog";
            this.colCatalog.MinWidth = 19;
            this.colCatalog.Name = "colCatalog";
            this.colCatalog.Visible = true;
            this.colCatalog.VisibleIndex = 8;
            this.colCatalog.Width = 70;
            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.lblCourse);
            this.stackPanel1.Controls.Add(this.cboCourseCatalog);
            this.stackPanel1.Controls.Add(this.lblCourseExam);
            this.stackPanel1.Controls.Add(this.cboCourse);
            this.stackPanel1.Controls.Add(this.label11);
            this.stackPanel1.Controls.Add(this.cboExamType);
            this.stackPanel1.Controls.Add(this.labelControl2);
            this.stackPanel1.Controls.Add(this.cboCourseExam);
            this.stackPanel1.Controls.Add(this.lblFilterText);
            this.stackPanel1.Controls.Add(this.txtFilterText);
            this.stackPanel1.Controls.Add(this.btnFind);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackPanel1.Location = new System.Drawing.Point(0, 55);
            this.stackPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(1328, 48);
            this.stackPanel1.TabIndex = 31;
            // 
            // lblCourse
            // 
            this.lblCourse.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.Appearance.Options.UseFont = true;
            this.lblCourse.Location = new System.Drawing.Point(11, 16);
            this.lblCourse.Margin = new System.Windows.Forms.Padding(11, 2, 2, 2);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(60, 16);
            this.lblCourse.TabIndex = 2;
            this.lblCourse.Text = "Danh mục";
            // 
            // cboCourseCatalog
            // 
            this.cboCourseCatalog.EditValue = "- Chọn phòng ban -";
            this.cboCourseCatalog.Location = new System.Drawing.Point(75, 11);
            this.cboCourseCatalog.Margin = new System.Windows.Forms.Padding(2);
            this.cboCourseCatalog.Name = "cboCourseCatalog";
            this.cboCourseCatalog.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCourseCatalog.Properties.Appearance.Options.UseFont = true;
            this.cboCourseCatalog.Properties.AutoHeight = false;
            this.cboCourseCatalog.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCourseCatalog.Properties.NullText = "";
            this.cboCourseCatalog.Properties.PopupView = this.grvCatalog;
            this.cboCourseCatalog.Size = new System.Drawing.Size(206, 25);
            this.cboCourseCatalog.TabIndex = 1;
            this.cboCourseCatalog.Popup += new System.EventHandler(this.cboCourse_Popup);
            this.cboCourseCatalog.EditValueChanged += new System.EventHandler(this.cboCourseCatalog_EditValueChanged);
            // 
            // grvCatalog
            // 
            this.grvCatalog.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvCatalog.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvCatalog.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvCatalog.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvCatalog.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvCatalog.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvCatalog.Appearance.Row.Options.UseFont = true;
            this.grvCatalog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCatalogID,
            this.colCourseCode,
            this.colCourseName});
            this.grvCatalog.DetailHeight = 284;
            this.grvCatalog.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvCatalog.Name = "grvCatalog";
            this.grvCatalog.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvCatalog.OptionsView.ShowGroupPanel = false;
            // 
            // colCatalogID
            // 
            this.colCatalogID.Caption = "ID";
            this.colCatalogID.FieldName = "ID";
            this.colCatalogID.MinWidth = 15;
            this.colCatalogID.Name = "colCatalogID";
            this.colCatalogID.Width = 56;
            // 
            // colCourseCode
            // 
            this.colCourseCode.Caption = "Mã";
            this.colCourseCode.FieldName = "Code";
            this.colCourseCode.MinWidth = 15;
            this.colCourseCode.Name = "colCourseCode";
            this.colCourseCode.Visible = true;
            this.colCourseCode.VisibleIndex = 0;
            this.colCourseCode.Width = 134;
            // 
            // colCourseName
            // 
            this.colCourseName.Caption = "Tên";
            this.colCourseName.FieldName = "Name";
            this.colCourseName.MinWidth = 15;
            this.colCourseName.Name = "colCourseName";
            this.colCourseName.Visible = true;
            this.colCourseName.VisibleIndex = 1;
            this.colCourseName.Width = 597;
            // 
            // lblCourseExam
            // 
            this.lblCourseExam.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourseExam.Appearance.Options.UseFont = true;
            this.lblCourseExam.Location = new System.Drawing.Point(294, 16);
            this.lblCourseExam.Margin = new System.Windows.Forms.Padding(11, 2, 2, 2);
            this.lblCourseExam.Name = "lblCourseExam";
            this.lblCourseExam.Size = new System.Drawing.Size(74, 16);
            this.lblCourseExam.TabIndex = 3;
            this.lblCourseExam.Text = "Từ khóa học";
            // 
            // cboCourse
            // 
            this.cboCourse.EditValue = "";
            this.cboCourse.Location = new System.Drawing.Point(372, 11);
            this.cboCourse.Margin = new System.Windows.Forms.Padding(2);
            this.cboCourse.Name = "cboCourse";
            this.cboCourse.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCourse.Properties.Appearance.Options.UseFont = true;
            this.cboCourse.Properties.AutoHeight = false;
            this.cboCourse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCourse.Properties.NullText = "";
            this.cboCourse.Properties.PopupView = this.grvCourseExam;
            this.cboCourse.Size = new System.Drawing.Size(193, 25);
            this.cboCourse.TabIndex = 0;
            this.cboCourse.EditValueChanged += new System.EventHandler(this.cboCourse_EditValueChanged_1);
            // 
            // grvCourseExam
            // 
            this.grvCourseExam.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvCourseExam.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvCourseExam.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvCourseExam.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvCourseExam.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvCourseExam.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvCourseExam.Appearance.Row.Options.UseFont = true;
            this.grvCourseExam.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCourseID,
            this.colCode,
            this.colName});
            this.grvCourseExam.DetailHeight = 284;
            this.grvCourseExam.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvCourseExam.Name = "grvCourseExam";
            this.grvCourseExam.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvCourseExam.OptionsView.ShowGroupPanel = false;
            // 
            // colCourseID
            // 
            this.colCourseID.Caption = "ID";
            this.colCourseID.MinWidth = 15;
            this.colCourseID.Name = "colCourseID";
            this.colCourseID.Width = 56;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã khóa học";
            this.colCode.FieldName = "Code";
            this.colCode.MinWidth = 15;
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 163;
            // 
            // colName
            // 
            this.colName.Caption = "Tên khóa học";
            this.colName.FieldName = "NameCourse";
            this.colName.MinWidth = 15;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 568;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(570, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 16);
            this.label11.TabIndex = 206;
            this.label11.Text = "Loại";
            // 
            // cboExamType
            // 
            this.cboExamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExamType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExamType.FormattingEnabled = true;
            this.cboExamType.Items.AddRange(new object[] {
            "--Chọn loại--",
            "Trắc nghiệm",
            "Thực hành",
            "Bài tập"});
            this.cboExamType.Location = new System.Drawing.Point(609, 12);
            this.cboExamType.Name = "cboExamType";
            this.cboExamType.Size = new System.Drawing.Size(108, 24);
            this.cboExamType.TabIndex = 207;
            this.cboExamType.SelectedIndexChanged += new System.EventHandler(this.cboExamType_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(731, 16);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(11, 2, 2, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Đến đề thi";
            // 
            // cboCourseExam
            // 
            this.cboCourseExam.EditValue = "";
            this.cboCourseExam.Location = new System.Drawing.Point(794, 11);
            this.cboCourseExam.Margin = new System.Windows.Forms.Padding(2);
            this.cboCourseExam.Name = "cboCourseExam";
            this.cboCourseExam.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCourseExam.Properties.Appearance.Options.UseFont = true;
            this.cboCourseExam.Properties.AutoHeight = false;
            this.cboCourseExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCourseExam.Properties.NullText = "";
            this.cboCourseExam.Properties.PopupView = this.gridView2;
            this.cboCourseExam.Size = new System.Drawing.Size(158, 25);
            this.cboCourseExam.TabIndex = 0;
            this.cboCourseExam.EditValueChanged += new System.EventHandler(this.cboCourse_EditValueChanged_1);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn1});
            this.gridView2.DetailHeight = 284;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GroupCount = 2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn8, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Mã đề thi";
            this.gridColumn4.FieldName = "CodeExam";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tên đề thi";
            this.gridColumn5.FieldName = "NameExam";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Số điểm cần đạt (%)";
            this.gridColumn6.FieldName = "Goal";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Thời gian (Phút)";
            this.gridColumn7.FieldName = "TestTime";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Loại";
            this.gridColumn8.FieldName = "ExamTypeText";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Khoá học";
            this.gridColumn1.FieldName = "NameCourse";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // lblFilterText
            // 
            this.lblFilterText.AutoSize = true;
            this.lblFilterText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblFilterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterText.Location = new System.Drawing.Point(965, 16);
            this.lblFilterText.Margin = new System.Windows.Forms.Padding(11, 0, 3, 0);
            this.lblFilterText.Name = "lblFilterText";
            this.lblFilterText.Size = new System.Drawing.Size(56, 16);
            this.lblFilterText.TabIndex = 203;
            this.lblFilterText.Text = "Từ khóa";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterText.Location = new System.Drawing.Point(1027, 13);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(201, 22);
            this.txtFilterText.TabIndex = 204;
            this.txtFilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterText_KeyDown);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(1234, 10);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(73, 27);
            this.btnFind.TabIndex = 205;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // frmCopyQuestions
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 692);
            this.Controls.Add(this.grdQuestion);
            this.Controls.Add(this.stackPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmCopyQuestions";
            this.Text = "SAO CHÉP CÂU HỎI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCopyQuestions_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCourseCatalog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCatalog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCourse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCourseExam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCourseExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevExpress.XtraGrid.GridControl grdQuestion;
        private DevExpress.XtraGrid.Views.Grid.GridView grvQuestion;
        private DevExpress.XtraGrid.Columns.GridColumn colCourseQuestionID;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colQuestionText;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colDapAnA;
        private DevExpress.XtraGrid.Columns.GridColumn colDapAnB;
        private DevExpress.XtraGrid.Columns.GridColumn colDapAnC;
        private DevExpress.XtraGrid.Columns.GridColumn colDapAnD;
        private DevExpress.XtraGrid.Columns.GridColumn colCorrectAnswers;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboCourse;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCourseExam;
        private DevExpress.XtraEditors.SearchLookUpEdit cboCourseCatalog;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCatalog;
        private DevExpress.XtraGrid.Columns.GridColumn colCatalogID;
        private DevExpress.XtraGrid.Columns.GridColumn colCourseCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCourseName;
        private DevExpress.XtraEditors.LabelControl lblCourse;
        private DevExpress.XtraEditors.LabelControl lblCourseExam;
        private DevExpress.XtraGrid.Columns.GridColumn colCourseID;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colCourseExamText;
        private DevExpress.XtraGrid.Columns.GridColumn colCourse;
        private DevExpress.XtraGrid.Columns.GridColumn colCatalog;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label lblFilterText;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        public DevExpress.XtraEditors.SearchLookUpEdit cboCourseExam;
        public System.Windows.Forms.ComboBox cboExamType;
    }
}