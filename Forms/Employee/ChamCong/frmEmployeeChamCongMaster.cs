
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeChamCongMaster : _Forms
    {
        public frmEmployeeChamCongMaster()
        {
            InitializeComponent();
        }

        private void frmEmployeeChamCongMaster_Load(object sender, EventArgs e)
        {
            nbrYear.Value = DateTime.Now.Year;
            loadDataMaster();
            //loadDetail();
        }
        void loadDataMaster()
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeChamCongMaster", "A",
                        new string[] { "@Year", "@Keyword" },
                        new object[] { TextUtils.ToInt(nbrYear.Value), txtFilterText.Text});

            grdMaster.DataSource = dt;
        }

        void loadDetail()
        {
            int month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetChamCongNew"
                , new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID", "@KeyWord" }
                , new object[] { month, year, 0, 0, ""});


            grdData.DataSource = ds.Tables[0];

            bandTitle.Caption = $"BẢNG CHẤM CÔNG THÁNG {month}";
            bandTotalWorkDay.Caption = $"Công tiêu chuẩn = {TextUtils.ToInt(ds.Tables[2].Rows[0]["TotalWorkDay"])}";

            GridBandCollection bandChild = bandTotalWorkDay.Children;
            DataTable dtAllDate = ds.Tables[1];
            if (dtAllDate.Rows.Count <= 0) return;
            
            foreach (GridBand item in bandChild)
            {
                GridBandColumnCollection columns = item.Columns;
                if (columns.Count <= 0)
                {
                    continue;
                }

                string fieldName = columns[0].FieldName;
                string caption = TextUtils.ToString(dtAllDate.Rows[0][fieldName]);

                item.Caption = caption.Substring(0, caption.IndexOf(";"));

                int status = TextUtils.ToInt(caption.Substring(caption.IndexOf(";") + 1));
                if (status == 0)
                {
                    item.AppearanceHeader.BackColor = Color.Orange;
                    item.AppearanceHeader.ForeColor = Color.White;

                    columns[0].AppearanceHeader.BackColor = Color.Orange;
                    columns[0].AppearanceHeader.ForeColor = Color.White;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmployeeChamCongDetail frm = new frmEmployeeChamCongDetail();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataMaster();
            }
        }



        private void grvMaster_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //int Month =TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            //int Year =TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            //int MasterID= TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //if (Month == null || Year == null) return;
            //loadDetail(Month, Year, MasterID);
            //label2.Text = $"BẢNG CHẤM CÔNG THÁNG {Month} NĂM {Year}" ;
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            if (isApproved)
            {
                MessageBox.Show($"Bảng chấm công  tháng {Month} năm {Year} đã được duyệt !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show($"Bạn có chắc muốn duyệt bảng chấm công này không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    EmployeeChamCongMasterModel model = (EmployeeChamCongMasterModel)EmployeeChamCongMasterBO.Instance.FindByPK(id);
                    model.IsApproved = true;
                    EmployeeChamCongMasterBO.Instance.Update(model);
                    grvMaster.SetFocusedRowCellValue(colIsBrowser, true);
                    grvMaster.FocusedRowHandle = -1;
                }

            }
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {

            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            if (!isApproved)
            {
                MessageBox.Show($"Bảng chấm công  tháng {Month} năm {Year} chưa được duyệt !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show($"Bạn có chắc muốn duyệt bảng chấm công này không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    EmployeeChamCongMasterModel model = (EmployeeChamCongMasterModel)EmployeeChamCongMasterBO.Instance.FindByPK(id);
                    model.IsApproved = false;
                    EmployeeChamCongMasterBO.Instance.Update(model);
                    grvMaster.SetFocusedRowCellValue(colIsBrowser, false);
                    grvMaster.FocusedRowHandle = -1;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            if (id <= 0) return;
            if (isApproved)
            {
                MessageBox.Show($"Bảng chấm công  tháng {Month} năm {Year} đã được duyệt không thể xoá !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show($"Bạn có chắc muốn xoá bảng chấm công tháng {Month} năm {Year} hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    EmployeeChamCongMasterBO.Instance.Delete(id);
                    EmployeeChamCongDetailBO.Instance.DeleteByAttribute("MasterID", id);
                    loadDataMaster();
                }
            }


        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadDataMaster();
            //loadDetail();
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //int MasterID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //if (MasterID <= 0)
            //{
            //    return;
            //}

            //loadDetail();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

            int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            int MasterID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (MasterID == 0)
            {
                return;
            }
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xls";
            f.FileName = $"DanhSachChamCongT{Month}_{Year}.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();

                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}/DanhSachChamCongT{Month}_{Year}.xls";
                    string filepath = f.FileName;
                    grvData.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                }
                grvData.ClearSelection();
            }
        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {

            //int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
            //int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
            //int MasterID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            //bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsBrowser));
            //if (MasterID == 0) return;
            //if (isApproved)
            //{
            //    MessageBox.Show($"Bảng chấm công  tháng {Month} năm {Year} đã được duyệt không thể Update !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang Update..."))
            //    {
            //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                    
            //        EmployeeChamCongDetailBO.Instance.DeleteByAttribute("MasterID", MasterID);
            //        TextUtils.ExcuteSQL($"Exec spInsertIntoEmployeeChamCongDetail {Month},{Year},{MasterID}");

            //        loadDetail();
            //    }
            //}

        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    object val1 = view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentID");
                    object val2 = view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentID");
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));

            if (id <= 0)
            {
                return;
            }

            EmployeeChamCongMasterModel employeeChamCong = (EmployeeChamCongMasterModel)EmployeeChamCongMasterBO.Instance.FindByPK(id);

            frmEmployeeChamCongDetail frm = new frmEmployeeChamCongDetail();
            frm.chamCongMasterModel = employeeChamCong;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataMaster();
            }

        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            btnViewDetail_Click(null, null);
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            try
            {
                int Month = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colMonth));
                int Year = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colYear));
                int MasterID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));

                if (MasterID <= 0)
                {
                    return;
                }

                EmployeeChamCongMasterModel employeeChamCong = (EmployeeChamCongMasterModel)EmployeeChamCongMasterBO.Instance.FindByPK(MasterID);

                frmEmployeeChamCong frm = new frmEmployeeChamCong();
                frm.employeeChamCong = employeeChamCong;

               
                EmployeeChamCongDetailModel detailModel = SQLHelper<EmployeeChamCongDetailModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.EmployeeChamCongDetail WHERE MasterID = {MasterID}");

                if (detailModel.ID <= 0)
                {
                    TextUtils.ExcuteProcedure("spInsertIntoEmployeeChamCongDetail"
                                                , new string[] { "@MasterID", "@Month", "@Year", "@EmployeeID", "@LoginName" }
                                                , new object[] { MasterID, Month, Year, 0, Global.AppCodeName });
                }

                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
    }
}
