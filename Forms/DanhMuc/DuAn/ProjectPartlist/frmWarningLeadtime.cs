using BMS;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.DanhMuc.DuAn.ProjectPartlist
{
    public partial class frmWarningLeadtime : _Forms
    {
        public string title;
        public DataTable DataTable;
        public frmWarningLeadtime()
        {
            InitializeComponent();
            //label1.Text = title;
        }

        private void frmWarningLeadtime_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            grdData.DataSource = DataTable;
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmWarningLeadtime_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}