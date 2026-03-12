using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPersonalProperty : _Forms
    {
        public frmPersonalProperty()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTranferAssetDetail frm = new frmTranferAssetDetail();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                loadDataDetail();
            }
        }

        public void RefreshData()
        {
            loadData();
        }



        private void frmTranferAssetMaster_Load(object sender, EventArgs e)
        {
            DateTime firstDate = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDS.Value = firstDate.AddYears(-1);
            lbEmployee.Text = Global.AppCodeName + " - " + Global.AppFullName;

            cboAssetCategory.SelectedIndex = 0;
            LoadTSAssetManagement();
            loadData();
            grvPersonalPropertyMaster.RowCellClick += grvPersonalPropertyMaster_RowCellClick;
        }

        private void loadData()
        {
            DateTime dateTimeS = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59).AddSeconds(+1);

            int assetCategory = cboAssetCategory.SelectedIndex - 1;
            //int receiverID = TextUtils.ToInt(cboReceiver.EditValue);
            int receiverID = Global.IsAdmin ? 0 : Global.EmployeeID;

            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());

            //DataTable dt = TextUtils.LoadDataFromSP("spGetPersonalProperty", "A",
            //                                new string[] { "@DateStart", "@DateEnd", "@ReceiverID", "@AssetCategory" },
            //                                new object[] { dateTimeS, dateTimeE, receiverID, assetCategory });
            //grdPersonalPropertyMaster.DataSource = dt;


            //DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetPersonalProperty",
            //                                new string[] { "@DateStart", "@DateEnd", "@ReceiverID", "@AssetCategory" },
            //                                new object[] { dateTimeS, dateTimeE, receiverID, assetCategory });
            //grdPersonalPropertyMaster.DataSource = oDataSet.Tables[0];
            //txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
            //receiverID = 339;
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetPersonalProperty",
                                            new string[] { "@DateStart", "@DateEnd", "@ReceiverID", "@AssetCategory", "@FilterText", "@PageNumber", "@PageSize" },
                                            new object[] { dateTimeS, dateTimeE, receiverID, assetCategory, txtKeyword.Text.Trim(), pageNumber, pageSize });
            //if (oDataSet.Tables.Count > 0)
            //{

            //}

            grdPersonalPropertyMaster.DataSource = oDataSet.Tables[0];
            if (oDataSet.Tables[0].Rows.Count <= 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[0].Rows[0]["TotalPage"]);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            LoadTSAssetManagement();
            loadDataDetail();
        }

        private void grvTranferMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataDetail();
        }
        private void loadDataDetail()
        {
            int ID = TextUtils.ToInt(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetID));
            int assetCategory = TextUtils.ToInt(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetCategory));

            DataTable dt = TextUtils.LoadDataFromSP("spGetPersonalPropertyDetail", "B", new string[] { "@AssetID", "@AssetCategory" }, new object[] { ID, assetCategory });
            grdData.DataSource = dt;
        }


        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvPersonalPropertyMaster.GetFocusedRowCellValue(colID));
            if (ID == 0) return;

            bool isApprovedPersonalProperty = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetFocusedRowCellValue(colIsApprovedPersonalProperty));
            bool isApproved = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetFocusedRowCellValue(colIsApproved));
            bool isApprovedAccountant = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            int assetCategory = TextUtils.ToInt(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetCategory));
            string code = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetCode));
            if (!isApprovedPersonalProperty)
            {
                MessageBox.Show($"Biên bản bàn giao [{code}] chưa duyệt.\nVui lòng duyệt trước!", "Thông báo");
                return;
            }
            if (!isApproved)
            {
                MessageBox.Show($"Biên bản bàn giao [{code}] chưa được nhân sự duyệt.\nVui lòng duyệt trước!", "Thông báo");
                return;
            }
            if (!isApprovedAccountant)
            {
                MessageBox.Show($"Biên bản bàn giao [{code}] chưa được kế toán duyệt.\nVui lòng duyệt trước!", "Thông báo");
                return;
            }

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
            string fileSourceName = "BienBanBanGiao.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;

            string currentPath = path + "\\" + code + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (assetCategory == 0)
            {
                DateTime tranferDate = TextUtils.ToDate5(grvPersonalPropertyMaster.GetFocusedRowCellValue(colImplementationDate));

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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


                        workSheet.Cells[6, 1] = date;
                        workSheet.Cells[8, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDeliverName));
                        workSheet.Cells[9, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colPossitionDeliver));
                        workSheet.Cells[10, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDepartmentDeliver));
                        workSheet.Cells[13, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colReceiverName));
                        workSheet.Cells[14, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colPossitionReceiver));
                        workSheet.Cells[15, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDepartmentReceiver));
                        workSheet.Cells[17, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetNote));
                        for (int i = grvData.RowCount - 1; i >= 0; i--)
                        {
                            workSheet.Cells[21, 1] = i + 1;
                            workSheet.Cells[21, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                            workSheet.Cells[21, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colTSAssetName));
                            workSheet.Cells[21, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));
                            workSheet.Cells[21, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colQuantity));
                            workSheet.Cells[21, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colStatus));
                            workSheet.Cells[21, 8] = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));

                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
                        }
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();



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
                    Process.Start(currentPath);
                }
            }
            else if (assetCategory == 1)
            {
                var dateValue = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colImplementationDate));
                DateTime tranferDate = TextUtils.ToDate5(grvPersonalPropertyMaster.GetFocusedRowCellValue(colImplementationDate));

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
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

                        for (int i = grvData.RowCount - 1; i >= 0; i--)
                        {
                            int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeIDDetail));
                            int positionID = TextUtils.ToInt(grvData.GetRowCellValue(i, colChucVuHDID));
                            int departmentID = TextUtils.ToInt(grvData.GetRowCellValue(i, colDepartmentIDDetail));

                            if (!listEmployeeID.Contains(employeeId))
                            {
                                listEmployeeID.Add(employeeId);
                                name += TextUtils.ToString(grvData.GetRowCellValue(i, colFullName)) + ",";
                            }

                            if (!listPositionID.Contains(positionID))
                            {
                                listPositionID.Add(positionID);
                                position += TextUtils.ToString(grvData.GetRowCellValue(i, colPositionName)) + ",";
                            }

                            if (!listDepartmentID.Contains(departmentID))
                            {
                                listDepartmentID.Add(departmentID);
                                department += TextUtils.ToString(grvData.GetRowCellValue(i, colDepartmentName)) + ",";
                            }

                            workSheet.Cells[21, 1] = i + 1;
                            workSheet.Cells[21, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                            workSheet.Cells[21, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colTSAssetName));
                            workSheet.Cells[21, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));
                            workSheet.Cells[21, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colQuantity));
                            workSheet.Cells[21, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colStatus));
                            workSheet.Cells[21, 8] = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));

                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
                        }
                        workSheet.Cells[6, 1] = date;
                        workSheet.Cells[8, 3] = TextUtils.ToString(name.Remove(name.LastIndexOf(",")));
                        //workSheet.Cells[9, 3] = TextUtils.ToString("Nhân viên hành chính");
                        workSheet.Cells[9, 3] = TextUtils.ToString(position.Remove(position.LastIndexOf(",")));
                        //workSheet.Cells[10, 3] = TextUtils.ToString("Hành chính nhân sự");
                        workSheet.Cells[10, 3] = TextUtils.ToString(department.Remove(department.LastIndexOf(",")));

                        workSheet.Cells[13, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colReceiverName));
                        workSheet.Cells[14, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colPossitionReceiver));
                        workSheet.Cells[15, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDepartmentReceiver));
                        workSheet.Cells[17, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetNote));

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();



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
                    Process.Start(currentPath);
                }
            }
            else if (assetCategory == 2)
            {
                DateTime tranferDate = TextUtils.ToDate5(grvPersonalPropertyMaster.GetFocusedRowCellValue(colImplementationDate));

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

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
                        workSheet.Cells[6, 1] = date;
                        workSheet.Cells[8, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colReceiverName));
                        workSheet.Cells[9, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colPossitionReceiver));
                        workSheet.Cells[10, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDepartmentReceiver));
                        workSheet.Cells[13, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDeliverName));
                        workSheet.Cells[14, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colPossitionDeliver));
                        workSheet.Cells[15, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colDepartmentDeliver));
                        workSheet.Cells[17, 3] = TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetNote));
                        for (int i = grvData.RowCount - 1; i >= 0; i--)
                        {
                            workSheet.Cells[21, 1] = i + 1;
                            workSheet.Cells[21, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                            workSheet.Cells[21, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colTSAssetName));
                            workSheet.Cells[21, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));
                            workSheet.Cells[21, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colQuantity));
                            workSheet.Cells[21, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colStatus));
                            workSheet.Cells[21, 8] = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));

                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
                        }
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();



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
                    Process.Start(currentPath);
                }
            }
        }

        private void grvTranferMaster_DoubleClick(object sender, EventArgs e)
        {
            btnShowDetail_Click(null, null);
        }
        private void btnApproved_Click(object sender, EventArgs e)
        {
            ProcessApproval(true);
            return;

            //Không dùng bên dưới nữa
            Int32[] selectedRowHandles = grvPersonalPropertyMaster.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;

            if (selectedRowHandles.Length > 1)
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có muốn duyệt [{selectedRowHandles.Length}] biên bản không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No) return;
            };

            //List<string> approvedCodes = new List<string>();
            //for (int i = 0; i < selectedRowHandles.Length; i++)
            //{
            //    int selectedRowHandle = selectedRowHandles[i];
            //    string code = TextUtils.ToString(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetCode));
            //    bool isApprovedPersonalProperty = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty));
            //    if (isApprovedPersonalProperty)
            //    {
            //        approvedCodes.Add(code);
            //    }
            //}

            //if (approvedCodes.Count > 0)
            //{
            //    string message = $"Các biên bản đã được duyệt:\n{string.Join("\n", approvedCodes)}";
            //    MessageBox.Show(message, "Thông báo");
            //}

            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];

                int id = TextUtils.ToInt(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetID));
                bool isApprovePersonalProperty = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty));
                int assetCategory = TextUtils.ToInt(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetCategory));

                if (id <= 0)
                {
                    continue;
                }
                if (isApprovePersonalProperty)
                {
                    continue;
                }

                if (assetCategory == 0)
                {
                    // điều chuyển
                    TSTranferAssetModel tSTranfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
                    tSTranfer.IsApprovedPersonalProperty = true;
                    tSTranfer.DateApprovedPersonalProperty = DateTime.Now;
                    TSTranferAssetBO.Instance.Update(tSTranfer);

                    grvPersonalPropertyMaster.SetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty, true);
                }
                else if (assetCategory == 1)
                {
                    //cấp phát
                    TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
                    allocation.IsApprovedPersonalProperty = true;
                    allocation.DateApprovedPersonalProperty = DateTime.Now;
                    TSAssetAllocationBO.Instance.Update(allocation);

                    grvPersonalPropertyMaster.SetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty, true);
                }
                else if (assetCategory == 2)
                {
                    // thu lồi
                    TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
                    recovery.IsApprovedPersonalProperty = true;
                    recovery.DateApprovedPersonalProperty = DateTime.Now;
                    TSAssetRecoveryBO.Instance.Update(recovery);

                    grvPersonalPropertyMaster.SetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty, true);
                }
            }
            loadData();
        }


        private void btnUnApproved_Click(object sender, EventArgs e)
        {

            ProcessApproval(false);
            return;

            //Không dùng bên dưới nữa
            Int32[] selectedRowHandles = grvPersonalPropertyMaster.GetSelectedRows();
            if (selectedRowHandles.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn biên bản muốn huỷ duyêt!", "Thông báo");
                return;
            }


            //List<string> lsCodes = new List<string>();
            //List<string> lsIsApproved = new List<string>();
            //for (int i = 0; i < selectedRowHandles.Length; i++)
            //{
            //    int selectedRowHandle = selectedRowHandles[i];
            //    string code = TextUtils.ToString(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetCode));
            //    bool isApprovedPersonalProperty = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty));
            //    bool isApproved = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApproved));
            //    bool isApprovedAccountant = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApproveAccountant));

            //    if (!isApprovedPersonalProperty)
            //    {
            //        lsCodes.Add(code);
            //    }
            //    if (isApproved || isApprovedAccountant)
            //    {
            //        lsIsApproved.Add(code);
            //    }
            //}

            //if (lsCodes.Count > 0)
            //{
            //    string message = $"Các biên bản chưa được duyệt:\n{string.Join("\n", lsCodes)}";
            //    MessageBox.Show(message, "Thông báo");
            //}

            //if (lsIsApproved.Count > 0)
            //{
            //    string message = $"Các biên bản đã được duyệt:\n{string.Join("\n", lsIsApproved)}\n\nVui lòng làm việc với nhân sự & kế toán để được huỷ duyệt!";
            //    MessageBox.Show(message, "Thông báo");
            //}

            DialogResult dialogResult = MessageBox.Show($"Bạn có muốn hủy duyệt [{selectedRowHandles.Length}] biên bản không?\n" +
                                                        $"Những biên bản đã được HR duyệt sẽ không thể huỷ duyệt.\n" +
                                                        $"Vui lòng liên hệ HR.", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No) return;

            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];

                int id = TextUtils.ToInt(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetID));
                bool isApprovePersonalProperty = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty));
                int assetCategory = TextUtils.ToInt(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetCategory));
                string code = TextUtils.ToString(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colAssetCode));

                bool isApprovedHR = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(selectedRowHandle, colIsApproved));
                if (id <= 0) continue;
                if (isApprovedHR) continue;

                //if (!isApprovePersonalProperty)
                //{
                //    continue;
                //}

                TSTranferAssetModel tSTranfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
                TSAssetAllocationModel allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
                TSAssetRecoveryModel recovery = (TSAssetRecoveryModel)TSAssetRecoveryBO.Instance.FindByPK(id);
                if (assetCategory == 0)
                {
                    if (tSTranfer.IsApproved || tSTranfer.IsApproveAccountant)
                    {
                        continue;
                    }
                    // điều chuyển
                    tSTranfer.IsApprovedPersonalProperty = false;
                    tSTranfer.DateApprovedPersonalProperty = DateTime.Now;
                    TSTranferAssetBO.Instance.Update(tSTranfer);

                    grvPersonalPropertyMaster.SetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty, false);
                }
                else if (assetCategory == 1)
                {
                    if (allocation.Status == 1 || allocation.IsApproveAccountant)
                    {
                        continue;
                    }
                    //cấp phát

                    allocation.IsApprovedPersonalProperty = false;
                    allocation.DateApprovedPersonalProperty = DateTime.Now;
                    TSAssetAllocationBO.Instance.Update(allocation);

                    grvPersonalPropertyMaster.SetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty, false);
                }
                else if (assetCategory == 2)
                {

                    if (recovery.Status == 1 || recovery.IsApproveAccountant)
                    {
                        continue;
                    }
                    // thu lồi

                    recovery.IsApprovedPersonalProperty = false;
                    recovery.DateApprovedPersonalProperty = DateTime.Now;
                    TSAssetRecoveryBO.Instance.Update(recovery);

                    grvPersonalPropertyMaster.SetRowCellValue(selectedRowHandle, colIsApprovedPersonalProperty, false);
                }
            }

            loadData();
        }


        private void ProcessApproval(bool isApprove)
        {
            Int32[] selectedRowHandles = grvPersonalPropertyMaster.GetSelectedRows();
            if (selectedRowHandles.Length <= 0)
            {
                MessageBox.Show(isApprove ? "Vui lòng chọn biên bản muốn duyệt!" : "Vui lòng chọn biên bản muốn huỷ duyệt!", "Thông báo");
                return;
            }

            if (isApprove && selectedRowHandles.Length > 1)
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có muốn duyệt [{selectedRowHandles.Length}] biên bản không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No) return;
            }
            else if (!isApprove)
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có muốn hủy duyệt [{selectedRowHandles.Length}] biên bản không?\n" +
                                                            $"Những biên bản đã được HR duyệt sẽ không thể huỷ duyệt.\n" +
                                                            $"Vui lòng liên hệ HR.", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No) return;
            }

            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int rowHandle = selectedRowHandles[i];

                int id = TextUtils.ToInt(grvPersonalPropertyMaster.GetRowCellValue(rowHandle, colAssetID));
                int assetCategory = TextUtils.ToInt(grvPersonalPropertyMaster.GetRowCellValue(rowHandle, colAssetCategory));
                bool isApprovedHR = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(rowHandle, colIsApproved));
                bool isApproveCurrent = TextUtils.ToBoolean(grvPersonalPropertyMaster.GetRowCellValue(rowHandle, colIsApprovedPersonalProperty));

                if (id <= 0) continue;
                if (!isApprove && isApprovedHR) continue; // Không được huỷ duyệt nếu HR đã duyệt
                if (isApprove && isApproveCurrent) continue; // Bỏ qua nếu đã được duyệt rồi
                if (!isApprove && !isApproveCurrent) continue; // Bỏ qua nếu chưa duyệt

                switch (assetCategory)
                {
                    case 0: // Điều chuyển
                        var transfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
                        if (!isApprove && (transfer.IsApproved || transfer.IsApproveAccountant)) continue;

                        transfer.IsApprovedPersonalProperty = isApprove;
                        transfer.DateApprovedPersonalProperty = DateTime.Now;
                        TSTranferAssetBO.Instance.Update(transfer);
                        break;

                    case 1: // Cấp phát
                        var allocation = (TSAssetAllocationModel)TSAssetAllocationBO.Instance.FindByPK(id);
                        if (!isApprove && (allocation.Status == 1 || allocation.IsApproveAccountant)) continue;

                        allocation.IsApprovedPersonalProperty = isApprove;
                        allocation.DateApprovedPersonalProperty = DateTime.Now;
                        TSAssetAllocationBO.Instance.Update(allocation);
                        break;

                    case 2: // Thu hồi (không xử lý duyệt)
                        continue;
                }

                grvPersonalPropertyMaster.SetRowCellValue(rowHandle, colIsApprovedPersonalProperty, isApprove);
            }

            loadData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));

                e.Handled = true;
            }
        }

        private void grvTranferMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvPersonalPropertyMaster.GetFocusedRowCellValue(grvPersonalPropertyMaster.FocusedColumn)));
                e.Handled = true;
            }
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
            if (pageNumber >= TextUtils.ToInt(txtTotalPage.Text.Trim()))
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

        private void cboAssetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();

            if (cboAssetCategory.SelectedIndex == 1 || cboAssetCategory.SelectedIndex == 2)
            {
                if (grvPersonalPropertyMaster.Columns.Count >= 9)
                {
                    grvPersonalPropertyMaster.Columns[6].Caption = "Người nhận";
                    grvPersonalPropertyMaster.Columns[9].Caption = "Người giao";
                }
            }
            else if (cboAssetCategory.SelectedIndex == 3)
            {
                if (grvPersonalPropertyMaster.Columns.Count >= 9)
                {
                    grvPersonalPropertyMaster.Columns[6].Caption = "Thu hồi từ";
                    grvPersonalPropertyMaster.Columns[9].Caption = "Người thu hồi";
                }
            }
            else
            {
                if (grvPersonalPropertyMaster.Columns.Count >= 9)
                {
                    grvPersonalPropertyMaster.Columns[6].Caption = "Người nhận/Thu hồi từ";
                    grvPersonalPropertyMaster.Columns[9].Caption = "Người giao/Người thu hồi";
                }
            }
        }

        private void dtpDS_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtpDE_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvPersonalPropertyMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.RowHandle >= 0 && e.Button == MouseButtons.Left)
            //{
            //    GridView view = sender as GridView;
            //    GridColumn clickedColumn = e.Column;

            //    if (clickedColumn != null && clickedColumn.ColumnType == typeof(bool))
            //    {
            //        bool cellValue = (bool)view.GetRowCellValue(e.RowHandle, clickedColumn);
            //        view.SetRowCellValue(e.RowHandle, clickedColumn, !cellValue);
            //    }
            //    else
            //    {
            //        view.SelectRow(e.RowHandle);
            //    }
            //}
        }

        private void frmPersonalProperty_Activated(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnShowDetail_Click(object sender, EventArgs e)
        {
            int assetID = TextUtils.ToInt(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetID));
            int assetCategory = TextUtils.ToInt(grvPersonalPropertyMaster.GetFocusedRowCellValue(colAssetCategory));

            if (assetID <= 0) return;
            if (assetCategory == 0) //Nếu là điều chuyển
            {
                TSTranferAssetModel tranferAssetModel = SQLHelper<TSTranferAssetModel>.FindByID(assetID);
                frmTranferAssetDetail frm = new frmTranferAssetDetail();
                frm.transfer = tranferAssetModel;
                frm.btnSave.Enabled = frm.btnSaveAndClose.Enabled = false;
                frm.Show();
            }
            else if (assetCategory == 1) //Nếu là cấp phát
            {
                TSAssetAllocationModel model = SQLHelper<TSAssetAllocationModel>.FindByID(assetID);
                frmTSAssetAllocationDetail frm = new frmTSAssetAllocationDetail();
                frm.allocationModel = model;
                frm.btnSave.Enabled = frm.btnSaveAndClose.Enabled = false;
                frm.Show();
            }
            else if (assetCategory == 2)//Nếu là thu hồi
            {
                TSAssetRecoveryModel model = SQLHelper<TSAssetRecoveryModel>.FindByID(assetID);
                frmTSAssetRecoveryDetail frm = new frmTSAssetRecoveryDetail();
                frm.recovery = model;
                frm.btnSave.Enabled = frm.btnSaveAndClose.Enabled = false;
                frm.Show();
            }
        }


        // ======================== lee min khooi update 03/12/2024 ========================
        private void LoadTSAssetManagement()
        {
            
            DateTime dateTimeS = new DateTime(TextUtils.MIN_DATE.Year, TextUtils.MIN_DATE.Month, TextUtils.MIN_DATE.Day, 00, 00, 00);
            DateTime dateTimeE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            int receiverID = Global.EmployeeID;
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());

            DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetManagement", "LmkTable", new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@EmployeeID" }
                                                            , new object[] { txtKeyword.Text.Trim(), pageNumber, pageSize, dateTimeS, dateTimeE, receiverID });
            grdMaster.DataSource = dt;
        }

        private void grvMaster_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column == colStatus)
            {
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colStatusID));

                if (status == (int)TSAssetStatus.CHUASUDUNG) //Chưa sử dụng
                {
                    e.Appearance.BackColor = Color.Lime;
                }
                else if (status == (int)TSAssetStatus.SUACHUABAODUONG) //Sữa chữa, Bảo dưỡng
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else if (status == (int)TSAssetStatus.MAT) //Mất
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else if (status == (int)TSAssetStatus.HONG) //Hỏng
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else if (status == (int)TSAssetStatus.THANHLY) //Thanh lý
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else if (status == (int)TSAssetStatus.DENGHITHANHLY) //Đề nghị thanh lý
                {
                    e.Appearance.BackColor = Color.Aqua;
                }


            }
        }

        private void grvMaster_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }
        }

        private void grvMaster_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colInsurance)
            {
                if (TextUtils.ToDecimal(e.Value) > 0)
                {
                    e.DisplayText = Decimal.Round((TextUtils.ToDecimal(e.Value)), 0).ToString() + " tháng";
                }
                else
                {
                    e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
                }
            }
        }
    }
}