using BMS;
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTranferAssetMaster : _Forms
    {
        public frmTranferAssetMaster()
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



        private void frmTranferAssetMaster_Load(object sender, EventArgs e)
        {
            DateTime firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDS.Value = firstDate;
            dtpDE.Value = firstDate.AddMonths(+1).AddDays(-1);

            cboStatus.SelectedIndex = 0;

            loadCboEmployee();

            loadData();

        }

        public void RefresData()
        {
            loadData();
        }

        void loadCboEmployee()
        {
            List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, Code, FullName FROM Employee");

            cboDeliver.Properties.DisplayMember = cboReceiver.Properties.DisplayMember = "FullName";
            cboDeliver.Properties.ValueMember = cboReceiver.Properties.ValueMember = "ID";
            cboDeliver.Properties.DataSource = cboReceiver.Properties.DataSource = listEmployee;
        }

        private void loadData()
        {
            DateTime dateTimeS = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59).AddSeconds(+1);

            int isApproved = cboStatus.SelectedIndex - 1;
            int deliverID = TextUtils.ToInt(cboDeliver.EditValue);
            int receiverID = TextUtils.ToInt(cboReceiver.EditValue);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text.Trim());

            //DataTable dt = TextUtils.LoadDataFromSP("spGetTranferAssetMaster", "A", 
            //                                    new string[] { "@DateStart", "@DateEnd", "@IsApproved", "@DeliverID", "@ReceiverID", "TextFilter", }, 
            //                                    new object[] { dateTimeS, dateTimeE,isApproved, deliverID, receiverID, txtKeyword.Text });
            //grdTranferMaster.DataSource = dt;

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetTranferAssetMaster",
                                            new string[] { "@DateStart", "@DateEnd", "@IsApproved", "@DeliverID", "@ReceiverID", "TextFilter", "@PageSize", "@PageNumber" },
                                            new object[] { dateTimeS, dateTimeE, isApproved, deliverID, receiverID, txtKeyword.Text.Trim(), pageSize, pageNumber });

            grdTranferMaster.DataSource = oDataSet.Tables[0];
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            loadDataDetail();
        }

        private void grvTranferMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataDetail();
        }
        private void loadDataDetail()
        {
            int ID = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetTranferAssetDetail", "A", new string[] { "@TranferAssetID" }, new object[] { ID });
            grdData.DataSource = dt;
        }

        //phúc sửa edit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));
            bool isApprovedAccountant = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            string receiverName = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colReceiverName));
            if (id <= 0)
            {
                return;
            }
            TSTranferAssetModel tranferAssetModel = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
            //if (isApproved)
            //{
            //    MessageBox.Show($"Biên bản bàn giao [{code}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else if (isApprovedAccountant)
            //{
            //    MessageBox.Show($"Biên bản bàn giao  [{code}] đã được kế toán duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else if (tranferAssetModel.IsApprovedPersonalProperty)
            //{
            //    MessageBox.Show($"Biên bản bàn giao  [{code}] đã NV [{receiverName}] duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //else
            {
                frmTranferAssetDetail frm = new frmTranferAssetDetail();
                frm.transfer = tranferAssetModel;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadData();
                    loadDataDetail();
                }
            }
        }
        //end


        //private void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    int ID = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
        //    if (ID == 0) return;

        //    bool isApproved = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproved));
        //    string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));
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

        //    string currentPath = path + "\\" + code + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
        //    try
        //    {
        //        File.Copy(sourcePath, currentPath, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return;
        //    }

        //    DateTime tranferDate = TextUtils.ToDate5(grvTranferMaster.GetFocusedRowCellValue(colTranferDate));

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
        //                workSheet.Cells[21, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colStatus));
        //                workSheet.Cells[21, 8] = TextUtils.ToString(grvData.GetRowCellValue(i, colGhiChu));

        //                ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[21]).Insert();
        //            }
        //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();
        //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[20]).Delete();


        //            workSheet.Cells[5, 1] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));
        //            workSheet.Cells[6, 1] = date;
        //            workSheet.Cells[8, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colDeliverName));
        //            workSheet.Cells[9, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colPossitionDeliver));
        //            workSheet.Cells[10, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colDepartmentDeliver));
        //            workSheet.Cells[13, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colReceiverName));
        //            workSheet.Cells[14, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colPossitionReceiver));
        //            workSheet.Cells[15, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colDepartmentReceiver));
        //            workSheet.Cells[17, 3] = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colReason));

        //            DateTime? createdDate = TextUtils.ToDate4(grvTranferMaster.GetFocusedRowCellValue("CreatedDate"));
        //            DateTime? dateApprovedPersonalProperty = TextUtils.ToDate4(grvTranferMaster.GetFocusedRowCellValue("DateApprovedPersonalProperty"));

        //            workSheet.Cells[32, 1] = createdDate.HasValue ? createdDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
        //            workSheet.Cells[32, 8] = dateApprovedPersonalProperty.HasValue ? dateApprovedPersonalProperty.Value.ToString("dd/MM/yyyy HH:mm") : "";

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

            int[] selectedRows = grvTranferMaster.GetSelectedRows();
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
                    int index = grvTranferMaster.FocusedRowHandle;
                    ExportExcel(path, index);
                }

            }
        }

        // ================================================= lee min khooi update 02/12/2024 =================================================
        private void ExportExcel(string path, int index)
        {
            int masterID = TextUtils.ToInt(grvTranferMaster.GetRowCellValue(index, colID));
            if (masterID == 0) return;

            bool isApproved = TextUtils.ToBoolean(grvTranferMaster.GetRowCellValue(index, colIsApproved));
            string code = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colCodeReport));
            DateTime tranferDate = TextUtils.ToDate5(grvTranferMaster.GetRowCellValue(index, colTranferDate));
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


                workSheet.Cells[5, 1] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colCodeReport));
                workSheet.Cells[6, 1] = date;
                workSheet.Cells[8, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colDeliverName));
                workSheet.Cells[9, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colPossitionDeliver));
                workSheet.Cells[10, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colDepartmentDeliver));
                workSheet.Cells[13, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colReceiverName));
                workSheet.Cells[14, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colPossitionReceiver));
                workSheet.Cells[15, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colDepartmentReceiver));
                workSheet.Cells[17, 3] = TextUtils.ToString(grvTranferMaster.GetRowCellValue(index, colReason));

                DateTime? createdDate = TextUtils.ToDate4(grvTranferMaster.GetRowCellValue(index, "CreatedDate"));
                DateTime? dateApprovedPersonalProperty = TextUtils.ToDate4(grvTranferMaster.GetRowCellValue(index, "DateApprovedPersonalProperty"));

                workSheet.Cells[32, 1] = createdDate.HasValue ? createdDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                workSheet.Cells[32, 8] = dateApprovedPersonalProperty.HasValue ? dateApprovedPersonalProperty.Value.ToString("dd/MM/yyyy HH:mm") : "";


                DataTable dt = TextUtils.LoadDataFromSP("spGetTranferAssetDetail", "A", new string[] { "@TranferAssetID" }, new object[] { masterID });
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    workSheet.Cells[21, 1] = i + 1;
                    workSheet.Cells[21, 2] = TextUtils.ToString(dt.Rows[i]["TSCodeNCC"]);
                    workSheet.Cells[21, 3] = TextUtils.ToString(dt.Rows[i]["TSAssetName"]);
                    workSheet.Cells[21, 5] = TextUtils.ToString(dt.Rows[i]["UnitName"]);
                    workSheet.Cells[21, 6] = TextUtils.ToString(dt.Rows[i]["Quantity"]);
                    workSheet.Cells[21, 7] = TextUtils.ToString(dt.Rows[i]["Status"]);
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


        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvTranferMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grdTranferMaster_Click(object sender, EventArgs e)
        {

        }

        // phúc sửa nút: xóa, duyệt, hủy duyệt
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));
            bool isApprovedAccountant = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproveAccountant));
            string receiverName = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colReceiverName));

            if (id <= 0)
            {
                return;
            }
            TSTranferAssetModel tranferAssetModel = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
            if (isApproved)
            {
                MessageBox.Show($"Biên bản bàn giao [{code}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (isApprovedAccountant)
            {
                MessageBox.Show($"Biên bản bàn giao  [{code}] đã được kế toán duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (tranferAssetModel.IsApprovedPersonalProperty)
            {
                MessageBox.Show($"Biên bản bàn giao  [{code}] đã NV [{receiverName}] duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                DialogResult dialog = MessageBox.Show($"Bạn có thực sự muốn xoá biên bản [{code}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    TSTranferAssetBO.Instance.Delete(id);
                    TSTranferAssetDetailBO.Instance.DeleteByAttribute("TSTranferAssetID", id);
                    grvTranferMaster.DeleteSelectedRows();
                }
            }

        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvTranferMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));
            string receiverName = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colReceiverName));

            if (id <= 0)
            {
                return;
            }
            //TSTranferAssetModel tSTranfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
            TSTranferAssetModel tSTranfer = SQLHelper<TSTranferAssetModel>.FindByID(id);

            if (isApprove)
            {
                MessageBox.Show($"Biên bản [{code}] đã được duyệt!", "Thông báo");
                return;
            }
            else if (!tSTranfer.IsApprovedPersonalProperty)
            {
                MessageBox.Show($"Biên bản [{code}] chưa được NV [{receiverName}] duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    tSTranfer.IsApproved = true;
                    tSTranfer.DateApprovedHR = DateTime.Now;
                    //TSTranferAssetBO.Instance.Update(tSTranfer);
                    SQLHelper<TSTranferAssetModel>.Update(tSTranfer);
                    approved(true);

                    grvTranferMaster.SetFocusedRowCellValue(colIsApproved, true);
                    //loadData();
                    grvTranferMaster.FocusedRowHandle = rowHanlde;
                }
            }

        }

        private void btnUnApproved_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvTranferMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));

            bool isApprovedAccountant = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproveAccountant));
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
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn huỷ duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    TSTranferAssetModel tSTranfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
                    tSTranfer.IsApproved = false;
                    tSTranfer.DateApprovedHR = DateTime.Now;
                    TSTranferAssetBO.Instance.Update(tSTranfer);
                    approved(false);

                    //loadData();
                    grvTranferMaster.SetFocusedRowCellValue(colIsApproved, false);
                    grvTranferMaster.FocusedRowHandle = rowHanlde;
                }

            }
        }
        //end

        void approved(bool isAppoved)
        {
            //Nếu mà duyệt phiếu
            //1.Update tên nhân viên ở bảng tài sản thành giá trị cột người nhận
            //2.Insert vào bảng TSAllocationEvictionAsset có trạng thái là đang sử dụng và nhân viên là giá trị cột người nhận
            //3.Update bảng TSAllocationEvictionAsset thành trạng thái đã điều chuyển, và ghi chú điều chuyển cho ai

            //Nếu mà huỷ duyệt phiếu
            //1.Update tên nhân viên ở bảng tài sản thành giá trị cột người điều chuyển
            //2.Insert vào bảng TSAllocationEvictionAsset có trạng thái là đang sử dụng và nhân viên là giá trị cột người điều chuyển
            //3.Update bảng TSAllocationEvictionAsset thành trạng thái đã điều chuyển, và ghi chú điều chuyển cho ai

            int deliverID = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colDeliverID));
            int receiverID = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colReceiverID));

            string deliver = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colDeliverName));
            string receiver = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colReceiverName));



            if (isAppoved)
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int assetManagementID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAssetManagementID));

                    //TSAssetManagementModel tSAsset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(assetManagementID);
                    TSAssetManagementModel tSAsset = SQLHelper<TSAssetManagementModel>.FindByID(assetManagementID);
                    tSAsset.EmployeeID = receiverID;

                    //TSAssetManagementBO.Instance.Update(tSAsset);
                    SQLHelper<TSAssetManagementModel>.Update(tSAsset);

                    TSAllocationEvictionAssetModel tSAllocation = SQLHelper<TSAllocationEvictionAssetModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TSAllocationEvictionAsset WHERE AssetManagementID = {assetManagementID} ORDER BY ID DESC");

                    tSAllocation.Status = "Đã điều chuyển";
                    tSAllocation.Note = "Đã điều chuyển cho " + receiver;

                    //TSAllocationEvictionAssetBO.Instance.Update(tSAllocation);
                    SQLHelper<TSAllocationEvictionAssetModel>.Update(tSAllocation);

                    //Insert thêm dòng mời

                    tSAllocation = new TSAllocationEvictionAssetModel();
                    tSAllocation.AssetManagementID = assetManagementID;
                    tSAllocation.EmployeeID = receiverID;
                    tSAllocation.Status = "Đang sử dụng";
                    tSAllocation.Note = "Được điều chuyển từ " + deliver;
                    tSAllocation.DateAllocation = TextUtils.ToDate4(grvTranferMaster.GetFocusedRowCellValue(colTranferDate));

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
                    tSAsset.EmployeeID = deliverID;

                    //TSAssetManagementBO.Instance.Update(tSAsset);
                    SQLHelper<TSAssetManagementModel>.Update(tSAsset);

                    TSAllocationEvictionAssetModel tSAllocation = SQLHelper<TSAllocationEvictionAssetModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TSAllocationEvictionAsset WHERE AssetManagementID = {assetManagementID} ORDER BY ID DESC");

                    tSAllocation.Status = "Đã điều chuyển";
                    tSAllocation.Note = "Đã điều chuyển cho " + deliver;

                    //TSAllocationEvictionAssetBO.Instance.Update(tSAllocation);
                    SQLHelper<TSAllocationEvictionAssetModel>.Update(tSAllocation);

                    //Insert thêm dòng mời

                    tSAllocation = new TSAllocationEvictionAssetModel();
                    tSAllocation.AssetManagementID = assetManagementID;
                    tSAllocation.EmployeeID = deliverID;
                    tSAllocation.Status = "Đang sử dụng";
                    tSAllocation.Note = "Được điều chuyển từ " + receiver;
                    tSAllocation.DateAllocation = TextUtils.ToDate4(grvTranferMaster.GetFocusedRowCellValue(colTranferDate));

                    //TSAllocationEvictionAssetBO.Instance.Insert(tSAllocation);
                    SQLHelper<TSAllocationEvictionAssetModel>.Insert(tSAllocation);

                }
            }

        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
            loadDataDetail();
        }

        private void cboDeliver_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
            loadDataDetail();
        }

        private void cboReceiver_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
            loadDataDetail();
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
                Clipboard.SetText(TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(grvTranferMaster.FocusedColumn)));
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

        //phúc thêm nút kế toán duyệt, hủy duyệt
        private void btnIsApproveAccountant_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvTranferMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            bool isApprove = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));
            bool isApproveAccountant = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproveAccountant));

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
                MessageBox.Show($"Biên bản [{code}] Đã được kế toán duyệt!", "Thông báo");
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    TSTranferAssetModel tSTranfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
                    tSTranfer.IsApproveAccountant = true;
                    tSTranfer.DateApproveAccountant = DateTime.Now;
                    TSTranferAssetBO.Instance.Update(tSTranfer);
                    grvTranferMaster.SetFocusedRowCellValue(colIsApproveAccountant, true);
                    grvTranferMaster.FocusedRowHandle = rowHanlde;
                }
            }
        }

        private void btnUnIsApproveAccountant_Click(object sender, EventArgs e)
        {
            int rowHanlde = grvTranferMaster.FocusedRowHandle;

            int id = TextUtils.ToInt(grvTranferMaster.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvTranferMaster.GetFocusedRowCellValue(colCodeReport));

            bool isApprovedAccountant = TextUtils.ToBoolean(grvTranferMaster.GetFocusedRowCellValue(colIsApproveAccountant));
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
                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn huỷ duyệt biên bản [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    TSTranferAssetModel tSTranfer = (TSTranferAssetModel)TSTranferAssetBO.Instance.FindByPK(id);
                    tSTranfer.IsApproveAccountant = false;
                    tSTranfer.DateApproveAccountant = DateTime.Now;
                    TSTranferAssetBO.Instance.Update(tSTranfer);

                    grvTranferMaster.SetFocusedRowCellValue(colIsApproveAccountant, false);
                    grvTranferMaster.FocusedRowHandle = rowHanlde;
                }
            }
        }

        private void frmTranferAssetMaster_Activated(object sender, EventArgs e)
        {
            RefresData();
        }
        //end
    }
}