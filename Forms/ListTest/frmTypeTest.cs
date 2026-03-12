using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.BO;
using BMS.Business;

namespace BMS
{
    public partial class frmTypeTest : _Forms
    {
        ExamTypeTestModel model = new ExamTypeTestModel();
        public frmTypeTest()
        {
            InitializeComponent();
        }
        bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Mã loại đề thi không được bỏ trống. Vui lòng nhập mã kì thi! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (TextUtils.Select($"Select top 1 ID FROM ExamTypeTest WHERE TypeCode = {txtCode.Text.Trim()}").Rows.Count > 0)
            {
                MessageBox.Show(string.Format("Mã loại đề thi đã tồn tại. Vui lòng nhập mã loại đề thi khác! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Tên loại đề thi không được bỏ trống. Vui lòng nhập tên loại đề thi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            model.TypeName = txtName.Text.Trim();
            model.TypeCode = txtCode.Text.Trim();

            if (model.ID > 0)
            {
                ExamTypeTestBO.Instance.Update(model);
            }
            else
                ExamTypeTestBO.Instance.Insert(model);
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
