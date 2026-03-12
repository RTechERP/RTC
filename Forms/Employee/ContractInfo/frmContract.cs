using BMS.Business;
using BMS.Model;
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
    public partial class frmContract : _Forms
    {
        public frmContract()
        {
            InitializeComponent();
        }

        private void frmContract_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            DataTable dt = TextUtils.Select("select * from EmployeeLoaiHDLD");
            grdContract.DataSource = dt;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmContractDetail frm = new frmContractDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvContract.GetFocusedRowCellValue(colID));
            if (id == 0) return;

            // _rownIndex = grvData.FocusedRowHandle;

            EmployeeLoaiHDLDModel model = (EmployeeLoaiHDLDModel)EmployeeLoaiHDLDBO.Instance.FindByPK(id);

            frmContractDetail frm = new frmContractDetail();
            frm.Model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvContract.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            string name = grvContract.GetFocusedRowCellValue(colCodeHD).ToString();
            DialogResult result = MessageBox.Show($"Bạn có thực sự muốn hợp đồng có mã {name} không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                EmployeeLoaiHDLDBO.Instance.Delete(id);
                loadData();
            }
        }

        private void grvContract_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
