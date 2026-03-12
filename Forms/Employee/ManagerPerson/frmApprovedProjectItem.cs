using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using Forms.DanhMuc.DuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

using DevExpress.Utils;

namespace BMS
{
    public partial class frmApprovedProjectItem : _Forms
    {
        public frmApprovedProjectItem()
        {
            InitializeComponent();
        }

        private void frmApprovedProjectItem_Load(object sender, EventArgs e)
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            dtpDateEnd.Value = date.AddMonths(+1).AddDays(-1);

            loadProject();
            loadTeam();
            loadUser();
            loadDepartment();
            loadApprovedLate();
            loadItemLate();
            LoadEmployeeRequest();
            LoadEmployeeRequestName();
            loadData();
        }

        void loadData()
        {
            grdData.DataSource = null;
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            string isApproved = TextUtils.ToString(cboIsApproved.EditValue);
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int userID = TextUtils.ToInt(cboUser.EditValue);
            int teamID = TextUtils.ToInt(cboTeam.EditValue);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            int IsApprovedLate = TextUtils.ToInt(cbApprovedLate.SelectedValue);
            int IsLateActual = TextUtils.ToInt(cbItemLate.SelectedValue);

            int employeeIdRequest = TextUtils.ToInt(cboEmployeeRequest.EditValue);
            int employeeRequestID = TextUtils.ToInt(cboEmployeeRequestID.EditValue);
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectItemApproved", "A",
                                        new string[] { "@DateStart", "@DateEnd", "@IsApproved", "@ProjectID", "@TeamID", "@UserID", "@Keyword", "@DepartmentID", "@IsApprovedLate", "@IsLateActual", "@EmployeeIDRequest", "@EmployeeRequestID" },
                                        new object[] { dateStart, dateEnd, isApproved, projectID, teamID, userID, txtKeyword.Text.Trim(), departmentID, IsApprovedLate, IsLateActual, employeeIdRequest, employeeRequestID });

