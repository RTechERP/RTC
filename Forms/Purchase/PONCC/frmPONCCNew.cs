using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Forms.Technical;
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
    public partial class frmPONCCNew : _Forms
    {


        List<string> listAllID = new List<string>();
        List<bool> checkList = new List<bool>();
        int rowDetailIndex = 0;


        bool isShowSign = true;
        bool isShowSeal = true;

        public frmPONCCNew()
        {
            InitializeComponent();
        }

        private void frmPONCCNew_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);
            cboStatus.SelectedIndex = 0;

            LoadSupplierSale();
            LoadEmployee();
            LoadData();

            LogActions();
            //xtraTabPage2.PageVisible = Global.IsAdmin;
            //LoadWarehouse();

        }

        void LoadListID()
        {
            List<PONCCDetailModel> listAllPONCCDetails = SQLHelper<PONCCDetailModel>.FindAll();
            foreach (var item in listAllPONCCDetails)
            {
                // tạo list ID_DetailID để đánh dấu các row detail của các PONCC khác nhau
                listAllID.Add(item.PONCCID + "_" + item.ID);

                //tạo check list tương ứng với list ID_DetailID để lưu giá trị selected or not của các row detail
                checkList.Add(false);
            }
        }


        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            string keyword = txtKeyword.Text.Trim();
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            int supplierId = TextUtils.ToInt(cboSupplier.EditValue);
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            int status = cboStatus.SelectedIndex - 1;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
            {

                DataSet data = TextUtils.LoadDataSetFromSP("spGetPONCC_Khanh",
                                                         new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@SupplierID", "@Status", "@EmployeeID" },
                                                         new object[] { keyword, pageNumber, pageSize, dateStart, dateEnd, supplierId, status, employeeId });
                DataTable dt = data.Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    string sql = "";
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        int idPO = TextUtils.ToInt(row["ID"]);
                //        if (idPO <= 0) continue;

                //        int totalImport = TextUtils.ToInt(row["TotalImport"]);
                //        int IsSuccess = TextUtils.ToInt(row["IsSuccess"]);

                //        int statusPO = TextUtils.ToInt(row["Status"]);
                //        if (totalImport <= 0) continue;
                //        else if (IsSuccess <= 0) statusPO = 5;
                //        else statusPO = 1;

                //        sql += $"UPDATE dbo.PONCC SET Status = {statusPO} WHERE ID = {idPO}";

                //        row["Status"] = statusPO;
                //        if (statusPO == 0) row["StatusText"] = "Đang tiến hành";
                //        else if (statusPO == 1) row["StatusText"] = "Hoàn thành";
                //        else if (statusPO == 2) row["StatusText"] = "Đã thanh toán";
                //        else if (statusPO == 3) row["StatusText"] = "Huỷ";
                //        else if (statusPO == 4) row["StatusText"] = "Xoá";
                //        else if (statusPO == 5) row["StatusText"] = "Đã Y/c nhập kho";
                //    }

                //    dt.AcceptChanges();
                //    if (!string.IsNullOrEmpty(sql))
                //    {
                //        TextUtils.ExcuteSQL(sql);
                //    }
                //}
                DataTable dtPOCommercial = dt.Clone();
                var dataCommercial = dt.Select("[POType] = 0");
                if (dataCommercial.Length > 0)
                {
                    dtPOCommercial = dataCommercial.CopyToDataTable();
                }
                grdData.DataSource = dtPOCommercial;

                DataTable dtPOBorrow = dt.Clone();
                var dataBorrow = dt.Select("[POType] = 1");
                if (dataBorrow.Length > 0)
                {
                    dtPOBorrow = dataBorrow.CopyToDataTable();
                }
                gridControl1.DataSource = dtPOBorrow;

                txtTotalPage.Text = TextUtils.ToString(data.Tables[1].Rows[0]["TotalPage"]);


                LoadListID();
            }
        }

        void LoadDetail()
        {
            int poId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetPONCCDetail_Khanh", "A", new string[] { "@PONCCID" }, new object[] { poId });
            grdDetail.DataSource = dt;
            //if (grvData.IsRowSelected(grvData.FocusedRowHandle) == true)
            //{
            //    for (int i = 0; i < grvDetail.RowCount; i++)
            //    {
            //        DataRowView row = grvDetail.GetRow(i) as DataRowView;
            //        grvDetail.SelectRow(i);
            //    }
            //}

            // Load lại detail của các row đã select

        }

        void LoadDetailBorrow()
        {

            //PQ.Chien - UPDATE - 17 / 04 / 2025
            int poIdBorrow = TextUtils.ToInt(gridView1.GetFocusedRowCellValue(colIDBorrow));
            DataTable dtBorrow = TextUtils.LoadDataFromSP("spGetPONCCDetail_Khanh", "A", new string[] { "@PONCCID" }, new object[] { poIdBorrow });
            gridControl2.DataSource = dtBorrow;
        }


        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll();
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.DataSource = list;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.EditValue = Global.EmployeeID;
        }


        void LoadWarehouse()
        {
            List<WarehouseModel> listWarehouses = SQLHelper<WarehouseModel>.FindAll();
            btnRequestImport.DropDownItems.Clear();
            foreach (var item in listWarehouses)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem()
                {
                    Tag = item.ID,
                    Text = item.WarehouseName,
                };
                menuItem.Click += MenuItem_Click; ;
                btnRequestImport.DropDownItems.Add(menuItem);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            int warehouseID = TextUtils.ToInt(menuItem.Tag);
            RequestImport(warehouseID);
        }


        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void Approved(bool isApprove)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            SplitContainerControl splitContainer = tabSelected.Controls[0] as SplitContainerControl;
            if (splitContainer == null) return;

            GridControl gridControl = splitContainer.Panel1.Controls[0] as GridControl;
            if (gridControl == null) return;
            GridView gridView = gridControl.MainView as GridView;

            //var tabSelected = xtraTabControl1.SelectedTabPage;

            //if (tabSelected.Controls.Count <= 0) return;
            //GridControl gridControl = (GridControl)tabSelected.Controls[0];
            //GridView gridView = gridControl.MainView as GridView;

            string isApproveText = isApprove ? "duyệt" : "huỷ duyệt";
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn PO muốn {isApproveText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApproveText} danh sách PO đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;

            List<int> listId = new List<int>();
            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                if (id <= 0) continue;
                listId.Add(id);
            }
            if (listId.Count <= 0) return;

            int isApproveValue = isApprove ? 1 : 0;
            string idText = string.Join(",", listId);
            string sql = $"UPDATE PONCC SET IsApproved = {isApproveValue},UpdatedBy = '{Global.LoginName}',UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID IN ({idText})";
            TextUtils.ExcuteSQL(sql);
            btnSearch_Click(null, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = rowHandle;
            LoadDetail();
            LoadDetailBorrow();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            btnSearch_Click(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            btnSearch_Click(null, null);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            btnSearch_Click(null, null);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            btnSearch_Click(null, null);
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPONCCDetailNew frm = new frmPONCCDetailNew();
            TextUtils.OpenChildForm(frm, null);
            frm.FormClosed += Frm_FormClosed;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    btnSearch_Click(null, null);
            //}
        }
        int rowHandle = 0;

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            SplitContainerControl splitContainer = tabSelected.Controls[0] as SplitContainerControl;
            if (splitContainer == null) return;

            GridControl gridControl = splitContainer.Panel1.Controls[0] as GridControl;
            if (gridControl == null) return;
            GridView gridView = gridControl.MainView as GridView;

            //var tabSelected = xtraTabControl1.SelectedTabPage;

            //if (tabSelected.Controls.Count <= 0) return;
            //GridControl gridControl = (GridControl)tabSelected.Controls[0];
            //GridView gridView = gridControl.MainView as GridView;

            rowHandle = gridView.FocusedRowHandle;
            int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID.FieldName));
            if (id <= 0) return;
            PONCCModel po = SQLHelper<PONCCModel>.FindByID(id);
            frmPONCCDetailNew frm = new frmPONCCDetailNew();
            frm.po = po;
            frm.Tag = po.BillCode;
            TextUtils.OpenChildForm(frm, null);
            frm.FormClosed += Frm_FormClosed;

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    btnSearch_Click(null, null);
            //    grvData.FocusedRowHandle = rowHandle;
            //}
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmPONCCDetailNew frm = (frmPONCCDetailNew)sender;
            if (frm.IsSave)
            {
                btnSearch_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;
            }
        }


        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            SplitContainerControl splitContainer = tabSelected.Controls[0] as SplitContainerControl;
            if (splitContainer == null) return;

            GridControl gridControl = splitContainer.Panel1.Controls[0] as GridControl;
            if (gridControl == null) return;
            GridView gridView = gridControl.MainView as GridView;

            //var tabSelected = xtraTabControl1.SelectedTabPage;

            //if (tabSelected.Controls.Count <= 0) return;
            //GridControl gridControl = (GridControl)tabSelected.Controls[0];
            //GridView gridView = gridControl.MainView as GridView;

            int[] rowSelected = gridView.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn PO muốn xoá!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá danh sách PO đã chọn không?\n" +
                                                    $"Những PO đã được duyệt sẽ bỏ qua hoặc đã có phiếu nhập!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;
                    bool isApproved = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApproved.FieldName));
                    if (isApproved) continue;
                    int totalImport = TextUtils.ToInt(gridView.GetRowCellValue(row, colTotalImport.FieldName));
                    if (totalImport > 0) continue;

                    PONCCModel po = SQLHelper<PONCCModel>.FindByID(id);
                    po.IsDeleted = true;
                    SQLHelper<PONCCModel>.Update(po);
                }
            }
            btnSearch_Click(null, null);


            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (id <= 0) return;
            //bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            //string poCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPOCode));
            //if (isApproved)
            //{
            //    MessageBox.Show($"PO [{poCode}] đã được duyệt.\n Bạn không thể xoá!", "Thông báo");
            //    return;
            //}
            //DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá [{poCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialog == DialogResult.Yes)
            //{
            //    PONCCModel po = SQLHelper<PONCCModel>.FindByID(id);
            //    po.IsDeleted = true;

            //    SQLHelper<PONCCModel>.Update(po);

            //    btnSearch_Click(null, null);
            //}
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            Approved(true);
        }

        private void btnUnApproved_Click(object sender, EventArgs e)
        {
            Approved(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachPO_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachPO_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
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

        private void iNPOTiếngViệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetail(1);
        }

        private void inPOTiếngAnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetail(2);
        }

        void ShowDetail(int type)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            frmPONCCViewDetail frm = new frmPONCCViewDetail();
            frm.poId = id;
            frm.type = type;
            frm.isShowSign = isShowSign;
            frm.isShowSeal = isShowSeal;
            frm.Show();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrEmpty(value.Trim())) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }



        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void btnRequestImport_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            SplitContainerControl splitContainer = tabSelected.Controls[0] as SplitContainerControl;
            if (splitContainer == null) return;

            GridControl gridControl = splitContainer.Panel1.Controls[0] as GridControl;
            if (gridControl == null) return;
            GridView gridView = gridControl.MainView as GridView;

            //var tabSelected = xtraTabControl1.SelectedTabPage;

            //if (tabSelected.Controls.Count <= 0) return;
            //GridControl gridControl = (GridControl)tabSelected.Controls[0];
            //GridView gridView = gridControl.MainView as GridView;

            int[] rowSelecteds = gridView.GetSelectedRows();

            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn PO muốn yêu cầu nhập kho!", "Thông báo");
                return;
            }

            foreach (int row in rowSelecteds)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                if (id <= 0) continue;
                int status = TextUtils.ToInt(gridView.GetRowCellValue(row, colStatus.FieldName));
                string statusText = TextUtils.ToString(gridView.GetRowCellValue(row, colStatusText.FieldName));
                string code = TextUtils.ToString(gridView.GetRowCellValue(row, colPOCode.FieldName));
                if (status != 0 & status != 5)
                {
                    MessageBox.Show($"PO [{code}] đã {statusText}.\n Bạn không thể yêu cầu nhập kho!", "Thông báo");
                    return;
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu nhập kho danh sách PO đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> ids = new List<int>();
                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;
                    ids.Add(id);
                }
                List<DataRow> toDelete = new List<DataRow>();
                if (ids.Count <= 0) return;
                string idText = string.Join(",", ids);
                DataTable dt = TextUtils.LoadDataFromSP("spGetPONCCDetailByID", "A", new string[] { "@ID" }, new object[] { idText });

                // Delete from datatable rows Detail not selected
                foreach (DataRow row in dt.Rows)
                {
                    int selectedIndex = listAllID.IndexOf(row["PONCCID"].ToString() + "_" + row["ID"].ToString());
                    if (checkList[selectedIndex] == false)
                    {
                        toDelete.Add(row);
                    }
                }
                foreach (DataRow r in toDelete)
                {
                    dt.Rows.Remove(r);
                }
                //DataTable dtDistinct = dt.AsEnumerable().Distinct(x=>x.)
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupID" });
                DataTable distinctValuesDemo = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupRTCID" });

                foreach (DataRow row in distinctValues.Rows)
                {
                    int supplierSaleID = TextUtils.ToInt(row["SupplierSaleID"]);
                    int productGroupID = TextUtils.ToInt(row["ProductGroupID"]);
                    DataRow[] dtDetails = dt.Select($"SupplierSaleID = {supplierSaleID} AND ProductGroupID = {productGroupID}");
                    if (dtDetails.Length <= 0) continue;
                    else
                    {
                        var checkQtyRemain = dtDetails.Where(x => x.Field<decimal>("QuantityRemain") > 0).ToList();
                        if (checkQtyRemain.Count <= 0) continue;
                    }
                    var dataRow = dtDetails[0];

                    //Insert BillImport
                    BillImportModel bill = new BillImportModel();
                    bill.BillImportCode = GetBillImportCode("BillImport");
                    bill.CreatDate = DateTime.Now;
                    bill.Deliver = TextUtils.ToString(dataRow["FullName"]); //NV mua
                    bill.Reciver = "Admin kho";//
                    bill.Status = false;
                    bill.Suplier = TextUtils.ToString(dataRow["NameNCC"]);
                    bill.BillType = false;
                    bill.KhoType = TextUtils.ToString(dataRow["ProductGroupName"]);
                    bill.GroupID = TextUtils.ToString(dataRow["ProductGroupID"]);
                    bill.SupplierID = TextUtils.ToInt(dataRow["SupplierSaleID"]);
                    bill.DeliverID = TextUtils.ToInt(dataRow["UserID"]);
                    bill.ReciverID = 0;//Get Admin kho
                    bill.KhoTypeID = TextUtils.ToInt(dataRow["ProductGroupID"]);
                    bill.WarehouseID = 1;//HN; 2:HCM
                    bill.BillTypeNew = 4;//Trạng thái yêu cầu nhập kho
                    bill.DateRequestImport = DateTime.Now;
                    bill.CreatedDate = DateTime.Now;
                    bill.CreatedBy = Global.LoginName;

                    bill.ID = SQLHelper<BillImportModel>.Insert(bill).ID;

                    TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { bill.ID, Global.LoginName });

                    for (int i = 0; i < dtDetails.Length; i++)
                    {
                        var dataRowDetail = dtDetails[i];
                        decimal quantityRemain = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
                        if (quantityRemain <= 0) continue;
                        BillImportDetailModel detail = new BillImportDetailModel();
                        detail.BillImportID = bill.ID;
                        detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
                        detail.QtyRequest = detail.Qty = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
                        detail.Price = TextUtils.ToDecimal(dataRowDetail["UnitPrice"]);

                        detail.TotalPrice = detail.Qty * detail.Price;
                        detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
                        detail.ProjectCode = TextUtils.ToString(dataRowDetail["ProductCodeOfSupplier"]);
                        detail.SomeBill = "";
                        detail.Note = TextUtils.ToString(dataRowDetail["POCode"]);
                        //detail.Note = TextUtils.ToString(dataRowDetail["BillCode"]);
                        detail.STT = i + 1;
                        //detail.TotalQty = totalQty;
                        detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]);
                        detail.PONCCDetailID = TextUtils.ToInt(dataRowDetail["ID"]);
                        detail.SerialNumber = "";
                        detail.CodeMaPhieuMuon = "";
                        detail.BillExportDetailID = 0;
                        detail.ProjectPartListID = TextUtils.ToInt(dataRowDetail["ProjectPartListID"]);
                        detail.IsKeepProject = true;
                        detail.BillCodePO = TextUtils.ToString(dataRowDetail["BillCode"]);

                        SQLHelper<BillImportDetailModel>.Insert(detail);
                    }

                    frmBillImportDetail frm = new frmBillImportDetail();
                    frm.billImport = bill;
                    frm.WarehouseCode = "HN";
                    frm.IDDetail = bill.ID;
                    frm.Text += $" - {bill.BillImportCode}";
                    frm.Tag = bill.BillImportCode;
                    frm.ShowDialog();

                }

                //string sql = $"UPDATE dbo.PONCC SET Status = 5 WHERE ID IN({idText})";
                //TextUtils.ExcuteSQL(sql);

                btnSearch_Click(null, null);
            }
        }

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void RequestImport(int warehouseID)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;

            SplitContainerControl splitContainer = tabSelected.Controls[0] as SplitContainerControl;
            if (splitContainer == null) return;

            GridControl gridControl = splitContainer.Panel1.Controls[0] as GridControl;
            if (gridControl == null) return;
            GridView gridView = gridControl.MainView as GridView;

            //GridView gridView = grvData;

            //var tabSelected = xtraTabControl1.SelectedTabPage;

            //if (tabSelected.Controls.Count <= 0) return;
            //GridControl gridControl = (GridControl)tabSelected.Controls[0];
            //GridView gridView = gridControl.MainView as GridView;

            // Get selected rows
            int[] rowSelecteds = gridView?.GetSelectedRows();

            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn PO muốn yêu cầu nhập kho!", "Thông báo");
                return;
            }

            foreach (int row in rowSelecteds)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                if (id <= 0) continue;
                int status = TextUtils.ToInt(gridView.GetRowCellValue(row, colStatus.FieldName));
                string statusText = TextUtils.ToString(gridView.GetRowCellValue(row, colStatusText.FieldName));
                string code = TextUtils.ToString(gridView.GetRowCellValue(row, colPOCode.FieldName));
                if (status != 0 & status != 5)
                {
                    MessageBox.Show($"PO [{code}] đã {statusText}.\n Bạn không thể yêu cầu nhập kho!", "Thông báo");
                    return;
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn yêu cầu nhập kho danh sách PO đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> ids = new List<int>();
                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;
                    ids.Add(id);
                }
                List<DataRow> toDelete = new List<DataRow>();
                if (ids.Count <= 0) return;
                string idText = string.Join(",", ids).Trim();
                DataTable dt = TextUtils.LoadDataFromSP("spGetPONCCDetailByID", "A", new string[] { "@ID" }, new object[] { idText });

                // Delete from datatable rows Detail not selected
                foreach (DataRow row in dt.Rows)
                {
                    int selectedIndex = listAllID.IndexOf(row["PONCCID"].ToString() + "_" + row["ID"].ToString());
                    if (selectedIndex < 0) continue;
                    if (checkList[selectedIndex] == false)
                    {
                        toDelete.Add(row);
                    }
                }
                foreach (DataRow r in toDelete)
                {
                    dt.Rows.Remove(r);
                }


                //DataTable dtDistinct = dt.AsEnumerable().Distinct(x=>x.)
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupID" });
                var listSale = distinctValues.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID") && !row.IsNull("ProductGroupID")).ToList();

                DataTable distinctValuesDemo = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupRTCID" });
                var listDemo = distinctValuesDemo.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID") && !row.IsNull("ProductGroupRTCID")).ToList();

                if (listSale.Count() > 0)
                {
                    RequestImportSale(dt, warehouseID, idText);
                }
                if (listDemo.Count > 0)
                {
                    RequestImportDemo(dt, warehouseID, idText);
                }
                //string sql = $"UPDATE dbo.PONCC SET Status = 5 WHERE ID IN({idText})";
                //TextUtils.ExcuteSQL(sql);

                btnSearch_Click(null, null);
            }
        }

        //void RequestImportSale(DataTable dt, int warehouseID)
        //{
        //    DataView view = new DataView(dt);
        //    DataTable distinctValues = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupID" });
        //    var filteredRowsSale = distinctValues.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID") && !row.IsNull("ProductGroupID")).CopyToDataTable();
        //    foreach (DataRow row in filteredRowsSale.Rows)
        //    {
        //        int supplierSaleID = TextUtils.ToInt(row["SupplierSaleID"]);
        //        int productGroupID = TextUtils.ToInt(row["ProductGroupID"]);
        //        DataRow[] dtDetails = dt.Select($"SupplierSaleID = {supplierSaleID} AND ProductGroupID = {productGroupID}");
        //        if (dtDetails.Length <= 0) continue;
        //        else
        //        {
        //            var checkQtyRemain = dtDetails.Where(x => x.Field<decimal>("QuantityRemain") > 0).ToList();
        //            if (checkQtyRemain.Count <= 0) continue;
        //        }
        //        var dataRow = dtDetails[0];

        //        //Insert BillImport
        //        BillImportModel bill = new BillImportModel();
        //        bill.BillImportCode = GetBillImportCode();
        //        bill.CreatDate = DateTime.Now;
        //        bill.Deliver = TextUtils.ToString(dataRow["FullName"]); //NV mua
        //        bill.Reciver = "Admin kho";//
        //        bill.Status = false;
        //        bill.Suplier = TextUtils.ToString(dataRow["NameNCC"]);
        //        bill.BillType = false;
        //        bill.KhoType = TextUtils.ToString(dataRow["ProductGroupName"]);
        //        bill.GroupID = TextUtils.ToString(dataRow["ProductGroupID"]);
        //        bill.SupplierID = TextUtils.ToInt(dataRow["SupplierSaleID"]);
        //        bill.DeliverID = TextUtils.ToInt(dataRow["UserID"]);
        //        bill.ReciverID = 0;//Get Admin kho
        //        bill.KhoTypeID = TextUtils.ToInt(dataRow["ProductGroupID"]);
        //        bill.WarehouseID = warehouseID;//HN; 2:HCM
        //        bill.BillTypeNew = 4;//Trạng thái yêu cầu nhập kho
        //        bill.DateRequestImport = DateTime.Now;
        //        bill.CreatedDate = DateTime.Now;
        //        bill.CreatedBy = Global.LoginName;

        //        bill.ID = SQLHelper<BillImportModel>.Insert(bill).ID;

        //        TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { bill.ID, Global.LoginName });

        //        for (int i = 0; i < dtDetails.Length; i++)
        //        {
        //            var dataRowDetail = dtDetails[i];
        //            decimal quantityRemain = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
        //            if (quantityRemain <= 0) continue;
        //            BillImportDetailModel detail = new BillImportDetailModel();
        //            detail.BillImportID = bill.ID;
        //            detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
        //            detail.QtyRequest = detail.Qty = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
        //            detail.Price = TextUtils.ToDecimal(dataRowDetail["UnitPrice"]);

        //            detail.TotalPrice = detail.Qty * detail.Price;
        //            detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
        //            detail.ProjectCode = TextUtils.ToString(dataRowDetail["ProductCodeOfSupplier"]);
        //            detail.SomeBill = "";
        //            detail.Note = TextUtils.ToString(dataRowDetail["POCode"]);
        //            //detail.Note = TextUtils.ToString(dataRowDetail["BillCode"]);
        //            detail.STT = i + 1;
        //            //detail.TotalQty = totalQty;
        //            detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]);
        //            detail.PONCCDetailID = TextUtils.ToInt(dataRowDetail["ID"]);
        //            detail.SerialNumber = "";
        //            detail.CodeMaPhieuMuon = "";
        //            detail.BillExportDetailID = 0;
        //            detail.ProjectPartListID = TextUtils.ToInt(dataRowDetail["ProjectPartListID"]);
        //            detail.IsKeepProject = true;
        //            detail.BillCodePO = TextUtils.ToString(dataRowDetail["BillCode"]);

        //            SQLHelper<BillImportDetailModel>.Insert(detail);
        //        }

        //        frmBillImportDetail frm = new frmBillImportDetail();
        //        string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
        //        frm.WarehouseCode = warehouseCode;
        //        frm.billImport = bill;
        //        frm.warehouseID = warehouseID;

        //        // frm.WarehouseCode = "HN";
        //        frm.IDDetail = bill.ID;
        //        frm.Text += $" - {bill.BillImportCode}";
        //        frm.Tag = bill.BillImportCode;
        //        frm.ShowDialog();

        //    }
        //}
        //void RequestImportDemo(DataTable dt, int warehouseID)
        //{
        //    DataView view = new DataView(dt);
        //    DataTable distinctValuesDemo = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupRTCID" });
        //    var filteredRowsDemo = distinctValuesDemo.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID") && !row.IsNull("ProductGroupRTCID")).CopyToDataTable();

        //    foreach (DataRow row in distinctValuesDemo.Rows)
        //    {
        //        int supplierSaleID = TextUtils.ToInt(row["SupplierSaleID"]);
        //        int productGroupRTCID = TextUtils.ToInt(row["ProductGroupRTCID"]);
        //        DataRow[] dtDetails = dt.Select($"SupplierSaleID = {supplierSaleID} AND ProductGroupRTCID = {productGroupRTCID}");
        //        if (dtDetails.Length <= 0) continue;
        //        else
        //        {
        //            var checkQtyRemain = dtDetails.Where(x => x.Field<decimal>("QuantityRemain") > 0).ToList();
        //            if (checkQtyRemain.Count <= 0) continue;
        //        }
        //        var dataRow = dtDetails[0];

        //        //Insert BillImport
        //        BillImportTechnicalModel bill = new BillImportTechnicalModel();
        //        bill.BillCode = GetBillImportDemoCode();
        //        bill.CreatDate = DateTime.Now;
        //        bill.Deliver = TextUtils.ToString(dataRow["FullName"]); //NV mua
        //        bill.Receiver = "Admin kho";//
        //        bill.Status = false;
        //        bill.Suplier = TextUtils.ToString(dataRow["NameNCC"]);
        //        bill.BillType = false;
        //        // bill.KhoType = TextUtils.ToString(dataRow["ProductGroupName"]);
        //        //  bill.GroupID = TextUtils.ToString(dataRow["ProductGroupID"]);
        //        bill.SupplierSaleID = TextUtils.ToInt(dataRow["SupplierSaleID"]);
        //        bill.DeliverID = TextUtils.ToInt(dataRow["UserID"]);
        //        bill.ReceiverID = 0;//Get Admin kho
        //        // bill.KhoTypeID = TextUtils.ToInt(dataRow["ProductGroupID"]);
        //        bill.WarehouseID = warehouseID;//HN; 2:HCM
        //        bill.BillTypeNew = 5;//Trạng thái yêu cầu nhập kho
        //        bill.DateRequestImport = DateTime.Now;
        //        bill.CreatedDate = DateTime.Now;
        //        bill.CreatedBy = Global.LoginName;
        //        bill.WarehouseType = "Demo";

        //        bill.ID = SQLHelper<BillImportTechnicalModel>.Insert(bill).ID;

        //        TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { bill.ID, Global.LoginName });

        //        for (int i = 0; i < dtDetails.Length; i++)
        //        {
        //            var dataRowDetail = dtDetails[i];
        //            decimal quantityRemain = TextUtils.ToDecimal(dataRowDetail["QuantityRemainDemo"]);
        //            if (quantityRemain <= 0) continue;
        //            BillImportDetailTechnicalModel detail = new BillImportDetailTechnicalModel();
        //            detail.BillImportTechID = bill.ID;
        //            detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductRTCID"]);
        //            detail.QtyRequest = detail.Quantity = TextUtils.ToDecimal(dataRowDetail["QuantityRemainDemo"]);
        //            detail.Price = TextUtils.ToDecimal(dataRowDetail["UnitPrice"]);

        //            detail.TotalPrice = detail.Quantity * detail.Price;
        //            detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
        //            detail.ProjectCode = TextUtils.ToString(dataRowDetail["ProductCodeOfSupplier"]);
        //            detail.SomeBill = "";
        //            detail.Note = TextUtils.ToString(dataRowDetail["POCode"]);
        //            //detail.Note = TextUtils.ToString(dataRowDetail["BillCode"]);
        //            detail.STT = i + 1;
        //            //detail.TotalQty = totalQty;
        //            detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]);
        //            detail.PONCCDetailID = TextUtils.ToInt(dataRowDetail["ID"]);
        //            //  detail.SerialNumber = "";
        //            //   detail.CodeMaPhieuMuon = "";
        //            //    detail.BillExportDetailID = 0;
        //            //   detail.ProjectPartListID = TextUtils.ToInt(dataRowDetail["ProjectPartListID"]);
        //            //   detail.IsKeepProject = true;
        //            detail.BillCodePO = TextUtils.ToString(dataRowDetail["BillCode"]);

        //            SQLHelper<BillImportDetailTechnicalModel>.Insert(detail);
        //        }

        //        frmBillImportTechDetail frm = new frmBillImportTechDetail();
        //        string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
        //        frm.billImport = bill;
        //        frm.warehouseIDNew = warehouseID;
        //        //frm.WarehouseCode = "HN";
        //        frm.IDDetail = bill.ID;
        //        frm.Text += $" - {bill.BillCode}";
        //        frm.Tag = bill.BillCode;
        //        frm.ShowDialog();

        //    }
        //}


        void RequestImportSale(DataTable dt, int warehouseID, string idPONCC)
        {
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupID" });
            var filteredRowsSale = distinctValues.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID") && !row.IsNull("ProductGroupID")).CopyToDataTable();
            foreach (DataRow row in filteredRowsSale.Rows)
            {

                int supplierSaleID = TextUtils.ToInt(row["SupplierSaleID"]);
                int productGroupID = TextUtils.ToInt(row["ProductGroupID"]);
                DataRow[] dtDetails = dt.Select($"SupplierSaleID = {supplierSaleID} AND ProductGroupID = {productGroupID}");
                if (dtDetails.Length <= 0) continue;
                else
                {
                    var checkQtyRemain = dtDetails.Where(x => x.Field<decimal>("QuantityRemain") > 0).ToList();
                    if (checkQtyRemain.Count <= 0) continue;
                }
                var dataRow = dtDetails[0];
                int poNCCId = TextUtils.ToInt(dataRow["PONCCID"]);

                //Insert BillImport
                BillImportModel bill = new BillImportModel();
                bill.BillImportCode = GetBillImportCode("BillImport");
                bill.CreatDate = DateTime.Now;
                bill.Deliver = TextUtils.ToString(dataRow["FullName"]); //NV mua
                bill.Reciver = "Admin kho";//
                bill.Status = false;
                bill.Suplier = TextUtils.ToString(dataRow["NameNCC"]);
                bill.BillType = false;
                bill.KhoType = TextUtils.ToString(dataRow["ProductGroupName"]);
                bill.GroupID = TextUtils.ToString(dataRow["ProductGroupID"]);
                bill.SupplierID = TextUtils.ToInt(dataRow["SupplierSaleID"]);
                bill.DeliverID = TextUtils.ToInt(dataRow["UserID"]);
                //bill.ReciverID = 0;//Get Admin kho
                bill.KhoTypeID = TextUtils.ToInt(dataRow["ProductGroupID"]);
                bill.WarehouseID = warehouseID;//HN; 2:HCM
                bill.BillTypeNew = 4;//Trạng thái yêu cầu nhập kho
                bill.DateRequestImport = DateTime.Now;
                bill.CreatedDate = DateTime.Now;
                bill.CreatedBy = Global.LoginName;

                bill.RulePayID = TextUtils.ToInt(dataRow["RulePayID"]);


                DataTable dtGroupWarehouse = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
                                                   new string[] { "@WarehouseID", "@ProductGroupID" },
                                                   new object[] { warehouseID, productGroupID });

                bill.ReciverID = dtGroupWarehouse.Rows.Count > 0 ? TextUtils.ToInt(dtGroupWarehouse.Rows[0]["UserID"]) : 0; //Get Admin kho


                //bill.ID = SQLHelper<BillImportModel>.Insert(bill).ID;

                //TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { bill.ID, Global.LoginName });

                //for (int i = 0; i < dtDetails.Length; i++)
                //{
                //    var dataRowDetail = dtDetails[i];
                //    decimal quantityRemain = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
                //    if (quantityRemain <= 0) continue;
                //    BillImportDetailModel detail = new BillImportDetailModel();
                //    detail.BillImportID = bill.ID;
                //    detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductSaleID"]);
                //    detail.QtyRequest = detail.Qty = TextUtils.ToDecimal(dataRowDetail["QuantityRemain"]);
                //    detail.Price = TextUtils.ToDecimal(dataRowDetail["UnitPrice"]);

                //    detail.TotalPrice = detail.Qty * detail.Price;
                //    detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
                //    detail.ProjectCode = TextUtils.ToString(dataRowDetail["ProductCodeOfSupplier"]);
                //    detail.SomeBill = "";
                //    detail.Note = TextUtils.ToString(dataRowDetail["POCode"]);
                //    //detail.Note = TextUtils.ToString(dataRowDetail["BillCode"]);
                //    detail.STT = i + 1;
                //    //detail.TotalQty = totalQty;
                //    detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]);
                //    detail.PONCCDetailID = TextUtils.ToInt(dataRowDetail["ID"]);
                //    detail.SerialNumber = "";
                //    detail.CodeMaPhieuMuon = "";
                //    detail.BillExportDetailID = 0;
                //    detail.ProjectPartListID = TextUtils.ToInt(dataRowDetail["ProjectPartListID"]);
                //    detail.IsKeepProject = true;
                //    detail.BillCodePO = TextUtils.ToString(dataRowDetail["BillCode"]);

                //    //SQLHelper<BillImportDetailModel>.Insert(detail);
                //}

                frmBillImportDetail frm = new frmBillImportDetail();
                string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
                frm.WarehouseCode = warehouseCode;
                frm.billImport = bill;
                frm.warehouseID = warehouseID;
                //NT.Huy update 14/05/24
                frm.dtDetails = dtDetails;

                // frm.POCode = TextUtils.ToString(row[colPOCode]);
                // frm.WarehouseCode = "HN";
                frm.IDDetail = bill.ID;
                frm.Text += $" - {bill.BillImportCode}";
                frm.Tag = bill.BillImportCode;
                //frm.idPONCCText = idPONCC;
                frm.poNCCId = poNCCId;

                //NT.Huy Update send Email
                frm.POCode = TextUtils.ToString(dataRow["POCode"]);
                //frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string subject = frm.subject;
                    string body = frm.body;
                    int receiverMailID = frm.receiverMailID;
                    //EmailSender.SendEmail(subject, body, receiverMailID, "");
                }

            }
        }

        void RequestImportDemo(DataTable dt, int warehouseID, string idPONCC)
        {

            //NT.Huy update 23/05/2024
            DataView view = new DataView(dt);
            //DataTable distinctValuesDemo = view.ToTable(true, new string[] { "SupplierSaleID", "ProductGroupRTCID" });
            DataTable distinctValuesDemo = view.ToTable(true, new string[] { "SupplierSaleID" });

            //var filteredRowsDemo = distinctValuesDemo.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID") && !row.IsNull("ProductGroupRTCID")).CopyToDataTable();
            var filteredRowsDemo = distinctValuesDemo.AsEnumerable().Where(row => !row.IsNull("SupplierSaleID")).CopyToDataTable();

            foreach (DataRow row in filteredRowsDemo.Rows)
            {
                int supplierSaleID = TextUtils.ToInt(row["SupplierSaleID"]);
                DataRow[] dtDetails = dt.Select($"SupplierSaleID = {supplierSaleID} and ProductRTCID is not null and ProductRTCID <> 0");
                //   DataRow[] dtDetails = dt.Select($"SupplierSaleID = {supplierSaleID}");

                if (dtDetails.Length <= 0) continue;
                else
                {
                    var checkQtyRemain = dtDetails.Where(x => x.Field<decimal>("QuantityRemain") > 0).ToList();
                    if (checkQtyRemain.Count <= 0) continue;
                }
                var dataRow = dtDetails[0];

                int poNCCId = TextUtils.ToInt(dataRow["PONCCID"]);

                //Insert BillImport
                BillImportTechnicalModel bill = new BillImportTechnicalModel();
                bill.BillCode = GetBillImportCode("BillImportTechnical");
                bill.CreatDate = DateTime.Now;
                bill.Deliver = TextUtils.ToString(dataRow["FullName"]); //NV mua
                bill.Receiver = "Admin kho";//
                bill.Status = false;
                bill.Suplier = TextUtils.ToString(dataRow["NameNCC"]);
                bill.BillType = false;
                // bill.KhoType = TextUtils.ToString(dataRow["ProductGroupName"]);
                //  bill.GroupID = TextUtils.ToString(dataRow["ProductGroupID"]);
                bill.SupplierSaleID = TextUtils.ToInt(dataRow["SupplierSaleID"]);
                bill.DeliverID = TextUtils.ToInt(dataRow["UserID"]);
                bill.ReceiverID = 0;//Get Admin kho
                // bill.KhoTypeID = TextUtils.ToInt(dataRow["ProductGroupID"]);
                bill.WarehouseID = 1;//HN; 2:HCM
                bill.BillTypeNew = 4;//Trạng thái yêu cầu nhập kho
                bill.DateRequestImport = DateTime.Now;
                bill.CreatedDate = DateTime.Now;
                bill.CreatedBy = Global.LoginName;
                bill.ApproverID = 54; //set ng duyệt mặc định a Quyền

                //NT.Huy update 22/05/2024
                bill.WarehouseType = "Demo";

                bill.RulePayID = TextUtils.ToInt(dataRow["RulePayID"]);

                //bill.ID = SQLHelper<BillImportTechnicalModel>.Insert(bill).ID;

                //TextUtils.ExcuteScalar("spCreateDocumentImport", new string[] { "@BillImportID", "@CreatedBy" }, new object[] { bill.ID, Global.LoginName });

                //for (int i = 0; i < dtDetails.Length; i++)
                //{
                //    var dataRowDetail = dtDetails[i];
                //    decimal quantityRemain = TextUtils.ToDecimal(dataRowDetail["QuantityRemainDemo"]);
                //    if (quantityRemain <= 0) continue;
                //    BillImportDetailTechnicalModel detail = new BillImportDetailTechnicalModel();
                //    detail.BillImportTechID = bill.ID;
                //    detail.ProductID = TextUtils.ToInt(dataRowDetail["ProductRTCID"]);
                //    detail.QtyRequest = detail.Quantity = TextUtils.ToDecimal(dataRowDetail["QuantityRemainDemo"]);
                //    detail.Price = TextUtils.ToDecimal(dataRowDetail["UnitPrice"]);

                //    detail.TotalPrice = detail.Quantity * detail.Price;
                //    detail.ProjectName = TextUtils.ToString(dataRowDetail["ProjectName"]);
                //    detail.ProjectCode = TextUtils.ToString(dataRowDetail["ProductCodeOfSupplier"]);
                //    detail.SomeBill = "";
                //    detail.Note = TextUtils.ToString(dataRowDetail["POCode"]);
                //    //detail.Note = TextUtils.ToString(dataRowDetail["BillCode"]);
                //    detail.STT = i + 1;
                //    //detail.TotalQty = totalQty;
                //    detail.ProjectID = TextUtils.ToInt(dataRowDetail["ProjectID"]);
                //    detail.PONCCDetailID = TextUtils.ToInt(dataRowDetail["ID"]);
                //    //detail.SerialNumber = "";
                //    //detail.CodeMaPhieuMuon = "";
                //    //detail.BillExportDetailID = 0;
                //    //detail.ProjectPartListID = TextUtils.ToInt(dataRowDetail["ProjectPartListID"]);
                //    //detail.IsKeepProject = true;
                //    detail.BillCodePO = TextUtils.ToString(dataRowDetail["BillCode"]);

                //  //  SQLHelper<BillImportDetailTechnicalModel>.Insert(detail);
                //}

                frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New();
                string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
                frm.billImport = bill;
                frm.warehouseID = warehouseID;
                frm.flag = 1;
                //NT.Huy update 14/05/24
                frm.dtDetails = dtDetails;

                //frm.WarehouseCode = "HN";
                frm.IDDetail = bill.ID;
                frm.Text += $" - {bill.BillCode}";
                frm.Tag = bill.BillCode;

                //NT.Huy Update send Email
                frm.POCode = TextUtils.ToString(dataRow["POCode"]);
                //frm.ShowDialog();

                //frm.idPONCCText = idPONCC;
                frm.poNCCId = poNCCId;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string subject = frm.subject;
                    string body = frm.body;
                    int receiverMailID = frm.receiverMailID;
                    //EmailSender.SendEmail(subject, body, receiverMailID, "");
                }

            }
        }
        string GetBillImportCode(string modelName)
        {
            //TextUtils.GetBillCode("BillImport", 4);


            //DateTime dateNow = DateTime.Now;
            //string code = $"PNK{dateNow.ToString("yyMMdd")}";
            ////Get bill code mới nhất
            //var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            //var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            //var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            //BillImportModel bill = SQLHelper<BillImportModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();

            //string currentCode = bill == null ? "" : bill.BillImportCode.Trim();
            //int stt = string.IsNullOrEmpty(currentCode) ? 1 : TextUtils.ToInt(currentCode.Substring(currentCode.Length - 3)) + 1;
            //string sttText = stt.ToString();
            //while (sttText.Length < 3)
            //{
            //    sttText = "0" + sttText;
            //}
            //string billImportCode = code + sttText;
            string billImportCode = TextUtils.GetBillCode(modelName, 4);
            return billImportCode;
        }

        string GetBillImportDemoCode()
        {
            DateTime dateNow = DateTime.Now;
            string code = $"PNKD{dateNow.ToString("yyMMdd")}";
            var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            BillImportTechnicalModel bill = SQLHelper<BillImportTechnicalModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();

            string currentCode = bill == null ? "" : bill.BillCode.Trim();
            int stt = string.IsNullOrEmpty(currentCode) ? 1 : TextUtils.ToInt(currentCode.Substring(currentCode.Length - 3)) + 1;
            string sttText = stt.ToString();
            while (sttText.Length < 3)
            {
                sttText = "0" + sttText;
            }

            string billImportCode = code + sttText;
            return billImportCode;
        }

        string GetPOCode(int supplierSaleId)
        {
            string code = "";
            SupplierSaleModel supplier = SQLHelper<SupplierSaleModel>.FindByID(supplierSaleId);
            if (supplier != null && supplier.ID > 0)
            {
                code = $"{DateTime.Now.ToString("MMyyyy")}-{supplier.CodeNCC}-";
                string currentCode = TextUtils.ToString(TextUtils.ExcuteScalar($"EXEC dbo.spGetPOCodeInPONCC N'{code}'"));

                int stt = 1;
                if (!string.IsNullOrEmpty(currentCode.Trim()))
                {
                    stt = TextUtils.ToInt(currentCode.Substring(code.Length + 1));
                    stt++;
                }

                string sttText = stt.ToString();
                while (sttText.Length < 3)
                {
                    sttText = $"0{sttText}";
                }
                code += sttText;
            }

            //txtPOCode.Text = code;
            return code;
        }

        string GetBillCode()
        {
            string code = "DMH";
            int stt = 1;
            PONCCModel po = SQLHelper<PONCCModel>.FindAll().OrderByDescending(x => x.ID).FirstOrDefault();
            string currentCode = TextUtils.ToString(po.BillCode);
            if (!string.IsNullOrEmpty(currentCode.Trim()))
            {
                stt = TextUtils.ToInt(currentCode.Substring(code.Length + 1));
                stt++;
            }

            string sttText = stt.ToString();
            while (sttText.Length < 5)
            {
                sttText = $"0{sttText}";
            }
            code += sttText;

            //txtBillCode.Text = code;
            return code;
        }

        private void grvDetail_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {


            // Kiểm tra sự kiện grvDetail selection change do người dùng click vào ô select trong grvDetail hay do event khác gọi đến 
            if (rowDetailIndex < 0)
            {
                for (int i = 0; i < grvDetail.RowCount; i++)
                {
                    string selectedPONCCID = grvDetail.GetRowCellValue(i, colPONCCID).ToString();
                    string selectedDetailID = grvDetail.GetRowCellValue(i, colID).ToString();
                    int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                    if (selectedIndex < 0) continue;
                    var checkSelected = grvDetail.IsRowSelected(i);

                    if (checkSelected == true)
                    {
                        checkList[selectedIndex] = true;
                    }
                    else
                    {
                        checkList[selectedIndex] = false;

                    }

                }
            }
            else
            {
                string selectedPONCCID = grvDetail.GetRowCellValue(rowDetailIndex, colPONCCID).ToString();
                string selectedDetailID = grvDetail.GetRowCellValue(rowDetailIndex, colID).ToString();
                int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                if (selectedIndex < 0) return;
                var checkSelected = grvDetail.IsRowSelected(rowDetailIndex);

                if (checkSelected == true)
                {
                    checkList[selectedIndex] = true;
                }
                else
                {
                    checkList[selectedIndex] = false;

                }
            }
            rowDetailIndex = -1;
        }

        private void grvData_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {


            try
            {
                int[] rowSelectedDetail = grvDetail.GetSelectedRows();

                // select all row in GrvDetail
                if (rowSelectedDetail.Length <= 0)
                {
                    for (int i = 0; i < grvDetail.RowCount; i++)
                    {
                        rowDetailIndex = i;


                        if (grvData.IsRowSelected(grvData.FocusedRowHandle))
                        {
                            grvDetail.SelectRow(i);
                            string selectedPONCCID = grvDetail.GetRowCellValue(i, colPONCCID).ToString();
                            string selectedDetailID = grvDetail.GetRowCellValue(i, colID).ToString();
                            int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                            if (selectedIndex < 0) continue;
                            checkList[selectedIndex] = true;
                        }
                        else
                        {
                            grvDetail.UnselectRow(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }

        }


        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();

            // check that rowdetails has selected before or not
            if (grvData.IsRowSelected(grvData.FocusedRowHandle) == true)
            {
                for (int i = 0; i < grvDetail.RowCount; i++)
                {
                    string selectedPONCCID = grvDetail.GetRowCellValue(i, colPONCCID).ToString();
                    string selectedDetailID = grvDetail.GetRowCellValue(i, colID).ToString();
                    int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                    if (selectedIndex < 0) continue;
                    if (checkList[selectedIndex] == true)
                    {
                        rowDetailIndex = i;
                        grvDetail.SelectRow(i);
                    }

                }
            }
        }

        private void grvDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvDetail.GetFocusedRowCellValue(grvDetail.FocusedColumn));
                if (string.IsNullOrEmpty(value.Trim())) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnPrintPOVietNamese_Click(object sender, EventArgs e)
        {
            ShowDetail(1);
        }

        private void btnPrintPOEnglish_Click(object sender, EventArgs e)
        {
            ShowDetail(2);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var tabSelected = xtraTabControl1.SelectedTabPage;
                if (tabSelected.Controls.Count <= 0) return;

                SplitContainerControl splitContainer = tabSelected.Controls[0] as SplitContainerControl;
                if (splitContainer == null) return;

                GridControl gridControl = splitContainer.Panel1.Controls[0] as GridControl;
                if (gridControl == null) return;
                GridView gridView = gridControl.MainView as GridView;

                int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
                if (id <= 0) return;

                PONCCModel po = SQLHelper<PONCCModel>.FindByID(id);

                frmPONCCDetailNew frm = new frmPONCCDetailNew();
                //frm.po = poNew;
                //frm.Tag = po.BillCode;
                frm.dataCopy = new Tuple<bool, PONCCModel>(true, po);
                TextUtils.OpenChildForm(frm, null);
                frm.FormClosed += Frm_FormClosed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCopy_Click(null, null);
        }

        private void btnPaymentOrder_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            //PONCCModel po = SQLHelper<PONCCModel>.FindByID(id);

            frmPaymentOrderDetail frm = new frmPaymentOrderDetail();
            frm.ponccID = id;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnSummaryPO_Click(object sender, EventArgs e)
        {
            frmGetAllPO frm = new frmGetAllPO();
            frm.Show();
        }

        private void grvData_ScrollAnnotationsStyle(object sender, DevExpress.XtraGrid.Views.Grid.GridScrollAnnotationsStyleEventArgs e)
        {

        }

        private void btnWarehouseHN_Click(object sender, EventArgs e)
        {
            RequestImport(1);
        }

        private void btnWarehouseHCM_Click(object sender, EventArgs e)
        {
            RequestImport(2);
        }

        private void btnWarehouseBN_Click(object sender, EventArgs e)
        {
            RequestImport(3);
        }

        private void btnWarehouseHP_Click(object sender, EventArgs e)
        {
            RequestImport(4);
        }

        private void btnRequestImport_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmImportExcelPONCC frm = new frmImportExcelPONCC();
            frm.Show();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }
        }

        private void btnShowSign_Click(object sender, EventArgs e)
        {
            isShowSign = !isShowSign;

            //string text = isShowSign ? "v" : "x";
            //btnShowSign.Text = $"Hiển thị chữ ký ({text})";
            //var imHide = btnShowSeal.Image;
            //var imShow = btnShowSign.Image;

            if (isShowSign) btnShowSign.Image = global::Forms.Properties.Resources.CheckBox_16x16;
            else btnShowSign.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreview;

        }

        private void btnShowSeal_Click(object sender, EventArgs e)
        {
            isShowSeal = !isShowSeal;
            //string text = isShowSign ? "v" : "x";
            //btnShowSeal.Text = $"Hiển thị dấu ({text})";

            if (isShowSeal) btnShowSeal.Image = global::Forms.Properties.Resources.CheckBox_16x16;
            else btnShowSeal.Image = global::Forms.PrintRibbonControllerResources.RibbonPrintPreview_ClosePreview;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetailBorrow();

            // check that rowdetails has selected before or not
            if (gridView1.IsRowSelected(gridView1.FocusedRowHandle) == true)
            {
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    string selectedPONCCID = gridView2.GetRowCellValue(i, colPONCCID).ToString();
                    string selectedDetailID = gridView2.GetRowCellValue(i, colIDBorrow).ToString();
                    int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                    if (selectedIndex < 0) continue;
                    if (checkList[selectedIndex] == true)
                    {
                        rowDetailIndex = i;
                        gridView1.SelectRow(i);
                    }

                }
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                int[] rowSelectedDetail = gridView4.GetSelectedRows();

                // select all row in GrvDetail
                if (rowSelectedDetail.Length <= 0)
                {
                    for (int i = 0; i < gridView4.RowCount; i++)
                    {
                        rowDetailIndex = i;


                        if (gridView1.IsRowSelected(gridView1.FocusedRowHandle))
                        {
                            gridView4.SelectRow(i);
                            string selectedPONCCID = gridView4.GetRowCellValue(i, colPONCCID.FieldName).ToString();
                            string selectedDetailID = gridView4.GetRowCellValue(i, colID.FieldName).ToString();
                            int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                            if (selectedIndex < 0) continue;
                            checkList[selectedIndex] = true;
                        }
                        else
                        {
                            gridView4.UnselectRow(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void gridView4_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            // Kiểm tra sự kiện grvDetail selection change do người dùng click vào ô select trong grvDetail hay do event khác gọi đến 
            if (rowDetailIndex < 0)
            {
                for (int i = 0; i < gridView4.RowCount; i++)
                {
                    string selectedPONCCID = gridView4.GetRowCellValue(i, colPONCCID.FieldName).ToString();
                    string selectedDetailID = gridView4.GetRowCellValue(i, colID.FieldName).ToString();
                    int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                    if (selectedIndex < 0) continue;
                    var checkSelected = gridView4.IsRowSelected(i);

                    if (checkSelected == true)
                    {
                        checkList[selectedIndex] = true;
                    }
                    else
                    {
                        checkList[selectedIndex] = false;

                    }

                }
            }
            else
            {
                string selectedPONCCID = gridView4.GetRowCellValue(rowDetailIndex, colPONCCID).ToString();
                string selectedDetailID = gridView4.GetRowCellValue(rowDetailIndex, colID.FieldName).ToString();
                int selectedIndex = listAllID.IndexOf(selectedPONCCID + "_" + selectedDetailID);
                if (selectedIndex < 0) return;
                var checkSelected = gridView4.IsRowSelected(rowDetailIndex);

                if (checkSelected == true)
                {
                    checkList[selectedIndex] = true;
                }
                else
                {
                    checkList[selectedIndex] = false;

                }
            }
            rowDetailIndex = -1;
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }



        #region lưu logs
        private void LogActions()
        {
            try
            {
                var controls = new Component[] { btnWarehouseBN, btnWarehouseHCM, btnWarehouseHN, btnWarehouseHP };
                var actions = Enumerable.Repeat<string>("Add", controls.Length).ToArray();
                var logDatas = Enumerable.Repeat<Func<dynamic, dynamic>>(GetDataChange, controls.Length).ToArray();
                var oldData = GetCurrentData();
                var logger = new Logger(controls, actions, logDatas, this, oldData);
                logger.Start();
            }
            catch
            {

            }
        }
        private List<PONCCLog> GetCurrentData()
        {
            try
            {
                var data = new List<PONCCLog>();
                var tabSelected = xtraTabControl1.SelectedTabPage;
                if (tabSelected.Controls.Count <= 0) throw new Exception();
                if (!(tabSelected.Controls[0] is SplitContainerControl splitContainer)) throw new Exception(); ;
                if (!(splitContainer.Panel1.Controls[0] is GridControl gridControl)) throw new Exception(); ;
                GridView gridView = gridControl.MainView as GridView;
                int[] rowSelecteds = gridView?.GetSelectedRows();
                if (rowSelecteds.Length <= 0) throw new Exception();

                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;
                    string code = TextUtils.ToString(gridView.GetRowCellValue(row, colPOCode.FieldName));
                    var log = new PONCCLog();
                    var trueIndexes = checkList
                        .Select((value, index) => new { value, index })
                        .Where(x => x.value)
                        .Select(x => x.index)
                        .ToList();
                    var selectedValues = trueIndexes
                        .Select(index => listAllID[index])
                        .Where(value => value.StartsWith(id.ToString()))
                        .ToList();
                    log.Id = id;
                    log.Code = code;
                    log.SelectedDetailsId = selectedValues;
                    data.Add(log);
                }
                return data;
            }
            catch
            {
                return new List<PONCCLog>();
            }
        }
        private dynamic GetDataChange(dynamic oldData)
        {
            var oldDataLog = (List<PONCCLog>)oldData;
            var newDataLog = GetCurrentData();
            return new
            {
                Old = oldDataLog,
                New = newDataLog
            };
        }
        #endregion

        private void btnWarehouseDP_Click(object sender, EventArgs e)
        {
            RequestImport(6);
        }
    }

    public class PONCCLog
    {
        public int Id;
        public string Code;
        public List<string> SelectedDetailsId;
    }
}
