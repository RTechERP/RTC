using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ExcelDataReader;
using Forms.Classes;
using MSScriptControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BMS
{
    public partial class frmBillExportDetail : _Forms
    {
        #region Variables
        public int IDDetail;
        public BillExportModel billExport = new BillExportModel();
        DataTable dtCustomer = new DataTable();
        DataTable dtProduct = new DataTable();
        DataTable dtProject = new DataTable();
        ArrayList lstIDDelete = new ArrayList();
        public BillImportModel billImport = new BillImportModel();
        public delegate void ListBillExportDetail(string group, DataTable dtData);
        public List<int> lstBillImportID = new List<int>();

        public string WarehouseCode;
        string projectCode;

        public int customerID = 0;
        public int KhoTypeID = 0;
        public DataTable dtDetail = new DataTable();
        public int saleAdminID = 0;

        public bool isPOKH = false;

        WarehouseModel warehouse = new WarehouseModel();


        public DataTable dtClone = new DataTable();
        public bool isAddExport = false;
        public int suplierId = 0;
        public int warehouseTypeId = 0;


        //public BillImportModel billImport = new BillImportModel();
        #endregion

        public frmBillExportDetail()
        {
            InitializeComponent();
        }

        private void frmBillExportDetail_Load(object sender, EventArgs e)
        {
            warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode.Trim()).FirstOrDefault();
            this.Text += " - " + WarehouseCode;

            coltreeListColumn1.ColumnEdit = cbProduct;
            coltreeListColumn6.ColumnEdit = cbProject;

            LoadSupplierSale();
            loadProductGroup();
            loadCustomer();
            loadSender();
            loadUsers();
            loadKhoType();
            loadProject();
            loadProductType();
            LoadStatus();
            if (lstBillImportID.Count == 0)
            {
                loadBillExportDetail();
            }
            else
            {
                loadBillImportDetail();
            }

            //if (txtCode.Text == "")
            //{
            //    loadBilllNumber();
            //}

            if (!cboGroup.Visible)
            {
                //this.tableLayoutPanel1.SetColumnSpan(cboStatusNew, 2);
            }

            this.cbProduct.EditValueChanged += new System.EventHandler(cboProduct_EditValueChanged);


            // KHI ĐƯỢC duyệt thì sẽ ẩn các button 
            btnSaveNew.Enabled = btnCloseSave.Enabled = btnNewProduct.Enabled = !TextUtils.ToBoolean(billExport.IsApproved);
            if (billExport.ID <= 0)
            {
                //cboStatus.SelectedIndex = 2;
                cboStatusNew.EditValue = 2;
            }


            if (KhoTypeID > 0)
            {
                cboStatusNew.EditValue = 6;
                EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(saleAdminID);
                cboUser.EditValue = employee.UserID;
            }

            if (isPOKH)
            {
                cboStatusNew.EditValue = 6;
            }


            if (isAddExport)
            {
                cbProject.EditValueChanged -= new EventHandler(cbProject_EditValueChanged);
                cbProduct.EditValueChanged -= new EventHandler(cboProduct_EditValueChanged);
                if (billImport != null)
                {
                    cboSupplier.EditValue = billImport.SupplierID;
                    cboUser.EditValue = billImport.ReciverID;
                    cboSender.EditValue = billImport.DeliverID;
                }
                cbProductType.EditValue = 2;
                dtpRequestDate.EditValue = DateTime.Now;
                cbKhoType.EditValue = warehouseTypeId;
                cboStatusNew.EditValue = 5;
                grdData.DataSource = dtClone;
                cbProject.EditValueChanged += new EventHandler(cbProject_EditValueChanged);
                cbProduct.EditValueChanged += new EventHandler(cboProduct_EditValueChanged);
                dtDetail = (DataTable)grdData.DataSource;
            }
        }

        #region Methods
        void LoadSupplierSale()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM SupplierSale ");
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll();
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = list;
        }

        private void LoadStatus()
        {
            List<object> list = new List<object>() {
                new {ID = 0,Name = "Mượn"},
                new {ID = 1,Name = "Tồn Kho"},
                new {ID = 2,Name = "Đã Xuất Kho"},
                //new {ID = 3,Name = "Chia Trước"},
                //new {ID = 4,Name = "Phiếu mượn nội bộ"},
                new {ID = 5,Name = "Xuất trả NCC"},
                new {ID = 6,Name = "Yêu cầu xuất kho"},
            };
            cboStatusNew.Properties.DataSource = list;
            cboStatusNew.Properties.ValueMember = "ID";
            cboStatusNew.Properties.DisplayMember = "Name";
        }

        /// <summary>
        /// load bill Export Detail
        /// </summary>
        private void loadBillExportDetail()
        {
            //if (WarehouseCode == "HCM") cboSender.EditValue = Global.UserID;
            if (WarehouseCode == "HCM") cboSender.EditValue = 88;
            //cboGroup.SetEditValue(billExport.GroupID);

            //TODO 27/10/2023: QUANG UPDATE GỘP TRẠNG THÁI CHIA TRƯỚC VÀO TỒN KHO VÀ MƯỢN NỘI BỘ VÀO MƯỢN
            //int _selectedIndex = -1;
            //if (billExport.Status == 0 || billExport.Status == 4)
            //{
            //	_selectedIndex = 0;
            //}
            //else if (billExport.Status == 1 || billExport.Status == 3)
            //{
            //	_selectedIndex = 1;
            //}
            //else
            //{
            //	_selectedIndex = 2;
            //}
            //cboStatus.SelectedIndex = billExport.Status;
            //cboStatus.SelectedIndex = _selectedIndex;
            //cbKhoType.Text = billExport.WarehouseType;

            txtCode.Text = billExport.Code;
            txtAddress.Text = billExport.Address;
            cboCustomer.EditValue = billExport.CustomerID;
            cbAdressStock.EditValue = billExport.AddressStockID;
            cboUser.EditValue = billExport.UserID;
            cboSender.EditValue = billExport.SenderID;
            cbKhoType.EditValue = billExport.KhoTypeID;
            cbProductType.EditValue = billExport.ProductType;
            ckbMerge.Checked = TextUtils.ToBoolean(billExport.IsMerge);

            cboStatusNew.EditValue = billExport.Status;
            cboSupplier.EditValue = billExport.SupplierID;
            if (KhoTypeID > 0) //PhucLH update 20/05/2024
            {
                cbKhoType.EditValue = KhoTypeID;
                cboCustomer.EditValue = customerID;
                cboStatusNew.EditValue = 6;
                cbProductType.EditValue = 1;

                dtDetail.DefaultView.Sort = "STT ASC";
                dtDetail = dtDetail.DefaultView.ToTable();
                grdData.DataSource = dtDetail;
            }
            else if (isPOKH) //lh Phuc 06/06/2024
            {
                dtDetail.DefaultView.Sort = "STT ASC";
                dtDetail = dtDetail.DefaultView.ToTable();
                dtDetail.AcceptChanges();
                grdData.DataSource = dtDetail;
                grvData.CloseEditor();
                grvData.FocusedRowHandle = -1;
                dtpRequestDate.EditValue = billExport.RequestDate;

                treeList1.DataSource = dtDetail;
                treeList1.ExpandAll();
            }
            else
            {
                // load data detail
                //DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { billExport.ID });
                dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { billExport.ID });
                grdData.DataSource = dtDetail;
                //if (dtDetail.Rows.Count == 0) return;
                //txtDateTime.Text = TextUtils.ToString(billExport.CreatDate);

                //if (billExport.Status != 6)
                {
                    dtpCreatDate.Value = !billExport.CreatDate.HasValue ? DateTime.Now : billExport.CreatDate.Value;
                    //dtpCreatDate.EditValue = !billExport.CreatDate.HasValue ? DateTime.Now : billExport.CreatDate.Value;
                }

                //dtpCreatDate.EditValue = billExport.CreatDate;
                dtpRequestDate.EditValue = billExport.RequestDate;
            }
            //// load data detail
            //DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { billExport.ID });
            //grdData.DataSource = dt;
            //if (dt.Rows.Count == 0) return;
            ////txtDateTime.Text = TextUtils.ToString(billExport.CreatDate);
            //dtpCreatDate.Value = !billExport.CreatDate.HasValue ? DateTime.Now : billExport.CreatDate.Value;
            //dtpRequestDate.EditValue = billExport.RequestDate;
        }
        /// <summary>
        /// convert import->export
        /// </summary>
        void loadBillImportDetail()
        {
            cbKhoType.EditValue = billImport.KhoTypeID;
            cboGroup.SetEditValue(billImport.GroupID);

            //dtpCreatDate.EditValue = DateTime.Now;
            string ID = string.Join(",", lstBillImportID);
            DataTable dtconvert = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });
            //dtconvert.Columns.Add("ProductFullName");
            dtconvert.Columns.Remove("ID");
            grdData.DataSource = dtconvert;
        }
        /// <summary>
        /// load nhóm
        /// </summary>
        void loadProductGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup where IsVisible=1 ");
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            cboGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load kho type
        /// </summary>
        void loadKhoType()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            cbKhoType.Properties.DisplayMember = "ProductGroupName";
            cbKhoType.Properties.ValueMember = "ID";
            cbKhoType.Properties.DataSource = dt;
        }

        void loadProductType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("ProductType");
            dt.Rows.Add("1", "Hàng thương mại");
            dt.Rows.Add("2", "Hàng dự án");
            cbProductType.Properties.DisplayMember = "ProductType";
            cbProductType.Properties.ValueMember = "ID";
            cbProductType.Properties.DataSource = dt;
        }

        /// <summary>
        /// load khách hàng
        /// </summary>

        private void loadCustomer()
        {
            dtCustomer = new DataTable();
            dtCustomer = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dtCustomer;
        }

        /// <summary>
        /// load nhân viên, người giao
        /// </summary>
        public void loadUsers()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM Users");

            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }
        public void loadSender()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cboSender.Properties.DisplayMember = "FullName";
            cboSender.Properties.ValueMember = "ID";
            cboSender.Properties.DataSource = dt;
        }
        /// <summary>
        /// lựa chọn mã dự án
        /// </summary>
        private void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,ProjectName FROM Project");
            cbProject.DisplayMember = "ProjectName";
            cbProject.ValueMember = "ID";
            cbProject.DataSource = dtProject;
            colProjectID.ColumnEdit = cbProject;
        }

        string[] preBillCodes = new string[]
        {
            "PM",//0
            "PXK", //1
            "PXK", //2
            "PCT", //3
            "PMNB", //4
            "PXM", //5
            "PXK" //6
        };
        /// <summary>
        /// hàm dùng load số phiếu
        /// </summary>
        /// 
        void loadBilllNumber()
        {
            //if (billExport.ID > 0 && !Global.IsAdmin) return;
            //int billtype = TextUtils.ToInt(cboStatusNew.EditValue);

            //if (!Global.DebugFlag)
            //{
            //    txtCode.Text = TextUtils.GetBillCode("BillExport", billtype);
            //    return;
            //}

            int billtype = TextUtils.ToInt(cboStatusNew.EditValue);
            if (billExport.ID > 0)
            {
                string preCodeNew = preBillCodes[billtype];
                string preCodeOld = preBillCodes[TextUtils.ToInt(billExport.Status)];

                string code = billExport.Code.Replace(preCodeOld.ToString(), preCodeNew.ToString());
                txtCode.Text = code;
            }
            else
            {
                //if (!Global.DebugFlag)
                {
                    txtCode.Text = TextUtils.GetBillCode("BillExport", billtype);
                    return;
                }
            }

            return;

            int so = 0;
            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;
            //string date = "220602";
            //string date = txtDateTime.Value.ToString("yyMMdd");
            string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExport Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} ORDER BY ID DESC"));
            //string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExport Where Month(CreatedDate)=6 and Year(CreatedDate)=2022 and Day(CreatedDate)=2 ORDER BY ID DESC"));

            if (Billcode.Contains("PM"))
            {

                Billcode = Billcode.Substring(2);
            }
            else if (Billcode.Contains("PXK") || Billcode.Contains("PCT"))
            {

                Billcode = Billcode.Substring(3);
            }

            if (billExport.ID == 0)
            {
                if (Billcode == "") // ktra tháng bdau và tháng đc update
                {

                    //if (cboStatus.SelectedIndex == 0)
                    //{
                    //	txtCode.Text = "PM" + date + "001";
                    //}
                    //else if (cboStatus.SelectedIndex == 3)
                    //{
                    //	txtCode.Text = "PCT" + date + "001";
                    //}
                    //else if (cboStatus.SelectedIndex == 4)
                    //{
                    //	txtCode.Text = "PMNB" + date + "001";
                    //}
                    //else
                    //{
                    //	txtCode.Text = "PXK" + date + "001";
                    //}
                    int billStatus = TextUtils.ToInt(cboStatusNew.EditValue);
                    if (billStatus == 0)
                    {
                        txtCode.Text = "PM" + date + "001";
                    }
                    else if (billStatus == 3)
                    {
                        txtCode.Text = "PCT" + date + "001";
                    }
                    else if (billStatus == 4)
                    {
                        txtCode.Text = "PMNB" + date + "001";
                    }
                    else if (billStatus == 5)
                    {
                        txtCode.Text = "PXM" + date + "001";
                    }
                    else
                    {
                        txtCode.Text = "PXK" + date + "001";
                    }
                    return;

                }
                else
                {
                    so = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 3)); // tách lấy 3 số cuối convert sang int
                }
                string dem = TextUtils.ToString(so + 1);
                for (int i = 0; dem.Length < 3; i++)
                {
                    dem = "0" + dem;
                }

                //if (cboStatus.SelectedIndex == 0)
                //{
                //	txtCode.Text = "PM" + date + TextUtils.ToString(dem);
                //}
                //else if (cboStatus.SelectedIndex == 3)
                //{
                //	txtCode.Text = "PCT" + date + TextUtils.ToString(dem);
                //}
                //else if (cboStatus.SelectedIndex == 4)
                //{
                //	txtCode.Text = "PMNB" + date + TextUtils.ToString(dem);
                //}
                //else
                //{
                //	txtCode.Text = "PXK" + date + TextUtils.ToString(dem);
                //}

                if (TextUtils.ToInt(cboStatusNew.EditValue) == 0)
                {
                    txtCode.Text = "PM" + date + TextUtils.ToString(dem);
                }
                else if (TextUtils.ToInt(cboStatusNew.EditValue) == 3)
                {
                    txtCode.Text = "PCT" + date + TextUtils.ToString(dem);
                }
                else if (TextUtils.ToInt(cboStatusNew.EditValue) == 4)
                {
                    txtCode.Text = "PMNB" + date + TextUtils.ToString(dem);
                }
                else if (TextUtils.ToInt(cboStatusNew.EditValue) == 5)
                {
                    txtCode.Text = "PXM" + date + TextUtils.ToString(dem);
                }
                else
                {
                    txtCode.Text = "PXK" + date + TextUtils.ToString(dem);
                }
            }
        }
        #endregion

        /// <summary>
        /// hàm dùng để chọn sản phẩm
        /// </summary>
        private void loadProduct()
        {
            if (cboGroup.Text == "") return;
            int ID = TextUtils.ToInt(cboGroup.EditValue);
            int IDType = TextUtils.ToInt(cbKhoType.EditValue);
            //dtProduct = TextUtils.LoadDataFromSP("spGetProductSale", "A", new string[] { "@IDgroup" }, new object[] { ID });
            //dtProduct = TextUtils.LoadDataFromSP($"spGetInventory", "A", new string[] { "@ID", "@Find", "@WarehouseCode" }, new object[] { IDType, " " , WarehouseCode });
            //dtProduct = TextUtils.LoadDataFromSP($"spGetInventory_Export", "A", new string[] { "@ID", "@Find", "@WarehouseCode" }, new object[] { IDType, " " , WarehouseCode });
            //TextUtils.LoadDataFromSP(StoreProcedure.spGetProductExportSale, "A", 
            //                        new string[] { "@GroupProductID", "@WarehouseCode" }, 
            //                        new object[] { ID,WarehouseCode });

            dtProduct = TextUtils.LoadDataFromSP($"spGetInventory", "A", new string[] { "@ID", "@Find", "@WarehouseCode" }, new object[] { IDType, "", WarehouseCode });
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ProductSaleID";
            cbProduct.DataSource = dtProduct;
            //colProductID.ColumnEdit = cbProduct;
        }

        // click vào khách hàng để tự động hiển thị ra địa chỉ
        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (dtCustomer.Rows.Count <= 0) return;
            if (cboCustomer.Text.Trim() == "") return;
            DataRow[] dr = dtCustomer.Select($"ID={cboCustomer.EditValue}");
            txtAddress.Text = TextUtils.ToString(dr[0]["Address"]);
            loadAddressStock();
        }
        void loadAddressStock()
        {
            DataTable dt = TextUtils.Select($"SELECT * FROM AddressStock where CustomerID = {cboCustomer.EditValue}");
            cbAdressStock.Properties.DisplayMember = "Address";
            cbAdressStock.Properties.ValueMember = "ID";
            cbAdressStock.Properties.DataSource = dt;
        }
        //khi chọn cboName -> tự động sinh ra tên,ĐVT
        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            //grvData.Focus();
            //txtCode.Focus();

            grvData.FocusedRowHandle = -1;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(coProductSaleID));
            DataRow[] rows = dtProduct.Select("ProductSaleID= " + ID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string productNewCode = TextUtils.ToString(rows[0]["ProductNewCode"]);
                string unit = TextUtils.ToString(rows[0]["Unit"]);
                grvData.SetFocusedRowCellValue(colProductName, productName);
                grvData.SetFocusedRowCellValue(colProductNewCode, productNewCode);
                grvData.SetFocusedRowCellValue(colUnit, unit);
            }
        }

        #region Buttons Events
        /// <summary>
        /// click button lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //loadBilllNumber();
            if (saveData())
            {
                //DateTime? creatDate = TextUtils.ToDate4(dtpCreatDate.EditValue);
                DateTime creatDate = dtpCreatDate.Value;
                //if (creatDate.HasValue)
                {
                    for (int i = 0; i < lstIDPOdetail.Count; i++)
                    {
                        TextUtils.ExcuteSQL($"UPDATE POKHDetail set IsExport = 1, ActualDeliveryDate = '{creatDate.ToString("yyyy/MM/dd HH:mm:ss")}' where ID ={lstIDPOdetail[i]}");
                    }
                }

                CheckPODetail(_POKHID);
                cPOStatus.AutoUpdateStatus(_POKHID);
                this.DialogResult = DialogResult.OK;
            }
        }

        void CheckPODetail(int id)
        {
            DataTable dt = TextUtils.Select($"Select * From POKHDetail where POKHID={id} and IsExport=0");
            int rowdetail = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select count(*) From POKHDetail where POKHID={id}"));
            //if(rowdetail-dt.Rows.Count >= 0)
            //    TextUtils.ExcuteSQL($"UPDATE POKH set IsShip = 1 where ID ={id}");
            if (dt.Rows.Count < rowdetail)
                TextUtils.ExcuteSQL($"UPDATE POKH set IsExport = 1 where ID ={id}");
        }

        /// <summary>
        /// click button để thêm khách hàng  mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouse.ID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }

        /// <summary>
        /// click button để thêm kho  mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewWarehouse_Click(object sender, EventArgs e)
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode).FirstOrDefault();
            warehouse = warehouse ?? new WarehouseModel();
            frmProductGroupDetail frm = new frmProductGroupDetail(warehouse.ID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadKhoType();
            }
        }

        /// <summary>
        /// click button để thêm dòng mới trong bảng dgvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;

            // khi click STT tự động tăng
            if (dt.Rows.Count == 0)
            {
                STT = 1;
            }
            else
            {
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
            }
            DataRow dtrow = dt.NewRow();
            dtrow["STT"] = STT;
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;
        }

        /// <summary>
        /// click button để xóa dòng trong dgvData
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
                return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(coProductSaleID));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (strID > 0)
                {
                    lstIDDelete.Add(strID);
                }

            }
        }

        /// <summary>
        /// click button thêm sản phẩm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }

        /// <summary>
        /// click button lưu thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu xuất mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!saveData()) return;
                statusOld = "";
                txtAddress.Clear();
                cboStatus.Text = "";
                cboUser.Text = "";
                cboSender.Text = "";
                cbKhoType.Text = "";
                cboCustomer.Text = "";
                cboStatus.SelectedIndex = 2;
                cbKhoType.EditValue = "";
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                billExport = new BillExportModel();
                //loadBilllNumber();
            }
        }
        #endregion

        /// <summary>
        /// hàm save Data
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            grvData.FocusedRowHandle = -1;
            RecheckQty();
            if (!ValidateForm()) return false;
            // focus: trỏ đến -> lưu và cất đi luôn
            grvData.Focus();
            //txtCode.Focus();
            billExport.TypeBill = false;
            billExport.Code = txtCode.Text.Trim();
            billExport.Address = txtAddress.Text.Trim();
            billExport.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            billExport.UserID = TextUtils.ToInt(cboUser.EditValue);
            billExport.SenderID = TextUtils.ToInt(cboSender.EditValue);
            //billExport.CreatDate = dtpCreatDate.Value;

            //MessageBox.Show(dtpCreatDate.EditValue.ToString(), "dtpCreatDate.EditValue");
            //MessageBox.Show(TextUtils.ToDate4(dtpCreatDate.EditValue).Value.ToString(), "TextUtils.ToDate4(dtpCreatDate.EditValue)");

            billExport.CreatDate = dtpCreatDate.Value;
            //billExport.CreatDate = TextUtils.ToDate4(dtpCreatDate.EditValue);
            //MessageBox.Show(billExport.CreatDate.ToString(), "billExport.CreatDate");

            //billExport.Status = cboStatus.SelectedIndex;
            billExport.WarehouseType = cbKhoType.Text.Trim();
            billExport.GroupID = TextUtils.ToString(cboGroup.EditValue);
            billExport.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            billExport.ProductType = TextUtils.ToInt(cbProductType.EditValue);
            billExport.AddressStockID = TextUtils.ToInt(cbAdressStock.EditValue);
            billExport.IsMerge = TextUtils.ToBoolean(ckbMerge.Checked);
            billExport.WarehouseID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM Warehouse WHERE WarehouseCode = '{WarehouseCode}'"));
            billExport.UpdatedDate = DateTime.Now;
            billExport.UpdatedBy = Global.AppUserName;

            billExport.Status = TextUtils.ToInt(cboStatusNew.EditValue);
            billExport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            if (billExport.ID > 0)
            {
                //BillExportBO.Instance.Update(billExport);
                SQLHelper<BillExportModel>.Update(billExport);
            }
            else
            {
                billExport.BillDocumentExportType = 2; // phúc add new



                billExport.CreatedDate = DateTime.Now;
                billExport.CreatedBy = Global.AppUserName;
                //billExport.ID = (int)BillExportBO.Instance.Insert(billExport);
                billExport.ID = SQLHelper<BillExportModel>.Insert(billExport).ID;

                // thêm mới chứng từ
                List<DocumentExportModel> listID = SQLHelper<DocumentExportModel>.SqlToList("SELECT ID FROM DocumentExport WHERE IsDeleted <> 1");
                for (int i = 0; i < listID.Count; i++)
                {
                    var documentExportID = listID[i];

                    BillDocumentExportModel bdeModel = new BillDocumentExportModel();

                    bdeModel.BillExportID = billExport.ID;
                    bdeModel.DocumentExportID = documentExportID.ID;

                    SQLHelper<BillDocumentExportModel>.Insert(bdeModel);
                }
            }
            for (int i = 0; i < grvData.RowCount; i++)
            {
                BillExportDetailModel billExportDetail = new BillExportDetailModel();

                long id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                //if (id > 0)
                //{
                //    billExportDetail = (BillExportDetailModel)BillExportDetailBO.Instance.FindByPK(id);
                //}
                billExportDetail = SQLHelper<BillExportDetailModel>.FindByID(id);
                string productName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                if (productName.Trim() == "") continue;
                billExportDetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                billExportDetail.BillID = billExport.ID;//Liên kết bảng Nhập Xuất
                billExportDetail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, coProductSaleID));//ID Sản phẩm
                billExportDetail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                billExportDetail.ProductFullName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductFullName));
                //billExportDetail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectID));
                billExportDetail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                billExportDetail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                billExportDetail.TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty));
                billExportDetail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                billExportDetail.ProductType = TextUtils.ToInt(cbProductType.EditValue);
                billExportDetail.ProductType = TextUtils.ToInt(cbProductType.EditValue);
                billExportDetail.POKHID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHID));
                billExportDetail.GroupExport = TextUtils.ToString(grvData.GetRowCellValue(i, colGroup));
                billExportDetail.SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));

                billExportDetail.TradePriceDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colTradePriceDetailID));

                billExportDetail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode));
                billExportDetail.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));
                billExportDetail.Specifications = TextUtils.ToString(grvData.GetRowCellValue(i, colspecifications));

                billExportDetail.BillImportDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colImportDetailID));

                if (billExportDetail.ProductID <= 0) continue;
                if (billExportDetail.ID > 0)
                {
                    BillExportDetailBO.Instance.Update(billExportDetail);

                }
                else
                {
                    billExportDetail.ID = (int)BillExportDetailBO.Instance.Insert(billExportDetail);
                    grvData.SetRowCellValue(i, colID, billExportDetail.ID);
                }

                //if (lstIDDelete.Count > 0)
                //    BillExportDetailBO.Instance.Delete(lstIDDelete);

                if (lstIDDelete.Count > 0)
                {
                    BillExportDetailBO.Instance.Delete(lstIDDelete);
                    for (int j = 0; j < lstIDDelete.Count; j++)
                    {
                        int IdBillExportDetail = TextUtils.ToInt(lstIDDelete[j]);
                        BillExportDetailSerialNumberBO.Instance.DeleteByAttribute("BillExportDetailID", IdBillExportDetail);
                    }
                }


                //check xem có trong Inventory chưa, nếu chưa có thì thêm mới   
                ArrayList arr = InventoryBO.Instance.FindByExpression(new Expression("WarehouseID", billExport.WarehouseID).And(new Expression("ProductSaleID", billExportDetail.ProductID)));
                if (arr.Count == 0)
                {
                    InventoryModel inventory = new InventoryModel();
                    inventory.WarehouseID = TextUtils.ToInt(billExport.WarehouseID);
                    inventory.ProductSaleID = TextUtils.ToInt(billExportDetail.ProductID);
                    inventory.TotalQuantityFirst = inventory.TotalQuantityLast = inventory.Import = inventory.Export = 0;

                    InventoryBO.Instance.Insert(inventory);
                }


                //Lưu sản phẩm xuất kho dự án
                string choseInventoryProject = TextUtils.ToString(grvData.GetRowCellValue(i, colChosenInventoryProject));
                string[] choseInventoryProjects = choseInventoryProject.Split(';');
                var exp1 = new Expression(InventoryProjectExportModel_Enum.BillExportDetailID, billExportDetail.ID);
                foreach (string item in choseInventoryProjects)
                {
                    int inventoryProjectID = TextUtils.ToInt(item);
                    var exp2 = new Expression(InventoryProjectExportModel_Enum.InventoryProjectID, inventoryProjectID);
                    var myDict = new Dictionary<string, object>()
                    {
                        { InventoryProjectExportModel_Enum.IsDeleted.ToString(),true},
                        { InventoryProjectExportModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                        { InventoryProjectExportModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    };
                    SQLHelper<InventoryProjectExportModel>.UpdateFields(myDict, exp1.And(exp2));

                    InventoryProjectExportModel projectExport = new InventoryProjectExportModel();
                    projectExport.BillExportDetailID = billExportDetail.ID;
                    projectExport.InventoryProjectID = TextUtils.ToInt(item);

                    SQLHelper<InventoryProjectExportModel>.Insert(projectExport);
                }

            }
            return true;
        }

        /// <summary>
        /// hàm kiểm tra thông tin nhập trước khi save
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            DataTable dt;
            if (billExport.ID > 0)
            {
                //string Billcode = txtCode.Text.Trim();
                //if (Billcode.Contains("PM"))
                //{
                //    Billcode = Billcode.Substring(2);
                //}
                //else if (Billcode.Contains("PXK") || Billcode.Contains("PCT"))
                //{
                //    Billcode = Billcode.Substring(3);
                //}
                //int strID = billExport.ID;
                ////dt = TextUtils.Select($"select top 1 ID from BillExport where Code LIKE '%{Billcode}%' and ID <> {strID}");
                //dt = TextUtils.Select($"select top 1 ID from BillExport where Code = '{txtCode.Text.Trim()}' and ID <> {strID}");
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("Số phiếu này đã tồn tại.\nVui lòng Load lại Số phiếu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
            }
            else
            {
                dt = TextUtils.Select($"select top 1 ID from BillExport where Code = '{txtCode.Text.Trim()}'");
                if (dt.Rows.Count > 0)
                {
                    loadBilllNumber();
                    MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtCode.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            int billStatus = TextUtils.ToInt(cboStatusNew.EditValue);
            DateTime? creatDateVN = TextUtils.ToDate4(dtpCreatDate.Value);
            DateTime? creatDateUS = TextUtils.ToDate2(dtpCreatDate.Value);

            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (TextUtils.ToInt(cboCustomer.EditValue) <= 0 && TextUtils.ToInt(cboSupplier.EditValue) <= 0)
            {
                MessageBox.Show("Xin hãy chọn Khách hàng hoặc Nhà cung câp!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboUser.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn nhân viên.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cbKhoType.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn kho quản lý.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboGroup.Text == "")
            {
                MessageBox.Show("Xin hãy chọn nhóm.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cboSender.Text == "")
            {
                MessageBox.Show("Xin hãy chọn người giao.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (TextUtils.ToInt(cboStatusNew.EditValue) < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (billStatus != 6 && (!creatDateVN.HasValue && !creatDateUS.HasValue))
            {
                MessageBox.Show("Xin hãy chọn Ngày xuất!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    string stt = TextUtils.ToString(grvData.GetRowCellValue(i, colSTT));
            //    decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
            //    decimal qtyRemain = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantityRemain));
            //    int pokhDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));
            //    if (pokhDetailID <= 0) continue;
            //    if (qty > qtyRemain)
            //    {
            //        MessageBox.Show($"Số lượng xuất của mã hàng STT [{stt}] không được nhiều hơn Số lượng còn lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}
            return true;
        }

        /// <summary>
        /// trạng thái được thay đổi khi chọn trong cboTrangThai
        /// </summary>

        string statusOld = "";
        int check = 0;
        private void cboStatus_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// giá trị cbName được chỉnh sửa khi cboGroup thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        /// <summary>
        /// giá trị cột TotalQty thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //          if (e.Column == colQty || e.Column == colProductID)
        //          {
        //              int ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colProductID));
        //              float sum = 0;
        //              for (int i = 0; i < grvData.RowCount; i++)
        //              {
        //                  // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
        //                  int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
        //                  if (ID == IDSearch)
        //                  {
        //                      float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
        //                      sum += qty;
        //                  }
        //              }

        //              // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
        //              for (int j = 0; j < grvData.RowCount; j++)
        //              {
        //                  int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
        //                  if (ID == IDSearch)
        //                  {
        //                      grvData.SetRowCellValue(j, colTotalQty, sum);
        //                  }
        //              }
        //          }
        //          if (e.Column == colProjectID)
        //          {
        //              int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
        //              if (projectID == 0) return;
        //              for (int i = 0; i < grvData.RowCount; i++)
        //              {
        //                  int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
        //                  if (item == 0)
        //                  {
        //                      grvData.SetRowCellValue(i, colProjectID, projectID);
        //                  }
        //              }
        //          }

        //      }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQty || e.Column == coProductSaleID)
            {
                BillExportDetailModel detail = new BillExportDetailModel();
                int idBillExport = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                detail = (BillExportDetailModel)(BillExportDetailBO.Instance.FindByPK(idBillExport));
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, coProductSaleID));
                float sum = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, coProductSaleID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
                        sum += qty;
                    }
                    if (idBillExport > 0)
                    {
                        detail.Qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
                        BillExportDetailBO.Instance.Update(detail);
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, coProductSaleID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQty, sum);
                    }
                    if (idBillExport > 0)
                    {
                        detail.Qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
                        BillExportDetailBO.Instance.Update(detail);
                    }
                }
            }
            if (e.Column == colProjectID)
            {
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                if (projectID == 0) return;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                    if (item == 0)
                    {
                        grvData.SetRowCellValue(i, colProjectID, projectID);
                    }
                }
            }
        }

        void RecheckQty()
        {
            for (int k = 0; k < grvData.RowCount; k++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(k, coProductSaleID));
                float sum = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, coProductSaleID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
                        sum += qty;
                    }
                }
                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, coProductSaleID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQty, sum);
                    }
                }
            }
        }
        private void frmGoodsDeliveryNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        string status;
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBilllNumber();
            //if ((txtCode.Text.Contains("PXK") || txtCode.Text.Contains("PCT")) && cboStatus.SelectedIndex == 0)
            //{
            //    if (txtCode.Text.Contains("PCT"))
            //        txtCode.Text = txtCode.Text.Replace("PCT", "PM");
            //    else
            //        txtCode.Text = txtCode.Text.Replace("PXK", "PM");
            //}
            //else if ((txtCode.Text.Contains("PM") || txtCode.Text.Contains("PXK")) && cboStatus.SelectedIndex == 3)
            //{
            //    if (txtCode.Text.Contains("PM"))
            //        txtCode.Text = txtCode.Text.Replace("PM", "PCT");
            //    else
            //        txtCode.Text = txtCode.Text.Replace("PXK", "PCT");
            //}
            //else
            //{
            //    if (txtCode.Text.Contains("PM"))
            //        txtCode.Text = txtCode.Text.Replace("PM", "PXK");
            //    else
            //        txtCode.Text = txtCode.Text.Replace("PCT", "PXK");
            //}
        }
        private void btnProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            //if (billExport.ID == 0)
            {
                loadBilllNumber();
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtDateTime_ValueChanged(object sender, EventArgs e)
        {
            //if (billExport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void grvData_LoadData(List<int> lstID, string group)
        {

            cboGroup.EditValue = group;
            for (int i = 0; i < lstID.Count; i++)
            {
                grvData.FocusedRowHandle = -1;
                btnNew_Click(null, null);
                DataRow[] rows = dtProduct.Select($"ID = { lstID[i]}");
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, coProductSaleID, lstID[i]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, productName);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, unit);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, coProductSaleID, lstID[i]);
                        grvData.SetRowCellValue(i, colProductName, productName);
                        grvData.SetRowCellValue(i, colUnit, unit);
                    }
                }
            }

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode.Trim()).FirstOrDefault();
            if (warehouse == null) return;
            frmPOKHData frm = new frmPOKHData(warehouse.ID);
            frm._listBillExportDetail = new ListBillExportDetail(Lst);
            frm.Tag = WarehouseCode;
            frm.ShowDialog();
        }

        List<int> lstIDPOdetail = new List<int>();
        int _POKHID;
        private void Lst(string group, DataTable dt)
        {
            cboGroup.EditValue = group;
            lstIDPOdetail.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _POKHID = TextUtils.ToInt(dt.Rows[0]["POKHID"]);
                lstIDPOdetail.Add(TextUtils.ToInt(dt.Rows[i]["ID"]));
                grvData.FocusedRowHandle = -1;
                if (dt.Rows.Count > 0)
                {
                    btnNew_Click(null, null);
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, coProductSaleID, dt.Rows[i]["ProductID"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, dt.Rows[i]["ProductName"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductNewCode, dt.Rows[i]["ProductNewCode"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, dt.Rows[i]["Unit"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colPOKHID, dt.Rows[i]["ID"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductFullName, dt.Rows[i]["GuestCode"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colGroup, dt.Rows[i]["GroupPO"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colQty, dt.Rows[i]["Qty"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProjectID, dt.Rows[i]["ProjectID"]);
                        //grvData.SetRowCellValue(grvData.RowCount - 1, colProjectCode, dt.Rows[i]["ProjectCodeText"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProjectCode, dt.Rows[i]["ProjectCode"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colNote, dt.Rows[i]["PONumber"]);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, coProductSaleID, dt.Rows[i]["ProductID"]);
                        grvData.SetRowCellValue(i, colProductName, dt.Rows[i]["ProductName"]);
                        grvData.SetRowCellValue(i, colProductNewCode, dt.Rows[i]["ProductNewCode"]);
                        grvData.SetRowCellValue(i, colUnit, dt.Rows[i]["Unit"]);
                        grvData.SetRowCellValue(i, colPOKHID, dt.Rows[i]["ID"]);
                        grvData.SetRowCellValue(i, colProductFullName, dt.Rows[i]["GuestCode"]);
                        grvData.SetRowCellValue(i, colGroup, dt.Rows[i]["GroupPO"]);
                        grvData.SetRowCellValue(i, colQty, dt.Rows[i]["Qty"]);
                        grvData.SetRowCellValue(i, colProjectID, dt.Rows[i]["ProjectID"]);
                        //grvData.SetRowCellValue(i, colProjectCode, dt.Rows[i]["ProjectCodeText"]);
                        grvData.SetRowCellValue(i, colProjectCode, dt.Rows[i]["ProjectCode"]);
                        grvData.SetRowCellValue(i, colNote, dt.Rows[i]["PONumber"]);
                    }
                }
            }

        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            //GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            //if (info.Column == colSTT && e.Y < 41)
            //{
            //    MyLib.AddNewRow(grdData, grvData);
            //}

            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;
                    dtDetail.AcceptChanges();
                    DataRow dtrow = dtDetail.NewRow();

                    int stt = 0;
                    if (dtDetail.Rows.Count > 0)
                    {
                        stt = dtDetail.AsEnumerable().Max(x => x.Field<int>("STT"));
                    }

                    //int projectID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectID));
                    //string projectName = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectName));

                    dtrow["STT"] = stt + 1;
                    //dtrow["ProjectID"] = projectID;
                    //dtrow["ProjectName"] = projectName;
                    dtDetail.Rows.Add(dtrow);
                }
            }
        }
        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column == colQty)
            {
                decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == 0);
                e.Handled = true;
                return;
            }
            else
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == "");
                e.Handled = true;
                return;
            }

        }
        private void ckbMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMerge.Checked)
            {
                grvData.OptionsView.AllowCellMerge = true;
                colGroup.GroupIndex = 0;
                grvData.ExpandAllGroups();
                grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            }
            else
            {
                colGroup.GroupIndex = -1;
                grvData.OptionsView.AllowCellMerge = false;
                grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            }

        }
        /// <summary>
        /// chọn dự án tự động hiển thị ra mã dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            DataRow[] rows = dtProject.Select("ID= " + ID);
            if (rows.Length > 0)
            {
                projectCode = TextUtils.ToString(rows[0]["ProjectCode"]);
                grvData.SetFocusedRowCellValue(colProjectCode, projectCode);
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    string item = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode));
                    if (item == "" || item == null)
                    {
                        grvData.SetRowCellValue(i, colProjectCode, projectCode);
                    }
                }
            }
        }

        private void cbKhoType_EditValueChanged(object sender, EventArgs e)
        {
            int value = TextUtils.ToInt(cbKhoType.EditValue);
            colQuantityRemain.VisibleIndex = 9;
            if (value == 70) colQuantityRemain.VisibleIndex = -1;

            cboGroup.EditValue = cbKhoType.EditValue;
            loadProduct();

            if (billExport.ID > 0) return; //HieuDV Update 12/01/2023: Chỉ tự động gán người nhận và người gửi khi tạo phiếu, Khi update thì lấy từ DB
            DataTable dtGroupWarehouse = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
                                                    new string[] { "@WarehouseID", "@ProductGroupID" }, new object[] { warehouse.ID, TextUtils.ToInt(cbKhoType.EditValue) });

            if (dtGroupWarehouse.Rows.Count > 0)
            {
                //model.SenderID = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
                cboSender.EditValue = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
            }
            else
            {
                cboSender.EditValue = 88;
                if (!WarehouseCode.Contains("HCM")) cboSender.EditValue = Global.UserID;

            }

            if (WarehouseCode.Contains("HN") && TextUtils.ToInt(cbKhoType.EditValue) == 15)
            {
                cboUser.EditValue = 23;//Nguyễn Thị Hiền
                                       //if (TextUtils.ToInt(cbKhoType.EditValue) == 15)
                                       //{
                                       //    cboUser.EditValue = 23;//Nguyễn Thị Hiền
                                       //    //cboSender.EditValue = 1146;//Lê Minh Thuỳ
                                       //}

                //if (TextUtils.ToInt(cbKhoType.EditValue) == 13)
                //{
                //    cboSender.EditValue = 1146;
                //}
                //else if (TextUtils.ToInt(cbKhoType.EditValue) == 14)
                //{
                //    cboSender.EditValue = 1146;
                //}
            }
            //else if (WarehouseCode.Contains("HCM"))
            //{
            //    cboSender.EditValue = 88;//Phan Thị Thu Thuỷ
            //}
            //else if (WarehouseCode.Contains("BN"))
            //{
            //    cboSender.EditValue = 1234;//Cáp Thị Hiên
            //}
            else return;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveNow_Click(object sender, EventArgs e)
        {
            if (billExport.IsApproved == true)
            {
                MessageBox.Show("Không thể cập nhật vì phiếu [" + txtCode.Text + "] đã được duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                saveData();
            }

        }

        private void btnShowHistoryPrice_Click(object sender, EventArgs e)
        {

        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //Add SerialNumber
            //if (e.Column == colAddSerialNumber)
            //{
            //    if (grdData.DataSource == null) return;
            //    int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //    if (strID == 0) return;
            //    int strQty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            //    int productID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            //    //BillExportDetailModel model = new BillExportDetailModel() { ID = strID, Qty = strQty };
            //    BillExportDetailModel model = new BillExportDetailModel() { ID = strID, Qty = strQty, ProductID = productID };
            //    frmBillSerialNumber frm = new frmBillSerialNumber();
            //    frm.billExportDetailModel = model;
            //    frm.Type = 2;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        loadBillExportDetail();
            //    }
            //}
        }

        private void cboStatusNew_EditValueChanged(object sender, EventArgs e)
        {
            //if (TextUtils.ToInt(cboStatusNew.EditValue) != 6)
            //{
            //    dtpCreatDate.Value = DateTime.Now;
            //}
            //else
            //{
            //    dtpCreatDate.Value = null;
            //}
            loadBilllNumber();
        }


        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            SupplierSaleModel supplierSale = (SupplierSaleModel)cboSupplier.GetSelectedDataRow();
            if (supplierSale != null)
            {
                txtAddress.Text = supplierSale.AddressNCC;
            }
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

            //grvData.CloseEditor();
            //if (grvData.FocusedColumn != colQty) return;
            //decimal qty = TextUtils.ToDecimal(e.Value);
            //decimal qtyRemain = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQuantityRemain));
            //int pokhDetailID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPOKHDetailID));
            //if (pokhDetailID <= 0) return;
            //if (qty > qtyRemain)
            //{
            //    grvData.BeginUpdate();
            //    e.Valid = false;
            //    e.ErrorText = "Số lượng xuất không được nhiều hơn Số lượng còn lại!";
            //    grvData.EndUpdate();
            //}
        }

        private void grvData_ShownEditor(object sender, EventArgs e)
        {
            //if (grvData.ActiveEditor != null)
            //{
            //    grvData.ActiveEditor.IsModified = true;
            //}
        }

        private void btnDownloadFilePO_Click(object sender, EventArgs e)
        {
            int pokhID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPOKHID));
            if (pokhID <= 0) return;
            POKHModel pokh = SQLHelper<POKHModel>.FindByID(pokhID);

            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(userFolder, "Downloads", "POKH", pokh.PONumber);

            if (!Directory.Exists(pathDownload))
            {
                Directory.CreateDirectory(pathDownload);
            }

            string url = $"{Global.Host}/api/pokh{WarehouseCode.ToLower()}/{pokh.PONumber}";
            List<POKHFileModel> filePOs = SQLHelper<POKHFileModel>.FindByAttribute("POKHID", pokhID);

            foreach (POKHFileModel file in filePOs)
            {
                try
                {
                    url += $@"/{file.FileName}";
                    string folderDownload = Path.Combine(pathDownload, file.FileName);

                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, folderDownload);
                    Process.Start(folderDownload);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            Process.Start(pathDownload);

        }

        private void btnAddSeialNumber_Click(object sender, EventArgs e)
        {
            //if (grdData.DataSource == null) return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (strID == 0) return;
            int strQty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            int productID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(coProductSaleID));
            //BillExportDetailModel model = new BillExportDetailModel() { ID = strID, Qty = strQty };
            BillExportDetailModel model = new BillExportDetailModel() { ID = strID, Qty = strQty, ProductID = productID };
            frmBillSerialNumber frm = new frmBillSerialNumber();
            frm.billExportDetailModel = model;
            frm.Type = 2;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExportDetail();
            }
        }

        private void btnInventoryProjectExport_Click(object sender, EventArgs e)
        {
            int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            int productSaleID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(coProductSaleID));
            if (projectID <= 0 || productSaleID <= 0) return;

            frmInventoryProject frm = new frmInventoryProject();
            //frm.isYCMH = true;
            //frm.supplierSaleId = TextUtils.ToInt(cboSupplierSale.EditValue);

            frm.btnDelete.Visibility = frm.btnExportExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            frm.btnChosen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            frm.cboProject.EditValue = projectID;
            frm.cboProject.Enabled = false;
            frm.productSaleID = productSaleID;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.inventoryProjects.Count <= 0) return;
                string idText = string.Join(";", frm.inventoryProjects.Select(x=>x.ID));
                string code = string.Join(";", frm.inventoryProjects.Select(x=>x.CreatedBy));
                //string lstCodes = String.Join("; ", frm.lstYCMHCode);
                grvData.SetFocusedRowCellValue(colChosenInventoryProject, idText);
                grvData.SetFocusedRowCellValue(colProductCodeExport, code);
            }
        }
    }
}
