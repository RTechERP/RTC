
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
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmImportBox : _Forms
	{
		public frmImportBox()
		{
			InitializeComponent();
		}
		DateTime start;
		DataSet ds;
		private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

		private void frmImportBox_Load(object sender, EventArgs e)
		{
			btnSave.Enabled = false;
		}

		private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
		{
			grvData.Columns.Clear();
			if (chkAutoCheck.Checked)
			{
				try
				{
					var tablename = cboSheet.SelectedItem.ToString();

					grdData.DataSource = ds; // dataset
					grdData.DataMember = tablename;
				}
				catch (Exception ex)
				{
					TextUtils.ShowError(ex);
					grdData.DataSource = null;
				}
			}
			else
			{
				try
				{
					DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

					grdData.DataSource = dt;
					grvData.PopulateColumns();
					grvData.BestFitColumns();
					grdData.Focus();
				}
				catch (Exception ex)
				{
					TextUtils.ShowError(ex);
					grdData.DataSource = null;
				}
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			txtRate.Text = "";

			int rowCount = grvData.RowCount;
			for (int i = 0; i < rowCount; i++)
			{
				try
				{
					if (i < 1) continue;
					progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
					txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));

					string Box2 = Lib.ToString(grvData.GetRowCellValue(i, "F2"));

					// kiểm tra nếu box 2 trống thì bỏ qua
					if (string.IsNullOrEmpty(Box2.Trim()))
					{
						continue;
					}
					string Box1= Lib.ToString(grvData.GetRowCellValue(i, "F1"));
					TextUtils.ExcuteProcedure("spUpdateBox", new string[] { "@Box1", "@Box2" }, new object[] { Box1.Trim(), Box2.Trim() });
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
				}

			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
			enableControl(true);
		}

		void enableControl(bool enable)
		{
			btnSave.Enabled = enable;
			grdData.Enabled = enable;
			cboSheet.Enabled = enable;
			btnBrowse.Enabled = enable;
		}

	}
}
