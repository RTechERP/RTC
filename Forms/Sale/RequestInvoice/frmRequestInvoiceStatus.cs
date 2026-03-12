using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Sale.RequestInvoice
{
    public partial class frmRequestInvoiceStatus: _Forms
    {
        public frmRequestInvoiceStatus()
        {
            InitializeComponent();
        }

        private void frmRequestInvoiceStatus_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            DataTable dt = SQLHelper<RequestInvoiceStatusModel>.GetTableData("SELECT * FROM RequestInvoiceStatus WHERE IsDeleted <> 1");
            grdMaster.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RequestInvoiceStatusModel model = new RequestInvoiceStatusModel();
            frmRequestInvoiceStatusDetail frm = new frmRequestInvoiceStatusDetail();
            frm.statusModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            RequestInvoiceStatusModel model = SQLHelper<RequestInvoiceStatusModel>.FindByID(ID);
            frmRequestInvoiceStatusDetail frm = new frmRequestInvoiceStatusDetail();
            frm.statusModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string statusCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colStatusCode));
            if (ID == 0) return;
            if (ID == 1)
            {
                MessageBox.Show("Không thể xóa trạng thái mặc định");
                return;
            }    

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa trạng thái có mã: {0} không?", statusCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                var myDict = new Dictionary<string, object>()
                {
                    { RequestInvoiceStatusModel_Enum.IsDeleted.ToString(),true},
                    { RequestInvoiceStatusModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    { RequestInvoiceStatusModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                };

                SQLHelper<RequestInvoiceStatusModel>.UpdateFieldsByID(myDict, ID);
                grvMaster.DeleteSelectedRows();
            }
        }

        private void frmRequestInvoiceStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.RowHandle >= 0) 
            {
                object idValue = view.GetRowCellValue(e.RowHandle, "ID");

                if (idValue != null && int.TryParse(idValue.ToString(), out int id))
                {
                    if (id == 1)
                    {
                        e.Appearance.BackColor = Color.Pink;
                        e.Appearance.BackColor2 = Color.Pink; 
                    }
                }
            }
        }
    }
}
