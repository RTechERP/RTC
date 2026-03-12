
namespace BMS
{
    partial class frmProductHistoryReturnDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductHistoryReturnDetail));
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.mnuMenu = new System.Windows.Forms.ToolStrip();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.label6 = new System.Windows.Forms.Label();
			this.cbUser = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colMaNV = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.label2 = new System.Windows.Forms.Label();
			this.txtQrCode = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.mnuMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cbUser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.mnuMenu.Size = new System.Drawing.Size(1210, 65);
			this.mnuMenu.TabIndex = 215;
			this.mnuMenu.Text = "toolStrip2";
			// 
			// btnSave
			// 
			this.btnSave.AutoSize = false;
			this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(80, 62);
			this.btnSave.Text = "&Trả";
			this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(379, 50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(186, 51);
			this.label6.TabIndex = 224;
			this.label6.Text = "Người trả";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cbUser
			// 
			this.cbUser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbUser.EditValue = "";
			this.cbUser.Enabled = false;
			this.cbUser.Location = new System.Drawing.Point(570, 52);
			this.cbUser.Margin = new System.Windows.Forms.Padding(2);
			this.cbUser.Name = "cbUser";
			this.cbUser.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbUser.Properties.Appearance.Options.UseFont = true;
			this.cbUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cbUser.Properties.NullText = "";
			this.cbUser.Properties.PopupView = this.searchLookUpEdit1View;
			this.cbUser.Size = new System.Drawing.Size(304, 44);
			this.cbUser.TabIndex = 225;
			// 
			// searchLookUpEdit1View
			// 
			this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaNV,
            this.colFullName,
            this.colUserID});
			this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
			this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// colMaNV
			// 
			this.colMaNV.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colMaNV.AppearanceHeader.Options.UseFont = true;
			this.colMaNV.AppearanceHeader.Options.UseForeColor = true;
			this.colMaNV.AppearanceHeader.Options.UseTextOptions = true;
			this.colMaNV.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMaNV.Caption = "Mã nhân viên";
			this.colMaNV.FieldName = "Code";
			this.colMaNV.Name = "colMaNV";
			this.colMaNV.Visible = true;
			this.colMaNV.VisibleIndex = 1;
			// 
			// colFullName
			// 
			this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colFullName.AppearanceHeader.Options.UseFont = true;
			this.colFullName.AppearanceHeader.Options.UseForeColor = true;
			this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
			this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colFullName.Caption = "Họ và tên";
			this.colFullName.FieldName = "FullName";
			this.colFullName.Name = "colFullName";
			this.colFullName.Visible = true;
			this.colFullName.VisibleIndex = 0;
			// 
			// colUserID
			// 
			this.colUserID.Caption = "ID";
			this.colUserID.FieldName = "ID";
			this.colUserID.Name = "colUserID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 50);
			this.label2.TabIndex = 213;
			this.label2.Text = "QrCode";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtQrCode
			// 
			this.txtQrCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.txtQrCode, 4);
			this.txtQrCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQrCode.Location = new System.Drawing.Point(177, 3);
			this.txtQrCode.Name = "txtQrCode";
			this.txtQrCode.Size = new System.Drawing.Size(1030, 45);
			this.txtQrCode.TabIndex = 1;
			this.txtQrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtQrCode.TextChanged += new System.EventHandler(this.txtQrCode_TextChanged);
			this.txtQrCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQrCode_KeyDown);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(3, 50);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(168, 51);
			this.label7.TabIndex = 213;
			this.label7.Text = "Ngày trả";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dtpReturnDate
			// 
			this.dtpReturnDate.CustomFormat = "dd/MM/yyyy";
			this.dtpReturnDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtpReturnDate.Enabled = false;
			this.dtpReturnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpReturnDate.Location = new System.Drawing.Point(177, 53);
			this.dtpReturnDate.Name = "dtpReturnDate";
			this.dtpReturnDate.Size = new System.Drawing.Size(196, 44);
			this.dtpReturnDate.TabIndex = 212;
			// 
			// grdData
			// 
			this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdData.Location = new System.Drawing.Point(0, 168);
			this.grdData.MainView = this.grvData;
			this.grdData.Name = "grdData";
			this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete,
            this.repositoryItemMemoEdit1});
			this.grdData.Size = new System.Drawing.Size(1210, 482);
			this.grdData.TabIndex = 227;
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
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 308F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 334F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtQrCode, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.dtpReturnDate, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbUser, 3, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 65);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1210, 101);
			this.tableLayoutPanel1.TabIndex = 228;
			// 
			// frmProductHistoryReturnDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1210, 651);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.grdData);
			this.Controls.Add(this.mnuMenu);
			this.KeyPreview = true;
			this.Name = "frmProductHistoryReturnDetail";
			this.Text = "ĐĂNG KÍ TRẢ THIẾT BỊ BẰNG QRCODE";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmProductHistoryReturnDetail_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmProductHistoryReturnDetail_KeyPress);
			this.mnuMenu.ResumeLayout(false);
			this.mnuMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cbUser.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQrCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRTCID;
        private DevExpress.XtraGrid.Columns.GridColumn colQrcode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCodeRTC;
        private DevExpress.XtraGrid.Columns.GridColumn colVitri;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colSoluong;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SearchLookUpEdit cbUser;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colMaNV;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colHistoryProductRTCID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}