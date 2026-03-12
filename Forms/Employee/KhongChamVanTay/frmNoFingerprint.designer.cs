
namespace BMS
{
    partial class frmNoFingerprint
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNoFingerprint));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprovedTP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApprovedHR = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnapproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbApprovedStatusTBP = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodeDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtPageSize = new System.Windows.Forms.NumericUpDown();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPageNumber = new System.Windows.Forms.TextBox();
            this.txtTotalPage = new System.Windows.Forms.TextBox();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tBPHuỷDuyệtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hRHuỷDuyệtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsApprovedTP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsApprovedHR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.memoNote = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colDateRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedTP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusHRtext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReasonDeciline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReasonHREdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNote)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator4,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnApprovedTP,
            this.toolStripSeparator1,
            this.btnApprovedHR,
            this.toolStripSeparator6,
            this.btnUnapproved,
            this.toolStripSeparator5,
            this.btnExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 44);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 40);
            this.btnNew.Tag = "frmNoFinger_HRUse";
            this.btnNew.Text = "Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 44);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Image = global::Forms.Properties.Resources.edit_16x16;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 40);
            this.btnEdit.Tag = "frmNoFinger_HRUse";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 44);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 40);
            this.btnDelete.Tag = "frmNoFinger_HRUse";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // btnApprovedTP
            // 
            this.btnApprovedTP.AutoSize = false;
            this.btnApprovedTP.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnApprovedTP.Image = ((System.Drawing.Image)(resources.GetObject("btnApprovedTP.Image")));
            this.btnApprovedTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprovedTP.Name = "btnApprovedTP";
            this.btnApprovedTP.Size = new System.Drawing.Size(100, 40);
            this.btnApprovedTP.Tag = "frmNoFinger_TBPUse";
            this.btnApprovedTP.Text = "TBP duyệt";
            this.btnApprovedTP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprovedTP.Click += new System.EventHandler(this.btnApproved_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // btnApprovedHR
            // 
            this.btnApprovedHR.AutoSize = false;
            this.btnApprovedHR.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnApprovedHR.Image = ((System.Drawing.Image)(resources.GetObject("btnApprovedHR.Image")));
            this.btnApprovedHR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApprovedHR.Name = "btnApprovedHR";
            this.btnApprovedHR.Size = new System.Drawing.Size(100, 40);
            this.btnApprovedHR.Tag = "frmNoFinger_ApprovedHR";
            this.btnApprovedHR.Text = "HR duyệt";
            this.btnApprovedHR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprovedHR.Click += new System.EventHandler(this.btnApprovedHR_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 44);
            // 
            // btnUnapproved
            // 
            this.btnUnapproved.AutoSize = false;
            this.btnUnapproved.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnUnapproved.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreviewLarge;
            this.btnUnapproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnapproved.Name = "btnUnapproved";
            this.btnUnapproved.Size = new System.Drawing.Size(120, 40);
            this.btnUnapproved.Tag = "frmNoFinger_ApprovedHR";
            this.btnUnapproved.Text = "HR Hủy duyệt";
            this.btnUnapproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnapproved.Click += new System.EventHandler(this.btnUnapproved_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 44);
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = false;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 41);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbApprovedStatusTBP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbDepartment);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.txtFilterText);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.dtpStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 63);
            this.panel1.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(3, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 210;
            this.label4.Text = "Trạng thái TBP";
            // 
            // cbApprovedStatusTBP
            // 
            this.cbApprovedStatusTBP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApprovedStatusTBP.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbApprovedStatusTBP.FormattingEnabled = true;
            this.cbApprovedStatusTBP.Items.AddRange(new object[] {
            "---Tất cả---",
            "Chờ duyệt",
            "Đã duyệt",
            "Không đồng ý duyệt"});
            this.cbApprovedStatusTBP.Location = new System.Drawing.Point(106, 34);
            this.cbApprovedStatusTBP.Name = "cbApprovedStatusTBP";
            this.cbApprovedStatusTBP.Size = new System.Drawing.Size(111, 24);
            this.cbApprovedStatusTBP.TabIndex = 209;
            this.cbApprovedStatusTBP.SelectedIndexChanged += new System.EventHandler(this.cbApprovedStatusTBP_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(408, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 205;
            this.label3.Text = "Phòng ban";
            // 
            // cbDepartment
            // 
            this.cbDepartment.Location = new System.Drawing.Point(489, 6);
            this.cbDepartment.Name = "cbDepartment";
            this.cbDepartment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cbDepartment.Properties.Appearance.Options.UseFont = true;
            this.cbDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbDepartment.Properties.NullText = "";
            this.cbDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cbDepartment.Size = new System.Drawing.Size(169, 22);
            this.cbDepartment.TabIndex = 204;
            this.cbDepartment.EditValueChanged += new System.EventHandler(this.cbDepartment_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 45;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDDepartment,
            this.colCodeDepartment,
            this.colNameDepartment});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.RowHeight = 25;
            // 
            // colIDDepartment
            // 
            this.colIDDepartment.Caption = "IDDepartment";
            this.colIDDepartment.FieldName = "ID";
            this.colIDDepartment.Name = "colIDDepartment";
            // 
            // colCodeDepartment
            // 
            this.colCodeDepartment.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.colCodeDepartment.AppearanceCell.Options.HighPriority = true;
            this.colCodeDepartment.AppearanceCell.Options.UseFont = true;
            this.colCodeDepartment.AppearanceCell.Options.UseTextOptions = true;
            this.colCodeDepartment.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCodeDepartment.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCodeDepartment.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.colCodeDepartment.AppearanceHeader.Options.UseBackColor = true;
            this.colCodeDepartment.AppearanceHeader.Options.UseFont = true;
            this.colCodeDepartment.AppearanceHeader.Options.UseForeColor = true;
            this.colCodeDepartment.AppearanceHeader.Options.UseTextOptions = true;
            this.colCodeDepartment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCodeDepartment.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCodeDepartment.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCodeDepartment.Caption = "Mã phòng ban";
            this.colCodeDepartment.FieldName = "Code";
            this.colCodeDepartment.Name = "colCodeDepartment";
            this.colCodeDepartment.OptionsColumn.ReadOnly = true;
            this.colCodeDepartment.Visible = true;
            this.colCodeDepartment.VisibleIndex = 0;
            // 
            // colNameDepartment
            // 
            this.colNameDepartment.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.colNameDepartment.AppearanceCell.Options.HighPriority = true;
            this.colNameDepartment.AppearanceCell.Options.UseFont = true;
            this.colNameDepartment.AppearanceCell.Options.UseTextOptions = true;
            this.colNameDepartment.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNameDepartment.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNameDepartment.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.colNameDepartment.AppearanceHeader.Options.UseBackColor = true;
            this.colNameDepartment.AppearanceHeader.Options.UseFont = true;
            this.colNameDepartment.AppearanceHeader.Options.UseForeColor = true;
            this.colNameDepartment.AppearanceHeader.Options.UseTextOptions = true;
            this.colNameDepartment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNameDepartment.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNameDepartment.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNameDepartment.Caption = "Tên phòng ban";
            this.colNameDepartment.FieldName = "Name";
            this.colNameDepartment.Name = "colNameDepartment";
            this.colNameDepartment.OptionsColumn.ReadOnly = true;
            this.colNameDepartment.Visible = true;
            this.colNameDepartment.VisibleIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.txtPageSize);
            this.panel6.Controls.Add(this.btnPrev);
            this.panel6.Controls.Add(this.btnFirst);
            this.panel6.Controls.Add(this.btnLast);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtPageNumber);
            this.panel6.Controls.Add(this.txtTotalPage);
            this.panel6.Controls.Add(this.btnNext);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(1071, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(299, 63);
            this.panel6.TabIndex = 203;
            // 
            // txtPageSize
            // 
            this.txtPageSize.BackColor = System.Drawing.SystemColors.Control;
            this.txtPageSize.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPageSize.Location = new System.Drawing.Point(189, 31);
            this.txtPageSize.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtPageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(101, 24);
            this.txtPageSize.TabIndex = 12;
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPageSize.Value = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtPageSize.ValueChanged += new System.EventHandler(this.txtPageSize_ValueChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrev.Appearance.Options.UseBackColor = true;
            this.btnPrev.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.ImageOptions.Image")));
            this.btnPrev.Location = new System.Drawing.Point(30, 32);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnPrev.Size = new System.Drawing.Size(23, 23);
            this.btnPrev.TabIndex = 141;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnFirst.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnFirst.Appearance.Options.UseBackColor = true;
            this.btnFirst.Appearance.Options.UseForeColor = true;
            this.btnFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.ImageOptions.Image")));
            this.btnFirst.Location = new System.Drawing.Point(3, 32);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnFirst.Size = new System.Drawing.Size(23, 23);
            this.btnFirst.TabIndex = 143;
            this.btnFirst.Text = "Trang trước";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnLast.Appearance.Options.UseBackColor = true;
            this.btnLast.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.ImageOptions.Image")));
            this.btnLast.Location = new System.Drawing.Point(162, 32);
            this.btnLast.Name = "btnLast";
            this.btnLast.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnLast.Size = new System.Drawing.Size(23, 23);
            this.btnLast.TabIndex = 144;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(86, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 23);
            this.label9.TabIndex = 151;
            this.label9.Text = "/";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPageNumber.Location = new System.Drawing.Point(57, 31);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.ReadOnly = true;
            this.txtPageNumber.Size = new System.Drawing.Size(25, 24);
            this.txtPageNumber.TabIndex = 13;
            this.txtPageNumber.Text = "1";
            this.txtPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTotalPage.Location = new System.Drawing.Point(106, 31);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.ReadOnly = true;
            this.txtTotalPage.Size = new System.Drawing.Size(25, 24);
            this.txtTotalPage.TabIndex = 12;
            this.txtTotalPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Appearance.Options.UseBackColor = true;
            this.btnNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.ImageOptions.Image")));
            this.btnNext.Location = new System.Drawing.Point(135, 32);
            this.btnNext.Name = "btnNext";
            this.btnNext.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 142;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnFind.Location = new System.Drawing.Point(583, 33);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 27);
            this.btnFind.TabIndex = 202;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFilterText
            // 
            this.txtFilterText.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFilterText.Location = new System.Drawing.Point(297, 34);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(280, 24);
            this.txtFilterText.TabIndex = 201;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(223, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 200;
            this.label7.Text = "Từ khóa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(223, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến ngày";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ ngày";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(297, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(105, 24);
            this.dtpEnd.TabIndex = 1;
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(106, 4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(111, 24);
            this.dtpStart.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tBPHuỷDuyệtToolStripMenuItem,
            this.hRHuỷDuyệtToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 48);
            // 
            // tBPHuỷDuyệtToolStripMenuItem
            // 
            this.tBPHuỷDuyệtToolStripMenuItem.Name = "tBPHuỷDuyệtToolStripMenuItem";
            this.tBPHuỷDuyệtToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.tBPHuỷDuyệtToolStripMenuItem.Text = "TBP huỷ duyệt";
            this.tBPHuỷDuyệtToolStripMenuItem.Click += new System.EventHandler(this.tBPHuỷDuyệtToolStripMenuItem_Click);
            // 
            // hRHuỷDuyệtToolStripMenuItem
            // 
            this.hRHuỷDuyệtToolStripMenuItem.Name = "hRHuỷDuyệtToolStripMenuItem";
            this.hRHuỷDuyệtToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.hRHuỷDuyệtToolStripMenuItem.Tag = "frmNoFinger_UnApprovedHR";
            this.hRHuỷDuyệtToolStripMenuItem.Text = "HR huỷ duyệt";
            this.hRHuỷDuyệtToolStripMenuItem.Click += new System.EventHandler(this.hRHuỷDuyệtToolStripMenuItem_Click);
            // 
            // grdData
            // 
            this.grdData.AllowDrop = true;
            this.grdData.ContextMenuStrip = this.contextMenuStrip1;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Location = new System.Drawing.Point(0, 107);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.memoNote});
            this.grdData.Size = new System.Drawing.Size(1370, 536);
            this.grdData.TabIndex = 26;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.DoubleClick += new System.EventHandler(this.grdData_DoubleClick);
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
            this.grvData.ColumnPanelRowHeight = 50;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colIsApprovedTP,
            this.colIsApprovedHR,
            this.colCode,
            this.colFullName,
            this.colDateRegister,
            this.colReason,
            this.colCreatedDate,
            this.colCreatedBy,
            this.colUpdatedBy,
            this.colUpdatedDate,
            this.colType,
            this.colEmployeeID,
            this.colDepartmentID,
            this.colDepartmentName,
            this.colTypeText,
            this.colApprovedTP,
            this.colApprovedName,
            this.colStatusText,
            this.colStatusHRtext,
            this.colReasonDeciline,
            this.colReasonHREdit});
            this.grvData.DetailHeight = 881;
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.grvData.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDepartmentName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 53;
            this.colID.Name = "colID";
            this.colID.Width = 294;
            // 
            // colIsApprovedTP
            // 
            this.colIsApprovedTP.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIsApprovedTP.AppearanceCell.Options.UseFont = true;
            this.colIsApprovedTP.AppearanceCell.Options.UseTextOptions = true;
            this.colIsApprovedTP.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIsApprovedTP.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApprovedTP.Caption = "TBP Duyệt";
            this.colIsApprovedTP.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsApprovedTP.FieldName = "IsApprovedTP";
            this.colIsApprovedTP.MaxWidth = 70;
            this.colIsApprovedTP.MinWidth = 70;
            this.colIsApprovedTP.Name = "colIsApprovedTP";
            this.colIsApprovedTP.OptionsColumn.AllowEdit = false;
            this.colIsApprovedTP.OptionsColumn.AllowMove = false;
            this.colIsApprovedTP.OptionsColumn.AllowSize = false;
            this.colIsApprovedTP.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIsApprovedTP.OptionsColumn.FixedWidth = true;
            this.colIsApprovedTP.OptionsColumn.TabStop = false;
            this.colIsApprovedTP.OptionsFilter.AllowAutoFilter = false;
            this.colIsApprovedTP.OptionsFilter.AllowFilter = false;
            this.colIsApprovedTP.Width = 53;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colIsApprovedHR
            // 
            this.colIsApprovedHR.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIsApprovedHR.AppearanceCell.Options.UseFont = true;
            this.colIsApprovedHR.AppearanceCell.Options.UseTextOptions = true;
            this.colIsApprovedHR.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIsApprovedHR.Caption = "HR Duyệt";
            this.colIsApprovedHR.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsApprovedHR.FieldName = "IsApprovedHR";
            this.colIsApprovedHR.MaxWidth = 70;
            this.colIsApprovedHR.MinWidth = 70;
            this.colIsApprovedHR.Name = "colIsApprovedHR";
            this.colIsApprovedHR.OptionsColumn.AllowEdit = false;
            this.colIsApprovedHR.OptionsColumn.AllowSize = false;
            this.colIsApprovedHR.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colIsApprovedHR.OptionsColumn.TabStop = false;
            this.colIsApprovedHR.OptionsFilter.AllowAutoFilter = false;
            this.colIsApprovedHR.OptionsFilter.AllowFilter = false;
            this.colIsApprovedHR.Width = 70;
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.MinWidth = 19;
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.OptionsColumn.AllowMove = false;
            this.colCode.OptionsColumn.AllowSize = false;
            this.colCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCode.OptionsColumn.FixedWidth = true;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 3;
            this.colCode.Width = 104;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.ColumnEdit = this.memoNote;
            this.colFullName.FieldName = "FullName";
            this.colFullName.MaxWidth = 150;
            this.colFullName.MinWidth = 53;
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.AllowSize = false;
            this.colFullName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFullName.OptionsFilter.AllowAutoFilter = false;
            this.colFullName.OptionsFilter.AllowFilter = false;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 4;
            this.colFullName.Width = 111;
            // 
            // memoNote
            // 
            this.memoNote.Name = "memoNote";
            // 
            // colDateRegister
            // 
            this.colDateRegister.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDateRegister.AppearanceCell.Options.UseFont = true;
            this.colDateRegister.AppearanceCell.Options.UseTextOptions = true;
            this.colDateRegister.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateRegister.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDateRegister.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDateRegister.Caption = "Ngày ";
            this.colDateRegister.FieldName = "DayWork";
            this.colDateRegister.MaxWidth = 150;
            this.colDateRegister.MinWidth = 53;
            this.colDateRegister.Name = "colDateRegister";
            this.colDateRegister.OptionsColumn.AllowEdit = false;
            this.colDateRegister.OptionsColumn.AllowSize = false;
            this.colDateRegister.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDateRegister.OptionsFilter.AllowAutoFilter = false;
            this.colDateRegister.OptionsFilter.AllowFilter = false;
            this.colDateRegister.Visible = true;
            this.colDateRegister.VisibleIndex = 6;
            this.colDateRegister.Width = 109;
            // 
            // colReason
            // 
            this.colReason.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colReason.AppearanceCell.Options.UseFont = true;
            this.colReason.AppearanceCell.Options.UseTextOptions = true;
            this.colReason.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colReason.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colReason.Caption = "Ghi chú";
            this.colReason.ColumnEdit = this.memoNote;
            this.colReason.FieldName = "Note";
            this.colReason.MinWidth = 37;
            this.colReason.Name = "colReason";
            this.colReason.OptionsColumn.AllowSize = false;
            this.colReason.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 8;
            this.colReason.Width = 273;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCreatedDate.AppearanceCell.Options.UseFont = true;
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.MinWidth = 37;
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Width = 136;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colCreatedBy.AppearanceCell.Options.UseFont = true;
            this.colCreatedBy.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedBy.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedBy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedBy.Caption = "Người tạo";
            this.colCreatedBy.FieldName = "CreatedBy";
            this.colCreatedBy.MinWidth = 37;
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.Width = 136;
            // 
            // colUpdatedBy
            // 
            this.colUpdatedBy.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colUpdatedBy.AppearanceCell.Options.UseFont = true;
            this.colUpdatedBy.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdatedBy.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdatedBy.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedBy.Caption = "Người update";
            this.colUpdatedBy.FieldName = "UpdatedBy";
            this.colUpdatedBy.MinWidth = 37;
            this.colUpdatedBy.Name = "colUpdatedBy";
            this.colUpdatedBy.Width = 136;
            // 
            // colUpdatedDate
            // 
            this.colUpdatedDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colUpdatedDate.AppearanceCell.Options.UseFont = true;
            this.colUpdatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdatedDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colUpdatedDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colUpdatedDate.Caption = "Ngày Update";
            this.colUpdatedDate.FieldName = "UpdatedDate";
            this.colUpdatedDate.MinWidth = 37;
            this.colUpdatedDate.Name = "colUpdatedDate";
            this.colUpdatedDate.Width = 136;
            // 
            // colType
            // 
            this.colType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colType.AppearanceCell.Options.UseFont = true;
            this.colType.AppearanceCell.Options.UseTextOptions = true;
            this.colType.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colType.Caption = "Type";
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colEmployeeID.AppearanceCell.Options.UseFont = true;
            this.colEmployeeID.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployeeID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colEmployeeID.Caption = "EmployeeID";
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.Name = "colEmployeeID";
            // 
            // colDepartmentID
            // 
            this.colDepartmentID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDepartmentID.AppearanceCell.Options.UseFont = true;
            this.colDepartmentID.AppearanceCell.Options.UseTextOptions = true;
            this.colDepartmentID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDepartmentID.Caption = "DepartmentID";
            this.colDepartmentID.FieldName = "DepartmentID";
            this.colDepartmentID.Name = "colDepartmentID";
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colDepartmentName.AppearanceCell.Options.UseFont = true;
            this.colDepartmentName.AppearanceCell.Options.UseTextOptions = true;
            this.colDepartmentName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colDepartmentName.Caption = "Phòng ban";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 5;
            // 
            // colTypeText
            // 
            this.colTypeText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colTypeText.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.colTypeText.AppearanceCell.Options.UseFont = true;
            this.colTypeText.AppearanceCell.Options.UseForeColor = true;
            this.colTypeText.AppearanceCell.Options.UseTextOptions = true;
            this.colTypeText.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTypeText.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTypeText.Caption = "Loại";
            this.colTypeText.ColumnEdit = this.memoNote;
            this.colTypeText.FieldName = "TypeText";
            this.colTypeText.MaxWidth = 150;
            this.colTypeText.Name = "colTypeText";
            this.colTypeText.Visible = true;
            this.colTypeText.VisibleIndex = 7;
            this.colTypeText.Width = 111;
            // 
            // colApprovedTP
            // 
            this.colApprovedTP.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colApprovedTP.AppearanceCell.Options.UseFont = true;
            this.colApprovedTP.AppearanceCell.Options.UseTextOptions = true;
            this.colApprovedTP.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colApprovedTP.FieldName = "ApprovedTP";
            this.colApprovedTP.Name = "colApprovedTP";
            // 
            // colApprovedName
            // 
            this.colApprovedName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colApprovedName.AppearanceCell.Options.UseFont = true;
            this.colApprovedName.AppearanceCell.Options.UseTextOptions = true;
            this.colApprovedName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colApprovedName.Caption = "Người duyệt";
            this.colApprovedName.FieldName = "ApprovedName";
            this.colApprovedName.Name = "colApprovedName";
            this.colApprovedName.Visible = true;
            this.colApprovedName.VisibleIndex = 5;
            this.colApprovedName.Width = 109;
            // 
            // colStatusText
            // 
            this.colStatusText.Caption = "TBP duyệt";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 1;
            this.colStatusText.Width = 55;
            // 
            // colStatusHRtext
            // 
            this.colStatusHRtext.Caption = "HR duyệt";
            this.colStatusHRtext.FieldName = "StatusHRText";
            this.colStatusHRtext.Name = "colStatusHRtext";
            this.colStatusHRtext.Visible = true;
            this.colStatusHRtext.VisibleIndex = 2;
            this.colStatusHRtext.Width = 55;
            // 
            // colReasonDeciline
            // 
            this.colReasonDeciline.Caption = "Lý do không đồng ý duyệt";
            this.colReasonDeciline.FieldName = "ReasonDeciline";
            this.colReasonDeciline.Name = "colReasonDeciline";
            this.colReasonDeciline.Visible = true;
            this.colReasonDeciline.VisibleIndex = 10;
            this.colReasonDeciline.Width = 155;
            // 
            // colReasonHREdit
            // 
            this.colReasonHREdit.Caption = "Lý do sửa";
            this.colReasonHREdit.ColumnEdit = this.memoNote;
            this.colReasonHREdit.FieldName = "ReasonHREdit";
            this.colReasonHREdit.Name = "colReasonHREdit";
            this.colReasonHREdit.Visible = true;
            this.colReasonHREdit.VisibleIndex = 9;
            this.colReasonHREdit.Width = 188;
            // 
            // frmNoFingerprint
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 643);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmNoFingerprint";
            this.Text = "QUÊN VÂN TAY";
            this.Load += new System.EventHandler(this.frmNoFingerprint_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnApprovedTP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUnapproved;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown txtPageSize;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPageNumber;
        private System.Windows.Forms.TextBox txtTotalPage;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private DevExpress.XtraEditors.SearchLookUpEdit cbDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.Columns.GridColumn colIDDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn colNameDepartment;
        private System.Windows.Forms.ComboBox cbApprovedStatusTBP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnApprovedHR;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tBPHuỷDuyệtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hRHuỷDuyệtToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApprovedTP;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsApprovedHR;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colDateRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memoNote;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentID;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeText;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedTP;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusHRtext;
        private DevExpress.XtraGrid.Columns.GridColumn colReasonDeciline;
        private DevExpress.XtraGrid.Columns.GridColumn colReasonHREdit;
    }
}