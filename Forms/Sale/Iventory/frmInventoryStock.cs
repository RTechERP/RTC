using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Business;
using BMS.Model;
using BMS.Utils;

namespace BMS
{
    public partial class frmInventoryStock : _Forms
    {
        public InventoryModel inventory = new InventoryModel();
        public InventoryStockModel inventoryStock = new InventoryStockModel(); //PQ.Chien - update - 28/07/2025
        //public int productSaleID;
        //public int warehouseID;
        public frmInventoryStock()
        {
            InitializeComponent();
        }

        private void frmInventoryStock_Load(object sender, EventArgs e)
        {

            LoadWareHouse();
            LoadProductSale();
            LoadEmployeeStock();  //PQ.Chien - update - 28/07/2025
            LoadProjectType();


            LoadDataDetails();
            //LogActions();  //PQ.Chien - update - 28/07/2025
        }

        //PQ.Chien - update - 28/07/2025
        private void LoadEmployeeStock()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindAll();
            cboEmployeeStock.Properties.DataSource = list;
            cboEmployeeStock.Properties.ValueMember = "ID";
            cboEmployeeStock.Properties.DisplayMember = "FullName";


            if (TextUtils.ToInt(cboEmployeeStock.EditValue) != 0)
            {
                cboEmployeeStock.Enabled = false;
            }
            else
            {
                cboEmployeeStock.EditValue = Global.EmployeeID;
            }

        }
        //END

        private void LoadWareHouse()
        {
            List<WarehouseModel> lst = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouse.Properties.DataSource = lst;
            cboWarehouse.Properties.ValueMember = "ID";
            cboWarehouse.Properties.DisplayMember = "WarehouseName";

            cboWarehouse.EditValue = inventory.WarehouseID;
        }
        private void LoadProductSale()
        {
            //List<ProductSaleModel> lst = SQLHelper<ProductSaleModel>.ProcedureToList("spGetAllProductSaleByWareHouseID", 
            //                                                        new string[] { "@WarehouseID" }, new object[] { inventory.WarehouseID });

            DataTable dt = TextUtils.LoadDataFromSP("spGetAllProductSaleByWareHouseID", "A",
                                                      new string[] { "@WarehouseID" }, new object[] { inventory.WarehouseID });
            cboProductSale.Properties.DataSource = dt;
            cboProductSale.Properties.ValueMember = "ProductSaleID"; //PQ.Chien - update - 09/08/2025
            //cboProductSale.Properties.ValueMember = "ID";
            cboProductSale.Properties.DisplayMember = "ProductCode";

            //cboProductSale.EditValue = inventory.ID;

            //cboProductSale.EditValue = inventory.ProductSaleID; //PQ.Chien - update - 09/08/2025
            cboProductSale.EditValue = inventoryStock.ProductSaleID;//ndnhat update - 18/08/2025
        }

        void LoadProjectType()
        {
            List<ProjectTypeModel> projects = SQLHelper<ProjectTypeModel>.FindAll();
            cboProjectType.Properties.DisplayMember = "ProjectTypeName";
            cboProjectType.Properties.ValueMember = "ID";
            cboProjectType.Properties.DataSource = projects;
        }

        private void LoadDataDetails()
        {

            //var dataRow = (DataRowView)cboProductSale.GetSelectedDataRow();

            //Expression e1 = new Expression("WarehouseID", dataRow == null ? 0: TextUtils.ToInt(dataRow["WarehouseID"]));
            //Expression e2 = new Expression("ProductSaleID", TextUtils.ToInt(cboProductSale.EditValue));
            //inventory = SQLHelper<InventoryModel>.FindByExpression(e1.And(e2)).FirstOrDefault() ?? new InventoryModel();

            //int id = dataRow == null ? 0 : TextUtils.ToInt(dataRow["ID"]);
            //int id = TextUtils.ToInt(cboProductSale.EditValue);
            //inventory = SQLHelper<InventoryModel>.FindByID(id);

            ////inventory = inventory ?? new InventoryModel();
            //if (inventory.ID > 0)
            //{
            //    txtNote.Text = inventory.Note;
            //    txtMinQuantity.EditValue = inventory.MinQuantity;
            //    ckbIsStock.Checked = inventory.IsStock;

            //    InventoryStockModel inventStock = SQLHelper<InventoryStockModel>.FindByAttribute("InventoryID", inventory.ID).FirstOrDefault();
            //    txtMinQuantityActual.EditValue = inventStock != null ? inventStock.Quantity : 0;
            //    cboEmployeeStock.EditValue = inventStock != null ? inventStock.EmployeeStock : 0;  //PQ.Chien - update - 28/07/2025
            //}

            //ndnhat update - 18/08/2025
            if (inventoryStock.ID > 0)
            {
                btnSave.Enabled = btnSaveNew.Enabled = !(inventoryStock.EmployeeIDRequest != Global.EmployeeID && !Global.IsAdmin);
                cboEmployeeStock.EditValue = inventoryStock.EmployeeIDRequest;
                cboProductSale.EditValue = inventoryStock.ProductSaleID;
                txtNote.Text = inventoryStock.Note;
                txtMinQuantity.EditValue = inventoryStock.Quantity;
                //cboWarehouse.EditValue = inventoryStock.WarehouseID;
                cboWarehouse.EditValue = 1;
                cboProjectType.EditValue = inventoryStock.ProjectTypeID;
            }

        }

