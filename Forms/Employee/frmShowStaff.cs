using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMS.Utils;
using BMS.Model;
using BMS.Business;
using Forms.Employee.UserDetail;
using System.Threading;
using Forms.Employee;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using Forms.Employee.TeamPhongBan;
using System.Threading.Tasks;

namespace BMS
{
    public partial class frmShowStaff : _Forms
    {

        #region Variables
        // public UsersModel Model = new UsersModel();

        public EmployeeModel Model = new EmployeeModel();

        // public UsersHRModel usersHRModel = new UsersHRModel();
        #endregion

        #region Contructors and Load
        public frmShowStaff()
        {
            InitializeComponent();

        }

        private void frmShowStaff_Load(object sender, EventArgs e)
        {
            //  CbFix();

            //DataTable dt = TextUtils.Select($"select LoginName,PasswordHash from Users Where ID={Model.UserID}");

            loadUserContract();
            loadUserOther();
            loadUserSalary();
            loadUserContact();
            loadTinhTrangHonNhan();
            loadTeam();
            loadDepartment();
            loadChucVuHD();
            loadChucVu();
            loadImage();
            loadEmployeeEduLevel();
            LoadEmployeeTeam();
            LoadTaxCompany();
            if (Model.ID != 0)
            {
                //txtBankAccount.Text = Model.BankAccount;
                //txtBHXH.Text = Model.BHXH;
                // txtBHYT.Text = Model.BHYT;
                dtpBirthOfDate.EditValue = Model.BirthOfDate;
                txtIDchamcong.Text = Model.IDChamCongMoi;
                txtCode.Text = Model.Code;
                cboDepartment.SelectedValue = Model.DepartmentID;
                txtQuoctich.Text = Model.QuocTich;
                cbHDID.EditValue = Model.ChucVuHDID;
                cbNBID.EditValue = Model.ChuVuID;
                txtFullName.Text = Model.FullName;
                txtQuoctich.Text = Model.QuocTich;
                txtHomeAddress.Text = Model.HomeAddress;
                txtTongiao.Text = Model.TonGiao;
                // txtLoginName.Text =TextUtils.ToString(dt.Rows[0]["LoginName"]); //Model.LoginName;
                txtNoisinh.Text = Model.NoiSinh;
                // txtMST.Text = Model.MST;
                // txtPasswordHash.Text = MD5.DecryptPassword(TextUtils.ToString(dt.Rows[0]["PasswordHash"]));//Model.PasswordHash);
                txtDantoc.Text = Model.DanToc;
                txtQualifications.Text = Model.Qualifications;
                cboSex.SelectedIndex = TextUtils.ToInt(Model.GioiTinh);
                cbTinhtrangHN.SelectedValue = Model.TinhTrangHonNhanID;
                //PbImage.ImageLocation = Model.ImagePath;
                cboStatus.SelectedIndex = TextUtils.ToInt(Model.Status);

                lookUpEdit1.EditValue = Model.UserGroupID;
                cboTeam.SelectedValue = Model.TeamID;
                txtDiadiemlv.Text = Model.DiaDiemLamViec;
                txtDVBHXH.Text = Model.DvBHXH;
                dtpStartWorking.EditValue = Model.StartWorking;

                cboEmployeeTeam.EditValue = Model.EmployeeTeamID;
                cboTaxCompany.EditValue = Model.TaxCompanyID;

                if (txtLoginName.Text != "")
                {
                    //chkHasUser.Checked = true;
                }
            }
            else
            {
                //int code = TextUtils.ToInt(TextUtils.ToString(TextUtils.ExcuteScalar("Select Top 1 Code From Users Order by ID desc")).Replace("NV", ""));
                //txtCode.Text = "NV" + TextUtils.ToString(code + 1);
                cboStatus.SelectedIndex = 0;
            }
        }
        #endregion

