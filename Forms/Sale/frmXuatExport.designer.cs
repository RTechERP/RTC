using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    partial class frmXuatExport
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
            this.chkIsExportSum = new System.Windows.Forms.CheckBox();
            this.cboTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkIsExportSum
            // 
            this.chkIsExportSum.AutoSize = true;
            this.chkIsExportSum.Location = new System.Drawing.Point(111, 52);
            this.chkIsExportSum.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsExportSum.Name = "chkIsExportSum";
            this.chkIsExportSum.Size = new System.Drawing.Size(99, 21);
            this.chkIsExportSum.TabIndex = 0;
            this.chkIsExportSum.Text = "Có bộ tổng";
            this.chkIsExportSum.UseVisualStyleBackColor = true;
            // 
            // cboTemplate
            // 
            this.cboTemplate.FormattingEnabled = true;
            this.cboTemplate.Items.AddRange(new object[] {
            "Phiếu Xuất Kho",
            "Phiếu Trả Mẫu",
            "Biên Bản Trả Mẫu"});
            this.cboTemplate.Location = new System.Drawing.Point(111, 15);
            this.cboTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.cboTemplate.Name = "cboTemplate";
            this.cboTemplate.Size = new System.Drawing.Size(247, 24);
            this.cboTemplate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Chọn biểu mẫu";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(267, 85);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 28);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Xuất phiếu";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmXuatExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 114);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTemplate);
            this.Controls.Add(this.chkIsExportSum);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmXuatExport";
            this.Text = "TÙY CHỌN XUẤT BÁO GIÁ";
            this.Load += new System.EventHandler(this.frmQuotationOptionExport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkIsExportSum;
        private System.Windows.Forms.ComboBox cboTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
    }
}