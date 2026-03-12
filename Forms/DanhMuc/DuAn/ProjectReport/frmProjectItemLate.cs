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
    public partial class frmProjectItemLate : _Forms
    {
        public frmProjectItemLate()
        {
            InitializeComponent();
        }

        private void frmProjectItemLate_Load(object sender, EventArgs e)
        {
            dtpStartDate.EditValue = TextUtils.ToDate5(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            dtpEndDate.EditValue = TextUtils.ToDate5(dtpStartDate.EditValue).AddMonths(1).AddSeconds(-1);
            loadDepartment();
            loadProject();
            loadUser();
            //cboUser.EditValue = Global.UserID;

            loadData();

        }
        void loadData()
        {
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetProjectItemLate", "A",
                new string[] { "@FilterText", "@UserID", "@ProjectID", "@DepartmentID", "@StartDate", "@EndDate" },
                new object[] { txtFind.Text.Trim(), TextUtils.ToInt(cboUser.EditValue), TextUtils.ToInt(cboProject.EditValue), TextUtils.ToInt(cboDepartment.EditValue), TextUtils.ToDate5(dtpStartDate.EditValue), TextUtils.ToDate5(dtpEndDate.EditValue) });
        }
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Users");
            cboUser.Properties.DataSource = dt;
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
        }
        void loadProject()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Project");
            cboProject.Properties.DataSource = dt;
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
        }
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Department");
            cboDepartment.Properties.DataSource = dt;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptions optionsEx = new XlsExportOptions();
                //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/HangMucCongViecChamTienDo_{DateTime.Now.ToString("ddMMyy")}.xls";
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

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void stackPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }
    }
}