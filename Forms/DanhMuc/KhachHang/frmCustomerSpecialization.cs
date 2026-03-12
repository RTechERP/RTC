using BMS;
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
    public partial class frmCustomerSpecialization : _Forms
    {
        private int indexRow = 0;
        public frmCustomerSpecialization()
        {
            InitializeComponent();
        }

        private void frmCustomerSpecialization_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string request = txtRequest.Text.ToString();
            List<CustomerSpecializationModel> list = SQLHelper<CustomerSpecializationModel>.FindAll();

                                                            //.Where(p=> string.IsNullOrWhiteSpace(request) || p.Code.Contains(request) || p.Name.Contains(request))
                                                            //.OrderBy(p => p.STT).ToList();
            grdData.DataSource = list;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmCustomerMajor frm = new frmCustomerMajor();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int i = grvData.FocusedRowHandle;
            indexRow = grvData.FocusedRowHandle;
            frmCustomerMajor frm = new frmCustomerMajor();
            frm.major = SQLHelper<CustomerSpecializationModel>.FindByID(TextUtils.ToInt(grvData.GetRowCellValue(i, colSpecializationID)));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = indexRow;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = grvData.FocusedRowHandle;
            int cusSpecializationID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSpecializationID));
            List<CustomerModel> checkList = SQLHelper<CustomerModel>.FindByAttribute("CustomerSpecializationID", cusSpecializationID).ToList();
            if(checkList.Count > 0)
            {
                MessageBox.Show($"Ngành nghề {grvData.GetRowCellValue(i, colSpecializationName)} đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            SQLHelper<CustomerSpecializationModel>.DeleteModelByID(cusSpecializationID);
            MessageBox.Show("Xóa thành công!", "Thông báo");
            LoadData();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
