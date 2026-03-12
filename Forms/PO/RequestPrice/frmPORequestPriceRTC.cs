using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPORequestPriceRTC : _Forms
    {
        public int POKHID = 0;
        DataTable dtPriceRequest = new DataTable();
        public frmPORequestPriceRTC()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistPriceRequestDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            //LoadSupplierSale();
            //LoadCurrency();
            //LoadDepartment();
            LoadData();
        }
        private void LoadData()
        {
            dtPriceRequest = TextUtils.LoadDataFromSP("spGetPOKHDetail", "A", new string[] { "@ID", "@IDDetail" }, new object[] { POKHID, 0 });
            grdData.DataSource = dtPriceRequest;
        }
        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.Enabled = Global.IsAdmin;
            cboEmployee.EditValue = Global.EmployeeID;
        }
        //void LoadSupplierSale()
        //{
        //    List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
        //    cboSupplierSale.ValueMember = "ID";
        //    cboSupplierSale.DisplayMember = "NameNCC";
        //    cboSupplierSale.DataSource = list;
        //}

        //void LoadCurrency()
        //{
        //    List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
        //    cboCurrency.ValueMember = "ID";
        //    cboCurrency.DisplayMember = "Code";
        //    cboCurrency.DataSource = list;
        //}
        //void LoadDepartment()
        //{
        //    var lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
        //    cboDepartment.Properties.DataSource = lst;
        //    cboDepartment.Properties.DisplayMember = "Name";
        //    cboDepartment.Properties.ValueMember = "ID";
        //}

        bool CheckValidate()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            if (employeeID <= 0)
            {
                MessageBox.Show($"Vui lòng chọn Người yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int[] rowSelecteds = grvData.GetSelectedRows();
            foreach (int index in rowSelecteds)
            {
                //bool isCheckPrice = TextUtils.ToBoolean(grvData.GetRowCellValue(index, colIsCheckPrice));
                //int statusRequest = TextUtils.ToInt(grvData.GetRowCellValue(index, colStatusRequest));
                //trạng thái đã báo giá, đã hoàn thành hoặc đã check giá
                //if (statusRequest == 2 || statusRequest == 3 || isCheckPrice) continue;

                string code = TextUtils.ToString(grvData.GetRowCellValue(index, colProductCode));
                int quantity = TextUtils.ToInt(grvData.GetRowCellValue(index, colQuantityRequestRemain));
                DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellValue(index, colDeadline).ToString());

                int requestID = TextUtils.ToInt(grvData.GetRowCellValue(index, colProjectPartlistPriceRequestID));
                ProjectPartlistPriceRequestModel priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(requestID);
                if (priceRequest.IsDeleted == false)
                {
                    if (priceRequest.StatusRequest == 2)
                    {
                        MessageBox.Show($"Sản phầm mã [{code}] đã báo giá.\nBạn không thể yêu cầu báo giá!", "Thông báo");
                        return false;
                    }

                    if (priceRequest.StatusRequest == 3)
                    {
                        MessageBox.Show($"Sản phầm mã [{code}] đã Hoàn thành báo giá.\nBạn không thể yêu cầu báo giá!", "Thông báo");
                        return false;
                    }

                    if (priceRequest.IsCheckPrice == true)
                    {
                        MessageBox.Show($"Sản phầm mã [{code}] đang check giá.\nBạn không thể yêu cầu báo giá!", "Thông báo");
                        return false;
                    }
                }

                if (!deadline.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Deadline sản phầm mã [{code}]!", "Thông báo");
                    return false;
                }
                else
                {
                    //var isCheckDeadline = CheckDeadLine(deadline.Value);

                    return CheckDeadLine(deadline.Value);
                }

                if (quantity <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập SL yêu cầu của mã [{code}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //Kiểm tra nhập Deadline - start
                //DateTime dateNow = DateTime.Now;

                //double timeSpan = (deadline.Date - dateNow.Date).TotalDays + 1;
                //if (dateNow.Hour < 15)
                //{
                //    if (timeSpan < 2)
                //    {
                //        MessageBox.Show($"Deadline của mã [{code}] tối thiếu là 2 ngày từ ngày hiện tại!", "Thông báo");
                //        return false;
                //    }
                //}
                //else if (timeSpan < 3)
                //{
                //    MessageBox.Show($"Yêu cầu từ sau 15h nên ngày Deadline của mã [{code}] sẽ bắt đầu tính từ ngày hôm sau và tối thiểu là 2 ngày!", "Thông báo");
                //    return false;
                //}

                //if (deadline.DayOfWeek == DayOfWeek.Sunday || deadline.DayOfWeek == DayOfWeek.Saturday)
                //{
                //    MessageBox.Show($"Deadline của mã [{code}] phải là ngày làm việc (T2 - T6)!", "Thông báo");
                //    return false;
                //}

                //int coutWeekday = 0;
                //for (int i = 0; i < timeSpan; i++)
                //{
                //    DateTime dateValue = dateNow.Date.AddDays(i);
                //    if (dateValue.DayOfWeek == DayOfWeek.Sunday || dateValue.DayOfWeek == DayOfWeek.Saturday)
                //    {
                //        coutWeekday++;
                //    }
                //}

                //if (coutWeekday > 0)
                //{
                //    DialogResult dialog = MessageBox.Show($"Deadline sẽ không tính Thứ 7 và Chủ nhật.\nBạn có chắc muốn chọn Deadline là ngày [{deadline.ToString("dd/MM/yyyy")}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    return dialog == DialogResult.Yes;
                //}
                //Kiểm tra nhập Deadline - end
            }

            return true;
        }


        bool CheckDeadLine(DateTime deadline)
        {
            //Nếu ngày yêu cầu từ sau 15h, thì bắt đầu tính từ ngày hôm sau
            //Nếu ngày yêu cầu là ngày T7 hoặc CN thì bắt đầu tính từ t2
            //Ngày deadline phải lơn hơn ngày yêu cầu từ 2 ngày trở lên
            //Và không tính T7, CN
            TimeSpan time = new TimeSpan(15, 0, 0);
            DateTime dateRequest = DateTime.Now;
            TimeSpan timeRequest = TimeSpan.Parse(dateRequest.ToString("HH:mm:ss"));
            if (timeRequest >= time)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            if (dateRequest.DayOfWeek == DayOfWeek.Saturday)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            if (dateRequest.DayOfWeek == DayOfWeek.Sunday)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            List<DateTime> listDates = new List<DateTime>();
            double totalDays = (deadline.Date - dateRequest.Date).TotalDays;
            for (int i = 0; i <= totalDays; i++)
            {
                var date = dateRequest.AddDays(i).Date;
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                if (!listDates.Contains(date))
                {
                    listDates.Add(date);
                }
            }

            if (listDates.Count < 2)
            {
                MessageBox.Show($"Dealine phải ít nhất là 2 ngày tính từ [{dateRequest.ToString("dd/MM/yyyy")}]", "Thông báo");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool SaveData()
        {
            grvData.CloseEditor();
            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn vào sản phẩm muốn yêu cầu báo giá!", "Thông báo");
                return false;
            }


            if (!CheckValidate()) return false;

            foreach (int i in rowSelecteds)
            {
                decimal quantityRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantityRequestRemain));
                //bool isPriceRequest = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsPriceRequestStatus));
                //bool isCheckPrice = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheckPrice));
                //int statusRequest = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatusRequest));

                //trạng thái đã báo giá, đã hoàn thành hoặc đã check giá
                //if (statusRequest == 2 || statusRequest == 3 || isCheckPrice) continue;

                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                var listRequests = SQLHelper<ProjectPartlistPriceRequestModel>.FindByAttribute(ProjectPartlistPriceRequestModel_Enum.POKHDetailID.ToString(), id);
                if (listRequests.Count > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(),true },
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.AppCodeName },
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    string idText = string.Join(",", listRequests.Select(x => x.ID));
                    SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, new Utils.Expression(ProjectPartlistPriceRequestModel_Enum.ID.ToString(), idText, "IN"));
                }

                ProjectPartlistPriceRequestModel model = new ProjectPartlistPriceRequestModel();
                model.DateRequest = TextUtils.ToDate5(dtpDateRequest.Value); // ngày yêu cầu
                model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue); // người yêu cầu
                model.Deadline = TextUtils.ToDate5(grvData.GetRowCellValue(i, colDeadline));
                model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                model.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                model.Quantity = quantityRequest;
                //model.DatePriceQuote = TextUtils.ToDate2(grvData.GetRowCellValue(i, colDatePriceQuote)); // ngày báo giá
                //model.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));  // đơn giá
                //model.CurrencyID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyID)); // loại tiền
                //model.CurrencyRate = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyRate));  //tỷ giá
                //model.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));  //thành tiền chưa VAT
                //model.TotalPriceExchange = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceExchange));  //thành tiền quy đổi VNĐ  
                //model.SupplierSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierSaleID));  //nhà cung cấp
                //model.TotalDayLeadTime = TextUtils.ToInt(grvData.GetRowCellValue(i, colTotalDayLeadTime));
                //model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                model.StatusRequest = 1; // yêu cầu báo giá
                //model.Quantity = quantityRequest; // số lương yêu cầu
                model.IsCommercialProduct = true; //hàng thương mại
                model.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                //var dtChange = dtPriceRequest.GetChanges();
                //DataRow dtCheck = null;
                //if (dtChange != null)
                //{
                //    dtCheck = dtChange.AsEnumerable().FirstOrDefault(row => row.Field<int>("ID") == model.POKHDetailID);
                //}
                //if (isPriceRequest == true)
                //{
                //    SQLHelper<ProjectPartlistPriceRequestModel>.DeleteByAttribute("POKHDetailID", TextUtils.ToInt(model.POKHDetailID));
                //}
                SQLHelper<ProjectPartlistPriceRequestModel>.Insert(model);
            }
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng tick vào sản phẩm muốn xóa!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Các yêu cầu đã báo giá, đã hoàn thành hoặc đang check giá sẽ không thể xóa và bỏ qua. Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int[] statusRequests = new int[] { 2, 3 };

                List<int> listRequests = new List<int>();
                foreach (int i in rowSelecteds)
                {
                    //bool isCheckPrice = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheckPrice));
                    //int statusRequest = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatusRequest));
                    //trạng thái đã báo giá, đã hoàn thành hoặc đã check giá
                    //if (statusRequest == 2 || statusRequest == 3 || isCheckPrice) continue;



                    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartlistPriceRequestID));
                    ProjectPartlistPriceRequestModel priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    if (statusRequests.Contains(TextUtils.ToInt(priceRequest.StatusRequest)) || priceRequest.IsCheckPrice == true) continue;
                    //SQLHelper<ProjectPartlistPriceRequestModel>.DeleteByAttribute("POKHDetailID", POKHDetailID);

                    listRequests.Add(id);
                }

                var myDict = new Dictionary<string, object>()
                {
                    {ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(),true },
                    {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.AppCodeName },
                    {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                };

                string idText = string.Join(",", listRequests);
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, new Utils.Expression(ProjectPartlistPriceRequestModel_Enum.ID.ToString(), idText, "IN"));

                //LoadData();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (gridView == null) return;

            //int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            //MessageBox.Show(gridView.Name, id.ToString());


            if (gridView.FocusedColumn == colCurrencyID) return;
            if (isRecallCellValueChanged == true) return;
            try
            {

                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    gridView.CloseEditor();

                    int[] selectedRows = gridView.GetSelectedRows();

                    if (selectedRows.Length > 0)
                    {
                        if (e.Value == null) return;
                        foreach (int row in selectedRows)
                        {
                            if (e.Column.FieldName != colTotalPriceExchange.FieldName && e.Column.FieldName != colTotalPrice.FieldName)
                            {
                                gridView.SetRowCellValue(row, gridView.Columns[e.Column.FieldName], e.Value);
                            }

                            //if (e.Column.FieldName != colUnitPrice.FieldName
                            //    && e.Column.FieldName != colUnitImportPrice.FieldName
                            //    && e.Column.FieldName != colVAT.FieldName
                            //    && e.Column.FieldName != colTotalDayLeadTime.FieldName) continue;

                            //UpdateValue(gridView, row);

                        }
                    }
                    else
                    {
                        gridView.SetRowCellValue(gridView.FocusedRowHandle, gridView.Columns[e.Column.FieldName], e.Value);

                        //UpdateValue(gridView, gridView.FocusedRowHandle);
                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }
        //private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        //{
        //    SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
        //    DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();

        //    cboDepartment.EditValue = dataRow.Row.Field<int>("DepartmentID");
        //}
    }
}