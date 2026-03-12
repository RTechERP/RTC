using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.Utils;
using BMS.Model;
using static DevExpress.Utils.Svg.CommonSvgImages;

namespace BMS
{
    public partial class frmOfficeSupplyRequestSummary : _Forms
    {
        public frmOfficeSupplyRequestSummary()
        {
            InitializeComponent();
        }


        private void OfficeSupplyRequestSummary_Load(object sender, EventArgs e)
        {
            dtpMonthPicker.DateTime = DateTime.Today;

            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;

            LoadDepartment();
            LoadData();
        }

        //List<string> userAlls = new List<string>() { "NV0151", "HR12", "NV0128" };
        //List<int> userAlls = new List<int>() { 354, 156, 331 };
        void LoadDepartment()
        {
            //int[] departmentIDs = new int[] { 9, 10 };
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            if (Global.EmployeeID == 331)
            {
                list = list.Where(x => Global.departmentIDs.Contains(x.ID)).ToList();
            }
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            if (!Global.userAllsOfficeSupply.Contains(Global.EmployeeID) && !Global.IsAdmin)
            {
                cboDepartment.EditValue = Global.DepartmentID;
                cboDepartment.Enabled = false;
            }

        }
        private void LoadData()
        {
            grdData.DataSource = null;
            //AutiGenColum();
            //DateTime MonthInput = dtpMonthPicker.EditValue == null ? DateTime.Now : dtpMonthPicker.DateTime;

            DateTime dateStart = new DateTime(TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value), 1, 0, 0, 0);
            DateTime dateEnd = dateStart.AddMonths(+1).AddSeconds(-1);

            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);


            DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupplyRequestSummary",
                                                        new string[] { "@DateStart", "@DateEnd", "@DepartmentID" },
                                                        new object[] { dateStart, dateEnd, departmentID });
            grdData.DataSource = dt;
        }



        void AutiGenColum()
        {
            if (bandAmount.Columns.Count > 0) bandAmount.Columns.Clear();


            var departments = SQLHelper<DepartmentModel>.FindAll();

            foreach (var item in departments)
            {
                BandedGridColumn column = new BandedGridColumn();

                column.Visible = true;
                column.FieldName = item.Code;
                column.Caption = item.Name;
                column.ColumnEdit = repositoryItemMemoEdit1;
                column.Width = 55;
                column.MinWidth = 50;

                column.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                column.Summary.Add(new DevExpress.XtraGrid.GridColumnSummaryItem { SummaryType = DevExpress.Data.SummaryItemType.Sum, FieldName = item.Code, DisplayFormat = "{0:n0}" });
                column.DisplayFormat.FormatType = FormatType.Numeric;
                column.DisplayFormat.FormatString = "{0:n0}";

                bandAmount.Columns.Add(column);

            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            //DateTime MonthInput = dtpMonthPicker.EditValue == null ? DateTime.Now : dtpMonthPicker.DateTime;
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xls)|*.xls;*.xls";
            //sfd.FileName = $"OfficeSupplyRequestSummary_T{MonthInput.Month}_{MonthInput.Year}.xls";

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        grvData.ExportToXlsx(sfd.FileName);
            //        Process.Start(sfd.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}


            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"OfficeSupplyRequestSummary_T{txtMonth.Value}_{txtYear.Value}.xls");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsExportOptions optionsEx = new XlsExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);


                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsExportMode.SingleFilePageByPage;
                    //optionsEx.AllowSortingAndFiltering = DefaultBoolean.True;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpMonthPicker_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            BandedGridColumn column = (BandedGridColumn)e.Column;
            bool isColumn = bandAmount.Columns.Contains(column);
            if (isColumn)
            {
                int quantity = TextUtils.ToInt(e.Value);
                if (quantity <= 0)
                {
                    e.DisplayText = "";
                }
            }
        }

        private void btnPriceRequest_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn báo giá!", "Thông báo");
                return;
            }
            if (MessageBox.Show("Bạn có xác nhận yêu cầu báo giá những sản phẩm đã chọn ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            int countSTT = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequest_New", "A",
                                new string[] { "@DateStart", "@DateEnd" },
                                new object[] { new DateTime(2000, 1, 1), new DateTime(2000, 1, 1) });
            DataTable dtClone = dt.Clone();
            foreach (int i in rowSelecteds)
            {
                countSTT++;

                decimal totalQuantity = TextUtils.ToInt(grvData.GetRowCellValue(i, "TotalQuantity"));
                decimal quantityHCM = TextUtils.ToInt(grvData.GetRowCellValue(i, colHCM.FieldName));
                if (totalQuantity - quantityHCM <= 0) continue;

                DataRow dr = dt.NewRow();
                dr["STT"] = countSTT;
                dr["ProductCode"] = TextUtils.ToString(grvData.GetRowCellValue(i, "CodeRTC"));
                dr["ProductName"] = TextUtils.ToString(grvData.GetRowCellValue(i, "OfficeSupplyName"));
                dr["Quantity"] = totalQuantity - quantityHCM;
                dr["UnitCount"] = TextUtils.ToString(grvData.GetRowCellValue(i, "OfficeSupplyUnit"));
                dt.Rows.Add(dr);
            }
            frmProjectPartlistPriceRequestDetailNew frm = new frmProjectPartlistPriceRequestDetailNew(0,3); //Hàng hr
            frm.dt = dt;
            frm.isVPP = true;
            frm.ShowDialog();
        }

        private void btnPurchaseRequest_Click(object sender, EventArgs e)
        {
            string unit = "";
            string codeRTC = "";
            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn yêu cầu mua hàng!", "Thông báo");
                return;
            }

            if (!CheckDeadline()) return;

            if (MessageBox.Show("Bạn có xác nhận yêu cầu mua những sản phẩm đã chọn ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                foreach (int i in rowSelecteds)
                {
                    ProjectPartlistPurchaseRequestModel purchaseRequest = new ProjectPartlistPurchaseRequestModel();
                    codeRTC = grvData.GetRowCellValue(i, "CodeRTC").ToString();
                    unit = grvData.GetRowCellValue(i, "OfficeSupplyUnit").ToString();
                    UnitCountModel unitCount = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", unit).FirstOrDefault();
                    ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByAttribute("ProductCode", codeRTC).FirstOrDefault();
                    if (productSale != null)
                    {
                        purchaseRequest.ProductGroupID = productSale.ProductGroupID;
                        purchaseRequest.ProductName = productSale.ProductName;
                        purchaseRequest.ProductSaleID = productSale.ID;
                        purchaseRequest.EmployeeID = Global.EmployeeID;
                        purchaseRequest.UnitCountID = unitCount == null ? 0 : unitCount.ID;
                        purchaseRequest.Quantity = Lib.ToInt(grvData.GetRowCellValue(i, "TotalQuantity").ToString());
                        purchaseRequest.TotalPrice = Lib.ToDecimal(grvData.GetRowCellValue(i, "TotalPrice").ToString());
                        purchaseRequest.UnitPrice = Lib.ToDecimal(grvData.GetRowCellValue(i, "UnitPrice").ToString());
                        purchaseRequest.UnitName = unit;
                        purchaseRequest.DateReturnExpected = dtpDeadline.Value;
                        SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(purchaseRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CheckDeadline()
        {
            //Kiểm tra nhập Deadline - start
            DateTime deadline = dtpDeadline.Value;
            DateTime dateNow = DateTime.Now;

            double timeSpan = (deadline.Date - dateNow.Date).TotalDays + 1;
            if (dateNow.Hour < 15)
            {
                if (timeSpan < 2)
                {
                    MessageBox.Show("Deadline tối thiếu là 2 ngày từ ngày hiện tại!", "Thông báo");
                    return false;
                }
            }
            else if (timeSpan < 3)
            {
                MessageBox.Show("Yêu cầu từ sau 15h nên ngày Deadline sẽ bắt đầu tính từ ngày hôm sau và tối thiểu là 2 ngày!", "Thông báo");
                return false;
            }

            if (deadline.DayOfWeek == DayOfWeek.Sunday || deadline.DayOfWeek == DayOfWeek.Saturday)
            {
                MessageBox.Show("Deadline phải là ngày làm việc (T2 - T6)!", "Thông báo");
                return false;
            }

            int coutWeekday = 0;
            for (int i = 0; i < timeSpan; i++)
            {
                DateTime dateValue = dateNow.Date.AddDays(i);
                if (dateValue.DayOfWeek == DayOfWeek.Sunday || dateValue.DayOfWeek == DayOfWeek.Saturday)
                {
                    coutWeekday++;
                }
            }

            if (coutWeekday > 0)
            {
                DialogResult dialog = MessageBox.Show($"Deadline sẽ không tính Thứ 7 và Chủ nhật.\nBạn có chắc muốn chọn Deadline là ngày [{deadline.ToString("dd/MM/yyyy")}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return false;
            }
            return true;
            //Kiểm tra nhập Deadline - end
        }

        private void btnViewPriceRequest_Click(object sender, EventArgs e)
        {
            frmProjectPartlistPriceRequestNew frm = new frmProjectPartlistPriceRequestNew(3); //Hàng HR
            frm.isVPP = true;
            frm.Show();
        }

        private void btnViewRequestBuy_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int id = 999999;
            if (id <= 0) return;
            frmJobRequeirementFurchaseRequest frm = new frmJobRequeirementFurchaseRequest();
            //frm.listJobRequirementID = listJobRequirementID;
            frm.jobRequirementID = id;
            frm.ShowDialog();
        }
    }
}