        //ndnhat update - 18/08/2025
        bool ValidateData()
        {
            if (TextUtils.ToInt(cboProductSale.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Sản phẩm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboWarehouse.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Kho!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboProjectType.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Loại dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboEmployeeStock.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Người yêu cầu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToDecimal(txtMinQuantity.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Tồn tối thiểu Y/c!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //ndnhat update -27/08/2025
            var exp1 = new Expression(InventoryStockModel_Enum.ProductSaleID, TextUtils.ToInt(cboProductSale.EditValue));
            var exp2 = new Expression(InventoryStockModel_Enum.WarehouseID, TextUtils.ToInt(cboWarehouse.EditValue));
            var exp3 = new Expression(InventoryStockModel_Enum.ProjectTypeID, TextUtils.ToInt(cboProjectType.EditValue));
            var exp4 = new Expression(InventoryStockModel_Enum.EmployeeIDRequest, TextUtils.ToInt(cboEmployeeStock.EditValue));
            var exp5 = new Expression(InventoryStockModel_Enum.IsDeleted, 0);
            var exp6 = new Expression(InventoryStockModel_Enum.ID, inventoryStock.ID, "<>");
            var existingRecords = SQLHelper<InventoryStockModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5).And(exp6));
            if (existingRecords.Count > 0)
            {
                MessageBox.Show($"Thiết bị mã [{cboProductSale.Text}] đã được nhân viên [{cboEmployeeStock.Text}] yêu cầu cho loại [{cboProjectType.Text}]!", "Thông báo");
                return false;
            }
            if (inventoryStock.ID > 0 && inventoryStock.EmployeeIDRequest != Global.EmployeeID && !Global.IsAdmin)
            {
                MessageBox.Show($"Bạn không thể sửa yêu cầu của nhân viên khác!", "Thông báo");
                return false;
            }
            //end ndnhat update -27/08/2025
            return true;
        }

        private bool SaveData()
        {
            //decimal minQuantity = TextUtils.ToDecimal(txtMinQuantity.EditValue);

            //if (minQuantity <= 0 && ckbIsStock.Checked)
            //{
            //    MessageBox.Show(string.Format($"Số lượng tồn tối thiểu phải > 0"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            //if (ckbIsStock.Checked)
            //{
            //    //Expression e1 = new Expression("WarehouseID", inventory.WarehouseID);
            //    //Expression e2 = new Expression("ProductSaleID", TextUtils.ToInt( cboProductSale.EditValue));
            //    //InventoryModel model = SQLHelper<InventoryModel>.FindByExpression(e1.And(e2)).FirstOrDefault() ?? new InventoryModel();
            //    if (inventory.ID == 0) return true;

            //    inventory.MinQuantity = TextUtils.ToDecimal(txtMinQuantity.EditValue);
            //    inventory.Note = txtNote.Text;
            //    inventory.IsStock = (ckbIsStock.Checked || inventory.MinQuantity > 0);

            //    InventoryBO.Instance.Update(inventory);

            //    //InventoryStockModel stock = SQLHelper<InventoryStockModel>.FindByAttribute("InventoryID", inventory.ID).FirstOrDefault() ?? new InventoryStockModel();
            //    //stock.Quantity = TextUtils.ToDecimal(txtMinQuantityActual.EditValue);
            //    //stock.InventoryID = inventory.ID;

            //    ////PQ.Chien - update - 28/07/2025
            //    //stock.EmployeeStock = TextUtils.ToInt(cboEmployeeStock.EditValue);
            //    //if (Global.EmployeeID != stock.EmployeeStock)
            //    //{
            //    //    MessageBox.Show("Chỉ người nhập stock mới có thể update số lượng");
            //    //    return false;
            //    //}
            //    ////END

            //    //if (stock.ID > 0) SQLHelper<InventoryStockModel>.Update(stock);
            //    //else SQLHelper<InventoryStockModel>.Insert(stock);
            //}

            //ndnhat update - 18/08/2025
            if (!ValidateData()) return false;

            inventoryStock.ProductSaleID = TextUtils.ToInt(cboProductSale.EditValue);
            inventoryStock.WarehouseID = TextUtils.ToInt(cboWarehouse.EditValue);
            inventoryStock.Quantity = TextUtils.ToDecimal(txtMinQuantity.EditValue);
            inventoryStock.ProjectTypeID = TextUtils.ToInt(cboProjectType.EditValue);
            inventoryStock.EmployeeIDRequest = TextUtils.ToInt(cboEmployeeStock.EditValue);
            inventoryStock.Note = txtNote.Text;

            if (inventoryStock.ID <= 0)
            {
                SQLHelper<InventoryStockModel>.Insert(inventoryStock);
            }
            else
            {
                SQLHelper<InventoryStockModel>.Update(inventoryStock);
            }

            //InventoryStockModel stock = SQLHelper<InventoryStockModel>.FindByAttribute("InventoryID", inventory.ID).FirstOrDefault() ?? new InventoryStockModel();
            //stock.Quantity = TextUtils.ToDecimal(txtMinQuantityActual.EditValue);
            //stock.InventoryID = inventory.ID;

            ////PQ.Chien - update - 28/07/2025
            //stock.EmployeeStock = TextUtils.ToInt(cboEmployeeStock.EditValue);
            //if (Global.EmployeeID != stock.EmployeeStock)
            //{
            //    MessageBox.Show("Chỉ người nhập stock mới có thể update số lượng");
            //    return false;
            //}
            ////END

            //if (stock.ID > 0) SQLHelper<InventoryStockModel>.Update(stock);
            //else SQLHelper<InventoryStockModel>.Insert(stock);

            return true;
        }


        //PQ.Chien - update - 09/08/2025
        //private bool SaveData()
        //{
        //    try
        //    {
        //        decimal minQuantity = TextUtils.ToDecimal(txtMinQuantity.EditValue);

        //        if (minQuantity <= 0)
        //        {
        //            MessageBox.Show(string.Format($"Số lượng tồn tối thiểu phải > 0"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return false;
        //        }
        //        var data = SQLHelper<InventoryModel>.FindByAttribute("ProductSaleID", TextUtils.ToInt(cboProductSale.EditValue)).FirstOrDefault();

        //        if (data != null)
        //        {
        //            data.IsStock = true;
        //            data.WarehouseID = TextUtils.ToInt(cboWarehouse.EditValue);
        //            data.Note = txtNote.Text;
        //            data.MinQuantity = TextUtils.ToDecimal(txtMinQuantity.EditValue);
        //        }
        //        SQLHelper<InventoryModel>.Update(data);

        //        //InventoryStockModel stock = SQLHelper<InventoryStockModel>.FindByAttribute("InventoryID", data.ID).FirstOrDefault() ?? new InventoryStockModel();


        //        //stock.Quantity = TextUtils.ToDecimal(txtMinQuantityActual.EditValue);
        //        //stock.InventoryID = inventory.ID;
        //        //stock.EmployeeStock = TextUtils.ToInt(cboEmployeeStock.EditValue);

        //        //// Kiểm tra quyền cập nhật stock
        //        //if (stock.ID > 0 && Global.EmployeeID != stock.EmployeeStock)
        //        //{
        //        //    MessageBox.Show("Chỉ người nhập stock mới có thể update số lượng",
        //        //                   TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //    return false;
        //        //}

        //        //// Lưu hoặc cập nhật InventoryStock
        //        //if (stock.ID > 0)
        //        //{
        //        //    SQLHelper<InventoryStockModel>.Update(stock);
        //        //}
        //        //else
        //        //{
        //        //    SQLHelper<InventoryStockModel>.Insert(stock);
        //        //}

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}",
        //                       TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}
        //END


