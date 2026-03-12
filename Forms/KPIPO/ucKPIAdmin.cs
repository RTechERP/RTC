using BMS;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.KPI_PO
{
    public partial class ucKPIAdmin : UserControl
    {
        public SearchLookUpEdit cbUser { get; set; }
        public GridControl grvPerformance { get; set; }
        public GridControl grvData { get; set; }
        public System.Windows.Forms.ComboBox cbMonth { get; set; }
        public System.Windows.Forms.NumericUpDown nbrYear { get; set; }
        public DataTable dtadmin;
        public DataTable dtMonth;
        public decimal TotalSale;
        public decimal SumActual;
        public event cGlobVar.SendData Send;
        public DataTable dtKPIAdmin;
        public ucKPIAdmin()
        {
            InitializeComponent();
        }

        private void grdAdmin_Click(object sender, EventArgs e)
        {

        }

        private void ucKPIAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
                loadData();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }


        }
        DataSet ds;
        void loadData()
        {
            ds = TextUtils.LoadDataSetFromSP("spGetAdminMarketing", new string[] { "@quy", "@UserID", "@year" }, new object[] { cbMonth.SelectedIndex, cbUser.EditValue, nbrYear.Value });
            dtKPIAdmin = ds.Tables[1];
            grdAdmin.DataSource = dtKPIAdmin;
            dtadmin = ds.Tables[1];
            dtMonth = ds.Tables[0];
            grvAdmin.ExpandAllGroups();
            colMonth1.Caption = "Tháng " + ds.Tables[0].Rows[0]["MonthReport"];
            colMonth2.Caption = "Tháng " + ds.Tables[0].Rows[1]["MonthReport"];
            colMonth3.Caption = "Tháng " + ds.Tables[0].Rows[2]["MonthReport"];
            tongket();


        }
        int sum;
        void tongket()
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                int mon1 = TextUtils.ToInt(grvAdmin.GetRowCellValue(i, colMonth1));
                int mon2 = TextUtils.ToInt(grvAdmin.GetRowCellValue(i, colMonth2));
                int mon3 = TextUtils.ToInt(grvAdmin.GetRowCellValue(i, colMonth3));
                int qty = TextUtils.ToInt(grvAdmin.GetRowCellValue(i, colQuantity));
                decimal com = TextUtils.ToInt(grvAdmin.GetRowCellValue(i, colCompletionRate));
                sum = mon1 + mon2 + mon3;
                if (sum > 0)
                {
                    grvAdmin.SetRowCellValue(i, colQuantityActual, sum);
                    grvAdmin.SetRowCellValue(i, colPercentActual, (decimal)sum / qty * com);
                }
            }
            SumActual = TextUtils.ToDecimal(colPercentActual.SummaryItem.SummaryValue);
        }

        private void grvAdmin_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            if (e.Column == colMonth1 || e.Column == colMonth2 || e.Column == colMonth3)
            {
                int mon1 = TextUtils.ToInt(grvAdmin.GetFocusedRowCellValue(colMonth1));
                int mon2 = TextUtils.ToInt(grvAdmin.GetFocusedRowCellValue(colMonth2));
                int mon3 = TextUtils.ToInt(grvAdmin.GetFocusedRowCellValue(colMonth3));
                int qty = TextUtils.ToInt(grvAdmin.GetFocusedRowCellValue(colQuantity));
                decimal com = TextUtils.ToInt(grvAdmin.GetFocusedRowCellValue(colCompletionRate));
                sum = mon1 + mon2 + mon3;
                if (sum > 0)
                {
                    grvAdmin.SetFocusedRowCellValue(colQuantityActual, sum);
                    grvAdmin.SetFocusedRowCellValue(colPercentActual, (decimal)sum / qty * com);
                }
            }
            TotalSale = TextUtils.ToDecimal(colPercentActual.SummaryItem.SummaryValue);
            SumActual = TextUtils.ToDecimal(colPercentActual.SummaryItem.SummaryValue);
            this.Send?.Invoke("A");
        }
    }
}
