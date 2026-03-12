using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmAssetDetail : _Forms
    {
        public TSAssetManagementModel asset = new TSAssetManagementModel();
        public TSAllocationEvictionAssetModel allocation = new TSAllocationEvictionAssetModel();
        public TSAssetModel tSAsset = new TSAssetModel();
        public DataTable quanly = new DataTable();
        public frmAssetDetail()
        {
            InitializeComponent();
        }

        #region Load Data
        private void frmAssetDetail_Load(object sender, EventArgs e)
        {
            cboOfficeActiveStatus.SelectedIndex = 0;
            cboWindowActiveStatus.SelectedIndex = 0;

            LoadcboPhongban();
            LoadcboQuanly();
            LoadcboLoaiTS();
            LoadUnits();
            LoadcboNguongoc();
            LoadcboNhaCungCap();
            LoadAssetManagement();

            if (asset.ID <= 0)
            {
                txtSTT.Value = TextUtils.ToDecimal(TextUtils.ExcuteScalar("SELECT TOP 1 STT FROM dbo.TSAssetManagement ORDER BY STT DESC")) + 1;
            }
        }
        void LoadAssetManagement()
        {
            if (asset.ID > 0)
            {
                ckbCapPhat.Enabled = false;
                ckbCapPhat.Checked = TextUtils.ToBoolean(asset.IsAllocation);
                cboPhongban.EditValue = asset.DepartmentID;
                cboQuanLy.EditValue = asset.EmployeeID;
                cboLoaiTS.EditValue = asset.TSAssetID;
                cboUnits.EditValue = asset.UnitID;
                txtMaTS.Text = asset.TSAssetCode;
                txtTenTS.Text = asset.TSAssetName;
                txtMaNCC.Text = asset.TSCodeNCC;
                cboNguonGoc.EditValue = asset.SourceID;
                txtSeri.Text = asset.Seri;
                txtQuyCach.Text = asset.SpecificationsAsset;
                cboNhaCungCap.EditValue = asset.SupplierID;
                dtpNgayMua.Value = asset.DateBuy.HasValue == true ? asset.DateBuy.Value : DateTime.Now;
                nmrThang.Value = TextUtils.ToInt(asset.Insurance);
                dtpHieuLuc.Value = asset.DateEffect.HasValue == true ? asset.DateEffect.Value : DateTime.Now;
                txtGhiChu.Text = asset.Note;

                txtSTT.Value = TextUtils.ToInt(asset.STT);


                cboOfficeActiveStatus.SelectedIndex = TextUtils.ToInt(asset.OfficeActiveStatus);
                cboWindowActiveStatus.SelectedIndex = TextUtils.ToInt(asset.WindowActiveStatus);
                txtModel.Text = asset.Model;
            }
        }
        void LoadcboPhongban()
        {
            DataTable phongban = TextUtils.Select("Select ID, Code, Name, HeadofDepartment from dbo.Department");
            cboPhongban.Properties.DataSource = phongban;
            cboPhongban.Properties.DisplayMember = "Name";
            cboPhongban.Properties.ValueMember = "ID";
        }
        void LoadcboLoaiTS()
        {
            DataTable TS = TextUtils.Select("Select ID, AssetCode, AssetType from dbo.TSAsset");
            cboLoaiTS.Properties.DataSource = TS;
            cboLoaiTS.Properties.DisplayMember = "AssetType";
            cboLoaiTS.Properties.ValueMember = "ID";
        }
        void LoadUnits()
        {
            DataTable unit = TextUtils.Select("Select ID, UnitCode, UnitName from dbo.UnitCount");
            cboUnits.Properties.DataSource = unit;
            cboUnits.Properties.DisplayMember = "UnitName";
            cboUnits.Properties.ValueMember = "ID";
        }

        void LoadcboNguongoc()
        {
            DataTable nguongoc = TextUtils.Select("Select ID, SourceCode, SourceName from dbo.TSSourceAsset");
            cboNguonGoc.Properties.DataSource = nguongoc;
            cboNguonGoc.Properties.DisplayMember = "SourceName";
            cboNguonGoc.Properties.ValueMember = "ID";
        }
        void LoadcboQuanly()
        {
            quanly = TextUtils.Select("Select ID, Code, FullName, DepartmentID, ChuVuID from dbo.Employee");
            cboQuanLy.Properties.DataSource = quanly;
            cboQuanLy.Properties.DisplayMember = "FullName";
            cboQuanLy.Properties.ValueMember = "ID";
        }
        void LoadcboNhaCungCap()
        {
            DataTable nhacungcap = TextUtils.Select("Select ID, SupplierName, SupplierCode from dbo.Supplier");
            cboNhaCungCap.Properties.DataSource = nhacungcap;
            cboNhaCungCap.Properties.DisplayMember = "SupplierName";
            cboNhaCungCap.Properties.ValueMember = "ID";
        }
        #endregion
        bool ValidateForm()
        {
            if (cboQuanLy.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên người quản lý !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboPhongban.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào phòng ban !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboNguonGoc.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào nguồn gốc tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboLoaiTS.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào loại tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtSeri.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào số Seri. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (TextUtils.CheckExistTable(asset.ID, "Seri", txtSeri.Text.Trim(), "TSAssetManagement"))
                {
                    MessageBox.Show("Số seri này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (txtMaNCC.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào mã nhà cung cấp. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                var exp1 = new Expression("TSCodeNCC", txtMaNCC.Text.Trim());
                var exp2 = new Expression("ID", asset.ID, "<>");

                var assets = SQLHelper<TSAssetManagementModel>.FindByExpression(exp1.And(exp2));
                if (assets.Count > 0)
                {
                    MessageBox.Show($"Mã nhà cung cấp [{txtMaNCC.Text.Trim()}] đã tồn tại. Vui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }
            }

            if (txtTenTS.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (TextUtils.ToInt(cboUnits.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng chọn Đơn vị!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (cboNhaCungCap.Text == "")
            //{
            //    MessageBox.Show(string.Format("Vui lòng nhập vào tên nhà cung cấp !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (txtModel.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào Model !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                var exp1 = new Expression("Model", txtModel.Text.Trim());
                var exp2 = new Expression("ID", asset.ID, "<>");
                var assets = SQLHelper<TSAssetManagementModel>.FindByExpression(exp1.And(exp2));
                if (assets.Count > 0)
                {
                    MessageBox.Show($"Model [{txtModel.Text.Trim()}] đã tồn tại. Vui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }
                var exp3 = new Expression("IsDeleted", 0);
                var exp4 = new Expression("ProductCode", txtModel.Text.Trim());
                ProductSaleModel productSaleModel = SQLHelper<ProductSaleModel>.FindByExpression(exp4.And(exp3)).FirstOrDefault() ?? new ProductSaleModel();
                if (productSaleModel.ID > 0)
                {
                    MessageBox.Show($"Mã sản phẩm [{txtModel.Text.Trim()}] đã tồn tại trong kho RTC. Vui lòng kiểm tra lại!", TextUtils.Caption);
                    return false;
                }
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;


            asset.IsAllocation = ckbCapPhat.Checked;
            asset.DepartmentID = TextUtils.ToInt(cboPhongban.EditValue);
            asset.EmployeeID = TextUtils.ToInt(cboQuanLy.EditValue);
            asset.TSAssetID = TextUtils.ToInt(cboLoaiTS.EditValue);
            asset.UnitID = TextUtils.ToInt(cboUnits.EditValue);
            asset.TSAssetCode = txtMaTS.Text.Trim();
            asset.TSCodeNCC = txtMaNCC.Text.Trim();
            asset.TSAssetName = txtTenTS.Text.Trim();
            asset.SourceID = TextUtils.ToInt(cboNguonGoc.EditValue);
            asset.Seri = txtSeri.Text.Trim();
            asset.SpecificationsAsset = txtQuyCach.Text.Trim();
            asset.SupplierID = TextUtils.ToInt(cboNhaCungCap.EditValue);
            asset.DateBuy = new DateTime(dtpNgayMua.Value.Year, dtpNgayMua.Value.Month, dtpNgayMua.Value.Day);
            asset.Insurance = TextUtils.ToInt(nmrThang.Value);
            asset.DateEffect = new DateTime(dtpHieuLuc.Value.Year, dtpHieuLuc.Value.Month, dtpHieuLuc.Value.Day);
            asset.STT = TextUtils.ToInt(txtSTT.Value);
            asset.Model = TextUtils.ToString(txtModel.Text.Trim());//ndnhat update 21/10/2025

            asset.WindowActiveStatus = TextUtils.ToInt(cboWindowActiveStatus.SelectedIndex); //VT.Nam update 09/12/2024
            asset.OfficeActiveStatus = TextUtils.ToInt(cboOfficeActiveStatus.SelectedIndex); //VT.Nam update 09/12/2024

            DataTable dt = TextUtils.Select($"Select StatusID, Status from dbo.TSAssetManagement Where ID = '{asset.ID}'");
            if (dt.Rows.Count == 0)
            {
                asset.Status = "Chưa sử dụng";
                asset.Note = txtGhiChu.Text.Trim();
                asset.StatusID = 1;
                //asset.ID = (int)TSAssetManagementBO.Instance.Insert(asset);
                asset.ID = SQLHelper<TSAssetManagementModel>.Insert(asset).ID;

                if (ckbCapPhat.Checked == true)
                {
                    allocation.AssetManagementID = asset.ID;
                    allocation.EmployeeID = TextUtils.ToInt(cboQuanLy.EditValue);
                    allocation.ChucVuID = TextUtils.ToInt(gridView1.GetFocusedRowCellValue(colChucVuID));
                    allocation.DepartmentID = TextUtils.ToInt(cboPhongban.EditValue);
                    allocation.DateAllocation = new DateTime(dtpHieuLuc.Value.Year, dtpHieuLuc.Value.Month, dtpHieuLuc.Value.Day);
                    allocation.Note = "Cấp phát thiết bị mới cho nhân viên ";
                    allocation.Status = "Đang sử dụng";
                    if (allocation.ID > 0)
                    {
                        //TSAllocationEvictionAssetBO.Instance.Update(allocation);
                        SQLHelper<TSAllocationEvictionAssetModel>.Update(allocation);
                    }
                    else
                    {
                        //allocation.ID = (int)TSAllocationEvictionAssetBO.Instance.Insert(allocation);
                        allocation.ID = SQLHelper<TSAllocationEvictionAssetModel>.Insert(allocation).ID;
                    }

                    //asset = (TSAssetManagementModel)TSAssetManagementBO.Instance.FindByPK(asset.ID);
                    asset = SQLHelper<TSAssetManagementModel>.FindByID(asset.ID);
                    asset.Status = "Đang sử dụng";
                    asset.StatusID = 2;
                    if (asset.ID > 0)
                    {
                        //TSAssetManagementBO.Instance.Update(asset);
                        SQLHelper<TSAssetManagementModel>.Update(asset);
                    }
                }
            }
            else
            {
                string status = TextUtils.ToString(dt.Rows[0]["Status"]);
                int statusID = TextUtils.ToInt(dt.Rows[0]["StatusID"]);
                asset.Status = status;
                asset.Note = txtGhiChu.Text.Trim();
                asset.StatusID = statusID;
                //TSAssetManagementBO.Instance.Update(asset);
                SQLHelper<TSAssetManagementModel>.Update(asset);
            }
            AutoInsertProductSale();
            return true;
        }


        void AutoInsertProductSale()
        {
            //ProductSaleModel productSale = new ProductSaleModel();
            //productSale.ProductName = txtTenTS.Text.Trim();
            var productgroupModel = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupID", "C").FirstOrDefault();
            int groupID = 0;
            if (productgroupModel != null)
            {
                groupID = productgroupModel.ID;
            }
            //productSale.ProductGroupID = groupID;
            //productSale.ProductNewCode = loadNewCode(groupID);
            //productSale.ProductCode = txtMaNCC.Text.Trim();
            //productSale.SupplierName = TextUtils.ToString(cboNhaCungCap.Properties.GetDisplayValueByKeyValue(cboNhaCungCap.EditValue));
            //productSale.Unit = TextUtils.ToString(cboUnits.Properties.GetDisplayValueByKeyValue(cboUnits.EditValue));

            ////Kiểm tra productsale ứng với asset có tồn tại trong db chưa (add or update)
            //if (asset.ID > 0)
            //{
            //    ProductSaleModel psModel = SQLHelper<ProductSaleModel>.FindByAttribute("ProductCode", asset.TSCodeNCC).FirstOrDefault();
            //    if (psModel != null)
            //    {
            //        psModel.ProductGroupID = productSale.ProductGroupID;
            //        psModel.ProductNewCode = productSale.ProductNewCode;
            //        psModel.ProductCode = productSale.ProductCode;
            //        psModel.SupplierName = productSale.SupplierName;
            //        psModel.Unit = productSale.Unit;
            //        ProductSaleBO.Instance.Update(psModel);
            //    }
            //    else
            //    {
            //        productSale.ID = (int)ProductSaleBO.Instance.Insert(productSale);
            //        InventoryModel inventoryModel = new InventoryModel();
            //        inventoryModel.WarehouseID = 1;
            //        inventoryModel.ProductSaleID = productSale.ID;
            //        InventoryBO.Instance.Insert(inventoryModel);
            //    }
            //}
            //else
            //{
            //    productSale.ID = (int)ProductSaleBO.Instance.Insert(productSale);
            //    InventoryModel inventoryModel = new InventoryModel();
            //    inventoryModel.WarehouseID = 1;
            //    inventoryModel.ProductSaleID = productSale.ID;
            //    InventoryBO.Instance.Insert(inventoryModel);
            //}

            string productCode = txtModel.Text.Trim();//NDNHAT update 21/10/2025
            var exp1 = new Expression("ProductGroupID", groupID);
            var exp2 = new Expression("ProductCode", productCode);
            ProductSaleModel product = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            product = product == null ? new ProductSaleModel() : product;
            product.ProductGroupID = groupID;

            product.ProductCode = txtModel.Text.Trim();
            product.ProductName = txtTenTS.Text.Trim();
            product.SupplierName = TextUtils.ToString(cboNhaCungCap.Properties.GetDisplayValueByKeyValue(cboNhaCungCap.EditValue));
            product.Unit = TextUtils.ToString(cboUnits.Properties.GetDisplayValueByKeyValue(cboUnits.EditValue));

            if (product.ID > 0)
            {
                SQLHelper<ProductSaleModel>.Update(product);
            }
            else
            {
                product.ProductNewCode = loadNewCode(groupID);
                product.ID = SQLHelper<ProductSaleModel>.Insert(product).ID;

                //productSale.ID = (int)ProductSaleBO.Instance.Insert(productSale);
                InventoryModel inventoryModel = new InventoryModel();
                inventoryModel.WarehouseID = 1;
                inventoryModel.ProductSaleID = product.ID;
                InventoryBO.Instance.Insert(inventoryModel);
            }
        }

        string loadNewCode(int groupID)
        {
            string _NewCodeRTC;

            DataSet ds = TextUtils.LoadDataSetFromSP("spLoadNewCodeRTC", new string[] { "@Group" }, new object[] { groupID });
            string code = "";
            string codeRTC = TextUtils.ToString(ds.Tables[1].Rows[0][0]);

            if (ds.Tables[0].Rows.Count == 0)
            {
                _NewCodeRTC = codeRTC + "000000001";
            }
            else
            {
                if (!codeRTC.Contains("HCM"))
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    for (int i = 0; i < (9 - stt.ToString().Length); i++)
                    {
                        codeRTC = codeRTC + "0";
                    }
                    _NewCodeRTC = codeRTC + stt.ToString();
                }
                else
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    string indexString = TextUtils.ToString(stt);
                    for (int i = 0; indexString.Length < code.Length; i++)
                    {
                        indexString = "0" + indexString;
                    }
                    _NewCodeRTC = codeRTC + indexString.ToString();
                }
            }
            return _NewCodeRTC;
        }
        #region Button
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cboLoaiTS_EditValueChanged(object sender, EventArgs e)
        {
            txtMaTS.Text = $"TS{DateTime.Now.ToString("yyyyMMddhhMMss")}";
        }

        private void cboQuanLy_EditValueChanged(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(cboQuanLy.EditValue);

            for (int i = 0; i < quanly.Rows.Count; i++)
            {
                if (TextUtils.ToInt(quanly.Rows[i]["ID"]) == ID)
                {
                    cboPhongban.EditValue = TextUtils.ToInt(quanly.Rows[i]["DepartmentID"]);
                }
            }
        }
        #endregion

        private void txtMaNCC_EditValueChanged(object sender, EventArgs e)
        {
            txtSeri.Text = txtMaNCC.Text;
        }
    }
}
