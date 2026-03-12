using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmKPITBPEvaluation : _Forms
    {
        //private static string defaultPath = @"D:\RTC_KPI";
        public frmKPITBPEvaluation()
        {
            InitializeComponent();
        }

        private void frmKPITBPEvaluation_Load(object sender, EventArgs e)
        {
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string empCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string empName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            string empTeam = TextUtils.ToString(cboUserTeam.Text);
            string year = txtYear.Value.ToString().Trim();
            string quarter = txtQuarter.Value.ToString().Trim();

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
            {
                string filePath = $"{Global.kpiFolderTechnical}\\Năm {year}\\Quý {quarter}\\{empTeam}\\{year}-Q{quarter}-{empCode.ToUpper()}-{empName.ToUpper()}.xlsx";
                if (System.IO.File.Exists(filePath))
                {
                    spreadsheetControl1.LoadDocument(filePath);
                }
                else
                {
                    MessageBox.Show("File không tồn tại!\n Vui lòng kiểm tra lại!");
                }
            }
        }
        private void SaveData()
        {
            int empID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int year = (int)txtYear.Value;
            int quarter = (int)txtQuarter.Value;
            Expression ex1 = new Expression("YearEvalution", year);
            Expression ex2 = new Expression("QuarterEvalution", quarter);
            Expression ex3 = new Expression("EmployeeID", empID);
            KPISumarizeModel model = SQLHelper<KPISumarizeModel>.FindByExpression(ex1.And(ex2).And(ex3)).FirstOrDefault() ?? new KPISumarizeModel();

            Worksheet worksheet = spreadsheetControl1.Document.Worksheets[0];

            model.TimeHours = TextUtils.ToInt(worksheet.Cells["I5"].Value.NumericValue);
            model.FiveSRegulatedProcedures = TextUtils.ToInt(worksheet.Cells["I7"].Value.NumericValue) + TextUtils.ToInt(worksheet.Cells["I9"].Value.NumericValue);
            model.PrepareGoodsReport = TextUtils.ToInt(worksheet.Cells["I10"].Value.NumericValue) + TextUtils.ToInt(worksheet.Cells["I11"].Value.NumericValue);
            model.AttitudeTowardsCustomers = TextUtils.ToInt(worksheet.Cells["I13"].Value.NumericValue);

            model.LossEquipment = 0; // TextUtils.ToInt(worksheet.Cells["A1"].Value);
            model.SkillPoints = TextUtils.ToDecimal(worksheet.Cells["I20"].Value.NumericValue);
            model.PLCExpertisePoints = TextUtils.ToDecimal(worksheet.Cells["I21"].Value.NumericValue);
            model.VisionExpertisePoints = TextUtils.ToDecimal(worksheet.Cells["I22"].Value.NumericValue);
            model.SoftwareExpertisePoints = TextUtils.ToDecimal(worksheet.Cells["I23"].Value.NumericValue);

            if (model.ID > 0) SQLHelper<KPISumarizeModel>.Update(model);
            else
            {
                model.YearEvalution = year;
                model.QuarterEvalution = quarter;
                model.EmployeeID = empID;
                model.ID = SQLHelper<KPISumarizeModel>.Insert(model).ID;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string empCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string empName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            string empTeam = TextUtils.ToString(cboUserTeam.Text);
            string year = txtYear.Value.ToString().Trim();
            string quarter = txtQuarter.Value.ToString().Trim();
            if (MessageBox.Show($"Bạn có chắc muốn lưu file {year}-Q{quarter}-{empCode.ToUpper()}-{empName.ToUpper()}.xlsx không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                {
                    try
                    {
                        SaveData();
                        string filePath = $"{Global.kpiFolderTechnical}\\Năm {year}\\Quý {quarter}\\{empTeam}\\{year}-Q{quarter}-{empCode.ToUpper()}-{empName.ToUpper()}.xlsx";
                        spreadsheetControl1.SaveDocument(filePath);
                    }
                    catch (Exception err)
                    {

                        MessageBox.Show($"Đã xảy ra lỗi khi lưu file: {err.Message}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
        }
    }
}
