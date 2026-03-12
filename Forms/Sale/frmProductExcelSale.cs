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
    public partial class frmProductExcelSale : _Forms
    {
        public frmProductExcelSale()
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

        private string NewCodeProduct(int id)
        {
            string _NewCodeRTC = "";
            DataSet ds = TextUtils.LoadDataSetFromSP("spLoadNewCodeRTC", new string[] { "@Group" }, new object[] { id });
            string code = "";
            string codeRTC = TextUtils.ToString(ds.Tables[1].Rows[0][0]);
            if (ds.Tables[0].Rows.Count == 0)
            {
                _NewCodeRTC = codeRTC + "000000001";
            }
            else
            {
                if (!codeRTC.Contains("HCM"))
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    for (int i = 0; codeRTC.Length < (9 - stt.ToString().Length); i++)
                    {
                        codeRTC = codeRTC + "0";
                    }
                    _NewCodeRTC = codeRTC + stt.ToString();
                }
                else
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    string indexString = TextUtils.ToString(stt);
                    for (int i = 0; indexString.Length < code.Length; i++)
                    {
                        indexString = "0" + indexString;
                    }
                    _NewCodeRTC = codeRTC + indexString.ToString();
                }
            }

            return _NewCodeRTC;
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
                    
                    string pGroupName = string.Empty;
                    string cellProductID = grvData.GetRowCellValue(i, "F1").ToString();
                    if (cellProductID.IndexOf(' ') == -1)
                    {
                        pGroupName = "Khác";
                    }
                    else
                    {
                        string regex = "^[a-z A-Z]+[0-9]+";
                        if (!Regex.IsMatch(cellProductID, regex))
                            pGroupName = cellProductID;
                        else
                            pGroupName = cellProductID.Substring(cellProductID.IndexOf(' ') + 1);
                    }
                    ArrayList arrGrModel = ProductGroupBO.Instance.FindByAttribute("ProductGroupName", pGroupName);
                    int pGroupID;
                    if (arrGrModel == null || arrGrModel.Count == 0)
                    {
                        ProductGroupModel grModel = new ProductGroupModel();
                        string sql = "Select max(ProductGroupID) from ProductGroup";
                        string newGroupNo = TextUtils.ToString(TextUtils.ToInt(Lib.ExcuteScalar(sql)) + 1);

                        grModel.ProductGroupID = newGroupNo;
                        grModel.ProductGroupName = pGroupName;
                        ProductGroupBO.Instance.Insert(grModel);
                        string sql_GetNewID = "Select max(ID) from ProductGroup";
                        pGroupID = TextUtils.ToInt(Lib.ExcuteScalar(sql_GetNewID));
                    }
                    else
                    {

                        ProductGroupModel grModel = arrGrModel[0] as ProductGroupModel;
                        pGroupID = grModel.ID;
                    }
                    ProductSaleModel model = new ProductSaleModel();
                    string productCode = grvData.GetRowCellValue(i, "F2").ToString();
                    ArrayList array = ProductSaleBO.Instance.FindByAttribute("ProductCode", productCode);
                    if (array.Count > 0)
                    {
                        model = (ProductSaleModel)array[0];
                    }
                    if (grvData.GetRowCellValue(i, "F2").ToString() != "")
                    {
                        string group = TextUtils.ToString(grvData.GetRowCellValue(i, "F1").ToString()); //Tên nhóm
                        int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"select ID from ProductGroup where ProductGroupName = N'{group}'"));

                        model.ProductGroupID = id;
                        model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());// mã sản phẩm
                        //model.ProductNewCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3").ToString());// mã Nội bộ
                       
                        model.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F3").ToString());// tên
                        model.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F4").ToString());//Hãng
                        model.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F5").ToString());//dvt
                        //model.NumberInStoreDauky = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7").ToString());//Tồn kho đầu kỳ
                        //model.Import = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8").ToString());//TNhập
                        //model.Export = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9").ToString());//Xuất
                        //model.NumberInStoreCuoiKy = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10").ToString());// Tồn kho Cuối kỳ
                        model.AddressBox = TextUtils.ToString(grvData.GetRowCellValue(i, "F6").ToString());//Vị trí (Hộp)
                        //model.ItemType = TextUtils.ToString(grvData.GetRowCellValue(i, "F12").ToString());
                        model.Note = grvData.GetRowCellValue(i, "F7").ToString();//Ghi chú
                        

                        Expression exp2 = new Expression("ProductCode", model.ProductCode);
                        //Expression exp1 = new Expression("AddressBox", model.AddressBox);
                        Expression exp3 = new Expression("ProductName", model.ProductName);
                        ArrayList arr = ProductSaleBO.Instance.FindByExpression(exp2.And(exp3));
                        if (arr.Count > 0)
                        {
                            for (int j = 0; j < arr.Count; j++)
                            {
                                //model.UpdateDate = DateTime.Now;
                                //model.ProductNewCode = NewCodeProduct(id);
                                model.ID = (arr[j] as ProductSaleModel).ID;
                                ProductSaleBO.Instance.Update(model);
                            }
                            //ProductSaleBO.Instance.Update(model);
                        }
                        else
                        {
                            model.ProductNewCode = NewCodeProduct(id);// mã Nội bộ
                            ProductSaleBO.Instance.Insert(model);
                        }
                            
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

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