                grdData.DataSource = dt;

            }
        }

        void loadIsApproved()
        {
            List<Response> listApproved = new List<Response>()
            {
                new Response(){ status = 0, message = "Chờ duyệt kế hoạch"},
                new Response(){ status = 1, message = "Duyệt kế hoạch"},
                new Response(){ status = 2, message = "Chờ duyệt thực tế"},
                new Response(){ status = 3, message = "Duyệt thực tế"},
            };

            cboIsApproved.Properties.DataSource = listApproved;
            cboIsApproved.Properties.ValueMember = "status";
            cboIsApproved.Properties.DisplayMember = "message";
            cboIsApproved.EditValue = "0,2";
        }

        void loadApprovedLate()
        {
            cbApprovedLate.Items.Clear();
            List<Response> listApproved = new List<Response>()
            {
                new Response(){ status = -1, message = "--Tất cả--"},
                new Response(){ status = 0, message = "Chưa duyệt"},
                new Response(){ status = 1, message = "Đã duyệt"},
            };

            cbApprovedLate.DataSource = listApproved;
            cbApprovedLate.ValueMember = "status";
            cbApprovedLate.DisplayMember = "message";
            cbApprovedLate.SelectedIndex = 0;
        }
        void loadItemLate()
        {
            cbItemLate.Items.Clear();
            List<Response> listApproved = new List<Response>()
            {
                new Response(){ status = -1, message = "--Tất cả--"},
                new Response(){ status = 0, message = "Không muộn"},
                new Response(){ status = 1, message = "Muộn"},
            };

            cbItemLate.DataSource = listApproved;
            cbItemLate.ValueMember = "status";
            cbItemLate.DisplayMember = "message";
            cbItemLate.SelectedIndex = 0;
        }

        void loadProject()
        {
            List<ProjectModel> listProject = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = listProject;
        }

        void loadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepartment;

            cboDepartment.EditValue = Global.DepartmentID;
        }
        void loadTeam()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            List<UserTeamModel> listTeam = SQLHelper<UserTeamModel>.FindAll().Where(x => x.DepartmentID == departmentID).ToList();
            cboTeam.Properties.ValueMember = "ID";
            cboTeam.Properties.DisplayMember = "Name";
            cboTeam.Properties.DataSource = listTeam;

            UserTeamModel userTeam = listTeam.FirstOrDefault(x => x.LeaderID == Global.EmployeeID);
            if (userTeam != null)
            {
                cboTeam.EditValue = userTeam.ID;
            }

        }

        void loadUser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.DataSource = dt;
        }

        void LoadEmployeeRequest()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.ProcedureToList("spGetEmployeeRequestProjectItem", new string[] { }, new object[] { });
            cboEmployeeRequest.Properties.ValueMember = "ID";
            cboEmployeeRequest.Properties.DisplayMember = "FullName";
            cboEmployeeRequest.Properties.DataSource = list;


            cboEmployeeRequest.EditValue = Global.EmployeeID;
            if (!list.Any(x => x.ID == Global.EmployeeID)) cboEmployeeRequest.EditValue = 0;

        }


        void LoadEmployeeRequestName()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployeeRequestID.Properties.DataSource = dt;
            cboEmployeeRequestID.Properties.DisplayMember = "Code";
            cboEmployeeRequestID.Properties.ValueMember = "ID";
        }

        void approved(bool isApprove)
        {
            int[] rowSelected = grvData.GetSelectedRows();

            List<int> listIsApproved = new List<int>();
            int id = 0;
            int isApproved = 0;

            string isApproveText = isApprove ? "duyệt" : "huỷ duyệt";
            string message = "";

            if (rowSelected.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn hạng mục công việc muốn {isApproveText}!", "Thông báo");
                return;
            }

            foreach (int row in rowSelected)
            {
                id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (id <= 0) continue;
                isApproved = TextUtils.ToInt(grvData.GetRowCellValue(row, colIsApproved));
                listIsApproved.Add(isApproved);
            }

            bool isApproveActual = listIsApproved.Any(x => x == 1 || x == 2);
            if (isApprove)
            {
                message = $"Bạn có chắc muốn {isApproveText} danh sách hạng mục công việc đã chọn không?\n";
                //$"Hạng mục đang Chờ duyệt duyệt kế hoạch được chuyển thành Duyệt kế hoạch.\n" +
                //$"Hạng mục đang Chờ duyệt duyệt thực tế được chuyển thành Duyệt thực tế.";
            }
            else
            {
                message = $"Bạn có chắc muốn {isApproveText} danh sách hạng mục công việc đã chọn.\n";
                //$"Hạng mục đang Duyệt kế hoạch được chuyển thành Chờ duyệt duyệt kế hoạch.\n" +
                //$"Hạng mục đang Duyệt thực tế được chuyển thành Chờ duyệt duyệt thực tế.";
            }


            if (isApproveActual && isApprove)
            {
                message += $"\nHạng mục công việc chưa có ngày kết thúc thực tế sẽ không được chuyển thành Duyệt thực tế.";
            }

            DialogResult dialog = MessageBox.Show(message, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in rowSelected)
                {
                    id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;
                    isApproved = TextUtils.ToInt(grvData.GetRowCellValue(row, colIsApproved));
                    if (isApprove)
                    {
                        if (isApproved == 0)
                        {
                            isApproved += 1;
                        }
                        else if (isApproved == 1 || isApproved == 2)
                        {
                            DateTime? actualEndDate = TextUtils.ToDate4(grvData.GetRowCellValue(row, colActualEndDate));
                            if (!actualEndDate.HasValue) continue;
                            isApproved = 3;
                        }
                    }
                    else
                    {
                        if (isApproved == 0 || isApproved == 2) continue;
                        isApproved -= 1;
                    }

                    string sql = $"UPDATE dbo.ProjectItem SET IsApproved = {isApproved}, " +
                                    $"UpdatedBy = '{Global.LoginName}', " +
                                    $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                    $"WHERE ID = {id}";

                    TextUtils.ExcuteSQL(sql);
                }
            }

            loadData();
        }

        //void approved(int isApprove)
        //{
        //    List<int> listID = new List<int>();
        //    int[] rowSelected = grvData.GetSelectedRows();
        //    if (rowSelected.Length <= 0)
        //    {
        //        MessageBox.Show("Vui lòng chọn Hạng mục công việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    string approved = isApprove == 0 ? "Huỷ duyệt kế hoạch" : (isApprove == 1 ? "Duyệt kế hoạch" : (isApprove == 2 ? "Huỷ duyệt kế hoạch" : "Duyệt thực tế"));

        //    //Check validate
        //    foreach (int row in rowSelected)
        //    {
        //        int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //        if (id <= 0)
        //        {
        //            continue;
        //        }

        //        int isApproved = TextUtils.ToInt(grvData.GetRowCellValue(row, colIsApproved));
        //        //string approvedText = TextUtils.ToString(grvData.GetRowCellValue(row, colIsApprovedText));
        //        string code = TextUtils.ToString(grvData.GetRowCellValue(row, colCode));

        //        if (isApprove == 1) //Duyệt kế hoạch
        //        {
        //            if (isApproved != 0)
        //            {
        //                //string message = isApproved == 1 ? "đã được duyệt kế hoạch" : (isApproved == 2 ? "đang chờ duyệt thực tế" : (isApproved == 3 ? "đã được duyệt thực tế" : ""));
        //                //MessageBox.Show($"Công việc [{code}] {message}!", "Thông báo");
        //                //return;
        //                continue;
        //            }
        //        }
        //        else if (isApprove == 3) //Duyệt thực tế
        //        {
        //            DateTime? actualEnd = TextUtils.ToDate4(grvData.GetRowCellValue(row, colActualEndDate));
        //            if (!actualEnd.HasValue)
        //            {
        //                //MessageBox.Show($"Công việc [{code}] chưa có [Ngày kết thúc thực tế]!", "Thông báo");
        //                //return;
        //                continue;
        //            }
        //            else if (isApproved != 1 && isApproved != 2)
        //            {
        //                //string message = isApproved == 0 ? "đang chờ duyệt kế hoạch" : (isApproved == 3 ? "đã được duyệt thực tế" : "");
        //                //MessageBox.Show($"Công việc [{code}] {message}!", "Thông báo");
        //                //return;

        //                continue;
        //            }
        //        }
        //        else if (isApprove == 2) //Huỷ duyệt thực tế
        //        {
        //            if (isApproved != 3)
        //            {
        //                //MessageBox.Show($"Công việc [{code}] chưa được duyệt thực tế!", "Thông báo");
        //                //return;
        //                continue;
        //            }
        //        }
        //        else //Huỷ duyệt kế hoạch
        //        {
        //            if (isApproved != 1)
        //            {
        //                string message = isApproved > 1 ? "đã có ngày Kết thúc thực tế.\nKhông thể Huỷ duyệt kế hoạch" : "";
        //                MessageBox.Show($"Công việc [{code}] {message}!", "Thông báo");
        //                return;
        //            }
        //        }
        //    }


        //    string message3 = isApprove == 3 ? "!\nNhững hạng mục chưa có ngày kết thúc thực tế sẽ không được duyệt kế hoạch.\nBạn có muốn tiếp tục không?" : " không?";
        //    DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {approved} hạng mục công việc đã chọn{message3}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (dialog == DialogResult.Yes)
        //    {
        //        foreach (int row in rowSelected)
        //        {
        //            int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //            int isApproved = TextUtils.ToInt(grvData.GetRowCellValue(row, colIsApproved));
        //            if (id <= 0)
        //            {
        //                continue;
        //            }

        //            if (isApprove == 1) //Duyệt kế hoạch
        //            {
        //                if (isApproved != 0)
        //                {
        //                    continue;
        //                }
        //            }
        //            else if (isApprove == 3) //Duyệt thực tế
        //            {
        //                DateTime? actualEnd = TextUtils.ToDate4(grvData.GetRowCellValue(row, colActualEndDate));
        //                if (!actualEnd.HasValue)
        //                {

        //                    continue;
        //                }
        //                else if (isApproved != 1 && isApproved != 2)
        //                {


        //                    continue;
        //                }
        //            }
        //            else if (isApprove == 2) //Huỷ duyệt thực tế
        //            {
        //                if (isApproved != 3)
        //                {

        //                    continue;
        //                }
        //            }
        //            else //Huỷ duyệt kế hoạch
        //            {
        //                if (isApproved != 1)
        //                {
        //                    continue;
        //                }
        //            }

        //            listID.Add(id);
        //        }
        //    }

        //    if (listID.Count > 0)
        //    {
        //        string sql = $"UPDATE dbo.ProjectItem SET IsApproved = {isApprove}, UpdatedBy = '{Global.LoginName}', UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
        //            $"WHERE ID IN ({string.Join(",", listID)})";

        //        TextUtils.ExcuteSQL(sql);
        //        loadData();
        //    }

        //}

        private void btnFind_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            loadData();
            grvData.FocusedRowHandle = rowHandle;
        }

        private void btnApprovedPlan_Click(object sender, EventArgs e)
        {
            //approved(1);
            approved(true);
        }

        private void btnApprovedActual_Click(object sender, EventArgs e)
        {
            //approved(3);
            //approved(true);
        }

        private void btnUnApprovedPlan_Click(object sender, EventArgs e)
        {
            //approved(0);
            approved(false);
        }

        private void btnUnApprovedActual_Click(object sender, EventArgs e)
        {
            //approved(2);
            approved(false);
        }


        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            //btnFind_Click(null, null);
        }

        private void cboTeam_EditValueChanged(object sender, EventArgs e)
        {
            //btnFind_Click(null, null);
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            //btnFind_Click(null, null);
        }

        private void cboIsApproved_EditValueChanged(object sender, EventArgs e)
        {
            //btnFind_Click(null, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int projectId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            if (projectId <= 0)
            {
                return;
            }

            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
            frmHangMucCongViec frm = new frmHangMucCongViec();
            frm.project = project;
            frm.projectItemCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode)); ;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadTeam();
            //loadData();
        }

        private void btnShowProblem_Click(object sender, EventArgs e)
        {
            btnShowItemProblem_Click(null, null);
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            try
            {
                if (e.Column.FieldName == "IsApprovedText")
                {
                    int val1 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "IsApproved"));
                    int val2 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "IsApproved"));
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnApprovedReasonLate_Click(object sender, EventArgs e)
        {
            int[] arrIndex = grvData.GetSelectedRows();
            if (arrIndex.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn Hạng mục công việc!", "Thông báo");
                return;
            }
            foreach (int index in arrIndex)
            {
                int ProjectItemID = TextUtils.ToInt(grvData.GetRowCellValue(index, colID));
                ProjectItemModel model = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(ProjectItemID);
                if (model == null) continue;
                model.IsApprovedLate = true;
                ProjectItemBO.Instance.Update(model);
            }
            //MessageBox.Show("Cập nhật thành công!", "Thông báo");
            loadData();
        }

        private void btnCancelApprovedReasonLate_Click(object sender, EventArgs e)
        {
            int[] arrIndex = grvData.GetSelectedRows();
            if (arrIndex.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn Hạng mục công việc!", "Thông báo");
                return;
            }
            foreach (int index in arrIndex)
            {
                int ProjectItemID = TextUtils.ToInt(grvData.GetRowCellValue(index, colID));
                ProjectItemModel model = (ProjectItemModel)ProjectItemBO.Instance.FindByPK(ProjectItemID);
                if (model == null) continue;
                model.IsApprovedLate = false;
                ProjectItemBO.Instance.Update(model);
            }
            //MessageBox.Show("Cập nhật thành công!", "Thông báo");
            loadData();
        }

        private void btnShowItemProblem_Click(object sender, EventArgs e)
        {
            int itemID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (itemID <= 0)
            {
                return;
            }

            ProjectItemModel model = SQLHelper<ProjectItemModel>.FindByID(itemID);
            frmProjectItemProblem frm = new frmProjectItemProblem();
            frm.projectItem = model;
            frm.ShowDialog();
        }

        private void cbItemLate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void cbApprovedLate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnProjectItemLog_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            frmProjectItemLog frm = new frmProjectItemLog();
            frm.projectItemID = id;
            frm.Text = $"LỊCH SỬ CẬP NHẬT - {code}";
            frm.ShowDialog();
        }
    }
}
