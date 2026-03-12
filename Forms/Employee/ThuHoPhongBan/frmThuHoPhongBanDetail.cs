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
    public partial class frmThuHoPhongBanDetail : _Forms
    {
        public EmployeeCollectMoneyModel employee = new EmployeeCollectMoneyModel();
        public frmThuHoPhongBanDetail()
        {
            InitializeComponent();
        }

        private void frmThuHoPhongBanDetail_Load(object sender, EventArgs e)
        {
            loadName();
            LoadPart();
            loadDetail();
        }
        private void loadDetail()
        {
            //if (employee.ID > 0)
            //{
            //    DataTable dt = TextUtils.Select($"select FullName from Employee where ID = '{employee.EmployeeID}'");
            //    cbName.Text = TextUtils.ToString(dt.Rows[0]["FullName"]);
            //}
            if (employee.ID > 0)
            {
                cbName.EditValue = TextUtils.ToInt(employee.EmployeeID);
                dtpDate.Value = TextUtils.ToDate5(employee.CollectDay);
                cboPart.EditValue = TextUtils.ToInt(employee.PartID);
                txtFund.Text = TextUtils.ToString(employee.Fund);
                txtError.Text = TextUtils.ToString(employee.Error);
                txtParty.Text = TextUtils.ToString(employee.Party);
                txtNote.Text = employee.Notes;
            }
        }
        void loadName()
        {
            DataTable dt = TextUtils.Select($"Select ID ,Code ,FullName from Employee");
            cbName.Properties.DataSource = dt;
            cbName.Properties.DisplayMember = "FullName";
            cbName.Properties.ValueMember = "ID";
        }
        void LoadPart()
        {
            DataTable dtt = TextUtils.Select($"Select ID, Name from dbo.Department");
            cboPart.Properties.DataSource = dtt;
            cboPart.Properties.DisplayMember = "Name";
            cboPart.Properties.ValueMember = "ID";
        } 

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;
            employee.EmployeeID = TextUtils.ToInt(cbName.EditValue);
            employee.CollectDay = TextUtils.ToDate4(dtpDate.Value.ToString());
            employee.PartID = TextUtils.ToInt(cboPart.EditValue);
            employee.Fund = TextUtils.ToDecimal(txtFund.Text.Trim());
            employee.Error = TextUtils.ToDecimal(txtError.Text.Trim());
            employee.Party = TextUtils.ToDecimal(txtParty.Text.Trim());
            employee.TotalMoney = employee.Fund + employee.Error + employee.Party;
            employee.Notes = txtNote.Text.Trim();
            if (employee.ID > 0)
            {
                EmployeeCollectMoneyBO.Instance.Update(employee);
            }
            else
            {
                employee.ID = (int)EmployeeCollectMoneyBO.Instance.Insert(employee);
            }
            return true;
        }
        bool ValidateForm()
        {
            if (cbName.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin họ và tên. ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboPart.Text == "")
            {
                MessageBox.Show("Vui lòng điền phòng ban. ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