        private void reset()
        {
            //txtMinQuantity.EditValue = 0;
            //txtNote.Clear();
            //ckbIsStock.Checked = false;

            txtMinQuantity.EditValue = 0;
            txtNote.Clear();
            cboProductSale.EditValue = null;
            cboWarehouse.EditValue = 1;
            cboProjectType.EditValue = null;
            cboEmployeeStock.EditValue = Global.EmployeeID; //PQ.Chien - update - 28/07/2025
                                                            //ckbIsStock.Checked = false;
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData()) reset();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtMinQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataDetails();
        }

        //PQ.Chien - update - 28/07/2025
        private void LogActions()
        {
            try
            {
                var controls = new Component[] { btnSave, btnSaveNew };
                var actions = Enumerable.Repeat<string>("Update", controls.Length).ToArray();
                var logDatas = Enumerable.Repeat<Func<dynamic, dynamic>>(GetDataChange, controls.Length).ToArray();
                var initialData = GetCurrentData();
                var logger = new Logger(controls, actions, logDatas, this, initialData);
                logger.Start();
            }
            catch
            {

            }
        }

        private dynamic GetDataChange(dynamic oldData)
        {
            var oldDataLog = (InventoryStockLog)oldData;
            var newDataLog = GetCurrentData();
            return new
            {
                InventoryStock = new
                {
                    Old = oldDataLog.InventoryStock,
                    New = newDataLog.InventoryStock
                },

            };
        }

        public class InventoryStockLog
        {
            public InventoryStockModel InventoryStock;
        }

        private InventoryStockLog GetCurrentData()
        {
            var data = new InventoryStockLog();
            var inventoryStockLog = new InventoryStockModel();
            inventoryStockLog.ID = inventoryStock.ID;

            inventoryStockLog.EmployeeStock = TextUtils.ToInt(cboEmployeeStock.EditValue);
            inventoryStockLog.UpdatedBy = Global.AppUserName;
            inventoryStockLog.UpdatedDate = DateTime.Now;
            inventoryStockLog.InventoryID = inventory.ID;

            data.InventoryStock = inventoryStockLog;
            return data;
        }
        //END

    }
}
