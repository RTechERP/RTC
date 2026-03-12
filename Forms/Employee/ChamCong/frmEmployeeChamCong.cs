
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmEmployeeChamCong : _Forms
    {
        public EmployeeChamCongMasterModel employeeChamCong = new EmployeeChamCongMasterModel();
        int idMaster = 0;
        public frmEmployeeChamCong()
        {
            InitializeComponent();
        }

        private void frmEmployeeChamCong_Load(object sender, EventArgs e)
        {
            idMaster = employeeChamCong.ID;
            txtMonth.Value = employeeChamCong._Month;//DateTime.Now.Month;
            txtYear.Value = employeeChamCong._Year; //DateTime.Now.Year;

            btnUpdateData.Enabled = btnUpdateAll.Enabled = !employeeChamCong.IsApproved;

            loadDepartment();
            loadEmployee();
            loadData();

        }

        void loadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList("SELECT ID, Name FROM dbo.Department");

            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DataSource = listDepartment;
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        void loadData()
        {
            int month = TextUtils.ToInt(txtMonth.Value);
            int year = TextUtils.ToInt(txtYear.Value);

            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataSet ds = TextUtils.LoadDataSetFromSP("spGetChamCongNew"
                , new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID", "@KeyWord" }
                , new object[] { month, year, departmentID, employeeID, txtKeyword.Text.Trim() });


                grdData.DataSource = ds.Tables[0];

                bandTitle.Caption = $"BẢNG CHẤM CÔNG THÁNG {month}";
                //bandTotalWorkDay.Caption = $"Công tiêu chuẩn = {TextUtils.ToInt(ds.Tables[2].Rows[0]["TotalWorkDay"])}";

                GridBandCollection bandChild = bandTitle.Children;
                DataTable dtAllDate = ds.Tables[1];
                if (dtAllDate.Rows.Count <= 0) return;

                foreach (GridBand item in bandChild)
                {
                    item.OptionsBand.AllowMove = false;
                    item.OptionsBand.AllowSize = false;
                    item.OptionsBand.FixedWidth = true;
                    item.MinWidth = item.Width;

                    GridBandColumnCollection columns = item.Columns;
                    if (columns.Count <= 0)
                    {
                        continue;
                    }

                    string fieldName = columns[0].FieldName;
                    string caption = TextUtils.ToString(dtAllDate.Rows[0][fieldName]);
                    if (!caption.Contains(";"))
                    {
                        continue;
                    }

                    item.Caption = caption.Substring(0, caption.IndexOf(";"));

                    int status = TextUtils.ToInt(caption.Substring(caption.IndexOf(";") + 1));
                    if (status == 0)
                    {
                        item.AppearanceHeader.BackColor = Color.FromName("Tan");
                        columns[0].AppearanceHeader.BackColor = Color.FromName("Tan");
                    }
                    else
                    {
                        item.AppearanceHeader.Reset();
                        columns[0].AppearanceHeader.Reset();
                    }
                }

                loadDetail(month,year,departmentID,employeeID);
            }
        }


        void loadDetail(int month, int year,int departmentID,int employeeID)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeChamCongDetail", "A"
                                    , new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID", "@KeyWord" }
                                    , new object[] { month, year, departmentID, employeeID, txtKeyword.Text.Trim() });


            grdDetail.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
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

        private void btnUpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = grvData.FocusedRowHandle;

                if (idMaster <= 0)
                {
                    return;
                }

                int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
                int month = TextUtils.ToInt(txtMonth.Value);
                int year = TextUtils.ToInt(txtYear.Value);

                string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
                if (employeeChamCong.IsApproved)
                {
                    MessageBox.Show($"Bảng công của nhân viên [{fullName}] đã được duyệt.\nVui lòng huỷ duyệt trước", "Thông báo");
                    return;
                }

                DialogResult dialog = MessageBox.Show($"Bạn có chắc muộn cập nhật bảng công của nhân viên [{fullName}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    TextUtils.ExcuteProcedure("spInsertIntoEmployeeChamCongDetail"
                                                , new string[] { "@MasterID", "@Month", "@Year", "@EmployeeID", "@LoginName" }
                                                , new object[] { idMaster, month, year, employeeID, Global.AppCodeName });

                    loadData();
                }


                grvData.FocusedRowHandle = rowHandle;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {

        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"BaoCaoChamCong_T{txtMonth.Text}_{txtYear.Value}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoChamCong_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";
          
                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                printableComponentLink2.Component = grdDetail;

                grvDetail.OptionsPrint.AutoWidth = false;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    compositeLink.Links.Add(printableComponentLink2);

                    compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    //optionsEx.AllowSortingAndFiltering = DefaultBoolean.True;

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

        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            e.SheetName = e.Index == 0 ? $"CÔNG" : "CHI TIẾT";
        }

        private void OptionsEx_CustomizeCell(DevExpress.Export.CustomizeCellEventArgs e)
        {
            try
            {
                e.Formatting = new XlFormattingObject
                {
                    Font = new XlCellFont
                    {
                        Size = 12,
                        Name = "Times New Roman"
                    }
                };


            }
            finally
            {
                e.Handled = true;
            }
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();

            var exp1 = new Expression("_Year", txtYear.Value);
            var exp2 = new Expression("[_Month]", txtMonth.Value);

            EmployeeChamCongMasterModel masterModel = SQLHelper<EmployeeChamCongMasterModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            if (masterModel != null)
            {
                idMaster = masterModel.ID;
                btnUpdateData.Enabled = btnUpdateAll.Enabled = !masterModel.IsApproved;
            }
            else
            {
                idMaster = 0;
            }
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn cập nhật lại bảng chấm công tháng {txtMonth.Value}/{txtYear.Value} của tất cả nhân viên không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    EmployeeChamCongDetailBO.Instance.DeleteByAttribute("MasterID", idMaster);
                    TextUtils.ExcuteProcedure("spInsertIntoEmployeeChamCongDetail"
                                                        , new string[] { "@MasterID", "@Month", "@Year", "@EmployeeID", "@LoginName" }
                                                        , new object[] { idMaster, txtMonth.Value, txtYear.Value, 0, Global.AppCodeName });

                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
            }

        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();

            var exp1 = new Expression("_Year", txtYear.Value);
            var exp2 = new Expression("[_Month]", txtMonth.Value);

            EmployeeChamCongMasterModel masterModel = SQLHelper<EmployeeChamCongMasterModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            if (masterModel != null)
            {
                idMaster = masterModel.ID;
                btnUpdateData.Enabled = btnUpdateAll.Enabled = !masterModel.IsApproved;
            }
            else
            {
                idMaster = 0;
            }
        }

        private void grvDetail_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
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
    }
}
