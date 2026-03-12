using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmLeaderProjectDetail : _Forms
    {
        List<EmployeeApproveModel> list = new List<EmployeeApproveModel>();
        public frmLeaderProjectDetail()
        {
            InitializeComponent();
        }

        private void frmLeaderProjectDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            DataTable dt = TextUtils.GetTable("spGetUserProjectItem");
            dt.Columns.Add("IsSelect", typeof(bool));
            //grdData.DataSource = TextUtils.LoadDataFromSP("spGetEmployeeWithDepartment", "A", new string[] { }, new object[] { });
            grdData.DataSource = dt;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //int[] rowSelected = grvData.GetSelectedRows();
            //foreach (int row in rowSelected)
            //{
            //    int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
            //    if (employeeID <= 0)
            //    {
            //        continue;
            //    }

            //    EmployeeApproveModel employee = SQLHelper<EmployeeApproveModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.EmployeeApprove WHERE EmployeeID = {employeeID} AND Type = 2");
            //    if (employee.EmployeeID <= 0)
            //    {
            //        employee = new EmployeeApproveModel();
            //    }
            //    employee.EmployeeID = employeeID;
            //    employee.Code = TextUtils.ToString(grvData.GetRowCellValue(row, colCode));
            //    employee.FullName = TextUtils.ToString(grvData.GetRowCellValue(row, colFullName));
            //    employee.Type = 2;
            //    //employee.UsersID = TextUtils.ToInt(grvData.GetRowCellValue(row, colUserID));
            //    if (employee.ID > 0)
            //    {
            //        SQLHelper<EmployeeApproveModel>.Update(employee);
            //    }
            //    else
            //    {
            //        SQLHelper<EmployeeApproveModel>.Insert(employee);
            //    }
            //}

            foreach (var item in list)
            {
                if (item.EmployeeID <= 0)
                {
                    continue;
                }
                EmployeeApproveModel employee = SQLHelper<EmployeeApproveModel>.SqlToModel($"SELECT * FROM dbo.EmployeeApprove WHERE EmployeeID = {item.EmployeeID}");
                if (employee.EmployeeID <= 0)
                {
                    employee = new EmployeeApproveModel();
                }
                employee.EmployeeID = item.EmployeeID;
                employee.Code = item.Code;
                employee.FullName = item.FullName;
                employee.Type = 2;
                if (employee.ID > 0)
                {
                    EmployeeApproveBO.Instance.Update(employee);
                }
                else
                {
                    EmployeeApproveBO.Instance.Insert(employee);
                }
            }

            this.DialogResult = DialogResult.OK;

        }

        private void grvData_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            ////var rowSelected = grvData.GetSelectedRows();
            ////MessageBox.Show(rowSelected.Length.ToString());

            //if (e.Action == CollectionChangeAction.Add || e.Action == CollectionChangeAction.Refresh)
            //{
            //    int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(e.ControllerRow, colID));
            //    EmployeeApproveModel selected = new EmployeeApproveModel();
            //    var match = list.Where(x => x.EmployeeID == employeeID).ToList();
            //    if (match.Count() <= 0)
            //    {
            //        selected.EmployeeID = TextUtils.ToInt(grvData.GetRowCellValue(e.ControllerRow, colID));
            //        selected.Code = TextUtils.ToString(grvData.GetRowCellValue(e.ControllerRow, colCode));
            //        selected.FullName = TextUtils.ToString(grvData.GetRowCellValue(e.ControllerRow, colFullName));
            //        list.Add(selected);
            //    }
            //}
            //else
            //{
            //    EmployeeApproveModel selected = list.FirstOrDefault(x => x.EmployeeID == TextUtils.ToInt(grvData.GetRowCellValue(e.ControllerRow, colID)));
            //    list.Remove(selected);
            //}
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    object val1 = view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentID");
                    object val2 = view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentID");
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkSelected_CheckedChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            bool isSelect = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsSelect));
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (isSelect)
            {
                EmployeeApproveModel selected = new EmployeeApproveModel();
                var match = list.Where(x => x.EmployeeID == id).ToList();
                if (match.Count() <= 0)
                {
                    selected.EmployeeID = id;
                    selected.Code = TextUtils.ToString(grvData.GetFocusedRowCellValue( colCode));
                    selected.FullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
                    list.Add(selected);
                }
            }
            else
            {
                EmployeeApproveModel selected = list.FirstOrDefault(x => x.EmployeeID == id);
                list.Remove(selected);
            }
        }
    }
}