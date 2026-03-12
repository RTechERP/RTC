using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmGeneralWorkPlan : _Forms
    {
        List<GridColumn> listCol = new List<GridColumn>();
        int Type = 0;
        public frmGeneralWorkPlan()
        {
            InitializeComponent();
        }

        private void frmGeneralWorkPlan_Load(object sender, EventArgs e)
        {
            dtpStartTime.Value = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);
            dtpEndTime.Value = dtpStartTime.Value.AddDays(+6);
            //rdGeneral.Checked = true;
            LoadUser();
            LoadTeam();
            LoadDepartment();
            LoadData();
        }
        private void LoadData()
        {
            DateTime dateTimeS = new DateTime(dtpStartTime.Value.Year, dtpStartTime.Value.Month, dtpStartTime.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndTime.Value.Year, dtpEndTime.Value.Month, dtpEndTime.Value.Day, 23, 59, 59);
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetWorkPlanDetail1",
                                new string[] { "@DateStart", "@DateEnd", "@FilterText", "@DepartmentID", "@TeamID", "@UserID", "@Type" },
                                new object[] { dateTimeS, dateTimeE, txtFilter.Text.Trim(), TextUtils.ToInt(cbDepartment.EditValue),TextUtils.ToInt(cbTeam.EditValue),TextUtils.ToInt(cbUser.EditValue), Type });

            DataTable dtColDate = dataSet.Tables[0];
            DataTable dtData = dataSet.Tables[1];

            if (listCol.Count > 0)
            {
                //grvData.Columns.Clear();
                foreach (GridColumn item in listCol)
                {
                    grvData.Columns.Remove(item);
                }
                listCol.Clear();
            }

            colHidden.Visible = false;

            for (int i = 0; i < dtColDate.Rows.Count; i++)
            {
                GridColumn col = grvData.Columns.Add();
                DateTime? dateCol = TextUtils.ToDate4(dtColDate.Rows[i]["AllDates"]);

                col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Options.UseForeColor = true;
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.Caption = dateCol.Value.ToString("dd/MM/yyyy");
                col.FieldName = dateCol.Value.ToString("yyyy-MM-dd");
                col.Name = "col" + dateCol.Value.ToString("ddMMyy");
                col.OptionsColumn.AllowEdit = false;
                col.Visible = true;
                col.Width = 200;
                col.MinWidth = 200;
                col.Width = 200;
                col.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                col.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                col.OptionsColumn.ReadOnly = true;
                col.ColumnEdit = repositoryItemMemoEdit1;
                listCol.Add(col);
            }

            grdData.DataSource = dtData;
        }
        void LoadDepartment()
        {
            DataTable dtDepartment = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }
        void LoadTeam()
        {
            DataTable dtTeam = TextUtils.Select($"SELECT ID,Name FROM UserTeam");
            cbTeam.Properties.DataSource = dtTeam;
            cbTeam.Properties.DisplayMember = "Name";
            cbTeam.Properties.ValueMember = "ID";
        }
        void LoadUser()
        {
            DataTable dtUser = TextUtils.Select($"SELECT ID,FullName, Code FROM Users");
            cbUser.Properties.DataSource = dtUser;
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            dtpEndTime.Value = dtpStartTime.Value.AddDays(+6);
        }

        private void rdGeneral_Click(object sender, EventArgs e)
        {
            //rdDetail.Checked = false;
            if (rdGeneral.Checked)
                Type = 0;
            LoadData();
        }

        private void rdDetail_Click(object sender, EventArgs e)
        {
            //rdGeneral.Checked = false;
            if (rdDetail.Checked)
                Type = 1;
            LoadData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }
    }
}
