
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
//using Forms.QuanLyVanBan;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;      // để dùng FileInfo
using System.Data;
//using Forms.QuanLyVanBan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.CodeParser;
using System.Globalization;
using System.Runtime.InteropServices;
using DevExpress.Utils;

namespace BMS
{
    public partial class FrmDocumnet : _Forms
    {

        private string fileCopy;
        private List<string> listFilename = new List<string>();
        public FrmDocumnet()
        {
            InitializeComponent();
            //InitializeOpenFileDialog();


        }
        #region LOAD DATA

        //Lấy danh sách loại văn bản
        void dataDocumentType()
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.Select("SELECT * FROM dbo.DocumentType");
            List<DocumentTypeModel> listDocType = SQLHelper<DocumentTypeModel>.FindAll();

            grdDocumentType.DataSource = listDocType;

        }
        //Lấy danh sách văn bản
        void dataDocument()
        {

            //int departID = -1;
            //DataTable dt = new DataTable();
            //int ID = TextUtils.ToInt(grvDocumentType.GetFocusedRowCellValue(colIDDocumentType));
            //if (ID == 0) return;
            //departID = TextUtils.ToInt(cboSearchDepartment.EditValue);

            //var checkVaueDepart = cboSearchDepartment.EditValue;
            //if(checkVaueDepart == null)
            //{
            //    departID = -1;
            //}

            int ID = TextUtils.ToInt(grvDocumentType.GetFocusedRowCellValue(colIDDocumentType));
            int departID = -1;
            if (cboSearchDepartment.EditValue != null && !string.IsNullOrEmpty(TextUtils.ToString(cboSearchDepartment.EditValue)))
            {
                departID = TextUtils.ToInt(cboSearchDepartment.EditValue);
            }

            DataTable dt = TextUtils.LoadDataFromSP("spGetDocument", "A", new string[] { "@IDDocumentType", "@DepartmentID" }, new object[] { ID, departID });
            grdDocument.DataSource = dt;

        }
        //Lấy danh sách file theo văn bản 
        void dataDocumentFile()
        {
            // DataTable dt = new DataTable();
            int ID = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
            if (ID == 0) return;

            //dt = TextUtils.Select("SELECT * FROM dbo.DocumentFile WHERE DocumentID = " + ID + "");
            //grdDocumentFile.DataSource = dt;

            List<DocumentFileModel> listDocFile = SQLHelper<DocumentFileModel>.FindByAttribute("DocumentID", ID);
            grdDocumentFile.DataSource = listDocFile;
        }

        //Lấy danh sách phòng ban
        void dataDeparment()
        {
            List<DepartmentModel> listDepart = SQLHelper<DepartmentModel>.FindAll();
            //DepartmentModel nullDepartOption = new DepartmentModel()
            //{
            //    ID = 0,
            //    Name = "Văn bản chung"
            //};
            //DepartmentModel allDepartOption = new DepartmentModel()
            //{
            //    ID = -1,
            //    Name = "Tất cả"
            //};
            //listDepart.Insert(0, nullDepartOption);      
            //listDepart.Insert(0, allDepartOption);

            listDepart.Insert(0, new DepartmentModel() { ID = 0, Name = "Văn bản chung" });
            cboSearchDepartment.Properties.DataSource = listDepart;
            cboSearchDepartment.Properties.DisplayMember = "Name";
            cboSearchDepartment.Properties.ValueMember = "ID";
        }

