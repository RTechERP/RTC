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
    public partial class frmWeekPlanDetail : _Forms
    {
        public DateTime DateS;
        public WeekPlanModel model = new WeekPlanModel();
        public DataRow[] dtr;
        public int UserID;
        public frmWeekPlanDetail()
        {
            InitializeComponent();
        }

        private void frmWeekReport_Load(object sender, EventArgs e)
        {
            loadUser();
            loadGrvData();
        }
        void loadUser()
        {
            DataTable dt = TextUtils.Select("Select * from Users");
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.DataSource = dt;
        }
        void loadGrvData()
        {
            if (dtr != null && dtr.Length>0)
            {
                cbUser.EditValue = UserID;
                grdData.DataSource = dtr.CopyToDataTable();
            }
            else
            {
                DataTable dt = TextUtils.Select($"Select * From WeekPlan where ID=0");
                if (dt.Rows.Count == 0)
                    for (int i = 0; i < 7; i++)
                    {
                        dt.Rows.Add(0, DateS);
                        DateS = DateS.AddDays(1);
                    }
                grdData.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                WeekPlanModel model = new WeekPlanModel();
                if (ID > 0)
                    model = (WeekPlanModel)WeekPlanBO.Instance.FindByPK(ID);
                model.DatePlan = TextUtils.ToDate2(grvData.GetRowCellValue(i, colDate));
                model.ContentPlan = TextUtils.ToString(grvData.GetRowCellValue(i, colContentPlan));
                model.Result = TextUtils.ToString(grvData.GetRowCellValue(i, colResult));
                model.UserID = TextUtils.ToInt(cbUser.EditValue);
                if (model.ID > 0)
                    WeekPlanBO.Instance.Update(model);
                else
                    WeekPlanBO.Instance.Insert(model);
            }
            MessageBox.Show("Đã cất thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmWeekReportDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
