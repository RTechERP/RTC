using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPayroll : _Forms
    {
        public frmPayroll()
        {
            InitializeComponent();
        }

        private void frmPayroll_Load(object sender, EventArgs e)
        {
            nbrYear.Value = TextUtils.ToInt(DateTime.Now.ToString("yyyy"));
            loadData();
        }

        void loadData()
        {
            DataTable data = TextUtils.LoadDataFromSP("spGetEmployeePayroll", "A",
                new string[] { "@Year", "@Keyword", "@PageNumber", "@PageSize" },
                new object[] { TextUtils.ToInt(nbrYear.Value), txtFilterText.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value) });

            grdMaster.DataSource = data;

            if (data.Rows.Count > 0)
            {
                txtTotalPage.Text = TextUtils.ToString(data.Rows[0]["TotalPage"]);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > 1)
            {
                txtPageNumber.Text = "" + (TextUtils.ToInt(txtPageNumber.Text) - 1);
                loadData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) < TextUtils.ToInt(txtTotalPage.Text))
            {
                txtPageNumber.Text = "" + (TextUtils.ToInt(txtPageNumber.Text) + 1);
                loadData();
            }
            else return;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = txtTotalPage.Text;
            loadData();
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            if (isApproved)
            {
                MessageBox.Show($"Bảng lương tháng {Month}/{Year} đã được duyệt !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show($"Bạn có chắc muốn duyệt bảng lương tháng {Month}/{Year} không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    EmployeePayrollModel model = (EmployeePayrollModel)EmployeePayrollBO.Instance.FindByPK(id);
                    model.IsApproved = true;
                    EmployeePayrollBO.Instance.Update(model);
                    grvMaster.SetFocusedRowCellValue(colIsApproved, true);
                    grvMaster.FocusedRowHandle = -1;
                }
            }
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            if (!isApproved)
            {
                MessageBox.Show($"Bảng lương  tháng {Month}/{Year} chưa được duyệt !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show($"Bạn có chắc muốn hủy duyệt bảng lương tháng {Month}/{Year} không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    EmployeePayrollModel model = (EmployeePayrollModel)EmployeePayrollBO.Instance.FindByPK(id);
                    model.IsApproved = false;
                    EmployeePayrollBO.Instance.Update(model);
                    grvMaster.SetFocusedRowCellValue(colIsApproved, false);
                    grvMaster.FocusedRowHandle = -1;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string name = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNameMater));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (id <= 0) return;
            if (isApproved)
            {
                MessageBox.Show($"Bảng lương [{name}] đã được duyệt.\nVui lòng hủy duyệt trước khi xóa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show($"Bạn có chắc muốn xoá bảng lương [{name}] hay không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EmployeePayrollBO.Instance.Delete(id);
                    //loadData();
                    grvMaster.DeleteSelectedRows();
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPayrollDetail frm = new frmPayrollDetail();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            string name = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNameMater));

            if (id <= 0)
            {
                MessageBox.Show("Bạn chưa chọn bảng lương nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isApproved)
            {
                MessageBox.Show($"Bảng chấm công [{name}] đã được duyệt.\nVui lòng hủy duyệt trước khi sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EmployeePayrollModel employeePayroll = (EmployeePayrollModel)EmployeePayrollBO.Instance.FindByPK(id);
            frmPayrollDetail frm = new frmPayrollDetail();
            frm.payrollMasterModel = employeePayroll;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnSummaryPayroll_Click(object sender, EventArgs e)
        {
            int payRollID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));

            if (payRollID <= 0)return;
            
            int year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            int month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));

            //TextUtils.ExcuteProcedure("spInsertEmployeePayrollDetail",
            //            new string[] { "@Year", "@Month", "@PayrollID", "@EmployeeID", "@ID" },
            //            new object[] { year, month, payRollID, 0, 0 });


            //EmployeePayrollDetailModel payrollDetail = SQLHelper<EmployeePayrollDetailModel>.SqlToModel($"SELECT * FROM dbo.EmployeePayrollDetail WHERE PayrollID = {payRollID}");
            EmployeePayrollDetailModel payrollDetail = SQLHelper<EmployeePayrollDetailModel>.FindByAttribute("PayrollID", payRollID).FirstOrDefault();
            payrollDetail = payrollDetail ?? new EmployeePayrollDetailModel();
            //if (payrollDetail.ID <= 0)
            //{
            //    TextUtils.ExcuteProcedure("spInsertIntoEmployeePayrollDetail",
            //            new string[] { "@PayrollID", "@Year", "@Month", "@EmployeeID", "@LoginName" },
            //            new object[] { payRollID, year, month, 0, Global.LoginName });
            //}

            frmSummaryPayroll frm = new frmSummaryPayroll();
            frm.txtYear.Value = year;
            frm.txtMonth.Value = month;
            frm.masterID = payRollID;
            frm.Show();
        }

        private void btnBonusDeuction_Click(object sender, EventArgs e)
        {
            frmEmployeePayrollBonusDeuction frm = new frmEmployeePayrollBonusDeuction();
            //frm.year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            //frm.month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));

            frm.txtYear.Value = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            frm.txtMonth.Value = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));

            frm.ShowDialog();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.Combine(f.SelectedPath, $"BangThanhToanLuong_Nam{nbrYear.Value}.xls");
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvMaster.OptionsPrint.AutoWidth = true;
                grvMaster.OptionsPrint.ExpandAllDetails = false;
                grvMaster.OptionsPrint.PrintDetails = true;
                grvMaster.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvMaster.ExportToXls(fileName, optionsEx);
                    Process.Start(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
