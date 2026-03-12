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
    public partial class frmProductExcel : _Forms
    {
        public frmProductExcel()
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
                    string pGroupName = string.Empty;
                    string cellAddressBox = grvData.GetRowCellValue(i, "F7").ToString();
                    if (cellAddressBox.IndexOf(' ') == -1)
                    {
                            pGroupName = "Khác";
                    }
                    else 
                    {
                        string regex = "^[a-z A-Z]+[0-9]+";
                        if (!Regex.IsMatch(cellAddressBox, regex))
                            pGroupName = cellAddressBox;
                        else
                            pGroupName = cellAddressBox.Substring(cellAddressBox.IndexOf(' ') + 1);
                    }
                    ArrayList arrGrModel = ProductGroupRTCBO.Instance.FindByAttribute("ProductGroupName", pGroupName);
                    int pGroupID;
                    if (arrGrModel == null || arrGrModel.Count == 0)
                    {
                        ProductGroupRTCModel grModel = new ProductGroupRTCModel();
                        string sql = "Select max(ProductGroupNo) from ProductGroupRTC";
                        decimal newGroupNo = TextUtils.ToDecimal(Lib.ExcuteScalar(sql)) + 1;
                        grModel.ProductGroupNo = newGroupNo.ToString();
                        grModel.ProductGroupName = pGroupName;
                        ProductGroupRTCBO.Instance.Insert(grModel);
                        string sql_GetNewID = "Select max(ID) from ProductGroupRTC";
                        pGroupID = TextUtils.ToInt(Lib.ExcuteScalar(sql_GetNewID));
                    }
                    else
                    {
                        ProductGroupRTCModel grModel = arrGrModel[0] as ProductGroupRTCModel;
                        pGroupID = grModel.ID;
                    }
                    ProductRTCModel model = new ProductRTCModel();
                    model.ProductCode = grvData.GetRowCellValue(i, "F2").ToString();// mã sản phẩm
                    model.ProductName = grvData.GetRowCellValue(i, "F3").ToString();// tên
                    model.Serial = grvData.GetRowCellValue(i, "F4").ToString();//Code
                    model.Maker = grvData.GetRowCellValue(i, "F5").ToString();//Hãng
                    model.Number = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F6").ToString());//Tổng số lượng
                    model.AddressBox = grvData.GetRowCellValue(i, "F8").ToString();//Vị trí (Hộp)
                    model.Note = grvData.GetRowCellValue(i, "F9").ToString();//Ghi chú
                    model.StatusProduct = false;
                    model.ProductGroupRTCID = pGroupID;
                    model.CreateDate = DateTime.Now;// ngày nhập
                    model.NumberInStore = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7").ToString());// số lượng tồn kho
                    ProductRTCBO.Instance.Insert(model);
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
