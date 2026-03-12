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
    public partial class frmBillDocumentExport : _Forms
    {
        public BillDocumentExportModel bDEModel = new BillDocumentExportModel();
        public int billExportID;
        public string code;

        DataTable dtBDExport = new DataTable();
        public frmBillDocumentExport()
        {
            InitializeComponent();
        }

        private void frmBillDocumentExport_Load(object sender, EventArgs e)
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

        void loadDataMaster()
        {
            dtBDExport = TextUtils.LoadDataFromSP("spGetBillDocumentExport", "A", new string[] { "@BillExportID" }, new object[] { billExportID });
            grdMaster.DataSource = dtBDExport;
        }

        void loadDataLog()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dtLog = TextUtils.LoadDataFromSP("spGetBillDocumentExportLog", "B", new string[] { "@BillDocumentExportID" }, new object[] { id });
            grdDataLog.DataSource = dtLog;
        }

        bool CheckValidete()
        {
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colStatus));
                string code = TextUtils.ToString(grvMaster.GetRowCellValue(i, colCode));
                string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote));

                if (status <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn trạng thái chứng từ [{code}] !", "Thông báo");
                    return false;
                }
                if (status == 2 && string.IsNullOrWhiteSpace(note))
                {
                    MessageBox.Show($"Vui điền lý do hủy nhận chứng từ [{code}] !", "Thông báo");
                    return false;
                }
            }
            return true;
        }

        bool save()
        {
            grvMaster.FocusedRowHandle = -1;

            if (!CheckValidete()) return false;

            bool isStatus = false;
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colStatus));
                string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote));

                BillDocumentExportModel bdeModel = SQLHelper<BillDocumentExportModel>.FindByID(id);
                if (bdeModel == null) continue;
                if (bdeModel.Status == status)
                {
                    if (bdeModel.Note == note)
                    {
                        if (bdeModel.Status == 2)
                        {
                            isStatus = true;
                        }
                        continue;
                    }
                }

                bdeModel.Status = status;
                bdeModel.Note = note;
                bdeModel.LogDate = DateTime.Now;

                if (bdeModel.ID > 0)
                {
                    SQLHelper<BillDocumentExportModel>.Update(bdeModel);
                }
                if (bdeModel.Status == 2)
                {
                    isStatus = true;
                }

                BillDocumentExportLogModel logModel = new BillDocumentExportLogModel();
                logModel.BillDocumentExportID = bdeModel.ID;
                logModel.Status = bdeModel.Status;
                logModel.Note = bdeModel.Note;
                logModel.LogDate = bdeModel.LogDate;
                SQLHelper<BillDocumentExportLogModel>.Insert(logModel);
            }

            BillExportModel model = SQLHelper<BillExportModel>.FindByID(billExportID);
            if (model != null)
            {
                if (isStatus)
                {
                    model.BillDocumentExportType = 2;
                }
                else
                {
                    model.BillDocumentExportType = 1;
                }

                if (model.ID > 0)
                {
                    BillExportBO.Instance.Update(model);
                }
            }

            return true;
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

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataLog();
        }
    }
}
