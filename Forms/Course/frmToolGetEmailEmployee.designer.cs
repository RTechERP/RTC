
namespace BMS
{
    partial class frmToolGetEmailEmployee
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
            this.GetEmailEmployee = new System.Windows.Forms.Button();
            this.cbxDepartment = new System.Windows.Forms.ComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.rtbValueEmail = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // GetEmailEmployee
            // 
            this.GetEmailEmployee.Location = new System.Drawing.Point(250, 40);
            this.GetEmailEmployee.Name = "GetEmailEmployee";
            this.GetEmailEmployee.Size = new System.Drawing.Size(108, 21);
            this.GetEmailEmployee.TabIndex = 0;
            this.GetEmailEmployee.Text = "Lấy ra";
            this.GetEmailEmployee.UseVisualStyleBackColor = true;
            this.GetEmailEmployee.Click += new System.EventHandler(this.GetEmailEmployee_Click);
            // 
            // cbxDepartment
            // 
            this.cbxDepartment.FormattingEnabled = true;
            this.cbxDepartment.Location = new System.Drawing.Point(123, 40);
            this.cbxDepartment.Name = "cbxDepartment";
            this.cbxDepartment.Size = new System.Drawing.Size(121, 21);
            this.cbxDepartment.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(79, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Chọn phòng ban";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(27, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Kết quả";
            // 
            // rtbValueEmail
            // 
            this.rtbValueEmail.Location = new System.Drawing.Point(123, 93);
            this.rtbValueEmail.Name = "rtbValueEmail";
            this.rtbValueEmail.Size = new System.Drawing.Size(629, 345);
            this.rtbValueEmail.TabIndex = 4;
            this.rtbValueEmail.Text = "";
            // 
            // frmToolGetEmailEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbValueEmail);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cbxDepartment);
            this.Controls.Add(this.GetEmailEmployee);
            this.Name = "frmToolGetEmailEmployee";
            this.Text = "Lấy email nhân viên";
            this.Load += new System.EventHandler(this.frmToolGetEmailEmployee_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetEmailEmployee;
        private System.Windows.Forms.ComboBox cbxDepartment;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.RichTextBox rtbValueEmail;
    }
}