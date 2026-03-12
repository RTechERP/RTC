using BMS.Model;
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

namespace BMS
{
    public partial class frmPOKHHistory : _Forms
    {
        public frmPOKHHistory()
        {
            InitializeComponent();
        }

        private void frmPOKHHistory_Load(object sender, EventArgs e)
        {
            LoadCustomer();
            LoadData();
        }
        private void LoadCustomer()
        {
            List<CustomerModel> lst = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0);
            cboCustomer.Properties.DataSource = lst;
            cboCustomer.Properties.DisplayMember = "CustomerCode";
            cboCustomer.Properties.ValueMember = "ID";
        }
        private void LoadData()
        {
            string keywords = txtKeywords.Text.Trim();
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;
            string cusCode = TextUtils.ToString(cboCustomer.Text);
            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHHistory", "lmktable", new string[] { "@Keywords", "@PODateStart", "@PODateEnd", "@CustomerCode" },
                                                                                    new object[] { keywords, startDate, endDate, cusCode });
            grdData.DataSource = dt;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null,null);
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmImportPOKHHistory frm = new frmImportPOKHHistory();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                int rowHandle = grvData.FocusedRowHandle;
                LoadData();
                grvData.FocusedRowHandle = rowHandle;

            }
        }
    }
}
