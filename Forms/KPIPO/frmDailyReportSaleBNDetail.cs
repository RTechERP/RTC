using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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

namespace BMS
{
    public partial class frmDailyReportSaleBNDetail : _Forms
    {
        public int warehouseID = 0;

        public DailyReportSaleModel dailyReportSaleModel = new DailyReportSaleModel();
        ArrayList lstIDDelete = new ArrayList();

        public frmDailyReportSaleBNDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        private void frmDailyReportSaleBNDetail_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            LoadGroup();
            LoadUsers();
            LoadContact();
            LoadDailyReportDetail();
        }
        #region LoadCombo 
        private void LoadCustomer()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p=>p.CreatedDate).ToList();
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";
            cboCustomer.DataSource = list;
        }
        private void LoadUsers()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboUser.DisplayMember = "FullName";
            cboUser.ValueMember = "UserID";
            cboUser.DataSource = dt;
        }
        private void LoadContact()
        {
            //List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(p => p.CreatedDate).ToList();
            DataTable dt = TextUtils.LoadDataFromSP("spGetContactCustomerBN", "A", new string[] { "@CustomerID" }, new object[] { 0 });
            cboContact.DisplayMember = "ContactName";
            cboContact.ValueMember = "ID";
            cboContact.DataSource = dt;
        }
        void LoadGroup()
        {
            List<MainIndexModel> list = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { "@Type" }, new object[] {2 });
            cboGroup.DisplayMember = "MainIndex";
            cboGroup.ValueMember = "ID";
            cboGroup.DataSource = list;
        }
        #endregion
        #region event button 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {

            if (SaveData())
            {
                for (int i = grvData.RowCount - 1; i >= 0; i--)
                {
                    grvData.DeleteRow(i);
                }
                dailyReportSaleModel = new DailyReportSaleModel();
            }
        }
        #endregion
        #region Validate 
        private bool ValidateForm()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colResult))))
                {
                    MessageBox.Show("Vui lòng nhập kết quả");
                    return false;
                }
                if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colContent))))
                {
                    MessageBox.Show("Vui lòng nhập nội dung báo cáo");
                    return false;
                }
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colContactID)) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn người liên hệ");
                    return false;
                }
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colCustomerID)) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng");
                    return false;
                }
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colGroupTypeID)) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn loại báo cáo");
                    return false;
                }
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID)) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn người phụ trách");
                    return false;
                }
                if (!TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateStart)).HasValue)
                {
                    MessageBox.Show("Vui lòng nhập ngày báo cáo");
                    return false;
                }
            }
            return true;
        }

        #endregion
        #region Save data 
        private bool SaveData()
        {

            grvData.CloseEditor();
            if (!ValidateForm()) return false;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                long ID = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                DailyReportSaleModel dailyReportSaleModel = new DailyReportSaleModel();
                if (ID > 0)
                {
                    dailyReportSaleModel = (DailyReportSaleModel)DailyReportSaleBO.Instance.FindByPK(ID);
                }
                dailyReportSaleModel.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                dailyReportSaleModel.DateStart = dailyReportSaleModel.DateEnd = TextUtils.ToDate(grvData.GetRowCellValue(i, colDateStart).ToString());
                dailyReportSaleModel.UserID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
                dailyReportSaleModel.GroupType = TextUtils.ToInt(grvData.GetRowCellValue(i, colGroupTypeID));
                dailyReportSaleModel.CustomerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCustomerID));
                dailyReportSaleModel.ContacID = TextUtils.ToInt(grvData.GetRowCellValue(i, colContactID));
                dailyReportSaleModel.Content = TextUtils.ToString(grvData.GetRowCellValue(i, colContent));
                dailyReportSaleModel.Result = TextUtils.ToString(grvData.GetRowCellValue(i, colResult));
                dailyReportSaleModel.WarehouseID = warehouseID; //VP BN

                if (dailyReportSaleModel.ID > 0)
                {
                    DailyReportSaleBO.Instance.Update(dailyReportSaleModel);
                }
                else
                {
                    dailyReportSaleModel.ID = (int)DailyReportSaleBO.Instance.Insert(dailyReportSaleModel);
                    grvData.SetRowCellValue(i, colID, dailyReportSaleModel.ID);
                }
            }
            if (lstIDDelete.Count > 0)
            {
                DailyReportSaleBO.Instance.Delete(lstIDDelete);
            }

            return true;
        }
        #endregion
        private void LoadDailyReportDetail()
        {

            DataTable dt = TextUtils.GetDataTableFromSP("spDailyReportSaleBNGetByID", new string[] { "@ID" }, new object[] { dailyReportSaleModel.ID });
            grdData.DataSource = dt;

        }
        private void grdData_MouseDown(object sender, MouseEventArgs e) // event button add 
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;

                    List<int> listSTT = new List<int>();
                    DataTable dt = (DataTable)grdData.DataSource;
                    dt.AcceptChanges();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strSTT = TextUtils.ToString(dt.Rows[i]["STT"]);
                        if (!strSTT.Contains("."))
                        {
                            int stt = TextUtils.ToInt(dt.Rows[i]["STT"]);
                            listSTT.Add(stt);
                        }
                    }

                    DateTime? dateReport = TextUtils.ToDate4(grvData.GetRowCellValue(grvData.FocusedRowHandle, colDateStart));
                    int userID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colUserID));
                    int reportTypeId = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colGroupTypeID));
                    int customerId = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colCustomerID));
                    int contactId = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colContactID));

                    DataRow dtrow = dt.NewRow();
                    dtrow["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";
                    dtrow["DateStart"] = dateReport.HasValue ? dateReport.Value : DateTime.Now;
                    dtrow["UserID"] = userID <= 0 ? Global.UserID : userID;
                    dtrow["GroupType"] = reportTypeId;
                    dtrow["CustomerID"] = customerId;
                    dtrow["ContacID"] = contactId;
                    dt.Rows.Add(dtrow);

                    grdData.DataSource = dt;
                    grvData.FocusedRowHandle = grvData.RowCount - 1;
                    grvData.FocusedColumn = colContent;
                }
            }
        }
                private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
            {
                return;
            }
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa báo cáo không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (strID > 0)
                {
                    lstIDDelete.Add(strID);
                }
            }
        }

        private void frmDailyReportSaleBNDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmAddMainIndex frm = new frmAddMainIndex();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadGroup();
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomer();
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadContact();
            }
        }
    }
}