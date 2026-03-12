using BMS.Model;
using Forms.Technical;
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
	public partial class frmBillExportProductsHCM : _Forms
	{
		int warehouseID = 2;
		public frmBillExportProductsHCM()
		{
			InitializeComponent();
		}

		private void frmBillExportProductsHCM_Load(object sender, EventArgs e)
		{
            this.Text += " - HCM";
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

            frmBillExportDetailProductsHCM frm = new frmBillExportDetailProductsHCM(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0) LoadData();
            }
            //frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //}
        }

		private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            BillExportTechnicalModel billExport = SQLHelper<BillExportTechnicalModel>.FindByID(id);
            frmBillExportDetailProductsHCM frm = new frmBillExportDetailProductsHCM(warehouseID);
            frm.billExport = billExport;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0)
                {
                    LoadData();
                    grvData.FocusedRowHandle = rowHandle;
                }
            }

            //var focusedRowHandle = grvData.FocusedRowHandle;
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;
            //BillExportTechnicalModel model = SQLHelper<BillExportTechnicalModel>.FindByID(ID);
            //frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
            //if (TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus)))
            //{
            //    frm.IsEdit = true;
            //}
            //frm.billExport = model;
            //frm.IDDetail = ID;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //    grvData.FocusedRowHandle = focusedRowHandle;
            //}
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

		private void grvDetail_DoubleClick(object sender, EventArgs e)
		{

		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

		private void btnPrev_Click(object sender, EventArgs e)
		{
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }

		private void btnNext_Click(object sender, EventArgs e)
		{
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

		private void btnLast_Click(object sender, EventArgs e)
		{
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();
        }

		private void txtPageSize_ValueChanged(object sender, EventArgs e)
		{
            txtPageNumber.Text = "1";
            LoadData();
        }
	}
}
