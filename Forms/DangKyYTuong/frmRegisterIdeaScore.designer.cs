
namespace BMS
{
    partial class frmRegisterIdeaScore
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
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIdeaName = new System.Windows.Forms.TextBox();
            this.chkIsTBP = new DevExpress.XtraEditors.CheckEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDepartment = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colScoreNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTBPName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTBPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApprovedTBP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateApprovedTBP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBGDName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboScore = new DevExpress.XtraEditors.ComboBoxEdit();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveCLose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTBP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboScore.Properties)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 161;
            this.label1.Text = "Phòng ban liên quan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 164;
            this.label5.Text = "Tên ý tưởng";
            // 
            // txtIdeaName
            // 
            this.txtIdeaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdeaName.Location = new System.Drawing.Point(96, 3);
            this.txtIdeaName.Name = "txtIdeaName";
            this.txtIdeaName.Size = new System.Drawing.Size(178, 22);
            this.txtIdeaName.TabIndex = 163;
            // 
            // chkIsTBP
            // 
            this.chkIsTBP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsTBP.Location = new System.Drawing.Point(747, 6);
            this.chkIsTBP.Name = "chkIsTBP";
            this.chkIsTBP.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsTBP.Properties.Appearance.Options.UseFont = true;
            this.chkIsTBP.Properties.AutoWidth = true;
            this.chkIsTBP.Properties.Caption = "TBP";
            this.chkIsTBP.Size = new System.Drawing.Size(50, 20);
            this.chkIsTBP.TabIndex = 165;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(606, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 167;
            this.label2.Text = "Điểm số";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Location = new System.Drawing.Point(415, 3);
            this.cboDepartment.Margin = new System.Windows.Forms.Padding(2);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Size = new System.Drawing.Size(186, 22);
            this.cboDepartment.TabIndex = 168;
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Location = new System.Drawing.Point(0, 89);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemLookUpEdit1});
            this.grdData.Size = new System.Drawing.Size(809, 334);
            this.grdData.TabIndex = 169;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
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
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colEmployeeID,
            this.colDepartmentName,
            this.colScoreNew,
            this.colTBPName,
            this.colTBPID,
            this.colIsApprovedTBP,
            this.colDateApprovedTBP,
            this.colApprovedID,
            this.colBGDName,
            this.colIsApproved,
            this.colDateApproved,
            this.colNote});
            this.grvData.DetailHeight = 231;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsFind.ShowFindButton = false;
            this.grvData.OptionsFind.ShowSearchNavButtons = false;
            this.grvData.OptionsPrint.PrintHeader = false;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "gridColumn1";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 28;
            this.colID.Name = "colID";
            this.colID.Width = 52;
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.Caption = "EmployeeID";
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.MinWidth = 11;
            this.colEmployeeID.Name = "colEmployeeID";
            this.colEmployeeID.OptionsColumn.AllowEdit = false;
            this.colEmployeeID.Width = 61;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.MinWidth = 15;
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 0;
            this.colDepartmentName.Width = 313;
            // 
            // colScoreNew
            // 
            this.colScoreNew.AppearanceCell.Options.UseTextOptions = true;
            this.colScoreNew.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colScoreNew.Caption = "Điểm số";
            this.colScoreNew.FieldName = "ScoreNew";
            this.colScoreNew.MinWidth = 15;
            this.colScoreNew.Name = "colScoreNew";
            this.colScoreNew.Visible = true;
            this.colScoreNew.VisibleIndex = 2;
            this.colScoreNew.Width = 152;
            // 
            // colTBPName
            // 
            this.colTBPName.Caption = "Trưởng bộ phận";
            this.colTBPName.FieldName = "TBPName";
            this.colTBPName.MinWidth = 15;
            this.colTBPName.Name = "colTBPName";
            this.colTBPName.OptionsColumn.AllowEdit = false;
            this.colTBPName.Visible = true;
            this.colTBPName.VisibleIndex = 1;
            this.colTBPName.Width = 319;
            // 
            // colTBPID
            // 
            this.colTBPID.Caption = "TBPID";
            this.colTBPID.FieldName = "ApprovedTBPID";
            this.colTBPID.MinWidth = 15;
            this.colTBPID.Name = "colTBPID";
            this.colTBPID.Width = 45;
            // 
            // colIsApprovedTBP
            // 
            this.colIsApprovedTBP.Caption = "TBP Duyệt";
            this.colIsApprovedTBP.FieldName = "IsApprovedTBP";
            this.colIsApprovedTBP.MinWidth = 11;
            this.colIsApprovedTBP.Name = "colIsApprovedTBP";
            this.colIsApprovedTBP.OptionsColumn.AllowEdit = false;
            this.colIsApprovedTBP.Width = 70;
            // 
            // colDateApprovedTBP
            // 
            this.colDateApprovedTBP.Caption = "Ngày TBP duyệt";
            this.colDateApprovedTBP.FieldName = "DateApprovedTBP";
            this.colDateApprovedTBP.MinWidth = 11;
            this.colDateApprovedTBP.Name = "colDateApprovedTBP";
            this.colDateApprovedTBP.OptionsColumn.AllowEdit = false;
            this.colDateApprovedTBP.Width = 187;
            // 
            // colApprovedID
            // 
            this.colApprovedID.Caption = "ApprovedID";
            this.colApprovedID.FieldName = "ApprovedID";
            this.colApprovedID.MinWidth = 15;
            this.colApprovedID.Name = "colApprovedID";
            this.colApprovedID.Width = 41;
            // 
            // colBGDName
            // 
            this.colBGDName.Caption = "Ban giám đốc";
            this.colBGDName.FieldName = "BGDName";
            this.colBGDName.MinWidth = 15;
            this.colBGDName.Name = "colBGDName";
            this.colBGDName.OptionsColumn.AllowEdit = false;
            this.colBGDName.Width = 187;
            // 
            // colIsApproved
            // 
            this.colIsApproved.Caption = "BGĐ duyệt ";
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.MinWidth = 15;
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.OptionsColumn.AllowEdit = false;
            this.colIsApproved.Width = 82;
            // 
            // colDateApproved
            // 
            this.colDateApproved.Caption = "Ngày BGĐ duyệt";
            this.colDateApproved.FieldName = "DateApproved";
            this.colDateApproved.MinWidth = 15;
            this.colDateApproved.Name = "colDateApproved";
            this.colDateApproved.OptionsColumn.AllowEdit = false;
            this.colDateApproved.Width = 172;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.MinWidth = 11;
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.Width = 267;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.cboDepartment);
            this.panelControl1.Controls.Add(this.txtIdeaName);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.chkIsTBP);
            this.panelControl1.Controls.Add(this.cboScore);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 55);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(809, 34);
            this.panelControl1.TabIndex = 170;
            // 
            // cboScore
            // 
            this.cboScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboScore.EditValue = "";
            this.cboScore.Location = new System.Drawing.Point(669, 5);
            this.cboScore.Name = "cboScore";
            this.cboScore.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboScore.Properties.Appearance.Options.UseFont = true;
            this.cboScore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboScore.Properties.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D"});
            this.cboScore.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboScore.Size = new System.Drawing.Size(72, 22);
            this.cboScore.TabIndex = 166;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveNew,
            this.toolStripSeparator2,
            this.btnSaveCLose});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(809, 55);
            this.toolStrip1.TabIndex = 171;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.AutoSize = false;
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSaveNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(60, 52);
            this.btnSaveNew.Text = "Cất";
            this.btnSaveNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveCLose
            // 
            this.btnSaveCLose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCLose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSaveCLose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveCLose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCLose.Name = "btnSaveCLose";
            this.btnSaveCLose.Size = new System.Drawing.Size(87, 52);
            this.btnSaveCLose.Text = "Cất && Đóng";
            this.btnSaveCLose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveCLose.Click += new System.EventHandler(this.btnSaveCLose_Click);
            // 
            // frmRegisterIdeaScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 423);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRegisterIdeaScore";
            this.Text = "CHẤM ĐIỂM Ý TƯỞNG";
            this.Load += new System.EventHandler(this.frmRegisterIdeaScore_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTBP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboScore.Properties)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIdeaName;
        private DevExpress.XtraEditors.CheckEdit chkIsTBP;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cboDepartment;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colScoreNew;
        private DevExpress.XtraGrid.Columns.GridColumn colTBPName;
        private DevExpress.XtraGrid.Columns.GridColumn colTBPID;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApprovedTBP;
        private DevExpress.XtraGrid.Columns.GridColumn colDateApprovedTBP;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedID;
        private DevExpress.XtraGrid.Columns.GridColumn colBGDName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colDateApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveCLose;
        private DevExpress.XtraEditors.ComboBoxEdit cboScore;
        private System.Windows.Forms.ToolStripButton btnSaveNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}