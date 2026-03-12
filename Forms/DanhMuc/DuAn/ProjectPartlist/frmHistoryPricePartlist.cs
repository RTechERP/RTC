using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
using DevExpress.Utils;

namespace BMS
{
    public partial class frmHistoryPricePartlist : _Forms
    {
        public frmHistoryPricePartlist()
        {
            InitializeComponent();
        }

        private void frmHistoryPricePartlist_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadSupplierSale();

            LoadData();
        }
        void LoadData()
        {
            string keyword = txtKeyword.Text.Trim();
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int supplierSaleID = TextUtils.ToInt(cboSupplier.EditValue);
            grdData.DataSource = null;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                DataTable dt = TextUtils.GetDataTableFromSP("spGetHistoryPricePartlist",
                                                    new string[] { "@Keyword", "@ProjectID", "@SupplierSaleID" },
                                                    new object[] { keyword, projectID, supplierSaleID });
                grdData.DataSource = dt;
            }

            
        }
        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();

            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
        }
        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.NgayUpdate).ToList();
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "CodeNCC";
            cboSupplier.Properties.DataSource = list;
        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboProductRTC_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) LoadData();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichSuGia{DateTime.Now:ddMMyy}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                //optionsEx. = DevExpress.Utils.DefaultBoolean.False;

                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }
        }
    }
}