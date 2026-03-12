using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmProjectEmployee : _Forms
    {
        //public int projectID = 0;
        List<int> listID = new List<int>();
        DataTable dtData = new DataTable();
        // Khai báo thêm hàm 
        DataTable dtDataMaster = new DataTable();

        public ProjectModel project = new ProjectModel();

        public bool isAddEmployee = false;
        public frmProjectEmployee()
        {
            InitializeComponent();
        }

        private void frmProjectEmployee_Load(object sender, EventArgs e)
        {
            loadProject();
            loadEmployee();
            loadProjectType();
            cboStatus.SelectedIndex = 1;
            loadData();
            loadPermission();

            loadDataMaster();
            loadProjectTypeMaster();
            loadEmployeeMaster();

        }

        bool loadPermission()
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectEmployeePermisstion", "A",
                                                        new string[] { "@ProjectID", "@EmployeeID" },
                                                        new object[] { project.ID, Global.EmployeeID });

            int valueRow = TextUtils.ToInt(dt.Rows[0]["RowNumber"]);
            //Khánh thêm isadmin
            if (project.ProjectManager == Global.EmployeeID || project.UserID == Global.UserID || Global.IsAdmin || valueRow > 0 || project.UserTechnicalID == Global.UserID)
            {
                btnSave.Enabled = btnSaveAndClose.Enabled = true;
                return true;
            }
            else
            {
                btnSave.Enabled = btnSaveAndClose.Enabled = false;
                return false;
            }
        }

        void loadDataMaster()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int isDeleted = cboStatus.SelectedIndex - 1;

            dtDataMaster = SQLHelper<ProjectEmployeeModel>.LoadDataFromSP("spGetProjectEmployee",
                                            new string[] { "@ProjectID", "@IsDeleted" },
                                            new object[] { projectID, isDeleted });

            grdMain.DataSource = dtDataMaster;
        }

        void loadData()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            //int isDeleted = cboStatus.SelectedIndex - 1;

            dtData = SQLHelper<ProjectEmployeeModel>.LoadDataFromSP("spGetProjectParticipant",
                                            new string[] { "@ProjectID" },
                                            new object[] { projectID });

            grdData.DataSource = dtData;
        }

        void loadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll();

            cboProject.Properties.DataSource = list;
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";

            cboProject.EditValue = project.ID;
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });

            cboEmployee.DataSource = dt;
            cboEmployee.ValueMember = "ID";
            cboEmployee.DisplayMember = "FullName";
        }

        void loadProjectType()
        {
            List<ProjectTypeModel> list = SQLHelper<ProjectTypeModel>.FindAll();

            cboProjectType.DataSource = list;
            cboProjectType.ValueMember = "ID";
            cboProjectType.DisplayMember = "ProjectTypeName";

            cboProjectTypeTree.DataSource = list;
            cboProjectTypeTree.ValueMember = "ID";
            cboProjectTypeTree.DisplayMember = "ProjectTypeName";
        }

        void loadEmployeeMaster()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });

            cboEmployeeMaster.DataSource = dt;
            cboEmployeeMaster.ValueMember = "ID";
            cboEmployeeMaster.DisplayMember = "FullName";


            //Mặc định lọc những hạng mục chưa làm hoặc đang làm
            string filterString = $"([Status] = 0)";
            gridView4.Columns["Status"].FilterInfo = new ColumnFilterInfo(filterString);
        }

        void loadProjectTypeMaster()
        {
            List<ProjectTypeModel> list = SQLHelper<ProjectTypeModel>.FindAll();

            cboProjectTypeMaster.DataSource = list;
            cboProjectTypeMaster.ValueMember = "ID";
            cboProjectTypeMaster.DisplayMember = "ProjectTypeName";

            cboProjectTypeTreeMaster.DataSource = list;
            cboProjectTypeTreeMaster.ValueMember = "ID";
            cboProjectTypeTreeMaster.DisplayMember = "ProjectTypeName";
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void grdMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                GridHitInfo info = grvMain.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    DataTable dt = (DataTable)grdMain.DataSource;
                    DataRow row = dt.NewRow();
                    row["Rownumber"] = grvMain.RowCount + 1;
                    dt.Rows.Add(row);

                    grdMain.DataSource = dt;
                    grvMain.FocusedRowHandle = grvData.RowCount - 1;
                    grvMain.FocusedColumn = colEmployeeID;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                dtDataMaster.AcceptChanges();
                loadData();
                loadDataMaster();
            }
        }

        bool save()
        {
            grvMain.CloseEditor();
            if (!validate()) return false;
            

            for (int i = 0; i < grvMain.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvMain.GetRowCellValue(i, "ID"));
                ProjectEmployeeModel model = new ProjectEmployeeModel();
                if (id > 0)
                {
                    model = SQLHelper<ProjectEmployeeModel>.FindByID(id);
                }


                model.STT = TextUtils.ToInt(grvMain.GetRowCellValue(i, colSTT));
                model.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                model.EmployeeID = TextUtils.ToInt(grvMain.GetRowCellValue(i, colEmployeeIDMaster));
                model.ProjectTypeID = TextUtils.ToInt(grvMain.GetRowCellValue(i, colProjectTypeIDMaster));
                model.ReceiverID = TextUtils.ToInt(grvMain.GetRowCellValue(i, colReceiverIDMaster));
                model.IsLeader = TextUtils.ToBoolean(grvMain.GetRowCellValue(i, colIsLeaderMaster));
                model.Note = TextUtils.ToString(grvMain.GetRowCellValue(i, colNoteMaster));

                if (model.ID > 0)
                {
                    SQLHelper<ProjectEmployeeModel>.Update(model);
                }
                else
                {
                    SQLHelper<ProjectEmployeeModel>.Insert(model);
                }

                //Kiểm tra người ban giao đã có trong bảng chưa
                if (model.ReceiverID > 0)
                {
                    var exp1 = new Expression("ProjectID", model.ProjectID);
                    var exp2 = new Expression("EmployeeID", model.ReceiverID);
                    var exp3 = new Expression("IsDeleted", 1, "<>");

                    var projectEmployees = SQLHelper<ProjectEmployeeModel>.FindByExpression(exp1.And(exp2).And(exp3)).ToList();
                    if (projectEmployees.Count <= 0)
                    {
                        string filterString = $"([IsDeleted] = Unchecked";
                        grvMain.Columns["IsDeleted"].FilterInfo = new ColumnFilterInfo(filterString);

                        model.STT = TextUtils.ToInt(grvMain.GetRowCellValue(grvData.RowCount - 1, colRownumber)) + 1;
                        model.EmployeeID = model.ReceiverID;
                        model.ReceiverID = 0;
                        model.IsLeader = false;
                        SQLHelper<ProjectEmployeeModel>.Insert(model);
                    }
                }

            }

            if (listID.Count > 0)
            {
                string id = string.Join(",", listID);
                string sql = $"UPDATE ProjectEmployee SET IsDeleted = 1 WHERE ID IN ({id})";
                SQLHelper<ProjectEmployeeModel>.ExcuteNonQuerySQL(sql);
                listID.Clear();
            }

            return true;
        }

        bool validate()
        {
            for (int i = 0; i < grvMain.RowCount; i++)
            {

                int employeeID = TextUtils.ToInt(grvMain.GetRowCellValue(i, colEmployeeIDMaster));
                int projectTypeID = TextUtils.ToInt(grvMain.GetRowCellValue(i, colProjectTypeIDMaster));

                if (employeeID <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Nhân viên dòng [{i + 1}]!");
                    grvMain.FocusedColumn = colEmployeeIDMaster;
                    grvMain.FocusedRowHandle = i;
                    return false;
                }

                if (projectTypeID <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Kiểu dự án dòng [{i + 1}]!");
                    grvMain.FocusedColumn = colProjectTypeIDMaster;
                    grvMain.FocusedRowHandle = i;
                    return false;
                }
            }

            return true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            loadDataMaster();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
            loadDataMaster();
            loadPermission();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMain.GetFocusedRowCellValue(colID));

            string fullName = TextUtils.ToString(grvMain.GetFocusedRowCellDisplayText(colEmployeeID));

            string message = id > 0 ? $" nhân viên [{fullName}] khỏi dự án " : " ";
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn xoá{message}không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                listID.Add(id);
                grvMain.DeleteSelectedRows();

                for (int i = 0; i < grvData.RowCount; i++)
                {
                    grvMain.SetRowCellValue(i, colRownumber, i + 1);
                }
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
            loadDataMaster();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void grvMain_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {

        }
        private void grvMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colEmployeeIDMaster)
                {
                    bool isDeleted = TextUtils.ToBoolean(grvMain.GetRowCellValue(e.RowHandle, colIsDeletedMaster));
                    if (isDeleted)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void grvMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            bool isDeleted = TextUtils.ToBoolean(grvMain.GetRowCellValue(grvMain.FocusedRowHandle, colIsDeletedMaster));
            if (isDeleted) e.Cancel = true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (save())
            {
                dtDataMaster.AcceptChanges();
                this.FormClosing -= frmProjectEmployee_FormClosing;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmProjectEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (loadPermission())
            {
                grvMain.FocusedRowHandle = -1;
                dtDataMaster = (DataTable)grdMain.DataSource;

                var dataChange = dtDataMaster.GetChanges();

                if (dataChange != null)
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        btnSaveAndClose_Click(null, null);

                    }
                    else if (dialog == DialogResult.No)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grdData_Click_1(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                int[] selectedRows = grvData.GetSelectedRows();
                if (selectedRows.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để thêm!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                DataTable dtCheck = SQLHelper<ProjectEmployeeModel>.LoadDataFromSP("spGetProjectEmployee",
                                                new string[] { "@ProjectID", "@IsDeleted" },
                                                new object[] { TextUtils.ToInt(cboProject.EditValue), 0 });
                DataTable dt = dtDataMaster.Copy();

                foreach (int i in selectedRows)
                {
                    if (i < 0) continue;
                    string emName = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colEmployeeID));
                    DataRow row = grvData.GetDataRow(i);
                    if (row != null)
                    {
                        bool exists = dtCheck.AsEnumerable().Any(x => x.Field<int>("EmployeeID") == TextUtils.ToInt(row["EmployeeID"]));

                        if (exists) continue;
                        dt.ImportRow(row);
                        dt.Rows[dt.Rows.Count - 1]["Rownumber"] = dt.Rows.Count;
                    }

                }
                if (dt.Rows.Count > 0)
                {
                    grdMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
    }
}
