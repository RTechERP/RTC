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
    public partial class frmStatusWorkingProcess : _Forms
    {
        public frmStatusWorkingProcess()
        {
            InitializeComponent();
        }

        private void frmStatusWorkingProcess_Load(object sender, EventArgs e)
        {
            LoadStatus();
        }
        void LoadStatus()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.EmployeeStatus");
            grdData.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmStatusWorkingProcessDetail frm = new frmStatusWorkingProcessDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadStatus();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeStatusModel model = (EmployeeStatusModel)EmployeeStatusBO.Instance.FindByPK(ID);
            frmStatusWorkingProcessDetail frm = new frmStatusWorkingProcessDetail();
            frm.status = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadStatus();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa nhân viên {0} không?", Name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeStatusBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null,null);
        }
    }
}
