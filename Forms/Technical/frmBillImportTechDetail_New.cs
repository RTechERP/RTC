using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
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
    public partial class frmBillImportTechDetail_New : _Forms
    {
        public bool IsEdit;
        public int IDDetail;
        public BillImportTechnicalModel billImport = new BillImportTechnicalModel();
        List<BillImportTechDetailSerialModel> lstSerial = new List<BillImportTechDetailSerialModel>();
        bool isSave = false;//ndnhat 02/04/2025
        ArrayList listIdDelete = new ArrayList();
        DataTable dtProduct = new DataTable();
        string status = "";
        public int warehouseID;
        public int warehouseIDNew;

        int check = 0;

        public DataRow[] dtDetails;
        public int flag;
        public string POCode;

        //Email content
        public string subject = "";
        public string body = "";
        public int receiverMailID = 0;


        public string idPONCCText = "";
        public int poNCCId = 0;
        List<string> oldTempSerial = new List<string>();//ndnhat 02/04/2025
        List<string> tempSerial = new List<string>();//ndnhat 02/04/2025

        public frmBillImportTechDetail_New()
        {
            InitializeComponent();
        }
        public frmBillImportTechDetail_New(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmBillImportTechDetail_Load(object sender, EventArgs e)
        {
            cboBillTypeNew.SelectedIndex = 0;
            txtBillCode.ReadOnly = true;
            if (IsEdit)
            {
                btnCloseAndSave.Enabled = btnSaveNew.Enabled = btnAddProduct.Enabled = toolStripButton1.Enabled = false;
            }

            if (billImport.Status == true && !Global.IsAdmin)
            {
                btnCloseAndSave.Enabled = btnSaveNew.Enabled = btnAddProduct.Enabled = toolStripButton1.Enabled = false;
            }
            cboReceiver.EditValue = Global.UserID;
            if (warehouseID == 2) cboReceiver.EditValue = 1434;

            LoadNCC();
            loadUser();
            LoadProduct();
            LoadCustomer();
            loadWarehouse();
            LoadDocumentImport();
            LoadEmployeeBorrow();
            LoadRulePay();
            LoadStatus();
            loadBillImportDetail();

        }

        private void LoadDocumentImport()
        {
            DataTable data = TextUtils.LoadDataFromSP("spGetAllDocumentImportPONCCTechnical", "A", new string[] { "@BillImportTechnicalID" }, new object[] { billImport.ID });
            grdDocImport.DataSource = data;
        }
        private void loadWarehouse()
        {
            List<WarehouseModel> listWarehouse = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouse.Properties.DataSource = listWarehouse;
            cboWarehouse.Properties.DisplayMember = "WarehouseName";
            cboWarehouse.Properties.ValueMember = "ID";
            if (warehouseIDNew > 0)
            {
                cboWarehouse.Enabled = true;

                cboWarehouse.EditValue = warehouseIDNew;

            }
            else
            {
                cboWarehouse.Enabled = false;
                cboWarehouse.EditValue = warehouseID;
            }
        }

        //Khánh update NCC 27/11/2023 
        private void LoadNCC()
        {
            cboSupplierSale.Properties.DataSource = SQLHelper<SupplierSaleModel>.FindAll();
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
            cboSupplierSale.Properties.ValueMember = "ID";
        }

        void LoadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(x => x.ID).ToList();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;
        }


        void LoadEmployeeBorrow()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployeeBorrow.ValueMember = "ID";
            cboEmployeeBorrow.DisplayMember = "FullName";
            cboEmployeeBorrow.DataSource = dt;

            cboApprover.Properties.ValueMember = "ID";
            cboApprover.Properties.DisplayMember = "FullName";
            cboApprover.Properties.DataSource = dt;

            cboApprover.EditValue = 54;
        }


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

        void loadUser()
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.Select("SELECT * FROM Users");


            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });
            cboReceiver.Properties.DisplayMember = cboDeliver.Properties.DisplayMember = "FullName";
            cboReceiver.Properties.ValueMember = cboDeliver.Properties.ValueMember = "ID";
            cboReceiver.Properties.DataSource = cboDeliver.Properties.DataSource = dt;

        }

        /// <summary>
        /// click button xóa dòng trong grvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductID));
            int rowIndex = grvData.GetSelectedRows()[0];
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                listIdDelete.Add(ID);
            }
        }

        /// <summary>
        /// load Data
        /// </summary>
        void loadBillImportDetail()
        {
            //cboDeliver.EditValue = Global.UserID;
            //txtDeliver.Text = Global.AppFullName;

            //NT.Huy update 22/05/2024
            cboReceiver.EditValue = Global.UserID;
            if (warehouseID == 2) cboReceiver.EditValue = 1434;

            cboRulePay.EditValue = billImport.RulePayID;
            if (billImport.RulePayID == 0) cboRulePay.EditValue = 34;
            if (flag != 0)
            {
                txtBillCode.Text = billImport.BillCode;
                txtWarehouseType.Text = billImport.WarehouseType;
                chkBillType.Checked = billImport.BillType == true;
                dtpCreatDate.Value = (DateTime)billImport.CreatDate;
                if (TextUtils.ToInt(cboReceiver.EditValue) == 0)
                {
                    txtReceiver.Visible = true;
                    txtReceiver.Text = billImport.Receiver;
                }
                cboDeliver.EditValue = billImport.DeliverID;
                txtDeliver.Text = billImport.Deliver;
                txtSupplier.Text = billImport.Suplier;
                cboSupplierSale.EditValue = billImport.SupplierSaleID;
                cboBillTypeNew.SelectedIndex = TextUtils.ToInt(billImport.BillTypeNew) + 1;
                cboCustomer.EditValue = billImport.CustomerID;
                cboApprover.EditValue = billImport.ApproverID;//ndnhat 03/04/2025
                chkIsNormalize.Checked = billImport.IsNormalize == true;//ndnhat 03/04/2025


                //Tiến Huy update 22/05/2024
                DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetailTechnical", "A", new string[] { "@ID" }, new object[] { billImport.ID });
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
                    ProductRTCModel productRTC = new ProductRTCModel();
                    int projectID = TextUtils.ToInt(row["ProjectID"]);
                    int productID = TextUtils.ToInt(row["ProductRTCID"]);
                    if (projectID > 0)
                    {
                        project = SQLHelper<ProjectModel>.FindByID(projectID);
                    }
                    if (productID > 0)
                    {
                        productRTC = SQLHelper<ProductRTCModel>.FindByID(productID);
                    }
                    DataRow dr = dtClone.NewRow();
                    dr["STT"] = i;
                    i++;
                    //dr["ProductNewCode"] = row[""];

                    dr["ProductID"] = productID;//ndNhat update 27/03/2025
                    if (productRTC != null)
                    {
                        dr["ProductName"] = productRTC.ProductName;
                        dr["ProductCodeRTC"] = productRTC.ProductCodeRTC;
                        dr["ProductCode"] = productRTC.ProductCode;
                        UnitCountKTModel unit = new UnitCountKTModel();
                        if (productRTC.UnitCountID > 0)
                        {
                            int id = productRTC.UnitCountID ?? 0;
                            unit = SQLHelper<UnitCountKTModel>.FindByID(id);
                        }
                        if (unit != null)
                        {
                            dr["UnitName"] = unit.UnitCountName;

                        }
                    }
                    //end ndnhat update

                    //dr["ProductID"] = row["ProductRTCID"];
                    //if (productRTC != null)
                    //{
                    //    dr["ProductName"] = productRTC.ProductName;
                    //    dr["ProductCodeRTC"] = productRTC.ProductCodeRTC;
                    //    UnitCountKTModel unit = new UnitCountKTModel();
                    //    if (productRTC.UnitCountID > 0)
                    //    {
                    //        unit = SQLHelper<UnitCountKTModel>.FindByID(productRTC.UnitCountID);
                    //    }
                    //    if (unit != null)
                    //    {
                    //        dr["UnitName"] = unit.UnitCountName;

                    //    }
                    //}

                    dr["UnitName"] = row["UnitName"];
                    //dr["ProductNewCode"] = row["ProductCode"];
                    dr["Maker"] = productRTC.Maker;
                    //dr["ProductCodeRTC"] = row["ProductCodeOfSupplier"];
                    dr["Quantity"] = row["QuantityRemainDemo"];
                    dr["SomeBill"] = "";
                    dr["ProjectID"] = row["ProjectID"];
                    dr["Note"] = row["POCode"];
                    dr["PONCCDetailID"] = row["ID"];
                    // dr["CodeMaPhieuMuon"] = "";
                    //dr["SerialNumber"] = "";
                    //dr["ProjectPartListID"] = row["ProjectPartListID"];
                    dr["Price"] = row["UnitPrice"];
                    dr["BillCodePO"] = row["BillCode"];
                    //dr["IsKeepProject"] = row["IsKeepProject"];

                    dtClone.Rows.Add(dr);
                }

                grdData.DataSource = dtClone;
            }
            else
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetailTechnical", "A", new string[] { "@ID" }, new object[] { billImport.ID });
                grdData.DataSource = dt;
            }

            // if (billImport.BillCode != null)
            // {
            //     txtBillCode.Text = billImport.BillCode;
            // }
            // if (billImport.WarehouseType != null)
            // {
            //     txtWarehouseType.Text = billImport.WarehouseType;
            // }
            //if(billImport.BillType != null)
            // {
            //     chkBillType.Checked = billImport.BillType;
            // }
            // if(billImport.CreatDate != null)
            // {
            //     dtpCreatDate.Value = (DateTime)billImport.CreatDate;
            // }
            //if (TextUtils.ToInt(cboReceiver.EditValue) == 0)
            //{
            //    txtReceiver.Visible = true;
            //    txtReceiver.Text = billImport.Receiver;
            //}
            //cboDeliver.EditValue = billImport.DeliverID;
            //txtDeliver.Text = billImport.Deliver;
            //txtSupplier.Text = billImport.Suplier;
            //cboSupplierSale.EditValue = billImport.SupplierSaleID;
            //cboBillTypeNew.SelectedIndex = billImport.BillTypeNew + 1;
            //cboCustomer.EditValue = billImport.CustomerID;

            if (billImport.ID > 0)
            {
                //grvData.Focus();
                //txtBillCode.Focus();
                txtBillCode.Text = billImport.BillCode;
                txtWarehouseType.Text = billImport.WarehouseType;
                chkBillType.Checked = billImport.BillType == true;
                dtpCreatDate.Value = (DateTime)billImport.CreatDate;

                cboReceiver.EditValue = billImport.ReceiverID;
                if (TextUtils.ToInt(cboReceiver.EditValue) == 0)
                {
                    txtReceiver.Visible = true;
                    txtReceiver.Text = billImport.Receiver;

                }
                cboDeliver.EditValue = billImport.DeliverID;
                txtDeliver.Text = billImport.Deliver;

                txtSupplier.Text = billImport.Suplier;
                cboSupplierSale.EditValue = billImport.SupplierSaleID;
                cboBillTypeNew.SelectedIndex = TextUtils.ToInt(billImport.BillTypeNew); // khánh update 23/01/2024
                //if (billImport.BillCode.StartsWith("T") == true)
                //{
                //    lbSupplier.Text = "Bộ phận :";
                //    lbUser.Text = "Người trả :";
                //}

                cboCustomer.EditValue = billImport.CustomerID;
                cboApprover.EditValue = billImport.ApproverID;//ndnhat 03/04/2025
                chkIsNormalize.Checked = billImport.IsNormalize == true;//ndnhat 03/04/2025

            }
            else
            {
                loadBilllNumber();
            }


        }
        /// <summary>
        /// load sản phẩm
        /// </summary>
        void LoadProduct()
        {
            //dtProduct = TextUtils.Select($"SELECT *,ROW_NUMBER() OVER(ORDER BY ID desc) AS STT FROM ProductRTC WHERE IsDelete <> 1 AND ProductGroupRTCID <> 140 ORDER BY ID DESC");

            dtProduct = TextUtils.LoadDataFromSP("spGetProductRTC", "A",
                                new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                new object[] { 0, "", 1, warehouseID });

            cboProduct.DisplayMember = "ProductCode";
            cboProduct.ValueMember = "ID";
            cboProduct.DataSource = dtProduct;
        }

        private void loadUnit(int id)
        {
            DataTable dt = TextUtils.Select($"SELECT ID, UnitCountName FROM UnitCountKT WHERE ID = {id}");

            string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {id}"));
            cboUnitID.DisplayMember = "UnitCountName";
            cboUnitID.ValueMember = "ID";
            cboUnitID.DataSource = dt;
        }
        private void grvDetailTech_MouseDown(object sender, MouseEventArgs e)
        {
            //GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            //if (info.Column == colSTT && e.Y < 40)
            //{
            //    MyLib.AddNewRow(grdData, grvData);
            //}


            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;

                    DataTable dt = (DataTable)grdData.DataSource;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    int stt = 0;
                    if (dt.Rows.Count > 0)
                    {
                        stt = dt.AsEnumerable().Max(x => x.Field<int>("STT"));
                    }

                    int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colEmployeeIDBorrow));
                    DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellValue(grvData.FocusedRowHandle, colDeadlineReturnNCC));

                    dtrow["STT"] = stt + 1;
                    dtrow["EmployeeIDBorrow"] = employeeID;

                    if (deadline.HasValue)
                    {
                        dtrow["DeadlineReturnNCC"] = deadline.Value;
                    }
                    dt.Rows.Add(dtrow);
                }
            }

        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
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
                }
            }
        }

        string[] preBillCodes = new string[]
        {
            "PNKD",//0
            "PNKD",//1
            "PNKD",//2
            "PTD",//3
            "PNKD",//4
            "PNKD",//5
            "PNKD",//6
            "PNKD",//7
        };
        void loadBilllNumber()
        {
            //if (billImport.ID > 0 && !Global.IsAdmin) return;
            //int billtype = cboBillTypeNew.SelectedIndex;
            //txtBillCode.Text = TextUtils.GetBillCode("BillImportTechnical", billtype);
            //return;

            int billtype = cboBillTypeNew.SelectedIndex;
            if (billImport.ID > 0)
            {
                string preCodeNew = preBillCodes[billtype];
                string preCodeOld = preBillCodes[TextUtils.ToInt(billImport.BillTypeNew)];

                string billlNumber = billImport.BillCode.Replace(preCodeOld.ToString(), preCodeNew.ToString());
                //string billlNumber = billImport.BillCode.Replace("PNKD", preCodeNew.ToString());
                txtBillCode.Text = billlNumber;
            }
            else
            {
                //if (!Global.DebugFlag)
                {
                    txtBillCode.Text = TextUtils.GetBillCode("BillImportTechnical", billtype);
                    return;
                }
            }
            return;

            int code = 0;

            //string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            //string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            //string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);

            string month = TextUtils.ToString(dtpCreatDate.Value.Month);
            if (TextUtils.ToInt(month) < 10)
            {
                month = "0" + month;
            }

            string day = TextUtils.ToString(dtpCreatDate.Value.Day);
            if (TextUtils.ToInt(day) < 10)
            {
                day = "0" + day;
            }

            string year = TextUtils.ToString(dtpCreatDate.Value.Year).Substring(2);
            string date = year + month + day;
            //string billCode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 BillCode FROM BillImportTechnical where month(CreatedDate) = {dtpCreatDate.Value.Month} and Year(CreatedDate)={dtpCreatDate.Value.Year} and Day(CreatedDate) ={dtpCreatDate.Value.Day} and WarehouseID = {warehouseID} ORDER BY ID DESC"));
            string billCode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 BillCode FROM BillImportTechnical where month(CreatedDate) = {dtpCreatDate.Value.Month} and Year(CreatedDate)={dtpCreatDate.Value.Year} and Day(CreatedDate) ={dtpCreatDate.Value.Day} ORDER BY ID DESC"));

            if (billCode.Contains("PT"))
            {
                billCode = billCode.Substring(2);
            }
            else if (billCode.Contains("PNK"))
            {

                billCode = billCode.Substring(3);
            }

            if (billCode == "")
            {
                if (cboBillTypeNew.SelectedIndex == 3) status = "PTD";
                else status = "PNKD";
                txtBillCode.Text = status + date + "001";
                return;
            }
            else
                code = TextUtils.ToInt(billCode.Substring(billCode.Length - 3));

            if (code == 0)
            {
                if (cboBillTypeNew.SelectedIndex == 3)
                {
                    txtBillCode.Text = "PTD" + date + "001";
                }
                else
                    txtBillCode.Text = "PNKD" + date + "001";
                return;
            }
            else
            {
                string dem = TextUtils.ToString(code + 1);
                for (int i = 0; dem.Length < 3; i++)
                {
                    dem = "0" + dem;
                }

                if (cboBillTypeNew.SelectedIndex == 3)
                {
                    txtBillCode.Text = "PTD" + date + TextUtils.ToString(dem);
                }
                else
                {
                    txtBillCode.Text = "PNKD" + date + TextUtils.ToString(dem);
                }
            }

        }
        void UpdateHistoryProductRTC()
        {
            bool values = true;

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colHistoryProductRTCID));
                if (ID <= 0) continue;
                DateTime dateTime = DateTime.Now;
                TextUtils.ExcuteSQL($"Exec spUpdateHistoryProductRTC {ID},{values},'{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'");

                int ProductRTCQRCodeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCQRCodeID));
                if (ProductRTCQRCodeID > 0)
                {
                    if (values)
                    {
                        //Status=1 Trong Kho
                        TextUtils.ExcuteProcedure("spUpdateStatusProductRTCQRCode", new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { ProductRTCQRCodeID, 1 });

                    }
                    else
                    {
                        //Status=3 Đã xuất
                        TextUtils.ExcuteProcedure("spUpdateStatusProductRTCQRCode", new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { ProductRTCQRCodeID, 3 });
                    }
                }
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                if (ProductID <= 0) continue;
                //ProductRTCModel productRTCModel = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ProductID);
                ProductRTCModel productRTCModel = SQLHelper<ProductRTCModel>.FindByID(ProductID);
                if (productRTCModel != null)
                {
                    productRTCModel.Note += " - " + TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                    //ProductRTCBO.Instance.Update(productRTCModel);
                    SQLHelper<ProductRTCModel>.Update(productRTCModel);
                }

            }
        }
        //bool saveData()
        //{
        //    grvData.FocusedColumn = colID;
        //    //RecheckQty();
        //    //cboReceiver.Focus();
        //    //grvData.Focus();

        //    if (!ValidateForm()) return false;

        //    billImport.BillCode = txtBillCode.Text.Trim();
        //    billImport.CreatDate = dtpCreatDate.Value;
        //    billImport.Deliver = cboDeliver.Text.Trim();
        //    billImport.Receiver = txtReceiver.Text.Trim();
        //    billImport.Suplier = txtSupplier.Text.Trim();
        //    billImport.WarehouseType = txtWarehouseType.Text.Trim();
        //    //billImport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
        //    //billImport.ReceiverID = TextUtils.ToInt(cboReceiver.EditValue);
        //    //billImport.SuplierID = TextUtils.ToInt(cboSuplier.EditValue);
        //    billImport.BillType = chkBillType.Checked;
        //    billImport.Status = false;
        //    billImport.WarehouseID = warehouseID;
        //    billImport.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
        //    billImport.BillTypeNew = chkIsBorrowSupplier.Checked ? 1 : 0;
        //    //billImport.Image = picSign.ImageLocation;
        //    if (chkBillType.Checked == true)
        //    {
        //        billImport.BillType = true;
        //    }
        //    else
        //    {
        //        billImport.BillType = false;
        //    }

        //    if (billImport.ID > 0)
        //    {
        //        BillImportTechnicalBO.Instance.Update(billImport);
        //    }
        //    else
        //    {
        //        billImport.ID = (int)BillImportBO.Instance.Insert(billImport);
        //    }

        //    for (int i = 0; i < grvData.RowCount; i++)
        //    {
        //        long ID = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
        //        BillImportDetailTechnicalModel detail = new BillImportDetailTechnicalModel();

        //        if (ID > 0)
        //        {
        //            detail = (BillImportDetailTechnicalModel)(BillImportDetailTechnicalBO.Instance.FindByPK(ID));
        //        }
        //        detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
        //        detail.BillImportTechID = billImport.ID; //billImport.ID
        //        detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
        //        detail.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));
        //        detail.Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice));
        //        detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
        //        detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
        //        detail.TotalQuantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQuantity));
        //        detail.UnitID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitName));
        //        detail.UnitName = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colUnitName));
        //        detail.HistoryProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colHistoryProductRTCID));
        //        detail.ProductRTCQRCodeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCQRCodeID));//HuyNV -5/11/2022

        //        detail.WarehouseID = warehouseID;
        //        if (detail.ID > 0)
        //        {
        //            BillImportDetailTechnicalBO.Instance.Update(detail);
        //            //if (listIdDelete.Count > 0)
        //            // BillImportDetailTechnicalBO.Instance.Delete(listIdDelete);
        //        }
        //        else
        //        {
        //            detail.ID = (int)BillImportDetailTechnicalBO.Instance.Insert(detail);
        //            if (lstSerialNumbers.Count > i)
        //            {
        //                foreach (var x in lstSerialNumbers[i])
        //                {
        //                    x.BillImportTechDetailID = detail.ID;

        //                    BillImportTechDetailSerialBO.Instance.Insert(x);
        //                }
        //            }

        //        }
        //    }
        //    UpdateHistoryProductRTC();
        //    if (listIdDelete.Count > 0)
        //        BillImportDetailTechnicalBO.Instance.Delete(listIdDelete);


        //    return true;
        //}

        bool saveData()
        {
            try
            {
                grvData.FocusedColumn = colID;
                //RecheckQty();
                //cboReceiver.Focus();
                //grvData.Focus();

                if (!ValidateForm()) return false;

                billImport.BillCode = txtBillCode.Text.Trim();
                billImport.CreatDate = dtpCreatDate.Value;
                List<int> list = new List<int> { 1151, 78 };
                if (!string.IsNullOrEmpty(txtDeliver.Text.Trim()))
                    billImport.Deliver = txtDeliver.Text.Trim();
                else billImport.Deliver = cboDeliver.Text.Trim();
                billImport.Receiver = cboReceiver.Text.Trim();
                billImport.Suplier = txtSupplier.Text.Trim();
                billImport.WarehouseType = txtWarehouseType.Text.Trim();
                billImport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
                billImport.ReceiverID = TextUtils.ToInt(cboReceiver.EditValue);
                //billImport.SuplierID = TextUtils.ToInt(cboSuplier.EditValue);
                billImport.BillType = chkBillType.Checked;

                billImport.WarehouseID = warehouseID;
                billImport.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
                //billImport.IsBorrowSupplier = cbMuonNCC.Checked;
                billImport.BillTypeNew = cboBillTypeNew.SelectedIndex;
                //if (chkBillType.Checked == true)
                //{
                //    billImport.BillType = true;
                //}
                //else
                //{
                //    billImport.BillType = false;
                //}
                if (billImport.BillTypeNew == 5) //nếu là yêu cầu nhập kho
                {
                    billImport.DateRequestImport = dtpCreatDate.Value;
                }
                billImport.CustomerID = TextUtils.ToInt(cboCustomer.EditValue); //Lt.Anh update 09/04/2024
                billImport.ApproverID = TextUtils.ToInt(cboApprover.EditValue);//ndnhat 03/04/2025
                billImport.IsNormalize = chkIsNormalize.Checked; //ndnhat 03/04/2025
                if (billImport.ID > 0)
                {
                    //BillImportTechnicalBO.Instance.Update(billImport);
                    SQLHelper<BillImportTechnicalModel>.Update(billImport);
                }
                else
                {
                    //billImport.ID = (int)BillImportBO.Instance.Insert(billImport);

                    //   billImport.BillDocumentImportType = 2;
                    billImport.Status = false;

                    //billImport.ID = (int)BillImportTechnicalBO.Instance.Insert(billImport);//cũ
                    billImport.ID = SQLHelper<BillImportTechnicalModel>.Insert(billImport).ID;

                    List<DocumentImportModel> listID = SQLHelper<DocumentImportModel>.SqlToList("SELECT ID FROM dbo.DocumentImport WHERE IsDeleted <> 1");
                    for (int i = 0; i < listID.Count; i++)
                    {
                        var documentImportID = listID[i];

                        //BillDocumentImportTechnicalModel bditModel = new BillDocumentImportTechnicalModel();

                        //bditModel.BillImportTechnicalID = billImport.ID;
                        //bditModel.DocumentImportID = documentImportID.ID;

                        //BillDocumentImportTechnicalBO.Instance.Insert(bditModel);
                    }

                    //Add Notify
                    DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequestByBillImportID", "A", new string[] { "@BillImportTechnicalID" }, new object[] { billImport.ID });
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string text = $"Kho: Demo\n" +
                                        $"Mã sản phẩm: {TextUtils.ToString(row["ProductCode"])}\n" +
                                        $"Tên sản phần: {TextUtils.ToString(row["ProductName"])}";
                            TextUtils.AddNotify("THÔNG BÁO HÀNG VỀ", text, TextUtils.ToInt(row["EmployeeID"]));
                        }
                    }
                }

                for (int i = 0; i < grvData.RowCount; i++)
                {
                    long ID = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                    BillImportDetailTechnicalModel detail = new BillImportDetailTechnicalModel();

                    if (ID > 0)
                    {
                        //detail = (BillImportDetailTechnicalModel)(BillImportDetailTechnicalBO.Instance.FindByPK(ID));
                        detail = SQLHelper<BillImportDetailTechnicalModel>.FindByID(ID);
                    }
                    detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
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

                    if (Global.UserID == billImport.DeliverID || Global.IsAdmin)
                    {
                        DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(i, colDateSomeBill));
                        detail.DateSomeBill = dateSomeBill;
                        detail.SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill));
                    }

                    //detail.SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill)); //--NTAB update 01/08/2025
                    //detail.DateSomeBill = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateSomeBill));
                    detail.DPO = TextUtils.ToInt(grvData.GetRowCellValue(i, colDPO));
                    detail.DueDate = TextUtils.ToDate4(grvData.GetRowCellDisplayText(i, colDueDate));
                    detail.TaxReduction = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxReduction));
                    detail.COFormE = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCOFormE));

                    detail.WarehouseID = warehouseID;
                    if (detail.ID > 0)
                    {
                        //BillImportDetailTechnicalBO.Instance.Update(detail);
                        SQLHelper<BillImportDetailTechnicalModel>.Update(detail);
                        //if (listIdDelete.Count > 0)
                        // BillImportDetailTechnicalBO.Instance.Delete(listIdDelete);
                        string[] serials = grvData.GetRowCellDisplayText(i, colJoinedIDs).Split(',');
                        foreach (string item in serials)
                        {
                            int id = TextUtils.ToInt(item);
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"BillImportTechDetailID", detail.ID}
                            };
                            SQLHelper<BillImportTechDetailSerialModel>.UpdateFieldsByID(data, id);
                        }
                    }
                    else
                    {
                        //detail.ID = (int)BillImportDetailTechnicalBO.Instance.Insert(detail);
                        detail.ID = SQLHelper<BillImportDetailTechnicalModel>.Insert(detail).ID;
                        string[] serials = grvData.GetRowCellDisplayText(i, colJoinedIDs).Split(',');
                        foreach (string item in serials)
                        {
                            int id = TextUtils.ToInt(item);
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                {"BillImportTechDetailID", detail.ID}
                            };
                            SQLHelper<BillImportTechDetailSerialModel>.UpdateFieldsByID(data, id);

                        }

                        


                        //Add notify
                        if (billImport.BillTypeNew == 5)
                        {
                            string productCode = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colProductID));
                            string productName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                            string text = $"Mã sản phầm: {productCode}\n" +
                                            $"Tên sản phẩm: {productName}\n";
                            TextUtils.AddNotify("THÔNG BÁO HÀNG VỀ - DEMO", text, TextUtils.ToInt(detail.EmployeeIDBorrow));
                        }
                    }


                    //Khánh update thêm inventory  18/01/2024
                    var exp1 = new Expression("ProductRTCID", detail.ProductID);
                    var exp2 = new Expression("WarehouseID", warehouseID);
                    var check = InventoryDemoBO.Instance.FindByExpression(exp1.And(exp2));
                    if (check.Count <= 0)
                    {
                        InventoryDemoModel inventory = new InventoryDemoModel();
                        inventory.ProductRTCID = TextUtils.ToInt(detail.ProductID);
                        inventory.WarehouseID = warehouseID;
                        InventoryDemoBO.Instance.Insert(inventory);
                    }

                    UpdateHistoryProductRTC();
                    if (listIdDelete.Count > 0)
                        BillImportDetailTechnicalBO.Instance.Delete(listIdDelete);

                    //Update trạng thái PO NCC
                    //TextUtils.ExcuteProcedure("spUpdateStatusPONCC", new string[] { "@BillImportID", "@UpdatedBy", "@WarehouseType" }, new object[] { billImport.ID, Global.LoginName, 0 });
                    var listDetails = SQLHelper<BillImportDetailTechnicalModel>.FindByAttribute("BillImportTechID", billImport.ID);
                    string poNCCDetailID = string.Join(",", listDetails.Select(x => x.PONCCDetailID));
                    TextUtils.ExcuteProcedure("spUpdateStatusPONCC",
                                                new string[] { "@PONCCDetailID", "@UpdatedBy" },
                                                new object[] { poNCCDetailID, Global.LoginName });

                    //Tiến huy update 22/05/2024
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
                }
                UpdateDocumentNew(billImport.ID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
                return false;
            }
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
        void UpdateDocumentNew(int billImportTechnicalID) //NTA B - update 04/08/2025
        {
            try
            {
                for (int i = 0; i < grvDocImport.RowCount; i++)
                {

                    int id = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, "ID"));

                    BillDocumentImportTechnicalModel document = SQLHelper<BillDocumentImportTechnicalModel>.FindByID(id);
                    document.DocumentImportID = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocumentImportID));
                    document.ReasonCancel = TextUtils.ToString(grvDocImport.GetRowCellValue(i, colReasonCancel)).Trim();
                    document.Note = TextUtils.ToString(grvDocImport.GetRowCellValue(i, colDocumentNote)).Trim();
                    //document.DateReceive = TextUtils.ToDate4(grvDocImport.GetRowCellValue(i, colDateReceive));
                    //document.EmployeeReciveID = Global.EmployeeID;
                    document.BillImportTechnicalID = billImportTechnicalID;
                    document.Status = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocumentStatus));



                    document.StatusPurchase = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocumentStatusPur));
                    //document.UpdatedBy = Global.AppUserName;
                    document.UpdatedDate = DateTime.Now;

                    if (document.ID <= 0)
                    {
                        SQLHelper<BillDocumentImportTechnicalModel>.Insert(document);
                    }
                    else
                    {
                        SQLHelper<BillDocumentImportTechnicalModel>.Update(document);
                    }


                    BillDocumentImportTechnicalLogModel log = new BillDocumentImportTechnicalLogModel();
                    log.BillDocumentImportTechnicalID = document.ID;
                    log.Status = TextUtils.ToInt(document.Status);
                    log.LogDate = TextUtils.ToDate4(grvDocImport.GetRowCellValue(i, colDateReceive));
                    log.Note = $"LÝ DO HUỶ: {document.ReasonCancel}\nGHI CHÚ: {document.Note}";
                    log.DocumentImportID = document.DocumentImportID;
                    SQLHelper<BillDocumentImportTechnicalLogModel>.Insert(log);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo");
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
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }
                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
        }


        public void LoadEmailContent()
        {

            subject = $"Thông báo hàng về kho Demo {txtBillCode.Text.Trim()}".ToUpper();
            //receiverMailID = TextUtils.ToInt(cboReceiver.EditValue);

            //EmployeeModel receiverMail = SQLHelper<EmployeeModel>.FindByID(receiverMailID);

            int reciver = TextUtils.ToInt(cboReceiver.EditValue);
            EmployeeModel receiverMail = SQLHelper<EmployeeModel>.FindByAttribute("UserID", reciver).FirstOrDefault();
            if (receiverMail == null) return;
            receiverMailID = receiverMail.ID;

            string tbody = "";
            for (int i = 0; i < grvData.RowCount; i++)
            {
                tbody += $"<tr style='border:1px solid black'>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colProjectCode)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{POCode}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colProductName)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellDisplayText(i, colProductID)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colProductCodeRTC)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colUnitName)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{grvData.GetRowCellValue(i, colMaker)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px; text-align: right;\">{grvData.GetRowCellValue(i, colQuantity)}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{txtWarehouseType.Text}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px;\">{cboSupplierSale.Text}</td>" +
                         $"<td style=\"border: 1px solid; padding: 5px; text-align: center\">{dtpCreatDate.Value.ToString("dd/MM/yyyy")}</td>" +
                         $"</tr>";

            }
            string body = $"<div>Dear anh/chị {receiverMail.FullName},<br>" +
                  $"Thông tin chi tiết hàng về: {txtBillCode.Text.Trim()} - {dtpCreatDate.Value.ToString("dd/MM/yyyy")}<br><br>" +
                  $"<table style=\"border-collapse: collapse;border: 1px solid; table-layout: auto; width: 100%;\"> <thead>" +
                  $"<tr style='border:1px solid black'>" +
                  $"<th style=\"border: 1px solid;\"> Dự án </th>" +
                  $"<th style=\"border: 1px solid;\"> Số PO </th>" +
                  $"<th style=\"border: 1px solid;\"> Tên sản phẩm </th>" +
                  $"<th style=\"border: 1px solid;\"> Mã nội bộ </th>" +
                  $"<th style=\"border: 1px solid;\"> Đơn vị tính</th>" +
                  $"<th style=\"border: 1px solid;\"> Hãng </th>" +
                  $"<th style=\"border: 1px solid;\"> Số lượng</th>" +
                  $"<th style=\"border: 1px solid;\"> Loại kho</th>" +
                  $"<th style=\"border: 1px solid;\"> Nhà cung cấp</th>" +
                  $"<th style=\"border: 1px solid;\"> Ngày nhập</th>" +
                  $"</tr>" +
                  $"</thead> " +
                  $"<tbody>" + tbody +
                  $"</tbody>" +
                  $"</table> " +
                  $"<br>" +
                  $"Trân trọng,<br>" +
                  $"{Global.AppFullName}." +
                  $"</div>";

        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            //if (billImport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void grvDetailTech_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //HuyNV 5/11/2022
            //if (e.Column == colQuantity || e.Column == colProductID)
            //{
            //    int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            //    float sum = 0;
            //    for (int j = 0; j < grvData.RowCount; j++)
            //    {
            //        int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
            //        if (id == idSearch)
            //        {
            //            int qty = TextUtils.ToInt(grvData.GetRowCellValue(j, colQuantity));
            //            sum += qty;
            //        }
            //    }
            //    for (int i = 0; i < grvData.RowCount; i++)
            //    {
            //        int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
            //        if (id == idSearch)
            //        {
            //            grvData.SetRowCellValue(i, colTotalQuantity, sum);
            //        }
            //    }
            //}


            //if (e.Column == colMaker)
            //{
            //    int projectID = TextUtils.ToInt(grvDetailTech.GetFocusedRowCellValue(colMaker));
            //    for (int i = 0; i < grvDetailTech.RowCount; i++)
            //    {
            //        int item = TextUtils.ToInt(grvDetailTech.GetRowCellValue(i, colMaker));
            //        if (item == 0)
            //            grvDetailTech.SetRowCellValue(i, colMaker, projectID);
            //    }
            //}

            //Nguyễn Tuấn Anh B 01/08/2025
            if (e.Column == colDPO)
            {
                int row = grvData.FocusedRowHandle;
                int DPO = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDPO));
                DateTime? dateSomeBill = TextUtils.ToDate4(grvData.GetRowCellDisplayText(row, colDateSomeBill));
                if (dateSomeBill.HasValue)
                {
                    DateTime dueDate = dateSomeBill.Value.AddDays(DPO);
                    grvData.SetRowCellValue(row, colDueDate, dueDate);
                }
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailRTC frm = new frmProductDetailRTC(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProduct();
            }
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadProject();
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu nhập mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                statusOld = "";
                txtSupplier.Text = "";
                //cboReceiver.Text = "";
                //cboDeliver.Text = "";

                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }

                billImport = new BillImportTechnicalModel();
                loadBilllNumber();
            }
        }

        /// <summary>
        /// hàm ckeck sản phẩm đã điền đủ hay không
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            string billCode = txtBillCode.Text.Trim();
            DataTable dt = TextUtils.Select($"select top 1 ID from BillImportTechnical where BillCode = '{billCode}' and ID <> {billImport.ID} AND WarehouseID = {warehouseID}");
            if (billImport.ID > 0)
            {

                //if (billCode.Contains("PT"))
                //{
                //    billCode = billCode.Substring(2);
                //}
                //else if (billCode.Contains("PNK"))
                //{
                //    billCode = billCode.Substring(3);
                //}
                ////int strID = billImport.ID;
                ////dt = TextUtils.Select($"select top 1 ID from BillImportTechnical where BillCode = '{billCode}' and ID <> {strID} AND WarehouseID = {warehouseID}");
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("Số phiếu này đã tồn tại.\nVui lòng Load lại Số phiếu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
            }
            else
            {
                //dt = TextUtils.Select($"select top 1 ID from BillImportTechnical where BillCode = '{txtBillCode.Text.Trim()}'");
                if (dt.Rows.Count > 0)
                {
                    loadBilllNumber();
                    MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtBillCode.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (txtBillCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //if (TextUtils.ToInt(cboSupplierSale.EditValue) <= 0)//Khánh update 28/11/2023
            //{
            //    MessageBox.Show("Xin hãy điền thông tin nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    txtSupplier.Focus();
            //    return false;
            //}
            if (cboReceiver.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền thông tin người nhập.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboDeliver.Text == "")
            {
                MessageBox.Show("Xin hãy điền thông tin người giao.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cboBillTypeNew.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn Loại phiếu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (TextUtils.ToInt(cboSupplierSale.EditValue) <= 0 && TextUtils.ToInt(cboCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp hoặc Khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (TextUtils.ToInt(cboRulePay.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Điều khoản TT!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (TextUtils.ToInt(cboApprover.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Người duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void dtpCreatDate_ValueChanged(object sender, EventArgs e)
        {
            //loadBilllNumber();
        }

        private void btnDelete1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colProductID));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                listIdDelete.Add(ID);
            }
        }
        private void picSign_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //picSign.ImageLocation = fileDialog.FileName;
            }
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            //ndNhat update 27/03/2025
            SearchLookUpEdit searchLookUpEdit = sender as SearchLookUpEdit;
            DataRowView dataRow = searchLookUpEdit.GetSelectedDataRow() as DataRowView;
            if (dataRow == null) return;
            int rowHandle = grvData.FocusedRowHandle;
            grvData.SetRowCellValue(rowHandle, colProductID, TextUtils.ToInt(dataRow["ID"]));
            grvData.SetRowCellValue(rowHandle, colProductName, TextUtils.ToString(dataRow["ProductName"]));
            int unit = TextUtils.ToInt(dataRow["UnitCountID"]);
            string Unitname = SQLHelper<UnitCountKTModel>.FindByID(unit).UnitCountName;
            grvData.SetRowCellValue(rowHandle, colUnitName, Unitname);
            grvData.SetRowCellValue(rowHandle, colProductCodeRTC, TextUtils.ToString(dataRow["ProductCodeRTC"]));
            grvData.SetRowCellValue(rowHandle, colMaker, TextUtils.ToString(dataRow["Maker"]));
            grvData.SetRowCellValue(rowHandle, colQuantity, TextUtils.ToInt(dataRow["NumberInStore"]));

            //end ndnhat update

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
        }
        string statusOld = "";
        private void chkBillType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBillType.Checked == false) status = "PNK";
            else status = "PT";
            if (check == 0)
            {
                check = 1;
                //statusOld = status;
            }
            //if (billImport.ID > 0)
            //{
            //    status = statusOld;
            //}
            //loadBilllNumber();
        }


        //Update ver 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //frmProductHistory frm = new frmProductHistory();
            //frm.Show();
            List<List<int>> ListGrv = new List<List<int>>();
            frmProductHistory frm = new frmProductHistory(warehouseID);
            frm.IsbtnXuat = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ListGrv = frm.ListGrv;

            }
            if (ListGrv.Count > 0)
            {
                int row = grvData.RowCount;
                for (int i = 0; i < ListGrv.Count; i++)
                {
                    MyLib.AddNewRow(grdData, grvData);
                    grvData.SetRowCellValue(i + row, colProductID, ListGrv[i][0]);
                    SetValueCol(ListGrv[i][0], i + row, ListGrv[i][1], ListGrv[i][2]);

                    //Update lại status trong HistoryProductRTC

                }

            }
        }
        void SetValueCol(int id, int rowNumber, int NumberBorrow, int HistoryProductRTCID)
        {
            HistoryProductRTCModel historyProductRTCModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryProductRTCID);

            DataRow[] rows = dtProduct.Select("ID = " + id);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string productCodeRTC = TextUtils.ToString(rows[0]["ProductCodeRTC"]);
                string maker = TextUtils.ToString(rows[0]["Maker"]);
                int idUnitCount = TextUtils.ToInt(rows[0]["UnitCountID"]);
                string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));

                string note = "";
                //if (historyProductRTCModel != null)
                //{
                //    note = historyProductRTCModel.Note;
                //}

                //BillExportTechnicalModel bill = (BillExportTechnicalModel)BillExportTechnicalBO.Instance.FindByPK(historyProductRTCModel.BillExportTechnicalID);
                BillExportTechnicalModel bill = SQLHelper<BillExportTechnicalModel>.FindByID(historyProductRTCModel.BillExportTechnicalID);
                if (string.IsNullOrEmpty(note.Trim()))
                {
                    note = "Phiếu xuất " + bill.Code;
                }

                grvData.SetRowCellValue(rowNumber, colProductName, productName);
                grvData.SetRowCellValue(rowNumber, colUnitName, unitName);
                grvData.SetRowCellValue(rowNumber, colProductCodeRTC, productCodeRTC);
                grvData.SetRowCellValue(rowNumber, colMaker, maker);
                grvData.SetRowCellValue(rowNumber, colUnitCountID, idUnitCount);
                grvData.SetRowCellValue(rowNumber, colQuantity, NumberBorrow);
                grvData.SetRowCellValue(rowNumber, colHistoryProductRTCID, HistoryProductRTCID);
                grvData.SetRowCellValue(rowNumber, colNote, note);
                grvData.SetRowCellValue(rowNumber, colProductRTCQRCodeID, historyProductRTCModel.ProductRTCQRCodeID);
                // grvDetailTechExport.FocusedColumn = colQuantity;


                grvData.FocusedRowHandle = -1;
            }
        }

        private void btnAddSuplier_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            frm.Tag = "demo";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadNCC();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            isSave = true;
            if (!TextUtils.ToBoolean(billImport.Status))
            {
                saveData();
            }
            else
            {
                MessageBox.Show("Phiếu [" + txtBillCode.Text.Trim() + "] đã được duyệt.\nBạn không thể sửa phiếu này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmBillImportTechDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!isSave)
            //{
            //    DialogResult rs = MessageBox.Show("Bạn có muốn lưu thay đổi trước khi đóng không?", "Thông báo", MessageBoxButtons.OKCancel);
            //    if (rs == DialogResult.OK)
            //    {
            //        btnSave_Click(sender, e);
            //    }
            //    else
            //    {
            //        // Chỉ xóa các serial mới thêm (không có trong oldtempSerial ban đầu)
            //        List<BillImportTechDetailSerialModel> lst = SQLHelper<BillImportTechDetailSerialModel>.FindAll().Where(p => p.BillImportTechDetailID == 0).ToList();

            //        if (lst.Count > 0 && lst != null)
            //        {
            //            SQLHelper<BillImportTechDetailSerialModel>.DeleteListModel(lst);
            //        }
            //        var newSerials = tempSerial.Except(oldTempSerial).ToList();
            //        foreach (string item in newSerials)
            //        {
            //            int id = TextUtils.ToInt(item);
            //            SQLHelper<BillImportTechDetailSerialModel>.DeleteModelByID(id);
            //        }
            //        this.DialogResult = DialogResult.OK;
            //    }

            //}
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //List<List<BillImportTechDetailSerialModel>> lstSerialNumbers = new List<List<BillImportTechDetailSerialModel>>();
        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column == colAddSerialNumber)
            //{

            //    if (grdData.DataSource == null) return;
            //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //    //if (ID == 0) return;
            //    //int ID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM BillImportDetailTechnical WHERE WarehouseID = {warehouseID} ORDER BY ID DESC"));
            //    //MessageBox.Show(ID.ToString());
            //    int rowIndex = e.RowHandle;
            //    int Quantity = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQuantity));
            //    while (lstSerialNumbers.Count <= rowIndex)
            //    {
            //        lstSerialNumbers.Add(new List<BillImportTechDetailSerialModel>());
            //    }
            //    BillImportDetailTechnicalModel model = new BillImportDetailTechnicalModel() { ID = ID, Quantity = Quantity };
            //    frmBillTechnicalSerialNumber frm = new frmBillTechnicalSerialNumber(warehouseID);
            //    frm.modelImportDetail = model;
            //    frm.Type = 1;
            //    frm.lstSerialNumberImport = lstSerialNumbers[rowIndex];
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        List<BillImportTechDetailSerialModel> lstSerialNumber = frm.lstSerialNumberImport;
            //        if (lstSerialNumbers.Count <= rowIndex)
            //        {
            //            lstSerialNumbers.Add(lstSerialNumber);
            //        }
            //        else
            //        {
            //            lstSerialNumbers[rowIndex] = lstSerialNumber;
            //        }
            //    }
            //}
        }

        private void cboBillTypeNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBilllNumber();
        }

        private void cboWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            warehouseID = TextUtils.ToInt(cboWarehouse.EditValue);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();

            int rowHandle = grvData.FocusedRowHandle;
            DateTime? value = TextUtils.ToDate4(grvData.GetRowCellValue(rowHandle, colDeadlineReturnNCC));
            if (!value.HasValue) return;
            for (int i = rowHandle; i < grvData.RowCount; i++)
            {
                DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDeadlineReturnNCC));
                if (deadline.HasValue) continue;

                grvData.SetRowCellValue(i, colDeadlineReturnNCC, value);

            }
        }

        private void cboEmployeeBorrow_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();

            int rowHandle = grvData.FocusedRowHandle;
            int value = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colEmployeeIDBorrow));
            if (value <= 0) return;
            for (int i = rowHandle; i < grvData.RowCount; i++)
            {
                int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeIDBorrow));
                if (employeeID > 0) continue;

                grvData.SetRowCellValue(i, colEmployeeIDBorrow, value);

            }
        }

        private void btnOpenSerial_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;

            // Lấy thông tin từ dòng đang focus
            int Quantity = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQuantity));
            if (Quantity == 0)
            {
                MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            int rowHandle = grvData.FocusedRowHandle;


            // Tạo model với ID = 0 (vì chưa lưu vào database)

            BillImportDetailTechnicalModel model = new BillImportDetailTechnicalModel()
            {
                ID = 0,
                Quantity = Quantity,
            };
            // Lưu oldtempSerial ban đầu (chỉ lần đầu)
            if (oldTempSerial.Count == 0)
            {
                oldTempSerial = grvData.GetRowCellDisplayText(rowHandle, colJoinedIDs)
                              .Split(',')
                              .Where(x => !string.IsNullOrEmpty(x))
                              .ToList();
            }
            List<string> lst = grvData.GetRowCellDisplayText(rowHandle, colJoinedIDs)
                              .Split(',')
                              .Where(x => !string.IsNullOrEmpty(x))
                              .ToList();
            lstSerial.Clear();
            foreach (string i in lst)
            {
                int id = TextUtils.ToInt(i);
                BillImportTechDetailSerialModel serialmodel = SQLHelper<BillImportTechDetailSerialModel>.FindByID(id);
                if (serialmodel != null)
                {
                    lstSerial.Add(serialmodel);
                }
            }

            using (frmBillTechnicalSerialNumber frm = new frmBillTechnicalSerialNumber(warehouseID))
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                if (id <= 0)
                {
                    frm.modelImportDetail = model;
                }
                else
                {
                    frm.modelImportDetail = SQLHelper<BillImportDetailTechnicalModel>.FindByID(id);
                    frm.quantity = Quantity;
                }


                frm.Type = 1;
                frm.lstSerialNumberImport = lstSerial;
                frm.lstSerialNumberid = lst;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    tempSerial = frm.lstSerialNumberid;
                    string joinedID = string.Join(",", tempSerial);
                    grvData.SetRowCellValue(rowHandle, colJoinedIDs, joinedID);
                    //string joinedIDs = string.Join(",", frm.lstSerialNumberid);
                    //if (string.IsNullOrEmpty(str))
                    //{
                    //    string tempSerials = string.Join(",", tempSerial);
                    //    grvData.SetRowCellValue(rowHandle, colJoinedIDs, tempSerials);
                    //}
                    //else 
                    //{
                    //    string[] cellvalue = str.Split(new char[] { ',' });
                    //    List<string> joinedIDs = cellvalue.ToList().Concat(frm.lstSerialNumberid).ToList();
                    //    string joinedID = string.Join(",", joinedIDs);
                    //    grvData.SetRowCellValue(rowHandle, colJoinedIDs, joinedID);
                    //}

                }
            }
        }
        void LoadStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            dt.Rows.Add(1, "Đã nhận");
            dt.Rows.Add(2, "Đã hủy nhận");
            dt.Rows.Add(3, "Không cần");

            cboDocumentStatus.DisplayMember = "Name";
            cboDocumentStatus.ValueMember = "ID";
            cboDocumentStatus.DataSource = dt;
        }

        private void grvData_ShowingEditor(object sender, CancelEventArgs e)
        {
            bool isColumn = grvData.FocusedColumn == colSomeBill
                || grvData.FocusedColumn == colDateSomeBill
                || grvData.FocusedColumn == colDPO
                || grvData.FocusedColumn == colTaxReduction
                || grvData.FocusedColumn == colCOFormE;

            if (isColumn) e.Cancel = (Global.UserID != billImport.DeliverID && !Global.IsAdmin);
        }
    }
}
