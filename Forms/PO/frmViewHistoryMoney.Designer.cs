
namespace Forms.PO
{
    partial class frmViewHistoryMoney
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewHistoryMoney));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFind = new System.Windows.Forms.Button();
            this.cbGroup = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUser = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIntoMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecivedMoneyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceivedDatePO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMainIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColorStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiveMoneyMaster = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGuestCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalMoneyKoVAT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalMoneyPO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOKHID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tiềnVềToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiTiếtPOKháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1313, 42);
            this.mnuMenu.TabIndex = 20;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(69, 37);
            this.btnExportExcel.Tag = "frmProductRTC_SALEAddProduct";
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdMaster, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.656101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.34389F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1313, 639);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.cbGroup);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbCustomer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbUser);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtFilterText);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1307, 36);
            this.panel1.TabIndex = 0;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(1104, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(61, 23);
            this.btnFind.TabIndex = 199;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // cbGroup
            // 
            this.cbGroup.Location = new System.Drawing.Point(565, 8);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbGroup.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGroup.Properties.Appearance.Options.UseBackColor = true;
            this.cbGroup.Properties.Appearance.Options.UseFont = true;
            this.cbGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbGroup.Properties.NullText = "";
            this.cbGroup.Properties.PopupView = this.gridView1;
            this.cbGroup.Size = new System.Drawing.Size(116, 20);
            this.cbGroup.TabIndex = 198;
            this.cbGroup.EditValueChanged += new System.EventHandler(this.cbGroup_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.ColumnPanelRowHeight = 41;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.DetailHeight = 284;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 24;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ID";
            this.gridColumn4.FieldName = "ID";
            this.gridColumn4.MinWidth = 15;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 15;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tên nhóm";
            this.gridColumn5.FieldName = "GroupSalesName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(524, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 197;
            this.label6.Text = "Nhóm";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpStartDate.CustomFormat = "dd/MM//yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(54, 8);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(91, 20);
            this.dtpStartDate.TabIndex = 194;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpEndDate.CustomFormat = "dd/MM//yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(210, 9);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(91, 20);
            this.dtpEndDate.TabIndex = 191;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 193;
            this.label3.Text = "Từ ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(151, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 192;
            this.label4.Text = "Đến ngày";
            // 
            // cbCustomer
            // 
            this.cbCustomer.EditValue = "";
            this.cbCustomer.Location = new System.Drawing.Point(758, 8);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCustomer.Properties.NullText = "";
            this.cbCustomer.Properties.PopupView = this.gridView3;
            this.cbCustomer.Size = new System.Drawing.Size(147, 20);
            this.cbCustomer.TabIndex = 189;
            this.cbCustomer.EditValueChanged += new System.EventHandler(this.cbCustomer_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.ColumnPanelRowHeight = 30;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn33,
            this.gridColumn35});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "ID";
            this.gridColumn33.FieldName = "ID";
            this.gridColumn33.Name = "gridColumn33";
            // 
            // gridColumn35
            // 
            this.gridColumn35.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn35.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn35.AppearanceHeader.Options.UseFont = true;
            this.gridColumn35.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn35.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn35.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn35.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn35.Caption = "Tên khách hàng";
            this.gridColumn35.FieldName = "CustomerName";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 0;
            this.gridColumn35.Width = 175;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(687, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 190;
            this.label2.Text = "Khách hàng";
            // 
            // cbUser
            // 
            this.cbUser.EditValue = "";
            this.cbUser.Location = new System.Drawing.Point(404, 8);
            this.cbUser.Name = "cbUser";
            this.cbUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbUser.Properties.NullText = "";
            this.cbUser.Properties.PopupView = this.searchLookUpEdit1View;
            this.cbUser.Size = new System.Drawing.Size(114, 20);
            this.cbUser.TabIndex = 187;
            this.cbUser.EditValueChanged += new System.EventHandler(this.cbUser_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 30;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn20});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ID";
            this.gridColumn2.FieldName = "ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn20
            // 
            this.gridColumn20.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn20.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridColumn20.AppearanceHeader.Options.UseFont = true;
            this.gridColumn20.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn20.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumn20.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn20.Caption = "Tên người phụ trách";
            this.gridColumn20.FieldName = "FullName";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 0;
            this.gridColumn20.Width = 175;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label11.Location = new System.Drawing.Point(315, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 188;
            this.label11.Text = "Người phụ trách";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(959, 9);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(139, 20);
            this.txtFilterText.TabIndex = 184;
            this.txtFilterText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterText_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(911, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 183;
            this.label7.Text = "Từ khóa";
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Location = new System.Drawing.Point(3, 45);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.grdMaster.Size = new System.Drawing.Size(1307, 591);
            this.grdMaster.TabIndex = 1;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            // 
            // grvMaster
            // 
            this.grvMaster.ColumnPanelRowHeight = 40;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colProductID,
            this.colIntoMoney,
            this.colIndex,
            this.colRecivedMoneyDate,
            this.colBillDate,
            this.colProjectCode,
            this.colReceivedDatePO,
            this.colMainIndex,
            this.colColorStatus,
            this.colReceiveMoneyMaster,
            this.colGuestCode,
            this.colBillNumber,
            this.colTotalMoneyKoVAT,
            this.colTotalMoneyPO,
            this.colPOKHID});
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.Editable = false;
            this.grvMaster.OptionsFind.AlwaysVisible = true;
            this.grvMaster.OptionsView.ShowAutoFilterRow = true;
            this.grvMaster.OptionsView.ShowFooter = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colProductID
            // 
            this.colProductID.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colProductID.AppearanceCell.Options.UseFont = true;
            this.colProductID.AppearanceCell.Options.UseTextOptions = true;
            this.colProductID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProductID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProductID.AppearanceHeader.Options.UseFont = true;
            this.colProductID.AppearanceHeader.Options.UseForeColor = true;
            this.colProductID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductID.Caption = "Mã PO";
            this.colProductID.FieldName = "POCode";
            this.colProductID.Name = "colProductID";
            this.colProductID.OptionsColumn.ReadOnly = true;
            this.colProductID.Visible = true;
            this.colProductID.VisibleIndex = 1;
            this.colProductID.Width = 182;
            // 
            // colIntoMoney
            // 
            this.colIntoMoney.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colIntoMoney.AppearanceCell.Options.UseFont = true;
            this.colIntoMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colIntoMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIntoMoney.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIntoMoney.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIntoMoney.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIntoMoney.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIntoMoney.AppearanceHeader.Options.UseFont = true;
            this.colIntoMoney.AppearanceHeader.Options.UseForeColor = true;
            this.colIntoMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.colIntoMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIntoMoney.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIntoMoney.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIntoMoney.Caption = "Tổng tiền về trước VAT";
            this.colIntoMoney.ColumnEdit = this.repositoryItemTextEdit1;
            this.colIntoMoney.DisplayFormat.FormatString = "n0";
            this.colIntoMoney.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colIntoMoney.FieldName = "MoneyVAT";
            this.colIntoMoney.Name = "colIntoMoney";
            this.colIntoMoney.OptionsColumn.AllowEdit = false;
            this.colIntoMoney.OptionsColumn.ReadOnly = true;
            this.colIntoMoney.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IntoMoney", "{0:n0}")});
            this.colIntoMoney.Visible = true;
            this.colIntoMoney.VisibleIndex = 6;
            this.colIntoMoney.Width = 219;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BeepOnError = false;
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "n0");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.UseMaskAsDisplayFormat = true;
            // 
            // colIndex
            // 
            this.colIndex.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colIndex.AppearanceCell.Options.UseFont = true;
            this.colIndex.AppearanceCell.Options.UseTextOptions = true;
            this.colIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIndex.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIndex.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIndex.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIndex.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIndex.AppearanceHeader.Options.UseFont = true;
            this.colIndex.AppearanceHeader.Options.UseForeColor = true;
            this.colIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIndex.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colIndex.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIndex.Caption = "Index";
            this.colIndex.FieldName = "IndexPO";
            this.colIndex.Name = "colIndex";
            this.colIndex.OptionsColumn.ReadOnly = true;
            this.colIndex.Width = 49;
            // 
            // colRecivedMoneyDate
            // 
            this.colRecivedMoneyDate.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colRecivedMoneyDate.AppearanceCell.Options.UseFont = true;
            this.colRecivedMoneyDate.AppearanceCell.Options.UseTextOptions = true;
            this.colRecivedMoneyDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRecivedMoneyDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRecivedMoneyDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRecivedMoneyDate.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colRecivedMoneyDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colRecivedMoneyDate.AppearanceHeader.Options.UseFont = true;
            this.colRecivedMoneyDate.AppearanceHeader.Options.UseForeColor = true;
            this.colRecivedMoneyDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colRecivedMoneyDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRecivedMoneyDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRecivedMoneyDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRecivedMoneyDate.Caption = "Ngày tiền về";
            this.colRecivedMoneyDate.FieldName = "MoneyDate";
            this.colRecivedMoneyDate.Name = "colRecivedMoneyDate";
            this.colRecivedMoneyDate.OptionsColumn.ReadOnly = true;
            this.colRecivedMoneyDate.Visible = true;
            this.colRecivedMoneyDate.VisibleIndex = 7;
            this.colRecivedMoneyDate.Width = 126;
            // 
            // colBillDate
            // 
            this.colBillDate.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colBillDate.AppearanceCell.Options.UseFont = true;
            this.colBillDate.AppearanceCell.Options.UseTextOptions = true;
            this.colBillDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBillDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBillDate.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBillDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBillDate.AppearanceHeader.Options.UseFont = true;
            this.colBillDate.AppearanceHeader.Options.UseForeColor = true;
            this.colBillDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colBillDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBillDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBillDate.Caption = "Ngày hóa đơn";
            this.colBillDate.FieldName = "BillDate";
            this.colBillDate.Name = "colBillDate";
            this.colBillDate.OptionsColumn.ReadOnly = true;
            this.colBillDate.Visible = true;
            this.colBillDate.VisibleIndex = 9;
            this.colBillDate.Width = 121;
            // 
            // colProjectCode
            // 
            this.colProjectCode.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colProjectCode.AppearanceCell.Options.UseFont = true;
            this.colProjectCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProjectCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectCode.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colProjectCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colProjectCode.AppearanceHeader.Options.UseFont = true;
            this.colProjectCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectCode.Caption = "Nhân viên";
            this.colProjectCode.FieldName = "FullName";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.OptionsColumn.ReadOnly = true;
            this.colProjectCode.Visible = true;
            this.colProjectCode.VisibleIndex = 2;
            this.colProjectCode.Width = 196;
            // 
            // colReceivedDatePO
            // 
            this.colReceivedDatePO.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colReceivedDatePO.AppearanceCell.Options.UseFont = true;
            this.colReceivedDatePO.AppearanceCell.Options.UseTextOptions = true;
            this.colReceivedDatePO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReceivedDatePO.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colReceivedDatePO.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colReceivedDatePO.AppearanceHeader.Options.UseFont = true;
            this.colReceivedDatePO.AppearanceHeader.Options.UseForeColor = true;
            this.colReceivedDatePO.AppearanceHeader.Options.UseTextOptions = true;
            this.colReceivedDatePO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReceivedDatePO.Caption = "Ngày PO";
            this.colReceivedDatePO.FieldName = "ReceivedDatePO";
            this.colReceivedDatePO.Name = "colReceivedDatePO";
            this.colReceivedDatePO.OptionsColumn.ReadOnly = true;
            this.colReceivedDatePO.Visible = true;
            this.colReceivedDatePO.VisibleIndex = 3;
            this.colReceivedDatePO.Width = 150;
            // 
            // colMainIndex
            // 
            this.colMainIndex.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colMainIndex.AppearanceCell.Options.UseFont = true;
            this.colMainIndex.AppearanceCell.Options.UseTextOptions = true;
            this.colMainIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMainIndex.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colMainIndex.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMainIndex.AppearanceHeader.Options.UseFont = true;
            this.colMainIndex.AppearanceHeader.Options.UseForeColor = true;
            this.colMainIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colMainIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMainIndex.Caption = "Loại PO";
            this.colMainIndex.FieldName = "MainIndex";
            this.colMainIndex.Name = "colMainIndex";
            this.colMainIndex.OptionsColumn.ReadOnly = true;
            this.colMainIndex.Width = 186;
            // 
            // colColorStatus
            // 
            this.colColorStatus.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colColorStatus.AppearanceCell.Options.UseFont = true;
            this.colColorStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colColorStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colColorStatus.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colColorStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colColorStatus.AppearanceHeader.Options.UseFont = true;
            this.colColorStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colColorStatus.Caption = "gridColumn1";
            this.colColorStatus.FieldName = "Status";
            this.colColorStatus.Name = "colColorStatus";
            this.colColorStatus.OptionsColumn.ReadOnly = true;
            // 
            // colReceiveMoneyMaster
            // 
            this.colReceiveMoneyMaster.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colReceiveMoneyMaster.AppearanceCell.Options.UseFont = true;
            this.colReceiveMoneyMaster.AppearanceCell.Options.UseTextOptions = true;
            this.colReceiveMoneyMaster.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReceiveMoneyMaster.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colReceiveMoneyMaster.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colReceiveMoneyMaster.AppearanceHeader.Options.UseFont = true;
            this.colReceiveMoneyMaster.AppearanceHeader.Options.UseForeColor = true;
            this.colReceiveMoneyMaster.AppearanceHeader.Options.UseTextOptions = true;
            this.colReceiveMoneyMaster.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colReceiveMoneyMaster.Caption = "Tiền về ";
            this.colReceiveMoneyMaster.ColumnEdit = this.repositoryItemTextEdit1;
            this.colReceiveMoneyMaster.FieldName = "Money";
            this.colReceiveMoneyMaster.Name = "colReceiveMoneyMaster";
            this.colReceiveMoneyMaster.Visible = true;
            this.colReceiveMoneyMaster.VisibleIndex = 8;
            this.colReceiveMoneyMaster.Width = 178;
            // 
            // colGuestCode
            // 
            this.colGuestCode.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colGuestCode.AppearanceCell.Options.UseFont = true;
            this.colGuestCode.AppearanceCell.Options.UseTextOptions = true;
            this.colGuestCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGuestCode.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colGuestCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGuestCode.AppearanceHeader.Options.UseFont = true;
            this.colGuestCode.AppearanceHeader.Options.UseForeColor = true;
            this.colGuestCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colGuestCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGuestCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colGuestCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGuestCode.Caption = "Số POKH";
            this.colGuestCode.FieldName = "PONumber";
            this.colGuestCode.Name = "colGuestCode";
            this.colGuestCode.Visible = true;
            this.colGuestCode.VisibleIndex = 0;
            this.colGuestCode.Width = 108;
            // 
            // colBillNumber
            // 
            this.colBillNumber.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.colBillNumber.AppearanceCell.Options.UseFont = true;
            this.colBillNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colBillNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillNumber.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.colBillNumber.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colBillNumber.AppearanceHeader.Options.UseFont = true;
            this.colBillNumber.AppearanceHeader.Options.UseForeColor = true;
            this.colBillNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colBillNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBillNumber.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colBillNumber.Caption = "Số hóa đơn";
            this.colBillNumber.FieldName = "BillNumber";
            this.colBillNumber.Name = "colBillNumber";
            this.colBillNumber.Visible = true;
            this.colBillNumber.VisibleIndex = 10;
            this.colBillNumber.Width = 111;
            // 
            // colTotalMoneyKoVAT
            // 
            this.colTotalMoneyKoVAT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalMoneyKoVAT.AppearanceCell.Options.UseFont = true;
            this.colTotalMoneyKoVAT.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalMoneyKoVAT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalMoneyKoVAT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalMoneyKoVAT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTotalMoneyKoVAT.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalMoneyKoVAT.AppearanceHeader.Options.UseFont = true;
            this.colTotalMoneyKoVAT.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalMoneyKoVAT.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalMoneyKoVAT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalMoneyKoVAT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalMoneyKoVAT.Caption = "Tiền PO không VAT";
            this.colTotalMoneyKoVAT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTotalMoneyKoVAT.FieldName = "TotalMoneyKoVAT";
            this.colTotalMoneyKoVAT.Name = "colTotalMoneyKoVAT";
            this.colTotalMoneyKoVAT.Visible = true;
            this.colTotalMoneyKoVAT.VisibleIndex = 4;
            this.colTotalMoneyKoVAT.Width = 143;
            // 
            // colTotalMoneyPO
            // 
            this.colTotalMoneyPO.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotalMoneyPO.AppearanceCell.Options.UseFont = true;
            this.colTotalMoneyPO.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalMoneyPO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalMoneyPO.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalMoneyPO.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colTotalMoneyPO.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colTotalMoneyPO.AppearanceHeader.Options.UseFont = true;
            this.colTotalMoneyPO.AppearanceHeader.Options.UseForeColor = true;
            this.colTotalMoneyPO.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalMoneyPO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalMoneyPO.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotalMoneyPO.Caption = "Tiền PO";
            this.colTotalMoneyPO.ColumnEdit = this.repositoryItemTextEdit1;
            this.colTotalMoneyPO.FieldName = "TotalMoneyPO";
            this.colTotalMoneyPO.Name = "colTotalMoneyPO";
            this.colTotalMoneyPO.Visible = true;
            this.colTotalMoneyPO.VisibleIndex = 5;
            this.colTotalMoneyPO.Width = 81;
            // 
            // colPOKHID
            // 
            this.colPOKHID.Caption = "gridColumn1";
            this.colPOKHID.FieldName = "POKHID";
            this.colPOKHID.Name = "colPOKHID";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiềnVềToolStripMenuItem,
            this.chiTiếtPOKháchHàngToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(237, 48);
            // 
            // tiềnVềToolStripMenuItem
            // 
            this.tiềnVềToolStripMenuItem.Name = "tiềnVềToolStripMenuItem";
            this.tiềnVềToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.tiềnVềToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.tiềnVềToolStripMenuItem.Text = "Tiền về";
            this.tiềnVềToolStripMenuItem.Click += new System.EventHandler(this.tiềnVềToolStripMenuItem_Click);
            // 
            // chiTiếtPOKháchHàngToolStripMenuItem
            // 
            this.chiTiếtPOKháchHàngToolStripMenuItem.Name = "chiTiếtPOKháchHàngToolStripMenuItem";
            this.chiTiếtPOKháchHàngToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.chiTiếtPOKháchHàngToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.chiTiếtPOKháchHàngToolStripMenuItem.Text = "Chi tiết PO khách hàng";
            this.chiTiếtPOKháchHàngToolStripMenuItem.Click += new System.EventHandler(this.chiTiếtPOKháchHàngToolStripMenuItem_Click);
            // 
            // frmViewHistoryMoney
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 681);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmViewHistoryMoney";
            this.Text = "CHI TIẾT PO KHÁCH HÀNG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmViewPOKH_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraEditors.SearchLookUpEdit cbGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SearchLookUpEdit cbCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchLookUpEdit cbUser;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraGrid.Columns.GridColumn colIntoMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colRecivedMoneyDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBillDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectCode;
        private DevExpress.XtraGrid.Columns.GridColumn colReceivedDatePO;
        private DevExpress.XtraGrid.Columns.GridColumn colMainIndex;
        private DevExpress.XtraGrid.Columns.GridColumn colColorStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiveMoneyMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colGuestCode;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalMoneyKoVAT;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalMoneyPO;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tiềnVềToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn colPOKHID;
        private System.Windows.Forms.ToolStripMenuItem chiTiếtPOKháchHàngToolStripMenuItem;
    }
}