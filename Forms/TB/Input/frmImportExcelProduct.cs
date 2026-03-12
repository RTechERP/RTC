using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmImportExcelProduct : _Forms
    {
        DateTime start;
        DataSet ds;
        string tablename = string.Empty;
        public frmImportExcelProduct()
        {
            InitializeComponent();
        }

        private void frmImportExcelProduct_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
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

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (new WaitDialogForm("VUI LÒNG CHỜ TRONG GIÂY LÁT...", "PHẦN MỀM ĐANG TIẾN HÀNH TRÍCH XUẤT DỮ LIỆU!", new Size(380, 50)))
            {
                grdData.DataSource = null;
                try
                {
                    tablename = cboSheet.SelectedItem.ToString();
                    grdData.DataSource = ds;
                    grdData.DataMember = tablename;
                }
                catch (Exception ex)
                {
                    Lib.ShowError(ex);
                    grdData.DataSource = null;
                }
                if (grdData.DataSource == null)
                {
                    try
                    {
                        DataTable dt = Lib.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                        grdData.DataSource = dt;
                        grvData.PopulateColumns();
                        grvData.Focus();
                    }
                    catch (Exception ex)
                    {
                        Lib.ShowError(ex);
                        grdData.DataSource = null;
                    }
                }

                for (int i = 0; i < grvData.Columns.Count; i++)
                {
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    GridColumn column = grvData.Columns[i];
                    column.ColumnEdit = memoEdit;
                    column.Width = 150;

                    if (i >= 0 && i <= 24)
                    {
                        char alpha = (char)('A' + i);
                        column.Caption = alpha.ToString();
                    }
                    else if (i > 24)
                    {
                        char alpha = (char)('A' + i - 25);
                        column.Caption = "A" + alpha.ToString();
                    }
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
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            if (chkAutoCheck.Checked)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                var result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    btnBrowse.Text = openFileDialog1.FileName;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }

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
            else
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
                    btnSave.Enabled = true;
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
            {
                MessageBox.Show("Vui lòng chọn Sheet");
                return;
            }
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
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


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (new WaitDialogForm("VUI LÒNG CHỜ TRONG GIÂY LÁT...", "PHẦN MỀM ĐANG TIẾN HÀNH NHẬP DỮ LIỆU!", new Size(350, 50)))
            {
                //string strInsert = "";
                string strUpdate = "";
                //List<ProductRTCModel> listAllProduct = SQLHelper<ProductRTCModel>.FindAll();
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    //ProductRTCModel importProduct = new ProductRTCModel();
                    //var x = grvData.Columns;
                    string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F18")).Trim();
                    string inputValue = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    string outputValue = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    string rateCurrent = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
                    string makerName = TextUtils.ToString(grvData.GetRowCellValue(i, "F7")).Trim();
                    var exp1 = new Expression(FirmModel_Enum.FirmName.ToString(), makerName);
                    var exp2 = new Expression(FirmModel_Enum.FirmType.ToString(), 2);
                    FirmModel maker = SQLHelper<FirmModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new FirmModel();

                    if (string.IsNullOrEmpty(code)) continue;


                    //ProductRTCModel checkProduct = listAllProduct.Where(a => a.ProductCodeRTC == code || a.ProductCode == code).FirstOrDefault() ?? new ProductRTCModel();
                    ProductRTCModel productRTC = SQLHelper<ProductRTCModel>.FindByAttribute("ProductCodeRTC", code).FirstOrDefault() ?? new ProductRTCModel();
                    if (productRTC.ID <= 0) continue;
                    //productRTC.InputValue = inputValue;
                    //productRTC.OutputValue = outputValue == null ? "" : outputValue;
                    //productRTC.CurrentIntensityMax = rateCurrent == null ? "" : rateCurrent;

                    strUpdate += $"Update ProductRTC SET InputValue = '{inputValue}'," +
                                                        $"OutputValue = '{outputValue}'," +
                                                        $"CurrentIntensityMax = '{rateCurrent}'," +
                                                        $"FirmID = {maker.ID}," +
                                                        $"Maker = '{makerName}'" +
                                                        $"where ID = {productRTC.ID};\n";

                }
                TextUtils.ExcuteSQL(strUpdate);

            }
            this.DialogResult = DialogResult.OK;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            enableControl(true);
            this.DialogResult = DialogResult.OK;
        }
    }
}