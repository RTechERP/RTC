
namespace BMS
{
	partial class frmAddQRCodeProductsHCM
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
			this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
			this.bar2 = new DevExpress.XtraBars.Bar();
			this.btnAddAndClose = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.btnAddAndNew = new DevExpress.XtraBars.BarLargeButtonItem();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtQrCode = new System.Windows.Forms.TextBox();
			this.cboProduct = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProductCodes = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colVT = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMaNB = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboProduct.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager1
			// 
			this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAddAndClose,
            this.btnAddAndNew});
			this.barManager1.MainMenu = this.bar2;
			this.barManager1.MaxItemId = 2;
			// 
			// bar2
			// 
			this.bar2.BarName = "Main menu";
			this.bar2.DockCol = 0;
			this.bar2.DockRow = 0;
			this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddAndClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddAndNew)});
			this.bar2.OptionsBar.MultiLine = true;
			this.bar2.OptionsBar.UseWholeRow = true;
			this.bar2.Text = "Main menu";
			// 
			// btnAddAndClose
			// 
			this.btnAddAndClose.Caption = "CẤT && ĐÓNG";
			this.btnAddAndClose.Id = 0;
			this.btnAddAndClose.ImageOptions.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
			this.btnAddAndClose.Name = "btnAddAndClose";
			this.btnAddAndClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.btnAddAndClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddAndClose_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager1;
			this.barDockControlTop.Size = new System.Drawing.Size(535, 56);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 203);
			this.barDockControlBottom.Manager = this.barManager1;
			this.barDockControlBottom.Size = new System.Drawing.Size(535, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 56);
			this.barDockControlLeft.Manager = this.barManager1;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 147);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(535, 56);
			this.barDockControlRight.Manager = this.barManager1;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 147);
			// 
			// btnAddAndNew
			// 
			this.btnAddAndNew.Caption = "CẤT && THÊM MỚI";
			this.btnAddAndNew.Id = 1;
			this.btnAddAndNew.ImageOptions.Image = global::Forms.Properties.Resources.Save_32x322;
			this.btnAddAndNew.Name = "btnAddAndNew";
			this.btnAddAndNew.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.btnAddAndNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddAndNew_ItemClick);
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("Tahoma", 11.25F);
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Trong Kho",
            "Đang mượn",
            "Đã xuất",
            "Lost"});
			this.comboBox1.Location = new System.Drawing.Point(100, 168);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(423, 26);
			this.comboBox1.TabIndex = 207;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 172);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 18);
			this.label3.TabIndex = 204;
			this.label3.Text = "Status";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82, 18);
			this.label2.TabIndex = 205;
			this.label2.Text = "Mã QrCode";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 18);
			this.label1.TabIndex = 206;
			this.label1.Text = "Thiết bị";
			// 
			// txtQrCode
			// 
			this.txtQrCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtQrCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQrCode.Location = new System.Drawing.Point(100, 92);
			this.txtQrCode.Multiline = true;
			this.txtQrCode.Name = "txtQrCode";
			this.txtQrCode.Size = new System.Drawing.Size(423, 70);
			this.txtQrCode.TabIndex = 203;
			// 
			// cboProduct
			// 
			this.cboProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboProduct.Location = new System.Drawing.Point(100, 62);
			this.cboProduct.Name = "cboProduct";
			this.cboProduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboProduct.Properties.Appearance.Options.UseFont = true;
			this.cboProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboProduct.Properties.NullText = "";
			this.cboProduct.Properties.PopupFormMinSize = new System.Drawing.Size(1000, 500);
			this.cboProduct.Properties.PopupView = this.searchLookUpEdit1View;
			this.cboProduct.Size = new System.Drawing.Size(423, 24);
			this.cboProduct.TabIndex = 202;
			// 
			// searchLookUpEdit1View
			// 
			this.searchLookUpEdit1View.ColumnPanelRowHeight = 35;
			this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colProductCodes,
            this.colProductName,
            this.colVT,
            this.colMaNB,
            this.colNote});
			this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
			this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// colID
			// 
			this.colID.Caption = "ID";
			this.colID.FieldName = "ID";
			this.colID.Name = "colID";
			// 
			// colProductCodes
			// 
			this.colProductCodes.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.colProductCodes.AppearanceHeader.Options.UseFont = true;
			this.colProductCodes.AppearanceHeader.Options.UseForeColor = true;
			this.colProductCodes.AppearanceHeader.Options.UseTextOptions = true;
			this.colProductCodes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colProductCodes.Caption = "Mã sản phẩm";
			this.colProductCodes.FieldName = "ProductCode";
			this.colProductCodes.Name = "colProductCodes";
			this.colProductCodes.Visible = true;
			this.colProductCodes.VisibleIndex = 0;
			this.colProductCodes.Width = 127;
			// 
			// colProductName
			// 
			this.colProductName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.colProductName.AppearanceHeader.Options.UseFont = true;
			this.colProductName.AppearanceHeader.Options.UseForeColor = true;
			this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
			this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colProductName.Caption = "Mã sản phẩm ";
			this.colProductName.FieldName = "ProductName";
			this.colProductName.Name = "colProductName";
			this.colProductName.Visible = true;
			this.colProductName.VisibleIndex = 1;
			this.colProductName.Width = 186;
			// 
			// colVT
			// 
			this.colVT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.colVT.AppearanceHeader.Options.UseFont = true;
			this.colVT.AppearanceHeader.Options.UseForeColor = true;
			this.colVT.AppearanceHeader.Options.UseTextOptions = true;
			this.colVT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colVT.Caption = "Vị trí";
			this.colVT.FieldName = "AddressBox";
			this.colVT.Name = "colVT";
			this.colVT.Visible = true;
			this.colVT.VisibleIndex = 2;
			this.colVT.Width = 151;
			// 
			// colMaNB
			// 
			this.colMaNB.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.colMaNB.AppearanceHeader.Options.UseFont = true;
			this.colMaNB.AppearanceHeader.Options.UseForeColor = true;
			this.colMaNB.AppearanceHeader.Options.UseTextOptions = true;
			this.colMaNB.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMaNB.Caption = "Mã nội bộ";
			this.colMaNB.FieldName = "ProductCodeRTC";
			this.colMaNB.Name = "colMaNB";
			this.colMaNB.Visible = true;
			this.colMaNB.VisibleIndex = 3;
			this.colMaNB.Width = 91;
			// 
			// colNote
			// 
			this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.colNote.AppearanceHeader.Options.UseFont = true;
			this.colNote.AppearanceHeader.Options.UseForeColor = true;
			this.colNote.AppearanceHeader.Options.UseTextOptions = true;
			this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colNote.Caption = "Ghi chú";
			this.colNote.FieldName = "Note";
			this.colNote.Name = "colNote";
			this.colNote.Visible = true;
			this.colNote.VisibleIndex = 4;
			this.colNote.Width = 419;
			// 
			// frmAddQRCodeProductsHCM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 203);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtQrCode);
			this.Controls.Add(this.cboProduct);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "frmAddQRCodeProductsHCM";
			this.Text = "CHI TIẾT QR CODE";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddQRCodeProductsHCM_FormClosed);
			this.Load += new System.EventHandler(this.frmAddQRCodeProductsHCM_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAddQRCodeProductsHCM_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboProduct.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.BarLargeButtonItem btnAddAndClose;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.BarLargeButtonItem btnAddAndNew;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtQrCode;
		private DevExpress.XtraEditors.SearchLookUpEdit cboProduct;
		private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
		private DevExpress.XtraGrid.Columns.GridColumn colID;
		private DevExpress.XtraGrid.Columns.GridColumn colProductCodes;
		private DevExpress.XtraGrid.Columns.GridColumn colProductName;
		private DevExpress.XtraGrid.Columns.GridColumn colVT;
		private DevExpress.XtraGrid.Columns.GridColumn colMaNB;
		private DevExpress.XtraGrid.Columns.GridColumn colNote;
	}
}