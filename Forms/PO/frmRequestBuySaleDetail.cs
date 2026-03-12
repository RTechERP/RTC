using BMS.Business;
using BMS.Model;
using System;
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
    public partial class frmRequestBuySaleDetail : _Forms
    {
        public RequestBuySaleModel dModel = new RequestBuySaleModel();
        public POKHModel oConvert = new POKHModel();
        DataTable dtProduct = new DataTable();
        public frmRequestBuySaleDetail()
        {
            InitializeComponent();
        }

        private void frmRequestBuySaleDetail_Load(object sender, EventArgs e)
        {
            loadSuplier();
            loadUser();
            loadCustomer();
            loadcbProductCode();
            loadGrdData();
            this.cbProductCode.EditValueChanged += new System.EventHandler(cbProductCode_EditValueChanged);
        }
        void sendData()
        {           
            DataTable dtConvert = new DataTable();
            dtConvert = TextUtils.LoadDataFromSP("spLoadPOKHDetail", "A", new string[] { "@ID" }, new object[] { oConvert.ID });
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spLoadRequestBuySaleDetail", "A", new string[] { "@ID" }, new object[] { dModel.ID });
            for (int i = 0; i < dtConvert.Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["ProductID"] = dtConvert.Rows[i]["ProductID"];
                dt.Rows[i]["Qty"] = dtConvert.Rows[i]["Qty"];
                dt.Rows[i]["UnitPrice"] = dtConvert.Rows[i]["UnitPrice"];
                dt.Rows[i]["IntoMoney"] = dtConvert.Rows[i]["IntoMoney"];
                dt.Rows[i]["Maker"] = dtConvert.Rows[i]["Maker"];
                dt.Rows[i]["Unit"] = dtConvert.Rows[i]["Unit"];
                dt.Rows[i]["ProductName"] = dtConvert.Rows[i]["ProductName"];
            }
            grdData.DataSource = dt;

        }
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        void loadSuplier()
        {
            DataTable dt = TextUtils.Select("SELECT ID,NameNCC,CodeNCC FROM dbo.SupplierSale");
            cbSuplier.DisplayMember = "NameNCC";
            cbSuplier.ValueMember = "ID";
            cbSuplier.DataSource = dt;
        }
        private void cbProductCode_EditValueChanged(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            int IDproduct = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            DataRow[] row = dtProduct.Select("ID=" + IDproduct);
            if (row.Length == 0) return;
            grvData.SetFocusedRowCellValue(colUnit, TextUtils.ToString(row[0]["Unit"]));
            grvData.SetFocusedRowCellValue(colProductName, TextUtils.ToString(row[0]["ProductName"]));
            grvData.SetFocusedRowCellValue(colMaker, TextUtils.ToString(row[0]["Maker"]));
        }
        void loadcbProductCode()
        {   
            dtProduct = TextUtils.Select("SELECT ID,ProductCode,ProductName,Unit,Maker FROM ProductSale");
            cbProductCode.DisplayMember = "ProductCode";
            cbProductCode.ValueMember = "ID";
            cbProductCode.DataSource = dtProduct;
        }

        void loadUser()
        {
            cbUser.Properties.DataSource = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            grvData.AddNewRow();
        }
        void loadGrdData()
        {
            DataTable dt = new DataTable();
            if (dModel.ID > 0)
            {
                txtCode.Text = TextUtils.ToString(dModel.RequestCode);
                txtPOCode.Text = TextUtils.ToString(dModel.POCode);
                cbUser.EditValue  = TextUtils.ToString(dModel.UserID);
                txtProject.Text = TextUtils.ToString(dModel.Project );
                txtTotalPrice.Text  = TextUtils.ToString(dModel.TotalPrice);
            }
            if(oConvert.ID>0)
            {
                sendData();
            }
            else
            {
                dt = TextUtils.LoadDataFromSP("spLoadRequestBuySaleDetail", "A", new string[] { "@ID" }, new object[] { dModel.ID });
                grdData.DataSource = dt;
            }    
            
        }
        List<int> lstDelete = new List<int>();
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDData));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductID));

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa vật tư [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                if (strID > 0)
                {
                    lstDelete.Add(strID);
                }
                grvData.DeleteSelectedRows();

            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            }
        }
        private bool checkValid()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã yêu cầu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                int strID = dModel.ID;
                if (TextUtils.CheckExistTable(strID, "RequestCode", txtCode.Text.Trim(), "RequestBuySale"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }
        void saveData()
        {
            try
            {
                if (!checkValid()) return;

                grvData.FocusedRowHandle = -1;
                for (int i = 0; i < lstDelete.Count; i++)
                {
                    RequestBuySaleDetailBO.Instance.Delete(lstDelete[i]);
                }
                dModel.RequestCode = TextUtils.ToString(txtCode.Text);
                dModel.POCode = TextUtils.ToString(txtPOCode.Text);
                dModel.UserID = TextUtils.ToInt(cbUser.EditValue);
                dModel.Project = TextUtils.ToString(txtProject.Text);
                dModel.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.Text);
                dModel.CustomerID= TextUtils.ToInt(cbCustomer.EditValue);
                if (dModel.ID == 0)
                {
                    dModel.ID = (int)RequestBuySaleBO.Instance.Insert(dModel);

                }
                else
                {
                    RequestBuySaleBO.Instance.Update(dModel);
                }

                int count = grvData.RowCount;
                for (int i = 0; i < count; i++)
                {
                    long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colIDData));
                    RequestBuySaleDetailModel detail = new RequestBuySaleDetailModel();
                    if (id > 0)
                    {
                        detail = (RequestBuySaleDetailModel)RequestBuySaleDetailBO.Instance.FindByPK(id);
                    }
                    detail.RequestBuySaleID = dModel.ID;
                    detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    detail.SuplierID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierID));
                    detail.ContactName = TextUtils.ToString(grvData.GetRowCellValue(i, colContactName));
                    detail.ContactPhone = TextUtils.ToInt(grvData.GetRowCellValue(i, colContactPhone));
                    detail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                    detail.QtyBuy = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyBuy));
                    detail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, colUnitPrice));
                    detail.IntoMoney = TextUtils.ToInt(grvData.GetRowCellValue(i, colIntoMoney));
                    detail.DeadLine = TextUtils.ToDate3(grvData.GetRowCellValue(i, colDeadLine));
                    detail.TargetUse = TextUtils.ToString(grvData.GetRowCellValue(i, colTargetUse));
                    detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                    if (detail.ID == 0)
                    {
                        RequestBuySaleDetailBO.Instance.Insert(detail);
                    }
                    else
                    {
                        RequestBuySaleDetailBO.Instance.Update(detail);
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            if (DialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
