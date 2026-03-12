
namespace Forms.KPI_PO
{
    partial class ucKPIStaff
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
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMainIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPercentIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colMGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.grbthang1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGoal0 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colResult0 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colACCP0 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grbthang2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGoal1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colResult1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colACCP1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.grbthang3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGoal2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colResult2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colACCP2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbQuy = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGoal = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colResult = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colACCP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIDHistory = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.cbMain = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbMain,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3});
            this.grdData.Size = new System.Drawing.Size(1343, 594);
            this.grdData.TabIndex = 6;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.Click += new System.EventHandler(this.grdData_Click);
            // 
            // grvData
            // 
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.grbthang1,
            this.grbthang2,
            this.grbthang3,
            this.gbQuy});
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colID,
            this.colMainIndex,
            this.colMGroup,
            this.colPercentIndex,
            this.colGoal0,
            this.colGoal1,
            this.colGoal2,
            this.colResult0,
            this.colResult1,
            this.colResult2,
            this.colACCP0,
            this.colACCP1,
            this.colACCP2,
            this.colGoal,
            this.colResult,
            this.colACCP,
            this.colIDHistory});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, "")});
            this.grvData.Name = "grvData";
            this.grvData.OptionsPrint.PrintFixedColumnsOnEveryPage = true;
            this.grvData.OptionsPrint.PrintPreview = true;
            this.grvData.OptionsPrint.SplitCellPreviewAcrossPages = true;
            this.grvData.OptionsPrint.SplitDataCellAcrossPages = true;
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grvData_CustomDrawCell);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridBand1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand1.AppearanceHeader.Options.UseBackColor = true;
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "PRODUCTIVITY INDEX";
            this.gridBand1.Columns.Add(this.colID);
            this.gridBand1.Columns.Add(this.colMainIndex);
            this.gridBand1.Columns.Add(this.colPercentIndex);
            this.gridBand1.Columns.Add(this.colMGroup);
            this.gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand1.MinWidth = 300;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 400;
            // 
            // colID
            // 
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colMainIndex
            // 
            this.colMainIndex.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colMainIndex.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMainIndex.AppearanceHeader.Options.UseFont = true;
            this.colMainIndex.AppearanceHeader.Options.UseForeColor = true;
            this.colMainIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colMainIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMainIndex.Caption = "Main Index";
            this.colMainIndex.FieldName = "MainIndex";
            this.colMainIndex.Name = "colMainIndex";
            this.colMainIndex.OptionsColumn.ReadOnly = true;
            this.colMainIndex.Visible = true;
            this.colMainIndex.Width = 325;
            // 
            // colPercentIndex
            // 
            this.colPercentIndex.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPercentIndex.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colPercentIndex.AppearanceHeader.Options.UseFont = true;
            this.colPercentIndex.AppearanceHeader.Options.UseForeColor = true;
            this.colPercentIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colPercentIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPercentIndex.Caption = "Percent";
            this.colPercentIndex.ColumnEdit = this.repositoryItemTextEdit1;
            this.colPercentIndex.FieldName = "PercentIndex";
            this.colPercentIndex.Name = "colPercentIndex";
            this.colPercentIndex.OptionsColumn.ReadOnly = true;
            this.colPercentIndex.Visible = true;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BeepOnError = false;
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "p");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.UseMaskAsDisplayFormat = true;
            // 
            // colMGroup
            // 
            this.colMGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colMGroup.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMGroup.AppearanceHeader.Options.UseFont = true;
            this.colMGroup.AppearanceHeader.Options.UseForeColor = true;
            this.colMGroup.AppearanceHeader.Options.UseTextOptions = true;
            this.colMGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMGroup.Caption = "Nhóm";
            this.colMGroup.FieldName = "MGroup";
            this.colMGroup.Name = "colMGroup";
            this.colMGroup.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colMGroup.OptionsColumn.ReadOnly = true;
            this.colMGroup.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            // 
            // grbthang1
            // 
            this.grbthang1.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grbthang1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grbthang1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.grbthang1.AppearanceHeader.Options.UseBackColor = true;
            this.grbthang1.AppearanceHeader.Options.UseFont = true;
            this.grbthang1.AppearanceHeader.Options.UseForeColor = true;
            this.grbthang1.AppearanceHeader.Options.UseTextOptions = true;
            this.grbthang1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grbthang1.Caption = "Tháng 1";
            this.grbthang1.Columns.Add(this.colGoal0);
            this.grbthang1.Columns.Add(this.colResult0);
            this.grbthang1.Columns.Add(this.colACCP0);
            this.grbthang1.Name = "grbthang1";
            this.grbthang1.VisibleIndex = 1;
            this.grbthang1.Width = 201;
            // 
            // colGoal0
            // 
            this.colGoal0.AppearanceCell.Options.UseTextOptions = true;
            this.colGoal0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGoal0.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGoal0.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal0.AppearanceHeader.Options.UseFont = true;
            this.colGoal0.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal0.AppearanceHeader.Options.UseTextOptions = true;
            this.colGoal0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGoal0.Caption = "Goal";
            this.colGoal0.ColumnEdit = this.repositoryItemTextEdit2;
            this.colGoal0.DisplayFormat.FormatString = "n0";
            this.colGoal0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGoal0.FieldName = "Goal0";
            this.colGoal0.Name = "colGoal0";
            this.colGoal0.OptionsColumn.ReadOnly = true;
            this.colGoal0.Visible = true;
            this.colGoal0.Width = 66;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.BeepOnError = false;
            this.repositoryItemTextEdit2.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit2.MaskSettings.Set("mask", "n0");
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            this.repositoryItemTextEdit2.UseMaskAsDisplayFormat = true;
            // 
            // colResult0
            // 
            this.colResult0.AppearanceCell.Options.UseTextOptions = true;
            this.colResult0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colResult0.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colResult0.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colResult0.AppearanceHeader.Options.UseFont = true;
            this.colResult0.AppearanceHeader.Options.UseForeColor = true;
            this.colResult0.AppearanceHeader.Options.UseTextOptions = true;
            this.colResult0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResult0.Caption = "Result";
            this.colResult0.ColumnEdit = this.repositoryItemTextEdit2;
            this.colResult0.FieldName = "Result0";
            this.colResult0.Name = "colResult0";
            this.colResult0.OptionsColumn.ReadOnly = true;
            this.colResult0.Visible = true;
            this.colResult0.Width = 66;
            // 
            // colACCP0
            // 
            this.colACCP0.AppearanceCell.Options.UseTextOptions = true;
            this.colACCP0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colACCP0.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colACCP0.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colACCP0.AppearanceHeader.Options.UseFont = true;
            this.colACCP0.AppearanceHeader.Options.UseForeColor = true;
            this.colACCP0.AppearanceHeader.Options.UseTextOptions = true;
            this.colACCP0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colACCP0.Caption = "ACCP";
            this.colACCP0.ColumnEdit = this.repositoryItemTextEdit3;
            this.colACCP0.DisplayFormat.FormatString = "p";
            this.colACCP0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colACCP0.FieldName = "ACCP0";
            this.colACCP0.Name = "colACCP0";
            this.colACCP0.OptionsColumn.ReadOnly = true;
            this.colACCP0.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ACCP0", "SUM={0:p}")});
            this.colACCP0.Visible = true;
            this.colACCP0.Width = 69;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.BeepOnError = false;
            this.repositoryItemTextEdit3.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit3.MaskSettings.Set("mask", "p");
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            this.repositoryItemTextEdit3.UseMaskAsDisplayFormat = true;
            // 
            // grbthang2
            // 
            this.grbthang2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grbthang2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grbthang2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.grbthang2.AppearanceHeader.Options.UseBackColor = true;
            this.grbthang2.AppearanceHeader.Options.UseFont = true;
            this.grbthang2.AppearanceHeader.Options.UseForeColor = true;
            this.grbthang2.AppearanceHeader.Options.UseTextOptions = true;
            this.grbthang2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grbthang2.Caption = "Tháng 2";
            this.grbthang2.Columns.Add(this.colGoal1);
            this.grbthang2.Columns.Add(this.colResult1);
            this.grbthang2.Columns.Add(this.colACCP1);
            this.grbthang2.Name = "grbthang2";
            this.grbthang2.VisibleIndex = 2;
            this.grbthang2.Width = 193;
            // 
            // colGoal1
            // 
            this.colGoal1.AppearanceCell.Options.UseTextOptions = true;
            this.colGoal1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGoal1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGoal1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal1.AppearanceHeader.Options.UseFont = true;
            this.colGoal1.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal1.AppearanceHeader.Options.UseTextOptions = true;
            this.colGoal1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGoal1.Caption = "Goal";
            this.colGoal1.ColumnEdit = this.repositoryItemTextEdit2;
            this.colGoal1.DisplayFormat.FormatString = "n0";
            this.colGoal1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGoal1.FieldName = "Goal1";
            this.colGoal1.Name = "colGoal1";
            this.colGoal1.OptionsColumn.ReadOnly = true;
            this.colGoal1.Visible = true;
            this.colGoal1.Width = 63;
            // 
            // colResult1
            // 
            this.colResult1.AppearanceCell.Options.UseTextOptions = true;
            this.colResult1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colResult1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colResult1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colResult1.AppearanceHeader.Options.UseFont = true;
            this.colResult1.AppearanceHeader.Options.UseForeColor = true;
            this.colResult1.AppearanceHeader.Options.UseTextOptions = true;
            this.colResult1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResult1.Caption = "Result";
            this.colResult1.ColumnEdit = this.repositoryItemTextEdit2;
            this.colResult1.FieldName = "Result1";
            this.colResult1.Name = "colResult1";
            this.colResult1.OptionsColumn.ReadOnly = true;
            this.colResult1.Visible = true;
            this.colResult1.Width = 63;
            // 
            // colACCP1
            // 
            this.colACCP1.AppearanceCell.Options.UseTextOptions = true;
            this.colACCP1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colACCP1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colACCP1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colACCP1.AppearanceHeader.Options.UseFont = true;
            this.colACCP1.AppearanceHeader.Options.UseForeColor = true;
            this.colACCP1.AppearanceHeader.Options.UseTextOptions = true;
            this.colACCP1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colACCP1.Caption = "ACCP";
            this.colACCP1.ColumnEdit = this.repositoryItemTextEdit3;
            this.colACCP1.DisplayFormat.FormatString = "p";
            this.colACCP1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colACCP1.FieldName = "ACCP1";
            this.colACCP1.Name = "colACCP1";
            this.colACCP1.OptionsColumn.ReadOnly = true;
            this.colACCP1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ACCP1", "SUM={0:p}")});
            this.colACCP1.Visible = true;
            this.colACCP1.Width = 67;
            // 
            // grbthang3
            // 
            this.grbthang3.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grbthang3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grbthang3.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.grbthang3.AppearanceHeader.Options.UseBackColor = true;
            this.grbthang3.AppearanceHeader.Options.UseFont = true;
            this.grbthang3.AppearanceHeader.Options.UseForeColor = true;
            this.grbthang3.AppearanceHeader.Options.UseTextOptions = true;
            this.grbthang3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grbthang3.Caption = "Tháng 3";
            this.grbthang3.Columns.Add(this.colGoal2);
            this.grbthang3.Columns.Add(this.colResult2);
            this.grbthang3.Columns.Add(this.colACCP2);
            this.grbthang3.Name = "grbthang3";
            this.grbthang3.VisibleIndex = 3;
            this.grbthang3.Width = 129;
            // 
            // colGoal2
            // 
            this.colGoal2.AppearanceCell.Options.UseTextOptions = true;
            this.colGoal2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGoal2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGoal2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal2.AppearanceHeader.Options.UseFont = true;
            this.colGoal2.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal2.AppearanceHeader.Options.UseTextOptions = true;
            this.colGoal2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGoal2.Caption = "Goal";
            this.colGoal2.ColumnEdit = this.repositoryItemTextEdit2;
            this.colGoal2.FieldName = "Goal2";
            this.colGoal2.Name = "colGoal2";
            this.colGoal2.OptionsColumn.ReadOnly = true;
            this.colGoal2.Visible = true;
            this.colGoal2.Width = 40;
            // 
            // colResult2
            // 
            this.colResult2.AppearanceCell.Options.UseTextOptions = true;
            this.colResult2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colResult2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colResult2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colResult2.AppearanceHeader.Options.UseFont = true;
            this.colResult2.AppearanceHeader.Options.UseForeColor = true;
            this.colResult2.AppearanceHeader.Options.UseTextOptions = true;
            this.colResult2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResult2.Caption = "Result";
            this.colResult2.ColumnEdit = this.repositoryItemTextEdit2;
            this.colResult2.FieldName = "Result2";
            this.colResult2.Name = "colResult2";
            this.colResult2.OptionsColumn.ReadOnly = true;
            this.colResult2.Visible = true;
            this.colResult2.Width = 47;
            // 
            // colACCP2
            // 
            this.colACCP2.AppearanceCell.Options.UseTextOptions = true;
            this.colACCP2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colACCP2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colACCP2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colACCP2.AppearanceHeader.Options.UseFont = true;
            this.colACCP2.AppearanceHeader.Options.UseForeColor = true;
            this.colACCP2.AppearanceHeader.Options.UseTextOptions = true;
            this.colACCP2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colACCP2.Caption = "ACCP";
            this.colACCP2.ColumnEdit = this.repositoryItemTextEdit3;
            this.colACCP2.DisplayFormat.FormatString = "p";
            this.colACCP2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colACCP2.FieldName = "ACCP2";
            this.colACCP2.Name = "colACCP2";
            this.colACCP2.OptionsColumn.ReadOnly = true;
            this.colACCP2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ACCP2", "SUM={0:p}")});
            this.colACCP2.Visible = true;
            this.colACCP2.Width = 42;
            // 
            // gbQuy
            // 
            this.gbQuy.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gbQuy.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbQuy.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gbQuy.AppearanceHeader.Options.UseBackColor = true;
            this.gbQuy.AppearanceHeader.Options.UseFont = true;
            this.gbQuy.AppearanceHeader.Options.UseForeColor = true;
            this.gbQuy.AppearanceHeader.Options.UseTextOptions = true;
            this.gbQuy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbQuy.Caption = "Quý";
            this.gbQuy.Columns.Add(this.colGoal);
            this.gbQuy.Columns.Add(this.colResult);
            this.gbQuy.Columns.Add(this.colACCP);
            this.gbQuy.MinWidth = 400;
            this.gbQuy.Name = "gbQuy";
            this.gbQuy.VisibleIndex = 4;
            this.gbQuy.Width = 400;
            // 
            // colGoal
            // 
            this.colGoal.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGoal.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal.AppearanceHeader.Options.UseFont = true;
            this.colGoal.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal.Caption = "Goal";
            this.colGoal.ColumnEdit = this.repositoryItemTextEdit2;
            this.colGoal.DisplayFormat.FormatString = "n0";
            this.colGoal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGoal.FieldName = "Goal";
            this.colGoal.Name = "colGoal";
            this.colGoal.OptionsColumn.ReadOnly = true;
            this.colGoal.Visible = true;
            this.colGoal.Width = 133;
            // 
            // colResult
            // 
            this.colResult.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colResult.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colResult.AppearanceHeader.Options.UseFont = true;
            this.colResult.AppearanceHeader.Options.UseForeColor = true;
            this.colResult.Caption = "Result";
            this.colResult.ColumnEdit = this.repositoryItemTextEdit2;
            this.colResult.FieldName = "Result";
            this.colResult.Name = "colResult";
            this.colResult.OptionsColumn.ReadOnly = true;
            this.colResult.Visible = true;
            this.colResult.Width = 133;
            // 
            // colACCP
            // 
            this.colACCP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colACCP.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colACCP.AppearanceHeader.Options.UseFont = true;
            this.colACCP.AppearanceHeader.Options.UseForeColor = true;
            this.colACCP.Caption = "ACCP";
            this.colACCP.ColumnEdit = this.repositoryItemTextEdit3;
            this.colACCP.FieldName = "ACCP";
            this.colACCP.Name = "colACCP";
            this.colACCP.OptionsColumn.ReadOnly = true;
            this.colACCP.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ACCP", "SUM={0:p}")});
            this.colACCP.Visible = true;
            this.colACCP.Width = 134;
            // 
            // colIDHistory
            // 
            this.colIDHistory.Caption = "IDHistory";
            this.colIDHistory.FieldName = "IDHistory";
            this.colIDHistory.Name = "colIDHistory";
            // 
            // cbMain
            // 
            this.cbMain.AutoHeight = false;
            this.cbMain.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbMain.Name = "cbMain";
            this.cbMain.NullText = "";
            this.cbMain.PopupView = this.repositoryItemSearchLookUpEdit2View;
            // 
            // repositoryItemSearchLookUpEdit2View
            // 
            this.repositoryItemSearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit2View.Name = "repositoryItemSearchLookUpEdit2View";
            this.repositoryItemSearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // ucKPIStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdData);
            this.Name = "ucKPIStaff";
            this.Size = new System.Drawing.Size(1343, 594);
            this.Load += new System.EventHandler(this.ucKPIStaff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMainIndex;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMGroup;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal0;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colResult0;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colACCP0;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colResult1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colACCP1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colResult2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colACCP2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colResult;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colACCP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPercentIndex;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbMain;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit2View;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand grbthang1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand grbthang2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand grbthang3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbQuy;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIDHistory;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
    }
}
