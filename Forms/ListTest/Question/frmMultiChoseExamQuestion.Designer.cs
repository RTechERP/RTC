
namespace Forms.ListTest.Question
{
    partial class frmMultiChoseExamQuestion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiChoseExamQuestion));
            this.colType_ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkImg = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCorrectAnswer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colContentTest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuestionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grvQuestion = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colScore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdQuestion = new DevExpress.XtraGrid.GridControl();
            this.stackPanel2 = new DevExpress.Utils.Layout.StackPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboType = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblCatCode = new System.Windows.Forms.Label();
            this.lblCatName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.picImageQuestion = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel2)).BeginInit();
            this.stackPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImageQuestion)).BeginInit();
            this.SuspendLayout();
            // 
            // colType_
            // 
            this.colType_.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colType_.AppearanceCell.Options.UseFont = true;
            this.colType_.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colType_.AppearanceHeader.Options.UseFont = true;
            this.colType_.AppearanceHeader.Options.UseForeColor = true;
            this.colType_.AppearanceHeader.Options.UseTextOptions = true;
            this.colType_.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colType_.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colType_.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colType_.Caption = "Loại câu hỏi";
            this.colType_.FieldName = "TypeName";
            this.colType_.Name = "colType_";
            this.colType_.Visible = true;
            this.colType_.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colAdd
            // 
            this.colAdd.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colAdd.AppearanceHeader.Options.UseFont = true;
            this.colAdd.AppearanceHeader.Options.UseForeColor = true;
            this.colAdd.AppearanceHeader.Options.UseTextOptions = true;
            this.colAdd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAdd.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAdd.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colAdd.Caption = "Lấy";
            this.colAdd.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colAdd.Name = "colAdd";
            // 
            // colLinkImg
            // 
            this.colLinkImg.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colLinkImg.AppearanceCell.Options.UseFont = true;
            this.colLinkImg.AppearanceCell.Options.UseTextOptions = true;
            this.colLinkImg.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLinkImg.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colLinkImg.AppearanceHeader.Options.UseFont = true;
            this.colLinkImg.AppearanceHeader.Options.UseForeColor = true;
            this.colLinkImg.AppearanceHeader.Options.UseTextOptions = true;
            this.colLinkImg.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLinkImg.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLinkImg.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLinkImg.Caption = "Link ảnh";
            this.colLinkImg.FieldName = "ImageName";
            this.colLinkImg.Name = "colLinkImg";
            this.colLinkImg.OptionsColumn.AllowEdit = false;
            this.colLinkImg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLinkImg.OptionsFilter.AllowAutoFilter = false;
            this.colLinkImg.OptionsFilter.AllowFilter = false;
            this.colLinkImg.Width = 261;
            // 
            // colCorrectAnswer
            // 
            this.colCorrectAnswer.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colCorrectAnswer.AppearanceCell.Options.UseFont = true;
            this.colCorrectAnswer.AppearanceCell.Options.UseForeColor = true;
            this.colCorrectAnswer.AppearanceCell.Options.UseTextOptions = true;
            this.colCorrectAnswer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCorrectAnswer.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCorrectAnswer.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCorrectAnswer.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colCorrectAnswer.AppearanceHeader.Options.UseFont = true;
            this.colCorrectAnswer.AppearanceHeader.Options.UseForeColor = true;
            this.colCorrectAnswer.AppearanceHeader.Options.UseTextOptions = true;
            this.colCorrectAnswer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCorrectAnswer.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCorrectAnswer.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCorrectAnswer.Caption = "Câu trả lời đúng";
            this.colCorrectAnswer.FieldName = "CorrectAnswer";
            this.colCorrectAnswer.MaxWidth = 70;
            this.colCorrectAnswer.MinWidth = 70;
            this.colCorrectAnswer.Name = "colCorrectAnswer";
            this.colCorrectAnswer.OptionsColumn.AllowEdit = false;
            this.colCorrectAnswer.OptionsColumn.AllowMove = false;
            this.colCorrectAnswer.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colCorrectAnswer.Visible = true;
            this.colCorrectAnswer.VisibleIndex = 2;
            this.colCorrectAnswer.Width = 70;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colContentTest
            // 
            this.colContentTest.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colContentTest.AppearanceCell.Options.UseFont = true;
            this.colContentTest.AppearanceCell.Options.UseForeColor = true;
            this.colContentTest.AppearanceCell.Options.UseTextOptions = true;
            this.colContentTest.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContentTest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContentTest.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colContentTest.AppearanceHeader.Options.UseFont = true;
            this.colContentTest.AppearanceHeader.Options.UseForeColor = true;
            this.colContentTest.AppearanceHeader.Options.UseTextOptions = true;
            this.colContentTest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colContentTest.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colContentTest.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colContentTest.Caption = "Nội dung câu hỏi";
            this.colContentTest.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colContentTest.FieldName = "ContentTest";
            this.colContentTest.Name = "colContentTest";
            this.colContentTest.OptionsColumn.AllowEdit = false;
            this.colContentTest.OptionsColumn.AllowMove = false;
            this.colContentTest.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colContentTest.Visible = true;
            this.colContentTest.VisibleIndex = 1;
            this.colContentTest.Width = 1209;
            // 
            // colSTT
            // 
            this.colSTT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colSTT.AppearanceCell.Options.UseFont = true;
            this.colSTT.AppearanceCell.Options.UseForeColor = true;
            this.colSTT.AppearanceCell.Options.UseTextOptions = true;
            this.colSTT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colSTT.AppearanceHeader.Options.UseFont = true;
            this.colSTT.AppearanceHeader.Options.UseForeColor = true;
            this.colSTT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSTT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSTT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSTT.Caption = "STT";
            this.colSTT.FieldName = "STT";
            this.colSTT.MaxWidth = 50;
            this.colSTT.MinWidth = 50;
            this.colSTT.Name = "colSTT";
            this.colSTT.OptionsColumn.AllowEdit = false;
            this.colSTT.OptionsColumn.AllowMove = false;
            this.colSTT.OptionsColumn.AllowShowHide = false;
            this.colSTT.OptionsColumn.TabStop = false;
            this.colSTT.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colSTT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "STT", "{0}")});
            this.colSTT.Visible = true;
            this.colSTT.VisibleIndex = 0;
            this.colSTT.Width = 50;
            // 
            // colQuestionID
            // 
            this.colQuestionID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colQuestionID.AppearanceCell.Options.UseFont = true;
            this.colQuestionID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colQuestionID.AppearanceHeader.Options.UseFont = true;
            this.colQuestionID.AppearanceHeader.Options.UseForeColor = true;
            this.colQuestionID.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuestionID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuestionID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuestionID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuestionID.Caption = "ID";
            this.colQuestionID.FieldName = "ID";
            this.colQuestionID.Name = "colQuestionID";
            this.colQuestionID.Width = 262;
            // 
            // grvQuestion
            // 
            this.grvQuestion.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.grvQuestion.Appearance.GroupRow.Options.UseFont = true;
            this.grvQuestion.Appearance.GroupRow.Options.UseForeColor = true;
            this.grvQuestion.AutoFillColumn = this.colContentTest;
            this.grvQuestion.ColumnPanelRowHeight = 100;
            this.grvQuestion.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colQuestionID,
            this.colSTT,
            this.colContentTest,
            this.colCorrectAnswer,
            this.colLinkImg,
            this.colAdd,
            this.colScore,
            this.colType_});
            this.grvQuestion.GridControl = this.grdQuestion;
            this.grvQuestion.GroupCount = 1;
            this.grvQuestion.Name = "grvQuestion";
            this.grvQuestion.OptionsCustomization.AllowSort = false;
            this.grvQuestion.OptionsView.RowAutoHeight = true;
            this.grvQuestion.OptionsView.ShowFooter = true;
            this.grvQuestion.OptionsView.ShowGroupPanel = false;
            this.grvQuestion.RowHeight = 35;
            this.grvQuestion.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colType_, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvQuestion.Tag = "";
            this.grvQuestion.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvQuestion_RowCellStyle);
            this.grvQuestion.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.grvQuestion_CustomRowCellEdit);
            this.grvQuestion.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.grvQuestion_CustomSummaryCalculate);
            this.grvQuestion.CustomSummaryExists += new DevExpress.Data.CustomSummaryExistEventHandler(this.grvQuestion_CustomSummaryExists);
            this.grvQuestion.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvQuestion_FocusedRowChanged);
            this.grvQuestion.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvQuestion_CellValueChanged);
            this.grvQuestion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grvQuestion_MouseMove);
            this.grvQuestion.DoubleClick += new System.EventHandler(this.grvQuestion_DoubleClick);
            // 
            // colScore
            // 
            this.colScore.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colScore.AppearanceCell.Options.UseFont = true;
            this.colScore.AppearanceCell.Options.UseTextOptions = true;
            this.colScore.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colScore.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colScore.AppearanceHeader.Options.UseFont = true;
            this.colScore.AppearanceHeader.Options.UseForeColor = true;
            this.colScore.AppearanceHeader.Options.UseTextOptions = true;
            this.colScore.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colScore.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colScore.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colScore.Caption = "Điểm";
            this.colScore.FieldName = "Score";
            this.colScore.MaxWidth = 50;
            this.colScore.MinWidth = 50;
            this.colScore.Name = "colScore";
            this.colScore.OptionsColumn.AllowEdit = false;
            this.colScore.OptionsColumn.ReadOnly = true;
            this.colScore.Visible = true;
            this.colScore.VisibleIndex = 3;
            this.colScore.Width = 50;
            // 
            // grdQuestion
            // 
            this.grdQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdQuestion.Location = new System.Drawing.Point(0, 93);
            this.grdQuestion.MainView = this.grvQuestion;
            this.grdQuestion.Name = "grdQuestion";
            this.grdQuestion.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemMemoEdit1});
            this.grdQuestion.Size = new System.Drawing.Size(1395, 643);
            this.grdQuestion.TabIndex = 197;
            this.grdQuestion.Tag = "";
            this.grdQuestion.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvQuestion});
            // 
            // stackPanel2
            // 
            this.stackPanel2.Controls.Add(this.label1);
            this.stackPanel2.Controls.Add(this.cboType);
            this.stackPanel2.Controls.Add(this.lblCatCode);
            this.stackPanel2.Controls.Add(this.lblCatName);
            this.stackPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackPanel2.Location = new System.Drawing.Point(0, 50);
            this.stackPanel2.Name = "stackPanel2";
            this.stackPanel2.Size = new System.Drawing.Size(1395, 43);
            this.stackPanel2.TabIndex = 196;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 22);
            this.label1.TabIndex = 192;
            this.label1.Text = "Nhóm";
            // 
            // cboType
            // 
            this.cboType.Location = new System.Drawing.Point(68, 9);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.Properties.Appearance.Options.UseFont = true;
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.NullText = "";
            this.cboType.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboType.Size = new System.Drawing.Size(187, 24);
            this.cboType.TabIndex = 0;
            this.cboType.EditValueChanged += new System.EventHandler(this.cboType_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.searchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = true;
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 40;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colTypeCode,
            this.colTypeName,
            this.colGroupName});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colID, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "gridColumn1";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colTypeCode
            // 
            this.colTypeCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colTypeCode.AppearanceCell.Options.UseFont = true;
            this.colTypeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colTypeCode.AppearanceHeader.Options.UseFont = true;
            this.colTypeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colTypeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeCode.Caption = "Mã loại";
            this.colTypeCode.FieldName = "TypeCode";
            this.colTypeCode.Name = "colTypeCode";
            // 
            // colTypeName
            // 
            this.colTypeName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colTypeName.AppearanceCell.Options.UseFont = true;
            this.colTypeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.colTypeName.AppearanceHeader.Options.UseFont = true;
            this.colTypeName.AppearanceHeader.Options.UseForeColor = true;
            this.colTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeName.Caption = "Tên loại";
            this.colTypeName.FieldName = "TypeName";
            this.colTypeName.Name = "colTypeName";
            // 
            // colGroupName
            // 
            this.colGroupName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colGroupName.AppearanceCell.Options.UseFont = true;
            this.colGroupName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.colGroupName.AppearanceHeader.Options.UseFont = true;
            this.colGroupName.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupName.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupName.Caption = "Nhóm";
            this.colGroupName.FieldName = "GroupName";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.Visible = true;
            this.colGroupName.VisibleIndex = 0;
            // 
            // lblCatCode
            // 
            this.lblCatCode.AutoSize = true;
            this.lblCatCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatCode.Location = new System.Drawing.Point(261, 12);
            this.lblCatCode.Name = "lblCatCode";
            this.lblCatCode.Size = new System.Drawing.Size(67, 19);
            this.lblCatCode.TabIndex = 193;
            this.lblCatCode.Text = "Kỳ thi: ";
            // 
            // lblCatName
            // 
            this.lblCatName.AutoSize = true;
            this.lblCatName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatName.Location = new System.Drawing.Point(334, 12);
            this.lblCatName.Name = "lblCatName";
            this.lblCatName.Size = new System.Drawing.Size(91, 19);
            this.lblCatName.TabIndex = 194;
            this.lblCatName.Text = "Tên kỳ thi";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(46, 47);
            this.btnSave.Tag = "frmListTest_Update";
            this.btnSave.Text = "Cất ";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1395, 50);
            this.mnuMenu.TabIndex = 195;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // picImageQuestion
            // 
            this.picImageQuestion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picImageQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImageQuestion.Location = new System.Drawing.Point(25, 490);
            this.picImageQuestion.Name = "picImageQuestion";
            this.picImageQuestion.Size = new System.Drawing.Size(230, 166);
            this.picImageQuestion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImageQuestion.TabIndex = 199;
            this.picImageQuestion.TabStop = false;
            this.picImageQuestion.Visible = false;
            // 
            // frmMultiChoseExamQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 736);
            this.Controls.Add(this.picImageQuestion);
            this.Controls.Add(this.grdQuestion);
            this.Controls.Add(this.stackPanel2);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmMultiChoseExamQuestion";
            this.Text = "THÊM CÂU HỎI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMultiChoseExamQuestion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel2)).EndInit();
            this.stackPanel2.ResumeLayout(false);
            this.stackPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImageQuestion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colType_;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colLinkImg;
        private DevExpress.XtraGrid.Columns.GridColumn colCorrectAnswer;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colContentTest;
        private DevExpress.XtraGrid.Columns.GridColumn colSTT;
        private DevExpress.XtraGrid.Columns.GridColumn colQuestionID;
        private DevExpress.XtraGrid.Views.Grid.GridView grvQuestion;
        private DevExpress.XtraGrid.Columns.GridColumn colScore;
        private DevExpress.XtraGrid.GridControl grdQuestion;
        private DevExpress.Utils.Layout.StackPanel stackPanel2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit cboType;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.Label lblCatCode;
        private System.Windows.Forms.Label lblCatName;
        private System.Windows.Forms.PictureBox picImageQuestion;
    }
}