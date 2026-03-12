using BMS.Business;
using BMS.Model;
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
    public partial class frmProjectPartListTypeDetail : _Forms
    {
        public int id = 0;
        public frmProjectPartListTypeDetail()
        {
            InitializeComponent();
        }

        private void frmCategoryPartListDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            if(id > 0)
            {
                var model = SQLHelper<ProjectPartListTypeModel>.FindByID(id);
                txtCode.Text = model.Code;
                txtName.Text = model.Name;
            }
        }
        bool validate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã danh mục!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên danh mục!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool save()
        {
            try
            {
                if (!validate()) return false;
                ProjectPartListTypeModel model = new ProjectPartListTypeModel();
                if (id > 0)
                {
                    model = SQLHelper<ProjectPartListTypeModel>.FindByID(id);
                }
                model.Code = txtCode.Text.Trim();
                model.Name = txtName.Text.Trim();
                if (model.ID > 0)
                {
                    ProjectPartListTypeBO.Instance.Update(model);
                }
                else
                {
                    ProjectPartListTypeBO.Instance.Insert(model);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            this.DialogResult = DialogResult.OK;
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            id = 0;
            txtCode.Clear();
            txtName.Clear();

        }
    }
}