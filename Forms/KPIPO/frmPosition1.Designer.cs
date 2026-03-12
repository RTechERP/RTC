
namespace BMS
{
    partial class frmPosition1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPosition1));
            DevExpress.XtraEditors.BreadCrumbNode breadCrumbNode1 = new DevExpress.XtraEditors.BreadCrumbNode();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSaleUserTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSaleUserTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdGroup = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grvGroup = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIDgroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupSalesCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteGroup = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemBreadCrumbEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemBreadCrumbEdit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroup)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemBreadCrumbEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(800, 42);
            this.mnuMenu.TabIndex = 5;
            this.mnuMenu.TabStop = true;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 37);
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdData
            // 
            this.grdData.ContextMenuStrip = this.contextMenuStrip2;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(3, 207);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(794, 198);
            this.grdData.TabIndex = 7;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(136, 26);
            // 
            // xóaToolStripMenuItem1
            // 
            this.xóaToolStripMenuItem1.Name = "xóaToolStripMenuItem1";
            this.xóaToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.xóaToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.xóaToolStripMenuItem1.Text = "&Xóa";
            this.xóaToolStripMenuItem1.Click += new System.EventHandler(this.xóaToolStripMenuItem1_Click);
            // 
            // grvData
            // 
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSaleUserTypeName,
            this.colSaleUserTypeCode});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvData.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colSaleUserTypeName
            // 
            this.colSaleUserTypeName.AppearanceCell.Options.UseTextOptions = true;
            this.colSaleUserTypeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSaleUserTypeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSaleUserTypeName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSaleUserTypeName.AppearanceHeader.Options.UseFont = true;
            this.colSaleUserTypeName.AppearanceHeader.Options.UseForeColor = true;
            this.colSaleUserTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colSaleUserTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSaleUserTypeName.Caption = "Chức vụ";
            this.colSaleUserTypeName.FieldName = "SaleUserTypeName";
            this.colSaleUserTypeName.Name = "colSaleUserTypeName";
            this.colSaleUserTypeName.Visible = true;
            this.colSaleUserTypeName.VisibleIndex = 0;
            // 
            // colSaleUserTypeCode
            // 
            this.colSaleUserTypeCode.AppearanceCell.Options.UseTextOptions = true;
            this.colSaleUserTypeCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSaleUserTypeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSaleUserTypeCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSaleUserTypeCode.AppearanceHeader.Options.UseFont = true;
            this.colSaleUserTypeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colSaleUserTypeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colSaleUserTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSaleUserTypeCode.Caption = "Mã";
            this.colSaleUserTypeCode.FieldName = "SaleUserTypeCode";
            this.colSaleUserTypeCode.Name = "colSaleUserTypeCode";
            this.colSaleUserTypeCode.Visible = true;
            this.colSaleUserTypeCode.VisibleIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.grdGroup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdData, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 408);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // grdGroup
            // 
            this.grdGroup.ContextMenuStrip = this.contextMenuStrip1;
            this.grdGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGroup.Location = new System.Drawing.Point(3, 3);
            this.grdGroup.MainView = this.grvGroup;
            this.grdGroup.Name = "grdGroup";
            this.grdGroup.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDeleteGroup,
            this.repositoryItemBreadCrumbEdit1});
            this.grdGroup.Size = new System.Drawing.Size(794, 198);
            this.grdGroup.TabIndex = 8;
            this.grdGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvGroup});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 26);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.xóaToolStripMenuItem.Text = "&Xóa";
            this.xóaToolStripMenuItem.Click += new System.EventHandler(this.xóaToolStripMenuItem_Click);
            // 
            // grvGroup
            // 
            this.grvGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIDgroup,
            this.colGroupSalesName,
            this.colGroupSalesCode});
            this.grvGroup.GridControl = this.grdGroup;
            this.grvGroup.Name = "grvGroup";
            this.grvGroup.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.grvGroup.OptionsView.ShowGroupPanel = false;
            // 
            // colIDgroup
            // 
            this.colIDgroup.AppearanceCell.Options.UseTextOptions = true;
            this.colIDgroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDgroup.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDgroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIDgroup.Caption = "ID";
            this.colIDgroup.FieldName = "ID";
            this.colIDgroup.Name = "colIDgroup";
            // 
            // colGroupSalesName
            // 
            this.colGroupSalesName.AppearanceCell.Options.UseTextOptions = true;
            this.colGroupSalesName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupSalesName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroupSalesName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGroupSalesName.AppearanceHeader.Options.UseFont = true;
            this.colGroupSalesName.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupSalesName.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupSalesName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupSalesName.Caption = "Nhóm";
            this.colGroupSalesName.FieldName = "GroupSalesName";
            this.colGroupSalesName.Name = "colGroupSalesName";
            this.colGroupSalesName.Visible = true;
            this.colGroupSalesName.VisibleIndex = 1;
            // 
            // colGroupSalesCode
            // 
            this.colGroupSalesCode.AppearanceCell.Options.UseTextOptions = true;
            this.colGroupSalesCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupSalesCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colGroupSalesCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGroupSalesCode.AppearanceHeader.Options.UseFont = true;
            this.colGroupSalesCode.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupSalesCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupSalesCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGroupSalesCode.Caption = "Mã nhóm";
            this.colGroupSalesCode.FieldName = "GroupSalesCode";
            this.colGroupSalesCode.Name = "colGroupSalesCode";
            this.colGroupSalesCode.Visible = true;
            this.colGroupSalesCode.VisibleIndex = 0;
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.AutoHeight = false;
            this.btnDeleteGroup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnDeleteGroup.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.btnDeleteGroup.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteGroup.ContextImageOptions.Image")));
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            // 
            // repositoryItemBreadCrumbEdit1
            // 
            this.repositoryItemBreadCrumbEdit1.AutoHeight = false;
            this.repositoryItemBreadCrumbEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBreadCrumbEdit1.Name = "repositoryItemBreadCrumbEdit1";
            breadCrumbNode1.Caption = "string";
            breadCrumbNode1.Value = "string";
            this.repositoryItemBreadCrumbEdit1.Nodes.AddRange(new DevExpress.XtraEditors.BreadCrumbNode[] {
            breadCrumbNode1});
            // 
            // frmPosition1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmPosition1";
            this.Text = "CHỨC VỤ";
            this.Load += new System.EventHandler(this.frmPosition1_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdGroup)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemBreadCrumbEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleUserTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleUserTypeCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl grdGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView grvGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colIDgroup;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupSalesCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteGroup;
        private DevExpress.XtraEditors.Repository.RepositoryItemBreadCrumbEdit repositoryItemBreadCrumbEdit1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
    }
}