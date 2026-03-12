using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmGetAllPO : _Forms
    {
        public frmGetAllPO()
        {
            InitializeComponent();
        }

        private void frmGetAllPO_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = dtpDateStart.Value.AddMonths(-1);
            LoadSupplierSale();
            LoadEmployee();

            LoadData();

        }
        private void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            string keyword = txtKeyword.Text.Trim();
            int supplierId = TextUtils.ToInt(cboSupplier.EditValue);
            int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            int status = cboStatus.SelectedIndex;
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataTable data = TextUtils.LoadDataFromSP("sp_GetAllPONCC", "A",
                                                   new string[] { "@FilterText", "@DateStart", "@DateEnd", "@SupplierID", "@Status", "@EmployeeID" },
                                                   new object[] { keyword, dateStart, dateEnd, supplierId, status, employeeId });
                grdData.DataSource = data;
            }

        }
        void LoadSupplierSale()
        {
            DateTime ngayUpdate = new DateTime(2024, 04, 04);
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().Where(x => x.NgayUpdate >= ngayUpdate).ToList();
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.DataSource = list;
        }
        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.EditValue = Global.EmployeeID;
        }
        private void grdData_Click(object sender, EventArgs e)
        {
        }
        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Files|*.xlsx";
            dialog.FileName = $"DanhSachPO_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"DanhSachPO_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                string filepath = dialog.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdDataMisa;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);
                        //compositeLink.Links.Add(printableComponentLink2);
                        //compositeLink.Links.Add(printableComponentLink3);

                        compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

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

        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            e.SheetName = e.Index == 0 ? $"PM NỘI BỘ" : "PM MISA";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = rowHandle;
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    LoadData();
            //}
        }
        private void grdData_Click_1(object sender, EventArgs e)
        {
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmImportExcelPONCC frm = new frmImportExcelPONCC();
            frm.Show();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
        }

        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSetDetailAllPO frm = new frmSetDetailAllPO();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnSearch_Click(null, null);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                MessageBox.Show("Các thông tin của RTC không được thay đổi!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            PONCCHistoryModel poh = SQLHelper<PONCCHistoryModel>.FindByID(id);
            frmSetDetailAllPO frm = new frmSetDetailAllPO();
            frm.poh = poh;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] rowSelected = grvData.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn dòng muốn xoá!", "Thông báo");
                return;
            }

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id > 0)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá dòng đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    SQLHelper<PONCCHistoryModel>.DeleteModelByID(id);
                    btnSearch_Click(null, null);
                }
            }
            else MessageBox.Show("Các thông tin của RTC không được xóa!", "Thông báo", MessageBoxButtons.OK);


        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit checkEdit = (CheckEdit)sender;
            int rowHandle = grvData.FocusedRowHandle;

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPONCCID));
            if (id <= 0) return;
            var myDict = new Dictionary<string, object>()
            {
                {grvData.FocusedColumn.FieldName, checkEdit.Checked},
                {"UpdatedBy", Global.AppUserName},
                {"UpdatedDate", DateTime.Now},
            };

            SQLHelper<PONCCModel>.UpdateFieldsByID(myDict, id);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode ==  Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        int rowHandle = 0;
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID.FieldName));
            if (id <= 0) return;
            PONCCModel po = SQLHelper<PONCCModel>.FindByID(id);
            frmPONCCDetailNew frm = new frmPONCCDetailNew();
            frm.po = po;
            frm.Tag = po.BillCode;
            TextUtils.OpenChildForm(frm, null);
            frm.FormClosed += Frm_FormClosed;

        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmPONCCDetailNew frm = (frmPONCCDetailNew)sender;
            if (frm.IsSave)
            {
                btnSearch_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;
            }
        }
    }
}
