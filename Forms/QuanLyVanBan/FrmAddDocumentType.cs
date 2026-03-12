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

    public partial class FrmAddDocumentType : _Forms
    {
        public DocumentTypeModel Group = new DocumentTypeModel();
        int flag;
        public FrmAddDocumentType()
        {
            InitializeComponent();
        }
        bool ValidateForm()
        {
            if (txtCode.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        /// <summary>
        /// void loadData
        /// </summary>
        private void loadData()
        {
            if (Group.ID > 0)
            {
                txtCode.Text = Group.Code.ToString();
                txtName.Text = Group.Name;

            }
        }
        /// <summary>
        /// void lưu dữ liệu
        /// </summary>
        private void SaveGroup()
        {
            if (!ValidateForm())
            {
                FrmAddDocumentType frmAddDocumentType = new FrmAddDocumentType();
                return;
            }
            Group.Name = txtName.Text;
            Group.Code = txtCode.Text.Trim();
            if (Group.ID > 0)
            {
                DocumentTypeBO.Instance.Update(Group);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (flag == 1)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                //Check trùng mã loại văn bản	
                DocumentTypeModel documentTypeModel = (DocumentTypeModel)DocumentTypeBO.Instance.FindByCode("Code", txtCode.Text.Trim());
                if (documentTypeModel != null)
                {
                    MessageBox.Show("Mã code đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                DocumentTypeBO.Instance.Insert(Group);
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Clear();
                txtName.Clear();
                if (flag == 1)
                {
                    this.DialogResult = DialogResult.OK;
                }


            }
        }



        private void FrmAddDocumentType_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void FrmAddDocumentType_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            flag = 0;
            SaveGroup();



        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            flag = 1;
            SaveGroup();
            

        }
    }
}
