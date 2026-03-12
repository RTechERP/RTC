using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Pdf;
using DevExpress.UIAutomation;
using DevExpress.Utils;
using DevExpress.Utils.Html.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using Microsoft.Office.Interop.Outlook;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Action = System.Action;
using Exception = System.Exception;

namespace BMS
{
    public partial class frmPartListImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;

        Regex regex = new Regex(@"^-?[\d\.]+$");
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
        //public object varImport = new { versionId = 0, versionCode = "", versionTypeId = 0, versionType = "", projectId = 0, projectCode = "", projectSolutionId = 0};
        List<ProjectPartListModel> partListDiffs = new List<ProjectPartListModel>();

        List<ProjectPartListModel> projectPartLists = new List<ProjectPartListModel>();
        public frmPartListImportExcel()
        {
            InitializeComponent();
        }

        private async void frmPartListImportExcel_Load(object sender, EventArgs e)
        {
            await LoadProjectPartListsAsync();
            //loadPartListType();
            LoadVersion();
            grvData.OptionsBehavior.Editable = Global.IsAdmin;
            grvData.OptionsBehavior.ReadOnly = !Global.IsAdmin;
        }

        private async Task LoadProjectPartListsAsync()
        {
            projectPartLists = await Task.Run(() =>
                SQLHelper<ProjectPartListModel>
                    .FindAll()
                    .GroupBy(x => new { x.ProductCode, x.Manufacturer, x.Unit, x.GroupMaterial })
                    .Select(g => g.First())
                    .ToList()
            );
        }

        private void loadPartListType()
        {
            List<ProjectPartListTypeModel> listType = SQLHelper<ProjectPartListTypeModel>.FindAll();
            cboCategoryPartList.Properties.DataSource = listType;
            cboCategoryPartList.Properties.ValueMember = "ID";
            cboCategoryPartList.Properties.DisplayMember = "Name";
        }

        void LoadVersion()
        {
            //int projectSolutionId = TextUtils.ToInt(varImport.GetType().GetProperty("projectSolutionId").GetValue(varImport));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartListVersion", "A", new string[] { "@ProjectSolutionID" }, new object[] { var.ProjectSolutionID });
            cboVersion.Properties.DisplayMember = "CodeNew";
            cboVersion.Properties.ValueMember = "ID";
            cboVersion.Properties.DataSource = dt;
            cboVersion.EditValue = var.VersionID;
        }

