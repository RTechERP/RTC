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
    public partial class frmQuotationSummary : _Forms
    {
        public frmQuotationSummary()
        {
            InitializeComponent();
        }

        private void frmQuotationSummary_Load(object sender, EventArgs e)
        {
            //cboStatus.SelectedIndex = 0;
            //loadHang();
            //loadNCC();
            loadUser();
            loadData();
        }

        #region Methods
        void loadHang()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Manufacturer");
            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.ValueMember = "ID";
            repositoryItemSearchLookUpEdit2.DisplayMember = "ManufacturerCode";
        }
        void loadNCC()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Supplier");
            repositoryItemSearchLookUpEdit1.DataSource = dt;
            repositoryItemSearchLookUpEdit1.ValueMember = "ID";
            repositoryItemSearchLookUpEdit1.DisplayMember = "SupplierShortName";
        }
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            //cboRequestUser.Properties.DisplayMember = "FullName";
            //cboRequestUser.Properties.ValueMember = "ID";
            //cboRequestUser.Properties.DataSource = dt;
            ////cboMonitorUser
            //cboMonitorUser.Properties.DisplayMember = "FullName";
            //cboMonitorUser.Properties.ValueMember = "ID";
            //cboMonitorUser.Properties.DataSource = dt;
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
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataSet loadDataSet()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetQuotationDetailPaging"
                    , new string[] { "@PageNumber", "@PageSize", "@FilterText" }
                    , new object[] { TextUtils.ToInt(txtPageNumber.Text)
                    ,TextUtils.ToInt(txtPageSize.Value)
                    //,cboStatus.SelectedIndex
                    //,TextUtils.ToInt(cboRequestUser.EditValue)
                    //,TextUtils.ToInt(cboMonitorUser.EditValue)
                    //,chkIsImport.Checked
                    , txtFilterText.Text.Trim() });
            grdData.DataSource = oDataSet.Tables[0];

            return oDataSet;
        }
        
        #endregion

        #region Buttons Events

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
            //    return;

            //int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));

            //string strName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("RequestPriceCode"));

            //if (RequestPriceDetailBO.Instance.CheckExist("RequestPriceID", strID))
            //{
            //    MessageBox.Show("Nhà cung cấp này đã phát sinh ở các nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            //if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    try
            //    {
            //        RequestPriceBO.Instance.Delete(strID);
            //        grvMaster.DeleteSelectedRows();
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            //    }
            //}
        }

        #endregion

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

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadData();
            }
        }

        private void btnShowQuotationDetail_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQuotationID));
                if (id == 0) return;
                QuotationModel model = (QuotationModel)QuotationBO.Instance.FindByPK(id);

                frmQuotationDetail frm = new frmQuotationDetail();
                frm.oQuotation = model;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnShowQuotationDetail_Click(null, null);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            TextUtils.ExportExcel(grvData);
        }
    }
}
