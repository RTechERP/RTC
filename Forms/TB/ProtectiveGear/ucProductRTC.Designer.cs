
namespace BMS
{
    partial class ucProductRTC
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProductRTC));
            this.lblProductName = new System.Windows.Forms.Label();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblDateBorrow = new System.Windows.Forms.Label();
            this.lblDateExpect = new System.Windows.Forms.Label();
            this.stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGiaHan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDuyenMuon = new System.Windows.Forms.ToolStripMenuItem();
            this.btnApprovedReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnApprovedGiaHan = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWashing = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBackToUse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).BeginInit();
            this.stackPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(9, 13);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(140, 16);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Mã - Tên sản phẩm";
            this.lblProductName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseDown);
            // 
            // btnMove
            // 
            this.btnMove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMove.ImageOptions.SvgImage")));
            this.btnMove.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnMove.Location = new System.Drawing.Point(369, 3);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(22, 22);
            this.btnMove.TabIndex = 1;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            this.btnMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseDown);
            this.btnMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseMove);
            // 
            // lblEmployee
            // 
            this.lblEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(9, 187);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(70, 13);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "Người mượn: ";
            // 
            // lblDateBorrow
            // 
            this.lblDateBorrow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateBorrow.AutoSize = true;
            this.lblDateBorrow.Location = new System.Drawing.Point(6, 44);
            this.lblDateBorrow.Name = "lblDateBorrow";
            this.lblDateBorrow.Size = new System.Drawing.Size(67, 13);
            this.lblDateBorrow.TabIndex = 0;
            this.lblDateBorrow.Text = "Ngày mượn: ";
            // 
            // lblDateExpect
            // 
            this.lblDateExpect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateExpect.AutoSize = true;
            this.lblDateExpect.Location = new System.Drawing.Point(6, 75);
            this.lblDateExpect.Name = "lblDateExpect";
            this.lblDateExpect.Size = new System.Drawing.Size(53, 13);
            this.lblDateExpect.TabIndex = 0;
            this.lblDateExpect.Text = "Ngày trả: ";
            // 
            // stackPanel1
            // 
            this.stackPanel1.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.stackPanel1.Appearance.Options.UseBackColor = true;
            this.stackPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.stackPanel1.Controls.Add(this.menuStrip1);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            this.stackPanel1.Location = new System.Drawing.Point(0, 238);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(270, 42);
            this.stackPanel1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(166, 6);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(104, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBorrow,
            this.btnReturn,
            this.btnGiaHan,
            this.toolStripSeparator3,
            this.btnDuyenMuon,
            this.btnApprovedReturn,
            this.btnApprovedGiaHan,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnWashing,
            this.btnBackToUse,
            this.toolStripSeparator2,
            this.btnDelete});
            this.actionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(96, 25);
            this.actionToolStripMenuItem.Text = "Chức năng";
            this.actionToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btnBorrow
            // 
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(197, 26);
            this.btnBorrow.Text = "Mượn";
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(197, 26);
            this.btnReturn.Text = "Trả";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(197, 26);
            this.btnGiaHan.Text = "Gia hạn";
            this.btnGiaHan.Click += new System.EventHandler(this.btnGiaHan_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(194, 6);
            // 
            // btnDuyenMuon
            // 
            this.btnDuyenMuon.Name = "btnDuyenMuon";
            this.btnDuyenMuon.Size = new System.Drawing.Size(197, 26);
            this.btnDuyenMuon.Text = "Duyệt mượn";
            this.btnDuyenMuon.Click += new System.EventHandler(this.btnDuyenMuon_Click);
            // 
            // btnApprovedReturn
            // 
            this.btnApprovedReturn.Name = "btnApprovedReturn";
            this.btnApprovedReturn.Size = new System.Drawing.Size(197, 26);
            this.btnApprovedReturn.Text = "Duyệt trả";
            this.btnApprovedReturn.Click += new System.EventHandler(this.btnApprovedReturn_Click);
            // 
            // btnApprovedGiaHan
            // 
            this.btnApprovedGiaHan.Name = "btnApprovedGiaHan";
            this.btnApprovedGiaHan.Size = new System.Drawing.Size(197, 26);
            this.btnApprovedGiaHan.Text = "Duyệt gia hạn";
            this.btnApprovedGiaHan.Click += new System.EventHandler(this.btnApprovedGiaHan_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(197, 26);
            this.btnEdit.Text = "Sửa người mượn";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // btnWashing
            // 
            this.btnWashing.Name = "btnWashing";
            this.btnWashing.Size = new System.Drawing.Size(197, 26);
            this.btnWashing.Text = "Đang giặt";
            this.btnWashing.Click += new System.EventHandler(this.btnWashing_Click);
            // 
            // btnBackToUse
            // 
            this.btnBackToUse.Name = "btnBackToUse";
            this.btnBackToUse.Size = new System.Drawing.Size(197, 26);
            this.btnBackToUse.Text = "Đưa vào sử dụng";
            this.btnBackToUse.Click += new System.EventHandler(this.btnBackToUse_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // btnDelete
            // 
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(197, 26);
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(85)))), ((int)(((byte)(21)))));
            this.lblStatusText.ForeColor = System.Drawing.Color.White;
            this.lblStatusText.Location = new System.Drawing.Point(9, 209);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Padding = new System.Windows.Forms.Padding(3);
            this.lblStatusText.Size = new System.Drawing.Size(61, 19);
            this.lblStatusText.TabIndex = 0;
            this.lblStatusText.Text = "Trạng thái";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatusText.Click += new System.EventHandler(this.lblStatusText_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(6, 13);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(29, 13);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Vị trí";
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.lblProductName);
            this.pnHeader.Controls.Add(this.btnMove);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(270, 73);
            this.pnHeader.TabIndex = 3;
            this.pnHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseDown);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem2)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barEditItem2
            // 
            this.barEditItem2.AutoFillWidth = true;
            this.barEditItem2.Edit = this.repositoryItemMemoEdit1;
            this.barEditItem2.EditHeight = 100;
            this.barEditItem2.Id = 2;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            this.repositoryItemMemoEdit1.ReadOnly = true;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barEditItem2});
            this.barManager1.MaxItemId = 3;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemMemoEdit1});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(270, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 280);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(270, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 280);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(270, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 280);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.ReadOnly = true;
            // 
            // pbImage
            // 
            this.pbImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbImage.Location = new System.Drawing.Point(160, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(104, 105);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 296;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.pbImage);
            this.panel3.Location = new System.Drawing.Point(3, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(264, 105);
            this.panel3.TabIndex = 307;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblDateExpect);
            this.panel4.Controls.Add(this.lblLocation);
            this.panel4.Controls.Add(this.lblDateBorrow);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(160, 105);
            this.panel4.TabIndex = 307;
            // 
            // ucProductRTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblStatusText);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.pnHeader);
            this.Controls.Add(this.stackPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucProductRTC";
            this.Size = new System.Drawing.Size(270, 280);
            this.Load += new System.EventHandler(this.ucProductRTC_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ucProductRTC_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ucProductRTC_MouseUp);
            this.Move += new System.EventHandler(this.ucProductRTC_Move);
            ((System.ComponentModel.ISupportInitialize)(this.stackPanel1)).EndInit();
            this.stackPanel1.ResumeLayout(false);
            this.stackPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnHeader.ResumeLayout(false);
            this.pnHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblProductName;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        public System.Windows.Forms.Label lblEmployee;
        public System.Windows.Forms.Label lblDateBorrow;
        public System.Windows.Forms.Label lblDateExpect;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        public System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        public System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.ToolStripMenuItem btnBorrow;
        private System.Windows.Forms.ToolStripMenuItem btnReturn;
        private System.Windows.Forms.ToolStripMenuItem btnGiaHan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnDuyenMuon;
        private System.Windows.Forms.ToolStripMenuItem btnApprovedReturn;
        private System.Windows.Forms.ToolStripMenuItem btnApprovedGiaHan;
        private System.Windows.Forms.ToolStripMenuItem btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnWashing;
        private System.Windows.Forms.ToolStripMenuItem btnBackToUse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        public System.Windows.Forms.Panel pnHeader;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        public System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}
