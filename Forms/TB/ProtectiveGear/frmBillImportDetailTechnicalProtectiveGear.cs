using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
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
    public partial class frmBillImportDetailTechnicalProtectiveGear : _Forms
    {
        int warehouseID = 0;
        public BillImportTechnicalModel billImport = new BillImportTechnicalModel();
        public List<int> totalRecords = new List<int>();

        DataTable dt = new DataTable();
        List<BillImportDetailTechnicalModel> listDeletes = new List<BillImportDetailTechnicalModel>();
        public frmBillImportDetailTechnicalProtectiveGear(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmBillImportDetailTechnicalProtectiveGear_Load(object sender, EventArgs e)
        {
            cboBillTypeNew.SelectedIndex = 0;

            LoadSupplier();
            LoadCustomer();
            LoadReceiverAndDeliver();
            LoadRulePay();
            LoadWarehouse();

            LoadProduct();

            LoadData();
        }


        void LoadData()
        {

            if (billImport.ID > 0)
            {
                txtBillCode.Text = billImport.BillCode;
                cboRulePay.EditValue = billImport.RulePayID;
                txtWarehouseType.Text = billImport.WarehouseType;
            }
            else GetBillCode();

            cboBillTypeNew.SelectedIndex = TextUtils.ToInt(billImport.BillTypeNew);
            cboSupplierSale.EditValue = billImport.SupplierSaleID;
            //txtWarehouseType.Text = billImport.ID <= 0 ? "Đồ bảo hộ" : billImport.WarehouseType;
            cboCustomer.EditValue = billImport.CustomerID;
            cboReceiver.EditValue = billImport.ReceiverID;
            cboDeliver.EditValue = billImport.DeliverID;
            dtpCreatDate.Value = billImport.CreatDate.HasValue ? billImport.CreatDate.Value : DateTime.Now;
            //cboRulePay.EditValue = billImport.RulePayID;

            //Load chi tiết
            LoadDetail();
        }


        void LoadDetail()
        {
            dt = TextUtils.LoadDataFromSP("spGetBillImportDetailTechnical", "A", new string[] { "@ID" }, new object[] { billImport.ID });
            grdData.DataSource = dt;
        }


        void GetBillCode()
        {
            if (billImport.ID > 0 && !Global.IsAdmin) return;
            int billtype = cboBillTypeNew.SelectedIndex;
            txtBillCode.Text = TextUtils.GetBillCode("BillImportTechnical", billtype);
            return;
        }

        void LoadSupplier()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.NgayUpdate).ToList();
            cboSupplierSale.Properties.ValueMember = "ID";
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
            cboSupplierSale.Properties.DataSource = list;
        }

        void LoadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(x => x.ID).ToList();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;
        }


        void LoadReceiverAndDeliver()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });
            cboReceiver.Properties.DisplayMember = cboDeliver.Properties.DisplayMember = "FullName";
            cboReceiver.Properties.ValueMember = cboDeliver.Properties.ValueMember = "ID";
            cboReceiver.Properties.DataSource = cboDeliver.Properties.DataSource = dt;

            cboReceiver.EditValue = Global.UserID;
        }


        void LoadRulePay()
        {
            var list = SQLHelper<RulePayModel>.FindAll();

            cboRulePay.Properties.ValueMember = "ID";
            cboRulePay.Properties.DisplayMember = "Note";
            cboRulePay.Properties.DataSource = list;

            cboRulePay.EditValue = 34;
        }

        void LoadWarehouse()
        {
            var list = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouse.Properties.ValueMember = "ID";
            cboWarehouse.Properties.DisplayMember = "WarehouseName";
            cboWarehouse.Properties.DataSource = list;

            cboWarehouse.EditValue = warehouseID;
        }


        void LoadProduct()
        {
            int productGroupID = 140;
            productGroupID = 0;
            string productGroupNo = "DBH";
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductRTC", "A",
                                new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID", "@ProductGroupNo" },
                                new object[] { productGroupID, "", 1, warehouseID, productGroupNo });


            cboProduct.ValueMember = "ID";
            cboProduct.DisplayMember = "ProductCode";
            cboProduct.DataSource = dt;
        }

        bool CheckValidate()
        {
            string billCode = txtBillCode.Text.Trim();

            if (cboBillTypeNew.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại phiếu.", TextUtils.Caption);
                return false;
            }

            if (string.IsNullOrWhiteSpace(billCode))
            {
                MessageBox.Show("Vui lòng nhập Số phiếu!\nClick nút bên cạnh số phiếu để tự động load tại số phiếu.", TextUtils.Caption);
                return false;
            }
            else
            {
                var exp1 = new Expression("BillCode", billCode);
                var exp2 = new Expression("ID", billImport.ID, "<>");
                var listBillImports = SQLHelper<BillImportTechnicalModel>.FindByExpression(exp1.And(exp2));
                if (listBillImports.Count > 0)
                {
                    GetBillCode();
                    MessageBox.Show($"Số phiếu đã được đổi thành [{txtBillCode.Text}]!", TextUtils.Caption);
                    //return false;
                }
            }


            //if (TextUtils.ToInt(cboSupplierSale.EditValue) <= 0 && TextUtils.ToInt(cboCustomer.EditValue) <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Nhà cung cấp hoặc Khách hàng!", TextUtils.Caption);
            //    return false;
            //}


            if (TextUtils.ToInt(cboReceiver.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người nhận.", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cboDeliver.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người giao.", TextUtils.Caption);
                return false;
            }

            if (TextUtils.ToInt(cboRulePay.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Điều khoản TT!", TextUtils.Caption);
                return false;
            }


            for (int i = 0; i < grvData.RowCount; i++)
            {
                int productID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));

                if (productID <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Mã sản phẩm!", TextUtils.Caption);
                    return false;
                }

                if (quantity <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Số lượng!", TextUtils.Caption);
                    return false;
                }
            }
            return true;
        }


        bool SaveData()
        {
            //grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;
            if (!CheckValidate()) return false;
            billImport.BillCode = txtBillCode.Text.Trim();
            billImport.WarehouseType = txtWarehouseType.Text.Trim();
            billImport.BillTypeNew = cboBillTypeNew.SelectedIndex;
            billImport.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
            billImport.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            billImport.ReceiverID = TextUtils.ToInt(cboReceiver.EditValue);
            billImport.Receiver = txtReceiver.Text.Trim();
            billImport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
            billImport.Deliver = txtDeliver.Text.Trim();
            billImport.CreatDate = dtpCreatDate.Value;
            billImport.RulePayID = TextUtils.ToInt(cboRulePay.EditValue);
            billImport.WarehouseID = warehouseID;

            if (billImport.ID > 0)
            {
                var result = SQLHelper<BillImportTechnicalModel>.Update(billImport);
                totalRecords.Add(result.TotalRow);
            }
            else
            {
                var result = SQLHelper<BillImportTechnicalModel>.Insert(billImport);
                totalRecords.Add(result.TotalRow);
                billImport.ID = result.ID;
            }

            //Lưu dữ liệu details
            for (int i = 0; i < grvData.RowCount; i++)
            {
                long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                BillImportDetailTechnicalModel detail = new BillImportDetailTechnicalModel();

                if (id > 0) detail = SQLHelper<BillImportDetailTechnicalModel>.FindByID(id);

                //detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.BillImportTechID = billImport.ID; //billImport.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));
                detail.Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.TotalQuantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQuantity));
                detail.UnitID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitName));
                detail.UnitName = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colUnitName));
                detail.HistoryProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colHistoryProductRTCID));
                detail.ProductRTCQRCodeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCQRCodeID));//HuyNV -5/11/2022

                detail.BillCodePO = TextUtils.ToString(grvData.GetRowCellValue(i, colBillCodePO));
                detail.PONCCDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPONCCDetailID));

                detail.EmployeeIDBorrow = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeIDBorrow));
                detail.DeadlineReturnNCC = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDeadlineReturnNCC));

                detail.WarehouseID = warehouseID;
                if (detail.ID > 0)
                {
                    var result = SQLHelper<BillImportDetailTechnicalModel>.Update(detail);
                    totalRecords.Add(result.TotalRow);
                }
                else
                {
                    var result = SQLHelper<BillImportDetailTechnicalModel>.Insert(detail);
                    detail.ID = result.ID;
                    totalRecords.Add(result.TotalRow);

                }

                //Update vào tồn kho
                var exp1 = new Expression("ProductRTCID", detail.ProductID);
                var exp2 = new Expression("WarehouseID", warehouseID);
                var check = SQLHelper<InventoryDemoModel>.FindByExpression(exp1.And(exp2));
                if (check.Count <= 0)
                {
                    InventoryDemoModel inventory = new InventoryDemoModel();
                    inventory.ProductRTCID = TextUtils.ToInt(detail.ProductID);
                    inventory.WarehouseID = warehouseID;
                    SQLHelper<InventoryDemoModel>.Insert(inventory);
                }
            }


            //Xóa sản phẩm
            if (listDeletes.Count > 0)
            {
                SQLHelper<BillImportDetailTechnicalModel>.DeleteListModel(listDeletes);
            }
            return true;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            GetBillCode();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                billImport = new BillImportTechnicalModel();
                LoadData();
            }
        }

        private void cboDeliver_EditValueChanged(object sender, EventArgs e)
        {
            txtDeliver.Text = cboDeliver.Text;
        }

        private void cboBillTypeNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBillCode();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductID));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();

                BillImportDetailTechnicalModel importDetail = SQLHelper<BillImportDetailTechnicalModel>.FindByID(id);

                listDeletes.Add(importDetail);
            }
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            //txtBillCode.Focus();
            //grvData.Focus();
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            //DataRow[] rows = dtProduct.Select("ID = " + id);
            //if (rows.Length > 0)
            //{
            //    string productName = TextUtils.ToString(rows[0]["ProductName"]);
            //    //string unitName = TextUtils.ToString(rows[0]["UnitCountID"]);
            //    string productCodeRTC = TextUtils.ToString(rows[0]["ProductCodeRTC"]);
            //    string maker = TextUtils.ToString(rows[0]["Maker"]);
            //    int idUnitCount = TextUtils.ToInt(rows[0]["UnitCountID"]);

            //    string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));

            //    int quantity = TextUtils.ToInt(rows[0]["NumberInStore"]);

            //    //DataTable dt = new DataTable();
            //    //dt = TextUtils.GetDataTableFromSP("spGetUnitNameProductRTC", new string[] { "@Id" }, new object[] { idUnitCount });

            //    grvData.SetFocusedRowCellValue(colProductName, productName);
            //    grvData.SetFocusedRowCellValue(colUnitCountID, idUnitCount);
            //    grvData.SetFocusedRowCellValue(colUnitName, unitName);
            //    grvData.SetFocusedRowCellValue(colProductCodeRTC, productCodeRTC);
            //    grvData.SetFocusedRowCellValue(colMaker, maker);
            //    grvData.SetFocusedRowCellValue(colQuantity, quantity);


            //}

            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;

            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();

            grvData.SetFocusedRowCellValue(colProductID, TextUtils.ToString(dataRow["ID"]));
            grvData.SetFocusedRowCellValue(colProductName, TextUtils.ToString(dataRow["ProductName"]));
            grvData.SetFocusedRowCellValue(colUnitName, TextUtils.ToString(dataRow["UnitCountName"]));
            grvData.SetFocusedRowCellValue(colProductCodeRTC, TextUtils.ToString(dataRow["ProductCodeRTC"]));
            grvData.SetFocusedRowCellValue(colMaker, TextUtils.ToString(dataRow["Maker"]));
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    int stt = 0;
                    if (dt.Rows.Count > 0)
                    {
                        stt = dt.AsEnumerable().Max(x => x.Field<int>("STT"));
                    }

                    //int projectID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectID));
                    //string projectName = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectName));

                    dtrow["STT"] = stt + 1;
                    //dtrow["ProjectID"] = projectID;
                    //dtrow["ProjectName"] = projectName;
                    dt.Rows.Add(dtrow);
                }
            }
        }

        private void dtpCreatDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
