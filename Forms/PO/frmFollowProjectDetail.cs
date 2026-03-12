using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class frmFollowProjectDetail : _Forms
    {
        int ad, solgPOKH, solg, code;
        decimal USD, thue, sotokhai, haiquan, baogia;
        public int Id = 0;
        public FollowProjectModel _bill = new FollowProjectModel();
        public POKHModel oConvert = new POKHModel();
        DataTable dtProject = new DataTable();

        int warehouseID = 1;
        public frmFollowProjectDetail()
        {
            InitializeComponent();
        }

        void loadData()
        {
            DataTable dt = TextUtils.Select("select top 1 * from ConfigPrice order by ID desc");
            txtVND.Text = TextUtils.ToString(dt.Rows[0]["Exchange"]);
            txtPhi1LanDien.Text = TextUtils.ToString(dt.Rows[0]["BankCharges"]);
            txtSoLanDien.Text = TextUtils.ToString(dt.Rows[0]["NumberOfTransactions"]);
            txtChiPhiHaiQuan.Text = TextUtils.ToString(dt.Rows[0]["CustomFees"]);
            txtSoToKhai.Text = TextUtils.ToString(dt.Rows[0]["Declaration"]);
            txtPhiVanChuyen.Text = TextUtils.ToString(dt.Rows[0]["TransportFee"]);
        }
        void loadGrdData()
        {
            if (_bill.ID > 0)
            {

                txtPOCode.Text = _bill.POCode;
                txtDateTime.Value = TextUtils.ToDate3(_bill.CreateDate);
                cbCustomer.EditValue = _bill.CustomerID;
                cbProject.EditValue = _bill.ProjectID;
                cbUser.EditValue = _bill.UserID;
                txtTongchiphikoVAT.Text = TextUtils.ToString(_bill.TotalCostIncludingVAT);
                txtTongchiphicoVAT.Text = TextUtils.ToString(_bill.TotalCostWithoutVAT);
                txtTongChiPhiNganHang.Text = TextUtils.ToString(_bill.TotalBankCharges);
                txtThueVAT.Text = TextUtils.ToString(_bill.Tax);
                txtVND.Text = TextUtils.ToString(_bill.Exchange);
                txtPhiVanChuyen.Text = TextUtils.ToString(_bill.TransportFee);
                txtChiPhiHaiQuan.Text = TextUtils.ToString(_bill.CustomFees);
                txtSoToKhai.Text = TextUtils.ToString(_bill.Declaration);
                txtPhi1LanDien.Text = TextUtils.ToString(_bill.BankCharges);
                txtSoLanDien.Text = TextUtils.ToString(_bill.NumberOfTransactions);
            }

            DataTable dt = TextUtils.LoadDataFromSP("spLoadFollowProject", "A", new string[] { "@ID" }, new object[] { _bill.ID });
            grdData.DataSource = dt;

        }
        private void frmBillImport_Load(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();
                cGlobVar.LockEvents = true;
                loadUsers();
                if (_bill.ID == 0)
                    loadData();
                loadProject();
                loadCustomer();
                loadProduct();
                loadGrdData();
                cbProductt.EditValueChanged += new EventHandler(cbProductt_EditValueChanged);
                btnDele.Click += new EventHandler(btnDelete_Click);
            }
            finally
            {
              
                cGlobVar.LockEvents = false;
                this.ResumeLayout();
            }

        }
        public void loadUsers()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }
        /// <summary>
        /// Load Thông tin dự án
        /// </summary>
        void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,UserID,ContactID,CustomerID,ProjectName From Project");
            cbProject.Properties.DisplayMember = "ProjectCode";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dtProject;
        }
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select(" SELECT * FROM Customer where IsDeleted <> 1");
            cbCustomer.Properties.DisplayMember = "CustomerCode";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        DataTable dtproduct = new DataTable();
        void loadProduct()
        {
            dtproduct = TextUtils.Select(" SELECT ID,ProductName,ProductNewCode,ProductCode,Maker FROM ProductSale");
            cbProductt.DisplayMember = "ProductNewCode";
            cbProductt.ValueMember = "ID";
            cbProductt.DataSource = dtproduct;
        }
        private void cbProductt_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            grvData.FocusedRowHandle = -1;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductNewCode));
            DataRow[] dtrow = dtproduct.Select("ID=" + ID);
            if (dtrow.Length == 0) return;
            grvData.SetFocusedRowCellValue(colMaker, dtrow[0]["Maker"]);
            grvData.SetFocusedRowCellValue(colPartner, dtrow[0]["Maker"]);
            grvData.SetFocusedRowCellValue(colStandardModel, dtrow[0]["ProductCode"]);
            grvData.SetFocusedRowCellValue(colProductID, dtrow[0]["ProductName"]);
            decimal oldprice= TextUtils.ToDecimal( TextUtils.Select($"Select top 1 UnitPrice From FollowProjectDetail where ProductID ={ID} order by ID desc"));
            grvData.SetFocusedRowCellValue(colOldPrice, oldprice);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            if (saveData())
            {
                this.Close();
            }
        }
        private bool ValidateForm()
        {
            if (_bill.ID == 0)
            {
                if (cbProject.Text.Trim() == "")
                {
                    MessageBox.Show("Xin hãy điền Mã dự án.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else
                {
                    DataTable dt;
                    if (_bill.ID > 0)
                    {
                        int strID = _bill.ID;
                        dt = TextUtils.Select($"select top 1 ID from FollowProject where POCode = '{txtPOCode.Text.Trim()}' and ID <> {strID}");
                    }
                    else
                    {
                        dt = TextUtils.Select($"select top 1 ID from FollowProject where ProjectCode = '{txtPOCode.Text.Trim()}'");
                    }
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Mã PO đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return false;
                        }
                    }
                }
            }
            if (txtChiPhiHaiQuan.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin và nhập liệu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtChiPhiHaiQuan.Focus();
                return false;
            }
            if (txtPhi1LanDien.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin và nhập liệu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPhi1LanDien.Focus();
                return false;
            }
            if (txtSoToKhai.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin và nhập liệu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSoToKhai.Focus();
                return false;
            }

            if (txtVND.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin và nhập liệu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtVND.Focus();
                return false;
            }
            if (txtPhiVanChuyen.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin và nhập liệu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPhiVanChuyen.Focus();
                return false;
            }
            if (txtSoLanDien.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin và nhập liệu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSoLanDien.Focus();
                return false;
            }



            return true;
        }
        bool saveData()
        {
            if (!ValidateForm())
                return false;
            if (lstDelete.Count > 0)
                FollowProjectDetailBO.Instance.Delete(lstDelete);
            _bill.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
            _bill.POCode = TextUtils.ToString(txtPOCode.Text);
            _bill.ProjectID = TextUtils.ToInt(cbProject.EditValue);
            _bill.UserID = TextUtils.ToInt(cbUser.EditValue);
            _bill.POKHID = oConvert.ID;
            _bill.TotalCostIncludingVAT = TextUtils.ToDecimal(txtTongchiphicoVAT.Text);
            _bill.TotalCostWithoutVAT = TextUtils.ToDecimal(txtTongchiphikoVAT.Text);
            _bill.TotalBankCharges = TextUtils.ToDecimal(txtTongChiPhiNganHang.Text);
            _bill.Tax = TextUtils.ToDecimal(txtThueVAT.Text);
            _bill.CreateDate = txtDateTime.Value;
            _bill.POKHID = TextUtils.ToInt(txtPOKHID.EditValue);
            _bill.Exchange = TextUtils.ToDecimal(txtVND.Text);
            _bill.TransportFee = TextUtils.ToDecimal(txtPhiVanChuyen.Text);
            _bill.CustomFees = TextUtils.ToDecimal(txtChiPhiHaiQuan.Text);
            _bill.Declaration = TextUtils.ToDecimal(txtSoToKhai.Text);
            _bill.BankCharges = TextUtils.ToDecimal(txtPhi1LanDien.Text);
            _bill.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);
            if (_bill.ID > 0)
            {
                FollowProjectBO.Instance.Update(_bill);
            }
            else
            {
                _bill.ID = (int)FollowProjectBO.Instance.Insert(_bill);
            }
            DataTable dt = (DataTable)grdData.DataSource;
            dt.AcceptChanges();
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                FollowProjectDetailModel detail = new FollowProjectDetailModel();

                if (id > 0)
                {
                    detail = (FollowProjectDetailModel)(FollowProjectDetailBO.Instance.FindByPK(id));
                }
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.QtyCustomer = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyCustomer));
                detail.FollowProjectID = TextUtils.ToInt(_bill.ID);
                detail.QtyCustomer = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyCustomer));
                detail.Partner = TextUtils.ToString(grvData.GetRowCellValue(i, colPartner));
                detail.PODate = txtDateTime.Value;
                detail.DeliveryRequestedDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colDeliveryRequestedDate));
                detail.OderDate = TextUtils.ToDate3(grvData.GetRowCellValue(i, colOderDate));
                detail.ShipmentDate = TextUtils.ToDate3(grvData.GetRowCellValue(i, colShipmentDate));
                detail.ArrivalDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colArrivalDate));
                detail.PONo = TextUtils.ToString(grvData.GetRowCellValue(i, colPONo));
                detail.LeadTime = TextUtils.ToInt(grvData.GetRowCellValue(i, colLeadTime));
                detail.PayDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colPayDate));
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductNewCode));
                detail.ProjectModel = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectModel));
                detail.StandardModel = TextUtils.ToString(grvData.GetRowCellValue(i, colStandardModel));
                detail.UnitPriceUSD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPriceUSD));
                detail.UnitPriceVND = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPriceVND));
                detail.TotalPriceUSD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceUSD));
                detail.TotalPriceVND = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceVND));
                detail.BankCharges = TextUtils.ToDecimal(txtPhi1LanDien.Text);
                detail.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);
                detail.TotalBankCharges = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalBankCharges)); // Phí ngân hàng 
                detail.ImportTax = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colImportTax));// thuế nhập (%)
                detail.ImportTaxVND = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colImportTaxVND));//thuế nhập 1/pcs (vnd)
                detail.TotalImportTax = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalImportTax));
                detail.CustomFees = TextUtils.ToDecimal(txtChiPhiHaiQuan.Text); //chi phí 1 tờ khai
                detail.Declaration = TextUtils.ToDecimal(txtSoToKhai.Text);   // số tờ khai
                detail.InsuranceFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colInsuranceFees)); //phí bảo hiểm
                detail.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);//số lần điện
                detail.CostWithoutVATDetail = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCostWithoutVATDetail));
                detail.CostIncludingVATDetail = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCostIncludingVATDetail));
                detail.TaxDetail = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxDetail));
                detail.Exchange = TextUtils.ToDecimal(txtVND.Text);
                detail.OldPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colOldPrice));
                detail.Debt = TextUtils.ToInt(grvData.GetRowCellValue(i, colDebt)); //công nợ nhà cung cấp
                detail.Bill = TextUtils.ToString(grvData.GetRowCellValue(i, colBill));
                detail.IsPay = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsPay));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colAdd));
                detail.SlowDelivery = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colSlowDelivery));
                TextUtils.ExcuteSQL($"Update POKHDetail set IsOder=1 where ID={ detail.POKHDetailID}");
                
                if (detail.IsAlreadyDelivered)
                {
                    if (detail.ArrivalDate != null)
                        detail.IsItemReceived = true;
                    else
                        detail.IsItemReceived = false;
                    if (detail.Bill != "")
                        detail.IsBillStatus = true;
                    else
                        detail.IsBillStatus = false;
                    if (detail.IsBillStatus && detail.IsItemReceived && detail.IsAddWarehouse && detail.IsPay)
                    {
                        detail.Status = 1;
                    }
                    else
                    {
                        detail.Status = 0;
                    }
                }
                else
                {
                    if (detail.ArrivalDate != null)
                        detail.Status = 2;
                    else detail.Status = 0;
                }

                if (detail.ID == 0)
                    detail.ID = (int)FollowProjectDetailBO.Instance.Insert(detail);
                else
                    FollowProjectDetailBO.Instance.Update(detail);

                foreach (int itemDelete in lstDelete)
                {
                    FollowProjectDetailBO.Instance.Delete(itemDelete);
                }

            }
            return true;
        }
        private void btnNewSP_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();

            if (frm.ShowDialog() == DialogResult.OK)
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
            if (e.KeyCode == Keys.Enter)
            {
                grvData.FocusedRowHandle++;
            }
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            string date = txtDateTime.Value.ToString("ddMMyyyy");
            string code = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 POCode FROM FollowProject Where POCode LIKE '%{cbCustomer.Text}%' ORDER BY ID DESC"));
            string[] arr = code.Split('_');
            if (arr.Count() < 2)
            {
                txtPOCode.Text =TextUtils.ToString(cbCustomer.Text+"_"+date+"_1");
                return;
            }
            string so = TextUtils.ToString("_" + (TextUtils.ToInt(arr[2]) + 1));
            txtPOCode.Text = cbCustomer.Text + "_" + date + "_" + so;
        }

        private void txtVND_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadtien();
        }

        private void txtPhiVanChuyen_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadtien();
        }

        private void txtPhi1LanDien_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadtien();
        }

        private void txtSoLanDien_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadtien();
        }

        private void txtChiPhiHaiQuan_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadtien();
        }

        private void txtSoToKhai_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadtien();
        }

        void loadtien()
        {
            if (cGlobVar.LockEvents) return;
            if (grvData.RowCount == 0) return;

            decimal tongbaogia = 0, TongGiaVND = 0, PhiVanChuyen = 0, Tongthuenhap = 0, Phinganhang = 0, Phihaiquan = 0, tongphikVAT = 0, PhiBaoHiem = 0, tongphidien = 0;
            solgPOKH = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            PhiVanChuyen = TextUtils.ToDecimal(txtPhiVanChuyen.Value);
            tongphidien = TextUtils.ToDecimal(txtPhi1LanDien.Value * txtSoLanDien.Value * txtVND.Value);
            for (int i = 0; i < grvData.RowCount; i++)
            {
                decimal costchild = 0;
                TongGiaVND = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceVND));
                Tongthuenhap = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalImportTax));
                PhiBaoHiem = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colInsuranceFees));
                Phihaiquan = TextUtils.ToDecimal(txtSoToKhai.Value * txtChiPhiHaiQuan.Value);
                Phinganhang = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalBankCharges));
                costchild = TongGiaVND + Tongthuenhap + PhiVanChuyen + Phihaiquan + Phinganhang + tongphidien + PhiBaoHiem;
                tongphikVAT = tongphikVAT + costchild;
                grvData.SetRowCellValue(i, colCostWithoutVATDetail, costchild);
                grvData.SetRowCellValue(i, colCostIncludingVATDetail, costchild + costchild * 10 / 100);
                grvData.SetRowCellValue(i, colTaxDetail, costchild * 10 / 100);
            }
            grvData.FocusedRowHandle = -1;
            txtTongchiphikoVAT.Text = String.Format("{0:n0}", colCostWithoutVATDetail.SummaryItem.SummaryValue);
            txtThueVAT.Text = String.Format("{0:n0}", (colTaxDetail.SummaryItem.SummaryValue));
            txtTongchiphicoVAT.Text = String.Format("{0:n0}", colCostIncludingVATDetail.SummaryItem.SummaryValue);
            txtTongChiPhiNganHang.Text = String.Format("{0:n0}", (Phinganhang + tongphidien));
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadProjectCode();
        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (cbProject.EditValue == null) return;
            DataRow[] row = dtProject.Select("ID=" + cbProject.EditValue);
            if (row.Length > 0)
            {
                cbUser.EditValue = row[0]["UserID"];
                cbCustomer.EditValue = row[0]["CustomerID"];
            }
            string date = txtDateTime.Value.ToString("ddMMyyyy");
            txtPOCode.Text = cbCustomer.Text + "_" + date + "_" + cbProject.EditValue;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }
        int POKHID;
        private void frmFollowProjectDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column == colUnitPriceUSD)
            {
                decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == 0 && value1 != 0);
                e.Handled = true;
                return;
            }
        }
        private void ckbMerge_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
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
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }

        private void repositoryItemComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            string a = repositoryItemComboBox1.Items.ToString();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column == colAdd && e.Y < 70)
                {
                    MyLib.AddNewRow(grdData, grvData);
                }
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            USD = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPriceUSD));
            solg = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            thue = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colImportTax));
            if (USD > 0 && solg > 0 && thue >= 0)
            {
                decimal VND = TextUtils.ToDecimal(txtVND.Text);
                if (e.Column == colUnitPriceUSD || e.Column == colQty || e.Column == colImportTax)
                {

                    decimal sum = USD * solg * VND;
                    grvData.SetFocusedRowCellValue(colTotalPriceUSD, USD * solg);
                    grvData.SetFocusedRowCellValue(colUnitPriceVND, VND * USD);
                    grvData.SetFocusedRowCellValue(colTotalPriceVND, sum);
                    grvData.SetFocusedRowCellValue(colImportTaxVND, VND * USD * thue);
                    grvData.SetFocusedRowCellValue(colTotalImportTax, sum * thue);
                    grvData.SetFocusedRowCellValue(colInsuranceFees, sum * 2 / 1000);
                    grvData.SetFocusedRowCellValue(colTotalBankCharges, sum * 5 / 1000);

                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPriceUSD));
                        if (qty == 0)
                            return;
                    }
                    loadtien();
                }
            }
            if (e.Column == colOderDate || e.Column == colLeadTime)
            {
                int date = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colLeadTime));
                DateTime dateTime = TextUtils.ToDate3(grvData.GetFocusedRowCellValue(colOderDate));
                DateTime dateTimeRequest = dateTime.AddDays(date);
                grvData.SetFocusedRowCellValue(colDeliveryRequestedDate, dateTimeRequest);
            }
            if (e.Column == colArrivalDate || e.Column == colDebt)
            {
                int congno = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDebt));
                DateTime dateTimeShip = TextUtils.ToDate3(grvData.GetFocusedRowCellValue(colArrivalDate));
                grvData.SetFocusedRowCellValue(colPayDate, dateTimeShip.AddDays(congno));
            }

        }

        private void grvData_LoadData(List<int> lstProductID, List<int> lstID, DataTable dt)
        {
            try
            {
                cGlobVar.LockEvents = true;
                cbProject.EditValue = TextUtils.ToInt(dt.Rows[0]["ProjectID"]);
                for (int i = 0; i < lstProductID.Count; i++)
                {
                    if (lstProductID[i] > 0)
                    {
                        MyLib.AddNewRow(grdData, grvData);
                        DataRow[] rows = dtproduct.Select($"ID = { lstProductID[i]}");
                        if (rows.Length > 0)
                        {
                            int Qty = TextUtils.ToInt(dt.Rows[i]["Qty"]);
                            int POKHDetailID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                            txtPOKHID.EditValue = TextUtils.ToInt(dt.Rows[i]["POKHID"]);
                            string productcode = TextUtils.ToString(rows[0]["ProductCode"]);
                            string maker = TextUtils.ToString(rows[0]["Maker"]);
                            if (grvData.RowCount > 0)
                            {
                                grvData.SetRowCellValue(grvData.RowCount - 1, colProductID, dt.Rows[i]["ProductName"]);
                                grvData.SetRowCellValue(grvData.RowCount - 1, colMaker, dt.Rows[i]["Maker"]);
                                grvData.SetRowCellValue(grvData.RowCount - 1, colStandardModel, dt.Rows[i]["ProductCode"]);
                                grvData.SetRowCellValue(grvData.RowCount - 1, colQtyCustomer, dt.Rows[i]["Qty"]);
                                grvData.SetRowCellValue(grvData.RowCount - 1, colPOKHDetailID, dt.Rows[i]["ID"]);
                                grvData.SetRowCellValue(grvData.RowCount - 1, colProjectModel, dt.Rows[i]["GuestCode"]);
                                grvData.SetRowCellValue(grvData.RowCount - 1, colProductNewCode, dt.Rows[i]["ProductID"]);
                            }
                            else
                            {
                                grvData.SetRowCellValue(0, colProductID, dt.Rows[i]["ProductName"]);
                                grvData.SetRowCellValue(0, colMaker, dt.Rows[i]["Maker"]);
                                grvData.SetRowCellValue(0, colStandardModel, dt.Rows[i]["ProductCode"]);
                                grvData.SetRowCellValue(0, colQtyCustomer, dt.Rows[i]["Qty"]);
                                grvData.SetRowCellValue(0, colPOKHDetailID, dt.Rows[i]["ID"]);
                                grvData.SetRowCellValue(0, colProjectModel, dt.Rows[i]["GuestCode"]);
                                grvData.SetRowCellValue(0, colProductNewCode, dt.Rows[i]["ProductID"]);
                            }
                        }
                    }

                }

            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            frmPOKHData frm = new frmPOKHData(0);
            frm.send += grvData_LoadData;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }

        }

        void loadProjectCode()
        {

            string date = txtDateTime.Value.ToString("ddMMyyyy");
            txtPOCode.Text = cbCustomer.Text + "_" + date + "_" + cbProject.EditValue;
        }
        ArrayList lstDelete = new ArrayList();



        //private void grvData_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {

        //        TreeListHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
        //        if (info.Column == coladd && e.Y < 50)
        //        {
        //            grvData.Nodes.Add();
        //        }
        //        if (e.Y > 20) return;
        //        if (info.Band != null && info.Band.Name == "grbID")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colOderDate.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbID.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 1)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;
        //                        }
        //                        this.grbID.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }
        //                }
        //            }
        //        }


        //        if (info.Band != null && info.Band.Name == "grbDate")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colDebt.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbDate.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 2)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;
        //                        }
        //                        this.grbDate.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }
        //                }
        //            }
        //        }


        //        if (info.Band != null && info.Band.Name == "grbMaker")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colMaker.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbMaker.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 3)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;

        //                        }
        //                        this.grbMaker.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }
        //                }
        //            }
        //        }
        //        if (info.Band != null && info.Band.Name == "grbProduct")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colProjectModel.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbProduct.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 4)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;
        //                        }
        //                        this.grbProduct.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }
        //                }
        //            }
        //        }
        //        if (info.Band != null && info.Band.Name == "grbQty")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colQty.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbQty.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 5)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;
        //                        }
        //                        this.grbQty.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }
        //                }
        //            }
        //        }
        //        if (info.Band != null && info.Band.Name == "grbPrice")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colUnitPriceUSD.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbPrice.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 6)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;
        //                        }
        //                        this.grbPrice.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }
        //                }
        //            }

        //        }
        //        if (info.Band != null && info.Band.Name == "grbCost")
        //        {
        //            if (info.Band.Columns.Count <= 0) return;
        //            if (colTotalBankCharges.Visible)
        //            {
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    item.Visible = false;
        //                }
        //                info.Band.Width = 30;
        //                this.grbCost.ImageOptions.Image = global::Forms.Properties.Resources.expand;
        //            }
        //            else
        //            {
        //                info.Band.Width = 0;
        //                foreach (var item in info.Band.Columns)
        //                {
        //                    if (item.Tag != null)
        //                    {
        //                        if (!int.TryParse(item.Tag.ToString(), out int tag))
        //                            tag = -1;
        //                        if (tag == 7)
        //                        {
        //                            item.Visible = true;
        //                            info.Band.Width += item.Width;
        //                        }
        //                        this.grbCost.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
        //                    }

        //                }
        //            }

        //        }
        //    }

        //}
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grvData.RowCount == 0) return;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            lstDelete.Add(id);
            grvData.DeleteSelectedRows();
        }


    }
}
