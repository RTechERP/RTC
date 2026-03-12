using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ExcelDataReader;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuotationKHDetail1 : _Forms
    {
        #region Variables
        public int IDDetail;
        public QuotationKHModel quotationKH = new QuotationKHModel();
        DataTable dtProduct = new DataTable();
        DataTable dtContact = new DataTable();
        List<int> lstDelete = new List<int>();
        DataTable dtProject;
        int warehouseID = 1;
        #endregion

        public frmQuotationKHDetail1()
        {
            InitializeComponent();
        }

        private void frmQuotationKHDetail_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                loadUser();
                loadCustomer();
                loadProject();
   
                loadContact();
                loadQuotationKHDetail();
                btnSave.Enabled = !quotationKH.IsApproved;
             
                cbProject.EditValueChanged += new EventHandler(cbProject_EditValueChanged);
              
            }
            finally
            {
                cGlobVar.LockEvents = false;
                
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
            if (quotationKH.AttachFile != null && quotationKH.AttachFile!= "")
            {
                string[] namefile = DocUtils.GetFilesList(quotationKH.AttachFile);
                cbFileName.DataSource = namefile;
                label9.Visible = btndelete.Visible = cbFileName.Visible = true;
            }
            DataTable dt = TextUtils.Select($"Select * From QuotationKHDetail where QuotationKHID={quotationKH.ID}");
            treeList1.DataSource = dt;
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
            cbCustomer.Properties.DisplayMember = "CustomerName";
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
            
        }

        /// <summary>
        /// click button thêm dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
           
        }

        /// <summary>
        /// click button xóa dòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    

        /// <summary>
        /// click button save new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {

           
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
            return true;
        }

        /// <summary>
        /// hàm tính FinishTotal
        /// </summary>
     
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

        

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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


        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadCode();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void ckCom_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtCom_EditValueChanged(object sender, EventArgs e)
        {
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
            
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

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
            int stt = 0;
            string code = string.Format($"RTC_QUO_{cbProject.Text}");
            string customer = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT CustomerShortName FROM dbo.Customer where ID = {cbCustomer.EditValue} ORDER BY CreatedDate DESC"));
            DataTable dt = TextUtils.Select($"Select * From QuotationKH where QuotationCode Like '%{code}%'");
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
            loadCode();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DocUtils.DeleteFile(quotationKH.AttachFile + "/" + cbFileName.Text);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            treeList1.Nodes.Add();
            treeList1.FocusedNode.Nodes.Add();

        }
    }
}
