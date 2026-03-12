
namespace BMS
{
    partial class frmEmployeeBussinessVehicle
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
            this.grdVeihcle = new DevExpress.XtraGrid.GridControl();
            this.grvVeihcle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVehicleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colCostVihicle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colBillImageDS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.colNoteVihicle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeVehicleBussinessID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboVehicle = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.dtpDayBussiness = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtTypeBussiness = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdVeihcle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvVeihcle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdVeihcle
            // 
            this.grdVeihcle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVeihcle.Location = new System.Drawing.Point(12, 94);
            this.grdVeihcle.MainView = this.grvVeihcle;
            this.grdVeihcle.Name = "grdVeihcle";
            this.grdVeihcle.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageEdit1,
            this.repositoryItemMemoEdit1,
            this.cboVehicle,
            this.repositoryItemTextEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemSpinEdit1});
            this.grdVeihcle.Size = new System.Drawing.Size(1103, 533);
            this.grdVeihcle.TabIndex = 0;
            this.grdVeihcle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvVeihcle});
            // 
            // grvVeihcle
            // 
            this.grvVeihcle.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvVeihcle.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvVeihcle.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvVeihcle.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvVeihcle.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvVeihcle.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvVeihcle.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvVeihcle.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvVeihcle.Appearance.Row.Options.UseFont = true;
            this.grvVeihcle.Appearance.Row.Options.UseTextOptions = true;
            this.grvVeihcle.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvVeihcle.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvVeihcle.ColumnPanelRowHeight = 40;
            this.grvVeihcle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVehicleName,
            this.colCostVihicle,
            this.colBillImageDS,
            this.colNoteVihicle,
            this.colEmployeeVehicleBussinessID,
            this.colID,
            this.colBillImage});
            this.grvVeihcle.GridControl = this.grdVeihcle;
            this.grvVeihcle.Name = "grvVeihcle";
            this.grvVeihcle.OptionsFind.AlwaysVisible = true;
            this.grvVeihcle.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvVeihcle.OptionsView.RowAutoHeight = true;
            this.grvVeihcle.OptionsView.ShowFooter = true;
            this.grvVeihcle.OptionsView.ShowGroupPanel = false;
            this.grvVeihcle.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvVeihcle_CellValueChanged);
            this.grvVeihcle.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.grvVeihcle_CustomUnboundColumnData);
            // 
            // colVehicleName
            // 
            this.colVehicleName.Caption = "Tên phương tiện";
            this.colVehicleName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colVehicleName.FieldName = "VehicleName";
            this.colVehicleName.Name = "colVehicleName";
            this.colVehicleName.Visible = true;
            this.colVehicleName.VisibleIndex = 1;
            this.colVehicleName.Width = 236;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colCostVihicle
            // 
            this.colCostVihicle.AppearanceCell.Options.UseTextOptions = true;
            this.colCostVihicle.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCostVihicle.Caption = "Chi phí";
            this.colCostVihicle.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCostVihicle.FieldName = "Cost";
            this.colCostVihicle.Name = "colCostVihicle";
            this.colCostVihicle.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Cost", "SUM={0:N0}")});
            this.colCostVihicle.Visible = true;
            this.colCostVihicle.VisibleIndex = 2;
            this.colCostVihicle.Width = 111;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "c0");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.UseMaskAsDisplayFormat = true;
            // 
            // colBillImageDS
            // 
            this.colBillImageDS.Caption = "Ảnh phương tiện";
            this.colBillImageDS.ColumnEdit = this.repositoryItemImageEdit1;
            this.colBillImageDS.FieldName = "ImageBillVehicle";
            this.colBillImageDS.Name = "colBillImageDS";
            this.colBillImageDS.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colBillImageDS.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colBillImageDS.Visible = true;
            this.colBillImageDS.VisibleIndex = 3;
            this.colBillImageDS.Width = 195;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            this.repositoryItemImageEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // colNoteVihicle
            // 
            this.colNoteVihicle.Caption = "Ghi chú";
            this.colNoteVihicle.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNoteVihicle.FieldName = "Note";
            this.colNoteVihicle.Name = "colNoteVihicle";
            this.colNoteVihicle.Visible = true;
            this.colNoteVihicle.VisibleIndex = 4;
            this.colNoteVihicle.Width = 291;
            // 
            // colEmployeeVehicleBussinessID
            // 
            this.colEmployeeVehicleBussinessID.Caption = "Loại phương tiện";
            this.colEmployeeVehicleBussinessID.ColumnEdit = this.cboVehicle;
            this.colEmployeeVehicleBussinessID.FieldName = "EmployeeVehicleBussinessID";
            this.colEmployeeVehicleBussinessID.Name = "colEmployeeVehicleBussinessID";
            this.colEmployeeVehicleBussinessID.Visible = true;
            this.colEmployeeVehicleBussinessID.VisibleIndex = 0;
            this.colEmployeeVehicleBussinessID.Width = 245;
            // 
            // cboVehicle
            // 
            this.cboVehicle.AutoHeight = false;
            this.cboVehicle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVehicle.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VehicleName", "Tên phương tiện"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Cost", "Chi phí", 20, DevExpress.Utils.FormatType.Numeric, "n0", true, DevExpress.Utils.HorzAlignment.Far, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboVehicle.Name = "cboVehicle";
            this.cboVehicle.NullText = "";
            this.cboVehicle.EditValueChanged += new System.EventHandler(this.cboVehicle_EditValueChanged);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colBillImage
            // 
            this.colBillImage.FieldName = "BillImage";
            this.colBillImage.Name = "colBillImage";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // mnuMenu
            // 
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
            this.mnuMenu.Size = new System.Drawing.Size(1126, 55);
            this.mnuMenu.TabIndex = 209;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 52);
            this.btnSave.Tag = "frmEmployeeNightShift_New";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpDayBussiness
            // 
            this.dtpDayBussiness.Enabled = false;
            this.dtpDayBussiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDayBussiness.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDayBussiness.Location = new System.Drawing.Point(997, 60);
            this.dtpDayBussiness.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDayBussiness.Name = "dtpDayBussiness";
            this.dtpDayBussiness.Size = new System.Drawing.Size(118, 26);
            this.dtpDayBussiness.TabIndex = 224;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(948, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 226;
            this.label2.Text = "Ngày";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(251, 63);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 20);
            this.label6.TabIndex = 220;
            this.label6.Text = "Loại công tác";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 221;
            this.label1.Text = "Họ tên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(582, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 220;
            this.label3.Text = "Địa điểm";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(658, 60);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(285, 26);
            this.txtLocation.TabIndex = 227;
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(73, 60);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(173, 26);
            this.txtFullName.TabIndex = 227;
            // 
            // txtTypeBussiness
            // 
            this.txtTypeBussiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTypeBussiness.Location = new System.Drawing.Point(360, 60);
            this.txtTypeBussiness.Name = "txtTypeBussiness";
            this.txtTypeBussiness.ReadOnly = true;
            this.txtTypeBussiness.Size = new System.Drawing.Size(217, 26);
            this.txtTypeBussiness.TabIndex = 227;
            // 
            // frmEmployeeBussinessVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 639);
            this.Controls.Add(this.txtTypeBussiness);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.dtpDayBussiness);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdVeihcle);
            this.Controls.Add(this.mnuMenu);
            this.KeyPreview = true;
            this.Name = "frmEmployeeBussinessVehicle";
            this.Text = "CHI TIẾT PHƯƠNG TIỆN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmployeeBussinessVehicle_FormClosing);
            this.Load += new System.EventHandler(this.frmEmployeeBussinessVehicle_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmployeeBussinessVehicle_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdVeihcle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvVeihcle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdVeihcle;
        private DevExpress.XtraGrid.Views.Grid.GridView grvVeihcle;
        private DevExpress.XtraGrid.Columns.GridColumn colVehicleName;
        private DevExpress.XtraGrid.Columns.GridColumn colCostVihicle;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteVihicle;
        private DevExpress.XtraGrid.Columns.GridColumn colBillImageDS;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DateTimePicker dtpDayBussiness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtTypeBussiness;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeVehicleBussinessID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboVehicle;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBillImage;
    }
}