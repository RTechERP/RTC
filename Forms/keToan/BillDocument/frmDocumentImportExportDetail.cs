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
    public partial class frmDocumentImportExportDetail : _Forms
    {
        public bool PN;
        public DocumentImportModel DImodel = new DocumentImportModel();
        public bool PX;
        public DocumentExportModel DEmodel = new DocumentExportModel();
        public frmDocumentImportExportDetail()
        {
            InitializeComponent();
        }

        private void frmDocumentImputDetails_Load(object sender, EventArgs e)
        {
            this.Text += PN ? " - PN" : PX ? " - PX" : "";
            loadData();
        }

        private void loadData()
        {
            if (PN)
            {
                txtCode.Text = DImodel.DocumentImportCode;
                txtName.Text = DImodel.DocumentImportName;
            }
            if (PX)
            {
                txtCode.Text = DEmodel.Code;
                txtName.Text = DEmodel.Name;
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã chứng từ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (PN)
                {
                    var exp1 = new Expression("DocumentImportCode", txtCode.Text.Trim());
                    var exp2 = new Expression("ID", DImodel.ID, "<>");
                    var exp3 = new Expression("IsDeleted", 1, "<>");
                    var documents = SQLHelper<DocumentImportModel>.FindByExpression(exp1.And(exp2).And(exp3));
                    if (documents.Count > 0)
                    {
                        MessageBox.Show($"Mã chứng từ [{txtCode.Text}] đã tồn tại.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                if (PX)
                {
                    var exp1 = new Expression("Code", txtCode.Text.Trim());
                    var exp2 = new Expression("ID", DEmodel.ID, "<>");
                    var exp3 = new Expression("IsDeleted", 1, "<>");
                    var documents = SQLHelper<DocumentExportModel>.FindByExpression(exp1.And(exp2).And(exp3));
                    if (documents.Count > 0)
                    {
                        MessageBox.Show($"Mã chứng từ [{txtCode.Text}] đã tồn tại.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }

            if(txtName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Tên chứng từ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        bool SaveData()
        {
            if (!ValidateForm()) return false;
            if (PN)
            {
                DImodel.DocumentImportCode = TextUtils.ToString(txtCode.Text.Trim());
                DImodel.DocumentImportName = TextUtils.ToString(txtName.Text.Trim()); 

                if (DImodel.ID > 0)
                {
                   SQLHelper<DocumentImportModel>.Update(DImodel);
                }
                else
                {
                    SQLHelper<DocumentImportModel>.Insert(DImodel);
                }
            }
            if (PX)
            {
                DEmodel.Code = TextUtils.ToString(txtCode.Text.Trim());
                DEmodel.Name = TextUtils.ToString(txtName.Text.Trim());

                if (DEmodel.ID > 0)
                {
                    SQLHelper<DocumentExportModel>.Update(DEmodel);
                }
                else
                {
                    SQLHelper<DocumentExportModel>.Insert(DEmodel);
                }
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
            txtCode.Text = "";
            txtName.Text = "";
            if (PN)
            {
                DImodel = new DocumentImportModel();
            }
            if (PX)
            {
                DEmodel = new DocumentExportModel();
            }
        }
    }
}
