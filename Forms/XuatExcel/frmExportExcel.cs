using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Forms;
using System.Diagnostics;
using System.Collections;
using DevExpress.Data.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
	public partial class frmExportExcel : _Forms
	{                                                                                                                                                                                                                                                                                                                                                   
		public int Status = -1;
		public frmExportExcel()
		{
			InitializeComponent();
		}
		private void frmListTool_Load(object sender, EventArgs e)
		{
			DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
			dtpFromDate.Value = datenow.AddMonths(-2);
			FindData();
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			FindData();
		}
		private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				FindData();
			}
		}
		private void FindData()
		{

            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataTable dt = TextUtils.LoadDataFromSP("spLoadXuatExcel", "A", new string[] { "@DateStart", "@DateEnd" }, new object[] { dateTimeS, dateTimeE });
            DataColumn data = new DataColumn("Location", typeof(Byte[]));
            dt.Columns.Add(data);
            grdData.DataSource = dt;
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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
			GridView view = sender as GridView;
			if (e.Control && e.KeyCode == Keys.C)
			{
				Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
				e.Handled = true;
			}
		}
    }
}

