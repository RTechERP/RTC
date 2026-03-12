
namespace BMS
{
    partial class frmProjectPriority
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
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnDeleteOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.TrListData = new DevExpress.XtraTreeList.TreeList();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProjectCheckpoint = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colScore = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPriority = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalScore = new System.Windows.Forms.TextBox();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrListData)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator2,
            this.btnUpdate,
            this.toolStripSeparator5,
            this.btnDelete,
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1174, 55);
            this.mnuMenu.TabIndex = 26;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Forms.Properties.Resources.AddFile_32x32;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 52);
            this.btnAdd.Tag = "frmProjectPriority_Add";
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = false;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Image = global::Forms.Properties.Resources.Edit_32x321;
            this.btnUpdate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 52);
            this.btnUpdate.Tag = "frmProjectPriority_Update";
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 52);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDeleteOnly,
            this.btnDeleteAll});
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Forms.Properties.Resources.DeleteList2_32x32;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 52);
            this.btnDelete.Tag = "frmProjectPriority_Delete";
            this.btnDelete.Text = "Xoá";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeleteOnly
            // 
            this.btnDeleteOnly.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteOnly.Name = "btnDeleteOnly";
            this.btnDeleteOnly.Size = new System.Drawing.Size(125, 22);
            this.btnDeleteOnly.Tag = "frmProjectPriority_Delete";
            this.btnDeleteOnly.Text = "Xóa 1";
            this.btnDeleteOnly.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnDeleteOnly.Click += new System.EventHandler(this.btnDeleteOnly_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(125, 22);
            this.btnDeleteAll.Tag = "frmProjectPriority_Delete";
            this.btnDeleteAll.Text = "Xóa tất";
            this.btnDeleteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.Apply_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 52);
            this.btnSave.Tag = "frmProject_Update";
            this.btnSave.Text = "Lưu";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TrListData
            // 
            this.TrListData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrListData.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.TrListData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.TrListData.Appearance.HeaderPanel.Options.UseFont = true;
            this.TrListData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.TrListData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.TrListData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TrListData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TrListData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TrListData.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrListData.Appearance.Row.Options.UseFont = true;
            this.TrListData.Appearance.Row.Options.UseTextOptions = true;
            this.TrListData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TrListData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TrListData.ColumnPanelRowHeight = 40;
            this.TrListData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colID,
            this.colCode,
            this.colProjectCheckpoint,
            this.colRate,
            this.colScore,
            this.colPriority,
            this.colParentID});
            this.TrListData.CustomizationFormBounds = new System.Drawing.Rectangle(922, 431, 252, 296);
            this.TrListData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrListData.Location = new System.Drawing.Point(0, 55);
            this.TrListData.Name = "TrListData";
            this.TrListData.OptionsSelection.MultiSelect = true;
            this.TrListData.OptionsSelection.UseIndicatorForSelection = true;
            this.TrListData.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.TrListData.OptionsView.RootCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            this.TrListData.Size = new System.Drawing.Size(1174, 533);
            this.TrListData.TabIndex = 28;
            this.TrListData.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.TrListData_NodeCellStyle);
            this.TrListData.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.TrListData_AfterCheckNode);
            this.TrListData.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.TrListData_FocusedNodeChanged);
            this.TrListData.CustomColumnDisplayText += new DevExpress.XtraTreeList.CustomColumnDisplayTextEventHandler(this.TrListData_CustomColumnDisplayText);
            this.TrListData.DoubleClick += new System.EventHandler(this.btnUpdate_Click);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã ưu tiên";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // colProjectCheckpoint
            // 
            this.colProjectCheckpoint.Caption = "Checkpoint";
            this.colProjectCheckpoint.FieldName = "ProjectCheckpoint";
            this.colProjectCheckpoint.Name = "colProjectCheckpoint";
            this.colProjectCheckpoint.OptionsColumn.AllowEdit = false;
            this.colProjectCheckpoint.Visible = true;
            this.colProjectCheckpoint.VisibleIndex = 1;
            // 
            // colRate
            // 
            this.colRate.Caption = "Trọng số";
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.AllowEdit = false;
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 2;
            // 
            // colScore
            // 
            this.colScore.Caption = "Điểm";
            this.colScore.FieldName = "Score";
            this.colScore.Name = "colScore";
            this.colScore.OptionsColumn.AllowEdit = false;
            this.colScore.Visible = true;
            this.colScore.VisibleIndex = 3;
            // 
            // colPriority
            // 
            this.colPriority.Caption = "Độ ưu tiên";
            this.colPriority.FieldName = "Priority";
            this.colPriority.Name = "colPriority";
            this.colPriority.OptionsColumn.AllowEdit = false;
            this.colPriority.Visible = true;
            this.colPriority.VisibleIndex = 4;
            // 
            // colParentID
            // 
            this.colParentID.Caption = "ParentID";
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            this.colParentID.OptionsColumn.AllowEdit = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(874, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 20);
            this.label7.TabIndex = 82;
            this.label7.Text = "Tổng điểm ưu tiên";
            // 
            // txtTotalScore
            // 
            this.txtTotalScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalScore.Location = new System.Drawing.Point(1015, 19);
            this.txtTotalScore.Name = "txtTotalScore";
            this.txtTotalScore.ReadOnly = true;
            this.txtTotalScore.Size = new System.Drawing.Size(145, 26);
            this.txtTotalScore.TabIndex = 83;
            // 
            // frmProjectPriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 588);
            this.Controls.Add(this.txtTotalScore);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TrListData);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmProjectPriority";
            this.Text = "Ưu tiên dự án";
            this.Load += new System.EventHandler(this.frmProjectPriority_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrListData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private DevExpress.XtraTreeList.TreeList TrListData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectCheckpoint;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colScore;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPriority;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private System.Windows.Forms.ToolStripDropDownButton btnDelete;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteOnly;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteAll;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalScore;
    }
}