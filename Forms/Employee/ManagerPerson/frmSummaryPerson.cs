using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
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
    public partial class frmSummaryPerson : _Forms
    {
        public frmSummaryPerson()
        {
            InitializeComponent();
        }

        private void frmSummaryPerson_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);

            LoadDepartment();
            LoadUserTeam();
            LoadEmployee();
            LoadData();

        }

        void LoadData()
        {
            //LinhTN update 23/10/2024 - START
            gbError.Columns.Clear();
            var lst = SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0).OrderBy(p => p.STT).ToList();

            //var model = new KPIErrorTypeModel();
            //model.STT = 0;
            //model.Name = "Tổng lỗi";
            //lst.Add(model);

            foreach (var item in lst)
            {
                BandedGridColumn col = new BandedGridColumn();
                col.Caption = item.Name;
                col.Visible = true;
                col.FieldName = $"KE{item.ID}";
                col.Width = 115;
                col.OptionsColumn.FixedWidth = true;
                col.ColumnEdit = repositoryItemMemoEdit1;
                col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                col.SummaryItem.FieldName = $"KE{item.ID}";
                col.BestFit();
                gbError.Columns.Add(col);
            }

            //BandedGridColumn colTotalEr = new BandedGridColumn();
            //colTotalEr.Caption = item.Name;
            //colTotalEr.Visible = true;
            //colTotalEr.FieldName = $"KE{item.ID}";
            //colTotalEr.Width = 115;
            //colTotalEr.OptionsColumn.FixedWidth = true;
            //colTotalEr.ColumnEdit = repositoryItemMemoEdit1;
            //colTotalEr.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //colTotalEr.SummaryItem.FieldName = $"KE{item.ID}";
            //gbError.Columns.Add(col);

            //LinhTN update 23/10/2024 - END
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int userTeamID = TextUtils.ToInt(cboUserTeam.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            string keyword = txtKeyword.Text.Trim();

            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryPerson", "A",
                                                    new string[] { "@DateStart", "@DateEnd", "@DepartmentID", "@UserTeamID", "@EmployeeID", "@Keyword" },
                                                    new object[] { dateStart, dateEnd, departmentID, userTeamID, employeeID, keyword });

            grdData.DataSource = dt;

            grvData.BestFitColumns();
        }


        void LoadDepartment()
        {
            var list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            cboDepartment.EditValue = Global.DepartmentID;
        }


        void LoadUserTeam()
        {

            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetTreeUserTeamData", "A",
                                                        new string[] { "@DepartmentID" },
                                                        new object[] { departmentID });

            cboUserTeam.Properties.ValueMember = "ID";
            cboUserTeam.Properties.DisplayMember = "Name";
            cboUserTeam.Properties.DataSource = dt;

            UserTeamModel team = SQLHelper<UserTeamModel>.FindByAttribute("LeaderID", Global.EmployeeID).FirstOrDefault() ?? new UserTeamModel();
            cboUserTeam.EditValue = team.ID;
        }


        void LoadEmployee()
        {
            int userTeamID = TextUtils.ToInt(cboUserTeam.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeByTeamID", "A",
                                                        new string[] { "@TeamID" },
                                                        new object[] { userTeamID });

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.EditValue = Global.EmployeeID;
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadUserTeam();
            LoadEmployee();
        }

        private void cboUserTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopCaNhan_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
