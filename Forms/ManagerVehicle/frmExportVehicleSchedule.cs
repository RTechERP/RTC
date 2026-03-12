using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmExportVehicleSchedule : _Forms
    {
        public DateTime date;
        public frmExportVehicleSchedule()
        {
            InitializeComponent();
        }

        private void ExportVehicleSchedule_Load(object sender, EventArgs e)
        {
            gridBand1.Caption = $"LỊCH TRÌNH XE NGÀY {date.ToString("dd/MM/yyyy")}";
            loadVehicleSchedule();
        }
        void loadVehicleSchedule()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            DataTable dt = TextUtils.LoadDataFromSP("spGetVehicleBookingManagementExcel", "A", 
                                                    new string[] { "@DateStart", "@DateEnd" }, 
                                                    new object[] { dateStart, dateEnd });
            grdData.DataSource = dt;
        }

        string[] colors = { "#81D4FA", "#64FFDA", "#C8E6C9", "#DCE775", "#FFEE58", "#FF9800", "#BDBDBD", "#CFD8DC", "#18FFFF", "#64FFDA",
                            "#81D4FA", "#64FFDA", "#C8E6C9", "#DCE775", "#FFEE58", "#FF9800", "#BDBDBD", "#CFD8DC", "#18FFFF", "#64FFDA" };
        private  void CustomizeCellEventHandler(CustomizeCellEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                string[] colors = { "#81D4FA", "#64FFDA", "#C8E6C9", "#DCE775", "#FFEE58", "#FF9800", "#BDBDBD", "#CFD8DC", "#18FFFF", "#64FFDA",
                                    "#81D4FA", "#64FFDA", "#C8E6C9", "#DCE775", "#FFEE58", "#FF9800", "#BDBDBD", "#CFD8DC", "#18FFFF", "#64FFDA" };
                int index = Math.Abs(e.RowHandle) - 1;
                if (colors.Length > index)
                {
                    //e.Appearance.BackColor = Color.FromName(colors[index]);
                    e.Formatting.BackColor = ColorTranslator.FromHtml(colors[index]);
                }
            }
            e.Handled = true;
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichTrinhXe_{date.ToString("ddMMyy")}.xlsx");
                //XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                //optionsEx.CustomizeCell -= CustomizeCellEventHandler;

                //grvData.ExportToXlsx(filepath, optionsEx);
                //Process.Start(filepath);


                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                optionsEx.CustomizeCell -= CustomizeCellEventHandler;
                optionsEx.CustomizeCell += CustomizeCellEventHandler;
                optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;

                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

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
        private static int GetGroupRowCount(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            for (int i = -1; i >= int.MinValue; i--)
            {
                if (!view.IsValidRowHandle(i))
                    return -(i + 1);
            }
            return 0;
        }

        private int _groupIndex = 0;
        private void grvData_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //return;
            string[] colors = { "#81D4FA", "#64FFDA", "#C8E6C9", "#DCE775", "#FFEE58", "#FF9800", "#BDBDBD", "#CFD8DC", "#18FFFF", "#64FFDA",
                                "#81D4FA", "#64FFDA", "#C8E6C9", "#DCE775", "#FFEE58", "#FF9800", "#BDBDBD", "#CFD8DC", "#18FFFF", "#64FFDA" };
            int index = Math.Abs(e.RowHandle) - 1;
            if (colors.Length > index)
            {
                //e.Appearance.BackColor = Color.FromName(colors[index]);
                e.Appearance.BackColor = ColorTranslator.FromHtml(colors[index]);
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

                grvData.AppearancePrint.GroupRow.BackColor= ColorTranslator.FromHtml(colors[index]);
            }
        }

        private void grvData_GroupLevelStyle(object sender, DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventArgs e)
        {
            
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadVehicleSchedule();
        }
    }
}