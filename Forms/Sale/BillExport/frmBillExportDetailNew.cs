using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
//using ExcelDataReader;
using Forms.Classes;
//using MSScriptControl;
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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

using DevExpress.Utils;
using DevExpress.XtraTreeList.Columns;

namespace BMS
{
    public partial class frmBillExportDetailNew : _Forms
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

        public DataTable lstTonCk = new DataTable(); // PQ.Chien ADD 12/03/2025==========================================
        public bool isBorrow = false;

        //public BillImportModel billImport = new BillImportModel();
        #endregion

        bool _isTranfer = false;

        public frmBillExportDetailNew(bool isTranfer)
        {
            InitializeComponent();
            _isTranfer = isTranfer;
        }

        private void frmBillExportDetail_Load(object sender, EventArgs e)
        {
            warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode.Trim()).FirstOrDefault();
            this.Text += " - " + WarehouseCode;

            LoadSupplierSale();
            loadProductGroup();
            loadCustomer();
            loadSender();
            loadUsers();
            loadKhoType();
            loadProject();
            loadProductType();
            LoadStatus();
            loadKhoTransfer();

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

            //this.cbProduct.EditValueChanged += new System.EventHandler(cboProduct_EditValueChanged);
            //this.cbProduct.EditValueChanged -= new System.EventHandler(cboProduct_EditValueChanged);


