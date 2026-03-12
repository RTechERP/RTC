using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class FrmAddReportType : _Forms
    {
        ReportTypeModel reportTypeModel = new ReportTypeModel();
        public FrmAddReportType()
        {
            InitializeComponent();
        }
        private void FrmAddReportType_FormClosing(object sender, FormClosingEventArgs e)
        {
            Close();
            this.DialogResult = DialogResult.OK;
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                reportTypeModel = new ReportTypeModel();
                txtReportType.Clear();
            }
            
        }
        private bool SaveData()
        {
            if (string.IsNullOrEmpty(txtReportType.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập loại cáo cáo!");
                return false;
            }

            //var exp1 = new Expression("ReportTypeName", txtContentReport.Text.Trim());
            //var lstReportType = SQLHelper<ReportTypeModel>.FindByExpression();
            var reportTypeExist = SQLHelper<ReportTypeModel>.FindAll().FirstOrDefault(x => x.ReportTypeName.ToLower() == txtReportType.Text.Trim().ToLower());
            if (reportTypeExist != null)
            {
                MessageBox.Show("Loại báo cáo đã tồn tại!");
                return false;
            }
            reportTypeModel.ReportTypeName = txtReportType.Text.Trim();
            ReportTypeBO.Instance.Insert(reportTypeModel);
            return true;
        }
    }
}
