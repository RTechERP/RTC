using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPORequestBuyRTC : _Forms
    {
        public int id;
        List<DepartmentModel> lsDepartment = new List<DepartmentModel>();
        DataTable dtEmployee = new DataTable();
        List<ProjectPartlistPurchaseRequestModel> lsProjectPartlists = new List<ProjectPartlistPurchaseRequestModel>();
        public frmPORequestBuyRTC()
        {
            InitializeComponent();
        }

        private void frmPORequestBuyRTC_Load(object sender, EventArgs e)
        {
            loadcbDepartment();
            loadcbEmployee();
            loadProjectPartlists();

            loadData();
        }

        void loadData()
        {
            DataTable dtDetail = TextUtils.LoadDataFromSP("spGetPOKHDetail", "A", new string[] { "@ID", "@IDDetail" }, new object[] { id, 0 });
            grdData.DataSource = dtDetail;


            grvData.SelectAll();
        }

        void loadcbEmployee()
        {
            dtEmployee = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = dtEmployee;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            cboEmployee.EditValue = Global.EmployeeID;
            cboEmployee.Enabled = Global.IsAdmin;
        }

        void loadcbDepartment()
        {
            lsDepartment = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cbDepartment.Properties.DataSource = lsDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
            cbDepartment.Enabled = Global.IsAdmin;
        }

        void loadProjectPartlists()
        {
            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    int id = TextUtils.ToInt(grvData.GetRowCellValue(0, colID));
            //    if (id <= 0) continue;

            //    var exp1 = new Expression("POKHDetailID", id);
            //    lsProjectPartlists = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(exp1).ToList();
            //    if (lsProjectPartlists.Count > 0)
            //    {
            //        cboEmployee.EditValue = lsProjectPartlists[0].EmployeeID;
            //        btnSave.Enabled = false;
            //        break;
            //    }
            //}
        }

        bool validate()
        {
            if (cboEmployee.Text == "")
            {
                MessageBox.Show("Người yêu cầu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbDepartment.Text == "")
            {
                MessageBox.Show("Phòng ban không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));

                if (lsProjectPartlists.Count > 0)
                {
                    MessageBox.Show($"Sản phẩm có mã [{productCode}] đã được yêu cầu báo giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool save()
        {
            if (!validate()) return false;
            grvData.FocusedRowHandle = -1;

            int[] rowSelecteds = grvData.GetSelectedRows();
            //string parentProductCode = string.Empty;

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int indexSelected = Array.IndexOf(rowSelecteds, i);
                int quantityRequest = TextUtils.ToInt(grvData.GetRowCellValue(i, colQuantityRequestRemain));
                if (quantityRequest <= 0 || indexSelected < 0) continue;


                ProjectPartlistPurchaseRequestModel model = new ProjectPartlistPurchaseRequestModel();

                model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                model.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));

                //lấy id của UnitCount
                string unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                List<UnitCountModel> lsUnitCount = SQLHelper<UnitCountModel>.FindByExpression(new Expression("UnitName", unit.Trim()));
                if (lsUnitCount.Count > 0)
                {
                    model.UnitCountID = lsUnitCount[0].ID;
                }

                model.StatusRequest = 1; // yêu cầu mua hàng
                model.DateRequest = dtpDateRequest.Value; // ngày yêu cầu
                model.DateReturnExpected = dtpDateReturnExpected.Value; // ngày yêu cầu giao
                model.DateReceive = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDeliveryRequestedDate)); //Ngày đặt hàng

                //model.Quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                model.Quantity = quantityRequest;
                // đơn giá
                //model.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));

                // tỷ giá
                decimal currencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCurrencyRate));
                model.CurrencyRate = currencyRate;

                // tông tiền chưa VAT
                decimal TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                model.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                //if( TotalPrice <= 0 && string.IsNullOrEmpty(parentProductCode))
                //{
                //        parentProductCode = model.ProductCode;
                //}
                model.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                // tính tiền quy đổi Việt Nam
                model.TotalPriceExchange = currencyRate <= 0 ? 0 : (TotalPrice * currencyRate);

                //Tính thành tiền có VAT
                decimal totalMoneyVAT_New = TotalPrice + ((TotalPrice * TextUtils.ToDecimal(model.VAT)) / 100);
                decimal totalMoneyVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceIncludeVAT));
                model.TotaMoneyVAT = totalMoneyVAT == totalMoneyVAT_New ? totalMoneyVAT : totalMoneyVAT_New;

                model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                //model.DateReturnExpected = TextUtils.ToDate5(grvData.GetRowCellValue(i, colActualDeliveryDate));


                model.ProductSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID)); // product sale id
                model.ProductGroupID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductGroupID));
                model.CurrencyID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyID));

                int day = (TextUtils.ToDate5(dtpDateReturnExpected.Value) - TextUtils.ToDate5(dtpDateRequest.Value)).Days;
                model.TotalDayLeadTime = day;

                model.IsCommercialProduct = true;
                model.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                model.ProjectPartlistPurchaseRequestTypeID = 5; //yc mua thương mại: ndnhat update 14/10/2025
                model.ParentProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colParentProductCode));//ndnhat update 14/10/2025

                SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(model);
            }
            return true;
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            DataRow rowEmployee = dtEmployee.AsEnumerable().FirstOrDefault(row => row.Field<int>("ID") == employeeID);
            if (rowEmployee != null)
            {
                int departmentID = rowEmployee.Field<int>("DepartmentID");
                cbDepartment.EditValue = departmentID;
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
    }
}
