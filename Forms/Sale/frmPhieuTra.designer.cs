
namespace Forms.Sale
{
    partial class frmPhieuTra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhieuTra));
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaPhieuMuon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNguoiMuon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSLT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemain = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdData.Location = new System.Drawing.Point(0, 53);
            this.grdData.MainView = this.grvData;
            this.grdData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(638, 534);
            this.grdData.TabIndex = 0;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // grvData
            // 
            this.grvData.ActiveFilterEnabled = false;
            this.grvData.Appearance.FocusedRow.BackColor = System.Drawing.Color.Red;
            this.grvData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaPhieuMuon,
            this.colNguoiMuon,
            this.colProductID,
            this.colQty,
            this.colSLT,
            this.colRemain,
            this.colCreatedDate});
            this.grvData.DetailHeight = 284;
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsFind.AlwaysVisible = true;
            this.grvData.OptionsView.ColumnAutoWidth = false;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.RowAutoHeight = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvData_CustomDrawCell);
            this.grvData.DoubleClick += new System.EventHandler(this.grvData_DoubleClick);
            // 
            // colMaPhieuMuon
            // 
            this.colMaPhieuMuon.AppearanceCell.Options.HighPriority = true;
            this.colMaPhieuMuon.AppearanceCell.Options.UseBorderColor = true;
            this.colMaPhieuMuon.AppearanceCell.Options.UseFont = true;
            this.colMaPhieuMuon.AppearanceCell.Options.UseForeColor = true;
            this.colMaPhieuMuon.AppearanceCell.Options.UseTextOptions = true;
            this.colMaPhieuMuon.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaPhieuMuon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaPhieuMuon.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colMaPhieuMuon.AppearanceHeader.Options.UseBackColor = true;
            this.colMaPhieuMuon.AppearanceHeader.Options.UseFont = true;
            this.colMaPhieuMuon.AppearanceHeader.Options.UseForeColor = true;
            this.colMaPhieuMuon.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaPhieuMuon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaPhieuMuon.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMaPhieuMuon.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMaPhieuMuon.Caption = "Mã phiếu mượn ";
            this.colMaPhieuMuon.FieldName = "Code";
            this.colMaPhieuMuon.MinWidth = 19;
            this.colMaPhieuMuon.Name = "colMaPhieuMuon";
            this.colMaPhieuMuon.OptionsColumn.AllowEdit = false;
            this.colMaPhieuMuon.Visible = true;
            this.colMaPhieuMuon.VisibleIndex = 1;
            this.colMaPhieuMuon.Width = 164;
            // 
            // colNguoiMuon
            // 
            this.colNguoiMuon.AppearanceCell.Options.HighPriority = true;
            this.colNguoiMuon.AppearanceCell.Options.UseBorderColor = true;
            this.colNguoiMuon.AppearanceCell.Options.UseFont = true;
            this.colNguoiMuon.AppearanceCell.Options.UseForeColor = true;
            this.colNguoiMuon.AppearanceCell.Options.UseTextOptions = true;
            this.colNguoiMuon.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNguoiMuon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNguoiMuon.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colNguoiMuon.AppearanceHeader.Options.UseBackColor = true;
            this.colNguoiMuon.AppearanceHeader.Options.UseFont = true;
            this.colNguoiMuon.AppearanceHeader.Options.UseForeColor = true;
            this.colNguoiMuon.AppearanceHeader.Options.UseTextOptions = true;
            this.colNguoiMuon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNguoiMuon.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNguoiMuon.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNguoiMuon.Caption = "Người mượn";
            this.colNguoiMuon.FieldName = "FullName";
            this.colNguoiMuon.MinWidth = 19;
            this.colNguoiMuon.Name = "colNguoiMuon";
            this.colNguoiMuon.OptionsColumn.AllowEdit = false;
            this.colNguoiMuon.Visible = true;
            this.colNguoiMuon.VisibleIndex = 2;
            this.colNguoiMuon.Width = 127;
            // 
            // colProductID
            // 
            this.colProductID.AppearanceCell.Options.HighPriority = true;
            this.colProductID.AppearanceCell.Options.UseBorderColor = true;
            this.colProductID.AppearanceCell.Options.UseFont = true;
            this.colProductID.AppearanceCell.Options.UseForeColor = true;
            this.colProductID.AppearanceCell.Options.UseTextOptions = true;
            this.colProductID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colProductID.AppearanceHeader.Options.UseBackColor = true;
            this.colProductID.AppearanceHeader.Options.UseFont = true;
            this.colProductID.AppearanceHeader.Options.UseForeColor = true;
            this.colProductID.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colProductID.Caption = "Product ID";
            this.colProductID.FieldName = "ProductID";
            this.colProductID.MinWidth = 19;
            this.colProductID.Name = "colProductID";
            this.colProductID.OptionsColumn.AllowEdit = false;
            this.colProductID.Width = 70;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Options.HighPriority = true;
            this.colQty.AppearanceCell.Options.UseBorderColor = true;
            this.colQty.AppearanceCell.Options.UseFont = true;
            this.colQty.AppearanceCell.Options.UseForeColor = true;
            this.colQty.AppearanceCell.Options.UseTextOptions = true;
            this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colQty.AppearanceHeader.Options.UseBackColor = true;
            this.colQty.AppearanceHeader.Options.UseFont = true;
            this.colQty.AppearanceHeader.Options.UseForeColor = true;
            this.colQty.AppearanceHeader.Options.UseTextOptions = true;
            this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQty.Caption = "Số lượng mượn";
            this.colQty.FieldName = "Qty";
            this.colQty.MinWidth = 19;
            this.colQty.Name = "colQty";
            this.colQty.OptionsColumn.AllowEdit = false;
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 3;
            this.colQty.Width = 87;
            // 
            // colSLT
            // 
            this.colSLT.AppearanceCell.Options.UseFont = true;
            this.colSLT.AppearanceCell.Options.UseTextOptions = true;
            this.colSLT.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colSLT.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSLT.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSLT.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colSLT.AppearanceHeader.Options.UseBackColor = true;
            this.colSLT.AppearanceHeader.Options.UseFont = true;
            this.colSLT.AppearanceHeader.Options.UseForeColor = true;
            this.colSLT.AppearanceHeader.Options.UseTextOptions = true;
            this.colSLT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSLT.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSLT.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSLT.Caption = "Số lượng trả";
            this.colSLT.FieldName = "ReturnAmount";
            this.colSLT.MinWidth = 19;
            this.colSLT.Name = "colSLT";
            this.colSLT.Visible = true;
            this.colSLT.VisibleIndex = 4;
            this.colSLT.Width = 70;
            // 
            // colRemain
            // 
            this.colRemain.AppearanceCell.Options.UseFont = true;
            this.colRemain.AppearanceCell.Options.UseTextOptions = true;
            this.colRemain.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colRemain.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRemain.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRemain.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colRemain.AppearanceHeader.Options.UseBackColor = true;
            this.colRemain.AppearanceHeader.Options.UseFont = true;
            this.colRemain.AppearanceHeader.Options.UseForeColor = true;
            this.colRemain.AppearanceHeader.Options.UseTextOptions = true;
            this.colRemain.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRemain.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colRemain.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colRemain.Caption = "Đang Mượn";
            this.colRemain.FieldName = "Remain";
            this.colRemain.MinWidth = 19;
            this.colRemain.Name = "colRemain";
            this.colRemain.Visible = true;
            this.colRemain.VisibleIndex = 5;
            this.colRemain.Width = 59;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colCreatedDate.AppearanceHeader.Options.UseBackColor = true;
            this.colCreatedDate.AppearanceHeader.Options.UseFont = true;
            this.colCreatedDate.AppearanceHeader.Options.UseForeColor = true;
            this.colCreatedDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCreatedDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCreatedDate.Caption = "Ngày tạo";
            this.colCreatedDate.FieldName = "CreatedDate";
            this.colCreatedDate.MinWidth = 19;
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.Visible = true;
            this.colCreatedDate.VisibleIndex = 0;
            this.colCreatedDate.Width = 104;
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelect});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(638, 42);
            this.mnuMenu.TabIndex = 1;
            this.mnuMenu.Text = "`";
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(44, 39);
            this.btnSelect.Tag = "";
            this.btnSelect.Text = "Chọn";
            this.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmPhieuTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 587);
            this.Controls.Add(this.mnuMenu);
            this.Controls.Add(this.grdData);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "frmPhieuTra";
            this.Text = "Phiếu Mượn";
            this.Load += new System.EventHandler(this.frmPhieuTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colMaPhieuMuon;
        private DevExpress.XtraGrid.Columns.GridColumn colNguoiMuon;
        private DevExpress.XtraGrid.Columns.GridColumn colProductID;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colSLT;
        private DevExpress.XtraGrid.Columns.GridColumn colRemain;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSelect;
    }
}