using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
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
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmQuotation : _Forms
    {
        int _rownIndex = 0;
        public frmQuotation()
        {
            InitializeComponent();
        }

        private void frmQuotation_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            loadHang();
            loadCustomer();
            loadUser();
            loadData();
        }

        #region Methods
        void loadHang()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Manufacturer");
            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.ValueMember = "ID";
            repositoryItemSearchLookUpEdit2.DisplayMember = "ManufacturerCode";
        }
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cboCustomer.Properties.DisplayMember = "CustomerShortName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dt;
        }
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;

            repositoryItemSearchLookUpEdit3.DisplayMember = "FullName";
            repositoryItemSearchLookUpEdit3.ValueMember = "ID";
            repositoryItemSearchLookUpEdit3.DataSource = dt;
        }

        private void loadData()
        {
            try
            {
                txtPageNumber.Text = "1";
                txtTotalPage.Text = "1";
                //grvMaster.FocusedRowHandle = -1;
                DataSet oDataSet = loadDataSet();

                if (_rownIndex >= grvMaster.RowCount)
                    _rownIndex = 0;
                if (_rownIndex > 0)
                    grvMaster.FocusedRowHandle = _rownIndex;
                grvMaster.SelectRow(_rownIndex);

                txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
            }
            catch (Exception)
            {
            }
        }

        DataSet loadDataSet()
        {
            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetQuotationPaging"
                    , new string[] { "PageNumber", "PageSize", "CustomerID", "Status", "SaleID", "FilterText" }
                    , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt( txtPageSize.Value)
                        ,TextUtils.ToInt(cboCustomer.EditValue),cboStatus.SelectedIndex,TextUtils.ToInt(cboUser.EditValue), txtFilterText.Text.Trim() });

            grdMaster.DataSource = oDataSet.Tables[0];

            return oDataSet;
        }

        #endregion

        #region Buttons Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmQuotationDetail frm = new frmQuotationDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _rownIndex = 0;
                loadData();
            }
        }
        private void btnNewImport_Click(object sender, EventArgs e)
        {
            frmQuotationDetailNK frm = new frmQuotationDetailNK();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _rownIndex = 0;
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                if (id == 0) return;
                QuotationModel model = (QuotationModel)QuotationBO.Instance.FindByPK(id);
                _rownIndex = grvMaster.FocusedRowHandle;

                if (model.QuotationType == 0)
                {
                    frmQuotationDetail frm = new frmQuotationDetail();
                    frm.oQuotation = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        loadData();
                    }
                }
                else
                {
                    frmQuotationDetailNK frm = new frmQuotationDetailNK();
                    frm.oQuotation = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        loadData();
                    }
                }
                 
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                if (id == 0) return;
                _rownIndex = 0;
                int type = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colQuotationType));
                if (type == 0)
                {
                    QuotationModel model = (QuotationModel)QuotationBO.Instance.FindByPK(id);
                    frmQuotationDetail frm = new frmQuotationDetail();
                    frm.IsCopy = true;
                    frm.oQuotation = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        loadData();
                    }
                }

                if (type == 1)
                {
                    QuotationModel model = (QuotationModel)QuotationBO.Instance.FindByPK(id);
                    frmQuotationDetailNK frm = new frmQuotationDetailNK();
                    frm.IsCopy = true;
                    frm.oQuotation = model;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        loadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (strID == 0) return;

            string strName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("QuotationCode"));

            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved)
            {
                MessageBox.Show("Báo giá này đã được duyệt nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            //if (QuotationDetailBO.Instance.CheckExist("QuotationID", strID))
            //{
            //    MessageBox.Show("Báo giá này đã có danh sách sản phẩm nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    QuotationBO.Instance.Delete(strID);
                    QuotationDetailBO.Instance.DeleteByAttribute("QuotationID", strID);
                    grvMaster.DeleteSelectedRows();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        #endregion

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (grvMaster.RowCount > 0 && btnEdit.Enabled == true)
                btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadDataSet();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadDataSet();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadDataSet();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadDataSet();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetQuotationDetail_ByMasterID", "A"
                   , new string[] { "@QuotationID" }
                   , new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colID)) });
            grdData.DataSource = dt;
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        void exportExcel()
        {
            int quotationID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (quotationID == 0) return;

            int quotationType = 0;
            bool isExportSum = false;
            frmQuotationOptionExport frm = new frmQuotationOptionExport();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                quotationType = frm.ExportType;
                isExportSum = frm.IsExportSum;
            }
            else
            {
                return;
            }

            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            string fileSourceName = "";
            switch (quotationType)
            {
                case 0://RTC
                    fileSourceName = "QuotationRTC.xls";
                    break;
                case 1://HB
                    fileSourceName = "QuotationHB.xls";
                    break;
                case 2://MVITECH
                    fileSourceName = "QuotationMvi.xls";
                    break;
                default:
                    break;
            }
            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string quotationCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colQuotationCode));
            string currentPath = path + "\\" + quotationCode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xls";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo báo giá!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetQuotionExport", new string[] { "@QuotationID" }, new object[] { quotationID });
            DataTable dtMaster = dataSet.Tables[0];
            DataTable dtDetail = dataSet.Tables[1];

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo báo giá..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Excel.Application app = default(Excel.Application);
                Excel.Workbook workBoook = default(Excel.Workbook);
                Excel.Worksheet workSheet = default(Excel.Worksheet);
                try
                {
                    app = new Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Excel.Worksheet)workBoook.Worksheets[1];
                    switch (quotationType)
                    {
                        case 0://RTC
                            InsertCellsRTC(workSheet, dtMaster, dtDetail, isExportSum);
                            break;
                        case 1://HB
                            InsertCellsHB(workSheet, dtMaster, dtDetail, isExportSum);
                            break;
                        case 2://MVITECH
                            InsertCellsMvi(workSheet, dtMaster, dtDetail, isExportSum);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }

        void InsertCellsRTC(Excel.Worksheet workSheet, DataTable dtMaster, DataTable dtDetail, bool isSum)
        {
            workSheet.Cells[2, 6] = TextUtils.ToString(dtMaster.Rows[0]["QuotationCode"]);
            //workSheet.Cells[3, 6] = TextUtils.ToDate3(dtMaster.Rows[0]["QuotationDate"]).ToString("dd/MM/yyyy");
            workSheet.Cells[3, 6] = DateTime.Now.ToString("dd/MM/yyyy");

            workSheet.Cells[5, 3] = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
            workSheet.Cells[6, 3] = TextUtils.ToString(dtMaster.Rows[0]["Address"]);
            workSheet.Cells[7, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactName"]);
            workSheet.Cells[8, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactPhone"]);
            workSheet.Cells[9, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactEmail"]);

            workSheet.Cells[11, 7] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]);
            workSheet.Cells[12, 7] = TextUtils.ToString(dtMaster.Rows[0]["HandPhone"]);
            workSheet.Cells[13, 7] = TextUtils.ToString(dtMaster.Rows[0]["EmailCom"]);

            workSheet.Cells[29, 1] = TextUtils.ToString(dtMaster.Rows[0]["DeliveryFees"]);
            workSheet.Cells[33, 1] = TextUtils.ToString(dtMaster.Rows[0]["Payment"]);
            workSheet.Cells[34, 1] = TextUtils.ToString(dtMaster.Rows[0]["PlaceDelivery"]);

            workSheet.Cells[24, 6] = TextUtils.ToString(dtMaster.Rows[0]["VAT"]);

            decimal total = TextUtils.ToDecimal(dtMaster.Rows[0]["TotalPriceVAT"]);
            string totalText = TextUtils.NumericToString(TextUtils.ToInt64(total));
            workSheet.Cells[26, 3] = totalText.Length > 0 ?
                totalText[0].ToString().ToUpper() + totalText.Substring(1, totalText.Length - 1) : "";

            for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
            {
                workSheet.Cells[21, 2] = TextUtils.ToString(dtDetail.Rows[i]["PartNameRTC"]);
                ((Excel.Range)workSheet.Cells[21, 2]).Font.Italic = true;
                ((Excel.Range)workSheet.Cells[21, 2]).Font.Bold = false;
                ((Excel.Range)workSheet.Rows[21]).Insert();

                workSheet.Cells[21, 1] = i + 1;
                workSheet.Cells[21, 2] = TextUtils.ToString(dtDetail.Rows[i]["PartCodeRTC"]);
                workSheet.Cells[21, 4] = TextUtils.ToDecimal(dtDetail.Rows[i]["Qty"]).ToString("n0");
                workSheet.Cells[21, 5] = TextUtils.ToString(dtDetail.Rows[i]["Unit"]);
                if (!isSum)
                {
                    workSheet.Cells[21, 6] = TextUtils.ToDecimal(dtDetail.Rows[i]["Price"]).ToString("n0");
                    workSheet.Cells[21, 7] = TextUtils.ToDecimal(dtDetail.Rows[i]["TotalPrice"]).ToString("n0");
                }

                ((Excel.Range)workSheet.Cells[21, 2]).Font.Bold = true;
                ((Excel.Range)workSheet.Rows[21]).Insert();
            }

            if (isSum)
            {
                string totalName = TextUtils.ToString(dtMaster.Rows[0]["TotalName"]);
                workSheet.Cells[19, 2] = totalName + " bao gồm:";
                ((Excel.Range)workSheet.Cells[20, 2]).Font.Italic = true;
                ((Excel.Range)workSheet.Rows[20]).Font.Bold = true;
                workSheet.Cells[19, 4] = TextUtils.ToDecimal(dtMaster.Rows[0]["QtySet"]).ToString("n0");
                workSheet.Cells[19, 5] = "set/bộ";
                workSheet.Cells[19, 6] = TextUtils.ToDecimal(dtMaster.Rows[0]["PricePS"]).ToString("n0");
                workSheet.Cells[19, 7] = TextUtils.ToDecimal(dtMaster.Rows[0]["TotalPrice"]).ToString("n0");
                ((Excel.Range)workSheet.Rows[20]).Delete();
                ((Excel.Range)workSheet.Rows[20]).Delete();
            }
            else
            {
                ((Excel.Range)workSheet.Rows[19]).Delete();
                ((Excel.Range)workSheet.Rows[19]).Delete();
                ((Excel.Range)workSheet.Rows[19]).Delete();
            }

            workSheet.Cells[19, 8] = TextUtils.ToString(dtMaster.Rows[0]["DeliveryPeriod"]);
        }

        void InsertCellsHB(Excel.Worksheet workSheet, DataTable dtMaster, DataTable dtDetail, bool isSum)
        {
            workSheet.Cells[2, 9] = TextUtils.ToString(dtMaster.Rows[0]["QuotationCode"]);
            //workSheet.Cells[3, 9] = TextUtils.ToDate3(dtMaster.Rows[0]["QuotationDate"]).ToString("dd/MM/yyyy");
            workSheet.Cells[3, 9] = DateTime.Now.ToString("dd/MM/yyyy");

            workSheet.Cells[5, 3] = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
            workSheet.Cells[6, 3] = TextUtils.ToString(dtMaster.Rows[0]["Address"]);
            workSheet.Cells[7, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactName"]);
            workSheet.Cells[8, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactPhone"]);
            workSheet.Cells[9, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactEmail"]);

            workSheet.Cells[10, 10] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]);
            workSheet.Cells[11, 10] = TextUtils.ToString(dtMaster.Rows[0]["HandPhone"]);
            workSheet.Cells[12, 10] = TextUtils.ToString(dtMaster.Rows[0]["EmailCom"]);

            workSheet.Cells[27, 1] = TextUtils.ToString(dtMaster.Rows[0]["DeliveryFees"]);
            workSheet.Cells[30, 1] = TextUtils.ToString(dtMaster.Rows[0]["Payment"]);
            workSheet.Cells[31, 1] = TextUtils.ToString(dtMaster.Rows[0]["PlaceDelivery"]);

            workSheet.Cells[23, 9] = TextUtils.ToString(dtMaster.Rows[0]["VAT"]);

            decimal total = TextUtils.ToDecimal(dtMaster.Rows[0]["TotalPriceVAT"]);
            string totalText = TextUtils.NumericToString(TextUtils.ToInt64(total));
            workSheet.Cells[25, 3] = totalText.Length > 0 ?
                totalText[0].ToString().ToUpper() + totalText.Substring(1, totalText.Length - 1) : "";

            for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
            {
                workSheet.Cells[20, 2] = TextUtils.ToString(dtDetail.Rows[i]["PartNameRTC"]);
                ((Excel.Range)workSheet.Cells[20, 2]).Font.Italic = true;
                ((Excel.Range)workSheet.Cells[20, 2]).Font.Bold = false;
                ((Excel.Range)workSheet.Rows[20]).Insert();

                workSheet.Cells[20, 1] = i + 1;
                workSheet.Cells[20, 2] = TextUtils.ToString(dtDetail.Rows[i]["PartCodeRTC"]);
                workSheet.Cells[20, 7] = TextUtils.ToDecimal(dtDetail.Rows[i]["Qty"]).ToString("n0");
                workSheet.Cells[20, 8] = TextUtils.ToString(dtDetail.Rows[i]["Unit"]);

                if (!isSum)
                {
                    workSheet.Cells[20, 9] = TextUtils.ToDecimal(dtDetail.Rows[i]["Price"]).ToString("n0");
                    workSheet.Cells[20, 10] = TextUtils.ToDecimal(dtDetail.Rows[i]["TotalPrice"]).ToString("n0");
                }
                ((Excel.Range)workSheet.Cells[20, 2]).Font.Bold = true;
                ((Excel.Range)workSheet.Rows[20]).Insert();
            }

            if (isSum)
            {
                string totalName = TextUtils.ToString(dtMaster.Rows[0]["TotalName"]);
                workSheet.Cells[18, 2] = totalName + " bao gồm:";
                workSheet.Cells[18, 7] = TextUtils.ToDecimal(dtMaster.Rows[0]["QtySet"]).ToString("n0");
                workSheet.Cells[18, 9] = TextUtils.ToDecimal(dtMaster.Rows[0]["PricePS"]).ToString("n0");
                workSheet.Cells[18, 10] = TextUtils.ToDecimal(dtMaster.Rows[0]["TotalPrice"]).ToString("n0");

                ((Excel.Range)workSheet.Rows[19]).Delete();
                ((Excel.Range)workSheet.Rows[19]).Delete();
            }
            else
            {
                ((Excel.Range)workSheet.Rows[18]).Delete();
                ((Excel.Range)workSheet.Rows[18]).Delete();
                ((Excel.Range)workSheet.Rows[18]).Delete();
            }

            workSheet.Cells[18, 11] = TextUtils.ToString(dtMaster.Rows[0]["DeliveryPeriod"]);
        }

        void InsertCellsMvi(Excel.Worksheet workSheet, DataTable dtMaster, DataTable dtDetail, bool isSum)
        {
            workSheet.Cells[1, 6] = TextUtils.ToString(dtMaster.Rows[0]["QuotationCode"]);
            //workSheet.Cells[2, 6] = TextUtils.ToDate3(dtMaster.Rows[0]["QuotationDate"]).ToString("dd/MM/yyyy");
            workSheet.Cells[2, 6] = DateTime.Now.ToString("dd/MM/yyyy");

            workSheet.Cells[7, 3] = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
            workSheet.Cells[8, 3] = TextUtils.ToString(dtMaster.Rows[0]["Address"]);
            workSheet.Cells[9, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactName"]);
            workSheet.Cells[10, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactPhone"]);
            workSheet.Cells[11, 3] = TextUtils.ToString(dtMaster.Rows[0]["ContactEmail"]);

            workSheet.Cells[8, 7] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]);
            workSheet.Cells[9, 7] = TextUtils.ToString(dtMaster.Rows[0]["HandPhone"]);
            workSheet.Cells[10, 7] = TextUtils.ToString(dtMaster.Rows[0]["EmailCom"]);

            workSheet.Cells[20, 1] = TextUtils.ToString(dtMaster.Rows[0]["DeliveryFees"]);
            workSheet.Cells[24, 1] = TextUtils.ToString(dtMaster.Rows[0]["Payment"]);
            workSheet.Cells[25, 1] = TextUtils.ToString(dtMaster.Rows[0]["PlaceDelivery"]);

            //decimal total = TextUtils.ToDecimal(dtMaster.Rows[0]["TotalPriceVAT"]);
            //string totalText = TextUtils.NumericToString(TextUtils.ToInt64(total));
            //workSheet.Cells[26, 3] = totalText.Length > 0 ?
            //    totalText[0].ToString().ToUpper() + totalText.Substring(1, totalText.Length - 1) : "";

            for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
            {
                workSheet.Cells[16, 2] = TextUtils.ToString(dtDetail.Rows[i]["PartNameRTC"]);
                ((Excel.Range)workSheet.Cells[16, 2]).Font.Italic = true;
                ((Excel.Range)workSheet.Cells[16, 2]).Font.Bold = false;
                ((Excel.Range)workSheet.Rows[16]).Insert();

                workSheet.Cells[16, 1] = i + 1;
                workSheet.Cells[16, 2] = TextUtils.ToString(dtDetail.Rows[i]["PartCodeRTC"]);
                workSheet.Cells[16, 4] = TextUtils.ToDecimal(dtDetail.Rows[i]["Qty"]).ToString("n0");
                workSheet.Cells[16, 5] = TextUtils.ToString(dtDetail.Rows[i]["Unit"]);
                if (!isSum)
                {
                    workSheet.Cells[16, 6] = TextUtils.ToDecimal(dtDetail.Rows[i]["Price"]).ToString("n0");
                    workSheet.Cells[16, 7] = TextUtils.ToDecimal(dtDetail.Rows[i]["TotalPrice"]).ToString("n0");
                }
                //total += TextUtils.ToDecimal(dtDetail.Rows[i]["TotalPrice"]);

                ((Excel.Range)workSheet.Cells[16, 2]).Font.Bold = true;
                ((Excel.Range)workSheet.Rows[16]).Insert();
            }

            if (isSum)
            {
                string totalName = TextUtils.ToString(dtMaster.Rows[0]["TotalName"]);
                workSheet.Cells[14, 2] = totalName + " bao gồm:";
                workSheet.Cells[14, 4] = TextUtils.ToDecimal(dtMaster.Rows[0]["QtySet"]).ToString("n0");
                workSheet.Cells[14, 6] = TextUtils.ToDecimal(dtMaster.Rows[0]["PricePS"]).ToString("n0");
                workSheet.Cells[14, 7] = TextUtils.ToDecimal(dtMaster.Rows[0]["TotalPrice"]).ToString("n0");

                ((Excel.Range)workSheet.Rows[14]).Font.Bold = true;
                ((Excel.Range)workSheet.Rows[15]).Delete();
                ((Excel.Range)workSheet.Rows[15]).Delete();
            }
            else
            {
                ((Excel.Range)workSheet.Rows[14]).Delete();
                ((Excel.Range)workSheet.Rows[14]).Delete();
                ((Excel.Range)workSheet.Rows[14]).Delete();
            }

            workSheet.Cells[14, 8] = TextUtils.ToString(dtMaster.Rows[0]["DeliveryPeriod"]);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            exportExcel();
        }

        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn{0} duyệt báo giá này?", isApproved ? "" : " bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return;

            int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            _rownIndex = grvMaster.FocusedRowHandle;

            string sql = string.Format(@"UPDATE dbo.Quotation SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, iD);

            TextUtils.ExcuteSQL(sql);
            loadData();
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            approved(true);
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            approved(false);
        }
    }
}
