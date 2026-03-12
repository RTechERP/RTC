using BMS;
using BMS.Model;
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
    public partial class frmChiTietPOKH : _Forms
    {
        public DateTime dateStart = DateTime.Now;
        public frmChiTietPOKH()
        {
            InitializeComponent();
        }

        private void frmChiTietPOKH_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = dateStart;
            loadgroupSale();
            loadCustomer();
            loadUser();
            LoadData();
        }
        private void loadgroupSale()
        {
            List<GroupSalesModel> dt = SQLHelper<GroupSalesModel>.FindAll();
            dt.Insert(0, new GroupSalesModel()
            {
                ID = 0,
                GroupSalesName = "---Tất cả---"
            });
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }
        private void loadCustomer()
        {
            List<CustomerModel> dt = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted",0);
            dt.Insert(0, new CustomerModel()
            {
                ID = 0,
                CustomerName = "---Tất cả---"
            });
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }
        private void loadUser()
        {
            List<UsersModel> dt = SQLHelper<UsersModel>.FindAll();
            dt.Insert(0, new UsersModel()
            {
                ID = 0,
                FullName = "---Tất cả---"
            });
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DataSource = dt;

        }
        private void LoadData()
        {
            DateTime dateStart = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 0, 0, 0);
            int groupSaleId = TextUtils.ToInt(cbGroup.EditValue);
            int userId = TextUtils.ToInt(cbUser.EditValue);
            int customerId = TextUtils.ToInt(cbCustomer.EditValue);
            string keywords = txtFilterText.Text;
            DataTable dt = TextUtils.LoadDataFromSP("spGetPOKHProductReturn", "LMKTable",
                                                    new string[] { "@FilterText", "@CustomerID", "@UserID", "@Group", "@StartDate", "@EndDate" },
                                                    new object[] { keywords, customerId , userId , groupSaleId , dateStart , dateEnd });
            grdData.DataSource = dt;
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colColorStatus));
            if (e.Column == colStatus)
            {
                switch (status)
                {
                    case 0:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    case 1:
                        break;
                    case 2:
                        e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                        break;
                    case 3:
                        e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                        break;
                    case 4:
                        e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                        break;
                    case 5:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichSuHangVe_{dtpStartDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    //compositeLink.Links.Add(printableComponentLink2);

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

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
    }
}
