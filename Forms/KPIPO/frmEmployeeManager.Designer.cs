
namespace BMS
{
    partial class frmEmployeeManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployeeManager));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUserType = new System.Windows.Forms.ToolStripButton();
            this.cbGroup = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tlGroupSale = new DevExpress.XtraTreeList.TreeList();
            this.colIDGroup = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroupSalesCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroupSalesName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colFullName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSaleUserTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colUserID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aDDLeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.ckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlGroupSale)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckEdit.Properties)).BeginInit();
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
            this.btnAdd,
            this.toolStripSeparator2,
            this.btnEdit,
            this.toolStripSeparator3,
            this.btnDel,
            this.toolStripSeparator5,
            this.btnUserType});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1090, 42);
            this.mnuMenu.TabIndex = 50;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 33);
            this.btnAdd.Tag = "frmEmployeeManager_Add";
            this.btnAdd.Text = "Thêm nhân viên";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(91, 33);
            this.btnEdit.Tag = "frmEmployeeManager_Edit";
            this.btnEdit.Text = "Sửa nhân viên";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 33);
            this.btnDel.Tag = "frmEmployeeManager_Del";
            this.btnDel.Text = "Xóa nhân viên";
            this.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 23);
            // 
            // btnUserType
            // 
            this.btnUserType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserType.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_Document;
            this.btnUserType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUserType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUserType.Name = "btnUserType";
            this.btnUserType.Size = new System.Drawing.Size(56, 33);
            this.btnUserType.Tag = "frmEmployeeManager_UserType";
            this.btnUserType.Text = "Chức vụ";
            this.btnUserType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUserType.Click += new System.EventHandler(this.btnUserType_Click);
            // 
            // cbGroup
            // 
            this.cbGroup.Location = new System.Drawing.Point(504, 12);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbGroup.Properties.NullText = "";
            this.cbGroup.Properties.PopupView = this.searchLookUpEdit1View;
            this.cbGroup.Size = new System.Drawing.Size(100, 20);
            this.cbGroup.TabIndex = 54;
            this.cbGroup.EditValueChanged += new System.EventHandler(this.cbGroup_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nhóm";
            this.gridColumn2.FieldName = "GroupSalesName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // tlGroupSale
            // 
            this.tlGroupSale.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colIDGroup,
            this.colGroupSalesCode,
            this.colGroupSalesName,
            this.colFullName,
            this.colSaleUserTypeName,
            this.colParentID,
            this.colUserID});
            this.tlGroupSale.ContextMenuStrip = this.contextMenuStrip1;
            this.tlGroupSale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlGroupSale.Location = new System.Drawing.Point(0, 42);
            this.tlGroupSale.Name = "tlGroupSale";
            this.tlGroupSale.OptionsBehavior.Editable = false;
            this.tlGroupSale.OptionsBehavior.PopulateServiceColumns = true;
            this.tlGroupSale.OptionsDragAndDrop.AcceptOuterNodes = true;
            this.tlGroupSale.OptionsDragAndDrop.CanCloneNodesOnDrop = true;
            this.tlGroupSale.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
            this.tlGroupSale.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tlGroupSale.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.tlGroupSale.Size = new System.Drawing.Size(1090, 579);
            this.tlGroupSale.TabIndex = 55;
            this.tlGroupSale.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.tlGroupSale_CustomDrawNodeCell);
            this.tlGroupSale.DragDrop += new System.Windows.Forms.DragEventHandler(this.tlGroupSale_DragDrop);
            this.tlGroupSale.DragEnter += new System.Windows.Forms.DragEventHandler(this.tlGroupSale_DragEnter);
            this.tlGroupSale.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tlGroupSale_MouseDown);
            this.tlGroupSale.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tlGroupSale_MouseUp);
            // 
            // colIDGroup
            // 
            this.colIDGroup.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIDGroup.AppearanceCell.Options.UseFont = true;
            this.colIDGroup.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colIDGroup.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colIDGroup.AppearanceHeader.Options.UseFont = true;
            this.colIDGroup.AppearanceHeader.Options.UseForeColor = true;
            this.colIDGroup.AppearanceHeader.Options.UseTextOptions = true;
            this.colIDGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colIDGroup.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colIDGroup.Caption = "ID";
            this.colIDGroup.FieldName = "ID";
            this.colIDGroup.Name = "colIDGroup";
            this.colIDGroup.OptionsColumn.ReadOnly = true;
            // 
            // colGroupSalesCode
            // 
            this.colGroupSalesCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGroupSalesCode.AppearanceCell.Options.UseFont = true;
            this.colGroupSalesCode.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGroupSalesCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGroupSalesCode.AppearanceHeader.Options.UseFont = true;
            this.colGroupSalesCode.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupSalesCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupSalesCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGroupSalesCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupSalesCode.Caption = "Mã";
            this.colGroupSalesCode.FieldName = "GroupSalesCode";
            this.colGroupSalesCode.Name = "colGroupSalesCode";
            this.colGroupSalesCode.OptionsColumn.ReadOnly = true;
            this.colGroupSalesCode.Width = 31;
            // 
            // colGroupSalesName
            // 
            this.colGroupSalesName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGroupSalesName.AppearanceCell.Options.UseFont = true;
            this.colGroupSalesName.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGroupSalesName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colGroupSalesName.AppearanceHeader.Options.UseFont = true;
            this.colGroupSalesName.AppearanceHeader.Options.UseForeColor = true;
            this.colGroupSalesName.AppearanceHeader.Options.UseTextOptions = true;
            this.colGroupSalesName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGroupSalesName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colGroupSalesName.Caption = "Tên nhóm";
            this.colGroupSalesName.FieldName = "GroupSalesName";
            this.colGroupSalesName.Name = "colGroupSalesName";
            this.colGroupSalesName.OptionsColumn.ReadOnly = true;
            this.colGroupSalesName.Width = 97;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFullName.AppearanceCell.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFullName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colFullName.AppearanceHeader.Options.UseFont = true;
            this.colFullName.AppearanceHeader.Options.UseForeColor = true;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colFullName.Caption = "Tên nhân viên";
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.ReadOnly = true;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 0;
            this.colFullName.Width = 493;
            // 
            // colSaleUserTypeName
            // 
            this.colSaleUserTypeName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSaleUserTypeName.AppearanceCell.Options.UseFont = true;
            this.colSaleUserTypeName.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSaleUserTypeName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colSaleUserTypeName.AppearanceHeader.Options.UseFont = true;
            this.colSaleUserTypeName.AppearanceHeader.Options.UseForeColor = true;
            this.colSaleUserTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colSaleUserTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSaleUserTypeName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSaleUserTypeName.Caption = "Chức vụ";
            this.colSaleUserTypeName.FieldName = "SaleUserTypeName";
            this.colSaleUserTypeName.Name = "colSaleUserTypeName";
            this.colSaleUserTypeName.OptionsColumn.ReadOnly = true;
            this.colSaleUserTypeName.Visible = true;
            this.colSaleUserTypeName.VisibleIndex = 1;
            this.colSaleUserTypeName.Width = 561;
            // 
            // colParentID
            // 
            this.colParentID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colParentID.AppearanceCell.Options.UseFont = true;
            this.colParentID.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colParentID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colParentID.AppearanceHeader.Options.UseFont = true;
            this.colParentID.AppearanceHeader.Options.UseForeColor = true;
            this.colParentID.AppearanceHeader.Options.UseTextOptions = true;
            this.colParentID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colParentID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colParentID.Caption = "ParentID";
            this.colParentID.FieldName = "ParentID";
            this.colParentID.Name = "colParentID";
            this.colParentID.OptionsColumn.ReadOnly = true;
            // 
            // colUserID
            // 
            this.colUserID.Caption = "UserUD";
            this.colUserID.FieldName = "UserID";
            this.colUserID.Name = "colUserID";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDDLeaderToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 48);
            // 
            // aDDLeaderToolStripMenuItem
            // 
            this.aDDLeaderToolStripMenuItem.Name = "aDDLeaderToolStripMenuItem";
            this.aDDLeaderToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.aDDLeaderToolStripMenuItem.Text = "ADD Leader";
            this.aDDLeaderToolStripMenuItem.Click += new System.EventHandler(this.aDDLeaderToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(417, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 76;
            this.label2.Text = "Chọn nhóm";
            // 
            // ckEdit
            // 
            this.ckEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckEdit.Location = new System.Drawing.Point(1035, 15);
            this.ckEdit.Name = "ckEdit";
            this.ckEdit.Properties.Caption = "Edit";
            this.ckEdit.Size = new System.Drawing.Size(43, 20);
            this.ckEdit.TabIndex = 77;
            this.ckEdit.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // frmEmployeeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 621);
            this.Controls.Add(this.ckEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tlGroupSale);
            this.Controls.Add(this.cbGroup);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmEmployeeManager";
            this.Text = "NHÂN VIÊN SALE";
            this.Load += new System.EventHandler(this.frmPosition_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlGroupSale)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnUserType;
        private DevExpress.XtraEditors.SearchLookUpEdit cbGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraTreeList.TreeList tlGroupSale;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIDGroup;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupSalesCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupSalesName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFullName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSaleUserTypeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aDDLeaderToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUserID;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private DevExpress.XtraEditors.CheckEdit ckEdit;
    }
}