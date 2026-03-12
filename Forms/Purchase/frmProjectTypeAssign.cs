using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectTypeAssign : _Forms
    {
        public frmProjectTypeAssign()
        {
            InitializeComponent();
        }

        private void loadTree()
        {
            try
            {
                //DataTable dt = TextUtils.Select(
                //    @"SELECT *,
                //        CASE
                //            WHEN EXISTS (
                //                SELECT 1
                //                FROM dbo.ProjectType AS pt
                //                WHERE pt.ParentID = p.ID
                //            ) THEN 0
                //            ELSE 1
                //        END AS IsAllowedToAdd
                //    FROM dbo.ProjectType AS p");


                //DataDaSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectType", new string[] { "@FilterText" }, new object[] { "" });
                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectType", "A", new string[] { "@FilterText" }, new object[] { "" });
                //DataTable dt = dataSet.Tables[1];

                DataRow row = dt.NewRow();
                row["ID"] = 0;
                row["ProjectTypeName"] = "--Tất cả--";
                dt.Rows.InsertAt(row, 0);

                tlProjectType.DataSource = dt;
                tlProjectType.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadAssignedEmployee()
        {
            try
            {
                int projectTypeID = TextUtils.ToInt(tlProjectType.FocusedNode.GetValue(colTreeID));
                DataTable dt = TextUtils.GetDataTableFromSP("spGetProjectTypeAssign",
                   new string[] { "@ProjectTypeID" },
                   new object[] { projectTypeID });
                grdUserTeamLink.DataSource = dt;
                grvUserTeamLink.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeTeam_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            loadAssignedEmployee();
        }

        private void frmteamtrees_Load(object sender, EventArgs e)
        {
            loadTree();
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            //int isAllowedToAdd = TextUtils.ToInt(treeTeam.FocusedNode.GetValue(colIsAllowedToAdd));
            //if (isAllowedToAdd == 0) return;
            int projectTypeID = TextUtils.ToInt(tlProjectType.FocusedNode.GetValue(colTreeID));

            var node = tlProjectType.FocusedNode;
            if (node.HasChildren) return;

            frmChooseEmployee frm = new frmChooseEmployee();
            frm.cboDepartment.EditValue = Global.DepartmentID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<int> lstUserID = frm.LstID;
                if (lstUserID.Count == 0) return;
                foreach (var userID in lstUserID)
                {
                    EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("UserID", userID).FirstOrDefault();
                    if (employee == null) continue;

                    var exp1 = new Expression("ProjectTypeID", projectTypeID);
                    var exp2 = new Expression("EmployeeID", employee.ID);
                    ProjectTypeAssignModel assign = SQLHelper<ProjectTypeAssignModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                    assign = assign == null ? new ProjectTypeAssignModel() : assign;
                    assign.ProjectTypeID = projectTypeID;
                    assign.EmployeeID = employee.ID;

                    //var newAssign = new ProjectTypeAssignModel
                    //{
                    //    ProjectTypeID = projectTypeID,
                    //    EmployeeID = employee.ID
                    //};

                    if (assign.ID <= 0)
                    {
                        SQLHelper<ProjectTypeAssignModel>.Insert(assign);
                    }
                    else
                    {
                        SQLHelper<ProjectTypeAssignModel>.Update(assign);
                    }

                }
                loadAssignedEmployee();
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (grvUserTeamLink.FocusedRowHandle < 0)
                return;
            if (MessageBox.Show("Bạn có chắc muốn xóa người dùng này không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int[] arrRowIndex = grvUserTeamLink.GetSelectedRows();
            for (int i = 0; i < arrRowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvUserTeamLink.GetRowCellValue(arrRowIndex[i], colAssignID));
                TextUtils.ExcuteSQL($"delete from ProjectTypeAssign where ID ={id}");
            }
            grvUserTeamLink.DeleteSelectedRows();
        }

        private void grdUserTeamLink_Load_1(object sender, EventArgs e)
        {
            grvUserTeamLink.OptionsSelection.MultiSelect = true;
            grvUserTeamLink.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            loadTree();
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
    }
}