
namespace BMS
{
    partial class frmHistoryExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistoryExam));
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRowNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colTotalQuestion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalChosen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalCorrect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalIncorrect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodeExamResult = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPercentageCorrect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodeExam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExamTypeText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.colExamType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 46);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit3});
            this.grdData.Size = new System.Drawing.Size(1368, 471);
            this.grdData.TabIndex = 170;
            this.grdData.Tag = "";
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRowNumber,
            this.colFullName,
            this.colTotalQuestion,
            this.colTotalChosen,
            this.colTotalCorrect,
            this.colTotalIncorrect,
            this.colCodeExamResult,
            this.colPercentageCorrect,
            this.colCreatedDate,
            this.colUpdatedDate,
            this.colIDDetail,
            this.colEmployeeID,
            this.colCode,
            this.colCodeExam,
            this.colExamTypeText,
            this.colExamType});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colExamTypeText, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvData.Tag = "";
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // colRowNumber
            // 
            this.colRowNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colRowNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRowNumber.Caption = "STT";
            this.colRowNumber.FieldName = "RowNumber";
            this.colRowNumber.Name = "colRowNumber";
            this.colRowNumber.OptionsColumn.AllowIncrementalSearch = false;
            this.colRowNumber.OptionsColumn.ShowInExpressionEditor = false;
            this.colRowNumber.OptionsColumn.TabStop = false;
            this.colRowNumber.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colRowNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "RowNumber", "{0}")});
            this.colRowNumber.Visible = true;
            this.colRowNumber.VisibleIndex = 0;
            this.colRowNumber.Width = 69;
            // 
            // colFullName
            // 
            this.colFullName.Caption = "Tên thí sinh";
            this.colFullName.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 2;
            this.colFullName.Width = 222;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colTotalQuestion
            // 
            this.colTotalQuestion.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalQuestion.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalQuestion.Caption = "Tổng số câu";
            this.colTotalQuestion.FieldName = "TotalQuestion";
            this.colTotalQuestion.Name = "colTotalQuestion";
            this.colTotalQuestion.Visible = true;
            this.colTotalQuestion.VisibleIndex = 4;
            this.colTotalQuestion.Width = 109;
            // 
            // colTotalChosen
            // 
            this.colTotalChosen.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalChosen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalChosen.Caption = "Số câu làm";
            this.colTotalChosen.FieldName = "TotalChosen";
            this.colTotalChosen.Name = "colTotalChosen";
            this.colTotalChosen.Visible = true;
            this.colTotalChosen.VisibleIndex = 5;
            this.colTotalChosen.Width = 109;
            // 
            // colTotalCorrect
            // 
            this.colTotalCorrect.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalCorrect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalCorrect.Caption = "Số câu đúng";
            this.colTotalCorrect.FieldName = "TotalCorrect";
            this.colTotalCorrect.Name = "colTotalCorrect";
            this.colTotalCorrect.Visible = true;
            this.colTotalCorrect.VisibleIndex = 6;
            this.colTotalCorrect.Width = 109;
            // 
            // colTotalIncorrect
            // 
            this.colTotalIncorrect.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalIncorrect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalIncorrect.Caption = "Số câu sai";
            this.colTotalIncorrect.FieldName = "TotalIncorrect";
            this.colTotalIncorrect.Name = "colTotalIncorrect";
            this.colTotalIncorrect.Visible = true;
            this.colTotalIncorrect.VisibleIndex = 7;
            this.colTotalIncorrect.Width = 109;
            // 
            // colCodeExamResult
            // 
            this.colCodeExamResult.Caption = "Mã đề thi";
            this.colCodeExamResult.FieldName = "CodeExam";
            this.colCodeExamResult.Name = "colCodeExamResult";
            this.colCodeExamResult.Visible = true;
            this.colCodeExamResult.VisibleIndex = 3;
            this.colCodeExamResult.Width = 121;
            // 
            // colPercentageCorrect
            // 
            this.colPercentageCorrect.AppearanceCell.Options.UseTextOptions = true;
            this.colPercentageCorrect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPercentageCorrect.Caption = "Điểm";
            this.colPercentageCorrect.FieldName = "PercentageCorrect";
            this.colPercentageCorrect.Name = "colPercentageCorrect";
            this.colPercentageCorrect.Visible = true;
            this.colPercentageCorrect.VisibleIndex = 8;
            this.colPercentageCorrect.Width = 109;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.Caption = "Bắt đầu";
            this.colCreatedDate.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colCreatedDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colCreatedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 9;
            this.colCreatedDate.Width = 150;
            // 
            // colUpdatedDate
            // 
            this.colUpdatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdatedDate.Caption = "Kết thúc";
            this.colUpdatedDate.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colUpdatedDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colUpdatedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdatedDate.FieldName = "UpdatedDate";
            this.colUpdatedDate.Name = "colUpdatedDate";
            this.colUpdatedDate.Visible = true;
            this.colUpdatedDate.VisibleIndex = 10;
            this.colUpdatedDate.Width = 150;
            // 
            // colIDDetail
            // 
            this.colIDDetail.Caption = "gridColumn1";
            this.colIDDetail.FieldName = "ID";
            this.colIDDetail.Name = "colIDDetail";
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.FieldName = "EmployeeId";
            this.colEmployeeID.Name = "colEmployeeID";
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã khoá học";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Width = 101;
            // 
            // colCodeExam
            // 
            this.colCodeExam.Caption = "Mã đề thi";
            this.colCodeExam.FieldName = "CodeExam";
            this.colCodeExam.Name = "colCodeExam";
            this.colCodeExam.Visible = true;
            this.colCodeExam.VisibleIndex = 1;
            this.colCodeExam.Width = 138;
            // 
            // colExamTypeText
            // 
            this.colExamTypeText.Caption = "Loại";
            this.colExamTypeText.FieldName = "ExamTypeText";
            this.colExamTypeText.Name = "colExamTypeText";
            this.colExamTypeText.Visible = true;
            this.colExamTypeText.VisibleIndex = 11;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.toolStripSeparator1,
            this.btnExcel});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1368, 46);
            this.toolStrip2.TabIndex = 171;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::Forms.Properties.Resources.refresh2_16x16;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(82, 43);
            this.btnRefresh.Tag = "";
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(99, 43);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // colExamType
            // 
            this.colExamType.FieldName = "ExamType";
            this.colExamType.Name = "colExamType";
            // 
            // frmHistoryExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 517);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.toolStrip2);
            this.Name = "frmHistoryExam";
            this.Text = "LỊCH SỬ THI";
            this.Load += new System.EventHandler(this.frmHistoryExam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colRowNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalQuestion;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalChosen;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalCorrect;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalIncorrect;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeExamResult;
        private DevExpress.XtraGrid.Columns.GridColumn colPercentageCorrect;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIDDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeExam;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colExamTypeText;
        private DevExpress.XtraGrid.Columns.GridColumn colExamType;
    }
}