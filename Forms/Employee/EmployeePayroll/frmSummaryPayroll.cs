using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSummaryPayroll : _Forms
    {
        public int masterID = 0;
        public frmSummaryPayroll()
        {
            InitializeComponent();
        }

        private void frmSummaryPayroll_Load(object sender, EventArgs e)
        {
            loadDepartment();
            loadEmployee();

            loadData();
        }


        void loadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DataSet dt = TextUtils.LoadDataSetFromSP("spGetEmployeePayrollDetail",
                            new string[] { "@Year", "@Month", "@DepartmentID", "@EmployeeID", "@Keyword" },
                            new object[] { txtYear.Value, txtMonth.Value, departmentID , employeeID, txtKeyword.Text.Trim()});

            bandTitle.Caption = $"BẢNG THANH TOÁN LƯƠNG THÁNG {txtMonth.Value} NĂM {txtYear.Value}";
            bandDayWorkStandard.Caption = TextUtils.ToString(dt.Tables[1].Rows[0]["TotalWorkday"]);
            grdData.DataSource = dt.Tables[0];
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }


        void loadDepartment()
        {
            //DataTable dt = TextUtils.Select("SELECT ID, Code, Name FROM Department");
            var listDepartments = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DataSource = listDepartments;
        }

        void loadEmployee()
        {
            //DataTable dt = TextUtils.Select("SELECT ID, Code, FullName FROM Employee");
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = dt;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xls)|*.xls;*.xls";
            //sfd.FileName = $"BangThanhToanLuong_T{txtMonth.Value}_{txtYear.Value}.xls";

            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"BangThanhToanLuong_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                grvData.OptionsPrint.AutoWidth = false;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    //optionsEx.AllowSortingAndFiltering = DefaultBoolean.True;
                    //optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnEditPayroll_Click(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int payrollID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPayrollID));

            if (id <= 0)
            {
                return;
            }

            EmployeePayrollDetailModel payroll = (EmployeePayrollDetailModel)EmployeePayrollDetailBO.Instance.FindByPK(id);

            //EmployeePayrollDetailModel payrollDetail = SQLHelper<EmployeePayrollDetailModel>
            //    .SqlToModel($"SELECT * FROM dbo.EmployeePayrollDetail WHERE PayrollID = {id} AND EmployeeID = {employeeID}");

            frmSummaryPayrollDetail frm = new frmSummaryPayrollDetail();
            frm.employeePayroll = payroll;
            frm.payrollID = payrollID;

            //frm.employeeID = payroll.EmployeeID;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowhandle;
                loadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int payrollID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPayrollID));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));

            if (id <= 0)
            {
                return;
            }

            TextUtils.ExcuteProcedure("spInsertIntoEmployeePayrollDetail",
                        new string[] { "@PayrollID", "@Year", "@Month", "@EmployeeID", "@LoginName" },
                        new object[] { payrollID, txtYear.Value, txtMonth.Value, employeeID, Global.LoginName });

            loadData();

            grvData.FocusedRowHandle = rowHandle;
        }

        
        

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    int val1 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentSTT"));
                    int val2 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentSTT"));
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvData_CellMerge(object sender, CellMergeEventArgs e)
        {
            int employee1 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle1, colEmployeeID));
            int employee2 = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle2, colEmployeeID));

            if (employee1 == employee2)
            {
                BandedGridColumn col = (BandedGridColumn)e.Column;
                GridBand band = col.OwnerBand.ParentBand;

                if (col.OwnerBand == bandInfo)
                {
                    string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                    string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                    e.Merge = (value1 == value2);
                }

                if (band == bandAllowance || band == bandAllowance || (band == bandBonus && col != colNightShiftMoney) || band == bandDeduction || col == colOT_TotalSalary || col == colRealSalary || col == colActualAmountReceived)
                {
                    decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                    decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                    e.Merge = (value1 == value2) || (value2 == 0);
                }
            }
            
            e.Handled = true;
            return;
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn cập nhật lại bảng lương tháng {txtMonth.Value}/{txtYear.Value} của tất cả nhân viên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    EmployeePayrollDetailBO.Instance.DeleteByAttribute("PayrollID", masterID);
                    TextUtils.ExcuteProcedure("spInsertIntoEmployeePayrollDetail",
                                                    new string[] { "@PayrollID", "@Year", "@Month", "@EmployeeID", "@LoginName" },
                                                    new object[] { masterID, txtYear.Value, txtMonth.Value, 0, Global.LoginName });

                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
            }
        }

        void PublishPayroll(int isPublish)
        {
            int[] rowSelected = grvData.GetSelectedRows();
            List<int> listID = new List<int>();
            string message = isPublish == 1 ? "công bố" : "huỷ công bố";
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {message} danh sách bảng lương đã chọn trên web không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    listID.Add(id);
                }
            }

            if (listID.Count > 0)
            {
                string query = $"UPDATE dbo.EmployeePayrollDetail SET IsPublish = {isPublish} WHERE ID IN ({string.Join(",", listID)})";
                TextUtils.ExcuteSQL(query);
            }

            loadData();
        }

        private void btnCancelPublish_Click(object sender, EventArgs e)
        {
            PublishPayroll(0);
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            PublishPayroll(1);
        }

        private void grvData_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band != null)
            {
                foreach (BandedGridColumn column in e.Band.Columns)
                {
                    if (column.FieldName == e.Band.View.OptionsSelection.CheckBoxSelectorField)
                    {
                        e.Band.Caption = "";
                        break;
                    }
                }
            }
        }

        private void grvData_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            //GridView view = sender as GridView;
            //view.OptionsView.AllowHtmlDrawGroups = true;
            //e.Appearance.BackColor = Color.FromArgb(1, 240, 240, 240);
            //e.Cache.DrawRectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height, Color.FromArgb(1, 240, 240, 240), 1);
            ////info.GroupText = info.Column.Caption + ": <color=Black>" + info.GroupValueText + "</color> ";
            //info.GroupText += "<color=LightSteelBlue>" + view.GetGroupSummaryText(e.RowHandle) + "</color> ";
            //e.Handled = true;
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        #region VTNam update nhập Excel bảng lương
        private void btnImportExel_Click(object sender, EventArgs e)
        {
            frmPayRollImportExcel frm = new frmPayRollImportExcel();
            frm.payrollID = masterID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void bntSummaryPayroll_Click(object sender, EventArgs e)
        {
            loadDepartment();
            loadEmployee();
            loadData();
        }

        // comment loadData();
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }
        #endregion


    }
}
