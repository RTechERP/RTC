using BMS;
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
    public partial class FrmAddDocument : _Forms
    {
        public DocumentModel Group = new DocumentModel();
        private int flag;

        public FrmAddDocument()
        {
            InitializeComponent();
        }

        //load phòng ban
        private void loadDepartment()
        {
            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = listDepart;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            listDepart.Insert(0, new DepartmentModel()
            {
                ID = 0,
                Name = "Văn bản chung",
                Code = ""
            });
            if (Group.DepartmentID == 0)
            {
                cboDepartment.EditValue = 0;
            }
        }

        private bool ValidateForm()
        {
            if (txtSTT.Text == "" || txtCodeDocument.Text == "" || txtNameDocument.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbxDocumentType.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại văn bản", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var exp1 = new Expression("Code", txtCodeDocument.Text.Trim());
            var exp2 = new Expression("GroupType", 1);
            var exp3 = new Expression("IsDeleted", 0);
            var exp4 = new Expression(DocumentModel_Enum.ID, Group.ID, "<>");
            DocumentModel documentTypeModel = SQLHelper<DocumentModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).FirstOrDefault();
            if (documentTypeModel != null)
            {
                MessageBox.Show("Mã code đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        private void SaveGroup()
        {
            if (!ValidateForm()) return;
            Group.STT = TextUtils.ToInt(txtSTT.Text);
            Group.Code = txtCodeDocument.Text.Trim();
            Group.NameDocument = txtNameDocument.Text;
            Group.DocumentTypeID = TextUtils.ToInt(cbxDocumentType.SelectedValue);
            Group.DatePromulgate = dtDatePromulqate.Value;
            Group.DateEffective = dtDocumentEffetive.Value;
            Group.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
            Group.GroupType = 1;//ndnhat
            Group.SignedEmployeeID = Global.EmployeeID;
            Group.AffectedScope = txtAffectedScope.Text;
            Group.IsOnWeb = chkIsOnWeb.Checked;
            Group.IsPromulgated = chkPromulgated.Checked;
            if (Group.ID > 0)
            {
                //DocumentBO.Instance.Update(Group);
                SQLHelper<DocumentModel>.Update(Group);
                //MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (flag == 1) this.DialogResult = DialogResult.OK;

            }
            else
            {
                //Check trùng mã loại văn bản
                DocumentModel documentTypeModel = (DocumentModel)DocumentBO.Instance.FindByCode("Code", txtCodeDocument.Text.Trim());
                if (documentTypeModel != null)
                {
                    MessageBox.Show("Mã code đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //DocumentBO.Instance.Insert(Group);
                SQLHelper<DocumentModel>.Insert(Group);
                //MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodeDocument.Text = txtNameDocument.Text = txtSTT.Text = txtAffectedScope.Text = "";
                chkIsOnWeb.Checked = chkPromulgated.Checked = false;
                if (flag == 1) this.DialogResult = DialogResult.OK;

            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            flag = 0;
            SaveGroup();
        }

        private void FrmAddDocument_Load(object sender, EventArgs e)
        {
            //LOAD CBX Loại văn bản
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.DocumentType");
            cbxDocumentType.DataSource = dt;
            cbxDocumentType.DisplayMember = "Name";
            cbxDocumentType.ValueMember = "ID";

            loadData();
            loadDepartment();
        }

        //Load data cho sửa
        private void loadData()
        {
            if (Group.ID > 0)
            {
                txtCodeDocument.Text = Group.Code.ToString();
                txtNameDocument.Text = Group.NameDocument;
                txtSTT.Text = Group.STT.ToString();
                dtDatePromulqate.Value = Group.DatePromulgate.Value;
                dtDocumentEffetive.Value = Group.DateEffective.Value;
                cboDepartment.EditValue = Group.DepartmentID;
                cbxDocumentType.SelectedValue = Group.DocumentTypeID;
                txtAffectedScope.Text = Group.AffectedScope;
                chkIsOnWeb.Checked = TextUtils.ToBoolean(Group.IsOnWeb);
                chkPromulgated.Checked = TextUtils.ToBoolean(Group.IsPromulgated);
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            flag = 1;
            SaveGroup();
        }

        private void FrmAddDocument_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}