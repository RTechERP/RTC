using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmChooseEmployee : _Forms
    {
        public bool IsChooseMulti = true;
        public List<int> LstID = new List<int>();
        public int UserTeamID = 0;

        public List<int> employeeIds = new List<int>();

        public frmChooseEmployee()
        {
            InitializeComponent();
        }

        private void frmChooseEmployee_Load(object sender, EventArgs e)
        {
            loadDepartment();
            loadEmployee();

            ckbCode.CheckedChanged += CkbCode_CheckedChanged;
        }

        private void CkbCode_CheckedChanged(object sender, EventArgs e)
        {
            //if (e.Column != colIsSelection) return;
            grvDetail.CloseEditor();
            bool isSelection = TextUtils.ToBoolean(grvDetail.GetFocusedRowCellValue(colIsSelection));
            int ID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colID));
            int employeeID = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colEmployeeID));
            if (isSelection)
            {
                if (!LstID.Contains(ID)) LstID.Add(ID);
                if (!employeeIds.Contains(employeeID)) employeeIds.Add(employeeID);
            }
            else
            {
                LstID.Remove(ID);
                employeeIds.Remove(employeeID);
            }
        }

        void loadDepartment()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Department WITH(NOLOCK)");
            cboDepartment.Properties.DataSource = dt;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }

        void loadEmployee()
        {
            int dID = TextUtils.ToInt(cboDepartment.EditValue);
            int status = chkAll.Checked ? -1 : 0;

            DataTable dt = TextUtils.GetDataTableFromSP("spGetEmployeeByDepartmentID_New",
                new string[] { "@DepartmentID", "@UserTeam", "@Status" },
                new object[] { dID, UserTeamID, status });
            dt.Columns.Add("IsSelect", typeof(bool));

            grdDetail.DataSource = dt;
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadEmployee();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }

        private void grvDetail_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        private void grvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                grvDetail.SetRowCellValue(i, colIsSelection, true);

                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colID));
                if (!LstID.Contains(id))
                {
                    LstID.Add(id);
                }
            }
        }

        private void bntCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                grvDetail.SetRowCellValue(i, colIsSelection, false);

                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colID));
                LstID.Remove(id);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            loadEmployee();
        }

        private void grvDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            int status = TextUtils.ToInt(grvDetail.GetRowCellValue(e.RowHandle, colStatus));
            if (status == 1)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }
        }
    }
}
