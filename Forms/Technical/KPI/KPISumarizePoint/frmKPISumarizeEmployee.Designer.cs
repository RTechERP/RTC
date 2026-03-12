
namespace BMS
{
    partial class frmKPISumarizeEmployee
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.btnReload = new System.Windows.Forms.Button();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserTeam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserTeamID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQuarter = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnLoadData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEvaluated = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEvaluationKPI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.spreadsheetControl1 = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuarter)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("8b0593c2-87da-4036-93b1-8774d95f25f2");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(439, 200);
            this.dockPanel1.Size = new System.Drawing.Size(439, 712);
            this.dockPanel1.Text = "NHÂN VIÊN";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.btnReload);
            this.dockPanel1_Container.Controls.Add(this.grdData);
            this.dockPanel1_Container.Controls.Add(this.txtYear);
            this.dockPanel1_Container.Controls.Add(this.label4);
            this.dockPanel1_Container.Controls.Add(this.txtQuarter);
            this.dockPanel1_Container.Controls.Add(this.label3);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(2);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(432, 683);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.AutoSize = true;
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Location = new System.Drawing.Point(349, 9);
            this.btnReload.Margin = new System.Windows.Forms.Padding(2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(81, 26);
            this.btnReload.TabIndex = 137;
            this.btnReload.Text = "Tìm kiếm";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Location = new System.Drawing.Point(8, 39);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(422, 636);
            this.grdData.TabIndex = 136;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserTeam,
            this.colEmployeeCode,
            this.colEmployeeName,
            this.colStatusText,
            this.colUserTeamID,
            this.colEmployeeID,
            this.colStatus});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colUserTeam, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvData.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.grvData_RowStyle);
            // 
            // colUserTeam
            // 
            this.colUserTeam.Caption = "TEAM";
            this.colUserTeam.FieldName = "UserTeam";
            this.colUserTeam.MinWidth = 19;
            this.colUserTeam.Name = "colUserTeam";
            this.colUserTeam.Visible = true;
            this.colUserTeam.VisibleIndex = 0;
            this.colUserTeam.Width = 70;
            // 
            // colEmployeeCode
            // 
            this.colEmployeeCode.Caption = "Mã nhân viên";
            this.colEmployeeCode.FieldName = "EmployeeCode";
            this.colEmployeeCode.MinWidth = 19;
            this.colEmployeeCode.Name = "colEmployeeCode";
            this.colEmployeeCode.Visible = true;
            this.colEmployeeCode.VisibleIndex = 0;
            this.colEmployeeCode.Width = 69;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Caption = "Tên nhân viên";
            this.colEmployeeName.FieldName = "EmployeeName";
            this.colEmployeeName.MinWidth = 19;
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 1;
            this.colEmployeeName.Width = 111;
            // 
            // colStatusText
            // 
            this.colStatusText.Caption = "Trạng thái";
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.MinWidth = 19;
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 2;
            this.colStatusText.Width = 93;
            // 
            // colUserTeamID
            // 
            this.colUserTeamID.Caption = "UserTeamID";
            this.colUserTeamID.FieldName = "UserTeamID";
            this.colUserTeamID.MinWidth = 19;
            this.colUserTeamID.Name = "colUserTeamID";
            this.colUserTeamID.Width = 70;
            // 
            // colEmployeeID
            // 
            this.colEmployeeID.Caption = "EmployeeID";
            this.colEmployeeID.FieldName = "EmployeeID";
            this.colEmployeeID.MinWidth = 19;
            this.colEmployeeID.Name = "colEmployeeID";
            this.colEmployeeID.Width = 70;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.MinWidth = 19;
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 70;
            // 
            // txtYear
            // 
            this.txtYear.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(46, 11);
            this.txtYear.Margin = new System.Windows.Forms.Padding(2);
            this.txtYear.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.txtYear.Minimum = new decimal(new int[] {
            2024,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(90, 24);
            this.txtYear.TabIndex = 132;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYear.Value = new decimal(new int[] {
            2024,
            0,
            0,
            0});
            this.txtYear.ValueChanged += new System.EventHandler(this.txtYear_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(141, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 16);
            this.label4.TabIndex = 135;
            this.label4.Text = "Quý";
            // 
            // txtQuarter
            // 
            this.txtQuarter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuarter.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuarter.Location = new System.Drawing.Point(176, 11);
            this.txtQuarter.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuarter.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.txtQuarter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtQuarter.Name = "txtQuarter";
            this.txtQuarter.Size = new System.Drawing.Size(162, 24);
            this.txtQuarter.TabIndex = 133;
            this.txtQuarter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuarter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtQuarter.ValueChanged += new System.EventHandler(this.txtQuarter_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 134;
            this.label3.Text = "Năm";
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadData,
            this.toolStripSeparator2,
            this.btnEvaluated,
            this.toolStripSeparator11,
            this.btnEvaluationKPI,
            this.toolStripSeparator1});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(439, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(875, 55);
            this.mnuMenu.TabIndex = 127;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadData.Image = global::Forms.Properties.Resources.InsertTableOfCaptions_32x32;
            this.btnLoadData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLoadData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(90, 52);
            this.btnLoadData.Tag = "";
            this.btnLoadData.Text = "Load dữ liệu";
            this.btnLoadData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // btnEvaluated
            // 
            this.btnEvaluated.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEvaluated.Image = global::Forms.Properties.Resources.clipboard;
            this.btnEvaluated.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEvaluated.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEvaluated.Name = "btnEvaluated";
            this.btnEvaluated.Size = new System.Drawing.Size(133, 52);
            this.btnEvaluated.Tag = "frmKPISumarizeEmployee_Evaluated";
            this.btnEvaluated.Text = "Tạo phiếu đánh giá";
            this.btnEvaluated.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEvaluated.Click += new System.EventHandler(this.btnEvaluated_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.AutoSize = false;
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 50);
            // 
            // btnEvaluationKPI
            // 
            this.btnEvaluationKPI.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEvaluationKPI.Image = global::Forms.Properties.Resources.UserGroup_32x32;
            this.btnEvaluationKPI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEvaluationKPI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEvaluationKPI.Name = "btnEvaluationKPI";
            this.btnEvaluationKPI.Size = new System.Drawing.Size(94, 52);
            this.btnEvaluationKPI.Tag = "frmKPISumarizeEmployee_EvaluationKPI";
            this.btnEvaluationKPI.Text = "TBP đánh giá";
            this.btnEvaluationKPI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEvaluationKPI.Click += new System.EventHandler(this.btnEvaluationKPI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // spreadsheetControl1
            // 
            this.spreadsheetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetControl1.Location = new System.Drawing.Point(439, 55);
            this.spreadsheetControl1.Margin = new System.Windows.Forms.Padding(2);
            this.spreadsheetControl1.Name = "spreadsheetControl1";
            this.spreadsheetControl1.ReadOnly = true;
            this.spreadsheetControl1.Size = new System.Drawing.Size(875, 657);
            this.spreadsheetControl1.TabIndex = 130;
            this.spreadsheetControl1.Text = "spreadsheetControl1";
            // 
            // frmKPISumarizeEmployee
            // 
            this.AcceptButton = this.btnReload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 712);
            this.Controls.Add(this.spreadsheetControl1);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.dockPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmKPISumarizeEmployee";
            this.Text = "TỔNG HỢP KPI";
            this.Load += new System.EventHandler(this.frmKPISumarizeEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuarter)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnEvaluated;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private System.Windows.Forms.NumericUpDown txtYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtQuarter;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl1;
        private System.Windows.Forms.ToolStripButton btnEvaluationKPI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnLoadData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnReload;
        private DevExpress.XtraGrid.Columns.GridColumn colUserTeam;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraGrid.Columns.GridColumn colUserTeamID;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
    }
}