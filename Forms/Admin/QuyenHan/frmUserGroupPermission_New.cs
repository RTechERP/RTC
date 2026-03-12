using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using DevExpress.XtraTreeList;
using System.Collections;
using DevExpress.XtraTreeList.Nodes;
using BMS.Business;

namespace BMS
{
    public partial class frmUserGroupPermission_New : _Forms
    {
        DataTable _dtPermission = new DataTable();
        public frmUserGroupPermission_New()
        {
            InitializeComponent();
        }
        public int GroupID = 0;
        private void frmUserGroupPermission_Load(object sender, EventArgs e)
        {
            loadStaffGroup();
        }

        void loadStaffGroup()
        {
            try
            {
                DataTable tbl = TextUtils.Select("Select * from UserGroup a with(nolock)");
                cboUserGroup1.DataSource = tbl;
                cboUserGroup1.DisplayMember = "Name";
                cboUserGroup1.ValueMember = "ID";

                if (GroupID > 0)
                {
                    cboUserGroup1.SelectedValue = GroupID;
                }
            }
            catch
            {
            }
        }

        void loadTree()
        {
            try
            {
                int id = TextUtils.ToInt(cboUserGroup1.SelectedValue) > 0 ? TextUtils.ToInt(cboUserGroup1.SelectedValue) : GroupID;
                _dtPermission = TextUtils.GetDataTableFromSP("spGetGroupPermission", new string[] { "@UserGroupID" }, new object[] { id });

                DataRow row = _dtPermission.NewRow();
                row["ID"] = 0;
                row["Name"] = "--Tất cả các nhóm--";
                _dtPermission.Rows.InsertAt(row, 0);

                treeData.DataSource = _dtPermission;

                treeData.CheckBoxFieldName = "IsCheck";
                treeData.ExpandAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            treeData.CloseEditor();
            if (cboUserGroup1.SelectedIndex < 0) return;
            int userGroupID = TextUtils.ToInt(cboUserGroup1.SelectedValue);

            List<TreeListNode> lstNode = treeData.GetNodeList();            
            foreach (TreeListNode node in lstNode)
            {
                //var a = node.Nodes;

                int id = TextUtils.ToInt(node.GetValue(colIDTree));
                if (id <= 0) continue;
                //Kiểm tra nó với thằng cũ
                bool oldValue = TextUtils.ToBoolean(node.GetValue(colIsCheckOld));
                if (oldValue == node.Checked) continue;

                if (oldValue && !node.Checked)
                {
                    //Xoá
                    int userGroupRightDistributionID = TextUtils.ToInt(node.GetValue(colUserGroupRightDistributionID));
                    UserGroupRightDistributionBO.Instance.Delete(userGroupRightDistributionID);
                }
                else
                {
                    //thêm mới
                    UserGroupRightDistributionModel ug = new UserGroupRightDistributionModel();
                    ug.FormAndFunctionID = id;
                    ug.UserGroupID = userGroupID;
                    UserGroupRightDistributionBO.Instance.Insert(ug);
                }
            }

            loadTree();
            MessageBox.Show("Phân quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void cboUserGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTree();
        }
    }
}
