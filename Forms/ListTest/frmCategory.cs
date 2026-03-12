using BMS;
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
    public partial class frmCategory : _Forms
    {
        public ExamCategoryModel model = new ExamCategoryModel();
        public frmCategory()
        {
            InitializeComponent();
        }
        private void frmCategory_Load(object sender, EventArgs e)
        {
            if (model.ID > 0)
            {
                txtCode.Text = model.CatCode;
                txtName.Text = model.CatName;
                cbStatus.Checked = model.Status;
                txtYear.Value = model.Year;
                txtQuy.Value = model.Quy;
            }
            else
            {
                DateTime now = DateTime.Now;
                txtYear.Value = now.Year;
                int month = now.Month;
                txtQuy.Value = month <= 3 ? 1 : month <= 6 ? 2 : month <= 9 ? 3 : 4;
               
            }
        }
        bool ValidateForm()
        {
            if(txtCode.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Mã kì thi không được bỏ trống. Vui lòng nhập mã kì thi! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (TextUtils.Select($"Select top 1 ID FROM ExamCategory WHERE ID <> {model.ID} AND CatCode = '{txtCode.Text.Trim()}'").Rows.Count > 0)
            {
                MessageBox.Show(string.Format("Mã kì thi đã tồn tại. Vui lòng nhập mã kì thi khác! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Tên kì thi không được bỏ trống. Vui lòng nhập tên kì thi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;
            model.CatName = txtName.Text.Trim();
            model.CatCode = txtCode.Text.Trim();
            model.Status = cbStatus.Checked;
            model.Year =TextUtils.ToInt(txtYear.Value);
            model.Quy =TextUtils.ToInt(txtQuy.Value);

            if (model.ID > 0)
            {
                ExamCategoryBO.Instance.Update(model);
            }
            else
                //model.ID = (int)ExamCategoryBO.Instance.Insert(model);
                ExamCategoryBO.Instance.Insert(model);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        
    }
}
