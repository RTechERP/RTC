using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmPriceCheckDetail : _Forms
    {
        public int _request;
        public string _product;
        public DataRow dataRow;
        public frmPriceCheckDetail()
        {
            InitializeComponent();
        }

        private void frmPriceCheck_Load(object sender, EventArgs e)
        {
            loadProduct();
            loadSupplier();
            loadData();
            loadUser();
            cbUser.EditValue = Global.UserID;
        }

        void loadSupplier()
        {
            DataTable dt = TextUtils.Select($"Select ID,CodeNCC,NameNCC From SupplierSale");
            cbSupplier.ValueMember = "ID";
            cbSupplier.DisplayMember = "NameNCC";
            cbSupplier.DataSource = dt;
        }
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        void loadProduct()
        {
            DataTable dtProduct = TextUtils.Select("SELECT ID,PartCode,PartName FROM RequestPriceDetail where IsApproved=0");
            cbProduct.Properties.DisplayMember = "PartName";
            cbProduct.Properties.ValueMember = "PartCode";
            cbProduct.Properties.DataSource = dtProduct;
        }
        void loadhistory()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetDatPriceCheck", "A", new string[] { "@IDCheck", "@Product" }, new object[] { _request, cbProduct.EditValue });
            grdHistory.DataSource = dt;
        }
        void loadData()
        {
            if (_request > 0)
                cbProduct.EditValue = _product;
            DataTable dt = TextUtils.Select($"Select * From PriceCheck where RequestID={_request}");         
            grdData.DataSource = dt;
            loadhistory();
        }
        string slow;
        void getData( string note)
        {
            slow = note;
        }
        bool IsApprove = true;
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            DateTime dt = TextUtils.ToDate3(dataRow["DeadLine"]);
            if (dt < DateTime.Now)
            {
                frmNote frm = new frmNote();
                frm.dataNote += getData;
                frm.ShowDialog();
                IsApprove = false;
            }
            grvData.FocusedRowHandle = -1;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                PriceCheckModel price = new PriceCheckModel();
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (ID > 0)
                    price = (PriceCheckModel)PriceCheckBO.Instance.FindByPK(ID);
                price.ProductName = TextUtils.ToString(cbProduct.Text);
                price.ProductCode = TextUtils.ToString(cbProduct.EditValue);
                price.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                price.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                price.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                price.SupplierID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierID));
                price.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                price.RequestID = TextUtils.ToInt(_request);
                price.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
                price.IsSelected = TextUtils.ToBoolean(grvData.GetRowCellValue(i,colIsSelected));
                if (price.IsSelected)
                {
                    TextUtils.ExcuteSQL($"Update RequestPriceDetail set IsApproved ='{IsApprove}' , Qty={price.Qty} , Price ={price.UnitPrice} , FinishTotalPrice={price.TotalPrice}, SupplierID={price.SupplierID}, AskDate='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',AskPriceID={cbUser.EditValue},NoteSlowCheck='{slow}' where ID ={_request}");
                }
                if (price.ID > 0)
                    PriceCheckBO.Instance.Update(price);
                else
                    PriceCheckBO.Instance.Insert(price);

            }
            this.Close();
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvData);
                grvData.SetRowCellValue(grvData.RowCount - 1, colQty,dataRow["Qty"]);
            }
        }
        ArrayList arrDelete = new ArrayList();
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc muốn xóa không !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                arrDelete.Add(ID);
                grvData.DeleteSelectedRows();
            }
        }

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column == colQty || e.Column == colUnitPrice)
            {
                decimal unit = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));
                decimal qty = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
                grvData.SetFocusedRowCellValue(colTotalPrice, qty * unit);
            }    
        }

        private void frmPriceCheckDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
