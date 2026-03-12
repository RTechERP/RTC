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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuotationKHDetail : _Forms
    {
        #region Variables
        public int IDDetail;
        public QuotationKHModel quotationKH = new QuotationKHModel();
        DataTable dtProduct = new DataTable();
        DataTable dtContact = new DataTable();
        ArrayList lstDelete = new ArrayList();
        DataTable dtProject;

        Regex regex = new Regex(@"\D");
        int warehouseID = 0;
        #endregion

        public frmQuotationKHDetail()
        {
            InitializeComponent();
        }

        private void frmQuotationKHDetail_Load(object sender, EventArgs e)
        {
            try
            {
                this.colProductNewCode.ColumnEdit = null;
                cGlobVar.LockEvents = true;
                loadUser();
                loadCustomer();
                loadProject();
                loadProduct();
                loadContact();
                loadQuotationKHDetail();
                btnSave.Enabled = !quotationKH.IsApproved;
                this.cbProduct.EditValueChanged += new System.EventHandler(cbProduct_EditValueChange);
                cbProject.EditValueChanged += new EventHandler(cbProject_EditValueChanged);
                btnDele.Click += new EventHandler(btnDele_Click);
                repositoryItemHyperLinkEdit1.Click += new EventHandler(repositoryItemHyperLinkEdit1_Click);
            }
            finally
            {
                checkEdit1.Checked = false;
                cGlobVar.LockEvents = false;
                CheckStock();
                calculateFinishTotal();
            }
        }

        #region Methods
        private void loadQuotationKHDetail()
        {
            cboStatus.SelectedIndex = quotationKH.Status;
            txtPOCode.Text = quotationKH.POCode;
            cbProject.EditValue = quotationKH.ProjectID;
            txtCode.Text = quotationKH.QuotationCode;
            txtExplanation.Text = quotationKH.Explanation;
            cbCustomer.EditValue = quotationKH.CustomerID;
            cboUser.EditValue = quotationKH.UserID;
            cbContact.EditValue = quotationKH.ContactID;
            txtComMoney.Text = TextUtils.ToString(quotationKH.ComMoney);
            txtCom.Text = TextUtils.ToString(quotationKH.Commission);
            cbCompany.Text = TextUtils.ToString(quotationKH.Company);
            ckbMerge.Checked = quotationKH.IsMerge;
            txtQuotationDate.Text = TextUtils.ToString(quotationKH.QuotationDate);
            txtCreateDate.Text = TextUtils.ToString(quotationKH.CreateDate);
            if (quotationKH.AttachFile != null && quotationKH.AttachFile != "")
            {
                string[] namefile = DocUtils.GetFilesList(quotationKH.AttachFile);
                cbFileName.DataSource = namefile;
                label9.Visible = btndelete.Visible = cbFileName.Visible = true;
            }
            DataTable dt = TextUtils.Select($"Select * From QuotationKHDetail  where QuotationKHID={quotationKH.ID}");
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
        }
        /// <summary>
        /// Lấy danh sách dự án lên combo
        /// </summary>
        private void loadProject()
        {
            dtProject = TextUtils.Select("SELECT ID,ProjectCode,ProjectName,UserID,ContactID,CustomerID From Project");
            cbProject.Properties.DisplayMember = "ProjectCode";
            cbProject.Properties.ValueMember = "ID";
            cbProject.Properties.DataSource = dtProject;
        }

        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        private void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM dbo.Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }

        /// <summary>
        /// Lấy danh sách khách hàng lên combo chọn
        /// </summary>
        private void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 ORDER BY CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerShortName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }

        /// <summary>
        /// load người liên hệ
        /// </summary>
        private void loadContact()
        {
            dtContact = TextUtils.Select($"SELECT ContactPhone,ContactEmail,ContactName,ID FROM dbo.CustomerContact where CustomerID={cbCustomer.EditValue}");
            cbContact.Properties.DisplayMember = "ContactName";
            cbContact.Properties.ValueMember = "ID";
            cbContact.Properties.DataSource = dtContact;
        }

        #endregion

        #region Button events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// click button thêm dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;

            string group = "";

            DataTable dt = (DataTable)grdData.DataSource;
            if (dt.Rows.Count == 0)
            {
                STT = 1;
            }
            else
            {
                group = TextUtils.ToString(grvData.GetRowCellValue(grvData.RowCount - 1, colGroup));
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
            }
            DataRow dtrow = dt.NewRow();
            dtrow["STT"] = STT;
            dtrow["GroupQuota"] = group;
            //dt.Rows.InsertAt(dtrow, 0);
            dt.Rows.Add(dtrow);
            grdData.DataSource = dt;
        }

        /// <summary>
        /// click button xóa dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {

        }
        private void btnDele_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;
            if (quotationKH.IsApproved) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa mã sản phẩm [{0}] không?", productName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                lstDelete.Add(ID);
            }
        }

        /// <summary>
        /// click button save new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {

            saveData();
            cboStatus.Text = "";
            cboUser.Text = "";
            txtCode.Clear();
            cbProject.EditValue = -1;
            txtPOCode.Clear();
            txtTotalPrice.Text = "";
            cbCustomer.Text = "";
            txtExplanation.Clear();
            cbContact.EditValue = -1;
            txtContactPhone.Clear();
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
            quotationKH = new QuotationKHModel();
        }

        /// <summary>
        /// click button thêm mới khách hàng
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
        #endregion

        /// <summary>
        /// hàm save
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {

            if (!ValidateForm()) return false;
            if (lstDelete.Count > 0)
                QuotationKHDetailBO.Instance.Delete(lstDelete);
            folderPath = "BaoGia//" + txtCode.Text;
            if (attachFile(folderPath))
                quotationKH.AttachFile = folderPath;
            quotationKH.Status = cboStatus.SelectedIndex;
            quotationKH.QuotationCode = txtCode.Text.Trim();
            quotationKH.POCode = txtPOCode.Text.Trim();
            quotationKH.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.Text);
            quotationKH.CreateDate = TextUtils.ToDate(txtCreateDate.Value.ToString());
            quotationKH.QuotationDate = TextUtils.ToDate(txtQuotationDate.Value.ToString());
            quotationKH.CustomerID = TextUtils.ToInt(cbCustomer.EditValue);
            quotationKH.UserID = TextUtils.ToInt(cboUser.EditValue);
            quotationKH.ProjectID = TextUtils.ToInt(cbProject.EditValue);
            quotationKH.ContactID = TextUtils.ToInt(cbContact.EditValue);
            quotationKH.Month = TextUtils.ToInt(DateTime.Now.Month);
            quotationKH.Year = TextUtils.ToInt(DateTime.Now.Year);
            quotationKH.Commission = TextUtils.ToDecimal(txtCom.EditValue);
            quotationKH.ComMoney = TextUtils.ToDecimal(txtComMoney.Text);
            quotationKH.Company = TextUtils.ToString(cbCompany.Text);
            
            quotationKH.IsMerge = ckbMerge.Checked;

            if (quotationKH.ID == 0)
            {
                quotationKH.ID = (int)QuotationKHBO.Instance.Insert(quotationKH);
            }
            else
            {
                QuotationKHBO.Instance.Update(quotationKH);
            }
            DataTable dt = (DataTable)grdData.DataSource;
            dt.AcceptChanges();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                QuotationKHDetailModel detail = new QuotationKHDetailModel();
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.QuotationKHID = quotationKH.ID;
                detail.InternalCode = TextUtils.ToString(grvData.GetRowCellValue(i, colInternalCode));
                detail.InternalName = TextUtils.ToString(grvData.GetRowCellValue(i, colInternalName));
                detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                detail.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                detail.IntoMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colIntoMoney));
                detail.GiaNet = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colGiaNet));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.ProductID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCCode));
                detail.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                detail.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                detail.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, colMaker));
                detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                detail.GroupQuota = TextUtils.ToString(grvData.GetRowCellValue(i, colGroup));
                detail.ProductNewCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductNewCode));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.IsSelected = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsSelected));
                detail.TypeOfPrice = TextUtils.ToString(grvData.GetRowCellValue(i, colTypeOfPrice));

                detail.UnitPriceImport = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPriceImport));
                detail.TotalPriceImport = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceImport));
                if (detail.ID > 0)
                {
                    QuotationKHDetailBO.Instance.Update(detail);

                }
                else
                {
                    QuotationKHDetailBO.Instance.Insert(detail);
                }
            }
            return true;
        }

        /// <summary>
        /// ckeck lỗi
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã báo giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cbCustomer.EditValue) == 0)
            {
                MessageBox.Show("Xin hãy chọn một khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cboUser.EditValue) == 0)
            {
                MessageBox.Show("Xin hãy chọn một người phụ trách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái báo giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            DataTable dt = (DataTable)grdData.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(i, colProductNewCode));
                if (code == "")
                {
                    MessageBox.Show("Mã nội bộ không thể để trống, vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// hàm tính FinishTotal
        /// </summary>
        void calculateFinishTotal()
        {
            if (cGlobVar.LockEvents) return;
            decimal total = 0;
            decimal gianet = 0;
            decimal com = TextUtils.ToDecimal(txtCom.EditValue);
            grvData.FocusedRowHandle = -1;

            total = TextUtils.ToDecimal(colIntoMoney.SummaryItem.SummaryValue);
            gianet = TextUtils.ToDecimal(colGiaNet.SummaryItem.SummaryValue);
            txtTotalPrice.EditValue = total;
            if (ckCom.Checked)
                txtComMoney.EditValue = TextUtils.ToDecimal((total - gianet) * com);
            else
                txtComMoney.EditValue = TextUtils.ToDecimal((total) * com);
        }

        void loadProduct()
        {
            // if (cboGroup.Text == "") return;
            dtProduct = TextUtils.Select("SELECT * FROM ProductSale");
            cbProduct.DisplayMember = "ProductNewCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = dtProduct;

        }
        private void cbProduct_EditValueChange(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            grvData.Focus();
            txtCode.Focus();
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductCode));
            DataRow[] rows = dtProduct.Select("ID = " + ID);
            if (rows.Length > 0)
            {
                string productName = TextUtils.ToString(rows[0]["ProductName"]);
                string unit = TextUtils.ToString(rows[0]["Unit"]);
                string maker = TextUtils.ToString(rows[0]["Maker"]);
                grvData.SetFocusedRowCellValue(colProductName, productName);
                grvData.SetFocusedRowCellValue(colUnit, unit);
                grvData.SetFocusedRowCellValue(colMaker, maker);
            }
        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (cbProject.EditValue == null) return;
            DataRow[] row = dtProject.Select("ID=" + cbProject.EditValue);
            if (row.Length > 0)
            {
                cboUser.EditValue = row[0]["UserID"];
                cbCustomer.EditValue = row[0]["CustomerID"];
                cbContact.EditValue = row[0]["ContactID"];
            }
        }

        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            calculateFinishTotal();
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grvData.AddNewRow();
            grvData.FocusedColumn = grvData.VisibleColumns[0];
            grvData.Focus();
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cbContact_EditValueChanged(object sender, EventArgs e)
        {
            if (dtContact.Rows.Count == 0) return;
            DataRow[] row = dtContact.Select("ID=" + cbContact.EditValue);
            if (row.Length > 0)
            {
                txtContactPhone.Text = TextUtils.ToString(row[0]["ContactPhone"]);
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (cGlobVar.LockEvents) return;
            int qty = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQty));
            decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));
            decimal unitPriceImport = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPriceImport));
            
            if (e.Column == colQty || e.Column == colUnitPrice || e.Column == colUnitPriceImport)
            {
                grvData.SetFocusedRowCellValue(colIntoMoney, qty * unitPrice);
                grvData.SetFocusedRowCellValue(colTotalPriceImport, qty * unitPriceImport);
                calculateFinishTotal();
            }
            if (e.Column == colProductNewCode)
            {
                DataTable dt = (DataTable)cbProduct.DataSource;
                DataRow[] dtr = dt.Select($"ProductNewCode='{e.Value}'");
                if (dtr.Length == 0) return;
                grvData.SetRowCellValue(e.RowHandle, colProductRTCCode, dtr[0]["ID"]);
                grvData.SetRowCellValue(e.RowHandle, colProductName, dtr[0]["ProductName"]);
                grvData.SetRowCellValue(e.RowHandle, colProductCode, dtr[0]["ProductCode"]);
                grvData.SetRowCellValue(e.RowHandle, colMaker, dtr[0]["Maker"]);
                grvData.SetRowCellValue(e.RowHandle, colUnit, dtr[0]["Unit"]);

                //DataTable dt = TextUtils.Select($"SELECT * FROM ProductSale WHERE ProductNewCode = '{e.Value}'");
                //DataRow[] dtr = dt.Select($"ProductNewCode='{e.Value}'");
                //if (dt.Rows.Count == 0) return;
                //grvData.SetRowCellValue(e.RowHandle, colProductRTCCode, dt.Rows[0]["ID"]);
                //grvData.SetRowCellValue(e.RowHandle, colProductName, dt.Rows[0]["ProductName"]);
                //grvData.SetRowCellValue(e.RowHandle, colProductCode, dt.Rows[0]["ProductCode"]);
                //grvData.SetRowCellValue(e.RowHandle, colMaker, dt.Rows[0]["Maker"]);
                //grvData.SetRowCellValue(e.RowHandle, colUnit, dt.Rows[0]["Unit"]);
            }

        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadCode();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void ckCom_CheckedChanged(object sender, EventArgs e)
        {
            calculateFinishTotal();
        }

        private void txtCom_EditValueChanged(object sender, EventArgs e)
        {
            calculateFinishTotal();
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
        private void btnProject_Click(object sender, EventArgs e)
        {
            frmProjectDetail frm = new frmProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProject();
            }
        }
        DataSet ds;


        private void cbSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cGlobVar.LockEvents) return;
                DataTable dt = TextUtils.Select($"Select * From QuotationKHDetail where QuotationKHID={quotationKH.ID}");
                int sheet = TextUtils.ToInt(cbSheet.SelectedIndex);
                int check = TextUtils.ToInt(ds.Tables[sheet].Rows[0]["F1"]);
                if (check == 0)
                    ds.Tables[sheet].Rows.RemoveAt(0);
                for (int i = 0; i < ds.Tables[sheet].Rows.Count; i++)
                {
                    string newcode = TextUtils.ToString(ds.Tables[sheet].Rows[i]["F2"]);
                    string code = TextUtils.ToString(ds.Tables[sheet].Rows[i]["F3"]);
                    if (string.IsNullOrEmpty(newcode) && string.IsNullOrEmpty(code)) continue;
                    if (quotationKH.ID == 0 || grvData.RowCount == 0)
                        dt.Rows.Add();
                    dt.Rows[i]["STT"] = ds.Tables[sheet].Rows[i]["F1"];
                    dt.Rows[i]["ProductNewCode"] = ds.Tables[sheet].Rows[i]["F2"];
                    dt.Rows[i]["ProductCode"] = ds.Tables[sheet].Rows[i]["F3"];
                    dt.Rows[i]["ProductName"] = ds.Tables[sheet].Rows[i]["F4"];
                    dt.Rows[i]["InternalCode"] = ds.Tables[sheet].Rows[i]["F5"];
                    dt.Rows[i]["Maker"] = ds.Tables[sheet].Rows[i]["F6"];
                    dt.Rows[i]["Unit"] = ds.Tables[sheet].Rows[i]["F7"];
                    dt.Rows[i]["Qty"] = ds.Tables[sheet].Rows[i]["F8"];
                    dt.Rows[i]["UnitPrice"] = ds.Tables[sheet].Rows[i]["F9"];
                    dt.Rows[i]["IntoMoney"] = TextUtils.ToDecimal(ds.Tables[sheet].Rows[i]["F10"]);
                    dt.Rows[i]["GroupQuota"] = ds.Tables[sheet].Rows[i]["F11"];
                }
                grdData.DataSource = dt;
                CheckStock();
            }
            catch (Exception ex) { }
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colSTT && info.InColumnPanel)
                {
                    btnAdd_Click(null, null);
                }
            }
        }
        string folderPath;
        OpenFileDialog ofd = new OpenFileDialog();

        bool attachFile(string folderPath)
        {
            if (ofd.FileName == "") return false;
            //create folder and Uploadfile
            if (!DocUtils.CheckExits(folderPath))
            {
                DocUtils.CreateDirectory(folderPath);

            }
            foreach (string filename in ofd.FileNames)
            {
                DocUtils.UploadFileWithStatus(filename, folderPath);
            }
            return true;
        }
        void CheckStock()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCode));
                string name = TextUtils.ToString(grvData.GetRowCellValue(i, colProductName));
                string newcode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductNewCode));
                DataRow[] dtr = dtProduct.Select($"ProductNewCode='{newcode}'");
                if (dtr.Length == 0)
                    dtr = dtProduct.Select($"ProductCode='{code}'");
                if (dtr.Length > 0)
                {
                    grvData.SetRowCellValue(i, colProductNewCode, dtr[0]["ProductNewCode"]);
                    grvData.SetRowCellValue(i, colProductCode, dtr[0]["ProductCode"]);
                    grvData.SetRowCellValue(i, colProductName, dtr[0]["ProductName"]);
                }
            }
        }
        private void btnAttach_Click(object sender, EventArgs e)
        {
            ofd.Multiselect = true;
            ofd.ShowDialog();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                label8.Visible = cbSheet.Visible = true;
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                    var tablename = GetTablenames(ds.Tables);
                    cbSheet.DataSource = tablename;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void loadCode()
        {
            if (quotationKH.ID > 0) return;
            int stt = 0;
            string code = string.Format($"RTC_QUO_{cbProject.Text}");
            string customer = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT CustomerShortName FROM dbo.Customer where ID = {cbCustomer.EditValue} ORDER BY CreatedDate DESC"));
            DataTable dt = TextUtils.Select($"Select * From QuotationKH where QuotationCode Like '%{customer}%' Order by ID desc");
            if (dt.Rows.Count > 0)
            {
                string[] arr = dt.Rows[dt.Rows.Count - 1]["QuotationCode"].ToString().Split('_');
                stt = TextUtils.ToInt(arr[arr.Length - 1]);
                stt++;
            }
            txtCode.Text = string.Format($"RTC_QUO_{customer}_{txtCreateDate.Value.ToString("ddMMyyyy")}_{stt}");
        }
        private void cbProject_EditValueChanged_1(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            //loadCode();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DocUtils.DeleteFile(quotationKH.AttachFile + "/" + cbFileName.Text);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                grvData.CopyToClipboard();
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                int rowCount = grvData.RowCount - (grvData.FocusedRowHandle + 1);
                string[] arChar = { "\r\n" };
                string[] arrListStr = Clipboard.GetText().Split(arChar, StringSplitOptions.None);

                if (arrListStr.Length > rowCount)
                    for (int i = 0; i < (arrListStr.Length - rowCount - 1); i++)
                    {
                        btnAdd_Click(null, null);
                    }

                grvData.PasteFromClipboard();
            }
            if (e.KeyCode == Keys.Enter)
            {
                grvData.FocusedRowHandle++;
            }

        }

        private void grvData_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string checkvalue = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, colIntoMoney));
            if ((e.Column == colIntoMoney && checkvalue != "") || e.Column == colUnitPrice || e.Column == colUnitPriceImport || e.Column == colTotalPriceImport || e.Column == colQty)
            {
                decimal value1 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                decimal value2 = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == 0);
                return;
            }
            else
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == "" && value1 != "");
                return;
            }
        }

        private void ckbMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMerge.Checked)
            {
                grvData.OptionsView.AllowCellMerge = true;
                grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                colUnitPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                colGroup.GroupIndex = 0;
                grvData.ExpandAllGroups();

            }
            else
            {
                colGroup.GroupIndex = -1;
                grvData.OptionsView.AllowCellMerge = false;
                grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                colUnitPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            }
        }

        private void frmQuotationKHDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (frm != null)
                frm.Close();
        }

        private void cbProduct_Click(object sender, EventArgs e)
        {

        }
        void ReloadProduct(string signal)
        {
            loadProduct();
        }
        frmProductSale frm;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            frm = new frmProductSale();
            frm.GetSignal += ReloadProduct;
            if (frm.ShowDialog() == DialogResult.OK)
            {


            }
        }

        private void txtCreateDate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                this.colProductNewCode.Visible = false;
                this.colProductRTCCode.Visible = true;
                this.colProductRTCCode.VisibleIndex = 3;
            }
            else
            {
                this.colProductNewCode.Visible = true; 
                this.colProductRTCCode.Visible = false;
                this.colProductNewCode.VisibleIndex = 3;
            }
        }

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            int newcode = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCCode));
            DataRow[] dtr = dtProduct.Select($"ID={newcode}");
            if (dtr.Length > 0)
            {
                grvData.SetFocusedRowCellValue( colProductNewCode, dtr[0]["ProductCode"]);
                grvData.SetFocusedRowCellValue(colProductName, dtr[0]["ProductName"]);
                grvData.SetFocusedRowCellValue(colMaker, dtr[0]["Maker"]);
                grvData.SetFocusedRowCellValue(colUnit, dtr[0]["Unit"]);
            }
        }

        private void grvData_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            
        }

        private void grvData_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //if (grvData.FocusedColumn == colTypeOfPrice && grvData.FocusedRowHandle >= 0)
            //{
            //    if (string.IsNullOrEmpty(TextUtils.ToString(e.Value)))
            //    {
            //        return;
            //    }
            //    if (!regex.IsMatch(TextUtils.ToString(e.Value)))
            //    {
            //        e.Valid = false;
            //        e.ErrorText = "Bạn phải nhập đơn vị tiền tệ";
            //        return;
            //    }
            //}
        }
    }
}
