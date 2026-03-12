
using System;

namespace BMS
{
    partial class frmCurrencyDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameVie = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameEng = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtPStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtPExpire = new System.Windows.Forms.DateTimePicker();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.txtCode = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSaveCLose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.txtMinunit = new DevExpress.XtraEditors.TextEdit();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtCurrencyRateOfficialQuota = new DevExpress.XtraEditors.TextEdit();
            this.txtCurrencyRateUnofficialQuota = new DevExpress.XtraEditors.TextEdit();
            this.txtDateExpriedOfficialQuota = new System.Windows.Forms.DateTimePicker();
            this.txtDateExpriedUnofficialQuota = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinunit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyRateOfficialQuota.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyRateUnofficialQuota.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã tiền tệ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đơn vị nhỏ nhất";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tỉ giá";
            // 
            // txtNameVie
            // 
            this.txtNameVie.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameVie.Location = new System.Drawing.Point(221, 182);
            this.txtNameVie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameVie.Name = "txtNameVie";
            this.txtNameVie.Size = new System.Drawing.Size(442, 32);
            this.txtNameVie.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tên tiếng Việt";
            // 
            // txtNameEng
            // 
            this.txtNameEng.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameEng.Location = new System.Drawing.Point(221, 220);
            this.txtNameEng.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameEng.Name = "txtNameEng";
            this.txtNameEng.Size = new System.Drawing.Size(442, 32);
            this.txtNameEng.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tên tiếng Anh";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ngày bắt đầu";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 24);
            this.label7.TabIndex = 13;
            this.label7.Text = "Ngày hết hạn";
            // 
            // dtPStartDate
            // 
            this.dtPStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtPStartDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPStartDate.Location = new System.Drawing.Point(221, 258);
            this.dtPStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtPStartDate.Name = "dtPStartDate";
            this.dtPStartDate.Size = new System.Drawing.Size(442, 32);
            this.dtPStartDate.TabIndex = 6;
            // 
            // dtPExpire
            // 
            this.dtPExpire.CustomFormat = "dd/MM/yyyy";
            this.dtPExpire.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPExpire.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPExpire.Location = new System.Drawing.Point(221, 297);
            this.dtPExpire.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtPExpire.Name = "dtPExpire";
            this.dtPExpire.Size = new System.Drawing.Size(442, 32);
            this.dtPExpire.TabIndex = 7;
            this.dtPExpire.Value = new System.DateTime(2024, 4, 18, 0, 0, 0, 0);
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(881, 220);
            this.txtNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(406, 109);
            this.txtNote.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(674, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "Ghi chú";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(220, 71);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(442, 32);
            this.txtCode.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveCLose,
            this.toolStripSeparator2,
            this.btnSaveNew,
            this.toolStripSeparator3});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1302, 60);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // btnSaveCLose
            // 
            this.btnSaveCLose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCLose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSaveCLose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveCLose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCLose.Name = "btnSaveCLose";
            this.btnSaveCLose.Size = new System.Drawing.Size(111, 57);
            this.btnSaveCLose.Tag = "frmCurrency_Add";
            this.btnSaveCLose.Text = "Cất && Đóng";
            this.btnSaveCLose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveCLose.Click += new System.EventHandler(this.btnSaveCLose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSaveNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(149, 57);
            this.btnSaveNew.Tag = "frmCurrency_Add";
            this.btnSaveNew.Text = "Cất && Thêm mới";
            this.btnSaveNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 52);
            this.toolStripSeparator3.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(119, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 24);
            this.label9.TabIndex = 23;
            this.label9.Text = "(*)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(171, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 24);
            this.label10.TabIndex = 24;
            this.label10.Text = "(*)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(153, 224);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 24);
            this.label11.TabIndex = 25;
            this.label11.Text = "(*)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(155, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 24);
            this.label12.TabIndex = 26;
            this.label12.Text = "(*)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(79, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 24);
            this.label13.TabIndex = 27;
            this.label13.Text = "(*)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(147, 261);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 24);
            this.label14.TabIndex = 28;
            this.label14.Text = "(*)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(147, 298);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 24);
            this.label15.TabIndex = 29;
            this.label15.Text = "(*)";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(221, 149);
            this.txtRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Properties.Appearance.Options.UseFont = true;
            this.txtRate.Properties.BeepOnError = false;
            this.txtRate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtRate.Properties.MaskSettings.Set("mask", "n2");
            this.txtRate.Properties.UseMaskAsDisplayFormat = true;
            this.txtRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRate.Size = new System.Drawing.Size(442, 26);
            this.txtRate.TabIndex = 3;
            // 
            // txtMinunit
            // 
            this.txtMinunit.EditValue = "";
            this.txtMinunit.Location = new System.Drawing.Point(221, 110);
            this.txtMinunit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMinunit.Name = "txtMinunit";
            this.txtMinunit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtMinunit.Properties.Appearance.Options.UseFont = true;
            this.txtMinunit.Size = new System.Drawing.Size(442, 30);
            this.txtMinunit.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(674, 73);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(147, 24);
            this.label16.TabIndex = 30;
            this.label16.Text = "TG chính ngạch";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(674, 151);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(134, 24);
            this.label17.TabIndex = 32;
            this.label17.Text = "TG tiểu ngạch";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(674, 186);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(185, 24);
            this.label18.TabIndex = 34;
            this.label18.Text = "Ngày hết hạn TGTN";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(674, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(185, 24);
            this.label19.TabIndex = 36;
            this.label19.Text = "Ngày hết hạn TGCN";
            // 
            // txtCurrencyRateOfficialQuota
            // 
            this.txtCurrencyRateOfficialQuota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrencyRateOfficialQuota.Location = new System.Drawing.Point(881, 71);
            this.txtCurrencyRateOfficialQuota.Margin = new System.Windows.Forms.Padding(4);
            this.txtCurrencyRateOfficialQuota.Name = "txtCurrencyRateOfficialQuota";
            this.txtCurrencyRateOfficialQuota.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrencyRateOfficialQuota.Properties.Appearance.Options.UseFont = true;
            this.txtCurrencyRateOfficialQuota.Properties.BeepOnError = false;
            this.txtCurrencyRateOfficialQuota.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtCurrencyRateOfficialQuota.Properties.MaskSettings.Set("mask", "n2");
            this.txtCurrencyRateOfficialQuota.Properties.UseMaskAsDisplayFormat = true;
            this.txtCurrencyRateOfficialQuota.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCurrencyRateOfficialQuota.Size = new System.Drawing.Size(406, 26);
            this.txtCurrencyRateOfficialQuota.TabIndex = 8;
            // 
            // txtCurrencyRateUnofficialQuota
            // 
            this.txtCurrencyRateUnofficialQuota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrencyRateUnofficialQuota.Location = new System.Drawing.Point(881, 149);
            this.txtCurrencyRateUnofficialQuota.Margin = new System.Windows.Forms.Padding(4);
            this.txtCurrencyRateUnofficialQuota.Name = "txtCurrencyRateUnofficialQuota";
            this.txtCurrencyRateUnofficialQuota.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrencyRateUnofficialQuota.Properties.Appearance.Options.UseFont = true;
            this.txtCurrencyRateUnofficialQuota.Properties.BeepOnError = false;
            this.txtCurrencyRateUnofficialQuota.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtCurrencyRateUnofficialQuota.Properties.MaskSettings.Set("mask", "n2");
            this.txtCurrencyRateUnofficialQuota.Properties.UseMaskAsDisplayFormat = true;
            this.txtCurrencyRateUnofficialQuota.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCurrencyRateUnofficialQuota.Size = new System.Drawing.Size(406, 26);
            this.txtCurrencyRateUnofficialQuota.TabIndex = 10;
            // 
            // txtDateExpriedOfficialQuota
            // 
            this.txtDateExpriedOfficialQuota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDateExpriedOfficialQuota.CustomFormat = "dd/MM/yyyy";
            this.txtDateExpriedOfficialQuota.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateExpriedOfficialQuota.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDateExpriedOfficialQuota.Location = new System.Drawing.Point(881, 110);
            this.txtDateExpriedOfficialQuota.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDateExpriedOfficialQuota.Name = "txtDateExpriedOfficialQuota";
            this.txtDateExpriedOfficialQuota.Size = new System.Drawing.Size(406, 32);
            this.txtDateExpriedOfficialQuota.TabIndex = 9;
            // 
            // txtDateExpriedUnofficialQuota
            // 
            this.txtDateExpriedUnofficialQuota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDateExpriedUnofficialQuota.CustomFormat = "dd/MM/yyyy";
            this.txtDateExpriedUnofficialQuota.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateExpriedUnofficialQuota.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDateExpriedUnofficialQuota.Location = new System.Drawing.Point(881, 182);
            this.txtDateExpriedUnofficialQuota.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDateExpriedUnofficialQuota.Name = "txtDateExpriedUnofficialQuota";
            this.txtDateExpriedUnofficialQuota.Size = new System.Drawing.Size(406, 32);
            this.txtDateExpriedUnofficialQuota.TabIndex = 11;
            // 
            // frmCurrencyDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 358);
            this.Controls.Add(this.txtDateExpriedUnofficialQuota);
            this.Controls.Add(this.txtDateExpriedOfficialQuota);
            this.Controls.Add(this.txtCurrencyRateUnofficialQuota);
            this.Controls.Add(this.txtCurrencyRateOfficialQuota);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtMinunit);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtPExpire);
            this.Controls.Add(this.dtPStartDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNameEng);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNameVie);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmCurrencyDetails";
            this.Text = "CHI TIẾT TIỀN TỆ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCurrencyDetails_FormClosed);
            this.Load += new System.EventHandler(this.frmCurrencyDetails_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCurrencyDetails_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinunit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyRateOfficialQuota.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyRateUnofficialQuota.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameVie;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameEng;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtPStartDate;
        private System.Windows.Forms.DateTimePicker dtPExpire;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtMinunit;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSaveCLose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSaveNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private DevExpress.XtraEditors.TextEdit txtCurrencyRateOfficialQuota;
        private DevExpress.XtraEditors.TextEdit txtCurrencyRateUnofficialQuota;
        private System.Windows.Forms.DateTimePicker txtDateExpriedOfficialQuota;
        private System.Windows.Forms.DateTimePicker txtDateExpriedUnofficialQuota;
    }
}