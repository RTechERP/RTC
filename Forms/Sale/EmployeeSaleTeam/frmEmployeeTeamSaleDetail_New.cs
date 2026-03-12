using BMS;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Sale.EmployeeSaleManager
{
    public partial class frmEmployeeTeamSaleDetail_New : _Forms
    {
        int departmentID = 0;
        public EmployeeTeamSaleLinkModel linkModel = new EmployeeTeamSaleLinkModel();
        List<int> lstAdd = new List<int>();
        public Func<EmployeeTeamSaleLinkModel, Task> SaveEvent;
        public frmEmployeeTeamSaleDetail_New()
        {
            InitializeComponent();
        }
        void loadData()
        {
            DataTable dt = SQLHelper<EmployeeModel>.LoadDataFromSP("spGetEmployeeByKPIPositionDepartmentID",
                                                    new string[] { "@DepartmentID" },
                                                    new object[] { departmentID });
            grdData.DataSource = dt;
        }

        private void frmEmployeeTeamSaleDetail_New_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                loadData();
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.Close();
            }
        }
        bool saveData()
        {
            List<int> selectedRows = grvData.GetSelectedRows().ToList();
            if(selectedRows.Count == 0)
            {
                TextUtils.ShowError("Vui lòng chọn nhân viên!");
                return false;
            }

            foreach (int rowHandle in selectedRows)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));

                if (!lstAdd.Contains(id))
                {
                    lstAdd.Add(id);
                }
            }
            List<int> itemsToRemove = new List<int>();

            foreach (int item in lstAdd) 
            {
                
                Expression ex1 = new Expression("EmployeeID", item);
                Expression ex2 = new Expression("EmployeeTeamSaleID", linkModel.EmployeeTeamSaleID);
                List<EmployeeTeamSaleLinkModel> existingRecords = SQLHelper<EmployeeTeamSaleLinkModel>.FindByExpression(ex1.And(ex2));

                if (existingRecords.Count > 0)
                {
                    itemsToRemove.Add(item);
                }

            }
            
            
            foreach (int id in itemsToRemove)
            {
                lstAdd.Remove(id);
            }

            foreach (int item in lstAdd)
            {
                EmployeeTeamSaleLinkModel newModel = new EmployeeTeamSaleLinkModel
                {
                    EmployeeID = item,
                    EmployeeTeamSaleID = linkModel.EmployeeTeamSaleID
                };

                newModel.ID = SQLHelper<EmployeeTeamSaleLinkModel>.Insert(newModel).ID;
                SaveEvent?.Invoke(newModel);             
                grdData.RefreshDataSource();
            }

            lstAdd.Clear();
            return true;
        }

        



        
    }
}

