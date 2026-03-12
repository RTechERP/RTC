using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEditPerson : _Forms
    {
        public HistoryProductRTCModel _historyProductRTC;
        public string ProductCode;
        public string ProductName;
        public List<HistoryProductRTCModel> list = new List<HistoryProductRTCModel>();
        public frmEditPerson()
        {
            InitializeComponent();
        }

        private void frmSupplierDetail_Load(object sender, EventArgs e)
        {
            LoadCboUsser();
            loadData();
        }
        void LoadCboUsser()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "a", new string[] { "@UsersID", "@Status" }, new object[] { 0, 0 });
            cboUser.Properties.DataSource = dt;
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";


            DataTable dtOld = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "a", new string[] { "@UsersID" }, new object[] { 0 }); 
            cboOldUser.DisplayMember = "FullName";
            cboOldUser.ValueMember = "ID";
            cboOldUser.DataSource = dtOld;

        }
        /// <summary>
        /// Load dữ liệu vào form
        /// </summary>
        public void loadData()
        {
            txtName.Text = ProductCode;
            txtCode.Text = ProductName;
            txtProject.Text = list[0].Project.ToString();
            //cboOldUser.SelectedValue = _historyProductRTC.PeopleID;
            //richTextBox1.Text = _historyProductRTC.Note;
            cboOldUser.SelectedValue = list[0].PeopleID;
            richTextBox1.Text = list[0].Note;
        }
        /// <summary>
        /// Cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {

            int BillExportTechnicalID = 0;

            int peopleID = Lib.ToInt(cboUser.EditValue);
            if (peopleID <= 0)
            {
                MessageBox.Show("Vui lòng chọn Người mượn mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            //tạp phiếu xuất khi là sale BillExportTechDetail
            if (cbAddExport.Checked == true)
            {
                //Master
                BillExportTechnicalModel billExport = new BillExportTechnicalModel();
                billExport.Code = loadBilllNumber();
                billExport.Status = 0;
                billExport.ReceiverID = TextUtils.ToInt(cboUser.EditValue);
                //billExport.SupplierName = "Nhà cung cấp";//éo biết
                billExport.Deliver = TextUtils.ToString("Vũ Kim Ngân");
                billExport.ExpectedDate = list[0].DateReturnExpected; //Lấy ngày dự kiến trả của list[0]//ExpectedDate
                billExport.Receiver = cboUser.Text;
                billExport.CreatedDate = DateTime.Now;
                billExport.WarehouseType ="Demo";
                billExport.SupplierName =txtNCC.Text;
                if(cboBillType.SelectedIndex == 0)
                {
                    //billExport.BillType = false;
                    billExport.BillType = 0;
                }
                if (cboBillType.SelectedIndex == 1)
                {
                    billExport.BillType = 1;
                }
                if (cboBillType.SelectedIndex == 2)
                {
                    //billExport.BillType = false;
                    billExport.BillType = 2;
                }
                if (cboBillType.SelectedIndex == 3)
                {
                    billExport.BillType = 3;
                }
                billExport.ProjectName = txtProject.Text;
                billExport.CheckAddHistoryProductRTC = false;
                BillExportTechnicalID =(int)BillExportTechnicalBO.Instance.Insert(billExport);

                //Detail
                for (int i = 0; i < list.Count; i++)
                {
                    BillExportDetailTechnicalModel billExportDetail = new BillExportDetailTechnicalModel();
                    //billExportDetail.ID = BillExportTechnicalID;
                    billExportDetail.BillExportTechID = BillExportTechnicalID;
                    billExportDetail.ProductID = list[i].ProductRTCID;
                    billExportDetail.Quantity = TextUtils.ToInt(list[i].NumberBorrow);
                    billExportDetail.TotalQuantity = TextUtils.ToInt(list[i].NumberBorrow);
                    billExportDetail.Note = list[i].Note;
                    billExportDetail.STT = i + 1;

                    billExportDetail.ProductRTCQRCodeID = list[i].ProductRTCQRCodeID;

                    //billExportDetail.ProjectID = TextUtils.ToInt(grvDetailTechExport.GetRowCellValue(i, colMaker));
                    //billExportDetail.UnitID = list[i].
                    //billExportDetail.UnitName = TextUtils.ToString(grvDetailTechExport.GetRowCellDisplayText(i, colUnitName));
                    billExportDetail.HistoryProductRTCID = list[i].ID;
                    //billExportDetail.InternalCode = TextUtils.ToString(grvDetailTechExport.GetRowCellValue(i, colInternalCode));
                    BillExportDetailTechnicalBO.Instance.Insert(billExportDetail);
                }

            }

            

            string note = richTextBox1.Text.Trim();
            foreach (HistoryProductRTCModel item in list)
            {
                item.PeopleID = peopleID;
                item.Note = note;
                item.BillExportTechnicalID = BillExportTechnicalID;
                item.Project = txtProject.Text;
                if (item.ID > 0)
                {
                    StockBO.Instance.Update(item);
                }
            }
            //_historyProductRTC.PeopleID = Lib.ToInt(cboUser.EditValue);
            //_historyProductRTC.Note = richTextBox1.Text.Trim();
            //if (_historyProductRTC.ID > 0)
            //{
            //	StockBO.Instance.Update(_historyProductRTC);
            //}

          
            return true;
        }
        string loadBilllNumber()
        {
            string Bill = "";

            int so = 0;
            //string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            //string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            //string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);

            DateTime dtpCreatedDate = DateTime.Now;
            string month = TextUtils.ToString(dtpCreatedDate.Month);
            if (TextUtils.ToInt(month) < 10)
            {
                month = "0" + month;
            }

            string day = TextUtils.ToString(dtpCreatedDate.Day);
            if (TextUtils.ToInt(day) < 10)
            {
                day = "0" + day;
            }

            string year = TextUtils.ToString(dtpCreatedDate.Year).Substring(2);

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

            if (Billcode == "")
            {
                Bill = "PXKD" + date + "001";
                return Bill;
            }
            else
                so = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 4));

            if (so == 0)
            {
                Bill = "PXKD" + date + "001";
                return Bill;
            }
            else
            {
                string dem = TextUtils.ToString(so + 1);
                for (int i = 0; dem.Length < 4; i++)
                {
                    dem = "0" + dem;
                }
                Bill = "PXKD" + date + TextUtils.ToString(dem);
            }

            return Bill;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
                this.DialogResult = DialogResult.OK;
        }
        private void frmSupplierDetail_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cbAddExport_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAddExport.Checked==true)
            {
                cboBillType.Visible = true;
                label7.Visible = true;
                txtNCC.Visible = true;
            }
            else
            {
                cboBillType.Visible = false;
                label7.Visible = false;
                txtNCC.Visible = false;
            }
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
