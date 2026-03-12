using BaseBusiness.DTO;
using BMS;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit.Import.Html;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Sale.HandoverMinutes
{
    //---------------------Ninh Duy Nhật 12-02-2025---------------------
    public partial class frmHandoverMinutesDetail : _Forms
    {

        public HandoverMinutesModel HandoverMinutesModel = new HandoverMinutesModel();
        public Func<HandoverMinutesModel, Task> SaveEvent;

        public DataTable dtContent = new DataTable();
        List<int> lstDeletedDetails = new List<int>();
        List<CustomerModel> lstCustomer = new List<CustomerModel>();
        public frmHandoverMinutesDetail()
        {
            InitializeComponent();
            loadCustomer();
            loadEmployee();
            loadData();
            loadProduct();
        }
        /// <summary>
        /// Load data from HandoverMinutesModel to form
        /// </summary>
        private void loadData()
        {
            HandoverMinutesModel = SQLHelper<HandoverMinutesModel>.FindByID(HandoverMinutesModel.ID);
            if (HandoverMinutesModel == null) return;
            deDateMinutes.DateTime = HandoverMinutesModel.DateMinutes ?? DateTime.Now;
            cboCustomerName.EditValue = HandoverMinutesModel.CustomerID;
            txtCustomerAddress.Text = HandoverMinutesModel.CustomerAddress;
            txtCustomerContact.Text = HandoverMinutesModel.CustomerContact;
            txtCustomerPhone.Text = HandoverMinutesModel.CustomerPhone;
            cboEmployeeName.EditValue = HandoverMinutesModel.EmployeeID;
            txtReceiver.Text = HandoverMinutesModel.Receiver;
            txtReceiverPhone.Text = HandoverMinutesModel.ReceiverPhone;
            cboAdminWarehouse.EditValue = HandoverMinutesModel.AdminWarehouseID;

            EmployeeModel e = SQLHelper<EmployeeModel>.FindAll().Where(c => c.ID == HandoverMinutesModel.EmployeeID).FirstOrDefault();
            if (e != null)
            {
                txtEmployeePhone.Text = e.SDTCaNhan;
                DepartmentModel d = SQLHelper<DepartmentModel>.FindAll().Where(c => c.ID == e.DepartmentID).FirstOrDefault();
                if (d != null)
                {
                    txtDepartmentName.Text = d.Name;
                }
                txtEmailCaNhan.Text = e.Email;
            }
        }
        /// <summary>
        /// Load HandoverMinutesDetail from database to form
        /// </summary>
        private void loadHandoverDetail()
        {
            if (dtContent.Columns.Count == 0 || dtContent.Rows.Count == 0)
            {

                DataTable ds = TextUtils.LoadDataFromSP("spGetHanoverMinutesDetail", "ndnData", new string[] { "@HandoverMinutesID" }, new object[] { HandoverMinutesModel.ID });
                dtContent = ds;
                grdDetails.DataSource = dtContent;

            }
            else
            {
                grdDetails.DataSource = dtContent;
                cboEmployeeName.EditValue = dtContent.Rows[0]["EID"];
                cboCustomerName.EditValue = dtContent.Rows[0]["CustomerID"];

            }

            loadProduct();
            loadCboProductStatusAndDeliveryStatus();

        }
        // Trong frmHandoverMinutesDetail
        public void LoadSelectedData(DataTable selectedData)
        {
            if (selectedData != null && selectedData.Rows.Count > 0)
            {
                grdDetails.RefreshDataSource();
                grdDetails.DataSource = selectedData;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nào để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Load customer from database to search lookup edit
        /// </summary>
        private void loadCustomer()
        {
            lstCustomer = SQLHelper<CustomerModel>.FindAll();

            cboCustomerName.Properties.DataSource = lstCustomer;
            cboCustomerName.Properties.DisplayMember = "CustomerName";
            cboCustomerName.Properties.ValueMember = "ID";
        }

        /// <summary>
        /// Load product status and delivery status to repository lookup edit
        /// </summary>
        private void loadCboProductStatusAndDeliveryStatus()
        {
            Dictionary<int, string> productStatus = new Dictionary<int, string>();
            productStatus.Add(1, "Mới");
            productStatus.Add(2, "Cũ");

            Dictionary<int, string> deliveryStatus = new Dictionary<int, string>();
            deliveryStatus.Add(1, "Nhận đủ");
            deliveryStatus.Add(2, "Thiếu");

            cboProductStatus1.DataSource = new BindingSource(productStatus, null);
            cboProductStatus1.DisplayMember = "Value";
            cboProductStatus1.ValueMember = "Key";

            cboDeliveryStatus1.DataSource = new BindingSource(deliveryStatus, null);
            cboDeliveryStatus1.DisplayMember = "Value";
            cboDeliveryStatus1.ValueMember = "Key";
        }
        /// <summary>
        /// Load employee from database to search lookup edit
        /// </summary>
        private void loadEmployee()
        {
            List<EmployeeModel> lst = SQLHelper<EmployeeModel>.FindAll();

            cboEmployeeName.Properties.DataSource = lst;
            cboEmployeeName.Properties.DisplayMember = "FullName";
            cboEmployeeName.Properties.ValueMember = "ID";

            cboAdminWarehouse.Properties.DataSource = lst;
            cboAdminWarehouse.Properties.DisplayMember = "FullName";
            cboAdminWarehouse.Properties.ValueMember = "ID";

        }

        private void frmHandoverMinutesDetail_Load(object sender, EventArgs e)
        {
            loadCustomer();
            loadEmployee();
            loadData();
            loadHandoverDetail();
            loadProduct();
        }
        /// <summary>
        /// Load product from database to repository search lookup edit
        /// </summary>
        private void loadProduct()
        {
            DataTable dt = TextUtils.GetTable("spGetPOKHDetail_New1");
            cboProduct.DataSource = dt;
            cboProduct.DisplayMember = "POCode";
            cboProduct.ValueMember = "POKHDetailID";
        }

        bool validate()
        {
            string patternPhone = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
            Regex regexPhone = new Regex(patternPhone);
            if (string.IsNullOrWhiteSpace(txtCustomerAddress.Text))
            {
                Lib.ShowError("Hãy điền địa chỉ của khách hàng!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerContact.Text))
            {
                Lib.ShowError("Hãy điền liên hệ của khách hàng!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerPhone.Text))
            {
                Lib.ShowError("Hãy chọn số điện thoại khách hàng!");
                return false;
            }
            else
            {
                bool isCheck = regexPhone.IsMatch(txtCustomerPhone.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Hãy nhập đúng định dạng số điện thoại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(txtReceiver.Text))
            {
                Lib.ShowError("Hãy chọn người nhận!");
                return false;
            }
            if (cboCustomerName.EditValue == null)
            {
                Lib.ShowError("Hãy chọn Khách hàng!");
                return false;
            }
            if (deDateMinutes.DateTime == null)
            {
                Lib.ShowError("Hãy chọn ngày!");
                return false;
            }
            if (cboAdminWarehouse.EditValue == null)
            {
                Lib.ShowError("Hãy chọn admin kho!");
                return false;
            }
            if (cboEmployeeName.EditValue == null)
            {
                Lib.ShowError("Hãy chọn nhân viên!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtReceiverPhone.Text))
            {
                Lib.ShowError("Hãy chọn số điện thoại người nhận!");
                return false;
            }
            else
            {
                bool isCheck = regexPhone.IsMatch(txtReceiverPhone.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Hãy nhập đúng định dạng số điện thoại!", TextUtils.Caption);
                    return false;
                }
            }


            for (int i = 0; i < grvDetails.RowCount; i++)
            {
                decimal quantityPending = TextUtils.ToDecimal(grvDetails.GetRowCellValue(i, colQuantityPending));
                decimal quantity = TextUtils.ToDecimal(grvDetails.GetRowCellValue(i, colQuantity));
                int stt = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colSTT));

                if (quantity > quantityPending)
                {
                    MessageBox.Show($"Bạn không thể giao nhiều hơn số lượng còn lại!\n(STT: {stt})", TextUtils.Caption);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Get next code number for HandoverMinutesModel.Code
        /// </summary>
        /// <returns></returns>
        private string GetNextCodeNumber()
        {
            string prefix = $"BBBG.{DateTime.Now:yy}.{DateTime.Now:MMdd}.";

            List<string> existingCodes = SQLHelper<HandoverMinutesModel>.FindAll()
                .Select(m => m.Code.Trim())
                .Where(c => c.StartsWith(prefix))
                .ToList();

            int maxNumber = 0;
            foreach (var code in existingCodes)
            {
                string[] parts = code.Split('.');
                if (parts.Length == 4 && int.TryParse(parts[3], out int num))
                {
                    maxNumber = Math.Max(maxNumber, num);
                }
            }

            return (maxNumber + 1).ToString("D3");
        }

        bool save()
        {
            try
            {
                if (!validate()) return false;

                HandoverMinutesModel model = SQLHelper<HandoverMinutesModel>.FindByID(HandoverMinutesModel.ID) ?? new HandoverMinutesModel();
                model.DateMinutes = deDateMinutes.DateTime;
                model.CustomerID = TextUtils.ToInt(cboCustomerName.EditValue);
                model.CustomerAddress = txtCustomerAddress.Text.Trim();
                model.CustomerContact = txtCustomerContact.Text.Trim();
                model.CustomerPhone = txtCustomerPhone.Text.Trim();
                model.EmployeeID = TextUtils.ToInt(cboEmployeeName.EditValue);
                model.Receiver = txtReceiver.Text.Trim();
                model.ReceiverPhone = txtReceiverPhone.Text.Trim();
                model.AdminWarehouseID = TextUtils.ToInt(cboAdminWarehouse.EditValue);
                model.UpdatedBy = Global.AppUserName;
                model.UpdatedDate = DateTime.Now;

                if (model.ID > 0)
                {
                    SQLHelper<HandoverMinutesModel>.Update(model);
                }
                else
                {
                    model.Code = $"BBBG.{DateTime.Now:yy}{DateTime.Now:MMdd}.{GetNextCodeNumber()}";
                    model.ID = SQLHelper<HandoverMinutesModel>.Insert(model).ID;
                }
                // Delete marked details
                if (lstDeletedDetails.Count > 0)
                {
                    foreach (var item in lstDeletedDetails)
                    {
                        SQLHelper<HandoverMinutesDetailModel>.DeleteModelByID(item);
                    }
                }

                for (int i = 0; i < grvDetails.RowCount; i++)
                {
                    int detailID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colID));
                    int pokhDetailID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colPOKHDetailID));
                    POKHDetailModel pOKHDetail = SQLHelper<POKHDetailModel>.FindByID(pokhDetailID);
                    HandoverMinutesDetailModel detailModel = SQLHelper<HandoverMinutesDetailModel>.FindByID(detailID);
                    detailModel.STT = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colSTT));
                    detailModel.HandoverMinutesID = model.ID;
                    detailModel.POKHID = pOKHDetail.POKHID;
                    detailModel.POKHDetailID = pokhDetailID;
                    detailModel.ProductSaleID = pOKHDetail.ProductID;
                    //detailModel.Quantity = pOKHDetail.Qty;
                    detailModel.Quantity = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colQuantity));
                    detailModel.ProductStatus = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colProductStatus));
                    detailModel.Guarantee = TextUtils.ToString(grvDetails.GetRowCellValue(i, colGuarantee));
                    detailModel.DeliveryStatus = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDeliveryStatus));
                    detailModel.STT = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colSTT));
                    detailModel.CreatedBy = Global.AppUserName;
                    detailModel.CreatedDate = DateTime.Now;
                    detailModel.UpdatedBy = Global.AppCodeName;
                    detailModel.UpdatedDate = DateTime.Now;
                    if (detailModel.ID > 0) SQLHelper<HandoverMinutesDetailModel>.Update(detailModel);
                    else detailModel.ID = SQLHelper<HandoverMinutesDetailModel>.Insert(detailModel).ID;
                }


                SaveEvent?.Invoke(model);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSaveAndClose_click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (save())
            {

                this.Close();
            }
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (save())
            {
                HandoverMinutesModel = new HandoverMinutesModel();
                loadData();
                loadHandoverDetail();
            }
        }

        private void cboCustomerName_EditValueChanged(object sender, EventArgs e)
        {
            int id = cboCustomerName.EditValue == null ? 0 : TextUtils.ToInt(cboCustomerName.EditValue);
            CustomerModel customer = lstCustomer.FirstOrDefault(p => p.ID == id);
            if (customer != null)
            {
                txtCustomerAddress.Text = customer.Address;
                txtCustomerContact.Text = customer.ContactName;
                txtCustomerPhone.Text = customer.ContactPhone;
            }
            else
            {
                txtCustomerAddress.Text = string.Empty;
                txtCustomerContact.Text = string.Empty;
                txtCustomerPhone.Text = string.Empty;
            }
        }

        private void cboEmployeeName_EditValueChanged(object sender, EventArgs e)
        {
            EmployeeModel employee = (EmployeeModel)cboEmployeeName.GetSelectedDataRow();
            if (employee != null)
            {
                txtEmployeePhone.Text = employee.SDTCaNhan;
                DepartmentModel department = SQLHelper<DepartmentModel>.FindAll().FirstOrDefault(c => c.ID == employee.DepartmentID);
                txtDepartmentName.Text = department?.Name ?? string.Empty;
                txtEmailCaNhan.Text = employee.EmailCaNhan;
            }
            else
            {
                txtEmailCaNhan.Text = string.Empty;
                txtEmployeePhone.Text = string.Empty;
                txtDepartmentName.Text = string.Empty;
            }
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = sender as SearchLookUpEdit;
            DataRowView dataRow = lookUpEdit.GetSelectedDataRow() as DataRowView;

            if (dataRow == null) return;


            int rowHandle = grvDetails.FocusedRowHandle;
            grvDetails.SetRowCellValue(rowHandle, colPOKHDetailID, dataRow["POKHDetailID"]);
            grvDetails.SetRowCellValue(rowHandle, colProductCode, dataRow["ProductCode"]);
            grvDetails.SetRowCellValue(rowHandle, colProductName, dataRow["ProductName"]);
            grvDetails.SetRowCellValue(rowHandle, colProductMaker, dataRow["Maker"]);
            grvDetails.SetRowCellValue(rowHandle, colQuantity, dataRow["Quantity"]);
            grvDetails.SetRowCellValue(rowHandle, colPOCode, dataRow["POKHDetailID"]);
            grvDetails.SetRowCellValue(rowHandle, colProductUnit, dataRow["Unit"]);

        }

        private void grvDetails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetails.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvDetails.FocusedRowHandle = -1;

                    DataRow dtrow = dtContent.NewRow();
                    dtrow["STT"] = grvDetails.RowCount + 1;
                    dtContent.Rows.Add(dtrow);

                    grdDetails.DataSource = dtContent;
                    dtContent.AcceptChanges();

                }
            }
        }


        private void frmHandoverMinutesDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnDelete_Click(object sender, EventArgs e)

        {
            int idDetail = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc là muốn xóa sản phẩm {TextUtils.ToString(grvDetails.GetFocusedRowCellValue(colProductName))} này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (idDetail > 0) lstDeletedDetails.Add(idDetail);
                grvDetails.DeleteSelectedRows();
            }
        }

        private void grvDetails_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view.FocusedColumn == colQuantity)
            //{
            //    decimal quantityPending = TextUtils.ToDecimal(view.GetFocusedRowCellValue(colQuantityPending));
            //    decimal quantity = TextUtils.ToDecimal(e.Value);
            //    if (quantity > quantityPending)
            //    {
            //        //grvDetails.BeginUpdate();
            //        e.Valid = false;
            //        e.ErrorText = "Bạn không thể giao nhiều hơn số lượng còn lại!";
            //        //grvDetails.EndUpdate();
            //    }
            //}
        }
    }
}