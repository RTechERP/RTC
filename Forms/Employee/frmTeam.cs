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
    public partial class frmTeam : _Forms
    {
        public frmTeam()
        {
            InitializeComponent();
        }

        private void frmTeam_Load(object sender, EventArgs e)
        {
            loadTeamUser();
            loadUser();
            loadUserTeamLink();
        }

        /// <summary>
        /// Load team
        /// </summary>
        void loadTeamUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetUserTeam", "A",
                new string[] { },
                new object[] { });
            grdMaster.DataSource = dt;
        }

        /// <summary>
        /// Load User lên combobox
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID, Code, FullName FROM Users ORDER BY ID DESC");
            cboUser.DisplayMember = "FullName";
            cboUser.ValueMember = "ID";
            cboUser.DataSource = dt;
        }

        /// <summary>
        /// Load User Team Link
        /// </summary>
        void loadUserTeamLink()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetUserTeamLink", "A",
               new string[] { "@UserTeamID" },
               new object[] { id });
            //DataTable dt = TextUtils.Select($"select * from UserTeamLink where UserTeamID = {id} order by ID desc");
            grdUserTeamLink.DataSource = dt;
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadUserTeamLink();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmUserTeamDetail frm = new frmUserTeamDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadTeamUser();
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));

            if (ID <= 0)
            {
                return;
            }
            UserTeamModel model = (UserTeamModel)UserTeamBO.Instance.FindByPK(ID);
            frmUserTeamDetail frm = new frmUserTeamDetail();
            //frm.userTeam = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadTeamUser();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //string groupFile = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colGroupFileCode));
            string teamName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colName));
            if (MessageBox.Show(string.Format("Bạn có muốn xóa team [{0}] hay không ?", teamName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserTeamBO.Instance.Delete(ID);
                grvMaster.DeleteSelectedRows();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            frmChooseEmployee frm = new frmChooseEmployee();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<int> lstEmployeeID = frm.LstID;
                if (lstEmployeeID.Count == 0) return;
                int masterID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                if (masterID == 0) return;

                string lstID = string.Join(";", lstEmployeeID);

                TextUtils.ExcuteProcedure("spAddUserToUserTeamLink", new string[] { "@ListUserID", "@UserTeamID" }, new object[] { lstID, masterID });
                loadUserTeamLink();
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (grvUserTeamLink.FocusedRowHandle < 0)
                return;

            int strID = TextUtils.ToInt(grvUserTeamLink.GetFocusedRowCellValue(colIDUserTeamLink));

            //string strName = TextUtils.ToString(grvUserTeamLink.GetFocusedRowCellValue("FullName"));

            if (MessageBox.Show("Bạn có chắc muốn xóa người dùng này không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    UserTeamLinkBO.Instance.Delete(strID);
                    grvUserTeamLink.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        private void frmTeam_SizeChanged(object sender, EventArgs e)
        {
            int width = splitContainerControl1.Width;
            splitContainerControl1.Panel1.MinSize = (width * 60) / 100;
            splitContainerControl1.Panel2.MinSize = (width * 40) / 100;
        }

        private void grdUserTeamLink_Load(object sender, EventArgs e)
        {
            grvUserTeamLink.OptionsSelection.MultiSelect = true;
            grvUserTeamLink.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }
    }
}
