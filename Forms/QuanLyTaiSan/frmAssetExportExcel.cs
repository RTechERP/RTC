using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.RichEdit.Core.Accessibility;
using DevExpress.Utils;
using DocumentFormat.OpenXml.Drawing;

//using DevExpress.Utils;
using ExcelDataReader;
using IronXL;
using System;
using System.Collections;
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
    public partial class frmAssetExportExcel : _Forms
    {
        string a;
        public frmAssetExportExcel()
        {
            InitializeComponent();
        }

        private void frmAssetExportExcel_Load(object sender, EventArgs e)
        {

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

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            //UpdateTSAsset();
            bool result = UpdateTSAssetNew();
            e.Result = result;

            //UpdateTSAssetNew();

            return;
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            for (int i = 0; i < rowCount; i++)
            {
                //if (i < 1916) continue;
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));

                if (stt <= 0) continue;


                string TSCodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                if (string.IsNullOrEmpty(TSCodeNCC)) continue;
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

                    TSAssetManagementModel model = new TSAssetManagementModel();
                    TSAllocationEvictionAssetModel allocation = new TSAllocationEvictionAssetModel();

                    //ArrayList arrModel = TSAssetManagementBO.Instance.FindByAttribute("TSCodeNCC", TSCodeNCC);
                    List<TSAssetManagementModel> lstModel = SQLHelper<TSAssetManagementModel>.SqlToList($"SELECT * FROM TSAssetManagement WHERE TSCodeNCC = '{TSCodeNCC}'");
                    model.TSCodeNCC = TSCodeNCC;
                    decimal amount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));

                    for (int j = 0; j < amount; j++)
                    {


                        if (lstModel != null)
                        {
                            if (lstModel.Count() == amount)
                            {
                                model = (TSAssetManagementModel)lstModel[j];
                            }
                            else
                            {
                                model = new TSAssetManagementModel();
                            }

                            //if (lstModel.Count > 0)
                            //{
                            //    model = (TSAssetManagementModel)lstModel[j];
                            //}
                            //model = new TSAssetManagementModel();
                        }
                        else
                        {
                            model = new TSAssetManagementModel();
                        }

                        string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F11"));
                        string department = TextUtils.ToString(grvData.GetRowCellValue(i, "F12"));
                        string employee = TextUtils.ToString(grvData.GetRowCellValue(i, "F13"));
                        string asset = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                        string source = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                        string supplier = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                        string units = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));

                        DataTable dt = TextUtils.GetDataTableFromSP("spGetID",
                            new string[] { "@Status", "@Code", "@CodeEmployee", "@AssetCode", "@SourceCode", "@SupplierCode", "@UnitName" },
                            new object[] { status, department, employee, asset, source, supplier, units });

                        //Lấy status ID
                        if (dt.Rows.Count == 0) continue;

                        model.StatusID = TextUtils.ToInt(dt.Rows[0]["TSStatusAssetID"]);

                        // Lấy phòng ban ID

                        model.DepartmentID = TextUtils.ToInt(dt.Rows[0]["DepartmentID"]);

                        //Lấy ID nhân viên

                        model.EmployeeID = TextUtils.ToInt(dt.Rows[0]["EmployID"]);

                        //Lấy ID chức vụ



                        //Lấy ID tài sản

                        model.TSAssetID = TextUtils.ToInt(dt.Rows[0]["TSAssetID"]);

                        model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                        model.TSCodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        model.TSAssetCode = $"TS{DateTime.Now.ToString("yyyyMMdd")}" + i;
                        model.TSAssetName = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));

                        //Lấy ID nguồn gốc tài sản

                        model.SourceID = TextUtils.ToInt(dt.Rows[0]["TSSourceAssetID"]);

                        model.Seri = TextUtils.ToString(grvData.GetRowCellValue(i, "F8"));
                        model.SpecificationsAsset = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));

                        //Lấy ID nhà cung cấp

                        model.SupplierID = TextUtils.ToInt(dt.Rows[0]["SupplierID"]);

                        //Lấy ID đơn vị tính

                        model.UnitID = TextUtils.ToInt(dt.Rows[0]["UnitCountID"]);

                        model.DateBuy = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F15"));
                        model.Insurance = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16"));
                        //model.DateEffect = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F17"));
                        model.DateEffect = model.DateBuy;
                        model.Status = TextUtils.ToString(grvData.GetRowCellValue(i, "F11"));
                        model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F18"));

                        if (lstModel != null)
                        {
                            if (lstModel.Count >= amount)
                            {
                                //TSAssetManagementBO.Instance.Update(model);
                                SQLHelper<TSAssetManagementModel>.Update(model);
                            }
                            else
                            {
                                model.TSAssetCode = $"TS{DateTime.Now.ToString("yyyyMMdd")}" + i + j;
                                //model.ID = (int)TSAssetManagementBO.Instance.Insert(model);
                                model.ID = SQLHelper<TSAssetManagementModel>.Insert(model).ID;
                            }
                        }
                        else
                        {
                            model.TSAssetCode = $"TS{DateTime.Now.ToString("yyyyMMdd")}" + i + j;
                            //model.ID = (int)TSAssetManagementBO.Instance.Insert(model);
                            model.ID = SQLHelper<TSAssetManagementModel>.Insert(model).ID;
                        }

                        if (model.ID <= 0) continue;

                        //var temp = TSAllocationEvictionAssetBO.Instance.FindByAttribute("AssetManagementID", model.ID); //check đã có chi tiết tài sản chưa
                        var temp = SQLHelper<TSAllocationEvictionAssetModel>.FindByAttribute("AssetManagementID", model.ID); //check đã có chi tiết tài sản chưa

                        if (temp.Count > 0)
                        {
                            //allocation = (TSAllocationEvictionAssetModel)temp[0];
                            allocation = temp.FirstOrDefault() ?? new TSAllocationEvictionAssetModel();
                        }

                        allocation.AssetManagementID = model.ID;
                        allocation.EmployeeID = TextUtils.ToInt(model.EmployeeID);
                        allocation.DepartmentID = TextUtils.ToInt(model.DepartmentID);
                        allocation.DateAllocation = model.DateEffect;
                        allocation.Note = "Cấp phát thiết bị mới cho nhân viên ";
                        allocation.Status = "Đang sử dụng";
                        allocation.ChucVuID = TextUtils.ToInt(dt.Rows[0]["ChucVuID"]);


                        if (allocation.ID <= 0)
                        {
                            //TSAllocationEvictionAssetBO.Instance.Insert(allocation);
                            SQLHelper<TSAllocationEvictionAssetModel>.Insert(allocation);
                        }
                        else
                        {

                            //TSAllocationEvictionAssetBO.Instance.Update(allocation);
                            SQLHelper<TSAllocationEvictionAssetModel>.Update(allocation);
                        }

                        AutoInsertProductSale(model);
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
            //MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.DialogResult = DialogResult.OK;
            //enableControl(true);

            //ndNhat update 10/07/2025
            if (e.Result is bool isSuccess && isSuccess)
            {
                MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount;
                progressBar1.Value = 1;
                txtRate.Text = "";
            }
            enableControl(true);
        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }

        void AutoInsertProductSale(TSAssetManagementModel tSAsset)
        {
            var productgroupModel = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupID", "C").FirstOrDefault();
            int groupID = 0;
            if (productgroupModel != null)
            {
                groupID = productgroupModel.ID;
            }

            //string productCode = tSAsset.TSCodeNCC;
            string productCode = tSAsset.Model;
            var exp1 = new Expression("ProductGroupID", groupID);
            var exp2 = new Expression("ProductCode", productCode);
            ProductSaleModel product = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            product = product == null ? new ProductSaleModel() : product;
            product.ProductGroupID = groupID;
            product.ProductCode = productCode;
            product.ProductName = tSAsset.TSAssetName;
            SupplierModel supplier = SQLHelper<SupplierModel>.FindByID(TextUtils.ToInt(tSAsset.SupplierID));
            product.SupplierName = supplier == null ? "" : supplier.SupplierName;
            UnitCountModel unit = SQLHelper<UnitCountModel>.FindByID(TextUtils.ToInt(tSAsset.UnitID));
            product.Unit = unit == null ? "" : unit.UnitName;

            if (product.ID > 0)
            {
                SQLHelper<ProductSaleModel>.Update(product);
            }
            else
            {
                product.ProductNewCode = loadNewCode(groupID);
                product.ID = SQLHelper<ProductSaleModel>.Insert(product).ID;

                //productSale.ID = (int)ProductSaleBO.Instance.Insert(productSale);
                InventoryModel inventoryModel = new InventoryModel();
                inventoryModel.WarehouseID = 1;
                inventoryModel.ProductSaleID = product.ID;
                InventoryBO.Instance.Insert(inventoryModel);
            }
        }

        string loadNewCode(int groupID)
        {
            string _NewCodeRTC;

            DataSet ds = TextUtils.LoadDataSetFromSP("spLoadNewCodeRTC", new string[] { "@Group" }, new object[] { groupID });
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
                    for (int i = 0; i < (9 - stt.ToString().Length); i++)
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



        void UpdateTSAsset()
        {
            int stt = 0;
            try
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt <= 0) continue;

                    int quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, "F13"));
                    string tsCodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    List<TSAssetManagementModel> listTSAssets = SQLHelper<TSAssetManagementModel>.FindByAttribute("TSCodeNCC", tsCodeNCC);
                    for (int j = 0; j < quantity; j++)
                    {
                        TSAssetManagementModel ts = listTSAssets.ElementAtOrDefault(j) ?? new TSAssetManagementModel();

                        ts.STT = stt;
                        ts.TSCodeNCC = tsCodeNCC;
                        ts.TSAssetName = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));

                        //Loại tài sản
                        string assetTypeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                        TSAssetModel tSAsset = SQLHelper<TSAssetModel>.FindByAttribute("AssetCode", assetTypeCode).FirstOrDefault() ?? new TSAssetModel();
                        ts.TSAssetID = tSAsset.ID;

                        //Nguồn gốc
                        string assetSourceCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                        TSSourceAssetModel tSSource = SQLHelper<TSSourceAssetModel>.FindByAttribute("SourceCode", assetSourceCode).FirstOrDefault() ?? new TSSourceAssetModel();
                        ts.SourceID = tSSource.ID;

                        //Nhà cung cấp
                        string supplierCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();
                        SupplierModel supplier = SQLHelper<SupplierModel>.FindByAttribute("SupplierCode", supplierCode).FirstOrDefault() ?? new SupplierModel();
                        ts.SupplierID = supplier.ID;

                        ts.SpecificationsAsset = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")).Trim();
                        ts.Seri = TextUtils.ToString(grvData.GetRowCellValue(i, "F11")).Trim();

                        //Đơn vị
                        string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F12")).Trim();
                        UnitCountModel unitCount = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", unit).FirstOrDefault() ?? new UnitCountModel();
                        ts.UnitID = unitCount.ID;


                        //Tình trạng
                        string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F14")).Trim(); ;
                        TSStatusAssetModel tSStatus = SQLHelper<TSStatusAssetModel>.FindByAttribute("Status", status).FirstOrDefault() ?? new TSStatusAssetModel();
                        ts.StatusID = tSStatus.ID;
                        ts.Status = status;

                        //Phòng ban
                        string departmentCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F15")).Trim(); ;
                        DepartmentModel department = SQLHelper<DepartmentModel>.FindByAttribute("Code", departmentCode).FirstOrDefault() ?? new DepartmentModel();
                        ts.DepartmentID = department.ID;

                        //Nhân viên
                        string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F17")).Trim(); ;
                        EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", employeeCode).FirstOrDefault() ?? new EmployeeModel();
                        ts.EmployeeID = employee.ID;

                        ts.DateBuy = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F19"));
                        ts.DateEffect = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F19"));
                        ts.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F22"));

                        // Lấy trạng thái active VT.Name update 09/12/2024
                        int officeActiveStatus = TextUtils.ToInt(TextUtils.ToString(grvData.GetRowCellValue(i, "F23")).Split('.')[0]);
                        int windowActiveStatus = TextUtils.ToInt(TextUtils.ToString(grvData.GetRowCellValue(i, "F24")).Split('.')[0]);
                        ts.WindowActiveStatus = windowActiveStatus;
                        ts.OfficeActiveStatus = officeActiveStatus;

                        if (ts.ID <= 0)
                        {
                            ts.TSAssetCode = $"TS{DateTime.Now.ToString("yyyyMMdd")}" + i + j;
                            SQLHelper<TSAssetManagementModel>.Insert(ts);
                        }
                        else
                        {
                            SQLHelper<TSAssetManagementModel>.Update(ts);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu (Stt: {stt})\r\n" + ex.ToString(), "Thông báo");
            }
        }



        //ndNhat update 10/07/2025
        bool UpdateTSAssetNew()
        {
            int stt = 0;
            try
            {
                if (!ValidateTSAsset()) return false;


                for (int i = 0; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt <= 0) continue;
                    //Số lượng
                    int quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, "F15"));

                    string tsCodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    List<TSAssetManagementModel> listTSAssets = SQLHelper<TSAssetManagementModel>.FindByAttribute("TSCodeNCC", tsCodeNCC);

                    for (int j = 0; j < quantity; j++)
                    {
                        TSAssetManagementModel ts = listTSAssets.ElementAtOrDefault(j) ?? new TSAssetManagementModel();
                        //STT
                        ts.STT = stt;
                        //Mã tài sản
                        ts.TSCodeNCC = tsCodeNCC;
                        //Tên tài sản
                        ts.TSAssetName = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                        //Mã loại tài sản
                        string assetTypeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                        TSAssetModel tSAsset = SQLHelper<TSAssetModel>.FindByAttribute("AssetCode", assetTypeCode).FirstOrDefault() ?? new TSAssetModel();
                        ts.TSAssetID = tSAsset.ID;
                        //Mã nguồn gốc
                        string assetSourceCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();
                        TSSourceAssetModel tSSource = SQLHelper<TSSourceAssetModel>.FindByAttribute("SourceCode", assetSourceCode).FirstOrDefault() ?? new TSSourceAssetModel();
                        ts.SourceID = tSSource.ID;
                        //Mã nhà cung cấp
                        string supplierCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F10")).Trim();
                        SupplierModel supplier = SQLHelper<SupplierModel>.FindByAttribute("SupplierCode", supplierCode).FirstOrDefault() ?? new SupplierModel();
                        ts.SupplierID = supplier.ID;
                        //Mô tả chi tiết
                        ts.SpecificationsAsset = TextUtils.ToString(grvData.GetRowCellValue(i, "F12")).Trim();
                        //Số Se-ri
                        ts.Seri = TextUtils.ToString(grvData.GetRowCellValue(i, "F13")).Trim();
                        //Đơn vị
                        string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F14")).Trim();
                        UnitCountModel unitCount = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", unit).FirstOrDefault() ?? new UnitCountModel();
                        ts.UnitID = unitCount.ID;
                        //Tình trạng
                        string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F16")).Trim();

                        TSStatusAssetModel tSStatus = SQLHelper<TSStatusAssetModel>.FindByAttribute("Status", status).FirstOrDefault() ?? new TSStatusAssetModel();

                        //if (ts.ID > 0 && ts.StatusID != tSStatus.ID)
                        //{
                        //    //MessageBox.Show($"Tài sản {ts.TSCodeNCC} tại STT {stt} đã có trạng thái sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    this.Invoke((MethodInvoker)delegate
                        //    {
                        //        MessageBox.Show(this, $"Tài sản {ts.TSCodeNCC} tại STT {stt} đã có trạng thái sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    });

                        //    return false;
                        //}
                        ts.StatusID = tSStatus.ID;
                        ts.Status = tSStatus.Status;
                        //Mã phòng ban
                        string departmentCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F17")).Trim();
                        DepartmentModel department = SQLHelper<DepartmentModel>.FindByAttribute("Code", departmentCode).FirstOrDefault() ?? new DepartmentModel();
                        ts.DepartmentID = department.ID;
                        //Mã nhân viên sử dụng
                        string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F19")).Trim();
                        EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", employeeCode).FirstOrDefault() ?? new EmployeeModel();
                        ts.EmployeeID = employee.ID;
                        //Thời gian ghi tăng
                        ts.DateBuy = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F21"));
                        //Thời gian hiệu lực
                        ts.DateEffect = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F23"));
                        //Ghi chú
                        ts.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F24"));

                        ts.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F25"));

                        // Trạng thái ACTIVE OFFICE và WINDOWS
                        string officeStatusText = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                        string windowStatusText = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                        ts.OfficeActiveStatus = MapStringToInt(officeStatusText);
                        ts.WindowActiveStatus = MapStringToInt(windowStatusText);

                        if (ts.ID <= 0)
                        {
                            ts.TSAssetCode = $"TS{DateTime.Now:yyyyMMdd}{i}{j}";
                            SQLHelper<TSAssetManagementModel>.Insert(ts);
                        }
                        else
                        {
                            SQLHelper<TSAssetManagementModel>.Update(ts);
                        }

                        AutoInsertProductSale(ts, supplier, unitCount);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu (Stt: {stt})\r\n" + ex.ToString(), "Thông báo");
                return false;
            }
        }

        void AutoInsertProductSale(TSAssetManagementModel tSAsset, SupplierModel supplier, UnitCountModel unitCount)
        {
            var productgroupModel = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupID", "C").FirstOrDefault();
            int groupID = 0;
            if (productgroupModel != null)
            {
                groupID = productgroupModel.ID;
            }
            
            string productCode = tSAsset.Model;//NDNHAT update 21/10/2025
            var exp1 = new Expression("ProductGroupID", groupID);
            var exp2 = new Expression("ProductCode", productCode);
            ProductSaleModel product = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            product = product == null ? new ProductSaleModel() : product;
            product.ProductGroupID = groupID;

            product.ProductCode = tSAsset.Model;
            product.ProductName = tSAsset.TSAssetName;
            product.SupplierName = supplier.SupplierName;
            product.Unit = unitCount.UnitName;

            if (product.ID > 0)
            {
                SQLHelper<ProductSaleModel>.Update(product);
            }
            else
            {
                product.ProductNewCode = loadNewCode(groupID);
                product.ID = SQLHelper<ProductSaleModel>.Insert(product).ID;

                //productSale.ID = (int)ProductSaleBO.Instance.Insert(productSale);
                InventoryModel inventoryModel = new InventoryModel();
                inventoryModel.WarehouseID = 1;
                inventoryModel.ProductSaleID = product.ID;
                InventoryBO.Instance.Insert(inventoryModel);
            }
        }

        bool ValidateTSAsset()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                if (stt <= 0) continue;

                int quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, "F15"));
                string tsCodeNCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                List<TSAssetManagementModel> listTSAssets = SQLHelper<TSAssetManagementModel>.FindByAttribute("TSCodeNCC", tsCodeNCC);

                for (int j = 0; j < quantity; j++)
                {
                    TSAssetManagementModel ts = listTSAssets.ElementAtOrDefault(j) ?? new TSAssetManagementModel();
                    ts.STT = stt;
                    ts.TSCodeNCC = tsCodeNCC;
                    string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F16")).Trim();
                    TSStatusAssetModel tSStatus = SQLHelper<TSStatusAssetModel>.FindByAttribute("Status", status).FirstOrDefault() ?? new TSStatusAssetModel();

                    if (ts.ID <= 0 && !string.Equals(tSStatus.Status, "Chưa sử dụng", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(this, $"Tài sản {ts.TSCodeNCC} tại STT {stt} là tài sản mới và phải có trạng thái 'Chưa sử dụng'!", "Thông báo");
                        });
                        return false;
                    }

                    if (ts.ID > 0 && ts.StatusID != tSStatus.ID)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(this, $"Tài sản {ts.TSCodeNCC} tại STT {stt} đã có trạng thái ban đầu là: \"{ts.Status}\"", "Thông báo");
                        });
                        return false;
                    }
                }
            }

            return true;
        }


        int MapStringToInt(string statusText)
        {
            if (string.IsNullOrWhiteSpace(statusText)) return 0;

            string[] parts = statusText.Split('-');
            return int.TryParse(parts[0].Trim(), out int result) ? result : 0;
        }
        //end ndNhat update 10/07/2025  

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            return;
            try
            {
                Process.Start(System.IO.Path.Combine(Application.StartupPath, "KiemKeTaiSanTemplateImport.xlsx"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
    }
}
