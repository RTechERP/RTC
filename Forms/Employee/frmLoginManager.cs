
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
    public partial class frmLoginManager : _Forms
    {
        public int ID;
        int UserID = 0;
        public UsersModel users = new UsersModel();
        public frmLoginManager()
        {
            InitializeComponent();
        }

        private void frmLoginManager_Load(object sender, EventArgs e)
        {
            loadCode();

            if (ID > 0)
            {
                cbCode.EditValue = ID;
            }

        }

        /// <summary>
        /// Load data vào combobox cbCode và cbName
        /// </summary>
        void loadCode()
        {
            DataTable dt = TextUtils.Select("select ID,UserID,FullName,Code from Employee");
            cbCode.Properties.DataSource = dt;
            cbCode.Properties.DisplayMember = "Code";
            cbCode.Properties.ValueMember = "ID";

            cbName.Properties.DataSource = dt;
            cbName.Properties.DisplayMember = "FullName";
            cbName.Properties.ValueMember = "ID";

        }

        void loadUsser()
        {

            if (cbCode.Text != "")
            {
                try
                {
                    UserID = (int)TextUtils.ExcuteScalar($"Select UserID from Employee where ID={cbCode.EditValue}");
                    if (UserID > 0)
                    {
                        chkHasUser.Checked = true;
                        DataTable dt = TextUtils.Select($"Select LoginName,PasswordHash, Status, TeamID from Users where ID={UserID}");
                        txtLoginName.Text = TextUtils.ToString(dt.Rows[0]["LoginName"]);
                        txtPasswordHash.Text = MD5.DecryptPassword(TextUtils.ToString(dt.Rows[0]["PasswordHash"]));
                        cbTeam.SelectedIndex = TextUtils.ToInt(dt.Rows[0]["TeamID"]);
                        int status = TextUtils.ToInt(dt.Rows[0]["Status"]);
                        if (status == 1)
                        {
                            chkHasUser.Checked = false;
                        }
                        else
                        {
                            chkHasUser.Checked = true;
                        }
                    }

                }
                catch (Exception)
                {

                    txtLoginName.Text = "";
                    txtPasswordHash.Text = "";
                    chkHasUser.Checked = false;
                    UserID = 0;
                    return;
                }

            }
        }

        private void cbCode_EditValueChanged(object sender, EventArgs e)
        {
            if (cbName.Text == "" || cbCode != cbName)
            {
                if (cbCode.Text == "") return;


                cbName.EditValue = cbCode.EditValue;
                loadUsser();
            }
        }

        private void cbName_EditValueChanged(object sender, EventArgs e)
        {
            if (cbCode.Text == "" || cbCode != cbName)
            {
                if (cbName.Text == "") return;


                cbCode.EditValue = cbName.EditValue;

            }
        }

        private void chkHasUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHasUser.Checked == true)
            {
                txtLoginName.Enabled = true;
                txtPasswordHash.Enabled = true;
                //cbTeam.SelectedIndex = 0;


                txtLoginName.Text = cbCode.Text.Trim();
            }
            else
            {
                //cbTeam.SelectedIndex = 1;
                txtLoginName.Enabled = false;
                txtPasswordHash.Enabled = false;
            }
        }

        bool validate()
        {
            if (TextUtils.ToInt(cbCode.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(txtLoginName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else 
            //if (UserID > 0)
            {

                var exp1 = new Expression("LoginName", txtLoginName.Text.Trim().ToLower());
                var exp2 = new Expression("Status", 1,"<>");
                var exp3 = new Expression("ID",UserID,"<>");
                var users = SQLHelper<UsersModel>.FindByExpression(exp1.And(exp2).And(exp3));
                //var users = SQLHelper<UsersModel>.FindAll().Where(x => x.LoginName.ToLower() == txtLoginName.Text.Trim().ToLower() && x.ID != UserID).ToList();
                if (users.Count > 0)
                {
                    MessageBox.Show($"Tên đăng nhập [{txtLoginName.Text}] đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtPasswordHash.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        bool save()
        {

            if (string.IsNullOrEmpty(cbCode.Text))
            {
                MessageBox.Show("Chưa có thông tin nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //UsersModel Model = new UsersModel();
            EmployeeModel employeeModel = new EmployeeModel();
            //if (UserID > 0)
            {
                //users = (UsersModel)UsersBO.Instance.FindByPK(UserID);

                var exp1 = new Expression("LoginName", txtLoginName.Text.Trim());
                var exp2 = new Expression("Status", 1,"<>");
                //users = SQLHelper<UsersModel>.FindByAttribute("LoginName", txtLoginName.Text.Trim()).FirstOrDefault() ?? new UsersModel();
                users = SQLHelper<UsersModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new UsersModel();
                //employeeModel = (EmployeeModel)EmployeeBO.Instance.FindByCode("UserID", users.ID.ToString());
                //employeeModel = SQLHelper<EmployeeModel>.FindByAttribute("UserID", TextUtils.ToString(users.ID)).FirstOrDefault() ?? new EmployeeModel();

                int employeeID = TextUtils.ToInt(cbCode.EditValue);
                employeeModel = SQLHelper<EmployeeModel>.FindByID(employeeID);
            }
            if (chkHasUser.Checked)
            {
                if (!validate()) return false;
                

                users.LoginName = txtLoginName.Text.Trim();
                users.PasswordHash = MD5.EncryptPassword(txtPasswordHash.Text.Trim());
                users.Status = 0;
                users.FullName = cbName.Text;
                employeeModel.Status = 0;
                employeeModel.EndWorking = null;

                //Model.Status = cboStatus.SelectedIndex;
            }
            else
            {
                users.Status = 1;
                employeeModel.Status = 1;
            }
            users.TeamID = cbTeam.SelectedIndex;
            //if (UserID > 0)
            if (users.ID > 0)
            {
                employeeModel.UserID = users.ID;
                UsersBO.Instance.Update(users);
                //EmployeeBO.Instance.Update(employeeModel);
                SQLHelper<EmployeeModel>.Update(employeeModel);
            }
            else
            {

                users.ID = (int)UsersBO.Instance.Insert(users);
                //employeeModel = (EmployeeModel)EmployeeBO.Instance.FindByPK(TextUtils.ToInt(cbCode.EditValue));
                employeeModel = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(cbCode.EditValue));
                employeeModel.UserID = users.ID;
                //EmployeeBO.Instance.Update(employeeModel);
                SQLHelper<EmployeeModel>.Update(employeeModel);
            }

            return true;
        }
        void ClearData()
        {
            cbCode.EditValue = null;
            cbName.EditValue = null;
            txtLoginName.Text = "";
            txtPasswordHash.Text = "";
            chkHasUser.Checked = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                if (ID > 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    ClearData();
                }

            }
        }

        private void txtLoginName_RegionChanged(object sender, EventArgs e)
        {
            DataTable dtUser = TextUtils.Select(string.Format("select * from users where LoginName = '{0}'", txtLoginName.Text.Trim()));
            if (dtUser.Rows.Count > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!");
                return;
            }
        }
    }
}
