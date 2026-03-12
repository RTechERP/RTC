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
    public partial class frmPOKHExcel : _Forms
    {
        public frmPOKHExcel()
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
        DataTable dt;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int rowCount = grvData.RowCount;
                int colCount = grvData.Columns.Count;

                for (int i = 1; i < rowCount; i++)
                {

                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

                    POKHModel poKH = new POKHModel();
                    POKHDetailModel _poKHDetail = new POKHDetailModel();
                    ArrayList array = POKHBO.Instance.FindByAttribute("Status", grvData.GetRowCellValue(i, "F2").ToString());
                    if (array.Count > 0)
                    {
                        poKH = (POKHModel)array[0];
                    }
                    if (grvData.GetRowCellValue(i, "F2").ToString() != "")
                    {
                        poKH.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, "F2").ToString());// trạng thái
                        poKH.POCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3").ToString());// mã po
                        poKH.UserID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F4").ToString());// ng phụ trách
                        poKH.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F5").ToString());// dự án
                        poKH.BillCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6").ToString());// mã hóa đơn
                        poKH.ReceivedDatePO = TextUtils.ToDate3(grvData.GetRowCellValue(i, "F7").ToString());//ngày nhận po
                        poKH.TotalMoneyPO = TextUtils.ToInt(grvData.GetRowCellValue(i, "F8").ToString());//tổng tiền po
                        poKH.StartDate = TextUtils.ToDate3(grvData.GetRowCellValue(i, "F12").ToString());// ngày yêu cầu giao hàng
                        poKH.EndDate = TextUtils.ToDate3(grvData.GetRowCellValue(i, "F13").ToString());// ngày giao hàng thực tế
                        poKH.DeliveryStatus = TextUtils.ToInt(grvData.GetRowCellValue(i, "F14").ToString());//tình trạng tiến độ giao hàng
                        poKH.ImportStatus = TextUtils.ToInt(grvData.GetRowCellValue(i, "F15").ToString());//tình trạng nhập kho
                        poKH.ExportStatus = TextUtils.ToInt(grvData.GetRowCellValue(i, "F16").ToString());//tình trạng xuất kho
                        poKH.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F17").ToString());// ghi chú
                        Expression exp1 = new Expression("POCode", poKH.POCode);
                        Expression exp2 = new Expression("UserName", poKH.UserName);
                        ArrayList arr = ProductSaleBO.Instance.FindByExpression(exp1.And(exp2));
                        if (array.Count > 0)
                        {
                            POKHBO.Instance.Update(poKH);
                        }
                        else
                            poKH.ID = (int)POKHBO.Instance.Insert(poKH);
                    }
                    string code = "";
                    string khachhang = "";
                    _poKHDetail.POKHID = poKH.ID;

                    //mã sản phẩm
                    string _code = TextUtils.ToString(grvData.GetRowCellValue(i, "F18").ToString());
                    dt = TextUtils.Select("SELECT ID,ProductCode FROM ProductSale where ProductCode = N'" + _code + "' ");

                    foreach (DataRow row in dt.Rows)
                    {
                        code = row[0].ToString();
                    }
                    if (code == "") return;
                    _poKHDetail.ProductID = TextUtils.ToInt(code);

                    //khách hàng
                    string _makh = TextUtils.ToString(grvData.GetRowCellValue(i, "F20").ToString());
                    DataTable dttt = TextUtils.Select("SELECT ID,MaKhachHang FROM ProductKhachHang where MaKhachHang = N'" + _makh + "' ");
                    foreach (DataRow row in dttt.Rows)
                    {
                        khachhang = row[0].ToString();
                    }
                    if (khachhang == "") return;
                    _poKHDetail.KHID = TextUtils.ToInt(khachhang);

                    //_poKHDetail.KHID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F20").ToString());
                    _poKHDetail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, "F24").ToString()); // Số lượng
                    _poKHDetail.UnitPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, "F25").ToString()); // đơn giá
                    _poKHDetail.IntoMoney = TextUtils.ToInt(grvData.GetRowCellValue(i, "F26").ToString()); // thành tiền

                    POKHDetailBO.Instance.Insert(_poKHDetail);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
            }
            this.DialogResult = DialogResult.OK;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
            enableControl(true);
        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
