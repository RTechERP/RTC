using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ExcelDataReader;
using Forms.Classes;
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
    public partial class frmBillImportKTDetail : _Forms
    {
        #region Variables
        public int IDDetail;
        public BillImportModel billImport = new BillImportModel();
        DataTable dtProductGroup;
        DataTable dtProduct = new DataTable();
        ArrayList lstIDDelete = new ArrayList();
        #endregion

        public frmBillImportKTDetail()
        {
            InitializeComponent();
        }

        private void frmBillImportDetail_Load(object sender, EventArgs e)
        {
            if (cbkType.Checked == false)
            {
                status = "PNK";
                lbSupplier.Text = "Nhà cung cấp :";
                lbUser.Text = "Người giao :";
            }
            else
            {
                status = "PT";
                lbSupplier.Text = "Bộ phận :";
                lbUser.Text = "Người trả :";
            }

            if (txtBilllNumber.Text == "")
            {
                loadBilllNumber();
            }
            loadProductGroup();
            loadKhoType();
            LoadSupplier();
            loadUser();
            loadProject();
            loadBillImportDetail();

            this.cbProduct.EditValueChanged += new EventHandler(cbProduct_EditValueChanged);

            btnSave.Enabled = btnSaveNew.Enabled = btnNewProduct.Enabled = !TextUtils.ToBoolean(billImport.Status);
        }

        #region Methods
        /// <summary>
        /// load group
        /// </summary>
        void loadProductGroup()
        {
            dtProductGroup = TextUtils.Select("SELECT * FROM ProductGroup where IsVisible = 1 ");
            cboGroup.Properties.DisplayMember = "ProductGroupName";
            cboGroup.Properties.ValueMember = "ID";
            cboGroup.Properties.DataSource = dtProductGroup;
        }

        /// <summary>
        /// load kho type
        /// </summary>
        void loadKhoType()
        {
            DataTable dtKhoType;
            dtKhoType = dtProductGroup.Copy();
            cbKhoType.Properties.DisplayMember = "ProductGroupName";
            cbKhoType.Properties.ValueMember = "ID";
            cbKhoType.Properties.DataSource = dtKhoType;
        }

        /// <summary>
        /// load nhà cung cấp
        /// </summary>
        void LoadSupplier()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM SupplierSale ");
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = dt;
        }

        /// <summary>
        /// load người nhập, người giao
        /// </summary>
        void loadUser()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM Users ");
            cboReciver.Properties.DisplayMember = cboDeliver.Properties.DisplayMember = "FullName";
            cboReciver.Properties.ValueMember = cboDeliver.Properties.ValueMember = "ID";
            cboReciver.Properties.DataSource = cboDeliver.Properties.DataSource = dt;
            
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
        /// load số phiếu
        /// </summary>
        void loadBilllNumber()
        {
            int code = 0;

            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;
            string billCode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT top 1 BillImportCode FROM BillImport where month(CreatedDate) ={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate) ={DateTime.Now.Day} ORDER BY ID DESC"));

            if (billCode.Contains("PT"))
            {
                billCode = billCode.Substring(2);
            }
            else if (billCode.Contains("PNK"))
            {

                billCode = billCode.Substring(3);
            }

            if (billImport.ID == 0)
            {
                if (billCode == "")
                {
                    txtBilllNumber.Text = status + date + "001";
                    return;
                }
                else
                    code = TextUtils.ToInt(billCode.Substring(billCode.Length - 3));

                if (code == 0)
                {
                    if (cbkType.Checked == true)
                    {
                        txtBilllNumber.Text = "PT" + date + "001";
                    }
                    else
                        txtBilllNumber.Text = "PNK" + date + "001";
                    return;
                }
                else
                {
                    string dem = TextUtils.ToString(code + 1);
                    for (int i = 0; dem.Length < 3; i++)
                    {
                        dem = "0" + dem;
                    }
                    if (cbkType.Checked == false)
                    {
                        txtBilllNumber.Text = "PNK" + date + TextUtils.ToString(dem);
                    }
                    else
                    {
                        txtBilllNumber.Text = "PT" + date + TextUtils.ToString(dem);
                    }
                }
            }

        }

        /// <summary>
        /// load mã sản phẩm, tên sp
        /// </summary>
        void loadProduct()
        {
            if (cboGroup.Text == "") return;
            string ID = TextUtils.ToString(cboGroup.EditValue);
            dtProduct = TextUtils.Select($"SELECT ID,ProductCode,ProductName,ItemType,Unit,AddressBox,NumberInStoreCuoiKy,Note,ProductNewCode FROM ProductSale where ProductGroupID IN ({ID})");
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;
            colProductID.ColumnEdit = cbProduct;
        }

        /// <summary>
        /// load Data
        /// </summary>
        void loadBillImportDetail()
        {
            if (billImport.ID > 0)
            {
                grvData.Focus();
                txtBilllNumber.Focus();
                cboGroup.SetEditValue(billImport.GroupID);
                txtBilllNumber.Text = billImport.BillImportCode;
                cbKhoType.Text = billImport.KhoType;
                cbkType.Checked = billImport.BillType == true;
                txtDateTime.Value =(DateTime) billImport.CreatDate;
                cboReciver.EditValue = billImport.ReciverID;
                cboDeliver.EditValue = billImport.DeliverID;
                cboSupplier.EditValue = billImport.SupplierID;
                cbKhoType.EditValue = billImport.KhoTypeID;
                if (billImport.BillImportCode.StartsWith("T") == true)
                {
                    lbSupplier.Text = "Bộ phận :";
                    lbUser.Text = "Người trả :";
                }
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { billImport.ID });
            grdData.DataSource = dt;
        }
        #endregion

        #region Buttons Events
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
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thêm phiếu nhập mới hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                saveData();
                statusOld = "";
                cboSupplier.Text = "";
                cboReciver.Text = "";
                cboDeliver.Text = "";
                cbKhoType.EditValue = "";
                cbKhoType.Text = "";
                cboGroup.EditValue = "";
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                billImport = new BillImportModel();
                loadBilllNumber();
            }
        }

        /// <summary>
        /// click button tạo thêm kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewType_Click(object sender, EventArgs e)
        {
            frmProductGroupDetail frm = new frmProductGroupDetail(0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadKhoType();
            }
        }

        /// <summary>
        /// click button xóa dòng trong grvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstIDDelete.Add(ID);
            }
        }

        #endregion



        /// <summary>
        /// hàm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvData.FocusedRowHandle = -1;
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
            catch (Exception)
            { }
        }

        /// <summary>
        /// hàm lưu thông tin
        /// </summary>
        bool saveData()
        {
            RecheckQty();
            cboReciver.Focus();
            grvData.Focus();
            if (!ValidateForm()) return false;
            billImport.BillImportCode = txtBilllNumber.Text.Trim();
            billImport.CreatDate =txtDateTime.Value;
            billImport.Deliver = cboDeliver.Text.Trim();
            billImport.Reciver = cboReciver.Text.Trim();
            billImport.Suplier = cboSupplier.Text.Trim();
            billImport.KhoType = cbKhoType.Text.Trim();
            billImport.DeliverID = TextUtils.ToInt(cboDeliver.EditValue);
            billImport.ReciverID = TextUtils.ToInt(cboReciver.EditValue);
            billImport.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            billImport.GroupID = TextUtils.ToString(cboGroup.EditValue);
            billImport.KhoTypeID = TextUtils.ToInt(cbKhoType.EditValue);
            billImport.BillType = cbkType.Checked;
            billImport.Status = false;
            if (cbkType.Checked == true)
            {
                billImport.BillType = true;
            }
            else
            {
                billImport.BillType = false;
            }

            if (billImport.ID > 0)
            {
                BillImportBO.Instance.Update(billImport);
            }
            else
            {
                billImport.ID = (int)BillImportBO.Instance.Insert(billImport);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                long ID = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                BillImportDetailModel detail = new BillImportDetailModel();

                if (ID > 0)
                {
                    detail = (BillImportDetailModel)(BillImportDetailBO.Instance.FindByPK(ID));
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.BillImportID = billImport.ID; //billImport.ID
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                detail.Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice));
                detail.ProjectCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode));
                //detail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectName));
                detail.SomeBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.TotalQty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalQty));
                detail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));

                if (detail.ID > 0)
                {
                    BillImportDetailBO.Instance.Update(detail);
                    if (lstIDDelete.Count > 0)
                        BillImportDetailBO.Instance.Delete(lstIDDelete);
                }
                else
                {
                    BillImportDetailBO.Instance.Insert(detail);
                }
            }
            return true;
        }
        /// <summary>
        /// Load lại số lượng
        /// </summary>
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
        /// <summary>
        /// hàm ckeck sản phẩm đã điền đủ hay không
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            DataTable dt;
            if (billImport.ID > 0)
            {
                string billCode = txtBilllNumber.Text.Trim();
                if (billCode.Contains("PT"))
                {
                    billCode = billCode.Substring(2);
                }
                else if (billCode.Contains("PNK"))
                {
                    billCode = billCode.Substring(3);
                }
                int strID = billImport.ID;
                dt = TextUtils.Select($"select top 1 ID from BillImport where BillImportCode LIKE '%{billCode}%' and ID <>{strID} ");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            else
            {
                if (billImport.ID == 0)
                {
                    dt = TextUtils.Select("select top 1 ID from BillImport where BillImportCode = '" + txtBilllNumber.Text.Trim() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        loadBilllNumber();
                        MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtBilllNumber.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            if (txtBilllNumber.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboSupplier.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền thông tin nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cboSupplier.Focus();
                return false;
            }
            if (cboReciver.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền thông tin người nhập.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cbKhoType.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn kho quản lý.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboGroup.Text == "")
            {
                MessageBox.Show("Xin hãy chọn nhóm.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cboDeliver.Text == "")
            {
                MessageBox.Show("Xin hãy điền thông tin người giao.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return false;
            }
            return true;
        }

        /// <summary>
        /// tắt form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        string status = "";
        string statusOld = "";
        int check = 0;
        private void cbkType_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkType.Checked == false)
            {
                status = "PNK";
                lbSupplier.Text = "Nhà cung cấp :";
                lbUser.Text = "Người giao :";
            }
            else
            {
                status = "PT";
                lbSupplier.Text = "Bộ phận :";
                lbUser.Text = "Người trả :";
            }
            if (check == 0)
            {
                check = 1;
                statusOld = status;
            }
            if (billImport.ID > 0)
            {
                status = statusOld;
            }
            loadBilllNumber();
        }

        /// <summary>
        /// giá trị thay đổi khi chọn trong group 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadProduct();
           
        }

        /// <summary>
        /// click button thêm nhà cung cấp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewSupplier_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplier();
            }
        }

        /// <summary>
        /// hàm thêm dòng trong grvData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;
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
            //dt.Rows.InsertAt(dtrow, 0);
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;

        }

        /// <summary>
        /// hàm thay đổi giá trị colTotalQty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ProductSaleModel productSaleModel = new ProductSaleModel();
            if (e.Column == colQty || e.Column == colProductID)
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
                float sum = 0;
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductID));
                    if (id == idSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(j, colQty));
                        sum += qty;
                    }
                }
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int idSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                    if (id == idSearch)
                    {
                        grvData.SetRowCellValue(i, colTotalQty, sum);
                    }
                }
            }
            if(e.Column == colProjectID)
            {
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int item = TextUtils.ToInt(grvData.GetRowCellValue(i,colProjectID));
                    if (item == 0)
                        grvData.SetRowCellValue(i, colProjectID, projectID);
                }
            }    
        }

        private void cbKhoType_EditValueChanged(object sender, EventArgs e)
        {
            cboGroup.EditValue = cbKhoType.EditValue;
            loadProduct();
            if(TextUtils.ToInt( cbKhoType.EditValue)==13)
            {
                cboDeliver.EditValue = 8;
                cboReciver.EditValue = 1146;
            }    
            else if( TextUtils.ToInt(cbKhoType.EditValue) == 14)
            {
                cboDeliver.EditValue = 6;
                cboReciver.EditValue = 1146;
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
            if (billImport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void txtDateTime_ValueChanged(object sender, EventArgs e)
        {
            if (billImport.ID == 0)
            {
                loadBilllNumber();
            }
        }

        private void getDataPONCC(List<int> lstID, string group, DataTable dt)
        {
            cboGroup.EditValue = group;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grvData.FocusedRowHandle = -1;
                btnAdd_Click(null, null);
                if (dt.Rows.Count > 0)
                {
                    string productName = TextUtils.ToString(dt.Rows[i]["ProductName"]);
                    string unit = TextUtils.ToString(dt.Rows[i]["Unit"]);
                    if (grvData.RowCount > 0)
                    {
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductID, dt.Rows[i]["ProductID"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProductName, productName);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colUnit, unit);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colQty, dt.Rows[i]["Qty"]);
                        grvData.SetRowCellValue(grvData.RowCount - 1, colProjectCode, dt.Rows[i]["ProjectModel"]);
                    }
                    else
                    {
                        grvData.SetRowCellValue(i, colProductID, dt.Rows[i]["ProductID"]);
                        grvData.SetRowCellValue(i, colProductName, productName);
                        grvData.SetRowCellValue(i, colUnit, unit);
                        grvData.SetRowCellValue(i, colQty, dt.Rows[i]["Qty"]);
                        grvData.SetRowCellValue(i, colProjectCode, dt.Rows[i]["ProjectModel"]);
                    }
                }
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            frmFollowProjectData frm = new frmFollowProjectData();
            frm.sendListID += getDataPONCC;
            frm.ShowDialog();
        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                grvData.CopyToClipboard();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                grvData.PasteFromClipboard();
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdData, grvData);
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
