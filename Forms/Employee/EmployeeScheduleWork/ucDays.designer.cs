
namespace BMS
{
    partial class ucDays
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
            this.lbDay = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.chkCheckSalary = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.Location = new System.Drawing.Point(3, 0);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(27, 20);
            this.lbDay.TabIndex = 0;
            this.lbDay.Text = "00";
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.Location = new System.Drawing.Point(3, 52);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(61, 25);
            this.lbText.TabIndex = 1;
            this.lbText.Text = "value";
            // 
            // chkCheckSalary
            // 
            this.chkCheckSalary.AutoSize = true;
            this.chkCheckSalary.Location = new System.Drawing.Point(3, 80);
            this.chkCheckSalary.Name = "chkCheckSalary";
            this.chkCheckSalary.Size = new System.Drawing.Size(103, 17);
            this.chkCheckSalary.TabIndex = 2;
            this.chkCheckSalary.Text = "Có hưởng lương";
            this.chkCheckSalary.UseVisualStyleBackColor = true;
            this.chkCheckSalary.CheckedChanged += new System.EventHandler(this.chkCheckSalary_CheckedChanged);
            // 
            // ucDays
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.chkCheckSalary);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.lbDay);
            this.Name = "ucDays";
            this.Size = new System.Drawing.Size(120, 100);
            this.Load += new System.EventHandler(this.ucDays_Load);
            this.Click += new System.EventHandler(this.ucDays_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.CheckBox chkCheckSalary;
    }
}
