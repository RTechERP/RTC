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
    public partial class frmRequestPrice : _Forms
    {
        public frmRequestPrice()
        {
            InitializeComponent();
        }

        private void frmRequestPrice_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            string sql = $"select * from Users where LoginName = '{Global.LoginName}'";
            if (Global.IsAdmin == true)
            {
                sql = $"select * from Users";
            }
            DataTable dt = TextUtils.Select(sql);
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
            cbUser.EditValue = Global.UserID;

            loadUser();
            loadData();
        }

        #region Methods
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }
        private void loadData()
        {
            try
            {
                txtPageNumber.Text = "1";
                txtTotalPage.Text = "1";

                DataSet oDataSet = loadDataSet();

                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
            }
            catch (Exception ex)
            {
            }
        }
        DataSet loadDataSet()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetRequestPriceDetail"
                    , new string[] { "@PageNumber", "@PageSize", "@Status", "@UserID", "@DateStart", "@DateEnd", "@FilterText" }
                    , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt( txtPageSize.Value)
                    ,cboStatus.Text.Trim(), TextUtils.ToInt(cbUser.EditValue) , dateTimeS, dateTimeE , txtFilterText.Text.Trim()});
            grdData.DataSource = oDataSet.Tables[0];
            return oDataSet;
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmRequestPriceDetail frm = new frmRequestPriceDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        /// <summary>
        /// click button sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                if (id == 0) return;
                RequestPriceDetailModel model = (RequestPriceDetailModel)RequestPriceDetailBO.Instance.FindByPK(id);

                frmRequestPriceDetail frm = new frmRequestPriceDetail();
                frm.dModel = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// click button xóa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPartCode));
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    RequestPriceDetailBO.Instance.Delete(ID);
                    grvData.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }

        }

        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0 && btnEdit.Enabled == true)
                btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            //loadDataSet();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadDataSet();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadDataSet();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadDataSet();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadDataSet();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime deadLine = TextUtils.ToDate3(TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDeadLine)).ToString("yyyy/MM/dd"));
            //if (nowDate.CompareTo(deadLine) == 0)
            //    e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
            //if (nowDate.CompareTo(deadLine) > 0)
            //    e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            frmPriceCheckDetail frm = new frmPriceCheckDetail();
            frm._request = ID;
        }
    }
}
