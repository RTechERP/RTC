using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
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
    public partial class frmQuotationSale : _Forms
    {
        int _rownIndex = 0;
        public frmQuotationSale()
        {
            InitializeComponent();
        }

        private void frmQuotationSale_Load(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            dtpDateStart.Value = new DateTime(now.Year, now.Month, 1);
            dtpDateEnd.Value = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
            loadAdminSale();
            loadCustomer();
            loadData();
            loadProject();
        }
        void loadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.DataSource = list;
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";

        }
        void loadAdminSale()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindAll();
            cboUser.Properties.DataSource = list;
            cboUser.Properties.DisplayMember = "Code";
            cboUser.Properties.ValueMember = "ID";
        }
        void loadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindAll();
            cboCustomer.Properties.DataSource = list;
            cboCustomer.Properties.DisplayMember = "CustomerCode";
            cboCustomer.Properties.ValueMember = "ID";
        }
        void loadData()
        {
            txtPageNumber.Text = "1";
            txtTotalPage.Text = "1";

            DataSet oDataSet = loadDataSet();

            if (_rownIndex >= grvMaster.RowCount)
                _rownIndex = 0;
            if (_rownIndex > 0)
                grvMaster.FocusedRowHandle = _rownIndex;
            grvMaster.SelectRow(_rownIndex);

            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
        }
        DataSet loadDataSet()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetQuotationPaging"
                    , new string[] { "PageNumber", "PageSize", "CustomerID", "Status", "SaleID", "FilterText", "ProjectID", "DateStart", "DateEnd" }
                    , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt( txtPageSize.Value)
                        ,TextUtils.ToInt(cboCustomer.EditValue),-1,TextUtils.ToInt(cboUser.EditValue), txtFilterText.Text.Trim(), TextUtils.ToInt(cboProject.EditValue), dtpDateStart.Value, dtpDateEnd.Value });
            oDataSet.Tables[0].Columns.Add("DescriptionText", typeof(System.String));
            foreach (DataRow row in oDataSet.Tables[0].Rows)
            {
                List<QuotationTermLinkModel> list = SQLHelper<QuotationTermLinkModel>.FindByAttribute("QuotationID", row["ID"]);
                string descriptionText = "";
                for (int i = 0; i < list.Count; i++)
                {
                    TermConditionModel term = SQLHelper<TermConditionModel>.FindByID(list[i].TermConditionID);
                    descriptionText = descriptionText + " * " + term.DescriptionVietnamese + " \n * " + term.DescriptionEnglish + "\n";
                }
                descriptionText = descriptionText.TrimEnd('\n');
                row["DescriptionText"] = descriptionText;
            }
            grdMaster.DataSource = oDataSet.Tables[0];

            return oDataSet;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var rowHandle = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            QuotationModel quotation = SQLHelper<QuotationModel>.FindByID(id);
            frmQuotationSaleDetail frm = new frmQuotationSaleDetail();
            frm.quotation = quotation;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvMaster.FocusedRowHandle = rowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadDataSet();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadDataSet();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadDataSet();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadDataSet();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        public void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetQuotationDetail_ByMasterID", "A"
                   , new string[] { "@QuotationID" }
                   , new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colID)) });
            TreeData.DataSource = dt;
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var rowHandle = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            QuotationModel quotation = SQLHelper<QuotationModel>.FindByID(id);
            frmQuotationSaleViewDetail frm = new frmQuotationSaleViewDetail();
            frm.quotationID = quotation.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void grdMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
                var copy = grvMaster.GetFocusedValue();
                Clipboard.SetText(TextUtils.ToString(copy));
            }
        }
    }
}