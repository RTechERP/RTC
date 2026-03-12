
namespace BMS
{
    partial class frmReturnAdmin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReturnAdmin));
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.mnuMenu = new System.Windows.Forms.ToolStrip();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.txtQrCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grdData = new DevExpress.XtraGrid.GridControl();
			this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProductRTCID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colQrcode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.colProductCodeRTC = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colVitri = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colSoluong = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colHistoryProductRTCID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.mnuMenu.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			this.SuspendLayout();
			// 
			// mnuMenu
			// 
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
			this.mnuMenu.Size = new System.Drawing.Size(1204, 54);
			this.mnuMenu.TabIndex = 215;
			this.mnuMenu.Text = "toolStrip2";
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(124, 51);
			this.btnSave.Text = "Cất && Đóng";
			this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 292F));
			this.tableLayoutPanel1.Controls.Add(this.dtpReturnDate, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtQrCode, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 54);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1204, 40);
			this.tableLayoutPanel1.TabIndex = 216;
			// 
			// dtpReturnDate
			// 
			this.dtpReturnDate.CustomFormat = "dd/MM/yyyy";
			this.dtpReturnDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtpReturnDate.Enabled = false;
			this.dtpReturnDate.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpReturnDate.Location = new System.Drawing.Point(915, 3);
			this.dtpReturnDate.Name = "dtpReturnDate";
			this.dtpReturnDate.Size = new System.Drawing.Size(286, 33);
			this.dtpReturnDate.TabIndex = 213;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(784, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125, 40);
			this.label2.TabIndex = 2;
			this.label2.Text = "Ngày trả";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtQrCode
			// 
			this.txtQrCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtQrCode.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQrCode.Location = new System.Drawing.Point(188, 3);
			this.txtQrCode.Name = "txtQrCode";
			this.txtQrCode.Size = new System.Drawing.Size(590, 33);
			this.txtQrCode.TabIndex = 0;
			this.txtQrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtQrCode.TextChanged += new System.EventHandler(this.txtQrCode_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(179, 40);
			this.label1.TabIndex = 1;
			this.label1.Text = "QRCode";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grdData
			// 
			this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdData.Location = new System.Drawing.Point(0, 94);
			this.grdData.MainView = this.grvData;
			this.grdData.Name = "grdData";
			this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete,
            this.repositoryItemMemoEdit1});
			this.grdData.Size = new System.Drawing.Size(1204, 581);
			this.grdData.TabIndex = 228;
			this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
			// 
			// grvData
			// 
			this.grvData.ColumnPanelRowHeight = 45;
			this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colProductRTCID,
            this.colQrcode,
            this.colProductCode,
            this.colProductName,
            this.colProductCodeRTC,
            this.colVitri,
            this.colNote,
            this.colSoluong,
            this.colHistoryProductRTCID});
			this.grvData.GridControl = this.grdData;
			this.grvData.Name = "grvData";
			this.grvData.OptionsBehavior.ReadOnly = true;
			this.grvData.OptionsFind.FindFilterColumns = "";
			this.grvData.OptionsView.ShowGroupPanel = false;
			this.grvData.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grvData_RowCellStyle);
			// 
			// colId
			// 
			this.colId.Caption = "ID";
			this.colId.FieldName = "ID";
			this.colId.Name = "colId";
			// 
			// colProductRTCID
			// 
			this.colProductRTCID.Caption = "ProductRTCID";
			this.colProductRTCID.FieldName = "ProductRTCID";
			this.colProductRTCID.Name = "colProductRTCID";
			// 
			// colQrcode
			// 
			this.colQrcode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colQrcode.AppearanceCell.Options.UseFont = true;
			this.colQrcode.AppearanceCell.Options.UseTextOptions = true;
			this.colQrcode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQrcode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colQrcode.AppearanceHeader.Options.UseFont = true;
			this.colQrcode.AppearanceHeader.Options.UseForeColor = true;
			this.colQrcode.AppearanceHeader.Options.UseTextOptions = true;
			this.colQrcode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQrcode.Caption = "Mã QRcode";
			this.colQrcode.FieldName = "ProductQRCode";
			this.colQrcode.Name = "colQrcode";
			this.colQrcode.OptionsFilter.AllowAutoFilter = false;
			this.colQrcode.OptionsFilter.AllowFilter = false;
			this.colQrcode.Visible = true;
			this.colQrcode.VisibleIndex = 1;
			this.colQrcode.Width = 182;
			// 
			// colProductCode
			// 
			this.colProductCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colProductCode.AppearanceCell.Options.UseFont = true;
			this.colProductCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colProductCode.AppearanceHeader.Options.UseFont = true;
			this.colProductCode.AppearanceHeader.Options.UseForeColor = true;
			this.colProductCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colProductCode.Caption = "Mã sản phẩm";
			this.colProductCode.FieldName = "ProductCode";
			this.colProductCode.Name = "colProductCode";
			this.colProductCode.OptionsFilter.AllowAutoFilter = false;
			this.colProductCode.OptionsFilter.AllowFilter = false;
			this.colProductCode.Visible = true;
			this.colProductCode.VisibleIndex = 3;
			this.colProductCode.Width = 222;
			// 
			// colProductName
			// 
			this.colProductName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colProductName.AppearanceCell.Options.UseFont = true;
			this.colProductName.AppearanceCell.Options.UseTextOptions = true;
			this.colProductName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colProductName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colProductName.AppearanceHeader.Options.UseFont = true;
			this.colProductName.AppearanceHeader.Options.UseForeColor = true;
			this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
			this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colProductName.Caption = "Tên sản phẩm";
			this.colProductName.ColumnEdit = this.repositoryItemMemoEdit1;
			this.colProductName.FieldName = "ProductName";
			this.colProductName.Name = "colProductName";
			this.colProductName.OptionsFilter.AllowAutoFilter = false;
			this.colProductName.OptionsFilter.AllowFilter = false;
			this.colProductName.Visible = true;
			this.colProductName.VisibleIndex = 4;
			this.colProductName.Width = 323;
			// 
			// repositoryItemMemoEdit1
			// 
			this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
			// 
			// colProductCodeRTC
			// 
			this.colProductCodeRTC.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colProductCodeRTC.AppearanceCell.Options.UseFont = true;
			this.colProductCodeRTC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colProductCodeRTC.AppearanceHeader.Options.UseFont = true;
			this.colProductCodeRTC.AppearanceHeader.Options.UseForeColor = true;
			this.colProductCodeRTC.AppearanceHeader.Options.UseTextOptions = true;
			this.colProductCodeRTC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colProductCodeRTC.Caption = "Mã nội bộ";
			this.colProductCodeRTC.FieldName = "ProductCodeRTC";
			this.colProductCodeRTC.Name = "colProductCodeRTC";
			this.colProductCodeRTC.OptionsFilter.AllowAutoFilter = false;
			this.colProductCodeRTC.OptionsFilter.AllowFilter = false;
			this.colProductCodeRTC.Visible = true;
			this.colProductCodeRTC.VisibleIndex = 2;
			this.colProductCodeRTC.Width = 169;
			// 
			// colVitri
			// 
			this.colVitri.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colVitri.AppearanceCell.Options.UseFont = true;
			this.colVitri.AppearanceCell.Options.UseTextOptions = true;
			this.colVitri.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colVitri.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colVitri.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colVitri.AppearanceHeader.Options.UseFont = true;
			this.colVitri.AppearanceHeader.Options.UseForeColor = true;
			this.colVitri.AppearanceHeader.Options.UseTextOptions = true;
			this.colVitri.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colVitri.Caption = "Vị trí";
			this.colVitri.ColumnEdit = this.repositoryItemMemoEdit1;
			this.colVitri.FieldName = "AddressBox";
			this.colVitri.Name = "colVitri";
			this.colVitri.OptionsFilter.AllowAutoFilter = false;
			this.colVitri.OptionsFilter.AllowFilter = false;
			this.colVitri.Visible = true;
			this.colVitri.VisibleIndex = 5;
			this.colVitri.Width = 208;
			// 
			// colNote
			// 
			this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colNote.AppearanceCell.Options.UseFont = true;
			this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colNote.AppearanceHeader.Options.UseFont = true;
			this.colNote.AppearanceHeader.Options.UseForeColor = true;
			this.colNote.AppearanceHeader.Options.UseTextOptions = true;
			this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colNote.Caption = "Ghi chú ";
			this.colNote.FieldName = "Note";
			this.colNote.Name = "colNote";
			this.colNote.OptionsFilter.AllowAutoFilter = false;
			this.colNote.OptionsFilter.AllowFilter = false;
			this.colNote.Width = 314;
			// 
			// colSoluong
			// 
			this.colSoluong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colSoluong.AppearanceCell.Options.UseFont = true;
			this.colSoluong.AppearanceCell.Options.UseTextOptions = true;
			this.colSoluong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSoluong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colSoluong.AppearanceHeader.Options.UseFont = true;
			this.colSoluong.AppearanceHeader.Options.UseForeColor = true;
			this.colSoluong.AppearanceHeader.Options.UseTextOptions = true;
			this.colSoluong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSoluong.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colSoluong.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.colSoluong.Caption = "Số lượng trả";
			this.colSoluong.FieldName = "Soluong";
			this.colSoluong.Name = "colSoluong";
			this.colSoluong.OptionsFilter.AllowAutoFilter = false;
			this.colSoluong.OptionsFilter.AllowFilter = false;
			this.colSoluong.Visible = true;
			this.colSoluong.VisibleIndex = 0;
			this.colSoluong.Width = 81;
			// 
			// colHistoryProductRTCID
			// 
			this.colHistoryProductRTCID.Caption = "HistoryProductRTCID";
			this.colHistoryProductRTCID.FieldName = "HistoryProductRTCID";
			this.colHistoryProductRTCID.Name = "colHistoryProductRTCID";
			// 
			// btnDelete
			// 
			this.btnDelete.AutoHeight = false;
			editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
			this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// frmReturnAdmin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1204, 675);
			this.Controls.Add(this.grdData);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.mnuMenu);
			this.Name = "frmReturnAdmin";
			this.Text = "TRẢ THIẾT BỊ";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmReturnAdmin_Load);
			this.mnuMenu.ResumeLayout(false);
			this.mnuMenu.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRTCID;
        private DevExpress.XtraGrid.Columns.GridColumn colQrcode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCodeRTC;
        private DevExpress.XtraGrid.Columns.GridColumn colVitri;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colSoluong;
        private DevExpress.XtraGrid.Columns.GridColumn colHistoryProductRTCID;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private System.Windows.Forms.TextBox txtQrCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
    }
}