using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
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

namespace BMS
{
    public partial class frmFingerprint : _Forms
    {
        EmployeeFingerprintMasterModel finmt = new EmployeeFingerprintMasterModel();
        EmployeeFingerprintModel model = new EmployeeFingerprintModel();
        public frmFingerprint()
        {
            InitializeComponent();
        }

        private void frmFingerprint_Load(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            nbrYear.Value = DateTime.Now.Year;
            LoadFingerprint();
            LoadFingerprintMaster();
        }
        void LoadFingerprint()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spLoadPageEmployeeFingerprint", "A",
                new string[] { "@FilterText", "@PageNumber", "@PageSize", "@ID" },
                new object[] { TextUtils.ToString(txtKeyword.Text.Trim()), TextUtils.ToInt(txtPageNumber.Text.Trim()), TextUtils.ToInt(txtPageSize.Text.Trim()), id });
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }
        void LoadFingerprintMaster()
        {
            int year = TextUtils.ToInt(nbrYear.Value);
            DataTable dtt = TextUtils.LoadDataFromSP("spGetEmployeeFingerprintMasterByYear", "A",new string[] { "@Year" },new object[] { year });
            grdMaster.DataSource = dtt;
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmFingerprintExcel frmExcel = new frmFingerprintExcel();
            if (frmExcel.ShowDialog() == DialogResult.OK)
            {
                LoadFingerprint();
                LoadFingerprintMaster();
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            if (isApproved == true)
            {
                string month = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colMonth));
                MessageBox.Show(String.Format("Tháng {0} đã được duyệt. Xin vui lòng kiểm tra lại !", month), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return;
            }
            approved(true);
        }
        void approved(bool isApproved)
        {
            string month = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colMonth));
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} bảng vân tay tháng {1} không ?",approved, month), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                string sql = string.Format("UPDATE dbo.EmployeeFingerprintMaster SET IsBrowser = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvMaster.SetFocusedRowCellValue(colIsBrowser, 1);
                else
                    grvMaster.SetFocusedRowCellValue(colIsBrowser, 0);
            }
        }
        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            if (isApproved == false)
            {
                string month = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colMonth));
                MessageBox.Show(String.Format("Tháng {0} này chưa được duyệt.", month), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }
            approved(false);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadFingerprint();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadFingerprint();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadFingerprint();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadFingerprint();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadFingerprint();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadFingerprint();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            for (int i = 0; i <= id; i++)
            {
                if (id > 0)
                {
                    LoadFingerprint();
                }
            }
        }

        private void btnOutputExcel_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            if (isApproved == false)
            {
                string month = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colMonth));
                MessageBox.Show(String.Format("Không thể xuất khẩu tháng {0} khi chưa được duyệt. Vui lòng kiểm tra lại!", month), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }    
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
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
            string fileSourceName = "Fingerprint.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string organization = TextUtils.ToString(grvData.GetFocusedRowCellValue(colOrganization));
            string currentPath = path + "\\" + organization + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xls";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //DataTable dt = TextUtils.LoadDataFromSP("spGetLoadEmployeeFingerprint", "A", new string[] { "@ID" }, new object[] { id });


            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

                try
                {
                   
                    DateTime dtime = TextUtils.ToDate3(grvData.GetRowCellValue(0,colDay));
                    string date = $"Ngày {dtime.Day} Tháng {dtime.Month} Năm {dtime.Year}";
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                    string ghichu = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colghichu));
                    workSheet.Cells[2, 3] = ghichu;
                    workSheet.Cells[14, 4] = date;
                    for (int i = grvData.RowCount-1; i>=0 ; i--)
                    {
                        workSheet.Cells[7, 1] = i+1;
                        workSheet.Cells[7, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colIDChamCong));
                        workSheet.Cells[7, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colFullName));
                        workSheet.Cells[7, 4] = TextUtils.ToString(grvData.GetRowCellValue(i, colOrganization));
                        workSheet.Cells[7, 5] = TextUtils.ToDate2(grvData.GetRowCellValue(i, colDay));
                        workSheet.Cells[7, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colDayOfWeek));
                        workSheet.Cells[7, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colPeriod));
                        workSheet.Cells[7, 8] = TextUtils.ToDate2(grvData.GetRowCellValue(i, colCheckIn));
                        workSheet.Cells[7, 9] = TextUtils.ToDate2(grvData.GetRowCellValue(i, colCheckOut));
                        workSheet.Cells[7, 10] = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                            
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[7]).Insert();

                    }
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[6]).Delete();
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[6]).Delete();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            string month = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colMonth));
            if (isApproved == true)
            {
                MessageBox.Show(string.Format("Bạn không thể xóa tháng {0} khi đã duyệt ! Xin hãy hủy duyệt nếu muốn xóa.", month), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (MessageBox.Show(string.Format("Bạn có muốn xóa tháng {0} hay không ?", month), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeFingerprintMasterBO.Instance.Delete(strID);
                EmployeeFingerprintBO.Instance.DeleteByAttribute("IDFingerprintMaster", strID);
                grvMaster.DeleteSelectedRows();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null,null);
                LoadFingerprint();
            }
        }

        private void nbrYear_ValueChanged(object sender, EventArgs e)
        {
            LoadFingerprintMaster();
            LoadFingerprint();
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colSumLate)
            //{
            //    if (TextUtils.ToDecimal(e.Value) > 60)
            //    {
            //        e.DisplayText = Decimal.Round((TextUtils.ToDecimal(e.Value) / 60), 2).ToString() + " (h)";
            //    }
            //    else
            //    {
            //        if (TextUtils.ToDecimal(e.Value) < 60 && TextUtils.ToDecimal(e.Value) > 0)
            //        {
            //            e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value),0).ToString() + " (p)";
            //        }
            //        else
            //            e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
            //    }
            //}
            //if (e.Column == colSumEarly)
            //{
            //    if (TextUtils.ToDecimal(e.Value) > 60)
            //    {
            //        e.DisplayText = Decimal.Round((TextUtils.ToDecimal(e.Value) / 60),2).ToString() + " (h)";
            //    }
            //    else
            //    {
            //        if (TextUtils.ToDecimal(e.Value) < 60 && TextUtils.ToDecimal(e.Value) > 0)
            //        {
            //            e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString() + " (p)";
            //        }
            //        else
            //            e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
            //    }    
            //}
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //e.Appearance.BackColor = Color.White;
            //e.Appearance.ForeColor = Color.Black;
            //if (e.Column == colSumLate)
            //{
            //    if (TextUtils.ToDecimal(e.CellValue) > 0)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.ForeColor = Color.White;
            //    }
                    
            //}
            //if (e.Column == colSumEarly)
            //{
                
            //    if (TextUtils.ToDecimal(e.CellValue) > 0)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.ForeColor = Color.White;
            //    }
            //}
        }

        private void btnQuyenChamVanTay_Click(object sender, EventArgs e)
        {
            //frmNoFingerprint frm = new frmNoFingerprint();
            //frm.ShowDialog();
            TextUtils.OpenForm(new frmNoFingerprint());

        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
         
            frmEmployeeChamCongMaster frm = new frmEmployeeChamCongMaster();
            frm.ShowDialog();
        }
    }

}
