using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using BMS.Utils;

namespace BMS
{
    public partial class frmDailyReportSale : _Forms
    {
        int warehouseID = 0;

        //int[] idAdminSale = new int[] { 1, 2, 1293, 1177, 1313, 23, 1380, 1132, 11, 17, 1185, 1463, 1431 };
        public frmDailyReportSale(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        private void frmDailyReportSale_Load(object sender, EventArgs e)
        {
            //WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            this.Text += $" - {this.Tag}";

            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadCustomer();
            loadUser();
            loadTeam();
            LoadProject();
            LoadEmployeeTeamSale();
            loadDailyReportSale();

            LoadPermission();
        }

        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        /// <summary>
        /// load team
        /// </summary>
        void loadTeam()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,GroupSalesName FROM dbo.GroupSales");
            var list = SQLHelper<GroupSalesModel>.FindAll();
            cbTeam.Properties.DisplayMember = "GroupSalesName";
            cbTeam.Properties.ValueMember = "ID";
            cbTeam.Properties.DataSource = list;

            GroupSalesModel groupSales = SQLHelper<GroupSalesModel>.ProcedureToList("spGetGroupSalesByUserID", new string[] { "@UserID" }, new object[] { Global.UserID })
                                                                .FirstOrDefault();
            groupSales = groupSales ?? new GroupSalesModel();
            cbTeam.EditValue = groupSales.ID;
        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");

            //int group = TextUtils.ToInt(cbTeam.EditValue);
            //DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeManager", new string[] { "@group" }, new object[] { group });
            //DataTable dt = dataSet.Tables[0];

            var users = SQLHelper<EmployeeModel>.FindByExpression(new Expression("UserID", 0, "<>"));

            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "UserID";
            cbUser.Properties.DataSource = users;

            //bool isAdmin = (!Global.IsAdmin && !Global.IsAdminSale && Global.UserID != 1177 && Global.UserID != 1313 && Global.UserID != 23 && Global.UserID != 1380);
        }

        /// <summary>
        /// Load danh sách dự án lên combo
        /// </summary>
        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = list;
        }

        void LoadEmployeeTeamSale()
        {
            List<EmployeeTeamSaleModel> teams = SQLHelper<EmployeeTeamSaleModel>.FindByAttribute(EmployeeTeamSaleModel_Enum.ParentID.ToString(), 0);

            cboEmployeeTeamSale.Properties.DisplayMember = "Name";
            cboEmployeeTeamSale.Properties.ValueMember = "ID";
            cboEmployeeTeamSale.Properties.DataSource = teams;
        }

