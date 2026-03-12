using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPartlistImportExcelDiff: _Forms
    {
        public DataTable dataDiff = new DataTable();
        public frmPartlistImportExcelDiff()
        {
            InitializeComponent();
        }

        private void frmPartlistImportExcelDiff_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dataDiff;
        }

        private void bandedGridView1_RowCellStyle_1(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            if (e.Column == colGroupMaterial || e.Column == colGroupMaterialStock)
            {
                string partlistName = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colGroupMaterial));
                string stockName = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colGroupMaterialStock));
                if (!string.Equals(partlistName, stockName, StringComparison.OrdinalIgnoreCase))
                {
                    e.Appearance.BackColor = System.Drawing.Color.Pink;
                }
            }

            if (e.Column == colManufacturer || e.Column == colManufacturerStock)
            {
                string partlistMaker = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colManufacturer));
                string stockMaker = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colManufacturerStock));
                if (!string.Equals(partlistMaker, stockMaker, StringComparison.OrdinalIgnoreCase))
                {
                    e.Appearance.BackColor = System.Drawing.Color.Pink;
                }
            }

            // Check Unit
            if (e.Column == colUnit || e.Column == colUnitStock)
            {
                string partlistUnit = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colUnit));
                string stockUnit = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colUnitStock));
                if (!string.Equals(partlistUnit, stockUnit, StringComparison.OrdinalIgnoreCase))
                {
                    e.Appearance.BackColor = System.Drawing.Color.Pink;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
