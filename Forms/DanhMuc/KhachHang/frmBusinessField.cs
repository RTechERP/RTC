using BMS.Model;
using BMS.Utils;
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
    public partial class frmBusinessField : _Forms
    {
        public int id = 0;
        public frmBusinessField()
        {
            InitializeComponent();
        }

        private void frmBusinessField_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            BusinessFieldModel business = SQLHelper<BusinessFieldModel>.FindByID(id);
            if (business.ID > 0)
            {

            }
            else
            {
                business = SQLHelper<BusinessFieldModel>.FindAll().OrderByDescending(x => x.ID).FirstOrDefault();
                txtSTT.Value = business != null ? business.STT + 1 : 1;

                txtCode.Text = "";
                txtName.Text = "";
                txtNote.Text = "";
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                id = 0;
                loadData();
            }
        }


        bool validate()
        {

            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã lĩnh vực!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCode.Text.Trim());
                var exp2 = new Expression("ID", id, "<>");
                BusinessFieldModel business = SQLHelper<BusinessFieldModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                if (business != null)
                {
                    MessageBox.Show($"Mã lĩnh vực [{txtCode.Text.Trim()}] đã tồn tại!", "Thông báo");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên lĩnh vực!", "Thông báo");
                return false;
            }
            return true;
        }


        bool saveData()
        {
            if (!validate())
            {
                return false;
            }
            BusinessFieldModel business = new BusinessFieldModel();
            if (id > 0)
            {
                business = SQLHelper<BusinessFieldModel>.FindByID(id);
            }
            business.Code = txtCode.Text.Trim();
            business.STT = TextUtils.ToInt(txtSTT.Value);
            business.Name = txtName.Text.Trim();
            business.Note = txtNote.Text.Trim();

            if (business.ID > 0)
            {
                SQLHelper<BusinessFieldModel>.Update(business);
            }
            else
            {
                SQLHelper<BusinessFieldModel>.Insert(business);
            }

            return true;
        }

        private void frmBusinessField_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
