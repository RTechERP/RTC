using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
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
    public partial class frmLeaderProjectType : _Forms
    {
        public frmLeaderProjectType()
        {
            InitializeComponent();
        }
        private void frmLeaderProjectType_Load(object sender, EventArgs e)
        {
            LoadProjectType();
        }
        void LoadProjectType()
        {

            //DataTable dt = TextUtils.LoadDataFromSP("ProjectTypeModel", "A",
            //                                        new string[] { "@ProjectSurveyID", "@ProjectID" },
            //                                        new object[] { 0, 0 });

            List<ProjectTypeModel> list = SQLHelper<ProjectTypeModel>.FindAll();
            list.Insert(0, new ProjectTypeModel()
            {
                ID = 0,
                ProjectTypeName = "--Tất cả--"
            });
            tlProjectType.DataSource = list;

            tlProjectType.ExpandAll();
            LoadLeader();
        }
        void LoadLeader()
        {
            int projectTypeID = TextUtils.ToInt(tlProjectType.GetFocusedRowCellValue(colProjectTypeID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeProjectType", "A",
                                                    new string[] { "@ProjectTypeID" },
                                                    new object[] { projectTypeID });
            grdLeader.DataSource = dt;
        }

        private void tlProjectType_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            LoadLeader();
        }

        private void btnAddLeader_Click(object sender, EventArgs e)
        {

            int projectTypeID = TextUtils.ToInt(tlProjectType.GetFocusedRowCellValue(colProjectTypeID));
            if (projectTypeID == 0) return;

            frmChooseLeader frm = new frmChooseLeader();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                List<int> lstEmployeeID = frm.lstID;
                if (lstEmployeeID.Count == 0) return;
                foreach (var employeeID in lstEmployeeID)
                {
                    EmployeeProjectTypeModel model = new EmployeeProjectTypeModel();
                    model.ProjectTypeID = projectTypeID;
                    model.EmployeeID = employeeID;

                    var ex1 = new Expression("ProjectTypeID", model.ProjectTypeID);
                    var ex2 = new Expression("EmployeeID", model.EmployeeID);
                    EmployeeProjectTypeModel exist = SQLHelper<EmployeeProjectTypeModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault();
                    if (exist != null) continue;

                    SQLHelper<EmployeeProjectTypeModel>.Insert(model);
                }
                LoadLeader();
            }
            
        }

        private void btnDeleteLeader_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvLeader.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn Leader muốn xóa!");
                return;
            }
            if (MessageBox.Show($"Bạn có chắc muốn xóa không?", "Thống báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                {
                    foreach (int rowIndex in rowSelecteds)
                    {
                        int ID = TextUtils.ToInt(grvLeader.GetRowCellValue(rowIndex, colID));
                        SQLHelper<EmployeeProjectTypeModel>.DeleteModelByID(ID);
                    }
                    LoadLeader();
                }
            }
        }

        private void btnAddLeaders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnAddLeader_Click(null, null);
        }

        private void btnDeleteLeaders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnDeleteLeader_Click(null, null);
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadProjectType();
        }
    }
}