using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
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
    public partial class frmPOKHImportExcel_New : _Forms
    {
        DateTime start;
        DataSet ds = new DataSet();
        int warehouseID = 0;
        public frmPOKHImportExcel_New(int warehouseID)
        {
            InitializeComponent();

            this.warehouseID = warehouseID;
        }

        private void frmPOKHImportExcel_New_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
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
                cboSheet_SelectionChangeCommitted(null, null);
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }
        private bool SaveData()
        {
            //partList.GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
            //partList.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
            //using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát ...", ""))
            //{
            List<POKHModel> lstMaster = new List<POKHModel>();
            for (int i = 2; i < grvData.RowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));


                    string customerCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    DateTime? datePO = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F9"));
                    CustomerModel cusModel = SQLHelper<CustomerModel>.FindByAttribute("CustomerCode", customerCode).FirstOrDefault() ?? new CustomerModel();
                    if (cusModel.ID <= 0) continue;
                    if (!datePO.HasValue) continue;
                    Expression ex1 = new Expression("CustomerID", cusModel.ID);
                    Expression ex2 = new Expression("ReceivedDatePO", datePO.Value.ToString("yyyy-MM-dd"));
                    Expression expWarehouse = new Expression("WarehouseID", warehouseID);

                    //POKHModel pokhModel = SQLHelper<POKHModel>.FindByExpression(ex1.And(ex2).And(expWarehouse)).FirstOrDefault() ?? new POKHModel();
                    //POKHModel pokhModel = new POKHModel();
                    POKHModel pokhModel = lstMaster.Where(p => p.CustomerID == cusModel.ID
                                                            && p.ReceivedDatePO.Value.Date == datePO.Value.Date)
                                                    .FirstOrDefault() ?? new POKHModel();

                    string deliverStatus = TextUtils.ToString(grvData.GetRowCellValue(i, "F1")).Split('.')[0];
                    string paymentSatus = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Split('.')[0];
                    string isBill = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Split('.')[0];

                    string poNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();
                    if (!string.IsNullOrWhiteSpace(poNumber)) pokhModel.PONumber = poNumber;



                    pokhModel.DeliveryStatus = TextUtils.ToInt(deliverStatus);
                    pokhModel.PaymentStatus = TextUtils.ToInt(paymentSatus);
                    pokhModel.IsBill = TextUtils.ToBoolean(TextUtils.ToInt(isBill));
                    pokhModel.WarehouseID = warehouseID;
                    if (pokhModel.ID <= 0)
                    {
                        pokhModel.CustomerID = cusModel.ID;
                        pokhModel.ReceivedDatePO = datePO;
                        pokhModel.ID = SQLHelper<POKHModel>.Insert(pokhModel).ID;
                    }
                    if (!lstMaster.Contains(pokhModel)) lstMaster.Add(pokhModel);

                    string producSaleCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    string producNewCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();
                    Expression ex3 = new Expression("ProductCode", producSaleCode);
                    Expression ex4 = new Expression("ProductNewCode", producNewCode);
                    ProductSaleModel productSaleModel = SQLHelper<ProductSaleModel>.FindByExpression(ex3.And(ex4)).FirstOrDefault();
                    if (productSaleModel == null) continue;


                    string indexPO = TextUtils.ToString(grvData.GetRowCellValue(i, "F7")).Trim();
                    List<POKHDetailModel> lstDetails = SQLHelper<POKHDetailModel>.FindByAttribute("POKHID", pokhModel.ID);
                    POKHDetailModel pokhDetail = lstDetails.FirstOrDefault(p => p.ProductID == productSaleModel.ID && p.IndexPO == indexPO) ?? new POKHDetailModel();

                    pokhDetail.POKHID = pokhModel.ID;
                    pokhDetail.ProductID = productSaleModel.ID;
                    pokhDetail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, "F12"));
                    pokhDetail.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F17"));
                    pokhDetail.IntoMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F18"));
                    pokhDetail.IndexPO = indexPO;
                    pokhDetail.BillNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F27")).Trim();
                    pokhDetail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F19"));
                    pokhDetail.TotalPriceIncludeVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F20"));
                    pokhDetail.RecivedMoneyDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F25"));
                    pokhDetail.BillDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F26"));
                    pokhDetail.ActualDeliveryDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F23"));
                    pokhDetail.STT = pokhDetail.ID > 0 ? pokhDetail.STT : (lstDetails.Count > 0 ? lstDetails.Max(p => p.STT) + 1 : 1);
                    pokhDetail.GuestCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")).Trim();
                    pokhDetail.UserReceiver = Global.AppFullName;
                    //pokhDetail.KHID = ;
                    //pokhDetail.QtyRequest = ;
                    //pokhDetail.FilmSize = ;
                    //pokhDetail.EstimatedPay = ;
                    //pokhDetail.DeliveryRequestedDate = ;
                    //pokhDetail.PayDate = ;
                    //pokhDetail.NewRow = ;
                    //pokhDetail.IsOder = ;
                    //pokhDetail.QuotationDetailID = ;
                    //pokhDetail.GroupPO = ;
                    //pokhDetail.QtyTT = ;
                    //pokhDetail.QtyCL = ;
                    //pokhDetail.IsExport = ;
                    //pokhDetail.Debt = ;
                    //pokhDetail.NetUnitPrice = ;
                    //pokhDetail.Note = ;

                    if (pokhDetail.ID > 0) SQLHelper<POKHDetailModel>.Update(pokhDetail);
                    else SQLHelper<POKHDetailModel>.Insert(pokhDetail);

                    if (lstDetails.Count > 0)
                    {
                        pokhModel.TotalMoneyKoVAT = lstDetails.Where(p => p.ID != pokhDetail.ID).Sum(p => p.IntoMoney) + pokhDetail.IntoMoney;
                        pokhModel.TotalMoneyPO = lstDetails.Where(p => p.ID != pokhDetail.ID).Sum(p => p.TotalPriceIncludeVAT) + pokhDetail.TotalPriceIncludeVAT;
                        SQLHelper<POKHModel>.Update(pokhModel);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dữ liệu tại dòng {i + 1} : " + ex.Message);
                    continue;
                }

            }

            return true;
            //}
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
        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("MAU_POKH.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("MAU_POKH.xlsx");
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



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //SaveData();
            UpdatePOType();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                enableControl(true);
                return;
            };

            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.Close();
        }


        private void UpdatePOType()
        {
            int selectedIndex = 0;
            cboSheet.Invoke(new Action(() => { selectedIndex = cboSheet.SelectedIndex; }));
            bool isTrueSheet = selectedIndex == 0 || selectedIndex == 1;
            if (!isTrueSheet) return;

            for (int i = 2; i < grvData.RowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));


                    string customerCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                    DateTime? datePO = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F9"));
                    CustomerModel cusModel = SQLHelper<CustomerModel>.FindByAttribute("CustomerCode", customerCode).FirstOrDefault() ?? new CustomerModel();
                    if (cusModel.ID <= 0) continue;

                    string pokhNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();

                    Expression ex1 = new Expression("CustomerID", cusModel.ID);
                    Expression ex2 = new Expression("ReceivedDatePO", datePO.Value.ToString("yyyy-MM-dd"));
                    List<POKHModel> lstResult = SQLHelper<POKHModel>.FindByExpression(ex1.And(ex2));
                    string idText = string.Join(",", lstResult.Select(p => p.ID));
                    Dictionary<string, object> myDict = new Dictionary<string, object>()
                    {
                        {"POType", selectedIndex == 0 ? 8 : 10 },
                    };

                    //Update PONumber
                    Dictionary<string, object> myDict1 = new Dictionary<string, object>()
                    {
                        {"PONumber", pokhNumber},
                        {"POCode", ""},
                        {"UpdatedDate", DateTime.Now }
                    };
                    SQLHelper<POKHModel>.UpdateFields(myDict1, new Expression("ID", idText, "IN"));


                    //Update POType < 0
                    foreach (POKHModel item in lstResult)
                    {
                        if (item.POType <= 0) SQLHelper<POKHModel>.UpdateFieldsByID(myDict, item.ID);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dữ liệu tại dòng {i + 1} : " + ex.Message);
                    continue;
                }

            }




        }
    }
}
