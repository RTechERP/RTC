namespace Forms.Sale.RequestInvoice
{
    partial class frmAmendReasonRequestInvoice
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
            this.mnuMenu = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCloseNew = new System.Windows.Forms.ToolStripButton();
            this.txtAmendReason = new System.Windows.Forms.RichTextBox();
            this.mnuMenu.SuspendLayout();
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
            this.toolStripSeparator1,
            this.btnCloseNew});
            this.mnuMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mnuMenu.Size = new System.Drawing.Size(610, 55);
            this.mnuMenu.TabIndex = 4;
            this.mnuMenu.Text = "toolStrip2";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 52);
            this.btnSave.Tag = "frmProductRTC_SaleCutomer";
            this.btnSave.Text = "Cất && Đóng";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // btnCloseNew
            // 
            this.btnCloseNew.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseNew.Image = global::Forms.Properties.Resources.cancel_32x321;
            this.btnCloseNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCloseNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseNew.Name = "btnCloseNew";
            this.btnCloseNew.Size = new System.Drawing.Size(45, 52);
            this.btnCloseNew.Tag = "frmProductRTC_SaleCutomer";
            this.btnCloseNew.Text = "Đóng";
            this.btnCloseNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCloseNew.Click += new System.EventHandler(this.btnCloseNew_Click);
            // 
            // txtAmendReason
            // 
            this.txtAmendReason.Location = new System.Drawing.Point(13, 59);
            this.txtAmendReason.MaxLength = 550;
            this.txtAmendReason.Name = "txtAmendReason";
            this.txtAmendReason.Size = new System.Drawing.Size(586, 210);
            this.txtAmendReason.TabIndex = 5;
            this.txtAmendReason.Text = "";
            // 
            // frmAmendReasonRequestInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 281);
            this.Controls.Add(this.txtAmendReason);
            this.Controls.Add(this.mnuMenu);
            this.Name = "frmAmendReasonRequestInvoice";
            this.Text = "Lý do yêu cầu bổ sung";
            this.Load += new System.EventHandler(this.frmAmendReasonRequestInvoice_Load);
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnuMenu;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCloseNew;
        private System.Windows.Forms.RichTextBox txtAmendReason;
    }
}