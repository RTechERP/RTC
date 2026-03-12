using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectWorkerDetail : _Forms
    {
        public int ID = 0;
        public int projectID = 0;
        public int ProjectWorkerVersionID = 0;

        public Action SaveEvent;
        public frmProjectWorkerDetail()
        {
            InitializeComponent();
        }

        private void frmProjectWorkerDetail_Load(object sender, EventArgs e)
        {
            //loadType();
            loadData();
        }
        //void loadType()
        //{
        //    cboProjectWorkerType.Properties.DataSource = SQLHelper<ProjectWorkerTypeModel>.FindAll();
        //    cboProjectWorkerType.Properties.ValueMember = "ID";
        //    cboProjectWorkerType.Properties.DisplayMember = "Name";

        //}
        void loadData()
        {
            if (ID > 0)
            {
                ProjectWorkerModel model = SQLHelper<ProjectWorkerModel>.FindByID(ID);
                txtTT.Text = model.TT;
                txtWorkContent.Text = model.WorkContent;
                txtAmountPeople.EditValue = model.AmountPeople;
                txtNumberOfDay.EditValue = model.NumberOfDay;
                txtTotalWorkforce.EditValue = model.TotalWorkforce;
                txtPrice.EditValue = model.Price;
                txtTotalPrice.EditValue = model.TotalPrice;
                //cboProjectWorkerType.EditValue = model.ProjectWorkerTypeID;
            }
        }

        bool validate()
        {
            if (string.IsNullOrEmpty(txtTT.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập TT!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (TextUtils.ToInt(cboProjectWorkerType.EditValue) <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Loại nhân công dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtWorkContent.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung công việc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtAmountPeople.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số người!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtNumberOfDay.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số ngày!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtPrice.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Đơn giá nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var exp1 = new Expression("ProjectID", projectID);
            var exp2 = new Expression("TT", txtTT.Text.Trim());
            var exp3 = new Expression("ID", ID, "<>");
            var exp4 = new Expression("ProjectWorkerVersionID", ProjectWorkerVersionID);
            var checkTT = SQLHelper<ProjectWorkerModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
            if (checkTT.Count > 0)
            {
                MessageBox.Show("TT đã tồn tại! Vui lòng nhập lại.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool save()
        {
            try
            {
                if (!validate()) return false;

                ProjectWorkerModel model = new ProjectWorkerModel();
                if (ID > 0)
                {
                    model = SQLHelper<ProjectWorkerModel>.FindByID(ID);
                }
                model.TT = txtTT.Text.Trim();
                model.WorkContent = txtWorkContent.Text;
                model.AmountPeople = TextUtils.ToInt(txtAmountPeople.EditValue);
                model.NumberOfDay = TextUtils.ToDecimal(txtNumberOfDay.EditValue);
                model.TotalWorkforce = TextUtils.ToDecimal(txtTotalWorkforce.EditValue);
                model.Price = TextUtils.ToDecimal(txtPrice.EditValue);
                model.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.EditValue);
                //model.ProjectWorkerTypeID = TextUtils.ToInt(cboProjectWorkerType.EditValue);
                model.ProjectID = projectID;
                model.ProjectWorkerVersionID = ProjectWorkerVersionID;
                string ttParent = model.TT.Substring(0, model.TT.LastIndexOf('.') == -1 ? 0 : model.TT.LastIndexOf('.'));
                var exp1 = new Expression("ProjectID", projectID);
                var exp2 = new Expression("TT", ttParent);
                //var exp3 = new Expression("ProjectWorkerTypeID", TextUtils.ToInt(cboProjectWorkerType.EditValue));
                ProjectWorkerModel checkParent = SQLHelper<ProjectWorkerModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                if (checkParent != null && checkParent.ID > 0)
                {
                    model.ParentID = checkParent.ID;
                }
                else
                {
                    //MessageBox.Show("TT không đúng! Vui lòng nhập lại");
                    //return false;
                    model.ParentID = 0;
                }
                if (model.ID > 0)
                {
                    ProjectWorkerBO.Instance.Update(model);
                }
                else
                {
                    ProjectWorkerBO.Instance.Insert(model);
                }
                SaveEvent();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            this.DialogResult = DialogResult.OK;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private bool lastCharWasDecimal = false;

        private void txtTT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu không phải là số, không phải là dấu chấm (.) và không phải là phím Control (các phím như Backspace, Delete, Arrow, ...)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Kiểm tra nếu ký tự là dấu chấm (.) và trước đó đã có dấu chấm
            if (e.KeyChar == '.' && lastCharWasDecimal)
            {
                e.Handled = true;
            }

            // Cập nhật trạng thái cho ký tự hiện tại
            lastCharWasDecimal = (e.KeyChar == '.');
        }


        private void txtNumberOfDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAmountPeople_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            txtTT.Clear();
            txtWorkContent.Clear();
            txtAmountPeople.Text = "0";
            txtNumberOfDay.Text = "0";
            txtPrice.Text = "0";
        }

        private void txtAmountPeople_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAmountPeople.Text))
            {
                txtTotalWorkforce.Text = "0";
                txtTotalPrice.Text = "0";
                return;
            }
            decimal totalWorkforce = TextUtils.ToDecimal(txtAmountPeople.Text.Trim()) * TextUtils.ToDecimal(txtNumberOfDay.Text.Trim());
            decimal totalPrice = totalWorkforce * TextUtils.ToDecimal(txtPrice.Text.Trim());
            txtTotalWorkforce.Text = TextUtils.ToString(totalWorkforce);
            txtTotalPrice.Text = TextUtils.ToString(totalPrice);
        }

        private void txtNumberOfDay_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNumberOfDay.Text))
            {
                txtTotalWorkforce.Text = "0";
                txtTotalPrice.Text = "0";
                return;
            }
            decimal totalWorkforce = TextUtils.ToDecimal(txtAmountPeople.Text.Trim()) * TextUtils.ToDecimal(txtNumberOfDay.Text.Trim());
            decimal totalPrice = totalWorkforce * TextUtils.ToDecimal(txtPrice.Text.Trim());
            txtTotalWorkforce.Text = TextUtils.ToString(totalWorkforce);
            txtTotalPrice.Text = TextUtils.ToString(totalPrice);
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                txtTotalWorkforce.Text = "0";
                txtTotalPrice.Text = "0";
                return;
            }
            decimal totalWorkforce = TextUtils.ToDecimal(txtAmountPeople.Text.Trim()) * TextUtils.ToDecimal(txtNumberOfDay.Text.Trim());
            decimal totalPrice = totalWorkforce * TextUtils.ToDecimal(txtPrice.Text.Trim());
            txtTotalWorkforce.Text = TextUtils.ToString(totalWorkforce);
            txtTotalPrice.Text = TextUtils.ToString(totalPrice);
        }

        //private void btnAddWorkerType_Click(object sender, EventArgs e)
        //{
        //    frmProjectWorkerTypeDetail frm = new frmProjectWorkerTypeDetail();
        //    if(frm.ShowDialog() == DialogResult.OK)
        //    {
        //        loadType();
        //    }
        //}

        private void txtAmountPeople_EditValueChanged(object sender, EventArgs e)
        {
            Calculator();
        }

        private void txtNumberOfDay_EditValueChanged(object sender, EventArgs e)
        {
            Calculator();
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {

            Calculator();
        }

        void Calculator()
        {
            decimal totalWorkforce = TextUtils.ToDecimal(txtAmountPeople.EditValue) * TextUtils.ToDecimal(txtNumberOfDay.EditValue);
            decimal totalPrice = totalWorkforce * TextUtils.ToDecimal(txtPrice.EditValue);
            txtTotalWorkforce.EditValue = totalWorkforce;
            txtTotalPrice.EditValue = totalPrice;
        }

        private void frmProjectWorkerDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;

            }
        }
    }
}