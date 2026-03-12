using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Technical
{
    public partial class frmBillExportTechDetail : _Forms
    {
        public bool IsEdit;
        public BillExportTechnicalModel billExport = new BillExportTechnicalModel();
        ArrayList lstIDDelete = new ArrayList();
        string statusOld = "";
        public int IDDetail;

        DataTable dtProduct = new DataTable();
        int warehouseID = 0;
        public frmBillExportTechDetail()
        {
            InitializeComponent();
        }
        public frmBillExportTechDetail(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmBillExportTechDetail_Load(object sender, EventArgs e)
        {
            if (IsEdit)
            {
                btnSave.Enabled = btnAddProduct.Enabled = btnSaveNew.Enabled = btnAddProject.Enabled = false;
            }
            loadUser();
            cboBillType.SelectedIndex = 0;
            loadProduct();
            loadBillExportDetail();


        }
        void loadUser()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("Select * from Users");
            cbUser.Properties.DataSource = dt;
            cbUser.Properties.ValueMember ="ID";
            cbUser.Properties.DisplayMember = "FullName";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            loadBilllNumber();
            if (saveData())
            { this.DialogResult = DialogResult.OK; }
        }

        /// <summary>
        /// hàm save Data
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            RecheckQty();
            if (!ValidateForm()) return false;
            // focus: trỏ đến -> lưu và cất đi luôn
            grvDetailTechExport.Focus();
            txtCode.Focus();
            billExport.Status = 0;
            billExport.Code = txtCode.Text.Trim();
            //billExport.Addres = txtAddress.Text.Trim();
            //billExport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            billExport.SupplierName = TextUtils.ToString(txtNCC.Text);
            billExport.Deliver = TextUtils.ToString(txtLienHe.Text);
            billExport.ExpectedDate = dtpExpectedDate.Value;


            //billExport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
            billExport.Receiver = txtNguoiNhan.Text;
            billExport.ReceiverID = TextUtils.ToInt(cbUser.EditValue);
            billExport.CreatedDate = dtpCreatedDate.Value;
            billExport.CheckAddHistoryProductRTC = CkbAddHistoryProductRTC.Checked;
            billExport.ProjectName = txtProject.Text;
           
            // billExport.Image = picSign.ImageLocation;
            // billExport.CustomerName = TextUtils.ToString(txtCustomer.Text);

            if (cboBillType.SelectedIndex == 0)
            {
                billExport.BillType = false;
            }
            else if (cboBillType.SelectedIndex == 1)
            {
                billExport.BillType = true;
            }
            billExport.WarehouseType = txtWarehouseType.Text.Trim();

            if (billExport.ID > 0)
            {
                BillExportTechnicalBO.Instance.Update(billExport);
            }
            else
            {
                billExport.ID = (int)BillExportTechnicalBO.Instance.Insert(billExport);
            }
            for (int i = 0; i < grvDetailTechExport.RowCount; i++)
            {
                BillExportDetailTechnicalModel billExportDetail = new BillExportDetailTechnicalModel();
                billExportDetail.ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colID));
                billExportDetail.BillExportTechID = billExport.ID;//Liên kết bảng Nhập Xuất
                billExportDetail.ProductID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));//ID Sản phẩm
                billExportDetail.Quantity = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                billExportDetail.Note = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colNote));
                billExportDetail.STT = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colSTT));
                billExportDetail.TotalQuantity = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colTotalQuantity));
                billExportDetail.ProjectID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
                billExportDetail.UnitID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colUnitName));
                billExportDetail.UnitName = TextUtils.ToString(grvDetailTechExport.GetRowCellDisplayText(i, colUnitName));
                billExportDetail.HistoryProductRTCID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colHistoryProductRTCID));
                //billExportDetail.InternalCode = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colInternalCode));


                if (billExportDetail.ID > 0)
                {
                    BillExportDetailTechnicalBO.Instance.Update(billExportDetail);
                   
                }
                else
                {
                    BillExportDetailTechnicalBO.Instance.Insert(billExportDetail);
                }
            }
            if (lstIDDelete.Count > 0)
                BillExportDetailTechnicalBO.Instance.Delete(lstIDDelete);
            return true;
        }

        /// <summary>
        /// hàm kiểm tra thông tin nhập trước khi save
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            DataTable dt;
            if (billExport.ID > 0)
            {
                string Billcode = txtCode.Text.Trim();
                if (Billcode.Contains("PM"))
                {
                    Billcode = Billcode.Substring(2);
                }
                else if (Billcode.Contains("PXK") || Billcode.Contains("PCT"))
                {
                    Billcode = Billcode.Substring(3);
                }
                int strID = billExport.ID;
                dt = TextUtils.Select($"select top 1 ID from BillExport where Code LIKE '%{Billcode}%' and ID <> {strID}");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            else
            {
                dt = TextUtils.Select("select top 1 ID from BillExport where Code = '" + txtCode.Text.Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    loadBilllNumber();
                    MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtCode.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (txtNCC.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else if (cboCustomer.Text.Trim() == "" && cboCustomer.Enabled == true)
            //{
            //    MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            else if (txtLienHe.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn người liên hệ.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            else if (txtNguoiNhan.Text == "")
            {
                MessageBox.Show("Chưa có thông tin người nhận.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cboBillType.Text == "")
            {
                MessageBox.Show("Xin hãy chọn loại phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// hàm dùng load số phiếu
        /// </summary>
        void loadBilllNumber()
        {

            int so = 0;
            //string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            //string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            //string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);

            string month = TextUtils.ToString(dtpCreatedDate.Value.Month);
            if (TextUtils.ToInt(month) < 10)
            {
                month = "0" + month;
            }

            string day = TextUtils.ToString(dtpCreatedDate.Value.Day);
            if (TextUtils.ToInt(day) < 10)
            {
                day = "0" + day;
            }

            string year = TextUtils.ToString(dtpCreatedDate.Value.Year).Substring(2);

            string date = year + month + day;

            string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExportTechnical Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} ORDER BY ID DESC"));


            if (Billcode.Contains("PMD"))
            {
                Billcode = Billcode.Substring(3);
            }
            else if (Billcode.Contains("PXKD"))
            {
                Billcode = Billcode.Substring(4);
            }


            if (billExport.ID == 0)
            {
                if (Billcode == "")
                {
                    txtCode.Text = "PXKD" + date + "001";
                    return;
                }
                else
                    so = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 4));

                if (so == 0)
                {
                    if (cboBillType.SelectedIndex == 0)
                    {
                        txtCode.Text = "PXKD" + date + "001";
                    }
                    else
                        txtCode.Text = "PMD" + date + "001";
                    return;
                }
                else
                {
                    string dem = TextUtils.ToString(so + 1);
                    for (int i = 0; dem.Length < 4; i++)
                    {
                        dem = "0" + dem;
                    }
                    if (cboBillType.SelectedIndex == 0)
                    {
                        txtCode.Text = "PXKD" + date + TextUtils.ToString(dem);
                    }
                    else
                    {
                        txtCode.Text = "PMD" + date + TextUtils.ToString(dem);
                    }
                }
            }

        }
        /// <summary>
        /// load bill Export Detail
        /// </summary>
        private void loadBillExportDetail()
        {
            if (billExport.ID > 0)
            {

                txtCode.Text = billExport.Code;

                txtNCC.Text = billExport.SupplierName;
                txtProject.Text = billExport.ProjectName;
                txtLienHe.Text = billExport.Deliver;
                txtNguoiNhan.Text = billExport.Receiver;
                CkbAddHistoryProductRTC.Checked = billExport.CheckAddHistoryProductRTC;
                cbUser.EditValue = billExport.ReceiverID;


                cboBillType.SelectedIndex = billExport.BillType ? 1 : 0;

                //if (!billExport.BillType)
                //{
                //    cboBillType.SelectedIndex = 0;
                //}
                //else if (billExport.BillType)
                //{
                //    cboBillType.SelectedIndex = 1;
                //}
            }
            else
            {
                loadBilllNumber();
            }

            DataTable dt = TextUtils.GetDataTableFromSP("spGetBillExportTechDetail", new string[] { "@ID" }, new object[] { billExport.ID });
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            dtpCreatedDate.Text = TextUtils.ToString(billExport.CreatedDate);
        }


        /// <summary>
        /// giá trị cột TotalQty thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQuantity || e.Column == colProductID)
            {
                int ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(e.RowHandle, colProductID));
                float sum = 0;
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvDetailTechExport.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvDetailTechExport.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
            if (e.Column == colMaker)
            {
                int projectID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colMaker));
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    int item = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
                    if (item == 0)
                        grvDetailTechExport.SetRowCellValue(i, colMaker, projectID);
                }
            }
        }
        void RecheckQty()
        {
            for (int k = 0; k < grvDetailTechExport.RowCount; k++)
            {
                int ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(k, colProductID));
                float sum = 0;
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }
                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvDetailTechExport.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvDetailTechExport.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
        }


        /// <summary>
        /// hàm dùng để chọn sản phẩm
        /// </summary>
        private void loadProduct()
        {
            //DataTable dt = new DataTable();
            dtProduct = TextUtils.Select("select * from ProductRTC  ");
            cboProduct.DisplayMember = "ProductCode";
            cboProduct.ValueMember = "ID";
            cboProduct.DataSource = dtProduct;
            colProductID.ColumnEdit = cboProduct;

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (billExport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void grvDetailTechExport_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvDetailTechExport.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvDetailTechExport);
            }
        }

        private void grvDetailTechExport_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQuantity || e.Column == colProductID)
            {
                int ID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(e.RowHandle, colProductID));
                float sum = 0;
                for (int i = 0; i < grvDetailTechExport.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvDetailTechExport.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvDetailTechExport.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvDetailTechExport.SetRowCellValue(j, colTotalQuantity, sum);
                    }
                }
            }
            //if (e.Column == colMaker)
            //{
            //    int projectID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colMaker));
            //    for (int i = 0; i < grvDetailTechExport.RowCount; i++)
            //    {
            //        int item = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
            //        if (item == 0)
            //            grvDetailTechExport.SetRowCellValue(i, colMaker, projectID);
            //    }
            //}
        }

        private void cboBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBilllNumber();
            //if (cboBillType.SelectedIndex == 0)
            //{
            //    cboSupplier.Enabled = true;
            //    cboCustomer.Enabled = false;
            //    cboCustomer.Text = "";
            //}
            //else
            //{
            //    cboCustomer.Enabled = true;
            //    cboSupplier.Enabled = false;
            //    cboSupplier.EditValue = "";
            //}
        }

        private void cboBillType_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu xuất mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                statusOld = "";
                //txtAddress.Clear();
                cboBillType.Text = "";
                txtNguoiNhan.Text = "";
                txtLienHe.Text = "";
                txtNCC.Text = "";

                for (int i = grvDetailTechExport.RowCount - 1; i >= 0; i--)
                {
                    grvDetailTechExport.DeleteRow(i);
                }
                billExport = new BillExportTechnicalModel();
                loadBilllNumber();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailRTC frm = new frmProductDetailRTC(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }
        DataTable dtXuat = new DataTable();
        private void btnAddProject_Click(object sender, EventArgs e)
        {
            List<List<int>> ListGrv = new List<List<int>>();
            frmProductHistory frm = new frmProductHistory();
            frm.IsbtnXuat = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ListGrv = frm.ListGrv;
            }
            if (ListGrv.Count > 0)
            {
                int row = grvDetailTechExport.RowCount;
                for (int i = 0; i < ListGrv.Count; i++)
                {
                    MyLib.AddNewRow(grdData, grvDetailTechExport);
                    grvDetailTechExport.SetRowCellValue(i + row, colProductID, ListGrv[i][0]);
                    SetValueCol(ListGrv[i][0], i + row, ListGrv[i][1], ListGrv[i][2]);
                }
            }
        }


        private void btnDelete1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colID));
            string productCode = TextUtils.ToString(grvDetailTechExport.GetFocusedRowCellDisplayText(colProductID));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvDetailTechExport.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
        }

        private void dtpCreatedDate_ValueChanged(object sender, EventArgs e)
        {
            //loadBilllNumber();
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            txtCode.Focus();
            grvDetailTechExport.Focus();
            int id = TextUtils.ToInt(grvDetailTechExport.GetFocusedRowCellValue(colProductID));
            int index = grvDetailTechExport.FocusedRowHandle;
            for (int i = 0; i < grvDetailTechExport.RowCount; i++)
            {
                if (i != index)
                {
                    if (TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colProductID)) == id)
                    {
                        return;
                    }
                }
            }
            DataRow[] rows = dtProduct.Select("ID = " + id);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string productCodeRTC = TextUtils.ToString(rows[0]["ProductCodeRTC"]);
                string maker = TextUtils.ToString(rows[0]["Maker"]);
                int idUnitCount = TextUtils.ToInt(rows[0]["UnitCountID"]);
                int NumberInStore = TextUtils.ToInt(rows[0]["NumberInStore"]);
                int Number = TextUtils.ToInt(rows[0]["Number"]);
                string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));




                grvDetailTechExport.SetFocusedRowCellValue(colProductName, productName);
                grvDetailTechExport.SetFocusedRowCellValue(colUnitName, unitName);
                grvDetailTechExport.SetFocusedRowCellValue(colInternalCode, productCodeRTC);
                grvDetailTechExport.SetFocusedRowCellValue(colMaker, maker);
                grvDetailTechExport.SetFocusedRowCellValue(colUnitID, idUnitCount);
                grvDetailTechExport.SetFocusedRowCellValue(colNumberInStore, NumberInStore);
                grvDetailTechExport.SetFocusedRowCellValue(colNumber, Number);
                grvDetailTechExport.FocusedColumn = colQuantity;

            }

        }

        void SetValueCol(int ProductRTCID, int rowNumber, int NumberBorrow, int HistoryProductRTCID)
        {
            DataRow[] rows = dtProduct.Select("ID = " + ProductRTCID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string productCodeRTC = TextUtils.ToString(rows[0]["ProductCodeRTC"]);
                string maker = TextUtils.ToString(rows[0]["Maker"]);
                int idUnitCount = TextUtils.ToInt(rows[0]["UnitCountID"]);
                string unitName = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT UnitCountName FROM UnitCountKT WHERE ID = {idUnitCount}"));



                grvDetailTechExport.SetRowCellValue(rowNumber, colProductName, productName);
                grvDetailTechExport.SetRowCellValue(rowNumber, colUnitName, unitName);
                grvDetailTechExport.SetRowCellValue(rowNumber, colInternalCode, productCodeRTC);
                grvDetailTechExport.SetRowCellValue(rowNumber, colMaker, maker);
                grvDetailTechExport.SetRowCellValue(rowNumber, colUnitID, idUnitCount);
                grvDetailTechExport.SetRowCellValue(rowNumber, colQuantity, NumberBorrow);
                grvDetailTechExport.SetRowCellValue(rowNumber, colHistoryProductRTCID, HistoryProductRTCID);
                // grvDetailTechExport.FocusedColumn = colQuantity;

                grvDetailTechExport.FocusedRowHandle = -1;
            }
        }



        private void grvDetailTechExport_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            int Number = TextUtils.ToInt(view.GetFocusedRowCellValue(colNumber));
            int NumberInStore = TextUtils.ToInt(view.GetFocusedRowCellValue(colNumberInStore));
            if (view.FocusedColumn.FieldName == "Quantity")
            {
                if (TextUtils.ToInt(e.Value) > NumberInStore)
                {
                    e.Valid = false;
                    e.ErrorText = $"Số lượng tồn kho chỉ còn {NumberInStore} !";
                    Show();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
