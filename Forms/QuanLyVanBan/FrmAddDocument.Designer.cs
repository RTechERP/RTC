
namespace BMS
{
	partial class FrmAddDocument
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSTT = new DevExpress.XtraEditors.LabelControl();
            this.lblLoaiVanBan = new DevExpress.XtraEditors.LabelControl();
            this.lblMaVanBan = new DevExpress.XtraEditors.LabelControl();
            this.lblTenVanBan = new DevExpress.XtraEditors.LabelControl();
            this.lblNgayBanHanh = new DevExpress.XtraEditors.LabelControl();
            this.lblNgayHieuLuc = new DevExpress.XtraEditors.LabelControl();
            this.txtCodeDocument = new DevExpress.XtraEditors.TextEdit();
            this.txtNameDocument = new DevExpress.XtraEditors.TextEdit();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.cbxDocumentType = new System.Windows.Forms.ComboBox();
            this.dtDatePromulqate = new System.Windows.Forms.DateTimePicker();
            this.dtDocumentEffetive = new System.Windows.Forms.DateTimePicker();
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.lblDepartment = new DevExpress.XtraEditors.LabelControl();
            this.cboDepartment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDepartCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblAffectedScope = new DevExpress.XtraEditors.LabelControl();
            this.chkPromulgated = new DevExpress.XtraEditors.CheckEdit();
            this.txtAffectedScope = new DevExpress.XtraEditors.MemoEdit();
            this.chkIsOnWeb = new DevExpress.XtraEditors.CheckEdit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeDocument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameDocument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPromulgated.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAffectedScope.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnWeb.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveAndClose,
            this.toolStripSeparator3,
            this.btnSave,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(446, 52);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndClose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSaveAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(75, 49);
            this.btnSaveAndClose.Tag = "FrmDocumnet_AddDoc";
            this.btnSaveAndClose.Text = "Cất && Đóng";
            this.btnSaveAndClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 49);
            this.btnSave.Tag = "FrmDocumnet_AddDoc";
            this.btnSave.Text = "Cất && Thêm mới";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 52);
            // 
            // lblSTT
            // 
            this.lblSTT.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSTT.Appearance.Options.UseFont = true;
            this.lblSTT.Location = new System.Drawing.Point(100, 64);
            this.lblSTT.Name = "lblSTT";
            this.lblSTT.Size = new System.Drawing.Size(33, 20);
            this.lblSTT.TabIndex = 2;
            this.lblSTT.Text = "STT:";
            // 
            // lblLoaiVanBan
            // 
            this.lblLoaiVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiVanBan.Appearance.Options.UseFont = true;
            this.lblLoaiVanBan.Location = new System.Drawing.Point(39, 106);
            this.lblLoaiVanBan.Name = "lblLoaiVanBan";
            this.lblLoaiVanBan.Size = new System.Drawing.Size(94, 20);
            this.lblLoaiVanBan.TabIndex = 2;
            this.lblLoaiVanBan.Text = "Loại văn bản:";
            // 
            // lblMaVanBan
            // 
            this.lblMaVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaVanBan.Appearance.Options.UseFont = true;
            this.lblMaVanBan.Location = new System.Drawing.Point(47, 156);
            this.lblMaVanBan.Name = "lblMaVanBan";
            this.lblMaVanBan.Size = new System.Drawing.Size(86, 20);
            this.lblMaVanBan.TabIndex = 2;
            this.lblMaVanBan.Text = "Mã văn bản:";
            // 
            // lblTenVanBan
            // 
            this.lblTenVanBan.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenVanBan.Appearance.Options.UseFont = true;
            this.lblTenVanBan.Location = new System.Drawing.Point(42, 202);
            this.lblTenVanBan.Name = "lblTenVanBan";
            this.lblTenVanBan.Size = new System.Drawing.Size(91, 20);
            this.lblTenVanBan.TabIndex = 2;
            this.lblTenVanBan.Text = "Tên văn bản:";
            // 
            // lblNgayBanHanh
            // 
            this.lblNgayBanHanh.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayBanHanh.Appearance.Options.UseFont = true;
            this.lblNgayBanHanh.Location = new System.Drawing.Point(22, 288);
            this.lblNgayBanHanh.Name = "lblNgayBanHanh";
            this.lblNgayBanHanh.Size = new System.Drawing.Size(111, 20);
            this.lblNgayBanHanh.TabIndex = 2;
            this.lblNgayBanHanh.Text = "Ngày ban hành:";
            // 
            // lblNgayHieuLuc
            // 
            this.lblNgayHieuLuc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayHieuLuc.Appearance.Options.UseFont = true;
            this.lblNgayHieuLuc.Location = new System.Drawing.Point(35, 334);
            this.lblNgayHieuLuc.Name = "lblNgayHieuLuc";
            this.lblNgayHieuLuc.Size = new System.Drawing.Size(98, 20);
            this.lblNgayHieuLuc.TabIndex = 2;
            this.lblNgayHieuLuc.Text = "Ngày hiệu lực:";
            // 
            // txtCodeDocument
            // 
            this.txtCodeDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodeDocument.Location = new System.Drawing.Point(148, 153);
            this.txtCodeDocument.Name = "txtCodeDocument";
            this.txtCodeDocument.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeDocument.Properties.Appearance.Options.UseFont = true;
            this.txtCodeDocument.Size = new System.Drawing.Size(286, 26);
            this.txtCodeDocument.TabIndex = 3;
            // 
            // txtNameDocument
            // 
            this.txtNameDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameDocument.Location = new System.Drawing.Point(148, 199);
            this.txtNameDocument.Name = "txtNameDocument";
            this.txtNameDocument.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameDocument.Properties.Appearance.Options.UseFont = true;
            this.txtNameDocument.Size = new System.Drawing.Size(286, 26);
            this.txtNameDocument.TabIndex = 4;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // cbxDocumentType
            // 
            this.cbxDocumentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDocumentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDocumentType.FormattingEnabled = true;
            this.cbxDocumentType.Location = new System.Drawing.Point(148, 102);
            this.cbxDocumentType.Name = "cbxDocumentType";
            this.cbxDocumentType.Size = new System.Drawing.Size(286, 28);
            this.cbxDocumentType.TabIndex = 2;
            // 
            // dtDatePromulqate
            // 
            this.dtDatePromulqate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDatePromulqate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDatePromulqate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDatePromulqate.Location = new System.Drawing.Point(148, 285);
            this.dtDatePromulqate.Name = "dtDatePromulqate";
            this.dtDatePromulqate.Size = new System.Drawing.Size(286, 26);
            this.dtDatePromulqate.TabIndex = 5;
            // 
            // dtDocumentEffetive
            // 
            this.dtDocumentEffetive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDocumentEffetive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDocumentEffetive.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDocumentEffetive.Location = new System.Drawing.Point(148, 331);
            this.dtDocumentEffetive.Name = "dtDocumentEffetive";
            this.dtDocumentEffetive.Size = new System.Drawing.Size(286, 26);
            this.dtDocumentEffetive.TabIndex = 6;
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtSTT.Location = new System.Drawing.Point(148, 64);
            this.txtSTT.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtSTT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(286, 26);
            this.txtSTT.TabIndex = 7;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSTT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDepartment
            // 
            this.lblDepartment.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Appearance.Options.UseFont = true;
            this.lblDepartment.Location = new System.Drawing.Point(52, 245);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(81, 20);
            this.lblDepartment.TabIndex = 8;
            this.lblDepartment.Text = "Phòng ban:";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDepartment.Location = new System.Drawing.Point(148, 242);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.Properties.Appearance.Options.UseFont = true;
            this.cboDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDepartment.Properties.NullText = "";
            this.cboDepartment.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboDepartment.Size = new System.Drawing.Size(286, 26);
            this.cboDepartment.TabIndex = 9;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDepartCode,
            this.colDepartName});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colDepartCode
            // 
            this.colDepartCode.Caption = "Mã phòng ban";
            this.colDepartCode.FieldName = "Code";
            this.colDepartCode.Name = "colDepartCode";
            this.colDepartCode.Visible = true;
            this.colDepartCode.VisibleIndex = 0;
            this.colDepartCode.Width = 50;
            // 
            // colDepartName
            // 
            this.colDepartName.Caption = "Tên phòng ban";
            this.colDepartName.FieldName = "Name";
            this.colDepartName.Name = "colDepartName";
            this.colDepartName.Visible = true;
            this.colDepartName.VisibleIndex = 1;
            this.colDepartName.Width = 200;
            // 
            // lblAffectedScope
            // 
            this.lblAffectedScope.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAffectedScope.Appearance.Options.UseFont = true;
            this.lblAffectedScope.Location = new System.Drawing.Point(12, 380);
            this.lblAffectedScope.Name = "lblAffectedScope";
            this.lblAffectedScope.Size = new System.Drawing.Size(121, 20);
            this.lblAffectedScope.TabIndex = 10;
            this.lblAffectedScope.Text = "Phạm vi áp dụng:";
            // 
            // chkPromulgated
            // 
            this.chkPromulgated.Location = new System.Drawing.Point(302, 445);
            this.chkPromulgated.Name = "chkPromulgated";
            this.chkPromulgated.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPromulgated.Properties.Appearance.Options.UseFont = true;
            this.chkPromulgated.Properties.Caption = "Đã ban hành";
            this.chkPromulgated.Size = new System.Drawing.Size(132, 24);
            this.chkPromulgated.TabIndex = 13;
            // 
            // txtAffectedScope
            // 
            this.txtAffectedScope.Location = new System.Drawing.Point(148, 379);
            this.txtAffectedScope.Name = "txtAffectedScope";
            this.txtAffectedScope.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtAffectedScope.Properties.Appearance.Options.UseFont = true;
            this.txtAffectedScope.Size = new System.Drawing.Size(286, 60);
            this.txtAffectedScope.TabIndex = 14;
            // 
            // chkIsOnWeb
            // 
            this.chkIsOnWeb.Location = new System.Drawing.Point(148, 445);
            this.chkIsOnWeb.Name = "chkIsOnWeb";
            this.chkIsOnWeb.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.chkIsOnWeb.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsOnWeb.Properties.Appearance.Options.UseBackColor = true;
            this.chkIsOnWeb.Properties.Appearance.Options.UseFont = true;
            this.chkIsOnWeb.Properties.Caption = "Có trên web";
            this.chkIsOnWeb.Size = new System.Drawing.Size(117, 24);
            this.chkIsOnWeb.TabIndex = 16;
            // 
            // FrmAddDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 500);
            this.Controls.Add(this.chkIsOnWeb);
            this.Controls.Add(this.txtAffectedScope);
            this.Controls.Add(this.chkPromulgated);
            this.Controls.Add(this.lblAffectedScope);
            this.Controls.Add(this.cboDepartment);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.txtSTT);
            this.Controls.Add(this.dtDocumentEffetive);
            this.Controls.Add(this.dtDatePromulqate);
            this.Controls.Add(this.cbxDocumentType);
            this.Controls.Add(this.txtNameDocument);
            this.Controls.Add(this.txtCodeDocument);
            this.Controls.Add(this.lblNgayHieuLuc);
            this.Controls.Add(this.lblNgayBanHanh);
            this.Controls.Add(this.lblTenVanBan);
            this.Controls.Add(this.lblMaVanBan);
            this.Controls.Add(this.lblLoaiVanBan);
            this.Controls.Add(this.lblSTT);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmAddDocument";
            this.Text = "CHI TIẾT VĂN BẢN";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAddDocument_FormClosed);
            this.Load += new System.EventHandler(this.FrmAddDocument_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeDocument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameDocument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPromulgated.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAffectedScope.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnWeb.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnSaveAndClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private DevExpress.XtraEditors.LabelControl lblSTT;
		private DevExpress.XtraEditors.LabelControl lblLoaiVanBan;
		private DevExpress.XtraEditors.LabelControl lblMaVanBan;
		private DevExpress.XtraEditors.LabelControl lblTenVanBan;
		private DevExpress.XtraEditors.LabelControl lblNgayBanHanh;
		private DevExpress.XtraEditors.LabelControl lblNgayHieuLuc;
		private DevExpress.XtraEditors.TextEdit txtCodeDocument;
		private DevExpress.XtraEditors.TextEdit txtNameDocument;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private System.Windows.Forms.ComboBox cbxDocumentType;
		private System.Windows.Forms.DateTimePicker dtDatePromulqate;
		private System.Windows.Forms.DateTimePicker dtDocumentEffetive;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private DevExpress.XtraEditors.LabelControl lblDepartment;
        private DevExpress.XtraEditors.SearchLookUpEdit cboDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartName;
        private DevExpress.XtraEditors.LabelControl lblAffectedScope;
        private DevExpress.XtraEditors.CheckEdit chkPromulgated;
        private DevExpress.XtraEditors.MemoEdit txtAffectedScope;
        private DevExpress.XtraEditors.CheckEdit chkIsOnWeb;
    }
}