using BMS.Business;
using BMS.Model;
using BMS.Utils;
//using DevExpress.XtraGrid.Views.Grid;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBillExportAcountantDetail : _Forms
    {
        #region Variables
        public int IDDetail;
        public BillExportAcountantModel billExportAcountant = new BillExportAcountantModel();
        DataTable dtCustomer = new DataTable();
        ArrayList lstIDDelete = new ArrayList();
        ArrayList lstDeleteBillExportSale = new ArrayList();
        ArrayList lstSelect = new ArrayList();
        List<int> rowSelectedRow = new List<int>();

        #endregion

        public frmBillExportAcountantDetail()
        {
            InitializeComponent();
        }

        private void frmBillExportDetail_Load(object sender, EventArgs e)
        {
            cboUser.EditValue = Global.UserID;
            txtPageNumber.Text = "1";
            loadCustomer();
            loadUsers();
            loadBillExportAcountantDetail();
            if (txtCode.Text == "")
            {
                loadBilllNumber();
            }
            loadBillExportSale();
            // KHI ĐƯỢC duyệt thì sẽ ẩn các button 
            btnSaveNew.Enabled = !billExportAcountant.IsApproved;
        }

        #region Methods
        DataTable dtBillExportSale;
        private void loadBillExportSale()
        {
            dtBillExportSale = TextUtils.LoadDataFromSP("spGetBillExportDetailSale", "A"
                , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@PO" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), txtPO.Text.Trim() });
            grdExportSale.DataSource = dtBillExportSale;
            if (dtBillExportSale.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dtBillExportSale.Rows[0]["TotalPage"]);
        }

        /// <summary>
        /// load bill Export Detail
        /// </summary>
        private void loadBillExportAcountantDetail()
        {
            if (billExportAcountant.ID > 0)
            {
                txtCode.Text = billExportAcountant.Code;
                txtInvoiceNumber.Text = billExportAcountant.InvoiceNumber;
                txtAddress.Text = billExportAcountant.Address;
                cboCustomer.EditValue = billExportAcountant.CustomerID;
                cboUser.EditValue = billExportAcountant.UserID;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportAcountantDetail", "A", new string[] { "@BillExportID" }, new object[] { billExportAcountant.ID });
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtDateTime.Text = TextUtils.ToString(billExportAcountant.CreatedDate);
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
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// hàm dùng load số phiếu
        /// </summary>
        void loadBilllNumber()
        {
            int number = 0;
            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;
            string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExportAcountant Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} ORDER BY ID DESC"));
            if (billExportAcountant.ID == 0)
            {
                if (Billcode == "") // ktra tháng bdau và tháng đc update
                {
                    txtCode.Text = "PXKT" + date + "001";
                    return;
                }
                else number = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 3)); // tách lấy 3 số cuối convert sang int
                string dem = TextUtils.ToString(number + 1);
                for (int i = 0; dem.Length < 3; i++)
                {
                    dem = "0" + dem;
                }
                txtCode.Text = "PXKT" + date + TextUtils.ToString(dem);
            }
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            loadBilllNumber();
            if (saveData())
            {
                lstIDDelete.Clear();
                lstSelect.Clear();
                this.DialogResult = DialogResult.OK;
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
            if (dt.Rows.Count == 0) STT = 1;
            else STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
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
            if (grvData.RowCount == 0) return;
            int billExportID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillExportSaleID));
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string strName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colProductNewCode));
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstSelect.Remove(billExportID);
                lstIDDelete.Add(id);
                if (id > 0) lstDeleteBillExportSale.Add(billExportID);
                grvData.DeleteSelectedRows();
                rowSelectedRow.Remove(billExportID); // xóa lựa chọn sp

                //var focusedRowHandle = grvExportSale.FocusedRowHandle;
                //grvExportSale.SetRowCellValue(focusedRowHandle, colIsInvoice, 0);
            }
        }

        /// <summary>
        /// click button lưu thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                txtInvoiceNumber.Clear();
                cboUser.Text = "";
                cboCustomer.Text = "";
                txtAddress.Clear();
                lstSelect.Clear();
                lstIDDelete.Clear();
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                billExportAcountant = new BillExportAcountantModel();
                loadBilllNumber();
            }
        }
        #endregion

        /// <summary>
        /// hàm save Data
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (!ValidateForm()) return false;
            // focus: trỏ đến -> lưu và cất đi luôn
            grvData.Focus();
            txtCode.Focus();
            billExportAcountant.Code = txtCode.Text.Trim();
            billExportAcountant.InvoiceNumber = txtInvoiceNumber.Text.Trim();
            billExportAcountant.Address = txtAddress.Text.Trim();
            billExportAcountant.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            billExportAcountant.UserID = TextUtils.ToInt(cboUser.EditValue);
            billExportAcountant.CreatedDate = txtDateTime.Value;
            if (TextUtils.ToString(billExportAcountant.CreatedDate) == "")
            {
                billExportAcountant.CreatedDate = txtDateTime.Value;
                FileStream fs = new FileStream("BillLog.text", FileMode.OpenOrCreate);
                fs.Write(Encoding.UTF8.GetBytes(txtDateTime.Value.ToString()), 0, Encoding.UTF8.GetByteCount(txtDateTime.Value.ToString()));
                fs.Write(Encoding.UTF8.GetBytes(txtCode.Text), 0, Encoding.UTF8.GetByteCount(txtCode.Text));
            }
            if (billExportAcountant.ID > 0)
            {
                BillExportAcountantBO.Instance.Update(billExportAcountant);
            }
            else
            {
                billExportAcountant.ID = (int)BillExportBO.Instance.Insert(billExportAcountant);
            }
            for (int i = 0; i < grvData.RowCount; i++)
            {
                BillExportAcountantDetailModel billExportAcountantDetail = new BillExportAcountantDetailModel();
                string productName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductNameSale));
                if (productName.Trim() == "") continue;
                billExportAcountantDetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                billExportAcountantDetail.BillExportID = billExportAcountant.ID;//Liên kết bảng Nhập Xuất
                billExportAcountantDetail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));//ID Sản phẩm
                billExportAcountantDetail.BillExportSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colBillExportSaleID));
                billExportAcountantDetail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtySale));
                billExportAcountantDetail.ProductFullName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductFullName));
                billExportAcountantDetail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectNameText));
                billExportAcountantDetail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                billExportAcountantDetail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                billExportAcountantDetail.TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty));
                billExportAcountantDetail.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                billExportAcountantDetail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                billExportAcountantDetail.IntoMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                billExportAcountantDetail.IntoMoneyWithoutVat = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoneyWithoutVat));
                billExportAcountantDetail.TotalIntoMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalIntoMoney));
                billExportAcountantDetail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectNameText));
                billExportAcountantDetail.GroupExport = TextUtils.ToString(grvData.GetRowCellValue(i, colGroupExport));
                if (billExportAcountantDetail.ID > 0)
                {
                    BillExportAcountantDetailBO.Instance.Update(billExportAcountantDetail);
                    if (lstIDDelete.Count > 0)
                        BillExportAcountantDetailBO.Instance.Delete(lstIDDelete);
                }
                else
                {
                    BillExportAcountantDetailBO.Instance.Insert(billExportAcountantDetail);
                }
                if (lstDeleteBillExportSale.Count > 0)
                {
                    for (int j = 0; j < lstDeleteBillExportSale.Count; j++)
                    {
                        TextUtils.ExcuteSQL($"UPDATE [RTC].[dbo].[BillExportDetail] SET IsInvoice = 0, InvoiceNumber = 0 WHERE ID = {lstDeleteBillExportSale[j]}");
                    }
                }
                // update trạng thái hóa đơn, số hóa đơn
                int billExportSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colBillExportSaleID));
                TextUtils.LoadDataFromSP("spUpdateStatusBillExportDetail", "A", new string[] { "@InvoiceNumber", "@ID", "@UpdatedDate" }, new object[] { txtInvoiceNumber.Text.Trim(), billExportSaleID, txtDateTime.Value });
            }
            return true;
        }

        /// <summary>
        /// click vào khách hàng để tự động hiển thị ra địa chỉ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (dtCustomer.Rows.Count <= 0) return;
            if (cboCustomer.Text.Trim() == "") return;
            DataRow[] dr = dtCustomer.Select($"ID={cboCustomer.EditValue}");
            txtAddress.Text = TextUtils.ToString(dr[0]["Address"]);
        }

        /// <summary>
        /// hàm kiểm tra thông tin nhập trước khi save
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            DataTable dt;
            if (billExportAcountant.ID > 0)
            {
                string Billcode = txtCode.Text.Trim();
                if (Billcode.Contains("PXKT"))
                {
                    Billcode = Billcode.Substring(4);
                }
                int strID = billExportAcountant.ID;
                dt = TextUtils.Select($"Select top 1 ID From BillExportAcountant Where Code LIKE '%{Billcode}%' and ID <> {strID}");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            else
            {
                dt = TextUtils.Select("Select top 1 ID From BillExportAcountant where Code = '" + txtCode.Text.Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    loadBilllNumber();
                    MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtCode.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (txtInvoiceNumber.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số hóa đơn.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboCustomer.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboUser.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn nhân viên.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (grvData.RowCount == 0)
            {
                MessageBox.Show("Xin vui lòng lựa chọn phiếu xuất kho!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void frmGoodsDeliveryNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (billExportAcountant.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void txtDateTime_ValueChanged(object sender, EventArgs e)
        {
            if (billExportAcountant.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //int[] selectedRowHandles = grvExportSale.GetSelectedRows();
            //if (selectedRowHandles.Length == 0)
            //{
            //    MessageBox.Show(String.Format("Xin vui lòng lựa chọn sản phẩm phiếu xuất"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}
            for (int i = 0; i < rowSelectedRow.Count; i++)
            {
                int id = rowSelectedRow[i];
                //int id = TextUtils.ToInt(grvExportSale.GetRowCellValue(rowSelectedRow[i], colID));
                //if (id == 0) continue;
                DataTable dt = TextUtils.LoadDataFromSP("spGetSeachBillExport", "A", new string[] { "@BillExportID" }, new object[] { id });
                if (!lstSelect.Contains(id) || lstSelect.Count == 0)
                {
                    if (!lstSelect.Contains(id)) lstSelect.Add(id);
                    grvData.FocusedRowHandle = -1;
                    btnNew_Click(null, null);
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, colBillExportSaleID, dt.Rows[0]["ID"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductID, dt.Rows[0]["ProductID"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductCode, dt.Rows[0]["ProductCode"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, dt.Rows[0]["ProductName"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductFullName, dt.Rows[0]["ProductFullName"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductNewCode, dt.Rows[0]["ProductNewCode"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, dt.Rows[0]["Unit"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colGroupExport, dt.Rows[0]["GroupExport"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colQty, dt.Rows[0]["Qty"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProjectNameText, dt.Rows[0]["ProjectNameText"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProjectName, dt.Rows[0]["ProjectName"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductTypeText, dt.Rows[0]["ProductTypeText"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductGroupName, dt.Rows[0]["ProductGroupName"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colNote, dt.Rows[0]["Note"]);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, colBillExportSaleID, dt.Rows[0]["ID"]);
                        grvData.SetRowCellValue(i, colProductIDSale, dt.Rows[0]["ProductID"]);
                        grvData.SetRowCellValue(i, colProductName, dt.Rows[0]["ProductName"]);
                        grvData.SetRowCellValue(i, colProductNewCodeExport, dt.Rows[0]["ProductNewCode"]);
                        grvData.SetRowCellValue(i, colUnit, dt.Rows[0]["Unit"]);
                        grvData.SetRowCellValue(i, colProductFullName, dt.Rows[0]["ProductFullName"]);
                        grvData.SetRowCellValue(i, colGroupExport, dt.Rows[0]["GroupExport"]);
                        grvData.SetRowCellValue(i, colQty, dt.Rows[0]["Qty"]);
                        grvData.SetRowCellValue(i, colProjectNameText, dt.Rows[0]["ProjectNameText"]);
                        grvData.SetRowCellValue(i, colProductTypeText, dt.Rows[0]["ProductTypeText"]);
                        grvData.SetRowCellValue(i, colProductGroupName, dt.Rows[0]["ProductGroupName"]);
                        grvData.SetRowCellValue(i, colNote, dt.Rows[0]["Note"]);
                        grvData.SetRowCellValue(i, colProjectName, dt.Rows[0]["ProjectName"]);
                    }
                }
            }
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadBillExportSale();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void txtPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQty || e.Column == colUnitPrice || e.Column == colVAT)
            {
                int qty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
                decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));
                decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVAT));
                if (unitPrice > 0 && qty > 0)
                {
                    double intoMoney;
                    if (vat != 0) intoMoney = TextUtils.ToDouble(qty) * TextUtils.ToDouble(unitPrice) * TextUtils.ToDouble(vat / 100);
                    else intoMoney = TextUtils.ToDouble(qty) * TextUtils.ToDouble(unitPrice);

                    grvData.SetFocusedRowCellValue(colIntoMoney, intoMoney);
                    decimal intoMoneyWithoutVat = TextUtils.ToDecimal(qty) * TextUtils.ToDecimal(unitPrice);
                    grvData.SetFocusedRowCellValue(colIntoMoneyWithoutVat, intoMoneyWithoutVat);

                    decimal totalIntoMoney = TextUtils.ToDecimal(intoMoney) + TextUtils.ToDecimal(intoMoneyWithoutVat);
                    grvData.SetFocusedRowCellValue(colTotalIntoMoney, totalIntoMoney);
                }
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillExportSale();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillExportSale();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillExportSale();
        }

        private void grvExportSale_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colIsInvoice)
            {
                var row = grvExportSale.FocusedRowHandle;
                int id = TextUtils.ToInt(grvExportSale.GetRowCellValue(row, colID));
                string code = TextUtils.ToString(grvExportSale.GetFocusedRowCellValue(colCode));
                string productCodeSale = TextUtils.ToString(grvExportSale.GetFocusedRowCellValue(colProductCodeSale));
                bool isApproved = TextUtils.ToBoolean(grvExportSale.GetFocusedRowCellValue(colIsInvoice));
                if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} chọn sản phẩm [" + productCodeSale + "] của phiếu [" + code + "] này?", isApproved ? "bỏ" : ""), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (isApproved == false)
                    {
                        grvExportSale.SetFocusedRowCellValue(colIsInvoice, 1);
                        if (!rowSelectedRow.Contains(id)) rowSelectedRow.Add(id);
                    }
                    else
                    {
                        grvExportSale.SetFocusedRowCellValue(colIsInvoice, 0);
                        if (rowSelectedRow.Contains(id)) rowSelectedRow.Remove(id);
                    }
                }
            }
        }

        private void btnShowHistoryPrice_Click(object sender, EventArgs e)
        {

        }
    }
}
