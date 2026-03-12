using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmQuotationNCCDetail : _Forms
    {
        #region Variables
        public int IDDetail;
        public QuotationNCCModel quotationNCC = new QuotationNCCModel();
        List<int> lstIDDelete = new List<int>();
        DataTable dtProduct = new DataTable();
        DataTable dtProject = new DataTable();
        DataTable dtSupplier = new DataTable();
        #endregion

        public frmQuotationNCCDetail()
        {
            InitializeComponent();
        }

        private void frmQuotationNCCDetail_Load(object sender, EventArgs e)
        {

            loadBillNumber();
            loadProduct();
            loadProject();
            loadUser();
            loadSupplier();
            loadQuotationNCCDetail();

            this.cbProduct.EditValueChanged += new EventHandler(cbProduct_EditValueChanged);
            this.cbProject.EditValueChanged += new EventHandler(cbProject_EditValueChanged);
            btnSave.Enabled = btnSaveNew.Enabled = btnadd.Enabled = btnNewProduct.Enabled = btnDelete.Enabled = !quotationNCC.IsApproved;
        }

        #region Methods
        /// <summary>
        /// Lấy danh sách báo giá ncc detail
        /// </summary>
        void loadQuotationNCCDetail()
        {
            if (quotationNCC.ID > 0)
            {
                cboSupplier.EditValue = quotationNCC.SupplierID;
                txtQuoteCode.Text = quotationNCC.QuoteCode;
                txtTotalPO.EditValue = quotationNCC.TotalMoney;
                txtContactPhone.Text = quotationNCC.Phone;
                txtCreateDate.Value = (DateTime)quotationNCC.CreateDate;
                txtQuoteDate.Value = (DateTime)quotationNCC.QuoteDate;
                cbProject.EditValue = quotationNCC.ProjectID;
                cboUser.EditValue = quotationNCC.UserID;
                cboStatus.SelectedIndex = quotationNCC.Status;
                cbContact.Text = quotationNCC.ContactName;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetQuotationNCCDetail", "A", new string[] { "@ID" }, new object[] { quotationNCC.ID });
            grdData.DataSource = dt;
            calculateFinishTotal();
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        void loadSupplier()
        {
            dtSupplier = TextUtils.Select("SELECT s.*,sc.SupplierID,sc.SupplierName,sc.SupplierPhone FROM dbo.SupplierSale s Left join [RTC].[dbo].[SupplierSaleContact] sc on sc.SupplierID = s.ID");
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = dtSupplier;
        }

        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        void loadProduct()
        {
            dtProduct = TextUtils.Select("SELECT ID,ProductCode,ProductName,Maker,Unit FROM ProductSale");
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
            colProductID.ColumnEdit = cbProduct;
        }

        /// <summary>
        /// Lấy danh sách dự án lên combo
        /// </summary>
        private void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,ProjectName,UserID,ContactID From Project");
            cbProject.Properties.DisplayMember = "ProjectName";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dtProject;
        }

        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        private void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// load mã báo giá
        /// </summary>
        void loadBillNumber()
        {
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string year = DateTime.Now.Year.ToString();
            string maPO = day + month + year;

            string code = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 QuoteCode FROM QuotationNCC order by ID desc"));
            string[] arr = code.Split('.');
            if (arr.Count() < 2)
            {
                txtQuoteCode.Text = maPO + ".1";
                return;
            }
            string number = TextUtils.ToString("." + (TextUtils.ToInt(arr[1]) + 1));
            txtQuoteCode.Text = maPO + TextUtils.ToString(number);
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData()) this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click button save  new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            cbProject.Text = "";
            cboSupplier.Text = "";
            txtTotalPO.EditValue = "";
            txtContactPhone.Clear();
            cboStatus.Text = "";
            cboUser.Text = "";
            cbContact.Text = "";
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
            loadBillNumber();
            quotationNCC = new QuotationNCCModel();
        }

        /// <summary>
        /// click button thêm dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;
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
        /// click button xóa dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa sản phẩm [{0}] không?", productName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
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
                loadSupplier();
            }
        }

        /// <summary>
        /// click button thêm dự án
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }
        #endregion

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.Focus();
                txtQuoteCode.Focus();
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
                DataRow[] rows = dtProduct.Select("ID = " + ID);
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    string maker = TextUtils.ToString(rows[0]["Maker"]);
                    grvData.SetFocusedRowCellValue(colProductName, productName);
                    grvData.SetFocusedRowCellValue(colMaker, maker);
                    grvData.SetFocusedRowCellValue(colUnit, unit);
                }
            }
            catch (Exception)
            { }
        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            DataRow[] row = dtProject.Select("ID=" + cbProject.EditValue);
            if (row.Length > 0)
            {
                cboUser.EditValue = row[0]["UserID"];
                //cbContact.EditValue = row[0]["ContactID"];
            }
        }

        /// <summary>
        /// ckeck lỗi
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            DataTable dt;
            if (txtQuoteCode.Text == "")
            {
                MessageBox.Show("Xin hãy diền mã báo giá", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cboSupplier.Text == "")
            {
                MessageBox.Show("Xin hãy chọn nhà cung cấp", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cbProject.Text == "")
            {
                MessageBox.Show("Xin hãy chọn dự án", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (quotationNCC.ID > 0)
            {
                dt = TextUtils.Select("select top 1 ID from QuotationNCC where QuotationNCC = '" + txtQuoteCode.Text.Trim() + "' and ID <> " + quotationNCC.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 ID from QuotationNCC where QuotationNCC = '" + txtQuoteCode.Text.Trim() + "'");
            }
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm save
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (quotationNCC.ID == 0) loadBillNumber();
            if (!ValidateForm()) return false;

            grvData.Focus();
            txtQuoteCode.Focus();

            quotationNCC.QuoteCode = txtQuoteCode.Text.Trim();
            quotationNCC.CreateDate = txtCreateDate.Value;
            quotationNCC.TotalMoney = TextUtils.ToInt(txtTotalPO.EditValue);
            quotationNCC.Phone = txtContactPhone.Text.Trim();
            quotationNCC.QuoteDate = txtQuoteDate.Value;
            quotationNCC.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            quotationNCC.ProjectID = TextUtils.ToInt(cbProject.EditValue);
            quotationNCC.UserID = TextUtils.ToInt(cboUser.EditValue);
            quotationNCC.Status = cboStatus.SelectedIndex;
            quotationNCC.ContactName = TextUtils.ToString(cbContact.SelectedItem);


            if (quotationNCC.ID > 0)
            {
                QuotationNCCBO.Instance.Update(quotationNCC);
            }
            else
            {
                quotationNCC.ID = (int)QuotationNCCBO.Instance.Insert(quotationNCC);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                QuotationNCCDetailModel detail = new QuotationNCCDetailModel();

                if (id > 0)
                {
                    detail = (QuotationNCCDetailModel)(QuotationNCCDetailBO.Instance.FindByPK(id));
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.QuotationNCCID = quotationNCC.ID; //quotationNCC.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
                detail.IntoMoney = TextUtils.ToInt(detail.Qty * detail.UnitPrice);
                detail.IntendTime = TextUtils.ToString(grvData.GetRowCellValue(i, colIntendTime));

                if (detail.ID > 0)
                {
                    QuotationNCCDetailBO.Instance.Update(detail);
                    foreach (int item in lstIDDelete)
                    {
                        QuotationNCCDetailBO.Instance.Delete(item);
                    }
                }
                else
                {
                    QuotationNCCDetailBO.Instance.Insert(detail);
                }
            }
            return true;
        }


        /// <summary>
        /// hàm tính FinishTotal
        /// </summary>
        void calculateFinishTotal()
        {
            int tien = 0;
            decimal total = 0;
            int count = grvData.RowCount;
            grvData.Focus();
            for (int i = 0; i < count; i++)
            {
                long id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                QuotationNCCDetailModel detail = new QuotationNCCDetailModel();
                if (id > 0)
                {
                    detail = (QuotationNCCDetailModel)(QuotationNCCDetailBO.Instance.FindByPK(id));
                }
                total += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
                tien = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty)) * TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
            }
            txtTotalPO.EditValue = total;
        }

        /// <summary>
        /// hàm đóng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// txtPhone chỉ nhập đc số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        /// số lượng, đơn giá -> focus đến thành tiền 
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int qty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            int unitPrice = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUnitPrice));
            if (unitPrice > 0 && qty > 0)
            {
                if (e.Column == colQty || e.Column == colUnitPrice)
                {
                    int a = e.RowHandle;
                    grvData.SetFocusedRowCellValue(colIntoMoney, qty * unitPrice);
                    calculateFinishTotal();
                }
                if (e.Column == colQty)
                    calculateFinishTotal();
            }
        }

        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            cbContact.Items.Clear();
            txtContactPhone.Clear();
            cbContact.ResetText();
            if (dtSupplier.Rows.Count <= 0) return;
            if (cboSupplier.Text.Trim() == "") return;

            DataRow[] dr = dtSupplier.Select($"ID={cboSupplier.EditValue}");
            if (dr.Length > 0)
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    cbContact.Items.Add(dr[i]["SupplierName"].ToString());
                }
            }
        }

        private void cbContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbContact.Items.Count > 0)
            {
                DataTable dtContact = TextUtils.Select($"SELECT * FROM [RTC].[dbo].[SupplierSaleContact] where SupplierName = N'{cbContact.Text}'");
                DataRow[] dr = dtContact.Select();
                if (dr.Length > 0)
                {
                    txtContactPhone.Text = TextUtils.ToString(dr[0]["SupplierPhone"]);
                }
            }
        }
    }
}
