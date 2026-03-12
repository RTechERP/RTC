using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class frmTaxEmployee : _Forms
    {
        DataTable dt = new DataTable();
        public frmTaxEmployee()
        {
            InitializeComponent();
        }

        private void frmTaxEmployee_Load(object sender, EventArgs e)
        {
            cboEmployeeStatus.SelectedIndex = 1;
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTaxShowStaff frm = new frmTaxShowStaff();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        void LoadData()
        {
            int status = cboEmployeeStatus.SelectedIndex - 1;
            dt = TextUtils.LoadDataFromSP("spGetTaxEmployee", "A",
                                                    new string[] { "@Status", "@Keyword" },
                                                    new object[] { status, txtKeyword.Text.Trim() });
            grdData.DataSource = dt;

        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            bool isColumnChecklist = bandChecklist.Columns.Contains((BandedGridColumn)e.Column);
            if (isColumnChecklist || e.Column == colTinhTrangCapDongPhuc)
            {
                bool isChecked = TextUtils.ToBoolean(e.Value);
                e.DisplayText = isChecked ? "x" : "";
            }

            if (isColumnChecklist || e.Column == colOurSide)
            {
                int employeeID = TextUtils.ToInt(e.Value);
                e.DisplayText = employeeID <= 0 ? "x" : "";
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            TaxEmployeeModel model = SQLHelper<TaxEmployeeModel>.FindByID(id);
            if (model == null) return;
            frmTaxShowStaff frm = new frmTaxShowStaff();
            frm.Model = model;
            if (frm.ShowDialog() == DialogResult.OK) ;
            {
                LoadData();
                grvData.FocusedRowHandle = rowhandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowhandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            string name = grvData.GetFocusedRowCellValue(colFullName).ToString();
            TaxEmployeeModel model = (TaxEmployeeModel)TaxEmployeeBO.Instance.FindByPK(id);
            if (model == null) return;

            frmTaxEmployeeDelete frm = new frmTaxEmployeeDelete();
            frm.taxEmployee = model;
            frm.lblMessage.Text = $"Bạn có thực sự muốn chuyển trạng thái của nhân viên [{name}] thành Ngừng hoạt động?";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowhandle;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = row;
        }

        private void cboEmployeeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            LoadData();
            grvData.FocusedRowHandle = row;
        }

        private void btnPickEmployee_Click(object sender, EventArgs e)
        {
            frmTaxEmployeePick frm = new frmTaxEmployeePick();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void OptionsEx_CustomizeCell(DevExpress.Export.CustomizeCellEventArgs e)
        {
            try
            {
                foreach (BandedGridColumn item in bandChecklist.Columns)
                {
                    bool isChecked = TextUtils.ToBoolean(e.Value);
                    if (isChecked)
                    {
                        e.Value = "ok";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
            finally
            {
                e.Handled = true;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachNhanVien_{DateTime.Now.ToString("ddMMyy")}.xls");

                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.ExportMode = XlsExportMode.SingleFilePageByPage;
                optionsEx.CustomizeCell += OptionsEx_CustomizeCell;
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;
                grvData.OptionsPrint.AutoWidth = false;
                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXls(filepath, optionsEx);
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
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus));
                int isExpireContract = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colIsExpireContract));
                if (status == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 231, 187);
                    e.Appearance.ForeColor = Color.Black;
                    //e.HighPriority = true;
                }
                else if (isExpireContract == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(204, 32, 39);
                    e.Appearance.ForeColor = Color.White;
                    //e.HighPriority = true;
                }
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Space)
            {
                grvData.ClearColumnsFilter();
            }
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }

        private void chkIsExpireContract_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsExpireContract.Checked)
            {
                string filterString = $"([IsExpireContract] = 1)";
                grvData.Columns["IsExpireContract"].FilterInfo = new ColumnFilterInfo(filterString);
            }
            else
            {
                grvData.ClearColumnsFilter();
            }
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int taxEmployeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (taxEmployeeID <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn một nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}

            frmTaxEmployeeContract frm = new frmTaxEmployeeContract();
            frm.taxEmployeeID = taxEmployeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }
    }
}
