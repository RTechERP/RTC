using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExcelEmployeeCurricular : _Forms
    {
        public frmExcelEmployeeCurricular()
        {
            InitializeComponent();
        }
        private void frmExcelEmployeeCurricular_Load(object sender, EventArgs e)
        {

        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            gdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        DateTime start;
        DataSet ds;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboSheet.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn sheet!");
                return;
            }
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount - 1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;

            for (int i = 0; i < rowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                if (stt <= 0) continue;

                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    string empCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();

                    int empID = 0;
                    EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", empCode).FirstOrDefault();
                    if (employee != null) empID = employee.ID;

                    int cDay = TextUtils.ToInt(grvData.GetRowCellValue(i, "F7"));
                    int cMonth = TextUtils.ToInt(grvData.GetRowCellValue(i, "F8"));
                    int cYear = TextUtils.ToInt(grvData.GetRowCellValue(i, "F9"));



                    var exp1 = new Expression("EmployeeID", empID);
                    var exp2 = new Expression("CurricularDay", cDay);
                    var exp3 = new Expression("CurricularMonth", cMonth);
                    var exp4 = new Expression("CurricularYear", cYear);

                    EmployeeCurricularModel model = SQLHelper<EmployeeCurricularModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).FirstOrDefault();
                    if (model == null) model = new EmployeeCurricularModel();


                    model.EmployeeID = empID;
                    model.CurricularCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                    model.CurricularName = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    model.CurricularDay = TextUtils.ToInt(grvData.GetRowCellValue(i, "F7"));
                    model.CurricularMonth = TextUtils.ToInt(grvData.GetRowCellValue(i, "F8"));
                    model.CurricularYear = TextUtils.ToInt(grvData.GetRowCellValue(i, "F9"));
                    model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));
                    if (model.ID > 0)
                    {
                        EmployeeCurricularBO.Instance.Update(model);
                    }
                    else
                    {
                        model.ID = (int)EmployeeCurricularBO.Instance.Insert(model);
                    }
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
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
            {
                grvData.Columns.Clear();
                try
                {
                    var tablename = cboSheet.SelectedItem.ToString();

                    gdData.DataSource = ds; // dataset
                    gdData.DataMember = tablename;
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    gdData.DataSource = null;
                }
                if (gdData.DataSource == null)
                {
                    try
                    {
                        DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                        gdData.DataSource = dt;
                        grvData.PopulateColumns();
                        grvData.BestFitColumns();
                        gdData.Focus();
                    }
                    catch (Exception ex)
                    {
                        TextUtils.ShowError(ex);
                        gdData.DataSource = null;
                    }
                }
            }
        }
        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("Danh_Muc_Nhan_Vien_Ngoai_Khoa.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("Danh_Muc_Nhan_Vien_Ngoai_Khoa.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
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

                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0)
                        cboSheet.SelectedIndex = 0;

                    btnSave.Enabled = true;
                    var tablename = cboSheet.SelectedItem.ToString();

                    gdData.DataSource = ds; // dataset
                    gdData.DataMember = tablename;

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

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stackPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboSheet_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}