        bool validate()
        {
            //if (Convert.ToInt32(cboCategoryPartList.EditValue) <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Danh mục vật tư!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            if (Global.DebugFlag || chkIsStock.Checked) return true;

            string code = TextUtils.ToString(grvData.GetRowCellValue(1, "F4")).Trim();
            //string projectCode = TextUtils.ToString(varImport.GetType().GetProperty("projectCode").GetValue(varImport)).Trim();

            if (TextUtils.ToInt(cboVersion.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Phiên bản!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!code.Equals(var.ProjectCode))
            {
                MessageBox.Show("Không đúng Mã dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            for (int i = 7; i < grvData.RowCount; i++)
            {
                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();

                if (string.IsNullOrEmpty(stt)) continue;
                if (!this.regex.IsMatch(stt)) continue;

                string GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                string ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                string Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();

                List<string> isParent = stt.Split('.').ToList();

                //if (isParent.Count <= 1) continue;

                if (isParent.Count >= 3)
                {
                    if (GroupMaterial == ProductCode)
                    {
                        MessageBox.Show($"[Tên vật tư] có số thứ tự [{stt}] đã bị trùng với [Mã thiết bị]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            frmProjectPartListTypeDetail frm = new frmProjectPartListTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPartListType();
            }
        }

        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                if (table.ToString().Contains('.')) continue;
                tableList.Add(table.ToString());
            }

            return tableList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                if (!validate()) return;
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
                    partListDiffs.Clear();
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
            if (chkIsStock.Checked) UpdateStock();
            else
            {
                if (!CheckValidate())
                {
                    e.Cancel = true;
                    return;
                }
                UpdatePartlist();
                LoadDataDiff(); //VtnUpdate

                if (Global.DebugFlag)
                {
                    //UpdatePONCC();
                }
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
            if (e.Cancelled)
            {
                enableControl(true);
                return;
            }

            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            //this.Close();
        }

        //private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    grvData.Columns.Clear();
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    var result = ofd.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        btnBrowse.Text = ofd.FileName;
        //    }
        //    else if (result == DialogResult.Cancel)
        //    {
        //        return;
        //    }
        //    try
        //    {
        //        try
        //        {
        //            var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        //            var sw = new Stopwatch();
        //            sw.Start();

        //            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

        //            var openTiming = sw.ElapsedMilliseconds;

        //            ds = reader.AsDataSet(new ExcelDataSetConfiguration()
        //            {
        //                UseColumnDataType = false,
        //                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
        //                {
        //                    UseHeaderRow = false
        //                }
        //            });
        //            //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

        //            var tablenames = GetTablenames(ds.Tables);

        //            cboSheet.DataSource = tablenames;

        //            if (tablenames.Count > 0)
        //                cboSheet.SelectedIndex = 0;
        //            btnSave.Enabled = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ofd.ShowDialog() == DialogResult.OK)
        //        {
        //            btnBrowse.Text = ofd.FileName;
        //            cboSheet.DataSource = null;
        //            cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

        //            cboSheet_SelectionChangeCommitted(null, null);
        //        }
        //    }
        //}

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            ofd.Multiselect = false;
            ofd.Title = "Chọn file excel";

            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;


                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
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

                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0) cboSheet.SelectedIndex = 0;

                    btnSave.Enabled = true;
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;

                }

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                try
                {

                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
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

                        var tablenames = GetTablenames(ds.Tables);

                        cboSheet.DataSource = tablenames;

                        if (tablenames.Count > 0)
                            cboSheet.SelectedIndex = 0;

                        btnSave.Enabled = true;
                    }

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
        private void frmPartListImportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmPartListImportExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        //void UpdatePartlist()
        //{
        //    try
        //    {
        //        //int projectId = TextUtils.ToInt(varImport.GetType().GetProperty("projectId").GetValue(varImport));
        //        //int versionId = TextUtils.ToInt(varImport.GetType().GetProperty("versionId").GetValue(varImport));
        //        //int projectTypeId = TextUtils.ToInt(varImport.GetType().GetProperty("versionTypeId").GetValue(varImport));
        //        int versionId = TextUtils.ToInt(cboVersion.EditValue);


        //        var exp1 = new Expression("ProjectID", var.ProjectID);
        //        var exp2 = new Expression("ProjectTypeID", var.ProjectTypeID);
        //        var exp3 = new Expression("ProjectPartListVersionID", versionId);
        //        var exp4 = new Expression("IsDeleted", 1, "<>");
        //        var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
        //        if (partlists.Count > 0 && !chkIsProblem.Checked)
        //        {
        //            DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (dialog != DialogResult.Yes) return;

        //            string idText = string.Join(",", partlists.Select(x => x.ID));
        //            string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN ({idText})";
        //            //SQLHelper<ProjectPartListModel>.DeleteListModel(partlists);

        //            TextUtils.ExcuteSQL(sql);
        //        }

        //        //Check phiên bản đã được active, duyệt hoặc PO chưa
        //        //ProjectPartListVersionModel version = SQLHelper<ProjectPartListVersionModel>.FindByID(versionId);
        //        //if (version != null)
        //        //{
        //        //    if (version.IsActive || version.IsApproved || version.StatusVersion == 2)
        //        //    {
        //        //        DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn cập nhật phát sinh không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        //    }
        //        //}

        //        Regex regex = new Regex(@"^-?[\d\.]+$");
        //        for (int i = 7; i < grvData.RowCount; i++)
        //        {
        //            progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
        //            txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

        //            string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
        //            if (string.IsNullOrEmpty(stt)) continue;
        //            if (!regex.IsMatch(stt)) continue;

        //            ProjectPartListModel partList = new ProjectPartListModel();
        //            partList.ProjectID = var.ProjectID;
        //            partList.TT = stt;
        //            partList.ParentID = GetParentId(stt, versionId, chkIsProblem.Checked);
        //            partList.ProjectTypeID = var.ProjectTypeID;
        //            partList.ProjectPartListVersionID = versionId;

        //            partList.GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
        //            partList.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
        //            partList.OrderCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
        //            partList.Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
        //            partList.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
        //            partList.QtyMin = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
        //            partList.QtyFull = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
        //            partList.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();
        //            partList.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));
        //            partList.Amount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
        //            partList.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, "F12")).Trim();
        //            partList.NCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F13")).Trim();
        //            partList.NCCFinal = TextUtils.ToString(grvData.GetRowCellValue(i, "F14")).Trim();
        //            partList.PriceOrder = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F15"));
        //            partList.TotalPriceOrder = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16"));
        //            partList.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F17"));
        //            partList.ExpectedReturnDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F18"));
        //            partList.OrderDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F19"));
        //            partList.ReturnDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F20"));
        //            partList.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, "F21"));
        //            partList.Quality = TextUtils.ToString(grvData.GetRowCellValue(i, "F22")).Trim();
        //            partList.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F23")).Trim();
        //            partList.ReasonProblem = TextUtils.ToString(grvData.GetRowCellValue(i, "F24")).Trim();
        //            partList.IsProblem = chkIsProblem.Checked;

        //            if (partList.ID > 0)
        //            {
        //                SQLHelper<ProjectPartListModel>.Update(partList);
        //            }
        //            else
        //            {
        //                SQLHelper<ProjectPartListModel>.Insert(partList);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        ////}
        DataTable dataDiff = new DataTable(); // VTN update 
        void UpdatePartlist()
        {
            int rowHandle = 0;
            string stt = "";
            try
            {
                bool isUpdatePartList = chkIsUpdate.Checked;
                int versionId = TextUtils.ToInt(cboVersion.EditValue);

                var exp1 = new Expression("ProjectID", var.ProjectID);
                //var exp2 = new Expression("ProjectTypeID", var.ProjectTypeID);
                var exp3 = new Expression("ProjectPartListVersionID", versionId);
                var exp4 = new Expression("IsDeleted", 1, "<>");
                var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp3).And(exp4));
                if (partlists.Count > 0 && !(chkIsProblem.Checked || isUpdatePartList))
                {
                    DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog != DialogResult.Yes) return;

                    string idText = string.Join(",", partlists.Select(x => x.ID));
                    string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN ({idText})";
                    //SQLHelper<ProjectPartListModel>.DeleteListModel(partlists);

                    TextUtils.ExcuteSQL(sql);
                    partlists.Clear();
                }

                //NT.Huy update lay ra list cac parent TT
                var listParentTT = new List<string>();
                foreach (var item in partlists)
                {
                    if (!item.TT.Contains(".")) continue;
                    string parentTt = item.TT.Substring(0, item.TT.LastIndexOf(".")).Trim();
                    listParentTT.Add(parentTt);
                }

                Regex regex = new Regex(@"^-?[\d\.]+$");

                //List<ProductSaleModel> productSales = SQLHelper<ProductSaleModel>.FindAll();
                //List<ProductRTCModel> productRTCs = SQLHelper<ProductRTCModel>.FindAll();
                //List<UnitCountModel> unitCounts = SQLHelper<UnitCountModel>.FindAll();
                //List<UnitCountKTModel> unitCounts = SQLHelper<UnitCountKTModel>.FindAll();

                var dtAll = TextUtils.GetTable("spGetProjectPartlistSuggest"); //ndnhat update 22/09/2025
                for (int i = 7; i < grvData.RowCount; i++)
                {
                    rowHandle = i;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();

                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!regex.IsMatch(stt)) continue;
                    //if (isUpdatePartList && partlists.Any(x => x.TT == stt)) continue;
                    if (partlists.Any(x => x.TT == stt)) continue;

                    ProjectPartListModel partList = new ProjectPartListModel();
                    partList.ProjectID = var.ProjectID;

                    partList.TT = stt;
                    partList.STT = i - 6;
                    if (chkIsProblem.Checked == true)
                    {
                        //NT.Huy update kiem tra xem partlist phat sinh co con hay khong
                        int existParentIndex = listParentTT.IndexOf(stt);
                        if (existParentIndex >= 0) continue;
                        int parentID = GetParentId(stt, versionId, chkIsProblem.Checked, var.ProjectTypeID);
                        //if (parentID <= 0) continue;
                        partList.ParentID = parentID;

                    }
                    else
                    {
                        partList.ParentID = GetParentId(stt, versionId, chkIsProblem.Checked, var.ProjectTypeID);
                    }

                    partList.ProjectTypeID = var.ProjectTypeID;
                    partList.ProjectPartListTypeID = var.ProjectTypeID;
                    partList.ProjectPartListVersionID = versionId;

                    partList.GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    partList.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    partList.OrderCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    partList.Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim().ToUpper();
                    partList.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    partList.QtyMin = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
                    partList.QtyFull = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
                    partList.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();
                    partList.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));
                    partList.Amount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
                    partList.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, "F12")).Trim(); // tien do
                    partList.NCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F13")).Trim(); // nha cung cap
                    partList.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F14")); //ngay yeu cau dat hang
                    partList.LeadTimeRequest = TextUtils.ToString(grvData.GetRowCellValue(i, "F15")); // tien do ycau f15                
                    partList.QuantityReturn = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16")); // so luong dat thuc te
                    partList.NCCFinal = TextUtils.ToString(grvData.GetRowCellValue(i, "F17")).Trim(); // nha cung cap mua
                    partList.PriceOrder = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F18")); // gia dat mua
                    partList.OrderDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F19")); // ngay dat hang thuc te
                    partList.ExpectedReturnDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F20")); // du kien hang ve
                    partList.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, "F21")); // tinh trang
                    partList.Quality = TextUtils.ToString(grvData.GetRowCellValue(i, "F22"));    // chat luong f22
                    partList.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F23")).Trim(); // note
                    partList.ReasonProblem = TextUtils.ToString(grvData.GetRowCellValue(i, "F24")).Trim(); // li do phat sinh
                    partList.SpecialCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F25")).Trim(); // mã đặc biệt
                    partList.IsProblem = chkIsProblem.Checked;

                    string productcode = (partList.ProductCode).ToUpper();
                    string manufacturer = (partList.Manufacturer).ToUpper();
                    string unit = (partList.Unit).ToUpper();
                    string productName = (partList.GroupMaterial).ToUpper();

                    //if (productcode == ("X-TC-HR10A-10P-12S-S-O-0500K".ToUpper()))
                    //{
                    //    MessageBox.Show("a");
                    //}
                    //DataTable dt = TextUtils.GetDataTableFromSP("spGetHistoryPricePartlist",
                    //        new string[] { "@Keyword", "@ProjectID", "@SupplierSaleID" },
                    //         new object[] { productcode, 0, 0 });

                    partList.IsNewCode = true;

                    //if (dt.Rows.Count > 0)
                    //{
                    //    DataRow dtr = dt.Rows[0];
                    //    partList.IsNewCode = !(dtr["Maker"].ToString().ToUpper().Trim() == manufacturer &&
                    //        dtr["Unit"].ToString().ToUpper().Trim() == unit &&
                    //        dtr["ProductName"].ToString().ToUpper().Trim() == productName);
                    //}

                    DataRow[] dtrow = dtAll.Select($"[ProductCode]='{productcode}'");
                    if (dtrow.Length > 0)
                    {
                        DataRow dtr = dtrow.First();
                        partList.IsNewCode = !(
                            TextUtils.ToString(dtr["Maker"]).ToUpper().Trim() == manufacturer &&
                            TextUtils.ToString(dtr["Unit"]).ToUpper().Trim() == unit &&
                            TextUtils.ToString(dtr["ProductName"]).ToUpper().Trim() == productName
                        );
                    }


                    //string productcode = (partList.ProductCode).ToUpper();
                    //string manufacturer = (partList.Manufacturer).ToUpper();
                    //string unit = (partList.Unit).ToUpper();

                    //var unitCount = unitCounts.Where(x => x.UnitCountName.Trim().ToUpper() == unit).FirstOrDefault() ?? new UnitCountKTModel();
                    //int unitCoutID = unitCount.ID;

                    //bool isProductSale = productSales.Any(x => x.ProductCode.Trim().ToUpper() == productcode &&
                    //                                           //x.ProductName.Trim().ToUpper() == partList.GroupMaterial.ToUpper() &&
                    //                                           x.Maker.Trim().ToUpper() == manufacturer &&
                    //                                           x.Unit.Trim().ToUpper() == unit);

                    //bool isProductRTC = productRTCs.Any(x => x.ProductCode.Trim().ToUpper() == productcode &&
                    //                                            //x.ProductName.Trim().ToUpper() == partList.GroupMaterial.ToUpper() &&
                    //                                            x.Maker.Trim().ToUpper() == manufacturer &&
                    //                                            x.UnitCountID == unitCoutID);
                    //partList.IsNewCode = (!isProductSale && !isProductRTC);

                    if (partList.ID > 0)
                    {
                        SQLHelper<ProjectPartListModel>.Update(partList);
                    }
                    else
                    {
                        partList.ID = SQLHelper<ProjectPartListModel>.Insert(partList).ID;
                    }

                    if (partList.IsNewCode == true && !partListDiffs.Contains(partList))
                    {
                        partListDiffs.Add(partList);
                    }
                }

                if (partListDiffs.Count > 0)
                {
                    List<int> ids = partListDiffs.Select(x => x.ID).ToList();
                    string idTextDiff = string.Join(",", ids);
                    dataDiff = TextUtils.LoadDataFromSP("spGetProjectParlistNotSame", "A", new string[] { "@ProjectParlistID" }, new object[] { idTextDiff }); // VTN Update diff
                }

                //Add Notify

                ProjectPartListVersionModel versionModel = SQLHelper<ProjectPartListVersionModel>.FindByID(versionId);
                if (versionModel.StatusVersion == 2)
                {

                    ProjectModel project = SQLHelper<ProjectModel>.FindByID(var.ProjectID);
                    ProjectTypeModel projectTypeModel = SQLHelper<ProjectTypeModel>.FindByID(var.ProjectTypeID);
                    string text = $"Yêu cầu duyệt partlist\n" +
                                    $"Dự án: {project.ProjectCode}\n" +
                                    $"Danh mục: {projectTypeModel.ProjectTypeName}\n" +
                                    $"Phiên bản: {versionModel.Code}";

                    //ProjectTypeModel projectType = SQLHelper<ProjectTypeModel>.FindByID(newVersion.ProjectTypeID);
                    TextUtils.AddNotify("DUYỆT PARTLIST", text, TextUtils.ToInt(projectTypeModel.ApprovedTBPID));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $"\nStt: {stt}", "Thông báo");
            }
        }

        // ===================================================PQ.Chiến - ADD - 06/03/2025====================================================
        //void UpdateStock()
        //{
        //    int rowHandle = 0;
        //    string stt = "";
        //    try
        //    {
        //        bool isUpdateStock = chkIsStock.Checked;
        //        int versionId = TextUtils.ToInt(cboVersion.EditValue);

        //        var exp1 = new Expression("ProjectID", var.ProjectID);
        //        //var exp2 = new Expression("ProjectTypeID", var.ProjectTypeID);
        //        var exp3 = new Expression("ProjectPartListVersionID", versionId);
        //        var exp4 = new Expression("IsDeleted", 1, "<>");
        //        var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp3).And(exp4));
        //        if (partlists.Count > 0 && !(chkIsProblem.Checked || chkIsUpdate.Checked || isUpdateStock))
        //        {
        //            string idText = string.Join(",", partlists.Select(x => x.ID));
        //            DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (dialog != DialogResult.Yes) return;

        //            string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN ({idText})";
        //            //SQLHelper<ProjectPartListModel>.DeleteListModel(partlists);

        //            TextUtils.ExcuteSQL(sql);
        //            partlists.Clear();
        //        }

        //        //var listParentTT = new List<string>();
        //        //foreach (var item in partlists)
        //        //{
        //        //    if (!item.TT.Contains(".")) continue;
        //        //    string parentTt = item.TT.Substring(0, item.TT.LastIndexOf(".")).Trim();
        //        //    listParentTT.Add(parentTt);
        //        //}

        //        //Regex regex = new Regex(@"^-?[\d\.]+$");
        //        for (int i = 7; i < grvData.RowCount; i++)
        //        {
        //            rowHandle = i;
        //            progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
        //            txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));
        //            stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
        //            if (string.IsNullOrEmpty(stt)) continue;
        //            if (!this.regex.IsMatch(stt)) continue;


        //            if (partlists.Any(x => x.TT == stt)) ;


        //            ProjectPartListModel partList = new ProjectPartListModel();
        //            partList.ProjectID = var.ProjectID;

        //            partList.TT = stt;
        //            partList.STT = i - 6;
        //            if (chkIsProblem.Checked == true)
        //            {
        //                //NT.Huy update kiem tra xem partlist phat sinh co con hay khong
        //                //int existParentIndex = listParentTT.IndexOf(stt);
        //                //if (existParentIndex >= 0) continue;
        //                int parentID = GetParentId(stt, versionId, chkIsProblem.Checked, var.ProjectTypeID);
        //                //if (parentID <= 0) continue;
        //                partList.ParentID = parentID;

        //            }
        //            else
        //            {
        //                partList.ParentID = GetParentId(stt, versionId, chkIsProblem.Checked, var.ProjectTypeID);
        //            }

        //            partList.ProjectTypeID = var.ProjectTypeID;
        //            partList.ProjectPartListTypeID = var.ProjectTypeID;
        //            partList.ProjectPartListVersionID = versionId;

        //            partList.GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();// tên vật tư
        //            partList.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();//mã thiết bị
        //            partList.OrderCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();//hãng SX
        //            partList.Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();// thông số kỹ thuật
        //            partList.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
        //            partList.QtyMin = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));//số lượng tối thiểu
        //            partList.QtyFull = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));//tổng số lượng
        //            partList.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();//đơn vị
        //            partList.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));//đơn giá
        //            //partList.Amount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
        //            partList.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")).Trim(); // tien do
        //            partList.NCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F11")).Trim(); // nha cung cap
        //            partList.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F12")); //ngay yeu cau dat hang
        //            partList.LeadTimeRequest = TextUtils.ToString(grvData.GetRowCellValue(i, "F13")); // tien do ycau f15                
        //            //partList.QuantityReturn = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16")); // so luong dat thuc te
        //            partList.NCCFinal = TextUtils.ToString(grvData.GetRowCellValue(i, "F14")).Trim(); // nha cung cap mua
        //            partList.PriceOrder = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F15")); // gia dat mua
        //            //partList.OrderDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F19")); // ngay dat hang thuc te
        //            partList.ExpectedReturnDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F16")); // du kien hang ve
        //            partList.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, "F17")); // tinh trang
        //            partList.Quality = TextUtils.ToString(grvData.GetRowCellValue(i, "F18"));    // chat luong f22
        //            partList.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F19")).Trim(); // note

        //            //partList.ReasonProblem = TextUtils.ToString(grvData.GetRowCellValue(i, "F24")).Trim(); // li do phat sinh
        //            partList.IsStock = chkIsStock.Checked;


        //            var existingPart = SQLHelper<ProjectPartListModel>.FindByExpression(new Expression("TT", stt)
        //                .And(new Expression("ProjectID", var.ProjectID))
        //                .And(new Expression("ProjectPartListVersionID", versionId)))
        //                .FirstOrDefault();

        //            if (existingPart != null)
        //            {
        //                partList.ID = existingPart.ID; // Lấy ID nếu tồn tại
        //            }

        //            if (partList.ID > 0)
        //            {
        //                SQLHelper<ProjectPartListModel>.Update(partList);
        //            }
        //            else
        //            {
        //                SQLHelper<ProjectPartListModel>.Insert(partList);
        //            }



        //            //string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
        //            //string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();
        //            //string maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
        //            decimal minQuantity = TextUtils.ToDecimal(partList.QtyMin);
        //            var exp5 = new Expression("ProductCode", partList.ProductCode);
        //            var exp6 = new Expression("Unit", partList.Unit);
        //            var exp7 = new Expression("Maker", partList.Manufacturer);



        //            //ProjectPartListModel projectPartList = SQLHelper<ProjectPartListModel>.FindByExpression(exp5).FirstOrDefault();
        //            //if (projectPartList == null) continue;
        //            //var exp7 = new Expression("ProjectPartListID", projectPartList.ID);

        //            ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp5.And(exp6).And(exp7)).FirstOrDefault() ?? new ProductSaleModel();
        //            if (productSale.ID <= 0) continue;
        //            var exp8 = new Expression("ProductSaleID", productSale.ID);
        //            var exp9 = new Expression("WarehouseID", 1);

        //            InventoryModel inventory = SQLHelper<InventoryModel>.FindByExpression(exp8.And(exp9)).FirstOrDefault() ?? new InventoryModel();
        //            if (inventory.ID <= 0)
        //            {
        //                //InventoryModel inventory1 = new InventoryModel();
        //                inventory.ProductSaleID = productSale.ID;
        //                inventory.WarehouseID = 1;
        //                inventory.MinQuantity = minQuantity;
        //                inventory.IsStock = minQuantity > 0;
        //                inventory.Note = "";
        //                SQLHelper<InventoryModel>.Insert(inventory);
        //            }
        //            else
        //            {
        //                inventory.MinQuantity = minQuantity;
        //                inventory.IsStock = minQuantity > 0;
        //                SQLHelper<InventoryModel>.Update(inventory);
        //            }
        //            //if (inventory == null) continue;
        //            //inventory.IsStock = minQuantity > 0;
        //            //inventory.MinQuantity = minQuantity;
        //            //SQLHelper<InventoryModel>.Update(inventory);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + $"\nStt: {stt}", "Thông báo");
        //    }
        //}


        void UpdateStock()
        {
            int rowHandle = 0;
            string stt = "";
            try
            {
                bool isUpdateStock = chkIsStock.Checked;
                int versionId = TextUtils.ToInt(cboVersion.EditValue);

                var exp1 = new Expression("ProjectID", var.ProjectID);
                //var exp2 = new Expression("ProjectTypeID", var.ProjectTypeID);
                var exp3 = new Expression("ProjectPartListVersionID", versionId);
                var exp4 = new Expression("IsDeleted", 1, "<>");
                var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp3).And(exp4));
                if (partlists.Count > 0 && !(chkIsProblem.Checked || chkIsUpdate.Checked || isUpdateStock))
                {
                    string idText = string.Join(",", partlists.Select(x => x.ID));
                    DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog != DialogResult.Yes) return;

                    string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN ({idText})";
                    //SQLHelper<ProjectPartListModel>.DeleteListModel(partlists);

                    TextUtils.ExcuteSQL(sql);
                    partlists.Clear();
                }

                //var listParentTT = new List<string>();
                //foreach (var item in partlists)
                //{
                //    if (!item.TT.Contains(".")) continue;
                //    string parentTt = item.TT.Substring(0, item.TT.LastIndexOf(".")).Trim();
                //    listParentTT.Add(parentTt);
                //}

                //Regex regex = new Regex(@"^-?[\d\.]+$");
                //ndnhat update 18/08/2025

                var firms = SQLHelper<FirmModel>.FindByExpression(new Expression(FirmModel_Enum.FirmType, 1).And(new Expression(FirmModel_Enum.IsDelete, 0)));
                for (int i = 7; i < grvData.RowCount; i++)
                {
                    stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!this.regex.IsMatch(stt)) continue;

                    string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();//đơn vị
                    string maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();

                    FirmModel firm = firms.FirstOrDefault(x => x.FirmName.Trim().ToLower() == maker.ToLower()) ?? new FirmModel();

                    Expression exp7 = new Expression("Maker", maker);
                    if (firm.ID > 0) exp7 = new Expression("FirmID", firm.ID);

                    // tìm productSale
                    var exp5 = new Expression("ProductCode", productCode);
                    var exp6 = new Expression("Unit", unit);

                    var exp11 = new Expression("IsDeleted", 0);
                    ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp5.And(exp6).And(exp7).And(exp11)).FirstOrDefault();
                    if (productSale == null || productSale.ID <= 0)
                    {
                        MessageBox.Show($"Sản phẩm có mã {productCode} không có trong kho, vui lòng kiểm tra lại", "Thông báo");
                        return;
                    }

                    // kiểm tra tồn kho
                    var exp8 = new Expression("ProductSaleID", productSale.ID);
                    var exp9 = new Expression("WarehouseID", 1);
                    var exp10 = new Expression("ProjectTypeID", var.ProjectTypeID);
                    var exp12 = new Expression(InventoryStockModel_Enum.EmployeeIDRequest, Global.EmployeeID);


                    InventoryStockModel inventory = SQLHelper<InventoryStockModel>.FindByExpression(exp8.And(exp9).And(exp10).And(exp11).And(exp12)).FirstOrDefault() ?? new InventoryStockModel();

                    if (inventory != null && inventory.ID > 0)
                    {
                        if (inventory.EmployeeIDRequest != Global.EmployeeID && !Global.IsAdmin)
                        {
                            MessageBox.Show($"Vật tư {productSale.ProductCode} đã được yêu cầu bởi nhân viên khác. Vui lòng kiểm tra lại.", "Thông báo");
                            return;
                        }
                    }
                }
                //end ndnhat update 18/08/2025

                for (int i = 7; i < grvData.RowCount; i++)
                {
                    rowHandle = i;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));
                    stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!this.regex.IsMatch(stt)) continue;


                    if (partlists.Any(x => x.TT == stt)) ;


                    ProjectPartListModel partList = new ProjectPartListModel();
                    partList.ProjectID = var.ProjectID;

                    partList.TT = stt;
                    partList.STT = i - 6;
                    if (chkIsProblem.Checked == true)
                    {
                        //NT.Huy update kiem tra xem partlist phat sinh co con hay khong
                        //int existParentIndex = listParentTT.IndexOf(stt);
                        //if (existParentIndex >= 0) continue;
                        int parentID = GetParentId(stt, versionId, chkIsProblem.Checked, var.ProjectTypeID);
                        //if (parentID <= 0) continue;
                        partList.ParentID = parentID;

                    }
                    else
                    {
                        partList.ParentID = GetParentId(stt, versionId, chkIsProblem.Checked, var.ProjectTypeID);
                    }

                    partList.ProjectTypeID = var.ProjectTypeID;
                    partList.ProjectPartListTypeID = var.ProjectTypeID;
                    partList.ProjectPartListVersionID = versionId;

                    partList.GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();// tên vật tư
                    partList.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();//mã thiết bị
                    partList.OrderCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();//hãng SX
                    partList.Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();// thông số kỹ thuật
                    partList.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    //ndnhat update 18/08/2025
                    partList.QtyMin = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));//số lượng tối thiểu
                    partList.QtyFull = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));//tổng số lượng
                    partList.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();//đơn vị
                    partList.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));//đơn giá
                    partList.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, "F12")).Trim(); // tien do
                    partList.NCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F13")).Trim(); // nha cung cap
                    partList.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F14")); //ngay yeu cau dat hang

                    partList.LeadTimeRequest = TextUtils.ToString(grvData.GetRowCellValue(i, "F15")); // tien do ycau f15                
                                                                                                      //partList.QuantityReturn = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16")); // so luong dat thuc te
                    partList.NCCFinal = TextUtils.ToString(grvData.GetRowCellValue(i, "F17")).Trim(); // nha cung cap mua
                    partList.PriceOrder = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F18")); // gia dat mua
                                                                                                  //partList.OrderDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F19")); // ngay dat hang thuc te
                    partList.ExpectedReturnDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F20")); // du kien hang ve
                    partList.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, "F21")); // tinh trang
                    partList.Quality = TextUtils.ToString(grvData.GetRowCellValue(i, "F22"));    // chat luong f22
                    partList.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F23")).Trim(); // note

                    //end ndnhat update 18/08/2025

                    var existingPart = SQLHelper<ProjectPartListModel>.FindByExpression(new Expression("TT", stt)
                        .And(new Expression("ProjectID", var.ProjectID))
                        .And(new Expression("ProjectPartListVersionID", versionId)))
                        .FirstOrDefault();

                    if (existingPart != null)
                    {
                        partList.ID = existingPart.ID; // Lấy ID nếu tồn tại
                    }

                    if (partList.ID > 0)
                    {
                        SQLHelper<ProjectPartListModel>.Update(partList);
                    }
                    else
                    {
                        SQLHelper<ProjectPartListModel>.Insert(partList);
                    }



                    //string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    //string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();
                    //string maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
                    decimal minQuantity = TextUtils.ToDecimal(partList.QtyMin);
                    var exp5 = new Expression("ProductCode", partList.ProductCode);
                    var exp6 = new Expression("Unit", partList.Unit);
                    var exp7 = new Expression("Maker", partList.Manufacturer);



                    //ProjectPartListModel projectPartList = SQLHelper<ProjectPartListModel>.FindByExpression(exp5).FirstOrDefault();
                    //if (projectPartList == null) continue;
                    //var exp7 = new Expression("ProjectPartListID", projectPartList.ID);

                    ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp5.And(exp6).And(exp7)).FirstOrDefault() ?? new ProductSaleModel();
                    if (productSale.ID <= 0) continue;
                    var exp8 = new Expression("ProductSaleID", productSale.ID);
                    var exp9 = new Expression("WarehouseID", 1);

                    //InventoryModel inventory = SQLHelper<InventoryModel>.FindByExpression(exp8.And(exp9)).FirstOrDefault() ?? new InventoryModel();
                    //if (inventory.ID <= 0)
                    //{
                    //    //InventoryModel inventory1 = new InventoryModel();
                    //    inventory.ProductSaleID = productSale.ID;
                    //    inventory.WarehouseID = 1;
                    //    inventory.MinQuantity = minQuantity;
                    //    inventory.IsStock = minQuantity > 0;
                    //    inventory.Note = "";
                    //    SQLHelper<InventoryModel>.Insert(inventory);
                    //}
                    //else
                    //{
                    //    inventory.MinQuantity = minQuantity;
                    //    inventory.IsStock = minQuantity > 0;
                    //    SQLHelper<InventoryModel>.Update(inventory);
                    //}
                    //ndnhat update 18/08/2025
                    var exp10 = new Expression(InventoryStockModel_Enum.ProjectTypeID, var.ProjectTypeID);
                    var exp11 = new Expression(InventoryStockModel_Enum.EmployeeIDRequest, Global.EmployeeID);
                    var exp12 = new Expression(InventoryStockModel_Enum.IsDeleted, 0);

                    InventoryStockModel inventory = SQLHelper<InventoryStockModel>.FindByExpression(exp8.And(exp9).And(exp10)).FirstOrDefault() ?? new InventoryStockModel();
                    if (inventory.ID <= 0)
                    {
                        //InventoryModel inventory1 = new InventoryModel();
                        inventory.ProductSaleID = productSale.ID;
                        inventory.WarehouseID = 1;
                        inventory.Quantity = minQuantity;
                        inventory.EmployeeIDRequest = Global.EmployeeID;
                        inventory.WarehouseID = 1;
                        inventory.ProjectTypeID = var.ProjectTypeID;
                        inventory.IsDeleted = false;
                        //inventory.IsStock = minQuantity > 0;
                        inventory.Note = "";
                        SQLHelper<InventoryStockModel>.Insert(inventory);
                    }
                    else
                    {
                        inventory.Quantity = minQuantity;
                        //inventory.IsStock = minQuantity > 0;
                        SQLHelper<InventoryStockModel>.Update(inventory);
                    }
                    //end ndnhat update 18/08/2025
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $"\nStt: {stt}", "Thông báo");
            }
        }
        void UpdatePONCC()
        {
            int rowHandle = 0;
            string code = "";
            try
            {
                //bool isUpdatePartList = isUpdate.Checked;
                //int versionId = TextUtils.ToInt(cboVersion.EditValue);


                //var exp1 = new Expression("ProjectID", var.ProjectID);
                //var exp2 = new Expression("ProjectTypeID", var.ProjectTypeID);
                //var exp3 = new Expression("ProjectPartListVersionID", versionId);
                //var exp4 = new Expression("IsDeleted", 1, "<>");
                //var partlists = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
                //if (partlists.Count > 0 && !(chkIsProblem.Checked || isUpdatePartList))
                //{
                //    DialogResult dialog = MessageBox.Show("Danh mục vật tư đã tồn tại.\nBạn có muốn ghi đè không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (dialog != DialogResult.Yes) return;

                //    string idText = string.Join(",", partlists.Select(x => x.ID));
                //    string sql = $"UPDATE dbo.ProjectPartList SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE ID IN ({idText})";
                //    //SQLHelper<ProjectPartListModel>.DeleteListModel(partlists);

                //    TextUtils.ExcuteSQL(sql);
                //}

                ////NT.Huy update lay ra list cac parent TT
                //var listParentTT = new List<string>();
                //foreach (var item in partlists)
                //{
                //    if (!item.TT.Contains(".")) continue;
                //    string parentTt = item.TT.Substring(0, item.TT.LastIndexOf(".")).Trim();
                //    listParentTT.Add(parentTt);
                //}

                //Regex regex = new Regex(@"^-?[\d\.]+$");
                for (int i = 1; i < grvData.RowCount; i++)
                {
                    //progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    //txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    //string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();

                    //if (string.IsNullOrEmpty(stt)) continue;
                    //if (!regex.IsMatch(stt)) continue;
                    //if (isUpdatePartList && partlists.Any(x => x.TT == stt)) continue;
                    //if (partlists.Any(x => x.TT == stt)) continue;

                    rowHandle = i;
                    code = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));

                    PONCCHistoryModel po = new PONCCHistoryModel();

                    po.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F1")); //Ngày đơn hàng
                    po.CompanyText = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")); //Công ty nhập
                    po.BillCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")); ////Số đơn hàng
                    po.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")); //diễn giải
                    po.DeliveryDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F5"));//Ngày giao hàng
                    po.CodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));//Mã NCC
                    po.NameNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));//Tên NCC
                    po.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")); //Tên dự án
                    po.POCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));//Số PO
                    po.ProductNewCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));//Mã nội bộ

                    po.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F11"));//Tên hàng
                    po.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F12"));//MÃ sản phẩm
                    po.ProductCodeOfSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, "F13")); //Mã sp Ncc
                    po.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F14"));//ĐƠn vị tính
                    po.CurrencyName = TextUtils.ToString(grvData.GetRowCellValue(i, "F15"));//Loại tiền

                    po.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16"));//Đơn giá mua chưa VAT
                    po.UnitPriceVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F17"));//Đơn giá mua có VAT

                    po.QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F18"));//Số lượng đặt hàng
                    po.QuantityReturn = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F19"));//Số lượng đã nhận
                    po.QuantityRemain = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F20"));//Số lượng còn lại 
                    po.TotalMoneyChangePO = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F21"));//Giá trị đặt hàng quy đổi
                    po.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F22"));//Giá trị đặt hàng
                    po.StatusText = TextUtils.ToString(grvData.GetRowCellValue(i, "F23"));//Tình trạng
                    po.FullName = TextUtils.ToString(grvData.GetRowCellValue(i, "F24"));//Nhân viên mua hàng
                    po.NCCNew = !string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F25"))); //Phân loại NCC/NCC mới
                    po.DeptSupplier = !string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F26")));//Công nợ
                    po.FeeShip = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F27"));//Phí vận chuyển
                    po.PriceSale = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F28"));//Giá bán
                    po.DeadlineDelivery = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F29"));//Deadline giao hàng
                    po.ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F30"));//Mã dự án 
                    po.PriceHistory = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F31"));//Giá lịch sử
                    po.BiddingPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F32"));//Giá chào thầu
                    po.SupplierVoucher = TextUtils.ToString(grvData.GetRowCellValue(i, "F33"));//NCC xử lí chứng từ
                    po.TotalQuantityLast = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F34"));//Stock kho hiện tại
                    po.MinQuantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F35"));//Ghi chú stock kho
                    po.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F36")) < 0 ? 0 : TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F36")); //Thuế VAT
                    po.CurrencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F37"));//Tỷ giá

                    if (po.ID > 0)
                    {
                        SQLHelper<PONCCHistoryModel>.Update(po);
                    }
                    else
                    {
                        SQLHelper<PONCCHistoryModel>.Insert(po);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"{code}:{rowHandle}");
            }
        }

        int GetParentId(string tt, int versionId, bool isProblem, int projectTypeID)
        {
            int parentId = 0;
            if (!tt.Contains(".")) return parentId;

            string parentTt = tt.Substring(0, tt.LastIndexOf(".")).Trim();
            int isProblemValue = isProblem ? 1 : 0;

            var exp1 = new Expression("TT", parentTt);
            var exp2 = new Expression("ProjectPartListVersionID", versionId);
            var exp3 = new Expression("IsDeleted", 1, "<>");
            var exp4 = new Expression("IsProblem", isProblemValue);
            var exp5 = new Expression("ProjectTypeID", projectTypeID);
            ProjectPartListModel parent = SQLHelper<ProjectPartListModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5)).FirstOrDefault();
            if (parent != null && parent.ID > 0)
            {
                parentId = parent.ID;
            }
            return parentId;
        }

        private bool CheckValidate()
        {
            //bool result = true;

            //string pattern = @"^[a-zA-Z0-9_!@#$%^()&*\-\s]+$";
            string pattern = @"^[^àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ]+$";
            Regex regex = new Regex(pattern);
            Regex regexStt = new Regex(@"^-?[\d\.]+$");


            List<string> listStt = new List<string>();
            List<string> listSttAll = new List<string>();
            List<string> specialCodes = new List<string>();
            for (int i = 7; i < grvData.RowCount; i++)
            {
                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                if (string.IsNullOrEmpty(stt)) continue;
                listSttAll.Add(stt);

                if (string.IsNullOrEmpty(stt)) continue;
                specialCodes.Add(TextUtils.ToString(grvData.GetRowCellValue(i, "F25")).Trim());

                if (!stt.Contains(".")) continue;
                if (!regexStt.IsMatch(stt)) continue;
                stt = stt.Substring(0, stt.LastIndexOf("."));

                if (listStt.Contains(stt)) continue;
                listStt.Add(stt);
            }

            //NTA B - update 10/09/25
            DataTable dtError = new DataTable();
            dtError.Columns.Add("ID", typeof(int));
            dtError.Columns.Add("ProductCode", typeof(string));
            dtError.Columns.Add("GroupMaterial_Partlist", typeof(string));
            dtError.Columns.Add("GroupMaterial_Stock", typeof(string));
            dtError.Columns.Add("Manufacturer_Partlist", typeof(string));
            dtError.Columns.Add("Manufacturer_Stock", typeof(string));
            dtError.Columns.Add("Unit_Partlist", typeof(string));
            dtError.Columns.Add("Unit_Stock", typeof(string));
            dtError.Columns.Add("IsFix", typeof(bool));
            //END NTA B - update 10/09/25

            var exp1 = new Expression("IsDeleted", 0);

            for (int i = 7; i < grvData.RowCount; i++)
            {
                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                string groupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                string manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
                decimal qtyMin = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
                decimal qtyFull = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
                string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();

                if (string.IsNullOrEmpty(stt)) continue;
                if (!regexStt.IsMatch(stt)) continue;

                var stts = listSttAll.Where(x => x.Equals(stt)).ToList();
                if (stts.Count() > 1)
                {
                    MessageBox.Show($"TT [{stt}] đã tồn tại.\nVui lòng kiểm tra lại!", "Thông báo");
                    return false;
                }

                if (!listStt.Contains(stt))
                {
                    if (string.IsNullOrWhiteSpace(productCode))
                    {
                        MessageBox.Show($"Vui lòng nhập Mã thiết bị!.\n(TT: {stt})", TextUtils.Caption);
                        return false;
                    }
                    else
                    {
                        bool isCheck = regex.IsMatch(productCode);
                        if (!isCheck)
                        {
                            MessageBox.Show($"Mã thiết bị không được chứa ký tự tiếng Việt.\nVui lòng kiểm tra lại!.\n(TT: {stt})", TextUtils.Caption);
                            return false;
                        }
                    }


                    if (string.IsNullOrWhiteSpace(groupMaterial))
                    {
                        MessageBox.Show($"Vui lòng nhập Tên thiết bị!.\n(TT: {stt})", TextUtils.Caption);
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(manufacturer))
                    {
                        MessageBox.Show($"Vui lòng nhập Hãng!.\n(TT: {stt})", TextUtils.Caption);
                        return false;
                    }

                    if (qtyMin <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Số lượng / 1 máy (Phải  > 0)!.\n(TT: {stt})", TextUtils.Caption);
                        return false;
                    }

                    if (qtyFull <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Số lượng tổng (Phải  > 0)!.\n(TT: {stt})", TextUtils.Caption);
                        return false;
                    }

                    if (string.IsNullOrEmpty(unit))
                    {
                        MessageBox.Show("Vui lòng nhập Đơn vị!", "Thông báo");
                        return false;
                    }



                    //NTA B - update 10/09/25
                    var exp2 = new Expression("ProductCode", productCode);
                    var exp3 = new Expression("IsFix", 1);


                    //var productSales = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();
                    var fixedProduct = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();

                    //var fixedProduct = productSales.FirstOrDefault(x => x.IsFix);

                    //List<string> errors = new List<string>();

                    if (fixedProduct != null)
                    {
                        string productNameConvert = TextUtils.ConvertUnicode(fixedProduct.ProductName.ToLower(), 1);
                        string makerConvert = TextUtils.ConvertUnicode(fixedProduct.Maker.ToLower(), 1);
                        string unitConvert = TextUtils.ConvertUnicode(fixedProduct.Unit.ToLower(), 1);

                        if (productNameConvert != TextUtils.ConvertUnicode(groupMaterial.ToLower(), 1) ||
                            makerConvert != TextUtils.ConvertUnicode(manufacturer.ToLower(), 1) ||
                            unitConvert != TextUtils.ConvertUnicode(unit.ToLower(), 1)
                        )
                        {
                            //errors.Add($"\nMã sản phẩm (tích xanh: [{fixedProduct.ProductName}], hiện tại: [{groupMaterial}])");

                            DataRow dr = dtError.NewRow();
                            dr["ID"] = fixedProduct.ID;
                            dr["ProductCode"] = productCode;
                            dr["GroupMaterial_Partlist"] = groupMaterial;
                            dr["GroupMaterial_Stock"] = fixedProduct.ProductName;
                            dr["Manufacturer_Partlist"] = manufacturer;
                            dr["Manufacturer_Stock"] = fixedProduct.Maker;
                            dr["Unit_Partlist"] = unit;
                            dr["Unit_Stock"] = fixedProduct.Unit;
                            dr["IsFix"] = fixedProduct.IsFix;
                            dtError.Rows.Add(dr);
                        }
                        

                    }
                    //END -------------------------------------


                    //CHECK MÃ ĐẶC BIỆT
                    string specialCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F25")).Trim();
                    if (!string.IsNullOrWhiteSpace(specialCode))
                    {
                        ////Check có trùng trong list nhập vào không
                        //if (specialCodes.Contains(specialCode))
                        //{
                        //    MessageBox.Show($"Mã đặc biệt [{specialCode}] đã tồn tại (Stt: {stt})!", "Thông báo");
                        //    return false;
                        //}

                        //Check có trong đb không
                        var expSpecialCode1 = new Expression(ProjectPartListModel_Enum.SpecialCode, specialCode);
                        var expSpecialCode2 = new Expression(ProjectPartListModel_Enum.IsDeleted, 0);
                        var specialCodeDbs = SQLHelper<ProjectPartListModel>.FindByExpression(expSpecialCode1.And(expSpecialCode2));
                        if (specialCodeDbs.Count > 0)
                        {
                            MessageBox.Show($"Mã đặc biệt [{specialCode}] đã tồn tại (Stt: {stt})!", "Thông báo");
                            return false;
                        }
                    }
                }

                //if (string.IsNullOrEmpty(productCode)) continue;

            }

            if (dtError.Rows.Count > 0) //NTA B - update 10/09/25
            {
                DataTable finalDataDiff = null;
                bool userConfirmed = false;

                // Invoke về UI thread để show dialog
                this.Invoke(new Action(() =>
                {
                    frmPartlistImportExcelDiff frm = new frmPartlistImportExcelDiff();
                    frm.dataDiff = dtError.Copy();
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        userConfirmed = true;
                        finalDataDiff = frm.dataDiff.Copy();
                    }
                }));

                if (!userConfirmed)
                {
                    return false;
                }

                // Invoke để cập nhật grid
                this.Invoke(new Action(() =>
                {
                    foreach (DataRow dr in finalDataDiff.Rows)
                    {
                        string productCode = TextUtils.ToString(dr["ProductCode"]);
                        for (int i = 7; i < grvData.RowCount; i++)
                        {
                            string excelProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                            if (excelProductCode == productCode)
                            {
                                grvData.SetRowCellValue(i, "F2", dr["GroupMaterial_Stock"]);
                                grvData.SetRowCellValue(i, "F5", dr["Manufacturer_Stock"]);
                                grvData.SetRowCellValue(i, "F9", dr["Unit_Stock"]);
                            }
                        }
                    }
                    grvData.RefreshData();
                }));
            }

            return true;
        }

        private void isUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsUpdate.Checked) chkIsProblem.Checked = !chkIsUpdate.Checked;
        }

        private void chkIsProblem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsProblem.Checked) chkIsUpdate.Checked = !chkIsProblem.Checked;
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        void LoadDataDiff() // VTn Update
        {
            if (gridControl1.InvokeRequired)
            {
                gridControl1.Invoke((Action)(() => { gridControl1.DataSource = dataDiff; }));
            }
            else
            {
                gridControl1.DataSource = dataDiff;
            }

            if (flyoutPanel1.InvokeRequired)
            {
                flyoutPanel1.Invoke((Action)(() => { flyoutPanel1.ShowPopup(); }));
            }
            else
            {
                flyoutPanel1.ShowPopup();
            }
        }


        void UpdateProjectPartlistDiff(string tag)
        {
            try
            {
                bandedGridView1.CloseEditor();
                int[] selectedRows = bandedGridView1.GetSelectedRows();
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn cập nhật thông tin sản phẩm đã chọn theo thông tin kho không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    //List<FirmModel> firms = SQLHelper<FirmModel>.FindByAttribute(FirmModel_Enum.FirmType.ToString(), 1);
                    foreach (int row in selectedRows)
                    {
                        //Update thông tin mã đang có trong kho
                        string maker = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colManufacturerStock));
                        string unit = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colUnitStock));
                        string productName = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colGroupMaterialStock));
                        int id = TextUtils.ToInt(bandedGridView1.GetRowCellValue(row, colID));
                        //int productSaleID = TextUtils.ToInt(bandedGridView1.GetRowCellValue(row, colProductSaleID));

                        if (string.IsNullOrWhiteSpace(maker)) maker = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colManufacturer));
                        if (string.IsNullOrWhiteSpace(unit)) unit = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colUnit));
                        if (string.IsNullOrWhiteSpace(productName)) productName = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colGroupMaterial));

                        if (tag == "btnUpdatePartlist")
                        {
                            maker = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colManufacturer));
                            unit = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colUnit));
                            productName = TextUtils.ToString(bandedGridView1.GetRowCellValue(row, colGroupMaterial));
                        }

                        var myDict = new Dictionary<string, object>()
                        {
                            { ProjectPartListModel_Enum.Manufacturer.ToString(),maker},
                            { ProjectPartListModel_Enum.Unit.ToString(),unit},
                            { ProjectPartListModel_Enum.GroupMaterial.ToString(),productName},
                            { ProjectPartListModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                            { ProjectPartListModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                        };

                        SQLHelper<ProjectPartListModel>.UpdateFieldsByID(myDict, id);
                    }

                    MessageBox.Show("Cập nhật thành công!", "Thông báo");

                    LoadDataDiff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void flyoutPanel1_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            string tag = e.Button.Tag.ToString();
            switch (tag)
            {
                case "btnUpdatePartlist"://Update theo partlist
                    UpdateProjectPartlistDiff("btnUpdatePartlist");
                    break;
                case "btnUpdateStock": //Update theo kho
                    UpdateProjectPartlistDiff("btnUpdateStock");
                    break;
                case "btnCancel":
                    flyoutPanel1.HidePopup();
                    break;
            }
        }

        private void bandedGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(bandedGridView1.GetFocusedRowCellValue(bandedGridView1.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void bandedGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int totalSameValue = 0;
            if (e.Column == colProductCode)
            {
                totalSameValue = TextUtils.ToInt(bandedGridView1.GetRowCellValue(e.RowHandle, colIsSameProductCode));
                if (totalSameValue != 0) return;
                e.Appearance.BackColor = System.Drawing.Color.Pink;
            }

            //check tên
            if (e.Column == colGroupMaterial || e.Column == colGroupMaterialStock)
            {
                totalSameValue = TextUtils.ToInt(bandedGridView1.GetRowCellValue(e.RowHandle, colIsSameProductName));
                if (totalSameValue != 0) return;
                e.Appearance.BackColor = System.Drawing.Color.Pink;
            }

            //check hãng
            if (e.Column == colManufacturer || e.Column == colManufacturerStock)
            {
                totalSameValue = TextUtils.ToInt(bandedGridView1.GetRowCellValue(e.RowHandle, colIsSameMaker));
                if (totalSameValue != 0) return;
                e.Appearance.BackColor = System.Drawing.Color.Pink;
            }

            //check đơn vị
            if (e.Column == colUnit || e.Column == colUnitStock)
            {
                totalSameValue = TextUtils.ToInt(bandedGridView1.GetRowCellValue(e.RowHandle, colIsSameUnit));
                if (totalSameValue != 0) return;
                e.Appearance.BackColor = System.Drawing.Color.Pink;
            }
        }


        #region Vtn update
        //private List<ProjectPartListModel> projectPartLists = SQLHelper<ProjectPartListModel>
        //    .FindAll()
        //    .GroupBy(x => new { x.ProductCode, x.Manufacturer, x.Unit, x.GroupMaterial })
        //    .Select(g => g.First())
        //    .ToList();
        private List<ProjectPartListModel> lsFillterProducts = new List<ProjectPartListModel>();


        private void bandedGridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == colSuggest.FieldName)
            {
                RepositoryItemGridLookUpEdit gridLookup = new RepositoryItemGridLookUpEdit();
                gridLookup.DisplayMember = "";
                gridLookup.ValueMember = "ID";
                gridLookup.PopupView = cboProduct.View;
                gridLookup.NullText = "Gợi ý";

                string productCode = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colProductCode));

                if (!projectPartLists.Any(x => x.ProductCode == productCode))
                {
                    string manufacturer = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colManufacturer));
                    string unit = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colUnit));
                    string groupMaterial = TextUtils.ToString(bandedGridView1.GetRowCellValue(e.RowHandle, colGroupMaterial));

                    ProjectPartListModel projectPartList = new ProjectPartListModel();
                    projectPartList.ProductCode = productCode;
                    projectPartList.Manufacturer = manufacturer;
                    projectPartList.Unit = unit;
                    projectPartList.GroupMaterial = groupMaterial;

                    lsFillterProducts.Add(projectPartList);
                }

                if (productCode != "")
                {
                    string cleanedCode = Regex.Replace(productCode, "[^a-zA-Z0-9]", " ");
                    string[] productCodes = cleanedCode.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    productCodes = productCodes.Distinct().ToArray();
                    lsFillterProducts = projectPartLists.Where(x => productCodes.Any(p => x.ProductCode.Contains(p))).ToList();
                }

                gridLookup.DataSource = lsFillterProducts;

                e.RepositoryItem = gridLookup;

                gridLookup.EditValueChanged += (s, args) =>
                {
                    GridLookUpEdit editor = s as GridLookUpEdit;
                    GridView popupView = editor.Properties.View as GridView;
                    int rowSelect = popupView.FocusedRowHandle;

                    ProjectPartListModel projectPartList = (ProjectPartListModel)popupView.GetRow(rowSelect);
                    if (projectPartList != null)
                    {
                        bandedGridView1.SetFocusedRowCellValue(colProductCode, projectPartList.ProductCode);
                        bandedGridView1.SetFocusedRowCellValue(colGroupMaterial, projectPartList.GroupMaterial);
                        bandedGridView1.SetFocusedRowCellValue(colManufacturer, projectPartList.Manufacturer);
                        bandedGridView1.SetFocusedRowCellValue(colUnit, projectPartList.Unit);
                    }
                };
            }
        }

        #endregion

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}