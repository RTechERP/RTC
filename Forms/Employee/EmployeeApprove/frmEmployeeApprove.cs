using BMS;
using BMS.Business;
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
    public partial class frmEmployeeApprove : _Forms
    {
        public frmEmployeeApprove()
        {
            InitializeComponent();
        }

        private void frmEmployeeApprove_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM EmployeeApprove");
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeApprove", "A", new string[] { "@Type", "@ProjectID" }, new object[] { 1, 0 });
            grdData.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEmployeeApproDetail frm = new frmEmployeeApproDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (MessageBox.Show($"Bạn có chắc muốn xóa nhân viên [{code} - {fullName}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                EmployeeApproveBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }
        }
    }
}
