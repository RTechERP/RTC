using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmFollowProjectBase : _Forms
    {
        int warehouseID = 0;
        public frmFollowProjectBase(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmFollowProjectBase_Load(object sender, EventArgs e)
        {
            //WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            this.Text += $" - {this.Tag}";

            DateTime datenow = new DateTime(2019, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow;
            txtPageNumber.Text = "1";
            //cbUser.EditValue = Global.UserID;

            //// phân quyền admin
            //DataTable dt = TextUtils.Select($"Select * From [GroupSalesUser] WHERE UserID = {cbUser.EditValue} AND (SaleUserTypeID = 1 OR SaleUserTypeID = 6 OR SaleUserTypeID = 7 OR SaleUserTypeID = 8)");
            //if (dt.Rows.Count > 0) cbUser.Enabled = true;
            //else cbUser.Enabled = false;

            loadUser();
            loadCustomerBase();
            LoadEmployee();
            loadData();
            LoadGroupSales(); //LinhTN update 06/08/2024

            //if (GetUserSale() == 1)
            //{
            //    cbTeam.Enabled = cbUser.Enabled = cboEmployee.Enabled = true; //LinhTN update 06 / 08 / 2024
            //}
            //else if (GetUserSale() == 2)
            //{
            //    cbTeam.Enabled = cbUser.Enabled = true;//LinhTN update 06 / 08 / 2024
            //    cboEmployee.Enabled = false;
            //}
            //else if (GetUserSale() == 3)
            //{
            //    cbTeam.Enabled = cbUser.Enabled = false;//LinhTN update 06 / 08 / 2024
            //    cboEmployee.Enabled = true;
            //}
            //else
            //{
            //    cbTeam.Enabled = cbUser.Enabled = cboEmployee.Enabled = false;//LinhTN update 06 / 08 / 2024
            //}

            //if (GetUserSale() == 1)
            //{
            //    cbTeam.Enabled = cbUser.Enabled = cboEmployee.Enabled = true; //LinhTN update 06/08/2024
            //}
            //else if (GetUserSale() == 2)
            //{
            //    cbUser.Enabled = true;
            //    cbTeam.Enabled = cboEmployee.Enabled = false; //LinhTN update 06/08/2024
            //}
            //else if (GetUserSale() == 3)
            //{
            //    cbTeam.Enabled = cbUser.Enabled = false; //LinhTN update 06/08/2024
            //    cboEmployee.Enabled = true;
            //}
            //else
            //{
            //    cbTeam.Enabled = cbUser.Enabled = cboEmployee.Enabled = false; //LinhTN update 06/08/2024
            //}


            splitContainerControl2.SplitterPosition = splitContainerControl2.Width / 2;
        }

        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        private void loadCustomerBase()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Customer");
            cbCustomerBase.Properties.DisplayMember = "CustomerName";
            cbCustomerBase.Properties.ValueMember = "ID";
            cbCustomerBase.Properties.DataSource = dt;
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        private void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

            if (GetUserSale() == 3 || GetUserSale() == 0)
            {
                cbUser.EditValue = Global.UserID;
            }
        }


        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A",
                                                    new string[] { "@Status" },
                                                    new object[] { -1 });

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            if (GetUserSale() == 2 || GetUserSale() == 0)
            {
                cboEmployee.EditValue = Global.EmployeeID;
            }
        }

        private void loadData()
        {
            AutoInsertFollowProjectBySale();

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            int userid = TextUtils.ToInt(cbUser.EditValue);
            int customerid = TextUtils.ToInt(cbCustomerBase.EditValue);
            string keyword = txtFilterText.Text.Trim();
            int pm = TextUtils.ToInt(cboEmployee.EditValue);
            int groupSaleID = TextUtils.ToInt(cbTeam.EditValue); //LinhTN update 06/08/2024
            if (GetUserSale() == 2 || GetUserSale() == 0)
            {
                pm = 0;
            }

            //LinhTN update 06/08/2024
            DataTable dt = TextUtils.LoadDataFromSP("spGetFollowProjectBase", "A"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@User", "@CustomerID", "@PM", "@WarehouseID", "@GroupSaleID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), txtPageSize.Value, dateTimeS, dateTimeE, keyword, userid, customerid, pm, warehouseID, groupSaleID });
            grdData.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);

            LoadDetail();
        }


        void LoadDetail()
        {
            //DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            int followProjectBaseID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetFollowProjectBaseDetail"
                                                            , new string[] { "@FollowProjectBaseID", "@ProjectID" }
                                                            , new object[] { followProjectBaseID, projectID });
            DataTable dtSale = dataSet.Tables[0];
            DataTable dtPM = dataSet.Tables[1];

            grdDataSale.DataSource = dtSale;
            grdDataPM.DataSource = dtPM;
        }

        //LinhTN update 06/08/2024 - start
        void LoadGroupSales()
        {
            int groupID = 0;
            int teamID = 0;
            GroupSalesUserModel model = SQLHelper<GroupSalesUserModel>.FindByAttribute("UserID", Global.UserID).FirstOrDefault();
            model = model ?? new GroupSalesUserModel();
            if (model.ID > 0) groupID = model.GroupSalesID;

            if (model.ParentID == 0) teamID = model.ID;
            else
            {
                teamID = model.ParentID;
            }

            if (Global.IsAdminSale || Global.IsAdmin)
            {
                groupID = 0;
                teamID = 0;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeManager", "A", new string[] { "@group", "@teamID" }, new object[] { groupID, teamID });
            cbTeam.Properties.ValueMember = "ID";
            cbTeam.Properties.DisplayMember = "FullName";
            cbTeam.Properties.DataSource = dt;
            cbTeam.Properties.AutoExpandAllNodes = true;

            if (model.ParentID == 0) cbTeam.EditValue = model.ID;
            else
            {
                var row = dt.AsEnumerable().Where(x => x.Field<int>("ID") == model.ParentID).Select(x => x.Field<int>("ID")).ToList();
                cbTeam.EditValue = TextUtils.ToInt(row.FirstOrDefault());
            }

            cbTeam.Enabled = Global.IsAdminSale;
        }
        //LinhTN update 06/08/2024 - end
        #endregion

        /// <summary>
        /// cliick add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FollowProjectBaseModel model = new FollowProjectBaseModel();
            frmFollowProjectBaseDetail frm = new frmFollowProjectBaseDetail(warehouseID);
            frm.followProjectBase = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        /// <summary>
        /// fix tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            editDataProduct();
        }
        /// <summary>
        /// void edit data
        /// </summary>
        private void editDataProduct()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            FollowProjectBaseModel model = (FollowProjectBaseModel)FollowProjectBaseBO.Instance.FindByPK(ID);
            frmFollowProjectBaseDetail frm = new frmFollowProjectBaseDetail(warehouseID);
            frm.followProjectBase = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        /// <summary>
        /// delete sản phẩm khỏi danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string firmCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectProjectName));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa có mã: {0} không?", firmCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FollowProjectBaseBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
            }
        }
        /// <summary>
        /// event editData by doubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            editDataProduct();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //	grvData.OptionsPrint.AutoWidth = false;
            //	grvData.OptionsPrint.ExpandAllDetails = false;
            //	grvData.OptionsPrint.PrintDetails = true;
            //	grvData.OptionsPrint.UsePrintStyles = true;
            //	try
            //	{
            //		grvData.ExportToXls(sfd.FileName);
            //		Process.Start(sfd.FileName);
            //	}
            //	catch (Exception)
            //	{
            //	}
            //}


            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    string filepath = Path.Combine(f.SelectedPath, $"DanhSachFollowDuAn_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx");
            //    //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

            //    XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
            //    PrintingSystem printingSystem = new PrintingSystem();

            //    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
            //    printableComponentLink1.Component = grdData;
            //    try
            //    {
            //        CompositeLink compositeLink = new CompositeLink(printingSystem);
            //        compositeLink.Links.Add(printableComponentLink1);


            //        compositeLink.CreatePageForEachLink();
            //        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

            //        compositeLink.PrintingSystem.SaveDocument(filepath);
            //        compositeLink.ExportToXlsx(filepath, optionsEx);
            //        Process.Start(filepath);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}

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

            string sourcePath = Path.Combine(Application.StartupPath, "TemplateFollowProjectBase.xls");
            //string projectCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectCode));
            //string currentPath = Path.Combine(path, $"FollowProject_{DateTime.Now.ToString("ddMMyy")}.xls");
            string fileNameElement = "";
            if (!string.IsNullOrEmpty(cbUser.Text.Trim()))
            {
                fileNameElement = cbUser.Text.Trim();
            }
            else if (!string.IsNullOrEmpty(cboEmployee.Text.Trim()))
            {
                fileNameElement = cboEmployee.Text.Trim();
            }
            else if (!string.IsNullOrEmpty(cbCustomerBase.Text.Trim()))
            {
                fileNameElement = cbCustomerBase.Text.Trim();
            }

            string currentPath = Path.Combine(path, $"FollowProject_{fileNameElement}_{DateTime.Now.ToString("ddMMyy")}.xlsx");
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                int followProjectBaseID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                int userId = TextUtils.ToInt(cbUser.EditValue);
                int customerId = TextUtils.ToInt(cbCustomerBase.EditValue);
                int pm = TextUtils.ToInt(cboEmployee.EditValue);
                string filterText = txtFilterText.Text.Trim();

                DataTable dt = TextUtils.LoadDataFromSP("spGetFollowProjectBaseExport", "A",
                                                        new string[] { "@FollowProjectBaseID", "@ProjectID", "@UserID", "@CustomerID", "@PM", "@WarehouseID", "@FilterText" },
                                                        new object[] { followProjectBaseID, projectID, userId, customerId, pm, warehouseID, filterText });
                grdDataExport.DataSource = dt;

                grvDataExport.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvDataExport_CellMerge);
                try
                {
                    //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoAnCa_T{txtMonth.Text}_{txtYear.Value}.xlsx");

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdDataExport;

                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.AddRange(new object[] { printableComponentLink1 });

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(currentPath);
                    compositeLink.ExportToXlsx(currentPath, optionsEx);
                    Process.Start(currentPath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }


                //Excel.Application app = default(Excel.Application);
                //Excel.Workbook workBoook = default(Excel.Workbook);
                //Excel.Worksheet workSheet = default(Excel.Worksheet);
                //try
                //{
                //    app = new Excel.Application();
                //    app.Workbooks.Open(currentPath);
                //    workBoook = app.Workbooks[1];
                //    workSheet = (Excel.Worksheet)workBoook.Worksheets[1];

                //    //string totalName = TextUtils.ToString(dtMaster.Rows[0]["Code"]);

                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        DataRow row = dt.Rows[i];

                //        string projectCode = TextUtils.ToString(row["ProjectCode"]);
                //        DateTime? projectStartDate = TextUtils.ToDate4(row["ProjectStartDate"]);

                //        DateTime? expectedPlanDate = TextUtils.ToDate4(row["ExpectedPlanDate"]);
                //        DateTime? expectedQuotationDate = TextUtils.ToDate4(row["ExpectedQuotationDate"]);
                //        DateTime? expectedPODate = TextUtils.ToDate4(row["ExpectedPODate"]);
                //        DateTime? expectedProjectEndDate = TextUtils.ToDate4(row["ExpectedProjectEndDate"]);

                //        DateTime? realityPlanDate = TextUtils.ToDate4(row["RealityPlanDate"]);
                //        DateTime? realityQuotationDate = TextUtils.ToDate4(row["RealityQuotationDate"]);
                //        DateTime? realityPODate = TextUtils.ToDate4(row["RealityPODate"]);
                //        DateTime? realityProjectEndDate = TextUtils.ToDate4(row["RealityProjectEndDate"]);

                //        DateTime? implementationDateSale = TextUtils.ToDate4(row["ImplementationDateSale"]);
                //        DateTime? expectedDateSale = TextUtils.ToDate4(row["ExpectedDateSale"]);
                //        DateTime? implementationDatePM = TextUtils.ToDate4(row["ImplementationDatePM"]);
                //        DateTime? expectedDatePM = TextUtils.ToDate4(row["ExpectedDatePM"]);

                //        workSheet.Cells[4, 1] = projectCode;
                //        workSheet.Cells[4, 2] = TextUtils.ToString(row["ProjectName"]);
                //        workSheet.Cells[4, 3] = TextUtils.ToString(row["FullName"]);
                //        workSheet.Cells[4, 4] = TextUtils.ToString(row["ProjectManager"]);
                //        workSheet.Cells[4, 5] = TextUtils.ToString(row["CustomerName"]);
                //        workSheet.Cells[4, 6] = TextUtils.ToString(row["EndUser"]);
                //        workSheet.Cells[4, 7] = TextUtils.ToString(row["ProjectStatusName"]);
                //        workSheet.Cells[4, 8] = projectStartDate.HasValue ? projectStartDate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 9] = TextUtils.ToString(row["ProjectTypeName"]);
                //        workSheet.Cells[4, 10] = TextUtils.ToString(row["FirmName"]);
                //        workSheet.Cells[4, 11] = "";
                //        workSheet.Cells[4, 12] = "";
                //        workSheet.Cells[4, 13] = TextUtils.ToString(row["PossibilityPO"]);

                //        //Dự kiến
                //        workSheet.Cells[4, 14] = expectedPlanDate.HasValue ? expectedPlanDate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 15] = expectedQuotationDate.HasValue ? expectedQuotationDate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 16] = expectedPODate.HasValue ? expectedPODate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 17] = expectedProjectEndDate.HasValue ? expectedProjectEndDate.Value.ToString("dd/MM/yyyy") : "";

                //        //Thực tế
                //        workSheet.Cells[4, 18] = realityPlanDate.HasValue ? realityPlanDate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 19] = realityQuotationDate.HasValue ? realityQuotationDate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 20] = realityPODate.HasValue ? realityPODate.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 21] = realityProjectEndDate.HasValue ? realityProjectEndDate.Value.ToString("dd/MM/yyyy") : "";

                //        //Follow dự án
                //        workSheet.Cells[4, 22] = TextUtils.ToInt(row["TotalWithoutVAT"]).ToString("N0");
                //        workSheet.Cells[4, 23] = TextUtils.ToString(row["ProjectContactName"]);
                //        workSheet.Cells[4, 24] = TextUtils.ToString(row["Note"]);

                //        //Sale báo cáo
                //        workSheet.Cells[4, 25] = implementationDateSale.HasValue ? implementationDateSale.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 26] = TextUtils.ToString(row["WorkDoneSale"]);
                //        workSheet.Cells[4, 27] = expectedDateSale.HasValue ? expectedDateSale.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 28] = TextUtils.ToString(row["WorkWillDoSale"]);

                //        //PM báo cáo
                //        workSheet.Cells[4, 29] = implementationDatePM.HasValue ? implementationDatePM.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 30] = TextUtils.ToString(row["WorkDonePM"]);
                //        workSheet.Cells[4, 31] = expectedDatePM.HasValue ? expectedDatePM.Value.ToString("dd/MM/yyyy") : "";
                //        workSheet.Cells[4, 32] = TextUtils.ToString(row["WorkWillDoPM"]);

                //        ((Excel.Range)workSheet.Rows[4]).Insert();

                //        //if (i - 1 >= 0)
                //        //{
                //        //    string _code_Old = TextUtils.ToString(dt.Rows[i - 1]["ProjectCode"]);
                //        //    if (projectCode == _code_Old)
                //        //    {
                //        //        MergeExcel(workSheet, "A4", "A5");
                //        //        MergeExcel(workSheet, "B4", "B5");
                //        //        MergeExcel(workSheet, "C4", "C5");
                //        //        MergeExcel(workSheet, "D4", "D5");
                //        //        MergeExcel(workSheet, "E4", "E5");
                //        //        MergeExcel(workSheet, "F4", "F5");
                //        //        MergeExcel(workSheet, "G4", "G5");
                //        //        MergeExcel(workSheet, "H4", "H5");
                //        //        MergeExcel(workSheet, "I4", "I5");
                //        //        MergeExcel(workSheet, "J4", "J5");
                //        //        MergeExcel(workSheet, "K4", "K5");
                //        //        MergeExcel(workSheet, "L4", "L5");
                //        //        MergeExcel(workSheet, "M4", "M5");
                //        //    }
                //        //}
                //    }

                //    ((Excel.Range)workSheet.Rows[3]).Delete();
                //    ((Excel.Range)workSheet.Rows[3]).Delete();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //finally
                //{
                //    if (app != null)
                //    {
                //        app.ActiveWorkbook.Save();
                //        app.Workbooks.Close();
                //        app.Quit();
                //    }
                //}
                //Process.Start(currentPath);
            }
        }

        static void MergeExcel(Excel.Worksheet workSheet, string startRow, string endRow)
        {
            try
            {
                Excel.Range range = workSheet.Range[startRow, endRow];
                range.Merge();
            }
            catch { }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmFollowProjectBaseExcel frm = new frmFollowProjectBaseExcel();
            frm.ShowDialog();
            loadData();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbCustomerBase_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }


        int GetUserSale()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeManager", "A",
                                                    new string[] { "@UserID" }, new object[] { Global.UserID });


            var dataLeader = dt.Select("SaleUserTypeCode = 'LeadG'");
            var dataPM = dt.Select("SaleUserTypeCode = 'PM'");
            var dataSale = dt.Select("SaleUserTypeCode = 'Sta'");
            if (dataLeader.Length > 0 || Global.IsAdmin || Global.IsAdminSale)
            {
                return 1;
            }
            else if (dataPM.Length > 0)
            {
                return 2;
            }
            else if (dataSale.Length > 0)
            {
                return 3;
            }
            else
            {
                return 0;
            }

        }


        //Lt.Anh update 19/02/2024
        void AutoInsertFollowProjectBySale()
        {
            //Get dự án theo sale phụ trách
            int userId = TextUtils.ToInt(cbUser.EditValue);
            if (userId != Global.UserID) return;
            //MessageBox.Show("Start insert");
            List<ProjectModel> projects = SQLHelper<ProjectModel>.FindByAttribute("UserID", userId);

            //Kiểm tra dự án đã có ở Follow chưa -->Chưa có thì thêm mới
            foreach (ProjectModel project in projects)
            {
                var exp1 = new Expression("ProjectID", project.ID);
                var exp2 = new Expression("UserID", userId);
                FollowProjectBaseModel follow = SQLHelper<FollowProjectBaseModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                follow = follow == null ? new FollowProjectBaseModel() : follow;
                if (follow.ID > 0) continue;
                follow.ProjectID = project.ID;
                follow.UserID = project.UserID;
                follow.ProjectStatusBaseID = project.ProjectStatus;
                follow.CustomerBaseID = project.CustomerID;
                follow.EndUserID = project.EndUser;
                follow.ProjectStartDate = project.CreatedDate;
                follow.WarehouseID = warehouseID;
                SQLHelper<FollowProjectBaseModel>.Insert(follow);
            }
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void grvDataExport_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string checkValue = TextUtils.ToString(grvDataExport.GetRowCellValue(e.RowHandle1, "ProjectCode"));
            //bool allowMerge = TextUtils.ToBoolean(e.Column.OptionsColumn.AllowMerge);

            //bool isFollow = gridBand7.Columns.Contains((BandedGridColumn)e.Column);
            //bool isPlan = gridBand8.Columns.Contains((BandedGridColumn)e.Column);
            //bool isActual = gridBand9.Columns.Contains((BandedGridColumn)e.Column);
            bool isFollow = gridBand10.Columns.Contains((BandedGridColumn)e.Column);
            if ((e.Column.FieldName == "ProjectCode" || e.Column.FieldName == "ProjectName" || e.Column.FieldName == "PossibilityPO" || isFollow) && !string.IsNullOrEmpty(checkValue))
            {
                string value1 = TextUtils.ToString(grvDataExport.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvDataExport.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value1 == value2);
                e.Handled = true;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //LinhTN update 06/08/2024
        private void cbTeam_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}


