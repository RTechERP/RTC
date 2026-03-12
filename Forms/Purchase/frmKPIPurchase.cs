using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using Forms.Classes;
using Forms.KPI_PO;
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
    public partial class frmKPIPurchase : _Forms
    {

        public decimal colACCP;

        public frmKPIPurchase()
        {
            InitializeComponent();
        }
        int quy;
        ucKPIStaff ucKPI;
        ucKPIAdmin ucKPIAdmin;
        private void frmProductivityIndex_Load(object sender, EventArgs e)
        {
            try
            {
                cGlobVar.LockEvents = true;
               
                checktime();
                loaduser();
                nbrYear.Value = DateTime.Now.Year;
                loadPositon();
            
                loadReport();
                cbQuy.SelectedIndex = quy;

            }
            finally
            {
                cGlobVar.LockEvents = false;
            }

        }
   
        void loadPositon()
        {
            //    DataTable dt = TextUtils.Select("Select * From [SaleUserType]  ");
            //    cbPosition.DisplayMember = "SaleUserTypeName";
            //    cbPosition.ValueMember = "ID";
            //    cbPosition.DataSource = dt;
        }

        string month;
        void checktime()
        {

            if (0 < DateTime.Now.Month && DateTime.Now.Month < 4)
            {
                month = "1,2,3";
                quy = 0;
            }
            if (3 < DateTime.Now.Month && DateTime.Now.Month < 7)
            {
                month = "4,5,6";
                quy = 1;
            }
            if (6 < DateTime.Now.Month && DateTime.Now.Month < 10)
            {
                month = "7,8,9";
                quy = 2;
            }
            if (9 < DateTime.Now.Month && DateTime.Now.Month < 13)
            {
                month = "10,11,12";
                quy = 3;
            }
        }
        decimal tongG = 0, tongR = 0, tongA = 0;
        void loaduser()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeManager", new string[] { "@group" }, new object[] { TextUtils.ToInt(0) });
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = ds.Tables[2];
        }
        void loadGrvDelivery()
        {
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetDataKPIPuschase", new string[] { "@quy", "@UserID", "@year" }, new object[] { quy, cbUser.EditValue, nbrYear.Value });
            grdDelivery.DataSource = ds.Tables[2];
            grdDiscount.DataSource = ds.Tables[2];
            grdDebt.DataSource = ds.Tables[2];
            grdCheck.DataSource = ds.Tables[2];
        }
        void saveData()
        {
            ReportPurchaseModel model = new ReportPurchaseModel();
            for (int i = 0; i < grvReport.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvReport.GetRowCellValue(i, colIDre));
                if(ID>0)
                model = (ReportPurchaseModel)ReportPurchaseBO.Instance.FindByPK(ID);
                model.UserID = TextUtils.ToInt( cbUser.EditValue);
                model.WorkingDays = TextUtils.ToInt(grvReport.GetRowCellValue(i, colWorkingDays));
                model.Year = TextUtils.ToInt(nbrYear.Value);
                model.Month = TextUtils.ToInt(grvReport.GetRowCellValue(i, colMonth));
                model.Quy = TextUtils.ToInt(quy);
                model.NoReport = TextUtils.ToInt(grvReport.GetRowCellValue(i, colNoReport));
                if (model.ID > 0)
                    PurchaseOrderBO.Instance.Update(model);
                else
                    PurchaseOrderBO.Instance.Insert(model);
            }
        }
        void loadReport()
        {
            DataTable dt = TextUtils.Select($"Select * From ReportPurchase where Quy={quy} And Year ={nbrYear.Value} And UserID={TextUtils.ToInt( cbUser.EditValue)}");

            string[] mon = month.Split(',');
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(new object[] { 0, 0, mon[0], nbrYear.Value, cbUser.EditValue,0, quy });
                dt.Rows.Add(new object[] { 0, 0, mon[1], nbrYear.Value, cbUser.EditValue,0, quy });
                dt.Rows.Add(new object[] { 0, 0, mon[2], nbrYear.Value, cbUser.EditValue,0, quy });
            }
            grdReport.DataSource = dt;
        }


        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;


        }

        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {

            if (cGlobVar.LockEvents) return;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tính toán..."))
            {
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            grvReport.FocusedRowHandle = -1;
            if (quy == cbQuy.SelectedIndex && nbrYear.Value == DateTime.Now.Year);
            saveData();
            loadReport();
        }


        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            frmBonusKPI frm = new frmBonusKPI();
            frm.ShowDialog();
        }

   

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEditKPI frm = new frmEditKPI();
            frm.user = TextUtils.ToInt(cbUser.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        DataSet ds;

        private void cbUser_EditValueChanged_1(object sender, EventArgs e)
        {
            loadGrvDelivery();
        }

        private void grvDebt_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colMonthDebt)
            {
              

            }
        }

        private void grvDebt_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int mon = TextUtils.ToInt(grvDebt.GetRowCellValue(e.RowHandle, colMonDebt));
            switch (mon)
            {
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(188, 238, 104);
                    break;
                case 1:
                    e.Appearance.BackColor = Color.FromArgb(255, 236, 139);
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(255, 211, 155);
                    break;
                default:
                    break;
            }

        }

        private void grvDelivery_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int mon = TextUtils.ToInt(grvDelivery.GetRowCellValue(e.RowHandle, colMonDelivery));
            switch (mon)
            {
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(188, 238, 104);
                    break;
                case 1:
                    e.Appearance.BackColor = Color.FromArgb(255, 236, 139);
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(255, 211, 155);
                    break;
                default:
                    break;
            }
        }

        private void grvDiscount_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int mon = TextUtils.ToInt(grvDiscount.GetRowCellValue(e.RowHandle, colMonDiscount));
            switch (mon)
            {
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(188, 238, 104);
                    break;
                case 1:
                    e.Appearance.BackColor = Color.FromArgb(255, 236, 139);
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(255, 211, 155);
                    break;
                default:
                    break;
            }
        }

        private void grvCheck_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            int mon = TextUtils.ToInt(grvCheck.GetRowCellValue(e.RowHandle, colMoncheck));
            switch (mon)
            {
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(188, 238, 104);
                    break;
                case 1:
                    e.Appearance.BackColor = Color.FromArgb(255, 236, 139);
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(255, 211, 155);
                    break;
                default:
                    break;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadGrvDelivery();
            loadReport();
        }

        private void btnEditMainIndex_Click(object sender, EventArgs e)
        {
            DataTable dt = TextUtils.Select($"Select g.MainIndexID from GroupSalesUser gu inner join GroupSales g on g.ID=gu.GroupSalesID where gu.UserID={cbUser.EditValue}");
            frmEditPercent frm = new frmEditPercent();
            if (dt.Rows.Count > 0)
            {
                frm.user = TextUtils.ToInt(cbUser.EditValue);
                frm.main = TextUtils.ToString(dt.Rows[0]["MainIndexID"]);
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
              

            }
        }
        string Position;
    }
}
