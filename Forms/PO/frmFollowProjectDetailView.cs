using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraTreeList;
using Forms.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmFollowProjectDetailView : _Forms
    {

        public int idview;

        public frmFollowProjectDetailView()
        {
            InitializeComponent();
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {
            try
            {
                txtPageNumber.Text = "1";
                cGlobVar.LockEvents = true;
                loadGrdData();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }

        async void loadGrdData()
        {
            int max = TextUtils.ToInt(TextUtils.ExcuteScalar("select top 1 ID from FollowProjectDetail ORDER by ID DESC"));
            DataTable dt = new DataTable();

            dt = TextUtils.LoadDataFromSP("spGetFollowProjectDetailView", "A", new string[] { "@Find", "@DateStart", "@DateEnd", "@Status", "@PageNumber", "@PageSize" }, new object[] { txtFilterText.Text.Trim(), dtpStartDate.Value, dtpEndDate.Value, cbStatus.SelectedIndex, txtPageNumber.Text, txtPageSize.Value });

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (TextUtils.ToInt(dt.Rows[i]["NewRow"]) == 1)
                {
                    dt.Rows[i]["ID"] = max + i;
                }
            }
            grdData.DataSource = dt;
            grvData.ExpandAllGroups();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadGrdData();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadGrdData();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadGrdData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadGrdData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadGrdData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadGrdData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadGrdData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadGrdData();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrdData();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colStatus")
            {
                string value = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colStatus));
                if (value == "Finish")
                    e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                if (value == "Chưa giao đủ")
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
            }
            if (e.Column.Name == "colCustomerQuotationDetail" || e.Column.Name == "colTotalCustomerQuotationDetail")
            {

                e.Appearance.BackColor = Color.Yellow;

            }
            if (e.Column == colDeliveryRequestedDate)
            {
                int c = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, ColorTest));
                switch (c)
                {
                    case 0:
                        e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                        break;
                    case 1:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    case 2:
                        e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                        break;
                    default:
                        e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                        break;
                }

            }
        }
    }
}
