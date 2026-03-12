using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmInvoice : _Forms
    {
        public int billImportDetailsID = 0;
        public string maHd = "";
        public List<int> listInvoiceID = new List<int>();
        public List<AccountingBillModel> listInvoice = new List<AccountingBillModel>();
        private DataTable data;

        //Lee min khooi update 09/08/2024
        public List<BillImportDetailModel> billDetails = new List<BillImportDetailModel>();
        public frmInvoice()
        {
            InitializeComponent();
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            data = TextUtils.LoadDataFromSP("spGetAllInvoice", "A", new string[] { "@BillImportDetailID" }, new object[] { billImportDetailsID });
            grdData.DataSource = data;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int STT = TextUtils.ToInt(grvData.GetFocusedRowCellDisplayText(colSTT));
            if (ID > 0)
            {
                MessageBox.Show("Hóa đơn đang được sử dụng! Không thể xóa!");
                return;
            }
            if (MessageBox.Show(String.Format("Bạn có chắc chắn muốn xóa Hóa đơn có STT [{0}] không?", STT), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    grvData.SetRowCellValue(i, colSTT, i + 1);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            //Lee min khooi update 09/08/2024
            foreach (BillImportDetailModel item in billDetails)
            {
                SQLHelper<InvoiceLinkModel>.DeleteByAttribute("BillImportDetailID", item.ID);
            }
            string _maHoaDon = "";
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int Id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                AccountingBillModel model = SQLHelper<AccountingBillModel>.FindByID(Id) ?? new AccountingBillModel();
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsBillImportDetailID));
                if (!isCheck || model.ID <= 0) continue;
                
                if (billDetails.Count > 0)
                {
                    foreach (BillImportDetailModel item in billDetails)
                    {
                        InvoiceLinkModel invoice = new InvoiceLinkModel();
                        invoice.BillImportDetailID = item.ID;
                        invoice.AccountingBillID = model.ID;
                        SQLHelper<InvoiceLinkModel>.Insert(invoice);
                    }
                }
                else listInvoice.Add(model);

            }

            if (listInvoice.Count <= 0 && billDetails.Count <= 0)
            {
                SQLHelper<InvoiceLinkModel>.DeleteByAttribute("BillImportDetailID", billImportDetailsID);
            }

            maHd = string.Join("\n", listInvoice.Select(x => x.BillNumber));

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            frmAccountingBillDetails frm = new frmAccountingBillDetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            };
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            AccountingBillModel model = SQLHelper<AccountingBillModel>.FindByID(ID);
            frmAccountingBillDetails frm = new frmAccountingBillDetails();
            if (model.ID > 0)
            {
                frm.accBill = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    grvData.FocusedRowHandle = rowHandle;
                };

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBillNumber));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (MessageBox.Show(string.Format("Bạn có chác chắn muốn xóa hoá đơn thanh toán [{0}] không ?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AccountingBillModel bill = SQLHelper<AccountingBillModel>.FindByID(ID);
                if (bill.ID <= 0) return;

                bill.IsDeleted = true;
                SQLHelper<AccountingBillModel>.Update(bill);
                LoadData();
            }
        }
    }
}
