using DevExpress.XtraGrid;
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
    public partial class frmHistoryIsApprovedBillLog : _Forms
    {
        public frmHistoryIsApprovedBillLog()
        {
            InitializeComponent();
        }

        private void frmHistoryIsApprovedBillLog_Load(object sender, EventArgs e)
        {
            cboWarehouse.SelectedIndex = 0;
            cboBillType.SelectedIndex = 0;
            LoadEmployee();
            LoadData();
        }

        void LoadData()
        {
            int billtype = cboBillType.SelectedIndex;
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            int warehouseID = cboWarehouse.SelectedIndex;

            DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryIsApprovedBillLog", "A",
                                                     new string[] { "@BillType", "@EmployeeID", "@WarehouseID" },
                                                     new object[] { billtype,employeeID,warehouseID });

            grdData.DataSource = dt;


            var summarys = grvData.Columns[colStatusBillText.FieldName].Summary;
            if (summarys.Count > 0)
            {
                grvData.Columns[colStatusBillText.FieldName].Summary.Clear();
            }

            var dataGet = dt.Select("StatusBill = 1");
            var dataCancel = dt.Select("StatusBill = 0");

            grvData.Columns[colStatusBillText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colStatusBillText.FieldName, $"Tổng nhận = {dataGet.Length.ToString()}"));
            grvData.Columns[colStatusBillText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colStatusBillText.FieldName, $"Tổng huỷ = {dataCancel.Length.ToString()}"));
        }


        void LoadEmployee()
        {
            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A",
            //                                        new string[] { "@Status" },
            //                                        new object[] { -1 });


            DataTable dt = TextUtils.Select("SELECT * FROM Users");

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    var status = grvData.GetRowCellValue(e.RowHandle, colStatusBill);
            //    if (status != null)
            //    {
            //        if (TextUtils.ToInt(status) == 1)
            //        {
            //            e.Appearance.BackColor = Color.Lime;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = Color.Orange;
            //        }
            //    }
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichSuHuyNhanChungTu_{DateTime.Now.ToString("ddMMyy")}.xlsx");

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

        private void cboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
