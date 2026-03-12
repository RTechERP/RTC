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
    public partial class frmEmployeeSetting : _Forms
    {
        public frmEmployeeSetting()
        {
            InitializeComponent();
        }

        private void frmEmployeeSetting_Load(object sender, EventArgs e)
        {
            loadData1();
            loadData2();
        }
        private void loadData1()
        {
            DataTable dt = TextUtils.Select("select * from EmployeeVehicleBussiness");
            grdVehicle.DataSource = dt;
        }

        private void loadData2()
        {
            DataTable dt = TextUtils.Select("select * from EmployeeTypeBussiness");
            grdTypeBusiness.DataSource = dt;
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmVehicleBussiness frm = new frmVehicleBussiness();
            if(frm.ShowDialog()==DialogResult.OK)
            {
                loadData1();
            }    
        }
        int _RowIndex;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            _RowIndex = grvVehicle.FocusedRowHandle;
            int id =TextUtils.ToInt(grvVehicle.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            EmployeeVehicleBussinessModel model = (EmployeeVehicleBussinessModel)EmployeeVehicleBussinessBO.Instance.FindByPK(id);
            frmVehicleBussiness frm = new frmVehicleBussiness();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData1();
            }
            grvVehicle.FocusedRowHandle = _RowIndex;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvVehicle.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvVehicle.GetFocusedRowCellValue(colVehicleCode)); 
            if (id <= 0) return;
            if(MessageBox.Show($"Bạn có chắc muốn xoá phương tiện có mã {code} không ?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                grvVehicle.DeleteSelectedRows();
                EmployeeVehicleBussinessBO.Instance.Delete(id);
            }    
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmTypeBusiness frm = new frmTypeBusiness();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData2();
            }
        }

        private void btnEditType_Click(object sender, EventArgs e)
        {
            _RowIndex = grvTypeBusiness.FocusedRowHandle;
            int id = TextUtils.ToInt(grvTypeBusiness.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            EmployeeTypeBussinessModel model = (EmployeeTypeBussinessModel)EmployeeTypeBussinessBO.Instance.FindByPK(id);
            frmTypeBusiness frm = new frmTypeBusiness();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData2();
            }
            grvTypeBusiness.FocusedRowHandle = _RowIndex;
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvTypeBusiness.GetFocusedRowCellValue(colIDtype));
            string code = TextUtils.ToString(grvTypeBusiness.GetFocusedRowCellValue(colTypeCode));
            if (id <= 0) return;
            if (MessageBox.Show($"Bạn có chắc muốn xoá loại công tác có mã {code} không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                grvTypeBusiness.DeleteSelectedRows();
                EmployeeTypeBussinessBO.Instance.Delete(id);
            }
        }
    }
}
