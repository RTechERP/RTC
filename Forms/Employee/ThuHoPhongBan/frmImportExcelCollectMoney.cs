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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmImportExcelEmployeeCollectMoney : _Forms
    {
		DateTime start;
		DataSet ds;
		public frmImportExcelEmployeeCollectMoney()
        {
            InitializeComponent();
        }

        private void frmImportExcelEmployeeCollectMoney_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
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
				if (grvData.RowCount == 0)
				{
					MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				else
				{
					progressBar1.Maximum = grvData.RowCount - 1;
					txtRate.Text = "";
					start = DateTime.Now;
					enableControl(false);
					backgroundWorker1.RunWorkerAsync();
				}
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
			int rowCount = grvData.RowCount;
			for (int i = 2; i < rowCount; i++)
			{
				try
				{
					progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
					txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
					EmployeeCollectMoneyModel model = new EmployeeCollectMoneyModel();
					ArrayList array = EmployeeCollectMoneyBO.Instance.FindByAttribute("STT", TextUtils.ToString(grvData.GetRowCellValue(i, "F1")));
					if (array.Count > 0)
					{
						model = (EmployeeCollectMoneyModel)array[0];
					}

					model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
					if (model.STT <= 0) continue;
					string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());
					int employeeID = TextUtils.ToInt(TextUtils.ExcuteScalar($"select TOP 1 ID from dbo.Employee where Code = N'{employeeCode}'"));
					model.EmployeeID = employeeID;
					if (employeeID == 0) continue;

					model.CollectDay = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F5"));
					model.Fund = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F6"));
					model.Error = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
					model.Party = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
					model.TotalMoney = model.Fund + model.Error + model.Party;
					model.Notes = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));

					if (model.ID > 0)
					{
						EmployeeCollectMoneyBO.Instance.Update(model);
					}
					else
					{
						EmployeeCollectMoneyBO.Instance.Insert(model);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
				}

			}
		}
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
			//grvData.Columns.Clear();
			//try
			//{
			//	MyLib.ShowWaitForm("Loading data ...");
			//	DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

			//	grdData.DataSource = dt;
			//	grvData.PopulateColumns();
			//	grvData.BestFitColumns();
			//	grdData.Focus();
			//}
			//catch (Exception ex)
			//{
			//	TextUtils.ShowError(ex);
			//	grdData.DataSource = null;
			//}
			//finally
			//{
			//	MyLib.CloseWaitForm();
			//}

            grvData.Columns.Clear();
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
            if (grdData.DataSource == null)
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
			MessageBox.Show("Cập nhật thành công !","Thông báo");
			enableControl(true);
			this.Close();
		}

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
			//grvData.Columns.Clear();
			//OpenFileDialog ofd = new OpenFileDialog();
			//if (ofd.ShowDialog() == DialogResult.OK)
			//{
			//	btnBrowse.Text = ofd.FileName;
			//	cboSheet.DataSource = null;
			//	cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
			//	cboSheet_SelectionChangeCommitted(null, null);
			//	btnSave.Enabled = true;
			//}

            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
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
					cboSheet_SelectionChangeCommitted(null, null);
				}
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

                    cboSheet_SelectionChangeCommitted(null, null);
                }
            }
        }
		private void txtMau_Click(object sender, EventArgs e)
		{
			try
			{
				string path = Path.Combine(Application.StartupPath, "Mau_Khau_Tru_Luong.xlsx");
                if (!File.Exists(path))
                {
					MessageBox.Show("File không tồn tại!");
                }
				Process.Start(path);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}
		private void frmImportExcelEmployeeCollectMoney_FormClosed(object sender, FormClosedEventArgs e)
        {
			this.DialogResult = DialogResult.OK;
		}

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
