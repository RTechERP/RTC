using BMS;
using BMS.Business;
using BMS.Model;
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

namespace Forms.Employee
{
    public partial class frmteamtrees : _Forms
    {
        public int ID = 0;
        public DepartmentModel dm = new DepartmentModel();
        public List<int> ListID = new List<int>();
        public frmteamtrees()
        {
            InitializeComponent();
        }
        void loadDepartmentGroup()
        {
            try
            {
                DataTable dt = TextUtils.Select("SELECT * FROM dbo.Department");
                DataRow row = dt.NewRow();
                row["ID"] = 0;
                row["Name"] = "--Tất cả các phòng--";
                dt.Rows.InsertAt(row, 0);
                cboDepartment.DataSource = dt;
                cboDepartment.DisplayMember = "Name";
                cboDepartment.ValueMember = "ID";
                cboDepartment.SelectedValue = Global.DepartmentID < 0 ? 0 : Global.DepartmentID;
            }
            catch
            {
            }
        }

        void loadTree()
        {
            try
            {
                int id = Global.DepartmentID < 0 ? 0 : (Global.IsAdmin ? 0 : Global.DepartmentID);
                DataTable dt = TextUtils.LoadDataFromSP("spGetTreeUserTeamData", "A",
                                                        new string[] { "@DepartmentID" },
                                                        new object[] { id });

                DataRow row = dt.NewRow();
                row["ID"] = 0;
                row["Name"] = "--Tất cả các phòng--";
                dt.Rows.InsertAt(row, 0);

                treeTeam.DataSource = dt;
                treeTeam.ExpandAll();
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        void loadEmployee()
        {
            int dID = TextUtils.ToInt(cboDepartment.SelectedValue);
            DataTable dt = TextUtils.GetDataTableFromSP("spGetTreeUserTeamData", new string[] { "@DepartmentID" }, new object[] { dID });
            treeTeam.DataSource = dt;
            treeTeam.ExpandAll();
        }
        void loadUserTeamLink()
        {
            try
            {
                int departmentID = 0;
                if (cboDepartment.SelectedIndex < 0) return;
                int userGroupID = TextUtils.ToInt(cboDepartment.SelectedValue);
                int id = TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colTreeID));
                if (id == 0) return;
                if (id < 0) departmentID = TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colDepartmentID));
                DataTable dt = TextUtils.LoadDataFromSP("spGetUserTeamLink_New", "A",
                   new string[] { "@UserTeamID", "@DepartmentID" },
                   new object[] { id, departmentID });
                grdUserTeamLink.DataSource = dt;
                grvUserTeamLink.ExpandAllGroups();
            }
            catch
            {
            }
        }

        private void treeTeam_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            loadUserTeamLink();
        }
        private void frmTeam_SizeChanged(object sender, EventArgs e)
        {
            int width = splitContainerControl1.Width;
            splitContainerControl1.Panel1.MinSize = (width * 40) / 100;
            splitContainerControl1.Panel2.MinSize = (width * 60) / 100;
        }
        private void frmteamtrees_Load(object sender, EventArgs e)
        {
            loadDepartmentGroup();
            loadTree();
            //loadUser();

        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colTreeID)));
            int DepartmentID = TextUtils.ToInt(TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colDepartmentID)));
            frmUserTeamDetail frm = new frmUserTeamDetail();
            frm.ID = ID;
            frm.DepartmentID = DepartmentID;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadUserTeamLink();
                loadTree();
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colTreeID));
            if (ID <= 0)
            {
                return;
            }
            UserTeamModel model = (UserTeamModel)UserTeamBO.Instance.FindByPK(ID);
            frmUserTeamDetail frm = new frmUserTeamDetail();
            frm.ut = model;
            frm.ID = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadUserTeamLink();
                loadTree();
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            int ID = TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colTreeID));
            string teamName = TextUtils.ToString(treeTeam.FocusedNode.GetValue(colName));
            if (MessageBox.Show(string.Format("Bạn có muốn xóa team [{0}] hay không ?", teamName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserTeamBO.Instance.Delete(ID);
                treeTeam.DeleteSelectedNodes();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnAddStaff_Click_1(object sender, EventArgs e)
        {
            int masterID = TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colTreeID));
            if (masterID <= 0) return;

            frmChooseEmployee frm = new frmChooseEmployee();
            frm.UserTeamID = masterID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<int> lstEmployeeID = frm.LstID;
                if (lstEmployeeID.Count == 0) return;
                string lstID = string.Join(";", lstEmployeeID);
                TextUtils.ExcuteProcedure("spAddUserToUserTeamLink", new string[] { "@ListUserID", "@UserTeamID" }, new object[] { lstID, masterID });
                loadUserTeamLink();
            }
        }

        private void btnDeleteStaff_Click_1(object sender, EventArgs e)
        {
            if (grvUserTeamLink.FocusedRowHandle < 0)
                return;
            if (MessageBox.Show("Bạn có chắc muốn xóa người dùng này không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int[] arrRowIndex = grvUserTeamLink.GetSelectedRows();
            for (int i = 0; i < arrRowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvUserTeamLink.GetRowCellValue(arrRowIndex[i], colIDUserTeamLink));
                UserTeamLinkBO.Instance.Delete(id);
            }
            grvUserTeamLink.DeleteSelectedRows();
        }

        private void grdUserTeamLink_Load_1(object sender, EventArgs e)
        {
            grvUserTeamLink.OptionsSelection.MultiSelect = true;
            grvUserTeamLink.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadEmployee();
        }

        private void treeTeam_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.WhiteSmoke;
            if (e.Node.Focused)
            {
                e.Appearance.BackColor = Color.BlueViolet;
                e.Appearance.ForeColor = Color.Yellow;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cboDepartment.SelectedValue = 0;
            int id = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetTreeUserTeamData", "A",
                                        new string[] { "@DepartmentID" },
                                        new object[] { id });

            DataRow row = dt.NewRow();
            row["ID"] = 0;
            row["Name"] = "--Tất cả các phòng--";
            dt.Rows.InsertAt(row, 0);

            treeTeam.DataSource = dt;
            treeTeam.ExpandAll();
        }

        private void treeTeam_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click_1(null, null);
        }
    }
}
