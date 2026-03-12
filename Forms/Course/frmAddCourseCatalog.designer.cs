namespace BMS
{
    partial class frmAddCourseCatalog
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
            this.txtCodeCourseCatalog = new DevExpress.XtraEditors.TextEdit();
            this.txtCourseCatalogName = new DevExpress.XtraEditors.TextEdit();
            this.btnSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblMaDanhMuc = new DevExpress.XtraEditors.LabelControl();
            this.lblPhongBan = new DevExpress.XtraEditors.LabelControl();
            this.lblTenVanBan = new DevExpress.XtraEditors.LabelControl();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.cbxDepartment = new System.Windows.Forms.ComboBox();
            this.chkDeleteFlag = new System.Windows.Forms.CheckBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.cboCatalogType = new System.Windows.Forms.ComboBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cboPosition = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeCourseCatalog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCourseCatalogName.Properties)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPosition.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCodeCourseCatalog
            // 
            this.txtCodeCourseCatalog.Location = new System.Drawing.Point(140, 129);
            this.txtCodeCourseCatalog.Name = "txtCodeCourseCatalog";
            this.txtCodeCourseCatalog.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeCourseCatalog.Properties.Appearance.Options.UseFont = true;
            this.txtCodeCourseCatalog.Size = new System.Drawing.Size(160, 26);
            this.txtCodeCourseCatalog.TabIndex = 16;
            // 
            // txtCourseCatalogName
            // 
            this.txtCourseCatalogName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCourseCatalogName.Location = new System.Drawing.Point(140, 161);
            this.txtCourseCatalogName.Name = "txtCourseCatalogName";
            this.txtCourseCatalogName.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourseCatalogName.Properties.Appearance.Options.UseFont = true;
            this.txtCourseCatalogName.Size = new System.Drawing.Size(503, 26);
            this.txtCourseCatalogName.TabIndex = 17;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndClose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSaveAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(75, 49);
            this.btnSaveAndClose.Tag = "frmCourse_AddCourseCatalog";
            this.btnSaveAndClose.Text = "Cất && Đóng";
            this.btnSaveAndClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 49);
            this.btnSave.Tag = "frmCourse_AddCourseCatalog";
            this.btnSave.Text = "Cất && Thêm mới";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveAndClose,
            this.toolStripSeparator3,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(655, 52);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            // 
            // lblMaDanhMuc
            // 
            this.lblMaDanhMuc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDanhMuc.Appearance.Options.UseFont = true;
            this.lblMaDanhMuc.Location = new System.Drawing.Point(16, 132);
            this.lblMaDanhMuc.Name = "lblMaDanhMuc";
            this.lblMaDanhMuc.Size = new System.Drawing.Size(96, 20);
            this.lblMaDanhMuc.TabIndex = 15;
            this.lblMaDanhMuc.Text = "Mã danh mục";
            // 
            // lblPhongBan
            // 
            this.lblPhongBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhongBan.Appearance.Options.UseFont = true;
            this.lblPhongBan.Location = new System.Drawing.Point(35, 99);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(77, 20);
            this.lblPhongBan.TabIndex = 13;
            this.lblPhongBan.Text = "Phòng ban";
            // 
            // lblTenVanBan
            // 
            this.lblTenVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenVanBan.Appearance.Options.UseFont = true;
            this.lblTenVanBan.Location = new System.Drawing.Point(11, 164);
            this.lblTenVanBan.Name = "lblTenVanBan";
            this.lblTenVanBan.Size = new System.Drawing.Size(101, 20);
            this.lblTenVanBan.TabIndex = 12;
            this.lblTenVanBan.Text = "Tên danh mục";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // cbxDepartment
            // 
            this.cbxDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDepartment.FormattingEnabled = true;
            this.cbxDepartment.Location = new System.Drawing.Point(140, 95);
            this.cbxDepartment.Name = "cbxDepartment";
            this.cbxDepartment.Size = new System.Drawing.Size(503, 28);
            this.cbxDepartment.TabIndex = 9;
            // 
            // chkDeleteFlag
            // 
            this.chkDeleteFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDeleteFlag.AutoSize = true;
            this.chkDeleteFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDeleteFlag.Location = new System.Drawing.Point(540, 63);
            this.chkDeleteFlag.Name = "chkDeleteFlag";
            this.chkDeleteFlag.Size = new System.Drawing.Size(103, 24);
            this.chkDeleteFlag.TabIndex = 24;
            this.chkDeleteFlag.Text = "Hoạt động";
            this.chkDeleteFlag.UseVisualStyleBackColor = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(572, 254);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 20);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "Trạng thái";
            this.labelControl3.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(118, 99);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(10, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "(*)";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(118, 132);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(10, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "(*)";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(118, 164);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(10, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "(*)";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(306, 132);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(29, 20);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "STT";
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTT.Location = new System.Drawing.Point(341, 129);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(302, 26);
            this.txtSTT.TabIndex = 26;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboCatalogType
            // 
            this.cboCatalogType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCatalogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCatalogType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCatalogType.FormattingEnabled = true;
            this.cboCatalogType.Items.AddRange(new object[] {
            "--Chọn loại--",
            "Cơ bản",
            "Nâng cao"});
            this.cboCatalogType.Location = new System.Drawing.Point(140, 61);
            this.cboCatalogType.Name = "cboCatalogType";
            this.cboCatalogType.Size = new System.Drawing.Size(394, 28);
            this.cboCatalogType.TabIndex = 27;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(82, 64);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(30, 20);
            this.labelControl6.TabIndex = 25;
            this.labelControl6.Text = "Loại";
            this.labelControl6.Click += new System.EventHandler(this.labelControl6_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(118, 64);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(10, 13);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "(*)";
            // 
            // cboPosition
            // 
            this.cboPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPosition.EditValue = "";
            this.cboPosition.Location = new System.Drawing.Point(140, 193);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPosition.Properties.Appearance.Options.UseFont = true;
            this.cboPosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPosition.Size = new System.Drawing.Size(503, 26);
            this.cboPosition.TabIndex = 28;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(72, 196);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(40, 20);
            this.labelControl8.TabIndex = 29;
            this.labelControl8.Text = "Team";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(119, 196);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(0, 13);
            this.labelControl9.TabIndex = 30;
            // 
            // frmAddCourseCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 230);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.cboPosition);
            this.Controls.Add(this.cboCatalogType);
            this.Controls.Add(this.txtSTT);
            this.Controls.Add(this.chkDeleteFlag);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cbxDepartment);
            this.Controls.Add(this.txtCourseCatalogName);
            this.Controls.Add(this.txtCodeCourseCatalog);
            this.Controls.Add(this.lblTenVanBan);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblPhongBan);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lblMaDanhMuc);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmAddCourseCatalog";
            this.Text = "THÊM DANH MỤC KHÓA HỌC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddCourseCatalog_FormClosed);
            this.Load += new System.EventHandler(this.frmAddCourseCatalog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeCourseCatalog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCourseCatalogName.Properties)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPosition.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCodeCourseCatalog;
        private DevExpress.XtraEditors.TextEdit txtCourseCatalogName;
        private System.Windows.Forms.ToolStripButton btnSaveAndClose;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevExpress.XtraEditors.LabelControl lblMaDanhMuc;
        private DevExpress.XtraEditors.LabelControl lblPhongBan;
        private DevExpress.XtraEditors.LabelControl lblTenVanBan;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ComboBox cbxDepartment;
        private System.Windows.Forms.CheckBox chkDeleteFlag;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.ComboBox cboCatalogType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cboPosition;
    }
}