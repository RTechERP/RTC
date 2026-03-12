using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    //public delegate void status();

    public partial class frmProductHistoryBorrowDetail : _Forms//, IMessageFilter
    {
        DataTable _dtList = new DataTable();
        DataTable _dtB = new DataTable();
        DataTable _dtName = new DataTable();
        public ProductRTCModel oProductRTCModel = new ProductRTCModel();
        public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        public UsersModel user = new UsersModel();
        int a = 1;
        //public status Status;
        public int warehouseID;



        //int[] _idAdminDemos = new int[] { 24, 1434, 88 };
        public frmProductHistoryBorrowDetail()
        {
            //Application.AddMessageFilter(this);
            InitializeComponent();
            txtProject.Text = "Test văn phòng";
        }
        public frmProductHistoryBorrowDetail(int WarehouseID)
        {
            //Application.AddMessageFilter(this);
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        //FindControl findControl;
        //public bool PreFilterMessage(ref Message m)
        //{
        //	Keys key = (Keys)m.WParam.ToInt32();
        //	return key == Keys.Enter && findControl.FindEdit.MaskBox.Focused;
        //}

        //protected override void OnShown(EventArgs e)
        //{
        //	base.OnShown(e);

        //	GridControl control = grdData;
        //	findControl = control.Controls.OfType<FindControl>().FirstOrDefault();
        //	if (findControl != null)
        //		findControl.FindEdit.KeyUp += FindEdit_KeyUp;
        //}


        //void FindEdit_KeyUp(object sender, KeyEventArgs e)
        //{
        //	TextEdit te = sender as TextEdit;
        //	BeginInvoke(new MethodInvoker(() =>
        //	{
        //		te.SelectionStart = te.Text.Length;
        //	}));
        //}

        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductRTC_Load(object sender, EventArgs e)
        {
            loadDataListProduct();
            loadDataBorrow();
            loadUsers();
            //DataTable dt = new DataTable(); TextUtils.Select(@"select * from Users");
            //string sql = $"select * from Users where LoginName = '{Global.LoginName}'";
            //if (Global.IsAdmin == true || Global.UserID == 24)
            //{
            //    sql = $"select * from Users where Status != 1";
            //}

            //DataTable dt = TextUtils.Select(sql);
            //cbUser.Properties.DisplayMember = "FullName";
            //cbUser.Properties.ValueMember = "ID";
            //cbUser.Properties.DataSource = dt;
            //cbUser.EditValue = Global.UserID;
            //lblUser.Text = Global.LoginName;
        }
        void loadUsers()
        {
            int userId = Global.UserID;
            bool isAdmin = Global.IDAdminDemo.Contains(userId);
            if (Global.IsAdmin == true || isAdmin) userId = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetUsersHistoryProductRTC", "a", new string[] { "@UsersID", "@Status" }, new object[] { userId, 0 });

            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
            cbUser.EditValue = userId;
        }
        /// <summary>
        /// load list data
        /// </summary>
        private void loadDataListProduct()
        {
            _dtList = TextUtils.LoadDataFromSP("spGetProductRTC_Detail", "A",
                                new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@Filter", "@WarehouseID" },
                                new object[] { 0, "", 1, txtFilter.Text.Trim(), warehouseID });

            //_dtList = _dtList.Select("InventoryReal > 0").CopyToDataTable();
            _dtList.Columns.Add("NumberBorrow");

            foreach (DataRow row in _dtList.Rows)
                if (row["NumberBorrow"] == DBNull.Value)
                {
                    row["NumberBorrow"] = 0;
                }


            grdData.DataSource = _dtList;
            //grvData.OptionsFind.SearchInPreview
        }
        /// <summary>
        /// load dữ liệu data cho mượn
        /// </summary>
        private void loadDataBorrow()
        {
            _dtB = TextUtils.Select("SELECT top 1 * FROM ProductRTC where id = 0");
            _dtB.Columns.Add("NumberBorrow");
            _dtB.Columns.Add("ProductQRCode");
            grdData2.DataSource = _dtB;
        }
        /// <summary>
        /// lựa chọn thiết bị để mượn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMove_Click(object sender, EventArgs e)
        {
            a--;
            //SelectDataBorrow();
            int[] lstIndex = grvData.GetSelectedRows();

            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(lstIndex[i], colID));
                if (id == 0) continue;
                DataRow[] rs = _dtList.Select("ID = " + id);
                rs[0]["NumberBorrow"] = 1;
                //rs[0]["ProductQRCode"] = 1;
                _dtB.ImportRow(rs[0]);
            }
            if (a == 0)
            {
                grvData.DeleteSelectedRows();
            }
            //grvData.DeleteSelectedRows();
        }
        /// <summary>
        /// save DataBorrow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
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
                //DataTable dt = TextUtils.Select("spGetDataProductInStore");
                //for (int i = 0; i < grvData2.RowCount; i++)
                //{
                //	oHistoryModel.ProductRTCID = TextUtils.ToInt(grvData2.GetRowCellValue(i, colID2));
                //	DataRow[] rs = dt.Select("ID = " + oHistoryModel.ProductRTCID);
                //	if (rs.Length <= 0)
                //	{
                //		MessageBox.Show("Số lượng tồn kho không đủ!");
                //		return;
                //	}

                //	int numInStore = TextUtils.ToInt(rs[0]["NumberInStore"]);
                //	int numBr = TextUtils.ToInt(grvData2.GetRowCellValue(i, colNumberBorrow));
                //	if (numBr > numInStore && numBr > 0)
                //	{
                //		MessageBox.Show("Số lượng tồn kho không đủ!");
                //		return;
                //	}

                //}
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn thêm ?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialog == DialogResult.Cancel)
                {
                    return;
                }

                bool isAdmin = Global.IDAdminDemo.Contains(Global.UserID);

                for (int i = 0; i < grvData2.RowCount; i++)
                {


                    oHistoryModel.ProductRTCID = TextUtils.ToInt(grvData2.GetRowCellValue(i, colID2));
                    //oHistoryModel.CreatedBy = Global.LoginName;
                    oHistoryModel.DateBorrow = TextUtils.ToDate(dtpBorrowDate.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                    oHistoryModel.DateReturnExpected = TextUtils.ToDate(dtpReturn.Value.ToString("yyyy/MM/dd HH:mm:ss"));

                    //  UsersModel user = UsersBO.Instance.FindByAttribute("LoginName", cbUser.Text)[0] as UsersModel;
                    oHistoryModel.PeopleID = TextUtils.ToInt(cbUser.EditValue);
                    oHistoryModel.Project = TextUtils.ToString(txtProject.Text);
                    oHistoryModel.Note = TextUtils.ToString(txtNote.Text);
                    oHistoryModel.Status = 7;
                    if (Global.IsAdmin || isAdmin) oHistoryModel.Status = 1;
                    oHistoryModel.NumberBorrow = TextUtils.ToDecimal(grvData2.GetRowCellValue(i, colNumberBorrow));
                    oHistoryModel.WarehouseID = warehouseID;
                    HistoryProductRTCBO.Instance.Insert(oHistoryModel);
                    //HuyNV_5/11/2022
                    //Không update lại số lượng trong bảng ProductRTC
                    //ProductRTCModel pModel = ProductRTCBO.Instance.FindByPK(oHistoryModel.ProductRTCID) as ProductRTCModel;
                    //if (pModel != null)
                    //{
                    //	pModel.NumberInStore -= TextUtils.ToInt(grvData2.GetRowCellValue(i, colNumberBorrow));
                    //	ProductRTCBO.Instance.Update(pModel);
                    //	_Forms frmProduct = (_Forms)Application.OpenForms["frmProductRTC"];
                    //	_Forms frmMain = (_Forms)Application.OpenForms["frmMain"];
                    //	if (frmProduct != null && frmMain != null)
                    //	{
                    //		frmProduct.Dispose();
                    //		TextUtils.OpenChildForm(new frmProductRTC(), frmMain);
                    //		//TextUtils.OpenChildForm(new frmProductHistory(), frmMain);.
                    //	}
                    //}
                }
                //Status();
                this.DialogResult = DialogResult.OK;
            }
            catch
            {

            }
        }
        /// <summary>
        /// return lại thiết bị khi chọn nhầm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int[] lstIndex = grvData2.GetSelectedRows();
            for (int i = 0; i < lstIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData2.GetRowCellValue(lstIndex[i], colID2));
                if (id == 0) continue;
                DataRow[] rs = _dtB.Select("ID = " + id);
                _dtList.ImportRow(rs[0]);
                //_dtB.Rows.Add(rs[0]);
            }
            grvData2.DeleteSelectedRows();
        }

        bool ValidateForm()
        {
            if (grvData2.RowCount <= 0)
            {
                MessageBox.Show("Chưa chọn thiết bị");
                return false;
            }

            if (dtpReturn.Value.Date < DateTime.Now.Date || dtpReturn.Value.Date < dtpBorrowDate.Value.Date)
            {
                MessageBox.Show("Ngày dự kiến trả không phù hợp! Ngày dự kiến trả phải lớn hơn ngày mượn hoặc thời gian hiện tại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            frmExport frm = new frmExport();
            frm.Show();
        }

        private void grvData_Click(object sender, EventArgs e)
        {
            //a = Lib.ToInt(grvData.GetFocusedRowCellValue(colNumber).ToString());
            a = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colNumber));
            //        if (a <= 0)
            //        {
            //return;
            //        }
        }


        private void btnFind_Click_1(object sender, EventArgs e)
        {
            //         if (txtFilter.Text.Trim() != "")
            //         {
            //	//string s = txtFilter.Text.Trim();
            //	//grvData.FindFilterText = s;
            //	loadDataListProduct();
            //}
            //         else
            //         {
            //	return;

            //}
            loadDataListProduct();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
