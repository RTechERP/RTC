using BMS.Business;
using BMS.Model;
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

namespace BMS
{
    public partial class frmEditPercent : _Forms
    {
        public frmEditPercent()
        {
            InitializeComponent();
        }
        public string main;
        public int user;
        private void frmChooseReport_Load(object sender, EventArgs e)
        {

            try
            {
                cGlobVar.LockEvents = true;
                cbUser.EditValue = user;
                checktime();
                loaduser();
                loadgrvData();
            }
            finally
            {
                cGlobVar.LockEvents = false;
            }
        }
        void loaduser()
        {
            DataTable dt = TextUtils.Select("Select u.FullName,u.ID,G.SaleUserTypeID,s.SaleUserTypeName From GroupSalesUser G Inner join Users u On u.ID = g.UserID Inner join SaleUserType s on s.ID=g.SaleUserTypeID");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        int quy;
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
        void loadgrvData()
        {
            try
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetMainIndexPercent", "A", new string[] { "@Main", "@userID" }, new object[] { main, cbUser.EditValue });
                grdData.DataSource = dt;
                grvData.ExpandAllGroups();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PercentMainIndexUserModel bonus = new PercentMainIndexUserModel();
            MainIndexModel main = new MainIndexModel();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                int MainID = TextUtils.ToInt(grvData.GetRowCellValue(i, colMainID));
                //Sửa phần trăm
                if (ID > 0)
                    bonus = (PercentMainIndexUserModel)PercentMainIndexUserBO.Instance.FindByPK(ID);
                bonus.UserID = TextUtils.ToInt(cbUser.EditValue);
                bonus.MainIndexID = TextUtils.ToInt(MainID);
                bonus.PercentIndex = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colper));
                bonus.Quy = TextUtils.ToInt(quy);
                bonus.Year = TextUtils.ToInt(DateTime.Now.Year);
                if (ID > 0)
                    PercentMainIndexUserBO.Instance.Update(bonus);
                else
                    PercentMainIndexUserBO.Instance.Insert(bonus);
            }
            MessageBox.Show("Đã cất thành công ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadgrvData();
        }
  
 

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void cbUser_EditValueChanged_1(object sender, EventArgs e)
        {
            if (cGlobVar.LockEvents) return;
            DataTable dt = TextUtils.Select($"Select g.MainIndexID from GroupSalesUser gu inner join GroupSales g on g.ID=gu.GroupSalesID where gu.UserID={cbUser.EditValue}");
            if (dt.Rows.Count > 0)
                main = TextUtils.ToString(dt.Rows[0]["MainIndexID"]);
            loadgrvData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvData.FocusedRowHandle++;
            }
        }
    }
}
