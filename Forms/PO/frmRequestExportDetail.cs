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
    public partial class frmRequestExportDetail : _Forms
    {
        public int GroupId;
        public RequestExportModel oRequestExport = new RequestExportModel();
        public frmRequestExportDetail()
        {
            InitializeComponent();
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {
            loadCode();
            loadItemType();
            loadGrdData();
            loadMaster();

            this.cbProduct.EditValueChanged += new EventHandler(cbProduct_EditValueChanged);

            btnSave.Enabled = btnSaveNew.Enabled = btnadd.Enabled = btnth.Enabled = btnDelete.Enabled = !oRequestExport.IsApproved;
        }

        void loadMaster()
        {
            if (oRequestExport.ID > 0)
            {
                txtRequestCode.Text = oRequestExport.RequestCode;
                txtUserRequest.Text = oRequestExport.UserRequest;
                txtUserExport.Text = oRequestExport.UserExport;
                txtCreatedDate.Value = TextUtils.ToDate3(oRequestExport.CreatedDate);
                txtExportDate.Value = TextUtils.ToDate3(oRequestExport.ExportDate);
                txtNote.Text = oRequestExport.Note;
            }
        }

        void loadGrdData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spLoadRequestExportDetail", "A", new string[] { "@ID" }, new object[] { oRequestExport.ID });
            grdData.DataSource = dt;
        }

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.Focus();
                txtRequestCode.Focus();
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
                DataRow[] rows = dttt.Select("ID = " + ID);
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string itemType = TextUtils.ToString(rows[0]["ItemType"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    grvData.SetFocusedRowCellValue(colProductName, productName);
                    grvData.SetFocusedRowCellValue(colMaker, itemType);
                    grvData.SetFocusedRowCellValue(colUnit, unit);
                }
            }
            catch (Exception ex)
            { }
        }

        DataTable dttt = new DataTable();
        void loadItemType()
        {
            dttt = TextUtils.Select("SELECT ID,ProductCode,ProductName,ItemType,Unit FROM ProductSale");
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dttt;
            colProductID.ColumnEdit = cbProduct;
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
                txtRequestCode.Text = "ĐNXK" + maPO + ".01";
                return;
            }
            txtRequestCode.Text = "ĐNXK." + maPO + "." + TextUtils.ToString(dem);
        }

        private bool ValidateForm()
        {
            if (txtRequestCode.Text == "" || txtUserRequest.Text == "" || txtUserExport.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (oRequestExport.ID == 0)
            {
                DataTable dt;
                if (oRequestExport.ID > 0)
                {
                    int strID = oRequestExport.ID;
                    dt = TextUtils.Select("select top 1 ID from RequestExport where RequestCode = '" + txtRequestCode.Text.Trim() + "' and ID <> " + oRequestExport.ID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 ID from RequestExport where RequestCode = '" + txtRequestCode.Text.Trim() + "'");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }

        bool saveData()
        {
            grvData.Focus();
            if (!ValidateForm()) return false;
            oRequestExport.RequestCode = TextUtils.ToString(txtRequestCode.Text);
            oRequestExport.CreatedDate = txtCreatedDate.Value;
            oRequestExport.ExportDate = txtExportDate.Value;
            oRequestExport.UserExport = TextUtils.ToString(txtUserExport.Text);
            oRequestExport.UserRequest = TextUtils.ToString(txtUserRequest.Text);
            oRequestExport.Note = TextUtils.ToString(txtNote.Text);
            if (oRequestExport.ID > 0)
            {
                RequestExportBO.Instance.Update(oRequestExport);
            }
            else
            {
                oRequestExport.ID = (int)RequestExportBO.Instance.Insert(oRequestExport);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                RequestExportDetailModel detail = new RequestExportDetailModel();

                if (id > 0)
                {
                    detail = (RequestExportDetailModel)(RequestExportDetailBO.Instance.FindByPK(id));
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.RequestID = oRequestExport.ID; //oRequestExport.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                detail.Warehouse = TextUtils.ToString(grvData.GetRowCellValue(i, colWarehouse));
                detail.POKHID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHID));
                detail.Project = TextUtils.ToString(grvData.GetRowCellValue(i, colProject));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));

                if (detail.ID > 0)
                {
                    RequestExportDetailBO.Instance.Update(detail);
                    foreach (int item in lstIDDelete)
                    {
                        RequestExportDetailBO.Instance.Delete(item);
                    }
                }
                else
                {
                    RequestExportDetailBO.Instance.Insert(detail);
                }
            }
            return true;
        }

        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        List<int> lstIDDelete = new List<int>();
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            loadCode();
            oRequestExport = new RequestExportModel();
            txtUserRequest.Clear();
            txtUserExport.Clear();
            txtNote.Clear();

            lstIDDelete.Clear();
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductID));
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
