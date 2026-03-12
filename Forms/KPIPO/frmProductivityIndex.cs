using BMS.Business;
using BMS.Model;
using System;
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
    public partial class frmProductivityIndex : _Forms
    {
        public ReportIndexModel reportIndex = new ReportIndexModel();
        public int ID;
        string month;
        int quy;
        public frmProductivityIndex()
        {
            InitializeComponent();
        }

        private void frmProductivityIndex_Load(object sender, EventArgs e)
        {

            nbrYear.Value = DateTime.Now.Year;

            loadPositon();
            cbMonth.SelectedIndex = 2;
            checktime();
            loadReportIndex();
            grvReport.ExpandAllGroups();

        }
        void checktime()
        {

            if (cbMonth.SelectedIndex == 0)
            {
                month = "1,2,3";
                quy = 1;
            }
            if (cbMonth.SelectedIndex == 1)
            {
                month = "4,5,6";
                quy = 2;
            }
            if (cbMonth.SelectedIndex == 2)
            {
                month = "7,8,9";
                quy = 3;
            }
            if (cbMonth.SelectedIndex == 3)
            {
                month = "10,11,12";
                quy = 4;
            }
        }

        void loadPositon()
        {
            DataTable dt = TextUtils.Select("Select su.[SaleUserTypeName],su.ID,sp.UserID From [GroupSalesUser] gs  Inner join [SaleUserType] su On su.ID = gs.[SaleUserTypeID] Inner join [SalesPerformanceRanking] sp on sp.UserID = gs.UserID");
            cbPosition.DisplayMember = "SaleUserTypeName";
            cbPosition.ValueMember = "ID";
            cbPosition.DataSource = dt;
        }
        void loadReportIndex()
        {
            DataTable dt = TextUtils.Select($"Select * From ReportIndex where UserID ={ID} and Month IN ({month.Trim()}) And Year ={DateTime.Now.Year} Order by Month,UserID");
            grdReport.DataSource = dt;
            DataTable dtt = TextUtils.Select($"Select su.ID as IDSale,su.[SaleUserTypeName],sp.* From [GroupSalesUser] gs  Inner join [SaleUserType] su On su.ID = gs.[SaleUserTypeID] Inner join [SalesPerformanceRanking] sp on sp.UserID = gs.UserID where sp.Quy={quy} And sp.UserID={ID}  And sp.Year ={DateTime.Now.Year}");
            grdPerformance.DataSource = dtt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //ReportIndexModel report = new ReportIndexModel();
            //SalesPerformanceRankingModel sale = new SalesPerformanceRankingModel();
            //for (int i = 0; i < grvReport.RowCount - 1; i++)
            //{

            //    if (ID > 0)
            //        ReportIndexBO.Instance.Update(report);
            //    else
            //        ReportIndexBO.Instance.Insert(report);
            //}
            //for (int i = 0; i < grvPerformance.RowCount; i++)
            //{
            //    int IDbot = TextUtils.ToInt(grvPerformance.GetRowCellValue(i, colIDBot));
            //    if (IDbot > 0)
            //        sale = (SalesPerformanceRankingModel)SalesPerformanceRankingBO.Instance.FindByPK(IDbot);
            //    sale.Performance = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(i, colPerformance));
            //    sale.PerformanceOld = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(0, colPerformanceOld));
            //    sale.Coefficient = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(0, colCoefficient));
            //    sale.UserID = TextUtils.ToInt(ID);
            //    sale.Quy = quy;
            //    sale.Year = DateTime.Now.Year;
            //    if (IDbot > 0)
            //        SalesPerformanceRankingBO.Instance.Update(sale);
            //    else
            //        SalesPerformanceRankingBO.Instance.Insert(sale);
            //}
            //MessageBox.Show("Đã cất thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        decimal date = 0, week = 0, index = 0, success = 0;

        private void grvPerformance_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int IDSale = TextUtils.ToInt(grvPerformance.GetRowCellValue(0, colPosition));
            if (e.Column == colPerformance)
            {
                switch (IDSale)
                {
                    case 1://Leader dự án
                        if (performance < (decimal)0.6)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0);
                        else if (performance < (decimal)0.8)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.5);
                        else if (performance < (decimal)0.9)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.7);
                        else if (performance <= (decimal)1.0)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.0);
                        else if (performance < (decimal)1.1)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.1);
                        else if (performance < (decimal)1.3)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.2);
                        else if (performance <= (decimal)1.5)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.5);
                        else
                            grvPerformance.SetRowCellValue(0, colCoefficient, 2.0);
                        break;
                    case 2://Leader MRO
                        if (performance < (decimal)0.7)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0);
                        else if (performance < (decimal)0.8)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.5);
                        else if (performance < (decimal)0.9)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.7);
                        else if (performance <= (decimal)1.0)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.0);
                        else if (performance < (decimal)1.1)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.1);
                        else if (performance < (decimal)1.3)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.2);
                        else if (performance <= (decimal)1.5)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.5);
                        else
                            grvPerformance.SetRowCellValue(0, colCoefficient, 2.0);
                        break;


                    case 3://nhân viên staff
                        if (performance < (decimal)0.5)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0);
                        else if (performance < (decimal)0.7)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.5);
                        else if (performance <= (decimal)0.8)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.7);
                        else if (performance <= (decimal)0.95)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.8);
                        else if (performance <= (decimal)1.0)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.0);
                        else if (performance <= (decimal)1.2)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.1);
                        else if (performance < (decimal)1.5)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.3);
                        else if (performance <= (decimal)2.0)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 2.0);
                        else
                            grvPerformance.SetRowCellValue(0, colCoefficient, 2.5);
                        break;
                    case 4://Admin
                        if (performance < (decimal)0.8)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0);
                        else
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.0);
                        break;
                    case 5://Leader Sale
                        if (performance < (decimal)0.7)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0);
                        else if (performance < (decimal)0.8)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.3);
                        else if (performance < (decimal)0.9)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.6);
                        else if (performance < (decimal)0.98)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.9);
                        else if (performance <= (decimal)1.0)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.0);
                        else if (performance < (decimal)1.1)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.1);
                        else if (performance < (decimal)1.3)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.2);
                        else if (performance <= (decimal)1.5)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.5);
                        else
                            grvPerformance.SetRowCellValue(0, colCoefficient, 2.0);
                        break;
                    case 6://Marketing
                        if (performance < (decimal)0.7)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0);
                        else if (performance < (decimal)0.8)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.3);
                        else if (performance < (decimal)0.9)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.6);
                        else if (performance < (decimal)0.98)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 0.9);
                        else if (performance <= (decimal)1.0)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.0);
                        else if (performance < (decimal)1.1)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.1);
                        else if (performance < (decimal)1.3)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.2);
                        else if (performance <= (decimal)1.5)
                            grvPerformance.SetRowCellValue(0, colCoefficient, 1.5);
                        else
                            grvPerformance.SetRowCellValue(0, colCoefficient, 2.0);
                        break;
                    default:
                        break;
                }

            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            caculPerformance();
        }

        private void cbUsser_EditValueChanged(object sender, EventArgs e)
        {
            loadReportIndex();
            caculPerformance();

        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colNoReportDate || e.Column == colNoReportWeek || e.Column == colIndexGrantedAdmin || e.Column == colSuccessfulProposal)
            {
                caculPerformance();
            }
        }

        private void grvReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvReport.FocusedRowHandle++;
            }
        }

        private void grvPerformance_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void grvPerformance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvPerformance.FocusedRowHandle++;
            }
        }

        decimal performance = 0;
        void caculPerformance()
        {
            performance = 0;
            date = 0; week = 0; index = 0; success = 0;
            for (int i = 0; i < grvReport.RowCount - 1; i++)
            {
                index += TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colIndexGrantedAdmin));
                date += TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colNoReportDate));
                week += TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colNoReportWeek));
                success += TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colSuccessfulProposal));
            }
            decimal per = TextUtils.ToDecimal(grvPerformance.GetRowCellValue(0, colPerformanceOld));
            performance = per - index / 3 - date / 3 - week / 3 + success / 3;
            grvPerformance.SetRowCellValue(0, colPerformance, performance);
        }
        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
