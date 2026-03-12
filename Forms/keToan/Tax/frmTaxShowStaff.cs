using BMS.Business;
using BMS.Model;
using BMS.Utils;
using Forms.Employee;
using Forms.Employee.UserDetail;
using Forms.keToan.Tax.TaxUserDetail;
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
    public partial class frmTaxShowStaff : _Forms
    {
        public TaxEmployeeModel Model = new TaxEmployeeModel();
        public frmTaxShowStaff()
        {
            InitializeComponent();
        }

        private void frmTaxShowStaff_Load(object sender, EventArgs e)
        {
            loadUserContract();
            loadUserOther();
            loadUserSalary();
            loadUserContact();
            loadTinhTrangHonNhan();
            loadTeam();
            loadDepartment();
            loadChucVuThue();
            loadCongTy();
            loadImage();
            if (Model.ID != 0)
            {

                dtpBirthOfDate.EditValue = Model.BirthOfDate;
                txtIDchamcong.Text = Model.IDChamCongMoi;
                txtCode.Text = Model.Code;
                cboTaxDepartment.SelectedValue = Model.TaxDepartmentID; ///-----------------------03
                txtQuoctich.Text = Model.QuocTich;
                cbTaxChucvu.EditValue = Model.TaxEmployeePositionID; ///---------------------------01
                cbTaxCompany.EditValue = Model.TaxCompanyID; ///------------------------------02
                txtFullName.Text = Model.FullName;
                txtQuoctich.Text = Model.QuocTich;
                txtHomeAddress.Text = Model.HomeAddress;
                txtTongiao.Text = Model.TonGiao;
                txtNoisinh.Text = Model.NoiSinh;
                txtDantoc.Text = Model.DanToc;
                txtQualifications.Text = Model.Qualifications;
                cboSex.SelectedIndex = Model.GioiTinh;
                cbTinhtrangHN.SelectedValue = Model.TinhTrangHonNhanID;
                cboStatus.SelectedIndex = Model.Status;

                lookUpEdit1.EditValue = Model.UserGroupID;
                cboTeam.SelectedValue = Model.TeamID;
                txtDiadiemlv.Text = Model.DiaDiemLamViec;
                txtDVBHXH.Text = Model.DvBHXH;
                dtpStartWorking.EditValue = Model.StartWorking;

                if (txtLoginName.Text != "")
                {
                    //chkHasUser.Checked = true;
                }
            }
            else
            {
                cboStatus.SelectedIndex = 0;
            }
        }

        ucTaxUserContract _ucTaxUserContract = new ucTaxUserContract();
        ucTaxUserOther _ucTaxUserOther = new ucTaxUserOther();
        ucTaxUserSalary _ucTaxUserSalary = new ucTaxUserSalary();
        ucTaxUserContact _ucTaxUserContact = new ucTaxUserContact();

        void loadUserContract()
        {
            //ucUserContract ucUserContract = new ucUserContract();
            tabPageUserContractNew.Controls.Add(_ucTaxUserContract);
            _ucTaxUserContract.Dock = DockStyle.Fill;
            _ucTaxUserContract.loadCb();
            _ucTaxUserContract.loadData(ref Model);

        }
        string ImagePath;
        void loadImage()
        {

            try
            {
                PbImage.Image = Image.FromFile($"{Model.AnhCBNV}");
            }
            catch (Exception)
            {

                return;
            }

        }
        void loadUserOther()
        {
            //ucUserOther ucUserOther = new ucUserOther();
            tabPageUserOther.Controls.Add(_ucTaxUserOther);
            _ucTaxUserOther.Dock = DockStyle.Fill;
            _ucTaxUserOther.loadData(ref Model);
        }
        void loadUserSalary()
        {
            TabUserSalary.Controls.Add(_ucTaxUserSalary);
            _ucTaxUserSalary.Dock = DockStyle.Fill;
            _ucTaxUserSalary.loadData(ref Model);
        }
        void loadUserContact()
        {
            tabPageUserOtherNew.Controls.Add(_ucTaxUserContact);
            _ucTaxUserContact.Dock = DockStyle.Fill;
            _ucTaxUserContact.loadData(ref Model);
        }

        void loadDepartment()
        {
            DataTable tbl = TextUtils.Select(@"SELECT ID,Name FROM TaxDepartment WITH(NOLOCK) ORDER BY ID");
            if (tbl == null)
            {
                return;
            }
            TextUtils.PopulateCombo(cboTaxDepartment, tbl.Copy(), "Name", "ID", "-- Phòng ban --");
            DataTable tblPerson = TextUtils.Select("Select ID, Code, Name from UserGroup a with(nolock)");
            TextUtils.PopulateCombo(lookUpEdit1, tblPerson.Copy(), "Name", "ID", "-- Chọn nhóm --");
        }
        void loadChucVuThue()
        {

            DataTable dt = TextUtils.Select("SELECT * FROM TaxEmployeePosition");
            cbTaxChucvu.Properties.DataSource = dt;
            cbTaxChucvu.Properties.ValueMember = "ID";
            cbTaxChucvu.Properties.DisplayMember = "Name";
        }
        void loadCongTy()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM TaxCompany");
            cbTaxCompany.Properties.DataSource = dt;
            cbTaxCompany.Properties.ValueMember = "ID";
            cbTaxCompany.Properties.DisplayMember = "Name";
        }
        void loadTinhTrangHonNhan()
        {

            DataTable dt = TextUtils.Select("SELECT * FROM EmployeeTinhTrangHonNhan");
            TextUtils.PopulateCombo(cbTinhtrangHN, dt.Copy(), "Name", "ID", "-- Tình trạng hôn nhân --");
        }


        void loadTeam()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM UserTeam");
            TextUtils.PopulateCombo(cboTeam, dt, "Name", "ID", "");
        }

        private bool ValidateForm()
        {
            DataTable dt = new DataTable();
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                if (Model.ID > 0)
                {
                    dt = TextUtils.Select("select Code from TaxEmployee where Code = '" + txtCode.Text.Trim() + "' and ID <> " + Model.ID);
                }
                else
                {
                    dt = TextUtils.Select("select Code from TaxEmployee where Code = '" + txtCode.Text.Trim() + "'");
                }

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã nhân viên này đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            if (txtFullName.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Tên nhân viên.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtIDchamcong.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập ID chấm công.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                if (Model.ID > 0)
                {
                    dt = TextUtils.Select($"select IDChamCongMoi from TaxEmployee where IDChamCongMoi = '{txtIDchamcong.Text.Trim()}' and ID <> {Model.ID}");
                }
                else
                {
                    dt = TextUtils.Select($"select IDChamCongMoi from TaxEmployee where IDChamCongMoi = '{txtIDchamcong.Text.Trim()}'");
                }

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("ID chấm công này đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }

            if (TextUtils.ToInt(cboTaxDepartment.SelectedValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Phòng ban!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(cbTaxChucvu.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Chức vụ nhân viên thuế!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(cbTaxCompany.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn công ty!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtDVBHXH.Text))
            {
                MessageBox.Show("Vui lòng nhập Đơn vị bảo hiểm xã hội!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(dtpBirthOfDate.Text))
            {
                MessageBox.Show("Vui lòng nhập Ngày sinh!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtNoisinh.Text))
            {
                MessageBox.Show("Vui lòng nhập Nơi sinh!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboSex.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Giới tính!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtDantoc.Text))
            {
                MessageBox.Show("Vui lòng nhập Dân tộc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtTongiao.Text))
            {
                MessageBox.Show("Vui lòng nhập Tôn giáo!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtQuoctich.Text))
            {
                MessageBox.Show("Vui lòng nhập Quốc tịch!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cbTinhtrangHN.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Tình trạng hôn nhân!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtDiadiemlv.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa điểm làm việc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!TextUtils.ToDate4(dtpStartWorking.EditValue).HasValue)
            {
                MessageBox.Show("Vui lòng nhập Ngày bắt đầu làm việc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                Model.BirthOfDate = (DateTime?)dtpBirthOfDate.EditValue;
                Model.NoiSinh = txtNoisinh.Text.Trim();
                Model.DanToc = txtDantoc.Text.Trim();
                Model.TonGiao = txtTongiao.Text.Trim();
                Model.QuocTich = txtQuoctich.Text.Trim();
                Model.Code = txtCode.Text.Trim();
                Model.TaxDepartmentID = TextUtils.ToInt(cboTaxDepartment.SelectedValue);
                Model.FullName = TextUtils.ToString(txtFullName.Text);
                Model.HomeAddress = TextUtils.ToString(txtHomeAddress.Text);
                Model.TaxEmployeePositionID = TextUtils.ToInt(cbTaxChucvu.EditValue);
                Model.TaxCompanyID = TextUtils.ToInt(cbTaxCompany.EditValue);

                if (Model.IDChamCongMoi != txtIDchamcong.Text)
                {
                    Model.IDChamCongCu = Model.IDChamCongMoi;
                }
                Model.IDChamCongMoi = TextUtils.ToString(txtIDchamcong.Text);
                Model.Qualifications = txtQualifications.Text.Trim();
                Model.GioiTinh = cboSex.SelectedIndex;
                Model.UserGroupID = TextUtils.ToInt(lookUpEdit1.EditValue);
                Model.TeamID = TextUtils.ToInt(cboTeam.SelectedValue);
                Model.TinhTrangHonNhanID = TextUtils.ToInt(cbTinhtrangHN.SelectedValue);
                Model.DiaDiemLamViec = txtDiadiemlv.Text.Trim();
                Model.ImagePath = ImagePath;
                Model.DvBHXH = TextUtils.ToString(txtDVBHXH.Text);
                Model.RoleID = 2;

                Model.StartWorking = TextUtils.ToDate5(dtpStartWorking.EditValue);

                try
                {
                    if (!_ucTaxUserContract.save(ref Model)) return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Form thông tin hợp đồng  bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;

                }
                try
                {
                    _ucTaxUserOther.save(ref Model);
                }
                catch (Exception)
                {
                    MessageBox.Show("Form thông tin liên hệ bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                try
                {
                    if (!_ucTaxUserSalary.save(ref Model)) return;

                }
                catch (Exception)
                {
                    MessageBox.Show("Form thông tin lương bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                try
                {
                    if (!_ucTaxUserContact.save(ref Model)) return;

                }
                catch (Exception)
                {
                    MessageBox.Show("Form check list hồ sơ bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                Model.Status = cboStatus.SelectedIndex;

                if (Model.ID == 0)
                {
                    Model.CreatedDate = TextUtils.GetSystemDate();
                    Model.CreatedBy = Global.AppUserName;
                    Model.UpdatedDate = Model.CreatedDate;
                    Model.UpdatedBy = Global.AppUserName;
                    Model.ID = TextUtils.ToInt(UsersBO.Instance.Insert(Model));

                    //MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    Model.UpdatedDate = TextUtils.GetSystemDate();
                    Model.UpdatedBy = Global.AppUserName;

                    UsersBO.Instance.Update(Model);

                    if (Model.UserID > 0)
                    {
                        UsersModel usersModel = (UsersModel)UsersBO.Instance.FindByPK(Model.UserID);
                        usersModel.Status = cboStatus.SelectedIndex;
                        UsersBO.Instance.Update(usersModel);
                    }
                }

                UpdateContract(Model.ID);

                this.DialogResult = DialogResult.OK;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// sửa lại sau
        /// </summary>
        /// <param name="employeeId"></param>
        void UpdateContract(int taxEmployeeId)
        {
            int contractType = TextUtils.ToInt(_ucTaxUserContract.cboLoaiHD.SelectedValue);
            var exp1 = new Expression("TaxEmployeeID", taxEmployeeId);
            var exp2 = new Expression("EmployeeLoaiHDLDID", contractType);
            var exp3 = new Expression("IsDelete", 1, "<>");

            var contracts = SQLHelper<TaxEmployeeContractModel>.FindByExpression(exp1.And(exp2).And(exp3));
            TaxEmployeeContractModel contract = contracts.FirstOrDefault();
            contract = contract == null ? new TaxEmployeeContractModel() : contract;

            contract.TaxEmployeeID = taxEmployeeId;
            contract.EmployeeLoaiHDLDID = contractType;
            contract.DateStart = TextUtils.ToDate4(_ucTaxUserContract.dtpDateStart.EditValue);
            contract.DateEnd = TextUtils.ToDate4(_ucTaxUserContract.dtpDateEnd.EditValue);
            contract.ContractNumber = _ucTaxUserContract.txtSoHD.Text.Trim();
            contract.StatusSign = _ucTaxUserContract.rdChuaky.Checked ? 1 : 2;
            contract.DateSign = TextUtils.ToDate4(_ucTaxUserContract.dtpDateSign.EditValue);
            if (contract.ID > 0)
            {
                SQLHelper<TaxEmployeeContractModel>.Update(contract);
            }
            else
            {
                contract.STT = contracts.Count > 0 ? contracts.Max(x => x.STT) + 1 : 1;
                SQLHelper<TaxEmployeeContractModel>.Insert(contract);
            }

        }

        private void TabControlUser_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (TabControlUser.SelectedTabPage == tabPageUserContractNew)
            {
                loadUserContract();
            }
        }

        private void txtPasswordHash_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!txtPasswordHash.UseSystemPasswordChar)
            {

                txtPasswordHash.UseSystemPasswordChar = true;
            }
            else
            {

                txtPasswordHash.UseSystemPasswordChar = false;
            }
        }

        private void PbImage_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files |*.png;*.jpge;*.jpeg";
            //openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Model.AnhCBNV = openFile.FileName;
                ImagePath = openFile.FileName;
                loadImage();
            }
        }

        private void btnNewChucVu_Click(object sender, EventArgs e)
        {
            frmTaxEmployeePositionDetail frm = new frmTaxEmployeePositionDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadChucVuThue();
            }
        }

        private void btnNewCongTy_Click(object sender, EventArgs e)
        {
            frmTaxCompanyDetail frm = new frmTaxCompanyDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCongTy();
            }
        }

        private void btnTeam_Click(object sender, EventArgs e)
        {
            frmUserTeamDetail frm = new frmUserTeamDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadTeam();
            }
        }

        private void frmTaxShowStaff_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
