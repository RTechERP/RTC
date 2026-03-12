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
    public partial class ucGoalAdmin : UserControl
    {
        public DataTable GoalAdmin;
        public SearchLookUpEdit cbUser;
        public NumericUpDown nbrMonth;
        public NumericUpDown nbrYear;
        public ToolStripButton btnSave;
        public int Quy;
        public string Position;
        public System.Windows.Forms.ComboBox cbQuy;

        public ucGoalAdmin()
        {
            InitializeComponent();
        }
    
        private void ucGoalAdmin_Load(object sender, EventArgs e)
        {
            this.nbrYear.ValueChanged += new System.EventHandler(this.nbrYear_ValueChanged);
            this.cbQuy.SelectedIndexChanged += new System.EventHandler(this.nbrMonth_ValueChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            LoadData();
        }
        DataTable dt;
        void LoadData()
        {
           
            dt = TextUtils.LoadDataFromSP("spGetGoalAdminMarketting", "A", new string[] { "@UserID", "@quy", "@Year" }, new object[] { cbUser.EditValue, cbQuy.SelectedIndex, nbrYear.Value });
            grdData.DataSource = dt;
            grvData.ExpandAllGroups();
            GoalAdmin = dt;
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GoalAdmin = dt;
        }
        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void nbrMonth_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        public void ReLoadData(string check)
        {
            if (Position == cConsts.Marketting || Position== cConsts.Admin)
                grvData.FocusedRowHandle = 1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
