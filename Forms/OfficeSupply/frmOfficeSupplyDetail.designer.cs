
namespace BMS
{
    partial class frmOfficeSupplyDetail
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
            this.txtNameNCC = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblNameNCC = new System.Windows.Forms.Label();
            this.lblNameRTC = new System.Windows.Forms.Label();
            this.lblCodeNCC = new System.Windows.Forms.Label();
            this.txtNameRTC = new System.Windows.Forms.TextBox();
            this.txtCodeNCC = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblRequestLimit = new System.Windows.Forms.Label();
            this.txtRequestLimit = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.cboUnit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblAsterisk1 = new System.Windows.Forms.Label();
            this.lblAsterisk2 = new System.Windows.Forms.Label();
            this.lblAsterisk3 = new System.Windows.Forms.Label();
            this.lblAsterisk5 = new System.Windows.Forms.Label();
            this.lblAsterisk4 = new System.Windows.Forms.Label();
            this.lblAsterisk6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodeRTC = new System.Windows.Forms.TextBox();
            this.lblAsterisk7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNameNCC
            // 
            this.txtNameNCC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameNCC.Location = new System.Drawing.Point(121, 136);
            this.txtNameNCC.Name = "txtNameNCC";
            this.txtNameNCC.Size = new System.Drawing.Size(444, 23);
            this.txtNameNCC.TabIndex = 3;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit.Location = new System.Drawing.Point(27, 180);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(69, 16);
            this.lblUnit.TabIndex = 18;
            this.lblUnit.Text = "Đơn vị tính";
            // 
            // lblNameNCC
            // 
            this.lblNameNCC.AutoSize = true;
            this.lblNameNCC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameNCC.Location = new System.Drawing.Point(27, 138);
            this.lblNameNCC.Name = "lblNameNCC";
            this.lblNameNCC.Size = new System.Drawing.Size(68, 16);
            this.lblNameNCC.TabIndex = 19;
            this.lblNameNCC.Text = "Tên (NCC)";
            // 
            // lblNameRTC
            // 
            this.lblNameRTC.AutoSize = true;
            this.lblNameRTC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameRTC.Location = new System.Drawing.Point(28, 97);
            this.lblNameRTC.Name = "lblNameRTC";
            this.lblNameRTC.Size = new System.Drawing.Size(68, 16);
            this.lblNameRTC.TabIndex = 20;
            this.lblNameRTC.Text = "Tên (RTC)";
            // 
            // lblCodeNCC
            // 
            this.lblCodeNCC.AutoSize = true;
            this.lblCodeNCC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodeNCC.Location = new System.Drawing.Point(306, 62);
            this.lblCodeNCC.Name = "lblCodeNCC";
            this.lblCodeNCC.Size = new System.Drawing.Size(53, 16);
            this.lblCodeNCC.TabIndex = 21;
            this.lblCodeNCC.Text = "Mã NCC";
            // 
            // txtNameRTC
            // 
            this.txtNameRTC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameRTC.Location = new System.Drawing.Point(121, 94);
            this.txtNameRTC.Name = "txtNameRTC";
            this.txtNameRTC.Size = new System.Drawing.Size(444, 23);
            this.txtNameRTC.TabIndex = 2;
            // 
            // txtCodeNCC
            // 
            this.txtCodeNCC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeNCC.Location = new System.Drawing.Point(382, 58);
            this.txtCodeNCC.Name = "txtCodeNCC";
            this.txtCodeNCC.Size = new System.Drawing.Size(182, 23);
            this.txtCodeNCC.TabIndex = 1;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(332, 180);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(26, 16);
            this.lblPrice.TabIndex = 31;
            this.lblPrice.Text = "Giá";
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(382, 177);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(182, 23);
            this.txtPrice.TabIndex = 5;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
            // 
            // lblRequestLimit
            // 
            this.lblRequestLimit.AutoSize = true;
            this.lblRequestLimit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestLimit.Location = new System.Drawing.Point(27, 223);
            this.lblRequestLimit.Name = "lblRequestLimit";
            this.lblRequestLimit.Size = new System.Drawing.Size(63, 16);
            this.lblRequestLimit.TabIndex = 33;
            this.lblRequestLimit.Text = "Định mức";
            // 
            // txtRequestLimit
            // 
            this.txtRequestLimit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestLimit.Location = new System.Drawing.Point(121, 221);
            this.txtRequestLimit.Name = "txtRequestLimit";
            this.txtRequestLimit.Size = new System.Drawing.Size(182, 23);
            this.txtRequestLimit.TabIndex = 6;
            this.txtRequestLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberBox_KeyPress);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(327, 223);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 16);
            this.lblType.TabIndex = 35;
            this.lblType.Text = "Loại";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 52);
            this.btnSave.Tag = "frmOfficeSupply_New";
            this.btnSave.Text = "Cất";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(590, 55);
            this.mnuMenu.TabIndex = 27;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(382, 222);
            this.cboType.Margin = new System.Windows.Forms.Padding(2);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(182, 23);
            this.cboType.TabIndex = 7;
            // 
            // cboUnit
            // 
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new System.Drawing.Point(121, 176);
            this.cboUnit.Margin = new System.Windows.Forms.Padding(6);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cboUnit.Properties.AutoHeight = false;
            this.cboUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUnit.Properties.NullText = "";
            this.cboUnit.Properties.PopupFormMinSize = new System.Drawing.Size(300, 244);
            this.cboUnit.Properties.PopupFormSize = new System.Drawing.Size(300, 244);
            this.cboUnit.Properties.PopupView = this.searchLookUpEdit1View;
            this.cboUnit.Size = new System.Drawing.Size(182, 22);
            this.cboUnit.TabIndex = 4;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colName});
            this.searchLookUpEdit1View.DetailHeight = 237;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 29;
            this.colID.Name = "colID";
            this.colID.Width = 136;
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Đơn vị";
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 29;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 136;
            // 
            // lblAsterisk1
            // 
            this.lblAsterisk1.AutoSize = true;
            this.lblAsterisk1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk1.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk1.Location = new System.Drawing.Point(352, 61);
            this.lblAsterisk1.Name = "lblAsterisk1";
            this.lblAsterisk1.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk1.TabIndex = 37;
            this.lblAsterisk1.Text = "(*)";
            // 
            // lblAsterisk2
            // 
            this.lblAsterisk2.AutoSize = true;
            this.lblAsterisk2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk2.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk2.Location = new System.Drawing.Point(90, 138);
            this.lblAsterisk2.Name = "lblAsterisk2";
            this.lblAsterisk2.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk2.TabIndex = 38;
            this.lblAsterisk2.Text = "(*)";
            // 
            // lblAsterisk3
            // 
            this.lblAsterisk3.AutoSize = true;
            this.lblAsterisk3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk3.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk3.Location = new System.Drawing.Point(90, 180);
            this.lblAsterisk3.Name = "lblAsterisk3";
            this.lblAsterisk3.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk3.TabIndex = 39;
            this.lblAsterisk3.Text = "(*)";
            // 
            // lblAsterisk5
            // 
            this.lblAsterisk5.AutoSize = true;
            this.lblAsterisk5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk5.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk5.Location = new System.Drawing.Point(352, 180);
            this.lblAsterisk5.Name = "lblAsterisk5";
            this.lblAsterisk5.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk5.TabIndex = 40;
            this.lblAsterisk5.Text = "(*)";
            // 
            // lblAsterisk4
            // 
            this.lblAsterisk4.AutoSize = true;
            this.lblAsterisk4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk4.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk4.Location = new System.Drawing.Point(83, 223);
            this.lblAsterisk4.Name = "lblAsterisk4";
            this.lblAsterisk4.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk4.TabIndex = 41;
            this.lblAsterisk4.Text = "(*)";
            // 
            // lblAsterisk6
            // 
            this.lblAsterisk6.AutoSize = true;
            this.lblAsterisk6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk6.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk6.Location = new System.Drawing.Point(352, 223);
            this.lblAsterisk6.Name = "lblAsterisk6";
            this.lblAsterisk6.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk6.TabIndex = 42;
            this.lblAsterisk6.Text = "(*)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 43;
            this.label1.Text = "Mã RTC";
            // 
            // txtCodeRTC
            // 
            this.txtCodeRTC.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtCodeRTC.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeRTC.Location = new System.Drawing.Point(121, 59);
            this.txtCodeRTC.Name = "txtCodeRTC";
            this.txtCodeRTC.ReadOnly = true;
            this.txtCodeRTC.Size = new System.Drawing.Size(162, 23);
            this.txtCodeRTC.TabIndex = 44;
            this.txtCodeRTC.TabStop = false;
            // 
            // lblAsterisk7
            // 
            this.lblAsterisk7.AutoSize = true;
            this.lblAsterisk7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsterisk7.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk7.Location = new System.Drawing.Point(76, 62);
            this.lblAsterisk7.Name = "lblAsterisk7";
            this.lblAsterisk7.Size = new System.Drawing.Size(26, 16);
            this.lblAsterisk7.TabIndex = 46;
            this.lblAsterisk7.Text = "(*)";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(282, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 22);
            this.label2.TabIndex = 47;
            this.label2.Text = "⟳";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.btnReloadCode_Click);
            // 
            // frmOfficeSupplyDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 305);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAsterisk7);
            this.Controls.Add(this.txtCodeRTC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAsterisk6);
            this.Controls.Add(this.lblAsterisk4);
            this.Controls.Add(this.lblAsterisk5);
            this.Controls.Add(this.lblAsterisk3);
            this.Controls.Add(this.lblAsterisk2);
            this.Controls.Add(this.lblAsterisk1);
            this.Controls.Add(this.cboUnit);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtRequestLimit);
            this.Controls.Add(this.lblRequestLimit);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtCodeNCC);
            this.Controls.Add(this.txtNameRTC);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.txtNameNCC);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblNameNCC);
            this.Controls.Add(this.lblNameRTC);
            this.Controls.Add(this.lblCodeNCC);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmOfficeSupplyDetail";
            this.Text = "CHI TIẾT VĂN PHÒNG PHẨM";
            this.Load += new System.EventHandler(this.frmOfficeSupplyDetail_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNameNCC;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblNameNCC;
        private System.Windows.Forms.Label lblNameRTC;
        private System.Windows.Forms.Label lblCodeNCC;
        private System.Windows.Forms.TextBox txtNameRTC;
        private System.Windows.Forms.TextBox txtCodeNCC;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblRequestLimit;
        private System.Windows.Forms.TextBox txtRequestLimit;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ComboBox cboType;
        private DevExpress.XtraEditors.SearchLookUpEdit cboUnit;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private System.Windows.Forms.Label lblAsterisk1;
        private System.Windows.Forms.Label lblAsterisk2;
        private System.Windows.Forms.Label lblAsterisk3;
        private System.Windows.Forms.Label lblAsterisk5;
        private System.Windows.Forms.Label lblAsterisk4;
        private System.Windows.Forms.Label lblAsterisk6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodeRTC;
        private System.Windows.Forms.Label lblAsterisk7;
        private System.Windows.Forms.Label label2;
    }
}