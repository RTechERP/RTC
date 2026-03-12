using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTSAssetAllocation : _Forms
    {
        public frmTSAssetAllocation()
        {
            InitializeComponent();
        }

        private void frmTSAssetAllocation_Load(object sender, EventArgs e)
        {
            //dtpDS.Value = new DateTime(DateTime.Now.Year-1, DateTime.Now.Month, 1, 0, 0, 0);
            dtpDS.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDE.Value = DateTime.Now.AddMonths(+1).AddDays(-1);
            cboStatus.SelectedIndex = 0;
            loadData();
            loadDetail();
            loadEmployee();

        }
        public void RefresData()
        {
            loadData();
        }
        private void loadData()
        {
            DateTime dateStart = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            int status = cboStatus.SelectedIndex - 1;
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);

            //DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetAllocation", "A",
            //   new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@Status", "@FilterText" },
            //   new object[] { dateStart, dateEnd, TextUtils.ToInt(cboEmployee.EditValue), cboStatus.SelectedIndex - 1, txtKeyword.Text.Trim() });
            //grdMaster.DataSource = dt;

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetTSAssetAllocation",
               new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@Status", "@FilterText", "@PageSize", "@PageNumber" },
               new object[] { dateStart, dateEnd, employeeID, status, txtKeyword.Text.Trim(), pageSize, pageNumber });
            grdMaster.DataSource = oDataSet.Tables[0];
            //if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);

        }
        private void loadDetail()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetTSAssetAllocationDetail", "B", new string[] { "@TSAssetAllocationID" }, new object[] { id });
        }
        private void loadEmployee()
        {
            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT * FROM dbo.Employee");

            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = listEmployee;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTSAssetAllocationDetail frm = new frmTSAssetAllocationDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                loadDetail();
            }
        }
        /// <summary>
        /// phúc chỉnh sửa nút sửa, xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string employeeName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeName));

            TSAssetAllocationModel model = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
            //if (model.Status == 1)
            //{
            //    MessageBox.Show($"Cấp phát [{model.Code}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (model.IsApprovedPersonalProperty)
            //{
            //    MessageBox.Show($"Cấp phát [{model.Code}] đã NV [{employeeName}] duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            frmTSAssetAllocationDetail frm = new frmTSAssetAllocationDetail();
            frm.allocationModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                loadDetail();

                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));

            bool isApproveAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            string employeeName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeName));

            if (id <= 0)
            {
                return;
            }
            TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
            if (isApproved)
            {
                MessageBox.Show($"Cấp phát [{code}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (isApproveAccountant)
            {
                MessageBox.Show($"Cấp phát [{code}] đã được kế toán duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (allocation.IsApprovedPersonalProperty)
            {
                MessageBox.Show($"Cấp phát [{code}] đã được NV [{employeeName}] duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialog = MessageBox.Show($"Bạn có thực sự muốn xoá Cấp phát [{code}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    TSAssetAllocationBO.Instance.Delete(id);
                    TSAssetAllocationDetailBO.Instance.DeleteByAttribute("TSAssetAllocationID", id);
                    grvMaster.DeleteSelectedRows();
                }
            }
            loadDetail();
        }

        /// <summary>
        /// phúc chỉnh sửa nút duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApproved_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));
            string employeeName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeName));

            if (id <= 0)
            {
                return;
            }
            TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
            if (isApprove)
            {
                MessageBox.Show($"Biên bản [{code}] đã được duyệt!", "Thông báo");
                return;
            }
            else if (!allocation.IsApprovedPersonalProperty)
            {
                MessageBox.Show($"Biên bản [{code}] chưa được NV [{employeeName}] duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    approved(true);
                    allocation.Status = 1;
                    allocation.DateApprovedHR = DateTime.Now;
                    //TSAssetAllocationBO.Instance.Update(allocation);
                    SQLHelper<TSAssetAllocationModel>.Update(allocation);


                    grvMaster.SetFocusedRowCellValue(colStatus, true);
                    //loadData();
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }
            loadData();
        }
        //<end>

        void approved(bool isAppoved)
        {
            int employeeID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colEmployeeID));
            int departmentID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colDepartmentID));
            string note = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNote));
            string receiver = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeName));

            if (isAppoved)
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));

                    //TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);
                    TSAssetManagementModel tSAsset = SQLHelper<TSAssetManagementModel>.FindByID(assetManagementID);
                    tSAsset.EmployeeID = employeeID;
                    //tSAsset.DepartmentID = departmentID;
                    tSAsset.StatusID = 2;
                    tSAsset.Status = "Đang sử dụng";
                    //tSAsset.Note = TextUtils.ToString(grvData.GetRowCellValue(i,colGhiChu));
                    //TSAssetManagementBO.Instance.Update(tSAsset);
                    SQLHelper<TSAssetManagementModel>.Update(tSAsset);

                    //TSAllocationEvictionAssetModel tSAllocation = SQLHelper<TSAllocationEvictionAssetModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TSAllocationEvictionAsset WHERE AssetManagementID = {assetManagementID} ORDER BY ID DESC");

                    //tSAllocation.Status = "Đã điều chuyển";
                    //tSAllocation.Note = "Đã điều chuyển cho " + receiver;

                    //TSAllocationEvictionAssetBO.Instance.Update(tSAllocation);

                    //Insert thêm dòng mời
                    TSAllocationEvictionAssetModel tSAllocation = new TSAllocationEvictionAssetModel();
                    tSAllocation.AssetManagementID = assetManagementID;
                    tSAllocation.EmployeeID = employeeID;
                    tSAllocation.Status = "Đang sử dụng";
                    tSAllocation.Note = $"Cấp phát cho {receiver}";
                    tSAllocation.DateAllocation = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue(colDateAllocation));

                    //TSAllocationEvictionAssetBO.Instance.Insert(tSAllocation);
                    SQLHelper<TSAllocationEvictionAssetModel>.Insert(tSAllocation);

                }
            }
            else
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));

                    //TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);
                    TSAssetManagementModel tSAsset = SQLHelper<TSAssetManagementModel>.FindByID(assetManagementID);
                    tSAsset.EmployeeID = 0;
                    //tSAsset.DepartmentID = 0;
                    tSAsset.StatusID = 1;

                    //tSAsset.Status = "Chưa sử dụng";
                    //tSAsset.Note = "Lưu kho";
                    //TSAssetManagementBO.Instance.Update(tSAsset);
                    SQLHelper<TSAssetManagementModel>.Update(tSAsset);


                    //TSAllocationEvictionAssetModel tSAllocation = SQLHelper<TSAllocationEvictionAssetModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TSAllocationEvictionAsset WHERE AssetManagementID = {assetManagementID} ORDER BY ID DESC");
                    //tSAllocation.Status = "Đã điều chuyển";
                    //tSAllocation.Note = "Đã điều chuyển cho Lương Thị Luyến ";

                    //TSAllocationEvictionAssetBO.Instance.Update(tSAllocation);

                    //Insert thêm dòng mời
                    TSAllocationEvictionAssetModel tSAllocation = new TSAllocationEvictionAssetModel();
                    tSAllocation.AssetManagementID = assetManagementID;
                    tSAllocation.EmployeeID = 0; //Chị luyến
                    tSAllocation.Status = "Chưa sử dụng";
                    //tSAllocation.Note = "Được điều chuyển từ " + receiver;
                    //tSAllocation.DateAllocation = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue(colDateAllocation));

                    //TSAllocationEvictionAssetBO.Instance.Insert(tSAllocation);
                    SQLHelper<TSAllocationEvictionAssetModel>.Insert(tSAllocation);
                }
            }

        }


        private void grvAssetAllocation_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDetail();
        }

        /// <summary>
        /// phúc chỉnh sửa nút hủy duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnApproved_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));

            bool isApproveAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            if (id <= 0)
            {
                return;
            }

            if (isApproveAccountant)
            {
                MessageBox.Show($"Biên bản [{code}] đã được kế toán được duyệt.\nVui lòng làm việc với kế toán để được huỷ duyệt!", "Thông báo");
                return;
            }
            else if (!isApprove)
            {
                MessageBox.Show($"Biên bản [{code}] chưa được duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn hủy duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    approved(false);

                    TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
                    allocation.Status = 0;
                    allocation.DateApprovedHR = DateTime.Now;

                    TSAssetAllocationBO.Instance.Update(allocation);

                    grvMaster.SetFocusedRowCellValue(colStatus, false);
                    //loadData();
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }
            loadDetail();
        }
        //<end>

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }


            //================================= lee min khooi update 02/12/2024 ================================= 
            int[] selectedRows = grvMaster.GetSelectedRows();
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                if (selectedRows.Length > 0)
                {
                    foreach (int index in selectedRows)
                    {
                        ExportExcel(path, index);
                    }
                }
                else
                {
                    int index = grvMaster.FocusedRowHandle;
                    ExportExcel(path, index);
                }
            }

        }

        //======================================================== lee min khooi update 02/12/2024 ===============================================
        private void ExportExcel(string path, int index)
        {
            int masterID = TextUtils.ToInt(grvMaster.GetRowCellValue(index, colID));
            if (masterID == 0) return;

            bool isApproved = TextUtils.ToBoolean(grvMaster.GetRowCellValue(index, colStatus));
            string code = TextUtils.ToString(grvMaster.GetRowCellValue(index, colCodeAllocation));
            string fileSourceName = "BienBanBanGiao.xlsx";
            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string currentPath = path + "\\" + code + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            var dateValue = TextUtils.ToString(grvMaster.GetRowCellValue(index, colDateAllocation));
            DateTime tranferDate = TextUtils.ToDate5(grvMaster.GetRowCellValue(index, colDateAllocation));

            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
            Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

            try
            {
                string date = $"Hà Nội, Ngày {tranferDate.Day} tháng {tranferDate.Month} năm {tranferDate.Year} tại Văn phòng Công ty Cổ phần RTC Technology Việt Nam. Chúng tôi gồm các bên sau:";

                string name = "";
                string position = "";
                string department = "";

                List<int> listEmployeeID = new List<int>();
                List<int> listPositionID = new List<int>();
                List<int> listDepartmentID = new List<int>();


                app = new Microsoft.Office.Interop.Excel.Application();
                app.Workbooks.Open(currentPath);
                workBoook = app.Workbooks[1];
                workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];



                workSheet.Cells[5, 1] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colCodeAllocation));
                workSheet.Cells[6, 1] = date;
                workSheet.Cells[13, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colEmployeeName));
                workSheet.Cells[14, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colPossition));
                workSheet.Cells[15, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colDepartment));
                workSheet.Cells[17, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colNote));

                DateTime? createdDate = TextUtils.ToDate4(grvMaster.GetRowCellValue(index, "CreatedDate"));
                DateTime? dateApprovedPersonalProperty = TextUtils.ToDate4(grvMaster.GetRowCellValue(index, "DateApprovedPersonalProperty"));

                workSheet.Cells[32, 1] = createdDate.HasValue ? createdDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                workSheet.Cells[32, 8] = dateApprovedPersonalProperty.HasValue ? dateApprovedPersonalProperty.Value.ToString("dd/MM/yyyy HH:mm") : "";

                DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetAllocationDetail", "B", new string[] { "@TSAssetAllocationID" }, new object[] { masterID });
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    int employeeId = TextUtils.ToInt(dt.Rows[i]["EmployeeID"]);
                    int positionID = TextUtils.ToInt(dt.Rows[i]["ChucVuHDID"]);
                    int departmentID = TextUtils.ToInt(dt.Rows[i]["DepartmentID"]);

                    if (!listEmployeeID.Contains(employeeId))
                    {
                        listEmployeeID.Add(employeeId);
                        name += TextUtils.ToString(dt.Rows[i]["FullName"]) + ",";
                    }

                    if (!listPositionID.Contains(positionID))
                    {
                        listPositionID.Add(positionID);
                        position += TextUtils.ToString(dt.Rows[i]["PositionName"]) + ",";
                    }

                    if (!listDepartmentID.Contains(departmentID))
                    {
                        listDepartmentID.Add(departmentID);
                        department += TextUtils.ToString(dt.Rows[i]["DepartmentName"]) + ",";
                    }

                    workSheet.Cells[21, 1] = i + 1;
                    workSheet.Cells[21, 2] = TextUtils.ToString(dt.Rows[i]["TSCodeNCC"]);
                    workSheet.Cells[21, 3] = TextUtils.ToString(dt.Rows[i]["TSAssetName"]);
                    workSheet.Cells[21, 5] = TextUtils.ToString(dt.Rows[i]["UnitName"]);
                    workSheet.Cells[21, 6] = TextUtils.ToString(dt.Rows[i]["Quantity"]);
                    workSheet.Cells[21, 7] = TextUtils.ToString(dt.Rows[i]["TinhTrang"]);
                    workSheet.Cells[21, 8] = TextUtils.ToString(dt.Rows[i]["Note"]);

                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
                }

                workSheet.Cells[8, 3] = TextUtils.ToString(name.Remove(name.LastIndexOf(",")));
                //workSheet.Cells[9, 3] = TextUtils.ToString("Nhân viên hành chính");
                workSheet.Cells[9, 3] = TextUtils.ToString(position.Remove(position.LastIndexOf(",")));
                //workSheet.Cells[10, 3] = TextUtils.ToString("Hành chính nhân sự");
                workSheet.Cells[10, 3] = TextUtils.ToString(department.Remove(department.LastIndexOf(",")));

                ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();
                ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();


                Process.Start(currentPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (app != null)
                {
                    app.ActiveWorkbook.Save();
                    app.Workbooks.Close();
                    app.Quit();
                }
            }
        }

        //private void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
        //    if (ID == 0) return;

        //    bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
        //    string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));

        //    //if (!isApproved)
        //    //{
        //    //    MessageBox.Show($"Biên bản bàn giao [{code}] chưa được duyệt.\nVui lòng duyệt trước!", "Thông báo");
        //    //    return;
        //    //}

        //    string path = "";
        //    FolderBrowserDialog fbd = new FolderBrowserDialog();
        //    if (fbd.ShowDialog() == DialogResult.OK)
        //    {
        //        path = fbd.SelectedPath;
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    string fileSourceName = "BienBanBanGiao.xlsx";

        //    string sourcePath = Application.StartupPath + "\\" + fileSourceName;

        //    string currentPath = path + "\\" + code + ".xlsx";
        //    try
        //    {
        //        File.Copy(sourcePath, currentPath, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return;
        //    }

        //    var dateValue = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colDateAllocation));
        //    DateTime tranferDate = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colDateAllocation));

        //    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
        //    {
        //        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //        Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
        //        Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
        //        Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

        //        try
        //        {
        //            string date = $"Hà Nội, Ngày {tranferDate.Day} tháng {tranferDate.Month} năm {tranferDate.Year} tại Văn phòng Công ty Cổ phần RTC Technology Việt Nam. Chúng tôi gồm các bên sau:";

        //            string name = "";
        //            string position = "";
        //            string department = "";

        //            List<int> listEmployeeID = new List<int>();
        //            List<int> listPositionID = new List<int>();
        //            List<int> listDepartmentID = new List<int>();


        //            app = new Microsoft.Office.Interop.Excel.Application();
        //            app.Workbooks.Open(currentPath);
        //            workBoook = app.Workbooks[1];
        //            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];



        //            workSheet.Cells[5, 1] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));
        //            workSheet.Cells[6, 1] = date;
        //            //workSheet.Cells[8, 3] = TextUtils.ToString(name.Remove(name.LastIndexOf(",")));
        //            ////workSheet.Cells[9, 3] = TextUtils.ToString("Nhân viên hành chính");
        //            //workSheet.Cells[9, 3] = TextUtils.ToString(position.Remove(position.LastIndexOf(",")));
        //            ////workSheet.Cells[10, 3] = TextUtils.ToString("Hành chính nhân sự");
        //            //workSheet.Cells[10, 3] = TextUtils.ToString(department.Remove(department.LastIndexOf(",")));

        //            workSheet.Cells[13, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeName));
        //            workSheet.Cells[14, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPossition));
        //            workSheet.Cells[15, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colDepartment));
        //            workSheet.Cells[17, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNote));

        //            DateTime? createdDate  = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue("CreatedDate"));
        //            DateTime? dateApprovedPersonalProperty = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue("DateApprovedPersonalProperty"));

        //            workSheet.Cells[32, 1] = createdDate.HasValue ? createdDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
        //            workSheet.Cells[32, 8] = dateApprovedPersonalProperty.HasValue ? dateApprovedPersonalProperty.Value.ToString("dd/MM/yyyy HH:mm") : "";

        //            for (int i = grvData.RowCount - 1; i >= 0; i--)
        //            {
        //                int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeIDDetail));
        //                int positionID = TextUtils.ToInt(grvData.GetRowCellValue(i, colChucVuHDID));
        //                int departmentID = TextUtils.ToInt(grvData.GetRowCellValue(i, colDepartmentIDDetail));

        //                if (!listEmployeeID.Contains(employeeId))
        //                {
        //                    listEmployeeID.Add(employeeId);
        //                    name += TextUtils.ToString(grvData.GetRowCellValue(i, colFullName)) + ",";
        //                }

        //                if (!listPositionID.Contains(positionID))
        //                {
        //                    listPositionID.Add(positionID);
        //                    position += TextUtils.ToString(grvData.GetRowCellValue(i, colPositionName)) + ",";
        //                }

        //                if (!listDepartmentID.Contains(departmentID))
        //                {
        //                    listDepartmentID.Add(departmentID);
        //                    department += TextUtils.ToString(grvData.GetRowCellValue(i, colDepartmentName)) + ",";
        //                }

        //                workSheet.Cells[21, 1] = i + 1;
        //                workSheet.Cells[21, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
        //                workSheet.Cells[21, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colTSAssetName));
        //                workSheet.Cells[21, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));
        //                workSheet.Cells[21, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colQuantity));
        //                workSheet.Cells[21, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colTinhTrang));
        //                workSheet.Cells[21, 8] = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));

        //                ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
        //            }

        //            workSheet.Cells[8, 3] = TextUtils.ToString(name.Remove(name.LastIndexOf(",")));
        //            //workSheet.Cells[9, 3] = TextUtils.ToString("Nhân viên hành chính");
        //            workSheet.Cells[9, 3] = TextUtils.ToString(position.Remove(position.LastIndexOf(",")));
        //            //workSheet.Cells[10, 3] = TextUtils.ToString("Hành chính nhân sự");
        //            workSheet.Cells[10, 3] = TextUtils.ToString(department.Remove(department.LastIndexOf(",")));

        //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();
        //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        finally
        //        {
        //            if (app != null)
        //            {
        //                app.ActiveWorkbook.Save();
        //                app.Workbooks.Close();
        //                app.Quit();
        //            }
        //        }
        //        Process.Start(currentPath);
        //    }
        //}

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            loadDetail();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
            loadDetail();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
            loadDetail();
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
            loadDetail();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());
            if (pageNumber >= TextUtils.ToInt(txtTotalPage.Text.Trim()))
            {
                return;
            }

            txtPageNumber.Text = TextUtils.ToString(pageNumber + 1);
            loadData();
            loadDetail();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            txtPageNumber.Text = txtTotalPage.Text.Trim();
            loadData();
            loadDetail();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
            loadDetail();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        /// <summary>
        /// Phúc thêm duyệt, hủy duyệt của kế qoán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsApproveAccountant_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));

            bool isApproveAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            if (id <= 0)
            {
                return;
            }

            if (!isApprove)
            {
                MessageBox.Show($"Biên bản [{code}] chưa được nhân sự duyệt!", "Thông báo");
                return;
            }
            else if (isApproveAccountant)
            {
                MessageBox.Show($"Biên bản [{code}] đã được kế toán duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                    //TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
                    TSAssetAllocationModel allocation = SQLHelper<TSAssetAllocationModel>.FindByID(id);
                    allocation.IsApproveAccountant = true;
                    allocation.DateApproveAccountant = DateTime.Now;
                    //TSAssetAllocationBO.Instance.Update(allocation);
                    SQLHelper<TSAssetAllocationModel>.Update(allocation);

                    grvMaster.SetFocusedRowCellValue(colIsApproveAccountant, true);
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }

            loadDetail();
        }

        private void btnUnIsApproveAccountant_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeAllocation));

            bool isApproveAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            if (id <= 0)
            {
                return;
            }

            if (!isApproveAccountant)
            {
                MessageBox.Show($"Biên bản [{code}] kế toán chưa duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn hủy duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    //TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
                    TSAssetAllocationModel allocation = SQLHelper<TSAssetAllocationModel>.FindByID(id);
                    allocation.IsApproveAccountant = false;
                    allocation.DateApproveAccountant = DateTime.Now;
                    //TSAssetAllocationBO.Instance.Update(allocation);
                    SQLHelper<TSAssetAllocationModel>.Update(allocation);

                    grvMaster.SetFocusedRowCellValue(colIsApproveAccountant, false);
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }

            loadDetail();
        }

        private void frmTSAssetAllocation_Activated(object sender, EventArgs e)
        {
            RefresData();
        }
    }
}