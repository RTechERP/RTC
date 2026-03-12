
namespace BMS
{
    partial class frmOfficeSupplyRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOfficeSupplyRequest));
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPassQuantity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDisapprove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportExcelOld = new System.Windows.Forms.ToolStripButton();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.lblFilterText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpMonthPicker = new System.Windows.Forms.DateTimePicker();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblDate = new System.Windows.Forms.Label();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfficeSupplyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfficeSupplyUnitID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOfficeSupplyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityReceived = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceived = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkReceived = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colUserIDReceive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateRequest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExceedsLimit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullNameApproved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceived)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(84, 37);
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Image = global::Forms.Properties.Resources.Trash_16x16;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(39, 37);
            this.btnDelete.Tag = "frmOfficeSupplyRequest_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(39, 37);
            this.btnEdit.Tag = "frmOfficeSupplyRequest_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 41);
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(51, 37);
            this.btnNew.Tag = "frmOfficeSupplyRequest_New";
            this.btnNew.Text = "&Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator6,
            this.btnEdit,
            this.toolStripSeparator2,
            this.btnDelete,
            this.toolStripSeparator7,
            this.btnPassQuantity,
            this.toolStripSeparator3,
            this.btnApprove,
            this.toolStripSeparator4,
            this.btnDisapprove,
            this.toolStripSeparator5,
            this.btnImportExcel,
            this.toolStripSeparator1,
            this.btnExportExcel,
            this.btnImportExcelOld});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1305, 56);
            this.mnuMenu.TabIndex = 23;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.AutoSize = false;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 41);
            // 
            // btnPassQuantity
            // 
            this.btnPassQuantity.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnPassQuantity.Image = global::Forms.Properties.Resources.Convert_16x16;
            this.btnPassQuantity.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPassQuantity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPassQuantity.Name = "btnPassQuantity";
            this.btnPassQuantity.Size = new System.Drawing.Size(112, 37);
            this.btnPassQuantity.Tag = "frmOfficeSupplyRequest_PassQuantity";
            this.btnPassQuantity.Text = "Copy số lượng";
            this.btnPassQuantity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPassQuantity.ToolTipText = "Chuyển số lượng đề xuất sang số lượng thực nhận";
            this.btnPassQuantity.Click += new System.EventHandler(this.btnPassQuantity_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 41);
            // 
            // btnApprove
            // 
            this.btnApprove.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnApprove.Image = global::Forms.Properties.Resources.Apply_16x16;
            this.btnApprove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(84, 37);
            this.btnApprove.Tag = "frmOfficeSupplyRequest_Approve";
            this.btnApprove.Text = "TBP duyệt";
            this.btnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprove.ToolTipText = "Ký nhận";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 41);
            // 
            // btnDisapprove
            // 
            this.btnDisapprove.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnDisapprove.Image = global::Forms.Properties.Resources.icons8_close_16;
            this.btnDisapprove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDisapprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisapprove.Name = "btnDisapprove";
            this.btnDisapprove.Size = new System.Drawing.Size(114, 37);
            this.btnDisapprove.Tag = "frmOfficeSupplyRequest_Disapprove";
            this.btnDisapprove.Text = "TBP hủy duyệt";
            this.btnDisapprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDisapprove.ToolTipText = "Hủy ký nhận";
            this.btnDisapprove.Click += new System.EventHandler(this.btnDisapprove_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 41);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(91, 36);
            this.btnImportExcel.Tag = "frmOfficeSupplyRequest_ImportExcel";
            this.btnImportExcel.Text = "Nhập Excel";
            this.btnImportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // btnImportExcelOld
            // 
            this.btnImportExcelOld.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnImportExcelOld.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ExportXlsLarge;
            this.btnImportExcelOld.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImportExcelOld.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportExcelOld.Name = "btnImportExcelOld";
            this.btnImportExcelOld.Size = new System.Drawing.Size(88, 53);
            this.btnImportExcelOld.Tag = "frmOfficeSupplyRequest_ImportExcel";
            this.btnImportExcelOld.Text = "Nhập Excel";
            this.btnImportExcelOld.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportExcelOld.Visible = false;
            this.btnImportExcelOld.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnFind.Location = new System.Drawing.Point(689, 15);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(72, 27);
            this.btnFind.TabIndex = 202;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFilterText.Location = new System.Drawing.Point(482, 16);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(201, 24);
            this.txtFilterText.TabIndex = 201;
            this.txtFilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterText_KeyDown);
            // 
            // lblFilterText
            // 
            this.lblFilterText.AutoSize = true;
            this.lblFilterText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblFilterText.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblFilterText.Location = new System.Drawing.Point(417, 20);
            this.lblFilterText.Name = "lblFilterText";
            this.lblFilterText.Size = new System.Drawing.Size(59, 17);
            this.lblFilterText.TabIndex = 200;
            this.lblFilterText.Text = "Từ khóa";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpMonthPicker);
            this.panel1.Controls.Add(this.lblDepartment);
            this.panel1.Controls.Add(this.cboDepartment);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.txtFilterText);
            this.panel1.Controls.Add(this.lblFilterText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1305, 47);
            this.panel1.TabIndex = 24;
            // 
            // dtpMonthPicker
            // 
            this.dtpMonthPicker.CustomFormat = "MM";
            this.dtpMonthPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMonthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonthPicker.Location = new System.Drawing.Point(64, 17);
            this.dtpMonthPicker.Name = "dtpMonthPicker";
            this.dtpMonthPicker.Size = new System.Drawing.Size(108, 22);
            this.dtpMonthPicker.TabIndex = 209;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Location = new System.Drawing.Point(178, 20);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(73, 16);
            this.lblDepartment.TabIndex = 207;
            this.lblDepartment.Text = "Phòng ban";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Location = new System.Drawing.Point(257, 17);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDepartment.Size = new System.Drawing.Size(154, 22);
            this.cboDepartment.TabIndex = 208;
            this.cboDepartment.EditValueChanged += new System.EventHandler(this.cboDepartment_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 40;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "Mã phòng ban";
            this.gridColumn2.FieldName = "Code";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 298;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Tên phòng ban";
            this.gridColumn3.FieldName = "Name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 789;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblDate.Location = new System.Drawing.Point(10, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(47, 17);
            this.lblDate.TabIndex = 205;
            this.lblDate.Text = "Tháng";
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(36, 39, 36, 39);
            this.grdData.Location = new System.Drawing.Point(0, 103);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(36, 39, 36, 39);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkReceived});
            this.grdData.Size = new System.Drawing.Size(1305, 405);
            this.grdData.TabIndex = 25;
            this.grdData.Tag = "";
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
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colUserID,
            this.colOfficeSupplyID,
            this.colOfficeSupplyUnitID,
            this.colUserName,
            this.colOfficeSupplyName,
            this.colUnit,
            this.colQuantity,
            this.colQuantityReceived,
            this.colNote,
            this.colReceived,
            this.colUserIDReceive,
            this.colDateRequest,
            this.colExceedsLimit,
            this.colReason,
            this.colIsApproved,
            this.colApprovedID,
            this.colDateApproved,
            this.colFullNameApproved,
            this.colDepartmentID,
            this.colDepartmentName});
            this.grvData.DetailHeight = 4125;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsSelection.ShowCheckBoxSelectorInPrintExport = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsView.ShowAutoFilterRow = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.Tag = "";
            this.grvData.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvData_RowStyle);
            this.grvData.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grvData_PopupMenuShowing);
            this.grvData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            this.grvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grvData_MouseDown);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 217;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.Width = 217;
            // 
            // colUserID
            // 
            this.colUserID.Caption = "ID nhân viên";
            this.colUserID.FieldName = "UserID";
            this.colUserID.MinWidth = 23;
            this.colUserID.Name = "colUserID";
            this.colUserID.Width = 88;
            // 
            // colOfficeSupplyID
            // 
            this.colOfficeSupplyID.Caption = "ID văn phòng phẩm";
            this.colOfficeSupplyID.FieldName = "OfficeSupplyID";
            this.colOfficeSupplyID.MinWidth = 23;
            this.colOfficeSupplyID.Name = "colOfficeSupplyID";
            this.colOfficeSupplyID.Width = 88;
            // 
            // colOfficeSupplyUnitID
            // 
            this.colOfficeSupplyUnitID.Caption = "ID đơn vị tính";
            this.colOfficeSupplyUnitID.FieldName = "OfficeSupplyUnitID";
            this.colOfficeSupplyUnitID.MinWidth = 23;
            this.colOfficeSupplyUnitID.Name = "colOfficeSupplyUnitID";
            this.colOfficeSupplyUnitID.Width = 88;
            // 
            // colUserName
            // 
            this.colUserName.Caption = "Người đăng Ký";
            this.colUserName.FieldName = "UserName";
            this.colUserName.MinWidth = 23;
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 4;
            this.colUserName.Width = 154;
            // 
            // colOfficeSupplyName
            // 
            this.colOfficeSupplyName.Caption = "Văn phòng phẩm";
            this.colOfficeSupplyName.FieldName = "OfficeSupplyName";
            this.colOfficeSupplyName.MinWidth = 23;
            this.colOfficeSupplyName.Name = "colOfficeSupplyName";
            this.colOfficeSupplyName.OptionsColumn.AllowEdit = false;
            this.colOfficeSupplyName.Visible = true;
            this.colOfficeSupplyName.VisibleIndex = 7;
            this.colOfficeSupplyName.Width = 160;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "ĐVT";
            this.colUnit.FieldName = "Unit";
            this.colUnit.MinWidth = 23;
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 8;
            this.colUnit.Width = 61;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantity.Caption = "SL đề xuất";
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.MinWidth = 23;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 9;
            this.colQuantity.Width = 71;
            // 
            // colQuantityReceived
            // 
            this.colQuantityReceived.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityReceived.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQuantityReceived.Caption = "SL thực tế";
            this.colQuantityReceived.FieldName = "QuantityReceived";
            this.colQuantityReceived.MinWidth = 23;
            this.colQuantityReceived.Name = "colQuantityReceived";
            this.colQuantityReceived.OptionsColumn.AllowEdit = false;
            this.colQuantityReceived.Visible = true;
            this.colQuantityReceived.VisibleIndex = 10;
            this.colQuantityReceived.Width = 72;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.FieldName = "Note";
            this.colNote.MinWidth = 23;
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 13;
            this.colNote.Width = 247;
            // 
            // colReceived
            // 
            this.colReceived.Caption = "Ký nhận";
            this.colReceived.ColumnEdit = this.chkReceived;
            this.colReceived.FieldName = "isReceived";
            this.colReceived.MinWidth = 23;
            this.colReceived.Name = "colReceived";
            this.colReceived.Width = 166;
            // 
            // chkReceived
            // 
            this.chkReceived.AutoHeight = false;
            this.chkReceived.DisplayValueChecked = "x";
            this.chkReceived.DisplayValueUnchecked = " ";
            this.chkReceived.Name = "chkReceived";
            this.chkReceived.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colUserIDReceive
            // 
            this.colUserIDReceive.Caption = "ID người nhận";
            this.colUserIDReceive.FieldName = "UserIDReceive";
            this.colUserIDReceive.MinWidth = 23;
            this.colUserIDReceive.Name = "colUserIDReceive";
            this.colUserIDReceive.Width = 88;
            // 
            // colDateRequest
            // 
            this.colDateRequest.AppearanceCell.Options.UseTextOptions = true;
            this.colDateRequest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateRequest.Caption = "Ngày đăng ký";
            this.colDateRequest.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateRequest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateRequest.FieldName = "DateRequest";
            this.colDateRequest.MinWidth = 23;
            this.colDateRequest.Name = "colDateRequest";
            this.colDateRequest.Visible = true;
            this.colDateRequest.VisibleIndex = 6;
            this.colDateRequest.Width = 116;
            // 
            // colExceedsLimit
            // 
            this.colExceedsLimit.Caption = "Vượt định mức";
            this.colExceedsLimit.FieldName = "ExceedsLimit";
            this.colExceedsLimit.MinWidth = 23;
            this.colExceedsLimit.Name = "colExceedsLimit";
            this.colExceedsLimit.OptionsColumn.AllowEdit = false;
            this.colExceedsLimit.Visible = true;
            this.colExceedsLimit.VisibleIndex = 11;
            this.colExceedsLimit.Width = 83;
            // 
            // colReason
            // 
            this.colReason.Caption = "Lý do vượt định mức";
            this.colReason.FieldName = "Reason";
            this.colReason.MinWidth = 23;
            this.colReason.Name = "colReason";
            this.colReason.OptionsColumn.AllowEdit = false;
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 12;
            this.colReason.Width = 144;
            // 
            // colIsApproved
            // 
            this.colIsApproved.Caption = "TBP duyệt";
            this.colIsApproved.ColumnEdit = this.chkReceived;
            this.colIsApproved.FieldName = "IsApproved";
            this.colIsApproved.Name = "colIsApproved";
            this.colIsApproved.Visible = true;
            this.colIsApproved.VisibleIndex = 1;
            this.colIsApproved.Width = 84;
            // 
            // colApprovedID
            // 
            this.colApprovedID.FieldName = "ApprovedID";
            this.colApprovedID.Name = "colApprovedID";
            // 
            // colDateApproved
            // 
            this.colDateApproved.AppearanceCell.Options.UseTextOptions = true;
            this.colDateApproved.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateApproved.Caption = "Ngày duyệt / huỷ duyệt";
            this.colDateApproved.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDateApproved.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateApproved.FieldName = "DateApproved";
            this.colDateApproved.Name = "colDateApproved";
            this.colDateApproved.Visible = true;
            this.colDateApproved.VisibleIndex = 2;
            this.colDateApproved.Width = 107;
            // 
            // colFullNameApproved
            // 
            this.colFullNameApproved.Caption = "Người duyệt / huỷ duyệt";
            this.colFullNameApproved.FieldName = "FullNameApproved";
            this.colFullNameApproved.Name = "colFullNameApproved";
            this.colFullNameApproved.Visible = true;
            this.colFullNameApproved.VisibleIndex = 3;
            this.colFullNameApproved.Width = 149;
            // 
            // colDepartmentID
            // 
            this.colDepartmentID.FieldName = "DepartmentID";
            this.colDepartmentID.Name = "colDepartmentID";
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 5;
            this.colDepartmentName.Width = 137;
            // 
            // frmOfficeSupplyRequest
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 508);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmOfficeSupplyRequest";
            this.Text = "ĐĂNG KÝ VĂN PHÒNG PHẨM";
            this.Load += new System.EventHandler(this.OfficeSupplyRequest_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReceived)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label lblFilterText;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colUserIDReceive;
        private DevExpress.XtraGrid.Columns.GridColumn colOfficeSupplyID;
        private DevExpress.XtraGrid.Columns.GridColumn colOfficeSupplyUnitID;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colOfficeSupplyName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityReceived;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colReceived;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private System.Windows.Forms.Label lblDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDateRequest;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkReceived;
        private DevExpress.XtraGrid.Columns.GridColumn colExceedsLimit;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private System.Windows.Forms.ToolStripButton btnImportExcelOld;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnApprove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDisapprove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Label lblDepartment;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnPassQuantity;
        private System.Windows.Forms.DateTimePicker dtpMonthPicker;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedID;
        private DevExpress.XtraGrid.Columns.GridColumn colDateApproved;
        private DevExpress.XtraGrid.Columns.GridColumn colFullNameApproved;
        private System.Windows.Forms.ToolStripButton btnImportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentID;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
    }
}