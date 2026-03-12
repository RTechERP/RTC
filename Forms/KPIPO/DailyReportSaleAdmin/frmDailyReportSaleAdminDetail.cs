using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace BMS
{
    public partial class frmDailyReportSaleAdminDetail : _Forms
    {
        public int warehouseID = 0;

        public DailyReportSaleAdminModel dailyReportSaleAdminModel = new DailyReportSaleAdminModel();
        ArrayList lstIDDelete = new ArrayList();

        public frmDailyReportSaleAdminDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        private void frmDailyReportSaleAdminDetail_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized; // mở toàn form 
            //WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            //this.Text += $" - {warehouse.WarehouseCode}";
            LoadCustomer();
            LoadReportType();
            LoadEmployee();
            //LoadData();
            LoadProject();
            LoadDailyReportDetail();
        }
        #region LoadCombo 
        private void LoadCustomer()
        {
            List<CustomerModel> listCustomer = SQLHelper<CustomerModel>.FindAll();
            Customer.DisplayMember = "CustomerName";
            Customer.ValueMember = "ID";
            Customer.DataSource = listCustomer;
        }
        private void LoadEmployee()
        {
            //List<EmployeeModel> listEmployee = SQLHelper<EmployeeModel>.FindAll();
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployee.DisplayMember = "FullName";
            cboEmployee.ValueMember = "ID";
            cboEmployee.DataSource = dt;
            // Employee request
            //EmployeeRepuest.DisplayMember = "FullName";
            //EmployeeRepuest.ValueMember = "ID";
            //EmployeeRepuest.DataSource = dt;

        }


        void LoadProject()
        {
            List<ProjectModel> projects = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProjectID.ValueMember = "ID";
            cboProjectID.DisplayMember = "ProjectCode";
            cboProjectID.DataSource = projects;
        }
        private void LoadReportType()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,Name FROM dbo.ReportType"); /=> Không dùng kiểu này vì dê viết sai =))) tìm  bug khó
            List<ReportTypeModel> listReportType = SQLHelper<ReportTypeModel>.FindAll();
            cboReportTypes.ValueMember = "ID";
            cboReportTypes.DisplayMember = "ReportTypeName";
            cboReportTypes.DataSource = listReportType;
        }

        private void LoadData()
        {
            //if(dailyReportSaleAdminModel.ID > 0)
            //{
            //     txtPlanNext.Text = dailyReportSaleAdminModel.PlanNextDay;
            //     txtProblem.Text = dailyReportSaleAdminModel.Problem;
            //     txtResult.Text = dailyReportSaleAdminModel.Result;
            //     txtContentReport.Text = dailyReportSaleAdminModel.ReportContent;
            //    dtpReportDate.Value = (DateTime)dailyReportSaleAdminModel.DateReport;
            //     cbReportType.EditValue = dailyReportSaleAdminModel.ReportTypeID;
            //    cbImployeeRequest.EditValue = dailyReportSaleAdminModel.EmployIDRequest;
            //    cbCustomer.EditValue = dailyReportSaleAdminModel.CustomerID;
            //    cbUser.EditValue = dailyReportSaleAdminModel.userID;
            //}
            //DataTable data = TextUtils.GetDataTableFromSP("SPGetDailyReportAdmin",
            //    new string[] { "@TimeStart", "@TimeEnd", "@CustomerID", "@EmployeeID",  "@ID" , "@KeyWord" },
            //    new object[] { "2021-01-01", DateTime.Now, 0, 0, -1 ,""});
            //grdData.DataSource = data;
        }
        #endregion
        #region event button 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
                //Close();
            }
        }
        // cất và thêm mới
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (SaveData())
            {
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                dailyReportSaleAdminModel = new DailyReportSaleAdminModel();
            }

            
            //  LoadData();
        }
        #endregion
        #region Validate 
        //private bool ValidateForm()
        //{
        //    for (int i = 0; i < grvData.RowCount; i++)
        //    {
        //        if (grvData.GetRowCellValue(i, colPlanNext).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng nhập kế hoạch tiếp theo");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colReportContent).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng nhập nội dung báo cáo");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colResult).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng nhập kết quả");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colEmployeeID).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng chọn nhân viên");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colEmploymentRequest).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng chọn người yêu cầu");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colCustomerID).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng chọn khách hàng");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colReportTypeID).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng chọn loại báo cáo");
        //            return false;
        //        }
        //        if (grvData.GetRowCellValue(i, colDateReport).ToString().Trim() == "")
        //        {
        //            MessageBox.Show("Vui lòng nhập ngày báo cáo");
        //            return false;
        //        }
        //        if (Convert.ToDateTime(grvData.GetRowCellValue(i, colDateReport)) > DateTime.UtcNow)
        //        {
        //            MessageBox.Show("Vui lòng không nhập thời gian trước thời gian hiện tại!!");
        //            return false;
        //        }
        //    }
        //    return true;
        ////}

        private bool ValidateForm()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colReportContent))))
                {
                    MessageBox.Show("Vui lòng nhập nội dung báo cáo");
                    return false;
                }
                if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colResult))))
                {
                    MessageBox.Show("Vui lòng nhập kết quả");
                    return false;
                }
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID)) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên");
                    return false;
                }
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colReportTypeID)) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn loại báo cáo");
                    return false;
                }
                if (!TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateReport)).HasValue)
                {
                    MessageBox.Show("Vui lòng nhập ngày báo cáo");
                    return false;
                }
                //if (Convert.ToDateTime(grvData.GetRowCellValue(i, colDateReport)).Day > DateTime.UtcNow.Day)
                //{
                //    MessageBox.Show("Vui lòng không nhập thời gian trước thời gian hiện tại!!");
                //    return false;
                //}
            }
            return true;
        }

        #endregion
        #region Save data 
        private bool SaveData()
        {

            grvData.CloseEditor();
            if (!ValidateForm())return false;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                long ID = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                DailyReportSaleAdminModel dailyReportSaleAdminModel = new DailyReportSaleAdminModel();
                if (ID > 0)
                {
                    dailyReportSaleAdminModel = (DailyReportSaleAdminModel)DailyReportSaleAdminBO.Instance.FindByPK(ID);
                }
                dailyReportSaleAdminModel.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                dailyReportSaleAdminModel.PlanNextDay = TextUtils.ToString(grvData.GetRowCellValue(i, colPlanNext));
                dailyReportSaleAdminModel.Problem = TextUtils.ToString(grvData.GetRowCellValue(i, colProblemt));
                dailyReportSaleAdminModel.ProblemSolve = TextUtils.ToString(grvData.GetRowCellValue(i, colSolveProblem));
                dailyReportSaleAdminModel.ReportContent = TextUtils.ToString(grvData.GetRowCellValue(i, colReportContent));
                dailyReportSaleAdminModel.Result = TextUtils.ToString(grvData.GetRowCellValue(i, colResult));
                dailyReportSaleAdminModel.EmployeeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID));
                dailyReportSaleAdminModel.EmployeeRequestID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmploymentRequest));
                dailyReportSaleAdminModel.CustomerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCustomerID));
                dailyReportSaleAdminModel.ReportTypeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colReportTypeID));
                dailyReportSaleAdminModel.DateReport = TextUtils.ToDate1(grvData.GetRowCellValue(i, colDateReport).ToString());
                dailyReportSaleAdminModel.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID).ToString());

                if (dailyReportSaleAdminModel.ID > 0)
                {
                    BillImportDetailBO.Instance.Update(dailyReportSaleAdminModel);
                }
                else
                {
                    dailyReportSaleAdminModel.ID = (int)BillImportDetailBO.Instance.Insert(dailyReportSaleAdminModel);
                    grvData.SetRowCellValue(i, colID, dailyReportSaleAdminModel.ID);
                }
            }
            if (lstIDDelete.Count > 0)
            {
                BillExportDetailBO.Instance.Delete(lstIDDelete);
                for (int j = 0; j < lstIDDelete.Count; j++)
                {
                    int IdDailyReportAdmin = TextUtils.ToInt(lstIDDelete[j]);
                    BillExportDetailSerialNumberBO.Instance.DeleteByAttribute("DailyReportAdminID", IdDailyReportAdmin);
                }
            }

            return true;
        }
        #endregion
        private void LoadDailyReportDetail()
        {
            btnSave.Enabled = toolStripButton1.Enabled = (dailyReportSaleAdminModel.EmployeeID == Global.EmployeeID || dailyReportSaleAdminModel.ID == 0);
            DataTable dt = TextUtils.GetDataTableFromSP("SPDailyReportSaleAdminGetByID", new string[] { "@ID" }, new object[] { dailyReportSaleAdminModel.ID });
            grdData.DataSource = dt;

        }
        private void grdData_MouseDown(object sender, MouseEventArgs e) // event button add 
        {
            //GridHitInfo info = grvDailyReportAdmin.CalcHitInfo(new Point(e.X, e.Y));
            //if (info.Column == colSTT && e.Y < 40)
            //{
            //    MyLib.AddNewRow(grdData, grvDailyReportAdmin);
            //}

            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;

                    List<int> listSTT = new List<int>();
                    DataTable dt = (DataTable)grdData.DataSource;
                    dt.AcceptChanges();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strSTT = TextUtils.ToString(dt.Rows[i]["STT"]);
                        if (!strSTT.Contains("."))
                        {
                            int stt = TextUtils.ToInt(dt.Rows[i]["STT"]);
                            listSTT.Add(stt);
                        }
                    }

                    DateTime? dateReport = TextUtils.ToDate4(grvData.GetRowCellValue(grvData.FocusedRowHandle, colDateReport));
                    int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colEmployeeID));
                    int reportTypeId = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colReportTypeID));
                    int customerId = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colCustomerID));


                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";
                    dtrow["DateReport"] = dateReport.HasValue ? dateReport.Value:DateTime.Now;
                    dtrow["EmployeeID"] = employeeId <= 0 ? Global.EmployeeID:employeeId;
                    dtrow["ReportTypeID"] = reportTypeId;
                    dtrow["CustomerID"] = customerId;
                    //dtrow["TypeProjectItem"] = typeProjectItem;
                    //dtrow["ID"] = idAdd--;
                    //dtrow["StatusUpdate"] = 1;
                    //dtrow["EmployeeIDRequest"] = employeeRequest;
                    //dtrow["IsApproved"] = 0;
                    //dtrow["Code"] = $"{project.ProjectCode}_{dt.Rows.Count + 1}";
                    dt.Rows.Add(dtrow);

                    grdData.DataSource = dt;
                    grvData.FocusedRowHandle = grvData.RowCount - 1;
                    grvData.FocusedColumn = colReportContent;
                    //grvData.Columns["TypeProjectItem"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                }
            }
        }
        private void grvDailyReportAdmin_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
            {
                return;
            }
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa báo cáo không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (strID > 0)
                {
                    lstIDDelete.Add(strID);
                }
            }
        }
        private void grdData_Click(object sender, EventArgs e)
        {
        }
        private void grvDailyReportAdmin_DoubleClick(object sender, EventArgs e)
        {
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {
            FrmAddReportType frm = new FrmAddReportType();
            //frm.ShowDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadReportType();
            }
        }

        private void frmDailyReportSaleAdminDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cboProjectID_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            ProjectModel project = (ProjectModel)lookUpEdit.GetSelectedDataRow() ?? new ProjectModel();

            grvData.SetFocusedRowCellValue(colProjectID, project.ID);
            grvData.SetFocusedRowCellValue(colCustomerID, project.CustomerID);
        }
    }
}
