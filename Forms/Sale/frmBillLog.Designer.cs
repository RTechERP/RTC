
namespace BMS
{
    partial class frmBillLog
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
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillExportID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusBillText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusBill = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillImportID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(800, 450);
            this.grdData.TabIndex = 0;
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
            this.colID,
            this.colBillImportID,
            this.colBillExportID,
            this.colBillCode,
            this.colStatusBillText,
            this.colDateStatus,
            this.colCreatedName,
            this.colStatusBill});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsBehavior.Editable = false;
            this.grvData.OptionsBehavior.ReadOnly = true;
            this.grvData.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvData.OptionsSelection.MultiSelect = true;
            this.grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colBillExportID
            // 
            this.colBillExportID.FieldName = "BillExportID";
            this.colBillExportID.Name = "colBillExportID";
            // 
            // colBillCode
            // 
            this.colBillCode.Caption = "Mã phiếu";
            this.colBillCode.FieldName = "BillCode";
            this.colBillCode.Name = "colBillCode";
            this.colBillCode.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BillCode", "{0}")});
            this.colBillCode.Visible = true;
            this.colBillCode.VisibleIndex = 0;
            // 
            // colStatusBillText
            // 
            this.colStatusBillText.Caption = "Trạng thái";
            this.colStatusBillText.FieldName = "StatusBillText";
            this.colStatusBillText.Name = "colStatusBillText";
            this.colStatusBillText.Visible = true;
            this.colStatusBillText.VisibleIndex = 1;
            // 
            // colDateStatus
            // 
            this.colDateStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colDateStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDateStatus.Caption = "Ngày";
            this.colDateStatus.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colDateStatus.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDateStatus.FieldName = "DateStatus";
            this.colDateStatus.Name = "colDateStatus";
            this.colDateStatus.Visible = true;
            this.colDateStatus.VisibleIndex = 2;
            // 
            // colCreatedName
            // 
            this.colCreatedName.Caption = "Người nhận";
            this.colCreatedName.FieldName = "CreatedName";
            this.colCreatedName.Name = "colCreatedName";
            this.colCreatedName.Visible = true;
            this.colCreatedName.VisibleIndex = 3;
            // 
            // colStatusBill
            // 
            this.colStatusBill.FieldName = "StatusBill";
            this.colStatusBill.Name = "colStatusBill";
            // 
            // colBillImportID
            // 
            this.colBillImportID.FieldName = "BillImportID";
            this.colBillImportID.Name = "colBillImportID";
            // 
            // frmBillLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grdData);
            this.Name = "frmBillLog";
            this.Text = "LỊCH SỬ NHẬN CHỨNG TỪ";
            this.Load += new System.EventHandler(this.frmBillLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colBillExportID;
        private DevExpress.XtraGrid.Columns.GridColumn colBillCode;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusBillText;
        private DevExpress.XtraGrid.Columns.GridColumn colDateStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedName;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusBill;
        private DevExpress.XtraGrid.Columns.GridColumn colBillImportID;
    }
}