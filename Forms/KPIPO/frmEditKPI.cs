using BMS.Business;
using BMS.Model;
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
    public partial class frmEditKPI : _Forms
    {
        public int user;
        public frmEditKPI()
        {
            InitializeComponent();
        }

        private void frmPosition_Load(object sender, EventArgs e)
        {
            loaduser();
            loadcbKPI();
            loadGrvData();
            cbUser.EditValue = user;
            cbKPI.EditValueChanged += new EventHandler(cbKPI_EditValueChange);
        }
        DataTable dtkpi;
        void loadcbKPI()
        {
            dtkpi = TextUtils.Select($"Select ID as IDKPI ,KPI,Note,Case GroupSalesID when 2 then 'MRO' when 3 then 'Samsung' end as MGroup From KPIDetail");
            cbKPI.DisplayMember = "KPI";
            cbKPI.ValueMember = "IDKPI";
            cbKPI.DataSource = dtkpi;
            grvCbKPI.ExpandAllGroups();
        }
        void loaduser()
        {
            DataTable dt = TextUtils.Select($"Select u.FullName,u.ID,G.SaleUserTypeID From GroupSalesUser G Inner join Users u On u.ID = g.UserID ");
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        void loadGrvData()
        {
            DataTable dt = TextUtils.Select($"Select ku.*,k.KPI,k.Note,K.ID as IDKPI From KPIDetailUser ku inner join KPIDetail k on ku.KPIID = k.ID Where ku.UserID = {cbUser.EditValue} ");
            grdReport.DataSource = dt;
        }



        private void cbKPI_EditValueChange(object sender, EventArgs e)
        {
            try
            {
                grvReport.Focus();
                cbUser.Focus();
                int ID = TextUtils.ToInt(grvReport.GetFocusedRowCellValue(colKPI));
                DataRow[] dtr = dtkpi.Select($"IDKPI={ID}");
                if (dtr.Length > 0)
                {
                    string note = TextUtils.ToString(dtr[0]["Note"]);
                    grvReport.SetFocusedRowCellValue(colNote, note);
                }
            }
            catch { }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int month = DateTime.Now.Month;
            if (arrDelete.Count > 0)
                KPIDetailUserBO.Instance.Delete(arrDelete);
            grvReport.FocusedRowHandle = -1;
            KPIDetailUserModel kpi = new KPIDetailUserModel();
            for (int i = 0; i < grvReport.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvReport.GetRowCellValue(i, colID));
                kpi.ID = ID;
                kpi.KPIID = TextUtils.ToInt(grvReport.GetRowCellValue(i, colKPI));
                kpi.PercentKPI = TextUtils.ToDecimal(grvReport.GetRowCellValue(i, colPercent));
                kpi.UserID = TextUtils.ToInt(cbUser.EditValue);
                if (month >=1 && month <=3)
                {
                    kpi.Quy = 1;
                }
                else if (month >= 4 && month <= 6)
                {
                    kpi.Quy = 2;
                }
                else if (month >= 7 && month <= 9)
                {
                    kpi.Quy = 3;
                }
                else
                {
                    kpi.Quy = 4;
                }
                kpi.Year = DateTime.Now.Year;
                if (ID > 0)
                    KPIDetailUserBO.Instance.Update(kpi);
                else
                    KPIDetailUserBO.Instance.Insert(kpi);
            }
            this.Close();
        }

        private void grvReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvReport.FocusedRowHandle = -1;
                grvReport.AddNewRow();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            grvReport.AddNewRow();
        }
        ArrayList arrDelete = new ArrayList();
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvReport.GetFocusedRowCellValue(colID));
                arrDelete.Add(ID);
                grvReport.DeleteSelectedRows();
            }

        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadGrvData();
        }

        private void frmEditKPI_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
