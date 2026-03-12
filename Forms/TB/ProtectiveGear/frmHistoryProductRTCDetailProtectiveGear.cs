using BMS.Model;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraGrid.Columns;
using System;
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
    public partial class frmHistoryProductRTCDetailProtectiveGear : _Forms
    {
        DataTable _dtList = new DataTable();
        DataTable _dtB = new DataTable();
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        public UsersModel user = new UsersModel();
        int a = 1;
        public int warehouseID = 5;
        List<int> hcnsIDs = SQLHelper<vUserGroupLinkModel>.FindByAttribute("Code", "N34").Select(p => p.UserID).ToList();


        public int productRTCID = 0;
        public frmHistoryProductRTCDetailProtectiveGear()
        {
            InitializeComponent();
        }

        private void frmHistoryProductRTCDetailProtectiveGear_Load(object sender, EventArgs e)
        {
            loadDataListProduct();
            loadDataBorrow();
            loadUsers();


            if (productRTCID > 0)
            {
                DataRow[] rs = _dtList.Select("ID = " + productRTCID);

                if (rs.Length <= 0) return;

                rs[0]["NumberBorrow"] = 1;
                _dtB.ImportRow(rs[0]);
            }
        }
        void loadUsers()
        {
            int userId = Global.UserID;
            bool isHCNS = hcnsIDs.Contains(userId);
            bool isAdmin = (Global.IsAdmin && Global.EmployeeID == 0);
            if (isHCNS || isAdmin) userId = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "a", new string[] { "@UsersID", "@Status" }, new object[] { userId, 0 });

            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
            cbUser.EditValue = userId;
        }
        private void loadDataListProduct()
        {
            //_dtList = TextUtils.LoadDataFromSP("spGetProductRTC_Detail", "A",
            //                    new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@Filter", "@WarehouseID" },
            //                    new object[] { 0, "", 0, txtFilter.Text.Trim(), warehouseID });

            DateTime date = new DateTime(2024, 09, 01);
            DateTime dateStart = date;
            DateTime dateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            string keyword = "";
            string status = "1,2,3,4,5,6,7,8";
            int employeeID = 0;
            int isDeleted = 0;
            int productGroupRTCID = 140;

            //_dtList = TextUtils.LoadDataFromSP("spGetHistoryProductRTCProtectiveGear","A",
            //        new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@Status", "@IsDeleted", "@WarehouseID", "@ProductGroupRTCID", "@Keyword" },
            //        new object[] { dateStart, dateEnd, employeeID, status, isDeleted, warehouseID, productGroupRTCID, keyword });

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetHistoryProductRTCProtectiveGear",
                   new string[] { "@DateStart", "@DateEnd", "@EmployeeID", "@Status", "@IsDeleted", "@WarehouseID", "@ProductGroupRTCID", "@Keyword" },
                   new object[] { dateStart, dateEnd, employeeID, status, isDeleted, warehouseID, productGroupRTCID, keyword });

            _dtList = dataSet.Tables[1];
            _dtList.Columns.Add("NumberBorrow");

            foreach (DataRow row in _dtList.Rows)
            {
                if (row["NumberBorrow"] == DBNull.Value)
                {
                    row["NumberBorrow"] = 0;
                }
            }

            DataTable dtProducts = _dtList.Clone();
            var data = _dtList.Select($"{colProductGroupRTCID.FieldName} <> 140");
            if (data.Length > 0)dtProducts = data.CopyToDataTable();
            
            grdData.DataSource = dtProducts;
            //grdData.DataSource = _dtList;

            //string filterString = $"([{colProductGroupRTCID.FieldName}] != (140))";
            //grvData.Columns[colProductGroupRTCID.FieldName].FilterInfo = new ColumnFilterInfo(filterString);
        }
        private void loadDataBorrow()
        {
            _dtB = TextUtils.Select("SELECT top 1 * FROM ProductRTC where id = 0");
            _dtB.Columns.Add("NumberBorrow");
            _dtB.Columns.Add("ProductQRCode");
            grdData2.DataSource = _dtB;
        }

        private void grvData_Click(object sender, EventArgs e)
        {
            a = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colNumber));
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadDataListProduct();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            a--;
            int[] lstIndex = grvData.GetSelectedRows();

            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(lstIndex[i], colID));
                if (id == 0) continue;
                DataRow[] rs = _dtList.Select("ID = " + id);
                rs[0]["NumberBorrow"] = 1;
                _dtB.ImportRow(rs[0]);
            }
            //if (a == 0)
            //{
            //    grvData.DeleteSelectedRows();
            //}
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int[] lstIndex = grvData2.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData2.GetRowCellValue(lstIndex[i], colID2));
                if (id == 0) continue;
                DataRow[] rs = _dtB.Select("ID = " + id);
                _dtList.ImportRow(rs[0]);
            }
            grvData2.DeleteSelectedRows();
        }
        bool ValidateForm()
        {
            if (grvData2.RowCount <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm");
                return false;
            }

            if (dtpReturn.Value.Date < DateTime.Now.Date || dtpReturn.Value.Date < dtpBorrowDate.Value.Date)
            {
                MessageBox.Show("Ngày dự kiến trả không phù hợp! Ngày dự kiến trả phải lớn hơn ngày mượn hoặc thời gian hiện tại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            for (int i = 0; i < grvData2.RowCount; i++)
            {
                int prdID = TextUtils.ToInt(grvData2.GetRowCellValue(i, "ID"));
                int numberBr = TextUtils.ToInt(grvData2.GetRowCellValue(i, "NumberBorrow"));
                string prName = TextUtils.ToString(grvData2.GetRowCellValue(i, "ProductName"));
                if (prdID == 0) continue;
                DataRow[] rs = _dtList.Select("ID = " + prdID);
                //int prdRemain = TextUtils.ToInt(rs[0]["InventoryReal"]);
                //if (numberBr > prdRemain)
                //{
                //    MessageBox.Show($"Số lượng mượn của sản phẩm [{prName}] không được vượt quá [{prdRemain}]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
            }

            return true;
        }
        private void btnBorrow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvData.Focus();
                if (!ValidateForm()) return;
                if (cbUser.Text.Trim() == "")
                {
                    MessageBox.Show("Chọn tên người mượn");
                    return;
                }
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn thêm ?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialog == DialogResult.Cancel)
                {
                    return;
                }

                for (int i = 0; i < grvData2.RowCount; i++)
                {


                    oHistoryModel.ProductRTCID = TextUtils.ToInt(grvData2.GetRowCellValue(i, colID2));
                    oHistoryModel.DateBorrow = TextUtils.ToDate(dtpBorrowDate.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                    oHistoryModel.DateReturnExpected = TextUtils.ToDate(dtpReturn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                    oHistoryModel.PeopleID = TextUtils.ToInt(cbUser.EditValue);
                    oHistoryModel.Project = TextUtils.ToString(txtProject.Text);
                    oHistoryModel.Note = TextUtils.ToString(txtNote.Text);
                    oHistoryModel.Status = 7;
                    if (Global.IsAdmin || hcnsIDs.Contains(Global.UserID))
                    {
                        oHistoryModel.Status = 1;
                    }
                    oHistoryModel.NumberBorrow = TextUtils.ToDecimal(grvData2.GetRowCellValue(i, colNumberBorrow));
                    oHistoryModel.WarehouseID = warehouseID;
                    SQLHelper<HistoryProductRTCModel>.Insert(oHistoryModel);

                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }
    }
}
