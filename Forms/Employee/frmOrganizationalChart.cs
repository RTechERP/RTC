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

namespace BMS
{
    public partial class frmOrganizationalChart: _Forms
    {
        public frmOrganizationalChart()
        {
            InitializeComponent();
        }

        private void frmOrganizationalChart_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        void LoadData()
        {
            tlData.DataSource = null;
            //List<OrganizationalChartModel> organizationals = SQLHelper<OrganizationalChartModel>.FindAll();
            int taxCompanyID = 0;
            int departmentID = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetOrganizationalChart", "A", new string[] { "@TaxCompanyID", "@DepartmentID" }, new object[] { taxCompanyID,departmentID });
            tlData.DataSource = dt;
            tlData.ExpandAll();

            LoadDetail();
        }


        void LoadDetail()
        {
            grdDetail.DataSource = null;
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(OrganizationalChartModel_Enum.ID.ToString()));
            DataTable dt = TextUtils.LoadDataFromSP("spGetOrganizationalChartDetail","A", new string[] { "@ID" }, new object[] { id });

            grdDetail.DataSource = dt;
        }

        private void btnAddTeam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmOrganizationalChartDetail frm = new frmOrganizationalChartDetail();
            frm.Show();
            frm.SaveEvent += LoadData;
        }

        private void btnEditTeam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(OrganizationalChartModel_Enum.ID.ToString()));
            if (id <= 0) return;
            OrganizationalChartModel organizational = SQLHelper<OrganizationalChartModel>.FindByID(id);
            frmOrganizationalChartDetail frm = new frmOrganizationalChartDetail();
            frm.organizational = organizational;
            frm.Show();
            frm.SaveEvent += LoadData;
        }

        private void btnDeleteTeam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(OrganizationalChartModel_Enum.ID.ToString()));
            if (id <= 0) return;

            string name = TextUtils.ToString(tlData.GetFocusedRowCellValue(OrganizationalChartModel_Enum.Name.ToString()));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa team [{name}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    { OrganizationalChartModel_Enum.IsDeleted.ToString(),true},
                    { OrganizationalChartModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    { OrganizationalChartModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                };

                SQLHelper<OrganizationalChartModel>.UpdateFieldsByID(myDict, id);

                LoadData();
            }
        }

        private void tlData_DoubleClick(object sender, EventArgs e)
        {
            btnEditTeam_ItemClick(null, null);
        }

        private void btnAddEmployee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(tlData.FocusedNode.GetValue(OrganizationalChartModel_Enum.ID.ToString()));
            if (id <= 0) return;

            frmChooseEmployee frm = new frmChooseEmployee();
            //frm.UserTeamID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //List<int> lstEmployeeID = frm.employeeIds;
                if (frm.employeeIds.Count == 0) return;

                foreach (var item in frm.employeeIds)
                {
                    OrganizationalChartDetailModel detail = new OrganizationalChartDetailModel()
                    {
                        OrganizationalChartID = id,
                        EmployeeID = item,
                    };

                    SQLHelper<OrganizationalChartDetailModel>.Insert(detail);
                    
                }

                LoadDetail();

            }
        }

        private void btnDeleteEmployee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = grvDetail.GetSelectedRows();

            if (selectedRows.Count() <= 0) return;
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> deletedIDs = new List<int>();
                foreach (var row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvDetail.GetRowCellValue(row, OrganizationalChartDetailModel_Enum.ID.ToString()));
                    if (!deletedIDs.Contains(id)) deletedIDs.Add(id);

                }

                if (deletedIDs.Count > 0)
                {
                    string idText = string.Join(",", deletedIDs);
                    var myDict = new Dictionary<string, object>()
                    {
                        { OrganizationalChartDetailModel_Enum.IsDeleted.ToString(),true},
                        { OrganizationalChartDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        { OrganizationalChartDetailModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    };

                    SQLHelper<OrganizationalChartDetailModel>.UpdateFields(myDict, new Utils.Expression(OrganizationalChartDetailModel_Enum.ID.ToString(), idText, "IN"));

                    LoadDetail();
                }
            }
        }

        private void tlData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            LoadDetail();
        }
    }
}
