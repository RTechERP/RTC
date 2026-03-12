
namespace BMS
{
    partial class frmAddLeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddLeader));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cbStaff = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cbStaff.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(284, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(41, 33);
            this.simpleButton1.TabIndex = 76;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cbStaff
            // 
            this.cbStaff.Location = new System.Drawing.Point(112, 20);
            this.cbStaff.Name = "cbStaff";
            this.cbStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbStaff.Size = new System.Drawing.Size(166, 20);
            this.cbStaff.TabIndex = 77;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 75;
            this.label2.Text = "Tên nhân viên";
            // 
            // frmAddLeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 65);
            this.Controls.Add(this.cbStaff);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label2);
            this.Name = "frmAddLeader";
            this.Text = "Thêm Nhân viên";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddGroupSalesUser_FormClosing);
            this.Load += new System.EventHandler(this.frmAddGroupSalesUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbStaff.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cbStaff;
        private System.Windows.Forms.Label label2;
    }
}