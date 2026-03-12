using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmOvertime : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmOvertime()
        {
            InitializeComponent();
        }

        private void frmOvertime_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(-1);
            txtPageNumber.Text = "1";
            LoadDepartment();
            IDApproved = Global.EmployeeID;
            cbDepartment.EditValue = Global.DepartmentID;
            cbApprovedStatusTBP.SelectedIndex = 1; //CHưa duyệt
            if (Global.DepartmentID == 1)
            {
                IDApproved = 0;
                cbDepartment.EditValue = 0;
            }
            else if (Global.DepartmentID == 6)
            {
                IDApproved = 0;
                cbDepartment.EditValue = 0;
            }
            else
                cbDepartment.EditValue = Global.DepartmentID;
            LoadOvertime();


            grdDataFile.ContextMenuStrip = contextMenuStrip2;
        }

        void LoadOvertime()
        {
            grdData.DataSource = null;
            //if (cbApprovedStatusTBP.SelectedIndex == 1)
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)
            //    StatusApprove = 0;
            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;

            DateTime datetimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime datetimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(1);
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            //Khánh bỏ vì thấy thừa 
            //if (cbApprovedStatusTBP.SelectedIndex == 0) //Tất cả
            //{
            //    arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP" };
            //    arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), datetimeS, datetimeE, departmentId, IDApproved };
            //}
            //else
            //{
            //    arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP", "@Status" };
            //    arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), datetimeS, datetimeE, departmentId, IDApproved, StatusApprove };
            //}
            arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@DepartmentID", "@IDApprovedTP", "@Status" };
            arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), datetimeS, datetimeE, departmentId, IDApproved, StatusApprove };
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeOvertime", "A", arrParamName, arrParamValue);
            //grdData.DataSource = dt;

            DataSet data = TextUtils.LoadDataSetFromSP("spGetEmployeeOvertime", arrParamName, arrParamValue);
            DataTable dt = data.Tables[0];
            grdData.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);

           
        }


        void LoadFile()
        {
            grdDataFile.DataSource = null;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            List<EmployeeOvertimeFileModel> lstFile = SQLHelper<EmployeeOvertimeFileModel>.FindByAttribute(EmployeeOvertimeFileModel_Enum.EmployeeOvertimeID.ToString(), id);
            grdDataFile.DataSource = lstFile;
        }

        void LoadDepartment()
        {
            DataTable dtDepartment = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //frmOvertimeDetail frm = new frmOvertimeDetail();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadOvertime();
            //}

            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;

            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            DateTime dateRegister = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateRegister));

            DataTable dt = TextUtils.Select($"SELECT * FROM dbo.EmployeeOvertime WHERE EmployeeID = {employeeID} AND DateRegister = '{dateRegister.ToString("yyyy-MM-dd")}' ORDER BY TimeStart");
            frmEmployeeOvertimeDetail frm = new frmEmployeeOvertimeDetail();
            frm.dtOvertime = dt;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadOvertime();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedrowhandle = grvData.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTBP));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //if (isApproved == true)
            //{
            //    MessageBox.Show(String.Format($"Nhân viên [{name}] đã được duyệt.\nVui lòng hủy duyệt trước khi sửa!"), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Error);
            //    return;
            //}

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //EmployeeOvertimeModel model = (EmployeeOvertimeModel)EmployeeOvertimeBO.Instance.FindByPK(ID);
            //frmOvertimeDetail frm = new frmOvertimeDetail();
            //frm.overtime = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadOvertime();
            //    grvData.FocusedRowHandle = focusedrowhandle;
            //}
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            DateTime dateRegister = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateRegister));

            //var exp1 = new Expression("EmployeeID", employeeID);
            //List<EmployeeOvertimeModel> listOvertime = SQLHelper<EmployeeOvertimeModel>.FindByExpression(exp1)
            //                                                                .Where(x => x.DateRegister.Value.Year == dateRegister.Year &&
            //                                                                        x.DateRegister.Value.Month == dateRegister.Month &&
            //                                                                        x.DateRegister.Value.Day == dateRegister.Day
            //                                                                        ).ToList();

            //var exp1 = new Expression("EmployeeID", employeeID);
            //var exp2 = new Expression("DateRegister", dateRegister.ToString("yyyy-MM-dd"));
            //DataTable dt = EmployeeOvertimeBO.Instance.FindByExpression(exp1.And(exp2)).OrderBy(x => x.TimeStart).ToList();
            DataTable dt = TextUtils.Select($"SELECT * FROM dbo.EmployeeOvertime WHERE EmployeeID = {employeeID} AND DateRegister = '{dateRegister.ToString("yyyy-MM-dd")}' ORDER BY TimeStart");
            frmEmployeeOvertimeDetail frm = new frmEmployeeOvertimeDetail();
            frm.dtOvertime = dt;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadOvertime();
                grvData.FocusedRowHandle = focusedrowhandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTBP));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (isApproved == true && !(Global.IsAdmin && Global.EmployeeID <= 0))
            {
                MessageBox.Show(String.Format($"Nhân viên [{name}] đã được duyệt.\nVui lòng hủy duyệt trước khi xóa!"), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa nhân viên {0} không?", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int[] rowIndex = grvData.GetSelectedRows();
                for (int i = 0; i < rowIndex.Length; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                    EmployeeOvertimeBO.Instance.Delete(id);
                }
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        void approved(bool IsApproved, string fieldName, bool isTBP)
        {
            string approved = IsApproved == true ? "duyệt" : "hủy duyệt";
            int[] rowIndex = grvData.GetSelectedRows();
            List<int> listID = new List<int>();

            for (int i = 0; i < rowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDepartmentID));
                bool isApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTBP));
                int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colEmployeeID));
                if (employeeId == 0) continue;
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));

                if (!isApprovedTP)
                {
                    MessageBox.Show($"Bạn không thể {approved} vì nhân viên [{employeeName}] chưa được TBP duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Global.IsAdmin)
                {
                    if (departmentId != Global.DepartmentID && Global.DepartmentID != 1 && isTBP)
                    {
                        MessageBox.Show($"Nhân viên [{employeeName}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (employeeId == Global.EmployeeID && isTBP)
                {
                    MessageBox.Show($"Bạn không được {approved} cho chính mình.\nVui lòng liên hệ cấp cao hơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show(string.Format($"Bạn có chắc muốn {approved} danh sách nhân viên đã chọn không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                for (int i = 0; i < rowIndex.Length; i++)
                {

                    if (isTBP && IsApproved) //Trương phong duyet
                    {
                        listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                    }
                    if (isTBP && !IsApproved) // Truong phong huy duyet
                    {
                        if (!TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedHR)))
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                        }
                    }
                }

                if (!isTBP && IsApproved) // Nhan su duyet
                {
                    for (int i = rowIndex.Length - 1; i >= 0; i--)
                    {
                        if (TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTBP)))
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                            int type = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colType));
                            //CalculateCoefficientOff(IsApproved, rowIndex[i]);
                            //CalculateDayOff(IsApproved, rowIndex[i]);
                            int ID = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                            EmployeeOvertimeModel model = (EmployeeOvertimeModel)EmployeeOvertimeBO.Instance.FindByPK(ID);
                            if (model != null)
                            {
                                model.ApprovedHR = Global.EmployeeID;
                                //EmployeeOvertimeBO.Instance.Update(model);

                                SQLHelper<EmployeeOvertimeModel>.Update(model);
                            }
                        }
                    }
                }

                if (!isTBP && !IsApproved) //Nhan su huy duyet
                {
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        if (!isTBP && !IsApproved) //Nhan su huy duyet
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                            int type = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colType));
                            //CalculateCoefficientOff(IsApproved, rowIndex[i]);
                            //CalculateDayOff(IsApproved, rowIndex[i]);
                            //int ID = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                            //EmployeeOnLeaveModel model = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(ID);
                            //if (model != null)
                            //{
                            //    model.ApprovedHR = 0;
                            //    EmployeeOnLeaveBO.Instance.Update(model);
                            //}
                        }
                    }
                }

                TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                    new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate" },
                    new object[] { "EmployeeOvertime", fieldName, IsApproved ? 1 : 0, string.Join(",", listID), Global.LoginName, DateTime.Now });
            }
            LoadOvertime();
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            //bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //if (isApproved == true)
            //{
            //    MessageBox.Show(String.Format("Nhân viên {0} đã được duyệt.", name), TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            //    return;
            //}
            approved(true, "IsApprovedHR", false);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            //bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //if (isApproved == false)
            //{
            //    MessageBox.Show(String.Format("Nhân viên {0} chưa được duyệt.", name), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Warning);
            //    return;
            //}
            approved(false, "IsApprovedHR", false);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadOvertime();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadOvertime();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadOvertime();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadOvertime();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadOvertime();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadOvertime();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colTimeReality)
            //{
            //    if (TextUtils.ToDecimal(e.Value) > 0)
            //    {
            //        e.DisplayText = Decimal.Round((TextUtils.ToDecimal(e.Value)), 2).ToString() + " (h)";
            //    }
            //    else
            //    {
            //        e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
            //    }

            //}
            //if (e.Column == colTotalTime)
            //{
            //    if (TextUtils.ToDecimal(e.Value) > 0)
            //    {
            //        e.DisplayText = Decimal.Round((TextUtils.ToDecimal(e.Value)), 2).ToString() + " (h)";
            //    }
            //    else
            //    {
            //        e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
            //    }
            //}
            //if (e.Column == colRatio)
            //{
            //    if (TextUtils.ToDecimal(e.Value) > 0)
            //    {
            //        e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString() + " %";
            //    }
            //}
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmType frm = new frmType();
            frm.ShowDialog();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachLamThem_T{dtpStartDate.Value.Month}_{dtpStartDate.Value.Year}";
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
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void cbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadOvertime();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOvertime();
        }

        private void btnApprovedTBP_Click(object sender, EventArgs e)
        {
            approved(true, "IsApprovedTBP", true);
        }

        private void btnUnapprovedTBP_Click(object sender, EventArgs e)
        {
            approved(false, "IsApprovedTBP", true);
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadFile();
        }

        private void btnViewAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDownloadAttachFile_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //    string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

            //    if (!Directory.Exists(pathDownload))
            //    {
            //        Directory.CreateDirectory(pathDownload);
            //    }

            //    DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateOrder));
            //    string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //    string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

            //    string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
            //    string folderDownload = Path.Combine(pathDownload, fileName);
            //    string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

            //    if (File.Exists(folderDownload))
            //    {
            //        folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
            //    }


            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFile(url, folderDownload);
            //    Process.Start(folderDownload);
            //}
            //catch
            //{
            //    try
            //    {
            //        string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //        string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

            //        if (!Directory.Exists(pathDownload))
            //        {
            //            Directory.CreateDirectory(pathDownload);
            //        }

            //        DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colCreatedDate.FieldName));
            //        string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //        string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

            //        string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
            //        string folderDownload = Path.Combine(pathDownload, fileName);
            //        string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

            //        if (File.Exists(folderDownload))
            //        {
            //            folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
            //        }


            //        WebClient webClient = new WebClient();
            //        webClient.DownloadFile(url, folderDownload);
            //        Process.Start(folderDownload);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Thông báo");
            //    }
            //}
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            frmTimeSheetOT frm = new frmTimeSheetOT();
            frm.ShowDialog();
        }
    }
}
