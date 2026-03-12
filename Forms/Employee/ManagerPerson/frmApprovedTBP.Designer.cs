
namespace BMS
{
    partial class frmApprovedTBP
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
            this.label15 = new System.Windows.Forms.Label();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.txtEvaluateResults = new System.Windows.Forms.TextBox();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEvaluateResults = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayDangKy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoiDung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReasonDeciline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(188, 16);
            this.label15.TabIndex = 13;
            this.label15.Text = "Đánh giá kết quả WFH (nếu có)";
            this.label15.Visible = false;
            // 
            // stackPanel1
            // 
            this.stackPanel1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.stackPanel1.Appearance.Options.UseBackColor = true;
            this.stackPanel1.Controls.Add(this.btnNo);
            this.stackPanel1.Controls.Add(this.btnYes);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            this.stackPanel1.Location = new System.Drawing.Point(0, 550);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(1276, 44);
            this.stackPanel1.TabIndex = 11;
            // 
            // btnNo
            // 
            this.btnNo.AutoSize = true;
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Location = new System.Drawing.Point(1198, 9);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 26);
            this.btnNo.TabIndex = 0;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.AutoSize = true;
            this.btnYes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(1117, 9);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 26);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // txtEvaluateResults
            // 
            this.txtEvaluateResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEvaluateResults.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEvaluateResults.Location = new System.Drawing.Point(12, 28);
            this.txtEvaluateResults.Multiline = true;
            this.txtEvaluateResults.Name = "txtEvaluateResults";
            this.txtEvaluateResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEvaluateResults.Size = new System.Drawing.Size(1252, 36);
            this.txtEvaluateResults.TabIndex = 16;
            this.txtEvaluateResults.Visible = false;
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(12, 12);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(1252, 528);
            this.grdData.TabIndex = 17;
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
            this.grvData.AutoFillColumn = this.colEvaluateResults;
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colStatusText,
            this.colFullName,
            this.colNgayDangKy,
            this.colNoiDung,
            this.colReason,
            this.colEvaluateResults,
            this.colReasonDeciline,
            this.colCreatedDate});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colEvaluateResults
            // 
            this.colEvaluateResults.Caption = "Đánh giá công việc";
            this.colEvaluateResults.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colEvaluateResults.FieldName = "EvaluateResults";
            this.colEvaluateResults.Name = "colEvaluateResults";
            this.colEvaluateResults.Visible = true;
            this.colEvaluateResults.VisibleIndex = 5;
            this.colEvaluateResults.Width = 211;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colID
            // 
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colStatusText
            // 
            this.colStatusText.Caption = "Trạng thái TBP";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.OptionsColumn.AllowEdit = false;
            this.colStatusText.OptionsColumn.ReadOnly = true;
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 0;
            this.colStatusText.Width = 97;
            // 
            // colFullName
            // 
            this.colFullName.Caption = "Họ tên";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.ReadOnly = true;
            this.colFullName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FullName", "{0}")});
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 1;
            this.colFullName.Width = 97;
            // 
            // colNgayDangKy
            // 
            this.colNgayDangKy.AppearanceCell.Options.UseTextOptions = true;
            this.colNgayDangKy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNgayDangKy.Caption = "Ngày";
            this.colNgayDangKy.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colNgayDangKy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayDangKy.FieldName = "NgayDangKy";
            this.colNgayDangKy.Name = "colNgayDangKy";
            this.colNgayDangKy.OptionsColumn.AllowEdit = false;
            this.colNgayDangKy.OptionsColumn.ReadOnly = true;
            this.colNgayDangKy.Visible = true;
            this.colNgayDangKy.VisibleIndex = 2;
            this.colNgayDangKy.Width = 80;
            // 
            // colNoiDung
            // 
            this.colNoiDung.Caption = "Nội dung";
            this.colNoiDung.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNoiDung.FieldName = "NoiDung";
            this.colNoiDung.Name = "colNoiDung";
            this.colNoiDung.OptionsColumn.AllowEdit = false;
            this.colNoiDung.OptionsColumn.ReadOnly = true;
            this.colNoiDung.Visible = true;
            this.colNoiDung.VisibleIndex = 3;
            this.colNoiDung.Width = 264;
            // 
            // colReason
            // 
            this.colReason.Caption = "Lý do";
            this.colReason.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.OptionsColumn.AllowEdit = false;
            this.colReason.OptionsColumn.ReadOnly = true;
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 4;
            this.colReason.Width = 207;
            // 
            // colReasonDeciline
            // 
            this.colReasonDeciline.Caption = "Lý do không duyệt";
            this.colReasonDeciline.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colReasonDeciline.FieldName = "ReasonDeciline";
            this.colReasonDeciline.Name = "colReasonDeciline";
            this.colReasonDeciline.OptionsColumn.AllowEdit = false;
            this.colReasonDeciline.OptionsColumn.ReadOnly = true;
            this.colReasonDeciline.Visible = true;
            this.colReasonDeciline.VisibleIndex = 6;
            this.colReasonDeciline.Width = 189;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCreatedDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colCreatedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.OptionsColumn.AllowEdit = false;
            this.colCreatedDate.OptionsColumn.ReadOnly = true;
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 7;
            this.colCreatedDate.Width = 82;
            // 
            // frmApprovedTBP
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 594);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.stackPanel1);
            this.Controls.Add(this.txtEvaluateResults);
            this.KeyPreview = true;
            this.Name = "frmApprovedTBP";
            this.Text = "ĐÁNH GIÁ CÔNG VIỆC WFH (Nếu có)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmApprovedTBP_FormClosed);
            this.Load += new System.EventHandler(this.frmApprovedTBP_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmApprovedTBP_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label15;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
        public System.Windows.Forms.TextBox txtEvaluateResults;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colEvaluateResults;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayDangKy;
        private DevExpress.XtraGrid.Columns.GridColumn colNoiDung;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraGrid.Columns.GridColumn colReasonDeciline;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        public DevExpress.XtraGrid.GridControl grdData;
    }
}