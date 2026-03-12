
namespace Forms.KPI_PO
{
    partial class ucGoalStaffLead
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
            this.colMainIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colGoal0 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.txtEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colGoal1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colGoal2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colIDMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(0, 0);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.txtEdit});
            this.grdData.Size = new System.Drawing.Size(827, 474);
            this.grdData.TabIndex = 8;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.Click += new System.EventHandler(this.grdData_Click);
            // 
            // grvData
            // 
            this.grvData.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2});
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colID,
            this.colMainIndex,
            this.colGoal0,
            this.colIDMain,
            this.colMGroup,
            this.colGoal2,
            this.colGoal1});
            this.grvData.GridControl = this.grdData;
            this.grvData.GroupCount = 1;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMGroup, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colMainIndex);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 369;
            // 
            // colMainIndex
            // 
            this.colMainIndex.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colMainIndex.AppearanceCell.Options.UseFont = true;
            this.colMainIndex.AppearanceCell.Options.UseTextOptions = true;
            this.colMainIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colMainIndex.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colMainIndex.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMainIndex.AppearanceHeader.Options.UseFont = true;
            this.colMainIndex.AppearanceHeader.Options.UseForeColor = true;
            this.colMainIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.colMainIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMainIndex.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMainIndex.Caption = "Main Index";
            this.colMainIndex.FieldName = "MainIndex";
            this.colMainIndex.Name = "colMainIndex";
            this.colMainIndex.OptionsColumn.ReadOnly = true;
            this.colMainIndex.Visible = true;
            this.colMainIndex.Width = 369;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridBand2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.gridBand2.AppearanceHeader.Options.UseFont = true;
            this.gridBand2.AppearanceHeader.Options.UseForeColor = true;
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "Goal";
            this.gridBand2.Columns.Add(this.colGoal0);
            this.gridBand2.Columns.Add(this.colGoal1);
            this.gridBand2.Columns.Add(this.colGoal2);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 1246;
            // 
            // colGoal0
            // 
            this.colGoal0.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colGoal0.AppearanceCell.Options.UseFont = true;
            this.colGoal0.AppearanceCell.Options.UseTextOptions = true;
            this.colGoal0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colGoal0.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colGoal0.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal0.AppearanceHeader.Options.UseFont = true;
            this.colGoal0.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal0.AppearanceHeader.Options.UseTextOptions = true;
            this.colGoal0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGoal0.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGoal0.Caption = "Tháng 1";
            this.colGoal0.ColumnEdit = this.txtEdit;
            this.colGoal0.FieldName = "Goal0";
            this.colGoal0.Name = "colGoal0";
            this.colGoal0.Visible = true;
            this.colGoal0.Width = 411;
            // 
            // txtEdit
            // 
            this.txtEdit.AutoHeight = false;
            this.txtEdit.BeepOnError = false;
            this.txtEdit.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtEdit.MaskSettings.Set("mask", "n0");
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.UseMaskAsDisplayFormat = true;
            // 
            // colGoal1
            // 
            this.colGoal1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colGoal1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal1.AppearanceHeader.Options.UseFont = true;
            this.colGoal1.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal1.AppearanceHeader.Options.UseTextOptions = true;
            this.colGoal1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGoal1.Caption = "Tháng 2";
            this.colGoal1.ColumnEdit = this.txtEdit;
            this.colGoal1.FieldName = "Goal1";
            this.colGoal1.Name = "colGoal1";
            this.colGoal1.Visible = true;
            this.colGoal1.Width = 415;
            // 
            // colGoal2
            // 
            this.colGoal2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colGoal2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGoal2.AppearanceHeader.Options.UseFont = true;
            this.colGoal2.AppearanceHeader.Options.UseForeColor = true;
            this.colGoal2.AppearanceHeader.Options.UseTextOptions = true;
            this.colGoal2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGoal2.Caption = "Tháng 3";
            this.colGoal2.ColumnEdit = this.txtEdit;
            this.colGoal2.FieldName = "Goal2";
            this.colGoal2.Name = "colGoal2";
            this.colGoal2.Visible = true;
            this.colGoal2.Width = 420;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colID.AppearanceCell.Options.UseFont = true;
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colIDMain
            // 
            this.colIDMain.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colIDMain.AppearanceCell.Options.UseFont = true;
            this.colIDMain.AppearanceCell.Options.UseTextOptions = true;
            this.colIDMain.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIDMain.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colIDMain.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIDMain.AppearanceHeader.Options.UseFont = true;
            this.colIDMain.AppearanceHeader.Options.UseForeColor = true;
            this.colIDMain.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDMain.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDMain.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDMain.Caption = "IDMain";
            this.colIDMain.FieldName = "IDMain";
            this.colIDMain.Name = "colIDMain";
            // 
            // colMGroup
            // 
            this.colMGroup.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colMGroup.AppearanceCell.Options.UseFont = true;
            this.colMGroup.AppearanceCell.Options.UseTextOptions = true;
            this.colMGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colMGroup.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.colMGroup.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colMGroup.AppearanceHeader.Options.UseFont = true;
            this.colMGroup.AppearanceHeader.Options.UseForeColor = true;
            this.colMGroup.AppearanceHeader.Options.UseTextOptions = true;
            this.colMGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMGroup.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colMGroup.Caption = "Nhóm";
            this.colMGroup.FieldName = "MGroup";
            this.colMGroup.Name = "colMGroup";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BeepOnError = false;
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.repositoryItemTextEdit1.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.repositoryItemTextEdit1.MaskSettings.Set("mask", "P");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.UseMaskAsDisplayFormat = true;
            // 
            // ucGoalStaffLead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdData);
            this.Name = "ucGoalStaffLead";
            this.Size = new System.Drawing.Size(827, 474);
            this.Load += new System.EventHandler(this.ucGoalStaffLead_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtEdit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView grvData;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMainIndex;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal0;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGoal2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colIDMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMGroup;
    }
}
