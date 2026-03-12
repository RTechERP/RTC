using System;
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
using BMS;
using BMS.Business;
using BMS.Model;
using ExcelDataReader;

namespace Forms.Employee
{
    public partial class frmInputByExcel : _Forms
    {
        DataSet ds;
        int temp;
        DateTime start;
        int quantity;
        public int ProjectID = 0;

        public frmInputByExcel()
        {
            InitializeComponent();
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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
                }) ;
         
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
            //if (temp == 0)
            //{
            //    grvData.Columns[0].Caption = "STT";
            //    grvData.Columns[1].Caption = "Tên vật tư";
            //    grvData.Columns[2].Caption = "Mã thiết bị";
            //    grvData.Columns[3].Caption = "Hãng sản xuất";
            //    grvData.Columns[4].Caption = "Thông số kỹ thuật";
            //    grvData.Columns[5].Caption = "Số lượng/ 1 máy";
            //    grvData.Columns[6].Caption = "Số lượng tổng";
            //    grvData.Columns[7].Caption = "Đơn vị";
            //    grvData.Columns[8].Caption = "Đơn Giá(VND)";
            //    grvData.Columns[9].Caption = "Thành tiền";
            //    grvData.Columns[10].Caption = "Tiến độ";
            //    grvData.Columns[11].Caption = "Nhà cung cấp";
            //    grvData.Columns[12].Caption = "Ngày yêu cầu đặt hàng";
            //    grvData.Columns[13].Caption = "Ngày về dự kiến";
            //    grvData.Columns[14].Caption = "Tình trạng";
            //    grvData.Columns[15].Caption = "Chất lượng";
            //    grvData.Columns[16].Caption = "Ghi chú";
            //}
            //else temp++;
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
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grdData.DataSource = null;
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();
                grdData.DataSource = ds;
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
                    grvData.Focus();
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
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
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            for (int i =0; i <rowCount; i++)
            {
                try
                {
                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    string produtCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();

                    if (stt <= 0 || string.IsNullOrEmpty(produtCode)) continue;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    //ProjectPartListModel s = new ProjectPartListModel();
                    ProjectPartListModel s = SQLHelper<ProjectPartListModel>.SqlToModel($"Select top 1 * FROM ProjectPartList WHERE ProductCode = '{produtCode}' AND ProjectID = {ProjectID}");

                    s.ProjectID = ProjectID;
                    //if (TextUtils.ToInt(grvData.GetRowCellValue(i, "F1")) <= 0) continue;

                    s.STT = stt;
                    s.GroupMaterial = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    s.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    s.ProductCode = produtCode;
                    //if (string.IsNullOrEmpty(s.GroupMaterial)) continue;
                    //if (string.IsNullOrEmpty(s.ProductCode)) continue;

                    s.Manufacturer = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                    s.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    s.QtyMin = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
                    s.QtyFull = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
                    s.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));
                    s.Amount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));
                    s.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
                    s.NCC = TextUtils.ToString(grvData.GetRowCellValue(i, "F12"));
                    s.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, "F13"));
                    s.ExpectedReturnDate = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F14"));
                    s.RequestDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F15"));
                    string status = TextUtils.ToString(grvData.GetRowCellValue(i, "F16"));
                    
                    if (status.Contains("Đã về"))
                    {
                        s.Status = 2;
                    }
                    else if (status.Contains("Đã đặt hàng"))
                    {
                        s.Status = 1;
                    }
                    else if(status.Contains("Không đặt hàng"))
                    {
                        s.Status = 0;
                    }
                    else
                    {
                        s.Status = -1;
                    }

                    s.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F17"));

                    int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID FROM ProjectPartList WHERE ProductCode = '{TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim()}'"));
                    if (s.ID > 0)
                    {
                        //s.ID = id;
                        ProjectPartListBO.Instance.Update(s);
                    }
                    else
                    {
                        ProjectPartListBO.Instance.Insert(s);
                    }
                        
                    quantity++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }
        
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (quantity != 0)
            //{
                
            //}

            MessageBox.Show($"Cập nhật thành công\n{start.ToString("dd/MM/yyyy HH:mm:ss")} - {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}", "Thông báo");
            this.DialogResult = DialogResult.OK;
            enableControl(true);
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            grvData.DeleteSelectedRows();

        }

        private void frmInputByExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("Mau.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("Mau.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist",TextUtils.Caption,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
        }

        private void frmInputByExcel_Load(object sender, EventArgs e)
        {

        }
    }
}
