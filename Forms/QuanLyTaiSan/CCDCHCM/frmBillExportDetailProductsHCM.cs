using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmBillExportDetailProductsHCM : _Forms
	{
		int warehouseID = 0;
		int productGroupID = 141;

		public BillExportTechnicalModel billExport = new BillExportTechnicalModel();
		public List<int> totalRecords = new List<int>();

		DataTable dt = new DataTable();
		List<BillExportDetailTechnicalModel> listDeletes = new List<BillExportDetailTechnicalModel>();

		public frmBillExportDetailProductsHCM(int warehouseID)
		{
			InitializeComponent();
			this.warehouseID = warehouseID;
		}

		private void frmBillExportDetailProductsHCM_Load(object sender, EventArgs e)
		{
			LoadSupplierSale();
			LoadCustomer();
			LoadBillType();
			LoadReceiver();
			LoadProduct();
			LoadData();
		}

        void LoadData()
        {
            if (billExport.ID > 0)
            {
                txtCode.Text = billExport.Code;
                cboSupplierSale.EditValue = billExport.SupplierSaleID;
                txtWarehouseType.Text = billExport.WarehouseType;
                txtDeliver.Text = billExport.WarehouseType;
            }
            else
            {
                GetBillCode();
                txtDeliver.Text = Global.AppFullName;
            }

            cboBillType.SelectedIndex = TextUtils.ToInt(billExport.BillType);
            cboCustomer.EditValue = billExport.CustomerID;
            cboReceiver.EditValue = billExport.ReceiverID;
            txtReceiver.Text = billExport.Receiver;
            dtpCreatedDate.Value = billExport.CreatedDate.HasValue ? billExport.CreatedDate.Value : DateTime.Now;
            LoadDetail();
        }


        void LoadDetail()
        {
            dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetail_New", "A", new string[] { "@ID" }, new object[] { billExport.ID });
            grdData.DataSource = dt;
        }


        void GetBillCode()
        {
            if (billExport.ID > 0 && !Global.IsAdmin) return;
            int billtype = cboBillType.SelectedIndex;
            txtCode.Text = TextUtils.GetBillCode("BillExportTechnical", billtype);
            return;
        }


        void LoadSupplierSale()
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

        void LoadBillType()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Application.StartupPath, "billtype.txt"));
            cboBillType.Items.AddRange(lines);
            cboBillType.SelectedIndex = 0;
        }

        void LoadReceiver()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });

            cboReceiver.Properties.DataSource = dt;
            cboReceiver.Properties.ValueMember = "ID";
            cboReceiver.Properties.DisplayMember = "FullName";
        }


        void LoadProduct()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetInventoryDemo", "A",
                                        new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                        new object[] { productGroupID, "", -1, warehouseID });

            cboProduct.DisplayMember = "ProductCode";
            cboProduct.ValueMember = "ProductRTCID";
            cboProduct.DataSource = dt;
        }


        bool CheckValidate()
        {
            string billCode = txtCode.Text.Trim();

            if (cboBillType.SelectedIndex < 0)
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
                var exp1 = new Expression("Code", billCode);
                var exp2 = new Expression("ID", billExport.ID, "<>");
                var listBillExports = SQLHelper<BillExportTechnicalModel>.FindByExpression(exp1.And(exp2));
                if (listBillExports.Count > 0)
                {
                    GetBillCode();
                    MessageBox.Show($"Số phiếu đã được đổi thành [{txtCode.Text}]!", TextUtils.Caption);
                    //return false;
                }
            }

            if (TextUtils.ToInt(cboReceiver.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Người nhận.", TextUtils.Caption);
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
            grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;
            if (!CheckValidate()) return false;

            billExport.Code = txtCode.Text.Trim();
            billExport.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
            billExport.WarehouseType = txtWarehouseType.Text.Trim();
            billExport.ProjectName = txtProjectName.Text.Trim();
            billExport.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            billExport.BillType = cboBillType.SelectedIndex;
            billExport.ReceiverID = TextUtils.ToInt(cboReceiver.EditValue);
            billExport.Deliver = txtDeliver.Text.Trim();
            billExport.Receiver = txtReceiver.Text.Trim();
            billExport.CreatedDate = dtpCreatedDate.Value;
            billExport.WarehouseID = warehouseID;
            if (billExport.ID > 0)
            {
                var result = SQLHelper<BillExportTechnicalModel>.Update(billExport);
                totalRecords.Add(result.TotalRow);
            }
            else
            {
                var result = SQLHelper<BillExportTechnicalModel>.Insert(billExport);
                totalRecords.Add(result.TotalRow);
                billExport.ID = result.ID;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                BillExportDetailTechnicalModel detail = new BillExportDetailTechnicalModel();

                if (id > 0) detail = SQLHelper<BillExportDetailTechnicalModel>.FindByID(id);

                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.BillExportTechID = billExport.ID;
                detail.UnitName = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName)); ;
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.WarehouseID = warehouseID;
                if (detail.ID > 0)
                {
                    var result = SQLHelper<BillExportDetailTechnicalModel>.Update(detail);
                    totalRecords.Add(result.TotalRow);
                }
                else
                {
                    var result = SQLHelper<BillExportDetailTechnicalModel>.Insert(detail);
                    detail.ID = result.ID;
                    totalRecords.Add(result.TotalRow);

                }
            }

            if (listDeletes.Count > 0)
            {
                SQLHelper<BillExportDetailTechnicalModel>.DeleteListModel(listDeletes);
            }

            return true;
        }

		private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

		private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            if (SaveData())
            {
                billExport = new BillExportTechnicalModel();
                LoadData();
            }
        }

		private void btnReload_Click(object sender, EventArgs e)
		{
            GetBillCode();
        }

		private void cboBillType_SelectedIndexChanged(object sender, EventArgs e)
		{
            GetBillCode();
        }

		private void cboReceiver_EditValueChanged(object sender, EventArgs e)
		{
            var dataRowSelected = (DataRowView)cboReceiver.GetSelectedDataRow();
            string receiver = "";
            if (dataRowSelected != null) receiver = TextUtils.ToString(dataRowSelected["FullName"]);
            txtReceiver.Text = receiver;
        }

		private void cboProduct_EditValueChanged(object sender, EventArgs e)
		{
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            var dataRowSelected = (DataRowView)lookUpEdit.GetSelectedDataRow();

            string productCodeRTC = "";
            string productName = "";
            string unitName = "";
            string maker = "";
            int productID = 0;
            if (dataRowSelected != null)
            {
                productID = TextUtils.ToInt(dataRowSelected["ProductRTCID"]);
                productCodeRTC = TextUtils.ToString(dataRowSelected["ProductCodeRTC"]);
                productName = TextUtils.ToString(dataRowSelected["ProductName"]);
                unitName = TextUtils.ToString(dataRowSelected["UnitCountName"]);
                maker = TextUtils.ToString(dataRowSelected["Maker"]);
            }


            grvData.SetFocusedRowCellValue(colProductID, productID);
            grvData.SetFocusedRowCellValue(colProductCodeRTC, productCodeRTC);
            grvData.SetFocusedRowCellValue(colProductName, productName);
            grvData.SetFocusedRowCellValue(colUnitName, unitName);
            grvData.SetFocusedRowCellValue(colMaker, maker);
        }

		private void btnDelete_Click(object sender, EventArgs e)
		{
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa mã sản phẩm [{productCode}] không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();

                BillExportDetailTechnicalModel exportDetail = SQLHelper<BillExportDetailTechnicalModel>.FindByID(id);
                listDeletes.Add(exportDetail);
            }
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
                    dtrow["STT"] = stt + 1;
                    dt.Rows.Add(dtrow);
                }
            }
        }
	}
}
