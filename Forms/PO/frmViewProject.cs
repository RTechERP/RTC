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
    public partial class frmViewProject : _Forms
    {

        public int idview;

        public frmViewProject()
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

            dt = TextUtils.LoadDataFromSP("spLoadFollowProjectDetail", "A", new string[] { "@Find", "@DateStart", "@DateEnd", "@Status", "@PageNumber", "@PageSize" }, new object[] { txtFilterText.Text.Trim(), dtpStartDate.Value, dtpEndDate.Value, cbStatus.SelectedIndex, txtPageNumber.Text, txtPageSize.Value });

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0][0]);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (TextUtils.ToInt(dt.Rows[i]["NewRow"]) == 1)
                {
                    dt.Rows[i]["ID"] = max + i;
                }
            }
            treeData.DataSource = dt;
            treeData.ExpandAll();
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

        private void treeData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                TreeListHitInfo info = treeData.CalcHitInfo(new Point(e.X, e.Y));
                if (e.Y > 20) return;
                if (info.Band != null && info.Band.Name == "grbID")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colPODate.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbID.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 1)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbID.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }
                }


                if (info.Band != null && info.Band.Name == "grbDate")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colDebt.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbDate.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 2)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbDate.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }
                }


                if (info.Band != null && info.Band.Name == "grbMaker")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colMaker.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbMaker.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 3)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;

                                }
                                this.grbMaker.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }
                }
                if (info.Band != null && info.Band.Name == "grbProduct")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colProjectModel.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbProduct.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 4)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbProduct.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }

                }
                if (info.Band != null && info.Band.Name == "grbQty")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colQty.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbQty.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 5)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbQty.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }

                }
                if (info.Band != null && info.Band.Name == "grbPrice")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colUnitPriceUSD.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbPrice.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 6)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbPrice.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }
                        }
                    }

                }
                if (info.Band != null && info.Band.Name == "grbCost")
                {
                    if (info.Band.Columns.Count <= 0) return;
                    if (colTotalBankCharges.Visible)
                    {
                        foreach (var item in info.Band.Columns)
                        {
                            item.Visible = false;
                        }
                        info.Band.Width = 30;
                        this.grbCost.ImageOptions.Image = global::Forms.Properties.Resources.expand;
                    }
                    else
                    {
                        info.Band.Width = 0;
                        foreach (var item in info.Band.Columns)
                        {
                            if (item.Tag != null)
                            {
                                if (!int.TryParse(item.Tag.ToString(), out int tag))
                                    tag = -1;
                                if (tag == 7)
                                {
                                    item.Visible = true;
                                    info.Band.Width += item.Width;
                                }
                                this.grbCost.ImageOptions.Image = global::Forms.Properties.Resources.collapse;
                            }

                        }
                    }

                }
            }
        }

        private void treeData_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            try
            {
                if (treeData.AllNodesCount > 0)
                {
                    if (e.Node == null) return;
                    if (e.Column == colStatus)
                    {
                        string value = TextUtils.ToString(treeData.GetRowCellValue(e.Node, colStatus));
                        if (value == "Finish")
                            e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                        if (value == "Chưa giao đủ")
                            e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                    }
                    else
                    if (e.Column == colCustomerQuotationDetail || e.Column == colTotalCustomerQuotationDetail)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    else
                    if (e.Column == colDeliveryRequestedDate)
                    {
                        int c = TextUtils.ToInt(treeData.GetRowCellValue(e.Node, ColorTest));
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
                    DataRow row = treeData.GetDataRow(e.Node.Id);
                    if (row[cConsts.NewRow] != DBNull.Value)
                    {
                        int color = (int)row[cConsts.NewRow];
                        if (color == 1)
                            e.Appearance.BackColor = Color.FromArgb(54, 75, 109);
                    }
                }
            }
            catch(Exception ex)
            {

            }
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
    }
}