            // KHI ĐƯỢC duyệt thì sẽ ẩn các button 
            if (!(Global.IsAdmin && Global.EmployeeID <= 0))
            {
                btnSaveNew.Enabled = btnCloseSave.Enabled = btnNewProduct.Enabled = !TextUtils.ToBoolean(billExport.IsApproved);
            }
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
                //cbProduct.EditValueChanged -= new EventHandler(cboProduct_EditValueChanged);
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
                treeList1.DataSource = dtClone;
                cbProject.EditValueChanged += new EventHandler(cbProject_EditValueChanged);
                //cbProduct.EditValueChanged += new EventHandler(cboProduct_EditValueChanged);
                dtDetail = (DataTable)treeList1.DataSource;
            }

            //==============================================PQ.Chien - 12/03/2025=======================================================================
            if (isBorrow)
            {
                cboStatusNew.EditValue = 7;
            }

            if (billExport.ID <= 0)
            {

                foreach (var item in treeList1.GetNodeList())
                {
                    loadInventoryProject(coltreeListColumn2, item);
                }
            }


            LinkBillImportTrasfer();
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
                new {ID = 6,Name = "Yêu cầu xuất kho / Chuyển kho"},
                new {ID = 7,Name = "Yêu cầu mượn"}, //PQ.Chien - 12/03/2025
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
            treeList1.DataSource = null;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {
                //if (WarehouseCode == "HCM") cboSender.EditValue = Global.UserID;
                if (WarehouseCode == "HCM") cboSender.EditValue = 88;

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

                //TN.Binh update 21/07/2025
                cboWarehouseTransfer.EditValue = billExport.WareHouseTranferID;
                chkTransfer.Checked = TextUtils.ToBoolean(billExport.IsTransfer);

                if (billExport.ID <= 0) chkTransfer.Checked = _isTranfer;
                //

                if (KhoTypeID > 0) //PhucLH update 20/05/2024
                {
                    cbKhoType.EditValue = KhoTypeID;
                    cboCustomer.EditValue = customerID;
                    cboStatusNew.EditValue = _isTranfer ? 2 : 6;
                    cbProductType.EditValue = 1;

                    dtDetail.DefaultView.Sort = "STT ASC";
                    dtDetail = dtDetail.DefaultView.ToTable();
                    treeList1.DataSource = dtDetail;
                }
                else if (isPOKH) //lh Phuc 06/06/2024
                {
                    dtDetail.DefaultView.Sort = "STT ASC";
                    dtDetail = dtDetail.DefaultView.ToTable();
                    dtDetail.AcceptChanges();
                    treeList1.DataSource = dtDetail;
                    treeList1.CloseEditor();
                    //grvData.FocusedRowHandle = -1;
                    dtpRequestDate.EditValue = billExport.RequestDate;

                    treeList1.DataSource = dtDetail;

                }
                //==============================================PQ.Chien - 12/03/2025=======================================================================
                else if (isBorrow)
                {
                    dtDetail.DefaultView.Sort = "STT ASC";
                    dtDetail = dtDetail.DefaultView.ToTable();
                    cboStatusNew.EditValue = 7;
                    dtpRequestDate.EditValue = DateTime.Now;
                    cbKhoType.EditValue = billExport.KhoTypeID;
                    dtDetail.AcceptChanges();
                    treeList1.DataSource = dtDetail;
                    treeList1.CloseEditor();
                    //grvData.FocusedRowHandle = -1;
                }
                else
                {
                    // load data detail
                    //DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { billExport.ID });
                    dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { billExport.ID });
                    treeList1.DataSource = dtDetail;

                    {
                        dtpCreatDate.Value = !billExport.CreatDate.HasValue ? DateTime.Now : billExport.CreatDate.Value;
                        //dtpCreatDate.EditValue = !billExport.CreatDate.HasValue ? DateTime.Now : billExport.CreatDate.Value;
                    }

                    //dtpCreatDate.EditValue = billExport.CreatDate;
                    dtpRequestDate.EditValue = billExport.RequestDate;
                }
                treeList1.ExpandAll();
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
            treeList1.DataSource = null;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {
                cbKhoType.EditValue = billImport.KhoTypeID;
                cboGroup.SetEditValue(billImport.GroupID);

                //dtpCreatDate.EditValue = DateTime.Now;
                string ID = string.Join(",", lstBillImportID);
                DataTable dtconvert = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });
                //dtconvert.Columns.Add("ProductFullName");
                //dtconvert.Columns.Remove("ID");

                foreach (DataRow row in dtconvert.Rows)
                {
                    row["ID"] = 0;
                }

                treeList1.DataSource = dtconvert;
            }
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
            //DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");

            var productGroups = SQLHelper<ProductGroupModel>.FindAll();
            //if (warehouse.ID == 6) productGroups = productGroups.Where(x => x.ID == 13).ToList(); //Chỉ lấy kho dự án

            cbKhoType.Properties.DisplayMember = "ProductGroupName";
            cbKhoType.Properties.ValueMember = "ID";
            cbKhoType.Properties.DataSource = productGroups;
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
            "PXK", //6
            "PM" //7
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
            //return;
            //grvData.Focus();
            //txtCode.Focus();
            treeList1.CloseEditor();
            var node = treeList1.FocusedNode;

            //treeList1.foc = -1;
            //int ID = TextUtils.ToInt(node.GetValue("ProductSaleID"));
            int ID = TextUtils.ToInt(node.GetValue("ProductID"));
            DataRow[] rows = dtProduct.Select("ProductSaleID=" + ID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string productNewCode = TextUtils.ToString(rows[0]["ProductNewCode"]);
                string unit = TextUtils.ToString(rows[0]["Unit"]);
                node.SetValue("ProductName", productName);
                node.SetValue("ProductNewCode", productNewCode);
                node.SetValue("Unit", unit);
                node.SetValue("TotalInventory", TextUtils.ToString(rows[0]["TotalQuantityLast"]));
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
            int STT = 1;
            DataTable dt = (DataTable)treeList1.DataSource;

            // khi click STT tự động tăng
            if (dt.Rows.Count == 0)
            {
                STT = 1;
            }
            else
            {
                //STT = TextUtils.ToInt(treeList1.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
            }
            DataRow dtrow = dt.NewRow();
            dtrow["STT"] = STT;
            dt.Rows.Add(dtrow);
            treeList1.DataSource = dt;
        }

        /// <summary>
        /// click button để xóa dòng trong dgvData
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeList1.DataSource == null)
                return;
            int strID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(treeList1.GetFocusedRowCellDisplayText("ProductID"));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                treeList1.DeleteSelectedNodes();
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

                chkTransfer.Checked = false;
                cboWarehouseTransfer.EditValue = 0;

                //for (int i = grvData.RowCount - 1; i >= 0; i--)
                //{
                //    grvData.DeleteRow(i);
                //}

                treeList1.DeleteNodes(treeList1.GetNodeList());
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
            //grvData.FocusedRowHandle = -1;
            try
            {
                treeList1.CloseEditor();
                RecheckQty();
                if (!ValidateForm()) return false;
                // focus: trỏ đến -> lưu và cất đi luôn
                treeList1.Focus();
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
                //billExport.IsMerge = TextUtils.ToBoolean(ckbMerge.Checked);
                billExport.WarehouseID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM Warehouse WHERE WarehouseCode = '{WarehouseCode}'"));
                billExport.UpdatedDate = DateTime.Now;
                billExport.UpdatedBy = Global.AppUserName;

                billExport.Status = TextUtils.ToInt(cboStatusNew.EditValue);
                billExport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
                billExport.RequestDate = TextUtils.ToDate4(dtpRequestDate.EditValue);

                billExport.IsTransfer = chkTransfer.Checked;
                billExport.WareHouseTranferID = chkTransfer.Checked ? TextUtils.ToInt(cboWarehouseTransfer.EditValue) : 0;
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
                //for (int i = 0; i < treeList1.RowCount; i++)


                foreach (var node in treeList1.GetNodeList())
                {
                    BillExportDetailModel billExportDetail = new BillExportDetailModel();

                    int id = TextUtils.ToInt(treeList1.GetRowCellValue(node, "ID"));

                    //if (id > 0)
                    //{
                    //    billExportDetail = (BillExportDetailModel)BillExportDetailBO.Instance.FindByPK(id);
                    //}
                    billExportDetail = SQLHelper<BillExportDetailModel>.FindByID(id);
                    string productName = TextUtils.ToString(treeList1.GetRowCellValue(node, "ProductName"));
                    if (productName.Trim() == "") continue;
                    billExportDetail.ID = id;
                    billExportDetail.BillID = billExport.ID;//Liên kết bảng Nhập Xuất
                    billExportDetail.ProductID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "ProductID"));//ID Sản phẩm
                    billExportDetail.Qty = TextUtils.ToDecimal(treeList1.GetRowCellValue(node, "Qty"));
                    billExportDetail.ProductFullName = TextUtils.ToString(treeList1.GetRowCellValue(node, "ProductFullName"));
                    //billExportDetail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectID));
                    billExportDetail.Note = TextUtils.ToString(treeList1.GetRowCellValue(node, "Note"));
                    billExportDetail.STT = TextUtils.ToInt(treeList1.GetRowCellValue(node, "STT"));
                    billExportDetail.TotalQty = TextUtils.ToDecimal(treeList1.GetRowCellValue(node, "TotalQty"));
                    billExportDetail.ProjectID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "ProjectID"));
                    billExportDetail.ProductType = TextUtils.ToInt(cbProductType.EditValue);
                    //billExportDetail.ProductType = TextUtils.ToInt(cbProductType.EditValue);
                    billExportDetail.POKHID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "POKHID"));
                    billExportDetail.GroupExport = TextUtils.ToString(treeList1.GetRowCellValue(node, "Group"));
                    billExportDetail.SerialNumber = TextUtils.ToString(treeList1.GetRowCellValue(node, "SerialNumber"));

                    billExportDetail.TradePriceDetailID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "TradePriceDetailID"));

                    billExportDetail.ProjectName = TextUtils.ToString(treeList1.GetRowCellValue(node, "ProjectCode"));
                    billExportDetail.POKHDetailID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "POKHDetailID"));
                    billExportDetail.Specifications = TextUtils.ToString(treeList1.GetRowCellValue(node, "specifications"));

                    billExportDetail.BillImportDetailID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "ImportDetailID"));
                    billExportDetail.TotalInventory = TextUtils.ToInt(treeList1.GetRowCellValue(node, "TotalInventory"));
                    billExportDetail.ExpectReturnDate = TextUtils.ToDate4(treeList1.GetRowCellDisplayText(node, "ExpectReturnDate"));
                    billExportDetail.ProjectPartListID = TextUtils.ToInt(treeList1.GetRowCellDisplayText(node, "ProjectPartListID"));

                    if (billExportDetail.ProductID <= 0) continue;
                    if (billExportDetail.ID > 0)
                    {
                        //BillExportDetailBO.Instance.Update(billExportDetail);
                        SQLHelper<BillExportDetailModel>.Update(billExportDetail);
                    }
                    else
                    {
                        //billExportDetail.ID = (int)BillExportDetailBO.Instance.Insert(billExportDetail);
                        billExportDetail.ID = SQLHelper<BillExportDetailModel>.Insert(billExportDetail).ID;
                        treeList1.SetRowCellValue(node, "ID", billExportDetail.ID);
                    }

                    //if (lstIDDelete.Count > 0)
                    //    BillExportDetailBO.Instance.Delete(lstIDDelete);

                    if (lstIDDelete.Count > 0)
                    {
                        //BillExportDetailBO.Instance.Delete(lstIDDelete);
                        var myDict = new Dictionary<string, object>()
                        {
                            {"IsDeleted",true },
                            {"UpdatedBy",Global.AppUserName },
                            {"UpdatedDate",DateTime.Now },
                        };

                        var exp = new Expression("ID", string.Join(",", lstIDDelete.ToArray()), "IN");

                        SQLHelper<BillExportDetailModel>.UpdateFields(myDict, exp);

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

                        //InventoryBO.Instance.Insert(inventory);
                        SQLHelper<InventoryModel>.Insert(inventory);
                    }


                    if (billExport.Status == 2 || billExport.Status == 6)
                    {
                        loadInventoryProject(coltreeListColumn1, node);
                    }

                    //Lưu sản phẩm xuất kho dự án
                    string choseInventoryProject = TextUtils.ToString(treeList1.GetRowCellValue(node, "ChosenInventoryProject"));
                    string[] choseInventoryProjects = choseInventoryProject.Split(';');
                    var exp1 = new Expression(InventoryProjectExportModel_Enum.BillExportDetailID, billExportDetail.ID);
                    foreach (string item in choseInventoryProjects)
                    {
                        //int inventoryProjectID = TextUtils.ToInt(item);
                        if (string.IsNullOrWhiteSpace(item)) continue;
                        var parts = item.Split('-');
                        if (parts.Length < 1) continue;
                        decimal quantity = 0;
                        int inventoryProjectID = 0;
                        int.TryParse(parts[0], out inventoryProjectID);
                        decimal.TryParse(parts[1], out quantity);

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
                        projectExport.InventoryProjectID = inventoryProjectID;
                        projectExport.Quantity = quantity;

                        SQLHelper<InventoryProjectExportModel>.Insert(projectExport);
                    }

                }


                //SaveBillImport();

                if (billExport.IsTransfer == true && billExport.Status == 2)
                {
                    /*  billExport.BillDocumentExportType = 2;
                      billExport.CreatedDate = DateTime.Now;
                      billExport.CreatedBy = Global.AppUserName;
                      //billExport.ID = (int)BillExportBO.Instance.Insert(billExport);
                      billExport.ID = SQLHelper<BillExportModel>.Insert(billExport).ID;*/
                    // Tạo mới phiếu nhập
                    billImport = CreateNewBillImport(); // viết hàm riêng để set dữ liệu
                    billImport.BillExportID = billExport.ID;
                    billImport.ID = SQLHelper<BillImportModel>.Insert(billImport).ID;
                    /*  billExport.BillImportID = billImport.ID;*/

                    // Gọi SP tạo chứng từ nhập
                    //TextUtils.ExcuteScalar("spCreateDocumentImport",
                    //    new string[] { "@BillImportID", "@CreatedBy" },
                    //    new object[] { billImport.ID, Global.LoginName });

                    //// Tải thông tin liên quan (nếu cần)
                    //DataTable dt = TextUtils.LoadDataFromSP(
                    //    "spGetProjectPartlistPurchaseRequestByBillImportID", "A",
                    //    new string[] { "@BillImportID" },
                    //    new object[] { billImport.ID });
                }
                else
                {
                    billExport.BillImportID = null; // Tránh gán sai ID làm lỗi khóa ngoại
                }
                if (billExport.IsTransfer == true && billImport.BillExportID != null) //TN.Binh update 10/08/25
                {
                    SaveBillImportDetails(billImport.ID, billExport);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
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

            if (chkTransfer.Checked == true && TextUtils.ToInt(cboWarehouseTransfer.EditValue) <= 0)
            {
                MessageBox.Show("Xin hãy chọn kho nhận!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            //============================================= PQ.Chien ADD 12/03/2025==========================================

            int status = TextUtils.ToInt(cboStatusNew.EditValue);


            foreach (var node in treeList1.GetNodeList())
            {
                string stt = TextUtils.ToString(node.GetValue("STT"));

                decimal qty = TextUtils.ToDecimal(node.GetValue(colQty.FieldName));
                if (qty <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập SL xuất dòng [{stt}]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;

                }

                if (status != 7 && status != 0) continue;
                //DateTime? expectReturnDate = TextUtils.ToDate4(node.GetValue("ExpectReturnDate"));
                string expectReturnDate = TextUtils.ToString(node.GetValue("ExpectReturnDate"));
                //int id = TextUtils.ToInt(node.GetValue("ID"));
                int projectID = TextUtils.ToInt(treeList1.GetRowCellValue(node, "ProjectID"));

                //if (!expectReturnDate.HasValue)
                if (string.IsNullOrWhiteSpace(expectReturnDate))
                {
                    MessageBox.Show($"Vui lòng nhập Ngày dự kiến trả dòng [{stt}]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (projectID <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Dự án dòng [{stt}]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (status == 2 || status == 6) return ValidateKeep(treeList1.GetNodeList());
            return true;
        }


        string[] unitNames = new string[] { "m", "mét" };

        /// <summary>
        /// Check validate hàng giữ
        /// </summary>
        bool ValidateKeep(List<TreeListNode> nodes)
        {
            if (nodes.Count <= 0) return false;
            //var totalQtyPOKHs = nodes
            //    .GroupBy(node => new
            //    {
            //        ProductID = TextUtils.ToInt(node.GetValue("ProductID")),
            //        //ProjectID = TextUtils.ToInt(node.GetValue("ProjectID")),
            //        POKHDetailIDActual = TextUtils.ToInt(node.GetValue("POKHDetailIDActual")),
            //        //ChosenInventoryProject = TextUtils.ToInt(node.GetValue("ChosenInventoryProject")),
            //        ID = TextUtils.ToInt(node.GetValue("ID")),
            //    })
            //    .Select(g => new
            //    {
            //        BillExportDetailID = string.Join(",", g.Select(node => TextUtils.ToInt(node.GetValue("ID")))),
            //        g.Key.ProductID,
            //        ProjectID = 0,
            //        g.Key.POKHDetailIDActual,
            //        g.Key.ID,
            //        ProductNewCode = g.First().GetValue("ProductNewCode")?.ToString(),
            //        ProjectCodeExport = g.First().GetValue("ProjectCodeExport")?.ToString(),
            //        PONumber = g.First().GetValue("PONumber")?.ToString(),
            //        TotalQty = g.Sum(node => TextUtils.ToDecimal(node.GetValue("Qty"))),
            //        UnitName = g.First().GetValue(colUnit.FieldName)?.ToString(),
            //    })
            //    .Where(x => x.POKHDetailIDActual > 0)
            //    .ToList();


            //var totalQtyProjects = nodes
            //    .GroupBy(node => new
            //    {
            //        ProductID = TextUtils.ToInt(node.GetValue("ProductID")),
            //        ProjectID = TextUtils.ToInt(node.GetValue("ProjectID")),
            //        //POKHDetailIDActual = TextUtils.ToInt(node.GetValue("POKHDetailIDActual")),
            //        //ChosenInventoryProject = TextUtils.ToInt(node.GetValue("ChosenInventoryProject")),
            //        ID = TextUtils.ToInt(node.GetValue("ID")),
            //    })
            //    .Select(g => new
            //    {
            //        BillExportDetailID = string.Join(",", g.Select(node => TextUtils.ToInt(node.GetValue("ID")))),
            //        g.Key.ProductID,
            //        ProjectID = g.Key.ProjectID,
            //        POKHDetailIDActual = 0,
            //        g.Key.ID,
            //        ProductNewCode = g.First().GetValue("ProductNewCode")?.ToString(),
            //        ProjectCodeExport = g.First().GetValue("ProjectCodeExport")?.ToString(),
            //        PONumber = g.First().GetValue("PONumber")?.ToString(),
            //        TotalQty = g.Sum(node => TextUtils.ToDecimal(node.GetValue("Qty"))),
            //        UnitName = g.First().GetValue(colUnit.FieldName)?.ToString(),
            //    })
            //    .Where(x => x.ProjectID > 0)
            //    .ToList();

            //var totalQtys = totalQtyPOKHs.Union(totalQtyProjects).ToList();

            var totalQtys = nodes
                            .GroupBy(node =>
                            {
                                int productId = TextUtils.ToInt(node.GetValue("ProductID"));
                                int projectId = TextUtils.ToInt(node.GetValue("ProjectID"));
                                int pokhDetailId = TextUtils.ToInt(node.GetValue("POKHDetailIDActual"));
                                int id = TextUtils.ToInt(node.GetValue("ID"));

                                return new
                                {
                                    ProductID = productId,
                                    ProjectID = pokhDetailId > 0 ? 0 : projectId,// nếu có POKH thì ProjectID = 0
                                    POKHDetailIDActual = pokhDetailId > 0 ? pokhDetailId : 0,
                                    ID = id
                                };
                            })
                            .Select(g => new
                            {
                                BillExportDetailID = string.Join(",", g.Select(node => TextUtils.ToInt(node.GetValue("ID")))),
                                g.Key.ProductID,
                                g.Key.ProjectID,
                                g.Key.POKHDetailIDActual,
                                g.Key.ID,
                                ProductNewCode = g.First().GetValue("ProductNewCode")?.ToString(),
                                ProjectCodeExport = g.First().GetValue("ProjectCodeExport")?.ToString(),
                                PONumber = g.First().GetValue("PONumber")?.ToString(),
                                TotalQty = g.Sum(node => TextUtils.ToDecimal(node.GetValue("Qty"))),
                                UnitName = g.First().GetValue(colUnit.FieldName)?.ToString(),
                            })
                            .Where(x => x.POKHDetailIDActual > 0 || x.ProjectID > 0)
                            .ToList();


            if (totalQtys.Count <= 0)
            {

                totalQtys = nodes.GroupBy(node => new
                {
                    ProductID = TextUtils.ToInt(node.GetValue("ProductID")),
                    ID = TextUtils.ToInt(node.GetValue("ID")),
                })
                                 .Select(g => new
                                 {
                                     BillExportDetailID = string.Join(",", g.Select(node => TextUtils.ToInt(node.GetValue("ID")))),
                                     g.Key.ProductID,
                                     ProjectID = 0,
                                     POKHDetailIDActual = 0,
                                     g.Key.ID,
                                     ProductNewCode = g.First().GetValue("ProductNewCode")?.ToString(),
                                     ProjectCodeExport = g.First().GetValue("ProjectCodeExport")?.ToString(),
                                     PONumber = g.First().GetValue("PONumber")?.ToString(),
                                     TotalQty = g.Sum(node => TextUtils.ToDecimal(node.GetValue("Qty"))),
                                     UnitName = g.First().GetValue(colUnit.FieldName)?.ToString(),
                                 })
                                 .Where(x => x.ProductID > 0)
                                 .ToList();
            }

            foreach (var item in totalQtys)
            {
                if (unitNames.Contains(item.UnitName.Trim().ToLower())) continue;
                //int billExportDetailID = TextUtils.ToInt(node.GetValue("ID"));
                int productID = TextUtils.ToInt(item.ProductID);
                int projectID = TextUtils.ToInt(item.ProjectID);
                int pokhDetailID = TextUtils.ToInt(item.POKHDetailIDActual);
                decimal totalQty = TextUtils.ToInt(item.TotalQty);

                string productNewCode = TextUtils.ToString(item.ProductNewCode);
                string projectCode = TextUtils.ToString(item.ProjectCodeExport);
                string poNumber = TextUtils.ToString(item.PONumber);

                DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetInventoryProjectImportExport",
                                        new string[] { "@WarehouseID", "@ProductID", "@ProjectID", "@POKHDetailID", "@BillExportDetailID" },
                                        new object[] { warehouse.ID, productID, projectID, pokhDetailID, item.BillExportDetailID });


                var inventoryProjects = dataSet.Tables[0];

                var dtImport = dataSet.Tables[1];
                var dtExport = dataSet.Tables[2];
                var dtStock = dataSet.Tables[3];

                decimal totalQuantityKeep = inventoryProjects.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(inventoryProjects.Rows[0]["TotalQuantity"]); //Sl giữ
                decimal totalQuantityKeepShow = totalQuantityKeep;
                totalQuantityKeep = Math.Max(totalQuantityKeep, 0);
                decimal totalQuantityLast = dtStock.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtStock.Rows[0]["TotalQuantityLast"]); //SL tồn CK
                decimal totalQuantityLastShow = totalQuantityLast;
                totalQuantityLast = Math.Max(totalQuantityLast, 0);

                decimal totalImport = dtImport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtImport.Rows[0]["TotalImport"]);
                decimal totalExport = dtExport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtExport.Rows[0]["TotalExport"]);
                decimal totalQuantityRemain = totalImport - totalExport;
                totalQuantityRemain = Math.Max(totalQuantityRemain, 0);

                //decimal totalStock = 0;
                //if (totalQuantityKeep > 0) totalStock = totalQuantityKeep + totalQuantityLast;
                //else if (totalQuantityRemain > 0) totalStock = totalQuantityRemain + totalQuantityLast;
                //else totalStock = totalQuantityLast;

                //if (totalQty > totalQuantityKeep)
                //{
                //    MessageBox.Show($"Số lượng xuất của sản phẩm [{productNewCode}] nhiều hơn SL đang giữ lại!\n" +
                //        $"SL xuất: {totalQty}\n" +
                //        $"SL giữ: {totalQuantityKeep}", "Thông báo");
                //    return false;
                //}

                decimal totalStock = totalQuantityKeep + totalQuantityRemain + totalQuantityLast;

                if (totalStock < totalQty)
                {
                    MessageBox.Show($"Số lượng còn lại của sản phẩm [{productNewCode}] không đủ!\n" +
                        $"SL xuất: {totalQty}\n" +
                        $"SL giữ: {totalQuantityKeepShow} | Tồn CK: {totalQuantityLastShow} | Tổng: {totalStock}", "Thông báo");
                    return false;
                }
            }

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
            //if (e.Column == colQty || e.Column == coProductSaleID)
            //{
            //    BillExportDetailModel detail = new BillExportDetailModel();
            //    int idBillExport = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //    detail = (BillExportDetailModel)(BillExportDetailBO.Instance.FindByPK(idBillExport));
            //    int ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, coProductSaleID));
            //    float sum = 0;
            //    for (int i = 0; i < grvData.RowCount; i++)
            //    {
            //        // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
            //        int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, coProductSaleID));
            //        if (ID == IDSearch)
            //        {
            //            float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
            //            sum += qty;
            //        }
            //        if (idBillExport > 0)
            //        {
            //            detail.Qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
            //            BillExportDetailBO.Instance.Update(detail);
            //        }
            //    }

            //    // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
            //    for (int j = 0; j < grvData.RowCount; j++)
            //    {
            //        int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, coProductSaleID));
            //        if (ID == IDSearch)
            //        {
            //            grvData.SetRowCellValue(j, colTotalQty, sum);
            //        }
            //        if (idBillExport > 0)
            //        {
            //            detail.Qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
            //            BillExportDetailBO.Instance.Update(detail);
            //        }
            //    }
            //}
            //if (e.Column == colProjectID)
            //{
            //    int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            //    if (projectID == 0) return;
            //    for (int i = 0; i < grvData.RowCount; i++)
            //    {
            //        int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
            //        if (item == 0)
            //        {
            //            grvData.SetRowCellValue(i, colProjectID, projectID);
            //        }
            //    }
            //}
        }

        void RecheckQty()
        {
            txtCode.Focus();
            //for (int k = 0; k < grvData.RowCount; k++)
            try
            {
                foreach (var k in treeList1.GetNodeList())
                {
                    int ID = TextUtils.ToInt(treeList1.GetRowCellValue(k, "ProductID"));
                    float sum = 0;
                    //for (int i = 0; i < grvData.RowCount; i++)
                    foreach (var i in treeList1.GetNodeList())
                    {
                        // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                        int IDSearch = TextUtils.ToInt(treeList1.GetRowCellValue(i, "ProductID"));
                        if (ID == IDSearch)
                        {
                            float qty = TextUtils.ToFloat(treeList1.GetRowCellValue(i, "Qty"));
                            sum += qty;
                        }
                    }
                    // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                    //for (int j = 0; j < grvData.RowCount; j++)
                    foreach (var j in treeList1.GetNodeList())
                    {
                        int IDSearch = TextUtils.ToInt(treeList1.GetRowCellValue(j, "ProductID"));
                        if (ID == IDSearch)
                        {
                            //treeList1.SetRowCellValue(j, "TotalQty", sum);
                            j.SetValue("TotalQty", sum);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
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

            //cboGroup.EditValue = group;
            //for (int i = 0; i < lstID.Count; i++)
            //{
            //    grvData.FocusedRowHandle = -1;
            //    btnNew_Click(null, null);
            //    DataRow[] rows = dtProduct.Select($"ID = {lstID[i]}");
            //    if (rows.Length > 0)
            //    {
            //        string productName = TextUtils.ToString(rows[0]["ProductName"]);
            //        string unit = TextUtils.ToString(rows[0]["Unit"]);
            //        if (grvData.RowCount > 0)
            //        {
            //            grvData.SetRowCellValue(grvData.RowCount - 1, coProductSaleID, lstID[i]);
            //            grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, productName);
            //            grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, unit);
            //        }
            //        else
            //        {
            //            grvData.SetRowCellValue(i, coProductSaleID, lstID[i]);
            //            grvData.SetRowCellValue(i, colProductName, productName);
            //            grvData.SetRowCellValue(i, colUnit, unit);
            //        }
            //    }
            //}

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode.Trim()).FirstOrDefault();
            if (warehouse == null) return;
            frmPOKHData frm = new frmPOKHData(warehouse.ID);
            //frm._listBillExportDetail = new ListBillExportDetail(Lst);
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
                //grvData.FocusedRowHandle = -1;
                if (dt.Rows.Count > 0)
                {
                    btnNew_Click(null, null);
                    if (treeList1.GetNodeList().Count > 0)
                    {
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "ProductID", dt.Rows[i]["ProductID"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "ProductName", dt.Rows[i]["ProductName"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "ProductNewCode", dt.Rows[i]["ProductNewCode"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "Unit", dt.Rows[i]["Unit"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "POKHID", dt.Rows[i]["ID"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "ProductFullName", dt.Rows[i]["GuestCode"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "Group", dt.Rows[i]["GroupPO"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "Qty", dt.Rows[i]["Qty"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "ProjectID", dt.Rows[i]["ProjectID"]);
                        //grvData.SetRowCellValue(treeList1.Nodes.LastNode, colProjectCode, dt.Rows[i]["ProjectCodeText"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "ProjectCode", dt.Rows[i]["ProjectCode"]);
                        treeList1.SetRowCellValue(treeList1.Nodes.LastNode, "Note", dt.Rows[i]["PONumber"]);
                    }
                    else
                    {
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "ProductID", dt.Rows[i]["ProductID"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colProductName", dt.Rows[i]["ProductName"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colProductNewCode", dt.Rows[i]["ProductNewCode"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colUnit", dt.Rows[i]["Unit"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colPOKHID", dt.Rows[i]["ID"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colProductFullName", dt.Rows[i]["GuestCode"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colGroup", dt.Rows[i]["GroupPO"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colQty", dt.Rows[i]["Qty"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colProjectID", dt.Rows[i]["ProjectID"]);
                        //grvData.SetRowCellValue(treeList1.Nodes[i], colProjectCode, dt.Rows[i]["ProjectCodeText"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colProjectCode", dt.Rows[i]["ProjectCode"]);
                        treeList1.SetRowCellValue(treeList1.Nodes[i], "colNote", dt.Rows[i]["PONumber"]);
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

            //if (e.Button == MouseButtons.Left)
            //{
            //    GridHitInfo info = grvData.CalcHitInfo(e.Location);

            //    if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
            //    {
            //        grvData.FocusedRowHandle = -1;
            //        dtDetail.AcceptChanges();
            //        DataRow dtrow = dtDetail.NewRow();

            //        int stt = 0;
            //        if (dtDetail.Rows.Count > 0)
            //        {
            //            stt = dtDetail.AsEnumerable().Max(x => x.Field<int>("STT"));
            //        }

            //        //int projectID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectID));
            //        //string projectName = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectName));

            //        dtrow["STT"] = stt + 1;
            //        //dtrow["ProjectID"] = projectID;
            //        //dtrow["ProjectName"] = projectName;
            //        dtDetail.Rows.Add(dtrow);
            //    }
            //}
        }

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            //if (e.Column == colQty)
            //{
            //    decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
            //    decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
            //    e.Merge = (value2 == 0);
            //    e.Handled = true;
            //    return;
            //}
            //else
            //{
            //    string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
            //    string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
            //    e.Merge = (value2 == "");
            //    e.Handled = true;
            //    return;
            //}

        }
        private void ckbMerge_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbMerge.Checked)
            //{
            //    grvData.OptionsView.AllowCellMerge = true;
            //    colGroup.GroupIndex = 0;
            //    grvData.ExpandAllGroups();
            //    grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            //}
            //else
            //{
            //    colGroup.GroupIndex = -1;
            //    grvData.OptionsView.AllowCellMerge = false;
            //    grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            //}

        }
        /// <summary>
        /// chọn dự án tự động hiển thị ra mã dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            txtCode.Focus();
            var node = treeList1.FocusedNode;
            int ID = TextUtils.ToInt(node.GetValue("ProjectID"));
            DataRow[] rows = dtProject.Select("ID= " + ID);
            if (rows.Length > 0)
            {
                projectCode = TextUtils.ToString(rows[0]["ProjectCode"]);
                node.SetValue("ProjectCodeExport", projectCode);
                //for (int i = 0; i < grvData.RowCount; i++)
                foreach (var i in treeList1.GetNodeList())
                {
                    string item = TextUtils.ToString(treeList1.GetRowCellValue(i, "ProjectCodeExport"));
                    if (item == "" || item == null)
                    {
                        treeList1.SetRowCellValue(i, "ProjectCodeExport", projectCode);
                    }
                }
            }
        }

        private void cbKhoType_EditValueChanged(object sender, EventArgs e)
        {
            //return;
            int value = TextUtils.ToInt(cbKhoType.EditValue);

            if (value == 4 && billExport.ID <= 0) //Kho vision
            {
                dtpCreatDate.MinDate = new DateTime(2025, 05, 28);
                dtpRequestDate.Properties.MinDate = new DateTime(2025, 05, 28);
            }
            //colQuantityRemain.VisibleIndex = 9;
            coltreeListColumn20.VisibleIndex = 9;
            //if (value == 70) colQuantityRemain.VisibleIndex = -1;
            if (value == 70) coltreeListColumn20.VisibleIndex = -1;

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
                //cboUser.EditValue = 23;//Nguyễn Thị Hiền
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
            int pokhID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("POKHID"));
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
            int strID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ID"));
            if (strID == 0) return;
            int strQty = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("Qty"));
            int productID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ProductID"));
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
            int projectID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ProjectID"));
            int productSaleID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ProductID"));
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
                string idText = string.Join(";", frm.inventoryProjects.Select(x => x.ID));
                string code = string.Join(";", frm.inventoryProjects.Select(x => x.CreatedBy));
                //string lstCodes = String.Join("; ", frm.lstYCMHCode);
                treeList1.SetFocusedRowCellValue("ChosenInventoryProject", idText);
                treeList1.SetFocusedRowCellValue("ProductCodeExport", code);
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //return;
            //treeList1.CloseEditor();
            if (Lib.LockEvents) return;
            Lib.LockEvents = true;

            try
            {
                if (treeList1.FocusedNode == null) return;
                if (e.Column == null) return;
                if (e.Column.FieldName == "Qty" || e.Column.FieldName == "ProductID")
                {
                    //BillExportDetailModel detail = new BillExportDetailModel();
                    //int idBillExport = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ID"));
                    //detail = (BillExportDetailModel)(BillExportDetailBO.Instance.FindByPK(idBillExport));
                    //detail = SQLHelper<BillExportDetailModel>.FindByID(idBillExport);
                    int ID = TextUtils.ToInt(treeList1.FocusedNode.GetValue("ProductID"));
                    float sum = 0;
                    //for (int i = 0; i < grvData.RowCount; i++)
                    foreach (var i in treeList1.GetNodeList())
                    {
                        // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                        int IDSearch = TextUtils.ToInt(treeList1.GetRowCellValue(i, "ProductID"));
                        if (ID == IDSearch)
                        {
                            float qty = TextUtils.ToFloat(treeList1.GetRowCellValue(i, "Qty"));
                            sum += qty;
                        }
                        //if (idBillExport > 0)
                        //{
                        //    detail.Qty = TextUtils.ToDecimal(treeList1.GetFocusedRowCellValue("Qty"));
                        //    //BillExportDetailBO.Instance.Update(detail);
                        //}
                    }

                    // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                    //for (int j = 0; j < grvData.RowCount; j++)
                    foreach (var j in treeList1.GetNodeList())
                    {
                        int IDSearch = TextUtils.ToInt(treeList1.GetRowCellValue(j, "ProductID"));
                        if (ID == IDSearch)
                        {
                            treeList1.SetRowCellValue(j, "TotalQty", sum);
                        }
                        //if (idBillExport > 0)
                        //{
                        //    detail.Qty = TextUtils.ToDecimal(treeList1.GetFocusedRowCellValue("Qty"));
                        //    //BillExportDetailBO.Instance.Update(detail);
                        //}
                    }

                }

                if (e.Column.FieldName == "ProjectID")
                {
                    int projectID = TextUtils.ToInt(treeList1.GetFocusedRowCellValue("ProjectID"));
                    if (projectID == 0) return;
                    //for (int i = 0; i < grvData.RowCount; i++)
                    foreach (var i in treeList1.GetNodeList())
                    {
                        int item = TextUtils.ToInt(treeList1.GetRowCellValue(i, "ProjectID"));
                        if (item == 0)
                        {
                            treeList1.SetRowCellValue(i, "ProjectID", projectID);
                        }
                    }
                }
                loadInventoryProject(e.Column, treeList1.FocusedNode);
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            int STT;
            TreeList tl = (TreeList)treeList1;

            //DataTable dt = (DataTable)tl.DataSource;
            //STT = dt.Rows.Count == 0 ? 1 : (TextUtils.ToInt(tl.GetRowCellValue(tl.Nodes.LastNode, colSTT)) + 1);
            STT = TextUtils.ToInt(tl.GetRowCellValue(tl.Nodes.LastNode, "STT")) + 1;

            TreeListNode node = treeList1.AppendNode(new object[] { }, null);

            node["STT"] = STT;
            node["ChildID"] = STT;
        }

        //private void loadInventoryProject(TreeListColumn treeListColumn, TreeListNode node)
        //{
        //    //Lib.LockEvents = true;
        //    try
        //    {
        //        if (node == null) return;
        //        if (treeListColumn.FieldName == "Qty" || treeListColumn.FieldName == "ProductID" || treeListColumn.FieldName == "ProjectCodeExport")
        //        {
        //            //if (Lib.LockEvents) return;
        //            float qty = TextUtils.ToFloat(node.GetValue("Qty"));
        //            int productID = TextUtils.ToInt(node.GetValue("ProductID"));
        //            //string projectCodeExport = TextUtils.ToString(treeList1.GetFocusedRowCellValue("ProjectCodeExport"));
        //            int projectID = TextUtils.ToInt(node.GetValue("ProjectID"));
        //            int poKHDetailID = TextUtils.ToInt(node.GetValue("POKHDetailIDActual"));

        //            if (qty <= 0 || productID <= 0 || (projectID <= 0 && poKHDetailID <= 0)) return;

        //            projectID = poKHDetailID > 0 ? 0 : projectID;
        //            List<DataRow> dt = SQLHelper<InventoryProjectModel>.LoadDataFromSP("spGetInventoryProject",
        //                new string[] { "@ProjectID", "@EmployeeID", "@ProductSaleID", "@Keyword", "@WarehouseID", "@POKHDetailID" },
        //                new object[] { projectID, 0, productID, "", warehouse.ID, poKHDetailID }).AsEnumerable()
        //                         .OrderBy(r => r.Field<DateTime>("CreatedDate"))
        //                         .Where(r => TextUtils.ToFloat(r["TotalQuantityRemain"]) > 0)
        //                         .ToList();


        //            //if (projectID <= 0)
        //            //{
        //            //    dt = dt.Where(p => TextUtils.ToString(p["Note"]).ToLower() == projectCodeExport.ToLower().Trim()).ToList();
        //            //}


        //            if (dt.Count <= 0) return;

        //            DataTable dt1 = treeList1.DataSource as DataTable;
        //            //if (dt1 == null) return;
        //            // Kiểm tra các sản phẩm đã tương tự trên grid check số lượng với  dt và kiểm tra ChosenInventoryProject map nếu id hết só lượng thì chọn id khác nếu còn sl thì sẽ map id 
        //            DataRow[] dt2 = new DataRow[] { };

        //            if (/*projectID > 0 &&*/ poKHDetailID > 0)
        //            {
        //                //dt2 = dt1.Select($"ProductID = {productID} And ProjectID = {projectID} and POKHDetailIDActual = {poKHDetailID}");
        //                dt2 = dt1.Select($"ProductID = {productID} and POKHDetailIDActual = {poKHDetailID}");
        //            }
        //            else if (projectID > 0)
        //            {
        //                dt2 = dt1.Select($"ProductID = {productID} And ProjectID = {projectID}");
        //            }
        //            //else
        //            //{
        //            //    dt2 = dt1.Select($"ProductID = {productID} and POKHDetailIDActual = {poKHDetailID}");
        //            //}
        //            // Nếu đã có ID đã chọn thì bỏ đi để chọn lại từ đầu
        //            node.SetValue("ChosenInventoryProject", "");
        //            node.SetValue("ProductCodeExport", "");
        //            // Tính tổng đã dùng của từng InventoryProject.ID trong treeList1
        //            Dictionary<int, float> usedQuantityByInventoryID = new Dictionary<int, float>();
        //            foreach (DataRow row in dt2)
        //            {
        //                string ids = TextUtils.ToString(row["ChosenInventoryProject"]);
        //                float usedQty = TextUtils.ToFloat(row["Qty"]);

        //                if (!string.IsNullOrWhiteSpace(ids))
        //                {
        //                    foreach (var idStr in ids.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
        //                    {
        //                        if (int.TryParse(idStr, out int id))
        //                        {
        //                            if (!usedQuantityByInventoryID.ContainsKey(id))
        //                                usedQuantityByInventoryID[id] = 0;

        //                            usedQuantityByInventoryID[id] += usedQty;
        //                        }
        //                    }
        //                }
        //            }

        //            var filtered = new List<DataRow>();
        //            float remainingQty = qty;


        //            //TreeListNode focused_aaa = treeList1.FocusedNode;
        //            foreach (var row in dt)
        //            {
        //                int id = row.Field<int>("ID");
        //                float remain = TextUtils.ToFloat(row["TotalQuantityRemain"]);
        //                float used = usedQuantityByInventoryID.ContainsKey(id) ? usedQuantityByInventoryID[id] : 0;

        //                //int idDetail = TextUtils.ToInt(focused_aaa.GetValue("ID"));

        //                float available = remain - used;
        //                if (available <= 0) continue;

        //                // Chỉ lấy phần còn lại đủ để đạt tới qty cần
        //                filtered.Add(row);
        //                remainingQty -= Math.Min(available, remainingQty);

        //                if (remainingQty <= 0) break;
        //            }
        //            // Kết quả sẽ lưu vào đây nếu tìm được tập con đúng bằng targetQty
        //            var exactMatch = new List<DataRow>();


        //            Dictionary<int, float> dicCheckQuantity = new Dictionary<int, float>();
        //            // Gọi hàm subset sum
        //            if (FindSubsetSum(filtered, qty, 0, new List<DataRow>(), exactMatch, usedQuantityByInventoryID))
        //            {
        //                // Tìm được tập con có tổng bằng qty
        //                dt = exactMatch;
        //                // 
        //                foreach (var row in dt)
        //                {
        //                    int id = row.Field<int>("ID");
        //                    float remain = TextUtils.ToFloat(row["TotalQuantityRemain"]);

        //                    dicCheckQuantity.Add(id, remain);
        //                }
        //            }
        //            else
        //            {

        //                float accumulated = 0;
        //                var greedy = new List<DataRow>();
        //                float qtyCheck = 0;
        //                foreach (var row in filtered)
        //                {
        //                    int id = row.Field<int>("ID");

        //                    float remain = TextUtils.ToFloat(row["TotalQuantityRemain"]);
        //                    if (remain <= 0) continue;
        //                    float used = usedQuantityByInventoryID.ContainsKey(id) ? usedQuantityByInventoryID[id] : 0;
        //                    float available = remain - used;
        //                    greedy.Add(row);
        //                    accumulated += available;
        //                    if (accumulated >= qty)
        //                    {
        //                        accumulated -= available;
        //                        dicCheckQuantity.Add(id, qty - accumulated);
        //                        accumulated += available;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        dicCheckQuantity.Add(id, remain);
        //                    }
        //                }
        //                if (qty > accumulated && accumulated > 0)
        //                {
        //                    float newQty = qty - accumulated;
        //                    TreeList tl = treeList1;

        //                    // 1) Tính STT mới
        //                    int STT = 1;
        //                    if (tl.Nodes.Count > 0)
        //                    {
        //                        STT = TextUtils.ToInt(tl.GetRowCellValue(tl.Nodes.LastNode, "STT")) + 1;
        //                    }

        //                    // 2) Lấy node đang focus
        //                    TreeListNode focused = tl.FocusedNode;
        //                    if (focused == null) return;

        //                    // 3) Tạo node mới bằng cách clone
        //                    TreeListNode newNode = tl.AppendNode(new object[] { }, null); // cùng cấp với node đang focus

        //                    // 3) Tạo row mới từ dữ liệu node đang focus
        //                    ////DataRow newRow = dt1.NewRow();
        //                    foreach (DataColumn col in dt1.Columns)
        //                    {
        //                        if (col.ColumnName == "STT" || col.ColumnName == "Qty") continue;

        //                        newNode[col.ColumnName] = focused[col.ColumnName];
        //                    }

        //                    // 5) Ghi đè lại STT và Qty
        //                    newNode.SetValue("STT", STT);
        //                    //newNode.SetValue("ID", STT);
        //                    newNode.SetValue("ChildID", STT);
        //                    newNode.SetValue("Qty", newQty);
        //                    newNode.SetValue("ChosenInventoryProject", "");
        //                    newNode.SetValue("ProductCodeExport", "");
        //                    // 6) Cập nhật lại Qty của node đang focus
        //                    focused.SetValue("Qty", accumulated);
        //                }
        //                if (greedy.Any()) dt = greedy;
        //            }

        //            string idText = string.Join(";", dt.Select(x => x.Field<int>("ID")));
        //            string result = string.Join(";", dicCheckQuantity.Select(kv => $"{kv.Key}-{kv.Value}"));
        //            string code = string.Join(";", dt.Select(x => x.Field<string>("ProductCode")));
        //            //string lstCodes = String.Join("; ", frm.lstYCMHCode);
        //            node.SetValue("ChosenInventoryProject", result);
        //            node.SetValue("ProductCodeExport", code);
        //            dt1.AcceptChanges();
        //            treeList1.DataSource = dt1;
        //        }

        //        //treeList1.RefreshDataSource();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //    finally
        //    {
        //        //Lib.LockEvents = false;
        //    }

        //}

        private void loadInventoryProject(TreeListColumn treeListColumn, TreeListNode node)
        {
            try
            {
                if (node == null) return;
                if (treeListColumn.FieldName == "Qty" || treeListColumn.FieldName == "ProductID" || treeListColumn.FieldName == "ProjectCodeExport")
                {
                    float qty = TextUtils.ToFloat(node.GetValue("Qty"));
                    int productID = TextUtils.ToInt(node.GetValue("ProductID"));
                    int projectID = TextUtils.ToInt(node.GetValue("ProjectID"));
                    int poKHDetailID = TextUtils.ToInt(node.GetValue("POKHDetailIDActual"));

                    if (qty <= 0 || productID <= 0 || (projectID <= 0 && poKHDetailID <= 0)) return;

                    if (poKHDetailID > 0) projectID = 0;

                    // Reset values
                    node.SetValue(colChosenInventoryProject.FieldName, "");
                    //node.SetValue("ProjectCodeExport", "");

                    // Lấy danh sách inventory có sẵn
                    List<DataRow> dt = SQLHelper<InventoryProjectModel>.LoadDataFromSP("spGetInventoryProject",
                        new string[] { "@ProjectID", "@EmployeeID", "@ProductSaleID", "@Keyword", "@WarehouseID", "@POKHDetailID" },
                        new object[] { projectID, 0, productID, "", warehouse.ID, poKHDetailID }).AsEnumerable()
                                 .OrderBy(r => r.Field<DateTime>("CreatedDate"))
                                 .Where(r => TextUtils.ToFloat(r["TotalQuantityRemain"]) > 0)
                                 .ToList();

                    DataTable dt1 = treeList1.DataSource as DataTable;
                    DataRow[] dt2 = new DataRow[] { };

                    if (poKHDetailID > 0)
                    {
                        dt2 = dt1.Select($"ProductID = {productID} and POKHDetailIDActual = {poKHDetailID}");
                    }
                    else if (projectID > 0)
                    {
                        dt2 = dt1.Select($"ProductID = {productID} And ProjectID = {projectID}");
                    }

                    // Kiểm tra tồn kho để đảm bảo có thể xuất
                    DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetInventoryProjectImportExport",
                        new string[] { "@WarehouseID", "@ProductID", "@ProjectID", "@POKHDetailID", "@BillExportDetailID" },
                        new object[] { warehouse.ID, productID, projectID, poKHDetailID, "" });

                    var dtStock = dataSet.Tables[3];
                    float totalStockAvailable = dtStock.Rows.Count > 0 ? TextUtils.ToFloat(dtStock.Rows[0]["TotalQuantityLast"]) : 0;
                    totalStockAvailable = Math.Max(totalStockAvailable, 0);
                    // Nếu không có kho giữ
                    if (dt.Count <= 0)
                    {
                        if (totalStockAvailable >= qty)
                        {
                            // Có đủ tồn kho, để trống để lấy từ tồn kho
                            return;
                        }
                        else
                        {
                            // Không đủ tồn kho
                            return;
                        }
                    }

                    // Tính tổng đã sử dụng từ các node khác
                    Dictionary<int, float> usedQuantityByInventoryID = new Dictionary<int, float>();
                    int currentNodeChildID = TextUtils.ToInt(node.GetValue("ChildID"));

                    foreach (DataRow row in dt2)
                    {
                        int rowChildID = TextUtils.ToInt(row["ChildID"]);
                        if (rowChildID == currentNodeChildID) continue;

                        string ids = TextUtils.ToString(row["ChosenInventoryProject"]);
                        if (!string.IsNullOrWhiteSpace(ids))
                        {
                            foreach (var idStr in ids.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (idStr.Contains('-'))
                                {
                                    var parts = idStr.Split('-');
                                    if (int.TryParse(parts[0], out int id) && float.TryParse(parts[1], out float allocatedQty))
                                    {
                                        if (!usedQuantityByInventoryID.ContainsKey(id))
                                            usedQuantityByInventoryID[id] = 0;
                                        usedQuantityByInventoryID[id] += allocatedQty;
                                    }
                                }
                            }
                        }
                    }

                    // Kiểm tra xem kho giữ có đủ cho node này không
                    float availableFromKeep = 0;
                    foreach (var row in dt)
                    {
                        int id = row.Field<int>("ID");
                        float totalRemain = TextUtils.ToFloat(row["TotalQuantityRemain"]);
                        float used = usedQuantityByInventoryID.ContainsKey(id) ? usedQuantityByInventoryID[id] : 0;
                        float available = Math.Max(0, totalRemain - used);
                        availableFromKeep += available;
                    }

                    float remainingQty = qty;
                    availableFromKeep = Math.Max(availableFromKeep, 0);
                    if (availableFromKeep > 0)
                    {
                        // Đủ kho giữ - Lấy từ kho giữ

                        var selectedInventory = new Dictionary<int, float>();
                        //float remainingQty = qty;

                        foreach (var row in dt)
                        {
                            int id = row.Field<int>("ID");
                            float totalRemain = TextUtils.ToFloat(row["TotalQuantityRemain"]);
                            float used = usedQuantityByInventoryID.ContainsKey(id) ? usedQuantityByInventoryID[id] : 0;
                            float available = Math.Max(0, totalRemain - used);

                            if (available <= 0 || remainingQty <= 0) continue;

                            float allocateQty = Math.Min(available, remainingQty);
                            selectedInventory[id] = allocateQty;
                            remainingQty -= allocateQty;

                            if (remainingQty <= 0) break;
                        }

                        if (selectedInventory.Any())
                        {
                            string result = string.Join(";", selectedInventory.Select(kv => $"{kv.Key}-{kv.Value}"));
                            string codes = string.Join(";", dt.Where(r => selectedInventory.ContainsKey(r.Field<int>("ID")))
                                                              .Select(r => r.Field<string>("ProductCode")));

                            node.SetValue("ChosenInventoryProject", result);
                            //node.SetValue("ProductCodeExport", codes);
                        }

                        availableFromKeep = remainingQty;
                    }

                    //if (true)
                    //{

                    //}
                    //else
                    //{
                    // Kho giữ không đủ - Kiểm tra tồn kho
                    //if (totalStockAvailable >= qty)
                    if (totalStockAvailable >= remainingQty)
                    {
                        // Bỏ qua kho giữ, lấy toàn bộ từ tồn kho
                        // Để trống ChosenInventoryProject để lấy từ tồn kho
                    }
                    else
                    {
                        // Không đủ cả kho giữ lẫn tồn kho
                        float totalAvailable = availableFromKeep + totalStockAvailable;
                        // Không làm gì, để validation xử lý
                    }
                    //}

                    dt1.AcceptChanges();
                    treeList1.DataSource = dt1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        /// <summary>
        /// Đệ quy tìm subset của 'rows' có tổng AvailableRemain == target (với epsilon),
        /// trong đó AvailableRemain = TotalQuantityRemain – usedQuantityByInventoryID[id].
        /// Lưu kết quả vào 'result' khi tìm được.
        /// </summary>
        private bool FindSubsetSum(List<DataRow> rows, float target, int index, List<DataRow> current, List<DataRow> result, Dictionary<int, float> usedQuantityByInventoryID, float epsilon = 0.001f)
        {
            // Tính tổng của current dựa trên available = total – used
            float sum = current.Sum(r =>
            {
                int id = r.Field<int>("ID");
                float total = TextUtils.ToFloat(r["TotalQuantityRemain"]);
                float used = usedQuantityByInventoryID.ContainsKey(id)
                             ? usedQuantityByInventoryID[id]
                             : 0f;
                return total - used;
            });

            // Nếu đã "đủ" gần bằng target
            if (Math.Abs(sum - target) < epsilon)
            {
                result.AddRange(current);
                return true;
            }

            // Ngắt điều kiện: vượt target hoặc hết danh sách
            if (sum > target || index >= rows.Count)
                return false;

            // Xét phần tử rows[index]:
            // - tính available = TotalQuantityRemain – used
            int idIndex = rows[index].Field<int>("ID");
            float totalIndex = TextUtils.ToFloat(rows[index]["TotalQuantityRemain"]);
            float usedIndex = usedQuantityByInventoryID.ContainsKey(idIndex)
                               ? usedQuantityByInventoryID[idIndex]
                               : 0f;
            float availIndex = totalIndex - usedIndex;

            // Chỉ chọn nếu còn > 0
            if (availIndex > epsilon)
            {
                current.Add(rows[index]);
                if (FindSubsetSum(rows, target, index + 1, current, result, usedQuantityByInventoryID, epsilon))
                    return true;
                current.RemoveAt(current.Count - 1);
            }

            // Bỏ qua phần tử rows[index]
            if (FindSubsetSum(rows, target, index + 1, current, result, usedQuantityByInventoryID, epsilon))
                return true;

            return false;
        }



        #region Update yc chuyển kho
        void loadKhoTransfer()
        {
            var warehouses = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouseTransfer.Properties.DataSource = warehouses;
            cboWarehouseTransfer.Properties.DisplayMember = "WarehouseName";
            cboWarehouseTransfer.Properties.ValueMember = "ID";
        }

        private BillImportModel CreateNewBillImport(BillImportModel import = null)
        {
            var billImport = import ?? new BillImportModel();

            billImport.Deliver = cboSender.Text.Trim();
            billImport.Reciver = cboUser.Text.Trim();
            //billImport.Suplier = cboSupplier.Text.Trim();
            billImport.KhoType = cbKhoType.Text.Trim();
            billImport.KhoTypeID = TextUtils.ToInt(cbProductType.EditValue); //TN.Binh update 10/08/25
            billImport.DeliverID = TextUtils.ToInt(cboSender.EditValue);
            billImport.ReciverID = TextUtils.ToInt(cboUser.EditValue);
            //billImport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            billImport.SupplierID = 16677;
            billImport.GroupID = TextUtils.ToString(cboGroup.EditValue);
            billImport.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            billImport.DateRequestImport = DateTime.Now; //TN.Binh update 09/08/25
            billImport.WarehouseID = TextUtils.ToInt(cboWarehouseTransfer.EditValue);
            billImport.BillTypeNew = 4; // Phiếu nhập kho do chuyển kho tạo ra
            billImport.CreatDate = dtpCreatDate.Value;

            if (import == null) // tạo mới
            {
                billImport.CreatedDate = DateTime.Now;
                billImport.CreatedBy = Global.AppUserName;

                // Sinh mã phiếu tự động
                billImport.BillImportCode = GetBillCode(billImport.BillTypeNew.GetValueOrDefault());
            }

            billImport.UpdatedDate = DateTime.Now;
            billImport.UpdatedBy = Global.AppUserName;
            billImport.Status = false;

            return billImport;
        }

        public string GetBillCode(int billtype)
        {
            string billCode = "";

            DateTime billDate = DateTime.Now;

            string preCode = "";
            if (billtype == 0 || billtype == 4) preCode = "PNK";
            else if (billtype == 1) preCode = "PT";
            else if (billtype == 3) preCode = "PNM";
            else preCode = "PTNB";

            //BillImport billImport = GetAll().Where(x => (x.CreatDate ?? DateTime.MinValue).Year == billDate.Year &&
            //                                                     (x.CreatDate ?? DateTime.MinValue).Month == billDate.Month &&
            //                                                     (x.CreatDate ?? DateTime.MinValue).Day == billDate.Day)
            //                                .OrderByDescending(x => x.ID)
            //                                .FirstOrDefault() ?? new BillImport();
            //                                .FirstOrDefault() ?? new BillImport();
            //string code = billDate.ToString("yyMMdd");
            List<BillImportModel> billImports = SQLHelper<BillImportModel>.FindAll().Where(x => (x.BillImportCode ?? "").Contains(billDate.ToString("yyMMdd"))).ToList();

            var listCode = billImports.Select(x => new
            {
                ID = x.ID,
                Code = x.BillImportCode,
                STT = string.IsNullOrWhiteSpace(x.BillImportCode) ? 0 : Convert.ToInt32(x.BillImportCode.Substring(x.BillImportCode.Length - 3)),
            }).ToList();

            string numberCodeText = "000";
            //string lastBillCode = string.IsNullOrWhiteSpace(billImport.BillImportCode) ? $"{preCode}{billDate.ToString("yyMMdd")}{numberCodeText}" : billImport.BillImportCode.Trim();
            //int numberCode = Convert.ToInt32(lastBillCode.Substring(lastBillCode.Length - 3));
            int numberCode = listCode.Count <= 0 ? 0 : listCode.Max(x => x.STT);
            numberCodeText = (++numberCode).ToString();
            while (numberCodeText.Length < 3)
            {
                numberCodeText = "0" + numberCodeText;
            }

            billCode = $"{preCode}{billDate.ToString("yyMMdd")}{numberCodeText}";

            return billCode;
        }


        void SaveBillImport()
        {
            List<BillImportModel> billImports = SQLHelper<BillImportModel>.FindByAttribute("BillExportID", billExport.ID);
            BillImportModel billImportModel = billImports.Where(p => p.IsDeleted == false).FirstOrDefault(); // Lấy bản ghi đầu tiên, hoặc null nếu danh sách rỗng
                                                                                                             //BillExportBO.Instance.Update(billExport);
            if (billImportModel == null && billExport.IsTransfer == true)
            {
                // Trường hợp: ban đầu không tạo phiếu nhập, giờ mới tạo
                billImport = CreateNewBillImport();
                billImport.BillExportID = billExport.ID;
                billImport.ID = SQLHelper<BillImportModel>.Insert(billImport).ID;
                //billExport.BillImportID = billImport.ID;

                TextUtils.ExcuteScalar("spCreateDocumentImport",
                    new string[] { "@BillImportID", "@CreatedBy" },
                    new object[] { billImport.ID, Global.LoginName });
                // **TỰ ĐỘNG TẠO CHI TIẾT PHIẾU NHẬP**
                //SaveBillImportDetails(billImport.ID, billExport.ID);
            }
            else if (billImportModel != null)
            {

                if (billImportModel.BillTypeNew != 4)
                {
                    MessageBox.Show("Phiếu nhập đã thay đổi trạng thái, không thể sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (billExport.IsTransfer == false)
                {
                    billImportModel.IsDeleted = true;
                }

                // Cho phép cập nhật phiếu nhập
                billImport = CreateNewBillImport(billImportModel); // override dữ liệu
                billImport.BillExportID = billExport.ID;
                SQLHelper<BillImportModel>.Update(billImport);

                //cập nhật phiếu xuất 
                SQLHelper<BillExportModel>.Update(billExport);

                // **CẬP NHẬT CHI TIẾT PHIẾU NHẬP**
                /* if (billExport.IsTransfer == true)
                 {
                     SaveBillImportDetails(billImport.ID, billExport.ID);
                 }*/
            }
        }


        private void SaveBillImportDetails(int billImportId, BillExportModel billExport)
        {
            try
            {
                // 1. XÓA ĐÚNG CÁCH - xóa theo BillImportID thay vì theo ID
                var existingDetails = SQLHelper<BillImportDetailModel>.FindByAttribute("BillImportID", billImportId);
                if (existingDetails != null && existingDetails.Count > 0)
                {
                    foreach (var detail in existingDetails)
                    {
                        // Xóa các bản ghi liên quan trước
                        BillImportDetailSerialNumberBO.Instance.DeleteByAttribute("BillImportDetailID", detail.ID);

                        // Xóa chi tiết phiếu nhập
                        SQLHelper<BillImportDetailModel>.DeleteModelByID(detail.ID);
                    }
                }

                // 2. LẤY DANH SÁCH CHI TIẾT PHIẾU XUẤT
                var exportDetails = SQLHelper<BillExportDetailModel>.FindByAttribute("BillID", billExport.ID);

                if (exportDetails == null || exportDetails.Count == 0)
                {
                    return; // Không có chi tiết nào để xử lý
                }

                foreach (var exportDetail in exportDetails)
                {
                    if (exportDetail.ProductID <= 0) continue; // Bỏ qua sản phẩm không hợp lệ

                    // Tạo chi tiết phiếu nhập mới
                    var importDetail = new BillImportDetailModel
                    {
                        BillImportID = billImportId,
                        ProductID = exportDetail.ProductID,
                        Qty = exportDetail.Qty,
                        Price = 0, // Có thể lấy từ bảng giá hoặc để 0
                        TotalPrice = 0, // Tính toán nếu cần
                        ProjectName = exportDetail.ProjectName,
                        ProjectCode = exportDetail.ProjectName, // Sử dụng ProjectName làm ProjectCode
                        SomeBill = "", // Để trống hoặc lấy từ nguồn khác
                        //Note = exportDetail.Note,
                        Note = billExport.Code,
                        STT = exportDetail.STT,
                        TotalQty = exportDetail.TotalQty,
                        ProjectID = exportDetail.ProjectID,
                        SerialNumber = exportDetail.SerialNumber,
                        BillExportDetailID = exportDetail.ID, // Liên kết với chi tiết phiếu xuất
                        //CreatedDate = DateTime.Now,
                        //CreatedBy = Global.AppUserName,
                        //UpdatedDate = DateTime.Now,
                        //UpdatedBy = Global.AppUserName,
                        ReturnedStatus = false, // Chưa trả
                        InventoryProjectID = null, // Sẽ được cập nhật sau nếu cần
                        //PONCCDetailID = exportDetail.POKHDetailID, // Map từ POKHDetailID nếu có
                        QtyRequest = exportDetail.Qty, // Số lượng yêu cầu = số lượng xuất
                        //IsKeepProject = true // Mặc định giữ dự án
                    };

                    ////Add vào kho giữ - tương tự code gốc
                    //if (importDetail.ProjectID > 0)
                    //{
                    //    // Tạo mới InventoryProject vì không có sẵn inventoryProjectID từ grid
                    //    InventoryProjectModel inventoryProject = new InventoryProjectModel();
                    //    inventoryProject.ProjectID = importDetail.ProjectID.Value;
                    //    inventoryProject.ProductSaleID = importDetail.ProductID.Value;
                    //    inventoryProject.WarehouseID = billImport.WarehouseID;
                    //    inventoryProject.Quantity = importDetail.Qty ?? 0;
                    //    inventoryProject.EmployeeID = Global.EmployeeID;

                    //    // Kiểm tra xem đã tồn tại chưa
                    //    var existingInventoryProject = SQLHelper<InventoryProjectModel>.FindAll()
                    //                                                                   .FirstOrDefault(x => x.ProjectID == inventoryProject.ProjectID &&
                    //     x.ProductSaleID == inventoryProject.ProductSaleID &&
                    //     x.WarehouseID == inventoryProject.WarehouseID);

                    //    if (existingInventoryProject != null && existingInventoryProject.ID > 0)
                    //    {
                    //        // Cập nhật số lượng
                    //        existingInventoryProject.Quantity += inventoryProject.Quantity;
                    //        SQLHelper<InventoryProjectModel>.Update(existingInventoryProject);
                    //        importDetail.InventoryProjectID = existingInventoryProject.ID;
                    //    }
                    //    else
                    //    {
                    //        // Tạo mới
                    //        inventoryProject.ID = SQLHelper<InventoryProjectModel>.Insert(inventoryProject).ID;
                    //        importDetail.InventoryProjectID = inventoryProject.ID;
                    //    }
                    //}


                    // INSERT CHI TIẾT PHIẾU NHẬP (luôn tạo mới sau khi đã xóa hết)
                    importDetail.ID = SQLHelper<BillImportDetailModel>.Insert(importDetail).ID;

                    // Xử lý xóa các record cũ nếu có (tương tự code gốc)
                    if (lstIDDelete.Count > 0)
                    {
                        foreach (int IdBillImportDetail in lstIDDelete)
                        {
                            BillImportDetailBO.Instance.Delete(IdBillImportDetail);
                            BillImportDetailSerialNumberBO.Instance.DeleteByAttribute("BillImportDetailID", IdBillImportDetail);
                        }
                    }

                    // Check xem có trong Inventory chưa, nếu chưa có thì thêm mới   
                    UpdateInventory(TextUtils.ToInt(importDetail.ProductID));

                    // Add notify nếu BillTypeNew = 4 (tương tự code gốc)
                    if (billImport.BillTypeNew == 4)
                    {
                        // Lấy thông tin sản phẩm để thông báo
                        var product = SQLHelper<ProductSaleModel>.FindByID(importDetail.ProductID ?? 0);
                        if (product != null)
                        {
                            string productCode = product.ProductCode ?? "";
                            string productName = product.ProductName ?? "";
                            string text = $"Mã sản phẩm: {productCode}\n" +
                                            $"Tên sản phẩm: {productName}\n";
                            // TextUtils.AddNotify("THÔNG BÁO HÀNG VỀ - SALE", text, detail.EmployeeIDBorrow);
                            // Note: EmployeeIDBorrow không có trong exportDetail, có thể cần lấy từ nguồn khác
                        }
                    }
                }

                // Cập nhật trạng thái đã trả cho phiếu mượn (tương tự code gốc)
                TextUtils.ExcuteScalar("spUpdateReturnedStatusForBillExportDetail",
                    new string[] { "@BillImportID", "@Approved" },
                    new object[] { billImportId, 0 });

                // Update trạng thái PO NCC nếu cần
                var listDetails = SQLHelper<BillImportDetailModel>.FindByAttribute("BillImportID", billImportId);
                string poNCCDetailID = string.Join(",", listDetails.Where(x => x.PONCCDetailID.HasValue).Select(x => x.PONCCDetailID));
                if (!string.IsNullOrEmpty(poNCCDetailID))
                {
                    TextUtils.ExcuteProcedure("spUpdateStatusPONCC",
                        new string[] { "@PONCCDetailID", "@UpdatedBy" },
                        new object[] { poNCCDetailID, Global.LoginName });
                }
            }
            catch (Exception ex)
            {
                // Log error hoặc throw exception
                throw new Exception($"Lỗi khi tạo chi tiết phiếu nhập: {ex.Message}");
            }
        }


        void UpdateInventory(int productId)
        {
            //check xem có trong Inventory chưa, nếu chưa có thì thêm mới   
            //ArrayList arr = InventoryBO.Instance.FindByExpression(new Expression("WarehouseID", billImport.WarehouseID).And(new Expression("ProductSaleID", detail.ProductID)));

            var exp1 = new Expression("ProductSaleID", productId);
            var exp2 = new Expression("WarehouseID", billImport.WarehouseID);

            List<InventoryModel> list = SQLHelper<InventoryModel>.FindByExpression(exp1.And(exp2)).ToList();

            if (list.Count <= 0)
            {
                InventoryModel inventory = new InventoryModel();
                inventory.WarehouseID = TextUtils.ToInt(billImport.WarehouseID);
                inventory.ProductSaleID = productId;
                inventory.TotalQuantityFirst = inventory.TotalQuantityLast = inventory.Import = inventory.Export = 0;
                InventoryBO.Instance.Insert(inventory);
            }
        }

        private void LinkBillImportTrasfer()
        {
            stackPanel2.Controls.Clear();
            // Lấy BillImportID từ phiếu xuất hiện tại
            if (billExport?.ID != null && billExport.ID > 0)
            {
                int billExportID = billExport.ID;
                // Truy vấn thông tin phiếu nhập tương ứng
                List<BillImportModel> billImports = SQLHelper<BillImportModel>.FindByAttribute("BillExportID", billExportID);
                BillImportModel billImport = billImports.Where(p => p.IsDeleted == false).FirstOrDefault() ?? new BillImportModel(); // Lấy bản ghi đầu tiên, hoặc null nếu danh sách rỗng
                WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(billImport.WarehouseID ?? 0) ?? new WarehouseModel();
                if (billImport != null) //TN.Binh update 09/08/2025
                {
                    LinkLabel linkLabel = new LinkLabel();
                    if (billImport.IsDeleted == true)
                    {
                        linkLabel.Text = $"{billImport.BillImportCode} - đã xóa";
                        linkLabel.Enabled = false;
                        linkLabel.Tag = $"{billImport.ID};{billImport.KhoTypeID};{warehouse.WarehouseCode}";
                        linkLabel.AutoSize = true;
                    }
                    else
                    {
                        // Hiển thị tên phiếu nhập - mã kho
                        linkLabel.Text = $"{billImport.BillImportCode}";
                        linkLabel.Tag = $"{billImport.ID};{billImport.KhoTypeID};{warehouse.WarehouseCode}";
                        linkLabel.AutoSize = true;

                        // Gắn sự kiện click để mở chi tiết phiếu nhập
                        linkLabel.LinkClicked += (s, evt) =>
                        {
                            string tag = linkLabel.Tag.ToString();
                            string[] parts = tag.Split(';');
                            if (parts.Length >= 3)
                            {
                                int billImportID = TextUtils.ToInt(parts[0]);
                                int warehouseType = TextUtils.ToInt(parts[1]);
                                string warehouseCode = parts[2];

                                BillImportModel model = SQLHelper<BillImportModel>.FindByID(billImportID);
                                if (model != null)
                                {
                                    frmBillImportDetail frm = new frmBillImportDetail();
                                    frm.billImport = model;
                                    frm.IDDetail = billImportID;
                                    frm.WarehouseCode = warehouseCode;
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        this.frmBillExportDetail_Load(null, null);
                                    }
                                }
                            }
                        };
                    }
                    // Thêm vào panel
                    stackPanel2.Controls.Add(linkLabel);
                }
            }
        }
        #endregion

        private void chkTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTransfer.Checked)
            {
                cboWarehouseTransfer.Enabled = true;
            }
            else
            {
                cboWarehouseTransfer.Enabled = false;
            }
        }
    }
}
