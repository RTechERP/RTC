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
    public partial class frmBillDocumentExportDetail : _Forms
    {
        public string WarehouseCode;

        public frmBillDocumentExportDetail()
        {
            InitializeComponent();
        }

        public void RefresData()
        {
            loadBillDocumentExportDetail();
        }

        private void frmBillDocumentExportDetail_Load(object sender, EventArgs e)
        {
            //this.Text += " - " + WarehouseCode;

            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            cboBillDocumentExportType.SelectedIndex = 2;
            loadProductGroup();
            cbProductGroup.CheckAll();
            LoadStatus();
            LoadCboView();
            loadBillDocumentExportDetail();
        }

        private void LoadStatus()
        {
            List<object> list = new List<object>() {
                new {ID = -1,Name = "--Tất cả--"},
                new {ID = 0,Name = "Mượn"},
                new {ID = 1,Name = "Tồn Kho"},
                new {ID = 2,Name = "Đã Xuất Kho"},
                //new {ID = 3,Name = "Chia Trước"},
                //new {ID = 4,Name = "Phiếu mượn nội bộ"},
                new {ID = 5,Name = "Xuất trả NCC"},
            };
            cboStatusNew.Properties.DataSource = list;
            cboStatusNew.Properties.ValueMember = "ID";
            cboStatusNew.Properties.DisplayMember = "Name";
            cboStatusNew.EditValue = -1;
        }

        void loadProductGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            cbProductGroup.Properties.DisplayMember = "ProductGroupName";
            cbProductGroup.Properties.ValueMember = "ID";
            cbProductGroup.Properties.DataSource = dt;
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

        private void loadBillDocumentExportDetail()
        {
            //DateTime dateTimeS = new DateTime();
            //if (!chkAllBillExport.Checked)
            //{
            //    dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //    dtpFromDate.Enabled = dtpEndDate.Enabled = true;
            //}
            //else
            //{
            //    DataTable dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate FROM [dbo].[BillExport] where BillDocumentExportType = 1 or BillDocumentExportType = 2 ");
            //    string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
            //    dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
            //    dtpFromDate.Enabled = dtpEndDate.Enabled = false;
            //}

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            if (chkAllBillExport.Checked)
            {
                dateTimeS = TextUtils.MIN_DATE;
                dateTimeE = DateTime.MaxValue;

                dtpFromDate.Enabled = dtpEndDate.Enabled = false;
            }
            int status = TextUtils.ToInt(cboStatusNew.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetBillDocumentExportDetail", "A"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText", "@WarehouseCode",
                    "@BillDocumentExportType"
                }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE,status,cbProductGroup.EditValue, txtFilterText.Text, WarehouseCode,
                    cboBillDocumentExportType.SelectedIndex
                });
            
            grdMaster.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }

        private void chkAllBillExport_CheckedChanged(object sender, EventArgs e)
        {
            loadBillDocumentExportDetail();
        }

        private void grvMaster_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadBillDocumentExportDetail();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillDocumentExportDetail();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillDocumentExportDetail();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            //txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            loadBillDocumentExportDetail();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillDocumentExportDetail();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillDocumentExportDetail();
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            if (ID == 0) return;
            //BillDocumentExportModel model = SQLHelper<BillDocumentExportModel>.FindByAttribute("BillExportID", ID).FirstOrDefault();

            frmBillDocumentExport frm = new frmBillDocumentExport();
            //frm.bDEModel = model;
            frm.billExportID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = focusedRowHandle;
                loadBillDocumentExportDetail();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            f.FileName = $"DanhSanhPhieuXuatkho_{DateTime.Now.ToString("ddMMyyyy ss")}";
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvMaster.OptionsPrint.PrintSelectedRowsOnly = false;
                //grvMaster.OptionsPrint.AutoWidth = true;
                //grvMaster.OptionsPrint.ExpandAllDetails = false;
                //grvMaster.OptionsPrint.PrintDetails = true;
                //grvMaster.OptionsPrint.UsePrintStyles = true;
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


        private void cboStatusNew_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            loadBillDocumentExportDetail();
        }

        private void cbProductGroup_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            loadBillDocumentExportDetail();
        }

        private void cboBillDocumentExportType_DropDownClosed(object sender, EventArgs e)
        {
            loadBillDocumentExportDetail();
        }

        private void dtpFromDate_Enter(object sender, EventArgs e)
        {
            loadBillDocumentExportDetail();
        }
    }
}
