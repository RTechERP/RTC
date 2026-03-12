using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuotationKH : _Forms
    {
        int warehouseID = 1;
        public frmQuotationKH()
        {
            InitializeComponent();
        }

        private void frmQuotationKH_Load(object sender, EventArgs e)
        {

            txtPageNumber.Text = "1";
            loadCustomer();
            loadUser();
            loadQuotationKH();
            btnDown.Click += new EventHandler(btnDown_Click);

        }

        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dt;
        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        /// <summary>
        /// load QuotationKH
        /// </summary>
        private void loadQuotationKH()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetQuotationKH", "A"
                , new string[] { "@FilterText", "@Status", "@CustomerID", "@UserID", "@PageSize", "@PageNumber" }
                , new object[] { txtFilterText.Text, cbStatus.SelectedIndex,TextUtils.ToInt(cboCustomer.EditValue),TextUtils.ToInt(cbUser.EditValue),
                    txtPageSize.Value, TextUtils.ToInt(txtPageNumber.Text) });
            grdMaster.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }

        /// <summary>
        /// load QuotationKH Detail
        /// </summary>
        void loadQuotationKHDetail()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            bool Merge = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colMerge));
            DataTable dt = TextUtils.Select($"Select * From QuotationKHDetail where QuotationKHID={ID}");
      
            grdData.DataSource = dt;
            if(Merge)
            {
                grvData.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                colIntoMoney.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                colUnitPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                grvData.OptionsView.AllowCellMerge = true;
                colGroup.GroupIndex = 0;
            }
            else
            {
                grvData.CellMerge -= new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(grvData_CellMerge);
                grvData.OptionsView.AllowCellMerge = false;
                colGroup.GroupIndex = -1;
            }
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmQuotationKHDetail frm = new frmQuotationKHDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadQuotationKH();
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            QuotationKHModel model = (QuotationKHModel)QuotationKHBO.Instance.FindByPK(ID);
            frmQuotationKHDetail frm = new frmQuotationKHDetail();
            frm.quotationKH = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadQuotationKH();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            string quotationCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colQuotationCode));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu xuất [{0}] này?", quotationCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }

            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa phiếu xuất [{0}] không?", quotationCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                QuotationKHBO.Instance.Delete(ID);
                QuotationKHDetailBO.Instance.DeleteByAttribute("QuotationKHID", ID);
                grvMaster.DeleteSelectedRows();
                loadQuotationKHDetail();
            }
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadQuotationKH();
        }

        /// <summary>
        /// click button duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            approved(true, true);
        }

        /// <summary>
        /// click button hủy duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            approved(false, false);
        }

        /// <summary>
        /// click button coppy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertPO_Click(object sender, EventArgs e)
        {
            frmPOKHDetail frm = new frmPOKHDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
              
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadQuotationKH();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadQuotationKH();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadQuotationKH();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadQuotationKH();
        }
        #endregion

        private void btnDown_Click(object sender, EventArgs e)
        {
            string path = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colAttachFile));
            if (path == "")
            {
                MessageBox.Show("Không có  tệp đính kèm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn download tệp đính kèm không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {

                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.ShowDialog();
                    DocUtils.DownloadFile(path, fbd.SelectedPath);
                    Process.Start(fbd.SelectedPath);
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadQuotationKH();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadQuotationKHDetail();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isApproved"></param>
        /// <param name="status"></param>
        void approved(bool isApproved, bool status)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt báo giá này?", isApproved ? "" : " bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return;

            int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string sql = string.Format(@"UPDATE dbo.QuotationKH SET IsApproved = {0}, Status = {1} WHERE ID = {2}", isApproved ? 1 : 0, status ? 1 : 0, iD);
            TextUtils.ExcuteSQL(sql);
            if (isApproved == true)
            {
                grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
            }
            else
            {
                grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
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
                e.Handled = true;

            }
            else
            {
                string value1 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle1, e.Column));
                string value2 = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle2, e.Column));
                e.Merge = (value2 == "");
                e.Handled = true;
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvMaster.OptionsPrint.AutoWidth = false;
                grvMaster.OptionsPrint.ExpandAllDetails = false;
                grvMaster.OptionsPrint.PrintDetails = true;
                grvMaster.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvMaster.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
       
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }
    }
}
