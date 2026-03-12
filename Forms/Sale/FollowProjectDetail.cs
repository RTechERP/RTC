using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
using static BMS.frmFollowProject;

namespace BMS
{
    public partial class FollowProjectDetail : _Forms
    {
        int ad, solgPOKH, solg, code;
        decimal USD, thue, sotokhai, haiquan, baogia;
        public int Id = 0;
        public FollowProjectModel _bill = new FollowProjectModel();
        public POKHModel oConvert = new POKHModel();
        DataTable dtProject = new DataTable();

        int warehouseID = 0;
        public FollowProjectDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        void loadGrdData()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spLoadFollowProject", "A", new string[] { "@ID" }, new object[] { Id });
            treeData.DataSource = dt;

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
        void loadupdate()
        {
            if (_bill.ID > 0)
            {

                DataTable dt = new DataTable();
                dt = TextUtils.LoadDataFromSP("spLoadFollowProject", "A", new string[] { "@ID" }, new object[] { Id });
                txtPOCode.Text = _bill.POCode;
                txtDateTime.Value = TextUtils.ToDate3(_bill.CreateDate);
                cbCustomer.EditValue = _bill.CustomerID;
                cbProject.EditValue = _bill.ProjectID;
                cbUser.EditValue = _bill.UserID;
                if (treeData.AllNodesCount > 0)
                {
                    txtVND.Text = TextUtils.ToString(dt.Rows[0]["Exchange"]);
                    txtPhiVanChuyen.Text = TextUtils.ToString(dt.Rows[0]["TransportFee"]);
                    txtChiPhiHaiQuan.Text = TextUtils.ToString(dt.Rows[0]["CustomFees"]);
                    txtSoToKhai.Text = TextUtils.ToString(dt.Rows[0]["Declaration"]);
                    txtPhi1LanDien.Text = TextUtils.ToString(dt.Rows[0]["BankCharges"]);
                    //txtBaogiakhachhang.Text = TextUtils.ToString(dt.Rows[0]["CustomerQuotationDetail"]);
                    txtSoLanDien.Text = TextUtils.ToString(dt.Rows[0]["NumberOfTransactions"]);
                    loadtien();
                }
            }
            loadData();
        }
        private void frmBillImport_Load(object sender, EventArgs e)
        {
            loadUsers();
            loadProject();
            loadupdate();
            loadCustomer();
            loadProduct();
            if (oConvert.ID > 0)
                ConvertPO();
            else
                loadGrdData();
            cbProductt.EditValueChanged += new EventHandler(cbProductt_EditValueChanged);
            btnDeletee.Click += new EventHandler(btnDelete_Click);
            btnModule.Click += new EventHandler(btnAddModule_Click);


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

        /// <summary>
        /// nhận data từ pokh
        /// </summary>
        /// 
        void ConvertPO()
        {
            txtDateTime.Value = TextUtils.ToDate3(oConvert.ReceivedDatePO);
            cbProject.EditValue = oConvert.ProjectID;
            cbCustomer.EditValue = oConvert.CustomerID;
            DataSet dtConvert = new DataSet();
            dtConvert = TextUtils.LoadDataSetFromSP("spGetConvertFollowProject",  new string[] { "@ID","@IDPO" }, new object[] { Id,oConvert.ID });
            treeData.DataSource = dtConvert.Tables[1];
            for (int i = 0; i < dtConvert.Tables[0].Rows.Count; i++)
            {
                dtConvert.Tables[1].Rows.Add();
                dtConvert.Tables[1].Rows[i]["ProductID"] = dtConvert.Tables[0].Rows[i]["ProductID"];
                dtConvert.Tables[1].Rows[i]["Maker"] = dtConvert.Tables[0].Rows[i]["Maker"];
                dtConvert.Tables[1].Rows[i]["Partner"] = dtConvert.Tables[0].Rows[i]["Maker"];
                dtConvert.Tables[1].Rows[i]["ProductName"] = dtConvert.Tables[0].Rows[i]["ProductName"];
                dtConvert.Tables[1].Rows[i]["StandardModel"] = dtConvert.Tables[0].Rows[i]["ProductName"];
                dtConvert.Tables[1].Rows[i]["Qty"] = dtConvert.Tables[0].Rows[i]["Qty"];
            }
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
            dtproduct = TextUtils.Select(" SELECT ID,ProductName,ProductCode,Maker FROM ProductSale");
            cbProductt.DisplayMember = "ProductName";
            cbProductt.ValueMember = "ID";
            cbProductt.DataSource = dtproduct;
        }
        private void cbProductt_EditValueChanged(object sender, EventArgs e)
        {
            treeData.Focus();
            int ID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colProductID));
            DataRow[] dtrow = dtproduct.Select("ID=" + ID);
            if (dtrow.Length == 0) return;
            treeData.FocusedNode.SetValue(colMaker, dtrow[0]["Maker"]);
            treeData.FocusedNode.SetValue(colPartner, dtrow[0]["Maker"]);
            treeData.FocusedNode.SetValue(colStandardModel, dtrow[0]["ProductName"]);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (oConvert.ID > 0)
                TextUtils.ExcuteSQL($"Update POKH set IsOder=1 where ID={oConvert.ID}");

