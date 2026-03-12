using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuotationOptionExport : _Forms
    {
        public int ExportType;
        public bool IsExportSum;
        public frmQuotationOptionExport()
        {
            InitializeComponent();
        }

        private void frmQuotationOptionExport_Load(object sender, EventArgs e)
        {
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (cboTemplate.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn phải chọn một loại báo giá cần xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExportType = cboTemplate.SelectedIndex;
            IsExportSum = chkIsExportSum.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}
