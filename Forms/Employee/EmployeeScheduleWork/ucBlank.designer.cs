
namespace BMS
{
    partial class ucBlank
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
            this.lbDayDisabled = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbDayDisabled
            // 
            this.lbDayDisabled.AutoSize = true;
            this.lbDayDisabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDayDisabled.Location = new System.Drawing.Point(3, 0);
            this.lbDayDisabled.Name = "lbDayDisabled";
            this.lbDayDisabled.Size = new System.Drawing.Size(27, 20);
            this.lbDayDisabled.TabIndex = 1;
            this.lbDayDisabled.Text = "00";
            // 
            // ucBlank
            // 
            this.Appearance.BackColor = System.Drawing.Color.Silver;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbDayDisabled);
            this.Name = "ucBlank";
            this.Size = new System.Drawing.Size(120, 100);
            this.Load += new System.EventHandler(this.ucBlank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDayDisabled;
    }
}
