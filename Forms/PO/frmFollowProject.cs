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
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraEditors.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraTreeList;

namespace BMS
{
    public partial class frmFollowProject : _Forms
    {
        public delegate void SendData(string load);
        public frmFollowProject()
        {
            InitializeComponent();
        }

        private void frmListTool_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            loadData();

        }

        void loadData()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetFollowProject", "A"
                 , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText" }
                 , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE,cbStatus.SelectedIndex, txtFilterText.Text });
            grdMaster.DataSource = dt;
            if (dt.Rows.Count > 0)
                txtTotalPage.Text = TextUtils.ToString(dt.Rows[0][0]);
            else txtTotalPage.Text = "1";

        }
        void loadGrdData()
        {
            int GroupId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spLoadFollowProject", "A", new string[] { "@ID" }, new object[] { GroupId });
            grdData.DataSource = dt;
            grvData.ExpandAllGroups();
        }

        private void btnNewGroup_Click_1(object sender, EventArgs e)
        {
            frmFollowProjectDetail frm = new frmFollowProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                loadGrdData();
            }
        }
        private void sendData(string load)
        {
            loadData();
            loadGrdData();
        }
        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            int GroupId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            FollowProjectModel model = (FollowProjectModel)FollowProjectBO.Instance.FindByPK(GroupId);
            frmFollowProjectDetail frm = new frmFollowProjectDetail();
            frm._bill = model;
            frm.Id = GroupId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (strID == 0) return;

            string strName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProjectTen));

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FollowProjectBO.Instance.Delete(strID);
                    FollowProjectDetailBO.Instance.DeleteByAttribute("FollowProjectID", strID);
                    grvMaster.DeleteSelectedRows();
                    loadData();
                    loadGrdData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }


        private void btnConfig_Click(object sender, EventArgs e)
        {
            //frmConfig frm = new frmConfig();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadData();
            //    loadGrdData();
            //}
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadGrdData();
        }




        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvMaster.FocusedRowHandle;
            grvMaster.FocusedRowHandle = focusedRowHandle - 1;
            loadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;

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
            string fileSourceName = "PONCC.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCustomer));
            string currentPath = path + "\\" + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DataSet dts = TextUtils.LoadDataSetFromSP("spGetExportPONCC", new string[] { "@IDmaster" }, new object[] { IDMaster });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
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


                    for (int i = dts.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        //xuất row maker ra exel
                        workSheet.Cells[57, 5] = TextUtils.ToString(dts.Tables[0].Rows[i]["MakerText"]);
                        workSheet.Cells[57, 1] = "";
                        workSheet.Cells[57, 16] = "";
                        workSheet.Cells[57, 19] = "";
                        workSheet.Cells[57, 24] = "";
                        workSheet.Cells[57, 25] = "";
                        workSheet.Cells[57, 29] = "";

                        ((Excel.Range)workSheet.Rows[57]).Insert(((Excel.Range)workSheet.Rows[57]).Copy());

                        //Xuất thông tin sản phẩm
                        workSheet.Cells[57, 1] = i + 1;
                        workSheet.Cells[57, 5] = TextUtils.ToString(dts.Tables[0].Rows[i]["StandardModel"]);
                        workSheet.Cells[57, 16] = TextUtils.ToString(dts.Tables[0].Rows[i]["ProjectModel"]);
                        workSheet.Cells[57, 19] = TextUtils.ToString(dts.Tables[0].Rows[i]["Qty"]);
                        workSheet.Cells[57, 24] = TextUtils.ToString(dts.Tables[0].Rows[i]["Unit"]);
                        workSheet.Cells[57, 25] = TextUtils.ToString(dts.Tables[0].Rows[i]["UnitPriceUSD"]);
                        workSheet.Cells[57, 29] = TextUtils.ToString(dts.Tables[0].Rows[i]["TotalPriceUSD"]);
                        ((Excel.Range)workSheet.Rows[57]).Insert(((Excel.Range)workSheet.Rows[57]).Copy());

                    }
                      ((Excel.Range)workSheet.Rows[57]).Delete();
                    workSheet.Cells[dts.Tables[0].Rows.Count * 2 + 57, 29] = TextUtils.ToString(dts.Tables[1].Rows[0]["Total"]);


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

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colStatus")
            {
                string value = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colStatus));
                if (value == "Finish")
                    e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                if (value == "Chưa giao đủ")
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
            }
            if (e.Column.Name == "colCustomerQuotationDetail" || e.Column.Name == "colTotalCustomerQuotationDetail")
            {

                e.Appearance.BackColor = Color.Yellow;

            }
            if (e.Column == colDeliveryRequestedDate)
            {
                int c = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, ColorTest));
                switch (c)
                {
                    case 0:
                        e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                        break;
                    case 1:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    case 2:
                        e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                        break;
                    default:
                        e.Appearance.BackColor = Color.FromArgb(0, 255, 0);
                        break;
                }

            }
        }
        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string sql = string.Format("UPDATE dbo.FollowProject SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
            }
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApprove)
            {
                MessageBox.Show("Phiếu đã được duyệt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
            approved(true);
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            bool isApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (!isApprove)
            {
                return;
            }
            approved(false);
        }
    }

}


