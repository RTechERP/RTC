
namespace BMS
{
    partial class frmBillDocumentExportTechnical
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveNew = new System.Windows.Forms.ToolStripButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grdMaster = new DevExpress.XtraGrid.GridControl();
            this.grvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboStatus = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.grvCboStatus = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCboID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcboStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLogDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colBillImportTechnicalID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cbProject = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.btnAddSerialNumber = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdDataLog = new DevExpress.XtraGrid.GridControl();
            this.grvDataLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLogID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCodeLog = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameLog = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLogDateHistory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoteDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colStatusHistory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCboStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSerialNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator2,
            this.btnSaveNew});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(1321, 55);
            this.mnuMenu.TabIndex = 5;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 52);
            this.btnSave.Tag = "frmBillDocumentImport_Save";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Forms.Properties.Resources.Save_32x322;
            this.btnSaveNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(38, 52);
            this.btnSaveNew.Tag = "frmBillDocumentImport_Save";
            this.btnSaveNew.Text = " Cất";
            this.btnSaveNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 55);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grdMaster);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1321, 547);
            this.splitContainerControl1.SplitterPosition = 257;
            this.splitContainerControl1.TabIndex = 151;
            // 
            // grdMaster
            // 
            this.grdMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMaster.Location = new System.Drawing.Point(0, 0);
            this.grdMaster.MainView = this.grvMaster;
            this.grdMaster.Name = "grdMaster";
            this.grdMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cbProject,
            this.repositoryItemMemoEdit3,
            this.btnAddSerialNumber,
            this.cboStatus,
            this.repositoryItemMemoEdit1});
            this.grdMaster.Size = new System.Drawing.Size(1321, 257);
            this.grdMaster.TabIndex = 10;
            this.grdMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvMaster});
            // 
            // grvMaster
            // 
            this.grvMaster.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.Row.Options.UseForeColor = true;
            this.grvMaster.Appearance.Row.Options.UseTextOptions = true;
            this.grvMaster.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvMaster.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvMaster.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grvMaster.Appearance.SelectedRow.Options.UseBackColor = true;
            this.grvMaster.AutoFillColumn = this.colName;
            this.grvMaster.ColumnPanelRowHeight = 40;
            this.grvMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colStatus,
            this.colCode,
            this.colName,
            this.colLogDate,
            this.colNote,
            this.colBillImportTechnicalID});
            this.grvMaster.DetailHeight = 284;
            this.grvMaster.GridControl = this.grdMaster;
            this.grvMaster.Name = "grvMaster";
            this.grvMaster.OptionsBehavior.AutoExpandAllGroups = true;
            this.grvMaster.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            this.grvMaster.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvMaster.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.grvMaster.OptionsCustomization.AllowRowSizing = true;
            this.grvMaster.OptionsSelection.MultiSelect = true;
            this.grvMaster.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvMaster.OptionsView.RowAutoHeight = true;
            this.grvMaster.OptionsView.ShowGroupPanel = false;
            this.grvMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grvMaster_FocusedRowChanged);
            // 
            // colName
            // 
            this.colName.AppearanceCell.Options.UseTextOptions = true;
            this.colName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.AppearanceHeader.Options.UseForeColor = true;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
            this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colName.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colName.Caption = "Tên chứng từ";
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 15;
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 2;
            this.colName.Width = 330;
            // 
            // colID
            // 
            this.colID.AppearanceCell.Options.UseTextOptions = true;
            this.colID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.colID.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colID.AppearanceHeader.Options.UseFont = true;
            this.colID.AppearanceHeader.Options.UseForeColor = true;
            this.colID.AppearanceHeader.Options.UseTextOptions = true;
            this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.MinWidth = 15;
            this.colID.Name = "colID";
            this.colID.Width = 56;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colStatus.AppearanceCell.Options.UseFont = true;
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colStatus.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.colStatus.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseForeColor = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatus.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.ColumnEdit = this.cboStatus;
            this.colStatus.FieldName = "Status";
            this.colStatus.MinWidth = 15;
            this.colStatus.Name = "colStatus";
            this.colStatus.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 0;
            this.colStatus.Width = 181;
            // 
            // cboStatus
            // 
            this.cboStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboStatus.Appearance.Options.UseFont = true;
            this.cboStatus.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboStatus.AppearanceDisabled.Options.UseFont = true;
            this.cboStatus.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboStatus.AppearanceDropDown.Options.UseFont = true;
            this.cboStatus.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboStatus.AppearanceFocused.Options.UseFont = true;
            this.cboStatus.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboStatus.AppearanceReadOnly.Options.UseFont = true;
            this.cboStatus.AutoHeight = false;
            serializableAppearanceObject4.Font = new System.Drawing.Font("Tahoma", 9F);
            serializableAppearanceObject4.Options.UseFont = true;
            this.cboStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.cboStatus.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.NullText = "";
            this.cboStatus.PopupView = this.grvCboStatus;
            // 
            // grvCboStatus
            // 
            this.grvCboStatus.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCboID,
            this.colcboStatus});
            this.grvCboStatus.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grvCboStatus.Name = "grvCboStatus";
            this.grvCboStatus.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grvCboStatus.OptionsView.ShowGroupPanel = false;
            // 
            // colCboID
            // 
            this.colCboID.Caption = "ID";
            this.colCboID.FieldName = "ID";
            this.colCboID.Name = "colCboID";
            // 
            // colcboStatus
            // 
            this.colcboStatus.Caption = "Trạng thái";
            this.colcboStatus.FieldName = "Name";
            this.colcboStatus.Name = "colcboStatus";
            this.colcboStatus.Visible = true;
            this.colcboStatus.VisibleIndex = 0;
            // 
            // colCode
            // 
            this.colCode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colCode.AppearanceCell.Options.UseFont = true;
            this.colCode.AppearanceCell.Options.UseTextOptions = true;
            this.colCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.colCode.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colCode.AppearanceHeader.Options.UseFont = true;
            this.colCode.AppearanceHeader.Options.UseForeColor = true;
            this.colCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCode.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCode.Caption = "Mã chứng từ";
            this.colCode.FieldName = "Code";
            this.colCode.MinWidth = 15;
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 153;
            // 
            // colLogDate
            // 
            this.colLogDate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colLogDate.AppearanceCell.Options.UseFont = true;
            this.colLogDate.AppearanceCell.Options.UseTextOptions = true;
            this.colLogDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLogDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLogDate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLogDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.colLogDate.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.colLogDate.AppearanceHeader.Options.UseFont = true;
            this.colLogDate.AppearanceHeader.Options.UseForeColor = true;
            this.colLogDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colLogDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLogDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLogDate.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLogDate.Caption = "Ngày thay đổi";
            this.colLogDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colLogDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLogDate.FieldName = "LogDate";
            this.colLogDate.MinWidth = 15;
            this.colLogDate.Name = "colLogDate";
            this.colLogDate.OptionsColumn.AllowEdit = false;
            this.colLogDate.Visible = true;
            this.colLogDate.VisibleIndex = 4;
            this.colLogDate.Width = 191;
            // 
            // colNote
            // 
            this.colNote.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colNote.AppearanceCell.Options.UseFont = true;
            this.colNote.AppearanceCell.Options.UseForeColor = true;
            this.colNote.AppearanceCell.Options.UseTextOptions = true;
            this.colNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colNote.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNote.AppearanceHeader.Options.UseFont = true;
            this.colNote.AppearanceHeader.Options.UseForeColor = true;
            this.colNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNote.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNote.Caption = "Lý do / Ghi chú";
            this.colNote.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNote.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
            this.colNote.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 3;
            this.colNote.Width = 671;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colBillImportTechnicalID
            // 
            this.colBillImportTechnicalID.Caption = "BillImportTechnicalID";
            this.colBillImportTechnicalID.FieldName = "BillExportTechnicalID";
            this.colBillImportTechnicalID.Name = "colBillImportTechnicalID";
            // 
            // cbProject
            // 
            this.cbProject.AutoHeight = false;
            this.cbProject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbProject.Name = "cbProject";
            this.cbProject.NullText = "";
            this.cbProject.PopupView = this.gridView5;
            // 
            // gridView5
            // 
            this.gridView5.ColumnPanelRowHeight = 40;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ID";
            this.gridColumn7.FieldName = "ID";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Mã dự án";
            this.gridColumn8.FieldName = "ProjectCode";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 241;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "Tên dự án";
            this.gridColumn9.FieldName = "ProjectName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            this.gridColumn9.Width = 462;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // btnAddSerialNumber
            // 
            this.btnAddSerialNumber.AutoHeight = false;
            this.btnAddSerialNumber.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.btnAddSerialNumber.Name = "btnAddSerialNumber";
            this.btnAddSerialNumber.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdDataLog);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1321, 280);
            this.groupControl1.TabIndex = 151;
            this.groupControl1.Text = "LỊCH SỬ";
            // 
            // grdDataLog
            // 
            this.grdDataLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDataLog.Location = new System.Drawing.Point(2, 23);
            this.grdDataLog.MainView = this.grvDataLog;
            this.grdDataLog.Name = "grdDataLog";
            this.grdDataLog.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4});
            this.grdDataLog.Size = new System.Drawing.Size(1317, 255);
            this.grdDataLog.TabIndex = 8;
            this.grdDataLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDataLog});
            // 
            // grvDataLog
            // 
            this.grvDataLog.Appearance.FocusedRow.Options.UseTextOptions = true;
            this.grvDataLog.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDataLog.Appearance.GroupRow.Options.UseTextOptions = true;
            this.grvDataLog.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDataLog.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.grvDataLog.Appearance.HeaderPanel.Options.UseFont = true;
            this.grvDataLog.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.grvDataLog.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.grvDataLog.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grvDataLog.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDataLog.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDataLog.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.grvDataLog.Appearance.Row.Options.UseFont = true;
            this.grvDataLog.Appearance.Row.Options.UseTextOptions = true;
            this.grvDataLog.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.grvDataLog.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.grvDataLog.ColumnPanelRowHeight = 40;
            this.grvDataLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLogID,
            this.colCodeLog,
            this.colNameLog,
            this.colLogDateHistory,
            this.colNoteDetail,
            this.colStatusHistory});
            this.grvDataLog.GridControl = this.grdDataLog;
            this.grvDataLog.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", this.colLogID, "")});
            this.grvDataLog.Name = "grvDataLog";
            this.grvDataLog.OptionsBehavior.Editable = false;
            this.grvDataLog.OptionsBehavior.ReadOnly = true;
            this.grvDataLog.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.grvDataLog.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True;
            this.grvDataLog.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            this.grvDataLog.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.grvDataLog.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Update;
            this.grvDataLog.OptionsCustomization.AllowRowSizing = true;
            this.grvDataLog.OptionsSelection.MultiSelect = true;
            this.grvDataLog.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.grvDataLog.OptionsView.RowAutoHeight = true;
            this.grvDataLog.OptionsView.ShowFooter = true;
            this.grvDataLog.OptionsView.ShowGroupPanel = false;
            this.grvDataLog.PaintStyleName = "(Default)";
            // 
            // colLogID
            // 
            this.colLogID.Caption = "ID";
            this.colLogID.FieldName = "ID";
            this.colLogID.Name = "colLogID";
            // 
            // colCodeLog
            // 
            this.colCodeLog.AppearanceCell.Options.UseTextOptions = true;
            this.colCodeLog.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCodeLog.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCodeLog.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colCodeLog.Caption = "Mã chứng từ";
            this.colCodeLog.FieldName = "Code";
            this.colCodeLog.Name = "colCodeLog";
            this.colCodeLog.Visible = true;
            this.colCodeLog.VisibleIndex = 1;
            this.colCodeLog.Width = 137;
            // 
            // colNameLog
            // 
            this.colNameLog.Caption = "Tên chứng từ";
            this.colNameLog.FieldName = "Name";
            this.colNameLog.Name = "colNameLog";
            this.colNameLog.Visible = true;
            this.colNameLog.VisibleIndex = 2;
            this.colNameLog.Width = 335;
            // 
            // colLogDateHistory
            // 
            this.colLogDateHistory.AppearanceCell.Options.UseForeColor = true;
            this.colLogDateHistory.AppearanceCell.Options.UseTextOptions = true;
            this.colLogDateHistory.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLogDateHistory.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colLogDateHistory.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colLogDateHistory.Caption = "Ngày thay đổi";
            this.colLogDateHistory.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.colLogDateHistory.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLogDateHistory.FieldName = "LogDate";
            this.colLogDateHistory.Name = "colLogDateHistory";
            this.colLogDateHistory.Visible = true;
            this.colLogDateHistory.VisibleIndex = 4;
            this.colLogDateHistory.Width = 258;
            // 
            // colNoteDetail
            // 
            this.colNoteDetail.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F);
            this.colNoteDetail.AppearanceCell.Options.UseForeColor = true;
            this.colNoteDetail.AppearanceCell.Options.UseTextOptions = true;
            this.colNoteDetail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colNoteDetail.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colNoteDetail.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteDetail.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.colNoteDetail.AppearanceHeader.Options.UseForeColor = true;
            this.colNoteDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colNoteDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNoteDetail.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNoteDetail.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNoteDetail.Caption = "Lý do / Ghi chú";
            this.colNoteDetail.ColumnEdit = this.repositoryItemMemoEdit4;
            this.colNoteDetail.FieldName = "Note";
            this.colNoteDetail.Name = "colNoteDetail";
            this.colNoteDetail.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.colNoteDetail.Visible = true;
            this.colNoteDetail.VisibleIndex = 3;
            this.colNoteDetail.Width = 640;
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // colStatusHistory
            // 
            this.colStatusHistory.AppearanceCell.Options.UseForeColor = true;
            this.colStatusHistory.AppearanceCell.Options.UseTextOptions = true;
            this.colStatusHistory.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colStatusHistory.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusHistory.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusHistory.AppearanceHeader.Options.UseForeColor = true;
            this.colStatusHistory.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusHistory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusHistory.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colStatusHistory.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colStatusHistory.Caption = "Trạng thái";
            this.colStatusHistory.FieldName = "StatusText";
            this.colStatusHistory.Name = "colStatusHistory";
            this.colStatusHistory.Visible = true;
            this.colStatusHistory.VisibleIndex = 0;
            this.colStatusHistory.Width = 164;
            // 
            // frmBillDocumentExportTechnical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 602);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmBillDocumentExportTechnical";
            this.Text = "CHI TIẾT HỒ SƠ CHỨNG TỪ PHIẾU XUẤT DEMO";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBillDocumentExportTechnical_FormClosed);
            this.Load += new System.EventHandler(this.frmBillDocumentExportTechnical_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvCboStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSerialNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSaveNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl grdMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMaster;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCboStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCboID;
        private DevExpress.XtraGrid.Columns.GridColumn colcboStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colLogDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colBillImportTechnicalID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnAddSerialNumber;
        private DevExpress.XtraGrid.GridControl grdDataLog;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDataLog;
        private DevExpress.XtraGrid.Columns.GridColumn colLogID;
        private DevExpress.XtraGrid.Columns.GridColumn colCodeLog;
        private DevExpress.XtraGrid.Columns.GridColumn colNameLog;
        private DevExpress.XtraGrid.Columns.GridColumn colLogDateHistory;
        private DevExpress.XtraGrid.Columns.GridColumn colNoteDetail;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusHistory;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}