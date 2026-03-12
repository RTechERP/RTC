
namespace BMS
{
    partial class frmRegisterIdeaType
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
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.STT = new DevExpress.XtraEditors.LabelControl();
            this.lblTenKhoaHoc = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblDanhMuc = new DevExpress.XtraEditors.LabelControl();
            this.txtRegisterTypeName = new DevExpress.XtraEditors.TextEdit();
            this.lblMaKhoahoc = new DevExpress.XtraEditors.LabelControl();
            this.txtRegisterTypeCode = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterTypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterTypeCode.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(536, 52);
            this.toolStrip1.TabIndex = 21;
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
            this.btnSaveAndClose.Tag = "frmCourse_AddCourse";
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
            this.btnSave.Tag = "frmCourse_AddCourse";
            this.btnSave.Text = "Cất && Thêm mới";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Enabled = false;
            this.txtSTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTT.Location = new System.Drawing.Point(418, 51);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(95, 26);
            this.txtSTT.TabIndex = 28;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // STT
            // 
            this.STT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.STT.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STT.Appearance.Options.UseFont = true;
            this.STT.Location = new System.Drawing.Point(367, 53);
            this.STT.Name = "STT";
            this.STT.Size = new System.Drawing.Size(29, 20);
            this.STT.TabIndex = 30;
            this.STT.Text = "STT";
            // 
            // lblTenKhoaHoc
            // 
            this.lblTenKhoaHoc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenKhoaHoc.Appearance.Options.UseFont = true;
            this.lblTenKhoaHoc.Location = new System.Drawing.Point(11, 117);
            this.lblTenKhoaHoc.Name = "lblTenKhoaHoc";
            this.lblTenKhoaHoc.Size = new System.Drawing.Size(55, 20);
            this.lblTenKhoaHoc.TabIndex = 31;
            this.lblTenKhoaHoc.Text = "Ghi chú";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(90, 84);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(16, 20);
            this.labelControl5.TabIndex = 33;
            this.labelControl5.Text = "(*)";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(90, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(16, 20);
            this.labelControl4.TabIndex = 34;
            this.labelControl4.Text = "(*)";
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhMuc.Appearance.Options.UseFont = true;
            this.lblDanhMuc.Location = new System.Drawing.Point(11, 51);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(65, 20);
            this.lblDanhMuc.TabIndex = 35;
            this.lblDanhMuc.Text = "Mã đề tài";
            // 
            // txtRegisterTypeName
            // 
            this.txtRegisterTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterTypeName.Location = new System.Drawing.Point(135, 84);
            this.txtRegisterTypeName.Name = "txtRegisterTypeName";
            this.txtRegisterTypeName.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegisterTypeName.Properties.Appearance.Options.UseFont = true;
            this.txtRegisterTypeName.Size = new System.Drawing.Size(378, 26);
            this.txtRegisterTypeName.TabIndex = 27;
            // 
            // lblMaKhoahoc
            // 
            this.lblMaKhoahoc.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaKhoahoc.Appearance.Options.UseFont = true;
            this.lblMaKhoahoc.Location = new System.Drawing.Point(11, 84);
            this.lblMaKhoahoc.Name = "lblMaKhoahoc";
            this.lblMaKhoahoc.Size = new System.Drawing.Size(70, 20);
            this.lblMaKhoahoc.TabIndex = 36;
            this.lblMaKhoahoc.Text = "Tên đề tài";
            // 
            // txtRegisterTypeCode
            // 
            this.txtRegisterTypeCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegisterTypeCode.Location = new System.Drawing.Point(135, 50);
            this.txtRegisterTypeCode.Name = "txtRegisterTypeCode";
            this.txtRegisterTypeCode.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegisterTypeCode.Properties.Appearance.Options.UseFont = true;
            this.txtRegisterTypeCode.Size = new System.Drawing.Size(213, 26);
            this.txtRegisterTypeCode.TabIndex = 38;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(135, 116);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(378, 181);
            this.txtNote.TabIndex = 39;
            // 
            // frmRegisterIdeaType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 309);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtRegisterTypeCode);
            this.Controls.Add(this.txtSTT);
            this.Controls.Add(this.STT);
            this.Controls.Add(this.lblTenKhoaHoc);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.lblDanhMuc);
            this.Controls.Add(this.txtRegisterTypeName);
            this.Controls.Add(this.lblMaKhoahoc);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmRegisterIdeaType";
            this.Text = "THÊM LOẠI ĐỀ TÀI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRegisterIdeaType_FormClosed);
            this.Load += new System.EventHandler(this.frmRegisterIdeaType_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterTypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterTypeCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveAndClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private DevExpress.XtraEditors.LabelControl STT;
        private DevExpress.XtraEditors.LabelControl lblTenKhoaHoc;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblDanhMuc;
        private DevExpress.XtraEditors.TextEdit txtRegisterTypeName;
        private DevExpress.XtraEditors.LabelControl lblMaKhoahoc;
        private DevExpress.XtraEditors.TextEdit txtRegisterTypeCode;
        private System.Windows.Forms.TextBox txtNote;
    }
}