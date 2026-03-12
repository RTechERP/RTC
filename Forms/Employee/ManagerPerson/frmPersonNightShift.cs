using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
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
    public partial class frmPersonNightShift : _Forms
    {
        public frmPersonNightShift()
        {
            InitializeComponent();
        }

        private void frmPersonNightShift_Load(object sender, EventArgs e)
        {
            loadDepartment();
            txtPageNumber.Text = "1";
            if(Global.DepartmentID > 0)
            {
                cboDepartment.EditValue = Global.DepartmentID;
            }
            
            cbApprovedStatusTBP.SelectedIndex = 0;
            loadData();
        }
        void loadData()
        {
            DateTime dateStart = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59);
            int approvedTBP = cbApprovedStatusTBP.SelectedIndex - 1;
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeNightShift",
                new string[] { "@EmployeeID", "@DateStart", "@DateEnd", "@DepartmentID", "@Keyword", "@PageNumber", "@PageSize" },
                new object[] { Global.EmployeeID, dateStart, dateEnd, TextUtils.ToInt(cboDepartment.EditValue), txtKeyword.Text.Trim(), TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value) });

            grdData.DataSource = ds.Tables[1];
            txtTotalPage.Text = TextUtils.ToString(ds.Tables[2].Rows[0]["TotalPage"]);
        }
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            cboDepartment.Properties.DataSource = dt;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "")
                return;
            else
            {
                txtPageNumber.Text = "1";
                loadData();
            }
        }

        private void txtPageSize_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();

                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/DanhSachLamDem_T{dtpStart.Value.ToString("ddMMyy")}_{dtpEnd.Value.ToString("ddMMyy")}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvData.ClearSelection();
            }
        }
    }
}