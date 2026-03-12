using BMS;
using BMS.Business;
using BMS.Model;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInputExcelHMCV : _Forms
    {
        //DataSet ds;
        //int temp;
        //DateTime start;
        //int quantity;
        public int ProjectID = 0;
        public frmInputExcelHMCV()
        {
            InitializeComponent();
        }

        private void frmInputExcelHMCV_Load(object sender, EventArgs e)
        {

        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("MauHMCV.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("MauHMCV.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        DateTime start;
        DataSet ds;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                if (grvData.RowCount == 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = grvData.RowCount;
                    txtRate.Text = "";
                    start = DateTime.Now;
                    enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        public RepositoryItemMemoEdit repositoryItemMemoEdit = new RepositoryItemMemoEdit();
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
            {
                grvData.Columns.Clear();
                try
                {
                    var tablename = cboSheet.SelectedItem.ToString();

                    //grvData.Columns["F6"].ColumnEdit = repositoryItemMemoEdit;
                    //grvData.Columns["F6"].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                    //grvData.Columns["F6"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

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

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
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
                    //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0)
                        cboSheet.SelectedIndex = 0;

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
                        //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

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
        int userID;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

                    ProjectItemModel pi = new ProjectItemModel();

                    pi.ProjectID = ProjectID;
                    if (TextUtils.ToInt(grvData.GetRowCellValue(i, "F1")) <= 0) continue;
                    pi.STT = TextUtils.ToString(grvData.GetRowCellValue(i, "F1"));
                    string projectTypeName = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    int idProjectType = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID FROM ProjectType WHERE ProjectTypeName LIKE '{projectTypeName}'"));
                    pi.TypeProjectItem = idProjectType;
                    string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    if (status.Contains("Chưa làm"))
                    {
                        pi.Status = 0;
                    }
                    else if (status.Contains("Đang làm"))
                    {
                        pi.Status = 1;
                    }
                    else if (status.Contains("Đã hoàn thành"))
                    {
                        pi.Status = 2;
                    }
                    else
                    {
                        pi.Status = 3;
                    }
                    //string userName = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));

                    //pi.UserID = TextUtils.ToInt("");
                    pi.PercentItem = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F5"));
                    pi.Mission = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    pi.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                    pi.PlanStartDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F8"));
                    pi.TotalDayPlan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));
                    pi.PlanEndDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F10"));
                    pi.ActualStartDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F11"));
                    pi.ActualEndDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F12"));

                    userID = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 UserID FROM Employee WHERE Code = '{TextUtils.ToString(grvData.GetRowCellValue(i, "F13"))}'"));
                    pi.UserID = userID;

                    pi.ItemLate = 0;

                    DateTime actualDE = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F12"));
                    DateTime actualDS = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F11"));
                    DateTime planDE = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F10"));

                    string strActualDS = TextUtils.ToString(grvData.GetRowCellValue(i, "F11")).Trim();
                    string strActualDE = TextUtils.ToString(grvData.GetRowCellValue(i, "F12")).Trim();

                    //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
                    //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed

                    if (!string.IsNullOrEmpty(strActualDS) && string.IsNullOrEmpty(strActualDE))
                    {
                        if ((actualDS - planDE).TotalDays > 0)
                        {
                            pi.ItemLate = 2; //Failed (Màu đỏ)
                        }
                    }

                    //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
                    //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
                    if (!string.IsNullOrEmpty(strActualDS) && !string.IsNullOrEmpty(strActualDE))
                    {
                        if ((actualDE - planDE).TotalDays > 0)
                        {
                            pi.ItemLate = 1; //Chậm (Màu cam)
                        }
                    }

                    //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
                    //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed

                    if (string.IsNullOrEmpty(strActualDS) && string.IsNullOrEmpty(strActualDE))
                    {
                        if ((DateTime.Now - planDE).TotalDays > 0)
                        {
                            pi.ItemLate = 2; //Failed (Màu đỏ)
                        }
                    }

                    //if (string.IsNullOrEmpty(strActualDE))
                    //{
                        
                    //    if ((date - planDE).TotalDays > 0)
                    //    {
                    //        pi.ItemLate = 1;
                    //    }
                    //}
                    //else
                    //{
                    //    if ((actualDE - planDE).TotalDays > 0)
                    //    {
                    //        pi.ItemLate = 1;
                    //    }
                    //}

                    //if ((actualDE - planDE).TotalDays > 0)
                    //{
                    //    pi.ItemLate = 1;
                    //}




                    ProjectItemBO.Instance.Insert(pi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            enableControl(true);
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Black;

                //DateTime actualDE = TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, "F12"));
                ////DateTime actualDS = TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, "F11"));

                ////DateTime planDS = TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, "F8"));
                //DateTime planDE = TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, "F10"));



                //if ((actualDE - planDE).TotalDays > 0)
                //{
                //    //e.Appearance.BackColor = Color.FromArgb(255, 205, 210);
                //    e.Appearance.BackColor = Color.Orange;
                //    //e.Appearance.ForeColor = Color.White;
                //    e.HighPriority = true;
                //}


                DateTime actualDE = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle, "F12"));
                DateTime actualDS = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle, "F11"));
                DateTime planDE = TextUtils.ToDate5(grvData.GetRowCellValue(e.RowHandle, "F10"));

                string strActualDS = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, "F11")).Trim();
                string strActualDE = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, "F12")).Trim();

                //Nếu đã có ngày bắt đầu thực tế và chưa có ngày kết thúc thực tế
                //Nếu ngày bắt đầu thực tế > ngày kết thúc dự kiến --> Failed

                if (!string.IsNullOrEmpty(strActualDS) && string.IsNullOrEmpty(strActualDE))
                {
                    if ((actualDS - planDE).TotalDays > 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }

                //Nếu đã có ngày bắt đầu thực tế và ngày kết thúc thực tế
                //Nếu ngày kết thúc thực tế > ngày kết thúc dự kiến --> Chậm
                if (!string.IsNullOrEmpty(strActualDS) && !string.IsNullOrEmpty(strActualDE))
                {
                    if ((actualDE - planDE).TotalDays > 0)
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.ForeColor = Color.White;
                    }
                }

                //Nếu chưa có ngày bắt đầu thực tế và ngày kết thúc thực tế
                //Nếu ngày hiện tại > ngày kết thúc dự kiến --> Failed

                if (string.IsNullOrEmpty(strActualDS) && string.IsNullOrEmpty(strActualDE))
                {
                    if ((DateTime.Now - planDE).TotalDays > 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    int rowCount = grvData.RowCount;
        //    int colCount = grvData.Columns.Count;
        //    for (int i = 1; i < rowCount; i++)
        //    {
        //        try
        //        {

        //            progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
        //            txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
        //            ProjectItemModel pi = new ProjectItemModel();

        //            pi.ProjectID = ProjectID;
        //            if (TextUtils.ToInt(grvData.GetRowCellValue(i, "F1")) <= 0) continue;
        //            pi.STT = TextUtils.ToString(grvData.GetRowCellValue(i, "F1"));
        //            string projectTypeName = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
        //            int idProjectType = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID FROM ProjectType WHERE ProjectTypeName LIKE '{projectTypeName}'"));
        //            pi.TypeProjectItem = idProjectType;
        //            string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
        //            if (status.Contains("Chưa làm"))
        //            {
        //                pi.Status = 0;
        //            }
        //            else if (status.Contains("Đang làm"))
        //            {
        //                pi.Status = 1;
        //            }
        //            else if (status.Contains("Đã hoàn thành"))
        //            {
        //                pi.Status = 2;
        //            }
        //            else
        //            {
        //                pi.Status = 3;
        //            }
        //            //string userName = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));

        //            //pi.UserID = TextUtils.ToInt("");
        //            pi.PercentItem = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F5"));
        //            pi.Mission = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
        //            pi.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
        //            pi.PlanStartDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F8"));
        //            pi.TotalDayPlan = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));
        //            pi.PlanEndDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F10"));
        //            pi.ActualStartDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F11"));
        //            pi.ActualEndDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F12"));
        //            int userID = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID FROM Users WHERE Code LIKE '{TextUtils.ToString(grvData.GetRowCellValue(i, "F13"))}'"));
        //            pi.UserID = userID;



        //            ProjectItemBO.Instance.Insert(pi);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
        //        }
        //    }
        //}


    }
}