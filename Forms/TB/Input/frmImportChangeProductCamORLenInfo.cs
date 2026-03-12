
using BMS.Model;
using BMS.Utils;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmImportChangeProductCamORLenInfo : _Forms
    {
        public string code;
        public frmImportChangeProductCamORLenInfo()
        {
            InitializeComponent();
            cbType.SelectedIndex = 0;
        }
        DateTime start;
        DataSet ds;
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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
                cboSheet_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //else
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        btnBrowse.Text = ofd.FileName;
            //        cboSheet.DataSource = null;
            //        cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
            //        btnSave.Enabled = true;
            //    }
            //}
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

        private void btnSave_Click(object sender, EventArgs e)
        {
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

        private void frmImportBox_Load(object sender, EventArgs e)
        {
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
        int TypeProduct;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            UpdateProduct();
            return;


            txtRate.Text = "";

            this.Invoke((MethodInvoker)delegate
            {
                //Type = cbType.Text.Trim();
                TypeProduct = cbType.SelectedIndex + 1;

                //if (cbType.SelectedIndex == 0) TypeProduct = 1;
                //else if (cbType.SelectedIndex == 1) TypeProduct = 2;
                //else if(cbType.SelectedIndex == 2) TypeProduct = 3;
                //else if(cbType.SelectedIndex == 3) TypeProduct = 4;
                //else if(cbType.SelectedIndex == 5) TypeProduct = 4;
            });
            int rowCount = grvData.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                try
                {
                    if (i < 1) continue;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));


                    string col2 = grvData.GetRowCellValue(i, "F2").ToString().Trim();
                    string col3 = grvData.GetRowCellValue(i, "F3").ToString().Trim();
                    string col4 = grvData.GetRowCellValue(i, "F4").ToString().Trim();
                    string col5 = grvData.GetRowCellValue(i, "F5").ToString().Trim();
                    string col6 = grvData.GetRowCellValue(i, "F6").ToString().Trim();
                    string col7 = grvData.GetRowCellValue(i, "F7").ToString().Trim();
                    string col8 = grvData.GetRowCellValue(i, "F8").ToString().Trim();
                    string ProductCodeRTC = "";
                    if (cbType.InvokeRequired)
                        cbType.Invoke((MethodInvoker)delegate
                        {
                            if (cbType.SelectedIndex == 0)
                            {
                                ProductCodeRTC = grvData.GetRowCellValue(i, "F19").ToString().Trim();
                            }
                            else
                            {
                                ProductCodeRTC = grvData.GetRowCellValue(i, "F18").ToString().Trim();
                            }
                        }
                        );
                    else
                    {
                        if (cbType.SelectedIndex == 0)
                        {
                            ProductCodeRTC = grvData.GetRowCellValue(i, "F19").ToString().Trim();
                        }
                        else
                        {
                            ProductCodeRTC = grvData.GetRowCellValue(i, "F18").ToString().Trim();
                        }
                    }

                    TextUtils.ExcuteProcedure("spUpdateInfoCamOrLen",
                        new string[] { "@TypeUpdate", "@Col2", "@Col3", "@Col4", "@Col5", "@Col6", "@Col7", "col8", "@ProductCodeRTC" },
                        new object[] { TypeProduct, col2, col3, col4, col5, col6, col7, col8, ProductCodeRTC });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
                }

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("SUCCESS!");
            enableControl(true);
            this.Close();
        }

        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }


        void UpdateProductLensFixFocal()
        {
            int i = 0;
            try
            {
                for (i = 1; i < grvData.RowCount; i++)
                {
                    string productCodeRTC = TextUtils.ToString(grvData.GetRowCellValue(i, "F24")).Trim();
                    if (string.IsNullOrWhiteSpace(productCodeRTC)) continue;

                    ProductRTCModel product = SQLHelper<ProductRTCModel>.FindByAttribute("ProductCodeRTC", productCodeRTC).FirstOrDefault();
                    if (product == null) continue;

                    product.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                    product.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    product.FocalLength = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    product.Resolution = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    product.SensorSizeMax = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
                    product.MOD = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    product.LensMount = TextUtils.ToString(grvData.GetRowCellValue(i, "F7")).Trim();
                    product.FNo = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();

                    string locationName = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();
                    ProductLocationModel location = SQLHelper<ProductLocationModel>.FindByAttribute("LocationName", locationName).FirstOrDefault();
                    location = location ?? new ProductLocationModel();
                    product.ProductLocationID = location.ID;
                    product.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")).Trim();

                    string unitName = TextUtils.ToString(grvData.GetRowCellValue(i, "F14")).Trim();
                    UnitCountKTModel unitCount = SQLHelper<UnitCountKTModel>.FindByAttribute("UnitCountName", unitName).FirstOrDefault();
                    unitCount = unitCount ?? new UnitCountKTModel();
                    product.UnitCountID = unitCount.ID;

                    string groupName = TextUtils.ToString(grvData.GetRowCellValue(i, "F23")).Trim();
                    var exp1 = new Expression("ProductGroupName", groupName);
                    var exp2 = new Expression("WarehouseID", 1);
                    ProductGroupRTCModel productGroup = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                    productGroup = productGroup ?? new ProductGroupRTCModel();
                    product.ProductGroupRTCID = productGroup.ID;
                    product.ProductCodeRTC = productCodeRTC;
                    product.BorrowCustomer = !string.IsNullOrWhiteSpace(TextUtils.ToString(grvData.GetRowCellValue(i, "F26")));
                    product.PartNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F27"));
                    product.SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F28"));
                    product.Serial = TextUtils.ToString(grvData.GetRowCellValue(i, "F29"));

                    if (product.ID > 0)
                    {
                        SQLHelper<ProductRTCModel>.Update(product);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message} (dòng {i}\n{ex.ToString()}", "Thông báo");
            }
        }
        void UpdateProductLensTele()
        {
            int i = 0;
            try
            {
                for (i = 1; i < grvData.RowCount; i++)
                {
                    string productCodeRTC = TextUtils.ToString(grvData.GetRowCellValue(i, "F24")).Trim();
                    if (string.IsNullOrWhiteSpace(productCodeRTC)) continue;

                    ProductRTCModel product = SQLHelper<ProductRTCModel>.FindByAttribute("ProductCodeRTC", productCodeRTC).FirstOrDefault();
                    if (product == null) continue;

                    product.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                    product.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    product.Magnification = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    product.Resolution = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    product.SensorSizeMax = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
                    product.WD = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    product.FNo = TextUtils.ToString(grvData.GetRowCellValue(i, "F7")).Trim();
                    product.LensMount = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();

                    string locationName = TextUtils.ToString(grvData.GetRowCellValue(i, "F9")).Trim();
                    ProductLocationModel location = SQLHelper<ProductLocationModel>.FindByAttribute("LocationName", locationName).FirstOrDefault();
                    location = location ?? new ProductLocationModel();
                    product.ProductLocationID = location.ID;
                    product.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")).Trim();

                    string unitName = TextUtils.ToString(grvData.GetRowCellValue(i, "F14")).Trim();
                    UnitCountKTModel unitCount = SQLHelper<UnitCountKTModel>.FindByAttribute("UnitCountName", unitName).FirstOrDefault();
                    unitCount = unitCount ?? new UnitCountKTModel();
                    product.UnitCountID = unitCount.ID;

                    string groupName = TextUtils.ToString(grvData.GetRowCellValue(i, "F23")).Trim();
                    var exp1 = new Expression("ProductGroupName", groupName);
                    var exp2 = new Expression("WarehouseID", 1);
                    ProductGroupRTCModel productGroup = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                    productGroup = productGroup ?? new ProductGroupRTCModel();
                    product.ProductGroupRTCID = productGroup.ID;
                    product.ProductCodeRTC = productCodeRTC;
                    product.BorrowCustomer = !string.IsNullOrWhiteSpace(TextUtils.ToString(grvData.GetRowCellValue(i, "F26")));
                    product.PartNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F27"));
                    product.SerialNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F28"));
                    product.Serial = TextUtils.ToString(grvData.GetRowCellValue(i, "F29"));

                    if (product.ID > 0)
                    {
                        SQLHelper<ProductRTCModel>.Update(product);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message} (dòng {i}\n{ex.ToString()}", "Thông báo");
            }
        }


        void UpdateProduct()
        {
            int type = -1;
            cbType.Invoke((Action)(() => {type = cbType.SelectedIndex; }));
            if (type == 2)
            {
                UpdateProductLensFixFocal();
            }
            else if (type == 3)
            {

                UpdateProductLensTele();
            }
        }
    }
}
