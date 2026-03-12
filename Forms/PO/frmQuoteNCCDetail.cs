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
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuoteNCCDetail : _Forms
    {
        public int GroupId;
        public QuoteNCCModel oQuoteNCC = new QuoteNCCModel();
        public frmQuoteNCCDetail()
        {
            InitializeComponent();
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {

            loadCode();
            loadItemType();
            LoadGroup();
            loadGrdData();
            LoadSupplier();
            loadMaster();

            this.cbCodeProduct.EditValueChanged += new EventHandler(cbCodeProduct_EditValueChanged);

            //this.cbMaKH.EditValueChanged += new EventHandler(cbMaKH_EditValueChanged);


            btnSave.Enabled = btnSaveNew.Enabled = btnadd.Enabled = btnth.Enabled = btnDelete.Enabled = !oQuoteNCC.IsApproved;

        }

        void loadMaster()
        {
            if (oQuoteNCC.ID > 0)
            {
                //cboGroup.SetEditValue(oQuoteNCC.GroupID);
                DataTable dt = (DataTable)bs.DataSource;
                dt = TextUtils.LoadDataFromSP("spLoadQuoteNCC", "A", new string[] { "@ID" }, new object[] { oQuoteNCC.ID });
                cboSupplier.EditValue = oQuoteNCC.SupplierID;
                txtQuoteCode.Text = oQuoteNCC.QuoteCode;
                txtCreatedDate.Value = TextUtils.ToDate3(oQuoteNCC.CreatedDate);
                txtTotalPO.EditValue = oQuoteNCC.TotalMoney;
                txtQuoteDate.Value = TextUtils.ToDate3(oQuoteNCC.QuoteDate);
                txtPhone.Text = oQuoteNCC.Phone;
                txtUserName.Text = oQuoteNCC.UserName;
                loadTien();
            }
        }

        void LoadGroup()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM ProductGroup");
            //cboGroup.Properties.DisplayMember = "ProductGroupName";
            //cboGroup.Properties.ValueMember = "ID";
            //cboGroup.Properties.DataSource = dt;
        }

        //nhà cung cấp
        void LoadSupplier()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM SupplierSale ");
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = dt;
        }

        private void loadData()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM dbo.QuoteNCCDetail");
            grdData.DataSource = dt;

        }
        DataTable dttt = new DataTable();
        void loadGrdData()
        {
            DataTable dt = (DataTable)bs.DataSource;
            dt = TextUtils.LoadDataFromSP("spLoadQuoteNCC", "A", new string[] { "@ID" }, new object[] { oQuoteNCC.ID });
            dttt = dt;
            DataColumn data = new DataColumn("Location", typeof(Byte[]));
            dt.Columns.Add(data);
            grdData.DataSource = dt;
            bs.DataSource = dt;
            grdData.DataSource = bs;
        }

        private void cbCodeProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.Focus();
                txtQuoteCode.Focus();
                int productCode = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductCode));
                DataTable dttt = new DataTable();
                dttt = TextUtils.LoadDataFromSP("spLoadProductPONCC", "A", new string[] { "@ID" }, new object[] { productCode });
                //dttt = TextUtils.Select($"SELECT  ID,ProductCode,ProductName,ItemType,Unit FROM [dbo].[ProductSale] where ID='{colID}'");
                string Name = TextUtils.ToString(dttt.Rows[0]["ProductName"]);
                string Type = TextUtils.ToString(dttt.Rows[0]["ItemType"]);
                string Unit = TextUtils.ToString(dttt.Rows[0]["Unit"]);
                grvData.SetFocusedRowCellValue(colProductName, Name);
                grvData.SetFocusedRowCellValue(colMaker, Type);
                grvData.SetFocusedRowCellValue(colUnit, Unit);
            }
            catch (Exception ex)
            { }
        }


        void loadItemType()
        {
            //if (cboGroup.Text == "") return;
            //string ID = TextUtils.ToString(cboGroup.EditValue);
            //string[] Arr = ID.Trim().Split(',');
            //ArrayList arrayList = new ArrayList(Arr);
            //dttt = TextUtils.Select($"SELECT ID,ProductCode,ProductName FROM ProductSale where ProductGroupID IN({PropertyUtils.ToListWithComma(arrayList)})");
            dttt = TextUtils.Select("SELECT ID,ProductCode,ProductName FROM ProductSale");
            cbCodeProduct.DisplayMember = "ProductCode";
            cbCodeProduct.ValueMember = "ID";
            cbCodeProduct.DataSource = dttt;
            colProductCode.ColumnEdit = cbCodeProduct;
        }

        private bool ValidateForm()
        {
            if (txtQuoteCode.Text == "" || cboSupplier.Text == "" )
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (oQuoteNCC.ID == 0)
            {
                DataTable dt;
                if (oQuoteNCC.ID > 0)
                {
                    int strID = oQuoteNCC.ID;
                    dt = TextUtils.Select("select top 1 ID from QuoteNCC where QuoteNCC = '" + txtQuoteCode.Text.Trim() + "'");
                }
                else
                {
                    dt = TextUtils.Select("select top 1 ID from QuoteNCC where QuoteNCC = '" + txtQuoteCode.Text.Trim() + "'");
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

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        

        void loadCode()
        {
            string _month = DateTime.Now.Month.ToString();
            string _day = DateTime.Now.Day.ToString();
            string _year = DateTime.Now.Year.ToString();
            string maPO = _day + _month + _year;

            string code = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 QuoteCode FROM QuoteNCC ORDER BY ID DESC"));
            string[] arr = code.Split('.');
            if (arr.Count() < 2)
            {
                txtQuoteCode.Text = maPO + ".1";
                return;
            }
            string so = TextUtils.ToString("." + (TextUtils.ToInt(arr[1]) + 1));

            txtQuoteCode.Text = maPO + TextUtils.ToString(so);


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }
        bool saveData()
        {
            cboSupplier.Focus();
            grvData.Focus();
            if (!ValidateForm()) return false;
            oQuoteNCC.QuoteCode = TextUtils.ToString(txtQuoteCode.Text);
            oQuoteNCC.CreatedDate = txtCreatedDate.Value;
            oQuoteNCC.TotalMoney = TextUtils.ToInt(txtTotalPO.EditValue);
            oQuoteNCC.Phone = TextUtils.ToString(txtPhone.Text);
            oQuoteNCC.QuoteDate = txtQuoteDate.Value;
            oQuoteNCC.UserName = TextUtils.ToString(txtUserName.Text);
           // oQuoteNCC.GroupID = TextUtils.ToString(cboGroup.EditValue);
            oQuoteNCC.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            if (oQuoteNCC.ID > 0)
            {
                QuoteNCCBO.Instance.Update(oQuoteNCC);
            }
            else
            {
                oQuoteNCC.ID = (int)QuoteNCCBO.Instance.Insert(oQuoteNCC);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                QuoteNCCDetailModel detail = new QuoteNCCDetailModel();

                if (id > 0)
                {
                    detail = (QuoteNCCDetailModel)(QuoteNCCDetailBO.Instance.FindByPK(id));
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.QuoteNCCID = oQuoteNCC.ID; //oQuoteNCC.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductCode));
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
                detail.IntoMoney = TextUtils.ToInt(detail.Qty * detail.UnitPrice);
                detail.IntendTime = TextUtils.ToString(grvData.GetRowCellValue(i, colIntendTime));

                if (detail.ID > 0)
                {
                    QuoteNCCDetailBO.Instance.Update(detail);
                    foreach (int item in lstIDDelete)
                    {
                        QuoteNCCDetailBO.Instance.Delete(item);
                    }
                }
                else
                {
                    QuoteNCCDetailBO.Instance.Insert(detail);
                }
            }
            return true;
        }


        // load tiền trong grvData
        void loadTien()
        {
            int tien = 0;
            decimal total = 0;
            int count = grvData.RowCount;
            grvData.Focus();
            for (int i = 0; i < count; i++)
            {
                long id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                QuoteNCCDetailModel detail = new QuoteNCCDetailModel();
                if (id > 0)
                {
                    detail = (QuoteNCCDetailModel)(QuoteNCCDetailBO.Instance.FindByPK(id));
                }
                total += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
                tien = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty)) * TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
            }
            txtTotalPO.EditValue = total;
        }


        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }


        BindingSource bs = new BindingSource();

        private void miniToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        

        private void txtSuplier_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
        private void txtSuplier_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtdeliver.Focus();
            //}
        }

        private void txtReciver_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //    txtUserName.Focus();
            //}
        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {
            grvData.AddNewRow();
            grvData.FocusedColumn = grvData.VisibleColumns[0];
            grvData.Focus();
        }

        private void btnth_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadItemType();
            }
        }

        private void cbKhoType_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            //loadItemType();
            //loadMaKH();
        }


        List<int> lstIDDelete = new List<int>();
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            loadCode();
            oQuoteNCC = new QuoteNCCModel();

            cboSupplier.Text = "";
            txtUserName.Clear();
            txtTotalPO.EditValue = "";
            txtPhone.Clear();

            lstIDDelete.Clear();
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] : tên [{1}] không?", productCode, ProductName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
        }


        // txtPhone chỉ nhập đc số
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        // số lượng, đơn giá -> focus đến thành tiền
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int soluong = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            int dongia = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUnitPrice));
            if (dongia > 0 && soluong > 0)
            {
                if (e.Column == colQty || e.Column == colUnitPrice)
                {
                    int a = e.RowHandle;
                    grvData.SetFocusedRowCellValue(colIntoMoney, soluong * dongia);
                    loadTien();
                }
                if (e.Column == colQty)
                    loadTien();
            }
        }

        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            frmSupplierDetail frm = new frmSupplierDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplier();
            }
        }
    }
}
