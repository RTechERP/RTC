using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmEmployeeContractDetail : _Forms
    {
        public int contractID = 0;
        public int employeeID = 0;
        public frmEmployeeContractDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeeContractDetail_Load(object sender, EventArgs e)
        {
            dtpDateStart.EditValue = DateTime.Now;
            dtpDateEnd.EditValue = DateTime.Now;
            //dtpDateSign.EditValue = DateTime.Now;
            cboStatusSign.SelectedIndex = 0;
            loadLoaiHDLD();
            loadEmployee();
            loadData();
        }

        private void loadLoaiHDLD()
        {
            cboLoaiHDLD.Properties.DataSource = SQLHelper<EmployeeLoaiHDLDModel>.SqlToList("SELECT ID,Code,Name FROM dbo.EmployeeLoaiHDLD");
            cboLoaiHDLD.Properties.ValueMember = "ID";
            cboLoaiHDLD.Properties.DisplayMember = "Name";
        }
        private void loadEmployee()
        {
            cboEmployee.Properties.DataSource = SQLHelper<EmployeeModel>.SqlToList("SELECT ID,Code,FullName FROM dbo.Employee");
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.EditValue = employeeID;
        }
        private void loadData()
        {
            if (contractID > 0)
            {
                EmployeeContractModel model = (EmployeeContractModel)EmployeeContractBO.Instance.FindByPK(contractID); ;
                txtSTT.Text = model.STT.ToString();
                cboLoaiHDLD.EditValue = model.EmployeeLoaiHDLDID;
                dtpDateStart.EditValue = model.DateStart;
                dtpDateEnd.EditValue = model.DateEnd;
                dtpDateSign.EditValue = model.DateSign;
                txtContractNumber.Text = model.ContractNumber;
                cboStatusSign.SelectedIndex = model.StatusSign - 1;
            }
            else
            {
                txtSTT.Text = loadSTT().ToString();
            }
        }
        private int loadSTT()
        {
            int id = TextUtils.ToInt(cboEmployee.EditValue);
            int stt = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM dbo.EmployeeContract WHERE EmployeeID = {id} ORDER BY STT DESC"));
            return stt + 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            updateEmployee();
            this.DialogResult = DialogResult.OK;
        }

        private bool save()
        {
            if (!validate()) return false;
            EmployeeContractModel model = new EmployeeContractModel();
            if (contractID > 0)
            {
                model = (EmployeeContractModel)EmployeeContractBO.Instance.FindByPK(contractID);
            }
            model.STT = TextUtils.ToInt(txtSTT.Text.Trim());
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.EmployeeLoaiHDLDID = TextUtils.ToInt(cboLoaiHDLD.EditValue);
            model.DateStart = TextUtils.ToDate4(dtpDateStart.EditValue);
            model.DateEnd = TextUtils.ToDate4(dtpDateEnd.EditValue);
            model.ContractNumber = txtContractNumber.Text.Trim();
            model.StatusSign = TextUtils.ToInt(cboStatusSign.SelectedIndex + 1);
            model.DateSign = TextUtils.ToDate4(dtpDateSign.EditValue);
            model.IsDelete = false;
            if (model.ID > 0)
            {
                EmployeeContractBO.Instance.Update(model);
            }
            else
            {
                EmployeeContractBO.Instance.Insert(model);
            }
            return true;
        }
        private void updateEmployee()
        {
            try
            {
                int id = TextUtils.ToInt(cboEmployee.EditValue);
                EmployeeModel employee =(EmployeeModel)EmployeeBO.Instance.FindByPK(id);
                EmployeeContractModel contract = SQLHelper<EmployeeContractModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.EmployeeContract WHERE EmployeeID = {id} AND IsDelete <> 1 ORDER BY STT DESC");
                employee.LoaiHDLDID = contract.EmployeeLoaiHDLDID;
                //employee.TinhTrangKyHD = contract.StatusSign == 1 ? "Chưa ký" : "Đã ký";

                //if (contract.EmployeeLoaiHDLDID == 1)
                //{
                //    employee.NgayBatDauThuViec = contract.DateStart;
                //    employee.NgayKetThucThuViec = contract.DateEnd;
                //    employee.SoHDTV = contract.ContractNumber;
                //}
                //else if (contract.EmployeeLoaiHDLDID == 2)
                //{
                //    employee.NgayBatDauHDXDTH = contract.DateStart;
                //    employee.NgayKetThucHDXDTH = contract.DateEnd;
                //    employee.SoHDXDTH = contract.ContractNumber;
                //}
                //else
                //{
                //    employee.NgayHieuLucHDKXDTH = contract.DateStart;
                //    employee.SoHDKXDTH = contract.ContractNumber;
                //}

                EmployeeBO.Instance.Update(employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private bool validate()
        {
            if (string.IsNullOrEmpty(cboEmployee.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn Nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(cboLoaiHDLD.Text.Trim()))
            {
                MessageBox.Show("Vui lòng chọn Loại hợp đồng lao động!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (!TextUtils.ToDate4(dtpDateStart.EditValue).HasValue)
            {
                MessageBox.Show("Vui lòng chọn Ngày bắt đầu hợp đồng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            int contractType = TextUtils.ToInt(cboLoaiHDLD.EditValue);
            if (!TextUtils.ToDate4(dtpDateEnd.EditValue).HasValue && contractType != 5)
            {
                MessageBox.Show("Vui lòng chọn Ngày kết thúc hợp đồng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtContractNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số hợp đồng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (dtpDateStart.DateTime > dtpDateEnd.DateTime && contractType != 5)
            {
                MessageBox.Show("Thời gian bắt đầu và kết thúc không hợp lệ.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadSTT();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            updateEmployee();

            txtSTT.Text = loadSTT().ToString();
            cboLoaiHDLD.EditValue = 0;
            dtpDateStart.EditValue = null;
            dtpDateEnd.EditValue = null;
            txtContractNumber.Text = "";
            cboStatusSign.SelectedIndex = 0;
            dtpDateSign.EditValue = null;

        }

        private void cboLoaiHDLD_EditValueChanged(object sender, EventArgs e)
        {
            int contractType = TextUtils.ToInt(cboLoaiHDLD.EditValue);
            if (contractType == 5)
            {
                label11.Visible = false;
                dtpDateEnd.EditValue = null;
            }
            else
            {
                label11.Visible = true;
                dtpDateEnd.EditValue = DateTime.Now;
            }
            
        }
    }
}