
namespace BMS
{
    partial class frmRandomListTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRandomListTest));
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSaveVSClose = new System.Windows.Forms.ToolStripButton();
            this.btnTest = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumberListTest = new System.Windows.Forms.NumericUpDown();
            this.txtNumber = new System.Windows.Forms.NumericUpDown();
            this.cbQuestionType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.NumericUpDown();
            this.ckQuestion = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbExamQuestionGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.grvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTypeQuestion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberl1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberl2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberl3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeQuestionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mnuMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberListTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbQuestionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExamQuestionGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMenu
            // 
            this.mnuMenu.AutoSize = false;
            this.mnuMenu.BackColor = System.Drawing.Color.Transparent;
            this.mnuMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnuMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveVSClose,
            this.btnTest});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(770, 50);
            this.mnuMenu.TabIndex = 202;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSaveVSClose
            // 
            this.btnSaveVSClose.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveVSClose.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveVSClose.Image")));
            this.btnSaveVSClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveVSClose.Name = "btnSaveVSClose";
            this.btnSaveVSClose.Size = new System.Drawing.Size(46, 47);
            this.btnSaveVSClose.Text = "Cất ";
            this.btnSaveVSClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveVSClose.Click += new System.EventHandler(this.btnSaveVSClose_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Image = ((System.Drawing.Image)(resources.GetObject("btnTest.Image")));
            this.btnTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(49, 47);
            this.btnTest.Text = "Test";
            this.btnTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 203;
            this.label1.Text = "Số đề";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 19);
            this.label2.TabIndex = 203;
            this.label2.Text = "Chọn nhóm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 19);
            this.label3.TabIndex = 203;
            this.label3.Text = "Số câu trong 1 đề";
            // 
            // txtNumberListTest
            // 
            this.txtNumberListTest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberListTest.Location = new System.Drawing.Point(167, 164);
            this.txtNumberListTest.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumberListTest.Name = "txtNumberListTest";
            this.txtNumberListTest.Size = new System.Drawing.Size(142, 27);
            this.txtNumberListTest.TabIndex = 204;
            this.txtNumberListTest.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtNumber
            // 
            this.txtNumber.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.Location = new System.Drawing.Point(167, 246);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(142, 27);
            this.txtNumber.TabIndex = 204;
            this.txtNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbQuestionType
            // 
            this.cbQuestionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbQuestionType.Location = new System.Drawing.Point(167, 121);
            this.cbQuestionType.Name = "cbQuestionType";
            this.cbQuestionType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQuestionType.Properties.Appearance.Options.UseFont = true;
            this.cbQuestionType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbQuestionType.Size = new System.Drawing.Size(589, 26);
            this.cbQuestionType.TabIndex = 206;
            this.cbQuestionType.EditValueChanged += new System.EventHandler(this.cbQuestionType_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 19);
            this.label4.TabIndex = 203;
            this.label4.Text = "Thời gian (Phút)";
            // 
            // txtTime
            // 
            this.txtTime.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(167, 204);
            this.txtTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.txtTime.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(142, 27);
            this.txtTime.TabIndex = 204;
            this.txtTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // ckQuestion
            // 
            this.ckQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckQuestion.AutoSize = true;
            this.ckQuestion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckQuestion.Location = new System.Drawing.Point(322, 250);
            this.ckQuestion.Name = "ckQuestion";
            this.ckQuestion.Size = new System.Drawing.Size(134, 23);
            this.ckQuestion.TabIndex = 207;
            this.ckQuestion.Text = "Chi tiết câu hỏi";
            this.ckQuestion.UseVisualStyleBackColor = true;
            this.ckQuestion.CheckedChanged += new System.EventHandler(this.ckQuestion_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 19);
            this.label5.TabIndex = 203;
            this.label5.Text = "Chọn loại câu hỏi";
            // 
            // cbExamQuestionGroup
            // 
            this.cbExamQuestionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExamQuestionGroup.Location = new System.Drawing.Point(167, 71);
            this.cbExamQuestionGroup.Name = "cbExamQuestionGroup";
            this.cbExamQuestionGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExamQuestionGroup.Properties.Appearance.Options.UseFont = true;
            this.cbExamQuestionGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbExamQuestionGroup.Size = new System.Drawing.Size(589, 26);
            this.cbExamQuestionGroup.TabIndex = 206;
            this.cbExamQuestionGroup.EditValueChanged += new System.EventHandler(this.cbExamQuestionGroup_EditValueChanged);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(0, 291);
            this.grdData.MainView = this.grvData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(770, 173);
            this.grdData.TabIndex = 209;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvData});
            this.grdData.Visible = false;
            // 
            // grvData
            // 
            this.grvData.ColumnPanelRowHeight = 45;
            this.grvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTypeQuestion,
            this.colNumberl1,
            this.colNumberl2,
            this.colNumberl3,
            this.colTotal,
            this.colTypeQuestionID});
            this.grvData.GridControl = this.grdData;
            this.grvData.Name = "grvData";
            this.grvData.OptionsView.ShowFooter = true;
            this.grvData.OptionsView.ShowGroupPanel = false;
            this.grvData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvData_CellValueChanged);
            // 
            // colTypeQuestion
            // 
            this.colTypeQuestion.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeQuestion.AppearanceCell.Options.UseFont = true;
            this.colTypeQuestion.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTypeQuestion.AppearanceHeader.Options.UseFont = true;
            this.colTypeQuestion.AppearanceHeader.Options.UseForeColor = true;
            this.colTypeQuestion.AppearanceHeader.Options.UseTextOptions = true;
            this.colTypeQuestion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTypeQuestion.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTypeQuestion.Caption = "Loại câu hỏi";
            this.colTypeQuestion.FieldName = "TypeQuestion";
            this.colTypeQuestion.Name = "colTypeQuestion";
            this.colTypeQuestion.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTypeQuestion.OptionsColumn.ReadOnly = true;
            this.colTypeQuestion.OptionsFilter.AllowAutoFilter = false;
            this.colTypeQuestion.OptionsFilter.AllowFilter = false;
            this.colTypeQuestion.Visible = true;
            this.colTypeQuestion.VisibleIndex = 0;
            // 
            // colNumberl1
            // 
            this.colNumberl1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNumberl1.AppearanceCell.Options.UseFont = true;
            this.colNumberl1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNumberl1.AppearanceHeader.Options.UseFont = true;
            this.colNumberl1.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberl1.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberl1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberl1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberl1.Caption = "Số câu hỏi dễ (1đ)";
            this.colNumberl1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumberl1.FieldName = "Numberl1";
            this.colNumberl1.Name = "colNumberl1";
            this.colNumberl1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNumberl1.OptionsFilter.AllowAutoFilter = false;
            this.colNumberl1.OptionsFilter.AllowFilter = false;
            this.colNumberl1.Visible = true;
            this.colNumberl1.VisibleIndex = 1;
            // 
            // colNumberl2
            // 
            this.colNumberl2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNumberl2.AppearanceCell.Options.UseFont = true;
            this.colNumberl2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNumberl2.AppearanceHeader.Options.UseFont = true;
            this.colNumberl2.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberl2.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberl2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberl2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberl2.Caption = "Số câu hỏi trung bình (2đ)";
            this.colNumberl2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumberl2.FieldName = "Numberl2";
            this.colNumberl2.Name = "colNumberl2";
            this.colNumberl2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNumberl2.OptionsFilter.AllowAutoFilter = false;
            this.colNumberl2.OptionsFilter.AllowFilter = false;
            this.colNumberl2.Visible = true;
            this.colNumberl2.VisibleIndex = 2;
            // 
            // colNumberl3
            // 
            this.colNumberl3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNumberl3.AppearanceCell.Options.UseFont = true;
            this.colNumberl3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colNumberl3.AppearanceHeader.Options.UseFont = true;
            this.colNumberl3.AppearanceHeader.Options.UseForeColor = true;
            this.colNumberl3.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberl3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberl3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colNumberl3.Caption = "Số câu hỏi khó (3đ)";
            this.colNumberl3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNumberl3.FieldName = "Numberl3";
            this.colNumberl3.Name = "colNumberl3";
            this.colNumberl3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNumberl3.OptionsFilter.AllowAutoFilter = false;
            this.colNumberl3.OptionsFilter.AllowFilter = false;
            this.colNumberl3.Visible = true;
            this.colNumberl3.VisibleIndex = 3;
            // 
            // colTotal
            // 
            this.colTotal.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotal.AppearanceCell.Options.UseFont = true;
            this.colTotal.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotal.AppearanceHeader.Options.UseFont = true;
            this.colTotal.AppearanceHeader.Options.UseForeColor = true;
            this.colTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotal.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colTotal.Caption = "Tổng số câu";
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTotal.OptionsColumn.ReadOnly = true;
            this.colTotal.OptionsFilter.AllowAutoFilter = false;
            this.colTotal.OptionsFilter.AllowFilter = false;
            this.colTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "Tổng số câu hỏi={0:0.##}")});
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 4;
            // 
            // colTypeQuestionID
            // 
            this.colTypeQuestionID.Caption = "ID";
            this.colTypeQuestionID.FieldName = "ID";
            this.colTypeQuestionID.Name = "colTypeQuestionID";
            // 
            // frmRandomListTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 464);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.ckQuestion);
            this.Controls.Add(this.cbExamQuestionGroup);
            this.Controls.Add(this.cbQuestionType);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtNumberListTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmRandomListTest";
            this.Text = "CHI TIẾT RANDOM";
            this.Load += new System.EventHandler(this.frmRandomListTest_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberListTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbQuestionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbExamQuestionGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSaveVSClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtNumberListTest;
        private System.Windows.Forms.NumericUpDown txtNumber;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbQuestionType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtTime;
        private System.Windows.Forms.CheckBox ckQuestion;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbExamQuestionGroup;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView grvData;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeQuestion;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberl1;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberl2;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberl3;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeQuestionID;
        private System.Windows.Forms.ToolStripButton btnTest;
    }
}