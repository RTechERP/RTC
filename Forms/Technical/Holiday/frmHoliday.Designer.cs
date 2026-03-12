
namespace Forms.Technical
{
    partial class frmHoliday
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHoliday));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddSaturday = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHolidayName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHolidayDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeHoliday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nmrMonth = new System.Windows.Forms.NumericUpDown();
            this.nmrYear = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ckAllDate = new DevExpress.XtraEditors.CheckEdit();
            this.btnFind = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckAllDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator3,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator4,
            this.btnAddSaturday,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1373, 44);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(50, 41);
            this.btnNew.Tag = "frmHoliday_New";
            this.btnNew.Text = "Thêm";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 44);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(50, 41);
            this.btnEdit.Tag = "frmHoliday_Edit";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 41);
            this.btnDelete.Tag = "frmHoliday_Delete";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 44);
            // 
            // btnAddSaturday
            // 
            this.btnAddSaturday.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSaturday.Image = global::Forms.Properties.Resources.add_32x321;
            this.btnAddSaturday.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSaturday.Name = "btnAddSaturday";
            this.btnAddSaturday.Size = new System.Drawing.Size(112, 41);
            this.btnAddSaturday.Tag = "frmHoliday_New";
            this.btnAddSaturday.Text = "Thêm lịch thứ bảy";
            this.btnAddSaturday.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddSaturday.Click += new System.EventHandler(this.btnAddSaturday_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 41);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(0, 77);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(1373, 634);
            this.grdData.TabIndex = 2;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.ColumnPanelRowHeight = 30;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colHolidayName,
            this.colHolidayDate,
            this.colTypeHoliday});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // colID
            // 
            this.colID.Caption = "Mã";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colHolidayName
            // 
            this.colHolidayName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colHolidayName.AppearanceHeader.Options.UseFont = true;
            this.colHolidayName.AppearanceHeader.Options.UseForeColor = true;
            this.colHolidayName.AppearanceHeader.Options.UseTextOptions = true;
            this.colHolidayName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHolidayName.Caption = "Tên";
            this.colHolidayName.FieldName = "HolidayName";
            this.colHolidayName.Name = "colHolidayName";
            this.colHolidayName.Visible = true;
            this.colHolidayName.VisibleIndex = 1;
            this.colHolidayName.Width = 347;
            // 
            // colHolidayDate
            // 
            this.colHolidayDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colHolidayDate.AppearanceHeader.Options.UseFont = true;
            this.colHolidayDate.AppearanceHeader.Options.UseForeColor = true;
            this.colHolidayDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colHolidayDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHolidayDate.Caption = "Ngày";
            this.colHolidayDate.FieldName = "HolidayDate";
            this.colHolidayDate.Name = "colHolidayDate";
            this.colHolidayDate.Visible = true;
            this.colHolidayDate.VisibleIndex = 0;
            this.colHolidayDate.Width = 120;
            // 
            // colTypeHoliday
            // 
            this.colTypeHoliday.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeHoliday.AppearanceHeader.Options.UseFont = true;
            this.colTypeHoliday.AppearanceHeader.Options.UseForeColor = true;
            this.colTypeHoliday.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeHoliday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeHoliday.Caption = "Loại";
            this.colTypeHoliday.FieldName = "TypeHolidayText";
            this.colTypeHoliday.Name = "colTypeHoliday";
            this.colTypeHoliday.Visible = true;
            this.colTypeHoliday.VisibleIndex = 2;
            this.colTypeHoliday.Width = 308;
            // 
            // nmrMonth
            // 
            this.nmrMonth.Font = new System.Drawing.Font("Tahoma", 10F);
            this.nmrMonth.Location = new System.Drawing.Point(183, 47);
            this.nmrMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmrMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrMonth.Name = "nmrMonth";
            this.nmrMonth.Size = new System.Drawing.Size(48, 24);
            this.nmrMonth.TabIndex = 17;
            this.nmrMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmrMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrMonth.ValueChanged += new System.EventHandler(this.nmrMonth_ValueChanged);
            // 
            // nmrYear
            // 
            this.nmrYear.Font = new System.Drawing.Font("Tahoma", 10F);
            this.nmrYear.Location = new System.Drawing.Point(60, 47);
            this.nmrYear.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nmrYear.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.nmrYear.Name = "nmrYear";
            this.nmrYear.Size = new System.Drawing.Size(66, 24);
            this.nmrYear.TabIndex = 16;
            this.nmrYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmrYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nmrYear.ValueChanged += new System.EventHandler(this.nmrYear_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(18, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Năm";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label4.Location = new System.Drawing.Point(132, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Tháng";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ckAllDate
            // 
            this.ckAllDate.Location = new System.Drawing.Point(318, 49);
            this.ckAllDate.Name = "ckAllDate";
            this.ckAllDate.Properties.Caption = "Xem tất cả";
            this.ckAllDate.Size = new System.Drawing.Size(75, 20);
            this.ckAllDate.TabIndex = 18;
            this.ckAllDate.CheckedChanged += new System.EventHandler(this.ckAllDate_CheckedChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(237, 48);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 19;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // frmHoliday
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1373, 711);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.ckAllDate);
            this.Controls.Add(this.nmrMonth);
            this.Controls.Add(this.nmrYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmHoliday";
            this.Text = "NGÀY NGHỈ";
            this.Load += new System.EventHandler(this.frmHoliday_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckAllDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colHolidayName;
        private DevExpress.XtraGrid.Columns.GridColumn colHolidayDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeHoliday;
        private System.Windows.Forms.ToolStripButton btnAddSaturday;
        private System.Windows.Forms.NumericUpDown nmrMonth;
        private System.Windows.Forms.NumericUpDown nmrYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.CheckEdit ckAllDate;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}