using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
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

namespace Forms.DanhMuc
{
    public partial class frmInventoryStockImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        public int wareHouseID = 0;
        public frmInventoryStockImportExcel()
        {
            InitializeComponent();
        }
        private void frmInventoryStockImportExcel_Load(object sender, EventArgs e)
        {
        }

        bool validate()
        {
            string code = TextUtils.ToString(grvData.GetRowCellValue(1, "F3")).Trim();
            return true;
            /*string projectCode = TextUtils.ToString(varImport.GetType().GetProperty("projectCode").GetValue(varImport)).Trim();
            if (!code.Equals(projectCode))
            {
                MessageBox.Show("Không đúng Mã dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;*/
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
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdatePartlist();
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
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

        void UpdatePartlist()
        {
            string _str = "";
            // regex kiểm tra số
            Regex regex = new Regex(@"^-?[\d\.]+$");
            for (int i = 7; i < grvData.RowCount; i++)
            {
                progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Trim();
                if (string.IsNullOrEmpty(stt)) continue;
                if (!regex.IsMatch(stt)) continue;

                string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                decimal minQuantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));

                var exp1 = new Expression("ProductCode", code);
                var exp2 = new Expression("WarehouseID", wareHouseID);
                //ProductSaleModel modelCheck = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp1).FirstOrDefault();
                if (productSale == null) continue;

                var exp3 = new Expression("ProductSaleID", productSale.ID);
                InventoryModel inventory = SQLHelper<InventoryModel>.FindByExpression(exp2.And(exp3)).FirstOrDefault();
                if (inventory == null) continue;
                inventory.IsStock = minQuantity > 0;
                inventory.MinQuantity = minQuantity;
                SQLHelper<InventoryModel>.Update(inventory);

                //InventoryStockModel inventoryStock = SQLHelper<InventoryStockModel>.FindByAttribute("InventoryID", inventory.ID).FirstOrDefault();
                //inventoryStock = inventoryStock ?? new InventoryStockModel();
                //inventoryStock.InventoryID = inventory.ID;
                //inventoryStock.Quantity = minQuantity;
                //if (inventoryStock.ID <= 0)
                //{
                //    SQLHelper<InventoryStockModel>.Insert(inventoryStock);
                //}
                //else
                //{
                //    SQLHelper<InventoryStockModel>.Update(inventoryStock);
                //}




                //int isStock = minQuantity > 0 ? 1 : 0;
                //_str += $"UPDATE dbo.Inventory Set IsStock = 1, MinQuantity = {minQuantity} WHERE ProductSaleID = {modelCheck.ID} AND WarehouseID = {wareHouseID}";
            }
            //if (!string.IsNullOrEmpty(_str))
            //{
            //    SQLHelper<InventoryModel>.ExcuteNonQuerySQL(_str);
            //}

        }

        private void frmInventoryStockImportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmInventoryStockImportExcel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnBrowse_Properties_Click(object sender, EventArgs e)
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

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("Nhap_Kho_Stock.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("Nhap_Kho_Stock.xlsx");
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
    }
}
