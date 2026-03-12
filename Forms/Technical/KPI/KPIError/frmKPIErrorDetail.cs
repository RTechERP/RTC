using BMS.Model;
using BMS.Utils;
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
    public partial class frmKPIErrorDetail : _Forms
    {
        public int departmentID = 0;
        public KPIErrorModel model = new KPIErrorModel();
        public frmKPIErrorDetail()
        {
            InitializeComponent();
        }
        private void frmKPIErrorDetail_Load(object sender, EventArgs e)
        {
            LoadDepartMent();
            LoadType();
            LoadData();
        }

        private void LoadDepartMent()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartMent.Properties.DataSource = lst;
            cboDepartMent.Properties.ValueMember = "ID";
            cboDepartMent.Properties.DisplayMember = "Name";

            cboDepartMent.EditValue = departmentID;
        }

        void LoadData()
        {
            txtCode.Text = model.Code;
            //txtName.Text = model.Name;
            cboKPIErrorType.EditValue = model.KPIErrorTypeID; //LinhTN update 22/10/2024
            txtQuantity.Value = model.Quantity == 0 ? 1 : model.Quantity;
            cboUnit.SelectedIndex = (model.Unit == 0 ? 1 : model.Unit) - 1;
            txtMonney.Text = TextUtils.ToString(model.Monney);
            txtContent.Text = model.Content;
            txtNote.Text = model.Note;
        }
        void LoadType()
        {
            var dt = SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0);
            cboKPIErrorType.Properties.DisplayMember = "Name";
            cboKPIErrorType.Properties.ValueMember = "ID";
            cboKPIErrorType.Properties.DataSource = dt;
        }
        bool CheckValidate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã lỗi vi phạm!", "Thông báo");
                return false;
            }
            //if (string.IsNullOrEmpty(txtName.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Tên lỗi vi phạm!", "Thông báo");
            //    return false;
            //}
            if (TextUtils.ToInt(cboKPIErrorType.EditValue) <= 0) //LinhTN update 22/10/2024
            {
                MessageBox.Show("Vui lòng chọn Loại lỗi vi phạm!", "Thông báo");
                return false;
            }
            //if (string.IsNullOrEmpty(txtMonney.Text.Trim()) || txtMonney.Text == "0")
            //{
            //    MessageBox.Show("Vui lòng nhập số Tiền phạt!", "Thông báo");
            //    return false;
            //}
            //if (TextUtils.ToDecimal(txtMonney.Text) <= 0)
            //{
            //    MessageBox.Show("Số tiền phạt phải lớn hơn 0!", "Thông báo");
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtContent.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung lỗi vi phạm!", "Thông báo");
                return false;
            }

            if (TextUtils.ToDecimal(txtMonney.Text) < 0) //LinhTN update 09/09/2024
            {
                MessageBox.Show("Tiền phạt không được nhỏ hơn 0!", "Thông báo");
                return false;
            }

            var ex1 = new Expression("ID", model.ID, "<>");
            var ex2 = new Expression("Code", txtCode.Text.Trim());
            //var exists = SQLHelper<KPIErrorModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault();

            var ex3 = new Expression("IsDelete", 0); //LinhTN update 09/08/2024
            var ex4 = new Expression("DepartmentID", departmentID); //LinhTN update 09/08/2024
            var exists = SQLHelper<KPIErrorModel>.FindByExpression(ex1.And(ex2).And(ex3).And(ex4));
            if (exists.Count() > 0)
            {
                MessageBox.Show($"Mã lỗi [{txtCode.Text.Trim()}] đã tồn tại]!", "Thông báo");
                return false;
            }
            return true;
        }
        bool Save()
        {
            if (!CheckValidate()) return false;
            model.Code = txtCode.Text;
            //model.Name = txtName.Text;
            model.KPIErrorTypeID = TextUtils.ToInt(cboKPIErrorType.EditValue); //LinhTN update 22/10/2024
            model.Quantity = TextUtils.ToInt(txtQuantity.Value);
            model.Unit = cboUnit.SelectedIndex + 1;
            model.Monney = TextUtils.ToDecimal(txtMonney.Text);
            model.Content = txtContent.Text;
            model.Note = txtNote.Text;
            model.DepartmentID = TextUtils.ToInt(cboDepartMent.EditValue);



            if (model.ID > 0)
            {
                SQLHelper<KPIErrorModel>.Update(model);
            }
            else
            {
                SQLHelper<KPIErrorModel>.Insert(model);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveAndReset_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                model = new KPIErrorModel();
                LoadData();
            }
        }

        private void frmKPIErrorDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //LinhTN update 22/10/2024
        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmKPIErrorTypeDetail frm = new frmKPIErrorTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadType();
            }
        }
    }
}