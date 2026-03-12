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
    public partial class frmDocumentImputDetails : _Forms
    {
        public DocumentImportModel DImodel = new DocumentImportModel();
        public frmDocumentImputDetails()
        {
            InitializeComponent();
        }

        private void frmDocumentImputDetails_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            txtDocumentImportCode.Text = DImodel.DocumentImportCode;
            txtDocumentImportName.Text = DImodel.DocumentImportName;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtDocumentImportCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã chứng từ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblValidateCodeEdit.Visible = true;
                return false;
            }
            else
            {
                var exp1 = new Expression("DocumentImportCode", txtDocumentImportCode.Text.Trim());
                var exp2 = new Expression("ID", DImodel.ID, "<>");
                var documents = SQLHelper<DocumentImportModel>.FindByExpression(exp1.And(exp2));
                if (documents.Count > 0)
                {
                    MessageBox.Show($"Mã chứng từ [{txtDocumentImportCode.Text}] đã tồn tại.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }


            if(txtDocumentImportName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Tên chứng từ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //lblValidateNameEdit.Visible = true;
                return false;
            }

            //DataTable dt;
            //if (DImodel.ID > 0)
            //{
            //    dt = TextUtils.Select("select top 1 DocumentImportCode from DocumentImportCode where DocumentImportCode = '" + txtDocumentImportCode.Text.Trim() + "' and ID <> " + DImodel.ID);
            //}
            //else
            //{
            //    dt = TextUtils.Select("select top 1 DocumentImportCode from DocumentImport where DocumentImportCode = '" + txtDocumentImportCode.Text.Trim() + "'");

            //}
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show($"Mã chứng từ [{}] đã tồn tại.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            //lblValidateCodeEdit.Visible = false;
            //lblValidateNameEdit.Visible = false;
            return true;
        }

        bool SaveData()
        {
            if (!ValidateForm()) return false;
            try
            {
                DImodel.DocumentImportCode = txtDocumentImportCode.Text.Trim();
                DImodel.DocumentImportName = txtDocumentImportName.Text.Trim();
                //DImodel.CreatedDate = DateTime.Now;

                if (DImodel.ID > 0)
                {
                    DocumentImportBO.Instance.Update(DImodel);
                }
                else
                {
                    DImodel.ID = (int)BillImportBO.Instance.Insert(DImodel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }
        private void btnColseAndSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmDocumentImputDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            txtDocumentImportCode.Text = "";
            txtDocumentImportName.Text = "";
            DImodel = new DocumentImportModel();
        }
    }
}
