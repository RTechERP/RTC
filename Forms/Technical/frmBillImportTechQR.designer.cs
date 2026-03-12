
namespace BMS
{
    partial class frmBillImportTechQR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillImportTechQR));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIsApprovedAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelApprovedAll = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClearData = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBillCode = new System.Windows.Forms.TextBox();
            this.grdBillImportTech = new DevExpress.XtraGrid.GridControl();
            this.grvBillImportTech = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSuplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBillType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colReceiver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colCreatDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWarehouseType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.colDateStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillTypeNewText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBillImportTech)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBillImportTech)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnIsApprovedAll,
            this.toolStripSeparator2,
            this.btnCancelApprovedAll});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1344, 37);
            this.mnuMenu.TabIndex = 119;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // btnIsApprovedAll
            // 
            this.btnIsApprovedAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIsApprovedAll.Image = ((System.Drawing.Image)(resources.GetObject("btnIsApprovedAll.Image")));
            this.btnIsApprovedAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnIsApprovedAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIsApprovedAll.Name = "btnIsApprovedAll";
            this.btnIsApprovedAll.Size = new System.Drawing.Size(129, 33);
            this.btnIsApprovedAll.Tag = "frmBillTechnical_IsApproved";
            this.btnIsApprovedAll.Text = "Nhận tất cả chứng từ";
            this.btnIsApprovedAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnIsApprovedAll.Click += new System.EventHandler(this.btnIsApprovedAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 37);
            // 
            // btnCancelApprovedAll
            // 
            this.btnCancelApprovedAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelApprovedAll.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreview;
            this.btnCancelApprovedAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelApprovedAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelApprovedAll.Name = "btnCancelApprovedAll";
            this.btnCancelApprovedAll.Size = new System.Drawing.Size(154, 33);
            this.btnCancelApprovedAll.Tag = "frmBillTechnical_UnApproved";
            this.btnCancelApprovedAll.Text = "Hủy nhận tất cả chứng từ";
            this.btnCancelApprovedAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelApprovedAll.Click += new System.EventHandler(this.btnCancelApprovedAll_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnClearData);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtBillCode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1344, 36);
            this.panel3.TabIndex = 120;
            // 
            // btnClearData
            // 
            this.btnClearData.Location = new System.Drawing.Point(434, 6);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(132, 24);
            this.btnClearData.TabIndex = 133;
            this.btnClearData.Text = "Xóa dữ liệu trên bảng";
            this.btnClearData.UseVisualStyleBackColor = true;
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new System.Drawing.Point(357, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 24);
            this.btnAdd.TabIndex = 133;
            this.btnAdd.Text = "Tìm kiếm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 132;
            this.label5.Text = "Mã phiếu ";
            // 
            // txtBillCode
            // 
            this.txtBillCode.Location = new System.Drawing.Point(72, 8);
            this.txtBillCode.Name = "txtBillCode";
            this.txtBillCode.Size = new System.Drawing.Size(279, 20);
            this.txtBillCode.TabIndex = 125;
            // 
            // grdBillImportTech
            // 
            this.grdBillImportTech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBillImportTech.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdBillImportTech.Location = new System.Drawing.Point(0, 73);
            this.grdBillImportTech.MainView = this.grvBillImportTech;
            this.grdBillImportTech.Margin = new System.Windows.Forms.Padding(2);
            this.grdBillImportTech.Name = "grdBillImportTech";
            this.grdBillImportTech.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemImageEdit2,
            this.repositoryItemMemoEdit3,
            this.repositoryItemMemoEdit4,
            this.repositoryItemMemoEdit5});
            this.grdBillImportTech.Size = new System.Drawing.Size(1344, 543);
            this.grdBillImportTech.TabIndex = 121;
            this.grdBillImportTech.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBillImportTech});
            // 
            // grvBillImportTech
            // 
            this.grvBillImportTech.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvBillImportTech.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvBillImportTech.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvBillImportTech.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvBillImportTech.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvBillImportTech.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvBillImportTech.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvBillImportTech.Appearance.Row.Options.UseTextOptions = true;
            this.grvBillImportTech.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvBillImportTech.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvBillImportTech.AutoFillColumn = this.colSuplier;
            this.grvBillImportTech.ColumnPanelRowHeight = 40;
            this.grvBillImportTech.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatus,
            this.colBillType,
            this.colSuplier,
            this.colDeliver,
            this.colReceiver,
            this.colCreatDate,
            this.colWarehouseType,
            this.colID,
            this.gridColumn10,
            this.colBillCode,
            this.colImage,
            this.colDateStatus,
            this.colDepartmentName,
            this.colCustomerName,
            this.colBillTypeNewText});
            this.grvBillImportTech.DetailHeight = 451;
            this.grvBillImportTech.GridControl = this.grdBillImportTech;
            this.grvBillImportTech.Name = "grvBillImportTech";
            this.grvBillImportTech.OptionsBehavior.Editable = false;
            this.grvBillImportTech.OptionsBehavior.ReadOnly = true;
            this.grvBillImportTech.OptionsView.ColumnAutoWidth = false;
            this.grvBillImportTech.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvBillImportTech.OptionsView.RowAutoHeight = true;
            this.grvBillImportTech.OptionsView.ShowAutoFilterRow = true;
            this.grvBillImportTech.OptionsView.ShowGroupPanel = false;
            this.grvBillImportTech.DoubleClick += new System.EventHandler(this.grvBillImportTech_DoubleClick);
            // 
            // colSuplier
            // 
            this.colSuplier.Caption = "Nhà cung cấp";
            this.colSuplier.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colSuplier.FieldName = "NCC";
            this.colSuplier.MinWidth = 28;
            this.colSuplier.Name = "colSuplier";
            this.colSuplier.OptionsColumn.AllowEdit = false;
            this.colSuplier.Visible = true;
            this.colSuplier.VisibleIndex = 4;
            this.colSuplier.Width = 322;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.Caption = "Nhận chứng từ";
            this.colStatus.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colStatus.FieldName = "Status";
            this.colStatus.MinWidth = 28;
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.AllowEdit = false;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 0;
            this.colStatus.Width = 73;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colBillType
            // 
            this.colBillType.Caption = "Trạng thái";
            this.colBillType.FieldName = "BillType";
            this.colBillType.MinWidth = 28;
            this.colBillType.Name = "colBillType";
            this.colBillType.Width = 123;
            // 
            // colDeliver
            // 
            this.colDeliver.Caption = "Người giao";
            this.colDeliver.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colDeliver.FieldName = "Deliver";
            this.colDeliver.MinWidth = 28;
            this.colDeliver.Name = "colDeliver";
            this.colDeliver.OptionsColumn.AllowEdit = false;
            this.colDeliver.Visible = true;
            this.colDeliver.VisibleIndex = 7;
            this.colDeliver.Width = 318;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colReceiver
            // 
            this.colReceiver.Caption = "Người nhận";
            this.colReceiver.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colReceiver.FieldName = "Receiver";
            this.colReceiver.MinWidth = 28;
            this.colReceiver.Name = "colReceiver";
            this.colReceiver.OptionsColumn.AllowEdit = false;
            this.colReceiver.Visible = true;
            this.colReceiver.VisibleIndex = 8;
            this.colReceiver.Width = 334;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // colCreatDate
            // 
            this.colCreatDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatDate.Caption = "Ngày tạo";
            this.colCreatDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colCreatDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreatDate.FieldName = "CreatDate";
            this.colCreatDate.MinWidth = 28;
            this.colCreatDate.Name = "colCreatDate";
            this.colCreatDate.OptionsColumn.AllowEdit = false;
            this.colCreatDate.Visible = true;
            this.colCreatDate.VisibleIndex = 9;
            this.colCreatDate.Width = 256;
            // 
            // colWarehouseType
            // 
            this.colWarehouseType.Caption = "Loại kho";
            this.colWarehouseType.ColumnEdit = this.repositoryItemMemoEdit5;
            this.colWarehouseType.FieldName = "WarehouseType";
            this.colWarehouseType.MinWidth = 28;
            this.colWarehouseType.Name = "colWarehouseType";
            this.colWarehouseType.OptionsColumn.AllowEdit = false;
            this.colWarehouseType.Visible = true;
            this.colWarehouseType.VisibleIndex = 10;
            this.colWarehouseType.Width = 288;
            // 
            // repositoryItemMemoEdit5
            // 
            this.repositoryItemMemoEdit5.Name = "repositoryItemMemoEdit5";
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 28;
            this.colID.Name = "colID";
            this.colID.Width = 151;
            // 
            // gridColumn10
            // 
            this.gridColumn10.FieldName = "Status";
            this.gridColumn10.MinWidth = 28;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Width = 159;
            // 
            // colBillCode
            // 
            this.colBillCode.Caption = "Mã phiếu nhập";
            this.colBillCode.FieldName = "BillCode";
            this.colBillCode.MinWidth = 28;
            this.colBillCode.Name = "colBillCode";
            this.colBillCode.OptionsColumn.AllowEdit = false;
            this.colBillCode.Visible = true;
            this.colBillCode.VisibleIndex = 3;
            this.colBillCode.Width = 245;
            // 
            // colImage
            // 
            this.colImage.Caption = "Ảnh";
            this.colImage.ColumnEdit = this.repositoryItemImageEdit2;
            this.colImage.FieldName = "Image";
            this.colImage.MinWidth = 28;
            this.colImage.Name = "colImage";
            this.colImage.Width = 157;
            // 
            // repositoryItemImageEdit2
            // 
            this.repositoryItemImageEdit2.AutoHeight = false;
            this.repositoryItemImageEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit2.Name = "repositoryItemImageEdit2";
            // 
            // colDateStatus
            // 
            this.colDateStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colDateStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStatus.Caption = "Ngày nhận";
            this.colDateStatus.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateStatus.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateStatus.FieldName = "DateStatus";
            this.colDateStatus.MinWidth = 29;
            this.colDateStatus.Name = "colDateStatus";
            this.colDateStatus.Visible = true;
            this.colDateStatus.VisibleIndex = 1;
            this.colDateStatus.Width = 147;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.MinWidth = 28;
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 6;
            this.colDepartmentName.Width = 235;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Khách hàng";
            this.colCustomerName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.MinWidth = 29;
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 5;
            this.colCustomerName.Width = 310;
            // 
            // colBillTypeNewText
            // 
            this.colBillTypeNewText.Caption = "Loại phiếu";
            this.colBillTypeNewText.FieldName = "BillTypeNewText";
            this.colBillTypeNewText.MinWidth = 29;
            this.colBillTypeNewText.Name = "colBillTypeNewText";
            this.colBillTypeNewText.Visible = true;
            this.colBillTypeNewText.VisibleIndex = 2;
            this.colBillTypeNewText.Width = 239;
            // 
            // frmBillImportTechQR
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 616);
            this.Controls.Add(this.grdBillImportTech);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.mnuMenu);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmBillImportTechQR";
            this.Text = "THÔNG TIN PHIẾU NHẬP DEMO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBillImportTechQR_FormClosed);
            this.Load += new System.EventHandler(this.frmBillImportTechQR_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBillImportTechQR_KeyDown);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBillImportTech)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBillImportTech)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnIsApprovedAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCancelApprovedAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBillCode;
        private DevExpress.XtraGrid.GridControl grdBillImportTech;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBillImportTech;
        private DevExpress.XtraGrid.Columns.GridColumn colSuplier;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBillType;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliver;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiver;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatDate;
        private DevExpress.XtraGrid.Columns.GridColumn colWarehouseType;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn colBillCode;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colDateStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colBillTypeNewText;
    }
}