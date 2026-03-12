using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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


namespace Forms.KPI_PO
{
    public partial class ucGoalStaffLead : UserControl
    {
        public string main;
        public DataTable GoalStaff;
        public SearchLookUpEdit cbUser;
        public NumericUpDown nbrMonth;
        public NumericUpDown nbrYear;
        public ToolStripButton btnSave;
        public string Position;
        public System.Windows.Forms.ComboBox cbQuy;

        public ucGoalStaffLead()
        {
            InitializeComponent();
        }

        private void ucGoalStaffLead_Load(object sender, EventArgs e)
        {
            this.nbrYear.ValueChanged += new System.EventHandler(this.nbrYear_ValueChanged);
            this.cbQuy.SelectedIndexChanged += new System.EventHandler(this.nbrMonth_ValueChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            loadForm();
        }
        DataSet ds;
        void loadForm()
        {
            string group = TextUtils.ToString(TextUtils.ExcuteScalar($"Select gs.GroupSalesCode From GroupSalesUser gu Inner join GroupSales gs on gs.ID=gu.GroupsalesID where UserID={cbUser.EditValue}"));
            if(group == cConsts.Purchase)
            {
                txtEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtEdit.Mask.EditMask = "p0";
                txtEdit.Mask.UseMaskAsDisplayFormat = true;
            }    
            ds = TextUtils.LoadDataSetFromSP("spGetGoalValueUser",  new string[] { "@Main", "@UserID", "@quy" }, new object[] { main, cbUser.EditValue,cbQuy.SelectedIndex });
            grdData.DataSource = ds.Tables[1];
            grvData.ExpandAllGroups();
            GoalStaff = ds.Tables[1];
            colGoal0.Caption = "Tháng " + ds.Tables[0].Rows[0]["MonthReport"];
            colGoal1.Caption = "Tháng " + ds.Tables[0].Rows[1]["MonthReport"];
            colGoal2.Caption = "Tháng " + ds.Tables[0].Rows[2]["MonthReport"];
        }
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GoalStaff = ds.Tables[1];
        }
        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            loadForm();
        }
        private void nbrMonth_ValueChanged(object sender, EventArgs e)
        {
            loadForm();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            loadForm();
        }

        public void ReLoadData(string check)
        {
            if (Position == cConsts.Staff || Position == cConsts.LeaderTeam || Position == cConsts.Leader)
                grvData.FocusedRowHandle = 1;
        }
        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
