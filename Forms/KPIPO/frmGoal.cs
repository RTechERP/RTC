using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid;
using Forms.Classes;
using Forms.KPI_PO;
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
    public partial class frmGoal : _Forms
    {

        public frmGoal()
        {
            InitializeComponent();
        }
        public event cGlobVar.SendData SaveID;
        private void frmGoal_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                checktime();
                loaduser();
                cbQuy.SelectedIndex = quy;
                nbrYear.Value = DateTime.Now.Year;
                
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }
        int quy;string month;
        void checktime()
        {

            if (0 < DateTime.Now.Month && DateTime.Now.Month < 4)
            {
                month = "1,2,3";
                quy = 0;
            }
            if (3 < DateTime.Now.Month && DateTime.Now.Month < 7)
            {
                month = "4,5,6";
                quy = 1;
            }
            if (6 < DateTime.Now.Month && DateTime.Now.Month < 10)
            {
                month = "7,8,9";
                quy = 2;
            }
            if (9 < DateTime.Now.Month && DateTime.Now.Month < 13)
            {
                month = "10,11,12";
                quy = 3;
            }
        }
        void loaduser()
        {
            DataTable dt = TextUtils.Select("Select u.FullName,u.ID,G.SaleUserTypeID,s.SaleUserTypeName From GroupSalesUser G Inner join Users u On u.ID = g.UserID Inner join SaleUserType s on s.ID=g.SaleUserTypeID");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveID?.Invoke(Position);
            if (Position == "Adm" || Position == "Mar")
            {          
                AdminMarketingDetailModel model = new AdminMarketingDetailModel();
                for (int i = 0; i < ucAdmin.GoalAdmin.Rows.Count; i++)
                {
                    int ID = TextUtils.ToInt(ucAdmin.GoalAdmin.Rows[i]["ID"]);
                    model.ID = ID;
                    model.Quantity = TextUtils.ToInt(ucAdmin.GoalAdmin.Rows[i]["Quantity"]);
                    model.CompletionRate = TextUtils.ToDecimal(ucAdmin.GoalAdmin.Rows[i]["CompletionRate"]);
                    model.Quy = cbQuy.SelectedIndex;
                    model.Year = TextUtils.ToInt(DateTime.Now.Year);
                    model.UserID = TextUtils.ToInt(cbUser.EditValue);
                    model.KPIID = TextUtils.ToInt(ucAdmin.GoalAdmin.Rows[i]["IDMaster"]);
                    if (ID > 0)
                        AdminMarketingDetailBO.Instance.Update(model);
                    else
                        AdminMarketingDetailBO.Instance.Insert(model);
                }
            }
            else if (Position == "Sta" || Position == "LeadT" || Position == "LeadG")
            {
              //  grvData.FocusedRowHandle = -1;
                GoalModel model = new GoalModel();
                for (int i = 0; i < ucStaff.GoalStaff.Rows.Count; i++)
                {
                    int ID = TextUtils.ToInt(ucStaff.GoalStaff.Rows[i]["ID"]);
                    model.ID = ID;
                    model.MainIndexID = TextUtils.ToInt(ucStaff.GoalStaff.Rows[i]["IDMain"]);
                    model.Goal0 = TextUtils.ToDecimal(ucStaff.GoalStaff.Rows[i]["Goal0"]);
                    model.Goal1 = TextUtils.ToDecimal(ucStaff.GoalStaff.Rows[i]["Goal1"]);
                    model.Goal2 = TextUtils.ToDecimal(ucStaff.GoalStaff.Rows[i]["Goal2"]);
                    //model.MonthReport = TextUtils.ToInt(nbrMonth.Value);
                    model.Quy = TextUtils.ToInt(cbQuy.SelectedIndex);
                    model.Year = TextUtils.ToInt(DateTime.Now.Year);
                    model.UserID = TextUtils.ToInt(cbUser.EditValue);
                    if (ID > 0)
                        GoalBO.Instance.Update(model);
                    else
                        GoalBO.Instance.Insert(model);
                }
            }
                MessageBox.Show("Đã lưu thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cbUsser_EditValueChanged(null, null);
        }
        ucGoalStaffLead ucStaff;
        ucGoalAdmin ucAdmin;
        string Position;
        List<ucGoalStaffLead> lstgoal = new List<ucGoalStaffLead>();
        private void cbUsser_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            tableLayoutPanel1.Controls.Clear();
            DataTable dtt = TextUtils.Select($"Select s.SaleUserTypeCode,gs.MainIndexID from GroupSalesUser g Inner join SaleUserType s on g.SaleUserTypeID=s.ID Inner Join GroupSales gs on gs.ID = g.GroupSalesID where UserID={ cbUser.EditValue}");
            if(dtt.Rows.Count==0) return;
            Position = TextUtils.ToString( dtt.Rows[0]["SaleUserTypeCode"]);
            string main = TextUtils.ToString( dtt.Rows[0]["MainIndexID"]);
            if (Position == cConsts.Staff || Position == cConsts.LeaderTeam || Position == cConsts.Leader )
            {
                 ucStaff = new ucGoalStaffLead();
                ucStaff.cbUser = cbUser;
                ucStaff.cbQuy = cbQuy;
                ucStaff.nbrYear = nbrYear;
                ucStaff.btnSave = btnSave;
                ucStaff.main = main;
                ucStaff.Position=Position;
                ucStaff.Dock = DockStyle.Fill;
                this.SaveID += ucStaff.ReLoadData;
                tableLayoutPanel1.Controls.Add(ucStaff);
            }
            else if(Position == cConsts.Marketting )
            {
                ucAdmin = new ucGoalAdmin();
                ucAdmin.cbUser = cbUser;
                ucAdmin.cbQuy = cbQuy;
                ucAdmin.nbrYear = nbrYear;
                ucAdmin.btnSave = btnSave;
                ucAdmin.Position = Position;
                ucAdmin.Dock = DockStyle.Fill;
                this.SaveID += ucAdmin.ReLoadData;
                tableLayoutPanel1.Controls.Add(ucAdmin);
            }    
        }

        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            
        }
        
        private void nbrMonth_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;

        }

        private void cbQuy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
