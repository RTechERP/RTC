using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
    public partial class frmBillDocumentImport : _Forms
    {
        public int billDocumentImportID;
        public BillDocumentImportModel billDIModel = new BillDocumentImportModel();
        private List<int> modifiedRows = new List<int>();
        public string code;


        DataTable dtDocument = new DataTable();
        public frmBillDocumentImport()
        {
            InitializeComponent();

            this.Text = "HỒ SƠ CHỨNG TỪ";
            this.Shown += frmBillDocumentImport_Shown;
        }

        private void frmBillDocumentImport_Shown(object sender, EventArgs e)
        {
            this.code = code;

            this.Text = "HỒ SƠ CHỨNG TỪ - " + code;
        }

        private void frmBillDocument_Load(object sender, EventArgs e)
        {
            LoadStatusPur(); // 16725
            LoadCboStatus(); // 16725
            LoadDataMaster();
            LoadDataLog();
        }

        void LoadCboStatus() // 16725
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            dt.Rows.Add(1, "Đã nhận");
            dt.Rows.Add(2, "Đã hủy nhận");
            dt.Rows.Add(3, "Không cần");

            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";
            cboStatus.DataSource = dt;
        }


        void LoadDataLog()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            int dcocumentImportID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colDocumentImportID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillDocumentImportLog", "B", 
                new string[] { "@billDocumentImportID", "@DocumentImportID" }, 
                new object[] { id, dcocumentImportID });
            //DataTable allLogs = new DataTable();
            dt.DefaultView.Sort = "LogDate DESC";
            dt = dt.DefaultView.ToTable();
            grdDataLog.DataSource = dt;
        }


        bool CheckValidate()
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
                 
                
                if (status == 2 && string.IsNullOrEmpty(reasonCancel))
                {
                    MessageBox.Show($"Vui lòng nhập Lý do huỷ của chứng từ [{code}]!", "Thông báo");
                    return false;
                }
            }
            return true;
        }

        void loadBillDocumentImportType()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillDocumentImport", "A"
                , new string[] { "@BillImportID" }
                , new object[] { billDocumentImportID });


            bool allStatusOne = true;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                if (row["DocumentStatus"].ToString() != "1" && row["DocumentStatus"].ToString() != "3")
                {
                    allStatusOne = false;
                    break;
                }
            }

            int billDocumentImportType = allStatusOne ? 1 : 2;
            TextUtils.ExcuteSQL($"UPDATE BillImport SET BillDocumentImportType = {billDocumentImportType} WHERE ID = {billDocumentImportID}");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                LoadDataMaster();
                LoadDataLog();
            }
        }

        private void grvMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int rowIndex = e.RowHandle;
            if (!modifiedRows.Contains(rowIndex))
            {
                modifiedRows.Add(rowIndex);
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataLog();
        }

        //void LoadDataLogAll()
        //{
        //    List<DataTable> logTables = new List<DataTable>();

        //    for (int i = 0; i < grvMaster.RowCount; i++)
        //    {
        //        int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
        //        DataTable dtLog = TextUtils.LoadDataFromSP("spGetBillDocumentImportLog", "B",
        //            new string[] { "@billDocumentImportID" },
        //            new object[] { id });

        //        logTables.Add(dtLog);
        //    }

        //    DataTable allLogs = new DataTable();

        //    foreach (DataTable logTable in logTables)
        //    {
        //        allLogs.Merge(logTable);
        //    }

        //    if (allLogs.Rows.Count > 0)
        //    {
        //        allLogs.DefaultView.Sort = "LogDate DESC";
        //        allLogs = allLogs.DefaultView.ToTable();
        //    }

        //    grdDataLog.DataSource = allLogs;
        //}

        private void frmBillDocumentImport_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Lấy tham chiếu đến form cũ (frmBillDocumentImportType)
            frmBillDocumentImportType oldForm = Application.OpenForms["frmBillDocumentImportType"] as frmBillDocumentImportType;
            if (oldForm != null)
            {
                // Gọi phương thức ReloadData() của form cũ để tải lại dữ liệu
                oldForm.ReloadData();
            }
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void grdDataLog_Click(object sender, EventArgs e)
        {

        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            //SearchLookUpEdit searchLook = (SearchLookUpEdit)sender;
            //var dataSelected = searchLook.GetSelectedDataRow();
            //int status = TextUtils.ToInt(dataSelected.GetType().GetProperty("ID").GetValue(dataSelected));
            //int id = TextUtils.ToInt(dataSelected.GetType().GetProperty("ID").GetValue(dataSelected));

            //grvMaster.SetFocusedRowCellValue(colDocumentStatus, status);
            //dtDocument.Rows[grvMaster.FocusedRowHandle]["Note"] = dtDocument.Rows[grvMaster.FocusedRowHandle]["Note", DataRowVersion.Original];

            //grvMaster.FocusedColumn = colNote;

            //if (status != 2)
            //{
            //    grvMaster.SetFocusedRowCellValue(colNote, "");
            //}
            //else
            //{
            //    // int index = dtDocument.Rows.IndexOf(dtDocument)
            //    dtDocument.Rows[grvMaster.FocusedRowHandle]["Note"] = dtDocument.Rows[grvMaster.FocusedRowHandle]["Note", DataRowVersion.Original];
            //}

        }


        //=========================================== Lee Min Khooii Update ========================================================
        void LoadDataMaster()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetBillDocumentImport", "A"
            //                                        , new string[] { "@BillImportID" }
            //                                        , new object[] { billDocumentImportID }); 

            dtDocument = TextUtils.LoadDataFromSP("spGetAllDocumentImportPONCC", "A"
                                                    , new string[] { "@BillImportID" }
                                                    , new object[] { billDocumentImportID });
            grdMaster.DataSource = dtDocument;
        }
        //============================
        bool SaveData()
        {
            grvMaster.FocusedRowHandle = -1;

            if (!CheckValidate()) return false;

            SaveDocument();

            //bool isStatus = false; //------------new

            ////string sqlInsert = "";
            //for (int i = 0; i < grvMaster.RowCount; i++)
            //{
            //    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
            //    int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatus));//new
            //    string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote)).Trim();//new

            //    DocumentImportPONCCModel document = SQLHelper<DocumentImportPONCCModel>.FindByID(id);
            //    if (document == null) continue;

            //    //-----------new
            //    if (document.Status == status)
            //    {
            //        if (document.Note == note)
            //        {
            //            if (document.Status == 2)
            //            {
            //                isStatus = true;
            //            }
            //            continue;
            //        }
            //    }
            //    //----------end

            //    document.Status = status;// new

            //    document.Status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatus));
            //    document.DocumentImportID = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentImportID));
            //    document.DateRecive = DateTime.Now;
            //    document.EmployeeReciveID = Global.EmployeeID;
            //    //document.Note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote)).Trim();
            //    document.Note = note;// new
            //    if (document.ID > 0)
            //    {
            //        SQLHelper<DocumentImportPONCCModel>.Update(document);
            //    }

            //    //------NEW------
            //    if (document.Status == 2)
            //    {
            //        isStatus = true;
            //    }
            //    //-------END-------


            //    BillDocumentImportLogModel log = new BillDocumentImportLogModel();
            //    log.BillDocumentImportID = document.ID;
            //    log.DocumentStatus = document.Status;
            //    log.LogDate = document.DateRecive;
            //    log.Note = document.Note;
            //    SQLHelper<BillDocumentImportLogModel>.Insert(log);



            //    //sqlInsert += $"Insert into BillDocumentImportLog(BillDocumentImportID, DocumentStatus, LogDate, Note) values ({id}, {newStatus}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', N'{note}')";
            //}
            ////=========================================== Lee Min Khooii Update  END========================================================


            ////---------------------------New------------------- sửa cập nhật trạng thái phiếu nhập
            //// nhớ đổi billDocumentImportID cho đúng
            //if (billDocumentImportID > 0)
            //{
            //    BillImportModel billImportModel = SQLHelper<BillImportModel>.FindByID(billDocumentImportID);
            //    if (billImportModel != null)
            //    {
            //        if (isStatus)
            //        {
            //            billImportModel.BillDocumentImportType = 2;
            //        }
            //        else
            //        {
            //            billImportModel.BillDocumentImportType = 1;
            //        }

            //        if (billImportModel.ID > 0)
            //        {
            //            BillImportBO.Instance.Update(billImportModel);
            //        }
            //    }
            //}


            //-----------------------END-----------------------------------

            //LoadDataMaster();
            //LoadDataLog();
            return true;

            //TextUtils.ExcuteSQL(sqlInsert);

            //try
            //{
            //    foreach (int rowIndex in modifiedRows)
            //    {
            //        int id = TextUtils.ToInt(grvMaster.GetRowCellValue(rowIndex, colID));
            //        int newStatus = TextUtils.ToInt(grvMaster.GetRowCellValue(rowIndex, colDocumentStatus));
            //        string note = TextUtils.ToString(grvMaster.GetRowCellValue(rowIndex, colNote));
            //        string documentImportCode= TextUtils.ToString(grvMaster.GetRowCellValue(rowIndex, colDocumentImportCode));
            //        if (newStatus == 2 && note == "")
            //        {
            //            MessageBox.Show($"Vui lòng nhập lý do hủy nhận chứng từ [{documentImportCode}] !", "Thông báo");
            //            return false;
            //        }
            //        if (newStatus == 1 || newStatus == 3 || newStatus == 0)
            //        {
            //            note = "";
            //        }

            //        TextUtils.ExcuteScalar("spUpdateBillDocumentImportStatus",
            //            new string[] { "@ID", "@DocumentStatus", "@LogDate", "@Note" },
            //            new object[] { id, newStatus, DateTime.Now, note });
            //        // INSERT LỊCH SỬ TRẠNG THÁI LogDate 
            //        TextUtils.ExcuteSQL($"Insert into BillDocumentImportLog(BillDocumentImportID, DocumentStatus, LogDate, Note) values ({id}, {newStatus}, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', N'{note}')");
            //    }

            //    modifiedRows.Clear();
            //    LoadDataMaster();
            //    loadBillDocumentImportType();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
        }


        void SaveDocument()
        {
            grvMaster.CloseEditor();
            //if (!CheckValidate()) return false;'

            DataTable dtChange = dtDocument.GetChanges();
            if (dtChange == null) return;

            foreach (DataRow row in dtChange.Rows)
            {
                int id = TextUtils.ToInt(row["ID"]);
                //int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatus));//new
                //string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote)).Trim();//new

                DocumentImportPONCCModel document = SQLHelper<DocumentImportPONCCModel>.FindByID(id);
                //if (document.ID <= 0) continue;
                //document.PONCCID = TextUtils.ToInt(row["PONCCID"]);
                document.DocumentImportID = TextUtils.ToInt(row["DocumentImportID"]);
                document.Status = TextUtils.ToInt(row["DocumentStatus"]);
                document.ReasonCancel = TextUtils.ToString(row["ReasonCancel"]).Trim();
                document.Note = TextUtils.ToString(row["Note"]).Trim();
                document.DateRecive = DateTime.Now;
                document.EmployeeReciveID = Global.EmployeeID;
                document.BillImportID = billDocumentImportID;


                document.StatusPurchase = TextUtils.ToInt(row[colDocumentStatusPur.FieldName]); // 16725
                //document.UpdatedBy = Global.AppUserName; // 16725

                document.StatusHR = TextUtils.ToInt(row["DocumentStatusHR"]);
                if (document.ID <= 0)
                {
                    SQLHelper<DocumentImportPONCCModel>.Insert(document);
                }
                else
                {
                    SQLHelper<DocumentImportPONCCModel>.Update(document);
                }


                BillDocumentImportLogModel log = new BillDocumentImportLogModel();
                log.BillDocumentImportID = document.ID;
                log.DocumentStatus = TextUtils.ToInt(document.Status);
                log.LogDate = document.DateRecive;
                log.Note = $"LÝ DO HUỶ: {document.ReasonCancel}\nGHI CHÚ: {document.Note}";
                log.DocumentImportID = document.DocumentImportID;
                SQLHelper<BillDocumentImportLogModel>.Insert(log);
            }


            //for (int i = 0; i < grvMaster.RowCount; i++)
            //{
            //    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, "ID"));
            //    //int status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatus));//new
            //    //string note = TextUtils.ToString(grvMaster.GetRowCellValue(i, colNote)).Trim();//new

            //    DocumentImportPONCCModel document = SQLHelper<DocumentImportPONCCModel>.FindByID(id);
            //    //if (document.ID <= 0) continue;
            //    //document.PONCCID = TextUtils.ToInt(row["PONCCID"]);
            //    document.DocumentImportID = TextUtils.ToInt(grvMaster.GetRowCellValue(i, "DocumentImportID"));
            //    document.Status = TextUtils.ToInt(grvMaster.GetRowCellValue(i, "DocumentStatus"));
            //    document.ReasonCancel = TextUtils.ToString(grvMaster.GetRowCellValue(i, "ReasonCancel")).Trim();
            //    document.Note = TextUtils.ToString(grvMaster.GetRowCellValue(i, "Note")).Trim();
            //    document.DateRecive = DateTime.Now;
            //    document.EmployeeReciveID = Global.EmployeeID;
            //    document.BillImportID = billDocumentImportID;


            //    document.StatusPurchase = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colDocumentStatusPur)); // 16725
            //    //document.UpdatedBy = Global.AppUserName; // 16725

            //    document.StatusHR = TextUtils.ToInt(grvMaster.GetRowCellValue(i, "DocumentStatusHR"));
            //    if (document.ID <= 0)
            //    {
            //        SQLHelper<DocumentImportPONCCModel>.Insert(document);
            //    }
            //    else
            //    {
            //        SQLHelper<DocumentImportPONCCModel>.Update(document);
            //    }


            //    BillDocumentImportLogModel log = new BillDocumentImportLogModel();
            //    log.BillDocumentImportID = document.ID;
            //    log.DocumentStatus = TextUtils.ToInt(document.Status);
            //    log.LogDate = document.DateRecive;
            //    log.Note = $"LÝ DO HUỶ: {document.ReasonCancel}\nGHI CHÚ: {document.Note}";
            //    log.DocumentImportID = document.DocumentImportID;
            //    SQLHelper<BillDocumentImportLogModel>.Insert(log);
            //}
        }

        private void grvMaster_ShownEditor(object sender, EventArgs e)
        {
            
        }

        private void grvMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (Global.IsAdmin && Global.EmployeeID <= 0) return;
            if (grvMaster.FocusedColumn == colDocumentStatusHR)
            {
                e.Cancel = !(Global.DepartmentID == 6); //HR được sửa
            }
            else if (grvMaster.FocusedColumn == colDocumentStatus)
            {
                e.Cancel = !(Global.DepartmentID == 5); //kế toán được sửa
            }
            else if (grvMaster.FocusedColumn == colDocumentStatusPur)
            {
                e.Cancel = !(Global.DepartmentID == 4); // pur được sửa
            }
        }

        #region Update chức năng phiếu xuất 16725
        void LoadStatusPur()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            dt.Rows.Add(1, "Đã bàn giao");
            dt.Rows.Add(2, "Hủy bàn giao");
            dt.Rows.Add(3, "Không cần");

            cboStatusPur.DisplayMember = "Name";
            cboStatusPur.ValueMember = "ID";
            cboStatusPur.DataSource = dt;
        }
        #endregion
    }
}
