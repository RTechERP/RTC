using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public partial class frmPurchaseRequestDemo : _Forms
    {
        public frmPurchaseRequestDemo()
        {
            InitializeComponent();
        }

        private void frmPurchaseRequestDemo_Load(object sender, EventArgs e)
        {

            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+2).AddDays(-1);
            cboStatusRequest.SelectedIndex = 1;

            cboIsApprovedBGD.SelectedIndex = 0;
            cboIsDeleted.SelectedIndex = 1;
            loadEmployee();
            LoadProject();
            LoadSupplierSale();
            LoadProductGroup();
            LoadCurrency();
            LoadEmployeeAprrove();
            loadData();
        }
        private void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.Enabled = Global.IsAdmin;
            cboEmployee.EditValue = Global.EmployeeID;
        }
        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();

            //DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectPartlistRequest", new string[] { }, new object[] { });
            //DataTable dt = dataSet.Tables[1];
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
        }


        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.DataSource = list;

            //PQ.Chien 19/04/2025====================
            cboSupplierSaleDemo.ValueMember = "ID";
            cboSupplierSaleDemo.DisplayMember = "NameNCC";
            cboSupplierSaleDemo.DataSource = list;
        }

        void LoadProductGroup()
        {
            var exp1 = new Expression("WarehouseID", 1);
            var exp2 = new Expression("ProductGroupNo", "DBH", "NOT LIKE");
            var exp3 = new Expression("ProductGroupNo", "CCDC", "<>");

            var listDemo = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2).And(exp3));
            cboProductGroupRTC.ValueMember = "ID";
            cboProductGroupRTC.DisplayMember = "ProductGroupName";
            cboProductGroupRTC.DataSource = listDemo;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            //PQ.Chien 19/04/2025====================
            cboCurrencyDemo.ValueMember = "ID";
            cboCurrencyDemo.DisplayMember = "Code";
            cboCurrencyDemo.DataSource = list;
        }

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadEmployeeAprrove()
        {
            List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindAll();
            cboEmployeeApprove.ValueMember = "ID";
            cboEmployeeApprove.DisplayMember = "FullName";
            cboEmployeeApprove.DataSource = list;
        }

        private void loadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int statusRequest = cboStatusRequest.SelectedIndex;
            int projectId = TextUtils.ToInt(cboProject.EditValue);
            string keyword = txtKeyword.Text.Trim();

            int supplierSaleId = TextUtils.ToInt(cboSupplier.EditValue);
            int isApprovedBGD = cboIsApprovedBGD.SelectedIndex - 1;
            int isDeleted = cboIsDeleted.SelectedIndex - 1;
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            DataTable dtAll = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New_Khanh", "A",
                     new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@SupplierSaleID", "@IsApprovedTBP", "@IsApprovedBGD", "@IsCommercialProduct", "@POKHID", "@ProductRTCID", "@IsDeleted", "@IsTechBought", "@IsJobRequirement", "@EmployeeID" },
                     new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, -1, isApprovedBGD, 0, 0, -1, isDeleted, -1, 0, employeeID });
            if (dtAll == null || dtAll.Rows.Count == 0)
            {
                grdProductRTC.DataSource = null;
                grdBorrowProduct.DataSource = null;
                return;
            }
            var dataRTC = dtAll.Select("[ProductRTCID] > 0 AND TicketType = 0");
            if (dataRTC.Length > 0) grdProductRTC.DataSource = dataRTC.CopyToDataTable();
            var dataBorrow = dtAll.Select("TicketType = 1");
            if (dataBorrow.Length > 0) grdBorrowProduct.DataSource = dataBorrow.CopyToDataTable();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductRTCPurchaseRequest frm = new frmProductRTCPurchaseRequest(0);
            frm.FormClosed += frmProductRTCPurchaseRequest_FormClosed;
            frm.Show();

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var tabSelected = xtraTabControl1.SelectedTabPage;

                if (tabSelected.Controls.Count <= 0) return;
                GridControl gridControl = (GridControl)tabSelected.Controls[0];
                GridView gridView = gridControl.MainView as GridView;

                int ID = TextUtils.ToInt(gridView.GetFocusedRowCellValue("ID"));
                if (ID == 0)
                {
                    MessageBox.Show("Vui lòng chọn yêu cầu muốn sửa!", "Thông báo");
                    return;
                }
                ProjectPartlistPurchaseRequestModel focusedModel = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(ID);

                if (focusedModel == null)
                {
                    MessageBox.Show("Không tìm thấy yêu cầu này!", "Thông báo");
                    return;
                }
                if (focusedModel.IsDeleted.Value)
                {
                    MessageBox.Show("Yêu cầu này đã bị xóa!", "Thông báo");
                    return;
                }
                if (focusedModel.IsApprovedTBP.Value || focusedModel.IsApprovedBGD.Value)
                {
                    MessageBox.Show("Yêu cầu này đã được phê duyệt TBP!", "Thông báo");
                    return;
                }
                string updateName = TextUtils.ToString(gridView.GetFocusedRowCellValue("UpdatedName"));

                if (!string.IsNullOrEmpty(updateName.Trim()))
                {
                    MessageBox.Show("Yêu cầu này đã có nhân viên mua!", "Thông báo");
                    return;
                }

                int productRTCID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colProductRTCID.FieldName));
                frmProductRTCPurchaseRequest frm = new frmProductRTCPurchaseRequest(productRTCID);
                frm.model = focusedModel;
                frm.FormClosed += frmProductRTCPurchaseRequest_FormClosed;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmProductRTCPurchaseRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmProductRTCPurchaseRequest frm = sender as frmProductRTCPurchaseRequest;
            if (frm.IsChanged)
                loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int[] rowSelected = gridView.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xoá!", "Thông báo");
                return;
            }


            //Check valiadate
            if (!Global.IsAdmin)
            {
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;
                    bool isCommercialProduct = TextUtils.ToBoolean(gridView.GetRowCellValue(row, "IsCommercialProduct"));
                    int poNCC = TextUtils.ToInt(gridView.GetRowCellValue(row, "PONCCID"));

                    string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, "ProductCode"));

                    string updateName = TextUtils.ToString(gridView.GetRowCellValue(row, "UpdatedName"));
                    int requestStatus = TextUtils.ToInt(gridView.GetRowCellValue(row, "StatusRequest"));
                    bool isApprovedTBP = TextUtils.ToBoolean(gridView.GetRowCellValue(row, "IsApprovedTBP"));
                    bool isApprovedBGD = TextUtils.ToBoolean(gridView.GetRowCellValue(row, "IsApprovedBGD"));

                    if (updateName != "" && requestStatus != 1)
                    {
                        MessageBox.Show($"Sản phẩm mã [{productCode}] đã nhân viên mua.\nBạn không thể hủy yêu cầu!", "Thông báo");
                        return;
                    }

                    if (isApprovedTBP)
                    {
                        MessageBox.Show($"Sản phẩm mã [{productCode}] đã được TBP duyệt.\nBạn không thể hủy yêu cầu!", "Thông báo");
                        return;
                    }

                    if (isApprovedBGD)
                    {
                        MessageBox.Show($"Sản phẩm mã [{productCode}] đã được BGD duyệt.\nBạn không thể hủy yêu cầu!", "Thông báo");
                        return;
                    }

                 
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> inventoryProjects = new List<int>();
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;
                    //idDeletes.Add(id);

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    request.IsDeleted = true;

                    //SQLHelper<ProjectPartlistPurchaseRequestModel>.DeleteModelByID(id);
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);


                    int inventoryProjectID = TextUtils.ToInt(gridView.GetRowCellValue(row, "InventoryProjectID"));
                    if (inventoryProjectID > 0) inventoryProjects.Add(inventoryProjectID);
                }


                if (inventoryProjects.Count > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {InventoryProjectModel_Enum.IsDeleted.ToString(),true },
                        {InventoryProjectModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {InventoryProjectModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };
                    string idInventoryProject = string.Join(",", inventoryProjects);
                    var exp = new Expression(InventoryProjectModel_Enum.ID.ToString(), idInventoryProject, "IN");

                    SQLHelper<InventoryProjectModel>.UpdateFields(myDict, exp);
                }
                //if (idDeletes.Count <= 0) return;
                //string idDelete = string.Join(",", idDeletes);
                //string sql = $"UPDATE ProjectPartlistPriceRequest SET IsDeleted = 1 WHERE ID IN ({idDelete})";
                //TextUtils.ExcuteSQL(sql);

                loadData();
            }
        }
    }
}