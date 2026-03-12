using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmPartlistProblem : _Forms
    {   
        public int projectID = 0;
        public frmPartlistProblem()
        {
            InitializeComponent();
        }

        private void frmPartlistProblem_Load(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            dtpFromDate.Value = new DateTime(datenow.Year, datenow.Month, 1);
            dtpEndDate.Value = dtpFromDate.Value.AddMonths(+1).AddDays(-1);

            txtPageNumber.Text = "1";
            LoadProject();
            LoadPartlist();
        }

        private void LoadPartlist()
        {
           
            projectID = TextUtils.ToInt(cboProject.EditValue);        
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetProjectPartlistProblem"
                   , new string[] { "@PageSize", "@PageNumber", "@DateStart", "@DateEnd", "@FilterText", "@ProjectID" }
                   , new object[] { TextUtils.ToInt(txtPageSize.Text), TextUtils.ToInt(txtPageNumber.Text), dateTimeS, dateTimeE, txtFilterText.Text.Trim(),projectID });
            grdData.DataSource = oDataSet.Tables[0];
            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);
            txtShowCount.Text = TextUtils.ToString(oDataSet.Tables[2].Rows[0]["TotalEntries"]) + " Entries";
        }

        private void LoadProject()
        {
            var listProject = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.DataSource = listProject;
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";
            cboProject.EditValue = projectID;
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadPartlist();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadPartlist();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadPartlist();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadPartlist();
        }

        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (txtPageSize.Text == "")
                return;
            else
            {
                txtPageNumber.Text = "1";
                LoadPartlist();
            }
        }

        private void txtPageSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadPartlist();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadPartlist();
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
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
    }
}