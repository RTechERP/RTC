using BMS.Business;
using BMS.Model;
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
    public partial class frmWorkingProcessDetail : _Forms
    {
        public EmployeeWorkingProcessModel workprocess = new EmployeeWorkingProcessModel();
        public frmWorkingProcessDetail()
        {
            InitializeComponent();
        }

        private void frmWorkingProcessDetail_Load(object sender, EventArgs e)
        {
            LoadcboNV();
            LoadcboViTriCV();
            LoadcboDonViCT();
            LoadcboTrangThai();
            LoadcboQuanLyTT();
            LoadcboQuanLyGT();
            LoadWorkProcess();
        }
        void LoadcboNV()
        {
            //DataTable NV = TextUtils.Select("Select ID, Code, FullName from dbo.Employee");
            DataTable NV = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboNV.Properties.DataSource = NV;
            cboNV.Properties.DisplayMember = "FullName";
            cboNV.Properties.ValueMember = "ID";
        }
        void LoadcboViTriCV()
        {
            DataTable Vitri = TextUtils.Select("Select ID, Code, Name from EmployeeChucVu");
            cboViTriCV.Properties.DataSource = Vitri;
            cboViTriCV.Properties.DisplayMember = "Name";
            cboViTriCV.Properties.ValueMember = "ID";
        }
        void LoadcboDonViCT()
        {
            DataTable Donvi = TextUtils.Select("Select ID, Code, Name from dbo.Department");
            cboDonViCT.Properties.DataSource = Donvi;
            cboDonViCT.Properties.DisplayMember = "Name";
            cboDonViCT.Properties.ValueMember = "ID";
        }
        void LoadcboTrangThai()
        {
            DataTable Status = TextUtils.Select("Select ID, StatusCode, StatusName from dbo.EmployeeStatus");
            cboTrangThai.Properties.DataSource = Status;
            cboTrangThai.Properties.DisplayMember = "StatusName";
            cboTrangThai.Properties.ValueMember = "ID";
        }
        void LoadcboQuanLyTT()
        {
            DataTable Tructiep = TextUtils.Select("Select ID, Code, FullName from dbo.Employee");
            cboQuanLyTT.Properties.DataSource = Tructiep;
            cboQuanLyTT.Properties.DisplayMember = "FullName";
            cboQuanLyTT.Properties.ValueMember = "ID";
        }
        void LoadcboQuanLyGT()
        {
            DataTable Giantiep = TextUtils.Select("Select ID, Code, FullName from dbo.Employee");
            cboQuanLyGT.Properties.DataSource = Giantiep;
            cboQuanLyGT.Properties.DisplayMember = "FullName";
            cboQuanLyGT.Properties.ValueMember = "ID";
        }
        void LoadWorkProcess()
        {
            if (workprocess.ID > 0)
            {
                cboNV.EditValue = TextUtils.ToString(workprocess.EmployeeID);
                dtpDateStart.Value = TextUtils.ToDate5(workprocess.StartDate);
                dtpDateEnd.Value = TextUtils.ToDate5(workprocess.EndDate);
                cboViTriCV.EditValue = TextUtils.ToString(workprocess.JobPosition);
                cboDonViCT.EditValue = TextUtils.ToString(workprocess.WorkUnit);
                cboTrangThai.EditValue = TextUtils.ToString(workprocess.Status);
                cboQuanLyGT.EditValue = TextUtils.ToString(workprocess.IndirectManagement);
                cboQuanLyTT.EditValue = TextUtils.ToString(workprocess.DirectManagement);
                txtSoQuyetDinh.Text = workprocess.DecisionNumber;
                dtpNgayQuyetDinh.Value = TextUtils.ToDate5(workprocess.DecisionDay);
                txtLuongTV.Text = TextUtils.ToString(workprocess.ProbationarySalary);
                txtLuongCB.Text = TextUtils.ToString(workprocess.BasicSalary);
                txtAnCa.Text = TextUtils.ToString(workprocess.ShiftEat);
                txtXangXe.Text = TextUtils.ToString(workprocess.Gasoline);
                txtDienThoai.Text = TextUtils.ToString(workprocess.Phone);
                txtNhaO.Text = TextUtils.ToString(workprocess.House);
                txtTrangPhuc.Text = TextUtils.ToString(workprocess.Skin);
                txtChuyenCan.Text = TextUtils.ToString(workprocess.Diligence);
                txtOther.Text = TextUtils.ToString(workprocess.Other);
                txtGhiChu.Text = TextUtils.ToString(workprocess.Note);
            }
        }

        public bool ValidateForm()
        {
            if(cboNV.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập nhân viên. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
      
            if (cboViTriCV.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào vị trí công việc. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboDonViCT.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào phòng. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboTrangThai.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập trạng thái làm việc. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtSoQuyetDinh.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập số quyết định. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtLuongTV.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập mức lương thử việc. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtLuongCB.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập mức lương cơ bản. "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

         bool saveData()
        {
            if (!ValidateForm()) return false;

            workprocess.EmployeeID = TextUtils.ToInt(cboNV.EditValue);
            workprocess.Approveder = Global.UserID;
            workprocess.StartDate = dtpDateStart.Value;
            workprocess.EndDate = dtpDateEnd.Value; 
            workprocess.JobPosition = TextUtils.ToInt(cboViTriCV.EditValue);
            workprocess.WorkUnit = TextUtils.ToInt(cboDonViCT.EditValue);
            workprocess.Status = TextUtils.ToInt(cboTrangThai.EditValue);
            workprocess.IndirectManagement = TextUtils.ToInt(cboQuanLyGT.EditValue);
            workprocess.DirectManagement = TextUtils.ToInt(cboQuanLyTT.EditValue);
            workprocess.DecisionNumber = txtSoQuyetDinh.Text.Trim();
            workprocess.DecisionDay = dtpNgayQuyetDinh.Value;
            workprocess.ProbationarySalary = TextUtils.ToDecimal(txtLuongTV.Text.Trim());
            workprocess.BasicSalary = TextUtils.ToDecimal(txtLuongCB.Text.Trim());
            workprocess.ShiftEat = TextUtils.ToDecimal(txtAnCa.Text.Trim());
            workprocess.Gasoline = TextUtils.ToDecimal(txtXangXe.Text.Trim());
            workprocess.Phone = TextUtils.ToDecimal(txtDienThoai.Text.Trim());
            workprocess.House = TextUtils.ToDecimal(txtNhaO.Text.Trim());
            workprocess.Skin = TextUtils.ToDecimal(txtTrangPhuc.Text.Trim());
            workprocess.Diligence = TextUtils.ToDecimal(txtChuyenCan.Text.Trim());
            workprocess.Other = TextUtils.ToDecimal(txtOther.Text.Trim());
            workprocess.Note = txtGhiChu.Text.Trim();

            if (workprocess.ID > 0)
            {
                EmployeeWorkingProcessBO.Instance.Update(workprocess);
            }
            else
                workprocess.ID = (int)EmployeeWorkingProcessBO.Instance.Insert(workprocess);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
           
        }

        private void btnAddStatus_Click(object sender, EventArgs e)
        {
            frmStatusWorkingProcessDetail frm = new frmStatusWorkingProcessDetail();
            if (frm.ShowDialog() ==DialogResult.OK)
            {
                LoadcboTrangThai();
            }
        }
    }
}
