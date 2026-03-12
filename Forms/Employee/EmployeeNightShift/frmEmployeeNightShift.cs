using BMS.Model;
using DevExpress.XtraPrinting;
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
    public partial class frmEmployeeNightShift : _Forms
    {
        public frmEmployeeNightShift()
        {
            InitializeComponent();
        }

        private void frmEmployeeNightShift_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;
            cbApprovedStatusTBP.SelectedIndex = 0;

            loadDepartment();
            loadData();
        }

        /// <summary>
        /// Load phòng ban vào commbo box
        /// </summary>
        void loadDepartment()
        {
            //DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList($"Select ID, Code, Name From Department");
            cboDepartment.Properties.DataSource = listDepartment;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }

        void loadData()
        {
            DateTime dateStart = new DateTime((int)txtYear.Value, (int)txtMonth.Value, 1, 0, 0, 0);
            DateTime dateEnd = dateStart.AddMonths(+1).AddSeconds(-1);

            int isApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());
            int pageSize = TextUtils.ToInt(txtPageSize.Text.Trim());

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeNightShift",
                                                    new string[] { "@EmployeeID", "@DateStart", "@DateEnd", "@IsApproved", "@DepartmentID", "@Keyword", "@PageNumber", "@PageSize" },
                                                    new object[] { 0, dateStart, dateEnd, isApprove, departmentID, txtKeyword.Text.Trim(), pageNumber, pageSize });

            grdData.DataSource = dataSet.Tables[1];
            txtTotalPage.Text = TextUtils.ToString(dataSet.Tables[2].Rows[0]["TotalPage"]);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnApprovedHR_Click(object sender, EventArgs e)
        {
            approved(true);
            loadData();
        }

        private void btnUnapprovedHR_Click(object sender, EventArgs e)
        {
            approved(false);
            loadData();
        }

        void approved(bool isApprove)
        {
            List<int> listID = new List<int>();

            int[] rowSelected = grvData.GetSelectedRows();

            string message = isApprove ? "duyệt" : "huỷ duyệt";
            string fullName = "";
            int isApprovedTbp = 0;
            int id = 0;

            if (rowSelected.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn nhân viên muốn {message}!", "Thông báo");
                return;
            }
            else if (rowSelected.Length == 1)
            {

                id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                if (id <= 0)
                {
                    return;
                }

                fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
                isApprovedTbp = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIsApprovedTBP));

                if (isApprovedTbp != 1)
                {
                    MessageBox.Show($"Nhân viên [{fullName}] chưa được TBP duyệt!", "Thông báo");
                    return;
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {message} nhân viên [{fullName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        listID.Add(id);
                    }
                }
            }
            else
            {
                bool isCheck = true;
                foreach (int row in rowSelected)
                {
                    id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0)
                    {
                        continue;
                    }

                    fullName = TextUtils.ToString(grvData.GetRowCellValue(row, colFullName));
                    isApprovedTbp = TextUtils.ToInt(grvData.GetRowCellValue(row, colIsApprovedTBP));

                    if (isApprovedTbp != 1)
                    {
                        MessageBox.Show($"Nhân viên [{fullName}] chưa được TBP duyệt!", "Thông báo");
                        isCheck = false;
                    }
                }

                if (isCheck)
                {
                    DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {message} danh sách nhân viên đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (int row in rowSelected)
                        {
                            id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                            if (id <= 0)
                            {
                                continue;
                            }
                            listID.Add(id);
                        }
                    }

                }
                else
                {
                    return;
                }
            }

            int value = isApprove ? 1 : 0;
            string idString = string.Join(",", listID);
            if (listID.Count > 0)
            {
                TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                    new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate" },
                    new object[] { "EmployeeNighShift", "IsApprovedHR", value, idString, Global.LoginName, DateTime.Now });
            }

        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    int isApproved = 0;
            //    if (e.Column == colIsApprovedTBPText)
            //    {
            //        isApproved = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colIsApprovedTBP));
            //        if (isApproved == 0)
            //        {
            //            e.Appearance.BackColor = Color.Orange;
            //        }
            //        else if (isApproved == 1)
            //        {
            //            e.Appearance.BackColor = Color.Lime;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = Color.DarkRed;
            //            e.Appearance.ForeColor = Color.White;
            //        }
            //    }

            //    if (e.Column == colIsApprovedHRText)
            //    {
            //        isApproved = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colIsApprovedHR));
            //        if (isApproved == 0)
            //        {
            //            e.Appearance.BackColor = Color.Orange;
            //        }
            //        else if (isApproved == 1)
            //        {
            //            e.Appearance.BackColor = Color.Lime;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = Color.DarkRed;
            //            e.Appearance.ForeColor = Color.White;
            //        }
            //    }

            //}

        }



        private void btnFirst_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());
            if (pageNumber == 1)
            {
                return;
            }

            txtPageNumber.Text = TextUtils.ToString(pageNumber - 1);
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());
            if (pageNumber == TextUtils.ToInt(txtTotalPage.Text.Trim()))
            {
                return;
            }

            txtPageNumber.Text = TextUtils.ToString(pageNumber + 1);
            loadData();

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = txtTotalPage.Text.Trim();
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/DanhSachDangKyLamDem_T{txtMonth.Value}_{txtYear.Value}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                grvData.ClearSelection();
            }
        }

        private void btnReportNightShift_Click(object sender, EventArgs e)
        {
            frmSummaryEmployeeNightShift frm = new frmSummaryEmployeeNightShift();
            frm.Show();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEmployeeNightShiftDetail frm = new frmEmployeeNightShiftDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            EmployeeNighShiftModel model = SQLHelper<EmployeeNighShiftModel>.FindByID(id);
            frmEmployeeNightShiftDetail frm = new frmEmployeeNightShiftDetail();
            frm.nighShift = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
