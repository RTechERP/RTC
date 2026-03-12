using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;

namespace BMS
{
    public partial class frmSummarizeWork : _Forms
    {
        private int _departmentID = 2;
        public frmSummarizeWork(int departmentID = 2)
        {
            InitializeComponent();
            _departmentID = departmentID;
        }

        private void frmSummarizeWork_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            int daysUntilMonday = (int)DayOfWeek.Monday - (int)today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(daysUntilMonday);
            dtpStartDate.Value = startOfWeek;
            dtpEndDate.Value = startOfWeek.AddDays(6);

            LoadDepartment();
            LoadTeam();
            LoadEmployee();
            LoadData();

            
        }
        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll(); //("SELECT * FROM Department");
            //DepartmentModel defaultOption = SQLHelper<DepartmentModel>.SqlToModel("SELECT * FROM Department WHERE Code = \'KYTHUAT\'");
            //DepartmentModel defaultOption = SQLHelper<DepartmentModel>.FindByAttribute("Code", "KYTHUAT");// ("SELECT * FROM Department WHERE Code = \'KYTHUAT\'");
            cboDepartment.Properties.DataSource = list;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            

            //cboDepartment.EditValue = defaultOption.ID;
            cboDepartment.EditValue = _departmentID;
        }


        void LoadTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            //List<UserTeamModel> list = SQLHelper<UserTeamModel>.SqlToList($"SELECT * FROM dbo.UserTeam WHERE DepartmentID = {departmentID}");
            List<UserTeamModel> list = SQLHelper<UserTeamModel>.FindByAttribute("DepartmentID", departmentID);// ($"SELECT * FROM dbo.UserTeam WHERE DepartmentID = {departmentID}");

            cboTeam.Properties.DataSource = list;
            cboTeam.Properties.DisplayMember = "Name";
            cboTeam.Properties.ValueMember = "ID";
        }

        void LoadEmployee()
        {
            int teamId = TextUtils.ToInt(cboTeam.EditValue);
            //List<EmployeeModel> list = SQLHelper<EmployeeModel>.SqlToList($"SELECT ID,UserID,Code,FullName FROM dbo.Employee WHERE TeamID = {TeamID}");
            //List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindByAttribute();// ($"SELECT ID,UserID,Code,FullName FROM dbo.Employee WHERE TeamID = {TeamID}");

            DataTable list = TextUtils.LoadDataFromSP("spGetUserTeamLink_New", "A", new string[] { "@UserTeamID" }, new object[] { teamId });

            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "UserID";
        }
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadTeam();
            LoadData();
        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int teamID = TextUtils.ToInt(cboTeam.EditValue);
            int userID = TextUtils.ToInt(cboEmployee.EditValue);

            DateTime ds = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetSummarizeWork",
                new string[] { "@DateStart", "@DateEnd", "@DepartmentID", "@TeamID", "@UserID", "@Keyword" },
                new object[] { ds, de, departmentID, teamID, userID, txtFilterText.Text.Trim() });

            grdData.DataSource = dt;
        }
        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Status")
            {
                //int itemLate = TextUtils.ToInt(e.CellValue);
                int itemLate = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "ItemLate"));

                if (itemLate == 1)
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.Appearance.ForeColor = Color.Black;
                }
                else if (itemLate == 2)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmProp frmProp = new FrmProp();
            frmProp.propertyGridControl1.SelectedObject = grvData;
            frmProp.Show();
        }
    }
}