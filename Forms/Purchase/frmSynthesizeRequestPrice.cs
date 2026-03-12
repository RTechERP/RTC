using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSynthesizeRequestPrice : _Forms
    {
        public frmSynthesizeRequestPrice()
        {
            InitializeComponent();
        }

        private void frmSynthesizeRequestPrice_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
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
                    ,cboStatus.Text.Trim(), cbUser.EditValue , dateTimeS, dateTimeE , txtFilterText.Text.Trim()});
            grdData.DataSource = oDataSet.Tables[0];
            return oDataSet;
        }
        #endregion

        #region Buttons Events
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
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
            //bool approve = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsApproved));
            //if (!approve)
            //{
            //    if (nowDate.CompareTo(deadLine) == 0)
            //        e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
            //    if (nowDate.CompareTo(deadLine) > 0)
            //        e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
            //}
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            bool approve = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string product = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPartCode));
            DataRow dtr =  grvData.GetDataRow(grvData.FocusedRowHandle);
            if (approve)
            {
                MessageBox.Show("Phiếu đã được duyệt không thế sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
                frmPriceCheckDetail frm = new frmPriceCheckDetail();
            frm._request = ID;
            frm._product = product;
            frm.dataRow = dtr;
            if( frm.ShowDialog()== DialogResult.OK)
            {
                loadDataSet(); 
            }    
        }
        void approve(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string sql = string.Format("UPDATE dbo.RequestPriceDetail SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
              
                if (isApproved )
                    grvData.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvData.SetFocusedRowCellValue(colIsApproved, 0);
               
            }
          
        }
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            approve(true);
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            approve(false);
        }

        private void btnPriceCheck_Click(object sender, EventArgs e)
        {
            frmPriceCheck frm = new frmPriceCheck();
            if( frm.ShowDialog()== DialogResult.OK)
            {

            }    
        }
    }
}
