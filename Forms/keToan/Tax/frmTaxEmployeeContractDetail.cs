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
    public partial class frmTaxEmployeeContractDetail : _Forms
    {
        public int taxContractID;
        public int taxEmployeeID;
        public frmTaxEmployeeContractDetail()
        {
            InitializeComponent();
        }

        private void frmTaxEmployeeContractDetail_Load(object sender, EventArgs e)
        {
            dtpDateStart.EditValue = DateTime.Now;
            dtpDateEnd.EditValue = DateTime.Now;
            cboStatusSign.SelectedIndex = 0;

            loadTaxEmployee();
            loadLoaiHDLD();
            if (taxEmployeeID > 0)
            {
                cboTaxEmployee.EditValue = taxEmployeeID;
            }
            loadData();
        }
        private void loadLoaiHDLD()
        {
            cboLoaiHDLD.Properties.DataSource = SQLHelper<EmployeeLoaiHDLDModel>.SqlToList("SELECT ID,Code,Name FROM dbo.EmployeeLoaiHDLD");
            cboLoaiHDLD.Properties.ValueMember = "ID";
            cboLoaiHDLD.Properties.DisplayMember = "Name";
        }
        private void loadTaxEmployee()
        {
            cboTaxEmployee.Properties.DataSource = SQLHelper<TaxEmployeeModel>.SqlToList("SELECT ID,Code,FullName FROM dbo.TaxEmployee");
            cboTaxEmployee.Properties.ValueMember = "ID";
            cboTaxEmployee.Properties.DisplayMember = "FullName";
        }
        private void loadData()
        {
            if (taxContractID > 0)
            {
                TaxEmployeeContractModel model = (TaxEmployeeContractModel)TaxEmployeeContractBO.Instance.FindByPK(taxContractID);
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
            int id = TextUtils.ToInt(cboTaxEmployee.EditValue);
            int stt = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM dbo.TaxEmployeeContract WHERE TaxEmployeeID = {id} ORDER BY STT DESC"));
            return stt + 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            updateEmployee();
            this.DialogResult = DialogResult.OK;
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

        private bool save()
        {
            if (!validate()) return false;
            TaxEmployeeContractModel model = new TaxEmployeeContractModel();
            if (taxContractID > 0)
            {
                model = (TaxEmployeeContractModel)TaxEmployeeContractBO.Instance.FindByPK(taxContractID);
            }
            model.STT = TextUtils.ToInt(txtSTT.Text.Trim());
            model.TaxEmployeeID = TextUtils.ToInt(cboTaxEmployee.EditValue);
            model.EmployeeLoaiHDLDID = TextUtils.ToInt(cboLoaiHDLD.EditValue);
            model.DateStart = TextUtils.ToDate4(dtpDateStart.EditValue);
            model.DateEnd = TextUtils.ToDate4(dtpDateEnd.EditValue);
            model.ContractNumber = txtContractNumber.Text.Trim();
            model.StatusSign = TextUtils.ToInt(cboStatusSign.SelectedIndex + 1);
            model.DateSign = TextUtils.ToDate4(dtpDateSign.EditValue);
            model.IsDelete = false;
            if (model.ID > 0)
            {

                TaxEmployeeContractBO.Instance.Update(model);
            }
            else
            {
                TaxEmployeeContractBO.Instance.Insert(model);
            }
            return true;
        }
        private void updateEmployee()
        {
            try
            {
                int id = TextUtils.ToInt(cboTaxEmployee.EditValue);
                TaxEmployeeModel employee = (TaxEmployeeModel)TaxEmployeeBO.Instance.FindByPK(id);
                TaxEmployeeContractModel contract = SQLHelper<TaxEmployeeContractModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.TaxEmployeeContract WHERE TaxEmployeeID = {id} AND IsDelete <> 1 ORDER BY STT DESC");
                if (contract == null) return;
                employee.LoaiHDLDID = contract.EmployeeLoaiHDLDID;

                EmployeeBO.Instance.Update(employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private bool validate()
        {
            if (string.IsNullOrEmpty(cboTaxEmployee.Text.Trim()))
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
            if (!TextUtils.ToDate4(dtpDateEnd.EditValue).HasValue)
            {
                MessageBox.Show("Vui lòng chọn Ngày kết thúc hợp đồng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtContractNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số hợp đồng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (dtpDateStart.DateTime > dtpDateEnd.DateTime)
            {
                MessageBox.Show("Thời gian bắt đầu và kết thúc không hợp lệ.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void cboTaxEmployee_EditValueChanged(object sender, EventArgs e)
        {
            taxEmployeeID = 0;
            txtSTT.Text = loadSTT().ToString();
        }

        private void frmTaxEmployeeContractDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
