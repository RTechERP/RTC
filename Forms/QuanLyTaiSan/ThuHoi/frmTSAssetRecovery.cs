using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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
    public partial class frmTSAssetRecovery : _Forms
    {
        public frmTSAssetRecovery()
        {
            InitializeComponent();
        }

        public void RefresData()
        {
            loadData();
        }
        private void frmTSAssetRecovery_Load(object sender, EventArgs e)
        {
            //dtpDS.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1, 0, 0, 0);
            dtpDS.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpDE.Value = DateTime.Now.AddMonths(+1).AddSeconds(-1);

            cboStatus.SelectedIndex = 0;
            loadData();
            loadDetail();
            loadEmployee();
        }

        private void loadData()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetRecovery", "A",
            //   new string[] { "@DateStart", "@DateEnd", "@EmployeeReturnID", "@EmployeeRecoveryID", "@Status", "@FilterText" },
            //   new object[] { dtpDS.Value, dtpDE.Value, TextUtils.ToInt(cboEmployeeReturn.EditValue),TextUtils.ToInt(cboEmployeeRecovery.EditValue), cboStatus.SelectedIndex - 1, txtKeyword.Text });
            //grdMaster.DataSource = dt;

            int employeeReturn = TextUtils.ToInt(cboEmployeeReturn.EditValue);
            int employeeRecovery = TextUtils.ToInt(cboEmployeeRecovery.EditValue);
            int status = cboStatus.SelectedIndex - 1;
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetTSAssetRecovery",
                                            new string[] { "@DateStart", "@DateEnd", "@EmployeeReturnID", "@EmployeeRecoveryID", "@Status", "@FilterText", "@PageSize", "@PageNumber" },
                                            new object[] { dtpDS.Value, dtpDE.Value, employeeReturn, employeeRecovery, status, txtKeyword.Text.Trim(), pageSize, pageNumber }); ;
            grdMaster.DataSource = oDataSet.Tables[0];
            //if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }
        private void loadDetail()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetTSAssetRecoveryDetail", "B", new string[] { "@TSAssetRecoveryID" }, new object[] { id });
        }
        private void loadEmployee()
        {
            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT * FROM dbo.Employee");

            cboEmployeeReturn.Properties.DisplayMember = "FullName";
            cboEmployeeReturn.Properties.ValueMember = "ID";
            cboEmployeeReturn.Properties.DataSource = listEmployee;

            cboEmployeeRecovery.Properties.DisplayMember = "FullName";
            cboEmployeeRecovery.Properties.ValueMember = "ID";
            cboEmployeeRecovery.Properties.DataSource = listEmployee;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTSAssetRecoveryDetail frm = new frmTSAssetRecoveryDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                loadDetail();
            }
        }

        //phúc chỉnh sửa nút: sửa, xóa, duyệt
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string employeeReturnName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeReturnName));
            TSAssetRecoveryModel model = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
            //if (model.Status == 1)
            //{
            //    MessageBox.Show($"Cấp phát [{model.Code}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (model.IsApprovedPersonalProperty)
            //{
            //    MessageBox.Show($"Cấp phát [{model.Code}] đã NV [{employeeReturnName}] duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            frmTSAssetRecoveryDetail frm = new frmTSAssetRecoveryDetail();
            frm.recovery = model;
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
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));
            bool isApprovedAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApprovedAccountant));
            string employeeReturnName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeReturnName));

            TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
            if (id <= 0)
            {
                return;
            }

            if (isApproved)
            {
                MessageBox.Show($"Cấp phát [{code}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (isApprovedAccountant)
            {
                MessageBox.Show($"Cấp phát [{code}] đã được kế toán duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (recovery.IsApprovedPersonalProperty)
            {
                MessageBox.Show($"Cấp phát [{code}] đã được NV [{employeeReturnName}] duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialog = MessageBox.Show($"Bạn có thực sự muốn xoá Cấp phát [{code}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    TSAssetRecoveryBO.Instance.Delete(id);
                    TSAssetRecoveryDetailBO.Instance.DeleteByAttribute("TSAssetRecoveryID", id);
                    grvMaster.DeleteSelectedRows();
                }
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));
            string employeeReturnName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeReturnName));

            if (id <= 0) return;
            

            TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
            if (isApprove)
            {
                MessageBox.Show($"Biên bản [{code}] đã được duyệt!", "Thông báo");
                return;
            }
            //else if (!recovery.IsApprovedPersonalProperty)
            //{
            //    MessageBox.Show($"Biên bản [{code}] chưa được NV [{employeeReturnName}] duyệt!", "Thông báo");
            //    return;
            //}
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    approved(true);

                    recovery.Status = 1;
                    recovery.DateApprovedHR = DateTime.Now;
                    TSAssetRecoveryBO.Instance.Update(recovery);

                    grvMaster.SetFocusedRowCellValue(colStatus, true);
                    //loadData();
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }

            loadDetail();
        }
        //end
        void approved(bool isAppoved)
        {
            //Thu hồi từ
            int employeeReturnID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colEmployeeReturnID));
            int departmentReturnID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colDepartmentReturnID));
            string returnName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeReturnName));

            //Người thu hồi
            int employeeRecoveryID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colEmployeeRecoveryID));
            int departmentRecoveryID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colDepartmentRecoveryID));
            string recoveryName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeRecoveryName));

            string note = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNote));
            if (isAppoved)
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
                    int lastTSStatusAssetID = TextUtils.ToInt(grvData.GetRowCellValue(i, colLastTSStatusAssetID));//ndnhat update 06/08/2025
                    TSStatusAssetModel statusModel = SQLHelper<TSStatusAssetModel>.FindByID(lastTSStatusAssetID);//ndnhat update 06/08/2025

                    //TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);
                    //tSAsset.EmployeeID = employeeRecoveryID;
                    //tSAsset.EmployeeID = 0;
                    //tSAsset.DepartmentID = departmentRecoveryID;
                    //tSAsset.StatusID = 1;

                    //tSAsset.Status = "Chưa sử dụng";
                    //tSAsset.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));
                    //TSAssetManagementBO.Instance.Update(tSAsset);


                    //Update thông tin tài sản
                    var myDict = new Dictionary<string, object>()
                    {
                        {"EmployeeID",employeeRecoveryID },
                        {"DepartmentID",departmentRecoveryID },
                        {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                        {"UpdatedBy",Global.LoginName },
                        {"StatusID",1 },
                    };
                    SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, assetManagementID);

                    //TSAllocationEvictionAssetModel tSAllocation = SQLHelper<TSAllocationEvictionAssetModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TSAllocationEvictionAsset WHERE AssetManagementID = {assetManagementID} ORDER BY ID DESC");

                    //tSAllocation.Status = "Đã thu hồi";
                    //tSAllocation.Note = recoveryName + " đã thu hồi của " + returnName;

                    //TSAllocationEvictionAssetBO.Instance.Update(tSAllocation);

                    //Insert thêm dòng mời
                    TSAssetManagementModel assetModel = SQLHelper<TSAssetManagementModel>.FindByID(assetManagementID);
                    TSAllocationEvictionAssetModel tSAllocation = new TSAllocationEvictionAssetModel();
                    tSAllocation.AssetManagementID = assetManagementID;
                    tSAllocation.EmployeeID = employeeReturnID;
                    //tSAllocation.Status = "Đang sử dụng";
                    tSAllocation.Status = "Đã thu hồi";
                    //tSAllocation.Note = recoveryName + " đã thu hồi của " + returnName; ;
                    tSAllocation.Note = $"{recoveryName} đã thu hồi tài sản mã {assetModel.TSCodeNCC} từ {returnName} từ trạng thái {statusModel.Status}"; //ndnhat update 06/08/2025
                    //tSAllocation.Note = note;
                    tSAllocation.DateAllocation = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue(colDateRecovery));
                    TSAllocationEvictionAssetBO.Instance.Insert(tSAllocation);
                }
            }
            else
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));

                    //TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);
                    //tSAsset.EmployeeID = employeeReturnID;
                    //tSAsset.DepartmentID = departmentReturnID;
                    //tSAsset.StatusID = 2;
                    //tSAsset.Status = "Đang sử dụng";
                    //tSAsset.Note = note;
                    //TSAssetManagementBO.Instance.Update(tSAsset);


                    //Update thông tin tài sản
                    var myDict = new Dictionary<string, object>()
                    {
                        {"EmployeeID",employeeReturnID },
                        {"DepartmentID",departmentReturnID },
                        {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                        {"UpdatedBy",Global.LoginName },
                        {"StatusID",2 },
                    };
                    SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, assetManagementID);

                    //TSAllocationEvictionAssetModel tSAllocation = SQLHelper<TSAllocationEvictionAssetModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TSAllocationEvictionAsset WHERE AssetManagementID = {assetManagementID} ORDER BY ID DESC");

                    //tSAllocation.Status = "Đã điều chuyển";
                    //tSAllocation.Note = "Đã điều chuyển cho " + returnName;

                    //TSAllocationEvictionAssetBO.Instance.Update(tSAllocation);

                    //Insert thêm dòng mời
                    TSAllocationEvictionAssetModel tSAllocation = new TSAllocationEvictionAssetModel();
                    tSAllocation.AssetManagementID = assetManagementID;
                    tSAllocation.EmployeeID = employeeReturnID;
                    tSAllocation.Status = "Đang sử dụng";
                    tSAllocation.Note = "Được điều chuyển từ " + recoveryName;
                    tSAllocation.DateAllocation = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue(colDateRecovery));

                    TSAllocationEvictionAssetBO.Instance.Insert(tSAllocation);
                }
            }

        }


        //phúc sửa nút hủy duyệt
        private void btnUnApproved_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));

            bool isApprovedAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApprovedAccountant));
            if (id <= 0)
            {
                return;
            }

            if (isApprovedAccountant)
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

                    TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
                    recovery.Status = 0;
                    recovery.DateApprovedHR = DateTime.Now;
                    TSAssetRecoveryBO.Instance.Update(recovery);


                    grvMaster.SetFocusedRowCellValue(colStatus, false);
                    //loadData();
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }

            loadDetail();
        }
        //end


        private void btnExportExcel_Click(object sender, EventArgs e)
        {


            // ====================================== lee min khooi update 02/12/2024 ==========================================
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

        // ================================================= leee min khooi update 02/12/2024 =================================================
        private void ExportExcel(string path, int index)
        {
            int masterID = TextUtils.ToInt(grvMaster.GetRowCellValue(index, colID));
            if (masterID == 0) return;

            DateTime tranferDate = TextUtils.ToDate5(grvMaster.GetRowCellValue(index, colDateRecovery));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetRowCellValue(index, colStatus));
            string code = TextUtils.ToString(grvMaster.GetRowCellValue(index, colCodeRecovery));
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

            Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
            Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

            try
            {
                string date = $"Hà Nội, Ngày {tranferDate.Day} tháng {tranferDate.Month} năm {tranferDate.Year} tại Văn phòng Công ty Cổ phần RTC Technology Việt Nam. Chúng tôi gồm các bên sau:";

                app = new Microsoft.Office.Interop.Excel.Application();
                app.Workbooks.Open(currentPath);
                workBoook = app.Workbooks[1];
                workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                workSheet.Cells[4, 1] = "BIÊN BẢN THU HỒI TÀI SẢN";
                workSheet.Cells[5, 1] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colCodeRecovery));
                workSheet.Cells[6, 1] = date;
                workSheet.Cells[8, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colEmployeeReturnName));
                workSheet.Cells[9, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colPossitionReturn));
                workSheet.Cells[10, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colDepartmentReturn));
                workSheet.Cells[13, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colEmployeeRecoveryName));
                workSheet.Cells[14, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colPossitionRecovery));
                workSheet.Cells[15, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colDepartmentRecovery));
                workSheet.Cells[17, 3] = TextUtils.ToString(grvMaster.GetRowCellValue(index, colNote));

                DateTime? createdDate = TextUtils.ToDate4(grvMaster.GetRowCellValue(index, "CreatedDate"));
                DateTime? dateApprovedPersonalProperty = TextUtils.ToDate4(grvMaster.GetRowCellValue(index, "DateApprovedPersonalProperty"));

                workSheet.Cells[32, 1] = createdDate.HasValue ? createdDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                workSheet.Cells[32, 8] = dateApprovedPersonalProperty.HasValue ? dateApprovedPersonalProperty.Value.ToString("dd/MM/yyyy HH:mm") : "";

                DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetRecoveryDetail", "B", new string[] { "@TSAssetRecoveryID" }, new object[] { masterID });
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    workSheet.Cells[21, 1] = i + 1;
                    workSheet.Cells[21, 2] = TextUtils.ToString(dt.Rows[i]["TSCodeNCC"]);
                    workSheet.Cells[21, 3] = TextUtils.ToString(dt.Rows[i]["TSAssetName"]);
                    workSheet.Cells[21, 5] = TextUtils.ToString(dt.Rows[i]["UnitName"]);
                    workSheet.Cells[21, 6] = TextUtils.ToString(dt.Rows[i]["Quantity"]);
                    workSheet.Cells[21, 7] = TextUtils.ToString(dt.Rows[i]["TinhTrang"]);
                    workSheet.Cells[21, 8] = TextUtils.ToString(dt.Rows[i]["Note"]);

                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
                }


                

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
        //    string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));

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

        //    DateTime tranferDate = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colDateRecovery));

        //    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
        //    {
        //        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //        Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
        //        Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
        //        Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

        //        try
        //        {
        //            string date = $"Hà Nội, Ngày {tranferDate.Day} tháng {tranferDate.Month} năm {tranferDate.Year} tại Văn phòng Công ty Cổ phần RTC Technology Việt Nam. Chúng tôi gồm các bên sau:";

        //            app = new Microsoft.Office.Interop.Excel.Application();
        //            app.Workbooks.Open(currentPath);
        //            workBoook = app.Workbooks[1];
        //            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                   
        //            for (int i = grvData.RowCount - 1; i >= 0; i--)
        //            {
        //                workSheet.Cells[21, 1] = i + 1;
        //                workSheet.Cells[21, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
        //                workSheet.Cells[21, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colTSAssetName));
        //                workSheet.Cells[21, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));
        //                workSheet.Cells[21, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colQuantity));
        //                workSheet.Cells[21, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colTinhTrang));
        //                workSheet.Cells[21, 8] = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));

        //                ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
        //            }


        //            workSheet.Cells[4, 1] = "BIÊN BẢN THU HỒI TÀI SẢN";

        //            workSheet.Cells[5, 1] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));

        //            workSheet.Cells[6, 1] = date;
        //            workSheet.Cells[8, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeReturnName));
        //            workSheet.Cells[9, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPossitionReturn));
        //            workSheet.Cells[10, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colDepartmentReturn));
        //            workSheet.Cells[13, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeRecoveryName));
        //            workSheet.Cells[14, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPossitionRecovery));
        //            workSheet.Cells[15, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colDepartmentRecovery));
        //            workSheet.Cells[17, 3] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNote));

        //            DateTime? createdDate = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue("CreatedDate"));
        //            DateTime? dateApprovedPersonalProperty = TextUtils.ToDate4(grvMaster.GetFocusedRowCellValue("DateApprovedPersonalProperty"));

        //            workSheet.Cells[32, 1] = createdDate.HasValue ? createdDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
        //            workSheet.Cells[32, 8] = dateApprovedPersonalProperty.HasValue ? dateApprovedPersonalProperty.Value.ToString("dd/MM/yyyy HH:mm") : "";

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

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDetail();
            loadDetail();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
        //void approvedaccountant(bool isAppovedAccountant)
        //{
        //    if (isAppovedAccountant)
        //    {
        //        for (int i = 0; i < grvData.RowCount; i++)
        //        {
        //            int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));
        //            TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);

        //            TSAssetManagementBO.Instance.Update(tSAsset);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < grvData.RowCount; i++)
        //        {
        //            int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));

        //            TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);

        //            TSAssetManagementBO.Instance.Update(tSAsset);

        //        }
        //    }

        //}

        //phúc them nút hủy , duyệt của kế toán
        private void btnApprovedAccountant_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));
            bool isApprovedAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApprovedAccountant));

            if (id <= 0)
            {
                return;
            }

            if (!isApprove)
            {
                MessageBox.Show($"Biên bản [{code}] chựa được nhân sự duyệt!", "Thông báo");
                return;
            }
            else if (isApprovedAccountant)
            {
                MessageBox.Show($"Biên bản [{code}] Đã được kế toán duyệt!", "Thông báo");
                return;
            }
            else
            {
                // đã được nhân sự duyệt
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //approvedaccountant(true);

                    TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
                    recovery.IsApproveAccountant = true;
                    recovery.DateApproveAccountant = DateTime.Now;
                    TSAssetRecoveryBO.Instance.Update(recovery);

                    grvMaster.SetFocusedRowCellValue(colIsApprovedAccountant, true);
                    //loadData();
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }

            loadDetail();
        }

        private void btnUnApprovedAccountant_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCodeRecovery));

            bool isApprovedAccountant = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApprovedAccountant));
            if (id <= 0)
            {
                return;
            }

            if (!isApprovedAccountant)
            {
                MessageBox.Show($"Biên bản [{code}] kế toán chưa duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn hủy duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
                    recovery.IsApproveAccountant = false;
                    recovery.DateApproveAccountant = DateTime.Now;
                    TSAssetRecoveryBO.Instance.Update(recovery);

                    grvMaster.SetFocusedRowCellValue(colIsApprovedAccountant, false);
                    grvMaster.FocusedRowHandle = rowHanlde;
                }
            }

            loadDetail();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void frmTSAssetRecovery_Activated(object sender, EventArgs e)
        {
            RefresData();
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
        //end
    }
}