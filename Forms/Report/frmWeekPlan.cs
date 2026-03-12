using BMS.Business;
using BMS.Model;
using Forms.Classes;
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
    public partial class frmWeekPlan : _Forms
    {
        public frmWeekPlan()
        {
            InitializeComponent();
        }

        private void frmWeekReport_Load(object sender, EventArgs e)
        {
            GetDayOfWeek();
            loadUser();
            loadGrvData();
        }
        void loadGrvData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetDataWeekPlan", "A", new string[] {"@DateS", "@DateE" }, new object[] {dtpDateS.Value, dtpDateE.Value });
            grdData.DataSource = dt;
            grvData.ExpandAllGroups();
        }
        void loadUser()
        {
            DataTable dt = TextUtils.Select("Select * from Users");
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.DataSource = dt;
        }
        void GetDayOfWeek()
        {
            string date = DateTime.Now.DayOfWeek.ToString();
            switch (date)
            {
                case cConsts.Monday:
                    dtpDateS.Value = DateTime.Now;
                    dtpDateE.Value = DateTime.Now.AddDays(6);
                    break;
                case cConsts.Tuesday:
                    dtpDateS.Value = DateTime.Now.AddDays(-1);
                    dtpDateE.Value = DateTime.Now.AddDays(5);
                    break;
                case cConsts.Wednesday:
                    dtpDateS.Value = DateTime.Now.AddDays(-2);
                    dtpDateE.Value = DateTime.Now.AddDays(4);
                    break;
                case cConsts.Thursday:
                    dtpDateS.Value = DateTime.Now.AddDays(-3);
                    dtpDateE.Value = DateTime.Now.AddDays(3);
                    break;
                case cConsts.Friday:
                    dtpDateS.Value = DateTime.Now.AddDays(-4);
                    dtpDateE.Value = DateTime.Now.AddDays(2);
                    break;
                case cConsts.Saturday:
                    dtpDateS.Value = DateTime.Now.AddDays(-5);
                    dtpDateE.Value = DateTime.Now.AddDays(1);
                    break;
                case cConsts.Sunday:
                    dtpDateS.Value = DateTime.Now.AddDays(-6);
                    dtpDateE.Value = DateTime.Now;
                    break;
                default:
                    break;
            }
            dtpDateS.Value = new DateTime(dtpDateS.Value.Year, dtpDateS.Value.Month, dtpDateS.Value.Day, 0, 0, 0);
            dtpDateE.Value = new DateTime(dtpDateE.Value.Year, dtpDateE.Value.Month, dtpDateE.Value.Day, 23, 59, 59);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmWeekPlanDetail frm = new frmWeekPlanDetail();
            frm.DateS = dtpDateS.Value;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                
            }    
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpDateS.Value = dtpDateS.Value.AddDays(7);
            dtpDateE.Value = dtpDateE.Value.AddDays(7);
            loadGrvData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int UserID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserID));
            DataTable dt = (DataTable)grdData.DataSource;
            DataRow[] dtr = dt.Select($"UserID ={UserID}");
            frmWeekPlanDetail frm = new frmWeekPlanDetail();
            frm.UserID = UserID;
            frm.DateS = dtpDateS.Value;
            frm.dtr = dtr;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadGrvData();
            }
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmWeekPlanExcel frm = new frmWeekPlanExcel();
            if(frm.ShowDialog()==DialogResult.OK)
            {
                loadGrvData();
            }    
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            dtpDateS.Value = dtpDateS.Value.AddDays(-7);
            dtpDateE.Value = dtpDateE.Value.AddDays(-7);
            loadGrvData();
        }
    }
}
