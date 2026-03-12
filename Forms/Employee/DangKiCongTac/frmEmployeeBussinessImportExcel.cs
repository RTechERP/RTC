using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeBussinessImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        public frmEmployeeBussinessImportExcel()
        {
            InitializeComponent();
        }

        private void frmEmployeeBussinessImportExcel_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
			grvData.Columns.Clear();
			OpenFileDialog ofd = new OpenFileDialog();
			if (chkAutoCheck.Checked)
			{
				OpenFileDialog openFileDialog1 = new OpenFileDialog();
				var result = openFileDialog1.ShowDialog();
				if (result == DialogResult.OK)
				{
					btnBrowse.Text = openFileDialog1.FileName;
				}
				else if (result == DialogResult.Cancel)
				{
					return;
				}

				try
				{
					var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

					var sw = new Stopwatch();
					sw.Start();

					IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

					var openTiming = sw.ElapsedMilliseconds;

					ds = reader.AsDataSet(new ExcelDataSetConfiguration()
					{
						UseColumnDataType = false,
						ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
						{
							UseHeaderRow = false
						}
					});
					//toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

					var tablenames = GetTablenames(ds.Tables);

					cboSheet.DataSource = tablenames;

					if (tablenames.Count > 0)
						cboSheet.SelectedIndex = 0;
					btnSave.Enabled = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

			}
			else
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					btnBrowse.Text = ofd.FileName;
					cboSheet.DataSource = null;
					cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
					btnSave.Enabled = true;
				}
			}
		}
		private static IList<string> GetTablenames(DataTableCollection tables)
		{
			var tableList = new List<string>();
			foreach (var table in tables)
			{
				tableList.Add(table.ToString());
			}
			return tableList;
		}

        private void btnSave_Click(object sender, EventArgs e)
        {
			if (grdData.DataSource == null)
			{
				MessageBox.Show("Vui lòng chọn Sheet");
				return;
			}
			if (backgroundWorker1.IsBusy)
			{
				backgroundWorker1.CancelAsync();
			}
			else
			{
				progressBar1.Minimum = 1;
				progressBar1.Maximum = grvData.RowCount;
				txtRate.Text = "";
				start = DateTime.Now;
				enableControl(false);
				backgroundWorker1.RunWorkerAsync();
			}
		}
		void enableControl(bool enable)
		{
			btnSave.Enabled = enable;
			grdData.Enabled = enable;
			cboSheet.Enabled = enable;
			btnBrowse.Enabled = enable;
		}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
			txtRate.Text = "";

			int rowCount = grvData.RowCount;
			for (int i = 1; i < rowCount; i++)
			{
				try
				{
					if (i < 1) continue;
					string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
					//if (HasDiacritics(productCode)) continue;
					//if (productCode.Contains( "MÃ SẢN PHẨM") || productCode.Contains(" ")) continue;
					progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
					txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
					EmployeeBussinessModel model = new EmployeeBussinessModel();

					ArrayList array = EmployeeBussinessBO.Instance.FindByAttribute("CollectDay", TextUtils.ToString(grvData.GetRowCellValue(i, "F1")));
					if (array.Count > 0)
					{
						model = (EmployeeBussinessModel)array[0];
					}
					if (grvData.GetRowCellValue(i, "F3").ToString() != "")
					{
						string employee = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());
						int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"select ID from Employee where Code = N'{employee}'"));
						model.EmployeeID = id;
						//model.CollectDay = TextUtils.ToDate3(grvData.GetRowCellValue(i, "F1"));
						model.TotalMoney = TextUtils.ToInt(grvData.GetRowCellValue(i, "F4"));
						//model.Notes = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
						DataTable dt = TextUtils.Select($"select * from EmployeeCollectMoney where EmployeeID = '{model.EmployeeID}'");
						if (dt.Rows.Count > 0)
						{
							EmployeeBussinessBO.Instance.Update(model);
						}
						else
						{
							EmployeeBussinessBO.Instance.Insert(model);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
				}

			}
		}
    }
}
