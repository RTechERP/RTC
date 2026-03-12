using BMS;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Utils;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Diagnostics;

namespace Forms.Technical.KPI
{
    public partial class frmKPIEvaluationFactorScoringSummary: _Forms
    {
        public int departmentID = 0;
        public int typeID = 0;
        public frmKPIEvaluationFactorScoringSummary()
        {
            InitializeComponent();
        }

        private void frmKPIEvaluationFactorScoringSummary_Load(object sender, EventArgs e)
        {
            LoadDepartMent();
            LoadKPISession();
            LoadUserTeam();
            LoadData();
            grvMaster.ExpandAllGroups();
        }
        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            //lst.Insert(0, new DepartmentModel()
            //{
            //    ID = 0,
            //    Name = "--Tất cả--"
            //});
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";

            //cboDepartMent.EditValue = Global.DepartmentID;
            cboDepartMent.EditValue = departmentID;
        }
        private void LoadUserTeam()
        {
            int departMent = TextUtils.ToInt(cboDepartMent.EditValue);
            //List<KPIEmployeeTeamModel> lst = SQLHelper<KPIEmployeeTeamModel>.FindByAttribute("DepartmentID", departMent).Where(p => typeID == 3 || Global.IsAdmin || p.LeaderID == Global.EmployeeID).ToList();

            //lst = lst.Select(c => { c.Name = c.Name.ToUpper(); return c; }).ToList();

            //cboUserTeam.Properties.DataSource = lst;
            //cboUserTeam.Properties.ValueMember = "ID";
            //cboUserTeam.Properties.DisplayMember = "Name";
            //cboUserTeam.EditValue = Global.UserTeamID;

            //int departMent = TextUtils.ToInt(cboDepartMent.EditValue);

            //int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
            //KPISessionModel kpiSession = SQLHelper<KPISessionModel>.FindByID(kpiSessionId);

            var kpiSession = (KPISessionModel)cboKPISession.GetSelectedDataRow() ?? new KPISessionModel();
            //int year = TextUtils.ToInt(gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "YearEvaluation")); //NTA B - update 28/08/25
            //int quarter = TextUtils.ToInt(gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "QuarterEvaluation")); //NTA B - update 28/08/25

            DataTable dt = TextUtils.LoadDataFromSP("spGetALLKPIEmployeeTeam", "A",
                                                        new string[] { "@YearValue", "@QuarterValue", "@DepartmentID" },
                                                        new object[] { kpiSession.YearEvaluation, kpiSession.QuarterEvaluation, departMent });
            var filteredRows = dt.AsEnumerable().Where(r => typeID == 3 || Global.IsAdmin || TextUtils.ToInt(r["LeaderID"]) == Global.EmployeeID).CopyToDataTable();
            cboUserTeam.Properties.DataSource = filteredRows;
            cboUserTeam.Properties.ValueMember = "ID";
            cboUserTeam.Properties.DisplayMember = "Name";
            cboUserTeam.EditValue = Global.UserTeamID;
        }
        private void LoadKPISession()
        {
            int year = DateTime.Now.Year;
            int quarter = (int)((DateTime.Now.Month + 2) / 3);
            departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            var exp1 = new Expression(KPISessionModel_Enum.DepartmentID, departmentID);
            var exp2 = new Expression(KPISessionModel_Enum.IsDeleted, 0);
            //List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.ID).ToList();
            List<KPISessionModel> lst = SQLHelper<KPISessionModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(p => p.QuarterEvaluation).ToList();
            cboKPISession.Properties.DataSource = lst;
            cboKPISession.Properties.DisplayMember = "Code";
            cboKPISession.Properties.ValueMember = "ID";
            KPISessionModel currentSession = lst.FirstOrDefault(p => p.YearEvaluation == year && p.QuarterEvaluation == quarter) ?? new KPISessionModel();
            cboKPISession.EditValue = currentSession.ID;
            //LoadKpiExam();
        }

        private void LoadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartMent.EditValue);
            int kpiSessionId = TextUtils.ToInt(cboKPISession.EditValue);
            int userTeamId = TextUtils.ToInt(cboUserTeam.EditValue);
            string filterText = txtKeywords.Text.Trim().ToLower();

            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIEvaluationFactorScoringSummary", "A",
                                                        new string[] { "@DepartmentID", "@KPIPositionID", "@UserTeamID", "@Keywords" },
                                                        new object[] { departmentID, kpiSessionId, userTeamId, filterText });
            grdMaster.DataSource = dt;
            grvMaster.ExpandAllGroups();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopKPI_{DateTime.Now.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //string filepath = Path.Combine(f.SelectedPath, $"");
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdMaster;

                    try
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

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
        }

        private void cboDepartMent_EditValueChanged(object sender, EventArgs e)
        {
            LoadUserTeam();
        }

        private void grvMaster_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == colExamName.FieldName)
            {
                string emp1 = grvMaster.GetRowCellValue(e.RowHandle1, colEmployeeCode.FieldName)?.ToString();
                string emp2 = grvMaster.GetRowCellValue(e.RowHandle2, colEmployeeCode.FieldName)?.ToString();

                string exam1 = grvMaster.GetRowCellValue(e.RowHandle1, colExamName.FieldName)?.ToString();
                string exam2 = grvMaster.GetRowCellValue(e.RowHandle2, colExamName.FieldName)?.ToString();

                if (emp1 == emp2 && exam1 == exam2)
                {
                    e.Merge = true;
                    e.Handled = true;
                }
                else
                {
                    e.Merge = false;
                    e.Handled = true;
                }
            }
        }

        private void cboKPISession_EditValueChanged(object sender, EventArgs e)
        {
            LoadUserTeam();
        }
    }
}
