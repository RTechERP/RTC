
namespace Forms.keToan.Tax.TaxUserDetail
{
    partial class ucTaxUserContract
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddLoaiHD = new System.Windows.Forms.Button();
            this.rdChuaky = new System.Windows.Forms.RadioButton();
            this.rdDaky = new System.Windows.Forms.RadioButton();
            this.cboLoaiHD = new System.Windows.Forms.ComboBox();
            this.txtTinhTrangKyHD = new System.Windows.Forms.TextBox();
            this.txtSoHD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.dtpDateSign = new DevExpress.XtraEditors.DateEdit();
            this.dtpDateStart = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateSign.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateSign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddLoaiHD
            // 
            this.btnAddLoaiHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLoaiHD.AutoSize = true;
            this.btnAddLoaiHD.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLoaiHD.Image = global::Forms.Properties.Resources.add_16x16;
            this.btnAddLoaiHD.Location = new System.Drawing.Point(689, 14);
            this.btnAddLoaiHD.Name = "btnAddLoaiHD";
            this.btnAddLoaiHD.Size = new System.Drawing.Size(31, 29);
            this.btnAddLoaiHD.TabIndex = 100;
            this.btnAddLoaiHD.UseVisualStyleBackColor = true;
            this.btnAddLoaiHD.Click += new System.EventHandler(this.btnAddLoaiHD_Click);
            // 
            // rdChuaky
            // 
            this.rdChuaky.AutoSize = true;
            this.rdChuaky.Checked = true;
            this.rdChuaky.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdChuaky.Location = new System.Drawing.Point(195, 147);
            this.rdChuaky.Name = "rdChuaky";
            this.rdChuaky.Size = new System.Drawing.Size(85, 23);
            this.rdChuaky.TabIndex = 99;
            this.rdChuaky.TabStop = true;
            this.rdChuaky.Text = "Chưa ký";
            this.rdChuaky.UseVisualStyleBackColor = true;
            this.rdChuaky.CheckedChanged += new System.EventHandler(this.rdChuaky_CheckedChanged);
            // 
            // rdDaky
            // 
            this.rdDaky.AutoSize = true;
            this.rdDaky.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDaky.Location = new System.Drawing.Point(286, 147);
            this.rdDaky.Name = "rdDaky";
            this.rdDaky.Size = new System.Drawing.Size(67, 23);
            this.rdDaky.TabIndex = 98;
            this.rdDaky.Text = "Đã ký";
            this.rdDaky.UseVisualStyleBackColor = true;
            this.rdDaky.CheckedChanged += new System.EventHandler(this.rdDaky_CheckedChanged);
            // 
            // cboLoaiHD
            // 
            this.cboLoaiHD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiHD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiHD.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiHD.FormattingEnabled = true;
            this.cboLoaiHD.Location = new System.Drawing.Point(190, 15);
            this.cboLoaiHD.Name = "cboLoaiHD";
            this.cboLoaiHD.Size = new System.Drawing.Size(493, 27);
            this.cboLoaiHD.TabIndex = 97;
            this.cboLoaiHD.SelectedIndexChanged += new System.EventHandler(this.cboLoaiHD_SelectedIndexChanged);
            // 
            // txtTinhTrangKyHD
            // 
            this.txtTinhTrangKyHD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTinhTrangKyHD.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhTrangKyHD.Location = new System.Drawing.Point(667, 298);
            this.txtTinhTrangKyHD.Name = "txtTinhTrangKyHD";
            this.txtTinhTrangKyHD.Size = new System.Drawing.Size(53, 27);
            this.txtTinhTrangKyHD.TabIndex = 95;
            this.txtTinhTrangKyHD.Visible = false;
            // 
            // txtSoHD
            // 
            this.txtSoHD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoHD.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoHD.Location = new System.Drawing.Point(190, 112);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.Size = new System.Drawing.Size(530, 27);
            this.txtSoHD.TabIndex = 96;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 19);
            this.label6.TabIndex = 94;
            this.label6.Text = "Số hợp đồng";
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDateEnd.EditValue = null;
            this.dtpDateEnd.Location = new System.Drawing.Point(190, 80);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateEnd.Properties.Appearance.Options.UseFont = true;
            this.dtpDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateEnd.Size = new System.Drawing.Size(530, 26);
            this.dtpDateEnd.TabIndex = 93;
            this.dtpDateEnd.EditValueChanged += new System.EventHandler(this.dtpDateEnd_EditValueChanged);
            // 
            // dtpDateSign
            // 
            this.dtpDateSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDateSign.EditValue = null;
            this.dtpDateSign.Location = new System.Drawing.Point(543, 145);
            this.dtpDateSign.Name = "dtpDateSign";
            this.dtpDateSign.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateSign.Properties.Appearance.Options.UseFont = true;
            this.dtpDateSign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateSign.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateSign.Size = new System.Drawing.Size(177, 26);
            this.dtpDateSign.TabIndex = 91;
            // 
            // dtpDateStart
            // 
            this.dtpDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDateStart.EditValue = null;
            this.dtpDateStart.Location = new System.Drawing.Point(190, 48);
            this.dtpDateStart.Name = "dtpDateStart";
            this.dtpDateStart.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateStart.Properties.Appearance.Options.UseFont = true;
            this.dtpDateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateStart.Size = new System.Drawing.Size(530, 26);
            this.dtpDateStart.TabIndex = 92;
            this.dtpDateStart.EditValueChanged += new System.EventHandler(this.dtpDateStart_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 19);
            this.label4.TabIndex = 90;
            this.label4.Text = "Ngày kết thúc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 87;
            this.label2.Text = "Tình trạng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(471, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 88;
            this.label5.Text = "Ngày ký";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 19);
            this.label3.TabIndex = 89;
            this.label3.Text = "Ngày bắt đầu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 19);
            this.label1.TabIndex = 86;
            this.label1.Text = "Loại hợp đồng hiện tại";
            // 
            // ucTaxUserContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddLoaiHD);
            this.Controls.Add(this.rdChuaky);
            this.Controls.Add(this.rdDaky);
            this.Controls.Add(this.cboLoaiHD);
            this.Controls.Add(this.txtTinhTrangKyHD);
            this.Controls.Add(this.txtSoHD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDateEnd);
            this.Controls.Add(this.dtpDateSign);
            this.Controls.Add(this.dtpDateStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "ucTaxUserContract";
            this.Size = new System.Drawing.Size(737, 338);
            this.Load += new System.EventHandler(this.ucTaxUserContract_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateSign.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateSign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateStart.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddLoaiHD;
        public System.Windows.Forms.RadioButton rdChuaky;
        public System.Windows.Forms.RadioButton rdDaky;
        public System.Windows.Forms.ComboBox cboLoaiHD;
        public System.Windows.Forms.TextBox txtTinhTrangKyHD;
        public System.Windows.Forms.TextBox txtSoHD;
        private System.Windows.Forms.Label label6;
        public DevExpress.XtraEditors.DateEdit dtpDateEnd;
        public DevExpress.XtraEditors.DateEdit dtpDateSign;
        public DevExpress.XtraEditors.DateEdit dtpDateStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}
