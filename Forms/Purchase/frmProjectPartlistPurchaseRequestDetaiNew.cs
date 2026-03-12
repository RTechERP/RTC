using BMS.Model;
using BMS.Utils;
using DevExpress.Utils.Gesture;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectPartlistPurchaseRequestDetaiNew : _Forms
    {
        public DataTable dt = new DataTable();
        int _requestTypeID = 0;
        public List<ProjectPartlistPurchaseRequestModel> _lstPurchaseRequest = new List<ProjectPartlistPurchaseRequestModel>();

        //const int PRODUCT_GROUP_MKT_ID = 79;
        const int PRODUCT_GROUP_MKT_ID = 81;
        public frmProjectPartlistPurchaseRequestDetaiNew(int requestTypeID)
        {
            InitializeComponent();
            _requestTypeID = requestTypeID;
        }

        private void frmProjectPartlistPurchaseRequestDetaiNew_Load(object sender, EventArgs e)
        {
            //LoadUnit();
            LoadProductSale();
            LoadEmployee();
            LoadCurrency();
            LoadProductGroup();
            LoadFirm();
            LoadNCC();
            LoadUnitCount();
            LoadData();
        }

        private void LoadNCC()
        {
            List<SupplierSaleModel> lstNCC = SQLHelper<SupplierSaleModel>.FindAll();
            if (lstNCC == null || lstNCC.Count <= 0) return;
            cboNCC.DataSource = lstNCC;
            cboNCC.DisplayMember = "CodeNCC";
            cboNCC.ValueMember = "ID";
        }

        private void LoadFirm()
        {
            List<FirmModel> lstFirm = SQLHelper<FirmModel>.FindAll();
            if (lstFirm == null || lstFirm.Count <= 0) return;
            cboFirm.DataSource = lstFirm;
            cboFirm.DisplayMember = "FirmName";
            cboFirm.ValueMember = "FirmName";
        }

        private void LoadProductGroup()
        {
            List<ProductGroupModel> lstProductGroups = SQLHelper<ProductGroupModel>.FindAll();
            if (lstProductGroups == null || lstProductGroups.Count <= 0) return;
            cboProductGroup.DataSource = lstProductGroups;
            cboProductGroup.DisplayMember = "ProductGroupName";
            cboProductGroup.ValueMember = "ID";
        }

        private void LoadCurrency()
        {
            List<CurrencyModel> lstCurrency = SQLHelper<CurrencyModel>.FindAll();
            if (lstCurrency.Count <= 0) return;
            cboCurrency.DisplayMember = "Code";
            cboCurrency.ValueMember = "ID";
            cboCurrency.DataSource = lstCurrency;
        }


        void LoadUnitCount()
        {
            var unitCounts = SQLHelper<UnitCountModel>.FindAll();

            cboUnitCount.DisplayMember = "UnitName";
            cboUnitCount.ValueMember = "ID";
            cboUnitCount.DataSource = unitCounts;
        }

        private void LoadData()
        {
            if (dt.Rows.Count <= 0)
            {
                dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New_Khanh", "A",
                                                new string[] { "@DateStart", "@DateEnd" },
                                                new object[] { new DateTime(2000, 1, 1), new DateTime(2000, 1, 1) });
                dt.Columns.Add("STT", typeof(int));
                dt = dt.Clone();
            }
            grdData.DataSource = dt;

        }

        //void LoadManufacturer()
        //{
        //    List<ManufacturerModel> list = SQLHelper<ManufacturerModel>.FindAll();
        //    cboMaker.DisplayMember = "ManufacturerName";
        //    cboMaker.ValueMember = "ManufacturerName";
        //    cboMaker.DataSource = list;
        //}

        //void LoadUnit()
        //{
        //    //List<string> unitsCountKTs = SQLHelper<UnitCountKTModel>.FindAll().Select(x => x.UnitCountName.ToUpper()).ToList();
        //    //List<string> unitsCounts = SQLHelper<UnitCountModel>.FindAll().Select(x => x.UnitName.ToUpper()).ToList();

        //    //List<string> units = unitsCountKTs.Union(unitsCounts).ToList();
        //    //cboUnit.DataSource = units.Select(x => new { UnitName = x }).ToList();
        //    //cboUnit.DisplayMember = "UnitName";
        //    //cboUnit.ValueMember = "ID";
        //}
        private void LoadProductSale()
        {
            List<ProductSaleModel> lst = SQLHelper<ProductSaleModel>.FindAll();
            cboProductSale.DataSource = lst;
            cboProductSale.DisplayMember = "ProductNewCode";
            cboProductSale.ValueMember = "ProductNewCode";
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

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);
                if (info.Column != null && info.Column == colAddRow && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = grvData.RowCount + 1;
                    dt.Rows.Add(dtrow);
                }
            }
        }

        private void grvData_Click(object sender, EventArgs e)
        {

        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit cboData = (SearchLookUpEdit)sender;
            ProductSaleModel data = (ProductSaleModel)cboData.GetSelectedDataRow();
            if (data == null) return;

            List<UnitCountModel> unitCounts = (List<UnitCountModel>)cboUnitCount.DataSource;

            UnitCountModel unitCount = unitCounts.Where(x => x.UnitName == data.Unit).FirstOrDefault() ?? new UnitCountModel();

            grvData.SetFocusedRowCellValue(colProductNewCode, data.ProductNewCode);
            grvData.SetFocusedRowCellValue(colProductCode, data.ProductCode);
            grvData.SetFocusedRowCellValue(colProductName, data.ProductName);
            grvData.SetFocusedRowCellValue(colMaker, data.Maker);
            grvData.SetFocusedRowCellValue(colUnitName, unitCount.ID);
            grvData.SetFocusedRowCellValue(colProductGroup, data.ProductGroupID);
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
            grvData.FocusedRowHandle = -1;

            if (grvData.RowCount <= 0)
            {
                MessageBox.Show($"Vui lòng chọn vào sản phẩm muốn yêu cầu báo giá!", "Thông báo");
                return false;
            }


            if (!CheckValidate()) return false;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                
                decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));
                string productNewCode = Lib.ToString(grvData.GetRowCellValue(i, colProductNewCode));
                //int productSaleID = SQLHelper<ProductSaleModel>.FindByAttribute("ProductNewCode", productNewCode).FirstOrDefault().ID;
                int productSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductNewCode));
                //int status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatusRequest));
                //bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheckPrice));
                //int empID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID));
                int id = Lib.ToInt(grvData.GetRowCellValue(i, colID));
                ProjectPartlistPurchaseRequestModel model = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                model.DateRequest = TextUtils.ToDate5(dtpDateRequest.Value); // ngày yêu cầu
                model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue); // người yêu cầu
                model.DateReturnExpected = TextUtils.ToDate5(grvData.GetRowCellValue(i, colDateReturnExpected));
                model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                model.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                model.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, colMaker));
                model.UnitName = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colUnitName));
                model.Quantity = quantity;
                model.ProjectPartlistPurchaseRequestTypeID = _requestTypeID; //KhoMarketing = 7
                //model.ProductGroupID = Lib.ToInt(grvData.GetRowCellValue(i, colProductGroup));
                model.ProductGroupID = PRODUCT_GROUP_MKT_ID;
                model.CurrencyID = Lib.ToInt(grvData.GetRowCellValue(i, colCurrency));
                model.CurrencyRate = Lib.ToDecimal(grvData.GetRowCellValue(i, colCurrencyRate));
                model.ProductSaleID = productSaleID;
                model.StatusRequest = 1;
                model.SupplierSaleID = Lib.ToInt(grvData.GetRowCellValue(i, colNCC));
                //UnitCountModel unit = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", model.UnitName).FirstOrDefault();
                //if (unit != null && unit.ID > 0) 
                model.UnitCountID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitName));

                //model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                //model.StatusRequest = 1; // yêu cầu báo giá
                //model.IsCommercialProduct = true; //hàng thương mại
                //if (isJobRequirement) model.IsJobRequirement = true; //Yêu cầu công việc
                //else model.IsCommercialProduct = true; //hàng thương mại
                // model.JobRequirementID = _jobRequirementID;


                if (model.ID <= 0)
                {
                    model.ID = SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(model).ID;

                    ProjectPartlistPurchaseRequestNoteModel purchaseNote = new ProjectPartlistPurchaseRequestNoteModel();
                    purchaseNote.ProjectPartlistPurchaseRequestID = model.ID;
                    purchaseNote.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));

                    SQLHelper<ProjectPartlistPurchaseRequestNoteModel>.Insert(purchaseNote);
                }
                else
                {
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(model);

                    //Update note
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartlistPurchaseRequestNoteModel_Enum.Note.ToString(), TextUtils.ToString(grvData.GetRowCellValue(i, colNote))},
                        {ProjectPartlistPurchaseRequestNoteModel_Enum.UpdatedBy.ToString(), Global.AppUserName},
                        {ProjectPartlistPurchaseRequestNoteModel_Enum.UpdatedDate.ToString(), DateTime.Now}
                    };

                    var exp = new Utils.Expression(ProjectPartlistPurchaseRequestNoteModel_Enum.ProjectPartlistPurchaseRequestID, model.ID);
                    SQLHelper<ProjectPartlistPurchaseRequestNoteModel>.UpdateFields(myDict, exp);
                }




                //// Insert và Update dữ liệu vào bảng ProjectPartlistPurchaseRequestNote
                //ProjectPartlistPurchaseRequestNoteModel purchaseNote = new ProjectPartlistPurchaseRequestNoteModel();
                //purchaseNote.ProjectPartlistPurchaseRequestID = id;
                //purchaseNote.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));

                
                //if (id <= 0)
                //{
                //    ResultQuery rs = SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(model);
                //    if (rs != null && rs.IsSuccess)
                //    {
                //        id = rs.ID;
                //        purchaseNote.ProjectPartlistPurchaseRequestID = rs.ID;
                //        SQLHelper<ProjectPartlistPurchaseRequestNoteModel>.Insert(purchaseNote);
                //    }
                //}
                //else
                //{
                //    model.ID = id;
                //    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(model);

                //    ProjectPartlistPurchaseRequestNoteModel checkNote = SQLHelper<ProjectPartlistPurchaseRequestNoteModel>.FindByAttribute("ProjectPartlistPurchaseRequestID", id).FirstOrDefault();
                //    if (checkNote != null && checkNote.ID > 0)
                //    {
                //        purchaseNote.ID = checkNote.ID;
                //        SQLHelper<ProjectPartlistPurchaseRequestNoteModel>.Update(purchaseNote);
                //    }
                //}
            }
            return true;
        }

        private bool CheckValidate()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            if (employeeID <= 0)
            {
                MessageBox.Show($"Vui lòng chọn Người yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (grvData.RowCount <= 0)
            {
                MessageBox.Show($"Vui lòng tạo ít nhất một yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            for (int index = 0; index < grvData.RowCount; index++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(index, colSTT));
                string code = TextUtils.ToString(grvData.GetRowCellValue(index, colProductCode));
                string name = TextUtils.ToString(grvData.GetRowCellValue(index, colProductName));
                string maker = TextUtils.ToString(grvData.GetRowCellValue(index, colMaker));
                string unit = TextUtils.ToString(grvData.GetRowCellValue(index, colUnitName));
                int quantity = TextUtils.ToInt(grvData.GetRowCellValue(index, colQuantity));
                DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellValue(index, colDateReturnExpected).ToString());

                if (string.IsNullOrWhiteSpace(code))
                {
                    MessageBox.Show($"Vui lòng nhập Mã sản phẩm tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show($"Vui lòng nhập Tên sản phẩm tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!deadline.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Deadline sản phầm tại dòng [{stt}]!", "Thông báo");
                    return false;
                }
                else if (!CheckDeadLine(deadline.Value))
                {
                    return false;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập SL yêu cầu tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //if (string.IsNullOrWhiteSpace(maker))
                //{
                //    MessageBox.Show($"Vui lòng nhập Hãng tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

                //if (string.IsNullOrWhiteSpace(unit))
                //{
                //    MessageBox.Show($"Vui lòng nhập ĐVT tại dòng [{stt}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}

            }

            return true;
        }

        bool CheckDeadLine(DateTime deadline)
        {
            //Nếu ngày yêu cầu từ sau 15h, thì bắt đầu tính từ ngày hôm sau
            //Nếu ngày yêu cầu là ngày T7 hoặc CN thì bắt đầu tính từ t2
            //Ngày deadline phải lơn hơn ngày yêu cầu từ 2 ngày trở lên
            //Và không tính T7, CN


            if (Global.IsAdmin) return true;

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
                MessageBox.Show($"Dealine phải ít nhất là 2 ngày tính từ [{dateRequest.ToString("dd/MM/yyyy")}] và KHÔNG tính Thứ 7, Chủ nhật", "Thông báo");
                return false;
            }

            return true;
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();
            if (currency == null) return;

            GridControl gridControl = (GridControl)lookUpEdit.Parent;
            GridView gridView = gridControl.MainView as GridView;
            if (gridView == null) return;
            int[] rowSelecteds = gridView.GetSelectedRows();
            bool isExpried = false;

            if (rowSelecteds.Length <= 0)
            {
                int row = gridView.FocusedRowHandle;
                gridView.SetRowCellValue(row, colCurrency, currency.ID);
                gridView.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                if (isExpried) gridView.SetRowCellValue(row, colCurrencyRate, 0);
            }
            else
            {
                foreach (int row in rowSelecteds)
                {
                    gridView.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                    gridView.SetRowCellValue(row, colCurrency, currency.ID);
                    if (isExpried) gridView.SetRowCellValue(row, colCurrencyRate, 0);
                }
            }

        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                dt = dt.Clone();
                grdData.DataSource = dt;
                cboEmployee.Clear();
                dtpDateRequest.Value = DateTime.Now;

            }
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm mã [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
            }
        }
    }
}