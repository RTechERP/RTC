using BMS.Model;
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
    public partial class frmKpiRuleSumarizeTeamChooseEmployee : _Forms
    {
        public List<EmployeeModel> lstEmp = new List<EmployeeModel>();
        public List<EmployeeModel> lstEmpChose = new List<EmployeeModel>();
        public frmKpiRuleSumarizeTeamChooseEmployee()
        {
            InitializeComponent();
        }

        private void frmKpiRuleSumarizeTeamChooseEmployee_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            grdData.DataSource = lstEmp;
            grvData.SelectAll();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            List<EmployeeModel> lstDel = new List<EmployeeModel>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                bool isSelected = grvData.IsRowSelected(i);
                if (!isSelected) continue;

                int empID = TextUtils.ToInt(grvData.GetRowCellValue(i,colID));
                EmployeeModel model = lstEmp.FirstOrDefault(x => x.ID == empID) ?? new EmployeeModel();
                lstEmpChose.Add(model);
            }

            //foreach (var item in lstDel)
            //{
            //    lstEmp.Remove(item);
            //}

            this.DialogResult = DialogResult.OK;
        }
    }
}
