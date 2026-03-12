namespace BMS
{
    partial class frmKPIPositionTypeDetail
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
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveNew = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericQuaterValue = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericYearValue = new System.Windows.Forms.NumericUpDown();
            this.txtTypeCode = new System.Windows.Forms.MaskedTextBox();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSTT = new System.Windows.Forms.NumericUpDown();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtTypeName = new System.Windows.Forms.MaskedTextBox();
            this.lbProjectType = new DevExpress.XtraEditors.LabelControl();
            this.cbxProjectType = new System.Windows.Forms.ComboBox();
            this.mnuMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuaterValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericYearValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator1,
            this.btnSaveNew});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(653, 55);
            this.mnuMenu.TabIndex = 4;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 52);
            this.btnSave.Tag = "frmVehicleManagement_Add";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSaveNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(112, 52);
            this.btnSaveNew.Tag = "frmVehicleManagement_Add";
            this.btnSaveNew.Text = "Cất && Thêm mới";
            this.btnSaveNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxProjectType);
            this.groupBox1.Controls.Add(this.lbProjectType);
            this.groupBox1.Controls.Add(this.numericQuaterValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericYearValue);
            this.groupBox1.Controls.Add(this.txtTypeCode);
            this.groupBox1.Controls.Add(this.lblDriverName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSTT);
            this.groupBox1.Controls.Add(this.lblPhoneNumber);
            this.groupBox1.Controls.Add(this.txtTypeName);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 196);
            this.groupBox1.TabIndex = 215;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin loại sở hữu";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(367, 140);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(38, 19);
            this.label3.TabIndex = 217;
            this.label3.Tag = "3";
            this.label3.Text = "Quý";
            // 
            // numericQuaterValue
            // 
            this.numericQuaterValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericQuaterValue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericQuaterValue.Location = new System.Drawing.Point(411, 136);
            this.numericQuaterValue.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericQuaterValue.Name = "numericQuaterValue";
            this.numericQuaterValue.Size = new System.Drawing.Size(236, 27);
            this.numericQuaterValue.TabIndex = 218;
            this.numericQuaterValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericQuaterValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(44, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(42, 19);
            this.label2.TabIndex = 215;
            this.label2.Tag = "3";
            this.label2.Text = "Năm";
            // 
            // numericYearValue
            // 
            this.numericYearValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericYearValue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericYearValue.Location = new System.Drawing.Point(98, 136);
            this.numericYearValue.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericYearValue.Name = "numericYearValue";
            this.numericYearValue.Size = new System.Drawing.Size(258, 27);
            this.numericYearValue.TabIndex = 216;
            this.numericYearValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTypeCode
            // 
            this.txtTypeCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTypeCode.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTypeCode.Location = new System.Drawing.Point(98, 26);
            this.txtTypeCode.Name = "txtTypeCode";
            this.txtTypeCode.Size = new System.Drawing.Size(549, 27);
            this.txtTypeCode.TabIndex = 213;
            // 
            // lblDriverName
            // 
            this.lblDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblDriverName.Location = new System.Drawing.Point(27, 29);
            this.lblDriverName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDriverName.Size = new System.Drawing.Size(59, 19);
            this.lblDriverName.TabIndex = 10;
            this.lblDriverName.Tag = "3";
            this.lblDriverName.Text = "Mã loại";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(493, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 11;
            this.label1.Tag = "3";
            this.label1.Text = "STT";
            // 
            // txtSTT
            // 
            this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSTT.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTT.Location = new System.Drawing.Point(537, 60);
            this.txtSTT.Name = "txtSTT";
            this.txtSTT.Size = new System.Drawing.Size(110, 27);
            this.txtSTT.TabIndex = 209;
            this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblPhoneNumber.Location = new System.Drawing.Point(20, 64);
            this.lblPhoneNumber.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblPhoneNumber.Size = new System.Drawing.Size(66, 19);
            this.lblPhoneNumber.TabIndex = 11;
            this.lblPhoneNumber.Tag = "3";
            this.lblPhoneNumber.Text = "Tên loại";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtTypeName.Location = new System.Drawing.Point(98, 60);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(375, 27);
            this.txtTypeName.TabIndex = 15;
            // 
            // lbProjectType
            // 
            this.lbProjectType.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProjectType.Appearance.Options.UseFont = true;
            this.lbProjectType.Location = new System.Drawing.Point(6, 102);
            this.lbProjectType.Name = "lbProjectType";
            this.lbProjectType.Size = new System.Drawing.Size(74, 20);
            this.lbProjectType.TabIndex = 219;
            this.lbProjectType.Text = "Loại dự án";
            // 
            // cbxProjectType
            // 
            this.cbxProjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxProjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProjectType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProjectType.FormattingEnabled = true;
            this.cbxProjectType.Location = new System.Drawing.Point(98, 99);
            this.cbxProjectType.Name = "cbxProjectType";
            this.cbxProjectType.Size = new System.Drawing.Size(549, 28);
            this.cbxProjectType.TabIndex = 220;
            // 
            // frmKPIPositionTypeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 266);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmKPIPositionTypeDetail";
            this.Load += new System.EventHandler(this.frmKPIPositionTypeDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuaterValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericYearValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveNew;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtTypeCode;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtSTT;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.MaskedTextBox txtTypeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericYearValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericQuaterValue;
        private DevExpress.XtraEditors.LabelControl lbProjectType;
        private System.Windows.Forms.ComboBox cbxProjectType;
    }
}
