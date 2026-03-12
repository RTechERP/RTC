using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmDocumentImportExport : _Forms
    {
        public frmDocumentImportExport()
        {
            InitializeComponent();
        }

        private void btnAddPN_Click(object sender, EventArgs e)
        {
            frmDocumentImportExportDetail frm = new frmDocumentImportExportDetail();
            frm.PN = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDocumentImport();
            }
        }
        private void btnAddPX_Click(object sender, EventArgs e)
        {
            frmDocumentImportExportDetail frm = new frmDocumentImportExportDetail();
            frm.PX = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDocumentExport();
            }
        }

        private void frmDocumentInportExport_Load(object sender, EventArgs e)
        {
            loadDocumentImport();
            loadDocumentExport();
        }

        void loadDocumentImport()
        {
            //DataTable dt = TextUtils.Select("Select * from DocumentImport");
            //DataTable dt = TextUtils.LoadDataFromSP("Select * from DocumentImport");

            List<DocumentImportModel> list = SQLHelper<DocumentImportModel>.FindByAttribute("IsDeleted", 0);
            grdDataPN.DataSource = list;
        }
        void loadDocumentExport()
        {
            List<DocumentExportModel> list = SQLHelper<DocumentExportModel>.FindByAttribute("IsDeleted", 0);
            grdDataPX.DataSource = list;
        }

        private void btnEditPN_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvDataPN.GetFocusedRowCellValue(colIDPN));
            if (ID == 0) return;
            DocumentImportModel model = (DocumentImportModel)DocumentImportBO.Instance.FindByPK(ID);
            frmDocumentImportExportDetail frm = new frmDocumentImportExportDetail();
            frm.PN = true;
            frm.DImodel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDocumentImport();
            }
        }
        private void btnEditPX_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataPX.GetFocusedRowCellValue(colIDPX));
            if (id <= 0) return;

            DocumentExportModel model = SQLHelper<DocumentExportModel>.FindByID(id);
            if (model == null) return;

            frmDocumentImportExportDetail frm = new frmDocumentImportExportDetail();
            frm.PX = true;
            frm.DEmodel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDocumentExport();
            }
        }

        private void btnDeletePN_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvDataPN.GetFocusedRowCellValue(colDocumentImportCodePN));
            int ID = TextUtils.ToInt(grvDataPN.GetFocusedRowCellValue(colIDPN));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format($"Bạn có muốn xóa hồ sơ [{code}] không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DocumentImportModel model = (DocumentImportModel)DocumentImportBO.Instance.FindByPK(ID);
                model.IsDeleted = true;
                DocumentImportBO.Instance.Update(model);
                grvDataPN.DeleteSelectedRows();
            }
        }

        private void btnDelPX_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataPX.GetFocusedRowCellValue(colIDPX));
            string code = TextUtils.ToString(grvDataPX.GetFocusedRowCellValue(colCodePX));
            if (id <= 0) return;

            if (MessageBox.Show(string.Format($"Bạn có muốn xóa hồ sơ [{code}] không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DocumentExportModel model = SQLHelper<DocumentExportModel>.FindByID(id);
                if (model == null) return;
                model.IsDeleted = true;

                SQLHelper<DocumentExportModel>.Update(model);
                grvDataPX.DeleteSelectedRows();
            }
        }
    }
}
