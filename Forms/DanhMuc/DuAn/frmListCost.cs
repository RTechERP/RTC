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

namespace BMS
{
    public partial class frmListCost : _Forms
    {
        public frmListCost()
        {
            InitializeComponent();
        }

        private void frmListCost_Load(object sender, EventArgs e)
        {
            loadListCost();
        }

        #region Methods
        /// <summary>
        /// load SALE
        /// </summary>
        private void loadListCost()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetListCost", "A", new string[] {"@FilterText"}, new object[] {txtFilterText.Text.Trim()});
            grdData.DataSource = dt;
        }
        #endregion

        #region Button Events
        /// <summary>
        /// click button add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListCostModel model = new ListCostModel();
            frmListCostDetail frm = new frmListCostDetail();
            frm.listCost = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadListCost();
            }
        }

        /// <summary>
        /// click button edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            ListCostModel model = (ListCostModel)ListCostBO.Instance.FindByPK(ID);
            frmListCostDetail frm = new frmListCostDetail();
            frm.listCost = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadListCost();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        /// <summary>
        /// click button delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string costCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCostCode));
            string costName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCostName));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa chi phí [{0}] : [{1}] không?", costCode, costName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ListCostBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadListCost();
        }
        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
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
    }
}


