using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
namespace BMS
{
    public partial class frmDailyReportSaleAdmin : _Forms
    {
        int warehouseID = 0;
        public frmDailyReportSaleAdmin(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        private void frmDailyReportSaleAdmin_Load(object sender, EventArgs e)
        {
            //this.Text += $" - {this.Tag}";
            LoadComstomer();
            LoadEmployee();
            LoadData();
        }
        private void LoadComstomer()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer");
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0);
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = list;
        }
        private void LoadEmployee()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Employee");
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }
        private void LoadData()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataTable data = TextUtils.GetDataTableFromSP("SPGetDailyReportAdmin",
                new string[] { "@TimeStart", "@TimeEnd", "@CustomerID", "@EmployeeID", "@ID", "@KeyWord" },
                new object[] { dateTimeS, dateTimeE, TextUtils.ToInt(cbCustomer.EditValue), TextUtils.ToInt(cbUser.EditValue), 0, TextUtils.ToString(txtKey.Text) });
            grdData.DataSource = data;


            grvData.OptionsView.AllowCellMerge = true;
            colDateReport.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            colEmployeeFullName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

            grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
        }



        private void btnExpott_Click(object sender, EventArgs e)
        {
            MyLib.ExportExcelGrid(grvData);
        }
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            DailyReportSaleAdminModel model = new DailyReportSaleAdminModel();
            frmDailyReportSaleAdminDetail frm = new frmDailyReportSaleAdminDetail(warehouseID);
            frm.dailyReportSaleAdminModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            DailyReportSaleAdminModel model = (DailyReportSaleAdminModel)DailyReportSaleAdminBO.Instance.FindByPK(ID);
            frmDailyReportSaleAdminDetail frm = new frmDailyReportSaleAdminDetail(warehouseID);
            frm.dailyReportSaleAdminModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            DailyReportSaleAdminModel report = SQLHelper<DailyReportSaleAdminModel>.FindByID(id);
            if (report.EmployeeID != Global.EmployeeID)
            {
                MessageBox.Show("Bạn không thể xoá báo cáo của người khác!", "Thông báo");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xoá báo cáo không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SQLHelper<DailyReportSaleAdminModel>.Delete(report);
                //DailyReportSaleAdminBO.Instance.Delete(id);
                LoadData();
            }
        }
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvData.FocusedRowHandle;
            grvData.FocusedRowHandle = focusedRowHandle - 1;
            LoadData();
            grvData_FocusedRowChanged(null, null);
        }

        private void btnExpott_Click_1(object sender, EventArgs e)
        {
            //MyLib.ExportExcelGrid(grvData);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DailyReportSaleAdmin_{cbUser.Text}_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = true;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {

                    grvData.ExportToXlsx(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column == colDateReport || e.Column == colEmployeeFullName)
            {
                string value1 = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle1, colDateReport)).ToString("ddMMyyyy");
                string value2 = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle2, colDateReport)).ToString("ddMMyyyy");

                string value3 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, colEmployeeFullName));
                string value4 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, colEmployeeFullName));

                e.Merge = (value1 == value2 && value3 == value4);
            }

            e.Handled = true;
            return;
        }
    }
}
