
namespace BMS
{
    partial class frmTSAssetManagementHistoryLog
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
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTSAssetCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateBorrow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateExpectedReturn = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(12, 12);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(1224, 507);
            this.grdData.TabIndex = 1;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // grvData
            // 
            this.grvData.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grvData.Appearance.Row.Options.UseFont = true;
            this.grvData.Appearance.Row.Options.UseTextOptions = true;
            this.grvData.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvData.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvData.ColumnPanelRowHeight = 40;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTSAssetCode,
            this.colTSAssetName,
            this.colDateBorrow,
            this.colDateExpectedReturn});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colTSAssetCode
            // 
            this.colTSAssetCode.Caption = "Mã tài sản";
            this.colTSAssetCode.FieldName = "TSAssetCode";
            this.colTSAssetCode.Name = "colTSAssetCode";
            this.colTSAssetCode.OptionsColumn.AllowEdit = false;
            this.colTSAssetCode.Visible = true;
            this.colTSAssetCode.VisibleIndex = 0;
            // 
            // colTSAssetName
            // 
            this.colTSAssetName.Caption = "Tên tài sản";
            this.colTSAssetName.FieldName = "TSAssetName";
            this.colTSAssetName.Name = "colTSAssetName";
            this.colTSAssetName.OptionsColumn.AllowEdit = false;
            this.colTSAssetName.Visible = true;
            this.colTSAssetName.VisibleIndex = 1;
            // 
            // colDateBorrow
            // 
            this.colDateBorrow.Caption = "Ngày mượn";
            this.colDateBorrow.FieldName = "DateBorrow";
            this.colDateBorrow.Name = "colDateBorrow";
            this.colDateBorrow.OptionsColumn.AllowEdit = false;
            this.colDateBorrow.Visible = true;
            this.colDateBorrow.VisibleIndex = 2;
            // 
            // colDateExpectedReturn
            // 
            this.colDateExpectedReturn.Caption = "Ngày dự kiến trả";
            this.colDateExpectedReturn.FieldName = "DateExpectedReturn";
            this.colDateExpectedReturn.Name = "colDateExpectedReturn";
            this.colDateExpectedReturn.OptionsColumn.AllowEdit = false;
            this.colDateExpectedReturn.Visible = true;
            this.colDateExpectedReturn.VisibleIndex = 3;
            // 
            // frmTSAssetManagementHistoryLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 531);
            this.Controls.Add(this.grdData);
            this.Name = "frmTSAssetManagementHistoryLog";
            this.Text = "LỊCH SỬ GIA HẠN TÀI SẢN";
            this.Load += new System.EventHandler(this.frmTSAssetManagementHistoryLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colTSAssetCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTSAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn colDateBorrow;
        private DevExpress.XtraGrid.Columns.GridColumn colDateExpectedReturn;
    }
}