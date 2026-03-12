using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRequestBuyDetail : _Forms
    {
        public int IDDetail;
        public PONCCDetailModel oConvert = new PONCCDetailModel();
        public RequestBuyModel requestBuy = new RequestBuyModel();
        List<int> lstIDDelete = new List<int>();
        DataTable dtProduct = new DataTable();
        public bool IsCopy;

        /// <summary>
        ///  khai báo delegate chuyền giữa 2 form
        /// </summary>
        /// <param name="lst"></param>
        public delegate void ListID(List<int> lst);

        public frmRequestBuyDetail()
        {
            InitializeComponent();
        }

        private void frmRequestBuyDetail_Load(object sender, EventArgs e)
        {
            loadRequestCode();
            loadUser();
            loadCustomer();
            loadProject();
            loadSupplier();
            loadProduct();
            loadMonitor(); // ng phụ trách
            loadPOKH();
            loadRequestBuyDetail();

            this.cbProduct.EditValueChanged += new System.EventHandler(cbProduct_EditValueChanged);
        }

        #region Methods
        /// <summary>
        /// load mã yêu cầu mua hàng
        /// </summary>
        void loadRequestCode()
        {
            if (requestBuy.ID == 0)
            {
                //cbUser.EditValue = Global.UserID;
                txtCode.Text = TextUtils.CreateNewCode("RequestBuy", "RequestBuyCode", "YCMH");
                this.Text = "THÊM " + this.Text;
            }
            else
            {
                this.Text = "SỬA " + this.Text + ": " + requestBuy.RequestBuyCode;
            }
        }
        /// <summary>
        /// load ra danh sách sản phẩm
        /// </summary>
        void loadProduct()
        {
            dtProduct = TextUtils.Select("SELECT * FROM ProductSale");
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
            colProductID.ColumnEdit = cbProduct;
        }

        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// Lấy danh sách PO KH
        /// </summary>
        void loadPOKH()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.POKH");
            cbPOKH.Properties.DisplayMember = "POCode";
            cbPOKH.Properties.ValueMember = "ID";
            cbPOKH.Properties.DataSource = dt;
        }

        /// <summary>
        /// lấy ra danh sách ng phụ trách
        /// </summary>
        void loadMonitor()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cbMonitor.DisplayMember = "FullName";
            cbMonitor.ValueMember = "ID";
            cbMonitor.DataSource = dt;
            colMonitorID.ColumnEdit = cbMonitor;
        }

        /// <summary>
        /// Lấy danh sách khách hàng lên combo chọn
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 ORDER BY ID desc");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }

        void loadProject()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Project");
            cbProject.Properties.DisplayMember = "ProjectName";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dt;
        }

        void loadSupplier()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.SupplierSale order by ID desc");
            cbSupplier.Properties.DataSource = dt;
            cbSupplier.Properties.DisplayMember = "NameNCC";
            cbSupplier.Properties.ValueMember = "ID";
        }

        private void loadRequestBuyDetail()
        {
            if (requestBuy.ID > 0)
            {
                cbProject.EditValue = requestBuy.ProjectID;
                cbCustomer.EditValue = requestBuy.CustomerID;
                cboStatus.SelectedIndex = requestBuy.RequestBuyStatus;
                cbUser.EditValue = requestBuy.RequestPersonID;
                cbSupplier.EditValue = requestBuy.SupplierID;
                txtCode.Text = requestBuy.RequestBuyCode;
                txtPurpose.Text = requestBuy.Purpose;
                txtNote.Text = requestBuy.Note;
            }

            DataTable dt = TextUtils.LoadDataFromSP("spGetRequestBuyDetail", "A"
               , new string[] { "@ID" }
               , new object[] { requestBuy.ID });

            if (IsCopy)
            {
                requestBuy.ID = 0;
                int count = dt.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dt.Rows[i]["ID"] = 0;
                }
            }
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtCreatedDate.Text = TextUtils.ToString(dt.Rows[0]["CreatedDate"]);

        }
        #endregion

        private bool checkValid()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã yêu cầu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else
            //{
            //    int strID = requestBuy.ID;
            //    if (TextUtils.CheckExistTable(strID, "RequestBuyCode", txtCode.Text.Trim(), "RequestPrice"))
            //    {
            //        MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        return false;
            //    }
            //}

            if (cbCustomer.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn một khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cbUser.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn một người yêu cầu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái yêu cầu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (grvData.RowCount <= 0)
            {
                MessageBox.Show("Xin hãy chọn vật tư yêu cầu vào danh sách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!checkDetail())
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin vật tư(mã, tên).", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        bool checkDetail()
        {
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(i, colProductID));
                string name = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))//|| string.IsNullOrWhiteSpace(supplier))
                {
                    return false;
                }
            }
            return true;
        }

        #region Button Events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            { this.DialogResult = DialogResult.OK; }
        }

        /// <summary>
        /// click button save new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                cboStatus.Text = "";
                cbProject.Text = "";
                cbCustomer.Text = "";
                cbUser.Text = "";
                cbSupplier.Text = "";
                txtNote.Clear();
                txtPurpose.Clear();
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                requestBuy = new RequestBuyModel();
                loadRequestCode();
            }
        }

        /// <summary>
        /// click button thêm dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
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
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;
        }

        /// <summary>
        /// click button xóa dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa sản phẩm [{0}] không?", productName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
        }

        /// <summary>
        /// click button thêm đơn vị tính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewUnit_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }

        private void btnPOKH_Click(object sender, EventArgs e)
        {
            frmPOKHData frm = new frmPOKHData(0);
            frm._listID = new ListID(Lst);
            frm.ShowDialog();
        }
        // dùng delegate
        int count;
        void Lst(List<int> LstID)
        {
            if (LstID.Count == 0)
            {
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                return;
            }
            else
            {
                for (int i = 0; i < LstID.Count; i++)
                {
                    btnAdd_Click(null, null);
                    int productID = TextUtils.ToInt(LstID[i]);
                    if (grvData.RowCount > 1)
                        count = grvData.RowCount - 1;
                    else
                        count = 0;
                    grvData.SetRowCellValue(count, colProductID, productID);

                    DataRow[] rows = dtProduct.Select("ID = " + productID);
                    if (rows.Length > 0)
                    {
                        string productName = TextUtils.ToString(rows[0]["ProductName"]);
                        string unit = TextUtils.ToString(rows[0]["Unit"]);
                        string maker = TextUtils.ToString(rows[0]["Maker"]);
                        grvData.SetRowCellValue(count, colProductName, productName);
                        grvData.SetRowCellValue(count, colUnit, unit);
                        grvData.SetRowCellValue(count, colMaker, maker);
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// hàm save Data
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            try
            {
                if (!checkValid()) return false;

                requestBuy.RequestBuyStatus = cboStatus.SelectedIndex;
                //requestBuy.RequestBuyCode = TextUtils.CreateNewCode("RequestBuy", "RequestBuyCode", "YCMH");
                requestBuy.RequestBuyCode = txtCode.Text.Trim();
                requestBuy.ProjectID = TextUtils.ToInt(cbProject.EditValue);
                requestBuy.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
                requestBuy.SupplierID = TextUtils.ToInt(cbSupplier.EditValue);
                requestBuy.Purpose = txtPurpose.Text.Trim();
                requestBuy.CreatedDate = TextUtils.ToDate3(txtCreatedDate.Value);
                requestBuy.RequestPersonID = TextUtils.ToInt(cbUser.EditValue);
                requestBuy.Note = txtNote.Text.Trim();
                requestBuy.QtySet = 1;
                requestBuy.IsImport = false;

                //if (requestBuy.ID == 0)
                //{
                //    requestBuy.ID = (int)RequestPriceBO.Instance.Insert(requestBuy);
                //    //Send email
                //    DataTable dt = TextUtils.Select("select emailcom from users where departmentid = 4");

                //    //Insert notify cho phòng mua
                //    Lib.ExcuteProcedure("spInsertRequestPriceNotify", new string[] { "RequestBuyID", "RequestBuyCode" }, new object[] { requestBuy.ID, requestBuy.RequestBuyCode });

                //    List<string> emailTo = new List<string>();
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        emailTo.Add(TextUtils.ToString(dt.Rows[i]["emailcom"]));
                //    }
                //    TextUtils.SetEmailSend("Yêu cầu hỏi giá: " + requestBuy.RequestBuyCode, "Dear phòng mua,<br>Nhờ phòng mua hỏi giúp giá YC-" + requestBuy.RequestBuyCode,
                //        string.Join(";", emailTo), "");
                //}
                //else
                //{
                //    RequestPriceBO.Instance.Update(requestBuy);
                //}

                if (requestBuy.ID > 0)
                {
                    RequestPriceBO.Instance.Update(requestBuy);
                }
                else
                {
                    requestBuy.ID = (int)RequestPriceBO.Instance.Insert(requestBuy);
                }

                int count = grvData.RowCount;
                for (int i = 0; i < count; i++)
                {
                    long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                    RequestBuyDetailModel detail = new RequestBuyDetailModel();
                    if (id > 0)
                    {
                        detail = (RequestBuyDetailModel)RequestBuyDetailBO.Instance.FindByPK(id);
                    }

                    detail.RequestBuyID = requestBuy.ID;
                    detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colAdd));
                    detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));//ID Sản phẩm
                    detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                    detail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                    detail.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPrice));
                    detail.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                    detail.PriceCurrency = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceCurrency));
                    detail.TotalPriceCurrency = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceCurrency));
                    detail.TaxImportPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImportPercent));
                    detail.TaxImporPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImporPrice));
                    detail.TaxImporTotal = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImporTotal));
                    detail.DeliveryCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDeliveryCost));
                    detail.BankCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBankCost));
                    detail.CustomsCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCustomsCost));
                    detail.FinishPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                    detail.FinishTotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
                    detail.PriceVAT = detail.VAT * detail.Price / 100;
                    detail.TotalVAT = detail.Qty * detail.PriceVAT;
                    detail.QtySet = requestBuy.QtySet;
                    detail.QtyPS = detail.Qty;
                    detail.PricePS = detail.FinishPrice;

                    //detail.SupplierID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierID));
                    detail.MonitorID = TextUtils.ToInt(grvData.GetRowCellValue(i, colMonitorID)); // người phụ trách

                    detail.PeriodExpected = TextUtils.ToString(grvData.GetRowCellValue(i, colPeriodExpected));
                    if (detail.ID == 0)
                    {
                        RequestBuyDetailBO.Instance.Insert(detail);
                    }
                    else
                    {
                        RequestBuyDetailBO.Instance.Update(detail);
                        foreach (int item in lstIDDelete)
                        {
                            BillExportDetailBO.Instance.Delete(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return true;
        }

        //hàm khi chọn cboName -> tự động sinh ra tên,ĐVT
        //private void cbProduct_EditValueChanged(object sender, EventArgs e)
        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            grvData.Focus();
            txtCode.Focus();
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            DataRow[] rows = dtProduct.Select("ID = " + ID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string unit = TextUtils.ToString(rows[0]["Unit"]);
                string maker = TextUtils.ToString(rows[0]["Maker"]);
                grvData.SetFocusedRowCellValue(colProductName, productName);
                grvData.SetFocusedRowCellValue(colUnit, unit);
                grvData.SetFocusedRowCellValue(colMaker, maker);
            }
        }

        /// <summary>
        /// hàm tính toán số liệu
        /// </summary>
        /// <param name="rowIndex"></param>
        void calculate(int rowIndex)
        {
            decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colPrice));
            decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colQty));
            decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colVAT));
            decimal delivery = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colDeliveryCost));

            decimal totalPrice = qty * price;
            decimal totalVAT = qty * price * vat / 100;
            decimal fTotalPrice = totalPrice + totalVAT + delivery;// + totalTax  + bank + customs;
            decimal fPrice = qty > 0 ? fTotalPrice / qty : 0;

            grvData.SetRowCellValue(rowIndex, colTotalPrice, totalPrice);
            grvData.SetRowCellValue(rowIndex, colTotalVAT, totalVAT);
            grvData.SetRowCellValue(rowIndex, colFinishPrice, fPrice);
            grvData.SetRowCellValue(rowIndex, colFinishTotalPrice, fTotalPrice);
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colPrice || e.Column == colQty || e.Column == colTaxImportPercent || e.Column == colVAT
               || e.Column == colDeliveryCost || e.Column == colBankCost || e.Column == colCustomsCost)
            {
                calculate(e.RowHandle);
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvProject.GetFocusedRowCellValue(colCustomerID));
            cbCustomer.EditValue = id;
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colAdd)
                {
                    btnAdd_Click(null, null);
                }
                //if (info.Column != null && info.Column == colDelete)
                //{
                //    btnDelete_Click(null, null);
                //}
            }

        }

        private void cbPODetail_EditValueChanged(object sender, EventArgs e)
        {
            //grvData.Focus();
            //txtCode.Focus();
            //string ID = TextUtils.ToString(cbPODetail.EditValue);
            //DataTable dt = TextUtils.Select($"SELECT p.*,po.UserID,ps.ProductCode,ps.ProductName,ps.Unit,ps.Maker FROM PONCCDetail as p INNER JOIN dbo.PONCC as po on po.ID = p.PONCCID inner join dbo.ProductSale as ps on ps.ID = p.ProductID where p.ID IN ({ID})");
            //if (cbPODetail.Text == "")
            //{
            //    for (int i = grvData.RowCount - 1; i >= 0; i--)
            //    {
            //        grvData.DeleteRow(i);
            //    }
            //    return;
            //}
            //DataRow[] rows = dt.Select();
            //if (rows.Length > 0)
            //{
            //    for (int i = 0; i < rows.Length; i++)
            //    {
            //        btnAdd_Click(null, null);
            //        int productID = TextUtils.ToInt(rows[i]["ProductID"]);
            //        int qty = TextUtils.ToInt(rows[i]["Qty"]);
            //        int userID = TextUtils.ToInt(rows[i]["UserID"]);
            //        grvData.SetRowCellValue(i, colProductID, productID);
            //        grvData.SetRowCellValue(i, colQty, qty);
            //        grvData.SetRowCellValue(i, colMonitorID, userID);

            //        string productName = TextUtils.ToString(rows[i]["ProductName"]);
            //        string unit = TextUtils.ToString(rows[i]["Unit"]);
            //        string maker = TextUtils.ToString(rows[i]["Maker"]);
            //        grvData.SetRowCellValue(i, "ProductName", productName);
            //        grvData.SetRowCellValue(i, "Unit", unit);
            //        grvData.SetRowCellValue(i, "Maker", maker);
            //    }
            //}
        }
    }
}
