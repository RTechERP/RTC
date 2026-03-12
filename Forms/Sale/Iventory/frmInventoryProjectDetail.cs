using BMS.Model;
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
    public partial class frmInventoryProjectDetail : _Forms
    {

        private WarehouseModel _warehouse;
        public InventoryProjectModel inventoryProject = new InventoryProjectModel();
        public int productSaleID = 0;
        public frmInventoryProjectDetail(WarehouseModel warehouse)
        {
            InitializeComponent();
            _warehouse = warehouse;
        }

        private void frmInventoryProjectDetail_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadCustomer();
            LoadProductSale();
            LoadWarehouse();
            POKH();
            LoadData();
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = list;
        }

        void LoadCustomer()
        {

            //DataTable dt = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(x => x.ID).ToList();
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = list;
        }


        void LoadProductSale()
        {
            DataTable dt = TextUtils.LoadDataFromSP($"spGetInventory", "A",
                                                    new string[] { "@ID", "@Find", "@WarehouseCode", "@IsStock" },
                                                    new object[] { 0, "", _warehouse.WarehouseCode, 0 });



            cboProductSale.Properties.DisplayMember = "ProductCode";
            cboProductSale.Properties.ValueMember = "ProductSaleID";
            cboProductSale.Properties.DataSource = dt;


            cboProductSale.EditValue = productSaleID;
        }


        void LoadWarehouse()
        {
            List<WarehouseModel> listWarehouse = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouse.Properties.DataSource = listWarehouse;
            cboWarehouse.Properties.DisplayMember = "WarehouseName";
            cboWarehouse.Properties.ValueMember = "ID";
            cboWarehouse.EditValue = _warehouse.ID;
        }


        void POKH()
        {
            var list = TextUtils.GetTable("spGetPOKHDetailForInventoryProject");
            cboPOKHDetail.Properties.DisplayMember = "POCode";
            cboPOKHDetail.Properties.ValueMember = "ID";
            cboPOKHDetail.Properties.DataSource = list;
        }

        void LoadData()
        {
            if (inventoryProject.ID > 0)
            {
                cboProject.EditValue = inventoryProject.ProjectID;
                cboCustomer.EditValue = inventoryProject.CustomerID;
                cboProductSale.EditValue = inventoryProject.ProductSaleID;
                txtQuantity.Value = TextUtils.ToDecimal(inventoryProject.Quantity);
                cboWarehouse.EditValue = inventoryProject.WarehouseID;
                txtNote.Text = inventoryProject.Note;
                cboPOKHDetail.EditValue = inventoryProject.POKHDetailID;
            }
        }


        bool CheckValidate()
        {

            //if (txtQuantity.Value > txtTotalQuantityLast.Value)
            //{
            //    MessageBox.Show("Số lượng trong kho không đủ.\nVui lòng kiểm tra lại!", "Thông báo");
            //    return false;
            //}

            //if (txtQuantity.Value <= 0)
            //{
            //    MessageBox.Show("Vui lòng nhập Số lượng giữ!", "Thông báo");
            //    return false;
            //}
            return true;
        }

        bool SaveData()
        {
            try
            {
                if (!CheckValidate()) return false;
                inventoryProject.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                inventoryProject.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                inventoryProject.ProductSaleID = TextUtils.ToInt(cboProductSale.EditValue);
                inventoryProject.Quantity = txtQuantity.Value;
                inventoryProject.WarehouseID = TextUtils.ToInt(cboWarehouse.EditValue);
                inventoryProject.Note = txtNote.Text.Trim();
                inventoryProject.EmployeeID = Global.EmployeeID;
                inventoryProject.POKHDetailID = TextUtils.ToInt(cboPOKHDetail.EditValue);

                if (inventoryProject.ID <= 0) SQLHelper<InventoryProjectModel>.Insert(inventoryProject);
                else SQLHelper<InventoryProjectModel>.Update(inventoryProject);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData()) this.Close();
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                inventoryProject = new InventoryProjectModel();
                LoadData();
            }
        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView data = (DataRowView)cboProductSale.GetSelectedDataRow();

            string productName = "";
            string productNewCode = "";
            decimal totalQuantityLast = 0;

            if (data != null)
            {
                productName = TextUtils.ToString(data["ProductName"]);
                productNewCode = TextUtils.ToString(data["ProductNewCode"]);
                totalQuantityLast = TextUtils.ToDecimal(data["TotalQuantityLast"]);
            }

            txtProductName.Text = productName;
            txtProductNewCode.Text = productNewCode;
            txtTotalQuantityLast.Value = totalQuantityLast;

            if (inventoryProject.ID <= 0)
            {
                if (totalQuantityLast > 0)
                {
                    txtQuantity.Maximum = totalQuantityLast;
                    txtQuantity.Value = 1;
                }
                else
                {
                    txtQuantity.Maximum = 0;
                    txtQuantity.Value = 0;
                }
            }
            
        }
    }
}
