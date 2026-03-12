using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Sale
{
    public partial class frmPendingInAndOutReport : _Forms
    {
        string _warehouseCode = "";
        public frmPendingInAndOutReport(string warehouseCode)
        {
            InitializeComponent();
            _warehouseCode = warehouseCode;
        }
        private void frmPendingInAndOutReport_Load(object sender, EventArgs e)
        {
            loadProductGroup();
        }
        private void loadProductGroup()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM dbo.ProductGroup where IsVisible = 1");
            treeList.DataSource = dt;
        }
        private void LoadGrdData()
        {
            int IDTree = TextUtils.ToInt(treeList.FocusedNode.GetValue(treeListColumn1));

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            if (chkAllBillExport.Checked) IDTree = -1; 

            DataTable dt = TextUtils.LoadDataFromSP("spGetDataPendingImportExportReport", "A",
                new string[] { "@StartDate", "@EndDate", "@Group", "@Find", "@WarehouseCode" },
                new object[] { dateTimeS, dateTimeE, IDTree, txtFilterText.Text.Trim(), _warehouseCode });
            grdMaster.DataSource = dt;
        }

        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string Id = TextUtils.ToString(treeList.FocusedNode.GetValue(colID));
            LoadGrdData();
        }

        private void chkAllBillExport_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrdData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadGrdData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyLib.ExportExcelGrid(grvMaster);
        }
    }
}
