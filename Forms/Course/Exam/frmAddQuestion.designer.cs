
namespace BMS
{
    partial class frmAddQuestion
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
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.btnChecked = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.PbImage = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtQuestionText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAnswerNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colAnswerText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRightAnswer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIDRightAnswer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChecked)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // btnChecked
            // 
            this.btnChecked.AutoHeight = false;
            this.btnChecked.Name = "btnChecked";
            this.btnChecked.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveAndClose,
            this.toolStripSeparator3,
            this.btnSave,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1256, 55);
            this.toolStrip1.TabIndex = 28;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndClose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSaveAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(86, 52);
            this.btnSaveAndClose.Tag = "frmExam_NewQuestion";
            this.btnSaveAndClose.Text = "Cất && Đóng";
            this.btnSaveAndClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 52);
            this.btnSave.Tag = "frmExam_NewQuestion";
            this.btnSave.Text = "Cất && Thêm mới";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 55);
            this.toolStripSeparator4.Visible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnBrowser);
            this.groupControl1.Controls.Add(this.PbImage);
            this.groupControl1.Controls.Add(this.label10);
            this.groupControl1.Controls.Add(this.txtQuestionText);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtSTT);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 55);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1256, 463);
            this.groupControl1.TabIndex = 224;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.AutoSize = true;
            this.btnBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowser.Location = new System.Drawing.Point(904, 427);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(342, 30);
            this.btnBrowser.TabIndex = 230;
            this.btnBrowser.Text = "Chọn ảnh";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // PbImage
            // 
            this.PbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbImage.Location = new System.Drawing.Point(904, 57);
            this.PbImage.Name = "PbImage";
            this.PbImage.Size = new System.Drawing.Size(342, 364);
            this.PbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbImage.TabIndex = 229;
            this.PbImage.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(904, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 19);
            this.label10.TabIndex = 228;
            this.label10.Text = "Ảnh";
            // 
            // txtQuestionText
            // 
            this.txtQuestionText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestionText.Location = new System.Drawing.Point(143, 57);
            this.txtQuestionText.Multiline = true;
            this.txtQuestionText.Name = "txtQuestionText";
            this.txtQuestionText.Size = new System.Drawing.Size(755, 400);
            this.txtQuestionText.TabIndex = 225;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 20);
            this.label3.TabIndex = 227;
            this.label3.Text = "STT";
            // 
            // txtSTT
            // 
            this.txtSTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTT.Location = new System.Drawing.Point(143, 25);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(136, 26);
            this.txtSTT.TabIndex = 224;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSTT.ThousandsSeparator = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 226;
            this.label1.Text = "Nội dung câu hỏi";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grdData);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(0, 518);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1256, 206);
            this.groupControl2.TabIndex = 225;
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(2, 23);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.grdData.Size = new System.Drawing.Size(1252, 181);
            this.grdData.TabIndex = 45;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grvData_MouseDown);
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colAnswerNumber,
            this.colDelete,
            this.colAnswerText,
            this.colRightAnswer,
            this.colIDRightAnswer,
            this.colCode});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowMove = false;
            this.colID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colID.OptionsFilter.AllowAutoFilter = false;
            this.colID.OptionsFilter.AllowFilter = false;
            // 
            // colAnswerNumber
            // 
            this.colAnswerNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colAnswerNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAnswerNumber.Caption = "AnswerNumber";
            this.colAnswerNumber.FieldName = "AnswerNumber";
            this.colAnswerNumber.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colAnswerNumber.ImageOptions.Image = global::Forms.Properties.Resources.add_16x16;
            this.colAnswerNumber.MaxWidth = 30;
            this.colAnswerNumber.MinWidth = 50;
            this.colAnswerNumber.Name = "colAnswerNumber";
            this.colAnswerNumber.OptionsColumn.AllowEdit = false;
            this.colAnswerNumber.OptionsColumn.AllowMove = false;
            this.colAnswerNumber.OptionsColumn.AllowSize = false;
            this.colAnswerNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAnswerNumber.OptionsColumn.ShowCaption = false;
            this.colAnswerNumber.OptionsFilter.AllowAutoFilter = false;
            this.colAnswerNumber.OptionsFilter.AllowFilter = false;
            this.colAnswerNumber.Visible = true;
            this.colAnswerNumber.VisibleIndex = 1;
            this.colAnswerNumber.Width = 50;
            // 
            // colDelete
            // 
            this.colDelete.Caption = "Xoá";
            this.colDelete.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colDelete.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            this.colDelete.ImageOptions.Image = global::Forms.Properties.Resources.cancel_16x16;
            this.colDelete.MaxWidth = 30;
            this.colDelete.MinWidth = 50;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowMove = false;
            this.colDelete.OptionsColumn.AllowSize = false;
            this.colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.OptionsFilter.AllowAutoFilter = false;
            this.colDelete.OptionsFilter.AllowFilter = false;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 0;
            this.colDelete.Width = 50;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // colAnswerText
            // 
            this.colAnswerText.Caption = "Nội dung đáp án";
            this.colAnswerText.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colAnswerText.FieldName = "AnswerText";
            this.colAnswerText.Name = "colAnswerText";
            this.colAnswerText.OptionsColumn.AllowMove = false;
            this.colAnswerText.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAnswerText.OptionsFilter.AllowAutoFilter = false;
            this.colAnswerText.OptionsFilter.AllowFilter = false;
            this.colAnswerText.Visible = true;
            this.colAnswerText.VisibleIndex = 3;
            this.colAnswerText.Width = 1072;
            // 
            // colRightAnswer
            // 
            this.colRightAnswer.Caption = "Đáp án đúng";
            this.colRightAnswer.ColumnEdit = this.btnChecked;
            this.colRightAnswer.FieldName = "RightAnswer";
            this.colRightAnswer.MaxWidth = 90;
            this.colRightAnswer.MinWidth = 90;
            this.colRightAnswer.Name = "colRightAnswer";
            this.colRightAnswer.OptionsColumn.AllowMove = false;
            this.colRightAnswer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRightAnswer.OptionsFilter.AllowAutoFilter = false;
            this.colRightAnswer.OptionsFilter.AllowFilter = false;
            this.colRightAnswer.Visible = true;
            this.colRightAnswer.VisibleIndex = 4;
            this.colRightAnswer.Width = 90;
            // 
            // colIDRightAnswer
            // 
            this.colIDRightAnswer.Caption = "IDRightAnswer";
            this.colIDRightAnswer.FieldName = "IDRightAnswer";
            this.colIDRightAnswer.Name = "colIDRightAnswer";
            this.colIDRightAnswer.OptionsColumn.AllowMove = false;
            this.colIDRightAnswer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIDRightAnswer.OptionsFilter.AllowAutoFilter = false;
            this.colIDRightAnswer.OptionsFilter.AllowFilter = false;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã";
            this.colCode.FieldName = "Code";
            this.colCode.MaxWidth = 50;
            this.colCode.MinWidth = 50;
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCode.OptionsFilter.AllowAutoFilter = false;
            this.colCode.OptionsFilter.AllowFilter = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 2;
            this.colCode.Width = 50;
            // 
            // frmAddQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 724);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupControl2);
            this.Name = "frmAddQuestion";
            this.Text = "CHI TIẾT CÂU HỎI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddQuestion_FormClosed);
            this.Load += new System.EventHandler(this.frmAddQuestion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChecked)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveAndClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit btnChecked;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.PictureBox PbImage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtQuestionText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colAnswerNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAnswerText;
        private DevExpress.XtraGrid.Columns.GridColumn colRightAnswer;
        private DevExpress.XtraGrid.Columns.GridColumn colIDRightAnswer;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
    }
}