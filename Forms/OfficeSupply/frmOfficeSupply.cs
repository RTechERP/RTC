using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using System.Collections;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace BMS
{
    public partial class frmOfficeSupply : _Forms
    {
        private bool isAdmin = Global.IsAdmin || Global.IsAdminSale;
        public frmOfficeSupply()
        {
            InitializeComponent();
        }
        #region Load Data
        private void LoadData()
        {

            DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupply",
                new string[] { "@KeyWord" }, new object[] { txtFilterText.Text.Trim() });
            grdData.DataSource = dt;
        }

        private void frmOfficeSupply_Load(object sender, EventArgs e)
        {
            //if (!isAdmin) {
            //    MessageBox.Show("Bạn không phải admin");
            //    return;
            //}
            LoadData();
        }
        private void frmOfficeSupply_Shown(object sender, EventArgs e)
        {
            //if (!isAdmin) this.Close();
        }
        #endregion
        #region Button click
        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows == null || selectedRows.Length == 0) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = selectedRows.Length - 1; i >= 0; i--)
                {
                    int rowHandle = selectedRows[i];
                    string strID = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colID));
                    SQLHelper<OfficeSupplyModel>.DeleteByAttribute("ID", strID);
                    grvData.DeleteRow(rowHandle);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmOfficeSupplyDetail frm = new frmOfficeSupplyDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            long id = TextUtils.ToInt64(grvData.GetFocusedRowCellValue(colID));
            var model = SQLHelper<OfficeSupplyModel>.FindByID(id);
            frmOfficeSupplyDetail frm = new frmOfficeSupplyDetail();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xls)|*.xls;*.xls";
            sfd.FileName = $"OfficeSupply";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    grvData.ExportToXlsx(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnUnit_Click(object sender, EventArgs e)
        {
            frmOfficeSupplyUnit frm = new frmOfficeSupplyUnit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        #endregion

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmOfficeSupplyImportExcel frm = new frmOfficeSupplyImportExcel();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}