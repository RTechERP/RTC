using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
//using ExcelDataReader;
//using MSScriptControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBillExportKTDetail : _Forms
    {
        #region Variables
        public int IDDetail;
        public BillExportModel billExport = new BillExportModel();
        DataTable dtCustomer = new DataTable();
        DataTable dtProduct = new DataTable();
        ArrayList lstIDDelete = new ArrayList();
        public BillImportModel billImport = new BillImportModel();
        int warehouseID = 1;
        #endregion

        public frmBillExportKTDetail()
        {
            InitializeComponent();
        }

        private void frmBillExportDetail_Load(object sender, EventArgs e)
        {
            loadProductGroup();
            loadCustomer();
            loadSender();
            loadUsers();
            loadKhoType();
            loadProject();
            loadProductType();
            if (billImport.ID == 0)
            {
                loadBillExportDetail();
            }
            else
            {
                loadBillImportDetail();
            }
            if (txtCode.Text == "")
            {
                loadBilllNumber();
            }

            this.cbProduct.EditValueChanged += new System.EventHandler(cboProduct_EditValueChanged);


            // KHI ĐƯỢC duyệt thì sẽ ẩn các button 
            btnSaveNew.Enabled = btnSave.Enabled = btnNewProduct.Enabled = !TextUtils.ToBoolean(billExport.IsApproved);
            cboStatus.SelectedIndex = 2;
        }

        #region Methods
        /// <summary>
        /// load bill Export Detail
        /// </summary>
        private void loadBillExportDetail()
        {
            cboGroup.SetEditValue(billExport.GroupID);
            cboStatus.SelectedIndex = TextUtils.ToInt(billExport.Status);
            //cbKhoType.Text = billExport.WarehouseType;
            txtCode.Text = billExport.Code;
            txtAddress.Text = billExport.Address;
            cboCustomer.EditValue = billExport.CustomerID;
            cboUser.EditValue = billExport.UserID;
            cboSender.EditValue = billExport.SenderID;
            cbKhoType.EditValue = billExport.KhoTypeID;
            cbProductType.EditValue = billExport.ProductType;
            ckbMerge.Checked = TextUtils.ToBoolean(billExport.IsMerge);
            // load data detail
            DataTable dt = TextUtils.GetDataTableFromSP("spGetBillExportDetail", new string[] { "@BillID" }, new object[] { billExport.ID });
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtDateTime.Text = TextUtils.ToString(billExport.CreatDate);
        }
        /// <summary>
        /// convert import->export
        /// </summary>
        void loadBillImportDetail()
        {
            cboGroup.SetEditValue(billImport.GroupID);
            DataTable dtconvert = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
            dtconvert.Columns.Add("ProductFullName");
            dtconvert.Columns.Remove("ID");
            grdData.DataSource = dtconvert;
        }
        /// <summary>
        /// load nhóm
        /// </summary>
        void loadProductGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup where IsVisible=1 ");
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            cboGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load kho type
        /// </summary>
        void loadKhoType()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            cbKhoType.Properties.DisplayMember = "ProductGroupName";
            cbKhoType.Properties.ValueMember = "ID";
            cbKhoType.Properties.DataSource = dt;
        }

        void loadProductType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("ProductType");
            dt.Rows.Add("1", "Hàng thương mại");
            dt.Rows.Add("2", "Hàng dự án");
            cbProductType.Properties.DisplayMember = "ProductType";
            cbProductType.Properties.ValueMember = "ID";
            cbProductType.Properties.DataSource = dt;
        }

        /// <summary>
        /// load khách hàng
        /// </summary>

        private void loadCustomer()
        {
            dtCustomer = new DataTable();
            dtCustomer = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dtCustomer;
        }

        /// <summary>
        /// load nhân viên, người giao
        /// </summary>
        public void loadUsers()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }
        public void loadSender()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM Users");
            cboSender.Properties.DisplayMember = "FullName";
            cboSender.Properties.ValueMember = "ID";
            cboSender.Properties.DataSource = dt;
        }
        /// <summary>
        /// lựa chọn mã dự án
        /// </summary>
        private void loadProject()
        {
            DataTable dt = TextUtils.Select("SELECT ID,ProjectCode,ProjectName FROM Project");
            cbProject.DisplayMember = "ProjectName";
            cbProject.ValueMember = "ID";
            cbProject.DataSource = dt;
            colProjectID.ColumnEdit = cbProject;
        }

        /// <summary>
        /// hàm dùng load số phiếu
        /// </summary>
        void loadBilllNumber()
        {
            int so = 0;
            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;
            string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 Code FROM BillExport Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} ORDER BY ID DESC"));

            if (Billcode.Contains("PM"))
            {

                Billcode = Billcode.Substring(2);
            }
            else if (Billcode.Contains("PXK") || Billcode.Contains("PCT"))
            {

                Billcode = Billcode.Substring(3);
            }

            if (billExport.ID == 0)
            {
                if (Billcode == "") // ktra tháng bdau và tháng đc update
                {
                    txtCode.Text = "PM" + date + "001";
                    return;
                }
                else
                    so = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 3)); // tách lấy 3 số cuối convert sang int
                string dem = TextUtils.ToString(so + 1);
                for (int i = 0; dem.Length < 3; i++)
                {
                    dem = "0" + dem;
                }
                if (cboStatus.SelectedIndex == 0)
                {
                    txtCode.Text = "PM" + date + TextUtils.ToString(dem);
                }
                else if (cboStatus.SelectedIndex == 3)
                {
                    txtCode.Text = "PCT" + date + TextUtils.ToString(dem);
                }
                else
                {
                    txtCode.Text = "PXK" + date + TextUtils.ToString(dem);
                }
            }
        }
        #endregion

        /// <summary>
        /// hàm dùng để chọn sản phẩm
        /// </summary>
        private void loadProduct()
        {
            if (cboGroup.Text == "") return;
            string ID = TextUtils.ToString(cboGroup.EditValue);
            dtProduct = TextUtils.LoadDataFromSP("spGetProductSale", "A", new string[] { "@IDgroup" }, new object[] { ID });
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
            colProductID.ColumnEdit = cbProduct;
        }

        // click vào khách hàng để tự động hiển thị ra địa chỉ
        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (dtCustomer.Rows.Count <= 0) return;
            if (cboCustomer.Text.Trim() == "") return;
            DataRow[] dr = dtCustomer.Select($"ID={cboCustomer.EditValue}");
            txtAddress.Text = TextUtils.ToString(dr[0]["Address"]);
            loadAddressStock();
        }
        void loadAddressStock()
        {
     
        }
        //khi chọn cboName -> tự động sinh ra tên,ĐVT
        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            grvData.Focus();
            txtCode.Focus();
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            DataRow[] rows = dtProduct.Select("ID = " + ID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string productNewCode = TextUtils.ToString(rows[0]["ProductNewCode"]);
                string unit = TextUtils.ToString(rows[0]["Unit"]);
                grvData.SetFocusedRowCellValue(colProductName, productName);
                grvData.SetFocusedRowCellValue(colProductNewCode, productNewCode);
                grvData.SetFocusedRowCellValue(colUnit, unit);
            }
        }

        #region Buttons Events
        /// <summary>
        /// click button lưu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            loadBilllNumber();
            if (saveData())
            { this.DialogResult = DialogResult.OK; }
        }

        /// <summary>
        /// click button để thêm khách hàng  mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }

        /// <summary>
        /// click button để thêm kho  mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewWarehouse_Click(object sender, EventArgs e)
        {

            frmProductGroupDetail frm = new frmProductGroupDetail(0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadKhoType();
            }
        }

        /// <summary>
        /// click button để thêm dòng mới trong bảng dgvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;

            // khi click STT tự động tăng
            if (dt.Rows.Count == 0)
            {
                STT = 1;
            }
            else
            {
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
            }
            DataRow dtrow = dt.NewRow();
            dtrow["STT"] = STT;
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;
        }

        /// <summary>
        /// click button để xóa dòng trong dgvData
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
                return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colProductID));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(strID);
            }
        }

        /// <summary>
        /// click button thêm sản phẩm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }

        /// <summary>
        /// click button lưu thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu xuất mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                statusOld = "";
                txtAddress.Clear();
                cboStatus.Text = "";
                cboUser.Text = "";
                cboSender.Text = "";
                cbKhoType.Text = "";
                cboCustomer.Text = "";
                cboStatus.SelectedIndex = -1;
                cbKhoType.EditValue = "";
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                billExport = new BillExportModel();
                //loadBilllNumber();
            }
        }
        #endregion

        /// <summary>
        /// hàm save Data
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            RecheckQty();
            if (!ValidateForm()) return false;
            // focus: trỏ đến -> lưu và cất đi luôn
            grvData.Focus();
            txtCode.Focus();
            billExport.TypeBill = false;
            billExport.Code = txtCode.Text.Trim();
            billExport.Address = txtAddress.Text.Trim();
            billExport.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            billExport.UserID = TextUtils.ToInt(cboUser.EditValue);
            billExport.SenderID = TextUtils.ToInt(cboSender.EditValue);
            billExport.CreatDate = txtDateTime.Value;
            billExport.Status = cboStatus.SelectedIndex;
            billExport.WarehouseType = cbKhoType.Text.Trim();
            billExport.GroupID = TextUtils.ToString(cboGroup.EditValue);
            billExport.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            billExport.ProductType = TextUtils.ToInt(cbProductType.EditValue);
          
            billExport.IsMerge = TextUtils.ToBoolean(ckbMerge.Checked);
            if (billExport.ID > 0)
            {
                BillExportBO.Instance.Update(billExport);
            }
            else
            {
                billExport.ID = (int)BillExportBO.Instance.Insert(billExport);
            }
            for (int i = 0; i < grvData.RowCount; i++)
            {
                BillExportDetailModel billExportDetail = new BillExportDetailModel();
                string productName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                if (productName.Trim() == "") continue;
                billExportDetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                billExportDetail.BillID = billExport.ID;//Liên kết bảng Nhập Xuất
                billExportDetail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));//ID Sản phẩm
                billExportDetail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                billExportDetail.ProductFullName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductFullName));
                //billExportDetail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectID));
                billExportDetail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                billExportDetail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                billExportDetail.TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty));
                billExportDetail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                billExportDetail.ProductType = TextUtils.ToInt(cbProductType.EditValue);
                billExportDetail.ProductType = TextUtils.ToInt(cbProductType.EditValue);
                billExportDetail.POKHID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHID));
                billExportDetail.GroupExport = TextUtils.ToString(grvData.GetRowCellValue(i, colGroup));
                if (billExportDetail.ID > 0)
                {
                    BillExportDetailBO.Instance.Update(billExportDetail);
                    if (lstIDDelete.Count > 0)
                        BillExportDetailBO.Instance.Delete(lstIDDelete);
                }
                else
                {
                    BillExportDetailBO.Instance.Insert(billExportDetail);
                }
            }
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
            else if (cboCustomer.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboUser.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn nhân viên.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cbKhoType.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn kho quản lý.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboGroup.Text == "")
            {
                MessageBox.Show("Xin hãy chọn nhóm.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cboSender.Text == "")
            {
                MessageBox.Show("Xin hãy chọn người giao.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (cboStatus.Text == "")
            {
                MessageBox.Show("Xin hãy chọn trạng thái.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        /// <summary>
        /// trạng thái được thay đổi khi chọn trong cboTrangThai
        /// </summary>

        string statusOld = "";
        int check = 0;
        private void cboStatus_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// giá trị cbName được chỉnh sửa khi cboGroup thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadProduct();
        }

        /// <summary>
        /// giá trị cột TotalQty thay đổi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQty || e.Column == colProductID)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colProductID));
                float sum = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
                        sum += qty;
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQty, sum);
                    }
                }
            }
            if (e.Column == colProjectID)
            {
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                    if (item == 0)
                        grvData.SetRowCellValue(i, colProjectID, projectID);
                }
            }
        }
        void RecheckQty()
        {
            for (int k = 0; k < grvData.RowCount; k++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(k, colProductID));
                float sum = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQty));
                        sum += qty;
                    }
                }
                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQty, sum);
                    }
                }
            }
        }
        private void frmGoodsDeliveryNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        string status;
        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((txtCode.Text.Contains("PXK") || txtCode.Text.Contains("PCT")) && cboStatus.SelectedIndex == 0)
            {
                if (txtCode.Text.Contains("PCT"))
                    txtCode.Text = txtCode.Text.Replace("PCT", "PM");
                else
                    txtCode.Text = txtCode.Text.Replace("PXK", "PM");
            }
            else if ((txtCode.Text.Contains("PM") || txtCode.Text.Contains("PXK")) && cboStatus.SelectedIndex == 3)
            {
                if (txtCode.Text.Contains("PM"))
                    txtCode.Text = txtCode.Text.Replace("PM", "PCT");
                else
                    txtCode.Text = txtCode.Text.Replace("PXK", "PCT");
            }
            else
            {
                if (txtCode.Text.Contains("PM"))
                    txtCode.Text = txtCode.Text.Replace("PM", "PXK");
                else
                    txtCode.Text = txtCode.Text.Replace("PCT", "PXK");
            }
        }
        private void btnProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (billExport.ID == 0)
            {
                loadBilllNumber();
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtDateTime_ValueChanged(object sender, EventArgs e)
        {
            if (billExport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void grvData_LoadData(List<int> lstID, string group)
        {

            cboGroup.EditValue = group;
            for (int i = 0; i < lstID.Count; i++)
            {
                grvData.FocusedRowHandle = -1;
                btnNew_Click(null, null);
                DataRow[] rows = dtProduct.Select($"ID = { lstID[i]}");
                if (rows.Length > 0)
                {
                    string productName = TextUtils.ToString(rows[0]["ProductName"]);
                    string unit = TextUtils.ToString(rows[0]["Unit"]);
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductID, lstID[i]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, productName);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, unit);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, colProductID, lstID[i]);
                        grvData.SetRowCellValue(i, colProductName, productName);
                        grvData.SetRowCellValue(i, colUnit, unit);
                    }
                }
            }

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }



        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 41)
            {
                MyLib.AddNewRow(grdData, grvData);
            }
        }
        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if ( e.Column == colQty)
            {
                decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == 0);
                e.Handled = true;
                return;
            }
            else
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == "");
                e.Handled = true;
                return;
            }

        }
        private void ckbMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMerge.Checked)
            {
                grvData.OptionsView.AllowCellMerge = true;
                colGroup.GroupIndex = 0;
                grvData.ExpandAllGroups();
                grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            }
            else
            {
                colGroup.GroupIndex = -1;
                grvData.OptionsView.AllowCellMerge = false;
                grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
            }

        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cbKhoType_EditValueChanged(object sender, EventArgs e)
        {
            cboGroup.EditValue = cbKhoType.EditValue;
            loadProduct();
            if (TextUtils.ToInt(cbKhoType.EditValue) == 13)
            {
                cboSender.EditValue = 1146;
            }
            else if (TextUtils.ToInt(cbKhoType.EditValue) == 14)
            {
                cboSender.EditValue = 1146;
            }
        }
    }
}
