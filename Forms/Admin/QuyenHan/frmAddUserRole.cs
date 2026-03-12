using BMS;
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

namespace Forms.Admin.QuyenHan
{
    public partial class frmAddUserRole : _Forms
    {
        public frmAddUserRole()
        {
            InitializeComponent();
        }

        private void frmAddUserRole_Load(object sender, EventArgs e)
        {
            loadEmployee();
            LoadUser2Data();
        }


        void loadEmployee()
        {
            //List<UsersModel> user = SQLHelper<UsersModel>.FindAll();

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });

            dt = dt.Select("UserID <> 0").CopyToDataTable();

            cboUserFrom.Properties.DataSource = dt;
            cboUserFrom.Properties.DisplayMember = "FullName";
            cboUserFrom.Properties.ValueMember = "UserID";

            cboUserTo.Properties.DataSource = dt;
            cboUserTo.Properties.DisplayMember = "FullName";
            cboUserTo.Properties.ValueMember = "UserID";
        }

        private void cboUser1_EditValueChanged(object sender, EventArgs e)
        {
            int userID = TextUtils.ToInt(cboUserFrom.EditValue);
            DataTable dt = TextUtils.GetDataTableFromSP("spGetUserGroupIdByUserID", new string[] { "@UserID" }, new object[] { userID });
            grdUser1.DataSource = dt;
        }

        private void cboUser2_EditValueChanged(object sender, EventArgs e)
        {
            LoadUser2Data();
        }

        private void LoadUser2Data()
        {
            int userID = TextUtils.ToInt(cboUserTo.EditValue);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetUserGroupIdByUserID", new string[] { "@UserID" }, new object[] { userID });

            grdUser2.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int[] selectedRowHandles = grvUser1.GetSelectedRows();
            int ID = TextUtils.ToInt(cboUserTo.EditValue);
            HashSet<int> processedRoleIDs = new HashSet<int>();

            foreach (int rowHandle in selectedRowHandles)
            {
                if (rowHandle >= 0) // Ensure it's a valid row
                {
                    int roleId = Convert.ToInt32(grvUser1.GetRowCellValue(rowHandle, "RoleID"));

                    if (processedRoleIDs.Contains(roleId)) continue; // Skip if this RoleID has already been processed

                    processedRoleIDs.Add(roleId); // Mark this RoleID as processed

                    UserGroupLinkModel userGroupLinkModel = new UserGroupLinkModel
                    {
                        UserGroupID = roleId,
                        UserID = ID
                    };

                    var exp1 = new Expression("UserID", ID);
                    var exp2 = new Expression("UserGroupID", roleId);
                    List<UserGroupLinkModel> lstusergrouplinkmodel = SQLHelper<UserGroupLinkModel>.FindByExpression(exp1.And(exp2));

                    if (lstusergrouplinkmodel.Count == 0)
                    {
                        SQLHelper<UserGroupLinkModel>.Insert(userGroupLinkModel);
                    }
                    else
                    {
                        foreach (UserGroupLinkModel obj in lstusergrouplinkmodel)
                        {
                            if (obj.UserGroupID == roleId && obj.UserID == ID) continue;
                            else
                            {
                                SQLHelper<UserGroupLinkModel>.Insert(userGroupLinkModel);
                            }
                        }
                    }
                }
            }
            LoadUser2Data();
        }


    }
}
