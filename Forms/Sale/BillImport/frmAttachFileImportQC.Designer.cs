namespace BMS
{
    partial class frmAttachFileImportQC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttachFileImportQC));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnCloseSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnChooseFile = new System.Windows.Forms.ToolStripButton();
            this.grdFileData = new DevExpress.XtraGrid.GridControl();
            this.grvFileData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteFile = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteFile)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseSave,
            this.toolStripSeparator3,
            this.btnChooseFile});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(650, 52);
            this.mnuMenu.TabIndex = 91;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnCloseSave
            // 
            this.btnCloseSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnCloseSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCloseSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseSave.Name = "btnCloseSave";
            this.btnCloseSave.Size = new System.Drawing.Size(80, 49);
            this.btnCloseSave.Tag = "";
            this.btnCloseSave.Text = "Cất và Đóng";
            this.btnCloseSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCloseSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCloseSave.Click += new System.EventHandler(this.btnCloseSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseFile.Image = global::Forms.Properties.Resources.ShowProduct_32x32;
            this.btnChooseFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnChooseFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(59, 49);
            this.btnChooseFile.Tag = "";
            this.btnChooseFile.Text = "Chọn file";
            this.btnChooseFile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChooseFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // grdFileData
            // 
            this.grdFileData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileData.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdFileData.Location = new System.Drawing.Point(0, 52);
            this.grdFileData.MainView = this.grvFileData;
            this.grdFileData.Margin = new System.Windows.Forms.Padding(2);
            this.grdFileData.Name = "grdFileData";
            this.grdFileData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDeleteFile});
            this.grdFileData.Size = new System.Drawing.Size(650, 327);
            this.grdFileData.TabIndex = 92;
            this.grdFileData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvFileData});
            // 
            // grvFileData
            // 
            this.grvFileData.Appearance.HeaderPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.grvFileData.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.grvFileData.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvFileData.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvFileData.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvFileData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvFileData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvFileData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvFileData.ColumnPanelRowHeight = 40;
            this.grvFileData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.colFileName,
            this.gridColumn6});
            this.grvFileData.DetailHeight = 284;
            this.grvFileData.GridControl = this.grdFileData;
            this.grvFileData.Name = "grvFileData";
            this.grvFileData.OptionsBehavior.ReadOnly = true;
            this.grvFileData.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.ColumnEdit = this.btnDeleteFile;
            this.gridColumn5.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("gridColumn5.ImageOptions.SvgImage")));
            this.gridColumn5.MaxWidth = 37;
            this.gridColumn5.MinWidth = 19;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 37;
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.AutoHeight = false;
            this.btnDeleteFile.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDeleteFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDeleteFile_ButtonClick);
            // 
            // colFileName
            // 
            this.colFileName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8F);
            this.colFileName.AppearanceCell.Options.UseFont = true;
            this.colFileName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.colFileName.AppearanceHeader.Options.UseFont = true;
            this.colFileName.AppearanceHeader.Options.UseForeColor = true;
            this.colFileName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFileName.Caption = "Tên File";
            this.colFileName.FieldName = "FileName";
            this.colFileName.MinWidth = 19;
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 1;
            this.colFileName.Width = 115;
            // 
            // gridColumn6
            // 
            this.gridColumn6.FieldName = "ID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Width = 50;
            // 
            // frmAttachFileImportQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 379);
            this.Controls.Add(this.grdFileData);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmAttachFileImportQC";
            this.Text = "CHỌN FILE";
            this.Load += new System.EventHandler(this.frmAttachFileImportQC_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvFileData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnCloseSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnChooseFile;
        private DevExpress.XtraGrid.GridControl grdFileData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvFileData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteFile;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}