using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmDocumentInport : _Forms
    {
        public frmDocumentInport()
        {
            InitializeComponent();
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            DocumentImportModel model = new DocumentImportModel();
            frmDocumentImputDetails frm = new frmDocumentImputDetails();
            frm.DImodel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDocumentImport();
            }
        }

        private void frmDocumentInport_Load(object sender, EventArgs e)
        {
            loadDocumentImport();
        }

        void loadDocumentImport()
        {
            //DataTable dt = TextUtils.Select("Select * from DocumentImport");
            //DataTable dt = TextUtils.LoadDataFromSP("Select * from DocumentImport");


            List<DocumentImportModel> list = SQLHelper<DocumentImportModel>.FindByAttribute("IsDeleted", 0);
            grdData.DataSource = list;
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            editDataDocumentImput();
        }

        private void editDataDocumentImput()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            DocumentImportModel model = (DocumentImportModel)DocumentImportBO.Instance.FindByPK(ID);
            frmDocumentImputDetails frm = new frmDocumentImputDetails();
            frm.DImodel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDocumentImport();
            }
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            //string DocunmentImportName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colDocumentImportName));
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;
            //if (MessageBox.Show(string.Format($"Bạn có muốn xóa hồ sơ [{DocunmentImportName}] hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    DocumentImportBO.Instance.Delete(ID);
            //    grvData.DeleteSelectedRows();
            //}

            string DocunmentImportName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colDocumentImportName));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format($"Bạn có muốn xóa hồ sơ [{DocunmentImportName}] không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DocumentImportModel model = (DocumentImportModel)DocumentImportBO.Instance.FindByPK(ID);
                model.IsDeleted = true;
                DocumentImportBO.Instance.Update(model);
                grvData.DeleteSelectedRows();
            }
        }

    }
}
