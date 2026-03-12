
namespace BMS
{
    partial class frmRepairMantain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepairMantain));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtQuyCach = new System.Windows.Forms.TextBox();
            this.dtpDateBuy = new System.Windows.Forms.DateTimePicker();
            this.txtStatus = new DevExpress.XtraEditors.TextEdit();
            this.txtSeri = new DevExpress.XtraEditors.TextEdit();
            this.txtMaTS = new DevExpress.XtraEditors.TextEdit();
            this.txtLoaiTS = new DevExpress.XtraEditors.TextEdit();
            this.txtTenTS = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDonViSua = new DevExpress.XtraEditors.TextEdit();
            this.txtChiPhi = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.dtpRepairDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaNCC = new DevExpress.XtraEditors.TextEdit();
            this.mnuMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaTS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoaiTS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenTS.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonViSua.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChiPhi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaNCC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(819, 50);
            this.mnuMenu.TabIndex = 237;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 45);
            this.btnSave.Text = "Cất";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.7396F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.2604F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(819, 381);
            this.tableLayoutPanel1.TabIndex = 238;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtQuyCach);
            this.groupBox1.Controls.Add(this.dtpDateBuy);
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.txtSeri);
            this.groupBox1.Controls.Add(this.txtMaNCC);
            this.groupBox1.Controls.Add(this.txtMaTS);
            this.groupBox1.Controls.Add(this.txtLoaiTS);
            this.groupBox1.Controls.Add(this.txtTenTS);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 375);
            this.groupBox1.TabIndex = 235;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tài sản";
            // 
            // txtQuyCach
            // 
            this.txtQuyCach.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuyCach.Location = new System.Drawing.Point(162, 308);
            this.txtQuyCach.Multiline = true;
            this.txtQuyCach.Name = "txtQuyCach";
            this.txtQuyCach.ReadOnly = true;
            this.txtQuyCach.Size = new System.Drawing.Size(290, 47);
            this.txtQuyCach.TabIndex = 11;
            // 
            // dtpDateBuy
            // 
            this.dtpDateBuy.Enabled = false;
            this.dtpDateBuy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateBuy.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateBuy.Location = new System.Drawing.Point(162, 227);
            this.dtpDateBuy.Name = "dtpDateBuy";
            this.dtpDateBuy.Size = new System.Drawing.Size(290, 27);
            this.dtpDateBuy.TabIndex = 9;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(162, 268);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Properties.Appearance.Options.UseFont = true;
            this.txtStatus.Properties.BeepOnError = false;
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Properties.UseMaskAsDisplayFormat = true;
            this.txtStatus.Size = new System.Drawing.Size(290, 26);
            this.txtStatus.TabIndex = 10;
            // 
            // txtSeri
            // 
            this.txtSeri.Location = new System.Drawing.Point(162, 187);
            this.txtSeri.Margin = new System.Windows.Forms.Padding(2);
            this.txtSeri.Name = "txtSeri";
            this.txtSeri.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeri.Properties.Appearance.Options.UseFont = true;
            this.txtSeri.Properties.BeepOnError = false;
            this.txtSeri.Properties.ReadOnly = true;
            this.txtSeri.Properties.UseMaskAsDisplayFormat = true;
            this.txtSeri.Size = new System.Drawing.Size(290, 26);
            this.txtSeri.TabIndex = 8;
            // 
            // txtMaTS
            // 
            this.txtMaTS.Location = new System.Drawing.Point(162, 67);
            this.txtMaTS.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaTS.Name = "txtMaTS";
            this.txtMaTS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaTS.Properties.Appearance.Options.UseFont = true;
            this.txtMaTS.Properties.BeepOnError = false;
            this.txtMaTS.Properties.ReadOnly = true;
            this.txtMaTS.Properties.UseMaskAsDisplayFormat = true;
            this.txtMaTS.Size = new System.Drawing.Size(290, 26);
            this.txtMaTS.TabIndex = 5;
            // 
            // txtLoaiTS
            // 
            this.txtLoaiTS.Location = new System.Drawing.Point(162, 147);
            this.txtLoaiTS.Margin = new System.Windows.Forms.Padding(2);
            this.txtLoaiTS.Name = "txtLoaiTS";
            this.txtLoaiTS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiTS.Properties.Appearance.Options.UseFont = true;
            this.txtLoaiTS.Properties.BeepOnError = false;
            this.txtLoaiTS.Properties.ReadOnly = true;
            this.txtLoaiTS.Properties.UseMaskAsDisplayFormat = true;
            this.txtLoaiTS.Size = new System.Drawing.Size(290, 26);
            this.txtLoaiTS.TabIndex = 7;
            // 
            // txtTenTS
            // 
            this.txtTenTS.Location = new System.Drawing.Point(162, 107);
            this.txtTenTS.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenTS.Name = "txtTenTS";
            this.txtTenTS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenTS.Properties.Appearance.Options.UseFont = true;
            this.txtTenTS.Properties.BeepOnError = false;
            this.txtTenTS.Properties.ReadOnly = true;
            this.txtTenTS.Properties.UseMaskAsDisplayFormat = true;
            this.txtTenTS.Size = new System.Drawing.Size(290, 26);
            this.txtTenTS.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 271);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 19);
            this.label8.TabIndex = 232;
            this.label8.Text = "Tình trạng";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 110);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 19);
            this.label7.TabIndex = 231;
            this.label7.Text = "Tên tài sản";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 19);
            this.label6.TabIndex = 230;
            this.label6.Text = "Mã tài sản";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 150);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 19);
            this.label5.TabIndex = 229;
            this.label5.Text = "Loại tài sản";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 311);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 19);
            this.label4.TabIndex = 228;
            this.label4.Text = "Quy cách tài sản";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 233);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 227;
            this.label3.Text = "Ngày mua";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 190);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 19);
            this.label2.TabIndex = 226;
            this.label2.Text = "Số Seri";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtDonViSua);
            this.groupBox3.Controls.Add(this.txtChiPhi);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtLyDo);
            this.groupBox3.Controls.Add(this.dtpRepairDate);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(484, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 375);
            this.groupBox3.TabIndex = 236;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin sửa chữa, bảo dưỡng";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(227, 236);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(21, 13);
            this.label17.TabIndex = 250;
            this.label17.Text = "(*)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(264, 96);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 249;
            this.label12.Text = "(*)";
            // 
            // txtDonViSua
            // 
            this.txtDonViSua.Location = new System.Drawing.Point(23, 126);
            this.txtDonViSua.Margin = new System.Windows.Forms.Padding(2);
            this.txtDonViSua.Name = "txtDonViSua";
            this.txtDonViSua.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDonViSua.Properties.Appearance.Options.UseFont = true;
            this.txtDonViSua.Properties.BeepOnError = false;
            this.txtDonViSua.Properties.UseMaskAsDisplayFormat = true;
            this.txtDonViSua.Size = new System.Drawing.Size(290, 26);
            this.txtDonViSua.TabIndex = 2;
            // 
            // txtChiPhi
            // 
            this.txtChiPhi.Location = new System.Drawing.Point(23, 197);
            this.txtChiPhi.Margin = new System.Windows.Forms.Padding(2);
            this.txtChiPhi.Name = "txtChiPhi";
            this.txtChiPhi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChiPhi.Properties.Appearance.Options.UseFont = true;
            this.txtChiPhi.Properties.BeepOnError = false;
            this.txtChiPhi.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtChiPhi.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtChiPhi.Properties.MaskSettings.Set("mask", "N0");
            this.txtChiPhi.Properties.UseMaskAsDisplayFormat = true;
            this.txtChiPhi.Size = new System.Drawing.Size(290, 26);
            this.txtChiPhi.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(19, 236);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(204, 19);
            this.label11.TabIndex = 244;
            this.label11.Text = "Lý do sửa chữa, bảo dưỡng";
            // 
            // txtLyDo
            // 
            this.txtLyDo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyDo.Location = new System.Drawing.Point(23, 268);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(290, 47);
            this.txtLyDo.TabIndex = 4;
            // 
            // dtpRepairDate
            // 
            this.dtpRepairDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRepairDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRepairDate.Location = new System.Drawing.Point(23, 58);
            this.dtpRepairDate.Name = "dtpRepairDate";
            this.dtpRepairDate.Size = new System.Drawing.Size(290, 27);
            this.dtpRepairDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 19);
            this.label1.TabIndex = 230;
            this.label1.Text = "Ngày sửa chữa, bảo dưỡng";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 96);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(241, 19);
            this.label9.TabIndex = 234;
            this.label9.Text = "Tên đơn vị sửa chữa, bảo dưỡng";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 167);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 19);
            this.label10.TabIndex = 235;
            this.label10.Text = "Chi phí dự kiến";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(22, 30);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 19);
            this.label13.TabIndex = 230;
            this.label13.Text = "Mã NCC";
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.Location = new System.Drawing.Point(162, 27);
            this.txtMaNCC.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaNCC.Properties.Appearance.Options.UseFont = true;
            this.txtMaNCC.Properties.BeepOnError = false;
            this.txtMaNCC.Properties.ReadOnly = true;
            this.txtMaNCC.Properties.UseMaskAsDisplayFormat = true;
            this.txtMaNCC.Size = new System.Drawing.Size(290, 26);
            this.txtMaNCC.TabIndex = 5;
            // 
            // frmRepairMantain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 431);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mnuMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmRepairMantain";
            this.Text = "BÁO CÁO SỬA CHỮA, BẢO DƯỠNG";
            this.Load += new System.EventHandler(this.frmRepairMantain_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaTS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLoaiTS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenTS.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonViSua.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChiPhi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaNCC.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpDateBuy;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private DevExpress.XtraEditors.TextEdit txtSeri;
        private DevExpress.XtraEditors.TextEdit txtMaTS;
        private DevExpress.XtraEditors.TextEdit txtLoaiTS;
        private DevExpress.XtraEditors.TextEdit txtTenTS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpRepairDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtQuyCach;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLyDo;
        private DevExpress.XtraEditors.TextEdit txtChiPhi;
        private DevExpress.XtraEditors.TextEdit txtDonViSua;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.TextEdit txtMaNCC;
        private System.Windows.Forms.Label label13;
    }
}