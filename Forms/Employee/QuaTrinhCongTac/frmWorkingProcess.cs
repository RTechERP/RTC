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
    public partial class frmWorkingProcess : _Forms
    {
        public frmWorkingProcess()
        {
            InitializeComponent();
        }

        private void WorkingProcess_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpEndDate.Value = new DateTime(dtpStartDate.Value.AddMonths(+1).AddDays(-1).Year, dtpStartDate.Value.AddMonths(+1).AddDays(-1).Month, dtpStartDate.Value.AddMonths(+1).AddDays(-1).Day, 23, 59, 59);
            LoadWorkingProcess();
        }
        void LoadWorkingProcess()
        {
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);

            DataTable dt = TextUtils.LoadDataFromSP("spLoadEmployeeWorkingProcess", "A",
                            new string[] { "@FilterText","@PageNumber","@PageSize","@DateStart","@DateEnd" }, 
                            new object[] { txtSearch.Text.Trim(), TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE });
            grdData.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmWorkingProcessDetail frm = new frmWorkingProcessDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadWorkingProcess();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            string Name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể sửa nhân viên {0} !", Name), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeWorkingProcessModel model = (EmployeeWorkingProcessModel)EmployeeWorkingProcessBO.Instance.FindByPK(ID);
            frmWorkingProcessDetail frm = new frmWorkingProcessDetail();
            frm.workprocess = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadWorkingProcess();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            string Name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa nhân viên {0} !", Name), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa nhân viên {0} không?", Name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeWorkingProcessBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        void approved(bool isApproved)
        {
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            string Name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} nhân viên {1} không ?", approved, Name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string sql = string.Format("UPDATE dbo.EmployeeWorkingProcess SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvData.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvData.SetFocusedRowCellValue(colIsApproved, 0);
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string Name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Nhân viên {0} đã được duyệt.", Name), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return;
            }
            approved(true);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string Name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (isApproved == false)
            {
                MessageBox.Show(String.Format("Nhân viên {0} chưa được duyệt.", Name), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            approved(false);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadWorkingProcess();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadWorkingProcess();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadWorkingProcess();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadWorkingProcess();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadWorkingProcess();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadWorkingProcess();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmStatusWorkingProcess frm = new frmStatusWorkingProcess();
            frm.ShowDialog();
        }
    }
}
