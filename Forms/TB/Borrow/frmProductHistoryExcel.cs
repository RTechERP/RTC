using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProductHistoryExcel : _Forms
    {
        public int Status { get; set; }
        public frmProductHistoryExcel()
        {
            InitializeComponent();
        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
                cboSheet.DataSource = null;
                cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
            }
        }

        private void frmImportCheckForceExcel_Load(object sender, EventArgs e)
        {
            //ArrayList
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                grdData.DataSource = dt;
                grvData.PopulateColumns();
                grvData.BestFitColumns();
                grdData.Focus();
                grvData.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvData.Columns[6].DisplayFormat.FormatString = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        DateTime start;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount - 1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            
            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    
                    string sqlGetProductID = "Select ID From ProductRTC Where ProductCode = '" + grvData.GetRowCellValue(i, "F2") + "'";
                    int ProductRTCID = TextUtils.ToInt(TextUtils.ExcuteScalar(sqlGetProductID));
                    string sqlGetPeopleID = "Select ID From Users Where FullName = N'" + grvData.GetRowCellValue(i, "F8") + "'";
                    int PeopleID = TextUtils.ToInt(TextUtils.ExcuteScalar(sqlGetPeopleID));

                    HistoryProductRTCModel model = new HistoryProductRTCModel();
                    model.ProductRTCID = ProductRTCID;
                    model.DateBorrow = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F7"));
                    model.DateReturnExpected = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F9"));
                    model.PeopleID = PeopleID;
                    model.Project = TextUtils.ToString(grvData.GetRowCellValue(i, "F11"));
                    model.DateReturn = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F10"));
                    model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F12"));
                    if (Status != -1)
                        model.Status = Status;
                    model.NumberBorrow = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F4"));
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    HistoryProductRTCBO.Instance.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
            enableControl(true);
        }
    }
}
