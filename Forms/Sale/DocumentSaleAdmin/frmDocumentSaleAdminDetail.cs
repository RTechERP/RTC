using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGauges.Core.Model;
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
    public partial class frmDocumentSaleAdminDetail : _Forms
    {
        public DocumentModel Group = new DocumentModel();
        public Action SaveEvent;
        public frmDocumentSaleAdminDetail()
        {
            InitializeComponent();
        }

        bool ValidateForm()
        {

            string code = txtCodeDocument.Text.Trim();

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhâp Mã văn bản!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtSTT.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập STT!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNameDocument.Text))
            {
                MessageBox.Show("Vui lòng nhâp Tên văn bản!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            Expression exp1 = new Expression("Code", code);
            Expression exp2 = new Expression("GroupType", 2);
            Expression exp3 = new Expression("IsDeleted", 0);
            Expression exp4 = new Expression("ID", Group.ID,"<>");
            DocumentModel documentTypeModel = SQLHelper<DocumentModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).FirstOrDefault();
            if (documentTypeModel != null)
            {
                MessageBox.Show("Mã code đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        void loadDepartment()
        {
            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll();
            //cboDepartment.DataSource = listDepart;
            //cboDepartment.ValueMember = "ID";
            //cboDepartment.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepart;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            listDepart.Insert(0, new DepartmentModel()
            {
                ID = 0,
                Name = "Văn bản chung",
                Code = ""
            });

            cboDepartment.EditValue = Global.DepartmentID;
            //if (Group.DepartmentID == 0)
            //{
            //}
        }
        void loadDocumentType()
        {
            List<DocumentTypeModel> listDocumentType = SQLHelper<DocumentTypeModel>.FindAll();

            listDocumentType.Insert(0, new DocumentTypeModel()
            {
                ID = 0,
                Name = "--Chọn loại--",
                //Code = ""
            });
            cbxDocumentType.DataSource = listDocumentType;
            cbxDocumentType.ValueMember = "ID";
            cbxDocumentType.DisplayMember = "Name";

            cbxDocumentType.SelectedValue = 47;

        }
        private void loadData()
        {
            if (Group.ID > 0)
            {
                txtCodeDocument.Text = Group.Code.ToString();
                txtNameDocument.Text = Group.NameDocument;
                txtSTT.Text = Group.STT.ToString();
                dtDatePromulqate.Value = Group.DatePromulgate.Value;
                dtDocumentEffetive.Value = Group.DateEffective.Value;
                cbxDocumentType.SelectedValue = Group.DocumentTypeID;
                cboDepartment.EditValue = Group.DepartmentID;
            }
            else
            {
                Expression exp1 = new Expression(DocumentModel_Enum.DocumentTypeID, cbxDocumentType.SelectedValue);
                Expression exp2 = new Expression(DocumentModel_Enum.DepartmentID, TextUtils.ToInt(cboDepartment.EditValue));
                Expression exp3 = new Expression(DocumentModel_Enum.GroupType, 2);
                Expression exp4 = new Expression(DocumentModel_Enum.IsDeleted, 0);
                var documents = SQLHelper<DocumentModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));

                int maxStt = documents.Count <= 0 ? 1 : TextUtils.ToInt(documents.Max(x => x.STT)) + 1;
                txtSTT.Value = maxStt;
            }
        }
        //bool checkCode(string code)
        //{

        //    return true;
        //}
        bool save()
        {
            if (!ValidateForm())
            {
                return false;
            }
            //if(!checkCode(txtCodeDocument.Text.Trim()))
            //{
            //    return false;
            //}
            Group.STT = TextUtils.ToInt(txtSTT.Text);
            Group.Code = txtCodeDocument.Text.Trim();
            Group.NameDocument = txtNameDocument.Text;
            Group.DocumentTypeID = TextUtils.ToInt(cbxDocumentType.SelectedValue);
            Group.DatePromulgate = dtDatePromulqate.Value;
            Group.DateEffective = dtDocumentEffetive.Value;
            Group.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
            Group.GroupType = 2;

            if (Group.ID > 0)
            {
                //Group.UpdatedBy = Global.AppCodeName;
                //Group.UpdatedDate = DateTime.Now;
                SQLHelper<DocumentModel>.Update(Group);

                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Group.CreatedBy = Global.AppCodeName;
                //Group.CreatedDate = DateTime.Now;
                Group.GroupType = 2;
                SQLHelper<DocumentModel>.Insert(Group);
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodeDocument.Text = "";
                txtNameDocument.Text = "";
                txtSTT.Text = "";
            }
            SaveEvent();
            return true;
        }
        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (save()) this.Close();
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (save())
            {
                Group = new DocumentModel();
                loadData();
            }
        }

        private void frmDocumentSaleAdminDetail_Load(object sender, EventArgs e)
        {
            loadDepartment();
            loadDocumentType();
            loadData();
            //cboDepartment.ReadOnly = true;
        }
    }
}
