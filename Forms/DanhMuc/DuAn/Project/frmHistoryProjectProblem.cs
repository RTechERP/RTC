using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Forms;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using System.Net.Http;

namespace BMS
{
    public partial class frmHistoryProjectProblem : _Forms
    {
        DataTable dtProjectHistoryProblem = new DataTable();
        DataTable dtDetail = new DataTable();
        DataSet dsDetail = new DataSet();
        ArrayList listID = new ArrayList();
        ArrayList listIDDetail = new ArrayList();
        public ProjectModel project = new ProjectModel();
        List<ProjectHistoryProblemDetailModel> listDetail = new List<ProjectHistoryProblemDetailModel>();
        List<GridColumn> listCol = new List<GridColumn>();
        public frmHistoryProjectProblem()
        {
            InitializeComponent();
        }
        public void frmHistoryProjectProblem_Load(object sender, EventArgs e)
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetProjectHistoryProblemDetail", new string[] { "@ProjectID" },
                                                                            new object[] { project.ID });

            listDetail = ds.Tables[1].AsEnumerable().Select(dataRow => new ProjectHistoryProblemDetailModel
            {
                ID = dataRow.Field<int>("ID"),
                ProjectHistoryProblemID = dataRow.Field<int>("ProjectHistoryProblemID"),
                STT = dataRow.Field<int>("STT"),
                Description = dataRow.Field<string>("Description"),
                Status = dataRow.Field<int>("Status"),
                Note = dataRow.Field<string>("Note")
            }).ToList();
            loadData();
            loadStatus();
            loadDetail();


        }
        void loadStatus()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Name = "Phát sinh lỗi"},
                new {ID = 2, Name = "Không phát sinh lỗi"},
                new {ID = 3, Name = "Đang xử lý"},
                new {ID = 4, Name = "Đã xử lý"},
                new {ID = 5, Name = "Phát sinh mới"}
            };
            repositoryItemSearchLookUpEdit1.DataSource = list;
            repositoryItemSearchLookUpEdit1.ValueMember = "ID";
            repositoryItemSearchLookUpEdit1.DisplayMember = "Name";
        }
        void loadDetail()
        {
            int idMaster = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            var listData = listDetail.Where(x => x.ProjectHistoryProblemID == idMaster).ToList();

            grdDetail.DataSource = listData;
        }
        void loadData()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetProjectHistoryProblemDetail", new string[] { "@ProjectID" },
                                                                new object[] { project.ID });
            dtDetail = ds.Tables[0];
            dtProjectHistoryProblem = ds.Tables[2];
            if (listCol.Count > 0)
            {
                //grvData.Columns.Clear();
                foreach (GridColumn item in listCol)
                {
                    grvData.Columns.Remove(item);
                }
                listCol.Clear();
            }
            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                GridColumn col = grvData.Columns.Add();
                string description = TextUtils.ToString(dtDetail.Rows[i]["AllDescriptions"]);

                col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Options.UseForeColor = true;
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.Caption = description;
                col.FieldName = description;
                col.Name = "col" + description;
                col.OptionsColumn.AllowEdit = false;
                col.Visible = false;
                col.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                col.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                col.OptionsColumn.ReadOnly = true;

                listCol.Add(col);
            }

            grdData.DataSource = dtProjectHistoryProblem;
        }
        bool saveData()
        {
            //grvData.CloseEditor();
            //grvDetail.CloseEditor();
            grvData.FocusedRowHandle = -1;
            grvDetail.FocusedRowHandle = -1;
            
            DataTable data = (DataTable)grdData.DataSource;
            DataTable dataChange = data?.GetChanges();
            if (dataChange != null)
            {
                dataChange.AcceptChanges();
                if (!validate(dataChange))
                {
                    return false;
                }
                foreach (DataRow row in dataChange.Rows)
                {
                    int id = TextUtils.ToInt(row["ID"]);
                    ProjectHistoryProblemModel projectHistoryProblem = new ProjectHistoryProblemModel();
                    if (id > 0)
                    {
                        projectHistoryProblem = (ProjectHistoryProblemModel)ProjectHistoryProblemBO.Instance.FindByPK(id);
                    }
                    projectHistoryProblem.STT = TextUtils.ToInt(row["STT"]);
                    projectHistoryProblem.TypeProblem = TextUtils.ToString(row["TypeProblem"]);
                    projectHistoryProblem.ProjectID = project.ID;
                    projectHistoryProblem.ContentError = TextUtils.ToString(row["ContentError"]);
                    projectHistoryProblem.Reason = TextUtils.ToString(row["Reason"]);
                    projectHistoryProblem.Remedies = TextUtils.ToString(row["Remedies"]);
                    projectHistoryProblem.TestMethod = TextUtils.ToString(row["TestMethod"]);
                    projectHistoryProblem.Image = TextUtils.ToString(row["Image"]);
                    projectHistoryProblem.DateProblem = TextUtils.ToDate4(row["DateProblem"]);
                    projectHistoryProblem.DateImplementation = TextUtils.ToDate4(row["DateImplementation"]);
                    projectHistoryProblem.PIC = TextUtils.ToString(row["PIC"]);

                    if (projectHistoryProblem.ID > 0)
                    {
                        ProjectHistoryProblemBO.Instance.Update(projectHistoryProblem);
                    }
                    else
                    {
                        projectHistoryProblem.CreatedBy = Global.LoginName;
                        projectHistoryProblem.CreatedDate = DateTime.Now;
                        projectHistoryProblem.ID = (int)ProjectHistoryProblemBO.Instance.Insert(projectHistoryProblem);
                    }

                    UploadFile(projectHistoryProblem.Image);
                }
            }
            else
            {
                foreach (DataRow row in data.Rows)
                {
                    int id = TextUtils.ToInt(row["ID"]);
                    saveDetail(id);
                }
            }
            if (listID.Count > 0)
            {
                ProjectHistoryProblemBO.Instance.Delete(listID);
                foreach (int item in listID)
                {
                    ProjectHistoryProblemDetailBO.Instance.DeleteByAttribute("ProjectHistoryProblemID", item);
                }
            }
            dtProjectHistoryProblem.AcceptChanges();
            return true;
        }
        void saveDetail(int id)
        {
            grvDetail.FocusedRowHandle = -1;
            //int row = grvData.FocusedRowHandle;
            //grvDetail.CloseEditor();
            var listData = listDetail.Where(x => x.ProjectHistoryProblemID == id).ToList();
            foreach (var item in listData)
            {
                if (item.ID > 0)
                {
                    SQLHelper<ProjectHistoryProblemDetailModel>.Update(item);
                }
                else
                {
                    item.ID = SQLHelper<ProjectHistoryProblemDetailModel>.Insert(item).ID;
                }
            }
        }
        bool validate(DataTable data)
        {
            //DataTable data = (DataTable)grdData.DataSource;
            //if (data != null)
            //{
            //    data.AcceptChanges();
            //}
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(TextUtils.ToString(data.Rows[i][2])))
                {
                    MessageBox.Show($"Vui lòng nhập Loại dòng thứ {i + 1}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrEmpty(TextUtils.ToString(data.Rows[i][3])))
                {
                    MessageBox.Show($"Vui lòng nhập Nội dung lỗi dòng thứ {i + 1}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrEmpty(TextUtils.ToString(data.Rows[i][4])))
                {
                    MessageBox.Show($"Vui lòng nhập Nguyên nhân dòng thứ {i + 1}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            for (int i = 0; i < listDetail.Count; i++)
            {
                List<ProjectHistoryProblemDetailModel> p = SQLHelper<ProjectHistoryProblemDetailModel>.FindByAttribute("Description", listDetail[i].Description);
                ProjectHistoryProblemModel hp = SQLHelper<ProjectHistoryProblemModel>.FindByID(TextUtils.ToInt(listDetail[i].ProjectHistoryProblemID));
                if (listDetail[i].ID <= 0 && p.Count > 0)
                {
                    MessageBox.Show($"Mô tả [{p.First().Description}] đã tồn tại ở Nội dung lỗi [{hp.ContentError}], vui lòng nhập lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }


        public async void UploadFile(string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(filename)) return;
                DateTime? createdDate = project.CreatedDate;
                if (!createdDate.HasValue) return;
                //string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectCode));
                //int year = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatedDate)).Year;
                string pathUpload = $@"\\192.168.1.190\duan\Projects\{createdDate.Value.Year}\{project.ProjectCode}\TaiLieuChung\TongHopPhatSinh\Image";

                
                FileInfo file = new FileInfo(filename);
                if (file.Length < 0) return;

                //using var fileStream = file.OpenReadStream();
                var fileStream = new FileStream(file.FullName, FileMode.Open);
                byte[] bytes = new byte[file.Length];
                fileStream.Read(bytes, 0, (int)file.Length);
                var byteArrayContent = new ByteArrayContent(bytes);

                var client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(byteArrayContent, "file", file.Name);

                var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                var result = await client.PostAsync(url, content);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //SQLHelper<PaymentOrderFileModel>.Insert(fileOrder);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnCloseAndSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                dtProjectHistoryProblem.AcceptChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                if (saveData())
                {
                    loadData();
                    loadDetail();
                    dtProjectHistoryProblem.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colAdd && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;

                    List<int> listSTT = new List<int>();
                    DataTable dt = (DataTable)grdData.DataSource;
                    dt.AcceptChanges();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strSTT = TextUtils.ToString(dt.Rows[i]["STT"]);
                        if (!strSTT.Contains("."))
                        {
                            int stt = TextUtils.ToInt(dt.Rows[i]["STT"]);
                            listSTT.Add(stt);
                        }
                    }
                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";
                    dt.Rows.Add(dtrow);

                    grdData.DataSource = dt;
                    grvData.FocusedRowHandle = grvData.RowCount - 1;
                }
            }
        }
        private void btnDeleteRepo_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (MessageBox.Show(string.Format("Bạn có muốn xóa hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listID.Add(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = row + 1;
            }

        }

        private void frmHistoryProjectProblem_FormClosing(object sender, FormClosingEventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            DataTable dataChange = dtProjectHistoryProblem.GetChanges();
            DataTable detailChange = dtDetail.GetChanges();
            if (dataChange != null || detailChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (saveData())
                    {

                        e.Cancel = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            string fileSourceName = "Issuses Report Template.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string currentPath = path + "\\" + DateTime.Now.ToString($"{TextUtils.ToString(project.ProjectCode)}_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo báo giá!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Excel.Application app = default(Excel.Application);
                Excel.Workbook workBoook = default(Excel.Workbook);
                Excel.Worksheet workSheet = default(Excel.Worksheet);
                try
                {
                    app = new Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Excel.Worksheet)workBoook.Worksheets[1];
                    workSheet.Cells[2, 4] = TextUtils.ToString(project.ProjectCode);
                    workSheet.Cells[3, 4] = TextUtils.ToString(project.ProjectName);
                    EmployeeModel emp = SQLHelper<EmployeeModel>.FindByID(Global.EmployeeID);
                    workSheet.Cells[5, 1] = emp.FullName;
                    workSheet.Cells[6, 2] = DateTime.Now;

                    Excel.Range insertColumn = workSheet.Columns[11] as Excel.Range;

                    for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
                    {
                        string description = TextUtils.ToString(dtDetail.Rows[i]["AllDescriptions"]);
                        workSheet.Cells[8, 11] = description;
                        //insertColumn.Insert(Excel.XlInsertShiftDirection.xlShiftToRight);
                        ((Excel.Range)workSheet.Columns[11]).Insert();
                    }
                    ((Excel.Range)workSheet.Columns[11]).Delete();
                    for (int i = dtProjectHistoryProblem.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[10, 1] = i + 1;
                        workSheet.Cells[10, 2] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["TypeProblem"]);
                        workSheet.Cells[10, 3] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["ContentError"]);
                        workSheet.Cells[10, 4] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["Reason"]);
                        workSheet.Cells[10, 5] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["Remedies"]);
                        workSheet.Cells[10, 6] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["TestMethod"]);
                        workSheet.Cells[10, 7] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["Image"]);
                        workSheet.Cells[10, 8] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["DateProblem"]);
                        workSheet.Cells[10, 9] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["DateImplementation"]);
                        workSheet.Cells[10, 10] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i]["PIC"]);
                        for (int j = 0; j < dtDetail.Rows.Count; j++)
                        {
                            string description = TextUtils.ToString(dtDetail.Rows[j]["AllDescriptions"]);
                            workSheet.Cells[10, 11 + j] = TextUtils.ToString(dtProjectHistoryProblem.Rows[i][$"{description}"]);
                            Excel.Range cell = (Excel.Range)workSheet.Cells[10, 11 + j];
                            cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            cell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            cell.Font.Bold = true;
                            switch (dtProjectHistoryProblem.Rows[i][$"{description}"])
                            {
                                case "x":
                                    cell.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    break;
                                case "o":
                                    break;
                                case "--":
                                    cell.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                                    break;
                                case "OK":
                                    cell.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                    break;
                                case "N":
                                    cell.ClearContents();
                                    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.ColorTranslator.FromHtml("#F9D9B7"));
                                    break;
                            }
                        }


                        ((Excel.Range)workSheet.Rows[10]).Insert();
                    }
                    ((Excel.Range)workSheet.Rows[9]).Delete();
                    ((Excel.Range)workSheet.Rows[9]).Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog1.FileName;

                grvData.SetFocusedRowCellValue(colImage, imagePath);

            }
        }
        private void dockPanel2_Expanding(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            dockPanel2.BringToFront();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int projectHistoryProblemID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (projectHistoryProblemID <= 0)
            {
                MessageBox.Show("Vui lòng lưu lịch sử phát sinh trước khi khai báo chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                List<ProjectHistoryProblemDetailModel> list = (List<ProjectHistoryProblemDetailModel>)grdDetail.DataSource;
                int stt = list.Count > 0 ? list.Last().STT + 1 : 1;
                ProjectHistoryProblemDetailModel model = new ProjectHistoryProblemDetailModel()
                {
                    ProjectHistoryProblemID = projectHistoryProblemID,
                    STT = stt
                };
                list.Add(model);

                grvDetail.RefreshData();
                grdDetail.DataSource = list;
                grvDetail.FocusedRowHandle = grvDetail.RowCount - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            grvDetail.CloseEditor();
            int row = grvDetail.FocusedRowHandle;
            if (row >= 0)
            {
                int id = TextUtils.ToInt(grvDetail.GetRowCellValue(row, colDetailID));
                List<ProjectHistoryProblemDetailModel> p = listDetail.Where(x => x.ProjectHistoryProblemID == TextUtils.ToInt(grvDetail.GetRowCellValue(row, colProjectHistoryProblemID))
                && x.STT == TextUtils.ToInt(grvDetail.GetRowCellValue(row, colSTT))).ToList();
                listDetail.Remove(p.First());
                ProjectHistoryProblemDetailBO.Instance.Delete(id);

            }
            grvDetail.DeleteRow(row);
        }
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDetail();
        }
        private void grvDetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is ProjectHistoryProblemDetailModel item)
            {
                List<ProjectHistoryProblemDetailModel> list = (List<ProjectHistoryProblemDetailModel>)grdDetail.DataSource;
                var list2 = list.Where(x => x.ProjectHistoryProblemID == item.ProjectHistoryProblemID).ToList();
                for (int i = 0; i < list2.Count; i++)
                {
                    if (!listDetail.Contains(list2[i]))
                    {
                        listDetail.Add(list2[i]);
                    }
                }
            }
        }
    }
}