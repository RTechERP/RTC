
namespace BMS
{
    partial class frmEmployeeSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeSetting));
            this.grdTypeBusiness = new DevExpress.XtraGrid.GridControl();
            this.grvTypeBusiness = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDtype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdVehicle = new DevExpress.XtraGrid.GridControl();
            this.grvVehicle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVehicleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVehicleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEditCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteType = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdTypeBusiness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTypeBusiness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvVehicle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdTypeBusiness
            // 
            this.grdTypeBusiness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTypeBusiness.Location = new System.Drawing.Point(0, 45);
            this.grdTypeBusiness.MainView = this.grvTypeBusiness;
            this.grdTypeBusiness.Name = "grdTypeBusiness";
            this.grdTypeBusiness.Size = new System.Drawing.Size(592, 695);
            this.grdTypeBusiness.TabIndex = 9;
            this.grdTypeBusiness.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTypeBusiness});
            // 
            // grvTypeBusiness
            // 
            this.grvTypeBusiness.ColumnPanelRowHeight = 40;
            this.grvTypeBusiness.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDtype,
            this.colTypeCode,
            this.colTypeName,
            this.colCostType});
            this.grvTypeBusiness.GridControl = this.grdTypeBusiness;
            this.grvTypeBusiness.Name = "grvTypeBusiness";
            this.grvTypeBusiness.OptionsBehavior.Editable = false;
            this.grvTypeBusiness.OptionsView.ShowGroupPanel = false;
            // 
            // colIDtype
            // 
            this.colIDtype.Caption = "ID";
            this.colIDtype.FieldName = "ID";
            this.colIDtype.Name = "colIDtype";
            // 
            // colTypeCode
            // 
            this.colTypeCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeCode.AppearanceCell.Options.UseFont = true;
            this.colTypeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeCode.AppearanceHeader.Options.UseFont = true;
            this.colTypeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colTypeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeCode.Caption = "Mã loại công tác";
            this.colTypeCode.FieldName = "TypeCode";
            this.colTypeCode.Name = "colTypeCode";
            this.colTypeCode.OptionsColumn.ReadOnly = true;
            this.colTypeCode.OptionsFilter.AllowAutoFilter = false;
            this.colTypeCode.OptionsFilter.AllowFilter = false;
            this.colTypeCode.Visible = true;
            this.colTypeCode.VisibleIndex = 0;
            // 
            // colTypeName
            // 
            this.colTypeName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeName.AppearanceCell.Options.UseFont = true;
            this.colTypeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeName.AppearanceHeader.Options.UseFont = true;
            this.colTypeName.AppearanceHeader.Options.UseForeColor = true;
            this.colTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeName.Caption = "Tên loại công tác";
            this.colTypeName.FieldName = "TypeName";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.OptionsColumn.ReadOnly = true;
            this.colTypeName.OptionsFilter.AllowAutoFilter = false;
            this.colTypeName.OptionsFilter.AllowFilter = false;
            this.colTypeName.Visible = true;
            this.colTypeName.VisibleIndex = 1;
            // 
            // colCostType
            // 
            this.colCostType.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCostType.AppearanceCell.Options.UseFont = true;
            this.colCostType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCostType.AppearanceHeader.Options.UseFont = true;
            this.colCostType.AppearanceHeader.Options.UseForeColor = true;
            this.colCostType.AppearanceHeader.Options.UseTextOptions = true;
            this.colCostType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCostType.Caption = "Phụ cấp";
            this.colCostType.DisplayFormat.FormatString = "N0";
            this.colCostType.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCostType.FieldName = "Cost";
            this.colCostType.Name = "colCostType";
            this.colCostType.OptionsColumn.ReadOnly = true;
            this.colCostType.OptionsFilter.AllowAutoFilter = false;
            this.colCostType.OptionsFilter.AllowFilter = false;
            this.colCostType.Visible = true;
            this.colCostType.VisibleIndex = 2;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grdVehicle);
            this.splitContainerControl1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grdTypeBusiness);
            this.splitContainerControl1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1274, 740);
            this.splitContainerControl1.SplitterPosition = 672;
            this.splitContainerControl1.TabIndex = 10;
            // 
            // grdVehicle
            // 
            this.grdVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVehicle.Location = new System.Drawing.Point(0, 45);
            this.grdVehicle.MainView = this.grvVehicle;
            this.grdVehicle.Name = "grdVehicle";
            this.grdVehicle.Size = new System.Drawing.Size(672, 695);
            this.grdVehicle.TabIndex = 9;
            this.grdVehicle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvVehicle});
            // 
            // grvVehicle
            // 
            this.grvVehicle.ColumnPanelRowHeight = 40;
            this.grvVehicle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colVehicleCode,
            this.colVehicleName,
            this.colCost,
            this.colEditCost});
            this.grvVehicle.GridControl = this.grdVehicle;
            this.grvVehicle.Name = "grvVehicle";
            this.grvVehicle.OptionsBehavior.Editable = false;
            this.grvVehicle.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colVehicleCode
            // 
            this.colVehicleCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVehicleCode.AppearanceCell.Options.UseFont = true;
            this.colVehicleCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVehicleCode.AppearanceHeader.Options.UseFont = true;
            this.colVehicleCode.AppearanceHeader.Options.UseForeColor = true;
            this.colVehicleCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colVehicleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVehicleCode.Caption = "Mã phương tiện";
            this.colVehicleCode.FieldName = "VehicleCode";
            this.colVehicleCode.Name = "colVehicleCode";
            this.colVehicleCode.OptionsColumn.ReadOnly = true;
            this.colVehicleCode.OptionsFilter.AllowAutoFilter = false;
            this.colVehicleCode.OptionsFilter.AllowFilter = false;
            this.colVehicleCode.Visible = true;
            this.colVehicleCode.VisibleIndex = 0;
            // 
            // colVehicleName
            // 
            this.colVehicleName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVehicleName.AppearanceCell.Options.UseFont = true;
            this.colVehicleName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colVehicleName.AppearanceHeader.Options.UseFont = true;
            this.colVehicleName.AppearanceHeader.Options.UseForeColor = true;
            this.colVehicleName.AppearanceHeader.Options.UseTextOptions = true;
            this.colVehicleName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVehicleName.Caption = "Tên phương tiện";
            this.colVehicleName.FieldName = "VehicleName";
            this.colVehicleName.Name = "colVehicleName";
            this.colVehicleName.OptionsColumn.ReadOnly = true;
            this.colVehicleName.OptionsFilter.AllowAutoFilter = false;
            this.colVehicleName.OptionsFilter.AllowFilter = false;
            this.colVehicleName.Visible = true;
            this.colVehicleName.VisibleIndex = 1;
            // 
            // colCost
            // 
            this.colCost.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCost.AppearanceCell.Options.UseFont = true;
            this.colCost.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCost.AppearanceHeader.Options.UseFont = true;
            this.colCost.AppearanceHeader.Options.UseForeColor = true;
            this.colCost.AppearanceHeader.Options.UseTextOptions = true;
            this.colCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCost.Caption = "Phụ cấp";
            this.colCost.DisplayFormat.FormatString = "N0";
            this.colCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCost.FieldName = "Cost";
            this.colCost.Name = "colCost";
            this.colCost.OptionsColumn.ReadOnly = true;
            this.colCost.OptionsFilter.AllowAutoFilter = false;
            this.colCost.OptionsFilter.AllowFilter = false;
            this.colCost.Visible = true;
            this.colCost.VisibleIndex = 2;
            // 
            // colEditCost
            // 
            this.colEditCost.Caption = "EditCost";
            this.colEditCost.FieldName = "EditCost";
            this.colEditCost.Name = "colEditCost";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator3,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnDelete});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(672, 45);
            this.toolStrip1.TabIndex = 28;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 41);
            this.btnAdd.Tag = "frmEmployeeSetting_Vehicle_Add";
            this.btnAdd.Text = "Thêm ";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 41);
            this.btnEdit.Tag = "frmEmployeeSetting_Vehicle_Edit";
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
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 41);
            this.btnDelete.Tag = "frmEmployeeSetting_Vehicle_Delete";
            this.btnDelete.Text = "Xóa ";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddType,
            this.toolStripSeparator4,
            this.btnEditType,
            this.toolStripSeparator5,
            this.btnDeleteType});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(592, 45);
            this.toolStrip2.TabIndex = 29;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddType
            // 
            this.btnAddType.AutoSize = false;
            this.btnAddType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddType.Image = ((System.Drawing.Image)(resources.GetObject("btnAddType.Image")));
            this.btnAddType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(80, 41);
            this.btnAddType.Tag = "frmEmployeeSetting_Bussiness_Add";
            this.btnAddType.Text = "Thêm ";
            this.btnAddType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddType.Click += new System.EventHandler(this.btnAddType_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
            // 
            // btnEditType
            // 
            this.btnEditType.AutoSize = false;
            this.btnEditType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditType.Image = ((System.Drawing.Image)(resources.GetObject("btnEditType.Image")));
            this.btnEditType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditType.Name = "btnEditType";
            this.btnEditType.Size = new System.Drawing.Size(80, 41);
            this.btnEditType.Tag = "frmEmployeeSetting_Bussiness_Edit";
            this.btnEditType.Text = "Sửa";
            this.btnEditType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditType.Click += new System.EventHandler(this.btnEditType_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
            // 
            // btnDeleteType
            // 
            this.btnDeleteType.AutoSize = false;
            this.btnDeleteType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteType.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteType.Image")));
            this.btnDeleteType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteType.Name = "btnDeleteType";
            this.btnDeleteType.Size = new System.Drawing.Size(80, 41);
            this.btnDeleteType.Tag = "frmEmployeeSetting_Bussiness_Delete";
            this.btnDeleteType.Text = "Xóa ";
            this.btnDeleteType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteType.Click += new System.EventHandler(this.btnDeleteType_Click);
            // 
            // frmEmployeeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 740);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEmployeeSetting";
            this.Text = "PHỤ PHÍ ";
            this.Load += new System.EventHandler(this.frmEmployeeSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTypeBusiness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTypeBusiness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvVehicle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdTypeBusiness;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTypeBusiness;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private DevExpress.XtraGrid.GridControl grdVehicle;
        private DevExpress.XtraGrid.Views.Grid.GridView grvVehicle;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEditType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnDeleteType;
        private DevExpress.XtraGrid.Columns.GridColumn colIDtype;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colCostType;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colVehicleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colVehicleName;
        private DevExpress.XtraGrid.Columns.GridColumn colCost;
        private DevExpress.XtraGrid.Columns.GridColumn colEditCost;
    }
}