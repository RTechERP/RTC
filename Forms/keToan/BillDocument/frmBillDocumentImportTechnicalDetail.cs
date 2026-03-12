using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBillDocumentImportTechnicalDetail : _Forms
    {
        public int warehouseID;
        public frmBillDocumentImportTechnicalDetail()
        {
            InitializeComponent();
        }
        public frmBillDocumentImportTechnicalDetail(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        public void RefresData()
        {
            LoadDataMaster();
        }

        private void frmBillDocumentImportTechnicalDetail_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            cboBillDocumentImportType.SelectedIndex = 2;

            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            LoadCboView();
            LoadDataMaster();
        }

        void LoadDataMaster()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetBillDocumentImportTechnicalDetail"
                   , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@BillDocumentImportType", "@WarehouseID" }
                   , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim(), cboBillDocumentImportType.SelectedIndex, warehouseID });

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

            if (e.Column.FieldName == "PXKtext" || e.Column.FieldName == "POtext" || e.Column.FieldName == "BBBGtext")
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

            if (e.Column.FieldName == "BillDocumentImportTypeText")
            {
                object cellValue = view.GetRowCellValue(e.RowHandle, e.Column);
                if (cellValue != null && cellValue.ToString() == "Chưa hoàn thành")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadDataMaster();
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

        private void txtPageSize_ValueChanged_1(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadDataMaster();
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
            if (ID == 0) return;
            //BillDocumentImportTechnicalModel model = SQLHelper<BillDocumentImportTechnicalModel>.FindByID(ID);

            frmBillDocumentImportTechnical frm = new frmBillDocumentImportTechnical();
            //frm.BDITModel = model;
            frm.billImportTechnicalID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = focusedRowHandle;
                LoadDataMaster();
            }

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.FileName = $"DanhSanhPhieuNhapKhoDemo_{DateTime.Now.ToString("ddMMyyyy ss")}.xls";
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

        private void frmBillDocumentImportTechnicalDetail_Activated(object sender, EventArgs e)
        {
            //RefresData();
        }

        private void cboBillDocumentImportType_DropDownClosed(object sender, EventArgs e)
        {
            LoadDataMaster();
        }
    }
}
