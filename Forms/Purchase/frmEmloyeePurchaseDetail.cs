using BMS.Model;
using System;
using BMS.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Business;
using System.Text.RegularExpressions;

namespace BMS
{
    public partial class frmEmloyeePurchaseDetail : _Forms
    {
        public EmployeePurchaseModel employeepurchase = new EmployeePurchaseModel();
        public frmEmloyeePurchaseDetail()
        {
            InitializeComponent();
        }

        private void frmEmloyeePurchaseDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadCompany();
            loademployeepurchasedetail();
        }


        private void loademployeepurchasedetail()
        {
            if (employeepurchase.ID > 0)
            {
                cboEmployee.EditValue = employeepurchase.EmployeeID;
                cboTaxCompany.EditValue = employeepurchase.TaxCompayID;
                //txtTelephone.Text = employeepurchase.Telephone;
                //txtEmail.Text = employeepurchase.Email;
                txtTelephone.Text = employeepurchase.Telephone;
                txtEmail.Text = employeepurchase.Email;
                //List<EmployeePurchaseModel> list = SQLHelper<EmployeePurchaseModel>.FindByAttribute("id", employeepurchase.ID);

                txtFullName.Text = employeepurchase.FullName;
            }


        }
        #region LOAD NHÂN VIÊN
        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }
        #endregion
        #region LOAD CTY
        void LoadCompany()
        {
            //DataTable list = TextUtils.LoadDataFromSP("spGetTaxCompany", "B", new string[] { "@Status" }, new object[] { -1 });
            List<TaxCompanyModel> list = SQLHelper<TaxCompanyModel>.FindAll();
            cboTaxCompany.Properties.ValueMember = "ID";
            cboTaxCompany.Properties.DisplayMember = "Name";
            cboTaxCompany.Properties.DataSource = list;
        }
        #endregion
        #region ĐIỀU KIỆN EMAIL
        static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }
        #endregion

        bool CheckValidate()
        {
            //DataTable dt;
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Xin hãy chọn Nhân viên mua hàng", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (string.IsNullOrWhiteSpace(txtTelephone.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Số điện thoại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            //string regexTelephone = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
            //if (!Regex.IsMatch(txtTelephone.Text, regexTelephone))
            //{
            //    MessageBox.Show("SDT liên hệ không đúng định dạng. Vui lòng nhập lại SDT liên hệ!", "Thông báo");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(txtEmail.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Email !", TextUtils.Caption);
            //    return false;
            //}
            //else if (!isEmail(txtEmail.Text.Trim()))
            //{
            //    MessageBox.Show("Email cá nhân không hợp lệ", TextUtils.Caption);
            //    return false;
            //}

            if (TextUtils.ToInt(cboTaxCompany.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Tên Công ty", TextUtils.Caption);
                return false;
            }
            else
            {
                var exp1 = new Expression("EmployeeID", TextUtils.ToInt(cboEmployee.EditValue));
                var exp2 = new Expression("TaxCompayID", TextUtils.ToInt(cboTaxCompany.EditValue));
                var exp3 = new Expression("ID", employeepurchase.ID, "<>");
                var model = SQLHelper<EmployeePurchaseModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (model.Count > 0)
                {
                    MessageBox.Show($"Đã tồn tại thông tin nhân viên [{cboEmployee.Text}] trong công ty [{cboTaxCompany.Text}]!", "Thông Báo");

                    return false;
                    //if () == DialogResult.OK)
                    //{
                    //}
                }
                else
                {
                    //SQLHelper<EmployeePurchaseModel>.Insert(employeepurchase);
                }
            }

            return true;
        }

        private bool saveData()
        {
            if (!CheckValidate()) return false;
            EmployeePurchaseModel model = SQLHelper<EmployeePurchaseModel>.FindByID(employeepurchase.ID);
            model.Telephone = txtTelephone.Text.Trim();
            model.Email = txtEmail.Text.Trim();
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.TaxCompayID = TextUtils.ToInt(cboTaxCompany.EditValue);
            model.FullName = txtFullName.Text.Trim();

            if (model.ID > 0)
            {
                SQLHelper<EmployeePurchaseModel>.Update(model);
            }
            else
            {
                SQLHelper<EmployeePurchaseModel>.Insert(model);
            }
            return true;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (saveData()) this.DialogResult = DialogResult.OK;
        }



        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                employeepurchase = new EmployeePurchaseModel();
                txtTelephone.Clear();
                txtEmail.Clear();
                //List<EmployeePurchaseModel> list = SQLHelper<EmployeePurchaseModel>.FindByAttribute("ID", employeepurchase.ID);
                loademployeepurchasedetail();
            }
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView data = (DataRowView)cboEmployee.GetSelectedDataRow();

            string fullName = data == null? "" : TextUtils.ToString(data["FullName"]);

            txtFullName.Text = fullName;
            
        }
    }
}
