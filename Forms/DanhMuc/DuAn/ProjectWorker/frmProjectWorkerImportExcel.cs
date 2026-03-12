using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectWorkerImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        public int projectID = 0;
        int parent1 = 0;
        int parent2 = 0;
        int parent3 = 0;
        int firstParentId = 0;
        public string projectCode = "";

        public int version = 0;


        public class Variable
        {
            public int VersionID { get; set; }
            public string VersionCode { get; set; }
            public int ProjectTypeID { get; set; }
            public string ProjectTypeName { get; set; }
            public int ProjectID { get; set; }
            public string ProjectCode { get; set; }
            public int ProjectSolutionID { get; set; }
        }

        public Variable var = new Variable();
        public frmProjectWorkerImportExcel()
        {
            InitializeComponent();
        }

        private void frmProjectWorkerImportExcel_Load(object sender, EventArgs e)
        {
            //loadType();
        }
        void loadType()
        {
            cboProjectWorkerType.Properties.DataSource = SQLHelper<ProjectWorkerTypeModel>.FindAll();
            cboProjectWorkerType.Properties.ValueMember = "ID";
            cboProjectWorkerType.Properties.DisplayMember = "Name";
        }
        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }
        bool validate()
        {
            //if (TextUtils.ToInt(cboProjectWorkerType.EditValue) <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn loại nhân công dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                try
                {
                    var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var sw = new Stopwatch();
                    sw.Start();

                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                    var openTiming = sw.ElapsedMilliseconds;

                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });
                    //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0)
                        cboSheet.SelectedIndex = 0;
                    btnSave.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

                    cboSheet_SelectionChangeCommitted(null, null);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!validate()) return;
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {

                progressBar1.Minimum = 1;
                if (grvData.RowCount == 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    progressBar1.Maximum = grvData.RowCount - 1;
                    txtRate.Text = "";
                    start = DateTime.Now;
                    enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateProjectWorker();

            return;
            int rowCount = grvData.RowCount;
            //int workerType = TextUtils.ToInt(cboProjectWorkerType.EditValue);

            var exp1 = new Expression("ProjectID", projectID);
            var exp2 = new Expression("ProjectWorkerVersionID", version);
            //var exp3 = new Expression("ProjectWorkerTypeID", workerType);
            var worker = SQLHelper<ProjectWorkerModel>.FindByExpression(exp1.And(exp2)).ToList();
            if (worker.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Nhân công dự án đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ProjectWorkerBO.Instance.DeleteByExpression(exp1.And(exp2));
                }
                else
                {
                    return;
                }
            }

            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

                    ProjectWorkerModel model = new ProjectWorkerModel();
                    string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();

                    if (string.IsNullOrEmpty(stt))
                    {
                        continue;
                    }

                    if (Regex.IsMatch(stt.First().ToString(), @"^\d+$"))
                    {
                        //var checkmodel = SQLHelper<ProjectWorkerModel>.SqlToModel($"SELECT * FROM dbo.ProjectWorker WHERE ProjectID = {projectID} AND TT = '{stt}' AND ProjectWorkerTypeID = {workerType} ");
                        if (!stt.Contains('.'))
                        {
                            //if (checkmodel.ID > 0)
                            //{
                            //    model = checkmodel;
                            //}
                            model.TT = stt;
                            model.WorkContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                            model.ProjectID = projectID;
                            model.ParentID = 0;
                            model.IsDeleted = false;
                            //model.ProjectWorkerTypeID = workerType;
                            model.ProjectWorkerVersionID = version;
                            if (model.ID > 0)
                            {
                                ProjectWorkerBO.Instance.Update(model);
                                firstParentId = model.ID;
                            }
                            else
                            {
                                firstParentId = (int)ProjectWorkerBO.Instance.Insert(model);
                            }
                        }
                        else
                        {
                            //if (checkmodel.ID > 0)
                            //{
                            //    model = checkmodel;
                            //}
                            var checkParent = stt.Substring(0, stt.LastIndexOf('.'));
                            var checkExistParent = SQLHelper<ProjectWorkerModel>.SqlToModel($"SELECT * FROM dbo.ProjectWorker WHERE ProjectID = {projectID} AND TT = '{checkParent}'");
                            if (checkExistParent.ID <= 0) continue;
                            if (model.IsApprovedTBP)
                            {
                                continue;
                            }
                            model.WorkContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                            model.ProjectID = projectID;
                            model.AmountPeople = TextUtils.ToInt(grvData.GetRowCellValue(i, "F3"));
                            model.NumberOfDay = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F4"));
                            model.TotalWorkforce = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F5"));
                            model.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F6"));
                            model.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
                            model.TT = stt;
                            model.IsDeleted = false;
                            //model.ProjectWorkerTypeID = workerType;
                            model.ProjectWorkerVersionID = version;
                            InsertUpdateChild(model, stt, i);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString(), "Thông báo");
                }
            }
        }

        void InsertUpdateChild(ProjectWorkerModel model, string stt, int i)
        {
            if (stt.Count(p => p == '.') == 1)
            {

                model.ParentID = firstParentId;

                if (model.ID > 0)
                {
                    ProjectPartListBO.Instance.Update(model);
                    parent1 = model.ID;
                }
                else
                {
                    parent1 = (int)ProjectWorkerBO.Instance.Insert(model);
                }
                return;
            }

            if (stt.Count(p => p == '.') == 2)
            {

                model.ParentID = parent1;
                if (model.ID > 0)
                {
                    ProjectWorkerBO.Instance.Update(model);
                    parent2 = model.ID;
                }
                else
                {
                    parent2 = (int)ProjectWorkerBO.Instance.Insert(model);
                }
                return;
            }

            if (stt.Count(p => p == '.') == 3)
            {

                model.ParentID = parent2;
                if (model.ID > 0)
                {
                    ProjectPartListBO.Instance.Update(model);
                    parent3 = model.ID;
                }
                else
                {
                    parent3 = (int)ProjectWorkerBO.Instance.Insert(model);
                }
                return;
            }

            if (stt.Count(p => p == '.') == 4)
            {
                model.ParentID = parent3;
                if (model.ID > 0)
                {
                    ProjectWorkerBO.Instance.Update(model);

                }
                else
                {
                    ProjectWorkerBO.Instance.Insert(model);
                }
                return;
            }
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();

                grdData.DataSource = ds; // dataset
                grdData.DataMember = tablename;
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
            if (grdData.DataSource == null)
            {
                try
                {
                    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                    grdData.DataSource = dt;
                    grvData.PopulateColumns();
                    grvData.BestFitColumns();
                    grdData.Focus();
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.Close();
        }

        private void frmProjectWorkerImportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnAddWorkerType_Click(object sender, EventArgs e)
        {
            //frmProjectWorkerTypeDetail frm = new frmProjectWorkerTypeDetail();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadType();
            //}
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("Danh_Muc_Vat_Tu.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("Danh_Muc_Vat_Tu.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void frmProjectWorkerImportExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        void UpdateWorker(int newVersionId, int oldVersionId)
        {
            try
            {

                List<ProjectWorkerModel> projectWorkers = SQLHelper<ProjectWorkerModel>.FindByAttribute("ProjectWorkerVersionID", oldVersionId);
                Regex regex = new Regex(@"^-?[\d\.]+$");
                foreach (ProjectWorkerModel item in projectWorkers)
                {
                    string stt = item.TT;

                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!regex.IsMatch(stt)) continue;
                    //if (isUpdatePartList && partlists.Any(x => x.TT == stt)) continue;

                    ProjectWorkerModel worker = new ProjectWorkerModel();
                    worker.TT = stt;
                    worker.WorkContent = item.WorkContent;
                    worker.AmountPeople = item.AmountPeople;
                    worker.NumberOfDay = item.NumberOfDay;
                    worker.TotalWorkforce = item.AmountPeople * item.NumberOfDay;
                    worker.Price = item.Price;
                    worker.TotalPrice = item.Price * worker.TotalWorkforce;
                    worker.ParentID = GetParentID(stt, newVersionId);
                    worker.ProjectID = item.ProjectID;
                    worker.ProjectWorkerVersionID = newVersionId;
                    worker.ProjectTypeID = item.ProjectTypeID;

                    if (worker.ID > 0)
                    {
                        SQLHelper<ProjectWorkerModel>.Update(worker);
                    }
                    else
                    {
                        SQLHelper<ProjectWorkerModel>.Insert(worker);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void UpdateProjectWorker()
        {
            string stt = "";
            Regex regex = new Regex(@"^-?[\d\.]+$");
            try
            {
                var exp1 = new Expression("IsDeleted", 1, "<>");
                var exp2 = new Expression("ProjectWorkerVersionID", var.VersionID);
                var projectWorkers = SQLHelper<ProjectWorkerModel>.FindByExpression(exp1.And(exp2)).ToList();
                if (projectWorkers.Count > 0)
                {
                    DialogResult dialog = MessageBox.Show("Nhân công dự án đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        //ProjectWorkerBO.Instance.DeleteByExpression(exp1.And(exp2));
                        string sql = $"UPDATE dbo.ProjectWorker SET IsDeleted = 1," +
                                    $"UpdatedDate='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                    $"UpdatedBy='{Global.LoginName}' " +
                                    $"WHERE ProjectWorkerVersionID = {var.VersionID}";
                        TextUtils.ExcuteSQL(sql);
                    }
                    else return;
                }

                for (int i = 1; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();

                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!regex.IsMatch(stt)) continue;

                    ProjectWorkerModel worker = new ProjectWorkerModel();
                    worker.TT = stt;
                    worker.WorkContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    worker.AmountPeople = TextUtils.ToInt(grvData.GetRowCellValue(i, "F3"));
                    worker.NumberOfDay = TextUtils.ToInt(grvData.GetRowCellValue(i, "F4"));
                    worker.TotalWorkforce = worker.AmountPeople * worker.NumberOfDay;

                    string price = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    price = Regex.Replace(price, "[,\\.]", "");
                    worker.Price = TextUtils.ToInt(price);
                    worker.TotalPrice = worker.Price * worker.TotalWorkforce;
                    worker.ParentID = GetParentID(stt, var.VersionID);
                    worker.ProjectID = var.ProjectID;
                    worker.ProjectWorkerVersionID = var.VersionID;
                    worker.ProjectTypeID = var.ProjectTypeID;

                    if (worker.ID > 0)
                    {
                        SQLHelper<ProjectWorkerModel>.Update(worker);
                    }
                    else
                    {
                        SQLHelper<ProjectWorkerModel>.Insert(worker);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $"\nStt: {stt}", "Thông báo");
            }
        }



        int GetParentID(string tt, int versionId)
        {
            int parentId = 0;
            if (!tt.Contains(".")) return parentId;

            string parentTt = tt.Substring(0, tt.LastIndexOf(".")).Trim();

            var exp1 = new Expression("TT", parentTt);
            var exp2 = new Expression("ProjectWorkerVersionID", versionId);
            var exp3 = new Expression("IsDeleted", 1, "<>");
            ProjectWorkerModel parent = SQLHelper<ProjectWorkerModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
            if (parent != null && parent.ID > 0)
            {
                parentId = parent.ID;
            }
            return parentId;
        }
    }
}