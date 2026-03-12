using BMS.Model;
using BMS.Utils;
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
    public partial class frmPaymentOrderLog : _Forms
    {
        public frmPaymentOrderLog()
        {
            InitializeComponent();
        }

        private void frmPaymentOrderLog_Load(object sender, EventArgs e)
        {
            LoadPaymentOrder();
            //LoadEmployee();
            LoadData();
        }

        void LoadPaymentOrder()
        {
            List<PaymentOrderModel> list = SQLHelper<PaymentOrderModel>.FindAll().OrderByDescending(x => x.DateOrder).ToList();
            cboPaymentOrder.Properties.ValueMember = "ID";
            cboPaymentOrder.Properties.DisplayMember = "Code";
            cboPaymentOrder.Properties.DataSource = list;
        }
        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }


        void LoadData()
        {
            int paymentOrderID = TextUtils.ToInt(cboPaymentOrder.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            var exp1 = new Expression("PaymentOrderID", paymentOrderID);
            //var exp1 = new Expression("PaymentOrderID", paymentOrderID);
            List<PaymentOrderLogModel> list = SQLHelper<PaymentOrderLogModel>.ProcedureToList("spGetPaymentOrderLog", new string[] { "@PaymentOrderID" }, new object[] { paymentOrderID });
            grdData.DataSource = list;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichSuDuyetKhongDuyet_{cboPaymentOrder.Text}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
