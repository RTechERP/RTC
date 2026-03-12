using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
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
    public partial class frmJobRequeirementFurchaseRequest : _Forms
    {
        //public string listJobRequirementID;
        //DataTable dtPurchaseRequest = new DataTable();

        public int jobRequirementID = 0;
        public frmJobRequeirementFurchaseRequest()
        {
            InitializeComponent();
        }

        private void frmJobRequeirementFurchaseRequest_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+2).AddDays(-1);

            LoadJobRequirement();
            LoadData();
        }
        void LoadJobRequirement()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,NumberRequest FROM dbo.JobRequirement");
            List<JobRequirementModel> listJobs = SQLHelper<JobRequirementModel>.FindByAttribute("IsDeleted", 0);
            cboJobRequirement.Properties.DisplayMember = "NumberRequest";
            cboJobRequirement.Properties.ValueMember = "ID";
            cboJobRequirement.Properties.DataSource = listJobs;
            cboJobRequirement.EditValue = jobRequirementID;
        }
        void LoadData()
        {
            try
            {
                Lib.LockEvents = true;

                DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
                DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
                string keyword = txtKeyword.Text.Trim();
                int jobRequirementID = TextUtils.ToInt(cboJobRequirement.EditValue);

                DataTable dtPurchaseRequest = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New_Khanh", "A",
                                                        new string[] { "@DateStart", "@DateEnd", "@Keyword", "@JobRequirementID" },
                                                        new object[] { dateStart, dateEnd, keyword, jobRequirementID });

                grdData.DataSource = dtPurchaseRequest;
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboJobRequirement_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column != colQuantityActual) return;
            decimal quantityActual = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colQuantityActual));
            if (quantityActual > 0)
            {
                e.Appearance.BackColor = Color.FromArgb(128, 255, 255);
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachYeuCauMuaHang_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachYeuCauMuaHang_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                bool isDeleted = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsDeleted));
                if (isDeleted)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                    e.HighPriority = true;
                }
            }
        }
    }
}