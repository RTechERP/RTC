using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmDailyReportSaleBN : _Forms
    {
        int warehouseID = 0;

        int[] idAdminSale = new int[] { 1177, 1313, 23, 1380, 1132, 11, 17 };
        public frmDailyReportSaleBN(int warehouseID)
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
            LoadCustomer();
            LoadUser();
            LoadGroup();
            LoadDailyReportSaleBN();
        }

        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void LoadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void LoadUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "UserID";
            cbUser.Properties.DataSource = dt;
            cbUser.EditValue = Global.UserID;

            //bool isAdmin = (!Global.IsAdmin && !Global.IsAdminSale && Global.UserID != 1177 && Global.UserID != 1313 && Global.UserID != 23 && Global.UserID != 1380);
            //bool isAdmin = idAdminSale.Contains(Global.UserID);
            //if (!isAdmin && !Global.IsAdmin && !Global.IsAdminSale)
            //{
            //    cbUser.EditValue = Global.UserID;
            //    cbUser.Enabled = false;
            //}
        }
        void LoadGroup()
        {
            List<MainIndexModel> list = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { "@Type" }, new object[] { 2});
            cbGroupType.Properties.DisplayMember = "MainIndex";
            cbGroupType.Properties.ValueMember = "ID";
            cbGroupType.Properties.DataSource = list;
        }

        /// <summary>
        /// load DailyReportSale
        /// </summary>
        private void LoadDailyReportSaleBN()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            //warehouseID = 3; //Bắc Ninh
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Text);
            string keyword = txtFilterText.Text.Trim();
            int customerID = TextUtils.ToInt(cbCustomer.EditValue);
            int userID = TextUtils.ToInt(cbUser.EditValue);
            int groupType = TextUtils.ToInt(cbGroupType.EditValue);
            groupType = groupType == 0 ? groupType - 1 : groupType;
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetDailyReportSale"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@CustomerID", "@UserID", "@GroupType", "@WarehouseID" }
                , new object[] { pageNumber, pageSize, dateTimeS, dateTimeE, keyword, customerID, userID, groupType, warehouseID });
            grdData.DataSource = oDataSet.Tables[0];

            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }
        #endregion

        #region Button Events
        /// <summary>
        /// click button add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DailyReportSaleModel model = new DailyReportSaleModel();
            frmDailyReportSaleBNDetail frm = new frmDailyReportSaleBNDetail(warehouseID);
            frm.dailyReportSaleModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDailyReportSaleBN();
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
        //        loadDailyReportSaleBN();
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
            DailyReportSaleModel model = SQLHelper<DailyReportSaleModel>.FindByID(ID);
            frmDailyReportSaleBNDetail frm = new frmDailyReportSaleBNDetail(warehouseID);
            frm.dailyReportSaleModel = model;
            if (CheckRole(salePersonID))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadDailyReportSaleBN();
                    grvData.FocusedRowHandle = focusedRowHandle;
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền sửa!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
            LoadDailyReportSaleBN();
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
            LoadDailyReportSaleBN();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadDailyReportSaleBN();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadDailyReportSaleBN();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadDailyReportSaleBN();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadDailyReportSaleBN();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            LoadDailyReportSaleBN();
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            LoadDailyReportSaleBN();
        }

        private void cbGroupType_EditValueChanged(object sender, EventArgs e)
        {
            LoadDailyReportSaleBN();
        }
        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadDailyReportSaleBN();
        }

        private void dtpEndDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadDailyReportSaleBN();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadDailyReportSaleBN();
        }
        private void btnExpott_Click(object sender, EventArgs e)
        {
            //MyLib.ExportExcelGrid(grvData);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sfd.FileName = $"DailyReportSaleBN_{cbUser.Text}_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx";
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
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}