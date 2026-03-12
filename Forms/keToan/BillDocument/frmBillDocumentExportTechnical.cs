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
    public partial class frmBillDocumentExportTechnical : _Forms
    {
        public BillDocumentExportTechnicalModel bdeTechModel = new BillDocumentExportTechnicalModel();
        public int BillExportTechnicalID;
        public string code;

        DataTable dtDataMaster = new DataTable();
        public frmBillDocumentExportTechnical()
        {
            InitializeComponent();
        }

        private void frmBillDocumentExportTechnical_Load(object sender, EventArgs e)
        {
            this.Text += " - " + code;
            LoadCboStatus();
            loadDataMaster();
            loadDataLog();
        }
        void LoadCboStatus()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Name = "Đã nhận"},
                new {ID = 2, Name = "Đã hủy nhận"},
                new {ID = 3, Name = "Không có"}
            };
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";
            cboStatus.DataSource = list;
        }

        private void loadDataMaster()
        {
            dtDataMaster = TextUtils.LoadDataFromSP("spGetBillDocumentExportTechnical", "A", new string[] { "@BillExportTechnicalID" }, new object[] { BillExportTechnicalID });
            grdMaster.DataSource = dtDataMaster;
        }

        private void loadDataLog()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillDocumentExportTechnicalLog", "B", new string[] { "@BillDocumentExportTechnicalID" }, new object[] { id });
            grdDataLog.DataSource = dt;
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataLog();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                loadDataMaster();
                loadDataLog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool save()
        {
            grvMaster.FocusedRowHandle = -1;
            bool isStatus = false;
            if (!CheckValidate()) return false;

            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colStatus));
                string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote));

                if (id <= 0) continue;

                BillDocumentExportTechnicalModel techModel = SQLHelper<BillDocumentExportTechnicalModel>.FindByID(id);
                if (techModel == null) continue;
                if (techModel.Status == status)
                {
                    if (techModel.Note == note)
                    {
                        if (techModel.Status == 2)
                        {
                            isStatus = true;
                        }
                        continue;
                    }
                }
                techModel.Status = status;
                techModel.Note = note;
                techModel.LogDate = DateTime.Now;
                if (techModel.ID > 0)
                {
                    SQLHelper<BillDocumentExportTechnicalModel>.Update(techModel);
                }
                if (techModel.Status == 2)
                {
                    isStatus = true;
                }

                BillDocumentExportTechnicalLogModel logModel = new BillDocumentExportTechnicalLogModel();
                logModel.BillDocumentExportTechnicalID = techModel.ID;
                logModel.Status = techModel.Status;
                logModel.Note = techModel.Note;
                logModel.LogDate = techModel.LogDate;
                if (techModel.ID > 0)
                {
                    SQLHelper<BillDocumentExportTechnicalLogModel>.Insert(logModel);
                }
            }

            BillExportTechnicalModel beModel = SQLHelper<BillExportTechnicalModel>.FindByID(BillExportTechnicalID);
            if (beModel != null)
            {
                if (isStatus)
                {

                    beModel.BillDocumentExportType = 2;
                }
                else
                {

                    beModel.BillDocumentExportType = 1;
                }

                if (beModel.ID > 0)
                {
                    BillExportTechnicalBO.Instance.Update(beModel);
                }
            }

            return true;
        }

        private bool CheckValidate()
        {
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colStatus));
                string code = TextUtils.ToString(grvMaster.GetRowCellValue(i, colCode));
                string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote));

                if (status <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập trạng thái chứng từ [{code}] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                if (status == 2 && string.IsNullOrWhiteSpace(note))
                {
                    MessageBox.Show($"Vui lòng nhập lý do hủy nhận chứng từ [{code}] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        private void frmBillDocumentExportTechnical_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmBillDocumentExportTechnicalDetail oldForm = Application.OpenForms["frmBillDocumentExportTechnicalDetail"] as frmBillDocumentExportTechnicalDetail;
            if (oldForm != null)
            {
                oldForm.RefresData();
            }
        }
    }
}
