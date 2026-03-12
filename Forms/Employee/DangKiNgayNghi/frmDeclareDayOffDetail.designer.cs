
namespace BMS
{
    partial class frmDeclareDayOffDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeclareDayOffDetail));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveAndNew = new System.Windows.Forms.ToolStripButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullNameUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotalDayInYear = new System.Windows.Forms.TextBox();
            this.txtTotalDayOnLeave = new System.Windows.Forms.TextBox();
            this.txtTotalDayNoOnLeave = new System.Windows.Forms.TextBox();
            this.txtTotalDayRemain = new System.Windows.Forms.TextBox();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator1,
            this.btnSaveAndNew});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(589, 42);
            this.mnuMenu.TabIndex = 19;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 39);
            this.btnSave.Tag = "frmDayOff_HRUse";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndNew.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAndNew.Image")));
            this.btnSaveAndNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAndNew.Name = "btnSaveAndNew";
            this.btnSaveAndNew.Size = new System.Drawing.Size(145, 39);
            this.btnSaveAndNew.Tag = "frmDayOff_HRUse";
            this.btnSaveAndNew.Text = "Cất && Thêm mới";
            this.btnSaveAndNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveAndNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAndNew.Click += new System.EventHandler(this.btnSaveAndNew_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 19);
            this.label5.TabIndex = 20;
            this.label5.Text = "Họ và tên";
            // 
            // cboEmployee
            // 
            this.cboEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEmployee.EditValue = "";
            this.cboEmployee.Location = new System.Drawing.Point(248, 52);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployee.Properties.Appearance.Options.UseFont = true;
            this.cboEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEmployee.Properties.NullText = "";
            this.cboEmployee.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboEmployee.Size = new System.Drawing.Size(329, 26);
            this.cboEmployee.TabIndex = 21;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.ColumnPanelRowHeight = 35;
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDUser,
            this.colCode,
            this.colFullNameUser});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colIDUser
            // 
            this.colIDUser.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colIDUser.AppearanceCell.Options.UseFont = true;
            this.colIDUser.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colIDUser.AppearanceHeader.Options.UseFont = true;
            this.colIDUser.AppearanceHeader.Options.UseForeColor = true;
            this.colIDUser.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDUser.Caption = "ID";
            this.colIDUser.FieldName = "ID";
            this.colIDUser.Name = "colIDUser";
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.Caption = "Mã nhân viên";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 228;
            // 
            // colFullNameUser
            // 
            this.colFullNameUser.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colFullNameUser.AppearanceCell.Options.UseFont = true;
            this.colFullNameUser.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colFullNameUser.AppearanceHeader.Options.UseFont = true;
            this.colFullNameUser.AppearanceHeader.Options.UseForeColor = true;
            this.colFullNameUser.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullNameUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullNameUser.Caption = "Họ và tên";
            this.colFullNameUser.FieldName = "FullName";
            this.colFullNameUser.Name = "colFullNameUser";
            this.colFullNameUser.Visible = true;
            this.colFullNameUser.VisibleIndex = 1;
            this.colFullNameUser.Width = 475;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "Năm";
            // 
            // txtYear
            // 
            this.txtYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYear.Location = new System.Drawing.Point(248, 84);
            this.txtYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(329, 27);
            this.txtYear.TabIndex = 23;
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 19);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tổng số ngày phép";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "Tổng số ngày phép đã nghỉ";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 19);
            this.label4.TabIndex = 26;
            this.label4.Text = "Tổng số ngày nghỉ không phép";
            this.label4.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 19);
            this.label6.TabIndex = 27;
            this.label6.Text = "Tổng số ngày phép còn lại";
            this.label6.Visible = false;
            // 
            // txtTotalDayInYear
            // 
            this.txtTotalDayInYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDayInYear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDayInYear.Location = new System.Drawing.Point(248, 117);
            this.txtTotalDayInYear.Name = "txtTotalDayInYear";
            this.txtTotalDayInYear.Size = new System.Drawing.Size(329, 27);
            this.txtTotalDayInYear.TabIndex = 28;
            this.txtTotalDayInYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalDayOnLeave
            // 
            this.txtTotalDayOnLeave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDayOnLeave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDayOnLeave.Location = new System.Drawing.Point(248, 150);
            this.txtTotalDayOnLeave.Name = "txtTotalDayOnLeave";
            this.txtTotalDayOnLeave.Size = new System.Drawing.Size(329, 27);
            this.txtTotalDayOnLeave.TabIndex = 29;
            this.txtTotalDayOnLeave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDayOnLeave.Visible = false;
            this.txtTotalDayOnLeave.TextChanged += new System.EventHandler(this.txtTotalDayOnLeave_TextChanged);
            // 
            // txtTotalDayNoOnLeave
            // 
            this.txtTotalDayNoOnLeave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDayNoOnLeave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDayNoOnLeave.Location = new System.Drawing.Point(248, 183);
            this.txtTotalDayNoOnLeave.Name = "txtTotalDayNoOnLeave";
            this.txtTotalDayNoOnLeave.Size = new System.Drawing.Size(329, 27);
            this.txtTotalDayNoOnLeave.TabIndex = 30;
            this.txtTotalDayNoOnLeave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDayNoOnLeave.Visible = false;
            // 
            // txtTotalDayRemain
            // 
            this.txtTotalDayRemain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDayRemain.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDayRemain.Location = new System.Drawing.Point(248, 216);
            this.txtTotalDayRemain.Name = "txtTotalDayRemain";
            this.txtTotalDayRemain.ReadOnly = true;
            this.txtTotalDayRemain.Size = new System.Drawing.Size(329, 27);
            this.txtTotalDayRemain.TabIndex = 31;
            this.txtTotalDayRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalDayRemain.Visible = false;
            // 
            // frmDeclareDayOffDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 151);
            this.Controls.Add(this.txtTotalDayRemain);
            this.Controls.Add(this.txtTotalDayNoOnLeave);
            this.Controls.Add(this.txtTotalDayOnLeave);
            this.Controls.Add(this.txtTotalDayInYear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboEmployee);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mnuMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDeclareDayOffDetail";
            this.Text = "CHI TIẾT NGÀY NGHỈ PHÉP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDeclareDayOffDetail_FormClosing);
            this.Load += new System.EventHandler(this.frmDeclareDayOffDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SearchLookUpEdit cboEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colIDUser;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colFullNameUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotalDayInYear;
        private System.Windows.Forms.TextBox txtTotalDayOnLeave;
        private System.Windows.Forms.TextBox txtTotalDayNoOnLeave;
        private System.Windows.Forms.TextBox txtTotalDayRemain;
        private System.Windows.Forms.ToolStripButton btnSaveAndNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}