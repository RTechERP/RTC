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
using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Forms.Technical;

namespace BMS
{
    public partial class frmCheckHistoryTech : _Forms
    {
        #region khai báo các biến khi mở form sửa
        DataTable dtLoad = new DataTable();
        public int wareHouseId;
        int supplierID, deliverID, customerID;
        string BillCode, nameNCC, receiver;
        bool checkLoadData = true;
        #endregion

        List<DataRow> listRows = new List<DataRow>();

        public frmCheckHistoryTech()
        {
            InitializeComponent();
        }

        private void frmCheckHistoryTech_Load(object sender, EventArgs e)
        {
            this.Text += wareHouseId == 1 ? " - HN" : (wareHouseId == 2 ? " - HCM" : " - BN");
            dtpDS.Value = dtpDS.Value.AddMonths(-1);
            LoadSupplierSale();
            LoadEmployee();
            LoadEmployeeBorrow();
            LoadData();
        }

        private void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59);

            string filterText = txtKeyword.Text.Trim();
            //if (cboSupplier.Text == "" || cboSupplier.Text == null) cboSupplier.EditValue = -1;
            int supplierId = TextUtils.ToInt(cboSupplier.EditValue);
            //if (cboEmployee.Text == "" || cboEmployee.Text == null) cboEmployee.EditValue = -1;
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            //if (cboEmployyBorrow.Text == "" || cboEmployyBorrow.Text == null) cboEmployyBorrow.EditValue = -1;
            int employeeBorrowId = TextUtils.ToInt(cboEmployyBorrow.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                dtLoad = TextUtils.LoadDataFromSP("spGetBillImportTechHistory", "A", // proc tao them
                                                    new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@EmployeeBorrowID", "@SupplierID", "@FilterText", "@WarehouseID" },
                                                    new object[] { dateStart, dateEnd, employeeId, employeeBorrowId, supplierId, filterText, wareHouseId });
                dtLoad.PrimaryKey = new DataColumn[] { dtLoad.Columns["BillImportDetailTechnicalID"] };
                foreach (var row in listRows)
                {
                    int id = TextUtils.ToInt(row["BillImportDetailTechnicalID"]);
                    DataRow row1 = dtLoad.Rows.Find(id);
                    int index = dtLoad.Rows.IndexOf(row1);
                    if (index < 0) continue;
                    dtLoad.Rows[index]["IsSelected"] = true;
                }

                grdData.DataSource = dtLoad;
            }

        }

        void LoadEmployeeBorrow()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] {0 }); // proc cap nhat
            cboEmployyBorrow.Properties.ValueMember = "ID";
            cboEmployyBorrow.Properties.DisplayMember = "FullName";
            cboEmployyBorrow.Properties.DataSource = dt;
        }

        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "A", new string[] { "@UsersID" }, new object[] { 0 });
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = dt;
        }
        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().ToList();
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.DataSource = list;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = rowHandle;
        }
        private void dtpDS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void dtpDE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvBillImportTech_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            int checkDealine = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colcheckDealine));
            decimal ReMain = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colReMain));

            bool isSelected = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsSelected));
            if (checkDealine <= 30 && ReMain != 0)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }

            if (isSelected)
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.HighPriority = true;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DSSanPhamMuonNCC_{dtpDS.Value.ToString("ddMMyy")}_{dtpDE.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DSSanPhamMuonNCC_{dtpDS.Value.ToString("ddMMyy")}_{dtpDE.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnShowDetail_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            string ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCodeRTC));
            //string NumberDauKy = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colNumber));
            //string NumberCuoiKy = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colInventoryLate));
            //string Import = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colImport));
            //string Export = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colExport));
            //string Borrowing = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBorrowing));
            //string NumberReal = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colInventoryReal));
            ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            frmMaterialDetailOfProductRTC frm = new frmMaterialDetailOfProductRTC(wareHouseId);
            frm.ProductRTCID = model.ID;
            frm.ProductName = ProductName;
            frm.ProductCode = ProductCode;
            //frm.NumberDauKy = NumberDauKy;
            //frm.NumberCuoiKy = NumberCuoiKy;
            //frm.NumberReal = NumberReal;
            //frm.Borrowing = Borrowing;
            //frm.Import = Import;
            //frm.Export = Export;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //}
            frm.Show();
        }



        private void chkIsSelected_CheckedChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            bool isSelected = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue("IsSelected"));

            DataRow dataRow = grvData.GetDataRow(grvData.FocusedRowHandle);
            bool isExist = listRows.Contains(dataRow);
            if (isSelected && !isExist)
            {
                listRows.Add(dataRow);
            }
            else if (!isSelected)
            {
                listRows.Remove(dataRow);
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnEditRowChecked_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            List<string> listSupplierSales = new List<string>();
            List<int> listCustomers = new List<int>();
            DataTable dt = dtLoad.Clone();

            dt.Clear();

            frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New();

            //int[] selectedRows = grvData.GetSelectedRows();
            //foreach (DataRow row in listRows)
            //{
            //    nameNCC = TextUtils.ToString(row["Suplier"]);// grvData.GetRowCellValue(row, colSuplier));
            //    if (!listSupplierSales.Contains(nameNCC)) listSupplierSales.Add(nameNCC);
            //    customerID = TextUtils.ToInt(row["Suplier"]); //grvData.GetRowCellValue(row, colCustomerID));
            //    if (!listCustomers.Contains(customerID)) listCustomers.Add(customerID);

            //    supplierID = TextUtils.ToInt(row["suplierID"]); //grvData.GetRowCellValue(row, colsuplierID));
            //    deliverID = TextUtils.ToInt(row["DeliverID"]); //grvData.GetRowCellValue(row, colDeliverID));
            //    BillCode = TextUtils.ToString(row["BillCode"]); //grvData.GetRowCellValue(row, colBillCode));
            //    receiver = TextUtils.ToString(row["Receiver"]); //grvData.GetRowCellValue(row, colReceiver));
            //    DataRow dr = row;// grvData.GetDataRow(row);

            //    dr["ID"] = 0;
            //    dr["Note"] = dr["BillCode"];
            //    dt.ImportRow(dr);
            //    if (listSupplierSales.Count > 1 || listSupplierSales.Count > 1)
            //    {
            //        MessageBox.Show("Bạn cần chọn các sản phẩm cùng NCC hoặc Khách hàng!", "Thông báo", MessageBoxButtons.OK);
            //        return;
            //    }
            //}


            for (int i = 0; i < listRows.Count; i++)
            {
                DataRow row = listRows[i];
                nameNCC = TextUtils.ToString(row["Suplier"]);// grvData.GetRowCellValue(row, colSuplier));
                if (!listSupplierSales.Contains(nameNCC)) listSupplierSales.Add(nameNCC);
                customerID = TextUtils.ToInt(row["Suplier"]); //grvData.GetRowCellValue(row, colCustomerID));
                if (!listCustomers.Contains(customerID)) listCustomers.Add(customerID);

                supplierID = TextUtils.ToInt(row["suplierID"]); //grvData.GetRowCellValue(row, colsuplierID));
                deliverID = TextUtils.ToInt(row["DeliverID"]); //grvData.GetRowCellValue(row, colDeliverID));
                BillCode = TextUtils.ToString(row["BillCode"]); //grvData.GetRowCellValue(row, colBillCode));
                receiver = TextUtils.ToString(row["Receiver"]); //grvData.GetRowCellValue(row, colReceiver));
                DataRow dr = row;// grvData.GetDataRow(row);

                dr["ID"] = 0;
                dr["Note"] = dr["BillCode"];
                dr["STT"] = i + 1;
                dt.ImportRow(dr);
                if (listSupplierSales.Count > 1 || listSupplierSales.Count > 1)
                {
                    MessageBox.Show("Bạn cần chọn các sản phẩm cùng NCC hoặc Khách hàng!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }


            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    if (grvData.IsRowSelected(i))
            //    {


            //    }

            //}
            if (dt.Rows.Count == 0) return;

            frm.dtLoad = dt;
            frm.deliverID = deliverID;
            frm.supplierID = supplierID;
            frm.BillCode = BillCode;
            frm.warehouseID = wareHouseId;
            frm.customerID = customerID;
            frm.openFrmSummary = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listRows.Clear();
                LoadData();
            }

        }
        private void grvBillImportTech_DoubleClick1(object sender, EventArgs e)
        {
            btnEditRowChecked_Click(null, null);
        }
        #region Trường hợp chọn ngẫu nhiên n NCC và mở lên n form
        private void btnEditRowChecked_Click_multiNCC(object sender, EventArgs e)
        {
            List<string> checkNCC = new List<string>();
            List<int> checkCustomer = new List<int>();
            DataTable dtSet1 = dtLoad.Clone();
            DataTable dtSet2 = dtLoad.Clone();
            dtSet1.Clear();


            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (grvData.IsRowSelected(i))
                {
                    nameNCC = TextUtils.ToString(grvData.GetRowCellValue(i, colSuplier));
                    if (!checkNCC.Contains(nameNCC)) checkNCC.Add(nameNCC);
                    customerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCustomerID));
                    if (!checkCustomer.Contains(customerID)) checkCustomer.Add(customerID);
                    DataRow dr = grvData.GetDataRow(i);
                    dtSet1.ImportRow(dr);
                }

            }
            if (dtSet1.Rows.Count == 0)
            {
                return;
            }
            if (checkNCC != null)
            {
                foreach (string name in checkNCC)
                {
                    frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New();
                    dtSet2.Clear();
                    for (int i = 0; i < dtSet1.Rows.Count; i++)
                    {
                        nameNCC = TextUtils.ToString(dtSet1.Rows[i][$"NCC"].ToString());
                        if (nameNCC == name)
                        {
                            supplierID = TextUtils.ToInt(dtSet1.Rows[i][$"suplierID"].ToString());
                            deliverID = TextUtils.ToInt(dtSet1.Rows[i][$"DeliverID"].ToString());
                            BillCode = dtSet1.Rows[i][$"BillCode"].ToString();
                            customerID = TextUtils.ToInt(dtSet1.Rows[i][$"CustomerID"].ToString());
                            dtSet2.ImportRow(dtSet1.Rows[i]);
                        }
                    }
                    SetData(dtSet2);
                }
            }
            else
            {
                foreach (int cusID in checkCustomer)
                {

                    dtSet2.Clear();
                    for (int i = 0; i < dtSet1.Rows.Count; i++)
                    {
                        customerID = TextUtils.ToInt(dtSet1.Rows[i][$"CustomerID"].ToString());
                        if (customerID == cusID)
                        {
                            supplierID = TextUtils.ToInt(dtSet1.Rows[i][$"suplierID"].ToString());
                            deliverID = TextUtils.ToInt(dtSet1.Rows[i][$"DeliverID"].ToString()); ;
                            BillCode = dtSet1.Rows[i][$"BillCode"].ToString();
                            customerID = TextUtils.ToInt(dtSet1.Rows[i][$"CustomerID"].ToString());
                            dtSet2.ImportRow(dtSet1.Rows[i]);
                        }
                    }
                    SetData(dtSet2);
                }
            }


        }
        void SetData(DataTable dt)
        {
            frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New();
            frm.dtLoad = dt;
            frm.deliverID = deliverID;
            frm.supplierID = supplierID;
            frm.BillCode = BillCode;
            frm.warehouseID = wareHouseId;
            frm.customerID = customerID;
            frm.openFrmSummary = true;
            frm.Show();
        }
        #endregion


    }
}

