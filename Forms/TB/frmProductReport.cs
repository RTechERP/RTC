using BMS;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace BMS
{
    public partial class frmProductReport : _Forms
    {
        public frmProductReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime From = new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day, 0, 0, 0);
            DateTime To = new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day, 23, 59, 59);
            DataTable tblReport = new DataTable();
            tblReport.Columns.Add("ProductID", typeof(System.Int16));
            tblReport.Columns.Add("DateReport", typeof(System.DateTime));
            tblReport.Columns.Add("ProductCode", typeof(System.String));
            tblReport.Columns.Add("ProductName", typeof(System.String));
            tblReport.Columns.Add("Maker", typeof(System.String));
            tblReport.Columns.Add("AddressBox", typeof(System.String));
            tblReport.Columns.Add("Input", typeof(System.Int16));
            tblReport.Columns.Add("Output", typeof(System.Int16));
            tblReport.Columns.Add("Note", typeof(System.String));
            DataTable tblProductInput = SqlHelper.ExecuteDataset(DBUtils.GetDBConnectionString(), 
                CommandType.Text, 
                String.Format("Select * From ProductRTC Where CreateDate <= N'{0}' And CreateDate >= N'{1}'", 
                To.ToString("yyyy/MM/dd HH:mm:ss"), From.ToString("yyyy/MM/dd HH:mm:ss"))).Tables[0];
            DataTable tblProductOutput = SqlHelper.ExecuteDataset(DBUtils.GetDBConnectionString(), 
                CommandType.Text, 
                String.Format("Select h.DateBorrow, h.NumberBorrow, h.Note, p.ID, P.ProductCode, p.ProductName, p.Maker, p.AddressBox " +
                "From HistoryProductRTC h Inner Join ProductRTC p On h.ProductRTCID = p.ID Where DateBorrow <= N'{0}' And DateBorrow >= N'{1}'", 
                To.ToString("yyyy/MM/dd HH:mm:ss"), From.ToString("yyyy/MM/dd HH:mm:ss"))).Tables[0];
            if (tblProductInput != null && tblProductInput.Rows.Count > 0)
            {
                for (int i = 0; i < tblProductInput.Rows.Count; i ++)
                {
                    DataRow row = tblReport.NewRow();
                    row["ProductID"] = tblProductInput.Rows[i]["ID"];
                    row["DateReport"] = tblProductInput.Rows[i]["CreateDate"];
                    row["ProductCode"] = tblProductInput.Rows[i]["ProductCode"];
                    row["ProductName"] = tblProductInput.Rows[i]["ProductName"];
                    row["Maker"] = tblProductInput.Rows[i]["Maker"];
                    row["AddressBox"] = tblProductInput.Rows[i]["AddressBox"];
                    row["Input"] = tblProductInput.Rows[i]["Number"];
                    row["Note"] = tblProductInput.Rows[i]["Note"];

                    tblReport.Rows.Add(row);
                }
            }
            if (tblProductOutput != null && tblProductOutput.Rows.Count > 0)
            {
                for (int i = 0; i < tblProductOutput.Rows.Count; i++)
                {
                    DataRow row = tblReport.NewRow();
                    row["ProductID"] = tblProductOutput.Rows[i]["ID"];
                    row["DateReport"] = tblProductOutput.Rows[i]["DateBorrow"];
                    row["ProductCode"] = tblProductOutput.Rows[i]["ProductCode"];
                    row["ProductName"] = tblProductOutput.Rows[i]["ProductName"];
                    row["Maker"] = tblProductOutput.Rows[i]["Maker"];
                    row["AddressBox"] = tblProductOutput.Rows[i]["AddressBox"];
                    row["Output"] = tblProductOutput.Rows[i]["NumberBorrow"];
                    row["Note"] = tblProductOutput.Rows[i]["Note"];
                    tblReport.Rows.Add(row);
                }
            }
            grdData.DataSource = tblReport;
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void frmProductReport_Load(object sender, EventArgs e)
        {

        }

        private void grvData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

        }
    }
}
