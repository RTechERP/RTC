//using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExport : _Forms
    {
        public frmExport()
        {
            InitializeComponent();
        }

        private void frmExport_Load(object sender, EventArgs e)
        {

        }

        private void pdfFilePrintBarItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            //pdfViewer1.Print();
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  XtraReport report = new XtraReport();
          //  PdfExportOptions options =report.ExportOptions.Pdf;
          //  options.PdfACompatibility = PdfACompatibility.PdfA1b;
          ////  options.PasswordSecurityOptions.PermissionsPassword = "pwd";
          //  options.ShowPrintDialogOnOpen = true;
          //  IList<string> result = options.Validate();
          //  if (result.Count > 0)
          //      Console.WriteLine(String.Join(Environment.NewLine, result));
          //  else
          //      report.ExportToPdf("Result.pdf", options);
        
         }
    }
}
