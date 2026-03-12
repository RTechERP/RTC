namespace BMS
{
    partial class frmProductQRCode
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
            this.btnIMG = new System.Windows.Forms.Button();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIMG
            // 
            this.btnIMG.Location = new System.Drawing.Point(12, 13);
            this.btnIMG.Name = "btnIMG";
            this.btnIMG.Size = new System.Drawing.Size(94, 30);
            this.btnIMG.TabIndex = 3;
            this.btnIMG.Text = "PRINT";
            this.btnIMG.UseVisualStyleBackColor = true;
            this.btnIMG.Click += new System.EventHandler(this.btnIMG_Click);
            // 
            // picQR
            // 
            this.picQR.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picQR.Location = new System.Drawing.Point(12, 49);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(35, 35);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQR.TabIndex = 4;
            this.picQR.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(112, 13);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(551, 231);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // frmProductQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 256);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.picQR);
            this.Controls.Add(this.btnIMG);
            this.Name = "frmProductQRCode";
            this.Text = "XUẤT QR-CODE";
            this.Load += new System.EventHandler(this.frmProductQRCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnIMG;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}