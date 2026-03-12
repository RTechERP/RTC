using BMS.Business;
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
    public partial class frmTaxEmployeePositionDetail : _Forms
    {
        public TaxEmployeePositionModel taxEPModel = new TaxEmployeePositionModel();
        public frmTaxEmployeePositionDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeePositionTaxDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            txtCode.Text = taxEPModel.Code;
            txtName.Text = taxEPModel.Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        bool validate()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text.Trim()))
            {
                MessageBox.Show("Hãy nhập mã chức vụ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var checkModel = SQLHelper<TaxEmployeePositionModel>.FindByExpression(new Expression("Code", txtCode.Text.Trim()).And(new Expression("ID", taxEPModel.ID, "<>")));
                if (checkModel.Count > 0)
                {
                    MessageBox.Show($"Mã chức vụ [{txtCode.Text.Trim()}] này đã tồn tại.\nVui lòng nhập mã chức vụ khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtName.Text.Trim()))
            {
                MessageBox.Show("Hãy nhập tên chức vụ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        bool save()
        {
            if (!validate()) return false;

            taxEPModel.Code = TextUtils.ToString(txtCode.Text);
            taxEPModel.Name = TextUtils.ToString(txtName.Text);

            if(taxEPModel.ID > 0)
            {
                TaxEmployeePositionBO.Instance.Update(taxEPModel);
            }
            else
            {
                TaxEmployeePositionBO.Instance.Insert(taxEPModel);
            }
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            save();
            txtCode.Text = "";
            txtName.Text = "";
            taxEPModel = new TaxEmployeePositionModel();
        }

        private void frmTaxEmployeePositionDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
