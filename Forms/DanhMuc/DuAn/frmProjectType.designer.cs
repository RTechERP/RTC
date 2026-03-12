namespace BMS
{
    partial class frmProjectType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectType));
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDeleteFolder = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aAAAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlData = new DevExpress.XtraTreeList.TreeList();
            this.colID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProjectTypeCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colProjectTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRootFolder = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tlFolderName = new DevExpress.XtraTreeList.TreeList();
            this.colFolderName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIDFolder = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentIDFolder = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.mnuMenu.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlFolderName)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Appearance.Options.UseTextOptions = true;
            this.repositoryItemMemoEdit3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 37);
            this.btnAdd.Tag = "frmProjectType_Update";
            this.btnAdd.Text = "Tạo";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = false;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 37);
            this.btnEdit.Tag = "frmProjectType_Update";
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.AutoSize = false;
            this.btnDel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(80, 37);
            this.btnDel.Tag = "frmProjectType_Update";
            this.btnDel.Text = "Xóa";
            this.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDel.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.toolStripSeparator1,
            this.btnAddFolder,
            this.toolStripSeparator4,
            this.btnEditFolder,
            this.toolStripSeparator5,
            this.btnDeleteFolder});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1265, 42);
            this.mnuMenu.TabIndex = 49;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 37);
            this.toolStripSeparator2.Tag = "frmProjectType_Update";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 37);
            this.toolStripSeparator3.Tag = "frmProjectType_Update";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            this.toolStripSeparator1.Tag = "frmProjectType_Update";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.AutoSize = false;
            this.btnAddFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFolder.Image = global::Forms.Properties.Resources.icons8_add_folder_16;
            this.btnAddFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(80, 37);
            this.btnAddFolder.Tag = "frmProjectType_Update";
            this.btnAddFolder.Text = "Thêm folder";
            this.btnAddFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 37);
            this.toolStripSeparator4.Tag = "frmProjectType_Update";
            this.toolStripSeparator4.Visible = false;
            // 
            // btnEditFolder
            // 
            this.btnEditFolder.AutoSize = false;
            this.btnEditFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditFolder.Image = global::Forms.Properties.Resources.LoadPageSetup_16x16;
            this.btnEditFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditFolder.Name = "btnEditFolder";
            this.btnEditFolder.Size = new System.Drawing.Size(80, 37);
            this.btnEditFolder.Tag = "frmProjectType_Update";
            this.btnEditFolder.Text = "Sửa folder";
            this.btnEditFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditFolder.Visible = false;
            this.btnEditFolder.Click += new System.EventHandler(this.btnEditFolder_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 37);
            this.toolStripSeparator5.Tag = "frmProjectType_Update";
            // 
            // btnDeleteFolder
            // 
            this.btnDeleteFolder.AutoSize = false;
            this.btnDeleteFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFolder.Image = global::Forms.Properties.Resources.icons8_delete_folder_16;
            this.btnDeleteFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDeleteFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteFolder.Name = "btnDeleteFolder";
            this.btnDeleteFolder.Size = new System.Drawing.Size(80, 37);
            this.btnDeleteFolder.Tag = "frmProjectType_Update";
            this.btnDeleteFolder.Text = "Xóa folder";
            this.btnDeleteFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteFolder.Click += new System.EventHandler(this.btnDeleteFolder_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.aAAAToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(146, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Visible = false;
            // 
            // aAAAToolStripMenuItem
            // 
            this.aAAAToolStripMenuItem.Name = "aAAAToolStripMenuItem";
            this.aAAAToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.aAAAToolStripMenuItem.Text = "AAAA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 141;
            this.label5.Text = "Từ khóa";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Location = new System.Drawing.Point(64, 44);
            this.txtFilterText.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.Size = new System.Drawing.Size(342, 20);
            this.txtFilterText.TabIndex = 140;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(410, 43);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(58, 23);
            this.btnFind.TabIndex = 139;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(15, 71);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tlData);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.tlFolderName);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1238, 526);
            this.splitContainerControl1.SplitterPosition = 908;
            this.splitContainerControl1.TabIndex = 142;
            // 
            // tlData
            // 
            this.tlData.Appearance.HeaderPanelBackground.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tlData.Appearance.HeaderPanelBackground.BackColor2 = System.Drawing.SystemColors.ButtonFace;
            this.tlData.Appearance.HeaderPanelBackground.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.tlData.Appearance.HeaderPanelBackground.Options.UseBackColor = true;
            this.tlData.Appearance.HeaderPanelBackground.Options.UseBorderColor = true;
            this.tlData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colID,
            this.colProjectTypeCode,
            this.colProjectTypeName,
            this.colRootFolder});
            this.tlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlData.Location = new System.Drawing.Point(0, 0);
            this.tlData.Name = "tlData";
            this.tlData.OptionsBehavior.Editable = false;
            this.tlData.OptionsBehavior.ReadOnly = true;
            this.tlData.OptionsView.ShowIndicator = false;
            this.tlData.Size = new System.Drawing.Size(908, 526);
            this.tlData.TabIndex = 53;
            this.tlData.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlData_FocusedNodeChanged);
            // 
            // colID
            // 
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colProjectTypeCode
            // 
            this.colProjectTypeCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProjectTypeCode.AppearanceHeader.Options.UseFont = true;
            this.colProjectTypeCode.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectTypeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectTypeCode.Caption = "Mã kiểu dự án";
            this.colProjectTypeCode.FieldName = "ProjectTypeCode";
            this.colProjectTypeCode.Name = "colProjectTypeCode";
            this.colProjectTypeCode.Visible = true;
            this.colProjectTypeCode.VisibleIndex = 0;
            this.colProjectTypeCode.Width = 127;
            // 
            // colProjectTypeName
            // 
            this.colProjectTypeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProjectTypeName.AppearanceHeader.Options.UseFont = true;
            this.colProjectTypeName.AppearanceHeader.Options.UseForeColor = true;
            this.colProjectTypeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProjectTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProjectTypeName.Caption = "Tên kiểu dự án";
            this.colProjectTypeName.FieldName = "ProjectTypeName";
            this.colProjectTypeName.Name = "colProjectTypeName";
            this.colProjectTypeName.Visible = true;
            this.colProjectTypeName.VisibleIndex = 1;
            this.colProjectTypeName.Width = 322;
            // 
            // colRootFolder
            // 
            this.colRootFolder.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colRootFolder.AppearanceHeader.Options.UseFont = true;
            this.colRootFolder.AppearanceHeader.Options.UseForeColor = true;
            this.colRootFolder.AppearanceHeader.Options.UseTextOptions = true;
            this.colRootFolder.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRootFolder.Caption = "Thư mục";
            this.colRootFolder.FieldName = "RootFolder";
            this.colRootFolder.Name = "colRootFolder";
            this.colRootFolder.Width = 413;
            // 
            // tlFolderName
            // 
            this.tlFolderName.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colFolderName,
            this.colIDFolder,
            this.colParentIDFolder});
            this.tlFolderName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlFolderName.Location = new System.Drawing.Point(0, 0);
            this.tlFolderName.Name = "tlFolderName";
            this.tlFolderName.OptionsBehavior.Editable = false;
            this.tlFolderName.OptionsBehavior.ReadOnly = true;
            this.tlFolderName.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.tlFolderName.Size = new System.Drawing.Size(320, 526);
            this.tlFolderName.TabIndex = 54;
            this.tlFolderName.DoubleClick += new System.EventHandler(this.tlFolderName_DoubleClick);
            this.tlFolderName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tlFolderName_MouseDown);
            // 
            // colFolderName
            // 
            this.colFolderName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colFolderName.AppearanceHeader.Options.UseFont = true;
            this.colFolderName.AppearanceHeader.Options.UseForeColor = true;
            this.colFolderName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFolderName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFolderName.Caption = "Folder name";
            this.colFolderName.FieldName = "FolderName";
            this.colFolderName.Name = "colFolderName";
            this.colFolderName.Visible = true;
            this.colFolderName.VisibleIndex = 0;
            // 
            // colIDFolder
            // 
            this.colIDFolder.Caption = "ID";
            this.colIDFolder.FieldName = "ID";
            this.colIDFolder.Name = "colIDFolder";
            // 
            // colParentIDFolder
            // 
            this.colParentIDFolder.Caption = "ParentID";
            this.colParentIDFolder.FieldName = "ParentID";
            this.colParentIDFolder.Name = "colParentIDFolder";
            // 
            // frmProjectType
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 609);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFilterText);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.mnuMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmProjectType";
            this.Text = " KIỂU DỰ ÁN";
            this.Load += new System.EventHandler(this.frmGroupFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlFolderName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.Button btnFind;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList tlData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectTypeCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProjectTypeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRootFolder;
        private DevExpress.XtraTreeList.TreeList tlFolderName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFolderName;
        private System.Windows.Forms.ToolStripButton btnAddFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEditFolder;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIDFolder;
        private System.Windows.Forms.ToolStripMenuItem aAAAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnDeleteFolder;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentIDFolder;
    }
}