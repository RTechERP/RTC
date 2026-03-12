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
    public partial class frmTypeDetail : _Forms
    {
        public EmployeeTypeOvertimeModel type = new EmployeeTypeOvertimeModel();
        public frmTypeDetail()
        {
            InitializeComponent();
        }

        private void frmTypeDetail_Load(object sender, EventArgs e)
        {
            LoadType();
        }

        void LoadType()
        {
            if (type.ID > 0)
            {
                txtCode.Text = type.TypeCode;
                txtName.Text = type.Type;
                txtRatio.Text = TextUtils.ToString(type.Ratio);
                txtNote.Text = type.Note;
            }
        }

        public bool ValidateForm()
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập mã loại !"),TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (TextUtils.CheckExistTable(type.ID, "TypeCode", txtCode.Text.Trim(), "EmployeeTypeOverTime"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if(txtName.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập tên loại !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtRatio.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập tỉ lệ !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        void saveData()
        {
             if (!ValidateForm()) return;

            type.TypeCode = txtCode.Text;
            type.Type = txtName.Text;
            type.Ratio = TextUtils.ToDecimal(txtRatio.Text.Trim());
            type.Note = txtNote.Text.Trim();
            if (type.ID > 0)
            {
                EmployeeTypeOvertimeBO.Instance.Update(type);
            }
            else
                type.ID = (int)EmployeeTypeOvertimeBO.Instance.Insert(type);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }
    }
}
