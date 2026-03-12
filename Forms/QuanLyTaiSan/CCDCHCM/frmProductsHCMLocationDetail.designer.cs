
namespace BMS
{
	partial class frmProductsHCMLocationDetail
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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnAddAndClose = new System.Windows.Forms.ToolStripButton();
			this.btnAddAndNew = new System.Windows.Forms.ToolStripButton();
			this.txtSTT = new System.Windows.Forms.NumericUpDown();
			this.txtlocationName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLocationCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSTT)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddAndClose,
            this.btnAddAndNew});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(441, 58);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnAddAndClose
			// 
			this.btnAddAndClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
			this.btnAddAndClose.Image = global::Forms.Properties.Resources.SaveAndClose_32x32;
			this.btnAddAndClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnAddAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAddAndClose.Name = "btnAddAndClose";
			this.btnAddAndClose.Size = new System.Drawing.Size(91, 55);
			this.btnAddAndClose.Text = "Cất && Đóng";
			this.btnAddAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnAddAndClose.Click += new System.EventHandler(this.btnAddAndClose_Click);
			// 
			// btnAddAndNew
			// 
			this.btnAddAndNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
			this.btnAddAndNew.Image = global::Forms.Properties.Resources.Save_32x322;
			this.btnAddAndNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnAddAndNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAddAndNew.Name = "btnAddAndNew";
			this.btnAddAndNew.Size = new System.Drawing.Size(122, 55);
			this.btnAddAndNew.Text = "Cất && Thêm mới";
			this.btnAddAndNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnAddAndNew.Click += new System.EventHandler(this.btnAddAndNew_Click);
			// 
			// txtSTT
			// 
			this.txtSTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSTT.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSTT.Location = new System.Drawing.Point(352, 74);
			this.txtSTT.Name = "txtSTT";
			this.txtSTT.Size = new System.Drawing.Size(71, 27);
			this.txtSTT.TabIndex = 11;
			this.txtSTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtlocationName
			// 
			this.txtlocationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtlocationName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtlocationName.Location = new System.Drawing.Point(100, 109);
			this.txtlocationName.Margin = new System.Windows.Forms.Padding(4);
			this.txtlocationName.Name = "txtlocationName";
			this.txtlocationName.Size = new System.Drawing.Size(323, 27);
			this.txtlocationName.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(20, 113);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 19);
			this.label2.TabIndex = 9;
			this.label2.Text = "Tên vị trí";
			// 
			// txtLocationCode
			// 
			this.txtLocationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLocationCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLocationCode.Location = new System.Drawing.Point(100, 74);
			this.txtLocationCode.Margin = new System.Windows.Forms.Padding(4);
			this.txtLocationCode.Name = "txtLocationCode";
			this.txtLocationCode.Size = new System.Drawing.Size(199, 27);
			this.txtLocationCode.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(307, 78);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 19);
			this.label3.TabIndex = 6;
			this.label3.Text = "STT";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(20, 78);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 19);
			this.label1.TabIndex = 7;
			this.label1.Text = "Mã vị trí";
			// 
			// frmProductsHCMLocationDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(441, 159);
			this.Controls.Add(this.txtSTT);
			this.Controls.Add(this.txtlocationName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtLocationCode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "frmProductsHCMLocationDetail";
			this.Text = "CHI TIẾT VỊ TRÍ VẬT TƯ";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductsHCMLocationDetail_FormClosed);
			this.Load += new System.EventHandler(this.frmProductsHCMLocationDetail_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSTT)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnAddAndClose;
		private System.Windows.Forms.ToolStripButton btnAddAndNew;
		private System.Windows.Forms.NumericUpDown txtSTT;
		private System.Windows.Forms.TextBox txtlocationName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtLocationCode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
	}
}