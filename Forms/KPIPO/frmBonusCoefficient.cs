using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using Forms.Classes;
using Forms.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmBonusCoefficient : _Forms
    {
        public frmBonusCoefficient()
        {
            InitializeComponent();
        }
        int quy;
        private void frmProductivityIndex_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                nbrYear.Value = DateTime.Now.Year;
                loadgroupSale();
                loaduser();
                checktime();

            }
            finally
            {
                cGlobVar.LockEvents = false;
            }

        }
        void loaduser()
        {
            DataTable dt = TextUtils.Select($"Select u.FullName,u.ID,G.SaleUserTypeID From GroupSalesUser G Inner join Users u On u.ID = g.UserID ");
            cbUser.DisplayMember = "FullName";
            cbUser.ValueMember = "ID";
            cbUser.DataSource = dt;

        }
        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select * From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }


        void checktime()
        {

            if (0 < DateTime.Now.Month && DateTime.Now.Month < 4)
            {
                cbMonth.SelectedIndex = 0;
                quy = 0;
            }
            if (3 < DateTime.Now.Month && DateTime.Now.Month < 7)
            {
                cbMonth.SelectedIndex = 1;
                quy = 1;
            }
            if (6 < DateTime.Now.Month && DateTime.Now.Month < 10)
            {
                quy = 2;
                cbMonth.SelectedIndex = 2;
            }
            if (9 < DateTime.Now.Month && DateTime.Now.Month < 13)
            {
                cbMonth.SelectedIndex = 3;
                quy = 3;
            }
        }
        int count;
        void loadPerformance()
        {
            DataTable  dt = TextUtils.LoadDataFromSP("spGetBonusCoefficient", "A",new string[] { "@Quy" , "@Year", "@GroupID"}, new object[] { quy,nbrYear.Value, TextUtils.ToInt( cbGroup.EditValue)});
            grdData.DataSource = dt;
            count = dt.Rows.Count;
            grvData.ExpandAllGroups();
            //loadCbUser();
            Bonus();
        }
        string Position;
        void Bonus()
        {
            try
            {
                decimal total = 0, totalheso = 0; decimal newacc = 0;
                decimal heso = 0;
                decimal totalsale = 0;
                DataTable dt = TextUtils.Select($"Select b.PercentBonus,s.SaleUserTypeCode From BonusRuleIndex b Inner Join SaleUserType s on b.SaleUserTypeID = s.ID where b.GroupSalesID = {cbGroup.EditValue} ");
                if (dt.Rows.Count == 0) return;


                if (grvData.RowCount == 0) return;
                for (int i = 0; i < count; i++)
                {
                    string Position = TextUtils.ToString(grvData.GetRowCellValue(i, colCodePosition));
                    DataRow[] dtr = dt.Select($"SaleUserTypeCode = '{Position}'");
                    decimal perBonus = TextUtils.ToDecimal(dtr[0]["PercentBonus"]);

                    switch (Position)
                    {
                        case cConsts.Leader:

                            heso = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colCoefficient));
                            if (heso == 0) continue;
                            totalsale = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colTotalSale));
                            grvData.SetRowCellValue(i, colBonusSales, perBonus * totalsale * heso);
                            grvData.SetRowCellValue(i, colTotalBonus, perBonus * totalsale * heso);
                            break;
                        case cConsts.Marketting:
                            heso = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colCoefficient));
                            if (heso == 0) continue;
                            totalsale = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colTotalSale));
                            grvData.SetRowCellValue(i, colBonusSales, perBonus * totalsale * heso);
                            grvData.SetRowCellValue(i, colTotalBonus, perBonus * totalsale * heso);
                            break;
                        case cConsts.Staff:
                            heso = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCoefficient));
                            if (heso == 0) continue;
                            totalsale = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalSale));
                            newacc = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colNewAccountQty));
                            grvData.SetRowCellValue(i, colBonusAcc, newacc * 500000);
                            total += TextUtils.ToDecimal(heso * totalsale);
                            totalheso += heso;
                            txtSum.Text = TextUtils.ToString(total * perBonus);
                            break;
                        case cConsts.Admin:
                            heso = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCoefficient));
                            if (heso == 0) continue;
                            totalsale = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colTotalSale));
                            grvData.SetRowCellValue(i, colBonusSales, perBonus * totalsale * heso);
                            grvData.SetRowCellValue(i, colTotalBonus, perBonus * totalsale * heso);
                            break;
                        case cConsts.LeaderTeam:
                            heso = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colCoefficient));
                            if (heso == 0) continue;
                            totalsale = TextUtils.ToDecimal(grvData.GetRowCellValue(0, colTotalSale));
                            grvData.SetRowCellValue(i, colBonusSales, perBonus * totalsale * heso);
                            grvData.SetRowCellValue(i, colTotalBonus, perBonus * totalsale * heso);
                            break;
                        default:
                            break;
                    }
                }
                for (int i = 0; i < count; i++)
                {
                    string Position = TextUtils.ToString(grvData.GetRowCellValue(i, colCodePosition));
                    DataRow[] dtr = dt.Select($"SaleUserTypeCode = '{Position}'");
                    decimal perBonus = TextUtils.ToDecimal(dtr[0]["PercentBonus"]);
                    if (Position == "Sta")
                    {
                        heso = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCoefficient));
                        if (heso == 0) continue;
                        grvData.SetRowCellValue(i, colBonusSales, total * perBonus / totalheso * heso);
                        decimal bonusadd = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBonusAdd));
                        decimal bonusrank = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBonusRank));
                        grvData.SetRowCellValue(i, colTotalBonus, bonusadd + bonusrank + (total * perBonus / totalheso * heso) + (newacc * 500000));
                    }

                }
            }
            catch { }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SalesPerformanceRankingModel sale = new SalesPerformanceRankingModel();
                for (int i = 0; i < count; i++)
                {
                    int IDbot = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    if (IDbot > 0)
                        sale = (SalesPerformanceRankingModel)SalesPerformanceRankingBO.Instance.FindByPK(IDbot);
                    sale.BonusSales = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBonusSales));
                    sale.BonusAdd = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBonusAdd));
                    sale.BonusRank = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBonusRank));
                    sale.BonusAcc = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBonusAcc));
                    sale.TotalBonus = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalBonus));
                    sale.UserID = TextUtils.ToInt(grvData.GetRowCellValue(i, colUserID));
                    sale.Quy = quy;
                    sale.Year = DateTime.Now.Year;
                    if (IDbot > 0)
                        SalesPerformanceRankingBO.Instance.Update(sale);
                    else
                        SalesPerformanceRankingBO.Instance.Insert(sale);
                }
                //this.Close();
            }
            catch { }
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPerformance();
        }

        private void cbPosition_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPerformance();
        }


        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPerformance();
        }

        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadPerformance();
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
