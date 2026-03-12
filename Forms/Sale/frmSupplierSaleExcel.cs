using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraSplashScreen;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSupplierSaleExcel : _Forms
    {
        public frmSupplierSaleExcel()
        {
            InitializeComponent();
        }
        DateTime start;
        DataSet ds;
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
                        cboSheet.SelectedIndex = -1;
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

        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }

        private void frmImportCheckForceExcel_Load(object sender, EventArgs e)
        {

            //ArrayList
            btnSave.Enabled = false;
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            if (chkAutoCheck.Checked)
            {
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
            }
            else
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
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }


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

            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    //string pGroupName = string.Empty;
                    //string cellProductID = grvData.GetRowCellValue(i, "F2").ToString();
                    //if (cellProductID.IndexOf(' ') == -1)
                    //{
                    //    pGroupName = "Khác";
                    //}
                    //else
                    //{
                    //    string regex = "^[a-z A-Z]+[0-9]+";
                    //    if (!Regex.IsMatch(cellProductID, regex))
                    //        pGroupName = cellProductID;
                    //    else
                    //        pGroupName = cellProductID.Substring(cellProductID.IndexOf(' ') + 1);
                    //}

                    string codeSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    DateTime? updateDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F1"));
                    if (!updateDate.HasValue || string.IsNullOrEmpty(codeSupplier)) continue;

                    SupplierSaleModel model = new SupplierSaleModel();

                    ArrayList array = SupplierSaleBO.Instance.FindByAttribute("CodeNCC", codeSupplier);
                    if (array.Count > 0)
                    {
                        model = (SupplierSaleModel)array[0];
                    }
                    if (!string.IsNullOrEmpty(codeSupplier))
                    {
                        //string group = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());
                        ////int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"select ID from ProductGroup where ProductGroupName = N'{group}'"));
                        ////model.ID = id;


                        // model.NgayUpdate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F1").ToString());

                        //model.NgayUpdate = DateTime.Now;
                        model.NgayUpdate = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F1").ToString());
                        model.NVPhuTrach = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());
                        model.LoaiHangHoa = TextUtils.ToString(grvData.GetRowCellValue(i, "F3").ToString());
                        model.Brand = TextUtils.ToString(grvData.GetRowCellValue(i, "F4").ToString());
                        model.MaNhom = TextUtils.ToString(grvData.GetRowCellValue(i, "F5").ToString());
                        model.CodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F6").ToString());// mã sản phẩm
                        model.NameNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F7").ToString());// tên
                        model.TenTiengAnh = TextUtils.ToString(grvData.GetRowCellValue(i, "F8").ToString());// tên
                        model.AddressNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F9").ToString());// tên
                        model.MaSoThue = TextUtils.ToString(grvData.GetRowCellValue(i, "F10").ToString());// tên
                        model.Website = TextUtils.ToString(grvData.GetRowCellValue(i, "F11").ToString());// tên
                        model.Debt = TextUtils.ToString(grvData.GetRowCellValue(i, "F12").ToString());// tên
                        model.SoTK = TextUtils.ToString(grvData.GetRowCellValue(i, "F13").ToString());// tên
                        model.NganHang = TextUtils.ToString(grvData.GetRowCellValue(i, "F14").ToString());// tên
                        //model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F17"));// tên

                        string company = TextUtils.ToString(grvData.GetRowCellValue(i, "F15")).Trim();
                        model.Company = company.ToUpper() == "RTC" ? 1 : (company.ToUpper() == "MVI" ? 2 : (company.ToUpper() == "APR" ? 3 : 4));
                        model.ShortNameSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, "F16"));// tên
                        model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F20"));// tên

                        if (model.ID > 0)
                        {
                            SupplierSaleBO.Instance.Update(model);
                        }
                        else
                        {
                            model.ID = (int)SupplierSaleBO.Instance.Insert(model);
                        }

                        SupplierSaleContactModel detail = new SupplierSaleContactModel();
                        //int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                        //if (id > 0)
                        //{
                        //    detail = (SupplierSaleContactModel)SupplierSaleContactBO.Instance.FindByPK(id);
                        //}

                        //detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));

                        detail.SupplierID = model.ID;
                        detail.SupplierName = TextUtils.ToString(grvData.GetRowCellValue(i, "F18"));
                        detail.SupplierPhone = TextUtils.ToString(grvData.GetRowCellValue(i, "F17"));
                        detail.SupplierEmail = TextUtils.ToString(grvData.GetRowCellValue(i, "F19"));
                        //detail.Describe = TextUtils.ToString(grvData.GetRowCellValue(i, "F18"));
                        SupplierSaleContactBO.Instance.Insert(detail);
                        //if (id == 0)
                        //{

                        //}
                        //else
                        //{
                        //    SupplierSaleContactBO.Instance.Update(detail);
                        //}
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
            MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
            enableControl(true);
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("TemplateImportSupplier.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("TemplateImportSupplier.xlsx");
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

        private void frmSupplierSaleExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
