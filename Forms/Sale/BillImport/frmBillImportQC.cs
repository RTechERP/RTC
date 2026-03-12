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
using System.Collections;
using DevExpress.XtraPrinting;
using static Forms.Classes.cGlobVar;
using System.Net.NetworkInformation;
using BMS.Utils;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BMS
{
    public partial class frmBillImportQC : _Forms
    {
        public frmBillImportQC()
        {
            InitializeComponent();
        }

        private void frmBillImportQC_Load(object sender, EventArgs e)
        {
            dtpDs.Value = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).Day, 0, 0, 0);
            dtpDe.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            LoadEmployeeRequest();
            LoadData();
        }

        void LoadData()
        {
            grdData.DataSource = null;
            DateTime dateStart = new DateTime(dtpDs.Value.Year, dtpDs.Value.Month, dtpDs.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDe.Value.Year, dtpDe.Value.Month, dtpDe.Value.Day, 23, 59, 59);
            int employeeRequestID = TextUtils.ToInt(cboEmployeeRequest.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportRequestQC", "A",
                new string[] { "@DateStart", "@DateEnd", "@EmployeeRequestID", "@Keyword" },
                new object[] { dateStart, dateEnd, employeeRequestID, txtKeyWord.Text.Trim() });
                grdData.DataSource = dt;
                LoadDetails();
            }
        }

        void LoadDetails()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable dtDetails = TextUtils.LoadDataFromSP("spGetBillImportRequestQCDetail", "A",
                new string[] { "@BillImportRequestID" },
                new object[] { id });
            grdDetail.DataSource = dtDetails;
        }

        void LoadEmployeeRequest()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployeeRequest.Properties.DataSource = dt;
            cboEmployeeRequest.Properties.ValueMember = "ID";
            cboEmployeeRequest.Properties.DisplayMember = "FullName";
        }

        private void btnAddRequestQC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBillImportQCDetail frm = new frmBillImportQCDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEditRequestQC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;

            int rowHandle = grvData.FocusedRowHandle;

            BillImportQCModel model = SQLHelper<BillImportQCModel>.FindByID(id);
            frmBillImportQCDetail frm = new frmBillImportQCDetail();
            frm.billImportQC = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDeleteRequestQC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colID));

            List<BillImportQCDetailModel> lsDetail = SQLHelper<BillImportQCDetailModel>.FindByAttribute(BillImportQCDetailModel_Enum.BillImportQCID.ToString(), id);

            if (lsDetail.Any(x => x.Status != 0) && !(Global.IsAdmin && Global.EmployeeID <= 0))
            {
                MessageBox.Show("Bạn không thể xóa do đã có sản phẩm được QC!", "Thông báo", MessageBoxButtons.OK);
                return;
            }


            string requestCode = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, colRequestCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa yêu cầu QC [{requestCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            List<BillImportQCDetailModel> lsDetails = SQLHelper<BillImportQCDetailModel>.FindByAttribute(BillImportQCDetailModel_Enum.BillImportQCID.ToString(), id);

            foreach (var item in lsDetails)
            {
                Dictionary<string, object> newDictDetail = new Dictionary<string, object>()
                {
                    {BillImportQCDetailModel_Enum.IsDeleted.ToString(), true },
                    {BillImportQCDetailModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                    {BillImportQCDetailModel_Enum.UpdatedDate.ToString(), DateTime.Now }
                };
                SQLHelper<BillImportQCDetailModel>.UpdateFieldsByID(newDictDetail, item.ID);
            }

            Dictionary<string, object> newDict = new Dictionary<string, object>()
            {
                {BillImportQCModel_Enum.IsDeleted.ToString(), true },
                {BillImportQCModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                {BillImportQCModel_Enum.UpdatedDate.ToString(), DateTime.Now }
            };
            SQLHelper<BillImportQCModel>.UpdateFieldsByID(newDict, id);
            LoadData();
        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        private void cboEmployeeRequest_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetails();
        }

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            
        }

        private void grvDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            //DateTime deadline = TextUtils.ToDate3(grvData.GetFocusedRowCellValue(colDealine));
            //if (deadline < DateTime.Now)
            //{
            //    string status = TextUtils.ToString(grvDetail.GetRowCellValue(e.RowHandle, colStatus));
            //    if (status == "")
            //    {
            //        e.Appearance.BackColor = System.Drawing.Color.Red;
            //        e.Appearance.ForeColor = System.Drawing.Color.White;
            //    }
            //}
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEditRequestQC_ItemClick(null, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                GridView gridView = (GridView)sender;
                string value = TextUtils.ToString(gridView.GetFocusedRowCellValue(gridView.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column != colRequestCode) return;
            if (e.RowHandle >= 0)
            {
                DateTime deadline = TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDealine));
                int id = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colID));
                int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "Status"));
                //List<BillImportQCDetailModel> lsDetail = SQLHelper<BillImportQCDetailModel>.FindByAttribute(BillImportQCDetailModel_Enum.BillImportQCID.ToString(), id);

                if (deadline < DateTime.Now && status == 0)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Orange;
                    //e.Appearance.ForeColor = System.Drawing.Color.White;
                }
                else if (status == 1) //Đã hoàn thành
                {
                    e.Appearance.BackColor = System.Drawing.Color.Lime;
                }
            }
        }
    }
}