            if (saveData())
            {
                this.Close();
            }
        }
        private bool ValidateForm()
        {
            //if (_bill.ID == 0)
            //{
            //    if (cbProject.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Xin hãy điền Mã dự án.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        return false;
            //    }
            //    else
            //    {
            //        DataTable dt;
            //        if (_bill.ID > 0)
            //        {
            //            int strID = _bill.ID;
            //            dt = TextUtils.Select("select top 1 ID from FollowProject where ProjectCode = '" + txtProjectCode.Text.Trim() + "'");
            //        }
            //        else
            //        {
            //            dt = TextUtils.Select("select top 1 ID from FollowProject where ProjectCode = '" + txtProjectCode.Text.Trim() + "'");
            //        }
            //        if (dt != null)
            //        {
            //            if (dt.Rows.Count > 0)
            //            {
            //                MessageBox.Show("Mã dự án đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                return false;
            //            }
            //        }
            //    }
            //}
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
          //  _bill.TotalCustomerQuotation = TextUtils.ToDecimal(txtTongbaogiakhachhang.Text);
            _bill.TotalCostIncludingVAT = TextUtils.ToDecimal(txtTongchiphicoVAT.Text);
            _bill.TotalCostWithoutVAT = TextUtils.ToDecimal(txtTongchiphikoVAT.Text);
            _bill.TotalBankCharges = TextUtils.ToDecimal(txtTongChiPhiNganHang.Text);
            _bill.Tax = TextUtils.ToDecimal(txtThueVAT.Text);
            _bill.CreateDate = TextUtils.ToDate2(txtDateTime.Value);
            _bill.POKHID = TextUtils.ToInt(txtPOKHID.EditValue);
            if (_bill.ID > 0)
            {
                FollowProjectBO.Instance.Update(_bill);
            }
            else
            {
                _bill.ID = (int)FollowProjectBO.Instance.Insert(_bill);
            }

            foreach (TreeListNode item in treeData.Nodes)
            {
                int id = TextUtils.ToInt(item.GetValue(colID));
                FollowProjectDetailModel detail = new FollowProjectDetailModel();

                if (id > 0)
                {
                    detail = (FollowProjectDetailModel)(FollowProjectDetailBO.Instance.FindByPK(id));
                }
                detail.Qty = TextUtils.ToInt(item.GetValue(colQty));
                detail.QtyCustomer = TextUtils.ToInt(item.GetValue(colQtyCustomer));
                detail.FollowProjectID = TextUtils.ToInt(_bill.ID);
                detail.QtyCustomer = TextUtils.ToInt(item.GetValue(colQtyCustomer));
                detail.Partner = TextUtils.ToString(item.GetValue(colPartner));
                detail.PODate = TextUtils.ToDate2(txtDateTime.Value);
                detail.DeliveryRequestedDate = TextUtils.ToDate2(item.GetValue(colDeliveryRequestedDate));
                detail.OderDate = TextUtils.ToDate2(item.GetValue(colOderDate));
                detail.ShipmentDate = TextUtils.ToDate2(item.GetValue(colShipmentDate));
                detail.ArrivalDate = TextUtils.ToDate2(item.GetValue(colArrivalDate));
                detail.PONo = TextUtils.ToString(item.GetValue(colPONo));
                detail.LeadTime = TextUtils.ToInt(item.GetValue(colLeadTime));
                detail.PayDate = TextUtils.ToDate2(item.GetValue(colPayDate));
                detail.ProductID = TextUtils.ToInt(item.GetValue(colProductID));
                detail.ProjectModel = TextUtils.ToString(item.GetValue(colProjectModel));
                detail.StandardModel = TextUtils.ToString(item.GetValue(colStandardModel));
                detail.UnitPriceUSD = TextUtils.ToDecimal(item.GetValue(colUnitPriceUSD));
                detail.UnitPriceVND = TextUtils.ToDecimal(item.GetValue(colUnitPriceVND));
                detail.TotalPriceUSD = TextUtils.ToDecimal(item.GetValue(colTotalPriceUSD));
                detail.TotalPriceVND = TextUtils.ToDecimal(item.GetValue(colTotalPriceVND));
                detail.BankCharges = TextUtils.ToDecimal(txtPhi1LanDien.Text);
                detail.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);
                detail.TotalBankCharges = TextUtils.ToDecimal(item.GetValue(colTotalBankCharges)); // Phí ngân hàng 
                detail.ImportTax = TextUtils.ToDecimal(item.GetValue(colImportTax));// thuế nhập (%)
                detail.ImportTaxVND = TextUtils.ToDecimal(item.GetValue(colImportTaxVND));//thuế nhập 1/pcs (vnd)
                detail.TotalImportTax = TextUtils.ToDecimal(item.GetValue(colTotalImportTax));
                detail.CustomFees = TextUtils.ToDecimal(txtChiPhiHaiQuan.Text); //chi phí 1 tờ khai
                detail.Declaration = TextUtils.ToDecimal(txtSoToKhai.Text);   // số tờ khai
                detail.InsuranceFees = TextUtils.ToDecimal(item.GetValue(colInsuranceFees)); //phí bảo hiểm
                detail.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);//số lần điện
                detail.CostWithoutVATDetail = TextUtils.ToDecimal(item.GetValue(colCostWithoutVATDetail));
                detail.CostIncludingVATDetail = TextUtils.ToDecimal(item.GetValue(colCostIncludingVATDetail));
                detail.TaxDetail = TextUtils.ToDecimal(item.GetValue(colTaxDetail));
                detail.Exchange = TextUtils.ToDecimal(txtVND.Text);
                //detail.CustomerQuotationDetail = TextUtils.ToDecimal(item.GetValue(colCustomerQuotationDetail));
                //detail.TotalCustomerQuotationDetail = TextUtils.ToDecimal(item.GetValue(colTotalCustomerQuotationDetail));
                detail.Debt = TextUtils.ToInt(item.GetValue(colDebt)); //công nợ nhà cung cấp
                detail.Bill = TextUtils.ToString(item.GetValue(colBill));
                detail.IsPay = TextUtils.ToBoolean(item.GetValue(colIsPay));
                detail.IsAddWarehouse = TextUtils.ToBoolean(item.GetValue(colIsAddWarehouse));
                detail.IsAlreadyDelivered = TextUtils.ToBoolean(item.GetValue(colIsAlreadyDelivered));
                detail.Note = TextUtils.ToString(item.GetValue(colNote));
                detail.POKHDetailID = TextUtils.ToInt(item.GetValue(colPOKHDetailID));
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
                {

                    detail.ID = (int)FollowProjectDetailBO.Instance.Insert(detail);
                }
                else
                {
                    FollowProjectDetailBO.Instance.Update(detail);
                }
                foreach (TreeListNode itemChild in item.Nodes)
                {
                    int idchild = TextUtils.ToInt(itemChild.GetValue(colID));
                    int parentID = TextUtils.ToInt(detail.ID);
                    FollowProjectDetailModel detailchild = new FollowProjectDetailModel();
                    if (idchild > 0)
                    {
                        detailchild = (FollowProjectDetailModel)(FollowProjectDetailBO.Instance.FindByPK(idchild));
                    }
                    detailchild.ParentID = TextUtils.ToInt(parentID);
                    detailchild.FollowProjectID = TextUtils.ToInt(_bill.ID);
                    detailchild.Qty = TextUtils.ToInt(itemChild.GetValue(colQty));
                    detailchild.QtyCustomer = TextUtils.ToInt(itemChild.GetValue(colQtyCustomer));
                    detailchild.Partner = TextUtils.ToString(itemChild.GetValue(colPartner));
                    detailchild.PODate = TextUtils.ToDate2(txtDateTime.Value);
                    detailchild.DeliveryRequestedDate = TextUtils.ToDate2(itemChild.GetValue(colDeliveryRequestedDate));
                    detailchild.OderDate = TextUtils.ToDate2(itemChild.GetValue(colOderDate));
                    detailchild.ShipmentDate = TextUtils.ToDate2(itemChild.GetValue(colShipmentDate));
                    detailchild.ArrivalDate = TextUtils.ToDate2(itemChild.GetValue(colArrivalDate));
                    detailchild.PONo = TextUtils.ToString(itemChild.GetValue(colPONo));
                    detailchild.LeadTime = TextUtils.ToInt(itemChild.GetValue(colLeadTime));
                    detailchild.PayDate = TextUtils.ToDate2(itemChild.GetValue(colPayDate));
                    detailchild.ProductID = TextUtils.ToInt(itemChild.GetValue(colProductID));
                    detailchild.ProjectModel = TextUtils.ToString(itemChild.GetValue(colProjectModel));
                    detailchild.StandardModel = TextUtils.ToString(itemChild.GetValue(colStandardModel));
                    detailchild.UnitPriceUSD = TextUtils.ToDecimal(itemChild.GetValue(colUnitPriceUSD));
                    detailchild.UnitPriceVND = TextUtils.ToDecimal(itemChild.GetValue(colUnitPriceVND));
                    detailchild.TotalPriceUSD = TextUtils.ToDecimal(itemChild.GetValue(colTotalPriceUSD));
                    detailchild.TotalPriceVND = TextUtils.ToDecimal(itemChild.GetValue(colTotalPriceVND));
                    detailchild.BankCharges = TextUtils.ToDecimal(txtPhi1LanDien.Text);
                    detailchild.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);
                    detailchild.TotalBankCharges = TextUtils.ToDecimal(itemChild.GetValue(colTotalBankCharges)); // Phí ngân hàng 
                    detailchild.ImportTax = TextUtils.ToDecimal(itemChild.GetValue(colImportTax));// thuế nhập (%)
                    detailchild.ImportTaxVND = TextUtils.ToDecimal(itemChild.GetValue(colImportTaxVND));//thuế nhập 1/pcs (vnd)
                    detailchild.TotalImportTax = TextUtils.ToDecimal(itemChild.GetValue(colTotalImportTax));
                    detailchild.CustomFees = TextUtils.ToDecimal(txtChiPhiHaiQuan.Text); //chi phí 1 tờ khai
                    detailchild.Declaration = TextUtils.ToDecimal(txtSoToKhai.Text);   // số tờ khai
                    detailchild.InsuranceFees = TextUtils.ToDecimal(itemChild.GetValue(colInsuranceFees)); //phí bảo hiểm
                    detailchild.NumberOfTransactions = TextUtils.ToDecimal(txtSoLanDien.Text);//số lần điện
                    detailchild.CostWithoutVATDetail = TextUtils.ToDecimal(itemChild.GetValue(colCostWithoutVATDetail));
                    detailchild.CostIncludingVATDetail = TextUtils.ToDecimal(itemChild.GetValue(colCostIncludingVATDetail));
                    detailchild.TaxDetail = TextUtils.ToDecimal(itemChild.GetValue(colTaxDetail));
                    detailchild.Exchange = TextUtils.ToDecimal(txtVND.Text);
                    //detailchild.CustomerQuotationDetail = TextUtils.ToDecimal(itemChild.GetValue(colCustomerQuotationDetail));
                    //.TotalCustomerQuotationDetail = TextUtils.ToDecimal(itemChild.GetValue(colTotalCustomerQuotationDetail));
                    detailchild.Debt = TextUtils.ToInt(itemChild.GetValue(colDebt)); //công nợ nhà cung cấp
                    detailchild.Bill = TextUtils.ToString(itemChild.GetValue(colBill));
                    detailchild.IsPay = TextUtils.ToBoolean(itemChild.GetValue(colIsPay));
                    detailchild.IsAddWarehouse = TextUtils.ToBoolean(itemChild.GetValue(colIsAddWarehouse));
                    detailchild.IsAlreadyDelivered = TextUtils.ToBoolean(itemChild.GetValue(colIsAlreadyDelivered));
                    detailchild.Note = TextUtils.ToString(itemChild.GetValue(colNote));
                    detailchild.Status = detail.Status;
                    detailchild.POKHDetailID = TextUtils.ToInt(itemChild.GetValue(colPOKHDetailID));
                    if (detailchild.ID == 0)
                    {
                        FollowProjectDetailBO.Instance.Insert(detailchild);
                    }
                    else
                    {
                        FollowProjectDetailBO.Instance.Update(detailchild);
                    }
                }
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

        private void treeData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                treeData.CopyToClipboard();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                //treeData.PasteFromClipboard();

                string copyContent = Clipboard.GetText();

                if (copyContent != null && copyContent.Trim() != string.Empty && treeData.FocusedNode != null)
                {
                    string[] arrRows = copyContent.Split('\n');
                    if (arrRows.Count() > 0)
                    {
                        string rowContent = arrRows[0];
                        string[] arrCols = rowContent.Replace('\r', '\0').Split('\t');
                        int beginCol = treeData.FocusedColumn.VisibleIndex;
                        int beginRow = treeData.GetVisibleIndexByNode(treeData.FocusedNode);
                        for (int i = 0; i < arrRows.Count() - 1; i++)
                        {

                            rowContent = arrRows[i];
                            arrCols = rowContent.Replace('\r'.ToString(), "").Split('\t');
                            for (int j = 0; j < arrCols.Count(); j++)
                            {
                                if (beginCol + j > treeData.Columns.Count - 1)
                                    break;
                                treeData.Nodes[beginRow].SetValue(treeData.GetColumnByVisibleIndex(beginCol + j), arrCols[j]);
                            }

                            beginRow += 1;
                            if (beginRow > treeData.Nodes.Count - 1)
                                break;
                        }
                    }
                }
            }
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            string date = txtDateTime.Value.ToString("ddMMyyyy");
            txtPOCode.Text = cbCustomer.Text + "_" + date + "_" + cbProject.EditValue;
        }

        private void txtVND_ValueChanged(object sender, EventArgs e)
        {
            loadtien();
        }

        private void txtPhiVanChuyen_ValueChanged(object sender, EventArgs e)
        {
            loadtien();
        }

        private void txtPhi1LanDien_ValueChanged(object sender, EventArgs e)
        {
            loadtien();
        }

        private void txtSoLanDien_ValueChanged(object sender, EventArgs e)
        {
            loadtien();
        }

        private void txtChiPhiHaiQuan_ValueChanged(object sender, EventArgs e)
        {
            loadtien();
        }

        private void txtSoToKhai_ValueChanged(object sender, EventArgs e)
        {
            loadtien();
        }

        void loadtien()
        {
            if (treeData.AllNodesCount == 0) return;
            decimal tongbaogia = 0, TongGiaVND = 0, PhiVanChuyen = 0, Tongthuenhap = 0, Phinganhang = 0, Phihaiquan = 0, tongphikVAT = 0, PhiBaoHiem = 0, tongphidien = 0;
            solgPOKH = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colQtyCustomer));
            PhiVanChuyen = TextUtils.ToDecimal(txtPhiVanChuyen.Value);
            tongphidien = TextUtils.ToDecimal(txtPhi1LanDien.Value) * TextUtils.ToDecimal(txtSoLanDien.Value) * TextUtils.ToDecimal(txtVND.Value);
            int count = treeData.AllNodesCount;
            foreach (TreeListNode item in treeData.Nodes)
            {

                foreach (TreeListNode itemchild in item.Nodes)
                {
                    long idc = TextUtils.ToInt(itemchild.GetValue(colID));
                    FollowProjectDetailModel detailchild = new FollowProjectDetailModel();
                    if (idc > 0)
                    {
                        detailchild = (FollowProjectDetailModel)(FollowProjectDetailBO.Instance.FindByPK(idc));
                    }
                    if (item.GetValue(colQty) != null && itemchild.GetValue(colUnitPriceUSD) != null)
                    {


                        if (detailchild != null)
                        {
                            decimal costchild = 0;
                           // detailchild.TotalCustomerQuotationDetail = TextUtils.ToDecimal(itemchild.GetValue(colTotalCustomerQuotationDetail));
                            detailchild.UnitPriceUSD = TextUtils.ToDecimal(itemchild.GetValue(colUnitPriceUSD));
                            detailchild.UnitPriceVND = TextUtils.ToDecimal(itemchild.GetValue(colUnitPriceVND));
                            detailchild.TotalPriceUSD = TextUtils.ToDecimal(itemchild.GetValue(colTotalPriceUSD));
                            detailchild.TotalPriceVND = TextUtils.ToDecimal(itemchild.GetValue(colTotalPriceVND));
                            TongGiaVND = detailchild.TotalPriceVND;
                            detailchild.ImportTax = TextUtils.ToDecimal(itemchild.GetValue(colImportTax));
                            detailchild.ImportTaxVND = TextUtils.ToDecimal(itemchild.GetValue(colImportTaxVND)) * solg;
                            Tongthuenhap = detailchild.ImportTaxVND;
                            detailchild.CustomFees = TextUtils.ToDecimal(txtChiPhiHaiQuan.Value);
                            detailchild.Declaration = TextUtils.ToDecimal(txtSoToKhai.Value);
                            PhiBaoHiem = TextUtils.ToDecimal(itemchild.GetValue(colInsuranceFees));
                            Phihaiquan = TextUtils.ToDecimal(detailchild.Declaration * detailchild.CustomFees);
                            Phinganhang = TextUtils.ToDecimal(itemchild.GetValue(colTotalBankCharges));
                            costchild = TongGiaVND + Tongthuenhap + PhiVanChuyen + Phihaiquan + Phinganhang + tongphidien + PhiBaoHiem;
                            tongphikVAT = tongphikVAT + costchild;
                            itemchild.SetValue(colCostWithoutVATDetail, costchild);
                            itemchild.SetValue(colCostIncludingVATDetail, costchild + costchild * 10 / 100);
                            itemchild.SetValue(colTaxDetail, costchild * 10 / 100);


                        }
                    }
                }
                if (item.GetValue(colQty) != null && item.GetValue(colUnitPriceUSD) != null)
                {
                    long id = TextUtils.ToInt(item.GetValue(colID));
                    FollowProjectDetailModel detail = new FollowProjectDetailModel();
                    if (id > 0)
                    {
                        detail = (FollowProjectDetailModel)(FollowProjectDetailBO.Instance.FindByPK(id));
                    }
                    if (detail != null)
                    {
                        decimal cost = 0;
                        detail.UnitPriceUSD = TextUtils.ToDecimal(item.GetValue(colUnitPriceUSD));
                        detail.UnitPriceVND = TextUtils.ToDecimal(item.GetValue(colUnitPriceVND));
                        detail.TotalPriceUSD = TextUtils.ToDecimal(item.GetValue(colTotalPriceUSD));
                        detail.TotalPriceVND = TextUtils.ToDecimal(item.GetValue(colTotalPriceVND));
                        TongGiaVND = detail.TotalPriceVND;
                        detail.ImportTax = TextUtils.ToDecimal(item.GetValue(colImportTax));
                        detail.ImportTaxVND = TextUtils.ToDecimal(item.GetValue(colImportTaxVND)) * solg;
                        Tongthuenhap = detail.ImportTaxVND;
                        detail.CustomFees = TextUtils.ToDecimal(txtChiPhiHaiQuan.Value);
                        detail.Declaration = TextUtils.ToDecimal(txtSoToKhai.Value);
                        PhiBaoHiem = TextUtils.ToDecimal(item.GetValue(colInsuranceFees));
                        Phihaiquan = detail.Declaration * detail.CustomFees;
                        Phinganhang = TextUtils.ToDecimal(item.GetValue(colTotalBankCharges));
                        cost = TongGiaVND + Tongthuenhap + PhiVanChuyen + Phihaiquan + Phinganhang + tongphidien + PhiBaoHiem;
                        tongphikVAT = tongphikVAT + cost;
                        item.SetValue(colCostWithoutVATDetail, cost);
                        item.SetValue(colCostIncludingVATDetail, cost + cost * 10 / 100);
                        item.SetValue(colTaxDetail, cost * 10 / 100);


                    }
                }

            }
            txtTongchiphikoVAT.Text = String.Format("{0:n0}", (tongphikVAT));
            txtThueVAT.Text = String.Format("{0:n0}", (tongphikVAT * 10 / 100));
            txtTongchiphicoVAT.Text = String.Format("{0:n0}", (tongphikVAT + tongphikVAT * 10 / 100));
            txtTongChiPhiNganHang.Text = String.Format("{0:n0}", (Phinganhang + tongphidien));
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            loadProjectCode();
        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {

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
        List<TreeListNode> node = new List<TreeListNode>();
        private void grvData_LoadData(List<int> lstProductID, List<int> lstID,DataTable dt)
        {
            cbProject.EditValue = TextUtils.ToInt(dt.Rows[0]["ProjectID"]);
            for (int i = 0; i < lstProductID.Count; i++)
            {
                if (lstProductID[i] > 0)
                {
                    node.Add(treeData.Nodes.Add());
                    DataRow[] rows = dtproduct.Select($"ID = { lstProductID[i]}");
                    if (rows.Length > 0)
                    {
                        int Qty = TextUtils.ToInt(dt.Rows[i]["Qty"]);
                        int POKHDetailID = TextUtils.ToInt(dt.Rows[i]["ID"]);
                        txtPOKHID.EditValue = TextUtils.ToInt(dt.Rows[i]["POKHID"]);
                        string productcode = TextUtils.ToString(rows[0]["ProductCode"]);
                        string maker = TextUtils.ToString(rows[0]["Maker"]);
                        if (treeData.Nodes.Count > 0)
                        {
                            treeData.SetRowCellValue(node[node.Count-1], colProductID, lstProductID[i]);
                            treeData.SetRowCellValue(node[node.Count-1], colMaker, maker);
                            treeData.SetRowCellValue(node[node.Count-1], colStandardModel, productcode);
                            treeData.SetRowCellValue(node[node.Count-1], colQtyCustomer, Qty);
                            treeData.SetRowCellValue(node[node.Count-1], colPOKHDetailID, POKHDetailID);
                        }
                        else
                        {
                            treeData.SetRowCellValue(node[node.Count], colProductID, lstProductID[i]);
                            treeData.SetRowCellValue(node[node.Count], colMaker, maker);
                            treeData.SetRowCellValue(node[node.Count], colStandardModel, productcode);
                            treeData.SetRowCellValue(node[node.Count], colQtyCustomer, Qty);
                            treeData.SetRowCellValue(node[node.Count], colPOKHDetailID, POKHDetailID);
                        }
                    }
                }
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

        private void btnAddModule_Click(object sender, EventArgs e)
        {
            if (treeData.AllNodesCount == 0) return;
            treeData.FocusedNode.Nodes.Add();
            treeData.ExpandAll();
        }

        private void treeData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                TreeListHitInfo info = treeData.CalcHitInfo(new Point(e.X, e.Y));
                if(info.Column== coladd && e.Y<50)
                {
                    treeData.Nodes.Add();
                }    
                if (e.Y > 20) return;
                if (info.Band != null && info.Band.Name == "grbID")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colPODate.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbID.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 1)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbID.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }
                }


                if (info.Band != null && info.Band.Name == "grbDate")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colDebt.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbDate.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 2)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbDate.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }
                }


                if (info.Band != null && info.Band.Name == "grbMaker")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colMaker.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbMaker.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 3)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;

                                }
                                this.grbMaker.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }
                }
                if (info.Band != null && info.Band.Name == "grbProduct")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colProjectModel.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbProduct.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 4)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbProduct.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }

                }
                if (info.Band != null && info.Band.Name == "grbQty")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colQty.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbQty.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 5)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbQty.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }

                }
                if (info.Band != null && info.Band.Name == "grbPrice")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colUnitPriceUSD.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbPrice.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 6)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbPrice.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }

                }
                if (info.Band != null && info.Band.Name == "grbCost")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colTotalBankCharges.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbCost.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 7)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbCost.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }

                        }
                    }

                }
            }
      
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeData.AllNodesCount == 0) return;
            int id = TextUtils.ToInt(treeData.FocusedNode.GetValue(colID));
            lstDelete.Add(id);
            treeData.DeleteSelectedNodes();
        }

        private void treeData_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            decimal tongbaogia = 0;
            solgPOKH = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colQtyCustomer));
            USD = TextUtils.ToDecimal(treeData.GetFocusedRowCellValue(colUnitPriceUSD));
            solg = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colQty));
            thue = TextUtils.ToDecimal(treeData.GetFocusedRowCellValue(colImportTax));
            solgPOKH = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colQtyCustomer));
            tongbaogia = baogia * solgPOKH * 110 / 100;
            if (USD > 0 && solg > 0 && thue >= 0)
            {
                decimal VND = TextUtils.ToDecimal(txtVND.Text);
                if (e.Column == colUnitPriceUSD || e.Column == colQty || e.Column == colImportTax)
                {
                    decimal sum = USD * solg * VND;
                    treeData.SetFocusedRowCellValue(colTotalPriceUSD, USD * solg);
                    treeData.SetFocusedRowCellValue(colUnitPriceVND, VND * USD);
                    treeData.SetFocusedRowCellValue(colTotalPriceVND, USD * solg * VND);
                    treeData.SetFocusedRowCellValue(colImportTaxVND, sum * thue);
                    treeData.SetFocusedRowCellValue(colTotalImportTax, sum * thue * solg);
                    treeData.SetFocusedRowCellValue(colInsuranceFees, sum * 2 / 1000);
                    treeData.SetFocusedRowCellValue(colTotalBankCharges, sum * 5 / 1000);
                    loadtien();
                }
                if (e.Column == colQtyCustomer)
                    loadtien();

            }
            if (e.Column == colOderDate || e.Column == colLeadTime)
            {
                int date = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colLeadTime));
                DateTime dateTime = TextUtils.ToDate3(treeData.GetFocusedRowCellValue(colOderDate));
                DateTime dateTimeRequest = dateTime.AddDays(date);
                treeData.SetFocusedRowCellValue(colDeliveryRequestedDate, dateTimeRequest);
            }
            if (e.Column == colArrivalDate || e.Column == colDebt)
            {
                int congno = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colDebt));
                DateTime dateTimeShip = TextUtils.ToDate3(treeData.GetFocusedRowCellValue(colArrivalDate));
                treeData.SetFocusedRowCellValue(colPayDate, dateTimeShip.AddDays(congno));
            }
        
        }
    }
}
