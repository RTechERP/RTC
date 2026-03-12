using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Card;
using Forms.KPI_PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.PO
{
    public partial class frmViewHistoryMoney : _Forms
    {
        int warehouseID = 1;
        public frmViewHistoryMoney()
        {
            InitializeComponent();
        }

        private void frmViewPOKH_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-1);
 
            loadgroupSale();
            loadCustomer();
            loadUser();
        
            loadPOKH();
        }
        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,[GroupSalesName] From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }

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



        /// <summary>
        /// load POKH
        /// </summary>
        private void loadPOKH()
        {
            try
            {
                DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
                DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

                DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryMoney", "A"
                         , new string[] { "@UserID", "@Customer", "@GroupSaleID", "@DateS", "@DateE" }
                         , new object[] {TextUtils.ToInt( cbUser.EditValue) , TextUtils.ToInt(cbCustomer.EditValue), TextUtils.ToInt(cbGroup.EditValue),dateTimeS,dateTimeE    });
                grdMaster.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPOKH();
        }

     
       


        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void cbStatus_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }


        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MyLib.ExportExcelGrid(grvMaster);
        }


        //The Anh 28072022
        private void tiềnVềToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistoryMoney frm = new frmHistoryMoney();
            frm.ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPOKHID));
            if(frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
            }    
        }

        private void chiTiếtPOKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPOKHID));
            if (id == 0) return;
            POKHModel model = (POKHModel)POKHBO.Instance.FindByPK(id);
            frmPOKHDetail frm = new frmPOKHDetail(warehouseID);
            frm.oPOKH = model;
            frm.ID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPOKH();
            }    
        }
    }
}
