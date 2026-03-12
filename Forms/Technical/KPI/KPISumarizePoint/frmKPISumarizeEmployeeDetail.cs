using BaseBusiness.DTO;
using BMS.Model;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraSpreadsheet;
using ExcelDataReader;
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
    public partial class frmKPISumarizeEmployeeDetail : _Forms
    {
        //private static string DefaultPath = Global.kpiFolderTechnical;
        public frmKPISumarizeEmployeeDetail()
        {
            InitializeComponent();
        }
        private void frmKPISumarizeEmployeeDetail_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtQuarter.Value = (int)((DateTime.Now.Month + 2) / 3);
            LoadUserTeam();
            LoadPosition();
            LoadEmployee();
        }
        private void LoadUserTeam()
        {
            //2 Là phòng kỹ thuật
            List<UserTeamModel> lst = SQLHelper<UserTeamModel>.FindByAttribute("DepartmentID", 2);

            cboUserTeam.Properties.DataSource = lst;
            cboUserTeam.Properties.ValueMember = "ID";
            cboUserTeam.Properties.DisplayMember = "Name";
            cboUserTeam.EditValue = Global.UserTeamID;
        }
        private void cboUserTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }
        private void LoadPosition()
        {
            List<KPIPositionModel> lst = SQLHelper<KPIPositionModel>.FindByAttribute("IsDeleted", 0);
            cboPosition.Properties.DataSource = lst;
            cboPosition.Properties.ValueMember = "ID";
            cboPosition.Properties.DisplayMember = "PositionCode";
        }
        private void cboPosition_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployee();
        }
        private void LoadEmployee()
        {
            int positionID = TextUtils.ToInt(cboPosition.EditValue);
            int userTeamID = TextUtils.ToInt(cboUserTeam.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeByKPIPosition_UserTeam", "LMKtable",
                                                    new string[] { "@UserTeamID", "@PositionID" },
                                                    new object[] { userTeamID, positionID });
            grdData.DataSource = dt;
        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";
            ofd.Title = "Chọn file Excel";
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                    {
                        var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        var sw = new Stopwatch();
                        sw.Start();

                        spreadsheetControl1.LoadDocument(stream);

                        var openTiming = sw.ElapsedMilliseconds;
                        sw.Stop();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private bool CheckValiDate()
        {
            int userTeamID = TextUtils.ToInt(cboUserTeam.EditValue);
            int positionID = TextUtils.ToInt(cboPosition.EditValue);
            int year = TextUtils.ToInt(txtYear.Value);
            int quarter = TextUtils.ToInt(txtQuarter.Value);
            string file = btnBrowse.Text;

            if (userTeamID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Team trước khi tạo file!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (positionID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Vị trí trước khi tạo file!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (year <= 0)
            {
                MessageBox.Show("Vui lòng chọn Năm trước khi tạo file!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (quarter <= 0)
            {
                MessageBox.Show("Vui lòng chọn Quý trước khi tạo file!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (grvData.RowCount <= 0)
            {
                MessageBox.Show("Vui lòng xem lại Bảng nhân viên trước khi tạo file!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(file))
            {
                MessageBox.Show("Vui lòng chọn File trước khi tạo file!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void SaveDocument(EmployeeModel emp)
        {
            string year = txtYear.Value.ToString();
            string quarter = txtQuarter.Value.ToString();
            string userTeamName = cboUserTeam.Text.Trim();

            IWorkbook workbook = spreadsheetControl1.Document;
            Worksheet worksheet = workbook.Worksheets[0];

            int positionID = TextUtils.ToInt(cboPosition.EditValue);
            List<KPISumarizeDTO> lstResult = SQLHelper<KPISumarizeDTO>.ProcedureToList("spGetKPISumarize",
                                                                        new string[] { "@EmployeeID", "@Year", "@Quarter", "@Position" },
                                                                        new object[] { emp.ID, TextUtils.ToInt(year), TextUtils.ToInt(quarter), positionID });

            ProjectTypeModel prjTypeModel = SQLHelper<ProjectTypeModel>.ProcedureToList("spFindEmployeeTeam",
                                                                        new string[] { "@UserID" },
                                                                        new object[] { emp.UserID }).FirstOrDefault() ?? new ProjectTypeModel();

            worksheet.Cells["C1"].Value = emp.FullName;
            worksheet.Cells["C2"].Value = prjTypeModel.ProjectTypeName;


            int lastRowWithData = worksheet.GetDataRange().BottomRowIndex;
            for (int row = 4; row <= lastRowWithData; row++) // 4: E, 5: F, 6: G, 7: H
            {
                foreach (KPISumarizeDTO item in lstResult)
                {
                    string codeEvaluation = TextUtils.ToString(worksheet.Cells[row, 3].Value).Trim().ToUpper();
                    bool isMatch = item.EvaluationCode.ToUpper() == codeEvaluation;
                    bool isSpecial = codeEvaluation == "KPINQ" || codeEvaluation == "KPINL" || codeEvaluation == "TIPTRICK";
                    bool isTeam = codeEvaluation.StartsWith("TEAM");
                    if (isMatch)
                    {
                        if (isSpecial)
                        {
                            worksheet.Cells[row, 7].Value = item.FirstMonth;
                        }
                        else if (isTeam)
                        {
                            worksheet.Cells[row, 7].Value = item.FirstMonth;
                        }
                        else
                        {
                            worksheet.Cells[row, 4].Value = item.FirstMonth;
                            worksheet.Cells[row, 5].Value = item.SecondMonth;
                            worksheet.Cells[row, 6].Value = item.ThirdMonth;
                        }
                    }

                }

            }

            string filePath = $"{Global.kpiFolderTechnical}\\Năm {year}\\Quý {quarter}\\{userTeamName}\\{year}-Q{quarter}-{emp.Code.ToUpper()}-{emp.FullName.ToUpper()}.xlsx";
            string directoryPath = Path.GetDirectoryName(filePath);


            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                spreadsheetControl1.SaveDocument(filePath);
            }
            catch (Exception err)
            {

                MessageBox.Show($"Đã xảy ra lỗi khi lưu file: {err.Message}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValiDate())
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                {
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        int empID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                        EmployeeModel empModel = SQLHelper<EmployeeModel>.FindByID(empID);
                        if (empModel.ID > 0)
                        {
                            SaveDocument(empModel);
                        }
                    }
                }
                string year = txtYear.Value.ToString();
                string quarter = txtQuarter.Value.ToString();
                string userTeamName = cboUserTeam.Text.Trim();
                Process.Start($"{Global.kpiFolderTechnical}\\Năm {year}\\Quý {quarter}\\{userTeamName}");
            }

        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
