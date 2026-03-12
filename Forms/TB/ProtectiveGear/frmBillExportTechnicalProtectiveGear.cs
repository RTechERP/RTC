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
    public partial class frmBillExportTechnicalProtectiveGear : _Forms
    {
        int warehouseID = 5;
        public frmBillExportTechnicalProtectiveGear()
        {
            InitializeComponent();
        }

        private void frmBillExportTechnicalProtectiveGear_Load(object sender, EventArgs e)
        {
            

            DateTime datenow = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            dtpDateStart.Value = datenow.AddMonths(-1);

            LoadData();
        }


        void LoadData()
        {

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);


            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int status = cboStatus.SelectedIndex;

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetBillExportTechnical"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText", "@WarehouseID" }
                , new object[] { pageNumber, pageSize, dateStart, dateEnd, status, txtKeyword.Text.Trim(), warehouseID });
            grdData.DataSource = dataSet.Tables[0];
            //if (dataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dataSet.Tables[1].Rows[0]["TotalPage"]);

            LoadDetail();
        }

        void LoadDetail()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetail_New", "A", new string[] { "@Id" }, new object[] { ID });
            grdDetail.DataSource = dt;
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBillExportDetailTechnicalProtectiveGear frm = new frmBillExportDetailTechnicalProtectiveGear(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0) LoadData();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            BillExportTechnicalModel billExport = SQLHelper<BillExportTechnicalModel>.FindByID(id);
            frmBillExportDetailTechnicalProtectiveGear frm = new frmBillExportDetailTechnicalProtectiveGear(warehouseID);
            frm.billExport = billExport;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0)
                {
                    LoadData();
                    grvData.FocusedRowHandle = rowHandle;
                }
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;

            bool status = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus));
            string billCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

            if (status)
            {
                MessageBox.Show($"Phiếu nhập [{billCode}] đã được duyệt.\nBạn không thể xóa phiếu này!)", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiếu nhập [{billCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {"IsDelete",true },
                };

                SQLHelper<BillImportTechnicalModel>.DeleteModelByID(id);
                SQLHelper<BillImportDetailTechnicalModel>.DeleteByAttribute("BillImportTechID", id);

                //thêm lịch sử người xóa phiếu
                HistoryDeleteBillModel history = new HistoryDeleteBillModel()
                {
                    BillID = id,
                    UserID = Global.UserID,
                    DeleteDate = DateTime.Now,
                    Name = Global.AppUserName,
                    TypeBill = billCode
                };

                SQLHelper<HistoryDeleteBillModel>.Insert(history);

                grvData.DeleteSelectedRows();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvDetail.GetFocusedRowCellValue(grvDetail.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
    }
}
