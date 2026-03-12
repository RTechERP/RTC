using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ExcelDataReader;
using Forms.Classes;
using Forms.Sale;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;
using DocumentFormat.OpenXml.Office2010.Excel;

using DevExpress.Utils;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using BMS.UP;

namespace BMS
{
    public partial class frmBillImportDetail : _Forms
    {
        public bool BillType;

        #region Variables
        public int IDDetail;
        public BillImportModel billImport = new BillImportModel();
        DataTable dtProductGroup;
        DataTable dtProduct = new DataTable();
        DataTable dtProject = new DataTable();
        ArrayList lstIDDelete = new ArrayList();

        List<int> rowIndex = new List<int>();

        public string WarehouseCode;
        public int warehouseID;

        public DataTable _dataHistory = new DataTable(); //TODO 26/10/2023: QUANG UPDATE NGÀY 26/10/2023 ĐỂ ĐẨY DỮ LIỆU TẠO PHIẾU TRẢ TỪ LỊCH SỬ TRẢ PHÒNG SALE
        public int groupID;

        //Email content
        public string subject = "";
        public string body = "";
        public int receiverMailID = 0;


        public string POCode = "";
        public DataRow[] dtDetails;

        public string idPONCCText = "";
        public int poNCCId = 0;

        // ==========================   Lee Min Khooi Update  ======================================
        private List<InvoiceDTO> listInvoice = new List<InvoiceDTO>();

        private int idMapping = 0;
        // ========================== End Lee Min Khooi Update  ======================================
        #endregion

        public frmBillImportDetail()
        {
            InitializeComponent();
        }

        private void frmBillImportDetail_Load(object sender, EventArgs e)
        {
            grdData.ContextMenuStrip = contextMenuStrip2;
            this.Text += " - " + WarehouseCode;


            if (txtBilllNumber.Text == "")
            {
                loadBilllNumber();
            }

            LoadStatusPur(); //16725
            LoadStatus();//16725
            loadProductGroup();
            loadKhoType();
            LoadSupplier();
            loadUser();
            loadProject();
            LoadBillType();
            loadWarehouse();
            LoadDocumentImport();
            LoadRulePay();

            loadBillImportDetail();

            this.cbProduct.EditValueChanged += new EventHandler(cbProduct_EditValueChanged);

            bool isSave = !TextUtils.ToBoolean(billImport.Status);
            if (Global.IsAdmin) isSave = true;
            btnColseAndSave.Enabled = btnSaveNew.Enabled = btnNewProduct.Enabled = isSave;

            if (_dataHistory.Rows.Count > 0) //TODO 26/10/2023 QUANG UPDATE NGÀY 26/10/2023
            {
                btnSaveNew.Enabled = btnNewProduct.Enabled = btnProject.Enabled = btnImport.Enabled = btnPONCC.Enabled = false;
                cbKhoType.Enabled = false;
                //cboStatus.SelectedIndex = 1;
                //cboStatus.Enabled = false;
            }


            LogActions();
        }

        #region Methods


        void LoadRulePay()
        {
            List<RulePayModel> list = SQLHelper<RulePayModel>.FindAll();
            //cboRulePay1.Properties.ValueMember = "ID";
            //cboRulePay1.Properties.DisplayMember = "Note";
            //cboRulePay1.Properties.DataSource = list;

            cboRulePay.Properties.ValueMember = "ID";
            cboRulePay.Properties.DisplayMember = "Note";
            cboRulePay.Properties.DataSource = list;
        }

        private void loadWarehouse()
        {
            List<WarehouseModel> listWarehouse = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouse.Properties.DataSource = listWarehouse;
            cboWarehouse.Properties.DisplayMember = "WarehouseName";
            cboWarehouse.Properties.ValueMember = "ID";
            if (warehouseID > 0)
            {
                cboWarehouse.Enabled = true;
                cboWarehouse.EditValue = warehouseID;
            }
            else
            {
                cboWarehouse.Enabled = false;
                var warehouseModel = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode).FirstOrDefault();
                cboWarehouse.EditValue = warehouseModel == null ? 0 : warehouseModel.ID;
            }


            cboWarehouse.Enabled = Global.IsAdmin;

            //if (billImport.ID > 0) return;
            ////cboReciver.EditValue = Global.UserID;//Phan Thị Thu Thuỷ

            ////string warehouseSelected = cboWarehouse.Text;

            ////cboReciver.EditValue = 88;//Phan Thị Thu Thuỷ

            //if (!WarehouseCode.Contains("HCM") && string.IsNullOrEmpty(POCode.Trim())) cboReciver.EditValue = Global.UserID;
            //else if (WarehouseCode.Contains("HCM")) cboReciver.EditValue = 88;//Phan Thị Thu Thuỷ


            //if (WarehouseCode.Contains("HN"))
            //{
            //    if (TextUtils.ToInt(cbKhoType.EditValue) == 13) cboDeliver.EditValue = 8;//Nguyễn Thị Ánh Tuyết
            //    else if (TextUtils.ToInt(cbKhoType.EditValue) == 14) cboDeliver.EditValue = 6;//Nguyễn Thị Hằng                    
            //    //cboReciver.EditValue = Global.UserID;//Lê Minh Thuỳ
            //}
        }

        private void LoadDocumentImport()
        {
            //DataTable data = TextUtils.LoadDataFromSP("spGetAllDocumentImportByPONCCID", "A",
            //                                        new string[] { "@PONCCID", "@BillImportID" },
            //                                        new object[] { poNCCId, billImport.ID });

            DataTable data = TextUtils.LoadDataFromSP("spGetAllDocumentImportPONCC", "A",
                                                    new string[] { "@BillImportID" },
                                                    new object[] { billImport.ID });
            grdDocImport.DataSource = data;
        }

        /// <summary>
        /// load group
        /// </summary>
        void loadProductGroup()
        {
            dtProductGroup = TextUtils.Select("SELECT * FROM ProductGroup where IsVisible = 1 ");
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            cboGroup.Properties.DataSource = dtProductGroup;
        }

        void LoadBillType()
        {
            List<object> list = new List<object>()
            {
                new {ID = 0, Name = "Phiếu nhập kho"},
                new {ID = 1, Name = "Phiếu trả"},
                //new {ID = 2, Name = "PTNB"},
                new {ID = 3, Name = "Phiếu mượn NCC"},
                new {ID = 4, Name = "Yêu cầu nhập kho"}
            };
            cboBillTypeNew.Properties.DataSource = list;
            cboBillTypeNew.Properties.ValueMember = "ID";
            cboBillTypeNew.Properties.DisplayMember = "Name";

            //cboBillTypeNew.EditValue = 0;
        }

        /// <summary>
        /// load kho type
        /// </summary>
        void loadKhoType()
        {
            //DataTable dtKhoType;
            //dtKhoType = dtProductGroup.Copy();

            var productGroups = SQLHelper<ProductGroupModel>.FindAll().Where(x => x.IsVisible == true).ToList();
            //if (WarehouseCode == "DP") productGroups = productGroups.Where(x => x.ID == 13).ToList(); //Chỉ lấy kho dự án

            cbKhoType.Properties.DisplayMember = "ProductGroupName";
            cbKhoType.Properties.ValueMember = "ID";
            cbKhoType.Properties.DataSource = productGroups;
        }

        /// <summary>
        /// load nhà cung cấp
        /// </summary>
        void LoadSupplier()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM SupplierSale ");

