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
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace BMS
{
    public partial class frmCustomerBase : _Forms
    {
        public frmCustomerBase()
        {
            InitializeComponent();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadUser();
            loadCustomer();
            cbUser.EditValue = Global.UserID;

            // phân quyền admin
            DataTable dt = TextUtils.Select($"Select * From [GroupSalesUser] WHERE UserID = {cbUser.EditValue} AND (SaleUserTypeID = 1 OR SaleUserTypeID = 6 OR SaleUserTypeID = 7 OR SaleUserTypeID = 8)");
            if (dt.Rows.Count > 0) cbUser.Enabled = true;
            else cbUser.Enabled = false;
        }
        #region Methods
        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;
        }

        private void loadCustomer()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetCustomerBase"
               , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@UserID" }
               , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text.Trim(), cbUser.EditValue });
            grdData.DataSource = oDataSet.Tables[0];

            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerBaseModel model = new CustomerBaseModel();
            frmCustomerBaseDetail frm = new frmCustomerBaseDetail();
            frm.customerBase = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }

        /// <summary>
        /// button sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// click button delete khách hàng khỏi danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            string customerName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCustomerName));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa khách hàng [{0}] không?", customerName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CustomerBaseBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button nhập excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmCustomerExcel frmExcel = new frmCustomerExcel();
            if (frmExcel.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
            }
        }
        #endregion

        /// <summary>
        /// void edit data 
        /// </summary>
        private void EditData()
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            CustomerBaseModel model = (CustomerBaseModel)CustomerBaseBO.Instance.FindByPK(ID);
            frmCustomerBaseDetail frm = new frmCustomerBaseDetail();
            frm.customerBase = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCustomer();
                grvData.FocusedRowHandle = focusedRowHandle;
                grvData_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// event EditData by doubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// link đến bảng contact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ArrayList arr = CustomerBaseContactBO.Instance.FindByAttribute("CustomerID", Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));
            grdContact.DataSource = arr;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadCustomer();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadCustomer();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadCustomer();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadCustomer();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadCustomer();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadCustomer();
        }

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

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {
        }

        private void grvData_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //if (e.CellValue != null)
            //{
            //    e.Appearance.BackColor = Color.DeepPink;
            //    e.Appearance.ForeColor = Color.White;
            //}
        }
    }
}


