namespace BMS
{
    partial class frmProductFilm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductFilm));
			this.mnuMenu = new System.Windows.Forms.ToolStrip();
			this.btnNew = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.btnStock = new System.Windows.Forms.ToolStripButton();
			this.btnStockLocation = new System.Windows.Forms.ToolStripButton();
			this.label7 = new System.Windows.Forms.Label();
			this.txtFilterText = new System.Windows.Forms.TextBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.grdData = new DevExpress.XtraTreeList.TreeList();
			this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colHeight = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colWidth = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colArea = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colPcsPerBox = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colManufactureID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colInventoryNumber = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colStockID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colStockLocationID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.ColParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.mnuMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
			this.SuspendLayout();
			// 
			// mnuMenu
			// 
			this.mnuMenu.AutoSize = false;
			this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete,
            this.btnStock,
            this.btnStockLocation});
			this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.mnuMenu.Location = new System.Drawing.Point(0, 0);
			this.mnuMenu.Name = "mnuMenu";
			this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.mnuMenu.Size = new System.Drawing.Size(1343, 42);
			this.mnuMenu.TabIndex = 23;
			this.mnuMenu.Text = "toolStrip2";
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
			this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(43, 37);
			this.btnNew.Tag = "";
			this.btnNew.Text = "Thêm";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
			// 
			// btnEdit
			// 
			this.btnEdit.AutoSize = false;
			this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
			this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(55, 40);
			this.btnEdit.Tag = "";
			this.btnEdit.Text = "Sửa";
			this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// btnDelete
			// 
			this.btnDelete.AutoSize = false;
			this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(55, 40);
			this.btnDelete.Tag = "";
			this.btnDelete.Text = "Xóa";
			this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnStock
			// 
			this.btnStock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStock.Image = ((System.Drawing.Image)(resources.GetObject("btnStock.Image")));
			this.btnStock.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStock.Name = "btnStock";
			this.btnStock.Size = new System.Drawing.Size(93, 37);
			this.btnStock.Tag = "";
			this.btnStock.Text = "Danh sách kho";
			this.btnStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
			// 
			// btnStockLocation
			// 
			this.btnStockLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStockLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnStockLocation.Image")));
			this.btnStockLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStockLocation.Name = "btnStockLocation";
			this.btnStockLocation.Size = new System.Drawing.Size(122, 37);
			this.btnStockLocation.Tag = "";
			this.btnStockLocation.Text = "Danh sách vị trí kho";
			this.btnStockLocation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStockLocation.Click += new System.EventHandler(this.btnStockLocation_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(5, 52);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 13);
			this.label7.TabIndex = 38;
			this.label7.Text = "Từ khóa";
			// 
			// txtFilterText
			// 
			this.txtFilterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFilterText.Location = new System.Drawing.Point(55, 48);
			this.txtFilterText.Name = "txtFilterText";
			this.txtFilterText.Size = new System.Drawing.Size(427, 24);
			this.txtFilterText.TabIndex = 42;
			// 
			// btnFind
			// 
			this.btnFind.Location = new System.Drawing.Point(487, 48);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 25);
			this.btnFind.TabIndex = 46;
			this.btnFind.Text = "Tìm kiếm";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// grdData
			// 
			this.grdData.AllowDrop = true;
			this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colID,
            this.colCode,
            this.colName,
            this.colHeight,
            this.colWidth,
            this.colArea,
            this.colPcsPerBox,
            this.colManufactureID,
            this.colInventoryNumber,
            this.colStockID,
            this.colStockLocationID,
            this.ColParentID});
			this.grdData.Location = new System.Drawing.Point(2, 80);
			this.grdData.Name = "grdData";
			this.grdData.OptionsBehavior.DragNodes = true;
			this.grdData.OptionsBehavior.Editable = false;
			this.grdData.Size = new System.Drawing.Size(1339, 510);
			this.grdData.TabIndex = 47;
			// 
			// colID
			// 
			this.colID.Caption = "Mã nhóm";
			this.colID.FieldName = "ID";
			this.colID.Name = "colID";
			// 
			// colCode
			// 
			this.colCode.AppearanceCell.Options.UseTextOptions = true;
			this.colCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCode.AppearanceHeader.Options.UseFont = true;
			this.colCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colCode.Caption = "Mã";
			this.colCode.FieldName = "Code";
			this.colCode.Name = "colCode";
			this.colCode.OptionsColumn.ReadOnly = true;
			this.colCode.Visible = true;
			this.colCode.VisibleIndex = 0;
			this.colCode.Width = 129;
			// 
			// colName
			// 
			this.colName.AppearanceCell.Options.UseTextOptions = true;
			this.colName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colName.AppearanceHeader.Options.UseFont = true;
			this.colName.AppearanceHeader.Options.UseTextOptions = true;
			this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colName.Caption = "Tên Sản Phẩm";
			this.colName.FieldName = "Name";
			this.colName.Name = "colName";
			this.colName.OptionsColumn.ReadOnly = true;
			this.colName.Visible = true;
			this.colName.VisibleIndex = 1;
			this.colName.Width = 374;
			// 
			// colHeight
			// 
			this.colHeight.AppearanceCell.Options.UseTextOptions = true;
			this.colHeight.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colHeight.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colHeight.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colHeight.AppearanceHeader.Options.UseFont = true;
			this.colHeight.AppearanceHeader.Options.UseTextOptions = true;
			this.colHeight.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colHeight.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colHeight.Caption = "Chiều Dài";
			this.colHeight.FieldName = "Height";
			this.colHeight.Name = "colHeight";
			this.colHeight.OptionsColumn.ReadOnly = true;
			this.colHeight.Visible = true;
			this.colHeight.VisibleIndex = 5;
			this.colHeight.Width = 118;
			// 
			// colWidth
			// 
			this.colWidth.AppearanceCell.Options.UseTextOptions = true;
			this.colWidth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colWidth.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colWidth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colWidth.AppearanceHeader.Options.UseFont = true;
			this.colWidth.AppearanceHeader.Options.UseTextOptions = true;
			this.colWidth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colWidth.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colWidth.Caption = "Chiều Rộng";
			this.colWidth.FieldName = "Width";
			this.colWidth.Name = "colWidth";
			this.colWidth.OptionsColumn.ReadOnly = true;
			this.colWidth.Visible = true;
			this.colWidth.VisibleIndex = 6;
			this.colWidth.Width = 118;
			// 
			// colArea
			// 
			this.colArea.AppearanceCell.Options.UseTextOptions = true;
			this.colArea.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colArea.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colArea.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colArea.AppearanceHeader.Options.UseFont = true;
			this.colArea.AppearanceHeader.Options.UseTextOptions = true;
			this.colArea.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colArea.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colArea.Caption = "Diện Tích";
			this.colArea.FieldName = "Area";
			this.colArea.Name = "colArea";
			this.colArea.OptionsColumn.ReadOnly = true;
			this.colArea.Visible = true;
			this.colArea.VisibleIndex = 7;
			this.colArea.Width = 118;
			// 
			// colPcsPerBox
			// 
			this.colPcsPerBox.AppearanceCell.Options.UseTextOptions = true;
			this.colPcsPerBox.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colPcsPerBox.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colPcsPerBox.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colPcsPerBox.AppearanceHeader.Options.UseFont = true;
			this.colPcsPerBox.AppearanceHeader.Options.UseTextOptions = true;
			this.colPcsPerBox.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colPcsPerBox.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colPcsPerBox.Caption = "Số miếng/ Box";
			this.colPcsPerBox.FieldName = "PcsPerBox";
			this.colPcsPerBox.Name = "colPcsPerBox";
			this.colPcsPerBox.OptionsColumn.ReadOnly = true;
			this.colPcsPerBox.Visible = true;
			this.colPcsPerBox.VisibleIndex = 8;
			this.colPcsPerBox.Width = 117;
			// 
			// colManufactureID
			// 
			this.colManufactureID.AppearanceCell.Options.UseTextOptions = true;
			this.colManufactureID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colManufactureID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colManufactureID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colManufactureID.AppearanceHeader.Options.UseFont = true;
			this.colManufactureID.AppearanceHeader.Options.UseTextOptions = true;
			this.colManufactureID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colManufactureID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colManufactureID.Caption = "Hãng";
			this.colManufactureID.FieldName = "ManufacturerName";
			this.colManufactureID.Name = "colManufactureID";
			this.colManufactureID.OptionsColumn.ReadOnly = true;
			this.colManufactureID.Visible = true;
			this.colManufactureID.VisibleIndex = 2;
			this.colManufactureID.Width = 71;
			// 
			// colInventoryNumber
			// 
			this.colInventoryNumber.AppearanceCell.Options.UseTextOptions = true;
			this.colInventoryNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colInventoryNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colInventoryNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colInventoryNumber.AppearanceHeader.Options.UseFont = true;
			this.colInventoryNumber.AppearanceHeader.Options.UseTextOptions = true;
			this.colInventoryNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colInventoryNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colInventoryNumber.Caption = " Số Lượng Tồn Kho";
			this.colInventoryNumber.FieldName = "InventoryNumber";
			this.colInventoryNumber.Name = "colInventoryNumber";
			this.colInventoryNumber.OptionsColumn.ReadOnly = true;
			this.colInventoryNumber.Visible = true;
			this.colInventoryNumber.VisibleIndex = 9;
			this.colInventoryNumber.Width = 126;
			// 
			// colStockID
			// 
			this.colStockID.AppearanceCell.Options.UseTextOptions = true;
			this.colStockID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStockID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colStockID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colStockID.AppearanceHeader.Options.UseFont = true;
			this.colStockID.AppearanceHeader.Options.UseTextOptions = true;
			this.colStockID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStockID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colStockID.Caption = "Kho";
			this.colStockID.FieldName = "StockName";
			this.colStockID.Name = "colStockID";
			this.colStockID.OptionsColumn.ReadOnly = true;
			this.colStockID.Visible = true;
			this.colStockID.VisibleIndex = 3;
			this.colStockID.Width = 70;
			// 
			// colStockLocationID
			// 
			this.colStockLocationID.AppearanceCell.Options.UseTextOptions = true;
			this.colStockLocationID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStockLocationID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colStockLocationID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colStockLocationID.AppearanceHeader.Options.UseFont = true;
			this.colStockLocationID.AppearanceHeader.Options.UseTextOptions = true;
			this.colStockLocationID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStockLocationID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.colStockLocationID.Caption = "Vị Trí";
			this.colStockLocationID.FieldName = "StockLocationName";
			this.colStockLocationID.Name = "colStockLocationID";
			this.colStockLocationID.OptionsColumn.ReadOnly = true;
			this.colStockLocationID.Visible = true;
			this.colStockLocationID.VisibleIndex = 4;
			this.colStockLocationID.Width = 63;
			// 
			// ColParentID
			// 
			this.ColParentID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ColParentID.AppearanceHeader.Options.UseFont = true;
			this.ColParentID.AppearanceHeader.Options.UseTextOptions = true;
			this.ColParentID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.ColParentID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.ColParentID.Caption = "Sản phẩm cha";
			this.ColParentID.FieldName = "ParentName";
			this.ColParentID.Name = "ColParentID";
			this.ColParentID.Width = 125;
			// 
			// frmProductFilm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1343, 592);
			this.Controls.Add(this.grdData);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.txtFilterText);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.mnuMenu);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmProductFilm";
			this.Text = "DANH SÁCH VẬT TƯ/THIẾT BỊ";
			this.Load += new System.EventHandler(this.frmPart_Load);
			this.mnuMenu.ResumeLayout(false);
			this.mnuMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraTreeList.TreeList grdData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colHeight;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colWidth;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colArea;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPcsPerBox;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colManufactureID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colInventoryNumber;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colStockID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colStockLocationID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ColParentID;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnStock;
		private System.Windows.Forms.ToolStripButton btnStockLocation;
	}
}