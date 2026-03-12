using BMS.Model;
using DevExpress.Utils;
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
    public partial class frmKPISumarizeEmployee : _Forms
    {
        //private static string defaultPath = @"D:\RTC_KPI";
        public int departmentID = 0;
        public string deName;
        public frmKPISumarizeEmployee()
        {
            InitializeComponent();
        }
        private void frmKPISumarizeEmployee_Load(object sender, EventArgs e)
        {
            this.Text += " - " + deName;
            txtYear.Value = DateTime.Now.Year;
            txtQuarter.Value = (int)((DateTime.Now.Month + 2) / 3);
            LoadEmployee();
        }
        private void LoadEmployee()
        {
            int year = (int)txtYear.Value;
            int quarter = (int)txtQuarter.Value;
            DataTable dt = TextUtils.LoadDataFromSP("spGetKPIEmployeeTechnical", "LMKTable", 
                                                    new string[] { "@Year", "@Quarter", "@Department" },
                                                    new object[] { year, quarter, departmentID});
            grdData.DataSource = dt;
        }
        private void btnEvaluated_Click(object sender, EventArgs e)
        {
            frmKPISumarizeEmployeeDetail frm = new frmKPISumarizeEmployeeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnEvaluationKPI_Click(object sender, EventArgs e)
        {
            frmKPITBPEvaluation frm = new frmKPITBPEvaluation();
            frm.ShowDialog();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            btnReload_Click(null,null);
        }

        private void txtQuarter_ValueChanged(object sender, EventArgs e)
        {
            btnReload_Click(null,null);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string empCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeCode));
            string empName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            string empTeam = "";
            string year = txtYear.Value.ToString().Trim();
            string quarter = txtQuarter.Value.ToString().Trim();

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
            {
                string filePath = "";
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    if(empCode == TextUtils.ToString(grvData.GetRowCellValue(i, colEmployeeCode)))
                    {
                        empTeam = TextUtils.ToString(grvData.GetRowCellValue(i, colUserTeam));
                        filePath = $"{Global.kpiFolderTechnical}\\Năm {year}\\Quý {quarter}\\{empTeam}\\{year}-Q{quarter}-{empCode.ToUpper()}-{empName.ToUpper()}.xlsx";

                        if (System.IO.File.Exists(filePath)) spreadsheetControl1.LoadDocument(filePath);
                        else continue; 
                    }
                }
                if(!System.IO.File.Exists(filePath)) MessageBox.Show("File không tồn tại!\n Vui lòng kiểm tra lại!");
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            bool isEvaluated = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 1;
            
            bool isGroup = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colEmployeeID)) <= 0;
            if (!isGroup)
            {
                if (isEvaluated)  e.Appearance.BackColor = Color.LightGreen;
                else  e.Appearance.BackColor = Color.LightYellow;
            }
            else e.Appearance.BackColor = Color.LightGray;

            e.HighPriority = true;
        }
    }
}
