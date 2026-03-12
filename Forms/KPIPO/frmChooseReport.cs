using BMS.Business;
using BMS.Model;
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
using System.Windows.Forms;

namespace BMS
{
    public partial class frmChooseReport : _Forms
    {
        public frmChooseReport()
        {
            InitializeComponent();
        }

        private void frmChooseReport_Load(object sender, EventArgs e)
        {

            try
            {
                cGlobVar.LockEvents = true;
                loadCompare();
                checktime();
                loadgroupSale();
                loadcbPosition();


            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }
        int  quy;
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
        /// <summary>
        /// Tạo cb dấu so sánh
        /// </summary>
        void loadCompare()
        {
            DataTable dtMax = new DataTable();
            DataTable dtMin = new DataTable();
            dtMin.Columns.Add("ID");
            dtMin.Columns.Add("Compare");
            dtMin.Rows.Add(0, "<");
            dtMin.Rows.Add(1, "<=");
     
            dtMax = dtMin.Copy();
            cbCompare.DataSource = dtMin;
            cbCompare.ValueMember = "ID";
            cbCompare.DisplayMember = "Compare";
            cbCompareMAX.DataSource = dtMax;
            cbCompareMAX.ValueMember = "ID";
            cbCompareMAX.DisplayMember = "Compare";
        }

        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,[GroupSalesName] From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }
        void loadcbPosition()
        {
            DataTable dt = TextUtils.Select($"Select ID,SaleUserTypeName From [SaleUserType] ");
            cbPosition.Properties.DisplayMember = "SaleUserTypeName";
            cbPosition.Properties.ValueMember = "ID";
            cbPosition.Properties.DataSource = dt;
        }
        void loadgrvData()
        {
            DataTable dt = TextUtils.Select($"Select * From BonusRule where GroupSaleID={cbGroup.EditValue} and SaleUserTypeID={cbPosition.EditValue}");
            grdData.DataSource = dt;
            txtPercent.EditValue = TextUtils.ToDecimal(TextUtils.ExcuteScalar($"Select PercentBonus From BonusRuleIndex where GroupSalesID = {cbGroup.EditValue} and SaleUserTypeID={cbPosition.EditValue}"));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            BonusRuleModel bonus = new BonusRuleModel();
            BonusRuleIndexModel bonusIndex = new BonusRuleIndexModel();
            if (lstIDDelete.Count > 0)
                BonusRuleBO.Instance.Delete(lstIDDelete);
            int index = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select ID From BonusRuleIndex where GroupSalesID = {cbGroup.EditValue} and SaleUserTypeID={cbPosition.EditValue}"));
            if (index > 0)
                bonusIndex = (BonusRuleIndexModel)BonusRuleIndexBO.Instance.FindByPK(index);
            bonusIndex.PercentBonus = TextUtils.ToDecimal(txtPercent.EditValue);
            bonusIndex.GroupSalesID = TextUtils.ToInt(cbGroup.EditValue);
            bonusIndex.SaleUserTypeID = TextUtils.ToInt(cbPosition.EditValue);
            if (bonusIndex.ID > 0)
                BonusRuleIndexBO.Instance.Update(bonusIndex);

            else
                BonusRuleIndexBO.Instance.Insert(bonusIndex);



            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (ID > 0)
                    bonus = (BonusRuleModel)BonusRuleBO.Instance.FindByPK(ID);
                bonus.Max = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colMax));
                bonus.Min = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colMIN));
                bonus.Value = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colValue));
                bonus.GroupSaleID = TextUtils.ToInt(cbGroup.EditValue);
                bonus.SaleUserTypeID = TextUtils.ToInt(cbPosition.EditValue);
                bonus.CompareMAX = TextUtils.ToInt(grvData.GetRowCellValue(i, colCompareMIN));
                bonus.CompareMIN = TextUtils.ToInt(grvData.GetRowCellValue(i, colCompareMIN));
                bonus.Quy = TextUtils.ToInt(quy);
                bonus.Year = TextUtils.ToInt(DateTime.Now.Year);
                if (bonus.ID > 0)
                    BonusRuleBO.Instance.Update(bonus);
                else
                    BonusRuleBO.Instance.Insert(bonus);
            }
            MessageBox.Show("Đã cất thành công !", "A", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadgrvData();
        }

        private void cbPosition_EditValueChanged(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            loadgrvData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            BonusRuleBO.Instance.Delete(ID);
            grvData.DeleteSelectedRows();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            grvData.AddNewRow();
        }
        ArrayList lstIDDelete = new ArrayList();
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            lstIDDelete.Add(ID);
            grvData.DeleteSelectedRows();
        }
    }
}
