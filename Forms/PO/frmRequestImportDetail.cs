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
    public partial class frmRequestImportDetail : _Forms
    {
        public int IDMaster;
        public RequestImportModel requestImport = new RequestImportModel();
        public frmRequestImportDetail()
        {
            InitializeComponent();
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {
            //loadCode();          
            loadMaster();
            loadGrdData();
            btnSave.Enabled = btnSaveNew.Enabled = btnadd.Enabled = btnth.Enabled = btnDelete.Enabled = !requestImport.IsApproved;
        }

        void loadMaster()
        {
            if (requestImport.ID > 0)
            {
                txtCreatedDate.Value = TextUtils.ToDate3(requestImport.CreatedDate);
                txtImportDate.Value = TextUtils.ToDate3(requestImport.ImportDate);
                txtImportCode.Text = TextUtils.ToString(requestImport.ImportCode);
                txtImporter.Text = TextUtils.ToString(requestImport.Importer);
                txtRequester.Text = TextUtils.ToString(requestImport.Requester);
                
            }
        }
        DataTable dttt = new DataTable();
        void loadGrdData()
        {
            DataTable dt = (DataTable)bs.DataSource;
            dt = TextUtils.LoadDataFromSP("spLoadRequestImportDetail", "A", new string[] { "@ID" }, new object[] { requestImport.ID });
            grdData.DataSource = dt;
        }

        private bool ValidateForm()
        {
            if (txtImportCode.Text == "" || txtRequester.Text == "" || txtImporter.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (requestImport.ID == 0)
            {
                DataTable dt;
                if (requestImport.ID > 0)
                {                 
                    dt = TextUtils.Select("select top 1 ID from RequestImport where ImportCode = '" + txtImportCode.Text.Trim() + "'");
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
            int so = 0;
            string month = TextUtils.ToString(DateTime.Now.Month);
            string day = TextUtils.ToString(DateTime.Now.Day);
            string year = TextUtils.ToString(DateTime.Now.Year);
            string maPO = day + month + year;
            string code = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 RequestCode FROM RequestExport ORDER BY ID DESC"));

            if (code != "")
                so = TextUtils.ToInt(code.Substring(code.Length - 2)); // tách lấy số cuối

            string dem = TextUtils.ToString(so + 1);
            for (int i = 0; dem.Length < 2; i++)
            {
                dem = "0" + dem;
            }
            string[] arr = code.Split('.');
            if (arr.Count() < 2)
            {
                txtImportCode.Text = "ĐNXK" + maPO + ".01";
                return;
            }
            txtImportCode.Text = "ĐNXK." + maPO + "." + TextUtils.ToString(dem);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }
        bool saveData()
        {
            grvData.Focus();
            if (!ValidateForm()) return false;
            requestImport.Requester = TextUtils.ToString(txtRequester.Text);
            requestImport.ImportCode = TextUtils.ToString(txtImportCode.Text);
            requestImport.Importer = TextUtils.ToString(txtImporter.Text);
            requestImport.ImportDate = TextUtils.ToDate2(txtImportDate.Text);
            requestImport.CreatedDate = TextUtils.ToDate2(txtCreatedDate.Text);
            if (requestImport.ID > 0)
            {
                RequestImportBO.Instance.Update(requestImport);
            }
            else
            {
                requestImport.ID = (int)RequestImportBO.Instance.Insert(requestImport);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                RequestImportDetailModel detail = new RequestImportDetailModel();

                if (id > 0)
                {
                    detail = (RequestImportDetailModel)(RequestImportDetailBO.Instance.FindByPK(id));
                }
                detail.RequestImportID = TextUtils.ToInt(requestImport.ID);
                detail.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                detail.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                detail.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, colMaker));
                detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                detail.IntoMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                detail.Pay = TextUtils.ToString(grvData.GetRowCellValue(i, colPay));
                detail.WareHouse = TextUtils.ToString(grvData.GetRowCellValue(i, colWarehouse));
                detail.Suplier = TextUtils.ToString(grvData.GetRowCellValue(i, colSuplier));
                detail.Project = TextUtils.ToString(grvData.GetRowCellValue(i, colProject));
                detail.POSuplier = TextUtils.ToString(grvData.GetRowCellValue(i, colPOSupiler));
                detail.RequestCode = TextUtils.ToString(grvData.GetRowCellValue(i, colRequestCode));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                if (detail.ID > 0)
                {
                    RequestImportDetailBO.Instance.Update(detail);
                    foreach (int item in lstIDDelete)
                    {
                        RequestImportDetailBO.Instance.Delete(item);
                    }
                }
                else
                {
                    RequestImportDetailBO.Instance.Insert(detail);
                }
            }
            return true;
        }

        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }


        BindingSource bs = new BindingSource();

        private void miniToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

            }
        }




        List<int> lstIDDelete = new List<int>();
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            loadCode();
            requestImport = new RequestImportModel();

            txtRequester.Clear();
            txtImporter.Clear();
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



    }
}
