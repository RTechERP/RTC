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
    public partial class frmGroupSales : _Forms
    {
        public GroupSalesModel groupSales = new GroupSalesModel();
        public frmGroupSales()
        {
            InitializeComponent();
        }

        private void frmGroupSales_Load(object sender, EventArgs e)
        {
            if (groupSales.ID > 0)
            {
                txtCode.Text = TextUtils.ToString(groupSales.GroupSalesCode);
                txtName.Text = TextUtils.ToString(groupSales.GroupSalesName);
            }
        }
        bool save()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã nhóm !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Tên nhóm !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int ID = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID From GroupSales Where GroupSalesCode=N'{txtCode.Text.Trim()}' Or GroupSalesName=N'{txtName.Text.Trim()}'"));
            if (ID > 0)
            {
                MessageBox.Show("Mã nhóm hoặc tên nhóm đã được sử dụng !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            GroupSalesModel model = new GroupSalesModel();
            if (groupSales.ID > 0)
                 model = (GroupSalesModel)GroupSalesBO.Instance.FindByPK(groupSales.ID);
            model.GroupSalesCode = txtCode.Text.Trim();
            model.GroupSalesName = txtName.Text.Trim();
            if (groupSales.ID > 0)
                GroupSalesBO.Instance.Update(model);
            else
                GroupSalesBO.Instance.Insert(model);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!save()) return;
            this.Close();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            save();
            txtCode.Clear();
            txtName.Clear();
        }

        private void frmGroupSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