            var list = SQLHelper<SupplierSaleModel>.FindAll();
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = list;
        }

        /// <summary>
        /// load người nhập, người giao
        /// </summary>
        void loadUser()
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.Select("SELECT * FROM Users");


            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });
            cboReciver.Properties.DisplayMember = cboDeliver.Properties.DisplayMember = "FullName";
            cboReciver.Properties.ValueMember = cboDeliver.Properties.ValueMember = "ID";
            cboReciver.Properties.DataSource = cboDeliver.Properties.DataSource = dt;
            cboDeliver.EditValue = Global.UserID;

            //if (cboReciver.EditValue == null) cboReciver.EditValue = 88;//Phan Thị Thu Thuỷ

        }

        /// <summary>
        /// lựa chọn mã dự án
        /// </summary>
        private void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,ProjectName FROM Project");
            cbProject.DisplayMember = "ProjectCode";
            cbProject.ValueMember = "ID";
            cbProject.DataSource = dtProject;
            colProjectID.ColumnEdit = cbProject;
        }


        string[] preBillCodes = new string[]
        {
            "PNK",//0
            "PT",//1
            "PTNB",//2
            "PNM",//3
            "PNK",//4
        };
        /// <summary>
        /// load số phiếu
        /// </summary>
        void loadBilllNumber()
        {
            //if (billImport.ID > 0 && !Global.IsAdmin) return;
            //int billtype = TextUtils.ToInt(cboBillTypeNew.EditValue);
            //txtBilllNumber.Text = TextUtils.GetBillCode("BillImport", billtype);
            //return;

            int billtype = TextUtils.ToInt(cboBillTypeNew.EditValue);
            if (billImport.ID > 0)
            {
                string preCodeNew = preBillCodes[billtype];
                string preCodeOld = preBillCodes[TextUtils.ToInt(billImport.BillTypeNew)];

                string billlNumber = billImport.BillImportCode.Replace(preCodeOld.ToString(), preCodeNew.ToString());
                txtBilllNumber.Text = billlNumber;
            }
            else
            {
                //if (!Global.DebugFlag)
                {
                    txtBilllNumber.Text = TextUtils.GetBillCode("BillImport", billtype);
                    return;
                }
            }

            return;

            int code = 0;

            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;
            string billCode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 BillImportCode FROM BillImport where month(CreatedDate) ={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate) ={DateTime.Now.Day} ORDER BY ID DESC"));

            if (billCode.Contains("PT"))
            {
                billCode = billCode.Substring(2);
            }
            else if (billCode.Contains("PNK") || billCode.Contains("PNM"))
            {

                billCode = billCode.Substring(3);
            }
            else if (billCode.Contains("PTNB"))
            {
                billCode = billCode.Substring(4);
            }

            if (billImport.ID == 0)
            {
                if (billCode == "")
                {
                    txtBilllNumber.Text = status + date + "001";
                    return;
                }
                else
                {
                    code = TextUtils.ToInt(billCode.Substring(billCode.Length - 3));
                }


                //if (code == 0)
                //{
                //    if (cboStatus.SelectedIndex == 0)
                //    {
                //        txtBilllNumber.Text = "PNK" + date + "001";
                //    }
                //    else if (cboStatus.SelectedIndex == 1)
                //    {
                //        txtBilllNumber.Text = "PT" + date + "001";
                //    }
                //    else
                //    {
                //        txtBilllNumber.Text = "PTNB" + date + "001";
                //    }
                //    return;
                //}
                //else
                //{
                //    string dem = TextUtils.ToString(code + 1);
                //    for (int i = 0; dem.Length < 3; i++)
                //    {
                //        dem = "0" + dem;
                //    }

                //    if (cboStatus.SelectedIndex == 0)
                //    {
                //        txtBilllNumber.Text = "PNK" + date + TextUtils.ToString(dem);
                //    }
                //    else if (cboStatus.SelectedIndex == 1)
                //    {
                //        txtBilllNumber.Text = "PT" + date + TextUtils.ToString(dem);
                //    }
                //    else
                //    {
                //        txtBilllNumber.Text = "PTNB" + date + TextUtils.ToString(dem);
                //    }
                //}
                int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);
                if (code == 0)
                {

                    if (billType == 0 || billType == 4)
                    {
                        txtBilllNumber.Text = "PNK" + date + "001";
                    }
                    else if (billType == 1)
                    {
                        txtBilllNumber.Text = "PT" + date + "001";
                    }
                    else if (billType == 3)
                    {
                        txtBilllNumber.Text = "PNM" + date + "001";
                    }
                    else
                    {
                        txtBilllNumber.Text = "PTNB" + date + "001";
                    }
                    return;
                }
                else
                {
                    string dem = TextUtils.ToString(code + 1);
                    for (int i = 0; dem.Length < 3; i++)
                    {
                        dem = "0" + dem;
                    }

                    if (billType == 0 || billType == 4)
                    {
                        txtBilllNumber.Text = "PNK" + date + TextUtils.ToString(dem);
                    }
                    else if (billType == 1)
                    {
                        txtBilllNumber.Text = "PT" + date + TextUtils.ToString(dem);
                    }
                    else if (billType == 3)
                    {
                        txtBilllNumber.Text = "PNM" + date + TextUtils.ToString(dem);
                    }
                    else
                    {
                        txtBilllNumber.Text = "PTNB" + date + TextUtils.ToString(dem);
                    }
                }
            }

        }

        /// <summary>
        /// load mã sản phẩm, tên sp
        /// </summary>
        void loadProduct()
        {
            if (cboGroup.Text == "") return;
            string ID = TextUtils.ToString(cboGroup.EditValue);
            //dtProduct = TextUtils.Select($"SELECT ID,ProductCode,ProductName,ItemType,Unit,AddressBox,NumberInStoreCuoiKy,Note,ProductNewCode FROM ProductSale where ProductGroupID IN ({ID})");

            dtProduct = TextUtils.LoadDataFromSP(StoreProcedure.spGetProductImportSale, "A",
                                    new string[] { "@GroupProductID", "@WarehouseCode" },
                                    new object[] { ID, WarehouseCode });


            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
            //colProductID.ColumnEdit = cbProduct;
        }

        /// <summary>
        /// load Data
        /// </summary>
        //void loadBillImportDetail()
        //{
        //    //cboBillTypeNew.EditValue = _dataHistory ;
        //    //if (_dataHistory == null || _dataHistory.Rows.Count <= 0)
        //    //{
        //    //    cboBillTypeNew.EditValue = 0;
        //    //}

        //    cboReciver.EditValue = billImport.ReciverID;
        //    cboDeliver.EditValue = billImport.DeliverID;
        //    cboSupplier.EditValue = billImport.SupplierID;
        //    cbKhoType.EditValue = billImport.KhoTypeID;
        //    cboBillTypeNew.EditValue = billImport.BillTypeNew;

        //    //loadBilllNumber();
        //    if (billImport.ID > 0)
        //    {
        //        grvData.Focus();
        //        txtBilllNumber.Focus();
        //        cboGroup.SetEditValue(billImport.GroupID);
        //        txtBilllNumber.Text = billImport.BillImportCode;
        //        //cbKhoType.Text = billImport.KhoType;
        //        //cbkType.Checked = billImport.BillType;

        //        if (billImport.BillTypeNew == 4)
        //        {
        //            dtpDateRequestImport.Visible = true;
        //            lblRequestDate.Visible = true;
        //            dtpCreatDate.Enabled = false;
        //            dtpCreatDate.EditValue = null;
        //            if (!billImport.DateRequestImport.HasValue)
        //            {
        //                dtpDateRequestImport.Value = DateTime.Now;
        //            }
        //            else
        //            {
        //                dtpDateRequestImport.Value = (DateTime)billImport.DateRequestImport;
        //            }

        //        }
        //        else
        //        {
        //            dtpCreatDate.Enabled = true;
        //            dtpDateRequestImport.Visible = false;
        //            lblRequestDate.Visible = false;
        //            if (billImport.CreatDate == null)
        //            {
        //                dtpCreatDate.EditValue = DateTime.Now;
        //            }
        //            else
        //            {
        //                dtpCreatDate.EditValue = (DateTime)billImport.CreatDate;
        //            }

        //        }
        //        //cboReciver.EditValue = billImport.ReciverID;
        //        //cboDeliver.EditValue = billImport.DeliverID;
        //        //cboSupplier.EditValue = billImport.SupplierID;
        //        //cbKhoType.EditValue = billImport.KhoTypeID;
        //        //billImport.BillType = BillType;

        //        //if (billImport.BillType == true)
        //        //{
        //        //    cboStatus.SelectedIndex = 1;
        //        //}
        //        //else
        //        //{
        //        //    cboStatus.SelectedIndex = 0;
        //        //}

        //        cboBillTypeNew.EditValue = billImport.BillTypeNew;

        //        if (billImport.BillImportCode.StartsWith("T") == true)
        //        {
        //            lbSupplier.Text = "Bộ phận";
        //            lbUser.Text = "Người trả";
        //        }


        //        int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);

        //        if (billType == 0 || billType == 4)
        //        {
        //            status = "PNK";
        //            lbSupplier.Text = "Nhà cung cấp";
        //            lbUser.Text = "Người giao";
        //        }
        //        else if (billType == 1)
        //        {
        //            status = "PT";
        //            lbSupplier.Text = "Bộ phận";
        //            lbUser.Text = "Người trả";
        //        }
        //        else if (billType == 3)
        //        {
        //            status = "PNM";
        //            lbSupplier.Text = "Nhà cung cấp";
        //            lbUser.Text = "Người giao";
        //        }
        //        else
        //        {
        //            status = "PTNB";
        //            lbSupplier.Text = "Bộ phận";
        //            lbUser.Text = "Người trả";
        //        }
        //    }

        //    if (_dataHistory.Rows.Count > 0)  //TODO: QUANG UPDATE NGÀY 26/10/2023 KHI SINH RA TỪ LỊCH SỬ MƯỢN
        //    {
        //        cbKhoType.EditValue = groupID;
        //        cboDeliver.EditValue = TextUtils.ToInt(_dataHistory.Rows[0]["UserID"]);

        //        _dataHistory.Columns.Add("SomeBill", typeof(string));
        //        _dataHistory.Columns["BorrowCode"].ColumnName = "CodeMaPhieuMuon";
        //        _dataHistory.Columns["BorrowQty"].ColumnName = "Qty";
        //        colPM.OptionsColumn.ReadOnly = true;
        //        grdData.DataSource = _dataHistory;
        //        for (int i = 0; i < grvData.RowCount; i++)
        //        {
        //            grvData.SetRowCellValue(i, colSTT, i + 1);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(POCode.Trim())) //Tiến Huy update 22/05/2024
        //    {


        //        //cboBillTypeNew.EditValue = 0;
        //        //DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
        //        //grdData.DataSource = dt;
        //        //for(int i = 0; i<grvData.RowCount; i++)
        //        //{
        //        //    grvData.SetRowCellValue(i,colQty, 0);
        //        //}


        //        DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
        //        var dtClone = dt.Clone();
        //        int i = 1;
        //        //NT.Huy update 22/05/2024
        //        if (dtDetails == null)
        //        {
        //            grdData.DataSource = dtClone;
        //            return;
        //        }
        //        if (dtDetails.Length <= 0)
        //        {
        //            grdData.DataSource = dtClone;
        //            return;
        //        }
        //        foreach (var row in dtDetails)
        //        {
        //            ProjectModel project = new ProjectModel();
        //            ProductSaleModel productSale = new ProductSaleModel();
        //            int projectID = TextUtils.ToInt(row["ProjectID"]);
        //            int productID = TextUtils.ToInt(row["ProductSaleID"]);
        //            if (projectID > 0)
        //            {
        //                project = SQLHelper<ProjectModel>.FindByID(projectID);
        //            }
        //            if (productID > 0)
        //            {
        //                productSale = SQLHelper<ProductSaleModel>.FindByID(productID);
        //            }
        //            DataRow dr = dtClone.NewRow();
        //            dr["STT"] = i;
        //            i++;
        //            // dr["ProductNewCode"] = row[""];
        //            dr["ProductID"] = row["ProductSaleID"];
        //            if (productSale != null)
        //            {
        //                dr["ProductName"] = productSale.ProductName;
        //                dr["ProductNewCode"] = productSale.ProductNewCode;
        //                dr["Unit"] = productSale.Unit;
        //            }
        //            if (project != null)
        //            {
        //                dr["ProjectNameText"] = project.ProjectName;
        //            }
        //            dr["ProjectCode"] = row["ProductCodeOfSupplier"];
        //            dr["SerialNumber"] = "";
        //            dr["QtyRequest"] = row["QuantityRemain"];
        //            dr["Qty"] = row["QuantityRemain"];
        //            dr["SomeBill"] = "";
        //            dr["ProjectID"] = row["ProjectID"];
        //            dr["Note"] = row["POCode"];
        //            dr["PONCCDetailID"] = row["ID"];
        //            dr["CodeMaPhieuMuon"] = "";
        //            dr["Price"] = row["UnitPrice"];
        //            dr["ProjectPartListID"] = row["ProjectPartListID"];
        //            dr["BillCodePO"] = row["BillCode"];
        //            dr["PONCCDetailID"] = row["ID"];

        //            dtClone.Rows.Add(dr);
        //        }
        //        grdData.DataSource = dtClone;


        //    }
        //    else
        //    {
        //        DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
        //        grdData.DataSource = dt;
        //    }

        //}


        #endregion

        #region Buttons Events
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
        /// click button lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //loadBilllNumber();
            try
            {
                if (saveData())
                {
                    if (poNCCId > 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu nhập mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (saveData())
                {
                    statusOld = "";
                    cboSupplier.Text = "";
                    cboReciver.Text = "";
                    cboDeliver.Text = "";
                    cbKhoType.EditValue = "";
                    cbKhoType.Text = "";
                    cboGroup.EditValue = "";
                    for (int i = grvData.RowCount - 1; i >= 0; i--)
                    {
                        grvData.DeleteRow(i);
                    }
                    billImport = new BillImportModel();
                    loadBilllNumber();
                }

            }
        }

        /// <summary>
        /// click button tạo thêm kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewType_Click(object sender, EventArgs e)
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
        /// click button xóa dòng trong grvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        #endregion



        /// <summary>
        /// hàm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.FocusedRowHandle = -1;
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
                DataRow[] rows = dtProduct.Select("ID = " + ID);
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string productNewCode = TextUtils.ToString(rows[0]["ProductNewCode"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    grvData.SetFocusedRowCellValue(colProductName, productName);
                    grvData.SetFocusedRowCellValue(colProductNewCode, productNewCode);
                    //grvData.SetFocusedRowCellValue(colUnit, unit);
                    grvData.SetFocusedRowCellValue(colUnitName, unit);
                    grvData.SetFocusedRowCellValue(colPONCCDetailID, 0);
                }
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// hàm lưu thông tin
        /// </summary>
        bool saveData()
        {
            grvData.FocusedRowHandle = -1;
            RecheckQty();
            cboReciver.Focus();
            grvData.Focus();
            if (!ValidateForm()) return false;
            billImport.BillImportCode = txtBilllNumber.Text.Trim();
            if (dtpCreatDate.EditValue != null)
            {
                billImport.CreatDate = (DateTime)dtpCreatDate.EditValue;
            }

            billImport.Deliver = cboDeliver.Text.Trim();
            billImport.Reciver = cboReciver.Text.Trim();
            billImport.Suplier = cboSupplier.Text.Trim();
            billImport.KhoType = cbKhoType.Text.Trim();
            billImport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
            billImport.ReciverID = TextUtils.ToInt(cboReciver.EditValue);
            billImport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            billImport.GroupID = TextUtils.ToString(cboGroup.EditValue);
            billImport.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            //billImport.WarehouseID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM Warehouse WHERE WarehouseCode = '{WarehouseCode}'"));

            warehouseID = TextUtils.ToInt(cboWarehouse.EditValue);
            if (warehouseID != 0)
            {
                billImport.WarehouseID = warehouseID;
            }
            else
            {
                billImport.WarehouseID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM Warehouse WHERE WarehouseCode = '{WarehouseCode}'"));
            }

            billImport.BillTypeNew = TextUtils.ToInt(cboBillTypeNew.EditValue);
            if (billImport.BillTypeNew == 4)
            {
                billImport.DateRequestImport = dtpDateRequestImport.Value;
            }
            //if (cboStatus.SelectedIndex == 0)
            //{
            //    billImport.BillType = false;
            //}
            //else if (cboStatus.SelectedIndex == 1)
            //{
            //    billImport.BillType = true;
            //}
            //else
            //{
            //    billImport.PTNB = true;
            //}
            //billImport.BillType = cbkType.Checked;
            billImport.UpdatedDate = DateTime.Now;
            billImport.UpdatedBy = Global.AppUserName;
            billImport.Status = false;

            //if (cbkType.Checked == true)
            //{
            //    billImport.BillType = true;
            //}
            //else
            //{
            //    billImport.BillType = false;
            //}

            if (billImport.ID > 0)
            {
                //BillImportBO.Instance.Update(billImport);
                SQLHelper<BillImportModel>.Update(billImport);
            }
            else
            {
                billImport.CreatedDate = DateTime.Now;
                billImport.CreatedBy = Global.AppUserName;

                billImport.BillDocumentImportType = 2; //TODO 13-03-2024 PHÚC UPDATE TRẠNG THÁI (HOÀN THÀNH/ CHƯA HOÀN THÀNH) CỦA CHỨNG TỪ PHIẾU NHẬP
                //billImport.ID = (int)BillImportBO.Instance.Insert(billImport);
                billImport.ID = SQLHelper<BillImportModel>.Insert(billImport).ID;

                // TODO 11-03-2024 PHÚC INSERT BẢNG BillDocumentImport
                TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { billImport.ID, Global.LoginName });

                //Add Notify
                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequestByBillImportID", "A", new string[] { "@BillImportID" }, new object[] { billImport.ID });
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string text = $"Kho: Sale\n" +
                                        $"Mã sản phẩm: {TextUtils.ToString(row["ProductCode"])}\n" +
                                        $"Tên sản phần: {TextUtils.ToString(row["ProductName"])}";
                        TextUtils.AddNotify("THÔNG BÁO HÀNG VỀ", text, TextUtils.ToInt(row["EmployeeID"]));
                    }
                }
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                long ID = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                BillImportDetailModel detail = new BillImportDetailModel();

                if (ID > 0)
                {
                    //detail = (BillImportDetailModel)(BillImportDetailBO.Instance.FindByPK(ID));
                    detail = SQLHelper<BillImportDetailModel>.FindByID(ID);
                }

                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.BillImportID = billImport.ID; //billImport.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                detail.Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice));
                detail.ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode));

                //detail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectName));

                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty));
                detail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                detail.PONCCDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPONCCDetailID));
                detail.SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber));
                //lưu thêm trường Mã phiếu kìa vào đấy --CodeMaPhieuMuon
                detail.CodeMaPhieuMuon = TextUtils.ToString(grvData.GetRowCellValue(i, colPM));

                //TODO 26/10/2023: LIÊN KẾT VỚI BẢNG PHIẾU XUẤT (MƯỢN)
                detail.BillExportDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colBorrowID));

                detail.QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyRequest));
                detail.BillCodePO = TextUtils.ToString(grvData.GetRowCellValue(i, colBillCodePO));

                detail.ProjectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartListID));
                detail.IsKeepProject = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsKeepProject));
                detail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText));
                detail.UnitName = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));


                if (Global.UserID == billImport.DeliverID || Global.IsAdmin)
                {
                    DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(i, colDateSomeBill));
                    detail.DateSomeBill = dateSomeBill;
                    detail.SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill));
                }

                detail.DPO = TextUtils.ToInt(grvData.GetRowCellValue(i, colDPO));
                detail.DueDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDueDate));
                detail.TaxReduction = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxReduction));
                detail.COFormE = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCOFormE));
                detail.IsNotKeep = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsNotKeep));
                if (detail.ProductID <= 0) continue;



                //Add vào kho giữ
                //UpdateInventoryProject(detail, i);
                UpdateInventoryProjectNew(detail, i);


                if (detail.ID > 0)
                {
                    //BillImportDetailBO.Instance.Update(detail);
                    SQLHelper<BillImportDetailModel>.Update(detail);
                }
                else
                {
                    //detail.ID = (int)BillImportDetailBO.Instance.Insert(detail);
                    detail.ID = SQLHelper<BillImportDetailModel>.Insert(detail).ID;
                    grvData.SetRowCellValue(i, colID, detail.ID);
                }


                //================================================ Lee Min Khooi Update =====================================================
                ////Lưu bảng n-n Hóa đơn và BillImportDetails
                //int idMap = TextUtils.ToInt(grvData.GetRowCellValue(i, colIdMapping));
                //List<InvoiceDTO> lst = listInvoice.Where(p => p.IdMapping == idMap).ToList();
                //if (lst.Count > 0)
                //{
                //    //Xóa bản ghi cũ
                //    SQLHelper<InvoiceLinkModel>.DeleteByAttribute("BillImportDetailID", detail.ID);
                //    foreach (InvoiceDTO item in lst)
                //    {
                //        foreach (InvoiceLinkModel model in item.Details)
                //        {
                //            model.BillImportDetailID = detail.ID;
                //            //InvoiceBO.Instance.Insert(model);
                //            SQLHelper<InvoiceLinkModel>.Insert(model);
                //        }
                //    }
                //}
                //========================================================================================

                //check xem có trong Inventory chưa, nếu chưa có thì thêm mới   
                UpdateInventory(TextUtils.ToInt(detail.ProductID));


                //PQ.Chien - ADD- 10/03/2025====================================================================================

                //bool isStock = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsStock));
                //if (detail.ProductID > 0 && isStock && billImport.BillTypeNew != 4)
                //{
                //    decimal quantity = (decimal)detail.Qty;
                //    var exp1 = new Expression("ProductSaleID", detail.ProductID);
                //    var exp2 = new Expression("WarehouseID", warehouseID);
                //    InventoryModel inventory = SQLHelper<InventoryModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new InventoryModel();
                //    if (inventory.ID > 0)
                //    {
                //        var exp3 = new Expression("InventoryID", inventory.ID);
                //        InventoryStockModel inventoryStock = SQLHelper<InventoryStockModel>.FindByExpression(exp3).FirstOrDefault();
                //        if (inventoryStock == null)
                //        {
                //            InventoryStockModel inventoryStock1 = new InventoryStockModel();
                //            inventoryStock1.InventoryID = inventory.ID;
                //            inventoryStock1.Quantity = TextUtils.ToDecimal(detail.Qty);
                //            SQLHelper<InventoryStockModel>.Insert(inventoryStock1);

                //        }
                //        else
                //        {
                //            SQLHelper<InventoryStockModel>.Update(inventoryStock);
                //        }

                //    }
                //    else
                //    {
                //        inventory.MinQuantity = (decimal)detail.Qty;
                //        inventory.IsStock = (decimal)detail.Qty > 0;
                //        SQLHelper<InventoryModel>.Update(inventory);
                //    }

                //}



                //Add notify
                if (billImport.BillTypeNew == 4)
                {
                    string productCode = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colProductID));
                    string productName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                    string text = $"Mã sản phầm: {productCode}\n" +
                                    $"Tên sản phẩm: {productName}\n";
                    //TextUtils.AddNotify("THÔNG BÁO HÀNG VỀ - SALE", text, detail.EmployeeIDBorrow);
                }
            }

            if (lstIDDelete.Count > 0)
            {
                //BillImportDetailBO.Instance.Delete(lstIDDelete);

                var myDict = new Dictionary<string, object>()
                {
                    { BillImportDetailModel_Enum.IsDeleted.ToString(),true},
                    { BillImportDetailModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    { BillImportDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                var exp = new Expression(BillImportDetailModel_Enum.ID, string.Join(",", lstIDDelete.ToArray()), "IN");
                SQLHelper<BillImportDetailModel>.UpdateFields(myDict, exp);

                for (int j = 0; j < lstIDDelete.Count; j++)
                {
                    int IdBillImportDetail = TextUtils.ToInt(lstIDDelete[j]);
                    BillImportDetailSerialNumberBO.Instance.DeleteByAttribute("BillImportDetailID", IdBillImportDetail);
                }
            }

            //QUANG UPDATE 31/10/2023 KIỂM TRA VÀ UPDATE LẠI TRẠNG THÁI ĐÃ TRẢ CHO PHIẾU MƯỢN
            TextUtils.ExcuteScalar("spUpdateReturnedStatusForBillExportDetail", new string[] { "@BillImportID", "@Approved" }, new object[] { billImport.ID, 0 });

            //Update trạng thái PO NCC
            //TextUtils.ExcuteProcedure("spUpdateStatusPONCC", new string[] { "@BillImportID", "@UpdatedBy" }, new object[] { billImport.ID, Global.LoginName });
            //TextUtils.ExcuteProcedure("spUpdateStatusPONCC", new string[] { "@BillImportID", "@UpdatedBy", "@WarehouseType" }, new object[] { billImport.ID, Global.LoginName, 1 });
            var listDetails = SQLHelper<BillImportDetailModel>.FindByAttribute("BillImportID", billImport.ID);
            string poNCCDetailID = string.Join(",", listDetails.Select(x => x.PONCCDetailID));
            TextUtils.ExcuteProcedure("spUpdateStatusPONCC",
                                        new string[] { "@PONCCDetailID", "@UpdatedBy" },
                                        new object[] { poNCCDetailID, Global.LoginName });


            //load noi dung email Tiến Huy update 22/05/2024
            LoadEmailContent();


            if (/*!string.IsNullOrEmpty(idPONCCText)*/ poNCCId > 0)
            {
                //string sql = $"UPDATE dbo.PONCC SET Status = 5 WHERE ID IN({idPONCCText})";

                PONCCModel po = SQLHelper<PONCCModel>.FindByID(poNCCId);
                po.Status = 5;
                SQLHelper<PONCCModel>.Update(po);
                //TextUtils.ExcuteSQL(sql);
            }

            //UpdateDocument(poNCCId);
            UpdateDocumentNew(billImport.ID);


            return true;
        }

        void UpdateInventoryProject(BillImportDetailModel detail, int i)
        {
            //bool isNotKeep = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsNotKeep));

            if (detail.IsNotKeep == true)  //Nếu tích không giữ
            {
                //var myDict = new Dictionary<string, object>()
                //{
                //    {InventoryProjectModel_Enum.IsDeleted.ToString(), detail.IsNotKeep},
                //    {InventoryProjectModel_Enum.UpdatedBy.ToString(), Global.AppUserName},
                //    {InventoryProjectModel_Enum.UpdatedDate.ToString(), DateTime.Now},
                //};

                //SQLHelper<InventoryProjectModel>.UpdateFieldsByID(myDict, TextUtils.ToInt(detail.InventoryProjectID));

                return;
            }

            if (billImport.BillTypeNew != 0) return; //nếu là nhập kho

            string projectNameText = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText));
            int pokhDetailId = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));

            if (detail.ProjectID <= 0 && pokhDetailId <= 0) return; //Nếu không có dự án và POKH Detail

            //if (detail.ProjectID > 0 || pokhDetailId > 0)
            //{
            decimal quantityRealy = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
            decimal quantityRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantityRequestBuy));

            decimal quantityKeep = quantityRealy;
            //if (quantityRealy > 0 && quantityRequest > 0) quantityKeep = quantityRealy > quantityRequest ? quantityRequest : quantityRealy;
            if (quantityRealy > 0 && quantityRequest > 0) quantityKeep = Math.Min(quantityRealy, quantityRequest);

            //quantityKeep = 3;
            //Check tự động bù lại SL đã nhả giữ (lt.anh update 13/09/2025)

            quantityKeep = UpdateReturnQuantityLoan(pokhDetailId, quantityKeep);
            //End update

            int inventoryProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colInventoryProjectID));
            InventoryProjectModel inventoryProject = SQLHelper<InventoryProjectModel>.FindByID(inventoryProjectID);
            inventoryProject.ProjectID = detail.ProjectID;
            //inventoryProject.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectIDKeep));
            inventoryProject.ProductSaleID = detail.ProductID;
            inventoryProject.WarehouseID = billImport.WarehouseID;
            //inventoryProject.Quantity = detail.Qty;
            //inventoryProject.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colProjectPartListQuantity));
            //inventoryProject.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKH));
            inventoryProject.Note = projectNameText;

            // VTN update 17725
            inventoryProject.EmployeeID = TextUtils.ToInt(cboDeliver.EditValue);

            inventoryProject.Quantity = quantityKeep;
            inventoryProject.QuantityOrigin = inventoryProject.Quantity;
            //if (quantityRealy > 0 && quantityRequest > 0)
            //{
            //    inventoryProject.Quantity = quantityRealy > quantityRequest ? quantityRequest : quantityRealy;
            //}
            inventoryProject.Note = detail.Note;
            inventoryProject.POKHDetailID = pokhDetailId;
            inventoryProject.CustomerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCustomerID));
            // VTN end update

            if (inventoryProject.ID <= 0 && inventoryProject.Quantity > 0)
            {
                inventoryProject.EmployeeID = Global.EmployeeID;
                inventoryProject.ID = SQLHelper<InventoryProjectModel>.Insert(inventoryProject).ID;
            }
            else
            {
                inventoryProject.IsDeleted = inventoryProject.Quantity <= 0;
                SQLHelper<InventoryProjectModel>.Update(inventoryProject);
            }

            detail.InventoryProjectID = inventoryProject.ID;
            //}
        }


        decimal UpdateReturnQuantityLoan(int pokhDetailId, decimal quantityKeep)
        {
            var exp1 = new Expression(InventoryProjectModel_Enum.POKHDetailID, pokhDetailId);
            var exp2 = new Expression(InventoryProjectModel_Enum.IsDeleted, 0);
            var exp3 = new Expression(InventoryProjectModel_Enum.ParentID, 0, ">");
            var inventoryProjects = SQLHelper<InventoryProjectModel>.FindByExpression(exp1.And(exp2).And(exp3));

            foreach (var item in inventoryProjects)
            {
                if (quantityKeep <= 0) continue;
                InventoryProjectModel inventoryProjectParent = SQLHelper<InventoryProjectModel>.FindByID(TextUtils.ToInt(item.ParentID));
                if (inventoryProjectParent.ID <= 0) continue;

                decimal quantityLoan = TextUtils.ToDecimal(inventoryProjectParent.QuantityOrigin) - TextUtils.ToDecimal(inventoryProjectParent.Quantity);
                if (quantityLoan <= 0) continue;
                inventoryProjectParent.Quantity += Math.Min(quantityLoan, quantityKeep);

                SQLHelper<InventoryProjectModel>.Update(inventoryProjectParent);
                quantityKeep -= quantityLoan;
            }

            decimal totalQuantityLoan = inventoryProjects.Sum(x => TextUtils.ToDecimal(x.Quantity));
            quantityKeep -= totalQuantityLoan;

            return quantityKeep;
        }
        void UpdateDocument(int poID)
        {
            try
            {
                grvDocImport.FocusedRowHandle = -1;

                if (poID <= 0) return;
                Expression x1 = new Expression("PONCCID", poID);
                Expression x2 = new Expression("Status", 0);
                List<DocumentImportPONCCModel> listDocument = SQLHelper<DocumentImportPONCCModel>.FindByExpression(x1.And(x2));
                if (listDocument.Count > 0) SQLHelper<DocumentImportPONCCModel>.DeleteListModel(listDocument);

                for (int i = 0; i < grvDocImport.RowCount; i++)
                {
                    int DocId = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocID));

                    Expression x3 = new Expression("DocumentImportID", DocId);
                    DocumentImportPONCCModel model = SQLHelper<DocumentImportPONCCModel>.FindByExpression(x1.And(x3)).FirstOrDefault() ?? new DocumentImportPONCCModel();

                    bool isCheck = TextUtils.ToBoolean(grvDocImport.GetRowCellValue(i, colIsChecked));
                    if (model.Status > 0) continue;
                    if (isCheck)
                    {
                        model.PONCCID = poID;
                        model.DocumentImportID = DocId;

                        //  16725
                        model.DateRecive = DateTime.Now;
                        model.Note = TextUtils.ToString(grvDocImport.GetRowCellValue(i, colDocumentNote));
                        model.ReasonCancel = TextUtils.ToString(grvDocImport.GetRowCellValue(i, colResionCancle));
                        model.StatusPurchase = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocumentStatusPur));
                        model.UpdatedBy = Global.AppFullName;
                        model.UpdatedDate = DateTime.Now;

                        //if (model.ID > 0) DocumentImportPONCCBO.Instance.Update(model);
                        DocumentImportPONCCBO.Instance.Insert(model);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        void UpdateDocumentNew(int billImportID) //VTNam update 21/07/2025
        {
            try
            {
                for (int i = 0; i < grvDocImport.RowCount; i++)
                {

                    int id = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, "ID"));

                    DocumentImportPONCCModel document = SQLHelper<DocumentImportPONCCModel>.FindByID(id);
                    document.DocumentImportID = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocumentImportID));
                    document.ReasonCancel = TextUtils.ToString(grvDocImport.GetRowCellValue(i, colResionCancle)).Trim();
                    document.Note = TextUtils.ToString(grvDocImport.GetRowCellValue(i, colDocumentNote)).Trim();
                    //document.DateRecive = DateTime.Now;
                    //document.EmployeeReciveID = Global.EmployeeID;
                    document.BillImportID = billImportID;


                    document.StatusPurchase = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocumentStatusPur));
                    //document.UpdatedBy = Global.AppUserName;
                    //document.UpdatedDate = DateTime.Now;

                    if (document.ID <= 0)
                    {
                        SQLHelper<DocumentImportPONCCModel>.Insert(document);
                    }
                    else
                    {
                        SQLHelper<DocumentImportPONCCModel>.Update(document);
                    }


                    BillDocumentImportLogModel log = new BillDocumentImportLogModel();
                    log.BillDocumentImportID = document.ID;
                    log.DocumentStatus = TextUtils.ToInt(document.Status);
                    log.LogDate = document.DateRecive;
                    log.Note = $"LÝ DO HUỶ: {document.ReasonCancel}\nGHI CHÚ: {document.Note}";
                    log.DocumentImportID = document.DocumentImportID;
                    SQLHelper<BillDocumentImportLogModel>.Insert(log);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        public void LoadEmailContent()
        {
            subject = $"Thông báo hàng về {txtBilllNumber.Text.Trim()}".ToUpper();
            int reciver = TextUtils.ToInt(cboReciver.EditValue);

            EmployeeModel receiverMail = SQLHelper<EmployeeModel>.FindByAttribute("UserID", reciver).FirstOrDefault();
            if (receiverMail == null) return;
            receiverMailID = receiverMail.ID;
            string tbody = "";
            for (int i = 0; i < grvData.RowCount; i++)
            {
                tbody += $"<tr style='border:1px solid black'>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellDisplayText(i, colProjectID)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{POCode}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellDisplayText(i, colProductID)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colProductName)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colProductNewCode)}</td>" +
                         //$"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colUnit)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colUnitName)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px; text-align: right;\">{grvData.GetRowCellValue(i, colQtyRequest)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{cbKhoType.Text}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{cboSupplier.Text}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;text-align: center;\">{dtpDateRequestImport.Value.ToString("dd/MM/yyyy")}</td>" +
                         $"</tr>";

            }
            body = $"<div>Dear anh/chị {receiverMail.FullName},<br>" +
                  $"Thông tin chi tiết hàng về: {txtBilllNumber.Text.Trim()} - {dtpDateRequestImport.Value.ToString("dd/MM/yyyy")}<br><br>" +
                  $"<table style=\"border-collapse: collapse;border: 1px solid; table-layout: auto; width: 100%;\"> " +
                  $"<thead>" +
                  $"<tr style='border:1px solid black'>" +
                  $"<th style=\"border: 1px solid;\"> Dự án </th>" +
                  $"<th style=\"border: 1px solid;\"> Số PO </th>" +
                  $"<th style=\"border: 1px solid;\"> Mã hàng </th>" +
                  $"<th style=\"border: 1px solid;\"> Tên hàng </th>" +
                  $"<th style=\"border: 1px solid;\"> Mã nội bộ </th>" +
                  $"<th style=\"border: 1px solid;\"> Đơn vị tính</th>" +
                  $"<th style=\"border: 1px solid;\"> SL yêu cầu</th>" +
                  $"<th style=\"border: 1px solid;\"> Loại kho</th>" +
                  $"<th style=\"border: 1px solid;\"> Nhà cung cấp</th>" +
                  $"<th style=\"border: 1px solid;\"> Ngày yêu cầu nhập</th>" +
                  $"</tr>" +
                  $"</thead> " +
                  $"<tbody>" + tbody +
                  $" </tbody>" +
                  $"</table> " +
                  $"<br>" +
                  $"Trân trọng,<br>" +
                  $"{Global.AppFullName}." +
                  $"</div>";

        }


        void AddNotify(int poNCCDetailID)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequestByPONCCDetailID", "A",
                                                    new string[] { "@PONCCDetailID" }, new object[] { poNCCDetailID });

            if (dt.Rows.Count > 0)
            {

            }
        }

        /// <summary>
        /// Load lại số lượng
        /// </summary>
        void RecheckQty()
        {
            for (int k = 0; k < grvData.RowCount; k++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(k, colProductID));
                float sum = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
                        sum += qty;
                    }
                }
                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQty, sum);
                    }
                }
            }
        }
        /// <summary>
        /// hàm ckeck sản phẩm đã điền đủ hay không
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            DataTable dt;
            if (billImport.ID > 0)
            {
                //string billCode = txtBilllNumber.Text.Trim();
                //if (billCode.Contains("PT"))
                //{
                //    billCode = billCode.Substring(2);
                //}
                //else if (billCode.Contains("PNK"))
                //{
                //    billCode = billCode.Substring(3);
                //}
                //int strID = billImport.ID;
                //dt = TextUtils.Select($"select top 1 ID from BillImport where BillImportCode LIKE '%{billCode}%' and ID <> {strID}");
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("Số phiếu này đã tồn tại.\nVui lòng Load lại Số phiếu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
            }
            else
            {
                if (billImport.ID == 0)
                {
                    dt = TextUtils.Select("select top 1 ID from BillImport where BillImportCode = '" + txtBilllNumber.Text.Trim() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        loadBilllNumber();
                        MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtBilllNumber.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            if (txtBilllNumber.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboSupplier.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền thông tin nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cboSupplier.Focus();
                return false;
            }
            if (cboReciver.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền thông tin người nhập.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cbKhoType.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn kho quản lý.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //if (cboGroup.Text == "")
            //{
            //    MessageBox.Show("Xin hãy chọn nhóm.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            if (cboDeliver.Text == "")
            {
                MessageBox.Show("Xin hãy điền thông tin người giao.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);
            DateTime? creatDateVN = TextUtils.ToDate4(dtpCreatDate.EditValue);
            DateTime? creatDateUS = TextUtils.ToDate2(dtpCreatDate.EditValue);

            if (billType != 4 && (!creatDateVN.HasValue && !creatDateUS.HasValue))
            {
                MessageBox.Show("Vui lòng nhập Ngày nhập!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;

            }


            if (TextUtils.ToInt(cboRulePay.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Điều khoản TT!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        /// <summary>
        /// tắt form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        string status = "";
        string statusOld = "";
        int check = 0;
        private void cbkType_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbkType.Checked == false)
            //{
            //    status = "PNK";
            //    lbSupplier.Text = "Nhà cung cấp";
            //    lbUser.Text = "Người giao";
            //}
            //else
            //{
            //    status = "PT";
            //    lbSupplier.Text = "Bộ phận";
            //    lbUser.Text = "Người trả";
            //    chkPTNB.Checked = false;
            //}
            if (check == 0)
            {
                check = 1;
                statusOld = status;
            }
            if (billImport.ID > 0)
            {
                status = statusOld;
            }
            loadBilllNumber();
        }

        /// <summary>
        /// giá trị thay đổi khi chọn trong group 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            //loadProduct();

        }

        /// <summary>
        /// click button thêm nhà cung cấp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplier();
            }
        }

        /// <summary>
        /// hàm thêm dòng trong grvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;
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
            //dt.Rows.InsertAt(dtrow, 0);
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;

        }

        /// <summary>
        /// hàm thay đổi giá trị colTotalQty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    ProductSaleModel productSaleModel = new ProductSaleModel();
        //    if (e.Column == colQty || e.Column == colProductID)
        //    {
        //        int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
        //        float sum = 0;
        //        for (int j = 0; j < grvData.RowCount; j++)
        //        {
        //            int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
        //            if (id == idSearch)
        //            {
        //                float qty = TextUtils.ToFloat(grvData.GetRowCellValue(j, colQty));
        //                sum += qty;
        //            }
        //        }
        //        for (int i = 0; i < grvData.RowCount; i++)
        //        {
        //            int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
        //            if (id == idSearch)
        //            {
        //                grvData.SetRowCellValue(i, colTotalQty, sum);
        //            }
        //        }
        //    }
        //    if (e.Column == colProjectID)
        //    {
        //        int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
        //        if (projectID > 0)
        //        {
        //            //int index = cbProject.GetIndexByKeyValue(projectID);
        //            //string projectName = TextUtils.ToString(dtProject.Rows[index]["ProjectName"]);

        //            //grvData.SetFocusedRowCellValue(colProjectNameText, projectName);
        //            //grvData.FocusedColumn = colProjectNameText;
        //            for (int i = 0; i < grvData.RowCount; i++)
        //            {
        //                int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
        //                if (item == 0)
        //                    grvData.SetRowCellValue(i, colProjectID, projectID);
        //            }
        //        }

        //    }
        //}

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isRecallCellValueChanged == true) return;
            try
            {

                //using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    grvData.CloseEditor();

                    if (e.Column.FieldName == colSomeBill.FieldName || e.Column.FieldName == colDateSomeBill.FieldName || e.Column.FieldName == colDPO.FieldName)
                    {
                        if (e.Value == null) return;
                        int[] selectedRows = grvData.GetSelectedRows();
                        if (selectedRows.Length > 0)
                        {
                            foreach (int row in selectedRows)
                            {
                                {
                                    grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);
                                    //grvData.SetRowCellValue(row, colIsUpdated, 1);

                                    //grvData.FocusedRowHandle = -1;
                                    int dpo = TextUtils.ToInt(grvData.GetRowCellValue(row, colDPO));
                                    DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(row, colDateSomeBill));
                                    if (dateSomeBill.HasValue)
                                    {
                                        DateTime dueDate = dateSomeBill.Value.AddDays(dpo);
                                        grvData.SetRowCellValue(row, colDueDate, dueDate);
                                    }

                                }

                            }
                        }
                        else
                        {
                            //grvData.SetRowCellValue(grvData.FocusedRowHandle, grvData.Columns[e.Column.FieldName], e.Value);
                            int row = grvData.FocusedRowHandle;
                            int dpo = TextUtils.ToInt(grvData.GetRowCellValue(row, colDPO));
                            DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(row, colDateSomeBill));
                            if (dateSomeBill.HasValue)
                            {
                                DateTime dueDate = dateSomeBill.Value.AddDays(dpo);
                                grvData.SetRowCellValue(row, colDueDate, dueDate);
                            }
                        }
                        //grvData.SetRowCellValue(e.RowHandle, colIsUpdated, 1);
                    }
                    else
                    {
                        ProductSaleModel productSaleModel = new ProductSaleModel();
                        BillImportDetailModel detail = new BillImportDetailModel();

                        if (e.Column == colQty || e.Column == colProductID)
                        {
                            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
                            int idBillImport = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                            //detail = (BillImportDetailModel)(BillImportDetailBO.Instance.FindByPK(idBillImport));
                            detail = SQLHelper<BillImportDetailModel>.FindByID(idBillImport);
                            float sum = 0;
                            for (int j = 0; j < grvData.RowCount; j++)
                            {
                                int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
                                if (id == idSearch)
                                {
                                    float qty = TextUtils.ToFloat(grvData.GetRowCellValue(j, colQty));
                                    sum += qty;
                                }
                                if (idBillImport > 0)
                                {
                                    detail.Qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
                                    //BillImportDetailBO.Instance.Update(detail);
                                    SQLHelper<BillImportDetailModel>.Update(detail);
                                }

                            }

                            for (int i = 0; i < grvData.RowCount; i++)
                            {
                                int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                                if (id == idSearch)
                                {
                                    grvData.SetRowCellValue(i, colTotalQty, sum);
                                }
                                if (idBillImport > 0)
                                {
                                    detail.Qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
                                    //BillImportDetailBO.Instance.Update(detail);
                                    SQLHelper<BillImportDetailModel>.Update(detail);
                                }
                            }

                        }
                        if (e.Column == colProjectID)
                        {
                            int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                            if (projectID > 0)
                            {
                                //int index = cbProject.GetIndexByKeyValue(projectID);
                                //string projectName = TextUtils.ToString(dtProject.Rows[index]["ProjectName"]);

                                //grvData.SetFocusedRowCellValue(colProjectNameText, projectName);
                                //grvData.FocusedColumn = colProjectNameText;
                                for (int i = 0; i < grvData.RowCount; i++)
                                {
                                    int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                                    if (item == 0)
                                        grvData.SetRowCellValue(i, colProjectID, projectID);
                                }
                            }

                        }
                    }

                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void cbKhoType_EditValueChanged(object sender, EventArgs e)
        {
            cboGroup.EditValue = cbKhoType.EditValue;
            loadProduct();

            if (TextUtils.ToInt(cboBillTypeNew.EditValue) == 4) return;
            //if (billImport.ID > 0) return;
            if (billImport.ID > 0) return; //HieuDV Update 12/01/2023: Chỉ tự động gán người nhận và người gửi khi tạo phiếu, Khi update thì lấy từ DB
            int khoType = TextUtils.ToInt(cbKhoType.EditValue);

            //DataTable dtGroupWarehouse = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
            //                                        new string[] { "@WarehouseID", "@ProductGroupID" },
            //                                        new object[] { warehouseID, khoType });

            //if (dtGroupWarehouse.Rows.Count > 0)
            //{
            //    //model.SenderID = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
            //    cboReciver.EditValue = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
            //}
            ////else cboReciver.EditValue = 0;

            //if (WarehouseCode.Contains("HCM"))
            //{
            //    DataTable dtGroupWarehouseHN = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
            //                                        new string[] { "@WarehouseID", "@ProductGroupID" },
            //                                        new object[] { 1, TextUtils.ToInt(cbKhoType.EditValue) });

            //    if (dtGroupWarehouseHN.Rows.Count > 0)
            //    {
            //        //model.SenderID = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
            //        cboDeliver.EditValue = TextUtils.ToInt(dtGroupWarehouseHN.Rows[0]["UserID"]);
            //    }
            //    else cboDeliver.EditValue = 0;
            //}

            LoadDataReciver();
        }

        int[] supplierIds = new int[] { 1175, 16677 };

        void LoadDataReciver()
        {
            if (billImport.ID > 0) return;
            int suppplierID = TextUtils.ToInt(cboSupplier.EditValue);
            //if (!supplierIds.Contains(suppplierID)) return;
            int khoType = TextUtils.ToInt(cbKhoType.EditValue);
            DataTable dtGroupWarehouse = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
                                                    new string[] { "@WarehouseID", "@ProductGroupID" },
                                                    new object[] { warehouseID, khoType });

            if (dtGroupWarehouse.Rows.Count > 0)
            {
                //model.SenderID = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
                cboReciver.EditValue = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
            }
            //else cboReciver.EditValue = 0;

            //cboDeliver.EditValue = Global.UserID;
            if (WarehouseCode.Contains("HCM"))
            {
                //cboReciver.EditValue = Global.UserID; //Người nhận

                if (dtGroupWarehouse.Rows.Count > 0) cboReciver.EditValue = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);

                if (supplierIds.Contains(suppplierID)) //Nếu là nhập nội bộ
                {
                    DataTable dtGroupWarehouseHN = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
                                                    new string[] { "@WarehouseID", "@ProductGroupID" },
                                                    new object[] { 1, TextUtils.ToInt(cbKhoType.EditValue) });

                    if (dtGroupWarehouseHN.Rows.Count > 0) cboDeliver.EditValue = TextUtils.ToInt(dtGroupWarehouseHN.Rows[0]["UserID"]);
                    else cboDeliver.EditValue = 0;
                }
                else
                {
                    cboDeliver.EditValue = Global.UserID;
                }



                //if (dtGroupWarehouseHN.Rows.Count > 0 && supplierIds.Contains(suppplierID))
                //{
                //    //model.SenderID = TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]);
                //    cboDeliver.EditValue = TextUtils.ToInt(dtGroupWarehouseHN.Rows[0]["UserID"]);
                //}
                //else cboDeliver.EditValue = 0; //Người giao
            }
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
            //if (billImport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void txtDateTime_ValueChanged(object sender, EventArgs e)
        {
            //if (billImport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void getDataPONCC(List<int> lstID, string group, DataTable dt)
        {
            cboGroup.EditValue = TextUtils.ToInt(group);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grvData.FocusedRowHandle = -1;
                btnAdd_Click(null, null);
                if (dt.Rows.Count > 0)
                {
                    string productName = TextUtils.ToString(dt.Rows[i]["ProductName"]);
                    string unit = TextUtils.ToString(dt.Rows[i]["Unit"]);
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductID, dt.Rows[i]["ProductID"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, productName);
                        //grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, unit);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnitName, unit);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colQty, dt.Rows[i]["Qty"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProjectCode, dt.Rows[i]["ProjectModel"]);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, colProductID, dt.Rows[i]["ProductID"]);
                        grvData.SetRowCellValue(i, colProductName, productName);
                        //grvData.SetRowCellValue(i, colUnit, unit);
                        grvData.SetRowCellValue(i, colUnitName, unit);
                        grvData.SetRowCellValue(i, colQty, dt.Rows[i]["Qty"]);
                        grvData.SetRowCellValue(i, colProjectCode, dt.Rows[i]["ProjectModel"]);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmFollowProjectData frm = new frmFollowProjectData();
            frm.sendListID += getDataPONCC;
            frm.ShowDialog();
        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                grvData.CopyToClipboard();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                grvData.PasteFromClipboard();
            }

        }

        private void grdData_Click(object sender, EventArgs e)
        {


        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.FocusedRowHandle = -1;
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                DataRow[] rows = dtProject.Select("ID = " + ID);
                if (rows.Length > 0)
                {
                    string projectName = TextUtils.ToString(rows[0]["ProjectName"]);

                    grvData.SetFocusedRowCellValue(colProjectNameText, projectName);
                }
            }
            catch (Exception)
            { }
        }


        private void btnSaveNow_Click(object sender, EventArgs e)
        {
            if (billImport.Status == true)
            {
                MessageBox.Show("Không thể cập nhật vì phiếu [" + txtBilllNumber.Text + "] đã được duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                saveData();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lbUser_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rdPT_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbkType.Checked == false)
            //{
            //    status = "PNK";
            //    lbSupplier.Text = "Nhà cung cấp";
            //    lbUser.Text = "Người giao";
            //}
            //else
            //{
            //    status = "PT";
            //    lbSupplier.Text = "Bộ phận";
            //    lbUser.Text = "Người trả";
            //}
            if (check == 0)
            {
                check = 1;
                statusOld = status;
            }
            if (billImport.ID > 0)
            {
                status = statusOld;
            }
            loadBilllNumber();
        }
        private void chkPTNB_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPTNB.Checked == false)
            //{
            //    status = "PNK";
            //    lbSupplier.Text = "Nhà cung cấp";
            //    lbUser.Text = "Người giao";
            //}
            //else
            //{
            //    status = "PTNB";
            //    lbSupplier.Text = "Bộ phận";
            //    lbUser.Text = "Người trả";
            //}
            //if (check == 0)
            //{
            //    check = 1;
            //    statusOld = status;
            //}
            //if (billImport.ID > 0)
            //{
            //    status = statusOld;
            //}
            //loadBilllNumber();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboStatus.SelectedIndex == 0)
            //{
            //    status = "PNK";
            //    lbSupplier.Text = "Nhà cung cấp";
            //    lbUser.Text = "Người giao";
            //    colPM.Visible = false;
            //}
            //else if (cboStatus.SelectedIndex == 1)
            //{
            //    status = "PT";
            //    lbSupplier.Text = "Bộ phận";
            //    lbUser.Text = "Người trả";
            //    colPM.Visible = true;
            //}
            //else
            //{
            //    status = "PTNB";
            //    lbSupplier.Text = "Bộ phận";
            //    lbUser.Text = "Người trả";
            //    colPM.Visible = true;
            //}
            //loadBilllNumber();
        }

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {

        }

        private void btnPONCC_Click(object sender, EventArgs e)
        {
            //frmListPONCCDetail frm = new frmListPONCCDetail();
            //List<DataRow> List = new List<DataRow>();
            //frm.ValuesIsSelect = true;
            //frm.rowIndex = rowIndex;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //    List = frm.List;
            //    int row = grvData.RowCount;
            //    rowIndex = frm.rowIndex;
            //    for (int i = 0; i < List.Count; i++)
            //    {
            //        btnAdd_Click(null, null);
            //        //MyLib.AddNewRow(grdData, grvDetailTechExport);
            //        AddPONCCDetailIntoGrv(List[i], i + row);
            //    }
            //}


        }

        private void AddPONCCDetailIntoGrv(DataRow dataRow, int rowNumber)
        {
            int ProductID = TextUtils.ToInt(dataRow["ProductID"]);
            int PONCCDetailID = TextUtils.ToInt(dataRow["ID"]);
            string productName = TextUtils.ToString(dataRow["ProductName"]);
            string productCodeRTC = TextUtils.ToString(dataRow["ProductNewCode"]);
            string ProjectModel = TextUtils.ToString(dataRow["ProjectCode"]);
            string unit = TextUtils.ToString(dataRow["Unit"]);

            grvData.SetRowCellValue(rowNumber, colProductID, ProductID);
            grvData.SetRowCellValue(rowNumber, colProductName, productName);
            grvData.SetRowCellValue(rowNumber, colProductNewCode, productCodeRTC);
            grvData.SetRowCellValue(rowNumber, colUnit, unit);
            grvData.SetRowCellValue(rowNumber, colProjectCode, ProjectModel);
            grvData.SetRowCellValue(rowNumber, colPONCCDetailID, PONCCDetailID);
            grvData.FocusedRowHandle = -1;
        }

        private void btnAddSerialNumber_Click(object sender, EventArgs e)
        {
            //if (grdData.DataSource == null)
            //    return;
            //int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (strID == 0) return;
            //int strQty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            //BillImportDetailModel model = (BillImportDetailModel)BillImportDetailBO.Instance.FindByPK(strID);
            //frmBillImportDetailSerialNumber frm = new frmBillImportDetailSerialNumber();
            //frm.model = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //    loadBillImportDetail();

            //}
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //    int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //    if (grdData.DataSource == null || strID<=0)
            //        return;
            //    BillImportDetailModel model = (BillImportDetailModel)BillImportDetailBO.Instance.FindByPK(strID);
            //    frmPhieuTra frm = new frmPhieuTra();
            //    frm.billImportDetailModel = model;
            //    frm.Type = 1;
            //    frm.productID = model.ProductID;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        string maphieu = frm.maphieu;
            //        grvData.SetFocusedRowCellValue(colPM, maphieu);

            //        //loadBillImportDetail();
            //    }
        }

        private void btnAddCode_Click(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cboBillTypeNew_EditValueChanged(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboBillTypeNew.EditValue) == 4)
            {
                dtpDateRequestImport.Visible = true;
                lblRequestDate.Visible = true;
                dtpCreatDate.Enabled = false;
                dtpCreatDate.EditValue = null;
                if (!billImport.DateRequestImport.HasValue)
                {
                    dtpDateRequestImport.Value = DateTime.Now;
                }
                else
                {
                    dtpDateRequestImport.Value = (DateTime)billImport.DateRequestImport;
                }

            }
            else
            {
                dtpCreatDate.Enabled = true;
                dtpDateRequestImport.Visible = false;
                lblRequestDate.Visible = false;
                if (billImport.CreatDate == null)
                {
                    dtpCreatDate.EditValue = DateTime.Now;
                }
                else
                {
                    dtpCreatDate.EditValue = (DateTime)billImport.CreatDate;
                }

            }

            int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);
            if (billType == 0 || billType == 4)
            {
                status = "PNK";
                lbSupplier.Text = "Nhà cung cấp";
                lbUser.Text = "Người giao";
            }
            else if (billType == 1)
            {
                status = "PT";
                lbSupplier.Text = "Bộ phận";
                lbUser.Text = "Người trả";
            }
            else if (billType == 3)
            {
                status = "PNM";
                lbSupplier.Text = "Nhà cung cấp";
                lbUser.Text = "Người giao";
            }
            else
            {
                status = "PTNB";
                lbSupplier.Text = "Bộ phận";
                lbUser.Text = "Người trả";
            }


            loadBilllNumber();
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
                //InventoryBO.Instance.Insert(inventory);
                SQLHelper<InventoryModel>.Insert(inventory);
            }
        }

        private void btnImportPONCC_Click(object sender, EventArgs e)
        {
            int supplierSaleId = TextUtils.ToInt(cboSupplier.EditValue);
            frmPONCCDetails frm = new frmPONCCDetails();
            frm.supplierSaleId = supplierSaleId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = (DataTable)grdData.DataSource;
                foreach (var item in frm.listDetails)
                {
                    var type = item.GetType();
                    DataRow row = dt.NewRow();
                    row["STT"] = dt.Rows.Count + 1;
                    row["ProductNewCode"] = TextUtils.ToString(type.GetProperty("ProductNewCode").GetValue(item));
                    row["ProductID"] = TextUtils.ToInt(type.GetProperty("ProductID").GetValue(item)); ;
                    row["ProductName"] = TextUtils.ToString(type.GetProperty("ProductName").GetValue(item)); ;
                    row["PONCCDetailID"] = TextUtils.ToInt(type.GetProperty("PONCCDetailID").GetValue(item)); ;
                    row["Unit"] = TextUtils.ToString(type.GetProperty("Unit").GetValue(item));
                    row["UnitName"] = TextUtils.ToString(type.GetProperty("Unit").GetValue(item));
                    row["Qty"] = TextUtils.ToDecimal(type.GetProperty("Qty").GetValue(item));

                    dt.Rows.Add(row);
                }
            }
        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (e.Column == colProductID || e.Column == colProjectID && e.RowHandle >= 0)
            {
                e.Handled = true;
                grvData.ShowEditor();
                SearchLookUpEdit editor = grvData.ActiveEditor as SearchLookUpEdit;
                editor.ShowPopup();
            }

            //Add SerialNumber
            if (e.Column == colAddSeialNumber)
            {
                if (grdData.DataSource == null)
                    return;
                if (strID == 0) return;
                int strQty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
                BillImportDetailModel model = new BillImportDetailModel() { ID = strID, Qty = strQty };
                frmBillSerialNumber frm = new frmBillSerialNumber();
                frm.billImportDetailModel = model;
                frm.Type = 1;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadBillImportDetail();
                }
            }
            ////Add Phieu muon
            if (e.Column == colPM)
            {
                if (grdData.DataSource == null)
                    return;
                //BillImportDetailModel model = (BillImportDetailModel)BillImportDetailBO.Instance.FindByPK(strID);
                frmPhieuTra frm = new frmPhieuTra();
                //frm.billImportDetailModel = model;
                //frm.Type = 1;
                frm.productID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string maphieu = frm.maphieu;
                    grvData.SetFocusedRowCellValue(colPM, maphieu);
                    //loadBillImportDetail();
                }
            }

            //============================================================= Lee Min Khooi Update 10/06/2024 =============================================================
            ////Add hoa don
            //return;
            //if (e.Column == colSomeBill)
            //{
            //    if (grdData.DataSource == null)
            //        return;

            //    frmInvoice frm = new frmInvoice();
            //    frm.billImportDetailsID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));


            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        string maHd = frm.maHd;

            //        // Get selected row
            //        int[] _rowHandle = grvData.GetSelectedRows();
            //        //List<int> invoiceId = frm.listInvoiceID;
            //        List<int> invoiceId = frm.listInvoice.Select(x => x.ID).ToList();
            //        if (_rowHandle.Length > 0)
            //        {
            //            foreach (int index in _rowHandle)
            //            {
            //                InvoiceDTO dto = new InvoiceDTO();

            //                int idMap = TextUtils.ToInt(grvData.GetRowCellValue(index, colIdMapping));
            //                dto.IdMapping = idMap;
            //                //Check khi chưa chọn sản phẩm
            //                if (dto.IdMapping <= 0) continue;

            //                dto.Details = new List<InvoiceLinkModel>();
            //                for (int i = 0; i < invoiceId.Count; i++)
            //                {
            //                    InvoiceLinkModel inLink = new InvoiceLinkModel();
            //                    inLink.InvoiceID = invoiceId[i];
            //                    dto.Details.Add(inLink);
            //                }

            //                //Xóa PRODUCT đã lưu
            //                InvoiceDTO duplicate = listInvoice.FirstOrDefault(p => p.IdMapping == dto.IdMapping);
            //                if (duplicate != null) listInvoice.Remove(duplicate);


            //                listInvoice.Add(dto);
            //                grvData.SetRowCellValue(index, colSomeBill, maHd);
            //            }
            //        }
            //        else
            //        {
            //            InvoiceDTO dto = new InvoiceDTO();
            //            int index = grvData.FocusedRowHandle;
            //            int idMap = TextUtils.ToInt(grvData.GetRowCellValue(index, colIdMapping));
            //            dto.IdMapping = idMap;
            //            //Check khi chưa chọn sản phẩm
            //            if (dto.IdMapping <= 0) return;

            //            dto.Details = new List<InvoiceLinkModel>();
            //            for (int i = 0; i < invoiceId.Count; i++)
            //            {
            //                InvoiceLinkModel inLink = new InvoiceLinkModel();
            //                inLink.InvoiceID = invoiceId[i];
            //                dto.Details.Add(inLink);
            //            }
            //            //Xóa PRODUCT đã lưu
            //            InvoiceDTO duplicate = listInvoice.FirstOrDefault(p => p.IdMapping == dto.IdMapping);
            //            if (duplicate != null) listInvoice.Remove(duplicate);


            //            listInvoice.Add(dto);
            //            grvData.SetRowCellValue(index, colSomeBill, maHd);
            //        }
            //        // Get InvoiceID

            //        if (invoiceId.Count <= 0) loadBillImportDetail();


            //    }
            //}
            //============================================================= End =============================================================
        }
        //======================================================================= Lee Min Khooi Update 11/06 ====================================================================================
        void loadBillImportDetail()
        {

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {


                if (billImport.ReciverID > 0) cboReciver.EditValue = billImport.ReciverID;
                if (billImport.ID > 0) cboDeliver.EditValue = billImport.DeliverID;

                cboSupplier.EditValue = billImport.SupplierID;
                cbKhoType.EditValue = billImport.KhoTypeID;
                cboBillTypeNew.EditValue = billImport.BillTypeNew;
                cboRulePay.EditValue = billImport.RulePayID;
                if (billImport.RulePayID == 0) cboRulePay.EditValue = 34;

                if (!string.IsNullOrEmpty(POCode.Trim())) //Tiến Huy update 22/05/2024
                {

                    DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
                    var dtClone = dt.Clone();
                    int i = 1;
                    //NT.Huy update 22/05/2024
                    if (dtDetails == null)
                    {
                        grdData.DataSource = dtClone;
                        return;
                    }
                    if (dtDetails.Length <= 0)
                    {
                        grdData.DataSource = dtClone;
                        return;
                    }
                    foreach (var row in dtDetails)
                    {
                        ProjectModel project = new ProjectModel();
                        ProductSaleModel productSale = new ProductSaleModel();
                        int projectID = TextUtils.ToInt(row["ProjectID"]);
                        int productID = TextUtils.ToInt(row["ProductSaleID"]);
                        if (projectID > 0)
                        {
                            project = SQLHelper<ProjectModel>.FindByID(projectID);
                        }
                        if (productID > 0)
                        {
                            productSale = SQLHelper<ProductSaleModel>.FindByID(productID);
                        }
                        DataRow dr = dtClone.NewRow();
                        dr["STT"] = i;
                        i++;
                        // dr["ProductNewCode"] = row[""];
                        dr["ProductID"] = row["ProductSaleID"];
                        if (productSale != null)
                        {
                            dr["ProductName"] = productSale.ProductName;
                            dr["ProductNewCode"] = productSale.ProductNewCode;
                            dr["Unit"] = productSale.Unit;
                        }
                        if (project != null)
                        {
                            dr["ProjectNameText"] = project.ProjectName;
                        }
                        dr["ProjectCode"] = row["ProductCodeOfSupplier"];
                        dr["SerialNumber"] = "";
                        dr["QtyRequest"] = row["QuantityRemain"];
                        dr["Qty"] = row["QuantityRemain"];
                        dr["SomeBill"] = "";
                        dr["ProjectID"] = row["ProjectID"];
                        dr["ProjectNameText"] = row["ProjectName"];
                        dr["Note"] = row["POCode"];
                        dr["PONCCDetailID"] = row["ID"];
                        dr["CodeMaPhieuMuon"] = "";
                        dr["Price"] = row["UnitPrice"];
                        dr["ProjectPartListID"] = row["ProjectPartListID"];
                        dr["BillCodePO"] = row["BillCode"];
                        dr["PONCCDetailID"] = row["ID"];
                        dr["ProjectPartListQuantity"] = row["ProjectPartListQuantity"];
                        dr[colIsStock.FieldName] = row[colIsStock.FieldName];
                        dr[colUnitName.FieldName] = row[colUnitName.FieldName];

                        dtClone.Rows.Add(dr);
                    }
                    grdData.DataSource = dtClone;


                }
                else
                {
                    DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
                    grdData.DataSource = dt;
                }

                //loadBilllNumber();
                if (billImport.ID > 0)
                {
                    grvData.Focus();
                    txtBilllNumber.Focus();
                    cboGroup.SetEditValue(billImport.GroupID);
                    txtBilllNumber.Text = billImport.BillImportCode;
                    //cbKhoType.Text = billImport.KhoType;
                    //cbkType.Checked = billImport.BillType;

                    if (billImport.BillTypeNew == 4)
                    {
                        dtpDateRequestImport.Visible = true;
                        lblRequestDate.Visible = true;
                        dtpCreatDate.Enabled = false;
                        dtpCreatDate.EditValue = null;
                        if (!billImport.DateRequestImport.HasValue)
                        {
                            dtpDateRequestImport.Value = DateTime.Now;
                        }
                        else
                        {
                            dtpDateRequestImport.Value = (DateTime)billImport.DateRequestImport;
                        }

                    }
                    else
                    {
                        dtpCreatDate.Enabled = true;
                        dtpDateRequestImport.Visible = false;
                        lblRequestDate.Visible = false;
                        if (billImport.CreatDate == null)
                        {
                            dtpCreatDate.EditValue = DateTime.Now;
                        }
                        else
                        {
                            dtpCreatDate.EditValue = (DateTime)billImport.CreatDate;
                        }

                    }

                    cboBillTypeNew.EditValue = billImport.BillTypeNew;

                    if (billImport.BillImportCode.StartsWith("T") == true)
                    {
                        lbSupplier.Text = "Bộ phận";
                        lbUser.Text = "Người trả";
                    }


                    int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);

                    if (billType == 0 || billType == 4)
                    {
                        status = "PNK";
                        lbSupplier.Text = "Nhà cung cấp";
                        lbUser.Text = "Người giao";
                    }
                    else if (billType == 1)
                    {
                        status = "PT";
                        lbSupplier.Text = "Bộ phận";
                        lbUser.Text = "Người trả";
                    }
                    else if (billType == 3)
                    {
                        status = "PNM";
                        lbSupplier.Text = "Nhà cung cấp";
                        lbUser.Text = "Người giao";
                    }
                    else
                    {
                        status = "PTNB";
                        lbSupplier.Text = "Bộ phận";
                        lbUser.Text = "Người trả";
                    }
                }

                if (_dataHistory.Rows.Count > 0)  //TODO: QUANG UPDATE NGÀY 26/10/2023 KHI SINH RA TỪ LỊCH SỬ MƯỢN
                {
                    cboBillTypeNew.EditValue = 1;

                    cbKhoType.EditValue = groupID;
                    cboDeliver.EditValue = TextUtils.ToInt(_dataHistory.Rows[0]["UserID"]);


                    _dataHistory.Columns.Add("SomeBill", typeof(string));
                    _dataHistory.Columns["BorrowCode"].ColumnName = "CodeMaPhieuMuon";
                    _dataHistory.Columns["BorrowQty"].ColumnName = "Qty";
                    colPM.OptionsColumn.ReadOnly = true;
                    grdData.DataSource = _dataHistory;
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        grvData.SetRowCellValue(i, colSTT, i + 1);
                    }
                }
                //============================================================= Lee Min Khooi Update 10/06/2024 =============================================================
                DataTable dtCount = (DataTable)grdData.DataSource;
                idMapping = dtCount.Rows.Count;
                //============================================================================================================================================================

            }
        }
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                int STT;

                DataTable dt = (DataTable)grdData.DataSource;
                STT = dt.Rows.Count == 0 ? 1 : TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;


                DataRow dtrow = dt.NewRow();
                dtrow["STT"] = STT;
                dtrow["IdMapping"] = ++idMapping;
                dt.Rows.Add(dtrow);
                grdData.DataSource = dt;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*int[] _rowHandle = grvData.GetSelectedRows();
            if (_rowHandle.Length > 0)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa các sản phẩm nàyy không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int index = 0 ; index < _rowHandle.Length; index++)
                    {
                        int ID = TextUtils.ToInt(grvData.GetRowCellValue(_rowHandle[index], colID));

                        //Xóa tất cả hóa đơn có IdMapping = dòng đang xóa
                        int IdMapping = TextUtils.ToInt(grvData.GetRowCellValue(_rowHandle[index], colIdMapping));
                        List<InvoiceDTO> lst = listInvoice.Where(p => p.IdMapping == IdMapping).ToList();
                        foreach (InvoiceDTO item in lst)
                        {
                            listInvoice.Remove(item);
                        }

                        grvData.DeleteRow(_rowHandle[index]);
                        if (ID > 0)
                        {
                            lstIDDelete.Add(ID);
                        }
                    }
                }
            }*/

            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa sản phẩm đã chọn không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Xóa tất cả hóa đơn có IdMapping = dòng đang xóa
                /*int IdMapping = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIdMapping));
                List<InvoiceDTO> lst = listInvoice.Where(p => p.IdMapping == IdMapping).ToList();
                foreach (InvoiceDTO item in lst)
                {
                    listInvoice.Remove(item);
                }*/

                int[] selectedRows = grvData.GetSelectedRows();
                foreach (int row in selectedRows)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    //int rowHandle = grvData.FocusedRowHandle;
                    //grvData.DeleteRow(row);
                    if (ID > 0) lstIDDelete.Add(ID);
                }
                grvData.DeleteSelectedRows();
                //foreach (int row in selectedRows)
                //{

                //}
            }
        }

        private void chkDocument_CheckedChanged(object sender, EventArgs e)
        {
            int curentRow = grvDocImport.FocusedRowHandle;
            bool isCheck = TextUtils.ToBoolean(grvDocImport.GetRowCellValue(curentRow, colIsChecked));
            int status = TextUtils.ToInt(grvDocImport.GetRowCellValue(curentRow, colDocStatus));
            string statusText = TextUtils.ToString(grvDocImport.GetRowCellValue(curentRow, "StatusText"));
            if (status > 0)
            {
                grvDocImport.SetRowCellValue(curentRow, "IsChecked", isCheck);
                MessageBox.Show($"Hồ sơ đi kèm [{statusText}], không thể sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //======================================================================= End Lee Min Khooi Update 11/06 ====================================================================================
        private void btnViewPONCC_Click(object sender, EventArgs e)
        {
            string poCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBillCodePO));
            if (string.IsNullOrWhiteSpace(poCode)) return;
            PONCCModel po = SQLHelper<PONCCModel>.FindByAttribute("BillCode", poCode).FirstOrDefault();
            frmPONCCDetailNew frm = new frmPONCCDetailNew();
            frm.po = po;
            frm.Tag = po.BillCode;
            frm.Show();
        }

        private void cboWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (billImport.ID > 0) return;
            //cboReciver.EditValue = Global.UserID;//Phan Thị Thu Thuỷ

            string warehouseSelected = cboWarehouse.Text;

            //if (!warehouseSelected.Contains("HCM") && string.IsNullOrEmpty(POCode.Trim())) cboReciver.EditValue = Global.UserID;
            //else 
            //if (warehouseSelected.Contains("HCM")) cboReciver.EditValue = 88;//Phan Thị Thu Thuỷ


            //if (WarehouseCode.Contains("HN"))
            //{
            //    if (TextUtils.ToInt(cbKhoType.EditValue) == 13)
            //    {
            //        cboDeliver.EditValue = 8;//Nguyễn Thị Ánh Tuyết

            //    }
            //    else if (TextUtils.ToInt(cbKhoType.EditValue) == 14)
            //    {
            //        cboDeliver.EditValue = 6;//Nguyễn Thị Hằng                    
            //    }
            //    //cboReciver.EditValue = Global.UserID;//Lê Minh Thuỳ
            //}
        }

        int[] idSupplierSale = new[] { 1175, 16677 };
        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            SupplierSaleModel supplierSale = (SupplierSaleModel)cboSupplier.GetSelectedDataRow() ?? new SupplierSaleModel();
            if (idSupplierSale.Contains(supplierSale.ID)) cboRulePay.EditValue = 34; //Điều khoản thanh toán:  no payment
            else cboRulePay.EditValue = 0;


            LoadDataReciver();
        }

        private void btnRequestQC_Click(object sender, EventArgs e)
        {
            List<int> lsProductID = new List<int>();
            if (grvData.GetSelectedRows().Count() <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần QC!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            int[] selectedRows = grvData.GetSelectedRows();

            DataTable dtDetails = TextUtils.LoadDataFromSP("spGetBillImportRequestQCDetail", "A",
                new string[] { "@BillImportRequestID" },
                new object[] { 0 });
            int stt = 1;
            int billImportDetailID = 0;
            foreach (int row in selectedRows)
            {
                DataRow dr = grvData.GetDataRow(row);
                billImportDetailID = TextUtils.ToInt(dr["ID"]);
                int productSaleID = TextUtils.ToInt(dr["ProductID"]);
                string productName = TextUtils.ToString(dr["ProductName"]);
                int billImportQCId = TextUtils.ToInt(dr["BillImportQCID"]);

                if (billImportQCId > 0)
                {
                    MessageBox.Show($"Sản phẩm thứ [{row + 1}] đã được QC!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (!lsProductID.Contains(productSaleID) && productSaleID > 0) lsProductID.Add(productSaleID);
                else
                {
                    //MessageBox.Show($"Sản phẩm thứ [{row + 1}] đã tồn tại!", "Thông báo", MessageBoxButtons.OK);
                    //return;
                }

                DataRow dtr = dtDetails.NewRow();
                dtr["STT"] = stt++;
                dtr["ProductName"] = productName;
                dtr["ProductSaleID"] = productSaleID;
                dtr["BillImportDetailID"] = billImportDetailID;
                dtr["ProjectID"] = TextUtils.ToInt(dr[colProjectID.FieldName]);
                dtr["POKHCode"] = TextUtils.ToString(dr[colPONumber.FieldName]);
                dtr["Quantity"] = TextUtils.ToInt(dr[colQty.FieldName]);
                dtr["BillCode"] = TextUtils.ToString(dr[colBillCodePO.FieldName]);

                dtDetails.Rows.Add(dtr);
            }

            frmBillImportQCDetail frm = new frmBillImportQCDetail();
            frm.dtDetails = dtDetails;
            frm.isAddNewToBillImport = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int billImportQCID = frm.billImportQC.ID;
                if (billImportQCID > 0)
                {
                    foreach (int row in selectedRows)
                    {
                        grvData.SetRowCellValue(row, colStatusQCText, "Đã yêu cầu QC");
                        grvData.SetRowCellValue(row, colBillImportQCID, billImportQCID);
                    }
                }
            }
        }

        private void btnCheckRequestQC_Click(object sender, EventArgs e)
        {
            if (grvData.GetSelectedRows().Count() <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần QC!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            int[] selectedRows = grvData.GetSelectedRows();
            List<int> ls = new List<int>();
            foreach (int row in selectedRows)
            {
                DataRow dr = grvData.GetDataRow(row);
                int billImportQCId = TextUtils.ToInt(dr["BillImportQCID"]);
                if (!ls.Contains(billImportQCId) && billImportQCId > 0) ls.Add(billImportQCId);
            }

            if (ls.Count <= 0)
            {
                MessageBox.Show("Sản phẩm chưa được yêu cầu QC!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else
            {
                foreach (int id in ls)
                {
                    BillImportQCModel model = SQLHelper<BillImportQCModel>.FindByID(id);
                    if (model != null)
                    {
                        frmBillImportQCDetail frm = new frmBillImportQCDetail();
                        frm.billImportQC = model;
                        frm.isCheckBillQC = true;
                        frm.Show();
                    }
                }
            }
        }




        #region Logs
        private void LogActions()
        {
            try
            {
                var controls = new Component[] { btnColseAndSave, btnSaveNew };
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

        private BillImportLog GetCurrentData()
        {
            var data = new BillImportLog();
            var billImportLog = new BillImportModel();
            billImportLog.ID = billImport.ID;

            billImportLog.BillImportCode = txtBilllNumber.Text.Trim();
            if (dtpCreatDate.EditValue != null)
            {
                billImportLog.CreatDate = (DateTime)dtpCreatDate.EditValue;
            }
            billImportLog.Deliver = cboDeliver.Text.Trim();
            billImportLog.Reciver = cboReciver.Text.Trim();
            billImportLog.Suplier = cboSupplier.Text.Trim();
            billImportLog.KhoType = cbKhoType.Text.Trim();
            billImportLog.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
            billImportLog.ReciverID = TextUtils.ToInt(cboReciver.EditValue);
            billImportLog.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            billImportLog.GroupID = TextUtils.ToString(cboGroup.EditValue);
            billImportLog.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            billImportLog.WarehouseID = TextUtils.ToInt(cboWarehouse.EditValue); ;
            billImportLog.BillTypeNew = TextUtils.ToInt(cboBillTypeNew.EditValue);
            if (billImportLog.BillTypeNew == 4)
            {
                billImportLog.DateRequestImport = dtpDateRequestImport.Value;
            }
            billImportLog.UpdatedDate = DateTime.Now;
            billImportLog.UpdatedBy = Global.AppUserName;
            billImportLog.Status = false;
            var detailsLog = new List<BillImportDetailModel>();
            var invProjectLog = new List<InventoryProjectModel>();

            grvData.CloseEditForm();
            grvData.FocusedRowHandle = -1;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                BillImportDetailModel detail = new BillImportDetailModel
                {
                    ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID)),
                    BillImportID = billImport.ID,
                    ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID)),
                    Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty)),
                    Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice)),
                    ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode)),
                    SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill)),
                    Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote)),
                    STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT)),
                    TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty)),
                    ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID)),
                    PONCCDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPONCCDetailID)),
                    SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colSerialNumber)),
                    CodeMaPhieuMuon = TextUtils.ToString(grvData.GetRowCellValue(i, colPM)),
                    BillExportDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colBorrowID)),
                    QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyRequest)),
                    BillCodePO = TextUtils.ToString(grvData.GetRowCellValue(i, colBillCodePO)),
                    ProjectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartListID)),
                    IsKeepProject = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsKeepProject)),
                    ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText)),
                    DateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(i, colDateSomeBill))
                };
                detailsLog.Add(detail);
                string projectNameText = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText));
                if (detail.ProjectID > 0 || string.IsNullOrWhiteSpace(projectNameText))
                {
                    InventoryProjectModel inventoryProject = new InventoryProjectModel();
                    inventoryProject.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colInventoryProjectID));
                    inventoryProject.ProjectID = detail.ProjectID;
                    inventoryProject.ProductSaleID = detail.ProductID;
                    inventoryProject.WarehouseID = billImport.WarehouseID;
                    inventoryProject.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colProjectPartListQuantity));
                    inventoryProject.Note = projectNameText;
                    invProjectLog.Add(inventoryProject);
                }
                else
                {
                    invProjectLog.Add(null);
                }
            }
            data.BillImport = billImportLog;
            data.Details = detailsLog;
            data.InvPrj = invProjectLog;
            return data;
        }

        private dynamic GetDataChange(dynamic oldData)
        {
            var oldDataLog = (BillImportLog)oldData;
            var newDataLog = GetCurrentData();
            return new
            {
                BillIport = new
                {
                    Old = oldDataLog.BillImport,
                    New = newDataLog.BillImport
                },
                Details = new
                {
                    Old = oldDataLog.Details,
                    New = newDataLog.Details
                },
                InvPrj = new
                {
                    Old = oldDataLog.InvPrj,
                    New = newDataLog.InvPrj
                }
            };
        }

        public class BillImportLog
        {
            public BillImportModel BillImport;
            public List<BillImportDetailModel> Details;
            public List<InventoryProjectModel> InvPrj;
        }
        #endregion

        private void grvData_ShowingEditor(object sender, CancelEventArgs e)
        {
            bool isColumn = grvData.FocusedColumn == colSomeBill
                || grvData.FocusedColumn == colDateSomeBill
                || grvData.FocusedColumn == colDPO
                || grvData.FocusedColumn == colTaxReduction
                || grvData.FocusedColumn == colCOFormE;

            int deliverID = TextUtils.ToInt(billImport.DeliverID);
            if (billImport.ID <= 0) deliverID = TextUtils.ToInt(cboDeliver.EditValue);

            if (isColumn) e.Cancel = (Global.UserID != deliverID && !Global.IsAdmin);

        }

        #region Update chức năng phiếu xuất 16725
        void LoadStatusPur()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            dt.Rows.Add(1, "Đã bàn giao");
            dt.Rows.Add(2, "Hủy bàn giao");
            dt.Rows.Add(3, "Không có");

            cboStatusPur.DisplayMember = "Name";
            cboStatusPur.ValueMember = "ID";
            cboStatusPur.DataSource = dt;
        }

        void LoadStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            dt.Rows.Add(1, "Đã nhận");
            dt.Rows.Add(2, "Đã hủy nhận");
            dt.Rows.Add(3, "Không có");

            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";
            cboStatus.DataSource = dt;
        }
        #endregion

        private void cboReciver_EditValueChanged(object sender, EventArgs e)
        {

        }


        #region Update lại chức năng giữ kho
        void UpdateInventoryProjectNew(BillImportDetailModel detail, int i)
        {
            if (detail.IsNotKeep == true) return; //Nếu tích không giữ
            if (billImport.BillTypeNew != 0) return; //nếu là nhập kho
            if (billImport.BillExportID > 0) return;//Nếu là chuyển kho

            string projectNameText = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText));
            string pokhDetailQtyString = TextUtils.ToString(grvData.GetRowCellValue(i, colPOKHDetailQuantity));

            //if (string.IsNullOrEmpty(pokhDetailQtyString)) return;

            var pokhList = new List<(int POKHDetailID, decimal QuantityRequest)>();


            if (!string.IsNullOrEmpty(pokhDetailQtyString))
            {
                string[] pairs = pokhDetailQtyString.Split(',');
                foreach (string pair in pairs)
                {
                    if (string.IsNullOrWhiteSpace(pair)) continue;
                    string[] parts = pair.Split('-');
                    if (parts.Length == 2)
                    {
                        int pokhId = TextUtils.ToInt(parts[0].Trim());
                        decimal pokhQty = TextUtils.ToDecimal(parts[1].Trim());
                        if (pokhId > 0 && pokhQty > 0)
                        {
                            pokhList.Add((pokhId, pokhQty));
                        }
                    }
                }
            }
           

            //if (pokhList.Count == 0) return;

            //check PO,dự án
            bool hasProjectOrPokh = detail.ProjectID > 0 || pokhList.Any(x => x.POKHDetailID > 0);
            if (!hasProjectOrPokh) return;

            decimal quantityRealy = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
            decimal quantityRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantityRequestBuy));

            decimal totalPokhRequest = pokhList.Sum(x => x.QuantityRequest);
            if (totalPokhRequest > 0)
            {
                quantityRequest = totalPokhRequest;
            }

            decimal quantityKeep = quantityRealy;
            if (quantityRealy > 0 && quantityRequest > 0) quantityKeep = Math.Min(quantityRealy, quantityRequest);

            decimal totalPokhQty = pokhList.Sum(x => x.QuantityRequest);
            if (totalPokhQty == 0) return;

            int deliverEmployeeID = TextUtils.ToInt(cboDeliver.EditValue);
            int customerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCustomerID));

            int firstInventoryProjectID = 0;
            var invProjectStuffs = new List<string>();

            foreach (var item in pokhList)
            {
                decimal proportion = totalPokhQty > 0 ? item.QuantityRequest / totalPokhQty : 0;
                decimal thisQuantityKeep = proportion * quantityKeep;

                if (thisQuantityKeep <= 0) continue;

                // Ưu tiên trả nợ cha parentID trước khi cập nhật số lượng giữ mới
                decimal adjustedQty = UpdateReturnQuantityLoanNew(item.POKHDetailID, thisQuantityKeep);


                var expFind = new Expression(InventoryProjectModel_Enum.POKHDetailID, item.POKHDetailID)
                    .And(new Expression(InventoryProjectModel_Enum.IsDeleted, 0))
                    .And(new Expression(InventoryProjectModel_Enum.ParentID, 0));
                var rootProjects = SQLHelper<InventoryProjectModel>.FindByExpression(expFind);
                InventoryProjectModel root = rootProjects.FirstOrDefault();

                bool isNew = root == null || root.ID <= 0;

                if (isNew)
                {
                    root = new InventoryProjectModel
                    {
                        ProjectID = detail.ProjectID,
                        ProductSaleID = detail.ProductID,
                        WarehouseID = billImport.WarehouseID,
                        Note = projectNameText,
                        EmployeeID = deliverEmployeeID,
                        Quantity = adjustedQty,
                        QuantityOrigin = adjustedQty,
                        POKHDetailID = item.POKHDetailID,
                        CustomerID = customerID,
                        IsDeleted = false,
                        ParentID = 0,
                        CreatedDate = DateTime.Now,
                        CreatedBy = Global.AppUserName
                    };

                    if (root.Quantity > 0)
                    {
                        var inserted = SQLHelper<InventoryProjectModel>.Insert(root);
                        root.ID = inserted.ID;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    root.Quantity = adjustedQty;
                    root.QuantityOrigin = adjustedQty;
                    root.Note = projectNameText;
                    root.EmployeeID = deliverEmployeeID;
                    root.CustomerID = customerID;
                    root.IsDeleted = root.Quantity <= 0;
                    root.UpdatedDate = DateTime.Now;
                    root.UpdatedBy = Global.AppUserName;
                    SQLHelper<InventoryProjectModel>.Update(root);
                }

                if (root.ID > 0 && adjustedQty > 0)
                {
                    invProjectStuffs.Add($"{root.ID}-{adjustedQty}");
                    if (firstInventoryProjectID == 0) firstInventoryProjectID = root.ID;
                }
            }

            string stuffedInvProjects = string.Join(",", invProjectStuffs);
            //grvData.SetRowCellValue(i, colInventoryProjectIDs, stuffedInvProjects);

            detail.InventoryProjectID = firstInventoryProjectID > 0 ? firstInventoryProjectID : (int?)null;
        }

        decimal UpdateReturnQuantityLoanNew(int pokhDetailId, decimal quantityKeep)
        {
            if (quantityKeep <= 0) return 0;

            var exp1 = new Expression(InventoryProjectModel_Enum.POKHDetailID, pokhDetailId);
            var exp2 = new Expression(InventoryProjectModel_Enum.IsDeleted, 0);
            var exp3 = new Expression(InventoryProjectModel_Enum.ParentID, 0, ">");
            var inventoryProjects = SQLHelper<InventoryProjectModel>.FindByExpression(exp1.And(exp2).And(exp3));

            inventoryProjects = inventoryProjects.OrderBy(x => x.ID).ToList();

            foreach (var item in inventoryProjects)
            {
                if (quantityKeep <= 0) break;

                int parentId = TextUtils.ToInt(item.ParentID);
                InventoryProjectModel inventoryProjectParent = SQLHelper<InventoryProjectModel>.FindByID(parentId);
                if (inventoryProjectParent == null || inventoryProjectParent.ID <= 0) continue;

                decimal quantityLoan = TextUtils.ToDecimal(inventoryProjectParent.QuantityOrigin) - TextUtils.ToDecimal(inventoryProjectParent.Quantity);
                if (quantityLoan <= 0) continue;

                decimal returnAmount = Math.Min(quantityLoan, quantityKeep);
                inventoryProjectParent.Quantity += returnAmount;
                inventoryProjectParent.UpdatedDate = DateTime.Now;
                inventoryProjectParent.UpdatedBy = Global.AppUserName;
                SQLHelper<InventoryProjectModel>.Update(inventoryProjectParent);

                quantityKeep -= returnAmount;
            }

            return quantityKeep;
        }
        #endregion
    }
}