using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBillDocumentExportTechnicalDetail : _Forms
    {
        public int warehouseID;
        public frmBillDocumentExportTechnicalDetail()
        {
            InitializeComponent();
        }

        public frmBillDocumentExportTechnicalDetail(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmBillDocumentExportTechnicalDetail_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            cboBillDocumentImportType.SelectedIndex = 2;
            cboStatus.SelectedIndex = 2;
            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            LoadCboView();
            LoadDataMaster();
        }

        public void RefresData()
        {
            LoadDataMaster();
        }

        void LoadDataMaster()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int status = cboStatus.SelectedIndex - 1;
            string filterText = txtFilterText.Text.Trim();
            int billDocumentExportType = cboBillDocumentImportType.SelectedIndex;

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetBillDocumentExportTechnicalDetail"
                   , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText", "@WarehouseID", "@BillDocumentExportType" }
                   , new object[] { pageNumber, pageSize, dateTimeS, dateTimeE, status, filterText, warehouseID, billDocumentExportType });

            if (oDataSet.Tables.Count > 0)
            {
                grdMaster.DataSource = oDataSet.Tables[0];

                if (oDataSet.Tables[0].Rows.Count == 0)
                    return;

                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
            }
        }

        void LoadCboView()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Name = "Đã nhận"},
                new {ID = 2, Name = "Đã hủy nhận"},
                new {ID = 3, Name = "Không có"}
            };
            cboViewNew.DisplayMember = "Name";
            cboViewNew.ValueMember = "ID";
            cboViewNew.DataSource = list;
        }

        private void grvMaster_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "PO_Status" || e.Column.FieldName == "BG_Status" || e.Column.FieldName == "HD_Status" || e.Column.FieldName == "BBBGHH_Status")
            {
                object cellValue = view.GetRowCellValue(e.RowHandle, e.Column);
                if (cellValue != null && cellValue.ToString() == "2")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else if (cellValue != null && cellValue.ToString() == "0")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }

            if (e.Column.FieldName == "BillDocumentExportTypeText")
            {
                object cellValue = view.GetRowCellValue(e.RowHandle, e.Column);
                if (cellValue != null && cellValue.ToString() == "Chưa hoàn thành")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadDataMaster();
        }
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnFind_Click(null, null);
            //}
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadDataMaster();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            LoadDataMaster();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            LoadDataMaster();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadDataMaster();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadDataMaster();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.FileName = $"DanhSanhPhieuXuatkhoDemo_{DateTime.Now.ToString("ddMMyyyy ss")}.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvMaster.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    grvMaster.ExportToXls(f.FileName, optionsEx);

                    Process.Start(f.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            if (ID == 0) return;

            frmBillDocumentExportTechnical frm = new frmBillDocumentExportTechnical();
            frm.BillExportTechnicalID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = focusedRowHandle;
                LoadDataMaster();
            }
        }

        private void frmBillDocumentExportTechnicalDetail_Activated(object sender, EventArgs e)
        {
            //RefresData();
        }

        private void cboBillDocumentImportType_DropDownClosed(object sender, EventArgs e)
        {
            LoadDataMaster();
        }

        private void cboStatus_DropDownClosed(object sender, EventArgs e)
        {
            LoadDataMaster();
        }
    }
}
