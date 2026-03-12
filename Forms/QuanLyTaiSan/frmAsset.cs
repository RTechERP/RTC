using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Forms.Enums;
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
    public partial class frmAsset : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        List<int> listIdSelected = new List<int>();
        public frmAsset()
        {
            InitializeComponent();
        }

        #region Load Data
        private void frmAsset_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            LoadcboTinhTrang();
            LoadchkPhongBan();
            //cbTinhTrang.CheckAll();
            //chkPhongBan.CheckAll();
            LoadTSAssetManagement();
            LoadTSAllocationAsset();

            LoadEmployee();

            label6.Visible = cboEmployee.Visible = btnUpdateEmployee.Visible = Global.IsAdmin;
        }
        void LoadTSAssetManagement()
        {
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 00, 00, 00);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            string status = TextUtils.ToString(cbTinhTrang.EditValue);
            string department = TextUtils.ToString(chkPhongBan.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spLoadTSAssetManagement", "A",
                new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@Department" },
                new object[] { txtSearch.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, status, department });

            dt.Columns.Add("selected", typeof(bool));

            grdMaster.DataSource = dt;

            foreach (int item in listIdSelected)
            {
                int rowHandle = grvMaster.LocateByValue("ID", item);

                grvMaster.SetRowCellValue(rowHandle, colSelected, true);
            }

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }
        void LoadTSAllocationAsset()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spLoadTSAllocationEvictionAsset", "A", new string[] { "@ID" }, new object[] { ID });
            grdDetail.DataSource = dt;
        }
        void LoadcboTinhTrang()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.TSStatusAsset");
            cbTinhTrang.Properties.DataSource = dt;
            cbTinhTrang.Properties.DisplayMember = "Status";
            cbTinhTrang.Properties.ValueMember = "ID";

            cbTinhTrang.CheckAll();
        }
        void LoadchkPhongBan()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.Department");
            chkPhongBan.Properties.DataSource = dt;
            chkPhongBan.Properties.DisplayMember = "Name";
            chkPhongBan.Properties.ValueMember = "ID";

            chkPhongBan.CheckAll();
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            //cboEmployee.EditValue = Global.EmployeeID;
        }

        #endregion

        #region Button
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmAssetDetail frm = new frmAssetDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTSAssetManagement();
                LoadTSAllocationAsset();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
            TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
            frmAssetDetail frm = new frmAssetDetail();
            frm.asset = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTSAssetManagement();
                LoadTSAllocationAsset();
                grvMaster.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
            if (ID == 0) return;
            DataTable dtAsset = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
            int id = TextUtils.ToInt(dtAsset.Rows[0]["StatusID"]);
            if (id == 2)
            {
                MessageBox.Show(string.Format("Tài sản [ {0} ] không thể xóa được !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtAlocation = TextUtils.Select($"Select TOP 1 Status from dbo.TSAllocationEvictionAsset where AssetManagemnetID = '{ID}' ORDER BY ID DESC ");
            if (dtAlocation.Rows.Count == 0)
            {

            }
            else
            {
                string status = TextUtils.ToString(dtAlocation.Rows[0]["Status"]);
                if (status == "Đang sử dụng")
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] không thể xóa được !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle)) return;

            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa tài sản [ {0} ] không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //TSAssetManagementBO.Instance.Delete(ID);
                //TSAllocationEvictionAssetBO.Instance.DeleteByAttribute("AssetManagementID", strID);
                SQLHelper<TSAssetManagementModel>.DeleteModelByID(ID);
                SQLHelper<TSAllocationEvictionAssetModel>.DeleteByAttribute("AssetManagementID", strID);

                grvMaster.DeleteSelectedRows();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
                LoadTSAllocationAsset();
                LoadTSAssetManagement();
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            //int focusedRowHandle = grvMaster.FocusedRowHandle;
            //grvMaster.FocusedRowHandle = focusedRowHandle - 1;
            LoadTSAssetManagement();
            LoadTSAllocationAsset();

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);

        }

        private void btnOutputExcel_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachTaiSan_{dtpStartDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdMaster;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdData;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);
                        //compositeLink.Links.Add(printableComponentLink2);

                        //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            //    optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
            //    grvMaster.OptionsPrint.PrintSelectedRowsOnly = false;
            //    try
            //    {
            //        string filepath = $"{f.SelectedPath}/DanhSachTaiSan_{dtpStartDate.Value.ToString("ddMMYY")}_{dtpEndDate.Value.ToString("ddMMYY")}.xls";
            //        grvMaster.ExportToXls(filepath, optionsEx);

            //        Process.Start(filepath);

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //        //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
            //    }
            //    grvMaster.ClearSelection();
            //}
        }

        private void btnCapPhat_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
                if (id != 1)
                {
                    if (id == 2)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đang được sử dụng. Không thể cấp phát !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 3 || id == 5)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đang bị hỏng. Không thể cấp phát !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 4)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã mất. Không thể cấp phát !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 6)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã bị thanh lý. Không thể cấp phát !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                    TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                    frmAllocationAsset frm = new frmAllocationAsset();
                    frm.asset = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadTSAssetManagement();
                        LoadTSAllocationAsset();
                        grvMaster.FocusedRowHandle = focusedRowHandle;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
        }


        private void btnThuHoi_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                var focusedRowHandle1 = grvDetail.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dtAsset = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                DataTable dtAlocation = TextUtils.Select($"Select TOP 1 Status from dbo.TSAllocationEvictionAsset where AssetManagementID = '{ID}' ORDER BY ID DESC");

                int id = TextUtils.ToInt(dtAsset.Rows[0]["StatusID"]);
                if (id != 2 && id != 5 && id != 7)
                {
                    if (id == 1)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] chưa được sử dụng. Không thể thu hồi !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 3 || id == 5)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đang bị hỏng. Không thể thu hồi !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 4)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã mất. Không thể thu hồi !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 6)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể thu hồi !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dtAlocation.Rows.Count == 0)
                {
                }
                else
                {
                    string status = TextUtils.ToString(dtAlocation.Rows[0]["Status"]);
                    if (status != "Đang sử dụng")
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] không có người sử dụng. Không thể thu hồi !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                frmEvictionAsset frm = new frmEvictionAsset();
                frm.asset = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTSAssetManagement();
                    LoadTSAllocationAsset();
                    grvMaster.FocusedRowHandle = focusedRowHandle;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
        }


        private void btnBaoMat_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
                if (id == 4)
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] đã báo mất !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else
                {
                    if (id != 1 && id != 2 && id != 5 && id != 3 && id != 7)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể báo mất !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                        TSAssetManagementModel model = SQLHelper< TSAssetManagementModel>.FindByID(ID);
                        frmLostReportAsset frm = new frmLostReportAsset();
                        frm.asset = model;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadTSAssetManagement();
                            LoadTSAllocationAsset();
                            grvMaster.FocusedRowHandle = focusedRowHandle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
        }

        private void btnBaoHong_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
                if (id != 1 && id != 2 && id != 7)
                {
                    if (id == 5)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã báo hỏng !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 3)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đang được sửa chữa, bảo dưỡng. Không thể báo hỏng !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 4)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã mất. Không thể báo hỏng !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 6)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể báo hỏng !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                    TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                    frmReportBrokenAsset frm = new frmReportBrokenAsset();
                    frm.asset = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadTSAssetManagement();
                        LoadTSAllocationAsset();
                        grvMaster.FocusedRowHandle = focusedRowHandle;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }


        private void btnSuaChua_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
                if (id != 1 && id != 2 && id != 5 && id != 7)
                {
                    if (id == 3)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đang được sửa chữa, bảo dưỡng. Không thể báo tiếp !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 4)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã mất. Không thể báo sửa chữa, bảo dưỡng !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (id == 6)
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể báo sửa chữa, bảo dưỡng !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                frmRepairMantain frm = new frmRepairMantain();
                frm.asset = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTSAssetManagement();
                    LoadTSAllocationAsset();
                    grvMaster.FocusedRowHandle = focusedRowHandle;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnSuDungLai_Click(object sender, EventArgs e)
        {
            try
            {
                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dtasset = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                int idstatus = TextUtils.ToInt(dtasset.Rows[0]["StatusID"]);
                if (idstatus == 1)
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] chưa được sử dụng. Không thể đưa vào sử dụng lại !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (idstatus == 2)
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] đang được sử dụng. Không thể đưa vào sử dụng lại !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (idstatus == 6)
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể đưa vào sử dụng lại !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable dt = TextUtils.Select($"Select TOP 1 Status from dbo.TSAllocationEvictionAsset where AssetManagementID = '{ID}' ORDER BY ID DESC");
                    if (dt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        string status = TextUtils.ToString(dt.Rows[0]["Status"]);
                        if (status == "Đang sử dụng")
                        {
                            MessageBox.Show(string.Format("Tài sản [ {0} ] đang có người sử dụng. Không thể đưa vào sử dụng lại !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                frmReuseAsset frm = new frmReuseAsset();
                frm.asset = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTSAssetManagement();
                    LoadTSAllocationAsset();
                    grvMaster.FocusedRowHandle = focusedRowHandle;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnDeNghiThanhLy_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
            if (ID == 0) return;
            DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
            int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
            if (id == 4)
            {
                MessageBox.Show(string.Format("Tài sản [ {0} ] đã mất. Không thể đề nghị thanh lý !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (id == 6)
            {
                MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể đề nghị thanh lý !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (id == 7)
            {
                MessageBox.Show(string.Format("Tài sản [ {0} ] đã được đề nghị thanh lý !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
            TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
            frmProposeLiquidationAsset frm = new frmProposeLiquidationAsset();
            frm.asset = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTSAssetManagement();
                LoadTSAllocationAsset();
                grvMaster.FocusedRowHandle = focusedRowHandle;
            }
        }


        private void btnThanhLy_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
            if (ID == 0) return;
            DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
            int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
            if (id != 7)
            {
                MessageBox.Show(string.Format("Tài sản [ {0} ] chưa được đề nghị thanh lý nên không thể thanh lý !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (id == 6)
            {
                MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DataTable Is = TextUtils.Select($"Select TOP 1 IsApproved from dbo.TSLiQuidationAsset where AssetManagementID = '{ID}' ");
                bool IsApproved = TextUtils.ToBoolean(Is.Rows[0]["IsApproved"]);
                if (IsApproved == true)
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] không thể thanh lý vì đã được duyệt !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                    TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                    frmLiquidation frm = new frmLiquidation();
                    frm.asset = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadTSAssetManagement();
                        LoadTSAllocationAsset();
                        grvMaster.FocusedRowHandle = focusedRowHandle;
                    }
                }
            }
        }

        private void btnDieuChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedId = listIdSelected;

                var focusedRowHandle = grvMaster.FocusedRowHandle;
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colTSCodeNCC));
                if (ID == 0) return;
                DataTable dtstatus = TextUtils.Select($"Select TOP 1 Status from dbo.TSAllocationEvictionAsset where AssetManagementID = '{ID}' ORDER BY ID DESC ");
                if (dtstatus.Rows.Count == 0)
                {
                    MessageBox.Show(string.Format("Tài sản [ {0} ] không thể điều chuyển !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataTable dt = TextUtils.Select($"Select TOP 1 StatusID from dbo.TSAssetManagement where ID = '{ID}' ");
                    int id = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
                    if (id != 2 && id != 7 && id != 5)
                    {
                        if (id == 3)
                        {
                            MessageBox.Show(string.Format("Tài sản [ {0} ] đang sửa chữa, bảo dưỡng. Không thể điều chuyển !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (id == 4)
                        {
                            MessageBox.Show(string.Format("Tài sản [ {0} ] đã mất. Không thể điều chuyển !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (id == 6)
                        {
                            MessageBox.Show(string.Format("Tài sản [ {0} ] đã thanh lý. Không thể điều chuyển !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    string status = TextUtils.ToString(dtstatus.Rows[0]["Status"]);
                    if (status != "Đang sử dụng")
                    {
                        MessageBox.Show(string.Format("Tài sản [ {0} ] đang không được sử dụng. Không thể điều chuyển !", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //TSAssetManagementModel model = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(ID);
                TSAssetManagementModel model = SQLHelper<TSAssetManagementModel>.FindByID(ID);
                frmTranferAssetDetail frm = new frmTranferAssetDetail();
                frm.asset = model;


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTSAssetManagement();
                    LoadTSAllocationAsset();
                    grvMaster.FocusedRowHandle = focusedRowHandle;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format($"{ex}"));
            }

        }

        private void btnNhapKhau_Click(object sender, EventArgs e)
        {
            frmAssetExportExcel frm = new frmAssetExportExcel();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTSAllocationAsset();
                LoadTSAssetManagement();
            }
        }
        private void cấpPhátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCapPhat_Click(null, null);
        }

        private void thuHồiTàiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThuHoi_Click(null, null);
        }

        private void bóaMấtTàiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBaoMat_Click(null, null);
        }

        private void báoHỏngTàiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBaoHong_Click(null, null);
        }

        private void sửaChữaBảoDưỡngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSuaChua_Click(null, null);
        }

        private void đưaVàoSửDụngLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSuDungLai_Click(null, null);
        }

        private void điềuChuyểnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDieuChuyen_Click(null, null);
        }

        private void đềNghịThanhLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDeNghiThanhLy_Click(null, null);
        }

        private void thanhLýTàiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnThanhLy_Click(null, null);
        }
        private void sửaTàiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void xóaTàiSảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDelete_Click(null, null);
        }
        #endregion
        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadTSAllocationAsset();

            //int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //for (int i = 0; i <= id; i++)
            //{
            //    if (id > 0)
            //    {
            // LoadTSAllocationAsset();
            //    }
            //}
        }


        private void grvMaster_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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


                //if (e.CellValue == null)
                //    return;
                //if (TextUtils.ToString(e.CellValue) == "Mất")
                //    e.Appearance.BackColor = Color.Red;
                //if (TextUtils.ToString(e.CellValue) == "Chưa sử dụng")
                //    e.Appearance.BackColor = Color.Lime;
                //if (TextUtils.ToString(e.CellValue) == "Hỏng")
                //    e.Appearance.BackColor = Color.Yellow;
                //if (TextUtils.ToString(e.CellValue) == "Sửa chữa, Bảo dưỡng")
                //    e.Appearance.BackColor = Color.Yellow;
                //if (TextUtils.ToString(e.CellValue) == "Đã thanh lý")
                //    e.Appearance.BackColor = Color.Red;
                //if (TextUtils.ToString(e.CellValue) == "Đề nghị thanh lý")
                //    e.Appearance.BackColor = Color.Aqua; 
            }

            //bool selected = TextUtils.ToBoolean(grvMaster.GetRowCellValue(e.RowHandle, colSelected));
            //if (selected)
            //{
            //    e.Appearance.BackColor = Color.FromArgb(192, 192, 255);
            //}
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
        private void grvDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            e.Appearance.BackColor = Color.White;
            if (e.Column == colStatuss)
            {
                if (e.CellValue == null)
                    return;
                if (TextUtils.ToString(e.CellValue) == "Đã điều chuyển")
                    e.Appearance.BackColor = Color.Orange;
                if (TextUtils.ToString(e.CellValue) == "Đang sử dụng")
                    e.Appearance.BackColor = Color.Lime;
                if (TextUtils.ToString(e.CellValue) == "Hỏng")
                    e.Appearance.BackColor = Color.Yellow;
                if (TextUtils.ToString(e.CellValue) == "Sửa chữa, Bảo dưỡng")
                    e.Appearance.BackColor = Color.Yellow;
                if (TextUtils.ToString(e.CellValue) == "Mất")
                    e.Appearance.BackColor = Color.Red;
                if (TextUtils.ToString(e.CellValue) == "Đã thanh lý")
                    e.Appearance.BackColor = Color.Red;
                if (TextUtils.ToString(e.CellValue) == "Đề nghị thanh lý")
                    e.Appearance.BackColor = Color.Aqua;
            }
        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(grvMaster.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void cbTinhTrang_EditValueChanged(object sender, EventArgs e)
        {
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void chkPhongBan_EditValueChanged(object sender, EventArgs e)
        {
            LoadTSAssetManagement();
            LoadTSAllocationAsset();
        }

        private void grvMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                grvMaster.CloseEditor();
                if (e.Column == colSelected)
                {

                    bool selected = TextUtils.ToBoolean(grvMaster.GetRowCellValue(e.RowHandle, colSelected));
                    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colID));
                    if (selected)
                    {
                        var match = listIdSelected.Contains(id);
                        if (!match)
                        {
                            listIdSelected.Add(id);
                        }

                    }
                    else
                    {
                        listIdSelected.Remove(id);
                    }
                }

            }
        }


        private void grvMaster_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetRowCellValue(e.ControllerRow, colID));

            if (e.Action == CollectionChangeAction.Add)
            {
                var matchValue = listIdSelected.Contains(id);
                if (!matchValue)
                {
                    listIdSelected.Add(id);
                }
            }
            else
            {
                listIdSelected.Remove(id);
            }
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int[] rowSelected = grvMaster.GetSelectedRows();

                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn cập nhật Người sử dụng cho danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    foreach (int row in rowSelected)
                    {
                        int id = TextUtils.ToInt(grvMaster.GetRowCellValue(row, "ID"));
                        if (id <= 0) continue;

                        var myDict = new Dictionary<string, object>()
                        {
                            {"EmployeeID",TextUtils.ToInt(cboEmployee.EditValue) },
                            {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                            {"UpdatedBy",Global.LoginName },
                        };
                        SQLHelper<TSAssetManagementModel>.UpdateFieldsByID(myDict, id);
                    }

                    btnFind_Click(null, null);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void grvMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
        }
    }
}
