using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBillDocumentImportTechnical : _Forms
    {
        public int billImportTechnicalID;
        public BillDocumentImportTechnicalModel BDITModel = new BillDocumentImportTechnicalModel();
        public string code;

        DataTable dtDocumentTech = new DataTable();
        public frmBillDocumentImportTechnical()
        {
            InitializeComponent();
        }

        private void frmBillDocumentImportTechnical_Load(object sender, EventArgs e)
        {
            this.Text += " - " + code;
            LoadCboStatus();
            LoadCboStatusPur();
            LoadDataMaster();
            LoadDataLog();
        }

        void LoadCboStatus()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Name = "Đã nhận"},
                new {ID = 2, Name = "Đã hủy nhận"},
                new {ID = 3, Name = "Không cần"}
            };
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";
            cboStatus.DataSource = list;
        }
        void LoadCboStatusPur()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Name = "Đã bàn giao"},
                new {ID = 2, Name = "Hủy bàn giao"},
                new {ID = 3, Name = "Không cần"}
            };
            cboStatusPur.DisplayMember = "Name";
            cboStatusPur.ValueMember = "ID";
            cboStatusPur.DataSource = list;
        }

        void LoadDataMaster()
        {
            dtDocumentTech = TextUtils.LoadDataFromSP("spGetBillDocumentImportTechnical", "A"
                                                    , new string[] { "@BillImportTechnicalID" }
                                                    , new object[] { billImportTechnicalID });
            grdMaster.DataSource = dtDocumentTech;
        }

        void LoadDataLog()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillDocumentImportTechnicalLog", "B", new string[] { "@BillDocumentImportTechnicalID" }, new object[] { ID });
            grdDataLog.DataSource = dt;
        }

        bool checkValidate()
        {
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatus));
                string reasonCancel = TextUtils.ToString(grvMaster.GetRowCellValue(i, colReasonCancel));
                string code = TextUtils.ToString(grvMaster.GetRowCellValue(i, colDocumentImportCode));

                //if (status <= 0)
                //{
                //    MessageBox.Show($"Vui lòng nhập Trạng thái của chứng từ [{code}]!", "Thông báo");
                //    return false;
                //}
                //else 
                if (status == 2 && string.IsNullOrWhiteSpace(reasonCancel))
                {
                    MessageBox.Show($"Vui lòng nhập Lý do hủy của chứng từ [{code}]!", "Thông báo");
                    return false;
                }
            }
            return true;
        }

        bool save()
        {
            grvMaster.FocusedRowHandle = -1;

            if (!checkValidate()) return false;

            bool isStatus = false;

            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
                int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatus));
                int statusPur = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatusPur));
                string reasonCancel = TextUtils.ToString(grvMaster.GetRowCellValue(i, colReasonCancel));
                string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote)).Trim();  //NTA B -- update 04/08/2025

                BillDocumentImportTechnicalModel techModel = SQLHelper<BillDocumentImportTechnicalModel>.FindByID(id);
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
                techModel.StatusPurchase = statusPur;
                techModel.ReasonCancel = reasonCancel;
                techModel.DateReceive = DateTime.Now;
                techModel.EmployeeReceiveID = Global.EmployeeID; //NTA B - update 06/08/2025
                techModel.LogDate = DateTime.Now;
                if (techModel.ID > 0)
                {
                    SQLHelper<BillDocumentImportTechnicalModel>.Update(techModel);
                }
                if (techModel.Status == 2)
                {
                    isStatus = true;
                }

                BillDocumentImportTechnicalLogModel logModel = new BillDocumentImportTechnicalLogModel();
                logModel.BillDocumentImportTechnicalID = techModel.ID;
                logModel.Status = techModel.Status;
                logModel.LogDate = techModel.LogDate;
                logModel.Note = $"LÝ DO HUỶ: {techModel.ReasonCancel}\nGHI CHÚ: {techModel.Note}";
                SQLHelper<BillDocumentImportTechnicalLogModel>.Insert(logModel);
            }

            // cập nhật trạng thái phiếu nhập
            BillImportTechnicalModel biTechModel = SQLHelper<BillImportTechnicalModel>.FindByID(billImportTechnicalID);
            if (biTechModel != null)
            {
                if (isStatus)
                {

                    biTechModel.BillDocumentImportType = 2;
                }
                else
                {

                    biTechModel.BillDocumentImportType = 1;
                }

                if (biTechModel.ID > 0)
                {
                    BillImportTechnicalBO.Instance.Update(biTechModel);
                }
            }


            return true;
        }

        private void frmBillDocumentImportTechnical_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Lấy tham chiếu đến form cũ (frmBillDocumentImportType)
            frmBillDocumentImportTechnicalDetail oldForm = Application.OpenForms["frmBillDocumentImportTechnicalDetail"] as frmBillDocumentImportTechnicalDetail;
            if (oldForm != null)
            {
                oldForm.RefresData();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataLog();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                LoadDataMaster();
                LoadDataLog();
                MessageBox.Show("Lưu thành công!", "Thông báo");
            }
        }
    }
}