        /// <summary>
        /// load DailyReportSale
        /// </summary>
        private void loadDailyReportSale()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int employeeTeamSaleID = TextUtils.ToInt(cboEmployeeTeamSale.EditValue);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetDailyReportSale"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@CustomerID", "@UserID", "@GroupType", "@Team", "@ProjectID", "@EmployeeTeamSaleID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim()
                                ,TextUtils.ToInt(cbCustomer.EditValue),TextUtils.ToInt(cbUser.EditValue),TextUtils.ToInt(cbGroupType.SelectedIndex), TextUtils.ToInt(cbTeam.EditValue),projectID,employeeTeamSaleID});
            grdData.DataSource = oDataSet.Tables[0];

            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }
        #endregion


        void LoadPermission()
        {
            string code = "BLESS";
            GroupSalesModel groupSales = (GroupSalesModel)cbTeam.GetSelectedDataRow() ?? new GroupSalesModel();
            if (TextUtils.ToString(groupSales.GroupSalesCode).ToLower() == code.ToLower())
            {
                cbTeam.Enabled = false;
                cbUser.Enabled = true;
            }
            else
            {
                bool isAdmin = Global.idAdminSale.Contains(Global.UserID);
                if (!isAdmin && !Global.IsAdmin && !Global.IsAdminSale)
                {
                    cbUser.EditValue = Global.UserID;
                    cbUser.Enabled = false;
                }
            }
        }


        #region Button Events
        /// <summary>
        /// click button add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DailyReportSaleModel model = new DailyReportSaleModel();
            frmDailyReportSaleDetail frm = new frmDailyReportSaleDetail(warehouseID);
            frm.dailyReportSaleModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDailyReportSale();
            }

        }

        /// <summary>
        /// click button edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    var focusedRowHandle = grvData.FocusedRowHandle;
        //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
        //    if (ID == 0) return;
        //    DailyReportSaleModel model = (DailyReportSaleModel)DailyReportSaleBO.Instance.FindByPK(ID);
        //    frmDailyReportSaleDetail frm = new frmDailyReportSaleDetail();
        //    frm.dailyReportSaleModel = model;
        //    if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        loadDailyReportSale();
        //        grvData.FocusedRowHandle = focusedRowHandle;
        //    }
        //}


        public bool CheckRole(int salePersonID)
        {
            if (Global.IsAdmin || Global.IsAdminSale)
            {
                return true;
            }

            return salePersonID == Global.UserID;
        }


        //sửa chỉ sale phụ trách mới được sửa
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int salePersonID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));//thêm
            if (ID == 0) return;
            DailyReportSaleModel model = (DailyReportSaleModel)DailyReportSaleBO.Instance.FindByPK(ID);
            frmDailyReportSaleDetail frm = new frmDailyReportSaleDetail(warehouseID);
            frm.dailyReportSaleModel = model;
            //if (CheckRole(salePersonID))
            //{
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDailyReportSale();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
            //}
            //else
            //{
            //    MessageBox.Show("Bạn không có quyền sửa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// click button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    var focusedRowHandle = grvData.FocusedRowHandle;
        //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
        //    string customerName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCustomerName));
        //    if (ID == 0) return;

        //    if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa khách hàng : [{0}] không?", customerName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        DailyReportSaleBO.Instance.Delete(ID);
        //        grvData.DeleteSelectedRows();
        //        grvData.FocusedRowHandle = focusedRowHandle;
        //    }
        //}
        //check chỉ sale phụ trách mới được xóa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //string customerName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCustomerName));
            string saleName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            int salePersonID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));

            if (ID == 0) return;

            if (CheckRole(salePersonID))
            {
                if (MessageBox.Show($"Bạn có chắc muốn xoá báo cáo không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DailyReportSaleBO.Instance.Delete(ID);
                    grvData.DeleteSelectedRows();
                    grvData.FocusedRowHandle = focusedRowHandle;
                }
            }
            else
            {
                MessageBox.Show($"Bạn không có quyền xoá báo cáo của nhân viên [{saleName}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadDailyReportSale();
        }
        #endregion

        /// <summary>
        /// event editData by doubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadDailyReportSale();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadDailyReportSale();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadDailyReportSale();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadDailyReportSale();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadDailyReportSale();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            frmDailyReportExcel frm = new frmDailyReportExcel();
            frm.ShowDialog();
        }
        void GetFormImportExcel()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetDataCustomer", new string[] { }, new object[] { });
            Excel.Application app = default(Excel.Application);
            Excel.Workbook workBoook = default(Excel.Workbook);
            Excel.Worksheet workSheet = default(Excel.Worksheet);
            try
            {
                string filePath = "FormNhapDailyReport.xlsx";
                app = new Excel.Application();
                app.Workbooks.Open(filePath);
                workBoook = app.Workbooks[1];
                workSheet = (Excel.Worksheet)workBoook.Worksheets[1];
                app.DisplayAlerts = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                app.ActiveWorkbook.Save();
                app.Workbooks.Close();
                app.Quit();
                //Process.Start(filePath);
            }
        }
        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadDailyReportSale();
        }

        private void btnExpott_Click(object sender, EventArgs e)
        {
            //MyLib.ExportExcelGrid(grvData);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DailyReportSale_{cbUser.Text}_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = true;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {

                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void cbTeam_EditValueChanged(object sender, EventArgs e)
        {
            loadUser();
            loadDailyReportSale();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadDailyReportSale();
        }
    }
}

