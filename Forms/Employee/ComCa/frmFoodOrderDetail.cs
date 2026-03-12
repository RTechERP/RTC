using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmFoodOrderDetail : _Forms
    {
        public EmployeeFoodOrderModel model = new EmployeeFoodOrderModel();

        int _location = 0;
        public frmFoodOrderDetail(int location  )
        {
            InitializeComponent();
            _location = location;
        }
        private void frmFoodOrderDetail_Load(object sender, EventArgs e)
        {
            loadUser();
            loadData();

        }


        void loadUser()
        {
            DataTable dt = TextUtils.Select("Select ID,FullName, Code from Employee");
            searchLookUpEdit1.Properties.DataSource = dt;
            searchLookUpEdit1.Properties.ValueMember = "ID";
            searchLookUpEdit1.Properties.DisplayMember = "FullName";

        }

        void loadData()
        {

            if (model.ID > 0)
            {
                searchLookUpEdit1.EditValue = model.EmployeeID;
                dateTimePicker1.Text = TextUtils.ToString(model.DateOrder);
                txtSL.Text = TextUtils.ToString(model.Quantity);
                txtNote.Text = TextUtils.ToString(model.Note);

                rdLocation1.Checked = model.Location == 1;
                rdLocation2.Checked = model.Location == 2;
            }
            else
            {
                txtSL.Text = 1.ToString();

                rdLocation1.Checked = _location == 1;
                rdLocation2.Checked = _location == 2;
            }
        }

        bool CheckValidate()
        {
            if (TextUtils.ToInt(searchLookUpEdit1.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng nhập Họ và tên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (model.ID == 0)
                {
                    int id = TextUtils.ToInt(searchLookUpEdit1.EditValue);
                    //DateTime dtm = DateTime.Parse(dateTimePicker1.Text);
                    //DateTime date = DateTime.ParseExact(dateTimePicker1.Text, new string[] { "dd/MM/yyyy" }, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces);
                    //string date2 = string.Format("{0: yyyy/MM/dd}", date);

                    //DataTable dt = TextUtils.Select($"Select * from EmployeeFoodOrder where EmployeeID={id} and DateOrder='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}'");

                    string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    var idFo = TextUtils.ExcuteScalar($"EXEC spGetEmployeeFoodOrderByDate '{date}', {id}");
                    if (TextUtils.ToInt(idFo) > 0)
                    {
                        MessageBox.Show($"Nhân viên [{searchLookUpEdit1.Text}] đã đặt cơm ngày {dateTimePicker1.Value.ToString("dd/MM/yyyy")}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            return true;
        }
        bool save()
        {
            if (!CheckValidate()) return false;


            EmployeeFoodOrderModel m = new EmployeeFoodOrderModel();
            if (model.ID > 0)
            {
                //m =(EmployeeFoodOrderModel)EmployeeFoodOrderBO.Instance.FindByPK(model.ID);
                m = SQLHelper<EmployeeFoodOrderModel>.FindByID(model.ID);
            }
            m.EmployeeID = TextUtils.ToInt(searchLookUpEdit1.EditValue);
            //m.DateOrder = TextUtils.ToDate(dateTimePicker1.Text);
            m.DateOrder = dateTimePicker1.Value;
            m.Quantity = TextUtils.ToInt(txtSL.Text);
            //m.Note = TextUtils.ToString(txtNote.Text);
            m.IsApproved = false;


            m.Location = rdLocation2.Checked ? 2 : 1;

            if (model.ID > 0)
            {
                EmployeeFoodOrderBO.Instance.Update(m);
            }
            else
            {
                EmployeeFoodOrderBO.Instance.Insert(m);
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
                this.DialogResult = DialogResult.OK;
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
