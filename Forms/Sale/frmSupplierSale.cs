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
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace BMS
{
    public partial class frmSupplierSale : _Forms
    {
        public frmSupplierSale()
        {
            InitializeComponent();
        }

        private void frmSupplierSale_Load(object sender, EventArgs e)
        {
            loadSupplierSale();
        }

        void loadSupplierSale()
        {
            //int ID = TextUtils.ToInt(grdData.FocusedView);
            //DataTable dt = TextUtils.LoadDataFromSP("spLoadSupplierSale", "A", new string[] { "@ID" }, new object[] { @ID });
            //grdData.DataSource = dt;


            string keyword = TextUtils.ToString(txtFilterText.Text.Trim());
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Text);

            DataSet data = TextUtils.LoadDataSetFromSP("spFindSupplierNCC",
                                                    new string[] { "@Find", "@PageNumber", "@PageSize" },
                                                    new object[] { keyword, pageNumber, pageSize });
            //DataColumn data = new DataColumn("Location", typeof(Byte[]));
            //dt.Columns.Add(data);
            //grdData.DataSource = dt;

            grdData.DataSource = data.Tables[0];
            txtTotalPage.Text = TextUtils.ToString(data.Tables[1].Rows[0]["TotalPage"]);
        }

        /// <summary>
        /// creat tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var focusrow = grvData.FocusedRowHandle;
            //SupplierSaleModel model = new SupplierSaleModel();
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            //frm.oSupplierSaleModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadSupplierSale();
                grvData.FocusedRowHandle = focusrow;
                grvData_FocusedRowChanged(null, null);
            }
        }
        /// <summary>
        /// fix tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            editDataProduct();
        }

        /// <summary>
        /// void edit data in productRTC
        /// </summary>
        int _rownIndex = 0;
        private void editDataProduct()
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            SupplierSaleModel model = (SupplierSaleModel)SupplierSaleBO.Instance.FindByPK(ID);
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            int rownIndex = grvData.FocusedRowHandle;
            frm.supplier = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                loadSupplierSale();
                grvData.FocusedRowHandle = rownIndex;
                grvData_FocusedRowChanged(null, null);
            }
        }
        /// <summary>
        /// delete sản phẩm khỏi danh sách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string Locationcode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCodeNCC));
            if (ID == 0) return;
            if (MessageBox.Show($"Bạn có chắc chắn muốn nhà cung cấp mã [{Locationcode}] không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //SupplierSaleBO.Instance.Delete(ID);
                SupplierSaleModel supplier = SQLHelper<SupplierSaleModel>.FindByID(ID);
                if (supplier == null) return;
                supplier.IsDeleted = true;
                SQLHelper<SupplierSaleModel>.Update(supplier);
                grvData.DeleteSelectedRows();
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmSupplierSaleExcel frmExcel = new frmSupplierSaleExcel();
            if (frmExcel.ShowDialog() == DialogResult.OK)
            {
                loadSupplierSale();
            }
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            //findData();

            loadSupplierSale();
        }

        /// <summary>
        /// hàm tìm kiếm sản phẩm
        /// </summary>
        //private void findData()
        //{

        //    //if (_data == "")
        //    //{
        //    //    loadSupplierSale();
        //    //}
        //    //else
        //    {
        //        string _data = TextUtils.ToString(txtFilterText.Text.Trim());
        //        DataTable dt = TextUtils.LoadDataFromSP("spFindSupplierNCC", "A", new string[] { "@Find" }, new object[] { _data });
        //        DataColumn data = new DataColumn("Location", typeof(Byte[]));
        //        dt.Columns.Add(data);
        //        grdData.DataSource = dt;
        //    }
        //}

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    findData();
            //}
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            editDataProduct();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ArrayList arr = SupplierSaleContactBO.Instance.FindByAttribute("SupplierID", Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));
            grdContact.DataSource = arr;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    grvData.OptionsPrint.AutoWidth = false;
            //    grvData.OptionsPrint.ExpandAllDetails = false;
            //    grvData.OptionsPrint.PrintDetails = true;
            //    grvData.OptionsPrint.UsePrintStyles = true;
            //    try
            //    {
            //        grvData.ExportToXls(sfd.FileName);
            //        Process.Start(sfd.FileName);
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachNCC_{DateTime.Now.ToString("ddMMyy")}.xlsx");

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

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            btnFind_Click_1(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            btnFind_Click_1(null, null);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            btnFind_Click_1(null, null);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            btnFind_Click_1(null, null);
        }
    }
}