        private void grvDocumentType_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataDocument();
        }

        private void grvDocument_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataDocumentFile();
        }

        #endregion
        #region CRUD LOẠI VĂN BẢN


        //Thêm mới loại văn bản
        private void btnNewDocType_Click(object sender, EventArgs e)
        {
            FrmAddDocumentType frm = new FrmAddDocumentType();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dataDocumentType();

            }
        }
        //xóa loại văn bản
        private void btnDeleteDocType_Click(object sender, EventArgs e)
        {
            int RowIndex = grvDocument.RowCount;
            if (RowIndex > 0)
            {
                MessageBox.Show("Loại văn bản đang được sử dụng không thể xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = TextUtils.ToInt(grvDocumentType.GetFocusedRowCellValue(colIDDocumentType));
            var namedoctype = TextUtils.ToString(grvDocumentType.GetFocusedRowCellValue(colNameDocumentType));
            if (id == 0) return;
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa tên loại " + namedoctype + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                grvDocumentType.DeleteSelectedRows();
                //delete row from database or datagridview...
                DocumentTypeBO.Instance.Delete(id);
                //MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataDocumentType();
            }

        }
        /// <summary>
        /// hàm sửa lại dữ liệu loại văn bản
        /// </summary>

        private void editDataDocumentTypeGroup()
        {

            var id = TextUtils.ToInt(grvDocumentType.GetFocusedRowCellValue(colIDDocumentType));
            if (id == 0) return;
            DocumentTypeModel model = (DocumentTypeModel)DocumentTypeBO.Instance.FindByPK(id);
            FrmAddDocumentType frm = new FrmAddDocumentType();
            frm.Group = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dataDocumentType();
            }
        }

        //Sửa loại văn bản
        private void grdDocumentType_DoubleClick(object sender, EventArgs e)
        {
            editDataDocumentTypeGroup();

        }

        #endregion


        #region CRUD VĂN BẢN


        private void btnAddDoc_Click(object sender, EventArgs e)
        {
            FrmAddDocument frmAddDocument = new FrmAddDocument();
            if (frmAddDocument.ShowDialog() == DialogResult.OK)
            {
                dataDocument();

            }

        }
        //Sửa văn bản
        private void grdDocument_DoubleClick(object sender, EventArgs e)
        {
            editDataDocumentGroup();

        }
        /// <summary>
        /// hàm sửa lại dữ liệu  văn bản
        /// </summary>
        private void editDataDocumentGroup()
        {

            var id = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            //DocumentModel model = (DocumentModel)DocumentBO.Instance.FindByPK(id);
            DocumentModel model = SQLHelper<DocumentModel>.FindByID(id);
            FrmAddDocument frm = new FrmAddDocument();
            frm.Group = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dataDocument();
            }
        }
        /// <summary>
        /// hàm xóa văn bản
        /// </summary>
        private void btnDeleteDocument_Click(object sender, EventArgs e)
        {
            int RowIndex = grvDocumentFile.RowCount;
            if (RowIndex > 0)
            {
                MessageBox.Show("Văn bản đang được sử dụng không thể xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var id = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
            var namedoc = TextUtils.ToString(grvDocument.GetFocusedRowCellValue(colDocumentName));
            if (id == 0) return;
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa  văn bản " + namedoc + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {

                grvDocument.DeleteSelectedRows();
                //delete row from database or datagridview...
                //DocumentBO.Instance.Delete(id);
                //MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var myDict = new Dictionary<string, object>()
                {
                    {DocumentModel_Enum.IsDeleted.ToString(),true },
                    {DocumentModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                    {DocumentModel_Enum.UpdatedDate.ToString(),DateTime.Now }
                };

                SQLHelper<DocumentModel>.UpdateFieldsByID(myDict, id);
                dataDocument();
            }
        }


        #endregion

        #region CRUD File Document


        /// <summary>
        /// Thêm file or mutiplite file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDocFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            if (o.ShowDialog() != DialogResult.OK) return;

            for (int i = 0; i < o.FileNames.Count(); i++)
            {
                try
                {
                    string path = System.IO.Path.GetDirectoryName(o.FileNames[i]);
                    string name = System.IO.Path.GetFileNameWithoutExtension(o.FileNames[i]);
                    string extension = System.IO.Path.GetExtension(o.FileNames[i]);

                    string newfile = name + "_" + DateTime.Now.ToString("ddMMyyHHmm") + extension;

                    System.IO.File.Copy(o.FileNames[i], path + "\\" + newfile);
                    string fileCopy = path + "\\" + newfile;

                    DocumentFileModel modelFile = (DocumentFileModel)DocumentFileBO.Instance.FindByCode("FileName", newfile);
                    if (modelFile != null)
                    {
                        MessageBox.Show("Tên File " + name + " đã tồn tại trên hệ thống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    modelFile = new DocumentFileModel();
                    modelFile.FileName = newfile;
                    modelFile.FileNameOrigin = name + extension;
                    modelFile.DocumentID = TextUtils.ToInt(grvDocument.GetFocusedRowCellValue(colID));
                    DocumentFileBO.Instance.Insert(modelFile);
                    listFilename.Add(fileCopy);
                    //Đẩy lên sever 
                    bool isFinish = i == o.FileNames.Count() - 1;
                    UploadFile(fileCopy, isFinish);

                    //Load lại data
                    dataDocumentFile();


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

        /// <summary>
        /// upload mutiplite file
        /// </summary>
        /// <param name="fileName"></param>
        public void UploadFile(string fileName, bool isFinish)
        {
            try
            {
                string API_UPLOAD = "http://192.168.1.2:8083/api/Home/uploaddocument";
                var client = new WebClient();
                client.Headers.Add("Content-Type", "binary/octet-stream");
                client.UploadFileAsync(new Uri(API_UPLOAD), fileName);
                if (isFinish)
                {
                    client.UploadFileCompleted += Client_UploadFileCompleted;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {

                foreach (var item in listFilename)
                {
                    System.IO.File.Delete(item);
                }
                listFilename.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Xóa toàn bộ file được chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteAllDocFile_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvDocumentFile.GetSelectedRows();
            if (RowIndex.Count() < 1)
            {
                MessageBox.Show("Vui lòng chọn để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa những file vừa chọn ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {


                for (int i = 0; i < RowIndex.Length; i++)
                {
                    int id = TextUtils.ToInt(grvDocumentFile.GetRowCellValue(RowIndex[i], colIDFileName));
                    DocumentFileBO.Instance.Delete(id);
                }
                dataDocumentFile();
            }


        }
        /// <summary>
        /// Tải những file được chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloadAllFileDoc_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvDocumentFile.GetSelectedRows();
            if (RowIndex.Count() < 1)
            {
                MessageBox.Show("Vui lòng chọn để download file !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FolderBrowserDialog o = new FolderBrowserDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    for (int i = 0; i < RowIndex.Length; i++)
                    {

                        string path = System.IO.Path.GetFullPath(o.SelectedPath) + "\\";
                        string url = "http://192.168.1.2:8083/api/Upload/RTCDocument/";
                        string fileName = TextUtils.ToString(grvDocumentFile.GetRowCellValue(RowIndex[i], colFileName));
                        WebClient cln = new WebClient();
                        cln.DownloadFileAsync(new System.Uri(url + fileName), path + fileName);

                        Process.Start(path + fileName);
                    }
                    //MessageBox.Show("Download file thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        private void FrmDocumnet_Load(object sender, EventArgs e)
        {
            //Load loại văn bản trước
            dataDocumentType();

            //load phòng ban
            dataDeparment();
        }


        private void cboSearchDepartment_EditValueChanged(object sender, EventArgs e)
        {
            dataDocument();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataDocument();

        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            var frm = new frmDocumentImportExcel();
            var result = frm.ShowDialog();
            if (result != DialogResult.Cancel || result != DialogResult.Abort) dataDocument();
        }

        private void grvDocument_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (gridView == null) return;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(gridView.GetFocusedRowCellValue(gridView.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
        //NXLuong Update 18/09/25




        //private void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    var dt = grdDocument.DataSource as DataTable;
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        MessageBox.Show("Không có dữ liệu để xuất.");
        //        return;
        //    }

        //    string templatePath = @"D:\Update_RTC\ExportFrmDocument\FrmDocumnet\MauVBPhatHanh.xlsx";

        //    string savePath;
        //    using (var sfd = new SaveFileDialog
        //    {
        //        Title = "Lưu file xuất",
        //        Filter = "Excel (*.xlsx)|*.xlsx",
        //        FileName = $"VBPhatHanh_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
        //    })
        //    {
        //        if (sfd.ShowDialog() != DialogResult.OK) return;
        //        savePath = sfd.FileName;
        //    }

        //    Excel.Application xlApp = null;
        //    Excel.Workbook xlWb = null;
        //    Excel.Worksheet xlWs = null;

        //    try
        //    {
        //        xlApp = new Excel.Application();
        //        xlWb = xlApp.Workbooks.Open(templatePath);
        //        xlWs = (Excel.Worksheet)xlWb.Sheets[1];

        //        // Nếu template có 3 ô ngày-tháng-năm header ở C3,E3,G3 và bạn muốn set:
        //        var now = DateTime.Now;
        //        xlWs.Range["C3"].Value2 = now.Day.ToString("00");
        //        xlWs.Range["E3"].Value2 = now.Month.ToString("00");
        //        xlWs.Range["G3"].Value2 = now.Year.ToString();

        //        // Helper
        //        bool TryGetDate(object v, out DateTime d)
        //        {
        //            d = default;
        //            if (v == null || v == DBNull.Value) return false;
        //            if (v is DateTime dd) { d = dd; return true; }
        //            var s = v.ToString();
        //            return DateTime.TryParse(s, new CultureInfo("vi-VN"), DateTimeStyles.None, out d)
        //                || DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
        //        }
        //        string GetStr(DataRow r, string col) => dt.Columns.Contains(col) && r[col] != DBNull.Value ? r[col].ToString() : "";

        //        // Map cột yêu cầu
        //        // A: STT | B: Code | C: DocumentTypeCode | D: NameDocumentType
        //        // E: DepartmentCode | F: DepartmentName | G: EmployeeSignCode | H: EmployeeSignName
        //        // I/J/K: day/month/year của DatePromulgate
        //        // L/M/N: day/month/year của DateEffective
        //        // O: AffectedScope
        //        int startRow = 5;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            int lastRow = startRow + dt.Rows.Count - 1;
        //            xlWs.Range[$"B{startRow}:B{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            xlWs.Range[$"C{startRow}:C{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            xlWs.Range[$"F{startRow}:F{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            xlWs.Range[$"G{startRow}:G{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            xlWs.Range[$"H{startRow}:H{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            xlWs.Range[$"O{startRow}:O{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            var r = dt.Rows[i];
        //            int row = startRow + i;

        //            // A: STT
        //            xlWs.Cells[row, 1] = i + 1;

        //            // B..H: text fields
        //            xlWs.Cells[row, 2] = GetStr(r, "Code");
        //            xlWs.Cells[row, 3] = GetStr(r, "DocumentTypeCode");
        //            xlWs.Cells[row, 4] = GetStr(r, "NameDocumentType");
        //            xlWs.Cells[row, 5] = GetStr(r, "DepartmentCode");
        //            xlWs.Cells[row, 6] = GetStr(r, "DepartmentName");
        //            xlWs.Cells[row, 7] = GetStr(r, "EmployeeSignCode");
        //            xlWs.Cells[row, 8] = GetStr(r, "EmployeeSignName");

        //            // IJK: DatePromulgate
        //            if (TryGetDate(r.Table.Columns.Contains("DatePromulgate") ? r["DatePromulgate"] : null, out var dp))
        //            {
        //                xlWs.Cells[row, 9] = dp.Day;   // I
        //                xlWs.Cells[row, 10] = dp.Month; // J
        //                xlWs.Cells[row, 11] = dp.Year;  // K
        //            }
        //            else
        //            {
        //                xlWs.Cells[row, 9] = "";
        //                xlWs.Cells[row, 10] = "";
        //                xlWs.Cells[row, 11] = "";
        //            }

        //            // LMN: DateEffective
        //            if (TryGetDate(r.Table.Columns.Contains("DateEffective") ? r["DateEffective"] : null, out var de))
        //            {
        //                xlWs.Cells[row, 12] = de.Day;   // L
        //                xlWs.Cells[row, 13] = de.Month; // M
        //                xlWs.Cells[row, 14] = de.Year;  // N
        //            }
        //            else
        //            {
        //                xlWs.Cells[row, 12] = "";
        //                xlWs.Cells[row, 13] = "";
        //                xlWs.Cells[row, 14] = "";
        //            }

        //            // O: AffectedScope
        //            xlWs.Cells[row, 15] = GetStr(r, "AffectedScope");
        //        }

        //        xlWb.SaveAs(savePath);
        //        MessageBox.Show("Xuất thành công.");

        //        xlWb.Close();
        //        xlApp.Quit();

        //        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        //        {
        //            FileName = savePath,
        //            UseShellExecute = true
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (xlWs != null) Marshal.ReleaseComObject(xlWs);
        //        if (xlWb != null) Marshal.ReleaseComObject(xlWb);
        //        if (xlApp != null) Marshal.ReleaseComObject(xlApp);
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}



        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Lấy DataTable gốc
            var dtAll = grdDocument.DataSource as DataTable;
            if (dtAll == null || dtAll.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            // Lấy đúng dữ liệu đang HIỂN THỊ trên GridView (sau filter/sort)
            var view = (GridView)grdDocument.MainView;
            DataTable dt = dtAll.Clone();
            for (int i = 0; i < view.RowCount; i++)
            {
                int vh = view.GetVisibleRowHandle(i);
                if (!view.IsDataRow(vh)) continue;
                var dr = view.GetDataRow(vh);
                dt.ImportRow(dr);
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu sau khi lọc.", "Thông báo");
                return;
            }

            // Đường dẫn template cố định
            //string templatePath = @"D:\Update_RTC\ExportFrmDocument\FrmDocumnet\MauVBPhatHanh.xlsx";
            string templatePath = Path.Combine(Application.StartupPath, "MauVBPhatHanh.xlsx");

            // Chọn nơi lưu
            string savePath;
            using (var sfd = new SaveFileDialog
            {
                Title = "Lưu file xuất",
                Filter = "Excel (*.xlsx)|*.xlsx",
                FileName = $"VBPhatHanh_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                savePath = sfd.FileName;
            }

            Excel.Application xlApp = null;
            Excel.Workbook xlWb = null;
            Excel.Worksheet xlWs = null;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                try
                {
                    xlApp = new Excel.Application();
                    xlWb = xlApp.Workbooks.Open(templatePath);
                    xlWs = (Excel.Worksheet)xlWb.Sheets[1];

                    // Ghi ngày-tháng-năm header nếu template có C3,E3,G3
                    var now = DateTime.Now;
                    xlWs.Range["C3"].Value2 = now.Day.ToString("00");
                    xlWs.Range["E3"].Value2 = now.Month.ToString("00");
                    //   xlWs.Range["G3"].Value2 = now.Year.ToString();

                    // Helpers
                    bool TryGetDate(object v, out DateTime d)
                    {
                        d = default;
                        if (v == null || v == DBNull.Value) return false;
                        if (v is DateTime dd) { d = dd; return true; }
                        var s = v.ToString();
                        return DateTime.TryParse(s, new CultureInfo("vi-VN"), DateTimeStyles.None, out d)
                            || DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                    }
                    string GetStr(DataRow r, string col)
                        => dt.Columns.Contains(col) && r[col] != DBNull.Value ? r[col].ToString() : "";

                    //NXLuong update sort 29/9/25
                    var sortCol = "SortDatePromulgate";
                    if (!dt.Columns.Contains(sortCol)) dt.Columns.Add(sortCol, typeof(DateTime));

                    foreach (DataRow r in dt.Rows)
                    {
                        if (TryGetDate(dt.Columns.Contains("DatePromulgate") ? r["DatePromulgate"] : null, out var d))
                            r[sortCol] = d;
                        else
                            r[sortCol] = DateTime.MinValue;
                    }
                    var dv = new DataView(dt);
                    dv.Sort = $"{sortCol} DESC";
                    dt = dv.ToTable();
                    dt.Columns.Remove(sortCol);
                    //NXLuong end update sort 29/9/25

                    // Map cột:
                    // A: STT | B: Code | C: DocumentTypeCode | D: NameDocumentType
                    // E: DepartmentCode | F: DepartmentName | G: EmployeeSignCode | H: EmployeeSignName
                    // I/J/K: day/month/year DatePromulgate
                    // L/M/N: day/month/year DateEffective
                    // O: AffectedScope
                    int startRow = 5;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var r = dt.Rows[i];
                        int row = startRow + i;

                        // A
                        xlWs.Cells[row, 1] = i + 1;

                        // B..H
                        xlWs.Cells[row, 2] = GetStr(r, "Code");
                        xlWs.Cells[row, 3] = GetStr(r, "DocumentTypeCode");
                        xlWs.Cells[row, 4] = GetStr(r, "NameDocument");
                        xlWs.Cells[row, 5] = GetStr(r, "DepartmentCode");
                        xlWs.Cells[row, 6] = GetStr(r, "DepartmentName");
                        xlWs.Cells[row, 7] = GetStr(r, "EmployeeSignCode");
                        xlWs.Cells[row, 8] = GetStr(r, "EmployeeSignName");

                        // IJK: DatePromulgate
                        if (TryGetDate(dt.Columns.Contains("DatePromulgate") ? r["DatePromulgate"] : null, out var dp))
                        {
                            xlWs.Cells[row, 9] = dp.Day;
                            xlWs.Cells[row, 10] = dp.Month;
                            xlWs.Cells[row, 11] = dp.Year;
                        }
                        else
                        {
                            xlWs.Cells[row, 9] = "";
                            xlWs.Cells[row, 10] = "";
                            xlWs.Cells[row, 11] = "";
                        }

                        // LMN: DateEffective
                        if (TryGetDate(dt.Columns.Contains("DateEffective") ? r["DateEffective"] : null, out var de))
                        {
                            xlWs.Cells[row, 12] = de.Day;
                            xlWs.Cells[row, 13] = de.Month;
                            xlWs.Cells[row, 14] = de.Year;
                        }
                        else
                        {
                            xlWs.Cells[row, 12] = "";
                            xlWs.Cells[row, 13] = "";
                            xlWs.Cells[row, 14] = "";
                        }

                        // O
                        xlWs.Cells[row, 15] = GetStr(r, "AffectedScope");
                    }

                    // Căn giữa các cột
                    int lastRow = startRow + dt.Rows.Count - 1;
                    xlWs.Range[$"B{startRow}:B{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWs.Range[$"C{startRow}:C{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWs.Range[$"F{startRow}:F{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWs.Range[$"G{startRow}:G{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWs.Range[$"H{startRow}:H{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWs.Range[$"O{startRow}:O{lastRow}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    xlWb.SaveAs(savePath);
                    //MessageBox.Show($"Xuất file thành công!", "Thông báo");

                    xlWb.Close();
                    xlApp.Quit();

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = savePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi");

                }
                finally
                {
                    if (xlWs != null) Marshal.ReleaseComObject(xlWs);
                    if (xlWb != null) Marshal.ReleaseComObject(xlWb);
                    if (xlApp != null) Marshal.ReleaseComObject(xlApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

    }
}