        #region Functions
        ucUserContract _ucUserContract = new ucUserContract();
        ucUserOther _ucUserOther = new ucUserOther();
        ucUserSalary _userSalary = new ucUserSalary();
        ucUserContact _ucUserContact = new ucUserContact();
        /// <summary>
        /// Add User Control vào Tab control 
        /// </summary>
        void loadUserContract()
        {
            //ucUserContract ucUserContract = new ucUserContract();
            tabPageUserContractNew.Controls.Add(_ucUserContract);
            _ucUserContract.Dock = DockStyle.Fill;
            _ucUserContract.loadCb();
            _ucUserContract.loadData(ref Model);

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

        void loadEmployeeEduLevel()
        {
            // Loại đào tạo
            DataTable dtTrainType = new DataTable();
            dtTrainType.Columns.Add("StatusT", typeof(int));
            dtTrainType.Columns.Add("TrainTypeR", typeof(string));
            dtTrainType.Rows.Add(1, "Đào tạo chính quy");
            dtTrainType.Rows.Add(2, "Đào tạo nghề");
            dtTrainType.Rows.Add(3, "Đào tạo từ xa");
            dtTrainType.Rows.Add(4, "Đào tạo liên kết quốc tế");
            dtTrainType.Rows.Add(5, "Đào tạo thường xuyên");
            dtTrainType.Rows.Add(6, "Đào tạo theo nhu cầu doanh nghiệp");

            cboTrainType.DisplayMember = "TrainTypeR";
            cboTrainType.ValueMember = "StatusT";
            cboTrainType.DataSource = dtTrainType;

            //xếp loại
            DataTable dtRankType = new DataTable();
            dtRankType.Columns.Add("StatusR", typeof(int));
            dtRankType.Columns.Add("RankTypeR", typeof(string));
            dtRankType.Rows.Add(1, "Đại học");
            dtRankType.Rows.Add(2, "Cao đẳng");
            dtRankType.Rows.Add(3, "Trung cấp");

            cboRankType.DisplayMember = "RankTypeR";
            cboRankType.ValueMember = "StatusR";
            cboRankType.DataSource = dtRankType;

            //cấp bậc
            DataTable dtClassification = new DataTable();
            dtClassification.Columns.Add("StatusC", typeof(int));
            dtClassification.Columns.Add("ClassificationR", typeof(string));
            dtClassification.Rows.Add(1, "Xuất sắc");
            dtClassification.Rows.Add(2, "Giỏi");
            dtClassification.Rows.Add(3, "Khá");
            dtClassification.Rows.Add(4, "Trung bình");
            dtClassification.Rows.Add(5, "Yếu");

            cboClassification.DisplayMember = "ClassificationR";
            cboClassification.ValueMember = "StatusC";
            cboClassification.DataSource = dtClassification;


            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeEduLevel", "A", new string[] { "@EmployeeID" }, new object[] { Model.ID });
            grdData.DataSource = dt;

        }

        void LoadEmployeeTeam()
        {
            DataTable dt = TextUtils.GetTable("spGetEmployeeTeam");
            cboEmployeeTeam.Properties.DataSource = dt;
            cboEmployeeTeam.Properties.DisplayMember = "Name";
            cboEmployeeTeam.Properties.ValueMember = "ID";

            //grvEmployeeTeam.Columns["DepartmentName"].Group();
            //grvEmployeeTeam.Columns["DepartmentName"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            //grvEmployeeTeam.OptionsView.ShowGroupPanel = false;
            //grvEmployeeTeam.ExpandAllGroups();

        }


        void LoadTaxCompany()
        {
            var companys = SQLHelper<TaxCompanyModel>.FindAll();

            cboTaxCompany.Properties.ValueMember = "ID";
            cboTaxCompany.Properties.DisplayMember = "Name";
            cboTaxCompany.Properties.DataSource = companys;
        }
        void loadUserOther()
        {
            //ucUserOther ucUserOther = new ucUserOther();
            tabPageUserOther.Controls.Add(_ucUserOther);
            _ucUserOther.Dock = DockStyle.Fill;
            _ucUserOther.loadData(ref Model);
        }
        void loadUserSalary()
        {
            TabUserSalary.Controls.Add(_userSalary);
            _userSalary.Dock = DockStyle.Fill;
            _userSalary.loadData(ref Model);
        }
        void loadUserContact()
        {
            tabPageUserOtherNew.Controls.Add(_ucUserContact);
            _ucUserContact.Dock = DockStyle.Fill;
            _ucUserContact.loadData(ref Model);
        }

        void loadDepartment()
        {
            DataTable tbl = TextUtils.Select(@"SELECT ID,Name FROM Department WITH(NOLOCK) ORDER BY ID");
            if (tbl == null)
            {
                return;
            }
            TextUtils.PopulateCombo(cboDepartment, tbl.Copy(), "Name", "ID", "-- Phòng ban --");
            DataTable tblPerson = TextUtils.Select("Select ID, Code, Name from UserGroup a with(nolock)");
            //lkTruongNhom.Properties.DataSource = tblPerson;
            //lkTruongNhom.Properties.DisplayMember = "FullName";
            //lkTruongNhom.Properties.ValueMember = "ID";
            TextUtils.PopulateCombo(lookUpEdit1, tblPerson.Copy(), "Name", "ID", "-- Chọn nhóm --");
        }
        void loadChucVuHD()
        {

            DataTable dt = TextUtils.Select("SELECT * FROM EmployeeChucVuHD");
            cbHDID.Properties.DataSource = dt;
            cbHDID.Properties.ValueMember = "ID";
            cbHDID.Properties.DisplayMember = "Name";
            // TextUtils.PopulateCombo(cbHDID, dt.Copy(), "Name", "ID", "-- Chức vụ HĐ --");
            //cbHDID.DataSource = dt;
            //cbHDID.DisplayMember = "Name";
            //cbHDID.ValueMember = "ID";
        }
        void loadChucVu()
        {

            DataTable dt = TextUtils.Select("SELECT * FROM EmployeeChucVu");
            //TextUtils.PopulateCombo(cbNBID, dt.Copy(), "Name", "ID", "-- Chức vụ --");
            cbNBID.Properties.DataSource = dt;
            cbNBID.Properties.ValueMember = "ID";
            cbNBID.Properties.DisplayMember = "Name";
            //cbNBID.DisplayMember = "Name";
            //cbNBID.ValueMember = "ID";
            //cbNBID.DataSource = dt;
        }
        void loadTinhTrangHonNhan()
        {

            DataTable dt = TextUtils.Select("SELECT * FROM EmployeeTinhTrangHonNhan");
            TextUtils.PopulateCombo(cbTinhtrangHN, dt.Copy(), "Name", "ID", "-- Tình trạng hôn nhân --");
            //cbTinhtrangHN.DataSource = dt;
            //cbTinhtrangHN.DisplayMember = "Name";
            //cbTinhtrangHN.ValueMember = "ID";
        }


        void loadTeam()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM UserTeam");
            //DataRow dataRow = dt.NewRow();
            //dataRow["ID"] = 0;
            //dataRow["Name"] = "-- Team --";

            //dt.Rows.InsertAt(dataRow, 0);
            //cboTeam.ValueMember = "ID";
            //cboTeam.DisplayMember = "Name";
            //cboTeam.DataSource = dt;
            //if (Model == null)
            //{
            //    cboTeam.SelectedIndex = -1;
            //}
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
                    dt = TextUtils.Select("select Code from Employee where Code = '" + txtCode.Text.Trim() + "' and ID <> " + Model.ID);
                }
                else
                {
                    dt = TextUtils.Select("select Code from Employee where Code = '" + txtCode.Text.Trim() + "'");
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

            //if (txtIDchamcong.Text.Trim() == "")
            //{
            //    MessageBox.Show("Vui lòng nhập ID chấm công.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //else
            {
                if (Model.ID > 0)
                {
                    dt = TextUtils.Select($"select IDChamCongMoi from Employee where IDChamCongMoi = '{txtIDchamcong.Text.Trim()}' and ID <> {Model.ID}");
                }
                else
                {
                    dt = TextUtils.Select($"select IDChamCongMoi from Employee where IDChamCongMoi = '{txtIDchamcong.Text.Trim()}'");
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

            if (TextUtils.ToInt(cboDepartment.SelectedValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Phòng ban!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(cbHDID.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Chức vụ(Theo HDLD)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(cbNBID.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Chức vụ(Theo Nội bộ)!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //if (string.IsNullOrEmpty(txtDVBHXH.Text))
            //{
            //    MessageBox.Show("Vui lòng nhập Đơn vị bảo hiểm xã hội!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            if (string.IsNullOrEmpty(cboTaxCompany.Text))
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

            //if (!TextUtils.ToDate4(dtpStartWorking.EditValue).HasValue)
            //{
            //    MessageBox.Show("Vui lòng nhập Ngày bắt đầu làm việc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            return true;
        }

        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region
            try
            {
                if (!ValidateForm()) return;

                Model.BirthOfDate = (DateTime?)dtpBirthOfDate.EditValue;
                Model.NoiSinh = txtNoisinh.Text.Trim();
                Model.DanToc = txtDantoc.Text.Trim();
                Model.TonGiao = txtTongiao.Text.Trim();
                Model.QuocTich = txtQuoctich.Text.Trim();
                Model.Code = txtCode.Text.Trim();
                Model.DepartmentID = TextUtils.ToInt(cboDepartment.SelectedValue);
                Model.FullName = txtFullName.Text;
                Model.HomeAddress = txtHomeAddress.Text;
                Model.ChucVuHDID = TextUtils.ToInt(cbHDID.EditValue);
                Model.ChuVuID = TextUtils.ToInt(cbNBID.EditValue);

                if (Model.IDChamCongMoi != txtIDchamcong.Text)
                {
                    Model.IDChamCongCu = Model.IDChamCongMoi;
                }
                Model.IDChamCongMoi = txtIDchamcong.Text;
                Model.Qualifications = txtQualifications.Text.Trim();
                Model.GioiTinh = cboSex.SelectedIndex;
                Model.UserGroupID = TextUtils.ToInt(lookUpEdit1.EditValue);
                Model.TeamID = TextUtils.ToInt(cboTeam.SelectedValue);
                Model.TinhTrangHonNhanID = TextUtils.ToInt(cbTinhtrangHN.SelectedValue);
                Model.DiaDiemLamViec = txtDiadiemlv.Text.Trim();
                Model.ImagePath = ImagePath;
                //Model.DvBHXH = txtDVBHXH.Text;
                Model.DvBHXH = cboTaxCompany.Text;
                Model.RoleID = 2;

                Model.StartWorking = TextUtils.ToDate4(dtpStartWorking.EditValue);
                Model.EmployeeTeamID = TextUtils.ToInt(cboEmployeeTeam.EditValue);
                Model.TaxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue);

                try
                {
                    if (!_ucUserContract.save(ref Model)) return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Form thông tin hợp đồng  bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;

                }
                try
                {
                    _ucUserOther.save(ref Model);
                }
                catch (Exception)
                {
                    MessageBox.Show("Form thông tin liên hệ bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                try
                {
                    if (!_userSalary.save(ref Model)) return;

                }
                catch (Exception)
                {
                    MessageBox.Show("Form thông tin lương bị lỗi", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                try
                {
                    if (!_ucUserContact.save(ref Model)) return;

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
                    //Model.ID = TextUtils.ToInt(UsersBO.Instance.Insert(Model));
                    Model.ID = SQLHelper<EmployeeModel>.Insert(Model).ID;

                    //MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    Model.UpdatedDate = TextUtils.GetSystemDate();
                    Model.UpdatedBy = Global.AppUserName;

                    //UsersBO.Instance.Update(Model);
                    SQLHelper<EmployeeModel>.Update(Model);

                    if (Model.UserID > 0)
                    {
                        //UsersModel usersModel = (UsersModel)UsersBO.Instance.FindByPK(TextUtils.ToInt(Model.UserID));
                        UsersModel usersModel = SQLHelper<UsersModel>.FindByID(TextUtils.ToInt(Model.UserID));
                        usersModel.Status = cboStatus.SelectedIndex;
                        UsersBO.Instance.Update(usersModel);
                    }

                    //MessageBox.Show("Update thành công", "Thông báo", MessageBoxButtons.OK);//❀◕ ‿ ◕❀
                }

                #region Cập nhật trình độ học vấn cán bộ nhân viên VTN
                if (grvData.RowCount > 0)
                {
                    grvData.CloseEditor();
                    List<EmployeeEducationLevelModel> lsCheckSTT = SQLHelper<EmployeeEducationLevelModel>.FindByAttribute(EmployeeEducationLevelModel_Enum.EmployeeID.ToString(), Model.ID);
                    List<int> ls = new List<int>();
                    foreach (var item in lsCheckSTT)
                    {
                        ls.Add(TextUtils.ToInt(item.STT));
                    }
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        EmployeeEducationLevelModel levelModel = new EmployeeEducationLevelModel();
                        levelModel.EmployeeID = Model.ID;
                        levelModel.STT = i + 1;
                        levelModel.SchoolName = TextUtils.ToString(grvData.GetRowCellValue(i, colSchoolName));
                        levelModel.RankType = TextUtils.ToInt(grvData.GetRowCellValue(i, colRankType));
                        levelModel.TrainType = TextUtils.ToInt(grvData.GetRowCellValue(i, colTrainType));
                        levelModel.Major = TextUtils.ToString(grvData.GetRowCellValue(i, colMajor));
                        levelModel.YearGraduate = TextUtils.ToInt(grvData.GetRowCellValue(i, colYearGraduate));
                        levelModel.Classification = TextUtils.ToInt(grvData.GetRowCellValue(i, colClassification));

                        if (levelModel.SchoolName == "")
                        {
                            MessageBox.Show($"Vui lòng nhập tên trường dòng [{i + 1}] và các thông tin liên quan!", "Thông báo", MessageBoxButtons.OK);
                            return;
                        }

                        var ex1 = new Expression(EmployeeEducationLevelModel_Enum.EmployeeID.ToString(), Model.ID);
                        var ex2 = new Expression(EmployeeEducationLevelModel_Enum.STT.ToString(), i + 1);
                        var exists1 = SQLHelper<EmployeeEducationLevelModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault();

                        if (exists1 == null)
                        {
                            var ex3 = new Expression(EmployeeEducationLevelModel_Enum.SchoolName.ToString(), levelModel.SchoolName);
                            var ex4 = new Expression(EmployeeEducationLevelModel_Enum.RankType.ToString(), levelModel.RankType);
                            var ex5 = new Expression(EmployeeEducationLevelModel_Enum.TrainType.ToString(), levelModel.TrainType);
                            var ex6 = new Expression(EmployeeEducationLevelModel_Enum.Major.ToString(), levelModel.Major);
                            var exists = SQLHelper<EmployeeEducationLevelModel>.FindByExpression(ex1.And(ex3).And(ex4).And(ex5).And(ex6)).FirstOrDefault();

                            if (exists != null)
                            {
                                MessageBox.Show($"Thông tin dòng [{i + 1}] đã tồn tại! /n/r Vui lòng kiểm tra lại thông tin ", "Thông báo", MessageBoxButtons.OK);
                                return;
                            }

                            SQLHelper<EmployeeEducationLevelModel>.Insert(levelModel);
                        }
                        else
                        {
                            levelModel.ID = exists1.ID;
                            SQLHelper<EmployeeEducationLevelModel>.Update(levelModel);
                        }

                        if (ls.Contains(i + 1))
                        {
                            ls.Remove(i + 1);
                        }
                    }

                    foreach (int item in ls)
                    {
                        var ex1 = new Expression(EmployeeEducationLevelModel_Enum.EmployeeID.ToString(), Model.ID);
                        var ex2 = new Expression(EmployeeEducationLevelModel_Enum.STT.ToString(), item);
                        int id = SQLHelper<EmployeeEducationLevelModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault().ID;
                        SQLHelper<EmployeeEducationLevelModel>.DeleteModelByID(id);
                    }
                }
                else
                {
                    SQLHelper<EmployeeEducationLevelModel>.DeleteByAttribute("EmployeeID", Model.ID);
                }
                #endregion

                //UpdateContract(Model.ID);


                // pt.CommitTransaction();
                this.DialogResult = DialogResult.OK;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }


        //Update thông tin hợp đồng của nhân viên
        void UpdateContract(int employeeId)
        {
            int contractType = TextUtils.ToInt(_ucUserContract.cboLoaiHD.SelectedValue);
            var exp1 = new Expression("EmployeeID", employeeId);
            var exp2 = new Expression("EmployeeLoaiHDLDID", contractType);
            var exp3 = new Expression("IsDelete", 1, "<>");

            var contracts = SQLHelper<EmployeeContractModel>.FindByExpression(exp1.And(exp2).And(exp3));
            EmployeeContractModel contract = contracts.FirstOrDefault();
            contract = contract == null ? new EmployeeContractModel() : contract;

            contract.EmployeeID = employeeId;
            contract.EmployeeLoaiHDLDID = contractType;
            contract.DateStart = TextUtils.ToDate4(_ucUserContract.dtpDateStart.EditValue);
            contract.DateEnd = TextUtils.ToDate4(_ucUserContract.dtpDateEnd.EditValue);
            contract.ContractNumber = _ucUserContract.txtSoHD.Text.Trim();
            contract.StatusSign = _ucUserContract.rdChuaky.Checked ? 1 : 2;
            contract.DateSign = TextUtils.ToDate4(_ucUserContract.dtpDateSign.EditValue);
            if (contract.ID > 0)
            {
                SQLHelper<EmployeeContractModel>.Update(contract);
            }
            else
            {
                contract.STT = contracts.Count > 0 ? contracts.Max(x => x.STT) + 1 : 1;
                SQLHelper<EmployeeContractModel>.Insert(contract);
            }


        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

            if (TabControlUser.SelectedTabPage == tabPageUserContractNew)
            {
                loadUserContract();
            }

        }

        #endregion

        private void chkHasUser_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkHasUser.Checked == true)
            //{
            //    txtLoginName.Enabled = true;
            //    txtPasswordHash.Enabled = true;
            //}
            //else
            //{
            //    txtLoginName.Enabled = false;
            //    txtPasswordHash.Enabled = false;
            //}
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

        private void btnCVHDLD_Click(object sender, EventArgs e)
        {
            frmPositionDetail frm = new frmPositionDetail();
            frm.HD = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadChucVuHD();
            }
        }

        private void btnCVNB_Click(object sender, EventArgs e)
        {
            frmPositionDetail frm = new frmPositionDetail();
            frm.NB = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadChucVu();
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

        private void cbNBID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PbImage_Click(object sender, EventArgs e)
        {

        }

        private void TabControlUser_Click(object sender, EventArgs e)
        {

        }

        private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {

        }

        #region Cập nhật trình độ học vấn cán bộ nhân viên VTN
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    GridHitInfo info = grvData.CalcHitInfo(e.Location);

                    if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                    {
                        grvData.FocusedRowHandle = -1;

                        DataTable dt = (DataTable)grdData.DataSource;
                        if (dt == null)
                        {
                            dt = new DataTable();
                            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grvData.Columns)
                            {
                                dt.Columns.Add(col.FieldName, col.ColumnType);
                            }
                            grdData.DataSource = dt;
                        }
                        dt.AcceptChanges();
                        DataRow dtrow = dt.NewRow();

                        int stt = 0;
                        if (dt.Rows.Count > 0)
                        {
                            stt = dt.AsEnumerable().Max(x => x.Field<int>("STT"));
                        }
                        dtrow["STT"] = stt + 1;
                        dt.Rows.Add(dtrow);
                    }
                }
            }
            catch { }
        }
        private void btnDelete1_Click(object sender, EventArgs e)
        {
            int STT = TextUtils.ToInt(grvData.GetFocusedRowCellDisplayText(colSTT));
            if (MessageBox.Show(string.Format($"Bạn có chắc chắn muốn số thứ tự [{STT}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    grvData.SetRowCellValue(i, colSTT, i + 1);
                }
            }
        }


        #endregion


        frmEmployeeTeamDetail _frmEmployeeTeamDetail;
        private void btnAddEmployeeTeam_Click(object sender, EventArgs e)
        {
            if (_frmEmployeeTeamDetail == null || _frmEmployeeTeamDetail.IsDisposed)
            {
                _frmEmployeeTeamDetail = new frmEmployeeTeamDetail();

                _frmEmployeeTeamDetail.SaveEvent += (newTeam) =>
                {
                    //_list.Add(newTeam);
                    LoadEmployeeTeam();
                    return Task.CompletedTask;
                };
                _frmEmployeeTeamDetail.FormClosed += (s, args) => _frmEmployeeTeamDetail = null;
                _frmEmployeeTeamDetail.Show();
                
            }
            else
            {
                _frmEmployeeTeamDetail.WindowState = FormWindowState.Normal;
                _frmEmployeeTeamDetail.Activate();
            }

        }
    }
}
