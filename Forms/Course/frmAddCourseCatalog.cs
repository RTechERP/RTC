using BMS;
using BMS.Business;
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

namespace BMS
{
    public partial class frmAddCourseCatalog : _Forms
    {
        public CourseCatalogModel Group = new CourseCatalogModel();
        int flag;

        public frmAddCourseCatalog()
        {
            InitializeComponent();
        }
        private bool ValidateForm()
        {
            if (TextUtils.ToInt(cbxDepartment.SelectedValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Phòng ban", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeCourseCatalog.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã danh mục!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCodeCourseCatalog.Text.Trim());
                var exp2 = new Expression("ID", Group.ID, "<>");

                var catalog = SQLHelper<CourseCatalogModel>.FindByExpression(exp1.And(exp2));
                if (catalog.Count > 0)
                {
                    MessageBox.Show($"Mã khoá học [{ txtCodeCourseCatalog.Text.Trim()}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtCourseCatalogName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên danh mục!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboCatalogType.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //=========================================== lee min khooi update 01/10/2024 ======================================================
            //if (string.IsNullOrEmpty(cboPosition.EditValue.ToString()))
            //{
            //    MessageBox.Show("Vui lòng Chọn ít nhất 1 Team!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            return true;

        }

        private void frmAddCourseCatalog_Load(object sender, EventArgs e)
        {
            cboCatalogType.SelectedIndex = 0;

            LoadDepartment();
            LoadPosition();

            loadData();
            btnSave.Enabled = btnSaveAndClose.Enabled = ValidateUser();
        }

        void LoadDepartment()
        {
            List<DepartmentModel> listDepartments = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();

            cbxDepartment.DataSource = listDepartments;
            cbxDepartment.DisplayMember = "Name";
            cbxDepartment.ValueMember = "ID";
            cbxDepartment.SelectedValue = Global.DepartmentID;
        }
        //Load data cho sửa
        private void loadData()
        {
            if (Group.ID > 0)
            {
                txtCodeCourseCatalog.Text = Group.Code.ToString();
                txtCourseCatalogName.Text = Group.Name;
                cbxDepartment.SelectedValue = Group.DepartmentID;
                chkDeleteFlag.Checked = Group.DeleteFlag;
                txtSTT.Value = Group.STT;
                cboCatalogType.SelectedIndex = Group.CatalogType;

                //=========================================== lee min khooi update 01/10/2024 ======================================================
                //var value = cboPosition.EditValue;
                var lstDetails = SQLHelper<CourseCatalogProjectTypeModel>.FindByAttribute("CourseCatalogID", Group.ID)
                                                                                                          .Select(p => p.ProjectTypeID)
                                                                                                          .ToList();
                string value = string.Join(",", lstDetails);
                cboPosition.SetEditValue(value);
            }
            else
            {
                var listCatalogs = SQLHelper<CourseCatalogModel>.FindAll();
                //int stt = TextUtils.ToInt(TextUtils.ExcuteScalar("SELECT TOP 1 STT FROM dbo.CourseCatalog ORDER BY STT DESC"));
                int stt = listCatalogs.Count <= 0 ? 0 : listCatalogs.Max(x => x.STT);

                txtSTT.Value = stt + 1;
                chkDeleteFlag.Checked = true;
                txtCodeCourseCatalog.Text = "";
                txtCourseCatalogName.Text = "";
            }
        }

        private void frmAddCourseCatalog_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
		/// void lưu dữ liệu thêm mới or sửa
		/// </summary>
		private void SaveGroup()
        {

            if (!ValidateForm())
            {
                frmAddCourseCatalog frmAddCourseCatalog = new frmAddCourseCatalog();
                return;
            }
            Group.Code = txtCodeCourseCatalog.Text.Trim();
            Group.Name = txtCourseCatalogName.Text;
            Group.DepartmentID = TextUtils.ToInt(cbxDepartment.SelectedValue);
            Group.DeleteFlag = chkDeleteFlag.Checked;
            Group.STT = TextUtils.ToInt(txtSTT.Value);
            Group.CatalogType = cboCatalogType.SelectedIndex;

            if (Group.ID > 0)
            {
                SQLHelper<CourseCatalogModel>.Update(Group);

            }
            else
            {
                Group.ID = SQLHelper<CourseCatalogModel>.Insert(Group).ID;
                txtCodeCourseCatalog.Text = "";
                txtCourseCatalogName.Text = "";
            }

            //=========================================== lee min khooi update 01/10/2024 ======================================================
            SQLHelper<CourseCatalogProjectTypeModel>.DeleteByAttribute("CourseCatalogID", Group.ID);
            string lstSrtIDs = TextUtils.ToString(cboPosition.EditValue);
            string[] lstProjectTypeIDs = lstSrtIDs.Split(',');
            foreach (string id in lstProjectTypeIDs)
            {
                int projectTypeID = TextUtils.ToInt(id);
                if (projectTypeID <= 0) continue;
                CourseCatalogProjectTypeModel model = new CourseCatalogProjectTypeModel();
                model.CourseCatalogID = Group.ID;
                model.ProjectTypeID = projectTypeID;
                SQLHelper<CourseCatalogProjectTypeModel>.Insert(model);
            }



            if (flag == 1) this.DialogResult = DialogResult.OK;
        }

        //Cất và đóng
        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            flag = 1;
            SaveGroup();
        }

        //Cất và thêm mới
        private void btnSave_Click(object sender, EventArgs e)
        {
            flag = 0;
            SaveGroup();
            Group = new CourseCatalogModel();

            loadData();
        }
        //=================== lee min khooi update 12/08/2024 ===============================================
        private bool ValidateUser()
        {
            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) != Group.CreatedBy && Group.ID > 0;
            if (isProSen && isCreated)
            {
                //MessageBox.Show("Bạn không thể cập nhật !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        //================= end update 12/09/2024 =============================================================

        //=========================================== lee min khooi update 01/10/2024 ======================================================
        private void LoadPosition()
        {
            List<ProjectTypeModel> list = SQLHelper<ProjectTypeModel>.ProcedureToList("spGetallProjectType", new string[] { }, new object[] { });

            cboPosition.Properties.DataSource = list;
            cboPosition.Properties.ValueMember = "ID";
            cboPosition.Properties.DisplayMember = "ProjectTypeName";
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }
    }
}
