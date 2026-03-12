using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmError : _Forms
    {
        public frmError()
        {
            InitializeComponent();
        }

        private void frmCollection_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadEmployeeError();
        }
        void LoadEmployeeError()
        {
            DateTime datetimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day , 00,00,00);
            DateTime datetimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataTable dt = TextUtils.LoadDataFromSP("spLoadEmployeeError", "A",
                new string[] { "@FilterText","@PageNumber","@PageSize","@DateStart","@DateEnd"}, 
                new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), datetimeS, datetimeE });
            grdData.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmErrorDetail frm = new frmErrorDetail();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeError();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể sửa mã nhân viên {0} !", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeErrorModel model =  (EmployeeErrorModel)EmployeeErrorBO.Instance.FindByPK(ID);
            frmErrorDetail frm = new frmErrorDetail();
            frm.error = model;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeError();
                grvData.FocusedRowHandle = focusedRowHandle;
            }    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa mã nhân viên {0} !", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa mã nhân viên {0} không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeErrorBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnOutputExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }
        void approved(bool isApproved)
        {
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} mã nhân viên {1} không ?", approved, code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string sql = string.Format("UPDATE dbo.EmployeeError SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
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
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Mã nhân viên {0} đã được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return;
            }
            approved(true);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isApproved == false)
            {
                MessageBox.Show(String.Format("Mã nhân viên {0} chưa được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            approved(false);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadEmployeeError();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadEmployeeError();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadEmployeeError();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadEmployeeError();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadEmployeeError();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadEmployeeError();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null,null);
        }
    }
}
