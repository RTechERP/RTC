using BMS.Model;
using DevExpress.XtraGrid;
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
    public partial class frmProjectItemInWeb : _Forms
    {
        public frmProjectItemInWeb()
        {
            InitializeComponent();
        }

        private void frmProjectItemInWeb_Load(object sender, EventArgs e)
        {
            //dtpFromDate.Value = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now;
            cboStatus.SelectedIndex = 0;
            LoadProject();
            LoadTeam();
            LoadEmployee();

            loadData();
        }
        void loadData()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int teamID = TextUtils.ToInt(cboTeam.EditValue);
            int userID = TextUtils.ToInt(cboEmployee.EditValue);
            int status = cboStatus.SelectedIndex - 1;

            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectItemInWeb",
                new string[] { "@DateStart", "@DateEnd", "@Keyword", "@Status", "@ProjectID", "@TeamID", "@UserID" },
                new object[] { ds, de, txtFilterText.Text.Trim(), status, projectID, teamID, userID });
            grdData.DataSource = dt;


            var summarys = grvData.Columns[colIsLateActualText.FieldName].Summary;
            if (summarys.Count > 0)
            {
                grvData.Columns[colIsLateActualText.FieldName].Summary.Clear();
            }

            var dataOverPlan = dt.Select("ItemLateActual = 1");
            var dataFail = dt.Select("IsLateActual = 2");
            var dataLate = dt.Select("IsLateActual = 3");

            grvData.Columns[colIsLateActualText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsLateActualText.FieldName, $"Chậm = {dataOverPlan.Length.ToString()}"));
            grvData.Columns[colIsLateActualText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsLateActualText.FieldName, $"Fail = {dataFail.Length.ToString()}"));
            grvData.Columns[colIsLateActualText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colIsLateActualText.FieldName, $"Muộn = {dataLate.Length.ToString()}"));
        }

        void LoadTeam()
        {
            //List<UserTeamModel> list = SQLHelper<UserTeamModel>.SqlToList($"SELECT * FROM dbo.UserTeam");

            //cboTeam.Properties.DataSource = list;
            //cboTeam.Properties.DisplayMember = "Name";
            //cboTeam.Properties.ValueMember = "ID";


            //int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int departmentID = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetTreeUserTeamData", "A",
                                                        new string[] { "@DepartmentID" },
                                                        new object[] { departmentID });

            cboTeam.Properties.ValueMember = "ID";
            cboTeam.Properties.DisplayMember = "Name";
            cboTeam.Properties.DataSource = dt;

            UserTeamModel team = SQLHelper<UserTeamModel>.FindByAttribute("LeaderID", Global.EmployeeID).FirstOrDefault() ?? new UserTeamModel();
            cboTeam.EditValue = team.ID;

        }

        void LoadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.SqlToList($"SELECT ID,UserID,Code,FullName FROM dbo.Employee");

            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "UserID";

            cboEmployee.EditValue = Global.UserID;
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.SqlToList($"SELECT ID,ProjectCode,ProjectName FROM dbo.Project");

            cboProject.Properties.DataSource = list;
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                //optionsEx.CustomizeCell += OptionsEx_CustomizeCell;
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/HangMucCongViec_{dtpFromDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
                grvData.ClearSelection();
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //e.Appearance.BackColor = Color.White;
            //e.Appearance.ForeColor = Color.Black;

            if (e.RowHandle >= 0)
            {
                int itemLate = 0;


                DateTime? planStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanStartDate));
                DateTime? planEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colPlanEndDate));
                DateTime? actualStart = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualStartDate));
                DateTime? actualEnd = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colActualEndDate));

                //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
                //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed
                if (actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualStart.Value.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                    else if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }

                }

                //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
                //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
                if (actualStart.HasValue && actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((actualEnd.Value.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 1;
                    }
                }

                //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
                //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed
                if (!actualStart.HasValue && !actualEnd.HasValue && planEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planEnd.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                //Nếu chỉ có ngày bắt đầu dự kiến
                if (planStart.HasValue && !planEnd.HasValue && !actualStart.HasValue && !actualEnd.HasValue)
                {
                    if ((DateTime.Now.Date - planStart.Value.Date).TotalDays > 0)
                    {
                        itemLate = 2;
                    }
                }

                if (itemLate == 1)
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.HighPriority = true;
                }
                else if (itemLate == 2)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
                else
                {
                    int totalDayExpridSoon = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "TotalDayExpridSoon"));
                    if (totalDayExpridSoon <= 3 && !actualEnd.HasValue)
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.HighPriority = true;
                    }
                }

            }
        }
    }
}
