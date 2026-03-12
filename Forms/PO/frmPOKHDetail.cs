using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Printing;
using Forms.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPOKHDetail : _Forms
    {
        public int ID;
        public POKHModel oPOKH = new POKHModel();
        public QuotationKHModel quotationKH = new QuotationKHModel();
        ArrayList lstIDDelete = new ArrayList();
        DataTable dtProject = new DataTable();
        DataTable dtProduct = new DataTable();
        int IDDetail = 0;

        int warehouseID = 0;

        public frmPOKHDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmPOKH_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                loadProduct();
                loadContact();
                loadCode();
                loadProject();
                loadCustomer();
                loadMainIndex();
                loadUser();
                loadMaster();
                loadGrdData();
                loadDetailUser();
                LoadUpdatePO();
                cbProduct.EditValueChanged += new EventHandler(cbProduct_EditValueChanged);
                cbUserName.EditValueChanged += new EventHandler(cbUserName_EditValueChanged);
                btnDelet.Click += new EventHandler(btnDelete_Click);
                if (oPOKH.ID == 0)
                    cboStatus.SelectedIndex = 0;
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }


        }


        #region Methods
        /// <summary>
        /// Load thông tin nhân viên 
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeManager", "A", new string[] { "@group" }, new object[] { 0 });
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DataSource = dt;
            cbUser.DisplayMember = "FullName";
            cbUser.ValueMember = "UserID";
            cbUser.DataSource = dt;
        }
        /// <summary>
        /// Load Thông tin dự án
        /// </summary>
        void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,UserID,ContactID,CustomerID,ProjectName,PO From Project");
            cbProject.Properties.DisplayMember = "ProjectCode";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dtProject;
        }

        /// <summary>
        /// load type
        /// </summary>
        public void loadMainIndex()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID IN( 8,9,10,11,12,13,24)");
            cbType.Properties.DisplayMember = "MainIndex";
            cbType.Properties.ValueMember = "ID";
            cbType.Properties.DataSource = dt;
        }

        /// <summary>
        /// load data từ form báo giá khách hàng--convert báo giá->POKH
        /// </summary>
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
            grdData.DataSource = dt;

        }
        /// <summary>
        /// Load thông tin từ bảng Master
        /// </summary>
        void loadMaster()
        {
            if (oPOKH.ID > 0)
            {
                cboStatus.SelectedIndex = oPOKH.Status;
                cboUser.EditValue = oPOKH.UserID;
                txtPOCode.Text = oPOKH.POCode;
                cbProject.EditValue = oPOKH.ProjectID;
                dtpPOdate.Value = (DateTime)oPOKH.ReceivedDatePO;
                txtNote.Text = oPOKH.Note;
                txtTotalPO.Text = TextUtils.ToString(oPOKH.TotalMoneyPO);
                cboCustomer.EditValue = oPOKH.CustomerID;
                cbType.EditValue = oPOKH.POType;
                ckbBigAccount.Checked = oPOKH.NewAccount;
                txtEndUser.Text = oPOKH.EndUser;
                cbPart.EditValue = oPOKH.PartID;
                txtPONumber.Text = oPOKH.PONumber;
                ckbMerge.Checked = oPOKH.IsMerge;
                if (oPOKH.UserType == 1)
                    ckType.Checked = true;
                else
                    ckType.Checked = false;

            }
        }
        /// <summary>
        /// load thông tin khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select(" SELECT * FROM Customer where IsDeleted <> 1");
            cboCustomer.Properties.DisplayMember = "CustomerShortName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dt;
        }
        /// <summary>
        /// Load thông tin detail của phiếu
        /// </summary>
        void loadGrdData()
        {
            if (quotationKH.ID > 0)
            {
                sendData();
                return;
            }
            DataSet ds = new DataSet();
            ds = TextUtils.LoadDataSetFromSP("spGetPOKHDetail", new string[] { "@ID", "@IDDetail" }, new object[] { oPOKH.ID, IDDetail });
            grdData.DataSource = ds.Tables[0];
            grdDetailUser.DataSource = ds.Tables[1];
            calculateTotal();
            grvData.ExpandAllGroups();
        }
        /// <summary>
        /// Load các thông tin của sản phẩm 
        /// </summary>
        void loadProduct()
        {
            // if (cboGroup.Text == "") return;
            dtProduct = TextUtils.Select("SELECT ID,ProductNewCode,ProductCode,ProductName,ItemType,Unit,Maker FROM ProductSale");
            cbProduct.DisplayMember = "ProductNewCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;

        }
        /// <summary>
        /// Load thông tin liên hệ khách hàng
        /// </summary>
        void loadContact()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT ID,ContactName FROM CustomerContact");
            cbMaKH.DisplayMember = "ContactName";
            cbMaKH.ValueMember = "ID";
            cbMaKH.DataSource = dt;
            col.ColumnEdit = cbMaKH;
        }
        #endregion
        /// <summary>
        /// Kiểm tra trước khi save
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {

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
            return true;
        }



        void loadCbPart()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCustomerPart", "A", new string[] { "@ID" }, new object[] { TextUtils.ToInt(cboCustomer.EditValue) });
            cbPart.Properties.DisplayMember = "PartCode";
            cbPart.Properties.ValueMember = "ID";
            cbPart.Properties.DataSource = dt;
        }
        /// <summary>
        /// load và khởi tạo mã phiếu
        /// </summary>
        void loadCode()
        {
            if (oPOKH.ID > 0) return;
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
        /// <summary>
        /// Tính tổng chi phí bao gồm VAT
        /// </summary>
        void calculateTotal()
        {
            if (cGlobVar.LockEvents) return;
            decimal totalPO = 0;
            grvData.Focus();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                totalPO += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceIncludeVAT));
            }
            txtTotalPO.EditValue = totalPO;
        }
        /// <summary>
        /// set value các thông tin của dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUserName_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (cbProject.EditValue == null) return;
            DataRow[] row = dtProject.Select("ID=" + cbProject.EditValue);
            if (row.Length > 0)
            {
                cboUser.EditValue = row[0]["UserID"];
                cboCustomer.EditValue = row[0]["CustomerID"];
                txtPONumber.Text = TextUtils.ToString(row[0]["PO"]);
            }

        }
        /// <summary>
        /// set value các thông tin của sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.Focus();
                txtPOCode.Focus();
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
                DataRow[] rows = dtProduct.Select("ID = " + ID);
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string productcode = TextUtils.ToString(rows[0]["ProductCode"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    string maker = TextUtils.ToString(rows[0]["Maker"]);
                    grvData.SetFocusedRowCellValue(colProductName, productName);
                    grvData.SetFocusedRowCellValue(colProductCode, productcode);
                    grvData.SetFocusedRowCellValue(colUnit, unit);
                    grvData.SetFocusedRowCellValue(colMaker, maker);
                }
            }
            catch (Exception ex)
            { }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            checkStatus();
            if (saveData())
            {
                cPOStatus.AutoUpdateStatus(oPOKH.ID);
                loadGrdData();
                this.DialogResult = DialogResult.OK;
            }
        }

        bool IsPay;
        bool IsShip;
        bool IsBill;
        void loadDetailUser()
        {
            IDDetail = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHDetailMoney", "A", new string[] { "@IDDetail" }, new object[] { IDDetail });
            grdDetailUser.DataSource = dt;


        }
        void LoadUpdatePO()
        {
            if (grvData.RowCount > 0 && oPOKH.UserType == 1)
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    IDDetail = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHDetailMoney", "A", new string[] { "@IDDetail" }, new object[] { IDDetail });
                    grdDetailUser.DataSource = dt;
                    lstDt.Add(dt);
                }
        }
        void checkStatus()
        {
            IsPay = true;
            IsBill = true;
            string billText = "";
            int bill = 0;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                bill = TextUtils.ToInt(grvData.GetRowCellValue(i, colBillNumber));
                billText = TextUtils.ToString(grvData.GetRowCellValue(i, colBillNumber));
                if (bill == 0 && billText == "")
                    IsBill = false;
                return;
            }
        }
        /// <summary>
        /// Kiểm tra và lưu thông tin phiếu
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            grvData.FocusedRowHandle = -1;
            grvData_FocusedRowChanged(null, null);
            Recalcu();
            if (!ValidateForm()) return false;
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
            if (lstDeleteIDDetail.Count > 0)
                POKHDetailMoneyBO.Instance.Delete(lstDeleteIDDetail);
            oPOKH.Status = cboStatus.SelectedIndex;
            oPOKH.UserID = TextUtils.ToInt(cboUser.EditValue);
            oPOKH.POCode = TextUtils.ToString(txtPOCode.Text);
            oPOKH.ReceivedDatePO = dtpPOdate.Value;
            oPOKH.TotalMoneyPO = TextUtils.ToDecimal(txtTotalPO.EditValue);
            oPOKH.TotalMoneyKoVAT = TextUtils.ToDecimal(colIntoMoney.SummaryItem.SummaryValue);
            oPOKH.Note = TextUtils.ToString(txtNote.Text);
            oPOKH.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            oPOKH.PartID = TextUtils.ToInt(cbPart.EditValue);
            oPOKH.ProjectID = TextUtils.ToInt(cbProject.EditValue);
            oPOKH.POType = TextUtils.ToInt(cbType.EditValue);
            oPOKH.NewAccount = ckbBigAccount.Checked;
            oPOKH.Year = dtpPOdate.Value.Year;
            oPOKH.Month = dtpPOdate.Value.Month;
            oPOKH.EndUser = TextUtils.ToString(txtEndUser.Text);
            oPOKH.IsBill = IsBill;
            oPOKH.UserType = ckType.Checked ? 1 : 0;
            oPOKH.QuotationID = TextUtils.ToInt(txtQuotatiton.EditValue);
            oPOKH.PONumber = TextUtils.ToString(txtPONumber.Text);
            oPOKH.IsMerge = TextUtils.ToBoolean(ckbMerge.Checked);
            
            if (ckbBigAccount.Checked == true)
            {
                oPOKH.NewAccount = true;
            }
            else
            {
                oPOKH.NewAccount = false;
            }
            if (oPOKH.ID > 0)
            {
                POKHBO.Instance.Update(oPOKH);
            }
            else
            {
                oPOKH.ID = TextUtils.ToInt(POKHBO.Instance.Insert(oPOKH));
            }
            DataTable dt = TextUtils.Select($"Select * from POKHDetailMoney where POKHID={oPOKH.ID}");
            DataTable grv = (DataTable)grdData.DataSource;
            grv.AcceptChanges();
            for (int i = 0; i < grv.Rows.Count; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                POKHDetailModel detail = new POKHDetailModel();

                if (id > 0)
                {
                    detail = (POKHDetailModel)(POKHDetailBO.Instance.FindByPK(id));
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.POKHID = oPOKH.ID; //oPOKH.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
                detail.IntoMoney = detail.Qty * detail.UnitPrice;
                detail.FilmSize = TextUtils.ToString(grvData.GetRowCellValue(i, colFilmSize));
                detail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                detail.BillNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colBillNumber));
                detail.BillDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colBillDate));
                detail.TotalPriceIncludeVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceIncludeVAT));
                detail.DeliveryRequestedDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colDeliveryRequestedDate));
                detail.PayDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colPayDate));
                detail.EstimatedPay = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colEstimatedPay));
                detail.QuotationDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDQuota));
                detail.GuestCode = TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerCode));
                detail.GroupPO = TextUtils.ToString(grvData.GetRowCellValue(i, colGroup));
                detail.Debt = TextUtils.ToInt(grvData.GetRowCellValue(i, colDebt));
                detail.UserReceiver = TextUtils.ToString(grvData.GetRowCellValue(i, colUserReceiver));
                TextUtils.ExcuteSQL($"Update QuotationKHDetail set IsPO=1,POKHID={oPOKH.ID} where ID ={detail.QuotationDetailID}");
                if (detail.ID > 0)
                {
                    POKHDetailBO.Instance.Update(detail);
                }
                else
                {
                    detail.ID = TextUtils.ToInt(POKHDetailBO.Instance.Insert(detail));
                }

                if (ckType.Checked)
                {
                    for (int j = 0; j < lstDt[i].Rows.Count; j++)
                    {
                        if (lstDt[i].Rows.Count > 0)
                        {
                            int iduser = TextUtils.ToInt(lstDt[i].Rows[j]["ID"]);
                            int product = TextUtils.ToInt(lstDt[i].Rows[j]["RowHandle"]);
                            POKHDetailMoneyModel detailuser = new POKHDetailMoneyModel();
                            if (iduser > 0)
                            {
                                detailuser = (POKHDetailMoneyModel)(POKHDetailMoneyBO.Instance.FindByPK(iduser));
                            }
                            detailuser.POKHDetailID = detail.ID;
                            detailuser.POKHID = oPOKH.ID;
                            detailuser.PercentUser = TextUtils.ToDecimal(lstDt[i].Rows[j]["PercentUser"]);
                            detailuser.MoneyUser = TextUtils.ToDecimal(lstDt[i].Rows[j]["MoneyUser"]);
                            detailuser.RowHandle = TextUtils.ToInt(lstDt[i].Rows[j]["RowHandle"]);
                            detailuser.UserID = TextUtils.ToInt(lstDt[i].Rows[j]["UserID"]);
                            detailuser.STT = TextUtils.ToInt(lstDt[i].Rows[j]["STT"]);
                            detailuser.Month = TextUtils.ToInt(dtpPOdate.Value.Month);
                            detailuser.Year = TextUtils.ToInt(dtpPOdate.Value.Year);
                            if (detailuser.ID > 0)
                                POKHDetailMoneyBO.Instance.Update(detailuser);
                            else
                                POKHDetailMoneyBO.Instance.Insert(detailuser);
                        }
                    }
                }
                else
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
                    detailuser.MoneyUser = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                    detailuser.UserID = TextUtils.ToInt(cboUser.EditValue);
                    detailuser.Month = TextUtils.ToInt(dtpPOdate.Value.Month);
                    detailuser.Year = TextUtils.ToInt(dtpPOdate.Value.Year);
                    detailuser.RowHandle = TextUtils.ToInt(i);

                    if (detailuser.ID > 0)
                        POKHDetailMoneyBO.Instance.Update(detailuser);
                    else
                        POKHDetailMoneyBO.Instance.Insert(detailuser);

                }
            }
            return true;
        }
        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (frm != null)
                frm.Close();

        }
        #region Sự kiện button

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
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
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn mã sản phẩm [{0}] không?", ProductName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
        }
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            int soluong = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            decimal dongia = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));
            decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVAT));
            decimal thanhtien = soluong * dongia;
            try
            {
                if (dongia >= 0 && soluong > 0)
                {
                    if (e.Column == colQty || e.Column == colUnitPrice || e.Column == colVAT)
                    {
                        int a = e.RowHandle;
                        grvData.SetFocusedRowCellValue(colIntoMoney, thanhtien);
                        grvData.SetFocusedRowCellValue(colTotalPriceIncludeVAT, thanhtien + thanhtien * vat);
                        calculateTotal();
                    }

                }

                if (e.Column == colIntoMoney)
                    caculMonmey();
                if (e.Column == colVAT)
                {
                    DataTable dt = grdData.DataSource as DataTable;
                    dt.AcceptChanges();
                    for (int i = e.RowHandle; i < dt.Rows.Count; i++)
                    {
                        decimal vatold = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                        string vatoldtext = TextUtils.ToString(grvData.GetRowCellValue(i, colVAT));
                        if (vatold == 0 && vat != 0 && vatoldtext == "")
                            grvData.SetRowCellValue(i, colVAT, vat);
                    }
                }
            }
            catch
            {

            }
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
        #endregion

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            //if (cGlobVar.LockEvents) return;
            loadCode();
            loadCbPart();
        }

        private void btnNewProjetc_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        private void ckbBigAccount_CheckedChanged(object sender, EventArgs e)
        {

        }

        int stt;
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colSTT && info.InColumnPanel)
                {
                    AddNewRow();
                }
            }
        }

        private void txtReceivedDatePO_ValueChanged(object sender, EventArgs e)
        {

        }
        DataRow AddNewRow()
        {
            DataRow newRow = null;
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;
            if (dt.Rows.Count == 0)
                STT = 1;
            else
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;

            newRow = dt.NewRow();
            newRow["STT"] = STT;
            dt.Rows.Add(newRow);
            //newRowDataSource = dt;

            return newRow;
        }
        private void DataReceive(List<string> lstcode, string group, DataTable dt)
        {
            cGlobVar.LockEvents = true;
            checkDataStock(lstcode, dt);
            genProductCode(lstcode, dt);
            cGlobVar.LockEvents = false;
        }

        /// <summary>
        /// kiểm tra sp trong kho, nếu không có thì tự khởi tạo
        /// </summary>
        /// <param name="lstcode"></param>
        /// <param name="dtproduct"></param>
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

        /// <summary>
        /// Gen tự động sản phẩm trong kho theo mã code
        /// </summary>
        /// <param name="lstcode"></param>
        void genProductCode(List<string> lstcode, DataTable dtquo)
        {
            string result = MyLib.ListToStringSQL(lstcode);
            DataTable dt = TextUtils.Select($"Select ID from ProductSale where ProductNewCode in ({result})");
            for (int i = 0; i < lstcode.Count; i++)
            {
                AddNewRow();
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
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductID, ID);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, productName);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductCode, productCode);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, unit);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colMaker, maker);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colIDQuota, IDQuota);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colQty, Qty);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnitPrice, UnitPrice);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colIntoMoney, IntoMoney);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colGroup, group);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, colProductID, ID);
                        grvData.SetRowCellValue(i, colProductCode, productName);
                        grvData.SetRowCellValue(i, colProductName, productCode);
                        grvData.SetRowCellValue(i, colUnit, unit);
                        grvData.SetRowCellValue(grvData.RowCount, colIDQuota, IDQuota);
                        grvData.SetRowCellValue(grvData.RowCount, colQty, Qty);
                        grvData.SetRowCellValue(grvData.RowCount, colUnitPrice, UnitPrice);
                        grvData.SetRowCellValue(grvData.RowCount, colIntoMoney, IntoMoney);
                        grvData.SetRowCellValue(grvData.RowCount, colGroup, group);
                    }
                }
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

        private void grdData_Click(object sender, EventArgs e)
        {

        }
        void addNewRowUser()
        {
            grvDetailUser.AddNewRow();
            grvDetailUser.FocusedRowHandle = -1;
            int row = TextUtils.ToInt(grvData.FocusedRowHandle);
            grvDetailUser.SetRowCellValue(grvDetailUser.RowCount - 1, colRowHandle, row);
        }

        private void grvDetailUser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetailUser.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column == colSTTUser && e.Y < 40)
                {
                    addNewRowUser();
                }
            }
        }
        List<DataTable> lstDt = new List<DataTable>();
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (cGlobVar.LockEvents) return;

            if (grvData.RowCount != lstDt.Count)
            {
                DataTable dt = (DataTable)grdDetailUser.DataSource;
                if (!lstDt.Contains(dt))
                {
                    lstDt.Add(dt);
                }
            }
            try
            {
                loadDetailUser();
                if (e != null)
                    grdDetailUser.DataSource = lstDt[e.FocusedRowHandle];
            }
            catch { }
        }
        ArrayList lstDeleteIDDetail = new ArrayList();
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataTable dt = (DataTable)grdDetailUser.DataSource;
                int id = TextUtils.ToInt(grvDetailUser.GetFocusedRowCellValue(colIDDetailUser));
                lstDeleteIDDetail.Add(id);
                grvDetailUser.DeleteSelectedRows();
                dt.AcceptChanges();
            }

        }
        void Recalcu()
        {
            DataTable dt = grdData.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                decimal thanhtien = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                grvData.SetRowCellValue(i, colTotalPriceIncludeVAT, thanhtien + thanhtien * vat);
            }
        }
        void caculMonmey()
        {
            if (cGlobVar.LockEvents) return;
            for (int i = 0; i < grvDetailUser.RowCount; i++)
            {
                decimal total = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colIntoMoney));
                decimal Per = TextUtils.ToDecimal(grvDetailUser.GetRowCellValue(i, colPercentUser));
                grvDetailUser.SetRowCellValue(i, colMoneyUser, total * Per);
            }

        }
        private void grvDetailUser_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (e.Column == colPercentUser)
            {
                caculMonmey();
            }
        }

        private void grdDetailUser_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        bool IsEnable = false;
        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

        }



        private void ckType_CheckedChanged(object sender, EventArgs e)
        {
            if (ckType.Checked)
            {
                grdDetailUser.Enabled = true;
            }
            else
            {
                grdDetailUser.Enabled = false;
            }
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

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string checkvalue = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, colIntoMoney));
            if ((e.Column == colIntoMoney && checkvalue != "") || e.Column == colUnitPrice || e.Column == colQty || e.Column == colVAT || e.Column == colTotalPriceIncludeVAT || e.Column == colBillNumber)
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
                colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                colUnitPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                colGroup.GroupIndex = 0;
                grvData.ExpandAllGroups();
                grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            }
            else
            {
                colGroup.GroupIndex = -1;
                grvData.OptionsView.AllowCellMerge = false;
                grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                colUnitPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            }

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowfocus = grvData.FocusedRowHandle;
                grvData.FocusedRowHandle = rowfocus + 1;
            }
            if (e.KeyCode == Keys.C && e.Control)
            {
                grvData.CopyToClipboard();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                if (grvData.FocusedColumn == colProductID)
                {
                    try
                    {
                        cGlobVar.LockEvents = true;
                        string[] arChar = { "\r\n" };
                        List<string> lstCode = Clipboard.GetText().Split(arChar, StringSplitOptions.None).ToList();
                        if (!lstCode.Any()) return;
                        if (lstCode[lstCode.Count - 1] == "")
                            lstCode.RemoveAt(lstCode.Count - 1);
                        DataTable dt = (DataTable)cbProduct.DataSource;
                        for (int i = 0; i < lstCode.Count; i++)
                        {
                            int rowCount = grvData.RowCount - (grvData.FocusedRowHandle + 1);
                            if (lstCode.Count > rowCount + 1)
                                AddNewRow();
                            DataRow[] dtr = dt.Select($"ProductNewCode='{lstCode[i]}'");
                            if (dtr.Length == 0) return;
                            grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductID, dtr[0]["ID"]);
                            grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductName, dtr[0]["ProductName"]);
                            grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colProductCode, dtr[0]["ProductCode"]);
                            grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colUnit, dtr[0]["Unit"]);
                            grvData.SetRowCellValue(grvData.FocusedRowHandle + i, colMaker, dtr[0]["Maker"]);
                        }
                    }
                    finally
                    {
                        e.Handled = true;
                        cGlobVar.LockEvents = false;
                    }
                }
                else
                {

                    int[] selectedRow = grvData.GetSelectedRows();
                    GridCell[] selectedColumn = grvData.GetSelectedCells();

                    List<GridColumn> listCol = new List<GridColumn>();

                    for (int i = 0; i < selectedColumn.Length; i++)
                    {
                        GridColumn colSelect = selectedColumn[i].Column;
                        listCol.Add(colSelect);
                    }

                    string[] separator = { "\r\n" };
                    var data = Clipboard.GetText();

                    List<string> listDataClipboard = Clipboard.GetText().Split(separator, StringSplitOptions.None).ToList();
                    foreach (string item in listDataClipboard.ToList())
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            listDataClipboard.Remove(item);
                        }
                    }

                    if (listDataClipboard.Count <= 1)
                    {

                        if (selectedRow.Length > 1 || selectedColumn.Length > 1)
                        {
                            for (int i = 0; i < selectedRow.Length; i++)
                            {
                                for (int j = 0; j < selectedColumn.Length; j++)
                                {
                                    grvData.SetRowCellValue(i, selectedColumn[j].Column, listDataClipboard[0]);
                                }

                            }
                        }
                        else
                        {
                            grvData.SetRowCellValue(grvData.FocusedRowHandle, selectedColumn[0].Column, listDataClipboard[0]);
                        }
                        //for (int i = 0; i < selectedRow.Length; i++)
                        //{
                        //    for (int j = 0; j < selectedColumn.Length; j++)
                        //    {
                        //        grvData.SetRowCellValue(i, selectedColumn[j].Column, listDataClipboard[0]);
                        //    }

                        //}
                    }
                    else
                    {
                        grvData.FocusedColumn = selectedColumn[0].Column;
                        grvData.FocusedRowHandle = selectedRow[0];

                        grvData.PasteFromClipboard();
                    }

                    //grvData.PasteFromClipboard();
                    //string[] arChar = { "\r\n" };
                    //List<string> lstValue = Clipboard.GetText().Split(arChar, StringSplitOptions.None).ToList();
                    //if (lstValue[lstValue.Count - 1] == "")
                    //    lstValue.RemoveAt(lstValue.Count - 1);
                    //for (int i = 0; i < (lstValue.Count); i++)
                    //{
                    //    int rowCount = grvData.RowCount - (grvData.FocusedRowHandle + 1);
                    //    if (lstValue.Count > rowCount + 1)
                    //        AddNewRow();
                    //}

                }
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

        private void dragDropEvents1_DragOver(object sender, DragOverEventArgs e)
        {
            DragOverGridEventArgs args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
            e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            Cursor.Current = args.Cursor;
            args.Handled = true;
        }

        private void dragDropEvents1_DragDrop(object sender, DragDropEventArgs e)
        {
            GridView targetGrid = e.Target as GridView;
            GridView sourceGrid = e.Source as GridView;
            if (e.Action == DragDropActions.None || targetGrid != sourceGrid)
                return;
            DataTable sourceTable = sourceGrid.GridControl.DataSource as DataTable;

            Point hitPoint = targetGrid.GridControl.PointToClient(Cursor.Position);
            GridHitInfo hitInfo = targetGrid.CalcHitInfo(hitPoint);

            int[] sourceHandles = e.GetData<int[]>();

            int targetRowHandle = hitInfo.RowHandle;
            int targetRowIndex = targetGrid.GetDataSourceRowIndex(targetRowHandle);

            List<DataRow> draggedRows = new List<DataRow>();
            foreach (int sourceHandle in sourceHandles)
            {
                int oldRowIndex = sourceGrid.GetDataSourceRowIndex(sourceHandle);
                DataRow oldRow = sourceTable.Rows[oldRowIndex];
                draggedRows.Add(oldRow);
            }

            int newRowIndex;

            switch (e.InsertType)
            {
                case InsertType.Before:
                    newRowIndex = targetRowIndex > sourceHandles[sourceHandles.Length - 1] ? targetRowIndex - 1 : targetRowIndex;
                    for (int i = draggedRows.Count - 1; i >= 0; i--)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        grvData.SetRowCellValue(i, colSTT, i + 1);
                    }
                    break;
                case InsertType.After:
                    newRowIndex = targetRowIndex < sourceHandles[0] ? targetRowIndex + 1 : targetRowIndex;
                    for (int i = 0; i < draggedRows.Count; i++)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }

                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        grvData.SetRowCellValue(i, colSTT, i + 1);
                    }
                    break;
                default:
                    newRowIndex = -1;
                    break;
            }
            int insertedIndex = targetGrid.GetRowHandle(newRowIndex);
            targetGrid.FocusedRowHandle = insertedIndex;
            targetGrid.SelectRow(targetGrid.FocusedRowHandle);
        }

        private void grvData_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("hello bạn nhỏ");
        }

        private void btnSetSTT_Click(object sender, EventArgs e)
        {
            int[] selectedRow = grvData.GetSelectedRows();

            var data = Clipboard.GetText();
            for (int i = 0; i < selectedRow.Length; i++)
            {
                grvData.SetRowCellValue(i, colCustomerCode, data);
            }
            //var selectedCol = grvData.GetSelectedCells();
        }

        private void grvData_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //DateTime time = DateTime.Now;
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));
            if (e.Column == colDeliveryRequestedDate)
            {
                switch (status)
                {
                    case 0:
                        e.Appearance.BackColor = Color.Green;

                        break;
                    case 1:
                        e.Appearance.BackColor = Color.Yellow;
                        break;
                    case 2:
                        e.Appearance.BackColor = Color.Green;

                        break;
                    case 3:
                        //e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                        e.Appearance.BackColor = Color.Yellow;
                        break;
                    case 4:
                        e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                        break;
                    case 5:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
