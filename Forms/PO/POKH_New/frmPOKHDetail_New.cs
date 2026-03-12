using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Forms.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPOKHDetail_New : _Forms
    {

        public int ID;
        public bool isCopy = false;
        public POKHModel oPOKH = new POKHModel();
        public QuotationKHModel quotationKH = new QuotationKHModel();
        ArrayList lstIDDelete = new ArrayList();
        DataTable dtProject = new DataTable();
        DataTable dtProduct = new DataTable();
        int IDDetail = 0;
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<POKHFileModel> listFiles = new List<POKHFileModel>();
        List<POKHFileModel> listFileDelete = new List<POKHFileModel>();
        List<DataTable> lstDt = new List<DataTable>();

        int warehouseID = 0;
        public string statusText = "";


        DataTable dtOld = new DataTable();
        DataSet ds = new DataSet();
        List<int> listIDInsert = new List<int>();
        List<TreeListNode> ListChildNode = new List<TreeListNode>();
        int indexNode = 0;
        int newID = 0;

        DataTable dtData = new DataTable();


        DataTable dtClone = new DataTable(); //vt.Nam update 03/10/2024


        public frmPOKHDetail_New(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void POKHDetail_New_Load(object sender, EventArgs e)
        {

            try
            {
                WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
                this.Text += $" - {warehouse.WarehouseCode}";

                cGlobVar.LockEvents = true;
                loadProduct();
                // loadContact();
                loadCode();
                loadProject();
                loadCustomer();
                // =========================================PQ.Chien - UPDATE - 22/03/2025=================================================================
                if (oPOKH.ID == 0)
                {
                    loadMainIndexNew();
                }
                else
                {
                    loadMainIndex();

                }
                loadUser();
                loadData();
                // lee min khooi update 11/09/2024
                //loadDetailUser();
                //LoadUpdatePO();
                LoadCurrency();
                loadMaster();
                if (oPOKH.ID == 0)
                    cboStatus.SelectedIndex = 0;
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.ValueMember = "ID";
            cboCurrency.Properties.DisplayMember = "Code";
            cboCurrency.Properties.DataSource = list;
            cboCurrency.EditValue = 1;

        }


        void loadProduct()
        {
            //if (cboGroup.Text == "") return;
            //dtProduct = TextUtils.Select("SELECT ID,ProductNewCode,ProductCode,ProductName,ItemType,Unit,Maker FROM ProductSale");
            //cbProduct.DisplayMember = "ProductNewCode";
            //cbProduct.ValueMember = "ID";
            //cbProduct.DataSource = dtProduct;


            var listGroup = SQLHelper<ProductGroupModel>.FindAll().Select(x => x.ID).ToList();
            var idGroup = string.Join(",", listGroup);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductSale", "A", new string[] { "@IDgroup" }, new object[] { idGroup });

            cboProduct.ValueMember = "ID";
            cboProduct.DisplayMember = "ProductNewCode";
            cboProduct.DataSource = dt;
        }


        void loadMaster()
        {
            if (oPOKH.ID > 0)
            {
                cboStatus.SelectedIndex = oPOKH.Status;
                cboStatus.Text = statusText;// VTNam update trạng thái thanh toán
                cboUser.EditValue = oPOKH.UserID;
                txtPOCode.Text = oPOKH.POCode;
                cbProject.EditValue = oPOKH.ProjectID;
                dtpPOdate.Value = (DateTime)oPOKH.ReceivedDatePO;
                txtNote.Text = oPOKH.Note;
                txtTotalPO.Text = TextUtils.ToString(oPOKH.TotalMoneyPO);
                cboCustomer.EditValue = oPOKH.CustomerID;
                cbType.EditValue = oPOKH.POType;
                ckbBigAccount.Checked = oPOKH.NewAccount;
                radBigAccount.Checked = oPOKH.AccountType == 0;           // =========================================PQ.Chien - UPDATE - 22/03/2025=================================================================
                radMinorAccount.Checked = oPOKH.AccountType == 1;         // =========================================PQ.Chien - UPDATE - 22/03/2025=================================================================
                txtEndUser.Text = oPOKH.EndUser;
                cbPart.EditValue = oPOKH.PartID;
                txtPONumber.Text = oPOKH.PONumber;
                //ckbMerge.Checked = oPOKH.IsMerge;

                ckType.Checked = oPOKH.UserType == 1;
                /*if (oPOKH.UserType == 1)
                    ckType.Checked = true;
                else
                    ckType.Checked = false;*/


                cboCurrency.EditValue = oPOKH.CurrencyID;

                txtDiscount.Value = TextUtils.ToDecimal(oPOKH.Discount); //NTA B update 041025
                txtTotalMoneyDiscount.Value = TextUtils.ToDecimal(oPOKH.TotalMoneyDiscount); //NTA B update 041025
                

            }

            if (!isCopy)
            {
                listFiles = SQLHelper<POKHFileModel>.FindByAttribute("POKHID", oPOKH.ID);
            }
            else
            {
                loadCode();
                txtPOCode.Text = txtPOCode.Text + oPOKH.POCode.Split('_')[0];
                dtpPOdate.Value = DateTime.Now;
                txtDiscount.Value = 0; //NTA B update 041025
                txtTotalMoneyDiscount.Value = 0; //NTA B update 041025
            }
            LoadFile(listFiles);
        }
        void loadData()
        {
            if (quotationKH.ID > 0)
            {
                sendData();
                return;
            }
            //DataSet ds = new DataSet();
            ds = TextUtils.LoadDataSetFromSP("[spGetPOKHDetail_New]", new string[] { "@ID", "@IDDetail" }, new object[] { oPOKH.ID, IDDetail });

            //================================== lee min khooi update 11/09/2024 ===============================
            if (isCopy)
            {
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    row["ID"] = 0;
                }
            }
            //==================================== end update 11/09/2024 ===================================

            dtData = ds.Tables[0];
            //TreeData.DataSource = ds.Tables[0];
            TreeData.DataSource = dtData;

            dtClone = dtData.Clone(); //vt.Nam update 03/10/2024

            grdDetailUser.DataSource = ds.Tables[1];
            calculateTotal();
            TreeData.ExpandAll();
        }
        void calculateTotal()
        {
            if (cGlobVar.LockEvents) return;
            decimal totalPO = 0;
            TreeData.Focus();
            for (int i = 0; i < TreeData.AllNodesCount; i++)
            {
                TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
                totalPO += TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colTotalPriceIncludeVAT));
            }
            txtTotalPO.EditValue = totalPO;
        }
        bool IsPay;
        bool IsShip;
        bool IsBill;
        void checkStatus()
        {
            try
            {
                IsPay = true;
                IsBill = true;
                string billText = "";
                int bill = 0;
                for (int i = 0; i < TreeData.AllNodesCount; i++)
                {
                    TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
                    bill = TextUtils.ToInt(TreeData.GetRowCellValue(node, colBillNumber));
                    billText = TextUtils.ToString(TreeData.GetRowCellValue(node, colBillNumber));
                    if (bill == 0 && billText == "")
                        IsBill = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        // lee min khooi update 11/09/2024
        /*void loadDetailUser()
        {
            TreeListNode node = TreeData.GetNodeByVisibleIndex(0);
            IDDetail = TextUtils.ToInt(TreeData.GetRowCellValue(node, colID_1));
            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHDetailMoney", "A", new string[] { "@IDDetail" }, new object[] { oPOKH.ID }); // lee min khooi update 11/09/2024
            grdDetailUser.DataSource = dt;
        }*/
        void loadCbPart()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCustomerPart", "A", new string[] { "@ID" }, new object[] { TextUtils.ToInt(cboCustomer.EditValue) });
            cbPart.Properties.DisplayMember = "PartCode";
            cbPart.Properties.ValueMember = "ID";
            cbPart.Properties.DataSource = dt;
        }
        void sendData()
        {
            cbProject.EditValue = TextUtils.ToString(quotationKH.ProjectID);
            cboUser.EditValue = TextUtils.ToString(quotationKH.UserID);
            cboCustomer.EditValue = TextUtils.ToString(quotationKH.CustomerID);
            DataTable dtConvert = new DataTable();
            dtConvert = TextUtils.LoadDataFromSP("spGetQuotationKHDetail", "A", new string[] { "@ID" }, new object[] { quotationKH.ID });
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetPOKHDetail", "A", new string[] { "@ID" }, new object[] { oPOKH.ID });
            for (int i = 0; i < dtConvert.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["ProductID"] = dtConvert.Rows[i]["ProductID"];
                dt.Rows[i]["Qty"] = dtConvert.Rows[i]["Qty"];
                //dt.Rows[i]["UnitPrice"] = dtConvert.Rows[i]["UnitPrice"];
                //dt.Rows[i]["IntoMoney"] = dtConvert.Rows[i]["IntoMoney"];
                dt.Rows[i]["Maker"] = dtConvert.Rows[i]["Maker"];
                dt.Rows[i]["Unit"] = dtConvert.Rows[i]["Unit"];
                dt.Rows[i]["ProductName"] = dtConvert.Rows[i]["ProductName"];
            }
            TreeData.DataSource = dt;

        }
        // lee min khooi update 11/09/2024
        //void LoadUpdatePO()
        //{
        //    if (TreeData.AllNodesCount > 0 && oPOKH.UserType == 1)
        //        for (int i = 0; i < TreeData.AllNodesCount; i++)
        //        {
        //            TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
        //            IDDetail = TextUtils.ToInt(TreeData.GetRowCellValue(node, colID_1));
        //            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHDetailMoney", "A", new string[] { "@IDDetail" }, new object[] { IDDetail });
        //            grdDetailUser.DataSource = dt;
        //            lstDt.Add(dt);
        //        }
        //}
        void loadCode()
        {
            if (oPOKH.ID > 0 && !isCopy && warehouseID != 2) return;
            string kh = TextUtils.ToString(cboCustomer.Text);
            string maPO = DateTime.Now.ToString("ddMMyyy");
            string code = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 POCode FROM POKH Where POCode LIKE '%{cboCustomer.Text}%' ORDER BY ID DESC"));
            string[] arr = code.Split('.');
            if (arr.Count() < 2)
            {
                txtPOCode.Text = kh + "_" + maPO + ".1";
                return;
            }
            string so = TextUtils.ToString("." + (TextUtils.ToInt(arr[1]) + 1));
            txtPOCode.Text = kh + "_" + maPO + TextUtils.ToString(so);
        }
        void loadUser()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeManager", "A", new string[] { "@group" }, new object[] { 0 });
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeManager", new string[] { "@group" }, new object[] { 0 });
            DataTable dtOld = dataSet.Tables[0];
            DataTable dtNew = dataSet.Tables[3];

            var dataOld = dtOld.AsEnumerable().Select(x => new
            {
                UserID = x.Field<int>("UserID"),
                FullName = x.Field<string>("FullName"),
                TeamType = x.Field<int>("TeamType"),
                TeamTypeText = x.Field<string>("TeamTypeText"),
            }).ToList();

            var dataNew = dtNew.AsEnumerable().Select(x => new
            {
                UserID = x.Field<int>("UserID"),
                FullName = x.Field<string>("FullName"),
                TeamType = x.Field<int>("TeamType"),
                TeamTypeText = x.Field<string>("TeamTypeText"),
            }).ToList();

            var dt = dataOld.Concat(dataNew).OrderByDescending(x => x.TeamTypeText).ToList();


            //List<UsersModel> listUser = SQLHelper<UsersModel>.FindAll();
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "UserID";
            //cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;

            ////NTA B update 161025
            var colGroup = cboUser.Properties.View.Columns["TeamTypeText"];
            colGroup.GroupIndex = 0;
            colGroup.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom; // Dùng custom sort

            cboUser.Properties.View.CustomColumnSort += (s, e) =>
            {
                if (e.Column.FieldName == "TeamTypeText")
                {
                    var customOrder = dt.Select(x => x.TeamTypeText).Distinct().ToList();

                    int i1 = customOrder.IndexOf(e.Value1?.ToString());
                    int i2 = customOrder.IndexOf(e.Value2?.ToString());

                    if (i1 < 0) i1 = int.MaxValue;
                    if (i2 < 0) i2 = int.MaxValue;

                    e.Result = i1.CompareTo(i2);
                    e.Handled = true;
                }
            };
            //END //NTA B update 161025

            cbUser.DisplayMember = "FullName";
            //cbUser.ValueMember = "ID";
            cbUser.ValueMember = "UserID";
            cbUser.DataSource = dt;


            

        }
        void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,UserID,ContactID,CustomerID,ProjectName,PO From Project");
            cbProject.Properties.DisplayMember = "ProjectCode";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dtProject;
        }

        // =========================================PQ.Chien - UPDATE - 22/03/2025=================================================================
        public void loadMainIndexNew()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID IN( 8,9,10,11,12,13,24)");
            List<MainIndexModel> list = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { "@Type" }, new object[] { 0 });
            cbType.Properties.DisplayMember = "MainIndex";
            cbType.Properties.ValueMember = "ID";
            cbType.Properties.DataSource = list;
        }
        public void loadMainIndex()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID IN( 8,9,10,11,12,13,24)");
            List<MainIndexModel> list = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { "@Type" }, new object[] { 1 });
            cbType.Properties.DisplayMember = "MainIndex";
            cbType.Properties.ValueMember = "ID";
            cbType.Properties.DataSource = list;

        }

        void loadCustomer()
        {
            //DataTable dt = TextUtils.Select(" SELECT * FROM Customer where IsDeleted <> 1");
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).ToList();
            cboCustomer.Properties.DisplayMember = "CustomerShortName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = list;
        }

        /// <summary>
        /// validate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool ValidateForm()
        {
            if (TextUtils.ToDecimal(txtDiscount.Text) > 100) //NTA B - update 04/10/25
            {
                MessageBox.Show("Discount không được lớn hơn 100%!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cbType.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn loại PO.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cboCustomer.EditValue) <= 0) //NTA B - update 12/09/25
            {
                MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cbProject.EditValue) <= 0) //NTA B - update 12/09/25
            {
                MessageBox.Show("Xin hãy chọn dự án.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            if (oPOKH.ID == 0)
            {
                DataTable dt;
                if (oPOKH.ID > 0)
                {
                    int strID = oPOKH.ID;
                    dt = TextUtils.Select("select top 1 ID from POCode where POKH = '" + txtPOCode.Text.Trim() + "'And ID<>" + oPOKH.ID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 ID from POCode where POKH = '" + txtPOCode.Text.Trim() + "'");
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }

            decimal totalPercent = 0;
            if (ckType.Checked)
            {

                for (int i = 0; i < grvDetailUser.RowCount; i++)
                {
                    totalPercent += TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(i, colPercentUser));
                }


                if (totalPercent > 1)
                {
                    MessageBox.Show("Tổng số phần trăm của người phụ trách sản phẩm KHÔNG được vượt quá 100%!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }


            if (TreeData.AllNodesCount == 0) //NTA B - update 12/09/25
            {
                MessageBox.Show("Xin hãy thêm sản phẩm.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }




        private void bntAdd_Click(object sender, EventArgs e)
        {
            //DataRow newRow = null;
            int STT;
            TreeList tl = (TreeList)TreeData;

            //DataTable dt = (DataTable)tl.DataSource;
            //STT = dt.Rows.Count == 0 ? 1 : (TextUtils.ToInt(tl.GetRowCellValue(tl.Nodes.LastNode, colSTT)) + 1);
            STT = TextUtils.ToInt(tl.GetRowCellValue(tl.Nodes.LastNode, colSTT)) + 1;

            TreeListNode node = TreeData.AppendNode(new object[] { }, null);

            node[colSTT] = STT;
            node[colID_1] = newID;

            //newRow = dt.NewRow();
            //newRow["STT"] = STT;

            //TreeData.Nodes.Add(newRow);

            //TreeNode node = new TreeNode();
            //TreeData.Nodes.Add(node);
        }
        private void btnAddChild_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)TreeData.DataSource;
            TreeListNode nodeFocus = TreeData.FocusedNode;

            if (nodeFocus == null)
            {
                return;
            }
            //foreach (TreeListNode item in TreeData.Nodes)
            //{
            //    if (nodeFocus.ParentNode == item)
            //    {
            //        return;
            //    }
            //}

            newID--;
            string parentTT = TextUtils.ToString(nodeFocus.GetValue("TT"));
            //int parentSTT = TextUtils.ToInt(nodeFocus.GetValue("STT"));
            int childCount = nodeFocus.Nodes.Count;

            string childTT = parentTT + "." + (childCount + 1).ToString();

            DataRow newRow = dt.NewRow();
            newRow["STT"] = (childCount + 1);
            newRow["TT"] = childTT;
            newRow["ID"] = newID;

            nodeFocus.Nodes.Add(newRow);

            TreeData.ExpandAll();


            //========================= lee min khoi update 11/09/2024 ================================
            //string fatherSTT = TextUtils.ToString(nodeFocus["STT"]);
            ////int parentSTT = TextUtils.ToInt(nodeFocus.GetValue("STT"));
            //int childCount = nodeFocus.Nodes.Count;
            //DataRow newRow = dt.NewRow();
            //nodeFocus.Nodes.Add(newRow);
            //for (int i = 0; i < childCount; i++)
            //{
            //    newRow["STT"] = $"{fatherSTT}.{i + 1}";
            //    TreeData.ExpandAll();
            //}
            //========================= end update 11/09/2024 ================================



            //TreeListNode nodeFocus = TreeData.FocusedNode;
            //if (nodeFocus == null)
            //{
            //    return;
            //}
            //foreach (TreeListNode item in TreeData.Nodes)
            //{
            //    if (nodeFocus.ParentNode == item)
            //    {
            //        return;
            //    }
            //}
            //nodeFocus.Nodes.Add();
            //TreeData.ExpandAll();
        }
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }

        private void btnNewSP_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            frmQuotationKHData frm = new frmQuotationKHData();
            frm.sendListID += DataReceive;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        private void DataReceive(List<string> lstcode, string group, DataTable dt)
        {
            cGlobVar.LockEvents = true;
            checkDataStock(lstcode, dt);
            genProductCode(lstcode, dt);
            cGlobVar.LockEvents = false;
        }
        void checkDataStock(List<string> lstcode, DataTable dtproduct)
        {
            try
            {
                string code = string.Join(",", lstcode);
                DataTable dt = TextUtils.LoadDataFromSP("[spLoadDataQuotationToPOKH]", "A", new string[] { "@result" }, new object[] { code });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                    if (ID == 0)
                    {
                        ProductSaleModel productSaleModel = new ProductSaleModel();
                        string productcode = TextUtils.ToString(dt.Rows[i]["NvarcharValue"]);
                        DataRow[] dtr = dtproduct.Select($"ProductNewCode='{productcode}'");
                        productSaleModel.ProductGroupID = TextUtils.ToInt(53);
                        productSaleModel.ProductNewCode = TextUtils.ToString(TextUtils.ExcuteScalar($"Exec spCreateNewCode @group={53}"));
                        productSaleModel.ProductCode = TextUtils.ToString(dtr[0]["ProductCode"]);
                        productSaleModel.ProductName = TextUtils.ToString(dtr[0]["ProductName"]);
                        productSaleModel.Maker = TextUtils.ToString(dtr[0]["Maker"]);
                        productSaleModel.Unit = TextUtils.ToString(dtr[0]["Unit"]);
                        DataTable dtcheck = TextUtils.Select($"Select * From ProductSale where ProductName='{productSaleModel.ProductName}'");
                        if (dtcheck.Rows.Count == 0)
                            ProductSaleBO.Instance.Insert(productSaleModel);
                    }
                }
                txtQuotatiton.EditValue = dtproduct.Rows[0]["QuotationKHID"];
                cbProject.EditValue = dtproduct.Rows[0]["ProjectID"];
                loadProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "checkDataStock");
            }
        }
        void genProductCode(List<string> lstcode, DataTable dtquo)
        {
            string result = MyLib.ListToStringSQL(lstcode);
            DataTable dt = TextUtils.Select($"Select ID from ProductSale where ProductNewCode in ({result})");
            for (int i = 0; i < lstcode.Count; i++)
            {
                TreeData.Nodes.Add();
                DataRow[] rows = dtProduct.Select($"ProductNewCode = '{lstcode[i]}'");
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string productCode = TextUtils.ToString(rows[0]["ProductCode"]);
                    int ID = TextUtils.ToInt(rows[0]["ID"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    string maker = TextUtils.ToString(rows[0]["Maker"]);
                    int IDQuota = TextUtils.ToInt(dtquo.Rows[i]["ID"]);
                    int Qty = TextUtils.ToInt(dtquo.Rows[i]["Qty"]);
                    int UnitPrice = TextUtils.ToInt(dtquo.Rows[i]["UnitPrice"]);
                    int IntoMoney = TextUtils.ToInt(dtquo.Rows[i]["IntoMoney"]);
                    string group = TextUtils.ToString(dtquo.Rows[i]["GroupQuota"]);
                    if (TreeData.AllNodesCount > 0)
                    {
                        TreeListNode node = TreeData.GetNodeByVisibleIndex(TreeData.AllNodesCount - 1);
                        TreeData.SetRowCellValue(node, colProductID, ID);
                        TreeData.SetRowCellValue(node, colProductName, productName);
                        TreeData.SetRowCellValue(node, colProductCode, productCode);
                        TreeData.SetRowCellValue(node, colUnit, unit);
                        TreeData.SetRowCellValue(node, colMaker, maker);
                        //TreeData.SetRowCellValue(node, colIDQuota, IDQuota);
                        TreeData.SetRowCellValue(node, colQty, Qty);
                        TreeData.SetRowCellValue(node, colUnitPrice, UnitPrice);
                        TreeData.SetRowCellValue(node, colIntoMoney, IntoMoney);
                        //TreeData.SetRowCellValue(node, colGroup, group);
                    }
                    else
                    {

                        TreeListNode node = TreeData.GetNodeByVisibleIndex(TreeData.AllNodesCount - 1);
                        TreeData.SetRowCellValue(node, colProductID, ID);
                        TreeData.SetRowCellValue(node, colProductName, productName);
                        TreeData.SetRowCellValue(node, colProductCode, productCode);
                        TreeData.SetRowCellValue(node, colUnit, unit);
                        TreeData.SetRowCellValue(node, colMaker, maker);
                        //TreeData.SetRowCellValue(node, colIDQuota, IDQuota);
                        TreeData.SetRowCellValue(node, colQty, Qty);
                        TreeData.SetRowCellValue(node, colUnitPrice, UnitPrice);
                        TreeData.SetRowCellValue(node, colIntoMoney, IntoMoney);
                        //TreeData.SetRowCellValue(node, colGroup, group);
                    }
                }
            }

        }

        private void ckType_CheckedChanged(object sender, EventArgs e)
        {

            grdDetailUser.Enabled = ckType.Checked;

            //if (ckType.Checked)
            //{
            //    grdDetailUser.Enabled = true;
            //}
            //else
            //{
            //    grdDetailUser.Enabled = false;
            //}
        }

        //private void TreeData_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        //{

        //    if (e.Column == colDelete_1)
        //    {
        //        if (MessageBox.Show("Bạn có chắc muốn xoá sản phẩm này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //        {
        //            //xoá nút cha
        //            TreeListNode nodeDele = TreeData.FocusedNode;
        //            if (nodeDele == null)returnl
        //            int id_Parent = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDele, colID_1));
        //            var listChild = TreeData.FindNodes((node) =>
        //            {
        //                return node.ParentNode == nodeDele;
        //            });
        //            if (listChild.Count() > 0)
        //            {
        //                //xoá nút con nếu có
        //                foreach (TreeListNode item in listChild)
        //                {
        //                    lstIDDelete.Add(TextUtils.ToInt(TreeData.GetRowCellValue(item, colID_1)));
        //                    TreeData.DeleteNode(item);
        //                }
        //            }

        //            lstIDDelete.Add(id_Parent);
        //            TreeData.DeleteNode(nodeDele);
        //            ds.Tables[0].AcceptChanges();
        //        }
        //    }

        //    //if (e.Column == colDelete_1)
        //    //{
        //    //    TreeListNode nodeDele = TreeData.FocusedNode;
        //    //    int id_Parent = TextUtils.ToInt(TreeData.GetRowCellValue(e.Node, colID_1));

        //    //    if (MessageBox.Show("Bạn có chắc muốn xoá sản phẩm này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //    //    {

        //    //        // xoá nút cha
        //    //        TreeData.ExpandAll();

        //    //        List<TreeListNode> listChild = new List<TreeListNode>();
        //    //        foreach (TreeListNode node in TreeData.GetNodeList())
        //    //        {
        //    //            int nodeDeleID = id_Parent;
        //    //            int id = TextUtils.ToInt(node.GetValue(colID_1));
        //    //            int childParentID = TextUtils.ToInt(node.GetValue(colParentID));
        //    //            if (TextUtils.ToInt(node.GetValue(colParentID)) == id_Parent)
        //    //            {
        //    //                listChild.Add(node);
        //    //            }
        //    //        }

        //    //        DeleteChildNode(nodeDele, listChild);

        //    //        lstIDDelete.Add(id_Parent);
        //    //        DataRow[] drr = ds.Tables[0].Select($"ID={id_Parent}");
        //    //        for (int i = 0; i < drr.Length; i++)
        //    //        {
        //    //            drr[i].Delete();
        //    //        }
        //    //        ds.Tables[0].AcceptChanges();
        //    //    }
        //    //}
        //}

        private void TreeData_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {

            try
            {
                if (e.Column == colDelete_1)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xoá sản phẩm này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //xoá nút cha
                        TreeListNode nodeDele = TreeData.FocusedNode;
                        if (nodeDele == null) return;
                        int id_Parent = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDele, colID_1));
                        //var listChild = TreeData.FindNodes((node) =>
                        //{
                        //    return node.ParentNode == nodeDele;
                        //});
                        var listChild = nodeDele.Nodes;
                        if (listChild.Count() > 0)
                        {
                            List<TreeListNode> childNodeDeleted = new List<TreeListNode>();
                            //xoá nút con nếu có
                            foreach (TreeListNode item in listChild)
                            {
                                int id = TextUtils.ToInt(TreeData.GetRowCellValue(item, colID_1));
                                //TreeData.DeleteNode(item);
                                childNodeDeleted.Add(item);
                                if (id <= 0) continue;
                                if (!lstIDDelete.Contains(id)) lstIDDelete.Add(id);
                            }

                            foreach (TreeListNode item in childNodeDeleted)
                            {
                                TreeData.DeleteNode(item);
                            }
                        }



                        lstIDDelete.Add(id_Parent);
                        TreeData.DeleteNode(nodeDele);
                        //ds.Tables[0].AcceptChanges();

                        //dtData.AcceptChanges();
                        //var dt = dtData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        void DeleteChildNode(TreeListNode nodeDele, List<TreeListNode> listChild)
        {

            if (listChild.Count() > 0)
            {
                foreach (TreeListNode item in listChild)
                {
                    int id = TextUtils.ToInt(TreeData.GetRowCellValue(item, colID_1));
                    List<TreeListNode> childs = new List<TreeListNode>();
                    foreach (TreeListNode node in TreeData.GetNodeList())
                    {
                        int nodeDeleID = TextUtils.ToInt(item.GetValue(colID_1));
                        int childParentID = TextUtils.ToInt(node.GetValue(colParentID));
                        if (TextUtils.ToInt(node.GetValue(colParentID)) == id)
                        {
                            childs.Add(node);
                        }
                    }

                    if (childs.Count() > 0)
                    {
                        DeleteChildNode(item, childs);
                    }
                    DataRow[] drr = dtData.Select($"ID={id}");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    dtData.AcceptChanges();
                    lstIDDelete.Add(id);

                }
            }
        }
        /// <summary>
        /// grvDetailUser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvDetailUser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetailUser.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column == colSTTUser && e.Y < 40)
                {
                    grvDetailUser.FocusedRowHandle = -1;
                    DataTable dt = (DataTable)grdDetailUser.DataSource;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    int stt = dt.Rows.Count;
                    int idMapping = TextUtils.ToInt(grvDetailUser.GetRowCellValue(stt - 1, colSTTUser));
                    dtrow["STT"] = stt + 1;
                    dt.Rows.Add(dtrow);
                    grdDetailUser.DataSource = dt;
                }
            }
        }
        void addNewRowUser()
        {
            grvDetailUser.AddNewRow();
            grvDetailUser.FocusedRowHandle = -1;
            TreeListNode nodefocus = TreeData.FocusedNode;
            int row = TextUtils.ToInt(TreeData.GetNodeIndex(nodefocus));
            grvDetailUser.SetRowCellValue(grvDetailUser.RowCount - 1, colRowHandle, row);
        }

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                TreeData.Focus();
                txtPOCode.Focus();
                int ID = TextUtils.ToInt(TreeData.GetFocusedRowCellValue(colProductID));
                DataRow[] rows = dtProduct.Select("ID = " + ID);
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string productcode = TextUtils.ToString(rows[0]["ProductCode"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    string maker = TextUtils.ToString(rows[0]["Maker"]);
                    TreeData.SetFocusedRowCellValue(colProductName, productName);
                    TreeData.SetFocusedRowCellValue(colProductCode, productcode);
                    TreeData.SetFocusedRowCellValue(colUnit, unit);
                    TreeData.SetFocusedRowCellValue(colMaker, maker);
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TreeData.FocusedNode = -1;
            checkStatus();
            if (saveData())
            {
                cPOStatus.AutoUpdateStatus(oPOKH.ID);
                loadData();
                this.DialogResult = DialogResult.OK;
            }
        }
        ArrayList lstDeleteIDDetail = new ArrayList();
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(grvDetailUser.GetFocusedRowCellValue(colIDDetailUser));
                if (id > 0) lstDeleteIDDetail.Add(id);
                grvDetailUser.DeleteSelectedRows();
                for (int i = 0; i < grvDetailUser.RowCount; i++)
                {
                    grvDetailUser.SetRowCellValue(i, colSTTUser, i + 1);
                }
            }

        }
        bool saveData()
        {
            try
            {
                //TreeData.FocusedColumn = colID_1;
                TreeData.CloseEditor();
                grvDetailUser.FocusedRowHandle = -1;
                if (!ValidateForm()) return false;
                //TreeData.FocusedRowHandle = -1;
                TreeData_FocusedNodeChanged(null, null);
                Recalcu();
                //Master
                if (lstDeleteIDDetail.Count > 0) POKHDetailMoneyBO.Instance.Delete(lstDeleteIDDetail);


                oPOKH.Status = cboStatus.SelectedIndex;
                oPOKH.UserID = TextUtils.ToInt(cboUser.EditValue);
                oPOKH.POCode = TextUtils.ToString(txtPOCode.Text);
                oPOKH.ReceivedDatePO = dtpPOdate.Value;
                oPOKH.TotalMoneyPO = TextUtils.ToDecimal(txtTotalPO.EditValue);
                oPOKH.TotalMoneyKoVAT = TextUtils.ToDecimal(TreeData.GetSummaryValue(colIntoMoney));
                oPOKH.Note = TextUtils.ToString(txtNote.Text);
                oPOKH.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                oPOKH.PartID = TextUtils.ToInt(cbPart.EditValue);
                oPOKH.ProjectID = TextUtils.ToInt(cbProject.EditValue);
                oPOKH.POType = TextUtils.ToInt(cbType.EditValue);
                oPOKH.NewAccount = ckbBigAccount.Checked;
                oPOKH.AccountType = radBigAccount.Checked ? 0 : (radMinorAccount.Checked ? 1 : 2);                 // =========================================PQ.Chien - UPDATE - 22/03/2025=================================================================
                oPOKH.Year = dtpPOdate.Value.Year;
                oPOKH.Month = dtpPOdate.Value.Month;
                oPOKH.EndUser = TextUtils.ToString(txtEndUser.Text);
                oPOKH.IsBill = IsBill;
                oPOKH.UserType = ckType.Checked ? 1 : 0;
                oPOKH.QuotationID = TextUtils.ToInt(txtQuotatiton.EditValue);
                oPOKH.PONumber = TextUtils.ToString(txtPONumber.Text);
                //oPOKH.IsMerge = TextUtils.ToBoolean(ckbMerge.Checked);

                oPOKH.WarehouseID = warehouseID;
                oPOKH.CurrencyID = TextUtils.ToInt(cboCurrency.EditValue);

                //=========== lee min khooi update 11/09/2024 ============================
                oPOKH.NewAccount = ckbBigAccount.Checked;

                oPOKH.Discount = TextUtils.ToDecimal(txtDiscount.Value); //NTA B update 041025
                oPOKH.TotalMoneyDiscount = TextUtils.ToDecimal(txtTotalMoneyDiscount.Value); //NTA B update 041025
                if (isCopy) oPOKH.ID = 0;

                /*if (ckbBigAccount.Checked == true)
                {
                    oPOKH.NewAccount = true;
                }
                else
                {
                    oPOKH.NewAccount = false;
                }*/



                if (oPOKH.ID > 0)
                {
                    SQLHelper<POKHModel>.Update(oPOKH);
                }
                else
                {
                    oPOKH.ID = SQLHelper<POKHModel>.Insert(oPOKH).ID;
                }



                List<TreeListNode> ListChildNode = new List<TreeListNode>();

                //Detail
                //DataTable dt = TextUtils.Select($"Select * from POKHDetailMoney where POKHID={oPOKH.ID}");
                // DataTable grv = (DataTable)TreeData.DataSource;
                // grv.AcceptChanges();




                foreach (TreeListNode node in TreeData.Nodes)
                {
                    SaveChildNode(node, 0);
                }


                //List<TreeListNode> allNodes = TreeData.GetNodeList();
                //int minLevel = allNodes.Count <= 0 ? 0 : allNodes.Min(node => node.Level);
                //for (int i = 0; i < TreeData.AllNodesCount; i++)
                //{
                //    TreeListNode nodeDetail = TreeData.GetNodeByVisibleIndex(i);
                //    if (nodeDetail == null || nodeDetail.Level != minLevel) continue;
                //    int id = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colID_1));
                //    if (ListChildNode.Contains(nodeDetail)) continue;
                //    POKHDetailModel detail = new POKHDetailModel();
                //    if (listIDInsert.Contains(id)) id = 0;
                //    if (id > 0 && !isCopy)
                //    {
                //        //detail = (POKHDetailModel)(POKHDetailBO.Instance.FindByPK(id));
                //        detail = SQLHelper<POKHDetailModel>.FindByID(id);
                //    }
                //    //detail.ID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colID_1));
                //    detail.STT = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colSTT));
                //    detail.POKHID = oPOKH.ID; //oPOKH.ID
                //    detail.ProductID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colProductID));
                //    detail.Qty = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colQty));
                //    detail.UnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colUnitPrice));
                //    detail.IntoMoney = detail.Qty * detail.UnitPrice;
                //    detail.FilmSize = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colFilmSize));
                //    detail.VAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colVAT));
                //    detail.BillNumber = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colBillNumber));
                //    detail.BillDate = TextUtils.ToDate2(TreeData.GetRowCellValue(nodeDetail, colBillDate));
                //    detail.TotalPriceIncludeVAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colTotalPriceIncludeVAT));
                //    detail.DeliveryRequestedDate = TextUtils.ToDate2(TreeData.GetRowCellValue(nodeDetail, colDeliveryRequestedDate));
                //    detail.PayDate = TextUtils.ToDate2(TreeData.GetRowCellValue(nodeDetail, colPayDate));
                //    detail.EstimatedPay = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colEstimatedPay));
                //    detail.QuotationDetailID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colIDQuota));
                //    detail.GuestCode = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colGuestCode));
                //    //detail.GroupPO = TextUtils.ToString(TreeData.GetRowCellValue(i, colGroup));
                //    detail.Debt = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colDebt));
                //    detail.UserReceiver = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colUserReceiver));
                //    detail.Note = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colNote));
                //    detail.NetUnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colNetUnitPrice));
                //    detail.ProjectPartListID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colProjectPartListID));

                //    detail.Spec = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colSpec));

                //    TextUtils.ExcuteSQL($"Update QuotationKHDetail set IsPO=1,POKHID={oPOKH.ID} where ID ={detail.QuotationDetailID}");
                //    if (detail.ID > 0)
                //    {
                //        POKHDetailBO.Instance.Update(detail);
                //    }
                //    else
                //    {
                //        detail.ID = TextUtils.ToInt(POKHDetailBO.Instance.Insert(detail));
                //    }

                //    //Find all NodeChild of nodeDetail
                //    FindTreeNode(detail, nodeDetail);

                //    //var childNode = TreeData.FindNodes((node) =>
                //    //{
                //    //    return node.ParentNode == nodeDetail;
                //    //}
                //    //);
                //    //if (childNode.Count() > 0)
                //    //{
                //    //    //ChildNode 
                //    //    foreach (TreeListNode item in childNode)
                //    //    {
                //    //        //if (item == nodeDetail) continue;
                //    //        ListChildNode.Add(item);
                //    //        int idChild = TextUtils.ToInt(TreeData.GetRowCellValue(item, colID_1));
                //    //        POKHDetailModel detailChild = new POKHDetailModel();

                //    //        if (idChild > 0 && !isCopy)
                //    //        {
                //    //            detailChild = (POKHDetailModel)(POKHDetailBO.Instance.FindByPK(idChild));
                //    //        }
                //    //        detailChild.ParentID = detail.ID;
                //    //        detailChild.STT = TextUtils.ToInt(TreeData.GetRowCellValue(item, colSTT));
                //    //        detailChild.POKHID = oPOKH.ID; //oPOKH.ID
                //    //        detailChild.ProductID = TextUtils.ToInt(TreeData.GetRowCellValue(item, colProductID));
                //    //        detailChild.Qty = TextUtils.ToInt(TreeData.GetRowCellValue(item, colQty));
                //    //        detailChild.UnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(item, colUnitPrice));
                //    //        detailChild.IntoMoney = detailChild.Qty * detailChild.UnitPrice;
                //    //        detailChild.FilmSize = TextUtils.ToString(TreeData.GetRowCellValue(item, colFilmSize));
                //    //        detailChild.VAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colVAT));
                //    //        detailChild.BillNumber = TextUtils.ToString(TreeData.GetRowCellValue(item, colBillNumber));
                //    //        detailChild.BillDate = TextUtils.ToDate2(TreeData.GetRowCellValue(item, colBillDate));
                //    //        detailChild.TotalPriceIncludeVAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colTotalPriceIncludeVAT));
                //    //        detailChild.DeliveryRequestedDate = TextUtils.ToDate2(TreeData.GetRowCellValue(item, colDeliveryRequestedDate));
                //    //        detailChild.PayDate = TextUtils.ToDate2(TreeData.GetRowCellValue(item, colPayDate));
                //    //        detailChild.EstimatedPay = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colEstimatedPay));
                //    //        detailChild.QuotationDetailID = TextUtils.ToInt(TreeData.GetRowCellValue(item, colIDQuota));
                //    //        detailChild.GuestCode = TextUtils.ToString(TreeData.GetRowCellValue(item, colGuestCode));
                //    //        //detail.GroupPO = TextUtils.ToString(TreeData.GetRowCellValue(i, colGroup));
                //    //        detailChild.Debt = TextUtils.ToInt(TreeData.GetRowCellValue(item, colDebt));
                //    //        detailChild.UserReceiver = TextUtils.ToString(TreeData.GetRowCellValue(item, colUserReceiver));
                //    //        detailChild.Note = TextUtils.ToString(TreeData.GetRowCellValue(item, colNote));
                //    //        detailChild.NetUnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(item, colNetUnitPrice));
                //    //        TextUtils.ExcuteSQL($"Update QuotationKHDetail set IsPO=1,POKHID={oPOKH.ID} where ID ={detailChild.QuotationDetailID}");
                //    //        if (detailChild.ID > 0)
                //    //        {
                //    //            POKHDetailBO.Instance.Update(detailChild);
                //    //        }
                //    //        else
                //    //        {
                //    //            detailChild.ID = TextUtils.ToInt(POKHDetailBO.Instance.Insert(detailChild));
                //    //        }

                //    //    }

                //    //}



                //}







                // ================== lee min khooi update 11/09 ====================================
                if (ckType.Checked)
                {
                    SQLHelper<POKHDetailMoneyModel>.DeleteByAttribute("POKHID", oPOKH.ID);
                    for (int j = 0; j < grvDetailUser.RowCount; j++)
                    {
                        int iduser = TextUtils.ToInt(grvDetailUser.GetRowCellValue(j, colFullName));
                        if (iduser <= 0) continue;
                        int poDeMoney = TextUtils.ToInt(grvDetailUser.GetRowCellValue(j, colIDDetailUser));
                        //int product = TextUtils.ToInt(lstDt[i].Rows[j]["RowHandle"]);
                        POKHDetailMoneyModel detailuser = new POKHDetailMoneyModel();


                        detailuser.POKHDetailID = 0;
                        detailuser.POKHID = oPOKH.ID;
                        detailuser.PercentUser = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(j, colPercentUser)); ;
                        detailuser.MoneyUser = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(j, colMoneyUser)); ;
                        //detailuser.RowHandle = TextUtils.ToInt(lstDt[i].Rows[j]["RowHandle"]);
                        detailuser.UserID = iduser;
                        detailuser.STT = TextUtils.ToInt(grvDetailUser.GetRowCellValue(j, colSTTUser));
                        detailuser.Month = TextUtils.ToInt(dtpPOdate.Value.Month);
                        detailuser.Year = TextUtils.ToInt(dtpPOdate.Value.Year);

                        POKHDetailMoneyBO.Instance.Insert(detailuser);
                    }
                }


                /* else
                 {
                     POKHDetailMoneyModel detailuser = new POKHDetailMoneyModel();
                     if (dt.Rows.Count > 0)
                     {
                         if (i <= dt.Rows.Count - 1)
                         {
                             int iduser = TextUtils.ToInt(dt.Rows[i]["ID"]);
                             if (iduser > 0)
                             {
                                 detailuser = (POKHDetailMoneyModel)(POKHDetailMoneyBO.Instance.FindByPK(iduser));
                             }
                         }
                     }
                     detailuser.POKHDetailID = detail.ID;
                     detailuser.POKHID = oPOKH.ID;
                     detailuser.PercentUser = TextUtils.ToDecimal(1);
                     detailuser.MoneyUser = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colIntoMoney));
                     detailuser.UserID = TextUtils.ToInt(cboUser.EditValue);
                     detailuser.Month = TextUtils.ToInt(dtpPOdate.Value.Month);
                     detailuser.Year = TextUtils.ToInt(dtpPOdate.Value.Year);
                     detailuser.RowHandle = TextUtils.ToInt(i);

                     if (detailuser.ID > 0)
                         POKHDetailMoneyBO.Instance.Update(detailuser);
                     else
                         POKHDetailMoneyBO.Instance.Insert(detailuser);

                 }*/

                if (lstIDDelete.Count > 0)
                {
                    try
                    {
                        POKHDetailBO.Instance.Delete(lstIDDelete);
                        foreach (int item in lstIDDelete)
                        {
                            if (item > 0)
                                POKHDetailMoneyBO.Instance.DeleteByAttribute("POKHIDDetail", item);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                UploadFile(oPOKH.ID);
                RemoveFile();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void FindTreeNode(POKHDetailModel detail, TreeListNode nodeDetail)
        {
            try
            {
                var childNode = TreeData.FindNodes((node) =>
                {
                    return node.ParentNode == nodeDetail;
                });

                if (childNode.Count() > 0)
                {
                    //ChildNode 
                    for (int i = 0; i < childNode.Count(); i++)
                    {
                        TreeListNode item = childNode[i];
                        ListChildNode.Add(item);
                        int idChild = TextUtils.ToInt(TreeData.GetRowCellValue(item, colID_1));
                        if (listIDInsert.Contains(idChild))
                        {
                            idChild = 0;
                        }
                        POKHDetailModel detailChild = new POKHDetailModel();

                        if (idChild > 0)
                        {
                            //detailChild = (POKHDetailModel)(POKHDetailBO.Instance.FindByPK(idChild));
                            detailChild = SQLHelper<POKHDetailModel>.FindByID(idChild);
                        }
                        detailChild.ParentID = detail.ID;
                        detailChild.STT = TextUtils.ToInt(TreeData.GetRowCellValue(item, colSTT));
                        //detailChild.TT = TextUtils.ToString(TreeData.GetRowCellValue(item, colTT)); //TODO: HuyNT 11/09/2024
                        detailChild.POKHID = oPOKH.ID; //oPOKH.ID
                        detailChild.ProductID = TextUtils.ToInt(TreeData.GetRowCellValue(item, colProductID));
                        detailChild.Qty = TextUtils.ToInt(TreeData.GetRowCellValue(item, colQty));
                        detailChild.UnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(item, colUnitPrice));
                        detailChild.IntoMoney = detailChild.Qty * detailChild.UnitPrice;
                        detailChild.FilmSize = TextUtils.ToString(TreeData.GetRowCellValue(item, colFilmSize));
                        detailChild.VAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colVAT));
                        detailChild.BillNumber = TextUtils.ToString(TreeData.GetRowCellValue(item, colBillNumber));
                        detailChild.BillDate = TextUtils.ToDate2(TreeData.GetRowCellValue(item, colBillDate));
                        detailChild.TotalPriceIncludeVAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colTotalPriceIncludeVAT));
                        detailChild.DeliveryRequestedDate = TextUtils.ToDate2(TreeData.GetRowCellValue(item, colDeliveryRequestedDate));
                        detailChild.PayDate = TextUtils.ToDate2(TreeData.GetRowCellValue(item, colPayDate));
                        detailChild.EstimatedPay = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colEstimatedPay));
                        detailChild.QuotationDetailID = TextUtils.ToInt(TreeData.GetRowCellValue(item, colIDQuota));
                        detailChild.GuestCode = TextUtils.ToString(TreeData.GetRowCellValue(item, colGuestCode));
                        //detail.GroupPO = TextUtils.ToString(TreeData.GetRowCellValue(i, colGroup));
                        detailChild.Debt = TextUtils.ToInt(TreeData.GetRowCellValue(item, colDebt));
                        detailChild.UserReceiver = TextUtils.ToString(TreeData.GetRowCellValue(item, colUserReceiver));
                        detailChild.Note = TextUtils.ToString(TreeData.GetRowCellValue(item, colNote));
                        detailChild.NetUnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(item, colNetUnitPrice));
                        TextUtils.ExcuteSQL($"Update QuotationKHDetail set IsPO=1,POKHID={oPOKH.ID} where ID ={detailChild.QuotationDetailID}");
                        if (detailChild.ID > 0)
                        {
                            POKHDetailBO.Instance.Update(detailChild);
                        }
                        else
                        {
                            detailChild.ID = TextUtils.ToInt(POKHDetailBO.Instance.Insert(detailChild));
                        }
                        indexNode++;
                        if (i == childNode.Count() - 1) continue;
                        FindTreeNode(detailChild, childNode[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }
        void Recalcu()
        {
            DataTable dt = TreeData.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
                decimal vat = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colVAT));
                decimal thanhtien = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colIntoMoney));
                TreeData.SetRowCellValue(node, colTotalPriceIncludeVAT, thanhtien + (thanhtien * (vat / 100)));
            }
        }

        public async void UploadFile(int poKHID)
        {
            try
            {
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "POKH").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                POKHModel po = SQLHelper<POKHModel>.FindByID(poKHID);
                if (po == null || po.ID <= 0) return;
                //if (po.UserID != Global.UserID) return;

                string pathServer = config.KeyValue.Trim();
                if (string.IsNullOrWhiteSpace(pathServer)) return;
                if (warehouseID == 1) pathServer = pathServer.Replace("Sales HCM", "PO sales MRO base");
                string pathPattern = $@"{po.PONumber}";
                //pathPattern = $@"TEST";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();
                //var content = new MultipartFormDataContent();

                List<POKHFileModel> listFiles = new List<POKHFileModel>();
                foreach (var file in listFileUpload)
                {
                    POKHFileModel filePO = new POKHFileModel();
                    filePO.POKHID = po.ID;
                    filePO.FileName = file.Name;
                    filePO.OriginPath = file.DirectoryName;
                    filePO.ServerPath = pathUpload;

                    if (file.Length < 0) continue;
                    var fileStream = new FileStream(file.FullName, FileMode.Open);
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<POKHFileModel>.Insert(filePO);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        public async void RemoveFile()
        {
            if (listFileDelete.Count <= 0) return;
            var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
            //var url = $"http://localhost:8390/api/Home/removefile?path=";
            var client = new HttpClient();
            foreach (var item in listFileDelete)
            {
                url += $@"{item.ServerPath}\{item.FileName}";
                var result = await client.GetAsync(url);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                }
                SQLHelper<POKHFileModel>.Delete(item);
            }
        }

        private void TreeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            if (cGlobVar.LockEvents) return;


            //if (TreeData.AllNodesCount != lstDt.Count)
            //{
            //    DataTable dt = (DataTable)grdDetailUser.DataSource;
            //    if (!lstDt.Contains(dt))
            //    {
            //        lstDt.Add(dt);
            //    }
            //}

            // lee min khooi update 11/09/2024
            //try
            //{
            //    //loadDetailUser();
            //    //if (e != null)
            //    //   grdDetailUser.DataSource = lstDt[e.FocusedRowHandle];
            //}
            //catch { }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                loadCode();
                oPOKH = new POKHModel();
                cboStatus.SelectedIndex = -1;
                txtNote.Clear();
                txtTotalPO.EditValue = "";
                cboCustomer.Text = "";
                cboUser.Text = "";
                cbProject.EditValue = 0;
                ckbBigAccount.Checked = false;
                lstIDDelete.Clear();
                for (int i = TreeData.AllNodesCount - 1; i >= 0; i--)
                {
                    TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
                    TreeData.DeleteNode(node);
                }
            }

        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadCode();
            loadCbPart();
        }

        private void btnPart_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboCustomer.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmCustomerPart frm = new frmCustomerPart();
            frm.IDCutomer = TextUtils.ToInt(cboCustomer.EditValue);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCbPart();
            }
        }
        void ReloadCbProduct(string signal)
        {
            loadProduct();
        }
        frmProductSale frm;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            frm = new frmProductSale();
            frm.GetSignal += ReloadCbProduct;
            frm.Show();
        }
        void caculMonmey()
        {
            if (cGlobVar.LockEvents) return;
            for (int i = 0; i < grvDetailUser.RowCount; i++)
            {
                decimal total = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colIntoMoney));
                decimal Per = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(i, colPercentUser));
                grvDetailUser.SetRowCellValue(i, colMoneyUser, total * Per);
            }

        }
        private void TreeData_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (cGlobVar.LockEvents) return;


            TreeListNode node = e.Node;
            // if (cGlobVar.LockEvents) return;
            decimal soluong = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colQty));
            decimal dongia = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colUnitPrice));
            decimal vat = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colVAT));
            string billDate = TextUtils.ToString(TreeData.GetFocusedRowCellValue(colBillDate));
            int debt = TextUtils.ToInt(TreeData.GetFocusedRowCellValue(colDebt));
            decimal thanhtien = soluong * dongia;


            try
            {
                decimal qty = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colQty));
                decimal unitprice = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colUnitPrice));

                if (dongia >= 0 && soluong > 0)
                {
                    if (e.Column == colQty || e.Column == colUnitPrice /*|| e.Column == colVAT*/)
                    {
                        //int a = e.RowHandle;
                        TreeData.SetFocusedRowCellValue(colIntoMoney, thanhtien);
                        TreeData.SetFocusedRowCellValue(colTotalPriceIncludeVAT, thanhtien + thanhtien * (vat / 100));
                        calculateTotal();
                    }

                }

                if (e.Column == colIntoMoney)
                    caculMonmey();
                if (e.Column == colVAT)
                {

                    decimal soluongVAT = 0;
                    decimal dongiaVAT = 0;
                    decimal thanhtienVAT = 0;
                    decimal totalPriceIncludeVAT = 0;

                    foreach (TreeListNode item in TreeData.Nodes)
                    {
                        decimal vatold = TextUtils.ToDecimal(TreeData.GetRowCellValue(item, colVAT));
                        string vatoldtext = TextUtils.ToString(TreeData.GetRowCellValue(item, colVAT));
                        if (vatold == 0 && vat != 0 && vatoldtext == "")
                        {
                            item.SetValue(colVAT, vat);

                            //TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
                            //decimal vat = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colVAT));
                            //decimal thanhtien = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colIntoMoney));

                            soluongVAT = TextUtils.ToDecimal(item.GetValue(colQty.FieldName));
                            dongiaVAT = TextUtils.ToDecimal(item.GetValue(colUnitPrice.FieldName));
                            thanhtienVAT = soluongVAT * dongiaVAT;
                            totalPriceIncludeVAT = thanhtienVAT + (thanhtienVAT * (vat / 100));
                            item.SetValue(colTotalPriceIncludeVAT, totalPriceIncludeVAT);
                        }
                        //TreeData.SetRowCellValue(item, colVAT, vat);

                    }

                    soluongVAT = TextUtils.ToDecimal(node.GetValue(colQty.FieldName));
                    dongiaVAT = TextUtils.ToDecimal(node.GetValue(colUnitPrice.FieldName));
                    thanhtienVAT = soluongVAT * dongiaVAT;
                    totalPriceIncludeVAT = thanhtienVAT + (thanhtienVAT * (vat / 100));
                    node.SetValue(colTotalPriceIncludeVAT, totalPriceIncludeVAT);
                }
                if (e.Column == colBillDate || e.Column == colDebt)
                {
                    TreeData.SetFocusedRowCellValue(colPayDate, TextUtils.ToDate(billDate).AddDays(debt));
                }

                if (e.Column == colQty || e.Column == colUnitPrice)
                {
                    TreeData.SetFocusedRowCellValue(colIntoMoney, qty * unitprice);
                    calculateTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnNewProjetc_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        private void TreeData_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {

            if (e.Node != null && e.Node.ParentNode == null)
            {
                e.Appearance.BackColor = Color.Gainsboro;
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
        }



        private void btnChosenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    POKHFileModel fileRequest = new POKHFileModel()
                    {
                        FileName = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName
                    };

                    listFiles.Insert(0, fileRequest);
                    listFileUpload.Add(fileInfo);
                }
                LoadFile(listFiles);
            }

        }
        void LoadFile(List<POKHFileModel> listFiles)
        {
            grdDataFile.DataSource = listFiles;
            grvDataFile.RefreshData();
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {

            int id = TextUtils.ToInt(grvDataFile.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvDataFile.DeleteSelectedRows();
                if (id <= 0) return;
                POKHFileModel file = SQLHelper<POKHFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TreeData_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    var rowfocus = TreeData.FocusedNode;
            //    TreeData.FocusedNode = rowfocus.NextNode;
            //}
            //if (e.KeyCode == Keys.C && e.Control)
            //{
            //    TreeData.CopyToClipboard();
            //}
            //else if (e.KeyCode == Keys.V && e.Control)
            //{
            //    if (TreeData.FocusedColumn == colProductID)
            //    {
            //        try
            //        {
            //            cGlobVar.LockEvents = true;
            //            string[] arChar = { "\r\n" };
            //            List<string> lstCode = Clipboard.GetText().Split(arChar, StringSplitOptions.None).ToList();
            //            if (!lstCode.Any()) return;
            //            if (lstCode[lstCode.Count - 1] == "")
            //                lstCode.RemoveAt(lstCode.Count - 1);
            //            DataTable dt = (DataTable)cbProduct.DataSource;
            //            for (int i = 0; i < lstCode.Count; i++)
            //            {
            //                int rowCount = TreeData.Nodes.Count - (TreeData.FocusedNode. + 1);
            //                if (lstCode.Count > rowCount + 1)AddNewRow();
            //                DataRow[] dtr = dt.Select($"ProductNewCode='{lstCode[i]}'");
            //                if (dtr.Length == 0) return;
            //                grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductID, dtr[0]["ID"]);
            //                grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductName, dtr[0]["ProductName"]);
            //                grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductCode, dtr[0]["ProductCode"]);
            //                grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colUnit, dtr[0]["Unit"]);
            //                grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colMaker, dtr[0]["Maker"]);
            //            }
            //        }
            //        finally
            //        {
            //            e.Handled = true;
            //            cGlobVar.LockEvents = false;
            //        }
            //    }
            //    else
            //    {

            //        int[] selectedRow = grvData.GetSelectedRows();
            //        GridCell[] selectedColumn = grvData.GetSelectedCells();

            //        List<GridColumn> listCol = new List<GridColumn>();

            //        for (int i = 0; i < selectedColumn.Length; i++)
            //        {
            //            GridColumn colSelect = selectedColumn[i].Column;
            //            listCol.Add(colSelect);
            //        }

            //        string[] separator = { "\r\n" };
            //        var data = Clipboard.GetText();

            //        List<string> listDataClipboard = Clipboard.GetText().Split(separator, StringSplitOptions.None).ToList();
            //        foreach (string item in listDataClipboard.ToList())
            //        {
            //            if (string.IsNullOrEmpty(item))
            //            {
            //                listDataClipboard.Remove(item);
            //            }
            //        }

            //        if (listDataClipboard.Count <= 1)
            //        {

            //            if (selectedRow.Length > 1 || selectedColumn.Length > 1)
            //            {
            //                for (int i = 0; i < selectedRow.Length; i++)
            //                {
            //                    for (int j = 0; j < selectedColumn.Length; j++)
            //                    {
            //                        grvData.SetRowCellValue(i, selectedColumn[j].Column, listDataClipboard[0]);
            //                    }

            //                }
            //            }
            //            else
            //            {
            //                grvData.SetRowCellValue(grvData.FocusedRowHandle, selectedColumn[0].Column, listDataClipboard[0]);
            //            }
            //        }
            //        else
            //        {
            //            grvData.FocusedColumn = selectedColumn[0].Column;
            //            grvData.FocusedRowHandle = selectedRow[0];

            //            grvData.PasteFromClipboard();
            //        }
            //    }
            //}
        }

        //===================== lee min khooi update 11/09/2024 ===========================================================
        private void grvDetailUser_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colPercentUser)
            {
                decimal totalPO = TextUtils.ToDecimal(txtTotalPO.EditValue);
                /*for (int i = 0; i < grvDetailUser.RowCount; i++)
                {
                    decimal totalPercent = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(i, colPercentUser));
                    grvDetailUser.SetRowCellValue(i, colMoneyUser, totalPO * totalPercent);
                }*/
                decimal totalPercent = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(e.RowHandle, colPercentUser));
                grvDetailUser.SetRowCellValue(e.RowHandle, colMoneyUser, totalPO * totalPercent);
            }
        }

        private void txtTotalPO_EditValueChanged(object sender, EventArgs e)
        {
            decimal totalPO = TextUtils.ToDecimal(txtTotalPO.EditValue);
            for (int i = 0; i < grvDetailUser.RowCount; i++)
            {
                decimal totalPercent = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(i, colPercentUser));
                grvDetailUser.SetRowCellValue(i, colMoneyUser, totalPO * totalPercent);
            }

            decimal moneyDiscount = TextUtils.ToDecimal(txtTotalPO.EditValue) * TextUtils.ToDecimal(txtDiscount.Value) / 100; //NTA B update 041025

            txtTotalMoneyDiscount.Value = TextUtils.ToDecimal(txtTotalPO.EditValue) - moneyDiscount; //NTA B update 041025
        }

        private void btnAddPartlist_Click(object sender, EventArgs e)
        {
            try
            {
                dtData.AcceptChanges();
                if (dtOld.Rows.Count <= 0)
                {
                    DataTable dtTreeData = TreeData.DataSource as DataTable;
                    dtOld = dtTreeData.Copy();
                }
                int projectID = Lib.ToInt(cbProject.EditValue);
                if (projectID <= 0)
                {
                    MessageBox.Show("Xin hãy chọn Dự án.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                dtOld.AcceptChanges();

                ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
                frmProjectPartList_New frm = new frmProjectPartList_New(false);
                frm.btnSelectProductPO.Visible = true;
                frm.splitContainerControl5.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;

                frm.dtAddDetail = dtOld.Copy();
                frm.project = project;
                frm.isSelectPartlist = true;
                int nodeMinLevelCount = 0;
                List<TreeListNode> allNodes = TreeData.GetNodeList();
                int minLevel = allNodes.Count > 0 ? allNodes.Min(node => node.Level) : 0;
                foreach (TreeListNode item in allNodes)
                {
                    if (item.Level == minLevel)
                    {
                        nodeMinLevelCount++;
                    }
                }
                frm.nodeMinLevelCount = nodeMinLevelCount;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    listIDInsert = frm.listIDInsert;
                    TreeData.Refresh();
                    if (frm.dtAddDetail.Rows.Count <= 0) return;
                    // TreeData.DataSource = frm.dtAddDetail;
                    dtData.Clear();
                    foreach (DataRow row in frm.dtAddDetail.Rows)
                    {
                        dtData.ImportRow(row);
                    }

                    dtData.AcceptChanges();
                    TreeData.RefreshDataSource();
                    //ds.Tables.Clear();
                    //ds.Tables.Add(frm.dtAddDetail);
                }

                TreeData.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
                DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();


                string productName = "";
                string productcode = "";
                string unit = "";
                string maker = "";
                //string guestCode = "";

                if (dataRow != null)
                {
                    productName = TextUtils.ToString(dataRow["ProductName"]);
                    productcode = TextUtils.ToString(dataRow["ProductCode"]);
                    unit = TextUtils.ToString(dataRow["Unit"]);
                    maker = TextUtils.ToString(dataRow["Maker"]);
                }

                TreeData.SetFocusedRowCellValue(colProductName, productName);
                TreeData.SetFocusedRowCellValue(colProductCode, productcode);
                TreeData.SetFocusedRowCellValue(colUnit, unit);
                TreeData.SetFocusedRowCellValue(colMaker, maker);
                if (warehouseID == 2) TreeData.SetFocusedRowCellValue(colGuestCode, productcode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        //private void btnImportExcel_Click(object sender, EventArgs e)
        //{
        //    frmPOKHDetailImportExcel frm = new frmPOKHDetailImportExcel();
        //    if (dtClone.Rows.Count > 0) dtClone.Clear();
        //    if (TreeData.Nodes.Count > 0) TreeData.Nodes.Clear();
        //    frm.idPOKH = oPOKH.ID;
        //    frm.dtClone = dtClone;
        //    frm.ShowDialog();
        //    #region
        //    try
        //    {
        //        using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo các Node..."))
        //        {
        //            cbProduct.EditValueChanged -= cbProduct_EditValueChanged;
        //            dtClone = frm.dtClone;
        //            Dictionary<string, TreeListNode> parentNodes = new Dictionary<string, TreeListNode>();

        //            foreach (DataRow row in dtClone.Rows)
        //            {
        //                int stt = Convert.ToInt32(row["STT"]);
        //                string tt = row["TT"].ToString();

        //                int productID = TextUtils.ToInt(row["ProductID"]);
        //                string productNewCode = row["ProductNewCode"].ToString();
        //                string productGroupID = row["ProductGroupID"].ToString();
        //                string productCode = row["ProductCode"].ToString();
        //                string productName = row["ProductName"].ToString();
        //                string guestCode = row["GuestCode"].ToString();
        //                string maker = row["Maker"].ToString();
        //                string unit = row["Unit"].ToString();
        //                string spec = row["Spec"].ToString();
        //                decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
        //                decimal qty = TextUtils.ToDecimal(row["Qty"]);


        //                if (string.IsNullOrEmpty(tt))
        //                {
        //                    if (!parentNodes.ContainsKey(stt.ToString()))
        //                    {
        //                        TreeListNode parentNode = TreeData.AppendNode(new object[]
        //                        {
        //                            stt,
        //                            "",
        //                            productID,
        //                            productNewCode,
        //                            productGroupID,
        //                            productCode,
        //                            productName,
        //                            guestCode,
        //                            maker,
        //                            unit,
        //                            spec,
        //                            unitPrice,
        //                            qty
        //                            }, null);

        //                        parentNode[$"{colSTT.FieldName}"] = stt;
        //                        parentNode[$"{colProductID.FieldName}"] = productID;
        //                        parentNode[$"{colProductCode.FieldName}"] = productCode;
        //                        parentNode[$"{colProductName.FieldName}"] = productName;
        //                        parentNode[$"{colMaker.FieldName}"] = maker;
        //                        parentNode[$"{colUnit.FieldName}"] = unit;
        //                        parentNode[$"{colSpec.FieldName}"] = spec;
        //                        parentNode[$"{colUnitPrice.FieldName}"] = unitPrice;
        //                        parentNode[$"{colGuestCode.FieldName}"] = guestCode;
        //                        parentNode[$"{colQty.FieldName}"] = qty;
        //                        parentNodes[stt.ToString()] = parentNode;
        //                    }

        //                }
        //                else
        //                {
        //                    string parentTT = tt.Substring(0, tt.LastIndexOf("."));
        //                    TreeListNode parentNode = FindNodeByTT(TreeData.Nodes, parentTT);

        //                    if (parentNode != null)
        //                    {
        //                        TreeListNode childNode = TreeData.AppendNode(new object[]
        //                        {
        //                        stt,
        //                        tt,
        //                        productID,
        //                        productNewCode,
        //                        productGroupID,
        //                        productCode,
        //                        productName,
        //                        guestCode,
        //                        maker,
        //                        unit,
        //                        spec,
        //                        unitPrice,
        //                        qty
        //                        }, parentNode);

        //                        childNode[$"{colSTT.FieldName}"] = stt;
        //                        childNode[$"{colProductID.FieldName}"] = productID;
        //                        childNode[$"{colProductCode.FieldName}"] = productCode;
        //                        childNode[$"{colProductName.FieldName}"] = productName;
        //                        childNode[$"{colMaker.FieldName}"] = maker;
        //                        childNode[$"{colUnit.FieldName}"] = unit;
        //                        childNode[$"{colSpec.FieldName}"] = spec;
        //                        childNode[$"{colNetUnitPrice.FieldName}"] = unitPrice;
        //                        childNode[$"{colGuestCode.FieldName}"] = guestCode;
        //                        childNode[$"{colQty.FieldName}"] = qty;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.ToString()}");
        //    }
        //    #endregion

        //    cbProduct.EditValueChanged += cbProduct_EditValueChanged;
        //    TreeData.ExpandAll();
        //}

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmPOKHDetailImportExcel frm = new frmPOKHDetailImportExcel();
            if (dtClone.Rows.Count > 0) dtClone.Clear();
            if (TreeData.Nodes.Count > 0) TreeData.Nodes.Clear();
            frm.idPOKH = oPOKH.ID;
            frm.dtClone = dtClone;
            frm.ShowDialog();
            #region
            try
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo các Node..."))
                {
                    cbProduct.EditValueChanged -= cbProduct_EditValueChanged;
                    dtClone = frm.dtClone;
                    Dictionary<string, TreeListNode> parentNodes = new Dictionary<string, TreeListNode>();

                    foreach (DataRow row in dtClone.Rows)
                    {
                        int stt = Convert.ToInt32(row["STT"]);
                        string tt = row["TT"].ToString();

                        int productID = TextUtils.ToInt(row["ProductID"]);
                        string productNewCode = row["ProductNewCode"].ToString();
                        string productGroupID = row["ProductGroupID"].ToString();
                        string productCode = row["ProductCode"].ToString();
                        string productName = row["ProductName"].ToString();
                        string guestCode = row["GuestCode"].ToString();
                        string maker = row["Maker"].ToString();
                        string unit = row["Unit"].ToString();
                        string spec = row["Spec"].ToString();
                        decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                        decimal qty = TextUtils.ToDecimal(row["Qty"]);


                        if (!tt.Contains("."))
                        {
                            if (!parentNodes.ContainsKey(stt.ToString()))
                            {
                                TreeListNode parentNode = TreeData.AppendNode(new object[]
                                {
                                    stt,
                                    "",
                                    productID,
                                    productNewCode,
                                    productGroupID,
                                    productCode,
                                    productName,
                                    guestCode,
                                    maker,
                                    unit,
                                    spec,
                                    unitPrice,
                                    qty
                                    }, null);

                                parentNode[$"{colSTT.FieldName}"] = stt;
                                parentNode[$"{colProductID.FieldName}"] = productID;
                                parentNode[$"{colProductCode.FieldName}"] = productCode;
                                parentNode[$"{colProductName.FieldName}"] = productName;
                                parentNode[$"{colMaker.FieldName}"] = maker;
                                parentNode[$"{colUnit.FieldName}"] = unit;
                                parentNode[$"{colSpec.FieldName}"] = spec;
                                parentNode[$"{colUnitPrice.FieldName}"] = unitPrice;
                                parentNode[$"{colGuestCode.FieldName}"] = guestCode;
                                parentNode[$"{colQty.FieldName}"] = qty;
                                parentNode[$"{colIntoMoney.FieldName}"] = qty * unitPrice;
                                parentNodes[tt] = parentNode;
                            }

                        }
                        else
                        {
                            string parentTT = tt.Substring(0, tt.LastIndexOf("."));
                            if (parentNodes.TryGetValue(parentTT, out TreeListNode prNode))
                            {
                                TreeListNode childNode = TreeData.AppendNode(new object[]
                                {
                                stt,
                                tt,
                                productID,
                                productNewCode,
                                productGroupID,
                                productCode,
                                productName,
                                guestCode,
                                maker,
                                unit,
                                spec,
                                unitPrice,
                                qty
                                }, prNode);

                                childNode[$"{colSTT.FieldName}"] = stt;
                                childNode[$"{colProductID.FieldName}"] = productID;
                                childNode[$"{colProductCode.FieldName}"] = productCode;
                                childNode[$"{colProductName.FieldName}"] = productName;
                                childNode[$"{colMaker.FieldName}"] = maker;
                                childNode[$"{colUnit.FieldName}"] = unit;
                                childNode[$"{colSpec.FieldName}"] = spec;
                                childNode[$"{colNetUnitPrice.FieldName}"] = unitPrice;
                                childNode[$"{colGuestCode.FieldName}"] = guestCode;
                                childNode[$"{colQty.FieldName}"] = qty;
                                childNode[$"{colIntoMoney.FieldName}"] = qty * unitPrice;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()}");
            }
            #endregion

            cbProduct.EditValueChanged += cbProduct_EditValueChanged;
            TreeData.ExpandAll();
        }

        //private TreeListNode FindNodeByTT(TreeListNodes nodes, string parentTT)
        //{
        //    foreach (TreeListNode node in nodes)
        //    {
        //        string nodeTT = node.GetValue("TT").ToString();
        //        if (nodeTT == parentTT)
        //        {
        //            return node;
        //        }

        //        // Tìm node trong các node con
        //        TreeListNode foundNode = FindNodeByTT(node.Nodes, parentTT);
        //        if (foundNode != null)
        //        {
        //            return foundNode;
        //        }
        //    }
        //    return null;
        //}


        //================================================= 30/12/2024 =================================================
        private int SaveNode(TreeListNode nodeDetail, int parentID)
        {

            if (nodeDetail == null) return parentID;
            int id = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colID_1));
            POKHDetailModel detail = new POKHDetailModel();
            if (listIDInsert.Contains(id)) id = 0;
            if (id > 0 && !isCopy)
            {
                detail = SQLHelper<POKHDetailModel>.FindByID(id);
            }
            detail.ParentID = parentID;
            detail.STT = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colSTT));
            detail.POKHID = oPOKH.ID; //oPOKH.ID
            detail.ProductID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colProductID));
            detail.Qty = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colQty));
            detail.UnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colUnitPrice));
            detail.IntoMoney = detail.Qty * detail.UnitPrice;
            detail.FilmSize = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colFilmSize));
            detail.VAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colVAT));
            detail.BillNumber = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colBillNumber));
            detail.BillDate = TextUtils.ToDate2(TreeData.GetRowCellValue(nodeDetail, colBillDate));
            detail.TotalPriceIncludeVAT = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colTotalPriceIncludeVAT));
            detail.DeliveryRequestedDate = TextUtils.ToDate2(TreeData.GetRowCellValue(nodeDetail, colDeliveryRequestedDate));
            detail.PayDate = TextUtils.ToDate2(TreeData.GetRowCellValue(nodeDetail, colPayDate));
            detail.EstimatedPay = TextUtils.ToDecimal(TreeData.GetRowCellValue(nodeDetail, colEstimatedPay));
            detail.QuotationDetailID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colIDQuota));
            detail.GuestCode = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colGuestCode));
            detail.Debt = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colDebt));
            detail.UserReceiver = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colUserReceiver));
            detail.Note = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colNote));
            detail.NetUnitPrice = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colNetUnitPrice));
            detail.ProjectPartListID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colProjectPartListID));
            detail.Spec = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colSpec));

            TextUtils.ExcuteSQL($"Update QuotationKHDetail set IsPO=1,POKHID={oPOKH.ID} where ID ={detail.QuotationDetailID}");
            if (detail.ID > 0)
            {

                //var result = SQLHelper<POKHDetailModel>.Update(detail);
                SQLHelper<POKHDetailModel>.Update(detail);
                parentID = detail.ID;
            }
            else parentID = detail.ID = SQLHelper<POKHDetailModel>.Insert(detail).ID;

            return parentID;
        }
        private void SaveChildNode(TreeListNode nodeDetail, int parentID)
        {
            if (nodeDetail == null) return;
            string test = TextUtils.ToString(TreeData.GetRowCellValue(nodeDetail, colGuestCode));

            parentID = SaveNode(nodeDetail, parentID);
            if (nodeDetail.Nodes.Count > 0)
            {
                foreach (TreeListNode node in nodeDetail.Nodes)
                {
                    SaveChildNode(node, parentID);
                }
            }
        }

        // =========================================PQ.Chien - ADD - 22/03/2025=================================================================

        private void gridView7_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            int mainGroup = TextUtils.ToInt(gridView7.GetRowCellValue(e.ListSourceRow, colMainGroup));
            if (mainGroup == 2 || mainGroup == 4)
            {
                e.Visible = true;
                e.Handled = true;
            }
        }

        private void gridView7_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) // Đảm bảo chỉ xử lý các dòng có dữ liệu
            {
                int mainGroup = TextUtils.ToInt(gridView7.GetRowCellValue(e.RowHandle, colMainGroup));
                if (mainGroup == 2 || mainGroup == 4)
                {
                    e.Appearance.BackColor = Color.LightGray; // Đổi màu nền để biểu thị không chọn được
                    //e.HighPriority = false; // Ngăn làm nổi bật dòng
                }
            }
        }

        private void cbType_Properties_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            if (e.Value != null)
            {
                var view = edit.Properties.View;
                int mainGroup = TextUtils.ToInt(view.GetFocusedRowCellValue(colMainGroup));
                if (mainGroup == 2 || mainGroup == 4)
                {
                    e.AcceptValue = false; // Từ chối giá trị được chọn
                    MessageBox.Show("Không thể chọn giá trị từ nhóm 2 hoặc 4.");
                }
            }
        }

        private void cboUser_Validated(object sender, EventArgs e)
        {
            //return;

        }

        private void cboUser_Properties_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            //SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            //var rowSelected = lookUpEdit.GetSelectedDataRow();


            //if (rowSelected == null)
            //{
            //    lookUpEdit.EditValue = 0;
            //    return;
            //}
            //int teamType = TextUtils.ToInt(rowSelected.GetType().GetProperty("TeamType").GetValue(rowSelected));
            //if (teamType == 1)
            //{
            //    MessageBox.Show("Bạn không thể chọn nhân viên từ nhóm team cũ!", "Thông báo");
            //    lookUpEdit.EditValue = 0;
            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtDiscount_ValueChanged(object sender, EventArgs e)
        {
            decimal moneyDiscount = TextUtils.ToDecimal(txtTotalPO.EditValue) * TextUtils.ToDecimal(txtDiscount.Value) / 100;

            txtTotalMoneyDiscount.Value = TextUtils.ToDecimal(txtTotalPO.EditValue) - moneyDiscount;
        }

        private void cboUser_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            if (e.Value != null)
            {
                var view = edit.Properties.View;
                int teamType = TextUtils.ToInt(view.GetFocusedRowCellValue(gridColumn25));
                if (teamType == 1)
                {
                    e.AcceptValue = false; // Từ chối giá trị được chọn
                    MessageBox.Show("Không thể chọn nhân viên từ team cũ");
                }
            }
        }

        private void gridView6_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) // Đảm bảo chỉ xử lý các dòng có dữ liệu
            {
                int mainGroup = TextUtils.ToInt(gridView6.GetRowCellValue(e.RowHandle, gridColumn25));
                if (mainGroup == 1)
                {
                    e.Appearance.BackColor = Color.LightGray; // Đổi màu nền để biểu thị không chọn được
                    e.HighPriority = false; // Ngăn làm nổi bật dòng
                }
            }
        }

        private void gridView6_GroupRowExpanding(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            //GridView view = sender as GridView;

            //int teamType = Convert.ToInt32(view.GetGroupRowValue(e.RowHandle, view.Columns["TeamType"]));
            //if (teamType == 1)
            //{
            //    e.Allow = false;
            //}
        }
    }
}
