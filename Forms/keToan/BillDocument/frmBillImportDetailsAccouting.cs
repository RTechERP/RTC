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
    public partial class frmBillImportDetailsAccouting : _Forms
    {
        public frmBillImportDetailsAccouting()
        {
            InitializeComponent();
        }

        private void frmBillImportDetailsAccouting_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddSeconds(-1);
            LoadStatus();
            LoadData();
        }
        private void LoadStatus()
        {
            List<object> lst = new List<object>()
            {
                new {ID = -1, Text = "---Tất cả---"},
                new {ID = 0, Text = "Chưa duyệt"},
                new {ID = 1, Text = "Đã duyệt"}
            };
            cboStatus.DataSource = lst;
            cboStatus.DisplayMember = "Text";
            cboStatus.ValueMember = "ID";
            cboStatus.SelectedValue = -1;
        }
        private void LoadData()
        {
            int status = TextUtils.ToInt(cboStatus.SelectedValue);
            DateTime dateStart = dtpDateStart.Value;
            DateTime dateEnd = dtpDateEnd.Value;
            string keyWords = txtFilterText.Text.Trim();
            DataTable dt = SQLHelper<BillImportDetailModel>.LoadDataFromSP("spGetAllBillImportWithBillImportDetails", 
                                                                            new string[] { "@DateStart", "@DateEnd", "@Status", "@Keywords" }, 
                                                                            new object[] { dateStart, dateEnd, status , keyWords });
            grdData.DataSource = dt;
        }

        private void btnImportBill_Click(object sender, EventArgs e)
        {
            int[] lstSelected = grvData.GetSelectedRows();
            if (lstSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng để nhập hóa đơn!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmInvoice frm = new frmInvoice();
            foreach (int index in lstSelected)
            {
                BillImportDetailModel model = SQLHelper<BillImportDetailModel>.FindByID(TextUtils.ToInt(grvData.GetRowCellValue(index, colBillImportDetailID)));
                if (model.ID <= 0) continue;
                if (!frm.billDetails.Contains(model)) 
                {
                    frm.billDetails.Add(model);
                };
            }
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void frmBillImportDetailsAccouting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }
    }
}
