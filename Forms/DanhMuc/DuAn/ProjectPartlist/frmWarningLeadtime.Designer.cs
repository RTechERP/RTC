namespace Forms.DanhMuc.DuAn.ProjectPartlist
{
    partial class frmWarningLeadtime
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
            this.label1 = new System.Windows.Forms.Label();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalDayLeadtimeQuote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeadlinePur = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDatetimeDeadline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(658, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Các vật tư có Deadline yêu cầu mua sớm hơn Ngày hàng về dự kiến. Bạn có muốn yêu " +
    "cầu mua số vật tư dưới đây không?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 36);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.grdData.Size = new System.Drawing.Size(658, 293);
            this.grdData.TabIndex = 1;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTT,
            this.colProductCode,
            this.colProductName,
            this.colTotalDayLeadtimeQuote,
            this.colQty,
            this.colDeadlinePur,
            this.colDatetimeDeadline});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colTT
            // 
            this.colTT.Caption = "TT";
            this.colTT.FieldName = "TT";
            this.colTT.Name = "colTT";
            this.colTT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "TT", "{0}")});
            this.colTT.Visible = true;
            this.colTT.VisibleIndex = 0;
            this.colTT.Width = 61;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "Mã sản phẩm";
            this.colProductCode.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductCode.FieldName = "ProductCode";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.Visible = true;
            this.colProductCode.VisibleIndex = 1;
            this.colProductCode.Width = 102;
            // 
            // colProductName
            // 
            this.colProductName.Caption = "Tên sản phẩm";
            this.colProductName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colProductName.FieldName = "GroupMaterial";
            this.colProductName.Name = "colProductName";
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 2;
            this.colProductName.Width = 155;
            // 
            // colTotalDayLeadtimeQuote
            // 
            this.colTotalDayLeadtimeQuote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colTotalDayLeadtimeQuote.AppearanceCell.Options.UseFont = true;
            this.colTotalDayLeadtimeQuote.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalDayLeadtimeQuote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalDayLeadtimeQuote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTotalDayLeadtimeQuote.Caption = "Leatime báo giá ( ngày)";
            this.colTotalDayLeadtimeQuote.FieldName = "TotalDayLeadtimeQuote";
            this.colTotalDayLeadtimeQuote.Name = "colTotalDayLeadtimeQuote";
            this.colTotalDayLeadtimeQuote.Visible = true;
            this.colTotalDayLeadtimeQuote.VisibleIndex = 4;
            this.colTotalDayLeadtimeQuote.Width = 98;
            // 
            // colQty
            // 
            this.colQty.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colQty.AppearanceCell.Options.UseFont = true;
            this.colQty.AppearanceCell.Options.UseTextOptions = true;
            this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colQty.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQty.Caption = "Số lượng";
            this.colQty.FieldName = "QtyFull";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 3;
            this.colQty.Width = 60;
            // 
            // colDeadlinePur
            // 
            this.colDeadlinePur.AppearanceCell.Options.UseTextOptions = true;
            this.colDeadlinePur.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeadlinePur.Caption = "Dealine yêu cầu mua";
            this.colDeadlinePur.FieldName = "DeadlinePur";
            this.colDeadlinePur.Name = "colDeadlinePur";
            this.colDeadlinePur.Visible = true;
            this.colDeadlinePur.VisibleIndex = 6;
            this.colDeadlinePur.Width = 103;
            // 
            // colDatetimeDeadline
            // 
            this.colDatetimeDeadline.AppearanceCell.Options.UseTextOptions = true;
            this.colDatetimeDeadline.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDatetimeDeadline.Caption = "Ngày hàng về dự kiến";
            this.colDatetimeDeadline.FieldName = "DatetimeDeadline";
            this.colDatetimeDeadline.Name = "colDatetimeDeadline";
            this.colDatetimeDeadline.Visible = true;
            this.colDatetimeDeadline.VisibleIndex = 5;
            this.colDatetimeDeadline.Width = 89;
            // 
            // btnNo
            // 
            this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Location = new System.Drawing.Point(578, 6);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.BackColor = System.Drawing.Color.LimeGreen;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnYes.Location = new System.Drawing.Point(497, 6);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnYes);
            this.panelControl1.Controls.Add(this.btnNo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 329);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(658, 34);
            this.panelControl1.TabIndex = 4;
            // 
            // frmWarningLeadtime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 363);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.label1);
            this.Name = "frmWarningLeadtime";
            this.Text = "CẢNH BÁO LEADTIME";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWarningLeadtime_FormClosed);
            this.Load += new System.EventHandler(this.frmWarningLeadtime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
        private DevExpress.XtraGrid.Columns.GridColumn colTT;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalDayLeadtimeQuote;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colDeadlinePur;
        private DevExpress.XtraGrid.Columns.GridColumn colDatetimeDeadline;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}