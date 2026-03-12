
namespace Forms.KPI_PO
{
    partial class ucKPIAdmin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdAdmin = new DevExpress.XtraGrid.GridControl();
            this.grvAdmin = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.KPI = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colKPI = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colQuantity = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCompletionRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colMonth1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMonth3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMonth2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colQuantityActual = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPercentActual = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colItemText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAdmin
            // 
            this.grdAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAdmin.Location = new System.Drawing.Point(0, 0);
            this.grdAdmin.MainView = this.grvAdmin;
            this.grdAdmin.Name = "grdAdmin";
            this.grdAdmin.Size = new System.Drawing.Size(813, 446);
            this.grdAdmin.TabIndex = 0;
            this.grdAdmin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvAdmin});
            this.grdAdmin.Click += new System.EventHandler(this.grdAdmin_Click);
            // 
            // grvAdmin
            // 
            this.grvAdmin.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.KPI,
            this.gridBand2,
            this.gridBand4,
            this.gridBand3});
            this.grvAdmin.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colKPI,
            this.colCompletionRate,
            this.colQuantity,
            this.colMonth1,
            this.colMonth2,
            this.colMonth3,
            this.colQuantityActual,
            this.colPercentActual,
            this.colID,
            this.colItemText});
            this.grvAdmin.GridControl = this.grdAdmin;
            this.grvAdmin.GroupCount = 1;
            this.grvAdmin.Name = "grvAdmin";
            this.grvAdmin.OptionsView.ShowFooter = true;
            this.grvAdmin.OptionsView.ShowGroupPanel = false;
            this.grvAdmin.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colItemText, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvAdmin.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvAdmin_CellValueChanged);
            // 
            // KPI
            // 
            this.KPI.Columns.Add(this.colKPI);
            this.KPI.Name = "KPI";
            this.KPI.VisibleIndex = 0;
            this.KPI.Width = 75;
            // 
            // colKPI
            // 
            this.colKPI.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colKPI.AppearanceCell.Options.UseFont = true;
            this.colKPI.AppearanceCell.Options.UseTextOptions = true;
            this.colKPI.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colKPI.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colKPI.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colKPI.AppearanceHeader.Options.UseFont = true;
            this.colKPI.AppearanceHeader.Options.UseForeColor = true;
            this.colKPI.AppearanceHeader.Options.UseTextOptions = true;
            this.colKPI.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKPI.Caption = "KPI";
            this.colKPI.FieldName = "KPI";
            this.colKPI.Name = "colKPI";
            this.colKPI.OptionsColumn.ReadOnly = true;
            this.colKPI.Visible = true;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBand2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand2.AppearanceHeader.Options.UseFont = true;
            this.gridBand2.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "Target";
            this.gridBand2.Columns.Add(this.colQuantity);
            this.gridBand2.Columns.Add(this.colCompletionRate);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 194;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQuantity.AppearanceCell.Options.UseFont = true;
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantity.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQuantity.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQuantity.AppearanceHeader.Options.UseFont = true;
            this.colQuantity.AppearanceHeader.Options.UseForeColor = true;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.DisplayFormat.FormatString = "n0";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Visible = true;
            this.colQuantity.Width = 97;
            // 
            // colCompletionRate
            // 
            this.colCompletionRate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCompletionRate.AppearanceCell.Options.UseFont = true;
            this.colCompletionRate.AppearanceCell.Options.UseTextOptions = true;
            this.colCompletionRate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCompletionRate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCompletionRate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCompletionRate.AppearanceHeader.Options.UseFont = true;
            this.colCompletionRate.AppearanceHeader.Options.UseForeColor = true;
            this.colCompletionRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompletionRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompletionRate.Caption = "Completion Rate";
            this.colCompletionRate.DisplayFormat.FormatString = "p";
            this.colCompletionRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCompletionRate.FieldName = "CompletionRate";
            this.colCompletionRate.Name = "colCompletionRate";
            this.colCompletionRate.OptionsColumn.ReadOnly = true;
            this.colCompletionRate.Visible = true;
            this.colCompletionRate.Width = 97;
            // 
            // gridBand4
            // 
            this.gridBand4.Columns.Add(this.colMonth1);
            this.gridBand4.Columns.Add(this.colMonth3);
            this.gridBand4.Columns.Add(this.colMonth2);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 2;
            this.gridBand4.Width = 225;
            // 
            // colMonth1
            // 
            this.colMonth1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth1.AppearanceCell.Options.UseFont = true;
            this.colMonth1.AppearanceCell.Options.UseTextOptions = true;
            this.colMonth1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMonth1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMonth1.AppearanceHeader.Options.UseFont = true;
            this.colMonth1.AppearanceHeader.Options.UseForeColor = true;
            this.colMonth1.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth1.Caption = "Tháng 1";
            this.colMonth1.FieldName = "Month1";
            this.colMonth1.Name = "colMonth1";
            this.colMonth1.Visible = true;
            // 
            // colMonth3
            // 
            this.colMonth3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth3.AppearanceCell.Options.UseFont = true;
            this.colMonth3.AppearanceCell.Options.UseTextOptions = true;
            this.colMonth3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMonth3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMonth3.AppearanceHeader.Options.UseFont = true;
            this.colMonth3.AppearanceHeader.Options.UseForeColor = true;
            this.colMonth3.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth3.Caption = "Tháng 3";
            this.colMonth3.FieldName = "Month3";
            this.colMonth3.Name = "colMonth3";
            this.colMonth3.Visible = true;
            // 
            // colMonth2
            // 
            this.colMonth2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth2.AppearanceCell.Options.UseFont = true;
            this.colMonth2.AppearanceCell.Options.UseTextOptions = true;
            this.colMonth2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMonth2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMonth2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMonth2.AppearanceHeader.Options.UseFont = true;
            this.colMonth2.AppearanceHeader.Options.UseForeColor = true;
            this.colMonth2.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth2.Caption = "Tháng 2";
            this.colMonth2.FieldName = "Month2";
            this.colMonth2.Name = "colMonth2";
            this.colMonth2.Visible = true;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBand3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand3.AppearanceHeader.Options.UseFont = true;
            this.gridBand3.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "Actual";
            this.gridBand3.Columns.Add(this.colQuantityActual);
            this.gridBand3.Columns.Add(this.colPercentActual);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 3;
            this.gridBand3.Width = 150;
            // 
            // colQuantityActual
            // 
            this.colQuantityActual.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQuantityActual.AppearanceCell.Options.UseFont = true;
            this.colQuantityActual.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantityActual.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantityActual.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colQuantityActual.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colQuantityActual.AppearanceHeader.Options.UseFont = true;
            this.colQuantityActual.AppearanceHeader.Options.UseForeColor = true;
            this.colQuantityActual.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantityActual.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantityActual.Caption = "Quantity";
            this.colQuantityActual.DisplayFormat.FormatString = "n0";
            this.colQuantityActual.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantityActual.FieldName = "QuantityActual";
            this.colQuantityActual.Name = "colQuantityActual";
            this.colQuantityActual.OptionsColumn.ReadOnly = true;
            this.colQuantityActual.Visible = true;
            // 
            // colPercentActual
            // 
            this.colPercentActual.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPercentActual.AppearanceCell.Options.UseFont = true;
            this.colPercentActual.AppearanceCell.Options.UseTextOptions = true;
            this.colPercentActual.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colPercentActual.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPercentActual.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPercentActual.AppearanceHeader.Options.UseFont = true;
            this.colPercentActual.AppearanceHeader.Options.UseForeColor = true;
            this.colPercentActual.AppearanceHeader.Options.UseTextOptions = true;
            this.colPercentActual.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPercentActual.Caption = "Percent";
            this.colPercentActual.DisplayFormat.FormatString = "p";
            this.colPercentActual.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPercentActual.FieldName = "PercentActual";
            this.colPercentActual.Name = "colPercentActual";
            this.colPercentActual.OptionsColumn.ReadOnly = true;
            this.colPercentActual.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PercentActual", "SUM={0:0.##}")});
            this.colPercentActual.Visible = true;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.ReadOnly = true;
            this.colID.Visible = true;
            // 
            // colItemText
            // 
            this.colItemText.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colItemText.AppearanceCell.Options.UseFont = true;
            this.colItemText.AppearanceCell.Options.UseTextOptions = true;
            this.colItemText.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colItemText.Caption = "Item";
            this.colItemText.FieldName = "ItemText";
            this.colItemText.Name = "colItemText";
            this.colItemText.OptionsColumn.ReadOnly = true;
            this.colItemText.Visible = true;
            // 
            // ucKPIAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdAdmin);
            this.Name = "ucKPIAdmin";
            this.Size = new System.Drawing.Size(813, 446);
            this.Load += new System.EventHandler(this.ucKPIAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvAdmin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdAdmin;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvAdmin;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKPI;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQuantity;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCompletionRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMonth1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMonth3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMonth2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colQuantityActual;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPercentActual;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand KPI;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colItemText;
    }
}
