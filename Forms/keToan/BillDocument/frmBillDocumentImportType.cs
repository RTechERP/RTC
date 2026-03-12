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
    public partial class frmBillDocumentImportType : _Forms
    {
        private List<int> modifiedRows = new List<int>();
        
        public frmBillDocumentImportType()
        {
            InitializeComponent();
        }

        private void frmBillDocumentImportType_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            cboBillDocumentImportType.SelectedIndex = 2;
            LoadDataMaster();
            LoadCboView();
        }

        #region LOAD DATA
        public void ReloadData()
        {
            LoadDataMaster();
        }

        void LoadDataMaster()
        {

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetBillImportType"
                   , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@BillDocumentImportType" }
                   , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim(), cboBillDocumentImportType.SelectedIndex });

            if (oDataSet.Tables.Count > 0)
            {
                grdMaster.DataSource = oDataSet.Tables[0];

                if (oDataSet.Tables[0].Rows.Count == 0)
                    return;


                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[0].Rows[0]["TotalPage"]);
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

        private void cboBillDocumentImportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataMaster();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
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
        #endregion

        #region OTHERS
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.FileName = $"DanhSanhPhieuNhap_{DateTime.Now.ToString("ddMMyyyy")}.xls";
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

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadDataMaster();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadDataMaster();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadDataMaster();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadDataMaster();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            LoadDataMaster();
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
            if (ID == 0) return;

            frmBillDocumentImport frm = new frmBillDocumentImport();
            frm.billDocumentImportID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
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
        #endregion

        private void grvMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int rowIndex = e.RowHandle;
            if (!modifiedRows.Contains(rowIndex))
            {
                modifiedRows.Add(rowIndex);
            }
        }
    }
}
