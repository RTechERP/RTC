using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmEmployeeApproDetail : _Forms
    {
        EmployeeApproveModel employee = new EmployeeApproveModel();

        List<EmployeeApproveSelected> employeeSelected = new List<EmployeeApproveSelected>();
        public frmEmployeeApproDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeeApproDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            DataTable dt = TextUtils.Select("SELECT ID, Code, FullName FROM Employee WHERE Status <> 1");
            grdData.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
            foreach (var item in employeeSelected)
            {
                var exp1 = new Expression("EmployeeID", item.EmployeeID);
                var exp2 = new Expression("Type", 1);

                var matchValue = SQLHelper<EmployeeApproveModel>.FindByExpression(exp1.And(exp2)).ToList();

                if (matchValue.Count > 0)
                {
                    //employee = (EmployeeApproveModel)EmployeeApproveBO.Instance.FindByCode("EmployeeID", item.EmployeeID.ToString());
                    employee = matchValue.FirstOrDefault();
                }

                //if (employee == null)
                //{
                //    employee = new EmployeeApproveModel();
                //}

                employee.EmployeeID = item.EmployeeID;
                employee.Code = item.Code;
                employee.FullName = item.FullName;
                employee.Type = 1;

                if (employee.ID > 0)
                {
                    EmployeeApproveBO.Instance.Update(employee);
                }
                else
                {
                    EmployeeApproveBO.Instance.Insert(employee);
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void grvData_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
            {
                EmployeeApproveSelected selected = new EmployeeApproveSelected();
                var match = employeeSelected.Where(x => x.EmployeeID == TextUtils.ToInt(grvData.GetRowCellValue(e.ControllerRow, colID))).ToList();
                if (match.Count() <= 0)
                {
                    selected.EmployeeID = TextUtils.ToInt(grvData.GetRowCellValue(e.ControllerRow, colID));
                    selected.Code = TextUtils.ToString(grvData.GetRowCellValue(e.ControllerRow, colCode));
                    selected.FullName = TextUtils.ToString(grvData.GetRowCellValue(e.ControllerRow, colFullName));
                    employeeSelected.Add(selected);
                }
            }
            else
            {
                EmployeeApproveSelected selected = employeeSelected.FirstOrDefault(x => x.EmployeeID == TextUtils.ToInt(grvData.GetRowCellValue(e.ControllerRow, colID)));
                employeeSelected.Remove(selected);
            }
        }

        private class EmployeeApproveSelected
        {
            public int EmployeeID { get; set; }
            public string Code { get; set; }
            public string FullName { get; set; }
        }
    }
}
