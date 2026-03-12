using System;
using BMS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    public partial class frmProjectMachinePrice : _Forms
    {
        public frmProjectMachinePrice()
        {
            InitializeComponent();
        }

        private void frmProjectMachinePrice_Load(object sender, EventArgs e)
        {
            dtpDS.Value = DateTime.Now.AddMonths(-1);
            LoadProject();
            LoadEmployee();
            LoadCustomer();
            LoadData();
        }

        void LoadProject()
        {
            List<ProjectModel> ls = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = ls;
        }

        private void LoadEmployee()
        {
            List<EmployeeModel> ls = SQLHelper<EmployeeModel>.FindAll();
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = ls;
        }

        private void LoadCustomer()
        {
            List<CustomerModel> ls = SQLHelper<CustomerModel>.FindAll();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = ls;
        }

        void LoadData()
        {
            DateTime DS = dtpDS.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
            DateTime DE = dtpDE.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            int EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            if (EmployeeID <= 0) EmployeeID = -1;

            int CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            if (CustomerID <= 0) CustomerID = -1;

            int ProjectID = TextUtils.ToInt(cboProject.EditValue);
            if (ProjectID <= 0) ProjectID = -1;

            DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectMachinePrice",
                new string[] { "@DS", "@DE", "@EmployeeID", "@CustomerID", "@ProjectID", "@IsDelete" },
                new object[] { DS, DE, EmployeeID, CustomerID, ProjectID, false });
            grdData.DataSource = dt;
        }

        private void LoadDataDetail()
        {
            int RowHandel = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetRowCellValue(RowHandel, $"{colIDMaster.FieldName}"));

            xtraTabControl1.TabPages.Clear();
            xtraTabControl1.TabPages.Add(DefaultPage);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectMachinePriceDetail",
               new string[] { "ID" },
               new object[] { ID });


            var groupedData = dt.AsEnumerable().GroupBy(row => row.Field<string>("NameGroup"));

            foreach (var group in groupedData)
            {
                DataTable groupTable = group.CopyToDataTable();
                XtraTabPage tabPage = new XtraTabPage();
                tabPage.Text = group.Key;
                tabPage.Controls.Add(CloneGridControl(grdDataDetail, groupTable));

                xtraTabControl1.TabPages.Insert(xtraTabControl1.TabPages.Count-1, tabPage);
            }
            if(xtraTabControl1.TabPages.Count > 1)
            {
                xtraTabControl1.TabPages.RemoveAt(xtraTabControl1.TabPages.IndexOf(DefaultPage));
            }
            xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count -1;

        }

        GridControl CloneGridControl(GridControl template, DataTable dt)
        {
            GridControl gridControl = new GridControl();
            GridView gridView = new GridView(gridControl);

            gridView.OptionsBehavior.Editable = true;
            gridView.OptionsBehavior.ReadOnly = true;

            gridControl.MainView = gridView;
            gridControl.Dock = template.Dock;

            gridControl.DataSource = dt;

            gridControl.ContextMenuStrip = template.ContextMenuStrip;

            gridView.Assign(template.MainView as GridView, false);
            return gridControl;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmSetProjectMachinePrice frm = new frmSetProjectMachinePrice();
            frm.IDProject = -1;
            frm.ProjectMachineID = -1;
            frm.IDEmployee = -1;
            frm.IDCustomer = -1;
            frm.d = DateTime.Now;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
                LoadDataDetail();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmSetProjectMachinePrice frm = new frmSetProjectMachinePrice();
            int ProjectMachineID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDMaster));
            int IDProject = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            int IDEmployee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int IDCustomer = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCustomerID));
            DateTime d = TextUtils.ToDate(TextUtils.ToString(grvData.GetFocusedRowCellValue(colCreatedDate)));

            frm.ProjectMachineID = ProjectMachineID;
            frm.IDEmployee = IDEmployee;
            frm.IDProject = IDProject;
            frm.IDCustomer = IDCustomer;
            frm.d = d;

            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
                LoadDataDetail();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (grvData.RowCount > 0)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDMaster));
                if (ID == 0) return;
                if (MessageBox.Show(string.Format($"Bạn có chắc chắn muốn xóa không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProjectMachinePriceModel pmp = SQLHelper<ProjectMachinePriceModel>.FindByID(ID);
                    pmp.IsDelete = true;
                    SQLHelper<ProjectMachinePriceModel>.Update(pmp);
                    grvData.DeleteSelectedRows();
                    LoadDataDetail();
                    LoadData();
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataDetail();
        }


    }
}
