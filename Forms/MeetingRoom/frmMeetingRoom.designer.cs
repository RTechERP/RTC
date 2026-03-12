
namespace BMS
{
    partial class frmMeetingRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeetingRoom));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grdRoomOne = new DevExpress.XtraGrid.GridControl();
            this.grvRoomOne = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDayOfWeek = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandRoom1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.grdRoomTwo = new DevExpress.XtraGrid.GridControl();
            this.grvRoomTwo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDate2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDayOfWeek2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandRoom2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.grdRoomThree = new DevExpress.XtraGrid.GridControl();
            this.grvRoomThree = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandRoom3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnApproved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.stackPanel3 = new DevExpress.Utils.Layout.StackPanel();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoomOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoomOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoomTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoomTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoomThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoomThree)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel3)).BeginInit();
            this.stackPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(176, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // btnLoad
            // 
            this.btnLoad.AutoSize = true;
            this.btnLoad.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoad.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(356, 6);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(79, 26);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Tìm kiếm";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // grdRoomOne
            // 
            this.grdRoomOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoomOne.Location = new System.Drawing.Point(0, 0);
            this.grdRoomOne.MainView = this.grvRoomOne;
            this.grdRoomOne.Name = "grdRoomOne";
            this.grdRoomOne.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdRoomOne.Size = new System.Drawing.Size(1295, 535);
            this.grdRoomOne.TabIndex = 0;
            this.grdRoomOne.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvRoomOne});
            // 
            // grvRoomOne
            // 
            this.grvRoomOne.ActiveFilterEnabled = false;
            this.grvRoomOne.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvRoomOne.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvRoomOne.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvRoomOne.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvRoomOne.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoomOne.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvRoomOne.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvRoomOne.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvRoomOne.Appearance.Row.Options.UseFont = true;
            this.grvRoomOne.Appearance.Row.Options.UseTextOptions = true;
            this.grvRoomOne.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvRoomOne.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvRoomOne.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.bandRoom1});
            this.grvRoomOne.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colDate,
            this.colDayOfWeek});
            this.grvRoomOne.GridControl = this.grdRoomOne;
            this.grvRoomOne.Name = "grvRoomOne";
            this.grvRoomOne.OptionsBehavior.Editable = false;
            this.grvRoomOne.OptionsBehavior.ReadOnly = true;
            this.grvRoomOne.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.grvRoomOne.OptionsView.ColumnAutoWidth = false;
            this.grvRoomOne.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvRoomOne.OptionsView.RowAutoHeight = true;
            this.grvRoomOne.OptionsView.ShowAutoFilterRow = true;
            this.grvRoomOne.OptionsView.ShowGroupPanel = false;
            this.grvRoomOne.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvRoomOne_CustomDrawCell);
            this.grvRoomOne.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvRoomOne_RowCellStyle);
            this.grvRoomOne.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvRoomOne_CustomColumnDisplayText);
            this.grvRoomOne.DoubleClick += new System.EventHandler(this.grvRoomOne_DoubleClick);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand1.Caption = "BẢNG THEO DÕI ĐĂNG KÝ PHÒNG HỌP SỐ 1 (HỒ TÂY)";
            this.gridBand1.Columns.Add(this.colDate);
            this.gridBand1.Columns.Add(this.colDayOfWeek);
            this.gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand1.MinWidth = 236;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 236;
            // 
            // colDate
            // 
            this.colDate.Caption = "Ngày";
            this.colDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate.FieldName = "AllDate";
            this.colDate.Name = "colDate";
            this.colDate.Visible = true;
            this.colDate.Width = 173;
            // 
            // colDayOfWeek
            // 
            this.colDayOfWeek.Caption = "Thứ";
            this.colDayOfWeek.FieldName = "DayOfWeek";
            this.colDayOfWeek.MinWidth = 63;
            this.colDayOfWeek.Name = "colDayOfWeek";
            this.colDayOfWeek.Visible = true;
            this.colDayOfWeek.Width = 63;
            // 
            // bandRoom1
            // 
            this.bandRoom1.Name = "bandRoom1";
            this.bandRoom1.VisibleIndex = 1;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // grdRoomTwo
            // 
            this.grdRoomTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoomTwo.Location = new System.Drawing.Point(0, 0);
            this.grdRoomTwo.MainView = this.grvRoomTwo;
            this.grdRoomTwo.Name = "grdRoomTwo";
            this.grdRoomTwo.Size = new System.Drawing.Size(1295, 535);
            this.grdRoomTwo.TabIndex = 1;
            this.grdRoomTwo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvRoomTwo});
            this.grdRoomTwo.Click += new System.EventHandler(this.grdRoomTwo_Click);
            // 
            // grvRoomTwo
            // 
            this.grvRoomTwo.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvRoomTwo.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvRoomTwo.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvRoomTwo.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvRoomTwo.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoomTwo.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvRoomTwo.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvRoomTwo.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvRoomTwo.Appearance.Row.Options.UseFont = true;
            this.grvRoomTwo.Appearance.Row.Options.UseTextOptions = true;
            this.grvRoomTwo.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvRoomTwo.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvRoomTwo.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.bandRoom2});
            this.grvRoomTwo.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colDate2,
            this.colDayOfWeek2});
            this.grvRoomTwo.GridControl = this.grdRoomTwo;
            this.grvRoomTwo.Name = "grvRoomTwo";
            this.grvRoomTwo.OptionsBehavior.Editable = false;
            this.grvRoomTwo.OptionsBehavior.ReadOnly = true;
            this.grvRoomTwo.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.grvRoomTwo.OptionsView.ColumnAutoWidth = false;
            this.grvRoomTwo.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvRoomTwo.OptionsView.RowAutoHeight = true;
            this.grvRoomTwo.OptionsView.ShowAutoFilterRow = true;
            this.grvRoomTwo.OptionsView.ShowGroupPanel = false;
            this.grvRoomTwo.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvRoomTwo_RowCellStyle);
            this.grvRoomTwo.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvRoomTwo_CustomColumnDisplayText);
            this.grvRoomTwo.DoubleClick += new System.EventHandler(this.grvRoomTwo_DoubleClick);
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridBand2.AppearanceHeader.Options.UseFont = true;
            this.gridBand2.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand2.Caption = "BẢNG THEO DÕI ĐĂNG KÝ PHÒNG HỌP SỐ 2 (HỒ GƯƠM)";
            this.gridBand2.Columns.Add(this.colDate2);
            this.gridBand2.Columns.Add(this.colDayOfWeek2);
            this.gridBand2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand2.MinWidth = 236;
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 236;
            // 
            // colDate2
            // 
            this.colDate2.Caption = "Ngày";
            this.colDate2.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colDate2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate2.FieldName = "AllDate";
            this.colDate2.Name = "colDate2";
            this.colDate2.Visible = true;
            this.colDate2.Width = 173;
            // 
            // colDayOfWeek2
            // 
            this.colDayOfWeek2.Caption = "Thứ";
            this.colDayOfWeek2.FieldName = "DayOfWeek";
            this.colDayOfWeek2.MinWidth = 63;
            this.colDayOfWeek2.Name = "colDayOfWeek2";
            this.colDayOfWeek2.Visible = true;
            this.colDayOfWeek2.Width = 63;
            // 
            // bandRoom2
            // 
            this.bandRoom2.Name = "bandRoom2";
            this.bandRoom2.VisibleIndex = 1;
            // 
            // grdRoomThree
            // 
            this.grdRoomThree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoomThree.Location = new System.Drawing.Point(0, 0);
            this.grdRoomThree.MainView = this.grvRoomThree;
            this.grdRoomThree.Name = "grdRoomThree";
            this.grdRoomThree.Size = new System.Drawing.Size(1295, 535);
            this.grdRoomThree.TabIndex = 2;
            this.grdRoomThree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvRoomThree});
            // 
            // grvRoomThree
            // 
            this.grvRoomThree.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvRoomThree.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvRoomThree.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvRoomThree.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvRoomThree.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvRoomThree.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvRoomThree.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvRoomThree.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvRoomThree.Appearance.Row.Options.UseFont = true;
            this.grvRoomThree.Appearance.Row.Options.UseTextOptions = true;
            this.grvRoomThree.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvRoomThree.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvRoomThree.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand3,
            this.bandRoom3});
            this.grvRoomThree.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn2});
            this.grvRoomThree.GridControl = this.grdRoomThree;
            this.grvRoomThree.Name = "grvRoomThree";
            this.grvRoomThree.OptionsBehavior.Editable = false;
            this.grvRoomThree.OptionsBehavior.ReadOnly = true;
            this.grvRoomThree.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.grvRoomThree.OptionsView.ColumnAutoWidth = false;
            this.grvRoomThree.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvRoomThree.OptionsView.RowAutoHeight = true;
            this.grvRoomThree.OptionsView.ShowAutoFilterRow = true;
            this.grvRoomThree.OptionsView.ShowGroupPanel = false;
            this.grvRoomThree.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvRoomThree_RowCellStyle);
            this.grvRoomThree.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grvRoomThree_CustomColumnDisplayText);
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridBand3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridBand3.Caption = "BẢNG THEO DÕI ĐĂNG KÝ PHÒNG HỌP SỐ 3 (HỒ TRÚC BẠCH)";
            this.gridBand3.Columns.Add(this.bandedGridColumn1);
            this.gridBand3.Columns.Add(this.bandedGridColumn2);
            this.gridBand3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand3.MinWidth = 236;
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 0;
            this.gridBand3.Width = 236;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Ngày";
            this.bandedGridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.bandedGridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.bandedGridColumn1.FieldName = "AllDate";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 173;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Thứ";
            this.bandedGridColumn2.FieldName = "DayOfWeek";
            this.bandedGridColumn2.MinWidth = 63;
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 63;
            // 
            // bandRoom3
            // 
            this.bandRoom3.Name = "bandRoom3";
            this.bandRoom3.VisibleIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator4,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnApproved,
            this.toolStripSeparator5,
            this.btnUnApproved,
            this.toolStripSeparator1,
            this.btnExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1297, 44);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 40);
            this.btnNew.Tag = "frmMeetingRoom_New";
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
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 40);
            this.btnEdit.Tag = "frmMeetingRoom_Edit";
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
            this.btnDelete.Image = global::Forms.Properties.Resources.Clear_32x32;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 40);
            this.btnDelete.Tag = "frmMeetingRoom_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // btnApproved
            // 
            this.btnApproved.AutoSize = false;
            this.btnApproved.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnApproved.Image = global::Forms.Properties.Resources.Apply_16x16;
            this.btnApproved.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApproved.Name = "btnApproved";
            this.btnApproved.Size = new System.Drawing.Size(80, 41);
            this.btnApproved.Tag = "frmMeetingRoom_Approved";
            this.btnApproved.Text = "Duyệt";
            this.btnApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApproved.Click += new System.EventHandler(this.btnIsApproved_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 44);
            // 
            // btnUnApproved
            // 
            this.btnUnApproved.AutoSize = false;
            this.btnUnApproved.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnUnApproved.Image = global::Forms.Properties.Resources.icons8_close_16;
            this.btnUnApproved.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUnApproved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnApproved.Name = "btnUnApproved";
            this.btnUnApproved.Size = new System.Drawing.Size(80, 41);
            this.btnUnApproved.Tag = "frmMeetingRoom_UnApproved";
            this.btnUnApproved.Text = "Huỷ duyệt";
            this.btnUnApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnApproved.Click += new System.EventHandler(this.btnCancelApprove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = false;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(80, 41);
            this.btnExcel.Tag = "";
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(63, 8);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(107, 22);
            this.dtpStartDate.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(243, 8);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(107, 22);
            this.dtpEndDate.TabIndex = 5;
            // 
            // stackPanel3
            // 
            this.stackPanel3.Controls.Add(this.label1);
            this.stackPanel3.Controls.Add(this.dtpStartDate);
            this.stackPanel3.Controls.Add(this.label2);
            this.stackPanel3.Controls.Add(this.dtpEndDate);
            this.stackPanel3.Controls.Add(this.btnLoad);
            this.stackPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.stackPanel3.Location = new System.Drawing.Point(0, 44);
            this.stackPanel3.Name = "stackPanel3";
            this.stackPanel3.Size = new System.Drawing.Size(1297, 38);
            this.stackPanel3.TabIndex = 6;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 82);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1297, 560);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage1.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage1.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage1.Controls.Add(this.grdRoomOne);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1295, 535);
            this.xtraTabPage1.Text = "MEETING ROOM 1 (HỒ TÂY)";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage2.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage2.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage2.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage2.Controls.Add(this.grdRoomTwo);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1295, 535);
            this.xtraTabPage2.Text = "MEETING ROOM 2 (HỒ GƯƠM)";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage3.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage3.Appearance.HeaderActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage3.Appearance.HeaderActive.Options.UseFont = true;
            this.xtraTabPage3.Controls.Add(this.grdRoomThree);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1295, 535);
            this.xtraTabPage3.Text = "MEETING ROOM 3 (HỒ TRÚC BẠCH)";
            // 
            // frmMeetingRoom
            // 
            this.AcceptButton = this.btnLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 642);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.stackPanel3);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmMeetingRoom";
            this.Text = "PHÒNG HỌP";
            this.Load += new System.EventHandler(this.frmMeetingRoom_Load);
            this.Resize += new System.EventHandler(this.frmMeetingRoom_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoomOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoomOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoomTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoomTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoomThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvRoomThree)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel3)).EndInit();
            this.stackPanel3.ResumeLayout(false);
            this.stackPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl grdRoomOne;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvRoomOne;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDayOfWeek;
        private DevExpress.XtraGrid.GridControl grdRoomTwo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvRoomTwo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDate2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDayOfWeek2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private DevExpress.Utils.Layout.StackPanel stackPanel3;
        private System.Windows.Forms.ToolStripButton btnApproved;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnUnApproved;
        private DevExpress.XtraGrid.GridControl grdRoomThree;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvRoomThree;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandRoom1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandRoom2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandRoom3;
    }
}